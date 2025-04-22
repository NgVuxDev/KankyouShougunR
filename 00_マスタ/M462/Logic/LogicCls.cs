// $Id: HikiaiGyoushaLogic.cs 462 2013-10-23 18:11:12Z maedomari $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
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
using Shougun.Core.Master.HikiaiGyousha.APP;
using Shougun.Core.Master.HikiaiGyousha.Const;
using Shougun.Core.Master.HikiaiGyousha.DBAccesser;
using Shougun.Core.Master.HikiaiGyousha.Validator;

namespace Shougun.Core.Master.HikiaiGyousha.Logic
{
    /// <summary>
    /// 引合業者保守画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.HikiaiGyousha.Setting.ButtonSetting.xml";

        private readonly string ButtonInfoXmlPath2 = "Shougun.Core.Master.HikiaiGyousha.Setting.ButtonSetting2.xml";

        /// <summary>
        /// 業者保守画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        internal M_HIKIAI_GYOUSHA hikiaiGyoushaEntity;

        private M_HIKIAI_TORIHIKISAKI hikiaiTorihikisakiEntity;

        private M_TORIHIKISAKI torihikisakiEntity;

        private M_KYOTEN kyotenEntity;

        private M_BUSHO bushoEntity;

        private M_SHAIN shainEntity;

        private M_TODOUFUKEN todoufukenEntity;

        private M_CHIIKI chiikiEntity;

        private M_SHUUKEI_KOUMOKU shuukeiEntity;

        private M_GYOUSHU gyoushuEntity;

        private M_SYS_INFO sysinfoEntity;

        private int rowCntGenba;

        private int rowCntItaku;

        /// <summary>
        /// 業者のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GYOUSHADao daoHikiaiGyousha;

        /// <summary>
        /// 営業担当者のDao
        /// </summary>
        private IM_EIGYOU_TANTOUSHADao daoEigyou;

        /// <summary>
        /// 現場のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GENBADao daoGenba;

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
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKIDao daoHikiaiTorihikisaki;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao daoHikiaiSeikyuu;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao daoHikiaiShiharai;

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

        // 20141209 katen #2927 実績報告書 フィードバック対応 start
        private M_CHIIKI upnHoukokushoTeishutsuChiikiEntity;
        // 20141209 katen #2927 実績報告書 フィードバック対応 end

        /// <summary>
        /// 業種のDao
        /// </summary>
        private IM_GYOUSHUDao daoGyoushu;

        /// <summary>
        /// タブコントロール制御用
        /// </summary>
        private TabPageManager _tabPageManager = null;

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

        private UIForm hikiaiGyoushaForm;

        /// <summary>
        /// 採番でエラーが起きたか判断するフラグ
        /// </summary>
        internal bool SaibanErrorFlg = false;

        /// <summary>
        /// 電子申請オプションフラグ
        /// true = 有効 / false = 無効
        /// </summary>
        private bool DensisinseiOptionEnabledFlg;

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
        /// 電子申請フラグ
        /// </summary>
        public bool denshiShinseiFlg { get; set; }

        /// <summary>
        /// 自社区分チェック
        /// </summary>
        public bool FlgJishaKbn { get; set; }

        /// <summary>
        /// 排出事業者区分チェック
        /// </summary>
        public bool FlgHaishutsuJigyoushaKbn { get; set; }

        /// <summary>
        /// 運搬受託者区分チェック
        /// </summary>
        public bool FlgUnpanJutakushaKbn { get; set; }

        /// <summary>
        /// 処分受託者区分チェック
        /// </summary>
        public bool FlgShobunJutakushaKbn { get; set; }

        /// <summary>
        /// マニ返送先区分チェック
        /// </summary>
        public bool FlgManiHensousakiKbn { get; set; }

        /// <summary>
        /// 検索結果(現場一覧)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// ポップアップからデータセットを実施したか否かのフラグ
        /// </summary>
        public bool IsSetDataFromPopup { get; set; }

        /// <summary>
        /// 初期化時にデータセットを実施したか否かのフラグ
        /// </summary>
        public bool IsSetDataInInit { get; set; }

        /// <summary>
        /// エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// 現場マスタ追加フラグ
        /// </summary>
        public bool IsHikiaiGenbaAdd { get; set; }

        /// <summary>
        /// 現場マスタ（現場CD=000000）
        /// </summary>
        public M_HIKIAI_GENBA hikiaiGenbaEntity;

        // 201400709 syunrei #947 №19 start
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;
        // 201400709 syunrei #947 №19 end

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
                this.daoHikiaiGyousha = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GYOUSHADao>();
                this.daoEigyou = DaoInitUtility.GetComponent<IM_EIGYOU_TANTOUSHADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoGenba = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_GENBADao>();
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
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoHikiaiSeikyuu = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
                this.daoHikiaiShiharai = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGyousha.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();
                // 201400709 syunrei #947 №19 start
                this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                // 201400709 syunrei #947 №19 end
                _tabPageManager = new TabPageManager(this.form.JOHOU);
                this.IsSetDataFromPopup = false;
                this.IsSetDataInInit = false;
                //this.DensisinseiOptionEnabledFlg = r_framework.Configuration.AppConfig.AppOptions.Workflow;
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
                this.allControl = this.form.allControl;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                // 201400709 syunrei #947 №19 start
                //修正モードの場合、F1移行ボタンが利用可
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
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parentForm"></param>
        private void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
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

                if (string.IsNullOrEmpty(this.GyoushaCd))
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
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロール操作可能とする
                this.AllControlLock(true);

                // BaseHeader部
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 共通部
                this.form.TIME_STAMP.Text = string.Empty;
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;
                //this.form.TORIHIKISAKI_UMU_KBN.Text = "1";
                this.form.Gyousha_KBN_1.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_UKEIRE;
                this.form.Gyousha_KBN_2.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_SHUKKA;
                this.form.Gyousha_KBN_3.Checked = (bool)this.sysinfoEntity.GYOUSHA_KBN_MANI;
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                this.form.KYOTEN_CD.Text = string.Empty;
                this.form.KYOTEN_NAME.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME1.Text = string.Empty;
                this.form.GYOUSHA_NAME2.Text = string.Empty;
                this.form.GYOUSHA_FURIGANA.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_KEISHOU1.Text = string.Empty;
                this.form.GYOUSHA_KEISHOU2.Text = string.Empty;
                this.form.GYOUSHA_TEL.Text = string.Empty;
                this.form.GYOUSHA_KEITAI_TEL.Text = string.Empty;
                this.form.GYOUSHA_FAX.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                this.form.BUSHO_NAME.Text = string.Empty;
                this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.form.SHAIN_NAME.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate;
                this.form.TEKIYOU_END.Value = null;
                this.form.CHUUSHI_RIYUU1.Text = string.Empty;
                this.form.CHUUSHI_RIYUU2.Text = string.Empty;
                this.form.SHOKUCHI_KBN.Checked = false;
                this.form.TORIHIKISAKI_UMU_KBN.Text = "1";

                // 基本情報
                this.form.GYOUSHA_POST.Text = string.Empty;
                this.form.GYOUSHA_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TODOUFUKEN_NAME.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS1.Text = string.Empty;
                this.form.GYOUSHA_ADDRESS2.Text = string.Empty;
                this.form.CHIIKI_CD.Text = string.Empty;
                this.form.CHIIKI_NAME.Text = string.Empty;
                this.form.BUSHO.Text = string.Empty;
                this.form.TANTOUSHA.Text = string.Empty;
                this.form.GYOUSHA_DAIHYOU.Text = string.Empty;
                this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                this.form.SHUUKEI_ITEM_NAME.Text = string.Empty;
                this.form.GYOUSHU_CD.Text = string.Empty;
                this.form.GYOUSHU_NAME.Text = string.Empty;
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;
                this.form.BIKOU3.Text = string.Empty;
                this.form.BIKOU4.Text = string.Empty;

                // 請求情報
                this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                this.form.SEIKYUU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
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
                this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                if (this.sysinfoEntity != null && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                }
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN shiharaiKyoten = this.daoKyoten.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                this.ChangeShiharaiKyotenPrintKbn();

                this.form.GENBA_ICHIRAN.DataSource = null;
                this.form.GENBA_ICHIRAN.Rows.Clear();

                // 業者分類
                this.form.JISHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_JISHA_KBN;
                if (!sysinfoEntity.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull)
                {
                    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull)
                {
                    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull)
                {
                    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN;
                }

