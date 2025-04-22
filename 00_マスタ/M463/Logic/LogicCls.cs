// $Id: LogicCls.cs 51960 2015-06-10 08:44:29Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Event;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.HikiaiGenbaHoshu.APP;
using Shougun.Core.Master.HikiaiGenbaHoshu.Const;
using Shougun.Core.Master.HikiaiGenbaHoshu.DBAccesser;
using Shougun.Core.Master.HikiaiGenbaHoshu.Entity;
using Shougun.Core.Master.HikiaiGenbaHoshu.Validator;
using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Logic
{
    /// <summary>
    /// 現場保守画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.HikiaiGenbaHoshu.Setting.ButtonSetting.xml";
        private readonly string ButtonInfoXmlPath2 = "Shougun.Core.Master.HikiaiGenbaHoshu.Setting.ButtonSetting2.xml";

        /// <summary>
        /// 現場保守画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private List<M_HIKIAI_GENBA_TEIKI_HINMEI> genbaTeikiEntity;
        private List<M_HIKIAI_GENBA_TSUKI_HINMEI> genbaTsukiEntity;

        internal M_HIKIAI_GENBA genbaEntity;
        private M_GYOUSHA gyoushaEntity;
        private M_HIKIAI_GYOUSHA hikiaiGyoushaEntity;
        private M_TORIHIKISAKI torihikisakiEntity;
        private M_HIKIAI_TORIHIKISAKI hikiaiTorihikisakiEntity;
        private M_TODOUFUKEN todoufukenEntity;
        private M_CHIIKI chiikiEntity;
        private M_SHUUKEI_KOUMOKU shuukeiEntity;
        private M_GYOUSHU gyoushuEntity;
        private M_SYS_INFO sysinfoEntity;
        private M_KYOTEN kyotenEntity;
        private M_SHAIN shainEntity;
        private M_BUSHO bushoEntity;
        private M_MANIFEST_SHURUI manishuruiEntity;
        private M_MANIFEST_TEHAI manitehaiEntity;

        private int rowCntItaku;

        // No2267-->
        private List<String> messageList;
        // No2267<--

        /// <summary>
        /// 現場のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBADao daoHikiaiGenba;

        // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
        /// <summary>
        /// 現場_定期品名のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBA_TEIKI_HINMEIDao daoGenbaTeiki;

        // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
        /// <summary>
        /// 現場_月極品名のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBA_TSUKI_HINMEIDao daoGenbaTsuki;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 引合業者のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GYOUSHADao daoHikiaiGyousha;

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
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao daoSeikyuu;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao daoShiharai;

        /// <summary>
        /// 引合取引先のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao daoHikiaiTorihikisaki;

        /// <summary>
        /// 引合取引先請求のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao daoHikiaiSeikyuu;

        /// <summary>
        /// 引合取引先支払のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao daoHikiaiShiharai;

        /// <summary>
        /// マニ種類のDao
        /// </summary>
        private IM_MANIFEST_SHURUIDao daoManishurui;

        /// <summary>
        /// マニ手配のDao
        /// </summary>
        private IM_MANIFEST_TEHAIDao daoManitehai;

        // 20141209 katen #2927 実績報告書　フィードバック対応 start
        private M_CHIIKI upnHoukokushoTeishutsuChiikiEntity;
        // 20141209 katen #2927 実績報告書　フィードバック対応 end

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        private TabPageManager _tabPageManager = null;

        /// <summary>
        /// マニ種類保持用
        /// </summary>
        internal string maniShuruiSave = "0";

        /// <summary>
        /// マニ手配保持用
        /// </summary>
        internal string maniTehaiSave = "0";

        /// <summary>
        /// 委託契約書種類
        /// </summary>
        private Dictionary<string, string> ItakuKeiyakuShurui = new Dictionary<string, string>()
        {
            {"1", "処分委託契約"},
            {"2", "収集・運搬委託契約"},
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
            {"4", "完了"},
            {"5", "解約済"}
        };

        /// <summary>
        /// 前回定期品名セル名
        /// </summary>
        private string prevTeikiCellName = string.Empty;

        /// <summary>
        /// 採番でエラーが起きたか判断するフラグ
        /// </summary>
        internal bool SaibanErrorFlg = false;

        /// <summary>
        /// 電子申請オプションフラグ
        /// true = 有効 / false = 無効
        /// </summary>
        private bool DensisinseiOptionEnabledFlg;

        /// <summary>
        /// 取引先チェック時にエラーを表示するか判断するフラグ
        /// </summary>
        internal bool IsShowTorihikisakiError = false;

        internal int preRowIndex = -1;

        internal int preCellIndex = -1;

        #endregion

        #region プロパティ

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 引合業者使用フラグ
        /// </summary>
        public bool HikiaiGyoushaUseFlg { get; set; }

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
        /// 定期品名データテーブル
        /// </summary>
        public DataTable TeikiHinmeiTable { get; set; }

        /// <summary>
        /// 月極品名データテーブル
        /// </summary>
        public DataTable TsukiHinmeiTable { get; set; }

        /// <summary>
        /// 業者排出事業者区分
        /// </summary>
        public bool FlgGyoushaHaishutuKbn { get; set; }

        /// <summary>
        /// エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// ポップアップからデータセットを実施したか否かのフラグ
        /// </summary>
        public bool IsSetDataFromPopup { get; set; }

        /// <summary>
        /// 画面設定中フラグ
        /// </summary>
        public bool isSettingWindowData { get; set; }

        // 201400709 syunrei #947 №19 start
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GENBADao daoGenbaMastar;
        // 201400709 syunrei #947 №19 end

        #endregion

        #region A票～E票で使うカスタムコントロール

        // ラジオボタン
        public CustomRadioButton MANI_HENSOUSAKI_PLACE_KBN_1;
        public CustomRadioButton MANI_HENSOUSAKI_PLACE_KBN_2;
        public CustomRadioButton HensousakiKbn1;
        public CustomRadioButton HensousakiKbn2;
        public CustomRadioButton HensousakiKbn3;
        // テキストボックス
        public CustomNumericTextBox2 MANI_HENSOUSAKI_PLACE_KBN;
        public CustomNumericTextBox2 HensousakiKbn;
        public CustomAlphaNumTextBox ManiHensousakiTorihikisakiCode;
        public CustomAlphaNumTextBox ManiHensousakiGyoushaCode;
        public CustomAlphaNumTextBox ManiHensousakiGenbaCode;
        public CustomTextBox MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
        public CustomTextBox MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
        public CustomTextBox MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
        // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
        public CustomNumericTextBox2 MANIFEST_USE;
        // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
        public CustomTextBox ManiHensousakiTorihikisakiName;
        public CustomTextBox ManiHensousakiGyoushaName;
        public CustomTextBox ManiHensousakiGenbaName;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiGenba = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBADao>();
                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTeiki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBA_TEIKI_HINMEIDao>();
                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTsuki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GENBA_TSUKI_HINMEIDao>();
                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiGyousha = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_GYOUSHADao>();
                this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
                this.daoChiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
                this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
                this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
                this.daoGyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                this.daoShuukei = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                this.daoSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                this.daoShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoHikiaiSeikyuu = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoHikiaiShiharai = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();
                this.daoManishurui = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();
                this.daoManitehai = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();

                // 201400709 syunrei #947 №19 start
                this.daoGenbaMastar = DaoInitUtility.GetComponent<IM_GENBADao>();
                // 201400709 syunrei #947 №19 end

                this.isError = false;
                this.isSettingWindowData = false;

                _tabPageManager = new TabPageManager(this.form.ManiHensousakiKeishou2B1);

                // No2267-->
                this.messageList = new List<string>();
                // No2267<--

                // this.DensisinseiOptionEnabledFlg = r_framework.Configuration.AppConfig.AppOptions.Workflow;
                this.DensisinseiOptionEnabledFlg = Convert.ToBoolean(r_framework.Configuration.AppConfig.AppOptions.OptionDictionary["workflow"].value);
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                var parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);

                if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(windowType))
                {
                    // 新規モード時、返送先区分の活性制御が仕様通りに行われないため、データを画面に設定後、改めて返送先区分の活性制御を行う。
                    // 返送先区分の活性制御には○票使用区分や返送先区分のTextChangedイベント制御してるため
                    // どこかで無理が生じしてしまっているようです。
                    this.SettingHensouSakiKbn();
                }

                this.allControl = this.form.allControl;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // A票～E票タブ制御
                this.ChangeTabAtoE();

                // 201400709 syunrei #947 №19 start
                // 修正モードの場合、F1移行ボタンが利用可
                this.SetF1Enabled(windowType);
                // 201400709 syunrei #947 №19 end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 登録ボタン(F9)イベント生成
                if (denshiShinseiFlg)
                {
                    // 「F9:申請」となる場合のイベント生成
                    this.form.C_Regist(parentForm.bt_func9);
                    parentForm.bt_func9.Click += new EventHandler(this.form.Shinsei);
                    parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                    // 閉じるボタン(F12)イベント生成
                    parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                }
                else
                {
                    // 移行ボタン(F1)イベント生成
                    parentForm.bt_func1.Click += new EventHandler(this.form.Ikou);

                    // 新規ボタン(F2)イベント生成
                    parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                    // 修正ボタン(F3)イベント生成
                    parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                    // 一覧ボタン(F7)イベント生成
                    parentForm.bt_func7.Click += new EventHandler(this.form.FormSearch);

                    // 登録ボタン(F9)イベント生成
                    this.form.C_Regist(parentForm.bt_func9);
                    parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                    parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                    // 取消ボタン(F11)イベント生成
                    parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                    // 閉じるボタン(F12)イベント生成
                    parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                }

                // [1]返送先コピーイベント生成
                parentForm.bt_process1.Click += new EventHandler(this.form.ManifestoTabCopy);

                // 業者CD Validatedイベント生成
                this.form.GyoushaCode.Validated += new EventHandler(this.form.GyoushaCDValidated);

                // 取引先CD Validatedイベント生成
                this.form.TorihikisakiCode.Validated += new EventHandler(this.form.TorihikisakiCDValidated);

                //// マニ取引先CD Validatedイベント生成
                // this.form.ManiHensousakiTorihikisakiCode.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);

                //// マニ業者CD Validatedイベント生成
                // this.form.ManiHensousakiGyoushaCode.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);

                //// マニ現場CD Validatedイベント生成
                // this.form.ManiHensousakiGenbaCode.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);

                #region  (A票～E票)についてイベント生成

                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                // マニ返送先使用可否 TextChangedイベント生成(A票～E票)
                this.form.MANIFEST_USE_AHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_B1Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_B2Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_B4Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_B6Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_C1Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_C2Hyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_DHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                this.form.MANIFEST_USE_EHyo.TextChanged += new System.EventHandler(this.form.MANI_HENSOUSAKI_USEKbn_TextChanged);
                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end

                // マニ返送先場所区分 TextChangedイベント生成(A票～E票)
                this.form.HENSOUSAKI_PLACE_KBN_AHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_DHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);
                this.form.HENSOUSAKI_PLACE_KBN_EHyo.TextChanged += new System.EventHandler(this.form.HENSOUSAKI_PLACE_KBN_TextChanged);

                // マニ返送先区分 TextChangedイベント生成(A票～E票)
                this.form.HensousakiKbn_AHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_B1Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_B2Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_B4Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_B6Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_C1Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_C2Hyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_DHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);
                this.form.HensousakiKbn_EHyo.TextChanged += new System.EventHandler(this.form.HensousakiKbn_TextChanged);

                // マニ取引先CD TextChangedイベント生成(A票～E票)
                this.form.ManiHensousakiTorihikisakiCode_AHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_DHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);
                this.form.ManiHensousakiTorihikisakiCode_EHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiTorihikisakiCode_TextChanged);

                // マニ業者CD TextChangedイベント生成(A票～E票)
                this.form.ManiHensousakiGyoushaCode_AHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_B1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_B2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_B4Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_B6Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_C1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_C2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_DHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);
                this.form.ManiHensousakiGyoushaCode_EHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGyoushaCode_TextChanged);

                // マニ現場CD TextChangedイベント生成(A票～E票)
                this.form.ManiHensousakiGenbaCode_AHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_B1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_B2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_B4Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_B6Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_C1Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_C2Hyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_DHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);
                this.form.ManiHensousakiGenbaCode_EHyo.TextChanged += new System.EventHandler(this.form.ManiHensousakiGenbaCode_TextChanged);

                // マニ取引先CD Validatedイベント生成(A票～E票)
                this.form.ManiHensousakiTorihikisakiCode_AHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_DHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);
                this.form.ManiHensousakiTorihikisakiCode_EHyo.Validating += new CancelEventHandler(this.form.ManiTorihikisakiCDValidating);

                // マニ業者CD Validatedイベント生成(A票～E票)
                this.form.ManiHensousakiGyoushaCode_AHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_DHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);
                this.form.ManiHensousakiGyoushaCode_EHyo.Validating += new CancelEventHandler(this.form.ManiGyoushaCDValidating);

                // マニ現場CD Validatedイベント生成(A票～E票)
                this.form.ManiHensousakiGenbaCode_AHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_B1Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_B2Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_B4Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_B6Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_C1Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_C2Hyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_DHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);
                this.form.ManiHensousakiGenbaCode_EHyo.Validating += new CancelEventHandler(this.form.ManiGenbaCDValidating);

                // A票～E票タブの取引先コピーボタン押下処理
                this.form.GENBA_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

                #endregion

                // 請求タブの取引先コピーボタン押下処理
                this.form.GENBA_COPY_SEIKYU_BUTTON.Click += new EventHandler(this.form.CopySeikyuButtonClick);

                // 支払タブの取引先コピーボタン押下処理
                this.form.GENBA_COPY_SIHARAI_BUTTON.Click += new EventHandler(this.form.CopySiharaiButtonClick);

                //// 分類タブの取引先コピーボタン押下処理
                // this.form.GENBA_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

                // 定期品名一覧セルエンター処理
                this.form.TeikiHinmeiIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.Ichiran_CellEnter);

                // 月極品名一覧セルエンター処理
                this.form.TsukiHinmeiIchiran.CellEnter += new EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.form.Ichiran_CellEnter);

                // 20141203 Houkakou 「引合現場入力」の日付チェックを追加する start
                this.form.TekiyouKikanForm.Leave += new System.EventHandler(TekiyouKikanForm_Leave);
                this.form.TekiyouKikanTo.Leave += new System.EventHandler(TekiyouKikanTo_Leave);
                // 20141203 Houkakou 「引合現場入力」の日付チェックを追加する end

                // 20141226 Houkakou 「引合現場入力」のダブルクリックを追加する start
                // 「To」のイベント生成
                this.form.TekiyouKikanTo.MouseDoubleClick += new MouseEventHandler(TekiyouKikanTo_MouseDoubleClick);

                // 20141226 Houkakou 「引合現場入力」のダブルクリックを追加する end

                // 20141209 katen #2927 実績報告書　フィードバック対応 start
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validating);
                // 20141209 katen #2927 実績報告書　フィードバック対応 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 動的イベント設定処理
        /// </summary>
        public void SetDynamicEvent()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 定期品名一覧の入力値チェック処理
                this.form.TeikiHinmeiIchiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TeikiHinmeiIchiran_CellValidating);

                // 定期品名一覧の行入力値チェック処理
                this.form.TeikiHinmeiIchiran.RowValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TeikiHinmeiIchiran_RowValidating);

                // 月極品名一覧の入力値チェック処理
                this.form.TsukiHinmeiIchiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TsukiHinmeiIchiran_CellValidating);

                // 定期品名一覧の行入力値チェック処理
                this.form.TsukiHinmeiIchiran.RowValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TsukiHinmeiIchiran_RowValidating);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDynamicEvent", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 動的イベント削除処理
        /// </summary>
        public void RemoveDynamicEvent()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 定期品名一覧の入力値チェック処理
                this.form.TeikiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TeikiHinmeiIchiran_CellValidating);

                // 定期品名一覧の行入力値チェック処理
                this.form.TeikiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TeikiHinmeiIchiran_RowValidating);

                // 月極品名一覧の入力値チェック処理
                this.form.TsukiHinmeiIchiran.CellValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.form.TsukiHinmeiIchiran_CellValidating);

                // 定期品名一覧の行入力値チェック処理
                this.form.TsukiHinmeiIchiran.RowValidating -= new System.EventHandler<GrapeCity.Win.MultiRow.CellCancelEventArgs>(this.form.TsukiHinmeiIchiran_RowValidating);
            }
            catch (Exception ex)
            {
                LogUtility.Error("RemoveDynamicEvent", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            ButtonSetting[] result = null;

            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                if (denshiShinseiFlg)
                {
                    result = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath2);
                }
                else
                {
                    result = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parentForm"></param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, parentForm);

                switch (windowType)
                {
                    // 【新規】モード
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.WindowInitNew(parentForm);
                        break;

                    // 【修正】モード
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.WindowInitUpdate(parentForm);
                        break;

                    // 【削除】モード
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.WindowInitDelete(parentForm);
                        break;

                    // 【参照】モード
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.WindowInitReference(parentForm);
                        break;

                    // デフォルトは【新規】モード
                    default:
                        this.WindowInitNew(parentForm);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ModeInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                if (string.IsNullOrEmpty(this.GenbaCd))
                {
                    this.GetSysInfo();

                    // 【新規】モードで初期化
                    WindowInitNewMode(parentForm);
                }
                else
                {
                    // 【複写】モードで初期化
                    WindowInitNewCopyMode(parentForm);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNew", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
                this.sysinfoEntity = sysInfo[0];
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            bool res = true;
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // タブを全表示する
                this._tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                this._tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                // 全コントロール操作可能とする
                this.AllControlLock(true);

                // 定期・月極品名の空テーブルを作成する
                this.TeikiHinmeiTable = this.daoGenbaTeiki.GetTeikiHinmeiStruct(new M_HIKIAI_GENBA_TEIKI_HINMEI());
                this.SetIchiranTeiki();
                this.TsukiHinmeiTable = this.daoGenbaTsuki.GetTsukiHinmeiStruct(new M_HIKIAI_GENBA_TSUKI_HINMEI());
                this.SetIchiranTsuki();

                // 定期回収タブの入力制限を解除する
                this.ChangeTeikiKaishuuControl(true);

                // 名称取得用処理
                this.genbaEntity = new M_HIKIAI_GENBA();
                this.genbaEntity.MANIFEST_SHURUI_CD = this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD;
                this.genbaEntity.MANIFEST_TEHAI_CD = this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD;
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
                this.form.GyoushaCode.Enabled = true; // 業者CD
                this.form.GyoushaCodeSearchButton.Enabled = true; // 業者CDボタン
                this.form.bt_genbacd_saiban.Enabled = true; // 採番ボタン

                // 初期値セット部
                this.form.HIKIAI_GYOUSHA_USE_FLG.Text = string.Empty;
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                this.form.TIME_STAMP.Text = string.Empty;

                this.form.SeikyuuDaihyouPrintKbn.Text = "2";
                // this.form.HensousakiKbn.Text = "1";
                this.form.GenbaKeishou1.SelectedIndex = -1;
                this.form.GenbaKeishou2.SelectedIndex = -1;
                this.form.SeikyuuSouhuKeishou1.SelectedIndex = -1;
                this.form.SeikyuuSouhuKeishou2.SelectedIndex = -1;
                this.form.ShiharaiSoufuKeishou1.SelectedIndex = -1;
                this.form.ShiharaiSoufuKeishou2.SelectedIndex = -1;
                // this.form.ManiHensousakiKeishou1.SelectedIndex = -1;
                // this.form.ManiHensousakiKeishou2.SelectedIndex = -1;

                // this.form.HensousakiKbn1.Checked = true;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;

                // this.form.ManiTorihikisakiCodeSearchButton.Enabled = true;
                // this.form.ManiGyoushaCodeSearchButton.Enabled = false;
                // this.form.ManiGenbaCodeSearchButton.Enabled = false;

                this.form.ManiHensousakiKeishou1.SelectedIndex = -1;
                this.form.ManiHensousakiKeishou2.SelectedIndex = -1;

                #region A票～E票初期化

                // A票
                if (this._tabPageManager.IsVisible(6))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // B1票
                if (this._tabPageManager.IsVisible(7))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // B2票
                if (this._tabPageManager.IsVisible(8))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // B4票
                if (this._tabPageManager.IsVisible(9))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // B6票
                if (this._tabPageManager.IsVisible(10))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // C1票
                if (this._tabPageManager.IsVisible(11))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // C2票
                if (this._tabPageManager.IsVisible(12))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // D票
                if (this._tabPageManager.IsVisible(13))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }

                // E票
                if (this._tabPageManager.IsVisible(14))
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
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
                }
                // A票
                if (this._tabPageManager.IsVisible(6))
                {
                    this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                }

                // B1票
                if (this._tabPageManager.IsVisible(7))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                }

                // B2票
                if (this._tabPageManager.IsVisible(8))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                }

                // B4票
                if (this._tabPageManager.IsVisible(9))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                }

                // B6票
                if (this._tabPageManager.IsVisible(10))
                {
                    this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                }

                // C1票
                if (this._tabPageManager.IsVisible(11))
                {
                    this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                }

                // C2票
                if (this._tabPageManager.IsVisible(12))
                {
                    this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                }

                // D票
                if (this._tabPageManager.IsVisible(13))
                {
                    this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                    this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                }

                // E票
                if (this._tabPageManager.IsVisible(14))
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
                this.form.GenbaKeishou1.Text = string.Empty;
                this.form.GenbaKeishou2.Text = string.Empty;
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
                this.form.TekiyouKikanForm.Value = parentForm.sysDate;
                this.form.TekiyouKikanTo.Value = null;
                this.form.ChuusiRiyuu1.Text = string.Empty;
                this.form.ChuusiRiyuu2.Text = string.Empty;
                this.form.ShokuchiKbn.Checked = false;
                //this.form.DenManiShoukaiKbn.Checked = false;
                this.form.KENSHU_YOUHI.Checked = false;

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

                // 20150609 #10698 「運転者指示事項」の項目を追加する。by hoanghm start
                this.form.UntenshaShijiJikou1.Text = string.Empty;
                this.form.UntenshaShijiJikou2.Text = string.Empty;
                this.form.UntenshaShijiJikou3.Text = string.Empty;
                // 20150609 #10698「運転者指示事項」の項目を追加する。by hoanghm end

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
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull && this.form.SEIKYUU_KYOTEN_CD.Equals(string.Empty))
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
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
                // 発行先チェック処理
                this.HakkousakuCheck();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

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

                // 現場分類
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

                #region 仕様変更A票～E票追加為に削除

                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;
                // this.form.ManiHensousakiName1.Text = string.Empty;
                // this.form.ManiHensousakiName2.Text = string.Empty;
                // this.form.ManiHensousakiKeishou1.Text = string.Empty;
                // this.form.ManiHensousakiKeishou2.Text = string.Empty;
                // this.form.ManiHensousakiPost.Text = string.Empty;
                // this.form.ManiHensousakiAddress1.Text = string.Empty;
                // this.form.ManiHensousakiAddress2.Text = string.Empty;
                // this.form.ManiHensousakiBusho.Text = string.Empty;
                // this.form.ManiHensousakiTantou.Text = string.Empty;

                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;

                #endregion

                // 定期品名情報
                this.form.SHIKUCHOUSON_CD.Text = string.Empty;
                this.form.SHIKUCHOUSON_NAME_RYAKU.Text = string.Empty;
                this.SetIchiranTeiki();

                // 月極品名情報
                this.SetIchiranTsuki();

                // functionボタン
                this.SetF1Enabled(this.form.WindowType);// 移行
                parentForm.bt_func3.Enabled = false; // 修正
                parentForm.bt_func9.Enabled = true; // 登録
                parentForm.bt_func11.Enabled = true; // 取消

                // 業者分類タブ初期化
                this.ManiCheckOffCheck(false);

                this.genbaEntity = null;

                this.form.GyoushaCode.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitNewMode", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                res = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                res = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
            return res;
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // 検索結果を画面に設定
                this.SetWindowData();

                // functionボタン
                parentForm.bt_func3.Enabled = false; // 修正
                parentForm.bt_func9.Enabled = true; // 登録
                parentForm.bt_func11.Enabled = true; // 取消

                // 複写モード時は現場コードのコピーはなし
                this.form.GenbaCode.Text = string.Empty;

                // 適用開始日(当日日付)
                this.form.TekiyouKikanForm.Value = parentForm.sysDate;
                // 適用終了日
                this.form.TekiyouKikanTo.Value = null;
                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 発行先コード
                this.form.HAKKOUSAKI_CD.Text = string.Empty;

                // 業者分類タブ初期化
                this.ManiCheckOffCheck(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewCopyMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.GyoushaCode.Enabled = false; // 業者CD
                this.form.GyoushaCodeSearchButton.Enabled = false; // 業者CDボタン
                this.form.GenbaCode.Enabled = false; // 現場CD
                this.form.bt_genbacd_saiban.Enabled = false; // 採番ボタン

                // functionボタン
                if (this.denshiShinseiFlg)
                {
                    parentForm.bt_func9.Enabled = true; // 申請
                    parentForm.bt_func12.Enabled = true; // 閉じる
                }
                else
                {
                    this.SetF1Enabled(this.form.WindowType);// 移行
                    parentForm.bt_func3.Enabled = false; // 修正
                    parentForm.bt_func9.Enabled = true; // 登録
                    parentForm.bt_func11.Enabled = true; // 取消
                }

                // 業者分類タブ初期化
                this.ManiCheckOffCheck(false);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitUpdate", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 削除モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func3.Enabled = true; // 修正
                parentForm.bt_func9.Enabled = true; // 登録
                parentForm.bt_func11.Enabled = false; // 取消
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 参照モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func2.Enabled = true; // 新規
                parentForm.bt_func3.Enabled = true; // 修正
                parentForm.bt_func7.Enabled = true; // 一覧
                parentForm.bt_func9.Enabled = false; // 登録
                parentForm.bt_func11.Enabled = false; // 取消
                parentForm.bt_func12.Enabled = true; // 閉じる
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitReference", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 画面設定中フラグ設定
                this.isSettingWindowData = true;

                // 各種データ取得
                if (this.SearchGenba() == 0)
                {
                    return;
                }

                this.SearchBusho();
                this.SearchChiiki();
                this.SearchGyousha();
                this.SearchHikiaiGyousha();
                this.SearchGyoushu();
                this.SearchTorihikisaki();
                this.SearchHikiaiTorihikisaki();
                this.SearchKyoten();
                this.SearchShain();
                this.SearchShuukeiItem();
                this.SearchTodoufuken();
                this.GetSysInfo();
                this.SearchManiShurui();
                this.SearchManiTehai();
                this.SearchTeiki();
                this.SearchTsuki();

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = this.genbaEntity.CREATE_DATE.ToString();
                header.CreateUser.Text = this.genbaEntity.CREATE_USER;
                header.LastUpdateDate.Text = this.genbaEntity.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.genbaEntity.UPDATE_USER;

                // 共通部
                this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.genbaEntity.TIME_STAMP);

                var hikiaiGyoushaUseFlg = false;
                this.form.HIKIAI_GYOUSHA_USE_FLG.Text = string.Empty;
                if (!this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsNull)
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG ? "1" : "0";
                    hikiaiGyoushaUseFlg = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.Value;
                }
                var hikiaiTorihikisakiUseFlg = false;
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                if (!this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                {
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG ? "1" : "0";
                    hikiaiTorihikisakiUseFlg = this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.Value;
                }

                if (this.gyoushaEntity != null && !hikiaiGyoushaUseFlg)
                {
                    this.form.GyoushaCode.Text = this.genbaEntity.GYOUSHA_CD;
                    this.form.GyoushaName1.Text = this.gyoushaEntity.GYOUSHA_NAME1;
                    this.form.GyoushaName2.Text = this.gyoushaEntity.GYOUSHA_NAME2;

                    this.form.GyoushaKbnUkeire.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_UKEIRE;
                    this.form.GyoushaKbnShukka.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_SHUKKA;
                    this.form.GyoushaKbnMani.Checked = (bool)this.gyoushaEntity.GYOUSHAKBN_MANI;

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
                }
                if (this.hikiaiGyoushaEntity != null && hikiaiGyoushaUseFlg)
                {
                    this.IsSetDataFromPopup = true;
                    this.form.GyoushaCode.Text = this.genbaEntity.GYOUSHA_CD;
                    this.form.GyoushaName1.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
                    this.form.GyoushaName2.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;

                    this.form.GyoushaKbnUkeire.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE;
                    this.form.GyoushaKbnShukka.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA;
                    this.form.GyoushaKbnMani.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI;

                    if (this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.GYOUSHA_TEKIYOU_BEGIN.Value = this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.Value;
                    }

                    if (this.hikiaiGyoushaEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.GYOUSHA_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.GYOUSHA_TEKIYOU_END.Value = this.hikiaiGyoushaEntity.TEKIYOU_END.Value;
                    }
                }

                if (this.torihikisakiEntity != null && !hikiaiTorihikisakiUseFlg)
                {
                    this.form.TorihikisakiCode.Text = this.genbaEntity.TORIHIKISAKI_CD;
                    this.form.TorihikisakiName1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                    this.form.TorihikisakiName2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                    if (!this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                    }

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
                if (this.hikiaiTorihikisakiEntity != null && hikiaiTorihikisakiUseFlg)
                {
                    this.IsSetDataFromPopup = true;
                    this.form.TorihikisakiCode.Text = this.genbaEntity.TORIHIKISAKI_CD;
                    this.form.TorihikisakiName1.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME1;
                    this.form.TorihikisakiName2.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME2;
                    if (!this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                    }

                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN.Value;
                    }

                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_END.Value;
                    }
                    if (!this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                    }
                }
                if (this.kyotenEntity != null)
                {
                    this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                }
                this.form.GenbaCode.Text = this.genbaEntity.GENBA_CD;
                this.form.GenbaFurigana.Text = this.genbaEntity.GENBA_FURIGANA;
                this.form.GenbaName1.Text = this.genbaEntity.GENBA_NAME1;
                this.form.GenbaName2.Text = this.genbaEntity.GENBA_NAME2;
                this.form.GenbaKeishou1.Text = this.genbaEntity.GENBA_KEISHOU1;
                this.form.GenbaKeishou2.Text = this.genbaEntity.GENBA_KEISHOU2;
                this.form.GenbaNameRyaku.Text = this.genbaEntity.GENBA_NAME_RYAKU;
                this.form.GenbaTel.Text = this.genbaEntity.GENBA_TEL;
                this.form.GenbaKeitaiTel.Text = this.genbaEntity.GENBA_KEITAI_TEL;
                this.form.GenbaFax.Text = this.genbaEntity.GENBA_FAX;

                if (this.bushoEntity != null)
                {
                    this.form.EigyouTantouBushoCode.Text = this.genbaEntity.EIGYOU_TANTOU_BUSHO_CD;
                    this.form.EigyouTantouBushoName.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
                }
                if (this.shainEntity != null)
                {
                    this.form.EigyouCode.Text = this.genbaEntity.EIGYOU_TANTOU_CD;
                    this.form.EigyouName.Text = this.shainEntity.SHAIN_NAME_RYAKU;
                }

                if (this.genbaEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TekiyouKikanForm.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanForm.Value = this.genbaEntity.TEKIYOU_BEGIN.Value;
                }

                if (this.genbaEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TekiyouKikanTo.Value = null;
                }
                else
                {
                    this.form.TekiyouKikanTo.Value = this.genbaEntity.TEKIYOU_END.Value;
                }

                this.form.ChuusiRiyuu1.Text = this.genbaEntity.CHUUSHI_RIYUU1;
                this.form.ChuusiRiyuu2.Text = this.genbaEntity.CHUUSHI_RIYUU2;

                if (!this.genbaEntity.SHOKUCHI_KBN.IsNull)
                {
                    this.form.ShokuchiKbn.Checked = (bool)this.genbaEntity.SHOKUCHI_KBN;
                }
                //if (!this.genbaEntity.DEN_MANI_SHOUKAI_KBN.IsNull)
                //{
                //    this.form.DenManiShoukaiKbn.Checked = (bool)this.genbaEntity.DEN_MANI_SHOUKAI_KBN;
                //}
                if (!this.genbaEntity.KENSHU_YOUHI.IsNull)
                {
                    this.form.KENSHU_YOUHI.Checked = (bool)this.genbaEntity.KENSHU_YOUHI;
                }

                // 基本情報
                this.form.GenbaPost.Text = this.genbaEntity.GENBA_POST;
                if (!this.genbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    if (this.todoufukenEntity != null)
                    {
                        this.form.GenbaTodoufukenCode.Text = this.genbaEntity.GENBA_TODOUFUKEN_CD.ToString();
                        this.form.GenbaTodoufukenNameRyaku.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                    }
                }
                this.form.GenbaAddress1.Text = this.genbaEntity.GENBA_ADDRESS1;
                this.form.GenbaAddress2.Text = this.genbaEntity.GENBA_ADDRESS2;

                if (this.chiikiEntity != null)
                {
                    this.form.ChiikiCode.Text = this.genbaEntity.CHIIKI_CD;
                    this.form.ChiikiName.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                }

                this.SearchUpnHoukokushoTeishutsuChiiki();
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.genbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = (this.upnHoukokushoTeishutsuChiikiEntity == null) ? string.Empty : this.upnHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;
                
                this.form.BushoCode.Text = this.genbaEntity.BUSHO;
                this.form.TantoushaCode.Text = this.genbaEntity.TANTOUSHA;
                this.form.KoufutantoushaCode.Text = this.genbaEntity.KOUFU_TANTOUSHA;

                if (this.shuukeiEntity != null)
                {
                    this.form.ShuukeiItemCode.Text = this.genbaEntity.SHUUKEI_ITEM_CD;
                    this.form.ShuukeiItemName.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                }

                if (this.gyoushuEntity != null)
                {
                    this.form.GyoushuCode.Text = this.genbaEntity.GYOUSHU_CD;
                    this.form.GyoushuName.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
                }

                this.form.Bikou1.Text = this.genbaEntity.BIKOU1;
                this.form.Bikou2.Text = this.genbaEntity.BIKOU2;
                this.form.Bikou3.Text = this.genbaEntity.BIKOU3;
                this.form.Bikou4.Text = this.genbaEntity.BIKOU4;

                // 20150609 #10698「運転者指示事項」の項目を追加する。by hoanghm start
                this.form.UntenshaShijiJikou1.Text = this.genbaEntity.UNTENSHA_SHIJI_JIKOU1;
                this.form.UntenshaShijiJikou2.Text = this.genbaEntity.UNTENSHA_SHIJI_JIKOU2;
                this.form.UntenshaShijiJikou3.Text = this.genbaEntity.UNTENSHA_SHIJI_JIKOU3;
                // 20150609 #10698「運転者指示事項」の項目を追加する。by hoanghm end

                // 請求情報
                if (this._tabPageManager.IsVisible(1))
                {
                    this.form.SeikyuushoSoufusaki1.Text = this.genbaEntity.SEIKYUU_SOUFU_NAME1;
                    this.form.SeikyuushoSoufusaki2.Text = this.genbaEntity.SEIKYUU_SOUFU_NAME2;
                    this.form.SeikyuuSouhuKeishou1.Text = this.genbaEntity.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SeikyuuSouhuKeishou2.Text = this.genbaEntity.SEIKYUU_SOUFU_KEISHOU2;

                    this.form.SeikyuuSoufuPost.Text = this.genbaEntity.SEIKYUU_SOUFU_POST;
                    this.form.SeikyuuSoufuAddress1.Text = this.genbaEntity.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SeikyuuSoufuAddress2.Text = this.genbaEntity.SEIKYUU_SOUFU_ADDRESS2;
                    this.form.SeikyuuSoufuBusho.Text = this.genbaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SeikyuuSoufuTantou.Text = this.genbaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SoufuGenbaTel.Text = this.genbaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SoufuGenbaFax.Text = this.genbaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SeikyuuTantou.Text = this.genbaEntity.SEIKYUU_TANTOU;

                    this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
                    if (!this.genbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SeikyuuDaihyouPrintKbn.Text = this.genbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.genbaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.genbaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.genbaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.genbaEntity.HAKKOUSAKI_CD;
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                }

                // 支払情報
                if (this._tabPageManager.IsVisible(2))
                {
                    this.form.ShiharaiSoufuName1.Text = this.genbaEntity.SHIHARAI_SOUFU_NAME1;
                    this.form.ShiharaiSoufuName2.Text = this.genbaEntity.SHIHARAI_SOUFU_NAME2;
                    this.form.ShiharaiSoufuKeishou1.Text = this.genbaEntity.SHIHARAI_SOUFU_KEISHOU1;
                    this.form.ShiharaiSoufuKeishou2.Text = this.genbaEntity.SHIHARAI_SOUFU_KEISHOU2;

                    this.form.ShiharaiSoufuPost.Text = this.genbaEntity.SHIHARAI_SOUFU_POST;
                    this.form.ShiharaiSoufuAddress1.Text = this.genbaEntity.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.ShiharaiSoufuAddress2.Text = this.genbaEntity.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.ShiharaiSoufuBusho.Text = this.genbaEntity.SHIHARAI_SOUFU_BUSHO;
                    this.form.ShiharaiSoufuTantou.Text = this.genbaEntity.SHIHARAI_SOUFU_TANTOU;
                    this.form.ShiharaiGenbaTel.Text = this.genbaEntity.SHIHARAI_SOUFU_TEL;
                    this.form.ShiharaiGenbaFax.Text = this.genbaEntity.SHIHARAI_SOUFU_FAX;

                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.genbaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.genbaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.genbaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                }

                // 現場分類
                if (!this.genbaEntity.JISHA_KBN.IsNull)
                {
                    this.form.JishaKbn.Checked = (bool)this.genbaEntity.JISHA_KBN;
                }
                else
                {
                    this.form.JishaKbn.Checked = false;
                }
                if (!this.genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsNull)
                {
                    this.form.HaishutsuKbn.Checked = (bool)this.genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
                }
                else
                {
                    this.form.HaishutsuKbn.Checked = false;
                }
                if (!this.genbaEntity.TSUMIKAEHOKAN_KBN.IsNull)
                {
                    this.form.TsumikaeHokanKbn.Checked = (bool)this.genbaEntity.TSUMIKAEHOKAN_KBN;
                }
                else
                {
                    this.form.TsumikaeHokanKbn.Checked = false;
                }
                if (!this.genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsNull)
                {
                    this.form.ShobunJigyoujouKbn.Checked = (bool)this.genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN;
                }
                else
                {
                    this.form.ShobunJigyoujouKbn.Checked = false;
                }
                if (!this.genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsNull)
                {
                    this.form.SaishuuShobunjouKbn.Checked = (bool)this.genbaEntity.SAISHUU_SHOBUNJOU_KBN;
                }
                else
                {
                    this.form.SaishuuShobunjouKbn.Checked = false;
                }
                if (!this.genbaEntity.MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.ManiHensousakiKbn.Checked = (bool)this.genbaEntity.MANI_HENSOUSAKI_KBN;
                }
                else
                {
                    this.form.ManiHensousakiKbn.Checked = false;
                }

                if (this.manishuruiEntity != null)
                {
                    this.form.ManifestShuruiCode.Text = this.genbaEntity.MANIFEST_SHURUI_CD.ToString();
                    this.form.ManifestShuruiName.Text = this.manishuruiEntity.MANIFEST_SHURUI_NAME;
                }
                else
                {
                    this.form.ManifestShuruiCode.Text = "";
                    this.form.ManifestShuruiName.Text = "";
                }
                if (this.manitehaiEntity != null)
                {
                    this.form.ManifestTehaiCode.Text = this.genbaEntity.MANIFEST_TEHAI_CD.ToString();
                    this.form.ManifestTehaiName.Text = this.manitehaiEntity.MANIFEST_TEHAI_NAME;
                }
                else
                {
                    this.form.ManifestTehaiCode.Text = "";
                    this.form.ManifestTehaiName.Text = "";
                }

                this.form.ShobunsakiCode.Text = this.genbaEntity.SHOBUNSAKI_NO;

                if (this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
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
                    this.form.ManiHensousakiName1.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.ManiHensousakiName2.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.ManiHensousakiKeishou1.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.ManiHensousakiKeishou2.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.ManiHensousakiPost.Text = this.genbaEntity.MANI_HENSOUSAKI_POST;
                    this.form.ManiHensousakiAddress1.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.ManiHensousakiAddress2.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.ManiHensousakiBusho.Text = this.genbaEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.ManiHensousakiTantou.Text = this.genbaEntity.MANI_HENSOUSAKI_TANTOU;
                }

                #region  A票～E票画面に設定

                this.SetAToEWindowsData();
                // if (this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD != "")
                // {
                // this.form.HensousakiKbn.Text = "1";
                // this.form.ManiHensousakiTorihikisakiCode.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }
                // else if (this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD != "")   // No3521
                // {
                // this.form.HensousakiKbn.Text = "3";
                // this.form.ManiHensousakiGenbaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD;
                // // No3521-->
                // // this.form.ManiHensousakiGyoushaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD;
                // // No3521<--
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false; // No3521
                // this.form.ManiHensousakiGenbaCode.Enabled = true;
                // }
                // else if (this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD != "")
                // {
                // this.form.HensousakiKbn.Text = "2";
                // this.form.ManiHensousakiGyoushaCode.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // this.form.ManiHensousakiGyoushaCode.Enabled = true;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }
                // else if (!this.form.ManiHensousakiKbn.Checked)
                // {
                // if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                // {
                // this.form.HensousakiKbn.Text = "1";
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }
                // else
                // {
                // this.form.HensousakiKbn.Text = string.Empty;
                // this.form.HensousakiKbn1.Checked = false;
                // this.form.HensousakiKbn2.Checked = false;
                // this.form.HensousakiKbn3.Checked = false;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }
                // }
                // else
                // {
                // this.form.HensousakiKbn.Text = string.Empty;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }

                // this.form.ManiHensousakiName1.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME1;
                // this.form.ManiHensousakiName2.Text = this.genbaEntity.MANI_HENSOUSAKI_NAME2;
                // this.form.ManiHensousakiKeishou1.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1;
                // this.form.ManiHensousakiKeishou2.Text = this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2;

                // this.form.ManiHensousakiPost.Text = this.genbaEntity.MANI_HENSOUSAKI_POST;
                // this.form.ManiHensousakiAddress1.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1;
                // this.form.ManiHensousakiAddress2.Text = this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2;
                // this.form.ManiHensousakiBusho.Text = this.genbaEntity.MANI_HENSOUSAKI_BUSHO;
                // this.form.ManiHensousakiTantou.Text = this.genbaEntity.MANI_HENSOUSAKI_TANTOU;

                #endregion

                // 定期契約情報
                if (this.genbaEntity.SHIKUCHOUSON_CD != null)
                {
                    this.form.SHIKUCHOUSON_CD.Text = this.genbaEntity.SHIKUCHOUSON_CD.ToString();
                    if (!string.IsNullOrWhiteSpace(this.genbaEntity.SHIKUCHOUSON_CD.ToString()))
                    {
                        M_SHIKUCHOUSON shiku = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>().GetDataByCd(this.genbaEntity.SHIKUCHOUSON_CD.ToString());
                        if (shiku != null)
                        {
                            this.form.SHIKUCHOUSON_NAME_RYAKU.Text = shiku.SHIKUCHOUSON_NAME_RYAKU;
                        }
                    }
                }

                this.SetIchiranTeiki();

                // 月極契約情報
                this.SetIchiranTsuki();

                // 業者データでの取引先チェック
                this.SearchchkGyousha(false, true);

                // 画面設定中フラグ解除
                this.isSettingWindowData = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 返送先区分調整メソッド

        /// <summary>
        /// 返送先区分調整メソッド
        /// 返送先区分の活性制御がおかしいときに調整用として呼び出してください。
        /// </summary>
        /// <param name="windowType"></param>
        internal void SettingHensouSakiKbn()
        {
            var maniHensousaki = this.form.ManiHensousakiKbn.Checked;

            #region A票

            var aHyouUseKbn = this.form.MANIFEST_USE_AHyo.Text;
            // 返送先(票)判定
            var ahyoName = this.ChkTabEvent(this.form.MANIFEST_USE_AHyo.Name);

            if ("1" == aHyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(ahyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(ahyoName);
            }

            #endregion

            #region B1票

            var b1HyouUseKbn = this.form.MANIFEST_USE_B1Hyo.Text;
            // 返送先(票)判定
            var b1hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B1Hyo.Name);

            if ("1" == b1HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(b1hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(b1hyoName);
            }

            #endregion

            #region B2票

            var b2HyouUseKbn = this.form.MANIFEST_USE_B2Hyo.Text;
            // 返送先(票)判定
            var b2hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B2Hyo.Name);

            if ("1" == b2HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(b2hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(b2hyoName);
            }

            #endregion

            #region B4票

            var b4HyouUseKbn = this.form.MANIFEST_USE_B4Hyo.Text;
            // 返送先(票)判定
            var b4hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B4Hyo.Name);

            if ("1" == b4HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(b4hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(b4hyoName);
            }

            #endregion

            #region B6票

            var b6HyouUseKbn = this.form.MANIFEST_USE_B6Hyo.Text;
            // 返送先(票)判定
            var b6hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_B6Hyo.Name);

            if ("1" == b6HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(b6hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(b6hyoName);
            }

            #endregion

            #region C1票

            var c1HyouUseKbn = this.form.MANIFEST_USE_C1Hyo.Text;
            // 返送先(票)判定
            var c1hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_C1Hyo.Name);

            if ("1" == c1HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(c1hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(c1hyoName);
            }

            #endregion

            #region C2票

            var c2HyouUseKbn = this.form.MANIFEST_USE_C2Hyo.Text;
            // 返送先(票)判定
            var c2hyoName = this.ChkTabEvent(this.form.MANIFEST_USE_C2Hyo.Name);

            if ("1" == c2HyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(c2hyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(c2hyoName);
            }

            #endregion

            #region D票

            var dHyouUseKbn = this.form.MANIFEST_USE_DHyo.Text;
            // 返送先(票)判定
            var dhyoName = this.ChkTabEvent(this.form.MANIFEST_USE_DHyo.Name);

            if ("1" == dHyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(dhyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(dhyoName);
            }

            #endregion

            #region E票

            var eHyouUseKbn = this.form.MANIFEST_USE_EHyo.Text;
            // 返送先(票)判定
            var ehyoName = this.ChkTabEvent(this.form.MANIFEST_USE_EHyo.Name);

            if ("1" == eHyouUseKbn)
            {
                // マニフェスト返送先のチェック状態に応じて状態変更
                this.SetEnabledManifestHensousakiRendou(ehyoName, maniHensousaki);
            }
            else
            {
                // 全て使用不可
                this.SetEnabledFalseManifestHensousaki(ehyoName);
            }

            #endregion
        }

        #endregion

        #region 全コントロール制御メソッド

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                // 初期値セット部
                this.form.SeikyuuDaihyouPrintKbn.Enabled = isBool;
                // this.form.HensousakiKbn.Enabled = isBool;

                // 共通部
                this.form.GyoushaCode.Enabled = isBool;
                // this.form.GyoushaKbnUkeire.Enabled = isBool;
                // this.form.GyoushaKbnShukka.Enabled = isBool;
                // this.form.GyoushaKbnMani.Enabled = isBool;
                // this.form.GyoushaName1.Enabled = isBool;
                // this.form.GyoushaName2.Enabled = isBool;
                this.form.TorihikisakiCode.Enabled = isBool;
                this.form.bt_gyousya_copy.Enabled = isBool;
                this.form.GyoushaNew.Enabled = isBool;
                this.form.BT_GYOUSHA_REFERENCE.Enabled = isBool;
                // this.form.TorihikisakiName1.Enabled = isBool;
                // this.form.TorihikisakiName2.Enabled = isBool;
                // this.form.KyotenCode.Enabled = isBool;
                // this.form.KyotenName.Enabled = isBool;
                this.form.GenbaCode.Enabled = isBool;
                this.form.GenbaFurigana.Enabled = isBool;
                this.form.GenbaName1.Enabled = isBool;
                this.form.GenbaName2.Enabled = isBool;
                // this.form.ManiHensousakiKbnHidden.Enabled = isBool;
                this.form.GenbaNameRyaku.Enabled = isBool;
                this.form.GenbaTel.Enabled = isBool;
                this.form.GenbaKeitaiTel.Enabled = isBool;
                this.form.GenbaFax.Enabled = isBool;
                this.form.EigyouTantouBushoCode.Enabled = isBool;
                // this.form.EigyouTantouBushoName.Enabled = isBool;
                this.form.EigyouCode.Enabled = isBool;
                // this.form.EigyouName.Enabled = isBool;
                this.form.EigyouTantouBushoCode.Enabled = isBool;
                this.form.TekiyouKikanForm.Enabled = isBool;
                this.form.TekiyouKikanTo.Enabled = isBool;
                this.form.ChuusiRiyuu1.Enabled = isBool;
                this.form.ChuusiRiyuu2.Enabled = isBool;
                this.form.ShokuchiKbn.Enabled = isBool;
                //this.form.DenManiShoukaiKbn.Enabled = isBool;
                this.form.KENSHU_YOUHI.Enabled = isBool;

                // 基本情報
                this.form.GenbaPost.Enabled = isBool;
                this.form.GenbaTodoufukenCode.Enabled = isBool;
                // this.form.GenbaTodoufukenNameRyaku.Enabled = isBool;
                this.form.GenbaAddress1.Enabled = isBool;
                this.form.GenbaAddress2.Enabled = isBool;
                this.form.ChiikiCode.Enabled = isBool;
                // this.form.ChiikiName.Enabled = isBool;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Enabled = isBool;
                this.form.cbtn_Unpanhoukoku.Enabled = isBool;

                this.form.BushoCode.Enabled = isBool;
                this.form.TantoushaCode.Enabled = isBool;
                this.form.KoufutantoushaCode.Enabled = isBool;
                this.form.ShuukeiItemCode.Enabled = isBool;
                // this.form.ShuukeiItemName.Enabled = isBool;
                this.form.GyoushuCode.Enabled = isBool;
                // this.form.GyoushuName.Enabled = isBool;
                this.form.Bikou1.Enabled = isBool;
                this.form.Bikou2.Enabled = isBool;
                this.form.Bikou3.Enabled = isBool;
                this.form.Bikou4.Enabled = isBool;
                // 20150609 #10698「運転者指示事項」の項目を追加する。by hoanghm start
                this.form.UntenshaShijiJikou1.Enabled = isBool;
                this.form.UntenshaShijiJikou2.Enabled = isBool;
                this.form.UntenshaShijiJikou3.Enabled = isBool;
                // 20150609 #10698「運転者指示事項」の項目を追加する。by hoanghm end

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
                // 現場分類
                this.form.ManifestShuruiCode.Enabled = isBool;
                // this.form.ManifestShuruiName.Enabled = isBool;
                this.form.ManifestTehaiCode.Enabled = isBool;
                // this.form.ManifestTehaiName.Enabled = isBool;
                this.form.ShobunsakiCode.Enabled = isBool;
                this.form.ManiHensousakiName1.Enabled = isBool;
                this.form.ManiHensousakiName2.Enabled = isBool;
                this.form.ManiHensousakiPost.Enabled = isBool;
                this.form.ManiHensousakiAddress1.Enabled = isBool;
                this.form.ManiHensousakiAddress2.Enabled = isBool;
                this.form.ManiHensousakiBusho.Enabled = isBool;
                this.form.ManiHensousakiTantou.Enabled = isBool;
                this.form.ManiHensousakiKeishou1.Enabled = isBool;
                this.form.ManiHensousakiKeishou2.Enabled = isBool;
                this.form.ManiHensousakiPostSearchButton.Enabled = isBool;
                this.form.ManiHensousakiAddressSearchButton.Enabled = isBool;
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

                // this.form.ManiHensousakiTorihikisakiCode.Enabled = isBool;
                // this.form.ManiHensousakiGyoushaCode.Enabled = isBool;
                // this.form.ManiHensousakiGenbaCode.Enabled = isBool;
                // this.form.ManiHensousakiName1.Enabled = isBool;
                // this.form.ManiHensousakiName2.Enabled = isBool;
                // this.form.ManiHensousakiPost.Enabled = isBool;
                // this.form.ManiHensousakiAddress1.Enabled = isBool;
                // this.form.ManiHensousakiAddress2.Enabled = isBool;
                // this.form.ManiHensousakiBusho.Enabled = isBool;
                // this.form.ManiHensousakiTantou.Enabled = isBool;
                // this.form.GENBA_COPY_MANI_BUTTON.Enabled = isBool;

                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
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
                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end

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
                // this.form.ItakuKeiyakuIchiran.Enabled = isBool;

                // 定期情報
                this.form.SHIKUCHOUSON_CD.Enabled = isBool;
                this.form.SHIKUCHOUSON_CD_SEARCH.Enabled = isBool;
                this.form.TeikiHinmeiIchiran.ReadOnly = !isBool;

                // 月極情報
                this.form.TsukiHinmeiIchiran.ReadOnly = !isBool;

                // 追加分
                this.form.GyoushaCodeSearchButton.Enabled = isBool;
                this.form.TorihikisakiCodeSearchButton.Enabled = isBool;
                this.form.bt_torihikisaki_copy.Enabled = isBool;
                this.form.TorihikisakiNew.Enabled = isBool;
                this.form.BT_TORIHIKISAKI_REFERENCE.Enabled = isBool;
                this.form.bt_genbacd_saiban.Enabled = isBool;
                this.form.GenbaKeishou1.Enabled = isBool;
                this.form.GenbaKeishou2.Enabled = isBool;
                this.form.EigyouTantouBushoSearchButton.Enabled = isBool;
                this.form.bt_tantousha_search.Enabled = isBool;

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

                #region 仕様変更A票～E票追加為に削除

                // this.form.HensousakiKbn1.Enabled = isBool;
                // this.form.HensousakiKbn2.Enabled = isBool;
                // this.form.HensousakiKbn3.Enabled = isBool;

                // this.form.ManiTorihikisakiCodeSearchButton.Enabled = isBool;
                // this.form.ManiGyoushaCodeSearchButton.Enabled = isBool;
                // this.form.ManiGenbaCodeSearchButton.Enabled = isBool;
                // this.form.HensousakiDelete.Enabled = isBool;

                // this.form.ManiHensousakiKeishou1.Enabled = isBool;
                // this.form.ManiHensousakiKeishou2.Enabled = isBool;
                // this.form.ManiHensousakiPostSearchButton.Enabled = isBool;
                // this.form.ManiHensousakiAddressSearchButton.Enabled = isBool;

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("AllControlLock", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 全コントロール制御メソッド

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateEntity(bool isDelete)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                DateTime timeBegin = new DateTime();
                DateTime timeEnd = new DateTime();

                this.genbaEntity = new M_HIKIAI_GENBA();

                this.genbaEntity.SetValue(this.form.HIKIAI_GYOUSHA_USE_FLG);
                this.genbaEntity.SetValue(this.form.HIKIAI_TORIHIKISAKI_USE_FLG);

                this.genbaEntity.BIKOU1 = this.form.Bikou1.Text;
                this.genbaEntity.BIKOU2 = this.form.Bikou2.Text;
                this.genbaEntity.BIKOU3 = this.form.Bikou3.Text;
                this.genbaEntity.BIKOU4 = this.form.Bikou4.Text;
                // 20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm start
                this.genbaEntity.UNTENSHA_SHIJI_JIKOU1 = this.form.UntenshaShijiJikou1.Text;
                this.genbaEntity.UNTENSHA_SHIJI_JIKOU2 = this.form.UntenshaShijiJikou2.Text;
                this.genbaEntity.UNTENSHA_SHIJI_JIKOU3 = this.form.UntenshaShijiJikou3.Text;
                // 20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end
                this.genbaEntity.BUSHO = this.form.BushoCode.Text;
                this.genbaEntity.CHIIKI_CD = this.form.ChiikiCode.Text;
                this.genbaEntity.CHUUSHI_RIYUU1 = this.form.ChuusiRiyuu1.Text;
                this.genbaEntity.CHUUSHI_RIYUU2 = this.form.ChuusiRiyuu2.Text;
                //this.genbaEntity.DEN_MANI_SHOUKAI_KBN = this.form.DenManiShoukaiKbn.Checked;
                this.genbaEntity.KENSHU_YOUHI = this.form.KENSHU_YOUHI.Checked;
                this.genbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                this.genbaEntity.EIGYOU_TANTOU_CD = this.form.EigyouCode.Text;
                this.genbaEntity.GENBA_ADDRESS1 = this.form.GenbaAddress1.Text;
                this.genbaEntity.GENBA_ADDRESS2 = this.form.GenbaAddress2.Text;
                this.genbaEntity.GENBA_CD = this.form.GenbaCode.Text;
                this.genbaEntity.GENBA_FAX = this.form.GenbaFax.Text;
                this.genbaEntity.GENBA_FURIGANA = this.form.GenbaFurigana.Text;
                this.genbaEntity.GENBA_KEISHOU1 = this.form.GenbaKeishou1.Text;
                this.genbaEntity.GENBA_KEISHOU2 = this.form.GenbaKeishou2.Text;
                this.genbaEntity.GENBA_KEITAI_TEL = this.form.GenbaKeitaiTel.Text;
                this.genbaEntity.GENBA_NAME_RYAKU = this.form.GenbaNameRyaku.Text;
                this.genbaEntity.GENBA_NAME1 = this.form.GenbaName1.Text;
                this.genbaEntity.GENBA_NAME2 = this.form.GenbaName2.Text;
                this.genbaEntity.GENBA_POST = this.form.GenbaPost.Text;
                this.genbaEntity.GENBA_TEL = this.form.GenbaTel.Text;

                if (!string.IsNullOrWhiteSpace(this.form.GenbaTodoufukenCode.Text))
                {
                    this.genbaEntity.GENBA_TODOUFUKEN_CD = Int16.Parse(this.form.GenbaTodoufukenCode.Text.ToString());
                }

                this.genbaEntity.GYOUSHA_CD = this.form.GyoushaCode.Text;
                this.genbaEntity.GYOUSHU_CD = this.form.GyoushuCode.Text;
                this.genbaEntity.JISHA_KBN = this.form.JishaKbn.Checked;
                this.genbaEntity.KOUFU_TANTOUSHA = this.form.KoufutantoushaCode.Text;
                this.genbaEntity.SHIKUCHOUSON_CD = this.form.SHIKUCHOUSON_CD.Text;

                this.form.KyotenCode.Text = "99"; // 強制的に99:全社を登録
                if (!string.IsNullOrWhiteSpace(this.form.KyotenCode.Text))
                {
                    this.genbaEntity.KYOTEN_CD = Int16.Parse(this.form.KyotenCode.Text);
                }

                this.genbaEntity.ITAKU_KEIYAKU_USE_KBN = 0;

                // this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.ManiHensousakiAddress1.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.ManiHensousakiAddress2.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.ManiHensousakiBusho.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD = this.form.ManiHensousakiGenbaCode.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD = this.form.ManiHensousakiGyoushaCode.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_KBN = this.form.ManiHensousakiKbn.Checked;
                // this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.ManiHensousakiKeishou1.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.ManiHensousakiKeishou2.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.ManiHensousakiName1.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.ManiHensousakiName2.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_POST = this.form.ManiHensousakiPost.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.ManiHensousakiTantou.Text;
                // this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD = this.form.ManiHensousakiTorihikisakiCode.Text;

                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.GenbaTodoufukenNameRyaku.Text + this.form.GenbaAddress1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.GenbaAddress2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.BushoCode.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.GenbaKeishou1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.GenbaKeishou2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.GenbaName1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.GenbaName2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_POST = this.form.GenbaPost.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.TantoushaCode.Text;
                }
                else
                {
                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 2;
                    }
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.ManiHensousakiAddress1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.ManiHensousakiAddress2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_BUSHO = this.form.ManiHensousakiBusho.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.ManiHensousakiKeishou1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.ManiHensousakiKeishou2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME1 = this.form.ManiHensousakiName1.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_NAME2 = this.form.ManiHensousakiName2.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_POST = this.form.ManiHensousakiPost.Text;
                    this.genbaEntity.MANI_HENSOUSAKI_TANTOU = this.form.ManiHensousakiTantou.Text;
                }

                #region A票 ～　E票

                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
                this.genbaEntity.MANI_HENSOUSAKI_KBN = this.form.ManiHensousakiKbn.Checked;

                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text);
                this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = Int16.Parse(this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text);

                // 返送先(A票)
                if ("2".Equals(this.form.MANIFEST_USE_AHyo.Text) || this.sysinfoEntity.MANIFEST_USE_A == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_AHyo.Text) || this.sysinfoEntity.MANIFEST_USE_A == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_A = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.form.ManiHensousakiTorihikisakiCode_AHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.form.ManiHensousakiGyoushaCode_AHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.form.ManiHensousakiGenbaCode_AHyo.Text;
                    }
                }

                // 返送先(B1票)
                if ("2".Equals(this.form.MANIFEST_USE_B1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B1 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_B1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B1 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_B1 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.form.ManiHensousakiGyoushaCode_B1Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.form.ManiHensousakiGenbaCode_B1Hyo.Text;
                    }
                }

                // 返送先(B2票)
                if ("2".Equals(this.form.MANIFEST_USE_B2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B2 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_B2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B2 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_B2 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.form.ManiHensousakiGyoushaCode_B2Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.form.ManiHensousakiGenbaCode_B2Hyo.Text;
                    }
                }

                // 返送先(B4票)
                if ("2".Equals(this.form.MANIFEST_USE_B4Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B4 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_B4Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B4 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_B4 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.form.ManiHensousakiGyoushaCode_B4Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.form.ManiHensousakiGenbaCode_B4Hyo.Text;
                    }
                }

                // 返送先(B6票)
                if ("2".Equals(this.form.MANIFEST_USE_B6Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B6 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_B6Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_B6 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_B6 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.form.ManiHensousakiGyoushaCode_B6Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.form.ManiHensousakiGenbaCode_B6Hyo.Text;
                    }
                }

                // 返送先(C1票)
                if ("2".Equals(this.form.MANIFEST_USE_C1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C1 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_C1Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C1 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_C1 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.form.ManiHensousakiGyoushaCode_C1Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.form.ManiHensousakiGenbaCode_C1Hyo.Text;
                    }
                }

                // 返送先(C2票)
                if ("2".Equals(this.form.MANIFEST_USE_C2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C2 == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_C2Hyo.Text) || this.sysinfoEntity.MANIFEST_USE_C2 == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_C2 = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.form.ManiHensousakiGyoushaCode_C2Hyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.form.ManiHensousakiGenbaCode_C2Hyo.Text;
                    }
                }

                // 返送先(D票)
                if ("2".Equals(this.form.MANIFEST_USE_DHyo.Text) || this.sysinfoEntity.MANIFEST_USE_D == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_DHyo.Text) || this.sysinfoEntity.MANIFEST_USE_D == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_D = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.form.ManiHensousakiTorihikisakiCode_DHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.form.ManiHensousakiGyoushaCode_DHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.form.ManiHensousakiGenbaCode_DHyo.Text;
                    }
                }

                // 返送先(E票)
                if ("2".Equals(this.form.MANIFEST_USE_EHyo.Text) || this.sysinfoEntity.MANIFEST_USE_E == 2)
                {
                    this.genbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                }
                else
                {
                    if ("1".Equals(this.form.MANIFEST_USE_EHyo.Text) || this.sysinfoEntity.MANIFEST_USE_E == 1)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_USE_E = 1;
                    }

                    if (this.form.ManiHensousakiKbn.Checked && this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked)
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.TorihikisakiCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.GyoushaCode.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.form.GenbaCode.Text;
                    }
                    else
                    {
                        this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.form.ManiHensousakiTorihikisakiCode_EHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.form.ManiHensousakiGyoushaCode_EHyo.Text;
                        this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.form.ManiHensousakiGenbaCode_EHyo.Text;
                    }
                }
                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end

                #endregion

                if (!string.IsNullOrWhiteSpace(this.form.ManifestShuruiCode.Text))
                {
                    this.genbaEntity.MANIFEST_SHURUI_CD = Int16.Parse(this.form.ManifestShuruiCode.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.ManifestTehaiCode.Text))
                {
                    this.genbaEntity.MANIFEST_TEHAI_CD = Int16.Parse(this.form.ManifestTehaiCode.Text);
                }

                this.genbaEntity.SAISHUU_SHOBUNJOU_KBN = this.form.SaishuuShobunjouKbn.Checked;
                this.genbaEntity.SEARCH_TEKIYOU_BEGIN = "";
                this.genbaEntity.SEARCH_TEKIYOU_END = "";
                this.genbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.form.SeikyuuSoufuAddress1.Text;
                this.genbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.form.SeikyuuSoufuAddress2.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.genbaEntity.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.genbaEntity.SEIKYUU_SOUFU_BUSHO = this.form.SeikyuuSoufuBusho.Text;
                this.genbaEntity.SEIKYUU_SOUFU_FAX = this.form.SoufuGenbaFax.Text;
                this.genbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.form.SeikyuuSouhuKeishou1.Text;
                this.genbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.form.SeikyuuSouhuKeishou2.Text;
                this.genbaEntity.SEIKYUU_SOUFU_NAME1 = this.form.SeikyuushoSoufusaki1.Text;
                this.genbaEntity.SEIKYUU_SOUFU_NAME2 = this.form.SeikyuushoSoufusaki2.Text;
                this.genbaEntity.SEIKYUU_SOUFU_POST = this.form.SeikyuuSoufuPost.Text;
                this.genbaEntity.SEIKYUU_SOUFU_TANTOU = this.form.SeikyuuSoufuTantou.Text;
                this.genbaEntity.SEIKYUU_SOUFU_TEL = this.form.SoufuGenbaTel.Text;
                this.genbaEntity.SEIKYUU_TANTOU = this.form.SeikyuuTantou.Text;
                if (this.form.SeikyuuDaihyouPrintKbn.Text.Length > 0)
                {
                    this.genbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = Int16.Parse(this.form.SeikyuuDaihyouPrintKbn.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.genbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    this.genbaEntity.SEIKYUU_KYOTEN_CD = Int16.Parse(this.form.SEIKYUU_KYOTEN_CD.Text);
                }
                this.genbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.form.ShiharaiSoufuAddress1.Text;
                this.genbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.form.ShiharaiSoufuAddress2.Text;
                this.genbaEntity.SHIHARAI_SOUFU_BUSHO = this.form.ShiharaiSoufuBusho.Text;
                this.genbaEntity.SHIHARAI_SOUFU_FAX = this.form.ShiharaiGenbaFax.Text;
                this.genbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.form.ShiharaiSoufuKeishou1.Text;
                this.genbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.form.ShiharaiSoufuKeishou2.Text;
                this.genbaEntity.SHIHARAI_SOUFU_NAME1 = this.form.ShiharaiSoufuName1.Text;
                this.genbaEntity.SHIHARAI_SOUFU_NAME2 = this.form.ShiharaiSoufuName2.Text;
                this.genbaEntity.SHIHARAI_SOUFU_POST = this.form.ShiharaiSoufuPost.Text;
                this.genbaEntity.SHIHARAI_SOUFU_TANTOU = this.form.ShiharaiSoufuTantou.Text;
                this.genbaEntity.SHIHARAI_SOUFU_TEL = this.form.ShiharaiGenbaTel.Text;
                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.genbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }
                if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    this.genbaEntity.SHIHARAI_KYOTEN_CD = Int16.Parse(this.form.SHIHARAI_KYOTEN_CD.Text);
                }
                this.genbaEntity.SHOBUNSAKI_NO = this.form.ShobunsakiCode.Text;
                this.genbaEntity.SHOKUCHI_KBN = this.form.ShokuchiKbn.Checked;
                this.genbaEntity.SHUUKEI_ITEM_CD = this.form.ShuukeiItemCode.Text;
                this.genbaEntity.TANTOUSHA = this.form.TantoushaCode.Text;

                if (this.form.TekiyouKikanForm.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanForm.Value.ToString(), out timeBegin);
                    this.genbaEntity.TEKIYOU_BEGIN = timeBegin;
                }
                if (this.form.TekiyouKikanTo.Value != null)
                {
                    DateTime.TryParse(this.form.TekiyouKikanTo.Value.ToString(), out timeEnd);
                    this.genbaEntity.TEKIYOU_END = timeEnd;
                }

                if (this.form.TorihikisakiCode.Text != null)
                {
                    this.genbaEntity.TORIHIKI_JOUKYOU = 1;
                }
                else
                {
                    this.genbaEntity.TORIHIKI_JOUKYOU = 2;
                }
                this.genbaEntity.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                this.genbaEntity.TSUMIKAEHOKAN_KBN = this.form.TsumikaeHokanKbn.Checked;

                // 20151102 BUNN #12040 STR
                // 排出事業場/荷積現場区分
                if (this.form.HaishutsuKbn.Checked)
                {
                    this.genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                }
                else
                {
                    this.genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = false;
                }
                // 処分事業場/荷降現場区分
                if (this.form.ShobunJigyoujouKbn.Checked)
                {
                    this.genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = true;
                }
                else
                {
                    this.genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = false;
                }
                // 20151102 BUNN #12040 STR

                // 20141209 katen #2927 実績報告書　フィードバック対応 start
                this.genbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                // 20141209 katen #2927 実績報告書　フィードバック対応 end

                this.genbaEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP.Text);

                // 更新者情報設定
                var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_HIKIAI_GENBA>(this.genbaEntity);
                dataBinderLogicGenba.SetSystemProperty(this.genbaEntity, false);

                // 定期情報
                // M_HIKIAI_GENBA_TEIKI_HINMEI[] hinTeiki = new M_HIKIAI_GENBA_TEIKI_HINMEI[this.form.TeikiHinmeiIchiran.RowCount - 1];
                REGIST_GENBA_TEIKI_HINMEI[] hinTeiki = new REGIST_GENBA_TEIKI_HINMEI[this.form.TeikiHinmeiIchiran.RowCount - 1];
                for (int i = 0; i < hinTeiki.Length; i++)
                {
                    hinTeiki[i] = new REGIST_GENBA_TEIKI_HINMEI();

                    if (!(Boolean)this.form.TeikiHinmeiIchiran.Rows[i].Cells["DELETE_FLG"].Value)
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
                    if (row[Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG] == DBNull.Value)
                    {
                        row[Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_MONDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_MONDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_TUESDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_TUESDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_WEDNESDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_WEDNESDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_THURSDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_THURSDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_FRIDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_FRIDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_SATURDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_SATURDAY] = false;
                    }
                    if (row[Const.ConstCls.TEIKI_SUNDAY] == DBNull.Value)
                    {
                        row[Const.ConstCls.TEIKI_SUNDAY] = false;
                    }
                }

                var dataBinderLogicGenbaTeiki = new DataBinderLogic<REGIST_GENBA_TEIKI_HINMEI>(hinTeiki);
                var genbaTeikiEntityList = dataBinderLogicGenbaTeiki.CreateEntityForDataTable(this.form.TeikiHinmeiIchiran);
                this.genbaTeikiEntity = new List<M_HIKIAI_GENBA_TEIKI_HINMEI>();
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

                    // 画面状態　削除フラグ取得
                    if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                    {
                        row.HIKIAI_GYOUSHA_USE_FLG = true;
                    }
                    else
                    {
                        row.HIKIAI_GYOUSHA_USE_FLG = false;
                    }

                    rowNo++;
                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    this.genbaTeikiEntity.Add(row);
                }

                // 定期回収情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TeikiHinmeiIchiran.Sort("HINMEI_CD");

                // 月極情報
                REGIST_GENBA_TSUKI_HINMEI[] hinTsuki = new REGIST_GENBA_TSUKI_HINMEI[this.form.TsukiHinmeiIchiran.RowCount - 1];
                for (int i = 0; i < hinTsuki.Length; i++)
                {
                    hinTsuki[i] = new REGIST_GENBA_TSUKI_HINMEI();

                    // 画面状態　削除フラグ取得
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
                    if (row[Const.ConstCls.TSUKI_CHOUKA_SETTING] == DBNull.Value)
                    {
                        row[Const.ConstCls.TSUKI_CHOUKA_SETTING] = false;
                    }
                    if (row[Const.ConstCls.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] == DBNull.Value)
                    {
                        row[Const.ConstCls.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN] = false;
                    }
                }

                var dataBinderLogicGenbaTsuki = new DataBinderLogic<REGIST_GENBA_TSUKI_HINMEI>(hinTsuki);
                var genbaTsukiEntityList = dataBinderLogicGenbaTsuki.CreateEntityForDataTable(this.form.TsukiHinmeiIchiran);
                this.genbaTsukiEntity = new List<M_HIKIAI_GENBA_TSUKI_HINMEI>();
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

                    if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                    {
                        row.HIKIAI_GYOUSHA_USE_FLG = true;
                    }
                    else
                    {
                        row.HIKIAI_GYOUSHA_USE_FLG = false;
                    }

                    row.GYOUSHA_CD = this.form.GyoushaCode.Text;
                    row.GENBA_CD = this.form.GenbaCode.Text;
                    row.ROW_NO = rowNo;
                    this.genbaTsukiEntity.Add(row);
                }

                // 月極情報タブの明細欄を「品名CD」でソート（昇順）
                this.form.TsukiHinmeiIchiran.Sort("HINMEI_CD");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 引合現場マスタ更新
                    this.genbaEntity.DELETE_FLG = false;
                    this.daoHikiaiGenba.Insert(this.genbaEntity);

                    // 現場_定期情報マスタ登録
                    foreach (M_HIKIAI_GENBA_TEIKI_HINMEI genbaTeiki in this.genbaTeikiEntity)
                    {
                        this.daoGenbaTeiki.Insert(genbaTeiki);
                    }

                    // 現場_月極情報マスタ登録
                    foreach (M_HIKIAI_GENBA_TSUKI_HINMEI genbaTsuki in this.genbaTsukiEntity)
                    {
                        this.daoGenbaTsuki.Insert(genbaTsuki);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.messBSL.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.messBSL.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.messBSL.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 引合現場マスタ更新
                    this.genbaEntity.DELETE_FLG = false;
                    this.daoHikiaiGenba.Update(this.genbaEntity);

                    // 現場_定期情報マスタ登録
                    M_HIKIAI_GENBA_TEIKI_HINMEI condTeiki = new M_HIKIAI_GENBA_TEIKI_HINMEI();
                    condTeiki.GYOUSHA_CD = this.genbaEntity.GYOUSHA_CD;
                    condTeiki.GENBA_CD = this.genbaEntity.GENBA_CD;
                    condTeiki.HIKIAI_GYOUSHA_USE_FLG = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;
                    this.daoGenbaTeiki.DeleteTeikiHinmei(condTeiki);
                    foreach (M_HIKIAI_GENBA_TEIKI_HINMEI genbaTeiki in this.genbaTeikiEntity)
                    {
                        this.daoGenbaTeiki.Insert(genbaTeiki);
                    }

                    // 現場_月極情報マスタ登録
                    M_HIKIAI_GENBA_TSUKI_HINMEI condTsuki = new M_HIKIAI_GENBA_TSUKI_HINMEI();
                    condTsuki.GYOUSHA_CD = this.genbaEntity.GYOUSHA_CD;
                    condTsuki.GENBA_CD = this.genbaEntity.GENBA_CD;
                    condTsuki.HIKIAI_GYOUSHA_USE_FLG = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;
                    this.daoGenbaTsuki.DeleteTsukiHinmei(condTsuki);
                    foreach (M_HIKIAI_GENBA_TSUKI_HINMEI genbaTsuki in this.genbaTsukiEntity)
                    {
                        this.daoGenbaTsuki.Insert(genbaTsuki);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "更新");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.messBSL.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.messBSL.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.messBSL.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 引合現場マスタ更新
                        this.genbaEntity.DELETE_FLG = true;
                        this.daoHikiaiGenba.Update(this.genbaEntity);
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.messBSL.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.messBSL.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.messBSL.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
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

        #endregion

        #region 申請処理群

        #region 電子申請画面用初期化DTO作成

        /// <summary>
        /// 電子申請画面用初期化DTO作成
        /// </summary>
        /// <returns></returns>
        internal DenshiShinseiNyuuryokuInitDTO CreateDenshiShinseiInitDto()
        {
            var returnVal = new DenshiShinseiNyuuryokuInitDTO();

            if ("1".Equals(this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text))
            {
                returnVal.HikiaiTorihikisakiCd = this.form.TorihikisakiCode.Text;
                returnVal.HikiaiTorihikisakiNameRyaku = this.hikiaiTorihikisakiEntity == null ? string.Empty : this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                returnVal.TorihikisakiCd = this.form.TorihikisakiCode.Text;
                returnVal.TorihikisakiNameRyaku = this.torihikisakiEntity == null ? string.Empty : this.torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }

            if ("1".Equals(this.form.HIKIAI_GYOUSHA_USE_FLG.Text))
            {
                returnVal.HikiaiGyoushaCd = this.form.GyoushaCode.Text;
                returnVal.HikiaiGyoushaNameRyaku = this.hikiaiGyoushaEntity == null ? string.Empty : this.hikiaiGyoushaEntity.GYOUSHA_NAME_RYAKU;
            }
            else
            {
                returnVal.GyoushaCd = this.form.GyoushaCode.Text;
                returnVal.GyoushaNameRyaku = this.gyoushaEntity == null ? string.Empty : this.gyoushaEntity.GYOUSHA_NAME_RYAKU;
            }

            returnVal.HikiaiGenbaCd = this.form.GenbaCode.Text;
            returnVal.HikiaiGenbaNameRyaku = this.form.GenbaNameRyaku.Text;
            returnVal.NaiyouCd = DenshiShinseiUtility.NAIYOU_CD_GENBA;

            return returnVal;
        }

        #endregion

        #region 電子申請データﾁｪｯｸ

        /// <summary>
        /// 電子申請データチェック
        /// </summary>
        /// <returns>true:申請OK、false:申請NG</returns>
        internal bool CheckDenshiShinseiData(out bool catchErr)
        {
            catchErr = true;
            var dsUtility = new DenshiShinseiUtility();
            DenshiShinseiUtility.SHINSEI_MASTER_KBN type;
            bool ret = true;

            try
            {
                if ("1".Equals(this.form.HIKIAI_GYOUSHA_USE_FLG.Text))
                {
                    type = DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_HIKIAI_GYOUSHA;
                }
                else
                {
                    type = DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_KIZON_GYOUSHA;
                }
                ret = dsUtility.IsPossibleData(type, null, this.form.GyoushaCode.Text, this.form.GenbaCode.Text);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LogicalDelete", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return ret;
        }

        #endregion

        #region 本登録済みか判定

        /// <summary>
        /// 指定された引合業者利用フラグ,業者CDが本登録済みデータか判定
        /// </summary>
        /// <param name="hikiaiGyoushaUseFlg">引合業者利用フラグ</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedGyousha(string hikiaiGyoushaUseFlg, string gyoushaCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(hikiaiGyoushaUseFlg)
                    || string.IsNullOrEmpty(gyoushaCD))
                {
                    return false;
                }

                if (hikiaiGyoushaUseFlg.Equals("1"))
                {
                    // 引合業者使用時は、引合業者CDも本登録済みかチェックを行う
                    var gyoushaEntity = daoHikiaiGyousha.GetDataByCd(gyoushaCD);
                    if (gyoushaEntity != null && !string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_CD_AFTER))
                    {
                        // 移行後業者CDに値がある場合は、本登録済みデータとみなす
                        return true;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ExistedGyousha", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistedGyousha", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return false;
        }

        /// <summary>
        /// 指定された引合業者利用フラグ,業者CD,現場CDが本登録済みデータか判定
        /// </summary>
        /// <param name="hikiaiGyoushaUseFlg">引合業者利用フラグ</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedGenba(string hikiaiGyoushaUseFlg, string gyoushaCD, string genbaCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(hikiaiGyoushaUseFlg)
                    || string.IsNullOrEmpty(gyoushaCD)
                    || string.IsNullOrEmpty(genbaCD))
                {
                    return false;
                }

                var hikiaiGenba = new M_HIKIAI_GENBA();
                if (hikiaiGyoushaUseFlg.Equals("1"))
                {
                    hikiaiGenba.HIKIAI_GYOUSHA_USE_FLG = System.Data.SqlTypes.SqlBoolean.True;
                }
                else
                {
                    hikiaiGenba.HIKIAI_GYOUSHA_USE_FLG = System.Data.SqlTypes.SqlBoolean.False;
                }
                hikiaiGenba.GYOUSHA_CD = gyoushaCD;
                hikiaiGenba.GENBA_CD = genbaCD;

                var entity = daoHikiaiGenba.GetDataByCd(hikiaiGenba);
                if (entity != null && !string.IsNullOrEmpty(entity.GENBA_CD_AFTER))
                {
                    // 移行後現場CDに値がある場合は、本登録済みデータとみなす
                    return true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ExistedGenba", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistedGenba", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return false;
        }

        #endregion

        #region 申請処理

        /// <summary>
        /// 申請処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Shinsei(bool errorFlag, T_DENSHI_SHINSEI_ENTRY dsEntry, List<T_DENSHI_SHINSEI_DETAIL> dsDetailList)
        {
            try
            {
                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 引合現場マスタ更新
                    this.genbaEntity.DELETE_FLG = false;
                    this.daoHikiaiGenba.Update(this.genbaEntity);

                    // 現場_定期情報マスタ登録
                    M_HIKIAI_GENBA_TEIKI_HINMEI condTeiki = new M_HIKIAI_GENBA_TEIKI_HINMEI();
                    condTeiki.GYOUSHA_CD = this.genbaEntity.GYOUSHA_CD;
                    condTeiki.GENBA_CD = this.genbaEntity.GENBA_CD;
                    condTeiki.HIKIAI_GYOUSHA_USE_FLG = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;
                    this.daoGenbaTeiki.DeleteTeikiHinmei(condTeiki);
                    foreach (M_HIKIAI_GENBA_TEIKI_HINMEI genbaTeiki in this.genbaTeikiEntity)
                    {
                        this.daoGenbaTeiki.Insert(genbaTeiki);
                    }

                    // 現場_月極情報マスタ登録
                    M_HIKIAI_GENBA_TSUKI_HINMEI condTsuki = new M_HIKIAI_GENBA_TSUKI_HINMEI();
                    condTsuki.GYOUSHA_CD = this.genbaEntity.GYOUSHA_CD;
                    condTsuki.GENBA_CD = this.genbaEntity.GENBA_CD;
                    condTsuki.HIKIAI_GYOUSHA_USE_FLG = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;
                    this.daoGenbaTsuki.DeleteTsukiHinmei(condTsuki);
                    foreach (M_HIKIAI_GENBA_TSUKI_HINMEI genbaTsuki in this.genbaTsukiEntity)
                    {
                        this.daoGenbaTsuki.Insert(genbaTsuki);
                    }

                    // 申請入力登録
                    DenshiShinseiDBAccessor dsDBAccessor = new DenshiShinseiDBAccessor();

                    // 電子申請入力の登録
                    DenshiShinseiUtility.SHINSEI_MASTER_KBN type;

                    if ("1".Equals(this.form.HIKIAI_GYOUSHA_USE_FLG.Text))
                    {
                        type = DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_HIKIAI_GYOUSHA;
                    }
                    else
                    {
                        type = DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_KIZON_GYOUSHA;
                    }

                    dsDBAccessor.InsertDSEntry(dsEntry, type);

                    // 電子申請明細の登録
                    foreach (T_DENSHI_SHINSEI_DETAIL dsDetail in dsDetailList)
                    {
                        dsDBAccessor.InsertDSDetail(dsDetail, dsEntry.SYSTEM_ID, dsEntry.SEQ, dsEntry.SHINSEI_NUMBER);
                    }

                    // 電子申請状態の登録
                    dsDBAccessor.InsertDSStatus(new T_DENSHI_SHINSEI_STATUS(), dsEntry.SYSTEM_ID, dsEntry.SEQ);

                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "申請");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Shinsei", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.messBSL.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.messBSL.MessageBoxShow("E093");
                }
                else
                {
                    this.form.messBSL.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
        }

        #endregion

        #endregion

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            // objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicCls localLogic = other as LogicCls;
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
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public ConstCls.GenbaCdLeaveResult DupliCheckGenbaCd(string zeroPadCd, bool isRegister)
        {
            ConstCls.GenbaCdLeaveResult ViewUpdateWindow = 0;

            try
            {
                LogUtility.DebugMethodStart(zeroPadCd, isRegister);

                // 現場マスタ検索
                M_HIKIAI_GENBA genbaSearchString = new M_HIKIAI_GENBA();
                genbaSearchString.HIKIAI_GYOUSHA_USE_FLG = this.HikiaiGyoushaUseFlg;
                genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
                genbaSearchString.GENBA_CD = zeroPadCd;
                M_HIKIAI_GENBA genbaEntity = this.daoHikiaiGenba.GetDataByCd(genbaSearchString);

                // 重複チェック
                M463Validator vali = new M463Validator();
                DialogResult resultDialog = new DialogResult();
                bool resultDupli = vali.GenbaCDValidator(genbaEntity, isRegister, out resultDialog);

                // 重複チェックの結果と、ポップアップの結果で動作を変える
                if (!resultDupli && resultDialog == DialogResult.OK)
                {
                    ViewUpdateWindow = ConstCls.GenbaCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli && resultDialog == DialogResult.Yes)
                {
                    ViewUpdateWindow = ConstCls.GenbaCdLeaveResult.FALSE_ON;
                }
                else if (!resultDupli && resultDialog == DialogResult.No)
                {
                    ViewUpdateWindow = ConstCls.GenbaCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli)
                {
                    ViewUpdateWindow = ConstCls.GenbaCdLeaveResult.FALSE_NONE;
                }
                else
                {
                    ViewUpdateWindow = ConstCls.GenbaCdLeaveResult.TURE_NONE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliCheckGenbaCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ViewUpdateWindow);
            }

            return ViewUpdateWindow;
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            string padData = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(inputData);

                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.GenbaCode.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaCode, null);

                padData = inputData.PadLeft((int)charNumber, '0');
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZeroPadding", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(padData);
            }

            return padData;
        }

        /// <summary>
        /// ゼロパディング(取引先)処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPaddingTorisaki(string inputData)
        {
            string padData = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(inputData);

                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.TorihikisakiCode.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaCode, null);

                padData = inputData.PadLeft((int)charNumber, '0');
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZeroPaddingTorisaki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(padData);
            }

            return padData;
        }

        /// <summary>
        /// ゼロパディング(県)処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPaddingKen(string inputData)
        {
            string padData = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(inputData);

                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.GenbaTodoufukenCode.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GenbaTodoufukenCode, null);

                padData = inputData.PadLeft((int)charNumber, '0');
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZeroPaddingKen", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(padData);
            }

            return padData;
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // イベント制御(削除)
                this.RemoveDynamicEvent();

                // エラー表示をクリアする
                foreach (var ctrl in this.allControl)
                {
                    if (ctrl is ICustomTextBox && ((ICustomTextBox)ctrl).IsInputErrorOccured)
                    {
                        ((ICustomTextBox)ctrl).IsInputErrorOccured = false;
                    }
                }

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規モードの場合は空画面を表示する
                    this.WindowInitNewMode(parentForm);
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得処理(現場)
        /// </summary>
        /// <returns></returns>
        public int SearchGenba()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                M_HIKIAI_GENBA genbaSearchString = new M_HIKIAI_GENBA();
                // genbaSearchString.GYOUSHA_CD = this.form.GyoushaCode.Text;
                // 現場CDの入力値をゼロパディング
                // string zeroPadCd = this.ZeroPadding(this.form.GenbaCode.Text);
                // genbaSearchString.GENBA_CD = zeroPadCd;

                genbaSearchString.HIKIAI_GYOUSHA_USE_FLG = this.HikiaiGyoushaUseFlg;
                genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.ZeroPadding(this.GenbaCd);
                genbaSearchString.GENBA_CD = zeroPadCd;

                this.genbaEntity = null;

                this.genbaEntity = daoHikiaiGenba.GetDataByCd(genbaSearchString);

                count = this.genbaEntity == null ? 0 : 1;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchGenba", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenba", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// 業者コード変更処理
        /// </summary>
        public bool ChangeGyousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.IsSetDataFromPopup)
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }

                this.IsSetDataFromPopup = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeGyousha", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得処理(業者変更時の後処理)
        /// </summary>
        /// <returns></returns>
        /// <param name="isInputCheck"></param>
        public bool SearchchkGyousha(bool isInputCheck, bool setGyoushaOnly)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isInputCheck, setGyoushaOnly);

                this.gyoushaEntity = null;
                this.hikiaiGyoushaEntity = null;

                // チェックBOXへのセットを行う
                bool isSet = false;
                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("0"))
                {
                    M_GYOUSHA queryParam = new M_GYOUSHA();
                    queryParam.GYOUSHA_CD = this.form.GyoushaCode.Text;

                    // 入力チェックフラグがtrueの場合は有効なデータを取得する、falseの場合は削除・期間外も対象とする
                    this.gyoushaEntity = null;
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

                    _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                    _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                    if (this.gyoushaEntity != null)
                    {
                        isSet = true;

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
                            this.form.ShobunJigyoujouKbn.Checked = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.SaishuuShobunjouKbn.Enabled = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.SaishuuShobunjouKbn.Checked = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.ShobunsakiCode.Enabled = (bool)this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                        }

                        // this.form.NioroshiGenbaKbn.Enabled = (bool)this.gyoushaEntity.NIOROSHI_GHOUSHA_KBN;
                        if ((!this.gyoushaEntity.JISHA_KBN && !this.gyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN && !this.gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN && !this.gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN && !this.gyoushaEntity.MANI_HENSOUSAKI_KBN)
                            || !this.gyoushaEntity.GYOUSHAKBN_MANI)
                        {
                            this.ChangeManifestsDisable();
                        }

                        // マニ記載業者チェックがついていないときは非活性
                        if (!this.gyoushaEntity.GYOUSHAKBN_MANI)
                        {
                            // マニ記載に関わらず、自社区分は常に入力可
                            // this.form.JishaKbn.Enabled = false;
                            this.form.TsumikaeHokanKbn.Enabled = false;
                            // this.form.HaishutsuKbn.Enabled = false;
                            // this.form.ShobunJigyoujouKbn.Enabled = false;
                            this.form.SaishuuShobunjouKbn.Enabled = false;

                            // this.form.JishaKbn.Checked = false;
                            this.form.TsumikaeHokanKbn.Checked = false;
                            // this.form.HaishutsuKbn.Checked = false;
                            // this.form.ShobunJigyoujouKbn.Checked = false;
                            this.form.SaishuuShobunjouKbn.Checked = false;
                        }

                        // 業者の取引先有無により処理をする1:有 2:無
                        this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                        if (this.gyoushaEntity.TORIHIKISAKI_UMU_KBN == 1)
                        {
                            this.torihikisakiEntity = null;
                            this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.gyoushaEntity.TORIHIKISAKI_CD);

                            _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                            _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                            // 20140722 ria EV005371 取引先が紐づいている既存業者を入力するとシステムエラー start
                            // if (this.torihikisakiEntity != null && !this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                            if (this.torihikisakiEntity != null)
                            // 20140722 ria EV005371 取引先が紐づいている既存業者を入力するとシステムエラー end
                            {
                                // 設定元の業者が既存業者であれば、必ず取引先も既存となる
                                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";

                                this.form.TorihikisakiCode.Enabled = true;
                                this.form.TorihikisakiCodeSearchButton.Enabled = true;
                                this.form.TorihikisakiNew.Enabled = true;

                                this.form.TorihikisakiCode.Text = this.torihikisakiEntity.TORIHIKISAKI_CD;
                                this.form.TorihikisakiName1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                                this.form.TorihikisakiName2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                                if (!this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                                {
                                    this.form.KyotenCode.Text = this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                                }

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
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;

                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;

                            // 取引先にはいるので現場に移動
                            this.form.GenbaCode.Focus();
                        }
                    }
                }
                else if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_GYOUSHA queryParam = new M_HIKIAI_GYOUSHA();
                    queryParam.GYOUSHA_CD = this.form.GyoushaCode.Text;

                    // 入力チェックフラグがtrueの場合は有効なデータを取得する、falseの場合は削除・期間外も対象とする
                    this.hikiaiGyoushaEntity = null;
                    if (isInputCheck)
                    {
                        M_HIKIAI_GYOUSHA[] result = this.daoHikiaiGyousha.GetAllValidData(queryParam);
                        if (result != null && result.Length > 0)
                        {
                            this.hikiaiGyoushaEntity = result[0];
                        }
                    }
                    else
                    {
                        this.hikiaiGyoushaEntity = this.daoHikiaiGyousha.GetDataByCd(queryParam.GYOUSHA_CD);
                    }

                    _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                    _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                    if (this.hikiaiGyoushaEntity != null)
                    {
                        isSet = true;

                        this.form.GyoushaKbnUkeire.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE;
                        this.form.GyoushaKbnShukka.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA;
                        this.form.GyoushaKbnMani.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI;

                        if (!this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                        {
                            this.FlgGyoushaHaishutuKbn = (bool)this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                        }
                        else
                        {
                            this.FlgGyoushaHaishutuKbn = false;
                        }

                        this.form.GyoushaName1.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
                        this.form.GyoushaName2.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;

                        if (this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.GYOUSHA_TEKIYOU_BEGIN.Value = null;
                        }
                        else
                        {
                            this.form.GYOUSHA_TEKIYOU_BEGIN.Value = this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.Value;
                        }

                        if (this.hikiaiGyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            this.form.GYOUSHA_TEKIYOU_END.Value = null;
                        }
                        else
                        {
                            this.form.GYOUSHA_TEKIYOU_END.Value = this.hikiaiGyoushaEntity.TEKIYOU_END.Value;
                        }

                        this.form.JishaKbn.Enabled = (bool)this.hikiaiGyoushaEntity.JISHA_KBN;
                        if (!this.hikiaiGyoushaEntity.JISHA_KBN)
                        {
                            this.form.JishaKbn.Checked = (bool)this.hikiaiGyoushaEntity.JISHA_KBN;
                        }

                        this.form.HaishutsuKbn.Enabled = this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue;
                        if (!this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN)
                        {
                            this.form.HaishutsuKbn.Checked = (bool)this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                        }

                        this.form.TsumikaeHokanKbn.Enabled = this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue;
                        if (!this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN)
                        {
                            this.form.TsumikaeHokanKbn.Enabled = (bool)this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                            this.form.TsumikaeHokanKbn.Checked = (bool)this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                        }

                        this.form.ShobunJigyoujouKbn.Enabled = this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                        this.form.SaishuuShobunjouKbn.Enabled = this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                        if (!this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN)
                        {
                            this.form.ShobunJigyoujouKbn.Checked = (bool)this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.SaishuuShobunjouKbn.Enabled = (bool)this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.SaishuuShobunjouKbn.Checked = (bool)this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                            this.form.ShobunsakiCode.Enabled = (bool)this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                        }

                        // No.3988修正時発見不具合修正-->
                        // if ((!this.hikiaiGyoushaEntity.JISHA_KBN && !this.hikiaiGyoushaEntity.HAISHUTSU_JIGYOUSHA_KBN && !this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KBN && !this.hikiaiGyoushaEntity.SHOBUN_JUTAKUSHA_KBN && !this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN)
                        // || !this.gyoushaEntity.GYOUSHAKBN_MANI)
                        if ((!this.hikiaiGyoushaEntity.JISHA_KBN && !this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN && !this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN && !this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN && !this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN)
                            || !this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI)
                        // No.3988修正時発見不具合修正<--
                        {
                            this.ChangeManifestsDisable();
                        }

                        // マニ記載業者チェックがついていないときは非活性
                        if (!this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI)
                        {
                            // 自社区分はマニ記載の状態に関わらず入力可
                            // this.form.JishaKbn.Enabled = false;
                            this.form.TsumikaeHokanKbn.Enabled = false;
                            // this.form.HaishutsuKbn.Enabled = false;
                            // this.form.ShobunJigyoujouKbn.Enabled = false;
                            this.form.SaishuuShobunjouKbn.Enabled = false;

                            // this.form.JishaKbn.Checked = false;
                            this.form.TsumikaeHokanKbn.Checked = false;
                            // this.form.HaishutsuKbn.Checked = false;
                            // this.form.ShobunJigyoujouKbn.Checked = false;
                            this.form.SaishuuShobunjouKbn.Checked = false;
                        }

                        // 業者の取引先有無により処理をする1:有 2:無
                        if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 1)
                        {
                            this.torihikisakiEntity = null;
                            this.hikiaiTorihikisakiEntity = null;

                            _tabPageManager.ChangeTabPageVisible("tabPageSeikyuuInfo", true);
                            _tabPageManager.ChangeTabPageVisible("tabPageShiharaiInfo", true);

                            var hikiaiTorihikisakiUseFlg = false;
                            if (setGyoushaOnly)
                            {
                                // データ読込時（修正/削除）は既に取得している引合取引先利用フラグを使用
                                hikiaiTorihikisakiUseFlg = "1".Equals(this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text);
                            }
                            else
                            {
                                // データ読込時以外は業者に紐付く引合取引先利用フラグを使用
                                if (!this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                                {
                                    hikiaiTorihikisakiUseFlg = this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.Value;
                                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG ? "1" : "0";
                                }
                            }

                            // 引合業者マスタの引合取引先利用フラグによって対象とする取引先を変更する
                            if (hikiaiTorihikisakiUseFlg)
                            {
                                if (setGyoushaOnly)
                                {
                                    // データ読込時（修正/削除）は既に取得しているCDより取引先検索
                                    this.hikiaiTorihikisakiEntity = daoHikiaiTorihikisaki.GetDataByCd(this.form.TorihikisakiCode.Text);
                                }
                                else
                                {
                                    // データ読込時以外は業者に紐付くCDより取引先検索
                                    this.hikiaiTorihikisakiEntity = daoHikiaiTorihikisaki.GetDataByCd(this.hikiaiGyoushaEntity.TORIHIKISAKI_CD);
                                }

                                if (this.hikiaiTorihikisakiEntity != null)
                                {
                                    this.form.TorihikisakiCode.Enabled = true;
                                    this.form.TorihikisakiCodeSearchButton.Enabled = true;
                                    this.form.TorihikisakiNew.Enabled = true;

                                    this.form.TorihikisakiCode.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_CD;
                                    this.form.TorihikisakiName1.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME1;
                                    this.form.TorihikisakiName2.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME2;
                                    if (!this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                                    {
                                        this.form.KyotenCode.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                                    }

                                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                                    {
                                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                                    }
                                    else
                                    {
                                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN.Value;
                                    }

                                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_END.IsNull)
                                    {
                                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                                    }
                                    else
                                    {
                                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_END.Value;
                                    }

                                    this.SearchsetTorihikisaki();
                                }
                            }
                            else
                            {
                                if (setGyoushaOnly)
                                {
                                    // データ読込時（修正/削除）は既に取得しているCDより取引先検索
                                    this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.form.TorihikisakiCode.Text);
                                }
                                else
                                {
                                    // データ読込時以外は業者に紐付くCDより取引先検索
                                    this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.hikiaiGyoushaEntity.TORIHIKISAKI_CD);
                                }

                                if (this.torihikisakiEntity != null)
                                {
                                    this.form.TorihikisakiCode.Enabled = true;
                                    this.form.TorihikisakiCodeSearchButton.Enabled = true;
                                    this.form.TorihikisakiNew.Enabled = true;

                                    this.form.TorihikisakiCode.Text = this.torihikisakiEntity.TORIHIKISAKI_CD;
                                    this.form.TorihikisakiName1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                                    this.form.TorihikisakiName2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
                                    if (!this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                                    {
                                        this.form.KyotenCode.Text = this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                                    }

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

                                    this.SearchsetTorihikisaki();
                                }
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
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;

                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                            // 取引先にはいるので現場に移動
                            this.form.GenbaCode.Focus();
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
                        this.form.KyotenCode.Text = string.Empty;
                        this.form.KyotenName.Text = string.Empty;

                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                        // 取引先にはいるので現場に移動
                        this.form.GenbaCode.Focus();
                    }
                }

                if (!isSet)
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = string.Empty;
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;

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

                    // 現場分類
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
                        // 業者に移動
                        this.form.GyoushaCode.Focus();
                    }
                }

                // マニ種類、マニ手配を動的に変える
                this.ChangeManiKbn();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchchkGyousha", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchchkGyousha", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 取引先コード変更処理
        /// </summary>
        public bool ChangeTorihikisai()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.IsSetDataFromPopup)
                {
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                }

                this.IsSetDataFromPopup = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikisai", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得処理(取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchsetTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                bool isSet = false;
                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
                {
                    M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                    M_TORIHIKISAKI[] result = this.daoTorihikisaki.GetAllValidData(queryParam);

                    M_TORIHIKISAKI_SHIHARAI shiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();

                    shiharaiEntity = this.daoShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                    if (result != null && result.Length > 0)
                    {
                        M_TORIHIKISAKI entity = result[0];
                        this.form.TorihikisakiName1.Text = entity.TORIHIKISAKI_NAME1;
                        this.form.TorihikisakiName2.Text = entity.TORIHIKISAKI_NAME2;
                        if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KyotenCode.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        this.form.KyotenName.Text = string.Empty;
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
                        isSet = true;
                    }

                    Boolean isKake = true;
                    if (seikyuuEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu, isKake);
                    }

                    isKake = true;
                    if (shiharaiEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[支払情報タブ]内部を非活性
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Siharai, isKake);
                    }
                }
                else if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_TORIHIKISAKI queryParam = new M_HIKIAI_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                    M_HIKIAI_TORIHIKISAKI[] result = this.daoHikiaiTorihikisaki.GetAllValidDataMinCols(queryParam);

                    M_HIKIAI_TORIHIKISAKI_SHIHARAI shiharaiEntity = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();
                    M_HIKIAI_TORIHIKISAKI_SEIKYUU seikyuuEntity = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();

                    shiharaiEntity = this.daoHikiaiShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    seikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                    if (result != null && result.Length > 0)
                    {
                        M_HIKIAI_TORIHIKISAKI entity = result[0];
                        this.form.TorihikisakiName1.Text = entity.TORIHIKISAKI_NAME1;
                        this.form.TorihikisakiName2.Text = entity.TORIHIKISAKI_NAME2;
                        if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KyotenCode.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        this.form.KyotenName.Text = string.Empty;
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
                        isSet = true;
                    }

                    Boolean isKake = true;
                    if (seikyuuEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else if (seikyuuEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu, isKake);
                    }

                    isKake = true;
                    if (shiharaiEntity != null)
                    {
                        // 取引先区分が[1.現金]時には[支払情報タブ]内部を非活性
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Siharai, isKake);
                    }
                }

                if (!isSet)
                {
                    this.form.TorihikisakiName1.Text = string.Empty;
                    this.form.TorihikisakiName2.Text = string.Empty;
                    this.form.KyotenCode.Text = string.Empty;
                    this.form.KyotenName.Text = string.Empty;
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;

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
                        // 取引先に移動
                        this.form.TorihikisakiCode.Focus();
                        // 非活性になっていた場合だけ活性化する。
                        if (!this.form.SeikyuushoSoufusaki1.Enabled)
                        {
                            this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu, true);
                        }
                        if (!this.form.ShiharaiSoufuName1.Enabled)
                        {
                            this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Siharai, true);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.form.KyotenCode.Text))
                {
                    this.kyotenEntity = null;
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.form.KyotenCode.Text);
                    count = this.kyotenEntity == null ? 0 : 1;

                    // 拠点名セットを行う
                    if (count > 0)
                    {
                        this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LogicalDelete", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// ポップアップ用部署情報取得メソッド
        /// </summary>
        /// <param name="eigyouTantouCd"></param>
        public bool SetBushoData(string eigyouTantouCd)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart(eigyouTantouCd);

                M_SHAIN condition = new M_SHAIN();
                condition.SHAIN_CD = eigyouTantouCd;
                if (!string.IsNullOrWhiteSpace(this.form.EigyouTantouBushoCode.Text))
                {
                    condition.BUSHO_CD = this.form.EigyouTantouBushoCode.Text;
                }
                DataTable dt = this.daoHikiaiGenba.GetPopupData(condition);
                if (0 < dt.Rows.Count)
                {
                    this.form.EigyouTantouBushoCode.Text = dt.Rows[0]["BUSHO_CD"].ToString();
                    this.form.EigyouTantouBushoName.Text = dt.Rows[0]["BUSHO_NAME"].ToString();
                    this.form.EigyouCode.Text = dt.Rows[0]["SHAIN_CD"].ToString();
                    this.form.EigyouName.Text = dt.Rows[0]["SHAIN_NAME"].ToString();
                }
                else
                {
                    this.form.EigyouCode.BackColor = Constans.ERROR_COLOR;
                    this.form.EigyouCode.ForeColor = Constans.ERROR_COLOR_FORE;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.form.EigyouCode.IsInputErrorOccured = true;
                    this.isError = true;
                    ret = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetBushoData", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBushoData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 現場CD採番処理
        /// </summary>
        /// <returns>最大CD+1</returns>
        public bool Saiban()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 現場マスタのCDの最大値+1を取得
                HikiaiGenbaMasterAccess genbaMasterAccess = new HikiaiGenbaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyGenba = -1;
                bool keyGenbasaibanFlag;

                // 採番処理
                keyGenbasaibanFlag = genbaMasterAccess.IsOverCDLimit(this.GyoushaCd, this.form.HIKIAI_GYOUSHA_USE_FLG.Text, out keyGenba);

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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Saiban", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.gyoushaEntity = null;

                if (!this.HikiaiGyoushaUseFlg)
                {
                    this.gyoushaEntity = daoGyousha.GetDataByCd(this.GyoushaCd);
                }

                count = this.gyoushaEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(引合業者)
        /// </summary>
        /// <returns></returns>
        public int SearchHikiaiGyousha()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.hikiaiGyoushaEntity = null;

                if (this.HikiaiGyoushaUseFlg)
                {
                    this.hikiaiGyoushaEntity = daoHikiaiGyousha.GetDataByCd(this.GyoushaCd);
                }

                count = this.gyoushaEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.torihikisakiEntity = null;

                if (!this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                {
                    this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.genbaEntity.TORIHIKISAKI_CD);
                }

                count = this.torihikisakiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisaki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(引合取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchHikiaiTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.hikiaiTorihikisakiEntity = null;

                if (this.genbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                {
                    this.hikiaiTorihikisakiEntity = daoHikiaiTorihikisaki.GetDataByCd(this.genbaEntity.TORIHIKISAKI_CD);
                }

                count = this.hikiaiTorihikisakiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHikiaiTorihikisaki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(拠点)
        /// </summary>
        /// <returns></returns>
        public int SearchKyoten()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.kyotenEntity = null;

                if (this.torihikisakiEntity != null && !this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                }

                count = this.kyotenEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKyoten", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(部署)
        /// </summary>
        /// <returns></returns>
        public int SearchBusho()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.bushoEntity = null;

                this.bushoEntity = daoBusho.GetDataByCd(this.genbaEntity.EIGYOU_TANTOU_BUSHO_CD);

                count = this.bushoEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchBusho", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(社員)
        /// </summary>
        /// <returns></returns>
        public int SearchShain()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.shainEntity = null;

                this.shainEntity = daoShain.GetDataByCd(this.genbaEntity.EIGYOU_TANTOU_CD);

                count = this.shainEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchShain", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(都道府県)
        /// </summary>
        /// <returns></returns>
        public int SearchTodoufuken()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.todoufukenEntity = null;

                if (!this.genbaEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.genbaEntity.GENBA_TODOUFUKEN_CD.Value.ToString());
                }
                else
                {
                    this.todoufukenEntity = null;
                }

                count = this.todoufukenEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTodoufuken", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(地域)
        /// </summary>
        /// <returns></returns>
        public int SearchChiiki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.chiikiEntity = null;

                this.chiikiEntity = daoChiiki.GetDataByCd(this.genbaEntity.CHIIKI_CD);

                count = this.chiikiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchChiiki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(集計項目)
        /// </summary>
        /// <returns></returns>
        public int SearchShuukeiItem()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.shuukeiEntity = null;

                this.shuukeiEntity = daoShuukei.GetDataByCd(this.genbaEntity.SHUUKEI_ITEM_CD);

                count = this.shuukeiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchShuukeiItem", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(業種)
        /// </summary>
        /// <returns></returns>
        public int SearchGyoushu()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.gyoushuEntity = null;

                this.gyoushuEntity = daoGyoushu.GetDataByCd(this.genbaEntity.GYOUSHU_CD);

                count = this.gyoushuEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(マニ種類)
        /// </summary>
        /// <returns></returns>
        public int SearchManiShurui()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.manishuruiEntity = null;

                if (!this.genbaEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.manishuruiEntity = daoManishurui.GetDataByCd(this.genbaEntity.MANIFEST_SHURUI_CD.ToString());
                }

                count = this.manishuruiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchManiShurui", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(マニ手配)
        /// </summary>
        /// <returns></returns>
        public int SearchManiTehai()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.manitehaiEntity = null;

                if (!this.genbaEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.manitehaiEntity = daoManitehai.GetDataByCd(this.genbaEntity.MANIFEST_TEHAI_CD.ToString());
                }

                count = this.manitehaiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchManiTehai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(定期)
        /// </summary>
        /// <returns></returns>
        public int SearchTeiki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.TeikiHinmeiTable = new DataTable();

                string gyoushaCd = this.genbaEntity.GYOUSHA_CD;
                string genbaCd = this.genbaEntity.GENBA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    return 0;
                }
                Boolean hikiaiGyoshaUseflag = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;

                M_HIKIAI_GENBA_TEIKI_HINMEI condition = new M_HIKIAI_GENBA_TEIKI_HINMEI();
                condition.GYOUSHA_CD = gyoushaCd;
                condition.GENBA_CD = genbaCd;
                condition.HIKIAI_GYOUSHA_USE_FLG = hikiaiGyoshaUseflag;
                this.TeikiHinmeiTable = daoGenbaTeiki.GetTeikiHinmeiData(condition);

                count = this.TeikiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTeiki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(月極)
        /// </summary>
        /// <returns></returns>
        public int SearchTsuki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.TsukiHinmeiTable = new DataTable();

                string gyoushaCd = this.genbaEntity.GYOUSHA_CD;
                string genbaCd = this.genbaEntity.GENBA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    return 0;
                }
                Boolean hikiaiGyoshaUseflag = this.genbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;

                M_HIKIAI_GENBA_TSUKI_HINMEI condition = new M_HIKIAI_GENBA_TSUKI_HINMEI();
                condition.GYOUSHA_CD = gyoushaCd;
                condition.GENBA_CD = genbaCd;
                condition.HIKIAI_GYOUSHA_USE_FLG = hikiaiGyoshaUseflag;
                this.TsukiHinmeiTable = daoGenbaTsuki.GetTsukiHinmeiData(condition);

                count = this.TsukiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTsuki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// マニフェスト返送先取引先コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiTorihikisaki(string Cd, CancelEventArgs e, string hensouCd)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);
                // コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                // タブ内(A票～E票)のコントロールに紐付ける
                // テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);
                // 引合ユーザフラグ
                MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensouCd);

                if (this.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_TORIHIKISAKI condition = new M_HIKIAI_TORIHIKISAKI();
                    condition.TORIHIKISAKI_CD = Cd;
                    condition.MANI_HENSOUSAKI_KBN = true;
                    M_HIKIAI_TORIHIKISAKI[] tori = this.daoHikiaiTorihikisaki.GetAllValidDataMinCols(condition);
                    if (tori != null && 0 < tori.Length)
                    {
                        ManiHensousakiTorihikisakiName.Text = tori[0].TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        ManiHensousakiTorihikisakiName.Text = string.Empty;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "引合取引先");
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            this.ManiHensousakiTorihikisakiCode.Focus();
                        }
                        this.isError = true;
                    }
                }
                else
                {
                    M_TORIHIKISAKI condition = new M_TORIHIKISAKI();
                    condition.TORIHIKISAKI_CD = Cd;
                    condition.MANI_HENSOUSAKI_KBN = true;
                    M_TORIHIKISAKI[] tori = this.daoTorihikisaki.GetAllValidData(condition);
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
                            this.ManiHensousakiTorihikisakiCode.Focus();
                        }
                        this.isError = true;
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetManiHensousakiTorihikisaki", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManiHensousakiTorihikisaki", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// マニフェスト返送先業者コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiGyousha(string Cd, CancelEventArgs e, string hensouCd)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);
                // コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                // タブ内(A票～E票)のコントロールに紐付ける
                // テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);
                // 引合ユーザフラグ
                MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensouCd);

                if (this.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_GYOUSHA condition = new M_HIKIAI_GYOUSHA();
                    condition.GYOUSHA_CD = Cd;
                    if ("3" != HensousakiKbn.Text)
                    {
                        condition.MANI_HENSOUSAKI_KBN = true;
                    }
                    M_HIKIAI_GYOUSHA[] gyousha = this.daoHikiaiGyousha.GetAllValidData(condition);
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
                        msgLogic.MessageBoxShow("E020", "引合業者");
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            this.ManiHensousakiGyoushaCode.Focus();
                        }
                        this.isError = true;
                    }
                }
                else
                {
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
                    }
                    else
                    {
                        ManiHensousakiGyoushaName.Text = string.Empty;
                        ManiHensousakiGenbaName.Text = string.Empty;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            this.ManiHensousakiGyoushaCode.Focus();
                        }
                        this.isError = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetManiHensousakiGyousha", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManiHensousakiGyousha", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// マニフェスト返送先現場コード検索セット
        /// </summary>
        /// <param name="Cd"></param>
        public bool SetManiHensousakiGenba(string Cd, CancelEventArgs e, string hensouCd)
        {
            bool result = false;
            try
            {
                LogUtility.DebugMethodStart(Cd, e, hensouCd);
                ManiGenbaCd = string.Empty; // No3521

                // コントロール操作クラスのオブジェクト
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.form.FindForm().Controls;

                // タブ内(A票～E票)のコントロールに紐付ける
                // テキストボックス
                HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
                ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
                ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
                ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
                ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
                ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
                ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);
                // 引合ユーザフラグ
                MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG" + hensouCd);
                MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = (CustomTextBox)controlUtil.GetSettingField("MANI_HENSOUSAKI_GENBA_HIKIAI_FLG" + hensouCd);

                // 業者が入力されていないとき
                if (string.IsNullOrWhiteSpace(this.ManiGyoushaCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E012", "業者CD");
                    ManiHensousakiGyoushaCode.Focus();

                    this.isError = true;

                    LogUtility.DebugMethodEnd(result);
                    return result;
                }

                if (this.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_GENBA condition = new M_HIKIAI_GENBA();
                    condition.GENBA_CD = Cd;
                    condition.GYOUSHA_CD = this.ManiGyoushaCd;
                    condition.MANI_HENSOUSAKI_KBN = true;
                    M_HIKIAI_GENBA[] genba = this.daoHikiaiGenba.GetAllValidDataMinCols(condition);
                    if (genba != null && genba.Length > 0)
                    {
                        ManiHensousakiGenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                        // No3521-->
                        ManiGenbaCd = Cd;
                        // No3521<--
                        result = true;
                    }
                    else
                    {
                        ManiHensousakiGenbaName.Text = string.Empty;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "引合現場");
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            this.ManiHensousakiGenbaCode.Focus();
                        }
                        this.isError = true;
                    }
                }
                else
                {
                    M_GENBA condition = new M_GENBA();
                    condition.GENBA_CD = Cd;
                    condition.GYOUSHA_CD = this.ManiGyoushaCd;
                    condition.MANI_HENSOUSAKI_KBN = true;
                    M_GENBA[] genba = DaoInitUtility.GetComponent<IM_GENBADao>().GetAllValidData(condition);
                    if (genba != null && genba.Length > 0)
                    {
                        ManiHensousakiGenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                        // No3521-->
                        ManiGenbaCd = Cd;
                        // No3521<--
                        result = true;
                    }
                    else
                    {
                        ManiHensousakiGenbaName.Text = string.Empty;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "現場");
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            this.ManiHensousakiGenbaCode.Focus();
                        }
                        this.isError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManiHensousakiGenba", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        /// <summary>
        /// 検索結果を定期一覧に設定
        /// </summary>
        internal void SetIchiranTeiki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var table = this.TeikiHinmeiTable;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].AllowDBNull = true;
                    table.Columns[i].ReadOnly = false;
                    table.Columns[i].Unique = false;
                }

                this.form.TeikiHinmeiIchiran.DataSource = table;
                this.SetIchiranTeikiRowControl();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTeiki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期一覧の行内容による制御を実施する
        /// </summary>
        public void SetIchiranTeikiRowControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

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

                    if (!isKake)
                    {
                        var keiyakuCell = row.Cells[ConstCls.TEIKI_KEIYAKU_KBN];
                        var keijyoCell = row.Cells[ConstCls.TEIKI_KEIYAKU_KBN];
                        if (Convert.ToString(keiyakuCell.Value) == "1")
                        {
                            row.Cells[ConstCls.TEIKI_KEIYAKU_KBN].Value = DBNull.Value;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                        }
                        if (Convert.ToString(keijyoCell.Value) == "2")
                        {
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value)) && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        row.Cells[Const.ConstCls.TEIKI_HINMEI_CD].ReadOnly = true;
                        row.Cells[Const.ConstCls.TEIKI_UNIT_CD].ReadOnly = true;
                        row.Cells[Const.ConstCls.TEIKI_UNIT_NAME_RYAKU].ReadOnly = true;
                        row.Cells[Const.ConstCls.TEIKI_HINMEI_CD].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells[Const.ConstCls.TEIKI_UNIT_CD].Style.BackColor = Constans.READONLY_COLOR;
                        row.Cells[Const.ConstCls.TEIKI_UNIT_NAME_RYAKU].Style.BackColor = Constans.READONLY_COLOR;
                    }
                    CellEventArgs ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.ConstCls.TEIKI_UNIT_CD].Index, Const.ConstCls.TEIKI_UNIT_CD);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.ConstCls.TEIKI_KEIYAKU_KBN].Index, Const.ConstCls.TEIKI_KEIYAKU_KBN);
                    this.TeikiHinmeiCellValueChanged(ea);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.ConstCls.TEIKI_KEIJYOU_KBN].Index, Const.ConstCls.TEIKI_KEIJYOU_KBN);
                    this.TeikiHinmeiCellValidated(ea);
                    ea = new CellEventArgs(row.Index, this.form.TeikiHinmeiIchiran.Columns[Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Index, Const.ConstCls.TEIKI_KANSAN_UNIT_CD);
                    this.TeikiHinmeiCellValidated(ea);

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
                    row.Cells[ConstCls.TEIKI_BEFORE_HINMEI_CD].Value = row.Cells[ConstCls.TEIKI_HINMEI_CD].Value;
                }
                this.form.TeikiHinmeiIchiran.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTeikiRowControl", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索結果を月極一覧に設定
        /// </summary>
        internal void SetIchiranTsuki()
        {
            try
            {
                LogUtility.DebugMethodStart();

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
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTsuki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 委託計約種類、ステータス変換処理
        /// </summary>
        /// <param name="e"></param>
        public bool ChangeItakuStatus(GrapeCity.Win.MultiRow.CellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

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
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeItakuStatus", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// マニありチェックボックスのON/OFFチェック
        /// </summary>
        /// <param name="clearFlag"></param>
        /// <returns></returns>
        public void ManiCheckOffCheck(bool clearFlag)
        {
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
            try
            {
                LogUtility.DebugMethodStart();
                FlgManiHensousakiKbn = this.form.ManiHensousakiKbn.Checked;

                if (clearFlag)
                {
                    if (!FlgManiHensousakiKbn)
                    {
                        // A票
                        if (this._tabPageManager.IsVisible(6))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }

                        // B1票
                        if (this._tabPageManager.IsVisible(7))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }

                        // B2票
                        if (this._tabPageManager.IsVisible(8))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }

                        // B4票
                        if (this._tabPageManager.IsVisible(9))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }

                        // B6票
                        if (this._tabPageManager.IsVisible(10))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }

                        // C1票
                        if (this._tabPageManager.IsVisible(11))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }

                        // C2票
                        if (this._tabPageManager.IsVisible(12))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        // D票
                        if (this._tabPageManager.IsVisible(13))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }

                        // E票
                        if (this._tabPageManager.IsVisible(14))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = string.Empty;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                    }
                }

                if (FlgManiHensousakiKbn)
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

                    // A票
                    if (this._tabPageManager.IsVisible(6))
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
                                    this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = false;
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
                        }
                    }

                    // B1票
                    if (this._tabPageManager.IsVisible(7))
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
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = false;
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
                        }
                    }

                    // B2票
                    if (this._tabPageManager.IsVisible(8))
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
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = false;
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
                        }
                    }

                    // B4票
                    if (this._tabPageManager.IsVisible(9))
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
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = false;
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
                        }
                    }

                    // B6票
                    if (this._tabPageManager.IsVisible(10))
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
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = false;
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
                        }
                    }

                    // C1票
                    if (this._tabPageManager.IsVisible(11))
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
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = false;
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
                        }
                    }

                    // C2票
                    if (this._tabPageManager.IsVisible(12))
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
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = false;
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
                        }
                    }

                    // D票
                    if (this._tabPageManager.IsVisible(13))
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
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = false;
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
                        }
                    }

                    // E票
                    if (this._tabPageManager.IsVisible(14))
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
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                            this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = false;
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

                    // A票
                    if (this._tabPageManager.IsVisible(6))
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

                    // B1票
                    if (this._tabPageManager.IsVisible(7))
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

                    // B2票
                    if (this._tabPageManager.IsVisible(8))
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

                    // B4票
                    if (this._tabPageManager.IsVisible(9))
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

                    // B6票
                    if (this._tabPageManager.IsVisible(10))
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

                    // C1票
                    if (this._tabPageManager.IsVisible(11))
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

                    // C2票
                    if (this._tabPageManager.IsVisible(12))
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

                    // D票
                    if (this._tabPageManager.IsVisible(13))
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

                    // E票
                    if (this._tabPageManager.IsVisible(14))
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
                // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end

                #region 古いソース

                // FlgManiHensousakiKbn = this.form.ManiHensousakiKbn.Checked;

                // if (clearFlag)
                // {
                // if (!FlgManiHensousakiKbn)
                // {
                // if (this.form.GyoushaKbnMani.Checked)
                // {
                // this.form.HensousakiKbn.Text = "1";
                // }
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;

                // this.form.ManiHensousakiName1.Text = string.Empty;
                // this.form.ManiHensousakiName2.Text = string.Empty;
                // this.form.ManiHensousakiKeishou1.Text = string.Empty;
                // this.form.ManiHensousakiKeishou2.Text = string.Empty;
                // this.form.ManiHensousakiPost.Text = string.Empty;
                // this.form.ManiHensousakiAddress1.Text = string.Empty;
                // this.form.ManiHensousakiAddress2.Text = string.Empty;
                // this.form.ManiHensousakiBusho.Text = string.Empty;
                // this.form.ManiHensousakiTantou.Text = string.Empty;
                // }
                // else
                // {
                // this.form.HensousakiKbn.Text = string.Empty;
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Enabled = false;
                // }
                // }

                // if (FlgManiHensousakiKbn)
                // {
                // // 返送先区分
                // this.form.HensousakiKbn.Enabled = false;
                // this.form.HensousakiKbn1.Enabled = false;
                // this.form.HensousakiKbn1.Checked = false;
                // this.form.HensousakiKbn2.Enabled = false;
                // this.form.HensousakiKbn3.Enabled = false;
                // this.form.HensousakiKbn.Text = string.Empty;
                // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;

                // // 使用可能
                // // this.form.HensousakiKbn.Enabled = true;
                // // this.form.HensousakiKbn1.Enabled = true;
                // // this.form.HensousakiKbn2.Enabled = true;
                // // this.form.HensousakiKbn3.Enabled = true;
                // // this.form.ManiHensousakiTorihikisakiCode.Enabled = true;
                // // this.form.ManiHensousakiGyoushaCode.Enabled = true;
                // // this.form.ManiHensousakiGenbaCode.Enabled = true;

                // this.form.ManiHensousakiName1.Enabled = true;
                // this.form.ManiHensousakiName2.Enabled = true;
                // this.form.ManiHensousakiKeishou1.Enabled = true;
                // this.form.ManiHensousakiKeishou2.Enabled = true;
                // this.form.ManiHensousakiPost.Enabled = true;
                // this.form.ManiHensousakiAddress1.Enabled = true;
                // this.form.ManiHensousakiAddress2.Enabled = true;
                // this.form.ManiHensousakiBusho.Enabled = true;
                // this.form.ManiHensousakiTantou.Enabled = true;
                // // this.form.ManiTorihikisakiCodeSearchButton.Enabled = true;
                // // this.form.ManiGyoushaCodeSearchButton.Enabled = true;
                // // this.form.ManiGenbaCodeSearchButton.Enabled = true;
                // this.form.HensousakiDelete.Enabled = true;
                // this.form.ManiHensousakiAddressSearchButton.Enabled = true;
                // this.form.ManiHensousakiPostSearchButton.Enabled = true;

                // this.form.GENBA_COPY_MANI_BUTTON.Enabled = true;

                // // this.HensousakiRendou();

                // }
                // else
                // {
                // // 返送先区分
                // if (this.form.GyoushaKbnMani.Checked)
                // {
                // this.form.HensousakiKbn.Enabled = true;
                // this.form.HensousakiKbn1.Enabled = true;
                // this.form.HensousakiKbn2.Enabled = true;
                // this.form.HensousakiKbn3.Enabled = true;
                // }
                // else
                // {
                // this.form.HensousakiKbn.Text = string.Empty;
                // this.form.HensousakiKbn.Enabled = false;
                // this.form.HensousakiKbn1.Enabled = false;
                // this.form.HensousakiKbn2.Enabled = false;
                // this.form.HensousakiKbn3.Enabled = false;
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;
                // }

                // // 使用不可
                // // this.form.HensousakiKbn.Enabled = false;
                // // this.form.HensousakiKbn1.Enabled = false;
                // // this.form.HensousakiKbn2.Enabled = false;
                // // this.form.HensousakiKbn3.Enabled = false;
                // // this.form.ManiHensousakiTorihikisakiCode.Enabled = false;
                // // this.form.ManiHensousakiGyoushaCode.Enabled = false;
                // // this.form.ManiHensousakiGenbaCode.Enabled = false;

                // this.form.ManiHensousakiName1.Enabled = false;
                // this.form.ManiHensousakiName2.Enabled = false;
                // this.form.ManiHensousakiKeishou1.Enabled = false;
                // this.form.ManiHensousakiKeishou2.Enabled = false;
                // this.form.ManiHensousakiPost.Enabled = false;
                // this.form.ManiHensousakiAddress1.Enabled = false;
                // this.form.ManiHensousakiAddress2.Enabled = false;
                // this.form.ManiHensousakiBusho.Enabled = false;
                // this.form.ManiHensousakiTantou.Enabled = false;
                // // this.form.ManiTorihikisakiCodeSearchButton.Enabled = false;
                // // this.form.ManiGyoushaCodeSearchButton.Enabled = false;
                // // this.form.ManiGenbaCodeSearchButton.Enabled = false;
                // this.form.HensousakiDelete.Enabled = false;
                // this.form.ManiHensousakiAddressSearchButton.Enabled = false;
                // this.form.ManiHensousakiPostSearchButton.Enabled = false;

                // this.form.GENBA_COPY_MANI_BUTTON.Enabled = false;

                // // テキストクリア
                // /*this.form.HensousakiKbn.Text = "1";
                // this.form.HensousakiKbn1.Checked = true;
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;

                // this.form.ManiHensousakiName1.Text = string.Empty;
                // this.form.ManiHensousakiName2.Text = string.Empty;
                // this.form.ManiHensousakiKeishou1.Text = string.Empty;
                // this.form.ManiHensousakiKeishou2.Text = string.Empty;
                // this.form.ManiHensousakiPost.Text = string.Empty;
                // this.form.ManiHensousakiAddress1.Text = string.Empty;
                // this.form.ManiHensousakiAddress2.Text = string.Empty;
                // this.form.ManiHensousakiBusho.Text = string.Empty;
                // this.form.ManiHensousakiTantou.Text = string.Empty;*/
                // }

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiCheckOffCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを使用区分の選択の状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        internal void SetEnabledManifestHensousakiKbnRendou(string hyoName)
        {
            // コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.form.FindForm().Controls;

            // タブ内(A票～E票)のコントロールに紐付ける
            // ラジオボタン
            HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
            HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
            HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

            // テキストボックス
            HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
            ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
            ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
            ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
            ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
            ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
            ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);

            // 非表示タブは設定なし
            if (HensousakiKbn == null)
            {
                return;
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
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールをマニフェスト返送先のチェックの状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        /// <param name="manifestHensousaki">マニフェスト返送先のチェック状態</param>
        internal void SetEnabledManifestHensousakiRendou(string hyoName, bool manifestHensousaki)
        {
            // コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.form.FindForm().Controls;

            // タブ内(A票～E票)のコントロールに紐付ける
            // ラジオボタン
            MANI_HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
            MANI_HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
            HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
            HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
            HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

            // テキストボックス
            MANI_HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
            HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
            ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
            ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
            ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
            ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
            ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
            ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);

            MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

            // 非表示タブは設定なし
            if (HensousakiKbn == null)
            {
                return;
            }
            else
            {
                if (manifestHensousaki)
                {
                    // 返送先等が使用可
                    MANI_HENSOUSAKI_PLACE_KBN.Enabled = true;
                    MANI_HENSOUSAKI_PLACE_KBN_1.Enabled = true;
                    MANI_HENSOUSAKI_PLACE_KBN_2.Enabled = true;
                    if (MANI_HENSOUSAKI_PLACE_KBN_1.Checked)
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
                        if (this.HensousakiKbn1.Checked)
                        {
                            this.ManiHensousakiTorihikisakiCode.Enabled = true;
                        }
                        if (this.HensousakiKbn2.Checked)
                        {
                            this.ManiHensousakiGyoushaCode.Enabled = true;
                        }
                        if (this.HensousakiKbn3.Checked)
                        {
                            this.ManiHensousakiGyoushaCode.Enabled = true;
                            this.ManiHensousakiGenbaCode.Enabled = true;
                        }
                    }
                }
                else
                {
                    MANI_HENSOUSAKI_PLACE_KBN.Enabled = false;
                    MANI_HENSOUSAKI_PLACE_KBN_1.Enabled = false;
                    MANI_HENSOUSAKI_PLACE_KBN_2.Enabled = false;

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
                            this.ManiHensousakiGyoushaCode.Enabled = true;
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
        }

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを全て使用不可にします
        /// </summary>
        /// <param name="hyoName">票</param>
        internal void SetEnabledFalseManifestHensousaki(string hyoName)
        {
            // コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.form.FindForm().Controls;

            // タブ内(A票～E票)のコントロールに紐付ける
            // ラジオボタン
            MANI_HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
            MANI_HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
            HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
            HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
            HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

            // テキストボックス
            MANI_HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
            HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
            ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
            ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
            ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
            ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
            ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
            ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);

            // 非表示タブは設定なし
            if (HensousakiKbn == null)
            {
                return;
            }
            else
            {
                if (this.form.ManiHensousakiKbn.Checked)
                {
                    MANI_HENSOUSAKI_PLACE_KBN.Text = "1";
                    MANI_HENSOUSAKI_PLACE_KBN_1.Checked = true;
                    MANI_HENSOUSAKI_PLACE_KBN_2.Checked = false;
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

                MANI_HENSOUSAKI_PLACE_KBN.Enabled = false;
                MANI_HENSOUSAKI_PLACE_KBN_1.Enabled = false;
                MANI_HENSOUSAKI_PLACE_KBN_2.Enabled = false;
                this.HensousakiKbn.Enabled = false;
                this.HensousakiKbn1.Enabled = false;
                this.HensousakiKbn2.Enabled = false;
                this.HensousakiKbn3.Enabled = false;
                this.ManiHensousakiTorihikisakiCode.Enabled = false;
                this.ManiHensousakiGyoushaCode.Enabled = false;
                this.ManiHensousakiGenbaCode.Enabled = false;
            }
        }

        /// <summary>
        /// 返送先削除ボタン押下時の処理
        /// </summary>
        /// <returns></returns>
        public void HensousakiDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //// テキスト情報クリア
                // this.form.ManiHensousakiName1.Text = string.Empty;
                // this.form.ManiHensousakiName2.Text = string.Empty;
                // this.form.ManiHensousakiKeishou1.Text = string.Empty;
                // this.form.ManiHensousakiKeishou2.Text = string.Empty;
                // this.form.ManiHensousakiPost.Text = string.Empty;
                // this.form.ManiHensousakiAddress1.Text = string.Empty;
                // this.form.ManiHensousakiAddress2.Text = string.Empty;
                // this.form.ManiHensousakiBusho.Text = string.Empty;
                // this.form.ManiHensousakiTantou.Text = string.Empty;

                //// マニフェスト返送先にチェックがついていない場合は区分もクリア
                // if (!FlgManiHensousakiKbn)
                // {
                // this.form.HensousakiKbn.Text = "1";
                // this.form.HensousakiKbn1.Checked = true;
                // this.form.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                // this.form.ManiHensousakiGyoushaCode.Text = string.Empty;
                // this.form.ManiHensousakiGenbaCode.Text = string.Empty;
                // }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HensousakiDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// サーチ
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;

            try
            {
                LogUtility.DebugMethodStart();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        /// <summary>
        /// 業者情報コピー処理
        /// </summary>
        public void GyousyaCopy()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string inputCd = this.GyoushaCd;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "業者CD");
                    return;
                }
                else
                {
                    string zeroPadCd = this.ZeroPadding(inputCd);
                    this.GyousyaSetting(zeroPadCd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyousyaCopy", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された業者CD</param>
        private void GyousyaSetting(string inputCd)
        {
            try
            {
                LogUtility.DebugMethodStart(inputCd);

                M_GYOUSHA gyousyaEntity = this.daoGyousha.GetDataByCd(inputCd);
                M_HIKIAI_GYOUSHA hikiGyousyaEntity = this.daoHikiaiGyousha.GetDataByCd(inputCd);

                #region 業者テーブルから取得する場合

                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("0") && gyousyaEntity != null)
                {
                    // 共通部分
                    if (!gyousyaEntity.KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = gyousyaEntity.KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(gyousyaEntity.KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    this.form.GenbaName1.Text = gyousyaEntity.GYOUSHA_NAME1; // 取引先名１
                    this.form.GenbaName2.Text = gyousyaEntity.GYOUSHA_NAME2; // 取引先名２

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

                    this.form.GenbaKeishou1.Text = gyousyaEntity.GYOUSHA_KEISHOU1; // 敬称１
                    this.form.GenbaKeishou2.Text = gyousyaEntity.GYOUSHA_KEISHOU2; // 敬称２

                    this.form.GenbaFurigana.Text = gyousyaEntity.GYOUSHA_FURIGANA; // フリガナ(不要な可能性有)
                    this.form.GenbaNameRyaku.Text = gyousyaEntity.GYOUSHA_NAME_RYAKU; // 略称(不要な可能性有)

                    this.form.GenbaTel.Text = gyousyaEntity.GYOUSHA_TEL; // 電話
                    this.form.GenbaFax.Text = gyousyaEntity.GYOUSHA_FAX; // FAX
                    this.form.GenbaKeitaiTel.Text = gyousyaEntity.GYOUSHA_KEITAI_TEL; // 携帯
                    this.form.EigyouTantouBushoCode.Text = gyousyaEntity.EIGYOU_TANTOU_BUSHO_CD; // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(gyousyaEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EigyouTantouBushoCode.Text = string.Empty;
                        this.form.EigyouTantouBushoName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU; // 営業担当部署名
                    }
                    this.form.EigyouCode.Text = gyousyaEntity.EIGYOU_TANTOU_CD; // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(gyousyaEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EigyouCode.Text = string.Empty;
                        this.form.EigyouName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU; // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GenbaPost.Text = gyousyaEntity.GYOUSHA_POST; // 郵便番号

                    if (!gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GenbaTodoufukenCode.Text = this.ZeroPaddingKen(gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());// 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(gyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GenbaTodoufukenCode.Text = string.Empty;
                            this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                        }
                        else
                        {
                            this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME; // 都道府県名
                        }
                    }

                    this.form.GenbaAddress1.Text = gyousyaEntity.GYOUSHA_ADDRESS1; // 住所１
                    this.form.GenbaAddress2.Text = gyousyaEntity.GYOUSHA_ADDRESS2; // 住所２

                    // 地域取得
                    string chiikiName = string.Empty;
                    string chiikiCd = gyousyaEntity.CHIIKI_CD;
                    var m_chiikiCd = daoChiiki.GetDataByCd(chiikiCd);
                    if (m_chiikiCd != null)
                    {
                        chiikiName = m_chiikiCd.CHIIKI_NAME;
                    }

                    if (!string.IsNullOrWhiteSpace(chiikiCd))
                    {
                        this.form.ChiikiCode.Text = chiikiCd; // 地域CD
                        this.form.ChiikiName.Text = chiikiName; // 地域名
                    }

                    // 運搬報告書提出先
                    chiikiName = string.Empty;
                    chiikiCd = gyousyaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
                    m_chiikiCd = daoChiiki.GetDataByCd(chiikiCd);
                    if (m_chiikiCd != null)
                    {
                        chiikiName = m_chiikiCd.CHIIKI_NAME;
                    }

                    if (!string.IsNullOrWhiteSpace(chiikiCd))
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = chiikiCd; // 運搬報告書提出先CD
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = chiikiName; // 運搬報告書提出先名
                    }

                    this.form.BushoCode.Text = gyousyaEntity.BUSHO; // 部署
                    this.form.TantoushaCode.Text = gyousyaEntity.TANTOUSHA; // 担当者
                    this.form.ShuukeiItemCode.Text = gyousyaEntity.SHUUKEI_ITEM_CD; // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(gyousyaEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.ShuukeiItemCode.Text = string.Empty;
                        this.form.ShuukeiItemName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU; // 集計項目名
                    }
                    this.form.GyoushuCode.Text = gyousyaEntity.GYOUSHU_CD; // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(gyousyaEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GyoushuCode.Text = string.Empty;
                        this.form.GyoushuName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU; // 業種名
                    }
                    this.form.Bikou1.Text = gyousyaEntity.BIKOU1; // 備考１
                    this.form.Bikou2.Text = gyousyaEntity.BIKOU2; // 備考２
                    this.form.Bikou3.Text = gyousyaEntity.BIKOU3; // 備考３
                    this.form.Bikou4.Text = gyousyaEntity.BIKOU4; // 備考４

                    // 現場分類タブ
                    this.form.HaishutsuKbn.Checked = gyousyaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue;
                    this.form.ShobunJigyoujouKbn.Checked = gyousyaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                    this.form.ManiHensousakiKbn.Checked = gyousyaEntity.MANI_HENSOUSAKI_KBN.IsTrue; // マニフェスト返送先

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
                        if (this._tabPageManager.IsVisible(6))
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

                        if (this._tabPageManager.IsVisible(7))
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
                        if (this._tabPageManager.IsVisible(8))
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
                        if (this._tabPageManager.IsVisible(9))
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
                        if (this._tabPageManager.IsVisible(10))
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
                        if (this._tabPageManager.IsVisible(11))
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
                        if (this._tabPageManager.IsVisible(12))
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
                        if (this._tabPageManager.IsVisible(13))
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
                        if (this._tabPageManager.IsVisible(14))
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
                        if (this._tabPageManager.IsVisible(6))
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(7))
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(8))
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(9))
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(10))
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(11))
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(12))
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(13))
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(14))
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        }
                    }

                    // 請求情報タブ
                    this.form.SeikyuushoSoufusaki1.Text = gyousyaEntity.SEIKYUU_SOUFU_NAME1; // 請求書送付先1
                    this.form.SeikyuuSouhuKeishou1.Text = gyousyaEntity.SEIKYUU_SOUFU_KEISHOU1; // 請求書送付先敬称1
                    this.form.SeikyuushoSoufusaki2.Text = gyousyaEntity.SEIKYUU_SOUFU_NAME2; // 請求書送付先2
                    this.form.SeikyuuSouhuKeishou2.Text = gyousyaEntity.SEIKYUU_SOUFU_KEISHOU2; // 請求書送付先敬称2
                    this.form.SeikyuuSoufuPost.Text = gyousyaEntity.SEIKYUU_SOUFU_POST; // 送付先郵便番号
                    this.form.SeikyuuSoufuAddress1.Text = gyousyaEntity.SEIKYUU_SOUFU_ADDRESS1; // 送付先住所１
                    this.form.SeikyuuSoufuAddress2.Text = gyousyaEntity.SEIKYUU_SOUFU_ADDRESS2; // 送付先住所２
                    this.form.SeikyuuSoufuBusho.Text = gyousyaEntity.SEIKYUU_SOUFU_BUSHO; // 送付先部署
                    this.form.SeikyuuSoufuTantou.Text = gyousyaEntity.SEIKYUU_SOUFU_TANTOU; // 送付先担当者
                    this.form.SoufuGenbaTel.Text = gyousyaEntity.SEIKYUU_SOUFU_TEL; // 送付先電話番号
                    this.form.SoufuGenbaFax.Text = gyousyaEntity.SEIKYUU_SOUFU_FAX; // 送付先FAX番号
                    this.form.SeikyuuTantou.Text = gyousyaEntity.SEIKYUU_TANTOU; // 請求担当者

                    if (!gyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SeikyuuDaihyouPrintKbn.Text = gyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString(); // 代表取締役
                    }

                    if (!gyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = gyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();// 拠点印字区分
                    }

                    if (!gyousyaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = gyousyaEntity.SEIKYUU_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(gyousyaEntity.SEIKYUU_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                        }
                        else
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    //this.form.HAKKOUSAKI_CD.Text = gyousyaEntity.HAKKOUSAKI_CD;
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end

                    // 支払情報タブ
                    this.form.ShiharaiSoufuName1.Text = gyousyaEntity.SHIHARAI_SOUFU_NAME1;		                    // 支払明細書送付先1
                    this.form.ShiharaiSoufuKeishou1.Text = gyousyaEntity.SHIHARAI_SOUFU_KEISHOU1;	                // 支払明細書送付先敬称1
                    this.form.ShiharaiSoufuName2.Text = gyousyaEntity.SHIHARAI_SOUFU_NAME2;		                    // 支払明細書送付先2
                    this.form.ShiharaiSoufuKeishou2.Text = gyousyaEntity.SHIHARAI_SOUFU_KEISHOU2;	                // 支払明細書送付先敬称2
                    this.form.ShiharaiSoufuPost.Text = gyousyaEntity.SHIHARAI_SOUFU_POST;		                    // 送付先郵便番号
                    this.form.ShiharaiSoufuAddress1.Text = gyousyaEntity.SHIHARAI_SOUFU_ADDRESS1;	                // 送付先住所１
                    this.form.ShiharaiSoufuAddress2.Text = gyousyaEntity.SHIHARAI_SOUFU_ADDRESS2;	                // 送付先住所２
                    this.form.ShiharaiSoufuBusho.Text = gyousyaEntity.SHIHARAI_SOUFU_BUSHO;		                    // 送付先部署
                    this.form.ShiharaiSoufuTantou.Text = gyousyaEntity.SHIHARAI_SOUFU_TANTOU;		                // 送付先担当者
                    this.form.ShiharaiGenbaTel.Text = gyousyaEntity.SHIHARAI_SOUFU_TEL;			                    // 送付先電話番号
                    this.form.ShiharaiGenbaFax.Text = gyousyaEntity.SHIHARAI_SOUFU_FAX;			                    // 送付先FAX番号

                    if (!gyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = gyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); // 拠点印字区分
                    }

                    if (!gyousyaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = gyousyaEntity.SHIHARAI_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(gyousyaEntity.SHIHARAI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                        }
                        else
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }
                }

                #endregion

                #region 引合業者テーブルから取得する場合

                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1") && hikiGyousyaEntity != null)
                {
                    // 共通部分
                    if (!hikiGyousyaEntity.KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = hikiGyousyaEntity.KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiGyousyaEntity.KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    this.form.GenbaName1.Text = hikiGyousyaEntity.GYOUSHA_NAME1; // 取引先名１
                    this.form.GenbaName2.Text = hikiGyousyaEntity.GYOUSHA_NAME2; // 取引先名２

                    if (hikiGyousyaEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TekiyouKikanForm.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanForm.Value = hikiGyousyaEntity.TEKIYOU_BEGIN;
                    }
                    if (hikiGyousyaEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TekiyouKikanTo.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanTo.Value = hikiGyousyaEntity.TEKIYOU_END;
                    }

                    this.form.ChuusiRiyuu1.Text = hikiGyousyaEntity.CHUUSHI_RIYUU1;
                    this.form.ChuusiRiyuu2.Text = hikiGyousyaEntity.CHUUSHI_RIYUU2;
                    this.form.ShokuchiKbn.Checked = hikiGyousyaEntity.SHOKUCHI_KBN.IsTrue;
                    this.form.JishaKbn.Checked = hikiGyousyaEntity.JISHA_KBN.IsTrue;

                    this.form.GenbaKeishou1.Text = hikiGyousyaEntity.GYOUSHA_KEISHOU1; // 敬称１
                    this.form.GenbaKeishou2.Text = hikiGyousyaEntity.GYOUSHA_KEISHOU2; // 敬称２

                    this.form.GenbaFurigana.Text = hikiGyousyaEntity.GYOUSHA_FURIGANA; // フリガナ(不要な可能性有)
                    this.form.GenbaNameRyaku.Text = hikiGyousyaEntity.GYOUSHA_NAME_RYAKU; // 略称(不要な可能性有)

                    this.form.GenbaTel.Text = hikiGyousyaEntity.GYOUSHA_TEL; // 電話
                    this.form.GenbaFax.Text = hikiGyousyaEntity.GYOUSHA_FAX; // FAX
                    this.form.GenbaKeitaiTel.Text = hikiGyousyaEntity.GYOUSHA_KEITAI_TEL; // 携帯
                    this.form.EigyouTantouBushoCode.Text = hikiGyousyaEntity.EIGYOU_TANTOU_BUSHO_CD; // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(hikiGyousyaEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EigyouTantouBushoCode.Text = string.Empty;
                        this.form.EigyouTantouBushoName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU; // 営業担当部署名
                    }
                    this.form.EigyouCode.Text = hikiGyousyaEntity.EIGYOU_TANTOU_CD; // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(hikiGyousyaEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EigyouCode.Text = string.Empty;
                        this.form.EigyouName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU; // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GenbaPost.Text = hikiGyousyaEntity.GYOUSHA_POST; // 郵便番号

                    if (!hikiGyousyaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GenbaTodoufukenCode.Text = this.ZeroPaddingKen(hikiGyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString()); // 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(hikiGyousyaEntity.GYOUSHA_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GenbaTodoufukenCode.Text = string.Empty;
                            this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                        }
                        else
                        {
                            this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME; // 都道府県名
                        }
                    }

                    this.form.GenbaAddress1.Text = hikiGyousyaEntity.GYOUSHA_ADDRESS1; // 住所１
                    this.form.GenbaAddress2.Text = hikiGyousyaEntity.GYOUSHA_ADDRESS2; // 住所２

                    // 地域取得
                    string chiikiName = string.Empty;
                    string chiikiCd = hikiGyousyaEntity.CHIIKI_CD;
                    var m_chiikiCd = daoChiiki.GetDataByCd(chiikiCd);
                    if (m_chiikiCd != null)
                    {
                        chiikiName = m_chiikiCd.CHIIKI_NAME;
                    }

                    if (!string.IsNullOrWhiteSpace(chiikiCd))
                    {
                        this.form.ChiikiCode.Text = chiikiCd; // 地域CD
                        this.form.ChiikiName.Text = chiikiName; // 地域名
                    }

                    this.form.BushoCode.Text = hikiGyousyaEntity.BUSHO; // 部署
                    this.form.TantoushaCode.Text = hikiGyousyaEntity.TANTOUSHA; // 担当者
                    this.form.ShuukeiItemCode.Text = hikiGyousyaEntity.SHUUKEI_ITEM_CD; // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(hikiGyousyaEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.ShuukeiItemCode.Text = string.Empty;
                        this.form.ShuukeiItemName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU; // 集計項目名
                    }
                    this.form.GyoushuCode.Text = hikiGyousyaEntity.GYOUSHU_CD; // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(hikiGyousyaEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GyoushuCode.Text = string.Empty;
                        this.form.GyoushuName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU; // 業種名
                    }
                    this.form.Bikou1.Text = hikiGyousyaEntity.BIKOU1; // 備考１
                    this.form.Bikou2.Text = hikiGyousyaEntity.BIKOU2; // 備考２
                    this.form.Bikou3.Text = hikiGyousyaEntity.BIKOU3; // 備考３
                    this.form.Bikou4.Text = hikiGyousyaEntity.BIKOU4; // 備考４

                    // 現場分類タブ
                    this.form.HaishutsuKbn.Checked = hikiGyousyaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue;
                    this.form.ShobunJigyoujouKbn.Checked = hikiGyousyaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue;
                    this.form.ManiHensousakiKbn.Checked = hikiGyousyaEntity.MANI_HENSOUSAKI_KBN.IsTrue; // マニフェスト返送先

                    if (hikiGyousyaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    }
                    else if (hikiGyousyaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
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
                        this.form.ManiHensousakiName1.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_NAME1;
                        this.form.ManiHensousakiName2.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_NAME2;
                        this.form.ManiHensousakiKeishou1.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.ManiHensousakiKeishou2.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.ManiHensousakiPost.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_POST;
                        this.form.ManiHensousakiAddress1.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.ManiHensousakiAddress2.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.ManiHensousakiBusho.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_BUSHO;
                        this.form.ManiHensousakiTantou.Text = hikiGyousyaEntity.MANI_HENSOUSAKI_TANTOU;
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
                        if (this._tabPageManager.IsVisible(6))
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

                        if (this._tabPageManager.IsVisible(7))
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
                        if (this._tabPageManager.IsVisible(8))
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
                        if (this._tabPageManager.IsVisible(9))
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
                        if (this._tabPageManager.IsVisible(10))
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
                        if (this._tabPageManager.IsVisible(11))
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
                        if (this._tabPageManager.IsVisible(12))
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
                        if (this._tabPageManager.IsVisible(13))
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
                        if (this._tabPageManager.IsVisible(14))
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
                        if (this._tabPageManager.IsVisible(6))
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(7))
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(8))
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(9))
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(10))
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(11))
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(12))
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(13))
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(14))
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        }
                    }

                    // 請求情報タブ
                    this.form.SeikyuushoSoufusaki1.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_NAME1; // 請求書送付先1
                    this.form.SeikyuuSouhuKeishou1.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_KEISHOU1; // 請求書送付先敬称1
                    this.form.SeikyuushoSoufusaki2.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_NAME2; // 請求書送付先2
                    this.form.SeikyuuSouhuKeishou2.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_KEISHOU2; // 請求書送付先敬称2
                    this.form.SeikyuuSoufuPost.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_POST; // 送付先郵便番号
                    this.form.SeikyuuSoufuAddress1.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_ADDRESS1; // 送付先住所１
                    this.form.SeikyuuSoufuAddress2.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_ADDRESS2; // 送付先住所２
                    this.form.SeikyuuSoufuBusho.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_BUSHO; // 送付先部署
                    this.form.SeikyuuSoufuTantou.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_TANTOU; // 送付先担当者
                    this.form.SoufuGenbaTel.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_TEL; // 送付先電話番号
                    this.form.SoufuGenbaFax.Text = hikiGyousyaEntity.SEIKYUU_SOUFU_FAX; // 送付先FAX番号
                    this.form.SeikyuuTantou.Text = hikiGyousyaEntity.SEIKYUU_TANTOU; // 請求担当者

                    if (!hikiGyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SeikyuuDaihyouPrintKbn.Text = hikiGyousyaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString(); // 代表取締役
                    }

                    if (!hikiGyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = hikiGyousyaEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();// 拠点印字区分
                    }

                    if (!hikiGyousyaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = hikiGyousyaEntity.SEIKYUU_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiGyousyaEntity.SEIKYUU_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                        }
                        else
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    //this.form.HAKKOUSAKI_CD.Text = hikiGyousyaEntity.HAKKOUSAKI_CD;
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end

                    // 支払情報タブ
                    this.form.ShiharaiSoufuName1.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_NAME1;		                // 支払明細書送付先1
                    this.form.ShiharaiSoufuKeishou1.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_KEISHOU1;	            // 支払明細書送付先敬称1
                    this.form.ShiharaiSoufuName2.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_NAME2;		                // 支払明細書送付先2
                    this.form.ShiharaiSoufuKeishou2.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_KEISHOU2;	            // 支払明細書送付先敬称2
                    this.form.ShiharaiSoufuPost.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_POST;		                // 送付先郵便番号
                    this.form.ShiharaiSoufuAddress1.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_ADDRESS1;	            // 送付先住所１
                    this.form.ShiharaiSoufuAddress2.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_ADDRESS2;	            // 送付先住所２
                    this.form.ShiharaiSoufuBusho.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_BUSHO;		                // 送付先部署
                    this.form.ShiharaiSoufuTantou.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_TANTOU;		            // 送付先担当者
                    this.form.ShiharaiGenbaTel.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_TEL;			                // 送付先電話番号
                    this.form.ShiharaiGenbaFax.Text = hikiGyousyaEntity.SHIHARAI_SOUFU_FAX;			                // 送付先FAX番号

                    if (!hikiGyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = hikiGyousyaEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString();// 拠点印字区分
                    }

                    if (!hikiGyousyaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = hikiGyousyaEntity.SHIHARAI_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiGyousyaEntity.SHIHARAI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                        }
                        else
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先情報コピー処理
        /// </summary>
        public bool TorihikisakiCopy()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                string inputCd = this.TorihikisakiCd;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "取引先CD");
                    ret = false;
                }
                else
                {
                    string zeroPadCd = this.ZeroPadding(inputCd);
                    this.TorihikisakiSetting(zeroPadCd);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TorihikisakiCopy", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCopy", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 取引先コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        private void TorihikisakiSetting(string inputCd)
        {
            try
            {
                LogUtility.DebugMethodStart(inputCd);

                M_TORIHIKISAKI torisakiEntity = this.daoTorihikisaki.GetDataByCd(inputCd);
                M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(inputCd);
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI hikiTorisakiEntity = this.daoHikiaiTorihikisaki.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI_SHIHARAI hikiShiharaiEntity = this.daoHikiaiShiharai.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI_SEIKYUU hikiSeikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(inputCd);

                #region 取引先テーブルから取得する場合

                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0") && torisakiEntity != null)
                {
                    // 共通部分
                    if (!torisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    this.form.GenbaName1.Text = torisakiEntity.TORIHIKISAKI_NAME1; // 取引先名１
                    this.form.GenbaName2.Text = torisakiEntity.TORIHIKISAKI_NAME2; // 取引先名２

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

                    this.form.GenbaKeishou1.Text = torisakiEntity.TORIHIKISAKI_KEISHOU1; // 敬称１
                    this.form.GenbaKeishou2.Text = torisakiEntity.TORIHIKISAKI_KEISHOU2; // 敬称２

                    this.form.GenbaFurigana.Text = torisakiEntity.TORIHIKISAKI_FURIGANA; // フリガナ(不要な可能性有)
                    this.form.GenbaNameRyaku.Text = torisakiEntity.TORIHIKISAKI_NAME_RYAKU; // 略称(不要な可能性有)

                    this.form.GenbaTel.Text = torisakiEntity.TORIHIKISAKI_TEL; // 電話
                    this.form.GenbaFax.Text = torisakiEntity.TORIHIKISAKI_FAX; // FAX
                    this.form.EigyouTantouBushoCode.Text = torisakiEntity.EIGYOU_TANTOU_BUSHO_CD; // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EigyouTantouBushoCode.Text = string.Empty;
                        this.form.EigyouTantouBushoName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU; // 営業担当部署名
                    }
                    this.form.EigyouCode.Text = torisakiEntity.EIGYOU_TANTOU_CD; // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EigyouCode.Text = string.Empty;
                        this.form.EigyouName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU; // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GenbaPost.Text = torisakiEntity.TORIHIKISAKI_POST; // 郵便番号

                    if (!torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GenbaTodoufukenCode.Text = this.ZeroPaddingKen(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString()); // 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GenbaTodoufukenCode.Text = string.Empty;
                            this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                        }
                        else
                        {
                            this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME; // 都道府県名
                        }
                    }

                    this.form.GenbaAddress1.Text = torisakiEntity.TORIHIKISAKI_ADDRESS1; // 住所１
                    this.form.GenbaAddress2.Text = torisakiEntity.TORIHIKISAKI_ADDRESS2; // 住所２

                    // 地域の判定は関数に任せる
                    if (!this.ChechChiiki(true))
                    {
                        this.form.ChiikiCode.Text = string.Empty;       // 地域CD
                        this.form.ChiikiName.Text = string.Empty;       // 地域名
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;       // 運搬報告書提出先地域CD
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;     // 運搬報告書提出先地域名
                    }

                    this.form.BushoCode.Text = torisakiEntity.BUSHO; // 部署
                    this.form.TantoushaCode.Text = torisakiEntity.TANTOUSHA; // 担当者
                    this.form.ShuukeiItemCode.Text = torisakiEntity.SHUUKEI_ITEM_CD; // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(torisakiEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.ShuukeiItemCode.Text = string.Empty;
                        this.form.ShuukeiItemName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU; // 集計項目名
                    }
                    this.form.GyoushuCode.Text = torisakiEntity.GYOUSHU_CD; // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(torisakiEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GyoushuCode.Text = string.Empty;
                        this.form.GyoushuName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU; // 業種名
                    }
                    this.form.Bikou1.Text = torisakiEntity.BIKOU1; // 備考１
                    this.form.Bikou2.Text = torisakiEntity.BIKOU2; // 備考２
                    this.form.Bikou3.Text = torisakiEntity.BIKOU3; // 備考３
                    this.form.Bikou4.Text = torisakiEntity.BIKOU4; // 備考４

                    // 現場分類タブ
                    this.form.ManiHensousakiKbn.Checked = torisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue; // マニフェスト返送先
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
                        if (this._tabPageManager.IsVisible(6))
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

                        if (this._tabPageManager.IsVisible(7))
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
                        if (this._tabPageManager.IsVisible(8))
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
                        if (this._tabPageManager.IsVisible(9))
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
                        if (this._tabPageManager.IsVisible(10))
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
                        if (this._tabPageManager.IsVisible(11))
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
                        if (this._tabPageManager.IsVisible(12))
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
                        if (this._tabPageManager.IsVisible(13))
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
                        if (this._tabPageManager.IsVisible(14))
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
                        if (this._tabPageManager.IsVisible(6))
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(7))
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(8))
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(9))
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(10))
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(11))
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(12))
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(13))
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(14))
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        }
                    }
                    if (seikyuuEntity != null)
                    {
                        // 請求情報タブ
                        this.form.SeikyuushoSoufusaki1.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME1; // 請求書送付先1
                        this.form.SeikyuuSouhuKeishou1.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU1; // 請求書送付先敬称1
                        this.form.SeikyuushoSoufusaki2.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME2; // 請求書送付先2
                        this.form.SeikyuuSouhuKeishou2.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU2; // 請求書送付先敬称2
                        this.form.SeikyuuSoufuPost.Text = seikyuuEntity.SEIKYUU_SOUFU_POST; // 送付先郵便番号
                        this.form.SeikyuuSoufuAddress1.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS1; // 送付先住所１
                        this.form.SeikyuuSoufuAddress2.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS2; // 送付先住所２
                        this.form.SeikyuuSoufuBusho.Text = seikyuuEntity.SEIKYUU_SOUFU_BUSHO; // 送付先部署
                        this.form.SeikyuuSoufuTantou.Text = seikyuuEntity.SEIKYUU_SOUFU_TANTOU; // 送付先担当者
                        this.form.SoufuGenbaTel.Text = seikyuuEntity.SEIKYUU_SOUFU_TEL; // 送付先電話番号
                        this.form.SoufuGenbaFax.Text = seikyuuEntity.SEIKYUU_SOUFU_FAX; // 送付先FAX番号
                        this.form.SeikyuuTantou.Text = seikyuuEntity.SEIKYUU_TANTOU; // 請求担当者
                        if (!seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SeikyuuDaihyouPrintKbn.Text = seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString(); // 代表取締役を印字
                        }
                        if (!seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString(); // 拠点名を印字
                        }
                        if (!seikyuuEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = seikyuuEntity.SEIKYUU_KYOTEN_CD.ToString(); // 請求書拠点
                            this.SeikyuuKyotenCdValidated();
                        }

                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        //this.form.HAKKOUSAKI_CD.Text = seikyuuEntity.HAKKOUSAKI_CD;
                        // 発行先チェック処理
                        this.HakkousakuCheck();
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    }

                    if (shiharaiEntity != null)
                    {
                        // 支払情報タブ
                        this.form.ShiharaiSoufuName1.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME1;		                    // 支払明細書送付先1
                        this.form.ShiharaiSoufuKeishou1.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU1; 	                // 支払明細書送付先敬称1
                        this.form.ShiharaiSoufuName2.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME2;		                    // 支払明細書送付先2
                        this.form.ShiharaiSoufuKeishou2.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU2;	                    // 支払明細書送付先敬称2
                        this.form.ShiharaiSoufuPost.Text = shiharaiEntity.SHIHARAI_SOUFU_POST;		                        // 送付先郵便番号
                        this.form.ShiharaiSoufuAddress1.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS1;	                    // 送付先住所１
                        this.form.ShiharaiSoufuAddress2.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS2; 	                // 送付先住所２
                        this.form.ShiharaiSoufuBusho.Text = shiharaiEntity.SHIHARAI_SOUFU_BUSHO;		                    // 送付先部署
                        this.form.ShiharaiSoufuTantou.Text = shiharaiEntity.SHIHARAI_SOUFU_TANTOU;		                    // 送付先担当者
                        this.form.ShiharaiGenbaTel.Text = shiharaiEntity.SHIHARAI_SOUFU_TEL;			                    // 送付先電話番号
                        this.form.ShiharaiGenbaFax.Text = shiharaiEntity.SHIHARAI_SOUFU_FAX;			                    // 送付先FAX番号
                        if (!shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); // 拠点名を印字
                        }
                        if (!shiharaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = shiharaiEntity.SHIHARAI_KYOTEN_CD.ToString(); // 支払書拠点
                            this.ShiharaiKyotenCdValidated();
                        }
                    }
                }

                #endregion

                #region 引合業者テーブルから取得する場合

                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1") && hikiTorisakiEntity != null)
                {
                    // 共通部分
                    if (!hikiTorisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KyotenCode.Text = hikiTorisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString(); // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiTorisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KyotenCode.Text = string.Empty;
                            this.form.KyotenName.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU; // 拠点名
                        }
                    }

                    this.form.GenbaName1.Text = hikiTorisakiEntity.TORIHIKISAKI_NAME1; // 取引先名１
                    this.form.GenbaName2.Text = hikiTorisakiEntity.TORIHIKISAKI_NAME2; // 取引先名２

                    if (hikiTorisakiEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TekiyouKikanForm.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanForm.Value = hikiTorisakiEntity.TEKIYOU_BEGIN;
                    }
                    if (hikiTorisakiEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TekiyouKikanTo.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanTo.Value = hikiTorisakiEntity.TEKIYOU_END;
                    }

                    this.form.ChuusiRiyuu1.Text = hikiTorisakiEntity.CHUUSHI_RIYUU1;
                    this.form.ChuusiRiyuu1.Text = hikiTorisakiEntity.CHUUSHI_RIYUU2;
                    this.form.ShokuchiKbn.Checked = hikiTorisakiEntity.SHOKUCHI_KBN.IsTrue;

                    this.form.GenbaKeishou1.Text = hikiTorisakiEntity.TORIHIKISAKI_KEISHOU1; // 敬称１
                    this.form.GenbaKeishou2.Text = hikiTorisakiEntity.TORIHIKISAKI_KEISHOU2; // 敬称２

                    this.form.GenbaFurigana.Text = hikiTorisakiEntity.TORIHIKISAKI_FURIGANA; // フリガナ(不要な可能性有)
                    this.form.GenbaNameRyaku.Text = hikiTorisakiEntity.TORIHIKISAKI_NAME_RYAKU; // 略称(不要な可能性有)

                    this.form.GenbaTel.Text = hikiTorisakiEntity.TORIHIKISAKI_TEL; // 電話
                    this.form.GenbaFax.Text = hikiTorisakiEntity.TORIHIKISAKI_FAX; // FAX
                    this.form.EigyouTantouBushoCode.Text = hikiTorisakiEntity.EIGYOU_TANTOU_BUSHO_CD; // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(hikiTorisakiEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EigyouTantouBushoCode.Text = string.Empty;
                        this.form.EigyouTantouBushoName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouTantouBushoName.Text = busho.BUSHO_NAME_RYAKU; // 営業担当部署名
                    }
                    this.form.EigyouCode.Text = hikiTorisakiEntity.EIGYOU_TANTOU_CD; // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(hikiTorisakiEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EigyouCode.Text = string.Empty;
                        this.form.EigyouName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.EigyouName.Text = shain.SHAIN_NAME_RYAKU; // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GenbaPost.Text = hikiTorisakiEntity.TORIHIKISAKI_POST; // 郵便番号

                    if (!hikiTorisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GenbaTodoufukenCode.Text = this.ZeroPaddingKen(hikiTorisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString()); // 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(hikiTorisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GenbaTodoufukenCode.Text = string.Empty;
                            this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                        }
                        else
                        {
                            this.form.GenbaTodoufukenNameRyaku.Text = temp.TODOUFUKEN_NAME; // 都道府県名
                        }
                    }

                    this.form.GenbaAddress1.Text = hikiTorisakiEntity.TORIHIKISAKI_ADDRESS1; // 住所１
                    this.form.GenbaAddress2.Text = hikiTorisakiEntity.TORIHIKISAKI_ADDRESS2; // 住所２

                    // 地域の判定は関数に任せる
                    if (!this.ChechChiiki(true))
                    {
                        this.form.ChiikiCode.Text = string.Empty;                                                    // 地域CD
                        this.form.ChiikiName.Text = string.Empty;                                                  // 地域名
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;                           // 運搬報告書提出先地域CD
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;                         // 運搬報告書提出先地域名
                    }

                    this.form.BushoCode.Text = hikiTorisakiEntity.BUSHO; // 部署
                    this.form.TantoushaCode.Text = hikiTorisakiEntity.TANTOUSHA; // 担当者
                    this.form.ShuukeiItemCode.Text = hikiTorisakiEntity.SHUUKEI_ITEM_CD; // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(hikiTorisakiEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.ShuukeiItemCode.Text = string.Empty;
                        this.form.ShuukeiItemName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.ShuukeiItemName.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU; // 集計項目名
                    }
                    this.form.GyoushuCode.Text = hikiTorisakiEntity.GYOUSHU_CD; // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(hikiTorisakiEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GyoushuCode.Text = string.Empty;
                        this.form.GyoushuName.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GyoushuName.Text = gyoushu.GYOUSHU_NAME_RYAKU; // 業種名
                    }
                    this.form.Bikou1.Text = hikiTorisakiEntity.BIKOU1; // 備考１
                    this.form.Bikou2.Text = hikiTorisakiEntity.BIKOU2; // 備考２
                    this.form.Bikou3.Text = hikiTorisakiEntity.BIKOU3; // 備考３
                    this.form.Bikou4.Text = hikiTorisakiEntity.BIKOU4; // 備考４

                    // 現場分類タブ
                    this.form.ManiHensousakiKbn.Checked = hikiTorisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue; // マニフェスト返送先
                    if (hikiTorisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    }
                    else if (hikiTorisakiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
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
                        this.form.ManiHensousakiName1.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_NAME1;
                        this.form.ManiHensousakiName2.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_NAME2;
                        this.form.ManiHensousakiKeishou1.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.ManiHensousakiKeishou2.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.ManiHensousakiPost.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_POST;
                        this.form.ManiHensousakiAddress1.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.ManiHensousakiAddress2.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.ManiHensousakiBusho.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_BUSHO;
                        this.form.ManiHensousakiTantou.Text = hikiTorisakiEntity.MANI_HENSOUSAKI_TANTOU;
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
                        if (this._tabPageManager.IsVisible(6))
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

                        if (this._tabPageManager.IsVisible(7))
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
                        if (this._tabPageManager.IsVisible(8))
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
                        if (this._tabPageManager.IsVisible(9))
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
                        if (this._tabPageManager.IsVisible(10))
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
                        if (this._tabPageManager.IsVisible(11))
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
                        if (this._tabPageManager.IsVisible(12))
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
                        if (this._tabPageManager.IsVisible(13))
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
                        if (this._tabPageManager.IsVisible(14))
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
                        if (this._tabPageManager.IsVisible(6))
                        {
                            this.form.MANIFEST_USE_AHyo.Text = "2";
                            this.form.MANIFEST_USE_2_AHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(7))
                        {
                            this.form.MANIFEST_USE_B1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(8))
                        {
                            this.form.MANIFEST_USE_B2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(9))
                        {
                            this.form.MANIFEST_USE_B4Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(10))
                        {
                            this.form.MANIFEST_USE_B6Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(11))
                        {
                            this.form.MANIFEST_USE_C1Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(12))
                        {
                            this.form.MANIFEST_USE_C2Hyo.Text = "2";
                            this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(13))
                        {
                            this.form.MANIFEST_USE_DHyo.Text = "2";
                            this.form.MANIFEST_USE_2_DHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        }

                        if (this._tabPageManager.IsVisible(14))
                        {
                            this.form.MANIFEST_USE_EHyo.Text = "2";
                            this.form.MANIFEST_USE_2_EHyo.Checked = true;
                            this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                            this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        }
                    }

                    if (hikiSeikyuuEntity != null)
                    {
                        // 請求情報タブ
                        this.form.SeikyuushoSoufusaki1.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_NAME1; // 請求書送付先1
                        this.form.SeikyuuSouhuKeishou1.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_KEISHOU1; // 請求書送付先敬称1
                        this.form.SeikyuushoSoufusaki2.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_NAME2; // 請求書送付先2
                        this.form.SeikyuuSouhuKeishou2.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_KEISHOU2; // 請求書送付先敬称2
                        this.form.SeikyuuSoufuPost.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_POST; // 送付先郵便番号
                        this.form.SeikyuuSoufuAddress1.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_ADDRESS1; // 送付先住所１
                        this.form.SeikyuuSoufuAddress2.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_ADDRESS2; // 送付先住所２
                        this.form.SeikyuuSoufuBusho.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_BUSHO; // 送付先部署
                        this.form.SeikyuuSoufuTantou.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_TANTOU; // 送付先担当者
                        this.form.SoufuGenbaTel.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_TEL; // 送付先電話番号
                        this.form.SoufuGenbaFax.Text = hikiSeikyuuEntity.SEIKYUU_SOUFU_FAX; // 送付先FAX番号
                        this.form.SeikyuuTantou.Text = hikiSeikyuuEntity.SEIKYUU_TANTOU; // 請求担当者
                        if (!hikiSeikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SeikyuuDaihyouPrintKbn.Text = hikiSeikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString(); // 代表取締役を印字
                        }
                        if (!hikiSeikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = hikiSeikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString(); // 拠点名を印字
                        }
                        if (!hikiSeikyuuEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = hikiSeikyuuEntity.SEIKYUU_KYOTEN_CD.ToString(); // 請求書拠点
                            this.SeikyuuKyotenCdValidated();
                        }

                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        //this.form.HAKKOUSAKI_CD.Text = hikiSeikyuuEntity.HAKKOUSAKI_CD;
                        // 発行先チェック処理
                        this.HakkousakuCheck();
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    }

                    if (hikiShiharaiEntity != null)
                    {
                        // 支払情報タブ
                        this.form.ShiharaiSoufuName1.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_NAME1;		                    // 支払明細書送付先1
                        this.form.ShiharaiSoufuKeishou1.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_KEISHOU1; 	                // 支払明細書送付先敬称1
                        this.form.ShiharaiSoufuName2.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_NAME2;		                    // 支払明細書送付先2
                        this.form.ShiharaiSoufuKeishou2.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_KEISHOU2;	                    // 支払明細書送付先敬称2
                        this.form.ShiharaiSoufuPost.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_POST;		                        // 送付先郵便番号
                        this.form.ShiharaiSoufuAddress1.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_ADDRESS1;	                    // 送付先住所１
                        this.form.ShiharaiSoufuAddress2.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_ADDRESS2; 	                // 送付先住所２
                        this.form.ShiharaiSoufuBusho.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_BUSHO;		                    // 送付先部署
                        this.form.ShiharaiSoufuTantou.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_TANTOU;		                    // 送付先担当者
                        this.form.ShiharaiGenbaTel.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_TEL;			                    // 送付先電話番号
                        this.form.ShiharaiGenbaFax.Text = hikiShiharaiEntity.SHIHARAI_SOUFU_FAX;			                    // 送付先FAX番号
                        if (!hikiShiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = hikiShiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); // 拠点名を印字
                        }
                        if (!hikiShiharaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = hikiShiharaiEntity.SHIHARAI_KYOTEN_CD.ToString(); // 支払書拠点
                            this.ShiharaiKyotenCdValidated();
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiSetting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = sityousonBypost;

                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);

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
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenBypost;
                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }

            // 都道府県＋市区町村＋OTHER1
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
                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
        /// 地域CD検索処理
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_HIKIAI_TORIHIKISAKI torisakiEntity, out string chiikiName)
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
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = sityousonBypost;

                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);

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
                // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                condition.CHIIKI_NAME = todoufukenBypost;
                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }

            // 都道府県＋市区町村＋OTHER1
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
                dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
        private string GetChiikiCd(M_GYOUSHA gyousyaEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(gyousyaEntity, chiikiName);

                // ①M_GYOUSHA.GYOUSHA_POST, M_GYOUSHA.GYOUSHA_ADDRESS1が空の場合は空を設定し、処理を終了する。
                string tempPost = gyousyaEntity.GYOUSHA_POST;
                string tempAddress = gyousyaEntity.GYOUSHA_ADDRESS1;
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
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = sityousonBypost;
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = todoufukenBypost;
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                        dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                        if (dtChiiki.Rows.Count == 1)
                        {
                            chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                            return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                        }
                    }
                }

                // 都道府県＋市区町村＋OTHER1
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
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetChiikiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, chiikiName);
            }

            // ⑧⑥または⑦の結果が複数件ある場合、空を設定し、処理を終了する。
            // ⑨⑤の検索結果が0件の場合、空を設定し、処理を終了する。
            return result;
        }

        /// <summary>
        /// 地域CD検索処理 引合業者用
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_HIKIAI_GYOUSHA hikiGyousyaEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(hikiGyousyaEntity, chiikiName);

                // ①GYOUSHA_POST, GYOUSHA_ADDRESS1が空の場合は空を設定し、処理を終了する。
                string tempPost = hikiGyousyaEntity.GYOUSHA_POST;
                string tempAddress = hikiGyousyaEntity.GYOUSHA_ADDRESS1;
                if (string.IsNullOrWhiteSpace(tempPost) && string.IsNullOrWhiteSpace(tempAddress))
                {
                    return result;
                }

                // ②GYOUSHA_POSTで郵便辞書を検索し、都道府県と市町村を取得する。
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
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = sityousonBypost;
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                    // 結果が1件であれば取得したM_CHIIKI.CHIIKI_CDを設定し、処理を終了する。
                    condition.CHIIKI_NAME = todoufukenBypost;
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
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
                        dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                        if (dtChiiki.Rows.Count == 1)
                        {
                            chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                            return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                        }
                    }
                }

                // 都道府県＋市区町村＋OTHER1
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
                    dtChiiki = daoHikiaiGenba.GetChiikiData(condition);
                    if (dtChiiki.Rows.Count == 1)
                    {
                        chiikiName = dtChiiki.Rows[0]["CHIIKI_NAME"].ToString();
                        return dtChiiki.Rows[0]["CHIIKI_CD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetChiikiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, chiikiName);
            }

            // ⑧⑥または⑦の結果が複数件ある場合、空を設定し、処理を終了する。
            // ⑨⑤の検索結果が0件の場合、空を設定し、処理を終了する。
            return result;
        }

        /// <summary>
        /// 業者入力表示処理
        /// </summary>
        internal void ShowGyoushaCreate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M462", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowGyoushaCreate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先入力表示処理
        /// </summary>
        internal void ShowTorihikisakiCreate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M461", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowTorihikisakiCreate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        internal void ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M486", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.HIKIAI_GENBA);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // <summary>
        // 現場の情報を請求にコピーする
        // </summary>
        public void CopyToSeikyu()
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSeikyu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // <summary>
        // 現場の情報を支払いのコピーする
        // </summary>
        public void CopyToSiharai()
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToSiharai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // <summary>
        // 現場の情報を分類にコピーする
        // </summary>
        public void CopyToMani()
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyToMani", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CustomTextBoxの入力文字数をチェックします
        /// </summary>
        /// <param name="txb">CustomTextBox</param>
        /// <returns>true:制限以内, false:制限超過</returns>
        public bool CheckTextBoxLength(CustomTextBox txb)
        {
            LogUtility.DebugMethodStart(txb);

            bool res = true;

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
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CheckTextBoxLength", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                res = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
            return res;
        }

        /// <summary>
        /// 地域判定処理
        /// </summary>
        /// <param name="isUnpanHoukokusyoChange">運搬報告書提出先CDを変更するかを示します</param>
        public bool ChechChiiki(bool isUnpanHoukokusyoChange)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 共通地域チェックロジックで地域検索を実行する
                M_CHIIKI chiiki = this.SearchChiikiFromAddress(this.form.GenbaTodoufukenCode.Text, this.form.GenbaAddress1.Text);
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
                    M_CHIIKI houkokuChiiki = this.SearchChiikiFromAddress(this.form.GenbaTodoufukenCode.Text, string.Empty);
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChechChiiki", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechChiiki", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 住所から地域を検索する
        /// </summary>
        /// <param name="todoufukenCd"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private M_CHIIKI SearchChiikiFromAddress(string todoufukenCd, string address)
        {
            M_CHIIKI chiiki = null;
            M_TODOUFUKEN todoufuken = null;

            try
            {
                LogUtility.DebugMethodStart(todoufukenCd, address);

                string chiikiCode = string.Empty;

                // 都道府県コードが入力されている場合、一旦都道府県を地域とする
                if (!string.IsNullOrWhiteSpace(todoufukenCd))
                {
                    chiikiCode = todoufukenCd.PadLeft(6, '0');
                    todoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>().GetDataByCd(chiikiCode);
                }

                // 住所1が入力されている場合、最初の市区町村で区切って、その市区町村が地域にあるかチェックする
                if (!string.IsNullOrWhiteSpace(address))
                {
                    string addr = address;

                    // 都道府県を取得し、除去
                    if (todoufuken != null)
                    {
                        addr = Regex.Replace(addr, todoufuken.TODOUFUKEN_NAME_RYAKU, "");
                    }
                    else
                    {
                        if (Regex.Match(addr, ".{2,3}?[都道府県]").Length > 0)
                        {
                            string tmpAddr = "";
                            tmpAddr = Regex.Match(addr, ".{2,3}?[都道府県]").Value;
                            //"("が残っていると後続の正規表現でエラーするため置換する。他の正規表現文字列も同様かもしれないが、"("だけとりあえず対応。
                            tmpAddr = tmpAddr.Replace("(", "");

                            todoufuken = new M_TODOUFUKEN();
                            todoufuken.TODOUFUKEN_NAME_RYAKU = tmpAddr;
                        }
                    }

                    // 市区を取得する
                    MatchCollection shikuArray;

                    // 市区を元に地域マスタをチェックする
                    // ※都道府県文字以前の除去前でチェック
                    shikuArray = Regex.Matches(addr, ".*?[市区]");
                    M_CHIIKI cond = new M_CHIIKI();
                    cond.CHIIKI_NAME = string.Empty;
                    for (int i = 0; i < shikuArray.Count; i++)
                    {
                        cond.CHIIKI_NAME += shikuArray[i].Value.ToString();
                        M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                        if (chiikiArray != null && chiikiArray.Length > 0)
                        {
                            // 最初に検索できた時点でループは終了する
                            chiiki = chiikiArray[0];
                            break;
                        }
                    }

                    // 市区を元に地域マスタをチェックする
                    // ※都道府県文字以前の除去後でチェック
                    if (chiiki == null && todoufuken != null && todoufuken.TODOUFUKEN_NAME_RYAKU != null && !string.IsNullOrWhiteSpace(todoufuken.TODOUFUKEN_NAME_RYAKU))
                    {
                        addr = Regex.Replace(addr, todoufuken.TODOUFUKEN_NAME_RYAKU, "");
                        shikuArray = Regex.Matches(addr, ".*?[市区]");
                        cond.CHIIKI_NAME = string.Empty;
                        for (int i = 0; i < shikuArray.Count; i++)
                        {
                            cond.CHIIKI_NAME += shikuArray[i].Value.ToString();
                            M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                            if (chiikiArray != null && chiikiArray.Length > 0)
                            {
                                // 最初に検索できた時点でループは終了する
                                chiiki = chiikiArray[0];
                                break;
                            }
                        }
                    }

                    // 都道府県名から地域を検索する
                    if (chiiki == null && todoufuken != null && todoufuken.TODOUFUKEN_NAME_RYAKU != null && !string.IsNullOrWhiteSpace(todoufuken.TODOUFUKEN_NAME_RYAKU))
                    {
                        cond.CHIIKI_NAME = todoufuken.TODOUFUKEN_NAME_RYAKU;
                        M_CHIIKI[] chiikiArray = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllValidData(cond);
                        if (chiikiArray != null && chiikiArray.Length > 0)
                        {
                            // 最初に検索できた時点でループは終了する
                            chiiki = chiikiArray[0];
                        }
                    }
                }

                // 地域が検索されておらず、地域コードが設定されている場合、地域マスタをチェック
                // ※都道府県が対象となる場合が該当
                if (chiiki == null && !string.IsNullOrWhiteSpace(chiikiCode))
                {
                    chiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetDataByCd(chiikiCode);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechChiiki", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(chiiki);
            }

            return chiiki;
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        public void ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType torihikisakiKbnProcess, Boolean isKake)
        {
            try
            {
                LogUtility.DebugMethodStart(torihikisakiKbnProcess, isKake);

                if (torihikisakiKbnProcess == Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu)
                {
                    this.ChangeSeikyuuControl(isKake);
                }
                else if (torihikisakiKbnProcess == Const.ConstCls.TorihikisakiKbnProcessType.Siharai)
                {
                    this.ChangeSiharaiControl(isKake);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikisakiKbn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(請求)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSeikyuuControl(Boolean isKake)
        {
            try
            {
                LogUtility.DebugMethodStart(isKake);

                this.form.SeikyuushoSoufusaki1.Text = string.Empty; // 請求書送付先1
                this.form.SeikyuushoSoufusaki1.Enabled = isKake;

                this.form.SeikyuuSouhuKeishou1.Text = string.Empty; // 請求書送付先敬称1
                this.form.SeikyuuSouhuKeishou1.Enabled = isKake;

                this.form.SeikyuushoSoufusaki2.Text = string.Empty; // 請求書送付先2
                this.form.SeikyuushoSoufusaki2.Enabled = isKake;

                this.form.SeikyuuSouhuKeishou2.Text = string.Empty; // 請求書送付先敬称2
                this.form.SeikyuuSouhuKeishou2.Enabled = isKake;

                this.form.SeikyuuSoufuPost.Text = string.Empty; // 送付先郵便番号
                this.form.SeikyuuSoufuPost.Enabled = isKake;

                this.form.SeikyuuSoufuAddress1.Text = string.Empty; // 送付先住所１
                this.form.SeikyuuSoufuAddress1.Enabled = isKake;

                this.form.SeikyuuSoufuAddress2.Text = string.Empty; // 送付先住所２
                this.form.SeikyuuSoufuAddress2.Enabled = isKake;

                this.form.SeikyuuSoufuBusho.Text = string.Empty; // 送付先部署
                this.form.SeikyuuSoufuBusho.Enabled = isKake;

                this.form.SeikyuuSoufuTantou.Text = string.Empty; // 送付先担当者
                this.form.SeikyuuSoufuTantou.Enabled = isKake;

                this.form.SoufuGenbaTel.Text = string.Empty; // 送付先電話番号
                this.form.SoufuGenbaTel.Enabled = isKake;

                this.form.SoufuGenbaFax.Text = string.Empty; // 送付先FAX番号
                this.form.SoufuGenbaFax.Enabled = isKake;

                this.form.SeikyuuTantou.Text = string.Empty; // 請求担当者
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
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull && isKake != this.form.SEIKYUU_KYOTEN_CD.Enabled)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()));
                }

                M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (this.form.SEIKYUU_KYOTEN_CD.Text != string.Empty)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }

                this.ChangeSeikyuuKyotenPrintKbn();

                this.form.GENBA_COPY_SEIKYU_BUTTON.Enabled = isKake; // 請求書業者情報コピー
                this.form.SeikyuuSoufuAddressSearchButton.Enabled = isKake; // 請求書住所参照
                this.form.SeikyuuSoufuPostSearchButton.Enabled = isKake; // 請求書郵便番号参照

                // 定期回収タブ
                this.ChangeTeikiKaishuuControl(isKake);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuControl", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(支払)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSiharaiControl(Boolean isKake)
        {
            try
            {
                LogUtility.DebugMethodStart(isKake);

                this.form.ShiharaiSoufuName1.Text = string.Empty; // 支払明細書送付先1
                this.form.ShiharaiSoufuName1.Enabled = isKake;

                this.form.ShiharaiSoufuKeishou1.Text = string.Empty; // 支払明細書送付先敬称1
                this.form.ShiharaiSoufuKeishou1.Enabled = isKake;

                this.form.ShiharaiSoufuName2.Text = string.Empty; // 支払明細書送付先2
                this.form.ShiharaiSoufuName2.Enabled = isKake;

                this.form.ShiharaiSoufuKeishou2.Text = string.Empty; // 支払明細書送付先敬称2
                this.form.ShiharaiSoufuKeishou2.Enabled = isKake;

                this.form.ShiharaiSoufuPost.Text = string.Empty; // 送付先郵便番号
                this.form.ShiharaiSoufuPost.Enabled = isKake;

                this.form.ShiharaiSoufuAddress1.Text = string.Empty; // 送付先住所１
                this.form.ShiharaiSoufuAddress1.Enabled = isKake;

                this.form.ShiharaiSoufuAddress2.Text = string.Empty; // 送付先住所２
                this.form.ShiharaiSoufuAddress2.Enabled = isKake;

                this.form.ShiharaiSoufuBusho.Text = string.Empty; // 送付先部署
                this.form.ShiharaiSoufuBusho.Enabled = isKake;

                this.form.ShiharaiSoufuTantou.Text = string.Empty; // 送付先担当者
                this.form.ShiharaiSoufuTantou.Enabled = isKake;

                this.form.ShiharaiGenbaTel.Text = string.Empty; // 送付先電話番号
                this.form.ShiharaiGenbaTel.Enabled = isKake;

                this.form.ShiharaiGenbaFax.Text = string.Empty; // 送付先FAX番号
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
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.IsNull && isKake != this.form.SHIHARAI_KYOTEN_CD.Enabled && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(String.Empty))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()));
                }

                M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (this.form.SHIHARAI_KYOTEN_CD.Text != string.Empty)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }

                this.ChangeShiharaiKyotenPrintKbn();

                this.form.GENBA_COPY_SIHARAI_BUTTON.Enabled = isKake; // 支払書業者情報コピー
                this.form.ShiharaiSoufuAddressSearchButton.Enabled = isKake; // 支払書住所参照
                this.form.ShiharaiSoufuPostSearchButton.Enabled = isKake; // 支払書郵便番号参照
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSiharaiControl", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        public bool ChangeSeikyuuKyotenPrintKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        // システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SEIKYUU_KYOTEN_CD.ToString()));
                        SeikyuuKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(定期回収情報タブ)
        /// </summary>
        /// <param name="isKake">取引先請求区分が[1.現金]時に true</param>
        private void ChangeTeikiKaishuuControl(Boolean isKake)
        {
            var teikiHinmeiDataTable = this.isSettingWindowData ? this.TeikiHinmeiTable : this.form.TeikiHinmeiIchiran.DataSource as DataTable;
            //var teikiHinmeiDetail = this.form.TeikiHinmeiIchiran.Template;
            //var templateChangeFlg = false;

            //// 定期回収情報のテンプレートが設定されていない場合、何もしない
            //if (teikiHinmeiDetail == null)
            //{
            //    return;
            //}

            //teikiHinmeiDetail = teikiHinmeiDetail.Clone();

            //if (!isKake)
            //{
            //    // 取引先区分が現金の際（実績売上支払確定画面に表示されない為）
            //    // 契約区分：定期を選択できないようにする。
            //    var teikiKeiyakuKbnCell = teikiHinmeiDetail.Row[Const.ConstCls.TEIKI_KEIYAKU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeiyakuKbnCell != null && !teikiKeiyakuKbnCell.CharacterLimitList.Equals(new char[] { '2', '3' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeiyakuKbnCell.CharacterLimitList = new char[] { '2', '3' };
            //        teikiKeiyakuKbnCell.Tag = "【2、3】のいずれかで入力してください";
            //    }

            //    // 集計単位：合算を選択できないようにする。
            //    var teikiKeijyouKbnCell = teikiHinmeiDetail.Row[Const.ConstCls.TEIKI_KEIJYOU_KBN] as GcCustomNumericTextBox2Cell;
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
            //    var teikiKeiyakuKbnCell = teikiHinmeiDetail.Row[Const.ConstCls.TEIKI_KEIYAKU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeiyakuKbnCell != null && !teikiKeiyakuKbnCell.CharacterLimitList.Equals(new char[] { '1', '2', '3' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeiyakuKbnCell.CharacterLimitList = new char[] { '1', '2', '3' };
            //        teikiKeiyakuKbnCell.Tag = "【1～3】のいずれかで入力してください";
            //    }

            //    // 集計単位：合算を選択可能にする。
            //    var teikiKeijyouKbnCell = teikiHinmeiDetail.Row[Const.ConstCls.TEIKI_KEIJYOU_KBN] as GcCustomNumericTextBox2Cell;
            //    if (teikiKeijyouKbnCell != null && !teikiKeijyouKbnCell.CharacterLimitList.Equals(new char[] { '1', '2' }))
            //    {
            //        templateChangeFlg = true;
            //        teikiKeijyouKbnCell.CharacterLimitList = new char[] { '1', '2' };
            //        teikiKeijyouKbnCell.Tag = "【1、2】のいずれかで入力してください";
            //    }
            //}

            // テンプレートが変更された場合
            // テンプレート設定/データソース設定/制限関連メソッド実行
            //if (templateChangeFlg)
            //{
                //this.form.TeikiHinmeiIchiran.Template = teikiHinmeiDetail;
                this.TeikiHinmeiTable = teikiHinmeiDataTable;
                this.SetIchiranTeiki();
            //}
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        public bool ChangeShiharaiKyotenPrintKbn()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        // システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GENBA_SHIHARAI_KYOTEN_CD.ToString()));
                        ShiharaiKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // No2267-->
        /// <summary>
        /// アラートの内容の重複確認クリア
        /// </summary>
        public void errorMessagesClear()
        {
            messageList.Clear();
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
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageUtility msgUtil = new MessageUtility();

                //// チェックボックスコントロールチェック
                // if (sender is CustomCheckBox)
                // {
                // CustomCheckBox item = sender as CustomCheckBox;
                // string controlName = item.Name;

                // if ((item.Name.Equals("HaishutsuKbn") && this.form.HaishutsuKbn.Enabled)
                // || (item.Name.Equals("TsumikaeHokanKbn") && !this.form.HaishutsuKbn.Enabled && this.form.TsumikaeHokanKbn.Enabled)
                // || (item.Name.Equals("ShobunJigyoujouKbn") && !this.form.HaishutsuKbn.Enabled && !this.form.TsumikaeHokanKbn.Enabled && this.form.ShobunJigyoujouKbn.Enabled))
                // {
                // if (item.Name.Equals("HaishutsuKbn")
                // || item.Name.Equals("TsumikaeHokanKbn")
                // || item.Name.Equals("ShobunJigyoujouKbn")
                // || item.Name.Equals("SaishuuShobunjouKbn"))
                // {
                // // 業者分類の必須チェック
                // if (this.form.GyoushaKbnMani.Checked
                // && (!this.form.HaishutsuKbn.Checked
                // && !this.form.TsumikaeHokanKbn.Checked
                // && !this.form.ShobunJigyoujouKbn.Checked
                // && !this.form.SaishuuShobunjouKbn.Checked))
                // {
                // e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "排出事業場、積み替え保管、処分事業場、最終処分場のいずれか"));
                // }
                // }
                // }
                // }

                // 通常コントロールチェック
                if (sender is CustomTextBox)
                {
                    CustomTextBox item = sender as CustomTextBox;

                    // マニありのチェックが1つでもついている場合必須チェックする。
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
                                item.IsInputErrorOccured = true;
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

                        if (item.Name.Equals(Const.ConstCls.TEIKI_HINMEI_CD))
                        {
                            Row row = this.form.TeikiHinmeiIchiran.Rows[rowIdx];

                            // 一旦、エラー表示を解除する
                            row[Const.ConstCls.TEIKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_MONDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_TUESDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_WEDNESDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_THURSDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_FRIDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_SATURDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_SUNDAY].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Style.BackColor = Constans.NOMAL_COLOR;
                            if (row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled)
                            {
                                row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            }
                            else
                            {
                                row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = true;
                                row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            }
                            row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = true;
                            row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            
                            // 何か入力されている行のみチェックする
                            // 削除チェックされている行はチェックしない
                            var teikiDeleteFlg = row["DELETE_FLG"].Value as bool?;
                            if (
                                !(teikiDeleteFlg.HasValue && teikiDeleteFlg.Value) &&
                                (
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KANSANCHI].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_MONDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_TUESDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_WEDNESDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_THURSDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_FRIDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_SATURDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TEIKI_SUNDAY].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Value)
                                )
                            )
                            {
                                GcCustomCheckBoxCell mon = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_MONDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell tue = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_TUESDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell wed = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_WEDNESDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell thu = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_THURSDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell fri = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_FRIDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell sat = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_SATURDAY] as GcCustomCheckBoxCell;
                                GcCustomCheckBoxCell sun = this.form.TeikiHinmeiIchiran[item.RowIndex, Const.ConstCls.TEIKI_SUNDAY] as GcCustomCheckBoxCell;

                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_HINMEI_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "品名CD"));
                                    row[Const.ConstCls.TEIKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_UNIT_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単位"));
                                    row[Const.ConstCls.TEIKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                else if (!this.ValueIsKgUnitCD(row[Const.ConstCls.TEIKI_UNIT_CD].Value) && !this.ValueIsKgUnitCD(row[Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value))
                                {
                                    var strWithoutKgUnitErrMsg = string.Format(msgUtil.GetMessage("W004").MESSAGE);
                                    if (!e.errorMessages.Contains(strWithoutKgUnitErrMsg))
                                    {
                                        e.errorMessages.Add(strWithoutKgUnitErrMsg);
                                    }
                                    row[Const.ConstCls.TEIKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
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
                                    row[Const.ConstCls.TEIKI_MONDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_TUESDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_WEDNESDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_THURSDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_FRIDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_SATURDAY].Style.BackColor = Constans.ERROR_COLOR;
                                    row[Const.ConstCls.TEIKI_SUNDAY].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "契約区分"));
                                    row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                else if (row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("1"))
                                {
                                    if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value))
                                    {
                                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "月極品名"));
                                        row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                    }
                                }
                                else if (row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("2"))
                                {
                                    if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Value))
                                    {
                                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "集計単位"));
                                        row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Style.BackColor = Constans.ERROR_COLOR;
                                        row[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = true;
                                    }
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

                        if (item.Name.Equals(Const.ConstCls.TSUKI_HINMEI_CD))
                        {
                            Row row = this.form.TsukiHinmeiIchiran.Rows[rowIdx];

                            // 一旦、エラー表示を解除する
                            row[Const.ConstCls.TSUKI_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TSUKI_UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                            row[Const.ConstCls.TSUKI_TANKA].Style.BackColor = Constans.NOMAL_COLOR;

                            // 何か入力されている行のみチェックする
                            // 削除チェックされている行はチェックしない
                            var tsukiDeleteFlg = row["DELETE_FLG"].Value as bool?;
                            if (
                                !(tsukiDeleteFlg.HasValue && tsukiDeleteFlg.Value) &&
                                (
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_UNIT_CD].Value) ||
                                    !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_TANKA].Value) ||
                                    !this.ValueIsNullOrDBNullOrFalse(row[Const.ConstCls.TSUKI_TEIKI_JISSEKI_NO_SEIKYUU_KBN].Value)
                                )
                            )
                            {
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "品名CD"));
                                    row[Const.ConstCls.TSUKI_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_UNIT_CD].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単位"));
                                    row[Const.ConstCls.TSUKI_UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                                }
                                if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_TANKA].Value))
                                {
                                    e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, "単価"));
                                    row[Const.ConstCls.TSUKI_TANKA].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }
                    }
                }

                // No2267-->
                this.errorMessagesUniq(e);
                // No2267<--
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegist", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
                // 業者分類の必須チェック
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
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.CellName.Equals(Const.ConstCls.TEIKI_TSUKI_HINMEI_CD))
                {
                    List<string> tsukiHinmei = new List<string>();
                    tsukiHinmei.Add(" ");
                    foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                    {
                        if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value))
                        {
                            continue;
                        }
                        tsukiHinmei.Add(row[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString());
                    }
                    ((GcCustomAlphaNumTextBoxCell)this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex]).PopupSearchSendParams[0].Value = string.Join(",", tsukiHinmei.ToArray());
                }
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIYAKU_KBN))
                {
                    GcCustomNumericTextBox2Cell cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    cell.PopupTitleLabel = "契約区分";
                    cell.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
                    cell.PopupGetMasterField = "CD,VALUE";
                    cell.PopupSetFormField = "KEIYAKU_KBN,KEIYAKU_KBN_NAME";
                    cell.PopupDataHeaderTitle = new string[] { "契約区分CD", "契約区分" };
                    DataTable dtKeiyaku = new DataTable();
                    dtKeiyaku.Columns.Add("CD", typeof(string));
                    dtKeiyaku.Columns.Add("VALUE", typeof(string));
                    dtKeiyaku.Columns[0].ReadOnly = true;
                    dtKeiyaku.Columns[1].ReadOnly = true;
                    DataRow row;
                    
                    row = dtKeiyaku.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "定期";
                    dtKeiyaku.Rows.Add(row);
                    row = dtKeiyaku.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "単価";
                    dtKeiyaku.Rows.Add(row);
                    row = dtKeiyaku.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = "回収のみ";
                    dtKeiyaku.Rows.Add(row);
                    dtKeiyaku.TableName = "契約区分";
                    cell.PopupDataSource = dtKeiyaku;
                }
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIJYOU_KBN))
                {
                    GcCustomNumericTextBox2Cell cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    cell.PopupTitleLabel = "集計単位";
                    cell.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
                    cell.PopupGetMasterField = "CD,VALUE";
                    cell.PopupSetFormField = "KEIJYOU_KBN,KEIJYOU_KBN_NAME";
                    cell.PopupDataHeaderTitle = new string[] { "集計単位CD", "集計単位" };
                    DataTable dtKeijou = new DataTable();
                    dtKeijou.Columns.Add("CD", typeof(string));
                    dtKeijou.Columns.Add("VALUE", typeof(string));
                    dtKeijou.Columns[0].ReadOnly = true;
                    dtKeijou.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dtKeijou.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "伝票";
                    dtKeijou.Rows.Add(row);
                    // 請求情報の取引先区分が現金の場合、[集計単位]の[合算]は選択不可とする
                    if (this.IsSeikyuuKake())
                    {
                        row = dtKeijou.NewRow();
                        row["CD"] = "2";
                        row["VALUE"] = "合算";
                        dtKeijou.Rows.Add(row);
                    }
                    dtKeijou.TableName = "集計単位";
                    cell.PopupDataSource = dtKeijou;
                }

                // 換算後単位モバイル出力フラグフォーカス時処理
                if (e.CellName.Equals(Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG))
                {
                    // 換算後単位が入力されていない場合、次コントロールをフォーカス
                    if (!this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                    {
                        if (this.prevTeikiCellName.Equals(Const.ConstCls.TEIKI_KANSAN_UNIT_CD))
                        {
                            // 20140718 katen No.5292 引合現場入力-定期回収情報タブに実数項目が無い start‏
                            // this.form.TeikiHinmeiIchiran.CurrentCellPosition = new CellPosition(e.RowIndex, Const.ConstCls.TEIKI_MONDAY);
                            this.form.TeikiHinmeiIchiran.CurrentCellPosition = new CellPosition(e.RowIndex, Const.ConstCls.TEIKI_ANBUN_FLG);
                            // 20140718 katen No.5292 引合現場入力-定期回収情報タブに実数項目が無い end‏
                        }
                        else
                        {
                            this.form.TeikiHinmeiIchiran.CurrentCellPosition = new CellPosition(e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellEnter", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 定期品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellFormatting(CellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                // LogUtility.DebugMethodStart(e);

                // 換算値の表示書式設定を行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KANSANCHI))
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = string.Format("{0:0.000}", e.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellFormat", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                // LogUtility.DebugMethodEnd();
            }
            return ret;
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValidated(CellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                // 前回セル名保存
                this.prevTeikiCellName = e.CellName;

                // 換算後単位の入力後処理を行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KANSAN_UNIT_CD)
                    && this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG
                    && this.form.WindowType != WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 換算後単位が入力されている場合、換算後単位モバイル出力フラグは活性
                    if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value != null
                        && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value.ToString()))
                    {
                        if (!this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = true;
                        }
                    }
                    // 換算後単位が入力されていない場合、換算後単位モバイル出力フラグは非活性
                    else if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                    {
                        if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = false;
                        }
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = false;
                    }
                }

                // 契約区分名の表示を行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIYAKU_KBN) && this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value != null)
                {
                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = "定期";
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = "単価";
                            break;

                        case "3":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = "回収のみ";
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = string.Empty;
                            break;
                    }
                }
                // 集計区分名の表示を行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIJYOU_KBN) && this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value != null)
                {
                    // 許容外の文字が入力されている場合、値のクリアを行う
                    var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    // if (cell != null && cell.CharacterLimitList != null && cell.Value != null && cell.Value != DBNull.Value)
                    // {
                    // if (Array.IndexOf(cell.CharacterLimitList, cell.Value.ToString()[0]) < 0)
                    // {
                    // cell.Value = DBNull.Value;
                    // }
                    // }

                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = "伝票";
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = "合算";
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            break;
                    }
                }
                // 品名CDの大文字対応
                if (e.CellName.Equals(Const.ConstCls.TEIKI_HINMEI_CD))
                {
                    if (this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString().ToUpper();

                        //set column hidden BEFORE_HINMEI_CD
                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_BEFORE_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_CD].Value;
                    }
                }
                // 月極品名CDの大文字対応
                if (e.CellName.Equals(Const.ConstCls.TEIKI_TSUKI_HINMEI_CD))
                {
                    if (this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value = this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value.ToString().ToUpper();
                    }
                }
                if (e.CellName.Equals(Const.ConstCls.TEIKI_UNIT_CD))
                {
                    if (this.form.beforeValuesForDetail.ContainsKey(e.CellName)
                        && !string.IsNullOrWhiteSpace(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value))
                        && !this.form.beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value)))
                    {
                        if (this.sysinfoEntity != null)
                        {
                            //set 換算後単位
                            if (!Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value).Equals("3")
                                && !Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value).Equals("03"))
                            {
                                if (!this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.IsNull)
                                {
                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.Value);
                                    if (uni != null)
                                    {
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellValidated", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
            targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value = 0;
            targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

            if (targetRow.Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            // ポップアップを打ち上げ、ユーザに選択してもらう
            CellPosition pos = this.form.TeikiHinmeiIchiran.CurrentCellPosition;
            CustomControlExtLogic.PopUp((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[pos.RowIndex].Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_CD]);

            var denpyouKbnCd = targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value;
            if (denpyouKbnCd == null
                || targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value.ToString() == "0"
                || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
            {
                // ポップアップでキャンセルが押された
                // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value = 0;
                targetRow.Cells[Const.ConstCls.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

                // ポップアップキャンセルフラグをTrueにする。
                this.form.bDetailErrorFlag = true;

                return false;
            }

            // 伝票区分が支払の場合、契約区分に定期が入力されていたら契約区分をクリアする（未入力の場合は処理が終了しているのでnullチェックは行わない）
            if (Const.ConstCls.DENPYOU_KBN_CD_SHIHARAI_STR == denpyouKbnCd.ToString())
            {
                if (null != targetRow.Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value && !String.IsNullOrEmpty(targetRow.Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString()))
                {
                    var keiyakuKbn = targetRow.Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString();
                    if (Const.ConstCls.KEIYAKU_KBN_CD_TEIKI == keiyakuKbn)
                    {
                        targetRow.Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value = 0;
                        targetRow.Cells[Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = String.Empty;
                    }
                }
            }

            this.form.FlgDenpyouKbn = true;

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValidating(CellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 明細エラーフラグ解除
                this.form.bDetailErrorFlag = false;

                // 品名の入力チェックを行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_HINMEI_CD))
                {
                    // エラーがない場合、品名情報の取得を行う
                    if (!e.Cancel)
                    {
                        bool isHinSet = false;
                        bool isUniSet = false;
                        bool isKbnSet = false;
                        if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()) && !this.form.beforeValuesForDetail[e.CellName].Equals(e.FormattedValue.ToString()))
                        {
                            // 品名をセットする場合、契約区分をクリアする
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KEIYAKU_KBN].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KEIYAKU_KBN_NAME].Value = String.Empty;

                            string hinCd = e.FormattedValue.ToString().PadLeft((int)((GcCustomAlphaNumTextBoxCell)this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_CD]).CharactersNumber, '0');

                            M_HINMEI cond = new M_HINMEI();
                            cond.HINMEI_CD = hinCd;
                            DataTable dt = this.daoHikiaiGenba.SqlGetHinmeiUriageShiharaiDataMinCols(cond);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                isHinSet = true;
                                this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU].Value = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();

                                if (dt.Rows[0]["UNIT_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["UNIT_CD"].ToString()))
                                {
                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(dt.Rows[0]["UNIT_CD"].ToString()));
                                    if (uni != null)
                                    {
                                        isUniSet = true;
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                    }
                                }
                                if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                {
                                    M_DENPYOU_KBN kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(dt.Rows[0]["DENPYOU_KBN_CD"].ToString());
                                    if (kbn != null)
                                    {
                                        switch (kbn.DENPYOU_KBN_CD.ToString())
                                        {
                                            case Const.ConstCls.DENPYOU_KBN_CD_URIAGE_STR:
                                            case Const.ConstCls.DENPYOU_KBN_CD_SHIHARAI_STR:
                                                isKbnSet = true;
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                                                break;

                                            default:
                                                // 処理が２度呼び出される為、ポップアップを１回のみ実行させています。
                                                // 他の処理は２度呼び出す必要があるか未調査の為、専用のフラグで制御しています。
                                                if (this.form.FlgDenpyouKbn)
                                                {
                                                    isKbnSet = true;
                                                }
                                                else
                                                {
                                                    if (SetTeikiDenpyouKbn())
                                                    {
                                                        isKbnSet = true;
                                                    }
                                                    else
                                                    {
                                                        e.Cancel = true;
                                                        this.form.bDetailErrorFlag = true;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                                
                                if(this.sysinfoEntity!=null)
                                {
                                    //check hinmei input is new
                                    string BeforeHinmeiCd = Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_BEFORE_HINMEI_CD].Value);
                                    if (string.IsNullOrEmpty(BeforeHinmeiCd))
                                    {
                                        //set 換算後単位
                                        if (!Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value).Equals("3")
                                            && !Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value).Equals("03"))
                                        {
                                            if (!this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.IsNull)
                                            {
                                                M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(this.sysinfoEntity.GENBA_KANSAN_UNIT_CD.Value);
                                                if (uni != null)
                                                {
                                                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Enabled = true;
                                                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                                    this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                                }
                                            }
                                        }

                                        //set 要記入、実数
                                        if (!string.IsNullOrEmpty(Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KANSAN_UNIT_CD].Value)))
                                        {
                                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = false;
                                            if (this.sysinfoEntity.YOUKI_NYUU.IsTrue)
                                            {
                                                this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = true;
                                            }
                                        }
                                        this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_ANBUN_FLG].Value = false;
                                        if (this.sysinfoEntity.JISSUU.IsTrue)
                                        {
                                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_ANBUN_FLG].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                if (this.form.TeikiHinmeiIchiran.EditingControl != null)
                                {
                                    ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                                }
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
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isUniSet)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isKbnSet)
                        {
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;
                        }
                    }
                }
                // 月極品名CDの入力チェックを行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_TSUKI_HINMEI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                    {
                        string hinmeiCd = e.FormattedValue.ToString().PadLeft(6, '0').ToUpper();
                        bool isExists = false;
                        foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                        {
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value))
                            {
                                continue;
                            }
                            if (hinmeiCd.Equals(row[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString()))
                            {
                                isExists = true;
                                this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value =
                                    row[Const.ConstCls.TSUKI_HINMEI_NAME_RYAKU].Value.ToString();
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
                        this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                    }
                }
                // 換算値のチェックを行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KANSANCHI))
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
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIYAKU_KBN))
                {
                    var keiyakuKbn = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString();
                    if (this.IsDenpyouKbnShiharai())
                    {
                        if (null != this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value && !String.IsNullOrEmpty(this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString()))
                        {
                            if (Const.ConstCls.KEIYAKU_KBN_CD_TEIKI == keiyakuKbn)
                            {
                                msgLogic.MessageBoxShow("E155");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                                return false;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(keiyakuKbn))
                    {
                        var TeikiHinmei = Convert.ToString(this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_HINMEI_CD].Value);

                        //check TeikiHinmei
                        if (string.IsNullOrEmpty(TeikiHinmei))
                        {
                            msgLogic.MessageBoxShowError(ConstCls.MSG_ERR_A);
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
                                    msgLogic.MessageBoxShowError(ConstCls.MSG_ERR_B);
                                    e.Cancel = true;
                                    this.form.bDetailErrorFlag = true;
                                    ((TextBox)this.form.TeikiHinmeiIchiran.EditingControl).SelectAll();
                                }
                            }
                            return false;
                        }

                        //check 取引先CD－取引区分
                        var TeikiDenpyouKbn = Convert.ToString(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_DENPYOU_KBN_CD].Value);
                        int TorihikiKbn = 1;
                        if (TeikiDenpyouKbn == "1")
                        {
                            if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
                            {
                                M_TORIHIKISAKI_SEIKYUU queryParam = new M_TORIHIKISAKI_SEIKYUU();
                                queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                                if (seikyuuEntity != null)
                                {
                                    TorihikiKbn = seikyuuEntity.TORIHIKI_KBN_CD.Value;
                                }
                            }
                            else if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1"))
                            {
                                M_HIKIAI_TORIHIKISAKI_SEIKYUU queryParam = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
                                queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                                M_HIKIAI_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                                if (seikyuuEntity != null)
                                {
                                    TorihikiKbn = seikyuuEntity.TORIHIKI_KBN_CD.Value;
                                }
                            }
                            if (TorihikiKbn == 1)
                            {
                                if (keiyakuKbn == "1")
                                {
                                    msgLogic.MessageBoxShowError(ConstCls.MSG_ERR_C);
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("LogicalDelete", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellValidating", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 定期品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiCellValueChanged(CellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                // 契約区分の設定により入力制限を行う
                if (e.CellName.Equals(Const.ConstCls.TEIKI_KEIYAKU_KBN))
                {
                    // 許容外の文字が入力されている場合、値のクリアを行う
                    var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex] as GcCustomNumericTextBox2Cell;
                    // if (cell != null && cell.CharacterLimitList != null && cell.Value != null && cell.Value != DBNull.Value)
                    // {
                    // if (Array.IndexOf(cell.CharacterLimitList, cell.Value.ToString()[0]) < 0)
                    // {
                    // cell.Value = DBNull.Value;
                    // }
                    // }

                    switch (this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Value.ToString())
                    {
                        case "1":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Selectable = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = true; // add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD]).RegistCheckMethod = null;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = false;// add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN]).RegistCheckMethod = null;
                            break;

                        case "2":
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false; // add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD]).RegistCheckMethod = null;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Selectable = true;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = true;// add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN]).RegistCheckMethod = null;
                            // TEIKI_KEIJYOU_KBNは入力可能になったので、Validatedを実行し、名称を設定する
                            this.TeikiHinmeiCellValidated(
                                new CellEventArgs(
                                    e.RowIndex,
                                    this.form.TeikiHinmeiIchiran.Columns[Const.ConstCls.TEIKI_KEIJYOU_KBN].Index,
                                    Const.ConstCls.TEIKI_KEIJYOU_KBN
                                    )
                                );
                            break;

                        default:
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false; // add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD]).RegistCheckMethod = null;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Selectable = false;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].Value = DBNull.Value;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Value = string.Empty;
                            this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = false;// add
                            ((ICustomControl)this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN]).RegistCheckMethod = null;
                            break;
                    }
                    this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].UpdateBackColor(false);
                    this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].UpdateBackColor(false);
                    this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN].UpdateBackColor(false);
                    this.form.TeikiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TEIKI_KEIJYOU_KBN_NAME].UpdateBackColor(false);
                }
                this.form.TeikiHinmeiIchiran.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiCellValueChanged", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool TeikiHinmeiCellSwitchCdName(CellEventArgs e, ConstCls.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case ConstCls.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(ConstCls.TEIKI_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TeikiHinmeiIchiran.CurrentCell = this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_UNIT_CD];
                        this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    else if (e.CellName.Equals(ConstCls.TEIKI_KANSAN_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TeikiHinmeiIchiran.CurrentCell = this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_KANSAN_UNIT_CD];
                        this.form.TeikiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case ConstCls.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(ConstCls.TEIKI_UNIT_CD))
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    else if (e.CellName.Equals(ConstCls.TEIKI_KANSAN_UNIT_CD))
                    {
                        this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_KANSAN_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TeikiHinmeiIchiran[e.RowIndex, ConstCls.TEIKI_KANSAN_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 定期品名一覧未確定データ処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiDirtyStateChanged(EventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.form.TeikiHinmeiIchiran.CurrentCell.Name.Equals(Const.ConstCls.TEIKI_KEIYAKU_KBN))
                {
                    this.form.TeikiHinmeiIchiran.CommitEdit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiDirtyStateChanged", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 定期品名一覧行入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TeikiHinmeiRowValidating(CellCancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 品名、単位の入力チェックを行う
                if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_CD].Value != null
                    && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString())
                    && this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value != null
                    && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value.ToString()))
                {
                    string hinmeiCd = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString();
                    string unitCd = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_UNIT_CD].Value.ToString();

                    // 重複チェックを行う行の契約区分の設定
                    string keiyakuKbn = string.Empty;
                    if (this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KEIYAKU_KBN].Value != null
                        && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString()))
                    {
                        keiyakuKbn = this.form.TeikiHinmeiIchiran[e.RowIndex, Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString();
                    }

                    // 重複チェックを行う行の伝票区分の設定
                    string denpyouKbn = string.Empty;
                    if (this.form.TeikiHinmeiIchiran[e.RowIndex, "DENPYOU_KBN_CD"].Value != null
                        && !string.IsNullOrWhiteSpace(this.form.TeikiHinmeiIchiran[e.RowIndex, "DENPYOU_KBN_CD"].Value.ToString()))
                    {
                        denpyouKbn = this.form.TeikiHinmeiIchiran[e.RowIndex, "DENPYOU_KBN_CD"].Value.ToString();
                    }

                    foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Index == e.RowIndex) continue;

                        if (row[Const.ConstCls.TEIKI_HINMEI_CD].Value != null
                            && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString())
                            && row[Const.ConstCls.TEIKI_UNIT_CD].Value != null
                            && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TEIKI_UNIT_CD].Value.ToString()))
                        {
                            string targetHinmeiCd = row[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString();
                            string targetUnitCd = row[Const.ConstCls.TEIKI_UNIT_CD].Value.ToString();

                            if (hinmeiCd.Equals(targetHinmeiCd) && unitCd.Equals(targetUnitCd))
                            {
                                if (!this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString())
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(keiyakuKbn)
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(row["DENPYOU_KBN_CD"].Value.ToString())
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(denpyouKbn))
                                {
                                    // 伝票区分が同じ、契約区分が[2.単価]の場合
                                    if (row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("2")
                                        && keiyakuKbn.ToString().Equals("2")
                                        && denpyouKbn.ToString().Equals(row["DENPYOU_KBN_CD"].Value.ToString()))
                                    {
                                    }
                                    // 伝票区分が同じ、契約区分が異なる場合、重複チェックを行う
                                    else if (((row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("2") && keiyakuKbn.ToString().Equals("1"))
                                            || (row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString().Equals("1") && keiyakuKbn.ToString().Equals("2")))
                                            && denpyouKbn.ToString().Equals(row["DENPYOU_KBN_CD"].Value.ToString()))
                                    {
                                        msgLogic.MessageBoxShow("E031", "品名CD,単位CD");
                                        e.Cancel = true;
                                        break;
                                    }
                                    // 上記以外の場合は、品名CDと単位CDの重複のみで結果を表示する
                                    else
                                    {
                                        // 上記に伝票区分の重複条件を追加
                                        if (denpyouKbn.ToString().Equals(row["DENPYOU_KBN_CD"].Value.ToString()))
                                        {
                                            msgLogic.MessageBoxShow("E031", "品名CD,単位CD");
                                            e.Cancel = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.ConstCls.TEIKI_KEIYAKU_KBN].Value.ToString())
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(keiyakuKbn)
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(row["DENPYOU_KBN_CD"].Value.ToString())
                                    && !this.ValueIsNullOrDBNullOrWhiteSpace(denpyouKbn))
                                    {
                                        if (denpyouKbn.ToString().Equals(row["DENPYOU_KBN_CD"].Value.ToString()))
                                        {
                                            msgLogic.MessageBoxShow("E031", "品名CD,単位CD");
                                            e.Cancel = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHinmeiRowValidating", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 月極品名一覧セルフォーマット処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellFormatting(CellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                // LogUtility.DebugMethodStart(e);

                // 単価の表示書式設定を行う
                if (e.CellName.Equals(Const.ConstCls.TSUKI_TANKA))
                {
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = string.Format("{0:" + this.sysinfoEntity.SYS_TANKA_FORMAT + "}", e.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellFormat", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false; ;
            }
            finally
            {
                // LogUtility.DebugMethodEnd();
            }
            return ret;
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
            targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value = 0;
            targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

            if (targetRow.Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            // ポップアップを打ち上げ、ユーザに選択してもらう
            CellPosition pos = this.form.TsukiHinmeiIchiran.CurrentCellPosition;
            CustomControlExtLogic.PopUp((ICustomControl)this.form.TsukiHinmeiIchiran.Rows[pos.RowIndex].Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_CD]);

            var denpyouKbnCd = targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value;
            if (denpyouKbnCd == null
                || targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value.ToString() == "0"
                || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
            {
                // ポップアップでキャンセルが押された
                // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value = 0;
                targetRow.Cells[Const.ConstCls.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;

                // ポップアップキャンセルフラグをTrueにする。
                this.form.bDetailErrorFlag = true;

                return false;
            }
            this.form.FlgDenpyouKbn = true;

            LogUtility.DebugMethodStart();

            return true;
        }

        /// <summary>
        /// 月極品名一覧入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellValidating(CellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 明細エラーフラグ解除
                this.form.bDetailErrorFlag = false;

                // 品名単独のチェックを行う
                if (e.CellName.Equals(Const.ConstCls.TSUKI_HINMEI_CD))
                {
                    if (!string.IsNullOrWhiteSpace(this.form.beforeValuesForDetail[e.CellName]) && (e.FormattedValue == null || !e.FormattedValue.Equals(this.form.beforeValuesForDetail[e.CellName])))
                    {
                        string hinmeiCd = this.form.beforeValuesForDetail[e.CellName];
                        string hinmeiName = this.form.beforeValuesForDetail[Const.ConstCls.TEIKI_HINMEI_NAME_RYAKU];
                        // 定期使用チェック
                        foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
                        {
                            if (row.IsNewRow) continue;

                            if (row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value != null
                                && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value.ToString())
                                && hinmeiCd.Equals(row[Const.ConstCls.TEIKI_TSUKI_HINMEI_CD].Value.ToString()))
                            {
                                msgLogic.MessageBoxShow("E086", "当品名CD", "定期回収タブ内", "変更");
                                e.Cancel = true;
                                this.form.bDetailErrorFlag = true;
                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_NAME_RYAKU].Value = hinmeiName;
                                ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).Text = hinmeiCd;
                                ((TextBox)this.form.TsukiHinmeiIchiran.EditingControl).SelectAll();
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
                            string hinCd = e.FormattedValue.ToString().PadLeft((int)((GcCustomAlphaNumTextBoxCell)this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_CD]).CharactersNumber, '0').ToUpper();
                            // 重複チェック
                            foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                            {
                                if (row.IsNewRow) continue;
                                if (row.Index == e.RowIndex) continue;

                                if (row[Const.ConstCls.TSUKI_HINMEI_CD].Value != null
                                    && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString()))
                                {
                                    string targetHinmeiCd = row[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString();

                                    if (hinCd.Equals(targetHinmeiCd))
                                    {
                                        msgLogic.MessageBoxShow("E031", "品名CD");
                                        e.Cancel = true;
                                        this.form.bDetailErrorFlag = true;
                                        return false;
                                    }
                                }
                            }
                            M_KOBETSU_HINMEI cond = new M_KOBETSU_HINMEI();
                            cond.GYOUSHA_CD = this.form.GyoushaCode.Text;
                            cond.GENBA_CD = this.form.GenbaCode.Text;
                            cond.HINMEI_CD = hinCd;
                            DataTable dt = this.daoHikiaiGenba.SqlGetHinmeiUriageShiharaiData(cond);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                {
                                    if (Const.ConstCls.DENPYOU_KBN_CD_SHIHARAI_STR == dt.Rows[0]["DENPYOU_KBN_CD"].ToString())
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
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_NAME_RYAKU].Value = Convert.ToString(dt.Rows[0]["HINMEI_NAME_RYAKU"]);
                                    }
                                }

                                if (dt.Rows[0]["UNIT_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["UNIT_CD"].ToString()))
                                {
                                    M_UNIT uni = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(int.Parse(dt.Rows[0]["UNIT_CD"].ToString()));
                                    if (uni != null)
                                    {
                                        isUniSet = true;
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_UNIT_CD].Value = uni.UNIT_CD.ToString();
                                        this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_UNIT_NAME_RYAKU].Value = uni.UNIT_NAME_RYAKU;
                                    }
                                }
                                if (dt.Rows[0]["DENPYOU_KBN_CD"] != null && !string.IsNullOrWhiteSpace(dt.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                                {
                                    M_DENPYOU_KBN kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(dt.Rows[0]["DENPYOU_KBN_CD"].ToString());
                                    if (kbn != null)
                                    {
                                        switch (kbn.DENPYOU_KBN_CD.ToString())
                                        {
                                            case Const.ConstCls.DENPYOU_KBN_CD_URIAGE_STR:
                                                isKbnSet = true;
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
                                                break;

                                            case Const.ConstCls.DENPYOU_KBN_CD_SHIHARAI_STR:
                                                break;

                                            default:
                                                kbn = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>().GetDataByCd(Const.ConstCls.DENPYOU_KBN_CD_URIAGE_STR);
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value = kbn.DENPYOU_KBN_CD.ToString();
                                                this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = kbn.DENPYOU_KBN_NAME_RYAKU;
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
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isUniSet)
                        {
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_UNIT_CD].Value = DBNull.Value;
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_UNIT_NAME_RYAKU].Value = string.Empty;
                        }
                        if (!isKbnSet)
                        {
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_CD].Value = DBNull.Value;
                            this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_DENPYOU_KBN_NAME_RYAKU].Value = string.Empty;
                        }
                    }
                }
                // 単価のチェックを行う
                if (e.CellName.Equals(Const.ConstCls.TSUKI_TANKA))
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TsukiHinmeiCellFormat", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellFormat", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 月極品名一覧入力値確定処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiCellValidated(CellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                // 品名CDの大文字対応
                if (e.CellName.Equals(Const.ConstCls.TSUKI_HINMEI_CD))
                {
                    if (this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value != null
                        && !string.IsNullOrEmpty(this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString()))
                    {
                        this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value = this.form.TsukiHinmeiIchiran.Rows[e.RowIndex].Cells[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString().ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellValidated", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 月極品名一覧行入力値確定前処理
        /// </summary>
        /// <param name="e"></param>
        public bool TsukiHinmeiRowValidating(CellCancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 品名、単位の入力チェックを行う
                if (this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_CD].Value != null
                    && !string.IsNullOrWhiteSpace(this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString()))
                {
                    string hinmeiCd = this.form.TsukiHinmeiIchiran[e.RowIndex, Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString();
                    // 重複チェック
                    foreach (Row row in this.form.TsukiHinmeiIchiran.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Index == e.RowIndex) continue;

                        if (row[Const.ConstCls.TSUKI_HINMEI_CD].Value != null
                            && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TSUKI_HINMEI_CD].Value.ToString())
                            && row[Const.ConstCls.TSUKI_UNIT_CD].Value != null
                            && !string.IsNullOrWhiteSpace(row[Const.ConstCls.TSUKI_UNIT_CD].Value.ToString()))
                        {
                            string targetHinmeiCd = row[Const.ConstCls.TEIKI_HINMEI_CD].Value.ToString();
                            string targetUnitCd = row[Const.ConstCls.TSUKI_UNIT_CD].Value.ToString();

                            if (hinmeiCd.Equals(targetHinmeiCd))
                            {
                                msgLogic.MessageBoxShow("E031", "品名CD");
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TsukiHinmeiCellFormat", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool TsukiHinmeiCellSwitchCdName(CellEventArgs e, ConstCls.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case ConstCls.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(ConstCls.TSUKI_UNIT_NAME_RYAKU))
                    {
                        var cell = this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex];
                        if (cell.ReadOnly || !cell.Enabled)
                        {
                            return true;
                        }
                        this.form.TsukiHinmeiIchiran.CurrentCell = this.form.TsukiHinmeiIchiran[e.RowIndex, ConstCls.TSUKI_UNIT_CD];
                        this.form.TsukiHinmeiIchiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case ConstCls.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(ConstCls.TSUKI_UNIT_CD))
                    {
                        this.form.TsukiHinmeiIchiran[e.RowIndex, ConstCls.TSUKI_UNIT_NAME_RYAKU].Visible = true;
                        this.form.TsukiHinmeiIchiran[e.RowIndex, ConstCls.TSUKI_UNIT_NAME_RYAKU].UpdateBackColor(false);
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 請求書拠点の値チェック
        /// </summary>
        public bool SeikyuuKyotenCdValidated()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

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
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 支払書拠点の値チェック
        /// </summary>
        public bool ShiharaiKyotenCdValidated()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShiharaiKyotenCdValidated", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 現場分類区分を変更します
        /// </summary>
        public bool BunruiKbnChanged()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // マニ記載業者にチェックのない場合は処理不要
                if (this.gyoushaEntity != null && !this.gyoushaEntity.GYOUSHAKBN_MANI)
                {
                    return true;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("BunruiKbnChanged", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

            // チェックパターン①　開始日存在
            if (startDate <= targetStartDate && targetStartDate <= endDate)
            {
                checkDate = false;
            }

            // チェックパターン②　終了日存在
            if (startDate <= targetEndDate && targetEndDate <= endDate)
            {
                checkDate = false;
            }

            // チェックパターン③　開始終了内包
            if (targetStartDate <= startDate && endDate <= targetEndDate)
            {
                checkDate = false;
            }

            return checkDate;
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

            // 既存取引先
            if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
            {
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
            }
            // 引合取引先
            else
            {
                var seikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(torihikisakiCd);
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
            var denpyouKbnCell = currentRow[Const.ConstCls.TEIKI_DENPYOU_KBN_CD];
            if (null != denpyouKbnCell.Value && !String.IsNullOrEmpty(denpyouKbnCell.Value.ToString()))
            {
                if (denpyouKbnCell.Value.ToString() == Const.ConstCls.DENPYOU_KBN_CD_SHIHARAI_STR)
                {
                    bRet = true;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 社員部署の値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                this.form.EigyouTantouBushoName.Text = string.Empty;
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("BushoCdValidated", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BushoCdValidated", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
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
        /// データを取得し、 A票～E票画面に設定
        /// </summary>
        private void SetAToEWindowsData()
        {
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
            // 【返送先】
            // A票
            if (this._tabPageManager.IsVisible(6))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_A == 2)
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                    this.form.MANIFEST_USE_2_AHyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_AHyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    this.form.MANIFEST_USE_1_AHyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_AHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.HensousakiKbn1_AHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A)) // No3521
                        {
                            this.form.HensousakiKbn_AHyo.Text = "3";
                            this.form.HensousakiKbn3_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_A).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "2";
                            this.form.HensousakiKbn2_AHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
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
                        this.form.HensousakiKbn_AHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    }
                }
            }
            // B1票
            if (this._tabPageManager.IsVisible(7))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_B1 == 2)
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B1Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_B1Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B1Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B1Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.HensousakiKbn1_B1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1)) // No3521
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "3";
                            this.form.HensousakiKbn3_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "2";
                            this.form.HensousakiKbn2_B1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
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
            // B2票
            if (this._tabPageManager.IsVisible(8))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_B2 == 2)
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B2Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_B2Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B2Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B2Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.HensousakiKbn1_B2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2)) // No3521
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "3";
                            this.form.HensousakiKbn3_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "2";
                            this.form.HensousakiKbn2_B2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
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
            // B4票
            if (this._tabPageManager.IsVisible(9))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_B4 == 2)
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B4Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_B4Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B4Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B4Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.HensousakiKbn1_B4Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4)) // No3521
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "3";
                            this.form.HensousakiKbn3_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "2";
                            this.form.HensousakiKbn2_B4Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
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

            // B6票
            if (this._tabPageManager.IsVisible(10))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_B6 == 2)
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_B6Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_B6Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_B6Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_B6Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.HensousakiKbn1_B6Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6)) // No3521
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "3";
                            this.form.HensousakiKbn3_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "2";
                            this.form.HensousakiKbn2_B6Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
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

            // C1票
            if (this._tabPageManager.IsVisible(11))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_C1 == 2)
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C1Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_C1Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C1Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C1Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.HensousakiKbn1_C1Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1)) // No3521
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "3";
                            this.form.HensousakiKbn3_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "2";
                            this.form.HensousakiKbn2_C1Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
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
            // C2票
            if (this._tabPageManager.IsVisible(12))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_C2 == 2)
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                    this.form.MANIFEST_USE_2_C2Hyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_C2Hyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    this.form.MANIFEST_USE_1_C2Hyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_C2Hyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.HensousakiKbn1_C2Hyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2)) // No3521
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "3";
                            this.form.HensousakiKbn3_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "2";
                            this.form.HensousakiKbn2_C2Hyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
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
            // D票
            if (this._tabPageManager.IsVisible(13))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_D == 2)
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                    this.form.MANIFEST_USE_2_DHyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_DHyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    this.form.MANIFEST_USE_1_DHyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_DHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.HensousakiKbn1_DHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D)) // No3521
                        {
                            this.form.HensousakiKbn_DHyo.Text = "3";
                            this.form.HensousakiKbn3_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_D).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "2";
                            this.form.HensousakiKbn2_DHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
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

            // E票
            if (this._tabPageManager.IsVisible(14))
            {
                if (this.genbaEntity.MANI_HENSOUSAKI_USE_E == 2)
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                    this.form.MANIFEST_USE_2_EHyo.Checked = true;
                    this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                    this.form.HENSOUSAKI_PLACE_KBN_1_EHyo.Checked = true;
                }
                else
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    this.form.MANIFEST_USE_1_EHyo.Checked = true;
                    if (this.genbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        this.form.HENSOUSAKI_PLACE_KBN_2_EHyo.Checked = true;
                        if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.HensousakiKbn1_EHyo.Checked = true;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = GetTorihikisaki(this.genbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E)) // No3521
                        {
                            this.form.HensousakiKbn_EHyo.Text = "3";
                            this.form.HensousakiKbn3_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = GetGenba(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E, this.genbaEntity.MANI_HENSOUSAKI_GENBA_CD_E).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true; // No3521
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "2";
                            this.form.HensousakiKbn2_EHyo.Checked = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.genbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
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
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
        }

        /// <summary>
        /// どのタブで発生したイベントか判定する
        /// </summary>
        /// <param name="ctlName">コントロール名</param>
        /// <returns>コントロール判定区分</returns>
        public string ChkTabEvent(string ctlName)
        {
            LogUtility.DebugMethodStart(ctlName);

            // A票返送先
            if (ctlName.IndexOf("_AHyo") != -1)
            {
                LogUtility.DebugMethodEnd("_AHyo");
                return "_AHyo";
            }

            // B1票返送先
            if (ctlName.IndexOf("_B1Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_B1Hyo");
                return "_B1Hyo";
            }

            // B2票返送先
            if (ctlName.IndexOf("_B2Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_B2Hyo");
                return "_B2Hyo";
            }

            // B4票返送先
            if (ctlName.IndexOf("_B4Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_B4Hyo");
                return "_B4Hyo";
            }

            // B6票返送先
            if (ctlName.IndexOf("_B6Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_B6Hyo");
                return "_B6Hyo";
            }

            // C1票返送先
            if (ctlName.IndexOf("_C1Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_C1Hyo");
                return "_C1Hyo";
            }

            // C2票返送先
            if (ctlName.IndexOf("_C2Hyo") != -1)
            {
                LogUtility.DebugMethodEnd("_C2Hyo");
                return "_C2Hyo";
            }

            // D票返送先
            if (ctlName.IndexOf("_DHyo") != -1)
            {
                LogUtility.DebugMethodEnd("_DHyo");
                return "_DHyo";
            }

            // E票返送先
            if (ctlName.IndexOf("_EHyo") != -1)
            {
                LogUtility.DebugMethodEnd("_EHyo");
                return "_EHyo";
            }

            LogUtility.DebugMethodEnd("");
            return "";
        }

        /// <summary>
        /// A票～E票タブ制御
        /// </summary>
        private void ChangeTabAtoE()
        {
            LogUtility.DebugMethodStart();

            // A票
            this._tabPageManager.ChangeTabPageVisible("tabPage1", (this.sysinfoEntity.MANIFEST_USE_A == 1) ? true : false);

            // B1票
            this._tabPageManager.ChangeTabPageVisible("tabPage2", (this.sysinfoEntity.MANIFEST_USE_B1 == 1) ? true : false);

            // B2票
            this._tabPageManager.ChangeTabPageVisible("tabPage3", (this.sysinfoEntity.MANIFEST_USE_B2 == 1) ? true : false);

            // B4票
            this._tabPageManager.ChangeTabPageVisible("tabPage4", (this.sysinfoEntity.MANIFEST_USE_B4 == 1) ? true : false);

            // B6票
            this._tabPageManager.ChangeTabPageVisible("tabPage5", (this.sysinfoEntity.MANIFEST_USE_B6 == 1) ? true : false);

            // C1票
            this._tabPageManager.ChangeTabPageVisible("tabPage6", (this.sysinfoEntity.MANIFEST_USE_C1 == 1) ? true : false);

            // C2票
            this._tabPageManager.ChangeTabPageVisible("tabPage7", (this.sysinfoEntity.MANIFEST_USE_C2 == 1) ? true : false);

            // D票
            this._tabPageManager.ChangeTabPageVisible("tabPage8", (this.sysinfoEntity.MANIFEST_USE_D == 1) ? true : false);

            // E票
            this._tabPageManager.ChangeTabPageVisible("tabPage9", (this.sysinfoEntity.MANIFEST_USE_E == 1) ? true : false);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// マニ種類、マニ手配を動的に変える
        /// </summary>
        internal bool ChangeManiKbn()
        {
            bool ret = true;
            try
            {
                if (!this.form.GyoushaKbnMani.Checked) { return ret; }

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
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeManiKbn", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeManiKbn", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 金額(文字列)を数値に変換できるかチェック
        /// </summary>
        /// <param name="kingaku">金額（文字列）</param>
        /// <returns>ture  = 引数を数値に変換できる
        /// false = 引数を数値に変換できない</returns>
        internal bool kingakuConversiontCheck(string kingaku)
        {
            int output;

            bool chekResult = int.TryParse(kingaku, out output);

            return chekResult;
        }

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
        // 201400709 syunrei #947 №19 start
        /// <summary>
        /// 修正モードに画面の業者コードより、移行前のデータ作り
        /// </summary>
        public M_HIKIAI_GENBA CreateIkouData(M_HIKIAI_GENBA ikouBeforeData)
        {
            // 移行前のデータ　理論削除
            if (ikouBeforeData != null && !string.IsNullOrEmpty(ikouBeforeData.GYOUSHA_CD)
                && !string.IsNullOrEmpty(ikouBeforeData.GENBA_CD))
            {
                this.DeleteIkouBfData(ikouBeforeData);
            }

            return ikouBeforeData;
        }

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end

        /// <summary>
        /// 理論削除
        /// </summary>
        public void DeleteIkouBfData(M_HIKIAI_GENBA ikouBefore)
        {
            try
            {
                LogUtility.DebugMethodStart(ikouBefore);

                // 理論削除
                // 業者マスタ更新
                ikouBefore.DELETE_FLG = true;
                this.daoHikiaiGenba.Update(ikouBefore);
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteIkouBfData", ex);

                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 移行前のデータ作り
        /// </summary>
        public bool InsertIkouData(M_GENBA ikouBefore, M_HIKIAI_GENBA oMHikiaiGenba)
        {
            LogUtility.DebugMethodStart(ikouBefore, oMHikiaiGenba);
            bool res = true;
            try
            {
                // 見積データに更新前の業者コード
                string oldGyoshaCd = string.Empty;

                // 見積データに更新前の現場コード
                string oldGenbaCd = string.Empty;
                // 見積データに更新後の現場コード
                string newGenbaCd = string.Empty;

                if (ikouBefore != null && !string.IsNullOrEmpty(ikouBefore.GYOUSHA_CD)
                    && !string.IsNullOrEmpty(ikouBefore.GENBA_CD))
                {
                    oldGyoshaCd = ikouBefore.GYOUSHA_CD;
                    oldGenbaCd = ikouBefore.GENBA_CD;

                    // 20140718 chinchisi EV005241_引合現場を移行させるとき、移行先の業者に同じCDの現場があった場合システムエラー start
                    newGenbaCd = this.GetM_GENBA_MaxCD(ikouBefore.GYOUSHA_CD);
                    // 20140718 chinchisi EV005241_引合現場を移行させるとき、移行先の業者に同じCDの現場があった場合システムエラー end

                    // 採番できなかったときはデータ移行を中止する
                    if (this.SaibanErrorFlg)
                    {
                        return false;
                    }

                    ikouBefore.GENBA_CD = newGenbaCd;
                    ikouBefore.DELETE_FLG = false;

                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_A == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_A = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_A = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_B1 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_B1 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_B2 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_B2 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_B4 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_B4 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_B6 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_B6 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_C1 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_C1 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_C2 == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_C2 = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_D == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_D = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_D = ikouBefore.GENBA_CD;
                    }
                    if (ikouBefore.MANI_HENSOUSAKI_PLACE_KBN_E == 1)
                    {
                        ikouBefore.MANI_HENSOUSAKI_GYOUSHA_CD_E = ikouBefore.GYOUSHA_CD;
                        ikouBefore.MANI_HENSOUSAKI_GENBA_CD_E = ikouBefore.GENBA_CD;
                    }

                    var WHO = new DataBinderLogic<M_GENBA>(ikouBefore);
                    WHO.SetSystemProperty(ikouBefore, false);

                    // 業者マスタに登録
                    daoGenbaMastar.InsertGenba(ikouBefore);

                    // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない start
                    // 現場_定期情報マスタ登録
                    this.daoHikiaiGenba.InsertTEIKI_HINMEI(oldGenbaCd, newGenbaCd, oldGyoshaCd);

                    // 現場_月極情報マスタ登録
                    this.daoHikiaiGenba.InsertTSUKI_HINMEI(oldGenbaCd, newGenbaCd, oldGyoshaCd);
                    // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない end

                    // 見積データを更新
                    daoHikiaiGenba.UpdateGYOUSHA_CD(oldGenbaCd, newGenbaCd, oldGyoshaCd);

                    // 20150930 hoanghm #1932 start
                    // update M_HIKIAI_GENBA set GENBA_CD_AFTER
                    oMHikiaiGenba.DELETE_FLG = true;
                    oMHikiaiGenba.GENBA_CD_AFTER = newGenbaCd;
                    daoHikiaiGenba.Update(oMHikiaiGenba);
                    // 20150930 hoanghm #1932 end
                }
                else
                {
                    res = false;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("InsertIkouData", ex1);
                this.form.messBSL.MessageBoxShow("E080", "");
                res = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("InsertIkouData", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                res = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InsertIkouData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                res = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
            return res;
        }

        /// <summary>
        /// 引合マスタ⇒通常マスタに変更
        /// </summary>
        public M_GENBA HikiToTsujou(M_HIKIAI_GENBA mhg)
        {
            M_GENBA ikouData = new M_GENBA();
            ikouData.GYOUSHA_CD = mhg.GYOUSHA_CD;
            ikouData.GENBA_CD = mhg.GENBA_CD;
            ikouData.TORIHIKISAKI_CD = mhg.TORIHIKISAKI_CD;
            ikouData.KYOTEN_CD = mhg.KYOTEN_CD;
            ikouData.GENBA_NAME1 = mhg.GENBA_NAME1;
            ikouData.GENBA_NAME2 = mhg.GENBA_NAME2;
            ikouData.GENBA_NAME_RYAKU = mhg.GENBA_NAME_RYAKU;
            ikouData.GENBA_FURIGANA = mhg.GENBA_FURIGANA;
            ikouData.GENBA_TEL = mhg.GENBA_TEL;
            ikouData.GENBA_FAX = mhg.GENBA_FAX;
            ikouData.GENBA_KEITAI_TEL = mhg.GENBA_KEITAI_TEL;
            ikouData.GENBA_KEISHOU1 = mhg.GENBA_KEISHOU1;
            ikouData.GENBA_KEISHOU2 = mhg.GENBA_KEISHOU2;
            ikouData.EIGYOU_TANTOU_BUSHO_CD = mhg.EIGYOU_TANTOU_BUSHO_CD;
            ikouData.EIGYOU_TANTOU_CD = mhg.EIGYOU_TANTOU_CD;
            ikouData.GENBA_POST = mhg.GENBA_POST;
            ikouData.GENBA_TODOUFUKEN_CD = mhg.GENBA_TODOUFUKEN_CD;
            ikouData.GENBA_ADDRESS1 = mhg.GENBA_ADDRESS1;
            ikouData.GENBA_ADDRESS2 = mhg.GENBA_ADDRESS2;
            ikouData.TORIHIKI_JOUKYOU = mhg.TORIHIKI_JOUKYOU;
            ikouData.CHUUSHI_RIYUU1 = mhg.CHUUSHI_RIYUU1;
            ikouData.CHUUSHI_RIYUU2 = mhg.CHUUSHI_RIYUU2;
            ikouData.BUSHO = mhg.BUSHO;
            ikouData.TANTOUSHA = mhg.TANTOUSHA;
            ikouData.KOUFU_TANTOUSHA = mhg.KOUFU_TANTOUSHA;
            ikouData.SHUUKEI_ITEM_CD = mhg.SHUUKEI_ITEM_CD;
            ikouData.GYOUSHU_CD = mhg.GYOUSHU_CD;
            ikouData.CHIIKI_CD = mhg.CHIIKI_CD;
            ikouData.BIKOU1 = mhg.BIKOU1;
            ikouData.BIKOU2 = mhg.BIKOU2;
            ikouData.BIKOU3 = mhg.BIKOU3;
            ikouData.BIKOU4 = mhg.BIKOU4;
            // 20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm start
            ikouData.UNTENSHA_SHIJI_JIKOU1 = mhg.UNTENSHA_SHIJI_JIKOU1;
            ikouData.UNTENSHA_SHIJI_JIKOU2 = mhg.UNTENSHA_SHIJI_JIKOU2;
            ikouData.UNTENSHA_SHIJI_JIKOU3 = mhg.UNTENSHA_SHIJI_JIKOU3;
            // 20150609 #10697「運転者指示事項」の項目を追加する。by hoanghm end
            ikouData.SEIKYUU_SOUFU_NAME1 = mhg.SEIKYUU_SOUFU_NAME1;
            ikouData.SEIKYUU_SOUFU_NAME2 = mhg.SEIKYUU_SOUFU_NAME2;
            ikouData.SEIKYUU_SOUFU_KEISHOU1 = mhg.SEIKYUU_SOUFU_KEISHOU1;
            ikouData.SEIKYUU_SOUFU_KEISHOU2 = mhg.SEIKYUU_SOUFU_KEISHOU2;
            ikouData.SEIKYUU_SOUFU_POST = mhg.SEIKYUU_SOUFU_POST;
            ikouData.SEIKYUU_SOUFU_ADDRESS1 = mhg.SEIKYUU_SOUFU_ADDRESS1;
            ikouData.SEIKYUU_SOUFU_ADDRESS2 = mhg.SEIKYUU_SOUFU_ADDRESS2;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            ikouData.HAKKOUSAKI_CD = mhg.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            ikouData.SEIKYUU_SOUFU_BUSHO = mhg.SEIKYUU_SOUFU_BUSHO;
            ikouData.SEIKYUU_SOUFU_TANTOU = mhg.SEIKYUU_SOUFU_TANTOU;
            ikouData.SEIKYUU_SOUFU_TEL = mhg.SEIKYUU_SOUFU_TEL;
            ikouData.SEIKYUU_SOUFU_FAX = mhg.SEIKYUU_SOUFU_FAX;
            ikouData.SEIKYUU_TANTOU = mhg.SEIKYUU_TANTOU;
            ikouData.SEIKYUU_DAIHYOU_PRINT_KBN = mhg.SEIKYUU_DAIHYOU_PRINT_KBN;
            ikouData.SEIKYUU_KYOTEN_PRINT_KBN = mhg.SEIKYUU_KYOTEN_PRINT_KBN;
            ikouData.SEIKYUU_KYOTEN_CD = mhg.SEIKYUU_KYOTEN_CD;
            ikouData.SHIHARAI_SOUFU_NAME1 = mhg.SHIHARAI_SOUFU_NAME1;
            ikouData.SHIHARAI_SOUFU_NAME2 = mhg.SHIHARAI_SOUFU_NAME2;
            ikouData.SHIHARAI_SOUFU_KEISHOU1 = mhg.SHIHARAI_SOUFU_KEISHOU1;
            ikouData.SHIHARAI_SOUFU_KEISHOU2 = mhg.SHIHARAI_SOUFU_KEISHOU2;
            ikouData.SHIHARAI_SOUFU_POST = mhg.SHIHARAI_SOUFU_POST;
            ikouData.SHIHARAI_SOUFU_ADDRESS1 = mhg.SHIHARAI_SOUFU_ADDRESS1;
            ikouData.SHIHARAI_SOUFU_ADDRESS2 = mhg.SHIHARAI_SOUFU_ADDRESS2;
            ikouData.SHIHARAI_SOUFU_BUSHO = mhg.SHIHARAI_SOUFU_BUSHO;
            ikouData.SHIHARAI_SOUFU_TANTOU = mhg.SHIHARAI_SOUFU_TANTOU;
            ikouData.SHIHARAI_SOUFU_TEL = mhg.SHIHARAI_SOUFU_TEL;
            ikouData.SHIHARAI_SOUFU_FAX = mhg.SHIHARAI_SOUFU_FAX;
            ikouData.SHIHARAI_KYOTEN_PRINT_KBN = mhg.SHIHARAI_KYOTEN_PRINT_KBN;
            ikouData.SHIHARAI_KYOTEN_CD = mhg.SHIHARAI_KYOTEN_CD;
            ikouData.JISHA_KBN = mhg.JISHA_KBN;
            ikouData.HAISHUTSU_NIZUMI_GENBA_KBN = mhg.HAISHUTSU_NIZUMI_GENBA_KBN;
            ikouData.TSUMIKAEHOKAN_KBN = mhg.TSUMIKAEHOKAN_KBN;
            ikouData.SHOBUN_NIOROSHI_GENBA_KBN = mhg.SHOBUN_NIOROSHI_GENBA_KBN;
            ikouData.SAISHUU_SHOBUNJOU_KBN = mhg.SAISHUU_SHOBUNJOU_KBN;
            ikouData.MANI_HENSOUSAKI_KBN = mhg.MANI_HENSOUSAKI_KBN;
            ikouData.MANIFEST_SHURUI_CD = mhg.MANIFEST_SHURUI_CD;
            ikouData.MANIFEST_TEHAI_CD = mhg.MANIFEST_TEHAI_CD;
            ikouData.SHOBUNSAKI_NO = mhg.SHOBUNSAKI_NO;
            ikouData.SHOKUCHI_KBN = mhg.SHOKUCHI_KBN;
            ikouData.DEN_MANI_SHOUKAI_KBN = mhg.DEN_MANI_SHOUKAI_KBN;
            ikouData.KENSHU_YOUHI = mhg.KENSHU_YOUHI;
            ikouData.ITAKU_KEIYAKU_USE_KBN = mhg.ITAKU_KEIYAKU_USE_KBN;
            ikouData.MANI_HENSOUSAKI_NAME1 = mhg.MANI_HENSOUSAKI_NAME1;
            ikouData.MANI_HENSOUSAKI_NAME2 = mhg.MANI_HENSOUSAKI_NAME2;
            ikouData.MANI_HENSOUSAKI_KEISHOU1 = mhg.MANI_HENSOUSAKI_KEISHOU1;
            ikouData.MANI_HENSOUSAKI_KEISHOU2 = mhg.MANI_HENSOUSAKI_KEISHOU2;
            ikouData.MANI_HENSOUSAKI_POST = mhg.MANI_HENSOUSAKI_POST;
            ikouData.MANI_HENSOUSAKI_ADDRESS1 = mhg.MANI_HENSOUSAKI_ADDRESS1;
            ikouData.MANI_HENSOUSAKI_ADDRESS2 = mhg.MANI_HENSOUSAKI_ADDRESS2;
            ikouData.MANI_HENSOUSAKI_BUSHO = mhg.MANI_HENSOUSAKI_BUSHO;
            ikouData.MANI_HENSOUSAKI_TANTOU = mhg.MANI_HENSOUSAKI_TANTOU;
            ikouData.SHIKUCHOUSON_CD = mhg.SHIKUCHOUSON_CD;
            ikouData.TEKIYOU_BEGIN = mhg.TEKIYOU_BEGIN;
            ikouData.TEKIYOU_END = mhg.TEKIYOU_END;
            ikouData.CREATE_USER = mhg.CREATE_USER;
            ikouData.CREATE_DATE = mhg.CREATE_DATE;
            ikouData.CREATE_PC = mhg.CREATE_PC;
            ikouData.UPDATE_USER = mhg.UPDATE_USER;
            ikouData.UPDATE_DATE = mhg.UPDATE_DATE;
            ikouData.UPDATE_PC = mhg.UPDATE_PC;

            // ikouData.DELETE_FLG = mhg.DELETE_FLG;
            // ikouData.TIME_STAMP = mhg.TIME_STAMP;

            ikouData.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = mhg.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;
            ikouData.MANI_HENSOUSAKI_NAME1 = mhg.MANI_HENSOUSAKI_NAME1;
            ikouData.MANI_HENSOUSAKI_NAME2 = mhg.MANI_HENSOUSAKI_NAME2;
            ikouData.MANI_HENSOUSAKI_KEISHOU1 = mhg.MANI_HENSOUSAKI_KEISHOU1;
            ikouData.MANI_HENSOUSAKI_KEISHOU2 = mhg.MANI_HENSOUSAKI_KEISHOU2;
            ikouData.MANI_HENSOUSAKI_POST = mhg.MANI_HENSOUSAKI_POST;
            ikouData.MANI_HENSOUSAKI_ADDRESS1 = mhg.MANI_HENSOUSAKI_ADDRESS1;
            ikouData.MANI_HENSOUSAKI_ADDRESS2 = mhg.MANI_HENSOUSAKI_ADDRESS2;
            ikouData.MANI_HENSOUSAKI_BUSHO = mhg.MANI_HENSOUSAKI_BUSHO;
            ikouData.MANI_HENSOUSAKI_TANTOU = mhg.MANI_HENSOUSAKI_TANTOU;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_B1 = mhg.MANI_HENSOUSAKI_GENBA_CD_B1;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_B2 = mhg.MANI_HENSOUSAKI_GENBA_CD_B2;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_B4 = mhg.MANI_HENSOUSAKI_GENBA_CD_B4;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_B6 = mhg.MANI_HENSOUSAKI_GENBA_CD_B6;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_C1 = mhg.MANI_HENSOUSAKI_GENBA_CD_C1;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_C2 = mhg.MANI_HENSOUSAKI_GENBA_CD_C2;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_D = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_D;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_D = mhg.MANI_HENSOUSAKI_GENBA_CD_D;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_E = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_E;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_E = mhg.MANI_HENSOUSAKI_GENBA_CD_E;
            ikouData.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = mhg.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
            ikouData.MANI_HENSOUSAKI_GYOUSHA_CD_A = mhg.MANI_HENSOUSAKI_GYOUSHA_CD_A;
            ikouData.MANI_HENSOUSAKI_GENBA_CD_A = mhg.MANI_HENSOUSAKI_GENBA_CD_A;
            ikouData.MANI_HENSOUSAKI_USE_A = mhg.MANI_HENSOUSAKI_USE_A;
            ikouData.MANI_HENSOUSAKI_USE_B1 = mhg.MANI_HENSOUSAKI_USE_B1;
            ikouData.MANI_HENSOUSAKI_USE_B2 = mhg.MANI_HENSOUSAKI_USE_B2;
            ikouData.MANI_HENSOUSAKI_USE_B4 = mhg.MANI_HENSOUSAKI_USE_B4;
            ikouData.MANI_HENSOUSAKI_USE_B6 = mhg.MANI_HENSOUSAKI_USE_B6;
            ikouData.MANI_HENSOUSAKI_USE_C1 = mhg.MANI_HENSOUSAKI_USE_C1;
            ikouData.MANI_HENSOUSAKI_USE_C2 = mhg.MANI_HENSOUSAKI_USE_C2;
            ikouData.MANI_HENSOUSAKI_USE_D = mhg.MANI_HENSOUSAKI_USE_D;
            ikouData.MANI_HENSOUSAKI_USE_E = mhg.MANI_HENSOUSAKI_USE_E;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_A = mhg.MANI_HENSOUSAKI_PLACE_KBN_A;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_B1 = mhg.MANI_HENSOUSAKI_PLACE_KBN_B1;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_B2 = mhg.MANI_HENSOUSAKI_PLACE_KBN_B2;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_B4 = mhg.MANI_HENSOUSAKI_PLACE_KBN_B4;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_B6 = mhg.MANI_HENSOUSAKI_PLACE_KBN_B6;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_C1 = mhg.MANI_HENSOUSAKI_PLACE_KBN_C1;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_C2 = mhg.MANI_HENSOUSAKI_PLACE_KBN_C2;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_D = mhg.MANI_HENSOUSAKI_PLACE_KBN_D;
            ikouData.MANI_HENSOUSAKI_PLACE_KBN_E = mhg.MANI_HENSOUSAKI_PLACE_KBN_E;
            // 20141209 katen #2927 実績報告書　フィードバック対応 start
            ikouData.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = mhg.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            // 20141209 katen #2927 実績報告書　フィードバック対応 end

            return ikouData;
        }

        /// <summary>
        /// 通常マスタの最大コードを取得する
        /// </summary>
        public string GetM_GENBA_MaxCD(string gyousha_cd)
        {
            try
            {
                LogUtility.DebugMethodStart();

                string res = string.Empty;
                this.SaibanErrorFlg = false;

                // 現場マスタのCDの最大値+1を取得
                HikiaiGenbaMasterAccess genbaMasterAccess = new HikiaiGenbaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { }, true);
                int keyGenba = -1;

                var keyGenbasaibanFlag = genbaMasterAccess.IsOverCDLimit(gyousha_cd, out keyGenba, true);

                if (keyGenbasaibanFlag || keyGenba < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.GenbaCode.Text = "";
                    this.SaibanErrorFlg = true;
                    return res;
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    res = String.Format("{0:D" + this.form.GenbaCode.MaxLength + "}", keyGenba);
                }
                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetM_GENBA_MaxCD", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F1 移行ボタンの表示を切り替えます
        /// </summary>
        /// <param name="windowType">ウィンドウタイプ</param>
        public void SetF1Enabled(WINDOW_TYPE windowType)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 修正モードの場合、F1移行ボタンが利用可
            if (windowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) && !this.DensisinseiOptionEnabledFlg)
            {
                parentForm.bt_func1.Enabled = true;
            }
            else
            {
                // 電子申請オプションが有効の場合
                if (DensisinseiOptionEnabledFlg)
                {
                    parentForm.bt_func1.Text = string.Empty;
                    parentForm.bt_func1.Tag = string.Empty;
                }
                parentForm.bt_func1.Enabled = false;
            }
        }

        // 201400709 syunrei #947 №19 end

        // 20140716 chinchisi  EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する start
        public string getGyoushaAFTER(string cd, out bool catchErr)
        {
            M_GYOUSHA hikiaiGyousha = new M_GYOUSHA();
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(cd);

                hikiaiGyousha = this.daoGyousha.GetDataByCd(cd);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getGyoushaAFTER", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getGyoushaAFTER", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hikiaiGyousha.GYOUSHA_CD, catchErr);
            }

            return hikiaiGyousha.GYOUSHA_CD;
        }

        public string getTorihikisakiAFTER(string cd, out bool catchErr)
        {
            M_TORIHIKISAKI hikiaiGyousha = new M_TORIHIKISAKI();
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(cd);

                hikiaiGyousha = daoTorihikisaki.GetDataByCd(cd);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchHikiaiTorihikisaki", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHikiaiTorihikisaki", ex);

                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hikiaiGyousha.TORIHIKISAKI_CD, catchErr);
            }

            return hikiaiGyousha.TORIHIKISAKI_CD;
        }

        // 20140716 chinchisi  EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end

        // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
        /// <summary>
        /// 返送先使用区分変更時の処理
        /// </summary>
        /// <returns></returns>
        public void SetManiHensousakiUseKbnChanged(string hensouCd)
        {
            // コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.form.FindForm().Controls;

            // タブ内(A票～E票)のコントロールに紐付ける
            // ラジオボタン
            MANI_HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hensouCd);
            MANI_HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hensouCd);
            HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hensouCd);
            HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hensouCd);
            HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hensouCd);

            // テキストボックス
            MANI_HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hensouCd);
            HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hensouCd);
            ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hensouCd);
            ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hensouCd);
            ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hensouCd);
            ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hensouCd);
            ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hensouCd);
            ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hensouCd);

            MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hensouCd);
            // 非表示タブは設定なし
            if (HensousakiKbn == null)
            {
                return;
            }
            // テキスト情報クリア
            ManiHensousakiGyoushaCode.Text = string.Empty;
            ManiHensousakiGenbaCode.Text = string.Empty;
            if (MANIFEST_USE.Text == "2")
            {
                MANI_HENSOUSAKI_PLACE_KBN.Text = "1";
                this.HensousakiKbn.Enabled = false;
                this.HensousakiKbn1.Enabled = false;
                this.HensousakiKbn1.Checked = false;
                this.HensousakiKbn2.Enabled = false;
                this.HensousakiKbn3.Enabled = false;
                this.HensousakiKbn.Text = string.Empty;
                this.ManiHensousakiTorihikisakiCode.Enabled = false;
                this.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                this.ManiHensousakiGyoushaCode.Enabled = false;
                this.ManiHensousakiGyoushaCode.Text = string.Empty;
                this.ManiHensousakiGenbaCode.Enabled = false;
                this.ManiHensousakiGenbaCode.Text = string.Empty;
            }
            else if (MANIFEST_USE.Text == "1")
            {
                if (this.form.ManiHensousakiKbn.Checked)
                {
                    this.HensousakiKbn.Enabled = true;
                    this.HensousakiKbn1.Enabled = true;
                    this.HensousakiKbn2.Enabled = true;
                    this.HensousakiKbn3.Enabled = true;
                    if ("1".Equals(this.HensousakiKbn.Text))
                    {
                        this.ManiHensousakiTorihikisakiCode.Enabled = true;
                    }
                }
                else
                {
                    // 返送先区分
                    if (this.form.GyoushaKbnMani.Checked && this.form.HaishutsuKbn.Checked)
                    {
                        this.HensousakiKbn.Text = "1";
                        this.HensousakiKbn.Enabled = true;
                        this.HensousakiKbn1.Enabled = true;
                        this.HensousakiKbn2.Enabled = true;
                        this.HensousakiKbn3.Enabled = true;
                        this.ManiHensousakiTorihikisakiCode.Enabled = true;
                    }
                    else
                    {
                        this.HensousakiKbn.Text = string.Empty;
                        this.HensousakiKbn.Enabled = false;
                        this.HensousakiKbn1.Enabled = false;
                        this.HensousakiKbn2.Enabled = false;
                        this.HensousakiKbn3.Enabled = false;
                        this.ManiHensousakiTorihikisakiCode.Text = string.Empty;
                        this.ManiHensousakiGyoushaCode.Text = string.Empty;
                        this.ManiHensousakiGenbaCode.Text = string.Empty;
                        this.ManiHensousakiTorihikisakiCode.Enabled = false;
                        this.ManiHensousakiGyoushaCode.Enabled = false;
                        this.ManiHensousakiGenbaCode.Enabled = false;
                    }
                }
            }
        }

        // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
        public M_HIKIAI_GENBA ikouBeforeData(M_HIKIAI_GENBA genbaData, out bool catchErr)
        {
            catchErr = true;
            try
            {
                return this.daoHikiaiGenba.GetGenbaData(genbaData);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ikouBeforeData", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ikouBeforeData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return null;
        }

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end

        /// <summary>
        /// 現在のアクティブなタブに基づいてサブファンクションの活性・非活性を切り替えます
        /// </summary>
        internal void SubFunctionEnabledChenge()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 削除モード及び参照モードの時は非活性とする
            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG
                && this.form.WindowType != WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                var tabName = this.form.ManiHensousakiKeishou2B1.SelectedTab.Name;

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
                        parentForm.bt_process1.Enabled = true;
                        break;

                    default:
                        parentForm.bt_process1.Enabled = false;
                        break;
                }
            }
            else
            {
                parentForm.bt_process1.Enabled = false;
            }
        }

        /// <summary>
        /// 現在表示している返送先情報を変数にコピー
        /// </summary>
        /// <returns>コピーした返送先</returns>
        internal MANIFESUTO_HENSOUSAKI HensousakiCopy()
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_AHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_AHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_AHyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B1Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B1Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B1Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B2Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B2Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B2Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B4Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B4Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B4Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B6Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B6Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B6Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_C1Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_C1Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_C1Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_C2Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_C2Hyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_C2Hyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_DHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_DHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_DHyo.Text;
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
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_EHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG = this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_EHyo.Text;
                    motoHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG = this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_EHyo.Text;
                    break;

                default:
                    break;
            }
            return motoHensousaki;
        }

        /// <summary>
        /// 現在表示している返送先情報を変数にコピー
        /// </summary>
        /// <param name="saveHensousaki">ペースト元返送先情報</param>
        /// <param name="targetTab">対象タブ名称</param>
        internal void HensousakiPaste(MANIFESUTO_HENSOUSAKI saveHensousaki, string targetTab)
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_AHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B4Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_B6Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_C1Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_C2Hyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_DHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
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
                        this.form.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG;
                        this.form.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG;
                        this.form.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG_EHyo.Text = saveHensousaki.MANI_HENSOUSAKI_GENBA_HIKIAI_FLG;
                    }
                    break;

                default:
                    break;
            }
        }

        /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                this.form.TekiyouKikanForm.BackColor = Constans.NOMAL_COLOR;
                this.form.TekiyouKikanTo.BackColor = Constans.NOMAL_COLOR;

                DateTime date_from = new DateTime(1, 1, 1);
                DateTime date_to = new DateTime(9999, 12, 31);
                if (!string.IsNullOrWhiteSpace(this.form.TekiyouKikanForm.Text))
                {
                    DateTime.TryParse(this.form.TekiyouKikanForm.Text, out date_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TekiyouKikanTo.Text))
                {
                    DateTime.TryParse(this.form.TekiyouKikanTo.Text, out date_to);
                }

                DateTime torihiki_from = new DateTime(1, 1, 1);
                DateTime torihiki_to = new DateTime(9999, 12, 31);
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Text, out torihiki_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.TORIHIKISAKI_TEKIYOU_END.Text, out torihiki_to);
                }

                DateTime gyousha_from = new DateTime(1, 1, 1);
                DateTime gyousha_to = new DateTime(9999, 12, 31);
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

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.TekiyouKikanForm.IsInputErrorOccured = true;
                    this.form.TekiyouKikanTo.IsInputErrorOccured = true;
                    this.form.TekiyouKikanForm.BackColor = Constans.ERROR_COLOR;
                    this.form.TekiyouKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "適用期間From", "適用期間To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.TekiyouKikanForm.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                return true;
            }

            return false;
        }

        #endregion

        #region TekiyouKikanForm_Leaveイベント

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TekiyouKikanForm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TekiyouKikanTo.Text))
            {
                this.form.TekiyouKikanTo.IsInputErrorOccured = false;
                this.form.TekiyouKikanTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region TekiyouKikanTo_Leaveイベント

        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TekiyouKikanTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TekiyouKikanForm.Text))
            {
                this.form.TekiyouKikanForm.IsInputErrorOccured = false;
                this.form.TekiyouKikanForm.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        /// 20141203 Houkakou 「引合現場入力」の日付チェックを追加する end

        /// 20141226 Houkakou 「引合現場入力」のダブルクリックを追加する start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TekiyouKikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.TekiyouKikanForm;
            var ToTextBox = this.form.TekiyouKikanTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// 20141226 Houkakou 「引合現場入力」のダブルクリックを追加する end

        // 20141209 katen #2927 実績報告書　フィードバック対応 start
        /// <summary>
        /// 運搬報告書提出先を取得します
        /// </summary>
        /// <returns>取得した件数</returns>
        public bool SearchsetUpanHoukokushoTeishutsu()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
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
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchsetUpanHoukokushoTeishutsu", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetUpanHoukokushoTeishutsu", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
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

            this.upnHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.genbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);

            int count = this.upnHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        // 20141209 katen #2927 実績報告書　フィードバック対応 end

        /// <summary>
        /// マニフェスト返送先タブ内のコントロールを使用区分の選択の状態に応じて変更します
        /// </summary>
        /// <param name="hyoName">票</param>
        internal void HENSOUSAKI_PLACE_KBN_TextChanged(string hyoName)
        {
            // コントロール操作クラスのオブジェクト
            ControlUtility controlUtil = new ControlUtility();
            controlUtil.ControlCollection = this.form.FindForm().Controls;

            // タブ内(A票～E票)のコントロールに紐付ける
            // ラジオボタン
            MANI_HENSOUSAKI_PLACE_KBN_1 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_1" + hyoName);
            MANI_HENSOUSAKI_PLACE_KBN_2 = (CustomRadioButton)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN_2" + hyoName);
            HensousakiKbn1 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn1" + hyoName);
            HensousakiKbn2 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn2" + hyoName);
            HensousakiKbn3 = (CustomRadioButton)controlUtil.GetSettingField("HensousakiKbn3" + hyoName);

            // テキストボックス
            MANI_HENSOUSAKI_PLACE_KBN = (CustomNumericTextBox2)controlUtil.GetSettingField("HENSOUSAKI_PLACE_KBN" + hyoName);
            HensousakiKbn = (CustomNumericTextBox2)controlUtil.GetSettingField("HensousakiKbn" + hyoName);
            ManiHensousakiTorihikisakiCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiCode" + hyoName);
            ManiHensousakiGyoushaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaCode" + hyoName);
            ManiHensousakiGenbaCode = (CustomAlphaNumTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaCode" + hyoName);
            ManiHensousakiTorihikisakiName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiTorihikisakiName" + hyoName);
            ManiHensousakiGyoushaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGyoushaName" + hyoName);
            ManiHensousakiGenbaName = (CustomTextBox)controlUtil.GetSettingField("ManiHensousakiGenbaName" + hyoName);
            MANIFEST_USE = (CustomNumericTextBox2)controlUtil.GetSettingField("MANIFEST_USE" + hyoName);

            // 非表示タブは設定なし
            if (MANI_HENSOUSAKI_PLACE_KBN == null)
            {
                return;
            }
            else
            {
                if ("1" == MANI_HENSOUSAKI_PLACE_KBN.Text)
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
                else if ("2" == MANI_HENSOUSAKI_PLACE_KBN.Text)
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

            M_TORIHIKISAKI[] result = this.daoTorihikisaki.GetAllValidData(entity);
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
        internal void MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged()
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
        }

        /// <summary>
        /// マニ返送先チェックボックスのON/OFFチェック
        /// </summary>
        /// <param name="clearFlag"></param>
        /// <returns></returns>
        public void ManiHensousakiKbn_CheckedChanged()
        {
            try
            {
                this.FlgManiHensousakiKbn = this.form.ManiHensousakiKbn.Checked;

                if (this.FlgManiHensousakiKbn)
                {
                    if (this._tabPageManager.IsVisible(6))
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

                    if (this._tabPageManager.IsVisible(7))
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

                    if (this._tabPageManager.IsVisible(8))
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

                    if (this._tabPageManager.IsVisible(9))
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

                    if (this._tabPageManager.IsVisible(10))
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

                    if (this._tabPageManager.IsVisible(11))
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

                    if (this._tabPageManager.IsVisible(12))
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

                    if (this._tabPageManager.IsVisible(13))
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

                    if (this._tabPageManager.IsVisible(14))
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
                    if (this._tabPageManager.IsVisible(6))
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

                    if (this._tabPageManager.IsVisible(7))
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

                    if (this._tabPageManager.IsVisible(8))
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

                    if (this._tabPageManager.IsVisible(9))
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

                    if (this._tabPageManager.IsVisible(10))
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

                    if (this._tabPageManager.IsVisible(11))
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

                    if (this._tabPageManager.IsVisible(12))
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

                    if (this._tabPageManager.IsVisible(13))
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

                    if (this._tabPageManager.IsVisible(14))
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiKbn_CheckedChanged", ex);
                throw;
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
                this.form.messBSL.MessageBoxShow("E245", "");
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
                    var cell = row.Cells[ConstCls.TSUKI_CHOUKA_SETTING];

                    if (Convert.ToBoolean(cell.EditedFormattedValue))
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
                this.form.messBSL.MessageBoxShow("E245", "");
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
                    if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
                    {
                        // 発行先チェック処理
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
                    else if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1"))
                    {
                        // 引合発行先チェック処理
                        M_HIKIAI_TORIHIKISAKI_SEIKYUU queryParam = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
                        queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                        M_HIKIAI_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
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
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HakkousakuCheck", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
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
                this.form.messBSL.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 登録データをチェックする。
        /// </summary>
        public bool CheckRegistData()
        {
            try
            {
                foreach (Row tsukiRow in this.form.TsukiHinmeiIchiran.Rows)
                {
                    if (tsukiRow.IsNewRow || Convert.ToString(tsukiRow.Cells["DELETE_FLG"].Value) != "True") continue;
                    var cell = tsukiRow.Cells[ConstCls.TSUKI_HINMEI_CD];
                    string hinmeiCd = Convert.ToString(cell.Value);
                    // 定期使用チェック
                    foreach (Row teikiRow in this.form.TeikiHinmeiIchiran.Rows)
                    {
                        if (teikiRow.IsNewRow || Convert.ToString(teikiRow.Cells["DELETE_FLG"].Value) == "True") continue;

                        if (!string.IsNullOrEmpty(Convert.ToString(teikiRow[ConstCls.TEIKI_TSUKI_HINMEI_CD].Value))
                            && hinmeiCd.Equals(Convert.ToString(teikiRow[ConstCls.TEIKI_TSUKI_HINMEI_CD].Value)))
                        {
                            cell.Style.BackColor = Constans.ERROR_COLOR;
                            this.form.messBSL.MessageBoxShow("E086", "当品名CD", "定期回収タブ内", "削除");
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegistData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// OpenHikiaiTorihikisakiFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenHikiaiTorihikisakiFormReference(string TorihikisakiCd)
        {
            LogUtility.DebugMethodStart(TorihikisakiCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            //check exist M_TORIHIKISAKI
            string formId = "M461";
            if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
            {
                M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                queryParam.TORIHIKISAKI_CD = this.form.TorihikisakiCode.Text;
                M_TORIHIKISAKI[] result = this.daoTorihikisaki.GetAllValidData(queryParam);
                if (result != null && result.Length > 0)
                {
                    formId = "M213";
                }

            }

                if (!r_framework.Authority.Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

                r_framework.FormManager.FormManager.OpenFormWithAuth(formId, windowType, windowType, TorihikisakiCd);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// OpenHikiaiGyoushaFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenHikiaiGyoushaFormReference(string GyoushaCd)
        {
            LogUtility.DebugMethodStart(GyoushaCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            //check exist M_GYOUSHA
            string formId = "M462";
            if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("0"))
            {
                M_GYOUSHA queryParam = new M_GYOUSHA();
                queryParam.GYOUSHA_CD = GyoushaCd;
                M_GYOUSHA[] result = this.daoGyousha.GetAllValidData(queryParam);
                if (result != null && result.Length > 0)
                {
                    formId = "M215";
                }
            }
            if (!r_framework.Authority.Manager.CheckAuthority(formId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                msgLogic.MessageBoxShow("E158", "修正");
                return;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth(formId, windowType, windowType, GyoushaCd);
            LogUtility.DebugMethodEnd();
        }
    }
}