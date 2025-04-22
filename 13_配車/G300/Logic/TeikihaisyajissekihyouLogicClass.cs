using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;
using System.Collections;
using CommonChouhyouPopup.App;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.Dto;

namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class TeikihaisyajissekihyouLogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// UIForm
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// DTO
        /// </summary>
        private TeikihaisyajissekihyouDTOClass dto;

        /// <summary>
        /// 定期配車実績表のDao
        /// </summary>
        private TeikihaisyajissekihyouDAOClass dao;

        /// <summary>
        /// 会社名前のDao
        /// </summary>
        private IM_CORP_INFODao daoCorp;

        /// <summary>
        /// 検索条件
        /// </summary>
        public TeikihaisyajissekihyouDTOClass SearchString { get; set; }

        /// <summary>
        /// 月報詳細内容検索結果
        /// </summary>
        public DataTable SearchDetailResult;

        /// <summary>
        /// 年報詳細内容検索結果
        /// </summary>
        public DataTable SearchDetailResult_Y;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        //出力区分変量を設定

        private string syutsuRyouku_KBN;

        // 業者CD、業者名、現場CD、場名のタイトルを設定のため
        private static readonly string SearchConditionHeader = "業者CD,業者名,現場CD,現場名";

        // 廃棄物種類のタイトルを設定のため
        private static readonly string DetailHeader = ",品名";

        // 日付のタイトルを設定のため
        private static readonly string DetailDateAndUnit = "日付,単位";

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.Teikihaisyajissekihyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        private string sysFormat = "#,###";
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TeikihaisyajissekihyouLogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                // dto initial
                this.dto = new TeikihaisyajissekihyouDTOClass();

                // dao initial
                this.dao = DaoInitUtility.GetComponent<TeikihaisyajissekihyouDAOClass>();

                // 会社名dao initial
                this.daoCorp = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                msgLogic = new MessageBoxShowLogic();

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion Constructor

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 画面初期表示設定
                this.InitializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                // サブファンクション非表示
                this.parentForm.ProcessButtonPanel.Visible = false;
                this.allControl = this.form.allControl;

                //拠点CD
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                IM_KYOTENDao kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                var kyotenP = kyotenDao.GetDataByCd(KYOTEN_CD);
                //拠点名称
                if (kyotenP != null && KYOTEN_CD != string.Empty)
                {
                    this.form.txt_KyotenCD.Text = KYOTEN_CD.PadLeft(this.form.txt_KyotenCD.MaxLength, '0'); ;
                    this.form.txt_KyotenName.Text = kyotenP.KYOTEN_NAME_RYAKU;
                }
                else
                {
                    //拠点CD、拠点 : ブランク
                    this.form.txt_KyotenCD.Text = string.Empty;
                    this.form.txt_KyotenName.Text = string.Empty;
                }

                this.form.SHIMEBI.Text = String.Empty;

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen()
        {
            //「出力区分」／月報を選択する
            this.form.txt_Shuturyokukubun.Text = "1";

            //「期間From」／システム日付
            this.form.dtp_KikanFrom.Value = parentForm.sysDate;

            //「期間To」／作業開始日
            this.form.dtp_KikanTo.Value = parentForm.sysDate;

            this.form.TORIHIKISAKI_CD_From.Text = String.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU_From.Text = String.Empty;
            this.form.TORIHIKISAKI_CD_To.Text = String.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU_To.Text = String.Empty;

            //「業者CD1 From」／空白にする
            this.form.GYOUSHA_CD_From.Text = "";

            //「業者名1 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_From.Text = "";

            //「業者CD2 From」／空白にする
            this.form.GYOUSHA_CD_To.Text = "";

            //「業者名2 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_To.Text = "";

            //「現場CD1 From」／空白にする
            this.form.GENBA_CD_From.Text = "";

            //「現場名2 From」／空白にする
            this.form.GENBA_NAME_RYAKU_From.Text = "";

            //「現場CD1 From」／空白にする
            this.form.GENBA_CD_To.Text = "";

            //「現場名2 From」／空白にする
            this.form.GYOUSHA_NAME_RYAKU_To.Text = "";

            this.form.SHURUI_CD_From.Text = String.Empty;
            this.form.SHURUI_NAME_RYAKU_From.Text = String.Empty;
            this.form.SHURUI_CD_To.Text = String.Empty;
            this.form.SHURUI_NAME_RYAKU_To.Text = String.Empty;

            //「集計対象数量」／実績数量を選択する
            this.form.txt_Shuukeisuuryou.Text = "1";
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタンイベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {

            //2014/01/15 削除 qiao start
            //// 「F5印刷ボタン」初期状態では非アクティブとする
            //parentForm.bt_func5.Enabled = false;
            //// 「F6CSV出力ボタン」初期状態では非アクティブとする
            //parentForm.bt_func6.Enabled = false;
            //2014/01/15 削除 qiao end

            //2014/01/15 修正 qiao start
            // 「Ｆ5 帳票印刷ボタン」イベントのイベント生成
            this.form.C_Regist(parentForm.bt_func5);
            parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);

            // 「Ｆ6 CSV出力ボタン」イベントのイベント生成
            this.form.C_Regist(parentForm.bt_func6);
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

            // 「Ｆ9 実行ボタン」イベントのイベント生成
            //this.form.C_Regist(parentForm.bt_func9);
            //parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
            //2014/01/15 修正 qiao end

            // 「Ｆ12 ﾃﾞｰﾀ出力ボタン」イベントのイベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            // 20141128 teikyou ダブルクリックを追加する　start
            this.form.dtp_KikanTo.MouseDoubleClick += new MouseEventHandler(dtp_KikanTo_MouseDoubleClick);
            this.form.TORIHIKISAKI_CD_To.MouseDoubleClick += new MouseEventHandler(TORIHIKISAKI_CD_To_MouseDoubleClick);
            this.form.GYOUSHA_CD_To.MouseDoubleClick += new MouseEventHandler(GYOUSHA_CD_To_MouseDoubleClick);
            this.form.GENBA_CD_To.MouseDoubleClick += new MouseEventHandler(GENBA_CD_To_MouseDoubleClick);
            this.form.SHURUI_CD_To.MouseDoubleClick += new MouseEventHandler(SHURUI_CD_To_MouseDoubleClick);
            // 20141128 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　start
            this.form.dtp_KikanFrom.Leave += new System.EventHandler(dtp_KikanFrom_Leave);
            this.form.dtp_KikanTo.Leave += new System.EventHandler(dtp_KikanTo_Leave);
            /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　end

        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        /// <summary>
        /// 会社名を取得
        /// </summary>
        /// <returns></returns>
        private string GetCorpName()
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                M_CORP_INFO condition = new M_CORP_INFO();
                condition.SYS_ID = 0;
                //会社名を検索結果を取得
                var entity = this.daoCorp.GetAllValidData(condition);
                if (entity != null && entity.Length > 0)
                {
                    returnVal = entity[0].CORP_NAME;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        #region 実行処理
        /// <summary>
        /// 実行処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件を設定する
                SetSearchString();

                string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();
                if (csvKb.Equals("1"))
                {
                    syutsuRyouku_KBN = "1";
                    // 月報の明細検索結果取得
                    this.SearchDetailResult = this.dao.GetReportDetailDataByMonth(this.SearchString);
                    //月報の 数量合計結果取得
                    //this.SearchResult = this.dao.GetReportDataByMonth(this.SearchString);
                    // 検索結果件数
                    result = this.SearchDetailResult.Rows.Count;
                }
                if (csvKb.Equals("2"))
                {

                    syutsuRyouku_KBN = "2";
                    // 年報の明細検索結果取得
                    this.SearchDetailResult_Y = this.dao.GetReportDetailDataByYear(this.SearchString);

                    result = this.SearchDetailResult_Y.Rows.Count;
                }

                return result;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region 月報CSV出力
        /// <summary>
        /// 月報CSV出力
        /// </summary>
        void monthCsvOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(月報)_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(月報)_" + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        var resultByMonthGG2 = SearchDetailResult.AsEnumerable()
                            .GroupBy(r => string.Format("{0}{1}",
                                        r.Field<string>("GYOUSHA_CD"),
                                        r.Field<string>("GENBA_CD")),
                                    (yy, ymggGroup) => new
                                    {
                                        yy,         
                                        ymggGroup
                                    }
                                    ).ToList();

                        foreach (var grpYMGG2 in resultByMonthGG2)
                        {
                            DateTime kjnDt = DateTime.Parse(this.form.dtp_KikanFrom.Text);
                            DateTime kikanToDt = DateTime.Parse(this.form.dtp_KikanTo.Text);
                            // 期間From から1ヶ月毎単位にデータを抽出して出力
                            do
                            {
                                // 基準日をもとに、１ヶ月間の開始日～終了日を定義
                                DateTime fromDt = kjnDt;
                                DateTime toDt = kjnDt.AddMonths(1);

                                if (kikanToDt < toDt)
                                {
                                    // 期間TOよりも大きい場合、終了日を期間TOで更新
                                    toDt = kikanToDt.AddDays(1);
                                }

                                // 該当期間中の作業日から、業者・現場でグループ化したデータ取得
                                var resultByMonthGG = SearchDetailResult.AsEnumerable()
                                    .Where(r => (fromDt <= r.Field<DateTime>("SAGYOU_DATE") && r.Field<DateTime>("SAGYOU_DATE") < toDt) && r.Field<string>("GYOUSHA_CD") + r.Field<string>("GENBA_CD") == grpYMGG2.yy)
                                    .GroupBy(r => string.Format("{0},{1},{2},{3}",
                                                r.Field<string>("GYOUSHA_CD"),
                                                r.Field<string>("GYOUSHA_NAME"),
                                                r.Field<string>("GENBA_CD"),
                                                r.Field<string>("GENBA_NAME")),
                                            (gg, ymggGroup) => new
                                            {
                                                gg,         // 業者CD,業者名,現場CD,現場名
                                                ymggGroup   // SearchDetailResultのレコード値
                                            }
                                            ).ToList();

                                // 「業者CD,業者名,現場CD,現場名」毎
                                foreach (var grpYMGG in resultByMonthGG)
                                {
                                    var grp = grpYMGG.ymggGroup;

                                    // 固定項目名前を出力する
                                    sw.WriteLine(SearchConditionHeader);
                                    // 業者CD、業者名、現場CD、現場名を書き込み
                                    sw.WriteLine(grpYMGG.gg);

                                    sw.WriteLine();

                                    //品名と単位名を取るのために、キーを設定
                                    var hinmeis = grp.GroupBy(r => new
                                    {
                                        HINMEI_CD = r.Field<string>("HINMEI_CD"),
                                        UNIT_CD = r.Field<short>("UNIT_CD"),
                                        UNIT_ORDER = r.Field<int>("UNIT_ORDER"),
                                    },
                                        (k, g) => new
                                        {
                                            HINMEI_CD = k.HINMEI_CD,
                                            HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                            UNIT_CD = k.UNIT_CD,
                                            //UNIT_NAME = g.First().Field<string>("UNIT_NAME"),
                                            UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                            GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                            GENBA_CD = g.First().Field<string>("GENBA_CD"),
                                            UNIT_ORDER = k.UNIT_ORDER,
                                        }).OrderBy(r => r.HINMEI_CD)
                                        .ThenBy(r => r.UNIT_ORDER)
                                        .ThenBy(r => r.UNIT_CD)
                                        .ToList();

                                    // カラムヘッダを書き込む
                                    // 品名
                                    var strHinmeiName = string.Join(",", hinmeis.Select(r => r.HINMEI_NAME));
                                    // 単位
                                    var strUnitName = string.Join(",", hinmeis.Select(r => r.UNIT_NAME));
                                    //項目、品名を書き込む
                                    sw.WriteLine(DetailHeader + "," + strHinmeiName);
                                    //項目、単位を書き込む
                                    sw.WriteLine(DetailDateAndUnit + "," + strUnitName);

                                    // 日数取得
                                    var days = (toDt - fromDt).Days;

                                    //毎日を取るのために、メソッドを設定する
                                    // 基準日をもとに日数分を足しこみ、期間のリストを作成
                                    var dates = Enumerable.Range(0, days)
                                        .Select(n => fromDt.AddDays(n))
                                        .Select(d => new { DATE = d, DATE_DAY = d.ToString("yyyy/MM/dd(ddd)") }).ToList();

                                    //日付、品名CD、業者CD、現場CDでをキーを設定する
                                    //毎日廃棄物数量データある、取るのデータを書き込む。データない場合、0を書き込む
                                    var result =
                                        dates.Select(d =>
                                            new
                                            {
                                                d.DATE,
                                                d.DATE_DAY,
                                                VALUES = hinmeis.Select(h =>
                                                    grp.Where(
                                                        r => r.Field<DateTime>("SAGYOU_DATE") == d.DATE &&
                                                        r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                        r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                        r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                        r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                                    ).Sum(r3 => r3.Field<decimal>("Expr1"))
                                                ),
                                                VALUES_COUNT = hinmeis.Select(h =>
                                                    grp.Where(
                                                        r => r.Field<DateTime>("SAGYOU_DATE") == d.DATE &&
                                                        r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                        r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                        r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                        r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                                    ).ToList().Count
                                                )
                                            }
                                        ).Select(r =>
                                            new
                                            {
                                                r.DATE,
                                                r.DATE_DAY,
                                                r.VALUES,
                                                //VALUES_STR = r.VALUES.Select(r2 => string.Format("{0:F2}", r2)).ToList(),
                                                VALUES_STR = r.VALUES.Select(r2 => r2.ToString(sysFormat)).ToList(),
                                                VALUES_COUNT = r.VALUES_COUNT.Select(r3 => r3.ToString()).ToList(),
                                            }
                                        ).ToList();
                                    //一ヶ月の廃棄物数量を合計する
                                    var sums = result.Select(r => r.VALUES)
                                        .Aggregate((acc, r) => acc.Zip(r, (v1, v2) => v1 + v2))
                                        //.Select(r => string.Format("{0:F2}", r)).ToList();
                                       .Select(r => r.ToString(sysFormat)).ToList();

                                    // 毎月で毎日の廃棄物数量を書き込む
                                    foreach (var d in result)
                                    {
                                        for (int i = 0; i < d.VALUES_STR.Count; i++)
                                        {
                                            // 0入力と空を区別するために
                                            // VALUEの要素数が0なら空白、それ以外は値を設定
                                            string valueStr = string.Empty;

                                            if (0 < int.Parse(d.VALUES_COUNT[i]))
                                            {
                                                // VALUEの要素数が1以上なら値を設定する

                                                // カンマ区切りがある数量値の場合、
                                                // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                                valueStr = "\"" + d.VALUES_STR[i] + "\"";
                                            }

                                            d.VALUES_STR[i] = valueStr;
                                        }

                                        var strQulity = d.DATE_DAY + "," + "," + string.Join(",", d.VALUES_STR);
                                        sw.WriteLine(strQulity);
                                    }
                                    sw.WriteLine();

                                    for (int i = 0; i < sums.Count; i++)
                                    {
                                        // カンマ区切りがある数量値の場合、
                                        // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                        sums[i] = "\"" + sums[i] + "\"";
                                    }

                                    // 一ヶ月の廃棄物数量合計を書き込む
                                    sw.WriteLine("合計," + "," + string.Join(",", sums));

                                    sw.WriteLine();
                                }

                                kjnDt = kjnDt.AddMonths(1);
                            }
                            while (kjnDt <= kikanToDt);    // 期間Toより大きくなったら終了
                        }
                    }

                    msgLogic.MessageBoxShow("I000", "CSV出力");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("monthCsvOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 年報CSV出力
        /// <summary>
        /// 年報CSV出力
        /// </summary>

        void yearCSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(年報)_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "(年報)_" + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        // 年月明細
                        var resultByGG = SearchDetailResult_Y.AsEnumerable()
                            .GroupBy(
                                r => string.Format("{0},{1},{2},{3}",
                                            r.Field<string>("GYOUSHA_CD"),
                                            r.Field<string>("GYOUSHA_NAME"),
                                            r.Field<string>("GENBA_CD"),
                                            r.Field<string>("GENBA_NAME"))).ToList();

                        //業者、現場より、ループ
                        foreach (var grp in resultByGG)
                        {
                            // ヘーダ固定項目名前を出力する
                            sw.WriteLine(SearchConditionHeader);
                            // 業者CD、業者名、現場CD、現場名を書き込み
                            sw.WriteLine(grp.Key);

                            sw.WriteLine();

                            //品名と単位名を取るのために、キーを設定
                            var hinmeis = grp.GroupBy(r => new
                            {
                                HINMEI_CD = r.Field<string>("HINMEI_CD"),
                                UNIT_CD = r.Field<short>("UNIT_CD"),
                                UNIT_ORDER = r.Field<int>("UNIT_ORDER"),
                            },
                                (k, g) => new
                                {
                                    HINMEI_CD = k.HINMEI_CD,
                                    HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                    UNIT_CD = k.UNIT_CD,
                                    UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                    GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                    GENBA_CD = g.First().Field<string>("GENBA_CD"),
                                    UNIT_ORDER = k.UNIT_ORDER,
                                })
                                .OrderBy(r => r.HINMEI_CD)
                                .ThenBy(r => r.UNIT_ORDER)
                                .ThenBy(r => r.UNIT_CD)
                                .ToList();

                            // カラムヘッダを書き込む
                            // 品名
                            var strHinmeiName = string.Join(",", hinmeis.Select(r => r.HINMEI_NAME));
                            // 単位
                            var strUnitName = string.Join(",", hinmeis.Select(r => r.UNIT_NAME));
                            //項目名、品名を書き込む
                            sw.WriteLine(DetailHeader + "," + strHinmeiName);
                            //項目名、単位を書き込む
                            sw.WriteLine(DetailDateAndUnit + "," + strUnitName);

                            //recordを取るのために、メソッドを設定する
                            //var dates = grp.Select(r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7)).Distinct().ToList();
                            var ymFrom = DateTime.Parse(this.form.dtp_KikanFrom.Text);
                            var ymTo = DateTime.Parse(this.form.dtp_KikanTo.Text);
                            int n1 = ymFrom.Year * 12 + ymFrom.Month - 1, n2 = ymTo.Year * 12 + ymTo.Month - 1;
                            var dates = Enumerable.Range(n1, n2 - n1 + 1)
                                .Select(n => new DateTime(n / 12, n % 12 + 1, 1))
                                .Select(d => new { DATE = d, DATE_DAY = d.ToString("yyyy/MM") }).ToList();

                            //日付、品名CD、単位CD、業者CD、現場CDで条件を設定、数量を取得
                            //毎月廃棄物数量データある、取るのデータを書き込む。データない場合、0を書き込む
                            var result =
                                dates.Select(d =>
                                    new
                                    {
                                        DATE_MONTH = d.DATE_DAY,
                                        VALUES = hinmeis.Select(h =>
                                            grp.Where(
                                                r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7) == d.DATE_DAY.ToString() &&
                                                r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                            ).Sum(r3 => r3.Field<decimal>("Expr1"))
                                        ),
                                        VALUES_COUNT = hinmeis.Select(h =>
                                            grp.Where(
                                                r => r.Field<DateTime>("SAGYOU_DATE").ToString().Substring(0, 7) == d.DATE_DAY.ToString() &&
                                                r.Field<string>("HINMEI_CD").Equals(h.HINMEI_CD) &&
                                                r.Field<short>("UNIT_CD").Equals(h.UNIT_CD) &&
                                                r.Field<string>("GYOUSHA_CD").Equals(h.GYOUSHA_CD) &&
                                                r.Field<string>("GENBA_CD").Equals(h.GENBA_CD)
                                            ).ToList().Count
                                        )
                                    }
                                ).Select(r =>
                                    new
                                    {
                                        r.DATE_MONTH,
                                        r.VALUES,
                                        //VALUES_STR = r.VALUES.Select(r2 => string.Format("{0:F2}", r2)).ToList(),
                                        VALUES_STR = r.VALUES.Select(r2 => r2.ToString(sysFormat)).ToList(),
                                        VALUES_COUNT = r.VALUES_COUNT.Select(r3 => r3.ToString()).ToList(),
                                    }
                                ).ToList();
                            //一年内の廃棄物数量を合計する
                            var sums = result.Select(r => r.VALUES)
                                .Aggregate((acc, r) => acc.Zip(r, (v1, v2) => v1 + v2))
                                //.Select(r => string.Format("{0:F2}", r)).ToList();
                                .Select(r => r.ToString(sysFormat)).ToList();

                            // 毎月の廃棄物数量を書き込む
                            foreach (var d in result)
                            {
                                for (int i = 0; i < d.VALUES_STR.Count; i++)
                                {
                                    // 0入力と空を区別するために
                                    // VALUEの要素数が0なら空白、それ以外は値を設定
                                    string valueStr = string.Empty;

                                    if (0 < int.Parse(d.VALUES_COUNT[i]))
                                    {
                                        // VALUEの要素数が1以上なら値を設定する

                                        // カンマ区切りがある数量値の場合、
                                        // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                        valueStr = "\"" + d.VALUES_STR[i] + "\"";
                                    }

                                    d.VALUES_STR[i] = valueStr;
                                }

                                var strQulity = d.DATE_MONTH + "," + "," + string.Join(",", d.VALUES_STR);
                                sw.WriteLine(strQulity);
                            }
                            sw.WriteLine();

                            for (int i = 0; i < sums.Count; i++)
                            {
                                // カンマ区切りがある数量値の場合、
                                // 項目区切りとしてみなされてしまうため、数量値を""で囲む
                                sums[i] = "\"" + sums[i] + "\"";
                            }

                            // 一年の廃棄物数量合計を書き込む
                            sw.WriteLine("合計," + "," + string.Join(",", sums));

                            sw.WriteLine();
                        }
                        sw.WriteLine();
                    }
                    msgLogic.MessageBoxShow("I000", "CSV出力");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("yearCSVOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region　帳票出力（月報）


        public void invoicePrint(ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region- データ取得処理 -

                // 会社名
                string corpName = this.GetCorpName();

                Dictionary<string, Dictionary<string, DataTable>> DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();

                DateTime kjnDt = DateTime.Parse(this.form.dtp_KikanFrom.Text);
                DateTime kikanToDt = DateTime.Parse(this.form.dtp_KikanTo.Text);

                DataRow rowTmp;
                string ctrlName = string.Empty;
                // 期間From から1ヶ月毎単位にデータを抽出して出力
                do
                {
                    // 基準日をもとに、１ヶ月間の開始日～終了日を定義
                    DateTime fromDt = kjnDt;
                    DateTime toDt = kjnDt.AddMonths(1);

                    if (kikanToDt < toDt)
                    {
                        // 期間TOよりも大きい場合、終了日を期間TOで更新
                        toDt = kikanToDt.AddDays(1);
                    }

                    // 該当期間中の作業日から、業者・現場でグループ化したデータ取得
                    var resultByMonthGG = SearchDetailResult.AsEnumerable()
                        .Where(r => (fromDt <= r.Field<DateTime>("SAGYOU_DATE") && r.Field<DateTime>("SAGYOU_DATE") < toDt))
                        .GroupBy(r => string.Format("{0},{1},{2},{3}",
                                    r.Field<string>("GYOUSHA_CD"),
                                    r.Field<string>("GYOUSHA_NAME"),
                                    r.Field<string>("GENBA_CD"),
                                    r.Field<string>("GENBA_NAME")),
                                (gg, ymggGroup) => new
                                {
                                    gg,         // 業者CD,業者名,現場CD,現場名
                                    ymggGroup   // SearchDetailResultのレコード値
                                }
                                ).ToList();

                    // 「業者CD,業者名,現場CD,現場名」毎
                    foreach (var grpYMGG in resultByMonthGG)
                    {
                        var grp = grpYMGG.ymggGroup;
                        //業者CD、業者名、現場CD、現場名を取得
                        string[] arrTmp = grpYMGG.gg.ToString().Split(',');
                        string gyoushaCD = arrTmp[0];
                        string gyoushaName = arrTmp[1];
                        string genbaCD = arrTmp[2];
                        string genbaName = arrTmp[3];

                        //「キー」の定義
                        string key = string.Format("{0}_{1}_{2}", gyoushaCD, genbaCD, fromDt);

                        DataTablePageList[key] = new Dictionary<string, DataTable>();

                        #region - Header -

                        DataTable dtHeader = new DataTable();
                        dtHeader.TableName = "Header";

                        // 会社名
                        dtHeader.Columns.Add("CORP_RYAKU_NAME");
                        // 業者CD
                        dtHeader.Columns.Add("GYOUSHA_CD");
                        // 業者名
                        dtHeader.Columns.Add("GYOUSYA_NAME");
                        // 現場CD
                        dtHeader.Columns.Add("GENBA_CD");
                        // 現場名
                        dtHeader.Columns.Add("GENBA_NAME");


                        //品名と単位名を取るのため
                        var hinmeis = grp.GroupBy(r => new
                        {
                            HINMEI_CD = r.Field<string>("HINMEI_CD"),
                            UNIT_CD = r.Field<short>("UNIT_CD"),
                            UNIT_ORDER = r.Field<int>("UNIT_ORDER"),
                        },
                            (k, g) => new
                            {
                                HINMEI_CD = k.HINMEI_CD,
                                HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                                UNIT_CD = k.UNIT_CD,
                                UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                                GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                                GENBA_CD = g.First().Field<string>("GENBA_CD"),
                                UNIT_ORDER = k.UNIT_ORDER,
                            }).OrderBy(r => r.HINMEI_CD)
                            .ThenBy(r => r.UNIT_ORDER)
                            .ThenBy(r => r.UNIT_CD)
                            .ToList();

                        //品名と単位のカラムを取得

                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            // 品名
                            ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                            dtHeader.Columns.Add(ctrlName);
                            // 単位
                            ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                            dtHeader.Columns.Add(ctrlName);
                        }

                        rowTmp = dtHeader.NewRow();

                        // 会社名8 
                        //rowTmp["CORP_RYAKU_NAME"] ="";
                        rowTmp["CORP_RYAKU_NAME"] = corpName;

                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = gyoushaCD;
                        // 業者名
                        rowTmp["GYOUSYA_NAME"] = gyoushaName;
                        // 現場CD
                        rowTmp["GENBA_CD"] = genbaCD;
                        // 現場名
                        rowTmp["GENBA_NAME"] = genbaName;

                        //品名と単位の値を取得
                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            // 品名
                            ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                            rowTmp[ctrlName] = hinmeis[i].HINMEI_NAME;
                            // 単位
                            ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                            rowTmp[ctrlName] = hinmeis[i].UNIT_NAME;
                        }

                        dtHeader.Rows.Add(rowTmp);

                        DataTablePageList[key]["Header"] = dtHeader;

                        #endregion - Header -

                        #region - Detail -

                        DataTable dtDetail = new DataTable();
                        dtDetail.TableName = "Detail";

                        // 日数取得
                        var days = (toDt - fromDt).Days;

                        //毎日を取るのために、メソッドを設定する
                        // 基準日をもとに日数分を足しこみ、期間のリストを作成
                        var dates = Enumerable.Range(0, days)
                            .Select(n => fromDt.AddDays(n))
                            .Select(d => new { DATE = d, DATE_DAY = d.ToString("MM/dd(ddd)") }).ToList();

                        // 日付カラムセート
                        dtDetail.Columns.Add("DATE");

                        // 数量カラム作成
                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                            dtDetail.Columns.Add(ctrlName);
                        }

                        // データ作成
                        foreach (var day in dates)
                        {
                            var newRow = dtDetail.NewRow();
                            newRow["DATE"] = day.DATE_DAY;

                            for (int i = 0; i < hinmeis.Count; i++)
                            {
                                var jisseki = grp.Where(r => r.Field<DateTime>("SAGYOU_DATE") == day.DATE
                                                          && r.Field<String>("GYOUSHA_CD") == gyoushaCD
                                                          && r.Field<String>("GENBA_CD") == genbaCD
                                                          && r.Field<String>("HINMEI_CD") == hinmeis[i].HINMEI_CD
                                                          && r.Field<Int16>("UNIT_CD") == hinmeis[i].UNIT_CD);
                                if (jisseki.Count() > 0)
                                {
                                    var suuryou = jisseki.Sum(r => r.Field<decimal>("Expr1"));
                                    ctrlName = String.Format("HINMEI_SURYO_{0}", i + 1);
                                    newRow[ctrlName] = suuryou.ToString(sysFormat);
                                }
                            }

                            dtDetail.Rows.Add(newRow);
                        }

                        DataTablePageList[key]["Detail"] = dtDetail;

                        #endregion - Detail -

                        #region - Footer -

                        DataTable dtFooter = new DataTable();
                        dtFooter.TableName = "Footer";

                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            // 合計カラム作成
                            ctrlName = string.Format("GOUGEI_{0}", j + 1);
                            dtFooter.Columns.Add(ctrlName);
                        }

                        var sumRow = dtFooter.NewRow();
                        for (int j = 0; j < hinmeis.Count; j++)
                        {
                            // 合計計算
                            var sumCtrlName = string.Format("GOUGEI_{0}", j + 1);
                            ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                            var sum = dtDetail.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<String>(ctrlName)))
                                                             .Sum(r => decimal.Parse(r.Field<String>(ctrlName)));
                            sumRow[sumCtrlName] = sum.ToString(sysFormat);
                        }

                        dtFooter.Rows.Add(sumRow);

                        DataTablePageList[key]["Footer"] = dtFooter;

                        #endregion - Footer -
                    }

                    kjnDt = kjnDt.AddMonths(1);
                }
                while (kjnDt <= kikanToDt);    // 期間Toより大きくなったら終了

                reportInfo.DataTablePageList = DataTablePageList;

                #endregion- データ取得処理 -
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #endregion

        #region　帳票出力（年報）


        public void yearInvoicePrint(ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region- データ取得処理 -
                // 会社名
                string corpName = this.GetCorpName();

                DataRow rowTmp;
                string ctrlName = string.Empty;
                //年月ソート順でデータを取得

                // 年月明細
                var resultByGG = SearchDetailResult_Y.AsEnumerable()
                    .GroupBy(
                        r => string.Format("{0},{1},{2},{3}",
                                    r.Field<string>("GYOUSHA_CD"),
                                    r.Field<string>("GYOUSHA_NAME"),
                                    r.Field<string>("GENBA_CD"),
                                    r.Field<string>("GENBA_NAME"))).ToList();


                Dictionary<string, Dictionary<string, DataTable>> DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();

                foreach (var grp in resultByGG)
                {
                    //業者CD、業者名、現場CD、現場名を取得
                    string[] arrTmp = grp.Key.ToString().Split(',');
                    string gyoushaCD = arrTmp[0];
                    string gyoushaName = arrTmp[1];
                    string genbaCD = arrTmp[2];
                    string genbaName = arrTmp[3];
                    //「キー」の定義
                    string dtFrom = this.form.dtp_KikanFrom.Text.Substring(0, 7);
                    string dtTo = this.form.dtp_KikanTo.Text.Substring(0, 7);
                    string ki = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", dtFrom.Substring(0, 4), dtFrom.Substring(5, 2), dtTo.Substring(0, 4), dtTo.Substring(5, 2), gyoushaCD, genbaCD);

                    DataTablePageList[ki] = new Dictionary<string, DataTable>();
                    #region - Header -

                    DataTable dtHeader = new DataTable();
                    dtHeader.TableName = "Header";

                    // 会社名
                    dtHeader.Columns.Add("CORP_RYAKU_NAME");
                    // 業者CD
                    dtHeader.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dtHeader.Columns.Add("GYOUSYA_NAME");
                    // 現場CD
                    dtHeader.Columns.Add("GENBA_CD");
                    // 現場名
                    dtHeader.Columns.Add("GENBA_NAME");


                    //品名と単位名を取るのために、キーを設定
                    var hinmeis = grp.GroupBy(r => new
                    {
                        HINMEI_CD = r.Field<string>("HINMEI_CD"),
                        UNIT_CD = r.Field<short>("UNIT_CD"),
                        UNIT_ORDER = r.Field<int>("UNIT_ORDER"),
                    },
                        (k, g) => new
                        {
                            HINMEI_CD = k.HINMEI_CD,
                            HINMEI_NAME = g.First().Field<string>("HINMEI_NAME"),
                            UNIT_CD = k.UNIT_CD,
                            UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                            GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                            GENBA_CD = g.First().Field<string>("GENBA_CD"),
                            UNIT_ORDER = k.UNIT_ORDER,
                        }).OrderBy(r => r.HINMEI_CD)
                        .ThenBy(r => r.UNIT_ORDER)
                        .ThenBy(r => r.UNIT_CD)
                        .ToList();

                    //品名と単位のカラムを取得

                    for (int i = 0; i < hinmeis.Count; i++)
                    {
                        // 品名
                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                        dtHeader.Columns.Add(ctrlName);
                        // 単位
                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                        dtHeader.Columns.Add(ctrlName);
                    }

                    rowTmp = dtHeader.NewRow();

                    // 会社名
                    rowTmp["CORP_RYAKU_NAME"] = corpName;

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = gyoushaCD;
                    // 業者名
                    rowTmp["GYOUSYA_NAME"] = gyoushaName;
                    // 現場CD
                    rowTmp["GENBA_CD"] = genbaCD;
                    // 現場名
                    rowTmp["GENBA_NAME"] = genbaName;

                    //品名と単位の値を取得
                    for (int i = 0; i < hinmeis.Count; i++)
                    {
                        // 品名
                        ctrlName = string.Format("HINMEI_NANE_{0}", i + 1);
                        rowTmp[ctrlName] = hinmeis[i].HINMEI_NAME;
                        // 単位
                        ctrlName = string.Format("HINMEI_UNIT_NAME_{0}", i + 1);
                        rowTmp[ctrlName] = hinmeis[i].UNIT_NAME;
                    }

                    dtHeader.Rows.Add(rowTmp);

                    DataTablePageList[ki]["Header"] = dtHeader;

                    #endregion - Header -

                    #region - Detail -

                    DataTable dtDetail = new DataTable();
                    dtDetail.TableName = "Detail";

                    //2014/01/14 修正 qiao start
                    //var ymFrom = (DateTime)this.form.dtp_KikanFrom.Value;
                    //var ymTo = (DateTime)this.form.dtp_KikanTo.Value;
                    var ymFrom = DateTime.Parse(this.form.dtp_KikanFrom.Text);
                    var ymTo = DateTime.Parse(this.form.dtp_KikanTo.Text);
                    //2014/01/14 修正 qiao end

                    //recordを取るのために、メソッドを設定する
                    //var dates = grp.Select(r => r.Field<DateTime>("DENPYOU_DATE").ToString().Substring(0, 7)).Distinct().ToList();
                    int n1 = ymFrom.Year * 12 + ymFrom.Month - 1, n2 = ymTo.Year * 12 + ymTo.Month - 1;
                    var dates = Enumerable.Range(n1, n2 - n1 + 1)
                        .Select(n => new DateTime(n / 12, n % 12 + 1, 1))
                        .Select(d => new { DATE = d, DATE_DAY = d.ToString("yyyy/MM") }).ToList();

                    // 日付カラムセート
                    dtDetail.Columns.Add("DATE");

                    // 数量カラム作成
                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                        dtDetail.Columns.Add(ctrlName);
                    }

                    // データ作成
                    foreach (var day in dates)
                    {
                        var newRow = dtDetail.NewRow();
                        newRow["DATE"] = day.DATE_DAY;

                        for (int i = 0; i < hinmeis.Count; i++)
                        {
                            var jisseki = grp.Where(r => r.Field<DateTime>("SAGYOU_DATE").ToString("yyyy/MM") == day.DATE_DAY
                                                      && r.Field<String>("GYOUSHA_CD") == gyoushaCD
                                                      && r.Field<String>("GENBA_CD") == genbaCD
                                                      && r.Field<String>("HINMEI_CD") == hinmeis[i].HINMEI_CD
                                                      && r.Field<Int16>("UNIT_CD") == hinmeis[i].UNIT_CD);
                            if (jisseki.Count() > 0)
                            {
                                var suuryou = jisseki.Sum(r => r.Field<decimal>("Expr1"));
                                ctrlName = String.Format("HINMEI_SURYO_{0}", i + 1);
                                newRow[ctrlName] = suuryou.ToString(sysFormat);
                            }
                        }

                        dtDetail.Rows.Add(newRow);
                    }

                    DataTablePageList[ki]["Detail"] = dtDetail;

                    #endregion - Detail -

                    #region - Footer -

                    DataTable dtFooter = new DataTable();
                    dtFooter.TableName = "Footer";

                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        // 合計カラム作成
                        ctrlName = string.Format("GOUGEI_{0}", j + 1);
                        dtFooter.Columns.Add(ctrlName);
                    }

                    var sumRow = dtFooter.NewRow();
                    for (int j = 0; j < hinmeis.Count; j++)
                    {
                        // 合計計算
                        var sumCtrlName = string.Format("GOUGEI_{0}", j + 1);
                        ctrlName = string.Format("HINMEI_SURYO_{0}", j + 1);
                        var sum = dtDetail.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<String>(ctrlName)))
                                                         .Sum(r => decimal.Parse(r.Field<String>(ctrlName)));
                        sumRow[sumCtrlName] = sum.ToString(sysFormat);
                    }

                    dtFooter.Rows.Add(sumRow);

                    DataTablePageList[ki]["Footer"] = dtFooter;

                    #endregion - Footer -

                }

                reportInfo.DataTablePageList = DataTablePageList;

                #endregion- データ取得処理 -


            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        #endregion 帳票出力（年報）

        #region Days取得処理
        /// <summary>
        /// Days取得処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int getDaysOfMonth(string month)
        {
            try
            {
                LogUtility.DebugMethodStart();
                int days = 0;
                string[] monthItem = month.Split('/');

                days = DateTime.DaysInMonth(int.Parse(monthItem[0]), int.Parse(monthItem[1]));

                return days;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }


        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                TeikihaisyajissekihyouDTOClass searchCondition = new TeikihaisyajissekihyouDTOClass();

                // 拠点
                if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
                {
                    if (this.form.txt_KyotenCD.Text != "99")
                    {
                        searchCondition.KyotenCD = this.form.txt_KyotenCD.Text;
                    }
                }

                // 出力区分
                if (!string.IsNullOrEmpty(this.form.txt_Shuturyokukubun.Text))
                {
                    searchCondition.SYUTSURYOKUKUBUN = this.form.txt_Shuturyokukubun.Text;
                }

                // 期間From
                if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
                {
                    if (this.form.rdo_Nenpou.Checked)
                    {
                        searchCondition.DENPYOU_DATE_FROM = this.form.dtp_KikanFrom.Text + "/" + "01";
                    }
                    else
                    {
                        DateTime dt = DateTime.Parse(this.form.dtp_KikanFrom.Value.ToString());
                        searchCondition.DENPYOU_DATE_FROM = dt.ToString("yyyy/MM/dd");
                    }
                }

                // 期間To
                if (!string.IsNullOrEmpty(this.form.dtp_KikanTo.Text))
                {
                    if (this.form.rdo_Nenpou.Checked)
                    {
                        int days = getDaysOfMonth(this.form.dtp_KikanTo.Text);
                        searchCondition.dtp_KikanTO = this.form.dtp_KikanTo.Text + "/" + days.ToString();
                    }
                    else
                    {
                        DateTime dt = DateTime.Parse(this.form.dtp_KikanTo.Value.ToString());
                        searchCondition.dtp_KikanTO = dt.ToString("yyyy/MM/dd");
                    }
                }

                // 取引先CDFrom
                if (!String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_From.Text))
                {
                    searchCondition.TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_From.Text;
                }

                // 取引先CDTo
                if (!String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_To.Text))
                {
                    searchCondition.TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_To.Text;
                }

                // 業者ＣＤ_From
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_From.Text))
                {
                    searchCondition.GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_From.Text;
                }

                // 業者ＣＤ_To
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_To.Text))
                {
                    searchCondition.GYOUSHA_CD_TO = this.form.GYOUSHA_CD_To.Text;
                }

                // 業者名From
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU_From.Text))
                {
                    searchCondition.GYOUSHA_NAME_RYAKU_FROM = this.form.GYOUSHA_NAME_RYAKU_From.Text;
                }

                // 業者名To
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_NAME_RYAKU_To.Text))
                {
                    searchCondition.GYOUSHA_NAME_RYAKU_TO = this.form.GYOUSHA_NAME_RYAKU_To.Text;
                }

                // 現場ＣＤ_From
                if (!string.IsNullOrEmpty(this.form.GENBA_CD_From.Text))
                {
                    searchCondition.GENBA_CD_FROM = this.form.GENBA_CD_From.Text;
                }

                // 現場ＣＤ_To
                if (!string.IsNullOrEmpty(this.form.GENBA_CD_To.Text))
                {
                    searchCondition.GENBA_CD_TO = this.form.GENBA_CD_To.Text;
                }

                // 現場名From
                if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU_From.Text))
                {
                    searchCondition.GENBA_NAME_RYAKU_FROM = this.form.GENBA_NAME_RYAKU_From.Text;
                }

                // 現場名To
                if (!string.IsNullOrEmpty(this.form.GENBA_NAME_RYAKU_To.Text))
                {
                    searchCondition.GENBA_NAME_RYAKU_TO = this.form.GENBA_NAME_RYAKU_To.Text;
                }

                // 種類CDFrom
                if (!String.IsNullOrEmpty(this.form.SHURUI_CD_From.Text))
                {
                    searchCondition.SHURUI_CD_FROM = this.form.SHURUI_CD_From.Text;
                }

                // 種類CDTo
                if (!String.IsNullOrEmpty(this.form.SHURUI_CD_To.Text))
                {
                    searchCondition.SHURUI_CD_TO = this.form.SHURUI_CD_To.Text;
                }

                // 年
                if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
                {
                    searchCondition.YEAR = this.form.dtp_KikanFrom.Text.Substring(0, 4);
                }

                // 集計対象数量
                if (!string.IsNullOrEmpty(this.form.txt_Shuukeisuuryou.Text))
                {
                    searchCondition.SHUUKEISUURYOU = int.Parse(this.form.txt_Shuukeisuuryou.Text);
                }

                this.SearchString = searchCondition;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }


        }
        #endregion

        #region 検索条件チェック
        /// <summary>
        /// 検索条件チェック
        /// </summary>
        private bool isOKCheck()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 月報期間チェック
                if (int.Parse(this.form.txt_Shuturyokukubun.Text.ToString()) == 1)
                {
                    string yearFrom = this.form.dtp_KikanFrom.Text;
                    string yearTo = this.form.dtp_KikanTo.Text;
                    if (yearFrom.CompareTo(yearTo) == 1)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E043");
                        return returnVal;
                    }
                }

                // 年報期間チェック
                if (int.Parse(this.form.txt_Shuturyokukubun.Text.ToString()) == 2)
                {
                    string yearFrom = this.form.dtp_KikanFrom.Text.Substring(0, 7);
                    DateTime kikanFrom = Convert.ToDateTime(yearFrom);
                    string yearTo = this.form.dtp_KikanTo.Text.Substring(0, 7);
                    DateTime kikanTo = Convert.ToDateTime(yearTo);


                    if (yearFrom.CompareTo(yearTo) == 1)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E043");
                        return returnVal;
                    }

                    if (kikanTo.AddMonths(-12) >= kikanFrom)
                    {//期間に関するエラー時にカーソルを期間Fromへ設定
                        this.form.dtp_KikanFrom.Focus();
                        msgLogic.MessageBoxShow("E002", "期間", "12ヶ月以内の範囲");
                        return returnVal;
                    }

                    //TimeSpan ts = kikanTo - kikanFrom;
                    //int d = ts.Days;
                    //if (d / 365 >= 1 || d / 366 >= 1)
                    //{
                    //    msgLogic.MessageBoxShow("E107", "日付範囲の指定");
                    //    return returnVal;
                    //}
                    //else if (yearFrom.CompareTo(yearTo) == 1)
                    //{
                    //    msgLogic.MessageBoxShow("E043");
                    //    return returnVal;
                    //}


                }
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 「F5 帳票印刷ボタン」イベント処理

        /// <summary>
        /// 「F5 帳票印刷ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // FromToのチェックでエラーになった項目の検証フラグを元に戻す
                this.form.TORIHIKISAKI_CD_From.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_To.CausesValidation = true;
                this.form.GYOUSHA_CD_From.CausesValidation = true;
                this.form.GYOUSHA_CD_To.CausesValidation = true;
                this.form.GENBA_CD_From.CausesValidation = true;
                this.form.GENBA_CD_To.CausesValidation = true;
                this.form.SHURUI_CD_From.CausesValidation = true;
                this.form.SHURUI_CD_To.CausesValidation = true;

                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start

                string csvKb = this.form.txt_Shuturyokukubun.Text;
                if (csvKb.Equals("1"))
                {
                    var count = this.SearchDetailResult.AsEnumerable()
                                                       .GroupBy(n => string.Format("{0},{1}", n.Field<string>("GYOUSHA_CD"), n.Field<string>("GENBA_CD")))
                                                       .Count();

                    // メモリ不足対応のためのチェック。XPSファイル作成時に例外が発生する。
                    if (TeikihaisyajissekihyouConst.MaxPrintGenbaNumber <= count)
                    {
                        // 既定数以上の現場が抽出されたら弾く。
                        msgLogic.MessageBoxShow("E239", "印刷", "現場", TeikihaisyajissekihyouConst.MaxPrintGenbaNumber.ToString());
                        return;
                    }
                }

                ReportInfoBase reportInfo;
                r_framework.Const.WINDOW_ID windowID;

                //string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();

                if (syutsuRyouku_KBN == "1")
                {   // 月報
                    windowID = WINDOW_ID.R_TEIKI_HAISYAHYOU_TSUKI;
                    reportInfo = new ReportInfoR429(windowID);
                    //月報帳票データ
                    invoicePrint(reportInfo);

                    reportInfo.Create(@".\Template\R429_R430-Form.xml", "LAYOUT1", new DataTable());
                }
                else if (syutsuRyouku_KBN == "2")
                {   // 年報
                    windowID = WINDOW_ID.R_TEIKI_HAISYAHYOU_NEN;
                    reportInfo = new ReportInfoR430(windowID);
                    //年報帳票データ
                    yearInvoicePrint(reportInfo);

                    reportInfo.Create(@".\Template\R429_R430-Form.xml", "LAYOUT2", new DataTable());
                }
                else
                {
                    return;
                }

                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfo, windowID))
                {
                    formReportPrintPopup.ReportCaption = String.Empty;
                    if (syutsuRyouku_KBN == "1")
                    {
                        formReportPrintPopup.ReportCaption = "定期配車実績表(月報)";
                    }
                    else if (syutsuRyouku_KBN == "2")
                    {
                        formReportPrintPopup.ReportCaption = "定期配車実績表(年報)";
                    }

                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion 「F5 帳票印刷ボタン」イベント処理

        #region 「F6 CSV出力ボタン」イベント処理
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // FromToのチェックでエラーになった項目の検証フラグを元に戻す
                this.form.TORIHIKISAKI_CD_From.CausesValidation = true;
                this.form.TORIHIKISAKI_CD_To.CausesValidation = true;
                this.form.GYOUSHA_CD_From.CausesValidation = true;
                this.form.GYOUSHA_CD_To.CausesValidation = true;
                this.form.GENBA_CD_From.CausesValidation = true;
                this.form.GENBA_CD_To.CausesValidation = true;
                this.form.SHURUI_CD_From.CausesValidation = true;
                this.form.SHURUI_CD_To.CausesValidation = true;

                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start

                var result = msgLogic.MessageBoxShow("C012");

                if (result == DialogResult.Yes)
                {
                    //string csvKb = this.form.txt_Shuturyokukubun.Text.ToString();

                    TeikihaisyajissekihyouDTOClass searchCondition = new TeikihaisyajissekihyouDTOClass();
                    searchCondition = this.SearchString;

                    if (syutsuRyouku_KBN != string.Empty && syutsuRyouku_KBN == "1")
                    {//出力区分1の場合
                        searchCondition.YEAR = null;

                        monthCsvOutput();

                    }
                    else if (syutsuRyouku_KBN != string.Empty && syutsuRyouku_KBN == "2")
                    {//出力区分２の場合
                        searchCondition.DENPYOU_DATE_FROM = null;
                        searchCondition.dtp_KikanTO = null;
                        yearCSVOutput();
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「Ｆ9実行ボタン」イベント
        /// <summary>
        /// 「Ｆ9実行ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        //private void bt_func9_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        //画面期間は必須入力項目チェック
        //        if (!this.form.registCheck())
        //        {
        //            return;
        //        }

        //        if (!isOKCheck())
        //        {
        //            return;
        //        }
        //        else if (Search() == 0)
        //        {
        //            msgLogic.MessageBoxShow("C001");
        //            //「F5印刷ボタン」初期状態では非活性にする
        //            parentForm.bt_func5.Enabled = false;
        //            // 「F6CSV出力ボタン」初期状態では非活性にする
        //            parentForm.bt_func6.Enabled = false;
        //            return;
        //        }
        //        else
        //        {
        //            //「F5印刷ボタン」初期状態ではアクティブにする
        //            parentForm.bt_func5.Enabled = true;
        //            // 「F6CSV出力ボタン」初期状態ではアクティブにする
        //            parentForm.bt_func6.Enabled = true;
        //        }          

        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}

        /// <summary>
        /// 画面で検索データが変わるの場合、F5（印刷）とF6（CSV出力）ボタンが非活性になる
        /// </summary>
        //public void buttonSeigyou(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);
        //        this.parentForm.bt_func5.Enabled = false;
        //        this.parentForm.bt_func6.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}
        #endregion

        #region 実行
        //2014/01/15 追加 qiao start
        /// <summary>
        /// 「F5印刷」または「F6CSV」押した、データを出力する
        /// </summary>
        /// <returns></returns>
        private bool dataGet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //画面期間は必須入力項目チェック
                if (!this.form.registCheck())
                {
                    return false;
                }

                /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return false;
                }
                /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　end

                if (!isOKCheck())
                {
                    return false;
                }
                else if (Search() == 0)
                {
                    msgLogic.MessageBoxShow("C001");
                    return false;
                }
                else
                {
                    // 最新のSYS_INFOを取得 TODO
                    M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                    if (sysInfo != null && sysInfo.Length > 0)
                    {
                        sysFormat = sysInfo[0].SYS_SUURYOU_FORMAT;
                        return true;
                    }
                    else
                    {
                        sysFormat = "#,###";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        //2014/01/15 追加 qiao end
        #endregion

        #region 「Ｆ12 閉じるボタン」イベント
        /// <summary>
        /// 「Ｆ12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        /// <summary>
        /// 取引先請求マスタを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先請求マスタ</returns>
        internal M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyu(string torihikisakiCd, out bool catchErr)
        {
            M_TORIHIKISAKI_SEIKYUU ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                var torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var torihikisakiList = torihikisakiDao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd });
                if (torihikisakiList.Count() == 1)
                {
                    var torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                    var torihikisakiSeikyuu = torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                    if (torihikisakiSeikyuu != null)
                    {
                        ret = torihikisakiSeikyuu;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiSeikyu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        // 期間のダブルクリック
        private void dtp_KikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var kikanFromTextBox = this.form.dtp_KikanFrom;
            var kikanToTextBox = this.form.dtp_KikanTo;
            kikanToTextBox.Text = kikanFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 取引先のダブルクリック
        private void TORIHIKISAKI_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCDFromTextBox = this.form.TORIHIKISAKI_CD_From;
            var torihikisakiCDToTextBox = this.form.TORIHIKISAKI_CD_To;
            var torihikisakiNameFromTextBox = this.form.TORIHIKISAKI_NAME_RYAKU_From;
            var torihikisakiNameToTextBox = this.form.TORIHIKISAKI_NAME_RYAKU_To;
            torihikisakiCDToTextBox.Text = torihikisakiCDFromTextBox.Text;
            torihikisakiNameToTextBox.Text = torihikisakiNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 業者のダブルクリック
        private void GYOUSHA_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCDFromTextBox = this.form.GYOUSHA_CD_From;
            var gyoushaCDToTextBox = this.form.GYOUSHA_CD_To;
            var gyoushaNameFromTextBox = this.form.GYOUSHA_NAME_RYAKU_From;
            var gyoushaNameToTextBox = this.form.GYOUSHA_NAME_RYAKU_To;
            gyoushaCDToTextBox.Text = gyoushaCDFromTextBox.Text;
            gyoushaNameToTextBox.Text = gyoushaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 現場のダブルクリック
        private void GENBA_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var genbaCDFromTextBox = this.form.GENBA_CD_From;
            var genbaCDToTextBox = this.form.GENBA_CD_To;
            var genbaNameFromTextBox = this.form.GENBA_NAME_RYAKU_From;
            var genbaNameToTextBox = this.form.GENBA_NAME_RYAKU_To;
            genbaCDToTextBox.Text = genbaCDFromTextBox.Text;
            genbaNameToTextBox.Text = genbaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 種類のダブルクリック
        private void SHURUI_CD_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var shuruiCDFromTextBox = this.form.SHURUI_CD_From;
            var shuruiCDToTextBox = this.form.SHURUI_CD_To;
            var shuruiNameFromTextBox = this.form.SHURUI_NAME_RYAKU_From;
            var shuruiNameToTextBox = this.form.SHURUI_NAME_RYAKU_To;
            shuruiCDToTextBox.Text = shuruiCDFromTextBox.Text;
            shuruiNameToTextBox.Text = shuruiNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtp_KikanFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtp_KikanTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtp_KikanTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.dtp_KikanFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.form.dtp_KikanTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtp_KikanFrom.IsInputErrorOccured = true;
                this.form.dtp_KikanTo.IsInputErrorOccured = true;
                this.form.dtp_KikanFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_KikanTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "期間From", "期間To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_KikanFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtp_KikanFrom_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KikanFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KikanTo.Text))
            {
                this.form.dtp_KikanTo.IsInputErrorOccured = false;
                this.form.dtp_KikanTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtp_KikanTo_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KikanTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KikanFrom.Text))
            {
                this.form.dtp_KikanFrom.IsInputErrorOccured = false;
                this.form.dtp_KikanFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「定期配車実績表」の日付チェックを追加する　end

        #region 自動生成（実装なし）
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

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

    }
}
