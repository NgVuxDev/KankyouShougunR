using System;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Master.OboegakiIkkatuHoshu.Logic;

namespace Shougun.Core.Master.OboegakiIkkatuHoshu.APP
{
    public partial class UIForm : SuperForm
    {
        #region 変数

        /// <summary>
        /// 覚書一括入力画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// 伝票番号
        /// </summary>
        private String mDenpyouNumber;

        public bool mstartFlg = false;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        internal string befHaishutsuJigyoushaCd { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_OBOE_IKKATSU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            this.InitializeComponent();
            //伝票番号
            this.mDenpyouNumber = string.Empty;
            this.mstartFlg = true;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(修正削除複写用)
        /// </summary>
        public UIForm(WINDOW_TYPE windowType, T_ITAKU_MEMO_IKKATSU_ENTRY ItakuMemoIkkatsuEntryEntity)
            : base(WINDOW_ID.M_OBOE_IKKATSU, windowType)
        {
            InitializeComponent();
            //伝票番号
            this.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();

            base.WindowType = windowType;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
            this.logic.mDenpyouNumber = ItakuMemoIkkatsuEntryEntity.DENPYOU_NUMBER.ToString();
            this.txtSystemId.Text = ItakuMemoIkkatsuEntryEntity.SYSTEM_ID.ToString();
            this.txtSeq.Text = ItakuMemoIkkatsuEntryEntity.SEQ.ToString();

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit(base.WindowType);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.grdIchiran != null)
                {
                    this.grdIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
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

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!Manager.CheckAuthority("M014", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                // 画面再描画
                this.mDenpyouNumber = string.Empty;
                this.logic.mDenpyouNumber = string.Empty;
                // 画面初期化
                this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                this.PreviousValue = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (Manager.CheckAuthority("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (Manager.CheckAuthority("M014", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                this.txtDenpyouNumber.Text = this.mDenpyouNumber;

                // 画面初期化
                if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    if (!this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent))
                    {
                        return;
                    }
                }
                else if (base.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    if (!this.logic.WindowInitReference((BusinessBaseForm)this.Parent))
                    {
                        return;
                    }
                }

                this.PreviousValue = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!base.RegistErrorFlag)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //更新日・覚書内容のどちらも未入力の場合のみ、エラーです。 チェック
                    if (string.IsNullOrEmpty(this.dtpMemoUpdateDate.Text)
                        && string.IsNullOrEmpty(this.txtMemo.Text))
                    {
                        this.dtpMemoUpdateDate.IsInputErrorOccured = true;
                        this.txtMemo.IsInputErrorOccured = true;
                        msgLogic.MessageBoxShow("E055");
                        return;
                    }
                    else
                    {
                        this.dtpMemoUpdateDate.IsInputErrorOccured = false;
                        this.txtMemo.IsInputErrorOccured = false;
                    }

                    if (!this.logic.CheckSerchKeiyakuDate())
                    {
                        return;
                    }

                    // 登録用データの作成
                    if (!this.logic.CreateEntity(base.WindowType))
                    {
                        return;
                    }

                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.Regist(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "登録");

                                // 権限チェック
                                if (Manager.CheckAuthority("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    // DB登録後、修正モードで再表示
                                    if (!this.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                                    {
                                        return;
                                    }
                                    //base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                    this.mDenpyouNumber = this.txtDenpyouNumber.Text;
                                    this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                                }
                                else
                                {
                                    // 修正権限が無い場合は、画面を閉じる
                                    this.FormClose(sender, e);
                                }
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "更新");

                                // 修正⇒修正モードのため、権限チェックは不要

                                // DB更新後、修正処理モード解除処理を実施
                                this.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                                //base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                this.logic.mDenpyouNumber = this.txtDenpyouNumber.Text;
                                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                            }
                            break;
                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.logic.LogicalDelete();
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "削除");

                                // 権限チェック
                                if (Manager.CheckAuthority("M014", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // DB登録後、新規モードで再表示
                                    if (!this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG))
                                    {
                                        return;
                                    }
                                    //base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                    this.logic.mDenpyouNumber = string.Empty;
                                    this.logic.isRegist = false;
                                    this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                                }
                                else
                                {
                                    // 新規権限が無い場合は、画面を閉じる
                                    this.FormClose(sender, e);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    this.txtDenpyouNumber.Focus();
                    // 画面初期化
                    this.PreviousValue = string.Empty;
                    //// システムIDと枝番クリア（中間処分パターン）
                    //this.txtShobunPatternSysId.Text = string.Empty;
                    //this.txtShobunPatternSeq.Text = string.Empty;
                    //// システムIDと枝番クリア（最終処分パターン）
                    //this.txtLastShobunPatternSysId.Text = string.Empty;
                    //this.txtLastShobunPatternSeq.Text = string.Empty;
                    //// システムIDと枝番クリア（中間処分パターン２）
                    //this.txtShobunPatternSysId2.Text = string.Empty;
                    //this.txtShobunPatternSeq2.Text = string.Empty;
                    //// システムIDと枝番クリア（最終処分パターン２）
                    //this.txtLastShobunPatternSysId2.Text = string.Empty;
                    //this.txtLastShobunPatternSeq2.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.logic.CheckSerchKeiyakuDate())
                {
                    return;
                }

                //this.logic.mDenpyouNumber = string.Empty;
                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    return;
                }
                this.logic.SetIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region F7 一覧画面へ遷移

        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.ShowIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region F12 Formクローズ処理

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 伝票番号変更時

        /// <summary>
        /// 伝票番号変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDenpyouNumber_LostFocus(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (string.IsNullOrEmpty(this.txtDenpyouNumber.Text)
                    || (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.mDenpyouNumber))
                    || (this.txtDenpyouNumber.Text.Equals(this.mDenpyouNumber)))
                {
                    return;
                }

