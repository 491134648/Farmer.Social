using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmer.Social.Utility
{
    public static class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="ingonrNull">是否忽略空值默认为false</param>
        /// <returns></returns>
        public static string ToJsonString(object obj,bool ingonrNull=false)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            if (ingonrNull)
               setting.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, setting);
        }
        public static T DeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public static object DeserializeObject(string jsonstr)
        {
            return JsonConvert.DeserializeObject(jsonstr);
        }
    }
}
