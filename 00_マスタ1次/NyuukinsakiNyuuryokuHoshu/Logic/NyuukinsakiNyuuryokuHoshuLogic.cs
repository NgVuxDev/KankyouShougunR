// $Id: NyuukinsakiNyuuryokuHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using NyuukinsakiNyuuryokuHoshu.APP;
using NyuukinsakiNyuuryokuHoshu.Const;
using NyuukinsakiNyuuryokuHoshu.Validator;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.MasterAccess;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace NyuukinsakiNyuuryokuHoshu.Logic
{
    /// <summary>
    /// 入金先入力保守画面ビジネスロジック
    /// </summary>
    public class NyuukinsakiNyuuryokuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "NyuukinsakiNyuuryokuHoshu.Setting.ButtonSetting.xml";

        private readonly string DELETE_FURIKOMISAKI_DATA_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.DeleteFurikomisakidataSql.sql";

        private readonly string GET_FURIKOMISAKI_DATA_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.GetFurikomisakidataSql.sql";

        private readonly string GET_INPUTCD_DATA_NYUUKINSAKI_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.GetInputCddataNyuukinsakiSql.sql";

        private readonly string GET_INPUTCD_DATA_TORIHIKISAKI_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.GetInputCddataTorihikisakiSql.sql";

        private readonly string GET_TORIHIKISAKI_SEIKYUU_DATA_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.GetTorihikisakiSeikyuudataSql.sql";

        private readonly string CHECK_DELETE_NYUUKINSAKI_SQL = "NyuukinsakiNyuuryokuHoshu.Sql.CheckDeleteNyuukinsakiSql.sql";

        /// <summary>
        /// 入金先保守画面Form
        /// </summary>
        private NyuukinsakiNyuuryokuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_NYUUKINSAKI nyuukinsakiEntity;

        private M_NYUUKINSAKI_FURIKOMI[] entitys;

        private DataTable furikomiTable;

        private DataTable torihikisakiTable;

        private M_TODOUFUKEN todoufukenEntity;

        /// <summary>
        /// 入金先のDao
        /// </summary>
        private IM_NYUUKINSAKIDao nyuukinsakiDao;

        /// <summary>
        /// 入金先_振り込みDao
        /// </summary>
        private IM_NYUUKINSAKI_FURIKOMIDao furikomiDao;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao torisakiDao;

        /// <summary>
        /// 取引先_請求のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torikyuuDao;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 入金先CD
        /// </summary>
        public string nyuukinsakiCd { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public NyuukinsakiNyuuryokuHoshuLogic(NyuukinsakiNyuuryokuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.nyuukinsakiDao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.furikomiDao = DaoInitUtility.GetComponent<IM_NYUUKINSAKI_FURIKOMIDao>();
            this.torisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.isRegist = false;

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

                // イベントの初期化
                this.EventInit(parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);

                this.allControl = this.form.allControl;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInit", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        private void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
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
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    catchErr = this.WindowInitReference(parentForm);
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    break;
            }
            if (catchErr)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            bool catchErr = false;
            if (string.IsNullOrEmpty(this.nyuukinsakiCd))
            {
                // 【新規】モードで初期化
                catchErr = WindowInitNewMode(parentForm);
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
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロール操作可能とする
                this.AllControlLock(false);

                // 各テキストボックス設定(BaseHeader部)
                BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
                DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;

                // 入力項目
                this.form.NYUUKINSAKI_CD.Text = string.Empty;
                this.form.NYUUKINSAKI_FURIGANA.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME1.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME2.Text = string.Empty;
                this.form.NYUUKINSAKI_NAME_RYAKU.Text = string.Empty;
                this.form.NYUUKINSAKI_TEL.Text = string.Empty;
                this.form.NYUUKINSAKI_FAX.Text = string.Empty;
                this.form.NYUUKINSAKI_POST.Text = string.Empty;
                this.form.NYUUKINSAKI_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TODOUFUKEN_NAME.Text = string.Empty;
                this.form.NYUUKINSAKI_ADDRESS1.Text = string.Empty;
                this.form.NYUUKINSAKI_ADDRESS2.Text = string.Empty;
                this.form.TORIKOMI_KBN.Text = this.GetNyuukinTorikomiKbn();

                // グリッド
                this.form.Ichiran_furikomi.DataSource = null;
                this.form.Ichiran_torihikisaki.DataSource = null;
                this.form.Ichiran_furikomi.Rows.Clear();
                this.form.Ichiran_furikomi.RowCount = 1;
                this.form.Ichiran_torihikisaki.Rows.Clear();
                this.form.Ichiran_torihikisaki.RowCount = 1;
                this.form.Ichiran_furikomi.ColumnHeaders[NyuukinsakiNyuuryokuHoshuConstans.COLUMN_HEADER_SECTION_NAME].Cells[NyuukinsakiNyuuryokuHoshuConstans.CUSTOM_CHECKBOX_NAME].Value = false;

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func4.Enabled = true;     // 行削除
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                // 内部データ
                this.nyuukinsakiEntity = new M_NYUUKINSAKI();
                this.todoufukenEntity = new M_TODOUFUKEN();

                this.form.NYUUKINSAKI_CD.Focus();
                return false;
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
            this.AllControlLock(false);

            // 検索結果を画面に設定
            this.SetWindowData();

            // functionボタン
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func4.Enabled = true;     // 行削除
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消

            // 複写モード時初期化項目
            this.form.NYUUKINSAKI_CD.Text = string.Empty;       // 入金先CD

            // ヘッダー項目初期化
            var header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;

            // 取引先一覧初期化
            this.form.Ichiran_torihikisaki.DataSource = null;
            this.form.Ichiran_torihikisaki.Rows.Clear();
            this.form.Ichiran_torihikisaki.RowCount = 1;
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
                this.AllControlLock(false);

                // 検索結果を画面に設定
                this.SetWindowData();

                // 自動的に作成された（取引先CDに同CDが存在する）入金先データ
                // は取引先の情報と整合性を取るために、取引先から引き継いだ情報は修正不可とする
                var entTorihikisaki = this.torisakiDao.GetDataByCd(this.nyuukinsakiCd);
                if (entTorihikisaki != null)
                {
                    this.ControlLock_ForAutoRegisteredData(true);
                }

                // 修正モード固有UI設定
                this.form.NYUUKINSAKI_CD.ReadOnly = true;   // 入金先CD
                this.form.bt_saiban.Enabled = false;        // 採番ボタン

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func4.Enabled = true;     // 行削除
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
            this.AllControlLock(true);

            // functionボタン
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func4.Enabled = false;     // 行削除
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

                // 参照モード固有UI設定
                this.AllControlLock(true);

                // functionボタン
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func4.Enabled = false;     // 行削除
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
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
                LogUtility.Error("WindowInitReference", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            this.Search();

            // 各テキストボックス設定(BaseHeader部)
            BusinessBaseForm findForm = (BusinessBaseForm)this.form.Parent.FindForm();
            DetailedHeaderForm header = (DetailedHeaderForm)findForm.headerForm;
            header.CreateDate.Text = this.nyuukinsakiEntity.CREATE_DATE.ToString();
            header.CreateUser.Text = this.nyuukinsakiEntity.CREATE_USER;
            header.LastUpdateDate.Text = this.nyuukinsakiEntity.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.nyuukinsakiEntity.UPDATE_USER;

            // 各テキストボックス設定(入金先マスタ)
            this.form.NYUUKINSAKI_CD.Text = this.nyuukinsakiCd;
            this.form.NYUUKINSAKI_NAME1.Text = this.nyuukinsakiEntity.NYUUKINSAKI_NAME1;
            this.form.NYUUKINSAKI_NAME2.Text = this.nyuukinsakiEntity.NYUUKINSAKI_NAME2;
            this.form.NYUUKINSAKI_FURIGANA.Text = this.nyuukinsakiEntity.NYUUKINSAKI_FURIGANA;
            this.form.NYUUKINSAKI_NAME_RYAKU.Text = this.nyuukinsakiEntity.NYUUKINSAKI_NAME_RYAKU;
            this.form.NYUUKINSAKI_TEL.Text = this.nyuukinsakiEntity.NYUUKINSAKI_TEL;
            this.form.NYUUKINSAKI_FAX.Text = this.nyuukinsakiEntity.NYUUKINSAKI_FAX;
            this.form.NYUUKINSAKI_POST.Text = this.nyuukinsakiEntity.NYUUKINSAKI_POST;
            if (!this.todoufukenEntity.TODOUFUKEN_CD.IsNull)
            {
                this.form.NYUUKINSAKI_TODOUFUKEN_CD.Text = this.todoufukenEntity.TODOUFUKEN_CD.ToString();
            }
            this.form.TODOUFUKEN_NAME.Text = this.todoufukenEntity.TODOUFUKEN_NAME;
            this.form.NYUUKINSAKI_ADDRESS1.Text = this.nyuukinsakiEntity.NYUUKINSAKI_ADDRESS1;
            this.form.NYUUKINSAKI_ADDRESS2.Text = this.nyuukinsakiEntity.NYUUKINSAKI_ADDRESS2;
            this.form.TORIKOMI_KBN.Text = this.GetNyuukinTorikomiKbn(this.nyuukinsakiEntity.TORIKOMI_KBN);

            // グリッド設定(振り込みマスタ、取引先マスタ)
            this.SetIchiran(this.form.Ichiran_furikomi, furikomiTable);

            this.form.Ichiran_torihikisaki.IsBrowsePurpose = false;
            this.SetIchiran(this.form.Ichiran_torihikisaki, torihikisakiTable);
            this.form.Ichiran_torihikisaki.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            this.form.NYUUKINSAKI_CD.ReadOnly = isBool;               // 入金先CD
            this.form.NYUUKINSAKI_FURIGANA.ReadOnly = isBool;         // フリガナ
            this.form.NYUUKINSAKI_NAME1.ReadOnly = isBool;            // 入金先名1
            this.form.NYUUKINSAKI_NAME2.ReadOnly = isBool;            // 入金先名2
            this.form.NYUUKINSAKI_NAME_RYAKU.ReadOnly = isBool;       // 略称名
            this.form.NYUUKINSAKI_POST.ReadOnly = isBool;             // 郵便番号
            this.form.NYUUKINSAKI_TEL.ReadOnly = isBool;              // 電話番号
            this.form.NYUUKINSAKI_FAX.ReadOnly = isBool;              // FAX番号
            this.form.NYUUKINSAKI_TODOUFUKEN_CD.ReadOnly = isBool;    // 都道府県CD
            this.form.NYUUKINSAKI_ADDRESS1.ReadOnly = isBool;         // 住所1
            this.form.NYUUKINSAKI_ADDRESS2.ReadOnly = isBool;         // 住所2
            this.form.TORIKOMI_KBN.ReadOnly = isBool;                 // 取込区分
            this.form.Ichiran_furikomi.ReadOnly = isBool;             // 振り込み人名一覧

            this.form.bt_saiban.Enabled = !isBool;                    // 採番ボタン
            this.form.bt_address.Enabled = !isBool;                   // 住所参照ボタン
            this.form.bt_post.Enabled = !isBool;                      // 郵便番号参照ボタン
            this.form.rbt_suru.Enabled = !isBool;                     // 「する」ラジオボタン
            this.form.rbt_shinai.Enabled = !isBool;                   // 「しない」ラジオボタン
            this.form.NYUUKINSAKI_TODOUFUKEN_CD.Enabled = !isBool;    // 都道府県CD(ポップアップ禁止の為)
        }

        /// <summary>
        /// コントロール制御
        /// 取引先から引き継いだ情報のロック／解除
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void ControlLock_ForAutoRegisteredData(bool isBool)
        {
            this.form.NYUUKINSAKI_CD.ReadOnly = isBool;               // 入金先CD
            this.form.NYUUKINSAKI_FURIGANA.ReadOnly = isBool;         // フリガナ
            this.form.NYUUKINSAKI_NAME1.ReadOnly = isBool;            // 入金先名1
            this.form.NYUUKINSAKI_NAME2.ReadOnly = isBool;            // 入金先名2
            this.form.NYUUKINSAKI_NAME_RYAKU.ReadOnly = isBool;       // 略称名
            this.form.NYUUKINSAKI_POST.ReadOnly = isBool;             // 郵便番号
            this.form.NYUUKINSAKI_TEL.ReadOnly = isBool;              // 電話番号
            this.form.NYUUKINSAKI_FAX.ReadOnly = isBool;              // FAX番号
            this.form.NYUUKINSAKI_TODOUFUKEN_CD.ReadOnly = isBool;    // 都道府県CD
            this.form.NYUUKINSAKI_ADDRESS1.ReadOnly = isBool;         // 住所1
            this.form.NYUUKINSAKI_ADDRESS2.ReadOnly = isBool;         // 住所2
            // ※[取込区分]はシステム設定から取得している為、修正可能とする
            // ※[振り込み人名一覧]は取引先入力で設定不可能の為、修正可能とする

            this.form.bt_saiban.Enabled = !isBool;                    // 採番ボタン
            this.form.bt_address.Enabled = !isBool;                   // 住所参照ボタン
            this.form.bt_post.Enabled = !isBool;                      // 郵便番号参照ボタン
            // ※「する」ラジオボタンは[取込区分]の為、修正可能とする
            // ※「しない」ラジオボタンは[取込区分]の為、修正可能とする
            this.form.NYUUKINSAKI_TODOUFUKEN_CD.Enabled = !isBool;    // 都道府県CD(ポップアップ禁止の為)
        }

        /// <summary>
        /// システム設定マスタから入金取込区分を取得
        /// </summary>
        /// <returns>入金取込区分(1:する 2:しない)</returns>
        private string GetNyuukinTorikomiKbn()
        {
            int temp = 0;

            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo.Length > 0)
            {
                temp = int.Parse(sysInfo[0].NYUUKIN_TORIKOMI_KBN.Value.ToString());
            }

            string kbn = temp < 1 || 2 < temp ? "1" : temp.ToString();

            return kbn;
        }

        /// <summary>
        /// 入金取込区分を取得
        /// </summary>
        /// <param name="torikomiKbn">入金取込区分</param>
        /// <returns>入金取込区分(1:する 2:しない)</returns>
        private string GetNyuukinTorikomiKbn(System.Data.SqlTypes.SqlInt16 torikomiKbn)
        {
            string kbn = string.Empty;

            if (torikomiKbn.IsNull)
            {
                kbn = this.GetNyuukinTorikomiKbn();
            }
            else
            {
                kbn = torikomiKbn.Value.ToString();
            }

            return kbn;
        }

        /// <summary>
        /// 入金先CD採番処理
        /// </summary>
        /// <returns>最大CD+1</returns>
        public bool Saiban()
        {
            try
            {
                // 入金先マスタと取引先マスタのCDの最大値+1を取得
                NyuukinsakiMasterAccess nuukinsakiMasterAccess = new NyuukinsakiMasterAccess(new CustomTextBox(), new object[] { }, new object[] { });
                int keyNyuukin = -1;

                var keyNuukinssaibanFlag = nuukinsakiMasterAccess.IsOverCDLimit(out keyNyuukin);

                if (keyNuukinssaibanFlag)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.NYUUKINSAKI_CD.Text = "";
                    return false;
                }

                if (keyNyuukin < 1)
                {
                    // 採番エラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E041");
                    this.form.NYUUKINSAKI_CD.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.NYUUKINSAKI_CD.Text = String.Format("{0:D" + this.form.NYUUKINSAKI_CD.MaxLength + "}", keyNyuukin);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

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
                    this.nyuukinsakiEntity == null ||
                    this.nyuukinsakiEntity.TIME_STAMP == null || this.nyuukinsakiEntity.TIME_STAMP.Length != 8)
                {
                    this.nyuukinsakiEntity = new M_NYUUKINSAKI();
                }
                else
                {
                    this.nyuukinsakiEntity = new M_NYUUKINSAKI() { TIME_STAMP = this.nyuukinsakiEntity.TIME_STAMP };
                }
                // 20151014 委託契約書入力(M001)システムエラー発生関連チケット End
                // 現在の入力内容でEntity作成
                this.nyuukinsakiEntity.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_FURIGANA = this.form.NYUUKINSAKI_FURIGANA.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_NAME1 = this.form.NYUUKINSAKI_NAME1.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_NAME2 = this.form.NYUUKINSAKI_NAME2.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_NAME_RYAKU = this.form.NYUUKINSAKI_NAME_RYAKU.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_TEL = this.form.NYUUKINSAKI_TEL.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_FAX = this.form.NYUUKINSAKI_FAX.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_POST = this.form.NYUUKINSAKI_POST.Text;
                if (!String.IsNullOrWhiteSpace(this.form.NYUUKINSAKI_TODOUFUKEN_CD.Text))
                {
                    this.nyuukinsakiEntity.NYUUKINSAKI_TODOUFUKEN_CD = Int16.Parse(this.form.NYUUKINSAKI_TODOUFUKEN_CD.Text);
                }
                this.nyuukinsakiEntity.NYUUKINSAKI_ADDRESS1 = this.form.NYUUKINSAKI_ADDRESS1.Text;
                this.nyuukinsakiEntity.NYUUKINSAKI_ADDRESS2 = this.form.NYUUKINSAKI_ADDRESS2.Text;
                this.nyuukinsakiEntity.TORIKOMI_KBN = Int16.Parse(this.form.TORIKOMI_KBN.Text);

                // 更新者情報設定
                var dataBinderLogicNyuukin = new DataBinderLogic<r_framework.Entity.M_NYUUKINSAKI>(this.nyuukinsakiEntity);
                dataBinderLogicNyuukin.SetSystemProperty(this.nyuukinsakiEntity, false);
                MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.nyuukinsakiEntity);

                // グリッド設定(振り込みマスタ)
                List<M_NYUUKINSAKI_FURIKOMI> tempList = new List<M_NYUUKINSAKI_FURIKOMI>();
                foreach (Row dr in this.form.Ichiran_furikomi.Rows)
                {
                    if (dr.IsNewRow || (bool)dr.Cells[0].FormattedValue || dr.Cells[1].Value == null)
                    {
                        // 新規行、チェックボックスON、フリコミ人名がnullの場合は飛ばす
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(dr.Cells[1].Value.ToString()))
                    {
                        // フリコミ人名が空でも飛ばす
                        continue;
                    }

                    M_NYUUKINSAKI_FURIKOMI temp = new M_NYUUKINSAKI_FURIKOMI();
                    temp.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;
                    temp.FURIKOMI_NAME = dr[1].Value.ToString();

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_NYUUKINSAKI_FURIKOMI>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), temp);
                    tempList.Add(temp);
                }

                this.entitys = new M_NYUUKINSAKI_FURIKOMI[tempList.Count];
                this.entitys = tempList.ToArray<M_NYUUKINSAKI_FURIKOMI>();

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

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var nyuukinsakiCd = this.form.NYUUKINSAKI_CD.Text;

            if (!string.IsNullOrEmpty(nyuukinsakiCd))
            {
                M_NYUUKINSAKI data = new M_NYUUKINSAKI();
                data.NYUUKINSAKI_CD = nyuukinsakiCd;

                DataTable dtTable = this.nyuukinsakiDao.GetDataBySqlFile(this.CHECK_DELETE_NYUUKINSAKI_SQL, data);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += Environment.NewLine + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "入金先", "入金先CD", strName);

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
        /// ゼロパディング処理
        /// </summary>
        /// <param name="inputCd">ゼロパディングしたい文字列</param>
        /// <returns>ゼロパディングされた文字列</returns>
        public string ZeroPadding(string inputData)
        {
            if (!(Regex.Match(inputData, "^[a-zA-Z0-9]+$")).Success)
            {
                return inputData;
            }

            PropertyInfo pi = this.form.NYUUKINSAKI_CD.GetType().GetProperty(NyuukinsakiNyuuryokuHoshuConstans.CHARACTERS_NUMBER);
            Decimal charNumber = (Decimal)pi.GetValue(this.form.NYUUKINSAKI_CD, null);

            string padData = inputData.PadLeft((int)charNumber, '0');

            return padData;
        }

        /// <summary>
        /// 入金先CD重複チェック
        /// </summary>
        /// <returns></returns>
        public NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult DupliCheckNyuukinsakiCd(string zeroPadCd)
        {
            LogUtility.DebugMethodStart();

            // 入金先マスタ検索
            M_NYUUKINSAKI nyuukinSearch = new M_NYUUKINSAKI();
            nyuukinSearch.NYUUKINSAKI_CD = zeroPadCd;
            DataTable nyuukinTable =
                this.nyuukinsakiDao.GetDataBySqlFile(GET_INPUTCD_DATA_NYUUKINSAKI_SQL, nyuukinSearch);

            // 取引先マスタ検索
            M_TORIHIKISAKI torihikisakiSearchSetting = new M_TORIHIKISAKI();
            torihikisakiSearchSetting.TORIHIKISAKI_CD = zeroPadCd;
            DataTable torihikiTable =
                this.torisakiDao.GetDataBySqlFile(GET_INPUTCD_DATA_TORIHIKISAKI_SQL, torihikisakiSearchSetting);

            // 重複チェック
            NyuukinsakiNyuuryokuHoshuValidator vali = new NyuukinsakiNyuuryokuHoshuValidator();
            DialogResult resultDialog = new DialogResult();
            bool resultDupli = vali.NyuukinsakiCDValidator(nyuukinTable, torihikiTable, out resultDialog);

            NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult ViewUpdateWindow = 0;

            // 重複あり かつ [はい]ボタン選択時
            if (!resultDupli && resultDialog == DialogResult.Yes)
            {
                ViewUpdateWindow = NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.FALSE_ON;
            }
            else if (!resultDupli && resultDialog == DialogResult.No)
            {
                ViewUpdateWindow = NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.FALSE_OFF;
            }
            else if (!resultDupli)
            {
                ViewUpdateWindow = NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.FALSE_NONE;
            }
            else
            {
                ViewUpdateWindow = NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.TURE_NONE;
            }

            LogUtility.DebugMethodEnd();

            return ViewUpdateWindow;
        }

        /// <summary>
        /// フリコミ人名の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DupliCheckFurikomiName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                NyuukinsakiNyuuryokuHoshuValidator vali = new NyuukinsakiNyuuryokuHoshuValidator();
                bool result = vali.FurikomiNameValidator(this.form.Ichiran_furikomi);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliCheckFurikomiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public virtual int Search()
        {
            // 入金先CDでDB検索(入金先マスタ)
            this.nyuukinsakiEntity = this.nyuukinsakiDao.GetDataByCd(this.nyuukinsakiCd);

            // 入金先CDでDB検索(入金先振り込みマスタ)
            M_NYUUKINSAKI_FURIKOMI furikomiSearchString = new M_NYUUKINSAKI_FURIKOMI();
            furikomiSearchString.NYUUKINSAKI_CD = this.nyuukinsakiCd;
            furikomiTable = this.furikomiDao.GetDataBySqlFile(GET_FURIKOMISAKI_DATA_SQL, furikomiSearchString);

            // 入金先CDでDB検索(取引先マスタ)
            M_TORIHIKISAKI_SEIKYUU torihikisakiSearchSetting = new M_TORIHIKISAKI_SEIKYUU();
            torihikisakiSearchSetting.NYUUKINSAKI_CD = this.nyuukinsakiCd;
            torihikisakiTable = this.torikyuuDao.GetDataBySqlFile(GET_TORIHIKISAKI_SEIKYUU_DATA_SQL, torihikisakiSearchSetting);

            // 都道府県CDでDB検索(都道府県マスタ)
            string todoufukenCd = string.Empty;
            string todoufukenName = string.Empty;
            todoufukenEntity = new M_TODOUFUKEN();
            if (!this.nyuukinsakiEntity.NYUUKINSAKI_TODOUFUKEN_CD.IsNull)
            {
                // 都道府県マスタ検索
                todoufukenEntity =
                    this.todoufukenDao.GetDataByCd(this.nyuukinsakiEntity.NYUUKINSAKI_TODOUFUKEN_CD.ToString());

                // 出力値取得
                todoufukenCd = todoufukenEntity.TODOUFUKEN_CD.ToString();
                todoufukenName = todoufukenEntity.TODOUFUKEN_NAME;
            }

            return 0;
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
                using (var tran = new Transaction())
                {
                    // 入金先マスタ更新
                    this.nyuukinsakiEntity.DELETE_FLG = false;
                    this.nyuukinsakiDao.Insert(this.nyuukinsakiEntity);

                    // 入金先振り込みマスタ更新(削除対象以外をINSERT)
                    for (int i = 0; i < this.entitys.Length; i++)
                    {
                        entitys[i].FURIKOMI_SEQ = (Int16)(i + 1);
                        this.furikomiDao.Insert(entitys[i]);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "登録");
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
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (errorFlag)
                {
                    return;
                }

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    // 入金先マスタ更新
                    this.nyuukinsakiEntity.DELETE_FLG = false;
                    this.nyuukinsakiDao.Update(this.nyuukinsakiEntity);

                    // 入金先振り込みマスタの該当CDデータ全DELETE
                    this.PhysicalDelete();

                    // 削除チェックが付いていないグリッドデータをINSERT
                    for (int i = 0; i < this.entitys.Length; i++)
                    {
                        // INSERT
                        this.entitys[i].FURIKOMI_SEQ = (Int16)(i + 1);
                        this.furikomiDao.Insert(entitys[i]);
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "更新");
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
                    this.nyuukinsakiEntity.DELETE_FLG = true;

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        this.nyuukinsakiDao.Update(this.nyuukinsakiEntity);
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");

                    this.isRegist = true;
                }

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
        public virtual void PhysicalDelete()
        {
            // 入金先振り込みマスタから該当する入金先CDのデータをDELETE
            M_NYUUKINSAKI_FURIKOMI furikomiSearchSetting = new M_NYUUKINSAKI_FURIKOMI();
            furikomiSearchSetting.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;

            this.furikomiDao.GetDataBySqlFile(DELETE_FURIKOMISAKI_DATA_SQL, furikomiSearchSetting);
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

            NyuukinsakiNyuuryokuHoshuLogic localLogic = other as NyuukinsakiNyuuryokuHoshuLogic;
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
        internal void SetIchiran(GcMultiRow control, DataTable dt)
        {
            var table = dt;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            control.DataSource = table;
        }

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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            //新規ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

            //修正ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

            //行削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.DeleteRow);

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
        /// フリコミ人名ヘッダーチェックボックス変更処理
        /// </summary>
        public bool ChangeHeaderCheckBox()
        {
            try
            {
                // ヘッダーチェックボックスの値取得
                bool check = (bool)this.form.Ichiran_furikomi.CurrentCell.EditedFormattedValue;

                foreach (Row temp in this.form.Ichiran_furikomi.Rows)
                {
                    if (temp.IsNewRow)
                    {
                        continue;
                    }

                    temp[0].Value = check;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeHeaderCheckBox", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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

                r_framework.FormManager.FormManager.OpenFormWithAuth("M555", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.NYUUKINSAKI);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 行削除処理
        /// </summary>
        internal void DeleteRow()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                LogUtility.DebugMethodStart();
                
                #region チェックボックス確認
                bool check = false;
                foreach (Row fDr in this.form.Ichiran_furikomi.Rows)
                {
                    if((bool)fDr.Cells["DELETE_FLG"].FormattedValue)
                    {
                        check = true;
                        break;
                    }
                }
                if(!check)
                {
                    // チェックオンである行が1つも存在しない場合
                    msgLogic.MessageBoxShow("E051","行");
                    return;
                }
                #endregion

                var result = msgLogic.MessageBoxShow("C021");
                if (result != DialogResult.Yes)
                {
                    return;
                }
                // 空のDataTable作成
                DataTable dt = new DataTable();
                dt.Columns.Add("DELETE_FLG",typeof(bool));
　	            dt.Columns.Add("FURIKOMI_NAME",typeof(string));
                foreach (Row row in this.form.Ichiran_furikomi.Rows)
　              { 
　	                if(row.IsNewRow || (bool)row.Cells["DELETE_FLG"].FormattedValue)
　	                {
                        // 新規行、チェックボックスONの場合は飛ばす
　		                continue;
　	                }
                    DataRow newRow = dt.NewRow();
                    newRow["DELETE_FLG"] = false;
                    newRow["FURIKOMI_NAME"] = row.Cells["FURIKOMI_NAME"].Value.ToString();
                    dt.Rows.Add(newRow);
                }
                this.form.Ichiran_furikomi.DataSource = dt;
                msgLogic.MessageBoxShow("I001", "削除");
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteRow", ex);
                msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}