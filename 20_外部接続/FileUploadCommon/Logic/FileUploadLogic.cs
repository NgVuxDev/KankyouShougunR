using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.FileUpload.FileUploadCommon.DTO;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using Shougun.Printing.Common;

namespace Shougun.Core.FileUpload.FileUploadCommon.Logic
{
    /// <summary>
    /// ファイルアップロード共通ロジッククラス
    /// </summary>
    public class FileUploadLogic
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
        /// ファイルデータDao
        /// </summary>
        private FILE_DATADAO fileDataDao;

        /// <summary>
        /// 委託契約基本Dao
        /// </summary>
        private IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao fileLinkItakukeiyakuKihonDao;

        /// <summary>
        /// システム設定入力Dao
        /// </summary>
        private IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileUploadLogic()
        {
            this.msgLogic = new MessageBoxShowLogic();
            this.sysinfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.fileLinkItakukeiyakuKihonDao = DaoInitUtility.GetComponent<IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao>();
            this.fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();
            this.fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
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
                var daoFile = (IS2Container)SingletonS2ContainerFactory.Container.GetComponent(Constans.DAO_FILE);
                var dataSourceFile = (Seasar.Extension.Tx.Impl.TxDataSource)daoFile.GetComponent("DataSource");
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
        private DBConnectionDTO GetDBConnection()
        {
            LogUtility.DebugMethodStart();

            DBConnectionDTO connection = null;

            try
            {
                var sysinfo = sysinfoDao.GetAllDataForCode("0");
                if (sysinfo == null || string.IsNullOrEmpty(sysinfo.DB_FILE_CONNECT))
                {
                    return null;
                }

                // 画面表示名は固定
                connection = new DBConnectionDTO("FILE_DB", sysinfo.DB_FILE_CONNECT);
            }
            catch
            {
                LogUtility.Error(string.Format("{0}の形式が正しくありません。", Path.GetFileName(XmlManager.DBConfigFile)));
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(connection);
            }

            return connection;
        }

        /// <summary>
        /// ファイルをバイナリに変換する。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] FileConvertByte(string file)
        {
            //ファイルをメモリへ読み込む
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);

            //バイナリ形式でプログラム内に読み込む
            BinaryReader br = new BinaryReader(fs);

            byte[] data;//データを格納する変数

            //ファイルサイズを求める
            FileInfo fi = new FileInfo(file);
            long filesize = fi.Length;

            //配列の長さをファイルサイズにして定義
            data = new byte[filesize];

            //１バイトずつ取得しながら１６進数で表示
            for (long i = 0; i < filesize; i++)
            {
                data[i] = br.ReadByte();
            }

            return data;
        }

