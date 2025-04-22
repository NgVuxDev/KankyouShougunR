using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ChouhyouPatternPopup.Controls
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

            this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>();
            this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>();
            this.SHUUKEI_KOUMOKU_3.DataSource = new List<S_LIST_COLUMN_SELECT>();
            this.SHUUKEI_KOUMOKU_4.DataSource = new List<S_LIST_COLUMN_SELECT>();

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

            //VAN 20200326 #134974, #134977 S
            //売上集計表画面、支払集計表画面の場合
            if (this.windowId == WINDOW_ID.T_URIAGE_SHUUKEIHYOU || this.windowId == WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU)
            {
                this.pnlMeisaiItem.Visible = true;
            }
            //VAN 20200326 #134974, #134977 E

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
                this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_KOUMOKU_3.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_KOUMOKU_4.DataSource = new List<S_LIST_COLUMN_SELECT>();
                this.SHUUKEI_FLAG_1.Checked = false;
                this.SHUUKEI_FLAG_2.Checked = false;
                this.SHUUKEI_FLAG_3.Checked = false;
                this.SHUUKEI_FLAG_4.Checked = false;

                //VAN 20200326 #134974, #134977 S
                this.HINMEI_DISP_FLG.Checked = false;
                this.NET_JUURYOU_DISP_FLG.Checked = false;
                this.SUURYOU_UNIT_DISP_FLG.Checked = false;
                //VAN 20200326 #134974, #134977 E
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
                var select3 = selectedItem.GetColumnSelect(3);
                if (select3 == null)
                {
                    select3 = new S_LIST_COLUMN_SELECT();
                }
                var select4 = selectedItem.GetColumnSelect(4);
                if (select4 == null)
                {
                    select4 = new S_LIST_COLUMN_SELECT();
                }
                this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>() { select1 };
                this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>() { select2 };
                this.SHUUKEI_KOUMOKU_3.DataSource = new List<S_LIST_COLUMN_SELECT>() { select3 };
                this.SHUUKEI_KOUMOKU_4.DataSource = new List<S_LIST_COLUMN_SELECT>() { select4 };
                this.SHUUKEI_FLAG_1.Checked = selectedItem.GetShuukeiFlag(1);
                this.SHUUKEI_FLAG_2.Checked = selectedItem.GetShuukeiFlag(2);
                this.SHUUKEI_FLAG_3.Checked = selectedItem.GetShuukeiFlag(3);
                this.SHUUKEI_FLAG_4.Checked = selectedItem.GetShuukeiFlag(4);

                //VAN 20200326 #134974, #134977 S
                this.HINMEI_DISP_FLG.Checked = selectedItem.Pattern.HINMEI_DISP_KBN.IsTrue;
                this.NET_JUURYOU_DISP_FLG.Checked = selectedItem.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue;
                this.SUURYOU_UNIT_DISP_FLG.Checked = selectedItem.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue;
                //VAN 20200326 #134974, #134977 E
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
        /// 集計FLGのチェックボックスを切り替える
        /// </summary>
        /// <param name="value"></param>
        public void ChangeVisibleShuukeiFlg(bool value)
        {
            this.SHUUKEI_FLAG_1.Visible = value;
            this.SHUUKEI_FLAG_2.Visible = value;
            this.SHUUKEI_FLAG_3.Visible = value;
            this.SHUUKEI_FLAG_4.Visible = value;
        }

        /// <summary>
        /// 項目のクリア
        /// </summary>
        public void ClearKoumoku()
        {
            LogUtility.DebugMethodStart();

            this.SHUUKEI_KOUMOKU_1.SelectedIndex = -1;
            this.SHUUKEI_KOUMOKU_2.SelectedIndex = -1;
            this.SHUUKEI_KOUMOKU_3.SelectedIndex = -1;
            this.SHUUKEI_KOUMOKU_4.SelectedIndex = -1;

            this.SHUUKEI_FLAG_1.Checked = false;
            this.SHUUKEI_FLAG_2.Checked = false;
            this.SHUUKEI_FLAG_3.Checked = false;
            this.SHUUKEI_FLAG_4.Checked = false;

            //VAN 20200326 #134974, #134977 S
            this.HINMEI_DISP_FLG.Checked = false;
            this.NET_JUURYOU_DISP_FLG.Checked = false;
            this.SUURYOU_UNIT_DISP_FLG.Checked = false;
            //VAN 20200326 #134974, #134977 E

            LogUtility.DebugMethodEnd();
        }
    }
}
