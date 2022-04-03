using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Implementations
{
    /// <summary>
    /// Class for calling logger Microservice
    /// </summary>
    public class LoggerService : ILoggerService
    {
        public Task ErrorLoggerAsync(Exception exception, string message, params object[] additionalData)
        {
            //Call logger Microservice
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task MessageLoggerAsync(string methodName, string message, params object[] additionalData)
        {
            //CallLogger Microservice
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
