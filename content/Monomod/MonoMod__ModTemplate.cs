using BepInEx;
using BepInEx.Logging;
#if (!UseHookGen)
using System.Collections.Generic;
using MonoMod.RuntimeDetour;
using HarmonyLib;
#endif
using MonoMod._ModTemplate.Hooks;

namespace MonoMod._ModTemplate;

#if (VanillaCompatible)
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true)]
#else
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
#endif
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class MonoMod__ModTemplate : BaseUnityPlugin
{
    public static MonoMod__ModTemplate Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
#if (!UseHookGen)
    internal static List<IDetour> Hooks { get; set; } = new List<IDetour>();
#endif

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Hook();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Hook()
    {
        Logger.LogDebug("Hooking...");

        /*
#if (MMHOOKLocation != "")
         *  Subscribe with 'On.Class.Method += CustomClass.CustomMethod;' for each method you're patching.
#else
         *  Add to the Hooks list for each method you're patching with:
         *
#if (PublicizeGameAssemblies)
         *  Hooks.Add(new Hook(
         *      typeof(Class).GetMethod(nameof(Class.Method), AccessTools.allDeclared),
         *      CustomClass.CustomMethod));
#else
         *  Hooks.Add(new Hook(
         *      typeof(Class).GetMethod("Method", AccessTools.allDeclared),
         *      CustomClass.CustomMethod));
#endif
#endif
         */

#if (UseHookGen)
        On.ShoppingCart.AddItemToCart += ExampleShoppingCartPatch.AddItemToCartPostfix;
#else
        Hooks.Add(new Hook(
#if (PublicizeGameAssemblies)
                typeof(ShoppingCart).GetMethod(nameof(ShoppingCart.AddItemToCart), AccessTools.allDeclared),
#else
                typeof(ShoppingCart).GetMethod("AddItemToCart", AccessTools.allDeclared),
#endif
                ExampleShoppingCartPatch.AddItemToCartPostfix));
#endif

        Logger.LogDebug("Finished Hooking!");
    }

    internal static void Unhook()
    {
        Logger.LogDebug("Unhooking...");

#if (UseHookGen)
        /*
         *  Unsubscribe with 'On.Class.Method -= CustomClass.CustomMethod;' for each method you're patching.
         */

        On.ShoppingCart.AddItemToCart -= ExampleShoppingCartPatch.AddItemToCartPostfix;
#else
        foreach (var detour in Hooks)
            detour.Undo();
        Hooks.Clear();
#endif

        Logger.LogDebug("Finished Unhooking!");
    }
}
