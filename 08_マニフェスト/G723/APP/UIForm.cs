using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.ManifestSuiihyoData
{
    /// <summary>
    /// マニフェスト推移表画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// マニフェスト推移表ロジッククラス
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// パターンDTOリスト
        /// </summary>
        private List<PatternDto> patternDtoList;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
        {
            this.InitializeComponent();

            this.WindowId = WINDOW_ID.T_MANIFEST_SUIIHYOU;

            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.logic.WindowInit()) { return; }
            this.SetWindowId(this.WindowId);
            this.SetDefault();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面の初期値をセットします
        /// </summary>
        private void SetDefault()
        {
            this.KYOTEN_CD.Text = "99";
            this.KYOTEN_NAME.Text = "全社";
            this.ICHIJI_NIJI_KBN.Text = "3";
        }

        /// <summary>
        /// [F1]新規ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var patternDto = new PatternDto();
            patternDto.Pattern.WINDOW_ID = (int)this.WindowId;
            this.ShowTourokuPopup(WINDOW_TYPE.NEW_WINDOW_FLAG, patternDto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F2]修正ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null){
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F4]削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc4_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null)
            {
                this.ShowTourokuPopup(WINDOW_TYPE.DELETE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F5]CSVボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            int searchCount = 0;
            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (dtos == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            // 日付チェック
            if (!this.logic.CheckDate())
            {
                return;
            }

            // 出力区分チェック
            if (!this.KBN_KAMI_MANI.Checked && !this.KBN_DEN_MANI.Checked)
            {
                // チェックボックスが両方ともFlaseの場合は必須チェックエラー
                new MessageBoxShowLogic().MessageBoxShowError("出力区分は必須項目です。設定してください。");
                return;
            }
            else if (dtos != null)
            {
                searchCount = this.logic.Search();
                if (searchCount == 0)
                {
                    new MessageBoxShowLogic().MessageBoxShow("C001");
                    return;
                }
                else if (searchCount == -1)
                {
                    // 検索でエラーの場合は以降何もしない。
                    return;
                }
            }

            if (this.logic.ManifestDataTable != null && 0 < this.logic.ManifestDataTable.Rows.Count)
            {
                // CSVデータ作成
                DataTable retDataTable = this.logic.CreateCSVDataDt(this.logic.ManifestDataTable, this.logic.ManifestSuiihyoDto);

                // CSV出力
                if (new CsvUtility(retDataTable, this, "マニフェスト推移表").Output())
                {
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]表示ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (dtos == null)
            {
                MessageBox.Show("パターンを選択してください。", "アラート",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PATTERN_LIST_BOX.Focus();
                this.PATTERN_LIST_BOX.BackColor = Constans.ERROR_COLOR;
                return;
            }

            if (!this.RegistErrorFlag)
            {
                // 日付チェック
                if (!this.logic.CheckDate())
                {
                    return;
                }

                // 出力区分チェック
                if (!this.KBN_KAMI_MANI.Checked && !this.KBN_DEN_MANI.Checked)
                {
                    // チェックボックスが両方ともFlaseの場合は必須チェックエラー
                    new MessageBoxShowLogic().MessageBoxShowError("出力区分は必須項目です。設定してください。");
                    this.RegistErrorFlag = true;
                    
                }
                else if (dtos != null)
                {
                    var count = this.logic.Search();
                    if (0 < count)
                    {
                        if (!this.logic.CreateManifestSuiihyo()) { return; }
                    }
                    else if (0 == count)
                    {
                        new MessageBoxShowLogic().MessageBoxShow("C001");
                    }
                    else
                    {
                        return;
                    }
                }
            }

            this.RecoveryFocusOutCheckMethod();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
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
                var select5 = selectedItem.GetColumnSelect(5);
                if (select5 == null)
                {
                    select5 = new S_LIST_COLUMN_SELECT();
                }
                var select6 = selectedItem.GetColumnSelect(6);
                if (select6 == null)
                {
                    select6 = new S_LIST_COLUMN_SELECT();
                }
                var select7 = selectedItem.GetColumnSelect(7);
                if (select7 == null)
                {
                    select7 = new S_LIST_COLUMN_SELECT();
                }
                this.SHUUKEI_KOUMOKU_1.DataSource = new List<S_LIST_COLUMN_SELECT>() { select1 };
                this.SHUUKEI_KOUMOKU_2.DataSource = new List<S_LIST_COLUMN_SELECT>() { select2 };
                this.SHUUKEI_KOUMOKU_3.DataSource = new List<S_LIST_COLUMN_SELECT>() { select3 };
                this.SHUUKEI_KOUMOKU_4.DataSource = new List<S_LIST_COLUMN_SELECT>() { select4 };
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターンリストボックスをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PATTERN_LIST_BOX_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var dtos = this.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dtos != null)
            {
                this.KYOTEN_CD.Focus();
                this.ShowTourokuPopup(WINDOW_TYPE.UPDATE_WINDOW_FLAG, dtos);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHUTSU_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeHaishutsuGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHUTSU_GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeHaishutsuGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHOBUN_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShobunGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHOBUN_GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeShobunGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終排出業者CD Fromテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void LAST_SHOBUN_GYOUSHA_CD_FROM_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeLastShobunGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終排出業者CD Toテキストボックスのテキストを変更したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void LAST_SHOBUN_GYOUSHA_CD_TO_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.ChangeLastShobunGenbaCdTextBoxEnabled();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付Toテキストボックスでダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!String.IsNullOrEmpty(this.DATE_FROM.Text))
            {
                this.DATE_TO.Text = this.DATE_FROM.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出事業者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSU_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出事業場 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSU_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分受託者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分事業場 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分業者 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分場所 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 廃棄物種類(報告書分類) Toテキストボックスをマウスダブルクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOUKOKUSHO_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 廃棄物名称 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKIBUTSU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分方法 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_HOUHOU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先 Toテキストボックスをマウスダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            SetToCdAndName(sender);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// パターン登録ポップアップを表示します
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="dto">パターンDTOクラス</param>
        private void ShowTourokuPopup(WINDOW_TYPE windowType, PatternDto dto)
        {
            LogUtility.DebugMethodStart(windowType, dto);

            var popup = new ChouhyouPatternTourokuPopupForm(windowType, dto);
            var dialogResult = popup.ShowDialog();
            popup.Dispose();

            if (DialogResult.Cancel != dialogResult)
            {
                // ポップアップを閉じたらパターンのリストを再読込み
                this.SetWindowId(this.WindowId);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力する帳票の画面IDをセットします
        /// </summary>
        /// <param name="windowId">画面ID</param>
        private void SetWindowId(WINDOW_ID windowId)
        {
            LogUtility.DebugMethodStart(windowId);

            this.patternDtoList = new List<PatternDto>();

            // セットされた画面IDで登録されているパターンを取得
            var patternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>();
            var patternColumnDao = DaoInitUtility.GetComponent<IM_LIST_PATTERN_COLUMNDao>();
            var columnSelectDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECTDao>();
            var columnSelectDetailDao = DaoInitUtility.GetComponent<IS_LIST_COLUMN_SELECT_DETAILDao>();
            var patternList = patternDao.GetMListPatternList((int)windowId);
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
        /// 排出現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeHaishutsuGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            var toCd = this.HAISHUTSU_GYOUSHA_CD_TO.Text;

            var enabled = !String.IsNullOrEmpty(fromCd) &&
                          !String.IsNullOrEmpty(toCd) &&
                          this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd);

            if (!enabled)
            {
                // 非活性時は初期化
                this.HAISHUTSU_GENBA_CD_FROM.Text = String.Empty;
                this.HAISHUTSU_GENBA_CD_TO.Text = String.Empty;
                this.HAISHUTSU_GENBA_NAME_FROM.Text = String.Empty;
                this.HAISHUTSU_GENBA_NAME_TO.Text = String.Empty;
            }

            this.HAISHUTSU_GENBA_CD_FROM.Enabled = enabled;
            this.HAISHUTSU_GENBA_CD_TO.Enabled = enabled;
            this.HAISHUTSU_GENBA_FROM_POPUP.Enabled = enabled;
            this.HAISHUTSU_GENBA_TO_POPUP.Enabled = enabled;
            this.HAISHUTSU_GENBA_NAME_FROM.Enabled = enabled;
            this.HAISHUTSU_GENBA_NAME_TO.Enabled = enabled;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeShobunGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.SHOBUN_GYOUSHA_CD_FROM.Text;
            var toCd = this.SHOBUN_GYOUSHA_CD_TO.Text;
            var enabled = !String.IsNullOrEmpty(fromCd) &&
                          !String.IsNullOrEmpty(toCd) &&
                          this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd);

            if (!enabled)
            {
                // 非活性時は初期化
                this.SHOBUN_GENBA_CD_FROM.Text = String.Empty;
                this.SHOBUN_GENBA_CD_TO.Text = String.Empty;
                this.SHOBUN_GENBA_NAME_FROM.Text = String.Empty;
                this.SHOBUN_GENBA_NAME_TO.Text = String.Empty;
            }

            this.SHOBUN_GENBA_CD_FROM.Enabled = enabled;
            this.SHOBUN_GENBA_CD_TO.Enabled = enabled;
            this.SHOBUN_GENBA_FROM_POPUP.Enabled = enabled;
            this.SHOBUN_GENBA_TO_POPUP.Enabled = enabled;
            this.SHOBUN_GENBA_NAME_FROM.Enabled = enabled;
            this.SHOBUN_GENBA_NAME_TO.Enabled = enabled;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 最終処分現場CDテキストボックスの使用可否を切り替えます
        /// </summary>
        private void ChangeLastShobunGenbaCdTextBoxEnabled()
        {
            LogUtility.DebugMethodStart();

            var fromCd = this.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            var toCd = this.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
            var enabled = !String.IsNullOrEmpty(fromCd) &&
                          !String.IsNullOrEmpty(toCd) &&
                          this.ZeroSuppressGenbaCd(fromCd) == this.ZeroSuppressGenbaCd(toCd);

            if (!enabled)
            {
                // 非活性時は初期化
                this.LAST_SHOBUN_GENBA_CD_FROM.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_CD_TO.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_NAME_FROM.Text = String.Empty;
                this.LAST_SHOBUN_GENBA_NAME_TO.Text = String.Empty;
            }

            this.LAST_SHOBUN_GENBA_CD_FROM.Enabled = enabled;
            this.LAST_SHOBUN_GENBA_CD_TO.Enabled = enabled;
            this.LAST_SHOBUN_GENBA_FROM_POPUP.Enabled = enabled;
            this.LAST_SHOBUN_GENBA_TO_POPUP.Enabled = enabled;
            this.LAST_SHOBUN_GENBA_NAME_FROM.Enabled = enabled;
            this.LAST_SHOBUN_GENBA_NAME_TO.Enabled = enabled;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのゼロ埋め処理を行います
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ゼロ埋めした現場CD</returns>
        private String ZeroSuppressGenbaCd(String genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            var ret = String.Empty;
            if (String.IsNullOrEmpty(genbaCd) == false)
            {
                ret = genbaCd.ToUpper().PadLeft(6, '0');
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// FromテキストボックスからToテキストボックスにCDと名称をコピーします
        /// </summary>
        /// <param name="sender">CdFromテキストボックス</param>
        private void SetToCdAndName(object sender)
        {
            LogUtility.DebugMethodStart(sender);

            var cdToTextBox = (TextBox)sender;
            var cdFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_TO", "_FROM")).First();
            if (!String.IsNullOrEmpty(cdFromTextBox.Text))
            {
                cdToTextBox.Text = cdFromTextBox.Text;

                var nameFromTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_FROM")).First();
                var nameToTextBox = this.allControl.Where(c => ((Control)c).Name == cdToTextBox.Name.Replace("_CD_TO", "_NAME_TO")).First();
                nameToTextBox.Text = nameFromTextBox.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付範囲のFromToチェックを行います
        /// </summary>
        /// <param name="fromTextBox">日付Fromテキストボックス</param>
        /// <param name="toTextBox">日付Toテキストボックス</param>
        /// <returns>エラーがある場合は、True</returns>
        private bool IsErrorDateFromTo(CustomDateTimePicker fromTextBox, CustomDateTimePicker toTextBox)
        {
            LogUtility.DebugMethodStart(fromTextBox, toTextBox);

            var ret = false;

            var fromDate = fromTextBox.Text;
            var toDate = toTextBox.Text;
            if (String.IsNullOrEmpty(fromDate) == false && String.IsNullOrEmpty(toDate) == false)
            {
                if (fromDate.CompareTo(toDate) > 0)
                {
                    fromTextBox.IsInputErrorOccured = true;
                    toTextBox.IsInputErrorOccured = true;
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 登録時のチェックでFromToのチェックが入力エラーになると、FocusOutCheckMethodが動作しなくなる対策
        /// （ValidatorでCausesValidationをfalseにしたままになる不具合）
        /// </summary>
        private void RecoveryFocusOutCheckMethod()
        {
            this.HAISHUTSU_GYOUSHA_CD_FROM.CausesValidation = true;
            this.HAISHUTSU_GYOUSHA_CD_TO.CausesValidation = true;
            this.HAISHUTSU_GENBA_CD_FROM.CausesValidation = true;
            this.HAISHUTSU_GENBA_CD_TO.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_FROM.CausesValidation = true;
            this.UNPAN_GYOUSHA_CD_TO.CausesValidation = true;
            this.SHOBUN_GYOUSHA_CD_FROM.CausesValidation = true;
            this.SHOBUN_GYOUSHA_CD_TO.CausesValidation = true;
            this.SHOBUN_GENBA_CD_FROM.CausesValidation = true;
            this.SHOBUN_GENBA_CD_TO.CausesValidation = true;
            this.LAST_SHOBUN_GYOUSHA_CD_FROM.CausesValidation = true;
            this.LAST_SHOBUN_GYOUSHA_CD_TO.CausesValidation = true;
            this.LAST_SHOBUN_GENBA_CD_FROM.CausesValidation = true;
            this.LAST_SHOBUN_GENBA_CD_TO.CausesValidation = true;
            this.HOUKOKUSHO_CD_FROM.CausesValidation = true;
            this.HOUKOKUSHO_CD_TO.CausesValidation = true;
            this.HAIKIBUTSU_CD_FROM.CausesValidation = true;
            this.HAIKIBUTSU_CD_TO.CausesValidation = true;
            this.SHOBUN_HOUHOU_CD_FROM.CausesValidation = true;
            this.SHOBUN_HOUHOU_CD_TO.CausesValidation = true;
            this.TORIHIKISAKI_CD_FROM.CausesValidation = true;
            this.TORIHIKISAKI_CD_TO.CausesValidation = true;
        }
    }
}
