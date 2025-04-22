using System;

namespace r_framework.FormManager
{
    [Serializable]
    public class FormAssembly
    {
        public string FormID { set; get;}
        public string Caption { set; get; }
        public string AssemblyName { set; get; }
        public string Namespace { set; get; }
        public override string ToString()
        {
            return string.Format("{0}:{1} <{2}> {3}", FormID, Caption, AssemblyName, Namespace);
        }
    }
}
