// $Id: LogicCls.cs 10115 2013-12-09 05:37:33Z sys_dev_22 $
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
using Seasar.Quill.Attrs;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.KaisyaKyujitsuHoshu.APP;
using Shougun.Core.Master.KaisyaKyujitsuHoshu.DAO;
using System.Collections;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.Master.KaisyaKyujitsuHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.KaisyaKyujitsuHoshu.Setting.ButtonSetting.xml";
        private readonly string GET_KYUJITU_DATA_SQL = "Shougun.Core.Master.KaisyaKyujitsuHoshu.Sql.GetKyujituDataSql.sql";
        private readonly string SET_CORPCLOSED_DATA_SQL = "Shougun.Core.Master.KaisyaKyujitsuHoshu.Sql.DeleteCorpClosedDataSql.sql";
       
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
        private DaoCls kyujitsuDao;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// トランザクション
        /// </summary>
        private Transaction Transaction;

        //2013/12/06 仕様変更 追加 start
        /// <summary>
        /// 日曜日の登録開始日
        /// </summary>
        private DateTime insertSundayStartDate;
        //2013/12/06 仕様変更 追加 end

        public MasterBaseForm parentForm;
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
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.corpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                this.kyujitsuDao = DaoInitUtility.GetComponent<DaoCls>();
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
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.form.calendarControl1.StartDateTime = new DateTime(this.parentForm.sysDate.Year, this.parentForm.sysDate.Month, 01);
                this.form.calendarControl1.viewCalendar();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                this.parentForm = (MasterBaseForm)this.form.Parent;

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

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (MasterBaseForm)this.form.Parent;

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
                //parentForm.bt_process1.Text = "[1]";
                parentForm.bt_process1.Enabled = false;
                //parentForm.bt_process2.Text = "[2]";
                parentForm.bt_process2.Enabled = false;
                //parentForm.bt_process3.Text = "[3]";
                parentForm.bt_process3.Enabled = false;
                //parentForm.bt_process4.Text = "[4]";
                parentForm.bt_process4.Enabled = false;
                //parentForm.bt_process5.Text = "[5]";
                parentForm.bt_process5.Enabled = false;
                parentForm.txb_process.Enabled = false;
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

        #region 検索、登録、取消
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int count = 0;
            try
            {
                //システム首月取得
                DateTime tmpDate = setSysStartMonth();
                this.form.calendarControl1.StartDateTime = tmpDate;
                this.form.calendarControl1.MinDate = tmpDate.AddMonths(1);
                this.form.calendarControl1.MaxDate = tmpDate.AddMonths(7).AddDays(-1);
                //休日リストを設定
                M_CORP_CLOSED Result = new M_CORP_CLOSED();

                //会社休日入力から休日を取得
                //SearchResult = kyujitsuDao.GetDataBySqlFile(GET_KYUJITU_DATA_SQL, Result);
                SearchResult = kyujitsuDao.GetDataBySqlFile(Result);
                //日曜休日設定を取得
                GetKyuujitsuSundayCheck();

                //画面に休日データを設定
                this.form.calendarControl1.CalendarDataSource = setSundayList();
                this.form.calendarControl1.MaxDateTime = new DateTime(2099, 12, 31);
                this.form.calendarControl1.MinDateTime = new DateTime(2000, 1, 1);
                this.form.calendarControl1.viewCalendar();
                count = SearchResult.Rows.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }
       
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //エラーではない場合登録処理を行う
            if (!errorFlag)
            {
                bool ret = true;
                try
                {
                    int flag = 0;
                    M_CORP_CLOSED getResult = new M_CORP_CLOSED();
                    //2013/12/06 仕様変更 修正 start
                    //***********************************
                    //KYUUJITSU_SUNDAY_CHECK = 1(する)且つ データ1件以上の場合、削除期間変更
                    //***********************************
                    //休日設定結果
                    DataTable setKyujitsuResult = this.form.calendarControl1.CalendarDataSource; ;
                    DataView dvResult = setKyujitsuResult.DefaultView;

                    DataTable newKyujitsuResult = new DataTable();

                    //最大日付と最古日付設定
                    M_CORP_CLOSED setStart = new M_CORP_CLOSED();
                    M_CORP_CLOSED setEnd = new M_CORP_CLOSED();
                    if (this.SysSunday == "1" && this.SearchResult.Rows.Count.Equals(0))
                    {
                        setStart.CORP_CLOSED_DATE = new DateTime(2000, 1, 1);
                        setEnd.CORP_CLOSED_DATE = new DateTime(2099, 12, 31);
                        newKyujitsuResult = setKyujitsuResult;
                    }
                    else if (this.SysSunday == "1" && this.SearchResult.Rows.Count > 0)
                    {
                        setStart.CORP_CLOSED_DATE = this.form.calendarControl1.MinDate;
                        setEnd.CORP_CLOSED_DATE = new DateTime(2099, 12, 31);
                        if (this.form.calendarControl1.MaxDate >= insertSundayStartDate)
                        {
                            dvResult.RowFilter = ("sunday >= '" + setStart.CORP_CLOSED_DATE + "'"
                                     + "and sunday <='" + setEnd.CORP_CLOSED_DATE + "'");
                            newKyujitsuResult = dvResult.ToTable();
                        }
                        else if (setEnd.CORP_CLOSED_DATE <= insertSundayStartDate)
                        {
                            dvResult.RowFilter = ("sunday >= '" + setStart.CORP_CLOSED_DATE + "'"
                                                  + "and sunday <='" + this.form.calendarControl1.MaxDate + "'");
                            newKyujitsuResult = dvResult.ToTable();
                            //再設定
                            setEnd.CORP_CLOSED_DATE = this.form.calendarControl1.MaxDate;
                        }
                        else
                        {

                            dvResult.RowFilter = ("sunday >= '" + setStart.CORP_CLOSED_DATE + "'"
                                                         + "and sunday <='" + this.form.calendarControl1.MaxDate + "'");
                            newKyujitsuResult = dvResult.ToTable();

                            DataView dvSundayResult = setKyujitsuResult.DefaultView;
                            dvSundayResult.RowFilter = ("sunday >= '" + insertSundayStartDate + "'"
                                                        + "and sunday <='" + setEnd.CORP_CLOSED_DATE + "'");

                            DataTable newKyujitsuSundayResult = dvSundayResult.ToTable();
                            //合并DataTable
                            newKyujitsuResult.Merge(newKyujitsuSundayResult);
                            //再設定
                            setEnd.CORP_CLOSED_DATE = this.form.calendarControl1.MaxDate;
                        }
                    }
                    else
                    {
                        setStart.CORP_CLOSED_DATE = this.form.calendarControl1.MinDate;
                        setEnd.CORP_CLOSED_DATE = this.form.calendarControl1.MaxDate;

                        dvResult.RowFilter = ("sunday >= '" + setStart.CORP_CLOSED_DATE + "'"
                                                     + "and sunday <='" + setEnd.CORP_CLOSED_DATE + "'");
                        newKyujitsuResult = dvResult.ToTable();
                    }
                    //2013/12/06 仕様変更 修正 end
                    DataTable tmpList = kyujitsuDao.GetDataBySqlFile(getResult);

                    if (!ChkBefDel(tmpList, Convert.ToDateTime(setStart.CORP_CLOSED_DATE.ToString()), Convert.ToDateTime(setEnd.CORP_CLOSED_DATE.ToString())))
                    {
                        //排他制御
                        msgLogic.MessageBoxShow("E022", "他のユーザにより会社休日");
                        return;
                    }

                    string sql = "INSERT INTO dbo.M_CORP_CLOSED (CORP_CLOSED_DATE, CREATE_USER, CREATE_DATE, CREATE_PC, UPDATE_USER, UPDATE_DATE, UPDATE_PC, DELETE_FLG) VALUES ";

                    int listCount = newKyujitsuResult.Rows.Count;
                    using (Transaction tran = new Transaction())
                    {
                        //データをDELETE
                        //todo
                        this.kyujitsuDao.DelDataBySqlFile(setStart, setEnd);
                        //データをINSERT
                        for (int i = 0; i < listCount; i++)
                        {
                            // 作成と更新情報設定
                            M_CORP_CLOSED setEntity = new M_CORP_CLOSED();
                            var dbLogic = new DataBinderLogic<r_framework.Entity.M_CORP_CLOSED>(setEntity);
                            dbLogic.SetSystemProperty(setEntity, false);

                            //todo
                            //登録のSQL文作成
                            sql = sql + "("
                                      + "CONVERT(DATETIME,'" + newKyujitsuResult.Rows[i][0].ToString() + "', 120)"
                                      + ","
                                      + "'" + setEntity.CREATE_USER + "'"
                                      + ","
                                //+ "CONVERT(DATETIME,'" + DateTime.Now + "', 120)"
                                       + "'" + setEntity.CREATE_DATE + "'"
                                      + ","
                                      + "'" + setEntity.CREATE_PC + "'"
                                      + ","
                                      + "'" + setEntity.UPDATE_USER + "'"
                                      + ","
                                //+ "CONVERT(DATETIME,'" + DateTime.Now + "', 120)"
                                      + "'" + setEntity.UPDATE_DATE + "'"
                                      + ","
                                      + "'" + setEntity.UPDATE_PC + "'"
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
                    }
                }
                catch (NotSingleRowUpdatedRuntimeException ex1)
                {
                    LogUtility.Error("Regist", ex1);
                    this.form.errmessage.MessageBoxShow("E080", "");
                    ret = false;
                }
                catch (SQLRuntimeException ex2)
                {
                    LogUtility.Error("Regist", ex2);
                    this.form.errmessage.MessageBoxShow("E093", "");
                    ret = false;
                }
                catch (Exception ex)
                {
                    LogUtility.Error("Regist", ex);
                    this.form.errmessage.MessageBoxShow("E245", "");
                    ret = false;
                }

                if (ret)
                {
                    msgLogic.MessageBoxShow("I001", "登録");
                    //画面の内容を初期化
                    this.Search(); 
                } 
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                //画面の内容を初期化
                this.Search();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// システム設定マスタから休日情報日曜チェックを取得
        /// </summary>
        public bool GetKyuujitsuSundayCheck()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                int temp = 0;

                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                if (sysInfo.Length > 0)
                {
                    temp = int.Parse(sysInfo[0].KYUUJITSU_SUNDAY_CHECK.Value.ToString());//TODO sysInfo[0]
                }

                string kbn = temp < 1 || 2 < temp ? "1" : temp.ToString();

                this.SysSunday = kbn;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetKyuujitsuSundayCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetKyuujitsuSundayCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 自社情報マスタから期首月を取得
        /// </summary>
        /// <returns>期首月</returns>
        private int GetKishuMonth()
        {
            LogUtility.DebugMethodStart();
            int temp = 0;
            try
            {
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
            catch (Exception ex)
            {
                LogUtility.Error("GetKishuMonth", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(temp);
            }
        }

        /// <summary>
        /// システム首月設定
        /// </summary>
        /// <returns></returns>
        private DateTime setSysStartMonth()
        {
            LogUtility.DebugMethodStart();
            DateTime startDateTime = this.parentForm.sysDate;
            try
            {
                //期首月を取得
                int month = this.GetKishuMonth();
                startDateTime = new DateTime(this.parentForm.sysDate.Year, month, 1);
                if (month <= this.parentForm.sysDate.Month)
                {
                    startDateTime = startDateTime.AddMonths(-1);
                }
                else
                {
                    startDateTime = startDateTime.AddMonths(-13);
                }
                return startDateTime;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setSysStartMonth", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(startDateTime);
            }
        }

        /// <summary>
        /// 日曜休日を設定
        /// </summary>
        /// <returns></returns>
        public DataTable setSundayList()
        {
            DataTable sundayList = new DataTable();
            try
            {
                LogUtility.DebugMethodStart();
                this.form.calendarControl1.sysSunday = this.SysSunday;
                sundayList.Columns.Add("sunday", typeof(DateTime));
                for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                {
                    sundayList.Rows.Add(this.SearchResult.Rows[i][0]);
                }
                int searchListCn = sundayList.Rows.Count;
                if (this.SysSunday.Equals("1"))
                {
                    //日曜休日設定=するの場合
                    DateTime endDate = new DateTime(2099, 12, 31);//2099年12月31日
                    DateTime startDate = this.parentForm.sysDate;

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
                    //2013/12/06 仕様変更 追加 start
                    //***********************************
                    //KYUUJITSU_SUNDAY_CHECK = 1(する)且つ データ1件以上の場合、削除期間変更
                    //***********************************
                    this.insertSundayStartDate = startDate;
                    //2013/12/06 仕様変更 追加 end
                    while (startDate <= endDate)
                    {
                        var holidayInfo = HolidayCheckLogic.Holiday(startDate);
                       // if (holidayInfo.holiday.Equals(HolidayCheckLogic.HolidayInfo.HOLIDAY.HOLIDAY))//日曜
                        //if (holidayInfo.name.Equals("日曜日"))//日曜
                        if(holidayInfo.name!=null&&holidayInfo.name.Equals("日曜日"))//日曜
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
            catch (Exception ex)
            {
                LogUtility.Error("setSundayList", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sundayList);
            }
        }
       
        /// <summary>
        /// 排他制御
        /// </summary>
        /// <param name="mop"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private Boolean ChkBefDel(DataTable mop, DateTime start, DateTime end)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(mop, start, end);
                //2013/12/06 仕様変更 追加 start
                //***********************************
                //KYUUJITSU_SUNDAY_CHECK = 1(する)且つ データ1件以上の場合、削除期間変更
                //***********************************
                //bool flg = false;
                Hashtable ht = new Hashtable();
                //変更期間内の画面初期データ件数と登録前データ件数
                int countOld = 0;
                int countNew = 0;

                //登録時、DBの最古と最大データを準備
                foreach (var mopRow in mop.AsEnumerable())
                {
                    if ((DateTime)mopRow[0] >= start && (DateTime)mopRow[0] <= end)
                    {
                        countOld++;
                        ht.Add(mopRow[0], mopRow[1]);
                    }
                    else if (this.SysSunday == "1" && this.SearchResult.Rows.Count > 0 && end < this.insertSundayStartDate)
                    {
                        //登録時、DBの最大データを変更の場合、排他チェックＮＧ
                        if ((DateTime)mopRow[0] >= this.insertSundayStartDate)
                        {
                            return ret;
                        }
                    }
                }
                //変更期間内、データを削除と更新の排他チェック
                foreach (var searchRow in this.SearchResult.AsEnumerable())
                {
                    if ((DateTime)searchRow[0] >= start && (DateTime)searchRow[0] <= end)
                    {
                        //画面表示時、DBの最古と最大データを準備
                        //flg = true;
                        countNew++;
                        //if (!(ht.ContainsKey(searchRow[0]) && ht.ContainsValue(searchRow[1])))
                        if (!ht.ContainsKey(searchRow[0]))
                        {
                            return ret;
                        }
                        else if ((DateTime)ht[searchRow[0]] != (DateTime)searchRow[1])
                        {
                            return ret;
                        }
                    }
                    else if (this.SysSunday == "1" && this.SearchResult.Rows.Count > 0 && (DateTime)searchRow[0] >= this.insertSundayStartDate)
                    {
                        return ret;
                    }
                }
                //変更期間内、データを追加の排他チェック
                if (countOld != countNew)
                {
                    //変更期間内の画面初期データ件数と登録前データ件数不一の場合
                    return ret;
                }
                //2013/12/06 仕様変更 追加 end
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkBefDel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
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
