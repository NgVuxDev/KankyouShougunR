using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou.Controls
{
    /// <summary>
    /// パターン選択コントロール
    /// </summary>
    public partial class PatternList : UserControl
    {
        /// <summary>
        /// パターンリストをダブルクリックしたときに発生するイベント
        /// </summary>
        [Browsable(true)]
        [Description("パターンリストをダブルクリックしたときに発生するイベントです")]
        public event EventHandler<EventArgs> PatternDoubleClicked;

        /// <summary>
        /// 画面ID
        /// </summary>
        private WINDOW_ID windowId;

        /// <summary>
        /// パターンDTOリスト
        /// </summary>
        private List<PatternDto> patternDtoList;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public PatternList()
        {
            LogUtility.DebugMethodStart();

            InitializeComponent();

            this.SHUTSURYOKU_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>();
            this.SHUTSURYOKU_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力する帳票の画面IDをセットします
        /// </summary>
        /// <param name="windowId">画面ID</param>
        public void SetWindowId(WINDOW_ID windowId)
        {
            LogUtility.DebugMethodStart(windowId);

            this.windowId = windowId;
            this.patternDtoList = new List<PatternDto>();

            // セットされた画面IDで登録されているパターンを取得
            var patternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>();
            var patternColumnDao = DaoInitUtility.GetComponent<IM_LIST_PATTERN_COLUMNDao>();
            var columnSelectDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECTDao>();
            var columnSelectDetailDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECT_DETAILDao>();
            var patternList = patternDao.GetMListPatternList((int)this.windowId);
            foreach (var pattern in patternList)
            {
                var dto = new PatternDto();
                dto.Pattern = pattern;
                dto.PatternColumnList = patternColumnDao.GetMListPatternColumnList(pattern.SYSTEM_ID.Value, pattern.SEQ.Value);
                foreach (var patternColumn in dto.PatternColumnList)
                {
                    dto.ColumnSelectList.Add(columnSelectDao.GetSListColumnSelectList(new S_LIST_COLUMN_SELECT() { WINDOW_ID = patternColumn.WINDOW_ID, KOUMOKU_ID = patternColumn.KOUMOKU_ID }).FirstOrDefault());
                    dto.ColumnSelectDetailList.Add(columnSelectDetailDao.GetSListColumnSelectDetailList(new S_LIST_COLUMN_SELECT_DETAIL() { WINDOW_ID = patternColumn.WINDOW_ID, KOUMOKU_ID = patternColumn.KOUMOKU_ID }).FirstOrDefault());
                }

                this.patternDtoList.Add(dto);
            }

            this.PATTERN_LIST_BOX.DataSource = this.patternDtoList;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 選択されているパターンDTOを取得します
        /// </summary>
        /// <returns>パターンDTO</returns>
        public PatternDto GetSelectedPatternDto()
        {
            LogUtility.DebugMethodStart();

            var ret = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// パターンリストボックスの選択Indexが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void PATTERN_LIST_BOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (PatternDto)this.PATTERN_LIST_BOX.SelectedItem;
            if (selectedItem == null)
            {
                this.SHUTSURYOKU_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUTSURYOKU_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_FLAG_1.Checked = false;
                this.SHUUKEI_FLAG_2.Checked = false;
            }
            else
            {
                var select1 = selectedItem.GetColumnSelect(1);
                if (select1 == null)
                {
                    select1 = new S_LIST_COLUMN_SELECT();
                }
                var select2 = selectedItem.GetColumnSelect(2);
                if (select2 == null)
                {
                    select2 = new S_LIST_COLUMN_SELECT();
                }

                this.SHUTSURYOKU_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>() { select1 };
                this.SHUTSURYOKU_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>() { select2 };

                this.SHUUKEI_FLAG_1.Checked = selectedItem.GetShuukeiFlag(1);
                this.SHUUKEI_FLAG_2.Checked = selectedItem.GetShuukeiFlag(2);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンリストボックスでマウスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void PATTERN_LIST_BOX_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            EventHandler<EventArgs> eventHandler = PatternDoubleClicked;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 項目のクリア
        /// </summary>
        public void ClearKoumoku()
        {
            LogUtility.DebugMethodStart();

            this.SHUTSURYOKU_KOUMOKU_1.SelectedIndex = -1;
            this.SHUTSURYOKU_KOUMOKU_2.SelectedIndex = -1;

            this.SHUUKEI_FLAG_1.Checked = false;
            this.SHUUKEI_FLAG_2.Checked = false;

            LogUtility.DebugMethodEnd();
        }
    }
}
