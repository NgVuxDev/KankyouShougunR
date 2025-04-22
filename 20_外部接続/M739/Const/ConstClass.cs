using System;

namespace Shougun.Core.ExternalConnection.MapRenkei.Const
{
    public class ConstClass
    {
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.MapRenkei.Setting.ButtonSetting.xml";

        /// <summary>
        /// CSVファイル名
        /// </summary>
        internal static readonly string CSV_FILE_PREFIX_FORMAT = "map_master_{0}";

        /// <summary>
        /// CSV出力を行うカラム名
        /// </summary>
        internal static readonly string CSV_COLUMN_MASTER_KBN= "MASTER_KBN";
        internal static readonly string CSV_COLUMN_CD1= "CD1";
        internal static readonly string CSV_COLUMN_NAME1 = "NAME1";
        internal static readonly string CSV_COLUMN_CD2 = "CD2";
        internal static readonly string CSV_COLUMN_NAME2 = "NAME2";
        internal static readonly string CSV_COLUMN_NAME_RYAKU = "NAME_RYAKU";
        internal static readonly string CSV_COLUMN_ADDRESS = "ADDRESS";
        internal static readonly string CSV_COLUMN_LATITUDE = "LATITUDE";
        internal static readonly string CSV_COLUMN_LONGITUDE = "LONGITUDE";
        internal static readonly string CSV_COLUMN_UPDATE_USER = "UPDATE_USER";
        internal static readonly string CSV_COLUMN_UPDATE_DATE = "UPDATE_DATE";
    }    
}