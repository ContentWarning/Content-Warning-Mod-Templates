# Github Workflow & TCLI

[!Warning]
This template assumes you are using **MinVer** for automatic versioning for builds. If you are not, please
add it to your project.

## Final Setup

There are a few more steps you need to take to fully prepare your project for GitHub.

- [ ] Finish Project Setup
  - [ ] Install TCLI

    > Run the following commands in the ***project*** directory:
    >
    > ```shell
    > cd ../
    > dotnet new tool-manifest
    > dotnet tool install tcli
    > ```
    >
    > This will install the tcli tool which the GitHub workflow uses to upload to thunderstore.

  - [ ] Add Import to `{ProjectName}.csproj`

    > Add the following to your `{ProjectName}.csproj` file, within the `<Project>` area but outside any Item or Property Groups:
    >
    > ```xml
    > <Import Project="./{ProjectName}.Packaging.targets" />
    > ```

  - [ ] Change Icon
    > Change the template `icon.png` file in `/ts-assets/` to your mod's icon. Your icon must be `256px x 256px`, as that is what is used by Thunderstore.
      Make sure the name of the file remains `icon.png`.
<!--#if (NuGetPackaging) -->
    >
    > Scale down your mod's icon to `128px x 128px` and save it as `icon_128x128.png`. NuGet uses this scale for package icons.
<!--#endif -->

  - [ ] Validate Mod/Package Info
    > You must validate the package info for Thunderstore. This can be found in `ts-assets/thunderstore.toml`. Just ensure that these properties/details are correct.
<!--#if (NuGetPackaging) -->
    >
    > You must also validate your NuGet package info. This info can be found at the top of `{ProjectName}.Packaging.targets`, within the `PropertyGroup`.
      Namely, ensure the `PackageId` and `Authors` properties contain the correct information.
<!--#endif -->

- [ ] Setup GitHub Repo

  - [ ] Enable Write Access for Actions

    > You must enable write access for the default `GITHUB_TOKEN` for the workflow to automatically upload files to the GitHub release.
For information on how to do so, [click here](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/enabling-features-for-your-repository/managing-github-actions-settings-for-a-repository#configuring-the-default-github_token-permissions).

  - [ ] Get Thunderstore API Token

    > You must generate a token to use for uploading to thunderstore. To do so, follow these steps:
    > - Open the **Settings** page by clicking on the `Settings` button in the top-right.
    > - Under `Account Settings`, select the `Teams` page on the left. A new page should load.
    > - Click on team `{Author}` within the list.
    > - Under `Team {Author}`, click on `Service Accounts`.
    > - Here, you can now click `Add service account`. This will ask you for a nickname to use. For example, `gh.{ModName}` would be a valid nickname.
    > - After hitting `Add service account` again, an API Token should appear. ***MAKE SURE YOU COPY/SAVE THIS TOKEN*** as you will not be able to access it again.
    > - You can now move on to the next step of adding this token to GitHub.

  - [ ] Add Thunderstore API Token as a GitHub Repo Secret

    > Add your Thunderstore API Token as a secret to your repo under Repository Secrets. For information on how to do so, [click here](https://docs.github.com/en/actions/security-guides/using-secrets-in-github-actions#creating-secrets-for-a-repository).
The secret name should be `THUNDERSTORE_API_TOKEN`.

<!--#if (NuGetPackaging) -->
  - [ ] Get NuGet API Key

    >   You must generate an API Key to use for uploading to NuGet. [This MSDN article](https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli#get-your-api-key) explains how to do so.

  - [ ] Add NuGet API Key as a GitHub Repo Secret

    >   Add your NuGet API Key as a secret to your repo's secrets like before. The secret name for this key should be `NUGET_API_KEY`.

<!--#endif -->
## Usage

Once you've set everything up, you can finally start releasing through GitHub! The procedure is quite simple.

Make sure you've pushed your commits to the `main`/`master` branch of your repo. Once the `main`/`master` branch has been updated, make
a GitHub release, and create a new tag for the release with the version you're releasing. The tag needs to be in the following format:
`vX.X.X`, where `v` is a necessary prefix. For example, `v1.2.3` **would be** a **valid** tag, but `1.2.3` **would not**.

After you've added the tag; all that you need to do is add a title & description, and post the release! The workflows will handle uploading
<!--#if (NuGetPackaging) -->
the "artifacts" to the GitHub release, to NuGet, and to Thunderstore for you!
<!--#else -->
the "artifacts" to the GitHub release and to Thunderstore for you!
<!--#endif -->

The template also comes with a "CI" workflow, which can be used to make sure Pull Requests or pushes to `main`/`master` will build correctly.
