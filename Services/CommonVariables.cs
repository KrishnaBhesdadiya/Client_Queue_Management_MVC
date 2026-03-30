namespace Frontend_Exam.Services
{
    public class CommonVariables
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static CommonVariables()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public static string? Token()
        {
            string? Token = null;

            // Check if the session contains the "JWTToken" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("JWTToken") != null)
            {
                // If it exists, get the token value from session
                Token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            }

            return Token;
        }
        public static string? UserName()
        {
            string? UserName = null;

            // Check if the session contains the "UserName" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("UserName") != null)
            {
                // If it exists, get the username value from session
                UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            }

            return UserName;
        }

        public static string? ClinicName()
        {
            string? ClinicName = null;

            // Check if the session contains the "UserName" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("ClinicName") != null)
            {
                // If it exists, get the username value from session
                ClinicName = _httpContextAccessor.HttpContext.Session.GetString("ClinicName");
            }

            return ClinicName;
        }

        public static string? UserRole()
        {
            string? UserRole = null;

            // Check if the session contains the "UserName" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("UserRole") != null)
            {
                // If it exists, get the username value from session
                UserRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");
            }

            return UserRole;
        }

        public static string? UserEmail()
        {
            string? UserEmail = null;

            // Check if the session contains the "UserName" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("UserEmail") != null)
            {
                // If it exists, get the username value from session
                UserEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            }

            return UserEmail;
        }

        public static string? ClinicCode()
        {
            string? ClinicCode = null;

            // Check if the session contains the "UserName" key
            if (_httpContextAccessor.HttpContext?.Session.GetString("ClinicCode") != null)
            {
                // If it exists, get the username value from session
                ClinicCode = _httpContextAccessor.HttpContext.Session.GetString("ClinicCode");
            }

            return ClinicCode;
        }
    }
}
