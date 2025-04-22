using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Shougun.Core.FileUpload.FileUploadCommon.DTO;
using r_framework.Configuration;
using r_framework.Dto;

namespace Shougun.Core.FileUpload.FileUploadCommon.Logic
{
    public class XmlManager
    {
        /// <summary>
        /// DatabaseFileConnectList.xmlのパス
        /// </summary>
        public static string DBConfigFile = @"DatabaseFileConnectList.xml";

        /// <summary>
        /// DatabaseLOGConnectList.xmlのパス
        /// </summary>
        public static string DBConfigLOG = @"DatabaseLogConnectList.xml";

        #region - Method -

        #region - DatabaseFileConnectList.xml用 -

        /// <summary>
        /// DatabaseFileConnectList.xmlに記述されている要素のリストを取得します。
        /// </summary>
        /// <param name="dbconfig">XDocument</param>
        /// <returns>接続先リスト</returns>
        public static IEnumerable<XElement> GetDatabaseConnectList()
        {
            if (!System.IO.File.Exists(DBConfigFile))
            {
                r_framework.Utility.LogUtility.Error(string.Format("{0}ファイルが存在しません。", DBConfigFile));
                return null;
            }

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
        public static XElement GetDatabaseConnectItem(string dbName)
        {
            XElement defaulItem = null;
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
        public static DBConnectionDTO GetDbConnectionDto(XElement ele)
        {
            var dispName = ele.Element("DispName").Value;
            var conStr = ele.Element("ConnectionString").Value;

            return new DBConnectionDTO(dispName, conStr);
        }

        #endregion - DatabaseFileConnectList.xml用 -

        #endregion - Method -

        //QN_QUAN add 20211229 #158952 S
        /// <summary>
        /// DatabaseFileConnectList.xmlに記述されている要素のリストを取得します。
        /// </summary>
        /// <param name="dbconfig">XDocument</param>
        /// <returns>接続先リスト</returns>
        public static IEnumerable<XElement> GetDatabaseLOGConnectList()
        {
            if (!System.IO.File.Exists(DBConfigLOG))
            {
                r_framework.Utility.LogUtility.Error(string.Format("{0}ファイルが存在しません。", DBConfigLOG));
                return null;
            }

            var list = XDocument.Load(DBConfigLOG).Element("DatabaseConnectList").Elements("Items");

            // 過去に誤ってバックスペース(\記号)を重複したままで運用開始したのでここで正しい接続文字列に更新する
            // (ファイル上はそのままにしておく）
            foreach (var item in list)
            {
                var value = item.Element("ConnectionString").Value;
                item.Element("ConnectionString").Value = value.Replace(@"\\", @"\");
            }
            return list;
        }

        public static DBConnectionDTOLOG GetDbConnectionLOGDto(XElement ele)
        {
            var dispName = ele.Element("DispName").Value;
            var conStr = ele.Element("ConnectionString").Value;

            return new DBConnectionDTOLOG(dispName, conStr);
        }
        //QN_QUAN add 20211229 #158952 E
    }
}
