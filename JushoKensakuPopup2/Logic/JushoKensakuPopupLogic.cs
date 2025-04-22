// $Id: JushoKensakuPopupLogic.cs 3620 2013-10-15 02:55:28Z sys_dev_02 $
using System.Collections.Generic;
using System.Linq;
using JushoKensakuPopup.APP;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace JushoKensakuPopup.Logic
{
    /// <summary>
    /// 住所検索ポップアップ画面ロジック
    /// </summary>
    public class JushoKensakuPopupLogic
    {
        #region フィールド

        private JushoKensakuPopupForm form;

        private S_ZIP_CODE[] list;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JushoKensakuPopupLogic()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        /// <param name="targetList"></param>
        public JushoKensakuPopupLogic(JushoKensakuPopupForm targetForm, S_ZIP_CODE[] targetList)
        {
            this.form = targetForm;
            this.list = targetList;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal void WindowInit()
        {
        }

        /// <summary>
        /// データ設定
        /// </summary>
        internal void SetDetailList()
        {
            LogUtility.DebugMethodStart();
            //検索結果設定
            for (int i = 0; i < this.list.Length; i++)
            {
                this.form.JushoDetail.Rows.Add();
                this.form.JushoDetail.Rows[i].Cells["ZIPCD"].Value = list[i].POST7;
                this.form.JushoDetail.Rows[i].Cells["TODOUFUKEN"].Value = list[i].TODOUFUKEN;
                this.form.JushoDetail.Rows[i].Cells["SIKUCHOUSON"].Value = list[i].SIKUCHOUSON;
                this.form.JushoDetail.Rows[i].Cells["OTHER1"].Value = list[i].OTHER1;
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 項目決定
        /// </summary>
        internal void ElementDecision()
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = new List<PopupReturnParam>();

            PopupReturnParam popupParam1 = new PopupReturnParam();
            popupParam1.Value = this.form.JushoDetail.Rows[this.form.JushoDetail.CurrentCell.RowIndex].Cells["ZIPCD"].Value.ToString();
            setParam.Add(popupParam1);

            PopupReturnParam popupParam2 = new PopupReturnParam();
            popupParam2.Value = this.form.JushoDetail.Rows[this.form.JushoDetail.CurrentCell.RowIndex].Cells["TODOUFUKEN"].Value.ToString();
            setParam.Add(popupParam2);

            PopupReturnParam popupParam3 = new PopupReturnParam();
            popupParam3.Value = this.form.JushoDetail.Rows[this.form.JushoDetail.CurrentCell.RowIndex].Cells["SIKUCHOUSON"].Value.ToString();
            setParam.Add(popupParam3);

            PopupReturnParam popupParam4 = new PopupReturnParam();
            popupParam4.Value = this.form.JushoDetail.Rows[this.form.JushoDetail.CurrentCell.RowIndex].Cells["OTHER1"].Value.ToString();
            setParam.Add(popupParam4);

            if (setParam.Any())
            {
                setParamList.Add(0, setParam);
            }

            this.form.ReturnParams = setParamList;
            this.form.Close();
        }
    }
}
