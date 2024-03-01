//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace BlazorApp1
//{
//    public class Startup
//    {
//        public void ConfigureServices(IServiceCollection services)
//        {
//            // ... other configurations ...

//            services.AddCors(options =>
//            {
//                options.AddPolicy("AllowAll",
//                    builder =>
//                    {
//                        builder.AllowAnyOrigin()
//                               .AllowAnyMethod()
//                               .AllowAnyHeader();
//                    });
//            });

//            // ... other configurations ...
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            // Other middleware configurations...

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//                app.UseHttpsRedirection(); // Optionally provide HttpsRedirectionOptions here
//            }

//            // More middleware configurations...

//            app.UseStaticFiles();

//            // Other configurations...

//            app.UseRouting();

//            // Configure CORS before endpoints
//            app.UseCors("AllowAll");

//            // More configurations...

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapBlazorHub();
//                endpoints.MapFallbackToPage("/_Host");
//            });
//        }
//    }
//}
