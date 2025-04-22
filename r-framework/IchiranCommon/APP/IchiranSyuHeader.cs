using System;
using r_framework.APP.Base;
using Shougun.Core.Common.IchiranCommon.Logic;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    /// <summary>
    /// ヘッダーベースクラス
    /// </summary>
    public partial class IchiranSyuHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private IchiranSyuLogic logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranSyuHeader()
            :base()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        //画面ロード
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}