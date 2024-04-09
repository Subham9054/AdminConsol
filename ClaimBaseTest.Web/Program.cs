#region
//using ClaimBaseTest.Repository.Container;
//using ExceptionHandling.Middlewares;
////-------------------------------------------
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System.Security.Claims;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddCustomContainer(builder.Configuration);
//builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(20);
//    options.Cookie.Name = ".Session";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
////builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


////--------Claims Login-----------------------------------------------------------------
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option =>
//    {
//        option.LoginPath = "/CodeGenLogin/UserLogin";
//        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        option.AccessDeniedPath = "/Home/AccessDenied"; // Specify the access denied path

//    });
////var serviceProvider = builder.Services.BuildServiceProvider();
////var repository = serviceProvider.GetRequiredService<IRepository>();
////var roles = await repository.GetRolesAsync();

////string[] anshuman = { "CityPolicy", "ViewCityPolicy", "CompanyPolicy", "ViewCompanyPolicy", "EmployeePolicy", "ViewEmployeePolicy" };

////--------------------------------------------------------------------------------------------------
////builder.Services.AddAuthorization(options =>
////{
////    //foreach (string item in anshuman)
////    //{
////    //    options.AddPolicy(item, policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
////    //}
////    options.AddPolicy("CityPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
////    options.AddPolicy("ViewCityPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
////    options.AddPolicy("CompanyPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
////    options.AddPolicy("ViewCompanyPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
////    options.AddPolicy("EmployeePolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
////    options.AddPolicy("ViewEmployeePolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));

////    options.AddPolicy("ViewCityPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
////    options.AddPolicy("ViewCompanyPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
////    options.AddPolicy("ViewEmployeePolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User"));



////    //options.AddPolicy("AdminOrUserRolePolicy", policy => policy.RequireRole(roleDetails.Roles));
////    //options.AddPolicy("AdminOrUserRolePolicy", policy => policy.RequireRole(roles));
////});
////-------------------------------------------------------------------------------------


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


////app.UseSession();
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
// pattern: "{controller=CodeGenLogin}/{action=UserLogin}/{id?}");
//// // //     pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.Run();
//-------------------------------------------------------------------------------------------------------------------------
#endregion

using ClaimBaseTest.Repository.Container;
using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Repository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace MyProgram.Repository
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Newly Added
            builder.Services.AddCustomContainer(builder.Configuration);
            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.Name = ".Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //-----
            //builder.Services.AddSingleton<IRepository, LoginRepository>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //builder.Services.AddSingleton<ICodeGenLoginRepository, CodeGenLoginRepository>(); //Already present in CustomContainer.cs
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    //option.LoginPath = "/Access/Login";
                    option.LoginPath = "/CodeGenLogin/UserLogin";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    option.AccessDeniedPath = "/Home/AccessDenied"; // Specify the access denied path

                });
            #region Retrieve Roles from List,string Array & Static json Data
            //IEnumerable<string> roleDetails = new List<string> { "Admin", "User" };
            //string[] roleDetails = { "Admin", "User" };
            //D:\Claim Based Login\roles.json
            //var json = "{\"Roles\": [\"Admin\", \"User\"]}";
            #endregion

            #region Retrieve Roles from json File & DataBase
            // Read the JSON file
            //string jsonFilePath = "D:\\Claim Based Login\\roles.json"; // Specify the path to your JSON file
            //string json = File.ReadAllText(jsonFilePath);

            //var roleDetails = JsonConvert.DeserializeObject<RoleDetails>(json);

            //-------------------------------------------------------------------------------------------------
            //For retrieve Roles from DataBase
            // In your ConfigureServices method in Startup.cs or equivalent
            //builder.Services.AddSingleton<VMLogin>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DBconnectionAdConsole4")));

            //protected SqlConnection CreateConnection()
            //{
            //    return new SqlConnection(_config.GetConnectionString("DBconnectionAdConsole4"));
            //}

            //var serviceProvider = builder.Services.BuildServiceProvider();
            //var repository = serviceProvider.GetRequiredService<IRepository>();
            //var roles = await repository.GetRolesAsync();
            #endregion


            string[] Policy = { "CityMaster/City", "CityMaster/ViewCity", "CompanyMaster/Company", "CompanyMaster/ViewCompany", "EmpMaster/Employee", "EmpMaster/ViewEmployee" };
            //--------------------------------------------------------------------------------------------------
            builder.Services.AddAuthorization(options =>
            {
                //foreach (string item in Policy)
                //{
                //    options.AddPolicy(item, policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                //}
                options.AddPolicy("CityMaster/City", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
                options.AddPolicy("CityMaster/ViewCity", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("CompanyMaster/Company", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
                options.AddPolicy("CompanyMaster/ViewCompany", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("EmpMaster/Employee", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));
                options.AddPolicy("EmpMaster/ViewEmployee", policy => policy.RequireClaim(ClaimTypes.Role, "Manager"));

                //options.AddPolicy("AdminOrUserRolePolicy", policy => policy.RequireRole(roleDetails.Roles));
                //options.AddPolicy("AdminOrUserRolePolicy", policy => policy.RequireRole(roles));
            });
            //--------------------------------------------------------------------------------------------------
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Access}/{action=Login}/{id?}");
                pattern: "{controller=CodeGenLogin}/{action=UserLogin}/{id?}");

            app.Run();
        }
    }

    //--For DataBase retrieve this is required
    public class RoleDetails
    {
        public List<string> Roles { get; set; }
    }
}

