using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Domain.Constants
{
    [ExcludeFromCodeCoverage]
    public class ShoppingBridgeConstants
    {
        public const string EnterMethodMessage = "Entering method {0}.{1}";
        public const string ExitMethodName = "Exiting method {0}.{1}";
        public const string ExitMethodNameWithException = "Exiting method {0}.{1} with exception";
        public const string ExceptionMessage = "Unexpected Error occured. Please contact system administrator.";
    }
}
