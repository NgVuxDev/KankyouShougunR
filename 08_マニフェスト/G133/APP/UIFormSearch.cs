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
using r_framework.Intercepter;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo
{
    public partial class UIFormSearch : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private r_framework.Logic.IBuisinessLogic logic;

        public UIFormSearch()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClassSearch(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }
    }
}
