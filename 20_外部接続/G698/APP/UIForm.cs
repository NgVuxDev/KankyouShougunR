using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass logic;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : this(0L)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="systemId">システムID</param>
        public UIForm(long systemId)
            : base(WINDOW_ID.T_COURSE_SAITEKIKA_NYUURYOKU)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.logic.SystemId = systemId;
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            this.customDataGridView2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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


            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント
        #region F4 上
        /// <summary>
        /// 上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カーソルCell
                DataGridViewCell currentCell = this.customDataGridView2.CurrentCell;
                // 選択行がない場合、カーソル行が１行目の場合、何もしない
                if (currentCell == null || currentCell.RowIndex == 0)
                {
                    return;
                }

                this.logic.MoveIchiran(currentCell.RowIndex, true);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F5 下
        /// <summary>
        /// 下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カーソルCell
                DataGridViewCell currentCell = this.customDataGridView2.CurrentCell;
                // 選択行がない場合、何もしない
                if (currentCell == null || currentCell.RowIndex == this.customDataGridView2.Rows.Count - 1)
                {
                    return;
                }

                this.logic.MoveIchiran(currentCell.RowIndex, false);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F7 再取込
        /// <summary>
        /// 再取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 実績情報取得
                this.logic.GetExperience();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F8 順番整列
        /// <summary>
        /// 順番整列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.SortIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F9 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.RegistErrorFlag)
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }

                // チェック処理
                if (this.logic.HasError())
                {
                    return;
                }

                // Entity作成
                this.logic.CreateEntity();
                // 登録処理
                var result = this.logic.RegistData();
                if (!result)
                {
                    return;
                }

                // 案件の実績情報を削除する
                var filePath = this.logic.CreateCSV();
                if (string.IsNullOrEmpty(filePath))
                {
                    // 出力失敗
                    return;
                }

                // API通信処理(削除)「3.配車計画情報一括登録」
                int pid = this.logic.RegistAPI(filePath);
                if (pid == 0)
                {
                    // API通信失敗
                    this.logic.msgLogic.MessageBoxShow("E245");
                    return;
                }

                // 配送計画関連のデータ更新
                result = this.logic.RegistDataNavi();
                if (result)
                {
                    this.logic.msgLogic.MessageBoxShow("I001", "登録連携");
                }
                else
                {
                    return;
                }

                // 画面を閉じる
                base.CloseTopForm();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F11 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.SetInitialForm();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// 閉じる
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

        #region [2]案件削除
        internal void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DeleteMatter();

            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion

        /// <summary>
        /// 一覧セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.customDataGridView1.Columns[ConstCls.BEFORE_KIBOU_TIME].Index)
            {
                // 希望時間を[HH:mm]形式に表示
                var val = this.customDataGridView1[ConstCls.BEFORE_KIBOU_TIME, e.RowIndex].Value;
                DateTime dateTime;
                if (val is DBNull || !DateTime.TryParse(val.ToString(), out dateTime))
                {
                    e.Value = string.Empty;
                }
                else
                {
                    e.Value = dateTime.ToString("HH:mm");
                }
            }
        }

        /// <summary>
        /// 一覧セルEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var columnName = customDataGridView2.Columns[e.ColumnIndex].Name;

            // IME制御
            if (columnName.Equals(ConstCls.AFTER_KIBOU_TIME))
            {
                customDataGridView2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
            else if (columnName.Equals(ConstCls.AFTER_ESTIMATED_ARRIVAL_TIME))
            {
                customDataGridView2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
            else if (columnName.Equals(ConstCls.AFTER_BIKOU))
            {
                customDataGridView2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
        }

        /// <summary>
        /// 表示中の作業者と指定されたシステムIDに紐付く作業者が同一か判定
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>true:同じ, false:違う</returns>
        internal bool IsSameSagyousha(long systemId)
        {
            return this.logic.IsSameSagyousha(systemId);
        }
    }
}
