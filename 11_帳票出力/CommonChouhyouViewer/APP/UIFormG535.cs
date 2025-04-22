// $Id: UIFormG535.cs 10982 2013-12-13 06:35:03Z sys_dev_43 $

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    /// <summary>
    /// ○○○前年対比表/一覧
    /// </summary>
    public partial class UIFormG535 : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G535_LogicClass logic;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        UIHeaderForm header_new;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">headerForm</param>
        public UIFormG535(UIHeaderForm headerForm, WINDOW_ID windowID)
            : base(windowID)
        {
            try
            {
                LogUtility.DebugMethodStart(headerForm, windowID);

                this.InitializeComponent();

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G535_LogicClass(this);
                this.logic.headerForm = this.header_new;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region - Properties -

        /// <summary>帳票情報を保持するプロパティ</summary>
        public ReportInfoBase ReportInfo { get; set; }

        #endregion - Properties -

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                // タイトル設定
                this.header_new.lb_title.Text += "／一覧";

                // タイトル設定
                this.Parent.Text += "／一覧";

                // 初期表示
                this.logic.WindowInit();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// [F5]印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //// 印刷ポップ画面表示
                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.ReportInfo, this.WindowId))
                {
                    formReportPrintPopup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F6]CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CsvOutput();

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
        }

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.header_new));
                //allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

                return allControl.ToArray();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion
    }
}
