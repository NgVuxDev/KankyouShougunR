// $Id: SelectForm.cs 13309 2014-01-06 05:34:25Z koga $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Dto;
using r_framework.Utility;
using Shougun.Function.ShougunCSCommon.Dto;

namespace ItakuKeiyakuHoshu.APP
{
    public partial class SelectForm : SuperPopupForm
    {

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private SelectLogic logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectForm()
        {
            LogUtility.DebugMethodStart();
            try
            {
                CommonShogunData.Create(SystemProperty.Shain.CD);

                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new SelectLogic(this);
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
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void ControlKeyUp(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

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
                this.toolTip1.SetToolTip(bt_ptn9, "委託契約書入力画面を表示します");
                this.toolTip1.SetToolTip(bt_ptn12, "画面を閉じます");
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

    }
}