        /// <summary>
        /// バイナリデータをファイルに変換し、一時フォルダに格納する。
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="form"></param>
        public bool ByteConvertFile(T_FILE_DATA fileData, SuperForm form)
        {
            try
            {
                // クラウドの場合、印刷設定で出力先フォルダが選択されているか確認する。
                if (Initializer.ProcessMode == ProcessMode.CloudServerSideProcess)
                {
                    // 出力フォルダが設定されているかチェックする。
                    if (string.IsNullOrEmpty(LocalDirectories.PrintingDirectory))
                    {
                        this.msgLogic.MessageBoxShow("印刷設定にて出力先フォルダを設定してください。");
                        return false;
                    }
                }
                // 一時フォルダを取得する。（暫定として、印刷設定のディレクトリとする）
                string directory = LocalDirectories.PrintingDirectory;

                // ファイルパスからファイル名を取得する。
                string fileName = Path.GetFileName(fileData.FILE_PATH);
                // 一時フォルダに格納するファイルパスを生成
                string tmpPath = Path.Combine(directory, fileName);

                // バイナリデータをファイルに変換して、一時フォルダに格納する。
                using (FileStream fs = new FileStream(tmpPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(fileData.BINARY_DATA.ToString());
                    }
                }


                // 保存ダイアログを表示する。
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "復元先のフォルダを指定してください。";
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                var initialPath = @"C:\Temp";
                if (Initializer.ProcessMode == ProcessMode.CloudServerSideProcess)
                {
                    initialPath = @"\\tsclient\";
                }
                fbd.SelectedPath = initialPath;
                fbd.ShowNewFolderButton = true;
                //ダイアログを表示する
                if (fbd.ShowDialog(form) == DialogResult.OK)
                {
                    // 保存先のフルパスを生成する。
                    string movePath = Path.Combine(fbd.SelectedPath, fileName);

                    // 一時フォルダから選択されたフォルダへファイルを移動する。
                    File.Move(tmpPath, movePath);
                }
                else
                {
                    // 保存ダイアログでキャンセルされた場合、一時フォルダからファイルを削除する。
                    File.Delete(tmpPath);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルIDリストをもとにデータ削除
        /// </summary>
        /// <param name="fileIdList"></param>
        public void DeleteFileData(List<long> fileIdList)
        {
            if (fileIdList == null || fileIdList.Count == 0)
            {
                return;
            }

            // ファイルIDをもとに、ファイルサーバーのファイルデータを削除する。
            FILE_DATADAO fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            var sb = new StringBuilder();
            foreach (var fileId in fileIdList)
            {
                sb.Append(fileId).Append(",");
            }
            var deleteFileIdList = string.Format("({0})", sb.ToString().TrimEnd(','));
            string sql = string.Format("DELETE FROM T_FILE_DATA WHERE FILE_ID IN {0}", deleteFileIdList);
            fileDataDao.GetDataBySqlFile(sql);
        }

        /// <summary>
        /// 事前チェックを行い、アップロード処理を行う。
        /// </summary>
        /// <param name="filePath">アップロード済みファイルパス（フルパス）</param>
        /// <param name="uploadFile">アップロードファイル名</param>
        /// <returns></returns>
        public bool upload(string uploadZumiFilePath, string uploadFileName)
        {
            // 拡張子チェック
            // 拡張子を取得する。
            string extension = Path.GetExtension(uploadFileName);

            // ★許容する拡張子について要確認
            // ★暫定でPDFのみ可能とする。
            // ★PDFファイルでなければ、エラーメッセージを表示して処理中止とする。
            if (!extension.Equals(".pdf"))
            {
                this.msgLogic.MessageBoxShowError("アップロード可能なファイルの拡張子は、pdfのみです。");
                return false;
            }

            // アップロード済みファイル名入力チェック
            // アップロード済ファイル名がある場合、確認メッセージを表示する。
            if (!string.IsNullOrEmpty(uploadZumiFilePath))
            {
                if (this.msgLogic.MessageBoxShowConfirm("アップロード済みのファイルが存在します。ファイルの上書きをしますか？") == DialogResult.No)
                {
                    return false;
                }
            }

            // ファイルサーバーの容量チェック
            string serverPath;
            // アップロード済みファイルパスがない場合、システム個別設定で設定から取得する。
            if (string.IsNullOrEmpty(uploadZumiFilePath))
            {
                // ユーザ定義情報を取得
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                serverPath = this.GetUserProfileValue(userProfile, "ファイルアップロード参照先");
            }
            else
            {
                serverPath = Path.GetDirectoryName(uploadZumiFilePath);
            }

            // 基準容量をシステム設定から取得する。
            M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();
            Int16 maxInsertCapacity = sysInfo.MAX_INSERT_CAPACITY.Value;

            if (maxInsertCapacity != 0)
            {
                long allFileSize = 0;
                if (!string.IsNullOrEmpty(serverPath))
                {
                    // 登録済ファイルサイズを計算する。
                    // 指定フォルダを起点として、サブフォルダ配下を含めた全てのファイルを取得する。
                    DirectoryInfo di = new DirectoryInfo(serverPath);
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        allFileSize += fi.Length;
                    }
                    // サブフォルダのサイズも加算する。
                    foreach (DirectoryInfo dirInfo in di.GetDirectories())
                    {
                        foreach (FileInfo fi in dirInfo.GetFiles())
                        {
                            allFileSize += fi.Length;
                        }
                    }

                    // 登録済ファイルサイズに登録予定ファイルサイズを加算する。
                    FileInfo upFile = new FileInfo(uploadFileName);
                    allFileSize += upFile.Length;
                }

                // 全ファイルサイズをギガバイト(GB)に変換する。
                allFileSize = allFileSize / 1024 / 1024 / 1024;

                // 全容量　≦　システム設定の最大登録容量(GB) の場合、警告メッセージを表示して処理を継続する。
                if (allFileSize <= maxInsertCapacity)
                {
                    this.msgLogic.MessageBoxShowWarn("ファイルサーバの容量が少なくなっています。\n不要なファイルを削除してください。");
                }
                // 全容量　＞　システム設定の最大登録容量(GB) の場合、エラーメッセージを表示して処理中止とする。
                else if (maxInsertCapacity < allFileSize)
                {
                    this.msgLogic.MessageBoxShowError("ご契約プランのサーバ容量を超えるためファイルアップロードが行えません。\n不要なファイルを削除してください。");
                    return false;
                }
            }

            return true;
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

        /// <summary>
        /// ファイルデータのファイルIDを採番します
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateNumber()
        {
            SqlInt64 returnInt = 1;

            var numberDao = FileConnectionUtility.GetComponent<NUMBER_FILEDAO>();
            var numberEntities = numberDao.GetAllData();
            returnInt = numberDao.GetMaxPlusKey();

            if (numberEntities == null || numberEntities.Count == 0 || numberEntities.First().CURRENT_NUMBER < 1)
            {
                var entity = new S_NUMBER_FILE();
                entity.DENSHU_KBN_CD = (Int16)DENSHU_KBN.FILE_UPLOAD;
                entity.CURRENT_NUMBER = returnInt;
                entity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_FILE>(entity);
                dataBinderEntry.SetSystemProperty(entity, false);

                numberDao.Insert(entity);
            }
            else
            {
                var entity = numberEntities.First();
                entity.CURRENT_NUMBER = returnInt;

                var dataBinderEntry = new DataBinderLogic<S_NUMBER_FILE>(entity);
                dataBinderEntry.SetSystemProperty(entity, false);

                numberDao.Update(entity);
            }

            return returnInt;
        }

        /// <summary>
        /// 指定された数だけファイルIDを発番し、リスト形式で取得
        /// </summary>
        /// <param name="no">発番するファイルIDの数</param>
        /// <returns></returns>
        public List<SqlInt64> CreateNumbers(int no)
        {
            List<SqlInt64> returnList = new List<SqlInt64>();

            if (no < 1)
            {
                return returnList;
            }

            int i = 0;
            while (i < no)
            {
                var number = CreateNumber();
                returnList.Add(number);

                i++;
            }

            return returnList;
        }

        /// <summary>
        /// ダウンロード処理
        /// </summary>
        /// <param name="fileIdList">ファイルIDリスト</param>
        /// <param name="folderPath">保存先フォルダ</param>
        /// <returns>true:正常終了 false:処理失敗</returns>
        public bool DownLoad(List<long> fileIdList, string folderPath)
        {
            if (fileIdList == null || fileIdList.Count == 0 || string.IsNullOrEmpty(folderPath))
            {
                return false;
            }

            FILE_DATADAO fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            var dataList = fileDataDao.GetDataByKeyList(fileIdList);

            foreach (var data in dataList)
            {
                var now = DateTime.Now;
                var fileName = Path.GetFileNameWithoutExtension(@data.FILE_PATH);
                var filePath = string.Format(@"{0}\\{1}_{2}{3}", folderPath, fileName, now.ToString("yyyyMMddHHmmssfff"), data.FILE_EXTENTION);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(data.BINARY_DATA.Value);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="fileId">ファイルID</param>
        /// <returns>true:正常終了 false:処理失敗</returns>
        public bool Preview(long fileId)
        {
            FILE_DATADAO fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            var data = fileDataDao.GetDataByKey(fileId);
            if (data == null)
            {
                this.msgLogic.MessageBoxShowError("該当データが存在しませんでした。");
                return false;
            }

            // 一時ファイル作成
            var folderPath = Path.Combine(Path.GetTempPath(), FileUploadLogic.TEMPORARY_FOLDER);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var now = DateTime.Now;
            var fileName = Path.GetFileNameWithoutExtension(@data.FILE_PATH);
            var filePath = string.Format(@"{0}\\{1}_{2}{3}", folderPath, fileName, now.ToString("yyyyMMddHHmmss"), data.FILE_EXTENTION);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(data.BINARY_DATA.Value);
                }
            }

            // プレビュー処理
            if (AppConfig.IsTerminalMode)
            {
                var r = new ReportInfoBase();
                // 画面ID,WINDOW_IDは特に使用しないので、適当なIDを割り当て
                using (FormReport report = new FormReport(r, "G731", WINDOW_ID.T_FILE_UPLOAD_ICHIRAN))
                {
                    report.Preview(filePath);
                }
                // 一時ファイルの削除は印刷PG起動時(or終了時)に実行
            }
            else
            {
                System.Diagnostics.Process.Start(filePath);
                // 一時ファイルの削除は環境将軍R終了時に実行
            }

            return true;
        }

        /// <summary>
        /// 事前チェックを行い、アップロード処理を行う。
        /// 1ファイル限定でのファイルアップロード
        /// (複数アップロードはファイルアップロード画面から対応してもらう)
        /// </summary>
        /// <param name="uploadFileName">アップロードファイル名（フルパス）</param>
        /// <param name="fileId">登録済みファイルID（新規登録時は0）</param>
        /// <param name="windowId">WINDOW_ID(登録元画面)</param>
        /// <param name="param">呼出し元画面のPK情報</param>
        /// <returns></returns>
        public bool UploadForOneFile(string uploadFileName, long fileId, WINDOW_ID windowId, string[] param)
        {
            // 拡張子を取得する。
            string fileExtension = Path.GetExtension(uploadFileName.ToString());
            /// <summary>アップロード可能な拡張子リスト</summary>
            string[] EXTENSION_KYOKA_LIST = {".pdf", ".png", ".bmp", ".jpg", ".jpeg", ".gif"};

            // 拡張子をチェックする。
            if (!EXTENSION_KYOKA_LIST.Contains(fileExtension.ToLower()))
            {
                this.msgLogic.MessageBoxShowError("アップロード可能なファイルの拡張子は、pdf/png/bmp/jpg/jpeg/gif　のみです。");
                return false;
            }

            // ファイルサイズチェック
            // 最大ファイルサイズ メガバイト(MB)⇒バイト(B)に変換する。
            var sysInfo = sysinfoDao.GetAllDataForCode("0");
            var maxFileSizeByte = sysInfo.MAX_FILE_SIZE.Value * 1024L * 1024L;

            // アップロード予定のファイルサイズをサマリする。
            double uploadYoteiTotalFileSize = 0;
            FileInfo upFile = new FileInfo(uploadFileName.ToString());
            if (maxFileSizeByte < upFile.Length)
            {
                this.msgLogic.MessageBoxShowError(string.Format("ファイルサイズ（MB）が、ファイルアップロードを行えるサイズを超えているためファイルアップロードできませんでした。\r\nファイルサイズを{0}（MB）以下にしてください", sysInfo.MAX_FILE_SIZE.Value));
                return false;
            }
            uploadYoteiTotalFileSize += upFile.Length;

            // ②ファイルサーバ容量チェック
            // アップロード済のファイルサイズを取得。
            double uploadZumiTotalFileSize = this.fileDataDao.GetSumFileLength().Value;
            // 最大登録容量の80%を算出する。
            // ギガバイト(GB)⇒バイト(B)に変換する。
            Int16 maxInsertCapacity = sysInfo.MAX_INSERT_CAPACITY.Value;
            double byteMaxInsertCapacity = (double)maxInsertCapacity * 1024L * 1024L * 1024L;

            // 最大登録容量 < (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計)　の場合
            if (byteMaxInsertCapacity < (uploadZumiTotalFileSize + uploadYoteiTotalFileSize))
            {
                this.msgLogic.MessageBoxShowError("ご契約プランのサーバ容量を超えるためファイルアップロードが行えません。\n不要なファイルを削除してください。");
                return false;
            }
            // 最大登録容量の割合を抽出
            // (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計) / システム設定の最大登録容量
            double limitCheck = (uploadZumiTotalFileSize + uploadYoteiTotalFileSize) / byteMaxInsertCapacity;
            // 最大登録容量の80% < (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計) ≦ 最大登録容量　の場合
            if ( 0.8 < limitCheck
                && (uploadZumiTotalFileSize + uploadYoteiTotalFileSize) <= byteMaxInsertCapacity)
            {
                this.msgLogic.MessageBoxShowWarn("ファイルサーバの容量が少なくなっています。\n不要なファイルを削除してください。");
                // 処理継続
            }

            // Entity作成
            T_FILE_DATA data = new T_FILE_DATA();

            // バイナリデータ
            byte[] binaryData = this.FileConvertByte(uploadFileName);
            data.BINARY_DATA = binaryData;
            // ファイルパス
            data.FILE_PATH = uploadFileName;
            // ファイルの拡張子
            data.FILE_EXTENTION = Path.GetExtension(uploadFileName);
            // ファイルのサイズ(バイト)
            data.FILE_LENGTH = new FileInfo(uploadFileName).Length;
            // ファイルの作成日付
            data.FILE_CREATION_TIME = new SqlDateTime(File.GetCreationTime(uploadFileName));
            // ファイルの最終更新日付
            data.FILE_LAST_WRITE_TIME = new SqlDateTime(File.GetLastWriteTime(uploadFileName));
            // ファイルの読み取り専用
            if (new FileInfo(uploadFileName).IsReadOnly)
            {
                data.IS_READ_ONLY = SqlBoolean.True;
            }
            else
            {
                data.IS_READ_ONLY = SqlBoolean.False;
            }
            // 登録元の画面名
            data.WINDOW_NAME = windowId.ToTitleString();

            // 更新者情報設定
            var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_FILE_DATA>(data);
            dataBinderLogic.SetSystemProperty(data, false);

            // ファイルIDが0の場合は新規登録処理とする
            var isInsert = (0 == fileId);
            if (isInsert)
            {
                data.FILE_ID = this.CreateNumber();

                //inser処理
                this.fileDataDao.Insert(data);
            }
            else
            {
                var fileData = this.fileDataDao.GetDataByKey(fileId);
                if (fileData != null)
                {
                    data.FILE_ID = fileId;

                    data.CREATE_USER = fileData.CREATE_USER;
                    data.CREATE_DATE = fileData.CREATE_DATE;
                    data.CREATE_PC = fileData.CREATE_PC;

                    this.fileDataDao.Update(fileData);
                }
            }

            if (!isInsert)
            {
                return true;
            }

            // 連携テーブル更新
            if (windowId == WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI || windowId == WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI)
            {
                // 委託契約書画面から遷移した場合
                var entity = new M_FILE_LINK_ITAKU_KEIYAKU_KIHON();
                string systemId = param[0].ToString();
                entity.SYSTEM_ID = systemId;
                entity.FILE_ID = data.FILE_ID;
                this.fileLinkItakukeiyakuKihonDao.Insert(entity);
            }
            if (windowId == WINDOW_ID.M_SYS_INFO)
            {
                // システム設定入力画面のアップロードボタンから遷移した場合
                var entity = new M_FILE_LINK_SYS_INFO();
                string sysId = param[0].ToString();
                var fileLink = this.fileLinkSysInfoDao.GetDataBySystemId(sysId);
                if (fileLink.Count == 0) 
                {
                    entity.SYS_ID = sysId;
                    entity.FILE_ID = data.FILE_ID;
                    this.fileLinkSysInfoDao.Insert(entity);
                }
            }
            return true;
        }

    }
}
