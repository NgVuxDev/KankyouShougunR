using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.CustomControl;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Carriage.UntinSyuusyuuhyoPopup
{
    /// <summary>
    ///コンテナ指定
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }
        /// <summary>
        /// ボッタンフラグ
        /// </summary>
        public bool buttonFlag = false;
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }
        /// <summary>
        /// フォーム幅
        /// </summary>
        public int formWidth = 750;

        /// <summary>
        /// フォーム高さ
        /// </summary>
        public int formHeight = 400;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// UIForm（「運賃集計表条件指定ポップアップ」）
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_UNCHIN_SYUUKEI_JYOUKEN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            messageShowLogic = new MessageBoxShowLogic();
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //初期化
            this.logic.WindowInit();
        }
        #endregion

        #region  実行処理(F9)

        /// <summary>
        /// 実行処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (!base.RegistErrorFlag)
                {

                    /// 20141209 teikyou 日付チェックを追加する　start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141209 teikyou 日付チェックを追加する　end

                    this.logic.Jikou();
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
        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);


                return allControl.ToArray();
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
        #endregion
        #endregion

        #region クローズ処理(F12)
        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnClosed_Click(object sender, EventArgs e)
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
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面キーダウンイウェント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.F12:
                //    btnClosed_Click(null,null);
                //    break;
                case Keys.F9:
                    btnSearch_Click(null, null);
                    break;
            }
            base.OnKeyDown(e);
        }
        #endregion

        #region 運搬業者更新後処理
        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UNPAN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox dbjCd = (CustomTextBox)sender;
                CustomTextBox dbjName = null;
                if (dbjCd.Name.Equals("UNPAN_GYOUSHA_CD"))
                {
                    dbjName = this.UNPAN_GYOUSHA_NAME;
                }
                else
                {
                    dbjName = this.UNPAN_GYOUSHA_NAME_TO;
                }
                // チェックNGの場合
                if (!this.logic.CheckUnpanGyoushaCd(dbjCd, dbjName))
                {
                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    // フォーカス設定
                    dbjCd.Focus();
                    return;
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

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void UnpanGyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

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
        #endregion

        #region 荷済業者更新後処理
        /// <summary>
        /// 荷済業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                CustomTextBox dbjCd = (CustomTextBox)sender;
                CustomTextBox dbjName = null;
                if (dbjCd.Name.Equals("NIZUMI_GYOUSHA_CD"))
                {
                    dbjName = this.NIZUMI_GYOUSHA_NAME;
                }
                else
                {
                    dbjName = this.NIZUMI_GYOUSHA_NAME_TO;
                }

                // チェックNGの場合
                if (!this.logic.CheckNizumiGyoushaCd(dbjCd, dbjName))
                {

                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    // フォーカス設定
                    dbjCd.Focus();
                    return;
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
        #endregion

        #region 荷積場更新後処理
        /// <summary>
        /// 荷積場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox dbjCd = (CustomTextBox)sender;
                CustomTextBox dbjName = null;
                CustomTextBox dbjGyosyaCd = null;
                MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();
                if (dbjCd.Name.Equals("NIZUMI_GENBA_CD"))
                {
                    dbjGyosyaCd = this.NIZUMI_GYOUSHA_CD;
                    dbjName = this.NIZUMI_GENBA_NAME;
                }
                else
                {
                    dbjGyosyaCd = this.NIZUMI_GYOUSHA_CD_TO;
                    dbjName = this.NIZUMI_GENBA_NAME_TO;
                }
                // チェックNGの場合
                if (!this.logic.ChechNizumiGenbaCd(dbjGyosyaCd, dbjCd, dbjName))
                {
                    if (!string.IsNullOrEmpty(dbjCd.Text))
                    {
                        // 背景色変更
                        dbjCd.IsInputErrorOccured = true;
                    }
                    // フォーカス設定
                    dbjCd.Focus();
                    return;
                }
                //dbjCd.IsInputErrorOccured = false;
                //「範囲条件」に荷積現場を指定する場合は、荷積業者の開始/終了CDを同じにしてください。
                if ((!string.IsNullOrEmpty(this.NIZUMI_GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.NIZUMI_GYOUSHA_CD_TO.Text))
                    && !this.NIZUMI_GYOUSHA_CD.Text.Equals(this.NIZUMI_GYOUSHA_CD_TO.Text) && !string.IsNullOrEmpty(dbjCd.Text))
                {
                    myMessageBox.MessageBoxShow("E102", "荷積現場", "荷積業者");
                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    dbjCd.Focus();
                    return;
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
        #endregion

        #region 荷降業者更新後処理
        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                CustomTextBox dbjCd = (CustomTextBox)sender;
                CustomTextBox dbjName = null;
                if (dbjCd.Name.Equals("NIOROSHI_GYOUSHA_CD"))
                {
                    dbjName = this.NIOROSHI_GYOUSHA_NAME;
                }
                else
                {
                    dbjName = this.NIOROSHI_GYOUSHA_NAME_TO;
                }

                // チェックNGの場合
                if (!this.logic.CheckNioroshiGyoushaCd(dbjCd, dbjName))
                {

                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    // フォーカス設定
                    dbjCd.Focus();
                    return;
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
        /// <summary>
        /// 
        /// </summary>
        public virtual void NioroshiGyoshaBtnPopupMethod()
        {
            this.NIOROSHI_GYOUSHA_CD.Focus();
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void NioroshiGyosha_TOBtnPopupMethod()
        {
            this.NIOROSHI_GYOUSHA_CD_TO.Focus();
        }
        #endregion

        #region 荷降場更新後処理
        /// <summary>
        /// 荷降場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox dbjCd = (CustomTextBox)sender;
                CustomTextBox dbjName = null;
                CustomTextBox dbjGyosyaCd = null;
                MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();
                if (dbjCd.Name.Equals("NIOROSHI_GENBA_CD"))
                {
                    dbjGyosyaCd = this.NIOROSHI_GYOUSHA_CD;
                    dbjName = this.NIOROSHI_GENBA_NAME;
                }
                else
                {
                    dbjGyosyaCd = this.NIOROSHI_GYOUSHA_CD_TO;
                    dbjName = this.NIOROSHI_GENBA_NAME_TO;
                }
                // チェックNGの場合
                if (!this.logic.ChechNioroshiGenbaCd(dbjGyosyaCd, dbjCd, dbjName))
                {
                    if (!string.IsNullOrEmpty(dbjCd.Text))
                    {
                        // 背景色変更
                        dbjCd.IsInputErrorOccured = true;
                    }
                    // フォーカス設定
                    dbjCd.Focus();
                    return;
                }
                //「範囲条件」に荷降現場を指定する場合は、荷降業者の開始/終了CDを同じにしてください。
                //dbjCd.IsInputErrorOccured = false;
                if ((!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD_TO.Text))
                    && !this.NIOROSHI_GYOUSHA_CD.Text.Equals(this.NIOROSHI_GYOUSHA_CD_TO.Text) && !string.IsNullOrEmpty(dbjCd.Text))
                {
                    myMessageBox.MessageBoxShow("E102", "荷降現場", "荷降業者");
                    // 背景色変更
                    dbjCd.IsInputErrorOccured = true;
                    dbjCd.Focus();
                    return;
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
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void NioroshiGenbaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD入力された且つ荷降業者名が入力不可且つ未入力の場合
                if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // 業者を取得
                    var gyoushaEntity = this.logic.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text);
                    // 取得できない場合
                    if (gyoushaEntity != null)
                    {
                        // 荷降業者名設定
                        this.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }

                // フォーカスセット
                this.NIOROSHI_GENBA_CD.Focus();
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
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void NioroshiGenba_TOBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD入力された且つ荷降業者名が入力不可且つ未入力の場合
                if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD_TO.Text))
                {
                    // 業者を取得
                    var gyoushaEntity = this.logic.GetGyousha(this.NIOROSHI_GYOUSHA_CD_TO.Text);
                    // 取得できない場合
                    if (gyoushaEntity != null)
                    {
                        // 荷降業者名設定
                        this.NIOROSHI_GYOUSHA_NAME_TO.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }

                // フォーカスセット
                this.NIOROSHI_GENBA_CD_TO.Focus();
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
        #endregion

        #region 帳票データ受け渡し
        /// <summary>
        /// 帳票データ受け渡し
        /// </summary>
        /// <returns></returns>
        public ReportInfoBase getReportInfo()
        {
            return this.logic.ReportInfo;
        }
        #endregion
    }
}
