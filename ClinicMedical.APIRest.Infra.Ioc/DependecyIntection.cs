using ClinicMedical.APIRest.Application.Interfaces;
using ClinicMedical.APIRest.Application.Mappings;
using ClinicMedical.APIRest.Application.Services;
using ClinicMedical.APIRest.Domain.Acount;
using ClinicMedical.APIRest.Domain.Interface;
using ClinicMedical.APIRest.Domain.Interfaces;
using ClinicMedical.APIRest.Domain.Services;
using ClinicMedical.APIRest.Infra.Data.Context;
using ClinicMedical.APIRest.Infra.Data.Identity;
using ClinicMedical.APIRest.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsOfficeAPIRest.Infra.Ioc
{
    public static class DependecyIntection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringMysql = configuration.GetConnectionString("ConnectionMysql");
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseMySql(connectionStringMysql, ServerVersion.AutoDetect(connectionStringMysql),
                    b => b.MigrationsAssembly(typeof(AppDBContext).Assembly.FullName));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            //Repositories
            services.AddScoped<IPatientRepository, PatientRepositry>();
            services.AddScoped<IAppointmentRepository, AppointmentRepositry>();
            services.AddScoped<IDoctorRepository, DoctorRepositry>();
            services.AddScoped<IEmployeeRepository, EmployeeRepositry>();

            //Services
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IFormatterService, FormatterService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;

        }
    }
}
