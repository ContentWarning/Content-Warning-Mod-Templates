# Content Warning MonoMod Template

Thank you for using the mod template! Here are a few tips to help you on your journey:

## Versioning

BepInEx uses [semantic versioning, or semver](https://semver.org/), for the mod's version info.
<!--#if (!UseMinVer) -->
To increment it, you can either modify the version tag in the `.csproj` file directly, or use your IDE's UX to increment the version. Below is an example of modifying the `.csproj` file directly:

```xml
<!-- BepInEx Properties -->
<PropertyGroup>
    <AssemblyName>{ModGuid}</AssemblyName>
    <Product>{Product}</Product>
    <!-- Change to whatever version you're currently on. -->
    <Version>{Version}</Version>
</PropertyGroup>
```

Your IDE will have the setting in `Package` or `NuGet` under `General` or `Metadata`, respectively.
<!--#else -->
[MinVer](https://github.com/adamralph/minver?tab=readme-ov-file#usage) will automatically
version your mod based on the latest git tag, as well as the number of commits made since then.

To create a new git tag, you can either use the git cli, or a git client,
such as GitHub Desktop, GitKraken, or the one built into your IDE.
For command line use, you can simply type in the following commands in the terminal/shell:

```shell
git tag v1.2.3
git push --tags
```

This creates a new tag, `v1.2.3`, at the currently checked-out commit,
and pushes the tag to the git version-control system (vcs).
MinVer will then be able to use this when you build your project to set your mod's version.

> **Note:** You *must* have a `v` in front of the version number, otherwise MinVer will not recognize it.
>
> If you prefer not to have `v1.2.3` and instead `1.2.3`, you can remove the `<MinVerTagPrefix>v</MinVerTagPrefix>` line in your `.csproj` file.
<!--#endif -->

## Logging

A logger is provided to help with logging to the console. You can access it by doing `Plugin.Logger` in any class outside the `Plugin` class.

***Please use*** `LogDebug()` ***whenever possible, as any other log method will be displayed to the console and potentially cause performance issues for users.***

If you chose to do so, make sure you change the following line in the `BepInEx.cfg` file to see the Debug messages:

```toml
[Logging.Console]

# ... #

## Which log levels to show in the console output.
# Setting type: LogLevel
# Default value: Fatal, Error, Warning, Message, Info
# Acceptable values: None, Fatal, Error, Warning, Message, Info, Debug, All
# Multiple values can be set at the same time by separating them with , (e.g. Debug, Warning)
LogLevels = All
```

## MonoMod

This template uses MonoMod. For more specifics on how to use it, look at
[the MonoMod Examples on lethal.wiki](https://lethal.wiki/dev/fundamentals/patching-code/monomod-examples) and
[the unofficial MonoMod Documentation on lethal.wiki](https://lethal.wiki/dev/fundamentals/patching-code/monomod-documentation).

Even though these resources are made for the Lethal Company Modding Wiki, they do apply for Content Warning modding. Only things to note are that the CW modding community uses [AutoHookGenPatcher](https://thunderstore.io/c/content-warning/p/Hamunii/AutoHookGenPatcher/) instead of the older [HookGenPatcher](https://github.com/harbingerofme/Bepinex.Monomod.HookGenPatcher), and also that AutoHookGenPatcher already depends on [DetourContext.Dispose Fix](https://thunderstore.io/c/content-warning/p/Hamunii/DetourContext_Dispose_Fix/) on Thunderstore.

See [AutoHookGenPatcher - Usage For Developers](https://github.com/Hamunii/BepInEx.MonoMod.AutoHookGenPatcher?tab=readme-ov-file#usage-for-developers) for information on how to generate MMHOOK files for assemblies other than `Assembly-CSharp.dll` or already referenced `MMHOOK` assemblies.