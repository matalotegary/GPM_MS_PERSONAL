using application.Constants;

namespace application.Common
{
    public static class SharedHelper
    {
        public static string GetFullActionMethodName(string controllerName, string actionMethodName)
        {
            return $"{SharedConstants.Common.MS_NAME}.{controllerName}.{actionMethodName}";
        }
    }
}
