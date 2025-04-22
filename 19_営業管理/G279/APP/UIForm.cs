using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        #region Field

        /// <summary>ヘッダーフォーム</summary>
        private UIHeader headerForm;

        /// <summary>ロジッククラス</summary>
        private DenshiShinseiNyuuryoku.LogicClass logic;

        /// <summary>
        /// 前回値 - 申請経路CD
        /// </summary>
        internal string beforeShinseiKeiroCd = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="type"></param>
        /// <param name="sysId">T_DENSHI_SHINSEI_ENTRY.SYSTEM_ID</param>
        /// <param name="seq">T_DENSHI_SHINSEI_ENTRY.SEQ</param>
        /// <param name="initDto">初期化用Dto</param>
        /// <param name="entry">新規登録用Entry</param>
        /// <param name="detailList">新規登録用Detail</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE type, long sysId, int seq
            , DenshiShinseiNyuuryokuInitDTO initDto, T_DENSHI_SHINSEI_ENTRY entry, List<T_DENSHI_SHINSEI_DETAIL> detailList)
            : base(WINDOW_ID.T_DENSHI_SHINSEI_NYUURYOKU, type)
        {
            InitializeComponent();

            this.logic = new LogicClass(headerForm, this);
            this.logic.SysId = sysId;
            this.logic.Seq = seq;
            this.logic.InitDto = initDto;
            this.logic.DenshiShinseiEntry = entry;
            this.logic.DenshiShinseiDetailList = detailList;
            this.headerForm = headerForm;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region Event

        /// <summary>
        /// 画面Loadイベント
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);

            this.logic.WindowInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// DETAIL - RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex <= 0)
            {
                return;
            }

            if (this.DETAIL.Rows[e.RowIndex].IsNewRow)
            {
                // No列設定
                this.DETAIL.Rows[e.RowIndex - 1].Cells["No"].Value = e.RowIndex;
            }
        }

        /// <summary>
        /// DETAIL - CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            /* IME制御 */
            this.logic.ChangeDetailImeMode(e.ColumnIndex);

            this.logic.CellEnter(e.RowIndex, e.ColumnIndex);
        }

        /// <summary>
        /// DETAIL - CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool catchErr = false;
            if (!this.logic.CellValueValidating(e.RowIndex, e.ColumnIndex, out catchErr))
            {
                if (catchErr)
                {
                    return;
                }
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 申請経路CD - Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHINSEI_KEIRO_CD_Enter(object sender, EventArgs e)
        {
            if (!this.logic.isInputError)
            {
                // マスタ存在チェックにてエラーになった場合に
                // エラーメッセージのポップアップから戻ってくるとEnterイベントが発生してします。
                // isInputErrorフラグを利用して、上記の場合に前回値に保存しないようにする。
                beforeShinseiKeiroCd = this.SHINSEI_KEIRO_CD.Text;
            }
        }

        /// <summary>
        /// 申請経路CD - Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHINSEI_KEIRO_CD_Validated(object sender, EventArgs e)
        {
            if (beforeShinseiKeiroCd.Equals(this.SHINSEI_KEIRO_CD.Text)) return;

            this.logic.RouteDataSet(this.SHINSEI_KEIRO_CD.Text);
        }

        /// <summary>
        /// コメント - Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHINSEISHA_COMMENT_Validated(object sender, EventArgs e)
        {
            // フォーカスアウト時にキャレット位置を先頭にして表示する
            this.SHINSEISHA_COMMENT.Select(0, 0);
            this.SHINSEISHA_COMMENT.ScrollToCaret();
        }

        /// <summary>
        /// MainForm - OnKeyDown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            /* 改行可能項目にてCtrl+Enterで次の項目にフォーカスが移動しないようにする */

            var act = ControlUtility.GetActiveControl(this);
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                var textBox = act as TextBox;
                if (textBox != null && textBox.Multiline)
                {
                    //TextBoxかつMultiline時のみ改行
                    e.Handled = true;
                    return;
                }
            }

            // 該当しないコントロールは通常通りフォーカス移動
            base.OnKeyDown(e);
        }
        
        #endregion

        #region Utility

        internal Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(this.headerForm));
            allControl.AddRange(controlUtil.GetAllControls(this.Parent));

            return allControl.ToArray();
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

        #region 部署CD、部署名取得
        /// <summary>
        /// 社員CDから関連する部署CD、部署名を取得し明細行にセットする
        /// </summary>
        public void GetBushoData()
        {
            this.logic.GetBushoData();
        }
        #endregion

        #endregion
    }
}
