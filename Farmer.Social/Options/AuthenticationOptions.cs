using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social.Options
{
    /// <summary>
    /// 授权选项
    /// </summary>
    public  class AuthenticationOptions
    {
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 客户端密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 授权链接
        /// </summary>
        public string AuthorizeUrl { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 回传
        /// </summary>
        public string Callback { get; set; }
        /// <summary>
        /// TokenUrl
        /// </summary>
        public string TokenUrl { get; set; }
        public string OpenUrl { get; set; }
        /// <summary>
        /// 用户信息地址
        /// </summary>
        public string UserUrl { get; set; }
    }
}
