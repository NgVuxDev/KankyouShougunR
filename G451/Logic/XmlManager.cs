using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using r_framework.Utility;
using r_framework.Configuration;
using Seasar.Framework.Container.Factory;

namespace Shougun.Core.Common.Login
{
    /// <summary>
    /// 各種設定を保存するXMLファイルを扱うクラス
    /// </summary>
    public class XmlManager
    {
        #region - Member -

        /// <summary>
        /// DatabaseConnectList.xmlのパス
        /// </summary>
        internal static string DBConfigFile = @"DatabaseConnectList.xml";

        /// <summary>
        /// CurrentUserCustomConfigProfile.xmlのパス
        /// </summary>
        internal static string UserConfigFile
        {
            get
            {
                return AppData.CurrentUserCustomConfigProfilePath;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Dao.diconのパス
        /// </summary>
        private static string daoDiconFile;

        /// <summary>
        /// Tx.diconのパス
        /// </summary>
        private static string txdiconFile;

        /// <summary>
        /// Ado.diconのパス
        /// </summary>
        private static string adoDiconFile;

        /// <summary>
        /// Dao_File.diconのパス
        /// </summary>
        private static string daoFileDiconFile;

        /// <summary>
        /// Tx_File.diconのパス
        /// </summary>
        private static string txFileDiconFile;

        /// <summary>
        /// Ado_File.diconのパス
        /// </summary>
        private static string adoFileDiconFile;

        /// <summary>
        /// Dao_File.diconのパス
        /// </summary>
        private static string daoFileDiconLog;

        /// <summary>
        /// Tx_File.diconのパス
        /// </summary>
        private static string txFileDiconLog;

        /// <summary>
        /// Ado_File.diconのパス
        /// </summary>
        private static string adoFileDiconLog;

        #endregion - Member -

        #region - Property -

        /// <summary>
        ///menu.xmlのパス
        /// </summary>
        internal static string MenuFile
        {
            get
            {
                var menuDir = Path.Combine(AppData.AppExecutableDirectory, "setting");
                var menuFile = string.Format("menu_{0}.xml", AppConfig.Series); 
                var menuPath = Path.Combine(menuDir, menuFile);
                return menuPath;
            }
        }

        /// <summary>
        /// Dao.diconのパス
        /// </summary>
        /// <returns>Dao.diconのパス</returns>
        internal static string DaoDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(daoDiconFile))
                {
                    daoDiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Dao.dicon");
                }

                return daoDiconFile;
            }
        }

        /// <summary>
        /// Tx.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string TxDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(txdiconFile))
                {
                    txdiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Tx.dicon");
                }

