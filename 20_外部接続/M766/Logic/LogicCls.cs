using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku
{
    /// <summary>
    /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力のエンティティ
        /// </summary>
        private M_SMS_RECEIVER[] entitys;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者のDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        internal DataTable SearchResult { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        private DataTable dtDetailList = new DataTable();

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        private DtoCls receiverDto { get; set; }

        /// <summary>
        /// システムIDの最大値を生成した回数
        /// </summary>
        private int systemIdCount;

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.receiverDto = new DtoCls();
            this.systemIdCount = 0;
            //メッセージ
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            //前の表示条件を初期値に設定する
            if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
            }

            this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
            this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
            this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
            this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M766", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.DispReferenceMode();
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.smsReceiverIchiran.ReadOnly = true;
            this.form.smsReceiverIchiran.AllowUserToAddRows = false;
            this.form.smsReceiverIchiran.IsBrowsePurpose = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            ButtonSetting[] rtn;

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            rtn = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            return rtn;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //SMS登録ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.ListRegist);

            //SMS削除ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.ListDelete);

            // 削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_SMS_RECEIVER entitySmsReceiver = new M_SMS_RECEIVER();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    bool isExistSmsReceiver = this.EntityExistCheck(entitySmsReceiver, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistSmsReceiver)
                    {
                        // 検索条件の設定(ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ)
                        entitySmsReceiver.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }
            this.receiverDto.ReceiverSearchString = entitySmsReceiver;
        }

        /// <summary>
        /// Entity内のプロパティに指定プロパティが存在するかチェック
        /// </summary>
        /// <param name="entity">マスタEntity</param>
        /// <param name="dbFieldName">存在チェックしたいプロパティ名</param>
        /// <returns>true:プロパティあり、false:プロパティなし</returns>
        private bool EntityExistCheck(object entity, string dbFieldName)
        {
            bool result = false;

            // マスタEntityのプロパティ取得
            var properties = entity.GetType().GetProperties();

            // プロパティ名検索
            foreach (var property in properties)
            {
                if (property.Name == dbFieldName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        #endregion

        #endregion

        #region 業務処理

        #region DataTableのクローン処理

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }
            return table;
        }

        #endregion

        #region 更新処理(未使用)
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 論理削除処理
        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!isSelect)
                {
                    this.msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    var result = this.msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        using (Transaction tran = new Transaction())
                        {
                            foreach (M_SMS_RECEIVER clientIdEntity in this.entitys)
                            {
                                M_SMS_RECEIVER entity = this.dao.GetDataBySystemId(clientIdEntity.SYSTEM_ID.ToString());
                                if (entity != null)
                                {
                                    this.dao.Update(clientIdEntity);
                                }
                            }
                            tran.Commit();
                        }
                        this.msgLogic.MessageBoxShow("I001", "削除");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);// 例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); // 排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error("LogicalDelete", ex); // その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理(未使用)
        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region データ取得処理
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
                SetSearchString();

                // 画面上の携帯番号の値チェック
                if(!string.IsNullOrEmpty(this.form.PHONE_NUMBER.Text))
                {
                    this.receiverDto.ReceiverSearchString.MOBILE_PHONE_NUMBER = this.form.PHONE_NUMBER.Text;
                }
                // 検索SQL設定
                string sql = this.ReceiverSearchSql(this.receiverDto.ReceiverSearchString);
                // 受信者名を条件として取得
                this.SearchResult = dao.GetIchiranReceiverDataSql(sql);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        // トランザクション開始
                        foreach (M_SMS_RECEIVER smsReceiverEntity in this.entitys)
                        {
                            M_SMS_RECEIVER entity = this.dao.GetDataBySystemId(smsReceiverEntity.SYSTEM_ID.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(smsReceiverEntity);
                            }
                            else
                            {
                                this.dao.Update(smsReceiverEntity);
                            }
                        }
                        tran.Commit();
                    }
                    this.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        internal bool Cancel()
        {
            // 検索項目を初期値にセットする
            this.form.PHONE_NUMBER.Text = "";
            this.form.CONDITION_VALUE.Text = "";
            this.form.CONDITION_VALUE.DBFieldsName = "";
            this.form.CONDITION_VALUE.ItemDefinedTypes = "";
            this.form.CONDITION_ITEM.Text = "";

            return true;
        }

        #endregion

        #region 携帯番号の重複チェック
        /// <summary>
        /// 携帯番号の重複チェック
        /// </summary>
        /// <param name="phone_Number">携帯番号</param>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool DuplicationCheck(string phone_Number, int sysId, out bool catchErr)
        {
            LogUtility.DebugMethodStart(phone_Number, sysId);
            catchErr = true;

            try
            {
                // 画面で携帯番号重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.smsReceiverIchiran.Rows.Count - 1; i++)
                {
                    string strNumber = this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString();
                    if (strNumber.Equals(Convert.ToString(phone_Number)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で携帯番号チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (phone_Number.Equals(dtDetailList.Rows[i][1]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで携帯番号重複チェック
                M_SMS_RECEIVER entity = this.dao.GetDataByPhoneNumber(phone_Number);

                if (entity != null && entity.SYSTEM_ID != sysId)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
                
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(false, catchErr);

            return false;
        }

        #endregion

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        /// <param name="catchErr"></param>
        internal bool CreateEntity(bool isDelete, bool isRenkei, out bool catchErr)
        {
            LogUtility.DebugMethodStart(isDelete, isRenkei);
            catchErr = true;
            // システムID生成回数を初期化
            this.systemIdCount = 0;

            try
            {
                var entityList = new M_SMS_RECEIVER[this.form.smsReceiverIchiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_SMS_RECEIVER();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_SMS_RECEIVER>(entityList);
                DataTable dt = this.form.smsReceiverIchiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, false, false, catchErr);
                    return false;
                }

                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_SMS_RECEIVER> mPhoneNumberList = new List<M_SMS_RECEIVER>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (this.form.smsReceiverIchiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.smsReceiverIchiran.Rows[i].Cells["CREATE_USER"].Value.ToString()))
                            {
                                isSelect = true;
                                continue;
                            }
                            isSelect = true;
                            var m_PhoneNumberEntity = CreateEntityForDataGridRow(this.form.smsReceiverIchiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_PhoneNumberEntity, true);
                            m_PhoneNumberEntity.DELETE_FLG = true;
                            mPhoneNumberList.Add(m_PhoneNumberEntity);
                        }
                    }
                }

                if (isRenkei)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value != null && (bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value)
                        {
                            var m_PhoneNumberEntity = CreateEntityForDataGridRow(this.form.smsReceiverIchiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_PhoneNumberEntity, true);
                            mPhoneNumberList.Add(m_PhoneNumberEntity);
                        }
                    }
                }

                if (isDelete || isRenkei)
                {
                    this.entitys = mPhoneNumberList.ToArray();
                }
                else
                {
                    // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!DataTableUtility.IsDataRowChanged(row))
                        {
                            row.AcceptChanges();
                        }
                    }

                    // 変更分のみ取得
                    List<M_SMS_RECEIVER> addList = new List<M_SMS_RECEIVER>();

                    DataTable dtChange = new DataTable();

                    if (dt.GetChanges() == null)
                    {
                        LogUtility.DebugMethodEnd(false, false, false, catchErr);
                        return false;
                    }
                    else
                    {
                        dtChange = dt.GetChanges();
                    }
                    var smsReceiverEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < smsReceiverEntityList.Count; i++)
                    {
                        var smsReceiverEntity = smsReceiverEntityList[i];
                        for (int j = 0; j < this.form.smsReceiverIchiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.smsReceiverIchiran.Rows[j].Cells["MOBILE_PHONE_NUMBER"].Value.Equals(Convert.ToString(smsReceiverEntity.MOBILE_PHONE_NUMBER)) &&
                                     bool.Parse(this.form.smsReceiverIchiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(smsReceiverEntity, false);

                                addList.Add(smsReceiverEntity);
                                break;
                            }
                        }
                        this.form.smsReceiverIchiran.DataSource = preDt;
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, true, catchErr);
            return true;
        }

        #endregion

        #region CreateEntityForDataGrid
        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns>entityList</returns>
        private List<M_SMS_RECEIVER> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            // システムID生成回数を初期化
            this.systemIdCount = 0;
            var entityList = new List<M_SMS_RECEIVER>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataRow(gridView.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        #endregion

        #region CreateEntityForDataGridRow
        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mClientId
        /// </returns>
        private M_SMS_RECEIVER CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_SMS_RECEIVER mPhoneNumber = new M_SMS_RECEIVER();

            // SYSTEM_ID
            if (!DBNull.Value.Equals(row.Cells["SYSTEM_ID"].Value))
            {
                mPhoneNumber.SYSTEM_ID = SqlInt32.Parse(row.Cells["SYSTEM_ID"].Value.ToString());
            }
            else
            {
                mPhoneNumber.SYSTEM_ID = this.dao.GetMaxPlusKey() + systemIdCount;
                systemIdCount++;
            }

            // RENKEI_FLG
            if (!DBNull.Value.Equals(row.Cells["RENKEI_FLG"].Value))
            {
                mPhoneNumber.RENKEI_FLG = (Boolean)row.Cells["RENKEI_FLG"].Value;
            }
            else
            {
                mPhoneNumber.RENKEI_FLG = false;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
            {
                mPhoneNumber.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
            }
            else
            {
                mPhoneNumber.DELETE_FLG = false;
            }

            // MOBILE_PHONE_NUMBER
            if (!DBNull.Value.Equals(row.Cells["MOBILE_PHONE_NUMBER"].Value))
            {
                if (row.Cells["MOBILE_PHONE_NUMBER"].Value.ToString().Contains("-"))
                {
                    row.Cells["MOBILE_PHONE_NUMBER"].Value = row.Cells["MOBILE_PHONE_NUMBER"].Value.ToString().Replace("-", "");
                }
                mPhoneNumber.MOBILE_PHONE_NUMBER = (string)row.Cells["MOBILE_PHONE_NUMBER"].Value;
            }

            // RECEIVER_NAME
            if (!DBNull.Value.Equals(row.Cells["RECEIVER_NAME"].Value))
            {
                mPhoneNumber.RECEIVER_NAME = (string)row.Cells["RECEIVER_NAME"].Value;
            }

            // BIKOU
            if (!DBNull.Value.Equals(row.Cells["BIKOU"].Value))
            {
                mPhoneNumber.BIKOU = (string)row.Cells["BIKOU"].Value;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mPhoneNumber.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mPhoneNumber);
            return mPhoneNumber;
        }

        #endregion

        #region CreateEntityForDataRow
        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private M_SMS_RECEIVER CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_SMS_RECEIVER mPhoneNumber = new M_SMS_RECEIVER();

            // SYSTEM_ID
            if (!DBNull.Value.Equals(row["SYSTEM_ID"]) && !"".Equals(row["SYSTEM_ID"]))
            {
                mPhoneNumber.SYSTEM_ID = SqlInt32.Parse(row["SYSTEM_ID"].ToString());
            }
            else
            {
                mPhoneNumber.SYSTEM_ID = this.dao.GetMaxPlusKey() + systemIdCount;
                systemIdCount++;
            }

            // RENKEI_FLG
            if (!DBNull.Value.Equals(row["RENKEI_FLG"]))
            {
                mPhoneNumber.RENKEI_FLG = (Boolean)row["RENKEI_FLG"];
            }
            else
            {
                mPhoneNumber.RENKEI_FLG = false;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mPhoneNumber.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mPhoneNumber.DELETE_FLG = false;
            }

            // MOBILE_PHONE_NUMBER
            if (!DBNull.Value.Equals(row["MOBILE_PHONE_NUMBER"]))
            {
                if (row["MOBILE_PHONE_NUMBER"].ToString().Contains("-"))
                {
                    row["MOBILE_PHONE_NUMBER"] = row["MOBILE_PHONE_NUMBER"].ToString().Replace("-", "");
                }
                mPhoneNumber.MOBILE_PHONE_NUMBER = (string)row["MOBILE_PHONE_NUMBER"];
            }

            // RECEIVER_NAME
            if (!DBNull.Value.Equals(row["RECEIVER_NAME"]))
            {
                mPhoneNumber.RECEIVER_NAME = (string)row["RECEIVER_NAME"];
            }

            // BIKOU
            if (!DBNull.Value.Equals(row["BIKOU"]))
            {
                mPhoneNumber.BIKOU = (string)row["BIKOU"];
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mPhoneNumber.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mPhoneNumber);

            return mPhoneNumber;
        }

        #endregion

        #region 登録前のチェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            ArrayList errColName = new ArrayList();

            Boolean rtn = false;

            try
            {
                Boolean isErr;

                // 必須入力チェック
                // 携帯電話番号
                isErr = false;
                for (int i = 0; i < this.form.smsReceiverIchiran.RowCount - 1; i++)
                {
                    if (this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value != null && this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True"
                        && (this.form.smsReceiverIchiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.smsReceiverIchiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }

                    if (null == this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value ||
                        DBNull.Value == this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value ||
                        "".Equals(this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString().Trim()))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("携帯電話番号");
                            isErr = true;
                        }
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"], true);
                    }
                }

                rtn = (errColName.Count == 0);

                MessageUtility messageUtility = new MessageUtility();
                string message = messageUtility.GetMessage("E001").MESSAGE;

                string errMsg = "";
                if (!rtn)
                {
                    foreach (string colName in errColName)
                    {
                        if (errMsg.Length > 0)
                        {
                            errMsg += "\n";
                        }
                        errMsg += message.Replace("{0}", colName);
                    }
                    MessageBox.Show(errMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
        }

        #endregion

        #region DataGridViewデータ件数チェック処理
        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        internal bool ActionBeforeCheck()
        {
            if (this.form.smsReceiverIchiran.Rows.Count > 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region NOT NULL制約を一時的に解除
        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        internal void ColumnAllowDBNull()
        {
            DataTable dt = this.form.smsReceiverIchiran.DataSource as DataTable;
            DataTable preDt = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;

                // TIME_STAMPがなぜか一意制約有のため、解除
                if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }
        }

        #endregion

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

        #region その他
        /// <summary>
        /// 表示条件の選択状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false
                && this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked == false)
            {
                this.msgLogic.MessageBoxShow("E001", "表示条件");
                ((CheckBox)sender).Checked = true;
            }
        }
        #endregion

        #region ｼｮｰﾄﾒｯｾｰｼﾞ登録・削除・API
        // API連携時のメッセージ内容
        private string smsMsg = string.Empty;

        /// <summary>
        /// ﾘｽﾄ登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void ListRegist()
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // トランザクション開始
                    foreach (M_SMS_RECEIVER smsReceiverEntity in this.entitys)
                    {
                        M_SMS_RECEIVER entity = this.dao.GetDataBySystemId(smsReceiverEntity.SYSTEM_ID.ToString());
                        if (entity == null)
                        {
                            this.dao.Insert(smsReceiverEntity);
                        }
                        else
                        {
                            this.dao.Update(smsReceiverEntity);
                        }
                    }
                    tran.Commit();
                }

                // 送信リスト設定変更API実行(登録タイプ：追加)
                ListUpdateAPI(true, false);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error("ListRegist", ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ﾘｽﾄ削除（ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ）
        /// </summary>
        public void ListDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    foreach (M_SMS_RECEIVER smsReceiverEntity in this.entitys)
                    {
                        M_SMS_RECEIVER entity = this.dao.GetDataBySystemId(smsReceiverEntity.SYSTEM_ID.ToString());
                        if (entity != null)
                        {
                            this.dao.Update(smsReceiverEntity);
                        }
                    }
                    tran.Commit();
                }

                // 送信リスト設定変更API実行(登録タイプ：上書き)
                ListUpdateAPI(false, true);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error("ListDelete", ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 送信リスト設定変更API
        /// </summary>
        /// <param name="registFlg"></param>
        /// <param name="deleteFlg"></param>
        public void ListUpdateAPI(bool registFlg, bool deleteFlg)
        {
            smsMsg = string.Empty;
            bool smsResponseFlg = false;
            var renkeiUpdateList = new List<M_SMS_RECEIVER>();
            var deleteList = new List<M_SMS_RECEIVER>();

            try 
            {
                // TLS1.2を指定（指定しないとAPI連携不可）
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);

                var url = "https://push-se.karaden.jp/v2/karadensetfilter.json";

                #region パラメータ項目チェック
                M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();

                string token = sysInfo.KARADEN_ACCESS_KEY;
                //var token = "K5QBgBsQzyFGB";

                string securitycode = sysInfo.KARADEN_SECURITY_CODE;
                //var securitycode = "Ed203388";
                var type = string.Empty;
                var phoneNumber = string.Empty;
                var status = "1";

                #endregion

                StringBuilder sbJson = new StringBuilder();

                sbJson.Append("{");
                sbJson.Append("\"token\":\"" + token + "\",");
                sbJson.Append("\"securitycode\":\"" + securitycode + "\",");
                if (registFlg) { type = "1"; } //追加
                else { type = "0"; } // 上書き
                sbJson.Append("\"type\":\"" + type + "\",");
                sbJson.Append("\"list\":[");
                for(int i = 0; i < this.form.smsReceiverIchiran.RowCount - 1; i++)
                {
                    if (!(bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value && 
                        !(bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value &&
                        !"済".Equals(this.form.smsReceiverIchiran.Rows[i].Cells["RENKEI_STATUS"].Value.ToString()))
                    {
                        continue;
                    }
                    var pNumber = this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString();
                    var addFlg = false;
                    var delFlg = false;
                    //if (this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value != null && (bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value)
                    if((bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value)
                    {
                        addFlg = true;
                    }
                    if((bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value)
                    {
                        delFlg = true;
                    }
                    M_SMS_RECEIVER entity = null;
                    M_SMS_RECEIVER delEntity = null;
                    if (registFlg)
                    {
                        if (addFlg)
                        {
                            entity = this.dao.GetDataByPhoneNumber(pNumber);
                        }
                    }
                    else
                    {
                        if(delFlg == true)
                        {
                            // リストから削除する携帯番号のEntity
                            delEntity = this.dao.GetData_Delete(pNumber);

                            // 送信リストから削除する携帯番号一覧
                            deleteList.Add(delEntity);
                        }
                        else
                        {
                            // リストに登録を行う携帯番号のEntity
                            entity = this.dao.GetData_NotDelete(pNumber);
                        }
                    }
                    if (entity != null)
                    {
                        renkeiUpdateList.Add(entity);

                        // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ参照
                        sbJson.Append("{\"phonenumber\":\"" + pNumber + "\",");
                        sbJson.Append("\"status\":\"" + status + "\"},");
                    }
                }
                    
                // 余計な文字列が入らないように一部文字を取り除く
                sbJson.Remove(sbJson.Length - 1, 1);
                sbJson.Append("]}");

                // リクエスト作成
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/json";

                // ポストデータをリクエストに書き込む
                using (StreamWriter reqStreamWriter = new StreamWriter(req.GetRequestStream()))
                    reqStreamWriter.Write(sbJson);

                // レスポンスの取得
                WebResponse response = (HttpWebResponse)req.GetResponse();
                RES_SMS_LIST_SETTING_CHANGE_API result = null;

                // 結果の読み込み
                using (Stream resStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(resStream, Encoding.GetEncoding("Shift_JIS")))
                    {
                        smsMsg += "【ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ】\n";
                        smsMsg += "正常に完了しました。\n\n";

                        var serializer = new DataContractJsonSerializer(typeof(RES_SMS_LIST_SETTING_CHANGE_API));
                        result = (RES_SMS_LIST_SETTING_CHANGE_API)serializer.ReadObject(resStream);

                        this.smsApiMessage(result, renkeiUpdateList, out smsResponseFlg);

                        if (smsResponseFlg)
                        {
                            using (Transaction tran = new Transaction())
                            {
                                // リストへの追加・更新トランザクション
                                foreach (M_SMS_RECEIVER RenkeiEntity in renkeiUpdateList)
                                {
                                    if (!RenkeiEntity.RENKEI_FLG)
                                    {
                                        RenkeiEntity.RENKEI_FLG = true;
                                    }
                                    this.dao.Update(RenkeiEntity);
                                }

                                // リストから削除された携帯番号の更新トランザクション
                                foreach (M_SMS_RECEIVER DeleteEntity in deleteList)
                                {
                                    DeleteEntity.RENKEI_FLG = false;
                                    this.dao.Update(DeleteEntity);
                                }
                                tran.Commit();
                            }
                        }
                    }
                }
            }
            #region API連携時のエラー
            catch (WebException webEx)
            {
                // WebExceptionだけ一括でエラー処理をする
                LogUtility.Error("ListUpdateAPI", webEx);

                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse errRes = (HttpWebResponse)webEx.Response;
                    var title = string.Empty;

                    switch (errRes.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:         // 400
                            // リクエスト不正
                            title = "HTTP STATUS 400 Bad Request";
                            break;
                        case HttpStatusCode.Unauthorized:       // 401
                            // アクセストークン無効
                            title = "HTTP STATUS 401 Unauthorized";
                            break;
                        case HttpStatusCode.PaymentRequired:    // 402
                            // 
                            title = "HTTP STATUS 402 Payment Required";
                            break;
                        case HttpStatusCode.Forbidden:          // 403
                            // アクセス拒否
                            title = "HTTP STATUS 403 Forbidden";
                            break;
                        case HttpStatusCode.NotFound:           // 404
                            // 指定されたページが存在しない。権限が無い。
                            title = "HTTP STATUS 404 Not Found";
                            break;
                        case HttpStatusCode.MethodNotAllowed:   // 405
                            // 未許可のメソッド
                            title = "HTTP STATUS 405 Method Not Allowed";
                            break;
                        case HttpStatusCode.InternalServerError:// 500
                            // サーバ内部エラー
                            title = "HTTP STATUS 500 Internal Server Error";
                            break;
                        default:
                            title = "その他エラー";
                            break;
                    }
                    this.msgLogic.MessageBoxShowError(string.Format("API連携において、エラーが発生しました。\r\nエラー内容：{0}", title));
                }
                else
                {
                    this.msgLogic.MessageBoxShowError("エラーが発生しました。");
                }
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListUpdateAPI", ex);
                this.msgLogic.MessageBoxShowError("エラーが発生しました。");
            }
            #endregion
        }

        #region SMSAPIレスポンスメッセージ一覧
        /// <summary>
        /// SMSAPIレスポンスメッセージ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="smsResponseFlg">true:正常、false:失敗</param>
        public void smsApiMessage(RES_SMS_LIST_SETTING_CHANGE_API result, List<M_SMS_RECEIVER> renkeiList, out bool smsResponseFlg)
        {
            smsResponseFlg = false;
            smsMsg += "【空電プッシュ-送信リスト】\n";

            if (result.Status.Contains("100"))
            {
                smsMsg += "正常に完了しました。";
                
                this.msgLogic.MessageBoxShowInformation(smsMsg);

                smsResponseFlg = true;
            }
            else if (result.Status.Contains("201"))
            {
                smsMsg += "認証に失敗しました。\nアクセスキー、セキュリティーコードなどに誤りがあります。\n確認を行ってください（コード：201）";
            }
            else if (result.Status.Contains("900"))
            {
                smsMsg += "送信リストの登録に失敗しました。エラー内容を確認してください。\n";

                foreach (ERRORS errors in result.Errors)
                {
                    for(int i = 0; i < renkeiList.Count; i++)
                    {
                        if(i + 1 == int.Parse(errors.Errorrow))
                        {
                            if (errors.Errorstatus.Contains("202"))
                            {
                                smsMsg += string.Format("・携帯番号に誤りがあります。確認を行ってください（エラーコード：202）（携帯番号：{0}）\n", renkeiList[i].MOBILE_PHONE_NUMBER);
                            }
                            if (errors.Errorstatus.Contains("901"))
                            {
                                smsMsg += string.Format("・既に登録済みの携帯番号です。確認を行ってください（コード：901）（携帯番号：{0}）\n", renkeiList[i].MOBILE_PHONE_NUMBER);
                            }
                            if (errors.Errorstatus.Contains("903"))
                            {
                                smsMsg += "・送信リストが未指定です （コード：903）。\n";
                            }
                        }
                    }
                }
            }
            else
            {
                smsMsg += "送信リストの登録に失敗しました。\n再度実行しても失敗する場合は、システム管理者にご連絡ください。";
            }
            if (!smsResponseFlg)
            {
                this.msgLogic.MessageBoxShowError(smsMsg);
            }
        }
        #endregion

        #region [F1]ﾘｽﾄ登録　チェック処理
        /// <summary>
        /// [F1]ﾘｽﾄ登録　連携チェックボックスのオン/オフチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsRenkeiCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                // 連携チェックボックスの値チェック
                int RenkeiCheckCount = 0;
                for (int i = 0; i < this.form.smsReceiverIchiran.Rows.Count - 1; i++)
                {
                    if ((bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value)
                    {
                        RenkeiCheckCount++;
                    }
                }

                rtn = (RenkeiCheckCount > 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShowError("処理対象となる明細行を選択してください。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsRenkeiCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd();
            return rtn;
        }

        /// <summary>
        /// [F1]ﾘｽﾄ登録　携帯番号重複チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsDuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                int duplicationCount = 0;

                // 重複チェック
                // 携帯電話番号
                for (int i = 0; i < this.form.smsReceiverIchiran.RowCount - 1; i++)
                {
                    string phone_Number = (string)this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value;
                    int system_Id = 0;
                    if (!DBNull.Value.Equals(this.form.smsReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value))
                    {
                        system_Id = (int)this.form.smsReceiverIchiran.Rows[i].Cells["SYSTEM_ID"].Value;
                    }
                    bool catchErr = true;
                    bool isError = false;
                    if (this.form.smsReceiverIchiran.Rows[i].IsNewRow == true)
                    {
                        isError = this.DuplicationCheck(phone_Number, system_Id, out catchErr);
                    }
                    if (!catchErr)
                    {
                        continue;
                    }
                    if (isError)
                    {
                        duplicationCount++;
                        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"], true);
                    }
                }

                rtn = (duplicationCount == 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShow("E022", "入力された携帯番号");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsDuplicationCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd();
            return rtn;
        }

        /// <summary>
        /// [F1]ﾘｽﾄ登録　連携チェックボックスのオン/オフチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsPhoneNumberCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                // 画面で携帯番号桁数チェック
                int PhoneNumberCount = 0;
                for (int i = 0; i < this.form.smsReceiverIchiran.Rows.Count - 1; i++)
                {
                    if (this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].Value.ToString().Length != 11)
                    {
                        PhoneNumberCount++;
                    }
                }

                rtn = (PhoneNumberCount == 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShowError("携帯番号が正しくありません。正しい番号を入力してください。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsPhoneNumberCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd();
            return rtn;
        }

        #endregion

        #region [F2]ﾘｽﾄ削除　チェック処理
        /// <summary>
        /// [F2]ﾘｽﾄ登録　連携チェックボックスのオン/オフチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsDeleteCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                // 削除チェックボックスの値チェック
                int DeleteCheckCount = 0;
                for (int i = 0; i < this.form.smsReceiverIchiran.Rows.Count - 1; i++)
                {
                    if ((bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value)
                    {
                        DeleteCheckCount++;
                    }
                }

                rtn = (DeleteCheckCount > 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShowError("処理対象となる明細行を選択してください。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsDeleteCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd();
            return rtn;
        }
        #endregion

        /// <summary>
        /// 連携状況更新
        /// </summary>
        internal void SMSRenkeiStatus()
        {
            if (this.form.smsReceiverIchiran != null)
            {
                for(int i = 0; i < this.form.smsReceiverIchiran.RowCount - 1; i++)
                {
                    // 連携チェックボックス、連携状況の既定値設定
                    this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].Value = false;
                    this.form.smsReceiverIchiran.Rows[i].Cells["RENKEI_STATUS"].Value = "";

                    // 連携済みである場合、連携状況に「済」表示と携帯番号の入力不可、連携チェックボックス押下不可
                    if ((bool)this.form.smsReceiverIchiran.Rows[i].Cells["RENKEI_FLG"].Value)
                    {
                        this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].ReadOnly = true;
                        this.form.smsReceiverIchiran.Rows[i].Cells["RENKEI_STATUS"].Value = "済";
                        this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].ReadOnly = true;
                    }
                    else
                    {
                        this.form.smsReceiverIchiran.Rows[i].Cells["MOBILE_PHONE_NUMBER"].ReadOnly = false;

                        // ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタで削除フラグがTrueの場合、連携チェックボックス押下不可
                        if ((bool)this.form.smsReceiverIchiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            this.form.smsReceiverIchiran.Rows[i].Cells["chb_renkei"].ReadOnly = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 検索SQL設定
        /// </summary>
        private string ReceiverSearchSql(M_SMS_RECEIVER receiverEntity)
        {
            string sql = "1 = 1 ";

            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                sql += "AND DELETE_FLG = 0";
            }

            if (receiverEntity.MOBILE_PHONE_NUMBER != null)
            {
                sql += string.Format("AND MOBILE_PHONE_NUMBER LIKE '%{0}%' ", receiverEntity.MOBILE_PHONE_NUMBER);
            }

            if (receiverEntity.RECEIVER_NAME != null)
            {
                sql += string.Format("AND RECEIVER_NAME LIKE '%{0}%' ", receiverEntity.RECEIVER_NAME);
            }

            if (receiverEntity.BIKOU != null)
            {
                sql += string.Format("AND BIKOU LIKE '%{0}%' ", receiverEntity.BIKOU);
            }

            if (receiverEntity.CREATE_USER != null)
            {
                sql += string.Format("AND CREATE_USER LIKE '%{0}%' ", receiverEntity.CREATE_USER);
            }

            if (!receiverEntity.CREATE_DATE.IsNull)
            {
                sql += string.Format("AND CREATE_DATE LIKE '%{0}%' ", receiverEntity.CREATE_DATE);
            }

            if (receiverEntity.UPDATE_PC != null)
            {
                sql += string.Format("AND UPDATE_PC LIKE '%{0}%' ", receiverEntity.UPDATE_PC);
            }

            if (!receiverEntity.UPDATE_DATE.IsNull)
            {
                sql += string.Format("AND UPDATE_DATE LIKE '%{0}%' ", receiverEntity.UPDATE_DATE);
            }

            return sql;
        }

        #endregion
    }
}