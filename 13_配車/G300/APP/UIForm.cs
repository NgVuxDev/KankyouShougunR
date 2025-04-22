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
using Shougun.Core.Allocation.Teikihaisyajissekihyou;
using r_framework.Utility;


namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
{

    /// <summary>
    /// 定期配車実績表
    /// </summary>
    public partial class UIForm : SuperForm
    {

        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private TeikihaisyajissekihyouLogicClass logic;

        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        internal string previousGyoushaFrom { get; set; }
        internal string previousGyoushaTo { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;        
        
        #endregion

        #region 初期処理

        #region コンストラクタ
        public UIForm()
            : base(WINDOW_ID.T_TEIKIHAISHA_ZISSEKI_HYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new TeikihaisyajissekihyouLogicClass(this);

                LogUtility.DebugMethodEnd();
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

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
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

        #endregion

        //画面期間を入力しない場合、チェック処理
        public bool registCheck()
        {
            bool returnValue = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (base.RegistErrorFlag)
                {//期間に関するエラー時にカーソルを期間Fromへ設定
                    this.dtp_KikanFrom.Focus();
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue);
            }
        }

        /// <summary>
        /// 出力区分を「月報」に変更した場合に、期間の表示を自動で変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo_Geppou_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo_Geppou.Checked)
            {
                this.dtp_KikanFrom.CustomFormat = "yyyy/MM/dd(ddd)";
                this.dtp_KikanFrom.ShowYoubi = true;
                this.dtp_KikanTo.CustomFormat = "yyyy/MM/dd(ddd)";
                this.dtp_KikanTo.ShowYoubi = true;

                //「期間From」／システム日付
                this.dtp_KikanFrom.Value = this.logic.parentForm.sysDate;

                //「期間To」／作業開始日
                this.dtp_KikanTo.Value = this.logic.parentForm.sysDate;
            }
        }

        /// <summary>
        /// 出力区分を「年報」に変更した場合に、期間の表示を自動で変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo_Nenpou_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo_Nenpou.Checked)   // No.938
            {
                this.dtp_KikanFrom.CustomFormat = "yyyy/MM";
                this.dtp_KikanFrom.ShowYoubi = false;
                this.dtp_KikanTo.CustomFormat = "yyyy/MM";
                this.dtp_KikanTo.ShowYoubi = false;

                if (!string.IsNullOrEmpty(this.dtp_KikanTo.Text))
                {
                    // フォーマットを反映させるため、値の再設定
                    this.dtp_KikanTo.Value = this.dtp_KikanTo.Value;

                    var ymTo = DateTime.Parse(this.dtp_KikanTo.Text);
                    //「期間From」／システム日付
                    this.dtp_KikanFrom.Value = ymTo.AddMonths(-11);
                }
                else
                {
                    //「期間To」／作業開始日
                    this.dtp_KikanTo.Value = this.logic.parentForm.sysDate;
                    //「期間From」／システム日付
                    this.dtp_KikanFrom.Value = this.logic.parentForm.sysDate.AddMonths(-11);
                }
            }
        }

        /// <summary>
        /// 取引先CDFromのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_From_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCd = this.TORIHIKISAKI_CD_From.Text;
            if (!String.IsNullOrEmpty(this.SHIMEBI.Text))
            {
                var shimebi = Int16.Parse(this.SHIMEBI.Text);
                if (!String.IsNullOrEmpty(torihikisakiCd))
                {
                    bool catchErr = false;
                    var torihikisakiSeikyuu = this.logic.GetTorihikisakiSeikyu(torihikisakiCd, out catchErr);
                    if (catchErr)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (torihikisakiSeikyuu == null || (torihikisakiSeikyuu.SHIMEBI1 != shimebi && torihikisakiSeikyuu.SHIMEBI2 != shimebi && torihikisakiSeikyuu.SHIMEBI3 != shimebi))
                    {
                        e.Cancel = true;

                        var messageBox = new MessageBoxShowLogic();
                        messageBox.MessageBoxShow("E058");
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先CDToのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_To_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCd = this.TORIHIKISAKI_CD_To.Text;
            if (!String.IsNullOrEmpty(this.SHIMEBI.Text))
            {
                var shimebi = Int16.Parse(this.SHIMEBI.Text);
                if (!String.IsNullOrEmpty(torihikisakiCd))
                {
                    bool catchErr = false;
                    var torihikisakiSeikyuu = this.logic.GetTorihikisakiSeikyu(torihikisakiCd, out catchErr);
                    if (catchErr)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (torihikisakiSeikyuu == null || (torihikisakiSeikyuu.SHIMEBI1 != shimebi && torihikisakiSeikyuu.SHIMEBI2 != shimebi && torihikisakiSeikyuu.SHIMEBI3 != shimebi))
                    {
                        e.Cancel = true;

                        var messageBox = new MessageBoxShowLogic();
                        messageBox.MessageBoxShow("E058");
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 締日コンボボックスでアイテムが選択されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHIMEBI_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.TORIHIKISAKI_CD_From.Text = String.Empty;
            this.TORIHIKISAKI_NAME_RYAKU_From.Text = String.Empty;
            this.TORIHIKISAKI_CD_To.Text = String.Empty;
            this.TORIHIKISAKI_NAME_RYAKU_To.Text = String.Empty;

            LogUtility.DebugMethodEnd();
        }

        private void GYOUSHA_CD_From_Enter(object sender, EventArgs e)
        {
            this.previousGyoushaFrom = this.GYOUSHA_CD_From.Text;
        }

        private void GYOUSHA_CD_To_Enter(object sender, EventArgs e)
        {
            this.previousGyoushaTo = this.GYOUSHA_CD_To.Text;
        }

        public void GYOUSHA_CD_FROM_PopBef()
        {
            this.previousGyoushaFrom = this.GYOUSHA_CD_From.Text;
        }

        public void GYOUSHA_CD_TO_PopBef()
        {
            this.previousGyoushaTo = this.GYOUSHA_CD_To.Text;
        }

        public void GYOUSHA_CD_FROM_PopAft()
        {
            if (this.previousGyoushaFrom != this.GYOUSHA_CD_From.Text)
            {
                this.ChangeGenbaState(this.GYOUSHA_CD_From);
            }
        }

        public void GYOUSHA_CD_TO_PopAft()
        {
            if (this.previousGyoushaTo != this.GYOUSHA_CD_To.Text)
            {
                this.ChangeGenbaState(this.GYOUSHA_CD_To);
            }
        }

        /// <summary>
        /// 業者CDFromのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_From_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.previousGyoushaFrom != this.GYOUSHA_CD_From.Text)
            {
                this.ChangeGenbaState(sender);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDToのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_To_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.previousGyoushaTo != this.GYOUSHA_CD_To.Text)
            {
                this.ChangeGenbaState(sender);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者の入力状態に応じて現場のコントロールの状態を変更します
        /// </summary>
        private void ChangeGenbaState(object sender)
        {
            LogUtility.DebugMethodStart();

            var controlName = ((TextBox)sender).Name;
            var gyoushaCdFrom = this.GYOUSHA_CD_From.Text;
            var gyoushaCdTo = this.GYOUSHA_CD_To.Text;
            if (!String.IsNullOrEmpty(gyoushaCdFrom) && !String.IsNullOrEmpty(gyoushaCdTo) && gyoushaCdFrom == gyoushaCdTo)
            {
                this.GENBA_CD_From.Enabled = true;
                this.GENBA_CD_To.Enabled = true;

                this.GENBA_CD_From.Text = String.Empty;
                this.GENBA_NAME_RYAKU_From.Text = String.Empty;
                this.GENBA_CD_To.Text = String.Empty;
                this.GENBA_NAME_RYAKU_To.Text = String.Empty;
            }
            else
            {
                this.GENBA_CD_From.Enabled = false;
                this.GENBA_CD_To.Enabled = false;

                this.GENBA_CD_From.Text = String.Empty;
                this.GENBA_NAME_RYAKU_From.Text = String.Empty;
                this.GENBA_CD_To.Text = String.Empty;
                this.GENBA_NAME_RYAKU_To.Text = String.Empty;
            }

            if (controlName.Equals(this.GYOUSHA_CD_To.Name))
            {
                // 上記ロジック内で現場CDの活性状態が活性に切り替わった時、
                // フォーカス遷移時に現場CDを飛び越えてしまうのでここで制御
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.SelectNextControl((Control)this.GYOUSHA_CD_To, !isPressShift, true, true, true);
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
