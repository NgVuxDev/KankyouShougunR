using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Logic
{
    public class XmlHelper
    {
        /// <summary>
        /// DatabaseFileConnectList.xmlのパス
        /// </summary>
        public string DBConfigFile = string.Empty;

        #region - Method -

        #region - DatabaseFileConnectList.xml用 -

        /// <summary>
        /// DatabaseFileConnectList.xmlに記述されている要素のリストを取得します。
        /// </summary>
        /// <param name="dbconfig">XDocument</param>
        /// <returns>接続先リスト</returns>
        public IEnumerable<XElement> GetDatabaseConnectList()
        {
            if (!System.IO.File.Exists(DBConfigFile))
            {
                return null;
            }

            var list = XDocument.Load(DBConfigFile).Element("DatabaseConnectList").Elements("Items");

            // 過去に誤ってバックスペース(\記号)を重複したままで運用開始したのでここで正しい接続文字列に更新する
            // (ファイル上はそのままにしておく）
            foreach (var item in list)
            {
                var conn = item.Element("ConnectionString").Value;
                item.Element("ConnectionString").Value = conn.Replace(@"\\", @"\"); 
                
                var connShougun = item.Element("ShougunRConnectionString").Value;
                item.Element("ShougunRConnectionString").Value = connShougun.Replace(@"\\", @"\");
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
        public XElement GetDatabaseConnectItem(string dbName)
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
        public InxsSubappConnectionDto GetDbConnectionDto(XElement ele)
        {
            var dispName = ele.Element("DispName").Value;
            var conStr = ele.Element("ConnectionString").Value;
            var shougunConStr = ele.Element("ShougunRConnectionString").Value;

            return new InxsSubappConnectionDto(dispName, conStr, shougunConStr);
        }

        #endregion - DatabaseFileConnectList.xml用 -

        #endregion - Method -
    }
}
