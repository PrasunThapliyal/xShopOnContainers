10 Feb 2020
-----------

Authentication/Authorization App - ASP.NET Core 3.1
Ref: 
ASP.NET Core 3 - Authentication - Ep.1 Basics (Claims/ClaimsIdentity/ClaimsPrincipal/Authorization)
Raw Coding https://www.youtube.com/watch?v=Fhfvbl_KbWo&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V
-------------------

(#) Visual Studio 2019 
	-> New Project -> ASP.NET Core Web Application -> ASP.NET Core 3.1 
	-> Empty project -> No Authentication, Configure for HTTPS, No Docker Support

(#) Basic Authentication concepts
	Ref: 
	ASP.NET Core 3 - Authentication - Ep.1 Basics (Claims/ClaimsIdentity/ClaimsPrincipal/Authorization)
	Raw Coding https://www.youtube.com/watch?v=Fhfvbl_KbWo&list=PLOeFnOV9YBa7dnrjpOG6lMpcyd7Wn7E8V
	
	app.UseAuthentication(); // Adds MS's AuthenticationMiddleware // Will ask Who you are .. 
	app.UseAuthorization(); // Adds MS's AuthorizationMiddleware // Will ask Are you allowed

	To get AuthenticationMiddleware to kick in, you have to call
	services.AddAuthentication and at a minimum the authentication scheme must
		define Cookie name ("Grandmas.Cookie")
		define login path (/api/Home/Authentication)
	Once you have done services.AddAuthentication, decorating an endpoint with [Authorize] will result in the following:
		If the request already has a cookie, then the login path endpoint will not be called and the authorization middleware
		will be invoked directly and if the cookie is good, you will be taken to the requested action directly
		If the request does not have a cookie, the login path endpoint will be called (which will sign in the user using HttpContext.SignInAsync)



