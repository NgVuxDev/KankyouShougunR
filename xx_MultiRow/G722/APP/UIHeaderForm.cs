using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku2
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        public LogicClass logic;    // No.3822

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        public UIHeaderForm(UIForm targetForm)
        {
            this.form = targetForm;
            InitializeComponent();
        }

        // No.3822-->
        /// <summary>
        /// KYOTEN_CD Key Press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress_KYOTEN_CD(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (this.logic != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                    if (!forward)
                    {
                        if ((this.form.beforbeforControlName == "KYOTEN_CD" && this.KYOTEN_CD.TabStop)
                            || (this.form.beforbeforControlName == "UKETSUKE_NUMBER" && this.UKETSUKE_NUMBER.TabStop && !this.KYOTEN_CD.TabStop)
                            || (this.form.beforbeforControlName == "KEIRYOU_NUMBER" && this.KEIRYOU_NUMBER.TabStop && !this.UKETSUKE_NUMBER.TabStop && !this.KYOTEN_CD.TabStop))
                        {
                            this.form.EMPTY_KEIRYOU_TIME.Focus();
                            return;
                        }

                    }

                    if (this.form.beforbeforControlName == "gcMultiRow1")
                    {
                        this.form.beforbeforControlName = "UKETSUKE_NUMBER";
                    }

                    this.ActiveControl = this.allControl.Where(c => c.Name == this.form.beforbeforControlName).FirstOrDefault();

                    //PhuocLoc 2020/12/01 #136220 -Start
                    //if (this.ActiveControl == null)
                    //    this.ActiveControl = this.form.allControl.Where(c => c.Name == this.form.beforbeforControlName).FirstOrDefault();
                    //PhuocLoc 2020/12/01 #136220 -End

                    this.logic.GotoNextControl(forward);
                }
            }
        }

        /// <summary>
        /// 計量番号のテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G053）が呼び出された時の引数に受付番号が含まれていた場合、
            // 初回起動時にフラグが立たないようにするための対策
            if (!this.form.isArgumentKeiryouNumber)
            {
                // 計量番号のテキストが変更されたらフラグをたてる
                this.form.KeiryouNumberTextChangeFlg = true;
            }
            else
            {
                this.form.KeiryouNumberTextChangeFlg = false;
                this.form.isArgumentKeiryouNumber = false;
            }
        }

        /// <summary>
        /// 計量番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_NUMBER_Validated(object sender, EventArgs e)
        {
            // 新規のみ
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 計量番号が変更されていない時は何もしない
                if (this.form.KeiryouNumberTextChangeFlg)
                {
                    // 計量番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ計量番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.KEIRYOU_NUMBER.Text))
                    {
                        // 初期化
                        this.form.KeiryouNumberTextChangeFlg = false;

                        if (!this.KEIRYOU_NUMBER.Text.Equals(this.form.KeiryouNumber.ToString()))
                        {
                            // No.2599-->
                            this.form.gcMultiRow1.BeginEdit(false);
                            this.form.gcMultiRow1.Rows.Clear();
                            this.form.gcMultiRow1.EndEdit();
                            // No.2599<--

                            this.form.KeiryouNumber = long.Parse(this.KEIRYOU_NUMBER.Text);
                            if (!this.logic.GetKeiryouNumber())
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
                            bool catchErr = false;
                            bool PattenName = this.logic.KeiryouBangoCheck(out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 受入番号のテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G053）が呼び出された時の引数に受付番号が含まれていた場合、
            // 初回起動時にフラグが立たないようにするための対策
            if (!this.form.isArgumentUketsukeNumber)
            {
                // 受付番号のテキストが変更されたらフラグをたてる
                this.form.UketsukeNumberTextChangeFlg = true;
            }
            else
            {
                this.form.UketsukeNumberTextChangeFlg = false;
                this.form.isArgumentUketsukeNumber = false;
            }
        }

        /// <summary>
        /// 受付番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            // 検収データの場合何もしない
            if (this.KENSHU_MUST_KBN.Checked && (this.logic.kenshuZumi.Equals(this.txtKensyuu.Text))) return;

            // 新規のみ
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 受付番号が変更されていない時は何もしない
                if (this.form.UketsukeNumberTextChangeFlg)
                {
                    // 受付番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ受付番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.UKETSUKE_NUMBER.Text))
                    {
                        // 初期化
                        this.form.UketsukeNumberTextChangeFlg = false;

                        if (!this.UKETSUKE_NUMBER.Text.Equals(this.form.UketukeNumber.ToString()))
                        {
                            bool catchErr = false;

                            // No.2599-->
                            this.form.gcMultiRow1.BeginEdit(false);
                            this.form.gcMultiRow1.Rows.Clear();
                            this.form.gcMultiRow1.EndEdit();
                            // No.2599<--

                            this.form.notEditingOperationFlg = true;
                            catchErr = this.logic.GetUketsukeNumber();
                            this.form.notEditingOperationFlg = false;
                            if (!catchErr) return;

                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
                            this.logic.UketukeBangoCheck(out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

                            long uketsukeNum = -1;
                            if (long.TryParse(this.UKETSUKE_NUMBER.Text, out uketsukeNum))
                            {
                                this.form.UketukeNumber = uketsukeNum;
                            }
                            else
                            {
                                this.form.UketukeNumber = -1;
                            }
                        }
                        else
                        {
                            if (this.logic.tUketsukeSkEntry == null)
                            {
                                // 更新用受付データ再取得
                                this.logic.GetUketsukeData();
                            }
                        }
                    }
                    else
                    {
                        this.logic.ClearTUketsukeSkEntry();
                    }
                }
            }
        }
        // No.3822<--
    }
}
