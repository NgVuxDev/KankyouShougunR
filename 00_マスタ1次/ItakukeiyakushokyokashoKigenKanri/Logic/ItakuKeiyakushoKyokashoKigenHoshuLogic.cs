// $Id: ItakuKeiyakushoKyokashoKigenHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using ItakuKeiyakushoKyokashoKigenHoshu.APP;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;

namespace ItakuKeiyakushoKyokashoKigenHoshu.Logic
{
    /// <summary>
    /// 社員保守画面のビジネスロジック
    /// </summary>
    public class ItakuKeiyakushoKyokashoKigenHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ItakuKeiyakushoKyokashoKigenHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_FUT_ICHIRAN_DATA_SQL = "ItakuKeiyakushoKyokashoKigenHoshu.Sql.GetFutIchiranDataSql.sql";

        private readonly string GET_TOK_ICHIRAN_DATA_SQL = "ItakuKeiyakushoKyokashoKigenHoshu.Sql.GetTokIchiranDataSql.sql";

        /// <summary>
        /// 社員保守画面Form
        /// </summary>
        private ItakuKeiyakushoKyokashoKigenHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 地域別許可証のDao
        /// </summary>
        private IM_CHIIKIBETSU_KYOKADao dao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 地域のDao
        /// </summary>
        private IM_CHIIKIDao chiikiDao;

