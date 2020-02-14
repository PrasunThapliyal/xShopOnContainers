using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Home Controller";
        }

        [HttpGet("protectedEntity")]
        [Authorize]
        public string GetProtectedEntity()
        {
            return "This is a protected string";

            // By Adding [Authorize] to this endpoint, the following exception is generated:
            /*
                System.InvalidOperationException: Endpoint Basic.Controllers.HomeController.GetProtectedEntity (Basic) contains authorization metadata, but a middleware was not found that supports authorization.
                Configure your application startup by adding app.UseAuthorization() inside the call to Configure(..) in the application startup code. The call to app.UseAuthorization() must appear between app.UseRouting() and app.UseEndpoints(...).
                   at Microsoft.AspNetCore.Routing.EndpointMiddleware.ThrowMissingAuthMiddlewareException(Endpoint endpoint)
                   at Microsoft.AspNetCore.Routing.EndpointMiddleware.Invoke(HttpContext httpContext)
                   at Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware.Invoke(HttpContext httpContext)
                   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)
             * 
             * */
            // This means that we need to add app.UseAuthorization() in Startup
            // But once you do that, you get the following exception
            /*
                System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).
                  at Microsoft.AspNetCore.Authentication.AuthenticationService.ChallengeAsync(HttpContext context, String scheme, AuthenticationProperties properties)
                  at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
                  at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)
             * */
            // So lets add serivces.AddAuthentication("Cookies");
            // But the error remains as is, and actually you have to also add .AddCookie() etc

            // Then something interesting happens, which is not visible in Postman, but will be visible if you are using Browser
            // The Authentication middleware requires some endpoint by us that it will call for authentication
            // And we specify this in the AddCookie() as options.LoginPath = "/api/Home/Authenticate";
            // And then we must also define an endpoint at /api/Home/Authenticate

            // At /api/Home/Authenticate, we must sign-in (using the cookie scheme)
            // We use HttpContext.SignInAsync() method to do that
            // This method uses the claims principal that we pass to it as arguments
            // and sets HttpContext.User entity with those claims
            // It would also set appropriate headers in the response such that Grandmas.Cookie gets set in the browser
            // (A header by the name set-cookie in response is like asking the browser to save this cookie in its application)

            // Now, there's a bit of OAuth type redirection going on here
            // If you notice the calls in browser
            // (1) We make a call to https://localhost:5001/api/Home/protectedEntity
            // The response is a 302 redirect to https://localhost:5001/api/Home/Authenticate?ReturnUrl=%2Fapi%2FHome%2FprotectedEntity
            // (2) As the response is 302, browser will automatically make another call to
            // https://localhost:5001/api/Home/Authenticate?ReturnUrl=%2Fapi%2FHome%2FprotectedEntity
            // (3) Inside Authenticate, we call SignInAsync. We should make appropriate redirection
            // Instead we redirect to Home page
            // So the first time it actually goes to Home rather than ProtectedEntity
            // Anyhow, if you now make another call to protectedEntity, now that the call comes with a cookie
            // already set, there is no need to go Authenticate. The AuthorizationMiddleware kicks in
            // and you get redirected to protectedEntity. Lovely !
        }

        [HttpGet("Authenticate")]
        public IActionResult Authenticate()
        {
            var grandmasClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim("Gandma Says", "Very nice boi")
            };

            var identity = new ClaimsIdentity(grandmasClaims, "Grandma Claims");

            var governmentClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob Rascal"),
                new Claim("Residency Status", "Citizen")
            };

            var govtIdentity = new ClaimsIdentity(governmentClaims, "Govt Claims");

            var principal = new ClaimsPrincipal(new List<ClaimsIdentity> { identity, govtIdentity });

            HttpContext.SignInAsync(principal);

            return RedirectToAction("Get");
        }
    }
}