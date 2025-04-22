using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KensakuKyoutsuuPopup.APP;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Class -

    /// <summary>明細表・集計表条件設定詳細画面を表すクラス・コントロール</summary>
    public partial class G418_MeisaihyoSyukeihyoJokenShiteiPopupForm : SuperForm
    {
        #region - Fields -

        /// <summary>最大集計項目を保持するフィールド</summary>
        private const int ConstMaxSyuukeiKoumoku = 4;

        /// <summary>最大出力項目数（デフォルト）を保持するフィールド</summary>
        private const int ConstDefaultMaxOutputItemCount = 8;

        /// <summary>画面ロジック</summary>
        private LogicClass logic;

        /// <summary>メッセージロジックオブジェクトを保持するフィールド</summary>
        private MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>現在選択されている集計項目インデックスを保持するフィールド</summary>
        private int[] selectSyuukeiKoumokuIndex = new int[ConstMaxSyuukeiKoumoku]
        {
            0, 0, 0, 0
        };

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="G418_MeisaihyoSyukeihyoJokenShiteiPopupForm" /> class.</summary>
        /// <param name="displayMode">画面編集モード</param>
        /// <param name="systemID">システムＩＤ</param>
        /// <param name="sequenceID">シーケンスＩＤを表す数値</param>
        /// <param name="chouhyouName">帳票名を表す文字列</param>
        /// <param name="windowID">ウィンドウＩＤ</param>
        /// <param name="patternNameList">既に登録されている帳票（パターン）名リスト</param>
        public G418_MeisaihyoSyukeihyoJokenShiteiPopupForm(
            DISPLAY_MODE displayMode,
            long systemID,
            int sequenceID,
            string chouhyouName,
            WINDOW_ID windowID,
            List<string> patternNameList,
            string createUser,
            string createPC,
            DateTime createDate,
            object timeStamp)
        {
            this.InitializeComponent();

            // MultiRowの存在有無
            this.IsMultiRowExist = true;

            this.PatternNameList = patternNameList;

            // 編集モードの場合は編集元の名前が衝突チェックに引っかからないように帳票パターン名リストから予め削除しておく
            if (displayMode == DISPLAY_MODE.Modify)
            {
                this.PatternNameList.Remove(chouhyouName);
            }

            this.CustomControlSyukeiKomokuList = new CustomControlSyukeiKomoku[ConstMaxSyuukeiKoumoku]
            {
                new CustomControlSyukeiKomoku(
                    this.customPanelSyukeiKomoku1,
                    this.customComboBoxSyukeiKomoku1ID,
                    this.customPanelSyukeiKomoku1StartEnd,
                    this.customAlphaNumTextBoxSyukeiKomoku1StartCD,
                    this.customTextBoxSyukeiKomoku1StartMeisho,
                    this.customPopupOpenButtonSyukeiKomoku1StartMeishoSearch,
                    this.customAlphaNumTextBoxSyukeiKomoku1EndCD,
                    this.customTextBoxSyukeiKomoku1EndMeisho,
                    this.customPopupOpenButtonSyukeiKomoku1EndMeishoSearch),
                
                new CustomControlSyukeiKomoku(
                    this.customPanelSyukeiKomoku2,
                    this.customComboBoxSyukeiKomoku2ID,
                    this.customPanelSyukeiKomoku2StartEnd,
                    this.customAlphaNumTextBoxSyukeiKomoku2StartCD,
                    this.customTextBoxSyukeiKomoku2StartMeisho,
                    this.customPopupOpenButtonSyukeiKomoku2StartMeishoSearch,
                    this.customAlphaNumTextBoxSyukeiKomoku2EndCD,
                    this.customTextBoxSyukeiKomoku2EndMeisho,
                    this.customPopupOpenButtonSyukeiKomoku2EndMeishoSearch),

                new CustomControlSyukeiKomoku(
                    this.customPanelSyukeiKomoku3,
                    this.customComboBoxSyukeiKomoku3ID,
                    this.customPanelSyukeiKomoku3StartEnd,
                    this.customAlphaNumTextBoxSyukeiKomoku3StartCD,
                    this.customTextBoxSyukeiKomoku3StartMeisho,
                    this.customPopupOpenButtonSyukeiKomoku3StartMeishoSearch,
                    this.customAlphaNumTextBoxSyukeiKomoku3EndCD,
                    this.customTextBoxSyukeiKomoku3EndMeisho,
                    this.customPopupOpenButtonSyukeiKomoku3EndMeishoSearch),

                new CustomControlSyukeiKomoku(
                    this.customPanelSyukeiKomoku4,
                    this.customComboBoxSyukeiKomoku4ID,
                    this.customPanelSyukeiKomoku4StartEnd,
                    this.customAlphaNumTextBoxSyukeiKomoku4StartCD,
                    this.customTextBoxSyukeiKomoku4StartMeisho,
                    this.customPopupOpenButtonSyukeiKomoku4StartMeishoSearch,
                    this.customAlphaNumTextBoxSyukeiKomoku4EndCD,
                    this.customTextBoxSyukeiKomoku4EndMeisho,
                    this.customPopupOpenButtonSyukeiKomoku4EndMeishoSearch),
            };

            this.DisplayMode = displayMode;
            this.SystemID = systemID;
            this.SequenceID = sequenceID;

            // 帳票名
            this.ChouhyouName = chouhyouName;

            // 作成者名
            this.CreateUser = createUser;

            // 作成ＰＣ
            this.CreatePC = createPC;

            // 作成日付
            this.CreateDate = createDate;

            // タイムスタンプ
            this.TimeStamp = timeStamp;

            this.WindowId = windowID;

            // タイトル編集する
            this.isHeaderTitleModify = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        #endregion - Constructors -

        #region - Enum -

        /// <summary>画面編集モードに関する列挙型</summary>
        public enum DISPLAY_MODE
        {
            /// <summary>新規</summary>
            New,

            /// <summary>新規の編集</summary>
            NewModify,

            /// <summary>修正</summary>
            Modify
        }

        #endregion - Enum -

        #region - Properties -

        /// <summary>キャプション名を保持するプロパティ</summary>
        public string Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        /// <summary>親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>帳票出力内容を保持するプロパティ</summary>
        public CommonChouhyouBase CommonChouhyou { get; set; }

        /// <summary>最大出力項目数</summary>
        public int MaxOutputCount { get; set; }

        /// <summary>登録有無の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：登録完了、偽の場合：未登録</remarks>
        public bool IsRegist { get; private set; }

        /// <summary>エクセル出力する際のエクセル出力をするかＰＤＦ出力するかの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ＰＤＦで出力、偽の場合：Ｅｘｃｅｌで出力</remarks>
        public bool IsOutputPDF { get; set; }

        /// <summary>MultiRowの存在有無の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：MultiRowが存在、偽の場合：MultiRowが存在しない</remarks>
        public bool IsMultiRowExist { get; set; }

        /// <summary>集計項目コントロールを保持するプロパティ</summary>
        internal CustomControlSyukeiKomoku[] CustomControlSyukeiKomokuList { get; set; }

        /// <summary>表示（編集）モードを保持するプロパティ</summary>
        internal DISPLAY_MODE DisplayMode { get; set; }

        /// <summary>システムＩＤを保持するプロパティ</summary>
        internal long SystemID { get; set; }

        /// <summary>シーケンスＩＤを保持するプロパティ</summary>
        internal int SequenceID { get; set; }

        /// <summary>帳票名を保持するプロパティ</summary>
        internal string ChouhyouName { get; set; }

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

        /// <summary>初期化処理を実行する</summary>
        public bool Initialize()
        {
            try
            {
                // コントロールの表示状態の更新
                this.UpdateDisplayStatus();

                // 集計項目ポップアップ関連コントロールの結びつきを変える
                for (int i = 0; i < this.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; i++)
                {
                    this.SyukeiKomokuChangePopupCtrlConnetion(i + 1, true);
                }

                // 最大出力項目数
                this.MaxOutputCount = ConstDefaultMaxOutputItemCount;

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Initialize", ex2);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Initialize", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>印刷ポップアップ表示処理を実行する</summary>
        public void PrintPopup()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.logic.Func7(false);

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
                throw e;
            }
        }

        #region - キー処理 -

        /// <summary>Ｆ１キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            this.logic.Func1();
        }

        /// <summary>Ｆ２キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            this.logic.Func2();
        }

        /// <summary>Ｆ３キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc3_Clicked(object sender, EventArgs e)
        {
            this.logic.Func3();
        }

        /// <summary>出力伝票上移動ボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void ButtonSyutsuryokuKanoKomokuDenpyoUeIdo_Click(object sender, EventArgs e)
        {
            if (!this.logic.SyutsuryokuKanoKomokuDenpyoUeIdo())
            {
                return;
            }
        }

        /// <summary>出力伝票項目下移動ボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void ButtonSyutsuryokuKanoKomokuDenpyoShitaIdo_Click(object sender, EventArgs e)
        {
            if (!this.logic.SyutsuryokuKanoKomokuDenpyoShitaIdo())
            {
                return;
            }
        }

        /// <summary>Ｆ４キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc4_Clicked(object sender, EventArgs e)
        {
            this.logic.Func4();
        }

        /// <summary>Ｆ５キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            this.logic.Func5();
        }

        /// <summary>Ｆ６キーボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc6_Clicked(object sender, EventArgs e)
        {
            this.logic.Func6();
        }

        /// <summary>出力明細上移動ボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void ButtonSyutsuryokuKanoKomokuMeisaiUeIdo_Click(object sender, EventArgs e)
        {
            this.logic.SyutsuryokuKanoKomokuMeisaiUeIdo();
        }

        /// <summary>出力明細項目下移動ボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void ButtonSyutsuryokuKanoKomokuMeisaiShitaIdo_Click(object sender, EventArgs e)
        {
            this.logic.SyutsuryokuKanoKomokuMeisaiShitaIdo();
        }

        /// <summary>Ｆ７キー（表示）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.RegistErrorFlag)
            {
                Cursor.Current = Cursors.Arrow;

                return;
            }

            this.logic.Func7();

            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>Ｆ９キー（登録）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc9_Clicked(object sender, EventArgs e)
        {
            if (this.RegistErrorFlag)
            {
                return;
            }

            if (this.logic.Func9())
            {   // 登録エラー
                return;
            }

            // No.2062
            MessageBoxShowLogic msgcls = new MessageBoxShowLogic();
            msgcls.MessageBoxShow("I001", "登録");

            this.IsRegist = true;

            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        /// <summary>Ｆ１２キー（閉じる）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        #endregion - キー処理 -

        /// <summary>画面Load処理</summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);

                // 伝票種類の表示状態を設定
                if (!this.SettingDenpyouSyurui())
                {
                    return;
                }

                if (!this.logic.WindowInit())
                {
                    return;
                }

                // 初期化処理
                if (!this.Initialize())
                {
                    return;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>集計項目表示状態の更新処理を実行する</summary>
        private void UpdateDisplayStatus()
        {
            try
            {
                //if (this.DisplayMode == DISPLAY_MODE.Modify)
                //{   // 修正モード
                //    //this.customTextBoxChohyoMei.Enabled = false;
                //    this.customTextBoxChohyoMei.Enabled = true;
                //}

                this.customCheckBoxSyuruiShiteiGroupKubun2.Visible = false;

                // 帳票名
                this.customTextBoxChohyoMei.Text = this.CommonChouhyou.Name;

                this.CustomNumericTextBox2HidukeHaniShiteiHoho.Text = ((int)this.CommonChouhyou.KikanShiteiType).ToString();

                if (this.CommonChouhyou.KikanShiteiType == CommonChouhyouBase.KIKAN_SHITEI_TYPE.Shitei)
                {
                    // 期間指定（開始日）
                    if (this.CommonChouhyou.DateTimeStart.Equals(new DateTime(0)))
                    {
                        this.customDateTimePickerHidukeHaniShiteiStart.Value = null;
                    }
                    else
                    {
                        this.customDateTimePickerHidukeHaniShiteiStart.Value = this.CommonChouhyou.DateTimeStart;
                    }

                    // 期間指定（終了日）
                    if (this.CommonChouhyou.DateTimeEnd.Equals(new DateTime(0)))
                    {
                        this.customDateTimePickerHidukeHaniShiteiEnd.Value = null;
                    }
                    else
                    {
                        this.customDateTimePickerHidukeHaniShiteiEnd.Value = this.CommonChouhyou.DateTimeEnd;
                    }
                }
                else
                {
                    // 期間指定（開始日）
                    this.customDateTimePickerHidukeHaniShiteiStart.Value = null;

                    // 期間指定（終了日）
                    this.customDateTimePickerHidukeHaniShiteiEnd.Value = null;
                }

                // 拠点指定コード
                this.CustomNumericTextBox2KyotenShiteiCD.Text = this.CommonChouhyou.KyotenCode;
                // 拠点指定コード名
                this.customTextBoxKyotenShiteiMei.Text = this.CommonChouhyou.KyotenCodeName;

                // 伝票種類指定グループ区分有無
                this.customCheckBoxSyuruiShiteiGroupKubun2.Checked = this.CommonChouhyou.IsDenpyouSyuruiGroupKubun;

                // 伝票種類指定
                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:                     // R358(売上明細表)
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:                   // R362(支払明細表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:            // R355(売上／支払明細表)
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:                    // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:                  // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:           // R356(売上／支払集計表)
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:                    // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:                   // R373(出金明細表)
                    case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:                   // R367(入金集計表)
                    case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:                  // R374(出金集計表)
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:                    // R379(請求明細表)
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:             // R384(支払明細明細表)
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:                 // R369(未入金一覧表)
                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:                 // R376(未出金一覧表)
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:             // R370(入金予定一覧表)
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:             // R377(出金予定一覧表)
                        this.CustomNumericTextBox2DenpyoSyuruiShitei.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)

                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)

                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();

                        this.customCheckBoxSyuruiShiteiGroupKubun2.Visible = true;
                        if (this.CommonChouhyou.DenpyouSyurui == CommonChouhyouBase.DENPYOU_SYURUI.Subete)
                        {   // 全て
                            this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = true;
                        }
                        else
                        {
                            this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;
                        }

                        break;

                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Visible = true;
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = true;

                        break;

                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Visible = true;
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                        break;
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:                   // R342 受付明細表
                        this.CustomNumericTextBox2DenpyoSyuruiShitei3.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                    // R398 運賃明細表
                        this.CustomNumericTextBox2DenpyoSyuruiShitei4.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();

                        break;
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:                    // R351 計量明細表
                    case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:                   // R352 計量集計表
                        this.CustomNumericTextBox2DenpyoSyuruiShitei.Text = ((int)this.CommonChouhyou.DenpyouSyurui).ToString();

                        break;

                    default:
                        break;
                }

                // 伝票区分指定
                this.CustomNumericTextBox2DenpyoKubunShitei.Text = ((int)this.CommonChouhyou.DenpyouKubun).ToString();
                this.customPanelDenpyoKubunShitei.Enabled = this.CommonChouhyou.IsDenpyouKubunEnable;
                if (this.WindowId == WINDOW_ID.R_KEIRYOU_SUIIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // R432(計量推移表・計量順位表・計量前年対比表)

                    if (this.CommonChouhyou.DenpyouKubun == CommonChouhyouBase.DENPYOU_KUBUN.Subete)
                    {   // 伝票区分指定が全て

                        this.customPanelDenpyoSyuruiShitei.Enabled = true;

                        this.customCheckBoxSyuruiShiteiGroupKubun2.Visible = true;
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = true;

                        this.customPanelDenpyoSyuruiShitei.Enabled = true;

                        this.customRadioButtonDenpyoSyuruiShitei2_1.Enabled = false;    // 受入
                        this.customRadioButtonDenpyoSyuruiShitei2_2.Enabled = false;    // 出荷
                        this.customRadioButtonDenpyoSyuruiShitei2_3.Enabled = false;    // 売上／支払
                        this.customRadioButtonDenpyoSyuruiShitei2_4.Enabled = true;     // 全て

                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.RangeSetting.Min = 4;
                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.Tag = "【4】のいずれかで入力してください";
                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.LinkedRadioButtonArray = new string[] { "customRadioButtonDenpyoSyuruiShitei2_4" };
                        this.CustomNumericTextBox2DenpyoSyuruiShitei2.CharacterLimitList = new char[] { '4' };
                    }
                    else
                    {   // 伝票区分指定が全てでない
                        this.customPanelDenpyoSyuruiShitei.Enabled = false;
                    }
                }
                else
                {
                    this.customPanelDenpyoSyuruiShitei.Enabled = this.CommonChouhyou.IsDenpyouSyuruiEnable;
                }

                SyuukeiKoumoku syuukeiKoumoku;
                int selectSyuukeiIndex;
                for (int i = 0; i < this.CustomControlSyukeiKomokuList.Length; i++)
                {
                    if (i >= this.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount)
                    {
                        continue;
                    }

                    // 集計項目パネルの有効・無効
                    this.CustomControlSyukeiKomokuList[i].PanelSyukeiKomoku.Enabled = true;

                    // 集計項目ＩＤ登録
                    for (int j = 0; j < this.CommonChouhyou.SelectEnableSyuukeiKoumokuList.Count; j++)
                    {
                        // 選択可能な集計項目番号
                        int index = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[j];

                        // 集計項目
                        syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[index];

                        // コンボボックスに追加
                        this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.Items.Add(syuukeiKoumoku.Name);
                    }

                    int selectedIndex = 0;
                    for (int j = 0; j < this.CommonChouhyou.SelectEnableSyuukeiKoumokuList.Count; j++)
                    {
                        selectSyuukeiIndex = this.CommonChouhyou.SelectSyuukeiKoumokuList[i];

                        // 集計項目
                        syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[selectSyuukeiIndex];

                        if (syuukeiKoumoku.Name.Equals(this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.Items[j].ToString()))
                        {
                            selectedIndex = j;
                            break;
                        }
                    }

                    // 選択されている集計項目
                    selectSyuukeiIndex = this.CommonChouhyou.SelectSyuukeiKoumokuList[i];

                    for (int ii = 0; ii < 2; ii++)
                    {   // フレームワーク側のバグっぽいので２回まわす
                        this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex = selectedIndex;
                    }

                    // 集計項目
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[selectSyuukeiIndex];
                    SyuukeiKoumokuHani syuukeiKoumokuHani = syuukeiKoumoku.SyuukeiKoumokuHani;

                    this.CustomControlSyukeiKomokuList[i].CustomPanelSyukeiKomokuStartEnd.Enabled = syuukeiKoumoku.IsSyuukeiKoumokuHani;
                    this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text = syuukeiKoumokuHani.CodeStart;
                    this.CustomControlSyukeiKomokuList[i].TextBoxSyukeiKomokuStartMeisho.Text = syuukeiKoumokuHani.CodeStartName;
                    this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text = syuukeiKoumokuHani.CodeEnd;
                    this.CustomControlSyukeiKomokuList[i].TextBoxSyukeiKomokuEndMeisho.Text = syuukeiKoumokuHani.CodeEndName;
                }

                ChouhyouOutKoumoku chouhyouOutKoumoku;

                // 出力可能項目（伝票）
                this.customPanelSyutsuryokuKanoKomokuDenpyo.Enabled = this.CommonChouhyou.OutEnableKoumokuDenpyou;

                if (this.CommonChouhyou.OutEnableKoumokuDenpyou)
                {   // 出力項目（伝票）
                    foreach (ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup in this.CommonChouhyou.SelectEnableChouhyouOutKoumokuDenpyouList)
                    {
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            this.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Add(chouhyouOutKoumoku.Name);

                            break;
                        }
                    }

                    if (this.customListBoxSyutsuryokuKanoKomokuDenpyo.Items.Count != 0)
                    {
                        this.customListBoxSyutsuryokuKanoKomokuDenpyo.SelectedIndex = 0;
                    }

                    foreach (ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup in this.CommonChouhyou.SelectChouhyouOutKoumokuDepyouList)
                    {
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            this.customListBoxSyutsuryokuKomokuDenpyo.Items.Add(chouhyouOutKoumoku.Name);

                            break;
                        }
                    }

                    if (this.customListBoxSyutsuryokuKomokuDenpyo.Items.Count != 0)
                    {
                        this.customListBoxSyutsuryokuKomokuDenpyo.SelectedIndex = 0;
                    }
                }

                // 出力可能項目（明細）
                this.customPanelSyutsuryokuKanoKomokuMeisai.Enabled = this.CommonChouhyou.OutEnableKoumokuMeisai;

                if (this.CommonChouhyou.OutEnableKoumokuMeisai)
                {   // 出力可能（明細）
                    foreach (ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup in this.CommonChouhyou.SelectEnableChouhyouOutKoumokuMeisaiList)
                    {
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            this.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Add(chouhyouOutKoumoku.Name);

                            break;
                        }
                    }

                    if (this.customListBoxSyutsuryokuKanoKomokuMeisai.Items.Count != 0)
                    {
                        this.customListBoxSyutsuryokuKanoKomokuMeisai.SelectedIndex = 0;
                    }

                    foreach (ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup in this.CommonChouhyou.SelectChouhyouOutKoumokuMeisaiList)
                    {
                        for (int k = 0; k < chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList.Length; k++)
                        {
                            chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[k];
                            if (chouhyouOutKoumoku == null)
                            {
                                continue;
                            }

                            this.customListBoxSyutsuryokuKomokuMeisai.Items.Add(chouhyouOutKoumoku.Name);

                            break;
                        }
                    }

                    if (this.customListBoxSyutsuryokuKomokuMeisai.Items.Count != 0)
                    {
                        this.customListBoxSyutsuryokuKomokuMeisai.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>直接開始コード又は終了コード用のSQL文発行処理を実行する</summary>
        /// <param name="selectSyukeiNo">集計項目番号を表す値</param>
        /// <param name="isStartCode">開始コードか否かを表す値</param>
        /// <param name="text">テキストを表す文字列</param>
        private void DirectSendSQL(int selectSyukeiNo, bool isStartCode, string text)
        {
            try
            {
                if (text.Equals(string.Empty))
                {
                    return;
                }

                LogicClass logicClass = (LogicClass)this.logic;
                MListPatternLogic masterListPatternLogic = (MListPatternLogic)logicClass.MasterListPatternLogic;
                DataTable dataTableTmp = null;

                string sql = string.Empty;

                int index;

                // 選択されているアイテムインデックス取得
                int selectIndex = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].ComboBoxSyukeiKomokuID.SelectedIndex;

                // 選択可能な集計項目番号
                int itemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[selectIndex];

                // 集計項目
                SyuukeiKoumoku syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[itemNo];

                if (isStartCode)
                {   // 開始コード側
                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UntenshaBetsu)
                    {
                        sql = string.Format("select M_SHAIN.SHAIN_CD AS CD, M_SHAIN.SHAIN_NAME_RYAKU AS NAME FROM M_SHAIN WHERE SHAIN_CD = {0} AND M_SHAIN.UNTEN_KBN = CONVERT(bit,'True')  group by M_SHAIN.SHAIN_CD,M_SHAIN.SHAIN_NAME_RYAKU", text);

                        dataTableTmp = masterListPatternLogic.MasterListPatternDao.GetDateForStringSql(sql);

                        if (dataTableTmp.Rows.Count != 0)
                        {   // 該当コードが有
                            index = dataTableTmp.Columns.IndexOf("NAME");

                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.Text = (string)dataTableTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // 該当コードが無
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.Focus();

                            if (this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.BackColor != Color.FromArgb(255, 100, 100))
                            {
                                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.BackColor = Color.FromArgb(255, 100, 100);
                                MessageBox.Show("運転者コードに存在しないエラーが入力されました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.Text = string.Empty;
                        }
                    }
                }
                else
                {   // 終了コード側
                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UntenshaBetsu)
                    {
                        sql = string.Format("select M_SHAIN.SHAIN_CD AS CD, M_SHAIN.SHAIN_NAME_RYAKU AS NAME FROM M_SHAIN WHERE SHAIN_CD = {0} AND M_SHAIN.UNTEN_KBN = CONVERT(bit,'True') group by M_SHAIN.SHAIN_CD,M_SHAIN.SHAIN_NAME_RYAKU", text);

                        dataTableTmp = masterListPatternLogic.MasterListPatternDao.GetDateForStringSql(sql);

                        if (dataTableTmp.Rows.Count != 0)
                        {   // 該当コードが有
                            index = dataTableTmp.Columns.IndexOf("NAME");

                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.Text = (string)dataTableTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // 該当コードが無
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.Focus();

                            if (this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.BackColor != Color.FromArgb(255, 100, 100))
                            {
                                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.BackColor = Color.FromArgb(255, 100, 100);
                                MessageBox.Show("運転者コードに存在しないエラーが入力されました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>集計項目ポップアップ関連コントロールの結びつきを変える処理を実行する</summary>
        private void SyukeiKomokuChangePopupCtrlConnetion(int selectSyukeiNo, bool isInit = false)
        {
            try
            {
                int index;

                // 選択されているアイテムインデックス取得
                int selectIndex = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].ComboBoxSyukeiKomokuID.SelectedIndex;

                // 選択可能な集計項目番号
                int itemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[selectIndex];

                // 集計項目
                SyuukeiKoumoku syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[itemNo];

                // 開始側
                string ctrlStartCDName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                string ctrlStartCDRyakuName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.Name;
                string buttonStartName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.Name;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.LostFocus -= (sender, e) =>
                {
                };

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.LostFocus += (sender, e) =>
                {
                    // 直接開始コード又は終了コード用のSQL文発行処理
                    this.DirectSendSQL(selectSyukeiNo, true, this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.Text);
                };

                // 終了側
                string ctrlEndCDName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                string ctrlEndCDRyakuName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.Name;
                string buttonEndName = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.Name;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.LostFocus -= (sender, e) =>
                {
                };

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.LostFocus += (sender, e) =>
                {
                    // 直接開始コード又は終了コード用のSQL文発行処理
                    this.DirectSendSQL(selectSyukeiNo, false, this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.Text);
                };

                if (!isInit)
                {   // 初期化時でない
                    int id;
                    for (int i = 0; i < this.CommonChouhyou.SelectEnableSyuukeiKoumokuGroupCount; i++)
                    {
                        id = this.CommonChouhyou.SelectSyuukeiKoumokuList[i];

                        SyuukeiKoumoku syuukeiKoumoku2 = this.CommonChouhyou.SyuukeiKomokuList[id];

                        if (syuukeiKoumoku2.Type == SYUKEUKOMOKU_TYPE.None)
                        {
                            continue;
                        }

                        if (syuukeiKoumoku.Type == syuukeiKoumoku2.Type)
                        {
                            if ((selectSyukeiNo - 1) != i)
                            {   // 異なるコンボボックスで集計項目が同じものが選択された場合

                                // 同じ集計項目は複数回指定できません。集計項目を変更してください
                                this.msgLogic.MessageBoxShow("E083", new string[] { string.Empty });

                                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].ComboBoxSyukeiKomokuID.SelectedIndex = this.selectSyuukeiKoumokuIndex[selectSyukeiNo - 1];

                                return;
                            }
                            else
                            {   // 同一コンボボックスで集計項目が同じものが選択された場合
                                return;
                            }
                        }
                    }

                    index = this.selectSyuukeiKoumokuIndex[selectSyukeiNo - 1];
                    this.CommonChouhyou.SyuukeiKomokuList[index].SyuukeiKoumokuHani.CodeStart = string.Empty;
                    this.CommonChouhyou.SyuukeiKomokuList[index].SyuukeiKoumokuHani.CodeStartName = string.Empty;
                    this.CommonChouhyou.SyuukeiKomokuList[index].SyuukeiKoumokuHani.CodeEnd = string.Empty;
                    this.CommonChouhyou.SyuukeiKomokuList[index].SyuukeiKoumokuHani.CodeEndName = string.Empty;

                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.Text = string.Empty;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.Text = string.Empty;

                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.Text = string.Empty;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.Text = string.Empty;
                }

                // 集計項目範囲の有効・無効
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].CustomPanelSyukeiKomokuStartEnd.Enabled = syuukeiKoumoku.IsSyuukeiKoumokuHani;

                // 選択された集計項目番号
                int itemNo2 = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[selectIndex];
                this.CommonChouhyou.SelectSyuukeiKoumokuList[selectSyukeiNo - 1] = itemNo2;

                WINDOW_ID masterID = syuukeiKoumoku.MasterTableID;
                string masterFieldNameCD = syuukeiKoumoku.FieldCD;
                string masterFieldNameRyaku = syuukeiKoumoku.FieldCDName;
                string windowName = syuukeiKoumoku.PopupWindowName;

                #region - 開始側 -

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.DBFieldsName = masterFieldNameCD;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.DisplayItemName = syuukeiKoumoku.DisplayItemName;


                if (syuukeiKoumoku.FocusOutCheckMethod != null)
                {
                    SelectCheckDto selectCheckDto = new SelectCheckDto();
                    selectCheckDto = syuukeiKoumoku.FocusOutCheckMethod[0];
                    selectCheckDto.SendParams = new string[] { "ISNOT_NEED_DELETE_FLG", };
                    syuukeiKoumoku.FocusOutCheckMethod.Add(selectCheckDto);
                }

                //this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.FocusOutCheckMethod = focusOutCheckMethod;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.FocusOutCheckMethod = syuukeiKoumoku.FocusOutCheckMethod;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.GetCodeMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupGetMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSetFormField = ctrlStartCDName + "," + ctrlStartCDRyakuName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupWindowId = masterID;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupWindowName = windowName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.SetFormField = ctrlStartCDName + "," + ctrlStartCDRyakuName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.ShortItemName = syuukeiKoumoku.ShortItemName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.Tag = syuukeiKoumoku.TagName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.CharactersNumber = syuukeiKoumoku.Length;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.MaxLength = syuukeiKoumoku.Length;

                if (syuukeiKoumoku.PopupWindowSetting != null)
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting = syuukeiKoumoku.PopupWindowSetting;
                }
                else
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting = new Collection<JoinMethodDto>();
                }

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.Text = syuukeiKoumoku.SyuukeiKoumokuHani.CodeStartName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuStartMeisho.DBFieldsName = masterFieldNameRyaku;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.PopupWindowId = masterID;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.PopupWindowName = windowName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.PopupGetMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.SetFormField = ctrlStartCDName + "," + ctrlStartCDRyakuName;

                if (syuukeiKoumoku.PopupWindowSetting != null)
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.popupWindowSetting = syuukeiKoumoku.PopupWindowSetting;
                }
                else
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuStart.popupWindowSetting = new Collection<JoinMethodDto>();
                }

                // No.2851-->
                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                {   // 銀行支店別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.ItemDefinedTypes = null;
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に銀行別がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                        {
                            string bankCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;  // 銀行の場合Start==End
                            r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                            param.And_Or = CONDITION_OPERATOR.AND;
                            param.KeyName = "BANK_CD";
                            param.Control = bankCDName;
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                            SelectCheckDto selectCheckDto = new SelectCheckDto("銀行支店マスタコードチェックandセッティング");
                            selectCheckDto.SendParams = new string[] { bankCDName };
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.FocusOutCheckMethod.Clear();
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.FocusOutCheckMethod.Add(selectCheckDto);
                            this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.ItemDefinedTypes = DB_TYPE.VARCHAR.ToTypeString();
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                {   // 現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                                else
                                {   // 開始と終了が異なる
                                    JoinMethodDto joinMethodDto;
                                    SearchConditionsDto searchConditionsDto;

                                    joinMethodDto = new JoinMethodDto();

                                    joinMethodDto.IsCheckLeftTable = true;
                                    joinMethodDto.IsCheckRightTable = false;
                                    joinMethodDto.Join = JOIN_METHOD.WHERE;
                                    joinMethodDto.LeftTable = "M_GENBA";

                                    if (!sval.Equals(""))
                                    {   // 開始が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.MORE_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = sval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    if (!eval.Equals(""))
                                    {   // 終了が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.LESS_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = eval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting.Add(joinMethodDto);
                                }
                            }
                            break;
                        }
                    }
                }
                // NBo.2851<--
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                {
                    // 荷降現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷降業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                {
                    // 荷積現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷積業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                {
                    // 車輌別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に運搬業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.Control = gyosyaCDName;
                                    param.KeyName = "key001";
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }

                #endregion - 開始側 -

                #region - 終了側 -

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.DBFieldsName = masterFieldNameCD;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.DisplayItemName = syuukeiKoumoku.DisplayItemName;

                // No.2850関連-->
                //Collection<SelectCheckDto> focusOutCheckMethod2 = null;
                //if (syuukeiKoumoku.FocusOutCheckMethod != null)
                //{
                //    focusOutCheckMethod2 = new Collection<SelectCheckDto>();
                //    SelectCheckDto selectCheckDto = new SelectCheckDto();
                //    selectCheckDto = syuukeiKoumoku.FocusOutCheckMethod[0];

                //    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu ||
                //        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu ||
                //        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu ||
                //        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu ||
                //        syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                //    {   // 現場別・銀行支店別・車両別・荷降現場別・荷積現場別
                //        selectCheckDto.SendParams = new string[0];
                //    }
                //    focusOutCheckMethod2.Add(selectCheckDto);
                //}
                //this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.FocusOutCheckMethod = focusOutCheckMethod2;
                // No.2850関連<--

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.FocusOutCheckMethod = syuukeiKoumoku.FocusOutCheckMethod;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.GetCodeMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupGetMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSetFormField = ctrlEndCDName + "," + ctrlEndCDRyakuName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupWindowId = masterID;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupWindowName = windowName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.SetFormField = ctrlEndCDName + "," + ctrlEndCDRyakuName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.ShortItemName = syuukeiKoumoku.ShortItemName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.Tag = syuukeiKoumoku.TagName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.CharactersNumber = syuukeiKoumoku.Length;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.MaxLength = syuukeiKoumoku.Length;

                if (syuukeiKoumoku.PopupWindowSetting != null)
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting = syuukeiKoumoku.PopupWindowSetting;
                }
                else
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting = new Collection<JoinMethodDto>();
                }

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.Text = syuukeiKoumoku.SyuukeiKoumokuHani.CodeEndName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].TextBoxSyukeiKomokuEndMeisho.DBFieldsName = masterFieldNameRyaku;

                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.PopupWindowId = masterID;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.PopupWindowName = windowName;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.PopupGetMasterField = masterFieldNameCD + "," + masterFieldNameRyaku;
                this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.SetFormField = ctrlEndCDName + "," + ctrlEndCDRyakuName;
                if (syuukeiKoumoku.PopupWindowSetting != null)
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.popupWindowSetting = syuukeiKoumoku.PopupWindowSetting;
                }
                else
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].PopupOpenButtonSyukeiKomokuEnd.popupWindowSetting = new Collection<JoinMethodDto>();
                }

                // No.2851-->
                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                {   // 銀行支店別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.ItemDefinedTypes = null;
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に銀行別がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                        {
                            string bankCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;  // 銀行の場合Start==End
                            r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                            param.And_Or = CONDITION_OPERATOR.AND;
                            param.KeyName = "BANK_CD";
                            param.Control = bankCDName;
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                            SelectCheckDto selectCheckDto = new SelectCheckDto("銀行支店マスタコードチェックandセッティング");
                            selectCheckDto.SendParams = new string[] { bankCDName };
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.FocusOutCheckMethod.Clear();
                            this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.FocusOutCheckMethod.Add(selectCheckDto);
                            this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.ItemDefinedTypes = DB_TYPE.VARCHAR.ToTypeString();
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                {   // 現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                                else
                                {   // 開始と終了が異なる
                                    JoinMethodDto joinMethodDto;
                                    SearchConditionsDto searchConditionsDto;

                                    joinMethodDto = new JoinMethodDto();

                                    joinMethodDto.IsCheckLeftTable = true;
                                    joinMethodDto.IsCheckRightTable = false;
                                    joinMethodDto.Join = JOIN_METHOD.WHERE;
                                    joinMethodDto.LeftTable = "M_GENBA";

                                    if (!sval.Equals(""))
                                    {   // 開始が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.MORE_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = sval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    if (!eval.Equals(""))
                                    {   // 終了が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.LESS_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = eval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Add(joinMethodDto);
                                }
                            }
                            break;
                        }
                    }
                }
                // NBo.2851<--
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                {
                    // 荷降現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷降業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                {
                    // 荷積現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷積業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                {
                    // 車輌別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に運搬業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.Control = gyosyaCDName;
                                    param.KeyName = "key001";
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }

                #endregion - 終了側 -

                if (this.WindowId == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU || this.WindowId == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU)
                {   // 入金予定一覧表又は出金予定一覧表
                    if (selectSyukeiNo == 2)
                    {   // 集計項目が２番目

                        // 集計項目範囲の有効・無効
                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].CustomPanelSyukeiKomokuStartEnd.Enabled = false;
                    }
                }
                else if (this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                {   // 売上・支払明細表又は売上・支払集計表

                    if (syuukeiKoumoku.MasterTableID == WINDOW_ID.M_DENSHU_KBN)
                    {   // 電種区分

                        // 集計項目範囲の有効・無効
                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].CustomPanelSyukeiKomokuStartEnd.Enabled = true;

                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.MaxLength = 1;
                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.CharactersNumber = 1;

                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.MaxLength = 1;
                        this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.CharactersNumber = 1;
                    }
                    else
                    {   // 電種区分以外
                    }
                }

                this.selectSyuukeiKoumokuIndex[selectSyukeiNo - 1] = selectIndex;

                if (this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].CustomPanelSyukeiKomokuStartEnd.Enabled)
                {   // コード範囲指定が有効
                    SelectCheckDto selectCheckDto;

                    // 開始側
                    selectCheckDto = new SelectCheckDto("CD整合性チェック(From用)");
                    selectCheckDto.DisplayMessage = "開始コードが終了コードより後のコードになっています。開始コードには終了コード以前のコードを指定してください。";

                    switch (selectSyukeiNo)
                    {
                        case 1:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku1EndCD", };
                            break;
                        case 2:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku2EndCD", };
                            break;
                        case 3:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku3EndCD",};
                            break;
                        case 4:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku4EndCD", };
                            break;
                    }

                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.FocusOutCheckMethod.Add(selectCheckDto);

                    // 終了側
                    selectCheckDto = new SelectCheckDto("CD整合性チェック(To用)");
                    selectCheckDto.DisplayMessage = "終了コードが開始コードより前のコードになっています。終了コードには開始コード以後のコードを指定してください。";

                    switch (selectSyukeiNo)
                    {
                        case 1:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku1StartCD", };
                            break;
                        case 2:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku2StartCD", };
                            break;
                        case 3:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku3StartCD", };
                            break;
                        case 4:
                            selectCheckDto.SendParams = new string[] { "customAlphaNumTextBoxSyukeiKomoku4StartCD", };
                            break;
                    }

                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.FocusOutCheckMethod.Add(selectCheckDto);

                    #region - 複数業者選択時の制御 -

                    if (this.WindowId == WINDOW_ID.R_URIAGE_MEISAIHYOU || this.WindowId == WINDOW_ID.R_URIAGE_SYUUKEIHYOU
                        || this.WindowId == WINDOW_ID.R_SHIHARAI_MEISAIHYOU || this.WindowId == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU
                        || this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU
                        || this.WindowId == WINDOW_ID.R_NYUUKIN_MEISAIHYOU || this.WindowId == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU
                        || this.WindowId == WINDOW_ID.R_UKETSUKE_MEISAIHYOU 
                        || this.WindowId == WINDOW_ID.R_KEIRYOU_MEISAIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                    {
                        if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                        {
                            // 選択された集計項目が現場別の場合
                            for (int i = 0; i < selectSyukeiNo - 1; i++)
                            {
                                int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                                int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                                SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                                // 他の集計項目に業者がある場合
                                if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                                {
                                    this.ChangeGenbaCdTextBoxEnabled(selectSyukeiNo - 1, i, syuukeiKoumoku.Type);
                                }
                            }
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                        {
                            // 選択された集計項目が車輌別の場合
                            for (int i = 0; i < selectSyukeiNo - 1; i++)
                            {
                                int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                                int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                                SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                                // 他の集計項目に運搬業者がある場合
                                if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                                {
                                    this.ChangeGenbaCdTextBoxEnabled(selectSyukeiNo - 1, i, syuukeiKoumoku.Type);
                                }
                            }
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                        {
                            // 選択された集計項目が荷降現場別の場合
                            for (int i = 0; i < selectSyukeiNo - 1; i++)
                            {
                                int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                                int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                                SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                                // 他の集計項目に荷降業者がある場合
                                if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                                {
                                    this.ChangeGenbaCdTextBoxEnabled(selectSyukeiNo - 1, i, syuukeiKoumoku.Type);
                                }
                            }
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                        {
                            // 選択された集計項目が荷積現場別の場合
                            for (int i = 0; i < selectSyukeiNo - 1; i++)
                            {
                                int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                                int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                                SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                                // 他の集計項目に荷積業者がある場合
                                if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                                {
                                    this.ChangeGenbaCdTextBoxEnabled(selectSyukeiNo - 1, i, syuukeiKoumoku.Type);
                                }
                            }
                        }
                        else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                        {
                            // 選択された集計項目が銀行支店別の場合
                            for (int i = 0; i < selectSyukeiNo - 1; i++)
                            {
                                int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                                int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                                SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                                // 他の集計項目に銀行がある場合
                                if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                                {
                                    this.ChangeGenbaCdTextBoxEnabled(selectSyukeiNo - 1, i, syuukeiKoumoku.Type);
                                }
                            }
                        }
                        else
                        {
                            this.CodeTextBoxInit(selectSyukeiNo - 1, syuukeiKoumoku.Type);
                        }
                    }

                    #endregion - 複数業者選択時の制御 -
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 業者CDの入力状態に応じて現場CDテキストボックスの活性状態を制御
        /// </summary>
        /// <param name="selectSyukeiNo"></param>
        /// <param name="anotherSyukeiNo"></param>
        /// <param name="syuukeiType"></param>
        private void ChangeGenbaCdTextBoxEnabled(int selectSyukeiNo, int anotherSyukeiNo, SYUKEUKOMOKU_TYPE syuukeiType)
        {
            LogUtility.DebugMethodStart(selectSyukeiNo, anotherSyukeiNo, syuukeiType);

            var gyoushaCdFrom = this.CustomControlSyukeiKomokuList[anotherSyukeiNo].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
            var gyoushaCdTo = this.CustomControlSyukeiKomokuList[anotherSyukeiNo].AlphaNumTextBoxSyukeiKomokuEndCD.Text;

            // 現場CDテキストボックスの活性状態を初期化
            this.CodeTextBoxInit(selectSyukeiNo, syuukeiType);

            if (!String.IsNullOrEmpty(gyoushaCdFrom) && !String.IsNullOrEmpty(gyoushaCdTo))
            {
                // どちらも入力されていて業者CDが異なる場合は、現場CDは入力不可
                if (gyoushaCdFrom != gyoushaCdTo)
                {
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuStartCD.Enabled = false;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuStartMeisho.Enabled = false;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].PopupOpenButtonSyukeiKomokuStart.Enabled = false;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuEndCD.Enabled = false;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuEndMeisho.Enabled = false;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].PopupOpenButtonSyukeiKomokuEnd.Enabled = false;

                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuStartCD.Text = String.Empty;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuStartMeisho.Text = String.Empty;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuEndCD.Text = String.Empty;
                    this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuEndMeisho.Text = String.Empty;
                    // 現場が全て選択されているものとみなす
                    SyuukeiKoumoku syuukeiKoumoku = null;
                    switch (syuukeiType)
                    {
                        case SYUKEUKOMOKU_TYPE.GyoshaBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[3];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                        case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[16];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                        case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[6];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                        case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[8];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                        case SYUKEUKOMOKU_TYPE.GinkoBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[22];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                        case SYUKEUKOMOKU_TYPE.GenbaBetsu:
                        case SYUKEUKOMOKU_TYPE.SharyoBetsu:
                        case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:
                        case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:
                        case SYUKEUKOMOKU_TYPE.GinkoShitenBetsu:
                            syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[(int)syuukeiType];
                            syuukeiKoumoku.IsSelectGenbaAll = true;
                            break;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CDテキストボックスの活性状態を初期化
        /// </summary>
        /// <param name="selectSyukeiNo"></param>
        private void CodeTextBoxInit(int selectSyukeiNo, SYUKEUKOMOKU_TYPE syuukeiType)
        {
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuStartCD.Enabled = true;
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuStartMeisho.Enabled = true;
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].PopupOpenButtonSyukeiKomokuStart.Enabled = true;
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].AlphaNumTextBoxSyukeiKomokuEndCD.Enabled = true;
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].TextBoxSyukeiKomokuEndMeisho.Enabled = true;
            this.CustomControlSyukeiKomokuList[selectSyukeiNo].PopupOpenButtonSyukeiKomokuEnd.Enabled = true;
            SyuukeiKoumoku syuukeiKoumoku = null;
            switch (syuukeiType)
            {
                case SYUKEUKOMOKU_TYPE.GyoshaBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[3];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
                case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[16];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
                case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[6];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
                case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[8];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
                case SYUKEUKOMOKU_TYPE.GinkoBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[22];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
                case SYUKEUKOMOKU_TYPE.GenbaBetsu:
                case SYUKEUKOMOKU_TYPE.SharyoBetsu:
                case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:
                case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:
                case SYUKEUKOMOKU_TYPE.GinkoShitenBetsu:
                    syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[(int)syuukeiType];
                    syuukeiKoumoku.IsSelectGenbaAll = false;
                    break;
            }
        }

        /// <summary>日付範囲指定区分が変更された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void RadioButtonHidukeHaniShitei_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CustomRadioButton customRadioButton = (CustomRadioButton)sender;
                this.customPanelHidukeHaniShitei.Enabled = int.Parse(customRadioButton.Value) == 3 ? true : false;

                if (this.customPanelHidukeHaniShitei.Enabled == false)
                {   // 無効
                    this.customDateTimePickerHidukeHaniShiteiStart.Value = null;
                    this.customDateTimePickerHidukeHaniShiteiEnd.Value = null;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>集計項目１がドロップダウンリストから選択され閉じた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void CustomComboBoxSyukeiKomoku1ID_DropDownClosed(object sender, EventArgs e)
        {
            // 集計項目ポップアップ関連コントロールの結びつきを変える
            this.SyukeiKomokuChangePopupCtrlConnetion(1);
        }

        /// <summary>集計項目２がドロップダウンリストから選択され閉じた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void CustomComboBoxSyukeiKomoku2ID_DropDownClosed(object sender, EventArgs e)
        {
            // 集計項目ポップアップ関連コントロールの結びつきを変える
            this.SyukeiKomokuChangePopupCtrlConnetion(2);
        }

        /// <summary>集計項目３がドロップダウンリストから選択され閉じた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void CustomComboBoxSyukeiKomoku3ID_DropDownClosed(object sender, EventArgs e)
        {
            // 集計項目ポップアップ関連コントロールの結びつきを変える
            this.SyukeiKomokuChangePopupCtrlConnetion(3);
        }

        /// <summary>集計項目４がドロップダウンリストから選択され閉じた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void CustomComboBoxSyukeiKomoku4ID_DropDownClosed(object sender, EventArgs e)
        {
            // 集計項目ポップアップ関連コントロールの結びつきを変える
            this.SyukeiKomokuChangePopupCtrlConnetion(4);
        }

        /// <summary>フォーム上でキーが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyData)
                {
                    case Keys.F1:   // 追加（伝票）
                        if (!this.logic.Func1())
                        {   
                            return;
                        }
                        break;
                    case Keys.F2:   // 削除（伝票）
                        if (!this.logic.Func2())
                        {
                            return;
                        }

                        break;
                    case Keys.F3:   // 全削除（伝票）
                        if (this.logic.Func3())
                        {
                            return;
                        }
                        break;
                    case Keys.F4:   // 追加（明細）
                        if (!this.logic.Func4())
                        {
                            return;
                        }
                        break;
                    case Keys.F5:   // 削除（明細）
                        if (!this.logic.Func5())
                        {
                            return;
                        }
                        break;
                    case Keys.F6:  // 全削除（明細）
                        if (!this.logic.Func6())
                        {
                            return;
                        }
                        break;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票種類の表示状態を設定する</summary>
        private bool SettingDenpyouSyurui()
        {
            try
            {
                Point location = new Point(102, 0);

                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:                     // R358(売上明細表)
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:                   // R362(支払明細表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:            // R355(売上／支払明細表)
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:                    // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:                  // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:           // R356(売上／支払集計表)
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:                    // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:                   // R373(出金明細表)
                    case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:                   // R367(入金集計表)
                    case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:                  // R374(出金集計表)
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:                    // R379(請求明細表)
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:             // R384(支払明細明細表)
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:                 // R369(未入金一覧表)
                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:                 // R376(未出金一覧表)
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:             // R370(入金予定一覧表)
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:             // R377(出金予定一覧表)
                        this.customPanelDenpyouSyurui1.Visible = true;
                        this.customPanelDenpyouSyurui1.Location = location;

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_SUIIHYOU:          // R432(売上・支払（全て）推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_JYUNNIHYOU:        // R433(売上・支払（全て）順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ALL_ZENNEN_TAIHIHYOU:  // R434(売上・支払（全て）前年対比表)
                        this.customPanelDenpyouSyurui2.Visible = true;
                        this.customPanelDenpyouSyurui2.Location = location;

                        break;
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:                   // R342 受付明細表
                        this.customPanelDenpyouSyurui3.Visible = true;
                        this.customPanelDenpyouSyurui3.Location = location;

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                    // R398 運賃明細表
                        this.customPanelDenpyouSyurui4.Visible = true;
                        this.customPanelDenpyouSyurui4.Location = location;

                        break;
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:                    // R351 計量明細表
                    case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:                   // R352 計量集計表
                        this.customPanelDenpyouSyurui1.Visible = true;
                        this.customPanelDenpyouSyurui1.Location = location;

                        break;

                    default:
                        break;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SettingDenpyouSyurui", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SettingDenpyouSyurui", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>伝票種類指定（受入）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoSyuruiShitei2_1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)

                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票種類指定（出荷）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoSyuruiShitei2_2_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)

                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票種類指定（売上／支払）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoSyuruiShitei2_3_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)

                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票種類指定（全て）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoSyuruiShitei2_4_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.WindowId)
                {
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)

                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = true;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票区分指定（売上）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoKubunShitei1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowId == WINDOW_ID.R_KEIRYOU_SUIIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // R432(計量推移表・計量順位表・計量前年対比表)

                    this.customPanelDenpyoSyuruiShitei.Enabled = false;

                    this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                    this.customRadioButtonDenpyoSyuruiShitei2_1.Enabled = true;     // 受入
                    this.customRadioButtonDenpyoSyuruiShitei2_2.Enabled = true;     // 出荷
                    this.customRadioButtonDenpyoSyuruiShitei2_3.Enabled = true;     // 売上／支払
                    this.customRadioButtonDenpyoSyuruiShitei2_4.Enabled = true;     // 全て

                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.RangeSetting.Min = 1;
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.Tag = "【1、2、3、4】のいずれかで入力してください";
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.LinkedRadioButtonArray = new string[] { "customRadioButtonDenpyoSyuruiShitei2_1", "customRadioButtonDenpyoSyuruiShitei2_2", "customRadioButtonDenpyoSyuruiShitei2_3", "customRadioButtonDenpyoSyuruiShitei2_4" };
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.CharacterLimitList = new char[] { '1', '2', '3', '4', '5' };
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票区分指定（支払）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoKubunShitei2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowId == WINDOW_ID.R_KEIRYOU_SUIIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // R432(計量推移表・計量順位表・計量前年対比表)

                    this.customPanelDenpyoSyuruiShitei.Enabled = false;

                    this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = false;

                    this.customRadioButtonDenpyoSyuruiShitei2_1.Enabled = true;     // 受入
                    this.customRadioButtonDenpyoSyuruiShitei2_2.Enabled = true;     // 出荷
                    this.customRadioButtonDenpyoSyuruiShitei2_3.Enabled = true;     // 売上／支払
                    this.customRadioButtonDenpyoSyuruiShitei2_4.Enabled = true;     // 全て

                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.RangeSetting.Min = 1;
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.Tag = "【1、2、3、4】のいずれかで入力してください";
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.LinkedRadioButtonArray = new string[] { "customRadioButtonDenpyoSyuruiShitei2_1", "customRadioButtonDenpyoSyuruiShitei2_2", "customRadioButtonDenpyoSyuruiShitei2_3", "customRadioButtonDenpyoSyuruiShitei2_4" };
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.CharacterLimitList = new char[] { '1', '2', '3', '4', '5' };
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        /// <summary>伝票区分指定（全て）ラジオボタンがクリックされた場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void CustomRadioButtonDenpyoKubunShitei3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowId == WINDOW_ID.R_KEIRYOU_SUIIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {   // R432(計量推移表・計量順位表・計量前年対比表)

                    this.customPanelDenpyoSyuruiShitei.Enabled = true;

                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.Text = "4";

                    this.customCheckBoxSyuruiShiteiGroupKubun2.Enabled = true;

                    this.customRadioButtonDenpyoSyuruiShitei2_1.Enabled = false;    // 受入
                    this.customRadioButtonDenpyoSyuruiShitei2_2.Enabled = false;    // 出荷
                    this.customRadioButtonDenpyoSyuruiShitei2_3.Enabled = false;    // 売上／支払
                    this.customRadioButtonDenpyoSyuruiShitei2_4.Enabled = true;     // 全て

                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.RangeSetting.Min = 4;
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.Tag = "【4】のいずれかで入力してください";
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.LinkedRadioButtonArray = new string[] { "customRadioButtonDenpyoSyuruiShitei2_4" };
                    this.CustomNumericTextBox2DenpyoSyuruiShitei2.CharacterLimitList = new char[] { '4' };
                }
            }
            catch (Exception ee)
            {
                LogUtility.Error(ee.Message, ee);
            }
        }

        #endregion - Methods -

        #region - Inner Classes -

        #region - CustomControlSyukeiKomoku -

        /// <summary>集計項目カスタムコントロールを表すクラス・コントロール</summary>
        internal class CustomControlSyukeiKomoku
        {
            #region - Consructors -

            /// <summary>Initializes a new instance of the <see cref="CustomControlSyukeiKomoku" /> class.</summary>
            /// <param name="panelSyukeiKomoku">集計項目（カスタムコントロールパネル）</param>
            /// <param name="comboBoxSyukeiKomokuID">集計項目ＩＤ（カスタムコントロールコンボボックス）</param>
            /// <param name="customPanelSyukeiKomokuStartEnd">集計項目開始～終了（カスタムコントロールパネル）</param>
            /// <param name="alphaNumTextBoxSyukeiKomokuStartCD">集計項目開始ＣＤ（カスタムコントロールテキストボックス）</param>
            /// <param name="textBoxSyukeiKomokuStartMeisho">集計項目開始名称（カスタムコントロールテキストボックス）</param>
            /// <param name="buttonSyukeiKomokuStart">集計項目開始ボタン（カスタムコントロールポップアップボタン）</param>
            /// <param name="alphaNumTextBoxSyukeiKomokuEndCD">集計項目終了ＣＤ（カスタムコントロールテキストボックス）</param>
            /// <param name="textBoxSyukeiKomokuEndMeisho">集計項目終了名称（カスタムコントロールテキストボックス）</param>
            /// <param name="buttonSyukeiKomokuEnd">集計項目終了ボタン（カスタムコントロールポップアップボタン）</param>
            public CustomControlSyukeiKomoku(
                CustomPanel panelSyukeiKomoku,
                CustomComboBox comboBoxSyukeiKomokuID,
                CustomPanel customPanelSyukeiKomokuStartEnd,
                CustomAlphaNumTextBox alphaNumTextBoxSyukeiKomokuStartCD,
                CustomTextBox textBoxSyukeiKomokuStartMeisho,
                CustomPopupOpenButton buttonSyukeiKomokuStart,
                CustomAlphaNumTextBox alphaNumTextBoxSyukeiKomokuEndCD,
                CustomTextBox textBoxSyukeiKomokuEndMeisho,
                CustomPopupOpenButton buttonSyukeiKomokuEnd)
            {
                // 集計項目（カスタムコントロールパネル）
                this.PanelSyukeiKomoku = panelSyukeiKomoku;

                // 集計項目ＩＤ（カスタムコントロールコンボボックス）
                this.ComboBoxSyukeiKomokuID = comboBoxSyukeiKomokuID;

                // 集計項目開始～終了（カスタムコントロールパネル）
                this.CustomPanelSyukeiKomokuStartEnd = customPanelSyukeiKomokuStartEnd;

                // 集計項目開始ＣＤ（カスタムコントロールテキストボックス）
                this.AlphaNumTextBoxSyukeiKomokuStartCD = alphaNumTextBoxSyukeiKomokuStartCD;

                // 集計項目開始名称（カスタムコントロールテキストボックス）
                this.TextBoxSyukeiKomokuStartMeisho = textBoxSyukeiKomokuStartMeisho;

                // 集計項目開始ボタン（カスタムコントロールポップアップボタン）
                this.PopupOpenButtonSyukeiKomokuStart = buttonSyukeiKomokuStart;

                // 集計項目終了ＣＤ（カスタムコントロールテキストボックス）
                this.AlphaNumTextBoxSyukeiKomokuEndCD = alphaNumTextBoxSyukeiKomokuEndCD;

                // 集計項目終了名称（カスタムコントロールテキストボックス）
                this.TextBoxSyukeiKomokuEndMeisho = textBoxSyukeiKomokuEndMeisho;

                // 集計項目終了ボタン（カスタムコントロールポップアップボタン）
                this.PopupOpenButtonSyukeiKomokuEnd = buttonSyukeiKomokuEnd;
            }

            #endregion - Consructors -

            #region - Properties -

            /// <summary>集計項目（カスタムコントロールパネル）を保持するプロパティ</summary>
            public CustomPanel PanelSyukeiKomoku { get; private set; }

            /// <summary>集計項目ＩＤ（カスタムコントロールコンボボックス）を保持するプロパティ</summary>
            public CustomComboBox ComboBoxSyukeiKomokuID { get; private set; }

            /// <summary>集計項目開始～終了（カスタムコントロールパネル）を保持するプロパティ</summary>
            public CustomPanel CustomPanelSyukeiKomokuStartEnd { get; private set; }

            /// <summary>集計項目開始ＣＤ（カスタムコントロールテキストボックス）を保持するプロパティ</summary>
            public CustomAlphaNumTextBox AlphaNumTextBoxSyukeiKomokuStartCD { get; private set; }

            /// <summary>集計項目開始名称（カスタムコントロールテキストボックス）を保持するプロパティ</summary>
            public CustomTextBox TextBoxSyukeiKomokuStartMeisho { get; private set; }

            /// <summary>集計項目開始ボタン（カスタムコントロールポップアップボタン）を保持するプロパティ</summary>
            public CustomPopupOpenButton PopupOpenButtonSyukeiKomokuStart { get; private set; }

            /// <summary>集計項目終了ＣＤ（カスタムコントロールテキストボックス）を保持するプロパティ</summary>
            public CustomAlphaNumTextBox AlphaNumTextBoxSyukeiKomokuEndCD { get; private set; }

            /// <summary>集計項目終了名称（カスタムコントロールテキストボックス）を保持するプロパティ</summary>
            public CustomTextBox TextBoxSyukeiKomokuEndMeisho { get; private set; }

            /// <summary>集計項目終了ボタン（カスタムコントロールポップアップボタン）を保持するプロパティ</summary>
            public CustomPopupOpenButton PopupOpenButtonSyukeiKomokuEnd { get; private set; }

            #endregion - Properties -
        }

        #endregion - CustomControlSyukeiKomoku -

        #region - KomokuDisplay -

        /// <summary>項目表示有無を表すクラス・コントロール</summary>
        internal class KomokuDisplay
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="KomokuDisplay" /> class.</summary>
            /// <param name="kyotenShiteiEnable">拠点指定表示するかどうかを表す値</param>
            /// <param name="denpyoSyuruiShiteiEnable">伝票種類表示するかどうかを表す値</param>
            /// <param name="denpyoKubunShiteiEnable">伝票区分指定表示するかどうかを表す値</param>
            /// <param name="syukeiKomokuEnable">集計項目表示するか否かのリスト</param>
            /// <param name="selectEnableKomokuDenpyoEnable">集計可能項目（伝票）表示するかどうかを表す値</param>
            /// <param name="selectEnableKomokuMeisaiEnable">集計可能項目（明細）表示するかどうかを表す値</param>
            /// <param name="syukeiKomokuNo">集計項目番号</param>
            public KomokuDisplay(bool kyotenShiteiEnable, bool denpyoSyuruiShiteiEnable, bool denpyoKubunShiteiEnable, bool[] syukeiKomokuEnable, bool selectEnableKomokuDenpyoEnable, bool selectEnableKomokuMeisaiEnable, int[] syukeiKomokuNo)
            {
                this.SyukeiKomokuListEnable = new List<bool>();
                this.SyukeiKomokuNoList = new List<int>();

                // 集計項目の有効・無効
                for (int i = 0; i < ConstMaxSyuukeiKoumoku; i++)
                {
                    if (i < syukeiKomokuEnable.Length)
                    {
                        this.SyukeiKomokuListEnable.Add(syukeiKomokuEnable[i]);
                    }
                }

                // 集計項目番号
                for (int i = 0; i < syukeiKomokuNo.Length; i++)
                {
                    this.SyukeiKomokuNoList.Add(syukeiKomokuNo[i]);
                }

                // 拠点指定の有効・無効
                this.KyotenShiteiEnable = kyotenShiteiEnable;

                // 伝票種類指定の有効・無効
                this.DenpyoSyuruiShiteiEnable = denpyoSyuruiShiteiEnable;

                // 伝票種類指定の有効・無効
                this.DenpyoKubunShiteiEnable = denpyoKubunShiteiEnable;

                // 選択可能項目（伝票）の有効・無効
                this.SelectEnableKomokuDenpyoEnable = selectEnableKomokuDenpyoEnable;

                // 選択可能項目（明細）の有効・無効
                this.SelectEnableKomokuMeisaiEnable = selectEnableKomokuMeisaiEnable;
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>拠点指定の有効・無効の真偽値を保持するプロパティ</summary>
            public bool KyotenShiteiEnable { get; private set; }

            /// <summary>伝票種類指定の有効・無効の真偽値を保持するプロパティ</summary>
            public bool DenpyoSyuruiShiteiEnable { get; private set; }

            /// <summary>伝票種類指定の有効・無効の真偽値を保持するプロパティ</summary>
            public bool DenpyoKubunShiteiEnable { get; private set; }

            /// <summary>集計項目の有効・無効の真偽値を保持するプロパティ</summary>
            public List<bool> SyukeiKomokuListEnable { get; private set; }

            /// <summary>集計項目番号を保持するプロパティ</summary>
            public List<int> SyukeiKomokuNoList { get; private set; }

            /// <summary>選択可能項目（伝票）の有効・無効の真偽値を保持するプロパティ</summary>
            public bool SelectEnableKomokuDenpyoEnable { get; private set; }

            /// <summary>選択可能項目（明細）の有効・無効の真偽値を保持するプロパティ</summary>
            public bool SelectEnableKomokuMeisaiEnable { get; private set; }

            #endregion - Properties -
        }

        #endregion - KomokuDisplay -

        // No.2851-->
        private void SyukeiKoumokuTextValidated(object sender, EventArgs e)
        {
            // コンボボックスで現場を選択された後で業者CDを変更された場合対応
            for (int i = 0; i < 4; i++)
            {
                this.SyukeiKomokuChangePopupCtrlConnetionAfter(i + 1);
            }
        }

        /// <summary>集計項目ポップアップ関連コントロールの結びつきを変える処理を実行する</summary>
        private void SyukeiKomokuChangePopupCtrlConnetionAfter(int selectSyukeiNo)
        {
            try
            {
                // 選択されているアイテムインデックス取得
                int selectIndex = this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].ComboBoxSyukeiKomokuID.SelectedIndex;
                if (selectIndex < 0)
                {
                    return;
                }

                // 選択可能な集計項目番号
                int itemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[selectIndex];

                // 集計項目
                SyuukeiKoumoku syuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[itemNo];

                #region - 開始側 -

                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                {   // 現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            r_framework.Utility.LogUtility.Info("sval：" + sval);
                            r_framework.Utility.LogUtility.Info("eval：" + eval);

                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                                else
                                {   // 開始と終了が異なる
                                    JoinMethodDto joinMethodDto;
                                    SearchConditionsDto searchConditionsDto;

                                    joinMethodDto = new JoinMethodDto();

                                    joinMethodDto.IsCheckLeftTable = true;
                                    joinMethodDto.IsCheckRightTable = false;
                                    joinMethodDto.Join = JOIN_METHOD.WHERE;
                                    joinMethodDto.LeftTable = "M_GENBA";

                                    if (!sval.Equals(""))
                                    {   // 開始が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.MORE_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = sval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    if (!eval.Equals(""))
                                    {   // 終了が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.LESS_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = eval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.popupWindowSetting.Add(joinMethodDto);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                {
                    // 荷降現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷降業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                {
                    // 荷積現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷積業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                {
                    // 車輌別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に運搬業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.Control = gyosyaCDName;
                                    param.KeyName = "key001";
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuStartCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }

                #endregion - 開始側 -

                #region - 終了側 -

                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                {   // 現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            r_framework.Utility.LogUtility.Info("sval：" + sval);
                            r_framework.Utility.LogUtility.Info("eval：" + eval);

                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                                else
                                {   // 開始と終了が異なる
                                    JoinMethodDto joinMethodDto;
                                    SearchConditionsDto searchConditionsDto;

                                    joinMethodDto = new JoinMethodDto();

                                    joinMethodDto.IsCheckLeftTable = true;
                                    joinMethodDto.IsCheckRightTable = false;
                                    joinMethodDto.Join = JOIN_METHOD.WHERE;
                                    joinMethodDto.LeftTable = "M_GENBA";

                                    if (!sval.Equals(""))
                                    {   // 開始が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.MORE_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = sval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    if (!eval.Equals(""))
                                    {   // 終了が空でない
                                        searchConditionsDto = new SearchConditionsDto();
                                        searchConditionsDto.And_Or = CONDITION_OPERATOR.AND;
                                        searchConditionsDto.Condition = JUGGMENT_CONDITION.LESS_THAN;
                                        searchConditionsDto.LeftColumn = "GYOUSHA_CD";
                                        searchConditionsDto.Value = eval;
                                        searchConditionsDto.ValueColumnType = DB_TYPE.VARCHAR;
                                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                                    }
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.popupWindowSetting.Add(joinMethodDto);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                {
                    // 荷降現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷降業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                {
                    // 荷積現場別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に荷積業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.KeyName = "GYOUSHA_CD";
                                    param.Control = gyosyaCDName;
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }
                else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                {
                    // 車輌別の場合
                    for (int i = 0; i < selectSyukeiNo - 1; i++)
                    {
                        int beforSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                        int beforItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[beforSelectIndex];
                        SyuukeiKoumoku beforSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[beforItemNo];
                        // 前の集計項目に運搬業者がある場合
                        if (beforSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                        {
                            string sval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuStartCD.Text;
                            string eval = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Text;
                            if (!sval.Equals("") || !eval.Equals(""))
                            {   // 開始、終了何れかが空でない
                                if (sval.Equals(eval))
                                {   // 開始と終了が同じ
                                    string gyosyaCDName = this.CustomControlSyukeiKomokuList[i].AlphaNumTextBoxSyukeiKomokuEndCD.Name;
                                    r_framework.Dto.PopupSearchSendParamDto param = new r_framework.Dto.PopupSearchSendParamDto();
                                    param.And_Or = CONDITION_OPERATOR.AND;
                                    param.Control = gyosyaCDName;
                                    param.KeyName = "key001";
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Clear();
                                    this.CustomControlSyukeiKomokuList[selectSyukeiNo - 1].AlphaNumTextBoxSyukeiKomokuEndCD.PopupSearchSendParams.Add(param);
                                }
                            }
                            break;
                        }
                    }
                }

                #endregion - 終了側 -

                #region - 複数業者選択時の制御 -

                if (this.WindowId == WINDOW_ID.R_URIAGE_MEISAIHYOU || this.WindowId == WINDOW_ID.R_URIAGE_SYUUKEIHYOU
                    || this.WindowId == WINDOW_ID.R_SHIHARAI_MEISAIHYOU || this.WindowId == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU
                    || this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowId == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU
                    || this.WindowId == WINDOW_ID.R_NYUUKIN_MEISAIHYOU || this.WindowId == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU
                    || this.WindowId == WINDOW_ID.R_UKETSUKE_MEISAIHYOU
                    || this.WindowId == WINDOW_ID.R_KEIRYOU_MEISAIHYOU || this.WindowId == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                {
                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GyoshaBetsu)
                    {
                        // 選択された集計項目が業者別の場合
                        for (int i = 0; i < ConstMaxSyuukeiKoumoku - 1; i++)
                        {
                            int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                            int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                            SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                            // 他の集計項目に現場がある場合
                            if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GenbaBetsu)
                            {
                                this.ChangeGenbaCdTextBoxEnabled(i, selectSyukeiNo - 1, syuukeiKoumoku.Type);
                                break;
                            }
                            else
                            {
                                this.CodeTextBoxInit(i, syuukeiKoumoku.Type);
                            }
                        }
                    }
                    else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu)
                    {
                        // 選択された集計項目が運搬業者別の場合
                        for (int i = 0; i < ConstMaxSyuukeiKoumoku - 1; i++)
                        {
                            int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                            int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                            SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                            // 他の集計項目に車輌がある場合
                            if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.SharyoBetsu)
                            {
                                this.ChangeGenbaCdTextBoxEnabled(i, selectSyukeiNo - 1, syuukeiKoumoku.Type);
                                break;
                            }
                            else
                            {
                                this.CodeTextBoxInit(i, syuukeiKoumoku.Type);
                            }
                        }
                    }
                    else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu)
                    {
                        // 選択された集計項目が荷降業者別の場合
                        for (int i = 0; i < ConstMaxSyuukeiKoumoku - 1; i++)
                        {
                            int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                            int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                            SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                            // 他の集計項目に荷降現場がある場合
                            if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu)
                            {
                                this.ChangeGenbaCdTextBoxEnabled(i, selectSyukeiNo - 1, syuukeiKoumoku.Type);
                                break;
                            }
                            else
                            {
                                this.CodeTextBoxInit(i, syuukeiKoumoku.Type);
                            }
                        }
                    }
                    else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu)
                    {
                        // 選択された集計項目が荷積業者別の場合
                        for (int i = 0; i < ConstMaxSyuukeiKoumoku - 1; i++)
                        {
                            int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                            int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                            SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                            // 他の集計項目に荷積現場がある場合
                            if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu)
                            {
                                this.ChangeGenbaCdTextBoxEnabled(i, selectSyukeiNo - 1, syuukeiKoumoku.Type);
                                break;
                            }
                            else
                            {
                                this.CodeTextBoxInit(i, syuukeiKoumoku.Type);
                            }
                        }
                    }
                    else if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoBetsu)
                    {
                        // 選択された集計項目が銀行別の場合
                        for (int i = 0; i < ConstMaxSyuukeiKoumoku - 1; i++)
                        {
                            int anotherSelectIndex = this.CustomControlSyukeiKomokuList[i].ComboBoxSyukeiKomokuID.SelectedIndex;
                            int anotherItemNo = this.CommonChouhyou.SelectEnableSyuukeiKoumokuList[anotherSelectIndex];
                            SyuukeiKoumoku anotherSyuukeiKoumoku = this.CommonChouhyou.SyuukeiKomokuList[anotherItemNo];
                            // 他の集計項目に銀行支店がある場合
                            if (anotherSyuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.GinkoShitenBetsu)
                            {
                                this.ChangeGenbaCdTextBoxEnabled(i, selectSyukeiNo - 1, syuukeiKoumoku.Type);
                                break;
                            }
                            else
                            {
                                this.CodeTextBoxInit(i, syuukeiKoumoku.Type);
                            }
                        }
                    }
                }

                #endregion - 複数業者選択時の制御 -

            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }
        // NBo.2851<--

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        private void customDateTimePickerHidukeHaniShiteiStart_Leave(object sender, EventArgs e)
        {
            this.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = false;
            this.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;
        }

        private void customDateTimePickerHidukeHaniShiteiEnd_Leave(object sender, EventArgs e)
        {
            this.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = false;
            this.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
        }

        private void G418_MeisaihyoSyukeihyoJokenShiteiPopupForm_Load(object sender, EventArgs e)
        {

        }
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        #endregion - Inner Classes -
    }

    #endregion - Class -
}
