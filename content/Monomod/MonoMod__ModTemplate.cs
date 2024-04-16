using BepInEx;
using BepInEx.Logging;
#if (UseHookGen)
using System.Reflection;
using MonoMod.RuntimeDetour.HookGen;
#else
using System.Collections.Generic;
using MonoMod.RuntimeDetour;
using HarmonyLib;
#endif
using MonoMod__ModTemplate.Patches;

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

        HookAll();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void HookAll()
    {
        Logger.LogDebug("Hooking...");

        ExampleShoppingCartPatch.Init();

        Logger.LogDebug("Finished Hooking!");
    }

    internal static void UnhookAll()
    {
        Logger.LogDebug("Unhooking...");

#if (UseHookGen)
        /*
         *  HookEndpointManager is from MonoMod.RuntimeDetour.HookGen, and is used by the MMHOOK assemblies.
         *  We can unhook all methods hooked with HookGen using this.
         *  Or we can unsubscribe specific patch methods with 'On.Namespace.Type.Method -= CustomMethod;'
         */
        HookEndpointManager.RemoveAllOwnedBy(Assembly.GetExecutingAssembly());
#else
        foreach (var detour in Hooks)
            detour.Undo();
        Hooks.Clear();
#endif

        Logger.LogDebug("Finished Unhooking!");
    }
}
