using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;
using Seasar.Quill;

// 電子契約最新照会のF1から展開

namespace Shougun.Core.ExternalConnection.SmsResult
{
    public partial class SmsResultShoukai : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClassShoukai logic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SmsResultShoukai()
            : base()
        {
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClassShoukai(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic = new LogicClassShoukai(this);

            if (!this.logic.WindowInit()) { return; }
        }

        #endregion

        #region ファンクションベント

        #region F8 状況照会
        /// <summary>
        /// F8 状況照会
        /// </summary>
        internal void Reference(object sender, EventArgs e)
        {
            if (this.logic.SmsJokyoShoukai())
            {
                this.logic.msgLogic.MessageBoxShowInformation("照会が完了しました。");
                
                // 照会して閉じる場合はOKを返す
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region F12 ｷｬﾝｾﾙ

        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, EventArgs e)
        {
            // ｷｬﾝｾﾙで閉じる場合はキャンセルを返す
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #endregion
    }
}