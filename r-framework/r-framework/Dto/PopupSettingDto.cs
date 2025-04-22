
using System;
namespace r_framework.Dto
{
    [Serializable]
    public class PopupSettingDto
    {
        public string CheckMethodName { get; set; }
        public string AssemblyName { get; set; }
        public string ClassNameSpace { get; set; }
        public string MethodName { get; set; }
    }
}
