using Farmer.Social.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social.Wechat.Options
{
    public class WechatAuthenticationTicket : AuthenticationTicket
    {
        public string Code { get; set; }
        public string OpenId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
