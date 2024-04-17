# Xilo's Content Warning Templates

Harmony & MonoMod templates to get you a quick-start with making Content Warning mods!

This template is forked from Xilophor's LC Mod Templates, [available here](https://github.com/Xilophor/Lethal-Company-Mod-Templates).

## Acknowledgements

* [@funlennysub](https://github.com/funlennysub) & [@cyclozarin](https://github.com/cyclozarin) for the Russian localization.

## Installation

Use the following command in the commandline or terminal to install the templates:

```shell
dotnet new install Xilophor.ContentWarningTemplates
```

The templates will also be updated on occasion. To ensure that your copy of the template is up-to-date, use the following command:

```shell
dotnet new update
```

## Usage

There are two main methods of using the templates. The first is through Visual Studio or Rider, with a relatively intuitive UX.

For a detailed overview of terminal/commandline options, use either `cwharmony --help` for the Harmony template, or `cwmonomod --help` for the MonoMod template, like so:

```shell
dotnet new cwharmony --help
dotnet new cwmonomod --help
```

### GitHub Workflow Template

There is also a GitHub workflow template, though it is not available through any UX/UI; it is strictly command-line only. This template also requires a fair amount
of post template set-up.

The template is available running the following ***in your project directory***:

```shell
dotnet new cwgithub
```

> [!IMPORTANT]
>
> This template assumes you are using MinVer and have a `README.md` file in the root of the repo (next to the `sln` file). Failing to do meet these prerequisites will
> lead to the template not working correctly.
>
> For MinVer, please use the `UseMinVer` setting in the Harmony/MonoMod Template. Otherwise, you can add the following to your `csproj`:
>
> ```xml
> <!-- Set MinVer Tag & Prerelease Properties -->
> <PropertyGroup>
>   <MinVerDefaultPreReleaseIdentifiers>dev</MinVerDefaultPreReleaseIdentifiers>
>   <MinVerTagPrefix>v</MinVerTagPrefix>
> </PropertyGroup>
>
> <!-- Set Mod Version with MinVer -->
> <Target Name="SetModVersion" BeforeTargets="AddGeneratedFile" DependsOnTargets="MinVer">
>   <PropertyGroup>
>     <PlainVersion>$(MinVerMajor).$(MinVerMinor).$(MinVerPatch)</PlainVersion>
>     <BepInExPluginVersion>$(PlainVersion)</BepInExPluginVersion>
>   </PropertyGroup>
> </Target>
> ```

The GitHub Workflow has quite a few parameters, listed below:

`-n`: The Mod Name
  - Self-Explanatory
  - Default: The project name.

`-A`: Author ***required***
  - The author name the mod will be uploaded under. For example, "xilophor" would be valid. Note: You need the API key from the Author/Namespace to be able to upload.

`-D`: Description
  - The description of the mod. This will appear below the mod name.
  - Default: `"A mod for Content Warning."`

`-W`: Website Url
  - A website to link to, visible on the mod listing. Most commonly used to link to the GitHub repo.

`-N`: NSFW
  - Whether the mod is NSFW or not.
  - Default: `false`

`-De`: Depend On AutoHookGenPatcher
  - Whether your mod requires AutoHookGenPatcher. This is usually the case when you are using MonoMod.
  - Default: `false`

`-U`: Use a Changelog
  - Whether you want to use a CHANGELOG file. Will be created if true.
  - Default: `false`

`-Nu`: NuGet Packaging
  - Whether you are packaging your mod to be uploaded to NuGet. Should only be used for APIs/libs.
  - Default: `false`

`-Up`: Upload Debug Build
  - Whether a Debug build should be uploaded to the GitHub release. Only useful if you have any "#if (DEBUG)" preprocessing directives.
  - Default: `false`

`-L`: License
  - What LICENSE you use. This will be automatically added to the solution directory and used for NuGet packaging.
  - Default: `None`
  - Choices:
    - `None`
    - `MIT`
    - `GPL-3.0-only`
    - `LGPL-3.0-only`
    - `AGPL-3.0-only`
    - `MPL-2.0`
    - `Apache-2.0`
    - `CC-BY-NC-4.0`
    - `CC-BY-NC-SA-4.0`

## Contributing

If you'd like to add a localization, please contact me at [ryanzgit@pm.me](mailto:ryanzgit@pm.me).
