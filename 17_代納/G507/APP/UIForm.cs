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
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Logic;
using Seasar.Quill.Attrs;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP
{
    /// <summary>
    /// G507：代納伝票発行
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 内部変数

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        #endregion

        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        //internal BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeader HeaderForm { get; private set; }

        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BasePopForm ParentPopForm { get; private set; }

        /// <summary>
        /// 代納No
        /// </summary>
        internal long DainoNo { get; set; }

        /// <summary>
        /// 伝票エンティティ
        /// </summary>
        public ParameterDTOClass ParameterDTO_IN { get; private set; }

        /// <summary>
        /// 伝票エンティティ戻る
        /// </summary>
        public ParameterDTOClass ParameterDTO { get; internal set; }

        //変更前税計算区分、税区分情報
        internal string tempSeiZeikeisanKBN = string.Empty;
        internal string tempSeiZeiKBN = string.Empty;
        internal string tempShiZeikeisanKBN = string.Empty;
        internal string tempShiZeiKBN = string.Empty;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面ID</param>
        /// <param name="dainoNo">代納番号</param>
        public UIForm(WINDOW_TYPE windowType, long dainoNo)
            : base(WINDOW_ID.T_DAINO_DENPYO_HAKKOU, windowType)
        {
            this.InitializeComponent();

            if (!this.DesignMode)
            {
                LogUtility.DebugMethodStart(windowType, dainoNo);

                // 代納No格納
                this.DainoNo = dainoNo;

                // ロジッククラス生成
                if (this.logic != null)
                {
                    this.logic.Dispose();
                    this.logic = null;
                }
                this.logic = new LogicClass(this);

                LogUtility.DebugMethodEnd(windowType, dainoNo);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dainoNo">代納番号</param>
        public UIForm(ParameterDTOClass ParameterDTO)
            : base(WINDOW_ID.T_DAINO_DENPYO_HAKKOU, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            LogUtility.DebugMethodStart(ParameterDTO);

            //伝票エンティティ
            this.ParameterDTO_IN = ParameterDTO;

            // ロジッククラス生成
            if (this.logic != null)
            {
                this.logic.Dispose();
                this.logic = null;
            }
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region override

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

                if (!this.DesignMode)
                {
                    this.ParentPopForm = (BasePopForm)this.Parent;

                    // 画面初期化
                    this.logic.WindowInit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
            }
        }

        #endregion

        internal void SaveOldZei()
        {
            //出荷売上取引
            tempSeiZeikeisanKBN = this.numtxt_ShukkaSeikyuZeiKeisanKbn.Text;
            tempSeiZeiKBN = this.numtxt_ShukkaSeikyuZeiKbn.Text;
            //受入支払取引
            tempShiZeikeisanKBN = this.numtxt_UkeireSeikyuZeiKeisanKbn.Text;
            tempShiZeiKBN = this.numtxt_UkeireSeikyuZeiKbn.Text;
        }
    }
}
