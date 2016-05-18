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

具体参见 http://www.cnblogs.com/shatanku/p/5502094.html
