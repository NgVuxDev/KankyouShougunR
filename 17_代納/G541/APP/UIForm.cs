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

namespace Shougun.Core.PayByProxy.DainoMeisaihyo
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private DainoMeisaihyoLogic logic;

        UIHeader header_new;

        public UIForm(UIHeader header)
            : base(WINDOW_ID.T_DAINO_MEISAIHYOU, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();
            this.header_new = header;
            this.logic = new DainoMeisaihyoLogic(this);

        }
        //public UIForm()
        //    : base(WINDOW_ID.T_DAINO_MEISAIHYOU, WINDOW_TYPE.NONE)
        //{
        //    this.InitializeComponent();
        //    //this.header_new = header;
        //    this.logic = new UketsukeMeisaihyoLogic(this);

        //}
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
