# Github Workflow & TCLI

[!Warning]
This template assumes you are using **MinVer** for automatic versioning for builds. If you are not, please
add it to your project.

## Final Setup

There are a few more steps you need to take to fully prepare your project for GitHub.

First, you need to run the following commands in the **project** directory:

```shell
cd ../
dotnet new tool-manifest
dotnet tool install tcli
```

This will install the tcli tool which the GitHub workflow uses to upload to thunderstore.

Next, add the following to your `{ProjectName}.csproj` file, within the `<Project>` area:

```xml
<Import Project="./pack-for-thunderstore.csproj" />
```

Finally, you need to setup your GitHub repo. There are a few things to do for this.

1. Enable Write Access for Actions

You must enable write access for the default `GITHUB_TOKEN` for the workflow to automatically upload files to the GitHub release.
For information on how to do so, [click here](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/enabling-features-for-your-repository/managing-github-actions-settings-for-a-repository#configuring-the-default-github_token-permissions).

2. Get Your Thunderstore API Token

You must generate a token to use for uploading to thunderstore. To do so, follow these steps:
- Open the **Settings** page by clicking on the `Settings` button in the top-right.

3. Add Your Token to GitHub
   Add your Thunderstore API Token as a secret to your repo under Repository Secrets. For information on how to do so, [click here](https://docs.github.com/en/actions/security-guides/using-secrets-in-github-actions#creating-secrets-for-a-repository).
   The secret name should be `THUNDERSTORE_API_TOKEN`.

## Usage

Once you've set everything up, you can finally start releasing through GitHub! The procedure is quite simple.

Make sure you've pushed your commits to the `main`/`master` branch of your repo. Once the `main`/`master` branch has been updated, make
a GitHub release, and create a new tag for the release with the version you're releasing. The tag needs to be in the following format:
`vX.X.X`, where `v` is a necessary prefix. For example, `v1.2.3` **would be** a **valid** tag, but `1.2.3` **would not**.

After you've added the tag; all that you need to do is add a title & description, and post the release! The workflows will handle uploading
the "artifacts" to the GitHub release and to Thunderstore for you!

The template also comes with a "CI" workflow, which can be used to make sure Pull Requests will build correctly.
