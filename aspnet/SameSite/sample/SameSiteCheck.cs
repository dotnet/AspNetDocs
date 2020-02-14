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
                if (BrowserDetection.DisallowsSameSiteNone(userAgent))
                {
                    cookie.SameSite = (SameSiteMode)(-1);
                }
            }
        }
        #endregion
    }
}

