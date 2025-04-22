using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.FormManager;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.DenpyouhimozukePatternIchiran.Const;
using System.Data;

namespace Shougun.Core.Common.DenpyouhimozukePatternIchiran
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls Logic;
        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }
        /// <summary>
        /// DGVDetailチェック
        /// </summary>
        public DataTable dtDetailList = new DataTable();

        #endregion

        #region メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="paramIn_ShaInCd"></param>
        /// <param name="paramIn_DenshuKb"></param>
        public UIForm(String paramIn_ShaInCd)
            : base(WINDOW_ID.C_DENPYOUHIMOZUKE_PATTERN_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart(WINDOW_ID.C_DENPYOUHIMOZUKE_PATTERN_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            try
            {
                this.InitializeComponent();

                //パラメータ設定
                Properties.Settings.Default.ParamIn_ShaInCd = paramIn_ShaInCd;
                //Properties.Settings.Default.ParamIn_DenshuKb = paramIn_DenshuKb;
                Properties.Settings.Default.Save();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.Logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 明細検索処理
        /// <summary>
        /// 明細検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Logic.Search();
            this.Logic.SetIchiran();
        }
        #endregion

        #region 画面読込処理
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            try
            {
                base.OnLoad(e);
                this.Logic.WindowInit();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Fromクローズ処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                var ParentForm = (BusinessBaseForm)this.Parent;

                //検索条件リセット
                this.Logic.ClearCondition();

                //画面クローズ
                this.Close();
                ParentForm.Close();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 適用処理
        /// <summary>
        /// 適用処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormTekiyou(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //リターン値を取得
                this.Logic.BottonTekiyou();

                //画面クローズ
                this.Logic.ClearCondition();
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 取り消し
        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.Logic.Cancel();
                Search(sender, e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.Logic.BottonDelete();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 登録/更新処理
        /// <summary>
        /// 登録/更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.Logic.UpdRegist();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 新規処理
        /// <summary>
        /// 新規処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NewAdd(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //String sysId = this.dgvDenpyouhimozuke.Rows[this.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString(); ;
                //FormManager.OpenForm("G480", sysId);
                FormManager.OpenForm("G480");
                //String sysId = this.dgvDenpyouhimozuke.Rows[this.dgvDenpyouhimozuke.CurrentCell.RowIndex].Cells["SYSTEM_ID_MOP"].Value.ToString(); ;
                //object[] args = new object[] { DENSHU_KBN.DENPYOU_HIMODUKE_ICHIRAN, sysId };
                //FormManager.OpenForm("G449", args);

                ////伝種区分
                //String denSyuKb = Properties.Settings.Default.ParamIn_DenshuKb;
                ////画面呼び出す
                //var callForm = new Shougun.Core.Common.IchiranSyu.UIForm(denSyuKb, "");
                //var callHeader = new Shougun.Core.Common.IchiranSyu.UIHeader();
                //var popForm = new BasePopForm(callForm, callHeader);
                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    //ポップアップ
                //    popForm.ShowDialog();
                //    //再検索
                //    this.Logic.Search();
                //    this.Logic.SetIchiran();
                //}
                //LogUtility.DebugMethodEnd();
                //return;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region デフォルトチェックボックスのクリック制御
        /// <summary>
        /// 一覧明細中のディフォルトチェックボックスのクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatternIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            try
            {
                this.Logic.PatternIchiran_CellContentClick(sender, e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ダブルクリック制御
        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatternIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);
            try
            {
                this.Logic.PatternIchiran_CellDoubleClick(sender, e);
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region データグリッド重複チェック
        /// <summary>
        /// データグリッド重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDenpyouhimozuke_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);
                //if (!DBNull.Value.Equals(this.dgvDenpyouhimozuke.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                //!"".Equals(this.dgvDenpyouhimozuke.Rows[e.RowIndex].Cells["CONTENA_SHURUI_CD"].Value) &&
                //this.dgvDenpyouhimozuke.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.DispNumber))
                //{
                //    string pattern_DispNumber = (string)this.dgvDenpyouhimozuke.Rows[e.RowIndex].Cells[ConstCls.DispNumber].Value;

                //    bool isError = this.Logic.DuplicationCheck(pattern_DispNumber, dtDetailList);

                //    if (isError)
                //    {
                //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //        msgLogic.MessageBoxShow("E022", "表示順序");
                //        e.Cancel = true;
                //        this.dgvDenpyouhimozuke.BeginEdit(false);
                //        return;
                //    }
                //}
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion
    }
}
