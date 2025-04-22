using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class M421Logic : IBuisinessLogic
    {
        /// <summary>
        /// Form
        /// </summary>
        private M421Form form;

        /// <summary>
        /// 
        /// </summary>
        private M421HeaderForm HeaderForm;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Master.OboeGakiIkkatuIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// システムID
        /// </summary>
        private long SystemId;

        /// <summary>
        /// 処理タイプ
        /// </summary>
        public WINDOW_TYPE WinType;

        /// <summary>
        /// 画面間のパラメータ
        /// </summary>
        private T_ITAKU_MEMO_IKKATSU_ENTRY SendDto;

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public T_ITAKU_MEMO_IKKATSU_ENTRY SearchString { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int AlertCount { get; set; }

        /// <summary>
        /// Dao
        /// </summary>
        private IM_ITAKUMEMOIKKATSUDao Dao;
        private IM_SYS_INFODao SysInfoDao;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal M421Logic(M421Form targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.HeaderForm = new M421HeaderForm();

            this.Dao = DaoInitUtility.GetComponent<IM_ITAKUMEMOIKKATSUDao>();
            this.SysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.SendDto = new T_ITAKU_MEMO_IKKATSU_ENTRY();

            this.SystemId = 0;

            LogUtility.DebugMethodEnd();
        }

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 新規ボタン(F2)イベント生成
            this.form.C_Regist(parentForm.bt_func2);
            parentForm.bt_func2.Click += new EventHandler(this.form.SinkiClick);
            parentForm.bt_func2.ProcessKbn = PROCESS_KBN.NEW;

            // 修正ボタン(F3)イベント生成
            this.form.C_Regist(parentForm.bt_func3);
            parentForm.bt_func3.Click += new EventHandler(this.form.ShuuseiClick);
            parentForm.bt_func3.ProcessKbn = PROCESS_KBN.UPDATE;

            // 削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.SakujyoClick);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVClick);

            // 検索
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 明細ダブルクリック
            this.form.Ichiran.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.form.Ichiran_MouseDoubleClick);

            //ESC
            parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            T_ITAKU_MEMO_IKKATSU_ENTRY entity = new T_ITAKU_MEMO_IKKATSU_ENTRY();

            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一覧データ検索
        /// </summary>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = Dao.GetDataForEntity(this.SearchString);

                count = this.SearchResult.Rows == null ? 0 : 1;
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

            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// ヘッダー部 読込み件数
        /// </summary>
        public void SetHearderItem()
        {
            LogUtility.DebugMethodStart();
            //ヘッダー部
            this.HeaderForm = (M421HeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;

            //読込み件数
            this.HeaderForm.txt_YOMIKOMI_KENSU.Text = this.SearchResult.Rows.Count.ToString();
            //// アラート件数
            //M_SYS_INFO sysInfo = this.SysInfoDao.GetAllDataForCode(this.SystemId.ToString());

            //if (sysInfo != null)
            //{
            //    // システム情報からアラート件数を取得
            //    int cnt = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
            //    string str = String.Format("{0:#,000} ", cnt);
            //    this.headerForm.CustomNumericTextBox2_ARAT_KENSU.Text = str;

            //}

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 初期表示のコントロール活性化
        /// </summary>
        private void ControlKasseika()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //件数
            int cnt = this.form.Ichiran.Rows.Count;
            if (cnt <= 0)
            {
                parentForm.bt_func3.Enabled = false;

                parentForm.bt_func4.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F2 新規
        /// </summary>
        public void Sinki()
        {
            LogUtility.DebugMethodStart();

            //システムID
            this.SendDto.SYSTEM_ID = 0;
            //伝票番号
            this.SendDto.DENPYOU_NUMBER = 0;
            //SEQ
            this.SendDto.SEQ = 0;
            //処理タイプ
            this.WinType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            //遷移先画面を呼び出し
            FormManager.OpenFormWithAuth("M014", WINDOW_TYPE.NEW_WINDOW_FLAG, this.WinType, this.SendDto);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        public bool Shuusei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一覧にデータが存在しない場合
                if (this.form.Ichiran.Rows.Count <= 0)
                {
                    // 「対象データを選択してください。」のアラート
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "対象データ");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //空行を外す
                int rowIndex = this.form.Ichiran.CurrentRow.Index;
                if (rowIndex < this.form.Ichiran.Rows.Count)
                {
                    //システムID
                    this.SendDto.SYSTEM_ID = (long)this.form.Ichiran.CurrentRow.Cells["SYSTEM_ID"].Value;
                    //伝票番号
                    this.SendDto.DENPYOU_NUMBER = (long)this.form.Ichiran.CurrentRow.Cells["DENPYOU_NUMBER"].Value;

                    //SEQ
                    this.SendDto.SEQ = (int)this.form.Ichiran.CurrentRow.Cells["SEQ"].Value;

                    //Check is delete
                    if (EntityIsDelete(this.SendDto))
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    //処理タイプ
                    this.WinType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                    //遷移先画面を呼び出し
                    if (Manager.CheckAuthority("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M014", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.SendDto);
                    }
                    else if (Manager.CheckAuthority("M014", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M014", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.SendDto);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Shuusei", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Shuusei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        public bool Sakujyo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一覧にデータが存在しない場合
                if (this.form.Ichiran.Rows.Count <= 0)
                {
                    // 「対象データを選択してください。」のアラート
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "対象データ");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                //空行を外す
                int rowIndex = this.form.Ichiran.CurrentRow.Index;
                if (rowIndex < this.form.Ichiran.Rows.Count)
                {
                    //システムID
                    this.SendDto.SYSTEM_ID = (long)this.form.Ichiran.CurrentRow.Cells["SYSTEM_ID"].Value;
                    //伝票番号
                    this.SendDto.DENPYOU_NUMBER = (long)this.form.Ichiran.CurrentRow.Cells["DENPYOU_NUMBER"].Value;

                    //SEQ
                    this.SendDto.SEQ = (int)this.form.Ichiran.CurrentRow.Cells["SEQ"].Value;

                    //Check is delete
                    if (EntityIsDelete(this.SendDto))
                    {
                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }

                    //処理タイプ
                    this.WinType = WINDOW_TYPE.DELETE_WINDOW_FLAG;

                    //遷移先画面を呼び出し
                    FormManager.OpenFormWithAuth("M014", WINDOW_TYPE.DELETE_WINDOW_FLAG, this.WinType, this.SendDto);
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Sakujyo", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Sakujyo", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Sakujyo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        public void CSVOutput()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (this.form.Ichiran.RowCount < 1)
            {
                msgLogic.MessageBoxShow("E044");
            }
            else if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN), this.form);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        public void FormClose()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #region 20150813 hoanghm add

        /// <summary>
        /// Check entity is delete or not befor often window, if entity is delete then return
        /// </summary>
        /// <param name="sendDto"></param>
        /// <returns>true: is delete; false: is not delete</returns>
        private bool EntityIsDelete(T_ITAKU_MEMO_IKKATSU_ENTRY sendDto)
        {
            var entity = Dao.GetDataByKey(sendDto.SYSTEM_ID.ToString(), sendDto.DENPYOU_NUMBER.ToString(), sendDto.SEQ.ToString());
            if (entity != null)
            {
                if (entity.DELETE_FLG)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return true;
                }
            }
            return false;
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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Insert(bool flg)
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
    }
}