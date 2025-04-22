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
using Seasar.Quill.Attrs;
using r_framework.Utility;
using Seasar.Quill;
using r_framework.CustomControl;
using Microsoft.VisualBasic;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran
{
    /// <summary>
    /// G511 交付等状況報告書一覧
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        /// <summary>
        /// 一覧GRID：X＝4
        /// </summary>
        private int mIchiranLocationX = 4;
        /// <summary>
        /// 一覧GRID：Y＝214
        /// </summary>
        private int mIchiranLocationY = 214;

        MessageBoxShowLogic myMessageBox = new MessageBoxShowLogic();

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(Dictionary<string, object> SearchResult)
            : base(WINDOW_ID.T_KOUHUJYOKYO_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart(SearchResult);
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                //検索データ取得
                this.logic.ReportInfoList = SearchResult;
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic.WindowInit();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customPanel3 != null)
                {
                    this.customPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.IchiranGRD != null)
                {
                    this.IchiranGRD.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
        	
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        #region Functionボタン 押下処理

        #region [F1] 番号「前」
        /// <summary>
        /// 番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void previousNumber_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 前の番号を取得
                this.logic.GetPreviousNumber();
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

        #region [F2] 番号「次」
        /// <summary>
        /// 番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void nextNumber_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 次の番号を取得
                this.logic.GetNextNumber();
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

        #region [F4]個別印刷
        /// <summary>
        /// [F4]個別印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.Print();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw new r_framework.OriginalException.EdisonException();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region [F5]全印刷
        /// <summary>
        /// [F5]全印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.AllPrint();
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

        // 20140709 chinchisi EV004844_交付状況報告書にてCSV出力機能がない start
        #region [F6]CSV出力
        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.IchiranGRD, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_KOUHUJYOKYO_ICHIRAN), this);
        }
        #endregion
        // 20140710 chinchisi EV004844_交付状況報告書にてCSV出力機能がない end

        #region [F8]検索
        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //// 画面表示
                //this.logic.Search();

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

    }
}
