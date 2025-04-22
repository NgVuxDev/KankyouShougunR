// $Id: UIForm.cs 24958 2014-07-08 06:41:18Z nagata $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.Logic;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.APP
{
    /// <summary>
    /// 社内経路入力（電子）画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 社内経路入力（電子）画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 社内経路名CDのBackUp
        /// </summary>
        private string denshiKeiyakuShanaiKeiroNameCdBkup;

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            // Componentの初期化
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 初期化
            this.denshiKeiyakuShanaiKeiroNameCdBkup = string.Empty;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion コンストラクタ

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // 親のLoad
            base.OnLoad(e);

            // 画面初期化
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            base.OnShown(e);
        }

        #region FunctionKeyイベント
        /// <summary>
        /// 行挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void AddRow(object sender, EventArgs e)
        {
            if (this.Ichiran.CurrentRow == null)
            {
                return;
            }

            this.logic.AddRow(this.Ichiran.CurrentRow.Index);
        }

        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DeleteRow(object sender, EventArgs e)
        {
            if (this.Ichiran.CurrentRow == null || this.Ichiran.CurrentRow.IsNewRow)
            {
                return;
            }

            this.logic.DeleteRow(this.Ichiran.CurrentRow.Index);
        }

        /// <summary>
        /// 物理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void LogicalDelete(object sender, EventArgs e)
        {
            // 登録時エラーが発生していない場合
            if (false == base.RegistErrorFlag)
            {
                // 削除処理実行
                this.logic.PhysicalDelete();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CSVOutput(object sender, EventArgs e)
        {
            // CSV出力実行
            this.logic.CSVOutput();
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ClearCondition(object sender, EventArgs e)
        {
            // 条件クリア処理実行
            this.logic.InitCondition();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Search(object sender, EventArgs e)
        {
            this.Ichiran.AllowUserToAddRows = true;
            // 検索処理実行
            this.logic.Search();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Regist(object sender, EventArgs e)
        {
            // 登録時エラーが発生していない場合
            if (false == base.RegistErrorFlag)
            {
                // 登録処理実行
                this.logic.Regist();
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Cancel(object sender, EventArgs e)
        {
            // 取り消し処理実行
            this.logic.Cancel();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            // 条件保存
            this.logic.SetPropertiesSettings();

            // 画面Close
            var parentForm = (MasterBaseForm)this.Parent;
            parentForm.Close();
        }

        #endregion FunctionKeyイベント

        #region その他イベント
        /// <summary>
        /// 検索条件選択ポップアップ表示後処理
        /// </summary>
        public void CONDITION_ITEM_PopupAfterExecuteMethod()
        {
            // 検索条件値クリア
            this.CONDITION_VALUE.Text = string.Empty;
        }

        /// <summary>
        /// 条件選択Enter時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            // Imeモードのセット
            this.logic.SetConditionValueImeMode();
        }

        /// <summary>
        /// 一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.CellEnter(e.RowIndex, e.ColumnIndex);
        }

        /// <summary>
        /// 一覧CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.logic.HasErrorCellValidating(e.RowIndex, e.ColumnIndex))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 社内経路名CDEnter時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD_Enter(object sender, EventArgs e)
        {
            var ctrl = (CustomAlphaNumTextBox)sender;

            // 前回値を保存
            this.denshiKeiyakuShanaiKeiroNameCdBkup = ctrl.Text;
        }

        /// <summary>
        /// 社内経路名CDValidating時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD_Validating(object sender, EventArgs e)
        {
            var ctrl = (CustomAlphaNumTextBox)sender;

            if (false == this.denshiKeiyakuShanaiKeiroNameCdBkup.Equals(ctrl.Text))
            {
                // 値が更新された場合、一覧をクリア
                this.logic.IchiranClear();

                // F4,6,9を非活性
                this.logic.SetFunctionButtonEnabled(false);

                // 明細操作不可
                this.logic.SetIchiranEnavled(false);
            }
        }

        /// <summary>
        /// 社内経路名CD検索ボタン - PopupBeforeExecuteMethod
        /// </summary>
        public void popBtn_Bpzkhm_PopupBeforeExecuteMethod()
        {
            // 前回値を保存
            this.denshiKeiyakuShanaiKeiroNameCdBkup = DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text;
        }

        /// <summary>
        /// 社内経路名CD検索ボタン - PopupAfterExecuteMethod
        /// </summary>
        public void popBtn_Bpzkhm_PopupAfterExecuteMethod()
        {
            if (!this.denshiKeiyakuShanaiKeiroNameCdBkup.Equals(DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
            {
                // 値が更新された場合、一覧をクリア
                this.logic.IchiranClear();

                // F4,6,9を非活性
                this.logic.SetFunctionButtonEnabled(false);

                // 明細操作不可
                this.logic.SetIchiranEnavled(false);
            }
        }

        #region 社員検索用DataSourceセット
        /// <summary>
        /// 社員検索ポップアップ用のDataSourceをセットする
        /// </summary>
        public void SetShainPopupProperty()
        {
            // 部署CDで絞込かつ、ログインIDが設定されている社員情報を表示させるため、
            // ポップアップに表示する内容を動的に作成する
            this.logic.SetShainPopupProperty();

        }
        #endregion

        #endregion その他イベント
    }
}
