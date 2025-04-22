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
using System.Collections.ObjectModel;
using r_framework.Dto;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{
    /// <summary>
    ///G131_交付等状況報告書
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
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }

        internal bool isSearchErr { get; set; }

        private string preGyoushaCd { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// UIForm（「交付等状況報告書条件指定ポップアップ」）
        /// </summary>
        public UIForm(): base(WINDOW_ID.T_KOUHUJYOKYO_HOKOKUSHO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
            try
            {
                base.OnLoad(e);
                //初期化
                if (!this.logic.WindowInit()) { return; }
                this.ParentBaseForm = (BasePopForm)this.Parent;

                // 一時的な表示制限
                this.AddLimitation();
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
                    if (this.txtGYOUSHASetKbn1.Text.Equals("1"))//「提出業者設定」＝ 個別
                    {
                        if (string.IsNullOrEmpty(this.txtGENBA_CD.Text) || string.IsNullOrEmpty(this.txtGENBA_CD_TO.Text))
                        {
                            if (!this.logic.SetTeishutsuJigyoujou()) { return; }
                        }
                    }
                    else//「提出業者設定」＝ 全体
                    {
                        if (string.IsNullOrEmpty(this.txtGYOUSHA_CD.Text) || string.IsNullOrEmpty(this.txtGYOUSHA_CD_TO.Text))
                        {
                            if (!this.logic.SetTeishutsuGyousha()) { return; }
                        }
                    }

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
                this.Close();
                this.ParentBaseForm.Close();
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

        #region コントロール制御（enabled）
        private void txtKbn_TextChanged(object sender, EventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.SetControlEnabled();
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

        #region 都道府県政令市※を取得
        private void txtCHIIKI_CD_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                CustomTextBox obj = (CustomTextBox)sender;
                //地域情報 ポップアップ初期化
                this.logic.PopUpDataInit(obj);
            }
            catch (Exception ex)
            {
                LogUtility.Error( ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 交付年月日を1年前に
        public void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 日付範囲チェック（下限チェック）
                if (!this.logic.KoufuDateRangeCheck(1))
                {
                    return;
                }

                DateTime hiduke_from = DateTime.Parse(this.HIDUKE_FROM.Text);
                DateTime hiduke_to = DateTime.Parse(this.HIDUKE_TO.Text);

                hiduke_from = hiduke_from.AddYears(-1);
                hiduke_to = hiduke_to.AddYears(-1);

                this.HIDUKE_FROM.Text = hiduke_from.ToString();
                this.HIDUKE_TO.Text = hiduke_to.ToString();
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

        #region 交付年月日を1年後に
        public void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 日付範囲チェック（上限チェック）
                if (!this.logic.KoufuDateRangeCheck(0))
                {
                    return;
                }

                DateTime hiduke_from = DateTime.Parse(this.HIDUKE_FROM.Text);
                DateTime hiduke_to = DateTime.Parse(this.HIDUKE_TO.Text);

                hiduke_from = hiduke_from.AddYears(1);
                hiduke_to = hiduke_to.AddYears(1);

                this.HIDUKE_FROM.Text = hiduke_from.ToString();
                this.HIDUKE_TO.Text = hiduke_to.ToString();
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

        /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_TO.Text))
            {
                this.HIDUKE_TO.IsInputErrorOccured = false;
                this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HIDUKE_FROM.Text))
            {
                this.HIDUKE_FROM.IsInputErrorOccured = false;
                this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　end

        /// <summary>
        /// 画面起動時の表示項目や設定値に制限を追加する。
        /// これは暫定的なものです。根本的なものは、そもそも画面ロジックに適合させていくべきです。
        /// </summary>
        private void AddLimitation()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.customPanel7.Visible = false;

                this.label4.Location = new Point(this.label4.Location.X, this.label4.Location.Y - 21);
                this.txtGYOUSHA_CD.Location = new Point(this.txtGYOUSHA_CD.Location.X, this.txtGYOUSHA_CD.Location.Y - 21);
                this.txtGYOUSHA_NAME.Location = new Point(this.txtGYOUSHA_NAME.Location.X, this.txtGYOUSHA_NAME.Location.Y - 21);
                this.label7.Location = new Point(this.label7.Location.X, this.label7.Location.Y - 21);
                this.txtGYOUSHA_CD_TO.Location = new Point(this.txtGYOUSHA_CD_TO.Location.X, this.txtGYOUSHA_CD_TO.Location.Y - 21);
                this.txtGYOUSHA_NAME_TO.Location = new Point(this.txtGYOUSHA_NAME_TO.Location.X, this.txtGYOUSHA_NAME_TO.Location.Y - 21);

                this.label14.Location = new Point(this.label14.Location.X, this.label14.Location.Y - 21);
                this.txtGENBA_CD.Location = new Point(this.txtGENBA_CD.Location.X, this.txtGENBA_CD.Location.Y - 21);
                this.txtGENBA_NAME.Location = new Point(this.txtGENBA_NAME.Location.X, this.txtGENBA_NAME.Location.Y - 21);
                this.label16.Location = new Point(this.label16.Location.X, this.label16.Location.Y - 21);
                this.txtGENBA_CD_TO.Location = new Point(this.txtGENBA_CD_TO.Location.X, this.txtGENBA_CD_TO.Location.Y - 21);
                this.txtGENBA_NAME_TO.Location = new Point(this.txtGENBA_NAME_TO.Location.X, this.txtGENBA_NAME_TO.Location.Y - 21);

                this.label10.Location = new Point(this.label10.Location.X, this.label10.Location.Y - 21);
                this.customPanel8.Location = new Point(this.customPanel8.Location.X, this.customPanel8.Location.Y - 21);

                this.label8.Location = new Point(this.label8.Location.X, this.label8.Location.Y - 21);
                this.txtGenbaNm.Location = new Point(this.txtGenbaNm.Location.X, this.txtGenbaNm.Location.Y - 21);
                this.label19.Location = new Point(this.label19.Location.X, this.label19.Location.Y - 21);

                this.label20.Location = new Point(this.label20.Location.X, this.label20.Location.Y - 21);
                this.txtGenbaAddress.Location = new Point(this.txtGenbaAddress.Location.X, this.txtGenbaAddress.Location.Y - 21);

                this.label9.Location = new Point(this.label9.Location.X, this.label9.Location.Y - 21);
                this.pnlChiikiNmBkn.Location = new Point(this.pnlChiikiNmBkn.Location.X, this.pnlChiikiNmBkn.Location.Y - 21);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.GyoushaValidated(sender);
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.GenbaValidated(sender);
        }

        /// <summary>
        /// 提出者 PopupAfterExecuteMethod
        /// </summary>
        public void txtGYOUSHA_CD_PopupAfterExecuteMethod()
        {
            if (this.txtGYOUSHA_CD.Text != this.preGyoushaCd)
            {
                if (this.txtGYOUSHASetKbn1.Text == "1")
                {
                    this.txtGENBA_CD.Text = string.Empty;
                    this.txtGENBA_NAME.Text = string.Empty;
                    this.txtGENBA_CD_TO.Text = string.Empty;
                    this.txtGENBA_NAME_TO.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 提出者 PopupBeforeExecuteMethod
        /// </summary>
        public void txtGYOUSHA_CD_PopupBeforeExecuteMethod()
        {
            this.preGyoushaCd = this.txtGYOUSHA_CD.Text;
        }
    }
}
