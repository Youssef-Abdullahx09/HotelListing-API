using DataAccess;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application
{
    static public class ServiceExtensions
    {
        //public static ILogger Log { get; private set; }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(options => options.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        //public static void ConfigreErrorHandling(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(error =>
        //    {
        //        error.Run(async context =>
        //        {
        //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //            context.Response.ContentType = "application/json";
        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if(contextFeature != null)
        //            {
        //                Log.LogError($"Something Went Wrong in the {contextFeature.Error}");
        //                await context.Response.WriteAsync(new Error
        //                {
        //                    StatusCode = context.Response.StatusCode,
        //                    Message = "Internal Server Error, Please Try Again Later"
        //                }).ToString() ;
        //            }

        //        }); 
        //    });
        //}
    }
}
