using Heartbeats.Infrastructure.Interfaces;
using Heartbeats.Infrastructure.Records;
using System.Diagnostics;
using System.Text;

namespace Heartbeats.Infrastructure.Managers
{
    public class ProcessExecutor : IProcessExecutor
    {
        private readonly ILogger<ProcessExecutor> _logger;

        public ProcessExecutor(ILogger<ProcessExecutor> logger)
        {
            _logger = logger;
        }

        public async Task<ProcessResult> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken = default)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();

            var startInfo = CreateProcessStartInfo(request);
            using var process = new Process { StartInfo = startInfo };

            process.OutputDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    output.AppendLine(e.Data);
                    _logger.LogInformation(e.Data);
                }
            };

            process.ErrorDataReceived += (_, e) =>
            {
                if (e.Data != null)
                {
                    error.AppendLine(e.Data);
                    _logger.LogError(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(cancellationToken);

            return new ProcessResult(
                process.ExitCode,
                output.ToString(),
                error.ToString(),
                process.ExitCode == 0
            );
        }

        public Process StartLongRunningProcess(ProcessRequest request)
        {
            var startInfo = CreateProcessStartInfo(request);
            var process = new Process { StartInfo = startInfo };

            process.OutputDataReceived += (_, e) => _logger.LogInformation(e.Data ?? string.Empty);
            process.ErrorDataReceived += (_, e) => _logger.LogError(e.Data ?? string.Empty);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;
        }

        private static ProcessStartInfo CreateProcessStartInfo(ProcessRequest request) =>
            new()
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{request.Command}\"",
                WorkingDirectory = request.WorkingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = request.RedirectOutput,
                RedirectStandardError = request.RedirectOutput,
                CreateNoWindow = request.CreateNoWindow
            };
    }
}