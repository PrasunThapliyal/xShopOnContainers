
02 Feb 2020
===========

Authentication + Authorization App
----------------------------------

(#) Create App
	Visual Studio 2019 -> New Project -> ASP.Net Core Web Application
	-> Auth (name the project) -> ASP.Net Core 3.1
	-> Authentication: Individual User Accounts (Store user accounts in-app)
	-> Configure for HTTPS
	-> No Docker Support

(#) The app created from template uses localdb (MSSQLLocalDB)
	-> The default connection string is defined in appsettings.json
	-> The DB can be viewed in VS using View -> SQL Server Object Explorer
		-> (localdb)\MSSQLLocalDB -> Databases -> aspnet-Auth-<some guid>
	-> You can rename the DB from the default connection string in appsettings
	-> The app uses SignInManager and UserManager classes, and ApplicationDbContext inherited from IdentityDbContext
	-> Uses EF Core as ORM

(#) The default app presents a simple UI (browser) that allows you to register and login
