
using ApiAuth.Services;
using Application.UnitOfWork;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace ApiAuth.Extensions;
    public  static class ApplicationExtensions
    {
        public static void AddAplicacionServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();        
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuth, Auth>();
        }
    }
