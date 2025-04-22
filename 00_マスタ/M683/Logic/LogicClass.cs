// $Id: LogicClass.cs 54103 2015-06-30 08:35:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KobetsuHinmeiTankaIchiran.APP;
using KobetsuHinmeiTankaIchiran.Const;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using System.ComponentModel;

namespace KobetsuHinmeiTankaIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "KobetsuHinmeiTankaIchiran.Setting.ButtonSetting.xml";

        private readonly string GET_HINMEI_DATA_SQL = "KobetsuHinmeiTankaIchiran.Sql.GetHinmeiDataSql.sql";

        private readonly string GET_UNPAN_GYOUSHA_DATA_SQL = "KobetsuHinmeiTankaIchiran.Sql.GetUnpanGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GYOUSHA_DATA_SQL = "KobetsuHinmeiTankaIchiran.Sql.GetNioroshiGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GENBA_DATA_SQL = "KobetsuHinmeiTankaIchiran.Sql.GetNioroshiGenbaDataSql.sql";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private KobetsuHinmeiTankaIchiranForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private IchiranHeaderForm1 headerForm;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 個別品名単価のDao
        /// </summary>
        private IM_KOBETSU_HINMEI_TANKADao kohitaDao;

        /// <summary>
        /// 取引先マスタのエンティティ
        /// </summary>
        private M_TORIHIKISAKI torihikisakiEntity;

        /// <summary>
        /// 業者マスタのエンティティ
        /// </summary>
        private M_GYOUSHA gyoushaEntity;

        /// <summary>
        /// 現場マスタのエンティティ
        /// </summary>
        private M_GENBA genbaEntity;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao daoGenba;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao hinmeiDAO;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(KobetsuHinmeiTankaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.headerForm = (IchiranHeaderForm1)((IchiranBaseForm)targetForm.ParentForm).headerForm;
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.hinmeiDAO = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.kohitaDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();

            // ヘッダーの拠点を非表示にする
            this.headerForm.lbl_読込日時.Visible = false;
            this.headerForm.HEADER_KYOTEN_CD.Visible = false;
            this.headerForm.HEADER_KYOTEN_NAME.Visible = false;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();
                if (!this.form.EventSetFlg)
                {
                    // イベントの初期化処理
                    this.EventInit();
                    this.form.EventSetFlg = true;
                }
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;   // 行の追加オプション(false)

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                this.InitHeaderArea();

                // 表示条件初期化
                this.RemoveIchiranHyoujiJoukenEvent();
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
                this.AddIchiranHyoujiJoukenEvent();
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
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
        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (IchiranBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);      // 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);      // 修正
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);      // CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);      // 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);      // 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);    // 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);    // 閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);       // パターン一覧画面へ遷移

            //明細ダブルクリック時のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);

            //表示条件イベント生成
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvLogic = new CSVExport();
            DENSHU_KBN id = this.form.DenshuKbn;
            csvLogic.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, id.ToTitleString(), this.form);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 条件初期化
            this.form.DenpyouKubun.Text = "1";
            this.form.DenpshuuKubun.Text = "5";
            this.form.TORIHIKISAKI_CD.Clear();
            this.form.TORIHIKISAKI_RNAME.Clear();
            this.form.GYOUSHA_CD.Clear();
            this.form.GYOUSHA_RNAME.Clear();
            this.form.GENBA_CD.Clear();
            this.form.GENBA_RNAME.Clear();
            this.form.HINMEI_CD.Clear();
            this.form.HINMEI_NAME.Clear();
            this.form.UNIT_CD.Clear();
            this.form.UNIT_NAME_RYAKU.Clear();
            this.form.UNPAN_GYOUSHA_CD.Clear();
            this.form.UNPAN_GYOUSHA_NAME.Clear();
            this.form.NIOROSHI_GYOUSHA_CD.Clear();
            this.form.NIOROSHI_GYOUSHA_NAME.Clear();
            this.form.NIOROSHI_GENBA_CD.Clear();
            this.form.NIOROSHI_GENBA_NAME.Clear();
            this.form.beforGyoushaCD = string.Empty;

            this.form.TorihikisakiKbn.Enabled = true;
            this.form.GyoushaKbn.Enabled = true;
            this.form.GenbaKbn.Enabled = true;
            this.form.UnpanGyoushaKbn.Enabled = true;
            this.form.NioroshiGyoushaKbn.Enabled = true;
            this.form.NioroshiGenbaKbn.Enabled = true;

            this.form.TorihikisakiKbn.Checked = false;
            this.form.GyoushaKbn.Checked = false;
            this.form.GenbaKbn.Checked = false;
            this.form.UnpanGyoushaKbn.Checked = false;
            this.form.NioroshiGyoushaKbn.Checked = false;
            this.form.NioroshiGenbaKbn.Checked = false;


            this.SetHyoujiJoukenInit();
            var ds = (DataTable)this.form.customDataGridView1.DataSource;
            if (ds != null)
            {
                ds.Clear();
                this.form.customDataGridView1.DataSource = ds;
            }

            KobetsuHinmeiTankaIchiran.Properties.Settings.Default.Save();

            this.form.customSearchHeader1.ClearCustomSearchSetting();
            this.form.customSortHeader1.ClearCustomSortSetting();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                if (this.SearchCheck())
                {
                    return;
                }

                //読込データ件数を取得
                this.headerForm.ReadDataNumber.Text = this.Search().ToString();

                if (this.form.customDataGridView1 != null)
                {
                    this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headerForm.ReadDataNumber.Text = "0";
                }

                if (this.headerForm.ReadDataNumber.Text == "0")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }

                this.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headerForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.SaveHyoujiJoukenDefault();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {

            // 初期値設定
            this.form.TORIHIKISAKI_CD.Text = KobetsuHinmeiTankaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
            this.form.GYOUSHA_CD.Text = KobetsuHinmeiTankaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT;
            this.form.GENBA_CD.Text = KobetsuHinmeiTankaIchiran.Properties.Settings.Default.GENBA_CD_TEXT;
            this.form.DenpyouKubun.Text = "1";
            this.form.DenpshuuKubun.Text = "5";

            //取引先名をセット
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                this.form.TORIHIKISAKI_RNAME.Text = this.torihikisakiEntity == null ? string.Empty : torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }

            //業者名をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.gyoushaEntity = daoGyousha.GetDataByCd(this.form.GYOUSHA_CD.Text);
                this.form.GYOUSHA_RNAME.Text = this.gyoushaEntity == null ? string.Empty : gyoushaEntity.GYOUSHA_NAME_RYAKU;
            }

            //現場名をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                M_GENBA condition = new M_GENBA();

                condition.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                condition.GENBA_CD = this.form.GENBA_CD.Text;

                this.genbaEntity = daoGenba.GetDataByCd(condition);
                this.form.GENBA_RNAME.Text = this.genbaEntity == null ? string.Empty : genbaEntity.GENBA_NAME_RYAKU;
            }

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }

            //アラートの保存データがあればそちらを表示
            if (KobetsuHinmeiTankaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
            {
                this.headerForm.alertNumber.Text = KobetsuHinmeiTankaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
            }

            //前回業者CDを設定
            this.form.beforGyoushaCD = this.form.GYOUSHA_CD.Text;
        }

        #endregion

        #region 個別品名単価一括更新画面起動処理

        /// <summary>
        /// 個別品名単価一括更新画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg);
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                r_framework.FormManager.FormManager.OpenFormWithAuth("M212", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, windowType);
            }
            else
            {
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)    // No.3988
                {
                    string cd1 = row.Cells[KobetsuHinmeiTankaIchiranConstans.KEY_ID1].Value.ToString();
                    string cd2 = row.Cells[KobetsuHinmeiTankaIchiranConstans.KEY_ID2].Value.ToString();
                    string cd3 = row.Cells[KobetsuHinmeiTankaIchiranConstans.KEY_ID3].Value.ToString();
                    string cd4 = row.Cells[KobetsuHinmeiTankaIchiranConstans.KEY_ID4].Value.ToString(); ;
                    r_framework.FormManager.FormManager.OpenFormWithAuth("M212", windowType, windowType, cd1, cd2, cd3, cd4);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)

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

            LogicClass localLogic = other as LogicClass;
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

        #region アラート件数取得処理

        /// <summary>
        /// アラート件数取得処理
        /// </summary>
        /// <returns></returns>
        public int GetAlertCount()
        {
            int result = 0;
            int.TryParse(this.headerForm.alertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            return result;
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.kohitaDao.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                sb.Append("SELECT DISTINCT ");
                sb.AppendFormat(" KON.TORIHIKISAKI_CD AS {0}, ", KobetsuHinmeiTankaIchiranConstans.KEY_ID1);
                sb.AppendFormat(" KON.GYOUSHA_CD AS {0}, ", KobetsuHinmeiTankaIchiranConstans.KEY_ID2);
                sb.AppendFormat(" KON.GENBA_CD AS {0}, ", KobetsuHinmeiTankaIchiranConstans.KEY_ID3);
                sb.AppendFormat(" KON.DENPYOU_KBN_CD AS {0}, ", KobetsuHinmeiTankaIchiranConstans.KEY_ID4);
                sb.Append(this.form.SelectQuery);
            }
            return sb.ToString();
        }

        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();

            // 個別品名単価マスタ
            sb.Append(" FROM M_KOBETSU_HINMEI_TANKA KON ");

            // 現場マスタ
            sb.Append(" LEFT JOIN M_GENBA MGEN ");
            sb.Append(" ON MGEN.GYOUSHA_CD = KON.GYOUSHA_CD AND MGEN.GENBA_CD = KON.GENBA_CD ");

            // 業者マスタ
            sb.Append(" LEFT JOIN M_GYOUSHA MGYOU ");
            sb.Append(" ON MGYOU.GYOUSHA_CD = KON.GYOUSHA_CD ");

            // 取引先マスタ
            sb.Append(" LEFT JOIN M_TORIHIKISAKI MTORI ");
            sb.Append(" ON MTORI.TORIHIKISAKI_CD = KON.TORIHIKISAKI_CD ");

            // 品名マスタ
            sb.Append(" LEFT JOIN M_HINMEI HIN ");
            sb.Append(" ON HIN.HINMEI_CD = KON.HINMEI_CD ");

            // 種類マスタ
            sb.Append(" LEFT JOIN M_SHURUI MSHR ");
            sb.Append(" ON MSHR.SHURUI_CD = HIN.SHURUI_CD ");

            // 分類マスタ
            sb.Append(" LEFT JOIN M_BUNRUI MBUR ");
            sb.Append(" ON MBUR.BUNRUI_CD = HIN.BUNRUI_CD ");

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string CreateWhereQuery()
        {
            var result = new StringBuilder(256);
            string strTemp;

            result.Append("1 = 1 ");

            // 伝票区分
            strTemp = this.form.DenpyouKubun.Text;
            if (!string.IsNullOrWhiteSpace(strTemp) && strTemp != "3")
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" KON.DENPYOU_KBN_CD = '{0}'", strTemp);

                this.form.dennpyouKbn = strTemp;
            }

            // 取引先CD
            if (this.form.TorihikisakiKbn.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (KON.TORIHIKISAKI_CD = '' OR KON.TORIHIKISAKI_CD = NULL)");
            }
            else
            {                
                strTemp = this.form.TORIHIKISAKI_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" KON.TORIHIKISAKI_CD = '{0}'", strTemp);
                }
            }
            
            if (this.form.GyoushaKbn.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (KON.GYOUSHA_CD = '' OR KON.GYOUSHA_CD = NULL)");
            }
            else
            {
                // 業者CD
                strTemp = this.form.GYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" KON.GYOUSHA_CD = '{0}'", strTemp);
                }

                // 現場CD
                strTemp = this.form.GENBA_CD.Text;
                if (this.form.GenbaKbn.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.Append(" (KON.GENBA_CD = '' OR KON.GENBA_CD = NULL)");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            result.Append(" AND ");
                            result.AppendFormat(" KON.GENBA_CD = '{0}'", strTemp);
                        }
                    }
                }
            }

            // 品名CD
            strTemp = this.form.HINMEI_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" KON.HINMEI_CD = '{0}'", strTemp);
            }
            // 伝種区分
            if (this.form.DenpshuuKubun.Text == "4")
            {
                strTemp = "9";
            }
            else
            {
                strTemp = this.form.DenpshuuKubun.Text;
            }            
            if (!string.IsNullOrWhiteSpace(strTemp) && strTemp != "5")
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" KON.DENSHU_KBN_CD = '{0}'", strTemp);
            }
            // 単位CD
            strTemp = this.form.UNIT_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" KON.UNIT_CD = '{0}'", strTemp);
            }

            // 運搬業者CD
            if (this.form.UnpanGyoushaKbn.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (KON.UNPAN_GYOUSHA_CD = '' OR KON.UNPAN_GYOUSHA_CD = NULL)");
            }
            else
            {                
                strTemp = this.form.UNPAN_GYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" KON.UNPAN_GYOUSHA_CD = '{0}'", strTemp);
                }
            }

            if (this.form.NioroshiGyoushaKbn.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append("(KON.NIOROSHI_GYOUSHA_CD = '' OR KON.NIOROSHI_GYOUSHA_CD = NULL)");
            }
            else
            {
                // 荷降先業者CD
                strTemp = this.form.NIOROSHI_GYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" KON.NIOROSHI_GYOUSHA_CD = '{0}'", strTemp);
                }

                // 荷降先現場CD
                strTemp = this.form.NIOROSHI_GENBA_CD.Text;
                if (this.form.NioroshiGenbaKbn.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.Append(" (KON.NIOROSHI_GENBA_CD = '' OR KON.NIOROSHI_GENBA_CD = NULL)");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            result.Append(" AND ");
                            result.AppendFormat(" KON.NIOROSHI_GENBA_CD = '{0}'", strTemp);
                        }
                    }
                }
            }

            //// 荷降先業者CD
            //strTemp = this.form.NIOROSHI_GYOUSHA_CD.Text;
            //if (!string.IsNullOrWhiteSpace(strTemp))
            //{
            //    if (!string.IsNullOrWhiteSpace(result.ToString()))
            //    {
            //        result.Append(" AND ");
            //    }
            //    result.AppendFormat(" KON.NIOROSHI_GYOUSHA_CD = '{0}'", strTemp);
            //}
            //// 荷降先現場CD
            //strTemp = this.form.NIOROSHI_GENBA_CD.Text;
            //if (!string.IsNullOrWhiteSpace(strTemp))
            //{
            //    if (!string.IsNullOrWhiteSpace(result.ToString()))
            //    {
            //        result.Append(" AND ");
            //    }
            //    result.AppendFormat(" KON.NIOROSHI_GENBA_CD = '{0}'", strTemp);
            //}

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" KON.DELETE_FLG = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (1 = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked)
            {
                result.Append(" OR (((KON.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= KON.TEKIYOU_END) or (KON.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and KON.TEKIYOU_END IS NULL) or (KON.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= KON.TEKIYOU_END) or (KON.TEKIYOU_BEGIN IS NULL and KON.TEKIYOU_END IS NULL)) and KON.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                result.Append(" OR KON.DELETE_FLG = 1");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(" OR ((KON.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > KON.TEKIYOU_END) and KON.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(")");
            }

            return result.Length > 0 ? result.Insert(0, " WHERE ").ToString() : string.Empty;
        }

        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            var query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
            {
                query += " ORDER BY " + this.form.OrderByQuery;
            }

            return query;
        }

        #endregion

        #region 表示条件

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            // 一覧表示イベントの削除
            this.RemoveIchiranHyoujiJoukenEvent();

            if (this.sysinfoEntity != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            // 一覧表示イベントの追加
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        public bool SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                KobetsuHinmeiTankaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                KobetsuHinmeiTankaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                KobetsuHinmeiTankaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                KobetsuHinmeiTankaIchiran.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                KobetsuHinmeiTankaIchiran.Properties.Settings.Default.Save();
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 一覧表示イベントの削除
        /// </summary>
        public void RemoveIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// 一覧表示イベントの追加
        /// </summary>
        public void AddIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        #endregion

        #region 伝票区分、伝種区分チェック
        /// <summary>
        /// 伝票区分、伝種区分チェック
        /// </summary>
        /// <returns></returns>
        internal bool SearchCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if(string.IsNullOrEmpty(this.form.DenpyouKubun.Text))
            {
                this.form.DenpyouKubun.Focus();
                msgLogic.MessageBoxShow("E001", "伝票区分");
                return true;
            }

            if(string.IsNullOrEmpty(this.form.DenpshuuKubun.Text))
            {
                this.form.DenpshuuKubun.Focus();
                msgLogic.MessageBoxShow("E001", "伝種区分");
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 品名名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchHinmeiName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD.Text;
                    DataTable dt = this.hinmeiDAO.GetIchiranDataSqlFile(this.GET_HINMEI_DATA_SQL, hinmei, false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.HINMEI_NAME.Text = dt.Rows[0]["HINMEI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.HINMEI_NAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "品名");
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.form.HINMEI_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmeiName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmeiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 運搬名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchUnpanGyoushaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                    DataTable dt = this.daoGyousha.GetDataBySqlFile(this.GET_UNPAN_GYOUSHA_DATA_SQL, gyousha);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = dt.Rows[0]["UNPAN_GYOUSHA_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "業者");
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchUnpanGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchUnpanGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }

        }

        // <summary>
        /// 荷降先名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchNioroshiGyoushaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // マスタ存在チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    DataTable dt = this.daoGyousha.GetDataBySqlFile(this.GET_NIOROSHI_GYOUSHA_DATA_SQL, gyousha);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "業者");
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchNioroshiGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchNioroshiGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 荷降先現場名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchNioroshiGenbaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    // 荷降先現場CDが入力されている状態で、荷降先業者CDがクリアされていた場合、エラーとする
                    if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                    {
                        msgLogic.MessageBoxShow("E051", "荷降業者");
                        this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                        e.Cancel = true;
                        LogUtility.DebugMethodEnd(e);
                        return false;
                    }

                    // マスタ存在チェック
                    M_GENBA genba = new M_GENBA();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    genba.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    genba.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                    DataTable dt = this.daoGenba.GetDataBySqlFile(this.GET_NIOROSHI_GENBA_DATA_SQL, genba);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.form.NIOROSHI_GYOUSHA_CD.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                        this.form.NIOROSHI_GENBA_NAME.Text = dt.Rows[0]["NIOROSHI_GENBA_RYAKU"].ToString();
                    }
                    else
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchNioroshiGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchNioroshiGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        internal bool CheckGenba()
        {
            try
            {
                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.daoGenba.GetAllValidData(entity);
                if (genba != null && genba.Length > 0)
                {
                    this.form.GENBA_RNAME.Text = genba[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GENBA_RNAME.Text = string.Empty;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
    }
}
