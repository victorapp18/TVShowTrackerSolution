using Framework.Filter.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowTracker.Webapi.Application.Configurations;

namespace TVShowTracker.Webapi.Assets
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationBuilder Builder { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Builder = new ConfigurationBuilder().SetBasePath(Path.Combine(env.ContentRootPath, "Settings"))
                                                .AddJsonFile($"appsettings.json", true);

            if (env.IsDevelopment())
            {
                Builder.AddUserSecrets<Startup>();
            }
            Builder.AddEnvironmentVariables();

            Configuration = Builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TVShowTracker.WebApi.Assets", Version = "v1" });
            });

            services.AddControllers();
            services.AddMvc(options => options.Filters.Add(new ApplicationRequestFilter()));
            services.AddSwaggerGen(setup => setup.CustomSchemaIds(t => t.ToString()));
            services.ConfigureOptions(Configuration);
            services.ConfigureDependencies(Configuration);
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 10;
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            byte[] keyJwt = Encoding.ASCII.GetBytes(Configuration.GetSection("AppKeys:jwt:clientSecret").Value);
            services.AddAuthentication(it =>
            {
                it.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                it.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(it =>
            {
                it.RequireHttpsMetadata = false;
                it.SaveToken = true;
                it.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyJwt),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TVShowTracker.WebApi.Assets v1"));
            //}

            app.UseRouting();
            app.UseCors(it => it.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureExceptionHandler(env.EnvironmentName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

