namespace Heartbeats.Infrastructure.Interfaces
{
    public interface IVirtualEnvironmentManager
    {
        bool Exists(string path);

        Task CreateAsync(string path, string[] setupCommands, CancellationToken cancellationToken = default);
    }
}