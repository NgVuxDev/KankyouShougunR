// $Id: KaisyaKyujitsuHoshuLogic.cs 4086 2013-10-18 05:24:25Z sys_dev_22 $
using System;
using System.Reflection;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using KaisyaKyujitsuHoshu.APP;
using Seasar.Quill.Attrs;
using KaisyaKyujitsuHoshu.DAO;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon;

namespace KaisyaKyujitsuHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class KaisyaKyujitsuHoshuLogic : IBuisinessLogic
    {
        #region フィールド
        private readonly string ButtonInfoXmlPath = "KaisyaKyujitsuHoshu.Setting.ButtonSetting.xml";
        private readonly string GET_KYUJITU_DATA_SQL = "KaisyaKyujitsuHoshu.Sql.GetKyujituDataSql.sql";
        private readonly string SET_CORPCLOSED_DATA_SQL = "KaisyaKyujitsuHoshu.Sql.DeleteCorpClosedDataSql.sql";
       
        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 自社情報マスタのDao
        /// </summary>
        private IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// 会社休日Dao
        /// </summary>
        private KaisyaKyujitsuHoshuDao kyujitsuDao;

        /// <summary>
        /// DTO
        /// </summary>

        /// <summary>
        /// Form
        /// </summary>
        private KaisyaKyujitsuHoshuForm form;

        /// <summary>
        /// トランザクション
        /// </summary>
        private Transaction Transaction;

        #endregion

        #region プロパティ
        /// <summary>
        /// 休日検索結果
        /// </summary>
        public DataTable SearchResult = new DataTable();

        /// <summary>
        /// 休日情報日曜
        /// </summary>
        public string SysSunday = "";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KaisyaKyujitsuHoshuLogic(KaisyaKyujitsuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.kyujitsuDao = DaoInitUtility.GetComponent<KaisyaKyujitsuHoshuDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();
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
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            parentForm.bt_process1.Text = "[1]";
            parentForm.bt_process1.Enabled = false;
            parentForm.bt_process2.Text = "[2]";
            parentForm.bt_process2.Enabled = false;
            parentForm.bt_process3.Text = "[3]";
            parentForm.bt_process3.Enabled = false;
            parentForm.bt_process4.Text = "[4]";
            parentForm.bt_process4.Enabled = false;
            parentForm.bt_process5.Text = "[5]";
            parentForm.bt_process5.Enabled = false;


        }
        #endregion

        #region 検索、登録、取消
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            //システム首月取得
            DateTime tmpDate = setSysStartMonth();
            this.form.calendarControl1.StartDateTime = tmpDate;
            this.form.calendarControl1.MinDate = tmpDate.AddMonths(1);
            this.form.calendarControl1.MaxDate = tmpDate.AddMonths(7).AddDays(-1);
            //休日リストを設定
            M_CORP_CLOSED Result = new M_CORP_CLOSED();

            //会社休日入力から休日を取得
            SearchResult = kyujitsuDao.GetDataBySqlFile(GET_KYUJITU_DATA_SQL, Result);
            //日曜休日設定を取得
            GetKyuujitsuSundayCheck();

            //画面に休日データを設定
            this.form.calendarControl1.CalendarDataSource = setSundayList();
            this.form.calendarControl1.MaxDateTime = new DateTime(2099, 12, 31);
            this.form.calendarControl1.MinDateTime = new DateTime(2000, 1, 1);
            this.form.calendarControl1.viewCalendar();
            this.form.calendarControl1.beforeButton.Focus();
            LogUtility.DebugMethodEnd();
            return 0;
        }
       
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //エラーではない場合登録処理を行う
            if (!errorFlag)
            {
                M_CORP_CLOSED getResult = new M_CORP_CLOSED();
                //休日設定結果
                DataTable setKyujitsuResult=this.form.calendarControl1.CalendarDataSource;;

                //最大日付と最古日付設定
                M_CORP_CLOSED setStart = new M_CORP_CLOSED();
                M_CORP_CLOSED setEnd = new M_CORP_CLOSED();
                if (this.SysSunday == "1" &&this.SearchResult.Rows.Count.Equals(0))
                {
                    setStart.CORP_CLOSED_DATE = new DateTime(2000,1,1);
                    setEnd.CORP_CLOSED_DATE = new DateTime(2099, 12, 31);
                }
                else if (this.SysSunday == "1" && this.SearchResult.Rows.Count > 0)
                {
                    setStart.CORP_CLOSED_DATE = this.form.calendarControl1.MinDate;
                    setEnd.CORP_CLOSED_DATE = new DateTime(2099, 12, 31);
                }
                else
                {
                    setStart.CORP_CLOSED_DATE = this.form.calendarControl1.MinDate;
                    setEnd.CORP_CLOSED_DATE = this.form.calendarControl1.MaxDate;
                }

                DataView dvResult = setKyujitsuResult.DefaultView;
                dvResult.RowFilter = ("sunday >= '" + setStart.CORP_CLOSED_DATE + "'"
                                             + "and sunday <='" + setEnd.CORP_CLOSED_DATE + "'");
                DataTable newKyujitsuResult = dvResult.ToTable();

                string sql = "INSERT INTO dbo.M_CORP_CLOSED (CORP_CLOSED_DATE, CREATE_USER, CREATE_DATE, CREATE_PC, UPDATE_USER, UPDATE_DATE, UPDATE_PC, DELETE_FLG) VALUES ";
                //ユーザー情報
                String UsrName = System.Environment.UserName;
                UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
                int listCount = newKyujitsuResult.Rows.Count;
                int flag = 0;
                DataTable tmpList = kyujitsuDao.GetDataBySqlFile(GET_KYUJITU_DATA_SQL, getResult);
                if (!ChkBefDel(tmpList))
                {
                    //排他制御
                    msgLogic.MessageBoxShow("E022", "他のユーザにより会社休日");
                    return;
                }
                try
                {
                    using (Transaction tran = new Transaction())
                    {
                        //データをDELETE
                        this.kyujitsuDao.DelDataBySqlFile(SET_CORPCLOSED_DATA_SQL, setStart, setEnd);
                        //データをINSERT
                        for (int i = 0; i < listCount; i++)
                        {
                            //登録のSQL文作成
                            sql = sql + "("
                                      + "CONVERT(DATETIME,'" + newKyujitsuResult.Rows[i][0].ToString() + "', 120)"
                                      + ","
                                      + "'" + UsrName + "'"
                                      + ","
                                      + "CONVERT(DATETIME,'" + DateTime.Now + "', 120)"
                                      + ","
                                      + "'" + System.Environment.MachineName.ToString() + "'"
                                      + ","
                                      + "'" + UsrName + "'"
                                      + ","
                                      + "CONVERT(DATETIME,'" + DateTime.Now + "', 120)"
                                      + ","
                                      + "'" + System.Environment.MachineName.ToString() + "'"
                                      + ","
                                      + 0
                                      + ")";

                            //1000行制御対応
                            if (flag > 998)
                            {
                                sql = sql + ";";
                                flag = 0;
                                this.kyujitsuDao.GetDateForStringSql(sql);
                                sql = "INSERT INTO dbo.M_CORP_CLOSED (CORP_CLOSED_DATE, CREATE_USER, CREATE_DATE, CREATE_PC, UPDATE_USER, UPDATE_DATE, UPDATE_PC, DELETE_FLG) VALUES ";
                            }
                            else if (i == newKyujitsuResult.Rows.Count - 1)
                            {
                                sql = sql + ";";
                                this.kyujitsuDao.GetDateForStringSql(sql);
                            }
                            else
                            {
                                sql = sql + ",";
                                flag++;
                            }
                        }

                        tran.Commit();

                        //画面の内容を初期化
                        this.Search();
                        //msgLogic.MessageBoxShow("I001", "更新");//TODO
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Debug(ex);
                    if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                    {
                        //排他制御
                        msgLogic.MessageBoxShow("E022", "他のユーザにより会社休日");
                        return;
                    }
                    else
                    {
                        //その他
                        throw;
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            //画面の内容を初期化
            this.Search();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// システム設定マスタから休日情報日曜チェックを取得
        /// </summary>
        /// <returns>休日情報日曜チェック(1.する　2.しない)</returns>
        public void GetKyuujitsuSundayCheck()
        {
            int temp = 0;

            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo.Length > 0)
            {
                temp = int.Parse(sysInfo[0].KYUUJITSU_SUNDAY_CHECK.Value.ToString());//TODO sysInfo[0]
            }

            string kbn = temp < 1 || 2 < temp ? "1" : temp.ToString();

            this.SysSunday = kbn;
        }

        /// <summary>
        /// 自社情報マスタから期首月を取得
        /// </summary>
        /// <returns>期首月</returns>
        private int GetKishuMonth()
        {
            int temp = 0;
            M_CORP_INFO[] corpInfo = this.corpInfoDao.GetAllData();
            if (corpInfo.Length == 0)
            {
                temp = 0;
            }
            else
            {
                temp = Int32.Parse(corpInfo[0].KISHU_MONTH.ToString());//TODO corpInfo[0]
            }
            return temp;
        }

        /// <summary>
        /// システム首月設定
        /// </summary>
        /// <returns></returns>
        private DateTime setSysStartMonth()
        {
            //期首月を取得
            int month = this.GetKishuMonth();
            DateTime startDateTime = new DateTime(DateTime.Now.Year, month, 1);
            if (month <= DateTime.Now.Month)
            {
                startDateTime = startDateTime.AddMonths(-1);
            }
            else
            {
                startDateTime = startDateTime.AddMonths(-13);
            }
            return startDateTime;
        }

        /// <summary>
        /// 日曜休日を設定
        /// </summary>
        /// <returns></returns>
        public DataTable setSundayList()
        {
            this.form.calendarControl1.sysSunday = this.SysSunday;
            DataTable sundayList = new DataTable();
            sundayList.Columns.Add("sunday", typeof(DateTime));
            for (int i = 0; i < this.SearchResult.Rows.Count;i++ )
            {
                sundayList.Rows.Add(this.SearchResult.Rows[i][0]);

            }
            int searchListCn = sundayList.Rows.Count;
            if (this.SysSunday.Equals("1"))
            {
                //日曜休日設定=するの場合
                DateTime endDate = new DateTime(2099, 12, 31);//2099年12月31日
                DateTime startDate = new DateTime();
                
                if (searchListCn.Equals(0))
                {
                    //「会社休日テーブル０件」の場合
                    //2000年1月1日から2099年12月31日までの日曜の会社休日データを作成し、保持リストに追加する
                     startDate = new DateTime(2000, 1, 1);//2000年1月1日
                }
                else
                {
                    //「会社休日テーブル０件以外」の場合
                    //会社休日マスタの最大日付データの次月以降の日曜の会社休日データを作成し、保持リストに追加する
                    startDate = Convert.ToDateTime(SearchResult.Rows[searchListCn - 1][0]).AddMonths(1);
                    startDate = new DateTime(startDate.Year, startDate.Month, 1);
                }
                while (startDate <= endDate)
                {
                    var holidayInfo = HolidayCheckLogic.Holiday(startDate);
                    if (holidayInfo.holiday.Equals(HolidayCheckLogic.HolidayInfo.HOLIDAY.HOLIDAY))//日曜
                    {
                        sundayList.Rows.Add(startDate);
                        startDate = startDate.AddDays(7);
                        continue;
                    }
                    startDate = startDate.AddDays(1);
                }
            }
            return sundayList;
        }
       
        /// <summary>
        /// 排他制御
        /// </summary>
        /// <param name="mop"></param>
        /// <returns></returns>
        private Boolean ChkBefDel(DataTable mop)
        {
            int dataFlag = 0;
            if (mop.Rows.Count == 0 && this.SearchResult.Rows.Count==0)
            {
                return true;
            }
            if (mop.Rows.Count != this.SearchResult.Rows.Count)
            {
                //テーブルのデータ行数変更の場合排他制御
                return false;
            }
            for (int i = 0; i < mop.Rows.Count; i++)
            {
                dataFlag = 0;
                for (int j = 0; j < this.SearchResult.Rows.Count; j++)
                {
                    if (mop.Rows[i][0].Equals(this.SearchResult.Rows[j][0]))
                    {
                        dataFlag = 1;
                        if (!mop.Rows[i][1].Equals(this.SearchResult.Rows[j][1]))
                        {
                            //テーブルのデータ更新の場合、排他制御
                            return false;
                        }
                    }
                }
                if (dataFlag == 0)
                {
                    //テーブルのデータ追加と削除の場合、排他制御
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
