namespace Heartbeats.Infrastructure.Records
{
    public record ProcessRequest(
         string Command,
         string WorkingDirectory,
         bool CreateNoWindow = false,
         bool RedirectOutput = true
    );
}