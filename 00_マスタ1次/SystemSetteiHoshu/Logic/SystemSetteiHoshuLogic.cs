// $Id: SystemSetteiHoshuLogic.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MasterCommon.Logic;
using MasterCommon.Xml;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using SystemSetteiHoshu.APP;
using SystemSetteiHoshu.Const;
using r_framework.Configuration;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using System.IO;
using System.Text;
using Shougun.Core.ExternalConnection.FileUpload;

namespace SystemSetteiHoshu.Logic
{
    /// <summary>
    /// システム設定入力画面のビジネスロジック
    /// </summary>
    public class SystemSetteiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "SystemSetteiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_SYSINFO_DATA_SQL = "SystemSetteiHoshu.Sql.GetSysInfoDataSql.sql";

        private readonly string GET_KYOTEN_DATA_SQL = "SystemSetteiHoshu.Sql.GetKyotenDataSql.sql";

        private readonly string GET_DEFAULTMARGINSETTINGS_DATA_SQL = "SystemSetteiHoshu.Sql.GetDefaultMarginSettingsDataSql.sql";

        string filePath = null;
        /// <summary>
        /// システム設定入力画面Form
        /// </summary>
        private SystemSetteiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao dao;

        // No12509 システム設定変更履歴出力機能-->
        private IM_SYS_INFO_CHANGE_LOGDao daoChangeLog;
        // No12509 システム設定変更履歴出力機能<--

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 形態区分のDao
        /// </summary>
        private IM_KEITAI_KBNDao daoKeitaiKbn;

        /// <summary>
        /// 帳票余白設定Dao
        /// </summary>
        private IS_DEFAULTMARGINSETTINGSDao daoDefaultMarginSettings;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO insEntitys;

        // No12509 システム設定変更履歴出力機能-->
        private List<M_SYS_INFO_CHANGE_LOG> changeLogEntitys = new List<M_SYS_INFO_CHANGE_LOG>();
        // No12509 システム設定変更履歴出力機能<--

        /// <summary>
        /// 帳票余白設定のエンティティ
        /// </summary>
        private DataTable defaultMarginSettings;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// ファイルアップロード共通処理クラス
        /// </summary>
        public FileUploadLogic uploadLogic;
        
        /// <summary>
        /// システム設定入力ファイル連携Dao
        /// </summary>
        private IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao;

        /// <summary>
        /// ファイルデータDao
        /// </summary>
        private FILE_DATADAO fileDataDao;

        //20250305
        private IM_BANK_SHITENDao daoIM_BANK_SHITEN;

        /// <summary>
        /// ファイルデータ
        /// </summary>
        T_FILE_DATA fileData;

        /// <summary>
        /// システム設定入力ファイル連携データ
        /// </summary>
        M_FILE_LINK_SYS_INFO fileLink;


        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public SystemSetteiHoshuLogic(SystemSetteiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            // No12509 システム設定変更履歴出力機能-->
            this.daoChangeLog = DaoInitUtility.GetComponent<IM_SYS_INFO_CHANGE_LOGDao>();
            // No12509 システム設定変更履歴出力機能<--
            this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoKeitaiKbn = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>();
            this.daoDefaultMarginSettings = DaoInitUtility.GetComponent<IS_DEFAULTMARGINSETTINGSDao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.uploadLogic = new FileUploadLogic();
            this.fileLink = new M_FILE_LINK_SYS_INFO();
            this.fileDataDao = DaoInitUtility.GetComponent<FILE_DATADAO>();
            this.fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();

            //20250305
            this.daoIM_BANK_SHITEN = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                findForm.ProcessButtonPanel.Visible = false;

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
                this.setDensiSeikyushoVisible();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end

                //#160058 start
                SetDensiSeikyushoAndRakurakuVisible();
                //#160058 end

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                //モバイルオプション
                MobileInit();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        internal bool DispReferenceMode()
        {
            try
            {
                // MainForm
                foreach (Control ctr in this.form.main_tab.Controls)
                {
                    this.SetReference(ctr);
                }

                // ReadOnlyになっているCDでスペースを押下すると、検索ポップアップが起動する問題が発生。
                // r_frameworkを使っているため発生してる。
                // 以下に暫定対策で顧客から指摘があった項目についてEnabledで制御をし
                // 検索ポップアップが起動しないように修正
                this.SetEnabled();

                // FunctionButton
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func9.Enabled = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DispReferenceMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 画面の各コントロールを参照モード用に設定します
        /// </summary>
        /// <param name="control"></param>
        private void SetReference(Control control)
        {
            if (control is System.Windows.Forms.Panel ||
                control is System.Windows.Forms.TabControl ||
                control is System.Windows.Forms.GroupBox)
            {
                // 再帰
                foreach (Control ctr in control.Controls)
                {
                    this.SetReference(ctr);
                }
            }
            else
            {
                // ラベルは設定しない
                if (control is System.Windows.Forms.Label) return;

                // ReadOnly優先で設定
                PropertyInfo ctl_property_readonly = control.GetType().GetProperty("ReadOnly");
                PropertyInfo ctl_property_enabled = control.GetType().GetProperty("Enabled");
                if (ctl_property_readonly != null)
                {
                    ctl_property_readonly.SetValue(control, true, null);
                }
                else if (ctl_property_enabled != null)
                {
                    ctl_property_enabled.SetValue(control, false, null);
                }
            }
        }

        #region 参照モードで検索ポップアップが表示される問題の暫定対応

        private string[] enabledControls = new string[] { "SEIKYUU_KAISHUU_HOUHOU", "SHIHARAI_HOUHOU", "GENBA_MANIFEST_SHURUI_CD"
            , "GENBA_MANIFEST_TEHAI_CD", "HINMEI_UNIT_CD", "HINMEI_DENSHU_KBN_CD", "HINMEI_DENPYOU_KBN_CD", "HINMEI_ZEI_KBN_CD", "MANI_KANSAN_KIHON_UNIT_CD"
            , "MANI_KANSAN_UNIT_CD", "UKETSUKE_UPN_GYOUSHA_CD_DEFALUT", "INJI_KYOTEN_CD1", "INJI_KYOTEN_CD2"
            , "KYOTEN_CD", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GENBA_CD", "NITSUMI_GYOUSHA_CD", "NITSUMI_GENBA_CD", "KEIRYOU_UKEIRE_KEIRYOU_KBN_CD", "KEIRYOU_SHUKKA_KEIRYOU_KBN_CD", "GENBA_TEIKI_KEIYAKU_KBN", "GENBA_TEIKI_KEIJYOU_KBN" };

        /// <summary>
        /// 画面の各コントロールを参照モード用に設定します
        /// </summary>
        private void SetEnabled()
        {
            var ctrlUtil = new ControlUtility();
            var targetCtrl = ctrlUtil.FindControl(this.allControl, enabledControls);
            if (targetCtrl == null || targetCtrl.Count() < 0)
            {
                return;
            }

            foreach (var obj in targetCtrl)
            {
                PropertyUtility.SetValue(obj, "Enabled", false);
            }
        }

        #endregion

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResult = dao.GetDataBySqlFile(this.GET_SYSINFO_DATA_SQL, new M_SYS_INFO());

                this.defaultMarginSettings = this.daoDefaultMarginSettings.GetDataBySqlFile(GET_DEFAULTMARGINSETTINGS_DATA_SQL, new S_DEFAULTMARGINSETTINGS());

                int count = this.SearchResult.Rows == null ? 0 : 1;
                
                // システム設定入力データからファイルIDを取得する。
                fileLink = fileLinkSysInfoDao.GetDataById("0");

                if (fileLink != null)
                {   
                    // ファイルIDからファイル情報を取得
                    long fileId =  (long)fileLink.FILE_ID;
                    fileData = this.fileDataDao.GetDataByKey(fileId);
                }

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
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

                this.insEntitys = new M_SYS_INFO();

                // キー
                this.insEntitys.SYS_ID = 0;

                // システムタブ
                this.insEntitys.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                this.insEntitys.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                this.insEntitys.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                if (this.form.ICHIRAN_ALERT_KENSUU.Text.Length > 0)
                {
                    // 20151112 アラート件数は"#,##0"フォーマットので、Valueを利用する Start
                    this.insEntitys.ICHIRAN_ALERT_KENSUU = Convert.ToInt32(this.form.ICHIRAN_ALERT_KENSUU.GetResultText());
                    // 20151112 アラート件数は"#,##0"フォーマットので、Valueを利用する End
                }

                if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.SYS_ZEI_KEISAN_KBN_USE_KBN = Convert.ToInt16(this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text);
                }

                if (this.form.SYS_JYURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_JYURYOU_FORMAT_CD = Convert.ToInt16(this.form.SYS_JYURYOU_FORMAT_CD.Text);
                }

                if (this.form.SYS_JYURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_JYURYOU_FORMAT = changeFormat(this.form.SYS_JYURYOU_FORMAT_CD.Text);
                }

                if (this.form.SYS_KAKUTEI__TANNI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SYS_KAKUTEI__TANNI_KBN = Convert.ToInt16(this.form.SYS_KAKUTEI__TANNI_KBN.Text);
                }

                if (this.form.SYS_MANI_KEITAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SYS_MANI_KEITAI_KBN = Convert.ToInt16(this.form.SYS_MANI_KEITAI_KBN.Text);
                }

                if (this.form.SYS_RENBAN_HOUHOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.SYS_RENBAN_HOUHOU_KBN = Convert.ToInt16(this.form.SYS_RENBAN_HOUHOU_KBN.Text);
                }

                if (!string.IsNullOrEmpty(this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text))
                {
                    this.insEntitys.SYS_RECEIPT_RENBAN_HOUHOU_KBN = Convert.ToInt16(this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text);
                }

                if (this.form.SYS_SUURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_SUURYOU_FORMAT_CD = Convert.ToInt16(this.form.SYS_SUURYOU_FORMAT_CD.Text);
                }

                if (this.form.SYS_SUURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_SUURYOU_FORMAT = changeFormat(this.form.SYS_SUURYOU_FORMAT_CD.Text);
                }

                if (this.form.SYS_TANKA_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_TANKA_FORMAT_CD = Convert.ToInt16(this.form.SYS_TANKA_FORMAT_CD.Text);
                }

                if (this.form.SYS_TANKA_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.SYS_TANKA_FORMAT = changeFormat(this.form.SYS_TANKA_FORMAT_CD.Text);
                }

                if (this.form.SYS_PWD_SAVE_KBN.Text.Length > 0)
                {
                    this.insEntitys.SYS_PWD_SAVE_KBN = Convert.ToInt16(this.form.SYS_PWD_SAVE_KBN.Text);
                }

                // モバイル将軍連携がONの場合
                if (AppConfig.AppOptions.IsMobile())
                {
                    if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Length > 0)
                    {
                        // 新規パスワード
                        this.insEntitys.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD = this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text;
                    }
                    else if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Length > 0)
                    {
                        // 旧パスワード
                        this.insEntitys.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD = this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text;
                    }
                }

                // マスタタブ（共通）
                if (this.form.COMMON_PASSWORD_DISP.Text.Length > 0)
                {
                    this.insEntitys.COMMON_PASSWORD_DISP = Convert.ToInt16(this.form.COMMON_PASSWORD_DISP.Text);
                }

                // マスタタブ（メニュー権限）
                //if (this.form.MENU_KENGEN_SETTEI_KIJYUNN.Text.Length > 0)
                //{
                //    this.insEntitys.MENU_KENGEN_SETTEI_KIJYUNN = Convert.ToInt16(this.form.MENU_KENGEN_SETTEI_KIJYUNN.Text);
                //}

                // 取引先
                this.insEntitys.TORIHIKISAKI_KEISHOU1 = this.form.TORIHIKISAKI_KEISHOU1.Text;
                this.insEntitys.TORIHIKISAKI_KEISHOU2 = this.form.TORIHIKISAKI_KEISHOU2.Text;

                // マスタタブ（取引先-請求情報）
                if (this.form.SEIKYUU_HICCHAKUBI.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_HICCHAKUBI = Convert.ToInt16(this.form.SEIKYUU_HICCHAKUBI.Text);
                }

                if (this.form.SEIKYUU_KAISHUU_DAY.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KAISHUU_DAY = Convert.ToInt16(this.form.SEIKYUU_KAISHUU_DAY.Text);
                }

                if (this.form.SEIKYUU_KAISHUU_HOUHOU.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KAISHUU_HOUHOU = Convert.ToInt16(this.form.SEIKYUU_KAISHUU_HOUHOU.Text);
                }
                //160028 S
                if (this.form.SEIKYUU_KAISHUU_BETSU.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KAISHUU_BETSU_KBN = Convert.ToInt16(this.form.SEIKYUU_KAISHUU_BETSU.Text);
                }
                if (this.form.SEIKYUU_KAISHUU_BETSU_NICHIGO.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KAISHUU_BETSU_NICHIGO = Convert.ToInt16(this.form.SEIKYUU_KAISHUU_BETSU_NICHIGO.Text);
                }
                //160028 E
                if (this.form.SEIKYUU_KAISHUU_MONTH.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KAISHUU_MONTH = Convert.ToInt16(this.form.SEIKYUU_KAISHUU_MONTH.Text);
                }

                if (this.form.SEIKYUU_KEITAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KEITAI_KBN = Convert.ToInt16(this.form.SEIKYUU_KEITAI_KBN.Text);
                }

                if (this.form.SEIKYUU_KINGAKU_HASUU_CD.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KINGAKU_HASUU_CD = Convert.ToInt16(this.form.SEIKYUU_KINGAKU_HASUU_CD.Text);
                }

                if (this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_NYUUKIN_MEISAI_KBN = Convert.ToInt16(this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.Text);
                }

                if (this.form.SEIKYUU_SHIMEBI1.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHIMEBI1 = Convert.ToInt16(this.form.SEIKYUU_SHIMEBI1.Text);
                }

                if (this.form.SEIKYUU_SHIMEBI2.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHIMEBI2 = Convert.ToInt16(this.form.SEIKYUU_SHIMEBI2.Text);
                }

                if (this.form.SEIKYUU_SHIMEBI3.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHIMEBI3 = Convert.ToInt16(this.form.SEIKYUU_SHIMEBI3.Text);
                }

                if (this.form.SEIKYUU_SHOSHIKI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHOSHIKI_KBN = Convert.ToInt16(this.form.SEIKYUU_SHOSHIKI_KBN.Text);
                }

                if (this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHOSHIKI_MEISAI_KBN = Convert.ToInt16(this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.Text);
                }

                if (this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHOSHIKI_GENBA_KBN = Convert.ToInt16(this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text);
                }

                if (this.form.SEIKYUU_TAX_HASUU_CD.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_TAX_HASUU_CD = Convert.ToInt16(this.form.SEIKYUU_TAX_HASUU_CD.Text);
                }

                if (this.form.SEIKYUU_TORIHIKI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_TORIHIKI_KBN = Convert.ToInt16(this.form.SEIKYUU_TORIHIKI_KBN.Text);
                }

                if (this.form.SEIKYUU_YOUSHI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_YOUSHI_KBN = Convert.ToInt16(this.form.SEIKYUU_YOUSHI_KBN.Text);
                }

                if (this.form.SEIKYUU_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_ZEI_KBN_CD = Convert.ToInt16(this.form.SEIKYUU_ZEI_KBN_CD.Text);
                }

                if (this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_ZEI_KEISAN_KBN_CD = Convert.ToInt16(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text);
                }

                // 20160419 koukoukon v2.1_電子請求書 start
                // 出力区分
                if (this.form.SEIKYUU_OUTPUT_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_OUTPUT_KBN = Convert.ToInt16(this.form.SEIKYUU_OUTPUT_KBN.Text);
                }
                // 20160419 koukoukon v2.1_電子請求書 end

                if (this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_DAIHYOU_PRINT_KBN = Convert.ToInt16(this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text);
                }

                if (this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_KYOTEN_CD = Convert.ToInt16(this.form.SEIKYUU_KYOTEN_CD.Text);
                }

                // マスタタブ（取引先-支払情報）
                if (this.form.SHIHARAI_DAY.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_DAY = Convert.ToInt16(this.form.SHIHARAI_DAY.Text);
                }

                if (this.form.SHIHARAI_HOUHOU.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_HOUHOU = Convert.ToInt16(this.form.SHIHARAI_HOUHOU.Text);
                }

                if (this.form.SHIHARAI_KEITAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KEITAI_KBN = Convert.ToInt16(this.form.SHIHARAI_KEITAI_KBN.Text);
                }

                if (this.form.SHIHARAI_KINGAKU_HASUU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KINGAKU_HASUU_CD = Convert.ToInt16(this.form.SHIHARAI_KINGAKU_HASUU_CD.Text);
                }
                //160028 S
                if (this.form.SHIHARAI_KAISHUU_BETSU.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KAISHUU_BETSU_KBN = Convert.ToInt16(this.form.SHIHARAI_KAISHUU_BETSU.Text);
                }
                if (this.form.SHIHARAI_KAISHUU_BETSU_NICHIGO.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KAISHUU_BETSU_NICHIGO = Convert.ToInt16(this.form.SHIHARAI_KAISHUU_BETSU_NICHIGO.Text);
                }
                //160028 E
                if (this.form.SHIHARAI_MONTH.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_MONTH = Convert.ToInt16(this.form.SHIHARAI_MONTH.Text);
                }

                if (this.form.SHIHARAI_SHIMEBI1.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHIMEBI1 = Convert.ToInt16(this.form.SHIHARAI_SHIMEBI1.Text);
                }

                if (this.form.SHIHARAI_SHIMEBI2.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHIMEBI2 = Convert.ToInt16(this.form.SHIHARAI_SHIMEBI2.Text);
                }

                if (this.form.SHIHARAI_SHIMEBI3.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHIMEBI3 = Convert.ToInt16(this.form.SHIHARAI_SHIMEBI3.Text);
                }

                if (this.form.SHIHARAI_SHOSHIKI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHOSHIKI_KBN = Convert.ToInt16(this.form.SHIHARAI_SHOSHIKI_KBN.Text);
                }

                if (this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHOSHIKI_MEISAI_KBN = Convert.ToInt16(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text);
                }

                if (this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHOSHIKI_GENBA_KBN = Convert.ToInt16(this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text);
                }

                if (this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHUKKIN_MEISAI_KBN = Convert.ToInt16(this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.Text);
                }

                if (this.form.SHIHARAI_TAX_HASUU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_TAX_HASUU_CD = Convert.ToInt16(this.form.SHIHARAI_TAX_HASUU_CD.Text);
                }

                if (this.form.SHIHARAI_TORIHIKI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_TORIHIKI_KBN = Convert.ToInt16(this.form.SHIHARAI_TORIHIKI_KBN.Text);
                }

                if (this.form.SHIHARAI_YOUSHI_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_YOUSHI_KBN = Convert.ToInt16(this.form.SHIHARAI_YOUSHI_KBN.Text);
                }

                if (this.form.SHIHARAI_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_ZEI_KBN_CD = Convert.ToInt16(this.form.SHIHARAI_ZEI_KBN_CD.Text);
                }

                if (this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_ZEI_KEISAN_KBN_CD = Convert.ToInt16(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text);
                }

                if (this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KYOTEN_CD = Convert.ToInt16(this.form.SHIHARAI_KYOTEN_CD.Text);
                }

                // マスタタブ（業者）
                this.insEntitys.GYOUSHA_KEISHOU1 = this.form.GYOUSHA_KEISHOU1.Text;
                this.insEntitys.GYOUSHA_KEISHOU2 = this.form.GYOUSHA_KEISHOU2.Text;

                this.insEntitys.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN = this.form.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked;
                //自社区分は常にfalse設定にて登録
                //this.insEntitys.GYOUSHA_JISHA_KBN = this.form.GYOUSHA_JISHA_KBN.Checked;
                this.insEntitys.GYOUSHA_JISHA_KBN = false;
                this.insEntitys.GYOUSHA_KBN_MANI = this.form.GYOUSHA_KBN_MANI.Checked;
                if (this.form.TORIHIKISAKI_UMU_KBN.Text.Length > 0)
                {
                    this.insEntitys.TORIHIKISAKI_UMU_KBN = Convert.ToInt16(this.form.TORIHIKISAKI_UMU_KBN.Text);
                }
                this.insEntitys.GYOUSHA_KBN_SHUKKA = this.form.GYOUSHA_KBN_SHUKKA.Checked;
                this.insEntitys.GYOUSHA_KBN_UKEIRE = this.form.GYOUSHA_KBN_UKEIRE.Checked;
                this.insEntitys.GYOUSHA_MANI_HENSOUSAKI_KBN = this.form.GYOUSHA_MANI_HENSOUSAKI_KBN.Checked;
                this.insEntitys.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN = this.form.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked;
                if (this.form.GYOUSHA_TORIHIKI_CHUUSHI.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_TORIHIKI_CHUUSHI = Convert.ToInt16(this.form.GYOUSHA_TORIHIKI_CHUUSHI.Text);
                }
                this.insEntitys.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN = this.form.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Checked;

                if (this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN = Convert.ToInt16(this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text);
                }

                if (this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_SEIKYUU_KYOTEN_CD = Convert.ToInt16(this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text);
                }

                if (this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.GYOUSHA_SHIHARAI_KYOTEN_CD = Convert.ToInt16(this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text);
                }

                // マスタタブ（現場）
                this.insEntitys.GENBA_KEISHOU1 = this.form.GENBA_KEISHOU1.Text;
                this.insEntitys.GENBA_KEISHOU2 = this.form.GENBA_KEISHOU2.Text;

                this.insEntitys.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN = this.form.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN.Checked;
                //自社区分は常にfalseにて登録
                //this.insEntitys.GENBA_JISHA_KBN = this.form.GENBA_JISHA_KBN.Checked;
                this.insEntitys.GENBA_JISHA_KBN = false;
                this.insEntitys.GENBA_MANI_HENSOUSAKI_KBN = this.form.GENBA_MANI_HENSOUSAKI_KBN.Checked;
                if (this.form.GENBA_MANIFEST_SHURUI_CD.Text.Length > 0)
                {
                    this.insEntitys.GENBA_MANIFEST_SHURUI_CD = Convert.ToInt16(this.form.GENBA_MANIFEST_SHURUI_CD.Text);
                }

                if (this.form.GENBA_MANIFEST_TEHAI_CD.Text.Length > 0)
                {
                    this.insEntitys.GENBA_MANIFEST_TEHAI_CD = Convert.ToInt16(this.form.GENBA_MANIFEST_TEHAI_CD.Text);
                }

                this.insEntitys.GENBA_SAISHUU_SHOBUNJOU_KBN = this.form.GENBA_SAISHUU_SHOBUNJOU_KBN.Checked;
                this.insEntitys.GENBA_SHOBUN_NIOROSHI_GENBA_KBN = this.form.GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked;
                this.insEntitys.GENBA_TSUMIKAEHOKAN_KBN = this.form.GENBA_TSUMIKAEHOKAN_KBN.Checked;

                if (this.form.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN = Convert.ToInt16(this.form.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.Text);
                }

                if (this.form.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GENBA_SEIKYUU_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.GENBA_SEIKYUU_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.GENBA_SEIKYUU_KYOTEN_CD = Convert.ToInt16(this.form.GENBA_SEIKYUU_KYOTEN_CD.Text);
                }

                if (this.form.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.GENBA_SHIHARAI_KYOTEN_PRINT_KBN = Convert.ToInt16(this.form.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.Text);
                }

                if (this.form.GENBA_SHIHARAI_KYOTEN_CD.Text.Length > 0)
                {
                    this.insEntitys.GENBA_SHIHARAI_KYOTEN_CD = Convert.ToInt16(this.form.GENBA_SHIHARAI_KYOTEN_CD.Text);
                }

                if (this.form.GENBA_KANSAN_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.GENBA_KANSAN_UNIT_CD = Convert.ToInt16(this.form.GENBA_KANSAN_UNIT_CD.Text);
                }
                this.insEntitys.YOUKI_NYUU = this.form.YOUKI_NYUU.Checked;
                this.insEntitys.JISSUU = this.form.JISSUU.Checked;

                if (this.form.GENBA_TEIKI_KEIYAKU_KBN.Text.Length > 0)
                {
                    this.insEntitys.GENBA_TEIKI_KEIYAKU_KBN = Convert.ToInt16(this.form.GENBA_TEIKI_KEIYAKU_KBN.Text);
                }

                if (this.form.GENBA_TEIKI_KEIJYOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.GENBA_TEIKI_KEIJYOU_KBN = Convert.ToInt16(this.form.GENBA_TEIKI_KEIJYOU_KBN.Text);
                }

                // マスタタブ（入/出金先）
                if (this.form.NYUUKIN_TORIKOMI_KBN.Text.Length > 0)
                {
                    this.insEntitys.NYUUKIN_TORIKOMI_KBN = Convert.ToInt16(this.form.NYUUKIN_TORIKOMI_KBN.Text);
                }

                // マスタタブ（品名）
                if (this.form.HINMEI_DENPYOU_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_DENPYOU_KBN_CD = Convert.ToInt16(this.form.HINMEI_DENPYOU_KBN_CD.Text);
                }

                if (this.form.HINMEI_DENSHU_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_DENSHU_KBN_CD = Convert.ToInt16(this.form.HINMEI_DENSHU_KBN_CD.Text);
                }

                if (this.form.HINMEI_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_UNIT_CD = Convert.ToInt16(this.form.HINMEI_UNIT_CD.Text);
                }

                if (this.form.HINMEI_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_ZEI_KBN_CD = Convert.ToInt16(this.form.HINMEI_ZEI_KBN_CD.Text);
                }

                if (this.form.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_SEARCH_CHUSYUTSU_JOKEN = Convert.ToInt16(this.form.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Text);
                }

                if (this.form.HINMEI_SEARCH_DENPYOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.HINMEI_SEARCH_DENPYOU_KBN = Convert.ToInt16(this.form.HINMEI_SEARCH_DENPYOU_KBN.Text);
                }

                // マスタタブ（基本品名単価）
                if (this.form.KIHON_HINMEI_DEFAULT.Text.Length > 0)
                {
                    this.insEntitys.KIHON_HINMEI_DEFAULT = Convert.ToInt16(this.form.KIHON_HINMEI_DEFAULT.Text);
                }

                // マスタタブ（個別品名単価）
                if (this.form.KOBETSU_HINMEI_DEFAULT.Text.Length > 0)
                {
                    this.insEntitys.KOBETSU_HINMEI_DEFAULT = Convert.ToInt16(this.form.KOBETSU_HINMEI_DEFAULT.Text);
                }

                // マスタタブ（換算値）
                if (this.form.KANSAN_DEFAULT.Text.Length > 0)
                {
                    this.insEntitys.KANSAN_DEFAULT = Convert.ToInt16(this.form.KANSAN_DEFAULT.Text);
                }

                if (this.form.KANSAN_KIHON_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.KANSAN_KIHON_UNIT_CD = Convert.ToInt16(this.form.KANSAN_KIHON_UNIT_CD.Text);
                }

                if (this.form.KANSAN_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.KANSAN_UNIT_CD = Convert.ToInt16(this.form.KANSAN_UNIT_CD.Text);
                }

                // マスタタブ（マニフェスト換算）
                if (this.form.MANI_KANSAN_KIHON_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.MANI_KANSAN_KIHON_UNIT_CD = Convert.ToInt16(this.form.MANI_KANSAN_KIHON_UNIT_CD.Text);
                }

                if (this.form.MANI_KANSAN_UNIT_CD.Text.Length > 0)
                {
                    this.insEntitys.MANI_KANSAN_UNIT_CD = Convert.ToInt16(this.form.MANI_KANSAN_UNIT_CD.Text);
                }

                // マスタタブ（委託契約登録）
                if (this.form.ITAKU_KEIYAKU_ALERT_AUTH.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_ALERT_AUTH = Convert.ToInt16(this.form.ITAKU_KEIYAKU_ALERT_AUTH.Text);
                }

                if (this.form.ITAKU_KEIYAKU_CHECK.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_CHECK = Convert.ToInt16(this.form.ITAKU_KEIYAKU_CHECK.Text);
                }

                if (this.form.ITAKU_KEIYAKU_KOUSHIN_SHUBETSU.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_KOUSHIN_SHUBETSU = Convert.ToInt16(this.form.ITAKU_KEIYAKU_KOUSHIN_SHUBETSU.Text);
                }

                if (this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD = Convert.ToInt16(this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text);
                }

                if (this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_SUURYOU_FORMAT = changeFormat(this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text);
                }

                if (this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_TANKA_FORMAT_CD = Convert.ToInt16(this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text);
                }

                if (this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_TANKA_FORMAT = changeFormat(this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text);
                }

                if (this.form.ITAKU_KEIYAKU_TYPE.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_TYPE = Convert.ToInt16(this.form.ITAKU_KEIYAKU_TYPE.Text);
                }

                if (this.form.ITAKU_KEIYAKU_SHURUI.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_SHURUI = Convert.ToInt16(this.form.ITAKU_KEIYAKU_SHURUI.Text);
                }

                if (this.form.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Text.Length > 0)
                {
                    this.insEntitys.ITAKU_KEIYAKU_TOUROKU_HOUHOU = Convert.ToInt16(this.form.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Text);
                }
                
                if (AppConfig.IsManiLite)
                {
                    // マニライト(C8)は収集運搬契約固定
                    this.insEntitys.ITAKU_KEIYAKU_SHURUI = 1;
                }

                // マスタタブ（コンテナ）
                if (this.form.CONTENA_MAX_SET_KEIKA_DATE.Text.Length > 0)
                {
                    this.insEntitys.CONTENA_MAX_SET_KEIKA_DATE = Convert.ToInt16(this.form.CONTENA_MAX_SET_KEIKA_DATE.Text);
                }

                // 受付タブ
                this.insEntitys.UKETSUKE_UPN_GYOUSHA_CD_DEFALUT = this.form.UKETSUKE_UPN_GYOUSHA_CD_DEFALUT.Text;

                if (this.form.UKETSUKE_SIJISHO_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKETSUKE_SIJISHO_PRINT_KBN = Convert.ToInt16(this.form.UKETSUKE_SIJISHO_PRINT_KBN.Text);
                }

                if (this.form.CONTENA_KANRI_HOUHOU.Text.Length > 0)
                {
                    this.insEntitys.CONTENA_KANRI_HOUHOU = Int16.Parse(this.form.CONTENA_KANRI_HOUHOU.Text);
                }

                if (this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKETSUKE_SIJISHO_SUB_PRINT_KBN = Convert.ToInt16(this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Text);
                }

                // 配車タブ
                if (this.form.HAISHA_NIPPOU_LAYOUT_KBN.Text.Length > 0)
                {
                    this.insEntitys.HAISHA_NIPPOU_LAYOUT_KBN = Convert.ToInt16(this.form.HAISHA_NIPPOU_LAYOUT_KBN.Text);
                }
                if (this.form.HAISHA_ONEDAY_NYUURYOKU_KBN.Text.Length > 0)
                {
                    this.insEntitys.HAISHA_ONEDAY_NYUURYOKU_KBN = Convert.ToInt16(this.form.HAISHA_ONEDAY_NYUURYOKU_KBN.Text);
                }

                // VUNGUYEN 20150703 START
                if (this.form.HAISHA_WARIATE_KAISHI.Text.Length > 0)
                {
                    this.insEntitys.HAISHA_WARIATE_KAISHI = Convert.ToInt16(this.form.HAISHA_WARIATE_KAISHI.Text);
                }
                if (this.form.HAISHA_WARIATE_KUUHAKU.Text.Length > 0)
                {
                    this.insEntitys.HAISHA_WARIATE_KUUHAKU = Convert.ToInt16(this.form.HAISHA_WARIATE_KUUHAKU.Text);
                }
                // VUNGUYEN 20150703 END

                // 受入タブ
                if (this.form.UKEIRE_CALC_BASE_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_CALC_BASE_KBN = Convert.ToInt16(this.form.UKEIRE_CALC_BASE_KBN.Text);
                }

                if (this.form.UKEIRE_CHOUSEI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_CHOUSEI_HASU_CD = Convert.ToInt16(this.form.UKEIRE_CHOUSEI_HASU_CD.Text);
                }

                if (this.form.UKEIRE_CHOUSEI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_CHOUSEI_HASU_KETA = Convert.ToInt16(this.form.UKEIRE_CHOUSEI_HASU_KETA.Text);
                }

                if (this.form.UKEIRE_CHOUSEI_WARIAI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_CHOUSEI_WARIAI_HASU_CD = Convert.ToInt16(this.form.UKEIRE_CHOUSEI_WARIAI_HASU_CD.Text);
                }

                if (this.form.UKEIRE_CHOUSEI_WARIAI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_CHOUSEI_WARIAI_HASU_KETA = Convert.ToInt16(this.form.UKEIRE_CHOUSEI_WARIAI_HASU_KETA.Text);
                }

                if (this.form.UKEIRE_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.UKEIRE_KAKUTEI_USE_KBN.Text);
                }

