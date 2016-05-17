using Farmer.Social.Handler;
using Farmer.Social.Options;
using Farmer.Social.Tencent.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Farmer.Social.Utility;

namespace Farmer.Social.Tencent
{
    public class TencentAuthenticationHandler : AuthenticationHandler<QQAuthenticationTicket>
    {
        private const string Tag = "TencentAuthenticationHandler";
        private readonly HttpClient _httpClient;
        public TencentAuthenticationHandler(AuthenticationOptions options):base(options)
        {
            this._httpClient = new HttpClient();
        }
        public override string GetAuthorizationUrl(AuthenticationScope scope)
        {
            string url = string.Empty;
            if (string.IsNullOrEmpty(scope.Scope))
            {
                url = string.Format("{0}/oauth2.0/authorize?response_type=code&client_id={1}&redirect_uri={2}&state={3}", _options.AuthorizeUrl, _options.AppId, string.Concat(_options.Host, _options.Callback), scope.State);
            }
            else
            {
                url = string.Format("{0}/oauth2.0/authorize?response_type=code&client_id={1}&redirect_uri={2}&state={3}&scope={4}", _options.AuthorizeUrl, _options.AppId, Uri.EscapeDataString(string.Concat(_options.Host, _options.Callback)), scope.State, scope.Scope);
            }
            return url;
        }
        public override QQAuthenticationTicket PreAuthorization(QQAuthenticationTicket ticket)
        {
            string tokenEndpoint = string.Concat(_options.AuthorizeUrl, "/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}");
            var url = string.Format(
                     tokenEndpoint,
                     Uri.EscapeDataString(_options.AppId),
                     Uri.EscapeDataString(_options.AppSecret),
                     Uri.EscapeDataString(ticket.Code), Uri.EscapeDataString(string.Concat(_options.Host, _options.Callback)));
            string tokenResponse = _httpClient.GetStringAsync(url).Result.ToString();
            if (tokenResponse.IndexOf('&') > 0)
            {
                var parameters = tokenResponse.Split('&');
                foreach (var parameter in parameters)
                {
                    var accessTokens = parameter.Split('=');
                    if (accessTokens[0] == "access_token")
                    {
                        ticket.AccessToken = accessTokens[1];
                    }
                    else if (accessTokens[0] == "refresh_token") 
                    {
                        ticket.RefreshToken = accessTokens[1];
                    }
                    
                }
            }
            return ticket;
        }
        public override QQAuthenticationTicket AuthenticateCore(QQAuthenticationTicket ticket)
        {
            string tokenEndpoint = string.Concat(_options.AuthorizeUrl, "/oauth2.0/me?access_token={0}");
            var url = string.Format(
                     tokenEndpoint,ticket.AccessToken);
            string tokenResponse = _httpClient.GetStringAsync(url).Result.ToString();
            string strJson = tokenResponse.Replace("callback(", "").Replace(");", "");
            var payload = JsonHelper.DeserializeObject<Callback>(strJson);
            ticket.OpenId=payload.openid;
            return ticket;
        }
        public override SocialUser GetUserInfo(QQAuthenticationTicket ticket)
        {
            string tokenEndpoint = string.Concat(_options.AuthorizeUrl, "/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}");
            var url = string.Format(
                     tokenEndpoint, ticket.AccessToken,_options.AppId,ticket.OpenId);
            string tokenResponse = _httpClient.GetStringAsync(url).Result.ToString();
            Qzone qzone = JsonHelper.DeserializeObject<Qzone>(tokenResponse);
            return new SocialUser
            {
                OpenId=ticket.OpenId,
                NickName=qzone.nickname,
                Profile=qzone.figureurl,
                Tag="Tencent.QQ",
                Token=ticket.AccessToken,
                RefreshToken=ticket.RefreshToken
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
            public string client_id { get; set; }

            /// <summary>
            /// 用户Id
            /// </summary>
            public string openid { get; set; }
        }
        private class Qzone
        {
            public int ret { get; set; }
            public string msg { get; set; }
            /// <summary>
            /// 昵称 
            /// </summary>
            public string nickname { get; set; }
            /// <summary>
            /// 头像URL
            /// </summary>
            public string figureurl { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string gender { get; set; }
        }
    }
}
