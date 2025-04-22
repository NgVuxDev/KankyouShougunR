// $Id: JushoKensakuPopupLogic.cs 12989 2013-12-26 09:22:04Z ishibashi $
using System.Collections.Generic;
using System.Linq;
using JushoKensakuPopup1.APP;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;

namespace JushoKensakuPopup1.Logic
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
            this.form.JushoDetail.DataSource = this.list;
        }

        /// <summary>
        /// 項目決定
        /// </summary>
        internal void ElementDecision()
        {
            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> setParam = new List<PopupReturnParam>();
            for (int i = 0; i < this.form.JushoDetail.Columns.Count; i++)
            {
                PopupReturnParam popupParam = new PopupReturnParam();
                var setDate = this.form.JushoDetail[this.form.JushoDetail.CurrentRow.Index, i];

                var control = setDate as ICustomControl;

                popupParam.Key = setDate.Name;
                popupParam.Value = setDate.Value == null ? string.Empty : setDate.Value.ToString();

                setParam.Add(popupParam);
            }

            if (setParam.Any())
            {
                setParamList.Add(0, setParam);
            }

            this.form.ReturnParams = setParamList;
            this.form.Close();
        }
    }
}
