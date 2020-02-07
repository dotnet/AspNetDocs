using System.Web;

namespace WebApplication64
{
    public class SameSiteCheck
    {
        #region snippet
        private void CheckSameSite(HttpContext httpContext, HttpCookie cookie)
        {
            if (cookie.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.UserAgent;
                if (MyUserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
                {
                    cookie.SameSite = (SameSiteMode)(-1);
                }
            }
        }
        #endregion
    }
    #region snippet2
    public static class MyUserAgentDetectionLib
    {
        public static bool DisallowsSameSiteNone(string userAgent)
        {
            // check if a null or empty string has been passed in, since this
            // will cause further interrogation of the useragent to fail
             if (String.IsNullOrWhiteSpace(userAgent))
                return false;
            
            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS 
            // networking stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}

