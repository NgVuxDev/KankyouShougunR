// $Id: ZipCodeKobetsuHoshuLogic.cs 50403 2015-05-22 07:03:32Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Shougun.Core.Common.BusinessCommon;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using ZipCodeHoshu.APP;
using ZipCodeHoshu.Const;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace ZipCodeHoshu.Logic
{
    /// <summary>
    /// 郵便辞書保守画面(個別)のビジネスロジック
    /// </summary>
    public class ZipCodeKobetsuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ZipCodeHoshu.Setting.ButtonSettingKobetsu.xml";

        private readonly string GET_KOBETSU_DATA_KYOTEN_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataKyotenSql.sql";

        private readonly string GET_KOBETSU_DATA_TORIHIKISAKI_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataTorihikisakiSql.sql";

        private readonly string GET_KOBETSU_DATA_TORIHIKISAKI_SEIKYUU_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataTorihikisakiSeikyuuSql.sql";

        private readonly string GET_KOBETSU_DATA_TORIHIKISAKI_SHIHARAI_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataTorihikisakiShiharaiSql.sql";

        private readonly string GET_KOBETSU_DATA_GYOUSHA_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataGyoushaSql.sql";

        private readonly string GET_KOBETSU_DATA_GENBA_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataGenbaSql.sql";

        private readonly string GET_KOBETSU_DATA_NYUUKINSAKI_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataNyuukinsakiSql.sql";

        private readonly string GET_KOBETSU_DATA_SYUKKINSAKI_SQL = "ZipCodeHoshu.Sql.GetKobetsuDataSyukkinsakiSql.sql";

        private readonly string GET_ZIP_CODE_DATA_BY_SYS_ID_SQL = "ZipCodeHoshu.Sql.GetZipCodeDataBySysId.sql";

        private readonly string UPDATE_KYOTEN_DATA_SQL = "ZipCodeHoshu.Sql.UpdateKyotenDataSql.sql";

        private readonly string UPDATE_TORIHIKISAKI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateTorihikisakiDataSql.sql";

        private readonly string UPDATE_TORIHIKISAKI_SEIKYUU_DATA_SQL = "ZipCodeHoshu.Sql.UpdateTorihikisakiSeikyuuDataSql.sql";

        private readonly string UPDATE_TORIHIKISAKI_SHIHARAI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateTorihikisakiShiharaiDataSql.sql";

        private readonly string UPDATE_TORIHIKISAKI_MANIFEST_DATA_SQL = "ZipCodeHoshu.Sql.UpdateTorihikisakiManifestDataSql.sql";

        private readonly string UPDATE_GYOUSHA_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGyoushaDataSql.sql";

        private readonly string UPDATE_GYOUSHA_SEIKYUU_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGyoushaSeikyuuDataSql.sql";

        private readonly string UPDATE_GYOUSHA_SHIHARAI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGyoushaShiharaiDataSql.sql";

        private readonly string UPDATE_GYOUSHA_MANIFEST_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGyoushaManifestDataSql.sql";

        private readonly string UPDATE_GENBA_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGenbaDataSql.sql";

        private readonly string UPDATE_GENBA_SEIKYUU_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGenbaSeikyuuDataSql.sql";

        private readonly string UPDATE_GENBA_SHIHARAI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGenbaShiharaiDataSql.sql";

        private readonly string UPDATE_GENBA_MANIFEST_DATA_SQL = "ZipCodeHoshu.Sql.UpdateGenbaManifestDataSql.sql";

        private readonly string UPDATE_NYUUKINSAKI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateNyuukinsakiDataSql.sql";

        private readonly string UPDATE_SYUKKINSAKI_DATA_SQL = "ZipCodeHoshu.Sql.UpdateSyukkinsakiDataSql.sql";

        /// <summary>
        /// 社員保守画面Form
        /// </summary>
        private ZipCodeKobetsuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 郵便番号辞書のエンティティ
        /// </summary>
        private S_ZIP_CODE[] entitys;

        /// <summary>
        /// 郵便番号辞書のDao
        /// </summary>
        private IS_ZIP_CODEDao dao;

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 取引先請求のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// 取引先支払のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 入金先のDao
        /// </summary>
        private IM_NYUUKINSAKIDao nyuukinsakiDao;

        /// <summary>
        /// 出金先のDao
        /// </summary>
        private IM_SYUKKINSAKIDao syukkinsakiDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件(拠点)
        /// </summary>
        public M_KYOTEN SearchStringKyoten { get; set; }

        /// <summary>
        /// 検索条件(取引先)
        /// </summary>
        public M_TORIHIKISAKI SearchStringTorihikisaki { get; set; }

        /// <summary>
        /// 検索条件(取引先_請求)
        /// </summary>
        public M_TORIHIKISAKI_SEIKYUU SearchStringTorihikisakiSeikyuu { get; set; }

        /// <summary>
        /// 検索条件(取引先_支払)
        /// </summary>
        public M_TORIHIKISAKI_SHIHARAI SearchStringTorihikisakiShiharai { get; set; }

        /// <summary>
        /// 検索条件(業者)
        /// </summary>
        public M_GYOUSHA SearchStringGyousha { get; set; }

        /// <summary>
        /// 検索条件(現場)
        /// </summary>
        public M_GENBA SearchStringGenba { get; set; }

        /// <summary>
        /// 検索条件(入金先)
        /// </summary>
        public M_NYUUKINSAKI SearchStringNyuukinsaki { get; set; }

        /// <summary>
        /// 検索条件(出金先)
        /// </summary>
        public M_SYUKKINSAKI SearchStringSyukkinsaki { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ZipCodeKobetsuHoshuLogic(ZipCodeKobetsuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.nyuukinsakiDao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.syukkinsakiDao = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();

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
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.POST7_OLD.Text = Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT;
                this.form.SIKUCHOUSON_OLD.Text = Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT;
                this.form.POST7_NEW.Text = Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT;
                this.form.SIKUCHOUSON_NEW.Text = Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M253", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }

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
        private void DispReferenceMode()
        {
            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                // マスタ毎にデータを抽出する
                DataTable dtKyoten = this.kyotenDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_KYOTEN_SQL, this.SearchStringKyoten);
                DataTable dtTorihikisaki = this.torihikisakiDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_TORIHIKISAKI_SQL, this.SearchStringTorihikisaki);
                DataTable dtTorihikisakiSeikyuu = this.torihikisakiSeikyuuDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_TORIHIKISAKI_SEIKYUU_SQL, this.SearchStringTorihikisakiSeikyuu);
                DataTable dtTorihikisakiShiharai = this.torihikisakiShiharaiDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_TORIHIKISAKI_SHIHARAI_SQL, this.SearchStringTorihikisakiShiharai);
                DataTable dtGyousha = this.gyoushaDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_GYOUSHA_SQL, this.SearchStringGyousha);
                DataTable dtGenba = this.genbaDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_GENBA_SQL, this.SearchStringGenba);
                DataTable dtNyuukinsaki = this.nyuukinsakiDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_NYUUKINSAKI_SQL, this.SearchStringNyuukinsaki);
                DataTable dtSyukkinsaki = this.syukkinsakiDao.GetDataBySqlFile(this.GET_KOBETSU_DATA_SYUKKINSAKI_SQL, this.SearchStringSyukkinsaki);

                // データのマージ
                DataTable mergeTable = this.GetCloneDataTable(dtKyoten);
                mergeTable.BeginLoadData();
                mergeTable.Merge(dtTorihikisaki);
                mergeTable.Merge(dtTorihikisakiSeikyuu);
                mergeTable.Merge(dtTorihikisakiShiharai);
                mergeTable.Merge(dtGyousha);
                mergeTable.Merge(dtGenba);
                mergeTable.Merge(dtNyuukinsaki);
                mergeTable.Merge(dtSyukkinsaki);

                // ソート処理
                this.SearchResult = mergeTable.Clone();
                this.SearchResult.BeginLoadData();
                DataRow[] rows = mergeTable.Select("", "MENU_NAME, ITEM_NAME");
                foreach (DataRow row in rows)
                {
                    DataRow add = this.SearchResult.NewRow();
                    add.ItemArray = row.ItemArray;
                    // 名前アップデート処理
                    switch (add["MENU_CD"].ToString())
                    {
                        case "1":
                            add["MENU_NAME"] = "拠点";
                            if (add["ITEM_CD"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "拠点住所";
                            }
                            break;
                        case "2":
                            add["MENU_NAME"] = "取引先";
                            if (add["ITEM_NAME"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "取引先住所";
                            }
                            break;
                        case "3":
                            add["MENU_NAME"] = "業者";
                            if (add["ITEM_NAME"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "業者住所";
                            }
                            break;
                        case "4":
                            add["MENU_NAME"] = "現場";
                            if (add["ITEM_NAME"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "現場住所";
                            }
                            break;
                        case "5":
                            add["MENU_NAME"] = "入金先";
                            if (add["ITEM_NAME"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "入金先住所";
                            }
                            break;
                        case "6":
                            add["MENU_NAME"] = "出金先";
                            if (add["ITEM_NAME"].ToString().Equals("1"))
                            {
                                add["ITEM_NAME"] = "出金先住所";
                            }
                            break;
                    }
                    switch (add["ITEM_CD"].ToString())
                    {
                        case "2":
                            add["ITEM_NAME"] = "請求書送付先住所";
                            break;
                        case "3":
                            add["ITEM_NAME"] = "支払明細書送付先住所";
                            break;
                        case "4":
                            add["ITEM_NAME"] = "マニフェスト返送先住所";
                            break;
                    }
                    this.SearchResult.Rows.Add(add);
                }

                Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT = this.form.POST7_OLD.Text;
                Properties.Settings.Default.KOBETSU_SIKUCHOUSON_OLD_TEXT = this.form.SIKUCHOUSON_OLD.Text;
                Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT = this.form.POST7_NEW.Text;
                Properties.Settings.Default.KOBETSU_SIKUCHOUSON_NEW_TEXT = this.form.SIKUCHOUSON_NEW.Text;
                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

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
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //this.form.POST7_OLD.Text = Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT;
                //this.form.SIKUCHOUSON_OLD.Text = Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT;
                //this.form.POST7_NEW.Text = Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT;
                //this.form.SIKUCHOUSON_NEW.Text = Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT;
                this.form.POST7_OLD.Text = string.Empty;
                this.form.SIKUCHOUSON_OLD.Text = string.Empty;
                this.form.Ichiran.DataSource = null;
                if (this.SearchResult != null)
                {
                    this.SearchResult.Rows.Clear();
                }
                SetSearchString();

                LogUtility.DebugMethodEnd();
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
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "郵便辞書一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除
                    //msgLogic.MessageBoxShow("I000");
                    #endregion
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.form.POST7_OLD.Text = string.Empty;
                this.form.SIKUCHOUSON_OLD.Text = string.Empty;
                //20150519 minhhoang edit #1748
                //do not reload search result when F7 press
                //this.form.Ichiran.DataSource = null;
                //this.SearchResult.Rows.Clear();
                //20150519 minhhoang end edit #1748
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
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
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                //独自チェックの記述例を書く
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                MessageUtility msgUtil = new MessageUtility();
                M_ERROR_MESSAGE errorData = msgUtil.GetMessage("E001");
                List<string> msg = new List<string>();
                if (string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    MessageBox.Show("変更を行う条件を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LogUtility.DebugMethodEnd();
                    return;
                }
                if (!string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "新郵便番号"));
                    errorFlag = true;
                }
                if (string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text) && !string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "旧郵便番号"));
                    errorFlag = true;
                }
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "新住所"));
                    errorFlag = true;
                }
                if (string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text) && !string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text))
                {
                    msg.Add(string.Format(errorData.MESSAGE, "旧住所"));
                    errorFlag = true;
                }
                if (errorFlag)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, msg.ToArray()), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    LogUtility.DebugMethodEnd();
                    return;
                }

                //エラーではない場合登録処理を行う
                DataRow row;
                string oldPost = string.Empty;
                string oldAddress = null;
                string newPost = string.Empty;
                string newAddress = null;
                if (!string.IsNullOrWhiteSpace(this.form.POST7_OLD.Text)) oldPost = this.form.POST7_OLD.Text;
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_OLD.Text)) oldAddress = this.form.SIKUCHOUSON_OLD.Text;
                if (!string.IsNullOrWhiteSpace(this.form.POST7_NEW.Text)) newPost = this.form.POST7_NEW.Text;
                if (!string.IsNullOrWhiteSpace(this.form.SIKUCHOUSON_NEW.Text)) newAddress = this.form.SIKUCHOUSON_NEW.Text;
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                    {
                        row = ((DataRowView)this.form.Ichiran.Rows[i].DataBoundItem).Row;
                        if (!(bool)row["CHANGE_FLG"]) continue;

                        switch (row["MENU_CD"].ToString())
                        {
                            case "1":   // 拠点マスタ
                                M_KYOTEN kyoten = new M_KYOTEN();
                                kyoten.KYOTEN_CD = Int16.Parse(row["PKEY1"].ToString());
                                this.kyotenDao.UpdatePartData(this.UPDATE_KYOTEN_DATA_SQL, kyoten, oldPost, oldAddress, newPost, newAddress);
                                break;
                            case "2":   // 取引先マスタ
                                switch (row["ITEM_CD"].ToString())
                                {
                                    case "1":   // 取引先住所
                                        M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
                                        torihikisaki.TORIHIKISAKI_CD = row["PKEY1"].ToString();
                                        this.torihikisakiDao.UpdatePartData(this.UPDATE_TORIHIKISAKI_DATA_SQL, torihikisaki, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "2":   // 請求書送付先住所
                                        M_TORIHIKISAKI_SEIKYUU torihikisakiSei = new M_TORIHIKISAKI_SEIKYUU();
                                        torihikisakiSei.TORIHIKISAKI_CD = row["PKEY1"].ToString();
                                        this.torihikisakiSeikyuuDao.UpdatePartData(this.UPDATE_TORIHIKISAKI_SEIKYUU_DATA_SQL, torihikisakiSei, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "3":   // 支払明細書送付先住所
                                        M_TORIHIKISAKI_SHIHARAI torihikisakiShi = new M_TORIHIKISAKI_SHIHARAI();
                                        torihikisakiShi.TORIHIKISAKI_CD = row["PKEY1"].ToString();
                                        this.torihikisakiShiharaiDao.UpdatePartData(this.UPDATE_TORIHIKISAKI_SHIHARAI_DATA_SQL, torihikisakiShi, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "4":   // マニフェスト送付先住所
                                        M_TORIHIKISAKI torihikisakiMani = new M_TORIHIKISAKI();
                                        torihikisakiMani.TORIHIKISAKI_CD = row["PKEY1"].ToString();
                                        this.torihikisakiDao.UpdatePartData(this.UPDATE_TORIHIKISAKI_MANIFEST_DATA_SQL, torihikisakiMani, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                }
                                break;
                            case "3":   // 業者マスタ
                                M_GYOUSHA gyousha = new M_GYOUSHA();
                                gyousha.GYOUSHA_CD = row["PKEY1"].ToString();
                                switch (row["ITEM_CD"].ToString())
                                {
                                    case "1":   // 業者住所
                                        this.gyoushaDao.UpdatePartData(this.UPDATE_GYOUSHA_DATA_SQL, gyousha, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "2":   // 請求書送付先住所
                                        this.gyoushaDao.UpdatePartData(this.UPDATE_GYOUSHA_SEIKYUU_DATA_SQL, gyousha, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "3":   // 支払明細書送付先住所
                                        this.gyoushaDao.UpdatePartData(this.UPDATE_GYOUSHA_SHIHARAI_DATA_SQL, gyousha, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "4":   // マニフェスト送付先住所
                                        this.gyoushaDao.UpdatePartData(this.UPDATE_GYOUSHA_MANIFEST_DATA_SQL, gyousha, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                }
                                break;
                            case "4":   // 現場マスタ
                                M_GENBA genba = new M_GENBA();
                                genba.GYOUSHA_CD = row["PKEY1"].ToString();
                                genba.GENBA_CD = row["PKEY2"].ToString();
                                switch (row["ITEM_CD"].ToString())
                                {
                                    case "1":   // 業者住所
                                        this.genbaDao.UpdatePartData(this.UPDATE_GENBA_DATA_SQL, genba, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "2":   // 請求書送付先住所
                                        this.genbaDao.UpdatePartData(this.UPDATE_GENBA_SEIKYUU_DATA_SQL, genba, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "3":   // 支払明細書送付先住所
                                        this.genbaDao.UpdatePartData(this.UPDATE_GENBA_SHIHARAI_DATA_SQL, genba, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                    case "4":   // マニフェスト送付先住所
                                        this.genbaDao.UpdatePartData(this.UPDATE_GENBA_MANIFEST_DATA_SQL, genba, oldPost, oldAddress, newPost, newAddress);
                                        break;
                                }
                                break;
                            case "5":   // 入金先マスタ
                                M_NYUUKINSAKI nyuu = new M_NYUUKINSAKI();
                                nyuu.NYUUKINSAKI_CD = row["PKEY1"].ToString();
                                this.nyuukinsakiDao.UpdatePartData(this.UPDATE_NYUUKINSAKI_DATA_SQL, nyuu, oldPost, oldAddress, newPost, newAddress);
                                break;
                            case "6":   // 出金先マスタ
                                M_SYUKKINSAKI syuk = new M_SYUKKINSAKI();
                                syuk.SYUKKINSAKI_CD = row["PKEY1"].ToString();
                                this.syukkinsakiDao.UpdatePartData(this.UPDATE_SYUKKINSAKI_DATA_SQL, syuk, oldPost, oldAddress, newPost, newAddress);
                                break;
                        }
                    }
                    // トランザクション終了
                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "登録");
                this.form.Search(null, null);

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
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

            ZipCodeHoshuLogic localLogic = other as ZipCodeHoshuLogic;
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
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;


            //個別切替(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.ChangeForm);

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

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
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            this.SearchStringKyoten = new M_KYOTEN();
            this.SearchStringTorihikisaki = new M_TORIHIKISAKI();
            this.SearchStringTorihikisakiSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
            this.SearchStringTorihikisakiShiharai = new M_TORIHIKISAKI_SHIHARAI();
            this.SearchStringGyousha = new M_GYOUSHA();
            this.SearchStringGenba = new M_GENBA();
            this.SearchStringNyuukinsaki = new M_NYUUKINSAKI();
            this.SearchStringSyukkinsaki = new M_SYUKKINSAKI();

            if (!string.IsNullOrEmpty(this.form.POST7_OLD.Text))
            {
                this.SearchStringKyoten.KYOTEN_POST = this.form.POST7_OLD.Text;
                this.SearchStringTorihikisaki.TORIHIKISAKI_POST = this.form.POST7_OLD.Text;
                this.SearchStringTorihikisaki.MANI_HENSOUSAKI_POST = this.form.POST7_OLD.Text;
                this.SearchStringTorihikisakiSeikyuu.SEIKYUU_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringTorihikisakiShiharai.SHIHARAI_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringGyousha.GYOUSHA_POST = this.form.POST7_OLD.Text;
                this.SearchStringGyousha.SEIKYUU_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringGyousha.SHIHARAI_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringGyousha.MANI_HENSOUSAKI_POST = this.form.POST7_OLD.Text;
                this.SearchStringGenba.GENBA_POST = this.form.POST7_OLD.Text;
                this.SearchStringGenba.SEIKYUU_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringGenba.SHIHARAI_SOUFU_POST = this.form.POST7_OLD.Text;
                this.SearchStringGenba.MANI_HENSOUSAKI_POST = this.form.POST7_OLD.Text;
                this.SearchStringNyuukinsaki.NYUUKINSAKI_POST = this.form.POST7_OLD.Text;
                this.SearchStringSyukkinsaki.SYUKKINSAKI_POST = this.form.POST7_OLD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SIKUCHOUSON_OLD.Text))
            {
                this.SearchStringKyoten.KYOTEN_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringTorihikisaki.TORIHIKISAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringTorihikisaki.MANI_HENSOUSAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringTorihikisakiSeikyuu.SEIKYUU_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringTorihikisakiShiharai.SHIHARAI_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGyousha.GYOUSHA_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGyousha.SEIKYUU_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGyousha.SHIHARAI_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGyousha.MANI_HENSOUSAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGenba.GENBA_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGenba.SEIKYUU_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGenba.SHIHARAI_SOUFU_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringGenba.MANI_HENSOUSAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringNyuukinsaki.NYUUKINSAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
                this.SearchStringSyukkinsaki.SYUKKINSAKI_ADDRESS1 = this.form.SIKUCHOUSON_OLD.Text;
            }
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// 変更対象ヘッダーチェックボックス変更処理
        /// </summary>
        public bool ChangeHeaderCheckBox()
        {
            try
            {
                // ヘッダーチェックボックスの値取得
                bool check = (bool)this.form.Ichiran.CurrentCell.EditedFormattedValue;

                if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
                {
                    return false;
                }
                foreach (DataRow temp in this.SearchResult.Rows)
                {
                    temp["CHANGE_FLG"] = check;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeHeaderCheckBox", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
    }
}
