using Heartbeats.Infrastructure.Records;
using System.Diagnostics;

namespace Heartbeats.Infrastructure.Interfaces
{
    public interface IProcessExecutor
    {
        Task<ProcessResult> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken = default);

        Process StartLongRunningProcess(ProcessRequest request);
    }
}