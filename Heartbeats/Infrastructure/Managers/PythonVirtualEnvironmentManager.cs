using Heartbeats.Infrastructure.Interfaces;
using Heartbeats.Infrastructure.Records;

namespace Heartbeats.Infrastructure.Managers
{
    public class PythonVirtualEnvironmentManager : IVirtualEnvironmentManager
    {
        private readonly IProcessExecutor _processExecutor;
        private readonly ILogger<PythonVirtualEnvironmentManager> _logger;

        public PythonVirtualEnvironmentManager(
            IProcessExecutor processExecutor,
            ILogger<PythonVirtualEnvironmentManager> logger)
        {
            _processExecutor = processExecutor;
            _logger = logger;
        }

        public bool Exists(string path) =>
            Directory.Exists(path) && File.Exists(Path.Combine(path, "Scripts", "activate"));

        public async Task CreateAsync(string path, string[] setupCommands, CancellationToken cancellationToken = default)
        {
            foreach (var command in setupCommands)
            {
                _logger.LogInformation("Executing command: {Command}", command);

                var result = await _processExecutor.ExecuteAsync(
                    new ProcessRequest(command, Path.GetDirectoryName(path)!),
                    cancellationToken
                );

                if (!result.Success)
                {
                    throw new Exception($"Command failed with exit code {result.ExitCode}: {command}\nError: {result.Error}");
                }
            }
        }
    }
}