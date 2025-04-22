// $Id: KensakuKyoutsuuPopupForMultiKeyLogic.cs 55781 2015-07-15 08:58:53Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KensakuKyoutsuuPopupForMultiKey.APP;
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
using KensakuKyoutsuuPopupForMultiKey.DTO;
using System.Data.SqlTypes;
using KensakuKyoutsuuPopupForMultiKey.DAO;
using r_framework.APP.Base;

using System.ComponentModel;

namespace KensakuKyoutsuuPopupForMultiKey.Logic
{
    /// <summary>
    /// 複数キー用検索共通ポップアップロジック
    /// </summary>
    public class KensakuKyoutsuuPopupForMultiKeyLogic
    {
        #region フィールド

        /// <summary>
        /// バインドするカラム名一覧
        /// </summary>
        internal string[] bindColumnNames = new string[] { "" };

        /// <summary>
        /// 表示カラム名
        /// </summary>
        internal string[] displayColumnNames = new string[] { };

        /// <summary>
        /// 隠し列
        /// </summary>
        internal string[] hideColumnNames = new string[] { };

        /// <summary>
        /// 表示Tag
        /// </summary>
        internal string[] displayTags = new string[] { };

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "KensakuKyoutsuuPopupForMultiKey.Setting.ButtonSetting.xml";

        private static readonly string tekiyou1 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, '{1}', 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, '{1}', 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0 ";
        private static readonly string tekiyou2 = " AND (({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) AND {0}.TEKIYOU_END IS NULL) OR ({0}.TEKIYOU_BEGIN IS NULL AND CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= {0}.TEKIYOU_END) OR ({0}.TEKIYOU_BEGIN IS NULL AND {0}.TEKIYOU_END IS NULL)) AND {0}.DELETE_FLG = 0 ";

        /// <summary>
        /// 共通一覧画面のForm
        /// </summary>
        private KensakuKyoutsuuPopupForMultiKeyForm form;

        /// <summary>
        /// 共通一覧画面にて利用されるDao
        /// </summary>
        private IS2Dao dao;

        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        //20250325
        r_framework.Dao.IM_SHURUIDao shuruiDao;

        /// <summary>
        /// M_SYS_INFO
        /// </summary>
        internal M_SYS_INFO sysInfoEntity;

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

        private bool ParentFilterDispFlag = true;

        /// <summary>
        /// Flag changed layout for Genba
        /// NvNhat #160897
        /// </summary>
        public bool _flagChangedJouken = false;
        public string[] allowForms = new string[] { 
            "委託契約書許可証期限管理", "伝票一括更新", "伝票明細一括更新", "顧客カルテ", "マニフェスト終了日一括更新",
            "設置コンテナ一覧", "検収一覧表", "売掛金一覧表", "入金予定一覧", "未入金一覧表", "買掛金一覧","混合廃棄物状況一覧","伝票連携状況一覧"
        };

        /// <summary>
        /// 絞り込み条件で使用する符号
        /// </summary>
        public enum CNNECTOR_SIGNS
        {
            EQUALS = 0,
            IN = 1
        }

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
        /// M_BANK_SHITENの表示項目
        /// </summary>
        private enum BANK_SHITEN_COLUMNS
        {
            BANK_CD,
            BANK_NAME_RYAKU,
            BANK_SHITEN_CD,
            BANK_SHIETN_NAME_RYAKU,
            BANK_SHITEN_FURIGANA,
            KOUZA_SHURUI,
            KOUZA_NO,
            KOUZA_NAME
        }

        /// <summary>
        /// M_GENBAの表示項目
        /// </summary>
        private enum GENBA_COLUMNS
        {
            //20250327
            TORIHIKISAKI_NAME_RYAKU,
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GENBA_CD,
            GENBA_NAME_RYAKU,
            TODOUFUKEN_NAME_RYAKU,
            GENBA_ADDRESS1,
            GENBA_FURIGANA,
            GENBA_POST,
            GENBA_TEL,

            GENBA_NAME1,//以下非表示
            GENBA_NAME2,
            GENBA_ADDRESS2,
            GYOUSHA_NAME1,
            GYOUSHA_NAME2,
            GYOUSHA_ADDRESS1,
            GYOUSHA_ADDRESS2,
            TORIHIKISAKI_CD,
            TORIHIKISAKI_NAME1,
            TORIHIKISAKI_NAME2,
            TORIHIKISAKI_ADDRESS1,
            TORIHIKISAKI_ADDRESS2,
            GYOUSHA_TODOUFUKEN_NAME_RYAKU,
            GENBA_TODOUFUKEN_NAME_RYAKU
        }

        /// <summary>
        /// M_DENSHI_JIGYOUJOUの表示項目
        /// </summary>
        private enum DENSHI_JIGYOUJOU_COLUMNS
        {
            EDI_MEMBER_ID,
            JIGYOUSHA_NAME,
            JIGYOUJOU_CD,
            JIGYOUJOU_NAME,
            JIGYOUJOU_POST,
            TODOFUKEN_NAME,
            JIGYOUJOU_ADDRESS,
            JIGYOUJOU_TEL,

            JIGYOUSHA_POST, //以下非表示
            JIGYOUSHA_TEL,
            JIGYOUSHA_ADDRESS1,
            JIGYOUSHA_ADDRESS2,
            JIGYOUSHA_ADDRESS3,
            JIGYOUSHA_ADDRESS4,
            JIGYOUJOU_ADDRESS1,
            JIGYOUJOU_ADDRESS2,
            JIGYOUJOU_ADDRESS3,
            JIGYOUJOU_ADDRESS4,
        }

        /// <summary>
        /// M_HINMEIの表示項目
        /// </summary>
        private enum HINMEI_COLUMNS
        {
            //20250326
            BUNRUI_CD,
            BUNRUI_NAME_RYAKU,
            SHURUI_CD,
            SHURUI_NAME_RYAKU,
            SHURUI_FURIGANA,
            HINMEI_CD,
            HINMEI_NAME_RYAKU,

            HINMEI_FURIGANA

                // 20140718 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　start
            , HINMEI_NAME

            // 20140718 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　end
        }

        /// <summary>
        /// M_HINMEIの表示項目
        /// </summary>
        private enum HINMEI_COLUMNS_FOR_MULTISELECT
        {
            SELECT_CHECK,
            SHURUI_CD,
            SHURUI_NAME_RYAKU,
            SHURUI_FURIGANA,
            HINMEI_CD,
            HINMEI_NAME_RYAKU,
            HINMEI_FURIGANA
        }

        /// <summary>
        /// M_HINMEIの表示項目
        /// </summary>
        private enum HINMEI_COLUMNS_FOR_DENPYOU
        {
            DENPYOU_KBN_NAME,
            //20250325
            BUNRUI_CD,
            BUNRUI_NAME_RYAKU,
            SHURUI_CD,
            SHURUI_NAME_RYAKU,
            SHURUI_FURIGANA,
            HINMEI_CD,
            HINMEI_NAME_RYAKU,
            HINMEI_FURIGANA,
            UNIT_NAME,
            TANKA,
            HINMEI_NAME
        }

        /// <summary>
        /// M_HIKIAI_GENBAの表示項目
        /// </summary>
        private enum HIKIAI_GENBA_COLUMNS
        {
            GYOUSHA_CD,
            GYOUSHA_NAME_RYAKU,
            GENBA_CD,
            GENBA_NAME_RYAKU,
            GENBA_FURIGANA,
            GENBA_POST,
            TODOUFUKEN_NAME_RYAKU,
            GENBA_ADDRESS1,
            GENBA_TEL,

            // 以下非表示
            GYOUSHA_HIKIAI_FLG,

