using System;
using System.Collections;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Authority;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using System.Linq;
using System.Drawing;
using r_framework.CustomControl;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Seasar.Framework.Exceptions;
using DataGridViewCheckBoxColumnHeeader;
using System.Data;

namespace Shougun.Core.ElectronicManifest.DenmaniSaishuShobun
{
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ElectronicManifest.DenmaniSaishuShobun.LogicClass MILogic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string befJigyousyaCD = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.DENSHI_MANIFEST_SAISHU_SHOBUN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            if (isLoaded == false)
            {
                //初期化、初期表示
                if (!this.MILogic.WindowInit())
                {
                    return;
                }

                //キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;
            }

            // 権限チェック
            if (Manager.CheckAuthority("G687", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.ParentBaseForm.bt_func9.Enabled = true;    // JWNET送信
            }
            else if (Manager.CheckAuthority("G687", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                this.ParentBaseForm.bt_func9.Enabled = false;    // JWNET送信
            }
            this.isLoaded = true;
        }

        #region 画面コントロールイベント
        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.Ichiran, true, true, DENSHU_KBN.DENSHI_MANIFEST_SAISHU_SHOBUN.ToTitleString(), this);
        }
        /// <summary>
        /// [F7]並び替えをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            if (this.MILogic.Search_TME.Rows.Count > 0)
            {
                // チェックボックスはソート条件に含めたくないので、一旦非表示にする
                this.Ichiran.Columns["CHECKBOX"].Visible = false;

                this.customSortHeader1.ShowCustomSortSettingDialog();

                this.Ichiran.Columns["CHECKBOX"].Visible = true;
            }
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // 日付チェック
            bool catchErr = false;
            bool retCheck = this.MILogic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (!retCheck)
            {
                if (this.MILogic.Search() == 0)
                {
                    // 検索結果が存在しない場合メッセージ表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                }
            }
        }

        /// <summary>
        /// JWNET送信(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            this.MILogic.JWEInsert();
        }

        /// <summary>
        /// 取消(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.MILogic.Clear();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            parentForm.Close();
        }

        #endregion

        private void SyoriKubun_Radio1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SyoriKubun_Radio1.Checked)
            {
                this.SyoriKubun_CD.Text = "1";
            }
        }

        private void SyoriKubun_Radio2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SyoriKubun_Radio2.Checked)
            {
                this.SyoriKubun_CD.Text = "2";
            }
        }

        private void DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_TO.Text))
            {
                this.DATE_TO.IsInputErrorOccured = false;
                this.DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_FROM.Text))
            {
                this.DATE_FROM.IsInputErrorOccured = false;
                this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// データグリッドのセルが描画されるときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
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
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt2);
                    e.Handled = true;
                }
            }
            else if (e.RowIndex == -1)
            {
                string name = this.Ichiran.Columns[e.ColumnIndex].Name;
                if (name == "NEXT_MANIFEST_ID" || name == "NEXT_SBN_GYOUSHA_NAME" || name == "NEXT_SBN_GENBA_NAME" || name == "LAST_SBN_END_DATE")
                {
                    this.Ichiran.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.FromArgb(0, 51, 160);
                }
            }
        }

        /// <summary>
        /// 一覧のセルがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
            {
                if (this.Ichiran.RowCount > 0)
                {
                    if (this.Ichiran.Columns[e.ColumnIndex].Name == "CHECKBOX")
                    {
                        // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                        this.CHECK_ALL.Checked = !this.CHECK_ALL.Checked;

                        // チェックボックスの全ての状態を書き換え
                        foreach (DataGridViewRow row in this.Ichiran.Rows)
                        {
                            row.Cells["CHECKBOX"].Value = this.CHECK_ALL.Checked;
                        }

                        // 再描画
                        this.MILogic.parentForm.txb_process.Focus();
                        this.Ichiran.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// 選択されている行があるかを表すフラグ
        /// </summary>
        /// <returns>True:選択されている行がある False:選択されている行がない</returns>
        internal bool IsRowSelected()
        {
            var ret = false;
            var selectedRowCount = this.Ichiran.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue).Count();
            if (selectedRowCount > 0)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void JigyousyaCD_PopupBeforeExecuteMethod()
        {
            this.befJigyousyaCD = this.Jigyousya_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void JigyousyaCD_PopupAfterxecuteMethod()
        {
            if (this.befJigyousyaCD != this.Jigyousya_CD.Text)
            {
                this.Jigyoujou_CD.Text = string.Empty;
                this.JIGYOUJOU_NAME.Text = string.Empty;
            }
        }
    }
}
