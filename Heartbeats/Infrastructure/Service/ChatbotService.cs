using Heartbeats.Infrastructure.Interfaces;
using Heartbeats.Infrastructure.Records;
using System.Diagnostics;

namespace Heartbeats.Infrastructure.Service
{
    public class ChatbotService : IHostedService
    {
        private readonly IVirtualEnvironmentManager _virtualEnvManager;
        private readonly IProcessExecutor _processExecutor;
        private readonly ILogger<ChatbotService> _logger;
        private Process? _chatbotProcess;

        public ChatbotService(
            IVirtualEnvironmentManager virtualEnvManager,
            IProcessExecutor processExecutor,
            ILogger<ChatbotService> logger)
        {
            _virtualEnvManager = virtualEnvManager;
            _processExecutor = processExecutor;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var basePath = AppContext.BaseDirectory;
                var chatbotFolderPath = Path.Combine(basePath, "..", "..", "..", "Chatbot");
                var venvPath = Path.Combine(chatbotFolderPath, "venv");

                if (!_virtualEnvManager.Exists(venvPath))
                {
                    _logger.LogInformation("Virtual environment not found. Creating it...");
                    await _virtualEnvManager.CreateAsync(venvPath, new[]
                    {
                        "python -m venv venv",
                        ".\\venv\\Scripts\\activate && pip install -r requirements.txt"
                    }, cancellationToken);
                    _logger.LogInformation("Virtual environment set up successfully.");
                }

                _logger.LogInformation("Starting chatbot...");
                _chatbotProcess = _processExecutor.StartLongRunningProcess(
                    new ProcessRequest(
                        ".\\venv\\Scripts\\activate && chainlit run .\\app.py -w -h",
                        chatbotFolderPath
                    )
                );

                _logger.LogInformation("Chatbot started successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start Python chatbot");
                throw;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_chatbotProcess != null && !_chatbotProcess.HasExited)
            {
                _logger.LogInformation("Terminating chatbot process...");
                _chatbotProcess.Kill(true);
                _chatbotProcess.Dispose();
                _chatbotProcess = null;
                _logger.LogInformation("Chatbot process terminated.");
            }
        }
    }
}