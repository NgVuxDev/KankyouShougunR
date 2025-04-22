using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Seasar.Quill;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// アウトパラメータ
        /// </summary>
        public Boolean ParamOut_Continue { get; set; }
                
        /// <summary>
        /// ヘッダー
        /// </summary>
        UIHeader headerForm;

        /// <summary>
        /// 拠点ＣＤ
        /// </summary>
        internal string kyotenCd;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <param name="headerForm">ヘッダー</param>
        public UIForm(DataTable dt, UIHeader headerForm, string kyotenCd)
            : base(WINDOW_ID.T_SHIHARAI_SHIME_ERROR, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            this.logic = new LogicClass(this);
            this.kyotenCd = kyotenCd;
            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            // 重複チェック&整形：伝票締め時に入出金が重複する場合があるため重複項目を取り除く
            DataTable formatedTable = this.logic.CheckAndFormat(dt);

            this.logic.SetIchiran(formatedTable);

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
            this.logic.WindowInit();
        }
    }
}
