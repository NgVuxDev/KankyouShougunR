using System.Collections.Generic;
using System.IO;
using System.Linq;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Shougun.Core.FileUpload.FileUploadCommon.DTO;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;

namespace Shougun.Core.FileUpload.FileUploadCommon.Utility
{
    /// <summary>
    /// ファイルアップロード用Daoの初期化クラス
    /// </summary>
    public class FileConnectionUtility
    {
        /// <summary>
        /// ファイルアップロード用Daoの生成
        /// </summary>
        public static T GetComponent<T>() where T : IS2Dao
        {
            return DaoInitUtility.GetComponent<T>(Constans.DAO_FILE);
        }
    }
}