                return txdiconFile;
            }
        }

        /// <summary>
        /// Ado.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string AdoDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(adoDiconFile))
                {
                    adoDiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Ado.dicon");
                }

                return adoDiconFile;
            }
        }

        /// <summary>
        /// Dao_File.diconのパス
        /// </summary>
        /// <returns>Dao.diconのパス</returns>
        internal static string DaoFileDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(daoFileDiconFile))
                {
                    daoFileDiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Dao_File.dicon");
                }

                return daoFileDiconFile;
            }
        }

        /// <summary>
        /// Tx_File.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string TxFileDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(txFileDiconFile))
                {
                    txFileDiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Tx_File.dicon");
                }

                return txFileDiconFile;
            }
        }

        /// <summary>
        /// Ado_File.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string AdoFileDiconFile
        {
            get
            {
                if (string.IsNullOrEmpty(adoFileDiconFile))
                {
                    adoFileDiconFile = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Ado_File.dicon");
                }

                return adoFileDiconFile;
            }
        }
        /// <summary>
        /// Dao_File.diconのパス
        /// </summary>
        /// <returns>Dao.diconのパス</returns>
        internal static string DaoFileDiconLog
        {
            get
            {
                if (string.IsNullOrEmpty(daoFileDiconLog))
                {
                    daoFileDiconLog = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Dao_Log.dicon");
                }

                return daoFileDiconLog;
            }
        }

        /// <summary>
        /// Tx_File.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string TxFileDiconLog
        {
            get
            {
                if (string.IsNullOrEmpty(txFileDiconLog))
                {
                    txFileDiconLog = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Tx_Log.dicon");
                }

                return txFileDiconLog;
            }
        }

        /// <summary>
        /// Ado_File.diconのパス
        /// </summary>
        /// <returns>Tx.diconのパス</returns>
        internal static string AdoFileDiconLog
        {
            get
            {
                if (string.IsNullOrEmpty(adoFileDiconLog))
                {
                    adoFileDiconLog = SingletonS2ContainerFactory.ConfigPath.Replace("App.dicon", "Ado_Log.dicon");
                }

                return adoFileDiconLog;
            }
        }
        #endregion

        #region - Method -

        #region - DatabaseConnectList.xml用 -

        /// <summary>
        /// DatabaseConnectList.xmlに記述されている要素のリストを取得します。
        /// </summary>
        /// <param name="dbconfig">XDocument</param>
        /// <returns>接続先リスト</returns>
        internal static IEnumerable<XElement> GetDatabaseConnectList()
        {
            var list = XDocument.Load(DBConfigFile).Element("DatabaseConnectList").Elements("Items");

            // 過去に誤ってバックスペース(\記号)を重複したままで運用開始したのでここで正しい接続文字列に更新する
            // (ファイル上はそのままにしておく）
            foreach (var item in list)
            {
                var value = item.Element("ConnectionString").Value;
                item.Element("ConnectionString").Value = value.Replace(@"\\", @"\");
            }
            return list;
        }

        /// <summary>
        /// DatabaseConnectListからデフォルトのDB接続情報要素を取得します。
        /// 引数で指定されたDB名に該当する要素があればそれを、無ければ先頭要素を返却します。
        /// リストが空の場合はnullを返却します。
        /// </summary>
        /// <param name="dbName">DB表示名</param>
        /// <returns>指定DBのItem要素または先頭要素またはnull</returns>
        internal static XElement GetDatabaseConnectItem(string dbName)
        {
            XElement　defaulItem = null;
            var items = GetDatabaseConnectList();

            if (items != null && items.Count() > 0)
            {
                defaulItem = items.First();
                foreach (var item in items)
                {
                    if (item.Element("DispName").Value.Equals(dbName))
                    {
                        defaulItem = item;
                        break;
                    }
                }
            }

            return defaulItem;
        }

        /// <summary>
        /// DatabaseConnectionListのItems要素からDBconnectionDTOの新しいインスタンスを生成して返します。
        /// </summary>
        /// <param name="ele">Items要素</param>
        /// <returns>DBConnectionDTOのインスタンス</returns>
        internal static DBConnectionDTO GetDbConnectionDto(XElement ele)
        {
            var dispName = ele.Element("DispName").Value;
            var conStr = ele.Element("ConnectionString").Value;

            return new DBConnectionDTO(dispName, conStr);
        }

        #endregion - DatabaseConnectList.xml用 -

        #region - CurrentUserCustomConfigProfile.xml用 -

        /// <summary>
        /// CurrentUserCustomConfigProfile.xmlから前回ログインユーザ情報を取得します。
        /// 書き込みの必要がない場合は引数を省略できます。
        /// </summary>
        /// <param name="userConfig">XDocument</param>
        /// <returns>前回ログインユーザー情報のXElement</returns>
        internal static XElement GetUserElement(XDocument userConfig = null)
        {
            if (userConfig != null)
            {
                return userConfig.Element("CurrentUserCustomConfigProfile").Element("Settings").Elements("LoginInfo").Elements("User").FirstOrDefault();
            }
            else
            {
                return XDocument.Load(UserConfigFile).Element("CurrentUserCustomConfigProfile").Element("Settings").Elements("LoginInfo").Elements("User").FirstOrDefault();
            }
        }

        #endregion - CurrentUserCustomConfigProfile.xml用 -

        #region - 汎用 -

        /// <summary>
        /// 指定した要素(XElement)に指定した属性があるかどうかを返します。
        /// </summary>
        /// <param name="elm">要素</param>
        /// <param name="name">属性名</param>
        /// <returns>True:成功, False:失敗</returns>
        internal static bool HasAttributeByName(XElement elm, string name)
        {
            return (elm.Attribute(name) == null || string.IsNullOrEmpty(elm.Attribute(name).Value)) ? false : true;
        }

        #endregion - 汎用 -

        #endregion - Method -
    }
}
