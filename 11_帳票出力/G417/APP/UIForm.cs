using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup;
using Seasar.Framework.Exceptions;
using r_framework.Utility;
using Shougun.Core.Message;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup
{
    #region - Class -

    /// <summary>ユーザーインターフェースフォームを表すクラス・コントロール</summary>
    public partial class G417_MeisaihyoSyukeihyoPatternSentakuPopupForm : Form
    {
        #region - Fields -

        /// <summary>デフォルトテンプレートパスを保持するフィールド</summary>
        private const string ConstDefaultTemplateFolder = @".\Template\";

        /// <summary>ビジネスロジックインターフェースを保持するフィールド</summary>
        private IBuisinessLogic logic;

        /// <summary>メッセージボックス表示ロジックオブジェクトを保持するフィールド</summary>
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="G417_MeisaihyoSyukeihyoPatternSentakuPopupForm" /> class.</summary>
        /// <param name="id">画面ＩＤを表す</param>
        public G417_MeisaihyoSyukeihyoPatternSentakuPopupForm(WINDOW_ID id)
        {
            this.InitializeComponent();

            /// <summary>システムＩＤを保持するフィールド</summary>
            this.SystemID = -1;

            /// <summary>シーケンスＩＤを保持するフィールド</summary>
            this.SequenceID = -1;

            /// <summary>帳票名を保持するフィールド</summary>
            this.PatternName = string.Empty;

            /// <summary>作成者名を保持するフィールド</summary>
            this.CreateUser = string.Empty;

            /// <summary>作成ＰＣを保持するフィールド</summary>
            this.CreatePC = string.Empty;

            /// <summary>作成日付を保持するフィールド</summary>
            this.CreateDate = new DateTime(0);

            /// <summary>タイムスタンプを保持するフィールド</summary>
            this.TimeStamp = null;

            /// <summary>既に登録されている帳票（パターン）名を保持するフィールド</summary>
            this.PatternNameList = new List<string>();

            // MultiRowの存在有無
            this.IsMultiRowExist = true;

            this.customListBox.Items.Clear();

            this.WindowID = id;

            // デフォルトテンプレートパス設定
            this.DefaultFormPath(id);

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>ウィンドウＩＤ</summary>
        public WINDOW_ID WindowID { get; set; }

        /// <summary>親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>帳票出力フルパスフォーム名を保持するプロパティ</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するプロパティ</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>帳票出力内容を保持するプロパティ</summary>
        public CommonChouhyouBase CommonChouhyou { get; set; }

        /// <summary>エクセル出力する際のエクセル出力をするかＰＤＦ出力するかの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ＰＤＦで出力、偽の場合：Ｅｘｃｅｌで出力</remarks>
        public bool IsOutputPDF { get; set; }

        /// <summary>MultiRowの存在有無の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：MultiRowが存在、偽の場合：MultiRowが存在しない</remarks>
        public bool IsMultiRowExist { get; set; }

        /// <summary>システムＩＤを保持するプロパティ</summary>
        internal long SystemID { get; set; }

        /// <summary>シーケンスＩＤを保持するプロパティ</summary>
        internal int SequenceID { get; set; }

        /// <summary>帳票名を保持するプロパティ</summary>
        internal string PatternName { get; set; }

        /// <summary>作成者名を保持するプロパティ</summary>
        internal string CreateUser { get; set; }

        /// <summary>作成ＰＣを保持するプロパティ</summary>
        internal string CreatePC { get; set; }

        /// <summary>作成日付を保持するプロパティ</summary>
        internal DateTime CreateDate { get; set; }

        /// <summary>タイムスタンプを保持するプロパティ</summary>
        internal object TimeStamp { get; set; }

        /// <summary>既に登録されている帳票（パターン）名を保持するプロパティ</summary>
        internal List<string> PatternNameList { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>デフォルトテンプレートパス設定処理を実行する</summary>
        /// <param name="id">ウィンドウＩＤ</param>
        private void DefaultFormPath(WINDOW_ID id)
        {
            switch (id)
            {
                case WINDOW_ID.R_URIAGE_MEISAIHYOU:             // R358(売上明細表)
                case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:           // R362(支払明細表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:    // R355(売上／支払明細表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R355_R358_R362-Form.xml";

                    break;
                case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:            // R359(売上集計表)
                case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:          // R363(支払集計表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:   // R356(売上／支払集計表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R356_R359_R363-Form.xml";

                    break;
                case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:            // R366(入金明細表)
                case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:           // R373(出金明細表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R366_R373-Form.xml";

                    break;
                case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:            // R367(入金集計表)
                case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:           // R374(出金集計表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R367_R374-Form.xml";

                    break;
                case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:            // R379(請求明細表)
                case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:     // R384(支払明細明細表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R379_R384-Form.xml";

                    break;
                case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:         // R369(未入金一覧表)
                case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:         // R376(未出金一覧表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R369_R376-Form.xml";

                    break;
                case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:     // R370(入金予定一覧表)
                case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:     // R377(出金予定一覧表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R370_R377-Form.xml";

                    break;
                case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R432-Form.xml";

                    break;
                case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R433-Form.xml";

                    break;
                case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R434-Form.xml";

                    break;
                case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:           // R342 受付明細表

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R342_R351_R398-Form.xml";

                    break;
                case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:            // R398 運賃明細表

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R342_R351_R398-Form.xml";

                    break;
                case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:            // R351 計量明細表

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R342_R351_R398-Form.xml";

                    break;
                case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:           // R352 計量集計表

                    // 帳票出力フルパスフォーム名
                    this.OutputFormFullPathName = ConstDefaultTemplateFolder + "R352-Form.xml";

                    break;

                default:
                    break;
            }

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        /// <summary>共通帳票作成処理を実行する</summary>
        private void MakeCommonChouhyou()
        {
            switch (this.WindowID)
            {
                case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:    // R355(売上／支払明細表)
                case WINDOW_ID.R_URIAGE_MEISAIHYOU:             // R358(売上明細表)
                case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:           // R362(支払明細表)
                    this.CommonChouhyou = new CommonChouhyouR355_R358_R362(this.WindowID);

                    break;
                case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:   // R356(売上／支払集計表)
                case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:            // R359(売上集計表)
                case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:          // R363(支払集計表)
                    this.CommonChouhyou = new CommonChouhyouR356_R359_R363(this.WindowID);

                    break;
                case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:            // R366(入金明細表)
                case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:           // R373(出金明細表)
                    this.CommonChouhyou = new CommonChouhyouR366_R373(this.WindowID);

                    break;
                case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:            // R367(入金集計表)
                case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:           // R374(出金集計表)
                    this.CommonChouhyou = new CommonChouhyouR367_R374(this.WindowID);

                    break;
                case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:            // R379(請求明細表)
                case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:     // R384(支払明細明細表)
                    this.CommonChouhyou = new CommonChouhyouR379_R384(this.WindowID);

                    break;
                case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:         // R369(未入金一覧表)
                case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:         // R376(未出金一覧表)
                    this.CommonChouhyou = new CommonChouhyouR369_R376(this.WindowID);

                    break;
                case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:     // R370(入金予定一覧表)
                case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:     // R377(出金予定一覧表)
                    this.CommonChouhyou = new CommonChouhyouR370_R377(this.WindowID);

                    break;
                case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_SUIIHYOU:          // R432(売上・支払（全て）推移表)
                    this.CommonChouhyou = new CommonChouhyouR432(this.WindowID);

                    break;
                case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_JYUNNIHYOU:        // R433(売上・支払（全て）順位表)
                    this.CommonChouhyou = new CommonChouhyouR433(this.WindowID);

                    break;
                case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_ZENNEN_TAIHIHYOU:  // R434(売上・支払（全て）前年対比表)
                    this.CommonChouhyou = new CommonChouhyouR434(this.WindowID);

                    break;
                case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:           // R342 受付明細表
                    this.CommonChouhyou = new CommonChouhyouR342(this.WindowID);

                    break;
                case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:            // R398 運賃明細表
                    this.CommonChouhyou = new CommonChouhyouR398(this.WindowID);

                    break;
                case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:            // R351 計量明細表
                    this.CommonChouhyou = new CommonChouhyouR351(this.WindowID);

                    break;
                case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:           // R352 計量集計表
                    this.CommonChouhyou = new CommonChouhyouR352(this.WindowID);

                    break;
            }
        }

        /// <summary>明細表・集計表条件指定画面の表示処理を実行する</summary>
        /// <param name="mode">画面編集モード</param>
        private void DisplayInfoSetting(G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE mode)
        {
            int index = this.customListBox.SelectedIndex;
            long systemID = -1;
            int sequenceNo = 1;
            string patternName = string.Empty;
            string createUser = string.Empty;
            string createPC = string.Empty;
            DateTime createDate = DateTime.Now;
            object timeStamp = new byte[8];

            // 共通帳票作成処理
            this.MakeCommonChouhyou();

            LogicClass logicClass = (LogicClass)this.logic;
            SListColumnSelectLogic summaryListColumnSelectLogic = (SListColumnSelectLogic)logicClass.SummaryListColumnSelectLogic;
            summaryListColumnSelectLogic.GetOutputSelectEnableItems();

            if (mode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify)
            {   // 編集モード
                ((LogicClass)this.logic).GetPatternInfo(index, ref systemID, ref sequenceNo, ref patternName, ref createUser, ref createPC, ref createDate, ref timeStamp);
                this.SystemID = systemID;
                this.SequenceID = sequenceNo;
                this.PatternName = patternName;

                // 作成者名
                this.CreateUser = createUser;

                // 作成ＰＣ
                this.CreatePC = createPC;

                // 作成日付
                this.CreateDate = createDate;

                // タイムスタンプ
                this.TimeStamp = timeStamp;
            }
            else if (mode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
            {   // 新規編集モード
                ((LogicClass)this.logic).GetPatternInfo(index, ref systemID, ref sequenceNo, ref patternName, ref createUser, ref createPC, ref createDate, ref timeStamp);
                this.SystemID = systemID;
                this.SequenceID = sequenceNo;
                this.PatternName = patternName;

                // 作成者名
                this.CreateUser = createUser;

                // 作成ＰＣ
                this.CreatePC = createPC;

                // 作成日付
                this.CreateDate = createDate;

                // タイムスタンプ
                this.TimeStamp = timeStamp;
            }
            else
            {   // 新規モード
                this.SystemID = -1;
                this.SequenceID = -1;

                // 作成者名
                this.CreateUser = string.Empty;

                // 作成ＰＣ
                this.CreatePC = string.Empty;

                // 作成日付
                this.CreateDate = DateTime.Now;

                // タイムスタンプ
                this.TimeStamp = new byte[8];
            }

            string sql = string.Empty;
            DataTable dataTable;
            DataTable dataTableTmp;

            MListPatternLogic masterListPatternLogic = (MListPatternLogic)logicClass.MasterListPatternLogic;
            MListPatternFillCondLogic masterListPatternFillCondLogic = (MListPatternFillCondLogic)logicClass.MasterListPatternFillCondLogic;
            MListPatternColumnLogic masterListPatternColumnLogic = (MListPatternColumnLogic)logicClass.MasterListPatternColumnLogic;

            if (mode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify || mode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify)
            {   // 新規編集モード又は編集モード

                // 帳票名
                if (mode == G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify)
                {   // 新規編集モード
                    this.CommonChouhyou.Name = string.Empty;
                }
                else
                {   // 編集モード
                    this.CommonChouhyou.Name = this.PatternName;
                }

                // 集計項目等
                dataTable = masterListPatternFillCondLogic.GetMasterListPatternFillCondData();

                index = dataTable.Columns.IndexOf("FILL_COND_DATE_KBN");
                this.CommonChouhyou.KikanShiteiType = (CommonChouhyouBase.KIKAN_SHITEI_TYPE)(short)dataTable.Rows[0].ItemArray[index];
                index = dataTable.Columns.IndexOf("FILL_COND_DATE_BEGIN");
                if (DBNull.Value.Equals(dataTable.Rows[0].ItemArray[index]))
                {   // NULL
                    this.CommonChouhyou.DateTimeStart = new DateTime(0);
                }
                else
                {
                    this.CommonChouhyou.DateTimeStart = (DateTime)dataTable.Rows[0].ItemArray[index];
                }

                index = dataTable.Columns.IndexOf("FILL_COND_DATE_END");
                if (DBNull.Value.Equals(dataTable.Rows[0].ItemArray[index]))
                {   // NULL
                    this.CommonChouhyou.DateTimeEnd = new DateTime(0);
                }
                else
                {
                    this.CommonChouhyou.DateTimeEnd = (DateTime)dataTable.Rows[0].ItemArray[index];
                }

                // 拠点
                index = dataTable.Columns.IndexOf("FILL_COND_KYOTEN_CD");
                this.CommonChouhyou.KyotenCode = ((short)dataTable.Rows[0].ItemArray[index]).ToString();

                if (!this.CommonChouhyou.KyotenCode.Equals(string.Empty))
                {
                    sql = "select M_KYOTEN.KYOTEN_NAME_RYAKU from M_KYOTEN where M_KYOTEN.KYOTEN_CD = " + this.CommonChouhyou.KyotenCode;
                    dataTableTmp = masterListPatternLogic.Dao.GetDateForStringSql(sql);
                    this.CommonChouhyou.KyotenCodeName = (string)dataTableTmp.Rows[0].ItemArray[0];
                }
                else
                {
                    this.CommonChouhyou.KyotenCodeName = string.Empty;
                }

                // 伝票種類
                index = dataTable.Columns.IndexOf("FILL_COND_DENPYOU_SBT");
                short denpyouSyurui = (short)dataTable.Rows[0].ItemArray[index];
                if (denpyouSyurui > 10)
                {   // 伝票区分有
                    denpyouSyurui -= 10;

                    this.CommonChouhyou.IsDenpyouSyuruiGroupKubun = true;
                }

                this.CommonChouhyou.DenpyouSyurui = (CommonChouhyouBase.DENPYOU_SYURUI)denpyouSyurui;

                // 伝票区分
                index = dataTable.Columns.IndexOf("FILL_COND_DENPYOU_KBN");
                this.CommonChouhyou.DenpyouKubun = (CommonChouhyouBase.DENPYOU_KUBUN)(short)dataTable.Rows[0].ItemArray[index];

                string masterTableID;
                string fieldCD;
                string fieldCDName;
                string codeStart = string.Empty;
                string codeEnd = string.Empty;
                string fieldName = string.Empty;
                int fillCondID = -1;

                for (int i = 0; i < 4; i++)
                {
                    fieldName = string.Format("FILL_COND_ID_{0}", i + 1);
                    index = dataTable.Columns.IndexOf(fieldName);
                    fillCondID = (int)dataTable.Rows[0].ItemArray[index];

                    switch (i)
                    {
                        case 0:
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].Type = (SYUKEUKOMOKU_TYPE)fillCondID;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_BEGIN_1");
                            codeStart = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeStart = codeStart;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_END_1");
                            codeEnd = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeEnd = codeEnd;

                            break;
                        case 1:
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].Type = (SYUKEUKOMOKU_TYPE)fillCondID;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_BEGIN_2");
                            codeStart = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeStart = codeStart;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_END_2");
                            codeEnd = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeEnd = codeEnd;

                            break;
                        case 2:
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].Type = (SYUKEUKOMOKU_TYPE)fillCondID;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_BEGIN_3");
                            codeStart = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeStart = codeStart;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_END_3");
                            codeEnd = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeEnd = codeEnd;

                            break;
                        case 3:
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].Type = (SYUKEUKOMOKU_TYPE)fillCondID;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_BEGIN_4");
                            codeStart = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeStart = codeStart;
                            index = dataTable.Columns.IndexOf("FILL_COND_CD_END_4");
                            codeEnd = (string)dataTable.Rows[0].ItemArray[index];
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeEnd = codeEnd;

                            break;
                    }

                    masterTableID = this.CommonChouhyou.SyuukeiKomokuList[fillCondID].MasterTableID.ToString();
                    fieldCD = this.CommonChouhyou.SyuukeiKomokuList[fillCondID].FieldCD;
                    fieldCDName = this.CommonChouhyou.SyuukeiKomokuList[fillCondID].FieldCDName;

                    if (!masterTableID.Equals("NONE"))
                    {
                        if (!codeStart.Equals(string.Empty))
                        {
                            sql = string.Format("select {0}.{1} from {2} where {3}.{4} = '{5}'", masterTableID, fieldCDName, masterTableID, masterTableID, fieldCD, codeStart);
                            dataTableTmp = masterListPatternLogic.Dao.GetDateForStringSql(sql);
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeStartName = (string)dataTableTmp.Rows[0].ItemArray[0];
                        }

                        if (!codeEnd.Equals(string.Empty))
                        {
                            sql = string.Format("select {0}.{1} from {2} where {3}.{4} = '{5}'", masterTableID, fieldCDName, masterTableID, masterTableID, fieldCD, codeEnd);
                            dataTableTmp = masterListPatternLogic.Dao.GetDateForStringSql(sql);
                            this.CommonChouhyou.SyuukeiKomokuList[fillCondID].SyuukeiKoumokuHani.CodeEndName = (string)dataTableTmp.Rows[0].ItemArray[0];
                        }
                    }

                    this.CommonChouhyou.SelectSyuukeiKoumokuList[i] = fillCondID;
                }

                // 出力選択項目（伝票＋明細）
                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = null;
                int rowNo = -1;
                bool isMeisai = false;
                int koumokuID = -1;
                bool isSearch = false;

                dataTable = masterListPatternColumnLogic.GetMasterListPatternColumnData();
                this.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Clear();

                // 伝票
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    index = dataTable.Columns.IndexOf("ROW_NO");
                    rowNo = (int)(short)dataTable.Rows[i].ItemArray[index];

                    index = dataTable.Columns.IndexOf("DETAIL_KBN");
                    isMeisai = (bool)dataTable.Rows[i].ItemArray[index];
                    if (isMeisai)
                    {   // 明細
                        continue;
                    }

                    index = dataTable.Columns.IndexOf("KOUMOKU_ID");
                    koumokuID = (int)dataTable.Rows[i].ItemArray[index];

                    for (int j = 0; j < this.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.Count; j++)
                    {
                        chouhyouOutKoumokuGroup = this.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList[j];

                        isSearch = false;
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            if (chouhyouOutKoumoku.ID == koumokuID)
                            {
                                isSearch = true;
                                index = j;
                                break;
                            }
                        }

                        if (isSearch)
                        {
                            break;
                        }
                    }

                    // 出力項目（伝票）へ追加
                    this.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList.Add(chouhyouOutKoumokuGroup);

                    // 選択されている出力可能項目（伝票）削除
                    this.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList.RemoveAt(index);
                }

                // 明細
                this.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Clear();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    index = dataTable.Columns.IndexOf("ROW_NO");
                    rowNo = (int)(short)dataTable.Rows[i].ItemArray[index];

                    index = dataTable.Columns.IndexOf("DETAIL_KBN");
                    isMeisai = (bool)dataTable.Rows[i].ItemArray[index];
                    if (!isMeisai)
                    {   // 明細
                        continue;
                    }

                    index = dataTable.Columns.IndexOf("KOUMOKU_ID");
                    koumokuID = (int)dataTable.Rows[i].ItemArray[index];

                    for (int j = 0; j < this.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Count; j++)
                    {
                        chouhyouOutKoumokuGroup = this.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList[j];

                        isSearch = false;
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            if (chouhyouOutKoumoku.ID == koumokuID)
                            {
                                isSearch = true;
                                index = j;
                                break;
                            }
                        }

                        if (isSearch)
                        {
                            break;
                        }
                    }

                    // 出力項目（伝票）へ追加
                    this.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList.Add(chouhyouOutKoumokuGroup);

                    // 選択されている出力可能項目（伝票）削除
                    this.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList.Remove(chouhyouOutKoumokuGroup);
                }
            }
            else
            {   // 新規モード

                // 拠点
                sql = "select M_KYOTEN.KYOTEN_NAME_RYAKU from M_KYOTEN where M_KYOTEN.KYOTEN_CD = " + this.CommonChouhyou.KyotenCode;
                dataTableTmp = masterListPatternLogic.Dao.GetDateForStringSql(sql);
                this.CommonChouhyou.KyotenCodeName = (string)dataTableTmp.Rows[0].ItemArray[0];
            }

            // 帳票出力フルパスフォーム名
            this.CommonChouhyou.OutputFormFullPathName = this.OutputFormFullPathName;

            // 帳票出力フォームレイアウト名
            this.CommonChouhyou.OutputFormLayout = this.OutputFormLayout;
        }

        #region - キー処理 -

        /// <summary>ファンクションボタン（Ｆ２キー：新規）がクリックされた場合の処理を実行する</summary>
        private bool Func2()
        {
            try
            {
                if (!System.IO.File.Exists(this.OutputFormFullPathName))
                {
                    string text = string.Format("フォームファイル[ {0} ]が存在しません。", this.OutputFormFullPathName);
                    MessageBox.Show(text, "フォームパスエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE mode;
                if (this.customListBox.SelectedIndex == 0)
                {
                    // 明細表・集計表条件設定画面の表示（新規）
                    mode = G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.New;
                    this.DisplayInfoSetting(mode);
                }
                else
                {
                    // 明細表・集計表条件設定画面の表示（新規の修正モード）
                    mode = G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.NewModify;
                    this.DisplayInfoSetting(mode);
                }

                var detailForm = new G418_MeisaihyoSyukeihyoJokenShiteiPopupForm(mode, this.SystemID, this.SequenceID, this.PatternName, this.WindowID, this.PatternNameList, this.CreateUser, this.CreatePC, this.CreateDate, this.TimeStamp);
                detailForm.IsOutputPDF = this.IsOutputPDF;
                detailForm.IsMultiRowExist = this.IsMultiRowExist;
                detailForm.CommonChouhyou = this.CommonChouhyou;

                var headerForm = new HeaderBaseForm();
                headerForm.lb_title.Size = new Size(headerForm.lb_title.Size.Width + 150, headerForm.lb_title.Size.Height);
                headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.WindowID) + " - 条件指定";
                headerForm.windowTypeLabel.Visible = false;
                var displayForm = new BusinessBaseForm(detailForm, headerForm);
                displayForm.Text = headerForm.lb_title.Text;

                displayForm.MinimizeBox = false;
                displayForm.MaximizeBox = false;

                // 共通画面を起動する
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(detailForm);
                if (!isExistForm)
                {
                    displayForm.IsPopupType = true;
                    detailForm.Location = new Point(10, 147);
                    displayForm.ShowDialog();
                }

                if (detailForm.IsRegist)
                {   // 登録した
                    LogicClass logicClass = (LogicClass)this.logic;
                    MListPatternLogic masterListPatternLogic = (MListPatternLogic)logicClass.MasterListPatternLogic;
                    masterListPatternLogic.PatternListItems.Clear();
                    this.customListBox.Items.Clear();

                    masterListPatternLogic.GetPatternListItems();
                }

                displayForm.Dispose();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func2", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func2", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>ファンクションボタン（Ｆ３キー：修正）がクリックされた場合の処理を実行する</summary>
        private bool Func3()
        {
            try
            {

                if (!System.IO.File.Exists(this.OutputFormFullPathName))
                {
                    string text = string.Format("フォームファイル[ {0} ]が存在しません。", this.OutputFormFullPathName);
                    MessageBox.Show(text, "フォームパスエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                int index = this.customListBox.SelectedIndex;
                if (index < 1)
                {   // 項目が選択されていない

                    // {0}を選択してください。
                    this.msgLogic.MessageBoxShow("E051", new string[] { "パターン一覧" });

                    return false;
                }

                G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE mode = G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify;

                // 明細表・集計表条件設定画面の表示（修正）
                this.DisplayInfoSetting(mode);

                var detailForm = new G418_MeisaihyoSyukeihyoJokenShiteiPopupForm(mode, this.SystemID, this.SequenceID, this.PatternName, this.WindowID, this.PatternNameList, this.CreateUser, this.CreatePC, this.CreateDate, this.TimeStamp);
                detailForm.IsOutputPDF = this.IsOutputPDF;
                detailForm.IsMultiRowExist = this.IsMultiRowExist;
                detailForm.CommonChouhyou = this.CommonChouhyou;

                // 初期化処理
                //detailForm.Initialize();

                var headerForm = new HeaderBaseForm();

                headerForm.lb_title.Size = new Size(headerForm.lb_title.Size.Width + 150, headerForm.lb_title.Size.Height);
                headerForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.WindowID) + " - 条件指定";
                headerForm.windowTypeLabel.Visible = false;
                var displayForm = new BusinessBaseForm(detailForm, headerForm);
                displayForm.Text = headerForm.lb_title.Text;

                displayForm.MinimizeBox = false;
                displayForm.MaximizeBox = false;

                // 共通画面を起動する
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(detailForm);
                if (!isExistForm)
                {
                    displayForm.IsPopupType = true;
                    detailForm.Location = new Point(10, 147);
                    displayForm.ShowDialog();
                }

                if (detailForm.IsRegist)
                {   // 登録した
                    LogicClass logicClass = (LogicClass)this.logic;
                    MListPatternLogic masterListPatternLogic = (MListPatternLogic)logicClass.MasterListPatternLogic;
                    masterListPatternLogic.PatternListItems.Clear();
                    this.customListBox.Items.Clear();

                    masterListPatternLogic.GetPatternListItems();
                }

                displayForm.Dispose();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func3", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func3", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>ファンクションボタン（Ｆ４キー：削除）がクリックされた場合の処理を実行する</summary>
        private bool Func4()
        {
            try
            {
                int index = this.customListBox.SelectedIndex;
                if (index < 1)
                {
                    // {0}を選択してください。
                    this.msgLogic.MessageBoxShow("E051", new string[] { "パターン一覧" });

                    return false;
                }

                // 削除してよろしいですか？
                var result = this.msgLogic.MessageBoxShow("C026");
                if (result != DialogResult.Yes)
                {   // はいではない
                    return false;
                }

                index = this.customListBox.SelectedIndex;
                long systemID = -1;
                int sequenceNo = 1;
                string patternName = string.Empty;
                string createUser = string.Empty;
                string createPC = string.Empty;
                DateTime createDate = DateTime.Now;
                object timeStamp = new byte[8];
                ((LogicClass)this.logic).GetPatternInfo(index, ref systemID, ref sequenceNo, ref patternName, ref createUser, ref createPC, ref createDate, ref timeStamp);

                LogicClass logicClass = (LogicClass)this.logic;
                MListPatternLogic masterListPatternLogic = (MListPatternLogic)logicClass.MasterListPatternLogic;
                masterListPatternLogic.Delete(systemID, sequenceNo, patternName, createUser, createPC, createDate, timeStamp);
                masterListPatternLogic.PatternListItems.Clear();
                this.customListBox.Items.Clear();

                masterListPatternLogic.GetPatternListItems();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func4", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func4", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>ファンクションボタン（Ｆ５キー：表示）がクリックされた場合の処理を実行する</summary>
        private bool Func5()
        {
            try
            {
                int index = this.customListBox.SelectedIndex;

                if (index < 1)
                {
                    // {0}を選択してください。
                    this.msgLogic.MessageBoxShow("E051", new string[] { "パターン一覧" });

                    return false;
                }

                G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE mode = G418_MeisaihyoSyukeihyoJokenShiteiPopupForm.DISPLAY_MODE.Modify;

                // 明細表・集計表条件設定画面の表示（修正）
                this.DisplayInfoSetting(mode);

                var detailForm = new G418_MeisaihyoSyukeihyoJokenShiteiPopupForm(mode, this.SystemID, this.SequenceID, this.PatternName, this.WindowID, this.PatternNameList, this.CreateUser, this.CreatePC, this.CreateDate, this.TimeStamp);

                detailForm.IsOutputPDF = this.IsOutputPDF;
                detailForm.IsMultiRowExist = this.IsMultiRowExist;

                // 印刷日時
                this.CommonChouhyou.DateTimePrint = DateTime.Now;

                detailForm.CommonChouhyou = this.CommonChouhyou;

                // 初期化処理
                detailForm.Initialize();

                // 印刷ポップアップ表示処理
                detailForm.PrintPopup();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Func3", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Func3", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>ファンクションボタン（Ｆ１２キー：閉じる）がクリックされた場合の処理を実行する</summary>
        private void Func12()
        {
            this.Close();
        }

        #endregion - キー処理 -

        /// <summary>画面のロード処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void UIForm_Load(object sender, EventArgs e)
        {
            this.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.WindowID);
            this.lb_title.Text = this.Text;
        }

        #region - キー処理 -

        /// <summary>ファンクションボタン（Ｆ２キー：新規）がクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonFunc2_Click(object sender, EventArgs e)
        {
            this.Func2();
        }

        /// <summary>ファンクションボタン（Ｆ３キー：修正）がクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonFunc3_Click(object sender, EventArgs e)
        {
            this.Func3();
        }

        /// <summary>ファンクションボタン（Ｆ４キー：削除）がクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonFunc4_Click(object sender, EventArgs e)
        {
            this.Func4();
        }

        /// <summary>ファンクションボタン（Ｆ５キー：表示）がクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonFunc5_Click(object sender, EventArgs e)
        {
            this.Func5();
        }

        /// <summary>ファンクションボタン（Ｆ１２キー：閉じる）がクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ButtonFunc12_Click(object sender, EventArgs e)
        {
            this.Func12();
        }

        #endregion - キー処理 -

        /// <summary>フォーム上でキーが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F2:   // 新規
                    if (!this.Func2())
                    {
                        return;
                    }
                    break;
                case Keys.F3:   // 修正
                    if (!this.Func3())
                    {
                        return;
                    }
                    break;
                case Keys.F4:   // 削除
                    if (!this.Func4())
                    {
                        return;
                    }
                    break;
                case Keys.F5:   // 表示
                    if (!this.Func5())
                    {
                        return;
                    }
                    break;
                case Keys.F12:  // 閉じる
                    this.Func12();
                    break;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
