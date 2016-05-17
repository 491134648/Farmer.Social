using Farmer.Social.Tencent;
using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Security;
using Farmer.Social.Options;


namespace Farmer.Social.WebSite.Controllers.OAuthControllers
{
    public partial class QQOAuthController : BaseController
    {
        private TencentAuthenticationHandler _tencentHandler;
        private AuthenticationOptions _options;
        public QQOAuthController(ILoggingService loggingService,
                                    IUnitOfWorkManager unitOfWorkManager,
                                    IMembershipService membershipService,
                                    ILocalizationService localizationService,
                                    IRoleService roleService,
                                    ISettingsService settingsService)
            : base(loggingService,
                  unitOfWorkManager,
                  membershipService,
                  localizationService,
                  roleService,
                  settingsService)
        {
            _options = new AuthenticationOptions()
            {
                AppId = "",
                AppSecret = "",
                AuthorizeUrl = "https://graph.qq.com",
                Host="http://www.nongyou360.com/",
                Callback ="qqconnect",
            };
            _tencentHandler = new TencentAuthenticationHandler(_options);
        }
        public ActionResult QQLogin(string returnUrl)
        {
            AuthenticationScope scope=new AuthenticationScope(){
                State=Guid.NewGuid().ToString().Replace("-", ""),
                Scope="get_user_info"
            };
            if (!string.IsNullOrEmpty(returnUrl))
            {
                Session["returnUrl"] = returnUrl;
            }
            Session["requeststate"] = scope.State;
            string url=_tencentHandler.GetAuthorizationUrl(scope);
            return Redirect(url);
        }
        public ActionResult CallBack()
        {
            var verifier = Request.Params["code"];
            string state = Session["requeststate"].ToString();
            QQAuthenticationTicket ticket = new QQAuthenticationTicket()
            {
                Code=verifier,
                Tag = "Tencent.QQ"
            };
            ticket = _tencentHandler.PreAuthorization(ticket);
            ticket = _tencentHandler.AuthenticateCore(ticket);
            UserClaim userClaim = getUserClaimByOpenIdOrUnionId(ticket.OpenId, "", ticket.Tag);
            if (userClaim != null)
            {
                FormsAuthentication.SetAuthCookie(userClaim.User.UserName, true);
                if (Session["returnUrl"] != null && string.IsNullOrEmpty(Session["returnUrl"].ToString()))
                {
                    return Redirect(Session["returnUrl"].ToString());
                }
                return RedirectToAction("Index", "Home");
            }
            SocialUser user = _tencentHandler.GetUserInfo(ticket);
            Session["social.current"] = user;
            return RedirectToAction("social", "members");
        }
     
    }
}