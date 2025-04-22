// $Id: PatternRegistForm.cs 14808 2014-01-22 06:39:17Z sc.n.tanaka $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Utility;

namespace ItakuKeiyakuHoshu.APP
{
    public partial class PatternRegistForm : SuperPopupForm
    {
      
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private PatternRegistLogic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// パターンのSYSTEM_ID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// パターンのSEQ
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 最終処分区分
        /// 1:処分、2:最終処分
        /// </summary>
        public short LastSbnKbn { get; set; }

        /// <summary>
        /// 委託契約タイプ
        /// </summary>
        public short ItakuKeiyakuType { get; set; }

        internal string beforePatternName = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PatternRegistForm()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.InitializeComponent();

                this.SystemId = 0;
                this.Seq = 0;
                this.LastSbnKbn = 0;
                this.ItakuKeiyakuType = 0;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new PatternRegistLogic(this);
            }
            catch
            {
                throw;
            }
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);
                this.logic.WindowInit();
                this.KasoruHento();
                this.beforePatternName = this.PATTERN_NAME.Text;
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        protected override void OnShown(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnShown(e);

                // Formキーイベント生成
                this.KeyDown += new KeyEventHandler(this.ControlKeyDown);

                // Formキーイベント生成
                this.KeyUp += new KeyEventHandler(this.ControlKeyUp);
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー押下(Down)処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void ControlKeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.imeStatus.IsConversion)
            {
                return;
            }

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        this.ProcessTabKey(!e.Shift);
                        e.Handled = true;
                        break;
                    case Keys.Tab:
                        this.ProcessTabKey(!e.Shift);
                        e.Handled = true;
                        break;
                    default:    
                        // NOTHING
                        break;
                }
                
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー押下(Up)処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void ControlKeyUp(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            if (this.imeStatus.IsConversion)
            {
                return;
            }

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F9:
                        //表示実行
                        this.logic.bt_ptn9_Click(sender, null);
                        break;
                    case Keys.F12:
                        // 閉じる
                        this.logic.bt_ptn12_Click(sender, null);
                        break;
                    default:
                        // NOTHING
                        break;
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// カーソルヒント
        /// </summary>
        internal void KasoruHento()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.toolTip1 = new ToolTip();
                this.toolTip1.SetToolTip(bt_ptn9, "パターンを登録します");
                this.toolTip1.SetToolTip(bt_ptn12, "画面を閉じます");
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// フリガナ全角チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PATTERN_FURIGANA_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckFurigana(e);
        }

    }
}
