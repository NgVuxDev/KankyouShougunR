// $Id: LogicCls.cs 52661 2015-06-18 01:47:06Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
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
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.APP;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Const;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.DBAccesser;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Validator;
using System.ComponentModel;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Logic
{
    /// <summary>
    /// 引合取引先入力のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.HikiaiTorihikisakiHoshu.Setting.ButtonSetting.xml";

        private readonly string ButtonInfoXmlPath2 = "Shougun.Core.Master.HikiaiTorihikisakiHoshu.Setting.ButtonSetting2.xml";

        /// <summary>
        /// 引合取引先入力Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        internal M_HIKIAI_TORIHIKISAKI entitysTORIHIKISAKI;

        private M_HIKIAI_TORIHIKISAKI_SHIHARAI entitysTOROHIKISAKI_SHIHARAI;

        private M_HIKIAI_TORIHIKISAKI_SEIKYUU entitysTORIHIKISAKI_SEIKYUU;

        private M_KYOTEN entitysM_KYOTEN;

        private M_BUSHO entitysM_BUSHO;

        private M_SHAIN entitysM_SHAIN;

        private M_SHUUKEI_KOUMOKU entitysM_SHUUKEI_KOUMOKU;

        private M_GYOUSHU entitysM_GYOUSHU;

        private M_TODOUFUKEN entitysM_TODOUFUKEN;

        internal M_SYS_INFO entitysM_SYS_INFO;

        private M_BANK entitysM_BANK;

        private M_BANK_SHITEN entitysM_BANK_SHITEN;

        private M_NYUUSHUKKIN_KBN entitysM_NYUUSHUKKIN_KBN;

        private M_HIKIAI_GYOUSHA[] entitysM_HIKIAI_GYOUSHA_LIST;

        private M_HIKIAI_GENBA[] entitysM_HIKIAI_GENBA_LIST;

        /// <summary>
        /// 加入者番号
        /// </summary>
        // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao daoTORIHIKISAKI;

        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao daoTORIHIKISAKI_SHIHARAI;

        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao daoTORIHIKISAKI_SEIKYUU;

        // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_GYOUSHADao daoGYOUSHA;

        private Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_GENBADao daoGENBA;

        // 20140708 ria No.947 営業管理機能改修 start
        //通常マスタ
        //private M_TORIHIKISAKI targetTORIHIKISAKI;
        //private M_TORIHIKISAKI_SHIHARAI targetTOROHIKISAKI_SHIHARAI;
        //private M_TORIHIKISAKI_SEIKYUU targetTORIHIKISAKI_SEIKYUU;

        //通常マスタDAO
        private IM_TORIHIKISAKIMASTERDao targetTORIHIKISAKIDAO;

        private IM_TORIHIKISAKI_SHIHARAIMASTERDao targetTUUTORIHIKISAKI_SHIHARAIDAO;

        private IM_TORIHIKISAKI_SEIKYUUMASTERDao targetTUUTORIHIKISAKI_SEIKYUUDAO;

        // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
        //入金先マスタ
        private M_NYUUKINSAKI entitysM_NYUUKINSAKI;

        private IM_NYUUKINSAKIMASTERDao targetM_NYUUKINSAKIDAO;

        //出金先マスタ
        private M_SYUKKINSAKI entitysM_SYUKKINSAKI;

        private IM_SYUKKINSAKIMASTERDao targetM_SYUKKINSAKIDAO;
        // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

        //見積もりデータ
        private T_MITSUMORI_ENTRY entitysMITSUMORI;

        private IT_MITSUMORI_ENTRYDao daoMITSUMORI_ENTRY;
        // 20140708 ria No.947 営業管理機能改修 end

        private IM_KYOTENDao daoIM_KYOTEN;

        private IM_BUSHODao daoIM_BUSHO;

        private IM_SHAINDao daoIM_SHAIN;

        private IM_SHUUKEI_KOUMOKUDao daoIM_SHUUKEI_KOUMOKU;

        private IM_GYOUSHADao daoIM_GYOUSHA;

        private IM_GYOUSHUDao daoIM_GYOUSHU;

        private IM_TODOUFUKENDao daoIM_TODOUFUKEN;

        private IM_SYS_INFODao daoIM_SYS_INFO;

        private IM_BANKDao daoIM_BANK;

        private IM_BANK_SHITENDao daoIM_BANK_SHITEN;

        private IM_NYUUSHUKKIN_KBNDao daoIM_NYUUSHUKKIN_KBN;

        private IS_ZIP_CODEDao daoIS_ZIP_CODE;

        private IM_CORP_INFODao daoIM_CORP_INFO;

        private int rowCntGyousha;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start

        /// <summary>
        /// 採番でエラーが起きたか判断するフラグ
        /// </summary>
        private bool SaibanErrorFlg = false;

        /// <summary>
        /// 電子申請オプションフラグ
        /// true = 有効 / false = 無効
        /// </summary>
        private bool DensisinseiOptionEnabledFlg;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_HIKIAI_TORIHIKISAKI SearchString { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string torihikisakiCD { get; set; }

        /// <summary>
        /// 電子申請フラグ
        /// </summary>
        public bool denshiShinseiFlg { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// 検索結果(業者一覧)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        ///　エラー判定フラグ
        /// </summary>
        public bool isError { get; set; }

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

                Console.WriteLine("M461Logic created!");

                this.form = targetForm;

                this.entitysTORIHIKISAKI = new M_HIKIAI_TORIHIKISAKI();
                this.entitysTOROHIKISAKI_SHIHARAI = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();
                this.entitysTORIHIKISAKI_SEIKYUU = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
                this.entitysM_KYOTEN = new M_KYOTEN();
                this.entitysM_BUSHO = new M_BUSHO();
                this.entitysM_SHAIN = new M_SHAIN();
                this.entitysM_SHUUKEI_KOUMOKU = new M_SHUUKEI_KOUMOKU();
                this.entitysM_GYOUSHU = new M_GYOUSHU();
                this.entitysM_TODOUFUKEN = new M_TODOUFUKEN();
                this.entitysM_SYS_INFO = new M_SYS_INFO();
                this.entitysM_BANK = new M_BANK();
                this.entitysM_BANK_SHITEN = new M_BANK_SHITEN();
                this.entitysM_NYUUSHUKKIN_KBN = new M_NYUUSHUKKIN_KBN();
                this.entitysM_HIKIAI_GYOUSHA_LIST = new M_HIKIAI_GYOUSHA[] { };
                this.entitysM_HIKIAI_GENBA_LIST = new M_HIKIAI_GENBA[] { };

                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoTORIHIKISAKI = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoTORIHIKISAKI_SHIHARAI = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao>();
                this.daoTORIHIKISAKI_SEIKYUU = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoGYOUSHA = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_GYOUSHADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoGENBA = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao.IM_HIKIAI_GENBADao>();
                this.daoIM_KYOTEN = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoIM_BUSHO = DaoInitUtility.GetComponent<IM_BUSHODao>();
                this.daoIM_SHAIN = DaoInitUtility.GetComponent<IM_SHAINDao>();
                this.daoIM_SHUUKEI_KOUMOKU = DaoInitUtility.GetComponent<IM_SHUUKEI_KOUMOKUDao>();
                this.daoIM_GYOUSHA = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.daoIM_GYOUSHU = DaoInitUtility.GetComponent<IM_GYOUSHUDao>();
                this.daoIM_TODOUFUKEN = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
                this.daoIM_SYS_INFO = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoIM_BANK = DaoInitUtility.GetComponent<IM_BANKDao>();
                this.daoIM_BANK_SHITEN = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                this.daoIM_NYUUSHUKKIN_KBN = DaoInitUtility.GetComponent<IM_NYUUSHUKKIN_KBNDao>();
                this.daoIS_ZIP_CODE = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
                this.daoIM_CORP_INFO = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                // 20140708 ria No.947 営業管理機能改修 start
                //this.targetTORIHIKISAKI = new M_TORIHIKISAKI();
                //this.targetTOROHIKISAKI_SHIHARAI = new M_TORIHIKISAKI_SHIHARAI();
                //this.targetTORIHIKISAKI_SEIKYUU = new M_TORIHIKISAKI_SEIKYUU();

                this.targetTORIHIKISAKIDAO = DaoInitUtility.GetComponent<IM_TORIHIKISAKIMASTERDao>();
                this.targetTUUTORIHIKISAKI_SHIHARAIDAO = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIMASTERDao>();
                this.targetTUUTORIHIKISAKI_SEIKYUUDAO = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUMASTERDao>();

                this.entitysMITSUMORI = new T_MITSUMORI_ENTRY();
                this.daoMITSUMORI_ENTRY = DaoInitUtility.GetComponent<IT_MITSUMORI_ENTRYDao>();
                // 20140708 ria No.947 営業管理機能改修 end

                // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
                //入金先マスタ
                this.targetM_NYUUKINSAKIDAO = DaoInitUtility.GetComponent<IM_NYUUKINSAKIMASTERDao>();
                //出金先マスタ
                this.targetM_SYUKKINSAKIDAO = DaoInitUtility.GetComponent<IM_SYUKKINSAKIMASTERDao>();
                // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

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
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);
                this.allControl = this.form.allControl;

                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                this.setOnlineBankVisible();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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

                if (string.IsNullOrEmpty(this.torihikisakiCD))
                {
                    //【新規】モードで初期化
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
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 全コントロールを操作可能とする
                this.AllControlLock(false);

                // 検索結果を画面に設定
                this.SetWindowData();
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // 修正モード固有UI設定
                this.form.TORIHIKISAKI_CD.Enabled = false;  // 取引先CD
                this.form.SAIBAN_BUTTON.Enabled = false;    // 採番ボタン

                // functionボタン
                // 電子申請内容選択入力から呼ばれた場合
                if (this.denshiShinseiFlg)
                {
                    if (DensisinseiOptionEnabledFlg)
                    {
                        // 電子申請オプションが有効の場合
                        parentForm.bt_func1.Text = string.Empty;
                        parentForm.bt_func1.Tag = string.Empty;
                    }
                    parentForm.bt_func1.Enabled = false;    // 移行
                    parentForm.bt_func2.Enabled = false;    // 新規
                    parentForm.bt_func3.Enabled = false;    // 修正
                    parentForm.bt_func7.Enabled = false;    // 一覧
                    parentForm.bt_func9.Enabled = true;     // 申請
                    parentForm.bt_func11.Enabled = false;   // 取消
                    parentForm.bt_func12.Enabled = true;    // 閉じる
                }
                else
                {
                    // 電子申請オプションが無効の時のみ表示
                    if (!DensisinseiOptionEnabledFlg)
                    {
                        parentForm.bt_func1.Enabled = true;     // 移行
                    }
                    parentForm.bt_func2.Enabled = true;     // 新規
                    parentForm.bt_func3.Enabled = false;    // 修正
                    parentForm.bt_func7.Enabled = true;     // 一覧
                    parentForm.bt_func9.Enabled = true;     // 登録
                    parentForm.bt_func11.Enabled = true;    // 取消
                    parentForm.bt_func12.Enabled = true;    // 閉じる
                }

                // 業者分類タブ初期化
                this.ManiCheckOffCheck();

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitUpdate", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // 全コントロールを操作不可とする
                this.AllControlLock(true);

                // functionボタン
                // 電子申請オプションが無効の時のみ表示
                if (!DensisinseiOptionEnabledFlg)
                {
                    parentForm.bt_func1.Enabled = true;     // 移行
                }
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = true;     // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
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
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // 全コントロールを操作不可とする
                this.AllControlLock(true);

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = true;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitDelete", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                this.AllControlLock(false);

                //// フォーカスをセットする
                //this.form.NYUUKINSAKI_KBN.Focus();

                // 最新のSYS_INFOを取得
                M_SYS_INFO[] sysInfo = this.daoIM_SYS_INFO.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.entitysM_SYS_INFO = sysInfo[0];
                }
                else
                {
                    this.entitysM_SYS_INFO = null;
                }

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 共通項目
                this.form.NYUUKINSAKI_KBN.Text = "1";
                this.form.TORIHIKISAKI_KYOTEN_CD.Text = "99";
                this.entitysM_KYOTEN = this.daoIM_KYOTEN.GetDataByCd("99");
                this.form.KYOTEN_NAME_RYAKU.Text = string.Empty;
                if (this.entitysM_KYOTEN != null)
                {
                    this.form.KYOTEN_NAME_RYAKU.Text = this.entitysM_KYOTEN.KYOTEN_NAME_RYAKU;
                }
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_FURIGANA.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_KEISHOU1.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.TORIHIKISAKI_KEISHOU2.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_TEL.Text = string.Empty;
                this.form.TORIHIKISAKI_FAX.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate;
                this.form.TEKIYOU_END.Value = null;
                this.form.CHUUSHI_RIYUU1.Text = string.Empty;
                this.form.CHUUSHI_RIYUU2.Text = string.Empty;

                // 基本情報タブ
                this.form.TORIHIKISAKI_POST.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = string.Empty;
                this.form.TORIHIKISAKI_ADDRESS1.Text = string.Empty;
                this.form.TORIHIKISAKI_ADDRESS2.Text = string.Empty;
                this.form.BUSHO.Text = string.Empty;
                this.form.TANTOUSHA.Text = string.Empty;
                this.form.SHUUKEI_ITEM_CD.Text = string.Empty;
                this.form.SHUUKEI_KOUMOKU_NAME.Text = string.Empty;
                this.form.GYOUSHU_CD.Text = string.Empty;
                this.form.GYOUSHU_NAME.Text = string.Empty;
                this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;
                this.form.BIKOU3.Text = string.Empty;
                this.form.BIKOU4.Text = string.Empty;
                this.form.SHOKUCHI_KBN.Checked = false;

                //請求情報1タブ
                this.form.TORIHIKI_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_TORIHIKI_KBN.IsNull)
                {
                    this.form.TORIHIKI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_TORIHIKI_KBN.ToString();
                }
                this.form.TAX_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_TAX_HASUU_CD.IsNull)
                {
                    this.form.TAX_HASUU_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_TAX_HASUU_CD.ToString();
                }
                this.form.KINGAKU_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.KINGAKU_HASUU_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_KINGAKU_HASUU_CD.ToString();
                }
                this.form.ZEI_KEISAN_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.ZEI_KEISAN_KBN_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_ZEI_KEISAN_KBN_CD.ToString();
                }
                this.form.ZEI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_ZEI_KBN_CD.IsNull)
                {
                    this.form.ZEI_KBN_CD.Text = this.entitysM_SYS_INFO.SEIKYUU_ZEI_KBN_CD.ToString();
                }
                // 出力区分
                this.form.OUTPUT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.IsNull)
                {
                    this.form.OUTPUT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.ToString();
                }
                // 出力区分変更後処理
                this.form.HAKKOUSAKI_CD.Text = string.Empty;
                this.ChangeOutputKbn();
                //160026 S
                this.form.KAISHUU_BETSU_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.IsNull)
                {
                    this.form.KAISHUU_BETSU_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.Value.ToString();
                }
                this.form.KAISHUU_BETSU_KBN_TextChanged(null, null);
                this.form.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.form.KAISHUU_BETSU_NICHIGO.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.Value.ToString();
                }
                //160026 E
                // 請求情報2タブ
                this.form.SEIKYUU_JOUHOU1.Text = string.Empty;
                this.form.SEIKYUU_JOUHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                this.form.NYUUKINSAKI_CD.Enabled = false;
                this.form.NYUUKINSAKI_CD.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                this.form.SEIKYUU_TANTOU.Text = string.Empty;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                this.form.FURIKOMI_NAME1.Text = string.Empty;
                this.form.FURIKOMI_NAME2.Text = string.Empty;
                //初期値をセットする。
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
                }
                this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN seikyuuKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                if (seikyuuKyoten != null)
                {
                    this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                }
                
                this.ChangeSeikyuuKyotenPrintKbn();

                // 振込銀行タブ
                this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                this.form.KOUZA_SHURUI.Text = string.Empty;
                this.form.KOUZA_NO.Text = string.Empty;
                this.form.KOUZA_NAME.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                this.form.LAST_TORIHIKI_DATE.Text = string.Empty;
                this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                this.form.KOUZA_SHURUI_2.Text = string.Empty;
                this.form.KOUZA_NO_2.Text = string.Empty;
                this.form.KOUZA_NAME_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                this.form.KOUZA_SHURUI_3.Text = string.Empty;
                this.form.KOUZA_NO_3.Text = string.Empty;
                this.form.KOUZA_NAME_3.Text = string.Empty;
                this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.torihikisakiCD))
                {
                    this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                }
                this.ChangeTorihikiKbn(false);

                // 支払情報1タブ
                this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_TORIHIKI_KBN.IsNull)
                {
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_TORIHIKI_KBN.ToString();
                }
                this.form.SHIHARAI_TAX_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_TAX_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_TAX_HASUU_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_TAX_HASUU_CD.ToString();
                }
                this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KINGAKU_HASUU_CD.IsNull)
                {
                    this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_KINGAKU_HASUU_CD.ToString();
                }
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_ZEI_KEISAN_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_ZEI_KEISAN_KBN_CD.ToString();
                }
                this.form.SHIHARAI_ZEI_KBN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_ZEI_KBN_CD.IsNull)
                {
                    this.form.SHIHARAI_ZEI_KBN_CD.Text = this.entitysM_SYS_INFO.SHIHARAI_ZEI_KBN_CD.ToString();
                }
                this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Empty;
                //160026 S
                this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.IsNull)
                {
                    this.form.SHIHARAI_BETSU_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.Value.ToString();
                }
                this.form.SHIHARAI_BETSU_KBN_TextChanged(null, null);
                this.form.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.Value.ToString();
                }
                //160026 E
                // 支払情報2タブ
                this.form.SHIHARAI_JOUHOU1.Text = string.Empty;
                this.form.SHIHARAI_JOUHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                }
                this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
                }
                this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                M_KYOTEN shiharaiKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                if (shiharaiKyoten != null)
                {
                    this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                }
                //160026 S
                this.form.FURIKOMI_EXPORT_KBN.Text = "2";
                this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                this.form.TEI_SUU_RYOU_KBN.Text = "1";
                this.form.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                this.SetDefaultValueFromFURIKOMI_BANK_MOTO();
                //160026 E
                this.ChangeSiharaiTorihikiKbn();
                this.ChangeShiharaiKyotenPrintKbn();

                // 取引先分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Checked = false;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_POST.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;

                this.form.TORIHIKISAKI_CD.Enabled = true;   // 取引先CD
                this.form.SAIBAN_BUTTON.Enabled = true;    // 採番ボタン

                // functionボタン
                if (DensisinseiOptionEnabledFlg)
                {
                    // 電子申請オプションが有効の場合
                    parentForm.bt_func1.Text = string.Empty;
                    parentForm.bt_func1.Tag = string.Empty;
                }
                parentForm.bt_func1.Enabled = false;    // 移行
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                // 業者分類タブ初期化
                this.ManiCheckOffCheck();

                this.form.GYOUSHA_ICHIRAN.DataSource = null;
                this.form.GYOUSHA_ICHIRAN.Rows.Clear();

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitNewMode", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // 画面を新規状態で初期化
                this.WindowInitNewMode(parentForm);

                // 検索結果を画面に設定
                this.SetWindowData();
                this.WindowInitNewMode(parentForm);
                this.SetDataForWindow();

                // functionボタン
                if (DensisinseiOptionEnabledFlg)
                {
                    // 電子申請オプションが有効の場合
                    parentForm.bt_func1.Text = string.Empty;
                    parentForm.bt_func1.Tag = string.Empty;
                }
                parentForm.bt_func1.Enabled = false;    // 移行
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                parentForm.bt_func12.Enabled = true;    // 閉じる

                //複写モード時
                //取引先コードのコピーはなし
                this.form.TORIHIKISAKI_CD.Text = string.Empty;

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

                //最終取引日時
                this.form.LAST_TORIHIKI_DATE.Text = string.Empty;
                this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Empty;

                // 発行先コード
                this.form.HAKKOUSAKI_CD.Text = string.Empty;

                // 業者分類タブ初期化
                this.ManiCheckOffCheck();

                // 初期フォーカス
                this.form.NYUUKINSAKI_KBN.Focus();
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
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string cd;

                cd = this.torihikisakiCD;

                entitysTORIHIKISAKI = daoTORIHIKISAKI.GetDataByCd(cd);

                entitysTORIHIKISAKI_SEIKYUU = daoTORIHIKISAKI_SEIKYUU.GetDataByCd(cd);

                entitysTOROHIKISAKI_SHIHARAI = daoTORIHIKISAKI_SHIHARAI.GetDataByCd(cd);
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

        /// <summary>
        /// データをDBから取得
        /// </summary>
        public void SetDataForWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
                header.CreateDate.Text = this.entitysTORIHIKISAKI.CREATE_DATE.ToString();
                header.CreateUser.Text = this.entitysTORIHIKISAKI.CREATE_USER;
                header.LastUpdateDate.Text = this.entitysTORIHIKISAKI.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.entitysTORIHIKISAKI.UPDATE_USER;

                // 共通情報
                this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI.Text = ConvertStrByte.ByteToString(this.entitysTORIHIKISAKI.TIME_STAMP);
                if (!this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.IsNull)
                {
                    this.form.NYUUKINSAKI_KBN.Text = this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.ToString();
                }
                if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.IsNull)
                {
                    this.entitysM_KYOTEN = this.daoIM_KYOTEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.ToString());
                    this.form.TORIHIKISAKI_KYOTEN_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD.ToString();
                    if (this.entitysM_KYOTEN != null)
                    {
                        this.form.KYOTEN_NAME_RYAKU.Text = this.entitysM_KYOTEN.KYOTEN_NAME_RYAKU;
                    }
                }
                this.form.TORIHIKISAKI_CD.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                this.form.TORIHIKISAKI_NAME1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
                this.form.TORIHIKISAKI_FURIGANA.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA;
                this.form.TORIHIKISAKI_NAME2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
                this.form.TORIHIKISAKI_TEL.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
                this.form.TORIHIKISAKI_FAX.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX;
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1;
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD;
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD))
                {
                    this.entitysM_BUSHO = this.daoIM_BUSHO.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD);
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.entitysM_BUSHO.BUSHO_NAME_RYAKU;
                }
                this.form.EIGYOU_TANTOU_CD.Text = this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD;
                if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD))
                {
                    this.entitysM_SHAIN = this.daoIM_SHAIN.GetDataByCd(this.entitysTORIHIKISAKI.EIGYOU_TANTOU_CD);
                    if (entitysM_SHAIN != null)
                    {
                        this.form.EIGYOU_TANTOU_NAME.Text = this.entitysM_SHAIN.SHAIN_NAME_RYAKU;
                    }
                }
                if (!this.entitysTORIHIKISAKI.TEKIYOU_BEGIN.IsNull)
                {
                    this.form.TEKIYOU_BEGIN.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_BEGIN;
                }
                if (!this.entitysTORIHIKISAKI.TEKIYOU_END.IsNull)
                {
                    this.form.TEKIYOU_END.Value = (DateTime)this.entitysTORIHIKISAKI.TEKIYOU_END;
                }
                this.form.CHUUSHI_RIYUU1.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU1;
                this.form.CHUUSHI_RIYUU2.Text = this.entitysTORIHIKISAKI.CHUUSHI_RIYUU2;

                // 基本情報タブ
                this.form.TORIHIKISAKI_POST.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_POST;
                if (!this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.IsNull)
                {
                    this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Format("{0:D" + this.form.TORIHIKISAKI_TODOUFUKEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString()));
                    this.entitysM_TODOUFUKEN = this.daoIM_TODOUFUKEN.GetDataByCd(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD.ToString());
                    if (this.entitysM_TODOUFUKEN != null)
                    {
                        this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = this.entitysM_TODOUFUKEN.TODOUFUKEN_NAME;
                    }
                }
                this.form.TORIHIKISAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
                this.form.TORIHIKISAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
                this.form.BUSHO.Text = this.entitysTORIHIKISAKI.BUSHO;
                this.form.TANTOUSHA.Text = this.entitysTORIHIKISAKI.TANTOUSHA;
                this.form.SHUUKEI_ITEM_CD.Text = this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD;
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD))
                {
                    this.entitysM_SHUUKEI_KOUMOKU = this.daoIM_SHUUKEI_KOUMOKU.GetDataByCd(this.entitysTORIHIKISAKI.SHUUKEI_ITEM_CD);
                    if (this.entitysM_SHUUKEI_KOUMOKU != null)
                    {
                        this.form.SHUUKEI_KOUMOKU_NAME.Text = this.entitysM_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU;
                    }
                }
                this.form.GYOUSHU_CD.Text = this.entitysTORIHIKISAKI.GYOUSHU_CD;
                if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI.GYOUSHU_CD))
                {
                    this.entitysM_GYOUSHU = this.daoIM_GYOUSHU.GetDataByCd(this.entitysTORIHIKISAKI.GYOUSHU_CD);
                    if (this.entitysM_GYOUSHU != null)
                    {
                        this.form.GYOUSHU_NAME.Text = this.entitysM_GYOUSHU.GYOUSHU_NAME_RYAKU;
                    }
                }
                this.form.DAIHYOU_PRINT_KBN.Text = string.Empty;
                if (!this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.IsNull)
                {
                    this.form.DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI.DAIHYOU_PRINT_KBN.ToString();
                }
                this.form.BIKOU1.Text = this.entitysTORIHIKISAKI.BIKOU1;
                this.form.BIKOU2.Text = this.entitysTORIHIKISAKI.BIKOU2;
                this.form.BIKOU3.Text = this.entitysTORIHIKISAKI.BIKOU3;
                this.form.BIKOU4.Text = this.entitysTORIHIKISAKI.BIKOU4;
                this.form.SHOKUCHI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.SHOKUCHI_KBN;

                //請求情報1タブ
                if (this.entitysTORIHIKISAKI_SEIKYUU != null)
                {
                    this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SEIKYUU.Text = ConvertStrByte.ByteToString(this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP);
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.IsNull)
                    {
                        this.form.TORIHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.TORIHIKI_KBN.Text = "";
                    }
                    this.ChangeTorihikiKbn(false);

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.IsNull)
                    {
                        this.form.SHIMEBI1.Text = "";
                        this.form.SHIMEBI2.Enabled = false;
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    else
                    {
                        this.form.SHIMEBI1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI1.ToString();
                        this.form.SHIMEBI2.Enabled = true;
                    }

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.IsNull)
                    {
                        this.form.SHIMEBI2.Text = "";
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    else
                    {
                        this.form.SHIMEBI2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI2.ToString();
                        this.form.SHIMEBI3.Enabled = true;
                    }

                    if (this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.IsNull)
                    {
                        this.form.SHIMEBI3.Text = "";
                    }
                    else
                    {
                        this.form.SHIMEBI3.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHIMEBI3.ToString();
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.IsNull)
                    {
                        this.form.HICCHAKUBI.Text = this.entitysTORIHIKISAKI_SEIKYUU.HICCHAKUBI.ToString();
                    }
                    else
                    {
                        this.form.HICCHAKUBI.Text = "";
                    }
                    //#160026 S
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN.IsNull)
                    {
                        this.form.KAISHUU_BETSU_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_BETSU_KBN.Text = "";
                    }
                    this.form.KAISHUU_BETSU_KBN_TextChanged(null, null);
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO.IsNull)
                    {
                        this.form.KAISHUU_BETSU_NICHIGO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_BETSU_NICHIGO.Text = "";
                    }
                    //#160026 E
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.IsNull)
                    {
                        this.form.KAISHUU_MONTH.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_MONTH.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_MONTH.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.IsNull)
                    {
                        this.form.KAISHUU_DAY.Text = this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_DAY.ToString();
                    }
                    else
                    {
                        this.form.KAISHUU_DAY.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.IsNull)
                    {
                        this.form.KAISHUU_HOUHOU.Text = string.Format("{0:D" + this.form.KAISHUU_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU.ToString()));
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.KAISHUU_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        this.form.KAISHUU_HOUHOU.Text = "";
                        this.form.KAISHUU_HOUHOU_NAME.Text = "";
                    }

                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.torihikisakiCD))
                    {
                        this.form.KAISHI_URIKAKE_ZANDAKA.Text = "0";
                    }
                    else if (!this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.IsNull)
                    {
                        this.SetZandakaFormat(this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA.ToString(), this.form.KAISHI_URIKAKE_ZANDAKA);
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHOSHIKI_KBN.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = "";
                    }

                    if (!this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.IsNull)
                    {
                        this.form.TAX_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.TAX_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.TAX_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.IsNull)
                    {
                        this.form.KINGAKU_HASUU_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.KINGAKU_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.KINGAKU_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KEITAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKIN_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.IsNull)
                    {
                        this.form.YOUSHI_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.YOUSHI_KBN.ToString();
                    }
                    else
                    {
                        this.form.YOUSHI_KBN.Text = "";
                    }
                    // 出力区分
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.IsNull)
                    {
                        this.form.OUTPUT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.OUTPUT_KBN.ToString();
                    }
                    else
                    {
                        this.form.OUTPUT_KBN.Text = "";
                    }
                    // 発行先コード
                    this.form.HAKKOUSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.IsNull)
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KEISAN_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = "";
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.IsNull)
                    {
                        this.form.ZEI_KBN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.ZEI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.ZEI_KBN_CD.Text = "";
                    }

                    // 請求情報2タブ
                    this.form.SEIKYUU_JOUHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU1;
                    this.form.SEIKYUU_JOUHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_JOUHOU2;
                    this.form.SEIKYUU_SOUFU_NAME1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME1;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_NAME2;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2;
                    this.form.SEIKYUU_SOUFU_POST.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_POST;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS1;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_ADDRESS2;
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_BUSHO;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TANTOU;
                    this.form.SEIKYUU_SOUFU_TEL.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_TEL;
                    this.form.SEIKYUU_SOUFU_FAX.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_FAX;
                    if (this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN.ToString().Equals("1"))
                    {
                        this.form.NYUUKINSAKI_CD.Enabled = false;
                    }
                    this.form.NYUUKINSAKI_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD;
                    if (!string.IsNullOrEmpty(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD))
                    {
                        M_NYUUKINSAKI nyu = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>().GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD);
                        if (nyu != null)
                        {
                            this.form.NYUUKINSAKI_NAME1.Text = nyu.NYUUKINSAKI_NAME1;
                            this.form.NYUUKINSAKI_NAME2.Text = nyu.NYUUKINSAKI_NAME2;
                        }
                    }
                    this.form.SEIKYUU_TANTOU.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_TANTOU;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                    }
                    this.ChangeSeikyuuKyotenPrintKbn();
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_KYOTEN_CD.ToString()));
                        M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                    this.form.FURIKOMI_NAME1.Text = entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME1;
                    this.form.FURIKOMI_NAME2.Text = entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_NAME2;

                    //振込銀行タブ
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                    {
                        this.form.FURIKOMI_BANK_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD))
                    {
                        this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD);
                        if (this.entitysM_BANK != null)
                        {
                            this.form.FURIKOMI_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                    {
                        this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
                        this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏
                        this.form.KOUZA_SHURUI.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                        this.form.KOUZA_NO.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                        this.form.KOUZA_NAME.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD))
                    {
                        M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD;
                        bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD;
                        bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI;
                        bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO;
                        this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                        if (this.entitysM_BANK_SHITEN != null)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        }
                    }
                    if (!this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE.IsNull)
                    {
                        this.form.LAST_TORIHIKI_DATE.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTORIHIKISAKI_SEIKYUU.LAST_TORIHIKI_DATE);
                    }
                    //振込銀行2
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                    {
                        this.form.FURIKOMI_BANK_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2))
                    {
                        this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2);
                        if (this.entitysM_BANK != null)
                        {
                            this.form.FURIKOMI_BANK_NAME_2.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                    {
                        this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                        this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                        this.form.KOUZA_SHURUI_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                        this.form.KOUZA_NO_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                        this.form.KOUZA_NAME_2.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME_2;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2))
                    {
                        M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2;
                        bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2;
                        bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2;
                        bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_2;
                        this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                        if (this.entitysM_BANK_SHITEN != null)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        }
                    }
                    //振込銀行3
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                    {
                        this.form.FURIKOMI_BANK_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3))
                    {
                        this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3);
                        if (this.entitysM_BANK != null)
                        {
                            this.form.FURIKOMI_BANK_NAME_3.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                    {
                        this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                        this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                        this.form.KOUZA_SHURUI_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                        this.form.KOUZA_NO_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                        this.form.KOUZA_NAME_3.Text = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NAME_3;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3) && !string.IsNullOrWhiteSpace(this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3))
                    {
                        M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3;
                        bankShiten.BANK_SHITEN_CD = this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3;
                        bankShiten.KOUZA_SHURUI = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3;
                        bankShiten.KOUZA_NO = this.entitysTORIHIKISAKI_SEIKYUU.KOUZA_NO_3;
                        this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                        if (this.entitysM_BANK_SHITEN != null)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                        }
                    }
                }

                //支払情報1タブ
                if (this.entitysTOROHIKISAKI_SHIHARAI != null)
                {
                    this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SHIHARAI.Text = ConvertStrByte.ByteToString(this.entitysTOROHIKISAKI_SHIHARAI.TIME_STAMP);
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.TORIHIKI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_TORIHIKI_KBN_CD.Text = "";
                    }
                    this.ChangeSiharaiTorihikiKbn();

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI1.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI1.Text = "";
                        this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI1.ToString();
                    }

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI2.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI2.Text = "";
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI2.ToString();
                    }

                    if (this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI3.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI3.Text = "";
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI3.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIMEBI3.ToString();
                    }
                    //160026 S
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN.IsNull)
                    {
                        this.form.SHIHARAI_BETSU_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_BETSU_KBN.Text = "";
                    }
                    this.form.SHIHARAI_BETSU_KBN_TextChanged(null, null);
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO.IsNull)
                    {
                        this.form.SHIHARAI_BETSU_NICHIGO.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_BETSU_NICHIGO.Text = "";
                    }
                    //160026 E
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH.IsNull)
                    {
                        this.form.SHIHARAI_MONTH.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_MONTH.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_MONTH.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_DAY.IsNull)
                    {
                        this.form.SHIHARAI_DAY.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_DAY.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_DAY.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.form.SHIHARAI_HOUHOU.Text = string.Format("{0:D" + this.form.SHIHARAI_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU.ToString()));
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.SHIHARAI_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    else
                    {
                        this.form.SHIHARAI_HOUHOU.Text = "";
                        this.form.SHIHARAI_HOUHOU_NAME.Text = "";
                    }
                    if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.torihikisakiCD))
                    {
                        this.form.KAISHI_KAIKAKE_ZANDAKA.Text = "0";
                    }
                    else if (!this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                    {
                        this.SetZandakaFormat(this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA.ToString(), this.form.KAISHI_KAIKAKE_ZANDAKA);
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.TAX_HASUU_CD.IsNull)
                    {
                        this.form.SHIHARAI_TAX_HASUU_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.TAX_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_TAX_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.IsNull)
                    {
                        this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.KINGAKU_HASUU_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KEITAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHUKKIN_MEISAI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.YOUSHI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.YOUSHI_KBN.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KBN_CD.IsNull)
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KBN_CD.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD.Text = "";
                    }
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE.IsNull)
                    {
                        this.form.LAST_TORIHIKI_DATE_SHIHARAI.Text = string.Format("yyyy/MM/dd HH:mm:ss", this.entitysTOROHIKISAKI_SHIHARAI.LAST_TORIHIKI_DATE);
                    }

                    // 支払情報2タブ
                    this.form.SHIHARAI_JOUHOU1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU1;
                    this.form.SHIHARAI_JOUHOU2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_JOUHOU2;
                    this.form.SHIHARAI_SOUFU_NAME1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME1;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_NAME2;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2;
                    this.form.SHIHARAI_SOUFU_POST.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_POST;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS1;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_ADDRESS2;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_BUSHO;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TANTOU;
                    this.form.SHIHARAI_SOUFU_TEL.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_TEL;
                    this.form.SHIHARAI_SOUFU_FAX.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_FAX;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;

                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                    }
                    //160026 S
                    //振込先銀行
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN.IsNull)
                    {
                        this.form.FURIKOMI_EXPORT_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN.Value.ToString();
                    }
                    this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                    this.form.FURIKOMI_SAKI_BANK_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_CD;
                    this.form.FURIKOMI_SAKI_BANK_NAME.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_NAME;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_CD;
                    this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_NAME;
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.IsNull)
                    {
                        this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Value.ToString();
                        this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI;
                    }
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NO;
                    this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NAME;
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN.IsNull)
                    {
                        this.form.TEI_SUU_RYOU_KBN.Text = this.entitysTOROHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN.Value.ToString();
                    }
                    //振込元銀行                
                    if (!string.IsNullOrWhiteSpace(this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD))
                    {
                        this.form.FURI_KOMI_MOTO_BANK_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD;
                        this.entitysM_BANK = this.daoIM_BANK.GetDataByCd(this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD);
                        if (this.entitysM_BANK != null)
                        {
                            this.form.FURI_KOMI_MOTO_BANK_NAME.Text = this.entitysM_BANK.BANK_NAME_RYAKU;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD))
                    {
                        this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                        this.form.FURI_KOMI_MOTO_SHURUI.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI;
                        this.form.FURI_KOMI_MOTO_NO.Text = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO;
                    }
                    if (!string.IsNullOrWhiteSpace(this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD) && !string.IsNullOrWhiteSpace(this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD))
                    {
                        M_BANK_SHITEN bankShiten = new M_BANK_SHITEN();
                        bankShiten.BANK_CD = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD;
                        bankShiten.BANK_SHITEN_CD = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD;
                        bankShiten.KOUZA_SHURUI = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI;
                        bankShiten.KOUZA_NO = this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO;
                        this.entitysM_BANK_SHITEN = this.daoIM_BANK_SHITEN.GetDataByCd(bankShiten);
                        if (this.entitysM_BANK_SHITEN != null)
                        {
                            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = this.entitysM_BANK_SHITEN.BANK_SHIETN_NAME_RYAKU;
                            this.form.FURI_KOMI_MOTO_NAME.Text = this.entitysM_BANK_SHITEN.KOUZA_NAME;
                        }
                    }
                    //160026 E
                    this.ChangeShiharaiKyotenPrintKbn();

                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_KYOTEN_CD.ToString()));
                        M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null)
                        {
                            this.form.SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
                        }
                    }
                }

                // 取引先分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Checked = (bool)this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KBN;

                if (this.form.MANI_HENSOUSAKI_KBN.Checked)
                {
                
                    if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                        if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value == 1)
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                        }
                        else if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value == 2)
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                        }
                    }

                    if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                    {
                        this.form.MANI_HENSOUSAKI_NAME1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1;
                        this.form.MANI_HENSOUSAKI_NAME2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2;
                        this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1;
                        this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2;
                        this.form.MANI_HENSOUSAKI_POST.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST;
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2;
                        this.form.MANI_HENSOUSAKI_BUSHO.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO;
                        this.form.MANI_HENSOUSAKI_TANTOU.Text = this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU;
                    }

                    this.rowCntGyousha = this.SearchGyousha();

                    if (this.rowCntGyousha > 0)
                    {
                        this.SetIchiranGyousha();
                    }

                }

                else
                { 

                    if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.IsNull)
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "";
                    }
                    else
                    {
                        this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = Convert.ToString(this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value);
                        if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value == 1)
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Checked = true;
                        }
                        else if (this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Value == 2)
                        {
                            this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked = true;
                        }
                    }

                    this.ManiCheckOffCheck();

                    this.rowCntGyousha = this.SearchGyousha();

                    if (this.rowCntGyousha > 0)
                    {
                        this.SetIchiranGyousha();
                    }


                } 

            } 
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForWindow", ex);
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
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                // 共通項目
                this.form.NYUUKINSAKI_KBN.ReadOnly = isBool;
                this.form.NYUUKINSAKI_KBN_1.Enabled = !isBool;
                this.form.NYUUKINSAKI_KBN_2.Enabled = !isBool;
                this.form.TORIHIKISAKI_KYOTEN_CD.Enabled = !isBool;
                this.form.KYOTEN_SEARCH_BUTTON.Enabled = !isBool;
                this.form.TORIHIKISAKI_CD.Enabled = !isBool;
                this.form.SAIBAN_BUTTON.Enabled = !isBool;
                this.form.TORIHIKISAKI_FURIGANA.ReadOnly = isBool;
                this.form.TORIHIKISAKI_NAME1.ReadOnly = isBool;
                this.form.TORIHIKISAKI_KEISHOU1.Enabled = !isBool;
                this.form.TORIHIKISAKI_NAME2.ReadOnly = isBool;
                this.form.TORIHIKISAKI_KEISHOU2.Enabled = !isBool;
                this.form.TORIHIKISAKI_NAME_RYAKU.ReadOnly = isBool;
                this.form.TORIHIKISAKI_TEL.ReadOnly = isBool;
                this.form.TORIHIKISAKI_FAX.ReadOnly = isBool;
                this.form.EIGYOU_TANTOU_BUSHO_CD.Enabled = !isBool;
                this.form.ENGYOU_TANTOUBUSHO_SEARCH_BUTTON.Enabled = !isBool;
                this.form.EIGYOU_TANTOU_CD.Enabled = !isBool;
                this.form.EIGYOU_TANTOU_SEARCH_BUTTON.Enabled = !isBool;
                this.form.TEKIYOU_BEGIN.Enabled = !isBool;
                this.form.TEKIYOU_END.Enabled = !isBool;
                this.form.CHUUSHI_RIYUU1.ReadOnly = isBool;
                this.form.CHUUSHI_RIYUU2.ReadOnly = isBool;

                //基本情報タブ
                this.form.TORIHIKISAKI_POST.Enabled = !isBool;
                this.form.TORIHIKISAKI_ADDRESS_SEARCH.Enabled = !isBool;
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Enabled = !isBool;
                this.form.TORIHIKISAKI_POST_SERACH.Enabled = !isBool;
                this.form.TORIHIKISAKI_ADDRESS1.ReadOnly = isBool;
                this.form.TORIHIKISAKI_ADDRESS2.ReadOnly = isBool;
                this.form.BUSHO.ReadOnly = isBool;
                this.form.TANTOUSHA.ReadOnly = isBool;
                this.form.SHUUKEI_ITEM_CD.Enabled = !isBool;
                this.form.SHUUKEI_KOUMOKU_SEARCH.Enabled = !isBool;
                this.form.GYOUSHU_CD.Enabled = !isBool;
                this.form.GYOUSHU_SEARCH.Enabled = !isBool;
                this.form.BIKOU1.ReadOnly = isBool;
                this.form.BIKOU2.ReadOnly = isBool;
                this.form.BIKOU3.ReadOnly = isBool;
                this.form.BIKOU4.ReadOnly = isBool;
                this.form.SHOKUCHI_KBN.Enabled = !isBool;
                this.form.TODOUFUKEN_SEARCH_BUTTON.Enabled = !isBool;
                this.form.DAIHYOU_PRINT_KBN.Enabled = !isBool;
                this.form.DAIHYOU_PRINT_KBN_1.Enabled = !isBool;
                this.form.DAIHYOU_PRINT_KBN_2.Enabled = !isBool;

                //請求情報1タブ
                this.form.TORIHIKI_KBN.ReadOnly = isBool;
                this.form.TORIHIKI_KBN_1.Enabled = !isBool;
                this.form.TORIHIKI_KBN_2.Enabled = !isBool;
                this.form.SHIMEBI1.Enabled = !isBool;
                this.form.SHIMEBI2.Enabled = !isBool;
                this.form.SHIMEBI3.Enabled = !isBool;
                this.form.HICCHAKUBI.ReadOnly = isBool;
                //160026 S
                this.form.KAISHUU_BETSU_KBN.ReadOnly = isBool;
                this.form.KAISHUU_BETSU_KBN_1.Enabled = !isBool;
                this.form.KAISHUU_BETSU_KBN_2.Enabled = !isBool;
                this.form.KAISHUU_BETSU_NICHIGO.Enabled = !isBool;
                //160026 E

                this.form.KAISHUU_MONTH.ReadOnly = isBool;
                this.form.KAISHUU_MONTH_1.Enabled = !isBool;
                this.form.KAISHUU_MONTH_2.Enabled = !isBool;
                this.form.KAISHUU_MONTH_3.Enabled = !isBool;
                this.form.KAISHUU_MONTH_4.Enabled = !isBool;
                this.form.KAISHUU_MONTH_5.Enabled = !isBool;
                this.form.KAISHUU_MONTH_6.Enabled = !isBool;
                this.form.KAISHUU_MONTH_7.Enabled = !isBool;
                this.form.KAISHUU_DAY.ReadOnly = isBool;
                this.form.KAISHUU_HOUHOU.Enabled = !isBool;
                this.form.KAISHUU_HOUHOU_SEARCH.Enabled = !isBool;
                this.form.KAISHI_URIKAKE_ZANDAKA.ReadOnly = isBool;
                this.form.SHOSHIKI_KBN.ReadOnly = isBool;
                this.form.SHOSHIKI_KBN_1.Enabled = !isBool;
                this.form.SHOSHIKI_KBN_2.Enabled = !isBool;
                this.form.SHOSHIKI_KBN_3.Enabled = !isBool;
                this.form.SHOSHIKI_MEISAI_KBN.ReadOnly = isBool;
                this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = !isBool;
                this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = !isBool;
                this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = !isBool;
                this.form.TAX_HASUU_CD.ReadOnly = isBool;
                this.form.TAX_HASUU_CD_1.Enabled = !isBool;
                this.form.TAX_HASUU_CD_2.Enabled = !isBool;
                this.form.TAX_HASUU_CD_3.Enabled = !isBool;
                this.form.KINGAKU_HASUU_CD.ReadOnly = isBool;
                this.form.KINGAKU_HASUU_CD_1.Enabled = !isBool;
                this.form.KINGAKU_HASUU_CD_2.Enabled = !isBool;
                this.form.KINGAKU_HASUU_CD_3.Enabled = !isBool;
                this.form.SEIKYUU_KEITAI_KBN.ReadOnly = isBool;
                this.form.SEIKYUU_KEITAI_KBN_1.Enabled = !isBool;
                this.form.SEIKYUU_KEITAI_KBN_2.Enabled = !isBool;
                this.form.NYUUKIN_MEISAI_KBN.ReadOnly = isBool;
                this.form.NYUUKIN_MEISAI_KBN_1.Enabled = !isBool;
                this.form.NYUUKIN_MEISAI_KBN_2.Enabled = !isBool;
                this.form.YOUSHI_KBN.ReadOnly = isBool;
                this.form.YOUSHI_KBN_1.Enabled = !isBool;
                this.form.YOUSHI_KBN_2.Enabled = !isBool;
                this.form.YOUSHI_KBN_3.Enabled = !isBool;
                this.form.OUTPUT_KBN.ReadOnly = isBool;
                this.form.OUTPUT_KBN_1.Enabled = !isBool;
                this.form.OUTPUT_KBN_2.Enabled = !isBool;
                this.form.HAKKOUSAKI_CD.ReadOnly = isBool;
                this.form.ZEI_KEISAN_KBN_CD.ReadOnly = isBool;
                this.form.ZEI_KEISAN_KBN_CD_1.Enabled = !isBool;
                this.form.ZEI_KEISAN_KBN_CD_2.Enabled = !isBool;
                this.form.ZEI_KEISAN_KBN_CD_3.Enabled = !isBool;
                this.form.ZEI_KBN_CD.ReadOnly = isBool;
                this.form.ZEI_KBN_CD_1.Enabled = !isBool;
                this.form.ZEI_KBN_CD_2.Enabled = !isBool;
                this.form.ZEI_KBN_CD_3.Enabled = !isBool;

                // 請求情報2タブ
                this.form.SEIKYUU_JOUHOU1.ReadOnly = isBool;
                this.form.SEIKYUU_JOUHOU2.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_NAME1.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = !isBool;
                this.form.SEIKYUU_SOUFU_NAME2.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = !isBool;
                this.form.SEIKYUU_SOUFU_POST.Enabled = !isBool;
                this.form.SEIKYUU_JUSHO_SEARCH.Enabled = !isBool;
                this.form.SEIKYUU_POST_SEARCH.Enabled = !isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS1.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_ADDRESS2.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_BUSHO.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_TANTOU.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_TEL.ReadOnly = isBool;
                this.form.SEIKYUU_SOUFU_FAX.ReadOnly = isBool;
                this.form.NYUUKINSAKI_CD.Enabled = !isBool;
                this.form.NYUUKINSAKI_SEARCH.Enabled = !isBool;
                this.form.SEIKYUU_TANTOU.ReadOnly = isBool;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN.ReadOnly = isBool;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = !isBool;
                this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = !isBool;
                this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = !isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = !isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = !isBool;
                this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = !isBool;
                this.form.SEIKYUU_KYOTEN_CD.Enabled = !isBool;
                this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = !isBool;
                this.form.FURIKOMI_NAME1.ReadOnly = isBool;
                this.form.FURIKOMI_NAME2.ReadOnly = isBool;

                // 振込銀行タブ
                this.form.FURIKOMI_BANK_CD.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SEARCH.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = !isBool;
                this.form.FURIKOMI_BANK_CD_2.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SEARCH_2.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = !isBool;
                this.form.FURIKOMI_BANK_CD_3.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SEARCH_3.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = !isBool;
                this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = !isBool;

                //支払情報1タブ
                this.form.SHIHARAI_TORIHIKI_KBN_CD.ReadOnly = isBool;
                this.form.SHIHARAI_TORIHIKI_KBN_CD_1.Enabled = !isBool;
                this.form.SHIHARAI_TORIHIKI_KBN_CD_2.Enabled = !isBool;
                this.form.SHIHARAI_SHIMEBI1.Enabled = !isBool;
                this.form.SHIHARAI_SHIMEBI2.Enabled = !isBool;
                this.form.SHIHARAI_SHIMEBI3.Enabled = !isBool;
                //160026 S
                this.form.SHIHARAI_BETSU_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_BETSU_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_BETSU_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_BETSU_NICHIGO.Enabled = !isBool;
                //160026 E

                this.form.SHIHARAI_MONTH.ReadOnly = isBool;
                this.form.SHIHARAI_MONTH_1.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_2.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_3.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_4.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_5.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_6.Enabled = !isBool;
                this.form.SHIHARAI_MONTH_7.Enabled = !isBool;
                this.form.SHIHARAI_DAY.ReadOnly = isBool;
                this.form.SHIHARAI_HOUHOU.Enabled = !isBool;
                this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = !isBool;
                this.form.KAISHI_KAIKAKE_ZANDAKA.ReadOnly = isBool;
                this.form.SHIHARAI_SHOSHIKI_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = !isBool;
                this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = !isBool;
                this.form.SHIHARAI_TAX_HASUU_CD.ReadOnly = isBool;
                this.form.SHIHARAI_TAX_HASUU_CD_1.Enabled = !isBool;
                this.form.SHIHARAI_TAX_HASUU_CD_2.Enabled = !isBool;
                this.form.SHIHARAI_TAX_HASUU_CD_3.Enabled = !isBool;
                this.form.SHIHARAI_KINGAKU_HASUU_CD.ReadOnly = isBool;
                this.form.SHIHARAI_KINGAKU_HASUU_CD_1.Enabled = !isBool;
                this.form.SHIHARAI_KINGAKU_HASUU_CD_2.Enabled = !isBool;
                this.form.SHIHARAI_KINGAKU_HASUU_CD_3.Enabled = !isBool;
                this.form.SHIHARAI_KEITAI_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_KEITAI_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_KEITAI_KBN_2.Enabled = !isBool;
                this.form.SHUKKIN_MEISAI_KBN.ReadOnly = isBool;
                this.form.SHUKKIN_MEISAI_KBN_1.Enabled = !isBool;
                this.form.SHUKKIN_MEISAI_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_YOUSHI_KBN.ReadOnly = isBool;
                this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.ReadOnly = isBool;
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KBN_CD.ReadOnly = isBool;
                this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = !isBool;
                this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = !isBool;

                // 支払情報2タブ
                this.form.SHIHARAI_JOUHOU1.ReadOnly = isBool;
                this.form.SHIHARAI_JOUHOU2.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_NAME1.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = !isBool;
                this.form.SHIHARAI_SOUFU_NAME2.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = !isBool;
                this.form.SHIHARAI_SOUFU_POST.Enabled = !isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = !isBool;
                this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = !isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS1.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_ADDRESS2.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_BUSHO.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_TANTOU.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_TEL.ReadOnly = isBool;
                this.form.SHIHARAI_SOUFU_FAX.ReadOnly = isBool;
                this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = !isBool;
                this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = !isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = !isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = !isBool;
                this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = !isBool;
                this.form.SHIHARAI_KYOTEN_CD.Enabled = !isBool;
                this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = !isBool;
                //160026 S
                this.form.FURIKOMI_EXPORT_KBN.Enabled = !isBool;
                this.form.FURIKOMI_EXPORT_KBN_1.Enabled = !isBool;
                this.form.FURIKOMI_EXPORT_KBN_2.Enabled = !isBool;
                this.form.FURIKOMI_SAKI_BANK_CD.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_NAME.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.ReadOnly = isBool;
                this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.ReadOnly = isBool;
                this.form.TEI_SUU_RYOU_KBN.Enabled = !isBool;
                this.form.TEI_SUU_RYOU_KBN_1.Enabled = !isBool;
                this.form.TEI_SUU_RYOU_KBN_2.Enabled = !isBool;
                this.form.FURI_KOMI_MOTO_BANK_CD.ReadOnly = isBool;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.ReadOnly = isBool;
                this.form.FURI_KOMI_MOTO_BANK_POPUP.Enabled = !isBool;
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = !isBool;
                //160026 E
                // 取引先分類タブ
                this.form.MANI_HENSOUSAKI_KBN.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_NAME1.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_NAME2.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_POST.ReadOnly = isBool;
                this.form.MANI_HENSHOUSAKI_ADDRESS_SEARCH.Enabled = !isBool;
                this.form.MANI_HENSHOUSAKI_POST_SEARCH.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS1.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_ADDRESS2.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_BUSHO.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_TANTOU.ReadOnly = isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_1.Enabled = !isBool;
                this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Enabled = !isBool;
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

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        ///
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int count = 1;

                //count = table.Rows.Count;

                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 最新のSYS_INFOを取得
                M_SYS_INFO[] sysInfo = this.daoIM_SYS_INFO.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.entitysM_SYS_INFO = sysInfo[0];
                }
                else
                {
                    this.entitysM_SYS_INFO = null;
                }

                // 引合取引先マスタ
                this.entitysTORIHIKISAKI = new M_HIKIAI_TORIHIKISAKI();
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_CD);
                this.form.TORIHIKISAKI_KYOTEN_CD.Text = "99";                           // 強制的に99:全社を登録
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_KYOTEN_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME1);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME2);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_NAME_RYAKU);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_FURIGANA);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_TEL);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_FAX);
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU1 = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.entitysTORIHIKISAKI.TORIHIKISAKI_KEISHOU2 = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.entitysTORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                this.entitysTORIHIKISAKI.SetValue(this.form.EIGYOU_TANTOU_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_POST);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_TODOUFUKEN_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_ADDRESS1);
                this.entitysTORIHIKISAKI.SetValue(this.form.TORIHIKISAKI_ADDRESS2);
                this.entitysTORIHIKISAKI.SetValue(this.form.CHUUSHI_RIYUU1);
                this.entitysTORIHIKISAKI.SetValue(this.form.CHUUSHI_RIYUU2);
                this.entitysTORIHIKISAKI.SetValue(this.form.BUSHO);
                this.entitysTORIHIKISAKI.SetValue(this.form.TANTOUSHA);
                this.entitysTORIHIKISAKI.SetValue(this.form.SHUUKEI_ITEM_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.GYOUSHU_CD);
                this.entitysTORIHIKISAKI.SetValue(this.form.DAIHYOU_PRINT_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU1);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU2);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU3);
                this.entitysTORIHIKISAKI.SetValue(this.form.BIKOU4);
                this.entitysTORIHIKISAKI.SetValue(this.form.NYUUKINSAKI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.SHOKUCHI_KBN);
                this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN);
                if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN_2.Checked)
                {
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_NAME1);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_NAME2);
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.form.MANI_HENSOUSAKI_KEISHOU1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.form.MANI_HENSOUSAKI_KEISHOU2.Text;
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_POST);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_ADDRESS1);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_ADDRESS2);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_BUSHO);
                    this.entitysTORIHIKISAKI.SetValue(this.form.MANI_HENSOUSAKI_TANTOU);
                }
                else
                {
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME1 = this.form.TORIHIKISAKI_NAME1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_NAME2 = this.form.TORIHIKISAKI_NAME2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU1 = this.form.TORIHIKISAKI_KEISHOU1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_KEISHOU2 = this.form.TORIHIKISAKI_KEISHOU2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_POST = this.form.TORIHIKISAKI_POST.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS1 = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_ADDRESS2 = this.form.TORIHIKISAKI_ADDRESS2.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_BUSHO = this.form.BUSHO.Text;
                    this.entitysTORIHIKISAKI.MANI_HENSOUSAKI_TANTOU = this.form.TANTOUSHA.Text;
                }
                this.entitysTORIHIKISAKI.TEKIYOU_BEGIN = (DateTime)this.form.TEKIYOU_BEGIN.Value;
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
                {
                    this.entitysTORIHIKISAKI.TEKIYOU_END = (DateTime)this.form.TEKIYOU_END.Value;
                }
                this.entitysTORIHIKISAKI.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI.Text);

                // 引合取引先　請求情報マスタ
                this.entitysTORIHIKISAKI_SEIKYUU = new M_HIKIAI_TORIHIKISAKI_SEIKYUU();
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TORIHIKISAKI_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TORIHIKI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHIMEBI3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.HICCHAKUBI);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_MONTH);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_DAY);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KAISHUU_HOUHOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_JOUHOU1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_JOUHOU2);
                //160026 S
                if (!string.IsNullOrWhiteSpace(this.form.KAISHUU_BETSU_KBN.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_KBN = Convert.ToInt16(this.form.KAISHUU_BETSU_KBN.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.KAISHUU_BETSU_NICHIGO.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHUU_BETSU_NICHIGO = Convert.ToInt16(this.form.KAISHUU_BETSU_NICHIGO.Text);
                }
                //160026 E

                if (string.IsNullOrWhiteSpace(this.form.KAISHI_URIKAKE_ZANDAKA.Text))
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = 0;
                }
                else
                {
                    this.entitysTORIHIKISAKI_SEIKYUU.KAISHI_URIKAKE_ZANDAKA = Decimal.Parse(this.form.KAISHI_URIKAKE_ZANDAKA.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowLeadingSign);
                }
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHOSHIKI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SHOSHIKI_MEISAI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KEITAI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.NYUUKIN_MEISAI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.YOUSHI_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.OUTPUT_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.HAKKOUSAKI_CD = this.form.HAKKOUSAKI_CD.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.ZEI_KEISAN_KBN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.ZEI_KBN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.TAX_HASUU_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KINGAKU_HASUU_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = this.form.FURIKOMI_BANK_CD.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME);
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2 = this.form.FURIKOMI_BANK_CD_2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI_2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO_2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME_2);
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3 = this.form.FURIKOMI_BANK_CD_3.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_SHURUI_3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NO_3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.KOUZA_NAME_3);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_NAME1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_NAME2);
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU1 = this.form.SEIKYUU_SOUFU_KEISHOU1.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SEIKYUU_SOUFU_KEISHOU2 = this.form.SEIKYUU_SOUFU_KEISHOU2.Text;
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_POST);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_ADDRESS1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_ADDRESS2);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_BUSHO);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_TANTOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_TEL);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_SOUFU_FAX);
                // 20140717 ria EV005304 入金先を「１．自社」とし登録後、修正モードで開くと取引先CDと同じ入金先CDがセットされている。 start
                //if (this.form.NYUUKINSAKI_KBN.Text.Equals("1"))
                //{
                //    this.entitysTORIHIKISAKI_SEIKYUU.NYUUKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                //}
                //else
                //{
                //    this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.NYUUKINSAKI_CD);
                //}
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.NYUUKINSAKI_CD);
                // 20140717 ria EV005304 入金先を「１．自社」とし登録後、修正モードで開くと取引先CDと同じ入金先CDがセットされている。 end
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_TANTOU);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_DAIHYOU_PRINT_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KYOTEN_PRINT_KBN);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.SEIKYUU_KYOTEN_CD);
                this.entitysTORIHIKISAKI_SEIKYUU.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SEIKYUU.Text);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.FURIKOMI_NAME1);
                this.entitysTORIHIKISAKI_SEIKYUU.SetValue(this.form.FURIKOMI_NAME2);

                // 引合取引先　支払情報マスタ
                this.entitysTOROHIKISAKI_SHIHARAI = new M_HIKIAI_TORIHIKISAKI_SHIHARAI();
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.TORIHIKISAKI_CD);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_TORIHIKI_KBN_CD);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI1);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI2);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHIMEBI3);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_MONTH);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_DAY);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_HOUHOU);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_JOUHOU1);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_JOUHOU2);
                //160026 S
                if (!string.IsNullOrWhiteSpace(this.form.SHIHARAI_BETSU_KBN.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_KBN = Convert.ToInt16(this.form.SHIHARAI_BETSU_KBN.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.SHIHARAI_BETSU_NICHIGO.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_BETSU_NICHIGO = Convert.ToInt16(this.form.SHIHARAI_BETSU_NICHIGO.Text);
                }
                //160026 E

                if (string.IsNullOrWhiteSpace(this.form.KAISHI_KAIKAKE_ZANDAKA.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = 0;
                }
                else
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.KAISHI_KAIKAKE_ZANDAKA = Decimal.Parse(this.form.KAISHI_KAIKAKE_ZANDAKA.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowCurrencySymbol | System.Globalization.NumberStyles.AllowLeadingSign);
                }
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHOSHIKI_KBN);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KEITAI_KBN);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHUKKIN_MEISAI_KBN);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_YOUSHI_KBN);
                if (!string.IsNullOrEmpty(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.ZEI_KEISAN_KBN_CD = Int16.Parse(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text);
                }
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_ZEI_KBN_CD);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_TAX_HASUU_CD);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KINGAKU_HASUU_CD);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_NAME1);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_NAME2);
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU1 = this.form.SHIHARAI_SOUFU_KEISHOU1.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.SHIHARAI_SOUFU_KEISHOU2 = this.form.SHIHARAI_SOUFU_KEISHOU2.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_POST);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_ADDRESS1);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_ADDRESS2);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_BUSHO);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_TANTOU);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_TEL);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_SOUFU_FAX);
                this.entitysTOROHIKISAKI_SHIHARAI.SYUUKINSAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KYOTEN_PRINT_KBN);
                this.entitysTOROHIKISAKI_SHIHARAI.SetValue(this.form.SHIHARAI_KYOTEN_CD);
                //160026 S
                if (!string.IsNullOrEmpty(this.form.FURIKOMI_EXPORT_KBN.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_EXPORT_KBN = Int16.Parse(this.form.FURIKOMI_EXPORT_KBN.Text);
                }
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_CD = this.form.FURIKOMI_SAKI_BANK_CD.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_NAME = this.form.FURIKOMI_SAKI_BANK_NAME.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_CD = this.form.FURIKOMI_SAKI_BANK_SHITEN_CD.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_SHITEN_NAME = this.form.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text;
                if (!string.IsNullOrEmpty(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD = Int16.Parse(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text);
                    this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_SHURUI = this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text;
                }
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NO = this.form.FURIKOMI_SAKI_BANK_KOUZA_NO.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURIKOMI_SAKI_BANK_KOUZA_NAME = this.form.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text;
                if (!string.IsNullOrEmpty(this.form.TEI_SUU_RYOU_KBN.Text))
                {
                    this.entitysTOROHIKISAKI_SHIHARAI.TEI_SUU_RYOU_KBN = Int16.Parse(this.form.TEI_SUU_RYOU_KBN.Text);
                }
                this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD = this.form.FURI_KOMI_MOTO_BANK_CD.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD = this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI = this.form.FURI_KOMI_MOTO_SHURUI.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO = this.form.FURI_KOMI_MOTO_NO.Text;
                this.entitysTOROHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NAME = this.form.FURI_KOMI_MOTO_NAME.Text;
                //160026 E
                this.entitysTOROHIKISAKI_SHIHARAI.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP_HIKIAI_TORIHIKISAKI_SHIHARAI.Text);

                // 更新者情報設定
                var dataBinderLogicTorihikisaki = new DataBinderLogic<M_HIKIAI_TORIHIKISAKI>(this.entitysTORIHIKISAKI);
                dataBinderLogicTorihikisaki.SetSystemProperty(this.entitysTORIHIKISAKI, false);
                var dataBinderLogicTorihikisakiSeikyuu = new DataBinderLogic<M_HIKIAI_TORIHIKISAKI_SEIKYUU>(this.entitysTORIHIKISAKI_SEIKYUU);
                dataBinderLogicTorihikisakiSeikyuu.SetSystemProperty(this.entitysTORIHIKISAKI_SEIKYUU, false);
                var dataBinderLogicTorihikisakiShiharai = new DataBinderLogic<M_HIKIAI_TORIHIKISAKI_SHIHARAI>(this.entitysTOROHIKISAKI_SHIHARAI);
                dataBinderLogicTorihikisakiShiharai.SetSystemProperty(this.entitysTOROHIKISAKI_SHIHARAI, false);

                #region 取引先CDで紐付いた引合業者と引合現場の拠点CDを更新

                {
                    var hikiaiSql = "SELECT * FROM {0} WHERE {0}.TORIHIKISAKI_CD = '{1}' AND {0}.KYOTEN_CD != {2} AND {0}.HIKIAI_TORIHIKISAKI_USE_FLG = 1;";
                    var torihikisakiCD = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                    var kyotenCD = this.entitysTORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD;

                    // 引合業者マスタ（最終更新情報等は更新しない）
                    this.entitysM_HIKIAI_GYOUSHA_LIST = this.daoGYOUSHA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GYOUSHA", torihikisakiCD, kyotenCD));
                    for (int i = 0; i < this.entitysM_HIKIAI_GYOUSHA_LIST.Length; i++)
                    {
                        this.entitysM_HIKIAI_GYOUSHA_LIST[i].KYOTEN_CD = kyotenCD;
                    }

                    // 引合現場マスタ（最終更新情報等は更新しない）
                    this.entitysM_HIKIAI_GENBA_LIST = this.daoGENBA.GetDateForStringSql(string.Format(hikiaiSql, "M_HIKIAI_GENBA", torihikisakiCD, kyotenCD));
                    for (int i = 0; i < this.entitysM_HIKIAI_GENBA_LIST.Length; i++)
                    {
                        this.entitysM_HIKIAI_GENBA_LIST[i].KYOTEN_CD = kyotenCD;
                    }
                }

                #endregion
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("CreateEntity", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateEntity", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputData">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            try
            {
                LogUtility.DebugMethodStart(inputData);

                if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
                {
                    return inputData;
                }

                PropertyInfo pi = this.form.TORIHIKISAKI_CD.GetType().GetProperty(ConstCls.CHARACTERS_NUMBER);
                Decimal charNumber = (Decimal)pi.GetValue(this.form.TORIHIKISAKI_CD, null);

                string padData = inputData.PadLeft((int)charNumber, '0');

                return padData;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZeroPadding", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD重複チェック
        /// </summary>
        /// <param name="zeroPadCd">CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        public ConstCls.TorihikisakiCdLeaveResult DupliCheckTorihikisakiCd(string zeroPadCd, bool isRegister)
        {
            try
            {
                LogUtility.DebugMethodStart(zeroPadCd, isRegister);

                // 取引先マスタ検索
                M_HIKIAI_TORIHIKISAKI torihikisakiKey = new M_HIKIAI_TORIHIKISAKI();
                torihikisakiKey.TORIHIKISAKI_CD = zeroPadCd;
                DataTable torihikiTable = this.daoTORIHIKISAKI.GetInputCdDataHikiaiTorihikisakiData(torihikisakiKey);

                // 重複チェック
                M461Validator vali = new M461Validator();
                DialogResult resultDialog = new DialogResult();
                bool resultDupli = vali.TorihikisakiCDValidator(torihikiTable, isRegister, out resultDialog);

                ConstCls.TorihikisakiCdLeaveResult ViewUpdateWindow = 0;

                // 重複チェックの結果と、ポップアップの結果で動作を変える
                if (!resultDupli && resultDialog == DialogResult.OK)
                {
                    ViewUpdateWindow = ConstCls.TorihikisakiCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli && resultDialog == DialogResult.Yes)
                {
                    ViewUpdateWindow = ConstCls.TorihikisakiCdLeaveResult.FALSE_ON;
                }
                else if (!resultDupli && resultDialog == DialogResult.No)
                {
                    ViewUpdateWindow = ConstCls.TorihikisakiCdLeaveResult.FALSE_OFF;
                }
                else if (!resultDupli)
                {
                    ViewUpdateWindow = ConstCls.TorihikisakiCdLeaveResult.FALSE_NONE;
                }
                else
                {
                    ViewUpdateWindow = ConstCls.TorihikisakiCdLeaveResult.TURE_NONE;
                }

                LogUtility.DebugMethodEnd();

                return ViewUpdateWindow;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliCheckTorihikisakiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
 
       /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
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
                    // 引合取引先入力画面表示時の取引先CDで再検索・再表示
                    this.SetWindowData();
                    this.WindowInitNewMode(parentForm);
                    this.SetDataForWindow();

                    this.form.TORIHIKISAKI_CD.Enabled = false;   // 取引先CD
                    this.form.SAIBAN_BUTTON.Enabled = false;    // 採番ボタン
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        // 20140708 ria No.947 営業管理機能改修 start

        #region 移行

        /// <summary>
        /// データ移行処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void MoveToTuujyou(string bak_TORIHIKISAKI_CD)
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                using (Transaction tran = new Transaction())
                {
                    string oldTORIHIKISAKI_CD = bak_TORIHIKISAKI_CD;
                    string newTORIHIKISAKI_CD = this.GetMaxTorihikisakiCD();

                    // 採番できなかったときはデータ移行を中止する
                    if (this.SaibanErrorFlg)
                    {
                        return;
                    }

                    // 作成データは初期化する
                    string computerName = SystemInformation.ComputerName;
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                    //var CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    var CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end
                    var CREATE_USER = SystemProperty.UserName;
                    var CREATE_PC = computerName;

                    //通常マスタに移行
                    this.targetTORIHIKISAKIDAO.MoveData(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD, CREATE_DATE, CREATE_USER, CREATE_PC);
                    this.targetTUUTORIHIKISAKI_SEIKYUUDAO.MoveData(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD, this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN);
                    this.targetTUUTORIHIKISAKI_SHIHARAIDAO.MoveData(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

                    // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
                    // 20140718 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
                    //入金先が自社の場合は、新たに採番した取引先のCDを入金先マスタに登録する。
                    //if (this.entitysTORIHIKISAKI.NYUUKINSAKI_KBN == 1)
                    //{
                    //    this.CreateNyuukinsakiEntity(newTORIHIKISAKI_CD);
                    //}
                    this.CreateNyuukinsakiEntity(newTORIHIKISAKI_CD);
                    // 20140718 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

                    //新たに採番した取引先のCDを出金先マスタに登録する。
                    this.CreateShukkinsakiEntity(newTORIHIKISAKI_CD);
                    // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

                    //見積もりデータを更新する
                    this.UpdateMitsumoriEntry(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

                    //移行後データを削除
                    this.MoveAfterDelete(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

                    // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　start
                    //引合業者関係のデータを更新する
                    this.UpdateHikiaiGyousha(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

                    //引合現場関係のデータを更新する
                    this.UpdateHikiaiGenba(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);
                    // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　end

                    // トランザクション終了
                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "通常マスタへの移行");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.errmessage.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 移行後見積のデータを更新処理
        /// </summary>
        [Transaction]
        public virtual void UpdateMitsumoriEntry(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            daoMITSUMORI_ENTRY.UpdateTORIHIKISAKICD(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 移行後データ削除処理
        /// </summary>
        [Transaction]
        public virtual void MoveAfterDelete(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            M_HIKIAI_TORIHIKISAKI newTORIHIKISAKI = this.daoTORIHIKISAKI.GetDataByCd(oldTORIHIKISAKI_CD);

            newTORIHIKISAKI.TORIHIKISAKI_CD_AFTER = newTORIHIKISAKI_CD;
            newTORIHIKISAKI.DELETE_FLG = true;

            this.daoTORIHIKISAKI.Update(newTORIHIKISAKI);

            LogUtility.DebugMethodEnd();
        }

        // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない start
        /// <summary>
        /// 入金先データ作成処理
        /// </summary>
        [Transaction]
        public virtual void CreateNyuukinsakiEntity(string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(newTORIHIKISAKI_CD);

            this.entitysM_NYUUKINSAKI = new M_NYUUKINSAKI();
            //入金先マスタ
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_CD = newTORIHIKISAKI_CD;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME1 = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME2 = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_NAME_RYAKU = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_FURIGANA = this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_TEL = this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_FAX = this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_POST = this.entitysTORIHIKISAKI.TORIHIKISAKI_POST;
            if (!string.IsNullOrEmpty(Convert.ToString(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD)))
            {
                this.entitysM_NYUUKINSAKI.NYUUKINSAKI_TODOUFUKEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD;
            }
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_ADDRESS1 = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
            this.entitysM_NYUUKINSAKI.NYUUKINSAKI_ADDRESS2 = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
            if (this.entitysM_SYS_INFO != null)
            {
                this.entitysM_NYUUKINSAKI.TORIKOMI_KBN = this.entitysM_SYS_INFO.NYUUKIN_TORIKOMI_KBN;
            }
            else
            {
                this.entitysM_NYUUKINSAKI.TORIKOMI_KBN = 0;
            }

            var WHO = new DataBinderLogic<M_NYUUKINSAKI>(this.entitysM_NYUUKINSAKI);
            WHO.SetSystemProperty(this.entitysM_NYUUKINSAKI, false);

            this.entitysM_NYUUKINSAKI.DELETE_FLG = false;
            this.targetM_NYUUKINSAKIDAO.Insert(this.entitysM_NYUUKINSAKI);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出金先データ作成処理
        /// </summary>
        [Transaction]
        public virtual void CreateShukkinsakiEntity(string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(newTORIHIKISAKI_CD);

            //出金先マスタ
            this.entitysM_SYUKKINSAKI = new M_SYUKKINSAKI();
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_CD = newTORIHIKISAKI_CD;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME1 = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME1;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME2 = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME2;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_NAME_RYAKU = this.entitysTORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_FURIGANA = this.entitysTORIHIKISAKI.TORIHIKISAKI_FURIGANA;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_TEL = this.entitysTORIHIKISAKI.TORIHIKISAKI_TEL;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_FAX = this.entitysTORIHIKISAKI.TORIHIKISAKI_FAX;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_POST = this.entitysTORIHIKISAKI.TORIHIKISAKI_POST;
            if (!string.IsNullOrEmpty(Convert.ToString(this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD)))
            {
                this.entitysM_SYUKKINSAKI.SYUKKINSAKI_TODOUFUKEN_CD = this.entitysTORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD;
            }
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_ADDRESS1 = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS1;
            this.entitysM_SYUKKINSAKI.SYUKKINSAKI_ADDRESS2 = this.entitysTORIHIKISAKI.TORIHIKISAKI_ADDRESS2;
            if (this.entitysM_SYS_INFO != null)
            {
                this.entitysM_SYUKKINSAKI.TORIKOMI_KBN = 1;
            }
            else
            {
                this.entitysM_SYUKKINSAKI.TORIKOMI_KBN = 0;
            }

            var WHO = new DataBinderLogic<M_SYUKKINSAKI>(this.entitysM_SYUKKINSAKI);
            WHO.SetSystemProperty(this.entitysM_SYUKKINSAKI, false);

            this.entitysM_SYUKKINSAKI.DELETE_FLG = false;
            this.targetM_SYUKKINSAKIDAO.Insert(this.entitysM_SYUKKINSAKI);

            LogUtility.DebugMethodEnd();
        }

        // 20140715 ria EV005233 引合取引先から取引先へ移行する際に入金先・出金先が作成されない end

        /// <summary>
        /// 取引先CD採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public string GetMaxTorihikisakiCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先マスタと入金先マスタと出金先マスタのCDの最大値+1をそれぞれ取得
                HikiaiTorihikisakiMasterAccess torihikisakiMasterAccess = new HikiaiTorihikisakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { }, true);
                NyuukinsakiMasterAccess nuukinsakiMasterAccess = new NyuukinsakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                ShukkinnsakiMasterAccess shukkinsakiMasterAccess = new ShukkinnsakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });

                string maxTorihikisakiCD = "";
                this.SaibanErrorFlg = false;

                int keyTorihikisaki = -1;
                int keyNyuukin = -1;
                int keyShukkin = -1;

                var keyTorihikisakiSsaibanFlag = torihikisakiMasterAccess.IsOverCDLimit(out keyTorihikisaki, true);
                var keyNuukinssaibanFlag = nuukinsakiMasterAccess.IsOverCDLimit(out keyNyuukin);
                var keyShukkinssaibanFlag = shukkinsakiMasterAccess.IsOverCDLimit(out keyShukkin);

                if (keyTorihikisakiSsaibanFlag && keyNuukinssaibanFlag && keyShukkinssaibanFlag)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    maxTorihikisakiCD = "";
                    this.SaibanErrorFlg = true;
                }

                if (!this.SaibanErrorFlg)
                {
                    // 大きい方の値を使用する
                    int plusKey = keyTorihikisaki < keyNyuukin ? keyNyuukin : keyTorihikisaki;
                    plusKey = plusKey < keyShukkin ? keyShukkin : plusKey;

                    if (plusKey < 1)
                    {
                        // 採番エラー
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E041");
                        maxTorihikisakiCD = "";
                        this.SaibanErrorFlg = true;
                    }
                    else
                    {
                        // ゼロパディング後、テキストへ設定
                        maxTorihikisakiCD = String.Format("{0:D" + this.form.TORIHIKISAKI_CD.MaxLength + "}", plusKey);
                    }
                }
                return maxTorihikisakiCD;
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

        #endregion

        // 20140708 ria No.947 営業管理機能改修 end

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

                // 入金先区分が他社の場合、入金先CDを必須とする
                if (this.form.NYUUKINSAKI_KBN.Text.Equals("2") && string.IsNullOrEmpty(this.form.NYUUKINSAKI_CD.Text))
                {
                    //取引先区分が現金の場合のみ必須とする
                    if (this.form.TORIHIKI_KBN.Text.Equals("2") && string.IsNullOrEmpty(this.form.TORIHIKI_KBN.Text))
                    {
                        msgLogic.MessageBoxShow("E001", HikiaiTorihikisakiHoshu.Properties.Resources.NYUUKINSAKI_CD);
                        errorFlag = true;
                    }
                }

                if (errorFlag)
                {
                    this.isRegist = false;
                    return;
                }

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        this.entitysTORIHIKISAKI.DELETE_FLG = false;
                        this.daoTORIHIKISAKI.Insert(entitysTORIHIKISAKI);
                        this.daoTORIHIKISAKI_SEIKYUU.Insert(entitysTORIHIKISAKI_SEIKYUU);
                        this.daoTORIHIKISAKI_SHIHARAI.Insert(entitysTOROHIKISAKI_SHIHARAI);
                        // トランザクション終了
                        tran.Commit();
                    }
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.isRegist = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.errmessage.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245", "");
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

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        this.entitysTORIHIKISAKI.DELETE_FLG = false;
                        this.daoTORIHIKISAKI.Update(entitysTORIHIKISAKI);
                        this.daoTORIHIKISAKI_SEIKYUU.Update(entitysTORIHIKISAKI_SEIKYUU);
                        this.daoTORIHIKISAKI_SHIHARAI.Update(entitysTOROHIKISAKI_SHIHARAI);

                        #region 取引先CDで紐付いた引合業者と引合現場の更新

                        // 引合業者
                        foreach (var hikiaiGyousha in this.entitysM_HIKIAI_GYOUSHA_LIST)
                        {
                            this.daoGYOUSHA.Update(hikiaiGyousha);
                        }

                        // 引合現場
                        foreach (var hikiaiGenba in this.entitysM_HIKIAI_GENBA_LIST)
                        {
                            this.daoGENBA.Update(hikiaiGenba);
                        }

                        #endregion

                        // トランザクション終了
                        tran.Commit();
                    }
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
                    this.form.errmessage.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245", "");
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
                    // 他マスタの使用有無を確認
                    if (!CheckDelete())
                    {
                        return;
                    }

                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        this.entitysTORIHIKISAKI.DELETE_FLG = true;
                        this.daoTORIHIKISAKI.Update(this.entitysTORIHIKISAKI);
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
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245", "");
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
        //[Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 電子申請登録

        /// <summary>
        /// 電子申請登録処理
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

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        this.entitysTORIHIKISAKI.DELETE_FLG = false;
                        this.daoTORIHIKISAKI.Update(entitysTORIHIKISAKI);
                        this.daoTORIHIKISAKI_SEIKYUU.Update(entitysTORIHIKISAKI_SEIKYUU);
                        this.daoTORIHIKISAKI_SHIHARAI.Update(entitysTOROHIKISAKI_SHIHARAI);

                        #region 取引先CDで紐付いた引合業者と引合現場の更新

                        // 引合業者
                        foreach (var hikiaiGyousha in this.entitysM_HIKIAI_GYOUSHA_LIST)
                        {
                            this.daoGYOUSHA.Update(hikiaiGyousha);
                        }

                        // 引合現場
                        foreach (var hikiaiGenba in this.entitysM_HIKIAI_GENBA_LIST)
                        {
                            this.daoGENBA.Update(hikiaiGenba);
                        }

                        #endregion

                        #region 電子申請

                        // 申請入力登録
                        DenshiShinseiDBAccessor dsDBAccessor = new DenshiShinseiDBAccessor();

                        // 電子申請入力の登録
                        dsDBAccessor.InsertDSEntry(dsEntry, DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_TORIHIKISAKI);

                        // 電子申請明細の登録
                        foreach (T_DENSHI_SHINSEI_DETAIL dsDetail in dsDetailList)
                        {
                            dsDBAccessor.InsertDSDetail(dsDetail, dsEntry.SYSTEM_ID, dsEntry.SEQ, dsEntry.SHINSEI_NUMBER);
                        }

                        // 電子申請状態の登録
                        dsDBAccessor.InsertDSStatus(new T_DENSHI_SHINSEI_STATUS(), dsEntry.SYSTEM_ID, dsEntry.SEQ);

                        #endregion

                        // トランザクション終了
                        tran.Commit();
                    }
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
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
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
            catchErr = true;
            bool isOk = true;
            try
            {
                var dsUtility = new DenshiShinseiUtility();
                isOk = dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.HIKIAI_TORIHIKISAKI, this.form.TORIHIKISAKI_CD.Text, null, null);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckDenshiShinseiData", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDenshiShinseiData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return isOk;
        }

        #endregion

        #region 本登録済みか判定

        /// <summary>
        /// 指定された取引先CDが本登録済みデータか判定
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedTorihikisaki(string torihikisakiCD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(torihikisakiCD))
                {
                    return false;
                }

                var entity = daoTORIHIKISAKI.GetDataByCd(torihikisakiCD);
                if (entity != null && !string.IsNullOrEmpty(entity.TORIHIKISAKI_CD_AFTER))
                {
                    // 移行後取引先CDに値がある場合は、本登録済みデータとみなす
                    return true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ExistedTorihikisaki", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ExistedTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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

            returnVal.HikiaiTorihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            returnVal.HikiaiTorihikisakiNameRyaku = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
            returnVal.NaiyouCd = DenshiShinseiUtility.NAIYOU_CD_TORIHIKISAKI;

            return returnVal;
        }

        #endregion

        #region Equals/GetHashCode/ToString

        //<summary>
        //クラスが等しいかどうか判定
        //</summary>
        //<param name="other"></param>
        //<returns></returns>
        public override bool Equals(object other)
        {
            try
            {
                LogUtility.DebugMethodStart(other);

                //objがnullか、型が違うときは、等価でない
                if (other == null || this.GetType() != other.GetType())
                {
                    return false;
                }

                LogicCls localLogic = other as LogicCls;
                return localLogic == null ? false : true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Equals", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //ハッシュコード取得
        //</summary>
        //<returns></returns>
        public override int GetHashCode()
        {
            try
            {
                LogUtility.DebugMethodStart();
                return base.GetHashCode();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHashCode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // <summary>
        //該当するオブジェクトを文字列形式で取得
        //</summary>
        //<returns></returns>
        public override string ToString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                return base.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ToString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        //<summary>
        //ボタン初期化処理
        //</summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
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

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;

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
                    // 電子申請オプションが無効の場合
                    if (!DensisinseiOptionEnabledFlg)
                    {
                        //移行(F1)イベント生成
                        parentForm.bt_func1.Click += new EventHandler(this.form.MoveToTuujyou);
                    }

                    //新規(F2)イベント生成
                    parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                    //修正ボタン(F3)イベント生成
                    parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                    //削除(F4)イベント生成
                    //parentForm.bt_func4.Click += new EventHandler(this.form.UpdateMode);

                    //一覧(F7)イベント生成
                    parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                    //登録(F9)イベント生成
                    this.form.C_Regist(parentForm.bt_func9);
                    parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                    parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                    //取消し(F11)イベント生成
                    parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                    //閉じるボタン(F12)イベント生成
                    parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                }

                //取引先CD変更時処理
                this.form.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.TorihikisakiCdValidating);

                //取引先CD変更後処理
                this.form.TORIHIKISAKI_CD.Validated += new EventHandler(this.form.TorihikisakiCdValidated);

                //採番ボタン押下処理
                this.form.SAIBAN_BUTTON.Click += new EventHandler(this.form.SaibanButtonClick);

                //請求タブの取引先コピーボタン押下処理
                this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Click += new EventHandler(this.form.CopySeikyuButtonClick);

                //支払タブの取引先コピーボタン押下処理
                this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Click += new EventHandler(this.form.CopySiharaiButtonClick);

                //分類タブの取引先コピーボタン押下処理
                this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Click += new EventHandler(this.form.CopyManiButtonClick);

                //営業担当部署CD変更後処理
                this.form.EIGYOU_TANTOU_BUSHO_CD.Validated += new EventHandler(this.form.EigyouTantouBushoCdValidated);

                //営業担当者CD変更後処理
                this.form.EIGYOU_TANTOU_CD.Validated += new EventHandler(this.form.EigyouTantouCdValidated);

                //振込銀行CD変更後処理
                this.form.FURIKOMI_BANK_CD.Validated += new EventHandler(this.form.FurikomiBankCdValidated);

                //振込銀行CD2変更後処理
                this.form.FURIKOMI_BANK_CD_2.Validated += new EventHandler(this.form.FurikomiBankCd2Validated);

                //振込銀行CD3変更後処理
                this.form.FURIKOMI_BANK_CD_3.Validated += new EventHandler(this.form.FurikomiBankCd3Validated);
                //振込元銀行
                this.form.FURI_KOMI_MOTO_BANK_CD.Validated += new EventHandler(this.form.FurikomiMotoBankCdValidated);//160026
                // 振込元支店バリデート処理
                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Validating += new CancelEventHandler(this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD_Validating);//160026
                this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Validated += new EventHandler(this.form.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD_Validated);//160026


                /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　start
                this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(TEKIYOU_BEGIN_Leave);
                this.form.TEKIYOU_END.Leave += new System.EventHandler(TEKIYOU_END_Leave);
                /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　start

                /// 20141226 Houkakou 「引合取引先入力」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
                /// 20141226 Houkakou 「引合取引先入力」のダブルクリックを追加する　end

                this.form.NYUUKINSAKI_KBN.TextChanged += new EventHandler(this.form.NYUUKINSAKI_KBN_TextChanged);
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

        //<summary>
        //ボタン設定の読込
        //</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

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
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //検索条件初期化
        //</summary>
        private void ClearCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.torihikisakiCD = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public bool Saiban()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先マスタと入金先マスタと出金先マスタのCDの最大値+1をそれぞれ取得
                HikiaiTorihikisakiMasterAccess torihikisakiMasterAccess = new HikiaiTorihikisakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });

                int keyTorihikisaki = -1;

                var keyTorihikisakiSsaibanFlag = torihikisakiMasterAccess.IsOverCDLimit(out keyTorihikisaki);

                if (keyTorihikisakiSsaibanFlag)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.TORIHIKISAKI_CD.Text = "";
                }

                // 大きい方の値を使用する
                int plusKey = keyTorihikisaki;

                if (plusKey < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.TORIHIKISAKI_CD.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.TORIHIKISAKI_CD.Text = String.Format("{0:D" + this.form.TORIHIKISAKI_CD.MaxLength + "}", plusKey);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Saiban", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        ///<summary>
        /// 取引先の情報を請求にコピーする
        ///</summary>
        public void CopyToSeikyu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SEIKYUU_SOUFU_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.SEIKYUU_SOUFU_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.SEIKYUU_SOUFU_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.SEIKYUU_SOUFU_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.SEIKYUU_SOUFU_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.SEIKYUU_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SEIKYUU_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SEIKYUU_SOUFU_TEL.Text = this.form.TORIHIKISAKI_TEL.Text;
                this.form.SEIKYUU_SOUFU_FAX.Text = this.form.TORIHIKISAKI_FAX.Text;
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

        ///<summary>
        /// 取引先の情報を支払いのコピーする
        ///</summary>
        public void CopyToSiharai()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.SHIHARAI_SOUFU_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.SHIHARAI_SOUFU_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.SHIHARAI_SOUFU_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.SHIHARAI_SOUFU_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.SHIHARAI_SOUFU_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.SHIHARAI_SOUFU_BUSHO.Text = this.form.BUSHO.Text;
                this.form.SHIHARAI_SOUFU_TANTOU.Text = this.form.TANTOUSHA.Text;
                this.form.SHIHARAI_SOUFU_TEL.Text = this.form.TORIHIKISAKI_TEL.Text;
                this.form.SHIHARAI_SOUFU_FAX.Text = this.form.TORIHIKISAKI_FAX.Text;
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

        ///<summary>
        /// 取引先の情報を分類にコピーする
        ///</summary>
        public void CopyToMani()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.MANI_HENSOUSAKI_NAME1.Text = this.form.TORIHIKISAKI_NAME1.Text;
                this.form.MANI_HENSOUSAKI_NAME2.Text = this.form.TORIHIKISAKI_NAME2.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = this.form.TORIHIKISAKI_KEISHOU2.Text;
                this.form.MANI_HENSOUSAKI_POST.Text = this.form.TORIHIKISAKI_POST.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Text = this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text + this.form.TORIHIKISAKI_ADDRESS1.Text;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Text = this.form.TORIHIKISAKI_ADDRESS2.Text;
                this.form.MANI_HENSOUSAKI_BUSHO.Text = this.form.BUSHO.Text;
                this.form.MANI_HENSOUSAKI_TANTOU.Text = this.form.TANTOUSHA.Text;
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
                this.form.errmessage.MessageBoxShow("E245", "");
                res = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
            return res;
        }

        /// <summary>
        /// 営業担当部署CD変更後処理
        /// </summary>
        public void EigyouTantouBushoCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    this.form.EIGYOU_TANTOU_BUSHO_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_CD.Text = string.Empty;
                    this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouBushoCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当者CD変更後処理
        /// </summary>
        public bool EigyouTantouCdValidated()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_CD.Text))
                {
                    M_SHAIN prm = new M_SHAIN();
                    prm.SHAIN_CD = this.form.EIGYOU_TANTOU_CD.Text;
                    prm.EIGYOU_TANTOU_KBN = true;
                    if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                    {
                        prm.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    }
                    M_SHAIN[] data = this.daoIM_SHAIN.GetAllValidData(prm);
                    if (data != null && data.Length > 0)
                    {
                        this.entitysM_SHAIN = data[0];
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = this.entitysM_SHAIN.BUSHO_CD;
                        this.entitysM_BUSHO = this.daoIM_BUSHO.GetDataByCd(this.entitysM_SHAIN.BUSHO_CD);
                        if (this.entitysM_BUSHO != null)
                        {
                            this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = this.entitysM_BUSHO.BUSHO_NAME_RYAKU;
                        }

                        this.form.EIGYOU_TANTOU_CD.Text = this.entitysM_SHAIN.SHAIN_CD;
                        this.form.EIGYOU_TANTOU_NAME.Text = this.entitysM_SHAIN.SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.EIGYOU_TANTOU_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.EIGYOU_TANTOU_CD.ForeColor = Constans.ERROR_COLOR_FORE;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "営業担当者");
                        this.form.EIGYOU_TANTOU_CD.Focus();
                        this.form.EIGYOU_TANTOU_CD.IsInputErrorOccured = true;
                        ret = false;
                    }
                }
                else
                {
                    this.form.EIGYOU_TANTOU_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EigyouTantouCdValidated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 振込銀行CD変更後処理
        /// </summary>
        public void FurikomiBankCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD.Text))
                {
                    this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                }

                // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
                this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 振込銀行CD2変更後処理
        /// </summary>
        public void FurikomiBankCd2Validated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD_2.Text))
                {
                    this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                    this.form.KOUZA_SHURUI_2.Text = string.Empty;
                    this.form.KOUZA_NO_2.Text = string.Empty;
                    this.form.KOUZA_NAME_2.Text = string.Empty;
                }

                this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 振込銀行CD3変更後処理
        /// </summary>
        public void FurikomiBankCd3Validated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURIKOMI_BANK_CD_3.Text))
                {
                    this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                    this.form.KOUZA_SHURUI_3.Text = string.Empty;
                    this.form.KOUZA_NO_3.Text = string.Empty;
                    this.form.KOUZA_NAME_3.Text = string.Empty;
                }

                this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCd3Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 入金先CD変更後処理
        /// </summary>
        public bool NyuukinsakiCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.NYUUKINSAKI_CD.Text))
                {
                    M_NYUUKINSAKI nyu = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>().GetDataByCd(this.form.NYUUKINSAKI_CD.Text);
                    if (nyu != null)
                    {
                        this.form.NYUUKINSAKI_NAME1.Text = nyu.NYUUKINSAKI_NAME1;
                        this.form.NYUUKINSAKI_NAME2.Text = nyu.NYUUKINSAKI_NAME2;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NyuukinsakiCdValidated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("NyuukinsakiCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 取引区分変更後処理
        /// </summary>
        /// <param name="isTextChanged">true:calling TextChangedEvent, false:other process</param>
        public bool ChangeTorihikiKbn(bool isTextChanged)
        {
            try
            {
                LogUtility.DebugMethodStart(isTextChanged);

                if (this.form.TORIHIKI_KBN.Text.Equals("1"))
                {
                    this.form.SHIMEBI1.SelectedIndex = -1;
                    this.form.SHIMEBI2.SelectedIndex = -1;
                    this.form.SHIMEBI3.SelectedIndex = -1;
                    this.form.SHIMEBI1.Enabled = false;
                    this.form.SHIMEBI2.Enabled = false;
                    this.form.SHIMEBI3.Enabled = false;
                    this.form.HICCHAKUBI.Text = string.Empty;
                    this.form.HICCHAKUBI.Enabled = false;
                    //158524 S
                    this.form.KAISHUU_BETSU_KBN.Text = string.Empty;
                    this.form.KAISHUU_BETSU_KBN.Enabled = false;
                    this.form.KAISHUU_BETSU_KBN_1.Enabled = false;
                    this.form.KAISHUU_BETSU_KBN_2.Enabled = false;
                    this.form.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                    this.form.KAISHUU_BETSU_NICHIGO.Enabled = false;
                    //158524 E

                    this.form.KAISHUU_MONTH.Text = string.Empty;
                    this.form.KAISHUU_MONTH.Enabled = false;
                    this.form.KAISHUU_MONTH_1.Enabled = false;
                    this.form.KAISHUU_MONTH_2.Enabled = false;
                    this.form.KAISHUU_MONTH_3.Enabled = false;
                    this.form.KAISHUU_MONTH_4.Enabled = false;
                    this.form.KAISHUU_MONTH_5.Enabled = false;
                    this.form.KAISHUU_MONTH_6.Enabled = false;
                    this.form.KAISHUU_MONTH_7.Enabled = false;
                    this.form.KAISHUU_DAY.Text = string.Empty;
                    this.form.KAISHUU_DAY.Enabled = false;
                    this.form.KAISHUU_HOUHOU.Text = string.Empty;
                    this.form.KAISHUU_HOUHOU.Enabled = false;
                    this.form.KAISHUU_HOUHOU_SEARCH.Enabled = false;
                    this.form.KAISHUU_HOUHOU_NAME.Text = string.Empty;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = string.Empty;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Enabled = false;
                    this.form.SHOSHIKI_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_KBN.Enabled = false;
                    this.form.SHOSHIKI_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_KBN_3.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    this.form.SHOSHIKI_MEISAI_KBN.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN.Text = string.Empty;
                    this.form.SEIKYUU_KEITAI_KBN.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_1.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN.Text = string.Empty;
                    this.form.NYUUKIN_MEISAI_KBN.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_2.Enabled = false;
                    this.form.YOUSHI_KBN.Text = string.Empty;
                    this.form.YOUSHI_KBN.Enabled = false;
                    this.form.YOUSHI_KBN_1.Enabled = false;
                    this.form.YOUSHI_KBN_2.Enabled = false;
                    this.form.YOUSHI_KBN_3.Enabled = false;
                    this.form.OUTPUT_KBN.Text = string.Empty;
                    this.form.OUTPUT_KBN.Enabled = false;
                    this.form.OUTPUT_KBN_1.Enabled = false;
                    this.form.OUTPUT_KBN_2.Enabled = false;
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                    this.form.HAKKOUSAKI_CD.Enabled = false;

                    this.form.ZEI_KEISAN_KBN_CD_2.Enabled = false;
                    if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text))
                    {
                        this.form.ZEI_KEISAN_KBN_CD.Text = ConstCls.ZEI_KEISAN_KBN_DENPYOU.ToString();
                    }

                    this.form.FURIKOMI_BANK_CD.Enabled = false;
                    this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                    // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
                    this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                    // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏
                    this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = false;
                    this.form.KOUZA_SHURUI.Text = string.Empty;
                    this.form.KOUZA_NO.Text = string.Empty;
                    this.form.KOUZA_NAME.Text = string.Empty;
                    this.form.FURIKOMI_BANK_CD_2.Enabled = false;
                    this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH_2.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                    this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = false;
                    this.form.KOUZA_SHURUI_2.Text = string.Empty;
                    this.form.KOUZA_NO_2.Text = string.Empty;
                    this.form.KOUZA_NAME_2.Text = string.Empty;
                    this.form.FURIKOMI_BANK_CD_3.Enabled = false;
                    this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SEARCH_3.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = false;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                    this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                    this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = false;
                    this.form.KOUZA_SHURUI_3.Text = string.Empty;
                    this.form.KOUZA_NO_3.Text = string.Empty;
                    this.form.KOUZA_NAME_3.Text = string.Empty;
                    this.form.SEIKYUU_JOUHOU1.Enabled = false;
                    this.form.SEIKYUU_JOUHOU1.Text = string.Empty;
                    this.form.SEIKYUU_JOUHOU2.Enabled = false;
                    this.form.SEIKYUU_JOUHOU2.Text = string.Empty;
                    this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_NAME2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_NAME2.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_POST.Enabled = false;
                    this.form.SEIKYUU_SOUFU_POST.Text = string.Empty;
                    this.form.SEIKYUU_JUSHO_SEARCH.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = false;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Text = string.Empty;
                    this.form.SEIKYUU_POST_SEARCH.Enabled = false;
                    this.form.SEIKYUU_SOUFU_BUSHO.Enabled = false;
                    this.form.SEIKYUU_SOUFU_BUSHO.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_TANTOU.Enabled = false;
                    this.form.SEIKYUU_SOUFU_TANTOU.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_TEL.Enabled = false;
                    this.form.SEIKYUU_SOUFU_TEL.Text = string.Empty;
                    this.form.SEIKYUU_SOUFU_FAX.Enabled = false;
                    this.form.SEIKYUU_SOUFU_FAX.Text = string.Empty;
                    this.form.NYUUKINSAKI_CD.Enabled = false;
                    this.form.NYUUKINSAKI_CD.Text = string.Empty;
                    this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                    this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                    this.form.NYUUKINSAKI_SEARCH.Enabled = false;
                    this.form.SEIKYUU_TANTOU.Enabled = false;
                    this.form.SEIKYUU_TANTOU.Text = string.Empty;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = false;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = false;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_NAME.Enabled = false;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
                    this.form.NYUUKINSAKI_KBN.Text = "1";
                    this.form.NYUUKINSAKI_KBN.Enabled = false;
                    this.form.NYUUKINSAKI_KBN_1.Enabled = false;
                    this.form.NYUUKINSAKI_KBN_2.Enabled = false;
                    this.form.FURIKOMI_NAME1.Enabled = false;
                    this.form.FURIKOMI_NAME1.Text = string.Empty;
                    this.form.FURIKOMI_NAME2.Enabled = false;
                    this.form.FURIKOMI_NAME2.Text = string.Empty;
                }
                else
                {
                    this.form.NYUUKINSAKI_KBN.Enabled = true;
                    this.form.NYUUKINSAKI_KBN_1.Enabled = true;
                    this.form.NYUUKINSAKI_KBN_2.Enabled = true;
                    this.form.SHIMEBI1.Enabled = true;
                    this.form.SHIMEBI2.Enabled = true;
                    this.form.SHIMEBI3.Enabled = true;
                    this.form.SHIMEBI1.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI1.IsNull)
                    {
                        this.form.SHIMEBI1.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.SHIMEBI2.Enabled = false;
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIMEBI2.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI2.IsNull)
                    {
                        this.form.SHIMEBI2.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI2.ToString();
                    }
                    else
                    {
                        this.form.SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIMEBI3.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI3.IsNull)
                    {
                        this.form.SHIMEBI3.SelectedItem = this.entitysM_SYS_INFO.SEIKYUU_SHIMEBI3.ToString();
                    }
                    this.form.HICCHAKUBI.Enabled = true;
                    this.form.HICCHAKUBI.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_HICCHAKUBI.IsNull)
                    {
                        this.form.HICCHAKUBI.Text = this.entitysM_SYS_INFO.SEIKYUU_HICCHAKUBI.ToString();
                    }
                    //160026 S
                    //this.form.KAISHUU_MONTH.Enabled = true;
                    //this.form.KAISHUU_MONTH_1.Enabled = true;
                    //this.form.KAISHUU_MONTH_2.Enabled = true;
                    //this.form.KAISHUU_MONTH_3.Enabled = true;
                    //this.form.KAISHUU_MONTH_4.Enabled = true;
                    //this.form.KAISHUU_MONTH_5.Enabled = true;
                    //this.form.KAISHUU_MONTH_6.Enabled = true;
                    //this.form.KAISHUU_MONTH_7.Enabled = true;
                    //this.form.KAISHUU_MONTH.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.IsNull)
                    //{
                    //    this.form.KAISHUU_MONTH.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.ToString();
                    //}    
                    this.form.KAISHUU_BETSU_KBN.Enabled = true;
                    this.form.KAISHUU_BETSU_KBN_1.Enabled = true;
                    this.form.KAISHUU_BETSU_KBN_2.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.IsNull)
                    {
                        this.form.KAISHUU_BETSU_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_KBN.ToString();
                    }
                    //160026 E
                    this.form.KAISHUU_DAY.Enabled = true;
                    this.form.KAISHUU_DAY.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.IsNull)
                    {
                        this.form.KAISHUU_DAY.Text = this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_DAY.ToString();
                    }
                    this.form.KAISHUU_HOUHOU.Enabled = true;
                    this.form.KAISHUU_HOUHOU_SEARCH.Enabled = true;
                    this.form.KAISHUU_HOUHOU.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.IsNull)
                    {
                        this.form.KAISHUU_HOUHOU.Text = string.Format("{0:D" + this.form.KAISHUU_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.ToString()));
                    }
                    this.form.KAISHUU_HOUHOU_NAME.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU.IsNull)
                    {
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysM_SYS_INFO.SEIKYUU_KAISHUU_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.KAISHUU_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    this.form.KAISHI_URIKAKE_ZANDAKA.Enabled = true;
                    this.form.KAISHI_URIKAKE_ZANDAKA.Text = "0";
                    this.form.SHOSHIKI_KBN.Enabled = true;
                    this.form.SHOSHIKI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_KBN_3.Enabled = true;
                    this.form.SHOSHIKI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_KBN.ToString();
                    }
                    this.form.SHOSHIKI_MEISAI_KBN.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHOSHIKI_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    this.form.SEIKYUU_KEITAI_KBN.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_1.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KEITAI_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KEITAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KEITAI_KBN.ToString();
                    }
                    this.form.NYUUKIN_MEISAI_KBN.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_2.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_NYUUKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.NYUUKIN_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_NYUUKIN_MEISAI_KBN.ToString();
                    }
                    this.form.YOUSHI_KBN.Enabled = true;
                    this.form.YOUSHI_KBN_1.Enabled = true;
                    this.form.YOUSHI_KBN_2.Enabled = true;
                    this.form.YOUSHI_KBN_3.Enabled = true;
                    this.form.YOUSHI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_YOUSHI_KBN.IsNull)
                    {
                        this.form.YOUSHI_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_YOUSHI_KBN.ToString();
                    }
                    this.form.OUTPUT_KBN.Enabled = true;
                    this.form.OUTPUT_KBN_1.Enabled = true;
                    this.form.OUTPUT_KBN_2.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.IsNull)
                    {
                        this.form.OUTPUT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_OUTPUT_KBN.ToString();
                    }
                    this.ChangeOutputKbn();
                    //var characterLimitListIndex = Array.IndexOf(this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList, '2');
                    //if (characterLimitListIndex < 0)
                    //{
                    //    List<char> list = new List<char>(this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList);
                    //    list.Insert(0, '2');
                    //    this.form.ZEI_KEISAN_KBN_CD.CharacterLimitList = list.ToArray();
                    //}
                    this.form.ZEI_KEISAN_KBN_CD_2.Enabled = true;

                    this.form.FURIKOMI_BANK_CD.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH.Enabled = true;
                    this.form.FURIKOMI_BANK_CD_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD_2.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_2.Enabled = true;
                    this.form.FURIKOMI_BANK_CD_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SEARCH_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_CD_3.Enabled = true;
                    this.form.FURIKOMI_BANK_SHITEN_SEARCH_3.Enabled = true;
                    this.form.SEIKYUU_JOUHOU1.Enabled = true;
                    this.form.SEIKYUU_JOUHOU2.Enabled = true;
                    this.form.TORIHIKISAKI_COPY_SEIKYU_BUTTON.Enabled = true;
                    this.form.SEIKYUU_SOUFU_NAME1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_KEISHOU1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_NAME2.Enabled = true;
                    this.form.SEIKYUU_SOUFU_KEISHOU2.Enabled = true;
                    this.form.SEIKYUU_SOUFU_POST.Enabled = true;
                    this.form.SEIKYUU_JUSHO_SEARCH.Enabled = true;
                    this.form.SEIKYUU_SOUFU_ADDRESS1.Enabled = true;
                    this.form.SEIKYUU_SOUFU_ADDRESS2.Enabled = true;
                    this.form.SEIKYUU_POST_SEARCH.Enabled = true;
                    this.form.SEIKYUU_SOUFU_BUSHO.Enabled = true;
                    this.form.SEIKYUU_SOUFU_TANTOU.Enabled = true;
                    this.form.SEIKYUU_SOUFU_TEL.Enabled = true;
                    this.form.SEIKYUU_SOUFU_FAX.Enabled = true;
                    if (this.form.NYUUKINSAKI_KBN.Text.Equals("2"))
                    {
                        this.form.NYUUKINSAKI_CD.Enabled = true;
                        this.form.NYUUKINSAKI_SEARCH.Enabled = true;
                    }
                    else
                    {
                        this.form.NYUUKINSAKI_CD.Enabled = false;
                        this.form.NYUUKINSAKI_CD.Text = string.Empty;
                        this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                        this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                        this.form.NYUUKINSAKI_SEARCH.Enabled = false;
                    }
                    this.form.SEIKYUU_TANTOU.Enabled = true;

                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Enabled = true;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_DAIHYOU_PRINT_KBN.ToString();
                    }
                    if (this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text == "1")
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_1.Enabled = true;
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN_2.Enabled = true;

                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_PRINT_KBN.ToString();
                    }
                    if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text == "1")
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_1.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN_2.Enabled = true;

                    this.form.SEIKYUU_KYOTEN_CD.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
                    }

                    this.form.SEIKYUU_KYOTEN_NAME.Enabled = true;
                    this.form.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    M_KYOTEN seikyuuKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
                    if (seikyuuKyoten != null)
                    {
                        this.form.SEIKYUU_KYOTEN_NAME.Text = seikyuuKyoten.KYOTEN_NAME_RYAKU;
                    }

                    this.form.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;

                    this.ChangeSeikyuuKyotenPrintKbn();

                    // [2.掛け]に変更した場合、以下を行う
                    // 請求書式明細区分の制限処理
                    this.LimitSeikyuuShoshikiMeisaiKbn();
                    this.LimitSeikyuuZeiKbn();

                    this.form.FURIKOMI_NAME1.Enabled = true;
                    this.form.FURIKOMI_NAME2.Enabled = true;

                    if (isTextChanged)
                    {
                        // TextChagngedイベントからの呼び出し。(またはユーザ操作による呼び出し)
                        this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                    }
                    else
                    {
                        //20150617 #3747 hoanghm start
                        //if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                        //if copy mod then don not set default value to FURIKOMI_BANK
                        if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && string.IsNullOrEmpty(this.torihikisakiCD))
                        {
                            //tab請求情報2の振込先銀行に初期値をセットする。
                            this.SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK();
                        }
                        //20150617 #3747 hoanghm end
                        else
                        {
                            this.form.FURIKOMI_BANK_CD.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                            this.form.KOUZA_SHURUI.Text = string.Empty;
                            this.form.KOUZA_NO.Text = string.Empty;
                            this.form.KOUZA_NAME.Text = string.Empty;
                            this.form.FURIKOMI_BANK_CD_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                            this.form.KOUZA_SHURUI_2.Text = string.Empty;
                            this.form.KOUZA_NO_2.Text = string.Empty;
                            this.form.KOUZA_NAME_2.Text = string.Empty;
                            this.form.FURIKOMI_BANK_CD_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_NAME_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                            this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                            this.form.KOUZA_SHURUI_3.Text = string.Empty;
                            this.form.KOUZA_NO_3.Text = string.Empty;
                            this.form.KOUZA_NAME_3.Text = string.Empty;
                        }
                    }

                    // 項目値を変更するとフォーカスが移動してしまうので戻す
                    this.form.TORIHIKI_KBN.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeTorihikiKbn", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeTorihikiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 支払取引区分変更後処理
        /// </summary>
        public bool ChangeSiharaiTorihikiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_TORIHIKI_KBN_CD.Text.Equals("1"))
                {
                    this.form.SHIHARAI_SHIMEBI1.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI2.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI3.SelectedIndex = -1;
                    this.form.SHIHARAI_SHIMEBI1.Enabled = false;
                    this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                    this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    //160026 S
                    this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_KBN.Enabled = false;
                    this.form.SHIHARAI_BETSU_KBN_1.Enabled = false;
                    this.form.SHIHARAI_BETSU_KBN_2.Enabled = false;
                    this.form.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_NICHIGO.Enabled = false;
                    //160026 E
                    this.form.SHIHARAI_MONTH.Text = string.Empty;
                    this.form.SHIHARAI_MONTH.Enabled = false;
                    this.form.SHIHARAI_MONTH_1.Enabled = false;
                    this.form.SHIHARAI_MONTH_2.Enabled = false;
                    this.form.SHIHARAI_MONTH_3.Enabled = false;
                    this.form.SHIHARAI_MONTH_4.Enabled = false;
                    this.form.SHIHARAI_MONTH_5.Enabled = false;
                    this.form.SHIHARAI_MONTH_6.Enabled = false;
                    this.form.SHIHARAI_MONTH_7.Enabled = false;
                    this.form.SHIHARAI_DAY.Text = string.Empty;
                    this.form.SHIHARAI_DAY.Enabled = false;
                    this.form.SHIHARAI_HOUHOU.Text = string.Empty;
                    this.form.SHIHARAI_HOUHOU.Enabled = false;
                    this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = false;
                    this.form.SHIHARAI_HOUHOU_NAME.Text = string.Empty;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = string.Empty;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_KEITAI_KBN.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN.Text = string.Empty;
                    this.form.SHUKKIN_MEISAI_KBN.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN.Text = string.Empty;
                    this.form.SHIHARAI_YOUSHI_KBN.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = false;

                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                    if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                    {
                        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = ConstCls.ZEI_KEISAN_KBN_DENPYOU.ToString();
                    }

                    this.form.SHIHARAI_JOUHOU1.Enabled = false;
                    this.form.SHIHARAI_JOUHOU1.Text = string.Empty;
                    this.form.SHIHARAI_JOUHOU2.Enabled = false;
                    this.form.SHIHARAI_JOUHOU2.Text = string.Empty;
                    this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_NAME2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_NAME2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_POST.Enabled = false;
                    this.form.SHIHARAI_SOUFU_POST.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = false;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = false;
                    this.form.SHIHARAI_SOUFU_BUSHO.Enabled = false;
                    this.form.SHIHARAI_SOUFU_BUSHO.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_TANTOU.Enabled = false;
                    this.form.SHIHARAI_SOUFU_TANTOU.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_TEL.Enabled = false;
                    this.form.SHIHARAI_SOUFU_TEL.Text = string.Empty;
                    this.form.SHIHARAI_SOUFU_FAX.Enabled = false;
                    this.form.SHIHARAI_SOUFU_FAX.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_NAME.Enabled = false;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
                    //160026 S
                    this.form.FURIKOMI_EXPORT_KBN.Enabled = false;
                    this.form.FURIKOMI_EXPORT_KBN.Text = string.Empty;
                    this.form.FURIKOMI_EXPORT_KBN_1.Enabled = false;
                    this.form.FURIKOMI_EXPORT_KBN_2.Enabled = false;

                    this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                    this.form.TEI_SUU_RYOU_KBN.Enabled = false;
                    this.form.TEI_SUU_RYOU_KBN.Text = string.Empty;
                    this.form.TEI_SUU_RYOU_KBN_1.Enabled = false;
                    this.form.TEI_SUU_RYOU_KBN_2.Enabled = false;

                    this.form.FURI_KOMI_MOTO_BANK_CD.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_POPUP.Enabled = false;
                    this.form.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                    //160026 E
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI1.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI2.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI3.Enabled = true;
                    this.form.SHIHARAI_SHIMEBI1.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI1.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI1.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI1.ToString();
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIHARAI_SHIMEBI2.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI2.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI2.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI2.ToString();
                        this.form.SHIHARAI_SHIMEBI3.Enabled = true;
                    }
                    else
                    {
                        this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                    }
                    this.form.SHIHARAI_SHIMEBI3.SelectedIndex = -1;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI3.IsNull)
                    {
                        this.form.SHIHARAI_SHIMEBI3.SelectedItem = this.entitysM_SYS_INFO.SHIHARAI_SHIMEBI3.ToString();
                    }
                    //160026 S
                    this.form.SHIHARAI_BETSU_KBN.Text = string.Empty;
                    this.form.SHIHARAI_BETSU_KBN.Enabled = true;
                    this.form.SHIHARAI_BETSU_KBN_1.Enabled = true;
                    this.form.SHIHARAI_BETSU_KBN_2.Enabled = true;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.IsNull)
                    {
                        this.form.SHIHARAI_BETSU_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_KBN.ToString();
                    }
                    //this.form.SHIHARAI_MONTH.Enabled = true;
                    //this.form.SHIHARAI_MONTH_1.Enabled = true;
                    //this.form.SHIHARAI_MONTH_2.Enabled = true;
                    //this.form.SHIHARAI_MONTH_3.Enabled = true;
                    //this.form.SHIHARAI_MONTH_4.Enabled = true;
                    //this.form.SHIHARAI_MONTH_5.Enabled = true;
                    //this.form.SHIHARAI_MONTH_6.Enabled = true;
                    //this.form.SHIHARAI_MONTH_7.Enabled = true;
                    //this.form.SHIHARAI_MONTH.Text = string.Empty;
                    //if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_MONTH.IsNull)
                    //{
                    //    this.form.SHIHARAI_MONTH.Text = this.entitysM_SYS_INFO.SHIHARAI_MONTH.ToString();
                    //}
                    //160026 E
                    this.form.SHIHARAI_DAY.Enabled = true;
                    this.form.SHIHARAI_DAY.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_DAY.IsNull)
                    {
                        this.form.SHIHARAI_DAY.Text = this.entitysM_SYS_INFO.SHIHARAI_DAY.ToString();
                    }
                    this.form.SHIHARAI_HOUHOU.Enabled = true;
                    this.form.SHIHARAI_HOUHOU_SEARCH.Enabled = true;
                    this.form.SHIHARAI_HOUHOU.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.form.SHIHARAI_HOUHOU.Text = string.Format("{0:D" + this.form.SHIHARAI_HOUHOU.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.ToString()));
                    }
                    this.form.SHIHARAI_HOUHOU_NAME.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_HOUHOU.IsNull)
                    {
                        this.entitysM_NYUUSHUKKIN_KBN = this.daoIM_NYUUSHUKKIN_KBN.GetDataByCd((int)this.entitysM_SYS_INFO.SHIHARAI_HOUHOU);
                        if (this.entitysM_NYUUSHUKKIN_KBN != null)
                        {
                            this.form.SHIHARAI_HOUHOU_NAME.Text = this.entitysM_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU;
                        }
                    }
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Enabled = true;
                    this.form.KAISHI_KAIKAKE_ZANDAKA.Text = "0";
                    this.form.SHIHARAI_SHOSHIKI_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_KBN.ToString();
                    }
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_MEISAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHOSHIKI_MEISAI_KBN.ToString();
                    }
                    this.form.SHIHARAI_KEITAI_KBN.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KEITAI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KEITAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KEITAI_KBN.ToString();
                    }
                    this.form.SHUKKIN_MEISAI_KBN.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_2.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_SHUKKIN_MEISAI_KBN.IsNull)
                    {
                        this.form.SHUKKIN_MEISAI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_SHUKKIN_MEISAI_KBN.ToString();
                    }
                    this.form.SHIHARAI_YOUSHI_KBN.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_YOUSHI_KBN.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_YOUSHI_KBN.IsNull)
                    {
                        this.form.SHIHARAI_YOUSHI_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_YOUSHI_KBN.ToString();
                    }
                    //var characterLimitListIndex = Array.IndexOf(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList, '2');
                    //if (characterLimitListIndex < 0)
                    //{
                    //    List<char> list = new List<char>(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList);
                    //    list.Insert(0, '2');
                    //    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = list.ToArray();
                    //}
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = true;

                    this.form.SHIHARAI_JOUHOU1.Enabled = true;
                    this.form.SHIHARAI_JOUHOU2.Enabled = true;
                    this.form.TORIHIKISAKI_COPY_SIHARAI_BUTTON.Enabled = true;
                    this.form.SHIHARAI_SOUFU_NAME1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_KEISHOU1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_NAME2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_KEISHOU2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_POST.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS_SEARCH.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS1.Enabled = true;
                    this.form.SHIHARAI_SOUFU_ADDRESS2.Enabled = true;
                    this.form.SHIHARAI_SOUFU_POST_SEARCH.Enabled = true;
                    this.form.SHIHARAI_SOUFU_BUSHO.Enabled = true;
                    this.form.SHIHARAI_SOUFU_TANTOU.Enabled = true;
                    this.form.SHIHARAI_SOUFU_TEL.Enabled = true;
                    this.form.SHIHARAI_SOUFU_FAX.Enabled = true;

                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = string.Empty;
                    if (!this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.IsNull)
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_PRINT_KBN.ToString();
                    }
                    if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text == "1")
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Checked = true;
                    }
                    else
                    {
                        this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Checked = true;
                    }
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_1.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN_2.Enabled = true;

                    this.form.SHIHARAI_KYOTEN_CD.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                    if (this.entitysM_SYS_INFO != null && !this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
                    }

                    this.form.SHIHARAI_KYOTEN_NAME.Enabled = true;
                    this.form.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    M_KYOTEN shiharaiKyoten = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
                    if (shiharaiKyoten != null)
                    {
                        this.form.SHIHARAI_KYOTEN_NAME.Text = shiharaiKyoten.KYOTEN_NAME_RYAKU;
                    }

                    this.form.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
                    //160026 S
                    this.form.FURIKOMI_EXPORT_KBN.Enabled = true;
                    this.form.FURIKOMI_EXPORT_KBN.Text = "2";
                    this.form.FURIKOMI_EXPORT_KBN_2.Checked = true;
                    this.form.FURIKOMI_EXPORT_KBN_1.Enabled = true;
                    this.form.FURIKOMI_EXPORT_KBN_2.Enabled = true;

                    this.form.FURIKOMI_EXPORT_KBN_TextChanged(null, null);
                    //160026 E
                    this.ChangeShiharaiKyotenPrintKbn();

                    // [2.掛け]に変更した場合、以下を行う
                    // 支払書式明細区分の制限処理
                    this.LimitShiharaiShoshikiMeisaiKbn();
                    this.LimitShiharaiZeiKbn();

                    // 項目値を変更するとフォーカスが移動してしまうので戻す
                    this.form.SHIHARAI_TORIHIKI_KBN_CD.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChangeSiharaiTorihikiKbn", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSiharaiTorihikiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 残高項目のフォーマット処理
        /// </summary>
        public bool SetZandakaFormat(string zan, CustomNumericTextBox2 target)
        {
            try
            {
                LogUtility.DebugMethodStart(zan, target);

                if (string.IsNullOrWhiteSpace(zan))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // マイナスが先頭以外に付与されていたら削除する
                var minus = zan.StartsWith("-");
                zan = (minus ? "-" : string.Empty) + zan.Replace("-", string.Empty).Replace(",", string.Empty);

                target.Text = string.Format("{0:#,##0}", Decimal.Parse(zan));
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZandakaFormat", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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

                if (this.form.MANI_HENSOUSAKI_KBN.Checked)
                {
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
                        this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                        this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                        this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                        this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                        this.form.MANI_HENSHOUSAKI_ADDRESS_SEARCH.Enabled = true;
                        this.form.MANI_HENSHOUSAKI_POST_SEARCH.Enabled = true;
                        this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.torihikisakiCD))
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
                    this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = false;
                    this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = false;
                    this.form.MANI_HENSOUSAKI_BUSHO.Enabled = false;
                    this.form.MANI_HENSOUSAKI_TANTOU.Enabled = false;
                    this.form.MANI_HENSHOUSAKI_ADDRESS_SEARCH.Enabled = false;
                    this.form.MANI_HENSHOUSAKI_POST_SEARCH.Enabled = false;
                    this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = false;
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
        /// 一覧画面表示処理
        /// </summary>
        internal void ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M484", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.HIKIAI_TORIHIKISAKI);
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
        /// 銀行情報設定処理
        /// </summary>
        public void SetBankInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrWhiteSpace(this.form.FURIKOMI_BANK_CD.Text) && !string.IsNullOrWhiteSpace(this.form.FURIKOMI_BANK_SHITEN_CD.Text))
                {
                    M_BANK_SHITEN searchParams = new M_BANK_SHITEN();
                    searchParams.BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text.PadLeft((int)this.form.FURIKOMI_BANK_SHITEN_CD.CharactersNumber, '0');
                    M_BANK_SHITEN[] bankShiten = this.daoIM_BANK_SHITEN.GetAllValidData(searchParams);
                    if (bankShiten != null && bankShiten.Length == 1)
                    {
                        M_BANK bank = this.daoIM_BANK.GetDataByCd(bankShiten[0].BANK_CD);
                        if (bank != null)
                        {
                            this.form.FURIKOMI_BANK_CD.Text = bank.BANK_CD;
                            this.form.FURIKOMI_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBankInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧検索
        /// </summary>
        public bool TorihikiStopIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.rowCntGyousha = this.SearchGyousha();
                if (this.rowCntGyousha == 0)
                {
                    this.form.GYOUSHA_ICHIRAN.DataSource = null;
                    this.form.GYOUSHA_ICHIRAN.Rows.Clear();
                    return true;
                }

                this.SetIchiranGyousha();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TorihikiStopIchiran", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikiStopIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <returns></returns>
        public int SearchGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultGyousha = new DataTable();

                string torihikisakiCd = this.entitysTORIHIKISAKI.TORIHIKISAKI_CD;
                if (string.IsNullOrWhiteSpace(torihikisakiCd))
                {
                    return 0;
                }

                M_HIKIAI_GYOUSHA condition = new M_HIKIAI_GYOUSHA();
                condition.TORIHIKISAKI_CD = torihikisakiCd;

                this.SearchResultGyousha = this.daoTORIHIKISAKI.GetIchiranHikiaiGyoushaData(condition);

                int count = this.SearchResultGyousha.Rows.Count;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索結果を業者一覧に設定
        /// </summary>
        internal void SetIchiranGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = false;
                var table = this.SearchResultGyousha;
                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                this.form.GYOUSHA_ICHIRAN.DataSource = table;
                this.form.GYOUSHA_ICHIRAN.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 引合業者入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        internal void ShowGyoushaWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //選択行からキー項目を取得する
                string cd1 = string.Empty;
                foreach (Row row in this.form.GYOUSHA_ICHIRAN.Rows)
                {
                    if (row.Selected)
                    {
                        cd1 = row.Cells["GYOUSHA_CD"].Value.ToString();
                        break;
                    }
                }

                //引合業者入力画面を表示する
                if (Manager.CheckAuthority("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FormManager.OpenFormWithAuth("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, cd1);
                }
                else if (Manager.CheckAuthority("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    FormManager.OpenFormWithAuth("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1);
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                //FormManager.OpenFormWithAuth("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, cd1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求書式明細区分の制限処理
        /// </summary>
        internal bool LimitSeikyuuShoshikiMeisaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (ConstCls.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    // 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = ConstCls.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.NYUUKIN_MEISAI_KBN.Text = ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;
                }
                else if (ConstCls.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    // 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = ConstCls.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.NYUUKIN_MEISAI_KBN.Text = ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = false;
                }
                else
                {
                    // その他
                    // 入力制限
                    //this.form.SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU,
                    //    ConstCls.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SEIKYUU_KEITAI_KBN_2.Enabled = true;
                    this.form.NYUUKIN_MEISAI_KBN_1.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                LogicCls.ClearText_NotAllowedInput(this.form.SHOSHIKI_MEISAI_KBN);
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 支払書式明細区分の制限処理
        /// </summary>
        internal bool LimitShiharaiShoshikiMeisaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (ConstCls.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = ConstCls.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHUKKIN_MEISAI_KBN.Text = ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;
                }
                else if (ConstCls.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = ConstCls.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHUKKIN_MEISAI_KBN.Text = ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = false;
                }
                else
                {
                    // その他
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_NONE,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //    ConstCls.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.SEIKYU_KEITAI_KBN_TANGETSU,
                    //    ConstCls.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //    ConstCls.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.SHIHARAI_KEITAI_KBN_2.Enabled = true;
                    this.form.SHUKKIN_MEISAI_KBN_1.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                LogicCls.ClearText_NotAllowedInput(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN);
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 請求税区分の制限処理
        /// </summary>
        internal bool LimitSeikyuuZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (ConstCls.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text) ||
                    ConstCls.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    // 入力制限
                    //this.form.ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.ZEI_KBN_SOTO,
                    //    ConstCls.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.ZEI_KBN_CD_2.Checked)
                    {
                        this.form.ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.ZEI_KBN_CD_1.Enabled = true;
                    this.form.ZEI_KBN_CD_2.Enabled = false;
                    this.form.ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    // 入力制限
                    //this.form.ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.ZEI_KBN_SOTO,
                    //    ConstCls.ZEI_KBN_UCHI,
                    //    ConstCls.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.ZEI_KBN_CD_1.Enabled = true;
                    this.form.ZEI_KBN_CD_2.Enabled = true;
                    this.form.ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                LogicCls.ClearText_NotAllowedInput(this.form.ZEI_KBN_CD);
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 支払税区分の制限処理
        /// </summary>
        internal bool LimitShiharaiZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (ConstCls.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text) ||
                    ConstCls.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    // 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.ZEI_KBN_SOTO,
                    //    ConstCls.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.SHIHARAI_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.SHIHARAI_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = false;
                    this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    // 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //    ConstCls.ZEI_KBN_SOTO,
                    //    ConstCls.ZEI_KBN_UCHI,
                    //    ConstCls.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_2.Enabled = true;
                    this.form.SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                LogicCls.ClearText_NotAllowedInput(this.form.SHIHARAI_ZEI_KBN_CD);
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 許容されていない入力の場合、テキストをクリアする
        /// </summary>
        /// <param name="textBox">数値入力テキストボックス</param>
        /// <param name="limitList">制限文字リスト</param>
        private static void ClearText_NotAllowedInput(CustomNumericTextBox2 textBox)
        {
            try
            {
                LogUtility.DebugMethodStart(textBox);

                if (textBox.LinkedRadioButtonArray.Length != 0)
                {
                    var allowed = false;

                    // ラジオボタンリンク処理
                    ControlUtility controlUtil = new ControlUtility();
                    controlUtil.ControlCollection = textBox.FindForm().Controls;
                    foreach (var radioButtonName in textBox.LinkedRadioButtonArray)
                    {
                        var radioButton = controlUtil.GetSettingField(radioButtonName) as CustomRadioButton;
                        if (radioButton != null && radioButton.Enabled)
                        {
                            allowed = true;
                            break;
                        }
                    }

                    // 許容外の場合、テキストをクリアする
                    if (!allowed)
                    {
                        textBox.Text = ConstCls.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearText_NotAllowedInput", ex);
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
                    if (!this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()) && this.form.SEIKYUU_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定データの読み込み
                        this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SEIKYUU_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SEIKYUU_KYOTEN_CD.ToString()));
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
                this.form.errmessage.MessageBoxShow("E245", "");
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
                    if (!this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.IsNull && !string.IsNullOrWhiteSpace(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()) && this.form.SHIHARAI_KYOTEN_CD.Text.Equals(string.Empty))
                    {
                        //システム設定値の読み取り
                        this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D" + this.form.SHIHARAI_KYOTEN_CD.CharactersNumber + "}", Int16.Parse(this.entitysM_SYS_INFO.SHIHARAI_KYOTEN_CD.ToString()));
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
                this.form.errmessage.MessageBoxShow("E245", "");
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
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SEIKYUU_KYOTEN_CD.Text);
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
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                    M_KYOTEN kyo = this.daoIM_KYOTEN.GetDataByCd(this.form.SHIHARAI_KYOTEN_CD.Text);
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
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 部署の値チェック
        /// </summary>
        public bool BushoCdValidated()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOU_BUSHO_CD.Text))
                {
                    M_BUSHO search = new M_BUSHO();
                    search.BUSHO_CD = this.form.EIGYOU_TANTOU_BUSHO_CD.Text;
                    M_BUSHO[] busho = this.daoIM_BUSHO.GetAllValidData(search);
                    if (busho != null && busho.Length > 0 && !busho[0].BUSHO_CD.Equals("999"))
                    {
                        this.form.EIGYOU_TANTOU_BUSHO_CD.Text = busho[0].BUSHO_CD.ToString();
                        this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = busho[0].BUSHO_NAME_RYAKU.ToString();
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
                    this.form.EIGYOU_TANTOU_BUSHO_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("BushoCdValidated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BushoCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 自社情報入力－会計情報タブに登録されている銀行と銀行支店を
        /// 請求情報2タブの振込銀行、支店の初期値としてセット
        /// </summary>
        public void SetDefaultValueFromJisyaJyouhouToFURIKOMI_BANK()
        {
            var jisyadata = this.daoIM_CORP_INFO.GetAllDataMinCols();
            foreach (var info in jisyadata)
            {
                this.form.FURIKOMI_BANK_CD.Text = info.BANK_CD;
                if (this.form.FURIKOMI_BANK_CD.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD.Text = info.BANK_SHITEN_CD;
                // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
                this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.KOUZA_SHURUI.Text = info.KOUZA_SHURUI;
                this.form.KOUZA_NO.Text = info.KOUZA_NO;
                if (this.form.FURIKOMI_BANK_SHITEN_CD.Text != "")
                {
                    var bankshitenSql =
                        "SELECT BANK_CD, BANK_SHITEN_CD, BANK_SHIETN_NAME_RYAKU FROM M_BANK_SHITEN" +
                        " WHERE BANK_CD = '{0}' AND BANK_SHITEN_CD = '{1}' AND KOUZA_SHURUI = '{2}' AND KOUZA_NO = '{3}';";
                    var localBANK_SHITEN_LIST = this.daoIM_BANK_SHITEN.GetDateForStringSql(
                        string.Format(bankshitenSql, this.form.FURIKOMI_BANK_CD.Text, this.form.FURIKOMI_BANK_SHITEN_CD.Text, this.form.KOUZA_SHURUI.Text, this.form.KOUZA_NO.Text)
                        );
                    if (localBANK_SHITEN_LIST != null)
                    {
                        foreach (DataRow dr in localBANK_SHITEN_LIST.Rows)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }

                this.form.KOUZA_NAME.Text = info.KOUZA_NAME;

                //振込銀行2
                this.form.FURIKOMI_BANK_CD_2.Text = info.BANK_CD_2;
                if (this.form.FURIKOMI_BANK_CD_2.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD_2.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_2.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD_2.Text = info.BANK_SHITEN_CD_2;
                this.form.previousBankShitenCd_2 = this.form.FURIKOMI_BANK_SHITEN_CD_2.Text;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.KOUZA_SHURUI_2.Text = info.KOUZA_SHURUI_2;
                this.form.KOUZA_NO_2.Text = info.KOUZA_NO_2;
                if (this.form.FURIKOMI_BANK_SHITEN_CD_2.Text != "")
                {
                    var bankshitenSql =
                        "SELECT BANK_CD, BANK_SHITEN_CD, BANK_SHIETN_NAME_RYAKU FROM M_BANK_SHITEN" +
                        " WHERE BANK_CD = '{0}' AND BANK_SHITEN_CD = '{1}' AND KOUZA_SHURUI = '{2}' AND KOUZA_NO = '{3}';";
                    var localBANK_SHITEN_LIST = this.daoIM_BANK_SHITEN.GetDateForStringSql(
                        string.Format(bankshitenSql, this.form.FURIKOMI_BANK_CD_2.Text, this.form.FURIKOMI_BANK_SHITEN_CD_2.Text, this.form.KOUZA_SHURUI_2.Text, this.form.KOUZA_NO_2.Text)
                        );
                    if (localBANK_SHITEN_LIST != null)
                    {
                        foreach (DataRow dr in localBANK_SHITEN_LIST.Rows)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME_2.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }

                this.form.KOUZA_NAME_2.Text = info.KOUZA_NAME_2;

                //振込銀行3
                this.form.FURIKOMI_BANK_CD_3.Text = info.BANK_CD_3;
                if (this.form.FURIKOMI_BANK_CD_3.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURIKOMI_BANK_CD_3.Text);
                    if (bank != null)
                    {
                        this.form.FURIKOMI_BANK_NAME_3.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURIKOMI_BANK_SHITEN_CD_3.Text = info.BANK_SHITEN_CD_3;
                this.form.previousBankShitenCd_3 = this.form.FURIKOMI_BANK_SHITEN_CD_3.Text;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.KOUZA_SHURUI_3.Text = info.KOUZA_SHURUI_3;
                this.form.KOUZA_NO_3.Text = info.KOUZA_NO_3;
                if (this.form.FURIKOMI_BANK_SHITEN_CD_3.Text != "")
                {
                    var bankshitenSql =
                        "SELECT BANK_CD, BANK_SHITEN_CD, BANK_SHIETN_NAME_RYAKU FROM M_BANK_SHITEN" +
                        " WHERE BANK_CD = '{0}' AND BANK_SHITEN_CD = '{1}' AND KOUZA_SHURUI = '{2}' AND KOUZA_NO = '{3}';";
                    var localBANK_SHITEN_LIST = this.daoIM_BANK_SHITEN.GetDateForStringSql(
                        string.Format(bankshitenSql, this.form.FURIKOMI_BANK_CD_3.Text, this.form.FURIKOMI_BANK_SHITEN_CD_3.Text, this.form.KOUZA_SHURUI_3.Text, this.form.KOUZA_NO_3.Text)
                        );
                    if (localBANK_SHITEN_LIST != null)
                    {
                        foreach (DataRow dr in localBANK_SHITEN_LIST.Rows)
                        {
                            this.form.FURIKOMI_BANK_SHITEN_NAME_3.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }

                this.form.KOUZA_NAME_3.Text = info.KOUZA_NAME_3;
            }
        }

        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
        /// <summary>
        /// 銀行支店チェック
        /// </summary>
        /// <param name="BANK_CD">BANK_CD</param>
        /// <param name="SHITEN_CD">SHITEN_CD</param>
        public M_BANK_SHITEN[] ShitenCdValidated(string BANK_CD, string SHITEN_CD, out bool catchErr)
        {
            catchErr = true;
            try
            {
                M_BANK_SHITEN entity = new M_BANK_SHITEN();
                entity.BANK_CD = BANK_CD;
                entity.BANK_SHITEN_CD = SHITEN_CD;
                M_BANK_SHITEN[] ret = daoIM_BANK_SHITEN.GetAllValidData(entity);

                return ret;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShitenCdValidated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShitenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }

        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏

        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　start
        /// <summary>
        /// 移行後引合業者のデータを更新処理
        /// </summary>
        [Transaction]
        public virtual void UpdateHikiaiGyousha(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            this.daoTORIHIKISAKI.UpdateGYOUSHA_CD(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 移行後現場業者のデータを更新処理
        /// </summary
        [Transaction]
        public virtual void UpdateHikiaiGenba(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD)
        {
            LogUtility.DebugMethodStart(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            this.daoTORIHIKISAKI.UpdateGenba_CD(oldTORIHIKISAKI_CD, newTORIHIKISAKI_CD);

            LogUtility.DebugMethodEnd();
        }

        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　end

        /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　start

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

                M_HIKIAI_TORIHIKISAKI data = new M_HIKIAI_TORIHIKISAKI();
                data.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                // 業者、現場の適用開始日<取引先の適用開始日
                DataTable begin = this.daoTORIHIKISAKI.GetTekiyouBegin(data);
                DateTime date;
                if (begin != null && begin.Rows.Count > 0)
                {
                    if (DateTime.TryParse(Convert.ToString(begin.Rows[0][0]), out date) && date.CompareTo(date_from) < 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用開始日", "取引先", "業者及び現場", "後", "以前");
                        this.form.TEKIYOU_BEGIN.Focus();
                        return true;
                    }
                }
                // 業者、現場の適用終了日>取引先の適用終了日
                DataTable end = this.daoTORIHIKISAKI.GetTekiyouEnd(data);
                if (end != null && begin.Rows.Count > 0)
                {
                    if (DateTime.TryParse(Convert.ToString(end.Rows[0][0]), out date) && date.CompareTo(date_to) > 0)
                    {
                        msgLogic.MessageBoxShow("E255", "適用終了日", "取引先", "業者及び現場", "前", "以降");
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
                    string[] errorMsg = { "適用期間From", "適用期間To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.TEKIYOU_BEGIN.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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

        /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　end
        ///
        /// 20141226 Houkakou 「引合取引先入力」のダブルクリックを追加する　start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TEKIYOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.TEKIYOU_BEGIN;
            var ToTextBox = this.form.TEKIYOU_END;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// 20141226 Houkakou 「引合取引先入力」のダブルクリックを追加する　end

        /// <summary>
        /// 返送先情報区分変更後処理
        /// </summary>
        public void ChangeManiHensousakiAddKbn()
        {
            LogUtility.DebugMethodStart();

            if (this.form.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text.Equals("1"))
            {
                this.form.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
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
                this.form.MANI_HENSHOUSAKI_ADDRESS_SEARCH.Enabled = false;
                this.form.MANI_HENSHOUSAKI_POST_SEARCH.Enabled = false;
                this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = false;
            }
            else
            {
                this.form.MANI_HENSOUSAKI_NAME1.Enabled = true;
                this.form.MANI_HENSOUSAKI_NAME2.Enabled = true;
                this.form.MANI_HENSOUSAKI_KEISHOU1.Enabled = true;
                this.form.MANI_HENSOUSAKI_KEISHOU2.Enabled = true;
                this.form.MANI_HENSOUSAKI_POST.Enabled = true;
                this.form.MANI_HENSOUSAKI_ADDRESS1.Enabled = true;
                this.form.MANI_HENSOUSAKI_ADDRESS2.Enabled = true;
                this.form.MANI_HENSOUSAKI_BUSHO.Enabled = true;
                this.form.MANI_HENSOUSAKI_TANTOU.Enabled = true;
                this.form.MANI_HENSHOUSAKI_ADDRESS_SEARCH.Enabled = true;
                this.form.MANI_HENSHOUSAKI_POST_SEARCH.Enabled = true;
                this.form.TORIHIKISAKI_COPY_MANI_BUTTON.Enabled = true;
            }

            LogUtility.DebugMethodEnd();
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        private bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;

            if (!string.IsNullOrEmpty(torihikisakiCd))
            {
                DataTable dtTable = this.daoTORIHIKISAKI.GetDataBySqlFileCheck(torihikisakiCd);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += Environment.NewLine + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "引合取引先", "引合取引先CD", strName);

                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 銀行支店リストを取得します
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店リスト</returns>
        internal List<M_BANK_SHITEN> GetBankShiten(string bankCd, string bankShitenCd, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(bankCd, bankShitenCd);

                catchErr = false;
                var bankShitenList = this.daoIM_BANK_SHITEN.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd }).ToList();

                LogUtility.DebugMethodEnd(bankShitenList, catchErr);

                return bankShitenList;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                var bankShitenList = new List<M_BANK_SHITEN>();
                LogUtility.Error("GetBankShiten", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(bankShitenList, catchErr);
                return bankShitenList;
            }
            catch (Exception ex)
            {
                catchErr = true;
                var bankShitenList = new List<M_BANK_SHITEN>();
                LogUtility.Error("GetBankShiten", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(bankShitenList, catchErr);
                return bankShitenList;
            }
        }

        /// <summary>
        /// 出力区分変更後処理
        /// </summary>
        public bool ChangeOutputKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //1．紙　2:電子CSV
                if (this.form.OUTPUT_KBN.Text.Equals("1"))
                {
                    this.form.HAKKOUSAKI_CD.Enabled = false;
                    this.form.HAKKOUSAKI_CD.Text = string.Empty;
                }
                else
                {
                    this.form.HAKKOUSAKI_CD.Enabled = true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeOutputKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                this.form.labelOutputKbn.Visible = densiVisible;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = densiVisible;
                this.form.labelHakkosaki.Visible = densiVisible;
                this.form.HAKKOUSAKI_CD.Visible = densiVisible;
                this.form.label214.Location = new System.Drawing.Point(this.form.label214.Location.X, this.form.label214.Location.Y - 44);
                this.form.panel11.Location = new System.Drawing.Point(this.form.panel11.Location.X, this.form.panel11.Location.Y - 44);
                this.form.label215.Location = new System.Drawing.Point(this.form.label215.Location.X, this.form.label215.Location.Y - 44);
                this.form.panel12.Location = new System.Drawing.Point(this.form.panel12.Location.X, this.form.panel12.Location.Y - 44);

            }
            return densiVisible;
        }

        /// <summary>
        /// 画面起動時にオプション有無を確認し、オンラインバンク連携で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        /// <returns></returns>
        private bool setOnlineBankVisible()
        {
            bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();
            if (!onlineBankVisible)
            {
                this.form.FURIKOMI_NAME1.Visible = onlineBankVisible;
                this.form.FURIKOMI_NAME2.Visible = onlineBankVisible;
                this.form.label15.Visible = onlineBankVisible;
                this.form.label16.Visible = onlineBankVisible;
            }
            return onlineBankVisible;
        }
        //160026 S
        /// <summary>
        /// 振込元銀行変更後処理
        /// </summary>
        public bool FurikomiMotoBankCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.FURI_KOMI_MOTO_BANK_CD.Text))
                {
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.form.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                }

                this.form.previousBankShitenMotoCd = this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiMotoBankCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        public void SetDefaultValueFromFURIKOMI_BANK_MOTO()
        {
            var jisyadata = this.daoIM_CORP_INFO.GetAllData();
            foreach (var info in jisyadata)
            {
                this.form.FURI_KOMI_MOTO_BANK_CD.Text = info.FURIKOMI_MOTO_BANK_CD;
                if (this.form.FURI_KOMI_MOTO_BANK_CD.Text != "")
                {
                    var bank = this.daoIM_BANK.GetDataByCd(this.form.FURI_KOMI_MOTO_BANK_CD.Text);
                    if (bank != null)
                    {
                        this.form.FURI_KOMI_MOTO_BANK_NAME.Text = bank.BANK_NAME_RYAKU;
                    }
                }

                this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = info.FURIKOMI_MOTO_BANK_SHITEN_CD;
                // 取引先マスタと自社情報マスタの銀行支店名が同一となるようにする。
                this.form.FURI_KOMI_MOTO_SHURUI.Text = info.FURIKOMI_MOTO_KOUZA_SHURUI;
                this.form.FURI_KOMI_MOTO_NO.Text = info.FURIKOMI_MOTO_KOUZA_NO;
                if (this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text != "")
                {
                    var bankshiten = this.daoIM_BANK_SHITEN.GetDateForStringSql("SELECT * FROM M_BANK_SHITEN " +
                                                                                " where BANK_CD =  " + this.form.FURI_KOMI_MOTO_BANK_CD.Text +
                                                                                " and BANK_SHITEN_CD = " + this.form.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text +
                                                                                " and KOUZA_SHURUI = " + "'" + this.form.FURI_KOMI_MOTO_SHURUI.Text + "'" +
                                                                                " and KOUZA_NO = " + this.form.FURI_KOMI_MOTO_NO.Text);
                    if (bankshiten != null)
                    {
                        foreach (DataRow dr in bankshiten.Rows)
                        {
                            Console.Write(dr["BANK_SHIETN_NAME_RYAKU"].ToString());

                            this.form.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = dr["BANK_SHIETN_NAME_RYAKU"].ToString();
                            break;
                        }
                    }
                }
                this.form.FURI_KOMI_MOTO_NAME.Text = info.FURIKOMI_MOTO_KOUZA_NAME;
            }
        }
        //160026 E
    }
}