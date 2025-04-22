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
using r_framework.APP.PopUp.Base;
using System.Data.SqlTypes;

namespace Shougun.Core.ElectronicManifest.SyobunnShuryouHoukokuIkkatuNyuuryoku
{
    public partial class UIForm : SuperForm
    {

        /// <summary>
        /// 入力内容
        /// </summary>
        public InputInfoDTOCls[] inputInfo;

        /// <summary>
        /// 出力内容
        /// </summary>
        public OutputInfoDTOCls[] outputInfo;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// 車輌エラーフラグ
        /// </summary>
        public bool isGyoushaError { get; set; }

        public UIForm()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            //メッセージクラス
            messageShowLogic = new MessageBoxShowLogic();
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 初期化
            this.logic.WindowInit();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }


        /// <summary>
        /// 全て選択ボタン押下処理
        /// </summary>
        private void bt_allSelect_Click(object sender, EventArgs e)
        {
            // 全て選択または全て解除
            this.logic.selectAllorNo();
        }

        /// <summary>
        /// 消去ボタン押下処理
        /// </summary>
        private void bt_Erase_Click(object sender, EventArgs e)
        {
            // 画面クリア
            this.logic.clearWindow();
        }

        /// <summary>
        /// 閉じるボタン押下処理
        /// </summary>
        private void bt_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 入力ボタン押下処理
        /// </summary>
        private void bt_Input_Click(object sender, EventArgs e)
        {
            // 一括出力実行
            if (!this.logic.allOutputExe())
            {
                return;
            }

            this.Close();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                // 全て選択または全て解除
                this.logic.selectAllorNo();
            }
            if (e.KeyCode == Keys.F9)
            {

                this.cDt_SyobunnSyuuryouhi.Focus();
                if (FocusOutErrorFlag)
                {
                    return;
                }

                // 一括出力実行
                if (!this.logic.allOutputExe())
                {
                    return;
                }

                this.Close();
            }
            if (e.KeyCode == Keys.F11)
            {
                // 画面クリア
                this.logic.clearWindow();
            }
            if (e.KeyCode == Keys.F12)
            {
                this.Close();
            }
        }


        /// <summary>
        /// ポップアップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyaryouCD_Enter(object sender, EventArgs e)
        {
            //cantxt_SyaryouCD.PopupDataSource = this.logic.Chksyaryou(cantxt_SyaryouCD.Text, cantxt_hideGyoushaCd.Text);
            bool catchErr = false;
            DataTable retDate = this.logic.Chksyaryou("", cantxt_hideGyoushaCd.Text, out catchErr);
            if (catchErr)
            {
                return;
            }

            cantxt_SyaryouCD.PopupDataSource = retDate;
            cantxt_SyaryouCD.PopupDataHeaderTitle = new string[] { "車輌CD", "車輌名" };
            cantxt_SyaryouCD.PopupDataSource.TableName = "車輌検索";
        }

        /// <summary>
        /// 車輌CDフォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyaryouCD_Leave(object sender, EventArgs e)
        {

            FocusOutErrorFlag = false;
            // 車輌名を設定する
            if (!string.Empty.Equals(cantxt_SyaryouCD.Text))
            {
                bool catchErr = false;
                DataTable result = this.logic.Chksyaryou(cantxt_SyaryouCD.Text, cantxt_hideGyoushaCd.Text, out catchErr);
                if (catchErr)
                {
                    return;
                }
                
                // データあるかどうか
                if (result.Rows.Count > 0)
                {
                    // 車輌略称名
                    ctxt_SyaryouName.Text = result.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    isGyoushaError = false;
                }
                else
                {
                    // フォーカスを設定する
                    cantxt_SyaryouCD.Focus();
                    // フォーカスを設定する
                    cantxt_SyaryouCD.BackColor = Constans.ERROR_COLOR;
                    // NULLを設定する
                    ctxt_SyaryouName.Text = string.Empty;
                    // エラーメッセージ
                    messageShowLogic.MessageBoxShow("E020", "車輛");
                    isGyoushaError = true;
                    FocusOutErrorFlag = true;
                }
            }
            else
            {
                ctxt_SyaryouName.Text = string.Empty;
                isGyoushaError = false;
            }
        }

        /// <summary>
        /// 車輌CD_BackColorChanged
        /// </summary>
        private void cantxt_SyaryouCD_BackColorChanged(object sender, EventArgs e)
        {
            if (isGyoushaError)
            {
                this.cantxt_SyaryouCD.BackColor = Constans.ERROR_COLOR;
            }
        }

        /// <summary>
        /// 受入量フォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ukeireryou_Leave(object sender, EventArgs e)
        {
            //if (!string.Empty.Equals(cantxt_ukeireryou.Text))
            //{
            //    if (!cantxt_ukeireryou.Text.Contains(".") && !cantxt_ukeireryou.Text.Contains(","))
            //    {
            //        if (SqlInt32.Parse(cantxt_ukeireryou.Text) <= 0 || SqlInt32.Parse(cantxt_ukeireryou.Text) >= 100000)
            //        {
            //            // フォーカスを設定する
            //            cantxt_ukeireryou.Focus();
            //            // フォーカスを設定する
            //            cantxt_ukeireryou.BackColor = Constans.ERROR_COLOR;
            //            messageShowLogic.MessageBoxShow("W001", "99999.999", "0.001");
            //        }
            //    }
            //    else
            //    {
            //        string valEx = @"^[0-9]{1,5}\.[0-9]{1,3}$";
            //        if (!System.Text.RegularExpressions.Regex.IsMatch(cantxt_ukeireryou.Text, valEx))
            //        {
            //            // フォーカスを設定する
            //            cantxt_ukeireryou.Focus();
            //            // フォーカスを設定する
            //            cantxt_ukeireryou.BackColor = Constans.ERROR_COLOR;
            //            messageShowLogic.MessageBoxShow("W001", "99999.999", "0.001");
            //        }
            //    }
            //}
            string str = cantxt_ukeireryou.Text.Replace(",", "");

            if (!string.Empty.Equals(str))
            {
                if (decimal.Parse(str) <= 0 || decimal.Parse(str) >= 100000)
                {
                    // フォーカスを設定する
                    cantxt_ukeireryou.Focus();
                    // フォーカスを設定する
                    cantxt_ukeireryou.BackColor = Constans.ERROR_COLOR;
                    messageShowLogic.MessageBoxShow("W001", "99999.999", "0.001");
                }
            }
        }

        /// <summary>
        /// 処分終了日TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunnSyuuryouhi_TextChanged(object sender, EventArgs e)
        {
            if (this.cantxt_SyobunnSyuuryouhi.Text.Equals("2")
                || this.cantxt_SyobunnSyuuryouhi.Text.Equals("3"))
            {
                this.cDt_SyobunnSyuuryouhi.Enabled = false;
                this.cDt_SyobunnSyuuryouhi.Value = null;
            }
            else
            {
                this.cDt_SyobunnSyuuryouhi.Enabled = true;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //this.cDt_SyobunnSyuuryouhi.Value = this.logic.parentForm.sysDate;
                this.cDt_SyobunnSyuuryouhi.Value = this.logic.now;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        /// <summary>
        /// 廃棄物受領日TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_haikibutuJyuryouhi_TextChanged(object sender, EventArgs e)
        {
            if (this.cantxt_haikibutuJyuryouhi.Text.Equals("2")
                || this.cantxt_haikibutuJyuryouhi.Text.Equals("3"))
            {
                this.cDt_haikibutuJyuryouhi.Enabled = false;
                this.cDt_haikibutuJyuryouhi.Value = null;
            }
            else
            {
                this.cDt_haikibutuJyuryouhi.Enabled = true;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 start
                //this.cDt_haikibutuJyuryouhi.Value = this.logic.parentForm.sysDate;
                this.cDt_haikibutuJyuryouhi.Value = this.logic.now;
                // 20151110 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        /// <summary>
        /// 運搬担当者TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_unnnpannTanntousya_TextChanged(object sender, EventArgs e)
        {
            if (this.cantxt_unnnpannTanntousya.Text.Equals("2")
                || this.cantxt_unnnpannTanntousya.Text.Equals("3"))
            {
                this.cantxt_UnnpannTanntousyaCD.Enabled = false;
            }
            else
            {
                this.cantxt_UnnpannTanntousyaCD.Enabled = true;
            }
        }

        /// <summary>
        /// 車輌番号TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_Syaryou_TextChanged(object sender, EventArgs e)
        {
            if (this.cantxt_Syaryou.Text.Equals("2")
                || this.cantxt_Syaryou.Text.Equals("3"))
            {
                this.cantxt_SyaryouCD.Enabled = false;
            }
            else
            {
                this.cantxt_SyaryouCD.Enabled = true;
            }
        }

        /// <summary>
        /// 授入量TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_ukeireryouKbn_TextChanged(object sender, EventArgs e)
        {
            if (this.cantxt_ukeireryouKbn.Text.Equals("2")
                || this.cantxt_ukeireryouKbn.Text.Equals("3"))
            {
                this.cantxt_ukeireryou.Enabled = false;
                this.cantxt_ukeireryouTanniCD.Enabled = false;
            }
            else
            {
                this.cantxt_ukeireryou.Enabled = true;
                this.cantxt_ukeireryouTanniCD.Enabled = true;
            }
        }

        /// <summary>
        /// 備考フォーカス取得時のIME制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_bikou_Enter(object sender, EventArgs e)
        {
            if (this.ctxt_bikou.ImeMode != ImeMode.Hiragana || this.ctxt_bikou.ImeMode != ImeMode.Katakana)
            {
                this.ctxt_bikou.ImeMode = ImeMode.Hiragana;
            }
        }
        /// <summary>
        /// 備考検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxt_bikou_Validating(object sender, CancelEventArgs e)
        {
            FocusOutErrorFlag = false;
            if (this.KinsokuMoziCheck(ctxt_bikou.Text) == false)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E071", "備考");
                e.Cancel = true;
                FocusOutErrorFlag = true;
            }
        }
        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="insertVal">登録項目</param>
        public bool KinsokuMoziCheck(string insertVal)
        {
            Validator v = new Validator();
            if (!v.isJWNetValidShiftJisCharForSign(insertVal))
            {
                return false;
            }
            return true;
        }

    }
}
