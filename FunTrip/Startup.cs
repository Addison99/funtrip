using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using FunTrip.Mapper;
using AutoMapper;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FunTrip
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                         builder => builder
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                         );

                }
                );
            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            //Tao mapper
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //Khoi tao REpository
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAreaGroupRepository, AreaGroupRepository>();
            services.AddSingleton<IAreaRepository, AreaRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICityRepository, CityRepository>();
            services.AddSingleton<IDistrictOutsideRepository, DistrictOutsideRepository>();
            services.AddSingleton<IDistrictRepository, DistrictRepository>();
            services.AddSingleton<IDriverRepository, DriverRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IRoleRepository, RoleRepository>();
            services.AddSingleton<IVehicleRepository, VehicleRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)


                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });            ;
            //services.AddAuthentication()
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FunTrip", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FunTrip v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FunTrip v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
