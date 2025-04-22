using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.CustomControl.DataGridCustomControl;
using Seasar.Framework.Exceptions;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// 一覧DataGridView
        /// </summary>
        //private DataGridView myGridView;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 定期配車入力のDao
        /// </summary>
        private IT_TEIKI_HAISHA_ENTRYDao teikiHaishaEntryDao;

        /// <summary>
        /// 定期配車明細のDao
        /// </summary>
        private IT_TEIKI_HAISHA_DETAILDao teikiHaishaDetailDao;

        /// <summary>
        /// 定期配車荷降のDao
        /// </summary>
        private IT_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioroshiDao;

        /// <summary>
        /// 定期配車詳細のDao
        /// </summary>
        private IT_TEIKI_HAISHA_SHOUSAIDao teikiHaishaShousaiDao;

        /// <summary>
        /// コース明細のDao
        /// </summary>
        private IM_COURSE_DETAILDao courseDetailDao;

        /// <summary>
        /// コース_荷降Dao
        /// </summary>
        private IM_COURSE_NIOROSHIDao courseNioroshiDao;

        /// <summary>
        /// コース_明細内訳のDao
        /// </summary>
        private IM_COURSE_DETAIL_ITEMSDao courseDetailItemsDao;

        /// <summary>
        /// コースマスタ Dao
        /// </summary>
        private M_COURSEDao courseDao;

        /// <summary>
        /// コースマスタ Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseNameDao;

        /// <summary>
        /// 車輌マスタ Dao
        /// </summary>
        private M_SHARYOUDao sharyouDao;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 社員Dao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        // 20141015 koukouei 休動管理機能 start
        /// <summary>
        /// 運転者休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workClosedUntenshaDao;
        /// <summary>
        /// 車輌休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workClosedSharyouDao;
        // 20141015 koukouei 休動管理機能 start

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 定期配車入力リスト(更新用)
        /// </summary>
        private List<T_TEIKI_HAISHA_ENTRY> entityEntryListForUpdate;

        /// <summary>
        /// 定期配車入力リスト(登録用)
        /// </summary>
        private List<T_TEIKI_HAISHA_ENTRY> entityEntryList;

        /// <summary>
        /// 定期配車明細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_DETAIL> entityHaishaDetailList;

        /// <summary>
        /// 定期配車荷降リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_NIOROSHI> entityHaishaNioroshiList;

        /// <summary>
        /// 定期配車明細内訳リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_SHOUSAI> entityHaishaShousaiList;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 新規検索結果
        /// </summary>
        public DataTable ShinkiSearchResult;

        /// <summary>
        /// 登録済み検索結果
        /// </summary>
        public DataTable ZumiSearchResult;

        /// <summary>
        /// Mコース明細検索結果
        /// </summary>
        public DataTable courseDetailResult;

        /// <summary>
        /// Mコース荷降検索結果
        /// </summary>
        public DataTable courseNioroshiResult;

        /// <summary>
        /// Mコース明細内訳検索結果
        /// </summary>
        public DataTable courseDetailItemsResult;

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        /// <summary>
        /// 車輌CD前回値
        /// </summary>
        private string oldSharyouCD;

        internal string beforeCd;
        internal string beforeDayCd;
        internal bool isInputError;

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResultEntry { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass searchString { get; set; }

        /// <summary>
        /// システム情報
        /// </summary>
        private M_SYS_INFO entitysM_SYS_INFO { get; set; }

        /// <summary>
        /// 定期配車入力テーブルの情報
        /// </summary>
        //private T_TEIKI_HAISHA_ENTRY entitysT_TEIKI_HAISHA_ENTRY { get; set; }

        /// <summary>
        /// 定期配車明細テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_DETAIL entitysT_TEIKI_HAISHA_DETAIL { get; set; }

        /// <summary>
        /// 定期配車明細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_DETAIL> entityDetailList { get; set; }

        /// <summary>
        /// 定期配車荷卸テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_NIOROSHI entitysT_TEIKI_HAISHA_NIOROSHI { get; set; }

        /// <summary>
        /// 定期配車荷卸リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_NIOROSHI> entityNioroshilList { get; set; }

        /// <summary>
        /// 定期配車詳細テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_SHOUSAI entitysT_TEIKI_HAISHA_SHOUSAI { get; set; }

        /// <summary>
        /// 検索結果（定期配車入力）
        /// </summary>
        public DataTable searchResultCourseDetail { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// 伝種採番Dao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;
        #endregion

        #region 初期化処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.msgLogic = new MessageBoxShowLogic();

            this.teikiHaishaEntryDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();

            this.oldSharyouCD = string.Empty;

            //定期配車明細DAO初期化
            this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_DETAILDao>();
            //定期配車荷降DAO初期化
            this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_NIOROSHIDao>();
            //定期配車明細DAO初期化
            this.teikiHaishaShousaiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_SHOUSAIDao>();
            //コース明細DAO初期化
            this.courseDetailDao = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();
            //コース荷降DAO初期化
            this.courseNioroshiDao = DaoInitUtility.GetComponent<IM_COURSE_NIOROSHIDao>();
            //コース明細内訳DAO初期化
            this.courseDetailItemsDao = DaoInitUtility.GetComponent<IM_COURSE_DETAIL_ITEMSDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            //コースDAO初期化
            this.courseDao = DaoInitUtility.GetComponent<M_COURSEDao>();
            //車輌DAO初期化
            this.sharyouDao = DaoInitUtility.GetComponent<M_SHARYOUDao>();
            // 拠点CD
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            // 業者Dao
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            // 社員Dao
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            // 20141015 koukouei 休動管理機能 start
            // 車輌休動マスタDao
            this.workClosedSharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            // 運転者休動マスタDao
            this.workClosedUntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            // 20141015 koukouei 休動管理機能 end
            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
            this.msgLogic = new MessageBoxShowLogic();
            this.courseNameDao = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ベースフォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.SAGYOU_DATE_FROM.Value = parentForm.sysDate;
                this.form.SAGYOU_DATE_TO.Value = parentForm.sysDate;

                // 抽出設定初期化
                this.form.OUTPUT_KBN_VALUE.Text = "1";

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 拠点初期値設定
                this.SetInitKyoten();

                // 検索する前に明細行が入力できると入力チェック等が実行されてしまうため、非活性にする
                this.form.DetailIchiran.Enabled = false;
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
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
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
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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
        #endregion

        #region 拠点設定
        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.form.txt_KyotenCD.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text.ToString()))
            {
                this.form.txt_KyotenCD.Text = this.form.txt_KyotenCD.Text.ToString().PadLeft(this.form.txt_KyotenCD.MaxLength, '0');
                this.CheckKyotenCd();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile">ＸＭＬファイルにアクセスするためのクラス</param>
        /// <param name="key">キー</param>
        /// <returns>キーに紐づく値</returns>
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

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.txt_KyotenName.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
                {
                    this.form.txt_KyotenName.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.form.txt_KyotenCD.Text, out kyoteCd))
                {
                    return;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = kyoteCd;
                var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "拠点");
                    //this.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.form.txt_KyotenName.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
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

        #region イベントの初期化処理
        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                //customTextBoxでのエンターキー押下イベント生成
                parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);

                //検索ボタン(F8)イベント生成
                this.form.C_Regist(parentForm.bt_func8);
                parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
                //登録(F9)イベント生成
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                //全明細を選択
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);
                //全明細を解除
                parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);

                // 20141127 teikyou ダブルクリックを追加する　start
                this.form.SAGYOU_DATE_TO.MouseDoubleClick += new MouseEventHandler(SAGYOU_DATE_TO_MouseDoubleClick);
                // 20141127 teikyou ダブルクリックを追加する　end
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

        #endregion

        #region Functionボタン 押下処理
        #region F8 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.dto = new DTOClass();

                //画面の検索条件をDTOにセット
                dto.SagyouDateFrom = DateTime.Parse(this.form.SAGYOU_DATE_FROM.Value.ToString()).Date.ToShortDateString();
                dto.SagyouDateTo = DateTime.Parse(this.form.SAGYOU_DATE_TO.Value.ToString()).Date.ToShortDateString();
                dto.ChusyutsuSettei = int.Parse(this.form.radbtnShinkiNomi.Value);
                dto.KyotenCd = this.form.txt_KyotenCD.Text;

                //新規のみ場合
                if (this.form.radbtnShinkiNomi.Checked)
                {
                    this.ShinkiSearchResult = SearchShinkiResult(this.dto, this.form.SAGYOU_DATE_FROM.Value.ToString(), this.form.SAGYOU_DATE_TO.Value.ToString());

                    // 件数
                    result = this.ShinkiSearchResult.Rows.Count;
                }

                //登録済み場合
                if (this.form.radbtnTourokuZumi.Checked)
                {
                    this.ShinkiSearchResult = SearchShinkiResult(this.dto, this.form.SAGYOU_DATE_FROM.Value.ToString(), this.form.SAGYOU_DATE_TO.Value.ToString());
                    this.ZumiSearchResult = teikiHaishaEntryDao.GetAllData(this.dto);

                    // 件数
                    result = this.ShinkiSearchResult.Rows.Count + this.ZumiSearchResult.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                result = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }
        #endregion

        #region 検索結果を一覧に設定
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        /// <returns></returns>
        public void SetIchiran(int count)
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            this.form.DetailIchiran.Rows.Clear();

            //一覧にデータ分の行数をAddする
            this.form.DetailIchiran.Rows.Add(count);

            // 新規データの設定開始行目
            int shinkiSetIndex = 0;

            #region 新規のみ表示
            //新規のみ表示

            // 新規結果を設定する
            var shinkiTable = this.ShinkiSearchResult;

            //検索結果設定
            for (int i = 0; i < shinkiTable.Rows.Count; i++)
            {

                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TOUROKU_FLG"].Value = false;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SAGYOU_DATE"].Value = shinkiTable.Rows[i]["SAGYOU_DATE"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SAGYOU_DATE"].ReadOnly = true;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TEIKI_JISSEKI_NUMBER"].Value = String.Empty;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TEIKI_HAISHA_NUMBER"].Value = String.Empty;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["COURSE_NAME_CD"].Value = shinkiTable.Rows[i]["COURSE_NAME_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["COURSE_NAME_RYAKU"].Value = shinkiTable.Rows[i]["COURSE_NAME_RYAKU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["KYOTEN_CD"].Value = shinkiTable.Rows[i]["KYOTEN_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["KYOTEN_NAME_RYAKU"].Value = shinkiTable.Rows[i]["KYOTEN_NAME_RYAKU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHASHU_CD"].Value = shinkiTable.Rows[i]["SHASHU_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHASHU_NAME_RYAKU"].Value = shinkiTable.Rows[i]["SHASHU_NAME_RYAKU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHARYOU_CD"].Value = shinkiTable.Rows[i]["SHARYOU_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHARYOU_NAME_RYAKU"].Value = shinkiTable.Rows[i]["SHARYOU_NAME_RYAKU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNTENSHA_CD"].Value = shinkiTable.Rows[i]["UNTENSHA_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNTENSHA_NAME_RYAKU"].Value = shinkiTable.Rows[i]["UNTENSHA_NAME_RYAKU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["HOJOIN_CD"].Value = String.Empty;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["HOJOIN_NAME_RYAKU"].Value = String.Empty;
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNPAN_GYOUSHA_CD"].Value = shinkiTable.Rows[i]["UNPAN_GYOUSHA_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNPAN_GYOUSHA_NAME"].Value = shinkiTable.Rows[i]["UNPAN_GYOUSHA_NAME"];
                //HIDDEN属性
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["DAY_CD"].Value = shinkiTable.Rows[i]["DAY_CD"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["COURSE_BIKOU"].Value = shinkiTable.Rows[i]["COURSE_BIKOU"];
                this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["MOD_FLG"].Value = "1";
                //this.form.DetailIchiran.Rows[i].Cells["TIME_STAMP"].Value = shinkiTable.Rows[i]["TIME_STAMP"];
            }

            #endregion

            #region 登録済みも表示
            //登録済みも表示
            if (this.form.radbtnTourokuZumi.Checked)
            {
                // 登録済み検索結果を設定する
                var zumiTable = this.ZumiSearchResult;

                shinkiSetIndex = shinkiTable.Rows.Count;
                //検索結果設定
                for (int i = 0; i < zumiTable.Rows.Count; i++)
                {

                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SAGYOU_DATE"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SAGYOU_DATE"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SAGYOU_DATE"].ReadOnly = true;
                    if (string.IsNullOrEmpty(zumiTable.Rows[i]["TEIKI_JISSEKI_NUMBER"].ToString()))
                    {
                        this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TEIKI_JISSEKI_NUMBER"].Value = string.Empty;
                    }
                    else
                    {
                        this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TEIKI_JISSEKI_NUMBER"].Value = "済";
                        this.form.DetailIchiran.Rows[shinkiSetIndex + i].ReadOnly = true;
                    }

                    if (!zumiTable.Rows[i]["TEIKI_HAISHA_NUMBER"].ToString().Equals(string.Empty))
                    {
                        this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TEIKI_HAISHA_NUMBER"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["TEIKI_HAISHA_NUMBER"], string.Empty);
                        if (!this.RenkeiCheck(zumiTable.Rows[i]["TEIKI_HAISHA_NUMBER"].ToString(), false))
                        {
                            this.form.DetailIchiran.Rows[shinkiSetIndex + i].ReadOnly = true;
                            this.form.DetailIchiran.Refresh();
                        }
                    }
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["COURSE_NAME_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["COURSE_NAME_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["COURSE_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["COURSE_NAME_RYAKU"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["KYOTEN_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["KYOTEN_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["KYOTEN_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["KYOTEN_NAME_RYAKU"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHASHU_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHASHU_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHASHU_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHASHU_NAME_RYAKU"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHARYOU_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHARYOU_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["SHARYOU_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHARYOU_NAME_RYAKU"], string.Empty);

                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNTENSHA_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["UNTENSHA_CD"], string.Empty);

                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNTENSHA_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHAIN_NAME_RYAKU"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["HOJOIN_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["HOJOIN_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["HOJOIN_NAME_RYAKU"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["SHAIN_NAME_RYAKU1"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNPAN_GYOUSHA_CD"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["UNPAN_GYOUSHA_CD"], string.Empty);
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["UNPAN_GYOUSHA_NAME"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["UNPAN_GYOUSHA_NAME"], string.Empty);
                    //HIDDEN属性　(登録済みは配車テーブル使うためDAY_CDなどない)
                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["DAY_CD"].Value = zumiTable.Rows[i]["DAY_CD"];

                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["TIME_STAMP"].Value = this.ChgDBNullToValue(zumiTable.Rows[i]["TIME_STAMP"], string.Empty);

                    this.form.DetailIchiran.Rows[shinkiSetIndex + i].Cells["MOD_FLG"].Value = "2";
                }
            }
            #endregion

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        #region F9 登録処理
        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name=""></param>
        [Transaction]
        //public virtual void Regist()
        public bool RegistData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 定期配車入力テーブル論理削除
                    foreach (T_TEIKI_HAISHA_ENTRY contenashuruiEntity in this.entityEntryListForUpdate)
                    {
                        // 定期配車入力テーブル論理削除
                        this.teikiHaishaEntryDao.Update(contenashuruiEntity);
                    }

                    // 定期配車入力テーブル登録
                    foreach (T_TEIKI_HAISHA_ENTRY contenashuruiEntity in this.entityEntryList)
                    {
                        // 定期配車入力テーブル登録
                        this.teikiHaishaEntryDao.Insert(contenashuruiEntity);
                    }

                    // 定期配車明細テーブル登録
                    foreach (T_TEIKI_HAISHA_DETAIL entityHaishaDetail in this.entityHaishaDetailList)
                    {
                        this.teikiHaishaDetailDao.Insert(entityHaishaDetail);
                    }

                    // 定期配車荷降テーブル登録
                    foreach (T_TEIKI_HAISHA_NIOROSHI entityHaishaNioroshi in this.entityHaishaNioroshiList)
                    {
                        this.teikiHaishaNioroshiDao.Insert(entityHaishaNioroshi);
                    }

                    // 定期配車明細内訳テーブル登録
                    foreach (T_TEIKI_HAISHA_SHOUSAI entityHaishaShousai in this.entityHaishaShousaiList)
                    {
                        this.teikiHaishaShousaiDao.Insert(entityHaishaShousai);
                    }

                    // コミット
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region プロセスボタン押下処理
        /// <summary>
        /// [1]全明細を選択
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                UIHeader headeForm = new UIHeader();

                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    if (this.form.DetailIchiran.Rows[i].Cells["TEIKI_JISSEKI_NUMBER"].Value == null)
                    {
                        this.form.DetailIchiran.Rows[i].Cells["TOUROKU_FLG"].Value = true;
                        continue;
                    }
                    if (!this.form.DetailIchiran.Rows[i].Cells["TEIKI_JISSEKI_NUMBER"].Value.Equals("済"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["TOUROKU_FLG"].Value = true;
                    }

                    // 連携場合false
                    if (this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value != null && !string.IsNullOrEmpty(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString()))
                    {
                        if (!this.RenkeiCheck(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value.ToString(), false))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["TOUROKU_FLG"].Value = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [2]全明細を解除
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                UIHeader headeForm = new UIHeader();

                for (int i = 0; i < this.form.DetailIchiran.Rows.Count; i++)
                {
                    this.form.DetailIchiran.Rows[i].Cells["TOUROKU_FLG"].Value = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント(※処理未実装)
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txb_process_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region コントロールから対象のEntityを作成する
        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <returns>true:成功、false:失敗</returns>
        public bool CreateEntity(String modFlg)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                #region テーブル更新

                //配車入力リスト
                this.entityEntryList = new List<T_TEIKI_HAISHA_ENTRY>();
                //配車入力リスト(更新用)
                this.entityEntryListForUpdate = new List<T_TEIKI_HAISHA_ENTRY>();
                T_TEIKI_HAISHA_ENTRY entitysT_TEIKI_HAISHA_ENTRY = new T_TEIKI_HAISHA_ENTRY();

                // 配車入力Listのインスタンス生成
                this.entityHaishaDetailList = new List<T_TEIKI_HAISHA_DETAIL>();
                // 配車詳細Listのインスタンス生成
                this.entityHaishaShousaiList = new List<T_TEIKI_HAISHA_SHOUSAI>();
                // 配車荷降Listのインスタンス生成
                this.entityHaishaNioroshiList = new List<T_TEIKI_HAISHA_NIOROSHI>();

                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    DataGridViewRow detailRow = this.form.DetailIchiran.Rows[i];

                    //判定の前処理
                    Boolean updateFlg = false;
                    if (detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value != null)
                    {
                        updateFlg = bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString());
                    }

                    if (updateFlg)
                    {

                        long sysId = 0;
                        int seq = 0;
                        long detailSysId = 0;
                        long teikiHaishaNumber = 0;

                        // 該当行が新規場合
                        if (detailRow.Cells["MOD_FLG"].Value == null || detailRow.Cells["MOD_FLG"].Value.Equals("1"))
                        {
                            sysId = SaibanSystemId();
                            seq = 1;
                            teikiHaishaNumber = SaibanTeikiHaishaNumber();
                        }
                        // 該当行が登録場合
                        else
                        {
                            // 画面の一覧から定期配車番号を取得
                            var haishaNo = this.form.DetailIchiran.Rows[i].Cells["TEIKI_HAISHA_NUMBER"].Value.ToString();
                            teikiHaishaNumber = long.Parse(haishaNo);

                            // 登録済み検索結果から、定期配車番号をキーにSYSTEM_ID,SEQを取得
                            var record = this.ZumiSearchResult.AsEnumerable()
                                                              .Where(row => row.Field<Int64>("TEIKI_HAISHA_NUMBER") == teikiHaishaNumber)
                                                              .Select(n => new { SysId = n.Field<Int64>("SYSTEM_ID"), 
                                                                                   Seq = n.Field<Int32>("SEQ")})
                                                              .First();

                            sysId = record.SysId;
                            seq = record.Seq + 1;

                            // 定期配車入力テーブル（論理削除用）
                            entitysT_TEIKI_HAISHA_ENTRY = new T_TEIKI_HAISHA_ENTRY();
                            entitysT_TEIKI_HAISHA_ENTRY.SYSTEM_ID = (SqlInt64)record.SysId;
                            entitysT_TEIKI_HAISHA_ENTRY.SEQ = (SqlInt32)record.Seq;
                            // 論理削除
                            entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = true;
                            // 更新者情報設定
                            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_TEIKI_HAISHA_ENTRY>(entitysT_TEIKI_HAISHA_ENTRY);
                            dataBinderLogic.SetSystemProperty(entitysT_TEIKI_HAISHA_ENTRY, false);
                            //TIME_STAMP
                            entitysT_TEIKI_HAISHA_ENTRY.TIME_STAMP = (byte[])detailRow.Cells["TIME_STAMP"].Value;
                            entityEntryListForUpdate.Add(entitysT_TEIKI_HAISHA_ENTRY);

                        }

                        #region 定期配車入力テーブル
                        entitysT_TEIKI_HAISHA_ENTRY = new T_TEIKI_HAISHA_ENTRY();
                        entitysT_TEIKI_HAISHA_ENTRY.SYSTEM_ID = sysId;
                        entitysT_TEIKI_HAISHA_ENTRY.SEQ = seq;
                        entitysT_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = teikiHaishaNumber;
                        if (detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue == null
                            || detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.Equals(string.Empty))
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.KYOTEN_CD = 0;
                        }
                        else
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.KYOTEN_CD = (SqlInt16)Int16.Parse(detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.ToString());
                        }

                        entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_DATE = Convert.ToDateTime(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString());
                        entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_HOUR = SqlInt16.Null;
                        entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_MINUTE = SqlInt16.Null;
                        entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_END_HOUR = SqlInt16.Null;
                        entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_END_MINUTE = SqlInt16.Null;
                        entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = false;
                        entitysT_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD = detailRow.Cells[ConstCls.DetailColName.COURSE_NAME_CD].FormattedValue.ToString();
                        entitysT_TEIKI_HAISHA_ENTRY.SHARYOU_CD = detailRow.Cells[ConstCls.DetailColName.SHARYOU_CD].FormattedValue.ToString();
                        entitysT_TEIKI_HAISHA_ENTRY.SHASHU_CD = detailRow.Cells[ConstCls.DetailColName.SHASHU_CD].FormattedValue.ToString();
                        entitysT_TEIKI_HAISHA_ENTRY.UNTENSHA_CD = detailRow.Cells[ConstCls.DetailColName.UNTENSHA_CD].FormattedValue.ToString();
                        entitysT_TEIKI_HAISHA_ENTRY.UNPAN_GYOUSHA_CD = detailRow.Cells[ConstCls.DetailColName.UNPAN_GYOUSHA_CD].FormattedValue.ToString();

                        if (detailRow.Cells[ConstCls.DetailColName.HOJOIN_CD].FormattedValue == null)
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.HOJOIN_CD = string.Empty;
                        }
                        else
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.HOJOIN_CD = detailRow.Cells[ConstCls.DetailColName.HOJOIN_CD].FormattedValue.ToString();
                        }

                        DTOClass data = new DTOClass();
                        data.CourseNameCd = entitysT_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD;
                        DateTime nowDay = entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_DATE.Value;
                        string dayCd = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.DAY_CD].FormattedValue);
                        switch (dayCd)
                        {
                            case "1":
                                data.DayCd = 1;
                                break;
                            case "2":
                                data.DayCd = 2;
                                break;
                            case "3":
                                data.DayCd = 3;
                                break;
                            case "4":
                                data.DayCd = 4;
                                break;
                            case "5":
                                data.DayCd = 5;
                                break;
                            case "6":
                                data.DayCd = 6;
                                break;
                            case "7":
                                data.DayCd = 7;
                                break;
                            default:
                                data.DayCd = DateUtility.GetShougunDayOfWeek(nowDay);
                                break;
                        }

                        int day = data.DayCd;
                        if (day == DateUtility.GetShougunDayOfWeek(nowDay))
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN = 1;
                        }
                        else
                        {
                            entitysT_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN = 2;
                            switch (dayCd)
                            {
                                case "1":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 1;
                                    break;
                                case "2":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 2;
                                    break;
                                case "3":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 3;
                                    break;
                                case "4":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 4;
                                    break;
                                case "5":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 5;
                                    break;
                                case "6":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 6;
                                    break;
                                case "7":
                                    entitysT_TEIKI_HAISHA_ENTRY.DAY_CD = 7;
                                    break;
                            }
                        }

                        DataTable dt = this.courseDao.GetCourseData(data);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Int16 time = 0;
                            string strtime = Convert.ToString(dt.Rows[0]["SAGYOU_BEGIN_HOUR"]);
                            if (!string.IsNullOrEmpty(strtime) && Int16.TryParse(strtime, out time))
                            {
                                entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_HOUR = time;
                            }
                            strtime = Convert.ToString(dt.Rows[0]["SAGYOU_BEGIN_MINUTE"]);
                            if (!string.IsNullOrEmpty(strtime) && Int16.TryParse(strtime, out time))
                            {
                                entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_MINUTE = time;
                            }
                            strtime = Convert.ToString(dt.Rows[0]["SAGYOU_END_HOUR"]);
                            if (!string.IsNullOrEmpty(strtime) && Int16.TryParse(strtime, out time))
                            {
                                entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_END_HOUR = time;
                            }
                            strtime = Convert.ToString(dt.Rows[0]["SAGYOU_END_MINUTE"]);
                            if (!string.IsNullOrEmpty(strtime) && Int16.TryParse(strtime, out time))
                            {
                                entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_END_MINUTE = time;
                            }

                            entitysT_TEIKI_HAISHA_ENTRY.SHUPPATSU_GYOUSHA_CD = Convert.ToString(dt.Rows[0]["SHUPPATSU_GYOUSHA_CD"]);
                            entitysT_TEIKI_HAISHA_ENTRY.SHUPPATSU_GENBA_CD = Convert.ToString(dt.Rows[0]["SHUPPATSU_GENBA_CD"]);

                        }

                        // 更新者情報設定
                        var dataBinderLogic1 = new DataBinderLogic<r_framework.Entity.T_TEIKI_HAISHA_ENTRY>(entitysT_TEIKI_HAISHA_ENTRY);
                        dataBinderLogic1.SetSystemProperty(entitysT_TEIKI_HAISHA_ENTRY, false);

                        if (detailRow.Cells["MOD_FLG"].Value == null || detailRow.Cells["MOD_FLG"].Value.Equals("1"))
                        {
                        }
                        else
                        {
                            T_TEIKI_HAISHA_ENTRY teikihaishaentry = new T_TEIKI_HAISHA_ENTRY();
                            string strSystemid = Convert.ToString(entitysT_TEIKI_HAISHA_ENTRY.SYSTEM_ID);
                            string strSeq = Convert.ToString(entitysT_TEIKI_HAISHA_ENTRY.SEQ - 1);
                            teikihaishaentry = this.teikiHaishaEntryDao.GetDataByCd(strSystemid, strSeq);

                            if (teikihaishaentry != null)
                            {
                                entitysT_TEIKI_HAISHA_ENTRY.CREATE_DATE = teikihaishaentry.CREATE_DATE;
                                entitysT_TEIKI_HAISHA_ENTRY.CREATE_PC = teikihaishaentry.CREATE_PC;
                                entitysT_TEIKI_HAISHA_ENTRY.CREATE_USER = teikihaishaentry.CREATE_USER;
                            }
                        }

                        entityEntryList.Add(entitysT_TEIKI_HAISHA_ENTRY);
                        #endregion

                        #region 定期配車明細テーブル
                        this.courseDetailResult = new DataTable();
                        dto = new DTOClass();
                        dto.SagyouDate = (DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString())).ToString();
                        dto.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString()));
                        dto.CourseNameCd = detailRow.Cells[ConstCls.DetailColName.COURSE_NAME_CD].FormattedValue.ToString();
                        if (detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue == null
                            || detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.Equals(string.Empty))
                        {
                            dto.KyotenCd = string.Empty;
                        }
                        else
                        {
                            dto.KyotenCd = detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.ToString();
                        }

                        if (entitysT_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN == 2)
                        {
                            dto.DayCd = (int)entitysT_TEIKI_HAISHA_ENTRY.DAY_CD.Value;
                        }
                        this.courseDetailResult = courseDetailDao.GetCourseDetailData(dto);

                        #region 速度改善
                        int rowCount = 0;
                        long currentDetailSystemId = -1;
                        long maxDetailSystemId = 0;

                        //DETAIL_SYSTEM_IDを行数分まとめて採番する
                        rowCount = this.courseDetailResult.Rows.Count;
                        if (rowCount != 0)
                        {
                            maxDetailSystemId = this.SaibanSystemId(rowCount);
                            currentDetailSystemId = maxDetailSystemId - rowCount;
                        }

                        //事前にコース対象行データを全て取得
                        DataTable courseDetailItemsResultAll = new DataTable();

                        //適用期間有効範囲のもののみ抽出(行を条件に含めず取得する)
                        DTOClass detailItemsAllDto = new DTOClass();
                        detailItemsAllDto.DayCd = dto.DayCd;
                        detailItemsAllDto.CourseNameCd = dto.CourseNameCd;
                        //detailItemsAllDto.RowNumber = dto.RowNumber;
                        detailItemsAllDto.SagyouDate = entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_DATE.IsNull ? string.Empty : entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_DATE.Value.ToString();

                        courseDetailItemsResultAll = courseDetailItemsDao.GetCourseDetailItemsData(detailItemsAllDto);
                        #endregion

                        for (int j = 0; j < this.courseDetailResult.Rows.Count; j++)
                        {
                            T_TEIKI_HAISHA_DETAIL haishaDetail = new T_TEIKI_HAISHA_DETAIL();

                            haishaDetail.SYSTEM_ID = sysId;
                            haishaDetail.SEQ = seq;

                            //detailSysId = SaibanSystemId();
                            detailSysId = currentDetailSystemId;
                            if (currentDetailSystemId <= maxDetailSystemId)
                            {
                                currentDetailSystemId++;
                            }

                            haishaDetail.DETAIL_SYSTEM_ID = detailSysId;
                            haishaDetail.TEIKI_HAISHA_NUMBER = teikiHaishaNumber;
                            if (!this.courseDetailResult.Rows[j]["ROW_NO"].ToString().Equals(string.Empty))
                            {
                                haishaDetail.ROW_NUMBER = (SqlInt16)Int16.Parse(this.courseDetailResult.Rows[j]["ROW_NO"].ToString());
                            }
                            if (false == string.IsNullOrEmpty(this.courseDetailResult.Rows[j]["ROUND_NO"].ToString()))
                            {
                                haishaDetail.ROUND_NO = Int32.Parse(this.courseDetailResult.Rows[j]["ROUND_NO"].ToString());
                            }
                            haishaDetail.GYOUSHA_CD = this.courseDetailResult.Rows[j]["GYOUSHA_CD"].ToString();
                            haishaDetail.GENBA_CD = this.courseDetailResult.Rows[j]["GENBA_CD"].ToString();
                            haishaDetail.MEISAI_BIKOU = this.courseDetailResult.Rows[j]["MEISAI_BIKOU"].ToString();
                            if (!string.IsNullOrEmpty(Convert.ToString(this.courseDetailResult.Rows[j]["KIBOU_TIME"])))
                            {
                                haishaDetail.KIBOU_TIME = Convert.ToDateTime(this.courseDetailResult.Rows[j]["KIBOU_TIME"].ToString());
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(this.courseDetailResult.Rows[j]["SAGYOU_TIME_MINUTE"])))
                            {
                                haishaDetail.SAGYOU_TIME_MINUTE = Convert.ToInt16(this.courseDetailResult.Rows[j]["SAGYOU_TIME_MINUTE"].ToString());
                            }

                            #region 定期配車詳細テーブル
                            this.courseDetailItemsResult = new DataTable();
                            dto.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString()));
                            dto.SagyouDate = detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].Value.ToString();
                            dto.CourseNameCd = detailRow.Cells[ConstCls.DetailColName.COURSE_NAME_CD].FormattedValue.ToString();
                            dto.RecId = int.Parse(this.courseDetailResult.Rows[j]["REC_ID"].ToString());
                            if (entitysT_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN == 2)
                            {
                                dto.DayCd = (int)entitysT_TEIKI_HAISHA_ENTRY.DAY_CD.Value;
                            }
                            this.courseDetailItemsResult = courseDetailItemsDao.GetCourseDetailItemsData(dto);
                            
                            if (0 < this.courseDetailItemsResult.Rows.Count)
                            {
                                // 詳細が1件以上ある場合、明細データ追加(適用期間を考慮)
                                this.entityHaishaDetailList.Add(haishaDetail);
                            }

                            for (int k = 0; k < this.courseDetailItemsResult.Rows.Count; k++)
                            {
                                T_TEIKI_HAISHA_SHOUSAI haishaShousai = new T_TEIKI_HAISHA_SHOUSAI();

                                haishaShousai.SYSTEM_ID = sysId;
                                haishaShousai.SEQ = seq;
                                haishaShousai.DETAIL_SYSTEM_ID = detailSysId;
                                haishaShousai.TEIKI_HAISHA_NUMBER = teikiHaishaNumber;
                                haishaShousai.ROW_NUMBER = k + 1;
                                haishaShousai.HINMEI_CD = this.courseDetailItemsResult.Rows[k]["HINMEI_CD"].ToString();
                                if (!this.courseDetailItemsResult.Rows[k]["UNIT_CD"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.UNIT_CD = (SqlInt16)Int16.Parse(this.courseDetailItemsResult.Rows[k]["UNIT_CD"].ToString());
                                }
                                if (!this.courseDetailItemsResult.Rows[k]["KANSANCHI"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.KANSANCHI = (SqlDecimal)SqlDecimal.Parse(this.courseDetailItemsResult.Rows[k]["KANSANCHI"].ToString());
                                }
                                if (!this.courseDetailItemsResult.Rows[k]["KANSAN_UNIT_CD"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.KANSAN_UNIT_CD = (SqlInt16)Int16.Parse(this.courseDetailItemsResult.Rows[k]["KANSAN_UNIT_CD"].ToString());
                                }
                                if (!this.courseDetailItemsResult.Rows[k]["KEIYAKU_KBN"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.KEIYAKU_KBN = (SqlInt16)Int16.Parse(this.courseDetailItemsResult.Rows[k]["KEIYAKU_KBN"].ToString());
                                }
                                if (!this.courseDetailItemsResult.Rows[k]["KEIJYOU_KBN"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.KEIJYOU_KBN = (SqlInt16)Int16.Parse(this.courseDetailItemsResult.Rows[k]["KEIJYOU_KBN"].ToString());
                                }
                                // 伝票区分CDと要記入
                                if (!this.courseDetailItemsResult.Rows[k]["DENPYOU_KBN_CD"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.DENPYOU_KBN_CD = (SqlInt16)SqlInt16.Parse(this.courseDetailItemsResult.Rows[k]["DENPYOU_KBN_CD"].ToString());
                                }
                                // 登録でエラーのため修正（string.Emptyチェック追加）
                                if (!this.courseDetailItemsResult.Rows[k]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString().Equals(string.Empty))
                                {
                                    haishaShousai.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (SqlBoolean)SqlBoolean.Parse(this.courseDetailItemsResult.Rows[k]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString());
                                }

                                if (!string.IsNullOrEmpty(this.courseDetailItemsResult.Rows[k]["INPUT_KBN"].ToString()))
                                {
                                    haishaShousai.INPUT_KBN = SqlInt16.Parse(this.courseDetailItemsResult.Rows[k]["INPUT_KBN"].ToString());
                                }
                                if (!string.IsNullOrEmpty(this.courseDetailItemsResult.Rows[k]["NIOROSHI_NUMBER"].ToString()))
                                {
                                    haishaShousai.NIOROSHI_NUMBER = SqlInt32.Parse(this.courseDetailItemsResult.Rows[k]["NIOROSHI_NUMBER"].ToString());
                                }
                                if (!string.IsNullOrEmpty(this.courseDetailItemsResult.Rows[k]["ANBUN_FLG"].ToString()))
                                {
                                    haishaShousai.ANBUN_FLG = SqlBoolean.Parse(this.courseDetailItemsResult.Rows[k]["ANBUN_FLG"].ToString());
                                }
                                haishaShousai.ADD_FLG = (SqlBoolean)SqlBoolean.Parse("0");
                                this.entityHaishaShousaiList.Add(haishaShousai);
                            }
                            #endregion
                        }
                        #endregion

                        #region 定期配車荷降テーブル
                        this.courseNioroshiResult = new DataTable();
                        dto.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString()));
                        dto.CourseNameCd = detailRow.Cells[ConstCls.DetailColName.COURSE_NAME_CD].FormattedValue.ToString();
                        if (entitysT_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN == 2)
                        {
                            dto.DayCd = (int)entitysT_TEIKI_HAISHA_ENTRY.DAY_CD.Value;
                        }
                        this.courseNioroshiResult = courseNioroshiDao.GetCourseNioroshiData(dto);
                        for (int j = 0; j < this.courseNioroshiResult.Rows.Count; j++)
                        {
                            T_TEIKI_HAISHA_NIOROSHI haishaNioroshi = new T_TEIKI_HAISHA_NIOROSHI();

                            haishaNioroshi.SYSTEM_ID = sysId;
                            haishaNioroshi.SEQ = seq;
                            haishaNioroshi.NIOROSHI_NUMBER = (SqlInt16)Int16.Parse(this.courseNioroshiResult.Rows[j]["NIOROSHI_NO"].ToString());
                            haishaNioroshi.TEIKI_HAISHA_NUMBER = teikiHaishaNumber;
                            if (!this.courseNioroshiResult.Rows[j]["NIOROSHI_GYOUSHA_CD"].ToString().Equals(string.Empty))
                            {
                                haishaNioroshi.NIOROSHI_GYOUSHA_CD = this.courseNioroshiResult.Rows[j]["NIOROSHI_GYOUSHA_CD"].ToString();
                            }
                            if (!this.courseNioroshiResult.Rows[j]["NIOROSHI_GENBA_CD"].ToString().Equals(string.Empty))
                            {
                                haishaNioroshi.NIOROSHI_GENBA_CD = this.courseNioroshiResult.Rows[j]["NIOROSHI_GENBA_CD"].ToString();
                            }
                            this.entityHaishaNioroshiList.Add(haishaNioroshi);
                        }
                        #endregion

                    }
                }
                #endregion

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region Mulit行メッセージを生成
        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            // 整形済みメッセージ
            string msgResult = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(msgID, str);

                // メッセージ原本
                MessageUtil = new MessageUtility();
                string msg = MessageUtil.GetMessage("E001").MESSAGE;

                for (int i = 0; i < str.Length; i++)
                {
                    string msgTmp = string.Format(msg, str[i]);
                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        msgResult += "\r\n";
                    }
                    msgResult += msgTmp;
                }

                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(msgResult);
            }
        }
        #endregion

        #region DataGridViewデータ件数チェック処理
        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.DetailIchiran.Rows.Count > 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 採番処理(システムID、定期配車番号)
        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanSystemId(int addCount = 0)
        {
            // 戻り値を初期化
            int returnInt = 1;

            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {

                    // 処理区分：120（定期配車）
                    var entity = new S_NUMBER_SYSTEM();
                    entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                    // システムIDの最大値+1を取得する
                    returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                    if (addCount != 0)
                    {
                        //指定数分追加して先行でSYSTEM_IDの採番を行う
                        returnInt = returnInt + addCount;
                    }

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_SYSTEM();
                        updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberSystemDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        this.numberSystemDao.Update(updateEntity);
                    }

                    // コミット
                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnInt);
            }
        }

        /// <summary>
        /// 定期配車番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanTeikiHaishaNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = -1;

                using (Transaction tran = new Transaction())
                {
                    // 処理区分：120（定期配車）
                    var entity = new S_NUMBER_DENSHU();
                    entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    var updateEntity = this.numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                    // 伝種連番の最大値+1を取得する
                    returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_DENSHU();
                        updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberDenshuDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        this.numberDenshuDao.Update(updateEntity);
                    }

                    // コミット
                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region その他
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

        #region コース名取得処理
        /// <summary>
        /// コース名取得処理
        /// </summary>
        public bool getCourseName(int index)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(index);

                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"], false);

                string courseCD = Convert.ToString(this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"].Value);
                if (!string.IsNullOrEmpty(courseCD))
                {
                    courseCD = courseCD.PadLeft(6, '0').ToUpper();
                    this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"].Value = courseCD;
                }
                else
                {
                    this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_RYAKU"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[index].Cells["KYOTEN_CD"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[index].Cells["KYOTEN_NAME_RYAKU"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[index].Cells["DAY_CD"].Value = string.Empty;

                    this.isInputError = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                string dayCd = Convert.ToString(this.form.DetailIchiran.Rows[index].Cells["DAY_CD"].Value);
                string dayNm = Convert.ToString(this.form.DetailIchiran.Rows[index].Cells["DAY_NM"].Value);

                if (this.beforeCd == courseCD && !this.isInputError && this.beforeDayCd == dayCd)
                {
                    return ret;
                }

                this.isInputError = false;
                DTOClass data = new DTOClass();

                // 曜日CD
                if (this.form.DetailIchiran.Rows[index].Cells["SAGYOU_DATE"].Value != null)
                {
                    data.SagyouDate = this.form.DetailIchiran.Rows[index].Cells["SAGYOU_DATE"].Value.ToString();
                    if (string.IsNullOrEmpty(Convert.ToString(this.form.DetailIchiran.Rows[index].Cells["TEIKI_HAISHA_NUMBER"].Value)))
                    {
                        DateTime nowDay = DateTime.Parse(data.SagyouDate);
                        data.DayCd = DateUtility.GetShougunDayOfWeek(nowDay);
                    }
                    else
                    {
                        // 曜日
                        switch (dayNm)
                        {
                            case "月":
                                data.DayCd = 1;
                                break;
                            case "火":
                                data.DayCd = 2;
                                break;
                            case "水":
                                data.DayCd = 3;
                                break;
                            case "木":
                                data.DayCd = 4;
                                break;
                            case "金":
                                data.DayCd = 5;
                                break;
                            case "土":
                                data.DayCd = 6;
                                break;
                            case "日":
                                data.DayCd = 7;
                                break;
                        }
                    }
                }

                // 拠点CD
                if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text) && !"99".Equals(this.form.txt_KyotenCD.Text))
                {
                    data.KyotenCd = this.form.txt_KyotenCD.Text;
                }
                // コースCD
                data.CourseNameCd = courseCD;

                // コース名称ポップアップデータのリストを取得する
                DataTable CourseNameSearchResult = courseDao.GetCourseData(data);

                // 0件場合
                if (CourseNameSearchResult.Rows.Count == 0)
                {

                    if (string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
                    {
                        // 該当CDがない時
                        msgLogic.MessageBoxShow("E020", "コース");
                    }
                    else
                    {
                        // 該当CDがない時
                        msgLogic.MessageBoxShow("E062", "コースCDは入力された拠点");
                    }

                    this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_RYAKU"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[index].Cells["KYOTEN_CD"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[index].Cells["KYOTEN_NAME_RYAKU"].Value = string.Empty;

                    // エラー項目背景色は赤色に設定
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"], true);
                    this.isInputError = true;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                // 画面に取得したデータを設定する
                else
                {
                    // コース名称ポップアップデータのリストを取得する
                    DataTable CourseSearchResult = courseDetailDao.GetCourseNameListForPopUp(data);

                    if (CourseSearchResult.Rows.Count == 0)
                    {
                        // 該当CDがない時
                        msgLogic.MessageBoxShow("E062", "コースCDは作業日の曜日");
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"], true);
                        this.isInputError = true;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    else if (CourseSearchResult.Rows.Count == 1)
                    {
                        this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_RYAKU"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["COURSE_NAME_RYAKU"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["KYOTEN_CD"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["KYOTEN_CD"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["KYOTEN_NAME_RYAKU"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["KYOTEN_NAME_RYAKU"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["SHASHU_CD"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["SHASHU_CD"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["SHASHU_NAME_RYAKU"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["SHASHU_NAME_RYAKU"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["SHARYOU_CD"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["SHARYOU_CD"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["SHARYOU_NAME_RYAKU"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["SHARYOU_NAME_RYAKU"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["UNTENSHA_CD"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["UNTENSHA_CD"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["UNTENSHA_NAME_RYAKU"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["UNTENSHA_NAME_RYAKU"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["UNPAN_GYOUSHA_CD"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["UNPAN_GYOUSHA_CD"], string.Empty);
                        this.form.DetailIchiran.Rows[index].Cells["UNPAN_GYOUSHA_NAME"].Value = ChgDBNullToValue(CourseNameSearchResult.Rows[0]["UNPAN_GYOUSHA_NAME_RYAKU"], string.Empty);
                    }
                    else
                    {
                        var cell = this.form.DetailIchiran.Rows[index].Cells["COURSE_NAME_CD"] as ICustomDataGridControl;
                        cell.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU,DAY_NM";

                        // コース情報 ポップアップ取得
                        // 表示用データ取得＆加工
                        var ShainDataTable = CourseSearchResult.Copy();
                        // TableNameを設定すれば、ポップアップのタイトルになる
                        ShainDataTable.TableName = "コース名選択";

                        // 列名とデータソース設定
                        cell.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称", "曜日" };
                        cell.PopupDataSource = CourseSearchResult;
                        CustomControlExtLogic.PopUp(cell as ICustomControl);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車輌チェック処理
        /// <summary>
        /// 車輌チェック処理
        /// </summary>
        public bool CheckSharyouCd(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                // 車輌CD
                string sharyouCD = string.Empty;

                // 車輌名取得して、設定する
                if (e.ColumnIndex == this.form.DetailIchiran.Columns["SHARYOU_CD"].Index)
                {
                    // 車輌CD取得
                    if (null != row.Cells["SHARYOU_CD"].Value && !string.IsNullOrEmpty(row.Cells["SHARYOU_CD"].Value.ToString()))
                    {
                        // 2015/09/08 koukoukon #12115 start
                        this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHARYOU_CD"].Value = this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHARYOU_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                        // 2015/09/08 koukoukon #12115 end
                        sharyouCD = row.Cells["SHARYOU_CD"].Value.ToString();
                        // 20141015 koukouei 休動管理機能 start
                        /*// 前回値と変わらない場合は処理を行わない
                        if(true == this.oldSharyouCD.Equals(sharyouCD))
                        {
                            // 処理終了
                            return;
                        }*/
                        // 20141015 koukouei 休動管理機能 start
                    }
                    else
                    {
                        row.Cells["SHARYOU_NAME_RYAKU"].Value = string.Empty;
                        return ret;
                    }

                    // 一旦クリア
                    row.Cells["SHARYOU_NAME_RYAKU"].Value = string.Empty;

                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["SHARYOU_CD"], false);

                    // 車輌CD取得
                    if (false == string.IsNullOrEmpty(sharyouCD))
                    {
                        var findEntity = new M_SHARYOU();

                        // 運搬業者CD取得
                        if (null != row.Cells["UNPAN_GYOUSHA_CD"].Value)
                        {
                            var unpanGyoushaCD = row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                            if (false == string.IsNullOrEmpty(unpanGyoushaCD))
                            {
                                // 運搬業者CDセット
                                findEntity.GYOUSHA_CD = unpanGyoushaCD;
                            }
                        }

                        // 車種CD取得
                        if (null != row.Cells["SHASHU_CD"].Value)
                        {
                            var shashuCD = row.Cells["SHASHU_CD"].Value.ToString();
                            if (false == string.IsNullOrEmpty(shashuCD))
                            {
                                // 車種CDセット
                                findEntity.SHASYU_CD = shashuCD;
                            }
                        }

                        // 車輌情報取得
                        findEntity.SHARYOU_CD = sharyouCD;
                        SqlDateTime sagyouDate = SqlDateTime.Null;
                        if(row.Cells["SAGYOU_DATE"].Value !=null && !string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                        {
                            sagyouDate = SqlDateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString());
                        }
                        var SharyouNameSearchResult = this.sharyouDao.GetSharyouNameData(findEntity, sagyouDate);

                        if (SharyouNameSearchResult.Rows.Count == 0)
                        {
                            // 該当CDがない時
                            msgLogic.MessageBoxShow("E020", "車輌");
                            ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["SHARYOU_CD"], true);
                            ret = false;
                        }
                        else
                        {
                            if (SharyouNameSearchResult.Rows.Count == 1)
                            {
                                // 取得結果が１件の場合
                                // 20141015 koukouei 休動管理機能 start
                                // 休動チェック
                                if (!this.ChkSharyouWordClose(row))
                                {
                                    ret = false;
                                }
                                // 20141015 koukouei 休動管理機能 end
                                else
                                {
                                    row.Cells["SHARYOU_NAME_RYAKU"].Value = SharyouNameSearchResult.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                                    row.Cells["SHASHU_CD"].Value = SharyouNameSearchResult.Rows[0]["SHASYU_CD"].ToString();
                                    row.Cells["UNTENSHA_CD"].Value = SharyouNameSearchResult.Rows[0]["SHAIN_CD"].ToString();
                                    row.Cells["UNPAN_GYOUSHA_CD"].Value = SharyouNameSearchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                                    row.Cells["SHASHU_NAME_RYAKU"].Value = SharyouNameSearchResult.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                                    row.Cells["UNTENSHA_NAME_RYAKU"].Value = SharyouNameSearchResult.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                                    row.Cells["UNPAN_GYOUSHA_NAME"].Value = SharyouNameSearchResult.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                                }
                            }
                            else
                            {
                                // ヒット数が複数件の場合、ポップアップ表示
                                ret = false;
                                SendKeys.Send(" ");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車種チェック処理
        /// <summary>
        /// 車種チェック処理
        /// </summary>
        public bool CheckShashuCd(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(e);


                if (e.ColumnIndex == this.form.DetailIchiran.Columns["SHASHU_CD"].Index)
                {
                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_CD"], false);

                    // 車種CDを取得
                    if (DBNull.Value.Equals(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_CD"]))
                    {
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    if (null == this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_CD"].Value)
                    {
                        this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_NAME_RYAKU"].Value = string.Empty;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    String shashuCD = this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_CD"].Value.ToString();

                    // 車種CDを入力されてない場合
                    if (shashuCD.Equals(string.Empty))
                    {
                        this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_NAME_RYAKU"].Value = string.Empty;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }

                    // 車輌CDを取得

                    String sharyouCD = string.Empty;
                    if (!DBNull.Value.Equals(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHARYOU_CD"]) &&
                        null != this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHARYOU_CD"].Value)
                    {
                        sharyouCD = this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHARYOU_CD"].Value.ToString();
                    }

                    if (sharyouCD.Equals(string.Empty))
                    {
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }

                    var findEntity = new M_SHARYOU();
                    findEntity.SHARYOU_CD = sharyouCD;
                    findEntity.SHASYU_CD = shashuCD;
                    SqlDateTime sagyouDate = SqlDateTime.Null;
                    if (this.form.DetailIchiran.Rows[e.RowIndex].Cells["SAGYOU_DATE"].Value != null && !string.IsNullOrEmpty(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SAGYOU_DATE"].Value.ToString()))
                    {
                        sagyouDate = SqlDateTime.Parse(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SAGYOU_DATE"].Value.ToString());
                    }
                    DataTable SharyouNameSearchResult = this.sharyouDao.GetSharyouNameData(findEntity, sagyouDate);

                    // 0件を取得する場合
                    if (SharyouNameSearchResult.Rows.Count == 0)
                    {
                        // 該当CDがない時
                        msgLogic.MessageBoxShow("E020", "車輌");
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.DetailIchiran.Rows[e.RowIndex].Cells["SHASHU_CD"], true);
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShashuCd", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 必須チェック設定処理
        /// <summary>
        /// 必須チェック設定処理
        /// </summary>
        public bool setRegistCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                existChecks.Add(existCheck);

                foreach (DataGridViewRow detailRow in this.form.DetailIchiran.Rows)
                {
                    Boolean updateFlg = false;
                    // 選択されたかどうか判断する
                    if (detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value != null)
                    {
                        updateFlg = bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString());
                    }

                    // 選択されている行目に必須チェックを設定する
                    if (updateFlg)
                    {
                        // 作業日
                        PropertyUtility.SetValue(detailRow.Cells["SAGYOU_DATE"], "RegistCheckMethod", existChecks);
                        // コース
                        PropertyUtility.SetValue(detailRow.Cells["COURSE_NAME_CD"], "RegistCheckMethod", existChecks);

                        // No.2950-->必須でなくする
                        // 車種
                        //PropertyUtility.SetValue(detailRow.Cells["SHASHU_CD"], "RegistCheckMethod", existChecks);
                        // 車輌
                        //PropertyUtility.SetValue(detailRow.Cells["SHARYOU_CD"], "RegistCheckMethod", existChecks);
                        // 運転者
                        //PropertyUtility.SetValue(detailRow.Cells["UNTENSHA_CD"], "RegistCheckMethod", existChecks);
                        // No.2950<--
                    }
                    // 選択されていない行目にnullを設定する
                    else
                    {
                        // 作業日
                        PropertyUtility.SetValue(detailRow.Cells["SAGYOU_DATE"], "RegistCheckMethod", null);
                        // コース
                        PropertyUtility.SetValue(detailRow.Cells["COURSE_NAME_CD"], "RegistCheckMethod", null);

                        // No.2950-->必須でなくする
                        // 車種
                        //PropertyUtility.SetValue(detailRow.Cells["SHASHU_CD"], "RegistCheckMethod", null);
                        // 車輌
                        //PropertyUtility.SetValue(detailRow.Cells["SHARYOU_CD"], "RegistCheckMethod", null);
                        // 運転者
                        //PropertyUtility.SetValue(detailRow.Cells["UNTENSHA_CD"], "RegistCheckMethod", null);
                        // No.2950<--
                    }
                }


                // 作業日
                PropertyUtility.SetValue(this.form.SAGYOU_DATE_FROM, "RegistCheckMethod", existChecks);
                PropertyUtility.SetValue(this.form.SAGYOU_DATE_TO, "RegistCheckMethod", existChecks);

            }
            catch (Exception ex)
            {
                LogUtility.Error("setRegistCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }
        #endregion

        #region 「詳細」ボタン押下処理
        /// <summary>
        /// 詳細」ボタン押下処理
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="detailRow">選択された明細行</param>
        public void ShowShousai(WINDOW_TYPE windowType, DataGridViewRow detailRow)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, detailRow);
                // 曜日を取得する
                DateTime nowDay = DateTime.Parse(detailRow.Cells["SAGYOU_DATE"].Value.ToString());
                int dayCd = DateUtility.GetShougunDayOfWeek(nowDay);
                // コースCD
                String courseNameCd = detailRow.Cells["COURSE_NAME_CD"].Value.ToString();
                if (courseNameCd.Equals(string.Empty))
                {
                    msgLogic.MessageBoxShow("E001", "コースCD");
                    return;
                }

                DTOClass data = new DTOClass();
                if (string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["TEIKI_HAISHA_NUMBER"].Value)))
                {
                    data.DayCd = dayCd;
                }
                else if(!string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["DAY_CD"].Value)))
                {
                    data.DayCd = Convert.ToInt32(detailRow.Cells["DAY_CD"].Value);
                }
                else if (string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["DAY_CD"].Value)))
                {
                    data.DayCd = dayCd;
                }

                data.SagyouDate = detailRow.Cells["SAGYOU_DATE"].Value.ToString();
                data.CourseNameCd = courseNameCd;
                DataTable CourseSearchResult = courseDetailDao.GetCourseNameListForPopUp(data);
                if (CourseSearchResult.Rows.Count == 0)
                {
                    // 該当CDがない時
                    msgLogic.MessageBoxShow("E062", "コースCDは作業日の曜日");
                    // エラー項目背景色は赤色に設定
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["COURSE_NAME_CD"], true);

                    return;
                }

                string furikaeKbn = "1";
                if (data.DayCd == dayCd)
                {
                    furikaeKbn = "1";
                }
                else
                {
                    furikaeKbn = "2";
                }

                string[] slist = { "", "", "", "", "", "", "", "" };
                FormManager.OpenFormWithAuth("G030", windowType, windowType, "", nowDay, courseNameCd, slist, furikaeKbn, data.DayCd);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowShousai", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「詳細」ボタン押下必須チェック設定処理
        /// <summary>
        /// 「詳細」ボタン押下必須チェック設定処理
        /// </summary>
        public void setRegistSyousaiCheck(DataGridViewRow detailRow)
        {
            try
            {
                LogUtility.DebugMethodStart();

                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                existChecks.Add(existCheck);

                // コース
                PropertyUtility.SetValue(detailRow.Cells["COURSE_NAME_CD"], "RegistCheckMethod", existChecks);

            }
            catch (Exception ex)
            {
                LogUtility.Error("setRegistSyousaiCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コース名称 ポップアップデータ取得処理
        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <param name="sagyouDate">作業日</param>
        /// <returns>マスタポップアップ用データテーブル</returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol, object sagyouDate, object teikiHaishaNumber)
        {
            var sortedDt = new DataTable();
            try
            {
                LogUtility.DebugMethodStart(displayCol, sagyouDate, teikiHaishaNumber);

                DTOClass data = new DTOClass();
                // 曜日CD
                if (sagyouDate != null)
                {
                    data.SagyouDate = sagyouDate.ToString();
                    if (string.IsNullOrEmpty(Convert.ToString(teikiHaishaNumber)))
                    {
                        DateTime nowDay = DateTime.Parse(data.SagyouDate);
                        data.DayCd = DateUtility.GetShougunDayOfWeek(nowDay);
                    }
                }
                // 拠点CD
                if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
                {
                    data.KyotenCd = this.form.txt_KyotenCD.Text;
                }

                // コース名称ポップアップデータのリストを取得する
                DataTable CourseNameList = courseDetailDao.GetCourseNameListForPopUp(data);
                if (CourseNameList.Rows.Count == 0)
                {
                    return CourseNameList;
                }

                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(CourseNameList.Columns[col].ColumnName, CourseNameList.Columns[col].DataType);
                }

                foreach (DataRow r in CourseNameList.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPopUpData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(sortedDt);
            }
            return sortedDt;
        }
        #endregion

        #region 登録するとき、画面のコースCD存在チェック
        /// <summary>
        /// 登録するとき、画面のコースCD存在チェック
        /// </summary>
        public bool courseCheck()
        {
            bool flg = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 曜日を取得する
                foreach (DataGridViewRow detailRow in this.form.DetailIchiran.Rows)
                {
                    // エラー項目背景色は赤色に設定しない
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["COURSE_NAME_CD"], false);
                    if (!string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["TEIKI_JISSEKI_NUMBER"].Value)))
                    {
                        continue;
                    }
                    if (null == detailRow.Cells["TOUROKU_FLG"].Value)
                    {
                        continue;
                    }
                    if (!bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString()))
                    {
                        continue;
                    }
                    // コースCD
                    String courseNameCd = detailRow.Cells["COURSE_NAME_CD"].Value.ToString();

                    DTOClass data = new DTOClass();

                    data.SagyouDate = detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].Value.ToString();
                    if (string.IsNullOrEmpty(Convert.ToString(detailRow.Cells[ConstCls.DetailColName.TEIKI_HAISHA_NUMBER].Value)))
                    {
                        DateTime nowDay = DateTime.Parse(data.SagyouDate);
                        data.DayCd = DateUtility.GetShougunDayOfWeek(nowDay);
                    }

                    data.CourseNameCd = courseNameCd;
                    DataTable CourseSearchResult = courseDetailDao.GetCourseNameListForPopUp(data);
                    if (CourseSearchResult.Rows.Count == 0)
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["COURSE_NAME_CD"], true);
                        flg = false;
                    }
                }

                if (!flg)
                {
                    // 該当CDがない時
                    msgLogic.MessageBoxShow("E062", "コースCDは作業日の曜日");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("courseCheck", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                flg = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(flg);
            }
            return flg;
        }
        #endregion

        #region
        /// <summary>
        /// 登録対象のコードマスタに適用期間内のデータがあるか判定
        /// </summary>
        /// <returns>true:エラー無, false:エラー有</returns>
        internal bool courseKikanCheck()
        {
            bool flg = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 曜日を取得する
                foreach (DataGridViewRow detailRow in this.form.DetailIchiran.Rows)
                {
                    // エラー項目背景色は赤色に設定しない
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["COURSE_NAME_CD"], false);
                    if (!string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["TEIKI_JISSEKI_NUMBER"].Value)))
                    {
                        continue;
                    }
                    if (null == detailRow.Cells["TOUROKU_FLG"].Value)
                    {
                        continue;
                    }
                    if (!bool.Parse(detailRow.Cells[ConstCls.DetailColName.TOUROKU_FLG].Value.ToString()))
                    {
                        continue;
                    }

                    // コース詳細件数
                    int detailItemsNo = 0;

                    // LogicClassのCreateEntityを参考に作成

                    // 曜日CD,振替配車区分
                    int dayCd = 0;
                    int furikaeHaishaKbn = 0;
                    #region 曜日CD取得
                    {
                        DTOClass data = new DTOClass();
                        DateTime sagyouDate = Convert.ToDateTime(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString());
                        string strDayCd = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.DAY_CD].FormattedValue);
                        switch (strDayCd)
                        {
                            case "1":
                                dayCd = 1;
                                break;
                            case "2":
                                dayCd = 2;
                                break;
                            case "3":
                                dayCd = 3;
                                break;
                            case "4":
                                dayCd = 4;
                                break;
                            case "5":
                                dayCd = 5;
                                break;
                            case "6":
                                dayCd = 6;
                                break;
                            case "7":
                                dayCd = 7;
                                break;
                            default:
                                dayCd = DateUtility.GetShougunDayOfWeek(sagyouDate);
                                break;
                        }

                        if (dayCd == DateUtility.GetShougunDayOfWeek(sagyouDate))
                        {
                            furikaeHaishaKbn = 1;
                        }
                        else
                        {
                            furikaeHaishaKbn = 2;
                        }
                    }
                    #endregion

                    // 明細データ
                    var courseDetailData = new DataTable();
                    #region 明細データ取得
                    {
                        var searchDto = new DTOClass();
                        // 拠点
                        if (detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue == null
                            || detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.Equals(string.Empty))
                        {
                            searchDto.KyotenCd = string.Empty;
                        }
                        else
                        {
                            searchDto.KyotenCd = detailRow.Cells[ConstCls.DetailColName.KYOTEN_CD].FormattedValue.ToString();
                        }
                        // コースCD
                        searchDto.CourseNameCd = detailRow.Cells["COURSE_NAME_CD"].Value.ToString();
                        // 曜日CD
                        if (furikaeHaishaKbn == 2)
                        {
                            searchDto.DayCd = dayCd;
                        }
                        courseDetailData = courseDetailDao.GetCourseDetailData(searchDto);
                    }
                    #endregion

                    for (int j = 0; j < courseDetailData.Rows.Count; j++)
                    {
                        var searchDto = new DTOClass();
                        // コースCD
                        searchDto.CourseNameCd = detailRow.Cells["COURSE_NAME_CD"].FormattedValue.ToString();
                        searchDto.SagyouDate = detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].Value.ToString();
                        searchDto.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_DATE].FormattedValue.ToString()));
                        searchDto.RecId = int.Parse(courseDetailData.Rows[j]["REC_ID"].ToString());
                        if (furikaeHaishaKbn == 2)
                        {
                            searchDto.DayCd = dayCd;
                        }

                        DataTable CourseDetailItemsSearchResult = courseDetailItemsDao.GetCourseDetailItemsData(searchDto);
                        detailItemsNo += CourseDetailItemsSearchResult.Rows.Count;
                    }
                    // 明細の適用期間内データが0件ならエラー
                    if (detailItemsNo == 0)
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["COURSE_NAME_CD"], true);
                        flg = false;
                    }
                }

                if (!flg)
                {
                    // 適用期間内のデータが１件も無い場合
                    msgLogic.MessageBoxShow("E261", "適用期間外", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("courseCheck", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                flg = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(flg);
            }
            return flg;
        }
        #endregion

        /// <summary>
        /// DetailIchiranCellEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void detailCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                if (row.Cells["SHARYOU_CD"].Value != null)
                {
                    // 前回値を保持する
                    this.oldSharyouCD = row.Cells["SHARYOU_CD"].Value.ToString();
                }

                var column = this.form.DetailIchiran.Columns[e.ColumnIndex];
                switch (column.Name)
                {
                    case ConstCls.DetailColName.COURSE_NAME_CD:
                        this.form.DetailIchiran.ImeMode = ImeMode.Disable;
                        this.beforeCd = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                        this.beforeDayCd = Convert.ToString(row.Cells["DAY_CD"].Value);
                        break;
                    case ConstCls.DetailColName.SHASHU_CD:
                    case ConstCls.DetailColName.SHARYOU_CD:
                    case ConstCls.DetailColName.UNTENSHA_CD:
                    case ConstCls.DetailColName.UNPAN_GYOUSHA_CD:
                    case ConstCls.DetailColName.HOJOIN_CD:
                        this.form.DetailIchiran.ImeMode = ImeMode.Disable;
                        break;
                    default:
                        this.form.DetailIchiran.ImeMode = ImeMode.NoControl;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("detailCellEnter", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 運転者CDValidated処理
        /// </summary>
        /// <param name="e">対象Cell</param>
        internal bool untenshaCDValidatedProc(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                if (row.Cells["UNTENSHA_CD"].Value != null)
                {
                    // 運転者CDの取得
                    var cd = row.Cells["UNTENSHA_CD"].Value.ToString();

                    // 一旦初期化
                    row.Cells["UNTENSHA_NAME_RYAKU"].Value = string.Empty;

                    // 入力CDが運転者として登録されているかのチェック
                    var shainFindEntity = new M_SHAIN();
                    shainFindEntity.SHAIN_CD = cd;
                    var shainEntitys = this.shainDao.GetAllValidData(shainFindEntity);

                    if ((shainEntitys != null) && (shainEntitys.Length > 0))
                    {
                        // 社員マスタは社員CDがKeyになっているため唯一がヒットする
                        if (shainEntitys[0].UNTEN_KBN.Value == true)
                        {
                            if (row.Cells["HOJOIN_CD"].Value != null
                                && !string.IsNullOrEmpty(row.Cells["HOJOIN_CD"].Value.ToString())
                                && cd.Equals(row.Cells["HOJOIN_CD"].Value.ToString()))
                            {
                                this.msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                                ret = false;
                            }
                            // 20141015 koukouei 休動管理機能 start
                            // 休動チェック
                            else if (!this.ChkUntenshaWordClose(row))
                            {
                                ret = false;
                            }
                            // 20141015 koukouei 休動管理機能 end
                            else
                            {
                                // 運転者名をセット
                                row.Cells["UNTENSHA_NAME_RYAKU"].Value = shainEntitys[0].SHAIN_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            // CDが運転者に該当していなかったため、エラー
                            this.msgLogic.MessageBoxShow("E020", "社員");
                            ret = false;
                        }
                    }
                    else
                    {
                        // そもそも該当外のCDはFocusOutCheckでチェックが行われる
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("untenshaCDValidatedProc", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
            }
            return ret;
        }

        /// <summary>
        /// 運搬業者CDValidated処理
        /// </summary>
        /// <param name="e">対象Cell</param>
        internal bool unpanGyoushaCDValidatedProc(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                if (row.Cells["UNPAN_GYOUSHA_CD"].Value != null)
                {
                    // 運転者CDの取得
                    var cd = row.Cells["UNPAN_GYOUSHA_CD"].Value.ToString();

                    // 一旦初期化
                    row.Cells["UNPAN_GYOUSHA_NAME"].Value = string.Empty;

                    // 入力CDが運搬業者として登録されているかのチェック
                    var gyoushaFindEntity = new M_GYOUSHA();
                    gyoushaFindEntity.GYOUSHA_CD = cd;
                    var gyoushaEntitys = this.gyoushaDao.GetAllValidData(gyoushaFindEntity);
                    if ((gyoushaEntitys != null) && (gyoushaEntitys.Length > 0))
                    {
                        // 業者マスタは業者CDがKeyになっているため唯一がヒットする
                        if (gyoushaEntitys[0].UNPAN_JUTAKUSHA_KAISHA_KBN.Value == true)
                        {
                            // 運搬業者名をセット
                            row.Cells["UNPAN_GYOUSHA_NAME"].Value = gyoushaEntitys[0].GYOUSHA_NAME_RYAKU;
                        }
                        else
                        {
                            // CDが運搬業者に該当していなかったため、エラー
                            this.msgLogic.MessageBoxShow("E020", "業者");
                            ret = false;
                        }
                    }
                    else
                    {
                        // そもそも該当外のCDはFocusOutCheckでチェックが行われる
                    }
                }
                else
                {
                    row.Cells["SHARYOU_CD"].Value = string.Empty;
                    row.Cells["SHARYOU_NAME_RYAKU"].Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("unpanGyoushaCDValidatedProc", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
            }
            return ret;
        }

        /// <summary>
        /// 作業日Validated処理
        /// </summary>
        /// <param name="e"></param>
        internal bool sagyouDateValidatedProc(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                if (row.Cells["SAGYOU_DATE"].Value != null)
                {
                    DateTime from = parentForm.sysDate.Date;
                    DateTime to = parentForm.sysDate.Date;
                    DateTime target = parentForm.sysDate.Date;

                    // 作業日の取得
                    if (this.dto == null
                        || string.IsNullOrEmpty(this.dto.SagyouDateFrom)
                        || string.IsNullOrEmpty(this.dto.SagyouDateTo))
                    {
                        from = parentForm.sysDate.Date;
                        to = parentForm.sysDate.Date;
                    }
                    else
                    {
                        DateTime.TryParse(this.dto.SagyouDateFrom, out from);
                        DateTime.TryParse(this.dto.SagyouDateTo, out to);
                    }

                    DateTime.TryParse(row.Cells["SAGYOU_DATE"].Value.ToString(), out target);

                    if (target < from || to < target)
                    {
                        // 検索条件の作業日(From～To)の範囲外の場合は、エラー
                        this.msgLogic.MessageBoxShow("E097", "検索条件の作業日", "作業日");
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("sagyouDateValidatedProc", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 新規用の一覧データ取得
        /// </summary>
        /// <param name="dtoClass">DTO</param>
        /// <param name="dateFrom">作業日From</param>
        /// <param name="dateTo">作業日To</param>
        /// <returns></returns>
        private DataTable SearchShinkiResult(DTOClass dtoClass, string dateFrom, string dateTo)
        {
            DataTable table = new DataTable();

            DateTime workFrom = new DateTime();
            DateTime workTo = new DateTime();

            if (!DateTime.TryParse(dateFrom, out workFrom)
                || !DateTime.TryParse(dateTo, out workTo))
            {
                return table;
            }

            // 指定された日付範囲の中で、日毎にデータを抽出
            while (workFrom <= workTo)
            {
                dtoClass.SagyouDate = workFrom.ToShortDateString();
                dtoClass.DayCd = DateUtility.GetShougunDayOfWeek(workFrom);

                if (table.Rows.Count == 0)
                {
                    table = teikiHaishaEntryDao.GetShinkiNomiData(dtoClass);
                }
                else
                {
                    var workTable = teikiHaishaEntryDao.GetShinkiNomiData(dtoClass);

                    foreach (DataRow row in workTable.Rows)
                    {
                        table.ImportRow(row);
                    }
                }

                // 日毎にデータ作成をするため加算する
                workFrom = workFrom.AddDays(1);
            }

            return table;
        }

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            bool isErr = false;
            try
            {
                this.form.SAGYOU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.SAGYOU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_FROM.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_TO.Text))
                {
                    return isErr;
                }

                DateTime date_from = DateTime.Parse(this.form.SAGYOU_DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.SAGYOU_DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.SAGYOU_DATE_FROM.IsInputErrorOccured = true;
                    this.form.SAGYOU_DATE_TO.IsInputErrorOccured = true;
                    this.form.SAGYOU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.SAGYOU_DATE_TO.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "作業日From", "作業日To" };
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E030", errorMsg);
                    this.form.SAGYOU_DATE_FROM.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end


        // 20141015 koukouei 休動管理機能 start
        #region 休動チェック
        /// <summary>
        /// 休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkWordClose()
        {
            bool ret = false;
            try
            {
                foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                {
                    // 車輛休動チェック
                    if (!this.ChkSharyouWordClose(row))
                    {
                        this.form.DetailIchiran.CurrentCell = row.Cells["SHARYOU_CD"];
                        this.form.DetailIchiran.Focus();
                        return ret;
                    }
                    // 運転者休動チェック
                    if (!this.ChkUntenshaWordClose(row))
                    {
                        this.form.DetailIchiran.CurrentCell = row.Cells["UNTENSHA_CD"];
                        this.form.DetailIchiran.Focus();
                        return ret;
                    }
                    // 補助員休動チェック
                    if (!this.ChkHojoinWordClose(row))
                    {
                        this.form.DetailIchiran.CurrentCell = row.Cells["HOJOIN_CD"];
                        this.form.DetailIchiran.Focus();
                        return ret;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkWordClose", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
            }
            // 処理終了
            return ret;
        }
        #endregion

        #region 車輌休動チェック
        /// <summary>
        /// 車輌休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkSharyouWordClose(DataGridViewRow row)
        {
            if (row == null)
            {
                return true;
            }

            string sharyouCD = Convert.ToString(row.Cells["SHARYOU_CD"].Value);
            string gyoushaCd = Convert.ToString(row.Cells["UNPAN_GYOUSHA_CD"].Value);
            string sagyouDate = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
            DateTime dt;

            if (string.IsNullOrEmpty(sharyouCD) || string.IsNullOrEmpty(sagyouDate)
                || !DateTime.TryParse(sagyouDate, out dt))
            {
                return true;
            }

            M_WORK_CLOSED_SHARYOU sharyou = new M_WORK_CLOSED_SHARYOU();
            sharyou.SHARYOU_CD = sharyouCD;
            sharyou.CLOSED_DATE = dt;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                sharyou.GYOUSHA_CD = gyoushaCd;
            }

            M_WORK_CLOSED_SHARYOU[] sharyous = this.workClosedSharyouDao.GetAllValidData(sharyou);
            if (sharyous.Length > 0)
            {
                // エラーメッセージ
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["SHARYOU_CD"], true);
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E206", "車輛", string.Format("作業日：{0}", dt.ToString("yyyy/MM/dd")));
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 運転者休動チェック
        /// <summary>
        /// 運転者休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkUntenshaWordClose(DataGridViewRow row)
        {
            if (row == null)
            {
                return true;
            }

            string untenshaCd = Convert.ToString(row.Cells["UNTENSHA_CD"].Value);
            string sagyouDate = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);
            DateTime dt;

            if (string.IsNullOrEmpty(untenshaCd) || string.IsNullOrEmpty(sagyouDate)
                || !DateTime.TryParse(sagyouDate, out dt))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA untensha = new M_WORK_CLOSED_UNTENSHA();
            untensha.SHAIN_CD = untenshaCd;
            untensha.CLOSED_DATE = dt;
            M_WORK_CLOSED_UNTENSHA[] untenshas = this.workClosedUntenshaDao.GetAllValidData(untensha);
            if (untenshas.Length > 0)
            {
                // エラーメッセージ
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["UNTENSHA_CD"], true);
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dt.ToString("yyyy/MM/dd")));
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 補助員休動チェック
        /// <summary>
        /// 補助員休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkHojoinWordClose(DataGridViewRow row)
        {
            if (row == null)
            {
                return true;
            }

            string hojoinCd = Convert.ToString(row.Cells["HOJOIN_CD"].Value);
            string sagyouDate = Convert.ToString(row.Cells["SAGYOU_DATE"].Value);

            DateTime dt;
            if (string.IsNullOrEmpty(hojoinCd) || string.IsNullOrEmpty(sagyouDate)
                || !DateTime.TryParse(sagyouDate, out dt))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA hojoin = new M_WORK_CLOSED_UNTENSHA();
            hojoin.SHAIN_CD = hojoinCd;
            hojoin.CLOSED_DATE = dt;
            M_WORK_CLOSED_UNTENSHA[] untenshas = this.workClosedUntenshaDao.GetAllValidData(hojoin);
            if (untenshas.Length > 0)
            {
                // エラーメッセージ
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["HOJOIN_CD"], true);
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E206", "補助員", string.Format("作業日：{0}", dt.ToString("yyyy/MM/dd")));
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 補助員CDValidated処理
        /// <summary>
        /// 補助員CDValidated処理
        /// </summary>
        /// <param name="e">対象Cell</param>
        internal bool hojoinCDValidatedProc(DataGridViewCellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                // カレント行のセット
                var row = this.form.DetailIchiran.Rows[e.RowIndex];

                if (row.Cells["HOJOIN_CD"].Value != null)
                {
                    // 補助員CDの取得
                    var cd = row.Cells["HOJOIN_CD"].Value.ToString();

                    // 一旦初期化
                    row.Cells["HOJOIN_NAME_RYAKU"].Value = string.Empty;

                    // 入力CDが運転者として登録されているかのチェック
                    var shainFindEntity = new M_SHAIN();
                    shainFindEntity.SHAIN_CD = cd;
                    var shainEntitys = this.shainDao.GetAllValidData(shainFindEntity);

                    if ((shainEntitys != null) && (shainEntitys.Length > 0))
                    {
                        // 社員マスタは社員CDがKeyになっているため唯一がヒットする
                        if (shainEntitys[0].UNTEN_KBN.Value == true)
                        {
                            if (row.Cells["UNTENSHA_CD"].Value != null
                                && !string.IsNullOrEmpty(row.Cells["UNTENSHA_CD"].Value.ToString())
                                && cd.Equals(row.Cells["UNTENSHA_CD"].Value.ToString()))
                            {
                                this.msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                                ret = false;
                            }
                            // 休動チェック
                            else if (!this.ChkHojoinWordClose(row))
                            {
                                ret = false;
                            }
                            else
                            {
                                // 補助員名をセット
                                row.Cells["HOJOIN_NAME_RYAKU"].Value = shainEntitys[0].SHAIN_NAME_RYAKU;
                            }
                        }
                        else
                        {
                            // CDが運転者に該当していなかったため、エラー
                            this.msgLogic.MessageBoxShow("E020", "社員");
                            ret = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("hojoinCDValidatedProc", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
            }
            return ret;
        }
        #endregion
        // 20141015 koukouei 休動管理機能 end

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void SAGYOU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sagyouDateFromTextBox = this.form.SAGYOU_DATE_FROM;
            var sagyouDateToTextBox = this.form.SAGYOU_DATE_TO;
            sagyouDateToTextBox.Text = sagyouDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum, bool outputMsg)
        {
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("0", uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (outputMsg)
                    {
                        this.msgLogic.MessageBoxShow("E261", "現在回収中", "編集");
                    }
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 3"  // 3：定期
                    + " and LDD.REF_DENPYOU_NO = " + uketsukeNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.gyoushaDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    if (outputMsg)
                    {
                        this.msgLogic.MessageBoxShow("E261", "ロジこんぱす連携中", "編集");
                    }
                    return false;
                }

                // NAVITIME連携中であるかをチェックする。
                selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                            + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                            + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                            + " WHERE T.DELETE_FLG = 0 "
                            + " AND T.TEIKI_HAISHA_NUMBER = " + uketsukeNum;

                // データ取得
                dt = this.gyoushaDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    if (outputMsg)
                    {
                        this.msgLogic.MessageBoxShow("E261", "NAVITIME連携中", "編集");
                    }
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion
    }
}