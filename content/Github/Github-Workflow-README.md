# Github Workflow & TCLI

[!Warning]
This template assumes you are using MinVer for automatic versioning for builds.

## Final Setup

There are a few more steps you need to take to fully prepare your project for GitHub.

First, you need to run the following two commands in the solution directory:

```shell
dotnet new tool-manifest
dotnet tool install tcli
```

This will install the tcli tool which the GitHub workflow uses to upload to thunderstore.

Next, you need to add your Thunderstore API Token as a secret to your repo under Env. Variables.
The secret name should be `THUNDERSTORE_API_TOKEN`

Finally, add the following to your `{ProjectName}.csproj` file, within the `<Project>` area:

```xml
<ItemGroup>
    <Content Include="pack-for-thunderstore.csproj" />
</ItemGroup>
```
