using AutoMapper;
using GreaterGradesBackend.Common.Extensions;
using GreaterGradesBackend.Domain.Interfaces;
using GreaterGradesBackend.Infrastructure;
using GreaterGradesBackend.Infrastructure.Repositories;
using GreaterGradesBackend.Infrastructure.UnitOfWork;
using GreaterGradesBackend.Domain.Entities;
using GreaterGradesBackend.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GreaterGradesBackend.Services.Interfaces;
using GreaterGradesBackend.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace GreaterGradesBackend.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });*/

            services.AddDbContext<GreaterGradesBackendDbContext>(options =>
                options.UseSqlServer("Server=localhost,1433;Database=GreaterGrades;User Id=sa;Password=NotPassword123!;TrustServerCertificate=True;"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IGradeService, GradeService>();


            // Register the password hasher
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


            // Configure JWT Authentication
            var jwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);

            var jwtSettings = jwtSection.Get<JwtSettings>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUserService>(provider => 
                new UserService(
                    provider.GetRequiredService<IUnitOfWork>(),
                    provider.GetRequiredService<IMapper>(),
                    provider.GetRequiredService<IPasswordHasher<User>>(),
                    jwtSettings));

            services.AddAutoMapper(typeof(Startup));

            services.AddCommonServices();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "GreaterGradesBackend API",
                    Version = "v1"
                });
            });
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GreaterGradesBackendDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreaterGradesBackend API V1"));
            }

            dbContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
