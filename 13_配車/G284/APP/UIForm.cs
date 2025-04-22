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
using Shougun.Core.Allocation.MobileShougunShutsuryoku;

namespace Shougun.Core.Allocation.MobileShougunShutsuryoku.APP
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private MobileShougunShutsuryokuLogic mobileShougunShutsuryokuLogic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public UIForm()
            //コンストラクタ
            : base(WINDOW_ID.T_MOBILE_SHOUGUN_SHUTSURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.mobileShougunShutsuryokuLogic = new MobileShougunShutsuryokuLogic(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            this.mobileShougunShutsuryokuLogic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }


    }
}