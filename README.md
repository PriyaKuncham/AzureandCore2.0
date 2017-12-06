# Azure AD With NetCore2.0

Claim Based Authentication in ASPNETCore2.0 and AzureAD.

There are 2 approaches to update Claims in ASPNETCore2.0 + Azure AD Integration. 

Code Location:\\fileshare\Trainings\SampleWebAppCore2\DotNetCoreAppliccation


Approach 1. Using ClaimIdentity to add or remove claims from IPrincipal object. This is the regular one but we need to add call HttpContext.SignOutAsync() and HttpContext.SignInAsync(). 

			var principal = HttpContext.User;

            ClaimsExtension.UpdateClaim(principal, ClaimTypes.Email, "test@gmail.com");

            ClaimsExtension.UpdateClaim(principal, ClaimTypes.Name, "test");

            ClaimsExtension.UpdateClaim(principal, "FullName", "test.test");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            var s = ClaimsExtension.GetClaimValue(principal, "FullName");

            var r = ClaimsExtension.GetClaimValue(principal, ClaimTypes.Email);


StartUp.cs

 public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })

 Approach 2. Using CookieAuthenticationEvents in StartUp.cs as below code(currently i commented it) . Use this approach, if you want to non-destructively update the user principal. 

***Note: But approach described here is triggered on every request. This can result in a large performance penalty for the app.  

 In StartUp.cs

  .AddCookie(
                o =>
                {
                    o.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = CustomCookieAuthenticationEvents.ValidateAsync
                    };

In CustomCookieAuthenticationEvents class:

  public static class CustomCookieAuthenticationEvents
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            ClaimsExtension.UpdateClaim(userPrincipal, ClaimTypes.Email, "test@gmail.com");

            ClaimsExtension.UpdateClaim(userPrincipal, ClaimTypes.Name, "test");

            ClaimsExtension.UpdateClaim(userPrincipal, "FullName", "test.test");

            context.ShouldRenew = true;

             context.ReplacePrincipal(userPrincipal);

            var s = ClaimsExtension.GetClaimValue(userPrincipal, "FullName");

            var r = ClaimsExtension.GetClaimValue(userPrincipal, ClaimTypes.Email);
            
https://github.com/Azure-Samples/active-directory-dotnet-webapp-webapi-openidconnect-aspnetcore
https://github.com/Azure-Samples/active-directory-webapp-webapi-multitenant-openidconnect-aspnetcore
https://github.com/Azure-Samples/active-directory-dotnet-webapp-openidconnect-aspnetcore-v2
https://azure.microsoft.com/en-us/resources/samples/active-directory-dotnet-webapp-openidconnect-aspnetcore/
https://auth0.com/authenticate/aspnet-core/azure-active-directory/
https://weblog.west-wind.com/posts/2017/May/15/Upgrading-to-NET-Core-20-Preview
https://stackoverflow.com/questions/36073362/replace-value-in-cookie-asp-net-core-1-0
https://shauntm.com/notes/entries/428
https://github.com/aspnet/Docs/blob/master/aspnetcore/security/authentication/cookie.md
https://marcusclasson.com/2017/08/03/adding-authentication-in-aspnetcore-2-0/
https://marcusclasson.com/2017/08/03/adding-authentication-in-aspnetcore-2-0/
https://stackoverflow.com/questions/43961571/asp-net-core-2-0-preview-1-how-to-set-up-cookie-authentication-with-custom-logi
