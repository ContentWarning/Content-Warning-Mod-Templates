#if (GlobalPluginUsing)
global using Plugin = MonoMod._ModTemplate.MonoMod__ModTemplate;
#endif
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Harmony._ModTemplate;

#if (VanillaCompatible)
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true)]
#else
[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, false)]
#endif
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Harmony__ModTemplate : BaseUnityPlugin
{
    public static Harmony__ModTemplate Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}
