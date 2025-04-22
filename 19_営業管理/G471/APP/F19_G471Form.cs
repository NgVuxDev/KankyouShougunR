using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.Const.Common;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku;



namespace Shougun.Core.BusinessManagement.Mitumorisyo
{
    public partial class G471Form : SuperPopupForm
    {

        /// <summary>
        /// パラメータ用Dto
        /// </summary>
        /// 
        internal FormShowParamDao dto;
       
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G471Logic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public G471Form()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G471Logic(this);
                this.dto = new FormShowParamDao();
            }
            catch
            {
                throw;
            }
            
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prmDto">DTO</param>
        public G471Form(FormShowParamDao prmDto)
        {
            LogUtility.DebugMethodStart(prmDto);
            try
            {
                this.InitializeComponent();
                this.dto = prmDto;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G471Logic(this);
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
                bool catchErr = this.logic.WindowInit();
                if (catchErr)
                {
                    return;
                }
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
             LogUtility.DebugMethodStart(sender,e);

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        //カーソル移動
                        this.logic.Enter_Click(sender, null);
                        
                        break;
                    case Keys.F5:
                        //表示実行
                        this.logic.bt_ptn5_Click(sender, null);
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
        internal bool KasoruHento()
        {
            LogUtility.DebugMethodStart();
            try
            {
                this.toolTip1 = new ToolTip();
                this.toolTip1.SetToolTip(CUSOMLISTBOX_MITUMORISYO, "見積書を選択してください");
                this.toolTip1.SetToolTip(bt_ptn5, "見積入力画面を表示します");
                this.toolTip1.SetToolTip(bt_ptn12, "画面を閉じます");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("KasoruHento", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// リストのダブルクリック
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void CUSOMLISTBOX_MITUMORISYO_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            try
            {
                //G276フォーム起動
                this.logic.bt_ptn5_Click(sender, e);
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

       

    }
}
