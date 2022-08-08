using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Data;
using TodoApp.Core.DBEntities;
using TodoApp.Core.DBEntities.Authentication;
using TodoApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Core.Services
{
    public class LogService : ILogService
    {
        private IRepository<Log> _logRepository;

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<Log>> GetAll()
        {
            return await _logRepository.ListAsync();
        }

        public async Task<Log> InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", CancellationToken cancellationToken = default)
        {
           
            try
            {

                var log = new Log
                {
                    LogLevel = logLevel,
                    ShortMessage = shortMessage,
                    FullMessage = fullMessage,
                    PageUrl = "",
                    ReferrerUrl = "",
                    CreatedOnUtc = DateTime.UtcNow
                };

                var ss = await _logRepository.AddAsync(log, cancellationToken);
                return ss;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
