using Heartbeats.Infrastructure.Interfaces;
using Heartbeats.Infrastructure.Managers;
using Heartbeats.Infrastructure.Service;
using System.Diagnostics;

namespace Heartbeats.Extensions
{
    public static class ChatbotServiceExtension
    {
        public static IServiceCollection AddChatbotServices(this IServiceCollection services)
        {
            services.AddSingleton<IProcessExecutor, ProcessExecutor>();
            services.AddSingleton<IVirtualEnvironmentManager, PythonVirtualEnvironmentManager>();
            services.AddHostedService<ChatbotService>();
            return services;
        }
    }
}