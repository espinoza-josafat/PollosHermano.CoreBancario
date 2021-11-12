using PollosHermano.CoreBancario.Common;
using PollosHermano.CoreBancario.Infraestructure.Identity.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace PollosHermano.CoreBancario.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(ConnectionStrings.IdentityConnectionString).UseLazyLoadingProxies());

            services.AddIdentity<Entities.Identity.User, Entities.Identity.Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecretKey")))
                };
            });

            services.AddScoped<Domian.Core.UnitOfWork.IPollosHermanoCoreBancarioDBUnitOfWork, Infraestructure.Core.UnitOfWork.PollosHermanoCoreBancarioDBUnitOfWork>();
            services.AddScoped<Infraestructure.Core.Factories.IPollosHermanoCoreBancarioDBFactory, Infraestructure.Core.Factories.PollosHermanoCoreBancarioDBFactory>();

            services.AddScoped<Domian.Identity.UnitOfWork.IIdentityUnitOfWork, Infraestructure.Identity.UnitOfWork.IdentityUnitOfWork>();
            services.AddScoped<Infraestructure.Identity.Factories.IIdentityFactory, Infraestructure.Identity.Factories.IdentityFactory>();

            AddScopeTypesEndsWith<Domian.Core.Dao.ISucursalDao, Data.Core.SucursalDao>(services, "Dao");
            AddScopeTypesEndsWith<Domian.Core.Repositories.ISucursalRepository, Infraestructure.Core.Repositories.SucursalRepository>(services, "Repository");
            AddScopeTypesEndsWith<Domian.Core.Services.ISucursalService, Application.Core.Services.SucursalService>(services, "Service");

            services.AddControllers().AddNewtonsoftJson(ConfigureJson);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PollosHermano.CoreBancario", Version = "v1" });

                // To Enable authorization using Swagger (JWT)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        void ConfigureJson(MvcNewtonsoftJsonOptions opts)
        {
            opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        static void AddScopeTypesEndsWith<TService, TImplementation>(IServiceCollection services, string endsWith = null) where TService : class where TImplementation : class, TService
        {
            endsWith = string.IsNullOrWhiteSpace(endsWith) ? null : endsWith.Trim();

            var interfaces = typeof(TService).Assembly.GetTypes().Where(x => x.IsInterface && (string.IsNullOrWhiteSpace(endsWith) || x.Name.EndsWith(endsWith)));
            var implementations = typeof(TImplementation).Assembly.GetTypes().Where(x => x.IsClass && (string.IsNullOrWhiteSpace(endsWith) || x.Name.EndsWith(endsWith)));

            foreach (var @interfaceBase in interfaces)
            {
                var typesImplement = implementations.Where(x => @interfaceBase.IsAssignableFrom(x));

                if (typesImplement != null && typesImplement.Any())
                {
                    var typeImplement = typesImplement.FirstOrDefault();
                    services.AddScoped(@interfaceBase, typeImplement);
                }
            }
        }

        static void AddScopeTypesStartsWith<TService, TImplementation>(IServiceCollection services, string startsWithInterfaces = null, string startsWithImplementations = null) where TService : class where TImplementation : class, TService
        {
            startsWithInterfaces = string.IsNullOrWhiteSpace(startsWithInterfaces) ? null : startsWithInterfaces.Trim();
            startsWithImplementations = string.IsNullOrWhiteSpace(startsWithImplementations) ? null : startsWithImplementations.Trim();

            var interfaces = typeof(TService).Assembly.GetTypes().Where(x => x.IsInterface && (string.IsNullOrWhiteSpace(startsWithInterfaces) || x.Name.StartsWith(startsWithInterfaces)));
            var implementations = typeof(TImplementation).Assembly.GetTypes().Where(x => x.IsClass && (string.IsNullOrWhiteSpace(startsWithImplementations) || x.Name.StartsWith(startsWithImplementations)));

            foreach (var @interfaceBase in interfaces)
            {
                var typesImplement = implementations.Where(x => @interfaceBase.IsAssignableFrom(x));

                if (typesImplement != null && typesImplement.Any())
                {
                    var typeImplement = typesImplement.FirstOrDefault();
                    services.AddScoped(@interfaceBase, typeImplement);
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PollosHermano.CoreBancario v1"));

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
