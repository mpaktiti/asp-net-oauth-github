# OAuth GitHub authentication using ASP .NET

In this tutorial we will learn how to build a very simple ASP .NET MVC application and enable GitHub authentication for it. Afterwards we will create a git repository and deploy to GitHub.

To be more specific, this article explains the following:
- [How to create a ASP .NET MVC application](#create-your-asp-net-mvc-application)
- [How to register your application in GitHub for OAuth](#register-your-application-in-github)
- [How to enable GitHub authentication in your ASP .NET app](#enable-github-authentication)
- [How to test your app](#ready-steady-go)
- [How to make your app a bit more pleasant for your users](#what-about-ux)
- [How to create a new git repository for your project ad push to GitHub](#push-to-github)
- [A list with the software I used for the project in cases something is off](#software-used)

Let's get started!

___

### Create your ASP .NET MVC application

If you already have an amazing web app and you just want to enable GitHub authentication skip this paragraph and head to the next one.

In Visual Studio go to the File menu and select _File > New > Project_

![alt text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_new_project.png "File > New > Project")

Select the _ASP .NET Web Application_ project template. Specify the name and location for your project and click on the OK button.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_new_project_02.png "Template: ASP .NET Web Application")

Select the _MVC_ template and make sure the Authentication is set to Individual User Accounts. Click _OK_ to create the application.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_new_project_03.png "ASP .NET Template: MVC")

Once the project has initialized, open the web application’s properties dialog. To do so right click on your solution and select _Properties_.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_new_project_04.png "Project Properties")

Click on _Web_ and copy the _Project Url_ value. We will need this information when specifying the Authorization callback URL in GitHub.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_new_project_05.png "Project URL")

___

### Register your application in GitHub

In order for you to use GitHub as an OAuth authentication provider in your web application, you will need to create an application in GitHub. 

If you do not already have a GitHub account, register for one following [these](https://help.github.com/articles/signing-up-for-a-new-github-account/ "Sign up for a new GitHub account") steps.

Sign in to your GitHub account and navigate to your Account settings by clicking on the _Settings_ icon on the top right of the GitHub website.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_register_app.png "GitHub Account Settings")

Navigate to the Applications section by selecting _Applications_ in the Personal Settings sidebar menu.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_register_app_02.png "Settings > Applications")

Click on _Developer Applications_ and select _Register new application_.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_register_app_03.png "Settings > Applications > Developer Applications > Register new application")

Choose a name for your application and set it at the _Application name_ field. Optionally, set a description at the _Application description_ field. Paste at the _Homepage URL_ the Project Url value you copied earlier from Visual Studio.

Select _Register application_ to save the information.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_register_app_04.png "Register a new OAuth application")

Once the application registration is complete your will be redirected to the application page. Note the _**Client ID**_ and _**Client Secret**_ information. Copy the values as you will need them shortly in order to enable the GitHub authentication in your ASP.NET MVC application.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_register_app_05.png "Copy Client ID and Client Secret")

___

### Enable GitHub authentication

Go back to Visual Studio. 

We will be using ASP .NET Identity for the authentication. There is a GitHub authentication provider we can use and it is included in the Owin.Security.Providers Nuget package.

To install this package via Nuget, select _Tools > NuGet Package Manager > Package Manager Console_

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_enable_github_auth.png "Tools > NuGet Package Manager > Package Manager Console")

The console should be displayed at the bottom of Visual Studio. Type
```
Install-Package Owin.Security.Providers
```

and press Enter. You should get a message that the package is successfully installed.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_enable_github_auth_02.png "Install-Package Owin.Security.Providers")

Using the Solution Explorer, expand the `App_Start` folder and open the `Startup.Auth.cs` file. It’s time to write some code!

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_enable_github_auth_03.png "Open file Startup.Auth.cs")

First we need to add the namespace of the provider from the Nuget package we just installed.

At the top of the file insert the following code:

