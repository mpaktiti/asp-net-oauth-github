# OAuth GitHub authentication using ASP .NET

In this tutorial we will learn how to build a very simple ASP .NET MVC application and enable GitHub authentication for it. 

The software I used for this tutorial includes:
* Windows 7 Ultimate 64-bit
* Microsoft Visual Studio 2015
* .NET Framework 4.5.2
* Owin.Security.Providers 1.26.0
* Google Chrome Version 48.0.2564.97 m

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