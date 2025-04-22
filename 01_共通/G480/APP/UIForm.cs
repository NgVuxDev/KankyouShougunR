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
using Shougun.Core.Common.IchiranSyuDenpyou;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.Common.IchiranSyuDenpyou
{
    public partial class UIForm : SuperForm
    {

        //伝種区分
        public String denshuKB = "";

        //システムID
        public String systemID = "";

        //出力対象機能
        public String outputTsKn = "";

        //出力区分
        public String outputKB = "";

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        private Boolean isLoaded = false;

        public UIForm(String paramIn_SystemID)
            : base(WINDOW_ID.C_ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);

                //denshuKB = paramIn_DenshuKb;
                systemID = paramIn_SystemID;

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        //public BusinessBaseForm ParentBaseForm { get; private set; }
        public BasePopForm ParentBaseForm { get; private set; }

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
                this.logic.WindowInit();

                //TextBox1のLostFocusイベントハンドラを追加する
                //OUTPUT_KBN_VALUE.LostFocus += new EventHandler(OUTPUT_KBN_VALUE_LostFocus);

                //選択項目、出力項目の読み込み
                this.logic.ClearScreen();
                this.logic.ClearScreenOutPut();
                this.logic.SearchOutPut();
                this.logic.Search();

                //フォーカスを出力対象機能へ移動
                this.cstTxtBoxSrTsKn.Select();

                isLoaded = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 出力区分
        /// </summary>
        private void OUTPUT_KBN_VALUE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                switch (this.OUTPUT_KBN_VALUE.Text)
                {
                    case "1"://伝票
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.OUTPUT_KBN_DENPYO.Checked = true;
                        this.OUTPUT_KBN_MEISAI.Checked = false;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;

                    case "2"://明細
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.OUTPUT_KBN_DENPYO.Checked = false;
                        this.OUTPUT_KBN_MEISAI.Checked = true;

                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;

                    default://その他
                        //var messageShowLogic = new MessageBoxShowLogic();
                        //messageShowLogic.MessageBoxShow("W001", "1", "2");

                        this.outputKB = "1";
                        this.OUTPUT_KBN_DENPYO.Checked = true;
                        this.OUTPUT_KBN_MEISAI.Checked = false;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }

                        //フォーカスを出力区分へ移動
                        this.OUTPUT_KBN_VALUE.SelectAll();
                        return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OUTPUT_KBN_VALUE_TextChanged", ex);
                throw;
            }
        }

        //private void OUTPUT_KBN_VALUE_Validated(object sender, EventArgs e)
        //{
        //    switch (this.OUTPUT_KBN_VALUE.Text)
        //    {
        //        case "1"://伝票
        //        case "2"://明細
        //            //this.logic.ClearScreen();
        //            //this.logic.Search();   
        //            break;

        //        default://空白など
        //            //var messageShowLogic = new MessageBoxShowLogic();
        //            //messageShowLogic.MessageBoxShow("W001", "1", "2");
        //            this.OUTPUT_KBN_VALUE.Text = "1";

        //            //フォーカスを出力区分へ移動
        //            this.OUTPUT_KBN_VALUE.SelectAll();
        //            break;
        //    }
        //}

        //private void OUTPUT_KBN_VALUE_LostFocus(object sender, EventArgs e)
        //{
        //    switch (this.OUTPUT_KBN_VALUE.Text)
        //    {
        //        case "1"://伝票
        //        case "2"://明細
        //            //this.logic.ClearScreen();
        //            //this.logic.Search();   
        //            break;

        //        default://空白など
        //            var messageShowLogic = new MessageBoxShowLogic();
        //            messageShowLogic.MessageBoxShow("W001", "1", "2");

        //            //フォーカスを出力区分へ移動
        //            this.OUTPUT_KBN_VALUE.Select();
        //            break;
        //    }
        //}

        /// <summary>
        /// 「<」(F1)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.MoveSelect();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customButton1_Click", ex);
                throw;
            }
        }

        /// <summary>
        /// 「>」(F2)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.MoveOutPut();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customButton2_Click", ex);
                throw;
            }
        }

        /// <summary>
        /// 「↑」(F3)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.UpRow();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customButton3_Click", ex);
                throw;
            }
        }

        /// <summary>
        /// 「↓」(F4)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton4_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.DownRow();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customButton4_Click", ex);
                throw;
            }
        }

        /// <summary>
        /// 登録処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.Regist(!base.RegistErrorFlag);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                throw;
            }
        }

        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.FormClose();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
        }

        //private void cstTxtBoxSrTsKn_Validated(object sender, EventArgs e)
        //{
        //    switch (this.cstTxtBoxSrTsKn.Text)
        //    {
        //        case "1":
        //        case "2":
        //        case "3":
        //        case "4":
        //        case "5":
        //        case "6":
        //        case "7":
        //        case "8":
        //        case "9":
        //        case "10":
        //            break;
        //        default://空白など
        //            //var messageShowLogic = new MessageBoxShowLogic();
        //            //messageShowLogic.MessageBoxShow("W001", "1", "10");
        //            this.cstTxtBoxSrTsKn.Text = "1";
        //            //フォーカスを出力区分へ移動
        //            this.cstTxtBoxSrTsKn.SelectAll();
        //            break;
        //    }
        //}

        private void cstTxtBoxSrTsKn_TextChanged(object sender, EventArgs e)
        {
            //if (int.Parse(this.cstTxtBoxSrTsKn.Text) > 10)
            //{
            //    this.cstTxtBoxSrTsKn.Text = this.cstTxtBoxSrTsKn.Text.Substring(1);
            //    return;
            //}

            try
            {
                switch (this.cstTxtBoxSrTsKn.Text)
                {
                    case "1"://受付
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn1.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;

                    case "2"://計量
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn2.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;

                    case "3"://受入
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn3.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "4"://出荷
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn4.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "5"://売上/支払
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn5.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "6"://マニ1次
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn6.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "7"://マニ2次
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn7.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "8"://電マニ
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn8.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "9"://運賃
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn9.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;
                    case "10"://代納
                        this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                        this.cstRdoBtn10.Checked = true;
                        if (isLoaded != false)
                        {
                            this.logic.ClearScreen();
                            this.logic.RightMoveBtnSet();
                            this.logic.LeftMoveBtnSet();
                            this.logic.Search();
                        }
                        break;

                    //default://その他
                    //    //var messageShowLogic = new MessageBoxShowLogic();
                    //    //messageShowLogic.MessageBoxShow("W001", "1", "10");
                    //    this.cstTxtBoxSrTsKn.Text = "1";
                    //    this.outputKB = "1";
                    //    this.cstRdoBtn1.Checked = true;
                    //    if (isLoaded != false)
                    //    {
                    //        this.logic.ClearScreen();
                    //        this.logic.RightMoveBtnSet();
                    //        this.logic.LeftMoveBtnSet();
                    //        this.logic.Search();
                    //    }
                    //    //フォーカスを出力区分へ移動
                    //    this.cstTxtBoxSrTsKn.SelectAll();
                    //    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstTxtBoxSrTsKn_TextChanged", ex);
                throw;
            }
        }

        private void cstTxtBoxSrTsKn_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int intSelLength = this.cstTxtBoxSrTsKn.SelectionLength;
                int intSelStart = this.cstTxtBoxSrTsKn.SelectionStart;
                string str = "";

                if (this.cstTxtBoxSrTsKn.Text.Length == 2)
                {
                    if (intSelLength == 0)
                    {
                        str = this.cstTxtBoxSrTsKn.Text;
                    }
                    else if (intSelLength == 1)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString() + this.cstTxtBoxSrTsKn.Text.Substring(1, 1);
                        }
                        else if (intSelStart == 1)
                        {
                            str = this.cstTxtBoxSrTsKn.Text.Substring(0, 1) + e.KeyChar.ToString();
                        }
                    }
                    else if (intSelLength == 2)
                    {
                        str = e.KeyChar.ToString();
                    }
                }

                if (this.cstTxtBoxSrTsKn.Text.Length == 1)
                {
                    if (intSelLength == 0)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString() + this.cstTxtBoxSrTsKn.Text;
                        }
                        else if (intSelStart == 1)
                        {
                            str = this.cstTxtBoxSrTsKn.Text + e.KeyChar.ToString();
                        }
                    }
                    else if (intSelLength == 1)
                    {
                        if (intSelStart == 0)
                        {
                            str = e.KeyChar.ToString();
                        }
                    }
                }

                str = str.Replace("\b", "");
                str = str.Replace("\t", "");

                if (str.Length > 0 && (int.Parse(str) > 10 || int.Parse(str) == 0))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstTxtBoxSrTsKn_KeyPress", ex);
                throw;
            }
        }

        private void customDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.logic.LeftMoveBtnSet();
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_SelectionChanged", ex);
                throw;
            }
        }

        private void cstTxtBoxSrTsKn_Leave(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(this.cstTxtBoxSrTsKn.Text) == true)
                {

                    this.cstTxtBoxSrTsKn.Text = "1";
                    this.outputKB = "1";
                    this.cstRdoBtn1.Checked = true;
                    if (isLoaded != false)
                    {
                        this.logic.ClearScreen();
                        this.logic.RightMoveBtnSet();
                        this.logic.LeftMoveBtnSet();
                        this.logic.Search();
                    }
                    //フォーカスを出力区分へ移動
                    this.cstTxtBoxSrTsKn.SelectAll();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cstTxtBoxSrTsKn_Leave", ex);
                throw;
            }
        }

        #endregion
    }
}