                // No.4089-->
                if (this.form.UKEIRE_KAKUTEI_FLAG.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_KAKUTEI_FLAG = Convert.ToInt16(this.form.UKEIRE_KAKUTEI_FLAG.Text);
                    //20151015 hoanghm #13316 start
                    //if (this.form.UKEIRE_KAKUTEI_USE_KBN.Text.Length > 0 && SystemSetteiHoshuConstans.KAKUTEI_RIYOU_OFF.ToString().Equals(this.form.UKEIRE_KAKUTEI_USE_KBN.Text))
                    //{   // 確定を利用しない場合、強制的に確定伝票にする
                    //    this.insEntitys.UKEIRE_KAKUTEI_FLAG = 1;
                    //}
                    //20151015 hoanghm #13316 end
                }
                // No.4089<--

                if (this.form.UKEIRE_ZANDAKA_JIDOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_ZANDAKA_JIDOU_KBN = Convert.ToInt16(this.form.UKEIRE_ZANDAKA_JIDOU_KBN.Text);
                }

                if (this.form.UKEIRE_WARIFURI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_WARIFURI_HASU_CD = Convert.ToInt16(this.form.UKEIRE_WARIFURI_HASU_CD.Text);
                }

                if (this.form.UKEIRE_WARIFURI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_WARIFURI_HASU_KETA = Convert.ToInt16(this.form.UKEIRE_WARIFURI_HASU_KETA.Text);
                }

                if (this.form.UKEIRE_WARIFURI_WARIAI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_WARIFURI_WARIAI_HASU_CD = Convert.ToInt16(this.form.UKEIRE_WARIFURI_WARIAI_HASU_CD.Text);
                }

                if (this.form.UKEIRE_WARIFURI_WARIAI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_WARIFURI_WARIAI_HASU_KETA = Convert.ToInt16(this.form.UKEIRE_WARIFURI_WARIAI_HASU_KETA.Text);
                }

                this.insEntitys.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1 = this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2 = this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3 = this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text;
                this.insEntitys.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1 = this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2 = this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3 = this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text;
                //160028 S
                if (this.form.UKEIRE_BARCODE_JOUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_BARCODE_JOUDAN_KBN = Convert.ToInt16(this.form.UKEIRE_BARCODE_JOUDAN_KBN.Text);
                }
                if (this.form.UKEIRE_BARCODE_CHUUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.UKEIRE_BARCODE_CHUUDAN_KBN = Convert.ToInt16(this.form.UKEIRE_BARCODE_CHUUDAN_KBN.Text);
                }
                //160028 E
                //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
                //将軍-INXS

                if (AppConfig.AppOptions.IsInxsUketsuke())
                {
                    if (!string.IsNullOrEmpty(this.form.HAISHA_HENKOU_SAGYOU_DATE_KBN.Text))
                    {
                        this.insEntitys.HAISHA_HENKOU_SAGYOU_DATE_KBN = Convert.ToInt16(this.form.HAISHA_HENKOU_SAGYOU_DATE_KBN.Text);
                    }
                }
                //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END

                // 出荷タブ
                if (this.form.SHUKKA_CALC_BASE_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_CALC_BASE_KBN = Convert.ToInt16(this.form.SHUKKA_CALC_BASE_KBN.Text);
                }

                if (this.form.SHUKKA_CHOUSEI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_CHOUSEI_HASU_CD = Convert.ToInt16(this.form.SHUKKA_CHOUSEI_HASU_CD.Text);
                }

                if (this.form.SHUKKA_CHOUSEI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_CHOUSEI_HASU_KETA = Convert.ToInt16(this.form.SHUKKA_CHOUSEI_HASU_KETA.Text);
                }

                if (this.form.SHUKKA_CHOUSEI_WARIAI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_CHOUSEI_WARIAI_HASU_CD = Convert.ToInt16(this.form.SHUKKA_CHOUSEI_WARIAI_HASU_CD.Text);
                }

                if (this.form.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_CHOUSEI_WARIAI_HASU_KETA = Convert.ToInt16(this.form.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.Text);
                }

