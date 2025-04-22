using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Message;
using Shougun.Function.ShougunCSCommon.Dto;

namespace ItakuKeiyakuHoshu.APP
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class SelectLogic : IBuisinessLogic
    {

        /// <summary>
        /// Form
        /// </summary>
        private SelectForm form;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectLogic(SelectForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // システム設定から初期値設定
            this.form.KEIYAKUSHO_SHOSHIKI.Text = CommonShogunData.SYS_INFO.ITAKU_KEIYAKU_TYPE.ToString();
            this.form.KEIYAKUSHO_SHURUI.Text = CommonShogunData.SYS_INFO.ITAKU_KEIYAKU_SHURUI.ToString();

            // イベントの初期化処理
            this.EventInit();

            //フォーカス当てる
            this.form.KEIYAKUSHO_SHOSHIKI.Focus();

            if (AppConfig.IsManiLite)
            {
                // マニライト版(C8)の初期化処理
                ManiLiteInit();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // Formキーイベント生成
            this.form.KeyUp += new KeyEventHandler(this.form.ControlKeyUp);

            // 表示ボタン(F9)イベント生成
            this.form.bt_ptn9.Click += new EventHandler(this.bt_ptn9_Click);

            // 閉じるボタン(F12)イベント生成
            this.form.bt_ptn12.Click += new EventHandler(this.bt_ptn12_Click);

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 委託契約書種類非表示(収集運搬契約固定)
            this.form.LABEL_KEIYAKUSHO_SHURUI.Visible = false;
            this.form.KEIYAKUSHO_SHURUI_PANEL.Visible = false;
            this.form.KEIYAKUSHO_SHURUI.Visible = false;

            this.form.KEIYAKUSHO_SHURUI.Text = "1";

            // 表示位置修正
            var shoshikiLabelPoint = this.form.LABEL_KEIYAKUSHO_SHOSHIKI.Location;
            this.form.LABEL_KEIYAKUSHO_SHOSHIKI.Location = new System.Drawing.Point(shoshikiLabelPoint.X, shoshikiLabelPoint.Y + 30);

            var shoshikiPanelPoint = this.form.KEIYAKUSHO_SHOSHIKI_PANEL.Location;
            this.form.KEIYAKUSHO_SHOSHIKI_PANEL.Location = new System.Drawing.Point(shoshikiPanelPoint.X, shoshikiPanelPoint.Y + 30);
        }

        /// <summary>
        /// 閉じるボタン[F12]処理
        /// </summary>
        internal void bt_ptn12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 確定ボタン[F9]処理
        /// </summary>
        internal void bt_ptn9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            
            // 登録チェック
            var hasError = RegistCheck();
            if (hasError)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            this.form.Close();

            // 引数遷移先パラメータ設定dto
            BusinessBaseForm baseForm = null;
            Form activeForm = Form.ActiveForm;
            // 20150902 katen #12048 「システム日付」の基準作成、適用 start
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT CONVERT(DATE, GETDATE()) AS DATE_TIME");//DBサーバ日付を取得する
            // 20150902 katen #12048 「システム日付」の基準作成、適用 end
            var shoshiki = this.form.KEIYAKUSHO_SHOSHIKI.Text;
            var shurui = this.form.KEIYAKUSHO_SHURUI.Text;
            var touroku = CommonShogunData.SYS_INFO.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Value;
            switch (shoshiki)
            {
                case "1":
                    ItakuKeiyakuHoshuForm keiyakuFormZensan = new ItakuKeiyakuHoshuForm(shurui, touroku);
                    baseForm = new BusinessBaseForm(keiyakuFormZensan, WINDOW_TYPE.NEW_WINDOW_FLAG);
                    // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                    if (dt.Rows.Count > 0)
                    {
                        baseForm.sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
                    }
                    // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    if (activeForm != null)
                    {
                        baseForm.StartPosition = FormStartPosition.Manual;
                        baseForm.SetBounds(activeForm.Bounds.Location.X,
                                           activeForm.Bounds.Location.Y,
                                           baseForm.Width,
                                           baseForm.Height);
                    }
                    baseForm.Show();
                    break;
                case "2":
                    ItakuKeiyakuKenpaiHoshuForm keiyakuFormKenpai = new ItakuKeiyakuKenpaiHoshuForm(shurui, touroku);
                    baseForm = new BusinessBaseForm(keiyakuFormKenpai, WINDOW_TYPE.NEW_WINDOW_FLAG);
                    // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                    if (dt.Rows.Count > 0)
                    {
                        baseForm.sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
                    }
                    // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    if (activeForm != null)
                    {
                        baseForm.StartPosition = FormStartPosition.Manual;
                        baseForm.SetBounds(activeForm.Bounds.Location.X,
                                           activeForm.Bounds.Location.Y,
                                           baseForm.Width,
                                           baseForm.Height);
                    }
                    baseForm.Show();
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns>true:エラー有, false:エラー無</returns>
        private bool RegistCheck()
        {
            // BaseFormクラスがSuperPopupFormで、FWのレジストチェックに対応できない
            // そのためShougun.Core.Messageを参照して、自前でメッセージを編集して一括で表示しています。
            bool hasError = false;
            List<string> errorMsgList = new List<string>();

            // 必須チェック
            if (string.IsNullOrEmpty(this.form.KEIYAKUSHO_SHOSHIKI.Text))
            {
                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                var msgStr = CreateErrorMessage(msg.Text, "委託契約書書式");

                errorMsgList.Add(msgStr);
                this.form.KEIYAKUSHO_SHOSHIKI.IsInputErrorOccured = true;
                hasError = true;
            }

            if (string.IsNullOrEmpty(this.form.KEIYAKUSHO_SHURUI.Text))
            {
                var msg = Shougun.Core.Message.MessageUtility.GetMessage("E001");
                var msgStr = CreateErrorMessage(msg.Text, "委託契約書種類");

                errorMsgList.Add(msgStr);
                this.form.KEIYAKUSHO_SHURUI.IsInputErrorOccured = true;
                hasError = true;
            }

            if (hasError)
            {
                // エラー表示
                StringBuilder sb = new StringBuilder();
                foreach(var errorMsg in errorMsgList)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append(Environment.NewLine);
                    }

                    sb.Append(errorMsg);
                }

                MessageBoxUtility.MessageBoxShowError(sb.ToString());
            }

            return hasError;
        }

        /// <summary>
        /// メッセージのformat形成
        /// </summary>
        /// <param name="errorMessage">メッセージ</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateErrorMessage(string errorMessage, params string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                errorMessage = errorMessage.Replace("{" + i + "}", str[i]);
            }

            return errorMessage;
        }

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
    }
}
