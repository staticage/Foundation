using System;

namespace Foundation.Core
{
    public static class Environments
    {
        public const string EnvironmentKey = "ASPNETCORE_ENVIRONMENT";

        public static bool IsDevelopment()
        {
            return EnvironmentName == "Development";
        }
        
        public static bool IsProduction()
        {
            return EnvironmentName== "Production";
        }
        
        public static bool IsStaging()
        {
            return EnvironmentName== "Staging";
        }

        public static void VerifyEnvironmentName()
        {
            if (EnvironmentName.IsNullOrEmpty())
            {
                throw new Exception("EnvironmentName:[ASPNETCORE_ENVIRONMENT] is NULL");
            }
        }

        public static string EnvironmentName => Environment.GetEnvironmentVariable(EnvironmentKey);
    }
}