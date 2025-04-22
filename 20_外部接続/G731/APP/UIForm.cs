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
using r_framework.Dto;
using Shougun.Core.ExternalConnection.FileUploadIchiran.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.ExternalConnection.FileUploadIchiran.APP
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_FILE_UPLOAD_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Logic.WindowInit();

            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        #region ファンクションボタン
        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Delete();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F5 プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Preview();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //一覧に明細行がない場合
                if (this.Ichiran.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    this.Logic.msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (this.Logic.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport csvExport = new CSVExport();
                        csvExport.ConvertCustomDataGridViewToCsv(this.Ichiran, true, true, DENSHU_KBN.FILE_UPLOAD_ICHIRAN.ToTitleString(), this);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Clear();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Search();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F9 ダウンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.DownLoad();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customSearchHeader1.ShowCustomSearchSettingDialog();

            this.Logic.RefreshHeaderForm();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            base.CloseTopForm();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 登録日付TOダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CREATE_DATE_TO_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CREATE_DATE_TO.Value = CREATE_DATE_FROM.Value;
        }

        /// <summary>
        /// 一覧セルフォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.Ichiran.Columns["FILE_LENGTH"].Index)
            {
                // ファイルサイズを[#,##0.000]形式に表示
                var val = this.Ichiran["FILE_LENGTH", e.RowIndex].Value;
                Decimal dec;
                if (val is DBNull || !Decimal.TryParse(val.ToString(), out dec))
                {
                    e.Value = string.Empty;
                }
                else
                {
                    e.Value = dec.ToString("#,##0.000");
                }
            }
        }
    }
}