```cs
using Owin.Security.Providers.GitHub;
```

Next we need to enable the GitHub provider, using the _Client ID_ and _Client Secret_ information that GitHub gave us earlier.
Inside the `ConfigureAuth` method, of the `Startup` class, type the following code:

```cs
// Enable logging in with GitHub
app.UseGitHubAuthentication(
clientId: "4ac29bea3cbc9f317ada",
clientSecret: "0c93df4a12f33c9b9450e46af1cf0612ce1878f4");
```

Set the `clientId` value to the _Client ID_ you copied earlier from GitHub.

Likewise, set the `clientSecret` value to the _Client Secret_ value.

Make sure that the values you set are exactly the same, otherwise the authentication will not work.

> ![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/paper-icon.jpg "Note") 
> _If later on you want to reset the Client Secret value at your GitHub application (because for example you published it on the web for a tutorial you were building, and it is no longer a secret), make sure that you update your ASP .NET web app as well, otherwise your authentication functionality will break._

___

### Ready Steady Go

So we created an application on GitHub and built an ASP .NET web app with enabled GitHub authentication. 
Let’s see if everything works!

Run your application by selecting the _Debug > Start Debugging_ menu item or pressing the F5 key in Visual Studio or clicking the browser button on your toolbar.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app.png "Start Debugging")

Your app will open in your browser, Google Chrome in my case.

Select the _Log In_ menu at the top.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_02.png "Select Log in")

At the _Use another service to log in_ section you see a _GitHub_ button.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_03.png "Use another service to log in > GitHub")

Click the button. You are now redirected to the GitHub website. 

If you are not logged in to your GitHub account you will be prompted to do so.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_04.png "Sign in to GitHub")

Once you enter your credentials, or if you were already logged in GitHub, you will see the _Authorize application_ popup.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_05.png "Authorize application")

GitHub prompts you to give the application permission to access your personal user data. 

Click _Authorize application_.

You are being redirected back to your application (check the URL). As a final step you need to supply your email address to complete the registration process. This is once-off and you will not see this page in the future.

Type your email and click _Register_.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_06.png "Finish logging in")

Congratulations, you are now logged into the application! :+1:

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_07.png "Successful login")

Try to Log off and on again. You will see that you will be directly logged in, without being redirected to GitHub to give the application permission to access your personal user data. That is because your application now has an access token. 

Log off from your app and let’s try something else.

Let’s go back to your GitHub profile and select _Settings > Applications > Developer applications_ and click on your app (`oauth_asp_webapp` in my case). Notice that 1 user has registered with your app.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_08.png "Authenticated users")

Click _Revoke all user tokens_.

Confirm the action in the popup GitHub displays.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_09.png "Revoke all user tokens")

Refresh the GitHub page. The users count now is back to zero.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_10.png "No authenticated users")

Go back to your web app and click Log In. Click the GitHub button from the services available for login. 

Once again you are redirected to GitHub to authorize your app. That is because you revoked the user token so your app’s access token is no longer valid.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_test_app_11.png "Authorize application")

> ![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/paper-icon.jpg "Note") You can find more information on the OAuth flow [here](https://developer.github.com/v3/oauth/)

___

### What about UX?

Well that was cute but what about user experience? I only want to use GitHub for logins, why do my users have to do so many clicks?

OK let’s go back to Visual Studio to customize our app so the _Log in_ link at the top invokes GitHub authentication directly.

Using Solution Explorer expand _Views > Shared_ and open the `_LoginPartial.cshtml` file.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_modify_ui.png "Open _LoginPartial.cshtml file")

