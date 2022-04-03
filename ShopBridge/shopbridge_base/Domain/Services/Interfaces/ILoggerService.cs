using System;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface ILoggerService
    {
        Task MessageLoggerAsync(string methodName, string message, params object[] additionalData);
        Task ErrorLoggerAsync(Exception exception, string message, params object[] additionalData);
    }
}