        internal string befGyoushaCd { get; set; }

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CHIIKIBETSU_KYOKA SearchStringFut { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CHIIKIBETSU_KYOKA SearchStringTok { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ItakuKeiyakushoKyokashoKigenHoshuLogic(ItakuKeiyakushoKyokashoKigenHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_KYOKADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.chiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();

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

                if (!string.IsNullOrEmpty(Properties.Settings.Default.Jigyoushakubun_Text))
                {
                    this.form.Jigyoushakubun.Text = Properties.Settings.Default.Jigyoushakubun_Text;
                }
                else
                {
                    this.form.Jigyoushakubun.Text = "4";
                }
                this.form.GyoushaCode.Text = Properties.Settings.Default.GyoushaCode_Text;
                this.form.GyoushaName.Text = this.GetGyoushaName(Properties.Settings.Default.GyoushaCode_Text);
                this.form.GenbaCode.Text = Properties.Settings.Default.GenbaCode_Text;
                this.form.GenbaName.Text = this.GetGenbaName(Properties.Settings.Default.GyoushaCode_Text, Properties.Settings.Default.GenbaCode_Text);
                this.form.ChiikiCode.Text = Properties.Settings.Default.ChiikiCode_Text;
                this.form.ChiikiName.Text = this.GetChiikiName(Properties.Settings.Default.ChiikiCode_Text);
                this.form.GyouseikyokaKubunCode.Text = Properties.Settings.Default.GyouseikyokaKubunCode_Text;
                if (Properties.Settings.Default["KigenFrom_Value"] != null && !string.IsNullOrWhiteSpace(Properties.Settings.Default.KigenFrom_Value))
                {
                    this.form.KigenFrom.Value = DateTime.Parse(Properties.Settings.Default.KigenFrom_Value);
                }
                else
                {
                    this.form.KigenFrom.Value = null;
                }
                if (Properties.Settings.Default["KigenTo_Value"] != null && !string.IsNullOrWhiteSpace(Properties.Settings.Default.KigenTo_Value))
                {
                    this.form.KigenTo.Value = DateTime.Parse(Properties.Settings.Default.KigenTo_Value);
                }
                else
                {
                    this.form.KigenTo.Value = null;
                }
                this.form.KyokaNo.Text = Properties.Settings.Default.KyokaNo_Text;

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
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

                DataTable dtFut = null;
                DataTable dtTok = null;
                if (string.IsNullOrWhiteSpace(this.form.GyouseikyokaKubunCode.Text) || this.form.GyouseikyokaKubunCode.Text.Equals("1"))
                {
                    dtFut = this.dao.GetIchiranDataSqlFileForKigenKanri(GET_FUT_ICHIRAN_DATA_SQL, this.SearchStringFut, this.form.GyouseikyokaKubunCode.Text);
                }
                if (string.IsNullOrWhiteSpace(this.form.GyouseikyokaKubunCode.Text) || this.form.GyouseikyokaKubunCode.Text.Equals("2"))
                {
                    dtTok = this.dao.GetIchiranDataSqlFileForKigenKanri(GET_TOK_ICHIRAN_DATA_SQL, this.SearchStringTok, this.form.GyouseikyokaKubunCode.Text);
                }

                DataTable sortTable = null;
                if (dtFut != null && dtTok != null)
                {
                    dtFut.Merge(dtTok, false);
                    sortTable = dtFut;
                }
                if (dtFut != null && dtTok == null)
                {
                    sortTable = dtFut;
                }
                if (dtFut == null && dtTok != null)
                {
                    sortTable = dtTok;
                }
                this.SearchResult = sortTable.Clone();
                DataRow[] rows = sortTable.Select("1 = 1", "KYOKA_KBN ASC, GYOUSHA_CD ASC, GENBA_CD ASC, CHIIKI_CD ASC, KYOKA_NO ASC");
                for (int i = 0; i < rows.Length; i++)
                {
                    this.SearchResult.Rows.Add(rows[i].ItemArray);
                }

                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Jigyoushakubun_Text = this.form.Jigyoushakubun.Text;
                Properties.Settings.Default.GyoushaCode_Text = this.form.GyoushaCode.Text;
                Properties.Settings.Default.GenbaCode_Text = this.form.GenbaCode.Text;
                Properties.Settings.Default.ChiikiCode_Text = this.form.ChiikiCode.Text;
                Properties.Settings.Default.GyouseikyokaKubunCode_Text = this.form.GyouseikyokaKubunCode.Text;
                if (this.form.KigenFrom.Value != null)
                {
                    Properties.Settings.Default.KigenFrom_Value = ((DateTime)this.form.KigenFrom.Value).ToString("yyyy/MM/dd");
                }
                if (this.form.KigenTo.Value != null)
                {
                    Properties.Settings.Default.KigenTo_Value = ((DateTime)this.form.KigenTo.Value).ToString("yyyy/MM/dd");
                }
                Properties.Settings.Default.KyokaNo_Text = this.form.KyokaNo.Text;
                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows.Count;

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
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
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

                ClearCondition();
                bool catchErr = SetIchiran();

                LogUtility.DebugMethodEnd();
                return catchErr;
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

            ItakuKeiyakushoKyokashoKigenHoshuLogic localLogic = other as ItakuKeiyakushoKyokashoKigenHoshuLogic;
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
                DataTable table = this.SearchResult;

                if (table != null)
                {
                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                    this.form.Ichiran.IsBrowsePurpose = false;
                    this.form.Ichiran.DataSource = table;
                    this.form.Ichiran.IsBrowsePurpose = true;
                }
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

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　start
            this.form.KigenFrom.Leave += new System.EventHandler(KigenFrom_Leave);
            this.form.KigenTo.Leave += new System.EventHandler(KigenTo_Leave);
            // 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　start

            // VUNGUYEN 20150525 #1294 START
            // 適用終了のダブルクリックイベント
            this.form.KigenTo.MouseDoubleClick += new MouseEventHandler(KigenTo_MouseDoubleClick);
            // VUNGUYEN 20150525 #1294 END

            // 事業者区分の変更イベント
            this.form.Jigyoushakubun.TextChanged += new EventHandler(Jigyoushakubun_TextChanged);

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
            // ヘッダーの「1.運搬　2.処分　3.最終　4.全て」を非表示
            this.form.Jigyoushakubun.Visible = false;
            this.form.Jigyoushakubun1.Visible = false;
            this.form.Jigyoushakubun2.Visible = false;
            this.form.Jigyoushakubun3.Visible = false;
            this.form.Jigyoushakubun4.Visible = false;

            // ヘッダー非表示による、初期フォーカス位置変更
            this.form.GyoushaCode.Focus();
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_CHIIKIBETSU_KYOKA entityFut = new M_CHIIKIBETSU_KYOKA();
            M_CHIIKIBETSU_KYOKA entityTok = new M_CHIIKIBETSU_KYOKA();

            if (!string.IsNullOrEmpty(this.form.GyoushaCode.Text))
            {
                entityFut.GYOUSHA_CD = this.form.GyoushaCode.Text;
                entityTok.GYOUSHA_CD = this.form.GyoushaCode.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GenbaCode.Text))
            {
                entityFut.GENBA_CD = this.form.GenbaCode.Text;
                entityTok.GENBA_CD = this.form.GenbaCode.Text;
            }
            if (!string.IsNullOrEmpty(this.form.ChiikiCode.Text))
            {
                entityFut.CHIIKI_CD = this.form.ChiikiCode.Text;
                entityTok.CHIIKI_CD = this.form.ChiikiCode.Text;
            }
            if (!string.IsNullOrWhiteSpace(this.form.Jigyoushakubun.Text) && !this.form.Jigyoushakubun.Text.Equals("4"))
            {
                entityFut.KYOKA_KBN = Int16.Parse(this.form.Jigyoushakubun.Text);
                entityTok.KYOKA_KBN = Int16.Parse(this.form.Jigyoushakubun.Text);
            }
            if (this.form.KigenFrom.Value != null)
            {
                entityFut.FUTSUU_KYOKA_BEGIN = (DateTime)this.form.KigenFrom.Value;
                entityTok.TOKUBETSU_KYOKA_BEGIN = (DateTime)this.form.KigenFrom.Value;
            }
            if (this.form.KigenTo.Value != null)
            {
                entityFut.FUTSUU_KYOKA_END = (DateTime)this.form.KigenTo.Value;
                entityTok.TOKUBETSU_KYOKA_END = (DateTime)this.form.KigenTo.Value;
            }
            if (!string.IsNullOrWhiteSpace(this.form.KyokaNo.Text))
            {
                entityFut.FUTSUU_KYOKA_NO = this.form.KyokaNo.Text;
                entityTok.TOKUBETSU_KYOKA_NO = this.form.KyokaNo.Text;
            }

            this.SearchStringFut = entityFut;
            this.SearchStringTok = entityTok;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.form.Jigyoushakubun.Text = "4";
            this.form.GyoushaCode.Text = string.Empty;
            this.form.GyoushaName.Text = string.Empty;
            this.form.GenbaCode.Text = string.Empty;
            this.form.GenbaName.Text = string.Empty;
            this.form.ChiikiCode.Text = string.Empty;
            this.form.ChiikiName.Text = string.Empty;
            this.form.GyouseikyokaKubunCode.Text = string.Empty;
            this.form.GyouseikyokaKubunName.Text = string.Empty;
            this.form.KigenFrom.Value = null;
            this.form.KigenTo.Value = null;
            this.form.KyokaNo.Text = string.Empty;
        }

        /// <summary>
        /// 業者名取得処理
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        private string GetGyoushaName(string gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            string ret = string.Empty;
            if (!string.IsNullOrWhiteSpace(gyoushaCd))
            {
                M_GYOUSHA entity = this.gyoushaDao.GetDataByCd(gyoushaCd);
                if (entity != null)
                {
                    ret = entity.GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(gyoushaCd);
            return ret;
        }

        /// <summary>
        /// 現場名取得処理
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        private string GetGenbaName(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            string ret = string.Empty;
            if (!string.IsNullOrWhiteSpace(gyoushaCd) && !string.IsNullOrWhiteSpace(genbaCd))
            {
                M_GENBA param = new M_GENBA();
                param.GYOUSHA_CD = gyoushaCd;
                param.GENBA_CD = genbaCd;
                M_GENBA entity = this.genbaDao.GetDataByCd(param);
                if (entity != null)
                {
                    ret = entity.GENBA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(gyoushaCd, genbaCd);
            return ret;
        }

        /// <summary>
        /// 地域名取得処理
        /// </summary>
        /// <param name="chiikiCd"></param>
        /// <returns></returns>
        private string GetChiikiName(string chiikiCd)
        {
            LogUtility.DebugMethodStart(chiikiCd);

            string ret = string.Empty;
            if (!string.IsNullOrWhiteSpace(chiikiCd))
            {
                M_CHIIKI entity = this.chiikiDao.GetDataByCd(chiikiCd);
                if (entity != null)
                {
                    ret = entity.CHIIKI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd(chiikiCd);
            return ret;
        }

        // 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　start

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

                this.form.KigenFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.KigenTo.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.KigenFrom.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.KigenTo.Text))
                {
                    return false;
                }

                DateTime date_from = Convert.ToDateTime(this.form.KigenFrom.Value);
                DateTime date_to = Convert.ToDateTime(this.form.KigenTo.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    //this.form.KigenFrom.IsInputErrorOccured = true;
                    //this.form.KigenTo.IsInputErrorOccured = true;
                    this.form.KigenFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.KigenTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "期限From", "期限To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.KigenFrom.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region KigenFrom_Leaveイベント

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void KigenFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.KigenTo.Text))
            {
                //this.form.KigenTo.IsInputErrorOccured = false;
                this.form.KigenTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        #region KigenTo_Leaveイベント

        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void KigenTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.KigenFrom.Text))
            {
                //this.form.KigenFrom.IsInputErrorOccured = false;
                this.form.KigenFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }

        #endregion

        // 20141203 Houkakou 「委託契約書許可証期限管理」の日付チェックを追加する　end

        // VUNGUYEN 20150525 #1294 START
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KigenTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.KigenTo.Text = this.form.KigenFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 事業者区分の変更時に現場の活性/非活性を切り替える。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Jigyoushakubun_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.Jigyoushakubun.Text.Equals("1"))
            {
                this.form.GenbaCode.Enabled = false;
                this.form.GenbaCode.ReadOnly = true;
                this.form.GenbaName.Enabled = false;
                this.form.GenbaName.ReadOnly = true;
                this.form.btnSearchGenba.Enabled = false;
                this.form.GenbaCode.Text = String.Empty;
                this.form.GenbaName.Text = String.Empty;
            }
            else
            {
                this.form.GenbaCode.Enabled = true;
                this.form.GenbaCode.ReadOnly = false;
                this.form.GenbaName.Enabled = true;
                this.form.GenbaName.ReadOnly = true;
                this.form.btnSearchGenba.Enabled = true;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool GyoushaValidated()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GyoushaCode.Text))
                {
                    this.form.GyoushaName.Text = string.Empty;
                    this.form.GenbaCode.Text = string.Empty;
                    this.form.GenbaName.Text = string.Empty;
                    return false;
                }

                // 業者CD変更しない場合、何も処理をしない
                if (this.befGyoushaCd == this.form.GyoushaCode.Text)
                {
                    return false;
                }

                // 現場情報の初期化
                this.form.GenbaCode.Text = string.Empty;
                this.form.GenbaName.Text = string.Empty;

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.GyoushaCode.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.gyoushaDao.GetAllValidData(entity);
                if (entitys != null && entitys.Length > 0)
                {
                    this.form.GyoushaName.Text = entitys[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.form.GyoushaName.Text = string.Empty;
                    var messagelog = new MessageBoxShowLogic();
                    messagelog.MessageBoxShow("E020", "業者");
                    this.form.GyoushaCode.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GyoushaValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool GenbaValidated()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GenbaCode.Text))
                {
                    this.form.GenbaName.Text = string.Empty;
                    return false;
                }

                var messagelog = new MessageBoxShowLogic();

                if (string.IsNullOrEmpty(this.form.GyoushaCode.Text))
                {
                    messagelog.MessageBoxShow("E051", "業者");
                    this.form.GenbaCode.Text = string.Empty;
                    this.form.GenbaCode.Focus();
                    return false;
                }

                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GyoushaCode.Text;
                entity.GENBA_CD = this.form.GenbaCode.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.genbaDao.GetAllValidData(entity);
                if (entitys != null && entitys.Length > 0)
                {
                    this.form.GenbaName.Text = entitys[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GenbaName.Text = string.Empty;
                    messagelog.MessageBoxShow("E020", "現場");
                    this.form.GenbaCode.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GenbaValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
    }
}