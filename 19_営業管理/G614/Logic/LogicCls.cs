// $Id: LogicCls.cs 26463 2014-07-24 07:40:09Z d-sato $
using System;
using System.Collections.Generic;
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
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.GenbaKakunin.APP;
using Shougun.Core.BusinessManagement.GenbaKakunin.Const;
using Shougun.Core.BusinessManagement.GenbaKakunin.Dao;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Logic
{

    /// <summary>
    /// 現場保守画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.GenbaKakunin.Setting.ButtonSetting.xml";

        /// <summary>
        /// 現場保守画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;
        //マスタエディタ定義
        private M_HIKIAI_GENBA genbaHikiaiEntity;

        private M_KARI_GENBA genbaKariEntity;

        private List<M_HIKIAI_GENBA_TEIKI_HINMEI> genbaTeikiEntity;

        private List<M_HIKIAI_GENBA_TSUKI_HINMEI> genbaTsukiEntity;

        private List<M_KARI_GENBA_TEIKI_HINMEI> genbaKariTeikiKariEntity;

        private List<M_KARI_GENBA_TSUKI_HINMEI> genbaTsukiKariEntity;

        private M_HIKIAI_GYOUSHA gyoushaHikiaiEntity;

        private M_KARI_GYOUSHA gyoushaKariEntity;

        private M_HIKIAI_TORIHIKISAKI TorihikisakiHikiaiEntity;

        private M_KARI_TORIHIKISAKI torihikisakiKariEntity;

        //業務マスタエディタ定義
        private M_TODOUFUKEN todoufukenEntity;

        private M_CHIIKI chiikiEntity;

        private M_CHIIKI unpanHoukokushoTeishutsuChiikiEntity;

        private M_SHUUKEI_KOUMOKU shuukeiEntity;

        private M_GYOUSHU gyoushuEntity;

        private M_SYS_INFO sysinfoEntity;

        private M_KYOTEN kyotenEntity;

        private M_SHAIN shainEntity;

        private M_BUSHO bushoEntity;

        private M_MANIFEST_SHURUI manishuruiEntity;

        private M_MANIFEST_TEHAI manitehaiEntity;

        private M_TORIHIKISAKI kizonTorihikisakiEntity;

        private M_GYOUSHA kizonGyoushaEntity;

        private List<String> messageList;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBADao daoGenbaHikiai;
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBADao daoGenbaKari;

        // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
        /// <summary>
        /// 現場_定期品名のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBA_TEIKI_HINMEIDao daoGenbaTeikiHikiai;
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBA_TEIKI_HINMEIDao daoGenbaTeikiKari;

        // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
        /// <summary>
        /// 現場_月極品名のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBA_TSUKI_HINMEIDao daoGenbaTsukiHikiai;
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBA_TSUKI_HINMEIDao daoGenbaTsukiKari;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 既存業者のDao
        /// </summary>
        private IM_GENBADao daoKizonGenba;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GYOUSHADao daoGyoushaKari;

        /// <summary>
        /// 引合業者のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GYOUSHADao daoGyoushaHikiai;

        /// <summary>
        /// 既存業者のDao
        /// </summary>
        private IM_GYOUSHADao daoKizonGyousha;

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
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_TORIHIKISAKIDao daoTorihikisakiKari;

        /// <summary>
        /// 引合取引先のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao daoHikiaiTorihikisaki;

        /// <summary>
        /// 既存取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoKizonTorihikisaki;

        /// <summary>
        /// マニ種類のDao
        /// </summary>
        private IM_MANIFEST_SHURUIDao daoManishurui;

        /// <summary>
        /// マニ手配のDao
        /// </summary>
        private IM_MANIFEST_TEHAIDao daoManitehai;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        TabPageManager _tabPageManager = null;

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
        Dictionary<string, string> ItakuKeiyakuShurui = new Dictionary<string, string>()
        {
            {"1", "処分委託契約"},
            {"2", "収集・運搬委託契約"},
            {"3", "収集・運搬/処分委託契約"}
        };

        /// <summary>
        /// 委託契約書ステータス
        /// </summary>
        Dictionary<string, string> ItakuKeiyakuStatus = new Dictionary<string, string>()
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
        /// 仮マスタ使用有無フラグ
        /// </summary>
        /// <remarks>
        /// true:仮マスタ使用,false:仮マスタ未使用
        /// </remarks>
        public bool UseKariData { get; set; }

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
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// ポップアップからデータセットを実施したか否かのフラグ
        /// </summary>
        public bool IsSetDataFromPopup { get; set; }

        /// <summary>
        ///　画面設定中フラグ
        /// </summary>
        public bool isSettingWindowData { get; set; }

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
                this.daoGenbaHikiai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBADao>();
                this.daoGenbaKari = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBADao>();

                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTeikiHikiai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBA_TEIKI_HINMEIDao>();
                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTsukiHikiai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GENBA_TSUKI_HINMEIDao>();

                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTeikiKari = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBA_TEIKI_HINMEIDao>();
                // TODO frameworkにDaoを作成したためnameSpaceを指定する。frameworkのDaoを使用するように修正すること
                this.daoGenbaTsukiKari = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GENBA_TSUKI_HINMEIDao>();

                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

                this.daoGyoushaKari = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_GYOUSHADao>();
                this.daoGyoushaHikiai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_GYOUSHADao>();

                this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
                this.daoChiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>();

                this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
                this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();

                this.daoGyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                this.daoShuukei = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                this.daoTorihikisakiKari = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_KARI_TORIHIKISAKIDao>();

                this.daoHikiaiTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GenbaKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoManishurui = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();
                this.daoManitehai = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();

                this.daoKizonTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                this.daoKizonGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.daoKizonGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
                this.isError = false;
                this.isSettingWindowData = false;

                _tabPageManager = new TabPageManager(this.form.ManiHensousakiKeishou2B1);

                // No2267-->
                this.messageList = new List<string>();
                // No2267<--
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw ex;
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
                this.WindowInitUpdate(parentForm);

                this.allControl = this.form.allControl;

                //すべて項目readonlyがtrueになる
                this.setControl();
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw ex;
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
                result = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
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
                throw ex;
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
        public void WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 検索結果を画面に設定
                this.SetWindowData();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                /**
                 * 初期化
                 */
                // とりあえず指摘のあったものだけを初期化
                this.TeikiHinmeiTable = new DataTable();
                this.TsukiHinmeiTable = new DataTable();

                // 画面設定中フラグ設定
                this.isSettingWindowData = true;

                // 各種データ取得
                this.SearchKariGenba();
                this.SearchHikiaiGenba();

                this.SearchBusho();
                this.SearchChiiki();
                this.SearchUnpanHoukokushoTeishutsuChiiki();

                this.SearchKariGyousha();
                this.SearchHikiaiGyousha();
                this.SearchKizonGyousha();

                this.SearchGyoushu();

                this.SearchKariTorihikisaki();
                this.SearchHikiaiTorihikisaki();
                this.SearchKizonTorihikisaki();

                this.SearchKyoten();
                this.SearchShain();
                this.SearchShuukeiItem();
                this.SearchTodoufuken();
                this.GetSysInfo();
                this.SearchManiShurui();
                this.SearchManiTehai();

                this.SearchTeikiHikiai();
                this.SearchTeikiKari();
                this.SearchTsukiHikiai();
                this.SearchTsukiKari();

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;

                if (!this.UseKariData)
                {
                    #region 引合マスタから取得されたデータで画面の項目に値を設定する
                    if (this.genbaHikiaiEntity == null)
                    {
                        return;
                    }
                    header.CreateDate.Text = this.genbaHikiaiEntity.CREATE_DATE.ToString();
                    header.CreateUser.Text = this.genbaHikiaiEntity.CREATE_USER;
                    header.LastUpdateDate.Text = this.genbaHikiaiEntity.UPDATE_DATE.ToString();
                    header.LastUpdateUser.Text = this.genbaHikiaiEntity.UPDATE_USER;

                    // 共通部
                    this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.genbaHikiaiEntity.TIME_STAMP);

                    if (this.gyoushaHikiaiEntity != null)
                    {
                        this.IsSetDataFromPopup = true;
                        this.form.GyoushaCode.Text = this.genbaHikiaiEntity.GYOUSHA_CD;
                        this.form.GyoushaName1.Text = this.gyoushaHikiaiEntity.GYOUSHA_NAME1;
                        this.form.GyoushaName2.Text = this.gyoushaHikiaiEntity.GYOUSHA_NAME2;

                        this.form.GyoushaKbnUkeire.Checked = (bool)this.gyoushaHikiaiEntity.GYOUSHAKBN_UKEIRE;
                        this.form.GyoushaKbnShukka.Checked = (bool)this.gyoushaHikiaiEntity.GYOUSHAKBN_SHUKKA;
                        this.form.GyoushaKbnMani.Checked = (bool)this.gyoushaHikiaiEntity.GYOUSHAKBN_MANI;
                        if (this.gyoushaHikiaiEntity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.GyoushaTekiyouKikanForm.Value = null;
                        }
                        else
                        {
                            this.form.GyoushaTekiyouKikanForm.Value = this.gyoushaHikiaiEntity.TEKIYOU_BEGIN;
                        }
                        if (this.gyoushaHikiaiEntity.TEKIYOU_END.IsNull)
                        {
                            this.form.GyoushaTekiyouKikanTo.Value = null;
                        }
                        else
                        {
                            this.form.GyoushaTekiyouKikanTo.Value = this.gyoushaHikiaiEntity.TEKIYOU_END;
                        }
                    }
                    else if (this.kizonGyoushaEntity != null)
                    {
                        this.IsSetDataFromPopup = true;
                        this.form.GyoushaCode.Text = this.kizonGyoushaEntity.GYOUSHA_CD;
                        this.form.GyoushaName1.Text = this.kizonGyoushaEntity.GYOUSHA_NAME1;
                        this.form.GyoushaName2.Text = this.kizonGyoushaEntity.GYOUSHA_NAME2;

                        this.form.GyoushaKbnUkeire.Checked = (bool)this.kizonGyoushaEntity.GYOUSHAKBN_UKEIRE;
                        this.form.GyoushaKbnShukka.Checked = (bool)this.kizonGyoushaEntity.GYOUSHAKBN_SHUKKA;
                        this.form.GyoushaKbnMani.Checked = (bool)this.kizonGyoushaEntity.GYOUSHAKBN_MANI;
                        if (this.kizonGyoushaEntity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.GyoushaTekiyouKikanForm.Value = null;
                        }
                        else
                        {
                            this.form.GyoushaTekiyouKikanForm.Value = this.kizonGyoushaEntity.TEKIYOU_BEGIN;
                        }
                        if (this.kizonGyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            this.form.GyoushaTekiyouKikanTo.Value = null;
                        }
                        else
                        {
                            this.form.GyoushaTekiyouKikanTo.Value = this.kizonGyoushaEntity.TEKIYOU_END;
                        }
                    }

                    if (this.TorihikisakiHikiaiEntity != null)
                    {
                        this.IsSetDataFromPopup = true;
                        this.form.TorihikisakiCode.Text = this.genbaHikiaiEntity.TORIHIKISAKI_CD;
                        this.form.TorihikisakiName1.Text = this.TorihikisakiHikiaiEntity.TORIHIKISAKI_NAME1;
                        this.form.TorihikisakiName2.Text = this.TorihikisakiHikiaiEntity.TORIHIKISAKI_NAME2;
                        if (!this.TorihikisakiHikiaiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KyotenCode.Text = this.TorihikisakiHikiaiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        if (this.TorihikisakiHikiaiEntity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.TorihikisakiTekiyouKikanForm.Value = null;
                        }
                        else
                        {
                            this.form.TorihikisakiTekiyouKikanForm.Value = this.TorihikisakiHikiaiEntity.TEKIYOU_BEGIN;
                        }
                        if (this.TorihikisakiHikiaiEntity.TEKIYOU_END.IsNull)
                        {
                            this.form.TorihikisakiTekiyouKikanTo.Value = null;
                        }
                        else
                        {
                            this.form.TorihikisakiTekiyouKikanTo.Value = this.TorihikisakiHikiaiEntity.TEKIYOU_END;
                        }
                    }
                    else if (this.kizonTorihikisakiEntity != null)
                    {
                        this.IsSetDataFromPopup = true;
                        this.form.TorihikisakiCode.Text = this.kizonTorihikisakiEntity.TORIHIKISAKI_CD;
                        this.form.TorihikisakiName1.Text = this.kizonTorihikisakiEntity.TORIHIKISAKI_NAME1;
                        this.form.TorihikisakiName2.Text = this.kizonTorihikisakiEntity.TORIHIKISAKI_NAME2;
                        if (!this.kizonTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KyotenCode.Text = this.kizonTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        if (this.kizonTorihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.TorihikisakiTekiyouKikanForm.Value = null;
                        }
                        else
                        {
                            this.form.TorihikisakiTekiyouKikanForm.Value = this.kizonTorihikisakiEntity.TEKIYOU_BEGIN;
                        }
                        if (this.kizonTorihikisakiEntity.TEKIYOU_END.IsNull)
                        {
                            this.form.TorihikisakiTekiyouKikanTo.Value = null;
                        }
                        else
                        {
                            this.form.TorihikisakiTekiyouKikanTo.Value = this.kizonTorihikisakiEntity.TEKIYOU_END;
                        }
                    }

                    if (this.kyotenEntity != null)
                    {
                        this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                    this.form.GenbaCode.Text = this.genbaHikiaiEntity.GENBA_CD;
                    this.form.GenbaFurigana.Text = this.genbaHikiaiEntity.GENBA_FURIGANA;
                    this.form.GenbaName1.Text = this.genbaHikiaiEntity.GENBA_NAME1;
                    this.form.GenbaName2.Text = this.genbaHikiaiEntity.GENBA_NAME2;
                    this.form.GenbaKeishou1.Text = this.genbaHikiaiEntity.GENBA_KEISHOU1;
                    this.form.GenbaKeishou2.Text = this.genbaHikiaiEntity.GENBA_KEISHOU2;
                    this.form.GenbaNameRyaku.Text = this.genbaHikiaiEntity.GENBA_NAME_RYAKU;
                    this.form.GenbaTel.Text = this.genbaHikiaiEntity.GENBA_TEL;
                    this.form.GenbaKeitaiTel.Text = this.genbaHikiaiEntity.GENBA_KEITAI_TEL;
                    this.form.GenbaFax.Text = this.genbaHikiaiEntity.GENBA_FAX;

                    if (this.bushoEntity != null)
                    {
                        this.form.EigyouTantouBushoCode.Text = this.genbaHikiaiEntity.EIGYOU_TANTOU_BUSHO_CD;
                        this.form.EigyouTantouBushoName.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
                    }
                    if (this.shainEntity != null)
                    {
                        this.form.EigyouCode.Text = this.genbaHikiaiEntity.EIGYOU_TANTOU_CD;
                        this.form.EigyouName.Text = this.shainEntity.SHAIN_NAME_RYAKU;
                    }

                    if (this.genbaHikiaiEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TekiyouKikanForm.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanForm.Value = this.genbaHikiaiEntity.TEKIYOU_BEGIN.Value;
                    }

                    if (this.genbaHikiaiEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TekiyouKikanTo.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanTo.Value = this.genbaHikiaiEntity.TEKIYOU_END.Value;
                    }

                    this.form.ChuusiRiyuu1.Text = this.genbaHikiaiEntity.CHUUSHI_RIYUU1;
                    this.form.ChuusiRiyuu2.Text = this.genbaHikiaiEntity.CHUUSHI_RIYUU2;

                    if (!this.genbaHikiaiEntity.SHOKUCHI_KBN.IsNull)
                    {
                        this.form.ShokuchiKbn.Checked = (bool)this.genbaHikiaiEntity.SHOKUCHI_KBN;
                    }
                    //if (!this.genbaHikiaiEntity.DEN_MANI_SHOUKAI_KBN.IsNull)
                    //{
                    //    this.form.DenManiShoukaiKbn.Checked = (bool)this.genbaHikiaiEntity.DEN_MANI_SHOUKAI_KBN;
                    //}
                    if (!this.genbaHikiaiEntity.KENSHU_YOUHI.IsNull)
                    {
                        this.form.KENSHU_YOUHI.Checked = (bool)this.genbaHikiaiEntity.KENSHU_YOUHI;
                    }

                    // 基本情報
                    this.form.GenbaPost.Text = this.genbaHikiaiEntity.GENBA_POST;
                    if (!this.genbaHikiaiEntity.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        if (this.todoufukenEntity != null)
                        {
                            this.form.GenbaTodoufukenCode.Text = this.genbaHikiaiEntity.GENBA_TODOUFUKEN_CD.ToString();
                            this.form.GenbaTodoufukenNameRyaku.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                        }
                    }
                    this.form.GenbaAddress1.Text = this.genbaHikiaiEntity.GENBA_ADDRESS1;
                    this.form.GenbaAddress2.Text = this.genbaHikiaiEntity.GENBA_ADDRESS2;

                    if (this.chiikiEntity != null)
                    {
                        this.form.ChiikiCode.Text = this.genbaHikiaiEntity.CHIIKI_CD;
                        this.form.ChiikiName.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                    }

                    if (this.unpanHoukokushoTeishutsuChiikiEntity != null)
                    {
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.genbaHikiaiEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = this.unpanHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;
                    }

                    this.form.BushoCode.Text = this.genbaHikiaiEntity.BUSHO;
                    this.form.TantoushaCode.Text = this.genbaHikiaiEntity.TANTOUSHA;
                    this.form.KoufutantoushaCode.Text = this.genbaHikiaiEntity.KOUFU_TANTOUSHA;

                    if (this.shuukeiEntity != null)
                    {
                        this.form.ShuukeiItemCode.Text = this.genbaHikiaiEntity.SHUUKEI_ITEM_CD;
                        this.form.ShuukeiItemName.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                    }

                    if (this.gyoushuEntity != null)
                    {
                        this.form.GyoushuCode.Text = this.genbaHikiaiEntity.GYOUSHU_CD;
                        this.form.GyoushuName.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
                    }

                    this.form.Bikou1.Text = this.genbaHikiaiEntity.BIKOU1;
                    this.form.Bikou2.Text = this.genbaHikiaiEntity.BIKOU2;
                    this.form.Bikou3.Text = this.genbaHikiaiEntity.BIKOU3;
                    this.form.Bikou4.Text = this.genbaHikiaiEntity.BIKOU4;

                    // 請求情報
                    if (this._tabPageManager.IsVisible(1))
                    {
                        this.form.SeikyuushoSoufusaki1.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_NAME1;
                        this.form.SeikyuushoSoufusaki2.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_NAME2;
                        this.form.SeikyuuSouhuKeishou1.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_KEISHOU1;
                        this.form.SeikyuuSouhuKeishou2.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_KEISHOU2;

                        this.form.SeikyuuSoufuPost.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_POST;
                        this.form.SeikyuuSoufuAddress1.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_ADDRESS1;
                        this.form.SeikyuuSoufuAddress2.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_ADDRESS2;
                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        this.form.HAKKOUSAKI_CD.Text = this.genbaHikiaiEntity.HAKKOUSAKI_CD;
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end
                        this.form.SeikyuuSoufuBusho.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_BUSHO;
                        this.form.SeikyuuSoufuTantou.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_TANTOU;
                        this.form.SoufuGenbaTel.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_TEL;
                        this.form.SoufuGenbaFax.Text = this.genbaHikiaiEntity.SEIKYUU_SOUFU_FAX;
                        this.form.SeikyuuTantou.Text = this.genbaHikiaiEntity.SEIKYUU_TANTOU;

                        this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
                        if (!this.genbaHikiaiEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SeikyuuDaihyouPrintKbn.Text = this.genbaHikiaiEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                        }
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                        if (!this.genbaHikiaiEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.genbaHikiaiEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                        }
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                        this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                        if (!this.genbaHikiaiEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaHikiaiEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                            M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                            if (kyo != null)
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                            }
                        }
                    }

                    // 支払情報
                    if (this._tabPageManager.IsVisible(2))
                    {
                        this.form.ShiharaiSoufuName1.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_NAME1;
                        this.form.ShiharaiSoufuName2.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_NAME2;
                        this.form.ShiharaiSoufuKeishou1.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_KEISHOU1;
                        this.form.ShiharaiSoufuKeishou2.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_KEISHOU2;

                        this.form.ShiharaiSoufuPost.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_POST;
                        this.form.ShiharaiSoufuAddress1.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_ADDRESS1;
                        this.form.ShiharaiSoufuAddress2.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_ADDRESS2;
                        this.form.ShiharaiSoufuBusho.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_BUSHO;
                        this.form.ShiharaiSoufuTantou.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_TANTOU;
                        this.form.ShiharaiGenbaTel.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_TEL;
                        this.form.ShiharaiGenbaFax.Text = this.genbaHikiaiEntity.SHIHARAI_SOUFU_FAX;

                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                        if (!this.genbaHikiaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.genbaHikiaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                        }
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                        this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                        if (!this.genbaHikiaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaHikiaiEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                            M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                            if (kyo != null)
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                            }
                        }
                    }

                    // 現場分類
                    if (!this.genbaHikiaiEntity.JISHA_KBN.IsNull)
                    {
                        this.form.JishaKbn.Checked = (bool)this.genbaHikiaiEntity.JISHA_KBN;
                    }
                    else
                    {
                        this.form.JishaKbn.Checked = false;
                    }
                    if (!this.genbaHikiaiEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsNull)
                    {
                        this.form.HaishutsuKbn.Checked = (bool)this.genbaHikiaiEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
                    }
                    else
                    {
                        this.form.HaishutsuKbn.Checked = false;
                    }
                    if (!this.genbaHikiaiEntity.TSUMIKAEHOKAN_KBN.IsNull)
                    {
                        this.form.TsumikaeHokanKbn.Checked = (bool)this.genbaHikiaiEntity.TSUMIKAEHOKAN_KBN;
                    }
                    else
                    {
                        this.form.TsumikaeHokanKbn.Checked = false;
                    }
                    if (!this.genbaHikiaiEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsNull)
                    {
                        this.form.ShobunJigyoujouKbn.Checked = (bool)this.genbaHikiaiEntity.SHOBUN_NIOROSHI_GENBA_KBN;
                    }
                    else
                    {
                        this.form.ShobunJigyoujouKbn.Checked = false;
                    }
                    if (!this.genbaHikiaiEntity.SAISHUU_SHOBUNJOU_KBN.IsNull)
                    {
                        this.form.SaishuuShobunjouKbn.Checked = (bool)this.genbaHikiaiEntity.SAISHUU_SHOBUNJOU_KBN;
                    }
                    else
                    {
                        this.form.SaishuuShobunjouKbn.Checked = false;
                    }
                    if (!this.genbaHikiaiEntity.MANI_HENSOUSAKI_KBN.IsNull)
                    {
                        this.form.ManiHensousakiKbn.Checked = (bool)this.genbaHikiaiEntity.MANI_HENSOUSAKI_KBN;
                    }
                    else
                    {
                        this.form.ManiHensousakiKbn.Checked = false;
                    }

                    if (this.manishuruiEntity != null)
                    {
                        this.form.ManifestShuruiCode.Text = this.genbaHikiaiEntity.MANIFEST_SHURUI_CD.ToString();
                        this.form.ManifestShuruiName.Text = this.manishuruiEntity.MANIFEST_SHURUI_NAME;
                    }
                    else
                    {
                        this.form.ManifestShuruiCode.Text = "";
                        this.form.ManifestShuruiName.Text = "";
                    }
                    if (this.manitehaiEntity != null)
                    {
                        this.form.ManifestTehaiCode.Text = this.genbaHikiaiEntity.MANIFEST_TEHAI_CD.ToString();
                        this.form.ManifestTehaiName.Text = this.manitehaiEntity.MANIFEST_TEHAI_NAME;
                    }
                    else
                    {
                        this.form.ManifestTehaiCode.Text = "";
                        this.form.ManifestTehaiName.Text = "";
                    }
                    this.form.ShobunsakiCode.Text = this.genbaHikiaiEntity.SHOBUNSAKI_NO;

                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.genbaHikiaiEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
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
                        this.form.ManiHensousakiName1.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_NAME1;
                        this.form.ManiHensousakiName2.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_NAME2;
                        this.form.ManiHensousakiKeishou1.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.ManiHensousakiKeishou2.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.ManiHensousakiPost.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_POST;
                        this.form.ManiHensousakiAddress1.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.ManiHensousakiAddress2.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.ManiHensousakiBusho.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_BUSHO;
                        this.form.ManiHensousakiTantou.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TANTOU;
                    }

                    #region  A票～E票画面に設定
                    this.SetAToEWindowsDataForHikiai();

                    #endregion

                    // 定期契約情報
                    if (this.genbaHikiaiEntity.SHIKUCHOUSON_CD != null)
                    {
                        this.form.SHIKUCHOUSON_CD.Text = this.genbaHikiaiEntity.SHIKUCHOUSON_CD.ToString();
                        if (!string.IsNullOrWhiteSpace(this.genbaHikiaiEntity.SHIKUCHOUSON_CD.ToString()))
                        {
                            M_SHIKUCHOUSON shiku = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>().GetDataByCd(this.genbaHikiaiEntity.SHIKUCHOUSON_CD.ToString());
                            if (shiku != null)
                            {
                                this.form.SHIKUCHOUSON_NAME_RYAKU.Text = shiku.SHIKUCHOUSON_NAME_RYAKU;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 仮マスタから取得されたデータで画面の項目に値を設定する
                    if (this.genbaKariEntity == null)
                    {
                        return;
                    }
                    header.CreateDate.Text = this.genbaKariEntity.CREATE_DATE.ToString();
                    header.CreateUser.Text = this.genbaKariEntity.CREATE_USER;
                    header.LastUpdateDate.Text = this.genbaKariEntity.UPDATE_DATE.ToString();
                    header.LastUpdateUser.Text = this.genbaKariEntity.UPDATE_USER;

                    // 共通部
                    this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.genbaKariEntity.TIME_STAMP);

                    if (this.gyoushaKariEntity != null)
                    {
                        this.form.GyoushaCode.Text = this.genbaKariEntity.GYOUSHA_CD;
                        this.form.GyoushaName1.Text = this.gyoushaKariEntity.GYOUSHA_NAME1;
                        this.form.GyoushaName2.Text = this.gyoushaKariEntity.GYOUSHA_NAME2;

                        this.form.GyoushaKbnUkeire.Checked = (bool)this.gyoushaKariEntity.GYOUSHAKBN_UKEIRE;
                        this.form.GyoushaKbnShukka.Checked = (bool)this.gyoushaKariEntity.GYOUSHAKBN_SHUKKA;
                        this.form.GyoushaKbnMani.Checked = (bool)this.gyoushaKariEntity.GYOUSHAKBN_MANI;
                    }

                    if (this.torihikisakiKariEntity != null)
                    {
                        this.form.TorihikisakiCode.Text = this.genbaKariEntity.TORIHIKISAKI_CD;
                        this.form.TorihikisakiName1.Text = this.torihikisakiKariEntity.TORIHIKISAKI_NAME1;
                        this.form.TorihikisakiName2.Text = this.torihikisakiKariEntity.TORIHIKISAKI_NAME2;
                        if (!this.torihikisakiKariEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KyotenCode.Text = this.torihikisakiKariEntity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                    }

                    if (this.kyotenEntity != null)
                    {
                        this.form.KyotenName.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                    this.form.GenbaCode.Text = this.genbaKariEntity.GENBA_CD;
                    this.form.GenbaFurigana.Text = this.genbaKariEntity.GENBA_FURIGANA;
                    this.form.GenbaName1.Text = this.genbaKariEntity.GENBA_NAME1;
                    this.form.GenbaName2.Text = this.genbaKariEntity.GENBA_NAME2;
                    this.form.GenbaKeishou1.Text = this.genbaKariEntity.GENBA_KEISHOU1;
                    this.form.GenbaKeishou2.Text = this.genbaKariEntity.GENBA_KEISHOU2;
                    this.form.GenbaNameRyaku.Text = this.genbaKariEntity.GENBA_NAME_RYAKU;
                    this.form.GenbaTel.Text = this.genbaKariEntity.GENBA_TEL;
                    this.form.GenbaKeitaiTel.Text = this.genbaKariEntity.GENBA_KEITAI_TEL;
                    this.form.GenbaFax.Text = this.genbaKariEntity.GENBA_FAX;

                    if (this.bushoEntity != null)
                    {
                        this.form.EigyouTantouBushoCode.Text = this.genbaKariEntity.EIGYOU_TANTOU_BUSHO_CD;
                        this.form.EigyouTantouBushoName.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
                    }
                    if (this.shainEntity != null)
                    {
                        this.form.EigyouCode.Text = this.genbaKariEntity.EIGYOU_TANTOU_CD;
                        this.form.EigyouName.Text = this.shainEntity.SHAIN_NAME_RYAKU;
                    }

                    if (this.genbaKariEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TekiyouKikanForm.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanForm.Value = this.genbaKariEntity.TEKIYOU_BEGIN.Value;
                    }

                    if (this.genbaKariEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TekiyouKikanTo.Value = null;
                    }
                    else
                    {
                        this.form.TekiyouKikanTo.Value = this.genbaKariEntity.TEKIYOU_END.Value;
                    }

                    this.form.ChuusiRiyuu1.Text = this.genbaKariEntity.CHUUSHI_RIYUU1;
                    this.form.ChuusiRiyuu2.Text = this.genbaKariEntity.CHUUSHI_RIYUU2;

                    if (!this.genbaKariEntity.SHOKUCHI_KBN.IsNull)
                    {
                        this.form.ShokuchiKbn.Checked = (bool)this.genbaKariEntity.SHOKUCHI_KBN;
                    }
                    //if (!this.genbaKariEntity.DEN_MANI_SHOUKAI_KBN.IsNull)
                    //{
                    //    this.form.DenManiShoukaiKbn.Checked = (bool)this.genbaKariEntity.DEN_MANI_SHOUKAI_KBN;
                    //}
                    if (!this.genbaKariEntity.KENSHU_YOUHI.IsNull)
                    {
                        this.form.KENSHU_YOUHI.Checked = (bool)this.genbaKariEntity.KENSHU_YOUHI;
                    }

                    // 基本情報
                    this.form.GenbaPost.Text = this.genbaKariEntity.GENBA_POST;
                    if (!this.genbaKariEntity.GENBA_TODOUFUKEN_CD.IsNull)
                    {
                        if (this.todoufukenEntity != null)
                        {
                            this.form.GenbaTodoufukenCode.Text = this.genbaKariEntity.GENBA_TODOUFUKEN_CD.ToString();
                            this.form.GenbaTodoufukenNameRyaku.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                        }
                    }
                    this.form.GenbaAddress1.Text = this.genbaKariEntity.GENBA_ADDRESS1;
                    this.form.GenbaAddress2.Text = this.genbaKariEntity.GENBA_ADDRESS2;

                    if (this.chiikiEntity != null)
                    {
                        this.form.ChiikiCode.Text = this.genbaKariEntity.CHIIKI_CD;
                        this.form.ChiikiName.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                    }

                    this.form.BushoCode.Text = this.genbaKariEntity.BUSHO;
                    this.form.TantoushaCode.Text = this.genbaKariEntity.TANTOUSHA;
                    this.form.KoufutantoushaCode.Text = this.genbaKariEntity.KOUFU_TANTOUSHA;

                    if (this.shuukeiEntity != null)
                    {
                        this.form.ShuukeiItemCode.Text = this.genbaKariEntity.SHUUKEI_ITEM_CD;
                        this.form.ShuukeiItemName.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                    }

                    if (this.gyoushuEntity != null)
                    {
                        this.form.GyoushuCode.Text = this.genbaKariEntity.GYOUSHU_CD;
                        this.form.GyoushuName.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
                    }

                    this.form.Bikou1.Text = this.genbaKariEntity.BIKOU1;
                    this.form.Bikou2.Text = this.genbaKariEntity.BIKOU2;
                    this.form.Bikou3.Text = this.genbaKariEntity.BIKOU3;
                    this.form.Bikou4.Text = this.genbaKariEntity.BIKOU4;

                    // 請求情報
                    if (this._tabPageManager.IsVisible(1))
                    {
                        this.form.SeikyuushoSoufusaki1.Text = this.genbaKariEntity.SEIKYUU_SOUFU_NAME1;
                        this.form.SeikyuushoSoufusaki2.Text = this.genbaKariEntity.SEIKYUU_SOUFU_NAME2;
                        this.form.SeikyuuSouhuKeishou1.Text = this.genbaKariEntity.SEIKYUU_SOUFU_KEISHOU1;
                        this.form.SeikyuuSouhuKeishou2.Text = this.genbaKariEntity.SEIKYUU_SOUFU_KEISHOU2;

                        this.form.SeikyuuSoufuPost.Text = this.genbaKariEntity.SEIKYUU_SOUFU_POST;
                        this.form.SeikyuuSoufuAddress1.Text = this.genbaKariEntity.SEIKYUU_SOUFU_ADDRESS1;
                        this.form.SeikyuuSoufuAddress2.Text = this.genbaKariEntity.SEIKYUU_SOUFU_ADDRESS2;
                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        this.form.HAKKOUSAKI_CD.Text = this.genbaKariEntity.HAKKOUSAKI_CD;
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end
                        this.form.SeikyuuSoufuBusho.Text = this.genbaKariEntity.SEIKYUU_SOUFU_BUSHO;
                        this.form.SeikyuuSoufuTantou.Text = this.genbaKariEntity.SEIKYUU_SOUFU_TANTOU;
                        this.form.SoufuGenbaTel.Text = this.genbaKariEntity.SEIKYUU_SOUFU_TEL;
                        this.form.SoufuGenbaFax.Text = this.genbaKariEntity.SEIKYUU_SOUFU_FAX;
                        this.form.SeikyuuTantou.Text = this.genbaKariEntity.SEIKYUU_TANTOU;

                        this.form.SeikyuuDaihyouPrintKbn.Text = string.Empty;
                        if (!this.genbaKariEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SeikyuuDaihyouPrintKbn.Text = this.genbaKariEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                        }
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                        if (!this.genbaKariEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.genbaKariEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                        }
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                        this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                        if (!this.genbaKariEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaKariEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                            M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                            if (kyo != null)
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                            }
                        }
                    }

                    // 支払情報
                    if (this._tabPageManager.IsVisible(2))
                    {
                        this.form.ShiharaiSoufuName1.Text = this.genbaKariEntity.SHIHARAI_SOUFU_NAME1;
                        this.form.ShiharaiSoufuName2.Text = this.genbaKariEntity.SHIHARAI_SOUFU_NAME2;
                        this.form.ShiharaiSoufuKeishou1.Text = this.genbaKariEntity.SHIHARAI_SOUFU_KEISHOU1;
                        this.form.ShiharaiSoufuKeishou2.Text = this.genbaKariEntity.SHIHARAI_SOUFU_KEISHOU2;

                        this.form.ShiharaiSoufuPost.Text = this.genbaKariEntity.SHIHARAI_SOUFU_POST;
                        this.form.ShiharaiSoufuAddress1.Text = this.genbaKariEntity.SHIHARAI_SOUFU_ADDRESS1;
                        this.form.ShiharaiSoufuAddress2.Text = this.genbaKariEntity.SHIHARAI_SOUFU_ADDRESS2;
                        this.form.ShiharaiSoufuBusho.Text = this.genbaKariEntity.SHIHARAI_SOUFU_BUSHO;
                        this.form.ShiharaiSoufuTantou.Text = this.genbaKariEntity.SHIHARAI_SOUFU_TANTOU;
                        this.form.ShiharaiGenbaTel.Text = this.genbaKariEntity.SHIHARAI_SOUFU_TEL;
                        this.form.ShiharaiGenbaFax.Text = this.genbaKariEntity.SHIHARAI_SOUFU_FAX;

                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                        if (!this.genbaKariEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.genbaKariEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                        }
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                        this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                        if (!this.genbaKariEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.genbaKariEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                            M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                            if (kyo != null)
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                            }
                        }
                    }

                    // 現場分類
                    if (!this.genbaKariEntity.JISHA_KBN.IsNull)
                    {
                        this.form.JishaKbn.Checked = (bool)this.genbaKariEntity.JISHA_KBN;
                    }
                    else
                    {
                        this.form.JishaKbn.Checked = false;
                    }
                    if (!this.genbaKariEntity.HAISHUTSU_NIZUMI_GENBA_KBN.IsNull)
                    {
                        this.form.HaishutsuKbn.Checked = (bool)this.genbaKariEntity.HAISHUTSU_NIZUMI_GENBA_KBN;
                    }
                    else
                    {
                        this.form.HaishutsuKbn.Checked = false;
                    }
                    if (!this.genbaKariEntity.TSUMIKAEHOKAN_KBN.IsNull)
                    {
                        this.form.TsumikaeHokanKbn.Checked = (bool)this.genbaKariEntity.TSUMIKAEHOKAN_KBN;
                    }
                    else
                    {
                        this.form.TsumikaeHokanKbn.Checked = false;
                    }
                    if (!this.genbaKariEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsNull)
                    {
                        this.form.ShobunJigyoujouKbn.Checked = (bool)this.genbaKariEntity.SHOBUN_NIOROSHI_GENBA_KBN;
                    }
                    else
                    {
                        this.form.ShobunJigyoujouKbn.Checked = false;
                    }
                    if (!this.genbaKariEntity.SAISHUU_SHOBUNJOU_KBN.IsNull)
                    {
                        this.form.SaishuuShobunjouKbn.Checked = (bool)this.genbaKariEntity.SAISHUU_SHOBUNJOU_KBN;
                    }
                    else
                    {
                        this.form.SaishuuShobunjouKbn.Checked = false;
                    }
                    if (!this.genbaKariEntity.MANI_HENSOUSAKI_KBN.IsNull)
                    {
                        this.form.ManiHensousakiKbn.Checked = (bool)this.genbaKariEntity.MANI_HENSOUSAKI_KBN;
                    }
                    else
                    {
                        this.form.ManiHensousakiKbn.Checked = false;
                    }

                    if (this.manishuruiEntity != null)
                    {
                        this.form.ManifestShuruiCode.Text = this.genbaKariEntity.MANIFEST_SHURUI_CD.ToString();
                        this.form.ManifestShuruiName.Text = this.manishuruiEntity.MANIFEST_SHURUI_NAME;
                    }
                    else
                    {
                        this.form.ManifestShuruiCode.Text = "";
                        this.form.ManifestShuruiName.Text = "";
                    }
                    if (this.manitehaiEntity != null)
                    {
                        this.form.ManifestTehaiCode.Text = this.genbaKariEntity.MANIFEST_TEHAI_CD.ToString();
                        this.form.ManifestTehaiName.Text = this.manitehaiEntity.MANIFEST_TEHAI_NAME;
                    }
                    else
                    {
                        this.form.ManifestTehaiCode.Text = "";
                        this.form.ManifestTehaiName.Text = "";
                    }

                    this.form.ShobunsakiCode.Text = this.genbaKariEntity.SHOBUNSAKI_NO;

                    if (this.genbaKariEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.genbaKariEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                    }
                    this.form.ManiHensousakiName2.Text = this.genbaKariEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.ManiHensousakiName1.Text = this.genbaKariEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.ManiHensousakiName2.Text = this.genbaKariEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.ManiHensousakiKeishou1.Text = this.genbaKariEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.ManiHensousakiKeishou2.Text = this.genbaKariEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.ManiHensousakiPost.Text = this.genbaKariEntity.MANI_HENSOUSAKI_POST;
                    this.form.ManiHensousakiAddress1.Text = this.genbaKariEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.ManiHensousakiAddress2.Text = this.genbaKariEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.ManiHensousakiBusho.Text = this.genbaKariEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.ManiHensousakiTantou.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TANTOU;

                    #region  A票～E票画面に設定
                    this.SetAToEWindowsDataForKari();

                    #endregion

                    // 定期契約情報
                    if (this.genbaKariEntity.SHIKUCHOUSON_CD != null)
                    {
                        this.form.SHIKUCHOUSON_CD.Text = this.genbaKariEntity.SHIKUCHOUSON_CD.ToString();
                        if (!string.IsNullOrWhiteSpace(this.genbaKariEntity.SHIKUCHOUSON_CD.ToString()))
                        {
                            M_SHIKUCHOUSON shiku = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>().GetDataByCd(this.genbaKariEntity.SHIKUCHOUSON_CD.ToString());
                            if (shiku != null)
                            {
                                this.form.SHIKUCHOUSON_NAME_RYAKU.Text = shiku.SHIKUCHOUSON_NAME_RYAKU;
                            }
                        }
                    }
                    #endregion
                }

                this.SetIchiranTeiki();
                this.SetNameForTeikiData();

                // 月極契約情報
                this.SetIchiranTsuki();
                // 画面設定中フラグ解除
                this.isSettingWindowData = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw ex;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                throw ex;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                throw ex;
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
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                throw ex;
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
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(padData);
            }

            return padData;
        }

        /// <summary>
        /// データ取得処理(現場)
        /// </summary>
        /// <returns></returns>
        public int SearchKariGenba()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                M_KARI_GENBA genbaSearchString = new M_KARI_GENBA();
                genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.ZeroPadding(this.GenbaCd);
                genbaSearchString.GENBA_CD = zeroPadCd;

                this.genbaKariEntity = null;

                this.genbaKariEntity = daoGenbaKari.GetDataByCd(genbaSearchString);

                count = this.genbaKariEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaKari", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(現場)
        /// </summary>
        /// <returns></returns>
        public int SearchHikiaiGenba()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                M_HIKIAI_GENBA genbaSearchString = new M_HIKIAI_GENBA();
                genbaSearchString.HIKIAI_GYOUSHA_USE_FLG = this.HikiaiGyoushaUseFlg;
                genbaSearchString.GYOUSHA_CD = this.GyoushaCd;
                // 現場CDの入力値をゼロパディング
                string zeroPadCd = this.ZeroPadding(this.GenbaCd);
                genbaSearchString.GENBA_CD = zeroPadCd;

                this.genbaHikiaiEntity = null;

                this.genbaHikiaiEntity = daoGenbaHikiai.GetDataByCd(genbaSearchString);

                count = this.genbaHikiaiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenba", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchKariGyousha()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.gyoushaKariEntity = null;

                this.gyoushaKariEntity = daoGyoushaKari.GetDataByCd(this.GyoushaCd);

                count = this.gyoushaKariEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKariGyousha", ex);
                throw ex;
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

                this.gyoushaHikiaiEntity = null;

                if (this.HikiaiGyoushaUseFlg)
                {
                    this.gyoushaHikiaiEntity = daoGyoushaHikiai.GetDataByCd(this.GyoushaCd);
                }

                count = this.gyoushaHikiaiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(既存業者)
        /// </summary>
        /// <returns></returns>
        public int SearchKizonGyousha()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.kizonGyoushaEntity = null;

                if (!this.HikiaiGyoushaUseFlg)
                {
                    this.kizonGyoushaEntity = this.daoKizonGyousha.GetDataByCd(this.GyoushaCd);
                }

                count = this.kizonGyoushaEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw ex;
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
        public int SearchKariTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.torihikisakiKariEntity = null;
                if (this.genbaKariEntity != null)
                {
                    this.torihikisakiKariEntity = daoTorihikisakiKari.GetDataByCd(this.genbaKariEntity.TORIHIKISAKI_CD);
                }

                count = this.torihikisakiKariEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKariTorihikisaki", ex);
                throw ex;
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

                this.TorihikisakiHikiaiEntity = null;

                if (this.genbaHikiaiEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                {
                    this.TorihikisakiHikiaiEntity = daoHikiaiTorihikisaki.GetDataByCd(this.genbaHikiaiEntity.TORIHIKISAKI_CD);
                }

                count = this.TorihikisakiHikiaiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHikiaiTorihikisaki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(既存取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchKizonTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.kizonTorihikisakiEntity = null;

                if (!this.genbaHikiaiEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                {
                    this.kizonTorihikisakiEntity = this.daoKizonTorihikisaki.GetDataByCd(this.genbaHikiaiEntity.TORIHIKISAKI_CD);
                }

                count = this.kizonTorihikisakiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKizonTorihikisaki", ex);
                throw ex;
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

                if (this.torihikisakiKariEntity != null && !this.torihikisakiKariEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.torihikisakiKariEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                }
                else if (this.kizonTorihikisakiEntity != null && !this.kizonTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.kizonTorihikisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                }

                count = this.kyotenEntity == null ? 0 : 1;
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKyoten", ex);
                throw ex;
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

                this.bushoEntity = daoBusho.GetDataByCd(this.genbaHikiaiEntity.EIGYOU_TANTOU_BUSHO_CD);

                count = this.bushoEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchBusho", ex);
                throw ex;
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

                this.shainEntity = daoShain.GetDataByCd(this.genbaHikiaiEntity.EIGYOU_TANTOU_CD);

                count = this.shainEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchShain", ex);
                throw ex;
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

                if (!this.genbaHikiaiEntity.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.genbaHikiaiEntity.GENBA_TODOUFUKEN_CD.Value.ToString());
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
                throw ex;
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

                this.chiikiEntity = daoChiiki.GetDataByCd(this.genbaHikiaiEntity.CHIIKI_CD);

                count = this.chiikiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchChiiki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(運搬報告書提出先)
        /// </summary>
        /// <returns></returns>
        public int SearchUnpanHoukokushoTeishutsuChiiki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.unpanHoukokushoTeishutsuChiikiEntity = null;

                this.unpanHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.genbaHikiaiEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);

                count = this.unpanHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchUnpanHoukokushoTeishutsuChiiki", ex);
                throw ex;
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

                this.shuukeiEntity = daoShuukei.GetDataByCd(this.genbaHikiaiEntity.SHUUKEI_ITEM_CD);

                count = this.shuukeiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchShuukeiItem", ex);
                throw ex;
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

                this.gyoushuEntity = daoGyoushu.GetDataByCd(this.genbaHikiaiEntity.GYOUSHU_CD);

                count = this.gyoushuEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushu", ex);
                throw ex;
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

                if (!this.genbaHikiaiEntity.MANIFEST_SHURUI_CD.IsNull)
                {
                    this.manishuruiEntity = daoManishurui.GetDataByCd(this.genbaHikiaiEntity.MANIFEST_SHURUI_CD.ToString());
                }

                count = this.manishuruiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchManiShurui", ex);
                throw ex;
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

                if (!this.genbaHikiaiEntity.MANIFEST_TEHAI_CD.IsNull)
                {
                    this.manitehaiEntity = daoManitehai.GetDataByCd(this.genbaHikiaiEntity.MANIFEST_TEHAI_CD.ToString());
                }

                count = this.manitehaiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchManiTehai", ex);
                throw ex;
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
        public int SearchTeikiHikiai()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (this.genbaHikiaiEntity != null)
                {
                    string gyoushaCd = this.genbaHikiaiEntity.GYOUSHA_CD;
                    string genbaCd = this.genbaHikiaiEntity.GENBA_CD;
                    if (string.IsNullOrWhiteSpace(gyoushaCd))
                    {
                        return 0;
                    }
                    Boolean hikiaiGyoshaUseflag = this.genbaHikiaiEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;

                    M_HIKIAI_GENBA_TEIKI_HINMEI condition = new M_HIKIAI_GENBA_TEIKI_HINMEI();
                    condition.GYOUSHA_CD = gyoushaCd;
                    condition.GENBA_CD = genbaCd;
                    condition.HIKIAI_GYOUSHA_USE_FLG = hikiaiGyoshaUseflag;
                    this.TeikiHinmeiTable = daoGenbaTeikiHikiai.GetTeikiHinmeiData(condition);
                }

                count = this.TeikiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTeiki", ex);
                throw ex;
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
        public int SearchTeikiKari()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (this.genbaKariEntity != null)
                {
                    string gyoushaCd = this.genbaKariEntity.GYOUSHA_CD;
                    string genbaCd = this.genbaKariEntity.GENBA_CD;
                    if (string.IsNullOrWhiteSpace(gyoushaCd))
                    {
                        return 0;
                    }

                    M_KARI_GENBA_TEIKI_HINMEI condition = new M_KARI_GENBA_TEIKI_HINMEI();
                    condition.GYOUSHA_CD = gyoushaCd;
                    condition.GENBA_CD = genbaCd;
                    this.TeikiHinmeiTable = daoGenbaTeikiKari.GetTeikiHinmeiData(condition);
                }

                count = this.TeikiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTeiki", ex);
                throw ex;
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
        public int SearchTsukiHikiai()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (this.genbaHikiaiEntity != null)
                {
                    string gyoushaCd = this.genbaHikiaiEntity.GYOUSHA_CD;
                    string genbaCd = this.genbaHikiaiEntity.GENBA_CD;
                    if (string.IsNullOrWhiteSpace(gyoushaCd))
                    {
                        return 0;
                    }
                    Boolean hikiaiGyoshaUseflag = this.genbaHikiaiEntity.HIKIAI_GYOUSHA_USE_FLG.IsTrue;

                    M_HIKIAI_GENBA_TSUKI_HINMEI condition = new M_HIKIAI_GENBA_TSUKI_HINMEI();
                    condition.GYOUSHA_CD = gyoushaCd;
                    condition.GENBA_CD = genbaCd;
                    condition.HIKIAI_GYOUSHA_USE_FLG = hikiaiGyoshaUseflag;
                    this.TsukiHinmeiTable = daoGenbaTsukiHikiai.GetTsukiHinmeiData(condition);
                }

                count = this.TsukiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTsuki", ex);
                throw ex;
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
        public int SearchTsukiKari()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (this.genbaKariEntity != null)
                {
                    string gyoushaCd = this.genbaKariEntity.GYOUSHA_CD;
                    string genbaCd = this.genbaKariEntity.GENBA_CD;
                    if (string.IsNullOrWhiteSpace(gyoushaCd))
                    {
                        return 0;
                    }

                    M_KARI_GENBA_TSUKI_HINMEI condition = new M_KARI_GENBA_TSUKI_HINMEI();
                    condition.GYOUSHA_CD = gyoushaCd;
                    condition.GENBA_CD = genbaCd;
                    this.TsukiHinmeiTable = daoGenbaTsukiKari.GetTsukiHinmeiData(condition);
                }

                count = this.TsukiHinmeiTable.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTsuki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTeiki", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 定期回収情報の契約区分名、集計単位名をセット
        /// <summary>
        /// 定期回収情報の名称関連をセットする
        /// </summary>
        internal void SetNameForTeikiData()
        {
            if (this.form.TeikiHinmeiIchiran == null
                || this.form.TeikiHinmeiIchiran.RowCount < 1)
            {
                return;
            }

            foreach (Row row in this.form.TeikiHinmeiIchiran.Rows)
            {
                if (row == null
                    || row.IsNewRow)
                {
                    continue;
                }

                // 契約区分名セット
                if (row.Cells["KEIYAKU_KBN"].Value != null)
                {
                    switch (row.Cells["KEIYAKU_KBN"].Value.ToString())
                    {
                        case "1":
                            row.Cells["KEIYAKU_KBN_NAME"].Value = ConstCls.KEIYAKU_KBN_NAME_TEIKI;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = true;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = true;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = false;
                            break;

                        case "2":
                            row.Cells["KEIYAKU_KBN_NAME"].Value = ConstCls.KEIYAKU_KBN_NAME_TANKA;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Enabled = true;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = true;
                            break;

                        case "3":
                            row.Cells["KEIYAKU_KBN_NAME"].Value = ConstCls.KEIYAKU_KBN_NAME_NASHI;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = false;
                            break;

                        default:
                            row.Cells["KEIYAKU_KBN_NAME"].Value = string.Empty;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_CD].Enabled = false;
                            row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].Enabled = false;
                            row.Cells[ConstCls.TEIKI_KEIJYOU_KBN_NAME].Enabled = false;
                            break;

                    }
                    row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_CD].UpdateBackColor(false);
                    row.Cells[ConstCls.TEIKI_TSUKI_HINMEI_NAME_RYAKU].UpdateBackColor(false);
                    row.Cells[ConstCls.TEIKI_KEIJYOU_KBN].UpdateBackColor(false);
                    row.Cells[ConstCls.TEIKI_KEIJYOU_KBN_NAME].UpdateBackColor(false);
                }

                // 契約区分名セット
                if (row.Cells["KEIJYOU_KBN"].Value != null)
                {
                    switch (row.Cells["KEIJYOU_KBN"].Value.ToString())
                    {
                        case "1":
                            row.Cells["KEIJYOU_KBN_NAME"].Value = ConstCls.KEIJYOU_KBN_NAME_DENPYOU;
                            break;

                        case "2":
                            row.Cells["KEIJYOU_KBN_NAME"].Value = ConstCls.KEIJYOU_KBN_NAME_GASSAN;
                            break;

                        default:
                            row.Cells["KEIJYOU_KBN_NAME"].Value = string.Empty; ;
                            break;

                    }
                }
            }
            this.form.TeikiHinmeiIchiran.Refresh();
        }
        #endregion

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
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranTsuki", ex);
                throw ex;
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
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        #region  A票～E票処理

        /// <summary>
        ///  引合データを取得し、 A票～E票画面に設定
        /// </summary>
        private void SetAToEWindowsDataForHikiai()
        {
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
            //【返送先】
            //A票
            if (this._tabPageManager.IsVisible(6))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_A == 2)
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_A == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_A == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "";
                    }
                    this.form.HensousakiKbn_AHyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_A == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_AHyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
                            this.form.ManiHensousakiTorihikisakiName_AHyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_AHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_A;
                            this.form.ManiHensousakiGenbaName_AHyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_A).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A))
                        {
                            this.form.HensousakiKbn_AHyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                            this.form.ManiHensousakiGyoushaName_AHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_AHyo.Text = "1";
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
                        this.form.HensousakiKbn_AHyo.Text = "1";
                        this.form.HensousakiKbn_AHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    }
                }
            }
            //B1票
            if (this._tabPageManager.IsVisible(7))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_B1 == 2)
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_B1Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B1Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
                            this.form.ManiHensousakiTorihikisakiName_B1Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
                            this.form.ManiHensousakiGenbaName_B1Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1))
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                            this.form.ManiHensousakiGyoushaName_B1Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B1Hyo.Text = "1";
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
                        this.form.HensousakiKbn_B1Hyo.Text = "1";
                        this.form.HensousakiKbn_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                    }
                }
            }
            //B2票
            if (this._tabPageManager.IsVisible(8))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_B2 == 2)
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_B2Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B2Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
                            this.form.ManiHensousakiTorihikisakiName_B2Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
                            this.form.ManiHensousakiGenbaName_B2Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2))
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                            this.form.ManiHensousakiGyoushaName_B2Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B2Hyo.Text = "1";
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
                        this.form.HensousakiKbn_B2Hyo.Text = "1";
                        this.form.HensousakiKbn_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                    }
                }
            }
            //B4票
            if (this._tabPageManager.IsVisible(9))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_B4 == 2)
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_B4Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B4Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
                            this.form.ManiHensousakiTorihikisakiName_B4Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
                            this.form.ManiHensousakiGenbaName_B4Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B4).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4))
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                            this.form.ManiHensousakiGyoushaName_B4Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B4Hyo.Text = "1";
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
                        this.form.HensousakiKbn_B4Hyo.Text = "1";
                        this.form.HensousakiKbn_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                    }
                }
            }

            //B6票
            if (this._tabPageManager.IsVisible(10))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_B6 == 2)
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_B6Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_B6Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
                            this.form.ManiHensousakiTorihikisakiName_B6Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
                            this.form.ManiHensousakiGenbaName_B6Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_B6).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6))
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                            this.form.ManiHensousakiGyoushaName_B6Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_B6Hyo.Text = "1";
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
                        this.form.HensousakiKbn_B6Hyo.Text = "1";
                        this.form.HensousakiKbn_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                    }
                }
            }

            //C1票
            if (this._tabPageManager.IsVisible(11))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_C1 == 2)
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_C1Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C1Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
                            this.form.ManiHensousakiTorihikisakiName_C1Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
                            this.form.ManiHensousakiGenbaName_C1Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C1).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1))
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                            this.form.ManiHensousakiGyoushaName_C1Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C1Hyo.Text = "1";
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
                        this.form.HensousakiKbn_C1Hyo.Text = "1";
                        this.form.HensousakiKbn_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                    }
                }
            }
            //C2票
            if (this._tabPageManager.IsVisible(12))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_C2 == 2)
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "";
                    }
                    this.form.HensousakiKbn_C2Hyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_C2Hyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
                            this.form.ManiHensousakiTorihikisakiName_C2Hyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
                            this.form.ManiHensousakiGenbaName_C2Hyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_C2).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2))
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                            this.form.ManiHensousakiGyoushaName_C2Hyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_C2Hyo.Text = "1";
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
                        this.form.HensousakiKbn_C2Hyo.Text = "1";
                        this.form.HensousakiKbn_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                    }
                }
            }
            //D票
            if (this._tabPageManager.IsVisible(13))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_D == 2)
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_D == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_D == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "";
                    }
                    this.form.HensousakiKbn_DHyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_D == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_DHyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
                            this.form.ManiHensousakiTorihikisakiName_DHyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_DHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_D;
                            this.form.ManiHensousakiGenbaName_DHyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_D).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D))
                        {
                            this.form.HensousakiKbn_DHyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                            this.form.ManiHensousakiGyoushaName_DHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_DHyo.Text = "1";
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
                        this.form.HensousakiKbn_DHyo.Text = "1";
                        this.form.HensousakiKbn_DHyo.Enabled = false;
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                    }
                }
            }

            //E票
            if (this._tabPageManager.IsVisible(14))
            {
                if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_USE_E == 2)
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_E == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                    }
                    else if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_E == 1)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "1";
                    }
                    else
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "";
                    }
                    this.form.HensousakiKbn_EHyo.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    if (this.genbaHikiaiEntity.MANI_HENSOUSAKI_PLACE_KBN_E == 2)
                    {
                        this.form.HENSOUSAKI_PLACE_KBN_EHyo.Text = "2";
                        if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
                            this.form.ManiHensousakiTorihikisakiName_EHyo.Text = GetTorihikisaki(this.genbaHikiaiEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E).TORIHIKISAKI_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "3";
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiGenbaCode_EHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_E;
                            this.form.ManiHensousakiGenbaName_EHyo.Text = GetGenba(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E, this.genbaHikiaiEntity.MANI_HENSOUSAKI_GENBA_CD_E).GENBA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                        }
                        else if (!string.IsNullOrEmpty(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E))
                        {
                            this.form.HensousakiKbn_EHyo.Text = "2";
                            this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                            this.form.ManiHensousakiGyoushaName_EHyo.Text = GetGyousha(this.genbaHikiaiEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E).GYOUSHA_NAME_RYAKU;
                            this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                            this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                            this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                        }
                        else if (!this.form.ManiHensousakiKbn.Checked)
                        {
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.HensousakiKbn_EHyo.Text = "1";
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
                        this.form.HensousakiKbn_EHyo.Text = "1";
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
        ///  仮データを取得し、 A票～E票画面に設定
        /// </summary>
        private void SetAToEWindowsDataForKari()
        {
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い start
            //【返送先】
            //A票
            if (this._tabPageManager.IsVisible(6))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_A == 2)
                {
                    this.form.MANIFEST_USE_AHyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_AHyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A))
                    {
                        this.form.HensousakiKbn_AHyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_A)) // No3521
                    {
                        this.form.HensousakiKbn_AHyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_AHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_A;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;       // No3521
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A))
                    {
                        this.form.HensousakiKbn_AHyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_AHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A;
                        this.form.ManiHensousakiTorihikisakiCode_AHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_AHyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_AHyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_AHyo.Text = "1";
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
            }
            //B1票
            if (this._tabPageManager.IsVisible(7))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_B1 == 2)
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_B1Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1))
                    {
                        this.form.HensousakiKbn_B1Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1;
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B1)) // No3521
                    {
                        this.form.HensousakiKbn_B1Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B1;
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;  // No3521
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1))
                    {
                        this.form.HensousakiKbn_B1Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1;
                        this.form.ManiHensousakiTorihikisakiCode_B1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B1Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_B1Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_B1Hyo.Text = "1";
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
            }
            //B2票
            if (this._tabPageManager.IsVisible(8))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_B2 == 2)
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_B2Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2))
                    {
                        this.form.HensousakiKbn_B2Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2;
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B2)) // No3521
                    {
                        this.form.HensousakiKbn_B2Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B2;
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;  // No3521
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2))
                    {
                        this.form.HensousakiKbn_B2Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2;
                        this.form.ManiHensousakiTorihikisakiCode_B2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B2Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_B2Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_B2Hyo.Text = "1";
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
            }
            //B4票
            if (this._tabPageManager.IsVisible(9))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_B4 == 2)
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_B4Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4))
                    {
                        this.form.HensousakiKbn_B4Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4;
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B4)) // No3521
                    {
                        this.form.HensousakiKbn_B4Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B4;
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true; // No3521
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4))
                    {
                        this.form.HensousakiKbn_B4Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4;
                        this.form.ManiHensousakiTorihikisakiCode_B4Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B4Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_B4Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_B4Hyo.Text = "1";
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
            }

            //B6票
            if (this._tabPageManager.IsVisible(10))
            {

                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_B6 == 2)
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_B6Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6))
                    {
                        this.form.HensousakiKbn_B6Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6;
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B6)) // No3521
                    {
                        this.form.HensousakiKbn_B6Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_B6;
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true; // No3521
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6))
                    {
                        this.form.HensousakiKbn_B6Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6;
                        this.form.ManiHensousakiTorihikisakiCode_B6Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_B6Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_B6Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_B6Hyo.Text = "1";
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
            }

            //C1票
            if (this._tabPageManager.IsVisible(11))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_C1 == 2)
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_C1Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1))
                    {
                        this.form.HensousakiKbn_C1Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1;
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_C1)) // No3521
                    {
                        this.form.HensousakiKbn_C1Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_C1;
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true; // No3521
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1))
                    {
                        this.form.HensousakiKbn_C1Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1;
                        this.form.ManiHensousakiTorihikisakiCode_C1Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C1Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_C1Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_C1Hyo.Text = "1";
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
            }
            //C2票
            if (this._tabPageManager.IsVisible(12))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_C2 == 2)
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_C2Hyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2))
                    {
                        this.form.HensousakiKbn_C2Hyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2;
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_C2)) // No3521
                    {
                        this.form.HensousakiKbn_C2Hyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_C2;
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true; // No3521
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2))
                    {
                        this.form.HensousakiKbn_C2Hyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2;
                        this.form.ManiHensousakiTorihikisakiCode_C2Hyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_C2Hyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_C2Hyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_C2Hyo.Text = "1";
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
            }
            //D票
            if (this._tabPageManager.IsVisible(13))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_D == 2)
                {
                    this.form.MANIFEST_USE_DHyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_DHyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D))
                    {
                        this.form.HensousakiKbn_DHyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D;
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_D)) // No3521
                    {
                        this.form.HensousakiKbn_DHyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_DHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_D;
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_DHyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D))
                    {
                        this.form.HensousakiKbn_DHyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_DHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D;
                        this.form.ManiHensousakiTorihikisakiCode_DHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_DHyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_DHyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_DHyo.Text = "1";
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
            }

            //E票
            if (this._tabPageManager.IsVisible(14))
            {
                if (this.genbaKariEntity.MANI_HENSOUSAKI_USE_E == 2)
                {
                    this.form.MANIFEST_USE_EHyo.Text = "2";
                }
                else
                {
                    this.form.MANIFEST_USE_EHyo.Text = "1";
                    if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E))
                    {
                        this.form.HensousakiKbn_EHyo.Text = "1";
                        this.form.ManiHensousakiTorihikisakiCode_EHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E;
                        this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = true;
                        this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = false;
                        this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_E)) // No3521
                    {
                        this.form.HensousakiKbn_EHyo.Text = "3";
                        this.form.ManiHensousakiGenbaCode_EHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GENBA_CD_E;
                        this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;  // No3521
                        this.form.ManiHensousakiGenbaCode_EHyo.Enabled = true;
                    }
                    else if (!string.IsNullOrEmpty(this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E))
                    {
                        this.form.HensousakiKbn_EHyo.Text = "2";
                        this.form.ManiHensousakiGyoushaCode_EHyo.Text = this.genbaKariEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E;
                        this.form.ManiHensousakiTorihikisakiCode_EHyo.Enabled = false;
                        this.form.ManiHensousakiGyoushaCode_EHyo.Enabled = true;
                        this.form.ManiHensousakiGenbaCode_EHyo.Enabled = false;
                    }
                    else if (!this.form.ManiHensousakiKbn.Checked)
                    {
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.HensousakiKbn_EHyo.Text = "1";
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
            }
            // 20140716 ria EV005281 引合現場入力のA票～E票返送先に使用区分が無い end
        }

        #endregion

        public void setControl()
        {
            // エラー表示をクリアする
            foreach (var ctrl in this.allControl)
            {
                //tab以外の項目が利用不
                if (ctrl is TabControl || ctrl is Label || ctrl is TabPage)
                {
                    ctrl.Enabled = true;
                }
                else if (ctrl is CustomTextBox)
                {
                    ((CustomTextBox)ctrl).Enabled = true;
                    ((CustomTextBox)ctrl).ReadOnly = true;
                }
                else if (ctrl is CustomDateTimePicker)
                {
                    ((CustomDateTimePicker)ctrl).Enabled = true;
                    ((CustomDateTimePicker)ctrl).ReadOnly = true;
                }
                else if (ctrl is CustomNumericTextBox2)
                {
                    ((CustomNumericTextBox2)ctrl).Enabled = true;
                    ((CustomNumericTextBox2)ctrl).ReadOnly = true;

                }
                else if (ctrl is CustomAlphaNumTextBox)
                {
                    ((CustomAlphaNumTextBox)ctrl).Enabled = true;
                    ((CustomAlphaNumTextBox)ctrl).ReadOnly = true;

                }
                else if (ctrl is GcCustomMultiRow)
                {
                    ((GcCustomMultiRow)ctrl).Enabled = true;
                    ((GcCustomMultiRow)ctrl).ReadOnly = true;
                }
                //else if (ctrl is CustomRadioButton)
                //{
                //    ((CustomRadioButton)ctrl).Enabled = false;
                //}

                //else if (ctrl is CustomPostSearchButton)
                //{
                //    ((CustomPostSearchButton)ctrl).Enabled = false;
                //}
                //else if (ctrl is CustomPopupOpenButton)
                //{
                //    ((CustomPopupOpenButton)ctrl).Enabled = false;
                //}
                //else if (ctrl is CustomComboBox)
                //{
                //    ((CustomComboBox)ctrl).Enabled = false;
                //}
                //else if (ctrl is CustomAddressSearchButton)
                //{
                //    ((CustomAddressSearchButton)ctrl).Enabled = false;
                //}
                //else if (ctrl is CustomPostSearchButton)
                //{
                //    ((CustomPostSearchButton)ctrl).Enabled = false;
                //}
                //else if (ctrl is CustomCheckBox)
                //{
                //    ((CustomCheckBox)ctrl).Enabled = false;
                //}

                else
                {
                    ctrl.Enabled = false;
                }

                ctrl.Tag = "";
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

            M_TORIHIKISAKI[] result = this.daoKizonTorihikisaki.GetAllValidData(entity);
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

            M_GYOUSHA[] result = this.daoKizonGyousha.GetAllValidData(entity);
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

            M_GENBA[] result = this.daoKizonGenba.GetAllValidData(entity);
            if (result != null && result.Length > 0)
            {
                entity = result[0];
            }

            LogUtility.DebugMethodEnd(entity);
            return entity;
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
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
    }
}
