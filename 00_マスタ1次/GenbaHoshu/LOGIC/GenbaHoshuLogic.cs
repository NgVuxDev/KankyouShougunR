// $Id: GenbaHoshuLogic.cs 51959 2015-06-10 08:41:56Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GenbaHoshu.APP;
using GenbaHoshu.Const;
using GenbaHoshu.Entity;
using GenbaHoshu.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Event;
using r_framework.Logic;
using r_framework.MasterAccess;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Dto;
using System.Collections.ObjectModel;

using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI;
using r_framework.Configuration;
using System.Data.SqlTypes;
using Shougun.Core.Master.CourseNyuryoku.Dao;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using GrapeCity.Win.MultiRow;

namespace GenbaHoshu.Logic
{
    /// <summary>
    /// 現場保守画面のビジネスロジック
    /// </summary>
    public class GenbaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "GenbaHoshu.Setting.ButtonSetting.xml";

        private readonly string ButtonInfoXmlPath2 = "GenbaHoshu.Setting.ButtonSetting2.xml";

        private readonly string GET_POPUP_DATA_SQL = "GenbaHoshu.Sql.GetPopupdataSql.sql";

        private readonly string GET_ICHIRAN_ITAKU_DATA_SQL = "GenbaHoshu.Sql.GetIchiranItakudataSql.sql";

        private readonly string GET_CHIIKI_DATA_SQL = "GenbaHoshu.Sql.GetChiikidataSql.sql";

        private readonly string GET_TEIKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetTeikiHinmeiDataSql.sql";

        private readonly string GET_TEIKI_HINMEI_STRUCT_SQL = "GenbaHoshu.Sql.GetTeikiHinmeiStructSql.sql";

        private readonly string GET_TSUKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetTsukiHinmeiDataSql.sql";

        private readonly string GET_TSUKI_HINMEI_STRUCT_SQL = "GenbaHoshu.Sql.GetTsukiHinmeiStructSql.sql";

        private readonly string GET_HINMEI_URIAGE_SHIHARAI_DATA_SQL = "GenbaHoshu.Sql.GetHinmeiUriageShiharaiDataSql.sql";

        private readonly string DELETE_TEIKI_HINMEI_SQL = "GenbaHoshu.Sql.DeleteTeikiHinmeiSql.sql";

        private readonly string DELETE_TSUKI_HINMEI_SQL = "GenbaHoshu.Sql.DeleteTsukiHinmeiSql.sql";

        private readonly string GET_KARI_TEIKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetKariTeikiHinmeiData.sql";

        private readonly string GET_HIKIAI_TEIKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetHikiaiTeikiHinmeiData.sql";

        private readonly string GET_KARI_TSUKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetKariTsukiHinmeiData.sql";

        private readonly string GET_HIKIAI_TSUKI_HINMEI_DATA_SQL = "GenbaHoshu.Sql.GetHikiaiTsukiHinmeiData.sql";

        private readonly string GET_SMS_RECEIVER_DATA_SQL = "GenbaHoshu.Sql.GetSmsReceiverDataSql.sql";

        private readonly string GET_SMS_RECEIVER_STRUCT_SQL = "GenbaHoshu.Sql.GetSmsReceiverStructSql.sql";

        /// <summary>
        /// 現場保守画面Form
        /// </summary>
        private GenbaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private List<M_GENBA_TEIKI_HINMEI> genbaTeikiEntity;

        // QN Tue Anh #158986 START
        private List<M_COURSE> updateCourseEntity;

        private List<M_COURSE_DETAIL> deleteCourseDetailEntity;

        private List<M_COURSE_DETAIL> insertCourseDetailEntity;

        private List<M_COURSE_DETAIL_ITEMS> deleteCourseDetailItemsEntity;

        private List<M_COURSE_DETAIL_ITEMS> insertCourseDetailItemsEntity;

        private List<M_COURSE_DETAIL_ITEMS> updateCourseDetailItemsEntity;
        // QN Tue Anh #158986 END

        private List<M_GENBA_TSUKI_HINMEI> genbaTsukiEntity;

        #region 電子申請用データ

        private M_KARI_GENBA kariGenbaEntity;
        private List<M_KARI_GENBA_TEIKI_HINMEI> kariGenbaTeikiEntity;
        private List<M_KARI_GENBA_TSUKI_HINMEI> kariGenbaTsukiEntity;

        #endregion

        private M_GYOUSHA gyoushaEntity;
        private M_TORIHIKISAKI torihikisakiEntity;
        private M_TODOUFUKEN todoufukenEntity;
        private M_CHIIKI chiikiEntity;
        private M_SHUUKEI_KOUMOKU shuukeiEntity;
        private M_GYOUSHU gyoushuEntity;
        public M_SYS_INFO sysinfoEntity;
        private M_KYOTEN kyotenEntity;
        private M_SHAIN shainEntity;
        private M_BUSHO bushoEntity;
        private M_MANIFEST_SHURUI manishuruiEntity;
        private M_MANIFEST_TEHAI manitehaiEntity;
        // QN Tue Anh #158986 START
        private M_COURSE_DaoCls courseDao;
        private DaoCls courseDetailDao;
        private M_COURSE_DETAIL_ITEMS_DaoCls courseDetailItemsDao;
        // QN Tue Anh #158986 END

        private int rowCntItaku;

        // 20141208 ブン 運搬報告書提出先の地域エンティティを追加する start
        private M_CHIIKI upnHoukokushoTeishutsuChiikiEntity;
        // 20141208 ブン 運搬報告書提出先の地域エンティティを追加する end

        // No2267-->
        private List<String> messageList;
        // No2267<--

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 現場_定期品名のDao
        /// </summary>
        private IM_GENBA_TEIKI_HINMEIDao daoGenbaTeiki;

        /// <summary>
        /// 現場_月極品名のDao
        /// </summary>
        private IM_GENBA_TSUKI_HINMEIDao daoGenbaTsuki;

        /// <summary>
        /// 仮現場Dao
        /// </summary>
        private IM_KARI_GENBADao daoKariGenba;

        /// <summary>
        /// 仮現場_定期品名Dao
        /// </summary>
        private IM_KARI_GENBA_TEIKI_HINMEIDao daoKariGenbaTeiki;

        /// <summary>
        /// 仮現場_月極品名Dao
        /// </summary>
        private IM_KARI_GENBA_TSUKI_HINMEIDao daoKariGenbaTsuki;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// 部署のDao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        /// <summary>
        /// 地域のDao
        /// </summary>
        private IM_CHIIKIDao daoChiiki;

        /// <summary>
        /// 集計項目のDao
        /// </summary>
        private IM_SHUUKEI_KOUMOKUDao daoShuukei;

        /// <summary>
        /// 業種のDao
        /// </summary>
        private IM_GYOUSHUDao daoGyoushu;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyou;

        /// <summary>
        /// 委託のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao daoItaku;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorisaki;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao daoSeikyuu;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao daoShiharai;

        /// <summary>
        /// マニ種類のDao
        /// </summary>
        private IM_MANIFEST_SHURUIDao daoManishurui;

        /// <summary>
        /// マニ手配のDao
        /// </summary>
        private IM_MANIFEST_TEHAIDao daoManitehai;

        /// <summary>
        /// コース明細のDao
        /// </summary>
        private IM_COURSE_DETAILDao daoCourseDetail;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者のDao
        /// </summary>
        private IM_SMS_RECEIVERDao daoSmsReceiver;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場のDao
        /// </summary>
        private IM_SMS_RECEIVER_LINK_GENBADao daoSmsReceiverGenba;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        private TabPageManager _tabPageManager = null;

        /// <summary>
        /// 委託契約書種類
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuShurui = new Dictionary<string, string>()
        {
            {"1", "収集・運搬委託契約"},
            {"2", "処分委託契約"},
            {"3", "収集・運搬/処分委託契約"}
        };

        /// <summary>
        /// 委託契約書ステータス
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuStatus = new Dictionary<string, string>()
        {
            {"1", "作成"},
            {"2", "送付"},
            {"3", "返送"},
            {"4", "保管"},
            {"5", "解約済"}
        };

        /// <summary>
        /// 委託契約書登録方法
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuTouroku = new Dictionary<string, string>()
        {
            {"1", "詳細登録"},
            {"2", "基本登録"}
        };

        /// <summary>
        /// 前回定期品名セル名
        /// </summary>
        private string prevTeikiCellName = string.Empty;

        /// <summary>
        /// マニ種類保持用
        /// </summary>
        internal string maniShuruiSave = "0";

        /// <summary>
        /// マニ手配保持用
        /// </summary>
        internal string maniTehaiSave = "0";

        /// <summary>
        /// 取引先チェック時にエラーを表示するか判断するフラグ
        /// </summary>
        internal bool IsShowTorihikisakiError = false;

        internal int preRowIndex = -1;

        internal int preCellIndex = -1;

        internal bool teikiHinmeiUpdate = false; // QN Tue Anh #158986

        #endregion

        #region プロパティ

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GenbaCd { get; set; }

        /// <summary>
        /// 電子申請フラグ
        /// </summary>
        public bool denshiShinseiFlg { get; set; }

        /// <summary>
        /// マニフェスト現場CD
        /// </summary>
        public string ManiGyoushaCd { get; set; }

        // No3521-->
        /// <summary>
        /// マニフェスト現場CD
        /// </summary>
        public string ManiGenbaCd { get; set; }
        // No3521<--

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 排出事業場区分チェック
        /// </summary>
        public bool FlgHaishutsuKbn { get; set; }

        /// <summary>
        /// マニ返送先区分チェック
        /// </summary>
        public bool FlgManiHensousakiKbn { get; set; }

        /// <summary>
        /// 検索結果(委託一覧)
        /// </summary>
        public DataTable SearchResultItaku { get; set; }

        /// <summary>
        /// 業者排出事業者区分
        /// </summary>
        public bool FlgGyoushaHaishutuKbn { get; set; }

        /// <summary>
        /// 定期品名データテーブル
        /// </summary>
        public DataTable TeikiHinmeiTable { get; set; }

        /// <summary>
        /// 月極品名データテーブル
        /// </summary>
        public DataTable TsukiHinmeiTable { get; set; }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者データテーブル
        /// </summary>
        public DataTable SmsReceiverTable { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        ///　画面設定中フラグ
        /// </summary>
        public bool isSettingWindowData { get; set; }

        /// <summary>
        /// 現場エンティティを取得・設定します
        /// </summary>
        internal M_GENBA GenbaEntity { get; private set; }

        /// <summary>
        /// 承認済申請一覧から呼び出されたかどうかのフラグを取得・設定します
        /// </summary>
        internal bool IsFromShouninzumiDenshiShinseiIchiran { get; set; }

        /// <summary>
        /// 本登録の元となる引合現場エンティティを取得・設定します
        /// </summary>
        internal M_HIKIAI_GENBA LoadHikiaiGenbaEntity { get; private set; }

        /// <summary>
        /// 本登録の元となる仮登録現場エンティティを取得・設定します
        /// </summary>
        internal M_KARI_GENBA LoadKariGenbaEntity { get; private set; }

        #endregion

        #region A票～E票で使うカスタムコントロール

        //ラジオボタン
        public CustomRadioButton HENSOUSAKI_PLACE_KBN_1;
        public CustomRadioButton HENSOUSAKI_PLACE_KBN_2;
        public CustomRadioButton HensousakiKbn1;
        public CustomRadioButton HensousakiKbn2;
        public CustomRadioButton HensousakiKbn3;
        //テキストボックス
        public CustomNumericTextBox2 HENSOUSAKI_PLACE_KBN;
        public CustomNumericTextBox2 HensousakiKbn;
        public CustomAlphaNumTextBox ManiHensousakiTorihikisakiCode;
        public CustomTextBox ManiHensousakiTorihikisakiName;
        public CustomAlphaNumTextBox ManiHensousakiGyoushaCode;
        public CustomTextBox ManiHensousakiGyoushaName;
        public CustomAlphaNumTextBox ManiHensousakiGenbaCode;
        public CustomTextBox ManiHensousakiGenbaName;
        //public CustomTextBox ManiHensousakiName1;
        //public CustomTextBox ManiHensousakiName2;
        //public CustomTextBox ManiHensousakiAddress1;
        //public CustomTextBox ManiHensousakiAddress2;
        //public CustomTextBox ManiHensousakiBusho;
        //public CustomTextBox ManiHensousakiTantou;
        //public CustomNumericTextBox2 ManiHensousakiPost;
        public CustomNumericTextBox2 MANIFEST_USE;
        //コンボボックス
        //public CustomComboBox ManiHensousakiKeishou1;
        //public CustomComboBox ManiHensousakiKeishou2;
        //ポップアップボタン
        //public CustomAddressSearchButton ManiHensousakiAddressSearchButton;
        //public CustomPostSearchButton ManiHensousakiPostSearchButton;
        //public CustomButton GENBA_COPY_MANI_BUTTON;


        // Begin: LANDUONG - 20220215 - refs#160054
        internal bool denshiSeikyusho, denshiSeikyuRaku;
        // End: LANDUONG - 20220215 - refs#160054

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public GenbaHoshuLogic(GenbaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoGenbaTeiki = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEIDao>();
            this.daoGenbaTsuki = DaoInitUtility.GetComponent<IM_GENBA_TSUKI_HINMEIDao>();
            this.daoKariGenba = DaoInitUtility.GetComponent<IM_KARI_GENBADao>();
            this.daoKariGenbaTeiki = DaoInitUtility.GetComponent<IM_KARI_GENBA_TEIKI_HINMEIDao>();
            this.daoKariGenbaTsuki = DaoInitUtility.GetComponent<IM_KARI_GENBA_TSUKI_HINMEIDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoChiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
            this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
            this.daoItaku = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
            this.daoGyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
            this.daoShuukei = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoTorisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.daoShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.daoManishurui = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();
            this.daoManitehai = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();
            this.daoCourseDetail = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();
            // QN Tue Anh #158986 START
            this.courseDao = DaoInitUtility.GetComponent<M_COURSE_DaoCls>();
            this.courseDetailDao = DaoInitUtility.GetComponent<DaoCls>();
            this.courseDetailItemsDao = DaoInitUtility.GetComponent<M_COURSE_DETAIL_ITEMS_DaoCls>();
            // QN Tue Anh #158986 END
            this.daoSmsReceiver = DaoInitUtility.GetComponent<IM_SMS_RECEIVERDao>();
            this.daoSmsReceiverGenba = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();
            this.isError = false;
            this.isSettingWindowData = false;

            _tabPageManager = new TabPageManager(this.form.ManiHensousakiKeishou2B1);

            // No2267-->
            this.messageList = new List<string>();
            // No2267<--

            // Begin: LANDUONG - 20220215 - refs#160054
            // 電子請求オプ
            denshiSeikyusho = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            //電子請求楽楽明細オプ
            denshiSeikyuRaku = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();
            // End: LANDUONG - 20220215 - refs#160054

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // MAPBOX連携が無効な場合
                this._tabPageManager.ChangeTabPageVisible("tabPageMapbox", (AppConfig.AppOptions.IsMAPBOX()) ? true : false);

                // ｼｮｰﾄﾒｯｾｰｼﾞオプションが無効な場合
                this._tabPageManager.ChangeTabPageVisible(17, (AppConfig.AppOptions.IsSMS()) ? true : false);

                // イベントの初期化
                this.EventInit(parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);

                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(windowType) && string.IsNullOrEmpty(this.GenbaCd))
                {
                    // 新規モード時、返送先区分の活性制御が仕様通りに行われないため、データを画面に設定後、改めて返送先区分の活性制御を行う。
                    // 返送先区分の活性制御には○票使用区分や返送先区分のTextChangedイベント制御してるため
                    // どこかで無理が生じしてしまっているようです。
                    bool catchErr = this.SettingHensouSakiKbn();
                    if (catchErr)
                    {
                        return true;
                    }
                }

                this.allControl = this.form.allControl;

                // Begin: LANDUONG - 20220215 - refs#160054
                this.SetDensiSeikyushoAndRakurakuVisible();
                // End: LANDUONG - 20211201 - refs#160054

                // A票～E票タブ制御
                this.ChangeTabAtoE();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            if (denshiShinseiFlg)
            {
                //「F9:申請」となる場合のイベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Shinsei);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.UPDATE;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            else
            {
                //新規ボタン(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                //修正ボタン(F3)イベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                //一覧ボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.FormSearch);

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }

            // [1]返送先コピーイベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.ManifestoTabCopy);
            // [2]コース一覧イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.MoveToCourseIchiran);

            //業者CD Validatedイベント生成
            this.form.GyoushaCode.Validated += new EventHandler(this.form.GyoushaCDValidated);
            this.form.GyoushaCode.Enter += new EventHandler(this.form.GyoushaCode_Enter);

            //取引先CD Validatedイベント生成
            this.form.TorihikisakiCode.Validated += new EventHandler(this.form.TorihikisakiCDValidated);

            ////マニ取引先CD Validatedイベント生成
            //this.form.ManiHensousakiTorihikisakiCode.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);

            ////マニ業者CD Validatedイベント生成
            //this.form.ManiHensousakiGyoushaCode.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);

            ////マニ現場CD Validatedイベント生成
            //this.form.ManiHensousakiGenbaCode.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);

            #region  (A票～E票)についてイベント生成

            //マニ返送先使用可否 TextChangedイベント生成(A票～E票)
            this.form.MANIFEST_USE_AHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_B1Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_B2Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_B4Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_B6Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_C1Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_C2Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_DHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
            this.form.MANIFEST_USE_EHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);

            //マニ返送先区分 TextChangedイベント生成(A票～E票)
            this.form.HENSOUSAKI_PLACE_KBN_AHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_DHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
            this.form.HENSOUSAKI_PLACE_KBN_EHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);

            this.form.HensousakiKbn_AHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_B1Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_B2Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_B4Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_B6Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_C1Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_C2Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_DHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
            this.form.HensousakiKbn_EHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);

            //マニ取引先CD Validatedイベント生成(A票～E票)
            this.form.ManiHensousakiTorihikisakiCode_AHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_DHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
            this.form.ManiHensousakiTorihikisakiCode_EHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);

            //マニ業者CD Validatedイベント生成(A票～E票)
            this.form.ManiHensousakiGyoushaCode_AHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_DHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
            this.form.ManiHensousakiGyoushaCode_EHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);

            //マニ現場CD Validatedイベント生成(A票～E票)
            this.form.ManiHensousakiGenbaCode_AHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_DHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
            this.form.ManiHensousakiGenbaCode_EHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);

            //A票～E票タブの取引先コピーボタン押下処理
            this.form.GENBA_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

            #endregion

            //請求タブの取引先コピーボタン押下処理
            this.form.GENBA_COPY_SEIKYU_BUTTON.Click += new EventHandler(this.form.CopySeikyuButtonClick);

            //支払タブの取引先コピーボタン押下処理
            this.form.GENBA_COPY_SIHARAI_BUTTON.Click += new EventHandler(this.form.CopySiharaiButtonClick);

            ////分類タブの取引先コピーボタン押下処理
            //this.form.GENBA_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

            //定期品名一覧セルエンター処理
            this.form.TeikiHinmeiIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.Ichiran_CellEnter);

            //月極品名一覧セルエンター処理
            this.form.TsukiHinmeiIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.Ichiran_CellEnter);

            // VUNGUYEN 20150525 #1294 START
            // 適用終了のダブルクリックイベント
            this.form.TekiyouKikanTo.MouseDoubleClick += new MouseEventHandler(TekiyouKikanTo_MouseDoubleClick);
            // VUNGUYEN 20150525 #1294 END

            // 運搬先提出先 Validatedイベント生成
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Validated += new EventHandler(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validated);

            // mapbox連携
            // 地図表示処理
            this.form.bt_map_open.Click += new EventHandler(this.OpenMap);
        }

        /// <summary>
        /// 動的イベント設定処理
        /// </summary>
        public void SetDynamicEvent()
        {
            LogUtility.DebugMethodStart();

            //定期品名一覧の入力値チェック処理
            this.form.TeikiHinmeiIchiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TeikiHinmeiIchiran_CellValidating);

            //定期品名一覧の行入力値チェック処理
            this.form.TeikiHinmeiIchiran.RowValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TeikiHinmeiIchiran_RowValidating);

            //月極品名一覧の入力値チェック処理
            this.form.TsukiHinmeiIchiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TsukiHinmeiIchiran_CellValidating);

            //定期品名一覧の行入力値チェック処理
            this.form.TsukiHinmeiIchiran.RowValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TsukiHinmeiIchiran_RowValidating);

            //[ｼｮｰﾄﾒｯｾｰｼﾞ]タブ携帯番号一覧の入力値チェック処理
            this.form.SMSReceiverIchiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.SMSReceiverIchiran_CellValidating);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 動的イベント削除処理
        /// </summary>
        public void RemoveDynamicEvent()
        {
            LogUtility.DebugMethodStart();

            //定期品名一覧の入力値チェック処理
            this.form.TeikiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TeikiHinmeiIchiran_CellValidating);

            //定期品名一覧の行入力値チェック処理
            this.form.TeikiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TeikiHinmeiIchiran_RowValidating);

            //月極品名一覧の入力値チェック処理
            this.form.TsukiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TsukiHinmeiIchiran_CellValidating);

            //定期品名一覧の行入力値チェック処理
            this.form.TsukiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TsukiHinmeiIchiran_RowValidating);

            //[ｼｮｰﾄﾒｯｾｰｼﾞ]タブ携帯番号一覧の入力値チェック処理
            this.form.SMSReceiverIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.SMSReceiverIchiran_CellValidating);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            if (denshiShinseiFlg)
            {
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath2);
            }
            else
            {
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parentForm"></param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            if (this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 電子申請から呼び出されたときは、コードを移動する
                this.GyoushaCd = this.form.ShinseiGyoushaCd;
                if (!String.IsNullOrEmpty(this.form.ShinseiGenbaCd))
                {
                    this.GenbaCd = this.form.ShinseiGenbaCd;
                }
                else
                {
                    this.GenbaCd = this.form.ShinseiHikiaiGenbaCd;
                }
            }
            bool catchErr = false;
            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew(parentForm);
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    catchErr = this.WindowInitUpdate(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    catchErr = this.WindowInitReference(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            if (string.IsNullOrEmpty(this.GenbaCd))
            {
                this.GetSysInfo();

                // 【新規】モードで初期化
                bool catchErr = WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
            else
            {
                // 【複写】モードで初期化
                WindowInitNewCopyMode(parentForm);
            }
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            try
            {
                // タブを全表示する
                this._tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                this._tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                // 全コントロール操作可能とする
                this.AllControlLock(true);

                // 定期・月極品名の空テーブルを作成する

                this.form.TeikiHinmeiIchiran.CellFormatting -= new EventHandler<CellFormattingEventArgs>(this.form.TeikiHinmeiIchiran_CellFormatting);
                this.form.TsukiHinmeiIchiran.CellFormatting -= new EventHandler<CellFormattingEventArgs>(this.form.TsukiHinmeiIchiran_CellFormatting);
                this.TeikiHinmeiTable = this.daoGenba.GetDataBySqlFile(this.GET_TEIKI_HINMEI_STRUCT_SQL, new M_GENBA());
                this.SetIchiranTeiki();
                this.TsukiHinmeiTable = this.daoGenba.GetDataBySqlFile(this.GET_TSUKI_HINMEI_STRUCT_SQL, new M_GENBA());
                this.SetIchiranTsuki();
                this.form.TeikiHinmeiIchiran.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.TeikiHinmeiIchiran_CellFormatting);
                this.form.TsukiHinmeiIchiran.CellFormatting += new EventHandler<CellFormattingEventArgs>(this.form.TsukiHinmeiIchiran_CellFormatting);

                // 名称取得用処理
                this.GenbaEntity = new M_GENBA();
                this.GenbaEntity.MANIFEST_SHURUI_CD = this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD;
                this.GenbaEntity.MANIFEST_TEHAI_CD = this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD;
                this.SearchManiShurui();
                this.SearchManiTehai();

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 新規モード固有UI設定
                this.form.GyoushaCode.Enabled = true;   // 業者CD
                this.form.GyoushaCodeSearchButton.Enabled = true;   // 業者CDボタン
                this.form.bt_genbacd_saiban.Enabled = true;    // 採番ボタン

                //初期値セット部
                this.form.SeikyuuDaihyouPrintKbn.Text = "2";
                this.form.ItakuKeiyakuUseKbn.Text = "1";
                //this.form.HensousakiKbn.Text = "1";
                this.form.GenbaKeishou1.SelectedIndex = -1;
                this.form.GenbaKeishou2.SelectedIndex = -1;
                this.form.SeikyuuSouhuKeishou1.SelectedIndex = -1;
                this.form.SeikyuuSouhuKeishou2.SelectedIndex = -1;
                this.form.ShiharaiSoufuKeishou1.SelectedIndex = -1;
                this.form.ShiharaiSoufuKeishou2.SelectedIndex = -1;
                //this.form.ManiHensousakiKeishou1.SelectedIndex = -1;
                //this.form.ManiHensousakiKeishou2.SelectedIndex = -1;

                //this.form.HensousakiKbn1.Checked = true;
                //this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
                //this.form.ManiHensousakiGyoushaCode.Enabled = false;
                //this.form.ManiHensousakiGenbaCode.Enabled = false;

                //this.form.ManiTorihikisakiCodeSearchButton.Enabled = true;
                //this.form.ManiGyoushaCodeSearchButton.Enabled = false;
                //this.form.ManiGenbaCodeSearchButton.Enabled = false;

                this.form.ManiHensousakiKeishou1.SelectedIndex = -1;
                this.form.ManiHensousakiKeishou2.SelectedIndex = -1;

                #region A票～E票初期化

                //A票
                if (this._tabPageManager.IsVisible("tabPage1"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                    this.form.HensousakiKbn_AHyo.Text = "1";
                    this.form.HensousakiKbn1_AHyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                }

                //B1票
                if (this._tabPageManager.IsVisible("tabPage2"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                    this.form.HensousakiKbn_B1Hyo.Text = "1";
                    this.form.HensousakiKbn1_B1Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                }

                //B2票
                if (this._tabPageManager.IsVisible("tabPage3"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                    this.form.HensousakiKbn_B2Hyo.Text = "1";
                    this.form.HensousakiKbn1_B2Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                }

                //B4票
                if (this._tabPageManager.IsVisible("tabPage4"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                    this.form.HensousakiKbn_B4Hyo.Text = "1";
                    this.form.HensousakiKbn1_B4Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                }

                //B6票
                if (this._tabPageManager.IsVisible("tabPage5"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                    this.form.HensousakiKbn_B6Hyo.Text = "1";
                    this.form.HensousakiKbn1_B6Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                }

                //C1票
                if (this._tabPageManager.IsVisible("tabPage6"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                    this.form.HensousakiKbn_C1Hyo.Text = "1";
                    this.form.HensousakiKbn1_C1Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                }

                //C2票
                if (this._tabPageManager.IsVisible("tabPage7"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                    this.form.HensousakiKbn_C2Hyo.Text = "1";
                    this.form.HensousakiKbn1_C2Hyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                }

                //D票
                if (this._tabPageManager.IsVisible("tabPage8"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                    this.form.HensousakiKbn_DHyo.Text = "1";
                    this.form.HensousakiKbn1_DHyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                }

                //E票
                if (this._tabPageManager.IsVisible("tabPage9"))
                {
                    this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                    this.form.HensousakiKbn_EHyo.Text = "1";
                    this.form.HensousakiKbn1_EHyo.Checked = true;
                    this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                    this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                    this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                    this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                }
                //A票
                if (this._tabPageManager.IsVisible("tabPage1"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                }

                //B1票
                if (this._tabPageManager.IsVisible("tabPage2"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                }

                //B2票
                if (this._tabPageManager.IsVisible("tabPage3"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                }

                //B4票
                if (this._tabPageManager.IsVisible("tabPage4"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                }

                //B6票
                if (this._tabPageManager.IsVisible("tabPage5"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                }

                //C1票
                if (this._tabPageManager.IsVisible("tabPage6"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                }

                //C2票
                if (this._tabPageManager.IsVisible("tabPage7"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                }

                //D票
                if (this._tabPageManager.IsVisible("tabPage8"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                }

                //E票
                if (this._tabPageManager.IsVisible("tabPage9"))
                {
                    this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                }

                #endregion

                // 共通部
                this.form.GyoushaCode.Text = string.Empty;
                this.form.GyoushaKbnUkeire.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_UKEIRE;
                this.form.GyoushaKbnShukka.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_SHUKKA;
                this.form.GyoushaKbnMani.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_MANI;
                this.form.GyoushaName1.Text = string.Empty;
                this.form.GyoushaName2.Text = string.Empty;
                this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                this.form.GYOUSHA_TEKIYOU_END.Value = null;
                this.form.TorihikisakiCode.Text = string.Empty;
                this.form.TorihikisakiName1.Text = string.Empty;
                this.form.TorihikisakiName2.Text = string.Empty;
                this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                this.form.KyotenCode.Text = string.Empty;
                this.form.KyotenName.Text = string.Empty;
                this.form.GenbaCode.Text = string.Empty;
                this.form.GenbaFurigana.Text = string.Empty;
                this.form.GenbaName1.Text = string.Empty;
                this.form.GenbaName2.Text = string.Empty;
                this.form.GenbaKeishou1.Text = this.sysinfoEntity.GENBA_KEISHOU1;
                this.form.GenbaKeishou2.Text = this.sysinfoEntity.GENBA_KEISHOU2;
                this.form.ManiHensousakiKbnHidden.Text = string.Empty;
                this.form.GenbaNameRyaku.Text = string.Empty;
                this.form.GenbaTel.Text = string.Empty;
                this.form.GenbaKeitaiTel.Text = string.Empty;
                this.form.GenbaFax.Text = string.Empty;
                this.form.EigyouTantouBushoCode.Text = string.Empty;
                this.form.EigyouTantouBushoName.Text = string.Empty;
                this.form.EigyouCode.Text = string.Empty;
                this.form.EigyouName.Text = string.Empty;
                this.form.EigyouTantouBushoCode.Text = string.Empty;
                this.form.TekiyouKikanForm.Value = findForm.sysDate;
                this.form.TekiyouKikanTo.Value = null;
                this.form.ChuusiRiyuu1.Text = string.Empty;
                this.form.ChuusiRiyuu2.Text = string.Empty;
                this.form.ShokuchiKbn.Checked = false;
                //this.form.DenManiShoukaiKbn.Checked = false;
                this.form.KENSHU_YOUHI.Checked = false;

                //20250320
                this.form.CHIZU.Text = string.Empty;

                // 基本情報
                this.form.GenbaPost.Text = string.Empty;
                this.form.GenbaTodoufukenCode.Text = string.Empty;
                this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                this.form.GenbaAddress1.Text = string.Empty;
                this.form.GenbaAddress2.Text = string.Empty;
                this.form.ChiikiCode.Text = string.Empty;
                this.form.ChiikiName.Text = string.Empty;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                this.form.BushoCode.Text = string.Empty;
                this.form.TantoushaCode.Text = string.Empty;
                this.form.KoufutantoushaCode.Text = string.Empty;
                this.form.ShuukeiItemCode.Text = string.Empty;
                this.form.ShuukeiItemName.Text = string.Empty;
                this.form.GyoushuCode.Text = string.Empty;
                this.form.GyoushuName.Text = string.Empty;
                this.form.Bikou1.Text = string.Empty;
                this.form.Bikou2.Text = string.Empty;
                this.form.Bikou3.Text = string.Empty;
                this.form.Bikou4.Text = string.Empty;

                // 請求情報
                this.form.SeikyuushoSoufusaki1.Text = string.Empty;
                this.form.SeikyuushoSoufusaki2.Text = string.Empty;
                this.form.SeikyuuSouhuKeishou1.Text = string.Empty;
                this.form.SeikyuuSouhuKeishou2.Text = string.Empty;
                this.form.SeikyuuSoufuPost.Text = string.Empty;
                this.form.SeikyuuSoufuAddress1.Text = string.Empty;
                this.form.SeikyuuSoufuAddress2.Text = string.Empty;
                this.form.SeikyuuSoufuBusho.Text = string.Empty;
                this.form.SeikyuuSoufuTantou.Text = string.Empty;
                this.form.SoufuGenbaTel.Text = string.Empty;
                this.form.SoufuGenbaFax.Text = string.Empty;
                this.form.SeikyuuTantou.Text = string.Empty;
                this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SeikyuuDaihyouPrintKbn.Text = this.sysinfoEntity.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()));
                }
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (seikyuuKyoten != null)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }
                this.ChangeSeikyuuKyotenPrintKbn();

                // Begin: LANDUONG - 20220215 - refs#160054                
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
                this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;                
                this.HakkousakuAndRakurakuCDCheck();
                // End: LANDUONG - 20220215 - refs#160054

                // 支払情報
                this.form.ShiharaiSoufuName1.Text = string.Empty;
                this.form.ShiharaiSoufuName2.Text = string.Empty;
                this.form.ShiharaiSoufuKeishou1.Text = string.Empty;
                this.form.ShiharaiSoufuKeishou2.Text = string.Empty;
                this.form.ShiharaiSoufuPost.Text = string.Empty;
                this.form.ShiharaiSoufuAddress1.Text = string.Empty;
                this.form.ShiharaiSoufuAddress2.Text = string.Empty;
                this.form.ShiharaiSoufuBusho.Text = string.Empty;
                this.form.ShiharaiSoufuTantou.Text = string.Empty;
                this.form.ShiharaiGenbaTel.Text = string.Empty;
                this.form.ShiharaiGenbaFax.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.IsNull && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()));
                }
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                this.ChangeShiharaiKyotenPrintKbn();

                //現場分類
                if (!sysinfoEntity.GENBA_JISHA_KBN.IsNull)
                {
                    this.form.JishaKbn.Checked = (bool)sysinfoEntity.GENBA_JISHA_KBN;
                }

                if (!sysinfoEntity.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN.IsNull)
                {
                    this.form.HaishutsuKbn.Checked = (bool)sysinfoEntity.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN;
                }

                if (!sysinfoEntity.GENBA_TSUMIKAEHOKAN_KBN.IsNull)
                {
                    this.form.TsumikaeHokanKbn.Checked = (bool)sysinfoEntity.GENBA_TSUMIKAEHOKAN_KBN;
                }

                if (!sysinfoEntity.GENBA_SHOBUN_NIOROSHI_GENBA_KBN.IsNull)
                {
                    this.form.ShobunJigyoujouKbn.Checked = (bool)sysinfoEntity.GENBA_SHOBUN_NIOROSHI_GENBA_KBN;
                }

                if (!sysinfoEntity.GENBA_SAISHUU_SHOBUNJOU_KBN.IsNull)
                {
                    this.form.SaishuuShobunjouKbn.Checked = (bool)sysinfoEntity.GENBA_SAISHUU_SHOBUNJOU_KBN;
                }

                if (!sysinfoEntity.GENBA_MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.ManiHensousakiKbn.Checked = (bool)sysinfoEntity.GENBA_MANI_HENSOUSAKI_KBN;
                }

                decimal shuruiketa = this.form.ManifestShuruiCode.CharactersNumber;
                int Sketa = (int)shuruiketa;
                if (!this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD.IsNull)
                {
                    this.form.ManifestShuruiCode.Text = this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD.ToString().PadLeft(Sketa, '0');
                }
                if (this.manishuruiEntity != null)
                {
                    this.form.ManifestShuruiName.Text = this.manishuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                }
                else
                {
                    this.form.ManifestShuruiName.Text = string.Empty;
                }

                decimal tehaiketa = this.form.ManifestTehaiCode.CharactersNumber;
                int Tketa = (int)shuruiketa;
                if (!this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD.IsNull)
                {
                    this.form.ManifestTehaiCode.Text = this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD.ToString().PadLeft(Tketa, '0');
                }
                if (this.manitehaiEntity != null)
                {
                    this.form.ManifestTehaiName.Text = this.manitehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                }
                else
                {
                    this.form.ManifestTehaiName.Text = string.Empty;
                }
                this.form.ShobunsakiCode.Text = string.Empty;
                this.form.ManiHensousakiName1.Text = string.Empty;
                this.form.ManiHensousakiName2.Text = string.Empty;
                this.form.ManiHensousakiKeishou1.Text = string.Empty;
                this.form.ManiHensousakiKeishou2.Text = string.Empty;
                this.form.ManiHensousakiPost.Text = string.Empty;
                this.form.ManiHensousakiAddress1.Text = string.Empty;
                this.form.ManiHensousakiAddress2.Text = string.Empty;
                this.form.ManiHensousakiBusho.Text = string.Empty;
                this.form.ManiHensousakiTantou.Text = string.Empty;

                // 委託契約情報
                this.form.ItakuKeiyakuIchiran.DataSource = null;
                this.form.ItakuKeiyakuIchiran.Rows.Clear();

                // 定期品名情報
                this.form.SHIKUCHOUSON_CD.Text = string.Empty;
                this.form.SHIKUCHOUSON_NAME_RYAKU.Text = string.Empty;

                //20150609 #10697 「運転者指示事項」の項目を追加する。by hoanghm start
                this.form.UntenshaShijiJikou1.Text = string.Empty;
                this.form.UntenshaShijiJikou2.Text = string.Empty;
                this.form.UntenshaShijiJikou3.Text = string.Empty;
                //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end

                this.SetIchiranTeiki();

                // 月極品名情報
                this.SetIchiranTsuki();

                // mapbox項目
                this.form.GenbaLatitude.Text = string.Empty;
                this.form.GenbaLongitude.Text = string.Empty;
                this.form.LocationInfoUpdateName.Text = string.Empty;
                this.form.LocationInfoUpdateDate.Text = string.Empty;

                // ｼｮｰﾄﾒｯｾｰｼﾞ項目
                if (AppConfig.AppOptions.IsSMS())
                {
                    this.form.SMS_USE.Text = "2";
                    this.SmsReceiverTable_Init();
                }

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                //業者分類タブ初期化
                bool catchErr = this.ManiCheckOffCheck(false);

                this.GenbaEntity = null;

                this.form.GyoushaCode.Focus();
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            // 全コントロールを操作可能とする
            this.AllControlLock(true);

            // 検索結果を画面に設定
            this.SetWindowData();

            // functionボタン
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消

            // 承認済申請一覧から遷移時は引合現場の適用開始・終了日を優先
            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                //適用開始日(当日日付)
                this.form.TekiyouKikanForm.Value = parentForm.sysDate;
                //適用終了日
                this.form.TekiyouKikanTo.Value = null;
            }
            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;

            // 委託契約情報
            this.form.ItakuKeiyakuUseKbn.Text = "1";
            this.form.ItakuKeiyakuIchiran.DataSource = null;
            this.form.ItakuKeiyakuIchiran.Rows.Clear();

            // 発行先コード
            if (!this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
            }

            // 業者分類タブ初期化
            bool catchErr = this.ManiCheckOffCheck(false);
            if (catchErr)
            {
                throw new Exception("");
            }

            //複写モード時は現場コードのコピーはなし
            this.form.GenbaCode.Text = string.Empty;

            // 複写モード時はmapbox項目のコピーなし
            this.form.GenbaLatitude.Text = string.Empty;
            this.form.GenbaLongitude.Text = string.Empty;
            this.form.LocationInfoUpdateName.Text = string.Empty;
            this.form.LocationInfoUpdateDate.Text = string.Empty;
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.GyoushaCode.Enabled = false;   // 業者CD
                this.form.GyoushaCodeSearchButton.Enabled = false;   // 業者CDボタン
                this.form.GenbaCode.Enabled = false;   // 現場CD
                this.form.bt_genbacd_saiban.Enabled = false;    // 採番ボタン

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                // 業者分類タブ初期化
                bool catchErr = this.ManiCheckOffCheck(false);
                return catchErr;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitUpdate", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            this.SetWindowData();

            // 削除モード固有UI設定
            this.AllControlLock(false);

            // functionボタン
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;   // 取消
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.SetWindowData();

                // 削除モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func3.Enabled = true;     // 修正
                parentForm.bt_func9.Enabled = false;     // 登録
                parentForm.bt_func11.Enabled = false;    // 取消
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitReference", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            // 画面設定中フラグ設定
            this.isSettingWindowData = true;

            // 各種データ取得
            this.GenbaEntity = null;
            this.LoadHikiaiGenbaEntity = null;
            this.LoadKariGenbaEntity = null;
            if (false == this.IsFromShouninzumiDenshiShinseiIchiran)
            {
                // 通常の取得処理
                this.SearchGenba();
                this.SearchTeiki();
                this.SearchTsuki();
            }
            else
            {
                if (null == this.form.ShinseiHikiaiGenbaCd || String.IsNullOrEmpty(this.form.ShinseiHikiaiGenbaCd))
                {
                    // 修正申請なので仮現場を取得
                    this.SearchKariGenba();
                    // 現場エンティティにコピー
                    this.CopyKariGenbaEntityToGenbaEntity();

                    this.SearchKariGenbaTeikiHinmei();
                    this.SearchKariGenbaTsukiHinmei();
                }
                else
                {
                    // 新規申請なので引合現場を取得
                    this.SearchHikiaiGenba();
                    // 現場エンティティにコピー
                    this.CopyHikiaiGenbaEntityToGenbaEntity();

                    this.SearchHikiaiGenbaTeikiHinmei();
                    this.SearchHikiaiGenbaTsukiHinmei();
                }
            }

            this.SearchBusho();
            this.SearchChiiki();
            this.SearchGyousha();
            this.SearchGyoushu();
            this.rowCntItaku = this.SearchItaku();
            this.SearchTorihikisaki();
            this.SearchKyoten();
            this.SearchShain();
            this.SearchShuukeiItem();
            this.SearchTodoufuken();
            this.GetSysInfo();
            this.SearchManiShurui();
            this.SearchManiTehai();

            // 20141208 ブン 運搬報告書提出データを取得する start
            this.SearchUpnHoukokushoTeishutsuChiiki();
            // 20141208 ブン 運搬報告書提出データを取得する end

            // BaseHeader部
            BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
            DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
            header.CreateDate.Text = this.GenbaEntity.CREATE_DATE.ToString();
            header.CreateUser.Text = this.GenbaEntity.CREATE_USER;
            header.LastUpdateDate.Text = this.GenbaEntity.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.GenbaEntity.UPDATE_USER;

            // 共通部
            if (this.gyoushaEntity != null)
            {
                this.form.GyoushaCode.Text = this.GenbaEntity.GYOUSHA_CD;
                this.form.GyoushaName1.Text = this.gyoushaEntity.GYOUSHA_NAME1;
                this.form.GyoushaName2.Text = this.gyoushaEntity.GYOUSHA_NAME2;

                if (this.gyoushaEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.GYOUSHA_TEKIYOU_BEGIN.Value = this.gyoushaEntity.TEKIYOU_BEGIN.Value;
                }

                if (this.gyoushaEntity.TEKIYOU_END.IsNull)
                {
                    this.form.GYOUSHA_TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.GYOUSHA_TEKIYOU_END.Value = this.gyoushaEntity.TEKIYOU_END.Value;
                }

                this.form.GyoushaKbnUkeire.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_UKEIRE;
                this.form.GyoushaKbnShukka.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_SHUKKA;
                this.form.GyoushaKbnMani.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_MANI;
            }

            if (this.torihikisakiEntity != null)
            {
                this.form.TorihikisakiCode.Text = this.GenbaEntity.TORIHIKISAKI_CD;
                this.form.TorihikisakiName1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                this.form.TorihikisakiName2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                if (this.torihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.torihikisakiEntity.TEKIYOU_BEGIN.Value;
                }

                if (this.torihikisakiEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.torihikisakiEntity.TEKIYOU_END.Value;
                }
                if (!this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.form.KyotenCode.Text = this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                }
            }
            if (this.kyotenEntity != null)
            {
                this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
            }
            this.form.GenbaCode.Text = this.GenbaEntity.GENBA_CD;
            this.form.GenbaFurigana.Text = this.GenbaEntity.GENBA_FURIGANA;
            this.form.GenbaName1.Text = this.GenbaEntity.GENBA_NAME1;
            this.form.GenbaName2.Text = this.GenbaEntity.GENBA_NAME2;
            this.form.GenbaKeishou1.Text = this.GenbaEntity.GENBA_KEISHOU1;
            this.form.GenbaKeishou2.Text = this.GenbaEntity.GENBA_KEISHOU2;
            this.form.GenbaNameRyaku.Text = this.GenbaEntity.GENBA_NAME_RYAKU;
            this.form.GenbaTel.Text = this.GenbaEntity.GENBA_TEL;
            this.form.GenbaKeitaiTel.Text = this.GenbaEntity.GENBA_KEITAI_TEL;
            this.form.GenbaFax.Text = this.GenbaEntity.GENBA_FAX;

            if (this.bushoEntity != null)
            {
                this.form.EigyouTantouBushoCode.Text = this.GenbaEntity.EIGYOU_TANTOU_BUSHO_CD;
                this.form.EigyouTantouBushoName.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
            }
            if (this.shainEntity != null)
            {
                this.form.EigyouCode.Text = this.GenbaEntity.EIGYOU_TANTOU_CD;
                this.form.EigyouName.Text = this.shainEntity.SHAIN_NAME_RYAKU;
            }

            if (this.GenbaEntity.TEKIYOU_BEGIN.IsNull)
            {
                this.form.TekiyouKikanForm.Value = null;
            }
            else
            {
                this.form.TekiyouKikanForm.Value = this.GenbaEntity.TEKIYOU_BEGIN.Value;
            }

            if (this.GenbaEntity.TEKIYOU_END.IsNull)
            {
                this.form.TekiyouKikanTo.Value = null;
            }
            else
            {
                this.form.TekiyouKikanTo.Value = this.GenbaEntity.TEKIYOU_END.Value;
            }

            this.form.ChuusiRiyuu1.Text = this.GenbaEntity.CHUUSHI_RIYUU1;
            this.form.ChuusiRiyuu2.Text = this.GenbaEntity.CHUUSHI_RIYUU2;

            if (!this.GenbaEntity.SHOKUCHI_KBN.IsNull)
            {
                this.form.ShokuchiKbn.Checked = (bool)this.GenbaEntity.SHOKUCHI_KBN;
            }
            //if (!this.GenbaEntity.DEN_MANI_SHOUKAI_KBN.IsNull)
            //{
            //    this.form.DenManiShoukaiKbn.Checked = (bool)this.GenbaEntity.DEN_MANI_SHOUKAI_KBN;
            //}
            if (!this.GenbaEntity.KENSHU_YOUHI.IsNull)
            {
                this.form.KENSHU_YOUHI.Checked = (bool)this.GenbaEntity.KENSHU_YOUHI;
            }

            //20250320
            this.form.CHIZU.Text = this.GenbaEntity.CHIZU;

            // 基本情報
            this.form.GenbaPost.Text = this.GenbaEntity.GENBA_POST;
            if (!this.GenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
            {
                if (this.todoufukenEntity != null)
                {
                    this.form.GenbaTodoufukenCode.Text = this.GenbaEntity.GENBA_TODOUFUKEN_CD.ToString();
                    this.form.GenbaTodoufukenNameRyaku.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                }
            }
            this.form.GenbaAddress1.Text = this.GenbaEntity.GENBA_ADDRESS1;
            this.form.GenbaAddress2.Text = this.GenbaEntity.GENBA_ADDRESS2;

            if (this.chiikiEntity != null)
            {
                this.form.ChiikiCode.Text = this.GenbaEntity.CHIIKI_CD;
                this.form.ChiikiName.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
            }

            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.GenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = (this.upnHoukokushoTeishutsuChiikiEntity == null) ? string.Empty : this.upnHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;

            this.form.BushoCode.Text = this.GenbaEntity.BUSHO;
            this.form.TantoushaCode.Text = this.GenbaEntity.TANTOUSHA;
            this.form.KoufutantoushaCode.Text = this.GenbaEntity.KOUFU_TANTOUSHA;

            if (this.shuukeiEntity != null)
            {
                this.form.ShuukeiItemCode.Text = this.GenbaEntity.SHUUKEI_ITEM_CD;
                this.form.ShuukeiItemName.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
            }

            if (this.gyoushuEntity != null)
            {
                this.form.GyoushuCode.Text = this.GenbaEntity.GYOUSHU_CD;
                this.form.GyoushuName.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
            }

            this.form.Bikou1.Text = this.GenbaEntity.BIKOU1;
            this.form.Bikou2.Text = this.GenbaEntity.BIKOU2;
            this.form.Bikou3.Text = this.GenbaEntity.BIKOU3;
            this.form.Bikou4.Text = this.GenbaEntity.BIKOU4;

            //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm start
            this.form.UntenshaShijiJikou1.Text = this.GenbaEntity.UNTENSHA_SHIJI_JIKOU1;
            this.form.UntenshaShijiJikou2.Text = this.GenbaEntity.UNTENSHA_SHIJI_JIKOU2;
            this.form.UntenshaShijiJikou3.Text = this.GenbaEntity.UNTENSHA_SHIJI_JIKOU3;
            //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end

            Boolean isKake = true;
            // 請求情報
            if (this._tabPageManager.IsVisible("tabPageSeikyuuInfo"))
            {
                this.form.SeikyuushoSoufusaki1.Text = this.GenbaEntity.SEIKYUU_SOUFU_NAME1;
                this.form.SeikyuushoSoufusaki2.Text = this.GenbaEntity.SEIKYUU_SOUFU_NAME2;
                this.form.SeikyuuSouhuKeishou1.Text = this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU1;
                this.form.SeikyuuSouhuKeishou2.Text = this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU2;

                this.form.SeikyuuSoufuPost.Text = this.GenbaEntity.SEIKYUU_SOUFU_POST;
                this.form.SeikyuuSoufuAddress1.Text = this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS1;
                this.form.SeikyuuSoufuAddress2.Text = this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS2;
                this.form.SeikyuuSoufuBusho.Text = this.GenbaEntity.SEIKYUU_SOUFU_BUSHO;
                this.form.SeikyuuSoufuTantou.Text = this.GenbaEntity.SEIKYUU_SOUFU_TANTOU;
                this.form.SoufuGenbaTel.Text = this.GenbaEntity.SEIKYUU_SOUFU_TEL;
                this.form.SoufuGenbaFax.Text = this.GenbaEntity.SEIKYUU_SOUFU_FAX;
                this.form.SeikyuuTantou.Text = this.GenbaEntity.SEIKYUU_TANTOU;

                this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
                if (!this.GenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SeikyuuDaihyouPrintKbn.Text = this.GenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.GenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.GenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!this.GenbaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GenbaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }

                // Begin: LANDUONG - 20220215 - refs#160054                
                this.form.HAKKOUSAKI_CD.Text = this.GenbaEntity.HAKKOUSAKI_CD;
                this.form.RAKURAKU_CUSTOMER_CD.Text = this.GenbaEntity.RAKURAKU_CUSTOMER_CD;                
                this.HakkousakuAndRakurakuCDCheck();                
                // End: LANDUONG - 20220215 - refs#160054

                Boolean isSeikyuuKake = true;
                if (this.torihikisakiEntity != null)
                {
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);

                    if (seikyuuEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isSeikyuuKake = false;
                        }
                        else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isSeikyuuKake = true;
                        }
                    }

                    //非活性⇔活性
                    if ((!isSeikyuuKake && this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled) || (isSeikyuuKake && !this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSeikyuuControl(isSeikyuuKake);
                    }
                }
            }

            // 支払情報
            if (this._tabPageManager.IsVisible("tabPageShiharaiInfo"))
            {
                this.form.ShiharaiSoufuName1.Text = this.GenbaEntity.SHIHARAI_SOUFU_NAME1;
                this.form.ShiharaiSoufuName2.Text = this.GenbaEntity.SHIHARAI_SOUFU_NAME2;
                this.form.ShiharaiSoufuKeishou1.Text = this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU1;
                this.form.ShiharaiSoufuKeishou2.Text = this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU2;

                this.form.ShiharaiSoufuPost.Text = this.GenbaEntity.SHIHARAI_SOUFU_POST;
                this.form.ShiharaiSoufuAddress1.Text = this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS1;
                this.form.ShiharaiSoufuAddress2.Text = this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS2;
                this.form.ShiharaiSoufuBusho.Text = this.GenbaEntity.SHIHARAI_SOUFU_BUSHO;
                this.form.ShiharaiSoufuTantou.Text = this.GenbaEntity.SHIHARAI_SOUFU_TANTOU;
                this.form.ShiharaiGenbaTel.Text = this.GenbaEntity.SHIHARAI_SOUFU_TEL;
                this.form.ShiharaiGenbaFax.Text = this.GenbaEntity.SHIHARAI_SOUFU_FAX;

                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (!this.GenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.GenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                if (!this.GenbaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.GenbaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (kyo != null)
                    {
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                }

                Boolean isShiharaiKake = true;
                if (this.torihikisakiEntity != null)
                {
                    M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);

                    if (shiharaiEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isShiharaiKake = false;
                        }
                        else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isShiharaiKake = true;
                        }
                    }

                    //非活性⇔活性
                    if ((!isShiharaiKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isShiharaiKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSiharaiControl(isKake);
                    }
                }
            }

            // 現場分類
            if (!this.GenbaEntity.JISHA_KBN.IsNull)
            {
                this.form.JishaKbn.Checked = (bool)this.GenbaEntity.JISHA_KBN;
            }
            else
            {
                this.form.JishaKbn.Checked = false;
            }
            if (!this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsNull)
            {
                this.form.HaishutsuKbn.Checked = (bool)this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
            }
            else
            {
                this.form.HaishutsuKbn.Checked = false;
            }
            if (!this.GenbaEntity.TSUMIKAEHOKAN_KBN.IsNull)
            {
                this.form.TsumikaeHokanKbn.Checked = (bool)this.GenbaEntity.TSUMIKAEHOKAN_KBN;
            }
            else
            {
                this.form.TsumikaeHokanKbn.Checked = false;
            }
            if (!this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsNull)
            {
                this.form.ShobunJigyoujouKbn.Checked = (bool)this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN;
            }
            else
            {
                this.form.ShobunJigyoujouKbn.Checked = false;
            }
            if (!this.GenbaEntity.SAISHUU_SHOBUNJOU_KBN.IsNull)
            {
                this.form.SaishuuShobunjouKbn.Checked = (bool)this.GenbaEntity.SAISHUU_SHOBUNJOU_KBN;
            }
            else
            {
                this.form.SaishuuShobunjouKbn.Checked = false;
            }
            if (!this.GenbaEntity.MANI_HENSOUSAKI_KBN.IsNull)
            {
                this.form.ManiHensousakiKbn.Checked = (bool)this.GenbaEntity.MANI_HENSOUSAKI_KBN;
            }
            else
            {
                this.form.ManiHensousakiKbn.Checked = false;
            }

            if (this.manishuruiEntity != null)
            {
                this.form.ManifestShuruiCode.Text = this.GenbaEntity.MANIFEST_SHURUI_CD.ToString();
                this.form.ManifestShuruiName.Text = this.manishuruiEntity.MANIFEST_SHURUI_NAME;
            }
            else
            {
                this.form.ManifestShuruiCode.Text = "";
                this.form.ManifestShuruiName.Text = "";
            }
            if (this.manitehaiEntity != null)
            {
                this.form.ManifestTehaiCode.Text = this.GenbaEntity.MANIFEST_TEHAI_CD.ToString();
                this.form.ManifestTehaiName.Text = this.manitehaiEntity.MANIFEST_TEHAI_NAME;
            }
            else
            {
                this.form.ManifestTehaiCode.Text = "";
                this.form.ManifestTehaiName.Text = "";
            }

            this.form.ShobunsakiCode.Text = this.GenbaEntity.SHOBUNSAKI_NO;

            if (this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
            }
            else
            {
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text == "1")
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                }
            }
            if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
            {
                this.form.ManiHensousakiName1.Text = this.GenbaEntity.MANI_HENSOUSAKI_NAME1;
                this.form.ManiHensousakiName2.Text = this.GenbaEntity.MANI_HENSOUSAKI_NAME2;
                this.form.ManiHensousakiKeishou1.Text = this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1;
                this.form.ManiHensousakiKeishou2.Text = this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2;
                this.form.ManiHensousakiPost.Text = this.GenbaEntity.MANI_HENSOUSAKI_POST;
                this.form.ManiHensousakiAddress1.Text = this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1;
                this.form.ManiHensousakiAddress2.Text = this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2;
                this.form.ManiHensousakiBusho.Text = this.GenbaEntity.MANI_HENSOUSAKI_BUSHO;
                this.form.ManiHensousakiTantou.Text = this.GenbaEntity.MANI_HENSOUSAKI_TANTOU;
            }

            #region  A票～E票画面に設定

            this.SetAToEWindowsData();

            //if (this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD != "")
            //{
            //    this.form.HensousakiKbn.Text = "1";
            //    this.form.ManiHensousakiTorihikisakiCode.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
            //    this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
            //    this.form.ManiHensousakiGyoushaCode.Enabled = false;
            //    this.form.ManiHensousakiGenbaCode.Enabled = false;
            //}
            //else if (this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD != "" && this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD != "")
            //{
            //    this.form.HensousakiKbn.Text = "3";
            //    this.form.ManiHensousakiGenbaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD;
            //    this.form.ManiHensousakiGyoushaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD;
            //    this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
            //    this.form.ManiHensousakiGyoushaCode.Enabled = true;
            //    this.form.ManiHensousakiGenbaCode.Enabled = true;
            //}
            //else if (this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD != "")
            //{
            //    this.form.HensousakiKbn.Text = "2";
            //    this.form.ManiHensousakiGyoushaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD;
            //    this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
            //    this.form.ManiHensousakiGyoushaCode.Enabled = true;
            //    this.form.ManiHensousakiGenbaCode.Enabled = false;
            //}
            //else if (!this.form.ManiHensousakiKbn.Checked)
            //{
            //    if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
            //    {
            //        this.form.HensousakiKbn.Text = "1";
            //        this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
            //        this.form.ManiHensousakiGyoushaCode.Enabled = false;
            //        this.form.ManiHensousakiGenbaCode.Enabled = false;
            //    }
            //    else
            //    {
            //        this.form.HensousakiKbn.Text = string.Empty;
            //        this.form.HensousakiKbn1.Checked = false;
            //        this.form.HensousakiKbn2.Checked = false;
            //        this.form.HensousakiKbn3.Checked = false;
            //        this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
            //        this.form.ManiHensousakiGyoushaCode.Enabled = false;
            //        this.form.ManiHensousakiGenbaCode.Enabled = false;
            //    }
            //}
            //else
            //{
            //    this.form.HensousakiKbn.Text = string.Empty;
            //    this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
            //    this.form.ManiHensousakiGyoushaCode.Enabled = false;
            //    this.form.ManiHensousakiGenbaCode.Enabled = false;
            //}

            //this.form.ManiHensousakiName1.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME1;
            //this.form.ManiHensousakiName2.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME2;
            //this.form.ManiHensousakiKeishou1.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1;
            //this.form.ManiHensousakiKeishou2.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2;

            //this.form.ManiHensousakiPost.Text = this.genbaEntity.MANI_HENSOUSAKI_POST;
            //this.form.ManiHensousakiAddress1.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1;
            //this.form.ManiHensousakiAddress2.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2;
            //this.form.ManiHensousakiBusho.Text = this.genbaEntity.MANI_HENSOUSAKI_BUSHO;
            //this.form.ManiHensousakiTantou.Text = this.genbaEntity.MANI_HENSOUSAKI_TANTOU;

            #endregion

            // 委託契約情報
            this.form.ItakuKeiyakuUseKbn.Text = this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN.IsNull ? string.Empty : this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN.Value.ToString();
            if (this.rowCntItaku > 0)
            {
                this.SetIchiranItaku();
            }

            // 定期契約情報
            if (this.GenbaEntity.SHIKUCHOUSON_CD != null)
            {
                this.form.SHIKUCHOUSON_CD.Text = this.GenbaEntity.SHIKUCHOUSON_CD.ToString();
                if (!string.IsNullOrWhiteSpace(this.GenbaEntity.SHIKUCHOUSON_CD.ToString()))
                {
                    M_SHIKUCHOUSON shiku = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>().GetDataByCd(this.GenbaEntity.SHIKUCHOUSON_CD.ToString());
                    if (shiku != null)
                    {
                        this.form.SHIKUCHOUSON_NAME_RYAKU.Text = shiku.SHIKUCHOUSON_NAME_RYAKU;
                    }
                }
            }
            this.SetIchiranTeiki();

            // 月極契約情報
            this.SetIchiranTsuki();

            // mapbox項目
            this.form.GenbaLatitude.Text = this.GenbaEntity.GENBA_LATITUDE;
            this.form.GenbaLongitude.Text = this.GenbaEntity.GENBA_LONGITUDE;
            this.form.LocationInfoUpdateName.Text = this.GenbaEntity.GENBA_LOCATION_INFO_UPDATE_NAME;
            if (!this.GenbaEntity.GENBA_LOCATION_INFO_UPDATE_DATE.IsNull)
            {
                this.form.LocationInfoUpdateDate.Text = this.GenbaEntity.GENBA_LOCATION_INFO_UPDATE_DATE.ToString();
            }

            if (AppConfig.AppOptions.IsSMS())
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞ項目
                if (!this.GenbaEntity.SMS_USE.IsNull)
                {
                    this.form.SMS_USE.Text = this.GenbaEntity.SMS_USE.ToString();
                }
                else
                {
                    this.form.SMS_USE.Text = "2";
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞ送信区分の値によって、一覧の入力可否を変更
                if (this.form.SMS_USE.Text == "1")
                {
                    this.SmsEnable(false);
                }
                else
                {
                    this.SmsEnable(true);
                }
            }

            // 業者データでの取引先チェック
            bool catchErr = this.SearchchkGyousha(false, true);
            if (catchErr)
            {
                throw new Exception("");
            }

            // 画面設定中フラグ解除
            this.isSettingWindowData = true;
        }

        #region 返送先区分調整メソッド

        /// <summary>
        /// 返送先区分調整メソッド
        /// 返送先区分の活性制御がおかしいときに調整用として呼び出してください。
        /// </summary>
        /// <param name="windowType"></param>
        internal bool SettingHensouSakiKbn()
        {
            try
            {
                var maniHensousaki = this.form.ManiHensousakiKbn.Checked;
                bool catchErr = false;

                #region A票

                var aHyouUseKbn = this.form.MANIFEST_USE_AHyo.Text;
                //返送先(票)判定
                var ahyoName = this.ChkTabEvent(this.form.MANIFEST_USE_AHyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == aHyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(ahyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(ahyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region B1票

                var b1HyouUseKbn = this.form.MANIFEST_USE_B1Hyo.Text;
                //返送先(票)判定
                var b1hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B1Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == b1HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(b1hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(b1hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region B2票

                var b2HyouUseKbn = this.form.MANIFEST_USE_B2Hyo.Text;
                //返送先(票)判定
                var b2hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B2Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == b2HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(b2hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(b2hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region B4票

                var b4HyouUseKbn = this.form.MANIFEST_USE_B4Hyo.Text;
                //返送先(票)判定
                var b4hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B4Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == b4HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(b4hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(b4hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region B6票

                var b6HyouUseKbn = this.form.MANIFEST_USE_B6Hyo.Text;
                //返送先(票)判定
                var b6hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B6Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == b6HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(b6hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(b6hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region C1票

                var c1HyouUseKbn = this.form.MANIFEST_USE_C1Hyo.Text;
                //返送先(票)判定
                var c1hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_C1Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == c1HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(c1hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(c1hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region C2票

                var c2HyouUseKbn = this.form.MANIFEST_USE_C2Hyo.Text;
                //返送先(票)判定
                var c2hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_C2Hyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == c2HyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(c2hyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(c2hyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region D票

                var dHyouUseKbn = this.form.MANIFEST_USE_DHyo.Text;
                //返送先(票)判定
                var dhyoName = this.ChkTabEvent(this.form.MANIFEST_USE_DHyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == dHyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(dhyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(dhyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                #region E票

                var eHyouUseKbn = this.form.MANIFEST_USE_EHyo.Text;
                //返送先(票)判定
                var ehyoName = this.ChkTabEvent(this.form.MANIFEST_USE_EHyo.Name, out catchErr);
                if (catchErr)
                {
                    return true;
                }

                if ("1" == eHyouUseKbn)
                {
                    // マニフェスト返送先のチェック状態に応じて状態変更
                    catchErr = this.SetEnabledManifestHensousakiRendou(ehyoName, maniHensousaki);
                }
                else
                {
                    // 全て使用不可
                    catchErr = this.SetEnabledFalseManifestHensousaki(ehyoName);
                }

                if (catchErr)
                {
                    return true;
                }

                #endregion

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SettingHensouSakiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 全コントロール制御メソッド

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            //初期値セット部
            this.form.SeikyuuDaihyouPrintKbn.Enabled = isBool;
            this.form.ItakuKeiyakuUseKbn.Enabled = isBool;
            //this.form.HensousakiKbn.Enabled = isBool;

            // 共通部
            this.form.GyoushaCode.Enabled = isBool;
            //this.form.GyoushaKbnUkeire.Enabled = isBool;
            //this.form.GyoushaKbnShukka.Enabled = isBool;
            //this.form.GyoushaKbnMani.Enabled = isBool;
            //this.form.GyoushaName1.Enabled = isBool;
            //this.form.GyoushaName2.Enabled = isBool;
            this.form.TorihikisakiCode.Enabled = isBool;
            this.form.bt_gyousya_copy.Enabled = isBool;
            this.form.GyoushaNew.Enabled = isBool;
            this.form.BT_GYOUSHA_REFERENCE.Enabled = isBool;
            this.form.bt_torihikisaki_copy.Enabled = isBool;
            //this.form.TorihikisakiName1.Enabled = isBool;
            //this.form.TorihikisakiName2.Enabled = isBool;
            //this.form.KyotenCode.Enabled = isBool;
            //this.form.KyotenName.Enabled = isBool;
            this.form.GenbaCode.Enabled = isBool;
            this.form.GenbaFurigana.Enabled = isBool;
            this.form.GenbaName1.Enabled = isBool;
            this.form.GenbaName2.Enabled = isBool;
            //this.form.ManiHensousakiKbnHidden.Enabled = isBool;
            this.form.GenbaNameRyaku.Enabled = isBool;
            this.form.GenbaTel.Enabled = isBool;
            this.form.GenbaKeitaiTel.Enabled = isBool;
            this.form.GenbaFax.Enabled = isBool;
            this.form.EigyouTantouBushoCode.Enabled = isBool;
            //this.form.EigyouTantouBushoName.Enabled = isBool;
            this.form.EigyouCode.Enabled = isBool;
            //this.form.EigyouName.Enabled = isBool;
            this.form.EigyouTantouBushoCode.Enabled = isBool;
            this.form.TekiyouKikanForm.Enabled = isBool;
            this.form.TekiyouKikanTo.Enabled = isBool;
            this.form.ChuusiRiyuu1.Enabled = isBool;
            this.form.ChuusiRiyuu2.Enabled = isBool;
            this.form.ShokuchiKbn.Enabled = isBool;
            //this.form.DenManiShoukaiKbn.Enabled = isBool;
            this.form.KENSHU_YOUHI.Enabled = isBool;

            //20250320
            this.form.CHIZU.Enabled = isBool;

            // 基本情報
            this.form.GenbaPost.Enabled = isBool;
            this.form.GenbaTodoufukenCode.Enabled = isBool;
            //this.form.GenbaTodoufukenNameRyaku.Enabled = isBool;
            this.form.GenbaAddress1.Enabled = isBool;
            this.form.GenbaAddress2.Enabled = isBool;
            this.form.ChiikiCode.Enabled = isBool;
            //this.form.ChiikiName.Enabled = isBool;
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Enabled = isBool;
            this.form.bt_upn_houkokusho_teishutsu_chiiki_search.Enabled = isBool;
            this.form.BushoCode.Enabled = isBool;
            this.form.TantoushaCode.Enabled = isBool;
            this.form.KoufutantoushaCode.Enabled = isBool;
            this.form.ShuukeiItemCode.Enabled = isBool;
            //this.form.ShuukeiItemName.Enabled = isBool;
            this.form.GyoushuCode.Enabled = isBool;
            //this.form.GyoushuName.Enabled = isBool;
            this.form.Bikou1.Enabled = isBool;
            this.form.Bikou2.Enabled = isBool;
            this.form.Bikou3.Enabled = isBool;
            this.form.Bikou4.Enabled = isBool;
            //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm start
            this.form.UntenshaShijiJikou1.Enabled = isBool;
            this.form.UntenshaShijiJikou2.Enabled = isBool;
            this.form.UntenshaShijiJikou3.Enabled = isBool;
            //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end

            // 請求情報
            this.form.SeikyuushoSoufusaki1.Enabled = isBool;
            this.form.SeikyuushoSoufusaki2.Enabled = isBool;
            this.form.SeikyuuSoufuPost.Enabled = isBool;
            this.form.SeikyuuSoufuAddress1.Enabled = isBool;
            this.form.SeikyuuSoufuAddress2.Enabled = isBool;
            this.form.SeikyuuSoufuBusho.Enabled = isBool;
            this.form.SeikyuuSoufuTantou.Enabled = isBool;
            this.form.SoufuGenbaTel.Enabled = isBool;
            this.form.SoufuGenbaFax.Enabled = isBool;
            this.form.SeikyuuTantou.Enabled = isBool;
            this.form.GENBA_COPY_SEIKYU_BUTTON.Enabled = isBool;
            this.form.SeikyuuDaihyouPrintKbn.Enabled = isBool;
            this.form.SeikyuuDaihyouPrintKbn1.Enabled = isBool;
            this.form.SeikyuuDaihyouPrintKbn2.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_CD.Enabled = isBool;
            this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = isBool;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.form.HAKKOUSAKI_CD.Enabled = isBool;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            // Begin: LANDUONG - 20220215 - refs#160054
            this.form.RAKURAKU_CUSTOMER_CD.Enabled = isBool;
            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = isBool;
            // End: LANDUONG - 20220215 - refs#160054

            this.form.bt_seikyuu_torihikisaki_copy.Enabled = isBool;
            this.form.bt_seikyuu_gyousha_copy.Enabled = isBool;

            // 支払情報
            this.form.ShiharaiSoufuName1.Enabled = isBool;
            this.form.ShiharaiSoufuName2.Enabled = isBool;
            this.form.ShiharaiSoufuPost.Enabled = isBool;
            this.form.ShiharaiSoufuAddress1.Enabled = isBool;
            this.form.ShiharaiSoufuAddress2.Enabled = isBool;
            this.form.ShiharaiSoufuBusho.Enabled = isBool;
            this.form.ShiharaiSoufuTantou.Enabled = isBool;
            this.form.ShiharaiGenbaTel.Enabled = isBool;
            this.form.ShiharaiGenbaFax.Enabled = isBool;
            this.form.GENBA_COPY_SIHARAI_BUTTON.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_CD.Enabled = isBool;
            this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = isBool;
            this.form.bt_shiharai_torihikisaki_copy.Enabled = isBool;
            this.form.bt_shiharai_gyousha_copy.Enabled = isBool;

            //現場分類
            this.form.ManifestShuruiCode.Enabled = isBool;
            //this.form.ManifestShuruiName.Enabled = isBool;
            this.form.ManifestTehaiCode.Enabled = isBool;
            //this.form.ManifestTehaiName.Enabled = isBool;
            this.form.ShobunsakiCode.Enabled = isBool;
            //this.form.ManiHensousakiTorihikisakiCode.Enabled = isBool;
            //this.form.ManiHensousakiGyoushaCode.Enabled = isBool;
            //this.form.ManiHensousakiGenbaCode.Enabled = isBool;
            this.form.ManiHensousakiName1.Enabled = isBool;
            this.form.ManiHensousakiName2.Enabled = isBool;
            this.form.ManiHensousakiKeishou1.Enabled = isBool;
            this.form.ManiHensousakiKeishou2.Enabled = isBool;
            this.form.ManiHensousakiPost.Enabled = isBool;
            this.form.ManiHensousakiAddress1.Enabled = isBool;
            this.form.ManiHensousakiAddress2.Enabled = isBool;
            this.form.ManiHensousakiBusho.Enabled = isBool;
            this.form.ManiHensousakiTantou.Enabled = isBool;
            this.form.GENBA_COPY_MANI_BUTTON.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = isBool;
            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = isBool;

            #region A票～E票設定

            this.form.HensousakiKbn_AHyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = isBool;

            this.form.HensousakiKbn_B1Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = isBool;

            this.form.HensousakiKbn_B2Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = isBool;

            this.form.HensousakiKbn_B4Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = isBool;

            this.form.HensousakiKbn_B6Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = isBool;

            this.form.MANIFEST_USE_C1Hyo.Enabled = isBool;
            this.form.HensousakiKbn_C1Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = isBool;

            this.form.HensousakiKbn_C2Hyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = isBool;

            this.form.HensousakiKbn_DHyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = isBool;

            this.form.HensousakiKbn_EHyo.Enabled = isBool;
            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = isBool;
            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = isBool;
            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = isBool;

            this.form.HensousakiKbn1_AHyo.Enabled = isBool;
            this.form.HensousakiKbn2_AHyo.Enabled = isBool;
            this.form.HensousakiKbn3_AHyo.Enabled = isBool;
            this.form.ManiHensousakiKeishou1.Enabled = isBool;
            this.form.ManiHensousakiKeishou2.Enabled = isBool;
            this.form.ManiHensousakiPostSearchButton.Enabled = isBool;
            this.form.ManiHensousakiAddressSearchButton.Enabled = isBool;

            this.form.HensousakiKbn1_B1Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_B1Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_B1Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_B2Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_B2Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_B2Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_B4Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_B4Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_B4Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_B6Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_B6Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_B6Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_C1Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_C1Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_C1Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_C2Hyo.Enabled = isBool;
            this.form.HensousakiKbn2_C2Hyo.Enabled = isBool;
            this.form.HensousakiKbn3_C2Hyo.Enabled = isBool;

            this.form.HensousakiKbn1_DHyo.Enabled = isBool;
            this.form.HensousakiKbn2_DHyo.Enabled = isBool;
            this.form.HensousakiKbn3_DHyo.Enabled = isBool;

            this.form.HensousakiKbn1_EHyo.Enabled = isBool;
            this.form.HensousakiKbn2_EHyo.Enabled = isBool;
            this.form.HensousakiKbn3_EHyo.Enabled = isBool;

            this.form.MANIFEST_USE_AHyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_AHyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_AHyo.Enabled = isBool;
            this.form.MANIFEST_USE_B1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_B1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_B1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_B2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_B2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_B2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_B4Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_B4Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_B4Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_B6Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_B6Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_B6Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_C1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_C1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_C1Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_C2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_C2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_C2Hyo.Enabled = isBool;
            this.form.MANIFEST_USE_DHyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_DHyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_DHyo.Enabled = isBool;
            this.form.MANIFEST_USE_EHyo.Enabled = isBool;
            this.form.MANIFEST_USE_1_EHyo.Enabled = isBool;
            this.form.MANIFEST_USE_2_EHyo.Enabled = isBool;

            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = isBool;
            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = isBool;

            #endregion

            // 委託契約情報
            //this.form.ItakuKeiyakuIchiran.Enabled = isBool;

            // 定期情報
            this.form.SHIKUCHOUSON_CD.Enabled = isBool;
            this.form.SHIKUCHOUSON_CD_SEARCH.Enabled = isBool;
            this.form.TeikiHinmeiIchiran.ReadOnly = !isBool;

            // 月極情報
            this.form.TsukiHinmeiIchiran.ReadOnly = !isBool;

            //追加分
            this.form.GyoushaCodeSearchButton.Enabled = isBool;
            this.form.TorihikisakiCodeSearchButton.Enabled = isBool;
            this.form.TorihikisakiNew.Enabled = isBool;
            this.form.BT_TORIHIKISAKI_REFERENCE.Enabled = isBool;
            this.form.bt_genbacd_saiban.Enabled = isBool;
            this.form.GenbaKeishou1.Enabled = isBool;
            this.form.GenbaKeishou2.Enabled = isBool;
            this.form.EigyouTantouBushoSearchButton.Enabled = isBool;
            this.form.bt_tantousha_search.Enabled = isBool;

            //20250320
            this.form.BT_SANSHO.Enabled = isBool;
            //this.form.BT_ETSURAN.Enabled = isBool;

            this.form.GenbaAddressSearchButton.Enabled = isBool;
            this.form.GenbaTodoufukenSearchButton.Enabled = isBool;
            this.form.GenbaPostSearchButton.Enabled = isBool;
            this.form.ChiikiSearchButton.Enabled = isBool;
            this.form.ShuukeiKoumokuSearchButton.Enabled = isBool;
            this.form.GyoushuSearchButton.Enabled = isBool;

            this.form.SeikyuuSouhuKeishou1.Enabled = isBool;
            this.form.SeikyuuSouhuKeishou2.Enabled = isBool;
            this.form.SeikyuuSoufuAddressSearchButton.Enabled = isBool;
            this.form.SeikyuuSoufuPostSearchButton.Enabled = isBool;
            this.form.SeikyuuDaihyouPrintKbn1.Enabled = isBool;
            this.form.SeikyuuDaihyouPrintKbn2.Enabled = isBool;

            this.form.ShiharaiSoufuKeishou1.Enabled = isBool;
            this.form.ShiharaiSoufuKeishou2.Enabled = isBool;
            this.form.ShiharaiSoufuAddressSearchButton.Enabled = isBool;
            this.form.ShiharaiSoufuPostSearchButton.Enabled = isBool;

            this.form.JishaKbn.Enabled = isBool;
            this.form.HaishutsuKbn.Enabled = isBool;
            this.form.TsumikaeHokanKbn.Enabled = isBool;
            this.form.ShobunJigyoujouKbn.Enabled = isBool;
            this.form.SaishuuShobunjouKbn.Enabled = isBool;
            this.form.ManiHensousakiKbn.Enabled = isBool;

            //this.form.HensousakiKbn1.Enabled = isBool;
            //this.form.HensousakiKbn2.Enabled = isBool;
            //this.form.HensousakiKbn3.Enabled = isBool;

            //this.form.ManiTorihikisakiCodeSearchButton.Enabled = isBool;
            //this.form.ManiGyoushaCodeSearchButton.Enabled = isBool;
            //this.form.ManiGenbaCodeSearchButton.Enabled = isBool;
            //this.form.HensousakiDelete.Enabled = isBool;

            //this.form.ManiHensousakiKeishou1.Enabled = isBool;
            //this.form.ManiHensousakiKeishou2.Enabled = isBool;
            //this.form.ManiHensousakiPostSearchButton.Enabled = isBool;
            //this.form.ManiHensousakiAddressSearchButton.Enabled = isBool;

            this.form.ItakuKeiyakuUseKbn1.Enabled = isBool;
            this.form.ItakuKeiyakuUseKbn2.Enabled = isBool;

            // 地図連携情報タブ
            this.form.GenbaLatitude.Enabled = isBool;
            this.form.GenbaLongitude.Enabled = isBool;
            this.form.bt_map_open.Enabled = isBool;

            // ｼｮｰﾄﾒｯｾｰｼﾞタブ
            this.form.SMS_USE_1.Enabled = isBool;
            this.form.SMS_USE_2.Enabled = isBool;
            this.form.SMSReceiverIchiran.ReadOnly = !isBool;
        }

        #endregion 全コントロール制御メソッド

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                DateTime timeBegin = new DateTime();
                DateTime timeEnd = new DateTime();

                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット Start
                //          TIME_STAMP設定部分修正方法更新
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.GenbaEntity == null ||
                    this.GenbaEntity.TIME_STAMP == null || this.GenbaEntity.TIME_STAMP.Length != 8)
                {
                    this.GenbaEntity = new M_GENBA();
                }
                else
                {
                    this.GenbaEntity = new M_GENBA() { TIME_STAMP = this.GenbaEntity.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End
                this.GenbaEntity.BIKOU1 = this.form.Bikou1.Text;
                this.GenbaEntity.BIKOU2 = this.form.Bikou2.Text;
                this.GenbaEntity.BIKOU3 = this.form.Bikou3.Text;
                this.GenbaEntity.BIKOU4 = this.form.Bikou4.Text;
                //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm start
                this.GenbaEntity.UNTENSHA_SHIJI_JIKOU1 = this.form.UntenshaShijiJikou1.Text;
                this.GenbaEntity.UNTENSHA_SHIJI_JIKOU2 = this.form.UntenshaShijiJikou2.Text;
                this.GenbaEntity.UNTENSHA_SHIJI_JIKOU3 = this.form.UntenshaShijiJikou3.Text;
                //20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end
                this.GenbaEntity.BUSHO = this.form.BushoCode.Text;
                this.GenbaEntity.CHIIKI_CD = this.form.ChiikiCode.Text;
                this.GenbaEntity.CHUUSHI_RIYUU1 = this.form.ChuusiRiyuu1.Text;
                this.GenbaEntity.CHUUSHI_RIYUU2 = this.form.ChuusiRiyuu2.Text;
                //this.GenbaEntity.DEN_MANI_SHOUKAI_KBN = this.form.DenManiShoukaiKbn.Checked;
                this.GenbaEntity.KENSHU_YOUHI = this.form.KENSHU_YOUHI.Checked;

                //20250320
                this.GenbaEntity.CHIZU = this.form.CHIZU.Text;

                this.GenbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                this.GenbaEntity.EIGYOU_TANTOU_CD = this.form.EigyouCode.Text;
                this.GenbaEntity.GENBA_ADDRESS1 = this.form.GenbaAddress1.Text;
                this.GenbaEntity.GENBA_ADDRESS2 = this.form.GenbaAddress2.Text;
                this.GenbaEntity.GENBA_CD = this.form.GenbaCode.Text;
                this.GenbaEntity.GENBA_FAX = this.form.GenbaFax.Text;
                this.GenbaEntity.GENBA_FURIGANA = this.form.GenbaFurigana.Text;
                this.GenbaEntity.GENBA_KEISHOU1 = this.form.GenbaKeishou1.Text;
                this.GenbaEntity.GENBA_KEISHOU2 = this.form.GenbaKeishou2.Text;
                this.GenbaEntity.GENBA_KEITAI_TEL = this.form.GenbaKeitaiTel.Text;
                this.GenbaEntity.GENBA_NAME_RYAKU = this.form.GenbaNameRyaku.Text;
                this.GenbaEntity.GENBA_NAME1 = this.form.GenbaName1.Text;
                this.GenbaEntity.GENBA_NAME2 = this.form.GenbaName2.Text;
                this.GenbaEntity.GENBA_POST = this.form.GenbaPost.Text;
                this.GenbaEntity.GENBA_TEL = this.form.GenbaTel.Text;

                if (!string.IsNullOrWhiteSpace(this.form.GenbaTodoufukenCode.Text))
                {
                    this.GenbaEntity.GENBA_TODOUFUKEN_CD = Int16.Parse(this.form.GenbaTodoufukenCode.Text.ToString());
                }

                this.GenbaEntity.GYOUSHA_CD = this.form.GyoushaCode.Text;
                this.GenbaEntity.GYOUSHU_CD = this.form.GyoushuCode.Text;
                this.GenbaEntity.JISHA_KBN = this.form.JishaKbn.Checked;
                this.GenbaEntity.KOUFU_TANTOUSHA = this.form.KoufutantoushaCode.Text;

                this.form.KyotenCode.Text = "99";                                         //強制的に99:全社を登録
                if (!string.IsNullOrWhiteSpace(this.form.KyotenCode.Text))
                {
                    this.GenbaEntity.KYOTEN_CD = Int16.Parse(this.form.KyotenCode.Text);
                }

                if (string.IsNullOrEmpty(this.form.ItakuKeiyakuUseKbn.Text))
                {
                    this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN = 1;
                }
                else
                {
                    this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN = Int16.Parse(this.form.ItakuKeiyakuUseKbn.Text.ToString());
                }

                this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.ManiHensousakiAddress1.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.ManiHensousakiAddress2.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.ManiHensousakiBusho.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.ManiHensousakiKeishou1.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.ManiHensousakiKeishou2.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.ManiHensousakiName1.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.ManiHensousakiName2.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_POST = this.form.ManiHensousakiPost.Text;
                this.GenbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.ManiHensousakiTantou.Text;

                // 20141208 ブン 運搬報告書提出先を追加する start
                this.GenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                // 20141208 ブン 運搬報告書提出先を追加する end

                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked)
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                    this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.GenbaAddress2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.BushoCode.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.GenbaKeishou1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.GenbaKeishou2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.GenbaName1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.GenbaName2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_POST = this.form.GenbaPost.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.TantoushaCode.Text;
                }
                else
                {
                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 2;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.ManiHensousakiAddress1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.ManiHensousakiAddress2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.ManiHensousakiBusho.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.ManiHensousakiKeishou1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.ManiHensousakiKeishou2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.ManiHensousakiName1.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.ManiHensousakiName2.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_POST = this.form.ManiHensousakiPost.Text;
                    this.GenbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.ManiHensousakiTantou.Text;
                }

                #region A票 ～　E票

                this.GenbaEntity.MANI_HENSOUSAKI_KBN = this.form.ManiHensousakiKbn.Checked;
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text);
                this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = PlaceKbnPase(this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text);
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_AHyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_AHyo.Text) || this.sysinfoEntity.MANIFEST_USE_A == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = string.Empty;
                }
                else
                {
                    //返送先(A票)
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.ManiHensousakiTorihikisakiCode_AHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.ManiHensousakiGyoushaCode_AHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.form.ManiHensousakiGenbaCode_AHyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_A = 1;
                }
                //返送先(B1票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_B1Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_B1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B1 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.ManiHensousakiGyoushaCode_B1Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.ManiHensousakiGenbaCode_B1Hyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B1 = 1;
                }
                //返送先(B2票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_B2Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_B2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B2 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.ManiHensousakiGyoushaCode_B2Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.ManiHensousakiGenbaCode_B2Hyo.Text;
                    }

                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B2 = 1;
                }
                //返送先(B4票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_B4Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_B4Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B4 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.ManiHensousakiGyoushaCode_B4Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.ManiHensousakiGenbaCode_B4Hyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B4 = 1;
                }
                //返送先(B6票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_B6Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_B6Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B6 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.ManiHensousakiGyoushaCode_B6Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.ManiHensousakiGenbaCode_B6Hyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_B6 = 1;
                }
                //返送先(C1票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_C1Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_C1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C1 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.ManiHensousakiGyoushaCode_C1Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.ManiHensousakiGenbaCode_C1Hyo.Text;
                    }

                    this.GenbaEntity.MANI_HENSOUSAKI_USE_C1 = 1;
                }
                //返送先(C2票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_C2Hyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_C2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C2 == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.ManiHensousakiGyoushaCode_C2Hyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.ManiHensousakiGenbaCode_C2Hyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_C2 = 1;
                }
                //返送先(D票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_DHyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_DHyo.Text) || this.sysinfoEntity.MANIFEST_USE_D == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.ManiHensousakiTorihikisakiCode_DHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.ManiHensousakiGyoushaCode_DHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.form.ManiHensousakiGenbaCode_DHyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_D = 1;
                }
                //返送先(E票)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている start
                //if ("2".Equals(this.form.MANIFEST_USE_EHyo.Text))
                if ("2".Equals(this.form.MANIFEST_USE_EHyo.Text) || this.sysinfoEntity.MANIFEST_USE_E == 2)
                // 20140624 katen EV004672 返却日使用区分を使用しないにして現場を登録したとき、各票返送先のタブが消えるが、使用区分が使用するで登録されている end
                {
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                    this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = string.Empty;
                    this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = string.Empty;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked)
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.TorihikisakiCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.GyoushaCode.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.ManiHensousakiTorihikisakiCode_EHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.ManiHensousakiGyoushaCode_EHyo.Text;
                        this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.form.ManiHensousakiGenbaCode_EHyo.Text;
                    }
                    this.GenbaEntity.MANI_HENSOUSAKI_USE_E = 1;
                }

                #endregion

                if (!string.IsNullOrWhiteSpace(this.form.ManifestShuruiCode.Text))
                {
                    this.GenbaEntity.MANIFEST_SHURUI_CD = Int16.Parse(this.form.ManifestShuruiCode.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.ManifestTehaiCode.Text))
                {
                    this.GenbaEntity.MANIFEST_TEHAI_CD = Int16.Parse(this.form.ManifestTehaiCode.Text);
                }

                this.GenbaEntity.SAISHUU_SHOBUNJOU_KBN = this.form.SaishuuShobunjouKbn.Checked;
                this.GenbaEntity.SEARCH_TEKIYOU_BEGIN = "";
                this.GenbaEntity.SEARCH_TEKIYOU_END = "";
                this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.form.SeikyuuSoufuAddress1.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.form.SeikyuuSoufuAddress2.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.GenbaEntity.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // Begin: LANDUONG - 20220215 - refs#160054
                this.GenbaEntity.RAKURAKU_CUSTOMER_CD = this.form.RAKURAKU_CUSTOMER_CD.Text;
                // End: LANDUONG - 20220215 - refs#160054

                this.GenbaEntity.SEIKYUU_SOUFU_BUSHO = this.form.SeikyuuSoufuBusho.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_FAX = this.form.SoufuGenbaFax.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.form.SeikyuuSouhuKeishou1.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.form.SeikyuuSouhuKeishou2.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_NAME1 = this.form.SeikyuushoSoufusaki1.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_NAME2 = this.form.SeikyuushoSoufusaki2.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_POST = this.form.SeikyuuSoufuPost.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_TANTOU = this.form.SeikyuuSoufuTantou.Text;
                this.GenbaEntity.SEIKYUU_SOUFU_TEL = this.form.SoufuGenbaTel.Text;
                this.GenbaEntity.SEIKYUU_TANTOU = this.form.SeikyuuTantou.Text;
                if (this.form.SeikyuuDaihyouPrintKbn.Text.Length > 0)
                {
                    this.GenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = Int16.Parse(this.form.SeikyuuDaihyouPrintKbn.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.GenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    this.GenbaEntity.SEIKYUU_KYOTEN_CD = Int16.Parse(this.form.SEIKYUU_KYOTEN_CD.Text);
                }
                this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.form.ShiharaiSoufuAddress1.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.form.ShiharaiSoufuAddress2.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_BUSHO = this.form.ShiharaiSoufuBusho.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_FAX = this.form.ShiharaiGenbaFax.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.form.ShiharaiSoufuKeishou1.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.form.ShiharaiSoufuKeishou2.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_NAME1 = this.form.ShiharaiSoufuName1.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_NAME2 = this.form.ShiharaiSoufuName2.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_POST = this.form.ShiharaiSoufuPost.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_TANTOU = this.form.ShiharaiSoufuTantou.Text;
                this.GenbaEntity.SHIHARAI_SOUFU_TEL = this.form.ShiharaiGenbaTel.Text;
                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.GenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    this.GenbaEntity.SHIHARAI_KYOTEN_CD = Int16.Parse(this.form.SHIHARAI_KYOTEN_CD.Text);
                }
                this.GenbaEntity.SHOBUNSAKI_NO = this.form.ShobunsakiCode.Text;
                this.GenbaEntity.SHOKUCHI_KBN = this.form.ShokuchiKbn.Checked;
                this.GenbaEntity.SHUUKEI_ITEM_CD = this.form.ShuukeiItemCode.Text;
                this.GenbaEntity.TANTOUSHA = this.form.TantoushaCode.Text;

                if (this.form.TekiyouKikanForm.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanForm.Value.ToString(), out timeBegin);
                    this.GenbaEntity.TEKIYOU_BEGIN = timeBegin;
                }
                if (this.form.TekiyouKikanTo.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanTo.Value.ToString(), out timeEnd);
                    this.GenbaEntity.TEKIYOU_END = timeEnd;
                }

                if (this.form.TorihikisakiCode.Text != null)
                {
                    this.GenbaEntity.TORIHIKI_JOUKYOU = 1;
                }
                else
                {
                    this.GenbaEntity.TORIHIKI_JOUKYOU = 2;
                }
                this.GenbaEntity.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                this.GenbaEntity.TSUMIKAEHOKAN_KBN = this.form.TsumikaeHokanKbn.Checked;

                this.GenbaEntity.SHIKUCHOUSON_CD = this.form.SHIKUCHOUSON_CD.Text;

                // 20151020 BUNN #12040 STR
                // 排出事業場/荷積現場区分
                if (this.form.HaishutsuKbn.Checked)
                {
                    this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                }
                else
                {
                    this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = false;
                }
                // 処分事業場/荷降現場区分
                if (this.form.ShobunJigyoujouKbn.Checked)
                {
                    this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = true;
                }
                else
                {
                    this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = false;
                }
                // 20151020 BUNN #12040 STR

                // mapbox情報
                this.GenbaEntity.GENBA_LATITUDE = this.form.GenbaLatitude.Text;
                this.GenbaEntity.GENBA_LONGITUDE = this.form.GenbaLongitude.Text;
                if (string.IsNullOrEmpty(this.form.GenbaLatitude.Text) &&
                    string.IsNullOrEmpty(this.form.GenbaLongitude.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateName.Text) &&
                    string.IsNullOrEmpty(this.form.LocationInfoUpdateDate.Text))
                { 
                    // 未入力の状態から入力なし、という扱いなのでこの場合のみ更新なし
                }
                else
                {
                    this.GenbaEntity.GENBA_LOCATION_INFO_UPDATE_NAME = SystemProperty.UserName;
                    this.GenbaEntity.GENBA_LOCATION_INFO_UPDATE_DATE = this.sysDate();
                }

                if (AppConfig.AppOptions.IsSMS())
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ情報
                    this.GenbaEntity.SMS_USE = short.Parse(this.form.SMS_USE.Text);
                }

                // 更新者情報設定
                var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_GENBA>(this.GenbaEntity);
                dataBinderLogicGenba.SetSystemProperty(this.GenbaEntity, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.GenbaEntity);

                // 定期情報
                REGIST_GENBA_TEIKI_HINMEI[] hinTeiki = new REGIST_GENBA_TEIKI_HINMEI[this.form.TeikiHinmeiIchiran.RowCount - 1];

                for (int i = 0; i < hinTeiki.Length; i++)
                {
                    //hinTeiki[i] = new M_GENBA_TEIKI_HINMEI();
                    hinTeiki[i] = new REGIST_GENBA_TEIKI_HINMEI();
                    if (!(Boolean)this.form.TeikiHinmeiIchiran.Rows[i].Cells[0].Value)
                    {
                        hinTeiki[i].DELETE_FLG = false;
                    }
                    else
                    {
                        hinTeiki[i].DELETE_FLG = true;
                    }
                }
                foreach (DataRow row in ((DataTable)this.form.TeikiHinmeiIchiran.DataSource).Rows)
                {
                    if (row[Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_MONDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_MONDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_TUESDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_TUESDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_THURSDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_THURSDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_FRIDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_FRIDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_SATURDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_SATURDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_SUNDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_SUNDAY] = false;
                    }
                }

                var dataBinderLogicGenbaTeiki = new DataBinderLogic<REGIST_GENBA_TEIKI_HINMEI>(hinTeiki);
                var genbaTeikiEntityList = dataBinderLogicGenbaTeiki.CreateEntityForDataTable(this.form.TeikiHinmeiIchiran);
                this.genbaTeikiEntity = new List<M_GENBA_TEIKI_HINMEI>();
                Int16 rowNo = 0;
                foreach (var row in genbaTeikiEntityList)
                {
                    // 品名、単位の入力されていない行は対象外とする
                    if (row.HINMEI_CD == null || string.IsNullOrWhiteSpace(row.HINMEI_CD) || row.UNIT_CD.IsNull)
                    {
                        continue;
                    }

                    // 削除フラグにチェックがある行は対象外
                    if (row.DELETE_FLG)
                    {
                        continue;
                    }

                    rowNo++;
                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), row);
                    this.genbaTeikiEntity.Add(row);
                }

                // 定期回収情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TeikiHinmeiIchiran.Sort("HINMEI_CD");
                // QN Tue Anh #158986 START
                this.updateCourseEntity = new List<M_COURSE>();
                this.deleteCourseDetailEntity = new List<M_COURSE_DETAIL>();
                this.insertCourseDetailEntity = new List<M_COURSE_DETAIL>();
                this.deleteCourseDetailItemsEntity = new List<M_COURSE_DETAIL_ITEMS>();
                this.insertCourseDetailItemsEntity = new List<M_COURSE_DETAIL_ITEMS>();
                this.updateCourseDetailItemsEntity = new List<M_COURSE_DETAIL_ITEMS>();
                // QN Tue Anh #158986 END

                // 月極情報
                REGIST_GENBA_TSUKI_HINMEI[] hinTsuki = new REGIST_GENBA_TSUKI_HINMEI[this.form.TsukiHinmeiIchiran.RowCount - 1];

                for (int i = 0; i < hinTsuki.Length; i++)
                {
                    hinTsuki[i] = new REGIST_GENBA_TSUKI_HINMEI();

                    //画面状態　削除フラグ取得
                    if (!(Boolean)this.form.TsukiHinmeiIchiran.Rows[i].Cells[0].Value)
                    {
                        hinTsuki[i].DELETE_FLG = false;
                    }
                    else
                    {
                        hinTsuki[i].DELETE_FLG = true;
                    }
                }
                foreach (DataRow row in ((DataTable)this.form.TsukiHinmeiIchiran.DataSource).Rows)
                {
                    if (row[Const.GenbaHoshuConstans.TSUKI_CHOUKA_SETTING] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TSUKI_CHOUKA_SETTING] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] = false;
                    }
                }

                var dataBinderLogicGenbaTsuki = new DataBinderLogic<REGIST_GENBA_TSUKI_HINMEI>(hinTsuki);
                var genbaTsukiEntityList = dataBinderLogicGenbaTsuki.CreateEntityForDataTable(this.form.TsukiHinmeiIchiran);
                this.genbaTsukiEntity = new List<M_GENBA_TSUKI_HINMEI>();
                rowNo = 0;
                foreach (var row in genbaTsukiEntityList)
                {
                    // 品名、単位の入力されていない行は対象外とする
                    if (row.HINMEI_CD == null || string.IsNullOrWhiteSpace(row.HINMEI_CD) || row.UNIT_CD.IsNull)
                    {
                        continue;
                    }

                    // 削除フラグにチェックがある行は対象外
                    if (row.DELETE_FLG)
                    {
                        continue;
                    }

                    rowNo++;
                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), row);
                    this.genbaTsukiEntity.Add(row);
                }

                // 月極情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TsukiHinmeiIchiran.Sort("HINMEI_CD");

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #region 仮現場データ作成

        /// <summary>
        /// コントロールから対象の仮Entityを作成する
        /// </summary>
        public bool CreateKariEntity()
        {
            try
            {
                DateTime timeBegin = new DateTime();
                DateTime timeEnd = new DateTime();

                var kariGenba = new M_KARI_GENBA();

                kariGenba.BIKOU1 = this.form.Bikou1.Text;
                kariGenba.BIKOU2 = this.form.Bikou2.Text;
                kariGenba.BIKOU3 = this.form.Bikou3.Text;
                kariGenba.BIKOU4 = this.form.Bikou4.Text;
                kariGenba.BUSHO = this.form.BushoCode.Text;
                kariGenba.CHIIKI_CD = this.form.ChiikiCode.Text;
                kariGenba.CHUUSHI_RIYUU1 = this.form.ChuusiRiyuu1.Text;
                kariGenba.CHUUSHI_RIYUU2 = this.form.ChuusiRiyuu2.Text;
                //kariGenba.DEN_MANI_SHOUKAI_KBN = this.form.DenManiShoukaiKbn.Checked;
                kariGenba.KENSHU_YOUHI = this.form.KENSHU_YOUHI.Checked;

                //20250320
                kariGenba.CHIZU = this.form.CHIZU.Text;
                kariGenba.EIGYOU_TANTOU_BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                kariGenba.EIGYOU_TANTOU_CD = this.form.EigyouCode.Text;
                kariGenba.GENBA_ADDRESS1 = this.form.GenbaAddress1.Text;
                kariGenba.GENBA_ADDRESS2 = this.form.GenbaAddress2.Text;
                kariGenba.GENBA_CD = this.form.GenbaCode.Text;
                kariGenba.GENBA_FAX = this.form.GenbaFax.Text;
                kariGenba.GENBA_FURIGANA = this.form.GenbaFurigana.Text;
                kariGenba.GENBA_KEISHOU1 = this.form.GenbaKeishou1.Text;
                kariGenba.GENBA_KEISHOU2 = this.form.GenbaKeishou2.Text;
                kariGenba.GENBA_KEITAI_TEL = this.form.GenbaKeitaiTel.Text;
                kariGenba.GENBA_NAME_RYAKU = this.form.GenbaNameRyaku.Text;
                kariGenba.GENBA_NAME1 = this.form.GenbaName1.Text;
                kariGenba.GENBA_NAME2 = this.form.GenbaName2.Text;
                kariGenba.GENBA_POST = this.form.GenbaPost.Text;
                kariGenba.GENBA_TEL = this.form.GenbaTel.Text;

                if (!string.IsNullOrWhiteSpace(this.form.GenbaTodoufukenCode.Text))
                {
                    kariGenba.GENBA_TODOUFUKEN_CD = Int16.Parse(this.form.GenbaTodoufukenCode.Text.ToString());
                }

                kariGenba.GYOUSHA_CD = this.form.GyoushaCode.Text;
                kariGenba.GYOUSHU_CD = this.form.GyoushuCode.Text;
                kariGenba.HAISHUTSU_NIZUMI_GENBA_KBN = this.form.HaishutsuKbn.Checked;
                kariGenba.JISHA_KBN = this.form.JishaKbn.Checked;
                kariGenba.KOUFU_TANTOUSHA = this.form.KoufutantoushaCode.Text;

                if (!string.IsNullOrWhiteSpace(this.form.KyotenCode.Text))
                {
                    kariGenba.KYOTEN_CD = Int16.Parse(this.form.KyotenCode.Text);
                }

                if (string.IsNullOrEmpty(this.form.ItakuKeiyakuUseKbn.Text))
                {
                    kariGenba.ITAKU_KEIYAKU_USE_KBN = 1;
                }
                else
                {
                    kariGenba.ITAKU_KEIYAKU_USE_KBN = Int16.Parse(this.form.ItakuKeiyakuUseKbn.Text.ToString());
                }

                #region A票 ～　E票

                kariGenba.MANI_HENSOUSAKI_KBN = this.form.ManiHensousakiKbn.Checked;
                if ("2".Equals(this.form.MANIFEST_USE_AHyo.Text) || this.sysinfoEntity.MANIFEST_USE_A == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_A = 2;
                }
                else
                {
                    //返送先(A票)
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_A = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_AHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.ManiHensousakiTorihikisakiCode_AHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_AHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.ManiHensousakiGyoushaCode_AHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_AHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_A = this.form.ManiHensousakiGenbaCode_AHyo.Text;
                        }
                    }
                    kariGenba.MANI_HENSOUSAKI_USE_A = 1;
                }
                //返送先(B1票)
                if ("2".Equals(this.form.MANIFEST_USE_B1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B1 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_B1 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_B1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.ManiHensousakiGyoushaCode_B1Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_B1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.ManiHensousakiGenbaCode_B1Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_B1 = 1;
                }
                //返送先(B2票)
                if ("2".Equals(this.form.MANIFEST_USE_B2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B2 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_B2 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_B2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.ManiHensousakiGyoushaCode_B2Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_B2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.ManiHensousakiGenbaCode_B2Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_B2 = 1;
                }
                //返送先(B4票)
                if ("2".Equals(this.form.MANIFEST_USE_B4Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B4 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_B4 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_B4Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.ManiHensousakiGyoushaCode_B4Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_B4Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.ManiHensousakiGenbaCode_B4Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_B4 = 1;
                }
                //返送先(B6票)
                if ("2".Equals(this.form.MANIFEST_USE_B6Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B6 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_B6 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_B6Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.ManiHensousakiGyoushaCode_B6Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_B6Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.ManiHensousakiGenbaCode_B6Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_B6 = 1;
                }
                //返送先(C1票)
                if ("2".Equals(this.form.MANIFEST_USE_C1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C1 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_C1 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_C1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.ManiHensousakiGyoushaCode_C1Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_C1Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.ManiHensousakiGenbaCode_C1Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_C1 = 1;
                }
                //返送先(C2票)
                if ("2".Equals(this.form.MANIFEST_USE_C2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C2 == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_C2 = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_C2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.ManiHensousakiGyoushaCode_C2Hyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_C2Hyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.ManiHensousakiGenbaCode_C2Hyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_C2 = 1;
                }
                //返送先(D票)
                if ("2".Equals(this.form.MANIFEST_USE_DHyo.Text) || this.sysinfoEntity.MANIFEST_USE_D == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_D = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_D = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_DHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.ManiHensousakiTorihikisakiCode_DHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_DHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.ManiHensousakiGyoushaCode_DHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_DHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_D = this.form.ManiHensousakiGenbaCode_DHyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_D = 1;
                }
                //返送先(E票)
                if ("2".Equals(this.form.MANIFEST_USE_EHyo.Text) || this.sysinfoEntity.MANIFEST_USE_E == 2)
                {
                    kariGenba.MANI_HENSOUSAKI_USE_E = 2;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked)
                    {
                        kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.TorihikisakiCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.GyoushaCode.Text;
                        kariGenba.MANI_HENSOUSAKI_GENBA_CD_E = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiTorihikisakiCode_EHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.ManiHensousakiTorihikisakiCode_EHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGyoushaCode_EHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.ManiHensousakiGyoushaCode_EHyo.Text;
                        }
                        if (!string.IsNullOrEmpty(this.form.ManiHensousakiGenbaCode_EHyo.Text))
                        {
                            kariGenba.MANI_HENSOUSAKI_GENBA_CD_E = this.form.ManiHensousakiGenbaCode_EHyo.Text;
                        }
                    }

                    kariGenba.MANI_HENSOUSAKI_USE_E = 1;
                }

                #endregion

                if (!string.IsNullOrWhiteSpace(this.form.ManifestShuruiCode.Text))
                {
                    kariGenba.MANIFEST_SHURUI_CD = Int16.Parse(this.form.ManifestShuruiCode.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.ManifestTehaiCode.Text))
                {
                    kariGenba.MANIFEST_TEHAI_CD = Int16.Parse(this.form.ManifestTehaiCode.Text);
                }

                kariGenba.SAISHUU_SHOBUNJOU_KBN = this.form.SaishuuShobunjouKbn.Checked;
                kariGenba.SEARCH_TEKIYOU_BEGIN = "";
                kariGenba.SEARCH_TEKIYOU_END = "";
                kariGenba.SEIKYUU_SOUFU_ADDRESS1 = this.form.SeikyuuSoufuAddress1.Text;
                kariGenba.SEIKYUU_SOUFU_ADDRESS2 = this.form.SeikyuuSoufuAddress2.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                kariGenba.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                kariGenba.SEIKYUU_SOUFU_BUSHO = this.form.SeikyuuSoufuBusho.Text;
                kariGenba.SEIKYUU_SOUFU_FAX = this.form.SoufuGenbaFax.Text;
                kariGenba.SEIKYUU_SOUFU_KEISHOU1 = this.form.SeikyuuSouhuKeishou1.Text;
                kariGenba.SEIKYUU_SOUFU_KEISHOU2 = this.form.SeikyuuSouhuKeishou2.Text;
                kariGenba.SEIKYUU_SOUFU_NAME1 = this.form.SeikyuushoSoufusaki1.Text;
                kariGenba.SEIKYUU_SOUFU_NAME2 = this.form.SeikyuushoSoufusaki2.Text;
                kariGenba.SEIKYUU_SOUFU_POST = this.form.SeikyuuSoufuPost.Text;
                kariGenba.SEIKYUU_SOUFU_TANTOU = this.form.SeikyuuSoufuTantou.Text;
                kariGenba.SEIKYUU_SOUFU_TEL = this.form.SoufuGenbaTel.Text;
                kariGenba.SEIKYUU_TANTOU = this.form.SeikyuuTantou.Text;
                if (this.form.SeikyuuDaihyouPrintKbn.Text.Length > 0)
                {
                    kariGenba.SEIKYUU_DAIHYOU_PRINT_KBN = Int16.Parse(this.form.SeikyuuDaihyouPrintKbn.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    kariGenba.SEIKYUU_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    kariGenba.SEIKYUU_KYOTEN_CD = Int16.Parse(this.form.SEIKYUU_KYOTEN_CD.Text);
                }
                kariGenba.SHIHARAI_SOUFU_ADDRESS1 = this.form.ShiharaiSoufuAddress1.Text;
                kariGenba.SHIHARAI_SOUFU_ADDRESS2 = this.form.ShiharaiSoufuAddress2.Text;
                kariGenba.SHIHARAI_SOUFU_BUSHO = this.form.ShiharaiSoufuBusho.Text;
                kariGenba.SHIHARAI_SOUFU_FAX = this.form.ShiharaiGenbaFax.Text;
                kariGenba.SHIHARAI_SOUFU_KEISHOU1 = this.form.ShiharaiSoufuKeishou1.Text;
                kariGenba.SHIHARAI_SOUFU_KEISHOU2 = this.form.ShiharaiSoufuKeishou2.Text;
                kariGenba.SHIHARAI_SOUFU_NAME1 = this.form.ShiharaiSoufuName1.Text;
                kariGenba.SHIHARAI_SOUFU_NAME2 = this.form.ShiharaiSoufuName2.Text;
                kariGenba.SHIHARAI_SOUFU_POST = this.form.ShiharaiSoufuPost.Text;
                kariGenba.SHIHARAI_SOUFU_TANTOU = this.form.ShiharaiSoufuTantou.Text;
                kariGenba.SHIHARAI_SOUFU_TEL = this.form.ShiharaiGenbaTel.Text;
                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    kariGenba.SHIHARAI_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    kariGenba.SHIHARAI_KYOTEN_CD = Int16.Parse(this.form.SHIHARAI_KYOTEN_CD.Text);
                }
                kariGenba.SHOBUN_NIOROSHI_GENBA_KBN = this.form.ShobunJigyoujouKbn.Checked;
                kariGenba.SHOBUNSAKI_NO = this.form.ShobunsakiCode.Text;
                kariGenba.SHOKUCHI_KBN = this.form.ShokuchiKbn.Checked;
                kariGenba.SHUUKEI_ITEM_CD = this.form.ShuukeiItemCode.Text;
                kariGenba.TANTOUSHA = this.form.TantoushaCode.Text;

                if (this.form.TekiyouKikanForm.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanForm.Value.ToString(), out timeBegin);
                    kariGenba.TEKIYOU_BEGIN = timeBegin;
                }
                if (this.form.TekiyouKikanTo.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanTo.Value.ToString(), out timeEnd);
                    kariGenba.TEKIYOU_END = timeEnd;
                }

                if (this.form.TorihikisakiCode.Text != null)
                {
                    kariGenba.TORIHIKI_JOUKYOU = 1;
                }
                else
                {
                    kariGenba.TORIHIKI_JOUKYOU = 2;
                }
                kariGenba.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                kariGenba.TSUMIKAEHOKAN_KBN = this.form.TsumikaeHokanKbn.Checked;

                kariGenba.SHIKUCHOUSON_CD = this.form.SHIKUCHOUSON_CD.Text;

                // 更新者情報設定
                var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KARI_GENBA>(kariGenba);
                dataBinderLogicGenba.SetSystemProperty(kariGenba, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kariGenba);
                // 既存現場の情報を引き継ぐ
                kariGenba.CREATE_DATE = this.GenbaEntity.CREATE_DATE;
                kariGenba.CREATE_PC = this.GenbaEntity.CREATE_PC;
                kariGenba.CREATE_USER = this.GenbaEntity.CREATE_USER;

                // 定期情報
                REGIST_KARI_GENBA_TEIKI_HINMEI[] hinTeiki = new REGIST_KARI_GENBA_TEIKI_HINMEI[this.form.TeikiHinmeiIchiran.RowCount - 1];
                for (int i = 0; i < hinTeiki.Length; i++)
                {
                    hinTeiki[i] = new REGIST_KARI_GENBA_TEIKI_HINMEI();
                    if (!(Boolean)this.form.TeikiHinmeiIchiran.Rows[i].Cells[0].Value)
                    {
                        hinTeiki[i].DELETE_FLG = false;
                    }
                    else
                    {
                        hinTeiki[i].DELETE_FLG = true;
                    }
                }
                foreach (DataRow row in ((DataTable)this.form.TeikiHinmeiIchiran.DataSource).Rows)
                {
                    if (row[Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_MONDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_MONDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_TUESDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_TUESDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_THURSDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_THURSDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_FRIDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_FRIDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_SATURDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_SATURDAY] = false;
                    }
                    if (row[Const.GenbaHoshuConstans.TEIKI_SUNDAY] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TEIKI_SUNDAY] = false;
                    }
                }

                // 定期回収情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TeikiHinmeiIchiran.Sort("HINMEI_CD");

                var dataBinderLogicGenbaTeiki = new DataBinderLogic<REGIST_KARI_GENBA_TEIKI_HINMEI>(hinTeiki);
                var genbaTeikiEntityList = dataBinderLogicGenbaTeiki.CreateEntityForDataTable(this.form.TeikiHinmeiIchiran);
                var genbaTeikiEntity = new List<M_KARI_GENBA_TEIKI_HINMEI>();
                Int16 rowNo = 0;
                foreach (var row in genbaTeikiEntityList)
                {
                    // 品名、単位の入力されていない行は対象外とする
                    if (row.HINMEI_CD == null || string.IsNullOrWhiteSpace(row.HINMEI_CD) || row.UNIT_CD.IsNull)
                    {
                        continue;
                    }

                    // 削除フラグにチェックがある行は対象外
                    if (row.DELETE_FLG)
                    {
                        continue;
                    }

                    rowNo++;
                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), row);
                    genbaTeikiEntity.Add(row);
                }

                // 月極情報
                REGIST_KARI_GENBA_TSUKI_HINMEI[] hinTsuki = new REGIST_KARI_GENBA_TSUKI_HINMEI[this.form.TsukiHinmeiIchiran.RowCount - 1];
                for (int i = 0; i < hinTsuki.Length; i++)
                {
                    hinTsuki[i] = new REGIST_KARI_GENBA_TSUKI_HINMEI();

                    //画面状態　削除フラグ取得
                    if (!(Boolean)this.form.TsukiHinmeiIchiran.Rows[i].Cells[0].Value)
                    {
                        hinTsuki[i].DELETE_FLG = false;
                    }
                    else
                    {
                        hinTsuki[i].DELETE_FLG = true;
                    }
                }
                foreach (DataRow row in ((DataTable)this.form.TsukiHinmeiIchiran.DataSource).Rows)
                {
                    if (row[Const.GenbaHoshuConstans.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] == DBNull.Value)
                    {
                        row[Const.GenbaHoshuConstans.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] = false;
                    }
                }

                // 月極情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TsukiHinmeiIchiran.Sort("HINMEI_CD");

                var dataBinderLogicGenbaTsuki = new DataBinderLogic<REGIST_KARI_GENBA_TSUKI_HINMEI>(hinTsuki);
                var genbaTsukiEntityList = dataBinderLogicGenbaTsuki.CreateEntityForDataTable(this.form.TsukiHinmeiIchiran);
                var genbaTsukiEntity = new List<M_KARI_GENBA_TSUKI_HINMEI>();
                rowNo = 0;
                foreach (var row in genbaTsukiEntityList)
                {
                    // 品名、単位の入力されていない行は対象外とする
                    if (row.HINMEI_CD == null || string.IsNullOrWhiteSpace(row.HINMEI_CD) || row.UNIT_CD.IsNull)
                    {
                        continue;
                    }

                    // 削除フラグにチェックがある行は対象外
                    if (row.DELETE_FLG)
                    {
                        continue;
                    }

                    rowNo++;
                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), row);
                    genbaTsukiEntity.Add(row);
                }

                this.kariGenbaEntity = kariGenba;
                this.kariGenbaTeikiEntity = genbaTeikiEntity;
                this.kariGenbaTsukiEntity = genbaTsukiEntity;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateKariEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                // 現場マスタ登録
                this.GenbaEntity.DELETE_FLG = false;
                this.daoGenba.Insert(this.GenbaEntity);

                // 現場_定期情報マスタ登録
                foreach (M_GENBA_TEIKI_HINMEI genbaTeiki in this.genbaTeikiEntity)
                {
                    this.daoGenbaTeiki.Insert(genbaTeiki);
                }

                // 現場_月極情報マスタ登録
                foreach (M_GENBA_TSUKI_HINMEI genbaTsuki in this.genbaTsukiEntity)
                {
                    this.daoGenbaTsuki.Insert(genbaTsuki);
                }

                if(this.GenbaEntity.SMS_USE == 1)
                {
                    this.SmsReceicerRegist();
                }

                this.isRegist = true;
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (errorFlag)
                {
                    return;
                }

                // 現場マスタ更新
                this.GenbaEntity.DELETE_FLG = false;
                this.daoGenba.Update(this.GenbaEntity);

                // QN Tue Anh #158986 START
                if (this.teikiHinmeiUpdate)
                {
                    this.CheckDeleteTeikiHinmei();
                    foreach (M_COURSE_DETAIL courseDetailEntity in this.deleteCourseDetailEntity)
                    {
                        this.courseDetailDao.Delete(courseDetailEntity);
                    }
                    foreach (M_COURSE_DETAIL courseDetailEntity in this.insertCourseDetailEntity)
                    {
                        this.courseDetailDao.Insert(courseDetailEntity);
                    }
                    foreach (M_COURSE_DETAIL_ITEMS courseDetailItemEntity in this.deleteCourseDetailItemsEntity)
                    {
                        this.courseDetailItemsDao.Delete(courseDetailItemEntity);
                    }
                    foreach (M_COURSE_DETAIL_ITEMS courseDetailItemEntity in this.insertCourseDetailItemsEntity)
                    {
                        this.courseDetailItemsDao.Insert(courseDetailItemEntity);
                    }

                    this.CheckUpdateTeikiHinmei();
                    foreach (M_COURSE_DETAIL_ITEMS courseDetailItemEntity in this.updateCourseDetailItemsEntity)
                    {
                        this.courseDetailItemsDao.Update(courseDetailItemEntity);
                    }
                    foreach (M_COURSE courseEntity in this.updateCourseEntity)
                    {
                        this.courseDao.Update(courseEntity);
                    }
                }
                // QN Tue Anh #158986 END

                // 現場_定期情報マスタ登録
                M_GENBA_TEIKI_HINMEI condTeiki = new M_GENBA_TEIKI_HINMEI();
                condTeiki.GYOUSHA_CD = this.GenbaEntity.GYOUSHA_CD;
                condTeiki.GENBA_CD = this.GenbaEntity.GENBA_CD;
                this.daoGenbaTeiki.GetDataBySqlFile(this.DELETE_TEIKI_HINMEI_SQL, condTeiki);
                foreach (M_GENBA_TEIKI_HINMEI genbaTeiki in this.genbaTeikiEntity)
                {
                    this.daoGenbaTeiki.Insert(genbaTeiki);
                }

                // 現場_月極情報マスタ登録
                M_GENBA_TSUKI_HINMEI condTsuki = new M_GENBA_TSUKI_HINMEI();
                condTsuki.GYOUSHA_CD = this.GenbaEntity.GYOUSHA_CD;
                condTsuki.GENBA_CD = this.GenbaEntity.GENBA_CD;
                this.daoGenbaTsuki.GetDataBySqlFile(this.DELETE_TSUKI_HINMEI_SQL, condTsuki);
                foreach (M_GENBA_TSUKI_HINMEI genbaTsuki in this.genbaTsukiEntity)
                {
                    this.daoGenbaTsuki.Insert(genbaTsuki);
                }

                if(this.GenbaEntity.SMS_USE == 1)
                {
                    this.SmsReceicerRegist();
                }

                this.isRegist = true;
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.GenbaEntity.DELETE_FLG = true;

                this.daoGenba.Update(this.GenbaEntity);

                this.isRegist = true;
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #region 電子申請

        /// <summary>
        /// 電子申請処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <param name="dsEntry"></param>
        /// <param name="dsDetailList"></param>
        [Transaction]
        public virtual void Shinsei(bool errorFlag, r_framework.Entity.T_DENSHI_SHINSEI_ENTRY dsEntry
                , List<r_framework.Entity.T_DENSHI_SHINSEI_DETAIL> dsDetailList)
        {
            try
            {
                if (errorFlag)
                {
                    return;
                }
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    // 仮現場マスタ更新
                    this.kariGenbaEntity.DELETE_FLG = false;
                    this.daoKariGenba.Insert(this.kariGenbaEntity);

                    // 仮現場_定期情報マスタ登録
                    foreach (M_KARI_GENBA_TEIKI_HINMEI kariGenbaTeiki in this.kariGenbaTeikiEntity)
                    {
                        this.daoKariGenbaTeiki.Insert(kariGenbaTeiki);
                    }

                    // 仮現場_月極情報マスタ登録
                    foreach (M_KARI_GENBA_TSUKI_HINMEI kariGenbaTsuki in this.kariGenbaTsukiEntity)
                    {
                        this.daoKariGenbaTsuki.Insert(kariGenbaTsuki);
                    }

                    // 申請入力登録
                    DenshiShinseiDBAccessor dsDBAccessor = new DenshiShinseiDBAccessor();

                    // 電子申請入力の登録
                    dsDBAccessor.InsertDSEntry(dsEntry, DenshiShinseiUtility.SHINSEI_MASTER_KBN.GENBA);

                    // 電子申請明細の登録
                    foreach (r_framework.Entity.T_DENSHI_SHINSEI_DETAIL dsDetail in dsDetailList)
                    {
                        dsDBAccessor.InsertDSDetail(dsDetail, dsEntry.SYSTEM_ID, dsEntry.SEQ, dsEntry.SHINSEI_NUMBER);
                    }

                    // 電子申請状態の登録
                    dsDBAccessor.InsertDSStatus(new r_framework.Entity.T_DENSHI_SHINSEI_STATUS(), dsEntry.SYSTEM_ID, dsEntry.SEQ);

                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "申請");
                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                LogUtility.Error("Shinsei", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                LogUtility.Error("Shinsei", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                LogUtility.Error("Shinsei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region 電子申請データﾁｪｯｸ

        /// <summary>
        /// 電子申請データチェック
        /// </summary>
        /// <returns>true:申請OK、false:申請NG</returns>
        internal bool CheckDenshiShinseiData(out bool catchErr)
        {
            try
            {
                catchErr = false;
                var dsUtility = new DenshiShinseiUtility();
                return dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.GENBA, null, this.form.GyoushaCode.Text, this.form.GenbaCode.Text);
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("CheckDenshiShinseiData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckDenshiShinseiData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        #endregion

        #region 電子申請画面用初期化DTO作成

        /// <summary>
        /// 電子申請画面用初期化DTO作成
        /// </summary>
        /// <returns></returns>
        internal DenshiShinseiNyuuryokuInitDTO CreateDenshiShinseiInitDto(out bool catchErr)
        {
            try
            {
                var returnVal = new DenshiShinseiNyuuryokuInitDTO();
                catchErr = false;
                returnVal.TorihikisakiCd = this.form.TorihikisakiCode.Text;
                returnVal.TorihikisakiNameRyaku = this.torihikisakiEntity == null ? string.Empty : this.torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;

                returnVal.GyoushaCd = this.form.GyoushaCode.Text;
                returnVal.GyoushaNameRyaku = this.gyoushaEntity == null ? string.Empty : this.gyoushaEntity.GYOUSHA_NAME_RYAKU;

                returnVal.GenbaCd = this.form.GenbaCode.Text;
                returnVal.GenbaNameRyaku = this.form.GenbaNameRyaku.Text;

                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CreateDenshiShinseiInitDto", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return new DenshiShinseiNyuuryokuInitDTO();
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        /// <returns></returns>
        public bool CheckDeleteForLogicalDel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var gyoushaCd = this.form.GyoushaCode.Text;
                var genbaCd = this.form.GenbaCode.Text;

                if (!string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                {
                    DataTable dtTable = DaoInitUtility.GetComponent<GenbaHoshu.Dao.IDenshiShinseiEntryDao>().GetDataBySqlFileCheck(gyoushaCd, genbaCd);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        new MessageBoxShowLogic().MessageBoxShow("E258", "現場", "現場CD", strName);

                        ret = false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDeleteForLogicalDel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            GenbaHoshuLogic localLogic = other as GenbaHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public GenbaHoshuConstans.GenbaCdLeaveResult DupliCheckGenbaCd(string zeroPadCd, bool isRegister)
        {
            LogUtility.DebugMethodStart(zeroPadCd, isRegister);

            // 現場マスタ検索
            M_GENBA genbaSearchString = new M_GENBA();
            genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
            genbaSearchString.GENBA_CD = zeroPadCd;
            M_GENBA genbaEntity = this.daoGenba.GetDataByCd(genbaSearchString);

            // 重複チェック
            GenbaHoshuValidator vali = new GenbaHoshuValidator();
            DialogResult resultDialog = new DialogResult();
            bool resultDupli = vali.GenbaCDValidator(genbaEntity, isRegister, out resultDialog);

            GenbaHoshuConstans.GenbaCdLeaveResult ViewUpdateWindow = 0;

            // 重複チェックの結果と、ポップアップの結果で動作を変える
            if (!resultDupli && resultDialog == DialogResult.OK)
            {
                ViewUpdateWindow = GenbaHoshuConstans.GenbaCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli && resultDialog == DialogResult.Yes)
            {
                ViewUpdateWindow = GenbaHoshuConstans.GenbaCdLeaveResult.FALSE_ON;
            }
            else if (!resultDupli && resultDialog == DialogResult.No)
            {
                ViewUpdateWindow = GenbaHoshuConstans.GenbaCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli)
            {
                ViewUpdateWindow = GenbaHoshuConstans.GenbaCdLeaveResult.FALSE_NONE;
            }
            else
            {
                ViewUpdateWindow = GenbaHoshuConstans.GenbaCdLeaveResult.TURE_NONE;
            }

            LogUtility.DebugMethodEnd();

            return ViewUpdateWindow;
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.GenbaCode.GetType().GetProperty(GenbaHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaCode, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// ゼロパディング(取引先)処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPaddingTorisaki(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.TorihikisakiCode.GetType().GetProperty(GenbaHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaCode, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// ゼロパディング処理(県用)
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding_Ken(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.GenbaTodoufukenCode.GetType().GetProperty(GenbaHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaTodoufukenCode, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool catchErr = false;
                // イベント制御(削除)
                this.RemoveDynamicEvent();

                // エラー表示をクリアする
                foreach (var ctrl in this.allControl)
                {
                    if (ctrl is CustomTextBox && ((CustomTextBox)ctrl).BackColor == Constans.ERROR_COLOR)
                    {
                        ((CustomTextBox)ctrl).BackColor = Constans.NOMAL_COLOR;
                    }
                }

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規モードの場合は空画面を表示する
                    catchErr = this.WindowInitNewMode(parentForm);
                }
                else
                {
                    // 入金先入力画面表示時の入金先CDで再検索・再表示
                    this.SetWindowData();

                    // 取消後のフォーカス位置設定
                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        this.form.GyoushuCode.Focus();
                    }
                    else
                    {
                        this.form.GyoushaName1.Focus();
                    }
                }

                // イベント制御(追加)
                this.SetDynamicEvent();

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("Cancel", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// データ取得処理(現場)
        /// </summary>
        /// <returns></returns>
        public int SearchGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_GENBA genbaSearchString = new M_GENBA();
                genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.ZeroPadding(this.GenbaCd);
                genbaSearchString.GENBA_CD = zeroPadCd;

                this.GenbaEntity = null;

                this.GenbaEntity = daoGenba.GetDataByCd(genbaSearchString);

                int count = this.GenbaEntity == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 引合現場エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchHikiaiGenba()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();
            M_HIKIAI_GENBA entry = new M_HIKIAI_GENBA() { GYOUSHA_CD = this.GyoushaCd, GENBA_CD = this.GenbaCd, DELETE_FLG = false };
            entry.HIKIAI_GYOUSHA_USE_FLG = this.form.ShinseiHikiaiGyoushaUseFlg;

            var entityList = dao.GetHikiaiGenbaList(entry);
            if (0 != entityList.Count())
            {
                this.LoadHikiaiGenbaEntity = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 仮現場エンティティを取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        private int SearchKariGenba()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_GENBADao>();
            var entityList = dao.GetKariGenbaList(new M_KARI_GENBA() { GYOUSHA_CD = this.GyoushaCd, GENBA_CD = this.GenbaCd, DELETE_FLG = false });
            if (0 != entityList.Count())
            {
                this.LoadKariGenbaEntity = entityList.FirstOrDefault();
                ret = 1;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// データ取得処理(業者変更時の後処理)
        /// </summary>
        /// <param name="isInputCheck"></param>
        public bool SearchchkGyousha(bool isInputCheck, bool setGyoushaOnly)
        {
            try
            {
                LogUtility.DebugMethodStart(isInputCheck, setGyoushaOnly);

                this.gyoushaEntity = null;
                M_GYOUSHA queryParam = new M_GYOUSHA();
                queryParam.GYOUSHA_CD = this.form.GyoushaCode.Text;

                _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                // 入力チェックフラグがtrueの場合は有効なデータを取得する、falseの場合は削除・期間外も対象とする
                if (isInputCheck)
                {
                    M_GYOUSHA[] result = this.daoGyousha.GetAllValidData(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        this.gyoushaEntity = result[0];
                    }
                }
                else
                {
                    this.gyoushaEntity = this.daoGyousha.GetDataByCd(queryParam.GYOUSHA_CD);
                }

                //チェックBOXへのセットを行う
                if (this.gyoushaEntity != null)
                {
                    this.form.GyoushaKbnUkeire.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_UKEIRE;
                    this.form.GyoushaKbnShukka.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_SHUKKA;
                    this.form.GyoushaKbnMani.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_MANI;

                    if (!this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                    {
                        this.FlgGyoushaHaishutuKbn = (bool)this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                    }
                    else
                    {
                        this.FlgGyoushaHaishutuKbn = false;
                    }

                    this.form.GyoushaName1.Text = this.gyoushaEntity.GYOUSHA_NAME1;
                    this.form.GyoushaName2.Text = this.gyoushaEntity.GYOUSHA_NAME2;

                    if (this.gyoushaEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.GYOUSHA_TEKIYOU_BEGIN.Value = this.gyoushaEntity.TEKIYOU_BEGIN.Value;
                    }

                    if (this.gyoushaEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.GYOUSHA_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.GYOUSHA_TEKIYOU_END.Value = this.gyoushaEntity.TEKIYOU_END.Value;
                    }

                    this.form.JishaKbn.Enabled = (bool)this.gyoushaEntity.JISHA_KBN;
                    if (!this.gyoushaEntity.JISHA_KBN)
                    {
                        this.form.JishaKbn.Checked = (bool)this.gyoushaEntity.JISHA_KBN;
                    }

                    this.form.HaishutsuKbn.Enabled = this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue;
                    if (!this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN)
                    {
                        this.form.HaishutsuKbn.Checked = (bool)this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                    }

                    this.form.TsumikaeHokanKbn.Enabled = this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue;
                    if (!this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN)
                    {
                        this.form.TsumikaeHokanKbn.Enabled = (bool)this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                        this.form.TsumikaeHokanKbn.Checked = (bool)this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                    }

                    this.form.ShobunJigyoujouKbn.Enabled = this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                    this.form.SaishuuShobunjouKbn.Enabled = this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                    if (!this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN)
                    {
                        this.form.SaishuuShobunjouKbn.Enabled = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                        this.form.ShobunJigyoujouKbn.Checked = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                        this.form.SaishuuShobunjouKbn.Checked = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                        this.form.ShobunsakiCode.Enabled = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                    }

                    //this.form.NioroshiGenbaKbn.Enabled = (bool)this.gyoushaEntity.NIOROSHI_GHOUSHA_KBN;
                    if ((!this.gyoushaEntity.JISHA_KBN && !this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN && !this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN && !this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN && !this.gyoushaEntity.MANI_HENSOUSAKI_KBN)
                        || !this.gyoushaEntity.GYOUSHAKBN_MANI)
                    {
                        this.ChangeManifestsDisable();
                    }

                    //マニ記載業者チェックがついていないときは非活性

                    if (!this.gyoushaEntity.GYOUSHAKBN_MANI)
                    {
                        //this.form.JishaKbn.Enabled = false;
                        this.form.TsumikaeHokanKbn.Enabled = false;
                        //this.form.HaishutsuKbn.Enabled = false;
                        //this.form.ShobunJigyoujouKbn.Enabled = false;
                        this.form.SaishuuShobunjouKbn.Enabled = false;

                        //this.form.JishaKbn.Checked = false;
                        this.form.TsumikaeHokanKbn.Checked = false;
                        //this.form.HaishutsuKbn.Checked = false;
                        //this.form.ShobunJigyoujouKbn.Checked = false;
                        this.form.SaishuuShobunjouKbn.Checked = false;
                    }

                    //業者の取引先有無により処理をする1:有 2:無
                    if (this.gyoushaEntity.TORIHIKISAKI_UMU_KBN == 1)
                    {
                        this.torihikisakiEntity = null;
                        if (setGyoushaOnly)
                        {
                            // データ読込時（修正/削除）は既に取得しているCDより取引先検索
                            this.torihikisakiEntity = daoTorisaki.GetDataByCd(this.form.TorihikisakiCode.Text);
                        }
                        else
                        {
                            // データ読込時以外は業者に紐付くCDより取引先検索
                            this.torihikisakiEntity = daoTorisaki.GetDataByCd(this.gyoushaEntity.TORIHIKISAKI_CD);
                        }

                        _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                        _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                        if (this.torihikisakiEntity != null)
                        {
                            this.form.TorihikisakiCode.Enabled = true;
                            this.form.TorihikisakiCodeSearchButton.Enabled = true;
                            this.form.TorihikisakiNew.Enabled = true;

                            this.form.TorihikisakiCode.Text = this.torihikisakiEntity.TORIHIKISAKI_CD;
                            this.form.TorihikisakiName1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                            this.form.TorihikisakiName2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                            if (this.torihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                            {
                                this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.torihikisakiEntity.TEKIYOU_BEGIN;
                            }

                            if (this.torihikisakiEntity.TEKIYOU_END.IsNull)
                            {
                                this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                            }
                            else
                            {
                                this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.torihikisakiEntity.TEKIYOU_END;
                            }
                            if (!this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                            {
                                this.form.KyotenCode.Text = this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                            }

                            this.SearchsetTorihikisaki();
                        }
                    }
                    else
                    {
                        _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", false);
                        _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", false);

                        this.form.TorihikisakiCode.Enabled = false;
                        this.form.TorihikisakiCodeSearchButton.Enabled = false;
                        this.form.TorihikisakiNew.Enabled = false;

                        this.form.TorihikisakiCode.Text = string.Empty;
                        this.form.TorihikisakiName1.Text = string.Empty;
                        this.form.TorihikisakiName2.Text = string.Empty;
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                        this.form.KyotenCode.Text = string.Empty;
                        this.form.KyotenName.Text = string.Empty;

                        //取引先にはいるので現場に移動
                        this.form.GenbaCode.Focus();
                    }
                }
                else
                {
                    this.form.GyoushaKbnUkeire.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_UKEIRE;
                    this.form.GyoushaKbnShukka.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_SHUKKA;
                    this.form.GyoushaKbnMani.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_MANI;

                    this.form.TorihikisakiCode.Enabled = true;
                    this.form.TorihikisakiCodeSearchButton.Enabled = true;
                    this.form.TorihikisakiNew.Enabled = true;

                    this.form.GyoushaName1.Text = string.Empty;
                    this.form.GyoushaName2.Text = string.Empty;
                    this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                    this.form.GYOUSHA_TEKIYOU_END.Value = null;

                    this.form.TorihikisakiCode.Text = string.Empty;
                    this.form.TorihikisakiName1.Text = string.Empty;
                    this.form.TorihikisakiName2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    this.form.KyotenCode.Text = string.Empty;
                    this.form.KyotenName.Text = string.Empty;

                    this.form.JishaKbn.Enabled = true;
                    this.form.HaishutsuKbn.Enabled = true;
                    this.form.TsumikaeHokanKbn.Enabled = true;
                    this.form.ShobunJigyoujouKbn.Enabled = true;
                    this.form.SaishuuShobunjouKbn.Enabled = true;
                    this.form.ShobunsakiCode.Enabled = true;
                    this.form.ManiHensousakiKbn.Enabled = true;
                    this.form.ManifestShuruiCode.Enabled = true;
                    this.form.ManifestTehaiCode.Enabled = true;

                    //現場分類
                    this.form.JishaKbn.Checked = (bool)sysinfoEntity.GENBA_JISHA_KBN;
                    this.form.HaishutsuKbn.Checked = (bool)sysinfoEntity.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN;
                    this.form.TsumikaeHokanKbn.Checked = (bool)sysinfoEntity.GENBA_TSUMIKAEHOKAN_KBN;
                    this.form.ShobunJigyoujouKbn.Checked = (bool)sysinfoEntity.GENBA_SHOBUN_NIOROSHI_GENBA_KBN;
                    this.form.SaishuuShobunjouKbn.Checked = (bool)sysinfoEntity.GENBA_SAISHUU_SHOBUNJOU_KBN;
                    this.form.ManiHensousakiKbn.Checked = (bool)sysinfoEntity.GENBA_MANI_HENSOUSAKI_KBN;
                    if (this.form.GyoushaCode.Text != "")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.isError = true;
                        //業者に移動
                        this.form.GyoushaCode.Focus();
                    }
                }

                // 必須区分変更処理
                this.form.DummyKbnChanged(null, new EventArgs());

                LogUtility.DebugMethodEnd(isInputCheck, setGyoushaOnly, false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchchkGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(isInputCheck, setGyoushaOnly, true);
                return true;
            }
        }

        /// <summary>
        /// データ取得処理(拠点)
        /// </summary>
        /// <returns></returns>
        public int SearchsetTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_TORIHIKISAKI entity = null;
                M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;

                M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                M_TORIHIKISAKI[] result = this.daoTorisaki.GetAllValidData(queryParam);
                if (result != null && result.Length > 0)
                {
                    entity = result[0];
                }

                if (entity == null)
                {
                    this.form.TorihikisakiName1.Text = string.Empty;
                    this.form.TorihikisakiName2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    this.form.KyotenCode.Text = string.Empty;
                    this.form.KyotenName.Text = string.Empty;

                    if (this.form.TorihikisakiCode.Text != "")
                    {
                        // フォーカスアウト時のみエラーを表示
                        // ※一覧からの呼出時などではエラーを表示しない
                        if (this.IsShowTorihikisakiError)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "取引先");
                        }
                        this.isError = true;
                        //取引先に移動
                        this.form.TorihikisakiCode.Focus();
                    }

                    //非活性になっていた場合だけ活性化する。
                    if (!this.form.SeikyuushoSoufusaki1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu, true);
                    }
                    if (!this.form.ShiharaiSoufuName1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Siharai, true);
                    }
                    return 0;
                }
                else
                {
                    this.form.TorihikisakiName1.Text = entity.TORIHIKISAKI_NAME1;
                    this.form.TorihikisakiName2.Text = entity.TORIHIKISAKI_NAME2;
                    if (entity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = entity.TEKIYOU_BEGIN.Value;
                    }
                    if (entity.TEKIYOU_END.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = entity.TEKIYOU_END.Value;
                    }
                    if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                    }

                    Boolean isSeikyuuKake = true;
                    if (seikyuuEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isSeikyuuKake = false;
                        }
                        else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isSeikyuuKake = true;
                        }
                    }
                    if ((!isSeikyuuKake && this.form.SeikyuuDaihyouPrintKbn.Enabled) || (isSeikyuuKake && !this.form.SeikyuuDaihyouPrintKbn.Enabled))
                    {
                        this.ChangeTorihikisakiKbn(Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu, isSeikyuuKake);
                    }

                    Boolean isShiharaiKake = true;
                    if (shiharaiEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[支払情報タブ]内部を非活性
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isShiharaiKake = false;
                        }
                        else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isShiharaiKake = true;
                        }
                    }
                    if ((!isShiharaiKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isShiharaiKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeTorihikisakiKbn(Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Siharai, isShiharaiKake);
                    }
                }

                foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
                {
                    var denpyouKbnCd = row.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
                    if (GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR == denpyouKbnCd.ToString())
                    {
                        this.ChangeTeikiKaishuuControl(this.IsSeikyuuKake(), row);
                    }
                    else if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
                    {
                        this.ChangeTeikiKaishuuControl(this.IsShiharaiKake(), row);
                    }
                    else
                    {
                        this.ChangeTeikiKaishuuControl(true, row);
                    }
                }

                this.kyotenEntity = null;

                this.kyotenEntity = daoKyoten.GetDataByCd(this.form.KyotenCode.Text);

                int count = this.kyotenEntity == null ? 0 : 1;

                //拠点名セットを行う
                if (count > 0)
                {
                    this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                }

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// ポップアップ用部署情報取得メソッド
        /// </summary>
        /// <param name="eigyouTantouCd"></param>
        public bool SetBushoData(string eigyouTantouCd, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(eigyouTantouCd);

                bool ret = true;
                M_SHAIN condition = new M_SHAIN();
                condition.SHAIN_CD = eigyouTantouCd;
                if (!string.IsNullOrWhiteSpace(this.form.EigyouTantouBushoCode.Text))
                {
                    condition.BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                }
                DataTable dt = this.daoShain.GetShainDataSqlFile(GET_POPUP_DATA_SQL, condition);
                if (0 < dt.Rows.Count)
                {
                    this.form.EigyouTantouBushoCode.Text = dt.Rows[0]["BUSHO_CD"].ToString();
                    this.form.EigyouTantouBushoName.Text = dt.Rows[0]["BUSHO_NAME"].ToString();
                    this.form.EigyouCode.Text = dt.Rows[0]["SHAIN_CD"].ToString();
                    this.form.EigyouName.Text = dt.Rows[0]["SHAIN_NAME"].ToString();
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.isError = true;
                    ret = false;
                }

                catchErr = false;
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SetBushoData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
        }

        /// <summary>
        /// 現場CD採番処理
        /// </summary>
        /// <returns>最大CD+1</returns>
        public bool Saiban()
        {
            try
            {
                // 現場マスタのCDの最大値+1を取得
                GenbaMasterAccess genbaMasterAccess = new GenbaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyGenba = -1;

                var keyGenbasaibanFlag = genbaMasterAccess.IsOverCDLimit(this.GyoushaCd, out keyGenba);

                if (keyGenbasaibanFlag || keyGenba < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.GenbaCode.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.GenbaCode.Text = String.Format("{0:D" + this.form.GenbaCode.MaxLength + "}", keyGenba);
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Saiban", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            LogUtility.DebugMethodStart();

            this.gyoushaEntity = null;

            this.gyoushaEntity = daoGyousha.GetDataByCd(this.GyoushaCd);

            int count = this.gyoushaEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchTorihikisaki()
        {
            LogUtility.DebugMethodStart();

            this.torihikisakiEntity = null;

            this.torihikisakiEntity = daoTorisaki.GetDataByCd(this.GenbaEntity.TORIHIKISAKI_CD);

            int count = this.torihikisakiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(拠点)
        /// </summary>
        /// <returns></returns>
        public int SearchKyoten()
        {
            LogUtility.DebugMethodStart();

            this.kyotenEntity = null;

            if (this.torihikisakiEntity != null && !this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
            {
                this.kyotenEntity = daoKyoten.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
            }

            int count = this.kyotenEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(部署)
        /// </summary>
        /// <returns></returns>
        public int SearchBusho()
        {
            LogUtility.DebugMethodStart();

            this.bushoEntity = null;

            this.bushoEntity = daoBusho.GetDataByCd(this.GenbaEntity.EIGYOU_TANTOU_BUSHO_CD);

            int count = this.bushoEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(社員)
        /// </summary>
        /// <returns></returns>
        public int SearchShain()
        {
            LogUtility.DebugMethodStart();

            this.shainEntity = null;

            this.shainEntity = daoShain.GetDataByCd(this.GenbaEntity.EIGYOU_TANTOU_CD);

            int count = this.shainEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(都道府県)
        /// </summary>
        /// <returns></returns>
        public int SearchTodoufuken()
        {
            LogUtility.DebugMethodStart();

            this.todoufukenEntity = null;

            if (!this.GenbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
            {
                this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.GenbaEntity.GENBA_TODOUFUKEN_CD.Value.ToString());
            }
            else
            {
                this.todoufukenEntity = null;
            }

            int count = this.todoufukenEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(地域)
        /// </summary>
        /// <returns></returns>
        public int SearchChiiki()
        {
            LogUtility.DebugMethodStart();

            this.chiikiEntity = null;

            this.chiikiEntity = daoChiiki.GetDataByCd(this.GenbaEntity.CHIIKI_CD);

            int count = this.chiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(集計項目)
        /// </summary>
        /// <returns></returns>
        public int SearchShuukeiItem()
        {
            LogUtility.DebugMethodStart();

            this.shuukeiEntity = null;

            this.shuukeiEntity = daoShuukei.GetDataByCd(this.GenbaEntity.SHUUKEI_ITEM_CD);

            int count = this.shuukeiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(業種)
        /// </summary>
        /// <returns></returns>
        public int SearchGyoushu()
        {
            LogUtility.DebugMethodStart();

            this.gyoushuEntity = null;

            this.gyoushuEntity = daoGyoushu.GetDataByCd(this.GenbaEntity.GYOUSHU_CD);

            int count = this.gyoushuEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(マニ種類)
        /// </summary>
        /// <returns></returns>
        public int SearchManiShurui()
        {
            LogUtility.DebugMethodStart();

            this.manishuruiEntity = null;

            if (!this.GenbaEntity.MANIFEST_SHURUI_CD.IsNull)
            {
                this.manishuruiEntity = daoManishurui.GetDataByCd(this.GenbaEntity.MANIFEST_SHURUI_CD.ToString());
            }

            int count = this.manishuruiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(マニ手配)
        /// </summary>
        /// <returns></returns>
        public int SearchManiTehai()
        {
            LogUtility.DebugMethodStart();

            this.manitehaiEntity = null;

            if (!this.GenbaEntity.MANIFEST_TEHAI_CD.IsNull)
            {
                this.manitehaiEntity = daoManitehai.GetDataByCd(this.GenbaEntity.MANIFEST_TEHAI_CD.ToString());
            }

            int count = this.manitehaiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(委託)
        /// </summary>
        /// <returns></returns>
        public int SearchItaku()
        {
            LogUtility.DebugMethodStart();

            this.SearchResultItaku = new DataTable();

            string gyoushaCd = this.GenbaEntity.GYOUSHA_CD;
            string genbaCd = this.GenbaEntity.GENBA_CD;
            if (string.IsNullOrWhiteSpace(gyoushaCd))
            {
                return 0;
            }

            M_ITAKU_KEIYAKU_KIHON condition = new M_ITAKU_KEIYAKU_KIHON();
            condition.HAISHUTSU_JIGYOUSHA_CD = gyoushaCd;
            condition.HAISHUTSU_JIGYOUJOU_CD = genbaCd;
            this.SearchResultItaku = daoItaku.GetDataBySqlFile(this.GET_ICHIRAN_ITAKU_DATA_SQL, condition);

            int count = this.SearchResultItaku.Rows.Count;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理(定期)
        /// </summary>
        /// <returns></returns>
        public int SearchTeiki()
        {
            LogUtility.DebugMethodStart();

            this.TeikiHinmeiTable = new DataTable();

            string gyoushaCd = this.GenbaEntity.GYOUSHA_CD;
            string genbaCd = this.GenbaEntity.GENBA_CD;
            if (string.IsNullOrWhiteSpace(gyoushaCd))
            {
                return 0;
            }

            M_GENBA_TEIKI_HINMEI condition = new M_GENBA_TEIKI_HINMEI();
            condition.GYOUSHA_CD = gyoushaCd;
            condition.GENBA_CD = genbaCd;
            this.TeikiHinmeiTable = daoGenbaTeiki.GetDataBySqlFile(this.GET_TEIKI_HINMEI_DATA_SQL, condition);

            int count = this.TeikiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 仮現場定期品名の一覧を取得します
        /// </summary>
        /// <returns>取得件数</returns>
        public int SearchKariGenbaTeikiHinmei()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_GENBA_TEIKI_HINMEIDao>();
            this.TeikiHinmeiTable = dao.GetDataBySqlFile(this.GET_KARI_TEIKI_HINMEI_DATA_SQL, new M_KARI_GENBA_TEIKI_HINMEI() { GYOUSHA_CD = this.GyoushaCd, GENBA_CD = this.GenbaCd });
            ret = this.TsukiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 引合現場定期品名の一覧を取得します
        /// </summary>
        /// <returns>取得件数</returns>
        public int SearchHikiaiGenbaTeikiHinmei()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBA_TEIKI_HINMEIDao>();
            M_HIKIAI_GENBA_TEIKI_HINMEI entity = new M_HIKIAI_GENBA_TEIKI_HINMEI();
            entity.GYOUSHA_CD = this.GyoushaCd;
            entity.GENBA_CD = this.GenbaCd;
            entity.HIKIAI_GYOUSHA_USE_FLG = this.form.ShinseiHikiaiGyoushaUseFlg;

            this.TeikiHinmeiTable = dao.GetDataBySqlFile(this.GET_HIKIAI_TEIKI_HINMEI_DATA_SQL, entity);
            ret = this.TeikiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// データ取得処理(月極)
        /// </summary>
        /// <returns></returns>
        public int SearchTsuki()
        {
            LogUtility.DebugMethodStart();

            this.TsukiHinmeiTable = new DataTable();

            string gyoushaCd = this.GenbaEntity.GYOUSHA_CD;
            string genbaCd = this.GenbaEntity.GENBA_CD;
            if (string.IsNullOrWhiteSpace(gyoushaCd))
            {
                return 0;
            }

            M_GENBA_TSUKI_HINMEI condition = new M_GENBA_TSUKI_HINMEI();
            condition.GYOUSHA_CD = gyoushaCd;
            condition.GENBA_CD = genbaCd;
            this.TsukiHinmeiTable = daoGenbaTsuki.GetDataBySqlFile(this.GET_TSUKI_HINMEI_DATA_SQL, condition);

            int count = this.TsukiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 仮現場月極品名の一覧を取得します
        /// </summary>
        /// <returns>取得件数</returns>
        public int SearchKariGenbaTsukiHinmei()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_KARI_GENBA_TSUKI_HINMEIDao>();
            this.TsukiHinmeiTable = dao.GetDataBySqlFile(this.GET_KARI_TSUKI_HINMEI_DATA_SQL, new M_KARI_GENBA_TSUKI_HINMEI() { GYOUSHA_CD = this.GyoushaCd, GENBA_CD = this.GenbaCd });
            ret = this.TsukiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 引合現場月極品名の一覧を取得します
        /// </summary>
        /// <returns>取得件数</returns>
        public int SearchHikiaiGenbaTsukiHinmei()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBA_TSUKI_HINMEIDao>();
            M_HIKIAI_GENBA_TSUKI_HINMEI entity = new M_HIKIAI_GENBA_TSUKI_HINMEI();
            entity.GYOUSHA_CD = this.GyoushaCd;
            entity.GENBA_CD = this.GenbaCd;
            entity.HIKIAI_GYOUSHA_USE_FLG = this.form.ShinseiHikiaiGyoushaUseFlg;

            this.TsukiHinmeiTable = dao.GetDataBySqlFile(this.GET_HIKIAI_TSUKI_HINMEI_DATA_SQL, entity);
            ret = this.TsukiHinmeiTable.Rows.Count;

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        ///// <summary>
        ///// マニフェスト返送先取引先コード検索セット
        ///// </summary>
        ///// <param name="Cd"></param>
        //public void SetManiHensousakiTorihikisaki(string Cd, CancelEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(Cd, e);

        //    M_TORIHIKISAKI condition = new M_TORIHIKISAKI();
        //    condition.TORIHIKISAKI_CD = Cd;
        //    condition.MANI_HENSOUSAKI_KBN = true;
        //    M_TORIHIKISAKI[] tori = this.daoTorisaki.GetAllValidData(condition);

        //    if (tori != null && 0 < tori.Length)
        //    {
        //        this.form.ManiHensousakiName1.Text = tori[0].MANI_HENSOUSAKI_NAME1;
        //        this.form.ManiHensousakiKeishou1.Text = tori[0].MANI_HENSOUSAKI_KEISHOU1;
        //        this.form.ManiHensousakiName2.Text = tori[0].MANI_HENSOUSAKI_NAME2;
        //        this.form.ManiHensousakiKeishou2.Text = tori[0].MANI_HENSOUSAKI_KEISHOU2;
        //        this.form.ManiHensousakiPost.Text = tori[0].MANI_HENSOUSAKI_POST;
        //        this.form.ManiHensousakiAddress1.Text = tori[0].MANI_HENSOUSAKI_ADDRESS1;
        //        this.form.ManiHensousakiAddress2.Text = tori[0].MANI_HENSOUSAKI_ADDRESS2;
        //        this.form.ManiHensousakiBusho.Text = tori[0].MANI_HENSOUSAKI_BUSHO;
        //        this.form.ManiHensousakiTantou.Text = tori[0].MANI_HENSOUSAKI_TANTOU;
        //    }
        //    else
        //    {
        //        this.form.ManiHensousakiName1.Text = string.Empty;
        //        this.form.ManiHensousakiKeishou1.Text = string.Empty;
        //        this.form.ManiHensousakiName2.Text = string.Empty;
        //        this.form.ManiHensousakiKeishou2.Text = string.Empty;
        //        this.form.ManiHensousakiPost.Text = string.Empty;
        //        this.form.ManiHensousakiAddress1.Text = string.Empty;
        //        this.form.ManiHensousakiAddress2.Text = string.Empty;
        //        this.form.ManiHensousakiBusho.Text = string.Empty;
        //        this.form.ManiHensousakiTantou.Text = string.Empty;

        //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //        msgLogic.MessageBoxShow("E020", "取引先");
        //        if (e != null)
        //        {
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            this.form.ManiHensousakiTorihikisakiCode.Focus();
        //        }
        //        this.isError = true;
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// マニフェスト返送先業者コード検索セット
        ///// </summary>
        ///// <param name="Cd"></param>
        //public void SetManiHensousakiGyousha(string Cd, CancelEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(Cd, e);

        //    M_GYOUSHA condition = new M_GYOUSHA();
        //    condition.GYOUSHA_CD = Cd;
        //    condition.MANI_HENSOUSAKI_KBN = true;
        //    M_GYOUSHA[] gyousha = this.daoGyousha.GetAllValidData(condition);
        //    if (gyousha != null && 0 < gyousha.Length && (this.form.HensousakiKbn.Text == "2" || this.form.HensousakiKbn.Text == "3"))
        //    {
        //        this.form.ManiHensousakiName1.Text = gyousha[0].MANI_HENSOUSAKI_NAME1;
        //        this.form.ManiHensousakiKeishou1.Text = gyousha[0].MANI_HENSOUSAKI_KEISHOU1;
        //        this.form.ManiHensousakiName2.Text = gyousha[0].MANI_HENSOUSAKI_NAME2;
        //        this.form.ManiHensousakiKeishou2.Text = gyousha[0].MANI_HENSOUSAKI_KEISHOU2;
        //        this.form.ManiHensousakiPost.Text = gyousha[0].MANI_HENSOUSAKI_POST;
        //        this.form.ManiHensousakiAddress1.Text = gyousha[0].MANI_HENSOUSAKI_ADDRESS1;
        //        this.form.ManiHensousakiAddress2.Text = gyousha[0].MANI_HENSOUSAKI_ADDRESS2;
        //        this.form.ManiHensousakiBusho.Text = gyousha[0].MANI_HENSOUSAKI_BUSHO;
        //        this.form.ManiHensousakiTantou.Text = gyousha[0].MANI_HENSOUSAKI_TANTOU;
        //    }
        //    else
        //    {
        //        if (this.form.HensousakiKbn.Text == "2" || this.form.HensousakiKbn.Text == "3")
        //        {
        //            this.form.ManiHensousakiName1.Text = string.Empty;
        //            this.form.ManiHensousakiKeishou1.Text = string.Empty;
        //            this.form.ManiHensousakiName2.Text = string.Empty;
        //            this.form.ManiHensousakiKeishou2.Text = string.Empty;
        //            this.form.ManiHensousakiPost.Text = string.Empty;
        //            this.form.ManiHensousakiAddress1.Text = string.Empty;
        //            this.form.ManiHensousakiAddress2.Text = string.Empty;
        //            this.form.ManiHensousakiBusho.Text = string.Empty;
        //            this.form.ManiHensousakiTantou.Text = string.Empty;

        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //            msgLogic.MessageBoxShow("E020", "業者");
        //            if (e != null)
        //            {
        //                e.Cancel = true;
        //            }
        //            else
        //            {
        //                this.form.ManiHensousakiGyoushaCode.Focus();
        //            }
        //            this.isError = true;
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// マニフェスト返送先現場コード検索セット
        ///// </summary>
        ///// <param name="Cd"></param>
        //public bool SetManiHensousakiGenba(string Cd, CancelEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(Cd, e);

        //    bool result = false;

        //    //業者が入力されていないとき
        //    if (string.IsNullOrWhiteSpace(this.ManiGyoushaCd))
        //    {
        //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //        msgLogic.MessageBoxShow("E012", "業者CD");
        //        this.form.ManiHensousakiGyoushaCode.Focus();

        //        this.isError = true;

        //        LogUtility.DebugMethodEnd(result);
        //        return result;
        //    }

        //    M_GENBA condition = new M_GENBA();
        //    condition.GENBA_CD = Cd;
        //    condition.GYOUSHA_CD = this.ManiGyoushaCd;
        //    condition.MANI_HENSOUSAKI_KBN = true;
        //    M_GENBA[] genba = this.daoGenba.GetAllValidData(condition);
        //    if (genba != null && genba.Length > 0)
        //    {
        //        this.form.ManiHensousakiName1.Text = genba[0].MANI_HENSOUSAKI_NAME1;
        //        this.form.ManiHensousakiKeishou1.Text = genba[0].MANI_HENSOUSAKI_KEISHOU1;
        //        this.form.ManiHensousakiName2.Text = genba[0].MANI_HENSOUSAKI_NAME2;
        //        this.form.ManiHensousakiKeishou2.Text = genba[0].MANI_HENSOUSAKI_KEISHOU2;
        //        this.form.ManiHensousakiPost.Text = genba[0].MANI_HENSOUSAKI_POST;
        //        this.form.ManiHensousakiAddress1.Text = genba[0].MANI_HENSOUSAKI_ADDRESS1;
        //        this.form.ManiHensousakiAddress2.Text = genba[0].MANI_HENSOUSAKI_ADDRESS2;
        //        this.form.ManiHensousakiBusho.Text = genba[0].MANI_HENSOUSAKI_BUSHO;
        //        this.form.ManiHensousakiTantou.Text = genba[0].MANI_HENSOUSAKI_TANTOU;
        //        result = true;
        //    }
        //    else
        //    {
        //        this.form.ManiHensousakiName1.Text = string.Empty;
        //        this.form.ManiHensousakiKeishou1.Text = string.Empty;
        //        this.form.ManiHensousakiName2.Text = string.Empty;
        //        this.form.ManiHensousakiKeishou2.Text = string.Empty;
        //        this.form.ManiHensousakiPost.Text = string.Empty;
        //        this.form.ManiHensousakiAddress1.Text = string.Empty;
        //        this.form.ManiHensousakiAddress2.Text = string.Empty;
        //        this.form.ManiHensousakiBusho.Text = string.Empty;
        //        this.form.ManiHensousakiTantou.Text = string.Empty;

        //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //        msgLogic.MessageBoxShow("E020", "現場");
        //        if (e != null)
        //        {
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            this.form.ManiHensousakiGenbaCode.Focus();
        //        }
        //        this.isError = true;
        //    }

        //    LogUtility.DebugMethodEnd(result);
        //    return result;
        //}

        /// <summary>
        /// マニフェスト返送先取引先コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiTorihikisaki(string Cd, CancelEventArgs e, string hensouCd)
        {
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);

                M_TORIHIKISAKI condition = new M_TORIHIKISAKI();
                condition.TORIHIKISAKI_CD = Cd;
                condition.MANI_HENSOUSAKI_KBN = true;
                M_TORIHIKISAKI[] tori = this.daoTorisaki.GetAllValidData(condition);

                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);

                if (tori != null && 0 < tori.Length)
                {
                    ManiHensousakiTorihikisakiName.Text = tori[0].TORIHIKISAKI_NAME_RYAKU;
                }
                else
                {
                    ManiHensousakiTorihikisakiName.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        ManiHensousakiTorihikisakiCode.Focus();
                    }
                    this.isError = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                this.isError = true;
                LogUtility.Error("SetManiHensousakiTorihikisaki", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("SetManiHensousakiTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// マニフェスト返送先業者コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiGyousha(string Cd, CancelEventArgs e, string hensouCd)
        {
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);

                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);

                M_GYOUSHA condition = new M_GYOUSHA();
                condition.GYOUSHA_CD = Cd;
                if ("3" != HensousakiKbn.Text)
                {
                    condition.MANI_HENSOUSAKI_KBN = true;
                }
                M_GYOUSHA[] gyousha = this.daoGyousha.GetAllValidData(condition);

                if (gyousha != null && 0 < gyousha.Length)
                {
                    ManiHensousakiGyoushaName.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                    ManiHensousakiGenbaCode.Text = string.Empty;
                    ManiHensousakiGenbaName.Text = string.Empty;
                }
                else
                {
                    ManiHensousakiGyoushaName.Text = string.Empty;
                    ManiHensousakiGenbaCode.Text = string.Empty;
                    ManiHensousakiGenbaName.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        ManiHensousakiGyoushaCode.Focus();
                    }
                    this.isError = true;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                this.isError = true;
                LogUtility.Error("SetManiHensousakiGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                this.isError = true;
                LogUtility.Error("SetManiHensousakiGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// マニフェスト返送先現場コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiGenba(string Cd, CancelEventArgs e, string hensouCd, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);

                bool result = false;
                ManiGenbaCd = string.Empty;    // No3521
                catchErr = false;

                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //業者が入力されていないとき
                if (string.IsNullOrWhiteSpace(this.ManiGyoushaCd))
                {
                    ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);

                    ManiHensousakiGenbaName.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E012", "業者CD");
                    ManiHensousakiGyoushaCode.Focus();

                    this.isError = true;

                    LogUtility.DebugMethodEnd(result, catchErr);
                    return result;
                }

                //タブ内(A票～E票)のコントロールに紐付ける
                //テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);

                M_GENBA condition = new M_GENBA();
                condition.GENBA_CD = Cd;
                condition.GYOUSHA_CD = this.ManiGyoushaCd;
                condition.MANI_HENSOUSAKI_KBN = true;
                M_GENBA[] genba = this.daoGenba.GetAllValidData(condition);
                if (genba != null && genba.Length > 0)
                {
                    ManiHensousakiGenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                    //this.SetManiHensousakiInfo(genba[0], hensouCd);
                    // No3521-->
                    ManiGenbaCd = Cd;
                    // No3521<--
                    result = true;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        ManiHensousakiGenbaCode.Focus();
                    }
                    this.isError = true;
                }

                LogUtility.DebugMethodEnd(result, catchErr);
                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("SetManiHensousakiGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("SetManiHensousakiGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
        }

        /// <summary>
        /// 検索結果を委託一覧に設定
        /// </summary>
        internal void SetIchiranItaku()
        {
            this.form.ItakuKeiyakuIchiran.IsBrowsePurpose = false;
            var table = this.SearchResultItaku;
            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.form.ItakuKeiyakuIchiran.DataSource = table;
            this.form.ItakuKeiyakuIchiran.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 検索結果を定期一覧に設定
        /// </summary>
        /// <param name="isCellEvent">CellEnter,CellValidatingイベントからの呼出時に true</param>
        internal void SetIchiranTeiki(bool isCellEvent = false)
        {
            var table = this.TeikiHinmeiTable;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].AllowDBNull = true;
                table.Columns[i].ReadOnly = false;
                table.Columns[i].Unique = false;
            }

            this.form.TeikiHinmeiIchiran.DataSource = table;
            this.SetIchiranTeikiRowControl(isCellEvent);
        }

        /// <summary>
        /// 定期一覧の行内容による制御を実施する
        /// </summary>
        /// <param name="isCellEvent">CellEnter,CellValidatingイベントからの呼出時に true</param>
        public bool SetIchiranTeikiRowControl(bool isCellEvent = false)
        {
            try
            {
                bool isKake = this.IsSeikyuuKake();
                foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                            || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                        {
                            foreach (var tmpCell in row.Cells)
                            {
                                switch (tmpCell.Name)
                                {
                                    case "DELETE_FLG":
                                    case "KOUSHIN_FLG": // QN Tue Anh #158986
                                    case "HINMEI_CD":
                                    case "UNIT_CD":
                                    case "UNIT_NAME_RYAKU":
                                    case "KANSANCHI":
                                    case "KANSAN_UNIT_CD":
                                    case "KANSAN_UNIT_NAME_RYAKU":
                                    case "KANSAN_UNIT_MOBILE_OUTPUT_FLG":
                                    case "ANBUN_FLG":
                                    case "MONDAY":
                                    case "TUESDAY":
                                    case "WEDNESDAY":
                                    case "THURSDAY":
                                    case "FRIDAY":
                                    case "SATURDAY":
                                    case "SUNDAY":
                                    case "KEIYAKU_KBN":
                                        tmpCell.Enabled = false;
                                        break;
                                }
                            }
                        }
                        continue;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value)) && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && !isCellEvent)
                    {
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].ReadOnly = true;
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].ReadOnly = true;
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].ReadOnly = true;
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells[Const.GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].Style.BackColor = Constans.READONLY_COLOR;
                    }

                    var denpyouKbnCd = row.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
                    if (GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR == denpyouKbnCd.ToString())
                    {
                        isKake = this.IsSeikyuuKake();
                    }
                    else if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
                    {
                        isKake = this.IsShiharaiKake();
                    }
                    if (!isKake)
                    {
                        var keiyakuCell = row.Cells[GenbaHoshuConstans.TEIKI_KEIYAKU_KBN];
                        var keijyoCell = row.Cells[GenbaHoshuConstans.TEIKI_KEIJYOU_KBN];
                        if (Convert.ToString(keiyakuCell.Value) == "1")
                        {
                            row.Cells[GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = DBNull.Value;
                            row.Cells[GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                        }
                        if (Convert.ToString(keijyoCell.Value) == "2")
                        {
                            row.Cells[GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                        }
                    }

                    CellEventArgs ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Index, Const.GenbaHoshuConstans.TEIKI_UNIT_CD);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Index, Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN);
                    this.TeikiHinmeiCellValueChanged(ea);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Index, Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Index, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Index, Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN);
                    this.TeikiHinmeiCellLeave(ea);

                    if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        foreach (var tmpCell in row.Cells)
                        {
                            switch (tmpCell.Name)
                            {
                                case "DELETE_FLG":
                                case "KOUSHIN_FLG": // QN Tue Anh #158986
                                case "HINMEI_CD":
                                case "UNIT_CD":
                                case "UNIT_NAME_RYAKU":
                                case "KANSANCHI":
                                case "KANSAN_UNIT_CD":
                                case "KANSAN_UNIT_NAME_RYAKU":
                                case "KANSAN_UNIT_MOBILE_OUTPUT_FLG":
                                case "ANBUN_FLG":
                                case "MONDAY":
                                case "TUESDAY":
                                case "WEDNESDAY":
                                case "THURSDAY":
                                case "FRIDAY":
                                case "SATURDAY":
                                case "SUNDAY":
                                case "KEIYAKU_KBN":
                                    tmpCell.Enabled = false;
                                    break;
                            }
                        }
                    }


                    //set column hide BEFORE_HINMEI_CD = HINMEI_CD
                    row.Cells[Const.GenbaHoshuConstans.TEIKI_BEFORE_HINMEI_CD].Value = row.Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value;
                }
                this.form.TeikiHinmeiIchiran.Refresh();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTeikiRowControl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 検索結果を月極一覧に設定
        /// </summary>
        internal void SetIchiranTsuki()
        {
            var table = this.TsukiHinmeiTable;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].AllowDBNull = true;
                table.Columns[i].ReadOnly = false;
                table.Columns[i].Unique = false;
            }

            this.form.TsukiHinmeiIchiran.DataSource = table;

            foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["CHOUKA_SETTING"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["CHOUKA_SETTING"].Value))
                {
                    row.Cells["CHOUKA_LIMIT_AMOUNT"].Enabled = true;
                    row.Cells["CHOUKA_HINMEI_NAME"].Enabled = true;
                    row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["CHOUKA_HINMEI_NAME"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;
                    row.Cells["CHOUKA_HINMEI_NAME"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;

                    SelectCheckDto existCheck = new SelectCheckDto();
                    existCheck.CheckMethodName = "必須チェック";
                    Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                    excitChecks.Add(existCheck);
                    ((GcCustomNumericTextBox2Cell)row.Cells["CHOUKA_LIMIT_AMOUNT"]).RegistCheckMethod = excitChecks;
                    ((GcCustomTextBoxCell)row.Cells["CHOUKA_HINMEI_NAME"]).RegistCheckMethod = excitChecks;
                }
                else
                {
                    ((GcCustomNumericTextBox2Cell)row.Cells["CHOUKA_LIMIT_AMOUNT"]).RegistCheckMethod = null;
                    ((GcCustomTextBoxCell)row.Cells["CHOUKA_HINMEI_NAME"]).RegistCheckMethod = null;
                }
            }
            this.form.TsukiHinmeiIchiran.Refresh();
        }

        /// <summary>
        /// 委託計約種類、ステータス変換処理
        /// </summary>
        /// <param name="e"></param>
        public bool ChangeItakuStatus(GrapeCity.Win.MultiRow.CellFormattingEventArgs e)
        {
            try
            {
                switch (e.CellName)
                {
                    case "ITAKU_KEIYAKU_SHURUI":
                        if (e.Value != null)
                        {
                            e.Value = this.ItakuKeiyakuShurui[e.Value.ToString()];
                        }
                        break;

                    case "YUUKOU_BEGIN":
                    case "YUUKOU_END":
                        if (!string.IsNullOrWhiteSpace(e.Value.ToString()))
                        {
                            e.Value = ((DateTime)e.Value).ToString("yyyy/MM/dd");
                        }
                        break;

                    case "ITAKU_KEIYAKU_STATUS":
                        if (e.Value != null)
                        {
                            e.Value = this.ItakuKeiyakuStatus[e.Value.ToString()];
                        }
                        break;
                    case "ITAKU_KEIYAKU_TOUROKU_HOUHOU":
                        if (e.Value != null)
                        {
                            e.Value = this.ItakuKeiyakuTouroku[e.Value.ToString()];
                        }
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeItakuStatus", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニありチェックボックスのON/OFFチェック
        /// </summary>
        /// <param name="clearFlag"></param>
        /// <returns></returns>
        public bool ManiCheckOffCheck(bool clearFlag)
        {
            try
            {
                this.FlgManiHensousakiKbn = this.form.ManiHensousakiKbn.Checked;

                if (this.FlgManiHensousakiKbn)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = true;
                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.form.ManiHensousakiName1.Enabled = true;
                        this.form.ManiHensousakiName2.Enabled = true;
                        this.form.ManiHensousakiKeishou1.Enabled = true;
                        this.form.ManiHensousakiKeishou2.Enabled = true;
                        this.form.ManiHensousakiPost.Enabled = true;
                        this.form.ManiHensousakiAddress1.Enabled = true;
                        this.form.ManiHensousakiAddress2.Enabled = true;
                        this.form.ManiHensousakiBusho.Enabled = true;
                        this.form.ManiHensousakiTantou.Enabled = true;
                        this.form.GENBA_COPY_MANI_BUTTON.Enabled = true;
                        this.form.ManiHensousakiPostSearchButton.Enabled = true;
                        this.form.ManiHensousakiAddressSearchButton.Enabled = true;
                    }
                    else
                    {
                        this.form.ManiHensousakiName1.Enabled = false;
                        this.form.ManiHensousakiName2.Enabled = false;
                        this.form.ManiHensousakiKeishou1.Enabled = false;
                        this.form.ManiHensousakiKeishou2.Enabled = false;
                        this.form.ManiHensousakiPost.Enabled = false;
                        this.form.ManiHensousakiAddress1.Enabled = false;
                        this.form.ManiHensousakiAddress2.Enabled = false;
                        this.form.ManiHensousakiBusho.Enabled = false;
                        this.form.ManiHensousakiTantou.Enabled = false;
                        this.form.GENBA_COPY_MANI_BUTTON.Enabled = false;
                        this.form.ManiHensousakiPostSearchButton.Enabled = false;
                        this.form.ManiHensousakiAddressSearchButton.Enabled = false;
                    }

                    // 各タブの返送先区分を非活性にする
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        if (this.form.MANIFEST_USE_1_AHyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked)
                            {
                                this.form.HensousakiKbn_AHyo.Enabled = false;
                                this.form.HensousakiKbn1_AHyo.Enabled = false;
                                this.form.HensousakiKbn2_AHyo.Enabled = false;
                                this.form.HensousakiKbn3_AHyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_AHyo.Enabled = true;
                                this.form.HensousakiKbn1_AHyo.Enabled = true;
                                this.form.HensousakiKbn2_AHyo.Enabled = true;
                                this.form.HensousakiKbn3_AHyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_AHyo.Text))
                                {
                                    this.form.HensousakiKbn1_AHyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_AHyo.Text))
                                {
                                    this.form.HensousakiKbn2_AHyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_AHyo.Text))
                                {
                                    this.form.HensousakiKbn3_AHyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                                    this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = false;
                            this.form.HensousakiKbn_AHyo.Enabled = false;
                            this.form.HensousakiKbn1_AHyo.Enabled = false;
                            this.form.HensousakiKbn2_AHyo.Enabled = false;
                            this.form.HensousakiKbn3_AHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.HensousakiKbn1_AHyo.Checked = true;
                            this.form.HensousakiKbn2_AHyo.Checked = false;
                            this.form.HensousakiKbn3_AHyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        if (this.form.MANIFEST_USE_1_B1Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked)
                            {
                                this.form.HensousakiKbn_B1Hyo.Enabled = false;
                                this.form.HensousakiKbn1_B1Hyo.Enabled = false;
                                this.form.HensousakiKbn2_B1Hyo.Enabled = false;
                                this.form.HensousakiKbn3_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B1Hyo.Enabled = true;
                                this.form.HensousakiKbn1_B1Hyo.Enabled = true;
                                this.form.HensousakiKbn2_B1Hyo.Enabled = true;
                                this.form.HensousakiKbn3_B1Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_B1Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_B1Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_B1Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.HensousakiKbn1_B1Hyo.Checked = true;
                            this.form.HensousakiKbn2_B1Hyo.Checked = false;
                            this.form.HensousakiKbn3_B1Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        if (this.form.MANIFEST_USE_1_B2Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked)
                            {
                                this.form.HensousakiKbn_B2Hyo.Enabled = false;
                                this.form.HensousakiKbn1_B2Hyo.Enabled = false;
                                this.form.HensousakiKbn2_B2Hyo.Enabled = false;
                                this.form.HensousakiKbn3_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B2Hyo.Enabled = true;
                                this.form.HensousakiKbn1_B2Hyo.Enabled = true;
                                this.form.HensousakiKbn2_B2Hyo.Enabled = true;
                                this.form.HensousakiKbn3_B2Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_B2Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_B2Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_B2Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.HensousakiKbn1_B2Hyo.Checked = true;
                            this.form.HensousakiKbn2_B2Hyo.Checked = false;
                            this.form.HensousakiKbn3_B2Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        if (this.form.MANIFEST_USE_1_B4Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked)
                            {
                                this.form.HensousakiKbn_B4Hyo.Enabled = false;
                                this.form.HensousakiKbn1_B4Hyo.Enabled = false;
                                this.form.HensousakiKbn2_B4Hyo.Enabled = false;
                                this.form.HensousakiKbn3_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B4Hyo.Enabled = true;
                                this.form.HensousakiKbn1_B4Hyo.Enabled = true;
                                this.form.HensousakiKbn2_B4Hyo.Enabled = true;
                                this.form.HensousakiKbn3_B4Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_B4Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_B4Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_B4Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.HensousakiKbn1_B4Hyo.Checked = true;
                            this.form.HensousakiKbn2_B4Hyo.Checked = false;
                            this.form.HensousakiKbn3_B4Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        if (this.form.MANIFEST_USE_1_B6Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked)
                            {
                                this.form.HensousakiKbn_B6Hyo.Enabled = false;
                                this.form.HensousakiKbn1_B6Hyo.Enabled = false;
                                this.form.HensousakiKbn2_B6Hyo.Enabled = false;
                                this.form.HensousakiKbn3_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B6Hyo.Enabled = true;
                                this.form.HensousakiKbn1_B6Hyo.Enabled = true;
                                this.form.HensousakiKbn2_B6Hyo.Enabled = true;
                                this.form.HensousakiKbn3_B6Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_B6Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_B6Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_B6Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.HensousakiKbn1_B6Hyo.Checked = true;
                            this.form.HensousakiKbn2_B6Hyo.Checked = false;
                            this.form.HensousakiKbn3_B6Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        if (this.form.MANIFEST_USE_1_C1Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked)
                            {
                                this.form.HensousakiKbn_C1Hyo.Enabled = false;
                                this.form.HensousakiKbn1_C1Hyo.Enabled = false;
                                this.form.HensousakiKbn2_C1Hyo.Enabled = false;
                                this.form.HensousakiKbn3_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C1Hyo.Enabled = true;
                                this.form.HensousakiKbn1_C1Hyo.Enabled = true;
                                this.form.HensousakiKbn2_C1Hyo.Enabled = true;
                                this.form.HensousakiKbn3_C1Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_C1Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_C1Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_C1Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn1_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn2_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn3_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.HensousakiKbn1_C1Hyo.Checked = true;
                            this.form.HensousakiKbn2_C1Hyo.Checked = false;
                            this.form.HensousakiKbn3_C1Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        if (this.form.MANIFEST_USE_1_C2Hyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked)
                            {
                                this.form.HensousakiKbn_C2Hyo.Enabled = false;
                                this.form.HensousakiKbn1_C2Hyo.Enabled = false;
                                this.form.HensousakiKbn2_C2Hyo.Enabled = false;
                                this.form.HensousakiKbn3_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C2Hyo.Enabled = true;
                                this.form.HensousakiKbn1_C2Hyo.Enabled = true;
                                this.form.HensousakiKbn2_C2Hyo.Enabled = true;
                                this.form.HensousakiKbn3_C2Hyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                                {
                                    this.form.HensousakiKbn1_C2Hyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                                {
                                    this.form.HensousakiKbn2_C2Hyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                                {
                                    this.form.HensousakiKbn3_C2Hyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn1_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn2_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn3_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.HensousakiKbn1_C2Hyo.Checked = true;
                            this.form.HensousakiKbn2_C2Hyo.Checked = false;
                            this.form.HensousakiKbn3_C2Hyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        if (this.form.MANIFEST_USE_1_DHyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked)
                            {
                                this.form.HensousakiKbn_DHyo.Enabled = false;
                                this.form.HensousakiKbn1_DHyo.Enabled = false;
                                this.form.HensousakiKbn2_DHyo.Enabled = false;
                                this.form.HensousakiKbn3_DHyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_DHyo.Enabled = true;
                                this.form.HensousakiKbn1_DHyo.Enabled = true;
                                this.form.HensousakiKbn2_DHyo.Enabled = true;
                                this.form.HensousakiKbn3_DHyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_DHyo.Text))
                                {
                                    this.form.HensousakiKbn1_DHyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_DHyo.Text))
                                {
                                    this.form.HensousakiKbn2_DHyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_DHyo.Text))
                                {
                                    this.form.HensousakiKbn3_DHyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = false;
                            this.form.HensousakiKbn_DHyo.Enabled = false;
                            this.form.HensousakiKbn1_DHyo.Enabled = false;
                            this.form.HensousakiKbn2_DHyo.Enabled = false;
                            this.form.HensousakiKbn3_DHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.HensousakiKbn1_DHyo.Checked = true;
                            this.form.HensousakiKbn2_DHyo.Checked = false;
                            this.form.HensousakiKbn3_DHyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        if (this.form.MANIFEST_USE_1_EHyo.Checked)
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = true;
                            if (this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked)
                            {
                                this.form.HensousakiKbn_EHyo.Enabled = false;
                                this.form.HensousakiKbn1_EHyo.Enabled = false;
                                this.form.HensousakiKbn2_EHyo.Enabled = false;
                                this.form.HensousakiKbn3_EHyo.Enabled = false;
                                this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;

                                this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                                this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                                this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                                this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                            }
                            else
                            {
                                this.form.HensousakiKbn_EHyo.Enabled = true;
                                this.form.HensousakiKbn1_EHyo.Enabled = true;
                                this.form.HensousakiKbn2_EHyo.Enabled = true;
                                this.form.HensousakiKbn3_EHyo.Enabled = true;

                                if ("1".Equals(this.form.HensousakiKbn_EHyo.Text))
                                {
                                    this.form.HensousakiKbn1_EHyo.Checked = true;
                                    this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                                }
                                else if ("2".Equals(this.form.HensousakiKbn_EHyo.Text))
                                {
                                    this.form.HensousakiKbn2_EHyo.Checked = true;
                                    this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                                }
                                else if ("3".Equals(this.form.HensousakiKbn_EHyo.Text))
                                {
                                    this.form.HensousakiKbn3_EHyo.Checked = true;
                                    this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = false;
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = false;
                            this.form.HensousakiKbn_EHyo.Enabled = false;
                            this.form.HensousakiKbn1_EHyo.Enabled = false;
                            this.form.HensousakiKbn2_EHyo.Enabled = false;
                            this.form.HensousakiKbn3_EHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.HensousakiKbn1_EHyo.Checked = true;
                            this.form.HensousakiKbn2_EHyo.Checked = false;
                            this.form.HensousakiKbn3_EHyo.Checked = false;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;

                    this.form.ManiHensousakiName1.Enabled = false;
                    this.form.ManiHensousakiName2.Enabled = false;
                    this.form.ManiHensousakiKeishou1.Enabled = false;
                    this.form.ManiHensousakiKeishou2.Enabled = false;
                    this.form.ManiHensousakiPost.Enabled = false;
                    this.form.ManiHensousakiAddress1.Enabled = false;
                    this.form.ManiHensousakiAddress2.Enabled = false;
                    this.form.ManiHensousakiBusho.Enabled = false;
                    this.form.ManiHensousakiTantou.Enabled = false;
                    this.form.GENBA_COPY_MANI_BUTTON.Enabled = false;
                    this.form.ManiHensousakiPostSearchButton.Enabled = false;
                    this.form.ManiHensousakiAddressSearchButton.Enabled = false;

                    this.form.ManiHensousakiName1.Text = string.Empty;
                    this.form.ManiHensousakiName2.Text = string.Empty;
                    this.form.ManiHensousakiKeishou1.Text = string.Empty;
                    this.form.ManiHensousakiKeishou2.Text = string.Empty;
                    this.form.ManiHensousakiPost.Text = string.Empty;
                    this.form.ManiHensousakiAddress1.Text = string.Empty;
                    this.form.ManiHensousakiAddress2.Text = string.Empty;
                    this.form.ManiHensousakiBusho.Text = string.Empty;
                    this.form.ManiHensousakiTantou.Text = string.Empty;

                    //A票
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_AHyo.Checked)
                        {
                            this.form.HensousakiKbn_AHyo.Enabled = true;
                            this.form.HensousakiKbn1_AHyo.Enabled = true;
                            this.form.HensousakiKbn2_AHyo.Enabled = true;
                            this.form.HensousakiKbn3_AHyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_AHyo.Text))
                            {
                                this.form.HensousakiKbn1_AHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_AHyo.Text))
                            {
                                this.form.HensousakiKbn2_AHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_AHyo.Text))
                            {
                                this.form.HensousakiKbn3_AHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_AHyo.Enabled = false;
                            this.form.HensousakiKbn1_AHyo.Enabled = false;
                            this.form.HensousakiKbn2_AHyo.Enabled = false;
                            this.form.HensousakiKbn3_AHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                        }
                    }

                    //B1票
                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_B1Hyo.Checked)
                        {
                            this.form.HensousakiKbn_B1Hyo.Enabled = true;
                            this.form.HensousakiKbn1_B1Hyo.Enabled = true;
                            this.form.HensousakiKbn2_B1Hyo.Enabled = true;
                            this.form.HensousakiKbn3_B1Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                            {
                                this.form.HensousakiKbn1_B1Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                            {
                                this.form.HensousakiKbn2_B1Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_B1Hyo.Text))
                            {
                                this.form.HensousakiKbn3_B1Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B1Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                        }
                    }

                    //B2票
                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_B2Hyo.Checked)
                        {
                            this.form.HensousakiKbn_B2Hyo.Enabled = true;
                            this.form.HensousakiKbn1_B2Hyo.Enabled = true;
                            this.form.HensousakiKbn2_B2Hyo.Enabled = true;
                            this.form.HensousakiKbn3_B2Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                            {
                                this.form.HensousakiKbn1_B2Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                            {
                                this.form.HensousakiKbn2_B2Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_B2Hyo.Text))
                            {
                                this.form.HensousakiKbn3_B2Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B2Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                        }
                    }

                    //B4票
                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_B4Hyo.Checked)
                        {
                            this.form.HensousakiKbn_B4Hyo.Enabled = true;
                            this.form.HensousakiKbn1_B4Hyo.Enabled = true;
                            this.form.HensousakiKbn2_B4Hyo.Enabled = true;
                            this.form.HensousakiKbn3_B4Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                            {
                                this.form.HensousakiKbn1_B4Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                            {
                                this.form.HensousakiKbn2_B4Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_B4Hyo.Text))
                            {
                                this.form.HensousakiKbn3_B4Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B4Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                        }
                    }

                    //B6票
                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_B6Hyo.Checked)
                        {
                            this.form.HensousakiKbn_B6Hyo.Enabled = true;
                            this.form.HensousakiKbn1_B6Hyo.Enabled = true;
                            this.form.HensousakiKbn2_B6Hyo.Enabled = true;
                            this.form.HensousakiKbn3_B6Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                            {
                                this.form.HensousakiKbn1_B6Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                            {
                                this.form.HensousakiKbn2_B6Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_B6Hyo.Text))
                            {
                                this.form.HensousakiKbn3_B6Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn1_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn2_B6Hyo.Enabled = false;
                            this.form.HensousakiKbn3_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                        }
                    }

                    //C1票
                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_C1Hyo.Checked)
                        {
                            this.form.HensousakiKbn_C1Hyo.Enabled = true;
                            this.form.HensousakiKbn1_C1Hyo.Enabled = true;
                            this.form.HensousakiKbn2_C1Hyo.Enabled = true;
                            this.form.HensousakiKbn3_C1Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                            {
                                this.form.HensousakiKbn1_C1Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                            {
                                this.form.HensousakiKbn2_C1Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_C1Hyo.Text))
                            {
                                this.form.HensousakiKbn3_C1Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn1_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn2_C1Hyo.Enabled = false;
                            this.form.HensousakiKbn3_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                        }
                    }

                    //C2票
                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_C2Hyo.Checked)
                        {
                            this.form.HensousakiKbn_C2Hyo.Enabled = true;
                            this.form.HensousakiKbn1_C2Hyo.Enabled = true;
                            this.form.HensousakiKbn2_C2Hyo.Enabled = true;
                            this.form.HensousakiKbn3_C2Hyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                            {
                                this.form.HensousakiKbn1_C2Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                            {
                                this.form.HensousakiKbn2_C2Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_C2Hyo.Text))
                            {
                                this.form.HensousakiKbn3_C2Hyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn1_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn2_C2Hyo.Enabled = false;
                            this.form.HensousakiKbn3_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                        }
                    }

                    //D票
                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_DHyo.Checked)
                        {
                            this.form.HensousakiKbn_DHyo.Enabled = true;
                            this.form.HensousakiKbn1_DHyo.Enabled = true;
                            this.form.HensousakiKbn2_DHyo.Enabled = true;
                            this.form.HensousakiKbn3_DHyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_DHyo.Text))
                            {
                                this.form.HensousakiKbn1_DHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_DHyo.Text))
                            {
                                this.form.HensousakiKbn2_DHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_DHyo.Text))
                            {
                                this.form.HensousakiKbn3_DHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_DHyo.Enabled = false;
                            this.form.HensousakiKbn1_DHyo.Enabled = false;
                            this.form.HensousakiKbn2_DHyo.Enabled = false;
                            this.form.HensousakiKbn3_DHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                        }
                    }

                    //E票
                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = false;
                        if (this.form.MANIFEST_USE_1_EHyo.Checked)
                        {
                            this.form.HensousakiKbn_EHyo.Enabled = true;
                            this.form.HensousakiKbn1_EHyo.Enabled = true;
                            this.form.HensousakiKbn2_EHyo.Enabled = true;
                            this.form.HensousakiKbn3_EHyo.Enabled = true;

                            if ("1".Equals(this.form.HensousakiKbn_EHyo.Text))
                            {
                                this.form.HensousakiKbn1_EHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                            }
                            else if ("2".Equals(this.form.HensousakiKbn_EHyo.Text))
                            {
                                this.form.HensousakiKbn2_EHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                            }
                            else if ("3".Equals(this.form.HensousakiKbn_EHyo.Text))
                            {
                                this.form.HensousakiKbn3_EHyo.Checked = true;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                                this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_EHyo.Enabled = false;
                            this.form.HensousakiKbn1_EHyo.Enabled = false;
                            this.form.HensousakiKbn2_EHyo.Enabled = false;
                            this.form.HensousakiKbn3_EHyo.Enabled = false;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;

                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiCheckOffCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを使用区分の選択の状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        internal bool SetEnabledManifestHensousakiKbnRendou(string hyoName)
        {
            try
            {
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //ラジオボタン
                HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
                HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
                HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

                //テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);
                MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

                //非表示タブは設定なし
                if (HensousakiKbn == null)
                {
                    return false;
                }
                else
                {
                    ManiHensousakiTorihikisakiCode.Text = string.Empty;
                    ManiHensousakiGyoushaCode.Text = string.Empty;
                    ManiHensousakiGenbaCode.Text = string.Empty;
                    ManiHensousakiTorihikisakiName.Text = string.Empty;
                    ManiHensousakiGyoushaName.Text = string.Empty;
                    ManiHensousakiGenbaName.Text = string.Empty;

                    if ("1" == HensousakiKbn.Text)
                    {
                        HensousakiKbn1.Checked = true;
                        ManiHensousakiTorihikisakiCode.Enabled = true;
                        ManiHensousakiGyoushaCode.Enabled = false;
                        ManiHensousakiGenbaCode.Enabled = false;
                    }
                    else if ("2" == HensousakiKbn.Text)
                    {
                        // 2017/06/22 DIQ 標準修正 #100122 START
                        // 返送先区分が「2．業者」の場合、マニフェスト返送先になっている業者のみを抽出・入力できるようにする。
                        r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                        dto.And_Or = CONDITION_OPERATOR.AND;
                        dto.KeyName = "TEKIYOU_FLG";
                        dto.Value = "FALSE";
                        r_framework.Dto.PopupSearchSendParamDto dto_2 = new r_framework.Dto.PopupSearchSendParamDto();
                        dto_2.And_Or = CONDITION_OPERATOR.AND;
                        dto_2.KeyName = "MANI_HENSOUSAKI_KBN";
                        dto_2.Value = "TRUE";
                        this.ManiHensousakiGyoushaCode.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                        this.ManiHensousakiGyoushaCode.PopupSearchSendParams.Add(dto);
                        this.ManiHensousakiGyoushaCode.PopupSearchSendParams.Add(dto_2);
                        // 2017/06/22 DIQ 標準修正 #100122 END
                        HensousakiKbn2.Checked = true;
                        ManiHensousakiTorihikisakiCode.Enabled = false;
                        ManiHensousakiGyoushaCode.Enabled = true;
                        ManiHensousakiGenbaCode.Enabled = false;
                    }
                    else if ("3" == HensousakiKbn.Text)
                    {
                        // 2017/06/22 DIQ 標準修正 #100122 START
                        // 返送先区分が「3．現場」の場合、全ての業者を入力できるようにする。
                        r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                        dto.And_Or = CONDITION_OPERATOR.AND;
                        dto.KeyName = "TEKIYOU_FLG";
                        dto.Value = "FALSE";
                        this.ManiHensousakiGyoushaCode.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>();
                        this.ManiHensousakiGyoushaCode.PopupSearchSendParams.Add(dto);
                        // 2017/06/22 DIQ 標準修正 #100122 END
                        HensousakiKbn3.Checked = true;
                        ManiHensousakiTorihikisakiCode.Enabled = false;
                        ManiHensousakiGyoushaCode.Enabled = true;
                        ManiHensousakiGenbaCode.Enabled = true;
                    }
                    else
                    {
                        ManiHensousakiTorihikisakiCode.Enabled = false;
                        ManiHensousakiGyoushaCode.Enabled = false;
                        ManiHensousakiGenbaCode.Enabled = false;
                    }
                    ManiHensousakiTorihikisakiCode.IsInputErrorOccured = false;
                    ManiHensousakiGyoushaCode.IsInputErrorOccured = false;
                    ManiHensousakiGenbaCode.IsInputErrorOccured = false;
                    ManiHensousakiTorihikisakiCode.UpdateBackColor();
                    ManiHensousakiGyoushaCode.UpdateBackColor();
                    ManiHensousakiGenbaCode.UpdateBackColor();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetEnabledManifestHensousakiKbnRendou", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールをマニフェスト返送先のチェックの状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        /// <param name="manifestHensousaki">マニフェスト返送先のチェック状態</param>
        internal bool SetEnabledManifestHensousakiRendou(string hyoName, bool manifestHensousaki)
        {
            try
            {
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //ラジオボタン
                HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
                HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
                HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
                HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
                HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

                //テキストボックス
                HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);
                MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

                //非表示タブは設定なし
                if (HensousakiKbn == null)
                {
                    return false;
                }
                else
                {
                    if (manifestHensousaki)
                    {
                        // 返送先等が使用可
                        HENSOUSAKI_PLACE_KBN.Enabled = true;
                        HENSOUSAKI_PLACE_KBN_1.Enabled = true;
                        HENSOUSAKI_PLACE_KBN_2.Enabled = true;
                        if (HENSOUSAKI_PLACE_KBN_1.Checked)
                        {
                            this.HensousakiKbn.Enabled = false;
                            this.HensousakiKbn1.Enabled = false;
                            this.HensousakiKbn2.Enabled = false;
                            this.HensousakiKbn3.Enabled = false;
                            this.ManiHensousakiTorihikisakiCode.Enabled = false;
                            this.ManiHensousakiGyoushaCode.Enabled = false;
                            this.ManiHensousakiGenbaCode.Enabled = false;

                            ManiHensousakiTorihikisakiCode.Text = string.Empty;
                            ManiHensousakiTorihikisakiName.Text = string.Empty;
                            ManiHensousakiGyoushaCode.Text = string.Empty;
                            ManiHensousakiGyoushaName.Text = string.Empty;
                            ManiHensousakiGenbaCode.Text = string.Empty;
                            ManiHensousakiGenbaName.Text = string.Empty;
                        }
                        else
                        {
                            this.HensousakiKbn.Enabled = true;
                            this.HensousakiKbn1.Enabled = true;
                            this.HensousakiKbn2.Enabled = true;
                            this.HensousakiKbn3.Enabled = true;
                            this.ManiHensousakiTorihikisakiCode.Enabled = false;
                            this.ManiHensousakiGyoushaCode.Enabled = false;
                            this.ManiHensousakiGenbaCode.Enabled = false;
                        }
                    }
                    else
                    {
                        HENSOUSAKI_PLACE_KBN.Enabled = false;
                        HENSOUSAKI_PLACE_KBN_1.Enabled = false;
                        HENSOUSAKI_PLACE_KBN_2.Enabled = false;

                        if ("1".Equals(MANIFEST_USE.Text))
                        {
                            this.HensousakiKbn.Enabled = true;
                            this.HensousakiKbn1.Enabled = true;
                            this.HensousakiKbn2.Enabled = true;
                            this.HensousakiKbn3.Enabled = true;
                            if (HensousakiKbn1.Checked)
                            {
                                this.ManiHensousakiTorihikisakiCode.Enabled = true;
                            }
                            else if (HensousakiKbn2.Checked)
                            {
                                this.ManiHensousakiGyoushaCode.Enabled = true;
                            }
                            else if (HensousakiKbn3.Checked)
                            {
                                this.ManiHensousakiGenbaCode.Enabled = true;
                            }
                        }
                        else
                        {
                            HensousakiKbn1.Checked = true;
                            ManiHensousakiTorihikisakiCode.Text = string.Empty;
                            ManiHensousakiTorihikisakiName.Text = string.Empty;
                            HensousakiKbn2.Checked = false;
                            ManiHensousakiGyoushaCode.Text = string.Empty;
                            ManiHensousakiGyoushaName.Text = string.Empty;
                            HensousakiKbn3.Checked = false;
                            ManiHensousakiGenbaCode.Text = string.Empty;
                            ManiHensousakiGenbaName.Text = string.Empty;

                            this.HensousakiKbn.Enabled = false;
                            this.HensousakiKbn1.Enabled = false;
                            this.HensousakiKbn2.Enabled = false;
                            this.HensousakiKbn3.Enabled = false;
                            this.ManiHensousakiTorihikisakiCode.Enabled = false;
                            this.ManiHensousakiGyoushaCode.Enabled = false;
                            this.ManiHensousakiGenbaCode.Enabled = false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetEnabledManifestHensousakiRendou", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを全て使用不可にします
        /// </summary>
        /// <param name="hyoName">票</param>
        internal bool SetEnabledFalseManifestHensousaki(string hyoName)
        {
            try
            {
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //ラジオボタン
                HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
                HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
                HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
                HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
                HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

                //テキストボックス
                HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);
                MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

                //非表示タブは設定なし
                if (HensousakiKbn == null)
                {
                    return false;
                }
                else
                {
                    if (this.form.ManiHensousakiKbn.Checked)
                    {
                        HENSOUSAKI_PLACE_KBN.Text = "1";
                        HENSOUSAKI_PLACE_KBN_1.Checked = true;
                        HENSOUSAKI_PLACE_KBN_2.Checked = false;
                    }
                    HensousakiKbn.Text = "1";
                    HensousakiKbn1.Checked = true;
                    ManiHensousakiTorihikisakiCode.Text = string.Empty;
                    ManiHensousakiTorihikisakiName.Text = string.Empty;
                    HensousakiKbn2.Checked = false;
                    ManiHensousakiGyoushaCode.Text = string.Empty;
                    ManiHensousakiGyoushaName.Text = string.Empty;
                    HensousakiKbn3.Checked = false;
                    ManiHensousakiGenbaCode.Text = string.Empty;
                    ManiHensousakiGenbaName.Text = string.Empty;

                    HENSOUSAKI_PLACE_KBN.Enabled = false;
                    HENSOUSAKI_PLACE_KBN_1.Enabled = false;
                    HENSOUSAKI_PLACE_KBN_2.Enabled = false;
                    this.HensousakiKbn.Enabled = false;
                    this.HensousakiKbn1.Enabled = false;
                    this.HensousakiKbn2.Enabled = false;
                    this.HensousakiKbn3.Enabled = false;
                    this.ManiHensousakiTorihikisakiCode.Enabled = false;
                    this.ManiHensousakiGyoushaCode.Enabled = false;
                    this.ManiHensousakiGenbaCode.Enabled = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetEnabledFalseManifestHensousaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// サーチ
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            return 0;
        }

        /// <summary>
        /// 業者情報コピー処理
        /// </summary>
        public bool GyousyaCopy()
        {
            try
            {
                string inputCd = this.GyoushaCd;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "業者CD");
                    return false;
                }
                else
                {
                    string zeroPadCd = this.ZeroPadding(inputCd);
                    this.GyousyaSetting(zeroPadCd);
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GyousyaCopy", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("GyousyaCopy", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 業者コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された業者CD</param>
        private void GyousyaSetting(string inputCd)
        {
            M_GYOUSHA gyousyaEntity = this.daoGyousha.GetDataByCd(inputCd);

            if (gyousyaEntity != null)
            {
                // 共通部分
                if (!gyousyaEntity.KYOTEN_CD.IsNull)
                {
                    this.form.KyotenCode.Text = gyousyaEntity.KYOTEN_CD.ToString();                    // 拠点CD
                    M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(gyousyaEntity.KYOTEN_CD.ToString());
                    if (kyoten == null)
                    {
                        this.form.KyotenCode.Text = string.Empty;
                        this.form.KyotenName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;                          // 拠点名
                    }
                }

                this.form.GenbaName1.Text = gyousyaEntity.GYOUSHA_NAME1;                               // 名１
                this.form.GenbaName2.Text = gyousyaEntity.GYOUSHA_NAME2;                               // 名２

                if (gyousyaEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TekiyouKikanForm.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanForm.Value = gyousyaEntity.TEKIYOU_BEGIN;
                }
                if (gyousyaEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TekiyouKikanTo.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanTo.Value = gyousyaEntity.TEKIYOU_END;
                }

                this.form.ChuusiRiyuu1.Text = gyousyaEntity.CHUUSHI_RIYUU1;
                this.form.ChuusiRiyuu2.Text = gyousyaEntity.CHUUSHI_RIYUU2;
                this.form.ShokuchiKbn.Checked = gyousyaEntity.SHOKUCHI_KBN.IsTrue;
                this.form.JishaKbn.Checked = gyousyaEntity.JISHA_KBN.IsTrue;

                //20250320
                this.form.CHIZU.Text = gyoushaEntity.CHIZU;

                this.form.GenbaKeishou1.Text = gyousyaEntity.GYOUSHA_KEISHOU1;                         // 敬称１
                this.form.GenbaKeishou2.Text = gyousyaEntity.GYOUSHA_KEISHOU2;                         // 敬称２

                this.form.GenbaFurigana.Text = gyousyaEntity.GYOUSHA_FURIGANA;                         // フリガナ(不要な可能性有)
                this.form.GenbaNameRyaku.Text = gyousyaEntity.GYOUSHA_NAME_RYAKU;                      // 略称(不要な可能性有)

                this.form.GenbaTel.Text = gyousyaEntity.GYOUSHA_TEL;                                   // 電話
                this.form.GenbaFax.Text = gyousyaEntity.GYOUSHA_FAX;                                   // FAX
                this.form.GenbaKeitaiTel.Text = gyousyaEntity.GYOUSHA_KEITAI_TEL;                　    // 携帯
                this.form.EigyouTantouBushoCode.Text = gyousyaEntity.EIGYOU_TANTOU_BUSHO_CD;           // 営業担当部署CD
                M_BUSHO busho = this.daoBusho.GetDataByCd(gyousyaEntity.EIGYOU_TANTOU_BUSHO_CD);
                if (busho == null)
                {
                    this.form.EigyouTantouBushoCode.Text = string.Empty;
                    this.form.EigyouTantouBushoName.Text = string.Empty;
                }
                else
                {
                    this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU;                     // 営業担当部署名
                }
                this.form.EigyouCode.Text = gyousyaEntity.EIGYOU_TANTOU_CD;                            // 営業担当者CD
                M_SHAIN shain = this.daoShain.GetDataByCd(gyousyaEntity.EIGYOU_TANTOU_CD);
                if (shain == null)
                {
                    this.form.EigyouCode.Text = string.Empty;
                    this.form.EigyouName.Text = string.Empty;
                }
                else
                {
                    this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU;                                // 営業担当者名
                }

                // 基本情報タブ
                this.form.GenbaPost.Text = gyousyaEntity.GYOUSHA_POST;                                 // 郵便番号

                if (!gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    this.form.GenbaTodoufukenCode.Text = this.ZeroPadding_Ken(gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());    // 都道府県CD
                    M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (temp == null)
                    {
                        this.form.GenbaTodoufukenCode.Text = string.Empty;
                        this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME;                // 都道府県名
                    }
                }

                this.form.GenbaAddress1.Text = gyousyaEntity.GYOUSHA_ADDRESS1;                         // 住所１
                this.form.GenbaAddress2.Text = gyousyaEntity.GYOUSHA_ADDRESS2;                         // 住所２

                // 地域取得
                string chiikiName = string.Empty;
                string chiikiCd = gyoushaEntity.CHIIKI_CD;
                var m_chiikiCd = daoChiiki.GetDataByCd(chiikiCd);
                if (m_chiikiCd != null)
                {
                    chiikiName = m_chiikiCd.CHIIKI_NAME;
                }

                if (!string.IsNullOrWhiteSpace(chiikiCd))
                {
                    this.form.ChiikiCode.Text = chiikiCd;                                              // 地域CD
                    this.form.ChiikiName.Text = chiikiName;                                            // 地域名
                }

                this.form.BushoCode.Text = gyousyaEntity.BUSHO;                                        // 部署
                this.form.TantoushaCode.Text = gyousyaEntity.TANTOUSHA;                                // 担当者
                this.form.ShuukeiItemCode.Text = gyousyaEntity.SHUUKEI_ITEM_CD;                        // 集計項目CD
                M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(gyousyaEntity.SHUUKEI_ITEM_CD);
                if (shuukei == null)
                {
                    this.form.ShuukeiItemCode.Text = string.Empty;
                    this.form.ShuukeiItemName.Text = string.Empty;
                }
                else
                {
                    this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU;               // 集計項目名
                }
                this.form.GyoushuCode.Text = gyousyaEntity.GYOUSHU_CD;                                 // 業種CD
                M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(gyousyaEntity.GYOUSHU_CD);
                if (gyoushu == null)
                {
                    this.form.GyoushuCode.Text = string.Empty;
                    this.form.GyoushuName.Text = string.Empty;
                }
                else
                {
                    this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU;                           // 業種名
                }
                this.form.Bikou1.Text = gyousyaEntity.BIKOU1;                                          // 備考１
                this.form.Bikou2.Text = gyousyaEntity.BIKOU2;                                          // 備考２
                this.form.Bikou3.Text = gyousyaEntity.BIKOU3;                                          // 備考３
                this.form.Bikou4.Text = gyousyaEntity.BIKOU4;                                          // 備考４

                // 現場分類タブ
                this.form.HaishutsuKbn.Checked = gyousyaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue;
                this.form.ShobunJigyoujouKbn.Checked = gyousyaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                this.form.ManiHensousakiKbn.Checked = gyousyaEntity.MANI_HENSOUSAKI_KBN.IsTrue;     // マニフェスト返送先
                if (gyousyaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                }
                else if (gyousyaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }

                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.ManiHensousakiName1.Text = gyousyaEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.ManiHensousakiName2.Text = gyousyaEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.ManiHensousakiKeishou1.Text = gyousyaEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.ManiHensousakiKeishou2.Text = gyousyaEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.ManiHensousakiPost.Text = gyousyaEntity.MANI_HENSOUSAKI_POST;
                    this.form.ManiHensousakiAddress1.Text = gyousyaEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.ManiHensousakiAddress2.Text = gyousyaEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.ManiHensousakiBusho.Text = gyousyaEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.ManiHensousakiTantou.Text = gyousyaEntity.MANI_HENSOUSAKI_TANTOU;
                }
                else
                {
                    this.form.ManiHensousakiName1.Text = string.Empty;
                    this.form.ManiHensousakiName2.Text = string.Empty;
                    this.form.ManiHensousakiKeishou1.Text = string.Empty;
                    this.form.ManiHensousakiKeishou2.Text = string.Empty;
                    this.form.ManiHensousakiPost.Text = string.Empty;
                    this.form.ManiHensousakiAddress1.Text = string.Empty;
                    this.form.ManiHensousakiAddress2.Text = string.Empty;
                    this.form.ManiHensousakiBusho.Text = string.Empty;
                    this.form.ManiHensousakiTantou.Text = string.Empty;
                }

                if (this.form.ManiHensousakiKbn.Checked)
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_A)
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "1";
                            this.form.MANIFEST_USE_1_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B1)
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B2)
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B4)
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B6)
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_C1)
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_C2)
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_D)
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "1";
                            this.form.MANIFEST_USE_1_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_E)
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "1";
                            this.form.MANIFEST_USE_1_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        }
                    }
                }
                else
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        this.form.MANIFEST_USE_AHyo.Text = "2";
                        this.form.MANIFEST_USE_2_AHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        this.form.MANIFEST_USE_B1Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        this.form.MANIFEST_USE_B2Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        this.form.MANIFEST_USE_B4Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        this.form.MANIFEST_USE_B6Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        this.form.MANIFEST_USE_C1Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        this.form.MANIFEST_USE_C2Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        this.form.MANIFEST_USE_DHyo.Text = "2";
                        this.form.MANIFEST_USE_2_DHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        this.form.MANIFEST_USE_EHyo.Text = "2";
                        this.form.MANIFEST_USE_2_EHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                    }
                }
            }

            bool catchErr = false;
            // 請求情報タブ
            this.form.SeikyuushoSoufusaki1.Text = gyousyaEntity.SEIKYUU_SOUFU_NAME1;                            // 請求書送付先1
            this.form.SeikyuuSouhuKeishou1.Text = gyousyaEntity.SEIKYUU_SOUFU_KEISHOU1;                         // 請求書送付先敬称1
            this.form.SeikyuushoSoufusaki2.Text = gyousyaEntity.SEIKYUU_SOUFU_NAME2;                            // 請求書送付先2
            this.form.SeikyuuSouhuKeishou2.Text = gyousyaEntity.SEIKYUU_SOUFU_KEISHOU2;                         // 請求書送付先敬称2
            this.form.SeikyuuSoufuPost.Text = gyousyaEntity.SEIKYUU_SOUFU_POST;                                 // 送付先郵便番号
            this.form.SeikyuuSoufuAddress1.Text = gyousyaEntity.SEIKYUU_SOUFU_ADDRESS1;                         // 送付先住所１
            this.form.SeikyuuSoufuAddress2.Text = gyousyaEntity.SEIKYUU_SOUFU_ADDRESS2;                         // 送付先住所２
            this.form.SeikyuuSoufuBusho.Text = gyousyaEntity.SEIKYUU_SOUFU_BUSHO;                               // 送付先部署
            this.form.SeikyuuSoufuTantou.Text = gyousyaEntity.SEIKYUU_SOUFU_TANTOU;                             // 送付先担当者
            this.form.SoufuGenbaTel.Text = gyousyaEntity.SEIKYUU_SOUFU_TEL;                                     // 送付先電話番号
            this.form.SoufuGenbaFax.Text = gyousyaEntity.SEIKYUU_SOUFU_FAX;                                     // 送付先FAX番号
            this.form.SeikyuuTantou.Text = gyousyaEntity.SEIKYUU_TANTOU;                                        // 請求担当者
            if (!gyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
            {
                this.form.SeikyuuDaihyouPrintKbn.Text = gyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();     // 代表取締役を印字
            }
            if (!gyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
            {
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = gyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();    // 拠点名を印字
            }
            if (!gyousyaEntity.SEIKYUU_KYOTEN_CD.IsNull)
            {
                this.form.SEIKYUU_KYOTEN_CD.Text = gyousyaEntity.SEIKYUU_KYOTEN_CD.ToString();                  // 請求書拠点
                this.SeikyuuKyotenCdValidated(out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }

            // Begin: LANDUONG - 20220215 - refs#160054
            this.HakkousakuAndRakurakuCDCheck();
            // End: LANDUONG - 20220215 - refs#160054

            // 支払情報タブ
            this.form.ShiharaiSoufuName1.Text = gyousyaEntity.SHIHARAI_SOUFU_NAME1;		                        // 支払明細書送付先1
            this.form.ShiharaiSoufuKeishou1.Text = gyousyaEntity.SHIHARAI_SOUFU_KEISHOU1;  	                    // 支払明細書送付先敬称1
            this.form.ShiharaiSoufuName2.Text = gyousyaEntity.SHIHARAI_SOUFU_NAME2;		                        // 支払明細書送付先2
            this.form.ShiharaiSoufuKeishou2.Text = gyousyaEntity.SHIHARAI_SOUFU_KEISHOU2;	                    // 支払明細書送付先敬称2
            this.form.ShiharaiSoufuPost.Text = gyousyaEntity.SHIHARAI_SOUFU_POST;		                        // 送付先郵便番号
            this.form.ShiharaiSoufuAddress1.Text = gyousyaEntity.SHIHARAI_SOUFU_ADDRESS1;	                    // 送付先住所１
            this.form.ShiharaiSoufuAddress2.Text = gyousyaEntity.SHIHARAI_SOUFU_ADDRESS2;  	                    // 送付先住所２
            this.form.ShiharaiSoufuBusho.Text = gyousyaEntity.SHIHARAI_SOUFU_BUSHO;		                        // 送付先部署
            this.form.ShiharaiSoufuTantou.Text = gyousyaEntity.SHIHARAI_SOUFU_TANTOU;		                    // 送付先担当者
            this.form.ShiharaiGenbaTel.Text = gyousyaEntity.SHIHARAI_SOUFU_TEL;			                        // 送付先電話番号
            this.form.ShiharaiGenbaFax.Text = gyousyaEntity.SHIHARAI_SOUFU_FAX;			                        // 送付先FAX番号
            if (!gyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
            {
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = gyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString();  //拠点名を印字
            }
            if (!gyousyaEntity.SHIHARAI_KYOTEN_CD.IsNull)
            {
                this.form.SHIHARAI_KYOTEN_CD.Text = gyousyaEntity.SHIHARAI_KYOTEN_CD.ToString();                //支払書拠点
                this.ShiharaiKyotenCdValidated(out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
        }

        /// <summary>
        /// 取引先情報コピー処理
        /// </summary>
        public bool TorihikisakiCopy()
        {
            try
            {
                string inputCd = this.TorihikisakiCd;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "取引先CD");
                    return false;
                }
                else
                {
                    string zeroPadCd = this.ZeroPadding(inputCd);
                    this.TorihikisakiSetting(zeroPadCd);
                }
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("TorihikisakiCopy", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 取引先コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        private void TorihikisakiSetting(string inputCd)
        {
            M_TORIHIKISAKI torisakiEntity = this.daoTorisaki.GetDataByCd(inputCd);
            M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(inputCd);
            M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(inputCd);

            if (torisakiEntity != null)
            {
                // 共通部分
                if (!torisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.form.KyotenCode.Text = torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();               // 拠点CD
                    M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                    if (kyoten == null)
                    {
                        this.form.KyotenCode.Text = string.Empty;
                        this.form.KyotenName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU;                                   // 拠点名
                    }
                }

                this.form.GenbaName1.Text = torisakiEntity.TORIHIKISAKI_NAME1;                                  // 取引先名１
                this.form.GenbaName2.Text = torisakiEntity.TORIHIKISAKI_NAME2;                                  // 取引先名２

                if (torisakiEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TekiyouKikanForm.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanForm.Value = torisakiEntity.TEKIYOU_BEGIN;
                }
                if (torisakiEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TekiyouKikanTo.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanTo.Value = torisakiEntity.TEKIYOU_END;
                }

                this.form.ChuusiRiyuu1.Text = torisakiEntity.CHUUSHI_RIYUU1;
                this.form.ChuusiRiyuu2.Text = torisakiEntity.CHUUSHI_RIYUU2;
                this.form.ShokuchiKbn.Checked = torisakiEntity.SHOKUCHI_KBN.IsTrue;

                //20250320
                this.form.CHIZU.Text = torihikisakiEntity.CHIZU;

                this.form.GenbaKeishou1.Text = torisakiEntity.TORIHIKISAKI_KEISHOU1;                            // 敬称１
                this.form.GenbaKeishou2.Text = torisakiEntity.TORIHIKISAKI_KEISHOU2;                            // 敬称２

                this.form.GenbaFurigana.Text = torisakiEntity.TORIHIKISAKI_FURIGANA;                            // フリガナ(不要な可能性有)
                this.form.GenbaNameRyaku.Text = torisakiEntity.TORIHIKISAKI_NAME_RYAKU;                         // 略称(不要な可能性有)

                this.form.GenbaTel.Text = torisakiEntity.TORIHIKISAKI_TEL;                                      // 電話
                this.form.GenbaFax.Text = torisakiEntity.TORIHIKISAKI_FAX;                                      // FAX
                this.form.EigyouTantouBushoCode.Text = torisakiEntity.EIGYOU_TANTOU_BUSHO_CD;                   // 営業担当部署CD
                M_BUSHO busho = this.daoBusho.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_BUSHO_CD);
                if (busho == null)
                {
                    this.form.EigyouTantouBushoCode.Text = string.Empty;
                    this.form.EigyouTantouBushoName.Text = string.Empty;
                }
                else
                {
                    this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU;                              // 営業担当部署名
                }
                this.form.EigyouCode.Text = torisakiEntity.EIGYOU_TANTOU_CD;                                    // 営業担当者CD
                M_SHAIN shain = this.daoShain.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_CD);
                if (shain == null)
                {
                    this.form.EigyouCode.Text = string.Empty;
                    this.form.EigyouName.Text = string.Empty;
                }
                else
                {
                    this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU;                                         // 営業担当者名
                }

                // 基本情報タブ
                this.form.GenbaPost.Text = torisakiEntity.TORIHIKISAKI_POST;                                    // 郵便番号

                if (!torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    this.form.GenbaTodoufukenCode.Text = this.ZeroPadding_Ken(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());    // 都道府県CD
                    M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (temp == null)
                    {
                        this.form.GenbaTodoufukenCode.Text = string.Empty;
                        this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME;                         // 都道府県名
                    }
                }

                this.form.GenbaAddress1.Text = torisakiEntity.TORIHIKISAKI_ADDRESS1;                            // 住所１
                this.form.GenbaAddress2.Text = torisakiEntity.TORIHIKISAKI_ADDRESS2;                            // 住所２

                // 地域の判定は関数に任せる
                if (this.ChechChiiki(true))
                {
                    this.form.ChiikiCode.Text = string.Empty;                                                   // 地域CD
                    this.form.ChiikiName.Text = string.Empty;                                                   // 地域名
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;                           // 運搬報告書提出先地域CD
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;                         // 運搬報告書提出先地域名
                }

                this.form.BushoCode.Text = torisakiEntity.BUSHO;                                                // 部署
                this.form.TantoushaCode.Text = torisakiEntity.TANTOUSHA;                                        // 担当者
                this.form.ShuukeiItemCode.Text = torisakiEntity.SHUUKEI_ITEM_CD;                                // 集計項目CD
                M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(torisakiEntity.SHUUKEI_ITEM_CD);
                if (shuukei == null)
                {
                    this.form.ShuukeiItemCode.Text = string.Empty;
                    this.form.ShuukeiItemName.Text = string.Empty;
                }
                else
                {
                    this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU;                        // 集計項目名
                }
                this.form.GyoushuCode.Text = torisakiEntity.GYOUSHU_CD;                                         // 業種CD
                M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(torisakiEntity.GYOUSHU_CD);
                if (gyoushu == null)
                {
                    this.form.GyoushuCode.Text = string.Empty;
                    this.form.GyoushuName.Text = string.Empty;
                }
                else
                {
                    this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU;                                    // 業種名
                }
                this.form.Bikou1.Text = torisakiEntity.BIKOU1;                                                  // 備考１
                this.form.Bikou2.Text = torisakiEntity.BIKOU2;                                                  // 備考２
                this.form.Bikou3.Text = torisakiEntity.BIKOU3;                                                  // 備考３
                this.form.Bikou4.Text = torisakiEntity.BIKOU4;                                                  // 備考４

                // 現場分類タブ
                this.form.ManiHensousakiKbn.Checked = torisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue;             // マニフェスト返送先

                if (torisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                }
                else if (torisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "1";
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }

                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.ManiHensousakiName1.Text = torisakiEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.ManiHensousakiName2.Text = torisakiEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.ManiHensousakiKeishou1.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.ManiHensousakiKeishou2.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.ManiHensousakiPost.Text = torisakiEntity.MANI_HENSOUSAKI_POST;
                    this.form.ManiHensousakiAddress1.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.ManiHensousakiAddress2.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.ManiHensousakiBusho.Text = torisakiEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.ManiHensousakiTantou.Text = torisakiEntity.MANI_HENSOUSAKI_TANTOU;
                }
                else
                {
                    this.form.ManiHensousakiName1.Text = string.Empty;
                    this.form.ManiHensousakiName2.Text = string.Empty;
                    this.form.ManiHensousakiKeishou1.Text = string.Empty;
                    this.form.ManiHensousakiKeishou2.Text = string.Empty;
                    this.form.ManiHensousakiPost.Text = string.Empty;
                    this.form.ManiHensousakiAddress1.Text = string.Empty;
                    this.form.ManiHensousakiAddress2.Text = string.Empty;
                    this.form.ManiHensousakiBusho.Text = string.Empty;
                    this.form.ManiHensousakiTantou.Text = string.Empty;
                }

                if (this.form.ManiHensousakiKbn.Checked)
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_A)
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "1";
                            this.form.MANIFEST_USE_1_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        }
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B1)
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B2)
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B4)
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_B6)
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_C1)
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_C2)
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "1";
                            this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_D)
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "1";
                            this.form.MANIFEST_USE_1_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        }
                    }
                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        if (1 == this.sysinfoEntity.MANIFEST_USE_E)
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "1";
                            this.form.MANIFEST_USE_1_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        }
                        else
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        }
                    }
                }
                else
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        this.form.MANIFEST_USE_AHyo.Text = "2";
                        this.form.MANIFEST_USE_2_AHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        this.form.MANIFEST_USE_B1Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        this.form.MANIFEST_USE_B2Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        this.form.MANIFEST_USE_B4Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        this.form.MANIFEST_USE_B6Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        this.form.MANIFEST_USE_C1Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        this.form.MANIFEST_USE_C2Hyo.Text = "2";
                        this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        this.form.MANIFEST_USE_DHyo.Text = "2";
                        this.form.MANIFEST_USE_2_DHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        this.form.MANIFEST_USE_EHyo.Text = "2";
                        this.form.MANIFEST_USE_2_EHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                    }
                }
            }

            bool catchErr = false;
            if (seikyuuEntity != null)
            {
                // 請求情報タブ
                this.form.SeikyuushoSoufusaki1.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME1;                            // 請求書送付先1
                this.form.SeikyuuSouhuKeishou1.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU1;                         // 請求書送付先敬称1
                this.form.SeikyuushoSoufusaki2.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME2;                            // 請求書送付先2
                this.form.SeikyuuSouhuKeishou2.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU2;                         // 請求書送付先敬称2
                this.form.SeikyuuSoufuPost.Text = seikyuuEntity.SEIKYUU_SOUFU_POST;                                 // 送付先郵便番号
                this.form.SeikyuuSoufuAddress1.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS1;                         // 送付先住所１
                this.form.SeikyuuSoufuAddress2.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS2;                         // 送付先住所２
                this.form.SeikyuuSoufuBusho.Text = seikyuuEntity.SEIKYUU_SOUFU_BUSHO;                               // 送付先部署
                this.form.SeikyuuSoufuTantou.Text = seikyuuEntity.SEIKYUU_SOUFU_TANTOU;                             // 送付先担当者
                this.form.SoufuGenbaTel.Text = seikyuuEntity.SEIKYUU_SOUFU_TEL;                                     // 送付先電話番号
                this.form.SoufuGenbaFax.Text = seikyuuEntity.SEIKYUU_SOUFU_FAX;                                     // 送付先FAX番号
                this.form.SeikyuuTantou.Text = seikyuuEntity.SEIKYUU_TANTOU;                                        // 請求担当者
                if (!seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SeikyuuDaihyouPrintKbn.Text = seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();     // 代表取締役を印字
                }
                if (!seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();    // 拠点名を印字
                }
                if (!seikyuuEntity.SEIKYUU_KYOTEN_CD.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = seikyuuEntity.SEIKYUU_KYOTEN_CD.ToString();                  // 請求書拠点
                    this.SeikyuuKyotenCdValidated(out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                }
            }

            // Begin: LANDUONG - 20220215 - refs#160054
            this.HakkousakuAndRakurakuCDCheck();
            // End: LANDUONG - 20220215 - refs#160054

            if (shiharaiEntity != null)
            {
                // 支払情報タブ
                this.form.ShiharaiSoufuName1.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME1;		                    // 支払明細書送付先1
                this.form.ShiharaiSoufuKeishou1.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU1;  	                // 支払明細書送付先敬称1
                this.form.ShiharaiSoufuName2.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME2;		                    // 支払明細書送付先2
                this.form.ShiharaiSoufuKeishou2.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU2;	                    // 支払明細書送付先敬称2
                this.form.ShiharaiSoufuPost.Text = shiharaiEntity.SHIHARAI_SOUFU_POST;		                        // 送付先郵便番号
                this.form.ShiharaiSoufuAddress1.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS1;	                    // 送付先住所１
                this.form.ShiharaiSoufuAddress2.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS2;  	                // 送付先住所２
                this.form.ShiharaiSoufuBusho.Text = shiharaiEntity.SHIHARAI_SOUFU_BUSHO;		                    // 送付先部署
                this.form.ShiharaiSoufuTantou.Text = shiharaiEntity.SHIHARAI_SOUFU_TANTOU;		                    // 送付先担当者
                this.form.ShiharaiGenbaTel.Text = shiharaiEntity.SHIHARAI_SOUFU_TEL;			                    // 送付先電話番号
                this.form.ShiharaiGenbaFax.Text = shiharaiEntity.SHIHARAI_SOUFU_FAX;			                    // 送付先FAX番号
                if (!shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); //拠点名を印字
                }
                if (!shiharaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = shiharaiEntity.SHIHARAI_KYOTEN_CD.ToString();               //支払書拠点
                    this.ShiharaiKyotenCdValidated(out catchErr);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                }
            }
        }

        /// <summary>
        /// 地域CD検索処理
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_TORIHIKISAKI torisakiEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            // ①M_TORIHIKISAKI.TORIHIKISAKI_POST, M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1が空の場合は空を設定し、処理を終了する。
            string tempPost = torisakiEntity.TORIHIKISAKI_POST;
            string tempAddress = torisakiEntity.TORIHIKISAKI_ADDRESS1;
            if (string.IsNullOrWhiteSpace(tempPost) && string.IsNullOrWhiteSpace(tempAddress))
            {
                return result;
            }

            // ②M_TORIHIKISAKI.TORIHIKISAKI_POSTで郵便辞書を検索し、都道府県と市町村を取得する。
            IS_ZIP_CODEDao daoPost = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
            S_ZIP_CODE[] postNum = daoPost.GetDataByPost7LikeSearch(tempPost);
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenBypost = postNum[0].TODOUFUKEN;
            string sityousonBypost = postNum[0].SIKUCHOUSON;

            M_CHIIKI condition = new M_CHIIKI();

            DataTable dtChiiki = new DataTable();

            // 市町村がＮＵＬＬ以外の場合、市町村で検索する
            if (!string.IsNullOrWhiteSpace(sityousonBypost))
            {
                // ③②の検索結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = sityousonBypost;

                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);

                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(todoufukenBypost))
            {
                // ④②の検索結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenBypost;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑤M_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1から郵便辞書を検索し、都道府県と市町村でグループ化を行う。
            postNum = daoPost.GetDataByJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できた場合、処理を行う
            if (postNum.Length >= 1)
            {
                string sityousonByaddress = postNum[0].SIKUCHOUSON + postNum[0].OTHER1;

                // 市町村がＮＵＬＬ以外の場合、市町村で検索する
                if (!string.IsNullOrWhiteSpace(postNum[0].SIKUCHOUSON))
                {
                    // ⑥⑤の検索結果が1件または複数件の場合、結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = sityousonByaddress;
                    dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }

            //都道府県＋市区町村＋OTHER1
            postNum = daoPost.GetDataByTodoufukenJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できない場合、処理を終了する
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenByaddress = postNum[0].TODOUFUKEN + postNum[0].SIKUCHOUSON + postNum[0].OTHER1;
            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(postNum[0].TODOUFUKEN))
            {
                // ⑦⑥の検索結果が0件の場合、結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenByaddress;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑧⑥または⑦の結果が複数件ある場合、空を設定し、処理を終了する。
            // ⑨⑤の検索結果が0件の場合、空を設定し、処理を終了する。
            return result;
        }

        /// <summary>
        /// 地域CD検索処理 業者用
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_GYOUSHA gyosyaEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            // ①M_GYOUSHA.GYOUSHA_POST, M_GYOUSHA.GYOUSHA_ADDRESS1が空の場合は空を設定し、処理を終了する。
            string tempPost = gyosyaEntity.GYOUSHA_POST;
            string tempAddress = gyosyaEntity.GYOUSHA_ADDRESS1;
            if (string.IsNullOrWhiteSpace(tempPost) && string.IsNullOrWhiteSpace(tempAddress))
            {
                return result;
            }

            // ②M_GYOUSHA.GYOUSHA_POSTで郵便辞書を検索し、都道府県と市町村を取得する。
            IS_ZIP_CODEDao daoPost = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
            S_ZIP_CODE[] postNum = daoPost.GetDataByPost7LikeSearch(tempPost);
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenBypost = postNum[0].TODOUFUKEN;
            string sityousonBypost = postNum[0].SIKUCHOUSON;

            M_CHIIKI condition = new M_CHIIKI();

            DataTable dtChiiki = new DataTable();

            // 市町村がＮＵＬＬ以外の場合、市町村で検索する
            if (!string.IsNullOrWhiteSpace(sityousonBypost))
            {
                // ③②の検索結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = sityousonBypost;

                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);

                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(todoufukenBypost))
            {
                // ④②の検索結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                //   結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenBypost;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑤M_GYOUSHA.GYOUSHA_ADDRESS1から郵便辞書を検索し、都道府県と市町村でグループ化を行う。
            postNum = daoPost.GetDataByJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できた場合、処理を行う
            if (postNum.Length >= 1)
            {
                string sityousonByaddress = postNum[0].SIKUCHOUSON + postNum[0].OTHER1;

                // 市町村がＮＵＬＬ以外の場合、市町村で検索する
                if (!string.IsNullOrWhiteSpace(postNum[0].SIKUCHOUSON))
                {
                    // ⑥⑤の検索結果が1件または複数件の場合、結果の市町村よりM_CHIIKI.CHIIKI_NAMEを検索し、
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = sityousonByaddress;
                    dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }

            //都道府県＋市区町村＋OTHER1
            postNum = daoPost.GetDataByTodoufukenJushoLikeSearch(tempAddress);

            // 郵便辞書マスタよりデータが取得できない場合、処理を終了する
            if (postNum.Length <= 0)
            {
                return result;
            }

            string todoufukenByaddress = postNum[0].TODOUFUKEN + postNum[0].SIKUCHOUSON + postNum[0].OTHER1;
            // 都道府県がＮＵＬＬ以外の場合、都道府県で検索する
            if (!string.IsNullOrWhiteSpace(postNum[0].TODOUFUKEN))
            {
                // ⑦⑥の検索結果が0件の場合、結果の都道府県よりM_CHIIKI.CHIIKI_NAMEを検索し、
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenByaddress;
                dtChiiki = daoChiiki.GetDataBySqlFile(GET_CHIIKI_DATA_SQL, condition);
                if (dtChiiki.Rows.Count == 1)
                {
                    chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                    return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                }
            }

            // ⑧⑥または⑦の結果が複数件ある場合、空を設定し、処理を終了する。
            // ⑨⑤の検索結果が0件の場合、空を設定し、処理を終了する。
            return result;
        }

        /// 業者入力表示処理
        /// </summary>
        internal bool ShowGyoushaCreate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowGyoushaCreate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// 取引先入力表示処理
        /// </summary>
        internal bool ShowTorihikisakiCreate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M213", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTorihikisakiCreate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        internal bool ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M218", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.GENBA);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 委託契約入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal bool ShowWindowItaku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                //選択行からキー項目を取得する
                string cd1 = string.Empty;
                string cd2 = string.Empty;
                foreach (Row row in this.form.ItakuKeiyakuIchiran.Rows)
                {
                    if (row.Selected)
                    {
                        cd1 = row.Cells["ITAKU_SYSTEM_ID"].Value.ToString();
                        cd2 = row.Cells["ITAKU_KEIYAKU_TOUROKU_HOUHOU"].Value.ToString();
                        break;
                    }
                }

                //委託契約入力画面を表示する
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, windowType, cd1, cd2);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        r_framework.FormManager.FormManager.OpenFormWithAuth("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1, cd2);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
                else
                {
                    // 1次FW ⇒ 2次FW
                    int iWindowType = (int)windowType;
                    r_framework.Const.WINDOW_TYPE newWindowType = (r_framework.Const.WINDOW_TYPE)iWindowType;

                    r_framework.FormManager.FormManager.OpenFormWithAuth("M001", newWindowType, windowType, cd1, cd2);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowWindowItaku", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //<summary>
        //現場の情報を請求にコピーする
        //</summary>
        public bool CopyToSeikyu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SeikyuushoSoufusaki1.Text = this.form.GenbaName1.Text;
                this.form.SeikyuushoSoufusaki2.Text = this.form.GenbaName2.Text;
                this.form.SeikyuuSouhuKeishou1.Text = this.form.GenbaKeishou1.Text;
                this.form.SeikyuuSouhuKeishou2.Text = this.form.GenbaKeishou2.Text;
                this.form.SeikyuuSoufuPost.Text = this.form.GenbaPost.Text;
                this.form.SeikyuuSoufuAddress1.Text = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text;
                this.form.SeikyuuSoufuAddress2.Text = this.form.GenbaAddress2.Text;
                this.form.SeikyuuSoufuBusho.Text = this.form.BushoCode.Text;
                this.form.SeikyuuSoufuTantou.Text = this.form.TantoushaCode.Text;
                this.form.SoufuGenbaTel.Text = this.form.GenbaTel.Text;
                this.form.SoufuGenbaFax.Text = this.form.GenbaFax.Text;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSeikyu", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //<summary>
        //現場の情報を支払いのコピーする
        //</summary>
        public bool CopyToSiharai()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.ShiharaiSoufuName1.Text = this.form.GenbaName1.Text;
                this.form.ShiharaiSoufuName2.Text = this.form.GenbaName2.Text;
                this.form.ShiharaiSoufuKeishou1.Text = this.form.GenbaKeishou1.Text;
                this.form.ShiharaiSoufuKeishou2.Text = this.form.GenbaKeishou2.Text;
                this.form.ShiharaiSoufuPost.Text = this.form.GenbaPost.Text;
                this.form.ShiharaiSoufuAddress1.Text = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text;
                this.form.ShiharaiSoufuAddress2.Text = this.form.GenbaAddress2.Text;
                this.form.ShiharaiSoufuBusho.Text = this.form.BushoCode.Text;
                this.form.ShiharaiSoufuTantou.Text = this.form.TantoushaCode.Text;
                this.form.ShiharaiGenbaTel.Text = this.form.GenbaTel.Text;
                this.form.ShiharaiGenbaFax.Text = this.form.GenbaFax.Text;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSiharai", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //<summary>
        //現場の情報を分類にコピーする
        //</summary>
        public bool CopyToMani()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.ManiHensousakiName1.Text = this.form.GenbaName1.Text;
                this.form.ManiHensousakiName2.Text = this.form.GenbaName2.Text;
                this.form.ManiHensousakiKeishou1.Text = this.form.GenbaKeishou1.Text;
                this.form.ManiHensousakiKeishou2.Text = this.form.GenbaKeishou2.Text;
                this.form.ManiHensousakiPost.Text = this.form.GenbaPost.Text;
                this.form.ManiHensousakiAddress1.Text = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text;
                this.form.ManiHensousakiAddress2.Text = this.form.GenbaAddress2.Text;
                this.form.ManiHensousakiBusho.Text = this.form.BushoCode.Text;
                this.form.ManiHensousakiTantou.Text = this.form.TantoushaCode.Text;

                bool catchErr = false;
                this.CheckTextBoxLength(this.form.ManiHensousakiAddress1, out catchErr);

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToMani", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// CustomTextBoxの入力文字数をチェックします
        /// </summary>
        /// <param name="txb">CustomTextBox</param>
        /// <returns>true:制限以内, false:制限超過</returns>
        public bool CheckTextBoxLength(CustomTextBox txb, out bool catchErr)
        {
            LogUtility.DebugMethodStart(txb);

            bool res = true;
            catchErr = false;

            try
            {
                var mxLenStr = txb.CharactersNumber.ToString();
                if (mxLenStr.Contains(".") || string.IsNullOrEmpty(txb.Text))
                {
                    // 最大桁数指定がおかしい場合はチェックしない
                    return res;
                }

                int mxLen;
                if (!int.TryParse(mxLenStr, out mxLen) || mxLen < 1)
                {
                    // 最大桁数指定がおかしい場合はチェックしない
                    return res;
                }

                var enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                var bytes = enc.GetBytes(txb.Text);
                var txLen = bytes.Length;
                res = !(mxLen < txLen);

                if (!res)
                {
                    // 制限超過の場合はアラートを表示してコントロールにフォーカス
                    var msl = new MessageBoxShowLogic();
                    msl.MessageBoxShow("E152", txb.DisplayItemName, (mxLen / 2).ToString());
                    txb.Select();
                }

                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTextBoxLength", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return res;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res, catchErr);
            }
        }

        /// <summary>
        /// 地域判定処理
        /// </summary>
        /// <param name="isUnpanHoukokusyoChange">運搬報告書提出先CDを変更するかを示します</param>
        public bool ChechChiiki(bool isUnpanHoukokusyoChange)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 共通地域チェックロジックで地域検索を実行する
                M_CHIIKI chiiki = MasterCommonLogic.SearchChiikiFromAddress(this.form.GenbaTodoufukenCode.Text, this.form.GenbaAddress1.Text);
                if (chiiki != null)
                {
                    this.form.ChiikiCode.Text = chiiki.CHIIKI_CD;
                    this.form.ChiikiName.Text = chiiki.CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.form.ChiikiCode.Text = string.Empty;
                    this.form.ChiikiName.Text = string.Empty;
                }

                // 運搬報告書提出先
                if (isUnpanHoukokusyoChange)
                {
                    M_CHIIKI houkokuChiiki = MasterCommonLogic.SearchChiikiFromAddress(this.form.GenbaTodoufukenCode.Text, string.Empty);
                    if (houkokuChiiki != null)
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = houkokuChiiki.CHIIKI_CD;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = houkokuChiiki.CHIIKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechChiiki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        public bool ChangeSeikyuuKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool catchErr = false;
                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()));
                        SeikyuuKyotenCdValidated(out catchErr);
                    }
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        public void ChangeTorihikisakiKbn(Const.GenbaHoshuConstans.TorihikisakiKbnProcessType torihikisakiKbnProcess, Boolean isKake)
        {
            if (torihikisakiKbnProcess == Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Seikyuu)
            {
                this.ChangeSeikyuuControl(isKake);
            }
            else if (torihikisakiKbnProcess == Const.GenbaHoshuConstans.TorihikisakiKbnProcessType.Siharai)
            {
                this.ChangeSiharaiControl(isKake);
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(請求)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSeikyuuControl(Boolean isKake)
        {
            this.form.SeikyuushoSoufusaki1.Text = string.Empty;     // 請求書送付先1
            this.form.SeikyuushoSoufusaki1.Enabled = isKake;

            this.form.SeikyuuSouhuKeishou1.Text = string.Empty;     // 請求書送付先敬称1
            this.form.SeikyuuSouhuKeishou1.Enabled = isKake;

            this.form.SeikyuushoSoufusaki2.Text = string.Empty;     // 請求書送付先2
            this.form.SeikyuushoSoufusaki2.Enabled = isKake;

            this.form.SeikyuuSouhuKeishou2.Text = string.Empty;     // 請求書送付先敬称2
            this.form.SeikyuuSouhuKeishou2.Enabled = isKake;

            this.form.SeikyuuSoufuPost.Text = string.Empty;         // 送付先郵便番号
            this.form.SeikyuuSoufuPost.Enabled = isKake;

            this.form.SeikyuuSoufuAddress1.Text = string.Empty;     // 送付先住所１
            this.form.SeikyuuSoufuAddress1.Enabled = isKake;

            this.form.SeikyuuSoufuAddress2.Text = string.Empty;     // 送付先住所２
            this.form.SeikyuuSoufuAddress2.Enabled = isKake;

            this.form.SeikyuuSoufuBusho.Text = string.Empty;        // 送付先部署
            this.form.SeikyuuSoufuBusho.Enabled = isKake;

            this.form.SeikyuuSoufuTantou.Text = string.Empty;       // 送付先担当者
            this.form.SeikyuuSoufuTantou.Enabled = isKake;

            this.form.SoufuGenbaTel.Text = string.Empty;            // 送付先電話番号
            this.form.SoufuGenbaTel.Enabled = isKake;

            this.form.SoufuGenbaFax.Text = string.Empty;            // 送付先FAX番号
            this.form.SoufuGenbaFax.Enabled = isKake;

            this.form.SeikyuuTantou.Text = string.Empty;            // 請求担当者
            this.form.SeikyuuTantou.Enabled = isKake;

            // 請求書代表印字区分
            if (!isKake)
            {
                this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull && isKake != this.form.SeikyuuDaihyouPrintKbn.Enabled)
            {
                this.form.SeikyuuDaihyouPrintKbn.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
            }

            this.form.SeikyuuDaihyouPrintKbn.Enabled = isKake;
            this.form.SeikyuuDaihyouPrintKbn1.Enabled = isKake;
            this.form.SeikyuuDaihyouPrintKbn2.Enabled = isKake;

            // 請求書拠点印字区分
            if (!isKake)
            {
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull && isKake != this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled)
            {
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
            }
            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = isKake;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isKake;
            this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isKake;

            // 請求書拠点CD
            if (!isKake)
            {
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull && isKake != this.form.SEIKYUU_KYOTEN_CD.Enabled && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
            {
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()));
            }

            M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
            if (this.form.SEIKYUU_KYOTEN_CD.Text != string.Empty)
            {
                this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
            }

            this.ChangeSeikyuuKyotenPrintKbn();

            this.form.GENBA_COPY_SEIKYU_BUTTON.Enabled = isKake;            // 請求書業者情報コピー
            this.form.SeikyuuSoufuAddressSearchButton.Enabled = isKake;     // 請求書住所参照
            this.form.SeikyuuSoufuPostSearchButton.Enabled = isKake;        // 請求書郵便番号参照
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(支払)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSiharaiControl(Boolean isKake)
        {
            this.form.ShiharaiSoufuName1.Text = string.Empty;       // 支払明細書送付先1
            this.form.ShiharaiSoufuName1.Enabled = isKake;

            this.form.ShiharaiSoufuKeishou1.Text = string.Empty;    // 支払明細書送付先敬称1
            this.form.ShiharaiSoufuKeishou1.Enabled = isKake;

            this.form.ShiharaiSoufuName2.Text = string.Empty;       // 支払明細書送付先2
            this.form.ShiharaiSoufuName2.Enabled = isKake;

            this.form.ShiharaiSoufuKeishou2.Text = string.Empty;    // 支払明細書送付先敬称2
            this.form.ShiharaiSoufuKeishou2.Enabled = isKake;

            this.form.ShiharaiSoufuPost.Text = string.Empty;        // 送付先郵便番号
            this.form.ShiharaiSoufuPost.Enabled = isKake;

            this.form.ShiharaiSoufuAddress1.Text = string.Empty;    // 送付先住所１
            this.form.ShiharaiSoufuAddress1.Enabled = isKake;

            this.form.ShiharaiSoufuAddress2.Text = string.Empty;    // 送付先住所２
            this.form.ShiharaiSoufuAddress2.Enabled = isKake;

            this.form.ShiharaiSoufuBusho.Text = string.Empty;       // 送付先部署
            this.form.ShiharaiSoufuBusho.Enabled = isKake;

            this.form.ShiharaiSoufuTantou.Text = string.Empty;      // 送付先担当者
            this.form.ShiharaiSoufuTantou.Enabled = isKake;

            this.form.ShiharaiGenbaTel.Text = string.Empty;         // 送付先電話番号
            this.form.ShiharaiGenbaTel.Enabled = isKake;

            this.form.ShiharaiGenbaFax.Text = string.Empty;         // 送付先FAX番号
            this.form.ShiharaiGenbaFax.Enabled = isKake;

            // 支払書拠点印字区分
            if (!isKake)
            {
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull && isKake != this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled)
            {
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
            }
            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = isKake;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isKake;
            this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isKake;

            // 支払書拠点CD
            if (!isKake)
            {
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
            }
            else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.IsNull && isKake != this.form.SHIHARAI_KYOTEN_CD.Enabled && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
            {
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()));
            }

            M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
            if (this.form.SHIHARAI_KYOTEN_CD.Text != string.Empty)
            {
                this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
            }

            this.ChangeShiharaiKyotenPrintKbn();

            this.form.GENBA_COPY_SIHARAI_BUTTON.Enabled = isKake;     // 支払書業者情報コピー
            this.form.ShiharaiSoufuAddressSearchButton.Enabled = isKake;                         // 支払書住所参照
            this.form.ShiharaiSoufuPostSearchButton.Enabled = isKake;                            // 支払書郵便番号参照
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(定期回収情報タブ)
        /// </summary>
        /// <param name="isKake">取引先区分が[1.現金]時に true</param>
        /// <param name="row"></param>
        /// <param name="isCellEvent">CellEnter,CellValidatingイベントからの呼出時に true</param>
        private void ChangeTeikiKaishuuControl(Boolean isKake, Row row, bool isCellEvent = false)
        {
            var teikiHinmeiDataTable = this.isSettingWindowData ? this.TeikiHinmeiTable : this.form.TeikiHinmeiIchiran.DataSource as DataTable;
            //var templateChangeFlg = false;

            //if (!isKake)
            //{
            //    // 取引先区分が現金の際（実績売上支払確定画面に表示されない為）
            //    // 契約区分：定期を選択できないようにする。
            //    var teikiKeiyakuKbnCell = row.Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeiyakuKbnCell != null && !teikiKeiyakuKbnCell.CharacterLimitList.Equals(new char[] { '2', '3' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeiyakuKbnCell.CharacterLimitList = new char[] { '2', '3' };
            //        teikiKeiyakuKbnCell.Tag = "【2、3】のいずれかで入力してください";
            //    }

            //    // 集計単位：合算を選択できないようにする。
            //    var teikiKeijyouKbnCell = row.Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeijyouKbnCell != null && !teikiKeijyouKbnCell.CharacterLimitList.Equals(new char[] { '1' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeijyouKbnCell.CharacterLimitList = new char[] { '1' };
            //        teikiKeijyouKbnCell.Tag = "【1】のいずれかで入力してください";
            //    }
            //}
            //else
            //{
            //    // 契約区分：定期を選択可能にする。
            //    var teikiKeiyakuKbnCell = row.Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeiyakuKbnCell != null && !teikiKeiyakuKbnCell.CharacterLimitList.Equals(new char[] { '1', '2', '3' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeiyakuKbnCell.CharacterLimitList = new char[] { '1', '2', '3' };
            //        teikiKeiyakuKbnCell.Tag = "【1～3】のいずれかで入力してください";
            //    }

            //    // 集計単位：合算を選択可能にする。
            //    var teikiKeijyouKbnCell = row.Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeijyouKbnCell != null && !teikiKeijyouKbnCell.CharacterLimitList.Equals(new char[] { '1', '2' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeijyouKbnCell.CharacterLimitList = new char[] { '1', '2' };
            //        teikiKeijyouKbnCell.Tag = "【1、2】のいずれかで入力してください";
            //    }
            //}

            //if (templateChangeFlg)
            //{
            this.TeikiHinmeiTable = teikiHinmeiDataTable;
            this.SetIchiranTeiki(isCellEvent);
            //}
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        public bool ChangeShiharaiKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool catchErr = false;
                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()));
                        ShiharaiKyotenCdValidated(out catchErr);
                    }
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                }

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        // No2267-->
        /// <summary>
        /// アラートの内容の重複確認クリア
        /// </summary>
        public bool errorMessagesClear()
        {
            try
            {
                messageList.Clear();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("errorMessagesClear", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// アラートの内容の重複削除
        /// </summary>
        /// <param name="e"></param>
        private void errorMessagesUniq(RegistCheckEventArgs e)
        {
            List<string> deleteList = new List<string>();

            foreach (string item in e.errorMessages)
            {
                if (messageList.Contains(item))
                {
                    deleteList.Add(item);
                }
                else
                {
                    messageList.Add(item);
                }
            }
            foreach (string item in deleteList)
            {
                e.errorMessages.Remove(item);
            }
        }
        // No2267<--

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool CheckRegist(object sender, RegistCheckEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                MessageUtility msgUtil = new MessageUtility();

                //// チェックボックスコントロールチェック
                //if (sender is CustomCheckBox)
                //{
                //CustomCheckBox item = sender as CustomCheckBox;
                //string controlName = item.Name;

                //if ((item.Name.Equals("HaishutsuKbn") && this.form.HaishutsuKbn.Enabled)
                //    || (item.Name.Equals("TsumikaeHokanKbn") && !this.form.HaishutsuKbn.Enabled && this.form.TsumikaeHokanKbn.Enabled)
                //    || (item.Name.Equals("ShobunJigyoujouKbn") && !this.form.HaishutsuKbn.Enabled && !this.form.TsumikaeHokanKbn.Enabled && this.form.ShobunJigyoujouKbn.Enabled))
                //{
                //    if (item.Name.Equals("HaishutsuKbn")
                //        || item.Name.Equals("TsumikaeHokanKbn")
                //            || item.Name.Equals("ShobunJigyoujouKbn")
                //                || item.Name.Equals("SaishuuShobunjouKbn"))
                //    {
                //        //業者分類の必須チェック
                //        if (this.form.GyoushaKbnMani.Checked
                //            && (!this.form.HaishutsuKbn.Checked
                //                && !this.form.TsumikaeHokanKbn.Checked
                //                && !this.form.ShobunJigyoujouKbn.Checked
                //                && !this.form.SaishuuShobunjouKbn.Checked))
                //        {
                //            e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "排出事業場、積み替え保管、処分事業場、最終処分場のいずれか"));
                //        }
                //    }
                //}
                //}

                // 通常コントロールチェック
                if (sender is CustomTextBox)
                {
                    CustomTextBox item = sender as CustomTextBox;
                    //マニありのチェックが1つでもついている場合必須チェックする。
                    if (!this.form.IsManiAriAllNoChecked())
                    {
                        // 都道府県CD必須チェック
                        if (item.Name.Equals("GenbaTodoufukenCode"))
                        {
                            if (!this.form.ShokuchiKbn.Checked && this.form.GyoushaKbnMani.Checked && string.IsNullOrWhiteSpace(item.Text))
                            {
                                e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, item.DisplayItemName));
                                item.BackColor = Constans.ERROR_COLOR;
                            }
                        }

                        // 地域CD必須チェック
                        if (item.Name.Equals("ChiikiCode"))
                        {
                            if (!this.form.ShokuchiKbn.Checked && this.form.GyoushaKbnMani.Checked && string.IsNullOrWhiteSpace(item.Text))
                            {
                                e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, item.DisplayItemName));
                                item.BackColor = Constans.ERROR_COLOR;
                            }
                        }
                    }
                }

                // 定期情報一覧内、必須チェック
                if (e.multiRow != null && e.multiRow.Name.Equals("TeikiHinmeiIchiran"))
                {
                    if (sender is GcCustomAlphaNumTextBoxCell)
                    {
                        GcCustomAlphaNumTextBoxCell item = sender as GcCustomAlphaNumTextBoxCell;
                        int rowIdx = item.RowIndex;

                        if (item.Name.Equals(Const.GenbaHoshuConstans.TEIKI_HINMEI_CD))
                        {
                            Row row = this.form.TeikiHinmeiIchiran.Rows[rowIdx];

                            // 一旦、エラー表示を解除する
                            row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_MONDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_TUESDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_THURSDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_FRIDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_SATURDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_SUNDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Style.BackColor = Constans.NOMAL_COLOR;
                            if (row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled)
                            {
                                row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            }
                            else
                            {
                                row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled = true;
                                row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            }
                            row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = true;
                            row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = false;

                            // 何か入力されている行のみチェックする
                            // 削除チェックされている行はチェックしない
                            var teikiDeleteFlg = row["DELETE_FLG"].Value as bool?;
                            if (
                                !(teikiDeleteFlg.HasValue && teikiDeleteFlg.Value) &&
                                (
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KANSANCHI].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_MONDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_TUESDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_THURSDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_FRIDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_SATURDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TEIKI_SUNDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value)
                                )
                            )
                            {
                                GcCustomCheckBoxCell mon = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_MONDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell tue = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_TUESDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell wed = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_WEDNESDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell thu = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_THURSDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell fri = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_FRIDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell sat = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_SATURDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell sun = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.GenbaHoshuConstans.TEIKI_SUNDAY] as GcCustomCheckBoxCell;

                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "品名CD"));
                                    row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単位"));
                                    row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                else if (!this.ValueIsKgUnitCD(row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value) && !this.ValueIsKgUnitCD(row[Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value))
                                {
                                    var strWithoutKgUnitErrMsg = string.Format(msgUtil.GetMessage("W004").MESSAGE);
                                    if (!e.errorMessages.Contains(strWithoutKgUnitErrMsg))
                                    {
                                        e.errorMessages.Add(strWithoutKgUnitErrMsg);
                                    }
                                    row[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrFalse(mon.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(tue.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(wed.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(thu.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(fri.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(sat.Value)
                                    && this.ValueIsNullOrDBNullOrFalse(sun.Value))
                                {
                                    string errorItem = string.Format("{0}、{1}、{2}、{3}、{4}、{5}、{6}のいづれか"
                                        , mon.DisplayItemName
                                        , tue.DisplayItemName
                                        , wed.DisplayItemName
                                        , thu.DisplayItemName
                                        , fri.DisplayItemName
                                        , sat.DisplayItemName
                                        , sun.DisplayItemName);
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, errorItem));
                                    row[Const.GenbaHoshuConstans.TEIKI_MONDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_TUESDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_WEDNESDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_THURSDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_FRIDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_SATURDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.GenbaHoshuConstans.TEIKI_SUNDAY].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "契約区分"));
                                    row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                else if (row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("1"))
                                {
                                    if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value))
                                    {
                                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "月極品名"));
                                        row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                    }
                                }
                                else if (row[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("2"))
                                {
                                    row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = true;
                                    if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value))
                                    {
                                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "集計単位"));
                                        row[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Style.BackColor = Constans.ERROR_COLOR;
                                    }
                                }

                                // 単位CDと換算後単位CDの重複チェック
                                if (this.ContainsDuplicatedUnitCd(row))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E031").MESSAGE, "同一品名で単位と換算後単位"));
                                    row[GenbaHoshuConstans.TEIKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                    row[GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }
                    }
                }

                // 月極情報一覧内、必須チェック
                if (e.multiRow != null && e.multiRow.Name.Equals("TsukiHinmeiIchiran"))
                {
                    if (sender is GcCustomAlphaNumTextBoxCell)
                    {
                        GcCustomAlphaNumTextBoxCell item = sender as GcCustomAlphaNumTextBoxCell;
                        int rowIdx = item.RowIndex;

                        if (item.Name.Equals(Const.GenbaHoshuConstans.TSUKI_HINMEI_CD))
                        {
                            Row row = this.form.TsukiHinmeiIchiran.Rows[rowIdx];

                            // 一旦、エラー表示を解除する
                            row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.GenbaHoshuConstans.TSUKI_TANKA].Style.BackColor = Constans.NOMAL_COLOR;

                            // 何か入力されている行のみチェックする
                            // 削除チェックされている行はチェックしない
                            var tsukiDeleteFlg = row["DELETE_FLG"].Value as bool?;
                            if (
                                !(tsukiDeleteFlg.HasValue && tsukiDeleteFlg.Value) &&
                                (
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_TANKA].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.GenbaHoshuConstans.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN].Value)
                                )
                            )
                            {
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "品名CD"));
                                    row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単位"));
                                    row[Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_TANKA].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単価"));
                                    row[Const.GenbaHoshuConstans.TSUKI_TANKA].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }
                    }
                }

                // No2267-->
                this.errorMessagesUniq(e);
                // No2267<--

                //LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 事業場分類チェック処理
        /// </summary>
        /// <returns>true:有効, false:警告</returns>
        public bool CheckBunruiKbn()
        {
            if (this.form.HaishutsuKbn.Enabled ||
                this.form.TsumikaeHokanKbn.Enabled ||
                this.form.ShobunJigyoujouKbn.Enabled ||
                this.form.SaishuuShobunjouKbn.Enabled)
            {
                //業者分類の必須チェック
                if (this.form.GyoushaKbnMani.Checked
                    && (!this.form.HaishutsuKbn.Checked
                        && !this.form.TsumikaeHokanKbn.Checked
                        && !this.form.ShobunJigyoujouKbn.Checked
                        && !this.form.SaishuuShobunjouKbn.Checked))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 定期品名一覧セルフォーカスエンター処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellEnter(CellEventArgs e)
        {
            try
            {
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD))
                {
                    List<string> tsukiHinmei = new List<string>();
                    tsukiHinmei.Add(" ");
                    foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                    {
                        if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value))
                        {
                            continue;
                        }
                        tsukiHinmei.Add(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString());
                    }
                    ((GcCustomAlphaNumTextBoxCell)this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex]).PopupSearchSendParams[0].Value = string.Join(",", tsukiHinmei.ToArray());
                }

                // 換算後単位モバイル出力フラグフォーカス時処理
                if (e.CellName.Equals(Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG))
                {
                    // 換算後単位が入力されていない場合、次コントロールをフォーカス
                    if (!this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                    {
                        if (this.prevTeikiCellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD))
                        {
                            this.form.TeikiHinmeiIchiran.CurrentCellPosition = new CellPosition(e.RowIndex, "ANBUN_FLG");
                        }
                        else
                        {
                            this.form.TeikiHinmeiIchiran.CurrentCellPosition = new CellPosition(e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD);
                        }
                    }
                }

                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN))
                {
                    var denpyouKbnCd = this.form.TeikiHinmeiIchiran.CurrentRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
                    if (GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR == denpyouKbnCd.ToString())
                    {
                        this.ChangeTeikiKaishuuControl(this.IsSeikyuuKake(), this.form.TeikiHinmeiIchiran.CurrentRow, true);
                    }
                    else if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
                    {
                        this.ChangeTeikiKaishuuControl(this.IsShiharaiKake(), this.form.TeikiHinmeiIchiran.CurrentRow, true);
                    }
                    else
                    {
                        this.ChangeTeikiKaishuuControl(true, this.form.TeikiHinmeiIchiran.CurrentRow, true);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 定期品名一覧セルリーブ処理
        /// </summary>
        /// <param name="e"></param>
        internal void TeikiHinmeiCellLeave(CellEventArgs e)
        {
            try
            {
                // 契約区分
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                {
                    var row = this.form.TeikiHinmeiIchiran.Rows[e.RowIndex];

                    // 月極品名CD～単価適用終了日まで背景色を更新
                    row.Cells[GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].UpdateBackColor(false);
                    row.Cells[GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].UpdateBackColor(false);
                    row.Cells[GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].UpdateBackColor(false);
                    row.Cells[GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].UpdateBackColor(false);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellLeave", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 定期品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellFormat(CellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);
                // 換算値の表示書式設定を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSANCHI))
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = string.Format("{0:0.000}", e.Value);
                    }
                }

                //LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellFormat", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValidated(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 前回セル名保存
                this.prevTeikiCellName = e.CellName;

                // 換算後単位の入力後処理を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD)
                    && this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG
                    && this.form.WindowType != WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 換算後単位が入力されている場合、換算後単位モバイル出力フラグは活性
                    if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value != null
                        && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value.ToString()))
                    {
                        if (!this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = true;
                        }
                    }
                    // 換算後単位が入力されていない場合、換算後単位モバイル出力フラグは非活性
                    else if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = false;
                    }
                }

                // 契約区分名の表示を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                {
                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = "定期";
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = "単価";
                            break;

                        case "3":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = "回収のみ";
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = string.Empty;
                            break;
                    }
                }
                // 集計区分名の表示を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN))
                {
                    // 許容外の文字が入力されている場合、値のクリアを行う
                    var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    if (cell != null && cell.CharacterLimitList != null && cell.Value != null && cell.Value != DBNull.Value)
                    {
                        if (!cell.CharacterLimitList.Contains(cell.Value.ToString()[0]))
                        {
                            cell.Value = DBNull.Value;
                        }
                    }

                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = "伝票";
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = "合算";
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            break;
                    }
                }
                // 品名CDの大文字対応
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_HINMEI_CD))
                {
                    if (this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString().ToUpper();

                        //set column hidden BEFORE_HINMEI_CD
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_BEFORE_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value;
                    }
                }
                // 月極品名CDの大文字対応
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD))
                {
                    if (this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value.ToString().ToUpper();
                    }
                }

                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_UNIT_CD))
                {
                    if (this.form.beforeValuesForDetail.ContainsKey(e.CellName)
                        && !string.IsNullOrWhiteSpace(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value))
                        && !this.form.beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value)))
                    {
                        if (this.sysinfoEntity != null)
                        {
                            //set 換算後単位
                            if (!Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value).Equals("3")
                                && !Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value).Equals("03"))
                            {
                                if (!this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.IsNull)
                                {
                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.Value);
                                    if (uni != null)
                                    {
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                    }
                                }
                            }

                            //set 要記入、実数
                            if (!string.IsNullOrEmpty(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value)))
                            {
                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = false;
                                if (this.sysinfoEntity.YOUKI_NYUU.IsTrue)
                                {
                                    this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = true;
                                }
                            }
                        }
                    }
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 伝票区分設定
        /// 定期明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetTeikiDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.TeikiHinmeiIchiran.CurrentRow;

            if (targetRow == null)
            {
                return true;
            }

            // 初期化
            targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = 0;
            targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

            if (targetRow.Cells[GenbaHoshuConstans.TEIKI_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            // ポップアップを打ち上げ、ユーザに選択してもらう
            CellPosition pos = this.form.TeikiHinmeiIchiran.CurrentCellPosition;
            CustomControlExtLogic.PopUp((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[pos.RowIndex].Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD]);

            var denpyouKbnCd = targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
            if (denpyouKbnCd == null
                || targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value.ToString() == "0"
                || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
            {
                // ポップアップでキャンセルが押された
                // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = 0;
                targetRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

                //ポップアップキャンセルフラグをTrueにする。
                this.form.bDetailErrorFlag = true;

                return false;
            }

            // 伝票区分が支払の場合、契約区分に定期が入力されていたら契約区分をクリアする（未入力の場合は処理が終了しているのでnullチェックは行わない）
            if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
            {
                if (null != targetRow.Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value && !String.IsNullOrEmpty(targetRow.Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value.ToString()))
                {
                    var keiyakuKbn = targetRow.Cells[Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value.ToString();
                    if (Const.GenbaHoshuConstans.KEIYAKU_KBN_CD_TEIKI == keiyakuKbn)
                    {
                        targetRow.Cells[GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = 0;
                        targetRow.Cells[GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = String.Empty;
                    }
                }
            }

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 明細エラーフラグ解除
                this.form.bDetailErrorFlag = false;

                // 品名の入力チェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_HINMEI_CD))
                {
                    // エラーがない場合、品名情報の取得を行う
                    if (!e.Cancel)
                    {
                        bool isHinSet = false;
                        bool isUniSet = false;
                        bool isKbnSet = false;

                        if (e.FormattedValue == null || (e.FormattedValue != null && string.IsNullOrWhiteSpace(e.FormattedValue.ToString())))
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = 0;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;
                            return false;
                        }

                        //20150813 hoang edit #12046
                        string hinCd = e.FormattedValue.ToString().PadLeft((int)((GcCustomAlphaNumTextBoxCell)this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD]).CharactersNumber, '0');
                        M_HINMEI cond = new M_HINMEI();
                        cond.HINMEI_CD = hinCd;
                        DataTable dt = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataBySqlFile(this.GET_HINMEI_URIAGE_SHIHARAI_DATA_SQL, cond);
                        if (dt == null || (dt != null && dt.Rows.Count <= 0))
                        {
                            msgLogic.MessageBoxShow("E020", "品名");
                            e.Cancel = true;
                            this.form.bDetailErrorFlag = true;
                            if (this.form.TeikiHinmeiIchiran.EditingControl != null)
                            {
                                ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                            }
                            return false;
                        }
                        //20150813 hoang end edit #12046

                        //if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()) && !this.form.beforeValuesForDetail[e.CellName].Equals(e.FormattedValue.ToString()))  //20150813 hoang edit #12046
                        if (string.IsNullOrEmpty(this.form.beforeValuesForDetail[e.CellName])
                            || (!string.IsNullOrEmpty(this.form.beforeValuesForDetail[e.CellName]) && !this.form.beforeValuesForDetail[e.CellName].Equals(hinCd)))
                        {
                            // 品名をセットする場合、契約区分をクリアする
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = 0;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = String.Empty;

                            //20150813 hoang edit #12046
                            //string hinCd = e.FormattedValue.ToString().PadLeft((int)((GcCustomAlphaNumTextBoxCell)this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD]).CharactersNumber, '0');
                            //M_HINMEI cond = new M_HINMEI();
                            //cond.HINMEI_CD = hinCd;
                            //DataTable dt = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataBySqlFile(this.GET_HINMEI_URIAGE_SHIHARAI_DATA_SQL, cond);

                            //if (dt != null && dt.Rows.Count > 0)
                            //{
                            //    isHinSet = true;
                            //    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                            //    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_PRINT_STRING].Value = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();

                            //    if (dt.Rows[0]["UNIT_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["UNIT_CD"].ToString()))
                            //    {
                            //        M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(dt.Rows[0]["UNIT_CD"].ToString()));
                            //        if (uni != null)
                            //        {
                            //            isUniSet = true;
                            //            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value = uni.UNIT_CD.ToString();
                            //            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                            //        }
                            //    }
                            //    if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                            //    {
                            //        M_DENPYOU_KBN kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(dt.Rows[0]["DENPYOU_KBN_CD"].ToString());
                            //        if (kbn != null)
                            //        {
                            //            switch (kbn.DENPYOU_KBN_CD.ToString())
                            //            {
                            //                case GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR:
                            //                case GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                            //                    isKbnSet = true;
                            //                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                            //                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                            //                    break;
                            //                default:
                            //                    if (SetTeikiDenpyouKbn())
                            //                    {
                            //                        isKbnSet = true;
                            //                    }
                            //                    else
                            //                    {
                            //                        e.Cancel = true;
                            //                        this.form.bDetailErrorFlag = true;
                            //                    }
                            //                    break;
                            //            }

                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    msgLogic.MessageBoxShow("E020", "品名");
                            //    e.Cancel = true;
                            //    this.form.bDetailErrorFlag = true;
                            //    if (this.form.TeikiHinmeiIchiran.EditingControl != null)
                            //    {
                            //        ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                            //    }
                            //}

                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value = dt.Rows[0]["HINMEI_CD"].ToString();
                            this.form.beforeValuesForDetail[e.CellName] = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString();

                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();

                            isHinSet = true;

                            if (dt.Rows[0]["UNIT_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["UNIT_CD"].ToString()))
                            {
                                M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(dt.Rows[0]["UNIT_CD"].ToString()));
                                if (uni != null)
                                {
                                    isUniSet = true;
                                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                }
                            }
                            if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                            {
                                M_DENPYOU_KBN kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(dt.Rows[0]["DENPYOU_KBN_CD"].ToString());
                                if (kbn != null)
                                {
                                    switch (kbn.DENPYOU_KBN_CD.ToString())
                                    {
                                        case GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR:
                                        case GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                                            isKbnSet = true;
                                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                                            break;

                                        default:
                                            if (SetTeikiDenpyouKbn())
                                            {
                                                isKbnSet = true;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                                this.form.beforeValuesForDetail[e.CellName] = string.Empty;
                                                this.form.bDetailErrorFlag = true;
                                            }
                                            break;
                                    }

                                    if (this.sysinfoEntity != null)
                                    {
                                        //check hinmei input is new
                                        string BeforeHinmeiCd = Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_BEFORE_HINMEI_CD].Value);
                                        if (string.IsNullOrEmpty(BeforeHinmeiCd))
                                        {
                                            //set 換算後単位
                                            if (!Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_UNIT_CD].Value).Equals("3")
                                                && !Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_UNIT_CD].Value).Equals("03"))
                                            {
                                                if (!this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.IsNull)
                                                {
                                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.Value);
                                                    if (uni != null)
                                                    {
                                                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = true;
                                                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                                    }
                                                }
                                            }

                                            //set 要記入、実数
                                            if (!string.IsNullOrEmpty(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD].Value)))
                                            {
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = false;
                                                if (this.sysinfoEntity.YOUKI_NYUU.IsTrue)
                                                {
                                                    this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = true;
                                                }
                                            }
                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_ANBUN_FLG].Value = false;
                                            if (this.sysinfoEntity.JISSUU.IsTrue)
                                            {
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_ANBUN_FLG].Value = true;
                                            }

                                            //set 契約区分、集計単位
                                            if (string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
                                            {
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = "3";
                                            }
                                            else
                                            {
                                                var seikyuuEntity = this.daoSeikyuu.GetDataByCd(this.form.TorihikisakiCode.Text);
                                                if (seikyuuEntity != null)
                                                {
                                                    var keiyakuKbn = this.sysinfoEntity.GENBA_TEIKI_KEIYAKU_KBN;
                                                    var keijouKbn = this.sysinfoEntity.GENBA_TEIKI_KEIJYOU_KBN;

                                                    if (seikyuuEntity.TORIHIKI_KBN_CD.Value == 1)
                                                    {
                                                        if (keiyakuKbn.IsNull || keiyakuKbn == 1)
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = DBNull.Value;
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                                                        }
                                                        else if (keiyakuKbn == 2 || keiyakuKbn == 3)
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = keiyakuKbn.Value;

                                                            if (keijouKbn.IsNull)
                                                            {
                                                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                                                            }
                                                            else
                                                            {
                                                                this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = keijouKbn.Value;
                                                            }
                                                        }
                                                    }
                                                    else if (seikyuuEntity.TORIHIKI_KBN_CD.Value == 2)
                                                    {
                                                        if (keiyakuKbn.IsNull)
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = DBNull.Value;
                                                        }
                                                        else
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIYAKU_KBN].Value = keiyakuKbn.Value;
                                                        }
                                                        if (keijouKbn.IsNull)
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                                                        }
                                                        else
                                                        {
                                                            this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = keijouKbn.Value;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    var denpyouKbnCd = this.form.TeikiHinmeiIchiran.CurrentRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
                                    if (GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR == denpyouKbnCd.ToString())
                                    {
                                        this.ChangeTeikiKaishuuControl(this.IsSeikyuuKake(), this.form.TeikiHinmeiIchiran.CurrentRow, true);
                                    }
                                    else if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
                                    {
                                        this.ChangeTeikiKaishuuControl(this.IsShiharaiKake(), this.form.TeikiHinmeiIchiran.CurrentRow, true);
                                    }
                                    else
                                    {
                                        this.ChangeTeikiKaishuuControl(true, this.form.TeikiHinmeiIchiran.CurrentRow, true);
                                    }
                                }
                            }
                            //20150813 hoang end edit #12046
                        }
                        if (this.form.beforeValuesForDetail[e.CellName].Equals(hinCd))
                        {
                            isHinSet = true;
                            isUniSet = true;
                            isKbnSet = true;
                        }
                        if (!isHinSet)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isUniSet)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isKbnSet)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;
                        }
                    }
                }
                // 月極品名CDの入力チェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        string hinmeiCd = e.FormattedValue.ToString().PadLeft(6, '0').ToUpper();
                        bool isExists = false;
                        foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                        {
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value))
                            {
                                continue;
                            }
                            if (hinmeiCd.Equals(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
                            {
                                isExists = true;
                                this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value =
                                    row[Const.GenbaHoshuConstans.TSUKI_HINMEI_NAME_RYAKU].Value.ToString();
                                break;
                            }
                        }

                        if (!isExists)
                        {
                            msgLogic.MessageBoxShow("E020", "月極情報");
                            e.Cancel = true;
                            this.form.bDetailErrorFlag = true;
                            ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                        }
                    }
                    else
                    {
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                    }
                }
                // 換算値のチェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSANCHI))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        string[] sep = e.FormattedValue.ToString().Replace(",", string.Empty).Split('.');
                        string integ = sep[0];
                        string frac = sep.Length >= 2 ? sep[1] : "0";

                        if (integ.Length > 4)
                        {
                            integ = integ.Substring(integ.Length - 4);
                        }
                        if (frac.Length > 3)
                        {
                            frac = frac.Substring(0, 3);
                        }

                        this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value = string.Format("{0:0.000}", Decimal.Parse(integ + "." + frac));
                    }
                }
                // 契約区分のチェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                {
                    var keiyakuKbn = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString();
                    if (this.IsDenpyouKbnShiharai())
                    {
                        if (null != this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value && !String.IsNullOrEmpty(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString()))
                        {
                            if (Const.GenbaHoshuConstans.KEIYAKU_KBN_CD_TEIKI == keiyakuKbn)
                            {
                                msgLogic.MessageBoxShow("E155");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(keiyakuKbn))
                    {
                        var TeikiHinmei = Convert.ToString(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value);

                        //check TeikiHinmei
                        if (string.IsNullOrEmpty(TeikiHinmei))
                        {
                            msgLogic.MessageBoxShowError(Const.GenbaHoshuConstans.MSG_ERR_A_KEIYAKU_KBN);
                            e.Cancel = true;
                            this.form.bDetailErrorFlag = true;
                            ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                            return false;
                        }
                        //check Torihikisaki
                        if (string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
                        {
                            if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(Convert.ToString(e.FormattedValue)))
                            {
                                if (!Convert.ToString(e.FormattedValue).Equals("3"))
                                {
                                    msgLogic.MessageBoxShowError(Const.GenbaHoshuConstans.MSG_ERR_B_KEIYAKU_KBN);
                                    e.Cancel = true;
                                    this.form.bDetailErrorFlag = true;
                                    ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                                }
                            }
                            return false;
                        }

                        //check 取引先CD－取引区分
                        var TeikiDenpyouKbn = Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value);
                        int TorihikiKbn = 1;
                        if (TeikiDenpyouKbn == "1")
                        {
                            M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                            queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                            M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                            if (seikyuuEntity != null)
                            {
                                TorihikiKbn = seikyuuEntity.TORIHIKI_KBN_CD.Value;
                            }
                            if (TorihikiKbn == 1)
                            {
                                if (keiyakuKbn == "1")
                                {
                                    msgLogic.MessageBoxShowError(Const.GenbaHoshuConstans.MSG_ERR_C_KEIYAKU_KBN);
                                    e.Cancel = true;
                                    this.form.bDetailErrorFlag = true;
                                    ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.form.bDetailErrorFlag = true;
                LogUtility.Error("TeikiHinmeiCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValueChanged(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 契約区分の設定により入力制限を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                {
                    // 許容外の文字が入力されている場合、値のクリアを行う
                    var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    if (cell != null && cell.CharacterLimitList != null && cell.Value != null && cell.Value != DBNull.Value)
                    {
                        if (!cell.CharacterLimitList.Contains(cell.Value.ToString()[0]))
                        {
                            cell.Value = DBNull.Value;
                        }
                    }

                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Selectable = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = true; //add
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty; //add
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Enabled = false; //add
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false; //add
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Selectable = true;
                            //this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Enabled = true; //add
                            // TEIKI_KEIJYOU_KBNは入力可能になったので、Validatedを実行し、名称を設定する
                            this.TeikiHinmeiCellValidated(
                                new CellEventArgs(
                                    e.RowIndex,
                                    this.form.TeikiHinmeiIchiran.Columns[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Index,
                                    Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN
                                    )
                                );
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false; //add
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Enabled = false; //add
                            break;
                    }
                }

                // 換算後単位の変更後処理を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU))
                {
                    // 換算後単位が入力されている場合、換算後単位モバイル出力フラグは活性
                    if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value != null
                        && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value.ToString()))
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = true;
                    }
                    // 換算後単位が入力されていない場合、換算後単位モバイル出力フラグは非活性
                    else
                    {
                        // 活性から非活性にした場合は値をFalseに設定する
                        if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = false;
                        }
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = false;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellValueChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 定期品名一覧未確定データ処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiDirtyStateChanged(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.form.TeikiHinmeiIchiran.CurrentCell.Name.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                {
                    this.form.TeikiHinmeiIchiran.CommitEdit();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiDirtyStateChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 定期品名一覧行入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiRowValidating(CellCancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 品名、単位の重複チェックを行う
                if (!this.ValueIsNullOrDBNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value)
                    && !this.ValueIsNullOrDBNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value))
                {
                    string hinmeiCd = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString();
                    string unitCd = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value.ToString();

                    if (this.form.TeikiHinmeiIchiran.Rows.Where(w => !w.IsNewRow && w.Index != e.RowIndex
                         && !this.ValueIsNullOrDBNullOrWhiteSpace(w[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value)
                         && !this.ValueIsNullOrDBNullOrWhiteSpace(w[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value)).ToList().Any(r =>
                             hinmeiCd.Equals(r[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString())
                             && unitCd.Equals(r[Const.GenbaHoshuConstans.TEIKI_UNIT_CD].Value.ToString())))
                    {
                        new MessageBoxShowLogic().MessageBoxShow("E031", "品名CD,単位CD");
                        e.Cancel = true;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiRowValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool TeikiHinmeiCellSwitchCdName(CellEventArgs e, GenbaHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case GenbaHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TeikiHinmeiIchiran.CurrentCell = this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_UNIT_CD];
                        this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    else if (e.CellName.Equals(GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TeikiHinmeiIchiran.CurrentCell = this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD];
                        this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case GenbaHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(GenbaHoshuConstans.TEIKI_UNIT_CD))
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    else if (e.CellName.Equals(GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD))
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TeikiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TEIKI_KANSAN_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public bool CheckPopup(KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.KeyCode == Keys.Space)
                {
                    if (this.form.TeikiHinmeiIchiran.Columns[this.form.TeikiHinmeiIchiran.CurrentCell.CellIndex].Name.Equals(Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN))
                    {
                        MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("CD", typeof(string));
                        dt.Columns.Add("VALUE", typeof(string));
                        dt.Columns[0].ReadOnly = true;
                        dt.Columns[1].ReadOnly = true;
                        DataRow row;
                        row = dt.NewRow();
                        row["CD"] = "1";
                        row["VALUE"] = "定期";
                        dt.Rows.Add(row);
                        row = dt.NewRow();
                        row["CD"] = "2";
                        row["VALUE"] = "単価";
                        dt.Rows.Add(row);
                        row = dt.NewRow();
                        row["CD"] = "3";
                        row["VALUE"] = "回収のみ";
                        dt.Rows.Add(row);
                        form.table = dt;
                        //form.title = "契約区分";
                        //form.headerList = new List<string>();
                        //form.headerList.Add("契約区分CD");
                        //form.headerList.Add("契約区分");
                        form.PopupTitleLabel = "契約区分";
                        form.PopupGetMasterField = "CD,VALUE";
                        form.PopupDataHeaderTitle = new string[] { "契約区分CD", "契約区分" };
                        form.ShowDialog();
                        if (form.ReturnParams != null)
                        {
                            this.form.TeikiHinmeiIchiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                            this.form.TeikiHinmeiIchiran[this.form.TeikiHinmeiIchiran.CurrentCell.RowIndex, Const.GenbaHoshuConstans.TEIKI_KEIYAKU_KBN_NAME].Value = form.ReturnParams[1][0].Value.ToString();
                        }
                    }
                    if (this.form.TeikiHinmeiIchiran.Columns[this.form.TeikiHinmeiIchiran.CurrentCell.CellIndex].Name.Equals(Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN))
                    {
                        MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("CD", typeof(string));
                        dt.Columns.Add("VALUE", typeof(string));
                        dt.Columns[0].ReadOnly = true;
                        dt.Columns[1].ReadOnly = true;
                        DataRow row;
                        row = dt.NewRow();
                        row["CD"] = "1";
                        row["VALUE"] = "伝票";
                        dt.Rows.Add(row);
                        var denpyouKbnCd = this.form.TeikiHinmeiIchiran.CurrentRow.Cells[GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD].Value;
                        if (GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR == denpyouKbnCd.ToString() && IsSeikyuuKake())
                        {
                            row = dt.NewRow();
                            row["CD"] = "2";
                            row["VALUE"] = "合算";
                            dt.Rows.Add(row);
                        }
                        else if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString() && IsShiharaiKake())
                        {
                            row = dt.NewRow();
                            row["CD"] = "2";
                            row["VALUE"] = "合算";
                            dt.Rows.Add(row);
                        }
                        else if (denpyouKbnCd.ToString() == "")
                        {
                            row = dt.NewRow();
                            row["CD"] = "2";
                            row["VALUE"] = "合算";
                            dt.Rows.Add(row);
                        }
                        form.table = dt;
                        //form.title = "集計単位";
                        //form.headerList = new List<string>();
                        //form.headerList.Add("集計単位CD");
                        //form.headerList.Add("集計単位");
                        form.PopupTitleLabel = "集計単位";
                        form.PopupGetMasterField = "CD,VALUE";
                        form.PopupDataHeaderTitle = new string[] { "集計単位CD", "集計単位" };
                        form.ShowDialog();
                        if (form.ReturnParams != null)
                        {
                            this.form.TeikiHinmeiIchiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                            this.form.TeikiHinmeiIchiran[this.form.TeikiHinmeiIchiran.CurrentCell.RowIndex, Const.GenbaHoshuConstans.TEIKI_KEIJYOU_KBN_NAME].Value = form.ReturnParams[1][0].Value.ToString();
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckPopup", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 月極品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellFormat(CellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);
                // 換算値の表示書式設定を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TEIKI_KANSANCHI))
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = string.Format("{0:0.000}", e.Value);
                    }
                }
                // 単価の表示書式設定を行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TSUKI_TANKA))
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = string.Format("{0:" + this.sysinfoEntity.SYS_TANKA_FORMAT + "}", e.Value);
                    }
                }

                //LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellFormat", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 伝票区分設定
        /// 月極明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetTsukiDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.TsukiHinmeiIchiran.CurrentRow;

            if (targetRow == null)
            {
                return true;
            }

            // 初期化
            targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value = 0;
            targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

            if (targetRow.Cells[GenbaHoshuConstans.TSUKI_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            // ポップアップを打ち上げ、ユーザに選択してもらう
            CellPosition pos = this.form.TsukiHinmeiIchiran.CurrentCellPosition;
            CustomControlExtLogic.PopUp((ICustomControl)this.form.TsukiHinmeiIchiran.Rows[pos.RowIndex].Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD]);

            var denpyouKbnCd = targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value;
            if (denpyouKbnCd == null
                || targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value.ToString() == "0"
                || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
            {
                // ポップアップでキャンセルが押された
                // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value = 0;
                targetRow.Cells[GenbaHoshuConstans.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

                //ポップアップキャンセルフラグをTrueにする。
                this.form.bDetailErrorFlag = true;

                return false;
            }

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 月極品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 明細エラーフラグ解除
                this.form.bDetailErrorFlag = false;

                // 品名単独のチェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TSUKI_HINMEI_CD))
                {
                    if (!string.IsNullOrWhiteSpace(this.form.beforeValuesForDetail[e.CellName]) && (e.FormattedValue == null || !e.FormattedValue.Equals(this.form.beforeValuesForDetail[e.CellName])))
                    {
                        string hinmeiCd = this.form.beforeValuesForDetail[e.CellName];
                        string hinmeiName = this.form.beforeValuesForDetail[Const.GenbaHoshuConstans.TEIKI_HINMEI_NAME_RYAKU];
                        // 定期使用チェック
                        foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
                        {
                            if (row.IsNewRow) continue;

                            if (row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value != null
                                && !string.IsNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value.ToString())
                                && hinmeiCd.Equals(row[Const.GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value.ToString()))
                            {
                                msgLogic.MessageBoxShow("E086", "当品名CD", "定期回収タブ内", "変更");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_NAME_RYAKU].Value = hinmeiName;
                                ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).Text = hinmeiCd;
                                ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).SelectAll();
                                break;
                            }
                        }
                    }

                    // エラーがない場合は品名の単位を取得
                    if (!e.Cancel)
                    {
                        bool isHinSet = false;
                        bool isUniSet = false;
                        bool isKbnSet = false;
                        if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()) && !this.form.beforeValuesForDetail[e.CellName].Equals(e.FormattedValue.ToString()))
                        {
                            string hinCd = e.FormattedValue.ToString().PadLeft((int)((GcCustomAlphaNumTextBoxCell)this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_CD]).CharactersNumber, '0').ToUpper();
                            // 重複チェック
                            foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                            {
                                if (row.IsNewRow) continue;
                                if (row.Index == e.RowIndex) continue;

                                if (row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value != null
                                    && !string.IsNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
                                {
                                    string targetHinmeiCd = row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString();

                                    if (hinCd.Equals(targetHinmeiCd))
                                    {
                                        msgLogic.MessageBoxShow("E031", "品名CD");
                                        e.Cancel = true;
                                        this.form.bDetailErrorFlag = true;
                                        return false;
                                    }
                                }
                            }

                            M_HINMEI cond = new M_HINMEI();
                            cond.HINMEI_CD = hinCd;
                            DataTable dt = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataBySqlFile(this.GET_HINMEI_URIAGE_SHIHARAI_DATA_SQL, cond);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                {
                                    if (GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR == dt.Rows[0]["DENPYOU_KBN_CD"].ToString())
                                    {
                                        isHinSet = false;
                                        msgLogic.MessageBoxShow("E020", "品名");
                                        e.Cancel = true;
                                        this.form.bDetailErrorFlag = true;
                                        ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).SelectAll();
                                    }
                                    else
                                    {
                                        isHinSet = true;
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_NAME_RYAKU].Value = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                                    }
                                }

                                if (dt.Rows[0]["UNIT_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["UNIT_CD"].ToString()))
                                {
                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(dt.Rows[0]["UNIT_CD"].ToString()));
                                    if (uni != null)
                                    {
                                        isUniSet = true;
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                    }
                                }
                                if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                {
                                    M_DENPYOU_KBN kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(dt.Rows[0]["DENPYOU_KBN_CD"].ToString());
                                    if (kbn != null)
                                    {
                                        switch (kbn.DENPYOU_KBN_CD.ToString())
                                        {
                                            case GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR:
                                                isKbnSet = true;
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                                                break;

                                            case GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                                                break;

                                            default:
                                                kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(Const.GenbaHoshuConstans.DENPYOU_KBN_CD_URIAGE_STR);
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                                                isKbnSet = true;
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).SelectAll();
                            }
                        }
                        if (e.FormattedValue != null && this.form.beforeValuesForDetail[e.CellName].Equals(e.FormattedValue.ToString()))
                        {
                            isHinSet = true;
                            isUniSet = true;
                            isKbnSet = true;
                        }
                        if (!isHinSet)
                        {
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isUniSet)
                        {
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_UNIT_CD].Value = DBNull.Value;
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_UNIT_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isKbnSet)
                        {
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_CD].Value = DBNull.Value;
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;
                        }
                    }
                }
                // 単価のチェックを行う
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TSUKI_TANKA))
                {
                    int integ_max = 10;
                    int frac_max = 0;
                    switch ((Int16)this.sysinfoEntity.SYS_TANKA_FORMAT_CD)
                    {
                        case 3:
                            integ_max = 9;
                            frac_max = 1;
                            break;

                        case 4:
                            integ_max = 8;
                            frac_max = 2;
                            break;

                        case 5:
                            integ_max = 7;
                            frac_max = 3;
                            break;
                    }

                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        string[] sep = e.FormattedValue.ToString().Replace(",", string.Empty).Split('.');
                        string integ = sep[0];
                        string frac = sep.Length >= 2 ? sep[1] : "0";

                        if (integ.Length > integ_max)
                        {
                            integ = integ.Substring(integ.Length - integ_max);
                        }
                        if (frac.Length > frac_max)
                        {
                            frac = frac.Substring(0, frac_max);
                        }

                        // 数値に変換できなければフォーカスアウトしない
                        if (kingakuConversiontCheck(integ))
                        {
                            // フォーマットは別に設定する
                            this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex].Style.Format = "{0:" + this.sysinfoEntity.SYS_TANKA_FORMAT + "}";

                            decimal tempTanka = 0;
                            if (decimal.TryParse(string.Format("{0:" + this.sysinfoEntity.SYS_TANKA_FORMAT + "}", Decimal.Parse(integ + "." + frac)),
                                    out tempTanka))
                            {
                                this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex].Value = tempTanka;
                            }
                            else
                            {
                                // フォーマットが"#,###"の場合、0だとValueに値を設定できないため、暫定対策として0を設定する
                                this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex].Value = 0;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.form.bDetailErrorFlag = true;
                LogUtility.Error("TsukiHinmeiCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool TsukiHinmeiCellSwitchCdName(CellEventArgs e, GenbaHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case GenbaHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(GenbaHoshuConstans.TSUKI_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TsukiHinmeiIchiran.CurrentCell = this.form.TsukiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TSUKI_UNIT_CD];
                        this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case GenbaHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(GenbaHoshuConstans.TSUKI_UNIT_CD))
                    {
                        this.form.TsukiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TSUKI_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TsukiHinmeiIchiran[e.RowIndex, GenbaHoshuConstans.TSUKI_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 金額(文字列)を数値に変換できるかチェック
        /// </summary>
        /// <param name="kingaku">金額（文字列）</param>
        /// <returns>ture  = 引数を数値に変換できる
        ///          false = 引数を数値に変換できない</returns>
        internal bool kingakuConversiontCheck(string kingaku)
        {
            int output;

            bool chekResult = int.TryParse(kingaku, out output);

            return chekResult;
        }

        /// <summary>
        /// 月極品名一覧入力値確定処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellValidated(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                // 品名CDの大文字対応
                if (e.CellName.Equals(Const.GenbaHoshuConstans.TSUKI_HINMEI_CD))
                {
                    if (this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value = this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString().ToUpper();
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 月極品名一覧行入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiRowValidating(CellCancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 品名、単位の入力チェックを行う
                if (this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value != null
                    && !string.IsNullOrWhiteSpace(this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
                {
                    string hinmeiCd = this.form.TsukiHinmeiIchiran[e.RowIndex, Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString();
                    // 重複チェック
                    foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Index == e.RowIndex) continue;

                        if (row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value != null
                            && !string.IsNullOrWhiteSpace(row[Const.GenbaHoshuConstans.TSUKI_HINMEI_CD].Value.ToString()))
                        {
                            string targetHinmeiCd = row[Const.GenbaHoshuConstans.TEIKI_HINMEI_CD].Value.ToString();

                            if (hinmeiCd.Equals(targetHinmeiCd))
                            {
                                msgLogic.MessageBoxShow("E031", "品名CD");
                                e.Cancel = true;
                            }
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiRowValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 定期回収情報タブ　市区町村コードチェック処理
        /// </summary>
        /// <param name="e"></param>
        public bool ShikuchousonValidating(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (!string.IsNullOrWhiteSpace(this.form.SHIKUCHOUSON_CD.Text))
                {
                    M_SHIKUCHOUSON entity = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>().GetDataByCd(this.form.SHIKUCHOUSON_CD.Text);
                    if (entity == null)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "市区町村");
                        e.Cancel = true;
                        this.form.SHIKUCHOUSON_CD.SelectAll();
                    }
                    else
                    {
                        this.form.SHIKUCHOUSON_NAME_RYAKU.Text = entity.SHIKUCHOUSON_NAME_RYAKU;
                    }
                }
                else
                {
                    this.form.SHIKUCHOUSON_NAME_RYAKU.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShikuchousonValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 請求書拠点の値チェック
        /// </summary>
        public bool SeikyuuKyotenCdValidated(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.SEIKYUU_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (kyo != null && kyo.KYOTEN_CD != 99 && !kyo.DELETE_FLG.IsTrue)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SEIKYUU_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SEIKYUU_KYOTEN_CD.Text = padData;
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                }

                catchErr = false;
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("SeikyuuKyotenCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 支払書拠点の値チェック
        /// </summary>
        public bool ShiharaiKyotenCdValidated(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                catchErr = false;
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_KYOTEN_CD.Text))
                {
                    M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (kyo != null && kyo.KYOTEN_CD != 99 && !kyo.DELETE_FLG.IsTrue)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SHIHARAI_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SHIHARAI_KYOTEN_CD.Text = padData;
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                }
                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("ShiharaiKyotenCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 現場分類区分を変更します
        /// </summary>
        public bool BunruiKbnChanged()
        {
            try
            {
                // マニ記載業者にチェックのない場合は処理不要
                if (this.gyoushaEntity != null && !this.gyoushaEntity.GYOUSHAKBN_MANI)
                {
                    return false;
                }

                if (this.form.TsumikaeHokanKbn.Checked)
                {
                    this.form.ShobunJigyoujouKbn.Checked = false;
                    this.form.SaishuuShobunjouKbn.Checked = false;
                    this.form.ShobunsakiCode.Text = string.Empty;

                    this.form.ShobunJigyoujouKbn.Enabled = false;
                    this.form.SaishuuShobunjouKbn.Enabled = false;
                    this.form.ShobunsakiCode.Enabled = false;
                }
                else
                {
                    this.form.ShobunJigyoujouKbn.Enabled = true;
                    this.form.SaishuuShobunjouKbn.Enabled = true;
                    this.form.ShobunsakiCode.Enabled = true;

                    if (this.gyoushaEntity != null)
                    {
                        if (this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull || !(bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN)
                        {
                            this.form.ShobunJigyoujouKbn.Enabled = false;
                            this.form.SaishuuShobunjouKbn.Enabled = false;
                            this.form.ShobunsakiCode.Enabled = false;
                        }
                    }
                }

                if (this.form.ShobunJigyoujouKbn.Checked || this.form.SaishuuShobunjouKbn.Checked)
                {
                    this.form.TsumikaeHokanKbn.Checked = false;
                    this.form.TsumikaeHokanKbn.Enabled = false;
                }
                else
                {
                    this.form.TsumikaeHokanKbn.Enabled = true;

                    if (this.gyoushaEntity != null)
                    {
                        if (this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull || !(bool)this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN)
                        {
                            this.form.TsumikaeHokanKbn.Enabled = false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BunruiKbnChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 値がNULLまたはDBNullまたは空白であるかどうかをチェックする。
        /// </summary>
        /// <param name="objValue">値</param>
        /// <returns>true:空白　false:空白でない</returns>
        private bool ValueIsNullOrDBNullOrWhiteSpace(object objValue)
        {
            return objValue == null || DBNull.Value.Equals(objValue) || string.IsNullOrWhiteSpace(objValue.ToString());
        }

        /// <summary>
        /// 値がNULLまたはDBNullまたはfalseであるかどうかをチェックする。
        /// </summary>
        /// <param name="objValue">値</param>
        /// <returns>true:false(未チェック)　false:false(未チェック)ではない</returns>
        private bool ValueIsNullOrDBNullOrFalse(object objValue)
        {
            var bTmp = objValue as bool?;
            return !bTmp.HasValue || !bTmp.Value;
        }

        /// <summary>
        /// 値がKgの単位CD：3であることをチェックする。
        /// </summary>
        /// <param name="objValue">値</param>
        /// <returns>true:Kgの単位CDである　false:Kgの単位CDでない</returns>
        private bool ValueIsKgUnitCD(object objValue)
        {
            if (objValue == null)
            {
                return false;
            }
            return "3".Equals(objValue.ToString());
        }

        /// <summary>
        /// 取引先請求情報が「掛け」かどうかを判定
        /// </summary>
        /// <returns>true:掛け　false:現金</returns>
        private bool IsSeikyuuKake()
        {
            var bRet = true;
            var torihikisakiCd = string.Empty;

            if (!string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
            {
                torihikisakiCd = this.form.TorihikisakiCode.Text;
            }
            else if (this.torihikisakiEntity != null && !string.IsNullOrEmpty(this.torihikisakiEntity.TORIHIKISAKI_CD))
            {
                torihikisakiCd = this.torihikisakiEntity.TORIHIKISAKI_CD;
            }

            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return bRet;
            }

            var seikyuuEntity = this.daoSeikyuu.GetDataByCd(torihikisakiCd);
            if (seikyuuEntity != null)
            {
                if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                {
                    bRet = false;
                }
                else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                {
                    bRet = true;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 取引先支払情報が「掛け」かどうかを判定
        /// </summary>
        /// <returns>true:掛け　false:現金</returns>
        private bool IsShiharaiKake()
        {
            var bRet = true;
            var torihikisakiCd = string.Empty;
            if (!string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
            {
                torihikisakiCd = this.form.TorihikisakiCode.Text;
            }
            else if (this.torihikisakiEntity != null && !string.IsNullOrEmpty(this.torihikisakiEntity.TORIHIKISAKI_CD))
            {
                torihikisakiCd = this.torihikisakiEntity.TORIHIKISAKI_CD;
            }
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return bRet;
            }
            var shiharaiEntity = this.daoShiharai.GetDataByCd(torihikisakiCd);
            if (shiharaiEntity != null)
            {
                if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                {
                    bRet = false;
                }
                else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                {
                    bRet = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// 伝票区分が「支払」かどうかを判定
        /// </summary>
        /// <returns>true:支払 false:その他</returns>
        private bool IsDenpyouKbnShiharai()
        {
            var bRet = false;

            var currentRow = this.form.TeikiHinmeiIchiran.CurrentRow;
            var denpyouKbnCell = currentRow[Const.GenbaHoshuConstans.TEIKI_DENPYOU_KBN_CD];
            if (null != denpyouKbnCell.Value && !String.IsNullOrEmpty(denpyouKbnCell.Value.ToString()))
            {
                if (denpyouKbnCell.Value.ToString() == Const.GenbaHoshuConstans.DENPYOU_KBN_CD_SHIHARAI_STR)
                {
                    bRet = true;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 単価適用期間の重複チェックを行います。
        /// </summary>
        /// <param name="checkStartDate">チェックする単価適用開始日</param>
        /// <param name="checkEndDate">チェックする単価適用終了日</param>
        /// <param name="compareStartDate">比較する単価適用開始日</param>
        /// <param name="compareEndDate">比較する単価適用終了日</param>
        /// <returns>重複しているかどうかで true Or false</returns>
        private bool CheckTankaTekiyouKikan(DateTime checkStartDate, DateTime checkEndDate, DateTime compareStartDate, DateTime compareEndDate)
        {
            bool checkDate = true;

            DateTime startDate = (DateTime)checkStartDate;
            DateTime endDate = DateTime.Parse("9999/12/31");

            if (checkEndDate != null && !string.IsNullOrWhiteSpace(checkEndDate.ToString()))
            {
                endDate = (DateTime)checkEndDate;
            }

            DateTime targetStartDate = (DateTime)compareStartDate;
            DateTime targetEndDate = DateTime.Parse("9999/12/31");
            if (compareEndDate != null && !string.IsNullOrWhiteSpace(compareEndDate.ToString()))
            {
                targetEndDate = (DateTime)compareEndDate;
            }

            //チェックパターン①　開始日存在
            if (startDate <= targetStartDate && targetStartDate <= endDate)
            {
                checkDate = false;
            }

            //チェックパターン②　終了日存在
            if (startDate <= targetEndDate && targetEndDate <= endDate)
            {
                checkDate = false;
            }

            //チェックパターン③　開始終了内包
            if (targetStartDate <= startDate && endDate <= targetEndDate)
            {
                checkDate = false;
            }

            return checkDate;
        }

        /// <summary>
        /// 営業拠点部署の値チェック
        /// </summary>
        public bool BushoCdValidated(out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                this.form.EigyouTantouBushoName.Text = string.Empty;
                catchErr = false;
                if (!string.IsNullOrEmpty(this.form.EigyouTantouBushoCode.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.EigyouTantouBushoCode.Text = busho[0].BUSHO_CD.ToString();
                        this.form.EigyouTantouBushoName.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "部署");
                        this.isError = true;
                        ret = false;
                    }
                }

                if (this.isError)
                {
                    this.form.EigyouTantouBushoCode.SelectAll();
                    this.form.EigyouTantouBushoName.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd(ret, catchErr);
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("BushoCdValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                catchErr = true;
                this.isError = true;
                LogUtility.Error("BushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
        }

        /// <summary>
        /// 単位、換算後単位の重複チェック
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns>true:エラー有, false:エラー無</returns>
        private bool ContainsDuplicatedUnitCd(Row row)
        {
            string unitCd = GetCellValueStr(row.Cells[GenbaHoshuConstans.TEIKI_UNIT_CD]);
            string kansanUnitCd = GetCellValueStr(row.Cells[GenbaHoshuConstans.TEIKI_KANSAN_UNIT_CD]);

            if (!string.IsNullOrEmpty(unitCd) && !string.IsNullOrEmpty(kansanUnitCd) && unitCd.Equals(kansanUnitCd))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// セルの値を文字列型で取得します。
        /// nullの場合空文字を返します。
        /// </summary>
        /// <param name="eCell"></param>
        /// <returns></returns>
        private static string GetCellValueStr(Cell cell)
        {
            if (cell.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return cell.Value.ToString();
            }
        }

        /// <summary>
        /// マニフェスト関連の非活性処理を行います
        /// </summary>
        private void ChangeManifestsDisable()
        {
            // マニ手配
            this.form.ManifestShuruiCode.Text = "09";
            this.form.ManifestShuruiCode.Enabled = false;

            M_MANIFEST_SHURUI manishuruiEntity = daoManishurui.GetDataByCd(this.form.ManifestShuruiCode.Text);
            if (manishuruiEntity != null)
            {
                this.form.ManifestShuruiName.Text = manishuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
            }
            else
            {
                this.form.ManifestShuruiName.Text = string.Empty;
            }

            // マニ種類
            this.form.ManifestTehaiCode.Text = "09";
            this.form.ManifestTehaiCode.Enabled = false;

            M_MANIFEST_TEHAI manitehaiEntity = daoManitehai.GetDataByCd(this.form.ManifestTehaiCode.Text);
            if (manitehaiEntity != null)
            {
                this.form.ManifestTehaiName.Text = manitehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
            }
            else
            {
                this.form.ManifestTehaiName.Text = string.Empty;
            }
        }

        #region  A票～E票処理

        /// <summary>
        ///  データを取得し、 A票～E票画面に設定
        /// </summary>
        private void SetAToEWindowsData()
        {
            //【返送先】
            //A票
            if (this._tabPageManager.IsVisible("tabPage1"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_A == 2)
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                    this.form.MANIFEST_USE_2_AHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    this.form.MANIFEST_USE_1_AHyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.HensousakiKbn1_AHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "3";
                            this.form.HensousakiKbn3_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "2";
                            this.form.HensousakiKbn2_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_AHyo.Text = "1";
                                this.form.HensousakiKbn1_AHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_AHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_AHyo.Checked = false;
                                this.form.HensousakiKbn2_AHyo.Checked = false;
                                this.form.HensousakiKbn3_AHyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        this.form.HensousakiKbn_AHyo.Text = "1";
                        this.form.HensousakiKbn1_AHyo.Checked = true;
                        this.form.HensousakiKbn_AHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    }
                }
            }
            //B1票
            if (this._tabPageManager.IsVisible("tabPage2"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_B1 == 2)
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.HensousakiKbn1_B1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "3";
                            this.form.HensousakiKbn3_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "2";
                            this.form.HensousakiKbn2_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B1Hyo.Text = "1";
                                this.form.HensousakiKbn1_B1Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B1Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B1Hyo.Checked = false;
                                this.form.HensousakiKbn2_B1Hyo.Checked = false;
                                this.form.HensousakiKbn3_B1Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        this.form.HensousakiKbn_B1Hyo.Text = "1";
                        this.form.HensousakiKbn1_B1Hyo.Checked = true;
                        this.form.HensousakiKbn_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                    }
                }
            }
            //B2票
            if (this._tabPageManager.IsVisible("tabPage3"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_B2 == 2)
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.HensousakiKbn1_B2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "3";
                            this.form.HensousakiKbn3_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "2";
                            this.form.HensousakiKbn2_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B2Hyo.Text = "1";
                                this.form.HensousakiKbn1_B2Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B2Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B2Hyo.Checked = false;
                                this.form.HensousakiKbn2_B2Hyo.Checked = false;
                                this.form.HensousakiKbn3_B2Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        this.form.HensousakiKbn_B2Hyo.Text = "1";
                        this.form.HensousakiKbn1_B2Hyo.Checked = true;
                        this.form.HensousakiKbn_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                    }
                }
            }
            //B4票
            if (this._tabPageManager.IsVisible("tabPage4"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_B4 == 2)
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.HensousakiKbn1_B4Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "3";
                            this.form.HensousakiKbn3_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "2";
                            this.form.HensousakiKbn2_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B4Hyo.Text = "1";
                                this.form.HensousakiKbn1_B4Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B4Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B4Hyo.Checked = false;
                                this.form.HensousakiKbn2_B4Hyo.Checked = false;
                                this.form.HensousakiKbn3_B4Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        this.form.HensousakiKbn_B4Hyo.Text = "1";
                        this.form.HensousakiKbn1_B4Hyo.Checked = true;
                        this.form.HensousakiKbn_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                    }
                }
            }

            //B6票
            if (this._tabPageManager.IsVisible("tabPage5"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_B6 == 2)
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.HensousakiKbn1_B6Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "3";
                            this.form.HensousakiKbn3_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "2";
                            this.form.HensousakiKbn2_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B6Hyo.Text = "1";
                                this.form.HensousakiKbn1_B6Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_B6Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_B6Hyo.Checked = false;
                                this.form.HensousakiKbn2_B6Hyo.Checked = false;
                                this.form.HensousakiKbn3_B6Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        this.form.HensousakiKbn_B6Hyo.Text = "1";
                        this.form.HensousakiKbn1_B6Hyo.Checked = true;
                        this.form.HensousakiKbn_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                    }
                }
            }

            //C1票
            if (this._tabPageManager.IsVisible("tabPage6"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_C1 == 2)
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.HensousakiKbn1_C1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "3";
                            this.form.HensousakiKbn3_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "2";
                            this.form.HensousakiKbn2_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C1Hyo.Text = "1";
                                this.form.HensousakiKbn1_C1Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C1Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_C1Hyo.Checked = false;
                                this.form.HensousakiKbn2_C1Hyo.Checked = false;
                                this.form.HensousakiKbn3_C1Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        this.form.HensousakiKbn_C1Hyo.Text = "1";
                        this.form.HensousakiKbn1_C1Hyo.Checked = true;
                        this.form.HensousakiKbn_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                    }
                }
            }
            //C2票
            if (this._tabPageManager.IsVisible("tabPage7"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_C2 == 2)
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.HensousakiKbn1_C2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "3";
                            this.form.HensousakiKbn3_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "2";
                            this.form.HensousakiKbn2_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C2Hyo.Text = "1";
                                this.form.HensousakiKbn1_C2Hyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_C2Hyo.Text = string.Empty;
                                this.form.HensousakiKbn1_C2Hyo.Checked = false;
                                this.form.HensousakiKbn2_C2Hyo.Checked = false;
                                this.form.HensousakiKbn3_C2Hyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        this.form.HensousakiKbn_C2Hyo.Text = "1";
                        this.form.HensousakiKbn1_C2Hyo.Checked = true;
                        this.form.HensousakiKbn_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                    }
                }
            }
            //D票
            if (this._tabPageManager.IsVisible("tabPage8"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_D == 2)
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                    this.form.MANIFEST_USE_2_DHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    this.form.MANIFEST_USE_1_DHyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.HensousakiKbn1_DHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "3";
                            this.form.HensousakiKbn3_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "2";
                            this.form.HensousakiKbn2_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_DHyo.Text = "1";
                                this.form.HensousakiKbn1_DHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_DHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_DHyo.Checked = false;
                                this.form.HensousakiKbn2_DHyo.Checked = false;
                                this.form.HensousakiKbn3_DHyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        this.form.HensousakiKbn_DHyo.Text = "1";
                        this.form.HensousakiKbn1_DHyo.Checked = true;
                        this.form.HensousakiKbn_DHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                    }
                }
            }

            //E票
            if (this._tabPageManager.IsVisible("tabPage9"))
            {
                if (this.GenbaEntity.MANI_HENSOUSAKI_USE_E == 2)
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                    this.form.MANIFEST_USE_2_EHyo.Checked = true;
                    if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                    }
                }
                else
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    this.form.MANIFEST_USE_1_EHyo.Checked = true;
                    if (this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.HensousakiKbn1_EHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = GetTorihikisaki(this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "3";
                            this.form.HensousakiKbn3_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = GetGenba(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E, this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "2";
                            this.form.HensousakiKbn2_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_EHyo.Text = "1";
                                this.form.HensousakiKbn1_EHyo.Checked = true;
                                this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                            }
                            else
                            {
                                this.form.HensousakiKbn_EHyo.Text = string.Empty;
                                this.form.HensousakiKbn1_EHyo.Checked = false;
                                this.form.HensousakiKbn2_EHyo.Checked = false;
                                this.form.HensousakiKbn3_EHyo.Checked = false;
                                this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                                this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                                this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                            }
                        }
                        else
                        {
                            this.form.HensousakiKbn_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        this.form.HensousakiKbn_EHyo.Text = "1";
                        this.form.HensousakiKbn1_EHyo.Checked = true;
                        this.form.HensousakiKbn_EHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// どのタブで発生したイベントか判定する
        /// </summary>
        /// <param name="ctlName">コントロール名</param>
        /// <returns>コントロール判定区分</returns>
        public string ChkTabEvent(string ctlName, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(ctlName);
                catchErr = false;

                //A票返送先
                if (ctlName.IndexOf("_AHyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_AHyo", catchErr);
                    return "_AHyo";
                }

                //B1票返送先
                if (ctlName.IndexOf("_B1Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_B1Hyo", catchErr);
                    return "_B1Hyo";
                }

                //B2票返送先
                if (ctlName.IndexOf("_B2Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_B2Hyo", catchErr);
                    return "_B2Hyo";
                }

                //B4票返送先
                if (ctlName.IndexOf("_B4Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_B4Hyo", catchErr);
                    return "_B4Hyo";
                }

                //B6票返送先
                if (ctlName.IndexOf("_B6Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_B6Hyo", catchErr);
                    return "_B6Hyo";
                }

                //C1票返送先
                if (ctlName.IndexOf("_C1Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_C1Hyo", catchErr);
                    return "_C1Hyo";
                }

                //C2票返送先
                if (ctlName.IndexOf("_C2Hyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_C2Hyo", catchErr);
                    return "_C2Hyo";
                }

                //D票返送先
                if (ctlName.IndexOf("_DHyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_DHyo", catchErr);
                    return "_DHyo";
                }

                //E票返送先
                if (ctlName.IndexOf("_EHyo") != -1)
                {
                    LogUtility.DebugMethodEnd("_EHyo", catchErr);
                    return "_EHyo";
                }

                LogUtility.DebugMethodEnd("", catchErr);
                return "";
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ChkTabEvent", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd("", catchErr);
                return "";
            }
        }

        /// <summary>
        /// A票～E票タブ制御
        /// </summary>
        private void ChangeTabAtoE()
        {
            LogUtility.DebugMethodStart();

            //A票
            this._tabPageManager.ChangeTabPageVisible("tabPage1", (this.sysinfoEntity.MANIFEST_USE_A == 1) ? true : false);

            //B1票
            this._tabPageManager.ChangeTabPageVisible("tabPage2", (this.sysinfoEntity.MANIFEST_USE_B1 == 1) ? true : false);

            //B2票
            this._tabPageManager.ChangeTabPageVisible("tabPage3", (this.sysinfoEntity.MANIFEST_USE_B2 == 1) ? true : false);

            //B4票
            this._tabPageManager.ChangeTabPageVisible("tabPage4", (this.sysinfoEntity.MANIFEST_USE_B4 == 1) ? true : false);

            //B6票
            this._tabPageManager.ChangeTabPageVisible("tabPage5", (this.sysinfoEntity.MANIFEST_USE_B6 == 1) ? true : false);

            //C1票
            this._tabPageManager.ChangeTabPageVisible("tabPage6", (this.sysinfoEntity.MANIFEST_USE_C1 == 1) ? true : false);

            //C2票
            this._tabPageManager.ChangeTabPageVisible("tabPage7", (this.sysinfoEntity.MANIFEST_USE_C2 == 1) ? true : false);

            //D票
            this._tabPageManager.ChangeTabPageVisible("tabPage8", (this.sysinfoEntity.MANIFEST_USE_D == 1) ? true : false);

            //E票
            this._tabPageManager.ChangeTabPageVisible("tabPage9", (this.sysinfoEntity.MANIFEST_USE_E == 1) ? true : false);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// マニ種類、マニ手配を動的に変える
        /// </summary>
        internal bool ChangeManiKbn()
        {
            try
            {
                if (!this.form.GyoushaKbnMani.Checked) { return false; }

                // 自動変換前の値を保持
                if (this.form.ManifestShuruiCode.Enabled)
                {
                    maniShuruiSave = this.form.ManifestShuruiCode.Text;
                }
                if (this.form.ManifestTehaiCode.Enabled)
                {
                    maniTehaiSave = this.form.ManifestTehaiCode.Text;
                }

                // 排出事業場、積み替え保管、処分事業場、最終処分場のいずれかにチェックがついている場合
                if (this.form.HaishutsuKbn.Checked || this.form.TsumikaeHokanKbn.Checked || this.form.ShobunJigyoujouKbn.Checked || this.form.SaishuuShobunjouKbn.Checked)
                {
                    this.form.ManifestShuruiCode.Text = maniShuruiSave;
                    this.form.ManifestTehaiCode.Text = maniTehaiSave;
                    if (!string.IsNullOrEmpty(this.form.ManifestShuruiCode.Text))
                    {
                        this.form.ManifestShuruiCode.Text = this.form.ManifestShuruiCode.Text.PadLeft(this.form.ManifestShuruiCode.MaxLength, '0');
                    }
                    if (!string.IsNullOrEmpty(this.form.ManifestTehaiCode.Text))
                    {
                        this.form.ManifestTehaiCode.Text = this.form.ManifestTehaiCode.Text.PadLeft(this.form.ManifestTehaiCode.MaxLength, '0');
                    }
                    // マニ種類、マニ手配を活性状態にする
                    this.form.ManifestShuruiCode.Enabled = true;
                    this.form.ManifestTehaiCode.Enabled = true;
                }
                else
                {
                    // マニ種類、マニ手配に「9:無」を設定
                    this.form.ManifestShuruiCode.Text = "9";
                    this.form.ManifestTehaiCode.Text = "9";
                    this.form.ManifestShuruiCode.Text = this.form.ManifestShuruiCode.Text.PadLeft(this.form.ManifestShuruiCode.MaxLength, '0');
                    this.form.ManifestTehaiCode.Text = this.form.ManifestTehaiCode.Text.PadLeft(this.form.ManifestTehaiCode.MaxLength, '0');
                    // マニ種類、マニ手配を非活性にする
                    this.form.ManifestShuruiCode.Enabled = false;
                    this.form.ManifestTehaiCode.Enabled = false;
                }

                // マニ種類
                M_MANIFEST_SHURUI manishuruiEntity = daoManishurui.GetDataByCd(this.form.ManifestShuruiCode.Text);
                if (manishuruiEntity != null)
                {
                    this.form.ManifestShuruiName.Text = manishuruiEntity.MANIFEST_SHURUI_NAME_RYAKU;
                }
                else
                {
                    this.form.ManifestShuruiName.Text = string.Empty;
                }

                // マニ手配
                M_MANIFEST_TEHAI manitehaiEntity = daoManitehai.GetDataByCd(this.form.ManifestTehaiCode.Text);
                if (manitehaiEntity != null)
                {
                    this.form.ManifestTehaiName.Text = manitehaiEntity.MANIFEST_TEHAI_NAME_RYAKU;
                }
                else
                {
                    this.form.ManifestTehaiName.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeManiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 仮現場エンティティから現場エンティティに内容をコピーします
        /// </summary>
        private void CopyKariGenbaEntityToGenbaEntity()
        {
            this.GenbaEntity = new M_GENBA();
            this.GenbaEntity.BIKOU1 = this.LoadKariGenbaEntity.BIKOU1;
            this.GenbaEntity.BIKOU2 = this.LoadKariGenbaEntity.BIKOU2;
            this.GenbaEntity.BIKOU3 = this.LoadKariGenbaEntity.BIKOU3;
            this.GenbaEntity.BIKOU4 = this.LoadKariGenbaEntity.BIKOU4;
            this.GenbaEntity.BUSHO = this.LoadKariGenbaEntity.BUSHO;
            this.GenbaEntity.CHIIKI_CD = this.LoadKariGenbaEntity.CHIIKI_CD;
            this.GenbaEntity.CHUUSHI_RIYUU1 = this.LoadKariGenbaEntity.CHUUSHI_RIYUU1;
            this.GenbaEntity.CHUUSHI_RIYUU2 = this.LoadKariGenbaEntity.CHUUSHI_RIYUU2;
            this.GenbaEntity.CREATE_DATE = this.LoadKariGenbaEntity.CREATE_DATE;
            this.GenbaEntity.CREATE_PC = this.LoadKariGenbaEntity.CREATE_PC;
            this.GenbaEntity.CREATE_USER = this.LoadKariGenbaEntity.CREATE_USER;
            this.GenbaEntity.DELETE_FLG = this.LoadKariGenbaEntity.DELETE_FLG;
            this.GenbaEntity.DEN_MANI_SHOUKAI_KBN = this.LoadKariGenbaEntity.DEN_MANI_SHOUKAI_KBN;
            this.GenbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.LoadKariGenbaEntity.EIGYOU_TANTOU_BUSHO_CD;
            this.GenbaEntity.EIGYOU_TANTOU_CD = this.LoadKariGenbaEntity.EIGYOU_TANTOU_CD;
            this.GenbaEntity.GENBA_ADDRESS1 = this.LoadKariGenbaEntity.GENBA_ADDRESS1;
            this.GenbaEntity.GENBA_ADDRESS2 = this.LoadKariGenbaEntity.GENBA_ADDRESS2;
            this.GenbaEntity.GENBA_CD = this.LoadKariGenbaEntity.GENBA_CD;
            this.GenbaEntity.GENBA_FAX = this.LoadKariGenbaEntity.GENBA_FAX;
            this.GenbaEntity.GENBA_FURIGANA = this.LoadKariGenbaEntity.GENBA_FURIGANA;
            this.GenbaEntity.GENBA_KEISHOU1 = this.LoadKariGenbaEntity.GENBA_KEISHOU1;
            this.GenbaEntity.GENBA_KEISHOU2 = this.LoadKariGenbaEntity.GENBA_KEISHOU2;
            this.GenbaEntity.GENBA_KEITAI_TEL = this.LoadKariGenbaEntity.GENBA_KEITAI_TEL;
            this.GenbaEntity.GENBA_NAME_RYAKU = this.LoadKariGenbaEntity.GENBA_NAME_RYAKU;
            this.GenbaEntity.GENBA_NAME1 = this.LoadKariGenbaEntity.GENBA_NAME1;
            this.GenbaEntity.GENBA_NAME2 = this.LoadKariGenbaEntity.GENBA_NAME2;
            this.GenbaEntity.GENBA_POST = this.LoadKariGenbaEntity.GENBA_POST;
            this.GenbaEntity.GENBA_TEL = this.LoadKariGenbaEntity.GENBA_TEL;
            this.GenbaEntity.GENBA_TODOUFUKEN_CD = this.LoadKariGenbaEntity.GENBA_TODOUFUKEN_CD;
            this.GenbaEntity.GYOUSHA_CD = this.LoadKariGenbaEntity.GYOUSHA_CD;
            this.GenbaEntity.GYOUSHU_CD = this.LoadKariGenbaEntity.GYOUSHU_CD;
            this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = this.LoadKariGenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
            this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN = this.LoadKariGenbaEntity.ITAKU_KEIYAKU_USE_KBN;
            this.GenbaEntity.JISHA_KBN = this.LoadKariGenbaEntity.JISHA_KBN;
            this.GenbaEntity.KENSHU_YOUHI = this.LoadKariGenbaEntity.KENSHU_YOUHI;
            this.GenbaEntity.KOUFU_TANTOUSHA = this.LoadKariGenbaEntity.KOUFU_TANTOUSHA;
            this.GenbaEntity.KYOTEN_CD = this.LoadKariGenbaEntity.KYOTEN_CD;
            this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_ADDRESS1;
            this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_ADDRESS2;
            this.GenbaEntity.MANI_HENSOUSAKI_BUSHO = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_BUSHO;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_KBN = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_KBN;
            this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_KEISHOU1;
            this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_KEISHOU2;
            this.GenbaEntity.MANI_HENSOUSAKI_NAME1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_NAME1;
            this.GenbaEntity.MANI_HENSOUSAKI_NAME2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_NAME2;
            this.GenbaEntity.MANI_HENSOUSAKI_POST = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_POST;
            this.GenbaEntity.MANI_HENSOUSAKI_TANTOU = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TANTOU;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_A = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_A;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B4 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B6 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_C1 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_C2 = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_D = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_D;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_E = this.LoadKariGenbaEntity.MANI_HENSOUSAKI_USE_E;
            this.GenbaEntity.MANIFEST_SHURUI_CD = this.LoadKariGenbaEntity.MANIFEST_SHURUI_CD;
            this.GenbaEntity.MANIFEST_TEHAI_CD = this.LoadKariGenbaEntity.MANIFEST_TEHAI_CD;
            this.GenbaEntity.SAISHUU_SHOBUNJOU_KBN = this.LoadKariGenbaEntity.SAISHUU_SHOBUNJOU_KBN;
            this.GenbaEntity.SEARCH_CREATE_DATE = this.LoadKariGenbaEntity.SEARCH_CREATE_DATE;
            this.GenbaEntity.SEARCH_TEKIYOU_BEGIN = this.LoadKariGenbaEntity.SEARCH_TEKIYOU_BEGIN;
            this.GenbaEntity.SEARCH_TEKIYOU_END = this.LoadKariGenbaEntity.SEARCH_TEKIYOU_END;
            this.GenbaEntity.SEARCH_UPDATE_DATE = this.LoadKariGenbaEntity.SEARCH_UPDATE_DATE;
            this.GenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadKariGenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.GenbaEntity.SEIKYUU_KYOTEN_CD = this.LoadKariGenbaEntity.SEIKYUU_KYOTEN_CD;
            this.GenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadKariGenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
            this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_ADDRESS1;
            this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_ADDRESS2;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.GenbaEntity.HAKKOUSAKI_CD = this.LoadKariGenbaEntity.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.GenbaEntity.SEIKYUU_SOUFU_BUSHO = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_BUSHO;
            this.GenbaEntity.SEIKYUU_SOUFU_FAX = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_FAX;
            this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_KEISHOU1;
            this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_KEISHOU2;
            this.GenbaEntity.SEIKYUU_SOUFU_NAME1 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_NAME1;
            this.GenbaEntity.SEIKYUU_SOUFU_NAME2 = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_NAME2;
            this.GenbaEntity.SEIKYUU_SOUFU_POST = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_POST;
            this.GenbaEntity.SEIKYUU_SOUFU_TANTOU = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_TANTOU;
            this.GenbaEntity.SEIKYUU_SOUFU_TEL = this.LoadKariGenbaEntity.SEIKYUU_SOUFU_TEL;
            this.GenbaEntity.SEIKYUU_TANTOU = this.LoadKariGenbaEntity.SEIKYUU_TANTOU;
            this.GenbaEntity.SHIHARAI_KYOTEN_CD = this.LoadKariGenbaEntity.SHIHARAI_KYOTEN_CD;
            this.GenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadKariGenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
            this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_ADDRESS1;
            this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_ADDRESS2;
            this.GenbaEntity.SHIHARAI_SOUFU_BUSHO = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_BUSHO;
            this.GenbaEntity.SHIHARAI_SOUFU_FAX = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_FAX;
            this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_KEISHOU1;
            this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_KEISHOU2;
            this.GenbaEntity.SHIHARAI_SOUFU_NAME1 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_NAME1;
            this.GenbaEntity.SHIHARAI_SOUFU_NAME2 = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_NAME2;
            this.GenbaEntity.SHIHARAI_SOUFU_POST = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_POST;
            this.GenbaEntity.SHIHARAI_SOUFU_TANTOU = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_TANTOU;
            this.GenbaEntity.SHIHARAI_SOUFU_TEL = this.LoadKariGenbaEntity.SHIHARAI_SOUFU_TEL;
            this.GenbaEntity.SHIKUCHOUSON_CD = this.LoadKariGenbaEntity.SHIKUCHOUSON_CD;
            this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = this.LoadKariGenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN;
            this.GenbaEntity.SHOBUNSAKI_NO = this.LoadKariGenbaEntity.SHOBUNSAKI_NO;
            this.GenbaEntity.SHOKUCHI_KBN = this.LoadKariGenbaEntity.SHOKUCHI_KBN;
            this.GenbaEntity.SHUUKEI_ITEM_CD = this.LoadKariGenbaEntity.SHUUKEI_ITEM_CD;
            this.GenbaEntity.TANTOUSHA = this.LoadKariGenbaEntity.TANTOUSHA;
            this.GenbaEntity.TEKIYOU_BEGIN = this.LoadKariGenbaEntity.TEKIYOU_BEGIN;
            this.GenbaEntity.TEKIYOU_END = this.LoadKariGenbaEntity.TEKIYOU_END;
            this.GenbaEntity.TORIHIKI_JOUKYOU = this.LoadKariGenbaEntity.TORIHIKI_JOUKYOU;
            this.GenbaEntity.TORIHIKISAKI_CD = this.LoadKariGenbaEntity.TORIHIKISAKI_CD;
            this.GenbaEntity.TSUMIKAEHOKAN_KBN = this.LoadKariGenbaEntity.TSUMIKAEHOKAN_KBN;
            this.GenbaEntity.UPDATE_DATE = this.LoadKariGenbaEntity.UPDATE_DATE;
            this.GenbaEntity.UPDATE_PC = this.LoadKariGenbaEntity.UPDATE_PC;
            this.GenbaEntity.UPDATE_USER = this.LoadKariGenbaEntity.UPDATE_USER;
        }

        /// <summary>
        /// 引合現場エンティティから現場エンティティに内容をコピーします
        /// </summary>
        private void CopyHikiaiGenbaEntityToGenbaEntity()
        {
            this.GenbaEntity = new M_GENBA();
            if (this.LoadHikiaiGenbaEntity == null)
            {
                return;
            }
            this.GenbaEntity.BIKOU1 = this.LoadHikiaiGenbaEntity.BIKOU1;
            this.GenbaEntity.BIKOU2 = this.LoadHikiaiGenbaEntity.BIKOU2;
            this.GenbaEntity.BIKOU3 = this.LoadHikiaiGenbaEntity.BIKOU3;
            this.GenbaEntity.BIKOU4 = this.LoadHikiaiGenbaEntity.BIKOU4;
            this.GenbaEntity.BUSHO = this.LoadHikiaiGenbaEntity.BUSHO;
            this.GenbaEntity.CHIIKI_CD = this.LoadHikiaiGenbaEntity.CHIIKI_CD;
            this.GenbaEntity.CHUUSHI_RIYUU1 = this.LoadHikiaiGenbaEntity.CHUUSHI_RIYUU1;
            this.GenbaEntity.CHUUSHI_RIYUU2 = this.LoadHikiaiGenbaEntity.CHUUSHI_RIYUU2;
            this.GenbaEntity.CREATE_DATE = this.LoadHikiaiGenbaEntity.CREATE_DATE;
            this.GenbaEntity.CREATE_PC = this.LoadHikiaiGenbaEntity.CREATE_PC;
            this.GenbaEntity.CREATE_USER = this.LoadHikiaiGenbaEntity.CREATE_USER;
            this.GenbaEntity.DELETE_FLG = this.LoadHikiaiGenbaEntity.DELETE_FLG;
            this.GenbaEntity.DEN_MANI_SHOUKAI_KBN = this.LoadHikiaiGenbaEntity.DEN_MANI_SHOUKAI_KBN;
            this.GenbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.LoadHikiaiGenbaEntity.EIGYOU_TANTOU_BUSHO_CD;
            this.GenbaEntity.EIGYOU_TANTOU_CD = this.LoadHikiaiGenbaEntity.EIGYOU_TANTOU_CD;
            this.GenbaEntity.GENBA_ADDRESS1 = this.LoadHikiaiGenbaEntity.GENBA_ADDRESS1;
            this.GenbaEntity.GENBA_ADDRESS2 = this.LoadHikiaiGenbaEntity.GENBA_ADDRESS2;
            this.GenbaEntity.GENBA_CD = this.LoadHikiaiGenbaEntity.GENBA_CD;
            this.GenbaEntity.GENBA_FAX = this.LoadHikiaiGenbaEntity.GENBA_FAX;
            this.GenbaEntity.GENBA_FURIGANA = this.LoadHikiaiGenbaEntity.GENBA_FURIGANA;
            this.GenbaEntity.GENBA_KEISHOU1 = this.LoadHikiaiGenbaEntity.GENBA_KEISHOU1;
            this.GenbaEntity.GENBA_KEISHOU2 = this.LoadHikiaiGenbaEntity.GENBA_KEISHOU2;
            this.GenbaEntity.GENBA_KEITAI_TEL = this.LoadHikiaiGenbaEntity.GENBA_KEITAI_TEL;
            this.GenbaEntity.GENBA_NAME_RYAKU = this.LoadHikiaiGenbaEntity.GENBA_NAME_RYAKU;
            this.GenbaEntity.GENBA_NAME1 = this.LoadHikiaiGenbaEntity.GENBA_NAME1;
            this.GenbaEntity.GENBA_NAME2 = this.LoadHikiaiGenbaEntity.GENBA_NAME2;
            this.GenbaEntity.GENBA_POST = this.LoadHikiaiGenbaEntity.GENBA_POST;
            this.GenbaEntity.GENBA_TEL = this.LoadHikiaiGenbaEntity.GENBA_TEL;
            this.GenbaEntity.GENBA_TODOUFUKEN_CD = this.LoadHikiaiGenbaEntity.GENBA_TODOUFUKEN_CD;
            this.GenbaEntity.GYOUSHA_CD = this.LoadHikiaiGenbaEntity.GYOUSHA_CD;
            this.GenbaEntity.GYOUSHU_CD = this.LoadHikiaiGenbaEntity.GYOUSHU_CD;
            this.GenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = this.LoadHikiaiGenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
            this.GenbaEntity.ITAKU_KEIYAKU_USE_KBN = this.LoadHikiaiGenbaEntity.ITAKU_KEIYAKU_USE_KBN;
            this.GenbaEntity.JISHA_KBN = this.LoadHikiaiGenbaEntity.JISHA_KBN;
            this.GenbaEntity.KENSHU_YOUHI = this.LoadHikiaiGenbaEntity.KENSHU_YOUHI;
            this.GenbaEntity.KOUFU_TANTOUSHA = this.LoadHikiaiGenbaEntity.KOUFU_TANTOUSHA;
            this.GenbaEntity.KYOTEN_CD = this.LoadHikiaiGenbaEntity.KYOTEN_CD;
            this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1;
            this.GenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2;
            this.GenbaEntity.MANI_HENSOUSAKI_BUSHO = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_KBN = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_KBN;
            this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1;
            this.GenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2;
            this.GenbaEntity.MANI_HENSOUSAKI_NAME1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1;
            this.GenbaEntity.MANI_HENSOUSAKI_NAME2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2;
            this.GenbaEntity.MANI_HENSOUSAKI_POST = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_POST;
            this.GenbaEntity.MANI_HENSOUSAKI_TANTOU = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
            this.GenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_A = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_A;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B4 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_B6 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_C1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_C2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_D = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_D;
            this.GenbaEntity.MANI_HENSOUSAKI_USE_E = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_USE_E;
            this.GenbaEntity.MANIFEST_SHURUI_CD = this.LoadHikiaiGenbaEntity.MANIFEST_SHURUI_CD;
            this.GenbaEntity.MANIFEST_TEHAI_CD = this.LoadHikiaiGenbaEntity.MANIFEST_TEHAI_CD;
            this.GenbaEntity.SAISHUU_SHOBUNJOU_KBN = this.LoadHikiaiGenbaEntity.SAISHUU_SHOBUNJOU_KBN;
            this.GenbaEntity.SEARCH_CREATE_DATE = this.LoadHikiaiGenbaEntity.SEARCH_CREATE_DATE;
            this.GenbaEntity.SEARCH_TEKIYOU_BEGIN = this.LoadHikiaiGenbaEntity.SEARCH_TEKIYOU_BEGIN;
            this.GenbaEntity.SEARCH_TEKIYOU_END = this.LoadHikiaiGenbaEntity.SEARCH_TEKIYOU_END;
            this.GenbaEntity.SEARCH_UPDATE_DATE = this.LoadHikiaiGenbaEntity.SEARCH_UPDATE_DATE;
            this.GenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.LoadHikiaiGenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.GenbaEntity.SEIKYUU_KYOTEN_CD = this.LoadHikiaiGenbaEntity.SEIKYUU_KYOTEN_CD;
            this.GenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.LoadHikiaiGenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
            this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_ADDRESS1;
            this.GenbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_ADDRESS2;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.GenbaEntity.HAKKOUSAKI_CD = this.LoadHikiaiGenbaEntity.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.GenbaEntity.SEIKYUU_SOUFU_BUSHO = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_BUSHO;
            this.GenbaEntity.SEIKYUU_SOUFU_FAX = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_FAX;
            this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_KEISHOU1;
            this.GenbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_KEISHOU2;
            this.GenbaEntity.SEIKYUU_SOUFU_NAME1 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_NAME1;
            this.GenbaEntity.SEIKYUU_SOUFU_NAME2 = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_NAME2;
            this.GenbaEntity.SEIKYUU_SOUFU_POST = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_POST;
            this.GenbaEntity.SEIKYUU_SOUFU_TANTOU = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_TANTOU;
            this.GenbaEntity.SEIKYUU_SOUFU_TEL = this.LoadHikiaiGenbaEntity.SEIKYUU_SOUFU_TEL;
            this.GenbaEntity.SEIKYUU_TANTOU = this.LoadHikiaiGenbaEntity.SEIKYUU_TANTOU;
            this.GenbaEntity.SHIHARAI_KYOTEN_CD = this.LoadHikiaiGenbaEntity.SHIHARAI_KYOTEN_CD;
            this.GenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.LoadHikiaiGenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
            this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_ADDRESS1;
            this.GenbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_ADDRESS2;
            this.GenbaEntity.SHIHARAI_SOUFU_BUSHO = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_BUSHO;
            this.GenbaEntity.SHIHARAI_SOUFU_FAX = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_FAX;
            this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_KEISHOU1;
            this.GenbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_KEISHOU2;
            this.GenbaEntity.SHIHARAI_SOUFU_NAME1 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_NAME1;
            this.GenbaEntity.SHIHARAI_SOUFU_NAME2 = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_NAME2;
            this.GenbaEntity.SHIHARAI_SOUFU_POST = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_POST;
            this.GenbaEntity.SHIHARAI_SOUFU_TANTOU = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_TANTOU;
            this.GenbaEntity.SHIHARAI_SOUFU_TEL = this.LoadHikiaiGenbaEntity.SHIHARAI_SOUFU_TEL;
            this.GenbaEntity.SHIKUCHOUSON_CD = this.LoadHikiaiGenbaEntity.SHIKUCHOUSON_CD;
            this.GenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = this.LoadHikiaiGenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN;
            this.GenbaEntity.SHOBUNSAKI_NO = this.LoadHikiaiGenbaEntity.SHOBUNSAKI_NO;
            this.GenbaEntity.SHOKUCHI_KBN = this.LoadHikiaiGenbaEntity.SHOKUCHI_KBN;
            this.GenbaEntity.SHUUKEI_ITEM_CD = this.LoadHikiaiGenbaEntity.SHUUKEI_ITEM_CD;
            this.GenbaEntity.TANTOUSHA = this.LoadHikiaiGenbaEntity.TANTOUSHA;
            this.GenbaEntity.TEKIYOU_BEGIN = this.LoadHikiaiGenbaEntity.TEKIYOU_BEGIN;
            this.GenbaEntity.TEKIYOU_END = this.LoadHikiaiGenbaEntity.TEKIYOU_END;
            this.GenbaEntity.TORIHIKI_JOUKYOU = this.LoadHikiaiGenbaEntity.TORIHIKI_JOUKYOU;
            this.GenbaEntity.TORIHIKISAKI_CD = this.LoadHikiaiGenbaEntity.TORIHIKISAKI_CD;
            this.GenbaEntity.TSUMIKAEHOKAN_KBN = this.LoadHikiaiGenbaEntity.TSUMIKAEHOKAN_KBN;
            this.GenbaEntity.UPDATE_DATE = this.LoadHikiaiGenbaEntity.UPDATE_DATE;
            this.GenbaEntity.UPDATE_PC = this.LoadHikiaiGenbaEntity.UPDATE_PC;
            this.GenbaEntity.UPDATE_USER = this.LoadHikiaiGenbaEntity.UPDATE_USER;
            this.GenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.LoadHikiaiGenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            //20150609 #10697「運転者指示事項」の項目を追加する。hoanghm start
            this.GenbaEntity.UNTENSHA_SHIJI_JIKOU1 = this.LoadHikiaiGenbaEntity.UNTENSHA_SHIJI_JIKOU1;
            this.GenbaEntity.UNTENSHA_SHIJI_JIKOU2 = this.LoadHikiaiGenbaEntity.UNTENSHA_SHIJI_JIKOU2;
            this.GenbaEntity.UNTENSHA_SHIJI_JIKOU3 = this.LoadHikiaiGenbaEntity.UNTENSHA_SHIJI_JIKOU3;
            //20150609 #10697「運転者指示事項」の項目を追加する。hoanghm end

            this.GenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D;
            this.GenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = this.LoadHikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E;
        }

        internal void UpdateHikiaiGenba(M_HIKIAI_GENBA entity)
        {
            var dao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();
            dao.Update(entity);
        }

        /// <summary>
        /// 現在のアクティブなタブに基づいてサブファンクションの活性・非活性を切り替えます
        /// </summary>
        internal bool SubFunctionEnabledChenge()
        {
            try
            {
                var tabName = this.form.ManiHensousakiKeishou2B1.SelectedTab.Name;
                var parentForm = (BusinessBaseForm)this.form.Parent;
                switch (tabName)
                {
                    // 返送先A～E表のタブの時のみ、「返送先コピー」ボタンを活性に切り替えます
                    case ("tabPage1"):
                    case ("tabPage2"):
                    case ("tabPage3"):
                    case ("tabPage4"):
                    case ("tabPage5"):
                    case ("tabPage6"):
                    case ("tabPage7"):
                    case ("tabPage8"):
                    case ("tabPage9"):
                        // 参照、削除モード時は非活性
                        if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                            this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            parentForm.bt_process1.Enabled = false;
                        }
                        else
                        {
                            parentForm.bt_process1.Enabled = true;
                        }
                        break;

                    default:
                        parentForm.bt_process1.Enabled = false;
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SubFunctionEnabledChenge", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 現在表示している返送先情報を変数にコピー
        /// </summary>
        /// <returns>コピーした返送先</returns>
        internal MANIFESUTO_HENSOUSAKI HensousakiCopy(out bool catchErr)
        {
            try
            {
                var motoHensousaki = new MANIFESUTO_HENSOUSAKI();
                var tabName = this.form.ManiHensousakiKeishou2B1.SelectedTab.Name;

                motoHensousaki.MANI_HENSOUSAKI_NAME1 = this.form.ManiHensousakiName1.Text;
                motoHensousaki.MANI_HENSOUSAKI_NAME2 = this.form.ManiHensousakiName2.Text;
                motoHensousaki.MANI_HENSOUSAKI_KEISHOU1 = this.form.ManiHensousakiKeishou1.Text;
                motoHensousaki.MANI_HENSOUSAKI_KEISHOU2 = this.form.ManiHensousakiKeishou2.Text;
                motoHensousaki.MANI_HENSOUSAKI_POST = this.form.ManiHensousakiPost.Text;
                motoHensousaki.MANI_HENSOUSAKIDDRESS1 = this.form.ManiHensousakiAddress1.Text;
                motoHensousaki.MANI_HENSOUSAKIDDRESS2 = this.form.ManiHensousakiAddress2.Text;
                motoHensousaki.MANI_HENSOUSAKI_BUSHO = this.form.ManiHensousakiBusho.Text;
                motoHensousaki.MANI_HENSOUSAKI_TANTOU = this.form.ManiHensousakiTantou.Text;

                switch (tabName)
                {
                    case ("tabPage1"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_AHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_AHyo.Text;
                        break;

                    case ("tabPage2"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_B1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_B1Hyo.Text;
                        break;

                    case ("tabPage3"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_B2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_B2Hyo.Text;
                        break;

                    case ("tabPage4"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_B4Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_B4Hyo.Text;
                        break;

                    case ("tabPage5"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_B6Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_B6Hyo.Text;
                        break;

                    case ("tabPage6"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_C1Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_C1Hyo.Text;
                        break;

                    case ("tabPage7"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_C2Hyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_C2Hyo.Text;
                        break;

                    case ("tabPage8"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_DHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_DHyo.Text;
                        break;

                    case ("tabPage9"):
                        motoHensousaki.MANI_HENSOUSAKI_USE = this.form.MANIFEST_USE_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_PLACE_KBN = this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_KBN = this.form.HensousakiKbn_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.ManiHensousakiTorihikisakiName_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME = this.form.ManiHensousakiGyoushaName_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode_EHyo.Text;
                        motoHensousaki.MANI_HENSOUSAKI_GENBA_NAME = this.form.ManiHensousakiGenbaName_EHyo.Text;
                        break;

                    default:
                        break;
                }
                catchErr = false;
                return motoHensousaki;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("HensousakiCopy", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return new MANIFESUTO_HENSOUSAKI();
            }
        }

        /// <summary>
        /// 現在表示している返送先情報を変数にコピー
        /// </summary>
        /// <param name="saveHensousaki">ペースト元返送先情報</param>
        /// <param name="targetTab">対象タブ名称</param>
        internal bool HensousakiPaste(MANIFESUTO_HENSOUSAKI saveHensousaki, string targetTab)
        {
            try
            {
                this.form.ManiHensousakiName1.Text = saveHensousaki.MANI_HENSOUSAKI_NAME1;
                this.form.ManiHensousakiName2.Text = saveHensousaki.MANI_HENSOUSAKI_NAME2;
                this.form.ManiHensousakiKeishou1.Text = saveHensousaki.MANI_HENSOUSAKI_KEISHOU1;
                this.form.ManiHensousakiKeishou2.Text = saveHensousaki.MANI_HENSOUSAKI_KEISHOU2;
                this.form.ManiHensousakiPost.Text = saveHensousaki.MANI_HENSOUSAKI_POST;
                this.form.ManiHensousakiAddress1.Text = saveHensousaki.MANI_HENSOUSAKIDDRESS1;
                this.form.ManiHensousakiAddress2.Text = saveHensousaki.MANI_HENSOUSAKIDDRESS2;
                this.form.ManiHensousakiBusho.Text = saveHensousaki.MANI_HENSOUSAKI_BUSHO;
                this.form.ManiHensousakiTantou.Text = saveHensousaki.MANI_HENSOUSAKI_TANTOU;

                switch (targetTab)
                {
                    case ("tabPage1"):
                        if (this.sysinfoEntity.MANIFEST_USE_A.Value == 1)
                        {
                            this.form.MANIFEST_USE_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage2"):
                        if (this.sysinfoEntity.MANIFEST_USE_B1.Value == 1)
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage3"):
                        if (this.sysinfoEntity.MANIFEST_USE_B2.Value == 1)
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage4"):
                        if (this.sysinfoEntity.MANIFEST_USE_B4.Value == 1)
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage5"):
                        if (this.sysinfoEntity.MANIFEST_USE_B6.Value == 1)
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage6"):
                        if (this.sysinfoEntity.MANIFEST_USE_C1.Value == 1)
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage7"):
                        if (this.sysinfoEntity.MANIFEST_USE_C2.Value == 1)
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage8"):
                        if (this.sysinfoEntity.MANIFEST_USE_D.Value == 1)
                        {
                            this.form.MANIFEST_USE_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    case ("tabPage9"):
                        if (this.sysinfoEntity.MANIFEST_USE_E.Value == 1)
                        {
                            this.form.MANIFEST_USE_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_USE;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_PLACE_KBN;
                            this.form.HensousakiKbn_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_KBN;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_TORIHIKISAKI_NAME;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_CD;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GYOUSHA_NAME;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_CD;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_NAME;
                        }
                        break;

                    default:
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HensousakiPaste", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// 20141217 Houkakou 「現場入力」の日付チェックを追加する　start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.TekiyouKikanForm.BackColor = Constans.NOMAL_COLOR;
                this.form.TekiyouKikanTo.BackColor = Constans.NOMAL_COLOR;

                var courseFrom = DateTime.MinValue;
                var courseTo = DateTime.MaxValue;
                var isNoCourse = false;

                // 業者、現場に該当するM_COURSE_DETAILからM_COURSE_DETAIL_ITEMSの情報を取得する
                M_COURSE_DETAIL data = new M_COURSE_DETAIL() { GYOUSHA_CD = this.form.GyoushaCode.Text, GENBA_CD = this.form.GenbaCode.Text };
                var courseData = this.daoCourseDetail.GetCourseDetailItemsData(data);
                if (courseData.Rows.Count != 0)
                {
                    // 取得してきたコースデータの適用開始日で一番過去の日付を取得。
                    List<DataRow> tekiyouBeginNullList = courseData.AsEnumerable().Where(row => row["TEKIYOU_BEGIN"].Equals(DBNull.Value)).ToList();
                    if (tekiyouBeginNullList.Count == 0)
                    {
                        courseFrom = courseData.AsEnumerable().Min(row => row.Field<DateTime>("TEKIYOU_BEGIN"));
                    }
                    // 取得してきたコースデータの適用終了日で一番未来の日付を取得。
                    List<DataRow> tekiyouEndNullList = courseData.AsEnumerable().Where(row => row["TEKIYOU_END"].Equals(DBNull.Value)).ToList();
                    if (tekiyouEndNullList.Count == 0)
                    {
                        courseTo = courseData.AsEnumerable().Max(row => row.Field<DateTime>("TEKIYOU_END"));
                    }
                }
                else
                {
                    isNoCourse = true;
                }


                DateTime date_from = DateTime.MinValue;
                DateTime date_to = DateTime.MaxValue;
                if (!string.IsNullOrWhiteSpace(this.form.TekiyouKikanForm.Text))
                {
                    DateTime.TryParse(this.form.TekiyouKikanForm.Text, out date_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TekiyouKikanTo.Text))
                {
                    DateTime.TryParse(this.form.TekiyouKikanTo.Text, out date_to);
                }

                DateTime torihiki_from = DateTime.MinValue;
                DateTime torihiki_to = DateTime.MaxValue;
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text, out torihiki_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_END.Text, out torihiki_to);
                }

                DateTime gyousha_from = DateTime.MinValue;
                DateTime gyousha_to = DateTime.MaxValue;
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.GYOUSHA_TEKIYOU_BEGIN.Text, out gyousha_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.GYOUSHA_TEKIYOU_END.Text, out gyousha_to);
                }

                // 現場適用開始日 < 取引先適用開始日 場合
                if (date_from.CompareTo(torihiki_from) < 0)
                {
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用開始日", "現場", "取引先", "前", "以降");
                    this.form.TekiyouKikanForm.Focus();
                    return true;
                }

                // 現場適用開始日 < 業者適用開始日 場合
                if (date_from.CompareTo(gyousha_from) < 0)
                {
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用開始日", "現場", "業者", "前", "以降");
                    this.form.TekiyouKikanForm.Focus();
                    return true;
                }

                // 現場適用開始日 > コース適用開始日 場合
                if (!isNoCourse && courseFrom.CompareTo(date_from) < 0)
                {
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用開始日", "現場", "コース", "後", "以前");
                    this.form.TekiyouKikanForm.Focus();
                    return true;
                }

                // 取引先適用終了日 < 現場適用終了日 場合
                if (torihiki_to.CompareTo(date_to) < 0)
                {
                    this.form.TekiyouKikanTo.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用終了日", "現場", "取引先", "後", "以前");
                    this.form.TekiyouKikanTo.Focus();
                    return true;
                }

                // 業者適用終了日 < 現場適用終了日 場合
                if (gyousha_to.CompareTo(date_to) < 0)
                {
                    this.form.TekiyouKikanTo.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用終了日", "現場", "業者", "後", "以前");
                    this.form.TekiyouKikanTo.Focus();
                    return true;
                }

                // 現場適用終了日 > コース適用終了日 場合
                if (!isNoCourse && date_to.CompareTo(courseTo) < 0)
                {
                    this.form.TekiyouKikanTo.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E255", "適用終了日", "現場", "コース", "前", "以降");
                    this.form.TekiyouKikanTo.Focus();
                    return true;
                }

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    this.form.TekiyouKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "適用期間From", "適用期間To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.TekiyouKikanForm.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        /// 20141217 Houkakou 「現場入力」の日付チェックを追加する　end

        // 20141208 ブン 運搬報告書提出先を追加する start
        /// <summary>
        /// 運搬報告書提出先を取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        public bool SearchsetUpanHoukokushoTeishutsu()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text))
            {
                M_CHIIKI search = new M_CHIIKI();
                search.CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                M_CHIIKI[] chiikiDto = this.daoChiiki.GetAllValidData(search);
                if (chiikiDto != null && chiikiDto.Length > 0)
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = chiikiDto[0].CHIIKI_CD.ToString();
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = chiikiDto[0].CHIIKI_NAME_RYAKU.ToString();
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "地域");
                    this.isError = true;
                    ret = false;
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Focus();
                }
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 運搬報告書提出先データ取得処理(地域)
        /// </summary>
        /// <returns></returns>
        public int SearchUpnHoukokushoTeishutsuChiiki()
        {
            LogUtility.DebugMethodStart();

            this.upnHoukokushoTeishutsuChiikiEntity = null;

            this.upnHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.GenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);

            int count = this.upnHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        // 20141208 ブン 運搬報告書提出先を追加する end

        // VUNGUYEN 20150525 #1294 START
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TekiyouKikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.TekiyouKikanTo.Text = this.form.TekiyouKikanForm.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録データをチェックする。
        /// </summary>
        public bool CheckRegistData()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.TekiyouKikanForm.Text))
                {
                    var result = this.form.errmessage.MessageBoxShow("E001", "適用開始日");
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    return true;
                }


                if (!string.IsNullOrEmpty(this.form.TorihikisakiCode.Text) && WINDOW_TYPE.DELETE_WINDOW_FLAG != this.form.WindowType)
                {
                    var TorihikisakiSeikyuu = daoSeikyuu.GetDataByCd(this.form.TorihikisakiCode.Text);
                    var TorihikisakiShiharai = daoShiharai.GetDataByCd(this.form.TorihikisakiCode.Text);

                    //check 請求書書式１ SHOSHIKI_KBN
                    string sMsg = string.Empty;
                    if (TorihikisakiSeikyuu != null)
                    {
                        if (!TorihikisakiSeikyuu.SHOSHIKI_KBN.IsNull)
                        {
                            if (TorihikisakiSeikyuu.SHOSHIKI_KBN.Value == 3)
                            {
                                if (string.IsNullOrEmpty(this.form.SeikyuushoSoufusaki1.Text))
                                {
                                    sMsg += Const.GenbaHoshuConstans.MSG_CONF_C_SEIKYUU;
                                    sMsg += "\n";
                                }
                                //check SEIKYUU_SOUFU_ADDRESS1
                                if (string.IsNullOrEmpty(this.form.SeikyuuSoufuAddress1.Text))
                                {
                                    sMsg += Const.GenbaHoshuConstans.MSG_CONF_D_SEIKYUU;
                                    sMsg += "\n";
                                }
                            }
                        }
                    }

                    //check 支払明細書書式１ SHIHARAI_SHOSHIKI_KBN
                    if (TorihikisakiShiharai != null)
                    {
                        if (!TorihikisakiShiharai.SHOSHIKI_KBN.IsNull)
                        {
                            if (TorihikisakiShiharai.SHOSHIKI_KBN.Value == 3)
                            {
                                if (string.IsNullOrEmpty(this.form.ShiharaiSoufuName1.Text))
                                {
                                    sMsg += Const.GenbaHoshuConstans.MSG_CONF_E_SHIHARAI;
                                    sMsg += "\n";
                                }
                                //check SHIHARAI_SOUFU_ADDRESS1
                                if (string.IsNullOrEmpty(this.form.ShiharaiSoufuAddress1.Text))
                                {
                                    sMsg += Const.GenbaHoshuConstans.MSG_CONF_F_SHIHARAI;
                                    sMsg += "\n";
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(sMsg))
                    {
                        var dlg = this.form.errmessage.MessageBoxShowConfirm(sMsg);
                        if (dlg == DialogResult.No)
                        {
                            return true;
                        }
                    }
                }

                foreach (Row tsukiRow in this.form.TsukiHinmeiIchiran.Rows)
                {
                    if (tsukiRow.IsNewRow || Convert.ToString(tsukiRow.Cells["DELETE_FLG"].Value) != "True") continue;
                    var cell = tsukiRow.Cells[GenbaHoshuConstans.TSUKI_HINMEI_CD];
                    string hinmeiCd = Convert.ToString(cell.Value);
                    // 定期使用チェック
                    foreach (Row teikiRow in this.form.TeikiHinmeiIchiran.Rows)
                    {
                        if (teikiRow.IsNewRow || Convert.ToString(teikiRow.Cells["DELETE_FLG"].Value) == "True") continue;

                        if (!string.IsNullOrEmpty(Convert.ToString(teikiRow[GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value))
                            && hinmeiCd.Equals(Convert.ToString(teikiRow[GenbaHoshuConstans.TEIKI_TSUKI_HINMEI_CD].Value)))
                        {
                            cell.Style.BackColor = Constans.ERROR_COLOR;
                            this.form.errmessage.MessageBoxShow("E086", "当品名CD", "定期回収タブ内", "削除");
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegistData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを使用区分の選択の状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        internal bool HENSOUSAKI_PLACE_KBN_TextChanged(string hyoName)
        {
            try
            {
                //コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                //タブ内(A票～E票)のコントロールに紐付ける
                //ラジオボタン
                HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
                HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
                HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
                HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
                HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

                //テキストボックス
                HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);
                MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

                //非表示タブは設定なし
                if (HENSOUSAKI_PLACE_KBN == null)
                {
                    return false;
                }
                else
                {
                    if ("1" == HENSOUSAKI_PLACE_KBN.Text)
                    {
                        HensousakiKbn.Enabled = false;
                        HensousakiKbn1.Enabled = false;
                        HensousakiKbn2.Enabled = false;
                        HensousakiKbn3.Enabled = false;
                        HensousakiKbn.Text = "1";
                        HensousakiKbn1.Checked = true;
                        HensousakiKbn2.Checked = false;
                        HensousakiKbn3.Checked = false;
                        ManiHensousakiTorihikisakiCode.Enabled = false;
                        ManiHensousakiGyoushaCode.Enabled = false;
                        ManiHensousakiGenbaCode.Enabled = false;

                        ManiHensousakiTorihikisakiCode.Text = string.Empty;
                        ManiHensousakiGyoushaCode.Text = string.Empty;
                        ManiHensousakiGenbaCode.Text = string.Empty;
                        ManiHensousakiTorihikisakiName.Text = string.Empty;
                        ManiHensousakiGyoushaName.Text = string.Empty;
                        ManiHensousakiGenbaName.Text = string.Empty;
                    }
                    else if ("2" == HENSOUSAKI_PLACE_KBN.Text)
                    {
                        HensousakiKbn.Enabled = true;
                        HensousakiKbn1.Enabled = true;
                        HensousakiKbn2.Enabled = true;
                        HensousakiKbn3.Enabled = true;
                        HensousakiKbn.Text = "1";
                        HensousakiKbn1.Checked = true;
                        HensousakiKbn2.Checked = false;
                        HensousakiKbn3.Checked = false;
                        ManiHensousakiTorihikisakiCode.Enabled = true;
                        ManiHensousakiGyoushaCode.Enabled = false;
                        ManiHensousakiGenbaCode.Enabled = false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HENSOUSAKI_PLACE_KBN_TextChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string cd)
        {
            LogUtility.DebugMethodStart(cd);

            M_TORIHIKISAKI entity = new M_TORIHIKISAKI();
            entity.TORIHIKISAKI_CD = cd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_TORIHIKISAKI[] result = this.daoTorisaki.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string cd)
        {
            LogUtility.DebugMethodStart(cd);

            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = cd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_GYOUSHA[] result = this.daoGyousha.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA entity = new M_GENBA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.GENBA_CD = genbaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;

            M_GENBA[] result = this.daoGenba.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        /// <summary>
        /// マニフェスト返送先場所区分の選択の状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        internal bool MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged()
        {
            try
            {
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                {
                    this.form.ManiHensousakiName1.Enabled = true;
                    this.form.ManiHensousakiName2.Enabled = true;
                    this.form.ManiHensousakiKeishou1.Enabled = true;
                    this.form.ManiHensousakiKeishou2.Enabled = true;
                    this.form.ManiHensousakiPost.Enabled = true;
                    this.form.ManiHensousakiAddress1.Enabled = true;
                    this.form.ManiHensousakiAddress2.Enabled = true;
                    this.form.ManiHensousakiBusho.Enabled = true;
                    this.form.ManiHensousakiTantou.Enabled = true;
                    this.form.GENBA_COPY_MANI_BUTTON.Enabled = true;
                    this.form.ManiHensousakiPostSearchButton.Enabled = true;
                    this.form.ManiHensousakiAddressSearchButton.Enabled = true;
                }
                else
                {
                    this.form.ManiHensousakiName1.Enabled = false;
                    this.form.ManiHensousakiName2.Enabled = false;
                    this.form.ManiHensousakiKeishou1.Enabled = false;
                    this.form.ManiHensousakiKeishou2.Enabled = false;
                    this.form.ManiHensousakiPost.Enabled = false;
                    this.form.ManiHensousakiAddress1.Enabled = false;
                    this.form.ManiHensousakiAddress2.Enabled = false;
                    this.form.ManiHensousakiBusho.Enabled = false;
                    this.form.ManiHensousakiTantou.Enabled = false;
                    this.form.GENBA_COPY_MANI_BUTTON.Enabled = false;
                    this.form.ManiHensousakiPostSearchButton.Enabled = false;
                    this.form.ManiHensousakiAddressSearchButton.Enabled = false;
                    this.form.ManiHensousakiName1.Text = string.Empty;
                    this.form.ManiHensousakiName2.Text = string.Empty;
                    this.form.ManiHensousakiKeishou1.Text = string.Empty;
                    this.form.ManiHensousakiKeishou2.Text = string.Empty;
                    this.form.ManiHensousakiPost.Text = string.Empty;
                    this.form.ManiHensousakiAddress1.Text = string.Empty;
                    this.form.ManiHensousakiAddress2.Text = string.Empty;
                    this.form.ManiHensousakiBusho.Text = string.Empty;
                    this.form.ManiHensousakiTantou.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニ返送先チェックボックスのON/OFFチェック
        /// </summary>
        /// <param name="clearFlag"></param>
        /// <returns></returns>
        public bool ManiHensousakiKbn_CheckedChanged()
        {
            try
            {
                this.FlgManiHensousakiKbn = this.form.ManiHensousakiKbn.Checked;

                if (this.FlgManiHensousakiKbn)
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        this.form.MANIFEST_USE_AHyo.Text = "1";
                        this.form.MANIFEST_USE_1_AHyo.Checked = true;
                        this.form.MANIFEST_USE_2_AHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        this.form.MANIFEST_USE_B1Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        this.form.MANIFEST_USE_B2Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        this.form.MANIFEST_USE_B4Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B4Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        this.form.MANIFEST_USE_B6Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B6Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        this.form.MANIFEST_USE_C1Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_C1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        this.form.MANIFEST_USE_C2Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_C2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        this.form.MANIFEST_USE_DHyo.Text = "1";
                        this.form.MANIFEST_USE_1_DHyo.Checked = true;
                        this.form.MANIFEST_USE_2_DHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        this.form.MANIFEST_USE_EHyo.Text = "1";
                        this.form.MANIFEST_USE_1_EHyo.Checked = true;
                        this.form.MANIFEST_USE_2_EHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = true;
                    }
                }
                else
                {
                    if (this._tabPageManager.IsVisible("tabPage1"))
                    {
                        this.form.MANIFEST_USE_AHyo.Text = "1";
                        this.form.MANIFEST_USE_1_AHyo.Checked = true;
                        this.form.MANIFEST_USE_2_AHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage2"))
                    {
                        this.form.MANIFEST_USE_B1Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage3"))
                    {
                        this.form.MANIFEST_USE_B2Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage4"))
                    {
                        this.form.MANIFEST_USE_B4Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B4Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage5"))
                    {
                        this.form.MANIFEST_USE_B6Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_B6Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage6"))
                    {
                        this.form.MANIFEST_USE_C1Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_C1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage7"))
                    {
                        this.form.MANIFEST_USE_C2Hyo.Text = "1";
                        this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                        this.form.MANIFEST_USE_2_C2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage8"))
                    {
                        this.form.MANIFEST_USE_DHyo.Text = "1";
                        this.form.MANIFEST_USE_1_DHyo.Checked = true;
                        this.form.MANIFEST_USE_2_DHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Enabled = true;
                    }

                    if (this._tabPageManager.IsVisible("tabPage9"))
                    {
                        this.form.MANIFEST_USE_EHyo.Text = "1";
                        this.form.MANIFEST_USE_1_EHyo.Checked = true;
                        this.form.MANIFEST_USE_2_EHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = false;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Enabled = true;
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Enabled = true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiKbn_CheckedChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 返送先区分
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        private Int16 PlaceKbnPase(string strTemp)
        {
            try
            {
                if (string.IsNullOrEmpty(strTemp))
                {
                    return 1;
                }
                else
                {
                    return Int16.Parse(strTemp);
                }
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        /// <returns>true:削除可, false:削除不可</returns>
        internal bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                string where = "(ci.HINMEI_CD = '{0}' AND ci.UNIT_CD = {1} AND ci.DENPYOU_KBN_CD = {2})";
                List<string> whereList = new List<string>();

                // 現場の定期回収情報がコース入力のコース明細で使用されているかチェック
                string baseSql = @"SELECT DISTINCT N'コース明細マスタ' AS NAME
FROM M_COURSE_DETAIL_ITEMS AS ci
INNER JOIN M_COURSE AS c
ON ci.DAY_CD = c.DAY_CD
AND ci.COURSE_NAME_CD = c.COURSE_NAME_CD
INNER JOIN M_COURSE_DETAIL AS cd
ON ci.DAY_CD = cd.DAY_CD
AND ci.COURSE_NAME_CD = cd.COURSE_NAME_CD
AND ci.REC_ID = cd.REC_ID
WHERE c.DELETE_FLG = 0
AND cd.GYOUSHA_CD = '{0}'
AND cd.GENBA_CD = '{1}'";

                foreach (Row gcRwos in this.form.TeikiHinmeiIchiran.Rows)
                {
                    if (gcRwos.IsNewRow)
                    {
                        continue;
                    }

                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["HINMEI_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["HINMEI_CD"].Value.ToString()) &&
                            gcRwos.Cells["UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["UNIT_CD"].Value.ToString()) &&
                            gcRwos.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                        {
                            var hinmeiCd = gcRwos.Cells["HINMEI_CD"].Value.ToString();
                            var unitCd = gcRwos.Cells["UNIT_CD"].Value.ToString();
                            var denpyouKbnCd = gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString();

                            // 削除対象の品名CDと単位CDを収集
                            whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd));
                        }
                    }
                }

                var sb = new System.Text.StringBuilder();
                if (whereList.Any())
                {
                    // SQLの品名・単位の条件文用に加工
                    sb.Append(" AND (");
                    bool isFirst = true;

                    foreach (var str in whereList)
                    {
                        if (!isFirst)
                        {
                            sb.Append(" OR ");
                        }

                        sb.Append(str);
                        isFirst = false;
                    }

                    sb.Append(")");
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    string gyoushaCd = this.form.GyoushaCode.Text;
                    string genbaCd = this.form.GenbaCode.Text;

                    var sql = string.Format(baseSql, gyoushaCd, genbaCd);

                    // SQLに品名・単位による絞込の条件文を追加
                    sb.Insert(0, sql);

                    DataTable dtTable = daoGenba.GetDateForStringSql(sb.ToString());

                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "現場", "定期回収情報(業者CD,現場CD,品名CD,伝票区分,単位)", strName);

                        ret = false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 超過規定量、超過分品名の制御
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool TsukiHinmeiIchiran_CellContentClick(CellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return false;
                }
                var row = this.form.TsukiHinmeiIchiran.Rows[e.RowIndex];
                var cell = this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex];
                if (cell.ReadOnly || !cell.Enabled)
                {
                    return false;
                }
                if (e.CellName == "CHOUKA_SETTING")
                {
                    if (Convert.ToBoolean(cell.EditedFormattedValue))
                    {
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Enabled = true;
                        row.Cells["CHOUKA_HINMEI_NAME"].Enabled = true;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.BackColor = Constans.NOMAL_COLOR;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.BackColor = Constans.NOMAL_COLOR;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;

                        SelectCheckDto existCheck = new SelectCheckDto();
                        existCheck.CheckMethodName = "必須チェック";
                        Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                        excitChecks.Add(existCheck);
                        ((GcCustomNumericTextBox2Cell)row.Cells["CHOUKA_LIMIT_AMOUNT"]).RegistCheckMethod = excitChecks;
                        ((GcCustomTextBoxCell)row.Cells["CHOUKA_HINMEI_NAME"]).RegistCheckMethod = excitChecks;
                    }
                    else
                    {
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Enabled = false;
                        row.Cells["CHOUKA_HINMEI_NAME"].Enabled = false;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Value = DBNull.Value;
                        row.Cells["CHOUKA_HINMEI_NAME"].Value = string.Empty;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.ForeColor = Constans.READONLY_COLOR_FORE;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.ForeColor = Constans.READONLY_COLOR_FORE;
                        ((GcCustomNumericTextBox2Cell)row.Cells["CHOUKA_LIMIT_AMOUNT"]).RegistCheckMethod = null;
                        ((GcCustomTextBoxCell)row.Cells["CHOUKA_HINMEI_NAME"]).RegistCheckMethod = null;
                    }
                    this.form.TsukiHinmeiIchiran.Refresh();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_CellContentClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 月極一覧の行内容による制御を実施する
        /// </summary>
        public bool SetIchiranTsukiRowControl()
        {
            try
            {
                foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                            || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                        {
                            foreach (var tmpCell in row.Cells)
                            {
                                switch (tmpCell.Name)
                                {
                                    case "DELETE_FLG":
                                    case "HINMEI_CD":
                                    case "UNIT_CD":
                                    case "UNIT_NAME_RYAKU":
                                    case "TANKA":
                                    case "CHOUKA_SETTING":
                                    case "CHOUKA_LIMIT_AMOUNT":
                                    case "CHOUKA_HINMEI_NAME":
                                    case "TEIKI_JISSEKI_NO_SEIKYUU_KBN":
                                        tmpCell.Enabled = false;
                                        break;
                                }
                            }
                        }
                        continue;
                    }
                    var cell = row.Cells[GenbaHoshuConstans.TSUKI_CHOUKA_SETTING];

                    if (Convert.ToBoolean(cell.EditedFormattedValue)
                        && this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG
                        && this.form.WindowType != WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Enabled = true;
                        row.Cells["CHOUKA_HINMEI_NAME"].Enabled = true;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.BackColor = Constans.NOMAL_COLOR;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.BackColor = Constans.NOMAL_COLOR;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;
                    }
                    else
                    {
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Enabled = false;
                        row.Cells["CHOUKA_HINMEI_NAME"].Enabled = false;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells["CHOUKA_LIMIT_AMOUNT"].Style.ForeColor = Constans.READONLY_COLOR_FORE;
                        row.Cells["CHOUKA_HINMEI_NAME"].Style.ForeColor = Constans.READONLY_COLOR_FORE;
                    }

                    if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        foreach (var tmpCell in row.Cells)
                        {
                            switch (tmpCell.Name)
                            {
                                case "DELETE_FLG":
                                case "HINMEI_CD":
                                case "UNIT_CD":
                                case "UNIT_NAME_RYAKU":
                                case "TANKA":
                                case "CHOUKA_SETTING":
                                case "CHOUKA_LIMIT_AMOUNT":
                                case "CHOUKA_HINMEI_NAME":
                                case "TEIKI_JISSEKI_NO_SEIKYUU_KBN":
                                    tmpCell.Enabled = false;
                                    break;
                            }
                        }
                    }
                }
                this.form.TsukiHinmeiIchiran.Refresh();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTsukiRowControl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 超過規定量、超過分品名の制御
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool TsukiHinmeiIchiran_CurrentCellChanged(EventArgs e)
        {
            try
            {
                var row = this.form.TsukiHinmeiIchiran.CurrentRow;
                var cell = this.form.TsukiHinmeiIchiran.CurrentCell;
                if (cell == null || this.preRowIndex == -1 || this.preCellIndex == -1)
                {
                    return false;
                }

                if (!cell.Enabled && cell.RowIndex == this.preRowIndex)
                {
                    if (cell.CellIndex - this.preCellIndex == 1)
                    {
                        SelectionActions.MoveToNextCell.Execute(this.form.TsukiHinmeiIchiran);
                    }
                    else if (cell.CellIndex - this.preCellIndex == -1)
                    {
                        SelectionActions.MoveToPreviousCell.Execute(this.form.TsukiHinmeiIchiran);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiIchiran_CurrentCellChanged", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 発行先チェック処理
        /// </summary>
        public bool HakkousakuCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先CDの値がブランクの場合
                if (string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
                {
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                }
                else
                {
                    M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                    queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    // 取引区「1.現金」の取引先を入力した時、発行先コードは非活性となるようにする。
                    if (seikyuuEntity != null && seikyuuEntity.OUTPUT_KBN.ToString() == "2" && seikyuuEntity.TORIHIKI_KBN_CD.ToString() == "2")
                    {
                        // 取引先マスタの出力区分が「２．電子CSV」の場合
                        this.form.HAKKOUSAKI_CD.Enabled = true;
                    }
                    else
                    {
                        // 取引先マスタの出力区分が「２．電子CSV」以外の場合
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HakkousakuCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        private bool setDensiSeikyushoVisible()
        {
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();

            if (!densiVisible)
            {

                this.form.labelDensiSeikyuuSho.Visible = densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;

            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>
        /// 業者CD変更時、〇票返送先の使用区分を更新
        /// マージの場合：カスタマイズでタブ数が変動している可能性もありますので、
        /// 　　　　　　　タブの番号は案件毎に一度確認をしてください。
        /// </summary>
        public bool SetHensosaki()
        {

            if (!this.form.GyoushaKbnMani.Checked)
            {
                //A票
                if (this._tabPageManager.IsVisible("tabPage1"))
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                }
                //B1票
                if (this._tabPageManager.IsVisible("tabPage2"))
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                }
                //B2票
                if (this._tabPageManager.IsVisible("tabPage3"))
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                }
                //B4票
                if (this._tabPageManager.IsVisible("tabPage4"))
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                }
                //B6票
                if (this._tabPageManager.IsVisible("tabPage5"))
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                }
                //C1票
                if (this._tabPageManager.IsVisible("tabPage6"))
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                }
                //C2票
                if (this._tabPageManager.IsVisible("tabPage7"))
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                }
                //D票
                if (this._tabPageManager.IsVisible("tabPage8"))
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                }
                //E票
                if (this._tabPageManager.IsVisible("tabPage9"))
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                }
            }
            else
            {
                //A票
                if (this._tabPageManager.IsVisible("tabPage1"))
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                }
                //B1票
                if (this._tabPageManager.IsVisible("tabPage2"))
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                }
                //B2票
                if (this._tabPageManager.IsVisible("tabPage3"))
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                }
                //B4票
                if (this._tabPageManager.IsVisible("tabPage4"))
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                }
                //B6票
                if (this._tabPageManager.IsVisible("tabPage5"))
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                }
                //C1票
                if (this._tabPageManager.IsVisible("tabPage6"))
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                }
                //C2票
                if (this._tabPageManager.IsVisible("tabPage7"))
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                }
                //D票
                if (this._tabPageManager.IsVisible("tabPage8"))
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                }
                //E票
                if (this._tabPageManager.IsVisible("tabPage9"))
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                }
            }

            bool catchErr = this.SettingHensouSakiKbn();
            if (catchErr)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// 請求情報の送付先情報を取引先の情報でコピー
        /// </summary>
        public bool TorihikisakiInfoCopyFromSeikyuuInfo()
        {
            try
            {
                M_TORIHIKISAKI entitysTORIHIKISAKI = daoTorisaki.GetDataByCd(this.TorihikisakiCd);

                if (entitysTORIHIKISAKI == null)
                {
                    return true;
                }

                this.form.SeikyuushoSoufusaki1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.SeikyuushoSoufusaki2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.SeikyuuSouhuKeishou1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.SeikyuuSouhuKeishou2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.SeikyuuSoufuPost.Text = entitysTORIHIKISAKI.TORIHIKISAKI_POST;

                string todoufukenName = "";
                if (!entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.SeikyuuSoufuAddress1.Text = todoufukenName + entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.SeikyuuSoufuAddress2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.SeikyuuSoufuBusho.Text = entitysTORIHIKISAKI.BUSHO;
                this.form.SeikyuuSoufuTantou.Text = entitysTORIHIKISAKI.TANTOUSHA;
                this.form.SoufuGenbaTel.Text = entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.SoufuGenbaFax.Text = entitysTORIHIKISAKI.TORIHIKISAKI_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiInfoCopyFromSeikyuuInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 請求情報の送付先情報を業者の情報でコピー
        /// </summary>
        public bool GyoushaInfoCopyFromSeikyuuInfo()
        {
            try
            {
                M_GYOUSHA entitysGYOUSHA = daoGyousha.GetDataByCd(this.GyoushaCd);

                if (entitysGYOUSHA == null)
                {
                    return true;
                }

                this.form.SeikyuushoSoufusaki1.Text = entitysGYOUSHA.GYOUSHA_NAME1;
                this.form.SeikyuushoSoufusaki2.Text = entitysGYOUSHA.GYOUSHA_NAME2;
                this.form.SeikyuuSouhuKeishou1.Text = entitysGYOUSHA.GYOUSHA_KEISHOU1;
                this.form.SeikyuuSouhuKeishou2.Text = entitysGYOUSHA.GYOUSHA_KEISHOU2;
                this.form.SeikyuuSoufuPost.Text = entitysGYOUSHA.GYOUSHA_POST;

                string todoufukenName = "";
                if (!entitysGYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysGYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.SeikyuuSoufuAddress1.Text = todoufukenName + entitysGYOUSHA.GYOUSHA_ADDRESS1;
                this.form.SeikyuuSoufuAddress2.Text = entitysGYOUSHA.GYOUSHA_ADDRESS2;
                this.form.SeikyuuSoufuBusho.Text = entitysGYOUSHA.BUSHO;
                this.form.SeikyuuSoufuTantou.Text = entitysGYOUSHA.TANTOUSHA;
                this.form.SoufuGenbaTel.Text = entitysGYOUSHA.GYOUSHA_TEL;
                this.form.SoufuGenbaFax.Text = entitysGYOUSHA.GYOUSHA_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromSeikyuuInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 支払情報の送付先情報を取引先の情報でコピー
        /// </summary>
        public bool TorihikisakiInfoCopyFromShiharaiInfo()
        {
            try
            {
                M_TORIHIKISAKI entitysTORIHIKISAKI = daoTorisaki.GetDataByCd(this.TorihikisakiCd);

                if (entitysTORIHIKISAKI == null)
                {
                    return true;
                }

                this.form.ShiharaiSoufuName1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.ShiharaiSoufuName2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.ShiharaiSoufuKeishou1.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.ShiharaiSoufuKeishou2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.ShiharaiSoufuPost.Text = entitysTORIHIKISAKI.TORIHIKISAKI_POST;

                string todoufukenName = "";
                if (!entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.ShiharaiSoufuAddress1.Text = todoufukenName + entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.ShiharaiSoufuAddress2.Text = entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.ShiharaiSoufuBusho.Text = entitysTORIHIKISAKI.BUSHO;
                this.form.ShiharaiSoufuTantou.Text = entitysTORIHIKISAKI.TANTOUSHA;
                this.form.ShiharaiGenbaTel.Text = entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.ShiharaiGenbaFax.Text = entitysTORIHIKISAKI.TORIHIKISAKI_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiInfoCopyFromShiharaiInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 支払情報の送付先情報を業者の情報でコピー
        /// </summary>
        public bool GyoushaInfoCopyFromShiharaiInfo()
        {
            try
            {
                M_GYOUSHA entitysGYOUSHA = daoGyousha.GetDataByCd(this.GyoushaCd);

                if (entitysGYOUSHA == null)
                {
                    return true;
                }

                this.form.ShiharaiSoufuName1.Text = entitysGYOUSHA.GYOUSHA_NAME1;
                this.form.ShiharaiSoufuName2.Text = entitysGYOUSHA.GYOUSHA_NAME2;
                this.form.ShiharaiSoufuKeishou1.Text = entitysGYOUSHA.GYOUSHA_KEISHOU1;
                this.form.ShiharaiSoufuKeishou2.Text = entitysGYOUSHA.GYOUSHA_KEISHOU2;
                this.form.ShiharaiSoufuPost.Text = entitysGYOUSHA.GYOUSHA_POST;

                string todoufukenName = "";
                if (!entitysGYOUSHA.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    M_TODOUFUKEN entitysM_TODOUFUKEN = this.daoTodoufuken.GetDataByCd(entitysGYOUSHA.GYOUSHA_TODOUFUKEN_CD.ToString());
                    if (entitysM_TODOUFUKEN != null)
                    {
                        todoufukenName = entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }

                this.form.ShiharaiSoufuAddress1.Text = todoufukenName + entitysGYOUSHA.GYOUSHA_ADDRESS1;
                this.form.ShiharaiSoufuAddress2.Text = entitysGYOUSHA.GYOUSHA_ADDRESS2;
                this.form.ShiharaiSoufuBusho.Text = entitysGYOUSHA.BUSHO;
                this.form.ShiharaiSoufuTantou.Text = entitysGYOUSHA.TANTOUSHA;
                this.form.ShiharaiGenbaTel.Text = entitysGYOUSHA.GYOUSHA_TEL;
                this.form.ShiharaiGenbaFax.Text = entitysGYOUSHA.GYOUSHA_FAX;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromShiharaiInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// OpenTorihikisakiFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenTorihikisakiFormReference(string TorihikisakiCd)
        {
            LogUtility.DebugMethodStart(TorihikisakiCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            if (!r_framework.Authority.Manager.CheckAuthority("M213", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth("M213", windowType, windowType, TorihikisakiCd);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// OpenTorihikisakiFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenGyoushaFormReference(string GyoushaCd)
        {
            LogUtility.DebugMethodStart(GyoushaCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            if (!r_framework.Authority.Manager.CheckAuthority("M215", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth("M215", windowType, windowType, GyoushaCd);
            LogUtility.DebugMethodEnd();
        }

        #region mapbox連携

        /// <summary>
        /// 地図表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OpenMap(object sender, EventArgs e)
        {
            try
            {
                // 緯度経度の入力チェック
                if (this.CheckLocation())
                {
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.GenbaLatitude.Text) && !string.IsNullOrEmpty(this.form.GenbaLongitude.Text))
                {
                    // 緯度経度入力済み
                    if (this.form.errmessage.MessageBoxShowConfirm("地図を表示します。よろしいですか？") == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (!string.IsNullOrEmpty(this.form.GenbaTodoufukenCode.Text) && !string.IsNullOrEmpty(this.form.GenbaAddress1.Text))
                {
                    // 緯度経度未入力、都道府県、住所有り
                    if (this.form.errmessage.MessageBoxShowConfirm("入力されている住所情報にて地図を表示します。よろしいですか？\r\n注）住所での地図表示の場合、正しい位置にならない場合があります。") == DialogResult.No)
                    {
                        return;
                    }

                    // 住所から緯度経度を取得するAPI
                    // 取得した緯度経度は画面にセットせず、APIに投げるだけ
                    MapboxAPILogic apiLogic = new MapboxAPILogic();
                    GeoCodingAPI result = null;

                    // 都道府県+住所1+住所2で検索
                    string address = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text + this.form.GenbaAddress2.Text;
                    if (!apiLogic.HttpGET<GeoCodingAPI>(address, out result))
                    {
                        // APIでエラー発生
                        return;
                    }
                    foreach (Feature feature in result.features)
                    {
                        // APIで取得した値を利用する
                        this.form.GenbaLatitude.Text = feature.geometry.coordinates[1];
                        this.form.GenbaLongitude.Text = feature.geometry.coordinates[0];
                    }
                }
                else
                {
                    // 緯度経度、都道府県住所全て未入力
                    this.form.errmessage.MessageBoxShowInformation("緯度及び経度、もしくは、住所を入力してください。");
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.createMapboxDto();
                if (dtos == null)
                {
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_GENBA);
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenMap", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }


        /// <summary>
        /// 地図表示ロジックに渡すDtoを作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                List<mapDtoList> dtoLists = new List<mapDtoList>();
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = 1;

                List<mapDto> dtos = new List<mapDto>();
                mapDto dto = new mapDto();
                dto.id = 1;
                dto.layerNo = 1;
                dto.dataShurui = "2";
                dto.torihikisakiCd = string.Empty;
                dto.torihikisakiName = string.Empty;
                dto.gyoushaCd = this.form.GyoushaCode.Text;
                // 略称を抽出
                var gyoushaEntity = daoGyousha.GetDataByCd(this.form.GyoushaCode.Text);
                if (gyoushaEntity == null)
                {
                    dto.gyoushaName = Convert.ToString(this.form.GyoushaName1.Text);
                }
                else
                {
                    dto.gyoushaName = Convert.ToString(gyoushaEntity.GYOUSHA_NAME_RYAKU);
                }
                dto.genbaCd = this.form.GenbaCode.Text;
                dto.genbaName = this.form.GenbaNameRyaku.Text;
                dto.address = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text + this.form.GenbaAddress2.Text;
                dto.latitude = this.form.GenbaLatitude.Text;
                dto.longitude = this.form.GenbaLongitude.Text;
                dtos.Add(dto);
                dtoList.dtos = dtos;
                dtoLists.Add(dtoList);

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlDateTime sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return SqlDateTime.Parse(now.ToString());
        }

        /// <summary>
        /// 緯度経度の入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckLocation()
        {
            bool ret = false;

            // 緯度のチェック
            if (!string.IsNullOrEmpty(this.form.GenbaLatitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.GenbaLatitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("緯度の入力が正しくありません");
                    this.form.GenbaLatitude.Focus();
                    return true;
                }
            }

            // 経度のチェック
            if (!string.IsNullOrEmpty(this.form.GenbaLongitude.Text))
            {
                try
                {
                    Convert.ToDouble(this.form.GenbaLongitude.Text);
                }
                catch (Exception ex)
                {
                    this.form.errmessage.MessageBoxShowWarn("経度の入力が正しくありません");
                    this.form.GenbaLongitude.Focus();
                    return true;
                }
            }

            // 緯度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.GenbaLatitude.Text) &&
               !string.IsNullOrEmpty(this.form.GenbaLongitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("緯度を入力してください。");
                this.form.GenbaLatitude.Focus();
                return true;
            }

            // 経度だけ入力されていない
            if (string.IsNullOrEmpty(this.form.GenbaLongitude.Text) &&
               !string.IsNullOrEmpty(this.form.GenbaLatitude.Text))
            {
                this.form.errmessage.MessageBoxShowWarn("経度を入力してください。");
                this.form.GenbaLongitude.Focus();
                return true;
            }

            return ret;
        }

        #endregion

        // QN Tue Anh #158986 START
        internal bool IsTeikiHinmeiUpdateCheck()
        {
            foreach (Row gcRwos in this.form.TeikiHinmeiIchiran.Rows)
            {
                if (gcRwos.IsNewRow)
                {
                    continue;
                }

                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    return true;
                }
                if (gcRwos.Cells["KOUSHIN_FLG"].Value != null && gcRwos.Cells["KOUSHIN_FLG"].Value.ToString() == "True")
                {
                    return true;
                }
            }

            return false;
        }

        internal bool CheckDeleteTeikiHinmei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = false;
                string gyoushaCd = this.form.GyoushaCode.Text;
                string genbaCd = this.form.GenbaCode.Text;
                string where = "(ci.HINMEI_CD = '{0}' AND ci.UNIT_CD = {1} AND ci.DENPYOU_KBN_CD = {2})";
                string whereDayCd = "(ci.HINMEI_CD = '{0}' AND ci.UNIT_CD = {1} AND ci.DENPYOU_KBN_CD = {2} AND c.DAY_CD = '{3}')";
                List<string> whereList = new List<string>();

                // 現場の定期回収情報がコース入力のコース明細で使用されているかチェック
                string baseSql = @"SELECT DISTINCT ci.*
                                    FROM M_COURSE_DETAIL_ITEMS AS ci
                                    INNER JOIN M_COURSE AS c
                                    ON ci.DAY_CD = c.DAY_CD
                                    AND ci.COURSE_NAME_CD = c.COURSE_NAME_CD
                                    INNER JOIN M_COURSE_DETAIL AS cd
                                    ON ci.DAY_CD = cd.DAY_CD
                                    AND ci.COURSE_NAME_CD = cd.COURSE_NAME_CD
                                    AND ci.REC_ID = cd.REC_ID
                                    WHERE c.DELETE_FLG = 0
                                    AND cd.GYOUSHA_CD = '{0}'
                                    AND cd.GENBA_CD = '{1}'
                                    AND ci.INPUT_KBN = 2";

                foreach (Row gcRwos in this.form.TeikiHinmeiIchiran.Rows)
                {
                    if (gcRwos.IsNewRow)
                    {
                        continue;
                    }

                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["HINMEI_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["HINMEI_CD"].Value.ToString()) &&
                            gcRwos.Cells["UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["UNIT_CD"].Value.ToString()) &&
                            gcRwos.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                        {
                            var hinmeiCd = gcRwos.Cells["HINMEI_CD"].Value.ToString();
                            var unitCd = gcRwos.Cells["UNIT_CD"].Value.ToString();
                            var denpyouKbnCd = gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString();

                            // 削除対象の品名CDと単位CDを収集
                            whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd));
                        }
                    }
                    if (gcRwos.Cells["KOUSHIN_FLG"].Value != null && gcRwos.Cells["KOUSHIN_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                        {
                            continue;
                        }

                        if (gcRwos.Cells["HINMEI_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["HINMEI_CD"].Value.ToString()) &&
                            gcRwos.Cells["UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["UNIT_CD"].Value.ToString()) &&
                            gcRwos.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                        {
                            string hinmeiCd = gcRwos.Cells["HINMEI_CD"].Value.ToString();
                            string unitCd = gcRwos.Cells["UNIT_CD"].Value.ToString();
                            string denpyouKbnCd = gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString();

                            bool monday = (gcRwos.Cells["MONDAY"].Value != null) ? (bool)gcRwos.Cells["MONDAY"].Value : false;
                            bool tuesday = (gcRwos.Cells["TUESDAY"].Value != null) ? (bool)gcRwos.Cells["TUESDAY"].Value : false;
                            bool wednesday = (gcRwos.Cells["WEDNESDAY"].Value != null) ? (bool)gcRwos.Cells["WEDNESDAY"].Value : false;
                            bool thursday = (gcRwos.Cells["THURSDAY"].Value != null) ? (bool)gcRwos.Cells["THURSDAY"].Value : false;
                            bool friday = (gcRwos.Cells["FRIDAY"].Value != null) ? (bool)gcRwos.Cells["FRIDAY"].Value : false;
                            bool saturday = (gcRwos.Cells["SATURDAY"].Value != null) ? (bool)gcRwos.Cells["SATURDAY"].Value : false;
                            bool sunday = (gcRwos.Cells["SUNDAY"].Value != null) ? (bool)gcRwos.Cells["SUNDAY"].Value : false;

                            M_GENBA_TEIKI_HINMEI genbaTeikiHinmeiEntity = this.daoGenbaTeiki.GetDataByCd(gyoushaCd, genbaCd, hinmeiCd, denpyouKbnCd, unitCd);
                            if (genbaTeikiHinmeiEntity != null)
                            {
                                if (genbaTeikiHinmeiEntity.MONDAY.IsTrue)
                                {
                                    if (!monday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "1"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.TUESDAY.IsTrue)
                                {
                                    if (!tuesday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "2"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.WEDNESDAY.IsTrue)
                                {
                                    if (!wednesday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "3"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.THURSDAY.IsTrue)
                                {
                                    if (!thursday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "4"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.FRIDAY.IsTrue)
                                {
                                    if (!friday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "5"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.SATURDAY.IsTrue)
                                {
                                    if (!saturday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "6"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.SUNDAY.IsTrue)
                                {
                                    if (!sunday)
                                    {
                                        whereList.Add(string.Format(whereDayCd, hinmeiCd, unitCd, denpyouKbnCd, "7"));
                                    }
                                }
                            }
                        }
                    }
                }

                var sb = new System.Text.StringBuilder();
                if (whereList.Any())
                {
                    // SQLの品名・単位の条件文用に加工
                    sb.Append(" AND (");
                    bool isFirst = true;

                    foreach (var str in whereList)
                    {
                        if (!isFirst)
                        {
                            sb.Append(" OR ");
                        }

                        sb.Append(str);
                        isFirst = false;
                    }

                    sb.Append(")");
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    var sql = string.Format(baseSql, gyoushaCd, genbaCd);

                    // SQLに品名・単位による絞込の条件文を追加
                    sb.Insert(0, sql);

                    DataTable dtTable = daoGenba.GetDateForStringSql(sb.ToString());
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        ret = true;
                        this.CreateCourseDeleteEntitys(dtTable);
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDeleteTeikiHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        internal bool CheckUpdateTeikiHinmei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = false;
                string gyoushaCd = this.form.GyoushaCode.Text;
                string genbaCd = this.form.GenbaCode.Text;
                string where = "(ci.HINMEI_CD = '{0}' AND ci.UNIT_CD = {1} AND ci.DENPYOU_KBN_CD = {2} AND c.DAY_CD = '{3}')";
                List<string> whereList = new List<string>();

                // 現場の定期回収情報がコース入力のコース明細で使用されているかチェック
                string baseSql = @"SELECT DISTINCT ci.*
                                    FROM M_COURSE_DETAIL_ITEMS AS ci
                                    INNER JOIN M_COURSE AS c
                                    ON ci.DAY_CD = c.DAY_CD
                                    AND ci.COURSE_NAME_CD = c.COURSE_NAME_CD
                                    INNER JOIN M_COURSE_DETAIL AS cd
                                    ON ci.DAY_CD = cd.DAY_CD
                                    AND ci.COURSE_NAME_CD = cd.COURSE_NAME_CD
                                    AND ci.REC_ID = cd.REC_ID
                                    WHERE c.DELETE_FLG = 0
                                    AND cd.GYOUSHA_CD = '{0}'
                                    AND cd.GENBA_CD = '{1}'
                                    AND ci.INPUT_KBN = 2";
                foreach (Row gcRwos in this.form.TeikiHinmeiIchiran.Rows)
                {
                    if (gcRwos.IsNewRow)
                    {
                        continue;
                    }

                    if (gcRwos.Cells["KOUSHIN_FLG"].Value != null && gcRwos.Cells["KOUSHIN_FLG"].Value.ToString() == "True")
                    {
                        if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                        {
                            continue;
                        }

                        if (gcRwos.Cells["HINMEI_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["HINMEI_CD"].Value.ToString()) &&
                            gcRwos.Cells["UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["UNIT_CD"].Value.ToString()) &&
                            gcRwos.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                        {
                            var hinmeiCd = gcRwos.Cells["HINMEI_CD"].Value.ToString();
                            var unitCd = gcRwos.Cells["UNIT_CD"].Value.ToString();
                            var denpyouKbnCd = gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString();


                            bool monday = (gcRwos.Cells["MONDAY"].Value != null) ? (bool)gcRwos.Cells["MONDAY"].Value : false;
                            bool tuesday = (gcRwos.Cells["TUESDAY"].Value != null) ? (bool)gcRwos.Cells["TUESDAY"].Value : false;
                            bool wednesday = (gcRwos.Cells["WEDNESDAY"].Value != null) ? (bool)gcRwos.Cells["WEDNESDAY"].Value : false;
                            bool thursday = (gcRwos.Cells["THURSDAY"].Value != null) ? (bool)gcRwos.Cells["THURSDAY"].Value : false;
                            bool friday = (gcRwos.Cells["FRIDAY"].Value != null) ? (bool)gcRwos.Cells["FRIDAY"].Value : false;
                            bool saturday = (gcRwos.Cells["SATURDAY"].Value != null) ? (bool)gcRwos.Cells["SATURDAY"].Value : false;
                            bool sunday = (gcRwos.Cells["SUNDAY"].Value != null) ? (bool)gcRwos.Cells["SUNDAY"].Value : false;

                            M_GENBA_TEIKI_HINMEI genbaTeikiHinmeiEntity = this.daoGenbaTeiki.GetDataByCd(gyoushaCd, genbaCd, hinmeiCd, denpyouKbnCd, unitCd);
                            if (genbaTeikiHinmeiEntity != null)
                            {
                                if (genbaTeikiHinmeiEntity.MONDAY.IsTrue)
                                {
                                    if (monday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "1"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.TUESDAY.IsTrue)
                                {
                                    if (tuesday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "2"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.WEDNESDAY.IsTrue)
                                {
                                    if (wednesday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "3"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.THURSDAY.IsTrue)
                                {
                                    if (thursday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "4"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.FRIDAY.IsTrue)
                                {
                                    if (friday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "5"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.SATURDAY.IsTrue)
                                {
                                    if (saturday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "6"));
                                    }
                                }
                                if (genbaTeikiHinmeiEntity.SUNDAY.IsTrue)
                                {
                                    if (sunday)
                                    {
                                        whereList.Add(string.Format(where, hinmeiCd, unitCd, denpyouKbnCd, "7"));
                                    }
                                }
                            }
                        }
                    }
                }

                var sb = new System.Text.StringBuilder();
                if (whereList.Any())
                {
                    // SQLの品名・単位の条件文用に加工
                    sb.Append(" AND (");
                    bool isFirst = true;

                    foreach (var str in whereList)
                    {
                        if (!isFirst)
                        {
                            sb.Append(" OR ");
                        }

                        sb.Append(str);
                        isFirst = false;
                    }

                    sb.Append(")");
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    var sql = string.Format(baseSql, gyoushaCd, genbaCd);

                    // SQLに品名・単位による絞込の条件文を追加
                    sb.Insert(0, sql);

                    DataTable dtTable = daoGenba.GetDateForStringSql(sb.ToString());
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        this.CreateCourseUpdateEntitys(dtTable);
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUpdateTeikiHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        private void CreateCourseDeleteEntitys(DataTable dtTable)
        {
            DataTable courseDt = this.getCourseDt(dtTable);
            foreach (DataRow courseRow in courseDt.Rows)
            {
                string dayCd = courseRow["DAY_CD"].ToString();
                string courseNameCd = courseRow["COURSE_NAME_CD"].ToString();
                M_COURSE courseEntity = this.courseDao.GetDataByCd(dayCd, courseNameCd);
                if (courseEntity != null)
                {
                    M_COURSE systemData = new M_COURSE();
                    var binder = new DataBinderLogic<M_COURSE>(systemData);
                    binder.SetSystemProperty(systemData, false);
                    systemData = binder.Entitys[0];

                    courseEntity.UPDATE_DATE = systemData.UPDATE_DATE;
                    courseEntity.UPDATE_USER = systemData.UPDATE_USER;
                    courseEntity.UPDATE_PC = systemData.UPDATE_PC;

                    this.updateCourseEntity.Add(courseEntity);

                    DataTable courseDetailDt = this.getCourseDetailDt(dtTable, dayCd, courseNameCd);
                    List<M_COURSE_DETAIL> deleteCourseDetailEntitys = new List<M_COURSE_DETAIL>();
                    List<M_COURSE_DETAIL> updateCourseDetailEntitys = new List<M_COURSE_DETAIL>();
                    M_COURSE_DETAIL[] courseDetailEntitys = this.courseDetailDao.GetCourseDetailDatabyCd(dayCd, courseNameCd);
                    if (courseDetailEntitys == null || courseDetailEntitys.Length == 0)
                    {
                        continue;
                    }
                    foreach (DataRow courseDetailRow in courseDetailDt.Rows)
                    {
                        string recId = courseDetailRow["REC_ID"].ToString();
                        M_COURSE_DETAIL_ITEMS[] courseDetailItemsEntitys = this.courseDetailItemsDao.GetCourseDetailItemsDataByCd(dayCd, courseNameCd, recId);
                        if (courseDetailItemsEntitys == null || courseDetailItemsEntitys.Length == 0)
                        {
                            continue;
                        }
                        if (courseDetailItemsEntitys != null && courseDetailItemsEntitys.Length > 0)
                        {
                            List<M_COURSE_DETAIL_ITEMS> deleteCourseDetailItemEntitys = new List<M_COURSE_DETAIL_ITEMS>();
                            List<M_COURSE_DETAIL_ITEMS> updateCourseDetailItemEntitys = new List<M_COURSE_DETAIL_ITEMS>();
                            foreach (M_COURSE_DETAIL_ITEMS courseDetailItemsEntity in courseDetailItemsEntitys)
                            {
                                if (dtTable.AsEnumerable()
                                    .Count(r => r["DAY_CD"].ToString() == courseDetailItemsEntity.DAY_CD.ToString()
                                             && r["COURSE_NAME_CD"].ToString() == courseDetailItemsEntity.COURSE_NAME_CD.ToString()
                                             && r["REC_ID"].ToString() == courseDetailItemsEntity.REC_ID.ToString()
                                             && r["REC_SEQ"].ToString() == courseDetailItemsEntity.REC_SEQ.ToString()) > 0)
                                {
                                    deleteCourseDetailItemEntitys.Add(courseDetailItemsEntity);
                                }
                                else
                                {
                                    updateCourseDetailItemEntitys.Add(courseDetailItemsEntity);
                                }
                            }

                            if (deleteCourseDetailItemEntitys.Count > 0)
                            {
                                M_COURSE_DETAIL_ITEMS[] delItemEntitys = this.courseDetailItemsDao.GetCourseDetailItemsDataByCd(dayCd, courseNameCd, recId);
                                this.deleteCourseDetailItemsEntity.AddRange(delItemEntitys);
                                if (updateCourseDetailItemEntitys.Count > 0)
                                {
                                    int recSeq = 0;
                                    foreach (M_COURSE_DETAIL_ITEMS courseDetailItemsEntity in updateCourseDetailItemEntitys)
                                    {
                                        recSeq++;
                                        courseDetailItemsEntity.REC_SEQ = recSeq;
                                    }
                                    this.insertCourseDetailItemsEntity.AddRange(updateCourseDetailItemEntitys);
                                }

                                if (deleteCourseDetailItemEntitys.Count == courseDetailItemsEntitys.Length)
                                {
                                    M_COURSE_DETAIL deleteCourseDetailEntity = this.courseDetailDao.GetDataByCd(dayCd, courseNameCd, recId);
                                    if (deleteCourseDetailEntity != null)
                                    {
                                        deleteCourseDetailEntitys.Add(deleteCourseDetailEntity);
                                    }
                                }
                            }
                        }
                    }

                    if (deleteCourseDetailEntitys.Count > 0)
                    {
                        M_COURSE_DETAIL[] detailFindEntity = this.courseDetailDao.GetCourseDetailDatabyCd(dayCd, courseNameCd);
                        this.deleteCourseDetailEntity.AddRange(detailFindEntity);
                        foreach (M_COURSE_DETAIL courseDetailEntity in courseDetailEntitys)
                        {
                            if (!deleteCourseDetailEntitys.Exists(x => (bool)(x.REC_ID == courseDetailEntity.REC_ID)))
                            {
                                updateCourseDetailEntitys.Add(courseDetailEntity);
                            }
                        }

                        if (updateCourseDetailEntitys.Count > 0)
                        {
                            int rowNo = 0;
                            foreach (M_COURSE_DETAIL courseDetailEntity in updateCourseDetailEntitys)
                            {
                                rowNo++;
                                courseDetailEntity.ROW_NO = rowNo;
                            }
                        }
                        this.insertCourseDetailEntity.AddRange(updateCourseDetailEntitys);
                    }
                }
            }
        }

        private DataTable getCourseDt(DataTable dt)
        {
            string[] courseColumns = new[] { "DAY_CD", "COURSE_NAME_CD" };
            DataTable courseDt = new DataView(dt).ToTable(false, courseColumns);
            courseDt = courseDt.AsEnumerable()
                               .GroupBy(r => new { DAY_CD = r["DAY_CD"], COURSE_NAME_CD = r["COURSE_NAME_CD"] })
                               .Select(g =>
                               {
                                   var row = courseDt.NewRow();
                                   row["DAY_CD"] = g.Key.DAY_CD;
                                   row["COURSE_NAME_CD"] = g.Key.COURSE_NAME_CD;
                                   return row;
                               })
                               .CopyToDataTable();

            return courseDt;
        }

        private DataTable getCourseDetailDt(DataTable dt, string dayCd, string courseNameCd)
        {
            string[] courseDetailColumns = new[] { "DAY_CD", "COURSE_NAME_CD", "REC_ID" };
            DataTable courseDetail = new DataView(dt).ToTable(false, courseDetailColumns);
            courseDetail = courseDetail.AsEnumerable()
                               .Where(r => r.Field<Int16>("DAY_CD").ToString() == dayCd && r.Field<string>("COURSE_NAME_CD") == courseNameCd)
                               .GroupBy(r => new { DAY_CD = r["DAY_CD"], COURSE_NAME_CD = r["COURSE_NAME_CD"], REC_ID = r["REC_ID"] })
                               .Select(g =>
                               {
                                   var row = courseDetail.NewRow();
                                   row["DAY_CD"] = g.Key.DAY_CD;
                                   row["COURSE_NAME_CD"] = g.Key.COURSE_NAME_CD;
                                   row["DAY_CD"] = g.Key.DAY_CD;
                                   row["REC_ID"] = g.Key.REC_ID;
                                   return row;
                               })
                               .CopyToDataTable();

            return courseDetail;
        }

        private void CreateCourseUpdateEntitys(DataTable dtTable)
        {
            foreach (DataRow courseDetailItemsRow in dtTable.Rows)
            {
                string dayCd = courseDetailItemsRow["DAY_CD"].ToString();
                string courseNameCd = courseDetailItemsRow["COURSE_NAME_CD"].ToString();
                string recId = courseDetailItemsRow["REC_ID"].ToString();
                string recSeq = courseDetailItemsRow["REC_SEQ"].ToString();
                M_COURSE courseEntity = this.courseDao.GetDataByCd(dayCd, courseNameCd);
                if (courseEntity != null)
                {
                    M_COURSE systemData = new M_COURSE();
                    var binder = new DataBinderLogic<M_COURSE>(systemData);
                    binder.SetSystemProperty(systemData, false);
                    systemData = binder.Entitys[0];

                    courseEntity.UPDATE_DATE = systemData.UPDATE_DATE;
                    courseEntity.UPDATE_USER = systemData.UPDATE_USER;
                    courseEntity.UPDATE_PC = systemData.UPDATE_PC;

                    if (!this.updateCourseEntity.Exists(x => (bool)(x.DAY_CD == courseEntity.DAY_CD && x.COURSE_NAME_CD == courseEntity.COURSE_NAME_CD)))
                    {
                        this.updateCourseEntity.Add(courseEntity);
                    }

                    M_COURSE_DETAIL_ITEMS detailItemEntity = this.courseDetailItemsDao.GetDataByCd(dayCd, courseNameCd, recId, recSeq);
                    if (detailItemEntity != null)
                    {
                        foreach (Row gcRwos in this.form.TeikiHinmeiIchiran.Rows)
                        {
                            if (gcRwos.IsNewRow)
                            {
                                continue;
                            }

                            if (gcRwos.Cells["KOUSHIN_FLG"].Value != null && gcRwos.Cells["KOUSHIN_FLG"].Value.ToString() == "True")
                            {
                                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                                {
                                    continue;
                                }

                                if (gcRwos.Cells["HINMEI_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["HINMEI_CD"].Value.ToString()) &&
                                    gcRwos.Cells["UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["UNIT_CD"].Value.ToString()) &&
                                    gcRwos.Cells["DENPYOU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                                {
                                    string hinmeiCd = gcRwos.Cells["HINMEI_CD"].Value.ToString();
                                    string unitCd = gcRwos.Cells["UNIT_CD"].Value.ToString();
                                    string denpyouKbnCd = gcRwos.Cells["DENPYOU_KBN_CD"].Value.ToString();
                                    if (detailItemEntity.HINMEI_CD == hinmeiCd
                                     && detailItemEntity.DENPYOU_KBN_CD == SqlInt16.Parse(denpyouKbnCd)
                                     && detailItemEntity.UNIT_CD == SqlInt16.Parse(unitCd))
                                    {
                                        detailItemEntity.KANSANCHI = (gcRwos.Cells["KANSANCHI"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["KANSANCHI"].Value.ToString())) ? SqlDecimal.Parse(gcRwos.Cells["KANSANCHI"].Value.ToString()) : SqlDecimal.Null;
                                        detailItemEntity.KANSAN_UNIT_CD = (gcRwos.Cells["KANSAN_UNIT_CD"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["KANSAN_UNIT_CD"].Value.ToString())) ? SqlInt16.Parse(gcRwos.Cells["KANSAN_UNIT_CD"].Value.ToString()) : SqlInt16.Null;
                                        detailItemEntity.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (gcRwos.Cells["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].Value.ToString())) ? SqlBoolean.Parse(gcRwos.Cells["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].Value.ToString()) : SqlBoolean.False;
                                        detailItemEntity.ANBUN_FLG = (gcRwos.Cells["ANBUN_FLG"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["ANBUN_FLG"].Value.ToString())) ? SqlBoolean.Parse(gcRwos.Cells["ANBUN_FLG"].Value.ToString()) : SqlBoolean.False;
                                        detailItemEntity.KEIYAKU_KBN = (gcRwos.Cells["KEIYAKU_KBN"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["KEIYAKU_KBN"].Value.ToString())) ? SqlInt16.Parse(gcRwos.Cells["KEIYAKU_KBN"].Value.ToString()) : SqlInt16.Null;
                                        detailItemEntity.KEIJYOU_KBN = (gcRwos.Cells["KEIJYOU_KBN"].Value != null && !string.IsNullOrWhiteSpace(gcRwos.Cells["KEIJYOU_KBN"].Value.ToString())) ? SqlInt16.Parse(gcRwos.Cells["KEIJYOU_KBN"].Value.ToString()) : SqlInt16.Null;
                                        this.updateCourseDetailItemsEntity.Add(detailItemEntity);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // QN Tue Anh #158986 END

        // Begin: LANDUONG - 20220215 - refs#160054
        private void SetDensiSeikyushoAndRakurakuVisible()
        {
            if (!denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = false;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label84.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;
            }
            else if (denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label84.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;
            }
            else if (denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = true;
                this.form.HAKKOUSAKI_CD.Visible = true;
                this.form.label84.Visible = false;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = false;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = false;
            }
            else if (!denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.labelDensiSeikyuuSho.Visible = true;
                this.form.labelHakkosaki.Visible = false;
                this.form.HAKKOUSAKI_CD.Visible = false;
                this.form.label84.Visible = true;
                this.form.RAKURAKU_CUSTOMER_CD.Visible = true;
                this.form.RAKURAKU_SAIBAN_BUTTON.Visible = true;

                this.form.label84.Location = new System.Drawing.Point(this.form.label84.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_CUSTOMER_CD.Location = new System.Drawing.Point(this.form.RAKURAKU_CUSTOMER_CD.Location.X, this.form.labelHakkosaki.Location.Y);
                this.form.RAKURAKU_SAIBAN_BUTTON.Location = new System.Drawing.Point(this.form.RAKURAKU_SAIBAN_BUTTON.Location.X, this.form.labelHakkosaki.Location.Y - 1);
            }

            return;
        }

        public bool HakkousakuAndRakurakuCDCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.TorihikisakiCode.Text))
                {
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                    this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                    this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                }
                else
                {
                    M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                    queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    if (seikyuuEntity != null)
                    {
                        if (seikyuuEntity.OUTPUT_KBN == 3)
                        {
                            this.form.HAKKOUSAKI_CD.Text = string.Empty;
                            this.form.HAKKOUSAKI_CD.Enabled = false;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = true;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;

                            // 楽楽顧客コードの採番ボタン
                            if (this.sysinfoEntity != null && !this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                                && this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 1)
                            {
                                this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                            }
                            else if (this.sysinfoEntity != null && !this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN.IsNull
                                && this.sysinfoEntity.RAKURAKU_CODE_NUMBERING_KBN == 2)
                            {
                                this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = true;
                            }
                        }
                        else if (seikyuuEntity.OUTPUT_KBN == 2)
                        {
                            this.form.HAKKOUSAKI_CD.Enabled = true;
                            this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                        }
                        else
                        {
                            this.form.HAKKOUSAKI_CD.Text = string.Empty;
                            this.form.HAKKOUSAKI_CD.Enabled = false;
                            this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                            this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                            this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                        }
                    }
                    else
                    {
                        this.form.HAKKOUSAKI_CD.Text = string.Empty;
                        this.form.HAKKOUSAKI_CD.Enabled = false;
                        this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                        this.form.RAKURAKU_CUSTOMER_CD.Enabled = false;
                        this.form.RAKURAKU_SAIBAN_BUTTON.Enabled = false;
                    }
                }

                this.form.HAKKOUSAKI_CD.BackColor = Constans.NOMAL_COLOR;
                this.form.RAKURAKU_CUSTOMER_CD.BackColor = Constans.NOMAL_COLOR;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HakkousakuCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        internal void SaibanRakurakuCode()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                var code = MasterCommonLogic.IsOverRakurakuCodeLimit();
                if (code < 0)
                {
                    msgLogic.MessageBoxShow("E041");
                    this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
                }

                this.form.RAKURAKU_CUSTOMER_CD.Text = code.ToString();

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanRakurakuCode", ex);
                this.form.RAKURAKU_CUSTOMER_CD.Text = string.Empty;
            }
        }

        internal string GetBeforeRakurakuCode()
        {
            string ret = string.Empty;

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (this.GenbaEntity != null && !string.IsNullOrEmpty(this.GenbaEntity.RAKURAKU_CUSTOMER_CD))
                {
                    ret = this.GenbaEntity.RAKURAKU_CUSTOMER_CD;
                }
            }

            return ret;
        }
        // End: LANDUONG - 20220215 - refs#160054

        #region ｼｮｰﾄﾒｯｾｰｼﾞオプション

        #region ｼｮｰﾄﾒｯｾｰｼﾞタブ-CellValidating
        internal bool SMSCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 入力した携帯番号があればｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタを検索
                if (e.CellName.Equals("MOBILE_PHONE_NUMBER"))
                {
                    var row = this.form.SMSReceiverIchiran.Rows[e.RowIndex];

                    if(row["MOBILE_PHONE_NUMBER"].FormattedValue != null && !string.IsNullOrEmpty(row["MOBILE_PHONE_NUMBER"].FormattedValue.ToString()))
                    {
                        // ハイフンを取り除く
                        string mPhoneNumber = row["MOBILE_PHONE_NUMBER"].FormattedValue.ToString();
                        if (mPhoneNumber.Contains("-"))
                        {
                            mPhoneNumber = mPhoneNumber.Replace("-", "");
                            row["MOBILE_PHONE_NUMBER"].Value = mPhoneNumber;
                        }

                        // 桁数チェック
                        if (mPhoneNumber.Length != 11)
                        {
                            msgLogic.MessageBoxShowError("携帯番号が正しくありません。正しい番号を入力してください。");
                            e.Cancel = true;
                            return false;
                        }

                        // 重複チェック
                        foreach (Row smsRow in this.form.SMSReceiverIchiran.Rows)
                        {
                            if (smsRow.IsNewRow) continue;
                            if (smsRow.Index == e.RowIndex) continue;

                            if (smsRow["MOBILE_PHONE_NUMBER"].Value != null
                                && !string.IsNullOrEmpty(smsRow["MOBILE_PHONE_NUMBER"].Value.ToString()))
                            {
                                string targetMPhoneNumber = smsRow["MOBILE_PHONE_NUMBER"].Value.ToString();

                                if (mPhoneNumber.Equals(targetMPhoneNumber))
                                {
                                    msgLogic.MessageBoxShow("E031", "携帯番号");
                                    e.Cancel = true;
                                    return false;
                                }
                            }
                        }

                        M_SMS_RECEIVER entity = this.daoSmsReceiver.GetDataByPhoneNumber(mPhoneNumber);
                        if (entity != null)
                        {
                            row["DELETE_FLG"].Value = entity.DELETE_FLG.ToString();
                            row["RECEIVER_NAME"].Value = entity.RECEIVER_NAME;
                            row["SYSTEM_ID"].Value = entity.SYSTEM_ID.ToString();
                            row["RENKEI_FLG"].Value = entity.RENKEI_FLG.ToString();

                            if(entity.RENKEI_FLG)
                            {
                                this.form.SMSReceiverIchiran.Rows[e.RowIndex].Cells["MOBILE_PHONE_NUMBER"].ReadOnly = true;
                            }
                        }
                    }
                    else if(row["MOBILE_PHONE_NUMBER"].FormattedValue == null || string.IsNullOrEmpty(row["MOBILE_PHONE_NUMBER"].FormattedValue.ToString()))
                    {
                        row["DELETE_FLG"].Value = false;
                        row["RECEIVER_NAME"].Value = "";
                        row["SYSTEM_ID"].Value = DBNull.Value;
                        row["RENKEI_FLG"].Value = false;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogUtility.Error("SMSCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        #endregion

        #region ｼｮｰﾄﾒｯｾｰｼﾞ可否切替用
        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ可否切替用
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        internal void SmsEnable(bool flg)
        {
            this.form.SMSReceiverIchiran.ReadOnly = flg;
            if (this.form.SMSReceiverIchiran.ReadOnly)
            {
                this.SmsReceiverTable_Init();
            }
            else
            {
                // 新規モード（複写）である場合は再表示しない
                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 再表示
                    this.SmsReceiverTable_GetDate();
                }
            }

            foreach (Row row in this.form.SMSReceiverIchiran.Rows)
            {
                row.Cells["DELETE_FLG"].ReadOnly = flg;
                row.Cells["RECEIVER_NAME"].ReadOnly = flg;
                row.Cells["registButton"].Enabled = !flg;
                row.Cells["copyButton"].Enabled = !flg;
                if (flg == false)
                {
                    row.Cells["DELETE_FLG"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["RECEIVER_NAME"].Style.BackColor = Constans.NOMAL_COLOR;
                    // 連携済みである携帯番号は、ReadOnly変更無
                    if (row.Cells["STATUS"].Value != null && row.Cells["STATUS"].Value.ToString() == "連携済")
                    {
                        row.Cells["MOBILE_PHONE_NUMBER"].ReadOnly = true;
                        row.Cells["MOBILE_PHONE_NUMBER"].Style.BackColor = Constans.READONLY_COLOR;
                    }
                    else
                    {
                        row.Cells["MOBILE_PHONE_NUMBER"].ReadOnly = false;
                        row.Cells["MOBILE_PHONE_NUMBER"].Style.BackColor = Constans.NOMAL_COLOR;
                    }
                }
                else
                {
                    row.Cells["DELETE_FLG"].Style.BackColor = Constans.READONLY_COLOR;
                    row.Cells["MOBILE_PHONE_NUMBER"].Style.BackColor = Constans.READONLY_COLOR;
                    row.Cells["RECEIVER_NAME"].Style.BackColor = Constans.READONLY_COLOR;
                }
            }
        }
        #endregion

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者データテーブルの初期化
        /// </summary>
        private void SmsReceiverTable_Init()
        {
            this.SmsReceiverTable = this.daoGenba.GetDataBySqlFile(this.GET_SMS_RECEIVER_STRUCT_SQL, new M_GENBA());
            this.SetIchiranSmsReceiver();
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者データテーブルの表示（データ取得）
        /// </summary>
        private void SmsReceiverTable_GetDate()
        {
            M_GENBA genba = new M_GENBA();
            genba.GYOUSHA_CD = this.GyoushaCd;
            genba.GENBA_CD = this.GenbaCd;
            this.SmsReceiverTable = this.daoGenba.GetDataBySqlFile(this.GET_SMS_RECEIVER_DATA_SQL, genba);
            if (SmsReceiverTable != null)
            {
                this.SetIchiranSmsReceiver();
            }
        }

        #region データセット
        /// <summary>
        /// 検索結果をｼｮｰﾄﾒｯｾｰｼﾞ受信者一覧に設定
        /// </summary>
        /// <param name="isCellEvent">CellEnter,CellValidatingイベントからの呼出時に true</param>
        internal void SetIchiranSmsReceiver(bool isCellEvent = false)
        {
            try
            {
                var table = this.SmsReceiverTable;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].AllowDBNull = true;
                    table.Columns[i].ReadOnly = false;
                    table.Columns[i].Unique = false;
                }

                this.form.SMSReceiverIchiran.DataSource = table;

                this.SetIchiranSmsRowControl(isCellEvent);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranSmsReceiver", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者一覧の行内容による制御を実施する
        /// </summary>
        /// <param name="isCellEvent">CellEnter,CellValidatingイベントからの呼出時に true</param>
        public bool SetIchiranSmsRowControl(bool isCellEvent = false)
        {
            try
            {
                foreach (Row row in this.form.SMSReceiverIchiran.Rows)
                {
                    // 削除、参照モード時は、各セル非活性化
                    if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        foreach (var tmpcell in row.Cells)
                        {
                            switch(tmpcell.Name)
                            {
                                case "DELETE_FLG":
                                case "MOBILE_PHONE_NUMBER":
                                case "RECEIVER_NAME":
                                case "registButton":
                                case "copyButton":
                                    tmpcell.Enabled = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // ｼｮｰﾄﾒｯｾｰｼﾞ送信区分=1．するの場合、各セルを活性、有効化
                        if (this.form.SMS_USE.Text == "1")
                        {
                            foreach (var tmpcell in row.Cells)
                            {
                                switch(tmpcell.Name)
                                {
                                    case "DELETE_FLG":
                                    case "MOBILE_PHONE_NUMBER":
                                    case "RECEIVER_NAME":
                                        tmpcell.ReadOnly = false;
                                        tmpcell.Style.BackColor = Constans.NOMAL_COLOR;
                                        break;
                                    case "registButton":
                                    case "copyButton":
                                        tmpcell.Enabled = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            row.Cells["registButton"].Enabled = false;
                            row.Cells["copyButton"].Enabled = false;
                        }
                        // 連携済みである携帯番号は、ReadOnlyをTrueに変更
                        if (row.Cells["STATUS"].Value != null && row.Cells["STATUS"].Value.ToString() == "連携済")
                        {
                            row.Cells["MOBILE_PHONE_NUMBER"].ReadOnly = true;
                            row.Cells["MOBILE_PHONE_NUMBER"].Style.BackColor = Constans.READONLY_COLOR;
                        }
                    }
                }
                this.form.SMSReceiverIchiran.Refresh();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranSmsRowControl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 登録
        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SMSReceiverInfoRegist(WINDOW_TYPE windowType, int rowIndex)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                LogUtility.DebugMethodStart(windowType, rowIndex);
                bool deleteFlg = false;
                bool addFlg = false;

                // 画面モードチェック
                if(windowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    msgLogic.MessageBoxShowError("受信者のマスタ登録は、修正モードの場合のみ行えます。");
                    return;
                }

                // 入力チェック
                if(string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value.ToString()))
                {
                    msgLogic.MessageBoxShow("E001", "携帯番号");
                    return;
                }
                // !string.IsNullOrEmpty(genba.Rows[0]["SAISHUU_SHOBUNJOU_KBN"].ToString())
                if(!string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["DELETE_FLG"].Value.ToString()) && (bool)this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["DELETE_FLG"].Value)
                {
                    deleteFlg = true;
                }

                SqlInt32 systemId = 0;
                // システムID、携帯番号を使って新規登録・更新・削除を実施
                if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["SYSTEM_ID"].Value != DBNull.Value)
                {
                    systemId = SqlInt32.Parse(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["SYSTEM_ID"].Value.ToString());
                }
                if(deleteFlg == true && systemId == 0)
                {
                    //msgLogic.MessageBoxShowError("受信者のマスタに登録されていない携帯番号は削除できません。");
                    return;
                }
                string mPhoneNumber = this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value.ToString();

                // マスタ存在チェック
                using (Transaction tran = new Transaction())
                {
                    if (deleteFlg)
                    {
                        M_SMS_RECEIVER_LINK_GENBA del = new M_SMS_RECEIVER_LINK_GENBA();
                        del.SYSTEM_ID = systemId;
                        del.MOBILE_PHONE_NUMBER = mPhoneNumber;
                        del.GYOUSHA_CD = this.GyoushaCd;
                        del.GENBA_CD = this.GenbaCd;
                        var deleteEntityGenba = this.daoSmsReceiverGenba.CheckOtherLinkGenba(del);
                        if (deleteEntityGenba != null)
                        {
                            this.daoSmsReceiverGenba.DeleteLinkGenba(deleteEntityGenba);
                        }
                        else
                        {
                            msgLogic.MessageBoxShowError("削除対象の携帯番号が存在しませんでした。\n再度実行しても失敗する場合は、システム管理社にご連絡ください。");
                            return;
                        }
                    }
                    else
                    {
                        string receiverName = string.Empty;
                        if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value != DBNull.Value)
                        {
                            receiverName = this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value.ToString();
                        }

                        // 携帯番号より受信者マスタを検索
                        M_SMS_RECEIVER entity = this.daoSmsReceiver.GetDataByPhoneNumber(mPhoneNumber);
                        if (entity == null && systemId == 0)
                        {
                            // 新規登録(M_SMS_RECEIVER)
                            M_SMS_RECEIVER newEntity = new M_SMS_RECEIVER();
                            newEntity.SYSTEM_ID = this.daoSmsReceiver.GetMaxPlusKey();
                            newEntity.RENKEI_FLG = false;
                            if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["DELETE_FLG"].Value != DBNull.Value)
                            {
                                newEntity.DELETE_FLG = (bool)this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["DELETE_FLG"].Value;
                            }
                            else
                            {
                                newEntity.DELETE_FLG = false;
                            }
                            newEntity.MOBILE_PHONE_NUMBER = mPhoneNumber;
                            if(!string.IsNullOrEmpty(receiverName))
                            {
                                newEntity.RECEIVER_NAME = receiverName;
                            }

                            // 更新者情報設定
                            var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_SMS_RECEIVER>(newEntity);
                            dataBinderLogicGenba.SetSystemProperty(newEntity, false);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), newEntity);

                            this.daoSmsReceiver.Insert(newEntity);

                            // 新規登録(M_SMS_RECEIVER_LINK_GENBA)
                            M_SMS_RECEIVER_LINK_GENBA newEntityGenba = new M_SMS_RECEIVER_LINK_GENBA();
                            newEntityGenba.SYSTEM_ID = newEntity.SYSTEM_ID;
                            newEntityGenba.MOBILE_PHONE_NUMBER = newEntity.MOBILE_PHONE_NUMBER;
                            newEntityGenba.GYOUSHA_CD = this.GyoushaCd.ToUpper();
                            newEntityGenba.GENBA_CD = this.GenbaCd.ToUpper();
                            this.daoSmsReceiverGenba.Insert(newEntityGenba);

                            addFlg = true;
                        }
                        else if(entity == null && systemId != 0)
                        {
                            // 更新
                            M_SMS_RECEIVER updateEntity = this.daoSmsReceiver.GetDataBySystemId(systemId.ToString());

                            // 変更前の電話番号を保持（リンクテーブル側で削除する用）
                            var delPhone_Genba = updateEntity.MOBILE_PHONE_NUMBER;

                            updateEntity.MOBILE_PHONE_NUMBER = mPhoneNumber;
                            if(!string.IsNullOrEmpty(receiverName))
                            {
                                updateEntity.RECEIVER_NAME = receiverName;
                            }
                            this.daoSmsReceiver.Update(updateEntity);

                            // 削除→新規登録(M_SMS_RECEIVER_LINK_GENBA)
                            M_SMS_RECEIVER_LINK_GENBA updateEntityGenba = new M_SMS_RECEIVER_LINK_GENBA();
                            updateEntityGenba.SYSTEM_ID = updateEntity.SYSTEM_ID;
                            updateEntityGenba.MOBILE_PHONE_NUMBER = delPhone_Genba;
                            updateEntityGenba.GYOUSHA_CD = this.GyoushaCd.ToUpper();
                            updateEntityGenba.GENBA_CD = this.GenbaCd.ToUpper();
                            this.daoSmsReceiverGenba.DeleteLinkGenba(updateEntityGenba);

                            // 携帯番号だけ更新
                            updateEntityGenba.MOBILE_PHONE_NUMBER = updateEntity.MOBILE_PHONE_NUMBER;
                            this.daoSmsReceiverGenba.Insert(updateEntityGenba);

                            // 選択行の携帯番号が空電プッシュに連携されていない場合に追加
                            if(!this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].ReadOnly)
                            {
                                addFlg = true;
                            }
                        }
                        else
                        {
                            var dlg = DialogResult.None;
                            

                            // 選択行の携帯番号が空電プッシュに連携されていない場合
                            if(!this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].ReadOnly)
                            {
                                dlg = msgLogic.MessageBoxShowConfirm("携帯番号を空電プッシュ（システム）へ連携しますか？");
                                if (dlg == DialogResult.Yes)
                                {
                                    addFlg = true;
                                }

                                this.SMSReceiver_LinkGenba_Update(entity, receiverName);
                            }
                            else
                            {
                                dlg = msgLogic.MessageBoxShowConfirm("既に登録済みの携帯番号です。更新しますか？");
                                if (dlg == DialogResult.Yes)
                                {
                                    this.SMSReceiver_LinkGenba_Update(entity, receiverName);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    tran.Commit();
                }
                if (deleteFlg)
                {
                    msgLogic.MessageBoxShowInformation("空電プッシュ（システム）からの削除が必要な場合、\nショートメッセージ受信者入力から削除処理を行ってください。");
                }
                else 
                {
                    if(addFlg)
                    {
                        // 送信リスト設定変更API実行
                        ListUpdateAPI(rowIndex);
                    }
                    else
                    {
                        msgLogic.MessageBoxShowInformation("受信者のマスタ登録が、正常に完了しました");
                    }
                }

                // 再表示
                this.SmsReceiverTable_GetDate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SMSReceiverInfoRegist", ex);
                msgLogic.MessageBoxShowError("受信者のマスタ登録に失敗しました。\n再度実行しても失敗する場合は、システム管理社にご連絡ください。");
                LogUtility.DebugMethodEnd(windowType, rowIndex);
            }
        }
        #endregion

        #region 空電プッシュAPI
        // API連携時のメッセージ内容
        private string smsMsg = string.Empty;

        /// <summary>
        /// 送信リスト設定変更API
        /// </summary>
        /// <param name="registFlg"></param>
        /// <param name="deleteFlg"></param>
        public void ListUpdateAPI(int rowIndex)
        {
            smsMsg = string.Empty;
            bool smsResponseFlg = false;
            var renkeiUpdateList = new List<M_SMS_RECEIVER>();
            M_SMS_RECEIVER entity = null;

            try 
            {
                // TLS1.2を指定（指定しないとAPI連携不可）
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                var url = "https://push-se.karaden.jp/v2/karadensetfilter.json";

                #region パラメータ項目チェック
                M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();

                string token = sysInfo.KARADEN_ACCESS_KEY;

                string securitycode = sysInfo.KARADEN_SECURITY_CODE;
                var type = "1"; // 追加のみ
                var pNumber = this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value.ToString();
                var status = "1";

                #endregion

                StringBuilder sbJson = new StringBuilder();
                sbJson.Append("{");
                sbJson.Append("\"token\":\"" + token + "\",");
                sbJson.Append("\"securitycode\":\"" + securitycode + "\",");
                sbJson.Append("\"type\":\"" + type + "\",");
                sbJson.Append("\"list\":[");

                entity = this.daoSmsReceiver.GetDataByPhoneNumber(pNumber);
                renkeiUpdateList.Add(entity);

                // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ参照
                sbJson.Append("{\"phonenumber\":\"" + pNumber + "\",");
                sbJson.Append("\"status\":\"" + status + "\"}");
                    
                //// 余計な文字列が入らないように一部文字を取り除く
                //sbJson.Remove(sbJson.Length - 1, 1);
                sbJson.Append("]}");

                // リクエスト作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/json";

                // ポストデータをリクエストに書き込む
                using (StreamWriter reqStreamWriter = new StreamWriter(req.GetRequestStream()))
                    reqStreamWriter.Write(sbJson);

                // レスポンスの取得
                WebResponse response = (HttpWebResponse)req.GetResponse();

                // 結果の読み込み
                using (Stream resStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(resStream, Encoding.GetEncoding("Shift_JIS")))
                    {
                        smsMsg += "【ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ】\n";
                        smsMsg += "正常に完了しました。\n\n";

                        string responseText = reader.ReadToEnd();

                        this.smsApiMessage(responseText, out smsResponseFlg);

                        if (smsResponseFlg)
                        {
                            using (Transaction tran = new Transaction())
                            {
                                // リストへの追加・更新トランザクション
                                foreach (M_SMS_RECEIVER RenkeiEntity in renkeiUpdateList)
                                {
                                    if (!RenkeiEntity.RENKEI_FLG)
                                    {
                                        RenkeiEntity.RENKEI_FLG = true;
                                    }
                                    this.daoSmsReceiver.Update(RenkeiEntity);
                                }
                                tran.Commit();
                            }
                        }
                    }
                }
            }
            #region API連携時のエラー
            catch (WebException ex)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("ListUpdateAPI", ex);

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errRes = (HttpWebResponse)ex.Response;
                    var title = string.Empty;

                    switch (errRes.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:         // 400
                            // リクエスト不正
                            title = "HTTP STATUS 400 Bad Request";
                            break;
                        case HttpStatusCode.Unauthorized:       // 401
                            // アクセストークン無効
                            title = "HTTP STATUS 401 Unauthorized";
                            break;
                        case HttpStatusCode.PaymentRequired:    // 402
                            // 
                            title = "HTTP STATUS 402 Payment Required";
                            break;
                        case HttpStatusCode.Forbidden:          // 403
                            // アクセス拒否
                            title = "HTTP STATUS 403 Forbidden";
                            break;
                        case HttpStatusCode.NotFound:           // 404
                            // 指定されたページが存在しない。権限が無い。
                            title = "HTTP STATUS 404 Not Found";
                            break;
                        case HttpStatusCode.MethodNotAllowed:   // 405
                            // 未許可のメソッド
                            title = "HTTP STATUS 405 Method Not Allowed";
                            break;
                        case HttpStatusCode.InternalServerError:// 500
                            // サーバ内部エラー
                            title = "HTTP STATUS 500 Internal Server Error";
                            break;
                        default:
                            title = "その他エラー";
                            break;
                    }
                    MessageBox.Show("API連携において、エラーが発生しました。\n\rタイトルのステータスコードを確認してください。", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            #endregion
        }
        #endregion

        #region SMSAPIレスポンスメッセージ一覧
        /// <summary>
        /// SMSAPIレスポンスメッセージ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="smsResponseFlg">true:正常、false:失敗</param>
        public void smsApiMessage(string msg, out bool smsResponseFlg)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            smsResponseFlg = false;
            smsMsg += "【空電プッシュ-送信リスト】\n";

            if (msg.Contains("\"status\":\"100\""))
            {
                smsMsg += "正常に完了しました。";
                
                msgLogic.MessageBoxShowInformation(smsMsg);

                smsResponseFlg = true;
            }
            else if (msg.Contains("\"status\":\"201\""))
            {
                smsMsg += "認証に失敗しました。\nアクセスキー、セキュリティーコードなどに誤りがあります。\n確認を行ってください（コード：201）";
            }
            else if (msg.Contains("\"status\":\"900\""))
            {
                smsMsg += "送信リストの登録に失敗しました。エラー内容を確認してください。\n";

                if (msg.Contains("\"errorstatus\":\"202\""))
                {
                    smsMsg += "・いずれかの携帯番号に誤りがあります（エラーコード：202）。\n";
                }
                if (msg.Contains("\"errorstatus\":\"901\""))
                {
                    smsMsg += "・いずれかの携帯番号は既に登録済みです（コード：901）。\n";
                }
                if (msg.Contains("\"errorstatus\":\"903\""))
                {
                    smsMsg += "・送信リストが未指定です （コード：903）。\n";
                }
            }
            else
            {
                smsMsg += "送信リストの登録に失敗しました。\n再度実行しても失敗する場合は、システム管理者にご連絡ください。";
            }
            if (!smsResponseFlg)
            {
                msgLogic.MessageBoxShowError(smsMsg);
            }
        }
        #endregion

        #region 受信者マスタ、リンクテーブル更新

        /// <summary>
        /// 受信者マスタ、リンクテーブル更新
        /// </summary>
        /// <param name="entity"></param>
        internal void SMSReceiver_LinkGenba_Update(M_SMS_RECEIVER entity, string ReceiverName)
        {
            if(!string.IsNullOrEmpty(ReceiverName))
            {
                entity.RECEIVER_NAME = ReceiverName;
            }
            this.daoSmsReceiver.Update(entity);

            M_SMS_RECEIVER_LINK_GENBA searchEntityGenba = new M_SMS_RECEIVER_LINK_GENBA();

            searchEntityGenba.SYSTEM_ID = entity.SYSTEM_ID;
            searchEntityGenba.MOBILE_PHONE_NUMBER = entity.MOBILE_PHONE_NUMBER;
            searchEntityGenba.GYOUSHA_CD = this.GyoushaCd.ToUpper();
            searchEntityGenba.GENBA_CD = this.GenbaCd.ToUpper();
            M_SMS_RECEIVER_LINK_GENBA reEntity = this.daoSmsReceiverGenba.GetLinkGenba(searchEntityGenba);
            if(reEntity == null)
            {
                this.daoSmsReceiverGenba.Insert(searchEntityGenba);
            }
        }

        #endregion

        #region 複写前、事前チェック
        internal bool SMSReceiverInfoCopyBeforeCheck()
        {
            try
            {
                // 重複フラグ
                bool dupFlg = false;
                if(!string.IsNullOrEmpty(this.form.GenbaKeitaiTel.Text))
                {
                    foreach(Row row in this.form.SMSReceiverIchiran.Rows)
                    {
                        if(row.Cells["MOBILE_PHONE_NUMBER"].Value.ToString() == this.form.GenbaKeitaiTel.Text)
                        {
                            this.form.errmessage.MessageBoxShowError("既に入力されている携帯電話番号のため、複写処理をキャンセルします。");
                            dupFlg = true;
                            break;
                        }
                    }
                }
                return dupFlg;
            }
            catch(Exception ex)
            {
                LogUtility.Error("SMSReceiverInfoCopyBeforeCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        #region 複写
        /// <summary>
        /// 複写ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SMSReceiverInfoCopy(int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(rowIndex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool copyFlg = false;

                if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["STATUS"].Value != null)
                {
                    if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["STATUS"].Value.ToString() == "連携済")
                    {
                        msgLogic.MessageBoxShowError("登録済みの受信者のため、複写は行えません。");
                        return;
                    }
                }

                // 携帯番号、受信者名どちらも非活性の場合処理無し
                if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].ReadOnly &&
                    this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].ReadOnly)
                {
                    return;
                }

                // 携帯番号、受信者名どちらも入力済みの場合は処理無し
                if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value != null &&
                   !string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value.ToString()))
                {
                    if(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value != null &&
                       !string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value.ToString()))
                    {
                        return;
                    }
                }

                if(string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.form.GenbaKeitaiTel.Text))
                    {
                        this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["MOBILE_PHONE_NUMBER"].Value = this.form.GenbaKeitaiTel.Text;
                        this.form.SMSReceiverIchiran.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowIndex, "MOBILE_PHONE_NUMBER");
                        copyFlg = true;
                    }
                }
                if(string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.form.TantoushaCode.Text))
                    {
                        this.form.SMSReceiverIchiran.Rows[rowIndex].Cells["RECEIVER_NAME"].Value = this.form.TantoushaCode.Text;
                        this.form.SMSReceiverIchiran.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowIndex, "RECEIVER_NAME");
                        copyFlg = true;
                    }
                }
                this.form.SMSReceiverIchiran.NotifyCurrentCellDirty(copyFlg);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SMSReceiverInfoCopy", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(rowIndex);
            }
        }
        #endregion

        #region [F9]登録　入力チェック

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞオプションの入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSms()
        {
            bool ret = false;

            for(int i = 0; i < this.form.SMSReceiverIchiran.RowCount - 1; i++)
            {
                // 携帯番号の入力チェック
                if (string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString()))
                {
                    this.form.errmessage.MessageBoxShowWarn("ショートメッセージ機能を使用する場合、携帯番号を入力してください。");
                    //this.form.GenbaLatitude.Focus();
                    return true;
                }

                // 携帯番号の桁数チェック
                if (this.form.SMSReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString().Length != 11)
                {
                    this.form.errmessage.MessageBoxShowWarn("携帯番号が正しくありません。正しい番号を入力してください。");
                    //this.form.GenbaLongitude.Focus();
                    return true;
                }
            }

            return ret;
        }

        #endregion

        #region [F9]登録　データ登録

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ情報登録
        /// </summary>
        /// <returns></returns>
        internal void SmsReceicerRegist()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // ｼｮｰﾄﾒｯｾｰｼﾞオプションの入力チェック
            if(this.CheckSms())
            {
                return;
            }

            for(int i = 0; i < this.form.SMSReceiverIchiran.RowCount - 1; i++)
            {
                bool deleteFlg = false;

                // 削除チェックボックスがチェックオンである場合
                if(this.form.SMSReceiverIchiran.Rows[i].Cells["DELETE_FLG"].Value != DBNull.Value)
                {
                    if((bool)this.form.SMSReceiverIchiran.Rows[i].Cells["DELETE_FLG"].Value)
                    {
                        deleteFlg = true;
                    }
                }

                if (deleteFlg)
                {
                    if (this.form.SMSReceiverIchiran.Rows[i].Cells["STATUS"].Value != null &&
                        (this.form.SMSReceiverIchiran.Rows[i].Cells["STATUS"].Value.ToString() == "連携済" ||
                         this.form.SMSReceiverIchiran.Rows[i].Cells["STATUS"].Value.ToString() == "送信待"))
                    {
                        // リンクテーブル
                        M_SMS_RECEIVER_LINK_GENBA del = new M_SMS_RECEIVER_LINK_GENBA();
                        if(this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value == DBNull.Value)
                        {
                            // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタに新規登録したシステムIDを参照
                            del.SYSTEM_ID = del.SYSTEM_ID;
                        }
                        else
                        {
                            del.SYSTEM_ID = SqlInt32.Parse(this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value.ToString());
                        }
                        del.MOBILE_PHONE_NUMBER = (string)this.form.SMSReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value;
                        del.GYOUSHA_CD = this.GyoushaCd;
                        del.GENBA_CD = this.GenbaCd;
                        var deleteEntityGenba = this.daoSmsReceiverGenba.CheckOtherLinkGenba(del);
                        if (deleteEntityGenba != null)
                        {
                            this.daoSmsReceiverGenba.DeleteLinkGenba(deleteEntityGenba);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ
                    M_SMS_RECEIVER entity = new M_SMS_RECEIVER();
                    if (this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value == DBNull.Value)
                    {
                        entity.SYSTEM_ID = this.daoSmsReceiver.GetMaxPlusKey();
                    }
                    else
                    {
                        entity.SYSTEM_ID = SqlInt32.Parse(this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value.ToString());
                    }

                    if (this.form.SMSReceiverIchiran.Rows[i].Cells["RENKEI_FLG"].Value != DBNull.Value)
                    {
                        entity.RENKEI_FLG = (bool)this.form.SMSReceiverIchiran.Rows[i].Cells["RENKEI_FLG"].Value;
                    }
                    else
                    {
                        entity.RENKEI_FLG = false;
                    }
                    entity.DELETE_FLG = false;
                    entity.MOBILE_PHONE_NUMBER = this.form.SMSReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString();
                    if(!string.IsNullOrEmpty(this.form.SMSReceiverIchiran.Rows[i].Cells["RECEIVER_NAME"].Value.ToString()))
                    {
                        entity.RECEIVER_NAME = this.form.SMSReceiverIchiran.Rows[i].Cells["RECEIVER_NAME"].Value.ToString();
                    }

                    // 更新者情報設定
                    var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_SMS_RECEIVER>(entity);
                    dataBinderLogicGenba.SetSystemProperty(entity, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entity);

                    M_SMS_RECEIVER searchEntity = this.daoSmsReceiver.GetDataBySystemId(entity.SYSTEM_ID.ToString());
                    // 新規行である場合は追加
                    if(searchEntity == null)
                    {
                        this.daoSmsReceiver.Insert(entity);
                    }
                    // 既存行である場合は更新
                    else
                    {
                        // NotSingleRowUpdatedRuntimeException回避
                        entity.TIME_STAMP = searchEntity.TIME_STAMP;
                        this.daoSmsReceiver.Update(entity);
                    }


                    // リンクテーブル
                    M_SMS_RECEIVER_LINK_GENBA enG = new M_SMS_RECEIVER_LINK_GENBA();
                    if(this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value == DBNull.Value)
                    {
                        // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタに新規登録したシステムIDを参照
                        enG.SYSTEM_ID = entity.SYSTEM_ID;
                    }
                    else
                    {
                        enG.SYSTEM_ID = SqlInt32.Parse(this.form.SMSReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value.ToString());
                    }
                    enG.MOBILE_PHONE_NUMBER = (string)this.form.SMSReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value;
                    enG.GYOUSHA_CD = this.GenbaEntity.GYOUSHA_CD;
                    enG.GENBA_CD = this.GenbaEntity.GENBA_CD;
                    M_SMS_RECEIVER_LINK_GENBA newEnGenba = this.daoSmsReceiverGenba.GetLinkGenba(enG);
                    if(newEnGenba != null)
                    {
                        this.daoSmsReceiverGenba.DeleteLinkGenba(newEnGenba);
                        this.daoSmsReceiverGenba.Insert(newEnGenba);
                    }
                    else
                    {
                        this.daoSmsReceiverGenba.Insert(enG);
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}