                int count = this.logic.Search(this.txtDenpyouNumber.Text);
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    if (!string.IsNullOrEmpty(this.mDenpyouNumber))
                    {
                        this.txtDenpyouNumber.Text = this.mDenpyouNumber;
                    }
                    else
                    {
                        this.txtDenpyouNumber.Text = string.Empty;
                    }
                    this.txtDenpyouNumber.Focus();
                    return;
                }

                var windowType = WINDOW_TYPE.NONE;
                if (Manager.CheckAuthority("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    windowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (Manager.CheckAuthority("M014", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    this.txtDenpyouNumber.Text = string.Empty;
                    this.txtDenpyouNumber.Focus();

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                this.mDenpyouNumber = this.txtDenpyouNumber.Text;
                this.logic.mDenpyouNumber = this.txtDenpyouNumber.Text;
                //処理モード変更
                if (!this.SetWindowType(windowType))
                {
                    return;
                }
                //base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面初期化
                if (!this.logic.SetEntry())
                {
                    return;
                }
                count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                if (!this.logic.SetIchiran())
                {
                    return;
                }
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == windowType)
                {
                    this.logic.InitDenpyouNumberUpdate((BusinessBaseForm)this.Parent);
                }
                else if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG == windowType)
                {
                    this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtDenpyouNumber_LostFocus", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ウインドウタイプ設定処理

        /// <summary>
        /// ウインドウタイプ設定処理
        /// </summary>
        /// <param name="type"></param>
        public bool SetWindowType(WINDOW_TYPE type)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(type);

                base.WindowType = type;
                base.OnLoad(new EventArgs());
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowType", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 処分パターン処理

        /// <summary>
        ///中間処分パターンをセットする
        /// </summary>
        private void txtShobun_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //中間処分パターンをセットする
                if (!this.txtShobun.Text.Equals("1"))
                {
                    this.txtShobunPatternNm2.Text = string.Empty;
                    this.txtShobunPatternSeq2.Text = string.Empty;
                    this.txtShobunPatternSysId2.Text = string.Empty;
                    this.txtShobunPatternNm2.ReadOnly = true;
                    this.btnShobunPattern2.Enabled = false;
                    this.txtShobunPatternNm2.TabStop = false;
                    this.logic.PatteenRequiredChange(1, false);
                }
                else
                {
                    if (this.txtShobun.Text.Equals("1") && this.txtShobun.ReadOnly == false)
                    {
                        this.txtShobunPatternNm2.ReadOnly = false;
                        this.btnShobunPattern2.Enabled = true;
                        this.txtShobunPatternNm2.TabStop = true;
                        this.logic.PatteenRequiredChange(1, true);
                    }
                    else
                    {
                        this.txtShobunPatternNm2.ReadOnly = true;
                        this.btnShobunPattern2.Enabled = false;
                        this.txtShobunPatternNm2.TabStop = false;
                        this.logic.PatteenRequiredChange(1, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtShobun_TextChanged", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 最終処分パターンをセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLastShobun_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //最終処分パターンをセットする
                if (!this.txtLastShobun.Text.Equals("1"))
                {
                    this.txtLastShobunPatternNm2.Text = string.Empty;
                    this.txtLastShobunPatternSeq2.Text = string.Empty;
                    this.txtLastShobunPatternSysId2.Text = string.Empty;
                    this.txtLastShobunPatternNm2.ReadOnly = true;
                    this.btnLastShobunPattern2.Enabled = false;
                    this.txtLastShobunPatternNm2.TabStop = false;
                    this.logic.PatteenRequiredChange(2, false);
                }
                else
                {
                    if (this.txtLastShobun.Text.Equals("1") && this.txtLastShobun.ReadOnly == false)
                    {
                        this.txtLastShobunPatternNm2.ReadOnly = false;
                        this.btnLastShobunPattern2.Enabled = true;
                        this.txtLastShobunPatternNm2.TabStop = true;
                        this.logic.PatteenRequiredChange(2, true);
                    }
                    else
                    {
                        this.txtLastShobunPatternNm2.ReadOnly = true;
                        this.btnLastShobunPattern2.Enabled = false;
                        this.txtLastShobunPatternNm2.TabStop = false;
                        this.logic.PatteenRequiredChange(2, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtShobun_TextChanged", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void txtShobunPatternNm_Leave(object sender, EventArgs e)
        {
            //処分パターンをセットする
            this.logic.SetPatternData(this.txtShobunPatternNm, this.txtShobunPatternSysId, this.txtShobunPatternSeq, 1);
        }

        private void txtLastShobunPatternNm_Leave(object sender, EventArgs e)
        {
            //処分パターンをセットする
            this.logic.SetPatternData(this.txtLastShobunPatternNm, this.txtLastShobunPatternSysId, this.txtLastShobunPatternSeq, 2);
        }

        private void txtShobunPatternNm2_Leave(object sender, EventArgs e)
        {
            //処分パターンをセットする
            this.logic.SetPatternData(this.txtShobunPatternNm2, this.txtShobunPatternSysId2, this.txtShobunPatternSeq2, 1);
        }

        private void txtLastShobunPatternNm2_Leave(object sender, EventArgs e)
        {
            //処分パターンをセットする
            this.logic.SetPatternData(this.txtLastShobunPatternNm2, this.txtLastShobunPatternSysId2, this.txtLastShobunPatternSeq2, 2);
        }

        #endregion

        #region private メッソド

        private void UPDATE_SHUBETSU_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var c = sender as CustomTextBox;
                if (c != null)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        c.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UPDATE_SHUBETSU_Validated", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void KEIYAKUSHO_SHURUI_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var c = sender as CustomTextBox;
                if (c != null)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        c.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KEIYAKUSHO_SHURUI_Validated", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void txtShobun_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var c = sender as CustomTextBox;
                if (c != null)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        c.Text = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtShobun_Validated", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void txtLastShobun_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var c = sender as CustomTextBox;
                if (c != null)
                {
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        c.Text = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtLastShobun_Validated", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 排出業者POPUP後処理
        /// </summary>
        public void PopupAfterHaishutsuJigyoushaCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.befHaishutsuJigyoushaCd != this.txtHaishutsuJigyoushaCd.Text)
                {
                    this.txtGenbaCd.Text = string.Empty;
                    this.txtGenbaNm.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterHaishutsuJigyoushaCD", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 排出業者POPUP前処理
        /// </summary>
        public void PopupBeforeHaishutsuJigyoushaCD()
        {
            this.befHaishutsuJigyoushaCd = this.txtHaishutsuJigyoushaCd.Text;
        }

        /// <summary>
        /// 業者ロックフォーカス後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaishutsuJigyoushaCd_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.GetHaishutsuGyoushaNm(this.txtHaishutsuJigyoushaCd.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場ロックフォーカス後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            //削除モード時処理しない。
            if (this.txtGenbaCd.ReadOnly)
            {
                return;
            }
            int count = this.logic.GetHaishutsuGenbaNm(this.txtHaishutsuJigyoushaCd, this.txtGenbaCd);
            if (count > 0)
            {
                SendKeys.Send(" ");
                e.Cancel = true;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者ロックフォーカス後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUnbanGyoushaCd_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.GetUnpanJutakusha(this.txtUnbanGyoushaCd.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 中間処分パターンスペースキー押下１

        /// <summary>
        /// 中間処分パターンスペースキー押下１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtShobunPatternNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyChar == 32 || e.KeyChar == 12288) && !this.txtShobunPatternNm.ReadOnly)
                {
                    e.Handled = true;
                    DENSHU_KBN kbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN;

                    bool catchErr = true;
                    String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (PattenName != null)
                    {
                        this.txtShobunPatternNm.Text = PattenName;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 中間処分パターンクリック処理１

        /// <summary>
        /// 中間処分パターンクリック処理１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShobunPattern_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DENSHU_KBN kbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN;

                bool catchErr = true;
                String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (PattenName != null)
                {
                    this.txtShobunPatternNm.Text = PattenName;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 最終処分パターンスペースキー押下１

        /// <summary>
        /// 最終処分パターンスペースキー押下１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLastShobunPatternNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyChar == 32 || e.KeyChar == 12288) && !this.txtLastShobunPatternNm.ReadOnly)
                {
                    e.Handled = true;
                    DENSHU_KBN kbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN;

                    bool catchErr = true;
                    String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (PattenName != null)
                    {
                        this.txtLastShobunPatternNm.Text = PattenName;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 最終処分パターンリック処理１

        /// <summary>
        /// 最終処分パターンリック処理１
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastShobunPattern_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DENSHU_KBN kbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN;

                bool catchErr = true;
                String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (PattenName != null)
                {
                    this.txtLastShobunPatternNm.Text = PattenName;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 中間処分パターンスペースキー押下２

        /// <summary>
        /// 中間処分パターンスペースキー押下２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtShobunPatternNm2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyChar == 32 || e.KeyChar == 12288) && !this.txtShobunPatternNm2.ReadOnly)
                {
                    e.Handled = true;
                    DENSHU_KBN kbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN;

                    bool catchErr = true;
                    String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (PattenName != null)
                    {
                        this.txtShobunPatternNm2.Text = PattenName;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 中間処分パターンクリック処理２

        /// <summary>
        /// 中間処分パターンクリック処理２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShobunPattern2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DENSHU_KBN kbn = DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN;

                bool catchErr = true;
                String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (PattenName != null)
                {
                    this.txtShobunPatternNm2.Text = PattenName;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 最終処分パターンスペースキー押下２

        /// <summary>
        /// 最終処分パターンスペースキー押下２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLastShobunPatternNm2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyChar == 32 || e.KeyChar == 12288) && !this.txtLastShobunPatternNm2.ReadOnly)
                {
                    e.Handled = true;
                    DENSHU_KBN kbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN;

                    bool catchErr = true;
                    String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (PattenName != null)
                    {
                        this.txtLastShobunPatternNm2.Text = PattenName;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 最終処分パターンリック処理２

        /// <summary>
        /// 最終処分パターンリック処理２
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastShobunPattern2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DENSHU_KBN kbn = DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN;

                bool catchErr = true;
                String PattenName = this.logic.OpenPattenPopUp(kbn, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (PattenName != null)
                {
                    this.txtLastShobunPatternNm2.Text = PattenName;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 覚書内容タブ制御

        /// <summary>
        /// 覚書内容タブ制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMemo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                this.SelectNextControl(txtMemo, forward, false, true, true);
                e.Handled = true;
            }
        }

        #endregion

        private void txtPatternNm_TextChanged(object sender, EventArgs e)
        {
            CustomTextBox obj = (CustomTextBox)sender;
            obj.Focus();
        }

        /// <summary>
        /// ヘッダーチェックボックス変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIchiran_CellContentClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.Scope == CellScope.ColumnHeader && grdIchiran.CurrentCell is CheckBoxCell)
            {
                //チェックボックス型セルの値を取得します
                this.logic.ChangeHeaderCheckBox();
            }
        }

        /// <summary>
        /// 排出事業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtHaishutsuJigyoushaCd_Enter(object sender, EventArgs e)
        {
            this.befHaishutsuJigyoushaCd = this.txtHaishutsuJigyoushaCd.Text;
        }

        #region 受付番号「前次」
        /// <summary>
        /// 受付番号「前次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isPrevious">前=True, 次=False</param>
        private void PREVIOUS_NEXT_BUTTON_Click(object sender, EventArgs e, bool isPrevious)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e, isPrevious);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("M014", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 現在の伝票番号を取得
                    long denpyouNumber = string.IsNullOrEmpty(this.txtDenpyouNumber.Text) ? 0 : Convert.ToInt64(this.txtDenpyouNumber.Text);

                    // 前or次の受付番号を取得
                    String number = this.logic.GetPreviousNextNumber(isPrevious, denpyouNumber);
                    if (number == "")
                    {
                        this.errmessage.MessageBoxShow("E045");
                        return;
                    }

                    // 伝票番号を設定
                    this.txtDenpyouNumber.Text = number;

                    // 受付番号更新後処理
                    txtDenpyouNumber_LostFocus(sender, e);
                }
                else
                {
                    this.errmessage.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PREVIOUS_NEXT_BUTTON_Click", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 前ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PREVIOUS_BUTTON_Click(object sender, EventArgs e)
        {
            this.PREVIOUS_NEXT_BUTTON_Click(sender, e, true);
        }

        /// <summary>
        /// 次ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NEXT_BUTTON_Click(object sender, EventArgs e)
        {
            this.PREVIOUS_NEXT_BUTTON_Click(sender, e, false);
        }


    }
}