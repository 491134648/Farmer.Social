准备资料：

各平台相关授权appid以及appkey(新浪为App Secret)

申请地址：

新浪

    申请入口  http://open.weibo.com/connect

    开发文档 http://open.weibo.com/wiki/%E7%BD%91%E7%AB%99%E6%8E%A5%E5%85%A5

腾讯QQ

   申请入口:http://connect.qq.com/

   开发文档  http://wiki.connect.qq.com/

微信 

    申请入口https://open.weixin.qq.com/

    开发文档 https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&lang=zh_CN

以QQ为例:

从以上文档中可以得知，获得openId以及QQ获得用户信息需要三步,第一步，封装请求链接，然后服务的返回浏览器302跳转至微信或QQ等用户授权窗口
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
         成功后浏览器会跳转至

redirect_uri传递的链接窗口即QQOAuthController下面的CallBack
具体 http://www.cnblogs.com/shatanku/p/5502094.html
