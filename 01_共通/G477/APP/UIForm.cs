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
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Dto;

namespace Shougun.Core.Common.TruckScaleTsuushin
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.C_TRUCKSCALE_TSUUSHINSETTEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BasePopForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic = new LogicClass(this);
                this.logic.WindowInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Shownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Form_Shown();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnUse2_Click(object sender, EventArgs e)
        {
            txtUse.Text = radbtnUse1.Checked ? "1" : "2";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUse_TextChanged(object sender, EventArgs e)
        {
            (txtUse.Text == "1" ? radbtnUse1 : radbtnUse2).Checked = true;
        }

        /// <summary>
        /// 参照ボタンがクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベントの引数</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "開くファイルを選択してください";
            var initialPath = @"C:\Temp";
            var windowHandle = this.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            if (false == String.IsNullOrEmpty(filePath))
            {
                txtFilePath.Text = filePath;
            }
        }
    }
}
