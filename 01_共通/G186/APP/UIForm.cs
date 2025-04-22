using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Common.PatternIchiran
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

        //2013.12.15 naitou upd start
        /// <summary>
        /// 更新フラグ
        /// </summary>
        public bool ParamOut_UpdateFlag { get; set; }
        //2013.12.15 naitou upd end

        /// <summary>
        /// 伝種区分
        /// </summary>
        public string DenshuKbn { get; set; }

        #endregion

        #region メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="paramIn_ShaInCd"></param>
        /// <param name="paramIn_DenshuKb"></param>
        public UIForm(String paramIn_ShaInCd, string paramIn_DenshuKb)
            : base(WINDOW_ID.C_PATTERN_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            //パラメータ設定
            Properties.Settings.Default.ParamIn_ShaInCd = paramIn_ShaInCd;
            Properties.Settings.Default.ParamIn_DenshuKb = paramIn_DenshuKb;
            Properties.Settings.Default.Save();

            //2013.12.15 naitou upd start
            this.ParamOut_SysID = "";
            this.ParamOut_UpdateFlag = false;
            //2013.12.15 naitou upd end

            this.DenshuKbn = paramIn_DenshuKb;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Logic.WindowInit();
            
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.Logic.Cancel();
            Search(sender, e);
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 一覧明細中のディフォルトチェックボックスのクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatternIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Logic.PatternIchiran_CellContentClick(sender, e);
        }

        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatternIchiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //2013-11-19 Upd ogawamut PT 東北 No.
            //this.Logic.PatternIchiran_CellDoubleClick(sender, e);
            this.Logic.PatternIchiran_CellDoubleClick();
        }

        /// <summary>
        /// [F1]適用処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormTekiyou(object sender, EventArgs e)
        {
            //リターン値を取得
            this.Logic.BottonTekiyou();

            //画面クローズ
            this.Logic.ClearCondition();
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// [F2]新規処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NewAdd(object sender, EventArgs e)
        {
            //伝種区分
            String denSyuKb = Properties.Settings.Default.ParamIn_DenshuKb;
            //画面呼び出す
            var callForm = new Shougun.Core.Common.IchiranSyu.UIForm(denSyuKb, "");
            var callHeader = new Shougun.Core.Common.IchiranSyu.UIHeader();
            var popForm = new BasePopForm(callForm, callHeader);
            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                //ポップアップ
                popForm.ShowDialog();
            }
            //2013-11-19 Upd ogawamut PT 東北 No.1203
            //FormManager.OpenForm("G187", denSyuKb, "");

            //再検索
            this.Logic.Search();
            this.Logic.SetIchiran();

            return;
        }

        //2013-11-19 Add ogawamut PT 東北 No.1203
        /// <summary>
        /// [F3]修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Update(object sender, EventArgs e)
        {
            this.Logic.PatternIchiran_CellDoubleClick();
        }

        /// <summary>
        /// [F4]論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            this.Logic.BottonDelete();
        }

        /// <summary>
        /// [F8]明細検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Logic.Search();
            this.Logic.SetIchiran();
        }

        /// <summary>
        /// [F9]登録/更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            this.Logic.UpdRegist();
        }

        /// <summary>
        /// [F12]Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var ParentForm = (BusinessBaseForm)this.Parent;

            //検索条件リセット
            this.Logic.ClearCondition();

            //画面クローズ
            this.Close();
            ParentForm.Close();
        }


        #endregion
    }
}
