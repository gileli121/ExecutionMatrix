using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using ExecutionsViewer.App.Services.Token;
using ExecutionsViewer.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExecutionsViewer.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExecutionsViewerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddScoped<ITokenService, TokenService>();

            // services.AddIdentityCore<User>
            //     (
            //         options => { options.Password.RequireNonAlphanumeric = false; }
            //     ).AddUserStore<TokenUserStore>()
            //     .AddDefaultTokenProviders()
            //     .AddSignInManager<SignInManager<User>>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddCors(); // TODO: Figure out what it does. It was from the udemy.com video
            
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer
            // (options =>
            //     {
            //         options.RequireHttpsMetadata = false;
            //         options.SaveToken = true;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuer = true,
            //             ValidateAudience = true,
            //             ValidateLifetime = true,
            //             ValidateIssuerSigningKey = true,
            //             ValidIssuer = Configuration["Jwt:Issuer"],
            //             ValidAudience = Configuration["Jwt:Issuer"],
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            //             ClockSkew = TimeSpan.Zero
            //         };
            //     }
            // );

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "ExecutionsViewer.InternalApi", Version = "v1"});
                // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Description = @"JWT Authorization header using the Bearer scheme. 
                //       Enter 'Bearer' [space] and then your token in the text input below.
                //       Example: 'Bearer 12345abcdef'",
                //     TestMethodName = "Authorization",
                //     In = ParameterLocation.Header,
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer"
                // });

                // options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             },
                //             Scheme = "oauth2",
                //             TestMethodName = "Bearer",
                //             In = ParameterLocation.Header,
                //         },
                //         new List<string>()
                //     }
                // });
            });
        }

        /*
         
        NOTE: This is the old Configure method.. It did not worked right. The generated tokens did not worked.
        I copied the whole configure method from the udemy.com course resource and now it works
        But I still leave it here in case that I missed something

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestMatrix.InternalApi v1"));
        
                app.UseCors(options => { options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseCors();
            }
        
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ExecutionsViewerDbContext>();
                context.Database.EnsureCreated();
            }
        
        
            app.UseDefaultFiles();
            app.UseStaticFiles();
        
            app.UseRouting();
        
            app.UseAuthorization();
            app.UseAuthentication();
        
            app.ConfigureExceptionHandler();
        
        
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        */


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

          

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ExecutionsViewerDbContext>();
                context.Database.EnsureCreated();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestMatrix.InternalApi v1"));
                app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }
            else
            {
                app.UseCors();
            }

            // app.ConfigureExceptionHandler();
            app.UseRouting();


            app.UseDefaultFiles();
            app.UseStaticFiles();

            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                    endpoints.MapControllers().WithMetadata(new AllowAnonymousAttribute());
                else
                    endpoints.MapControllers();
            });
        }
    }
}