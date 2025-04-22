using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Xml;
using Seasar.Dao;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Exceptions;

namespace r_framework.Logic
{
    /// <summary>
    /// ファイルアップロード共通ロジッククラス
    /// </summary>
    public class DBConnectionLogLogic
    {
        /// <summary>プレビュー時のダウンロード先フォルダ</summary>
        public static readonly string TEMPORARY_FOLDER = "KankyouShougunR_Preview";
        
        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgLogic = null;

        /// <summary>システム設定</summary>
        private IM_SYS_INFODao sysinfoDao;

        /// <summary>
        /// 委託契約基本Dao
        /// </summary>
        private IT_OPERATE_LOGDao Log_Dao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBConnectionLogLogic()
        {
            this.msgLogic = new MessageBoxShowLogic();
            this.sysinfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.Log_Dao = DaoInitUtilityLOG.GetComponent<IT_OPERATE_LOGDao>();
        }

        /// <summary>
        /// DB接続
        /// </summary>
        /// <returns></returns>
        public bool ConnectDB()
        {
            // 接続情報の取得
            var dto = GetDBConnection();

            // 接続可能の場合、DataSourceを再生成する。
            if (dto != null && dto.CanConnect())
            {
                var daoLog = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO_LOG);
                var dataSourceFile = (Seasar.Extension.Tx.Impl.TxDataSource)daoLog.GetComponent("DataSource");
                dataSourceFile.ConnectionString = dto.ConnectionString;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// DB接続可能か判定
        /// </summary>
        /// <returns></returns>
        public bool CanConnectDB()
        {
            // 接続情報の取得
            var dto = GetDBConnection();
            if (dto == null || !dto.CanConnect())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// XMLからDB接続情報取得
        /// </summary>
        /// <returns></returns>
        private DBConnectionDTOLOG GetDBConnection()
        {
            LogUtility.DebugMethodStart();

            DBConnectionDTOLOG connection = null;

            try
            {
                var sysinfo = sysinfoDao.GetAllDataForCode("0");
                if (sysinfo == null || string.IsNullOrEmpty(sysinfo.DB_LOG_CONNECT))
                {
                    return null;
                }

                // 画面表示名は固定
                connection = new DBConnectionDTOLOG("LOGDB", sysinfo.DB_LOG_CONNECT);
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", ""));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(connection);
            }

            return connection;
        }
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
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
        public void InserDBLog(string caption)
        {
            try
            {
                using (TransactionUtility tran = new TransactionUtility())
                {
                    GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                    var dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
                    var sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);//取得した結果をDateTimeに転換する
                    IT_OPERATE_LOGDao daoentry = DaoInitUtilityLOG.GetComponent<IT_OPERATE_LOGDao>();
                    T_OPERATE_LOG entry = new T_OPERATE_LOG();
                    entry.SYSTEM_ID = Int64.Parse(daoentry.GetMaxSystem_ID());
                    entry.OPERATE_DATE = sysDate.ToString("yyyy/MM/dd");
                    entry.GAMEN_NAME = caption;
                    var dataBinderOperate = new DataBinderLogic<T_OPERATE_LOG>(entry);
                    dataBinderOperate.SetSystemProperty(entry, false);
                    entry.OPERATE_PC = entry.CREATE_PC;
                    entry.OPERATE_USER = entry.CREATE_USER;
                    daoentry.Insert(entry);
                    tran.Commit();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("InserDBLog", ex1);
                this.msgLogic.MessageBoxShow("E350", "");
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("InserDBLog", ex1);
                this.msgLogic.MessageBoxShow("E350", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("InserDBLog", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }
    }
}
