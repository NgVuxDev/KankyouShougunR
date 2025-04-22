using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou
{
    /// <summary>
    /// 帳票パターン登録ポップアップ 画面クラス
    /// </summary>
    public partial class ChouhyouPatternTourokuPopupForm : SuperPopupForm
    {
        #region Const

        /// <summary>
        /// 銀行 項目ID
        /// </summary>
        private const int BANK_KOUMOKU_ID = 3;

        /// <summary>
        /// 銀行支店 項目ID
        /// </summary>
        private const int BANK_SHITEN_KOUMOKU_ID = 4;

        #endregion

        /// <summary>
        /// コンボボックスの表示に使用するプロパティ名
        /// </summary>
        private static readonly string DISPLAY_MEMBER = "KOUMOKU_RONRI_NAME";

        /// <summary>
        /// コンボボックスの選択値に使用するプロパティ名
        /// </summary>
        private static readonly string VALUE_MEMBER = "KOUMOKU_ID";

        /// <summary>
        /// 画面区分
        /// </summary>
        private WINDOW_TYPE windowType;

        /// <summary>
        /// ロジッククラス
        /// </summary>
        private ChouhyouPatternTourokuPopupLogic logic;

        /// <summary>
        /// パターンDTOクラス
        /// </summary>
        private PatternDto dto;

        /// <summary>
        /// 集計項目のコンボボックスに表示するリスト
        /// </summary>
        private List<S_LIST_COLUMN_SELECT> shuukeiKoumokuList;

        /// <summary>
        /// 集計項目１の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// 集計項目２の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// 集計項目３の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// 集計項目４の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// 集計項目５の前回値
        /// </summary>
        private S_LIST_COLUMN_SELECT shuukeiKoumoku5ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        private ChouhyouPatternTourokuPopupForm()
        {
            LogUtility.DebugMethodStart();

            InitializeComponent();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="patternDto">パターンDTO</param>
        public ChouhyouPatternTourokuPopupForm(WINDOW_TYPE windowType, PatternDto patternDto)
        {
            LogUtility.DebugMethodStart(windowType, patternDto);

            InitializeComponent();

            this.windowType = windowType;
            this.WindowId = (WINDOW_ID)patternDto.Pattern.WINDOW_ID.Value;

            this.logic = new ChouhyouPatternTourokuPopupLogic();
            this.dto = patternDto;

            this.Text = "入金集計表パターン登録";
            this.TITLE_LABEL.Text = "入金集計表パターン登録";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダイアログを表示します
        /// </summary>
        /// <returns>ダイアログボックスの戻り値</returns>
        public new DialogResult ShowDialog(int length = 0)
        {
            LogUtility.DebugMethodStart();

            var ret = DialogResult.OK;
            
            this.shuukeiKoumokuList = this.logic.GetShuukeiKoumokuList(this.WindowId);
            this.shuukeiKoumokuList.Insert(0, new S_LIST_COLUMN_SELECT());

            this.SetShuukeiKoumokuListToComboBox1();

            ret = base.ShowDialog();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 画面がロードされたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            if (!this.GetDtoData())
            {
                return;
            }

            switch (this.windowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.PATTERN_NAME.ReadOnly = true;
                    this.SHUTSURYOKU_KOUMOKU_1.Enabled = false;
                    this.SHUTSURYOKU_KOUMOKU_2.Enabled = false;
                    this.SHUTSURYOKU_KOUMOKU_3.Enabled = false;
                    this.SHUTSURYOKU_KOUMOKU_4.Enabled = false;
                    this.SHUTSURYOKU_KOUMOKU_5.Enabled = false;
                    this.SHUUKEI_FLAG_1.Enabled = false;
                    this.SHUUKEI_FLAG_2.Enabled = false;
                    this.SHUUKEI_FLAG_3.Enabled = false;
                    this.SHUUKEI_FLAG_4.Enabled = false;
                    this.SHUUKEI_FLAG_5.Enabled = false;
                    this.PATTERN_NAME.TabStop = false;
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面にパターンDTOからデータを取得します
        /// </summary>
        private bool GetDtoData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.PATTERN_NAME.Text = this.dto.PATTERN_NAME;
                    this.SHUTSURYOKU_KOUMOKU_1.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(1).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUTSURYOKU_KOUMOKU_1.SelectedItem != null)
                    {
                        if (!this.SetShuukeiKoumokuListToComboBox2(true))
                        {
                            return false;
                        }
                    }
                    this.SHUTSURYOKU_KOUMOKU_2.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(2).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUTSURYOKU_KOUMOKU_2.SelectedItem != null)
                    {
                        if (!this.SetShuukeiKoumokuListToComboBox3(true))
                        {
                            return false;
                        }
                    }
                    this.SHUTSURYOKU_KOUMOKU_3.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(3).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUTSURYOKU_KOUMOKU_3.SelectedItem != null)
                    {
                        if (!this.SetShuukeiKoumokuListToComboBox4(true))
                        {
                            return false;
                        }
                    }
                    this.SHUTSURYOKU_KOUMOKU_4.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(4).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUTSURYOKU_KOUMOKU_4.SelectedItem != null)
                    {
                        if(!this.SetShuukeiKoumokuListToComboBox5(true))
                        {
                            return false;
                        }
                    }
                    this.SHUTSURYOKU_KOUMOKU_5.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(5).KOUMOKU_ID) == 0).FirstOrDefault();

                    this.SHUUKEI_FLAG_1.Checked = this.dto.GetShuukeiFlag(1);
                    this.SHUUKEI_FLAG_2.Checked = this.dto.GetShuukeiFlag(2);
                    this.SHUUKEI_FLAG_3.Checked = this.dto.GetShuukeiFlag(3);
                    this.SHUUKEI_FLAG_4.Checked = this.dto.GetShuukeiFlag(4);
                    this.SHUUKEI_FLAG_5.Checked = this.dto.GetShuukeiFlag(5);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDtoData", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// パターンDTOに画面からデータをセットします
        /// </summary>
        private void SetDtoData()
        {
            LogUtility.DebugMethodStart();

            if (this.windowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.dto.Pattern.PATTERN_NAME = this.PATTERN_NAME.Text;
                this.dto.PatternColumnList = new List<M_LIST_PATTERN_COLUMN>();
                var shuukeiKoumoku1 = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
                if (shuukeiKoumoku1 != null && shuukeiKoumoku1.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = this.SHUUKEI_FLAG_1.Checked,
                        ROW_NO = 1,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku1.KOUMOKU_ID
                    });
                }
                var shuukeiKoumoku2 = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
                if (shuukeiKoumoku2 != null && shuukeiKoumoku2.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = this.SHUUKEI_FLAG_2.Checked,
                        ROW_NO = 2,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku2.KOUMOKU_ID
                    });
                }
                var shuukeiKoumoku3 = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_3.SelectedItem;
                if (shuukeiKoumoku3 != null && shuukeiKoumoku3.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = this.SHUUKEI_FLAG_3.Checked,
                        ROW_NO = 3,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku3.KOUMOKU_ID
                    });
                }
                var shuukeiKoumoku4 = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_4.SelectedItem;
                if (shuukeiKoumoku4 != null && shuukeiKoumoku4.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = this.SHUUKEI_FLAG_4.Checked,
                        ROW_NO = 4,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku4.KOUMOKU_ID
                    });
                }
                var shuukeiKoumoku5 = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_5.SelectedItem;
                if (shuukeiKoumoku5 != null && shuukeiKoumoku5.KOUMOKU_ID.IsNull == false)
                {
                    this.dto.PatternColumnList.Add(new M_LIST_PATTERN_COLUMN()
                    {
                        // テーブル定義を変更したくないため、集計のフラグはDETAIL_KBNを使用する
                        DETAIL_KBN = this.SHUUKEI_FLAG_5.Checked,
                        ROW_NO = 5,
                        WINDOW_ID = (int)this.WindowId,
                        KOUMOKU_ID = shuukeiKoumoku5.KOUMOKU_ID
                    });
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キーが押されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ChouhyouPatternSentakuPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            switch (e.KeyData)
            {
                case Keys.F9:
                    this.btnF9.Focus();
                    this.Regist();
                    break;
                case Keys.F12:
                    this.FormClose(DialogResult.Cancel);
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]登録ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnF9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Regist();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnF12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.FormClose(DialogResult.Cancel);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理を行います
        /// </summary>
        private bool Regist()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var res = false;

                if (this.InputError() == false)
                {
                    this.SetDtoData();

                    res = this.logic.Regist(this.windowType, this.dto);
                    if (true == res)
                    {
                        this.FormClose(DialogResult.OK);
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.logic.errmessage.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.logic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 入力チェックを行います
        /// </summary>
        /// <returns>入力チェックがある場合、True</returns>
        private bool InputError()
        {
            LogUtility.DebugMethodStart();

            var ret = false;
            var koumoku = String.Empty;
            if (String.IsNullOrEmpty(this.PATTERN_NAME.Text))
            {
                this.PATTERN_NAME.IsInputErrorOccured = true;
                koumoku = koumoku + "帳票名";
                ret = true;
            }
            else
            {
                if (this.windowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    (this.windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && this.dto.PATTERN_NAME != this.PATTERN_NAME.Text))
                {
                    // 既に登録されているパターン名の場合
                    var mListPatternDao = DaoInitUtility.GetComponent<IM_LIST_PATTERNDao>().GetMListPatternList((int)this.WindowId);
                    foreach (M_LIST_PATTERN ptn in mListPatternDao)
                    {
                        if (ptn.PATTERN_NAME == this.PATTERN_NAME.Text)
                        {
                            this.PATTERN_NAME.IsInputErrorOccured = true;
                            MessageBox.Show("既に登録されているパターン名です。" + Environment.NewLine +
                                            "違うパターン名に変更してください。", "アラート",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }
            }

            if (this.SHUTSURYOKU_KOUMOKU_1.SelectedIndex <= 0 &&
                this.SHUTSURYOKU_KOUMOKU_2.SelectedIndex <= 0 &&
                this.SHUTSURYOKU_KOUMOKU_3.SelectedIndex <= 0 &&
                this.SHUTSURYOKU_KOUMOKU_4.SelectedIndex <= 0 &&
                this.SHUTSURYOKU_KOUMOKU_5.SelectedIndex <= 0)
            {
                this.SHUTSURYOKU_KOUMOKU_1.IsInputErrorOccured = true;
                this.SHUTSURYOKU_KOUMOKU_2.IsInputErrorOccured = true;
                this.SHUTSURYOKU_KOUMOKU_3.IsInputErrorOccured = true;
                this.SHUTSURYOKU_KOUMOKU_4.IsInputErrorOccured = true;
                this.SHUTSURYOKU_KOUMOKU_5.IsInputErrorOccured = true;

                if (String.IsNullOrEmpty(koumoku) == false)
                {
                    koumoku = koumoku + "、";
                }
                koumoku = koumoku + "集計項目";
                ret = true;
            }

            if (ret)
            {
                new MessageBoxShowLogic().MessageBoxShow("E001", koumoku);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ポップアップを閉じます
        /// </summary>
        /// <param name="result">ダイアログの戻り値</param>
        private void FormClose(DialogResult result)
        {
            LogUtility.DebugMethodStart(result);

            this.DialogResult = result;
            this.Close();

            LogUtility.DebugMethodEnd();
        }

        #region 各ComboBoxのEnter
        /// <summary>
        /// 集計項目１にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_1_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku1ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_2_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku2ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目３にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_3_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_3.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku3ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目４にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_4_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_4.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku4ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目５にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_5_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_5.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku5ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 各コンボボックスの値変更イベント
        /// <summary>
        /// 集計項目１のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_1_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetShuukeiKoumokuListToComboBox2(false))
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_1.Enabled = true;
                this.shuukeiKoumoku1ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_1.Checked = false;
                this.SHUUKEI_FLAG_1.Enabled = false;
                this.shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_2_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetShuukeiKoumokuListToComboBox3(false))
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_2.Enabled = true;
                this.shuukeiKoumoku2ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_2.Checked = false;
                this.SHUUKEI_FLAG_2.Enabled = false;
                this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目３のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_3_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetShuukeiKoumokuListToComboBox4(false))
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_3.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_3.Enabled = true;
                this.shuukeiKoumoku3ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_3.Checked = false;
                this.SHUUKEI_FLAG_3.Enabled = false;
                this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目４のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_4_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetShuukeiKoumokuListToComboBox5(false))
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_4.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_4.Enabled = true;
                this.shuukeiKoumoku4ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_4.Checked = false;
                this.SHUUKEI_FLAG_4.Enabled = false;
                this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目５のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUTSURYOKU_KOUMOKU_5_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_5.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_5.Enabled = true;
                this.shuukeiKoumoku5ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_5.Checked = false;
                this.SHUUKEI_FLAG_5.Enabled = false;
                this.shuukeiKoumoku5ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 集計項目１のリストボックスに集計項目リストをセットします
        /// </summary>
        private void SetShuukeiKoumokuListToComboBox1()
        {
            LogUtility.DebugMethodStart();

            if (this.shuukeiKoumokuList.Count() > 0)
            {
                // 親子関係項目の子項目を取り除いたリストを作成
                var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                // 銀行支店
                newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(BANK_SHITEN_KOUMOKU_ID) == 0).FirstOrDefault());

                this.SHUTSURYOKU_KOUMOKU_1.DataSource = newList;
                this.SHUTSURYOKU_KOUMOKU_1.DisplayMember = DISPLAY_MEMBER;
                this.SHUTSURYOKU_KOUMOKU_1.ValueMember = VALUE_MEMBER;
                this.SHUTSURYOKU_KOUMOKU_1.SelectedIndex = 0;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox2(bool isForceSet)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目１が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
                if (shuukeiKoumoku1SelectedItem != null)
                {
                    if (isForceSet || this.shuukeiKoumoku1ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) != 0)
                    {
                        // 集計項目２以降のリストをクリア
                        this.SHUTSURYOKU_KOUMOKU_2.DataSource = null;
                        this.SHUTSURYOKU_KOUMOKU_3.DataSource = null;
                        this.SHUTSURYOKU_KOUMOKU_4.DataSource = null;
                        this.SHUTSURYOKU_KOUMOKU_2.Enabled = false;
                        this.SHUTSURYOKU_KOUMOKU_3.Enabled = false;
                        this.SHUTSURYOKU_KOUMOKU_4.Enabled = false;
                        this.SHUUKEI_FLAG_2.Enabled = false;
                        this.SHUUKEI_FLAG_3.Enabled = false;
                        this.SHUUKEI_FLAG_4.Enabled = false;

                        // 集計項目１が選択されていたら
                        if (shuukeiKoumoku1SelectedItem.KOUMOKU_ID.IsNull == false)
                        {
                            // 集計項目１で選択されている項目を除いたリストを作成
                            var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());

                            // 親子関係項目の親が上位項目で選択されているかチェックし、選択されていない場合は子項目を取り除いたリストを作成
                            // 銀行 - 銀行支店
                            if (!this.CheckParentKoumokuUsed(BANK_KOUMOKU_ID, 1))
                            {
                                newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(BANK_SHITEN_KOUMOKU_ID) == 0).FirstOrDefault());
                            }

                            // 選択できる項目が残っていたら
                            if (0 < newList.Count())
                            {
                                // 集計項目２にセット
                                this.SHUTSURYOKU_KOUMOKU_2.DataSource = newList;
                                this.SHUTSURYOKU_KOUMOKU_2.DisplayMember = DISPLAY_MEMBER;
                                this.SHUTSURYOKU_KOUMOKU_2.ValueMember = VALUE_MEMBER;
                                this.SHUTSURYOKU_KOUMOKU_2.Enabled = true;
                            }
                        }
                        else
                        {
                            this.shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                            this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                            this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                            this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox2", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 集計項目３のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox3(bool isForceSet)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目２が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
                var shuukeiKoumoku2SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
                if (shuukeiKoumoku2SelectedItem == null)
                {
                    shuukeiKoumoku2SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku2ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku2SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目３以降のリストをクリア
                    this.SHUTSURYOKU_KOUMOKU_3.DataSource = null;
                    this.SHUTSURYOKU_KOUMOKU_4.DataSource = null;
                    this.SHUTSURYOKU_KOUMOKU_3.Enabled = false;
                    this.SHUTSURYOKU_KOUMOKU_4.Enabled = false;
                    this.SHUUKEI_FLAG_3.Enabled = false;
                    this.SHUUKEI_FLAG_4.Enabled = false;

                    // 集計項目２が選択されていたら
                    if (shuukeiKoumoku2SelectedItem.KOUMOKU_ID.IsNull == false)
                    {
                        // 集計項目１、集計項目２で選択されている項目を除いたリストを作成
                        var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku2SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());

                        // 親子関係項目の親が上位項目で選択されているかチェックし、選択されていない場合は子項目を取り除いたリストを作成
                        // 銀行 - 銀行支店
                        if (!this.CheckParentKoumokuUsed(BANK_KOUMOKU_ID, 2))
                        {
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(BANK_SHITEN_KOUMOKU_ID) == 0).FirstOrDefault());
                        }

                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目３にセット
                            this.SHUTSURYOKU_KOUMOKU_3.DataSource = newList;
                            this.SHUTSURYOKU_KOUMOKU_3.DisplayMember = DISPLAY_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_3.ValueMember = VALUE_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_3.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox3", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 集計項目４のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox4(bool isForceSet)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目３が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
                var shuukeiKoumoku2SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
                var shuukeiKoumoku3SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_3.SelectedItem;
                if (shuukeiKoumoku3SelectedItem == null)
                {
                    shuukeiKoumoku3SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku3ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku3SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目４以降のリストをクリア
                    this.SHUTSURYOKU_KOUMOKU_4.DataSource = null;
                    this.SHUTSURYOKU_KOUMOKU_4.Enabled = false;
                    this.SHUUKEI_FLAG_4.Enabled = false;

                    // 集計項目３が選択されていたら
                    if (shuukeiKoumoku3SelectedItem.KOUMOKU_ID.IsNull == false)
                    {
                        // 集計項目１、集計項目２、集計項目３で選択されている項目を除いたリストを作成
                        var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku2SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku3SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());

                        // 親子関係項目の親が上位項目で選択されているかチェックし、選択されていない場合は子項目を取り除いたリストを作成
                        // 銀行 - 銀行支店
                        if (!this.CheckParentKoumokuUsed(BANK_KOUMOKU_ID, 3))
                        {
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(BANK_SHITEN_KOUMOKU_ID) == 0).FirstOrDefault());
                        }

                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目４にセット
                            this.SHUTSURYOKU_KOUMOKU_4.DataSource = newList;
                            this.SHUTSURYOKU_KOUMOKU_4.DisplayMember = DISPLAY_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_4.ValueMember = VALUE_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_4.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox4", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 集計項目５のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox5(bool isForceSet)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目４が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_1.SelectedItem;
                var shuukeiKoumoku2SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_2.SelectedItem;
                var shuukeiKoumoku3SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_3.SelectedItem;
                var shuukeiKoumoku4SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUTSURYOKU_KOUMOKU_4.SelectedItem;
                if (shuukeiKoumoku4SelectedItem == null)
                {
                    shuukeiKoumoku4SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku4ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku4SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目５以降のリストをクリア
                    this.SHUTSURYOKU_KOUMOKU_5.DataSource = null;
                    this.SHUTSURYOKU_KOUMOKU_5.Enabled = false;
                    this.SHUUKEI_FLAG_5.Enabled = false;

                    // 集計項目４が選択されていたら
                    if (shuukeiKoumoku4SelectedItem.KOUMOKU_ID.IsNull == false)
                    {
                        // 集計項目１、集計項目２、集計項目３で選択されている項目を除いたリストを作成
                        var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku2SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku3SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku4SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());

                        // 親子関係項目の親が上位項目で選択されているかチェックし、選択されていない場合は子項目を取り除いたリストを作成
                        // 銀行 - 銀行支店
                        if (!this.CheckParentKoumokuUsed(BANK_KOUMOKU_ID, 4))
                        {
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(BANK_SHITEN_KOUMOKU_ID) == 0).FirstOrDefault());
                        }

                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目５にセット
                            this.SHUTSURYOKU_KOUMOKU_5.DataSource = newList;
                            this.SHUTSURYOKU_KOUMOKU_5.DisplayMember = DISPLAY_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_5.ValueMember = VALUE_MEMBER;
                            this.SHUTSURYOKU_KOUMOKU_5.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku5ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox5", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 親子関係に属する項目のうち親に当たる項目が既に使用されているかをチェックします
        /// </summary>
        /// <param name="parentId">親に当たる項目の項目ID</param>
        /// <param name="checkIndex">チェック対象位置</param>
        /// <returns>使用されている：True</returns>
        private bool CheckParentKoumokuUsed(int parentId, int checkIndex)
        {
            // 使用例
            // checkIndexが3の場合、第一 ～ 第三項目までで指定されたparentIdに該当する項目が存在するかチェックします

            bool result = false;

            for (int i = 1; i <= checkIndex; i++)
            {
                r_framework.CustomControl.CustomComboBox control = (r_framework.CustomControl.CustomComboBox)this.Controls["SHUTSURYOKU_KOUMOKU_" + i];
                S_LIST_COLUMN_SELECT selectedItem = (S_LIST_COLUMN_SELECT)control.SelectedItem;
                if (selectedItem.KOUMOKU_ID == parentId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