                if (!sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)sysinfoEntity.GYOUSHA_MANI_HENSOUSAKI_KBN;
                }
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

                //if (!this.form.Gyousha_KBN_3.Checked)
                //{
                //    //this.form.JISHA_KBN.Enabled = false;
                //    this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
                //    this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
                //    this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
                //}

                // 20141209 katen #2927 実績報告書 フィードバック対応 start
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;
                // 20141209 katen #2927 実績報告書 フィードバック対応 end

                //set TORIHIKISAKI_UMU_KBN from system
                if (this.sysinfoEntity != null)
                {
                    if (!this.sysinfoEntity.TORIHIKISAKI_UMU_KBN.IsNull)
                    {
                        this.form.TORIHIKISAKI_UMU_KBN.Text = Convert.ToString(this.sysinfoEntity.TORIHIKISAKI_UMU_KBN);
                    }
                }

                // functionボタン
                this.SetF1Enabled(this.form.WindowType);// 移行
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                // 業者分類タブ初期化
                this.ManiCheckOffCheck();

                this.hikiaiGyoushaEntity = null;
                this.hikiaiGenbaEntity = null;

                this.form.GYOUSHA_CD.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitNewMode", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                //複写モード時は業者コードのコピーはなし
                this.hikiaiGyoushaEntity.GYOUSHA_CD = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.GyoushaCd = string.Empty;

                // 複写モード時は、紐付く現場はないためクリア
                this.hikiaiGenbaEntity = null;

                //適用開始日(当日日付)
                this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate;
                //適用終了日
                this.form.TEKIYOU_END.Value = null;
                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 発行先コード
                this.form.HAKKOUSAKI_CD.Text = string.Empty;

                //現場一覧クリア
                this.form.GENBA_ICHIRAN.DataSource = null;
                this.form.GENBA_ICHIRAN.Rows.Clear();

                // 業者分類タブ初期化
                this.ManiCheckOffCheck();
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
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.GYOUSHA_CD.Enabled = false;   // 業者CD
                this.form.bt_gyoushacd_saiban.Enabled = false;    // 採番ボタン

                // functionボタン
                // 電子申請内容選択入力から呼ばれた場合
                if (this.denshiShinseiFlg)
                {
                    parentForm.bt_func9.Enabled = true;     // 申請
                    parentForm.bt_func12.Enabled = true;    // 閉じる
                }
                else
                {
                    this.SetF1Enabled(this.form.WindowType);                    // 移行
                    parentForm.bt_func3.Enabled = false;    // 修正
                    parentForm.bt_func9.Enabled = true;     // 登録
                    parentForm.bt_func11.Enabled = true;    // 取消
                }
                // 業者分類タブ初期化
                this.ManiCheckOffCheck();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitUpdate", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                parentForm.bt_func3.Enabled = true;     // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
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
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 参照モード固有UI設定
                this.AllControlLock(false);

                // functionボタン
                parentForm.bt_func3.Enabled = true;     // 修正
                parentForm.bt_func9.Enabled = false;     // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitReference", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.IsSetDataInInit = true;

                // 各種データ取得
                this.SearchGenba(this.GyoushaCd, "000000");
                this.SearchGyousha();
                this.SearchBusho();
                this.SearchChiiki();
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

                // No.3988修正時発見不具合修正-->
                //this.form.TORIHIKISAKI_UMU_KBN.Text = this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN.ToString();
                if (!this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN.IsNull)
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN.ToString();
                }
                else
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = string.Empty;
                }
                // No.3988修正時発見不具合修正<--

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

                // 20140716 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する start
                /*if (this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG)
                {
                    string torihikisaki = string.Empty;
                    torihikisaki = this.getTorihikisakiAFTER(this.hikiaiGyoushaEntity.TORIHIKISAKI_CD);
                    if (torihikisaki != "" && !string.IsNullOrEmpty(torihikisaki))
                    {
                        this.form.TORIHIKISAKI_CD.Text = torihikisaki;
                       // hikiaiTorihikisakiUseFlg = false;
                        this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                    }
                }*/
                // 20140716 chinchisi EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end

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

                this.form.GYOUSHA_CD.Text = this.hikiaiGyoushaEntity.GYOUSHA_CD;  //TODO:ここのCDを引数で渡されるか？
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

                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.hikiaiGyoushaEntity.HAKKOUSAKI_CD;
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end

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

                if (this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                }
                else
                {
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
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
                // 20141209 katen #2927 実績報告書 フィードバック対応 start
                this.SearchUpnHoukokushoTeishutsuChiiki();
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = (this.upnHoukokushoTeishutsuChiikiEntity == null) ? string.Empty : this.upnHoukokushoTeishutsuChiikiEntity.CHIIKI_NAME_RYAKU;
                // 20141209 katen #2927 実績報告書 フィードバック対応 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw;
            }
            finally
            {
                this.IsSetDataInInit = false;
                LogUtility.DebugMethodEnd();
            }
        }

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

                // 共通部
                this.form.TORIHIKISAKI_UMU_KBN.Enabled = isBool;
                this.form.Torihiki_ari.Enabled = isBool;
                this.form.Torihiki_nashi.Enabled = isBool;
                this.form.Gyousha_KBN_1.Enabled = isBool;
                this.form.Gyousha_KBN_2.Enabled = isBool;
                this.form.Gyousha_KBN_3.Enabled = isBool;

                this.form.TORIHIKISAKI_CD.Enabled = isBool;
                this.form.bt_torihikisaki_copy.Enabled = isBool;
                this.form.bt_torihikisaki_create.Enabled = isBool;
                this.form.BT_TORIHIKISAKI_REFERENCE.Enabled = isBool;
                this.form.bt_torihikisaki_search.Enabled = isBool;
                this.form.KYOTEN_CD.Enabled = isBool;
                this.form.GYOUSHU_CD.Enabled = isBool;
                this.form.bt_gyoushacd_saiban.Enabled = isBool;
                this.form.GYOUSHA_NAME1.Enabled = isBool;
                this.form.GYOUSHA_NAME2.Enabled = isBool;
                this.form.GYOUSHA_FURIGANA.Enabled = isBool;
                this.form.GYOUSHA_NAME_RYAKU.Enabled = isBool;
                this.form.GYOUSHA_KEISHOU1.Enabled = isBool;
                this.form.GYOUSHA_KEISHOU2.Enabled = isBool;
                this.form.GYOUSHA_TEL.Enabled = isBool;
                this.form.GYOUSHA_KEITAI_TEL.Enabled = isBool;
                this.form.GYOUSHA_FAX.Enabled = isBool;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Enabled = isBool;
                this.form.EIGYOU_TANTOU_CD.Enabled = isBool;
                this.form.bt_tantousha_search.Enabled = isBool;
                this.form.bt_tantoubusho_search.Enabled = isBool;
                this.form.TEKIYOU_BEGIN.Enabled = isBool;
                this.form.TEKIYOU_END.Enabled = isBool;
                this.form.CHUUSHI_RIYUU1.Enabled = isBool;
                this.form.CHUUSHI_RIYUU2.Enabled = isBool;
                this.form.SHOKUCHI_KBN.Enabled = isBool;

                // 基本情報
                this.form.GYOUSHA_POST.Enabled = isBool;
                this.form.bt_address.Enabled = isBool;
                this.form.GYOUSHA_TODOUFUKEN_CD.Enabled = isBool;
                this.form.bt_todoufuken_search.Enabled = isBool;
                this.form.bt_post.Enabled = isBool;
                this.form.GYOUSHA_ADDRESS1.Enabled = isBool;
                this.form.GYOUSHA_ADDRESS2.Enabled = isBool;
                this.form.CHIIKI_CD.Enabled = isBool;
                this.form.bt_chiiki_search.Enabled = isBool;
                this.form.BUSHO.Enabled = isBool;
                this.form.TANTOUSHA.Enabled = isBool;
                this.form.GYOUSHA_DAIHYOU.Enabled = isBool;
                this.form.SHUUKEI_ITEM_CD.Enabled = isBool;
                this.form.bt_syuukeiitem_search.Enabled = isBool;
                this.form.GYOUSHA_CD.Enabled = isBool;
                this.form.bt_gyoushu_search.Enabled = isBool;
                this.form.BIKOU1.Enabled = isBool;
                this.form.BIKOU2.Enabled = isBool;
                this.form.BIKOU3.Enabled = isBool;
                this.form.BIKOU4.Enabled = isBool;

                // 請求情報
                this.form.SEIKYUU_SOUFU_NAME1.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_NAME2.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_POST.Enabled = isBool;
                this.form.bt_souhusaki_torihikisaki_copy.Enabled = isBool;
                this.form.bt_souhusaki_address.Enabled = isBool;
                this.form.bt_souhusaki_post.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_BUSHO.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_TANTOU.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_TEL.Enabled = isBool;
                this.form.SEIKYUU_SOUFU_FAX.Enabled = isBool;
                this.form.SEIKYUU_TANTOU.Enabled = isBool;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = isBool;
                this.form.rdb_seikyuu_suru.Enabled = isBool;
                this.form.rdb_seikyuu_shinai.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_CD.Enabled = isBool;
                this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = isBool;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.form.HAKKOUSAKI_CD.Enabled = isBool;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                // 支払情報
                this.form.SHIHARAI_SOUFU_NAME1.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_NAME2.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_POST.Enabled = isBool;
                this.form.bt_shiharai_souhusaki_torihikisaki_copy.Enabled = isBool;
                this.form.bt_shiharai_address.Enabled = isBool;
                this.form.bt_shiharai_post.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_BUSHO.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_TANTOU.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_TEL.Enabled = isBool;
                this.form.SHIHARAI_SOUFU_FAX.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_CD.Enabled = isBool;
                this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = isBool;

                // 業者分類
                this.form.GENBA_ICHIRAN.ReadOnly = !isBool;
                this.form.JISHA_KBN.Enabled = isBool;
                this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = isBool;
                this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = isBool;
                this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KBN.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_NAME1.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_NAME2.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_POST.Enabled = isBool;
                this.form.bt_hensousaki_torihikisaki_copy.Enabled = isBool;
                this.form.bt_hensousaki_address.Enabled = isBool;
                this.form.bt_hensousaki_post.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_BUSHO.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_TANTOU.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = isBool;
                // 20141209 katen #2927 実績報告書 フィードバック対応 start
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Enabled = isBool;
                this.form.bt_upn_houkokusho_teishutsu_chiiki_search.Enabled = isBool;
                // 20141209 katen #2927 実績報告書 フィードバック対応 end
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
            var hikiaiGenbas = daoGenba.GetAllValidData(new M_HIKIAI_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, HIKIAI_GYOUSHA_USE_FLG = true, ISNOT_NEED_DELETE_FLG = true });
            if (hikiaiGenbas != null)
            {
                foreach (M_HIKIAI_GENBA entity in hikiaiGenbas)
                {
                    if (entity.HIKIAI_GYOUSHA_USE_FLG.IsTrue)
                    {
                        this.hikiaiGenbaEntity = entity;
                        break;
                    }
                }
            }
            else
            {
                this.hikiaiGenbaEntity = null;
            }
            if (null != this.hikiaiGenbaEntity)
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

                if (!this.hikiaiGyoushaEntity.KYOTEN_CD.IsNull)
                {
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.hikiaiGyoushaEntity.KYOTEN_CD.ToString());
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

                this.bushoEntity = daoBusho.GetDataByCd(this.hikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD);

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

                this.shainEntity = daoShain.GetDataByCd(this.hikiaiGyoushaEntity.EIGYOU_TANTOU_CD);

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

                if (!this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.IsNull)
                {
                    this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD.Value.ToString());
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

                this.chiikiEntity = daoChiiki.GetDataByCd(this.hikiaiGyoushaEntity.CHIIKI_CD);

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

                this.shuukeiEntity = daoShuukei.GetDataByCd(this.hikiaiGyoushaEntity.SHUUKEI_ITEM_CD);

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

                this.gyoushuEntity = daoGyoushu.GetDataByCd(this.hikiaiGyoushaEntity.GYOUSHU_CD);

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
        /// 取消処理
        /// </summary>
        public bool Cancel(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

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
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 業者CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public ConstCls.GyoushaCdLeaveResult DupliCheckGyoushaCd(string zeroPadCd, bool isRegister)
        {
            ConstCls.GyoushaCdLeaveResult ViewUpdateWindow = 0;

            try
            {
                LogUtility.DebugMethodStart(zeroPadCd, isRegister);

                // 業者マスタ検索
                M_HIKIAI_GYOUSHA gyoushaEntity = this.daoHikiaiGyousha.GetDataByCd(zeroPadCd);

                // 重複チェック
                HikiaiGyoushaValidator vali = new HikiaiGyoushaValidator();
                DialogResult resultDialog = new DialogResult();
                bool resultDupli = vali.GyoushaCDValidator(gyoushaEntity, isRegister, out resultDialog);

                // 重複チェックの結果と、ポップアップの結果で動作を変える
                if (!resultDupli && resultDialog == DialogResult.OK)
                {
                    ViewUpdateWindow = ConstCls.GyoushaCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli && resultDialog == DialogResult.Yes)
                {
                    ViewUpdateWindow = ConstCls.GyoushaCdLeaveResult.FALSE_ON;
                }
                else if (!resultDupli && resultDialog == DialogResult.No)
                {
                    ViewUpdateWindow = ConstCls.GyoushaCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli)
                {
                    ViewUpdateWindow = ConstCls.GyoushaCdLeaveResult.FALSE_NONE;
                }
                else
                {
                    ViewUpdateWindow = ConstCls.GyoushaCdLeaveResult.TURE_NONE;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliCheckGyoushaCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ViewUpdateWindow);
            }

            return ViewUpdateWindow;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateHikiaiGyoushaEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                DateTime timeBegin = new DateTime();
                DateTime timeEnd = new DateTime();

                this.hikiaiGyoushaEntity = new M_HIKIAI_GYOUSHA();

                // 現在の入力内容でEntity作成
                this.hikiaiGyoushaEntity.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP.Text);
                this.hikiaiGyoushaEntity.SetValue(this.form.HIKIAI_TORIHIKISAKI_USE_FLG);
                this.hikiaiGyoushaEntity.BIKOU1 = this.form.BIKOU1.Text;
                this.hikiaiGyoushaEntity.BIKOU2 = this.form.BIKOU2.Text;
                this.hikiaiGyoushaEntity.BIKOU3 = this.form.BIKOU3.Text;
                this.hikiaiGyoushaEntity.BIKOU4 = this.form.BIKOU4.Text;
                this.hikiaiGyoushaEntity.BUSHO = this.form.BUSHO.Text;
                this.hikiaiGyoushaEntity.CHIIKI_CD = this.form.CHIIKI_CD.Text;
                this.hikiaiGyoushaEntity.CHUUSHI_RIYUU1 = this.form.CHUUSHI_RIYUU1.Text;
                this.hikiaiGyoushaEntity.CHUUSHI_RIYUU2 = this.form.CHUUSHI_RIYUU2.Text;
                this.hikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                this.hikiaiGyoushaEntity.EIGYOU_TANTOU_CD = this.form.EIGYOU_TANTOU_CD.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS1 = this.form.GYOUSHA_ADDRESS1.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS2 = this.form.GYOUSHA_ADDRESS2.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_FAX = this.form.GYOUSHA_FAX.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_FURIGANA = this.form.GYOUSHA_FURIGANA.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1 = this.form.GYOUSHA_KEISHOU1.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2 = this.form.GYOUSHA_KEISHOU2.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_KEITAI_TEL = this.form.GYOUSHA_KEITAI_TEL.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_NAME_RYAKU = this.form.GYOUSHA_NAME_RYAKU.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_NAME1 = this.form.GYOUSHA_NAME1.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_NAME2 = this.form.GYOUSHA_NAME2.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_POST = this.form.GYOUSHA_POST.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_TEL = this.form.GYOUSHA_TEL.Text;
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_TODOUFUKEN_CD.Text))
                {
                    this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD = Int16.Parse(this.form.GYOUSHA_TODOUFUKEN_CD.Text.ToString());
                }
                this.hikiaiGyoushaEntity.GYOUSHAKBN_MANI = this.form.Gyousha_KBN_3.Checked;
                this.hikiaiGyoushaEntity.GYOUSHAKBN_SHUKKA = this.form.Gyousha_KBN_2.Checked;
                this.hikiaiGyoushaEntity.GYOUSHAKBN_UKEIRE = this.form.Gyousha_KBN_1.Checked;
                this.hikiaiGyoushaEntity.GYOUSHU_CD = this.form.GYOUSHU_CD.Text;
                this.hikiaiGyoushaEntity.JISHA_KBN = this.form.JISHA_KBN.Checked;
                this.form.KYOTEN_CD.Text = "99";                            // 強制的に99:全社で登録
                if (!string.IsNullOrWhiteSpace(this.form.KYOTEN_CD.Text))
                {
                    this.hikiaiGyoushaEntity.KYOTEN_CD = Int16.Parse(this.form.KYOTEN_CD.Text);
                }

                if (!string.IsNullOrWhiteSpace(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text))
                {
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = Int16.Parse(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text);
                }
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked)
                {
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.GYOUSHA_ADDRESS2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.form.BUSHO.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.GYOUSHA_KEISHOU1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.GYOUSHA_KEISHOU2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.form.GYOUSHA_NAME1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.form.GYOUSHA_NAME2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_POST = this.form.GYOUSHA_POST.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.form.TANTOUSHA.Text;
                }
                else
                {
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.MANI_HENSOUSAKI_ADDRESS1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.form.MANI_HENSOUSAKI_ADDRESS2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_BUSHO = this.form.MANI_HENSOUSAKI_BUSHO.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN = this.form.MANI_HENSOUSAKI_KBN.Checked;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.form.MANI_HENSOUSAKI_KEISHOU1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.form.MANI_HENSOUSAKI_KEISHOU2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME1 = this.form.MANI_HENSOUSAKI_NAME1.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME2 = this.form.MANI_HENSOUSAKI_NAME2.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_POST = this.form.MANI_HENSOUSAKI_POST.Text;
                    this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_TANTOU = this.form.MANI_HENSOUSAKI_TANTOU.Text;
                }
                if (this._tabPageManager.IsVisible(1))
                {
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.form.SEIKYUU_SOUFU_ADDRESS1.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.form.SEIKYUU_SOUFU_ADDRESS2.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO = this.form.SEIKYUU_SOUFU_BUSHO.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX = this.form.SEIKYUU_SOUFU_FAX.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.form.SEIKYUU_SOUFU_KEISHOU1.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.form.SEIKYUU_SOUFU_KEISHOU2.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME1 = this.form.SEIKYUU_SOUFU_NAME1.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME2 = this.form.SEIKYUU_SOUFU_NAME2.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_POST = this.form.SEIKYUU_SOUFU_POST.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU = this.form.SEIKYUU_SOUFU_TANTOU.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL = this.form.SEIKYUU_SOUFU_TEL.Text;
                    this.hikiaiGyoushaEntity.SEIKYUU_TANTOU = this.form.SEIKYUU_TANTOU.Text;

                    if (this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text.Length > 0)
                    {
                        this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text);
                    }
                    if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                    {
                        this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                    }
                    if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                    {
                        this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD = Int16.Parse(this.form.SEIKYUU_KYOTEN_CD.Text);
                    }
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.hikiaiGyoushaEntity.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                }
                if (this._tabPageManager.IsVisible(2))
                {
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.form.SHIHARAI_SOUFU_ADDRESS1.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.form.SHIHARAI_SOUFU_ADDRESS2.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_BUSHO = this.form.SHIHARAI_SOUFU_BUSHO.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_FAX = this.form.SHIHARAI_SOUFU_FAX.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.form.SHIHARAI_SOUFU_KEISHOU1.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.form.SHIHARAI_SOUFU_KEISHOU2.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME1 = this.form.SHIHARAI_SOUFU_NAME1.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME2 = this.form.SHIHARAI_SOUFU_NAME2.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_POST = this.form.SHIHARAI_SOUFU_POST.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TANTOU = this.form.SHIHARAI_SOUFU_TANTOU.Text;
                    this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TEL = this.form.SHIHARAI_SOUFU_TEL.Text;
                    if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                    {
                        this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN = Int16.Parse(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                    }
                    if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                    {
                        this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD = Int16.Parse(this.form.SHIHARAI_KYOTEN_CD.Text);
                    }
                }
                this.hikiaiGyoushaEntity.SHOKUCHI_KBN = this.form.SHOKUCHI_KBN.Checked;
                this.hikiaiGyoushaEntity.SHUUKEI_ITEM_CD = this.form.SHUUKEI_ITEM_CD.Text;
                this.hikiaiGyoushaEntity.TANTOUSHA = this.form.TANTOUSHA.Text;
                this.hikiaiGyoushaEntity.GYOUSHA_DAIHYOU = this.form.GYOUSHA_DAIHYOU.Text;
                if (this.form.TEKIYOU_BEGIN.Value != null)
                {
                    DateTime.TryParse(this.form.TEKIYOU_BEGIN.Value.ToString(), out timeBegin);
                    this.hikiaiGyoushaEntity.TEKIYOU_BEGIN = timeBegin;
                }
                if (this.form.TEKIYOU_END.Value != null)
                {
                    DateTime.TryParse(this.form.TEKIYOU_END.Value.ToString(), out timeEnd);
                    this.hikiaiGyoushaEntity.TEKIYOU_END = timeEnd;
                }
                this.hikiaiGyoushaEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN = Int16.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text);

                // 20151102 BUNN #12040 STR
                // 排出事業者/荷積業者区分
                if (this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
                {
                    this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = true;
                }
                else
                {
                    this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN = false;
                }
                // 運搬受託者/運搬会社区分
                if (this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked)
                {
                    this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                }
                else
                {
                    this.hikiaiGyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN = false;
                }
                // 処分受託者/荷降業者区分
                if (this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
                {
                    this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                }
                else
                {
                    this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN = false;
                }
                // 20151102 BUNN #12040 END

                // 20141209 katen #2927 実績報告書 フィードバック対応 start
                this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text;
                // 20141209 katen #2927 実績報告書 フィードバック対応 end
                // 更新者情報設定
                var dataBinderLogicNyuukin = new DataBinderLogic<r_framework.Entity.M_HIKIAI_GYOUSHA>(this.hikiaiGyoushaEntity);
                dataBinderLogicNyuukin.SetSystemProperty(this.hikiaiGyoushaEntity, false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateHikiaiGyoushaEntity", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 現場エンティティを作成します
        /// </summary>
        internal void CreateHikiaiGenbaEntity()
        {
            LogUtility.DebugMethodStart();

            if (this.IsHikiaiGenbaAdd)
            {
                this.hikiaiGenbaEntity = new M_HIKIAI_GENBA();
            }

            this.hikiaiGenbaEntity.HIKIAI_GYOUSHA_USE_FLG = true;
            this.hikiaiGenbaEntity.GYOUSHA_CD = this.hikiaiGyoushaEntity.GYOUSHA_CD;
            this.hikiaiGenbaEntity.GENBA_CD = "000000";
            this.hikiaiGenbaEntity.HIKIAI_TORIHIKISAKI_USE_FLG = this.hikiaiGyoushaEntity.HIKIAI_TORIHIKISAKI_USE_FLG;
            this.hikiaiGenbaEntity.TORIHIKISAKI_CD = this.hikiaiGyoushaEntity.TORIHIKISAKI_CD;
            this.hikiaiGenbaEntity.KYOTEN_CD = this.hikiaiGyoushaEntity.KYOTEN_CD;
            this.hikiaiGenbaEntity.GENBA_NAME1 = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
            this.hikiaiGenbaEntity.GENBA_NAME2 = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;
            this.hikiaiGenbaEntity.GENBA_NAME_RYAKU = this.hikiaiGyoushaEntity.GYOUSHA_NAME_RYAKU;
            this.hikiaiGenbaEntity.GENBA_FURIGANA = this.hikiaiGyoushaEntity.GYOUSHA_FURIGANA;
            this.hikiaiGenbaEntity.GENBA_TEL = this.hikiaiGyoushaEntity.GYOUSHA_TEL;
            this.hikiaiGenbaEntity.GENBA_FAX = this.hikiaiGyoushaEntity.GYOUSHA_FAX;
            this.hikiaiGenbaEntity.GENBA_KEITAI_TEL = this.hikiaiGyoushaEntity.GYOUSHA_KEITAI_TEL;
            this.hikiaiGenbaEntity.GENBA_KEISHOU1 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
            this.hikiaiGenbaEntity.GENBA_KEISHOU2 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
            this.hikiaiGenbaEntity.EIGYOU_TANTOU_BUSHO_CD = this.hikiaiGyoushaEntity.EIGYOU_TANTOU_BUSHO_CD;
            this.hikiaiGenbaEntity.EIGYOU_TANTOU_CD = this.hikiaiGyoushaEntity.EIGYOU_TANTOU_CD;
            this.hikiaiGenbaEntity.GENBA_POST = this.hikiaiGyoushaEntity.GYOUSHA_POST;
            this.hikiaiGenbaEntity.GENBA_TODOUFUKEN_CD = this.hikiaiGyoushaEntity.GYOUSHA_TODOUFUKEN_CD;
            this.hikiaiGenbaEntity.GENBA_ADDRESS1 = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS1;
            this.hikiaiGenbaEntity.GENBA_ADDRESS2 = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS2;
            this.hikiaiGenbaEntity.TORIHIKI_JOUKYOU = this.hikiaiGyoushaEntity.TORIHIKI_JOUKYOU;
            this.hikiaiGenbaEntity.CHUUSHI_RIYUU1 = this.hikiaiGyoushaEntity.CHUUSHI_RIYUU1;
            this.hikiaiGenbaEntity.CHUUSHI_RIYUU2 = this.hikiaiGyoushaEntity.CHUUSHI_RIYUU2;
            this.hikiaiGenbaEntity.BUSHO = this.hikiaiGyoushaEntity.BUSHO;
            this.hikiaiGenbaEntity.TANTOUSHA = this.hikiaiGyoushaEntity.TANTOUSHA;
            this.hikiaiGenbaEntity.SHUUKEI_ITEM_CD = this.hikiaiGyoushaEntity.SHUUKEI_ITEM_CD;
            this.hikiaiGenbaEntity.GYOUSHU_CD = this.hikiaiGyoushaEntity.GYOUSHU_CD;
            this.hikiaiGenbaEntity.CHIIKI_CD = this.hikiaiGyoushaEntity.CHIIKI_CD;
            this.hikiaiGenbaEntity.BIKOU1 = this.hikiaiGyoushaEntity.BIKOU1;
            this.hikiaiGenbaEntity.BIKOU2 = this.hikiaiGyoushaEntity.BIKOU2;
            this.hikiaiGenbaEntity.BIKOU3 = this.hikiaiGyoushaEntity.BIKOU3;
            this.hikiaiGenbaEntity.BIKOU4 = this.hikiaiGyoushaEntity.BIKOU4;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_NAME1 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME1;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_NAME2 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_NAME2;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_KEISHOU1 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU1;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_KEISHOU2 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_KEISHOU2;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_POST = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_POST;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_ADDRESS1 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS1;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_ADDRESS2 = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_ADDRESS2;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_BUSHO = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_TANTOU = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_TEL = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL;
            this.hikiaiGenbaEntity.SEIKYUU_SOUFU_FAX = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX;
            this.hikiaiGenbaEntity.SEIKYUU_TANTOU = this.hikiaiGyoushaEntity.SEIKYUU_TANTOU;
            this.hikiaiGenbaEntity.SEIKYUU_DAIHYOU_PRINT_KBN = this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN;
            this.hikiaiGenbaEntity.SEIKYUU_KYOTEN_PRINT_KBN = this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN;
            this.hikiaiGenbaEntity.SEIKYUU_KYOTEN_CD = this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_CD;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_NAME1 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME1;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_NAME2 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_NAME2;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_KEISHOU1 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU1;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_KEISHOU2 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_KEISHOU2;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_POST = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_POST;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_ADDRESS1 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS1;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_ADDRESS2 = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_ADDRESS2;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_BUSHO = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_BUSHO;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_TANTOU = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TANTOU;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_TEL = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_TEL;
            this.hikiaiGenbaEntity.SHIHARAI_SOUFU_FAX = this.hikiaiGyoushaEntity.SHIHARAI_SOUFU_FAX;
            this.hikiaiGenbaEntity.SHIHARAI_KYOTEN_PRINT_KBN = this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_PRINT_KBN;
            this.hikiaiGenbaEntity.SHIHARAI_KYOTEN_CD = this.hikiaiGyoushaEntity.SHIHARAI_KYOTEN_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            //this.hikiaiGenbaEntity.HAKKOUSAKI_CD = this.hikiaiGyoushaEntity.HAKKOUSAKI_CD;
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.hikiaiGenbaEntity.JISHA_KBN = this.hikiaiGyoushaEntity.JISHA_KBN;
            this.hikiaiGenbaEntity.SHOKUCHI_KBN = this.hikiaiGyoushaEntity.SHOKUCHI_KBN;
            this.hikiaiGenbaEntity.ITAKU_KEIYAKU_USE_KBN = 1;
            if (this.IsHikiaiGenbaAdd)
            {
                this.hikiaiGenbaEntity.TSUMIKAEHOKAN_KBN = false;
                this.hikiaiGenbaEntity.SAISHUU_SHOBUNJOU_KBN = false;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KBN = true;
                this.hikiaiGenbaEntity.MANIFEST_SHURUI_CD = this.sysinfoEntity.GENBA_MANIFEST_SHURUI_CD;
                this.hikiaiGenbaEntity.MANIFEST_TEHAI_CD = this.sysinfoEntity.GENBA_MANIFEST_TEHAI_CD;
                this.hikiaiGenbaEntity.SHOBUNSAKI_NO = null;
                this.hikiaiGenbaEntity.DEN_MANI_SHOUKAI_KBN = false;
                this.hikiaiGenbaEntity.KENSHU_YOUHI = false;

                // 20151102 BUNN #12040 STR
                this.hikiaiGenbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN = this.hikiaiGyoushaEntity.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
                this.hikiaiGenbaEntity.SHOBUN_NIOROSHI_GENBA_KBN = this.hikiaiGyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN;
                // 20151102 BUNN #12040 END

                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1 = this.hikiaiGenbaEntity.GENBA_NAME1;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2 = this.hikiaiGenbaEntity.GENBA_NAME2;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.hikiaiGenbaEntity.GENBA_KEISHOU1;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.hikiaiGenbaEntity.GENBA_KEISHOU2;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_POST = this.hikiaiGenbaEntity.GENBA_POST;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.form.TODOUFUKEN_NAME.Text + this.hikiaiGenbaEntity.GENBA_ADDRESS1;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.hikiaiGenbaEntity.GENBA_ADDRESS2;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO = this.hikiaiGenbaEntity.BUSHO;
                this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU = this.hikiaiGenbaEntity.TANTOUSHA;

                if (this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KBN.IsTrue)
                {
                    if (this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1 = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2 = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_POST = this.hikiaiGyoushaEntity.GYOUSHA_POST;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO = this.hikiaiGyoushaEntity.BUSHO;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU = this.hikiaiGyoushaEntity.TANTOUSHA;
                    }
                    else if (this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_NAME2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_POST = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_POST;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_BUSHO;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU = this.hikiaiGyoushaEntity.MANI_HENSOUSAKI_TANTOU;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_POST = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO = string.Empty;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU = string.Empty;
                    }
                    if (1 == this.sysinfoEntity.MANIFEST_USE_A)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_A = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_B1)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_B2)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_B4)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B4 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_B6)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B6 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_C1)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_C2)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_D)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_D = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = null;
                    }

                    if (1 == this.sysinfoEntity.MANIFEST_USE_E)
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_E = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = this.hikiaiGenbaEntity.TORIHIKISAKI_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = this.hikiaiGenbaEntity.GYOUSHA_CD;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = this.hikiaiGenbaEntity.GENBA_CD;
                    }
                    else
                    {
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 1;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = null;
                        this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = null;
                    }
                }
                else
                {
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = 1;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME1 = this.hikiaiGyoushaEntity.GYOUSHA_NAME1;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_NAME2 = this.hikiaiGyoushaEntity.GYOUSHA_NAME2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU1 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU1;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_KEISHOU2 = this.hikiaiGyoushaEntity.GYOUSHA_KEISHOU2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_POST = this.hikiaiGyoushaEntity.GYOUSHA_POST;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS1 = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS1;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_ADDRESS2 = this.hikiaiGyoushaEntity.GYOUSHA_ADDRESS2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_BUSHO = this.hikiaiGyoushaEntity.BUSHO;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TANTOU = this.hikiaiGyoushaEntity.TANTOUSHA;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_A = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_A = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_A = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_A = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B1 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B1 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B1 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B2 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B2 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B2 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B4 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B4 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B4 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B4 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_B6 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_B6 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_B6 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_B6 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C1 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C1 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C1 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_C2 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_C2 = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_C2 = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_D = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_D = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_D = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_D = null;

                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_USE_E = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_PLACE_KBN_E = 2;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GYOUSHA_CD_E = null;
                    this.hikiaiGenbaEntity.MANI_HENSOUSAKI_GENBA_CD_E = null;
                }
            }

            this.hikiaiGenbaEntity.SHIKUCHOUSON_CD = null;
            this.hikiaiGenbaEntity.TEKIYOU_BEGIN = this.hikiaiGyoushaEntity.TEKIYOU_BEGIN;
            this.hikiaiGenbaEntity.TEKIYOU_END = this.hikiaiGyoushaEntity.TEKIYOU_END;
            this.hikiaiGenbaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;

            var dataBinderLogic = new DataBinderLogic<M_HIKIAI_GENBA>(this.hikiaiGenbaEntity);
            dataBinderLogic.SetSystemProperty(this.hikiaiGenbaEntity, false);

            LogUtility.DebugMethodEnd();
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

                PropertyInfo pi = this.form.GYOUSHA_CD.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GYOUSHA_CD, null);

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

                PropertyInfo pi = this.form.GYOUSHA_TODOUFUKEN_CD.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.GYOUSHA_TODOUFUKEN_CD, null);

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
        /// 登録時ユーザーコードチェック処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void FormUserRegistCheck(object source, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(source, e);

                MessageUtility msgUtil = new MessageUtility();
                if (e.multiRow == null)
                {
                    // 通常コントロールのチェック
                    Control ctrl = (Control)source;
                    ICustomControl customCtrl = source as ICustomControl;

                    // 業者区分のマニ記載者がチェック済みかつ
                    // 業者分類タブの排出事業者、運搬受託者、処分受託者全て未チェックの場合、エラー
                    if (ctrl != null
                        && customCtrl != null
                        && ctrl.Name.Equals("Gyousha_KBN_3")
                        && DB_FLAG.TRUE.ToString().Equals(customCtrl.GetResultText())
                        && !this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked
                        && !this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked
                        && !this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
                    {
                        string errorItem = string.Format("{0}、{1}、{2}のいづれか", this.form.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Text, this.form.UNPAN_JUTAKUSHA_KAISHA_KBN.Text, this.form.SHOBUN_NIOROSHI_GYOUSHA_KBN.Text);
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E027").MESSAGE, errorItem));
                    }
                }
                else
                {
                    // グリッドセル内のチェック
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormUserRegistCheck", ex);
                throw;
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
                LogUtility.DebugMethodStart(errorFlag);

                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 業者マスタ更新
                    this.hikiaiGyoushaEntity.DELETE_FLG = false;
                    this.daoHikiaiGyousha.Insert(this.hikiaiGyoushaEntity);
                    // 現場マスタ更新
                    if (null != this.hikiaiGenbaEntity)
                    {
                        this.daoGenba.Insert(this.hikiaiGenbaEntity);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "登録");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);

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
                    // 業者マスタ更新
                    this.hikiaiGyoushaEntity.DELETE_FLG = false;
                    this.daoHikiaiGyousha.Update(this.hikiaiGyoushaEntity);
                    // 現場マスタ更新
                    if (this.IsHikiaiGenbaAdd)
                    {
                        if (null != this.hikiaiGenbaEntity)
                        {
                            this.daoGenba.Insert(this.hikiaiGenbaEntity);
                        }
                    }
                    else
                    {
                        if (null != this.hikiaiGenbaEntity)
                        {
                            this.daoGenba.Update(this.hikiaiGenbaEntity);
                        }
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
                LogUtility.Error("Update", ex);

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
                        // 業者マスタ更新
                        this.hikiaiGyoushaEntity.DELETE_FLG = true;
                        this.daoHikiaiGyousha.Update(this.hikiaiGyoushaEntity);
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
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

        #region 電子申請

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <param name="dsEntry"></param>
        /// <param name="dsDetailList"></param>
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
                    // 業者マスタ更新
                    this.hikiaiGyoushaEntity.DELETE_FLG = false;
                    this.daoHikiaiGyousha.Update(this.hikiaiGyoushaEntity);

                    // 申請入力登録
                    DenshiShinseiDBAccessor dsDBAccessor = new DenshiShinseiDBAccessor();

                    // 電子申請入力の登録
                    dsDBAccessor.InsertDSEntry(dsEntry, DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GYOUSHA);

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
                this.isRegist = false;
                LogUtility.Error("Shinsei", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    throw ex;
                }
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
            var dsUtility = new DenshiShinseiUtility();
            catchErr = true;
            bool isOk = true;
            try
            {
                isOk = dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_GYOUSHA, null, this.form.GYOUSHA_CD.Text, null);
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckDenshiShinseiData", ex2);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDenshiShinseiData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return isOk;
        }

        #endregion

        #region 本登録済みか判定

        /// <summary>
        /// 指定された業者CDが本登録済みデータか判定
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedGyousha(string gyoushaCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(gyoushaCD))
                {
                    return false;
                }

                var entity = daoHikiaiGyousha.GetDataByCd(gyoushaCD);
                if (entity != null && !string.IsNullOrEmpty(entity.GYOUSHA_CD_AFTER))
                {
                    // 移行後業者CDに値がある場合は、本登録済みデータとみなす
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ExistedGyousha", ex1);
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

        #endregion

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
                returnVal.HikiaiTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                returnVal.HikiaiTorihikisakiNameRyaku = this.hikiaiTorihikisakiEntity == null ? string.Empty : this.hikiaiTorihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }
            else
            {
                returnVal.TorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                returnVal.TorihikisakiNameRyaku = this.torihikisakiEntity == null ? string.Empty : this.torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }

            returnVal.HikiaiGyoushaCd = this.form.GYOUSHA_CD.Text;
            returnVal.HikiaiGyoushaNameRyaku = this.form.GYOUSHA_NAME_RYAKU.Text;
            returnVal.NaiyouCd = DenshiShinseiUtility.NAIYOU_CD_GYOUSHA;

            return returnVal;
        }

        #endregion

        #region 削除できるかどうかチェックする

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        /// <returns></returns>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var cd = this.form.GYOUSHA_CD.Text;

                if (!string.IsNullOrEmpty(cd))
                {
                    DataTable dtTable = daoHikiaiGyousha.GetDataBySqlFileCheck(cd);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        this.form.messBSL.MessageBoxShow("E258", "引合業者", "引合業者CD", strName);

                        ret = false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
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

                this.form.GENBA_ICHIRAN.IsBrowsePurpose = false;
                var table = this.SearchResultGenba;
                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                this.form.GENBA_ICHIRAN.DataSource = table;
                this.form.GENBA_ICHIRAN.IsBrowsePurpose = true;
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
                    // 電子申請オプションが無効の時のみ表示
                    if (!DensisinseiOptionEnabledFlg)
                    {
                        //移行ボタン(F1)イベント生成
                        parentForm.bt_func1.Click += new EventHandler(this.form.Ikou);
                    }

                    //新規ボタン(F2)イベント生成
                    parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                    //修正ボタン(F3)イベント生成
                    parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                    //一覧ボタン(F7)イベント生成
                    parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                    //登録ボタン(F9)イベント生成
                    this.form.C_Regist(parentForm.bt_func9);
                    parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                    parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                    //取消ボタン(F11)イベント生成
                    parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                    //閉じるボタン(F12)イベント生成
                    parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                }

                // SuperFormイベント処理
                this.form.UserRegistCheck += new SuperForm.UserRegistCheckHandler(this.form.FormUserRegistCheck);

                // 20141203 Houkakou 「引合業者入力」の日付チェックを追加する start
                this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(TEKIYOU_BEGIN_Leave);
                this.form.TEKIYOU_END.Leave += new System.EventHandler(TEKIYOU_END_Leave);
                // 20141203 Houkakou 「引合業者入力」の日付チェックを追加する start

                // 20141209 katen #2927 実績報告書 フィードバック対応 start
                this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validating);
                // 20141209 katen #2927 実績報告書 フィードバック対応 end
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
        /// 現場一覧検索
        /// </summary>
        public bool TorihikiStopIchiran()
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TorihikiStopIchiran", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikiStopIchiran", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.bt_torihikisaki_copy.Enabled = false;
                    this.form.bt_torihikisaki_search.Enabled = false;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);

                    return;
                }

                if (int.Parse(this.form.TORIHIKISAKI_UMU_KBN.Text) == 1)
                {
                    // 使用可能にする
                    this.form.TORIHIKISAKI_CD.ReadOnly = false;
                    this.form.TORIHIKISAKI_CD.Enabled = true;
                    this.form.bt_torihikisaki_copy.Enabled = true;
                    this.form.bt_torihikisaki_search.Enabled = true;
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
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.hikiaiGyoushaEntity.SEIKYUU_SOUFU_FAX;
                    this.form.SEIKYUU_TANTOU.Text = this.hikiaiGyoushaEntity.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    this.form.HAKKOUSAKI_CD.Text = this.hikiaiGyoushaEntity.HAKKOUSAKI_CD;
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    else if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Value.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.hikiaiGyoushaEntity.SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
                    }
                    else if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Value.ToString();
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
                    else if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.Value.ToString()));
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
                    else if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Value.ToString();
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
                    else if (this.hikiaiGyoushaEntity.TORIHIKISAKI_UMU_KBN == 2 && !this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.Value.ToString()));
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
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    // 発行先チェック処理
                    this.HakkousakuCheck();
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                    this.form.KYOTEN_CD.Text = string.Empty;
                    this.form.KYOTEN_NAME.Text = string.Empty;
                    this.form.bt_torihikisaki_copy.Enabled = false;
                    this.form.bt_torihikisaki_search.Enabled = false;
                    _tabPageManager.ChangeTabPageVisible(1, false);
                    _tabPageManager.ChangeTabPageVisible(2, false);
                    this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
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
        /// マニありチェックボックスのON/OFFチェック
        /// </summary>
        /// <returns></returns>
        public void ManiCheckOffCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (FlgManiHensousakiKbn)
                {
                    // 使用可能
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = true;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = true;

                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                        this.form.bt_hensousaki_torihikisaki_copy.Enabled = true;
                        this.form.bt_hensousaki_address.Enabled = true;
                        this.form.bt_hensousaki_post.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                    }
                }
                else
                {
                    // 使用不可
                    if (string.IsNullOrEmpty(this.GyoushaCd))
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                    }
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                    this.form.bt_hensousaki_torihikisaki_copy.Enabled = false;
                    this.form.bt_hensousaki_address.Enabled = false;
                    this.form.bt_hensousaki_post.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;

                    // テキストクリア
                    this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                }
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
        /// 業者と現場のマニ不整合チェック
        /// </summary>
        /// <returns></returns>
        public bool ManiCheckMsg(M_HIKIAI_GENBA queryParam, out bool catchErr)
        {
            bool result = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(queryParam);

                M_HIKIAI_GENBA[] test = this.daoGenba.GetAllValidExistCheckData(queryParam);
                if (test == null || test.Length == 0)
                {
                    result = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ManiCheckMsg", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiCheckMsg", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }

            return result;
        }

        /// <summary>
        /// 取引先情報コピー処理
        /// </summary>
        public bool TorihikisakiCopy()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string inputCd = this.form.TORIHIKISAKI_CD.Text;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    var result = msgLogic.MessageBoxShow("E012", "取引先CD");
                    LogUtility.DebugMethodEnd(false);
                    return false;
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
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCopy", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 取引先コピー＆セッティング
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        private void TorihikisakiSetting(string inputCd)
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_TORIHIKISAKI torisakiEntity = this.daoTorihikisaki.GetDataByCd(inputCd);
                M_TORIHIKISAKI_SHIHARAI shiharaiEntity = this.daoShiharai.GetDataByCd(inputCd);
                M_TORIHIKISAKI_SEIKYUU seikyuuEntity = this.daoSeikyuu.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI hikiToriEntity = this.daoHikiaiTorihikisaki.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI_SHIHARAI hikiShihaEntity = this.daoHikiaiShiharai.GetDataByCd(inputCd);
                M_HIKIAI_TORIHIKISAKI_SEIKYUU hikiSeiEntity = this.daoHikiaiSeikyuu.GetDataByCd(inputCd);

                #region 取引先テーブルから取得する場合

                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0") && torisakiEntity != null)
                {
                    // 共通部分
                    if (!torisakiEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KYOTEN_CD.Text = torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString();                // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(torisakiEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KYOTEN_CD.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                                  // 拠点名
                        }
                    }

                    this.form.GYOUSHA_NAME1.Text = torisakiEntity.TORIHIKISAKI_NAME1;                               // 取引先名１
                    this.form.GYOUSHA_NAME2.Text = torisakiEntity.TORIHIKISAKI_NAME2;                               // 取引先名２
                    if (torisakiEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TEKIYOU_BEGIN.Value = torisakiEntity.TEKIYOU_BEGIN;
                    }
                    if (torisakiEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TEKIYOU_END.Value = torisakiEntity.TEKIYOU_END;
                    }

                    this.form.CHUUSHI_RIYUU1.Text = torisakiEntity.CHUUSHI_RIYUU1;
                    this.form.CHUUSHI_RIYUU2.Text = torisakiEntity.CHUUSHI_RIYUU2;
                    this.form.SHOKUCHI_KBN.Checked = torisakiEntity.SHOKUCHI_KBN.IsTrue;

                    this.form.GYOUSHA_KEISHOU1.Text = torisakiEntity.TORIHIKISAKI_KEISHOU1;                         // 敬称１
                    this.form.GYOUSHA_KEISHOU2.Text = torisakiEntity.TORIHIKISAKI_KEISHOU2;                         // 敬称２

                    this.form.GYOUSHA_FURIGANA.Text = torisakiEntity.TORIHIKISAKI_FURIGANA;                         // フリガナ(不要な可能性有)
                    this.form.GYOUSHA_NAME_RYAKU.Text = torisakiEntity.TORIHIKISAKI_NAME_RYAKU;                     // 略称(不要な可能性有)

                    this.form.GYOUSHA_TEL.Text = torisakiEntity.TORIHIKISAKI_TEL;                                   // 電話
                    this.form.GYOUSHA_FAX.Text = torisakiEntity.TORIHIKISAKI_FAX;                                   // FAX
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = torisakiEntity.EIGYOU_TANTOU_BUSHO_CD;                  // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.BUSHO_NAME.Text = busho.BUSHO_NAME_RYAKU;                                         // 営業担当部署名
                    }
                    this.form.EIGYOU_TANTOU_CD.Text = torisakiEntity.EIGYOU_TANTOU_CD;                              // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(torisakiEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;                                         // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GYOUSHA_POST.Text = torisakiEntity.TORIHIKISAKI_POST;                                 // 郵便番号

                    if (!torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.ZeroPaddingKen(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());    // 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(torisakiEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GYOUSHA_TODOUFUKEN_CD.Text = string.Empty;
                        }
                        else
                        {
                            this.form.TODOUFUKEN_NAME.Text = temp.TODOUFUKEN_NAME;                                  // 都道府県名
                        }
                    }

                    this.form.GYOUSHA_ADDRESS1.Text = torisakiEntity.TORIHIKISAKI_ADDRESS1;                         // 住所１
                    this.form.GYOUSHA_ADDRESS2.Text = torisakiEntity.TORIHIKISAKI_ADDRESS2;                         // 住所２

                    // 地域の判定は関数に任せる
                    if (!this.ChechChiiki(true))
                    {
                        this.form.CHIIKI_CD.Text = string.Empty;                                                    // 地域CD
                        this.form.CHIIKI_NAME.Text = string.Empty;                                                  // 地域名
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;                           // 運搬報告書提出先地域CD
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;                         // 運搬報告書提出先地域名
                    }

                    this.form.BUSHO.Text = torisakiEntity.BUSHO;                                                    // 部署
                    this.form.TANTOUSHA.Text = torisakiEntity.TANTOUSHA;                                        // 担当者
                    this.form.SHUUKEI_ITEM_CD.Text = torisakiEntity.SHUUKEI_ITEM_CD;                                // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(torisakiEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHUUKEI_ITEM_NAME.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU;                      // 集計項目名
                    }
                    this.form.GYOUSHU_CD.Text = torisakiEntity.GYOUSHU_CD;                                          // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(torisakiEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GYOUSHU_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GYOUSHU_NAME.Text = gyoushu.GYOUSHU_NAME_RYAKU;                                   // 業種名
                    }
                    this.form.BIKOU1.Text = torisakiEntity.BIKOU1;                                                  // 備考１
                    this.form.BIKOU2.Text = torisakiEntity.BIKOU2;                                                  // 備考２
                    this.form.BIKOU3.Text = torisakiEntity.BIKOU3;                                                  // 備考３
                    this.form.BIKOU4.Text = torisakiEntity.BIKOU4;                                                  // 備考４

                    // 業者分類タブ
                    this.form.MANI_HENSOUSAKI_KBN.Checked = torisakiEntity.MANI_HENSOUSAKI_KBN.IsTrue;           // マニフェスト返送先
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
                        this.form.MANI_HENSOUSAKI_NAME1.Text = torisakiEntity.MANI_HENSOUSAKI_NAME1;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = torisakiEntity.MANI_HENSOUSAKI_NAME2;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = torisakiEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.MANI_HENSOUSAKI_POST.Text = torisakiEntity.MANI_HENSOUSAKI_POST;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = torisakiEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = torisakiEntity.MANI_HENSOUSAKI_BUSHO;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = torisakiEntity.MANI_HENSOUSAKI_TANTOU;
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                    }

                    if (seikyuuEntity != null)
                    {
                        // 請求情報タブ
                        this.form.SEIKYUU_SOUFU_NAME1.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME1;                         // 請求書送付先1
                        this.form.SEIKYUU_SOUFU_KEISHOU1.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU1;                   // 請求書送付先敬称1
                        this.form.SEIKYUU_SOUFU_NAME2.Text = seikyuuEntity.SEIKYUU_SOUFU_NAME2;                         // 請求書送付先2
                        this.form.SEIKYUU_SOUFU_KEISHOU2.Text = seikyuuEntity.SEIKYUU_SOUFU_KEISHOU2;                   // 請求書送付先敬称2
                        this.form.SEIKYUU_SOUFU_POST.Text = seikyuuEntity.SEIKYUU_SOUFU_POST;                           // 送付先郵便番号
                        this.form.SEIKYUU_SOUFU_ADDRESS1.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS1;                   // 送付先住所１
                        this.form.SEIKYUU_SOUFU_ADDRESS2.Text = seikyuuEntity.SEIKYUU_SOUFU_ADDRESS2;                   // 送付先住所２
                        this.form.SEIKYUU_SOUFU_BUSHO.Text = seikyuuEntity.SEIKYUU_SOUFU_BUSHO;                         // 送付先部署
                        this.form.SEIKYUU_SOUFU_TANTOU.Text = seikyuuEntity.SEIKYUU_SOUFU_TANTOU;                       // 送付先担当者
                        this.form.SEIKYUU_SOUFU_TEL.Text = seikyuuEntity.SEIKYUU_SOUFU_TEL;                             // 送付先電話番号
                        this.form.SEIKYUU_SOUFU_FAX.Text = seikyuuEntity.SEIKYUU_SOUFU_FAX;                             // 送付先FAX番号
                        this.form.SEIKYUU_TANTOU.Text = seikyuuEntity.SEIKYUU_TANTOU;                                   // 請求担当者
                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        //this.form.HAKKOUSAKI_CD.Text = seikyuuEntity.HAKKOUSAKI_CD;                                     // 発行先コード
                        // 発行先チェック処理
                        this.HakkousakuCheck();
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end

                        if (!seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();  // 代表取締役
                        }

                        if (!seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = seikyuuEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();    // 請求拠点印字区分
                        }

                        if (!seikyuuEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = seikyuuEntity.SEIKYUU_KYOTEN_CD.ToString();              // 請求拠点CD
                            M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(seikyuuEntity.SEIKYUU_KYOTEN_CD.ToString());
                            if (kyoten == null)
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                            }
                            else
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                          // 請求拠点名
                            }
                        }
                    }

                    if (shiharaiEntity != null)
                    {
                        // 支払情報タブ
                        this.form.SHIHARAI_SOUFU_NAME1.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME1;		                // 支払明細書送付先1
                        this.form.SHIHARAI_SOUFU_KEISHOU1.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU1;	            // 支払明細書送付先敬称1
                        this.form.SHIHARAI_SOUFU_NAME2.Text = shiharaiEntity.SHIHARAI_SOUFU_NAME2;		                // 支払明細書送付先2
                        this.form.SHIHARAI_SOUFU_KEISHOU2.Text = shiharaiEntity.SHIHARAI_SOUFU_KEISHOU2;	            // 支払明細書送付先敬称2
                        this.form.SHIHARAI_SOUFU_POST.Text = shiharaiEntity.SHIHARAI_SOUFU_POST;		                // 送付先郵便番号
                        this.form.SHIHARAI_SOUFU_ADDRESS1.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS1;	            // 送付先住所１
                        this.form.SHIHARAI_SOUFU_ADDRESS2.Text = shiharaiEntity.SHIHARAI_SOUFU_ADDRESS2;	            // 送付先住所２
                        this.form.SHIHARAI_SOUFU_BUSHO.Text = shiharaiEntity.SHIHARAI_SOUFU_BUSHO;		                // 送付先部署
                        this.form.SHIHARAI_SOUFU_TANTOU.Text = shiharaiEntity.SHIHARAI_SOUFU_TANTOU;		            // 送付先担当者
                        this.form.SHIHARAI_SOUFU_TEL.Text = shiharaiEntity.SHIHARAI_SOUFU_TEL;			                // 送付先電話番号
                        this.form.SHIHARAI_SOUFU_FAX.Text = shiharaiEntity.SHIHARAI_SOUFU_FAX;			                // 送付先FAX番号

                        if (!shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = shiharaiEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString(); // 支払拠点印字区分
                        }

                        if (!shiharaiEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = shiharaiEntity.SHIHARAI_KYOTEN_CD.ToString();           // 支払拠点CD
                            M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(shiharaiEntity.SHIHARAI_KYOTEN_CD.ToString());
                            if (kyoten == null)
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                            }
                            else
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                         // 支払請求拠点名
                            }
                        }
                    }
                }

                #endregion

                #region 引合取引先テーブルから取得する場合

                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1") && hikiToriEntity != null)
                {
                    // 共通部分
                    if (!hikiToriEntity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                    {
                        this.form.KYOTEN_CD.Text = hikiToriEntity.TORIHIKISAKI_KYOTEN_CD.ToString();                // 拠点CD
                        M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiToriEntity.TORIHIKISAKI_KYOTEN_CD.ToString());
                        if (kyoten == null)
                        {
                            this.form.KYOTEN_CD.Text = string.Empty;
                        }
                        else
                        {
                            this.form.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                                  // 拠点名
                        }
                    }

                    this.form.GYOUSHA_NAME1.Text = hikiToriEntity.TORIHIKISAKI_NAME1;                               // 取引先名１
                    this.form.GYOUSHA_NAME2.Text = hikiToriEntity.TORIHIKISAKI_NAME2;                               // 取引先名２
                    if (hikiToriEntity.TEKIYOU_BEGIN.IsNull)
                    {
                        this.form.TEKIYOU_BEGIN.Value = null;
                    }
                    else
                    {
                        this.form.TEKIYOU_BEGIN.Value = hikiToriEntity.TEKIYOU_BEGIN;
                    }
                    if (hikiToriEntity.TEKIYOU_END.IsNull)
                    {
                        this.form.TEKIYOU_END.Value = null;
                    }
                    else
                    {
                        this.form.TEKIYOU_END.Value = hikiToriEntity.TEKIYOU_END;
                    }
                    this.form.CHUUSHI_RIYUU1.Text = hikiToriEntity.CHUUSHI_RIYUU1;
                    this.form.CHUUSHI_RIYUU2.Text = hikiToriEntity.CHUUSHI_RIYUU2;
                    this.form.SHOKUCHI_KBN.Checked = hikiToriEntity.SHOKUCHI_KBN.IsTrue;

                    this.form.GYOUSHA_KEISHOU1.Text = hikiToriEntity.TORIHIKISAKI_KEISHOU1;                         // 敬称１
                    this.form.GYOUSHA_KEISHOU2.Text = hikiToriEntity.TORIHIKISAKI_KEISHOU2;                         // 敬称２

                    this.form.GYOUSHA_FURIGANA.Text = hikiToriEntity.TORIHIKISAKI_FURIGANA;                         // フリガナ(不要な可能性有)
                    this.form.GYOUSHA_NAME_RYAKU.Text = hikiToriEntity.TORIHIKISAKI_NAME_RYAKU;                     // 略称(不要な可能性有)

                    this.form.GYOUSHA_TEL.Text = hikiToriEntity.TORIHIKISAKI_TEL;                                   // 電話
                    this.form.GYOUSHA_FAX.Text = hikiToriEntity.TORIHIKISAKI_FAX;                                   // FAX
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = hikiToriEntity.EIGYOU_TANTOU_BUSHO_CD;                  // 営業担当部署CD
                    M_BUSHO busho = this.daoBusho.GetDataByCd(hikiToriEntity.EIGYOU_TANTOU_BUSHO_CD);
                    if (busho == null)
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.BUSHO_NAME.Text = busho.BUSHO_NAME_RYAKU;                                         // 営業担当部署名
                    }
                    this.form.EIGYOU_TANTOU_CD.Text = hikiToriEntity.EIGYOU_TANTOU_CD;                              // 営業担当者CD
                    M_SHAIN shain = this.daoShain.GetDataByCd(hikiToriEntity.EIGYOU_TANTOU_CD);
                    if (shain == null)
                    {
                        this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;                                         // 営業担当者名
                    }

                    // 基本情報タブ
                    this.form.GYOUSHA_POST.Text = hikiToriEntity.TORIHIKISAKI_POST;                                 // 郵便番号

                    if (!hikiToriEntity.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                    {
                        this.form.GYOUSHA_TODOUFUKEN_CD.Text = this.ZeroPaddingKen(hikiToriEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());    // 都道府県CD
                        M_TODOUFUKEN temp = this.daoTodoufuken.GetDataByCd(hikiToriEntity.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                        if (temp == null)
                        {
                            this.form.GYOUSHA_TODOUFUKEN_CD.Text = string.Empty;
                        }
                        else
                        {
                            this.form.TODOUFUKEN_NAME.Text = temp.TODOUFUKEN_NAME;                                  // 都道府県名
                        }
                    }

                    this.form.GYOUSHA_ADDRESS1.Text = hikiToriEntity.TORIHIKISAKI_ADDRESS1;                         // 住所１
                    this.form.GYOUSHA_ADDRESS2.Text = hikiToriEntity.TORIHIKISAKI_ADDRESS2;                         // 住所２

                    // 地域の判定は関数に任せる
                    if (!this.ChechChiiki(true))
                    {
                        this.form.CHIIKI_CD.Text = string.Empty;                                                    // 地域CD
                        this.form.CHIIKI_NAME.Text = string.Empty;                                                  // 地域名
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD.Text = string.Empty;                           // 運搬報告書提出先地域CD
                        this.form.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_NAME.Text = string.Empty;                         // 運搬報告書提出先地域名
                    }

                    this.form.BUSHO.Text = hikiToriEntity.BUSHO;                                                    // 部署
                    this.form.TANTOUSHA.Text = hikiToriEntity.TANTOUSHA;                                            // 担当者
                    this.form.SHUUKEI_ITEM_CD.Text = hikiToriEntity.SHUUKEI_ITEM_CD;                                // 集計項目CD
                    M_SHUUKEI_KOUMOKU shuukei = this.daoShuukei.GetDataByCd(hikiToriEntity.SHUUKEI_ITEM_CD);
                    if (shuukei == null)
                    {
                        this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.SHUUKEI_ITEM_NAME.Text = shuukei.SHUUKEI_KOUMOKU_NAME_RYAKU;                      // 集計項目名
                    }
                    this.form.GYOUSHU_CD.Text = hikiToriEntity.GYOUSHU_CD;                                          // 業種CD
                    M_GYOUSHU gyoushu = this.daoGyoushu.GetDataByCd(hikiToriEntity.GYOUSHU_CD);
                    if (gyoushu == null)
                    {
                        this.form.GYOUSHU_CD.Text = string.Empty;
                    }
                    else
                    {
                        this.form.GYOUSHU_NAME.Text = gyoushu.GYOUSHU_NAME_RYAKU;                                   // 業種名
                    }
                    this.form.BIKOU1.Text = hikiToriEntity.BIKOU1;                                                  // 備考１
                    this.form.BIKOU2.Text = hikiToriEntity.BIKOU2;                                                  // 備考２
                    this.form.BIKOU3.Text = hikiToriEntity.BIKOU3;                                                  // 備考３
                    this.form.BIKOU4.Text = hikiToriEntity.BIKOU4;                                                  // 備考４

                    // 業者分類タブ
                    this.form.MANI_HENSOUSAKI_KBN.Checked = hikiToriEntity.MANI_HENSOUSAKI_KBN.IsTrue;           // マニフェスト返送先
                    if (hikiToriEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 2)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = false;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    }
                    else if (hikiToriEntity.MANI_HENSOUSAKI_THIS_ADDRESS_KBN == 1)
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
                        this.form.MANI_HENSOUSAKI_NAME1.Text = hikiToriEntity.MANI_HENSOUSAKI_NAME1;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = hikiToriEntity.MANI_HENSOUSAKI_NAME2;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = hikiToriEntity.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = hikiToriEntity.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.MANI_HENSOUSAKI_POST.Text = hikiToriEntity.MANI_HENSOUSAKI_POST;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = hikiToriEntity.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = hikiToriEntity.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = hikiToriEntity.MANI_HENSOUSAKI_BUSHO;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = hikiToriEntity.MANI_HENSOUSAKI_TANTOU;
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                        this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                    }

                    if (hikiSeiEntity != null)
                    {
                        // 請求情報タブ
                        this.form.SEIKYUU_SOUFU_NAME1.Text = hikiSeiEntity.SEIKYUU_SOUFU_NAME1;                         // 請求書送付先1
                        this.form.SEIKYUU_SOUFU_KEISHOU1.Text = hikiSeiEntity.SEIKYUU_SOUFU_KEISHOU1;                   // 請求書送付先敬称1
                        this.form.SEIKYUU_SOUFU_NAME2.Text = hikiSeiEntity.SEIKYUU_SOUFU_NAME2;                         // 請求書送付先2
                        this.form.SEIKYUU_SOUFU_KEISHOU2.Text = hikiSeiEntity.SEIKYUU_SOUFU_KEISHOU2;                   // 請求書送付先敬称2
                        this.form.SEIKYUU_SOUFU_POST.Text = hikiSeiEntity.SEIKYUU_SOUFU_POST;                           // 送付先郵便番号
                        this.form.SEIKYUU_SOUFU_ADDRESS1.Text = hikiSeiEntity.SEIKYUU_SOUFU_ADDRESS1;                   // 送付先住所１
                        this.form.SEIKYUU_SOUFU_ADDRESS2.Text = hikiSeiEntity.SEIKYUU_SOUFU_ADDRESS2;                   // 送付先住所２
                        this.form.SEIKYUU_SOUFU_BUSHO.Text = hikiSeiEntity.SEIKYUU_SOUFU_BUSHO;                         // 送付先部署
                        this.form.SEIKYUU_SOUFU_TANTOU.Text = hikiSeiEntity.SEIKYUU_SOUFU_TANTOU;                       // 送付先担当者
                        this.form.SEIKYUU_SOUFU_TEL.Text = hikiSeiEntity.SEIKYUU_SOUFU_TEL;                             // 送付先電話番号
                        this.form.SEIKYUU_SOUFU_FAX.Text = hikiSeiEntity.SEIKYUU_SOUFU_FAX;                             // 送付先FAX番号
                        this.form.SEIKYUU_TANTOU.Text = hikiSeiEntity.SEIKYUU_TANTOU;                                   // 請求担当者
                        // 20160429 koukoukon v2.1_電子請求書 #16612 start
                        //this.form.HAKKOUSAKI_CD.Text = hikiSeiEntity.HAKKOUSAKI_CD;                                     // 発行先コード
                        // 発行先チェック処理
                        this.HakkousakuCheck();
                        // 20160429 koukoukon v2.1_電子請求書 #16612 end

                        if (!hikiSeiEntity.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = hikiSeiEntity.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();  // 代表取締役
                        }

                        if (!hikiSeiEntity.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = hikiSeiEntity.SEIKYUU_KYOTEN_PRINT_KBN.ToString();    // 請求拠点印字区分
                        }

                        if (!hikiSeiEntity.SEIKYUU_KYOTEN_CD.IsNull)
                        {
                            this.form.SEIKYUU_KYOTEN_CD.Text = hikiSeiEntity.SEIKYUU_KYOTEN_CD.ToString();              // 請求拠点CD
                            M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiSeiEntity.SEIKYUU_KYOTEN_CD.ToString());
                            if (kyoten == null)
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                            }
                            else
                            {
                                this.form.SEIKYUU_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                          // 請求拠点名
                            }
                        }
                    }

                    if (hikiShihaEntity != null)
                    {
                        // 支払情報タブ
                        this.form.SHIHARAI_SOUFU_NAME1.Text = hikiShihaEntity.SHIHARAI_SOUFU_NAME1;		                // 支払明細書送付先1
                        this.form.SHIHARAI_SOUFU_KEISHOU1.Text = hikiShihaEntity.SHIHARAI_SOUFU_KEISHOU1;	            // 支払明細書送付先敬称1
                        this.form.SHIHARAI_SOUFU_NAME2.Text = hikiShihaEntity.SHIHARAI_SOUFU_NAME2;		                // 支払明細書送付先2
                        this.form.SHIHARAI_SOUFU_KEISHOU2.Text = hikiShihaEntity.SHIHARAI_SOUFU_KEISHOU2;	            // 支払明細書送付先敬称2
                        this.form.SHIHARAI_SOUFU_POST.Text = hikiShihaEntity.SHIHARAI_SOUFU_POST;		                // 送付先郵便番号
                        this.form.SHIHARAI_SOUFU_ADDRESS1.Text = hikiShihaEntity.SHIHARAI_SOUFU_ADDRESS1;	            // 送付先住所１
                        this.form.SHIHARAI_SOUFU_ADDRESS2.Text = hikiShihaEntity.SHIHARAI_SOUFU_ADDRESS2;	            // 送付先住所２
                        this.form.SHIHARAI_SOUFU_BUSHO.Text = hikiShihaEntity.SHIHARAI_SOUFU_BUSHO;		                // 送付先部署
                        this.form.SHIHARAI_SOUFU_TANTOU.Text = hikiShihaEntity.SHIHARAI_SOUFU_TANTOU;		            // 送付先担当者
                        this.form.SHIHARAI_SOUFU_TEL.Text = hikiShihaEntity.SHIHARAI_SOUFU_TEL;			                // 送付先電話番号
                        this.form.SHIHARAI_SOUFU_FAX.Text = hikiShihaEntity.SHIHARAI_SOUFU_FAX;			                // 送付先FAX番号

                        if (!hikiShihaEntity.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = hikiShihaEntity.SHIHARAI_KYOTEN_PRINT_KBN.ToString();    // 支払拠点印字区分
                        }

                        if (!hikiShihaEntity.SHIHARAI_KYOTEN_CD.IsNull)
                        {
                            this.form.SHIHARAI_KYOTEN_CD.Text = hikiShihaEntity.SHIHARAI_KYOTEN_CD.ToString();          // 支払拠点CD
                            M_KYOTEN kyoten = this.daoKyoten.GetDataByCd(hikiShihaEntity.SHIHARAI_KYOTEN_CD.ToString());
                            if (kyoten == null)
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                            }
                            else
                            {
                                this.form.SHIHARAI_KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;                         // 支払請求拠点名
                            }
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
        /// <param name="torisakiEntity"></param>
        /// <param name="chiikiName"></param>
        /// <returns></returns>
        private string GetChiikiCd(M_TORIHIKISAKI torisakiEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(torisakiEntity, chiikiName);

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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                        dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
            catch (Exception ex)
            {
                LogUtility.Error("GetChiikiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, chiikiName);
            }
        }

        /// <summary>
        /// 地域CD検索処理
        /// </summary>
        /// <returns></returns>
        private string GetChiikiCd(M_HIKIAI_TORIHIKISAKI torisakiEntity, out string chiikiName)
        {
            string result = string.Empty;
            chiikiName = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(torisakiEntity, chiikiName);

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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                        dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
                    dtChiiki = daoHikiaiGyousha.GetChiikiData(condition);
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
            catch (Exception ex)
            {
                LogUtility.Error("GetChiikiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, chiikiName);
            }
        }

        /// <summary>
        /// 業者CD採番処理
        /// </summary>
        /// <returns>最大CD+1</returns>
        public bool Saiban()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 引合業者マスタのCDの最大値+1を取得
                HikiaiGyoushaMasterAccess gyoushaMasterAccess = new HikiaiGyoushaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyGyousha = -1;

                var keyGyoushasaibanFlag = gyoushaMasterAccess.IsOverCDLimit(out keyGyousha);

                if (keyGyoushasaibanFlag || keyGyousha < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.GYOUSHA_CD.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.GYOUSHA_CD.Text = String.Format("{0:D" + this.form.GYOUSHA_CD.MaxLength + "}", keyGyousha);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Saiban", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                LogUtility.DebugMethodStart();

                M_SHAIN condition = new M_SHAIN();
                condition.SHAIN_CD = eigyouTantouCd;
                if (!string.IsNullOrWhiteSpace(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    condition.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                }
                DataTable dt = this.daoHikiaiGyousha.GetPopupData(condition);
                if (0 < dt.Rows.Count)
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = dt.Rows[0]["BUSHO_CD"].ToString();
                    this.form.BUSHO_NAME.Text = dt.Rows[0]["BUSHO_NAME"].ToString();
                    this.form.EIGYOU_TANTOU_CD.Text = dt.Rows[0]["SHAIN_CD"].ToString();
                    this.form.SHAIN_NAME.Text = dt.Rows[0]["SHAIN_NAME"].ToString();
                }
                else
                {
                    this.form.EIGYOU_TANTOU_CD.BackColor = Constans.ERROR_COLOR;
                    this.form.EIGYOU_TANTOU_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.SHAIN_NAME.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "営業担当者");
                    this.form.EIGYOU_TANTOU_CD.Focus();
                    this.form.EIGYOU_TANTOU_CD.IsInputErrorOccured = true;
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
        /// 現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal bool ShowWindow(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                //選択行からキー項目を取得する
                string cd1 = this.form.GYOUSHA_CD.Text.ToString();
                string cd2 = string.Empty;
                string hikiaiGyoushaUseFlg = string.Empty;
                bool useFlg = false;
                foreach (Row row in this.form.GENBA_ICHIRAN.Rows)
                {
                    if (row.Selected)
                    {
                        cd2 = row.Cells["GENBA_CD"].Value.ToString();
                        hikiaiGyoushaUseFlg = row.Cells["HIKIAI_GYOUSHA_USE_FLG"].Value.ToString();
                        break;
                    }
                }

                if (hikiaiGyoushaUseFlg != null && hikiaiGyoushaUseFlg.ToUpper().Equals("TRUE"))
                {
                    useFlg = true;
                }

                //現場入力画面を表示する
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == windowType)
                {
                    if (Manager.CheckAuthority("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M463", windowType, windowType, useFlg, cd1, cd2);
                    }
                    else if (Manager.CheckAuthority("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, useFlg, cd1, cd2);
                    }
                    else
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }
                else
                {
                    FormManager.OpenFormWithAuth("M463", windowType, windowType, useFlg, cd1, cd2);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowWindow", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 請求情報の送付先情報を業者の情報でコピー
        /// </summary>
        public void GyoushaInfoCopyFromSeikyuuInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SEIKYUU_SOUFU_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.SEIKYUU_SOUFU_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.form.GYOUSHA_TEL.Text;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.form.GYOUSHA_FAX.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromSeikyuuInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払情報の送付先情報を業者の情報でコピー
        /// </summary>
        public void GyoushaInfoCopyFromShiharaiInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SHIHARAI_SOUFU_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.SHIHARAI_SOUFU_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.form.GYOUSHA_TEL.Text;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.form.GYOUSHA_FAX.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromShiharaiInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者分類の返送先情報を業者の情報でコピー
        /// </summary>
        public void GyoushaInfoCopyFromGyoushaBunrui()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.MANI_HENSOUSAKI_NAME1.Text = this.form.GYOUSHA_NAME1.Text;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.form.GYOUSHA_NAME2.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.form.GYOUSHA_KEISHOU1.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.form.GYOUSHA_KEISHOU2.Text;
                this.form.MANI_HENSOUSAKI_POST.Text = this.form.GYOUSHA_POST.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.form.TODOUFUKEN_NAME.Text + this.form.GYOUSHA_ADDRESS1.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.form.GYOUSHA_ADDRESS2.Text;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.form.BUSHO.Text;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.form.TANTOUSHA.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaInfoCopyFromGyoushaBunrui", ex);
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
            try
            {
                LogUtility.DebugMethodStart();

                // 共通地域チェックロジックで地域検索を実行する
                M_CHIIKI chiiki = this.SearchChiikiFromAddress(this.form.GYOUSHA_TODOUFUKEN_CD.Text, this.form.GYOUSHA_ADDRESS1.Text);
                if (chiiki != null)
                {
                    this.form.CHIIKI_CD.Text = chiiki.CHIIKI_CD;
                    this.form.CHIIKI_NAME.Text = chiiki.CHIIKI_NAME_RYAKU;
                }
                else
                {
                    this.form.CHIIKI_CD.Text = string.Empty;
                    this.form.CHIIKI_NAME.Text = string.Empty;
                }

                // 運搬報告書提出先
                if (isUnpanHoukokusyoChange)
                {
                    M_CHIIKI houkokuChiiki = this.SearchChiikiFromAddress(this.form.GYOUSHA_TODOUFUKEN_CD.Text, string.Empty);
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
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechChiiki", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
        /// 取引先コード変更処理
        /// </summary>
        public void ChangeTorihikisai()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.IsSetDataFromPopup && !this.IsSetDataInInit)
                {
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikisai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                    M_TORIHIKISAKI entity = null;
                    M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    M_TORIHIKISAKI[] result = this.daoTorihikisaki.GetAllValidData(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.TORIHIKISAKI_NAME1.Text = entity.TORIHIKISAKI_NAME1;
                        this.form.TORIHIKISAKI_NAME2.Text = entity.TORIHIKISAKI_NAME2;
                        if (entity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = entity.TEKIYOU_BEGIN;
                        }
                        if (entity.TEKIYOU_END.IsNull)
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = entity.TEKIYOU_END;
                        }

                        if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KYOTEN_CD.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        this.form.KYOTEN_NAME.Text = string.Empty;
                        isSet = true;
                    }

                    _tabPageManager.ChangeTabPageVisible(1, true);
                    _tabPageManager.ChangeTabPageVisible(2, true);

                    //取引先区分でタブ状態を変更
                    M_TORIHIKISAKI_SEIKYUU seikyuuEntity = daoSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    M_TORIHIKISAKI_SHIHARAI shiharaiEntity = daoShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                    //請求タブ
                    bool isKake = true;
                    if (seikyuuEntity != null)
                    {
                        if (seikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu, isKake);
                    }

                    //支払タブ
                    isKake = true;
                    if (shiharaiEntity != null)
                    {
                        if (shiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else
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
                    M_HIKIAI_TORIHIKISAKI entity = null;
                    M_HIKIAI_TORIHIKISAKI queryParam = new M_HIKIAI_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    M_HIKIAI_TORIHIKISAKI[] result = this.daoHikiaiTorihikisaki.GetAllValidDataMinCols(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.TORIHIKISAKI_NAME1.Text = entity.TORIHIKISAKI_NAME1;
                        this.form.TORIHIKISAKI_NAME2.Text = entity.TORIHIKISAKI_NAME2;
                        if (entity.TEKIYOU_BEGIN.IsNull)
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = entity.TEKIYOU_BEGIN;
                        }
                        if (entity.TEKIYOU_END.IsNull)
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_TEKIYOU_END.Value = entity.TEKIYOU_END;
                        }

                        if (!entity.TORIHIKISAKI_KYOTEN_CD.IsNull)
                        {
                            this.form.KYOTEN_CD.Text = entity.TORIHIKISAKI_KYOTEN_CD.ToString();
                        }
                        this.form.KYOTEN_NAME.Text = string.Empty;
                        isSet = true;
                    }

                    _tabPageManager.ChangeTabPageVisible(1, true);
                    _tabPageManager.ChangeTabPageVisible(2, true);

                    M_HIKIAI_TORIHIKISAKI_SEIKYUU hikiaiSeikyuuEntity = this.daoHikiaiSeikyuu.GetDataByCd(queryParam.TORIHIKISAKI_CD);
                    M_HIKIAI_TORIHIKISAKI_SHIHARAI hikiaishiharaiEntity = this.daoHikiaiShiharai.GetDataByCd(queryParam.TORIHIKISAKI_CD);

                    bool isKake = true;
                    if (hikiaiSeikyuuEntity != null)
                    {
                        if (hikiaiSeikyuuEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else
                        {
                            isKake = true;
                        }
                    }
                    // 非活性⇔活性にするタイミングでのみ値を設定
                    if ((!this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && isKake) || (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled && !isKake))
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Siharai, isKake);
                    }

                    isKake = true;
                    if (hikiaishiharaiEntity != null)
                    {
                        if (hikiaishiharaiEntity.TORIHIKI_KBN_CD == 1)
                        {
                            isKake = false;
                        }
                        else
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
                    this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                    this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                    this.form.TORIHIKISAKI_TEKIYOU_BEGIN.Value = null;
                    this.form.TORIHIKISAKI_TEKIYOU_END.Value = null;

                    this.form.KYOTEN_CD.Text = string.Empty;
                    this.form.KYOTEN_NAME.Text = string.Empty;
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = string.Empty;

                    if (this.form.TORIHIKISAKI_CD.Text != "")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "取引先");
                        //取引先に移動
                        this.form.TORIHIKISAKI_CD.Focus();
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    }

                    _tabPageManager.ChangeTabPageVisible(1, true);
                    _tabPageManager.ChangeTabPageVisible(2, true);

                    //非活性になっていた場合だけ活性化する。
                    if (!this.form.SEIKYUU_SOUFU_NAME1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Seikyuu, true);
                    }
                    if (!this.form.SHIHARAI_SOUFU_NAME1.Enabled)
                    {
                        this.ChangeTorihikisakiKbn(Const.ConstCls.TorihikisakiKbnProcessType.Siharai, true);
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.form.KYOTEN_CD.Text))
                {
                    this.kyotenEntity = null;
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.form.KYOTEN_CD.Text);
                    count = this.kyotenEntity == null ? 0 : 1;

                    //拠点名セットを行う
                    if (count > 0)
                    {
                        this.form.KYOTEN_NAME.Text = this.kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex1);
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

                this.form.bt_souhusaki_torihikisaki_copy.Enabled = isKake;              // 請求書業者情報コピー
                this.form.bt_souhusaki_address.Enabled = isKake;                        // 請求書住所参照
                this.form.bt_souhusaki_post.Enabled = isKake;                           // 請求書郵便番号参照
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

                this.form.bt_shiharai_souhusaki_torihikisaki_copy.Enabled = isKake;     // 支払書業者情報コピー
                this.form.bt_shiharai_address.Enabled = isKake;                         // 支払書住所参照
                this.form.bt_shiharai_post.Enabled = isKake;                            // 支払書郵便番号参照
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
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SEIKYUU_KYOTEN_CD.ToString()));
                        if (!this.SeikyuuKyotenCdValidated())
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
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
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSeikyuuKyotenPrintKbn", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        public bool ChangeShiharaiKyotenPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
                    if (!sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.sysinfoEntity.GYOUSHA_SHIHARAI_KYOTEN_CD.ToString()));
                        if (!this.ShiharaiKyotenCdValidated())
                        {
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
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
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKyotenPrintKbn", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
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
                        ret = false;
                    }
                }

                if (!ret)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex1);
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
                        ret = false;
                    }
                }

                if (!ret)
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
        /// 営業拠点部署の値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.form.BUSHO_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    M_BUSHO[] busho = this.daoBusho.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        this.form.BUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
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
                    this.form.EIGYOU_TANTOU_BUSHO_CD.SelectAll();
                    this.form.BUSHO_NAME.Text = string.Empty;
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
        // 201400709 syunrei #947 №19 start
        /// <summary>
        /// 修正モードに画面の業者コードより、移行前のデータ作り
        /// </summary>
        public M_HIKIAI_GYOUSHA CreateIkouData(M_HIKIAI_GYOUSHA ikouBeforeData, out bool catchErr)
        {
            catchErr = true;
            try
            {
                //移行前のデータ 理論削除
                if (ikouBeforeData != null && !string.IsNullOrEmpty(ikouBeforeData.GYOUSHA_CD))
                {
                    this.DeleteIkouBfData(ikouBeforeData);
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateIkouData", ex1);
                this.form.messBSL.MessageBoxShow("E080", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateIkouData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return ikouBeforeData;
        }
        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end

        /// <summary>
        /// 理論削除
        /// </summary>
        public void DeleteIkouBfData(M_HIKIAI_GYOUSHA ikouBefore)
        {
            try
            {
                LogUtility.DebugMethodStart(ikouBefore);

                //理論削除
                // 業者マスタ更新
                ikouBefore.DELETE_FLG = true;

                this.daoHikiaiGyousha.Update(ikouBefore);
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
        public bool InsertIkouData(M_GYOUSHA ikouBefore)
        {
            LogUtility.DebugMethodStart(ikouBefore);
            bool res = true;
            try
            {
                //見積データに更新前の業者コード
                string oldGyoshaCd = string.Empty;
                //見積データに更新後の業者コード
                string newGyoshaCd = string.Empty;

                if (ikouBefore != null && !string.IsNullOrEmpty(ikouBefore.GYOUSHA_CD))
                {
                    oldGyoshaCd = ikouBefore.GYOUSHA_CD;
                    newGyoshaCd = this.GetM_GYOUSHA_MaxCD();

                    // 採番できなかったときはデータ移行を中止する
                    if (this.SaibanErrorFlg)
                    {
                        return false;
                    }

                    ikouBefore.GYOUSHA_CD = newGyoshaCd;
                    ikouBefore.DELETE_FLG = false;

                    var WHO = new DataBinderLogic<M_GYOUSHA>(ikouBefore);
                    WHO.SetSystemProperty(ikouBefore, false);

                    //業者マスタに登録
                    daoGyousha.InsertGyosha(ikouBefore);

                    //見積データを更新
                    daoHikiaiGyousha.UpdateGYOUSHA_CD(oldGyoshaCd, newGyoshaCd);

                    // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
                    // 連携キを更新
                    daoHikiaiGyousha.UpdateGYOUSHA_CD_AFTER(oldGyoshaCd, newGyoshaCd);
                    //移行後現場業者のデータを更新処理
                    daoGenba.UpdateGenba_CD(oldGyoshaCd, newGyoshaCd);
                    // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end

                    // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない start
                    // 現場_定期情報マスタに業者CDを更新する
                    daoGenba.UpdateGenbaTEIKI(oldGyoshaCd, newGyoshaCd);

                    // 現場_月極情報マスタに業者CDを更新する
                    daoGenba.UpdateGenbaTSUKI(oldGyoshaCd, newGyoshaCd);
                    // 20140718 ria EV005242 引合現場を移行させるとき、定期回収情報タブと月極情報タブのみ移行されない end
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
        public M_GYOUSHA HikiToTsujou(M_HIKIAI_GYOUSHA mhg)
        {
            M_GYOUSHA ikouData = new M_GYOUSHA();

            ikouData.GYOUSHA_CD = mhg.GYOUSHA_CD;
            ikouData.TORIHIKISAKI_UMU_KBN = mhg.TORIHIKISAKI_UMU_KBN;
            ikouData.GYOUSHAKBN_UKEIRE = mhg.GYOUSHAKBN_UKEIRE;
            ikouData.GYOUSHAKBN_SHUKKA = mhg.GYOUSHAKBN_SHUKKA;
            ikouData.GYOUSHAKBN_MANI = mhg.GYOUSHAKBN_MANI;
            ikouData.TORIHIKISAKI_CD = mhg.TORIHIKISAKI_CD;
            ikouData.KYOTEN_CD = mhg.KYOTEN_CD;
            ikouData.GYOUSHA_NAME1 = mhg.GYOUSHA_NAME1;
            ikouData.GYOUSHA_NAME2 = mhg.GYOUSHA_NAME2;
            ikouData.GYOUSHA_NAME_RYAKU = mhg.GYOUSHA_NAME_RYAKU;
            ikouData.GYOUSHA_FURIGANA = mhg.GYOUSHA_FURIGANA;
            ikouData.GYOUSHA_TEL = mhg.GYOUSHA_TEL;
            ikouData.GYOUSHA_FAX = mhg.GYOUSHA_FAX;
            ikouData.GYOUSHA_KEITAI_TEL = mhg.GYOUSHA_KEITAI_TEL;
            ikouData.GYOUSHA_KEISHOU1 = mhg.GYOUSHA_KEISHOU1;
            ikouData.GYOUSHA_KEISHOU2 = mhg.GYOUSHA_KEISHOU2;
            ikouData.EIGYOU_TANTOU_BUSHO_CD = mhg.EIGYOU_TANTOU_BUSHO_CD;
            ikouData.EIGYOU_TANTOU_CD = mhg.EIGYOU_TANTOU_CD;
            ikouData.GYOUSHA_POST = mhg.GYOUSHA_POST;
            ikouData.GYOUSHA_TODOUFUKEN_CD = mhg.GYOUSHA_TODOUFUKEN_CD;
            ikouData.GYOUSHA_ADDRESS1 = mhg.GYOUSHA_ADDRESS1;
            ikouData.GYOUSHA_ADDRESS2 = mhg.GYOUSHA_ADDRESS2;
            ikouData.TORIHIKI_JOUKYOU = mhg.TORIHIKI_JOUKYOU;
            ikouData.CHUUSHI_RIYUU1 = mhg.CHUUSHI_RIYUU1;
            ikouData.CHUUSHI_RIYUU2 = mhg.CHUUSHI_RIYUU2;
            ikouData.BUSHO = mhg.BUSHO;
            ikouData.TANTOUSHA = mhg.TANTOUSHA;
            ikouData.SHUUKEI_ITEM_CD = mhg.SHUUKEI_ITEM_CD;
            ikouData.GYOUSHU_CD = mhg.GYOUSHU_CD;
            ikouData.CHIIKI_CD = mhg.CHIIKI_CD;
            ikouData.BIKOU1 = mhg.BIKOU1;
            ikouData.BIKOU2 = mhg.BIKOU2;
            ikouData.BIKOU3 = mhg.BIKOU3;
            ikouData.BIKOU4 = mhg.BIKOU4;
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
            ikouData.HAISHUTSU_NIZUMI_GYOUSHA_KBN = mhg.HAISHUTSU_NIZUMI_GYOUSHA_KBN;
            ikouData.UNPAN_JUTAKUSHA_KAISHA_KBN = mhg.UNPAN_JUTAKUSHA_KAISHA_KBN;
            ikouData.SHOBUN_NIOROSHI_GYOUSHA_KBN = mhg.SHOBUN_NIOROSHI_GYOUSHA_KBN;
            ikouData.MANI_HENSOUSAKI_KBN = mhg.MANI_HENSOUSAKI_KBN;
            ikouData.SHOKUCHI_KBN = mhg.SHOKUCHI_KBN;
            ikouData.MANI_HENSOUSAKI_NAME1 = mhg.MANI_HENSOUSAKI_NAME1;
            ikouData.MANI_HENSOUSAKI_NAME2 = mhg.MANI_HENSOUSAKI_NAME2;
            ikouData.MANI_HENSOUSAKI_KEISHOU1 = mhg.MANI_HENSOUSAKI_KEISHOU1;
            ikouData.MANI_HENSOUSAKI_KEISHOU2 = mhg.MANI_HENSOUSAKI_KEISHOU2;
            ikouData.MANI_HENSOUSAKI_POST = mhg.MANI_HENSOUSAKI_POST;
            ikouData.MANI_HENSOUSAKI_ADDRESS1 = mhg.MANI_HENSOUSAKI_ADDRESS1;
            ikouData.MANI_HENSOUSAKI_ADDRESS2 = mhg.MANI_HENSOUSAKI_ADDRESS2;
            ikouData.MANI_HENSOUSAKI_BUSHO = mhg.MANI_HENSOUSAKI_BUSHO;
            ikouData.MANI_HENSOUSAKI_TANTOU = mhg.MANI_HENSOUSAKI_TANTOU;
            ikouData.TEKIYOU_BEGIN = mhg.TEKIYOU_BEGIN;
            ikouData.TEKIYOU_END = mhg.TEKIYOU_END;
            ikouData.CREATE_USER = mhg.CREATE_USER;
            ikouData.CREATE_DATE = mhg.CREATE_DATE;
            ikouData.CREATE_PC = mhg.CREATE_PC;
            ikouData.UPDATE_USER = mhg.UPDATE_USER;
            ikouData.UPDATE_DATE = mhg.UPDATE_DATE;
            ikouData.UPDATE_PC = mhg.UPDATE_PC;
            ikouData.DELETE_FLG = mhg.DELETE_FLG;
            //ikouData.TIME_STAMP = mhg.TIME_STAMP;
            //ikouData.TIME_STAMP ='System.Byte[]'
            ikouData.GYOUSHA_DAIHYOU = mhg.GYOUSHA_DAIHYOU;
            // 20141209 katen #2927 実績報告書 フィードバック対応 start
            ikouData.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD = mhg.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD;
            // 20141209 katen #2927 実績報告書 フィードバック対応 end
            ikouData.MANI_HENSOUSAKI_THIS_ADDRESS_KBN = mhg.MANI_HENSOUSAKI_THIS_ADDRESS_KBN;

            return ikouData;
        }

        /// <summary>
        /// 通常マスタの最大コードを取得する
        /// </summary>
        public string GetM_GYOUSHA_MaxCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string res = string.Empty;
                this.SaibanErrorFlg = false;

                // 通常業者マスタのCDの最大値+1を取得 trueの場合、通常業者マスタから取得する
                HikiaiGyoushaMasterAccess gyoushaMasterAccess = new HikiaiGyoushaMasterAccess(new CustomTextBox(), new object[] { }, new object[] { }, true);
                int keyGyousha = -1;

                var keyGyoushasaibanFlag = gyoushaMasterAccess.IsOverCDLimit(out keyGyousha, true);

                if (keyGyoushasaibanFlag || keyGyousha < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.SaibanErrorFlg = true;
                    return res;
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    res = String.Format("{0:D" + this.form.GYOUSHA_CD.MaxLength + "}", keyGyousha);
                }
                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
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
            //修正モードの場合、F1移行ボタンが利用可
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

        // 20140716 chinchis  EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する start
        public string getTorihikisakiAFTER(string cd, out bool catchErr)
        {
            M_TORIHIKISAKI hikiaiGyousha = new M_TORIHIKISAKI();
            catchErr = true;
            string res = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(cd);

                hikiaiGyousha = daoTorihikisaki.GetDataByCd(cd);
                if (hikiaiGyousha != null)
                {
                    res = hikiaiGyousha.TORIHIKISAKI_CD;
                }
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
                LogUtility.DebugMethodEnd(res, catchErr);
            }

            return res;
        }

        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
        public M_HIKIAI_GYOUSHA ikouBeforeData(M_HIKIAI_GYOUSHA gyoCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                return this.daoHikiaiGyousha.GetGyoushaData(gyoCd);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ikouBeforeData", ex1);
                this.form.messBSL.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ikouBeforeData", ex);
                this.form.messBSL.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }
        // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end
        // 20140716 chinchis  EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end
        // 20141203 Houkakou 「引合業者入力」の日付チェックを追加する start

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
                this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
                this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;

                DateTime date_from = new DateTime(1, 1, 1);
                DateTime date_to = new DateTime(9999, 12, 31);
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
                {
                    DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out date_from);
                }
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
                {
                    DateTime.TryParse(this.form.TEKIYOU_END.Text, out date_to);
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

                // 業者適用開始日 < 取引先適用開始日 場合
                if (date_from.CompareTo(torihiki_from) < 0)
                {
                    string[] errorMsg = { "適用開始日", "業者", "取引先", "前", "以降" };
                    msgLogic.MessageBoxShow("E255", errorMsg);
                    this.form.TEKIYOU_BEGIN.Focus();
                    return true;
                }

                // 取引先適用終了日 < 業者適用終了日 場合
                if (torihiki_to.CompareTo(date_to) < 0)
                {
                    string[] errorMsg = { "適用終了日", "業者", "取引先", "後", "以前" };
                    msgLogic.MessageBoxShow("E255", errorMsg);
                    this.form.TEKIYOU_END.Focus();
                    return true;
                }

                M_HIKIAI_GYOUSHA data = new M_HIKIAI_GYOUSHA();
                data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                // 現場適用開始日<業者適用開始日
                DataTable begin = this.daoHikiaiGyousha.GetTekiyouBegin(data);
                DateTime date;
                if (begin != null && begin.Rows.Count > 0)
                {
                    DateTime.TryParse(Convert.ToString(begin.Rows[0][0]), out date);
                    if (date.CompareTo(date_from) < 0)
                    {
                        string[] errorMsg = { "適用開始日", "業者", "現場", "後", "以前" };
                        msgLogic.MessageBoxShow("E255", errorMsg);
                        this.form.TEKIYOU_BEGIN.Focus();
                        return true;
                    }
                }

                // 現場適用終了日>業者適用終了日
                DataTable end = this.daoHikiaiGyousha.GetTekiyouEnd(data);
                if (end != null && end.Rows.Count > 0)
                {
                    DateTime.TryParse(Convert.ToString(end.Rows[0][0]), out date);
                    if (date.CompareTo(date_to) > 0)
                    {
                        string[] errorMsg = { "適用終了日", "業者", "現場", "前", "以降" };
                        msgLogic.MessageBoxShow("E255", errorMsg);
                        this.form.TEKIYOU_END.Focus();
                        return true;
                    }
                }

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.TEKIYOU_BEGIN.IsInputErrorOccured = true;
                    this.form.TEKIYOU_END.IsInputErrorOccured = true;
                    this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                    this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "適用開始", "適用終了" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.TEKIYOU_BEGIN.Focus();
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DateCheck", ex1);
                msgLogic.MessageBoxShow("E093", "");
                return true;
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

        #region TEKIYOU_BEGIN_Leaveイベント

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TEKIYOU_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TEKIYOU_END.Text))
            {
                this.form.TEKIYOU_END.IsInputErrorOccured = false;
                this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region TEKIYOU_END_Leaveイベント

        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TEKIYOU_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
            {
                this.form.TEKIYOU_BEGIN.IsInputErrorOccured = false;
                this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        // 20141203 Houkakou 「引合業者入力」の日付チェックを追加する end

        // 20141209 katen #2927 実績報告書 フィードバック対応 start
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
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

            this.upnHoukokushoTeishutsuChiikiEntity = daoChiiki.GetDataByCd(this.hikiaiGyoushaEntity.UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);

            int count = this.upnHoukokushoTeishutsuChiikiEntity == null ? 0 : 1;

            LogUtility.DebugMethodEnd(count);
            return count;
        }
        // 20141209 katen #2927 実績報告書 フィードバック対応 end

        /// <summary>
        /// 返送先情報変更処理
        /// </summary>
        public void ChangeManiHensousakiAddKbn()
        {
            LogUtility.DebugMethodStart();

            if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text.Equals("1"))
            {
                this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

                this.form.MANI_HENSOUSAKI_NAME1.Enabled = false;
                this.form.MANI_HENSOUSAKI_NAME2.Enabled = false;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = false;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = false;
                this.form.MANI_HENSOUSAKI_POST.Enabled = false;
                this.form.bt_hensousaki_torihikisaki_copy.Enabled = false;
                this.form.bt_hensousaki_address.Enabled = false;
                this.form.bt_hensousaki_post.Enabled = false;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
            }
            else
            {
                this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                this.form.bt_hensousaki_torihikisaki_copy.Enabled = true;
                this.form.bt_hensousaki_address.Enabled = true;
                this.form.bt_hensousaki_post.Enabled = true;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
            }

            LogUtility.DebugMethodEnd();
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
                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
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
                        queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
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
                        queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
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
        /// OpenGenbaForm
        /// </summary>
        /// <param name="GyoushaCd"></param>
        /// <param name="GenbaCd"></param>
        internal void OpenHikiaiGenbaForm(WINDOW_TYPE wType, bool HikiaiKihonFlg, string HikiaiGyoushaCd, string HikiaiGenbaCd)
        {
            LogUtility.DebugMethodStart(wType, HikiaiKihonFlg, HikiaiGyoushaCd, HikiaiGenbaCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (!r_framework.Authority.Manager.CheckAuthority("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                if (!r_framework.Authority.Manager.CheckAuthority("M463", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                wType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }

            r_framework.FormManager.FormManager.OpenFormWithAuth("M463", wType, wType, HikiaiKihonFlg, HikiaiGyoushaCd, HikiaiGenbaCd);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// OpenHikiaiTorihikisakiFormReference
        /// </summary>
        /// <param name="TorihikisakiCd"></param>
        internal void OpenHikiaiTorihikisakiFormReference(string HikiaiTorihikisakiCd)
        {
            LogUtility.DebugMethodStart(HikiaiTorihikisakiCd);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            WINDOW_TYPE windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            //check exist M_TORIHIKISAKI
            string formId = "M461";
            if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
            {
                M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
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

            r_framework.FormManager.FormManager.OpenFormWithAuth(formId, windowType, windowType, HikiaiTorihikisakiCd);
            LogUtility.DebugMethodEnd();
        }
    }
}