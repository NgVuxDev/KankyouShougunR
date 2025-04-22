// $Id: HikiaiGyoushaLogic.cs 462 2013-10-23 18:11:12Z maedomari $
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
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.BusinessManagement.GyoushaKakunin.APP;
using Shougun.Core.BusinessManagement.GyoushaKakunin.Const;
using Shougun.Core.BusinessManagement.GyoushaKakunin.Dao;
using System.Data.SqlTypes;
using Shougun.Core.Master.GyoushaHoshu;
using r_framework;
using r_framework.MasterAccess;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Logic
{
    /// <summary>
    /// 業者保守画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.GyoushaKakunin.Setting.ButtonSetting.xml";

        /// <summary>
        /// 業者保守画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_HIKIAI_GYOUSHA hikiaiGyoushaEntity;

        private M_HIKIAI_TORIHIKISAKI hikiaiTorihikisakiEntity;

        private M_KARI_GYOUSHA kariGyoushaEntity;

        private M_KARI_TORIHIKISAKI kariTorihikisakiEntity;

        private M_TORIHIKISAKI torihikisakiEntity;

        private M_KYOTEN kyotenEntity;

        private M_BUSHO bushoEntity;

        private M_SHAIN shainEntity;

        private M_TODOUFUKEN todoufukenEntity;

        private M_CHIIKI chiikiEntity;

        private M_CHIIKI unpanHoukokushoTeishutsuChiikiEntity;

        private M_SHUUKEI_KOUMOKU shuukeiEntity;

        private M_GYOUSHU gyoushuEntity;

        private M_SYS_INFO sysinfoEntity;

        private int rowCntGenba;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_GYOUSHADao daoHikiaiGyousha;

        /// <summary>
        /// 仮業者のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_GYOUSHADao daoKariGyousha;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyou;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_GENBADao daoGenba;

        /// <summary>
        /// 仮現場のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_GENBADao daoKariGenba;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

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
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao daoHikiaiTorihikisaki;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao daoHikiaiSeikyuu;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao daoHikiaiShiharai;

        /// <summary>
        /// 仮取引先のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKIDao daoKariTorihikisaki;

        /// <summary>
        /// 仮取引先請求のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKI_SEIKYUUDao daoKariSeikyuu;

        /// <summary>
        /// 仮取引先支払のDao
        /// </summary>
        private Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKI_SHIHARAIDao daoKariShiharai;


        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 部署のDao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

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
        /// タブコントロール制御用
        /// </summary>
        TabPageManager _tabPageManager = null;

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

        #endregion

        #region プロパティ

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 業者区分
        /// </summary>
        public int GyoshaKbn { get; set; }

        /// <summary>
        /// 検索結果(現場一覧)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 現場マスタ（現場CD=000000）
        /// </summary>
        public M_HIKIAI_GENBA hikiaiGenbaEntity;

        /// <summary>
        /// 仮現場マスタ（現場CD=000000）
        /// </summary>
        public M_KARI_GENBA kariGenbaEntity;

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
                this.daoHikiaiGyousha = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_GYOUSHADao>();
                this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
                this.daoGenba = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_GENBADao>();
                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
                this.daoChiiki = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
                this.daoGyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
                this.daoShuukei = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                this.daoSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                this.daoShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                this.daoHikiaiTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoHikiaiSeikyuu = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoHikiaiShiharai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();

                this.daoKariGenba = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_GENBADao>();
                this.daoKariGyousha = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_GYOUSHADao>();
                this.daoKariSeikyuu = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoKariShiharai = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKI_SHIHARAIDao>();
                this.daoKariTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.GyoushaKakunin.Dao.IM_KARI_TORIHIKISAKIDao>();
                
                _tabPageManager = new TabPageManager(this.form.JOHOU);
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

                // 確認画面初期化
                this.WindowInitKakunin(parentForm);
                this.allControl = this.form.allControl;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
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
        /// 確認画面項目表示
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitKakunin(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロールを操作可能とする
                this.TextBoxLock(true);

                // 検索結果を画面に設定
                if (this.GyoshaKbn == 1)
                {
                    this.SetWindowDataFromHikiai();
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.SetWindowDataFromKari();
                }

                // 全コントロールを操作不可能とする
                this.AllControlLock(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データを引合から取得し、画面に設定
        /// </summary>
        private void SetWindowDataFromHikiai()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 各種データ取得
                this.SearchGenba(this.GyoushaCd, "000000");
                this.SearchGyousha();
                this.SearchBusho();
                this.SearchChiiki();
                this.SearchUnpanHoukokushoTeishutsuChiiki();
                this.SearchGyoushu();
                this.SearchKyoten();
                this.SearchShain();
                this.SearchShuukeiItem();
                this.SearchTodoufuken();
                this.SearchTorihikisaki();
                this.SearchHikiaiTorihikisaki();
                this.GetSysInfo();

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = this.hikiaiGyoushaEntity.CREATE_DATE.ToString();
                header.CreateUser.Text = this.hikiaiGyoushaEntity.CREATE_USER;
                header.LastUpdateDate.Text = this.hikiaiGyoushaEntity.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.hikiaiGyoushaEntity.UPDATE_USER;

                // 共通部
                this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.hikiaiGyoushaEntity.TIME_STAMP);
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                if (!this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                {
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG ? "1" : "0";
                }

                this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty; // 取引先有無区分が前回表示と同じだった場合入力許可となってしまうので一旦クリアしてイベント駆動するようにしておく

                if (!this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN.IsNull)
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN.ToString();
                }
                else
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty;
                }

                if (!this.hikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE.IsNull)
                {
                    this.form.Gyousha_KBN_1.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE;
                }
                else
                {
                    this.form.Gyousha_KBN_1.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA.IsNull)
                {
                    this.form.Gyousha_KBN_2.Checked = (bool)this.hikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA;
                }
                else
                {
                    this.form.Gyousha_KBN_2.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI.IsNull)
                {
                    // マニ記載のチェックを外す場合は各区分をクリアしておく
                    // ※チェックを外す際にエラーとなる為
                    var gyoushaKbnMani = this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI.Value;
                    //if (!gyoushaKbnMani)
                    //{
                    //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                    //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                    //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                    //    this.form.JISHA_KBN.Checked = false;
                    //}
                    this.form.Gyousha_KBN_3.Checked = gyoushaKbnMani;
                }
                else
                {
                    this.form.Gyousha_KBN_3.Checked = false;
                }

                this.form.TORIHIKISAKI_CD.Text = this.hikiaiGyoushaEntity.TORIHIKISAKI_CD;

                var hikiaiTorihikisakiUseFlg = false;
                if (!this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                {
                    hikiaiTorihikisakiUseFlg = this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.Value;
                }

                if (this.hikiaiTorihikisakiEntity != null && hikiaiTorihikisakiUseFlg)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME2;
                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_BEGIN;
                    }
                    if (this.hikiaiTorihikisakiEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_TEKIYOU_END.Value = this.hikiaiTorihikisakiEntity.TEKIYOU_END;
                    }
                }
                else if (this.torihikisakiEntity != null && !hikiaiTorihikisakiUseFlg)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = this.torihikisakiEntity.TORIHIKISAKI_NAME2;
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
                }

                if (!this.hikiaiGyoushaEntity.KYOTEN_CD.IsNull)
                {
                    this.form.KYOTEN_CD.Text = this.hikiaiGyoushaEntity.KYOTEN_CD.Value.ToString();
                }
                if (this.kyotenEntity != null)
                {
                    this.form.KYOTEN_NAME.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                }


                this.form.GYOUSHA_CD.Text = this.hikiaiGyoushaEntity.GYOUSHA_CD;  
                this.form.GYOUSHA_NAME1.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
                this.form.GYOUSHA_NAME2.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;
                this.form.GYOUSHA_FURIGANA.Text = this.hikiaiGyoushaEntity.GYOUSHA_FURIGANA;
                this.form.GYOUSHA_NAME_RYAKU.Text = this.hikiaiGyoushaEntity.GYOUSHA_NAME_RYAKU;
                this.form.GYOUSHA_KEISHOU1.Text = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
                this.form.GYOUSHA_KEISHOU2.Text = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
                this.form.GYOUSHA_TEL.Text = this.hikiaiGyoushaEntity.GYOUSHA_TEL;
                this.form.GYOUSHA_KEITAI_TEL.Text = this.hikiaiGyoushaEntity.GYOUSHA_KEITAI_TEL;
                this.form.GYOUSHA_FAX.Text = this.hikiaiGyoushaEntity.GYOUSHA_FAX;
                if (this.bushoEntity != null)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.hikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
                    this.form.BUSHO_NAME.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
                }
                if (this.shainEntity != null)
                {
                    this.form.EIGYOU_TANTOU_CD.Text = this.hikiaiGyoushaEntity.EIGYOU_TANTOU_CD;
                    this.form.SHAIN_NAME.Text = this.shainEntity.SHAIN_NAME_RYAKU;
                }

                if (this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_BEGIN.Value = this.hikiaiGyoushaEntity.TEKIYOU_BEGIN.Value;
                }

                if (this.hikiaiGyoushaEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_END.Value = this.hikiaiGyoushaEntity.TEKIYOU_END.Value;
                }
                this.form.CHUUSHI_RIYUU1.Text = this.hikiaiGyoushaEntity.CHUUSHI_RIYUU1;
                this.form.CHUUSHI_RIYUU2.Text = this.hikiaiGyoushaEntity.CHUUSHI_RIYUU2;

                if (!this.hikiaiGyoushaEntity.SHOKUCHI_KBN.IsNull)
                {
                    this.form.SHOKUCHI_KBN.Checked = (bool)this.hikiaiGyoushaEntity.SHOKUCHI_KBN;
                }
                else
                {
                    this.form.SHOKUCHI_KBN.Checked = false;
                }

                // 基本情報
                this.form.GYOUSHA_POST.Text = this.hikiaiGyoushaEntity.GYOUSHA_POST;
                if (!this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    if (this.todoufukenEntity != null)
                    {
                        this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString();
                        this.form.TODOUFUKEN_NAME.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                    }
                }

                this.form.GYOUSHA_ADDRESS1.Text = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS1;
                this.form.GYOUSHA_ADDRESS2.Text = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS2;
                if (this.chiikiEntity != null)
                {
                    this.form.CHIIKI_CD.Text = this.hikiaiGyoushaEntity.CHIIKI_CD;
                    this.form.CHIIKI_NAME.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                }

                this.form.BUSHO.Text = this.hikiaiGyoushaEntity.BUSHO;
                this.form.TANTOUSHA.Text = this.hikiaiGyoushaEntity.TANTOUSHA;
                this.form.GYOUSHA_DAIHYOU.Text = this.hikiaiGyoushaEntity.GYOUSHA_DAIHYOU;
                if (this.shuukeiEntity != null)
                {
                    this.form.SHUUKEI_ITEM_CD.Text = this.hikiaiGyoushaEntity.SHUUKEI_ITEM_CD;
                    this.form.SHUUKEI_ITEM_NAME.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                }
                if (this.gyoushuEntity != null)
                {
                    this.form.GYOUSHU_CD.Text = this.hikiaiGyoushaEntity.GYOUSHU_CD;
                    this.form.GYOUSHU_NAME.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
                }
                this.form.BIKOU1.Text = this.hikiaiGyoushaEntity.BIKOU1;
                this.form.BIKOU2.Text = this.hikiaiGyoushaEntity.BIKOU2;
                this.form.BIKOU3.Text = this.hikiaiGyoushaEntity.BIKOU3;
                this.form.BIKOU4.Text = this.hikiaiGyoushaEntity.BIKOU4;

                if (this.form.TORIHIKISAKI_UMU_KBN.Text == "1")
                {
                    // 請求情報
                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.hikiaiGyoushaEntity.HAKKOUSAKI_CD;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SEIKYUU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    Boolean isKake = true;

                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = new M_TORIHIKISAKI_SEIKYUU();
                    M_HIKIAI_TORIHIKISAKI_SEIKYUU hikiaiSeikyuuEntity = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();

                    if (this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsTrue && this.hikiaiTorihikisakiEntity != null)
                    {
                        hikiaiSeikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(this.hikiaiTorihikisakiEntity.TORIHIKISAKI_CD);
                        if (hikiaiSeikyuuEntity != null)
                        {
                            // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                            if (hikiaiSeikyuuEntity.TORIHIKI_KBN_CD == 1)
                            {
                                isKake = false;
                            }
                            else if (hikiaiSeikyuuEntity.TORIHIKI_KBN_CD == 2)
                            {
                                isKake = true;
                            }
                        }
                    }
                    else if (this.torihikisakiEntity != null)
                    {
                        seikyuuEntity = this.daoSeikyuu.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);
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
                    }

                    if ((!isKake && this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled) || (isKake && !this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSeikyuuControl(isKake);
                    }

                    // 支払情報
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    isKake = true;

                    M_TORIHIKISAKI_SHIHARAI shiharaiEntity = new M_TORIHIKISAKI_SHIHARAI();
                    M_HIKIAI_TORIHIKISAKI_SHIHARAI hikiaishiharaiEntity = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();

                    if (this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsTrue && this.hikiaiTorihikisakiEntity != null)
                    {
                        hikiaishiharaiEntity = this.daoHikiaiShiharai.GetDataByCd(this.hikiaiTorihikisakiEntity.TORIHIKISAKI_CD);
                        if (hikiaishiharaiEntity != null)
                        {
                            // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                            if (hikiaishiharaiEntity.TORIHIKI_KBN_CD == 1)
                            {
                                isKake = false;
                            }
                            else if (hikiaishiharaiEntity.TORIHIKI_KBN_CD == 2)
                            {
                                isKake = true;
                            }
                        }
                    }
                    else if (this.torihikisakiEntity != null)
                    {
                        shiharaiEntity = this.daoShiharai.GetDataByCd(this.torihikisakiEntity.TORIHIKISAKI_CD);
                        if (shiharaiEntity != null)
                        {
                            // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                            if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                            {
                                isKake = false;
                            }
                            else if (shiharaiEntity.TORIHIKI_KBN_CD == 2)
                            {
                                isKake = true;
                            }
                        }
                    }

                    //非活性⇔活性
                    if ((!isKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSiharaiControl(isKake);
                    }
                }

                // 現場一覧
                if (this.sysinfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
                {
                    this.form.TORIHIKI_STOP.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKI_STOP.Text = this.sysinfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
                }
                this.rowCntGenba = this.SearchGenbaIchiran();

                if (this.rowCntGenba > 0)
                {
                    this.SetIchiranGenba();
                }

                // 業者分類
                if (!this.hikiaiGyoushaEntity.JISHA_KBN.IsNull)
                {
                    this.form.JISHA_KBN.Checked = (bool)this.hikiaiGyoushaEntity.JISHA_KBN;
                }
                else
                {
                    this.form.JISHA_KBN.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = (bool)this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                }
                else
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull)
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = (bool)this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                }
                else
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull)
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = (bool)this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                }
                else
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                }
                if (!this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = false;
                }

                //if (!this.form.Gyousha_KBN_3.Checked)
                //{
                //    //this.form.JISHA_KBN.Enabled = false;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
                //}
                //else
                //{
                //    //this.form.JISHA_KBN.Enabled = true;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;
                //}
                if (this.unpanHoukokushoTeishutsuChiikiEntity != null)
                {
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
                    this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = this.unpanHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;
                }

                if (this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                    if ("1".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    }
                    else 
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    }
                }
                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.MANI_HENSOUSAKI_NAME1.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.MANI_HENSOUSAKI_POST.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_POST;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_TANTOU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowDataFromHikiai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データを仮から取得し、画面に設定
        /// </summary>
        private void SetWindowDataFromKari()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 各種データ取得
                this.SearchKariGenba(this.GyoushaCd, "000000");
                this.SearchKariGyousha();
                this.SearchBusho();
                this.SearchChiiki();
                this.SearchGyoushu();
                this.SearchKyoten();
                this.SearchShain();
                this.SearchShuukeiItem();
                this.SearchTodoufuken();
                this.SearchKariTorihikisaki();
                this.GetSysInfo();

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = this.kariGyoushaEntity.CREATE_DATE.ToString();
                header.CreateUser.Text = this.kariGyoushaEntity.CREATE_USER;
                header.LastUpdateDate.Text = this.kariGyoushaEntity.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.kariGyoushaEntity.UPDATE_USER;

                // 共通部
                this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString(this.kariGyoushaEntity.TIME_STAMP);
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                //if (!this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG.IsNull)
                //{
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text =  "1" ;
                //}

                this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty; // 取引先有無区分が前回表示と同じだった場合入力許可となってしまうので一旦クリアしてイベント駆動するようにしておく

                if (!this.kariGyoushaEntity.TORIHIKISAKI_UMU_KBN.IsNull)
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = this.kariGyoushaEntity.TORIHIKISAKI_UMU_KBN.ToString();
                }
                else
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty;
                }

                if (!this.kariGyoushaEntity.GYOUSHAKBN_UKEIRE.IsNull)
                {
                    this.form.Gyousha_KBN_1.Checked = (bool)this.kariGyoushaEntity.GYOUSHAKBN_UKEIRE;
                }
                else
                {
                    this.form.Gyousha_KBN_1.Checked = false;
                }

                if (!this.kariGyoushaEntity.GYOUSHAKBN_SHUKKA.IsNull)
                {
                    this.form.Gyousha_KBN_2.Checked = (bool)this.kariGyoushaEntity.GYOUSHAKBN_SHUKKA;
                }
                else
                {
                    this.form.Gyousha_KBN_2.Checked = false;
                }

                if (!this.kariGyoushaEntity.GYOUSHAKBN_MANI.IsNull)
                {
                    // マニ記載のチェックを外す場合は各区分をクリアしておく
                    // ※チェックを外す際にエラーとなる為
                    var gyoushaKbnMani = this.kariGyoushaEntity.GYOUSHAKBN_MANI.Value;
                    if (!gyoushaKbnMani)
                    {
                        this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                        this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                        this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                        this.form.JISHA_KBN.Checked = false;
                    }
                    this.form.Gyousha_KBN_3.Checked = gyoushaKbnMani;
                }
                else
                {
                    this.form.Gyousha_KBN_3.Checked = false;
                }

                this.form.TORIHIKISAKI_CD.Text = this.kariGyoushaEntity.TORIHIKISAKI_CD;

                if (this.kariTorihikisakiEntity != null)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = this.kariTorihikisakiEntity.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = this.kariTorihikisakiEntity.TORIHIKISAKI_NAME2;
                }

                if (!this.kariGyoushaEntity.KYOTEN_CD.IsNull)
                {
                    this.form.KYOTEN_CD.Text = this.kariGyoushaEntity.KYOTEN_CD.Value.ToString();
                }
                if (this.kyotenEntity != null)
                {
                    this.form.KYOTEN_NAME.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                }


                this.form.GYOUSHA_CD.Text = this.kariGyoushaEntity.GYOUSHA_CD;
                this.form.GYOUSHA_NAME1.Text = this.kariGyoushaEntity.GYOUSHA_NAME1;
                this.form.GYOUSHA_NAME2.Text = this.kariGyoushaEntity.GYOUSHA_NAME2;
                this.form.GYOUSHA_FURIGANA.Text = this.kariGyoushaEntity.GYOUSHA_FURIGANA;
                this.form.GYOUSHA_NAME_RYAKU.Text = this.kariGyoushaEntity.GYOUSHA_NAME_RYAKU;
                this.form.GYOUSHA_KEISHOU1.Text = this.kariGyoushaEntity.GYOUSHA_KEISHOU1;
                this.form.GYOUSHA_KEISHOU2.Text = this.kariGyoushaEntity.GYOUSHA_KEISHOU2;
                this.form.GYOUSHA_TEL.Text = this.kariGyoushaEntity.GYOUSHA_TEL;
                this.form.GYOUSHA_KEITAI_TEL.Text = this.kariGyoushaEntity.GYOUSHA_KEITAI_TEL;
                this.form.GYOUSHA_FAX.Text = this.kariGyoushaEntity.GYOUSHA_FAX;
                if (this.bushoEntity != null)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.kariGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
                    this.form.BUSHO_NAME.Text = this.bushoEntity.BUSHO_NAME_RYAKU;
                }
                if (this.shainEntity != null)
                {
                    this.form.EIGYOU_TANTOU_CD.Text = this.kariGyoushaEntity.EIGYOU_TANTOU_CD;
                    this.form.SHAIN_NAME.Text = this.shainEntity.SHAIN_NAME_RYAKU;
                }

                if (this.kariGyoushaEntity.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_BEGIN.Value = this.kariGyoushaEntity.TEKIYOU_BEGIN.Value;
                }

                if (this.kariGyoushaEntity.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else
                {
                    this.form.TEKIYOU_END.Value = this.kariGyoushaEntity.TEKIYOU_END.Value;
                }

                if (this.kariGyoushaEntity != null)
                {
                    this.form.CHUUSHI_RIYUU1.Text = this.kariGyoushaEntity.CHUUSHI_RIYUU1;
                    this.form.CHUUSHI_RIYUU2.Text = this.kariGyoushaEntity.CHUUSHI_RIYUU2;
                }

                if (!this.kariGyoushaEntity.SHOKUCHI_KBN.IsNull)
                {
                    this.form.SHOKUCHI_KBN.Checked = (bool)this.kariGyoushaEntity.SHOKUCHI_KBN;
                }
                else
                {
                    this.form.SHOKUCHI_KBN.Checked = false;
                }

                // 基本情報
                this.form.GYOUSHA_POST.Text = this.kariGyoushaEntity.GYOUSHA_POST;
                if (!this.kariGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    if (this.todoufukenEntity != null)
                    {
                        this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.kariGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.ToString();
                        this.form.TODOUFUKEN_NAME.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
                    }
                }

                this.form.GYOUSHA_ADDRESS1.Text = this.kariGyoushaEntity.GYOUSHA_ADDRESS1;
                this.form.GYOUSHA_ADDRESS2.Text = this.kariGyoushaEntity.GYOUSHA_ADDRESS2;
                if (this.chiikiEntity != null)
                {
                    this.form.CHIIKI_CD.Text = this.kariGyoushaEntity.CHIIKI_CD;
                    this.form.CHIIKI_NAME.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                }

                this.form.BUSHO.Text = this.kariGyoushaEntity.BUSHO;
                this.form.TANTOUSHA.Text = this.kariGyoushaEntity.TANTOUSHA;
                this.form.GYOUSHA_DAIHYOU.Text = this.kariGyoushaEntity.GYOUSHA_DAIHYOU;
                if (this.shuukeiEntity != null)
                {
                    this.form.SHUUKEI_ITEM_CD.Text = this.kariGyoushaEntity.SHUUKEI_ITEM_CD;
                    this.form.SHUUKEI_ITEM_NAME.Text = this.shuukeiEntity.SHUUKEI_KOUMOKU_NAME_RYAKU;
                }
                if (this.gyoushuEntity != null)
                {
                    this.form.GYOUSHU_CD.Text = this.kariGyoushaEntity.GYOUSHU_CD;
                    this.form.GYOUSHU_NAME.Text = this.gyoushuEntity.GYOUSHU_NAME_RYAKU;
                }
                this.form.BIKOU1.Text = this.kariGyoushaEntity.BIKOU1;
                this.form.BIKOU2.Text = this.kariGyoushaEntity.BIKOU2;
                this.form.BIKOU3.Text = this.kariGyoushaEntity.BIKOU3;
                this.form.BIKOU4.Text = this.kariGyoushaEntity.BIKOU4;

                if (this.form.TORIHIKISAKI_UMU_KBN.Text == "1")
                {
                    // 請求情報
                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.kariGyoushaEntity.HAKKOUSAKI_CD;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.kariGyoushaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SEIKYUU_TANTOU.Text = this.kariGyoushaEntity.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (!this.kariGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.kariGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.kariGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.kariGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.kariGyoushaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.kariGyoushaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    Boolean isKake = true;

                    M_KARI_TORIHIKISAKI_SEIKYUU kariSeikyuuEntity = new M_KARI_TORIHIKISAKI_SEIKYUU();

                    if (this.kariTorihikisakiEntity != null)
                    {
                        kariSeikyuuEntity = this.daoKariSeikyuu.GetDataByCd(this.kariTorihikisakiEntity.TORIHIKISAKI_CD);
                        if (kariSeikyuuEntity != null)
                        {
                            // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                            if (kariSeikyuuEntity.TORIHIKI_KBN_CD == 1)
                            {
                                isKake = false;
                            }
                            else if (kariSeikyuuEntity.TORIHIKI_KBN_CD == 2)
                            {
                                isKake = true;
                            }
                        }
                    }

                    if ((!isKake && this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled) || (isKake && !this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSeikyuuControl(isKake);
                    }

                    // 支払情報
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.kariGyoushaEntity.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.kariGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.kariGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.kariGyoushaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.kariGyoushaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    isKake = true;

                    M_KARI_TORIHIKISAKI_SHIHARAI karishiharaiEntity = new M_KARI_TORIHIKISAKI_SHIHARAI();

                    if (this.kariTorihikisakiEntity != null)
                    {
                        karishiharaiEntity = this.daoKariShiharai.GetDataByCd(this.kariTorihikisakiEntity.TORIHIKISAKI_CD);
                        if (karishiharaiEntity != null)
                        {
                            // 取引先区分が[1.現金]時には[請求情報タブ]内部を非活性
                            if (karishiharaiEntity.TORIHIKI_KBN_CD == 1)
                            {
                                isKake = false;
                            }
                            else if (karishiharaiEntity.TORIHIKI_KBN_CD == 2)
                            {
                                isKake = true;
                            }
                        }
                    }

                    //非活性⇔活性
                    if ((!isKake && this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled) || (isKake && !this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled))
                    {
                        this.ChangeSiharaiControl(isKake);
                    }
                }

                // 現場一覧
                if (this.sysinfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.IsNull)
                {
                    this.form.TORIHIKI_STOP.Text = string.Empty;
                }
                else
                {
                    this.form.TORIHIKI_STOP.Text = this.sysinfoEntity.GYOUSHA_TORIHIKI_CHUUSHI.ToString();
                }
                this.rowCntGenba = this.SearchKariGenbaIchiran();

                if (this.rowCntGenba > 0)
                {
                    this.SetIchiranGenba();
                }

                // 業者分類
                if (!this.kariGyoushaEntity.JISHA_KBN.IsNull)
                {
                    this.form.JISHA_KBN.Checked = (bool)this.kariGyoushaEntity.JISHA_KBN;
                }
                else
                {
                    this.form.JISHA_KBN.Checked = false;
                }
                if (!this.kariGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = (bool)this.kariGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                }
                else
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                }
                if (!this.kariGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull)
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = (bool)this.kariGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN;
                }
                else
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                }
                if (!this.kariGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull)
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = (bool)this.kariGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                }
                else
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                }
                if (!this.kariGyoushaEntity.MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.kariGyoushaEntity.MANI_HENSOUSAKI_KBN;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = false;
                }

                //if (!this.form.Gyousha_KBN_3.Checked)
                //{
                //    //this.form.JISHA_KBN.Enabled = false;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
                //}
                //else
                //{
                //    //this.form.JISHA_KBN.Enabled = true;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;
                //}

                if (this.kariGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.kariGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                    if ("1".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    }
                }
                if ("2".Equals(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
                    this.form.MANI_HENSOUSAKI_NAME1.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_NAME1;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_NAME2;
                    this.form.MANI_HENSOUSAKI_POST.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_POST;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_BUSHO;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = this.kariGyoushaEntity.MANI_HENSOUSAKI_TANTOU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowDataFromHikiai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 全コントロール制御メソッド

        /// <summary>
        /// テキストボックス制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void TextBoxLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                // 共通部
                this.form.TORIHIKISAKI_UMU_KBN.ReadOnly = isBool;

                this.form.TORIHIKISAKI_CD.ReadOnly = isBool;
                this.form.KYOTEN_CD.ReadOnly = isBool;
                this.form.GYOUSHU_CD.ReadOnly = isBool;
                this.form.GYOUSHA_NAME1.ReadOnly = isBool;
                this.form.GYOUSHA_NAME2.ReadOnly = isBool;
                this.form.GYOUSHA_FURIGANA.ReadOnly = isBool;
                this.form.GYOUSHA_NAME_RYAKU.ReadOnly = isBool;
                this.form.GYOUSHA_TEL.ReadOnly = isBool;
                this.form.GYOUSHA_KEITAI_TEL.ReadOnly = isBool;
                this.form.GYOUSHA_FAX.ReadOnly = isBool;
                this.form.EIGYOU_TANTOU_BUSHO_CD.ReadOnly = isBool;
                this.form.EIGYOU_TANTOU_CD.ReadOnly = isBool;
                this.form.TEKIYOU_BEGIN.ReadOnly = isBool;
                this.form.TEKIYOU_END.ReadOnly = isBool;
                this.form.CHUUSHI_RIYUU1.ReadOnly = isBool;
                this.form.CHUUSHI_RIYUU2.ReadOnly = isBool;

                // 基本情報
                this.form.GYOUSHA_POST.ReadOnly = isBool;
                this.form.GYOUSHA_TODOUFUKEN_CD.ReadOnly = isBool;
                this.form.GYOUSHA_ADDRESS1.ReadOnly = isBool;
                this.form.GYOUSHA_ADDRESS2.ReadOnly = isBool;
                this.form.CHIIKI_CD.ReadOnly = isBool;
                this.form.BUSHO.ReadOnly = isBool;
                this.form.TANTOUSHA.ReadOnly = isBool;
                this.form.GYOUSHA_DAIHYOU.ReadOnly = isBool;
                this.form.SHUUKEI_ITEM_CD.ReadOnly = isBool;
                this.form.GYOUSHA_CD.ReadOnly = isBool;
                this.form.BIKOU1.ReadOnly = isBool;
                this.form.BIKOU2.ReadOnly = isBool;
                this.form.BIKOU3.ReadOnly = isBool;
                this.form.BIKOU4.ReadOnly = isBool;

                // 請求情報
                this.form.SEIKYUU_SOUFU_NAME1.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_NAME2.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_POST.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS1.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS2.ReadOnly = isBool;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.form.HAKKOUSAKI_CD.ReadOnly = isBool;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.form.SEIKYUU_SOUFU_BUSHO.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_TANTOU.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_TEL.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_FAX.ReadOnly = isBool;
                this.form.SEIKYUU_TANTOU.ReadOnly = isBool;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.ReadOnly = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.ReadOnly = isBool;
                this.form.SEIKYUU_KYOTEN_CD.ReadOnly = isBool;

                // 支払情報
                this.form.SHIHARAI_SOUFU_NAME1.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_NAME2.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_POST.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS1.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS2.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_BUSHO.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_TANTOU.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_TEL.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_FAX.ReadOnly = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_KYOTEN_CD.ReadOnly = isBool;

                // 現場一覧
                this.form.TORIHIKI_STOP.ReadOnly = isBool;

                // 業者分類
                this.form.GENBA_ICHIRAN.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_NAME1.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_NAME2.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_POST.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS1.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS2.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_BUSHO.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_TANTOU.ReadOnly = isBool;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.ReadOnly = isBool;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TextBoxLock", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                // 共通部
                this.form.Torihiki_ari.Enabled = isBool;
                this.form.Torihiki_nashi.Enabled = isBool;
                this.form.Gyousha_KBN_1.Enabled = isBool;
                this.form.Gyousha_KBN_2.Enabled = isBool;
                this.form.Gyousha_KBN_3.Enabled = isBool;

                this.form.GYOUSHA_KEISHOU1.Enabled = isBool;
                this.form.GYOUSHA_KEISHOU2.Enabled = isBool;
                this.form.SHOKUCHI_KBN.Enabled = isBool;

                // 請求情報
                this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = isBool;
                this.form.rdb_seikyuu_suru.Enabled = isBool;
                this.form.rdb_seikyuu_shinai.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isBool;

                // 支払情報
                this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isBool;

                // 現場一覧
                this.form.rdb_genba_shinai.Enabled = isBool;
                this.form.rdb_genba_suru.Enabled = isBool;

                // 業者分類
                this.form.JISHA_KBN.Enabled = isBool;
                this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = isBool;
                this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = isBool;
                this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KBN.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = isBool;
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
        /// 業者CDと現場CDで引合現場マスタを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>件数</returns>
        public int SearchGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            var ret = 0;
            this.hikiaiGenbaEntity = daoGenba.GetDataByCd(new M_HIKIAI_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd });
            if (null != this.hikiaiGenbaEntity)
            {
                ret = 1;
            }

            LogUtility.DebugMethodEnd();

            return ret;
        }

        /// <summary>
        /// 業者CDと現場CDで仮現場マスタを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>件数</returns>
        public int SearchKariGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            var ret = 0;
            this.kariGenbaEntity = daoKariGenba.GetDataByCd(new M_KARI_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd });
            if (null != this.kariGenbaEntity)
            {
                ret = 1;
            }

            LogUtility.DebugMethodEnd();

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

                this.hikiaiGyoushaEntity = null;
                this.hikiaiGyoushaEntity = daoHikiaiGyousha.GetDataByCd(this.GyoushaCd);

                count = this.hikiaiGyoushaEntity == null ? 0 : 1;
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
        /// データ取得処理(仮業者)
        /// </summary>
        /// <returns></returns>
        public int SearchKariGyousha()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.kariGyoushaEntity = null;
                this.kariGyoushaEntity = daoKariGyousha.GetDataByCd(this.GyoushaCd);

                count = this.kariGyoushaEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKariGyousha", ex);
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

                this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.hikiaiGyoushaEntity.TORIHIKISAKI_CD);

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

                this.hikiaiTorihikisakiEntity = daoHikiaiTorihikisaki.GetDataByCd(this.hikiaiGyoushaEntity.TORIHIKISAKI_CD);

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
        /// データ取得処理(仮取引先)
        /// </summary>
        /// <returns></returns>
        public int SearchKariTorihikisaki()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.kariTorihikisakiEntity = null;

                this.kariTorihikisakiEntity = daoKariTorihikisaki.GetDataByCd(this.kariGyoushaEntity.TORIHIKISAKI_CD);

                count = this.kariTorihikisakiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKariTorihikisaki", ex);
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
                    if (this.GyoshaKbn == 1)
                    {
                        if (!this.hikiaiGyoushaEntity.KYOTEN_CD.IsNull)
                        {
                            this.kyotenEntity = daoKyoten.GetDataByCd(this.hikiaiGyoushaEntity.KYOTEN_CD.ToString());
                        }
                    }
                    else if (this.GyoshaKbn == 0)
                    {
                        if (!this.kariGyoushaEntity.KYOTEN_CD.IsNull)
                        {
                            this.kyotenEntity = daoKyoten.GetDataByCd(this.kariGyoushaEntity.KYOTEN_CD.ToString());
                        }
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

                if (this.GyoshaKbn == 1)
                {
                    this.bushoEntity = daoBusho.GetDataByCd(this.hikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD);
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.bushoEntity = daoBusho.GetDataByCd(this.kariGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD);
                }

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

                if (this.GyoshaKbn == 1)
                {
                    this.shainEntity = daoShain.GetDataByCd(this.hikiaiGyoushaEntity.EIGYOU_TANTOU_CD);
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.shainEntity = daoShain.GetDataByCd(this.kariGyoushaEntity.EIGYOU_TANTOU_CD);
                }

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

                if (this.GyoshaKbn == 1)
                {
                    if (!this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.Value.ToString());
                    }
                }
                else if (this.GyoshaKbn == 0)
                {
                    if (!this.kariGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                    {
                        this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.kariGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.Value.ToString());
                    }
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

                if (this.GyoshaKbn == 1)
                {
                    this.chiikiEntity = daoChiiki.GetDataByCd(this.hikiaiGyoushaEntity.CHIIKI_CD);
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.chiikiEntity = daoChiiki.GetDataByCd(this.kariGyoushaEntity.CHIIKI_CD);
                }

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

                if (this.GyoshaKbn == 1)
                {
                    this.unpanHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);
                }

                count = this.unpanHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchUnpanHoukokushoTeishutsuChiiki", ex);
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

                if (this.GyoshaKbn == 1)
                {
                    this.shuukeiEntity = daoShuukei.GetDataByCd(this.hikiaiGyoushaEntity.SHUUKEI_ITEM_CD);
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.shuukeiEntity = daoShuukei.GetDataByCd(this.kariGyoushaEntity.SHUUKEI_ITEM_CD);
                }

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

                if (this.GyoshaKbn == 1)
                {
                    this.gyoushuEntity = daoGyoushu.GetDataByCd(this.hikiaiGyoushaEntity.GYOUSHU_CD);
                }
                else if (this.GyoshaKbn == 0)
                {
                    this.gyoushuEntity = daoGyoushu.GetDataByCd(this.kariGyoushaEntity.GYOUSHU_CD);
                }

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
        /// データ取得処理(現場)
        /// </summary>
        /// <returns></returns>
        public int SearchGenbaIchiran()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();

                string gyoushaCd = this.hikiaiGyoushaEntity.GYOUSHA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    return 0;
                }

                M_HIKIAI_GENBA condition = new M_HIKIAI_GENBA();
                condition.GYOUSHA_CD = gyoushaCd;
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKI_STOP.Text))
                {
                    condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.TORIHIKI_STOP.Text);
                }

                this.SearchResultGenba = daoHikiaiGyousha.GetIchiranGenbaData(condition);

                count = this.SearchResultGenba.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenba", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        /// <summary>
        /// データ取得処理(仮現場)
        /// </summary>
        /// <returns></returns>
        public int SearchKariGenbaIchiran()
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();

                string gyoushaCd = this.kariGyoushaEntity.GYOUSHA_CD;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    return 0;
                }

                M_KARI_GENBA condition = new M_KARI_GENBA();
                condition.GYOUSHA_CD = gyoushaCd;
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKI_STOP.Text))
                {
                    condition.TORIHIKI_JOUKYOU = Int16.Parse(this.form.TORIHIKI_STOP.Text);
                }

                this.SearchResultGenba = daoKariGyousha.GetIchiranGenbaData(condition);

                count = this.SearchResultGenba.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKariGenbaIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }

        #region 登録/更新/削除
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
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
        /// 検索結果を現場一覧に設定
        /// </summary>
        internal void SetIchiranGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var table = this.SearchResultGenba;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.GENBA_ICHIRAN.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranGenba", ex);
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
        private void ChangeItakuStatus(DataTable table)
        {
            try
            {
                LogUtility.DebugMethodStart(table);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // 委託契約種類変換
                    string keyShurui = table.Rows[i][ConstCls.ITAKU_SHURUI].ToString();
                    string shurui = this.ItakuKeiyakuShurui[keyShurui];
                    table.Rows[i][ConstCls.ITAKU_SHURUI] = shurui;

                    // 委託契約ステータス変換
                    string keyStatus = table.Rows[i][ConstCls.ITAKU_STATUS].ToString();
                    string status = this.ItakuKeiyakuStatus[keyStatus];
                    table.Rows[i][ConstCls.ITAKU_STATUS] = status;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeItakuStatus", ex);
                throw;
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
                LogUtility.Error("EventInit", ex);
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
                result = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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
        /// 現場一覧検索
        /// </summary>
        public void TorihikiStopIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGenba = new DataTable();

                this.rowCntGenba = this.SearchGenbaIchiran();
                if (this.rowCntGenba > 0)
                {
                    this.SetIchiranGenba();
                }
                else
                {
                    this.form.GENBA_ICHIRAN.DataSource = null;
                    this.form.GENBA_ICHIRAN.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikiStopIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// タブ表示制御
        /// </summary>
        public void TabDispControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_UMU_KBN.Text))
                {
                    //使用不能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = true;
                    this.form.TORIHIKISAKI_CD.Enabled = false;
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);

                    return;
                }

                if (int.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text) == 1)
                {
                    // 使用可能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = false;
                    this.form.TORIHIKISAKI_CD.Enabled = true;
                    _tabPageManager.ChangeTabPageVisible(1, true);
                    _tabPageManager.ChangeTabPageVisible(2, true);

                    // 請求情報
                    if (this.hikiaiGyoushaEntity == null)
                    {
                        return;
                    }

                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.hikiaiGyoushaEntity.HAKKOUSAKI_CD;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SEIKYUU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }

                    // 支払情報
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD.Value.ToString()));
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                }
                else
                {
                    //使用不能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = true;
                    this.form.TORIHIKISAKI_CD.Enabled = false;
                    this.form.TORIHIKISAKI_CD.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                    this.form.KYOTEN_CD.Text = string.Empty;
                    this.form.KYOTEN_NAME.Text = string.Empty;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TabDispControl", ex);
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

                FormManager.OpenFormWithAuth("M485", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.HIKIAI_GYOUSHA);
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

        /// <summary>
        /// 取引先区分に基づくコントロールの変更処理(請求)
        /// </summary>
        /// <param name="isKake"> 取引先区分が[1.現金]時に true</param>
        private void ChangeSeikyuuControl(Boolean isKake)
        {
            try
            {
                LogUtility.DebugMethodStart(isKake);

                this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;      // 請求書送付先1
                this.form.SEIKYUU_SOUFU_NAME1.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;   // 請求書送付先敬称1
                this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;      // 請求書送付先2
                this.form.SEIKYUU_SOUFU_NAME2.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;   // 請求書送付先敬称2
                this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;       // 送付先郵便番号
                this.form.SEIKYUU_SOUFU_POST.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;   // 送付先住所１
                this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;   // 送付先住所２
                this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;      // 送付先部署
                this.form.SEIKYUU_SOUFU_BUSHO.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;     // 送付先担当者
                this.form.SEIKYUU_SOUFU_TANTOU.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;        // 送付先電話番号
                this.form.SEIKYUU_SOUFU_TEL.Enabled = isKake;

                this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;        // 送付先FAX番号
                this.form.SEIKYUU_SOUFU_FAX.Enabled = isKake;

                this.form.SEIKYUU_TANTOU.Text = string.Empty;           // 請求担当者
                this.form.SEIKYUU_TANTOU.Enabled = isKake;

                // 請求書代表印字区分
                if (!isKake)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                }
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }

                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = isKake;
                this.form.rdb_seikyuu_suru.Enabled = isKake;
                this.form.rdb_seikyuu_shinai.Enabled = isKake;

                // 請求書拠点印字区分
                if (!isKake)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                }
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
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
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
                }

                M_KYOTEN seikyuuKyoten = this.daoKyoten.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (this.form.SEIKYUU_KYOTEN_CD.Text != string.Empty)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }

                this.ChangeSeikyuuKyotenPrintKbn();
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

                this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;         // 支払明細書送付先1
                this.form.SHIHARAI_SOUFU_NAME1.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;      // 支払明細書送付先敬称1
                this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;         // 支払明細書送付先2
                this.form.SHIHARAI_SOUFU_NAME2.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;      // 支払明細書送付先敬称2
                this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;          // 送付先郵便番号
                this.form.SHIHARAI_SOUFU_POST.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;      // 送付先住所１
                this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;      // 送付先住所２
                this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;         // 送付先部署
                this.form.SHIHARAI_SOUFU_BUSHO.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;        // 送付先担当者
                this.form.SHIHARAI_SOUFU_TANTOU.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;           // 送付先電話番号
                this.form.SHIHARAI_SOUFU_TEL.Enabled = isKake;

                this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;           // 送付先FAX番号
                this.form.SHIHARAI_SOUFU_FAX.Enabled = isKake;

                // 支払書拠点印字区分
                if (!isKake)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                }
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
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
                else if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                }

                M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (this.form.SHIHARAI_KYOTEN_CD.Text != string.Empty)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }

                this.ChangeShiharaiKyotenPrintKbn();
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
        public void ChangeSeikyuuKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
                        this.SeikyuuKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        public void ChangeShiharaiKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                        this.ShiharaiKyotenCdValidated();
                    }
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                    if (kyo != null && kyo.KYOTEN_CD != 99)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SEIKYUU_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SEIKYUU_KYOTEN_CD.Text = padData;
                        this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        ret = false;
                    }
                }

                if (!ret)
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                throw;
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
                    if (kyo != null && kyo.KYOTEN_CD != 99)
                    {
                        string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.SHIHARAI_KYOTEN_CD.CharactersNumber, '0');
                        this.form.SHIHARAI_KYOTEN_CD.Text = padData;
                        this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "拠点");
                        ret = false;
                    }
                }

                if (!ret)
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
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
