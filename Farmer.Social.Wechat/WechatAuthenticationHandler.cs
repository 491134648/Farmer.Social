using Farmer.Social.Handler;
using Farmer.Social.Options;
using Farmer.Social.Wechat.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Farmer.Social.Utility;

namespace Farmer.Social.Wechat
{
    public class WechatAuthenticationHandler : AuthenticationHandler<WechatAuthenticationTicket>
    {

        private readonly HttpClient _httpClient;
        public WechatAuthenticationHandler(AuthenticationOptions options)
            : base(options)
        {
            this._httpClient = new HttpClient();
        }
        public override string GetAuthorizationUrl(AuthenticationScope scope)
        {
            string url =string.Format("{0}/connect/qrconnect?appid={1}&redirect_uri={2}&response_type=code&scope=snsapi_login&state={3}#wechat_redirect",
                            this._options.AuthorizeUrl, this._options.AppId, Uri.EscapeDataString(string.Concat(_options.Host, _options.Callback)), scope.State);
            return url;
        }
        public override WechatAuthenticationTicket PreAuthorization(WechatAuthenticationTicket ticket)
        {
            //构建获取Access Token的参数
            string url = string.Format("{0}/sns/oauth2/access_token?appid={1}&secret={2}&code={3}&grant_type=authorization_code",
                                             this._options.AuthorizeUrl, this._options.AppId, this._options.AppSecret, ticket.Code);
            string tokenResponse = _httpClient.GetStringAsync(url).Result.ToString();
            if (tokenResponse.IndexOf("errcode") > 0)
            {
                throw new SocialExeception(tokenResponse);
            }
                var callback = JsonHelper.DeserializeObject<Callback>(tokenResponse);
                ticket.OpenId = callback.openid;
                ticket.AccessToken = callback.access_token;
                ticket.RefreshToken = callback.refresh_token;
                return ticket;
        }
        public override WechatAuthenticationTicket AuthenticateCore(WechatAuthenticationTicket ticket)
        {
            return ticket;
        }
        public override SocialUser GetUserInfo(WechatAuthenticationTicket ticket)
        {
           string url = string.Format("{0}/sns/userinfo?access_token={1}&openid={2}",
                                         this._options.AuthorizeUrl,ticket.AccessToken, ticket.OpenId);
            string tokenResponse = _httpClient.GetStringAsync(url).Result.ToString();
            if (tokenResponse.IndexOf("errcode") > 0)
            {
                throw new SocialExeception(tokenResponse); 
            }
            WeChat qzone = JsonHelper.DeserializeObject<WeChat>(tokenResponse);
            return new SocialUser
            {
                OpenId=ticket.OpenId,
                NickName=qzone.nickname,
                Profile=qzone.headimgurl,
                Tag="Wechat",
                Token=ticket.AccessToken,
                RefreshToken=ticket.RefreshToken,
                Extend=qzone.unionid
            };
        }
        /// <summary>
        /// 根据access_token获得对应用户身份的openid
        /// </summary>
        private class Callback
        {
            /// <summary>
            /// 客户端Id
            /// </summary>
            public string access_token { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refresh_token { get; set; }
            /// <summary>
            /// 用户Id
            /// </summary>
            public string openid { get; set; }
        }
        private class WeChat
        {
            /// <summary>
            /// 昵称 
            /// </summary>
            public string nickname { get; set; }
            /// <summary>
            /// 头像URL
            /// </summary>
            public string headimgurl { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public int sex { get; set; }
            public string unionid { get; set; }
        }
    }
}
