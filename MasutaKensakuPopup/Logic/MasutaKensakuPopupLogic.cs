// $Id: MasutaKensakuPopupLogic.cs 12772 2013-12-25 12:09:53Z sys_dev_33 $
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasutaKensakuPopup.APP;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.CustomControl.DataGridCustomControl;
using System.Linq;

namespace MasutaKensakuPopup.Logic
{
    /// <summary>
    /// 検索項目ポップアップ画面ロジック
    /// </summary>
    public class MasutaKensakuPopupLogic
    {
        #region フィールド

        private MasutaKensakuPopupForm form;

        
        #endregion

        public MasutaKensakuPopupLogic()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        /// <param name="gcMultiRow"></param>
        public MasutaKensakuPopupLogic(MasutaKensakuPopupForm targetForm, GcCustomMultiRow gcMultiRow, CustomDataGridView dgv)
        {
            this.form = targetForm;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                //最大最小ボタンは出さない
                this.form.MaximizeBox = false;
                this.form.MinimizeBox = false;

                //フォームタイトルはラベルタイトルに合わせる
                this.form.Text = this.form.lb_title.Text;

                //アンカー対応
                this.form.bt_func12.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                this.form.MasterItem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
          
        }
        
       
        /// <summary>
        /// 項目決定
        /// </summary>
        internal bool ElementDecision()
        {
            try
            {
                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam;
                for (int i = 0; i < this.form.MasterItem.Columns.Count; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();
                    var setDate = this.form.MasterItem[i, this.form.MasterItem.CurrentRow.Index];
                    var control = setDate as ICustomControl;

                    popupParam.Key = setDate.OwningColumn.DataPropertyName;

                    if ("ImeMode" == setDate.OwningColumn.DataPropertyName)
                    {
                        popupParam.Value = (ImeMode)Enum.ToObject(typeof(ImeMode), int.Parse(setDate.Value.ToString()));
                    }
                    else
                    {
                        popupParam.Value = setDate.Value.ToString();
                    }

                    if (setParamList.ContainsKey(setDate.ColumnIndex == 0 ? 1 : 2))
                    {
                        setParam = setParamList[setDate.ColumnIndex == 0 ? 1 : 2];
                        setParamList.Remove(setDate.ColumnIndex == 0 ? 1 : 2);
                    }
                    else
                    {
                        setParam = new List<PopupReturnParam>();
                    }


                    setParam.Add(popupParam);
                    setParamList.Add(setDate.ColumnIndex == 0 ? 1 : 2, setParam);

                }

                var staticPopupParam = new PopupReturnParam();

                //初期化のため、ベタ書きにて実装
                staticPopupParam.Key = "Text";
                staticPopupParam.Value = string.Empty;
                setParam = new List<PopupReturnParam>();
                setParam = setParamList[2];
                setParamList.Remove(2);

                setParam.Add(staticPopupParam);
                setParamList.Add(2, setParam);


                this.form.ReturnParams = setParamList;
                this.form.DialogResult = DialogResult.OK;
                this.form.Close();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

    }
}
