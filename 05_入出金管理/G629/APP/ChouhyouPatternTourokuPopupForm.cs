using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Seasar.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou
{
    /// <summary>
    /// 帳票パターン登録ポップアップ 画面クラス
    /// </summary>
    public partial class ChouhyouPatternTourokuPopupForm : SuperPopupForm
    {
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

            this.Text = "出金集計表パターン登録";
            this.TITLE_LABEL.Text = "出金集計表パターン登録";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダイアログを表示します
        /// </summary>
        /// <returns>ダイアログボックスの戻り値</returns>
        public new DialogResult ShowDialog()
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
                    this.SHUUKEI_KOUMOKU_1.Enabled = false;
                    this.SHUUKEI_KOUMOKU_2.Enabled = false;
                    this.SHUUKEI_FLAG_1.Enabled = false;
                    this.SHUUKEI_FLAG_2.Enabled = false;
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
                    this.SHUUKEI_KOUMOKU_1.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(1).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUUKEI_KOUMOKU_1.SelectedItem != null)
                    {
                        if (!this.SetShuukeiKoumokuListToComboBox2(true))
                        {
                            return false;
                        }
                    }
                    this.SHUUKEI_KOUMOKU_2.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(2).KOUMOKU_ID) == 0).FirstOrDefault();

                    this.SHUUKEI_FLAG_1.Checked = this.dto.GetShuukeiFlag(1);
                    this.SHUUKEI_FLAG_2.Checked = this.dto.GetShuukeiFlag(2);
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
                
                var shuukeiKoumoku1 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
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
                
                var shuukeiKoumoku2 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
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

            if (((S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem).KOUMOKU_ID.IsNull == true)
            {
                this.SHUUKEI_KOUMOKU_1.IsInputErrorOccured = true;
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

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
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

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku2ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }
        #endregion

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

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUUKEI_KOUMOKU_2_TextChanged(object sender, EventArgs e)
        {
            if (this.shuukeiKoumoku2ComboBoxSelectedItem == null || this.shuukeiKoumoku2ComboBoxSelectedItem.KOUMOKU_ID.IsNull)
            {
                this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
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
        }

        /// <summary>
        /// 集計項目１のリストボックスに集計項目リストをセットします
        /// </summary>
        private void SetShuukeiKoumokuListToComboBox1()
        {
            LogUtility.DebugMethodStart();

            if (this.shuukeiKoumokuList.Count() > 0)
            {
                this.SHUUKEI_KOUMOKU_1.DataSource = this.shuukeiKoumokuList;
                this.SHUUKEI_KOUMOKU_1.DisplayMember = DISPLAY_MEMBER;
                this.SHUUKEI_KOUMOKU_1.ValueMember = VALUE_MEMBER;
                this.SHUUKEI_KOUMOKU_1.SelectedIndex = 0;
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
                LogUtility.DebugMethodStart();

                // 集計項目１が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
                if (shuukeiKoumoku1SelectedItem == null)
                {
                    shuukeiKoumoku1SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku1ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目２以降のリストをクリア
                    this.SHUUKEI_KOUMOKU_2.DataSource = null;
                    this.SHUUKEI_KOUMOKU_2.Enabled = false;
                    this.SHUUKEI_FLAG_2.Enabled = false;

                    // 集計項目１が選択されていたら
                    if (shuukeiKoumoku1SelectedItem.KOUMOKU_ID.IsNull == false)
                    {
                        // 集計項目１で選択されている項目を除いたリストを作成
                        var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) == 0).FirstOrDefault());
                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目２にセット
                            this.SHUUKEI_KOUMOKU_2.DataSource = newList;
                            this.SHUUKEI_KOUMOKU_2.DisplayMember = DISPLAY_MEMBER;
                            this.SHUUKEI_KOUMOKU_2.ValueMember = VALUE_MEMBER;
                            this.SHUUKEI_KOUMOKU_2.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
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
    }
}
