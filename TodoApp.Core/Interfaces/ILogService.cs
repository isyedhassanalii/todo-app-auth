using Microsoft.Extensions.Logging;
using TodoApp.Core.DBEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LogLevel = TodoApp.Core.DBEntities.LogLevel;

namespace TodoApp.Core.Interfaces
{
    public interface ILogService
    {
        Task<Log> InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", CancellationToken cancellationToken = default);
        Task<List<Log>> GetAll();

    }
}