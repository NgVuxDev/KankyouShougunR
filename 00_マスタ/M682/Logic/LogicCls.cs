using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.APP.Base;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.APP;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.Const;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.DAO;
using System.ComponentModel;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.Dto;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.CustomControl;
using System.Drawing;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.KobetsuHimeiTankaUpdate.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;

        /// <summary>
        /// 紐付くデータグリッドビュー
        /// </summary>
        private CustomDataGridView linkedDataGridView = null;

        /// <summary>
        /// 個別品名単価一覧のDao
        /// </summary>
        private IchiranDao ichiranDao;

        /// <summary>
        /// 個別品名単価のDao
        /// </summary>
        private IM_KOBETSU_HINMEI_TANKADao kohitaDao;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 運搬業者のDao
        /// </summary>
        private UnpanGyoushaDAO unpanGyoushaDAO;

        /// <summary>
        ///　荷降先業者のDao
        /// </summary>
        private NioroshiGyoushaDAO nioroshiGyoushaDAO;

        /// <summary>
        /// 荷降先現場のDao
        /// </summary>
        private NioroshiGenbaDAO nioroshiGenbaDAO;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private HinmeiDAO hinmeiDAO;

        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        private string denpyouKbn;

        /// <summary>
        /// 個別品名単価list
        /// </summary>
        public List<M_KOBETSU_HINMEI_TANKA> entitys;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        private DataTable dtSort = null;

        /// <summary>
        /// アラートを出力する件数
        /// </summary>
        public int AlertCount { get; set; }

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable SearchResult { get; set; }

        /// <summary>
        /// テーブルクリア用
        /// </summary>
        internal DataTable ClearTable { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        internal MeisaiSearchJoukenDto SearchString { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GenbaCd { get; set; }

        /// <summary>
        /// 状態Flg
        /// </summary>
        public string JoutaiFlg { get; set; }

        /// <summary>
        /// 検索結果(取引先)
        /// </summary>
        public DataTable SearchResultTorihikisaki { get; set; }

        /// <summary>
        /// 検索結果(現場)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 検索結果(業者)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal M_GYOUSHA prevGyousha = null;

        /// <summary>
        ///
        /// </summary>
        internal M_GENBA prevGenba = null;

        /// <summary>
        ///
        /// </summary>
        internal bool isRegist = true;

        /// <summary>
        ///
        /// </summary>
        internal bool entitysFlg { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;

                this.ichiranDao = DaoInitUtility.GetComponent<IchiranDao>();
                this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                this.kohitaDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEI_TANKADao>();
                this.unpanGyoushaDAO = DaoInitUtility.GetComponent<UnpanGyoushaDAO>();
                this.nioroshiGyoushaDAO = DaoInitUtility.GetComponent<NioroshiGyoushaDAO>();
                this.nioroshiGenbaDAO = DaoInitUtility.GetComponent<NioroshiGenbaDAO>();
                this.hinmeiDAO = DaoInitUtility.GetComponent<HinmeiDAO>();
                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
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

        #endregion

        #region 初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //画面初期値設定
                this.DefaultInit();

                //システム設定情報読み込み
                this.GetSysInfo();

                this.HearderInit();

            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msglogic.MessageBoxShow("E093");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msglogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
 
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            var id = this.form.customSortHeader1.FindForm().Name + "." + this.form.customSortHeader1.Name;
            sortSettingInfo = SortSettingHelper.LoadSortSettingInfo(id);

            // DataGridViewとの連携初期化
            if (this.form.customSortHeader1.LinkedDataGridViewName != null && this.form.customSortHeader1.LinkedDataGridViewName.Length > 0)
            {
                // プロパティで設定されたDataGridViewの名前からコントロールを探す
                foreach (Control control in this.form.customSortHeader1.FindForm().Controls)
                {
                    if (control.Name.Equals(this.form.customSortHeader1.LinkedDataGridViewName))
                    {
                        this.linkedDataGridView = control as CustomDataGridView;
                        break;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                // 一括入力ボタン(F1)イベント生成
                parentForm.bt_func1.Click -= new System.EventHandler(this.form.TikkatsuNyuuryoku);
                parentForm.bt_func1.Click += new System.EventHandler(this.form.TikkatsuNyuuryoku);

                //条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click -= new System.EventHandler(this.form.ClearCondition);
                parentForm.bt_func7.Click += new System.EventHandler(this.form.ClearCondition);

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click -= new System.EventHandler(this.form.Search);
                parentForm.bt_func8.Click += new System.EventHandler(this.form.Search);

                //登録ボタン(F9)イベント生成
                parentForm.bt_func9.Click -= new System.EventHandler(this.form.Regist);
                parentForm.bt_func9.Click += new System.EventHandler(this.form.Regist);
                parentForm.bt_func9.Enabled = false;

                //並び順えボタン(F10)イベント生成
                parentForm.bt_func10.Click -= new System.EventHandler(this.sort);
                parentForm.bt_func10.Click += new System.EventHandler(this.sort);

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click -= new System.EventHandler(this.form.Cancel);
                parentForm.bt_func11.Click += new System.EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click -= new System.EventHandler(this.form.FormClose);
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);

                this.form.TANK_TEKIYOU_BEGIN_END.MouseDoubleClick += new MouseEventHandler(TANK_TEKIYOU_BEGIN_END_MouseDoubleClick);
                this.form.TANK_TEKIYOU_END_END.MouseDoubleClick += new MouseEventHandler(TANK_TEKIYOU_END_END_MouseDoubleClick);

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

        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TANK_TEKIYOU_BEGIN_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var tekiyouBeginTextBox = this.form.TANK_TEKIYOU_BEGIN_START;
            var tekiyouEndTextBox = this.form.TANK_TEKIYOU_BEGIN_END;
            tekiyouEndTextBox.Text = tekiyouBeginTextBox.Text;

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
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_RNAME.Text;
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_RNAME.Text;
                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT = this.form.GENBA_RNAME.Text;

                Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.Save();
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }


        /// <summary>
        /// 一括入力単価と新単価の適用開始日を設定する。
        /// </summary>
        public void setTankaAndTekiyouData()
        {
           
        }

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TANK_TEKIYOU_END_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var tekiyouBeginTextBox = this.form.TANK_TEKIYOU_END_START;
            var tekiyouEndTextBox = this.form.TANK_TEKIYOU_END_END;
            tekiyouEndTextBox.Text = tekiyouBeginTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        internal bool CheckGenba()
        {
            try
            {
                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(entity);
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
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region Func処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SetSearchResult();

                int count = this.SearchResult.Rows == null || this.SearchResult.Rows.Count == 0 ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.msglogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        #region 登録チェック

        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns></returns>
        internal bool RegistCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                int checkCount = 0;
                int rowCount = this.form.Ichiran.Rows.Count;
                bool xinTankaFlg = true;
                for (int i = 0; i < rowCount; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null && bool.Parse(this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                    {
                        if (this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value == null
                            || (this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value != null && string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "新単価（明細）");
                            xinTankaFlg = false;
                            break;
                        }

                        if (this.form.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value == null
                            || (this.form.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value != null && string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value.ToString())))
                        {
                            msgLogic.MessageBoxShow("E001", "新単価適用開始日（明細）");
                            xinTankaFlg = false;
                            break;
                        }
                        checkCount += 1;
                    }
                }

                if (!xinTankaFlg)
                {
                    return false;
                }

                if (checkCount == 0)
                {
                    msgLogic.MessageBoxShow("E051", "更新する単価データ");
                    //フォーカス設定
                    this.form.DenpyouKubun.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 日付チェック

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                this.entitys = new List<M_KOBETSU_HINMEI_TANKA>();
                M_KOBETSU_HINMEI_TANKA kohitanEntry = new M_KOBETSU_HINMEI_TANKA();

                DataTable checkTrueDT = new DataTable("CheckTrueDT");
                DataColumn dc = null;
                dc = checkTrueDT.Columns.Add("ID", Type.GetType("System.Int32"));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                dc.AllowDBNull = false;
                dc = checkTrueDT.Columns.Add("MEISAI_HINMEI_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_UNIT_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("DENSHU_KBN_RYAKU", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_UNPAN_GYOUSHA_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_NIOROSHI_GYOUSHA_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_NIOROSHI_GENBA_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_TORIHIKISAKI_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_GYOUSHA_CD", Type.GetType("System.String"));
                dc = checkTrueDT.Columns.Add("MEISAI_GENBA_CD", Type.GetType("System.String"));



                int rowCount = this.form.Ichiran.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null && bool.Parse(this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                    {
                        DataRow newRow = checkTrueDT.NewRow();

                        newRow["MEISAI_HINMEI_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_HINMEI_CD"].Value;
                        newRow["MEISAI_UNIT_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_UNIT_CD"].Value;
                        newRow["DENSHU_KBN_RYAKU"] = this.form.Ichiran.Rows[i].Cells["DENSHU_KBN_RYAKU"].Value;
                        newRow["MEISAI_UNPAN_GYOUSHA_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_UNPAN_GYOUSHA_CD"].Value;
                        newRow["MEISAI_NIOROSHI_GYOUSHA_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GYOUSHA_CD"].Value;
                        newRow["MEISAI_NIOROSHI_GENBA_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GENBA_CD"].Value;
                        newRow["MEISAI_TORIHIKISAKI_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_TORIHIKISAKI_CD"].Value;
                        newRow["MEISAI_GYOUSHA_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_GYOUSHA_CD"].Value;
                        newRow["MEISAI_GENBA_CD"] = this.form.Ichiran.Rows[i].Cells["MEISAI_GENBA_CD"].Value;

                        checkTrueDT.Rows.Add(newRow);
                    }

                }

                // 重複チェック
                DataTable dtDistinct = GetDistinctTable(checkTrueDT, 
                                                        "MEISAI_HINMEI_CD", 
                                                        "MEISAI_UNIT_CD", 
                                                        "DENSHU_KBN_RYAKU", 
                                                        "MEISAI_UNPAN_GYOUSHA_CD", 
                                                        "MEISAI_NIOROSHI_GYOUSHA_CD", 
                                                        "MEISAI_NIOROSHI_GENBA_CD",
                                                        "MEISAI_TORIHIKISAKI_CD",
                                                        "MEISAI_GYOUSHA_CD",
                                                        "MEISAI_GENBA_CD");

                if (dtDistinct.Rows.Count < checkTrueDT.Rows.Count)
                {
                    msgLogic.MessageBoxShow("E013");
                    return true;
                }

                for (int i = 0; i < rowCount; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null && bool.Parse(this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                    {
                        MeisaiSearchJoukenDto searchParams = new MeisaiSearchJoukenDto();
                        searchParams.HINMEI_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_HINMEI_CD"].Value.ToString();
                        searchParams.UNIT_CD = Convert.ToInt16(this.form.Ichiran.Rows[i].Cells["MEISAI_UNIT_CD"].Value.ToString());
                        searchParams.DENSHU_KBN_CD = Convert.ToInt16(this.form.Ichiran.Rows[i].Cells["DENSHU_KBN_CD"].Value.ToString());

                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_UNPAN_GYOUSHA_CD"].Value != null)
                        {
                            searchParams.UNPAN_GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_UNPAN_GYOUSHA_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.UNPAN_GYOUSHA_CD = "";
                        }
                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GYOUSHA_CD"].Value != null)
                        {
                            searchParams.NIOROSHI_GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.NIOROSHI_GYOUSHA_CD = "";
                        }
                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GENBA_CD"].Value != null)
                        {
                            searchParams.NIOROSHI_GENBA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GENBA_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.NIOROSHI_GENBA_CD = "";
                        }

                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_TORIHIKISAKI_CD"].Value != null)
                        {
                            searchParams.TORIHIKISAKI_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_TORIHIKISAKI_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.TORIHIKISAKI_CD = "";
                        }

                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_GYOUSHA_CD"].Value != null)
                        {
                            searchParams.GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_GYOUSHA_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.GYOUSHA_CD = "";
                        }

                        if (this.form.Ichiran.Rows[i].Cells["MEISAI_GENBA_CD"].Value != null)
                        {
                            searchParams.GENBA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_GENBA_CD"].Value.ToString();
                        }
                        else
                        {
                            searchParams.GENBA_CD = "";
                        }

                        searchParams.DENPYOU_KBN_CD = Convert.ToInt16(this.denpyouKbn);

                        // キーが同一の明細
                        DataTable SearchOnajiKeyResult = ichiranDao.GetOnajiKeyMeisaiData(searchParams);

                        DateTime dtTekiyouBegin = Convert.ToDateTime(null);
                        DateTime dtTekiyouEnd = Convert.ToDateTime(null);
                        bool tekiyouEndFlg = false;
                        DateTime dtXinTekiyouBegin = Convert.ToDateTime(this.form.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value);;
                        bool tekiyouStartFlg = true;
                        int returnCount = -1;
                        for (int j = 0; j < SearchOnajiKeyResult.Rows.Count; j++)
                        {
                            dtTekiyouBegin = Convert.ToDateTime(SearchOnajiKeyResult.Rows[j]["TEKIYOU_BEGIN"]);

                            if (!string.IsNullOrEmpty(SearchOnajiKeyResult.Rows[j]["TEKIYOU_END"].ToString()))
                            {
                                dtTekiyouEnd = Convert.ToDateTime(SearchOnajiKeyResult.Rows[j]["TEKIYOU_END"]);
                            }
                            else
                            {
                                tekiyouEndFlg = true;
                            }

                            if (DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) > 0 && !tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouEnd) == 0)
                            {
                                msgLogic.MessageBoxShow("E013");
                                return true;
                            }

                            if (DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) > 0 && !tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouEnd) < 0
                                || tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) > 0)
                            {
                                M_KOBETSU_HINMEI_TANKA kohitanKisonnEntry = new M_KOBETSU_HINMEI_TANKA();
                                kohitanKisonnEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());
                                kohitanKisonnEntry.TEKIYOU_END = dtXinTekiyouBegin.AddDays(-1);
                                
                                string createPc = kohitanKisonnEntry.CREATE_PC;
                                string createUser = kohitanKisonnEntry.CREATE_USER;
                                SqlDateTime createDate = kohitanKisonnEntry.CREATE_DATE;
                                var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(kohitanKisonnEntry);
                                dataBinderLogicGenba.SetSystemProperty(kohitanKisonnEntry, false);
                                kohitanKisonnEntry.CREATE_PC = createPc;
                                kohitanKisonnEntry.CREATE_USER = createUser;
                                kohitanKisonnEntry.CREATE_DATE = createDate;

                                entitys.Add(kohitanKisonnEntry);

                                M_KOBETSU_HINMEI_TANKA kohitanEntryadd = new M_KOBETSU_HINMEI_TANKA();
                                kohitanEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());

                                //if (this.form.KOSHINNHOUHOU_VALUE.Text == "1")
                                //{
                                    kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString());
                                //}
                                //else if(this.form.KOSHINNHOUHOU_VALUE.Text == "2")
                                //{
                                //    kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString()) + kohitanEntry.TANKA;
                                //}
                                

                                kohitanEntryadd.TEKIYOU_BEGIN = dtXinTekiyouBegin;
                                if (!tekiyouEndFlg)
                                {
                                    kohitanEntryadd.TEKIYOU_END = dtTekiyouEnd;
                                }
                                kohitanEntryadd.DENPYOU_KBN_CD = kohitanEntry.DENPYOU_KBN_CD;
                                kohitanEntryadd.TORIHIKISAKI_CD = kohitanEntry.TORIHIKISAKI_CD;
                                kohitanEntryadd.GYOUSHA_CD = kohitanEntry.GYOUSHA_CD;
                                kohitanEntryadd.GENBA_CD = kohitanEntry.GENBA_CD;
                                kohitanEntryadd.HINMEI_CD = kohitanEntry.HINMEI_CD;
                                kohitanEntryadd.DENSHU_KBN_CD = kohitanEntry.DENSHU_KBN_CD;
                                kohitanEntryadd.UNIT_CD = kohitanEntry.UNIT_CD;
                                kohitanEntryadd.UNPAN_GYOUSHA_CD = kohitanEntry.UNPAN_GYOUSHA_CD;
                                kohitanEntryadd.NIOROSHI_GYOUSHA_CD = kohitanEntry.NIOROSHI_GYOUSHA_CD;
                                kohitanEntryadd.NIOROSHI_GENBA_CD = kohitanEntry.NIOROSHI_GENBA_CD;
                                kohitanEntryadd.BIKOU = this.form.Ichiran.Rows[i].Cells["MEISAI_BIKOU"].Value.ToString();
                                kohitanEntryadd.SEARCH_TEKIYOU_BEGIN = kohitanEntry.SEARCH_TEKIYOU_BEGIN;
                                kohitanEntryadd.SEARCH_TEKIYOU_END = kohitanEntry.SEARCH_TEKIYOU_END;
                                kohitanEntryadd.DELETE_FLG = kohitanEntry.DELETE_FLG;

                                dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(kohitanEntryadd);
                                dataBinderLogicGenba.SetSystemProperty(kohitanEntryadd, false);

                                entitys.Add(kohitanEntryadd);
                                break;
                            }
                            else if (DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) == 0 && !tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouEnd) < 0
                                     || tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) == 0)
                            {
                                if (tekiyouStartFlg)
                                {
                                    var result = msgLogic.MessageBoxShow("C102");
                                    if (result == DialogResult.Yes)
                                    {
                                        tekiyouStartFlg = false;
                                    }
                                }
                                if (!tekiyouStartFlg)
                                {
                                    M_KOBETSU_HINMEI_TANKA kohitanKisonnEntry = new M_KOBETSU_HINMEI_TANKA();
                                    kohitanKisonnEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());
                                    kohitanKisonnEntry.DELETE_FLG = true;

                                    entitys.Add(kohitanKisonnEntry);

                                    M_KOBETSU_HINMEI_TANKA kohitanEntryadd = new M_KOBETSU_HINMEI_TANKA();
                                    kohitanEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());

                                    //if (this.form.KOSHINNHOUHOU_VALUE.Text == "1")
                                    //{
                                        kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString());
                                    //}
                                    //else if(this.form.KOSHINNHOUHOU_VALUE.Text == "2")
                                    //{
                                        //kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString()) + kohitanEntry.TANKA;
                                    //}

                                    kohitanEntryadd.TEKIYOU_BEGIN = dtXinTekiyouBegin;
                                    if (!tekiyouEndFlg)
                                    {
                                        kohitanEntryadd.TEKIYOU_END = dtTekiyouEnd;
                                    }                                  
                                    kohitanEntryadd.DENPYOU_KBN_CD = kohitanEntry.DENPYOU_KBN_CD;
                                    kohitanEntryadd.TORIHIKISAKI_CD = kohitanEntry.TORIHIKISAKI_CD;
                                    kohitanEntryadd.GYOUSHA_CD = kohitanEntry.GYOUSHA_CD;
                                    kohitanEntryadd.GENBA_CD = kohitanEntry.GENBA_CD;
                                    kohitanEntryadd.HINMEI_CD = kohitanEntry.HINMEI_CD;
                                    kohitanEntryadd.DENSHU_KBN_CD = kohitanEntry.DENSHU_KBN_CD;
                                    kohitanEntryadd.UNIT_CD = kohitanEntry.UNIT_CD;
                                    kohitanEntryadd.UNPAN_GYOUSHA_CD = kohitanEntry.UNPAN_GYOUSHA_CD;
                                    kohitanEntryadd.NIOROSHI_GYOUSHA_CD = kohitanEntry.NIOROSHI_GYOUSHA_CD;
                                    kohitanEntryadd.NIOROSHI_GENBA_CD = kohitanEntry.NIOROSHI_GENBA_CD;
                                    kohitanEntryadd.BIKOU = this.form.Ichiran.Rows[i].Cells["MEISAI_BIKOU"].Value.ToString();
                                    kohitanEntryadd.SEARCH_TEKIYOU_BEGIN = kohitanEntry.SEARCH_TEKIYOU_BEGIN;
                                    kohitanEntryadd.SEARCH_TEKIYOU_END = kohitanEntry.SEARCH_TEKIYOU_END;
                                    kohitanEntryadd.DELETE_FLG = kohitanEntry.DELETE_FLG;
                                    kohitanEntryadd.CREATE_USER = kohitanEntry.CREATE_USER;

                                    var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(kohitanEntryadd);
                                    dataBinderLogicGenba.SetSystemProperty(kohitanEntryadd, false);
                                    entitys.Add(kohitanEntryadd);
                                }
                                break;
                            }
                            else if (DateTime.Compare(dtXinTekiyouBegin, dtTekiyouBegin) == 0 && !tekiyouEndFlg && DateTime.Compare(dtXinTekiyouBegin, dtTekiyouEnd) == 0)
                            {
                                if (tekiyouStartFlg)
                                {
                                    var result = msgLogic.MessageBoxShow("C102");
                                    if (result == DialogResult.Yes)
                                    {
                                        tekiyouStartFlg = false;
                                    }
                                }
                                if (!tekiyouStartFlg)
                                {
                                    M_KOBETSU_HINMEI_TANKA kohitanKisonnEntry = new M_KOBETSU_HINMEI_TANKA();
                                    kohitanKisonnEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());
                                    kohitanKisonnEntry.DELETE_FLG = true;

                                    entitys.Add(kohitanKisonnEntry);

                                    M_KOBETSU_HINMEI_TANKA kohitanEntryadd = new M_KOBETSU_HINMEI_TANKA();
                                    kohitanEntry = kohitaDao.GetDataByCd(SearchOnajiKeyResult.Rows[j]["SYS_ID"].ToString());

                                    //if (this.form.KOSHINNHOUHOU_VALUE.Text == "1")
                                    //{
                                        kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString());
                                    //}
                                    //else if(this.form.KOSHINNHOUHOU_VALUE.Text == "2")
                                    //{
                                        //kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString()) + kohitanEntry.TANKA;
                                    //}

                                    kohitanEntryadd.TEKIYOU_BEGIN = dtXinTekiyouBegin;
                                    kohitanEntryadd.TEKIYOU_END = dtXinTekiyouBegin;
                                    kohitanEntryadd.DENPYOU_KBN_CD = kohitanEntry.DENPYOU_KBN_CD;
                                    kohitanEntryadd.TORIHIKISAKI_CD = kohitanEntry.TORIHIKISAKI_CD;
                                    kohitanEntryadd.GYOUSHA_CD = kohitanEntry.GYOUSHA_CD;
                                    kohitanEntryadd.GENBA_CD = kohitanEntry.GENBA_CD;
                                    kohitanEntryadd.HINMEI_CD = kohitanEntry.HINMEI_CD;
                                    kohitanEntryadd.DENSHU_KBN_CD = kohitanEntry.DENSHU_KBN_CD;
                                    kohitanEntryadd.UNIT_CD = kohitanEntry.UNIT_CD;
                                    kohitanEntryadd.UNPAN_GYOUSHA_CD = kohitanEntry.UNPAN_GYOUSHA_CD;
                                    kohitanEntryadd.NIOROSHI_GYOUSHA_CD = kohitanEntry.NIOROSHI_GYOUSHA_CD;
                                    kohitanEntryadd.NIOROSHI_GENBA_CD = kohitanEntry.NIOROSHI_GENBA_CD;
                                    kohitanEntryadd.BIKOU = this.form.Ichiran.Rows[i].Cells["MEISAI_BIKOU"].Value.ToString();
                                    kohitanEntryadd.SEARCH_TEKIYOU_BEGIN = kohitanEntry.SEARCH_TEKIYOU_BEGIN;
                                    kohitanEntryadd.SEARCH_TEKIYOU_END = kohitanEntry.SEARCH_TEKIYOU_END;
                                    kohitanEntryadd.DELETE_FLG = kohitanEntry.DELETE_FLG;
                                    kohitanEntryadd.CREATE_USER = kohitanEntry.CREATE_USER;

                                    var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(kohitanEntryadd);
                                    dataBinderLogicGenba.SetSystemProperty(kohitanEntryadd, false);
                                    entitys.Add(kohitanEntryadd);
                                }
                                break;

                            }

                            returnCount++;
                        }

                        if (returnCount >= SearchOnajiKeyResult.Rows.Count - 1)
                        {
                            M_KOBETSU_HINMEI_TANKA kohitanEntryadd = new M_KOBETSU_HINMEI_TANKA();
                            kohitanEntry = kohitaDao.GetDataByCd(this.form.Ichiran.Rows[i].Cells["SYS_ID"].Value.ToString());

                            //if (this.form.KOSHINNHOUHOU_VALUE.Text == "1")
                            //{
                                kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString());
                            //}
                            //else if (this.form.KOSHINNHOUHOU_VALUE.Text == "2")
                            //{
                                //kohitanEntryadd.TANKA = Convert.ToDecimal(this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Value.ToString()) + kohitanEntry.TANKA;
                            //}

                            kohitanEntryadd.TEKIYOU_BEGIN = dtXinTekiyouBegin;

                            for (int k = 0; k < SearchOnajiKeyResult.Rows.Count; k++)
                            {
                                if (DateTime.Compare(dtXinTekiyouBegin, Convert.ToDateTime(SearchOnajiKeyResult.Rows[k]["TEKIYOU_BEGIN"])) < 0)
                                {
                                    kohitanEntryadd.TEKIYOU_END = Convert.ToDateTime(SearchOnajiKeyResult.Rows[k]["TEKIYOU_BEGIN"]).AddDays(-1);
                                    break;
                                }
                            }

                            kohitanEntryadd.DENPYOU_KBN_CD = kohitanEntry.DENPYOU_KBN_CD;
                            kohitanEntryadd.TORIHIKISAKI_CD = kohitanEntry.TORIHIKISAKI_CD;
                            kohitanEntryadd.GYOUSHA_CD = kohitanEntry.GYOUSHA_CD;
                            kohitanEntryadd.GENBA_CD = kohitanEntry.GENBA_CD;
                            kohitanEntryadd.HINMEI_CD = kohitanEntry.HINMEI_CD;
                            kohitanEntryadd.DENSHU_KBN_CD = kohitanEntry.DENSHU_KBN_CD;
                            kohitanEntryadd.UNIT_CD = kohitanEntry.UNIT_CD;
                            kohitanEntryadd.UNPAN_GYOUSHA_CD = kohitanEntry.UNPAN_GYOUSHA_CD;
                            kohitanEntryadd.NIOROSHI_GYOUSHA_CD = kohitanEntry.NIOROSHI_GYOUSHA_CD;
                            kohitanEntryadd.NIOROSHI_GENBA_CD = kohitanEntry.NIOROSHI_GENBA_CD;
                            kohitanEntryadd.BIKOU = this.form.Ichiran.Rows[i].Cells["MEISAI_BIKOU"].Value.ToString();
                            kohitanEntryadd.SEARCH_TEKIYOU_BEGIN = kohitanEntry.SEARCH_TEKIYOU_BEGIN;
                            kohitanEntryadd.SEARCH_TEKIYOU_END = kohitanEntry.SEARCH_TEKIYOU_END;
                            kohitanEntryadd.DELETE_FLG = kohitanEntry.DELETE_FLG;
                            kohitanEntryadd.CREATE_USER = kohitanEntry.CREATE_USER;

                            var dataBinderLogicGenba = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI_TANKA>(kohitanEntryadd);
                            dataBinderLogicGenba.SetSystemProperty(kohitanEntryadd, false);

                            var dt = GetDeleteMeisaiData(kohitanEntryadd);
                            if (dt != null && dt.Rows.Count == 1)
                            {
                                // 削除データが1件だけであれば、削除フラグを変更して更新処理とする
                                kohitanEntryadd.SYS_ID = SqlInt64.Parse(dt.Rows[0]["SYS_ID"].ToString());
                                kohitanEntryadd.DELETE_FLG = false;
                                kohitanEntryadd.TIME_STAMP = (byte[])dt.Rows[0]["TIME_STAMP"];
                            }

                            entitys.Add(kohitanEntryadd);
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        private DataTable GetDeleteMeisaiData(M_KOBETSU_HINMEI_TANKA entity)
        {
            MeisaiSearchJoukenDto searchParams = new MeisaiSearchJoukenDto();

            searchParams.TORIHIKISAKI_CD = entity.TORIHIKISAKI_CD;
            searchParams.GYOUSHA_CD = entity.GYOUSHA_CD;
            searchParams.GENBA_CD = entity.GENBA_CD;
            searchParams.HINMEI_CD = entity.HINMEI_CD;
            searchParams.DENSHU_KBN_CD = entity.DENSHU_KBN_CD;
            searchParams.UNIT_CD = entity.UNIT_CD;
            searchParams.UNPAN_GYOUSHA_CD = entity.UNPAN_GYOUSHA_CD;
            searchParams.NIOROSHI_GYOUSHA_CD = entity.NIOROSHI_GYOUSHA_CD;
            searchParams.NIOROSHI_GENBA_CD = entity.NIOROSHI_GENBA_CD;
            searchParams.DENPYOU_KBN_CD = entity.DENPYOU_KBN_CD;
            // 削除したデータを取得
            DataTable deleteTankaData = ichiranDao.GetDeleteMeisaiDataCount(searchParams);
            // 削除済みかつ対象の適用期間内のレコードを保持
            DataTable dt = deleteTankaData.Clone(); 

            if (deleteTankaData != null && deleteTankaData.Rows.Count > 0)
            {
                if (entity.TEKIYOU_BEGIN.Value != null && !entity.TEKIYOU_END.IsNull)
                {
                    for (int i = 0; i < deleteTankaData.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())
                            && !string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString()))
                        {
                            if (DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) == 0)
                            {
                                var newRow = dt.NewRow();
                                newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                                dt.Rows.Add(newRow);
                                continue;
                            }

                            if ((DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) > 0
                                && DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) < 0)
                                || (DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) > 0
                                && DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) < 0))
                            {
                                var newRow = dt.NewRow();
                                newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                                dt.Rows.Add(newRow);
                                continue;
                            }

                        }
                        else if (!string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())
                            && string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString()))
                        {
                            if (DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_END.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) > 0)
                            {
                                var newRow = dt.NewRow();
                                newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                                dt.Rows.Add(newRow);
                                continue;
                            }
                        }
                    }
                }
                else if (entity.TEKIYOU_BEGIN.Value != null && entity.TEKIYOU_END.IsNull)
                {
                    for (int i = 0; i < deleteTankaData.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())
                            && !string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString()))
                        {
                            if (DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) == 0
                                || DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) == 0)
                            {
                                var newRow = dt.NewRow();
                                newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                                dt.Rows.Add(newRow);
                                continue;
                            }

                            if ((DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) > 0
                                && DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString())) < 0)
                                || DateTime.Compare(entity.TEKIYOU_BEGIN.Value, Convert.ToDateTime(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())) < 0)
                            {
                                var newRow = dt.NewRow();
                                newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                                dt.Rows.Add(newRow);
                                continue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_BEGIN"].ToString())
                            && string.IsNullOrEmpty(deleteTankaData.Rows[i]["TEKIYOU_END"].ToString()))
                        {
                            var newRow = dt.NewRow();
                            newRow.ItemArray = deleteTankaData.Rows[i].ItemArray;
                            dt.Rows.Add(newRow);
                            continue;
                        }
                    }
                }
            }

            return dt;
        }

        public static DataTable GetDistinctTable(DataTable dtSource, params string[] columnNames)
        {
            DataTable distinctTable = dtSource.Clone();
            try
            {
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtSource);
                    distinctTable = dv.ToTable(true, columnNames);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            return distinctTable;
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void sort(object sender, EventArgs e)
        {
            DataTable dtSave = new DataTable("dtSave");
            DataColumn dc = null;
            dc = dtSave.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            dc.AllowDBNull = false;
            dc = dtSave.Columns.Add("SYS_ID", Type.GetType("System.String"));
            dc = dtSave.Columns.Add("CHECK_BOX", Type.GetType("System.String"));

            int rowCount = this.form.Ichiran.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow newRow = dtSave.NewRow();
                newRow["SYS_ID"] = this.form.Ichiran.Rows[i].Cells["SYS_ID"].Value;
                newRow["CHECK_BOX"] = this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value;

                dtSave.Rows.Add(newRow);
            }

            this.ShowCustomSortSettingDialog();          

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < dtSave.Rows.Count; j++)
                {
                    string a = dtSave.Rows[j]["SYS_ID"].ToString();
                    string b = this.form.Ichiran.Rows[i].Cells["SYS_ID"].Value.ToString();

                    if (dtSave.Rows[j]["SYS_ID"].ToString() == this.form.Ichiran.Rows[i].Cells["SYS_ID"].Value.ToString())
                    {
                        if (!string.IsNullOrEmpty(dtSave.Rows[j]["CHECK_BOX"].ToString()))
                        {
                            this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value = dtSave.Rows[j]["CHECK_BOX"];
                        }
                        else
                        {
                            this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value = false;
                        }                       
                    }
                }
            }
        }

        /// <summary>
        /// ソート条件のユーザー変更
        /// </summary>
        public void ShowCustomSortSettingDialog()
        {
            if (sortSettingInfo != null && linkedDataGridView != null)
            {
                var dataTable = linkedDataGridView.DataSource as DataTable;

                {
                    sortSettingInfo.SetDataGridViewColumns(linkedDataGridView);
                    var dlg = new SortSettingForm(sortSettingInfo);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.form.customSortHeader1.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
                        if (dataTable != null)
                        {
                            SortDataTable(dataTable);
                        }
                    }
                    dlg.Dispose();
                }

                if (this.form.Ichiran.Rows.Count > 0 && string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.Search();
                    DataTable dt = (DataTable)this.form.Ichiran.DataSource;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Rows.Clear();
                    }
                    this.form.Ichiran.DataSource = dt;
                    this.form.Ichiran.DataSource = this.SearchResult;
                }
            }
        }

        /// <summary>
        /// 検索してSearchResultAllにセット
        /// </summary>
        public void SetSearchResult()
        {
            SetSearchString();
            MeisaiSearchJoukenDto searchParams = new MeisaiSearchJoukenDto();
            searchParams.TORIHIKISAKI_CD = this.SearchString.TORIHIKISAKI_CD;
            searchParams.GYOUSHA_CD = this.SearchString.GYOUSHA_CD;
            searchParams.GENBA_CD = this.SearchString.GENBA_CD;
            searchParams.DENPYOU_KBN_CD = this.SearchString.DENPYOU_KBN_CD;
            searchParams.UNPAN_GYOUSHA_CD = this.SearchString.UNPAN_GYOUSHA_CD;
            searchParams.NIOROSHI_GYOUSHA_CD = this.SearchString.NIOROSHI_GYOUSHA_CD;
            searchParams.NIOROSHI_GENBA_CD = this.SearchString.NIOROSHI_GENBA_CD;
            searchParams.HINMEI_CD = this.SearchString.HINMEI_CD;

            if (!string.IsNullOrEmpty(this.form.UNIT_CD.Text))
            {
                searchParams.UNIT_CD = Convert.ToInt16(this.form.UNIT_CD.Text);
            }

            searchParams.SHURUI_CD = this.SearchString.SHURUI_CD;
            searchParams.BUNRUI_CD = this.SearchString.BUNRUI_CD;
            searchParams.TANK_FROM = this.SearchString.TANK_FROM;
            searchParams.TANK_TO = this.SearchString.TANK_TO;
            searchParams.TANK_TEKIYOU_BEGIN_START = this.SearchString.TANK_TEKIYOU_BEGIN_START;
            searchParams.TANK_TEKIYOU_BEGIN_END = this.SearchString.TANK_TEKIYOU_BEGIN_END;
            searchParams.TANK_TEKIYOU_END_START = this.SearchString.TANK_TEKIYOU_END_START;
            searchParams.TANK_TEKIYOU_END_END = this.SearchString.TANK_TEKIYOU_END_END;
            searchParams.TANK_FROM = this.SearchString.TANK_FROM;
            searchParams.TANK_TO = this.SearchString.TANK_TO;

            this.SearchResult = ichiranDao.GetIchiranData(searchParams);
            this.SearchResult.Columns["XIN_TANKA"].ReadOnly = false;
            this.SearchResult.Columns["XIN_TANKA"].MaxLength = 12;
            this.SearchResult.Columns["XIN_TEKIYOU_BEGIN"].ReadOnly = false;
            this.SearchResult.Columns["XIN_TEKIYOU_BEGIN"].MaxLength = 60;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                if (this.SearchResult.Rows != null)
                {
                    this.form.headerForm.ReadDataNumber.Text = this.SearchResult.Rows.Count.ToString();
                }
                else
                {
                    this.form.headerForm.ReadDataNumber.Text = "0";
                }

                DataTable dt = (DataTable)this.form.Ichiran.DataSource;

                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.SortDataTable(this.SearchResult);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows.Clear();
                }
                
                this.form.Ichiran.DataSource = dt;

                if (!string.IsNullOrEmpty(this.form.customSortHeader1.txboxSortSettingInfo.Text))
                {
                    this.form.Ichiran.DataSource = this.dtSort;
                }
                else
                {
                    this.form.Ichiran.DataSource = this.SearchResult;
                }
                
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func9.Enabled = true;

                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetIchiran", ex2);
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ソート条件のユーザー変更
        /// </summary>
        public void SortDataTable(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return;
            }

            if (sortSettingInfo == null)
            {
                return;
            }

            sortSettingInfo.SetDataTableColumns(dataTable);
            this.form.customSortHeader1.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
            var sb = new System.Text.StringBuilder();

            foreach (var item in sortSettingInfo.SortColumns)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat("{0} {1}", item.Name, item.IsAsc ? "ASC" : "DESC");
            }

            dataTable.DefaultView.Sort = sb.ToString();

            this.dtSort = dataTable.DefaultView.ToTable();
        }
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.isRegist = true;
            this.entitysFlg = true;
            try
            {
                if(this.entitys == null || this.entitys.Count <= 0)
                {
                    entitysFlg = false;
                }

                // 削除したデータと同じデータを登録できない。
                if (!this.CheckDeleteMeisaiData())
                {
                    return;
                }

                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_KOBETSU_HINMEI_TANKA kobetsuEntity in this.entitys)
                        {
                            M_KOBETSU_HINMEI_TANKA entity = null;

                            // SYS_IDの値判断
                            if (!kobetsuEntity.SYS_ID.IsNull)
                            {
                                entity = this.kohitaDao.GetDataByCd(kobetsuEntity.SYS_ID.ToString());

                                if (entity == null)
                                {
                                    this.kohitaDao.Insert(kobetsuEntity);
                                }
                                else
                                {
                                    if (kobetsuEntity.DELETE_FLG == true)
                                    {
                                        this.kohitaDao.Delete(kobetsuEntity);
                                    }
                                    else
                                    {
                                       this.kohitaDao.Update(kobetsuEntity);
                                    }   
                                }
                            }
                            else
                            {
                                // MAXのSYS_IDを取得する
                                Int64 sysId = Convert.ToInt64(this.kohitaDao.GetMaxPlusKey());
                                kobetsuEntity.SYS_ID = sysId;
                                this.kohitaDao.Insert(kobetsuEntity);
                            }
                            
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    LogUtility.Warn(ex); //排他は警告
                    this.form.msglogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.msglogic.MessageBoxShow("E093");
                }
                this.isRegist = false;
            }

            if (this.isRegist && this.entitysFlg)
                this.form.msglogic.MessageBoxShow("I001", "登録");

            LogUtility.DebugMethodEnd(errorFlag);
        }

        /// <summary>
        /// 削除したデータと同じデータ処理
        /// </summary>
        /// /// <param name="errorFlag"></param>
        public bool CheckDeleteMeisaiData()
        {
            // 削除したデータと同じデータを登録できない。
            foreach (M_KOBETSU_HINMEI_TANKA kobetsuEntity in this.entitys)
            {
                if (kobetsuEntity.DELETE_FLG == false)
                {
                    var delDt = GetDeleteMeisaiData(kobetsuEntity);

                    if (delDt != null && 2 <= delDt.Rows.Count)
                    {
                        // 1件の時は削除フラグを更新するので修正可
                        // 2件レコード以上あれば修正不可なのでエラー
                        this.isRegist = false;
                    }
                    
                    if (!this.isRegist)
                    {
                        int rowCount = this.form.Ichiran.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            MeisaiSearchJoukenDto CompareParams = null;

                            if (this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null
                                && bool.Parse(this.form.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                            {
                                CompareParams = new MeisaiSearchJoukenDto();
                                CompareParams.HINMEI_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_HINMEI_CD"].Value.ToString();
                                CompareParams.UNIT_CD = Convert.ToInt16(this.form.Ichiran.Rows[i].Cells["MEISAI_UNIT_CD"].Value.ToString());
                                CompareParams.DENSHU_KBN_CD = Convert.ToInt16(this.form.Ichiran.Rows[i].Cells["DENSHU_KBN_CD"].Value.ToString());

                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_UNPAN_GYOUSHA_CD"].Value != null)
                                {
                                    CompareParams.UNPAN_GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_UNPAN_GYOUSHA_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.UNPAN_GYOUSHA_CD = "";
                                }
                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GYOUSHA_CD"].Value != null)
                                {
                                    CompareParams.NIOROSHI_GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GYOUSHA_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.NIOROSHI_GYOUSHA_CD = "";
                                }
                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GENBA_CD"].Value != null)
                                {
                                    CompareParams.NIOROSHI_GENBA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_NIOROSHI_GENBA_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.NIOROSHI_GENBA_CD = "";
                                }

                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_TORIHIKISAKI_CD"].Value != null)
                                {
                                    CompareParams.TORIHIKISAKI_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_TORIHIKISAKI_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.TORIHIKISAKI_CD = "";
                                }

                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_GYOUSHA_CD"].Value != null)
                                {
                                    CompareParams.GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_GYOUSHA_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.GYOUSHA_CD = "";
                                }

                                if (this.form.Ichiran.Rows[i].Cells["MEISAI_GENBA_CD"].Value != null)
                                {
                                    CompareParams.GENBA_CD = this.form.Ichiran.Rows[i].Cells["MEISAI_GENBA_CD"].Value.ToString();
                                }
                                else
                                {
                                    CompareParams.GENBA_CD = "";
                                }

                                if (kobetsuEntity.TORIHIKISAKI_CD.Equals(CompareParams.TORIHIKISAKI_CD)
                                        && kobetsuEntity.GYOUSHA_CD.Equals(CompareParams.GYOUSHA_CD)
                                        && kobetsuEntity.GENBA_CD.Equals(CompareParams.GENBA_CD)
                                        && kobetsuEntity.HINMEI_CD.Equals(CompareParams.HINMEI_CD)
                                        && kobetsuEntity.DENSHU_KBN_CD.Equals(CompareParams.DENSHU_KBN_CD)
                                        && kobetsuEntity.UNIT_CD.Equals(CompareParams.UNIT_CD)
                                        && kobetsuEntity.UNPAN_GYOUSHA_CD.Equals(CompareParams.UNPAN_GYOUSHA_CD)
                                        && kobetsuEntity.NIOROSHI_GYOUSHA_CD.Equals(CompareParams.NIOROSHI_GYOUSHA_CD)
                                        && kobetsuEntity.NIOROSHI_GENBA_CD.Equals(CompareParams.NIOROSHI_GENBA_CD))
                                {
                                    this.form.Ichiran.CurrentCell = this.form.Ichiran.Rows[i].Cells["UTSUTSU_TANKA"];
                                    this.form.Ichiran.Rows[i].Cells["XIN_TANKA"].Style.BackColor = Constans.ERROR_COLOR;
                                }
                            }
                        }

                        msgLogic.MessageBoxShow("E013");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.CancelInit();
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.msglogic.MessageBoxShow("E093");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.msglogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// クリア
        /// </summary>
        public bool ClearCondition()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.form.DenpyouKubun.Text = "1";
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_RNAME.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_RNAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.HINMEI_CD.Text = string.Empty;
                this.form.HINMEI_NAME.Text = string.Empty;
                this.form.SHURUI_CD.Text = string.Empty;
                this.form.SHURUI_NAME.Text = string.Empty;
                this.form.BUNRUI_CD.Text = string.Empty;
                this.form.BUNRUI_NAME.Text = string.Empty;
                this.form.TANK_FROM.Text = string.Empty;
                this.form.TANK_TO.Text = string.Empty;
                this.form.TANK_TEKIYOU_BEGIN_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_BEGIN_END.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_END.Value = string.Empty;

                this.form.UNIT_CD.Text = string.Empty;
                this.form.UNIT_NAME.Text = string.Empty;

                this.ClearCustomSortSetting();

                DataTable dt = (DataTable)this.form.Ichiran.DataSource;
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows.Clear();
                }
                this.form.Ichiran.DataSource = dt;
                this.form.BaseForm.bt_func9.Enabled = false;
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.form.msglogic.MessageBoxShow("E093");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.form.msglogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region データ設定

        /// <summary>
        ///  画面初期値設定
        /// </summary>
        private void DefaultInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.TORIHIKISAKI_CD.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
                this.form.GYOUSHA_CD.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GYOUSHA_CD_TEXT;
                this.form.GENBA_CD.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GENBA_CD_TEXT;
                this.form.TORIHIKISAKI_RNAME.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT;
                this.form.GYOUSHA_RNAME.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;
                this.form.GENBA_RNAME.Text = Shougun.Core.Master.KobetsuHimeiTankaUpdate.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT;

                this.form.DenpyouKubun.Text = "1";
                this.form.KOSHINNHOUHOU_VALUE.Text = "1";
                this.form.TANK_TEKIYOU_BEGIN_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_BEGIN_END.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_END.Value = string.Empty;
                this.form.TANK_TEKIYOU.Value = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
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
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        /// <summary>
        /// ソート条件のクリア
        /// </summary>
        public void ClearCustomSortSetting()
        {
            if (this.sortSettingInfo != null)
            {
                this.sortSettingInfo.Clear();
                this.form.customSortHeader1.txboxSortSettingInfo.Text = sortSettingInfo.SortSettingCaption;
            }
        }

        /// <summary>
        ///  ヘットタイトル設定
        /// </summary>
        /// <param name="type">1、受入　2、出荷</param>
        public void HearderInit()
        {
            var lblTitle = new ControlUtility().FindControl(this.form.BaseForm, "lb_title");
            if (lblTitle != null)
                lblTitle.Text = this.form.WindowId.ToTitleString();

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.form.headerForm.AlertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }
        }

        /// <summary>
        ///  取り消し設定
        /// </summary>
        private void CancelInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.DenpyouKubun.Text = "1";
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_RNAME.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_RNAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.form.HINMEI_CD.Text = string.Empty;
                this.form.HINMEI_NAME.Text = string.Empty;
                this.form.SHURUI_CD.Text = string.Empty;
                this.form.SHURUI_NAME.Text = string.Empty;
                this.form.BUNRUI_CD.Text = string.Empty;
                this.form.BUNRUI_NAME.Text = string.Empty;
                this.form.TANK_FROM.Text = string.Empty;
                this.form.TANK_TO.Text = string.Empty;
                this.form.TANK_TEKIYOU_BEGIN_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_BEGIN_END.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_START.Value = string.Empty;
                this.form.TANK_TEKIYOU_END_END.Value = string.Empty;

                this.form.UNIT_CD.Text = string.Empty;
                this.form.UNIT_NAME.Text = string.Empty;

                this.ClearCustomSortSetting();

                DataTable dt = (DataTable)this.form.Ichiran.DataSource;
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows.Clear();
                }
                this.form.Ichiran.DataSource = dt;
                this.form.BaseForm.bt_func9.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DefaultInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            MeisaiSearchJoukenDto entity = new MeisaiSearchJoukenDto();

            // 取引先の検索条件の設定
            entity.SetValue(this.form.TORIHIKISAKI_CD);           
            // 業者の検索条件の設定
            entity.SetValue(this.form.GYOUSHA_CD);
            // 現場の検索条件の設定
            entity.SetValue(this.form.GENBA_CD);
            // 運搬業者の検索条件の設定
            entity.SetValue(this.form.UNPAN_GYOUSHA_CD);
            // 荷降先業者の検索条件の設定
            entity.SetValue(this.form.NIOROSHI_GYOUSHA_CD);
            // 荷降先現場の検索条件の設定
            entity.SetValue(this.form.NIOROSHI_GENBA_CD);
            // 品名の検索条件の設定
            entity.SetValue(this.form.HINMEI_CD);
            // 種類の検索条件の設定
            entity.SetValue(this.form.SHURUI_CD);
            // 分類の検索条件の設定
            entity.SetValue(this.form.BUNRUI_CD);
            // 伝票区分の検索条件の設定
            entity.DENPYOU_KBN_CD = Convert.ToInt16(this.form.DenpyouKubun.Text);

            this.denpyouKbn = this.form.DenpyouKubun.Text;

            if (this.form.TANK_TEKIYOU_BEGIN_START.Value != null)
            {
                entity.TANK_TEKIYOU_BEGIN_START = Convert.ToDateTime(this.form.TANK_TEKIYOU_BEGIN_START.Value);
            }
            // 単価適用開始日の検索条件の設定
            if (this.form.TANK_TEKIYOU_BEGIN_END.Value != null)
            {
                entity.TANK_TEKIYOU_BEGIN_END = Convert.ToDateTime(this.form.TANK_TEKIYOU_BEGIN_END.Value);
            }
            if (this.form.TANK_TEKIYOU_END_START.Value != null)
            {
                entity.TANK_TEKIYOU_END_START = Convert.ToDateTime(this.form.TANK_TEKIYOU_END_START.Value);
            }
            // 単価適用終了日の検索条件の設定
            if (this.form.TANK_TEKIYOU_END_END.Value != null)
            {
                entity.TANK_TEKIYOU_END_END = Convert.ToDateTime(this.form.TANK_TEKIYOU_END_END.Value);
            }
            if (!string.IsNullOrEmpty(this.form.TANK_FROM.Text))
            {
                entity.TANK_FROM = Convert.ToDecimal(this.form.TANK_FROM.Text);
            }
            if (!string.IsNullOrEmpty(this.form.TANK_TO.Text))
            {
                entity.TANK_TO = Convert.ToDecimal(this.form.TANK_TO.Text);
            }                
            this.SearchString = entity;
        }

        #endregion
      
        #region IBuisinessLogicで必須実装(論理削除以外未使用)

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete(List<M_KAISHI_ZAIKO_INFO> listDelete)
        {
            LogUtility.DebugMethodStart(listDelete);
            try
            {
                            
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.form.msglogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //DBエラー
                    this.form.msglogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.msglogic.MessageBoxShow("E245");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(listDelete);
            }
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

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
                    this.msgLogic = new MessageBoxShowLogic();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                    DataTable dt = this.unpanGyoushaDAO.GetUnpanGyoushaData(gyousha);
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
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchUnpanGyoushaName", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            
        }

        /// <summary>
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
                    this.msgLogic = new MessageBoxShowLogic();
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                    DataTable dt = this.nioroshiGyoushaDAO.GetNioroshiGyoushaData(gyousha);
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
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchNioroshiGyoushaName", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
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
                    DataTable dt = this.nioroshiGenbaDAO.GetNioroshiGenbaData(genba);
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
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchNioroshiGenbaName", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 業者名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        //public virtual bool SearchGyoushaName(CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart();

        //        if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
        //        {
        //            this.SearchGenbaName(null);
        //        }
        //        else if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
        //        {
        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        //            M_GYOUSHA tmp = new M_GYOUSHA();
        //            tmp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
        //            this.SearchResultGyousha = gyoushaDao_1.GetGyoushaData(tmp);

        //            if (this.SearchResultGyousha != null && this.SearchResultGyousha.Rows.Count > 0)
        //            {
        //                DataRow row = this.SearchResultGyousha.Rows[0];
        //                this.form.GYOUSHA_NAME_RYAKU.Text = row["GYOUSHA_NAME_RYAKU"].ToString();

        //                if (!this.form.beforGyousaCD.Equals(this.form.GYOUSHA_CD.Text))
        //                {
        //                    if (row["TORIHIKISAKI_UMU_KBN"].ToString() == "1")
        //                    {
        //                        M_TORIHIKISAKI tmpTorihikisaki = new M_TORIHIKISAKI();
        //                        tmpTorihikisaki.TORIHIKISAKI_CD = row["TORIHIKISAKI_CD"].ToString();
        //                        this.SearchResultTorihikisaki = torihikisakiDao_1.GetTorihikisakiData(tmpTorihikisaki);

        //                        if (this.SearchResultTorihikisaki != null && this.SearchResultTorihikisaki.Rows.Count > 0)
        //                        {
        //                            DataRow rowTorihikisaki = this.SearchResultTorihikisaki.Rows[0];
        //                            this.form.TORIHIKISAKI_CD.Text = rowTorihikisaki["TORIHIKISAKI_CD"].ToString();
        //                            this.form.TORIHIKISAKI_RNAME.Text = rowTorihikisaki["TORIHIKISAKI_NAME_RYAKU"].ToString();
        //                        }
        //                        else
        //                        {
        //                            this.form.TORIHIKISAKI_CD.Text = string.Empty;
        //                            this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        this.form.TORIHIKISAKI_CD.Text = string.Empty;
        //                        this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                if (e != null)
        //                {
        //                    this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
        //                    msgLogic.MessageBoxShow("E020", "業者");
        //                    e.Cancel = true;
        //                }
        //            }
        //        }

        //        LogUtility.DebugMethodEnd();
        //        return false;
        //    }
        //    catch (SQLRuntimeException ex2)
        //    {
        //        LogUtility.Error("SearchGyoushaName", ex2);
        //        this.form.msglogic.MessageBoxShow("E093", "");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("SearchGyoushaName", ex);
        //        this.form.msglogic.MessageBoxShow("E245", "");
        //        LogUtility.DebugMethodEnd();
        //        return true;
        //    }
        //}

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
                    this.msgLogic = new MessageBoxShowLogic();
                    M_HINMEI hinmei = new M_HINMEI();
                    hinmei.HINMEI_CD = this.form.HINMEI_CD.Text;
                    DataTable dt = this.hinmeiDAO.GetHinmeiData(hinmei);
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
                this.form.msglogic.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmeiName", ex);
                this.form.msglogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #region 単価チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool TankaCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.TANK_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.TANK_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.TANK_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TANK_TO.Text))
            {
                return false;
            }

            decimal tank_from = Convert.ToDecimal(this.form.TANK_FROM.Text);
            decimal tank_to = Convert.ToDecimal(this.form.TANK_TO.Text);

            // 単価FROM > 単価TO 場合
            if (tank_to.CompareTo(tank_from) < 0)
            {
                this.form.TANK_FROM.IsInputErrorOccured = true;
                this.form.TANK_TO.IsInputErrorOccured = true;
                this.form.TANK_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.TANK_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "単価From", "単価To" };
                msgLogic.MessageBoxShow("E279", errorMsg);
                this.form.TANK_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateStrarCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.TANK_TEKIYOU_BEGIN_START.BackColor = Constans.NOMAL_COLOR;
            this.form.TANK_TEKIYOU_BEGIN_END.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.TANK_TEKIYOU_BEGIN_START.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TANK_TEKIYOU_BEGIN_END.Text))
            {
                return false;
            }

            DateTime date_from_start = DateTime.Parse(this.form.TANK_TEKIYOU_BEGIN_START.Text);
            DateTime date_to_start = DateTime.Parse(this.form.TANK_TEKIYOU_BEGIN_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to_start.CompareTo(date_from_start) < 0)
            {
                this.form.TANK_TEKIYOU_BEGIN_START.IsInputErrorOccured = true;
                this.form.TANK_TEKIYOU_BEGIN_END.IsInputErrorOccured = true;
                this.form.TANK_TEKIYOU_BEGIN_START.BackColor = Constans.ERROR_COLOR;
                this.form.TANK_TEKIYOU_BEGIN_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "適用期間From", "適用期間To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.TANK_TEKIYOU_BEGIN_START.Focus();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateEndCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.TANK_TEKIYOU_END_START.BackColor = Constans.NOMAL_COLOR;
            this.form.TANK_TEKIYOU_END_END.BackColor = Constans.NOMAL_COLOR;

            if (string.IsNullOrEmpty(this.form.TANK_TEKIYOU_END_START.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TANK_TEKIYOU_END_END.Text))
            {
                return false;
            }

            DateTime date_from_end = DateTime.Parse(this.form.TANK_TEKIYOU_END_START.Text);
            DateTime date_to_end = DateTime.Parse(this.form.TANK_TEKIYOU_END_END.Text);

            //nullチェック
            
            // 日付FROM > 日付TO 場合
            if (date_to_end.CompareTo(date_from_end) < 0)
            {
                this.form.TANK_TEKIYOU_END_START.IsInputErrorOccured = true;
                this.form.TANK_TEKIYOU_END_END.IsInputErrorOccured = true;
                this.form.TANK_TEKIYOU_END_START.BackColor = Constans.ERROR_COLOR;
                this.form.TANK_TEKIYOU_END_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "適用期間From", "適用期間To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.TANK_TEKIYOU_END_START.Focus();
                return true;
            }

            return false;
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
            int.TryParse(this.form.headerForm.AlertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            return result;
        }

        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        public bool CreateDataGridView(DataTable table)
        {
            if (this.AlertCount != 0 && this.AlertCount < table.Rows.Count)
            {
                // 件数アラート
                DialogResult result = this.form.msglogic.MessageBoxShow("C025");
                if (result != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}