> ![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/paper-icon.jpg "Note") Hey wait, what are these cshtml files?
> 
> The extension cshtml is used by ASP .NET web pages with Razor syntax. Razor is a markup language that lets you embed server-based code (C# in our case) into web pages. For more information on this refer to [MSDN](http://www.asp.net/web-pages/overview/getting-started/introducing-razor-syntax-c), [w3schools](http://www.w3schools.com/aspnet/razor_syntax.asp) or simply [google it](https://www.google.com), there are many informative pages out there.

The file checks if the user is authenticated.

```cs
@if (Request.IsAuthenticated)
```
If so a Welcome message is displayed and a log off option

```html
using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm", @class = "navbar-right" }))
{
	@Html.AntiForgeryToken()
	<ul class="nav navbar-nav navbar-right">
		<li> @Html.ActionLink("Hello " + User.Identity.GetUserName() + "!", "Index", "Manage", routeValues: null, htmlAttributes: new { title = "Manage" })</li>
        <li><a href="javascript:document.getElementById('logoutForm').submit()">Log off</a></li>
    </ul>
}
```

If not two options are displayed: Log In, Register
```html
<ul class="nav navbar-nav navbar-right">
	<li>@Html.ActionLink("Register", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink" })</li>
	<li>@Html.ActionLink("Log in", "Login", "Account", routeValues: null, htmlAttributes: new { id = "loginLink" })</li>
</ul>
```

What we want to do is modify the login option (hence the `else` part) so it directs the user to GitHub authentication.

Remember, when you clicked on Log in there was a nice _GitHub_ button. 

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_modify_ui_02.png "GitHub button")

Let’s display this button directly on the header.

Using Solution Explorer expand _Views > Account_ and open the `_ExternalLoginsListPartial.cshtml` file.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_modify_ui_03.png "Open _ExternalLoginsListPartial.cshtml file")

> ![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/paper-icon.jpg "Note") You can find more information about partial views [here](http://www.codeproject.com/Tips/617361/Partial-View-in-ASP-NET-MVC)

This file renders the available login providers.

It is formatted to display info at a section of the page but now we want to move this to the page header, a significantly smaller section. So we will make some changes to make things look better.

Instead of modifying this file, let’s copy it and modify our copy.

Using Solution Explorer, right click on the file and select _Copy_. 
Then right click on its parent folder (`Account`) and select _Paste_. A copy of the file is now created. 

Rename your copy to `_ExternalLoginsListPartialHeader.cshtml` and open it. We will make the following changes:
- Delete the `<h4>` tag and text
- Delete the horizontal line added by tag `<hr>`
- Change the `if` text to: GitHub authentication is broken
- Change the button’s caption to Login with `@p.AuthenticationType`

Your code should look like this:

```cs
@model GithubOAuth.Models.ExternalLoginListViewModel
@using Microsoft.Owin.Security

@{
    var loginProviders = Context.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
    if (loginProviders.Count() == 0) {
        <div>
            <p>
                OAuth with GitHub is broken
            </p>
        </div>
    }
    else {
        using (Html.BeginForm("ExternalLogin", "Account", new { ReturnUrl = Model.ReturnUrl })) {
            @Html.AntiForgeryToken()
            <div id="socialLoginList">
                <p>
                    @foreach (AuthenticationDescription p in loginProviders) {
                        <button type="submit" class="btn btn-default" id="@p.AuthenticationType" name="provider" value="@p.AuthenticationType" title="Log in using your @p.Caption account">Login with @p.AuthenticationType</button>
                    }
                </p>
            </div>
        }
    }
}

```

Save and close the file. Let’s go back to `_LoginPartial.cshtml`

Open the file and make the following changes:
- Delete the `else` clause contents (`<ul>` and `<li>`)
- Add your new partial view to the `else` clause

Your code should look like this:

```cs
@using Microsoft.AspNet.Identity
@using GithubOAuth.Models

@if (Request.IsAuthenticated)
{
    using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm", @class = "navbar-right" }))
    {
        @Html.AntiForgeryToken()

        <ul class="nav navbar-nav navbar-right">
            <li>
                @Html.ActionLink("Hello " + User.Identity.GetUserName() + "!", "Index", "Manage", routeValues: null, htmlAttributes: new { title = "Manage" })
            </li>
            <li><a href="javascript:document.getElementById('logoutForm').submit()">Log off</a></li>
        </ul>
    }
}
else
{
    <ul class="nav navbar-nav navbar-right">
        @Html.Partial("~/Views/Account/_ExternalLoginsListPartialHeader.cshtml", new ExternalLoginListViewModel { ReturnUrl = ViewBag.ReturnUrl })
    </ul>
}

```

Fire up your app, let’s test our changes.

Hopefully you see the same thing as myself, the button displayed at the page header.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_modify_ui_04.png "Login with GitHub button")

