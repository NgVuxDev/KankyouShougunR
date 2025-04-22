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

namespace Shougun.Core.PayByProxy.DainoSyukeihyo
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        private UIHeader header;
        public UIForm(UIHeader header)
            : base(WINDOW_ID.T_DAINO_SYUUKEIHYOU, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();
            this.header = header;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           this.logic.WindowInit();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.logic.CallPopup();
        }
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
    }
}
