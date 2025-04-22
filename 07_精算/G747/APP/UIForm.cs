using System;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using r_framework.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.Dto;
using r_framework.FormManager;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko
{
    public partial class UIForm : SuperForm
    {

        internal string transactionSettingUserId;

        internal string transactionUploadId;

        #region プロパティ
        /// <summary>
        ///　帳票出力用支払データ
        /// </summary>
        public DataTable ShiharaiDt { get; set; }
        #endregion プロパティ

        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// ヘッダ
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// CheckedChangedイベントの処理を行うかどうか
        /// </summary>
        internal bool IsCheckedChangedEventRun = true;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_INXS_SHIHARAI_MESAISHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.dgvSeisanDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            //this.logic = new LogicClass(this, headerForm);
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            chkHakko.SendToBack();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="dto">画面初期表示DTO</param>
        public UIForm(UIHeader headerForm, DTOClass dto)
            : base(WINDOW_ID.T_INXS_SHIHARAI_MESAISHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.dgvSeisanDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            //this.logic = new LogicClass(this, headerForm);
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            this.logic.InitDto = dto;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            chkHakko.SendToBack();
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
            if (this.dgvSeisanDenpyouItiran != null)
            {
                this.dgvSeisanDenpyouItiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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
        /// プレビュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Function5Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.Function5ClickLogic(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Function8Click(object sender, EventArgs e)
        {
            this.logic.Function8ClickLogic();
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

        #region グリッド発行列のチェックボックス設定
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.logic.SeisanDenpyouItiranCellPaintingLogic(e);

        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.SeisanDenpyouItiranCellClickLogic(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSeisanDenpyouItiran_Enter(object sender, EventArgs e)
        {
            if (this.dgvSeisanDenpyouItiran.CurrentRow != null)
            {
                this.dgvSeisanDenpyouItiran.CurrentCell = this.dgvSeisanDenpyouItiran.CurrentRow.Cells["colDenpyoNumber"];
            }
        }
        /// <summary>
        /// 発行列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.checkBoxAllCheckedChangedLogic();
        }

        #endregion グリッド発行列のチェックボックス設定

        #region ラジオボタン項目未選択時の自動設定
        /// <summary>
        /// 各ラジオボタンに対応したテキストボックスのValidatedイベント発生時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void SetOfRadioButtonNotSelected(object sender, EventArgs e)
        //{
        //    r_framework.CustomControl.CustomNumericTextBox2 textBox = (r_framework.CustomControl.CustomNumericTextBox2)sender;
        //}
        #endregion ラジオボタン項目未選択時の自動設定

        #region 印刷日変更処理
        /// <summary>
        /// 印刷日変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInsatsubi_TextChanged(object sender, EventArgs e)
        {
            this.logic.CdtSiteiPrintHidukeEnable(txtInsatsubi.Text);
        }
        #endregion 印刷日変更処理

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #region Communicate PublishedUserSetting
        string seisanNumber = string.Empty;
        private void dgvSeisanDenpyouItiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            try
            {
                //PubLishedUserSettingsDto
                LogUtility.DebugMethodStart(sender, e);

                //品名ポップアップボタンをクリックする時
                if (this.dgvSeisanDenpyouItiran.Columns[e.ColumnIndex].Name.Equals(ConstCls.COL_PUBLISHED_USER_SETTING_BUTTON)
                    && !this.dgvSeisanDenpyouItiran[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    seisanNumber = this.dgvSeisanDenpyouItiran.Rows[e.RowIndex].Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString();
                    string pubLishedUserSettings = string.Empty;
                    var userSettings = logic.GetKagamiUserSettings(this.dgvSeisanDenpyouItiran.Rows[e.RowIndex]);
                    if (userSettings != null)
                    {
                        pubLishedUserSettings = JsonUtility.SerializeObject(userSettings);
                    }

                    RemoteAppCls remoteAppCls = new RemoteAppCls();
                    var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                    {
                        TransactionId = transactionSettingUserId,
                        ReferenceID = seisanNumber
                    });
                    var closeToken = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                    {
                        TransactionId = transactionSettingUserId
                    });

                    short denpyouShuruiShukkin = 20;
                    FormManager.OpenFormSubApp("S011", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, "InxsShiharaiMeisaishoHakkou", seisanNumber, pubLishedUserSettings, token, this.logic.parentForm.Text, denpyouShuruiShukkin, closeToken);

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
                                    var listseisanNumber = JsonUtility.DeserializeObject<List<long>>(msgDto.Args[0].ToString());
                                    if (listseisanNumber != null)
                                    {
                                        logic.LoadUploadStatus(listseisanNumber.ToArray());
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
                //var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                //{
                //    TransactionId = transactionSettingUserId,
                //    ReferenceID = seisanNumber
                //});
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

        private void dgvSeisanDenpyouItiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvSeisanDenpyouItiran.Columns[e.ColumnIndex].Name == ConstCls.COL_PUBLISHED_USER_SETTING_BUTTON)
            {
                logic.parentForm.lb_hint.Text = "公開ユーザー設定画面を表示します";
            }
        }

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
                this.logic.Function5ClickLogic(true);
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