If you click your new button you are authenticated via GitHub and the button is no longer displayed at the site header.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/vs_modify_ui_05.png "Logged with GitHub")

If you click _Log off_ the button is again displayed.

___

### Push to GitHub

>**TL;DR**
This paragraph goes a bit into length to explain what the commands do. If you are familiar with git feel free to skip this paragraph. Simply generate a .gitignore file, initialize a repository, add, commit and push changes to GitHub.

Now let’s create a git repository for our app and push our code to GitHub.

If you do not have Git installed you can find instructions on how to configure it on your machine [here](http://githowto.com/).

Let's start by initializing a new repository. Open a Git Bush and navigate to the directory containing your solution.

```sh
mariap GithubOAuth $ pwd
/c/Users/mariap/Dropbox/coding/asp.net/GithubOAuth
```

Initialize a Git repository:

```sh
mariap GithubOAuth $ git init
Initialized empty Git repository in C:/Users/mariap/Dropbox/coding/asp.net/GithubOAuth/.git/

```

The next step is to generate a gitignore file. This file should be populated with all the files that should not be committed to git, it will simply ignore them. You can find the contents of this file [online](https://github.com/github/gitignore/blob/master/VisualStudio.gitignore) or generate it using Visual Studio. 

Let’s see how the latter is done. Go back to Visual Studio and select _View > Team Explorer_

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push.png "View > Team Explorer")

On the _Team Explorer_ window, click on _Settings_.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_02.png "View > Team Explorer > Settings")

Click on _Repository Settings_.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_03.png "View > Team Explorer > Settings > Repository Settings")

Locate the _Ignore & Attributes Files_ section. At the Ignore File it should say that no file was found.
Click on _Add_ to create one.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_04.png "Add .gitignore file")

A `.gitignore` file is created and populated for you.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_05.png ".gitignore file in VS")

<br><br>
![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_06.png ".gitignore file in windows explorer")

Now we are ready to add the files we want to commit to your repository.

Head back to your Git Bus and execute the following commands:

```sh
mariap (master #) GithubOAuth $ git status
On branch master

Initial commit

Untracked files:
  (use "git add <file>..." to include in what will be committed)
        .gitignore
        GithubOAuth.sln
        GithubOAuth/
nothing added to commit but untracked files present (use "git add" to track)
```

The `git status` command checks the current state of the repository and displays the results. In this case it informs us that we are on branch master and lists the untracked files (new files that have never been committed). 

Notice here that git does not list all the files in this directory, for example the `packages/` directory is omitted. The reason for that is the `.gitignore` file we created earlier. For the packages example we can see the following lines in `.gitignore`:

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/gitignore_contents.png ".gitignore file contents")


```sh
mariap (master #) GithubOAuth $ git add .
warning: LF will be replaced by CRLF in GithubOAuth/Content/bootstrap.css.
The file will have its original line endings in your working directory.
...
warning: LF will be replaced by CRLF in GithubOAuth/fonts/glyphicons-halflings-regular.svg.
The file will have its original line endings in your working directory.

```

The `git add .` command stages all new files under this directory. The next commit will include the changes staged.

> ![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/paper-icon.jpg "Note") Don't be troubled by all these _LF will be replaced by CRLF_ warnings that you might see. This is just git turning all CRLF line endings to just LF before it stores it in the commit. This is done for consistency reasons (scenarios where several developers on various OS and editors modify the same files). You can find more information [here](https://help.github.com/articles/dealing-with-line-endings/).

