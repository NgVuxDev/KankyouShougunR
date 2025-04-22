using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Shougun.Core.Billing.InxsSeikyuushoHakkou.APP;
using Shougun.Core.Billing.InxsSeikyuushoHakkou.Const;
using r_framework.Dto;
using System.Collections.ObjectModel;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.FormManager;
using System.Web.Script.Serialization;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Shougun.Core.Billing.InxsSeikyuushoHakkou
{
    public partial class UIForm : SuperForm
    {
        #region プロパティ
        /// <summary>
        ///　帳票出力用支払データ
        /// </summary>
        public DataTable SeikyuDt { get; set; }

        internal string transactionSettingUserId;

        internal string transactionUploadId;
        #endregion プロパティ

        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// HeaderInxsSeikyuushoHakkou.cs
        /// </summary>
        UIHeader headerForm;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        #endregion

        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_INXS_SEIKYUSHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        { 
            this.InitializeComponent();
            this.SeikyuuDenpyouIchiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.SetHeaderForm(headerForm);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            checkBoxAll.SendToBack();
        }

        public UIForm(UIHeader headerForm, DTOClass dto)
            : base(WINDOW_ID.T_INXS_SEIKYUSHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.SeikyuuDenpyouIchiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.SetHeaderForm(headerForm);

            this.logic.InitDto = dto;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            checkBoxAll.SendToBack();
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (string.IsNullOrEmpty(this.transactionSettingUserId))
            {
                this.transactionSettingUserId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrEmpty(this.transactionUploadId))
            {
                this.transactionUploadId = Guid.NewGuid().ToString();
            }

            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.SeikyuuDenpyouIchiran != null)
            {
                this.SeikyuuDenpyouIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbShimebi.Text))
            {
                MessageBox.Show(ConstCls.ErrStop2, ConstCls.DialogTitle);
                return;
            }

            if (string.IsNullOrEmpty(this.PRINT_ORDER.Text))
            {
                errMsg.MessageBoxShow("E001", "印刷順");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_PAPER.Text))
            {
                errMsg.MessageBoxShow("E001", "請求用紙");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_STYLE.Text))
            {
                errMsg.MessageBoxShow("E001", "請求形態");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYUSHO_PRINTDAY.Text))
            {
                errMsg.MessageBoxShow("E001", "請求年月日");
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYU_HAKKOU.Text))
            {
                errMsg.MessageBoxShow("E001", "請求書発行日");
                return;
            }

            if (string.IsNullOrEmpty(this.HIKAE_INSATSU_KBN.Text))
            {
                errMsg.MessageBoxShow("E001", "発行区分");
                return;
            }

            if (string.IsNullOrEmpty(this.FILTERING_DATA.Text))
            {
                errMsg.MessageBoxShow("E001", "抽出データ");
                return;
            }

            if (string.IsNullOrEmpty(this.UPLOAD_STATUS.Text))
            {
                errMsg.MessageBoxShow("E001", "アップロード状況");
                return;
            }

            this.headerForm.DenpyouHidukeFrom.IsInputErrorOccured = false;
            this.headerForm.DenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            this.headerForm.DenpyouHidukeTo.IsInputErrorOccured = false;
            this.headerForm.DenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;

            if (!string.IsNullOrEmpty(this.headerForm.DenpyouHidukeFrom.GetResultText())
                && !string.IsNullOrEmpty(this.headerForm.DenpyouHidukeTo.GetResultText()))
            {
                DateTime dtpFrom = DateTime.Parse(this.headerForm.DenpyouHidukeFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.headerForm.DenpyouHidukeTo.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    this.headerForm.DenpyouHidukeFrom.IsInputErrorOccured = true;
                    this.headerForm.DenpyouHidukeFrom.BackColor = Constans.ERROR_COLOR;
                    this.headerForm.DenpyouHidukeTo.IsInputErrorOccured = true;
                    this.headerForm.DenpyouHidukeTo.BackColor = Constans.ERROR_COLOR;
                    errMsg.MessageBoxShow("E030", "請求日付From", "請求日付To");
                    this.headerForm.DenpyouHidukeFrom.Select();
                    this.headerForm.DenpyouHidukeFrom.Focus();

                    return;
                }
            }

            if (this.logic.Search() == -1)
            {
                return;
            }
            if (!this.logic.SetIchiran())
            {
                return;
            }

            // 列ヘッダチェックボックスを初期化
            checkBoxAll.Checked = false;
        }

        ///// <summary>
        ///// 登録処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Regist(object sender, EventArgs e)
        //{
        //    this.logic.Regist();

        //}


        /// <summary>
        /// プレビュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PreView(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Enabled = false;
                this.logic.PreView(false);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Parent.Enabled = true;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.logic.SearchResult = new DataTable();
            parentForm.Close();
        }

        /// <summary>
        /// [1]INXSアップロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UploadToINXS(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Enabled = false;
                this.logic.UploadToINXS();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Parent.Enabled = true;
            }
        }

        private void Kyoten_Cd_Leave(object sender, EventArgs e)
        {
            int i;
            if (!int.TryParse(this.KYOTEN_CD.Text, out i))
            {
                this.KYOTEN_CD.Text = "";
            }
        }

        #region DBGRIDVIEW

        // 列ヘッダーにチェックボックスを表示
        private void SeikyuuDenpyouItiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                    Point pt1 = new Point((bmp.Width - checkBoxAll.Width) / 2, (bmp.Height - checkBoxAll.Height + 28) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

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
        private void SeikyuuDenpyouItiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                SeikyuuDenpyouIchiran.Refresh();
                checkBoxAll.Focus();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuDenpyouItiran_Enter(object sender, EventArgs e)
        {
            if (this.SeikyuuDenpyouIchiran.CurrentRow != null)
            {
                this.SeikyuuDenpyouIchiran.CurrentCell = this.SeikyuuDenpyouIchiran.CurrentRow.Cells[ConstCls.COL_SEIKYUU_NUMBER];
            }
        }
        /// <summary>
        /// 発行列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SeikyuuDenpyouIchiran.Rows.Count == 0)
            {
                return;
            }
            var currentCell = this.SeikyuuDenpyouIchiran.CurrentCell;
            this.SeikyuuDenpyouIchiran.CurrentCell = null;
            foreach (DataGridViewRow row in this.SeikyuuDenpyouIchiran.Rows)
            {
                row.Cells[ConstCls.COL_HAKKOU].Value = checkBoxAll.Checked;
            }
            this.SeikyuuDenpyouIchiran.CurrentCell = currentCell;
            this.SeikyuuDenpyouIchiran.Refresh();
        }

        #endregion

        private void SEIKYUSHO_PRINTDAY_TextChanged(object sender, EventArgs e)
        {
            this.logic.CdtSiteiPrintHidukeEnable(SEIKYUSHO_PRINTDAY.Text);
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.ExportCSV();
        }

        #region Communicate PublishedUserSetting
        string seikyuuNumber = string.Empty;

        private void SeikyuuDenpyouItiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            try
            {
                //PubLishedUserSettingsDto
                LogUtility.DebugMethodStart(sender, e);

                //品名ポップアップボタンをクリックする時
                if (this.SeikyuuDenpyouIchiran.Columns[e.ColumnIndex].Name.Equals(ConstCls.COL_PUBLISHED_USER_SETTING_BUTTON)
                    && !this.SeikyuuDenpyouIchiran[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    seikyuuNumber = this.SeikyuuDenpyouIchiran.Rows[e.RowIndex].Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString();
                    string pubLishedUserSettings = string.Empty;
                    var userSettings = logic.GetKagamiUserSettings(this.SeikyuuDenpyouIchiran.Rows[e.RowIndex]);
                    if (userSettings != null)
                    {
                        pubLishedUserSettings = JsonUtility.SerializeObject(userSettings);
                    }

                    RemoteAppCls remoteAppCls = new RemoteAppCls();
                    var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                    {
                        TransactionId = transactionSettingUserId,
                        ReferenceID = seikyuuNumber
                    });

                    var closeToken = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                    {
                        TransactionId = transactionSettingUserId
                    });

                    short denpyouShuruiNyuukin = 10;
                    FormManager.OpenFormSubApp("S011", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "InxsSeikyuushoHakkou", seikyuuNumber, pubLishedUserSettings, token, this.logic.parentForm.Text, denpyouShuruiNyuukin, closeToken);

                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                    if (arg != null)
                    {
                        var msgDto = (CommunicateAppDto)arg;
                        var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                        if (token != null)
                        {
                            var tokenDto = (CommunicateTokenDto)token;
                            //Recieve msg from setting user form
                            if (tokenDto.TransactionId == this.transactionSettingUserId
                                && tokenDto.ReferenceID != null && tokenDto.ReferenceID.ToString() != string.Empty)
                            {
                                if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                                {
                                    logic.SetPublishedUserSetting(tokenDto.ReferenceID.ToString(), msgDto.Args[0].ToString());
                                }
                            }
                            //Recieve msg from upload form
                            if (tokenDto.TransactionId == this.transactionUploadId)
                            {
                                if (msgDto.Args[0] != null)
                                {
                                    var listSeikyuuNumber = JsonUtility.DeserializeObject<List<long>>(msgDto.Args[0].ToString());
                                    if (listSeikyuuNumber != null)
                                    {
                                        logic.LoadUploadStatus(listSeikyuuNumber.ToArray());
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }
        
        internal void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSubAppForm();
        }

        private void CloseSubAppForm()
        {
            try
            {
                RemoteAppCls remoteAppCls = new RemoteAppCls();
                var closeToken = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                {
                    TransactionId = transactionSettingUserId
                });
                var closeFromDto = new CloseFormDto()
                {
                    FormID = "S011",
                    Token = closeToken,
                    Type = ExternalConnection.CommunicateLib.Enums.NotificationType.CloseForm,
                    Args = null
                };
                remoteAppCls.CloseForm(Constans.StartFormText, closeFromDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        // <summary>
        /// 控え印刷処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PrintDirect(object sender, EventArgs e)
        {
            try
            {
                this.Parent.Enabled = false;
                this.logic.PreView(true);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Parent.Enabled = true;
            }
        }
    }
}
