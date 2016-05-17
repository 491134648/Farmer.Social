using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmer.Social.WebSite.Controllers
{
    public class BaseController : Controller
    {
        protected UserClaim getUserClaimByOpenIdOrUnionId(string openId, string unionId, string tag)
        {
            UserClaim claim = MembershipService.GetExtendSocialByOpenId(openId, tag);
            if (claim != null)
            {
                return claim;
            }
            return MembershipService.GetExtentSocialByUnionId(unionId, tag);
        }
    }
}