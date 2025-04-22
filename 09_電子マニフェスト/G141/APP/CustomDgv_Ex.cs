using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using r_framework.CustomControl;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// 継承したデータグリッドビーユー
    /// </summary>
    public partial class CustomDgv_Ex : CustomDataGridView
    {
        //property
        private bool isRowChanged = false;
        /// <summary>
        /// セル選択変更時、行の変更判断フラグ
        /// </summary>
        public bool IsRowChanged 
        {
            get { return isRowChanged; }
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomDgv_Ex()
        {
            InitializeComponent();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public CustomDgv_Ex(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

      

    }



}
