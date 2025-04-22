using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace ChouhyouPatternPopup
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
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

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

            this.Text = this.WindowId.ToTitleString() + "パターン登録";
            this.TITLE_LABEL.Text = this.WindowId.ToTitleString() + "パターン登録";

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

            bool catchErr = this.SetShuukeiKoumokuListToComboBox1();
            if (catchErr)
            {
                return DialogResult.No;
            }

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

            //VAN 20200326 #134974, #134977 S
            //売上集計表画面、支払集計表画面以外の場合、明細項目を非表示
            if (this.WindowId != WINDOW_ID.T_URIAGE_SHUUKEIHYOU && this.WindowId != WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU)
            {
                this.pnlMeisaiItem.Visible = false;

                //画面の幅を調整
                int chouseiWidth = -160;
                this.Size = new System.Drawing.Size(this.Width + chouseiWidth, this.Size.Height);
            }
            //VAN 20200326 #134974, #134977 E

            if (this.WindowId == WINDOW_ID.T_URIAGE_ZENNEN_TAIHIHYOU
                || this.WindowId == WINDOW_ID.T_SHIHARAI_ZENNEN_TAIHIHYOU)
            {
                this.Width += 50;
                this.TITLE_LABEL.Width += 50;
                this.btnF12.Location = new System.Drawing.Point(this.btnF12.Location.X + 10, this.btnF12.Location.Y);
            }

            bool catchErr = this.GetDtoData();
            if (catchErr)
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
                    this.SHUUKEI_KOUMOKU_3.Enabled = false;
                    this.SHUUKEI_KOUMOKU_4.Enabled = false;
                    this.SHUUKEI_FLAG_1.Enabled = false;
                    this.SHUUKEI_FLAG_2.Enabled = false;
                    this.SHUUKEI_FLAG_3.Enabled = false;
                    this.SHUUKEI_FLAG_4.Enabled = false;
                    this.PATTERN_NAME.TabStop = false;
                    //VAN 20200326 #134974, #134977 S
                    this.HINMEI_DISP_FLG.Enabled = false;
                    this.NET_JUURYOU_DISP_FLG.Enabled = false;
                    this.SUURYOU_UNIT_DISP_FLG.Enabled = false;
                    //VAN 20200326 #134974, #134977 E
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面にパターンDTOからデータを取得します
        /// </summary>
        private bool GetDtoData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool catchErr = false;
                if (this.windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.PATTERN_NAME.Text = this.dto.PATTERN_NAME;
                    this.SHUUKEI_KOUMOKU_1.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(1).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUUKEI_KOUMOKU_1.SelectedItem != null)
                    {
                        catchErr = this.SetShuukeiKoumokuListToComboBox2(true);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                    this.SHUUKEI_KOUMOKU_2.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(2).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUUKEI_KOUMOKU_2.SelectedItem != null)
                    {
                        catchErr = this.SetShuukeiKoumokuListToComboBox3(true);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                    this.SHUUKEI_KOUMOKU_3.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(3).KOUMOKU_ID) == 0).FirstOrDefault();
                    if (this.SHUUKEI_KOUMOKU_3.SelectedItem != null)
                    {
                        catchErr = this.SetShuukeiKoumokuListToComboBox4(true);
                        if (catchErr)
                        {
                            return true;
                        }
                    }
                    this.SHUUKEI_KOUMOKU_4.SelectedItem = this.shuukeiKoumokuList.Where(s => s.KOUMOKU_ID.CompareTo(this.dto.GetPatternColumn(4).KOUMOKU_ID) == 0).FirstOrDefault();

                    this.SHUUKEI_FLAG_1.Checked = this.dto.GetShuukeiFlag(1);
                    this.SHUUKEI_FLAG_2.Checked = this.dto.GetShuukeiFlag(2);
                    this.SHUUKEI_FLAG_3.Checked = this.dto.GetShuukeiFlag(3);
                    this.SHUUKEI_FLAG_4.Checked = this.dto.GetShuukeiFlag(4);

                    //VAN 20200326 #134974, #134977 S
                    //売上集計表画面、支払集計表画面の場合
                    if (this.WindowId == WINDOW_ID.T_URIAGE_SHUUKEIHYOU || this.WindowId == WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU)
                    {
                        this.HINMEI_DISP_FLG.Checked = this.dto.Pattern.HINMEI_DISP_KBN.IsTrue;
                        this.NET_JUURYOU_DISP_FLG.Checked = this.dto.Pattern.NET_JYUURYOU_DISP_KBN.IsTrue;
                        this.SUURYOU_UNIT_DISP_FLG.Checked = this.dto.Pattern.SUURYOU_UNIT_DISP_KBN.IsTrue;
                    }
                    //VAN 20200326 #134974, #134977 E
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDtoData", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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
                if (shuukeiKoumoku1.KOUMOKU_ID.IsNull == false)
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
                var shuukeiKoumoku3 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_3.SelectedItem;
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
                var shuukeiKoumoku4 = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_4.SelectedItem;
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

                //VAN 20200326 #134974, #134977 S
                //売上集計表画面、支払集計表画面の場合
                if (this.WindowId == WINDOW_ID.T_URIAGE_SHUUKEIHYOU || this.WindowId == WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU)
                {
                    this.dto.Pattern.HINMEI_DISP_KBN = this.HINMEI_DISP_FLG.Checked;
                    this.dto.Pattern.NET_JYUURYOU_DISP_KBN = this.NET_JUURYOU_DISP_FLG.Checked;
                    this.dto.Pattern.SUURYOU_UNIT_DISP_KBN = this.SUURYOU_UNIT_DISP_FLG.Checked;
                }
                //VAN 20200326 #134974, #134977 E
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

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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

        /// <summary>
        /// 集計項目３にフォーカスが移動したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_3_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_3.SelectedItem;
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

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_4.SelectedItem;
            if (selectedItem == null)
            {
                selectedItem = new S_LIST_COLUMN_SELECT();
            }
            this.shuukeiKoumoku4ComboBoxSelectedItem = selectedItem;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目１のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_1_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = this.SetShuukeiKoumokuListToComboBox2(false);
            if (catchErr)
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
                this.SHUUKEI_FLAG_1.Enabled = false;
                this.shuukeiKoumoku1ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            //VAN 20200326 #134974, #134977 S
            ClearCheckMeisaiKoumoku();
            //VAN 20200326 #134974, #134977 E

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目２のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox2(bool isForceSet)
        {
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目１が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
                if (isForceSet || this.shuukeiKoumoku1ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku1SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目２以降のリストをクリア
                    this.SHUUKEI_KOUMOKU_2.DataSource = null;
                    this.SHUUKEI_KOUMOKU_3.DataSource = null;
                    this.SHUUKEI_KOUMOKU_4.DataSource = null;
                    this.SHUUKEI_KOUMOKU_2.Enabled = false;
                    this.SHUUKEI_KOUMOKU_3.Enabled = false;
                    this.SHUUKEI_KOUMOKU_4.Enabled = false;
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
                        newList = this.CreateShukeiKoumokuListWithCheckParentKoumokuUsed(newList, 1);

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
                        this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox2", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 集計項目２のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_2_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = this.SetShuukeiKoumokuListToComboBox3(false);
            if (catchErr)
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_2.Enabled = true;
                this.shuukeiKoumoku2ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_2.Enabled = false;
                this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            //VAN 20200326 #134974, #134977 S
            ClearCheckMeisaiKoumoku();
            //VAN 20200326 #134974, #134977 E

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目３のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox3(bool isForceSet)
        {
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目２が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
                var shuukeiKoumoku2SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
                if (shuukeiKoumoku2SelectedItem == null)
                {
                    shuukeiKoumoku2SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku2ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku2SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目３以降のリストをクリア
                    this.SHUUKEI_KOUMOKU_3.DataSource = null;
                    this.SHUUKEI_KOUMOKU_4.DataSource = null;
                    this.SHUUKEI_KOUMOKU_3.Enabled = false;
                    this.SHUUKEI_KOUMOKU_4.Enabled = false;
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
                        newList = this.CreateShukeiKoumokuListWithCheckParentKoumokuUsed(newList, 2);

                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目３にセット
                            this.SHUUKEI_KOUMOKU_3.DataSource = newList;
                            this.SHUUKEI_KOUMOKU_3.DisplayMember = DISPLAY_MEMBER;
                            this.SHUUKEI_KOUMOKU_3.ValueMember = VALUE_MEMBER;
                            this.SHUUKEI_KOUMOKU_3.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku2ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox3", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 集計項目３のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_3_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = this.SetShuukeiKoumokuListToComboBox4(false);
            if (catchErr)
            {
                return;
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_3.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_3.Enabled = true;
                this.shuukeiKoumoku3ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_3.Enabled = false;
                this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            //VAN 20200326 #134974, #134977 S
            ClearCheckMeisaiKoumoku();
            //VAN 20200326 #134974, #134977 E

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目４のリストボックスに集計項目リストをセットします
        /// </summary>
        /// <param name="isForceSet">強制的にセットする場合は、True</param>
        private bool SetShuukeiKoumokuListToComboBox4(bool isForceSet)
        {
            try
            {
                LogUtility.DebugMethodStart(isForceSet);

                // 集計項目３が変更されたら
                var shuukeiKoumoku1SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_1.SelectedItem;
                var shuukeiKoumoku2SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_2.SelectedItem;
                var shuukeiKoumoku3SelectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_3.SelectedItem;
                if (shuukeiKoumoku3SelectedItem == null)
                {
                    shuukeiKoumoku3SelectedItem = new S_LIST_COLUMN_SELECT();
                }
                if (isForceSet || this.shuukeiKoumoku3ComboBoxSelectedItem.KOUMOKU_ID.CompareTo(shuukeiKoumoku3SelectedItem.KOUMOKU_ID) != 0)
                {
                    // 集計項目４以降のリストをクリア
                    this.SHUUKEI_KOUMOKU_4.DataSource = null;
                    this.SHUUKEI_KOUMOKU_4.Enabled = false;
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
                        newList = this.CreateShukeiKoumokuListWithCheckParentKoumokuUsed(newList, 3);

                        // 選択できる項目が残っていたら
                        if (0 < newList.Count())
                        {
                            // 集計項目４にセット
                            this.SHUUKEI_KOUMOKU_4.DataSource = newList;
                            this.SHUUKEI_KOUMOKU_4.DisplayMember = DISPLAY_MEMBER;
                            this.SHUUKEI_KOUMOKU_4.ValueMember = VALUE_MEMBER;
                            this.SHUUKEI_KOUMOKU_4.Enabled = true;
                        }
                    }
                    else
                    {
                        this.shuukeiKoumoku3ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                        this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox4", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 集計項目４のテキストが変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHUUKEI_KOUMOKU_4_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.shuukeiKoumoku4ComboBoxSelectedItem == null || this.shuukeiKoumoku4ComboBoxSelectedItem.KOUMOKU_ID.IsNull)
            {
                this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            var selectedItem = (S_LIST_COLUMN_SELECT)this.SHUUKEI_KOUMOKU_4.SelectedItem;
            if (selectedItem != null && selectedItem.KOUMOKU_ID.IsNull == false)
            {
                this.SHUUKEI_FLAG_4.Enabled = true;
                this.shuukeiKoumoku4ComboBoxSelectedItem = selectedItem;
            }
            else
            {
                this.SHUUKEI_FLAG_4.Enabled = false;
                this.shuukeiKoumoku4ComboBoxSelectedItem = new S_LIST_COLUMN_SELECT();
            }

            //VAN 20200326 #134974, #134977 S
            ClearCheckMeisaiKoumoku();
            //VAN 20200326 #134974, #134977 E

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 集計項目１のリストボックスに集計項目リストをセットします
        /// </summary>
        private bool SetShuukeiKoumokuListToComboBox1()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.shuukeiKoumokuList.Count() > 0)
                {
                    var newList = new List<S_LIST_COLUMN_SELECT>(this.shuukeiKoumokuList);

                    // WindowID(帳票)別に親子関係項目の子項目を取り除いたリストを作成
                    switch (this.WindowId)
                    {
                        // 売上推移表
                        case WINDOW_ID.T_URIAGE_SUIIHYOU:

                            // 現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷卸現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        // 支払推移表
                        case WINDOW_ID.T_SHIHARAI_SUIIHYOU:

                            // 現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷卸現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        // 売上集計表
                        case WINDOW_ID.T_URIAGE_SHUUKEIHYOU:
                        case WINDOW_ID.T_URIAGE_ZENNEN_TAIHIHYOU:
                            // 現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷降現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        // 支払集計表
                        case WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU:

                            // 現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷降現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        // 運賃集計表
                        case WINDOW_ID.T_UNCHIN_SHUUKEIHYOU:

                            // 荷降現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場                                                        
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌                                                             
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        // 計量集計表
                        case WINDOW_ID.T_KEIRYOU_SHUUKEIHYOU:

                            // 現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷降現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 荷積現場
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                            // 車輌
                            newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                            break;

                        default:
                            // Nothing
                            break;
                    }

                    this.SHUUKEI_KOUMOKU_1.DataSource = newList;
                    this.SHUUKEI_KOUMOKU_1.DisplayMember = DISPLAY_MEMBER;
                    this.SHUUKEI_KOUMOKU_1.ValueMember = VALUE_MEMBER;
                    this.SHUUKEI_KOUMOKU_1.SelectedIndex = 0;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShuukeiKoumokuListToComboBox1", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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
        /// 現在のWindowIdにより各帳票で親子関係に属する項目のうち親に当たる項目が既に使用されているかをチェックし、
        /// 使用されていない場合は親項目を取り除いたリストを返します。
        /// </summary>
        /// <param name="list">リストボックスに表示するデータのリスト</param>
        /// <param name="checkIndex">チェック対象最大項目番号</param>
        /// <returns>リストボックスに表示するデータのリスト</returns>
        private List<S_LIST_COLUMN_SELECT> CreateShukeiKoumokuListWithCheckParentKoumokuUsed(List<S_LIST_COLUMN_SELECT> list, int checkIndex)
        {
            List<S_LIST_COLUMN_SELECT> newList = new List<S_LIST_COLUMN_SELECT>(list);

            switch (this.WindowId)
            {
                // 売上推移表
                case WINDOW_ID.T_URIAGE_SUIIHYOU:

                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                // 支払推移表
                case WINDOW_ID.T_SHIHARAI_SUIIHYOU:

                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.SUIIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.SUIIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                // 売上集計表
                case WINDOW_ID.T_URIAGE_SHUUKEIHYOU:
                case WINDOW_ID.T_URIAGE_ZENNEN_TAIHIHYOU:
                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                // 支払集計表
                case WINDOW_ID.T_SHIHARAI_SHUUKEIHYOU:

                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UR_SH_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                // 運賃集計表
                case WINDOW_ID.T_UNCHIN_SHUUKEIHYOU:

                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UNCHIN_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.UNCHIN_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.UNCHIN_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.UNCHIN_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                // 計量集計表
                case WINDOW_ID.T_KEIRYOU_SHUUKEIHYOU:

                    // 業者 - 現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.KEIRYOU_SHUKEIHYO_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷卸業者 - 荷卸現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.KEIRYOU_SHUKEIHYO_NIOROSHI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_NIOROSHI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 荷積業者 - 荷積現場
                    if (!this.CheckParentKoumokuUsed(ConstClass.KEIRYOU_SHUKEIHYO_NIDUMI_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_NIDUMI_GENBA_KOUMOKU_ID) == 0).FirstOrDefault());
                    }
                    // 運搬業者 - 車輌
                    if (!this.CheckParentKoumokuUsed(ConstClass.KEIRYOU_SHUKEIHYO_UNPAN_GYOUSHA_KOUMOKU_ID, checkIndex))
                    {
                        newList.Remove(newList.Where(s => s.KOUMOKU_ID.CompareTo(ConstClass.KEIRYOU_SHUKEIHYO_SHARYO_KOUMOKU_ID) == 0).FirstOrDefault());
                    }

                    break;

                default:

                    // Nothing
                    break;
            }

            return newList;
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
                r_framework.CustomControl.CustomComboBox control = (r_framework.CustomControl.CustomComboBox)this.Controls["SHUUKEI_KOUMOKU_" + i];
                S_LIST_COLUMN_SELECT selectedItem = (S_LIST_COLUMN_SELECT)control.SelectedItem;
                if (selectedItem != null && selectedItem.KOUMOKU_ID == parentId)//VAN 20200326 #134974, #134977
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        //VAN 20200326 #134974, #134977 S
        #region 明細項目
        private void HINMEI_DISP_FLG_CheckedChanged(object sender, EventArgs e)
        {
            if (HINMEI_DISP_FLG.Checked)
            {
                //集計項目に「品名」選択有りの場合
                if (IsShuukeiHinmei())
                {
                    this.errmessage.MessageBoxShowError(ConstClass.MSG_ERR_CAN_NOT_SET_HINMEI_MEISAI);
                    HINMEI_DISP_FLG.Checked = false;
                }
            }
            else
            {
                ClearCheckMeisaiKoumoku();
            }
        }

        private void NET_JUURYOU_DISP_FLG_CheckedChanged(object sender, EventArgs e)
        {
            //PhuocLoc 2020/12/08 #136226 -Start
            //if (NET_JUURYOU_DISP_FLG.Checked && !CheckUsedHinmei())
            //{
            //    this.errmessage.MessageBoxShowError(ConstClass.MSG_ERR_REQUIRED_HINMEI);
            //    NET_JUURYOU_DISP_FLG.Checked = false;
            //}
            //PhuocLoc 2020/12/08 #136226 -End
        }

        private void SUURYOU_UNIT_DISP_FLG_CheckedChanged(object sender, EventArgs e)
        {
            //PhuocLoc 2020/12/08 #136226 -Start
            //if (SUURYOU_UNIT_DISP_FLG.Checked && !CheckUsedHinmei())
            //{
            //    this.errmessage.MessageBoxShowError(ConstClass.MSG_ERR_REQUIRED_HINMEI);
            //    SUURYOU_UNIT_DISP_FLG.Checked = false;
            //}
            //PhuocLoc 2020/12/08 #136226 -End
        }

        /// <summary>
        /// 集計項目、明細項目のいずれかで「品名」を選択するかないかチェック
        /// </summary>
        /// <returns></returns>
        private bool CheckUsedHinmei()
        {
            bool ret = false;
            if (HINMEI_DISP_FLG.Checked || IsShuukeiHinmei())
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 集計項目「品名」を選択するかないかチェック
        /// </summary>
        /// <returns></returns>
        private bool IsShuukeiHinmei()
        {
            bool ret = false;

            if (this.SHUUKEI_KOUMOKU_1.SelectedItem == null &&
                this.SHUUKEI_KOUMOKU_2.SelectedItem == null &&
                this.SHUUKEI_KOUMOKU_3.SelectedItem == null &&
                this.SHUUKEI_KOUMOKU_4.SelectedItem == null)
            {
                return ret;
            }

            ret = this.CheckParentKoumokuUsed(ConstClass.UR_SH_SHUKEIHYO_HINMEI_KOUMOKU_ID, 4);
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearCheckMeisaiKoumoku()
        {
            if (IsShuukeiHinmei())
            {
                this.HINMEI_DISP_FLG.Checked = false;
            }
            //PhuocLoc 2020/12/08 #136226 -Start
            //else
            //{
            //    if (this.HINMEI_DISP_FLG.Checked == false)
            //    {
            //        this.NET_JUURYOU_DISP_FLG.Checked = false;
            //        this.SUURYOU_UNIT_DISP_FLG.Checked = false;
            //    }
            //}
            //PhuocLoc 2020/12/08 #136226 -End
        }

        #endregion
        //VAN 20200326 #134974, #134977 E
    }
}
