// $Id: KensakuKyoutsuuPopupLogic.cs 55781 2015-07-15 08:58:53Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KensakuKyoutsuuPopup.APP;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace KensakuKyoutsuuPopup.Logic
{
    /// <summary>
    /// 検索共通ポップアップロジック
    /// </summary>
    public class KensakuKyoutsuuPopupLogic
    {
        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        internal string[] displayColumnNames = new string[] { };

        internal string[] hideColumnNames = new string[] { };

        private static readonly string tekiyou1 = " (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0 ";

        private static readonly string tekiyou2 = " (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0 ";

        private static readonly string tekiyou3 = " (((CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_BEGIN AND {0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{2}', 120)) OR ({0}.TEKIYOU_END IS NOT NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_BEGIN AND {0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{2}', 120)))"
            + " OR(({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL))"
            + " OR(({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{2}', 120) AND CONVERT(DATETIME, '{2}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{2}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{2}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)))"
            + " AND {0}.DELETE_FLG = 0 ";

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "KensakuKyoutsuuPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private KensakuKyoutsuuPopupForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// Daoに引き渡すSQLファイルのパス
        /// </summary>
        private string executeSqlFilePath = string.Empty;

        /// <summary>
        /// join句
        /// </summary>
        private string joinStr = string.Empty;

        /// <summary>
        /// where句
        /// </summary>
        private string whereStr = string.Empty;

        /// <summary>
        /// orderby句
        /// </summary>
        private string orderStr = string.Empty;

        /// <summary>
        /// orderby句
        /// </summary>
        private string sqlTekiyou = string.Empty;

        /// <summary>
        /// PopupSearchSendParamDtoの最大深度を保持する
        /// </summary>
        private int depthCnt = 0;

        /// <summary>
        /// 検索条件
        /// </summary>
        public SuperEntity SearchInfo { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 起動元への戻り値(カラム名)
        /// </summary>
        public string[] returnParamNames = new string[] { };

        /// <summary>
        /// 絞り込み条件で使用する符号
        /// </summary>
        public enum CNNECTOR_SIGNS
        {
            EQUALS = 0,
            IN = 1
        }

        /// <summary>
        /// Flag changed layout for Torihikisaki, Gyousha
        /// NvNhat #158998 #158999
        /// </summary>
        public bool _flagChangedJouken = false;
        public string[] allowForms = new string[] { 
            "委託契約書許可証期限管理","伝票一括更新", "伝票明細一括更新", "顧客カルテ","マニフェスト終了日一括更新",
            "設置コンテナ一覧", "検収一覧表", "売掛金一覧表","入金予定一覧", "未入金一覧表", "買掛金一覧","混合廃棄物状況一覧","伝票連携状況一覧"
        };

        /// <summary>
        /// CNNECTOR_SIGNSを文字列に変換する
        /// </summary>
        public static class CNNECTOR_SIGNSExt
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string ToTypeString(CNNECTOR_SIGNS e)
            {
                switch (e)
                {
                    case CNNECTOR_SIGNS.EQUALS:
                        return "=";

                    case CNNECTOR_SIGNS.IN:
                        return "IN";
                }
                return String.Empty;
            }
        }

        /// <summary>
        /// M_TORIHIKISAKIの表示項目
        /// </summary>
        private enum TORIHIKISAKI_COLUMNS
        {
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            TODOUFUKEN_NAME_RYAKU,
            TORIHIKISAKI_ADDRESS1,
            TORIHIKISAKI_FURIGANA,
            TORIHIKISAKI_POST,
            TORIHIKISAKI_TEL,
            TORIHIKISAKI_NAME1,
            TORIHIKISAKI_NAME2,
            TORIHIKISAKI_ADDRESS2
        }

        /// <summary>
        /// M_GYOUSHAの表示項目
        /// </summary>
        private enum GYOUSHA_COLUMNS
        {
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            TODOUFUKEN_NAME_RYAKU,
            GYOUSHA_ADDRESS1,
            GYOUSHA_FURIGANA,
            GYOUSHA_POST,
            GYOUSHA_TEL,
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            GYOUSHA_NAME1,
            GYOUSHA_NAME2,
            GYOUSHA_ADDRESS2,
            TORIHIKISAKI_NAME1,
            TORIHIKISAKI_NAME2,
            TORIHIKISAKI_ADDRESS1,
            TORIHIKISAKI_ADDRESS2
        }

        /// <summary>
        /// DENSHI_JIGYOUSHAの表示項目
        /// </summary>
        private enum DENSHI_JIGYOUSHA_COLUMNS
        {
            EDI_MEMBER_ID,
            JIGYOUSHA_NAME,
            JIGYOUSHA_POST,
            TODOFUKEN_NAME,
            JIGYOUSHA_ADDRESS,
            JIGYOUSHA_TEL,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            JIGYOUSHA_ADDRESS1,
            JIGYOUSHA_ADDRESS2,
            JIGYOUSHA_ADDRESS3,
            JIGYOUSHA_ADDRESS4,
            GYOUSHA_NAME1,
            GYOUSHA_NAME2,
            GYOUSHA_ADDRESS1,
            GYOUSHA_ADDRESS2
        }

        /// <summary>
        /// M_NYUUKINSAKIの表示項目
        /// </summary>
        private enum NYUUKINSAKI_COLUMNS
        {
            NYUUKINSAKI_CD,
            NYUUKINSAKI_NAME_RYAKU,
            NYUUKINSAKI_FURIGANA,
            NYUUKINSAKI_POST,
            TODOUFUKEN_NAME_RYAKU,
            NYUUKINSAKI_ADDRESS1,
            NYUUKINSAKI_TEL
        }

        /// <summary>
        /// M_SYUKKINSAKIの表示項目
        /// </summary>
        private enum SYUKKINSAKI_COLUMNS
        {
            SYUKKINSAKI_CD,
            SYUKKINSAKI_NAME_RYAKU,
            SYUKKINSAKI_FURIGANA,
            SYUKKINSAKI_POST,
            TODOUFUKEN_NAME_RYAKU,
            SYUKKINSAKI_ADDRESS1,
            SYUKKINSAKI_TEL
        }

        /// <summary>
        /// M_HIKIAI_TORIHIKISAKIの表示項目
        /// </summary>
        private enum HIKIAI_TORIHIKISAKI_COLUMNS
        {
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            TORIHIKISAKI_FURIGANA,
            TORIHIKISAKI_POST,
            TODOUFUKEN_NAME_RYAKU,
            TORIHIKISAKI_ADDRESS1,
            TORIHIKISAKI_TEL,
            TORIHIKISAKI_HIKIAI_FLG
        }

        /// <summary>
        /// M_HIKIAI_GYOUSHAの表示項目
        /// </summary>
        private enum HIKIAI_GYOUSHA_COLUMNS
        {
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME_RYAKU,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GYOUSHA_FURIGANA,
            GYOUSHA_POST,
            TODOUFUKEN_NAME_RYAKU,
            GYOUSHA_ADDRESS1,
            GYOUSHA_TEL,
            GYOUSHA_HIKIAI_FLG
        }

        /// <summary>
        /// M_GENBAMEMO_BUNRUIの表示項目
        /// </summary>
        private enum GENBAMEMO_BUNRUI_COLUMNS
        {
            GENBAMEMO_BUNRUI_CD,
            GENBAMEMO_BUNRUI_NAME_RYAKU,
            GENBAMEMO_BUNRUI_FURIGANA
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal KensakuKyoutsuuPopupLogic(KensakuKyoutsuuPopupForm targetForm)
        {
            this.form = targetForm;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            //クリアボタン(F5)イベント生成
            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //並び替えボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.SortSetting);

            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.Close);
        }

        /// <summary>
        /// アンカーを設定して、フォームサイズの変更に自動対応
        /// </summary>
        private void CopeResize()
        {
            //リサイズ対応
            this.form.customDataGridView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.form.bt_func1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func11.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.form.bt_func12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            this.form.lb_hint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.form.customSortHeader1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                //ファンクションキー対応
                this.form.KeyPreview = true;
                //リサイズ対応
                this.CopeResize();

                //ボタンの初期化
                this.ButtonInit();
                // 画面タイトルやDaoを初期化
                this.DisplyInit();

                this.form.customDataGridView1.AllowUserToAddRows = false;

                this.form.customDataGridView1.AllowUserToResizeColumns = false; //行サイズは固定

                EventInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面のタイトルなどを初期化を行う
        /// </summary>
        private void DisplyInit()
        {
            // PopupGetMasterFieldプロパティから返却値を設定
            string[] popupGetMasterField = new string[] { };
            if (!string.IsNullOrEmpty(this.form.PopupGetMasterField))
            {
                string str = this.form.PopupGetMasterField.Replace(" ", "");
                str = str.Replace("　", "");
                if (!string.IsNullOrEmpty(str))
                {
                    popupGetMasterField = str.Split(',');
                }
            }

            //初期値()個別対応する場合は以下のswithで上書きすること
            this.form.CONDITION_ITEM.Text = "3";
            this.form.CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;

            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                    // TODO:SQLファイルのパス設定を忘れずに
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetTorihikisakiDataSql.sql";

                    //20250326
                    this.form.CONDITION_ITEM.Text = "7";

                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "都道府県", "住所", "フリガナ", "郵便番号", "電話番号", "非表示１", "非表示２", "非表示３" };
                    this.hideColumnNames = new string[] { "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "TORIHIKISAKI_ADDRESS2" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                case WINDOW_ID.T_SEIKYU_SHIME:
                    // TODO:SQLファイルのパス設定を忘れずに
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetTorihikisakiSeikyuuShimeDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "都道府県", "住所", "フリガナ", "郵便番号", "電話番号", "非表示１", "非表示２", "非表示３" };
                    this.hideColumnNames = new string[] { "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "TORIHIKISAKI_ADDRESS2" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                case WINDOW_ID.T_SHIHARAI_SHIME:
                    // TODO:SQLファイルのパス設定を忘れずに
                    this.dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "取引先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetTorihikisakiShiharaiShimeDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "都道府県", "住所", "フリガナ", "郵便番号", "電話番号", "非表示１", "非表示２", "非表示３" };
                    this.hideColumnNames = new string[] { "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "TORIHIKISAKI_ADDRESS2" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                case WINDOW_ID.M_GYOUSHA:
                case WINDOW_ID.M_GYOUSHA_ALL:
                    this.dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    this.form.lb_title.Text = "業者検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetGyoushaDataSql.sql";

                    //20250326
                    this.form.CONDITION_ITEM.Text = "7";

                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(GYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "業者CD", "業者名", "都道府県", "住所", "フリガナ", "郵便番号", "電話番号", "取引先CD", "取引先名", "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７" };
                    this.hideColumnNames = new string[] { "GYOUSHA_NAME1", "GYOUSHA_NAME2", "GYOUSHA_ADDRESS2", "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "TORIHIKISAKI_ADDRESS1", "TORIHIKISAKI_ADDRESS2" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                    this.form.lb_title.Text = "電子事業者検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetDenshiGyoushaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(DENSHI_JIGYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "加入者番号", "電子事業者名", "郵便番号", "都道府県", "住所", "電話番号", "業者CD", "業者名", "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８" };
                    this.hideColumnNames = new string[] { "JIGYOUSHA_ADDRESS1", "JIGYOUSHA_ADDRESS2", "JIGYOUSHA_ADDRESS3", "JIGYOUSHA_ADDRESS4", "GYOUSHA_NAME1", "GYOUSHA_NAME2", "GYOUSHA_ADDRESS1", "GYOUSHA_ADDRESS2" }; //画面へ戻せるように隠し

                    //フリガナがないため利用不可に
                    this.form.plBOIN.Enabled = false;
                    this.form.panel2.Enabled = false;

                    this.form.CONDITION2.Text = "事業者名";
                    this.form.CONDITION2.Tag = "事業者名が対象の場合チェックを付けてください";
                    this.form.CONDITION3.Enabled = false;
                    this.form.CONDITION_ITEM.Tag = "【1、2、4～7】のいずれかで入力してください";

                    this.form.CONDITION_ITEM.Text = "2"; //ふりがながないので事業者名を初期にする

                    this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;

                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    this.dao = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
                    this.form.lb_title.Text = "入金先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetNyuukinsakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(NYUUKINSAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "入金先CD", "入金先名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号" };
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    this.dao = DaoInitUtility.GetComponent<IM_SYUKKINSAKIDao>();
                    this.form.lb_title.Text = "出金先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetSyukkinsakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(SYUKKINSAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "出金先CD", "出金先名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号" };
                    break;

                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                    this.dao = DaoInitUtility.GetComponent<IM_HIKIAI_TORIHIKISAKIDao>();
                    this.form.lb_title.Text = "引合取引先検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetHikiaiTorihikisakiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(HIKIAI_TORIHIKISAKI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "都道府県", "住所", "フリガナ", "郵便番号", "電話番号", "非表示１", "非表示２", "非表示３" };
                    this.hideColumnNames = new string[] { "TORIHIKISAKI_HIKIAI_FLG" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                    this.dao = DaoInitUtility.GetComponent<IM_HIKIAI_GYOUSHADao>();
                    this.form.lb_title.Text = "引合業者検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetHikiaiGyoushaDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(HIKIAI_GYOUSHA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先CD", "取引先名", "業者CD", "業者名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号", "非表示１" };
                    this.hideColumnNames = new string[] { "GYOUSHA_HIKIAI_FLG" }; //画面へ戻せるように隠し
                    this.SettingDisplayKensakuJouken();//NvNhat #158998 #158999
                    break;

                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_GENBAMEMO_BUNRUI:
                    this.dao = DaoInitUtility.GetComponent<IM_GENBAMEMO_BUNRUIDao>();
                    this.form.lb_title.Text = "現場メモ分類検索";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopup.Sql.GetGenbamemoBunruiDataSql.sql";
                    // 現在はCDだけだが将来は複数返したいかもしれないので、
                    // 画面ごとに設定
                    this.bindColumnNames = Enum.GetNames(typeof(GENBAMEMO_BUNRUI_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "現場メモ分類CD", "現場メモ分類名", "フリガナ" };
                    break;

                default:
                    break;
            }

            //タイトルラベルの強制変更対応
            if (!string.IsNullOrEmpty(this.form.PopupTitleLabel))
            {
                this.form.lb_title.Text = this.form.PopupTitleLabel;
                ControlUtility.AdjustTitleSize(this.form.lb_title, this.TitleMaxWidth);
            }

            // Formタイトルの初期化
            this.form.Text = this.form.lb_title.Text;

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            for (int i = 0; i < bindColumnNames.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = bindColumnNames[i];
                column.Name = bindColumnNames[i];
                column.HeaderText = displayColumnNames[i];

                if (this.hideColumnNames.Contains(column.Name)) //非表示
                {
                    column.Visible = false;
                }

                this.form.customDataGridView1.Columns.Add(column);
            }

            //ダミーカラム EMPTY　：空文字 を画面反映したい場合に利用
            this.form.customDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "EMPTY",
                HeaderText = "EMPTY",
                Visible = false
            });

            //列リサイズ(ここでの処理の場合は、一度だけでは反映されず、2回呼ぶと反映された)
            ResizeColumns(this.form.customDataGridView1);
            ResizeColumns(this.form.customDataGridView1);
        }

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        #endregion

        #region イベント用処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件生成
                this.sqlTekiyou = string.Empty;
                this.SetSearchString();

                DataTable dt = new DataTable();
                // 基本的なスクリプトを取得
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
                {
                    using (var sqlStr = new StreamReader(resourceStream))
                    {
                        string sql = string.Empty;
                        if (this.form.WindowId == WINDOW_ID.T_SEIKYU_SHIME || this.form.WindowId == WINDOW_ID.T_SHIHARAI_SHIME)
                        {
                            if (string.IsNullOrEmpty(this.sqlTekiyou))
                            {
                                this.sqlTekiyou = " 1 = 0 ";
                            }
                            sql = sqlStr.ReadToEnd().Replace(Environment.NewLine, "").Replace("/*joinStr*/", joinStr).Replace("/*sqlWhere*/", this.whereStr).Replace("/*sqlTekiyou*/", sqlTekiyou);
                        }
                        else
                        {
                            sql = sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.joinStr + this.whereStr + this.orderStr;
                        }

                        //distinct対応 結合条件によっては列が増えるため
                        int idx = sql.IndexOf("SELECT ", StringComparison.InvariantCultureIgnoreCase);
                        int idxDis = sql.IndexOf("DISTINCT ", StringComparison.InvariantCultureIgnoreCase);

                        if (idx > -1 && idxDis < 0)
                        {
                            var newsql = sql.Substring(0, idx) + "SELECT DISTINCT " + sql.Substring(idx + 7);
                            sql = newsql; // ステップ実行用に変数を経由
                        }

                        dt = this.dao.GetDateForStringSql(sql);
                        sqlStr.Close();
                    }
                }

                // DataTable table = GetStringDataTable(dt);
                this.SearchResult = dt;

                // 頭文字絞込み
                this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                this.form.customSortHeader1.SortDataTable(this.SearchResult);
                this.form.customDataGridView1.DataSource = this.SearchResult;
                this.form.customDataGridView1.ReadOnly = true;
                ResizeColumns(this.form.customDataGridView1);

                //PT498 検索0件時のフォーカス移動不正
                //int returnCount = this.SearchResult.Rows == null ? 0 : 1;
                int returnCount = this.SearchResult == null ? 0 : this.SearchResult.Rows.Count;

                LogUtility.DebugMethodEnd(returnCount);
                return returnCount;
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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool InvokeInitialSort()
        {
            try
            {
                if (this.SearchResult != null)
                {
                    // 頭文字絞込み
                    this.SearchResult.DefaultView.RowFilter = string.Empty;
                    this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();
                    ResizeColumns(this.form.customDataGridView1);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InvokeInitialSort", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 選択データ決定処理
        /// </summary>
        internal bool ElementDecision()
        {
            try
            {
                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam = new List<PopupReturnParam>();
                for (int i = 0; i < this.returnParamNames.Length; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();

                    //列名に+をつけると結合する
                    var setDate = string.Concat(this.returnParamNames[i].Split('+').Select(x => x.Trim()).Select(x => (this.form.customDataGridView1.CurrentRow.Cells[x].Value ?? "").ToString()));

                    popupParam.Key = "Value";

                    popupParam.Value = setDate;

                    if (setParamList.ContainsKey(i))
                    {
                        setParam = setParamList[i];
                    }
                    else
                    {
                        setParam = new List<PopupReturnParam>();
                    }

                    setParam.Add(popupParam);

                    setParamList.Add(i, setParam);
                }

                this.form.ReturnParams = setParamList;
                this.form.Close();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal bool ImeControlCondition()
        {
            try
            {
                switch (this.form.CONDITION_ITEM.Text)
                {
                    case "1":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Disable;
                        break;

                    case "2":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "3":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                        break;

                    case "4":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "5":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "6":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Disable;
                        break;

                    case "7":
                        this.form.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    default:
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            // 初期化
            this.joinStr = string.Empty;
            this.whereStr = string.Empty;
            this.orderStr = string.Empty;

            // 検索画面が増えたら以下に設定を追加していく
            string tableName = string.Empty;
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                case WINDOW_ID.T_SEIKYU_SHIME:
                case WINDOW_ID.T_SHIHARAI_SHIME:
                    this.SearchInfo = new M_TORIHIKISAKI();
                    tableName = typeof(M_TORIHIKISAKI).Name;
                    break;

                case WINDOW_ID.M_GYOUSHA:
                case WINDOW_ID.M_GYOUSHA_ALL:
                    this.SearchInfo = new M_GYOUSHA();
                    tableName = typeof(M_GYOUSHA).Name;
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.SearchInfo = new M_DENSHI_JIGYOUSHA();
                    tableName = typeof(M_DENSHI_JIGYOUSHA).Name;
                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    this.SearchInfo = new M_NYUUKINSAKI();
                    tableName = typeof(M_NYUUKINSAKI).Name;
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    this.SearchInfo = new M_SYUKKINSAKI();
                    tableName = typeof(M_SYUKKINSAKI).Name;
                    break;

                case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                    this.SearchInfo = new M_HIKIAI_TORIHIKISAKI();
                    tableName = typeof(M_HIKIAI_TORIHIKISAKI).Name;
                    break;

                case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                    this.SearchInfo = new M_HIKIAI_GYOUSHA();
                    tableName = typeof(M_HIKIAI_GYOUSHA).Name;
                    break;

                case WINDOW_ID.M_GENBAMEMO_BUNRUI:
                    this.SearchInfo = new M_GENBAMEMO_BUNRUI();
                    tableName = typeof(M_GENBAMEMO_BUNRUI).Name;
                    break;

                default:
                    break;
            }

            //hack:SQLインジェクション対策必要
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    this.orderStr = " ORDER BY " + tableName + ".EDI_MEMBER_ID";
                    break;

                case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                    this.orderStr = " ORDER BY " + tableName + ".TORIHIKISAKI_CD";
                    break;

                case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                    this.orderStr = " ORDER BY " + tableName + ".GYOUSHA_CD";
                    break;

                default:
                    this.orderStr = " ORDER BY " + tableName + "." + tableName.Substring(2) + "_CD ";
                    break;
            }

            // カラム名を動的に指定するために必要
            var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                // シングルクォートは2つ重ねる
                var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.CONDITION_VALUE.Text);
                switch (this.form.CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".TORIHIKISAKI_CD LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".GYOUSHA_CD LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "2":
                        // 略称名
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%' "; //略名なし
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".TORIHIKISAKI_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".GYOUSHA_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;
                        }
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND 1=1 "; //フリガナ無
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".TORIHIKISAKI_FURIGANA LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".GYOUSHA_FURIGANA LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' "; //結合無
                                break;

                            default:
                                this.whereStr = " AND M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "5":
                        // 住所
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND (" + tableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND (" + tableName + ".TORIHIKISAKI_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".TORIHIKISAKI_ADDRESS2 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND (" + tableName + ".GYOUSHA_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".GYOUSHA_ADDRESS2 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;

                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "6":
                        // 電話
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND " + tableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".TORIHIKISAKI_TEL LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND " + tableName + ".GYOUSHA_TEL LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = " AND " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                                this.whereStr = " AND (" + tableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                                //フリガナ無
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%')";
                                break;

                            case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                                this.whereStr = " AND (" + tableName + ".TORIHIKISAKI_CD LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".TORIHIKISAKI_NAME_RYAKU LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".TORIHIKISAKI_FURIGANA LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".TORIHIKISAKI_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".TORIHIKISAKI_ADDRESS2 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".TORIHIKISAKI_TEL LIKE '%" + condition + "%')";
                                break;

                            case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                                this.whereStr = " AND (" + tableName + ".GYOUSHA_CD LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GYOUSHA_NAME_RYAKU LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GYOUSHA_FURIGANA LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".GYOUSHA_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".GYOUSHA_ADDRESS2 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GYOUSHA_TEL LIKE '%" + condition + "%')";
                                break;

                            default:
                                this.whereStr = " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%' ";

                                //20250326
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME1 LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME2 LIKE '%" + condition + "%' ";

                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%' ";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }

            //Add Query NvNhat #158998 #158999 BEGIN
            if (this._flagChangedJouken)
            {
                // チェックボックスからくる条件句
                if (this.form.HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.HYOUJI_JOUKEN_DELETED.Checked || this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    string queryJouken = "";
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        queryJouken += "  AND  (1 = 0";

                        // 適用
                        if (this.form.HYOUJI_JOUKEN_TEKIYOU.Checked)
                        {
                            queryJouken += " OR ((({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) or ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and {0}.TEKIYOU_END IS NULL) or ({0}.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) or ({0}.TEKIYOU_BEGIN IS NULL and {0}.TEKIYOU_END IS NULL)) and {0}.DELETE_FLG = 0)";
                        }

                        // 削除
                        if (this.form.HYOUJI_JOUKEN_DELETED.Checked)
                        {
                            queryJouken += " OR {0}.DELETE_FLG = 1";
                        }

                        // 適用外
                        if (this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                        {
                            queryJouken += " OR (({0}.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > {0}.TEKIYOU_END) and {0}.DELETE_FLG = 0)";
                        }
                        queryJouken += ")";
                    }
                    this.whereStr += string.Format(queryJouken.ToString(), tableName);
                }
                //Save Properties
                this.SettingProperties(false);
            }
            //NvNhat #158998 #158999 END

            if (this.form.WindowId.Equals(WINDOW_ID.M_GENBAMEMO_BUNRUI))
            {
                this.whereStr = " AND DELETE_FLG = 0 ";
            }

            this.whereStr = " WHERE 1 = 1 " + this.whereStr;

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                //Check if Delete, Tekiyou
                if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))//NvNhat #158998 #158999
                {
                    continue;
                }

                this.depthCnt = 1;
                existSearchParam = false;
                string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref existSearchParam);
                sb.Append(where);
                if (sb.Length > 0)
                {
                    if (!existSearchParam)
                    {
                        this.depthCnt--;
                    }
                    for (int i = 0; i < this.depthCnt; i++)
                    {
                        sb.Append(") ");
                    }
                }
            }

            this.whereStr += sb.ToString();
            if (this.form.WindowId == WINDOW_ID.T_SEIKYU_SHIME || this.form.WindowId == WINDOW_ID.T_SHIHARAI_SHIME)
            {
                this.sqlTekiyou = sb.ToString().Substring(4);
                this.whereStr = this.whereStr.Replace(sb.ToString(), "");
            }
            this.CreateJoinStr();
        }

        /// <summary>
        /// 画面から来た絞込み情報による条件句を生成
        /// </summary>
        /// <param name="dto">PopupSearchSendParamDto</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="existSearchParam">条件が生成されたかどうかのフラグ</param>
        /// <returns></returns>
        private string CreateWhereStrFromScreen(PopupSearchSendParamDto dto, string tableName, ref bool existSearchParam)
        {
            StringBuilder sb = new StringBuilder();

            bool subExistSearchParam = false;
            int thisDepth = this.depthCnt;

            // 括弧付きの条件対応
            if (dto.SubCondition != null && 0 < dto.SubCondition.Count)
            {
                this.depthCnt++;
                foreach (PopupSearchSendParamDto popupSearchSendParam in dto.SubCondition)
                {
                    //Check if Delete, Tekiyou
                    if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))//NvNhat #158998 #158999
                    {
                        continue;
                    }

                    string where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref subExistSearchParam);
                    sb.Append(where);
                }

                // 条件をまとめるため
                if (subExistSearchParam)
                {
                    for (int i = 0; i < thisDepth; i++)
                    {
                        sb.Append(") ");
                    }
                }
                else
                {
                    this.depthCnt--;
                }
            }

            if (dto.KeyName != null && dto.KeyName.Equals("TEKIYOU_BEGIN"))
            {
                string[] con = dto.Control.Split(',');
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, con);
                string tekiyouSql = string.Empty;
                if (this.form.WindowId == WINDOW_ID.T_SEIKYU_SHIME || this.form.WindowId == WINDOW_ID.T_SHIHARAI_SHIME)
                {
                    string controlText1 = PropertyUtility.GetTextOrValue(control[0]);
                    var ctr1_1 = control[0] as CustomDateTimePicker;
                    string controlText2 = "";
                    CustomDateTimePicker ctr2_1 = null;
                    if (con.Length == 2)
                    {
                        controlText2 = PropertyUtility.GetTextOrValue(control[1]);
                        ctr2_1 = control[1] as CustomDateTimePicker;
                    }
                    
                    if (ctr1_1 != null && ctr1_1.Value != null && ctr2_1 != null && ctr2_1.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou3, tableName, ctr1_1.Value, ctr2_1.Value);
                    }
                    else if (ctr1_1 != null && ctr1_1.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou1, tableName, ctr1_1.Value);
                    }
                    else
                    {
                        tekiyouSql = string.Format(tekiyou2, tableName);
                    }
                }
                else
                {
                    if (con.Length == 2)
                    {
                        string controlText1 = PropertyUtility.GetTextOrValue(control[0]);
                        var ctr1_1 = control[0] as CustomDateTimePicker;
                        string controlText2 = "";
                        controlText2 = PropertyUtility.GetTextOrValue(control[1]);
                        var ctr2_1 = control[1] as CustomDateTimePicker;

                        if (ctr1_1 != null && ctr1_1.Value != null && ctr2_1 != null && ctr2_1.Value != null)
                        {
                            tekiyouSql = string.Format(tekiyou3, tableName, ctr1_1.Value, ctr2_1.Value);
                        }
                        else if (ctr1_1 != null && ctr1_1.Value != null)
                        {
                            tekiyouSql = string.Format(tekiyou1, tableName, ctr1_1.Value);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou2, tableName);
                        }
                    }
                    else
                    {
                        string controlText = PropertyUtility.GetTextOrValue(control[0]);
                        var ctr = control[0] as CustomDateTimePicker;
                        var ctr2 = control[0] as DataGridViewTextBoxCell;
                        if (ctr != null && ctr.Value != null)
                        {
                            tekiyouSql = string.Format(tekiyou1, tableName, ctr.Value);
                        }
                        else if (ctr2 != null && ctr2.Value != null)
                        {
                            tekiyouSql = string.Format(tekiyou1, tableName, ctr2.Value);
                        }
                        else
                        {
                            tekiyouSql = string.Format(tekiyou2, tableName);
                        }
                    }
                }
                sb.Append(dto.And_Or.ToString());

                if (!existSearchParam)
                {
                    for (int i = 0; i < thisDepth; i++)
                    {
                        sb.Append(" (");
                    }
                }
                sb.Append(tekiyouSql);
                existSearchParam = true;

                return sb.ToString();
            }
            else if (dto.KeyName != null && dto.KeyName.Equals("TEKIYOU_FLG")
                && !string.IsNullOrEmpty(dto.Value))
            {
                string tekiyouSql = string.Empty;
                if ("TRUE".Equals(dto.Value.ToUpper()))
                {
                    tekiyouSql = string.Format(tekiyou2, tableName);
                }
                else if ("FALSE".Equals(dto.Value.ToUpper()))
                {
                    tekiyouSql = string.Format(" {0}.DELETE_FLG = 0 ", tableName);
                }

                sb.Append(dto.And_Or.ToString());

                if (!existSearchParam)
                {
                    for (int i = 0; i < thisDepth; i++)
                    {
                        sb.Append(" (");
                    }
                }
                sb.Append(tekiyouSql);
                existSearchParam = true;

                return sb.ToString();
            }

            // 通常のWHERE句を生成
            if (string.IsNullOrEmpty(dto.KeyName))
            {
                return sb.ToString();
            }

            // 絞込み条件にControlが指定されていればそれを使い、無ければValueを使用する
            // 両方無ければ条件句の生成はしない
            string whereValue = this.CreateWhere(dto);

            if (string.IsNullOrEmpty(whereValue))
            {
                return sb.ToString();
            }

            sb.Append(dto.And_Or.ToString());

            if (!existSearchParam)
            {
                for (int i = 0; i < thisDepth; i++)
                {
                    sb.Append(" (");
                }
            }

            if (dto.KeyName.Contains("."))
            {
                sb.Append(" (")
                  .Append(dto.KeyName)
                  .Append(" ")
                  .Append(whereValue)
                  .Append(") ");
            }
            else
            {
                sb.Append(" (")
                  .Append(tableName)
                  .Append(".")
                  .Append(dto.KeyName)
                  .Append(" ")
                  .Append(whereValue)
                  .Append(" ) ");
            }

            existSearchParam = true;

            return sb.ToString();
        }

        /// <summary>
        /// 頭文字条件の生成
        /// DataGridViewのフィルタ条件に使用すること
        /// </summary>
        /// <returns>フィルタ条件</returns>
        private String SetInitialSearchString()
        {
            string filterStr = string.Empty;

            // DBアクセスを発生させないためDataGridView用の条件を作成する

            if (string.IsNullOrEmpty(this.form.FILTER_SHIIN_VALUE.Text))
            {
                return string.Empty;
            }

            string furiganaCol = GetFuriganaColName();
            if (string.IsNullOrEmpty(this.form.FILTER_BOIN_VALUE.Text))
            {
                string filterInitialStr = string.Empty;
                // 母音(段)が選択されてなければ選択されている子音(行)のをすべてを表示
                switch (this.form.FILTER_SHIIN_VALUE.Text)
                {
                    case "1":
                        filterInitialStr = Constans.Agyou;
                        break;

                    case "2":
                        filterInitialStr = Constans.KAgyou;
                        break;

                    case "3":
                        filterInitialStr = Constans.SAgyou;
                        break;

                    case "4":
                        filterInitialStr = Constans.TAgyou;
                        break;

                    case "5":
                        filterInitialStr = Constans.NAgyou;
                        break;

                    case "6":
                        filterInitialStr = Constans.HAgyou;
                        break;

                    case "7":
                        filterInitialStr = Constans.MAgyou;
                        break;

                    case "8":
                        filterInitialStr = Constans.YAgyou;
                        break;

                    case "9":
                        filterInitialStr = Constans.RAgyou;
                        break;

                    case "10":
                        filterInitialStr = Constans.WAgyou;
                        break;

                    case "11":
                        filterInitialStr = Constans.alphanumeric;
                        break;

                    case "12":
                        // 以下、うまく動かない
                        filterInitialStr = string.Join(",", Constans.Agyou, Constans.KAgyou, Constans.SAgyou, Constans.TAgyou, Constans.NAgyou,
                            Constans.HAgyou, Constans.MAgyou, Constans.YAgyou, Constans.RAgyou, Constans.WAgyou, Constans.alphanumeric);
                        break;

                    default:
                        return string.Empty;
                }

                if ("12".Equals(this.form.FILTER_SHIIN_VALUE.Text))
                {
                    filterStr = string.Format("substring({0}, 1, 1) not in ({1})", furiganaCol, filterInitialStr);
                }
                else
                {
                    filterStr = string.Format("substring({0}, 1, 1) in ({1})", furiganaCol, filterInitialStr);
                }
            }
            else
            {
                int BOINIndex = -1;
                if (int.TryParse(this.form.FILTER_BOIN_VALUE.Text, out BOINIndex)
                    && BOINIndex <= this.form.BOINListFilter.Length)
                {
                    // 母音(段)があれば母音(段)で絞込み
                    //濁点等対応
                    //filterStr = furiganaCol + " LIKE '" + this.form.BOINList[BOINIndex - 1] + "%'";
                    filterStr = string.Format("substring({0}, 1, 1) in ({1})", furiganaCol, this.form.BOINListFilter[BOINIndex - 1]);
                }
            }

            return filterStr;
        }

        /// <summary>
        /// 該当テーブルからフリガナのカラム名を取得
        /// </summary>
        /// <returns></returns>
        private string GetFuriganaColName()
        {
            string colName = string.Empty;
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_TORIHIKISAKI:
                case WINDOW_ID.M_TORIHIKISAKI_ALL:
                case WINDOW_ID.T_SEIKYU_SHIME:
                case WINDOW_ID.T_SHIHARAI_SHIME:
                    colName = TORIHIKISAKI_COLUMNS.TORIHIKISAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GYOUSHA:
                case WINDOW_ID.M_GYOUSHA_ALL:
                    colName = GYOUSHA_COLUMNS.GYOUSHA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    colName = DENSHI_JIGYOUSHA_COLUMNS.JIGYOUSHA_NAME.ToString();
                    break;

                case WINDOW_ID.M_NYUUKINSAKI:
                    colName = NYUUKINSAKI_COLUMNS.NYUUKINSAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_SYUKKINSAKI:
                    colName = SYUKKINSAKI_COLUMNS.SYUKKINSAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU:
                    colName = HIKIAI_TORIHIKISAKI_COLUMNS.TORIHIKISAKI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU:
                    colName = HIKIAI_GYOUSHA_COLUMNS.GYOUSHA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GENBAMEMO_BUNRUI:
                    colName = GENBAMEMO_BUNRUI_COLUMNS.GENBAMEMO_BUNRUI_FURIGANA.ToString();
                    break;

                default:
                    break;
            }

            return colName;
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }

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
        /// PopupSearchSendParamDtoからWHERE句を作成します。
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="whereValue"></param>
        private string CreateWhere(PopupSearchSendParamDto dto)
        {
            CNNECTOR_SIGNS sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;  // KeyとValueをつなぐ符号
            string returnStr = string.Empty;

            if (dto == null)
            {
                return returnStr;
            }
            string sqlValue = string.Empty;
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    if (dto.Value.Contains(","))
                    {
                        sqlConnectorSign = CNNECTOR_SIGNS.IN;
                        // 使用側で"'"を意識しないで使わせたいので、FW側で"'"をつける
                        string[] valueList = dto.Value.Replace(" ", "").Split(',');
                        foreach (string tempValue in valueList)
                        {
                            // Where句の文字列に対してエスケープシーケンス対策を行う
                            sqlValue = SqlCreateUtility.CounterplanEscapeSequence(tempValue);
                            if (string.IsNullOrEmpty(returnStr))
                            {
                                returnStr = "'" + sqlValue + "'";
                            }
                            else
                            {
                                returnStr = returnStr + ", '" + sqlValue + "'";
                            }
                        }
                        returnStr = "(" + returnStr + ")";
                    }
                    else
                    {
                        sqlValue = SqlCreateUtility.CounterplanEscapeSequence(dto.Value);
                        sqlConnectorSign = CNNECTOR_SIGNS.EQUALS;
                        returnStr = "'" + sqlValue + "'";
                    }
                }
            }
            else
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                string controlText = PropertyUtility.GetTextOrValue(control[0]);
                if (control != null && !string.IsNullOrEmpty(controlText))
                {
                    sqlValue = SqlCreateUtility.CounterplanEscapeSequence(controlText);
                    // 複数同じ名前のコントロールは存在しないはず
                    returnStr = "'" + sqlValue + "'";
                }
            }

            if (!string.IsNullOrEmpty(returnStr))
            {
                return CNNECTOR_SIGNSExt.ToTypeString(sqlConnectorSign) + " " + returnStr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 有効レコードをチェックするSQLを作成します。
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <returns></returns>
        private static string CreateWhereStr(string tableName)
        {
            //todo:ポップアップ対象追加時修正箇所
            switch (tableName)
            {
                case "M_DENSHI_JIGYOUSHA": //エンティティに定義はあるが実テーブルには列がない。FW修正の影響回避のためここでブロックする
                    return " 1 = 1 ";
            }
            return String.Format("{0}.DELETE_FLG = 0", tableName);
        }

        /// <summary>
        /// popupWindowSettingの内容からJOIN句を作成します。
        /// </summary>
        private void CreateJoinStr()
        {
            var join = new StringBuilder();
            var where = new StringBuilder();
            var isChecked = new List<string>();
            foreach (JoinMethodDto joinData in this.form.popupWindowSetting)
            {
                if (joinData.Join != JOIN_METHOD.WHERE)
                {
                    if (!string.IsNullOrEmpty(joinData.LeftTable) && !string.IsNullOrEmpty(joinData.LeftKeyColumn) &&
                        !string.IsNullOrEmpty(joinData.RightTable) && !string.IsNullOrEmpty(joinData.RightKeyColumn))
                    {
                        join.Append(" " + JOIN_METHODExt.ToString(joinData.Join) + " ");
                        join.Append(joinData.LeftTable + " ON ");
                        join.Append(joinData.LeftTable + "." + joinData.LeftKeyColumn + " = ");
                        join.Append(joinData.RightTable + "." + joinData.RightKeyColumn + " ");
                    }
                }
                else if (joinData.Join == JOIN_METHOD.WHERE)
                {
                    var searchStr = new StringBuilder();
                    foreach (var searchData in joinData.SearchCondition)
                    {
                        //Check if Delete, Tekiyou
                        if (this.CheckParamsIsValid(searchData.LeftColumn))//NvNhat #158998 #158999
                        {
                            continue;
                        }
                        //検索条件設定
                        if (string.IsNullOrEmpty(searchData.Value))
                        {
                            //value値がnullのため、テーブル同士のカラム結合を行う
                            if (searchStr.Length == 0)
                            {
                                searchStr.Append(" AND (");
                            }
                            else
                            {
                                searchStr.Append(" ");
                                searchStr.Append(searchData.And_Or.ToString());
                                searchStr.Append(" ");
                            }
                            searchStr.Append(joinData.LeftTable);
                            searchStr.Append(".");
                            searchStr.Append(searchData.LeftColumn);
                            var data = joinData.RightTable + "." + searchData.RightColumn;
                            searchStr.Append(searchData.Condition.ToConditionString(data));
                        }
                        else
                        {
                            // コントロールの値が有効な場合WHERE句を作成する
                            var data = createValues(this.form.ParentControls, searchData);

                            if (!string.IsNullOrEmpty(data))
                            {
                                if (searchStr.Length == 0)
                                {
                                    searchStr.Append(" AND (");
                                }
                                else
                                {
                                    searchStr.Append(" ");
                                    searchStr.Append(searchData.And_Or.ToString());
                                    searchStr.Append(" ");
                                }
                                searchStr.Append(joinData.LeftTable);
                                searchStr.Append(".");
                                searchStr.Append(searchData.LeftColumn);
                                searchStr.Append(searchData.Condition.ToConditionString(data));
                                searchStr.Append(" ");
                            }
                        }
                    }
                    if (searchStr.Length > 0)
                    {
                        // 閉じる
                        searchStr.Append(") ");
                    }
                    where.Append(searchStr);
                }
                // 有効レコードをチェックする
                if (joinData.IsCheckLeftTable == true && !isChecked.Contains(joinData.LeftTable))
                {
                    var type = Type.GetType("r_framework.Entity." + joinData.LeftTable);
                    if (type != null)
                    {
                        var pNames = type.GetProperties().Select(p => p.Name);
                        if (pNames.Contains("TEKIYOU_BEGIN") && pNames.Contains("TEKIYOU_END") && pNames.Contains("DELETE_FLG"))
                        {
                            where.Append(" AND ");
                            where.Append(CreateWhereStr(joinData.LeftTable));
                            where.Append(" ");
                        }
                    }
                    isChecked.Add(joinData.LeftTable);
                }
            }
            this.joinStr += join.ToString();
            this.whereStr += where.ToString();
        }

        /// <summary>
        /// 検索条件を作成する
        /// 対象のコントロールが見つけれた場合については、コントロールの値とする
        /// コントロールが見つからない場合は、Valuesの値を直接設定する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static string createValues(object[] controls, SearchConditionsDto dto)
        {
            var field = ControlUtility.CreateFields(controls, dto.Value);

            if (field[0] != null)
            {
                var control = field[0] as ICustomControl;

                if (control != null)
                {
                    return dto.ValueColumnType.ToConvertString(control.GetResultText());
                }
                throw new Exception();
            }
            return dto.ValueColumnType.ToConvertString(dto.Value.ToString());
        }

        /// <summary>
        /// 一部の列で自動調整がうまくいかないので 補正つきリサイズ
        /// </summary>
        /// <param name="dgv"></param>
        static public void ResizeColumns(DataGridView dgv)
        {
            //自動整列
            //dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //自動調整を解除しつつ、すべての横幅を1ドットプラス
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                c.Width += 1;
            }
        }

        /// <summary>
        /// Setting Display for Torihikisaki,Gyousha
        /// NvNhat #158998 #158999 BEGIN
        /// </summary>
        private void SettingDisplayKensakuJouken()
        {
            #region Setting Layout
            Control itemChild = this.form.ParentControls[0] as Control;
            if (itemChild == null)
            {
                _flagChangedJouken = false;
                return;
            }
            BaseBaseForm parentForm = itemChild.Parent as BaseBaseForm;
            if (allowForms.Any(x=>itemChild.Parent != null && itemChild.Parent.Text.Contains(x)))
            {
                _flagChangedJouken = true;
            }
            else if(parentForm == null)
            {
                _flagChangedJouken = false;
                return;
            }    
            else if (allowForms.Any(x => parentForm.Text.Contains(x)))
            {
                _flagChangedJouken = true;
            }

            if (!_flagChangedJouken)
            {
                var ichiranForm = parentForm.inForm as Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm;
                if (ichiranForm == null)
                {
                    _flagChangedJouken = false;
                    return;
                }

                if (ichiranForm.DenshuKbn.Equals(DENSHU_KBN.NONE))
                {
                    _flagChangedJouken = false;
                    return;
                }
                _flagChangedJouken = true;
            }

            this.form.label3 = new System.Windows.Forms.Label();
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.form.HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.form.HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            // 
            // label3
            // 
            this.form.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.label3.ForeColor = System.Drawing.Color.White;
            this.form.label3.Location = new System.Drawing.Point(14, 137);
            this.form.label3.Name = "label3";
            this.form.label3.Size = new System.Drawing.Size(108, 22);
            this.form.label3.TabIndex = 515;
            this.form.label3.Text = "表示条件";
            this.form.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.form.label3.Visible = true;
            
            // 
            // HYOUJI_JOUKEN_TEKIYOU
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(130, 141);
            this.form.HYOUJI_JOUKEN_TEKIYOU.Name = "HYOUJI_JOUKEN_TEKIYOU";
            this.form.HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(68, 17);
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabIndex = 5;
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.form.HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // HYOUJI_JOUKEN_DELETED
            // 
            this.form.HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.form.HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(196, 141);
            this.form.HYOUJI_JOUKEN_DELETED.Name = "HYOUJI_JOUKEN_DELETED";
            this.form.HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(54, 17);
            this.form.HYOUJI_JOUKEN_DELETED.TabIndex = 6;
            this.form.HYOUJI_JOUKEN_DELETED.TabStop = false;
            this.form.HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.form.HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_DELETED.Visible = true;
            this.form.HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(250, 141);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Name = "HYOUJI_JOUKEN_TEKIYOUGAI";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(96, 17);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 7;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);

            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_DELETED);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOUGAI);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOU);
            this.form.Controls.Add(this.form.label3);
            
            #endregion

            this.form.label1.Location = new System.Drawing.Point(14, 85);
            this.form.panel2.Location = new System.Drawing.Point(191, 86);

            this.form.label2.Location = new System.Drawing.Point(14, 111);
            this.form.plBOIN.Location = new System.Drawing.Point(191, 112);

            this.SettingProperties(true);
        }
        /// <summary>
        /// Load And Save setting
        /// </summary>
        /// <param name="isLoad"></param>
        public void SettingProperties(bool isLoad = false)
        {
            try
            {
                if (this._flagChangedJouken)
                {
                    if (isLoad)
                    {
                        this.form.HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOU;
                        this.form.HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_DELETED;
                        this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOUGAI;
                    }
                    else
                    {
                        //Save Properties
                        Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOU = this.form.HYOUJI_JOUKEN_TEKIYOU.Checked;
                        Properties.Settings.Default.HYOUJI_JOUKEN_DELETED = this.form.HYOUJI_JOUKEN_DELETED.Checked;
                        Properties.Settings.Default.HYOUJI_JOUKEN_TEKIYOUGAI = this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch(Exception ex)
            {
                LogUtility.Error(ex);
            }    
        }
        /// <summary>
        /// Check required one Checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var CheckCurrent = sender as CheckBox;
                if (!CheckCurrent.Checked)
                {
                    if (!this.form.HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.HYOUJI_JOUKEN_DELETED.Checked && !this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        this.form.errmessage.MessageBoxShow("E001", "表示条件");
                        CheckCurrent.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }
        /// <summary>
        /// Check value will skip
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool CheckParamsIsValid(string param)
        {
            try
            {
                bool isContinue = false;
                if (this._flagChangedJouken && param != null &&
                   (param.Equals("TEKIYOU_BEGIN") || param.Equals("TEKIYOU_FLG")|| param.Equals("DELETE_FLG") || param.Contains("DELETE_FLG")))
                {
                    isContinue = true;
                }
                return isContinue;
            }
            catch(Exception ex)
            {
                LogUtility.Error(ex);
                return false;
            }
        }
        //NvNhat #158998 #158999 END
        #endregion
    }
}