```sh
mariap (master #) GithubOAuth $ git status
On branch master

Initial commit

Changes to be committed:
  (use "git rm --cached <file>..." to unstage)
        new file:   .gitignore
        new file:   GithubOAuth.sln
        new file:   GithubOAuth/App_Start/BundleConfig.cs
        ...
        new file:   GithubOAuth/packages.config
```

The `git status` command is again about checking the repository status. It was ran simply to demonstrate all the files that have been staged and will be committed shortly.

Speaking of that, you are now ready to commit the staged changes to your repository. Run the following command:

```sh
mariap (master #) GithubOAuth $ git commit -m "Initial commit"

[master (root-commit) bb06108] Initial commit
warning: LF will be replaced by CRLF in GithubOAuth/Content/bootstrap.css.
The file will have its original line endings in your working directory.
...
warning: LF will be replaced by CRLF in GithubOAuth/fonts/glyphicons-halflings-regular.svg.
The file will have its original line endings in your working directory.
 78 files changed, 32356 insertions(+)
 create mode 100644 .gitignore
 create mode 100644 GithubOAuth.sln
 create mode 100644 GithubOAuth/App_Start/BundleConfig.cs
 ...
 create mode 100644 GithubOAuth/fonts/glyphicons-halflings-regular.woff
 create mode 100644 GithubOAuth/packages.config
```

Run once more git status and see what changed after the commit:

```sh
mariap (master) GithubOAuth $ git status
On branch master
nothing to commit, working directory clean
```

You are now ready to push your changes to GitHub. Since this is a new repository we first need to create the remote repository and add this new remote.

Head back to GitHub and create a new repository.

Click on the _New repository_ button and enter a name.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_07.png "New GitHub repository")

<br><br>
![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_08.png "Set GitHub repository info")

Do not initialize with a README. Click on _Create repository_.

On the next screen select the _HTTPS_ option. Copy the link and head back to your Git Bush.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_09.png "Switch to HTTPS")

Let’s add now the remote we just created using the `git remote add` command.

```sh
mariap (master) GithubOAuth $ git remote add origin https://github.com/mpaktiti/asp-net-oauth-github.git
mariap (master) GithubOAuth $ git remote -v
origin  https://github.com/mpaktiti/asp-net-oauth-github.git (fetch)
origin  https://github.com/mpaktiti/asp-net-oauth-github.git (push)
```

The `git remote –v` lists the remote repositories. 

Proceed with a `git push` to commit the changes:

```sh
mariap (master) GithubOAuth $ git push origin master
Username for 'https://github.com': maria.paktiti@gmail.com
Password for 'https://maria.paktiti@gmail.com@github.com':
Counting objects: 94, done.
Delta compression using up to 4 threads.
Compressing objects: 100% (93/93), done.
Writing objects: 100% (94/94), 425.29 KiB | 0 bytes/s, done.
Total 94 (delta 15), reused 0 (delta 0)
To https://github.com/mpaktiti/asp-net-oauth-github.git
 * [new branch]      master -> master
```

Let’s go back to GitHub and see what changed to the repository we created earlier.

![alt-text](https://github.com/mpaktiti/asp-net-oauth-github/raw/master/documentation/images/github_push_10.png "View repository at GitHub")

We can see that all the files from our local directory, except for those listed in the .gitignore file, have been uploaded to GitHub.

That's it! You can now easily share your project with anyone you want.

___

### Software used

Here is the list of the software and versions I used for this tutorial:
- Windows 7 Ultimate 64-bit
- Microsoft Visual Studio 2015
- .NET Framework 4.5.2
- Owin.Security.Providers 1.26.0
- Google Chrome Version 48.0.2564.97 m
- git version 2.5.0.windows.1

___
