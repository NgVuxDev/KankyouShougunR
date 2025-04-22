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

namespace ShukkinDataShutsuryoku
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        public UIForm()
            : base(WINDOW_ID.T_SHUKKIN_DATA_SHUTSURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ParentBaseForm = (BusinessBaseForm)this.Parent;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this); 
            this.logic.WindowInit();
        }
        #region メソッド
        /// <summary>
        /// 
        /// </summary>
        public void BankPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.logic.Control_Enter(this.BANK_CD, new EventArgs());
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BankShitenPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.logic.Control_Enter(this.BANK_SHITEN_CD, new EventArgs());
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BankPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.logic.BANK_CD_Validated(this.BANK_CD, new EventArgs());
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        public void BankShitenPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.logic.BANK_SHITEN_CD_Validated(this.BANK_SHITEN_CD, new EventArgs());
            LogUtility.DebugMethodEnd();
        }
        #endregion
    }
}
