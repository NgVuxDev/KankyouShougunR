using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.App;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Logic;
using System.Windows.Forms;
using r_framework.FormManager;
using r_framework.Configuration;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    /// <summary>
    /// >終了日警告ロジック
    /// </summary>
    public class SyuuryouBiKeikokuLogic
    {
        /// <summary>終了日警告Dao</summary>
        private SyuuryouBiKeikokuDAOClass SyuuryouBiKeikokuDAO;

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
        /// コンストラクタ
        /// </summary>
        public SyuuryouBiKeikokuLogic()
        {
            LogUtility.DebugMethodStart();
            this.SyuuryouBiKeikokuDAO = DaoInitUtility.GetComponent<SyuuryouBiKeikokuDAOClass>();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// システム情報マスター
        /// </summary>
        public M_SYS_INFO SystemInformation { get; private set; }

        /// <summary>
        /// 終了日警告データを取得する
        /// </summary>
        /// <returns>検索結果件数</returns>
        public int GetSyuuryouBiKeikokuData()
        {
            LogUtility.DebugMethodStart();

            this.SystemInformation = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");

            DataTable SearchResult = new DataTable();

            SyuuryouBiKeikokuDTOClass SearchString = new SyuuryouBiKeikokuDTOClass();
            SearchString.Mani_UNPAN_DAYS = this.SystemInformation.MANIFEST_UNPAN_DAYS;
            SearchString.Mani_SBN_DAYS = this.SystemInformation.MANIFEST_SBN_DAYS;
            SearchString.Mani_TOK_SBN_DAYS = this.SystemInformation.MANIFEST_TOK_SBN_DAYS;
            SearchString.Mani_LAST_SBN_DAYS = this.SystemInformation.MANIFEST_LAST_SBN_DAYS;

            SearchResult = SyuuryouBiKeikokuDAO.GetDataForEntity(SearchString);

            LogUtility.DebugMethodEnd();
            return SearchResult.Rows.Count;
        }

        /// <summary>
        /// 終了日警告一覧画面開くチェック
        /// </summary>
        public void CheckSyuuryouBiKeikoku()
        {
            LogUtility.DebugMethodStart();

            int retultCount = this.GetSyuuryouBiKeikokuData();
            if (this.CheckSysInfo() && retultCount >= 1)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                //「はい」を押下
                if (messageShowLogic.MessageBoxShow("C068") == DialogResult.Yes)
                {
                    FormManager.OpenForm("G599");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニ終了日警告と個別終了日警告設定値を取得する
        /// </summary>
        /// <returns>設定値ある：TRUE</returns>
        /// <returns>設定値ない：FALSE</returns>
        public bool CheckSysInfo()
        {
            LogUtility.DebugMethodStart();

            var itemElement = SyuuryouBiKeikokuLogic.GetDefaultValueElement();

            if (itemElement == null)
            {
                LogUtility.DebugMethodEnd();
                return false;
            }

            string itemName = itemElement.Element("Name").Value;
            string itemValue = itemElement.Element("Value").Value;

            this.SystemInformation = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");

            if (this.SystemInformation.MANIFEST_ENDDATE_USE_KBN == 1 && itemName == "終了日警告" && itemValue == "1")
            {
                LogUtility.DebugMethodEnd();
                return true;
            }
            else
            {
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        #region - CurrentUserCustomConfigProfile.xml用 -
        /// <summary>
        /// CurrentUserCustomConfigProfile.xmlから前回「終了日警告」情報を取得します。
        /// 書き込みの必要がない場合は引数を省略できます。
        /// </summary>
        /// <param name="userConfig">XDocument</param>
        /// <returns>前回「終了日警告」情報のXElement</returns>
        internal static XElement GetDefaultValueElement()
        {
            XDocument defaultValue = XDocument.Load(SyuuryouBiKeikokuLogic.UserConfigFile);
            XElement itema = null;
            if (defaultValue != null)
            {
                foreach (var item in defaultValue.Element("CurrentUserCustomConfigProfile").Element("Settings").Element("DefaultValue").Elements("ItemSettings"))
                {
                    if (item.Element("Name").Value == "終了日警告")
                    {
                        itema = item;
                        break;
                    }
                }
            }
            else
            {
                foreach (var item in XDocument.Load(UserConfigFile).Element("CurrentUserCustomConfigProfile").Element("Settings").Elements("DefaultValue").Elements("ItemSettings"))
                {
                    if (item.Element("Name").Value == "終了日警告")
                    {
                        itema = item;
                        break;
                    }
                }
            }
            return itema;
        }
        #endregion - CurrentUserCustomConfigProfile.xml用 -

    }
}
