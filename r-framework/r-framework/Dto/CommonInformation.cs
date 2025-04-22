using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace r_framework.Dto
{
    /// <summary>
    /// アプリケーション全体で使用する情報
    /// </summary>
    [Serializable()]
    public class CommonInformation : ICloneable
    {
        public M_SYS_INFO SysInfo { get; private set; }

        public M_CORP_INFO CorpInfo { get; private set; }

        public M_SHAIN CurrentShain { get; private set; }

        public CommonInformation(M_SYS_INFO mSysInfo, M_CORP_INFO mCorpInfo, M_SHAIN mShain)
        {
            this.SysInfo = mSysInfo;
            this.CorpInfo = mCorpInfo;
            this.CurrentShain = mShain;
        }

        public virtual object Clone()
        {
            CommonInformation result;
            BinaryFormatter b = new BinaryFormatter();

            MemoryStream mem = new MemoryStream();

            try
            {
                b.Serialize(mem, this);
                mem.Position = 0;
                result = (CommonInformation)b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }

            return result;
        }

    }
}
