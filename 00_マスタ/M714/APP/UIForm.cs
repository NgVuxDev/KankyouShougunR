using System;
using Seasar.Quill.Attrs;
using r_framework.APP.Base;
using r_framework.Utility;
using r_framework.Logic;
using r_framework.Const;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Master.ChiikiIkkatsu
{
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public string ChiikiCdNewPre = string.Empty;
        #endregion

        #region イベント

        /// <summary>
        /// コンストラクター
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_CHIIKI_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.CHECK_ALL.SendToBack();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic = new LogicClass(this);
                this.logic.WindowInit();

				// Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        #endregion

        #region 業務処理

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.CHECK_ALL.Checked = false;

            if (string.IsNullOrEmpty(this.CHIIKI_CD_OLD.Text)
                && string.IsNullOrEmpty(this.JYUUSHO_OLD.Text))
            {
                int k = this.customDataGridView1.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.customDataGridView1.Rows.RemoveAt(this.customDataGridView1.Rows[i - 1].Index);
                }
                this.CHIIKI_CD_OLD.IsInputErrorOccured = true;
                this.JYUUSHO_OLD.IsInputErrorOccured = true;
                this.errmessage.MessageBoxShow("E012", "変更前地域　または　住所");
                this.CHIIKI_CD_OLD.Focus();
                return;
            }

            this.logic.dto = new DTOClass();

            if (!string.IsNullOrEmpty(this.CHIIKI_HENKOU_HOUHOU.Text))
                this.logic.dto.Henkouhouhou = this.CHIIKI_HENKOU_HOUHOU.Text;
            else
                this.logic.dto.Henkouhouhou = string.Empty;

            if (!string.IsNullOrEmpty(this.CHIIKI_MASTER_MANE.Text))
                this.logic.dto.ChiikiMasterName = this.CHIIKI_MASTER_MANE.Text;
            else
                this.logic.dto.ChiikiMasterName = string.Empty;

            if (!string.IsNullOrEmpty(this.CHIIKI_CD_OLD.Text))
                this.logic.dto.ChiikiCdOld = this.CHIIKI_CD_OLD.Text;
            else
                this.logic.dto.ChiikiCdOld = string.Empty;

            if (!string.IsNullOrEmpty(this.JYUUSHO_OLD.Text))
                this.logic.dto.ChiikiJyuushoOld = this.JYUUSHO_OLD.Text;
            else
                this.logic.dto.ChiikiJyuushoOld = string.Empty;

            this.logic.SearchData(this.logic.dto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F9 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (string.IsNullOrEmpty(this.CHIIKI_CD_NEW.Text))
            {
                this.errmessage.MessageBoxShow("E001", "変更後地域");
                return;
            }

            if (string.IsNullOrEmpty(this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text))
            {
                this.errmessage.MessageBoxShow("E001", "変更後運搬提出先");
                return;
            }

            if (this.CHIIKI_HENKOU_HOUHOU.Text == "1")
            {
                Dictionary<int, List<string>> SentakuData = new Dictionary<int, List<string>>();

                for (int i = 0; i < this.customDataGridView1.Rows.Count; i++)
                {
                    var row = this.customDataGridView1.Rows[i];

                    if (row.Cells["HENKOU_FLG"].Value == null
                           || string.IsNullOrEmpty(row.Cells["HENKOU_FLG"].Value.ToString())
                           || !Convert.ToBoolean(row.Cells["HENKOU_FLG"].Value))
                    {
                        continue;
                    }
                    else
                    {
                        List<string> SentakuInfo = new List<string>();

                        SentakuInfo.Add(row.Cells["GYOUSHA_CD"].Value.ToString());
                        if (row.Cells["GENBA_CD"].Value != null
                            && !string.IsNullOrEmpty(row.Cells["GENBA_CD"].Value.ToString()))
                        {
                            SentakuInfo.Add(row.Cells["GENBA_CD"].Value.ToString());
                        }
                        else
                        {
                            SentakuInfo.Add(string.Empty);
                        }

                        SentakuData.Add(i, SentakuInfo);
                    }
                }

                if (SentakuData != null && SentakuData.Count > 0)
                {
                    this.logic.CreateEntitys(SentakuData);
                }
                else
                {
                    this.errmessage.MessageBoxShow("W002", "地域を変更する");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.CHIIKI_CD_OLD.Text))
                {
                    this.errmessage.MessageBoxShow("E001", "変更前地域");
                    return;
                }

                try
                {
                    using (var tran = new Transaction())
                    {
                        this.logic.GyoushaDao.UpdateChiikiMaster(this.CHIIKI_CD_OLD.Text, this.CHIIKI_CD_NEW.Text, this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text);
                        tran.Commit();
                    }
                    this.errmessage.MessageBoxShow("I001", "登録");
                }
                catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.errmessage.MessageBoxShow("E080");
                }
                catch (Exception ex)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.errmessage.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F11 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.CHIIKI_HENKOU_HOUHOU.Text = "1";
            this.CHIIKI_MASTER_MANE.Text = "1";
            this.CHIIKI_CD_OLD.Text = string.Empty;
            this.CHIIKI_NAME_OLD.Text = string.Empty;
            this.JYUUSHO_OLD.Text = string.Empty;
            this.CHIIKI_CD_NEW.Text = string.Empty;
            this.CHIIKI_NAME_NEW.Text = string.Empty;
            this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text = string.Empty;
            this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_NEW.Text = string.Empty;

            int k = this.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.customDataGridView1.Rows.RemoveAt(this.customDataGridView1.Rows[i - 1].Index);
            }
            this.customDataGridView1.Refresh();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.CancelEdit();
            this.customDataGridView1.CurrentCell = null;
            // フォームを閉じる
            this.Close();
            this.logic.parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 変更方法TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CHIIKI_HENKOU_HOUHOU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.CHIIKI_HENKOU_HOUHOU_TextChanged();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マスタ名TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CHIIKI_MASTER_MANE_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            int k = this.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.customDataGridView1.Rows.RemoveAt(this.customDataGridView1.Rows[i - 1].Index);
            }

            if (!string.IsNullOrEmpty(this.CHIIKI_MASTER_MANE.Text))
            {
                if (this.CHIIKI_MASTER_MANE.Text == "1"
                    || this.CHIIKI_MASTER_MANE.Text == "3"
                    || this.CHIIKI_MASTER_MANE.Text == "5")
                {
                    this.customDataGridView1.Columns["GENBA_CD"].Visible = false;
                    this.customDataGridView1.Columns["GENBA_NAME"].Visible = false;
                }
                else
                {
                    this.customDataGridView1.Columns["GENBA_CD"].Visible = true;
                    this.customDataGridView1.Columns["GENBA_NAME"].Visible = true;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        // 列ヘッダーにチェックボックスを表示
        private void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp.Width - this.CHECK_ALL.Width) / 2, (bmp.Height - this.CHECK_ALL.Height) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    this.CHECK_ALL.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2; ;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt2);
                    e.Handled = true;
                }
            }
        }

        // 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        private void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                this.CHECK_ALL.Checked = !this.CHECK_ALL.Checked;
                this.customDataGridView1.Refresh();
                this.CHECK_ALL.Focus();
            }
        }

        /// <summary>
        /// チェックボックス状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CHECK_ALL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customDataGridView1.Rows.Count == 0)
            {
                return;
            }
            this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[0].Cells[2];
            foreach (DataGridViewRow row in this.customDataGridView1.Rows)
            {
                row.Cells[0].Value = this.CHECK_ALL.Checked;
            }
            this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[0].Cells[0];
        }

        private void CHIIKI_CD_NEW_Validating(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CHIIKI_CD_NEW.Text))
            {
                this.logic.GetChiikiInfo(this.CHIIKI_CD_NEW.Text);
            }
            else
            {
                this.CHIIKI_NAME_NEW.Text = string.Empty;

                if (!this.CHIIKI_CD_NEW.Text.Equals(this.ChiikiCdNewPre))
                {
                    this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_NEW.Text = string.Empty;
                    this.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME_NEW.Text = string.Empty;
                }
            }
        }

        // 変更後地域Enterイベント
        private void CHIIKI_CD_NEW_Enter(object sender, EventArgs e)
        {
            this.ChiikiCdNewPre = this.CHIIKI_CD_NEW.Text;
        }
        // 変更後地域PopupAfterイベント
        public void CHIIKI_CD_NEW_PopupAfter()
        {
            this.logic.GetChiikiInfo(this.CHIIKI_CD_NEW.Text);
        }
        // 変更後地域PopupBeforeイベント
        public void CHIIKI_CD_NEW_PopupBefore()
        {
            this.ChiikiCdNewPre = this.CHIIKI_CD_NEW.Text;
        }

    }
}
