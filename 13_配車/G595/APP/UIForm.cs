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
using r_framework.Utility;

namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou
{
    public partial class UIForm : SuperForm
    {
        #region Fields
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region Constructors
        public UIForm()
            : base(WINDOW_ID.T_CONTENA_RIREKI_ICHIRAN_HYOU, WINDOW_TYPE.NONE)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();
            this.WindowId = WINDOW_ID.T_CONTENA_RIREKI_ICHIRAN_HYOU;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region Inits
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }
        #endregion

        #region F5 CSVイベント
        /// <summary>
        /// F5 CSVイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // 必須入力エラーチェック
            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;
                return;
            }

            if (this.logic.DateCheck())
            {
                return;
            }

            // コンテナ種類、業者、現場、コンテナが空の場合は自動でセット
            if (!this.logic.SetContenaShuruiCdFromTo()) { return; }
            if (!this.logic.SetGyoushaCdFromTo()) { return; }
            if (!this.logic.SetGenbaCdFromTo()) { return; }
            if (!this.logic.SetContenaCdFromTo()) { return; }

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = String.Empty;
            bool catchErr = false;
            errMsg = this.logic.CheckErr(out catchErr);
            if (catchErr) { return; }
            if (!String.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");
                Cursor.Current = Cursors.Arrow;
                return;
            }
            this.logic.CSVPrint();
            
            Cursor.Current = Cursors.Arrow;
        }
        #endregion

        #region Functions
        /// <summary>Ｆ７キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // 必須入力エラーチェック
            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;
                return;
            }

            /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　start
            if (this.logic.DateCheck())
            {
                return;
            }
            /// 20141203 Houkakou 「コンテナ履歴一覧表」の日付チェックを追加する　end

            // コンテナ種類、業者、現場、コンテナが空の場合は自動でセット
            if (!this.logic.SetContenaShuruiCdFromTo()) { return; }
            if (!this.logic.SetGyoushaCdFromTo()) { return; }
            if (!this.logic.SetGenbaCdFromTo()) { return; }
            if (!this.logic.SetContenaCdFromTo()) { return; }

            // FromToのチェックがうまくいかないので自前でチェックする
            var errMsg = String.Empty;
            bool catchErr = false;
            errMsg = this.logic.CheckErr(out catchErr);
            if (catchErr) { return; }
            if (!String.IsNullOrEmpty(errMsg))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E032", errMsg + "From", errMsg + "To");
                Cursor.Current = Cursors.Arrow;
                return;
            }

            this.logic.Print();

            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }
        #endregion

        #region 入力項目別イベント
        /// <summary>
        /// 業者／現場設定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gyoushaGenbaSetting_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeGyoushaAndGenbaEnabled();
        }

        /// <summary>
        /// コンテナ種類FromTextChanged処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contenaShuruiFrom_TextChanged(object sender, EventArgs e)
        {
            // コンテナ名状態更新
            this.logic.checkContenaNameCtrlStatus();
        }

        /// <summary>
        /// コンテナ種類ToTextChanged処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contenaShuruiTo_TextChanged(object sender, EventArgs e)
        {
            // コンテナ名状態更新
            this.logic.checkContenaNameCtrlStatus();
        }

        /// <summary>
        /// コンテナ名FromValidating処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contenaFrom_Validating(object sender, CancelEventArgs e)
        {
            // コンテナ名が空の場合はチェックを行わない
            if (false == string.IsNullOrEmpty(this.contenaFrom.Text))
            {
                // 既にエラーが発生している場合はチェックを行わない
                if (this.FocusOutErrorFlag == false)
                {
                    // コンテナ名チェック
                    var contenaName = string.Empty;
                    if (false == this.logic.checkContenaNameValidate(this.contenaFrom.Text, ref contenaName))
                    {
                        // 入力をキャンセル
                        e.Cancel = true;
                    }
                    else
                    {
                        // コンテナ名のセット
                        this.contenaMeiFrom.Text = contenaName;
                    }
                }
            }
            else
            {
                // 空の場合は名称をクリア
                this.contenaMeiFrom.Text = string.Empty;
            }
        }

        /// <summary>
        /// コンテナ名ToValidating処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contenaTo_Validating(object sender, CancelEventArgs e)
        {
            // コンテナ名が空の場合は処理を行わない
            if (false == string.IsNullOrEmpty(this.contenaTo.Text))
            {
                // 既にエラーが発生している場合はチェックを行わない
                if (this.FocusOutErrorFlag == false)
                {
                    // コンテナ名チェック
                    var contenaName = string.Empty;
                    if (false == this.logic.checkContenaNameValidate(this.contenaTo.Text, ref contenaName))
                    {
                        // 入力をキャンセル
                        e.Cancel = true;
                    }
                    else
                    {
                        // コンテナ名のセット
                        this.contenaMeiTo.Text = contenaName;
                    }
                }
            }
            else
            {
                this.contenaMeiTo.Text = string.Empty;
            }
        }
        #endregion

        #region 業者CD(From)のイベント
        /// <summary>
        /// 業者CD(From)のEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gyoushaFrom_Enter(object sender, EventArgs e)
        {
            this.SetBeforeGyoushaCdFrom();
        }

        /// <summary>
        /// 業者CD(From)のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gyoushaFrom_Validating(object sender, CancelEventArgs e)
        {
            this.CheckGyoushaCdFrom();
        }

        /// <summary>
        /// 変更前の業者CD(From)を保存
        /// 検索ボタンクリック時にも呼び出したいのでメソッド化
        /// </summary>
        public void SetBeforeGyoushaCdFrom()
        {
            this.logic.beforeGyoushaCdFrom = this.gyoushaFrom.Text;
        }

        /// <summary>
        /// 業者CD(From)のチェック
        /// 検索ボタンクリック時にも呼び出したいのでメソッド化
        /// </summary>
        public void CheckGyoushaCdFrom()
        {
            this.logic.CheckGyoushaCdFrom();
        }
        #endregion

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 現場更新後処理
        /// <summary>
        /// 現場CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void genbaFrom_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.gyoushaFrom.Text;
            var genbaCd = this.genbaFrom.Text;
            int fromToFlg = 1;

            if (!this.logic.ErrorCheckGenba(gyoushaCd, genbaCd, fromToFlg))
            {
                this.genbaFrom.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.genbaFrom.Text))
            {
                this.genbaFrom.Text = String.Empty;
                this.genbaMeiFrom.Text = String.Empty;
            }
            LogUtility.DebugMethodEnd();
        }

        private void genbaTo_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCd = this.gyoushaFrom.Text;
            var genbaCd = this.genbaTo.Text;
            int fromToFlg = 2;

            if (!this.logic.ErrorCheckGenba(gyoushaCd, genbaCd, fromToFlg))
            {
                this.genbaTo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.genbaTo.Text))
            {
                this.genbaTo.Text = String.Empty;
                this.genbaMeiTo.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 現場CDに関連する項目をセットする
        /// </summary>
        public void SetGenba()
        {
            var gyoushaCd = this.gyoushaFrom.Text;
            var genbaCd = this.genbaFrom.Text;
            bool catchErr = false;
            var genba = this.logic.GetGenba(gyoushaCd, gyoushaCd, out catchErr);
            if (catchErr) { return; }
            if (genba != null && genba.Count() > 0)
            {
                this.genbaMeiFrom.Text = genba[0].GENBA_NAME_RYAKU;
            }
           
        }
        #endregion
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
    }
}
