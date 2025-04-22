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
using Shougun.Core.Common.IchiranSyu;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.IchiranSyu.Const;

namespace Shougun.Core.Common.IchiranSyu
{
    public partial class UIForm : SuperForm
    {

        //伝種区分
        public String denshuKB = "";

        //システムID
        public String systemID = "";

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

        public UIForm(String paramIn_DenshuKb ,String paramIn_SystemID)
            : base(WINDOW_ID.C_ICHIRANSYUTSURYOKU_KOUMOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            denshuKB = paramIn_DenshuKb;
            systemID = paramIn_SystemID;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
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
            base.OnLoad(e);
            //初期化
            this.logic.WindowInit();

            //TextBox1のLostFocusイベントハンドラを追加する
            //OUTPUT_KBN_VALUE.LostFocus += new EventHandler(OUTPUT_KBN_VALUE_LostFocus);

            //選択項目、出力項目の読み込み
            this.logic.ClearScreen();
            this.logic.Search();

            //フォーカスを出力区分へ移動
            this.customDataGridView2.Select();
            this.OUTPUT_KBN_VALUE.Select();

            isLoaded = true;
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 出力区分
        /// </summary>
        private void OUTPUT_KBN_VALUE_TextChanged(object sender, EventArgs e)
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
                        this.logic.Search();
                    }
                    this.OUTPUT_KBN_VALUE.SelectAll();
                    break;

                case "2"://明細
                    this.outputKB = this.OUTPUT_KBN_VALUE.Text;
                    this.OUTPUT_KBN_DENPYO.Checked = false;
                    this.OUTPUT_KBN_MEISAI.Checked = true;

                    if (isLoaded != false)
                    {
                        this.logic.ClearScreen();
                        this.logic.Search();
                    }
                    this.OUTPUT_KBN_VALUE.SelectAll();
                    break;

                default://その他
                    return;
            }
        }

        //private void OUTPUT_KBN_VALUE_LostFocus(object sender, EventArgs e)
        //{
            //switch (this.OUTPUT_KBN_VALUE.Text)
            //{
            //    case "1"://伝票
            //    case "2"://明細
            //        //this.logic.ClearScreen();
            //        //this.logic.Search();   
            //        break;

            //    default://空白など
            //        var messageShowLogic = new MessageBoxShowLogic();
            //        messageShowLogic.MessageBoxShow("W001", ConstCls.OuptKbn_DenPyou, ConstCls.OuptKbn_Meisai);

            //        //フォーカスを出力区分へ移動
            //        this.OUTPUT_KBN_VALUE.Select();
            //        break;
            //}
        //}

        private void OUTPUT_KBN_VALUE_Validated(object sender, EventArgs e)
        {
            switch (this.OUTPUT_KBN_VALUE.Text)
            {
                case "1"://伝票
                case "2"://明細
                    //this.logic.ClearScreen();
                    //this.logic.Search();   
                    break;

                default://空白など
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("W001", ConstCls.OuptKbn_DenPyou, ConstCls.OuptKbn_Meisai);

                    //フォーカスを出力区分へ移動
                    this.OUTPUT_KBN_VALUE.Select();
                    break;
            }
        }

        /// <summary>
        /// 「<」(F1)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton1_Click(object sender, EventArgs e)
        {
            this.logic.MoveSelect();
        }

        /// <summary>
        /// 「>」(F2)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton2_Click(object sender, EventArgs e)
        {
            this.logic.MoveOutPut();
        }

        /// <summary>
        /// 「↑」(F3)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton3_Click(object sender, EventArgs e)
        {
            this.logic.UpRow();
        }

        /// <summary>
        /// 「↓」(F4)ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customButton4_Click(object sender, EventArgs e)
        {
            this.logic.DownRow();
        }

        /// <summary>
        /// 登録処理(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            this.logic.Regist(!base.RegistErrorFlag);
        }

        /// <summary>
        /// Formクローズ処理(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            this.logic.FormClose();
        }

        #endregion

    }
}
