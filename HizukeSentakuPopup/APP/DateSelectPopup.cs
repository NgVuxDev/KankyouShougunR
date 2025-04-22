using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dto;

namespace HizukeSentakuPopup.APP
{
    /// <summary>
    /// カレンダーポップアップ
    /// </summary>
    public partial class DateSelectPopup : SuperPopupForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DateSelectPopup()
            : base(WINDOW_ID.KOKYAKU_ITIRAN)
        {
            InitializeComponent();
            this.monthCalendar1.Focus();
        }

        /// <summary>
        /// 選択ボタン押下時処理
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {

            Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
            List<PopupReturnParam> paramList = new List<PopupReturnParam>();
            PopupReturnParam setParam = new PopupReturnParam();
            var setDate = this.monthCalendar1.SelectionStart.ToShortDateString();

            setParam.Key = "Text";
            setParam.Value = setDate;

            paramList.Add(setParam);

            setParamList.Add(1, paramList);

            base.ReturnParams = setParamList;

            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 閉じるボタン押下時処理
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void monthCalendar1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> paramList = new List<PopupReturnParam>();
                PopupReturnParam setParam = new PopupReturnParam();
                var setDate = this.monthCalendar1.SelectionStart.ToShortDateString();

                setParam.Key = "Text";
                setParam.Value = setDate;

                paramList.Add(setParam);

                setParamList.Add(1, paramList);

                base.ReturnParams = setParamList;

                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}