// $Id: UIFormG533.cs 11464 2013-12-17 04:38:46Z sys_dev_23 $

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
    /// 推移表/一覧画面
    /// </summary>
    public partial class UIFormG533 : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G533_LogicClass logic;

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
        public UIFormG533(UIHeaderForm headerForm, WINDOW_ID winID)
            : base(winID)
        {
            try
            {
                LogUtility.DebugMethodStart(headerForm, winID);

                this.InitializeComponent();

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G533_LogicClass(this);
                this.logic.SetHeader(header_new);
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

        #region [F5]印刷
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
        #endregion

        #region [F6]CSV出力
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
                this.logic.CSVOutput();
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
        #endregion

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

        #endregion

    }
}
