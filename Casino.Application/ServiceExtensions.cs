using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Casino.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient<IEncryptService, EncryptService>();
            //services.AddTransient<IJwtService, JwtService>();
            //services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<IApiService, ApiService>();
            //services.AddTransient<IFileService, FileService>();
        }
    }
}
