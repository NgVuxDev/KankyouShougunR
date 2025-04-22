using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Text.RegularExpressions;


namespace Shougun.Core.PaperManifest.ManifestHimoduke
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestHimoduke.Setting.ButtonSetting.xml";
        ///<summary>
        ///ComponentResourceManager
        ///</summary>
        private ComponentResourceManager resources;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        public MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 紐付済み一次マニ情報検索処理用Dao
        /// </summary>
        private MRLDaoCls ManiRealationDao;

        /// <summary>
        /// 紙マニ検索処理用Dao
        /// </summary>
        private PaperDaoCls PaperManiDao;

        /// <summary>
        /// 電子マニ検索処理用Dao
        /// </summary>
        private ElecDaoCls ElecManiDao;

        /// <summary>
        /// 全てマニ検索処理用Dao
        /// </summary>
        private PaperAndElecDaoCls PaperAndElecManiDao;
        /// <summary>
        /// 紐付テーブルの最大SEQ取得用DAO
        /// </summary>
        private MAXSeqDaoCls MaxSeqGetDao;
        /// <summary>
        /// 電子マニフェスト基本拡張で既存データ判断用Dao
        /// </summary>
        private R18_EXDataExistDaoCls R18_EXDataExistDao;
        /// <summary>
        /// 電子マニフェスト存在チェック検索用Dao
        /// </summary>
        private DT_R18SearchDaoCls DT_R18SearchDao;
        /// <summary>
        /// 紙マニフェスト存在チェック検索用Dao
        /// </summary>
        private PaperExistDaoCls PaperExistDao;
        /// <summary>
        /// 電子廃棄物種類コード名称検索用Dao
        /// </summary>
        private DENSHI_HAIKI_SHURUIE_SearchDaoCls DENSHI_HAIKI_SHURUIE_SearchDao;

        /// <summary>
        /// 電子廃棄物名称コードと名称検索用Dao
        /// </summary>
        private DENSHI_HAIKI_NAME_SearchDaoCls DENSHI_HAIKI_NAME_SearchDao;

        // 20140519 kayo No.734 機能追加 start
        /// <summary>
        /// マニフェスト明細相関の検索、更新用Dao
        /// </summary>
        private TMDDaoCls TMDDao;
        // 20140519 kayo No.734 機能追加 end

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// マニフェスト紐付画面フォーム
        /// </summary>
        private UIForm form;

        /// <summary>マニフェスト紐付画面のHeader</summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        public ConstCls ConstCls { get; set; }

        /// <summary>二次マニの最終処分場情報</summary>
        internal DataTable LastSbnJyouDataForSecondMani { get; set; }

        /// <summary>マニフェスト情報数量書式CD</summary>
        internal string ManifestSuuryoFormatCD = String.Empty;

        /// <summary>マニフェスト情報数量書式</summary>
        internal string ManifestSuuryoFormat = String.Empty;

        private MessageBoxShowLogic MsgBox;

        internal M_SYS_INFO mSysInfo;
        /// <summary>
        /// 端数処理種別
        /// </summary>
        internal enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        /// <summary>
        /// 端数処理桁用Enum
        /// </summary>
        private enum hasuKetaType : short
        {
            NONE = 1,       // 1の位
            ONEPOINT,       // 小数第一位
            TOWPOINT,       // 小数第二位
            THREEPOINT,     // 小数第三位
            FOUR,           // 小数第四位
            FIVE,           // 小数第五位
        }

        #endregion

        #region プロパティ(DTO)


        /// <summary>
        /// 検索条(組込み条件DTO)
        /// </summary>
        public FirstManifestDTOCls dtoMani { get; set; }

        /// <summary>
        /// 検索条件(二次マニシステム番号)
        /// </summary>
        public HIMODUKE_DTOCls dtoHimo { get; set; }
        /// <summary>
        /// 電子マニフェスト基本拡張で既存データ判断用検索条件DTO
        /// </summary>
        public SearchExistDTOCls SearchExistDto { get; set; }

        /// <summary>
        /// 紐付した一次情報検索結果
        /// </summary>
        public DataTable RelationResult { get; set; }

        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        /// 結果紐付したと紐付したい対象の合計
        /// </summary>
        public DataTable TotalResults { get; set; }
        /// <summary>
        /// 紐付用DTO情報
        /// </summary>
        public RelationInfo_DTOCls RelationInfo { get; set; }
        /// <summary>
        /// 電子廃棄物名称CDマスタ情報
        /// </summary>
        public DataTable DenshiHaikiNameCodeResult { get; set; }
        /// <summary>
        /// 電子廃棄物種類マスタ情報
        /// </summary>
        public DataTable DenshiHaikiShuruiCodeResult { get; set; }
        /// <summary>
        /// 存在するチェック検索条件DTO
        /// </summary>
        public SearchExistDTOCls SearchExistDTO { get; set; }

        /// <summary>
        /// 現在の紐付テーブル情報（論理削除用）
        /// 検索するたびに取得しなおすこと。論理削除のためにとっておきます。
        /// </summary>
        private T_MANIFEST_RELATION[] currentRelation;


        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.resources = new ComponentResourceManager(typeof(UIForm));

            this.dtoMani = new FirstManifestDTOCls();
            this.dtoHimo = new HIMODUKE_DTOCls();
            this.ManiRealationDao = DaoInitUtility.GetComponent<MRLDaoCls>();
            this.PaperManiDao = DaoInitUtility.GetComponent<PaperDaoCls>();
            this.ElecManiDao = DaoInitUtility.GetComponent<ElecDaoCls>();
            this.PaperAndElecManiDao = DaoInitUtility.GetComponent<PaperAndElecDaoCls>();
            this.MaxSeqGetDao = DaoInitUtility.GetComponent<MAXSeqDaoCls>();
            this.R18_EXDataExistDao = DaoInitUtility.GetComponent<R18_EXDataExistDaoCls>();
            this.DT_R18SearchDao = DaoInitUtility.GetComponent<DT_R18SearchDaoCls>();
            this.PaperExistDao = DaoInitUtility.GetComponent<PaperExistDaoCls>();
            this.DENSHI_HAIKI_SHURUIE_SearchDao = DaoInitUtility.GetComponent<DENSHI_HAIKI_SHURUIE_SearchDaoCls>();
            this.DENSHI_HAIKI_NAME_SearchDao = DaoInitUtility.GetComponent<DENSHI_HAIKI_NAME_SearchDaoCls>();
            // 20140519 kayo No.734 機能追加 start
            this.TMDDao = DaoInitUtility.GetComponent<TMDDaoCls>();
            // 20140519 kayo No.734 機能追加 end
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            this.msgLogic = new MessageBoxShowLogic();
            //マスタデータを取得
            this.GetPopUpDenshiHaikiNameData();
            this.GetPopUpDenshiHaikiShuruiData();

            this.ConstCls = new ConstCls();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // フォームインスタンスを取得
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                this.headerform = (UIHeader)parentbaseform.headerForm;

                // ボタンを初期化
                this.ButtonInit();

                //footボタン処理イベントを初期化
                this.EventInit();

                //マニフェスト数値フォーマット情報取得
                 mSysInfo = new DBAccessor().GetSysInfo();
                ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
                ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();
                var FormatSettingCustom = "カスタム";


                this.form.cntxt_AmtChokko.FormatSetting = FormatSettingCustom;
                this.form.cntxt_AmtDumikai.FormatSetting = FormatSettingCustom;
                this.form.cntxt_AmtKenbai.FormatSetting = FormatSettingCustom;
                this.form.cntxt_AmtDensi.FormatSetting = FormatSettingCustom;
                this.form.cntxt_AmtTotal.FormatSetting = FormatSettingCustom;
                this.form.cntxt_AmtChokko.CustomFormatSetting = ManifestSuuryoFormat;
                this.form.cntxt_AmtDumikai.CustomFormatSetting = ManifestSuuryoFormat;
                this.form.cntxt_AmtKenbai.CustomFormatSetting = ManifestSuuryoFormat;
                this.form.cntxt_AmtDensi.CustomFormatSetting = ManifestSuuryoFormat;
                this.form.cntxt_AmtTotal.CustomFormatSetting = ManifestSuuryoFormat;

                //検索条件の初期値を設定
                //契約の有無
                this.form.cntxt_KeiyakuFlg.Text = "3";

                // 20140519 kayo No.734 機能追加 start
                this.form.cntxt_LastShobunGenbaFlg.Text = "3";
                // 20140519 kayo No.734 機能追加 end

                //マニ種類
                this.form.cntxt_ManiType.Text = "5";

                //積替と建廃の区分交換
                switch (this.form.cntxt_ManiType.Text)
                {
                    case "2"://積替
                        this.form.cntxt_HiddenManiType.Text = "3";
                        break;

                    case "3"://建廃
                        this.form.cntxt_HiddenManiType.Text = "2";
                        break;

                    default://その他
                        this.form.cntxt_HiddenManiType.Text = this.form.cntxt_ManiType.Text;
                        break;
                }

                //日付範囲
                if (string.IsNullOrEmpty(Properties.Settings.Default.DATE_TIME_TYPE))
                {
                    this.form.cntxt_DatetimeType.Text = "1";
                }
                else
                {
                    this.form.cntxt_DatetimeType.Text = Properties.Settings.Default.DATE_TIME_TYPE;
                }

                if (string.IsNullOrEmpty(Properties.Settings.Default.HIDUKE_FROM))
                {
                    this.form.cDtPicker_StartDay.Value = new DateTime(this.parentbaseform.sysDate.Year, this.parentbaseform.sysDate.Month, 1);
                }
                else
                {
                    this.form.cDtPicker_StartDay.Value = Convert.ToDateTime(Properties.Settings.Default.HIDUKE_FROM);
                }

                if (string.IsNullOrEmpty(Properties.Settings.Default.HIDUKE_TO))
                {
                    this.form.cDtPicker_EndDay.Value = this.parentbaseform.sysDate;
                }
                else
                {
                    this.form.cDtPicker_EndDay.Value = Convert.ToDateTime(Properties.Settings.Default.HIDUKE_TO);
                }

                //廃棄物種類
                this.form.cantxt_HaikibutuTypeCD.Text = String.Empty;
                this.form.ctxt_HaikibutuTypeName.Text = String.Empty;
                switch (this.form.cntxt_HiddenManiType.Text)
                {
                    case "1"://産廃(直行)
                    case "2"://産廃(積替)
                    case "3"://建廃
                    case "4"://電子
                        this.form.cantxt_HaikibutuTypeCD.Enabled = true;
                        this.form.ctxt_HaikibutuTypeName.Enabled = true;
                        break;

                    case "5"://全て
                        this.form.cantxt_HaikibutuTypeCD.Enabled = false;
                        this.form.ctxt_HaikibutuTypeName.Enabled = false;
                        break;

                    default://その他
                        break;
                }

                //廃棄物名称
                this.form.cantxt_HaikibutuCD.Text = String.Empty;
                this.form.ctxt_HaikibutuName.Text = String.Empty;
                switch (this.form.cntxt_HiddenManiType.Text)
                {
                    case "1"://産廃(直行)
                    case "2"://産廃(積替)
                    case "3"://建廃
                    case "4"://電子
                        this.form.cantxt_HaikibutuCD.Enabled = true;
                        this.form.ctxt_HaikibutuName.Enabled = true;
                        break;

                    case "5"://全て
                        this.form.cantxt_HaikibutuCD.Enabled = false;
                        this.form.ctxt_HaikibutuName.Enabled = false;
                        break;

                    default://その他
                        break;
                }

                //報告書分類
                this.form.cantxt_HoukokushoTypeCD.Text = String.Empty;
                this.form.ctxt_HoukokushoTypeName.Text = String.Empty;

                //荷姿
                this.form.cantxt_NisugataCD.Text = String.Empty;
                this.form.ctxt_NisugataName.Text = String.Empty;

                //処分方法
                this.form.cantxt_ShobunHouhouCD.Text = String.Empty;
                this.form.ctxt_ShobunHouhouName.Text = String.Empty;

                //排出事業者
                this.form.cantxt_HaisyutugyoshaCD.Text = String.Empty;
                this.form.ctxt_HaisyutugyoshaName.Text = String.Empty;

                //排出事業場
                this.form.cantxt_HaisyutugenbaCD.Text = String.Empty;
                this.form.ctxt_HaisyutugenbaName.Text = String.Empty;

                //運搬受託者
                this.form.cantxt_UnpangyoshaCD.Text = String.Empty;
                this.form.ctxt_UnpangyoshaName.Text = String.Empty;

                //処分受託者
                this.form.cantxt_ShobungyoshaCD.Text = String.Empty;
                this.form.ctxt_ShobungyoshaName.Text = String.Empty;

                //処分事業場
                this.form.cantxt_ShobunGenbaCD.Text = String.Empty;
                this.form.ctxt_ShobunGenbaName.Text = String.Empty;

                // 20140519 kayo No.liang 機能追加 start
                ////最終処分業者
                //this.form.cantxt_LastShobunGyoShaCD.Text = String.Empty;
                //this.form.ctxt_LastShobunGyoShaName.Text = String.Empty;

                ////最終処分の場所
                //this.form.cantxt_LastShobunGenbaCD.Text = String.Empty;
                //this.form.ctxt_LastShobunGenbaName.Text = String.Empty;
                // 20140519 kayo No.734 機能追加 end

                //減容後数量(t):産廃(直行)
                this.form.cntxt_AmtChokko.Text = String.Empty;

                //減容後数量(t):産廃(積替)
                this.form.cntxt_AmtDumikai.Text = String.Empty;

                //減容後数量(t):建廃
                this.form.cntxt_AmtKenbai.Text = String.Empty;

                //減容後数量(t):電子
                this.form.cntxt_AmtDensi.Text = String.Empty;

                //減容後数量(t):合計数量
                this.form.cntxt_AmtTotal.Text = String.Empty;

                //二次排出数量
                this.form.cntxt_NextOutAmt.Text = String.Empty;

                //一覧にチェックボックスカラムを追加
                this.form.HeaderCheckBoxSupport();

                //紐付け済み一次マニ情報取得
                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start

                //if (!this.form.relationParam.SECOND_SYSTEM_ID.IsNull) //nullでないとき以外に取得
                //{
                //    this.SearchFirstManifestInfo(this.form.relationParam.SECOND_SYSTEM_ID.Value.ToString(), int.Parse(this.form.relationParam.MANI_KBN));
                //}
                //else
                //{
                //    this.SearchFirstManifestInfo("", 0); //空文字で検索して0件でデータテーブル作っておく
                //}

                if (!this.form.relationParam.SECOND_SYSTEM_ID.IsNull) //nullでないとき以外に取得
                {
                    this.SearchFirstManifestInfo(this.form.relationParam.SECOND_DETAIL_SYSTEM_ID.Value.ToString(), int.Parse(this.form.relationParam.MANI_KBN));
                }
                else
                {
                    this.SearchFirstManifestInfo("", 0); //空文字で検索して0件でデータテーブル作っておく
                }

                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

                // 20140519 kayo No.734 機能追加 start
                //if (this.form.relationParam.preResult != null &&
                //    this.form.relationParam.preResult.IsSelected() )
                //{

                //    //検索条件保持
                //    this.form.relationParam.preResult.gamen.Load(this.form);

                //    //紐付済み情報検索
                //    this.dtoMani = new FirstManifestDTOCls();
                //    this.dtoMani.KANRI_ID = new List<string>();
                //    this.dtoMani.DETAIL_SYSTEM_ID = new List<SqlInt64>();

                //    if (this.form.relationParam.preResult.regist_relations != null)
                //    {
                //        //紙用
                //        foreach (var e in this.form.relationParam.preResult.regist_relations)
                //        {
                //            if (e.FIRST_HAIKI_KBN_CD != 4)
                //            {
                //                //紙の場合
                //                this.dtoMani.DETAIL_SYSTEM_ID.Add(e.FIRST_SYSTEM_ID);
                //            }
                //        }
                //    }
                //    //電子用
                //    if (this.form.relationParam.preResult.elecEntriesIns != null)
                //    {
                //        foreach (var e in this.form.relationParam.preResult.elecEntriesIns)
                //        {
                //            this.dtoMani.KANRI_ID.Add(e.KANRI_ID);
                //        }
                //    }
                //    if (this.form.relationParam.preResult.elecEntriesUpd != null)
                //    {
                //        foreach (var e in this.form.relationParam.preResult.elecEntriesUpd)
                //        {
                //            this.dtoMani.KANRI_ID.Add(e.KANRI_ID);
                //        }
                //    }

                //    this.dtoMani.MANI_TYPE = "9"; //OR条件の前側を常にfalseにする

                //    ////自分自身は出さないようにする //自分は二次なので発生しないはず
                //    //this.dtoMani.NEXT_SYSTEM_ID = this.form.relationParam.SECOND_SYSTEM_ID;

                //    //選択されたものも表示させる
                //    this.SearchResult = PaperAndElecManiDao.GetDataForEntity(this.dtoMani);

                //    //検索結果が画面で反映する
                //    this.form.SetDataToDgv(true);
                //}
                //else
                //{
                //    //検索結果が画面で反映する
                //    this.form.SetDataToDgv(true); //全てチェック
                //}
                //検索結果が画面で反映する
                this.form.SetDataToDgv(true); //全てチェック
                // 20140519 kayo No.734 機能追加 end

                //二次排出量
                this.form.cntxt_NextOutAmt.Text = this.form.relationParam.NEXT_HAISYUTU_AMT.ToString();

                // エラーチェック用に二次マニ最終処分場情報を取得
                if ("4".Equals(this.form.relationParam.MANI_KBN))
                {
                    // 電マニ
                    this.LastSbnJyouDataForSecondMani = this.PaperAndElecManiDao.GetLastSbnJyouInfoForElec(this.form.relationParam);
                }
                else
                {
                    // 紙マニ
                    this.LastSbnJyouDataForSecondMani = this.PaperAndElecManiDao.GetLastSbnJyouInfoForPaper(this.form.relationParam);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            // 20140519 kayo No.734 機能追加 start
            //選択外クリアボタン(F1)イベント生成
            parentform.bt_func1.Click += new EventHandler(this.form.DoNondisplay);
            parentform.bt_func1.Font = new Font("ＭＳ ゴシック", 7.5F);
            // 20140519 kayo No.734 機能追加 end

            //検索ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentform.bt_func9);
            parentform.bt_func9.Click += new EventHandler(this.form.RegistManifestRelationInfo);
            parentform.bt_func9.ProcessKbn = PROCESS_KBN.NEW;


            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.FormClose);

            //フォームが閉じたとき
            this.form.FormClosing += new FormClosingEventHandler(this.SetPrevStatus);
            parentform.FormClosed += new FormClosedEventHandler(this.ParentFormClosed);

            //ESCテキストイベント生成
            //parentform.txb_process.KeyDown += new KeyEventHandler(this.form.txb_process_Enter);
            //明細フォーマット
            this.form.cdgv_FirstMani.CellFormatting += new DataGridViewCellFormattingEventHandler(this.form.cdgv_FirstMani_CellFormatting);




            //プロパティ設定
            this.form.cantxt_HaisyutugyoshaCD.DisplayItemName = "排出事業者";
            this.form.cantxt_HaisyutugenbaCD.DisplayItemName = "排出事業場";
            this.form.cantxt_UnpangyoshaCD.DisplayItemName = "運搬受託者";
            this.form.cantxt_ShobungyoshaCD.DisplayItemName = "処分受託者";
            this.form.cantxt_ShobunGenbaCD.DisplayItemName = "処分事業場";
            // 20140519 kayo No.734 機能追加 start
            //this.form.cantxt_LastShobunGyoShaCD.DisplayItemName = "最終処分業者";
            //this.form.ctxt_LastShobunGenbaName.DisplayItemName = "最終処分の場所";
            // 20140519 kayo No.734 機能追加 end

            this.form.cantxt_HaisyutugyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_HaisyutugenbaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_UnpangyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_ShobungyoshaCD.FocusOutCheckMethod.Clear();
            this.form.cantxt_ShobunGenbaCD.FocusOutCheckMethod.Clear();
            // 20140519 kayo No.734 機能追加 start
            //this.form.cantxt_LastShobunGyoShaCD.FocusOutCheckMethod.Clear();
            //this.form.ctxt_LastShobunGenbaName.FocusOutCheckMethod.Clear();
            // 20140519 kayo No.734 機能追加 end


            //ポップアップ設定

            //排出
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_HaisyutugyoshaCD,
                this.form.ctxt_HaisyutugyoshaName,
                null,
                this.form.cantxt_HaisyutugenbaCD,
                this.form.ctxt_HaisyutugenbaName,
                null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                false, true, true);
            //運搬
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_UnpangyoshaCD,
                this.form.ctxt_UnpangyoshaName,
                null,
                null,
                null,
                null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                false, true, true);

            //処分
            Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.form.cantxt_ShobungyoshaCD,
                this.form.ctxt_ShobungyoshaName,
                null,
                this.form.cantxt_ShobunGenbaCD,
                this.form.ctxt_ShobunGenbaName,
                null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA,
                false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA,
                false, true, true);

            // 20140519 kayo No.734 機能追加 start
            //最終処分
            //Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
            //    this.form.cantxt_LastShobunGyoShaCD,
            //    this.form.ctxt_LastShobunGyoShaName,
            //    null,
            //    this.form.cantxt_LastShobunGenbaCD,
            //    this.form.ctxt_LastShobunGenbaName,
            //    null, Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.KAMI,
            //    Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_JUTAKUSHA,
            //    false, false, Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SAISHUU_SHOBUNJOU,
            //    false, true, true);
            // 20140519 kayo No.734 機能追加 end

            // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 start
            this.form.cdgv_FirstMani.CellContentClick += new DataGridViewCellEventHandler(this.form.cdgv_FirstMani_CellContentClick);
            this.form.cdgv_FirstMani.CellValueChanged += new DataGridViewCellEventHandler(this.form.cdgv_FirstMani_CellValueChanged);
            // 20140606 kayo 不具合#4693 チェックをつけているもののサマリーとする修正 end

            this.form.cdgv_FirstMani.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.form.cdgv_FirstMani_ColumnHeaderMouseClick);

            /// 20141226 Houkakou 「マニフェスト紐付画面」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.cDtPicker_EndDay.MouseDoubleClick += new MouseEventHandler(cDtPicker_EndDay_MouseDoubleClick);
            /// 20141226 Houkakou 「マニフェスト紐付画面」のダブルクリックを追加する　end


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Close時のRelationResultセット処理
        /// </summary>
        /// <param name="e"></param>
        private void ParentFormClosed(object sender, EventArgs e)
        {
            if (this.parentbaseform.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                this.SetPrevStatus(sender, e);
                this.form.RelationResult.Cancel();
            }
        }

        /// <summary>
        /// 前回値をセッティングファイルに保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            // 日付範囲
            if (!String.IsNullOrEmpty(this.form.cntxt_DatetimeType.Text.Trim()))
            {
                Properties.Settings.Default.DATE_TIME_TYPE = this.form.cntxt_DatetimeType.Text;
            }

            // 日付From
            DateTime resultDt;
            if (!String.IsNullOrEmpty(this.form.cDtPicker_StartDay.Text.Trim()) && DateTime.TryParse(this.form.cDtPicker_StartDay.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.HIDUKE_FROM = this.form.cDtPicker_StartDay.Text;
            }

            // 日付To
            if (!String.IsNullOrEmpty(this.form.cDtPicker_EndDay.Text.Trim()) && DateTime.TryParse(this.form.cDtPicker_EndDay.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.HIDUKE_TO = this.form.cDtPicker_EndDay.Text;
            }

            // 保存
            Properties.Settings.Default.Save();
        }


        #endregion

        #region データ検索処理

        /// <summary>
        /// 二次マニより紐付された一次マニフェスト情報取得処理　※初回のみ呼ばれる 最初は既存の紐付情報が表示される。
        /// </summary>
        /// <param name="systemId"></param>
        [Transaction]
        public virtual int SearchFirstManifestInfo(string systemId, int haikiKbnCd)
        {
            LogUtility.DebugMethodStart(systemId, haikiKbnCd);

            try
            {

                this.TotalResults = new DataTable();
                //検索結果テーブル
                this.RelationResult = new DataTable();

                //検索条件の設定
                this.dtoHimo.NEXT_SYSTEM_ID = systemId;
                this.dtoHimo.NEXT_HAIKI_KBN_CD = haikiKbnCd;

                //検索実行
                this.RelationResult = ManiRealationDao.GetDataForEntity(dtoHimo);

                // 20140519 kayo No.734 機能追加 start
                // 既存バグ修正、データを取得するではなく、データの構造を抽出することは目的
                this.SearchResult = PaperAndElecManiDao.GetDataForEntity(new FirstManifestDTOCls());
                // 20140519 kayo No.734 機能追加 end

                //エンティティ取得
                if (string.IsNullOrEmpty(systemId))
                {
                    this.currentRelation = null;
                }
                else
                {
                    this.currentRelation = PaperAndElecManiDao.SelectCurrent(SqlInt64.Parse(systemId), (SqlInt16)haikiKbnCd, false);
                }


            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(this.TotalResults.Rows.Count);

            //行数の戻る
            return this.TotalResults.Rows.Count;

        }

        /// <summary>
        /// 検索ボタンクリック後ロジック処理
        /// </summary>
        [Transaction]
        public virtual int SearchLogic()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.isSearchErr = false;

                //合計結果テーブル
                this.TotalResults = new DataTable();

                //検索結果テーブル
                this.SearchResult = new DataTable();

                //検索実行
                if (dtoMani.MANI_TYPE == "5")//全ての場合
                {
                    dtoMani.paper = 1;
                    dtoMani.elec = 1;
                    this.SearchResult = PaperAndElecManiDao.GetDataForEntity(dtoMani);
                }
                else if (dtoMani.MANI_TYPE == "4")//電子の場合
                {
                    dtoMani.paper = 0;
                    dtoMani.elec = 1;
                    this.SearchResult = PaperAndElecManiDao.GetDataForEntity(dtoMani);
                    //this.SearchResult = ElecManiDao.GetDataForEntity(dtoMani);
                }
                else//紙マニの場合
                {
                    dtoMani.paper = 1;
                    dtoMani.elec = 0;
                    this.SearchResult = PaperAndElecManiDao.GetDataForEntity(dtoMani);
                    //this.SearchResult = PaperManiDao.GetDataForEntity(dtoMani);
                }
                //全てDBNULL許可
                for (int i = 0; i < this.SearchResult.Columns.Count; i++)
                {
                    this.SearchResult.Columns[i].AllowDBNull = true;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchLogic", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isSearchErr = true;
            }
            LogUtility.DebugMethodEnd();

            //行数の戻る
            return this.TotalResults.Rows.Count;

        }
        #endregion

        #region マスタコードのチェック処理初期化（DataTableの準備）
        /// <summary>
        /// 電子廃棄物名称選択ポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public void GetPopUpDenshiHaikiNameData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                DenshiHaikiNameCodeResult = new DataTable();
                //DenshiHaikiNameCodeResult.TableName = "電子廃棄物名称";
                SearchExistDTO = new SearchExistDTOCls();
                DenshiHaikiNameCodeResult = DENSHI_HAIKI_NAME_SearchDao.GetDataForEntity(SearchExistDTO);

                // 列名とデータソース設
                this.form.cantxt_ElecHaikibutuCD.PopupDataHeaderTitle = new string[] { "電子廃棄物CD", "電子廃棄物名称" };
                this.form.cantxt_ElecHaikibutuCD.PopupDataSource = DenshiHaikiNameCodeResult;
                //検索画面のタイトルを設定
                this.form.cantxt_ElecHaikibutuCD.PopupDataSource.TableName = "電子廃棄物名称";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 電子廃棄物種類選択ポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public void GetPopUpDenshiHaikiShuruiData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                DenshiHaikiShuruiCodeResult = new DataTable();
                DenshiHaikiShuruiCodeResult.TableName = "";
                SearchExistDTO = new SearchExistDTOCls();
                DenshiHaikiShuruiCodeResult = DENSHI_HAIKI_SHURUIE_SearchDao.GetDataForEntity(SearchExistDTO);

                // 列名とデータソース設
                this.form.cantxt_ElecHaikibutuTypeCD.PopupDataHeaderTitle = new string[] { "電子廃棄物種類CD", "電子廃棄物種類名称" };
                this.form.cantxt_ElecHaikibutuTypeCD.PopupDataSource = DenshiHaikiShuruiCodeResult;
                //検索画面のタイトルを設定
                this.form.cantxt_ElecHaikibutuTypeCD.PopupDataSource.TableName = "電子廃棄物種類";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 紐付データ登録
        /// <summary>
        /// 紐付情報を登録する
        /// </summary>
        /// <returns></returns>
        public virtual int RegistManiRelationInfo()
        {
            int Cnt = 0;
            this.form.isRegistErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                List<RelationInfo_DTOCls> lstRelationInfo = new List<RelationInfo_DTOCls>();
                //チェックした行のシステムIDを収集
                for (int i = 0; i < this.form.cdgv_FirstMani.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.form.cdgv_FirstMani.Rows[i].Cells[0];
                    Boolean flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == true)
                    {
                        //紐付対象のシステムIDリストを取得
                        if (this.form.cdgv_FirstMani.Rows[i].Tag != null)
                        {
                            lstRelationInfo.Add((RelationInfo_DTOCls)this.form.cdgv_FirstMani.Rows[i].Tag);
                        }
                    }
                }

                //仕様変更2013/11/08 確認メッセージ追加
                if (lstRelationInfo.Count > 0 ||  //紐付がある場合
                    this.RelationResult.Rows.Count > 0   //元紐付有で、全部クリアした場合
                    // 20140519 kayo No.734 機能追加 start
                    //|| (this.form.relationParam.preResult != null && this.form.relationParam.preResult.IsSelected()) //一度選択したが、、改めてクリアした場合
                    // 20140519 kayo No.734 機能追加 end
                    )
                {
                    // １次マニと２次マニの最終処分終了日が一致しているかチェックする。
                    // ２次マニの情報を取得する。
                    DataTable SecondPaperInfo = new DataTable();
                    if (this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(this.form.relationParam.MANI_KBN))
                    {
                        SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecondForElecMani(this.form.relationParam);
                    }
                    else
                    {
                        SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecond(this.form.relationParam.SECOND_DETAIL_SYSTEM_ID);
                    }

                    // 電子の１次マニであること
                    // １次マニ/２次マニともに最終処分終了日が設定されていること。
                    bool lastSbnDateNoSame = false;
                    foreach (var r in lstRelationInfo)
                    {
                        // 電子１次であること
                        if (r.MANIFEST_TYPE.Equals(ConstCls.MANIFEST_TYPE_DENSHI))
                        {
                            // DT_R13を検索
                            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
                            DT_R13[] r13list = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = r.KANRI_ID, SEQ = r.LATEST_SEQ });
                            List<string> dateList = new List<string>();
                            foreach (var r13 in r13list)
                            {
                                dateList.Add(r13.LAST_SBN_END_DATE);
                            }

                            // 一次の日付リストに二次の日付が含まれているかチェックする。
                            if (dateList.Count > 0)
                            {
                                for (int i = 0; i < SecondPaperInfo.Rows.Count; i++)
                                {
                                    string firstManiLastSbnDate = "";
                                    if (this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(this.form.relationParam.MANI_KBN))
                                    {
                                        firstManiLastSbnDate = SecondPaperInfo.Rows[i]["HUKUSUU_LAST_SBN_END_DATE"].ToString();
                                    }
                                    else
                                    {
                                        firstManiLastSbnDate = SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString();
                                        if (!string.IsNullOrEmpty(firstManiLastSbnDate))
                                        {
                                            firstManiLastSbnDate = Convert.ToDateTime(firstManiLastSbnDate).ToString("yyyyMMdd");
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(firstManiLastSbnDate) && !dateList.Contains(firstManiLastSbnDate))
                                    {
                                        lastSbnDateNoSame = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    // 一致している日付がない場合、メッセージを表示する。
                    if (lastSbnDateNoSame)
                    {
                        if (this.msgLogic.MessageBoxShow("C046", "1次の最終処分終了日と2次の最終処分終了日に差異があります。\n登録") == DialogResult.Yes)
                        {
                            if (this.msgLogic.MessageBoxShow("C046", "画面の内容で紐付") == DialogResult.Yes)
                            {
                                //紐付情報の登録処理を行う
                                Cnt = RegistRelationInfo(lstRelationInfo, this.RelationResult);
                            }
                        }
                    }
                    else
                    {
                        if (this.msgLogic.MessageBoxShow("C046", "画面の内容で紐付") == DialogResult.Yes)
                        {
                            //紐付情報の登録処理を行う
                            Cnt = RegistRelationInfo(lstRelationInfo, this.RelationResult);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistManiRelationInfo", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.form.isRegistErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(Cnt);
            }
            return Cnt;
        }
        /// <summary>
        /// 紐付情報のインサート処理
        /// </summary>
        /// <param name="lstRelation"></param>
        /// <returns></returns>
        [Transaction]
        public virtual int RegistRelationInfo(List<RelationInfo_DTOCls> lstRelationInfo, DataTable Result)
        {
            LogUtility.DebugMethodStart(lstRelationInfo, Result);

            try
            {

                //旧紐付情報取得(select時に取得済）
                //this.currentRelation

                //新紐付のSEQ取得

                int seq;
                if (this.form.relationParam.SECOND_SYSTEM_ID.IsNull)
                {
                    seq = 1; //新規の場合は1でOK
                }
                else
                {

                    //最大値取得（すべて削除されていると 旧紐付が0件でSEQが2以上から開始とかがあり得る）
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                    //var dt = this.MaxSeqGetDao.GetDataForEntity(new HIMODUKE_DTOCls() { NEXT_SYSTEM_ID = this.form.relationParam.SECOND_SYSTEM_ID.ToString(), NEXT_HAIKI_KBN_CD = int.Parse(this.form.relationParam.MANI_KBN) });
                    var dt = this.MaxSeqGetDao.GetDataForEntity(new HIMODUKE_DTOCls() { NEXT_SYSTEM_ID = this.form.relationParam.SECOND_DETAIL_SYSTEM_ID.ToString(), NEXT_HAIKI_KBN_CD = int.Parse(this.form.relationParam.MANI_KBN) });
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
                    if (dt.Rows.Count > 0 && dt.Rows[0]["MAXSEQ"] != DBNull.Value)
                    {
                        seq = Convert.ToInt32(dt.Rows[0]["MAXSEQ"]) + 1; //MAXの次の番号を利用
                    }
                    else
                    {
                        seq = 1; //nullの場合は1から
                    }
                }


                //新紐付作成
                var newRelation = new List<T_MANIFEST_RELATION>();
                var tme = new Dictionary<SqlInt64, T_MANIFEST_ENTRY>(); //複数明細の場合1つで良いので重複排除が必要
                var r18exIns = new List<DT_R18_EX>();  //電子は1明細なので重複考慮不要だが　更新情報があるのでInsとDelの二種必要
                var r18exUpd = new List<DT_R18_EX>();
                // 20140519 kayo No.734 機能追加 start
                //var listFirstEntryForUpdate = new List<T_MANIFEST_ENTRY>(); ※要らない処理が、ソースが残りたいと思って、とりあえずコメントアウト
                var listFirstDetailForUpdate = new List<T_MANIFEST_DETAIL>();
                DataTable SecondPaperInfo = new DataTable();
                if (this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(this.form.relationParam.MANI_KBN))
                {
                    SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecondForElecMani(this.form.relationParam);
                }
                else
                {
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                    //SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecond(this.form.relationParam.SECOND_SYSTEM_ID);
                    SecondPaperInfo = PaperAndElecManiDao.GetDataForEntitySecond(this.form.relationParam.SECOND_DETAIL_SYSTEM_ID);
                    // 2016.11.23 chinkeigen マニフェスト紐付 #101095  end
                }
                // 20140519 kayo No.734 機能追加 end

                int cnt = 0;
                foreach (var r in lstRelationInfo)
                {
                    cnt++;

                    var rel = new T_MANIFEST_RELATION()
                    {

                        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                        //NEXT_SYSTEM_ID = this.form.relationParam.SECOND_SYSTEM_ID,
                        NEXT_SYSTEM_ID = this.form.relationParam.SECOND_DETAIL_SYSTEM_ID,
                        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
                        SEQ = seq,
                        REC_SEQ = cnt,
                        NEXT_HAIKI_KBN_CD = SqlInt16.Parse(this.form.relationParam.MANI_KBN),
                        //FIRST_SYSTEM_ID = SqlInt64.Parse(r.FIRST_SYSTEM_ID), //紙と電子で値がことなる
                        FIRST_HAIKI_KBN_CD = SqlInt16.Parse(r.MANIFEST_TYPE),
                        DELETE_FLG = false
                    };

                    // 電マニの場合は最終処分終了日等は別機能で登録させるため紐付機能では更新しない。
                    if (!this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(r.MANIFEST_TYPE))
                    {
                        // 20140519 kayo No.734 機能追加 start
                        var dtl = TMDDao.GetDataForEntity(r.TME_SYSTEM_ID, r.TME_SEQ, SqlInt64.Parse(r.FIRST_SYSTEM_ID));
                        // 最終処分終了日
                        if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString()))
                        {
                            DateTime date;
                            if (DateTime.TryParseExact(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString(),
                                                "yyyyMMdd",
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None,
                                                out date))
                            {
                                // 電マニの最終処分日付の形式にあわせる
                                dtl.LAST_SBN_END_DATE = date;
                            }
                            else
                            {
                                dtl.LAST_SBN_END_DATE = Convert.ToDateTime(SecondPaperInfo.Rows[0]["LAST_SBN_END_DATE"].ToString());
                            }
                        }
                        else
                        {
                            dtl.LAST_SBN_END_DATE = SqlDateTime.Null;
                        }

                        if (int.Parse(SecondPaperInfo.Rows[0]["LINE_COUNT"].ToString()) <= 1)
                        {
                            // 最終処分業者
                            if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString()))
                            {
                                dtl.LAST_SBN_GYOUSHA_CD = SecondPaperInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                            }
                            else
                            {
                                dtl.LAST_SBN_GYOUSHA_CD = null;
                            }

                            // 最終処分現場
                            if (!string.IsNullOrEmpty(SecondPaperInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                            {
                                dtl.LAST_SBN_GENBA_CD = SecondPaperInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                            }
                            else
                            {
                                dtl.LAST_SBN_GENBA_CD = null;
                            }
                        }
                        else
                        {
                            // 二次マニフェストの実績が2行以上ある場合
                            dtl.LAST_SBN_GYOUSHA_CD = null;
                            dtl.LAST_SBN_GENBA_CD = null;
                            SqlInt64 SystemID = SqlInt64.Parse(SecondPaperInfo.Rows[0]["SYSTEM_ID"].ToString());
                            SqlInt32 SEQ = SqlInt32.Parse(SecondPaperInfo.Rows[0]["SEQ"].ToString());
                            DataTable SecondPaperLastsbnInfo = new DataTable();
                            if (this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(this.form.relationParam.MANI_KBN))
                            {
                                // 電マニ
                                SecondPaperLastsbnInfo = PaperAndElecManiDao.GetDataForEntitySecondLastSbnForElecMani(SystemID, SEQ);
                            }
                            else
                            {
                                // 紙マニ
                                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                                SqlInt64 DetailSystemID = SqlInt64.Parse(SecondPaperInfo.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

                                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
                                //SecondPaperLastsbnInfo = PaperAndElecManiDao.GetDataForEntitySecondLastSbn(SystemID, SEQ);
                                SecondPaperLastsbnInfo = PaperAndElecManiDao.GetDataForEntitySecondLastSbn(DetailSystemID, SEQ);
                                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
                            }
                            if (SecondPaperLastsbnInfo.Rows.Count <= 1)
                            {
                                // 全ての行で最終処分業者、最終処分場所が一致している場合
                                if (SecondPaperLastsbnInfo.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString())
                                        && !string.IsNullOrEmpty(SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString()))
                                    {
                                        dtl.LAST_SBN_GYOUSHA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                        dtl.LAST_SBN_GENBA_CD = SecondPaperLastsbnInfo.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                // 最終処分業者、最終処分場所が異なる行がある場合
                                DataView SecondInfoDataView = new DataView(SecondPaperLastsbnInfo);
                                DataTable tblLastSbnGyousha = SecondInfoDataView.ToTable("DistinctTable", true, new string[] { "LAST_SBN_GYOUSHA_CD" });
                                if (tblLastSbnGyousha.Rows.Count <= 1)
                                {
                                    // 最終処分業者が全て同じ場合
                                    dtl.LAST_SBN_GYOUSHA_CD = tblLastSbnGyousha.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
                                }
                            }
                        }
                        //二次マニフェストは原本もしくは一件実績しかない場合
                        listFirstDetailForUpdate.Add(dtl);
                        // 20140519 kayo No.734 機能追加 end 

                        //紙はシステムIDをそのまま利用
                        rel.FIRST_SYSTEM_ID = SqlInt64.Parse(r.FIRST_SYSTEM_ID);
                        //ENTRY作成
                        if (!tme.ContainsKey(r.TME_SYSTEM_ID))
                        {
                            tme.Add(r.TME_SYSTEM_ID, new T_MANIFEST_ENTRY()
                            {
                                SYSTEM_ID = r.TME_SYSTEM_ID,
                                SEQ = r.TME_SEQ,
                                TIME_STAMP = r.TME_TIME_STAMP,
                                MANIFEST_ID = r.MANIFEST_ID
                            });

                        }
                    }
                    else
                    {

                        //電子はR18EXを作り直すのでSYSTEM_IDを取り直す

                        //旧18
                        if (!r.DT_R18_EX_SYSTEM_ID.IsNull)
                        {
                            rel.FIRST_SYSTEM_ID = r.DT_R18_EX_SYSTEM_ID;

                        }
                        else
                        {
                            //新18
                            var newR18EX = new DT_R18_EX();
                            //システムIDの採番
                            Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                            newR18EX.SYSTEM_ID = dba.createSystemId((int)DENSHU_KBN.DENSHI_MANIFEST);
                            newR18EX.SEQ = 1;
                            newR18EX.KANRI_ID = r.KANRI_ID; ;
                            newR18EX.MANIFEST_ID = r.MANIFEST_ID;

                            //R18から一部データ引用
                            DataTable tblR18 = new DataTable();
                            SearchExistDto = new SearchExistDTOCls();
                            SearchExistDto.KANRI_ID = r.KANRI_ID;
                            //最新データ検索
                            tblR18 = DT_R18SearchDao.GetDataForEntity(SearchExistDto);
                            newR18EX.HAIKI_NAME_CD = Convert.ToString(tblR18.Rows[0]["HAIKI_NAME_CD"]);  //廃棄物名称CD
                            newR18EX.KANSAN_SUU = tblR18.Rows[0]["KANSAN_SUU"] is DBNull ? SqlDecimal.Null : (SqlDecimal)tblR18.Rows[0]["KANSAN_SUU"];  //換算後数量

                            r18exIns.Add(newR18EX);

                            rel.FIRST_SYSTEM_ID = newR18EX.SYSTEM_ID; //採番した値

                        }
                    }

                    newRelation.Add(rel);
                }

                //登録
                this.form.RelationResult.Selected(newRelation.ToArray(), this.currentRelation, tme.Values.ToArray(), r18exIns.ToArray(), r18exUpd.ToArray(), new GamenDTOCls(this.form));
                // 20140519 kayo No.734 機能追加 start
                using (Transaction tran = new Transaction())
                {
                    this.form.RelationResult.Regist(tran, this.form.relationParam.SECOND_SYSTEM_ID);

                    // 20140619 kayo 不具合#4597 紐付けを解除したが、最終XXX情報はそのまま残ってしまう start
                    if (this.currentRelation != null)
                    {
                        List<RelationInfo_DTOCls> maniTable = new List<RelationInfo_DTOCls>();
                        foreach (DataGridViewRow dgr in this.form.cdgv_FirstMani.Rows)
                        {
                            if (!Convert.ToBoolean(dgr.Cells[0].Value))
                            {
                                maniTable.Add((RelationInfo_DTOCls)dgr.Tag);
                            }
                        }

                        foreach (T_MANIFEST_RELATION r in this.currentRelation)
                        {
                            // 1次電子マニフェストの場合、最終XXX情報の初期化はしない。
                            if (r.FIRST_HAIKI_KBN_CD == 4) continue;

                            var result = from n in maniTable where n.FIRST_SYSTEM_ID == r.FIRST_SYSTEM_ID.ToString() && n.MANIFEST_TYPE == r.FIRST_HAIKI_KBN_CD.ToString() select n;

                            if (result.Count() <= 0) continue;
                            RelationInfo_DTOCls searchDto = (RelationInfo_DTOCls)result.First();

                            var dtl = TMDDao.GetDataForEntity(searchDto.TME_SYSTEM_ID, searchDto.TME_SEQ, SqlInt64.Parse(searchDto.FIRST_SYSTEM_ID));
                            dtl.LAST_SBN_END_DATE = SqlDateTime.Null;
                            dtl.LAST_SBN_GENBA_CD = null;
                            dtl.LAST_SBN_GYOUSHA_CD = null;
                            TMDDao.Update(dtl);
                        }
                    }
                    // 20140619 kayo 不具合#4597 紐付けを解除したが、最終XXX情報はそのまま残ってしまう end

                    // 一次紙マニフェスト情報明細更新（最終処分場所、最終処分終了日更新）
                    if (listFirstDetailForUpdate != null)
                    {
                        foreach (var r in listFirstDetailForUpdate)
                        {
                            TMDDao.Update(r);
                        }
                    }

                    tran.Commit();
                }
                // 20140519 kayo No.734 機能追加 end

                // 一次電マニの最終処分情報更新
                this.UpdateLastSbnInfoForFirstElecMani(this.currentRelation, lstRelationInfo);

//<<<<<<< .working
//=======
//                foreach (var r in lstDeleteRelationInfo)
//                {
//                    var rel = new T_MANIFEST_RELATION()
//                    {
//                        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
//                        //NEXT_SYSTEM_ID = this.form.relationParam.SECOND_SYSTEM_ID,
//                        NEXT_SYSTEM_ID = this.form.relationParam.SECOND_DETAIL_SYSTEM_ID,
//                        // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end
//                        NEXT_HAIKI_KBN_CD = SqlInt16.Parse(this.form.relationParam.MANI_KBN),
//                        FIRST_SYSTEM_ID = SqlInt64.Parse(r.FIRST_SYSTEM_ID),
//                        FIRST_HAIKI_KBN_CD = SqlInt16.Parse(r.MANIFEST_TYPE)
//                    };
//                    this.ManiRealationDao.DeleteRelationData(rel);
//                }

//>>>>>>> .merge-right.r90884
                msgLogic.MessageBoxShow("I001", "マニフェスト紐付け");

                this.form.DialogResult = DialogResult.OK;
                this.parentbaseform.DialogResult = DialogResult.OK;

                this.form.FormClose(null, null); //閉じる

//<<<<<<< .working
//=======
//                this.form.RelationResult = new ManiRelrationResult(this.form.relationParam);

//                //紐付け済み一次マニ情報取得
//                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 start
//                //if (!this.form.relationParam.SECOND_SYSTEM_ID.IsNull) //nullでないとき以外に取得
//                //{
//                //    this.SearchFirstManifestInfo(this.form.relationParam.SECOND_SYSTEM_ID.Value.ToString(), int.Parse(this.form.relationParam.MANI_KBN));
//                //}
//                //else
//                //{
//                //    this.SearchFirstManifestInfo("", 0); //空文字で検索して0件でデータテーブル作っておく
//                //}

//                if (!this.form.relationParam.SECOND_DETAIL_SYSTEM_ID.IsNull) //nullでないとき以外に取得
//                {
//                    this.SearchFirstManifestInfo(this.form.relationParam.SECOND_DETAIL_SYSTEM_ID.Value.ToString(), int.Parse(this.form.relationParam.MANI_KBN));
//                }
//                else
//                {
//                    this.SearchFirstManifestInfo("", 0); //空文字で検索して0件でデータテーブル作っておく
//                }

//>>>>>>> .merge-right.r90884
//                // 2016.11.23 chinkeigen マニフェスト紐付 #101095 end

                LogUtility.DebugMethodEnd(0);
                return 0;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd(lstRelationInfo.Count);
            return lstRelationInfo.Count;
        }

        #region 一次電マニの最終処分情報更新
        /// <summary>
        /// 一次電マニの最終処分情報更新
        /// 既に紐付いているマニ、画面で新たに紐付けたマニの両方を更新する
        /// </summary>
        /// <param name="currentRelation">既に紐付いているマニ</param>
        /// <param name="lstRelationInfo">画面で選択したマニ</param>
        internal void UpdateLastSbnInfoForFirstElecMani(T_MANIFEST_RELATION[] currentRelation, List<RelationInfo_DTOCls> lstRelationInfo)
        {
            #region 一次電マニ更新用

            // 更新用変数
            var mfTocList = new List<DT_MF_TOC>();
            var r18List = new List<DT_R18>();
            var r19List = new List<DT_R19[]>();
            var r02List = new List<DT_R02[]>();
            var r04List = new List<DT_R04[]>();
            var r05List = new List<DT_R05[]>();
            var r08List = new List<DT_R08[]>();
            var R13List = new List<DT_R13[]>();
            var r18ExList = new List<DT_R18_EX>();
            var r19ExList = new List<DT_R19_EX[]>();
            var r04ExList = new List<DT_R04_EX[]>();
            var r08ExList = new List<DT_R08_EX[]>();
            var oldR13ExList = new List<DT_R13_EX[]>();

            var newR13ExList = new List<DT_R13_EX[]>();

            // 一次電マニの重複更新防止用変数
            var executedKanriIds = new List<string>();
            #region 電マニ用Dao生成
            var mfTocDao = DaoInitUtility.GetComponent<CommonDT_MF_TOCDaoCls>();
            var r18Dao = DaoInitUtility.GetComponent<CommonDT_R18DaoCls>();
            var r19Dao = DaoInitUtility.GetComponent<CommonDT_R19DaoCls>();
            var r02Dao = DaoInitUtility.GetComponent<CommonDT_R02DaoCls>();
            var r04Dao = DaoInitUtility.GetComponent<CommonDT_R04DaoCls>();
            var r05Dao = DaoInitUtility.GetComponent<CommonDT_R05DaoCls>();
            var r06Dao = DaoInitUtility.GetComponent<CommonDT_R06DaoCls>();
            var r08Dao = DaoInitUtility.GetComponent<CommonDT_R08DaoCls>();
            var r13Dao = DaoInitUtility.GetComponent<CommonDT_R13DaoCls>();
            var r18ExDao = DaoInitUtility.GetComponent<CommonDT_R18_EXDaoCls>();
            var r19ExDao = DaoInitUtility.GetComponent<CommonDT_R19_EXDaoCls>();
            var r04ExDao = DaoInitUtility.GetComponent<CommonDT_R04_EXDaoCls>();
            var r08ExDao = DaoInitUtility.GetComponent<CommonDT_R08_EXDaoCls>();
            var r13ExDao = DaoInitUtility.GetComponent<CommonDT_R13_EXDaoCls>();
            #endregion

            #endregion

            foreach (var r in lstRelationInfo)
            {
                // 電マニの場合は最終処分終了日等は別機能で登録させるため紐付機能では更新しない。
                if (this.ConstCls.MANIFEST_TYPE_DENSHI.Equals(r.MANIFEST_TYPE))
                {
                    #region 一次電マニ更新用
                    // 一次電マニの更新情報をセット
                    if (!r.DT_R18_EX_SYSTEM_ID.IsNull
                        && !r.DT_R18_EX_SEQ.IsNull
                        && !string.IsNullOrEmpty(r.KANRI_ID)
                        && !executedKanriIds.Contains(r.KANRI_ID))
                    {
                        executedKanriIds.Add(r.KANRI_ID);
                        DT_R18_EX oldR18Ex = r18ExDao.GetDataForSystemId(r.DT_R18_EX_SYSTEM_ID);

                        // 一次電マニの場合は紙マニと違い、一括で紐付いている二次マニを参照してDT_R13, DT_R13_EXを作成しないとならい
                        #region 現在のデータを取得
                        DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = r.KANRI_ID });
                        SqlDecimal latestSeq = mfToc.LATEST_SEQ;
                        DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = r.KANRI_ID, SEQ = latestSeq });
                        DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = oldR18Ex.SYSTEM_ID, SEQ = oldR18Ex.SEQ });
                        #endregion

                        if (mfToc.KIND.IsNull
                            || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)
                        {
                            // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                            // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                            continue;
                        }

                        SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                        SqlInt32 updateExSeq = oldR18Ex.SEQ + 1;

                        // DT_R13, DT_R13_EXの更新データセット
                        this.CreateUpdateElecMani(r.KANRI_ID, oldR18Ex.SYSTEM_ID, r.DT_R18_EX_SYSTEM_ID, updateLatestSeq, updateExSeq, r18, oldR13Ex,
                                                ref r18List, ref R13List, ref oldR13ExList, ref newR13ExList);

                        #region DT_R13, DT_R13_EX以外の更新データをセット
                        mfToc.LATEST_SEQ = updateLatestSeq;
                        mfTocList.Add(mfToc);

                        foreach (var tempR19 in r19)
                        {
                            tempR19.SEQ = updateLatestSeq;
                        }
                        r19List.Add(r19);

                        foreach (var tempR02 in r02)
                        {
                            tempR02.SEQ = updateLatestSeq;
                        }
                        r02List.Add(r02);

                        foreach (var tempR04 in r04)
                        {
                            tempR04.SEQ = updateLatestSeq;
                        }
                        r04List.Add(r04);

                        foreach (var tempR05 in r05)
                        {
                            tempR05.SEQ = updateLatestSeq;
                        }
                        r05List.Add(r05);

                        foreach (var tempR08 in r08)
                        {
                            tempR08.SEQ = updateLatestSeq;
                        }
                        r08List.Add(r08);

                        r18ExList.Add(oldR18Ex);
                        r19ExList.Add(oldR19Ex);
                        r04ExList.Add(oldR04Ex);
                        r08ExList.Add(oldR08Ex);
                        #endregion
                    }
                    #endregion
                }
            }

            #region 紐付け削除時用データ作成
            foreach (T_MANIFEST_RELATION r in currentRelation)
            {
                if (r.FIRST_HAIKI_KBN_CD != 4) continue;

                #region 現在のデータを取得
                DT_R18_EX oldR18Ex = r18ExDao.GetDataForSystemId(r.FIRST_SYSTEM_ID);
                if (oldR18Ex == null)
                {
                    // DT_R18_EX.SYSTEM_IDと紐付け後、DT_R18_MIXを作成している場合、上記メソッドでは取得できないので
                    // DT_R18_EXを再取得
                    oldR18Ex = r18ExDao.GetDataForExSystemId(r.FIRST_SYSTEM_ID);
                }

                if (oldR18Ex == null)
                {
                    // 取得できない場合は削除されている可能性があるため、スキップ
                    continue;
                }

                string kanriId = oldR18Ex.KANRI_ID;

                // 更新用の方で一度処理している場合は、重複して更新しまう
                if (executedKanriIds.Contains(kanriId)) continue;

                executedKanriIds.Add(kanriId);

                SqlInt64 exSystemId = oldR18Ex.SYSTEM_ID;
                SqlInt32 exSeq = oldR18Ex.SEQ;
                DT_MF_TOC mfToc = mfTocDao.GetDataForEntity(new DT_MF_TOC() { KANRI_ID = kanriId });
                SqlDecimal latestSeq = mfToc.LATEST_SEQ;
                DT_R18 r18 = r18Dao.GetDataForEntity(new DT_R18() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R19[] r19 = r19Dao.GetDataForEntity(new DT_R19() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R02[] r02 = r02Dao.GetDataForEntity(new DT_R02() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R04[] r04 = r04Dao.GetDataForEntity(new DT_R04() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R05[] r05 = r05Dao.GetDataForEntity(new DT_R05() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R08[] r08 = r08Dao.GetDataForEntity(new DT_R08() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R13[] r13 = r13Dao.GetDataForEntity(new DT_R13() { KANRI_ID = kanriId, SEQ = latestSeq });
                DT_R19_EX[] oldR19Ex = r19ExDao.GetDataForEntity(new DT_R19_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                DT_R04_EX[] oldR04Ex = r04ExDao.GetDataForEntity(new DT_R04_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                DT_R08_EX[] oldR08Ex = r08ExDao.GetDataForEntity(new DT_R08_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                DT_R13_EX[] oldR13Ex = r13ExDao.GetDataForEntity(new DT_R13_EX() { SYSTEM_ID = exSystemId, SEQ = exSeq });
                #endregion

                if (mfToc.KIND.IsNull
                    || mfToc.KIND != CommonConst.MF_TOC_KIND_NOT_EDI)
                {
                    // 一次電マニがNot EDI(手動)以外の場合、最終処分情報は更新しない。
                    // Not EDI(手動)以外の場合は[3] 最終処分終了報告ボタンから最終処分情報を更新する。
                    continue;
                }

                SqlDecimal updateLatestSeq = mfToc.LATEST_SEQ + 1;
                SqlInt32 updateExSeq = exSeq + 1;

                // DT_R13, DT_R13_EXの更新データセット
                this.CreateUpdateElecMani(kanriId, exSystemId, r.FIRST_SYSTEM_ID, updateLatestSeq, updateExSeq, r18, oldR13Ex,
                                        ref r18List, ref R13List, ref oldR13ExList, ref newR13ExList);

                #region DT_R13, DT_R13_EX以外の更新データをセット
                mfToc.LATEST_SEQ = updateLatestSeq;
                mfTocList.Add(mfToc);

                foreach (var tempR19 in r19)
                {
                    tempR19.SEQ = updateLatestSeq;
                }
                r19List.Add(r19);

                foreach (var tempR02 in r02)
                {
                    tempR02.SEQ = updateLatestSeq;
                }
                r02List.Add(r02);

                foreach (var tempR04 in r04)
                {
                    tempR04.SEQ = updateLatestSeq;
                }
                r04List.Add(r04);

                foreach (var tempR05 in r05)
                {
                    tempR05.SEQ = updateLatestSeq;
                }
                r05List.Add(r05);

                foreach (var tempR08 in r08)
                {
                    tempR08.SEQ = updateLatestSeq;
                }
                r08List.Add(r08);

                r18ExList.Add(oldR18Ex);
                r19ExList.Add(oldR19Ex);
                r04ExList.Add(oldR04Ex);
                r08ExList.Add(oldR08Ex);
                #endregion

            }
            #endregion

            using (Transaction tran = new Transaction())
            {
                // 一次電マニ情報更新
                this.form.RelationResult.UpdateFirstElecMani(mfTocList, r18List, r19List, r02List, r04List, r05List, r08List, R13List
                    , r18ExList, r19ExList, r04ExList, r08ExList, oldR13ExList, newR13ExList);
                tran.Commit();
            }
        }
        #endregion

        #region 電マニ更新用データ作成
        /// <summary>
        /// 電マニ更新用データ作成
        /// DT_R18, DT_R13, DT_R13_EXのデータを作成する
        /// </summary>
        /// <param name="kanriId">管理ID</param>
        /// <param name="exSystemId">DT_R18_EX.SYSTEM_ID</param>
        /// <param name="exOrMixSystemId">DT_R18_EX.SYSTEM_IDまたはDT_R18_MIX.DETAIL_SYSTEM_IDの有効なほう</param>
        /// <param name="updateLatestSeq">DT_MF_TOC.LATEST_SEQ + 1</param>
        /// <param name="updateExSeq">DT_R18_EX.SYSTEM_ID + 1</param>
        /// <param name="r18"></param>
        /// <param name="oldR13Ex"></param>
        /// <param name="r18List"></param>
        /// <param name="R13List"></param>
        /// <param name="oldR13ExList"></param>
        /// <param name="newR13ExList"></param>
        internal void CreateUpdateElecMani(string kanriId, SqlInt64 exSystemId, SqlInt64 exOrMixSystemId, SqlDecimal updateLatestSeq, SqlInt32 updateExSeq, DT_R18 r18, DT_R13_EX[] oldR13Ex,
                            ref List<DT_R18> r18List, ref List<DT_R13[]> R13List, ref List<DT_R13_EX[]> oldR13ExList, ref List<DT_R13_EX[]> newR13ExList)
        {
            var getManiRelDao = DaoInitUtility.GetComponent<GetManifestRelationDaoCls>();
            decimal lastSbnEndRepFlg = 0;

            // 二次マニ全件取得
            DataTable nextManis = new DataTable();
            nextManis = getManiRelDao.GetLastSbnInfoForNexttMani(exOrMixSystemId);

            // 業者、現場の一覧を生成
            var gyoushaAndGenbaList = nextManis.AsEnumerable().Select(result => new
            {
                SECOND_HAIKI_KBN_CD = result.Field<int>("SECOND_HAIKI_KBN_CD"),
                SECOND_SYS_ID = result.Field<long>("SECOND_SYSTEM_ID"),
                SECOND_DETAIL_SYS_ID = result.Field<decimal>("SECOND_DETAIL_SYSTEM_ID"),
                LAST_SBN_JOU_NAME = result.Field<string>("LAST_SBN_JOU_NAME"),
                LAST_SBN_JOU_ADDRESS = result.Field<string>("LAST_SBN_JOU_ADDRESS")
            }).
                                        GroupBy(gryoup => new { gryoup.SECOND_HAIKI_KBN_CD, gryoup.SECOND_SYS_ID, gryoup.SECOND_DETAIL_SYS_ID, gryoup.LAST_SBN_JOU_NAME, gryoup.LAST_SBN_JOU_ADDRESS });

            int recSeq = 1;
            // DT_R13_EX用のCreate情報をセット
            string createUser = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_USER;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? DateTime.Now : oldR13Ex[0].CREATE_DATE;
            SqlDateTime createDate = oldR13Ex == null || oldR13Ex.Count() < 1 ? this.getDBDateTime() : oldR13Ex[0].CREATE_DATE;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            string createPc = oldR13Ex == null || oldR13Ex.Count() < 1 ? string.Empty : oldR13Ex[0].CREATE_PC;
            DateTime lastSbnEndDate = DateTime.MinValue;

            var commonManiLogic = new ManifestoLogic();
            var tempR13 = new List<DT_R13>();
            var tempR13Ex = new List<DT_R13_EX>();

            // 業者、現場毎に最終処分終了報告情報を生成
            foreach (var gyoushaAndGenbaRow in gyoushaAndGenbaList)
            {
                if (string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME)
                    || string.IsNullOrEmpty(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS))
                {
                    // 最終処分場がない場合、データの作りようがないので、除外
                    continue;
                }

                // SQL Injectionが発生する可能性があるので、予約文字をエスケープ
                string lastSbnJouName = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_NAME.Replace("'", "''"), @"([\[\]*%])", "[$1]");
                string lastSbnJouAddress = Regex.Replace(gyoushaAndGenbaRow.Key.LAST_SBN_JOU_ADDRESS.Replace("'", "''"), @"([\[\]*%])", "[$1]");

                DateTime tempLastSbnEndDate = DateTime.MinValue;
                var groupData = nextManis.Select(string.Format(
                            "SECOND_HAIKI_KBN_CD = {0} AND SECOND_SYSTEM_ID = {1} AND SECOND_DETAIL_SYSTEM_ID = {2} AND LAST_SBN_JOU_NAME = '{3}' AND LAST_SBN_JOU_ADDRESS = '{4}'"
                            , gyoushaAndGenbaRow.Key.SECOND_HAIKI_KBN_CD, gyoushaAndGenbaRow.Key.SECOND_SYS_ID, gyoushaAndGenbaRow.Key.SECOND_DETAIL_SYS_ID
                            , lastSbnJouName, lastSbnJouAddress)
                        );

                foreach (var tempRow in groupData)
                {
                    if (tempRow["LAST_SBN_END_DATE"] == null
                        || string.IsNullOrEmpty(tempRow["LAST_SBN_END_DATE"].ToString()))
                    {
                        // 最終処分終了日が設定されていないものがあれば最終処分未完了
                        tempLastSbnEndDate = DateTime.MinValue;
                        break;
                    }

                    // 一番新しい日付をセット
                    if (DateTime.Compare(tempLastSbnEndDate, DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString())) < 0)
                    {
                        tempLastSbnEndDate = DateTime.Parse(tempRow["LAST_SBN_END_DATE"].ToString());
                    }
                }

                // 
                var firstManiR13 = new DT_R13();
                var firstManiR13EX = new DT_R13_EX();

                // 住所分割
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                commonManiLogic.SetAddress1ToAddress4(groupData[0].Field<string>("LAST_SBN_JOU_ADDRESS"),
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                // set keys
                firstManiR13.KANRI_ID = kanriId;
                firstManiR13.SEQ = updateLatestSeq;
                firstManiR13.REC_SEQ = recSeq;
                firstManiR13EX.KANRI_ID = kanriId;
                firstManiR13EX.SYSTEM_ID = exSystemId;
                firstManiR13EX.SEQ = updateExSeq;
                firstManiR13EX.REC_SEQ = recSeq;

                // DT_R18.LAST_SBN_END_DATE用の日付
                lastSbnEndDate = DateTime.Compare(lastSbnEndDate, tempLastSbnEndDate) < 0 ? tempLastSbnEndDate : lastSbnEndDate;

                // DT_R13
                firstManiR13.LAST_SBN_END_DATE = tempLastSbnEndDate.Equals(DateTime.MinValue) ? null : tempLastSbnEndDate.ToString("yyyyMMdd");
                firstManiR13.MANIFEST_ID = r18.MANIFEST_ID;
                firstManiR13.LAST_SBN_JOU_NAME = groupData[0].Field<string>("LAST_SBN_JOU_NAME");
                firstManiR13.LAST_SBN_JOU_POST = groupData[0].Field<string>("LAST_SBN_JOU_POST");
                firstManiR13.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                firstManiR13.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                firstManiR13.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                firstManiR13.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                firstManiR13.LAST_SBN_JOU_TEL = groupData[0].Field<string>("LAST_SBN_JOU_TEL");
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                DateTime now = this.getDBDateTime();
                //firstManiR13.CREATE_DATE = DateTime.Now;
                //firstManiR13.UPDATE_TS = DateTime.Now;
                firstManiR13.CREATE_DATE = now;
                firstManiR13.UPDATE_TS = now;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // DT_R13_EX
                firstManiR13EX.MANIFEST_ID = r18.MANIFEST_ID;
                firstManiR13EX.LAST_SBN_GYOUSHA_CD = groupData[0].Field<string>("LAST_SBN_GYOUSHA_CD");
                firstManiR13EX.LAST_SBN_GENBA_CD = groupData[0].Field<string>("LAST_SBN_GENBA_CD");
                firstManiR13EX.CREATE_USER = createUser;
                firstManiR13EX.CREATE_DATE = createDate;
                firstManiR13EX.CREATE_PC = createPc;
                firstManiR13EX.UPDATE_USER = SystemProperty.UserName;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //firstManiR13EX.UPDATE_DATE = DateTime.Now;
                firstManiR13EX.UPDATE_DATE = now;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                firstManiR13EX.UPDATE_PC = SystemInformation.ComputerName;
                firstManiR13EX.DELETE_FLG = false;

                // DT_R13, DT_R13_EX追加
                tempR13.Add(firstManiR13);
                tempR13Ex.Add(firstManiR13EX);

                recSeq++;
            }

            var blankLastSbnJou = nextManis.Select("ISNULL(LAST_SBN_JOU_NAME, '') = '' OR ISNULL(LAST_SBN_JOU_ADDRESS, '') = ''");
            var blankLstSbnEndDate = nextManis.Select("LAST_SBN_END_DATE IS NULL");
            lastSbnEndRepFlg = (nextManis.Rows.Count > 0 && blankLastSbnJou.Count() < 1 && blankLstSbnEndDate.Count() < 1) ? 1 : 0;

            bool blankLastSbnEndDateFlg = false;
            for (int i = 0; i < tempR13.Count; i++)
            {
                var tmpLastSbnEndDate = tempR13[i].LAST_SBN_END_DATE;
                DateTime tempDate = DateTime.MinValue;

                if (tmpLastSbnEndDate != null
                    && !string.IsNullOrEmpty(tmpLastSbnEndDate.ToString()))
                {
                    if (DateTime.Compare(lastSbnEndDate, tempDate.Date) < 0)
                    {
                        lastSbnEndDate = tempDate.Date;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempR13Ex[i].LAST_SBN_GENBA_CD))
                    {
                        blankLastSbnEndDateFlg = true;
                        break;
                    }
                }
            }

            R13List.Add(tempR13.ToArray());
            newR13ExList.Add(tempR13Ex.ToArray());
            oldR13ExList.Add(oldR13Ex);

            r18.SEQ = updateLatestSeq;
            r18.LAST_SBN_ENDREP_FLAG = lastSbnEndRepFlg;
            r18.LAST_SBN_END_DATE = null;
            r18.LAST_SBN_END_REP_DATE = null;
            if (lastSbnEndRepFlg == 1)
            {
                r18.LAST_SBN_END_REP_DATE = this.parentbaseform.sysDate.ToString("yyyyMMdd");
            }
            if (!blankLastSbnEndDateFlg)
            {
                r18.LAST_SBN_END_DATE = lastSbnEndDate.Equals(DateTime.MinValue) ? null : lastSbnEndDate.ToString("yyyyMMdd");
            }
            else
            {
                r18.LAST_SBN_END_DATE = null;
            }
            r18List.Add(r18);
        }
        #endregion

        /// <summary>
        /// 紐付した情報の論理削除
        /// </summary>
        /// <param name="Next_SystemID"></param>
        /// <param name="Seq"></param>
        /// <param name="lstRec_Seq"></param>
        /// <returns></returns>
        [Transaction]
        public virtual int DeleteRelationInfo(string Next_SystemID, string Seq, DataTable Result)
        {
            LogUtility.DebugMethodStart(Next_SystemID, Seq, Result);
            int nRet = 0;
            try
            {
                if (string.IsNullOrEmpty(Next_SystemID) || string.IsNullOrEmpty(Seq))
                {
                    return nRet;
                }
                if (Result.Rows.Count == 0)
                {
                    return nRet;
                }

                //更新Entity対象の設定
                T_MANIFEST_RELATION del = new T_MANIFEST_RELATION();
                del.NEXT_SYSTEM_ID = Convert.ToInt64(Next_SystemID);
                del.SEQ = Convert.ToInt32(Seq);
                del.DELETE_FLG = true;
                //del.UPDATE_USER = System.Environment.MachineName;
                //del.UPDATE_DATE = DateTime.Now;
                //del.UPDATE_PC = System.Environment.MachineName;
                var dataBinderEntry = new DataBinderLogic<T_MANIFEST_RELATION>(del);
                dataBinderEntry.SetSystemProperty(del, true);

                for (int i = 0; i < Result.Rows.Count; i++)
                {
                    del.REC_SEQ = Convert.ToInt32(RelationResult.Rows[i]["NEXT_REC_SEQ"].ToString());
                    del.TIME_STAMP = (byte[])Result.Rows[i]["TIME_STAMP"];
                    //論理削除実行
                    nRet = ManiRealationDao.Update(del);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd(Next_SystemID, Seq, Result);

            return Result.Rows.Count;
        }

        #endregion

        #region 実現必須メソッド
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 紐付け可能チェック
        /// <summary>
        /// 紐付け可能かどうか判定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="catchErr"></param>
        /// <returns>true:紐付けOK, false:紐付けNG</returns>
        internal bool IsHimodukeOk(int rowIndex, out bool catchErr)
        {
            bool isHimodukeOkFlg = true;
            catchErr = false;
            try
            {
                if (rowIndex < 0)
                {
                    return isHimodukeOkFlg;
                }

                var targetRow = this.form.cdgv_FirstMani.Rows[rowIndex];
                var relationInfo = targetRow.Tag as RelationInfo_DTOCls;

                // 選択されたのが電マニの場合だけチェックする
                if (relationInfo == null
                    || !"4".Equals(relationInfo.MANIFEST_TYPE))
                {
                    return isHimodukeOkFlg;
                }

                if (string.IsNullOrEmpty(relationInfo.LAST_SBN_GENBA_NAME_AND_ADDRESS))
                {
                    // 一次マニの最終処分場所がない場合はOK
                    return isHimodukeOkFlg;
                }

                // 一次マニの最終処分場所があるのに
                // 二次マニの最終処分場所がない場合はNG
                if (this.LastSbnJyouDataForSecondMani == null
                    || this.LastSbnJyouDataForSecondMani.Rows.Count < 1)
                {
                    return false;
                }

                isHimodukeOkFlg = false;

                // マスタ、マニデータを使ってチェック
                foreach (DataRow row in this.LastSbnJyouDataForSecondMani.Rows)
                {
                    if (row["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"] != null
                        && !string.IsNullOrEmpty(row["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString()))
                    {
                        // マスタの値でチェック
                        DataTable LastSbnInfo = this.PaperAndElecManiDao.GetLastSbnJyouInfoForElecByKanriID(relationInfo.KANRI_ID);
                        foreach (DataRow rowLastSbn in LastSbnInfo.Rows)
                        {
                            if (row["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString()
                                .Equals(rowLastSbn["LAST_SBN_JIGYOUJOU_NAME_AND_ADDRESS"].ToString())
                                && Convert.ToString(row["LAST_SBN_END_DATE"]).
                                Equals(Convert.ToString(rowLastSbn["LAST_SBN_END_DATE"])))
                            {
                                isHimodukeOkFlg = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsHimodukeOk", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            return isHimodukeOkFlg;
        }
        #endregion

        /// <summary>
        /// 指定された端数CDに従い端数処理を行う
        /// </summary>
        /// <param name="targetNumber">端数処理対象</param>
        /// <param name="calcCD">端数CD</param>
        /// <returns name="double">端数処理後</returns>
        internal decimal FractionCalc(decimal targetNumber, int calcCD)
        {
            decimal returnVal = 0;		// 戻り値
            decimal sign = 1;           // 1(正) or -1(負)
            decimal hasuKetaCoefficient = 1;
            short hasuKeta = short.Parse(ManifestSuuryoFormatCD);


            if (targetNumber < 0)
                sign = -1;

            switch ((hasuKetaType)hasuKeta)
            {
                case hasuKetaType.NONE:
                    break;

                default:
                    hasuKetaCoefficient = (decimal)Math.Pow(10, hasuKeta - 2);
                    break;
            }

            switch ((fractionType)calcCD)
            {
                case fractionType.CEILING:
                    returnVal = (Math.Ceiling(Math.Abs(targetNumber) * hasuKetaCoefficient) / hasuKetaCoefficient) * sign;
                    break;
                case fractionType.FLOOR:
                    returnVal = (Math.Floor(Math.Abs(targetNumber) * hasuKetaCoefficient) / hasuKetaCoefficient) * sign;
                    break;
                case fractionType.ROUND:
                    returnVal = (Math.Round(Math.Abs(targetNumber) * hasuKetaCoefficient, MidpointRounding.AwayFromZero) / hasuKetaCoefficient) * sign;
                    break;
                default:
                    // NOTHING
                    break;
            }

            return returnVal;
        }

        /// <summary>
        /// 指定したDataTableを対象にsort文字列で並び替えた結果を返します
        /// </summary>
        /// <param name="dt">並び替え対象となるDataTableです。</param>
        /// <param name="sort">ソート条件</param>
        /// <returns>並び替え後のDataTalbe</returns>
        public DataTable GetSortedDataTable(DataTable dt, string sort)
        {
            // dtのスキーマや制約をコピーしたDataTableを作成します。
            DataTable table = dt.Clone();

            DataRow[] rows = dt.Select(null, sort);

            foreach (DataRow row in rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピーします
                addRow.ItemArray = row.ItemArray;

                // DataTableに格納します
                table.Rows.Add(addRow);
            }

            return table;
        }
        /// 20141226 Houkakou 「マニフェスト紐付画面」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDtPicker_EndDay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cDtPicker_StartDay;
            var ToTextBox = this.form.cDtPicker_EndDay;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141226 Houkakou 「マニフェスト紐付画面」のダブルクリックを追加する　end

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            bool isErr = true;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.cDtPicker_StartDay.BackColor = Constans.NOMAL_COLOR;
                this.form.cDtPicker_EndDay.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.cDtPicker_StartDay.Text))
                {
                    //伝票種類
                    msgLogic.MessageBoxShow("E001", "日付範囲");
                    //フォーカス設定
                    this.form.cDtPicker_StartDay.Focus();
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.cDtPicker_EndDay.Text))
                {
                    //伝票種類
                    msgLogic.MessageBoxShow("E001", "日付範囲");
                    //フォーカス設定
                    this.form.cDtPicker_EndDay.Focus();
                    return isErr;
                }

                DateTime date_from = Convert.ToDateTime(this.form.cDtPicker_StartDay.Value);
                DateTime date_to = Convert.ToDateTime(this.form.cDtPicker_EndDay.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.cDtPicker_StartDay.IsInputErrorOccured = true;
                    this.form.cDtPicker_EndDay.IsInputErrorOccured = true;
                    this.form.cDtPicker_StartDay.BackColor = Constans.ERROR_COLOR;
                    this.form.cDtPicker_EndDay.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "日付範囲From", "日付範囲To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.cDtPicker_StartDay.Focus();
                    return isErr;
                }
                isErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion

        #region 最終処分終了報告状況のチェック

        /// <summary>
        /// 最終処分終了報告状況のチェック
        /// </summary>
        /// <param name="systemId">該当一次マニフェストのsystemId</param>
        /// <param name="Dispmsg">True：アラート表示、False：アラート表示せず</param>
        /// <returns>True：最終処分終了報告無し、False：最終処分終了報告中、済、保留のいずれか</returns>
        internal bool CheckLastSbnEndFlg(string systemId, bool Dispmsg)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool rtValue = true;

            DataTable LastSbnFlgInfo = this.PaperAndElecManiDao.GetLastSbnEndFlgForElecBySystemID(systemId);
            if (LastSbnFlgInfo.Rows.Count > 0)
            {
                switch (LastSbnFlgInfo.Rows[0]["FLG"].ToString())
                {
                    case "1":
                        if (Dispmsg)
                        {
                            msgLogic.MessageBoxShow("E234", "済");
                        }
                        rtValue = false;
                        break;
                    case "2":
                        if (Dispmsg)
                        {
                            msgLogic.MessageBoxShow("E234", "中");
                        }
                        rtValue = false;
                        break;
                    case "3":
                        if (Dispmsg)
                        {
                            msgLogic.MessageBoxShowError("最終処分終了報告の送信保留中のため、紐付解除できません。\n最終処分終了報告の送信保留を削除してから紐付解除してください。");
                        }
                        rtValue = false;
                        break;
                    default:
                        break;
                }
            }

            return rtValue;
        }

        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
