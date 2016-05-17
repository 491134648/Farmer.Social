using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social
{
    /// <summary>
    /// 第三方登录用户信息
    /// </summary>
    public class SocialUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 登陆类型
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 个人信息
        /// </summary>
        public string Profile { get; set; }
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extend { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