                if (this.form.SHUKKA_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.SHUKKA_KAKUTEI_USE_KBN.Text);
                }

                // No.4089-->
                if (this.form.SHUKKA_KAKUTEI_FLAG.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_KAKUTEI_FLAG = Convert.ToInt16(this.form.SHUKKA_KAKUTEI_FLAG.Text);
                    //20151015 hoanghm #13316 start
                    //if (this.form.SHUKKA_KAKUTEI_USE_KBN.Text.Length > 0 && SystemSetteiHoshuConstans.KAKUTEI_RIYOU_OFF.ToString().Equals(this.form.SHUKKA_KAKUTEI_USE_KBN.Text))
                    //{   // 確定を利用しない場合、強制的に確定伝票にする
                    //    this.insEntitys.SHUKKA_KAKUTEI_FLAG = 1;
                    //}
                    //20151015 hoanghm #13316 end
                }
                // No.4089<--

                if (this.form.SHUKKA_ZANDAKA_JIDOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_ZANDAKA_JIDOU_KBN = Convert.ToInt16(this.form.SHUKKA_ZANDAKA_JIDOU_KBN.Text);
                }

                if (this.form.SHUKKA_WARIFURI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_WARIFURI_HASU_CD = Convert.ToInt16(this.form.SHUKKA_WARIFURI_HASU_CD.Text);
                }

                if (this.form.SHUKKA_WARIFURI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_WARIFURI_HASU_KETA = Convert.ToInt16(this.form.SHUKKA_WARIFURI_HASU_KETA.Text);
                }

                if (this.form.SHUKKA_WARIFURI_WARIAI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_WARIFURI_WARIAI_HASU_CD = Convert.ToInt16(this.form.SHUKKA_WARIFURI_WARIAI_HASU_CD.Text);
                }

                if (this.form.SHUKKA_WARIFURI_WARIAI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_WARIFURI_WARIAI_HASU_KETA = Convert.ToInt16(this.form.SHUKKA_WARIFURI_WARIAI_HASU_KETA.Text);
                }

                this.insEntitys.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1 = this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2 = this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3 = this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text;
                this.insEntitys.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1 = this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2 = this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3 = this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text;
                this.insEntitys.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1 = this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2 = this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3 = this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text;
                this.insEntitys.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1 = this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2 = this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3 = this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text;
                //160028 S
                if (this.form.SHUKKA_BARCODE_JOUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_BARCODE_JOUDAN_KBN = Convert.ToInt16(this.form.SHUKKA_BARCODE_JOUDAN_KBN.Text);
                }
                if (this.form.SHUKKA_BARCODE_CHUUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHUKKA_BARCODE_CHUUDAN_KBN = Convert.ToInt16(this.form.SHUKKA_BARCODE_CHUUDAN_KBN.Text);
                }
                if (this.form.KENSHUU_BARCODE_JOUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.KENSHUU_BARCODE_JOUDAN_KBN = Convert.ToInt16(this.form.KENSHUU_BARCODE_JOUDAN_KBN.Text);
                }
                if (this.form.KENSHUU_BARCODE_CHUUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.KENSHUU_BARCODE_CHUUDAN_KBN = Convert.ToInt16(this.form.KENSHUU_BARCODE_CHUUDAN_KBN.Text);
                }
                //160028 E
                // 売上/支払タブ
                if (this.form.UR_SH_CALC_BASE_KBN.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_CALC_BASE_KBN = Convert.ToInt16(this.form.UR_SH_CALC_BASE_KBN.Text);
                }

                if (this.form.UR_SH_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.UR_SH_KAKUTEI_USE_KBN.Text);
                }

                // No.4089-->
                if (this.form.UR_SH_KAKUTEI_FLAG.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_KAKUTEI_FLAG = Convert.ToInt16(this.form.UR_SH_KAKUTEI_FLAG.Text);
                    //20151015 hoanghm #13316 start
                    //if (this.form.UR_SH_KAKUTEI_USE_KBN.Text.Length > 0 && SystemSetteiHoshuConstans.KAKUTEI_RIYOU_OFF.ToString().Equals(this.form.UR_SH_KAKUTEI_USE_KBN.Text))
                    //{   // 確定を利用しない場合、強制的に確定伝票にする
                    //    this.insEntitys.UR_SH_KAKUTEI_FLAG = 1;
                    //}
                    //20151015 hoanghm #13316 end
                }
                // No.4089<--

                if (this.form.UR_SH_ZANDAKA_JIDOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_ZANDAKA_JIDOU_KBN = Convert.ToInt16(this.form.UR_SH_ZANDAKA_JIDOU_KBN.Text);
                }

                this.insEntitys.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1 = this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2 = this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3 = this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text;
                this.insEntitys.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1 = this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text;
                this.insEntitys.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2 = this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text;
                this.insEntitys.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3 = this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text;
                //160028 S
                if (this.form.UR_SH_BARCODE_JOUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_BARCODE_JOUDAN_KBN = Convert.ToInt16(this.form.UR_SH_BARCODE_JOUDAN_KBN.Text);
                }
                if (this.form.UR_SH_BARCODE_CHUUDAN_KBN.Text.Length > 0)
                {
                    this.insEntitys.UR_SH_BARCODE_CHUUDAN_KBN = Convert.ToInt16(this.form.UR_SH_BARCODE_CHUUDAN_KBN.Text);
                }
                //160028 E
                // 計量タブ
                if (this.form.KEIRYOU_TORIHIKISAKI_DISP_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_TORIHIKISAKI_DISP_KBN = Convert.ToInt16(this.form.KEIRYOU_TORIHIKISAKI_DISP_KBN.Text);
                }

                if (this.form.KEIRYOU_LAYOUT_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_LAYOUT_KBN = Convert.ToInt16(this.form.KEIRYOU_LAYOUT_KBN.Text);
                }

                if (this.form.KEIRYOU_GOODS_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_GOODS_KBN = Convert.ToInt16(this.form.KEIRYOU_GOODS_KBN.Text);
                }

                this.insEntitys.KEIRYOU_HYOU_TITLE_1 = this.form.KEIRYOU_HYOU_TITLE_1.Text;
                this.insEntitys.KEIRYOU_HYOU_TITLE_2 = this.form.KEIRYOU_HYOU_TITLE_2.Text;
                this.insEntitys.KEIRYOU_HYOU_TITLE_3 = this.form.KEIRYOU_HYOU_TITLE_3.Text;
                //this.insEntitys.KEIRYOU_SHOUMEI_1 = this.form.KEIRYOU_SHOUMEI_1.Text;
                //this.insEntitys.KEIRYOU_SHOUMEI_2 = this.form.KEIRYOU_SHOUMEI_2.Text;
                //this.insEntitys.KEIRYOU_SHOUMEI_3 = this.form.KEIRYOU_SHOUMEI_3.Text;

                if (this.form.KEIRYOU_BARCODE_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_BARCODE_KBN = Convert.ToInt16(this.form.KEIRYOU_BARCODE_KBN.Text);
                }

                if (this.form.KEIRYOU_KIHON_KEIRYOU.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_KIHON_KEIRYOU = Convert.ToInt16(this.form.KEIRYOU_KIHON_KEIRYOU.Text);
                }

                if (this.form.KEIRYOU_HYOU_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_HYOU_PRINT_KBN = Convert.ToInt16(this.form.KEIRYOU_HYOU_PRINT_KBN.Text);
                }

                if (this.form.KEIRYOU_CHOUSEI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_CHOUSEI_HASU_CD = Convert.ToInt16(this.form.KEIRYOU_CHOUSEI_HASU_CD.Text);
                }

                if (this.form.KEIRYOU_CHOUSEI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_CHOUSEI_HASU_KETA = Convert.ToInt16(this.form.KEIRYOU_CHOUSEI_HASU_KETA.Text);
                }

                if (this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_CHOUSEI_WARIAI_HASU_CD = Convert.ToInt16(this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_CD.Text);
                }

                if (this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA = Convert.ToInt16(this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA.Text);
                }

                if (this.form.KEIRYOU_CHOUSEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_CHOUSEI_USE_KBN = Convert.ToInt16(this.form.KEIRYOU_CHOUSEI_USE_KBN.Text);
                }

                if (this.form.KEIRYOU_YOUKI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_YOUKI_USE_KBN = Convert.ToInt16(this.form.KEIRYOU_YOUKI_USE_KBN.Text);
                }

                if (this.form.KEIRYOU_TANKA_KINGAKU_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_TANKA_KINGAKU_USE_KBN = Convert.ToInt16(this.form.KEIRYOU_TANKA_KINGAKU_USE_KBN.Text);
                }

                if (this.form.KEIRYOU_MANIFEST_HAIKI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_MANIFEST_HAIKI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_MANIFEST_HAIKI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SEIKYUU_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SEIKYUU_ZEI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SEIKYUU_ZEI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SHIHARAI_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SHIHARAI_ZEI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SHIHARAI_ZEI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_UKEIRE_KEITAI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SHUKKA_KEITAI_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text);
                }

                if (this.form.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD = Convert.ToInt16(this.form.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text);
                }

                // 売上タブ
                if (this.form.URIAGE_HYOUJI_JOUKEN.Text.Length > 0)
                {
                    this.insEntitys.URIAGE_HYOUJI_JOUKEN = Convert.ToInt16(this.form.URIAGE_HYOUJI_JOUKEN.Text);
                }

                this.insEntitys.URIAGE_HYOUJI_JOUKEN_SHUKKA = this.form.URIAGE_HYOUJI_JOUKEN_SHUKKA.Checked;
                this.insEntitys.URIAGE_HYOUJI_JOUKEN_UKEIRE = this.form.URIAGE_HYOUJI_JOUKEN_UKEIRE.Checked;
                this.insEntitys.URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI = this.form.URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI.Checked;
                if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0)
                {
                    // 新規パスワード
                    this.insEntitys.URIAGE_KAKUTEI_KAIJO_PASSWORD = this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text;
                }
                else if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0)
                {
                    // 旧パスワード
                    this.insEntitys.URIAGE_KAKUTEI_KAIJO_PASSWORD = this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text;
                }

                if (this.form.URIAGE_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.URIAGE_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.URIAGE_KAKUTEI_USE_KBN.Text);
                }

                // 支払タブ
                if (this.form.SHIHARAI_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.SHIHARAI_KAKUTEI_USE_KBN.Text);
                }

                if (this.form.SHIHARAI_HYOUJI_JOUKEN.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_HYOUJI_JOUKEN = Convert.ToInt16(this.form.SHIHARAI_HYOUJI_JOUKEN.Text);
                }

                this.insEntitys.SHIHARAI_HYOUJI_JOUKEN_SHUKKA = this.form.SHIHARAI_HYOUJI_JOUKEN_SHUKKA.Checked;
                this.insEntitys.SHIHARAI_HYOUJI_JOUKEN_UKEIRE = this.form.SHIHARAI_HYOUJI_JOUKEN_UKEIRE.Checked;
                this.insEntitys.SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI = this.form.SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI.Checked;
                if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0)
                {
                    // 新規パスワード
                    this.insEntitys.SHIHARAI_KAKUTEI_KAIJO_PASSWORD = this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text;
                }
                else if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0)
                {
                    // 旧パスワード
                    this.insEntitys.SHIHARAI_KAKUTEI_KAIJO_PASSWORD = this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text;
                }

                // 入金タブ
                if (this.form.NYUUKIN_HANDAN_BEGIN.Text.Length > 0)
                {
                    this.insEntitys.NYUUKIN_HANDAN_BEGIN = Convert.ToDecimal(this.form.NYUUKIN_HANDAN_BEGIN.Text);
                }

                if (this.form.NYUUKIN_HANDAN_END.Text.Length > 0)
                {
                    this.insEntitys.NYUUKIN_HANDAN_END = Convert.ToDecimal(this.form.NYUUKIN_HANDAN_END.Text);
                }

                if (this.form.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text.Length > 0)
                {
                    this.insEntitys.NYUUKIN_IKKATSU_KBN_DISP_SUU = Convert.ToInt16(this.form.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text);
                }

                if (this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD = Convert.ToInt16(this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text);
                }

                // 請求タブ
                if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.Length > 0)
                {
                    // 税区分/締処理利用形態により値を設定する
                    var sysZeiKeisanKbnUseKbn = Convert.ToInt16(this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text);
                    this.insEntitys.SEIKYUU_SHIME_SHORI_KBN = 4;
                }

                if (this.form.SEIKYUU_SHIME_SEIKYUU_CHECK.Text.Length > 0)
                {
                    this.insEntitys.SEIKYUU_SHIME_SEIKYUU_CHECK = Convert.ToInt16(this.form.SEIKYUU_SHIME_SEIKYUU_CHECK.Text);
                }
                //請求書備考
                this.insEntitys.SEIKYUU_BIKOU_1 = this.form.SEIKYUU_BIKOU_1.Text;
                this.insEntitys.SEIKYUU_BIKOU_2 = this.form.SEIKYUU_BIKOU_2.Text;
                //旧請求書
                if (this.form.OLD_SEIKYUU_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.OLD_SEIKYUU_PRINT_KBN = Convert.ToInt16(this.form.OLD_SEIKYUU_PRINT_KBN.Text);
                }
                //角印印刷位置(上)
                if (this.form.KAKUIN_POSITION_TOP.Text.Length > 0)
                {
                    this.insEntitys.KAKUIN_POSITION_TOP = Convert.ToInt16(this.form.KAKUIN_POSITION_TOP.Text);
                }
                //角印印刷位置(左)
                if (this.form.KAKUIN_POSITION_LEFT.Text.Length > 0)
                {
                    this.insEntitys.KAKUIN_POSITION_LEFT = Convert.ToInt16(this.form.KAKUIN_POSITION_LEFT.Text);
                }
                //角印サイズ
                if (this.form.KAKUIN_SIZE.Text.Length > 0)
                {
                    this.insEntitys.KAKUIN_SIZE = Convert.ToInt16(this.form.KAKUIN_SIZE.Text);
                }
                //車輛名印字
                if (this.form.SHARYOU_NAME_INGI.Text.Length > 0)
                {
                    this.insEntitys.SHARYOU_NAME_INGI = Convert.ToInt16(this.form.SHARYOU_NAME_INGI.Text);
                }
                // 支払明細タブ
                if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.Length > 0)
                {
                    // 税区分/締処理利用形態により値を設定する
                    var sysZeiKeisanKbnUseKbn = Convert.ToInt16(this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text);
                    this.insEntitys.SHIHARAI_SHIME_SHORI_KBN = 4;
                }

                if (this.form.SHIHARAI_SHIME_SHIHARAI_CHECK.Text.Length > 0)
                {
                    this.insEntitys.SHIHARAI_SHIME_SHIHARAI_CHECK = Convert.ToInt16(this.form.SHIHARAI_SHIME_SHIHARAI_CHECK.Text);
                }
                //支払明細書備考
                this.insEntitys.SHIHARAI_BIKOU_1 = this.form.SHIHARAI_BIKOU_1.Text;
                this.insEntitys.SHIHARAI_BIKOU_2 = this.form.SHIHARAI_BIKOU_2.Text;
                //旧支払明細書
                if (this.form.OLD_SHIHARAI_PRINT_KBN.Text.Length > 0)
                {
                    this.insEntitys.OLD_SHIHARAI_PRINT_KBN = Convert.ToInt16(this.form.OLD_SHIHARAI_PRINT_KBN.Text);
                }

                // マニフェストタブ
                if (this.form.MANIFEST_JYUUYOU_DISP_KBN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_JYUUYOU_DISP_KBN = Convert.ToInt16(this.form.MANIFEST_JYUUYOU_DISP_KBN.Text);
                }

                // 20140606 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
                if (this.form.MANIFEST_VALIDATION_CHECK.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_VALIDATION_CHECK = Convert.ToInt16(this.form.MANIFEST_VALIDATION_CHECK.Text);
                }
                // 20140606 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end

                if (this.form.SANPAI_MANIFEST_CHECK.Text.Length > 0)
                {
                    this.insEntitys.SANPAI_MANIFEST_MERCURY_CHECK = Convert.ToInt16(this.form.SANPAI_MANIFEST_CHECK.Text);
                }
                if (this.form.KENPAI_MANIFEST_CHECK.Text.Length > 0)
                {
                    this.insEntitys.KENPAI_MANIFEST_MERCURY_CHECK = Convert.ToInt16(this.form.KENPAI_MANIFEST_CHECK.Text);
                }

                // 20140630 ria No.730 マニフェスト追加機能仕様_４ start
                this.insEntitys.MANIFEST_ENDDATE_USE_KBN = Convert.ToInt16(this.form.MANIFEST_ENDDATE_USE_KBN.Text);
                this.insEntitys.MANIFEST_UNPAN_DAYS = this.form.MANIFEST_UNPAN_DAYS.Text.Length > 0 ? Convert.ToInt16(this.form.MANIFEST_UNPAN_DAYS.Text) : Convert.ToInt16(0);
                this.insEntitys.MANIFEST_SBN_DAYS = this.form.MANIFEST_SBN_DAYS.Text.Length > 0 ? Convert.ToInt16(this.form.MANIFEST_SBN_DAYS.Text) : Convert.ToInt16(0);
                this.insEntitys.MANIFEST_TOK_SBN_DAYS = this.form.MANIFEST_TOK_SBN_DAYS.Text.Length > 0 ? Convert.ToInt16(this.form.MANIFEST_TOK_SBN_DAYS.Text) : Convert.ToInt16(0);
                this.insEntitys.MANIFEST_LAST_SBN_DAYS = this.form.MANIFEST_LAST_SBN_DAYS.Text.Length > 0 ? Convert.ToInt16(this.form.MANIFEST_LAST_SBN_DAYS.Text) : Convert.ToInt16(0);
                // 20140630 ria No.730 マニフェスト追加機能仕様_４ end

                if (this.form.MANIFEST_OSHIRASE_DISP_KBN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_OSHIRASE_DISP_KBN = Convert.ToInt16(this.form.MANIFEST_OSHIRASE_DISP_KBN.Text);
                }

                if (this.form.MANIFEST_REPORT_SUU_KBN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_REPORT_SUU_KBN = Convert.ToInt16(this.form.MANIFEST_REPORT_SUU_KBN.Text);
                }

                if (this.form.MANIFEST_RIREKI_DISP_KBN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_RIREKI_DISP_KBN = Convert.ToInt16(this.form.MANIFEST_RIREKI_DISP_KBN.Text);
                }

                if (this.form.MANIFEST_SUURYO_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_SUURYO_FORMAT_CD = Convert.ToInt16(this.form.MANIFEST_SUURYO_FORMAT_CD.Text);
                }

                if (this.form.MANIFEST_SUURYO_FORMAT_CD.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_SUURYO_FORMAT = changeFormat(this.form.MANIFEST_SUURYO_FORMAT_CD.Text);
                }

                if (this.form.MANIFEST_TUUCHI_BEGIN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_TUUCHI_BEGIN = Convert.ToInt16(this.form.MANIFEST_TUUCHI_BEGIN.Text);
                }

                if (this.form.MANIFEST_TUUCHI_END.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_TUUCHI_END = Convert.ToInt16(this.form.MANIFEST_TUUCHI_END.Text);
                }

                if (this.form.MANIFEST_USE_A.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_A = Convert.ToInt16(this.form.MANIFEST_USE_A.Text);
                }

                if (this.form.MANIFEST_USE_B1.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_B1 = Convert.ToInt16(this.form.MANIFEST_USE_B1.Text);
                }

                if (this.form.MANIFEST_USE_B2.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_B2 = Convert.ToInt16(this.form.MANIFEST_USE_B2.Text);
                }

                if (this.form.MANIFEST_USE_B4.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_B4 = Convert.ToInt16(this.form.MANIFEST_USE_B4.Text);
                }

                if (this.form.MANIFEST_USE_B6.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_B6 = Convert.ToInt16(this.form.MANIFEST_USE_B6.Text);
                }

                if (this.form.MANIFEST_USE_C1.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_C1 = Convert.ToInt16(this.form.MANIFEST_USE_C1.Text);
                }

                if (this.form.MANIFEST_USE_C2.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_C2 = Convert.ToInt16(this.form.MANIFEST_USE_C2.Text);
                }

                if (this.form.MANIFEST_USE_D.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_D = Convert.ToInt16(this.form.MANIFEST_USE_D.Text);
                }

                if (this.form.MANIFEST_USE_E.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_USE_E = Convert.ToInt16(this.form.MANIFEST_USE_E.Text);
                }

                if (this.form.MANIFEST_HENSOU_NATSUIN_1.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_HENSOU_NATSUIN_1 = this.form.MANIFEST_HENSOU_NATSUIN_1.Text;
                }

                if (this.form.MANIFEST_HENSOU_NATSUIN_2.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_HENSOU_NATSUIN_2 = this.form.MANIFEST_HENSOU_NATSUIN_2.Text;
                }

                if (this.form.MANIFEST_HENSOU_RENRAKU_1.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_HENSOU_RENRAKU_1 = this.form.MANIFEST_HENSOU_RENRAKU_1.Text;
                }

                if (this.form.MANIFEST_HENSOU_RENRAKU_2.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_HENSOU_RENRAKU_2 = this.form.MANIFEST_HENSOU_RENRAKU_2.Text;
                }

                if (this.form.MANIFEST_PRINTERNAME.SelectedIndex >= 0)
                {
                    this.insEntitys.MANIFEST_PRINTERMAKERNAME = this.defaultMarginSettings.Rows[this.form.MANIFEST_PRINTERNAME.SelectedIndex]["PRINTERMAKERNAME"].ToString();
                    this.insEntitys.MANIFEST_PRINTERNAME = this.defaultMarginSettings.Rows[this.form.MANIFEST_PRINTERNAME.SelectedIndex]["PRINTERNAME"].ToString();
                }

                if (this.form.MANIFEST_RENKEI_KBN.Text.Length > 0)
                {
                    this.insEntitys.MANIFEST_RENKEI_KBN = Convert.ToInt16(this.form.MANIFEST_RENKEI_KBN.Text);
                }

                // 見積タブ
                if (this.form.MITSUMORI_SUBJECT_DEFAULT.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_SUBJECT_DEFAULT = this.form.MITSUMORI_SUBJECT_DEFAULT.Text;
                }

                //20250416
                if (this.form.MITSUMORI_SUBJECT_DEFAULT_1.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_SUBJECT_DEFAULT_1 = this.form.MITSUMORI_SUBJECT_DEFAULT_1.Text;
                }

                if (this.form.MITSUMORI_KOUMOKU1.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_KOUMOKU1 = this.form.MITSUMORI_KOUMOKU1.Text;
                }

                if (this.form.MITSUMORI_KOUMOKU2.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_KOUMOKU2 = this.form.MITSUMORI_KOUMOKU2.Text;
                }

                if (this.form.MITSUMORI_KOUMOKU3.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_KOUMOKU3 = this.form.MITSUMORI_KOUMOKU3.Text;
                }

                if (this.form.MITSUMORI_KOUMOKU4.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_KOUMOKU4 = this.form.MITSUMORI_KOUMOKU4.Text;
                }

                //20250416
                if (this.form.MITSUMORI_KOUMOKU5.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_KOUMOKU5 = this.form.MITSUMORI_KOUMOKU5.Text;
                }

                if (this.form.MITSUMORI_NAIYOU1.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_NAIYOU1 = this.form.MITSUMORI_NAIYOU1.Text;
                }

                if (this.form.MITSUMORI_NAIYOU2.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_NAIYOU2 = this.form.MITSUMORI_NAIYOU2.Text;
                }

                if (this.form.MITSUMORI_NAIYOU3.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_NAIYOU3 = this.form.MITSUMORI_NAIYOU3.Text;
                }

                if (this.form.MITSUMORI_NAIYOU4.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_NAIYOU4 = this.form.MITSUMORI_NAIYOU4.Text;
                }

                //20250416
                if (this.form.MITSUMORI_NAIYOU5.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_NAIYOU5 = this.form.MITSUMORI_NAIYOU5.Text;
                }

                if (this.form.MITSUMORI_BIKOU1.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_BIKOU1 = this.form.MITSUMORI_BIKOU1.Text;
                }

                if (this.form.MITSUMORI_BIKOU2.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_BIKOU2 = this.form.MITSUMORI_BIKOU2.Text;
                }

                if (this.form.MITSUMORI_BIKOU3.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_BIKOU3 = this.form.MITSUMORI_BIKOU3.Text;
                }

                if (this.form.MITSUMORI_BIKOU4.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_BIKOU4 = this.form.MITSUMORI_BIKOU4.Text;
                }

                if (this.form.MITSUMORI_BIKOU5.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_BIKOU5 = this.form.MITSUMORI_BIKOU5.Text;
                }

                if (!string.IsNullOrEmpty(this.form.BUSHO_NAME_PRINT.Text))
                {
                    this.insEntitys.BUSHO_NAME_PRINT = Convert.ToInt16(this.form.BUSHO_NAME_PRINT.Text);
                }

                if (this.form.INJI_KYOTEN_CD1.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_INJI_KYOTEN_CD1 = Convert.ToInt16(this.form.INJI_KYOTEN_CD1.Text);
                }

                if (this.form.INJI_KYOTEN_CD2.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_INJI_KYOTEN_CD2 = Convert.ToInt16(this.form.INJI_KYOTEN_CD2.Text);
                }

                if (this.form.MITSUMORI_ZEIKEISAN_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_ZEIKEISAN_KBN_CD = Convert.ToInt16(this.form.MITSUMORI_ZEIKEISAN_KBN_CD.Text);
                }

                if (this.form.MITSUMORI_ZEI_KBN_CD.Text.Length > 0)
                {
                    this.insEntitys.MITSUMORI_ZEI_KBN_CD = Convert.ToInt16(this.form.MITSUMORI_ZEI_KBN_CD.Text);
                }

                // 在庫タブ
                if (this.form.ZAIKO_KANRI.Text.Length > 0)
                {
                    this.insEntitys.ZAIKO_KANRI = Int16.Parse(this.form.ZAIKO_KANRI.Text);
                }
                if (this.form.ZAIKO_HYOUKA_HOUHOU.Text.Length > 0)
                {
                    this.insEntitys.ZAIKO_HYOUKA_HOUHOU = Int16.Parse(this.form.ZAIKO_HYOUKA_HOUHOU.Text);
                }

                // 代納タブ
                if (this.form.DAINO_CALC_BASE_KBN.Text.Length > 0)
                {
                    this.insEntitys.DAINO_CALC_BASE_KBN = Convert.ToInt16(this.form.DAINO_CALC_BASE_KBN.Text);
                }

                if (this.form.DAINO_KAKUTEI_FLAG.Text.Length > 0)
                {
                    this.insEntitys.DAINO_KAKUTEI_FLAG = Convert.ToInt16(this.form.DAINO_KAKUTEI_FLAG.Text);
                    if (this.form.DAINO_KAKUTEI_USE_KBN.Text.Length > 0 && SystemSetteiHoshuConstans.KAKUTEI_RIYOU_OFF.ToString().Equals(this.form.DAINO_KAKUTEI_USE_KBN.Text))
                    {   // 確定を利用しない場合、強制的に確定伝票にする
                        this.insEntitys.DAINO_KAKUTEI_FLAG = 1;
                    }
                }

                if (this.form.DAINO_ZANDAKA_JIDOU_KBN.Text.Length > 0)
                {
                    this.insEntitys.DAINO_ZANDAKA_JIDOU_KBN = Convert.ToInt16(this.form.DAINO_ZANDAKA_JIDOU_KBN.Text);
                }

                if (this.form.DAINO_KAKUTEI_USE_KBN.Text.Length > 0)
                {
                    this.insEntitys.DAINO_KAKUTEI_USE_KBN = Convert.ToInt16(this.form.DAINO_KAKUTEI_USE_KBN.Text);
                }

                // 電子契約タブ
                // オプションがONの場合
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    if (this.form.DENSHI_KEIYAKU_MESSAGE.Text.Length > 0)
                    {
                        this.insEntitys.DENSHI_KEIYAKU_MESSAGE = this.form.DENSHI_KEIYAKU_MESSAGE.Text;
                    }

                    if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_SOUFUHOUHOU.Text))
                    {
                        this.insEntitys.DENSHI_KEIYAKU_SOUFUHOUHOU = Convert.ToInt16(this.form.DENSHI_KEIYAKU_SOUFUHOUHOU.Text);
                    }

                    if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_ACCESS_CODE_CHECK.Text))
                    {
                        this.insEntitys.DENSHI_KEIYAKU_ACCESS_CODE_CHECK = Convert.ToInt16(this.form.DENSHI_KEIYAKU_ACCESS_CODE_CHECK.Text);
                    }
                    if (this.form.DENSHI_KEIYAKU_ACCESS_CODE.Text.Length > 0)
                    {
                        this.insEntitys.DENSHI_KEIYAKU_ACCESS_CODE = this.form.DENSHI_KEIYAKU_ACCESS_CODE.Text;
                    }
                    if (this.form.DENSHI_KEIYAKU_X_APP_ID.Text.Length > 0)
                    {
                        this.insEntitys.DENSHI_KEIYAKU_X_APP_ID = this.form.DENSHI_KEIYAKU_X_APP_ID.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.DENSHI_KEIYAKU_KYOUYUUSAKI_CC.Text))
                    {
                        this.insEntitys.DENSHI_KEIYAKU_KYOUYUUSAKI_CC = Convert.ToInt16(this.form.DENSHI_KEIYAKU_KYOUYUUSAKI_CC.Text);
                    }
                }
                else
                {
                    // オプションOFFの場合は初期値で更新
                    this.insEntitys.DENSHI_KEIYAKU_MESSAGE = null;
                    this.insEntitys.DENSHI_KEIYAKU_SOUFUHOUHOU = 2;
                    this.insEntitys.DENSHI_KEIYAKU_ACCESS_CODE_CHECK = 1;
                    this.insEntitys.DENSHI_KEIYAKU_ACCESS_CODE = string.Empty;
                    this.insEntitys.DENSHI_KEIYAKU_X_APP_ID = null;
                    this.insEntitys.DENSHI_KEIYAKU_KYOUYUUSAKI_CC = 2;
                }

                // 更新者情報設定
                var dataBinderLogicSystemS = new DataBinderLogic<r_framework.Entity.M_SYS_INFO>(this.insEntitys);
                dataBinderLogicSystemS.SetSystemProperty(this.insEntitys, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.insEntitys);

                // 削除フラグ、適用開始日・終了日の設定
                if (!string.IsNullOrEmpty(this.SearchResult.Rows[0]["DELETE_FLG"].ToString()))
                {
                    this.insEntitys.DELETE_FLG = Convert.ToBoolean(this.SearchResult.Rows[0]["DELETE_FLG"]);
                }

                // 検索結果がある場合はTimeStamp情報を設定
                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    byte[] timeStamp = (byte[])this.SearchResult.Rows[0]["TIME_STAMP"];
                    this.insEntitys.TIME_STAMP = timeStamp;
                }

                // デジタコ項目を設定
                this.insEntitys.DIGI_CORP_ID = this.form.digiCorpId;
                if (this.form.digiRangeRadius != 0)
                {
                    this.insEntitys.DIGI_RANGE_RADIUS = this.form.digiRangeRadius;
                }
                this.insEntitys.DIGI_UID = this.form.digiUserId;
                this.insEntitys.DIGI_PWD = this.form.digiPassword;
                this.insEntitys.DIGI_CARRY_ORVER_NEXT_DAY = this.form.digiCarryOverNextDay;
                this.insEntitys.MAX_WINDOW_COUNT = this.form.maxWindowCount;
                this.insEntitys.UKEIRESHUKA_GAMEN_SIZE = this.form.UKEIRESHUKA_GAMEN_SIZE;
                this.insEntitys.DENPYOU_HAKOU_HYOUJI = this.form.DENPYOU_HAKOU_HYOUJI;

                // NAVITIME項目を設定
                this.insEntitys.NAVI_CORP_ID = this.form.naviCorpId;
                this.insEntitys.NAVI_ACCOUNT = this.form.naviAccount;
                this.insEntitys.NAVI_PWD = this.form.naviPassword;
                this.insEntitys.NAVI_SAGYOU_TIME = this.form.naviSagyouTime;
                this.insEntitys.NAVI_TRAFFIC = this.form.naviTraffic;
                this.insEntitys.NAVI_SMART_IC = this.form.naviSmartIc;
                this.insEntitys.NAVI_TOLL = this.form.naviToll;
                this.insEntitys.NAVI_GET_TIME = this.form.naviGetTime;

                // MAPBOX項目を設定(画面からは変更不可)
                this.insEntitys.MAPBOX_ACCESS_TOKEN = Convert.ToString(this.SearchResult.Rows[0]["MAPBOX_ACCESS_TOKEN"]);
                this.insEntitys.MAPBOX_MAP_STYLE = Convert.ToString(this.SearchResult.Rows[0]["MAPBOX_MAP_STYLE"]);

                // オプション項目を設定
                this.insEntitys.MAX_INSERT_CAPACITY = this.form.maxInsertCapacity;
                this.insEntitys.MAX_FILE_SIZE = this.form.maxFileSize;
                this.insEntitys.DB_FILE_CONNECT = this.form.dbFileConnect;
                this.insEntitys.DB_INXS_SUBAPP_CONNECT_STRING = this.form.dbInxsSubappConnectString;
                this.insEntitys.DB_INXS_SUBAPP_CONNECT_NAME = this.form.dbInxsSubappConnectName;

                //CongBinh 20210712 #152799 S
                this.insEntitys.SUPPORT_KBN = short.Parse(this.form.SupportKbn);
                this.insEntitys.SUPPORT_URL = this.form.SupportUrl;
                //CongBinh 20210712 #152799 E

                //QN_QUAN add 20211229 #158952 S
                this.insEntitys.DB_LOG_CONNECT = this.form.dbLOGConnect;
                //QN_QUAN add 20211229 #158952 E

                this.insEntitys.SYS_BARCODO_SHINKAKU_KBN = this.form.SYS_BARCODO_SHINKAKU_KBN;//160029

                //PhuocLoc 2022/01/04 #158897, #158898 -Start
                if (AppConfig.AppOptions.IsWANSign())
                {
                    this.insEntitys.SECRET_KEY = this.form.ModSecretKey;
                    this.insEntitys.CUSTOMER_ID = this.form.ModCustomerId;

                    // 備考
                    this.insEntitys.WAN_SIGN_BIKOU_1 = this.form.WAN_SIGN_BIKOU_1.Text;
                    this.insEntitys.WAN_SIGN_BIKOU_2 = this.form.WAN_SIGN_BIKOU_2.Text;
                    this.insEntitys.WAN_SIGN_BIKOU_3 = this.form.WAN_SIGN_BIKOU_3.Text;

                    // フィールド
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_1.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_1 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_1.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_2.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_2 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_2.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_3.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_3 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_3.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_4.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_4 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_4.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_5.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_5 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_5.Text);
                    }

                    //フィールド名称
                    this.insEntitys.WAN_SIGN_FIELD_NAME_1 = this.form.WAN_SIGN_FIELD_NAME_1.Text;
                    this.insEntitys.WAN_SIGN_FIELD_NAME_2 = this.form.WAN_SIGN_FIELD_NAME_2.Text;
                    this.insEntitys.WAN_SIGN_FIELD_NAME_3 = this.form.WAN_SIGN_FIELD_NAME_3.Text;
                    this.insEntitys.WAN_SIGN_FIELD_NAME_4 = this.form.WAN_SIGN_FIELD_NAME_4.Text;
                    this.insEntitys.WAN_SIGN_FIELD_NAME_5 = this.form.WAN_SIGN_FIELD_NAME_5.Text;

                    // フィールド属性
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_ATTRIBUTE_1 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_ATTRIBUTE_2 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_ATTRIBUTE_3 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_ATTRIBUTE_4 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text))
                    {
                        this.insEntitys.WAN_SIGN_FIELD_ATTRIBUTE_5 = Convert.ToInt16(this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text);
                    }
                }
                //PhuocLoc 2022/01/04 #158897, #158898 -End

                // 空電プッシュ項目を設定
                this.insEntitys.KARADEN_ACCESS_KEY = this.form.KaradenAccessKey;
                this.insEntitys.KARADEN_SECURITY_CODE = this.form.karadenSecurityCode;
                this.insEntitys.KARADEN_MAX_WORD_COUNT = this.form.KaradenMaxWordCount;

                // ｼｮｰﾄﾒｯｾｰｼﾞタブ
                if (AppConfig.AppOptions.IsSMS())
                {
                    this.insEntitys.SMS_ALERT_CHARACTER_LIMIT = Convert.ToInt16(this.form.SMS_ALERT_CHARACTER_LIMIT.Text);
                    this.insEntitys.SMS_SEND_JOKYO = short.Parse(this.form.SMS_SEND_JOKYO.Text);
                    this.insEntitys.SMS_DENPYOU_SHURUI = short.Parse(this.form.SMS_DENPYOU_SHURUI.Text);
                    if (!string.IsNullOrEmpty(this.form.SMS_HAISHA_JOKYO.Text))
                    {
                        this.insEntitys.SMS_HAISHA_JOKYO = short.Parse(this.form.SMS_HAISHA_JOKYO.Text);
                    }
                    this.insEntitys.SMS_GREETINGS = this.form.GREETINGS.Text;
                    this.insEntitys.SMS_SIGNATURE = this.form.SIGNATURE.Text;
                }

                // 楽楽明細連携
                if (!string.IsNullOrEmpty(this.form.RakurakuCodeKbn))
                {
                    this.insEntitys.RAKURAKU_CODE_NUMBERING_KBN = Convert.ToInt16(this.form.RakurakuCodeKbn);
                }

                // No12509 システム設定変更履歴出力機能-->
                this.changeLogEntitys = new List<M_SYS_INFO_CHANGE_LOG>();
                M_SYS_INFO_CHANGE_LOG changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                DataTable MaxId = this.daoChangeLog.GetMaxId(changeLogEntity);
                long ID = Convert.ToInt64(this.daoChangeLog.GetMaxId(changeLogEntity).Rows[0]["ID"].ToString());
                int ROW_NO = 0;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //SqlDateTime CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                SqlDateTime CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                string CREATE_USER = SystemProperty.UserName;
                string CREATE_PC = SystemInformation.ComputerName;
                // 重量書式
                if (this.form.SYS_JYURYOU_FORMAT_CD.Text != this.SearchResult.Rows[0]["SYS_JYURYOU_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_JYURYOU_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_JYURYOU_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_JYURYOU_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 数量書式
                if (this.form.SYS_SUURYOU_FORMAT_CD.Text != this.SearchResult.Rows[0]["SYS_SUURYOU_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_SUURYOU_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_SUURYOU_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_SUURYOU_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 単価書式
                if (this.form.SYS_TANKA_FORMAT_CD.Text != this.SearchResult.Rows[0]["SYS_TANKA_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_TANKA_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_TANKA_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_TANKA_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 委託契約書-数量書式
                if (this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text != this.SearchResult.Rows[0]["ITAKU_KEIYAKU_SUURYOU_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "ITAKU_KEIYAKU_SUURYOU_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_SUURYOU_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 委託契約書-単価書式
                if (this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text != this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TANKA_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "ITAKU_KEIYAKU_TANKA_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TANKA_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-割振値端数
                if (this.form.UKEIRE_WARIFURI_HASU_CD.Text != this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_WARIFURI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_WARIFURI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-割振値端数桁
                if (this.form.UKEIRE_WARIFURI_HASU_KETA.Text != this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_WARIFURI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_WARIFURI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-割振割合端数
                if (this.form.UKEIRE_WARIFURI_WARIAI_HASU_CD.Text != this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_WARIFURI_WARIAI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_WARIFURI_WARIAI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-割振割合端数桁
                if (this.form.UKEIRE_WARIFURI_WARIAI_HASU_KETA.Text != this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_WARIFURI_WARIAI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_WARIFURI_WARIAI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-調整値端数
                if (this.form.UKEIRE_CHOUSEI_HASU_CD.Text != this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_CHOUSEI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_CHOUSEI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-調整値端数桁
                if (this.form.UKEIRE_CHOUSEI_HASU_KETA.Text != this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_CHOUSEI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_CHOUSEI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-調整割合端数
                if (this.form.UKEIRE_CHOUSEI_WARIAI_HASU_CD.Text != this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_CHOUSEI_WARIAI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_CHOUSEI_WARIAI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 受入-調整割合端数桁
                if (this.form.UKEIRE_CHOUSEI_WARIAI_HASU_KETA.Text != this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "UKEIRE_CHOUSEI_WARIAI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.UKEIRE_CHOUSEI_WARIAI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-割振値端数
                if (this.form.SHUKKA_WARIFURI_HASU_CD.Text != this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_WARIFURI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_WARIFURI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-割振値端数桁
                if (this.form.SHUKKA_WARIFURI_HASU_KETA.Text != this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_WARIFURI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_WARIFURI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-割振割合端数
                if (this.form.SHUKKA_WARIFURI_WARIAI_HASU_CD.Text != this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_WARIFURI_WARIAI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_WARIFURI_WARIAI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-割振割合端数桁
                if (this.form.SHUKKA_WARIFURI_WARIAI_HASU_KETA.Text != this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_WARIFURI_WARIAI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_WARIFURI_WARIAI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-調整値端数
                if (this.form.SHUKKA_CHOUSEI_HASU_CD.Text != this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_CHOUSEI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_CHOUSEI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-調整値端数桁
                if (this.form.SHUKKA_CHOUSEI_HASU_KETA.Text != this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_CHOUSEI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_CHOUSEI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-調整割合端数
                if (this.form.SHUKKA_CHOUSEI_WARIAI_HASU_CD.Text != this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_CHOUSEI_WARIAI_HASU_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_CHOUSEI_WARIAI_HASU_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 出荷-調整割合端数桁
                if (this.form.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.Text != this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_KETA"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SHUKKA_CHOUSEI_WARIAI_HASU_KETA";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_KETA"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // マニフェスト-数量書式
                if (this.form.MANIFEST_SUURYO_FORMAT_CD.Text != this.SearchResult.Rows[0]["MANIFEST_SUURYO_FORMAT_CD"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "MANIFEST_SUURYO_FORMAT_CD";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["MANIFEST_SUURYO_FORMAT_CD"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.MANIFEST_SUURYO_FORMAT_CD.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-連番方法
                if (this.form.SYS_RENBAN_HOUHOU_KBN.Text != this.SearchResult.Rows[0]["SYS_RENBAN_HOUHOU_KBN"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_RENBAN_HOUHOU_KBN";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_RENBAN_HOUHOU_KBN"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_RENBAN_HOUHOU_KBN.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-マニ登録形態
                if (this.form.SYS_MANI_KEITAI_KBN.Text != this.SearchResult.Rows[0]["SYS_MANI_KEITAI_KBN"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_MANI_KEITAI_KBN";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_MANI_KEITAI_KBN"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_MANI_KEITAI_KBN.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-在庫管理
                if (this.form.ZAIKO_KANRI.Text != this.SearchResult.Rows[0]["ZAIKO_KANRI"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "ZAIKO_KANRI";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["ZAIKO_KANRI"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.ZAIKO_KANRI.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-評価方法
                if (this.form.ZAIKO_HYOUKA_HOUHOU.Text != this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "ZAIKO_HYOUKA_HOUHOU";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.ZAIKO_HYOUKA_HOUHOU.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-コンテナ管理方法
                if (this.form.CONTENA_KANRI_HOUHOU.Text != this.SearchResult.Rows[0]["CONTENA_KANRI_HOUHOU"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "CONTENA_KANRI_HOUHOU";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["CONTENA_KANRI_HOUHOU"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.CONTENA_KANRI_HOUHOU.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }

                // 初回登録-領収書管理_年連番
                if (this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text != this.SearchResult.Rows[0]["SYS_RECEIPT_RENBAN_HOUHOU_KBN"].ToString())
                {
                    changeLogEntity = new M_SYS_INFO_CHANGE_LOG();
                    ROW_NO = ROW_NO + 1;
                    changeLogEntity.ID = ID;
                    changeLogEntity.ROW_NO = ROW_NO;
                    changeLogEntity.CHANGE_COLUMN_NAME = "SYS_RECEIPT_RENBAN_HOUHOU_KBN";
                    changeLogEntity.OLD_VALUE = this.SearchResult.Rows[0]["SYS_RECEIPT_RENBAN_HOUHOU_KBN"].ToString();
                    changeLogEntity.NEW_VALUE = this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text;
                    changeLogEntity.CREATE_DATE = CREATE_DATE;
                    changeLogEntity.CREATE_USER = CREATE_USER;
                    changeLogEntity.CREATE_PC = CREATE_PC;
                    this.changeLogEntitys.Add(changeLogEntity);
                }
                // No12509 システム設定変更履歴出力機能<--

                #region 20250303
                //受付
                this.insEntitys.SHONIN_RAN_1 = this.form.SHONIN_RAN_1.Text;
                this.insEntitys.SHONIN_RAN_2 = this.form.SHONIN_RAN_2.Text;
                this.insEntitys.SHONIN_RAN_3 = this.form.SHONIN_RAN_3.Text;
                this.insEntitys.SHONIN_RAN_4 = this.form.SHONIN_RAN_4.Text;
                this.insEntitys.SHONIN_RAN_5 = this.form.SHONIN_RAN_5.Text;
                this.insEntitys.SHONIN_RAN_6 = this.form.SHONIN_RAN_6.Text;
                this.insEntitys.SHONIN_RAN_7 = this.form.SHONIN_RAN_7.Text;
                this.insEntitys.SHONIN_RAN_8 = this.form.SHONIN_RAN_8.Text;
                this.insEntitys.SHONIN_RAN_9 = this.form.SHONIN_RAN_9.Text;
                this.insEntitys.SHONIN_RAN_10 = this.form.SHONIN_RAN_10.Text;

                //4
                if (this.form.HIDZUKE_INJI_CHECK.Text.Length > 0)
                {
                    this.insEntitys.HIDZUKE_INJI_CHECK = Convert.ToInt16(this.form.HIDZUKE_INJI_CHECK.Text);
                }
                #endregion 20250303

                #region 20250304
                //掲示板
                DateTime dttemp;

                if(DateTime.TryParse(this.form.KOKAI_KIKAN_FROM.Text, out dttemp))
                {
                    this.insEntitys.KOKAI_KIKAN_FROM = dttemp;
                }
                else
                {
                    this.insEntitys.KOKAI_KIKAN_FROM = SqlDateTime.Null;
                }

                if (DateTime.TryParse(this.form.KOKAI_KIKAN_TO.Text, out dttemp))
                {
                    this.insEntitys.KOKAI_KIKAN_TO = dttemp;
                }
                else
                {
                    this.insEntitys.KOKAI_KIKAN_TO = SqlDateTime.Null;
                }

                this.insEntitys.KEIJIBAN_TEKISUTO_BOKKUSU = this.form.KEIJIBAN_TEKISUTO_BOKKUSU.Text;

                //計量タブ
                this.insEntitys.KEIRYO_ORIJINARU = this.form.KEIRYO_ORIJINARU.Checked;

                #endregion 20250304

                #region 20250305
                //システムﾀﾌﾞ
                this.insEntitys.FURIKOMI_BANK_CD = this.form.FURIKOMI_BANK_CD.Text;
                this.insEntitys.FURIKOMI_BANK_SHITEN_CD = this.form.FURIKOMI_BANK_SHITEN_CD.Text;
                this.insEntitys.KOUZA_SHURUI = this.form.KOUZA_SHURUI.Text;
                this.insEntitys.KOUZA_NO = this.form.KOUZA_NO.Text;
                this.insEntitys.KOUZA_NAME = this.form.KOUZA_NAME.Text;

                #endregion 20250305

                #region 20250306
                //システムﾀﾌﾞ
                this.insEntitys.FURIKOMI_BANK_NAME = this.form.FURIKOMI_BANK_NAME.Text;
                this.insEntitys.FURIKOMI_BANK_SHITEN_NAME = this.form.FURIKOMI_BANK_SHITEN_NAME.Text;

                #endregion 20250306

                #region 20250307
                this.insEntitys.HIDZUKE_CHECK = this.form.HIDZUKE_CHECK.Checked;
                this.insEntitys.SHORI_RAIN_CHECK = this.form.SHORI_RAIN_CHECK.Checked;

                #endregion 20250307

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
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
            LogUtility.DebugMethodStart(errorFlag);

            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    M_SYS_INFO[] dataari = dao.GetAllData();

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        if (dataari.LongLength <= 0)
                        {
                            this.dao.Insert(insEntitys);
                        }
                        else
                        {
                            this.dao.Update(insEntitys);
                        }

                        foreach (var changeLogEntity in this.changeLogEntitys)
                        {
                            this.daoChangeLog.Insert(changeLogEntity);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");

                // 再表示
                this.form.Search(null, null);
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
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

            SystemSetteiHoshuLogic localLogic = other as SystemSetteiHoshuLogic;
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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                if (this.SearchResult.Rows.Count <= 0)
                {
                    return false;
                }

                // 各テキストボックス設定(BaseHeader部)
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = this.SearchResult.Rows[0]["CREATE_DATE"].ToString();
                header.CreateUser.Text = this.SearchResult.Rows[0]["CREATE_USER"].ToString();
                header.LastUpdateDate.Text = this.SearchResult.Rows[0]["UPDATE_DATE"].ToString();
                header.LastUpdateUser.Text = this.SearchResult.Rows[0]["UPDATE_USER"].ToString();

                // システムタブ
                this.form.ICHIRAN_ALERT_KENSUU.Text = this.SearchResult.Rows[0]["ICHIRAN_ALERT_KENSUU"].ToString();
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["ICHIRAN_HYOUJI_JOUKEN_DELETED"].ToString());
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["ICHIRAN_HYOUJI_JOUKEN_TEKIYOU"].ToString());
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI"].ToString());
                // 税区分CDの設定は取引先毎に自由に選択させるためSYS_ZEI_KEISAN_KBN_USE_KBNは非表示
                // DB上はとりあえず1を設定
                this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text = "1";
                this.form.SYS_JYURYOU_FORMAT_CD.Text = this.SearchResult.Rows[0]["SYS_JYURYOU_FORMAT_CD"].ToString();
                this.form.SYS_KAKUTEI__TANNI_KBN.Text = this.SearchResult.Rows[0]["SYS_KAKUTEI__TANNI_KBN"].ToString();
                this.form.SYS_MANI_KEITAI_KBN.Text = this.SearchResult.Rows[0]["SYS_MANI_KEITAI_KBN"].ToString();
                this.form.SYS_RENBAN_HOUHOU_KBN.Text = this.SearchResult.Rows[0]["SYS_RENBAN_HOUHOU_KBN"].ToString();
                this.form.SYS_RECEIPT_RENBAN_HOUHOU_KBN.Text = this.SearchResult.Rows[0]["SYS_RECEIPT_RENBAN_HOUHOU_KBN"].ToString();
                this.form.SYS_SUURYOU_FORMAT_CD.Text = this.SearchResult.Rows[0]["SYS_SUURYOU_FORMAT_CD"].ToString();
                this.form.SYS_TANKA_FORMAT_CD.Text = this.SearchResult.Rows[0]["SYS_TANKA_FORMAT_CD"].ToString();
                //パスワード保存機能 が Null の場合 1:有り を設定する
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SYS_PWD_SAVE_KBN"]))
                {
                    this.form.SYS_PWD_SAVE_KBN.Text = "1";
                }
                else
                {
                    this.form.SYS_PWD_SAVE_KBN.Text = this.SearchResult.Rows[0]["SYS_PWD_SAVE_KBN"].ToString();
                }

                // モバイル将軍システム設定起動パスワード
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text = this.SearchResult.Rows[0]["MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD"].ToString();
                
                // パスワードの初期化
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text = "";
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text = "";
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text = "";
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = true;
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = true;
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = true;
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = false;
                this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = false;

                // モバイル将軍システム設定起動パスワードの項目制御
                if (this.ChangeStatusMobilePassword()) { return true; }

                // マスタタブ（共通）
                this.form.COMMON_PASSWORD_DISP.Text = this.SearchResult.Rows[0]["COMMON_PASSWORD_DISP"].ToString();

                // マスタタブ（メニュー権限）
                //this.form.MENU_KENGEN_SETTEI_KIJYUNN.Text = this.SearchResult.Rows[0]["MENU_KENGEN_SETTEI_KIJYUNN"].ToString();

                // 取引先
                this.form.TORIHIKISAKI_KEISHOU1.Text = this.SearchResult.Rows[0]["TORIHIKISAKI_KEISHOU1"].ToString();
                this.form.TORIHIKISAKI_KEISHOU2.Text = this.SearchResult.Rows[0]["TORIHIKISAKI_KEISHOU2"].ToString();

                // マスタタブ（取引先-請求情報）
                this.form.SEIKYUU_HICCHAKUBI.Text = this.SearchResult.Rows[0]["SEIKYUU_HICCHAKUBI"].ToString();
                this.form.SEIKYUU_KAISHUU_DAY.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_DAY"].ToString();
                this.form.SEIKYUU_KAISHUU_HOUHOU.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_HOUHOU"].ToString();
                this.form.SEIKYUU_KAISHUU_HOUHOU_NAME.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_HOUHOU_NAME"].ToString();
                this.form.SEIKYUU_KAISHUU_MONTH.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_MONTH"].ToString();
                this.form.SEIKYUU_KEITAI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_KEITAI_KBN"].ToString();
                this.form.SEIKYUU_KINGAKU_HASUU_CD.Text = this.SearchResult.Rows[0]["SEIKYUU_KINGAKU_HASUU_CD"].ToString();
                this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_NYUUKIN_MEISAI_KBN"].ToString();
                this.form.SEIKYUU_SHIMEBI1.Text = this.SearchResult.Rows[0]["SEIKYUU_SHIMEBI1"].ToString();
                //160028 S
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_BETSU_KBN"]))
                {
                    this.form.SEIKYUU_KAISHUU_BETSU.Text = "1";
                }
                else
                {
                    this.form.SEIKYUU_KAISHUU_BETSU.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_BETSU_KBN"].ToString();
                }
                this.form.SEIKYUU_KAISHUU_BETSU_NICHIGO.Text = this.SearchResult.Rows[0]["SEIKYUU_KAISHUU_BETSU_NICHIGO"].ToString();
                //160028 E
                //読み込み時、請求締日１がNull or Spaceの場合　Enable_falseにする
                if (string.IsNullOrWhiteSpace(this.form.SEIKYUU_SHIMEBI1.Text))
                {
                    this.form.SEIKYUU_SHIMEBI2.Enabled = false;
                }
                else
                {
                    this.form.SEIKYUU_SHIMEBI2.Text = this.SearchResult.Rows[0]["SEIKYUU_SHIMEBI2"].ToString();
                }
                if (string.IsNullOrWhiteSpace(this.form.SEIKYUU_SHIMEBI2.Text))
                {
                    this.form.SEIKYUU_SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SEIKYUU_SHIMEBI3.Text = this.SearchResult.Rows[0]["SEIKYUU_SHIMEBI3"].ToString();
                }
                this.form.SEIKYUU_SHOSHIKI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_SHOSHIKI_KBN"].ToString();
                this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_SHOSHIKI_MEISAI_KBN"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_SHOSHIKI_GENBA_KBN"]))
                {
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text = "";
                }
                else
                {
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_SHOSHIKI_GENBA_KBN"].ToString();
                }

                this.form.SEIKYUU_TAX_HASUU_CD.Text = this.SearchResult.Rows[0]["SEIKYUU_TAX_HASUU_CD"].ToString();
                this.form.SEIKYUU_TORIHIKI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_TORIHIKI_KBN"].ToString();
                this.form.SEIKYUU_YOUSHI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_YOUSHI_KBN"].ToString();
                this.form.SEIKYUU_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["SEIKYUU_ZEI_KBN_CD"].ToString();
                this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = this.SearchResult.Rows[0]["SEIKYUU_ZEI_KEISAN_KBN_CD"].ToString();
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.form.SEIKYUU_OUTPUT_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_OUTPUT_KBN"].ToString();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_DAIHYOU_PRINT_KBN"]))
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_DAIHYOU_PRINT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_KYOTEN_PRINT_KBN"]))
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.SEIKYUU_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_KYOTEN_CD"]))
                {
                    this.form.SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["SEIKYUU_KYOTEN_CD"].ToString()));
                    this.form.SEIKYUU_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["SEIKYUU_KYOTEN_NAME"].ToString();
                }

                // マスタタブ（取引先-支払情報）
                this.form.SHIHARAI_DAY.Text = this.SearchResult.Rows[0]["SHIHARAI_DAY"].ToString();
                this.form.SHIHARAI_HOUHOU.Text = this.SearchResult.Rows[0]["SHIHARAI_HOUHOU"].ToString();
                this.form.SHIHARAI_HOUHOU_NAME.Text = this.SearchResult.Rows[0]["SHIHARAI_HOUHOU_NAME"].ToString();
                this.form.SHIHARAI_KEITAI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_KEITAI_KBN"].ToString();
                this.form.SHIHARAI_KINGAKU_HASUU_CD.Text = this.SearchResult.Rows[0]["SHIHARAI_KINGAKU_HASUU_CD"].ToString();
                //160028 S
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHIHARAI_KAISHUU_BETSU_KBN"]))
                {
                    this.form.SHIHARAI_KAISHUU_BETSU.Text = "1";
                }
                else
                {
                    this.form.SHIHARAI_KAISHUU_BETSU.Text = this.SearchResult.Rows[0]["SHIHARAI_KAISHUU_BETSU_KBN"].ToString();
                }
                this.form.SHIHARAI_KAISHUU_BETSU_NICHIGO.Text = this.SearchResult.Rows[0]["SHIHARAI_KAISHUU_BETSU_NICHIGO"].ToString();
                //160028 E
                this.form.SHIHARAI_MONTH.Text = this.SearchResult.Rows[0]["SHIHARAI_MONTH"].ToString();
                this.form.SHIHARAI_SHIMEBI1.Text = this.SearchResult.Rows[0]["SHIHARAI_SHIMEBI1"].ToString();
                //読み込み時、支払締日１がNull or Spaceの場合　Enable_falseにする
                if (string.IsNullOrWhiteSpace(this.form.SHIHARAI_SHIMEBI1.Text))
                {
                    this.form.SHIHARAI_SHIMEBI2.Enabled = false;
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI2.Text = this.SearchResult.Rows[0]["SHIHARAI_SHIMEBI2"].ToString();
                }
                if (string.IsNullOrWhiteSpace(this.form.SHIHARAI_SHIMEBI2.Text))
                {
                    this.form.SHIHARAI_SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.form.SHIHARAI_SHIMEBI3.Text = this.SearchResult.Rows[0]["SHIHARAI_SHIMEBI3"].ToString();
                }
                this.form.SHIHARAI_SHOSHIKI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_SHOSHIKI_KBN"].ToString();
                this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_SHOSHIKI_MEISAI_KBN"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHIHARAI_SHOSHIKI_GENBA_KBN"]))
                {
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = "";
                }
                else
                {
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_SHOSHIKI_GENBA_KBN"].ToString();
                }
                this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_SHUKKIN_MEISAI_KBN"].ToString();
                this.form.SHIHARAI_TAX_HASUU_CD.Text = this.SearchResult.Rows[0]["SHIHARAI_TAX_HASUU_CD"].ToString();
                this.form.SHIHARAI_TORIHIKI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_TORIHIKI_KBN"].ToString();
                this.form.SHIHARAI_YOUSHI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_YOUSHI_KBN"].ToString();
                this.form.SHIHARAI_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString();
                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.SearchResult.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHIHARAI_KYOTEN_PRINT_KBN"]))
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.SHIHARAI_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SHIHARAI_KYOTEN_CD"]))
                {
                    this.form.SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["SHIHARAI_KYOTEN_CD"].ToString()));
                    this.form.SHIHARAI_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["SHIHARAI_KYOTEN_NAME"].ToString();
                }

                // マスタタブ（業者）
                this.form.GYOUSHA_KEISHOU1.Text = this.SearchResult.Rows[0]["GYOUSHA_KEISHOU1"].ToString();
                this.form.GYOUSHA_KEISHOU2.Text = this.SearchResult.Rows[0]["GYOUSHA_KEISHOU2"].ToString();

                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN"]))
                {
                    this.form.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN"].ToString());
                }
                else
                {
                    this.form.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
                }
                this.form.GYOUSHA_JISHA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_JISHA_KBN"].ToString());
                this.form.GYOUSHA_KBN_MANI.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_KBN_MANI"].ToString());
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["TORIHIKISAKI_UMU_KBN"]))
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = "1";
                }
                else
                {
                    this.form.TORIHIKISAKI_UMU_KBN.Text = Convert.ToString(this.SearchResult.Rows[0]["TORIHIKISAKI_UMU_KBN"]);
                }
                this.form.GYOUSHA_KBN_SHUKKA.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_KBN_SHUKKA"].ToString());
                this.form.GYOUSHA_KBN_UKEIRE.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_KBN_UKEIRE"].ToString());
                this.form.GYOUSHA_MANI_HENSOUSAKI_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_MANI_HENSOUSAKI_KBN"].ToString());
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN"]))
                {
                    this.form.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN"].ToString());
                }
                else
                {
                    this.form.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;
                }
                this.form.GYOUSHA_TORIHIKI_CHUUSHI.Text = this.SearchResult.Rows[0]["GYOUSHA_TORIHIKI_CHUUSHI"].ToString();
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN"]))
                {
                    this.form.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN"].ToString());
                }
                else
                {
                    this.form.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN"]))
                {
                    this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN"]))
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_KYOTEN_CD"]))
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_KYOTEN_CD"].ToString()));
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["GYOUSHA_SEIKYUU_KYOTEN_NAME"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN"]))
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GYOUSHA_SHIHARAI_KYOTEN_CD"]))
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["GYOUSHA_SHIHARAI_KYOTEN_CD"].ToString()));
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["GYOUSHA_SHIHARAI_KYOTEN_NAME"].ToString();
                }

                //マニ記載業者チェックがついていないときは非活性
                //if (!this.form.GYOUSHA_KBN_MANI.Checked)
                //{
                //    //this.form.GYOUSHA_JISHA_KBN.Enabled = false;
                //    this.form.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
                //    this.form.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
                //    this.form.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
                //}

                // マスタタブ（現場）
                this.form.GENBA_KEISHOU1.Text = this.SearchResult.Rows[0]["GENBA_KEISHOU1"].ToString();
                this.form.GENBA_KEISHOU2.Text = this.SearchResult.Rows[0]["GENBA_KEISHOU2"].ToString();

                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_HAISHUTSU_NIZUMI_GENBA_KBN"]))
                {
                    this.form.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_HAISHUTSU_NIZUMI_GENBA_KBN"].ToString());
                }
                else
                {
                    this.form.GENBA_HAISHUTSU_NIZUMI_GENBA_KBN.Checked = false;
                }
                this.form.GENBA_JISHA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_JISHA_KBN"].ToString());
                this.form.GENBA_MANI_HENSOUSAKI_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_MANI_HENSOUSAKI_KBN"].ToString());
                this.form.GENBA_MANIFEST_SHURUI_CD.Text = this.SearchResult.Rows[0]["GENBA_MANIFEST_SHURUI_CD"].ToString();
                this.form.GENBA_MANIFEST_SHURUI_NAME.Text = this.SearchResult.Rows[0]["GENBA_MANIFEST_SHURUI_NAME"].ToString();
                this.form.GENBA_MANIFEST_TEHAI_CD.Text = this.SearchResult.Rows[0]["GENBA_MANIFEST_TEHAI_CD"].ToString();
                this.form.GENBA_MANIFEST_TEHAI_NAME.Text = this.SearchResult.Rows[0]["GENBA_MANIFEST_TEHAI_NAME"].ToString();
                this.form.GENBA_SAISHUU_SHOBUNJOU_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_SAISHUU_SHOBUNJOU_KBN"].ToString());
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SHOBUN_NIOROSHI_GENBA_KBN"]))
                {
                    this.form.GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_SHOBUN_NIOROSHI_GENBA_KBN"].ToString());
                }
                else
                {
                    this.form.GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked = false;
                }
                this.form.GENBA_TSUMIKAEHOKAN_KBN.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["GENBA_TSUMIKAEHOKAN_KBN"].ToString());
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SEIKYUU_DAIHYOU_PRINT_KBN"]))
                {
                    this.form.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GENBA_SEIKYUU_DAIHYOU_PRINT_KBN.Text = this.SearchResult.Rows[0]["GENBA_SEIKYUU_DAIHYOU_PRINT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SEIKYUU_KYOTEN_PRINT_KBN"]))
                {
                    this.form.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["GENBA_SEIKYUU_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SEIKYUU_KYOTEN_CD"]))
                {
                    this.form.GENBA_SEIKYUU_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["GENBA_SEIKYUU_KYOTEN_CD"].ToString()));
                    this.form.GENBA_SEIKYUU_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["GENBA_SEIKYUU_KYOTEN_NAME"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SHIHARAI_KYOTEN_PRINT_KBN"]))
                {
                    this.form.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.Text = this.SearchResult.Rows[0]["GENBA_SHIHARAI_KYOTEN_PRINT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_SHIHARAI_KYOTEN_CD"]))
                {
                    this.form.GENBA_SHIHARAI_KYOTEN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["GENBA_SHIHARAI_KYOTEN_CD"].ToString()));
                    this.form.GENBA_SHIHARAI_KYOTEN_NAME.Text = this.SearchResult.Rows[0]["GENBA_SHIHARAI_KYOTEN_NAME"].ToString();
                }

                this.form.GENBA_KANSAN_UNIT_CD.Text = this.SearchResult.Rows[0]["GENBA_KANSAN_UNIT_CD"].ToString();
                this.form.GENBA_KANSAN_UNIT_NAME.Text = this.SearchResult.Rows[0]["GENBA_KANSAN_UNIT_NAME"].ToString();
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["YOUKI_NYUU"]))
                {
                    this.form.YOUKI_NYUU.Checked = System.Convert.ToBoolean(Convert.ToString(this.SearchResult.Rows[0]["YOUKI_NYUU"]));
                }
                else
                {
                    this.form.YOUKI_NYUU.Checked = false;
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["JISSUU"]))
                {
                    this.form.JISSUU.Checked = System.Convert.ToBoolean(Convert.ToString(this.SearchResult.Rows[0]["JISSUU"]));
                }
                else
                {
                    this.form.JISSUU.Checked = false;
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_TEIKI_KEIYAKU_KBN"]))
                {
                    this.form.GENBA_TEIKI_KEIYAKU_KBN.Text = this.SearchResult.Rows[0]["GENBA_TEIKI_KEIYAKU_KBN"].ToString();
                    //this.form.GENBA_TEIKI_KEIYAKU_KBN_NAME.Text = this.SearchResult.Rows[0]["GENBA_TEIKI_KEIYAKU_KBN_NAME"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["GENBA_TEIKI_KEIJYOU_KBN"]))
                {
                    this.form.GENBA_TEIKI_KEIJYOU_KBN.Text = this.SearchResult.Rows[0]["GENBA_TEIKI_KEIJYOU_KBN"].ToString();
                    //this.form.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = this.SearchResult.Rows[0]["GENBA_TEIKI_KEIJYOU_KBN_NAME"].ToString();
                }
                if ("2".Equals(this.form.GENBA_TEIKI_KEIYAKU_KBN.Text))
                {
                    // 「2.単価」選択時のみ、集計単位は入力可
                    this.form.GENBA_TEIKI_KEIJYOU_KBN.Enabled = true;
                }
                else
                {
                    this.form.GENBA_TEIKI_KEIJYOU_KBN.Text = string.Empty;
                    this.form.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = string.Empty;
                    this.form.GENBA_TEIKI_KEIJYOU_KBN.Enabled = false;
                }

                // マスタタブ（入/出金先）
                this.form.NYUUKIN_TORIKOMI_KBN.Text = this.SearchResult.Rows[0]["NYUUKIN_TORIKOMI_KBN"].ToString();

                // マスタタブ（品名）
                this.form.HINMEI_DENPYOU_KBN_CD.Text = this.SearchResult.Rows[0]["HINMEI_DENPYOU_KBN_CD"].ToString();
                this.form.HINMEI_DENPYOU_KBN_NAME.Text = this.SearchResult.Rows[0]["HINMEI_DENPYOU_KBN_NAME"].ToString();
                this.form.HINMEI_DENSHU_KBN_CD.Text = this.SearchResult.Rows[0]["HINMEI_DENSHU_KBN_CD"].ToString();
                this.form.HINMEI_DENSHU_KBN_NAME.Text = this.SearchResult.Rows[0]["HINMEI_DENSHU_KBN_NAME"].ToString();
                this.form.HINMEI_UNIT_CD.Text = this.SearchResult.Rows[0]["HINMEI_UNIT_CD"].ToString();
                this.form.HINMEI_UNIT_NAME.Text = this.SearchResult.Rows[0]["HINMEI_UNIT_NAME"].ToString();
                this.form.HINMEI_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString();
                //抽出条件機能 が Null の場合 1．個別品名単価 を設定する
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HINMEI_SEARCH_CHUSYUTSU_JOKEN"]))
                {
                    this.form.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Text = "2";
                }
                else
                {
                    this.form.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Text = this.SearchResult.Rows[0]["HINMEI_SEARCH_CHUSYUTSU_JOKEN"].ToString();
                }
                //伝票区分機能 が Null の場合 1.売上 を設定する
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HINMEI_SEARCH_DENPYOU_KBN"]))
                {
                    this.form.HINMEI_SEARCH_DENPYOU_KBN.Text = "1";
                }
                else
                {
                    this.form.HINMEI_SEARCH_DENPYOU_KBN.Text = this.SearchResult.Rows[0]["HINMEI_SEARCH_DENPYOU_KBN"].ToString();
                }

                // マスタタブ（基本品名単価）
                this.form.KIHON_HINMEI_DEFAULT.Text = this.SearchResult.Rows[0]["KIHON_HINMEI_DEFAULT"].ToString();

                // マスタタブ（個別品名単価）
                this.form.KOBETSU_HINMEI_DEFAULT.Text = this.SearchResult.Rows[0]["KOBETSU_HINMEI_DEFAULT"].ToString();

                // マスタタブ（換算値）
                if (null != this.form.sub_tab.TabPages.Cast<TabPage>().Where(t => t.Name == "kansanchi_tabpage").FirstOrDefault())
                {
                    this.form.KANSAN_DEFAULT.Text = this.SearchResult.Rows[0]["KANSAN_DEFAULT"].ToString();
                    this.form.KANSAN_KIHON_UNIT_CD.Text = this.SearchResult.Rows[0]["KANSAN_KIHON_UNIT_CD"].ToString();
                    this.form.KANSAN_KIHON_UNIT_NAME.Text = this.SearchResult.Rows[0]["KANSAN_KIHON_UNIT_NAME"].ToString();
                    this.form.KANSAN_UNIT_CD.Text = this.SearchResult.Rows[0]["KANSAN_UNIT_CD"].ToString();
                    this.form.KANSAN_UNIT_NAME.Text = this.SearchResult.Rows[0]["KANSAN_UNIT_NAME"].ToString();
                }

                // マスタタブ（マニフェスト換算）
                this.form.MANI_KANSAN_KIHON_UNIT_CD.Text = this.SearchResult.Rows[0]["MANI_KANSAN_KIHON_UNIT_CD"].ToString();
                this.form.MANI_KANSAN_KIHON_UNIT_NAME.Text = this.SearchResult.Rows[0]["MANI_KANSAN_KIHON_UNIT_NAME"].ToString();
                this.form.MANI_KANSAN_UNIT_CD.Text = this.SearchResult.Rows[0]["MANI_KANSAN_UNIT_CD"].ToString();
                this.form.MANI_KANSAN_UNIT_NAME.Text = this.SearchResult.Rows[0]["MANI_KANSAN_UNIT_NAME"].ToString();

                // マスタタブ（委託契約登録）
                this.form.ITAKU_KEIYAKU_ALERT_AUTH.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_ALERT_AUTH"].ToString();
                this.form.ITAKU_KEIYAKU_CHECK.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_CHECK"].ToString();
                if (this.form.ITAKU_KEIYAKU_CHECK.Text == "1")
                {
                    this.form.pl_ITAKU_KEIYAKU_ALERT_AUTH.Enabled = true;
                }
                else
                {
                    this.form.pl_ITAKU_KEIYAKU_ALERT_AUTH.Enabled = false;
                }
                this.form.ITAKU_KEIYAKU_KOUSHIN_SHUBETSU.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_KOUSHIN_SHUBETSU"].ToString();
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["ITAKU_KEIYAKU_SUURYOU_FORMAT_CD"]))
                {
                    this.form.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_SUURYOU_FORMAT_CD"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TANKA_FORMAT_CD"]))
                {
                    this.form.ITAKU_KEIYAKU_TANKA_FORMAT_CD.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TANKA_FORMAT_CD"].ToString();
                }
                this.form.ITAKU_KEIYAKU_TYPE.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TYPE"].ToString();
                this.form.ITAKU_KEIYAKU_SHURUI.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_SHURUI"].ToString();
                this.form.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Text = this.SearchResult.Rows[0]["ITAKU_KEIYAKU_TOUROKU_HOUHOU"].ToString();

                // マスタタブ（コンテナ）
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["CONTENA_MAX_SET_KEIKA_DATE"]))
                {
                    this.form.CONTENA_MAX_SET_KEIKA_DATE.Text = "5";
                }
                else
                {
                    this.form.CONTENA_MAX_SET_KEIKA_DATE.Text = this.SearchResult.Rows[0]["CONTENA_MAX_SET_KEIKA_DATE"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["CONTENA_KANRI_HOUHOU"]))
                {
                    this.form.CONTENA_KANRI_HOUHOU.Text = "1";
                }
                else
                {
                    this.form.CONTENA_KANRI_HOUHOU.Text = this.SearchResult.Rows[0]["CONTENA_KANRI_HOUHOU"].ToString();
                }

                // 受付タブ
                this.form.UKETSUKE_UPN_GYOUSHA_CD_DEFALUT.Text = this.SearchResult.Rows[0]["UKETSUKE_UPN_GYOUSHA_CD_DEFALUT"].ToString();
                this.form.UKETSUKE_UPN_GYOUSHA_CD_DEFALUT_NAME.Text = this.SearchResult.Rows[0]["UKETSUKE_UPN_GYOUSHA_CD_DEFALUT_NAME"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UKETSUKE_SIJISHO_PRINT_KBN"]))
                {
                    this.form.UKETSUKE_SIJISHO_PRINT_KBN.Text = "1";
                }
                else
                {
                    this.form.UKETSUKE_SIJISHO_PRINT_KBN.Text = this.SearchResult.Rows[0]["UKETSUKE_SIJISHO_PRINT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UKETSUKE_SIJISHO_SUB_PRINT_KBN"]))
                {
                    this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Text = this.SearchResult.Rows[0]["UKETSUKE_SIJISHO_SUB_PRINT_KBN"].ToString();
                }
                if (this.ChangeUketsukeSijishoPrintKbn()) { return true; }

                // 配車タブ
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HAISHA_NIPPOU_LAYOUT_KBN"]))
                {
                    this.form.HAISHA_NIPPOU_LAYOUT_KBN.Text = "1";
                }
                else
                {
                    this.form.HAISHA_NIPPOU_LAYOUT_KBN.Text = this.SearchResult.Rows[0]["HAISHA_NIPPOU_LAYOUT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HAISHA_ONEDAY_NYUURYOKU_KBN"]))
                {
                    this.form.HAISHA_ONEDAY_NYUURYOKU_KBN.Text = "1";
                }
                else
                {
                    this.form.HAISHA_ONEDAY_NYUURYOKU_KBN.Text = this.SearchResult.Rows[0]["HAISHA_ONEDAY_NYUURYOKU_KBN"].ToString();
                }

                // VUNGUYEN 20150703 START
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HAISHA_WARIATE_KAISHI"]))
                {
                    this.form.HAISHA_WARIATE_KAISHI.Text = "1";
                }
                else
                {
                    this.form.HAISHA_WARIATE_KAISHI.Text = this.SearchResult.Rows[0]["HAISHA_WARIATE_KAISHI"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HAISHA_WARIATE_KUUHAKU"]))
                {
                    this.form.HAISHA_WARIATE_KUUHAKU.Text = "1";
                }
                else
                {
                    this.form.HAISHA_WARIATE_KUUHAKU.Text = this.SearchResult.Rows[0]["HAISHA_WARIATE_KUUHAKU"].ToString();
                }
                // VUNGUYEN 20150703 END

                // 受入タブ
                this.form.UKEIRE_CALC_BASE_KBN.Text = this.SearchResult.Rows[0]["UKEIRE_CALC_BASE_KBN"].ToString();
                this.form.UKEIRE_CHOUSEI_HASU_CD.Text = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_CD"].ToString();
                this.form.UKEIRE_CHOUSEI_HASU_KETA.Text = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_HASU_KETA"].ToString();
                this.form.UKEIRE_CHOUSEI_WARIAI_HASU_CD.Text = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_CD"].ToString();
                this.form.UKEIRE_CHOUSEI_WARIAI_HASU_KETA.Text = this.SearchResult.Rows[0]["UKEIRE_CHOUSEI_WARIAI_HASU_KETA"].ToString();
                this.form.UKEIRE_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["UKEIRE_KAKUTEI_USE_KBN"].ToString();
                this.form.UKEIRE_KAKUTEI_FLAG.Text = this.SearchResult.Rows[0]["UKEIRE_KAKUTEI_FLAG"].ToString();       // No.4089
                this.form.UKEIRE_ZANDAKA_JIDOU_KBN.Text = this.SearchResult.Rows[0]["UKEIRE_ZANDAKA_JIDOU_KBN"].ToString();
                this.form.UKEIRE_WARIFURI_HASU_CD.Text = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_CD"].ToString();
                this.form.UKEIRE_WARIFURI_HASU_KETA.Text = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_HASU_KETA"].ToString();
                this.form.UKEIRE_WARIFURI_WARIAI_HASU_CD.Text = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_CD"].ToString();
                this.form.UKEIRE_WARIFURI_WARIAI_HASU_KETA.Text = this.SearchResult.Rows[0]["UKEIRE_WARIFURI_WARIAI_HASU_KETA"].ToString();
                this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3"].ToString();
                this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3"].ToString();
                //160028 S
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UKEIRE_BARCODE_JOUDAN_KBN"]))
                {
                    this.form.UKEIRE_BARCODE_JOUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.UKEIRE_BARCODE_JOUDAN_KBN.Text = this.SearchResult.Rows[0]["UKEIRE_BARCODE_JOUDAN_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UKEIRE_BARCODE_CHUUDAN_KBN"]))
                {
                    this.form.UKEIRE_BARCODE_CHUUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.UKEIRE_BARCODE_CHUUDAN_KBN.Text = this.SearchResult.Rows[0]["UKEIRE_BARCODE_CHUUDAN_KBN"].ToString();
                }
                //160028 E
                //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
                //将軍-INXS
                if (AppConfig.AppOptions.IsInxsUketsuke())
                {
                    if (DBNull.Value.Equals(this.SearchResult.Rows[0]["HAISHA_HENKOU_SAGYOU_DATE_KBN"]))
                    {
                        this.form.HAISHA_HENKOU_SAGYOU_DATE_KBN.Text = "1";
                    }
                    else
                    {
                        this.form.HAISHA_HENKOU_SAGYOU_DATE_KBN.Text = this.SearchResult.Rows[0]["HAISHA_HENKOU_SAGYOU_DATE_KBN"].ToString();
                    }
                }
                //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END

                // 出荷タブ
                this.form.SHUKKA_CALC_BASE_KBN.Text = this.SearchResult.Rows[0]["SHUKKA_CALC_BASE_KBN"].ToString();
                this.form.SHUKKA_CHOUSEI_HASU_CD.Text = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_CD"].ToString();
                this.form.SHUKKA_CHOUSEI_HASU_KETA.Text = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_HASU_KETA"].ToString();
                this.form.SHUKKA_CHOUSEI_WARIAI_HASU_CD.Text = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_CD"].ToString();
                this.form.SHUKKA_CHOUSEI_WARIAI_HASU_KETA.Text = this.SearchResult.Rows[0]["SHUKKA_CHOUSEI_WARIAI_HASU_KETA"].ToString();
                this.form.SHUKKA_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["SHUKKA_KAKUTEI_USE_KBN"].ToString();
                this.form.SHUKKA_KAKUTEI_FLAG.Text = this.SearchResult.Rows[0]["SHUKKA_KAKUTEI_FLAG"].ToString();     // No.4089
                this.form.SHUKKA_ZANDAKA_JIDOU_KBN.Text = this.SearchResult.Rows[0]["SHUKKA_ZANDAKA_JIDOU_KBN"].ToString();
                this.form.SHUKKA_WARIFURI_HASU_CD.Text = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_CD"].ToString();
                this.form.SHUKKA_WARIFURI_HASU_KETA.Text = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_HASU_KETA"].ToString();
                this.form.SHUKKA_WARIFURI_WARIAI_HASU_CD.Text = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_CD"].ToString();
                this.form.SHUKKA_WARIFURI_WARIAI_HASU_KETA.Text = this.SearchResult.Rows[0]["SHUKKA_WARIFURI_WARIAI_HASU_KETA"].ToString();
                this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3"].ToString();
                this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3"].ToString();
                this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3"].ToString();
                this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3"].ToString();
                //160028 S
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHUKKA_BARCODE_JOUDAN_KBN"]))
                {
                    this.form.SHUKKA_BARCODE_JOUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.SHUKKA_BARCODE_JOUDAN_KBN.Text = this.SearchResult.Rows[0]["SHUKKA_BARCODE_JOUDAN_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHUKKA_BARCODE_CHUUDAN_KBN"]))
                {
                    this.form.SHUKKA_BARCODE_CHUUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.SHUKKA_BARCODE_CHUUDAN_KBN.Text = this.SearchResult.Rows[0]["SHUKKA_BARCODE_CHUUDAN_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KENSHUU_BARCODE_JOUDAN_KBN"]))
                {
                    this.form.KENSHUU_BARCODE_JOUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.KENSHUU_BARCODE_JOUDAN_KBN.Text = this.SearchResult.Rows[0]["KENSHUU_BARCODE_JOUDAN_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KENSHUU_BARCODE_CHUUDAN_KBN"]))
                {
                    this.form.KENSHUU_BARCODE_CHUUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.KENSHUU_BARCODE_CHUUDAN_KBN.Text = this.SearchResult.Rows[0]["KENSHUU_BARCODE_CHUUDAN_KBN"].ToString();
                }
                //160028 E
                // 売上/支払タブ
                this.form.UR_SH_CALC_BASE_KBN.Text = this.SearchResult.Rows[0]["UR_SH_CALC_BASE_KBN"].ToString();
                this.form.UR_SH_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["UR_SH_KAKUTEI_USE_KBN"].ToString();
                this.form.UR_SH_KAKUTEI_FLAG.Text = this.SearchResult.Rows[0]["UR_SH_KAKUTEI_FLAG"].ToString();      // No.4089
                this.form.UR_SH_ZANDAKA_JIDOU_KBN.Text = this.SearchResult.Rows[0]["UR_SH_ZANDAKA_JIDOU_KBN"].ToString();
                this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3"].ToString();
                this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1.Text = this.SearchResult.Rows[0]["UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1"].ToString();
                this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2.Text = this.SearchResult.Rows[0]["UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2"].ToString();
                this.form.UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3.Text = this.SearchResult.Rows[0]["UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3"].ToString();
                //160028 S
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UR_SH_BARCODE_JOUDAN_KBN"]))
                {
                    this.form.UR_SH_BARCODE_JOUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.UR_SH_BARCODE_JOUDAN_KBN.Text = this.SearchResult.Rows[0]["UR_SH_BARCODE_JOUDAN_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["UR_SH_BARCODE_CHUUDAN_KBN"]))
                {
                    this.form.UR_SH_BARCODE_CHUUDAN_KBN.Text = "2";
                }
                else
                {
                    this.form.UR_SH_BARCODE_CHUUDAN_KBN.Text = this.SearchResult.Rows[0]["UR_SH_BARCODE_CHUUDAN_KBN"].ToString();
                }
                //160028 E
                // 計量タブ
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_TORIHIKISAKI_DISP_KBN"]))
                {
                    this.form.KEIRYOU_TORIHIKISAKI_DISP_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_TORIHIKISAKI_DISP_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_TORIHIKISAKI_DISP_KBN"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_LAYOUT_KBN"]))
                {
                    this.form.KEIRYOU_LAYOUT_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_LAYOUT_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_LAYOUT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_GOODS_KBN"]))
                {
                    this.form.KEIRYOU_GOODS_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_GOODS_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_GOODS_KBN"].ToString();
                }
                this.form.KEIRYOU_HYOU_TITLE_1.Text = this.SearchResult.Rows[0]["KEIRYOU_HYOU_TITLE_1"].ToString();
                this.form.KEIRYOU_HYOU_TITLE_2.Text = this.SearchResult.Rows[0]["KEIRYOU_HYOU_TITLE_2"].ToString();
                this.form.KEIRYOU_HYOU_TITLE_3.Text = this.SearchResult.Rows[0]["KEIRYOU_HYOU_TITLE_3"].ToString();
                //this.form.KEIRYOU_SHOUMEI_1.Text = this.SearchResult.Rows[0]["KEIRYOU_SHOUMEI_1"].ToString();
                //this.form.KEIRYOU_SHOUMEI_2.Text = this.SearchResult.Rows[0]["KEIRYOU_SHOUMEI_2"].ToString();
                //this.form.KEIRYOU_SHOUMEI_3.Text = this.SearchResult.Rows[0]["KEIRYOU_SHOUMEI_3"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_BARCODE_KBN"]))
                {
                    this.form.KEIRYOU_BARCODE_KBN.Text = "2";
                }
                else
                {
                    this.form.KEIRYOU_BARCODE_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_BARCODE_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_KIHON_KEIRYOU"]))
                {
                    this.form.KEIRYOU_KIHON_KEIRYOU.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_KIHON_KEIRYOU.Text = this.SearchResult.Rows[0]["KEIRYOU_KIHON_KEIRYOU"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_HYOU_PRINT_KBN"]))
                {
                    this.form.KEIRYOU_HYOU_PRINT_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_HYOU_PRINT_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_HYOU_PRINT_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_HASU_CD"]))
                {
                    this.form.KEIRYOU_CHOUSEI_HASU_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_CHOUSEI_HASU_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_HASU_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_HASU_KETA"]))
                {
                    this.form.KEIRYOU_CHOUSEI_HASU_KETA.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_CHOUSEI_HASU_KETA.Text = this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_HASU_KETA"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_WARIAI_HASU_CD"]))
                {
                    this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_WARIAI_HASU_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_WARIAI_HASU_KETA"]))
                {
                    this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_CHOUSEI_WARIAI_HASU_KETA.Text = this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_WARIAI_HASU_KETA"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_USE_KBN"]))
                {
                    this.form.KEIRYOU_CHOUSEI_USE_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_CHOUSEI_USE_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_CHOUSEI_USE_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_YOUKI_USE_KBN"]))
                {
                    this.form.KEIRYOU_YOUKI_USE_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_YOUKI_USE_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_YOUKI_USE_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_TANKA_KINGAKU_USE_KBN"]))
                {
                    this.form.KEIRYOU_TANKA_KINGAKU_USE_KBN.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_TANKA_KINGAKU_USE_KBN.Text = this.SearchResult.Rows[0]["KEIRYOU_TANKA_KINGAKU_USE_KBN"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_TANKA_KINGAKU_USE_KBN"]))
                {
                    this.form.KEIRYOU_MANIFEST_HAIKI_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_MANIFEST_HAIKI_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_MANIFEST_HAIKI_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD"]))
                {
                    this.form.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD"]))
                {
                    this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_ZEI_KBN_CD"]))
                {
                    this.form.KEIRYOU_SEIKYUU_ZEI_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SEIKYUU_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SEIKYUU_ZEI_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD"]))
                {
                    this.form.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD"]))
                {
                    this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_ZEI_KBN_CD"]))
                {
                    this.form.KEIRYOU_SHIHARAI_ZEI_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SHIHARAI_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SHIHARAI_ZEI_KBN_CD"].ToString();
                }

                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_UKEIRE_KEITAI_KBN_CD"]))
                {
                    this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["KEIRYOU_UKEIRE_KEITAI_KBN_CD"].ToString()));
                    this.form.KEIRYOU_UKEIRE_KEITAI_KBN_NAME.Text = this.SearchResult.Rows[0]["KEIRYOU_UKEIRE_KEITAI_KBN_NAME"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_UKEIRE_KEIRYOU_KBN_CD"]))
                {
                    this.form.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_UKEIRE_KEIRYOU_KBN_CD"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SHUKKA_KEITAI_KBN_CD"]))
                {
                    this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["KEIRYOU_SHUKKA_KEITAI_KBN_CD"].ToString()));
                    this.form.KEIRYOU_SHUKKA_KEITAI_KBN_NAME.Text = this.SearchResult.Rows[0]["KEIRYOU_SHUKKA_KEITAI_KBN_NAME"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KEIRYOU_SHUKKA_KEIRYOU_KBN_CD"]))
                {
                    this.form.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text = "1";
                }
                else
                {
                    this.form.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text = this.SearchResult.Rows[0]["KEIRYOU_SHUKKA_KEIRYOU_KBN_CD"].ToString();
                }

                // 売上タブ
                this.form.URIAGE_HYOUJI_JOUKEN.Text = this.SearchResult.Rows[0]["URIAGE_HYOUJI_JOUKEN"].ToString();
                this.form.URIAGE_HYOUJI_JOUKEN_SHUKKA.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["URIAGE_HYOUJI_JOUKEN_SHUKKA"].ToString());
                this.form.URIAGE_HYOUJI_JOUKEN_UKEIRE.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["URIAGE_HYOUJI_JOUKEN_UKEIRE"].ToString());
                this.form.URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI"].ToString());
                this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text = this.SearchResult.Rows[0]["URIAGE_KAKUTEI_KAIJO_PASSWORD"].ToString();
                // パスワードの初期化
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;

                // 確定解除パスワードを参照するために、パスワードの後に値を設定しています
                this.form.URIAGE_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["URIAGE_KAKUTEI_USE_KBN"].ToString();
                if (this.ChangeUriageKakuteiUseKbn()) { return true; }

                ///// 支払タブを表示するときはここのコメントアウトを戻してコンストラクタでタブを削除しない /////
                //// 支払タブ
                //this.form.SHIHARAI_HYOUJI_JOUKEN.Text = this.SearchResult.Rows[0]["SHIHARAI_HYOUJI_JOUKEN"].ToString();
                //this.form.SHIHARAI_HYOUJI_JOUKEN_SHUKKA.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["SHIHARAI_HYOUJI_JOUKEN_SHUKKA"].ToString());
                //this.form.SHIHARAI_HYOUJI_JOUKEN_UKEIRE.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["SHIHARAI_HYOUJI_JOUKEN_UKEIRE"].ToString());
                //this.form.SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI"].ToString());
                //this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text = this.SearchResult.Rows[0]["SHIHARAI_KAKUTEI_KAIJO_PASSWORD"].ToString();
                //// パスワードの初期化
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                //this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                //this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                //this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                //this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;

                //// 確定解除パスワードを参照するために、パスワードの後に値を設定しています
                //this.form.SHIHARAI_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_KAKUTEI_USE_KBN"].ToString();
                //this.ChangeShiharaiKakuteiUseKbn();

                // 入金タブ
                this.form.NYUUKIN_HANDAN_BEGIN.Text = this.SearchResult.Rows[0]["NYUUKIN_HANDAN_BEGIN"].ToString();
                this.form.NYUUKIN_HANDAN_END.Text = this.SearchResult.Rows[0]["NYUUKIN_HANDAN_END"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["NYUUKIN_IKKATSU_KBN_DISP_SUU"]))
                {
                    this.form.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text = "1";
                }
                else
                {
                    this.form.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text = this.SearchResult.Rows[0]["NYUUKIN_IKKATSU_KBN_DISP_SUU"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD"]))
                {
                    this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text = "2";
                }
                else
                {
                    this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text = this.SearchResult.Rows[0]["NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD"].ToString();
                    if (this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text.Length > 0)
                    {
                        this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text = string.Format("{0:D" + this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.CharactersNumber + "}", Int16.Parse(this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD.Text));
                    }
                }
                this.form.NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD_NAME.Text = this.SearchResult.Rows[0]["NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD_NAME"].ToString();

                // 請求
                // 項目が変更予定のためなし
                //this.form.SEIKYUU_SHIME_SHORI_KBN.Text = this.SearchResult.Rows[0]["SEIKYUU_SHIME_SHORI_KBN"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SEIKYUU_SHIME_SEIKYUU_CHECK"]))
                {
                    this.form.SEIKYUU_SHIME_SEIKYUU_CHECK.Text = "1";
                }
                else
                {
                    this.form.SEIKYUU_SHIME_SEIKYUU_CHECK.Text = this.SearchResult.Rows[0]["SEIKYUU_SHIME_SEIKYUU_CHECK"].ToString();
                }
                //請求書備考
                this.form.SEIKYUU_BIKOU_1.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["SEIKYUU_BIKOU_1"]);
                this.form.SEIKYUU_BIKOU_2.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["SEIKYUU_BIKOU_2"]);
                //旧請求書
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["OLD_SEIKYUU_PRINT_KBN"]))
                {
                    this.form.OLD_SEIKYUU_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.OLD_SEIKYUU_PRINT_KBN.Text = this.SearchResult.Rows[0]["OLD_SEIKYUU_PRINT_KBN"].ToString();
                }
                //角印印刷位置
                this.form.KAKUIN_POSITION_TOP.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["KAKUIN_POSITION_TOP"]);
                this.form.KAKUIN_POSITION_LEFT.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["KAKUIN_POSITION_LEFT"]);
                //角印ファイル名
                if (fileLink != null)
                {
                    this.form.KAKUIN_FILE_NAME.Text = StringUtil.ConverToString(Path.GetFileName(this.fileData.FILE_PATH));
                    //各ボタンの活性/非活性
                    this.form.btnFileRef.Enabled = false;
                    this.form.btnUpload.Enabled = false;
                    this.form.btnBrowse.Enabled = true;
                    this.form.btnDelete.Enabled = true;
                }
                else
                {
                    this.form.KAKUIN_FILE_NAME.Text = String.Empty;
                    //各ボタンの活性/非活性
                    this.form.btnFileRef.Enabled = true;
                    this.form.btnUpload.Enabled = false;
                    this.form.btnBrowse.Enabled = false;
                    this.form.btnDelete.Enabled = false;
                }
                //角印サイズ
                if (AppConfig.AppOptions.IsFileUpload())
                {
                    if (DBNull.Value.Equals(this.SearchResult.Rows[0]["KAKUIN_SIZE"]))
                    {
                        this.form.KAKUIN_SIZE.Text = "1";
                    }
                    else
                    {
                        this.form.KAKUIN_SIZE.Text = this.SearchResult.Rows[0]["KAKUIN_SIZE"].ToString();
                    }
                }
                //車輛名印字
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHARYOU_NAME_INGI"]))
                {
                    this.form.SHARYOU_NAME_INGI.Text = "2";
                }
                else
                {
                    this.form.SHARYOU_NAME_INGI.Text = this.SearchResult.Rows[0]["SHARYOU_NAME_INGI"].ToString();
                }

                // 支払明細
                // 項目が変更予定のためなし
                //this.form.SHIHARAI_SHIME_SHORI_KBN.Text = this.SearchResult.Rows[0]["SHIHARAI_SHIME_SHORI_KBN"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["SHIHARAI_SHIME_SHIHARAI_CHECK"]))
                {
                    this.form.SHIHARAI_SHIME_SHIHARAI_CHECK.Text = "1";
                }
                else
                {
                    this.form.SHIHARAI_SHIME_SHIHARAI_CHECK.Text = this.SearchResult.Rows[0]["SHIHARAI_SHIME_SHIHARAI_CHECK"].ToString();
                }
                //支払明細書備考
                this.form.SHIHARAI_BIKOU_1.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["SHIHARAI_BIKOU_1"]);
                this.form.SHIHARAI_BIKOU_2.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["SHIHARAI_BIKOU_2"]);
                //旧支払明細書
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["OLD_SHIHARAI_PRINT_KBN"]))
                {
                    this.form.OLD_SHIHARAI_PRINT_KBN.Text = "2";
                }
                else
                {
                    this.form.OLD_SHIHARAI_PRINT_KBN.Text = this.SearchResult.Rows[0]["OLD_SHIHARAI_PRINT_KBN"].ToString();
                }

                // マニフェストタブ
                this.form.MANIFEST_JYUUYOU_DISP_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_JYUUYOU_DISP_KBN"].ToString();
                // 20140606 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する start
                this.form.MANIFEST_VALIDATION_CHECK.Text = this.SearchResult.Rows[0]["MANIFEST_VALIDATION_CHECK"].ToString();
                // 20140606 ria EV004285 マニフェスト登録の際に項目入力チェック機能を追加する end
                this.form.SANPAI_MANIFEST_CHECK.Text = this.SearchResult.Rows[0]["SANPAI_MANIFEST_MERCURY_CHECK"].ToString();
                this.form.KENPAI_MANIFEST_CHECK.Text = this.SearchResult.Rows[0]["KENPAI_MANIFEST_MERCURY_CHECK"].ToString();
                // 20140630 ria No.730 マニフェスト追加機能仕様_４ start
                this.form.MANIFEST_ENDDATE_USE_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_ENDDATE_USE_KBN"].ToString();
                //20150615 #1984 終了日警告日数に０を入力すると登録後ブランクになる hoanghm start
                //this.form.MANIFEST_UNPAN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString().Equals("0") ? "" : this.SearchResult.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString();
                //this.form.MANIFEST_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_SBN_DAYS"].ToString().Equals("0") ? "" : this.SearchResult.Rows[0]["MANIFEST_SBN_DAYS"].ToString();
                //this.form.MANIFEST_TOK_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString().Equals("0") ? "" : this.SearchResult.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString();
                //this.form.MANIFEST_LAST_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString().Equals("0") ? "" : this.SearchResult.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_UNPAN_DAYS"]))
                {
                    this.form.MANIFEST_UNPAN_DAYS.Text = "60";
                }
                else
                {
                    this.form.MANIFEST_UNPAN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_UNPAN_DAYS"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_SBN_DAYS"]))
                {
                    this.form.MANIFEST_SBN_DAYS.Text = "90";
                }
                else
                {
                    this.form.MANIFEST_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_SBN_DAYS"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_TOK_SBN_DAYS"]))
                {
                    this.form.MANIFEST_TOK_SBN_DAYS.Text = "60";
                }
                else
                {
                    this.form.MANIFEST_TOK_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_TOK_SBN_DAYS"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_LAST_SBN_DAYS"]))
                {
                    this.form.MANIFEST_LAST_SBN_DAYS.Text = "90";
                }
                else
                {
                    this.form.MANIFEST_LAST_SBN_DAYS.Text = this.SearchResult.Rows[0]["MANIFEST_LAST_SBN_DAYS"].ToString();
                }

                //20150615 #1984 終了日警告日数に０を入力すると登録後ブランクになる hoanghm end
                // 20140630 ria No.730 マニフェスト追加機能仕様_４ end
                this.form.MANIFEST_OSHIRASE_DISP_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_OSHIRASE_DISP_KBN"].ToString();
                this.form.MANIFEST_REPORT_SUU_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_REPORT_SUU_KBN"].ToString();
                this.form.MANIFEST_RIREKI_DISP_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_RIREKI_DISP_KBN"].ToString();
                this.form.MANIFEST_SUURYO_FORMAT_CD.Text = this.SearchResult.Rows[0]["MANIFEST_SUURYO_FORMAT_CD"].ToString();
                this.form.MANIFEST_TUUCHI_BEGIN.Text = this.SearchResult.Rows[0]["MANIFEST_TUUCHI_BEGIN"].ToString();
                this.form.MANIFEST_TUUCHI_END.Text = this.SearchResult.Rows[0]["MANIFEST_TUUCHI_END"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_A"]))
                {
                    this.form.MANIFEST_USE_A.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_A.Text = this.SearchResult.Rows[0]["MANIFEST_USE_A"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_B1"]))
                {
                    this.form.MANIFEST_USE_B1.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B1.Text = this.SearchResult.Rows[0]["MANIFEST_USE_B1"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_B2"]))
                {
                    this.form.MANIFEST_USE_B2.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B2.Text = this.SearchResult.Rows[0]["MANIFEST_USE_B2"].ToString();
                }

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_B4"]))
                {
                    this.form.MANIFEST_USE_B4.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B4.Text = this.SearchResult.Rows[0]["MANIFEST_USE_B4"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_B6"]))
                {
                    this.form.MANIFEST_USE_B6.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_B6.Text = this.SearchResult.Rows[0]["MANIFEST_USE_B6"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_C1"]))
                {
                    this.form.MANIFEST_USE_C1.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_C1.Text = this.SearchResult.Rows[0]["MANIFEST_USE_C1"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_C2"]))
                {
                    this.form.MANIFEST_USE_C2.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_C2.Text = this.SearchResult.Rows[0]["MANIFEST_USE_C2"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_D"]))
                {
                    this.form.MANIFEST_USE_D.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_D.Text = this.SearchResult.Rows[0]["MANIFEST_USE_D"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_USE_E"]))
                {
                    this.form.MANIFEST_USE_E.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_USE_E.Text = this.SearchResult.Rows[0]["MANIFEST_USE_E"].ToString();
                }

                this.form.MANIFEST_HENSOU_NATSUIN_1.Text = this.SearchResult.Rows[0]["MANIFEST_HENSOU_NATSUIN_1"].ToString();
                this.form.MANIFEST_HENSOU_NATSUIN_2.Text = this.SearchResult.Rows[0]["MANIFEST_HENSOU_NATSUIN_2"].ToString();
                this.form.MANIFEST_HENSOU_RENRAKU_1.Text = this.SearchResult.Rows[0]["MANIFEST_HENSOU_RENRAKU_1"].ToString();
                this.form.MANIFEST_HENSOU_RENRAKU_2.Text = this.SearchResult.Rows[0]["MANIFEST_HENSOU_RENRAKU_2"].ToString();
                this.form.MANIFEST_PRINTERNAME.DataSource = this.defaultMarginSettings;
                this.form.MANIFEST_PRINTERNAME.SelectedValue = this.SearchResult.Rows[0]["MANIFEST_PRINTERNAME"].ToString();

                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["MANIFEST_RENKEI_KBN"]))
                {
                    this.form.MANIFEST_RENKEI_KBN.Text = "1";
                }
                else
                {
                    this.form.MANIFEST_RENKEI_KBN.Text = this.SearchResult.Rows[0]["MANIFEST_RENKEI_KBN"].ToString();
                }

                // 見積タブ
                this.form.MITSUMORI_SUBJECT_DEFAULT.Text = this.SearchResult.Rows[0]["MITSUMORI_SUBJECT_DEFAULT"].ToString();

                //20250416
                this.form.MITSUMORI_SUBJECT_DEFAULT_1.Text = this.SearchResult.Rows[0]["MITSUMORI_SUBJECT_DEFAULT_1"].ToString();

                this.form.MITSUMORI_KOUMOKU1.Text = this.SearchResult.Rows[0]["MITSUMORI_KOUMOKU1"].ToString();
                this.form.MITSUMORI_KOUMOKU2.Text = this.SearchResult.Rows[0]["MITSUMORI_KOUMOKU2"].ToString();
                this.form.MITSUMORI_KOUMOKU3.Text = this.SearchResult.Rows[0]["MITSUMORI_KOUMOKU3"].ToString();
                this.form.MITSUMORI_KOUMOKU4.Text = this.SearchResult.Rows[0]["MITSUMORI_KOUMOKU4"].ToString();

                this.form.MITSUMORI_KOUMOKU5.Text = this.SearchResult.Rows[0]["MITSUMORI_KOUMOKU5"].ToString();

                this.form.MITSUMORI_NAIYOU1.Text = this.SearchResult.Rows[0]["MITSUMORI_NAIYOU1"].ToString();
                this.form.MITSUMORI_NAIYOU2.Text = this.SearchResult.Rows[0]["MITSUMORI_NAIYOU2"].ToString();
                this.form.MITSUMORI_NAIYOU3.Text = this.SearchResult.Rows[0]["MITSUMORI_NAIYOU3"].ToString();
                this.form.MITSUMORI_NAIYOU4.Text = this.SearchResult.Rows[0]["MITSUMORI_NAIYOU4"].ToString();

                this.form.MITSUMORI_NAIYOU5.Text = this.SearchResult.Rows[0]["MITSUMORI_NAIYOU5"].ToString();

                this.form.MITSUMORI_BIKOU1.Text = this.SearchResult.Rows[0]["MITSUMORI_BIKOU1"].ToString();
                this.form.MITSUMORI_BIKOU2.Text = this.SearchResult.Rows[0]["MITSUMORI_BIKOU2"].ToString();
                this.form.MITSUMORI_BIKOU3.Text = this.SearchResult.Rows[0]["MITSUMORI_BIKOU3"].ToString();
                this.form.MITSUMORI_BIKOU4.Text = this.SearchResult.Rows[0]["MITSUMORI_BIKOU4"].ToString();
                this.form.MITSUMORI_BIKOU5.Text = this.SearchResult.Rows[0]["MITSUMORI_BIKOU5"].ToString();
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["BUSHO_NAME_PRINT"]))
                {
                    this.form.BUSHO_NAME_PRINT.Text = "1";
                }
                else
                {
                    this.form.BUSHO_NAME_PRINT.Text = this.SearchResult.Rows[0]["BUSHO_NAME_PRINT"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_CD1"]))
                {
                    this.form.INJI_KYOTEN_CD1.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_CD1"].ToString()));
                    this.form.INJI_KYOTEN_NAME_RYAKU1.Text = this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_NAME1"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_CD2"]))
                {
                    this.form.INJI_KYOTEN_CD2.Text = string.Format("{0:D2}", Int16.Parse(this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_CD2"].ToString()));
                    this.form.INJI_KYOTEN_NAME_RYAKU2.Text = this.SearchResult.Rows[0]["MITSUMORI_INJI_KYOTEN_NAME2"].ToString();
                }
                this.form.MITSUMORI_ZEIKEISAN_KBN_CD.Text = this.SearchResult.Rows[0]["MITSUMORI_ZEIKEISAN_KBN_CD"].ToString();
                this.form.MITSUMORI_ZEI_KBN_CD.Text = this.SearchResult.Rows[0]["MITSUMORI_ZEI_KBN_CD"].ToString();
                // 在庫タブ
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["ZAIKO_KANRI"]))
                {
                    this.form.ZAIKO_KANRI.Text = "1";
                }
                else
                {
                    this.form.ZAIKO_KANRI.Text = this.SearchResult.Rows[0]["ZAIKO_KANRI"].ToString();
                }
                if (DBNull.Value.Equals(this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"]) || (this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"].ToString() != "1" && this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"].ToString() != "2"))
                {
                    this.form.ZAIKO_HYOUKA_HOUHOU.Text = "1";
                }
                else
                {
                    this.form.ZAIKO_HYOUKA_HOUHOU.Text = this.SearchResult.Rows[0]["ZAIKO_HYOUKA_HOUHOU"].ToString();
                }

                // 代納タブ
                this.form.DAINO_CALC_BASE_KBN.Text = this.SearchResult.Rows[0]["DAINO_CALC_BASE_KBN"].ToString();
                // 代納の確定区分が使えるようになるまでの暫定的修正
                //this.form.DAINO_KAKUTEI_USE_KBN.Text = this.SearchResult.Rows[0]["DAINO_KAKUTEI_USE_KBN"].ToString();
                this.form.DAINO_KAKUTEI_USE_KBN.Text = "2";
                this.form.DAINO_KAKUTEI_FLAG.Text = this.SearchResult.Rows[0]["DAINO_KAKUTEI_FLAG"].ToString();
                this.form.DAINO_ZANDAKA_JIDOU_KBN.Text = this.SearchResult.Rows[0]["DAINO_ZANDAKA_JIDOU_KBN"].ToString();

                // 電子契約タブ
                // オプションがONの場合のみ
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    this.form.DENSHI_KEIYAKU_MESSAGE.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_MESSAGE"].ToString();
                    if (DBNull.Value.Equals(this.SearchResult.Rows[0]["DENSHI_KEIYAKU_SOUFUHOUHOU"]))
                    {
                        this.form.DENSHI_KEIYAKU_SOUFUHOUHOU.Text = "2";
                    }
                    else
                    {
                        this.form.DENSHI_KEIYAKU_SOUFUHOUHOU.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_SOUFUHOUHOU"].ToString();
                    }
                    if (DBNull.Value.Equals(this.SearchResult.Rows[0]["DENSHI_KEIYAKU_ACCESS_CODE_CHECK"]))
                    {
                        this.form.DENSHI_KEIYAKU_ACCESS_CODE_CHECK.Text = "1";
                    }
                    else
                    {
                        this.form.DENSHI_KEIYAKU_ACCESS_CODE_CHECK.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_ACCESS_CODE_CHECK"].ToString();
                    }
                    this.form.DENSHI_KEIYAKU_ACCESS_CODE.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_ACCESS_CODE"].ToString();
                    this.form.DENSHI_KEIYAKU_X_APP_ID.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_X_APP_ID"].ToString();
                    this.form.DENSHI_KEIYAKU_KYOUYUUSAKI_CC.Text = this.SearchResult.Rows[0]["DENSHI_KEIYAKU_KYOUYUUSAKI_CC"].ToString();
                }

                // デジタコ項目を設定
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DIGI_CORP_ID"]))
                {
                    this.form.digiCorpId = this.SearchResult.Rows[0]["DIGI_CORP_ID"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DIGI_RANGE_RADIUS"]))
                {
                    this.form.digiRangeRadius = Convert.ToInt16(this.SearchResult.Rows[0]["DIGI_RANGE_RADIUS"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DIGI_UID"]))
                {
                    this.form.digiUserId = this.SearchResult.Rows[0]["DIGI_UID"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DIGI_PWD"]))
                {
                    this.form.digiPassword = this.SearchResult.Rows[0]["DIGI_PWD"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DIGI_CARRY_ORVER_NEXT_DAY"]))
                {
                    this.form.digiCarryOverNextDay = Convert.ToInt16(this.SearchResult.Rows[0]["DIGI_CARRY_ORVER_NEXT_DAY"].ToString());
                }

                // NAVITIME項目を設定
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_CORP_ID"]))
                {
                    this.form.naviCorpId = this.SearchResult.Rows[0]["NAVI_CORP_ID"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_ACCOUNT"]))
                {
                    this.form.naviAccount = this.SearchResult.Rows[0]["NAVI_ACCOUNT"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_PWD"]))
                {
                    this.form.naviPassword = this.SearchResult.Rows[0]["NAVI_PWD"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_SAGYOU_TIME"]))
                {
                    this.form.naviSagyouTime = Convert.ToInt16(this.SearchResult.Rows[0]["NAVI_SAGYOU_TIME"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_TRAFFIC"]))
                {
                    this.form.naviTraffic = (bool)this.SearchResult.Rows[0]["NAVI_TRAFFIC"];
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_SMART_IC"]))
                {
                    this.form.naviSmartIc = (bool)this.SearchResult.Rows[0]["NAVI_SMART_IC"];
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_TOLL"]))
                {
                    this.form.naviToll = (bool)this.SearchResult.Rows[0]["NAVI_TOLL"];
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["NAVI_GET_TIME"]))
                {
                    this.form.naviGetTime = Convert.ToInt16(this.SearchResult.Rows[0]["NAVI_GET_TIME"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["MAX_WINDOW_COUNT"]))
                {
                    this.form.maxWindowCount = Convert.ToInt16(this.SearchResult.Rows[0]["MAX_WINDOW_COUNT"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["UKEIRESHUKA_GAMEN_SIZE"]))
                {
                    this.form.UKEIRESHUKA_GAMEN_SIZE = Convert.ToInt16(this.SearchResult.Rows[0]["UKEIRESHUKA_GAMEN_SIZE"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DENPYOU_HAKOU_HYOUJI"]))
                {
                    this.form.DENPYOU_HAKOU_HYOUJI = Convert.ToInt16(this.SearchResult.Rows[0]["DENPYOU_HAKOU_HYOUJI"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["MAX_INSERT_CAPACITY"]))
                {
                    this.form.maxInsertCapacity = Convert.ToInt16(this.SearchResult.Rows[0]["MAX_INSERT_CAPACITY"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["MAX_FILE_SIZE"]))
                {
                    this.form.maxFileSize = Convert.ToDecimal(this.SearchResult.Rows[0]["MAX_FILE_SIZE"].ToString());
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DB_FILE_CONNECT"]))
                {
                    this.form.dbFileConnect = this.SearchResult.Rows[0]["DB_FILE_CONNECT"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DB_INXS_SUBAPP_CONNECT_STRING"]))
                {
                    this.form.dbInxsSubappConnectString = this.SearchResult.Rows[0]["DB_INXS_SUBAPP_CONNECT_STRING"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DB_INXS_SUBAPP_CONNECT_NAME"]))
                {
                    this.form.dbInxsSubappConnectName = this.SearchResult.Rows[0]["DB_INXS_SUBAPP_CONNECT_NAME"].ToString();
                }

                //CongBinh 20210712 #152799 S
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SUPPORT_KBN"]))
                {
                    this.form.SupportKbn = this.SearchResult.Rows[0]["SUPPORT_KBN"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SUPPORT_URL"]))
                {
                    this.form.SupportUrl = this.SearchResult.Rows[0]["SUPPORT_URL"].ToString();
                }
                //CongBinh 20210712 #152799 E
                //QN_QUAN add 20211229 #158952 S
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["DB_LOG_CONNECT"]))
                {
                    this.form.dbLOGConnect = this.SearchResult.Rows[0]["DB_LOG_CONNECT"].ToString();
                }
                //QN_QUAN add 20211229 #158952 E

                //160029 S
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SYS_BARCODO_SHINKAKU_KBN"]))
                {
                    this.form.SYS_BARCODO_SHINKAKU_KBN = Convert.ToInt16(this.SearchResult.Rows[0]["SYS_BARCODO_SHINKAKU_KBN"].ToString());
                }
                //160029 E

                //PhuocLoc 2022/01/04 #158897, #158898 -Start
                if (AppConfig.AppOptions.IsWANSign())
                {
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SECRET_KEY"]))
                    {
                        this.form.ModSecretKey = this.SearchResult.Rows[0]["SECRET_KEY"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["CUSTOMER_ID"]))
                    {
                        this.form.ModCustomerId = this.SearchResult.Rows[0]["CUSTOMER_ID"].ToString();
                    }

                    // 備考
                    this.form.WAN_SIGN_BIKOU_1.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_BIKOU_1"]);
                    this.form.WAN_SIGN_BIKOU_2.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_BIKOU_2"]);
                    this.form.WAN_SIGN_BIKOU_3.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_BIKOU_3"]);

                    // フィールド
                    if (DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_1"]))
                    {
                        this.form.beforeValue1 = this.form.isNotUsing;
                        this.form.WAN_SIGN_FIELD_1.Text = this.form.isNotUsing;
                    }
                    else
                    {
                        this.form.beforeValue1 = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_1"].ToString();
                        this.form.WAN_SIGN_FIELD_1.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_1"].ToString();
                    }
                    this.FieldControlSetting(1, this.form.WAN_SIGN_FIELD_1.Text);

                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_2"]))
                    {
                        this.form.beforeValue2 = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_2"].ToString();
                        this.form.WAN_SIGN_FIELD_2.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_2"].ToString();
                    }
                    this.FieldControlSetting(2, this.form.WAN_SIGN_FIELD_2.Text);

                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_3"]))
                    {
                        this.form.beforeValue3 = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_3"].ToString();
                        this.form.WAN_SIGN_FIELD_3.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_3"].ToString();
                    }
                    this.FieldControlSetting(3, this.form.WAN_SIGN_FIELD_3.Text);

                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_4"]))
                    {
                        this.form.beforeValue4 = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_4"].ToString();
                        this.form.WAN_SIGN_FIELD_4.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_4"].ToString();
                    }
                    this.FieldControlSetting(4, this.form.WAN_SIGN_FIELD_4.Text);

                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_5"]))
                    {
                        this.form.WAN_SIGN_FIELD_5.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_5"].ToString();
                    }
                    this.FieldControlSetting(5, this.form.WAN_SIGN_FIELD_5.Text);

                    //フィールド名称
                    this.form.WAN_SIGN_FIELD_NAME_1.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_NAME_1"]);
                    this.form.WAN_SIGN_FIELD_NAME_2.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_NAME_2"]);
                    this.form.WAN_SIGN_FIELD_NAME_3.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_NAME_3"]);
                    this.form.WAN_SIGN_FIELD_NAME_4.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_NAME_4"]);
                    this.form.WAN_SIGN_FIELD_NAME_5.Text = StringUtil.ConverToString(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_NAME_5"]);

                    // フィールド属性
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_1"]))
                    {
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_1"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_2"]))
                    {
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_2"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_3"]))
                    {
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_3"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_4"]))
                    {
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_4"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_5"]))
                    {
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = this.SearchResult.Rows[0]["WAN_SIGN_FIELD_ATTRIBUTE_5"].ToString();
                    }
                }
                //PhuocLoc 2022/01/04 #158897, #158898 -End

                // 空電プッシュ
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["KARADEN_ACCESS_KEY"]))
                {
                    this.form.KaradenAccessKey = this.SearchResult.Rows[0]["KARADEN_ACCESS_KEY"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["KARADEN_SECURITY_CODE"]))
                {
                    this.form.karadenSecurityCode = this.SearchResult.Rows[0]["KARADEN_SECURITY_CODE"].ToString();
                }
                if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["KARADEN_MAX_WORD_COUNT"]))
                {
                    this.form.KaradenMaxWordCount = Convert.ToInt16(this.SearchResult.Rows[0]["KARADEN_MAX_WORD_COUNT"].ToString());
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞ
                if (AppConfig.AppOptions.IsSMS())
                {
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_ALERT_CHARACTER_LIMIT"]))
                    {
                        this.form.SMS_ALERT_CHARACTER_LIMIT.Text = this.SearchResult.Rows[0]["SMS_ALERT_CHARACTER_LIMIT"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_SEND_JOKYO"]))
                    {
                        this.form.SMS_SEND_JOKYO.Text = this.SearchResult.Rows[0]["SMS_SEND_JOKYO"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_DENPYOU_SHURUI"]))
                    {
                        this.form.SMS_DENPYOU_SHURUI.Text = this.SearchResult.Rows[0]["SMS_DENPYOU_SHURUI"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_HAISHA_JOKYO"]))
                    {
                        this.form.SMS_HAISHA_JOKYO.Text = this.SearchResult.Rows[0]["SMS_HAISHA_JOKYO"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_GREETINGS"]))
                    {
                        // 初期化
                        this.form.GREETINGS.Text = string.Empty;
                        // 値セット
                        this.form.GREETINGS.Text = this.SearchResult.Rows[0]["SMS_GREETINGS"].ToString();
                    }
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["SMS_SIGNATURE"]))
                    {
                        // 初期化
                        this.form.SIGNATURE.Text = string.Empty;
                        // 値セット
                        this.form.SIGNATURE.Text = this.SearchResult.Rows[0]["SMS_SIGNATURE"].ToString();
                    }
                }

                // 楽楽明細連携
                if (AppConfig.AppOptions.IsRakurakuMeisai())
                {
                    if (!DBNull.Value.Equals(this.SearchResult.Rows[0]["RAKURAKU_CODE_NUMBERING_KBN"]))
                    {
                        this.form.RakurakuCodeKbn = this.SearchResult.Rows[0]["RAKURAKU_CODE_NUMBERING_KBN"].ToString();
                    }
                }

                #region 20250303
                //受付
                this.form.SHONIN_RAN_1.Text = this.SearchResult.Rows[0]["SHONIN_RAN_1"].ToString();
                this.form.SHONIN_RAN_2.Text = this.SearchResult.Rows[0]["SHONIN_RAN_2"].ToString();
                this.form.SHONIN_RAN_3.Text = this.SearchResult.Rows[0]["SHONIN_RAN_3"].ToString();
                this.form.SHONIN_RAN_4.Text = this.SearchResult.Rows[0]["SHONIN_RAN_4"].ToString();
                this.form.SHONIN_RAN_5.Text = this.SearchResult.Rows[0]["SHONIN_RAN_5"].ToString();
                this.form.SHONIN_RAN_6.Text = this.SearchResult.Rows[0]["SHONIN_RAN_6"].ToString();
                this.form.SHONIN_RAN_7.Text = this.SearchResult.Rows[0]["SHONIN_RAN_7"].ToString();
                this.form.SHONIN_RAN_8.Text = this.SearchResult.Rows[0]["SHONIN_RAN_8"].ToString();
                this.form.SHONIN_RAN_9.Text = this.SearchResult.Rows[0]["SHONIN_RAN_9"].ToString();
                this.form.SHONIN_RAN_10.Text = this.SearchResult.Rows[0]["SHONIN_RAN_10"].ToString();

                //4
                this.form.HIDZUKE_INJI_CHECK.Text = this.SearchResult.Rows[0]["HIDZUKE_INJI_CHECK"].ToString();
                #endregion 20250303

                #region 20250304
                //掲示板
                this.form.KOKAI_KIKAN_FROM.Value = Convert.ToDateTime(this.SearchResult.Rows[0]["KOKAI_KIKAN_FROM"].ToString());
                this.form.KOKAI_KIKAN_TO.Value = Convert.ToDateTime(this.SearchResult.Rows[0]["KOKAI_KIKAN_TO"].ToString());
                this.form.KEIJIBAN_TEKISUTO_BOKKUSU.Text = this.SearchResult.Rows[0]["KEIJIBAN_TEKISUTO_BOKKUSU"].ToString();

                //計量タブ
                this.form.KEIRYO_ORIJINARU.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["KEIRYO_ORIJINARU"].ToString());

                #endregion 20250304

                #region 20250305
                //システムﾀﾌﾞ ctrl + k + c / u
                this.form.FURIKOMI_BANK_CD.Text = this.SearchResult.Rows[0]["FURIKOMI_BANK_CD"].ToString();
                this.form.FURIKOMI_BANK_SHITEN_CD.Text = this.SearchResult.Rows[0]["FURIKOMI_BANK_SHITEN_CD"].ToString();
                this.form.KOUZA_SHURUI.Text = this.SearchResult.Rows[0]["KOUZA_SHURUI"].ToString();
                this.form.KOUZA_NO.Text = this.SearchResult.Rows[0]["KOUZA_NO"].ToString();
                this.form.KOUZA_NAME.Text = this.SearchResult.Rows[0]["KOUZA_NAME"].ToString();

                #endregion 20250305

                #region 20250306
                //システムﾀﾌﾞ
                this.form.FURIKOMI_BANK_NAME.Text = this.SearchResult.Rows[0]["FURIKOMI_BANK_NAME"].ToString();
                this.form.FURIKOMI_BANK_SHITEN_NAME.Text = this.SearchResult.Rows[0]["FURIKOMI_BANK_SHITEN_NAME"].ToString();

                #endregion 20250306

                #region 20250307
                this.form.HIDZUKE_CHECK.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["HIDZUKE_CHECK"].ToString());
                this.form.SHORI_RAIN_CHECK.Checked = System.Convert.ToBoolean(this.SearchResult.Rows[0]["SHORI_RAIN_CHECK"].ToString());

                #endregion 20250307

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        //PhuocLoc 2022/01/04 #158897, #158898 -Start
        internal void FieldControlSetting(int levelKbn, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            switch (levelKbn)
            {
                case 1:
                    if (text == this.form.isUsing)
                    {
                        this.form.WAN_SIGN_FIELD_NAME_1.ReadOnly = false;
                        this.form.WAN_SIGN_FIELD_NAME_1.TabStop = true;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = "1";
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_1.Enabled = true;
                        this.form.beforeValue2 = this.form.isNotUsing;
                        this.form.WAN_SIGN_FIELD_2.Text = this.form.isNotUsing;
                        this.form.pn_WAN_SIGN_FIELD_2.Enabled = true;
                    }
                    else
                    {
                        this.form.WAN_SIGN_FIELD_NAME_1.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_1.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_1.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_1.Enabled = false;

                        this.form.WAN_SIGN_FIELD_2.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_2.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_2.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_2.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_2.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_2.Enabled = false;

                        this.form.WAN_SIGN_FIELD_3.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_3.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_3.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_3.Enabled = false;

                        this.form.WAN_SIGN_FIELD_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_4.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_4.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_4.Enabled = false;

                        this.form.WAN_SIGN_FIELD_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_5.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = false;
                    }
                    break;
                case 2:
                    if (text == this.form.isUsing)
                    {
                        this.form.WAN_SIGN_FIELD_NAME_2.ReadOnly = false;
                        this.form.WAN_SIGN_FIELD_NAME_2.TabStop = true;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = "1";
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_2.Enabled = true;
                        this.form.beforeValue3 = this.form.isNotUsing;
                        this.form.WAN_SIGN_FIELD_3.Text = this.form.isNotUsing;
                        this.form.pn_WAN_SIGN_FIELD_3.Enabled = true;
                    }
                    else
                    {
                        this.form.WAN_SIGN_FIELD_NAME_2.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_2.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_2.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_2.Enabled = false;

                        this.form.WAN_SIGN_FIELD_3.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_3.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_3.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_3.Enabled = false;

                        this.form.WAN_SIGN_FIELD_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_4.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_4.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_4.Enabled = false;

                        this.form.WAN_SIGN_FIELD_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_5.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = false;
                    }
                    break;
                case 3:
                    if (text == this.form.isUsing)
                    {
                        this.form.WAN_SIGN_FIELD_NAME_3.ReadOnly = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.TabStop = true;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = "1";
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_3.Enabled = true;
                        this.form.beforeValue4 = this.form.isNotUsing;
                        this.form.WAN_SIGN_FIELD_4.Text = this.form.isNotUsing;
                        this.form.pn_WAN_SIGN_FIELD_4.Enabled = true;
                    }
                    else
                    {
                        this.form.WAN_SIGN_FIELD_NAME_3.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_3.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_3.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_3.Enabled = false;

                        this.form.WAN_SIGN_FIELD_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_4.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_4.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_4.Enabled = false;

                        this.form.WAN_SIGN_FIELD_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_5.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = false;
                    }
                    break;
                case 4:
                    if (text == this.form.isUsing)
                    {
                        this.form.WAN_SIGN_FIELD_NAME_4.ReadOnly = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.TabStop = true;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = "1";
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_4.Enabled = true;
                        this.form.WAN_SIGN_FIELD_5.Text = this.form.isNotUsing;
                        this.form.pn_WAN_SIGN_FIELD_5.Enabled = true;
                    }
                    else
                    {
                        this.form.WAN_SIGN_FIELD_NAME_4.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_4.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_4.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_4.Enabled = false;

                        this.form.WAN_SIGN_FIELD_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_5.Enabled = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = false;
                    }
                    break;
                case 5:
                    if (text == this.form.isUsing)
                    {
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = true;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = "1";
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = true;
                    }
                    else
                    {
                        this.form.WAN_SIGN_FIELD_NAME_5.ReadOnly = true;
                        this.form.WAN_SIGN_FIELD_NAME_5.TabStop = false;
                        this.form.WAN_SIGN_FIELD_NAME_5.Text = string.Empty;
                        this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = string.Empty;
                        this.form.pn_WAN_SIGN_FIELD_ATTRIBUTE_5.Enabled = false;
                    }
                    break;
            }
        }
        //PhuocLoc 2022/01/04 #158897, #158898 -End

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //システム設定入力(初回登録)ボタン(F1)イベント生成
            this.form.C_Regist(parentForm.bt_func1);
            parentForm.bt_func1.Click += new EventHandler(this.form.Initial);
            parentForm.bt_func1.ProcessKbn = PROCESS_KBN.NEW;

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //参照ボタンイベント生成
            this.form.btnFileRef.Click += new EventHandler(this.form.FileRefClick);

            //アップロードボタンイベント生成
            this.form.btnUpload.Click += new EventHandler(this.form.UploadClick);
            this.form.btnUpload.Enabled = false;

            //閲覧ボタンイベント生成
            this.form.btnBrowse.Click += new EventHandler(this.form.BrowseClick);
            this.form.btnBrowse.Enabled = false;
            
            //削除ボタンイベント生成
            this.form.btnDelete.Click += new EventHandler(this.form.DeleteClick);
            this.form.btnDelete.Enabled = false;

            this.form.FURIKOMI_BANK_SHITEN_CD.Validating += new CancelEventHandler(this.form.FURIKOMI_BANK_SHITEN_CD_Validating);

            //20250310
            this.form.FURIKOMI_BANK_CD.Validated += new EventHandler(this.form.FurikomiBankCdValidated);
        }

        private void FURIKOMI_BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 委託契約書種類非表示(収集運搬契約固定)
            this.form.label69.Visible = false;
            this.form.panel34.Visible = false;

            this.form.ITAKU_KEIYAKU_SHURUI.Text = "1";
        }

        // フォーマットＣＤからフォーマットの文字列に変換する
        private String changeFormat(String formatCd)
        {
            String nullStr = "";
            String formatStr = nullStr;

            switch (formatCd)
            {
                case "1":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_1;
                    break;

                case "2":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_2;
                    break;

                case "3":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_3;
                    break;

                case "4":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_4;
                    break;

                case "5":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_5;
                    break;

                case "6":
                    formatStr = Const.SystemSetteiHoshuConstans.FORMAT_6;
                    break;

                default:
                    formatStr = nullStr;
                    break;
            }

            return formatStr;
        }

        /// <summary>
        /// CD表示変更処理
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="maxLength"></param>
        public string CdFormatting(string cd, string maxLength, out bool catchErr)
        {
            try
            {
                catchErr = false;
                // ゼロパディング後、テキストへ設定
                return String.Format("{0:D" + maxLength + "}", int.Parse(cd));
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CdFormatting", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }

        /// <summary>
        /// FromToチェック
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        public bool fromToCheck(string fromValue, string toValue)
        {
            // fromとtoの関係性をチェック
            if (Convert.ToDecimal(fromValue) > Convert.ToDecimal(toValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 拠点略称検索
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public string SearchKyotenName(string kyotenCd, CancelEventArgs e, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(kyotenCd, e);

                catchErr = false;
                string kyotenName = string.Empty;
                if (!string.IsNullOrWhiteSpace(kyotenCd))
                {
                    M_KYOTEN cond = new M_KYOTEN();
                    cond.KYOTEN_CD = Int16.Parse(kyotenCd);
                    DataTable dt = this.daoKyoten.GetDataBySqlFile(this.GET_KYOTEN_DATA_SQL, cond);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        kyotenName = dt.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "拠点");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(kyotenName, catchErr);
                return kyotenName;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("SearchKyotenName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd("", catchErr);
                return "";
            }
        }

        /// <summary>
        /// 形態区分(受入)の値チェック
        /// </summary>
        public bool UkeireKeitaiKbnCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.IsInputErrorOccured)
                {
                    this.form.KEIRYOU_UKEIRE_KEITAI_KBN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text))
                    {
                        M_KEITAI_KBN keitai = this.daoKeitaiKbn.GetDataByCd(this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text);
                        if (keitai != null && (keitai.DENSHU_KBN_CD == 1 || keitai.DENSHU_KBN_CD == 9))
                        {
                            string padData = keitai.KEITAI_KBN_CD.ToString().PadLeft((int)this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.CharactersNumber, '0');
                            this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text = padData;
                            this.form.KEIRYOU_UKEIRE_KEITAI_KBN_NAME.Text = keitai.KEITAI_KBN_NAME_RYAKU;
                        }
                        else
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "形態区分");
                            ret = false;
                        }
                    }

                    if (!ret)
                    {
                        this.form.KEIRYOU_UKEIRE_KEITAI_KBN_CD.Text = string.Empty;
                        this.form.KEIRYOU_UKEIRE_KEITAI_KBN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UkeireKeitaiKbnCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 形態区分(出荷)の値チェック
        /// </summary>
        public bool ShukkaKeitaiKbnCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.IsInputErrorOccured)
                {
                    this.form.KEIRYOU_SHUKKA_KEITAI_KBN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text))
                    {
                        M_KEITAI_KBN keitai = this.daoKeitaiKbn.GetDataByCd(this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text);
                        if (keitai != null && (keitai.DENSHU_KBN_CD == 2 || keitai.DENSHU_KBN_CD == 9))
                        {
                            string padData = keitai.KEITAI_KBN_CD.ToString().PadLeft((int)this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.CharactersNumber, '0');
                            this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text = padData;
                            this.form.KEIRYOU_SHUKKA_KEITAI_KBN_NAME.Text = keitai.KEITAI_KBN_NAME_RYAKU;
                        }
                        else
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "形態区分");
                            ret = false;
                        }
                    }

                    if (!ret)
                    {
                        this.form.KEIRYOU_SHUKKA_KEITAI_KBN_CD.Text = string.Empty;
                        this.form.KEIRYOU_SHUKKA_KEITAI_KBN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShukkaKeitaiKbnCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
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

                if (SystemSetteiHoshuConstans.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SEIKYUU_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    //// 入力制限
                    //this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.Text = SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.rb_SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.rb_SEIKYUU_NYUUKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }
                else if (SystemSetteiHoshuConstans.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SEIKYUU_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    //// 入力制限
                    //this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SEIKYUU_KEITAI_KBN.Text = SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.Text = SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.rb_SEIKYUU_KEITAI_KBN_2.Enabled = false;
                    this.form.rb_SEIKYUU_NYUUKIN_MEISAI_KBN_1.Enabled = false;
                    
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Enabled = true;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_1.Enabled = true;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_2.Enabled = true;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text ="1";
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SEIKYUU_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU,
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.SEIKYUU_NYUUKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.rb_SEIKYUU_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.rb_SEIKYUU_KEITAI_KBN_2.Enabled = true;
                    this.form.rb_SEIKYUU_NYUUKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SEIKYUU_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.SEIKYUU_SHOSHIKI_MEISAI_KBN);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払書式明細区分の制限処理
        /// </summary>
        internal bool LimitShiharaiShoshikiMeisaiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.SHOSHIKI_KBN_GYOUSHA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：業者別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.Text = SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.rb_SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.rb_SHIHARAI_SHUKKIN_MEISAI_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }
                else if (SystemSetteiHoshuConstans.SHOSHIKI_KBN_GENBA.ToString().Equals(this.form.SHIHARAI_SHOSHIKI_KBN.Text))
                {
                    // 書式区分：現場別
                    // 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU };
                    //this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI };
                    this.form.SHIHARAI_KEITAI_KBN.Text = SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU.ToString();
                    this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.Text = SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI.ToString();
                    // チェック制限
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = false;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = false;
                    this.form.rb_SHIHARAI_KEITAI_KBN_2.Enabled = false;
                    this.form.rb_SHIHARAI_SHUKKIN_MEISAI_KBN_1.Enabled = false;
                    
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = "1";
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GYOUSHA,
                    //        SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_GENBA };
                    //this.form.SHIHARAI_KEITAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_TANGETSU,
                    //        SystemSetteiHoshuConstans.SEIKYU_KEITAI_KBN_KURIKOSI};
                    //this.form.SHIHARAI_SHUKKIN_MEISAI_KBN.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HYOUJI,
                    //        SystemSetteiHoshuConstans.NYUSYUKIN_MEISAI_KBN_HIHYOUJI};
                    // チェック制限
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_1.Enabled = true;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_2.Enabled = true;
                    this.form.rb_SHIHARAI_SHOSHIKI_MEISAI_KBN_3.Enabled = true;
                    this.form.rb_SHIHARAI_KEITAI_KBN_2.Enabled = true;
                    this.form.rb_SHIHARAI_SHUKKIN_MEISAI_KBN_1.Enabled = true;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Text = string.Empty;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_1.Enabled = false;
                    this.form.SHIHARAI_SHOSHIKI_GENBA_KBN_2.Enabled = false;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.SHIHARAI_SHOSHIKI_MEISAI_KBN);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiShoshikiMeisaiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 請求税区分の制限処理
        /// </summary>
        internal bool LimitSeikyuuZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text) ||
                    SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    //// 入力制限
                    //this.form.SEIKYUU_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.rb_SEIKYUU_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.rb_SEIKYUU_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_2.Enabled = false;
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SEIKYUU_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_UCHI,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_2.Enabled = true;
                    this.form.rb_SEIKYUU_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.SEIKYUU_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitSeikyuuZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払税区分の制限処理
        /// </summary>
        internal bool LimitShiharaiZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text) ||
                    SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    //// 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.rb_SHIHARAI_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.rb_SHIHARAI_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_2.Enabled = false;
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.SHIHARAI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_UCHI,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_2.Enabled = true;
                    this.form.rb_SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.SHIHARAI_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitShiharaiZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 見積税区分の制限処理
        /// </summary>
        internal bool LimitMitsumoriZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.MITSUMORI_ZEIKEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    //// 入力制限
                    //this.form.MITSUMORI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    if (this.form.rdo_Uchizei.Checked)
                    {
                        this.form.rdo_Sotozei.Checked = true;
                    }
                    this.form.rdo_Sotozei.Enabled = true;
                    this.form.rdo_Uchizei.Enabled = false;
                    this.form.rdo_Hikazei.Enabled = true;
                }
                else
                {
                    // その他
                    //// 入力制限
                    //this.form.MITSUMORI_ZEI_KBN_CD.CharacterLimitList =
                    //    new char[] {
                    //        SystemSetteiHoshuConstans.ZEI_KBN_SOTO,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_UCHI,
                    //        SystemSetteiHoshuConstans.ZEI_KBN_NONE };
                    // チェック制限
                    this.form.rdo_Sotozei.Enabled = true;
                    this.form.rdo_Uchizei.Enabled = true;
                    this.form.rdo_Hikazei.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.MITSUMORI_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitMitsumoriZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 許容されていない入力の場合、テキストをクリアする
        /// </summary>
        /// <param name="textBox">数値入力テキストボックス</param>
        /// <param name="limitList">制限文字リスト</param>
        internal static void ClearText_NotAllowedInput(CustomNumericTextBox2 textBox)
        {
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
                    textBox.Text = SystemSetteiHoshuConstans.SHOSHIKI_MEISAI_KBN_NONE.ToString();
                }
            }
        }

        /// <summary>
        /// 指示書／依頼書印刷有無変更処理
        /// </summary>
        public bool ChangeUketsukeSijishoPrintKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.UKETSUKE_SIJISHO_PRINT_KBN.Text.Equals("1"))
                {
                    this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Enabled = true;
                    this.form.rb_UKETSUKE_SIJISHO_SUB_PRINT_KBN_1.Enabled = true;
                    this.form.rb_UKETSUKE_SIJISHO_SUB_PRINT_KBN_2.Enabled = true;
                }
                else
                {
                    this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Text = "2";
                    this.form.UKETSUKE_SIJISHO_SUB_PRINT_KBN.Enabled = false;
                    this.form.rb_UKETSUKE_SIJISHO_SUB_PRINT_KBN_1.Enabled = false;
                    this.form.rb_UKETSUKE_SIJISHO_SUB_PRINT_KBN_2.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeUketsukeSijishoPrintKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 請求書拠点の値チェック
        /// </summary>
        public bool SeikyuuKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.SEIKYUU_KYOTEN_CD.IsInputErrorOccured)
                {
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

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 支払書拠点の値チェック
        /// </summary>
        public bool ShiharaiKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.SHIHARAI_KYOTEN_CD.IsInputErrorOccured)
                {
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

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 業者請求書拠点の値チェック
        /// </summary>
        public bool GyoushaSeikyuuKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.IsInputErrorOccured)
                {
                    this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text))
                    {
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null && kyo.KYOTEN_CD != 99)
                        {
                            string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.CharactersNumber, '0');
                            this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = padData;
                            this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
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
                        this.form.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = string.Empty;
                        this.form.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaSeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 業者支払書拠点の値チェック
        /// </summary>
        public bool GyoushaShiharaiKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.IsInputErrorOccured)
                {
                    this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text))
                    {
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null && kyo.KYOTEN_CD != 99)
                        {
                            string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.CharactersNumber, '0');
                            this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = padData;
                            this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
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
                        this.form.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = string.Empty;
                        this.form.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 現場請求書拠点の値チェック
        /// </summary>
        public bool GenbaSeikyuuKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.GENBA_SEIKYUU_KYOTEN_CD.IsInputErrorOccured)
                {
                    this.form.GENBA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.GENBA_SEIKYUU_KYOTEN_CD.Text))
                    {
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.GENBA_SEIKYUU_KYOTEN_CD.Text);
                        if (kyo != null && kyo.KYOTEN_CD != 99)
                        {
                            string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.GENBA_SEIKYUU_KYOTEN_CD.CharactersNumber, '0');
                            this.form.GENBA_SEIKYUU_KYOTEN_CD.Text = padData;
                            this.form.GENBA_SEIKYUU_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
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
                        this.form.GENBA_SEIKYUU_KYOTEN_CD.Text = string.Empty;
                        this.form.GENBA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaSeikyuuKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 現場支払書拠点の値チェック
        /// </summary>
        public bool GenbaShiharaiKyotenCdValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                if (!this.form.GENBA_SHIHARAI_KYOTEN_CD.IsInputErrorOccured)
                {
                    this.form.GENBA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    if (!string.IsNullOrEmpty(this.form.GENBA_SHIHARAI_KYOTEN_CD.Text))
                    {
                        M_KYOTEN kyo = this.daoKyoten.GetDataByCd(this.form.GENBA_SHIHARAI_KYOTEN_CD.Text);
                        if (kyo != null && kyo.KYOTEN_CD != 99)
                        {
                            string padData = kyo.KYOTEN_CD.ToString().PadLeft((int)this.form.GENBA_SHIHARAI_KYOTEN_CD.CharactersNumber, '0');
                            this.form.GENBA_SHIHARAI_KYOTEN_CD.Text = padData;
                            this.form.GENBA_SHIHARAI_KYOTEN_NAME.Text = kyo.KYOTEN_NAME_RYAKU;
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
                        this.form.GENBA_SHIHARAI_KYOTEN_CD.Text = string.Empty;
                        this.form.GENBA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaShiharaiKyotenCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 「支払確定入力を利用する」の値変更時
        /// </summary>
        public bool ChangeShiharaiKakuteiUseKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.SHIHARAI_KAKUTEI_USE_KBN.Text.Equals("1"))
                {
                    // パスワード未設定：初期状態
                    if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Length == 0)
                    {
                        // Enabled
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                        // ReadOnly
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                    }
                    // パスワード一致
                    else if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Equals(this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text))
                    {
                        // Enabled
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                        // ReadOnly
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                    }
                    // パスワード不一致
                    else
                    {
                        // Enabled
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                        // ReadOnly
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    }
                }
                // パスワード
                else
                {
                    // Text
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // ReadOnly
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                    // Enalbed
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeShiharaiKakuteiUseKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 支払確定のパスワード値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない/params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool ShiharaiKakuteiPasswordValidate(bool fDlg)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0 &&
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length > 0 &&
                    this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Equals(this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text))
                {
                    // Text
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                    // Enabled
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    // ReadOnly
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                }
                // パスワード無し
                else if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Length == 0 &&
                         this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // Enabled
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                }
                // 確認パスワード無し
                else if (this.form.SHIHARAI_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0 &&
                         this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // Enabled
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;

                    // Focus移動
                    this.form.SelectNextControl(this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD, true, true, false, false);
                }
                // パスワード有り、確認パスワードと不一致
                else
                {
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // Enabled
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;

                    // メッセージ表示
                    if (fDlg)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    // フォーカス
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Focus();
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD.SelectAll();

                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKakuteiPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 支払確定の新パスワード値チェック
        /// </summary>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// <returns>true:処理継続、false:処理中断</returns>
        public bool ShiharaiKakuteiNewPasswordValidate(bool isRegist, bool fTab)
        {
            try
            {
                // パスワード有り 確認パスワードと一致、あるいは パスワード無し時
                if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Equals(this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text))
                {
                    // 新パスワード一致時
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length >= 0 &&
                         this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    // メッセージ表示
                    if (fTab)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    return isRegist ? false : true;
                }
                else
                {
                    // メッセージ表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E143");
                    // フォーカス
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Focus();
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.SelectAll();
                    return isRegist ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKakuteiNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 支払確定の新パスワード確認値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない</params>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool ShiharaiKakuteiConfirmNewPasswordValidate(bool fDlg, bool fTab)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Equals(this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text))
                {
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length == 0 &&
                         this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    return true;
                }
                else
                {
                    // 確認パスワード有り
                    if (this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0 &&
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                    {
                        // 確認用の新パスワードを空文字にすると新パスワードに移動できる
                        // フォーカス
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.Focus();
                        this.form.SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD.SelectAll();
                        // メッセージ表示
                        if (fTab)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }
                        if (fTab)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // メッセージ表示
                        if (fDlg)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }
                        // フォーカス
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Focus();
                        this.form.SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.SelectAll();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShiharaiKakuteiConfirmNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 「売上確定入力を利用する」の値変更時
        /// </summary>
        public bool ChangeUriageKakuteiUseKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.URIAGE_KAKUTEI_USE_KBN.Text.Equals("1"))
                {
                    // パスワード未設定：初期状態
                    if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Length == 0)
                    {
                        // Enabled
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                        // ReadOnly
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                    }
                    // パスワード一致
                    else if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Equals(this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text))
                    {
                        // Enabled
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                        // ReadOnly
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                    }
                    // パスワード不一致
                    else
                    {
                        // Enabled
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                        // ReadOnly
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    }
                }
                // パスワード
                else
                {
                    // Text
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // ReadOnly
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                    // Enalbed
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeUriageKakuteiUseKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 売上確定のパスワード値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない/params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool UriageKakuteiPasswordValidate(bool fDlg)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0 &&
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length > 0 &&
                    this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Equals(this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text))
                {
                    // Text
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                    // Enabled
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    // ReadOnly
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = false;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                }
                // パスワード無し
                else if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Length == 0 &&
                         this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    //this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    //this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    //// Enabled
                    //this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = false;
                    //this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    //this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                    //// ReadOnly
                    //this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    //this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    //this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                }
                // 確認パスワード無し
                else if (this.form.URIAGE_KAKUTEI_KAIJO_PASSWORD.Text.Length > 0 &&
                         this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    if (!"2".Equals(this.form.URIAGE_KAKUTEI_USE_KBN.Text))
                    {
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Text = "";
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                        // Enabled
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                        // ReadOnly
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                        // Focus移動
                        this.form.SelectNextControl(this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD, true, true, false, false);
                    }
                }
                // パスワード有り、確認パスワードと不一致
                else
                {
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text = "";
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text = "";
                    // Enabled
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Enabled = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Enabled = false;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.ReadOnly = true;
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.ReadOnly = true;

                    // メッセージ表示
                    if (fDlg)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    // フォーカス
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.Focus();
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD.SelectAll();

                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UriageKakuteiPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 売上確定の新パスワード値チェック
        /// </summary>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// /// <returns>true:処理継続、false:処理中断/returns>
        public bool UriageKakuteiNewPasswordValidate(bool isRegist, bool fTab)
        {
            try
            {
                // パスワード有り 確認パスワードと一致、あるいは パスワード無し時
                if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Equals(this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text))
                {
                    // 新パスワード一致時
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length >= 0 &&
                         this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    // メッセージ表示
                    if (fTab)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    return isRegist ? false : true;
                }
                else
                {
                    // メッセージ表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E143");
                    // フォーカス
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Focus();
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.SelectAll();
                    return isRegist ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UriageKakuteiNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 売上確定の新パスワード確認値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない/params>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool UriageKakuteiConfirmNewPasswordValidate(bool fDlg, bool fTab)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Equals(this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text))
                {
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length == 0 &&
                         this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    return true;
                }
                else
                {
                    // 確認パスワード有り
                    if (this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Text.Length > 0 &&
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                    {
                        // フォーカス
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.Focus();
                        this.form.URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD.SelectAll();
                        // メッセージ表示
                        if (fTab)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }
                        if (fTab)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // 確認用の新パスワードを空文字にすると新パスワードに移動できる
                        // メッセージ表示
                        if (fDlg)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }                    // フォーカス
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.Focus();
                        this.form.URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD.SelectAll();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UriageKakuteiConfirmNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 請求取引区分を変更時に税計算区分を再設定する
        /// </summary>
        public void ChangeSeikyuuTorihikiKbn()
        {
            //// 初期化
            this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = true;
            this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = true;
            this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = true;
            this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2', '3' };

            if ("1".Equals(this.form.SEIKYUU_TORIHIKI_KBN.Text))
            {
                this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                if ("2".Equals(this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
                }

                this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '3' };
            }

            //if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.ToString() == "1")
            //{
            //    if (this.form.SEIKYUU_TORIHIKI_KBN.Text.ToString() == "1")
            //    {
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = false;
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = false;
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1' };

            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
            //    }
            //    else if (this.form.SEIKYUU_TORIHIKI_KBN.Text.ToString() == "2")
            //    {
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = false;
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2' };
            //    }

            //    if (this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text == "3")
            //    {
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
            //    }
            //}
            //else if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.ToString() == "2")
            //{
            //    if (this.form.SEIKYUU_TORIHIKI_KBN.Text.ToString() == "1")
            //    {
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = false;
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = false;
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '3' };

            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "3";
            //    }
            //    else if (this.form.SEIKYUU_TORIHIKI_KBN.Text.ToString() == "2")
            //    {
            //        this.form.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = false;
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '2', '3' };
            //    }

            //    if (this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text == "1")
            //    {
            //        this.form.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "2";
            //    }
            //}
        }

        /// <summary>
        /// 支払取引区分を変更時に税計算区分を再設定する
        /// </summary>
        public void ChangeShiharaiTorihikiKbn()
        {
            //// 初期化
            this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = true;
            this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = true;
            this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = true;
            this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2', '3' };

            if ("1".Equals(this.form.SHIHARAI_TORIHIKI_KBN.Text))
            {
                this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                if ("2".Equals(this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                }

                this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '3' };
            }

            //if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.ToString() == "1")
            //{
            //    if (this.form.SHIHARAI_TORIHIKI_KBN.Text.ToString() == "1")
            //    {
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = false;
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1' };

            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
            //    }
            //    else if (this.form.SHIHARAI_TORIHIKI_KBN.Text.ToString() == "2")
            //    {
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = false;
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2' };
            //    }

            //    if (this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text == "3")
            //    {
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
            //    }
            //}
            //else if (this.form.SYS_ZEI_KEISAN_KBN_USE_KBN.Text.ToString() == "2")
            //{
            //    if (this.form.SHIHARAI_TORIHIKI_KBN.Text.ToString() == "1")
            //    {
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = false;
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '3' };

            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "3";
            //    }
            //    else if (this.form.SHIHARAI_TORIHIKI_KBN.Text.ToString() == "2")
            //    {
            //        this.form.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = false;
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '2', '3' };
            //    }

            //    if (this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text == "1")
            //    {
            //        this.form.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "2";
            //    }
            //}
        }

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
                this.form.titleLableOutputKbn.Visible = densiVisible;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = densiVisible;
                this.form.titleLabel26.Location = new System.Drawing.Point(this.form.titleLabel26.Location.X, this.form.titleLabel26.Location.Y );
                this.form.pl_SEIKYUU_ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.pl_SEIKYUU_ZEI_KBN_CD.Location.X, this.form.pl_SEIKYUU_ZEI_KBN_CD.Location.Y );
                this.form.label153.Location = new System.Drawing.Point(this.form.label153.Location.X, this.form.label153.Location.Y );
                this.form.panel97.Location = new System.Drawing.Point(this.form.panel97.Location.X, this.form.panel97.Location.Y );
                this.form.label152.Location = new System.Drawing.Point(this.form.label152.Location.X, this.form.label152.Location.Y );
                this.form.panel96.Location = new System.Drawing.Point(this.form.panel96.Location.X, this.form.panel96.Location.Y );
                this.form.label151.Location = new System.Drawing.Point(this.form.label151.Location.X, this.form.label151.Location.Y );
                this.form.SEIKYUU_KYOTEN_CD.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_CD.Location.X, this.form.SEIKYUU_KYOTEN_CD.Location.Y );
                this.form.SEIKYUU_KYOTEN_NAME.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_NAME.Location.X, this.form.SEIKYUU_KYOTEN_NAME.Location.Y );
                this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location.X, this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location.Y );
                this.form.DummySkcd.Location = new System.Drawing.Point(this.form.DummySkcd.Location.X, this.form.DummySkcd.Location.Y );
            }
            return densiVisible;
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>
        /// 計量タブ：請求取引区分を変更時に税計算区分を再設定する
        /// </summary>
        internal void ChangeKeiryouSeikyuuTorihikiKbn()
        {
            //// 初期化
            this.form.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = true;
            this.form.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = true;
            this.form.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = true;
            this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2', '3' };

            // 取引区分：現金
            // チェック制限：請求毎非活性
            if ("1".Equals(this.form.KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD.Text))
            {
                this.form.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                if ("2".Equals(this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
                }
                this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '3' };
            }
        }

        /// <summary>
        /// 計量タブ：支払取引区分を変更時に税計算区分を再設定する
        /// </summary>
        internal void ChangeKeiryouShiharaiTorihikiKbn()
        {
            //// 初期化
            this.form.rb_KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = true;
            this.form.rb_KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = true;
            this.form.rb_KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = true;
            this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '2', '3' };

            // 取引区分：現金
            // チェック制限：精算毎非活性
            if ("1".Equals(this.form.KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD.Text))
            {
                this.form.rb_KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD_2.Enabled = false;
                if ("2".Equals(this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                }
                this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.CharacterLimitList = new char[] { '1', '3' };
            }
        }

        /// <summary>
        /// 計量タブ：請求税区分の制限処理
        /// </summary>
        internal bool LimitKeiryouSeikyuuZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text) ||
                    SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    // チェック制限：内税非活性
                    if (this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_2.Enabled = false;
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    // チェック制限
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_2.Enabled = true;
                    this.form.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.KEIRYOU_SEIKYUU_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitKeiryouSeikyuuZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 計量タブ：支払税区分の制限処理
        /// </summary>
        internal bool LimitKeiryouShiharaiZeiKbn()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text) ||
                    SystemSetteiHoshuConstans.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.form.KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                {
                    // 税計算区分：伝票毎、請求毎
                    // チェック制限：内税非活性
                    if (this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_2.Checked)
                    {
                        this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_1.Checked = true;
                    }
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_2.Enabled = false;
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }
                else
                {
                    // その他
                    // チェック制限
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_1.Enabled = true;
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_2.Enabled = true;
                    this.form.rb_KEIRYOU_SHIHARAI_ZEI_KBN_CD_3.Enabled = true;
                }

                // 許容されていない入力の場合、テキストをクリアする
                SystemSetteiHoshuLogic.ClearText_NotAllowedInput(this.form.KEIRYOU_SHIHARAI_ZEI_KBN_CD);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LimitKeiryouShiharaiZeiKbn", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        /// <summary>
        /// モバイルオプション用初期化処理
        /// </summary>
        private void MobileInit()
        {
            bool ismobile_mode = r_framework.Configuration.AppConfig.AppOptions.IsMobile();
            if (ismobile_mode)
            {
                this.form.label45.Visible = true;
                this.form.panel1.Visible = true;
            }
            else
            {
                this.form.label45.Visible = false;
                this.form.panel1.Visible = false;
            }
        }

        /// <summary>
        /// 計量票タイトル文字数チェック
        /// </summary>
        /// <returns>true：継続、false：中断</returns>
        internal bool KeiryouHyouTitleCheck()
        {
            if (this.form.KEIRYOU_HYOU_TITLE_1.Text.Length > 6 ||
                this.form.KEIRYOU_HYOU_TITLE_2.Text.Length > 6 ||
                this.form.KEIRYOU_HYOU_TITLE_3.Text.Length > 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// モバイル将軍の起動パスワードの活性/非活性制御
        /// </summary>
        public bool ChangeStatusMobilePassword()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // モバイル将軍連携オプションがOFFの場合は、起動パスワード項目を全て非表示にする。
                if (!AppConfig.AppOptions.IsMobile())
                {
                    // ラベル
                    this.form.label130.Visible = false;
                    this.form.label126.Visible = false;
                    this.form.label127.Visible = false;
                    this.form.label128.Visible = false;
                    this.form.label129.Visible = false;

                    // 項目
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Visible = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Visible = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Visible = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Visible = false;

                    // 必須チェックイベントを削除
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.RegistCheckMethod.Clear();
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.RegistCheckMethod.Clear();

                    return false;
                }

                // パスワード設定なし
                if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Length == 0)
                {
                    // Enabled
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = true;
                    // ReadOnly
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                }
                // パスワード設定あり
                else
                {
                    // Enabled
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeStatusMobilePassword", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// モバイル将軍システム設定起動のパスワード値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない/params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool MobilePasswordValidate(bool fDlg)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Length > 0 &&
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text.Length > 0 &&
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Equals(this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text))
                {
                    // Text
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = true;
                    // Enabled
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                    // ReadOnly
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = false;
                }
                // パスワード無し
                else if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Length == 0 &&
                         this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text = "";
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text = "";
                    //// Enabled
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = false;
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = false;
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = false;
                    //// ReadOnly
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = true;
                    //this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                }
                // 確認パスワード無し
                else if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD.Text.Length > 0 &&
                         this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text.Length == 0)
                {
                    if (!"2".Equals(this.form.URIAGE_KAKUTEI_USE_KBN.Text))
                    {
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Text = "";
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text = "";
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text = "";
                        // Enabled
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = true;
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = false;
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = false;
                        // ReadOnly
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = true;
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = true;
                        // Focus移動
                        this.form.SelectNextControl(this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD, true, true, false, false);
                    }
                }
                // パスワード有り、確認パスワードと不一致
                else
                {
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text = "";
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text = "";
                    // Enabled
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Enabled = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Enabled = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Enabled = false;
                    // ReadOnly
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.ReadOnly = false;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.ReadOnly = true;
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.ReadOnly = true;

                    // メッセージ表示
                    if (fDlg)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    // フォーカス
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.Focus();
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD.SelectAll();

                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("MobilePasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// モバイル将軍システム設定起動の新パスワード値チェック
        /// </summary>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// /// <returns>true:処理継続、false:処理中断/returns>
        public bool MobileNewPasswordValidate(bool isRegist, bool fTab)
        {
            try
            {
                // パスワード有り 確認パスワードと一致、あるいは パスワード無し時
                if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Equals(this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text))
                {
                    // 新パスワード一致時
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Length >= 0 &&
                         this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    // メッセージ表示
                    if (fTab)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E143");
                    }
                    return isRegist ? false : true;
                }
                else
                {
                    // メッセージ表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E143");
                    // フォーカス
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Focus();
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.SelectAll();
                    return isRegist ? false : true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MobileNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// モバイル将軍システム設定起動の新パスワード確認値チェック
        /// </summary>
        /// <params>fDlg true:ダイアログ表示する、false:ダイアログ表示しない/params>
        /// <params>fTab true:TAB移動時、false:TAB移動しない</params>
        /// <returns>true:処理継続、false:処理中断/returns>
        public bool MobileConfirmNewPasswordValidate(bool fDlg, bool fTab)
        {
            try
            {
                // パスワード有り、確認パスワードと一致
                if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text.Length > 0 &&
                    this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Equals(this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text))
                {
                    return true;
                }
                // 確認パスワード無し
                else if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Length == 0 &&
                         this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                {
                    return true;
                }
                else
                {
                    // 確認パスワード有り
                    if (this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Text.Length > 0 &&
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Text.Length == 0)
                    {
                        // フォーカス
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.Focus();
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD.SelectAll();
                        // メッセージ表示
                        if (fTab)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }
                        if (fTab)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // 確認用の新パスワードを空文字にすると新パスワードに移動できる
                        // メッセージ表示
                        if (fDlg)
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E143");
                        }                    // フォーカス
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.Focus();
                        this.form.MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD.SelectAll();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MobileConfirmNewPasswordValidate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        private void SetDensiSeikyushoAndRakurakuVisible()
        {
            // 電子請求オプ
            bool denshiSeikyusho = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            //電子請求楽楽明細オプ
            bool denshiSeikyuRaku = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();
            if (!denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.titleLableOutputKbn.Visible = false;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = false;

                int loc_y = this.form.titleLableOutputKbn.Location.Y;
                this.form.titleLabel26.Location = new System.Drawing.Point(this.form.titleLabel26.Location.X, this.form.titleLableOutputKbn.Location.Y);
                this.form.pl_SEIKYUU_ZEI_KBN_CD.Location = new System.Drawing.Point(this.form.pl_SEIKYUU_ZEI_KBN_CD.Location.X, this.form.pl_SEIKYUU_OUTPUT_KBN.Location.Y);
                this.form.label153.Location = new System.Drawing.Point(this.form.label153.Location.X, this.form.titleLabel26.Location.Y + 22);
                this.form.panel97.Location = new System.Drawing.Point(this.form.panel97.Location.X, this.form.pl_SEIKYUU_ZEI_KBN_CD.Location.Y + 22);

                this.form.label152.Location = new System.Drawing.Point(this.form.label152.Location.X, this.form.label153.Location.Y + 22);
                this.form.panel96.Location = new System.Drawing.Point(this.form.panel96.Location.X, this.form.panel97.Location.Y + 22);

                this.form.label151.Location = new System.Drawing.Point(this.form.label151.Location.X, this.form.label152.Location.Y + 22);
                this.form.SEIKYUU_KYOTEN_CD.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_CD.Location.X, this.form.panel96.Location.Y + 22);
                this.form.SEIKYUU_KYOTEN_NAME.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_NAME.Location.X, this.form.panel96.Location.Y + 22);
                this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location = new System.Drawing.Point(this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location.X, this.form.panel96.Location.Y + 22);
                this.form.DummySkcd.Location = new System.Drawing.Point(this.form.DummySkcd.Location.X, this.form.SEIKYUU_KYOTEN_CD_SEARCH.Location.Y);
            }
            else if (denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.titleLableOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;

                this.form.SEIKYUU_OUTPUT_KBN.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_1.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_2.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_3.Enabled = true;
            }
            else if (denshiSeikyusho && !denshiSeikyuRaku)
            {
                this.form.titleLableOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;

                this.form.SEIKYUU_OUTPUT_KBN.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_1.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_2.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_3.Enabled = false;
            }
            else if (!denshiSeikyusho && denshiSeikyuRaku)
            {
                this.form.titleLableOutputKbn.Visible = true;
                this.form.pl_SEIKYUU_OUTPUT_KBN.Visible = true;

                this.form.SEIKYUU_OUTPUT_KBN.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_1.Enabled = true;
                this.form.rb_SEIKYUU_OUTPUT_KBN_2.Enabled = false;
                this.form.rb_SEIKYUU_OUTPUT_KBN_3.Enabled = true;
            }

            return;
        }

        /// <summary>
        /// 伝票種類の値より、各項目設定
        /// </summary>
        internal void DenpyouShuruiSetting()
        {
            string denpyouShurui = string.Empty;
            if (!string.IsNullOrEmpty(this.form.SMS_DENPYOU_SHURUI.Text))
            {
                denpyouShurui = this.form.SMS_DENPYOU_SHURUI.Text;
                if (denpyouShurui == "1" || denpyouShurui == "2" || denpyouShurui == "4" || denpyouShurui == "5")
                {
                    // 送信状況=1．未送信時のみ変更
                    if (this.form.SMS_SEND_JOKYO.Text == "1")
                    {
                        // 1．収集、2．出荷、4．収集+出荷、5．収集+持込を選択した場合、
                        // 配車状況を活性化、初期値をセット（1．受注）
                        this.form.haishaJokyoPanel.Enabled = true;
                        this.form.SMS_HAISHA_JOKYO.Text = "1";
                    }
                }
            
                else if (denpyouShurui == "3" || denpyouShurui == "6")
                {
                    // 3．持込、6．定期配車を選択した場合、
                    // 配車状況を非活性化、値をクリア
                    this.form.haishaJokyoPanel.Enabled = false;
                    this.form.SMS_HAISHA_JOKYO.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 送信状況の値より、各項目設定
        /// </summary>
        internal void SmsStatusSetting()
        {
            // 送信状況=1．未送信である場合
            if (this.form.SMS_SEND_JOKYO.Text == "1")
            {
                this.form.haishaJokyoPanel.Enabled = true;
                this.form.SMS_HAISHA_JOKYO.Text = "1";
            }
            else if(this.form.SMS_SEND_JOKYO.Text == "2")
            {
                this.form.haishaJokyoPanel.Enabled = false;
                this.form.SMS_HAISHA_JOKYO.Text = string.Empty;
            }
            else if (this.form.SMS_SEND_JOKYO.Text == "3")
            {
                this.form.haishaJokyoPanel.Enabled = false;
                this.form.SMS_HAISHA_JOKYO.Text = string.Empty;
            }
        }
        /// <summary>
        /// 参照ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FileRefClick()
        {
            try
            {
                // ユーザ定義情報を取得
                Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile userProfile = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                string serverPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "参照するファイルを選択してください。";
                var initialPath = @"C:\Temp";
                if (!string.IsNullOrEmpty(serverPath))
                {
                    initialPath = serverPath;
                }
                var windowHandle = this.form.Handle;
                var isFileSelect = true;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    this.form.KAKUIN_FILE_NAME.Text = Path.GetFileName(filePath);
                    // アップロードボタン活性化
                    this.form.btnUpload.Enabled = true;
                }
                else
                {
                    this.form.KAKUIN_FILE_NAME.Text = string.Empty;
                    // アップロードボタン非活性化
                    this.form.btnUpload.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FileRefClick", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
            }
        }
        /// <summary>
        /// アップロードボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UploadClick()
        {
            // アップロードファイルの選択確認
            if (String.IsNullOrEmpty(this.form.KAKUIN_FILE_NAME.Text)) 
            {
                this.form.errmessage.MessageBoxShowError("ファイルアップロードを行うファイルが未選択です。");
            }
            // ファイルアップロード用DB接続を確立
            if (!this.uploadLogic.CanConnectDB())
            {
                this.form.errmessage.MessageBoxShowError("ファイルアップロードの事前処理が未完了です。接続先データベースを確認してください。");
            }
            // ファイルアップロードlogicに渡す引数セット
            string[] paramList = new string[1];
            paramList[0] = "0";

            //アップロード
            if (this.uploadLogic.UploadForOneFile(filePath, 0, WINDOW_ID.M_SYS_INFO, paramList))
            {
                // メッセージ表示
                this.form.errmessage.MessageBoxShowInformation("アップロードしました。");

                //各ボタンの活性/非活性
                this.form.btnUpload.Enabled = false;
                this.form.btnFileRef.Enabled = false;
                this.form.btnBrowse.Enabled = true;
                this.form.btnDelete.Enabled = true;
            }
            else
            {
                //各ボタンの活性/非活性
                this.form.btnUpload.Enabled = true;
                this.form.btnFileRef.Enabled = false;
                this.form.btnBrowse.Enabled = true;
                this.form.btnDelete.Enabled = true;
            }
        }
        /// <summary>
        /// 閲覧ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BrowseClick()
        {

            try
            {
                LogUtility.DebugMethodStart();
                string browseFilePath = "";

                if (!string.IsNullOrWhiteSpace(this.form.KAKUIN_FILE_NAME.Text))
                {
                    // システム設定入力データからファイルIDを取得する。
                    fileLink = fileLinkSysInfoDao.GetDataById("0");

                    if (fileLink != null)
                    {
                        // ファイルIDからファイル情報を取得
                        long fileId = (long)fileLink.FILE_ID;
                        fileData = this.fileDataDao.GetDataByKey(fileId);
                        
                        // 閲覧するファイルパス取得
                        browseFilePath = this.fileData.FILE_PATH;
                    }

                    if (SystemProperty.IsTerminalMode)
                    {
                        if (string.IsNullOrEmpty(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectoryNonMsg()))
                        {
                            MessageBox.Show("閲覧を行う前に、印刷設定の出力先フォルダを指定してください。",
                                            "アラート",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        }

                        // クラウド環境でもオンプレと同じようにプロセス起動する
                        string clientFilePathInfo = Path.Combine(Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory(), "ClientFilePathInfo.txt");

                        // 5回ファイル作成を試す
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                using (var writer = new StreamWriter(clientFilePathInfo, false, Encoding.UTF8))
                                {
                                    writer.Write(browseFilePath);
                                }
                                break;
                            }
                            catch (Exception e)
                            {
                                System.Threading.Thread.Sleep(100);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(browseFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is Win32Exception)
                {
                    // SQLエラー用メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E200");
                }
                else
                {
                    LogUtility.Error("BrowseClick", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 削除ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DeleteClick()
        {
            // トランザクション開始
            using (var tran = new Transaction())
            {
                // ファイルデータ削除
                var list = fileLinkSysInfoDao.GetDataBySystemId("0");

                if (list != null && 0 < list.Count)
                {
                    // ファイルデータを物理削除する。
                    var fileIdList = list.Select(n => n.FILE_ID.Value).ToList();
                    this.uploadLogic.DeleteFileData(fileIdList);

                    // 連携データ削除
                    string sql = string.Format("DELETE FROM M_FILE_LINK_SYS_INFO WHERE SYS_ID = {0}", 0);
                    fileLinkSysInfoDao.GetDateForStringSql(sql);
                }

                // トランザクション終了
                tran.Commit();
            }
            //各ボタンの活性/非活性
            this.form.btnFileRef.Enabled = true;
            this.form.btnUpload.Enabled = false;
            this.form.btnBrowse.Enabled = false;
            this.form.btnDelete.Enabled = false;

            // ファイル名の削除
            this.form.KAKUIN_FILE_NAME.Text = String.Empty;

            // メッセージ表示
            var messageShowLogic = new MessageBoxShowLogic();
            messageShowLogic.MessageBoxShow("I001", "削除");
        }

        //20250305
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

        //20250310
        public bool FurikomiBankCdValidated()
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

                this.form.previousBankShitenCd = this.form.FURIKOMI_BANK_SHITEN_CD.Text;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCdValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
    }
}