            GENBA_HIKIAI_FLG,
            GYOUSHA_TODOUFUKEN_NAME_RYAKU,
            GENBA_TODOUFUKEN_NAME_RYAKU
        }

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal KensakuKyoutsuuPopupForMultiKeyLogic(KensakuKyoutsuuPopupForMultiKeyForm targetForm)
        {
            this.form = targetForm;
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();

            //20250325
            this.shuruiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHURUIDao>();

            this.sysInfoEntity = new M_SYS_INFO();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            //品名ﾏｽﾀ/個別単価ボタン(F1)イベント生成
            this.form.bt_func1.Click += new EventHandler(this.form.BetsuSearch);

            //クリアボタン(F5)イベント生成
            this.form.bt_func5.Click += new EventHandler(this.form.Clear);

            //検索ボタン(F8)イベント生成
            this.form.bt_func8.Click += new EventHandler(this.form.Search);

            //確定ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

            //ソートボタン(F10)イベント生成
            this.form.bt_func10.Click += new EventHandler(this.form.MoveToSort);

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

                // SYS_INFOを取得する
                this.sysInfoEntity = this.GetSysInfo();

                //ボタンの初期化
                this.ButtonInit();
                // 画面タイトルやDaoを初期化
                this.DisplyInit();

                this.form.customDataGridView1.AllowUserToResizeColumns = false; //行サイズは固定

                // 条件によってPARENT_CONDITION_ITEMの初期かもするので初期化の一番最後に実行
                this.ChangeDisplayFilter();
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
            this.form.PARENT_CONDITION_ITEM.Text = "3";
            this.form.PARENT_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.CHILD_CONDITION_ITEM.Text = "3";
            this.form.CHILD_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.form.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
            this.form.customDataGridView1.AllowUserToAddRows = false;

            string parentLabel = string.Empty;
            string childLabel = string.Empty;
            string hintTextConditon = string.Empty;
            bool setCheckBoxFirst = false;

            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // 画面IDごとに生成を行うDaoを変更する
                case WINDOW_ID.M_BANK_SHITEN:
                    this.dao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                    parentLabel = "銀行";
                    childLabel = "銀行支店";
                    hintTextConditon = "1～3,7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetBankShitenDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(BANK_SHITEN_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "銀行CD", "銀行名", "銀行支店CD", "銀行支店名", "銀行支店ﾌﾘｶﾞﾅ", "口座種類", "口座番号", "口座名義" };

                    // レイアウト調整
                    this.form.PARENT_CONDITION4.Visible = false;
                    this.form.PARENT_CONDITION5.Visible = false;
                    this.form.PARENT_CONDITION6.Visible = false;
                    this.form.CHILD_CONDITION4.Visible = false;
                    this.form.CHILD_CONDITION5.Visible = false;
                    this.form.CHILD_CONDITION6.Visible = false;

                    this.form.PARENT_CONDITION7.Location = this.form.PARENT_CONDITION4.Location;
                    this.form.PARENT_CONDITION_VALUE.Left = this.form.PARENT_CONDITION7.Right;
                    this.form.CHILD_CONDITION7.Location = this.form.CHILD_CONDITION4.Location;
                    this.form.CHILD_CONDITION_VALUE.Left = this.form.CHILD_CONDITION7.Right;

                    break;

                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                case WINDOW_ID.M_GENBA_ALL:
                    this.ChangeDisplayGenba();// NvNhat #160897
                    this.dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    parentLabel = "業者";
                    childLabel = "現場";
                    hintTextConditon = "1～7";

                    //20250327
                    this.form.PARENT_CONDITION_ITEM.Text = "7";
                    this.form.CHILD_CONDITION_ITEM.Text = "7";

                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetGenbaDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(GENBA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "取引先名", "業者CD", "業者名", "現場CD", "現場名", "都道府県", "現場住所", "現場ﾌﾘｶﾞﾅ", "郵便番号", "現場電話番号",
                                                            "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８", "非表示９", "非表示１０",
                                                            "非表示１１", "非表示１２", "非表示１３", "非表示１４" };

                    this.displayTags = new string[] { "業者CDが表示されます", "業者名が表示されます", "現場CDが表示されます",
                        "現場名が表示されます",	"現場フリガナが表示されます", "郵便番号が表示されます",
                        "都道府県が表示されます", "現場住所が表示されます", "現場電話番号が表示されます" ,"非表示１","非表示２","非表示３","非表示４","非表示５","非表示６","非表示７","非表示８","非表示９","非表示１０",
                        "非表示１１", "非表示１２", "非表示１３", "非表示１４", "非表示１５" };

                    this.hideColumnNames = new string[] { "GENBA_NAME1", "GENBA_NAME2", "GENBA_ADDRESS2", "GYOUSHA_NAME1", "GYOUSHA_NAME2", "GYOUSHA_ADDRESS1", "GYOUSHA_ADDRESS2",
                        "TORIHIKISAKI_CD", "TOHIRISAKI_NAME_RYAKU", "TORIHIKISAKI_NAME1", "TORIHIKISAKI_NAME2", "TORIHIKISAKI_ADDRESS1", "TORIHIKISAKI_ADDRESS2" ,
                        "GYOUSHA_TODOUFUKEN_NAME_RYAKU","GENBA_TODOUFUKEN_NAME_RYAKU"}; //画面へ戻せるように隠し
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                    parentLabel = "電事業者";
                    childLabel = "電子事業場";
                    hintTextConditon = "1、2、3～7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetDenshiGenbaDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(DENSHI_JIGYOUJOU_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "加入者番号", "事業者名", "事業場CD", "事業場名", "郵便番号", "都道府県", "事業場住所", "事業場電話番号", "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８", "非表示９", "非表示１０" };
                    this.displayTags = new string[] { "加入者番号が表示されます", "事業名が表示されます", "事業場CDが表示されます",
                        "事業場名が表示されます", "郵便番号が表示されます",
                        "都道府県が表示されます", "事業場住所が表示されます", "事業場電話番号が表示されます" , "非表示１", "非表示２", "非表示３", "非表示４", "非表示５", "非表示６", "非表示７", "非表示８", "非表示９", "非表示１０"  };

                    this.hideColumnNames = new string[] { "JIGYOUSHA_POST", "JIGYOUSHA_TEL", "JIGYOUSHA_ADDRESS1", "JIGYOUSHA_ADDRESS2", "JIGYOUSHA_ADDRESS3", "JIGYOUSHA_ADDRESS4", "JIGYOUJOU_ADDRESS1", "JIGYOUJOU_ADDRESS2", "JIGYOUJOU_ADDRESS3", "JIGYOUJOU_ADDRESS4" }; //画面へ戻せるように隠し

                    // レイアウト調整

                    //フリガナ利用不可
                    this.form.panel3.Enabled = false;
                    this.form.plBOIN.Enabled = false;

                    this.form.PARENT_CONDITION3.Enabled = false;
                    this.form.CHILD_CONDITION3.Enabled = false;

                    this.form.PARENT_CONDITION2.Text = "名称";
                    this.form.CHILD_CONDITION2.Text = "名称";

                    this.form.PARENT_CONDITION_ITEM.Text = "2"; //ふりがながないので名前を初期にする
                    this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;

                    this.form.CHILD_CONDITION_ITEM.Text = "2"; //ふりがながないので名前を初期にする
                    this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                    break;

                case WINDOW_ID.M_HINMEI:
                case WINDOW_ID.M_HINMEI_SEARCH:
                    this.dao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
                    parentLabel = "種類";
                    childLabel = "品名";
                    hintTextConditon = "1～3,7";
                    if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
                    {
                        this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataForDenpyouSql.sql";
                        this.bindColumnNames = Enum.GetNames(typeof(HINMEI_COLUMNS_FOR_DENPYOU));

                        //20250326
                        this.displayColumnNames = new string[] { "伝票", "分類CD", "分類名", "種類CD", "種類名", "種類名ﾌﾘｶﾞﾅ", "品名CD", "品名", "品名ﾌﾘｶﾞﾅ", "単位", "単価"};
                        // 画面レイアウト変更
                        this.ChangeLayout();

                        //20250325
                        this.GetListCbbShuruiName();

                        // 呼び出し元（受入入力、出荷入力、売上/支払入力）画面から取得
                        object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "TORIHIKISAKI_CD" });
                        if (control != null)
                        {
                            this.form.TORIHIKISAKI_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "TORIHIKISAKI_NAME_RYAKU", "TORIHIKISAKI_NAME" });
                        if (control != null)
                        {
                            if (control[0] != null)
                            {
                                this.form.TORIHIKISAKI_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                            }
                            else if(control[1] != null)
                            {
                                // 受付入力向け対応(収集、出荷、持込)
                                this.form.TORIHIKISAKI_NAME.Text = PropertyUtility.GetTextOrValue(control[1]);
                            }
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "GYOUSHA_CD" });
                        if (control != null)
                        {
                            this.form.GYOUSHA_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "GYOUSHA_NAME_RYAKU", "GYOUSHA_NAME" });
                        if (control != null)
                        {
                            if (control[0] != null)
                            {
                                this.form.GYOUSHA_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                            }
                            else if (control[1] != null)
                            {
                                // 受付入力向け対応(収集、出荷、持込)
                                this.form.GYOUSHA_NAME.Text = PropertyUtility.GetTextOrValue(control[1]);
                            }
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "GENBA_CD" });
                        if (control != null)
                        {
                            this.form.GENBA_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "GENBA_NAME_RYAKU", "GENBA_NAME" });
                        if (control != null)
                        {
                            if (control[0] != null)
                            {
                                this.form.GENBA_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                            }
                            else if (control[1] != null)
                            {
                                // 受付入力向け対応(収集、出荷、持込)
                                this.form.GENBA_NAME.Text = PropertyUtility.GetTextOrValue(control[1]);
                            }
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "NIOROSHI_GYOUSHA_CD" });
                        if (control != null)
                        {
                            this.form.NIOROSHI_GYOUSHA_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "NIOROSHI_GYOUSHA_NAME" });
                        if (control != null)
                        {
                            this.form.NIOROSHI_GYOUSHA_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "NIOROSHI_GENBA_CD" });
                        if (control != null)
                        {
                            this.form.NIOROSHI_GENBA_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "NIOROSHI_GENBA_NAME" });
                        if (control != null)
                        {
                            this.form.NIOROSHI_GENBA_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "UNPAN_GYOUSHA_CD" });
                        if (control != null)
                        {
                            this.form.UNPAN_GYOUSHA_CD.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "UNPAN_GYOUSHA_NAME" });
                        if (control != null)
                        {
                            this.form.UNPAN_GYOUSHA_NAME.Text = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "DENSHU_KBN_CD" });
                        if (control != null)
                        {
                            this.form.DENSHU_KBN_CD = PropertyUtility.GetTextOrValue(control[0]);
                        }
                        control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { "DENPYOU_DATE", "UKETSUKE_DATE" });
                        if (control != null)
                        {
                            string strDenpyouDate = string.Empty;
                            if (control[0] != null)
                            {
                                strDenpyouDate = PropertyUtility.GetTextOrValue(control[0]);
                            }
                            else if (control[1] != null)
                            {
                                // 受付入力向け対応(収集、出荷、持込)
                                strDenpyouDate = PropertyUtility.GetTextOrValue(control[1]);
                            }

                            if (!string.IsNullOrEmpty(strDenpyouDate))
                            {
                                DateTime denpyouDate;
                                if (DateTime.TryParse(strDenpyouDate, out denpyouDate))
                                {
                                    this.form.DENPYOU_DATE = denpyouDate;
                                }
                                else
                                {
                                    this.form.DENPYOU_DATE = DateTime.Now;
                                }
                            }
                            else
                            {
                                this.form.DENPYOU_DATE = DateTime.Now;
                            }
                        }

                        // コントロール制御
                        this.form.isKobetsuHinmeiTankSearchFlg = this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value == 1;
                        //this.form.isKobetsuHinmeiTankSearchFlg = this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value;

                        InitControlForHinmeiSearch(this.form.isKobetsuHinmeiTankSearchFlg);
                    }
                    else
                    {
                        this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataSql.sql";
                        this.bindColumnNames = Enum.GetNames(typeof(HINMEI_COLUMNS));

                        //20250326
                        this.displayColumnNames = new string[] { "分類CD", "分類名", "種類CD", "種類名", "種類名ﾌﾘｶﾞﾅ", "品名CD", "品名", "品名ﾌﾘｶﾞﾅ" };

                        this.ChangeLayoutForHimei();
                        this.GetListCbbShuruiName();

                        if (this.form.PopupMultiSelect)
                        {
                            setCheckBoxFirst = true;
                            this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataForMultiSelectSql.sql";
                            this.bindColumnNames = Enum.GetNames(typeof(HINMEI_COLUMNS_FOR_MULTISELECT));
                            this.displayColumnNames = new string[] { string.Empty, "種類CD", "種類名", "種類名ﾌﾘｶﾞﾅ", "品名CD", "品名", "品名ﾌﾘｶﾞﾅ" };
                        }
                    }
                    this.returnParamNames = popupGetMasterField;

                    // レイアウト調整
                    this.form.PARENT_CONDITION4.Visible = false;
                    this.form.PARENT_CONDITION5.Visible = false;
                    this.form.PARENT_CONDITION6.Visible = false;
                    this.form.CHILD_CONDITION4.Visible = false;
                    this.form.CHILD_CONDITION5.Visible = false;
                    this.form.CHILD_CONDITION6.Visible = false;

                    this.form.PARENT_CONDITION7.Location = this.form.PARENT_CONDITION4.Location;
                    this.form.PARENT_CONDITION_VALUE.Left = this.form.PARENT_CONDITION7.Right;
                    this.form.CHILD_CONDITION7.Location = this.form.CHILD_CONDITION4.Location;
                    this.form.CHILD_CONDITION_VALUE.Left = this.form.CHILD_CONDITION7.Right;
                    break;

                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                    this.ChangeDisplayGenba();// NvNhat #160897
                    this.dao = DaoInitUtility.GetComponent<IM_HIKIAI_GENBADao>();
                    parentLabel = "業者";
                    childLabel = "現場";
                    hintTextConditon = "1～7";
                    this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHikiaiGenbaDataSql.sql";
                    this.bindColumnNames = Enum.GetNames(typeof(HIKIAI_GENBA_COLUMNS));
                    this.returnParamNames = popupGetMasterField;
                    this.displayColumnNames = new string[] { "業者CD", "業者名", "現場CD", "現場名", "フリガナ", "郵便番号", "都道府県", "住所", "電話番号", "非表示１", "非表示２", "非表示３", "非表示４" };

                    this.displayTags = new string[] { "引合業者CDが表示されます", "引合業者名が表示されます", "引合現場CDが表示されます",
                        "引合現場名が表示されます",	"引合現場フリガナが表示されます", "郵便番号が表示されます",
                        "都道府県が表示されます", "住所が表示されます", "電話番号が表示されます" ,"非表示１", "非表示２", "非表示３", "非表示４"};

                    this.hideColumnNames = new string[] { "GYOUSHA_HIKIAI_FLG", "GENBA_HIKIAI_FLG", "GYOUSHA_TODOUFUKEN_NAME_RYAKU", "GENBA_TODOUFUKEN_NAME_RYAKU" }; //画面へ戻せるように隠し
                    break;

                default:
                    break;
            }

            // 画面のラベル表示名を更新
            this.form.lb_title.Text = childLabel + "検索";
            this.form.label16.Text = parentLabel + "検索条件";

            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_BANK_SHITEN:
                    this.form.label3.Text = "支店検索条件";
                    this.form.label1.Text = "支店ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = "支店ﾌﾘｶﾞﾅ頭文字(母音)";
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    this.form.lb_title.Text = "電子事業場検索";
                    this.form.label16.Text = "事業者検索条件";

                    this.form.label3.Text = "事業場検索条件";
                    this.form.label1.Text = "ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = "ﾌﾘｶﾞﾅ頭文字(母音)";
                    break;

                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                    this.form.lb_title.Text = "引合現場検索";
                    this.form.label1.Text = "現場ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = "現場ﾌﾘｶﾞﾅ頭文字(母音)";
                    this.form.label3.Text = "現場検索条件";
                    break;

                default:
                    this.form.label3.Text = childLabel + "検索条件";
                    this.form.label1.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(子音)";
                    this.form.label2.Text = childLabel + "ﾌﾘｶﾞﾅ頭文字(母音)";
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

            // ヒントテキストを更新
            this.form.PARENT_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.PARENT_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", parentLabel);
            this.form.CHILD_CONDITION_ITEM.Tag = string.Format("【{0}】のいずれかで入力してください", hintTextConditon);
            this.form.CHILD_CONDITION_VALUE.Tag = string.Format("{0}について検索条件を入力してください", childLabel);

            // カラム設定(画面ごとに表示カラムは変わらないはず)
            for (int i = 0; i < displayColumnNames.Length; i++)
            {
                if (i == 0 && setCheckBoxFirst) //品名の複数選択チェックボックス
                {
                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    column.DataPropertyName = bindColumnNames[i];
                    column.Name = bindColumnNames[i];
                    column.HeaderText = displayColumnNames[i];
                    //column.CellTemplate = new DataGridViewCheckBoxCell();
                    if (displayTags.Length > 0)
                    {
                        column.Tag = displayTags[i];
                    }
                    column.ReadOnly = false;
                    this.form.customDataGridView1.Columns.Add(column);
                }
                else//通常列
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.DataPropertyName = bindColumnNames[i];
                    column.Name = bindColumnNames[i];
                    column.HeaderText = displayColumnNames[i];
                    if (displayTags.Length > 0)
                    {
                        //column.Tag = displayTags[i];
                        column.CellTemplate.Tag = displayTags[i];
                    }
                    column.ReadOnly = true;

                    if (this.hideColumnNames.Contains(column.Name)) //非表示
                    {
                        column.Visible = false;
                    }

                    this.form.customDataGridView1.Columns.Add(column);
                }

            }

            //ダミーカラム EMPTY　：空文字 を画面反映したい場合に利用
            this.form.customDataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "EMPTY",
                HeaderText = "EMPTY",
                Visible = false
            });

            if (WINDOW_ID.M_HINMEI_SEARCH == this.form.WindowId)
            {
                // 品名マスタ検索時は伝票、単位、単価を隠す
                this.form.customDataGridView1.Columns["DENPYOU_KBN_NAME"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;

                //20250325 --> update 20250402
                this.form.customDataGridView1.Columns["SHURUI_FURIGANA"].Visible = false;
                this.form.customDataGridView1.Columns["HINMEI_FURIGANA"].Visible = false;

                this.form.customDataGridView1.Columns["UNIT_NAME"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                this.form.customDataGridView1.Columns["TANKA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
            }

            if (WINDOW_ID.M_HINMEI == this.form.WindowId)
            {
                //20250326
                this.form.customDataGridView1.Columns["SHURUI_FURIGANA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                this.form.customDataGridView1.Columns["HINMEI_FURIGANA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
            }

            //列リサイズ(ここでの処理の場合は、一度だけでは反映されず、2回呼ぶと反映された)
            ResizeColumns(this.form.customDataGridView1, this.form.WindowId);
            ResizeColumns(this.form.customDataGridView1, this.form.WindowId);
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
            var buttonSetting = this.CcreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            EventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CcreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の入力項目制御処理（画面に業者が入力されていると、検索固定して現場だけの検索にする機能）
        /// 起動ポイントでpopupSearchSendParamが指定されている場合、
        /// Enabledにする検索条件があるため、ここでコントロール。
        /// </summary>
        private void ChangeDisplayFilter()
        {
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                if (popupSearchSendParam == null || string.IsNullOrEmpty(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                //todo:ポップアップ対象追加時修正箇所
                switch (popupSearchSendParam.KeyName)
                {
                    case "BANK_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_BANK_SHITEN))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    case "GYOUSHA_CD":
                    case "M_GYOUSHA.GYOUSHA_CD":
                    case "M_HIKIAI_GYOUSHA.GYOUSHA_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_GENBA) || this.form.WindowId.Equals(WINDOW_ID.M_GENBA_CLOSED) || this.form.WindowId.Equals(WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU) || this.form.WindowId.Equals(WINDOW_ID.M_GENBA_ALL))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    case "EDI_MEMBER_ID":
                    case "M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID":

                        if (this.form.WindowId.Equals(WINDOW_ID.M_DENSHI_JIGYOUJOU))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }

                        //現場で、電子事業者を指定された場合業者コードを取得する
                        if (this.form.WindowId.Equals(WINDOW_ID.M_GENBA) || this.form.WindowId.Equals(WINDOW_ID.M_GENBA_CLOSED) || this.form.WindowId.Equals(WINDOW_ID.M_GENBA_ALL))
                        {
                            var edi_men_id = this.GetControlOrValue(popupSearchSendParam);

                            if (!string.IsNullOrEmpty(edi_men_id))
                            {
                                var jigyoushaDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                                var search = new M_DENSHI_JIGYOUSHA() { EDI_MEMBER_ID = edi_men_id };

                                var result = jigyoushaDao.GetAllValidData(search);

                                if (result != null && result.Length > 0 && !string.IsNullOrEmpty(result[0].GYOUSHA_CD))
                                {
                                    this.form.PARENT_CONDITION_VALUE.Text = result[0].GYOUSHA_CD;
                                    this.form.panel1.Enabled = false;
                                    this.ParentFilterDispFlag = false;
                                    this.form.PARENT_CONDITION_ITEM.Text = "1";
                                }
                                else
                                {
                                    Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E057", "電子事業場が将軍連携マスタと紐付け", "事業場を特定"); //{0}されていないため、{1}できません。
                                    this.form.Close();
                                    return;
                                }
                            }
                        }

                        break;

                    case "SHURUI_CD":
                        if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI))
                        {
                            this.form.PARENT_CONDITION_VALUE.Text = this.GetControlOrValue(popupSearchSendParam);
                            if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                            {
                                this.form.panel1.Enabled = false;
                                this.ParentFilterDispFlag = false;
                                this.form.PARENT_CONDITION_ITEM.Text = "1";
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #region イベント用処理

        /// <summary>
        /// 請求・精算宛名ラベルから呼び出されたかどうかの判定する条件リスト
        /// </summary>
        public List<string> AddJoinTitle = new List<string>{"lb_title"};
        public List<string> AddJoinText = new List<string>{"請求宛名ラベル　条件指定", "精算宛名ラベル　条件指定"};

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search(bool isF1 = false)
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.isKobetsuHinmeiTankSearchFlg = false;

                // 個別品名単価マスタから品名情報を取得する、かつ業者CDはBlank場合、エラーを出す
                if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
                {
                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        if ((this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value.Equals(1) && !isF1)
                            || (this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value.Equals(2) && isF1))
                        {
                            this.form.errmessage.MessageBoxShow("E287");
                            LogUtility.DebugMethodEnd(-1);
                            return -1;
                        }
                    }

                    if ((this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value.Equals(1) && !isF1)
                        || (this.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value.Equals(2) && isF1))
                    {
                        this.form.isKobetsuHinmeiTankSearchFlg = true;
                    }

                    // コントロール制御
                    InitControlForHinmeiSearch(this.form.isKobetsuHinmeiTankSearchFlg);
                }

                // 検索条件生成
                this.SetSearchString();

                if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
                {
                    if (this.form.isKobetsuHinmeiTankSearchFlg)
                    {
                        this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataForDenpyouSql.sql";
                    }
                    else
                    {
                        this.executeSqlFilePath = "KensakuKyoutsuuPopupForMultiKey.Sql.GetHinmeiDataForMasterSql.sql";
                    }
                }

                DataTable dt = new DataTable();

                if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH) && this.form.isKobetsuHinmeiTankSearchFlg)
                {
                    DTOClass data = new DTOClass();
                    data.DENSHU_KBN_CD = string.IsNullOrEmpty(this.form.DENSHU_KBN_CD) ? 3 : SqlInt16.Parse(this.form.DENSHU_KBN_CD);
                    data.DENPYOU_KBN_CD = string.IsNullOrEmpty(this.form.DENPYOU_KBN.Text) ? 3 : SqlInt16.Parse(this.form.DENPYOU_KBN.Text);
                    data.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    data.GENBA_CD = this.form.GENBA_CD.Text;
                    data.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                    data.NIOROSHI_GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    data.NIOROSHI_GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;

                    //data.SHURUI_KBN_CD = string.IsNullOrEmpty(this.form.PARENT_CONDITION_ITEM.Text) ? SqlInt16.Null : SqlInt16.Parse(this.form.PARENT_CONDITION_ITEM.Text);
                    //data.SHURUI_KBN_INFO = string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text) ? string.Empty : this.form.PARENT_CONDITION_VALUE.Text;

                    //20250403
                    data.SHURUI_KBN_INFO = string.IsNullOrEmpty(this.form.SHURUI_KENSAKU_JOKEN_CBB.SelectedValue.ToString()) ? string.Empty : this.form.SHURUI_KENSAKU_JOKEN_CBB.SelectedValue.ToString();

                    data.HINMEI_KBN_CD = string.IsNullOrEmpty(this.form.CHILD_CONDITION_ITEM.Text) ? SqlInt16.Null : SqlInt16.Parse(this.form.CHILD_CONDITION_ITEM.Text);
                    data.HINMEI_KBN_INFO = string.IsNullOrEmpty(this.form.CHILD_CONDITION_VALUE.Text) ? string.Empty : this.form.CHILD_CONDITION_VALUE.Text;
                    data.DENPYOU_DATE = this.form.DENPYOU_DATE;

                    MKHTClass mkhtDao = DaoInitUtility.GetComponent<MKHTClass>();
                    dt = mkhtDao.GetHinmeiDataForDenpyouSql(data);
                }
                else
                {
                    // 基本的なスクリプトを取得
                    var thisAssembly = Assembly.GetExecutingAssembly();
                    using (var resourceStream = thisAssembly.GetManifestResourceStream(this.executeSqlFilePath))
                    {
                        using (var sqlStr = new StreamReader(resourceStream))
                        {
                            //joinは未対応
                            //sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.joinStr + this.whereStr;
                            string sql = string.Empty;

                            // Join句を含めるかどうかのフラグ
                            bool addJoinFlg = false;

                            // 請求・精算宛名ラベルから呼び出され、かつpopupWindowSettingがNULLではない場合はjoinを追加
                            if(!string.IsNullOrEmpty(this.joinStr))
                            {
                                var ctrls = this.form.ParentControls.Where(n => n is Control)
                                                                    .Select(n => (Control)n)
                                                                    .Where(n => AddJoinTitle[0].Equals(n.Name)
                                                                             && ((AddJoinText[0].Equals(n.Text)) || AddJoinText[1].Equals(n.Text)))
                                                                    .ToList();
                                if (ctrls.Count == 1)
                                {
                                    addJoinFlg = true;
                                }
                            }
                            if (addJoinFlg)
                            {
                                sql =  sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.joinStr  + this.whereStr + this.orderStr;
                            }
                            else
                            {
                                sql = sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + this.whereStr + this.orderStr;
                            }

                            //distinct対応 結合条件によっては列が増えるため
                            int idx = sql.IndexOf("SELECT ", StringComparison.InvariantCultureIgnoreCase);
                            int idxDis = sql.IndexOf("DISTINCT ", StringComparison.InvariantCultureIgnoreCase);

                            if (idx > -1 && idxDis < 0)
                            {
                                var newsql = sql.Substring(0, idx) + "SELECT DISTINCT " + sql.Substring(idx + 7);
                                sql = newsql; //ステップ実行用に変数を経由
                            }

                            dt = this.dao.GetDateForStringSql(sql);
                            sqlStr.Close();
                        }
                    }
                }

                // DataTable table = GetStringDataTable(dt);
                this.SearchResult = dt;

                // 頭文字絞込み
                this.SearchResult.DefaultView.RowFilter = this.SetInitialSearchString();

                foreach (DataColumn col in this.SearchResult.Columns)
                {
                    if (col.ColumnName.Equals("SELECT_CHECK"))
                    {
                        col.ReadOnly = false;
                    }
                }

                if (this.form.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
                {
                    for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                    {
                        if (this.SearchResult.Rows[i]["TANKA"] != null
                            && !string.IsNullOrEmpty(this.SearchResult.Rows[i]["TANKA"].ToString()))
                        {
                            if (Convert.ToDecimal(this.SearchResult.Rows[i]["TANKA"]) == 0 && this.sysInfoEntity.SYS_TANKA_FORMAT.Equals("#,###"))
                            {
                                this.SearchResult.Rows[i]["TANKA"] = 0;
                            }
                            else
                            {
                                this.SearchResult.Rows[i]["TANKA"] = Convert.ToDecimal(this.SearchResult.Rows[i]["TANKA"]).ToString(this.sysInfoEntity.SYS_TANKA_FORMAT);
                            }
                        }
                    }
                }

                this.form.customDataGridView1.DataSource = this.SearchResult;
                //this.form.customDataGridView1.ReadOnly = true;

                // 品名検索時以外はカラムが存在しないので非表示処理は行わない
                if (WINDOW_ID.M_HINMEI == this.form.WindowId)
                {
                    // 20140718 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　start
                    //正名を隠す
                    this.form.customDataGridView1.Columns["HINMEI_NAME"].Visible = false;

                    if (this.form.customDataGridView1.Columns.Contains("UNIT_CD"))
                    {
                        this.form.customDataGridView1.Columns["UNIT_CD"].Visible = false;
                    }

                    if (this.form.customDataGridView1.Columns.Contains("DENPYOU_KBN_CD"))
                    {
                        this.form.customDataGridView1.Columns["DENPYOU_KBN_CD"].Visible = false;
                    }
                    // 20140718 syunrei EV005319_見積書を出力した時、品名が略称で表示されてしまうため、品名CDを入力した時にセットする品名は正式名称で表示する。　end

                    //20250326
                    this.form.customDataGridView1.Columns["SHURUI_FURIGANA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                    this.form.customDataGridView1.Columns["HINMEI_FURIGANA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                }
                if (WINDOW_ID.M_HINMEI_SEARCH == this.form.WindowId)
                {
                    //正名を隠す
                    this.form.customDataGridView1.Columns["HINMEI_NAME"].Visible = false;
                    this.form.customDataGridView1.Columns["UNIT_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["DENPYOU_KBN_CD"].Visible = false;

                    // 品名マスタ検索時は伝票、単位、単価を隠す
                    this.form.customDataGridView1.Columns["DENPYOU_KBN_NAME"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;

                    //20250325 --> update 20250403
                    this.form.customDataGridView1.Columns["SHURUI_FURIGANA"].Visible = false;
                    this.form.customDataGridView1.Columns["HINMEI_FURIGANA"].Visible = false;

                    this.form.customDataGridView1.Columns["UNIT_NAME"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                    this.form.customDataGridView1.Columns["TANKA"].Visible = this.form.isKobetsuHinmeiTankSearchFlg;
                }

                ResizeColumns(this.form.customDataGridView1, this.form.WindowId);

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
        /// 品名検索（個別単価検索機能有りバージョン）時の検索コントロール制御
        /// </summary>
        /// <param name="isEnabled"></param>
        private void InitControlForHinmeiSearch(bool isEnabled)
        {
            this.form.TORIHIKISAKI_CD.Enabled = isEnabled;
            this.form.TORIHIKISAKI_NAME.Enabled = isEnabled;
            this.form.GYOUSHA_CD.Enabled = isEnabled;
            this.form.GYOUSHA_NAME.Enabled = isEnabled;
            this.form.GENBA_CD.Enabled = isEnabled;
            this.form.GENBA_NAME.Enabled = isEnabled;
            this.form.UNPAN_GYOUSHA_CD.Enabled = isEnabled;
            this.form.UNPAN_GYOUSHA_NAME.Enabled = isEnabled;
            this.form.NIOROSHI_GYOUSHA_CD.Enabled = isEnabled;
            this.form.NIOROSHI_GYOUSHA_NAME.Enabled = isEnabled;
            this.form.NIOROSHI_GENBA_CD.Enabled = isEnabled;
            this.form.NIOROSHI_GENBA_NAME.Enabled = isEnabled;
            this.form.PANEL_DENPYOU_KBN.Enabled = isEnabled;
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
                    ResizeColumns(this.form.customDataGridView1, this.form.WindowId);
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
                if (!this.form.PopupMultiSelect)
                {
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
                }
                else
                {
                    for (int i = 0; i < this.returnParamNames.Length; i++)
                    {
                        List<string> list = new List<string>();
                        for (int j = 0; j < this.form.customDataGridView1.Rows.Count; j++)
                        {
                            if (((bool)this.form.customDataGridView1.Rows[j].Cells[0].Value))
                            {
                                var setData = this.form.customDataGridView1.Rows[j].Cells[this.returnParamNames[i]];
                                list.Add(setData.Value.ToString());
                            }
                        }
                        PopupReturnParam popupParam = new PopupReturnParam();
                        popupParam.Key = "Value";
                        popupParam.Value = string.Join(",", list.ToArray());
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
        internal bool ImeControlParentCondition()
        {
            try
            {
                switch (this.form.PARENT_CONDITION_ITEM.Text)
                {
                    case "1":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                        break;

                    case "2":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "3":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                        break;

                    case "4":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "5":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "6":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                        break;

                    case "7":
                        this.form.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    default:
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlParentCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 検索条件入力欄のIME制御
        /// </summary>
        internal bool ImeControlChildCondition()
        {
            try
            {
                switch (this.form.CHILD_CONDITION_ITEM.Text)
                {
                    case "1":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                        break;

                    case "2":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "3":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
                        break;

                    case "4":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "5":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    case "6":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Alpha;
                        break;

                    case "7":
                        this.form.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
                        break;

                    default:
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImeControlChildCondition", ex);
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
            string parentTableName = string.Empty;
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                // TODO:.Nemeでちゃんとクラス名取れているか確認
                case WINDOW_ID.M_BANK_SHITEN:
                    this.SearchInfo = new M_BANK_SHITEN();
                    tableName = typeof(M_BANK_SHITEN).Name;
                    parentTableName = typeof(M_BANK).Name;
                    break;

                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                case WINDOW_ID.M_GENBA_ALL:
                    this.SearchInfo = new M_GENBA();
                    tableName = typeof(M_GENBA).Name;
                    parentTableName = typeof(M_GYOUSHA).Name;
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    this.SearchInfo = new M_DENSHI_JIGYOUJOU();
                    tableName = typeof(M_DENSHI_JIGYOUJOU).Name;
                    parentTableName = typeof(M_DENSHI_JIGYOUSHA).Name;
                    break;

                case WINDOW_ID.M_HINMEI:
                case WINDOW_ID.M_HINMEI_SEARCH:
                    this.SearchInfo = new M_HINMEI();
                    tableName = typeof(M_HINMEI).Name;
                    parentTableName = typeof(M_SHURUI).Name;
                    break;

                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                    this.SearchInfo = new M_HIKIAI_GENBA();
                    tableName = typeof(M_HIKIAI_GENBA).Name;
                    parentTableName = typeof(M_HIKIAI_GYOUSHA).Name;
                    break;

                default:
                    break;
            }

            //hack:SQLインジェクション対策必要
            //todo:ポップアップ対象追加時修正箇所
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    this.orderStr = " ORDER BY " + tableName + ".EDI_MEMBER_ID , " + tableName + ".JIGYOUJOU_CD ";
                    break;

                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                case WINDOW_ID.M_GENBA_ALL:
                    this.orderStr = " ORDER BY " + tableName + ".GYOUSHA_CD , " + tableName + ".GENBA_CD ";
                    break;

                case WINDOW_ID.M_BANK_SHITEN:
                    this.orderStr = " ORDER BY " + tableName + ".BANK_CD , " + tableName + ".BANK_SHITEN_CD ";
                    break;

                case WINDOW_ID.M_HINMEI:
                    this.orderStr = " ORDER BY " + tableName + ".HINMEI_CD ";
                    break;

                case WINDOW_ID.M_HINMEI_SEARCH:
                    break;

                default:
                    this.orderStr = " ORDER BY " + tableName + "." + tableName.Substring(2) + "_CD ";
                    break;
            }

            // カラム名を動的に指定するために必要
            var ColumnHeaderName = tableName.Substring(2, tableName.Length - 2);
            var parentColumnHeaderName = parentTableName.Substring(2, parentTableName.Length - 2);

            // 親の検索条件
            // 起動時にParentの条件が指定されている場合はPearent用条件句を生成してはいけない
            if (ParentFilterDispFlag)
            {
                if (!string.IsNullOrEmpty(this.form.PARENT_CONDITION_VALUE.Text))
                {
                    // シングルクォートは2つ重ねる
                    var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.PARENT_CONDITION_VALUE.Text);
                    switch (this.form.PARENT_CONDITION_ITEM.Text)
                    {
                        case "1":
                            // ｺｰﾄﾞ
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND " + parentTableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%' ";
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND " + parentTableName + ".GYOUSHA_CD LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "2":
                            // 略称名
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND " + parentTableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%' ";
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND " + parentTableName + ".GYOUSHA_NAME_RYAKU LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "3":
                            // ﾌﾘｶﾞﾅ
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND 1=1 ";  //フリガナ無
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND " + parentTableName + ".GYOUSHA_FURIGANA LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "4":
                            // 都道府県
                            // もし数値変換できない場合は設定しない
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND " + parentTableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%'";
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
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND (" + parentTableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + parentTableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) "; //1と2がある
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND (" + parentTableName + ".GYOUSHA_ADDRESS1 LIKE '%" + condition + "%' OR " + parentTableName + ".GYOUSHA_ADDRESS2 LIKE '%" + condition + "%' ) "; //1と2がある
                                    break;

                                default:
                                    this.whereStr = " AND " + parentTableName + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "6":
                            // 電話
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                    this.whereStr = " AND " + parentTableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%' ";
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND " + parentTableName + ".GYOUSHA_TEL LIKE '%" + condition + "%' ";
                                    break;

                                default:
                                    this.whereStr = "AND " + parentTableName + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                                    break;
                            }
                            break;

                        case "7":
                            // ﾌﾘｰ
                            // ﾌﾘｰでは1～6のすべてに対して検索をかける
                            switch (this.form.WindowId)
                            {
                                case WINDOW_ID.M_DENSHI_JIGYOUJOU: //電子は列名が独特
                                    this.whereStr = " AND (" + parentTableName + ".EDI_MEMBER_ID LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".JIGYOUSHA_NAME LIKE '%" + condition + "%'";
                                    //フリガナ無
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".JIGYOUSHA_ADDRESS1 LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR (" + parentTableName + ".JIGYOUSHA_ADDRESS2 LIKE '%" + condition + "%' OR " + parentTableName + ".JIGYOUSHA_ADDRESS3 LIKE '%" + condition + "%' ) ";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".JIGYOUSHA_TEL LIKE '%" + condition + "%') ";
                                    break;

                                case WINDOW_ID.M_HINMEI: //品名は郵便番号なし
                                case WINDOW_ID.M_HINMEI_SEARCH:
                                case WINDOW_ID.M_BANK_SHITEN:
                                    this.whereStr = " AND (" + parentTableName + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                    //品名は郵便番号系がない
                                    this.whereStr = this.whereStr + " )";
                                    break;

                                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                    this.whereStr = " AND (" + parentTableName + ".GYOUSHA_CD LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".GYOUSHA_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".GYOUSHA_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".GYOUSHA_FURIGANA LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR (" + parentTableName + ".GYOUSHA_ADDRESS1 LIKE '%" + condition + "%' OR " + parentTableName + ".GYOUSHA_ADDRESS2 LIKE '%" + condition + "%' ) ";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + ".GYOUSHA_TEL LIKE '%" + condition + "%') ";
                                    break;

                                default:
                                    this.whereStr = " AND (" + parentTableName + "." + parentColumnHeaderName + "_CD LIKE '%" + condition + "%'";

                                    //20250327
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_NAME1 LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_NAME2 LIKE '%" + condition + "%'";

                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                    this.whereStr = this.whereStr + " OR " + parentTableName + "." + parentColumnHeaderName + "_TEL LIKE '%" + condition + "%')";
                                    break;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            //20250326
            switch (this.form.WindowId)
            {
                case WINDOW_ID.M_HINMEI_SEARCH:
                    if (!string.IsNullOrEmpty(this.form.SHURUI_KENSAKU_JOKEN_CBB.Text))
                    {
                        var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.SHURUI_KENSAKU_JOKEN_CBB.SelectedValue.ToString());

                        this.whereStr = this.whereStr + " AND " + tableName + ".SHURUI_CD LIKE '%" + condition + "%' ";
                    }
                    break;

                case WINDOW_ID.M_HINMEI:
                    if (!string.IsNullOrEmpty(this.form.SHURUI_KENSAKU_JOKEN_CBB1.Text))
                    {
                        var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.SHURUI_KENSAKU_JOKEN_CBB1.SelectedValue.ToString());

                        this.whereStr = this.whereStr + " AND " + tableName + ".SHURUI_CD LIKE '%" + condition + "%' ";
                    }
                    break;

                default:
                    break;
            }

            // 子の検索条件
            if (!string.IsNullOrEmpty(this.form.CHILD_CONDITION_VALUE.Text))
            {
                var condition = SqlCreateUtility.CounterplanEscapeSequence(this.form.CHILD_CONDITION_VALUE.Text);
                switch (this.form.CHILD_CONDITION_ITEM.Text)
                {
                    case "1":
                        // ｺｰﾄﾞ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".JIGYOUJOU_CD LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".GENBA_CD LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "2":
                        // 略称名
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".JIGYOUJOU_NAME LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".GENBA_NAME_RYAKU LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_BANK_SHITEN:
                                this.whereStr = this.whereStr + " AND " + tableName + ".BANK_SHIETN_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "3":
                        // ﾌﾘｶﾞﾅ
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND 1=1 ";  //フリガナ無
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".GENBA_FURIGANA LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "4":
                        // 都道府県
                        // もし数値変換できない場合は設定しない
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".JIGYOUJOU_ADDRESS1 LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND M_TODOUFUKEN2.TODOUFUKEN_NAME_RYAKU LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "5":
                        // 住所
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND (" + tableName + ".JIGYOUJOU_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUJOU_ADDRESS3 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = this.whereStr + " AND (" + tableName + ".GENBA_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".GENBA_ADDRESS2 LIKE '%" + condition + "%' ) "; //1と2がある
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "6":
                        // 電話
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".JIGYOUJOU_TEL LIKE '%" + condition + "%' ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = this.whereStr + " AND " + tableName + ".GENBA_TEL LIKE '%" + condition + "%' ";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + condition + "%'";
                                break;
                        }
                        break;

                    case "7":
                        // ﾌﾘｰ
                        // ﾌﾘｰでは1～6のすべてに対して検索をかける
                        switch (this.form.WindowId)
                        {
                            case WINDOW_ID.M_DENSHI_JIGYOUJOU: //電子は列名が独特
                                this.whereStr = this.whereStr + " AND (" + tableName + ".JIGYOUJOU_CD LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUJOU_NAME LIKE '%" + condition + "%'";
                                //フリガナ無
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUJOU_ADDRESS1 LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".JIGYOUJOU_ADDRESS2 LIKE '%" + condition + "%' OR " + tableName + ".JIGYOUJOU_ADDRESS3 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".JIGYOUJOU_TEL LIKE '%" + condition + "%') ";
                                break;

                            case WINDOW_ID.M_HINMEI: //住所等なし
                            case WINDOW_ID.M_HINMEI_SEARCH:
                                this.whereStr = this.whereStr + " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " ) ";
                                break;

                            case WINDOW_ID.M_BANK_SHITEN:
                                this.whereStr = this.whereStr + " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + ".BANK_SHIETN_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " ) ";
                                break;

                            case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                                this.whereStr = " AND (" + tableName + ".GENBA_CD LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GENBA_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GENBA_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GENBA_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR (" + tableName + ".GENBA_ADDRESS1 LIKE '%" + condition + "%' OR " + tableName + ".GENBA_ADDRESS2 LIKE '%" + condition + "%' ) ";
                                this.whereStr = this.whereStr + " OR " + tableName + ".GENBA_TEL LIKE '%" + condition + "%') ";
                                break;

                            default:
                                this.whereStr = this.whereStr + " AND (" + tableName + "." + ColumnHeaderName + "_CD LIKE '%" + condition + "%'";

                                //20250327
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME1 LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME2 LIKE '%" + condition + "%'";

                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_NAME_RYAKU LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_FURIGANA LIKE '%" + condition + "%'";
                                this.whereStr = this.whereStr + " OR M_TODOUFUKEN2.TODOUFUKEN_NAME_RYAKU LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%'";
                                this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%') ";
                                break;
                        }

                        //PT722
                        //this.whereStr = this.whereStr + " OR M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%'";
                        //this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_ADDRESS1 LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%'";
                        //this.whereStr = this.whereStr + " OR " + tableName + "." + ColumnHeaderName + "_TEL LIKE '%" + this.form.CHILD_CONDITION_VALUE.Text + "%')";
                        break;

                    default:
                        break;
                }
            }

            //Add Query  // NvNhat #160897
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

            this.whereStr = " WHERE 1 = 1 " + this.whereStr;

            // 画面から来た絞込み情報で条件句を作成
            bool existSearchParam = false;  // popupSearchSendParamからWHEREが生成されたかどうかのフラグ
            StringBuilder sb = new StringBuilder(" ");
            foreach (PopupSearchSendParamDto popupSearchSendParam in this.form.PopupSearchSendParams)
            {
                //Check if Delete, Tekiyou // NvNhat #160897
                if (this.CheckParamsIsValid(popupSearchSendParam.KeyName))
                {
                    continue;
                }

                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_BEGIN"))
                {
                    object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { popupSearchSendParam.Control });
                    string controlText = PropertyUtility.GetTextOrValue(control[0]);
                    var ctr = control[0] as CustomDateTimePicker;
                    var ctr2 = control[0] as DataGridViewTextBoxCell;
                    string tekiyouSql = string.Empty;
                    if (ctr != null && ctr.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou1, tableName, ctr.Value);
                        sb.Append(tekiyouSql);
                    }
                    else if (ctr2 != null && ctr2.Value != null)
                    {
                        tekiyouSql = string.Format(tekiyou1, tableName, ctr2.Value);
                        sb.Append(tekiyouSql);
                    }
                    else
                    {
                        tekiyouSql = string.Format(tekiyou2, tableName);
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }

                if (popupSearchSendParam.KeyName != null && popupSearchSendParam.KeyName.Equals("TEKIYOU_FLG")
                    && !string.IsNullOrEmpty(popupSearchSendParam.Value))
                {
                    if ("TRUE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Format(tekiyou2, tableName);
                        sb.Append(tekiyouSql);
                    }
                    else if ("FALSE".Equals(popupSearchSendParam.Value.ToUpper()))
                    {
                        string tekiyouSql = string.Format(" AND {0}.DELETE_FLG = 0 ", tableName);
                        sb.Append(tekiyouSql);
                    }
                    continue;
                }

                this.depthCnt = 1;
                existSearchParam = false;
                string where = "";
                if (this.form.WindowId == WINDOW_ID.M_GENBA_CLOSED && (popupSearchSendParam.KeyName == "SAGYOU_DATE" || popupSearchSendParam.KeyName == "DENPYOU_DATE"))
                {
                    where = this.AddSql(popupSearchSendParam);
                }
                else
                {
                    where = this.CreateWhereStrFromScreen(popupSearchSendParam, tableName, ref existSearchParam);
                }
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
                    if (this.CheckParamsIsValid(popupSearchSendParam.KeyName)) // NvNhat #160897
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
                // 母音が選択されてなければ選択されている母音のをすべてを表示
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
                    // 母音があれば母音で絞込み

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
                case WINDOW_ID.M_BANK_SHITEN:
                    colName = BANK_SHITEN_COLUMNS.BANK_SHITEN_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_GENBA:
                case WINDOW_ID.M_GENBA_CLOSED:
                case WINDOW_ID.M_GENBA_ALL:
                    colName = GENBA_COLUMNS.GENBA_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    colName = DENSHI_JIGYOUJOU_COLUMNS.JIGYOUJOU_NAME.ToString();
                    break;

                case WINDOW_ID.M_HINMEI:
                    colName = HINMEI_COLUMNS.HINMEI_FURIGANA.ToString();
                    break;

                case WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU:
                    colName = HIKIAI_GENBA_COLUMNS.GENBA_FURIGANA.ToString();
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
        /// PopupSearchSendParamDtoのControlの値か、Valueを返す
        /// 先にControlをチェックし、存在しなければValueを返す。
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string GetControlOrValue(PopupSearchSendParamDto dto)
        {
            if (dto == null)
            {
                return string.Empty;
            }

            // 絞込み条件にControlが指定されていればそれを使い、無ければValueを使用する
            // 両方無ければ空を返す
            if (dto.Control == null || string.IsNullOrEmpty(dto.Control))
            {
                if (dto.Value != null && !string.IsNullOrEmpty(dto.Value))
                {
                    return dto.Value;
                }
            }
            else
            {
                object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });
                if (control != null)
                {
                    // 複数同じ名前のコントロールは存在しないはず
                    return PropertyUtility.GetTextOrValue(control[0]);
                }
            }

            return string.Empty;
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
                case "M_DENSHI_JIGYOUJOU": //エンティティに定義はあるが実テーブルには列がない。FW修正の影響回避のためここでブロックする
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
                        if (this.CheckParamsIsValid(searchData.LeftColumn))// NvNhat #160897
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
        static public void ResizeColumns(DataGridView dgv, WINDOW_ID windowId)
        {
            //自動整列 (処理時間が掛かる為コメント。下のforeach内でAllCells指定する)
            //dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //自動調整を解除しつつ、すべての横幅を1ドットプラス
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (windowId.Equals(WINDOW_ID.M_HINMEI_SEARCH) && (c.Name.Equals("SHURUI_FURIGANA") || c.Name.Equals("HINMEI_FURIGANA")))
                {
                    c.MinimumWidth = 50;
                }
                else
                {
                    //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    c.Width += 1;
                }

                if (c.DisplayIndex == 0 || (windowId.Equals(WINDOW_ID.M_HINMEI_SEARCH) && c.Name.Equals("SHURUI_CD")))
                {
                    // 先頭の表示項目が折り返されてしまう為、Noneを設定
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                //20250327
                if (windowId.Equals(WINDOW_ID.M_GENBA) && (c.Name.Equals("TORIHIKISAKI_NAME_RYAKU")) || (c.Name.Equals("GYOUSHA_NAME_RYAKU")))
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.Width = 170;
                }

                if (windowId.Equals(WINDOW_ID.M_GENBA) && (c.Name.Equals("GENBA_NAME_RYAKU")))
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.Width = 240;
                }
            }
        }
        /// <summary>
        /// Changed display filter for Genba
        /// NvNhat #160897 BEGIN
        /// </summary>
        private void ChangeDisplayGenba()
        {
            #region Setting Layout
            Control itemChild = this.form.ParentControls[0] as Control;
            if (itemChild == null)
            {
                _flagChangedJouken = false;
                return;
            }
            BaseBaseForm parentForm = itemChild.Parent as BaseBaseForm;
            if (allowForms.Any(x => itemChild.Parent != null && itemChild.Parent.Text.Contains(x)))
            {
                _flagChangedJouken = true;
            }
            else if (parentForm == null)
            {
                _flagChangedJouken = false;
                return;
            }
            else if (allowForms.Any(x => parentForm.Text.Contains(x) || (itemChild.Parent != null && itemChild.Parent.Text.Contains(x))))
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
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.form.HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.form.label4 = new System.Windows.Forms.Label();
            this.form.HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();

            // 
            // HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(248, 167);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Name = "HYOUJI_JOUKEN_TEKIYOUGAI";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(96, 17);
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 518;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // HYOUJI_JOUKEN_DELETED
            // 
            this.form.HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.form.HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(194, 167);
            this.form.HYOUJI_JOUKEN_DELETED.Name = "HYOUJI_JOUKEN_DELETED";
            this.form.HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(54, 17);
            this.form.HYOUJI_JOUKEN_DELETED.TabIndex = 517;
            this.form.HYOUJI_JOUKEN_DELETED.TabStop = false;
            this.form.HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.form.HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_DELETED.Visible = true;
            this.form.HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);
            // 
            // label4
            // 
            this.form.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.label4.ForeColor = System.Drawing.Color.White;
            this.form.label4.Location = new System.Drawing.Point(14, 163);
            this.form.label4.Name = "label4";
            this.form.label4.Size = new System.Drawing.Size(108, 22);
            this.form.label4.TabIndex = 519;
            this.form.label4.Text = "表示条件";
            this.form.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.form.label4.Visible = true;
            // 
            // HYOUJI_JOUKEN_TEKIYOU
            // 
            this.form.HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(128, 167);
            this.form.HYOUJI_JOUKEN_TEKIYOU.Name = "HYOUJI_JOUKEN_TEKIYOU";
            this.form.HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(68, 17);
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabIndex = 516;
            this.form.HYOUJI_JOUKEN_TEKIYOU.TabStop = false;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.form.HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.Visible = true;
            this.form.HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.JOUKEN_CheckedChanged);

            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_DELETED);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOUGAI);
            this.form.Controls.Add(this.form.HYOUJI_JOUKEN_TEKIYOU);
            this.form.Controls.Add(this.form.label4);

            #endregion

            this.form.label3.Location = new System.Drawing.Point(14, 85);
            this.form.panel2.Location = new System.Drawing.Point(126, 86);

            this.form.label1.Location = new System.Drawing.Point(14, 111);
            this.form.panel3.Location = new System.Drawing.Point(190, 112);

            this.form.label2.Location = new System.Drawing.Point(14, 137);
            this.form.plBOIN.Location = new System.Drawing.Point(190, 138);

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
            catch (Exception ex)
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
                   (param.Equals("TEKIYOU_BEGIN") || param.Equals("TEKIYOU_FLG") || param.Equals("DELETE_FLG") || param.Contains("DELETE_FLG")))
                {
                    isContinue = true;
                }
                return isContinue;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                return false;
            }
        }
        // NvNhat #160897 END
        /// <summary>
        /// 休動検索条件を作成する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string AddSql(PopupSearchSendParamDto dto)
        {
            string sql = string.Empty;
            object[] control = this.form.controlUtil.FindControl(this.form.ParentControls, new string[] { dto.Control });

            sql += " AND NOT EXISTS( ";
            sql += "SELECT 1 FROM M_WORK_CLOSED_HANNYUUSAKI T2 ";
            sql += "WHERE M_GENBA.GYOUSHA_CD = T2.GYOUSHA_CD ";
            sql += "AND M_GENBA.GENBA_CD = T2.GENBA_CD ";
            sql += string.Format("AND CONVERT(CHAR(10), T2.CLOSED_DATE, 111) = CONVERT(CHAR(10), '{0}', 111) ", PropertyUtility.GetTextOrValue(control[0]));
            sql += "AND M_GENBA.DELETE_FLG = 0 ";
            sql += "AND T2.DELETE_FLG = 0) ";
            return sql;
        }

        #endregion

        #region 画面レイアウト変更

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            // TODO: ログイン時に共通メンバでSYS_INFOの情報を保持する可能性があるため、
            //       その場合、このメソッドは必要なくなる。
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }

        /// <summary>
        /// 画面レイアウトを変更
        /// Ver 2.13で、画面レイアウトが変更になったため、このメソッドに処理をまとめる。
        /// </summary>
        public void ChangeLayout()
        {
            #region 不要なプロパティを初期化
            this.form.panel1.Visible = false; //20250324
            this.form.label1.Visible = false;
            this.form.panel3.Visible = false;
            this.form.label2.Visible = false;
            this.form.plBOIN.Visible = false;
            #endregion

            #region コントロールの初期化
            this.form.LABEL_TORIHIKISAKI = new System.Windows.Forms.Label();
            this.form.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.form.LABEL_GYOUSHA = new System.Windows.Forms.Label();
            this.form.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.form.LABEL_GENBA = new System.Windows.Forms.Label();
            this.form.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.form.LABEL_UNPAN_GYOUSHA = new System.Windows.Forms.Label();
            this.form.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.UNPAN_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.form.LABEL_NIOROSHI_GYOUSHA = new System.Windows.Forms.Label();
            this.form.NIOROSHI_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.NIOROSHI_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.form.LABEL_NIOROSHI_GENBA = new System.Windows.Forms.Label();
            this.form.NIOROSHI_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.form.NIOROSHI_GENBA_NAME = new r_framework.CustomControl.CustomTextBox();

            //20250324
            this.form.SHURUI_KENSAKU_JOKEN_CBB = new r_framework.CustomControl.CustomComboBox();

            this.form.LABEL_DENPYOU_KBN = new System.Windows.Forms.Label();
            this.form.PANEL_DENPYOU_KBN = new System.Windows.Forms.Panel();
            this.form.DENPYOU_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.form.DENPYOU_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.form.DENPYOU_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.form.DENPYOU_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            #endregion

            // Location用
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KensakuKyoutsuuPopupForMultiKeyForm));

            #region ○○検索条件
            this.form.label16.Location = new System.Drawing.Point(14, 119);
            this.form.panel1.Location = new System.Drawing.Point(126, 120);
            this.form.label3.Location = new System.Drawing.Point(14, 144);
            this.form.panel2.Location = new System.Drawing.Point(126, 145);
            #endregion

            #region 取引先
            // add Form
            form.Controls.Add(this.form.LABEL_TORIHIKISAKI);
            form.Controls.Add(this.form.TORIHIKISAKI_CD);
            form.Controls.Add(this.form.TORIHIKISAKI_NAME);

            // 
            // LABEL_TORIHIKISAKI
            // 
            this.form.LABEL_TORIHIKISAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_TORIHIKISAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_TORIHIKISAKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_TORIHIKISAKI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_TORIHIKISAKI.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_TORIHIKISAKI.Location = new System.Drawing.Point(14, 50);
            this.form.LABEL_TORIHIKISAKI.Name = "LABEL_TORIHIKISAKI";
            this.form.LABEL_TORIHIKISAKI.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_TORIHIKISAKI.TabIndex = 755;
            this.form.LABEL_TORIHIKISAKI.Text = "取引先";
            this.form.LABEL_TORIHIKISAKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // TORIHIKISAKI_CD
            // 
            this.form.TORIHIKISAKI_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.form.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.form.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.TORIHIKISAKI_CD.DBFieldsName = "";
            this.form.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.TORIHIKISAKI_CD.DisplayItemName = "";
            this.form.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.form.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.form.TORIHIKISAKI_CD.GetCodeMasterField = "";
            this.form.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.form.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.form.TORIHIKISAKI_CD.Location = new System.Drawing.Point(124, 50);
            this.form.TORIHIKISAKI_CD.MaxLength = 6;
            this.form.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.form.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.form.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.form.TORIHIKISAKI_CD.PopupGetMasterField = "";
            this.form.TORIHIKISAKI_CD.PopupSetFormField = "";
            this.form.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.TORIHIKISAKI_CD.PopupWindowName = "";
            this.form.TORIHIKISAKI_CD.ReadOnly = true;
            this.form.TORIHIKISAKI_CD.SetFormField = "";
            this.form.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.form.TORIHIKISAKI_CD.TabIndex = 753;
            this.form.TORIHIKISAKI_CD.TabStop = false;
            this.form.TORIHIKISAKI_CD.Tag = " ";
            this.form.TORIHIKISAKI_CD.ZeroPaddengFlag = true;

            // 
            // TORIHIKISAKI_NAME
            // 
            this.form.TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.TORIHIKISAKI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.TORIHIKISAKI_NAME.DBFieldsName = "TORIHIKISAKI_NAME";
            this.form.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.TORIHIKISAKI_NAME.DisplayItemName = "取引先名";
            this.form.TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.form.TORIHIKISAKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.TORIHIKISAKI_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.form.TORIHIKISAKI_NAME.ItemDefinedTypes = "varchar";
            this.form.TORIHIKISAKI_NAME.Location = new System.Drawing.Point(173, 50);
            this.form.TORIHIKISAKI_NAME.MaxLength = 40;
            this.form.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.form.TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.form.TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.form.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.TORIHIKISAKI_NAME.ReadOnly = true;
            this.form.TORIHIKISAKI_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.TORIHIKISAKI_NAME.TabIndex = 754;
            this.form.TORIHIKISAKI_NAME.Tag = " ";
            #endregion

            #region 業者
            // add Form
            form.Controls.Add(this.form.LABEL_GYOUSHA);
            form.Controls.Add(this.form.GYOUSHA_CD);
            form.Controls.Add(this.form.GYOUSHA_NAME);

            // 
            // LABEL_GYOUSHA
            // 
            this.form.LABEL_GYOUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_GYOUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_GYOUSHA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_GYOUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_GYOUSHA.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_GYOUSHA.Location = new System.Drawing.Point(14, 73);
            this.form.LABEL_GYOUSHA.Name = "LABEL_GYOUSHA";
            this.form.LABEL_GYOUSHA.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_GYOUSHA.TabIndex = 761;
            this.form.LABEL_GYOUSHA.Text = "業者";
            this.form.LABEL_GYOUSHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // GYOUSHA_CD
            // 
            this.form.GYOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.GYOUSHA_CD.ChangeUpperCase = true;
            this.form.GYOUSHA_CD.CharacterLimitList = null;
            this.form.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.GYOUSHA_CD.DBFieldsName = "";
            this.form.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.GYOUSHA_CD.DisplayItemName = "";
            this.form.GYOUSHA_CD.DisplayPopUp = null;
            this.form.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.form.GYOUSHA_CD.GetCodeMasterField = "";
            this.form.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.GYOUSHA_CD.IsInputErrorOccured = false;
            this.form.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.form.GYOUSHA_CD.Location = new System.Drawing.Point(124, 73);
            this.form.GYOUSHA_CD.MaxLength = 6;
            this.form.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.form.GYOUSHA_CD.PopupAfterExecute = null;
            this.form.GYOUSHA_CD.PopupBeforeExecute = null;
            this.form.GYOUSHA_CD.PopupGetMasterField = "";
            this.form.GYOUSHA_CD.PopupSetFormField = "";
            this.form.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.GYOUSHA_CD.PopupWindowName = "";
            this.form.GYOUSHA_CD.ReadOnly = true;
            this.form.GYOUSHA_CD.SetFormField = "";
            this.form.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.form.GYOUSHA_CD.TabIndex = 759;
            this.form.GYOUSHA_CD.TabStop = false;
            this.form.GYOUSHA_CD.Tag = " ";
            this.form.GYOUSHA_CD.ZeroPaddengFlag = true;

            // 
            // GYOUSHA_NAME
            // 
            this.form.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.GYOUSHA_NAME.DBFieldsName = "";
            this.form.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.GYOUSHA_NAME.DisplayItemName = "";
            this.form.GYOUSHA_NAME.DisplayPopUp = null;
            this.form.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.form.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.form.GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.form.GYOUSHA_NAME.Location = new System.Drawing.Point(173, 73);
            this.form.GYOUSHA_NAME.MaxLength = 40;
            this.form.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.form.GYOUSHA_NAME.PopupAfterExecute = null;
            this.form.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.form.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.form.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.form.GYOUSHA_NAME.ReadOnly = true;
            this.form.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.form.GYOUSHA_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.GYOUSHA_NAME.TabIndex = 760;
            this.form.GYOUSHA_NAME.Tag = " ";
            #endregion

            #region 現場
            // add Form
            form.Controls.Add(this.form.LABEL_GENBA);
            form.Controls.Add(this.form.GENBA_CD);
            form.Controls.Add(this.form.GENBA_NAME);

            // 
            // LABEL_GENBA
            // 
            this.form.LABEL_GENBA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_GENBA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_GENBA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_GENBA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_GENBA.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_GENBA.Location = new System.Drawing.Point(14, 96);
            this.form.LABEL_GENBA.Name = "LABEL_GENBA";
            this.form.LABEL_GENBA.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_GENBA.TabIndex = 767;
            this.form.LABEL_GENBA.Text = "現場";
            this.form.LABEL_GENBA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // GENBA_CD
            // 
            this.form.GENBA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.GENBA_CD.ChangeUpperCase = true;
            this.form.GENBA_CD.CharacterLimitList = null;
            this.form.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.GENBA_CD.DBFieldsName = "";
            this.form.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.GENBA_CD.DisplayItemName = "";
            this.form.GENBA_CD.DisplayPopUp = null;
            this.form.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.form.GENBA_CD.GetCodeMasterField = "";
            this.form.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.GENBA_CD.IsInputErrorOccured = false;
            this.form.GENBA_CD.ItemDefinedTypes = "varchar";
            this.form.GENBA_CD.Location = new System.Drawing.Point(124, 96);
            this.form.GENBA_CD.MaxLength = 6;
            this.form.GENBA_CD.Name = "GENBA_CD";
            this.form.GENBA_CD.PopupAfterExecute = null;
            this.form.GENBA_CD.PopupBeforeExecute = null;
            this.form.GENBA_CD.PopupGetMasterField = "";
            this.form.GENBA_CD.PopupSetFormField = "";
            this.form.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.GENBA_CD.PopupWindowName = "";
            this.form.GENBA_CD.ReadOnly = true;
            this.form.GENBA_CD.SetFormField = "";
            this.form.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.form.GENBA_CD.TabIndex = 765;
            this.form.GENBA_CD.TabStop = false;
            this.form.GENBA_CD.Tag = " ";
            this.form.GENBA_CD.ZeroPaddengFlag = true;

            // 
            // GENBA_NAME
            // 
            this.form.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.GENBA_NAME.DBFieldsName = "";
            this.form.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.GENBA_NAME.DisplayItemName = "";
            this.form.GENBA_NAME.DisplayPopUp = null;
            this.form.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.GENBA_NAME.IsInputErrorOccured = false;
            this.form.GENBA_NAME.ItemDefinedTypes = "varchar";
            this.form.GENBA_NAME.Location = new System.Drawing.Point(173, 96);
            this.form.GENBA_NAME.MaxLength = 40;
            this.form.GENBA_NAME.Name = "GENBA_NAME";
            this.form.GENBA_NAME.PopupAfterExecute = null;
            this.form.GENBA_NAME.PopupBeforeExecute = null;
            this.form.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.GENBA_NAME.ReadOnly = true;
            this.form.GENBA_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.GENBA_NAME.TabIndex = 766;
            this.form.GENBA_NAME.Tag = " ";
            #endregion

            #region 運搬業者
            // add Form
            form.Controls.Add(this.form.LABEL_UNPAN_GYOUSHA);
            form.Controls.Add(this.form.UNPAN_GYOUSHA_CD);
            form.Controls.Add(this.form.UNPAN_GYOUSHA_NAME);

            // 
            // LABEL_UNPAN_GYOUSHA
            // 
            this.form.LABEL_UNPAN_GYOUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_UNPAN_GYOUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_UNPAN_GYOUSHA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_UNPAN_GYOUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_UNPAN_GYOUSHA.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_UNPAN_GYOUSHA.Location = new System.Drawing.Point(481, 50);
            this.form.LABEL_UNPAN_GYOUSHA.Name = "label1";
            this.form.LABEL_UNPAN_GYOUSHA.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_UNPAN_GYOUSHA.TabIndex = 758;
            this.form.LABEL_UNPAN_GYOUSHA.Text = "運搬業者";
            this.form.LABEL_UNPAN_GYOUSHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // UNPAN_GYOUSHA_CD
            // 
            this.form.UNPAN_GYOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.UNPAN_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.UNPAN_GYOUSHA_CD.ChangeUpperCase = true;
            this.form.UNPAN_GYOUSHA_CD.CharacterLimitList = null;
            this.form.UNPAN_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.UNPAN_GYOUSHA_CD.DBFieldsName = "";
            this.form.UNPAN_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.UNPAN_GYOUSHA_CD.DisplayItemName = "";
            this.form.UNPAN_GYOUSHA_CD.DisplayPopUp = null;
            this.form.UNPAN_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.UNPAN_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.form.UNPAN_GYOUSHA_CD.GetCodeMasterField = "";
            this.form.UNPAN_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.form.UNPAN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.form.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(591, 50);
            this.form.UNPAN_GYOUSHA_CD.MaxLength = 6;
            this.form.UNPAN_GYOUSHA_CD.Name = "UNPAN_GYOUSHA_CD";
            this.form.UNPAN_GYOUSHA_CD.PopupAfterExecute = null;
            this.form.UNPAN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.form.UNPAN_GYOUSHA_CD.PopupGetMasterField = "";
            this.form.UNPAN_GYOUSHA_CD.PopupSetFormField = "";
            this.form.UNPAN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.UNPAN_GYOUSHA_CD.PopupWindowName = "";
            this.form.UNPAN_GYOUSHA_CD.ReadOnly = true;
            this.form.UNPAN_GYOUSHA_CD.SetFormField = "";
            this.form.UNPAN_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.form.UNPAN_GYOUSHA_CD.TabIndex = 756;
            this.form.UNPAN_GYOUSHA_CD.TabStop = false;
            this.form.UNPAN_GYOUSHA_CD.Tag = "";
            this.form.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;

            // 
            // UNPAN_GYOUSHA_NAME
            // 
            this.form.UNPAN_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.UNPAN_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.UNPAN_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.UNPAN_GYOUSHA_NAME.DBFieldsName = "";
            this.form.UNPAN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.UNPAN_GYOUSHA_NAME.DisplayItemName = "";
            this.form.UNPAN_GYOUSHA_NAME.DisplayPopUp = null;
            this.form.UNPAN_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.UNPAN_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.UNPAN_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.UNPAN_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.form.UNPAN_GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.form.UNPAN_GYOUSHA_NAME.Location = new System.Drawing.Point(640, 50);
            this.form.UNPAN_GYOUSHA_NAME.MaxLength = 40;
            this.form.UNPAN_GYOUSHA_NAME.Name = "UNPAN_GYOUSHA_NAME";
            this.form.UNPAN_GYOUSHA_NAME.PopupAfterExecute = null;
            this.form.UNPAN_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.form.UNPAN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.form.UNPAN_GYOUSHA_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.UNPAN_GYOUSHA_NAME.TabIndex = 757;
            this.form.UNPAN_GYOUSHA_NAME.Tag = " ";
            #endregion

            #region 荷降業者
            // add Form
            form.Controls.Add(this.form.LABEL_NIOROSHI_GYOUSHA);
            form.Controls.Add(this.form.NIOROSHI_GYOUSHA_CD);
            form.Controls.Add(this.form.NIOROSHI_GYOUSHA_NAME);

            // 
            // LABEL_NIOROSHI_GYOUSHA
            // 
            this.form.LABEL_NIOROSHI_GYOUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_NIOROSHI_GYOUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_NIOROSHI_GYOUSHA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_NIOROSHI_GYOUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_NIOROSHI_GYOUSHA.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_NIOROSHI_GYOUSHA.Location = new System.Drawing.Point(481, 73);
            this.form.LABEL_NIOROSHI_GYOUSHA.Name = "label4";
            this.form.LABEL_NIOROSHI_GYOUSHA.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_NIOROSHI_GYOUSHA.TabIndex = 764;
            this.form.LABEL_NIOROSHI_GYOUSHA.Text = "荷降業者";
            this.form.LABEL_NIOROSHI_GYOUSHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // NIOROSHI_GYOUSHA_CD
            // 
            this.form.NIOROSHI_GYOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.NIOROSHI_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.NIOROSHI_GYOUSHA_CD.ChangeUpperCase = true;
            this.form.NIOROSHI_GYOUSHA_CD.CharacterLimitList = null;
            this.form.NIOROSHI_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.NIOROSHI_GYOUSHA_CD.DBFieldsName = "";
            this.form.NIOROSHI_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.NIOROSHI_GYOUSHA_CD.DisplayItemName = "";
            this.form.NIOROSHI_GYOUSHA_CD.DisplayPopUp = null;
            this.form.NIOROSHI_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.NIOROSHI_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.form.NIOROSHI_GYOUSHA_CD.GetCodeMasterField = "";
            this.form.NIOROSHI_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
            this.form.NIOROSHI_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.form.NIOROSHI_GYOUSHA_CD.Location = new System.Drawing.Point(591, 73);
            this.form.NIOROSHI_GYOUSHA_CD.MaxLength = 6;
            this.form.NIOROSHI_GYOUSHA_CD.Name = "NIOROSHI_GYOUSHA_CD";
            this.form.NIOROSHI_GYOUSHA_CD.PopupAfterExecute = null;
            this.form.NIOROSHI_GYOUSHA_CD.PopupBeforeExecute = null;
            this.form.NIOROSHI_GYOUSHA_CD.PopupGetMasterField = "";
            this.form.NIOROSHI_GYOUSHA_CD.PopupSetFormField = "";
            this.form.NIOROSHI_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.NIOROSHI_GYOUSHA_CD.PopupWindowName = "";
            this.form.NIOROSHI_GYOUSHA_CD.ReadOnly = true;
            this.form.NIOROSHI_GYOUSHA_CD.SetFormField = "";
            this.form.NIOROSHI_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.form.NIOROSHI_GYOUSHA_CD.TabIndex = 762;
            this.form.NIOROSHI_GYOUSHA_CD.TabStop = false;
            this.form.NIOROSHI_GYOUSHA_CD.Tag = "";
            this.form.NIOROSHI_GYOUSHA_CD.ZeroPaddengFlag = true;

            // 
            // NIOROSHI_GYOUSHA_NAME
            // 
            this.form.NIOROSHI_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.NIOROSHI_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.NIOROSHI_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.NIOROSHI_GYOUSHA_NAME.DBFieldsName = "";
            this.form.NIOROSHI_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.DisplayItemName = "";
            this.form.NIOROSHI_GYOUSHA_NAME.DisplayPopUp = null;
            this.form.NIOROSHI_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.NIOROSHI_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.NIOROSHI_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.NIOROSHI_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.form.NIOROSHI_GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.form.NIOROSHI_GYOUSHA_NAME.Location = new System.Drawing.Point(640, 73);
            this.form.NIOROSHI_GYOUSHA_NAME.MaxLength = 40;
            this.form.NIOROSHI_GYOUSHA_NAME.Name = "NIOROSHI_GYOUSHA_NAME";
            this.form.NIOROSHI_GYOUSHA_NAME.PopupAfterExecute = null;
            this.form.NIOROSHI_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.form.NIOROSHI_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GYOUSHA_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.NIOROSHI_GYOUSHA_NAME.TabIndex = 763;
            this.form.NIOROSHI_GYOUSHA_NAME.Tag = " ";
            #endregion

            #region 荷降現場
            // add Form
            form.Controls.Add(this.form.LABEL_NIOROSHI_GENBA);
            form.Controls.Add(this.form.NIOROSHI_GENBA_CD);
            form.Controls.Add(this.form.NIOROSHI_GENBA_NAME);

            // 
            // LABEL_NIOROSHI_GENBA
            // 
            this.form.LABEL_NIOROSHI_GENBA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_NIOROSHI_GENBA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_NIOROSHI_GENBA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_NIOROSHI_GENBA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.LABEL_NIOROSHI_GENBA.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_NIOROSHI_GENBA.Location = new System.Drawing.Point(481, 96);
            this.form.LABEL_NIOROSHI_GENBA.Name = "label6";
            this.form.LABEL_NIOROSHI_GENBA.Size = new System.Drawing.Size(108, 20);
            this.form.LABEL_NIOROSHI_GENBA.TabIndex = 770;
            this.form.LABEL_NIOROSHI_GENBA.Text = "荷降現場";
            this.form.LABEL_NIOROSHI_GENBA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // NIOROSHI_GENBA_CD
            // 
            this.form.NIOROSHI_GENBA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.NIOROSHI_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.NIOROSHI_GENBA_CD.ChangeUpperCase = true;
            this.form.NIOROSHI_GENBA_CD.CharacterLimitList = null;
            this.form.NIOROSHI_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.form.NIOROSHI_GENBA_CD.DBFieldsName = "";
            this.form.NIOROSHI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.NIOROSHI_GENBA_CD.DisplayItemName = "";
            this.form.NIOROSHI_GENBA_CD.DisplayPopUp = null;
            this.form.NIOROSHI_GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.NIOROSHI_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.form.NIOROSHI_GENBA_CD.GetCodeMasterField = "";
            this.form.NIOROSHI_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
            this.form.NIOROSHI_GENBA_CD.ItemDefinedTypes = "varchar";
            this.form.NIOROSHI_GENBA_CD.Location = new System.Drawing.Point(591, 96);
            this.form.NIOROSHI_GENBA_CD.MaxLength = 6;
            this.form.NIOROSHI_GENBA_CD.Name = "NIOROSHI_GENBA_CD";
            this.form.NIOROSHI_GENBA_CD.PopupAfterExecute = null;
            this.form.NIOROSHI_GENBA_CD.PopupBeforeExecute = null;
            this.form.NIOROSHI_GENBA_CD.PopupGetMasterField = "";
            this.form.NIOROSHI_GENBA_CD.PopupSetFormField = "";
            this.form.NIOROSHI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.NIOROSHI_GENBA_CD.PopupWindowName = "";
            this.form.NIOROSHI_GENBA_CD.ReadOnly = true;
            this.form.NIOROSHI_GENBA_CD.SetFormField = "";
            this.form.NIOROSHI_GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.form.NIOROSHI_GENBA_CD.TabIndex = 768;
            this.form.NIOROSHI_GENBA_CD.TabStop = false;
            this.form.NIOROSHI_GENBA_CD.Tag = "";
            this.form.NIOROSHI_GENBA_CD.ZeroPaddengFlag = true;

            // 
            // NIOROSHI_GENBA_NAME
            // 
            this.form.NIOROSHI_GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.form.NIOROSHI_GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.NIOROSHI_GENBA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.form.NIOROSHI_GENBA_NAME.DBFieldsName = "";
            this.form.NIOROSHI_GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.NIOROSHI_GENBA_NAME.DisplayItemName = "";
            this.form.NIOROSHI_GENBA_NAME.DisplayPopUp = null;
            this.form.NIOROSHI_GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.NIOROSHI_GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.form.NIOROSHI_GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.NIOROSHI_GENBA_NAME.IsInputErrorOccured = false;
            this.form.NIOROSHI_GENBA_NAME.ItemDefinedTypes = "varchar";
            this.form.NIOROSHI_GENBA_NAME.Location = new System.Drawing.Point(640, 96);
            this.form.NIOROSHI_GENBA_NAME.MaxLength = 40;
            this.form.NIOROSHI_GENBA_NAME.Name = "NIOROSHI_GENBA_NAME";
            this.form.NIOROSHI_GENBA_NAME.PopupAfterExecute = null;
            this.form.NIOROSHI_GENBA_NAME.PopupBeforeExecute = null;
            this.form.NIOROSHI_GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.NIOROSHI_GENBA_NAME.ReadOnly = true;
            this.form.NIOROSHI_GENBA_NAME.Size = new System.Drawing.Size(287, 20);
            this.form.NIOROSHI_GENBA_NAME.TabIndex = 769;
            this.form.NIOROSHI_GENBA_NAME.Tag = " ";
            #endregion

            #region 種類検索条件 20250324

            form.Controls.Add(this.form.SHURUI_KENSAKU_JOKEN_CBB);

            //
            //SHURUI_KENSAKU_JOKEN_CBB
            //
            this.form.SHURUI_KENSAKU_JOKEN_CBB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.BackColor = System.Drawing.SystemColors.Window;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.form.SHURUI_KENSAKU_JOKEN_CBB.DBFieldsName = "SHURUI_KENSAKU_JOKEN_CBB";
            this.form.SHURUI_KENSAKU_JOKEN_CBB.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.DisplayItemName = "種類検索条件";
            this.form.SHURUI_KENSAKU_JOKEN_CBB.DisplayPopUp = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB.FocusOutCheckMethod")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.SHURUI_KENSAKU_JOKEN_CBB.FormattingEnabled = true;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.IsInputErrorOccured = false;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.ItemDefinedTypes = "varchar";
            //this.form.SHURUI_KENSAKU_JOKEN_CBB.Items.AddRange(new object[] {
            //""
            //});
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Location = new System.Drawing.Point(125, 120);
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.form.SHURUI_KENSAKU_JOKEN_CBB.MaxLength = 20;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Name = "SHURUI_KENSAKU_JOKEN_CBB";
            this.form.SHURUI_KENSAKU_JOKEN_CBB.PopupAfterExecute = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.PopupBeforeExecute = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB.PopupSearchSendParams")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.SHURUI_KENSAKU_JOKEN_CBB.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB.popupWindowSetting")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB.RegistCheckMethod")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB.ShortItemName = "種類検索条件";
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Size = new System.Drawing.Size(170, 24);
            this.form.SHURUI_KENSAKU_JOKEN_CBB.TabIndex = 0;
            //20250325
            this.form.SHURUI_KENSAKU_JOKEN_CBB.Validating += new CancelEventHandler(this.form.SHURUI_KENSAKU_JOKEN_CBB_Validating);

            #endregion

            #region 伝票区分

            // add Form
            this.form.Controls.Add(this.form.LABEL_DENPYOU_KBN);
            this.form.Controls.Add(this.form.PANEL_DENPYOU_KBN);

            // 
            // LABEL_DENPYOU_KBN
            // 
            this.form.LABEL_DENPYOU_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.form.LABEL_DENPYOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.LABEL_DENPYOU_KBN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.form.LABEL_DENPYOU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.LABEL_DENPYOU_KBN.ForeColor = System.Drawing.Color.White;
            this.form.LABEL_DENPYOU_KBN.Location = new System.Drawing.Point(14, 169);
            this.form.LABEL_DENPYOU_KBN.Name = "LABEL_DENPYOU_KBN";
            this.form.LABEL_DENPYOU_KBN.Size = new System.Drawing.Size(108, 22);
            this.form.LABEL_DENPYOU_KBN.TabIndex = 772;
            this.form.LABEL_DENPYOU_KBN.Text = "伝票区分";
            this.form.LABEL_DENPYOU_KBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // PANEL_DENPYOU_KBN
            // 
            this.form.PANEL_DENPYOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.PANEL_DENPYOU_KBN.Controls.Add(this.form.DENPYOU_KBN_3);
            this.form.PANEL_DENPYOU_KBN.Controls.Add(this.form.DENPYOU_KBN_2);
            this.form.PANEL_DENPYOU_KBN.Controls.Add(this.form.DENPYOU_KBN_1);
            this.form.PANEL_DENPYOU_KBN.Controls.Add(this.form.DENPYOU_KBN);
            this.form.PANEL_DENPYOU_KBN.Location = new System.Drawing.Point(126, 169);
            this.form.PANEL_DENPYOU_KBN.Name = "PANEL_DENPYOU_KBN";
            this.form.PANEL_DENPYOU_KBN.Size = new System.Drawing.Size(270, 20);
            this.form.PANEL_DENPYOU_KBN.TabIndex = 4;

            // 
            // DENPYOU_KBN_3
            // 
            this.form.DENPYOU_KBN_3.AutoSize = true;
            this.form.DENPYOU_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DENPYOU_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_3.FocusOutCheckMethod")));
            this.form.DENPYOU_KBN_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DENPYOU_KBN_3.LinkedTextBox = "DENPYOU_KBN";
            this.form.DENPYOU_KBN_3.Location = new System.Drawing.Point(187, 1);
            this.form.DENPYOU_KBN_3.Name = "DENPYOU_KBN_3";
            this.form.DENPYOU_KBN_3.PopupAfterExecute = null;
            this.form.DENPYOU_KBN_3.PopupBeforeExecute = null;
            this.form.DENPYOU_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_3.PopupSearchSendParams")));
            this.form.DENPYOU_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DENPYOU_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_3.popupWindowSetting")));
            this.form.DENPYOU_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_3.RegistCheckMethod")));
            this.form.DENPYOU_KBN_3.Size = new System.Drawing.Size(74, 17);
            this.form.DENPYOU_KBN_3.TabIndex = 494;
            this.form.DENPYOU_KBN_3.Tag = "伝票区分を全ての場合にはチェックを付けてください";
            this.form.DENPYOU_KBN_3.Text = "3. 全て";
            this.form.DENPYOU_KBN_3.UseVisualStyleBackColor = true;
            this.form.DENPYOU_KBN_3.Value = "3";
            // 
            // DENPYOU_KBN_2
            // 
            this.form.DENPYOU_KBN_2.AutoSize = true;
            this.form.DENPYOU_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DENPYOU_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_2.FocusOutCheckMethod")));
            this.form.DENPYOU_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DENPYOU_KBN_2.LinkedTextBox = "DENPYOU_KBN";
            this.form.DENPYOU_KBN_2.Location = new System.Drawing.Point(111, 1);
            this.form.DENPYOU_KBN_2.Name = "DENPYOU_KBN_2";
            this.form.DENPYOU_KBN_2.PopupAfterExecute = null;
            this.form.DENPYOU_KBN_2.PopupBeforeExecute = null;
            this.form.DENPYOU_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_2.PopupSearchSendParams")));
            this.form.DENPYOU_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DENPYOU_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_2.popupWindowSetting")));
            this.form.DENPYOU_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_2.RegistCheckMethod")));
            this.form.DENPYOU_KBN_2.Size = new System.Drawing.Size(74, 17);
            this.form.DENPYOU_KBN_2.TabIndex = 493;
            this.form.DENPYOU_KBN_2.Tag = "伝票区分を支払の場合にはチェックを付けてください";
            this.form.DENPYOU_KBN_2.Text = "2. 支払";
            this.form.DENPYOU_KBN_2.UseVisualStyleBackColor = true;
            this.form.DENPYOU_KBN_2.Value = "2";
            // 
            // DENPYOU_KBN_1
            // 
            this.form.DENPYOU_KBN_1.AutoSize = true;
            this.form.DENPYOU_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DENPYOU_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_1.FocusOutCheckMethod")));
            this.form.DENPYOU_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DENPYOU_KBN_1.LinkedTextBox = "DENPYOU_KBN";
            this.form.DENPYOU_KBN_1.Location = new System.Drawing.Point(39, 1);
            this.form.DENPYOU_KBN_1.Name = "DENPYOU_KBN_1";
            this.form.DENPYOU_KBN_1.PopupAfterExecute = null;
            this.form.DENPYOU_KBN_1.PopupBeforeExecute = null;
            this.form.DENPYOU_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_1.PopupSearchSendParams")));
            this.form.DENPYOU_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DENPYOU_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_1.popupWindowSetting")));
            this.form.DENPYOU_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_1.RegistCheckMethod")));
            this.form.DENPYOU_KBN_1.Size = new System.Drawing.Size(74, 17);
            this.form.DENPYOU_KBN_1.TabIndex = 489;
            this.form.DENPYOU_KBN_1.Tag = "伝票区分を売上の場合にはチェックを付けてください";
            this.form.DENPYOU_KBN_1.Text = "1. 売上";
            this.form.DENPYOU_KBN_1.UseVisualStyleBackColor = true;
            this.form.DENPYOU_KBN_1.Value = "1";
            // 
            // DENPYOU_KBN
            // 
            this.form.DENPYOU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.form.DENPYOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.form.DENPYOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.DENPYOU_KBN.DisplayPopUp = null;
            this.form.DENPYOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN.FocusOutCheckMethod")));
            this.form.DENPYOU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.form.DENPYOU_KBN.ForeColor = System.Drawing.Color.Black;
            this.form.DENPYOU_KBN.IsInputErrorOccured = false;
            this.form.DENPYOU_KBN.LinkedRadioButtonArray = new string[] {
        "DENPYOU_KBN_1",
        "DENPYOU_KBN_2",
        "DENPYOU_KBN_3"};
            this.form.DENPYOU_KBN.Location = new System.Drawing.Point(3, -1);
            this.form.DENPYOU_KBN.Name = "DENPYOU_KBN";
            this.form.DENPYOU_KBN.PopupAfterExecute = null;
            this.form.DENPYOU_KBN.PopupBeforeExecute = null;
            this.form.DENPYOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN.PopupSearchSendParams")));
            this.form.DENPYOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.DENPYOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.form.DENPYOU_KBN.RangeSetting = rangeSettingDto1;
            this.form.DENPYOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN.RegistCheckMethod")));
            this.form.DENPYOU_KBN.Size = new System.Drawing.Size(33, 20);
            this.form.DENPYOU_KBN.TabIndex = 1;
            this.form.DENPYOU_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.form.DENPYOU_KBN.WordWrap = false;
            #endregion
        }
        #endregion

        //20250325
        public void GetListCbbShuruiName()
        {
            try
            {
                this.SearchResult = this.shuruiDao.GetAllDataForCbb();

                DataRow emptyRow = this.SearchResult.NewRow();
                emptyRow["SHURUI_NAME_RYAKU"] = "";
                emptyRow["SHURUI_CD"] = "";
                this.SearchResult.Rows.InsertAt(emptyRow, 0);

                switch (this.form.WindowId)
                {
                    case WINDOW_ID.M_HINMEI:
                        this.form.SHURUI_KENSAKU_JOKEN_CBB1.DataSource = this.SearchResult;
                        this.form.SHURUI_KENSAKU_JOKEN_CBB1.DisplayMember = "SHURUI_NAME_RYAKU";
                        this.form.SHURUI_KENSAKU_JOKEN_CBB1.ValueMember = "SHURUI_CD";
                        break;

                    case WINDOW_ID.M_HINMEI_SEARCH:
                        this.form.SHURUI_KENSAKU_JOKEN_CBB.DataSource = this.SearchResult;
                        this.form.SHURUI_KENSAKU_JOKEN_CBB.DisplayMember = "SHURUI_NAME_RYAKU";
                        this.form.SHURUI_KENSAKU_JOKEN_CBB.ValueMember = "SHURUI_CD";
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
            }
        }

        //20250326
        public void ChangeLayoutForHimei()
        {
            this.form.panel1.Visible = false;

            this.form.SHURUI_KENSAKU_JOKEN_CBB1 = new r_framework.CustomControl.CustomComboBox();

            // Location用
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KensakuKyoutsuuPopupForMultiKeyForm));


            #region 種類検索条件 20250326

            form.Controls.Add(this.form.SHURUI_KENSAKU_JOKEN_CBB1);

            //
            //SHURUI_KENSAKU_JOKEN_CBB1
            //
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.BackColor = System.Drawing.SystemColors.Window;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.DBFieldsName = "SHURUI_KENSAKU_JOKEN_CBB1";
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.DefaultBackColor = System.Drawing.Color.Empty;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.DisplayItemName = "種類検索条件";
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.DisplayPopUp = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB.FocusOutCheckMethod")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.FormattingEnabled = true;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.IsInputErrorOccured = false;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.ItemDefinedTypes = "varchar";
            //this.form.SHURUI_KENSAKU_JOKEN_CBB1.Items.AddRange(new object[] {
            //""
            //});
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Location = new System.Drawing.Point(126, 60);
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.MaxLength = 20;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Name = "SHURUI_KENSAKU_JOKEN_CBB1";
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.PopupAfterExecute = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.PopupBeforeExecute = null;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB1.PopupSearchSendParams")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB1.popupWindowSetting")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_KENSAKU_JOKEN_CBB1.RegistCheckMethod")));
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.ShortItemName = "種類検索条件";
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Size = new System.Drawing.Size(170, 24);
            this.form.SHURUI_KENSAKU_JOKEN_CBB1.TabIndex = 0;

            this.form.SHURUI_KENSAKU_JOKEN_CBB1.Validating += new CancelEventHandler(this.form.SHURUI_KENSAKU_JOKEN_CBB1_Validating);

            #endregion
        }

    }
}