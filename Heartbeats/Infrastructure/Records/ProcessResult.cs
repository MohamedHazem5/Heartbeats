namespace Heartbeats.Infrastructure.Records
{
    public record ProcessResult(
        int ExitCode,
        string Output,
        string Error,
        bool Success
    );
}