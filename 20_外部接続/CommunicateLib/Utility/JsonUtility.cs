using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Utility
{
    public static class JsonUtility
    {
        public static T DeserializeObject<T>(string msg)
        {
            var obj = new JavaScriptSerializer().Deserialize<T>(msg);
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static string SerializeObject<T>(T obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
