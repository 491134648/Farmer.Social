using Farmer.Social.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social.Handler
{
    /// <summary>
    /// 生成登陆授权
    /// </summary>
    /// <typeparam name="AuthenticationOptions"></typeparam>
    public class AuthenticationHandler<T> where T:AuthenticationTicket
    {
        protected  AuthenticationOptions _options { get; set; }
        public AuthenticationHandler(AuthenticationOptions options)
        {
            this._options = options;
        }
       
        /// <summary>
        /// 依据指定信息生成链接
        /// </summary>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public virtual string GetAuthorizationUrl(AuthenticationScope scope) 
        {
            return _options.AuthorizeUrl;
        }
        
        /// <summary>
        /// 生成预授权Ticket
        /// </summary>
        /// <param name="ticket"></param>
        public virtual T PreAuthorization(T ticket)
        {
            return ticket;
        }
        /// <summary>
        /// 生成Token等
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public virtual T AuthenticateCore(T ticket)
        {
            return ticket;
        }
        /// <summary>
        /// 获取授权用户信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public virtual SocialUser GetUserInfo(T ticket)
        {
            return new SocialUser();
        }
        
    }
}
