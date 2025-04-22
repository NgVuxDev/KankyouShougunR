using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using System.Threading;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.ExternalConnection.FileUpload.Dtos;
using r_framework.Configuration;
using System.Data;

namespace Shougun.Core.ExternalConnection.FileUpload
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.FileUpload.Setting.ButtonSetting.xml";

        /// <summary>
        /// ファイルデータDao
        /// </summary>
        private FILE_DATADAO fileDataDao;

        /// <summary>
        /// 委託契約基本Dao
        /// </summary>
        private IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao fileLinkItakukeiyakuKihonDao;

        /// <summary>
        /// 地域別許可Dao
        /// </summary>
        private IM_FILE_LINK_CHIIKIBETSU_KYOKADao fileLinkchiikibetsuKyokaDao;

        /// <summary>
        /// 現場メモDao
        /// </summary>
        private IM_FILE_LINK_GENBAMEMO_ENTRYDao fileLinkGenbamemoDao;

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        private FileUploadLogic uploadLogic;

        /// <summary>
        /// ファイルアップロードのEntityクラス
        /// </summary>
        private List<T_FILE_DATA> fileDataList;

        /// <summary>
        /// 画面フォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgBox;

        /// <summary>
        /// 初期フォルダ
        /// </summary>
        private string initFolder;

        /// <summary>
        /// システム設定入力Dao
        /// </summary>
        private IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao;


        #endregion

        #region プロパティ
        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm parentbaseform { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">フォーム</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.msgBox = new MessageBoxShowLogic();

            this.fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            this.fileLinkItakukeiyakuKihonDao = DaoInitUtility.GetComponent<IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao>();
            this.fileLinkchiikibetsuKyokaDao = DaoInitUtility.GetComponent<IM_FILE_LINK_CHIIKIBETSU_KYOKADao>();
            this.fileLinkGenbamemoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_GENBAMEMO_ENTRYDao>();

            this.uploadLogic = new FileUploadLogic();

            LogUtility.DebugMethodEnd(targetForm);
        }
        #endregion

        #region ボタン初期化処理
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
        /// ボタン初期化処理
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // フォームインスタンスを取得
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;

                // ボタンを初期化
                this.ButtonInit(this.parentbaseform);

                //footボタン処理イベントを初期化
                this.EventInit(this.parentbaseform);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgBox.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        /// <param name="parentform">ベースフォーム</param>
        private void EventInit(BusinessBaseForm parentform)
        {
            // 削除(F4)イベント生成
            parentform.bt_func4.Click += new System.EventHandler(this.form.bt_func4_Click);

            // プレビュー(F5)イベント生成
            parentform.bt_func5.Click += new System.EventHandler(this.form.bt_func5_Click);

            // 条件クリア(F7)イベント生成
            parentform.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);

            // 検索ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // アップロードボタン(F9)イベント生成
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // ダウンロードボタン(F10)イベント生成
            parentform.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            // 閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //Receive message from INXS Subapplication
            if (AppConfig.AppOptions.IsInxsItaku() || AppConfig.AppOptions.IsInxsKyokasho())
            {
                parentform.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(ParentForm_OnReceiveMessageEvent);
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        internal void Delete()
        {
            try
            {
                // ファイルがアップロードされているかチェックする。
                List<long> checkList = this.form.customDataGridView1.Rows
                                                                    .Cast<DataGridViewRow>()
                                                                    .Where(n => n.Cells[ConstCls.CELL_CHECKBOX].Value != null
                                                                             && n.Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True")
                                                                             && n.Cells["upJisshiKbn"].Value != null
                                                                             && n.Cells["upJisshiKbn"].Value.ToString().Equals("済")
                                                                             && n.Cells["hidden_fileId"].Value != null)
                                                                    .Select(n => long.Parse(n.Cells["hidden_fileId"].Value.ToString()))
                                                                    .ToList();

                if (checkList.Count == 0)
                {
                    this.msgBox.MessageBoxShowError("チェックされている明細がありません。\r\n削除する明細にチェックを付けてください。");
                    return;
                }

                if (this.msgBox.MessageBoxShowConfirm("チェックされた明細を削除します。よろしいですか？", MessageBoxDefaultButton.Button1)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    // ファイルアップロード用DB接続を確立
                    if (!this.uploadLogic.CanConnectDB())
                    {
                        this.msgBox.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");

                        // 処理を行わない
                        return;
                    }

                    //INXS check for delete INXS data start
                    bool isUploadToInxs = false;
                    if (AppConfig.AppOptions.IsInxsItaku() || AppConfig.AppOptions.IsInxsKyokasho())
                    {
                        switch (this.form.windowId)
                        {
                            case WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI:
                            case WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI:
                                isUploadToInxs = this.CheckIsUploadContractToInxs(checkList);
                                break;
                            case WINDOW_ID.M_CHIIKIBETSU_KYOKA:
                                isUploadToInxs = this.CheckIsUploadLicenseToInxs(checkList);
                                break;
                        }
                        if (isUploadToInxs && this.msgBox.MessageBoxShow("C119") == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //INXS end

                        // トランザクション開始
                        using (var tran = new Transaction())
                    {
                        // ファイルIDをもとに、ファイルサーバーのファイルデータを削除する。
                        this.uploadLogic.DeleteFileData(checkList);

                        // 連携テーブル削除
                        if (this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI || this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI)
                        {
                            // 委託契約書
                            DeleteFileLinkItakuKeiyakuKihon(checkList);
                        }
                        else if (this.form.windowId == WINDOW_ID.M_CHIIKIBETSU_KYOKA)
                        {
                            // 地域別許可番号
                            DeleteFileLinkChikibetsuKyoka(checkList);
                        }
                        else if (this.form.windowId == WINDOW_ID.M_SYS_INFO)
                        {
                            // システム設定入力
                            DeleteFileLinkSysInfo(checkList);

                        }

                        // トランザクション終了
                        tran.Commit();
                    }

                    // 削除したファイルIDを除外
                    this.form.fileIdList = this.form.fileIdList.Except(checkList).ToList();

                    //INXS delete INXS data start
                    if (isUploadToInxs)
                    {
                        switch (this.form.windowId)
                        {
                            case WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI:
                            case WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI:
                                DeleteContractInInsx(checkList);
                                break;
                            case WINDOW_ID.M_CHIIKIBETSU_KYOKA:
                                DeleteLicenseInInsx(checkList);
                                break;
                        }
                    }
                    //INXS end

                    // リロード
                    this.SearchLogic(false);

                    this.msgBox.MessageBoxShow("I001", "削除");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Delete", ex1);
                this.msgBox.MessageBoxShow("E093");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Delete", ex2);
                this.msgBox.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F5 プレビュー
        /// </summary>
        internal void Preview()
        {
            try
            {
                if (this.form.customDataGridView1.RowCount == 0 || this.form.customDataGridView1.CurrentRow == null)
                {
                    this.msgBox.MessageBoxShowError("プレビュー対象の行を選択してください。");
                    return;
                }

                var cellUpJisshiKbn = this.form.customDataGridView1.CurrentRow.Cells["upJisshiKbn"].Value;
                var cellHidden_fileId = this.form.customDataGridView1.CurrentRow.Cells["hidden_fileId"].Value;
                if (cellUpJisshiKbn == null || cellHidden_fileId == null || !cellUpJisshiKbn.ToString().Equals("済"))
                {
                    var filePath = this.form.customDataGridView1.CurrentRow.Cells["filePath"].Value.ToString();
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        return;
                    }

                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    var fileId = long.Parse(cellHidden_fileId.ToString());
                    this.uploadLogic.Preview(fileId);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgBox.MessageBoxShow("E093");
                }
                else
                {
                    this.msgBox.MessageBoxShow("E245");
                }
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        internal void Clear()
        {
            // 検索条件の初期化
            this.form.txt_FileName.Clear();
            this.form.cntxt_HyoujiKbn.Clear();
            if (this.form.fileIdList == null || this.form.fileIdList.Count == 0)
            {
                this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_LOCAL;
            }
            else
            {
                this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_UP;
            }
        }

        /// <summary>
        /// F9 アップロード
        /// </summary>
        internal void Upload()
        {
            try
            {
                // 選択チェックボックスの確認
                bool checkBoxFlg = false;
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                        && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True"))
                    {
                        checkBoxFlg = true;
                    }
                }

                if (!checkBoxFlg)
                {
                    this.msgBox.MessageBoxShowError("ファイルアップロードを行うファイルが未選択です。");
                    return;
                }

                // システム設定の項目なので序盤にチェックする。
                // システム設定から最大登録容量を取得する。
                M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();
                Int16 maxInsertCapacity = sysInfo.MAX_INSERT_CAPACITY.Value;
                if (maxInsertCapacity == 0)
                {
                    this.msgBox.MessageBoxShowError("最大登録容量が未設定です。システム管理者へ連絡をしてください。");
                    return;
                }

                // アップロード対象のファイル存在チェック
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                        && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True"))
                    {
                        if (!File.Exists(this.form.customDataGridView1.Rows[i].Cells["filePath"].Value.ToString()))
                        {
                            this.msgBox.MessageBoxShowError("アップロード対象のファイルが見つかりませんでした。フォルダ内にファイルが存在するか確認をしてください。");
                            return;
                        }
                    }
                }

                // ファイルアップロード状況チェック
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                        && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True")
                        && this.form.customDataGridView1.Rows[i].Cells["upJisshiKbn"].Value != null)
                    {
                        if (this.msgBox.MessageBoxShowConfirm("アップロード済みのファイルが存在します。ファイルの上書きをしますか？", MessageBoxDefaultButton.Button1)
                            == System.Windows.Forms.DialogResult.Yes)
                        {
                            break;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                // 入力チェック
                // ①拡張子チェック
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                        && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True"))
                    {
                        // 拡張子を取得する。
                        string fileExtension = Path.GetExtension(this.form.customDataGridView1.Rows[i].Cells["filePath"].Value.ToString());

                        // 拡張子をチェックする。
                        if (!ConstCls.EXTENSION_KYOKA_LIST.Contains(fileExtension.ToLower()))
                        {
                            this.msgBox.MessageBoxShowError("アップロード可能なファイルの拡張子は、pdf/png/bmp/jpg/jpeg/gif　のみです。");
                            return;
                        }
                    }
                }

                // ファイルサイズチェック
                // 最大ファイルサイズ メガバイト(MB)⇒バイト(B)に変換する。
                var maxFileSizeByte = sysInfo.MAX_FILE_SIZE.Value * 1024L * 1024L;
                // アップロード予定のファイルサイズをサマリする。
                double uploadYoteiTotalFileSize = 0;
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                        && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True"))
                    {
                        FileInfo upFile = new FileInfo(this.form.customDataGridView1.Rows[i].Cells["filePath"].Value.ToString());
                        if (maxFileSizeByte < upFile.Length)
                        {
                            this.msgBox.MessageBoxShowError(string.Format("ファイルサイズ（MB）が、ファイルアップロードを行えるサイズを超えているためファイルアップロードできませんでした。\r\nファイルサイズを{0}（MB）以下にしてください", sysInfo.MAX_FILE_SIZE.Value));
                            return;
                        }
                        uploadYoteiTotalFileSize += upFile.Length;
                    }
                }

                // ②ファイルサーバ容量チェック
                // アップロード済のファイルサイズを取得。
                double uploadZumiTotalFileSize = this.fileDataDao.GetSumFileLength().Value;
                // 最大登録容量の80%を算出する。
                // ギガバイト(GB)⇒バイト(B)に変換する。
                double byteMaxInsertCapacity = (double)maxInsertCapacity * 1024L * 1024L * 1024L; 

                // 最大登録容量 < (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計)　の場合
                if (byteMaxInsertCapacity < (uploadZumiTotalFileSize + uploadYoteiTotalFileSize))
                {
                    this.msgBox.MessageBoxShowError("ご契約プランのサーバ容量を超えるためファイルアップロードが行えません。\n不要なファイルを削除してください。");
                    return;
                }
                // 最大登録容量の割合を抽出
                // (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計) / システム設定の最大登録容量
                double limitCheck = (uploadZumiTotalFileSize + uploadYoteiTotalFileSize) / byteMaxInsertCapacity;
                // 最大登録容量の80% < (アップロード済ファイルサイズの合計 + アップロード予定ファイルサイズの合計) ≦ 最大登録容量　の場合
                if (ConstCls.CAPACITY_WARNING_RANGE < limitCheck
                    && (uploadZumiTotalFileSize + uploadYoteiTotalFileSize) <= byteMaxInsertCapacity)
                {
                    this.msgBox.MessageBoxShowWarn("ファイルサーバの容量が少なくなっています。\n不要なファイルを削除してください。");
                    // 処理継続
                }


                // アップロード処理を行う。

                // Entityを作成する。
                List<T_FILE_DATA> fileDataList = this.CreateEntity();

                // 登録処理
                using (Transaction tran = new Transaction())
                {
                    List<long> updateFileId = new List<long>();
                    foreach (T_FILE_DATA data in fileDataList)
                    {
                        // 登録済のデータがあれば更新、新規の場合は登録を行う。
                        T_FILE_DATA fileData = this.fileDataDao.GetDataByKey(long.Parse(data.FILE_ID.ToString()));
                        if (fileData != null)
                        {
                            updateFileId.Add(fileData.FILE_ID.Value);

                            fileData.BINARY_DATA = data.BINARY_DATA;
                            fileData.IS_READ_ONLY = data.IS_READ_ONLY;
                            fileData.FILE_LENGTH = data.FILE_LENGTH;
                            fileData.FILE_CREATION_TIME = data.FILE_CREATION_TIME;
                            fileData.FILE_LAST_WRITE_TIME = data.FILE_LAST_WRITE_TIME;
                            fileData.UPDATE_USER = data.UPDATE_USER;
                            fileData.UPDATE_DATE = data.UPDATE_DATE;
                            fileData.UPDATE_PC = data.UPDATE_PC;

                            this.fileDataDao.Update(fileData);
                        }
                        else
                        {
                            this.fileDataDao.Insert(data);
                        }
                    }

                    // UpdateしたファイルIDを除外
                    var idList = this.form.fileIdList.Except(updateFileId);

                    // 委託契約書画面から遷移した場合
                    if (this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI || this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI)
                    {
                        // 連携テーブル更新
                        var systemId = this.form.paramList[0].ToString();
                        var entityList = new List<M_FILE_LINK_ITAKU_KEIYAKU_KIHON>();
                        foreach (var id in idList)
                        {
                            var entity = new M_FILE_LINK_ITAKU_KEIYAKU_KIHON();
                            entity.SYSTEM_ID = systemId;
                            entity.FILE_ID = id;

                            entityList.Add(entity);
                        }

                        entityList.ForEach(n => this.fileLinkItakukeiyakuKihonDao.Insert(n));

                        // 最新のファイルIDリストに更新
                        var dtoList = this.fileLinkItakukeiyakuKihonDao.GetDataBySystemId(systemId);
                        if (dtoList != null)
                        {
                            this.form.fileIdList = dtoList.Select(n => n.FILE_ID.Value).ToList();
                        }
                    }
                    // 地域別許可番号画面から遷移した場合
                    else if (this.form.windowId == WINDOW_ID.M_CHIIKIBETSU_KYOKA)
                    {
                        // 連携テーブル更新
                        var kyokaKbn = Convert.ToInt16(this.form.paramList[0]);
                        var gyoushaCd = this.form.paramList[1];
                        var genbaCd = this.form.paramList[2];
                        var chiikiCd = this.form.paramList[3];
                        var entityList = new List<M_FILE_LINK_CHIIKIBETSU_KYOKA>();
                        foreach (var id in idList)
                        {
                            var entity = new M_FILE_LINK_CHIIKIBETSU_KYOKA();
                            entity.KYOKA_KBN = kyokaKbn;
                            entity.GYOUSHA_CD = gyoushaCd;
                            entity.GENBA_CD = genbaCd;
                            entity.CHIIKI_CD = chiikiCd;
                            entity.FILE_ID = id;

                            entityList.Add(entity);
                        }

                        entityList.ForEach(n => this.fileLinkchiikibetsuKyokaDao.Insert(n));

                        // 最新のファイルIDリストに更新
                        var dtoList = this.fileLinkchiikibetsuKyokaDao.GetDataByCd(kyokaKbn, gyoushaCd, genbaCd, chiikiCd);
                        if (dtoList != null)
                        {
                            this.form.fileIdList = dtoList.Select(n => n.FILE_ID.Value).ToList();
                        }
                    }
                    // 現場メモ画面から遷移した場合
                    else if (this.form.windowId == WINDOW_ID.T_GENBA_MEMO_NYUURYOKU)
                    {
                        var systemId = this.form.paramList[0].ToString();
                        var entityList = new List<M_FILE_LINK_GENBAMEMO_ENTRY>();
                        foreach (var id in idList)
                        {
                            var entity = new M_FILE_LINK_GENBAMEMO_ENTRY();
                            entity.SYSTEM_ID = SqlInt64.Parse(systemId);
                            entity.FILE_ID = id;

                            entityList.Add(entity);
                        }

                        entityList.ForEach(n => this.fileLinkGenbamemoDao.Insert(n));

                        // 最新のファイルIDリストに更新
                        var dtoList = this.fileLinkGenbamemoDao.GetDataBySystemId(systemId);
                        if (dtoList != null)
                        {
                            this.form.fileIdList = dtoList.Select(n => n.FILE_ID.Value).ToList();
                        }
                    }

                    tran.Commit();
                }

                // リロード
                this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_UP;

                if (!string.IsNullOrEmpty(this.form.txt_FileName.Text))
                {
                    this.form.txt_FileName.Clear();
                }

                this.SearchLogic(false);

                this.msgBox.MessageBoxShowInformation("アップロードしました。");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Upload", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgBox.MessageBoxShow("E093");
                }
                else
                {
                    this.msgBox.MessageBoxShow("E245");
                }
            }
        }

        /// <summary>
        /// F10 ダウンロード
        /// </summary>
        internal void Download()
        {
            try
            {
                // チェック処理
                if (string.IsNullOrEmpty(this.initFolder))
                {
                    this.msgBox.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
                    return;
                }

                List<long> checkList = this.form.customDataGridView1.Rows
                                                                    .Cast<DataGridViewRow>()
                                                                    .Where(n => n.Cells[ConstCls.CELL_CHECKBOX].Value != null
                                                                             && n.Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True")
                                                                             && n.Cells["upJisshiKbn"].Value != null
                                                                             && n.Cells["upJisshiKbn"].Value.ToString().Equals("済")
                                                                             && n.Cells["hidden_fileId"].Value != null)
                                                                    .Select(n => long.Parse(n.Cells["hidden_fileId"].Value.ToString()))
                                                                    .ToList();

                if (checkList.Count == 0)
                {
                    this.msgBox.MessageBoxShowError("チェックされている明細がありません。\r\nダウンロードを行う明細にチェックを付けてください。");
                    return;
                }

                var result = this.msgBox.MessageBoxShowConfirm("チェックされた明細をダウンロードします。よろしいですか？");
                if (DialogResult.Yes != result)
                {
                    return;
                }

                if(this.uploadLogic.DownLoad(checkList, this.initFolder))
                {
                    this.msgBox.MessageBoxShowInformation("ダウンロード完了しました。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Download", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgBox.MessageBoxShow("E093");
                }
                else
                {
                    this.msgBox.MessageBoxShow("E245");
                }
            }
        }

        /// <summary>
        /// ファイルアップロード用のEntiryを作成する。
        /// </summary>
        /// <returns></returns>
        private List<T_FILE_DATA> CreateEntity()
        {
            List<T_FILE_DATA> fileDataList = new List<T_FILE_DATA>();

            List<long> idList = new List<long>();
            int checkboxOnCnt = 0;
            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                if (this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value != null
                    && this.form.customDataGridView1.Rows[i].Cells[ConstCls.CELL_CHECKBOX].Value.ToString().Equals("True"))
                {
                    // Entity作成
                    T_FILE_DATA data = new T_FILE_DATA();

                    if (this.form.fileIdList != null && 0 < this.form.fileIdList.Count)
                    {
                        // ファイルIDをもとにSEQの最大値を取得する。（初回のみ）
                        if (checkboxOnCnt == 0)
                        {
                            checkboxOnCnt++;
                        }

                        if (this.form.customDataGridView1.Rows[i].Cells["upJisshiKbn"].Value != null)
                        {
                            // 「済」の場合、ファイルIDとSEQは一覧上で保持している値を設定する。
                            data.FILE_ID = Convert.ToInt64(this.form.customDataGridView1.Rows[i].Cells["hidden_fileId"].Value.ToString());
                        }
                        else
                        {
                            // FILE_ID（画面が保持している値を設定する）
                            data.FILE_ID = this.uploadLogic.CreateNumber();
                        }
                        idList.Add(data.FILE_ID.Value);
                    }
                    else
                    {
                        // ファイルIDの最大値を取得する。（初回のみ）
                        if (checkboxOnCnt == 0)
                        {
                            checkboxOnCnt++;
                        }

                        // FILE_ID
                        data.FILE_ID = this.uploadLogic.CreateNumber();
                        idList.Add(data.FILE_ID.Value);
                    }

                    string filePath = this.form.customDataGridView1.Rows[i].Cells["filePath"].Value.ToString();

                    // バイナリデータ
                    byte[] binaryData = this.uploadLogic.FileConvertByte(filePath);
                    data.BINARY_DATA = binaryData;
                    // ファイルパス
                    data.FILE_PATH = filePath;
                    // ファイルの拡張子
                    data.FILE_EXTENTION = Path.GetExtension(filePath);
                    // ファイルのサイズ(バイト)
                    data.FILE_LENGTH = new FileInfo(filePath).Length;
                    // ファイルの作成日付
                    data.FILE_CREATION_TIME = new SqlDateTime(File.GetCreationTime(filePath));
                    // ファイルの最終更新日付
                    data.FILE_LAST_WRITE_TIME = new SqlDateTime(File.GetLastWriteTime(filePath));
                    // ファイルの読み取り専用
                    if (new FileInfo(filePath).IsReadOnly)
                    {
                        data.IS_READ_ONLY = SqlBoolean.True;
                    }
                    else
                    {
                        data.IS_READ_ONLY = SqlBoolean.False;
                    }
                    // 登録元の画面名
                    data.WINDOW_NAME = this.form.windowId.ToTitleString();

                    // 更新者情報設定
                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_FILE_DATA>(data);
                    dataBinderLogic.SetSystemProperty(data, false);

                    // リストに追加
                    fileDataList.Add(data);
                }
            }

            // 画面上でファイルIDを保持する。
            this.form.fileIdList = idList;

            return fileDataList;
        }

        /// <summary>
        /// ファイル連携データ削除
        /// </summary>
        /// <param name="fileIdList">ファイルIDリスト</param>
        private void DeleteFileLinkItakuKeiyakuKihon(List<long> fileIdList)
        {
            var str = ConvertWhereStr(fileIdList);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            string sql = string.Format("DELETE FROM M_FILE_LINK_ITAKU_KEIYAKU_KIHON WHERE FILE_ID IN {0}", str);
            this.fileLinkItakukeiyakuKihonDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// ファイル連携データ削除
        /// </summary>
        /// <param name="fileIdList">ファイルIDリスト</param>
        private void DeleteFileLinkChikibetsuKyoka(List<long> fileIdList)
        {
            var str = ConvertWhereStr(fileIdList);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            string sql = string.Format("DELETE FROM M_FILE_LINK_CHIIKIBETSU_KYOKA WHERE FILE_ID IN {0}", str);
            this.fileLinkchiikibetsuKyokaDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// ファイル連携データ削除
        /// </summary>
        /// <param name="fileIdList">ファイルIDリスト</param>
        private void DeleteFileLinkSysInfo(List<long> fileIdList)
        {
            var str = ConvertWhereStr(fileIdList);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            string sql = string.Format("DELETE FROM M_FILE_LINK_SYS_INFO WHERE FILE_ID IN {0}", str);
            this.fileLinkSysInfoDao.GetDateForStringSql(sql);
        }

        /// <summary>
        /// WHERE IN句向けにリストをカンマ区切りの文字列に編集
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string ConvertWhereStr(List<long> list)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var fileId in list)
            {
                sb.Append(fileId).Append(",");
            }

            return string.Format("({0})", sb.ToString().TrimEnd(','));
        }

        /// <summary>
        /// ファイルIDをもとにファイルDBを検索し、呼び出し元の委託契約書のファイル情報の存在有無により、表示区分を制御する。
        /// </summary>
        /// <param name="fileIdList">ファイルID</param>
        /// <returns>true:一覧検索対象 false:一覧検索対象外</returns>
        internal bool SetHyoujiKbn(List<long> fileIdList)
        {
            // 条件によって一覧検索の可否を設定
            var result = true;

            if (fileIdList == null || fileIdList.Count == 0)
            {
                this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_LOCAL;

                // 委託契約書画面から遷移した場合
                if (this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI || this.form.windowId == WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI)
                {
                    if (this.form.paramList[1] != null && !string.IsNullOrEmpty(this.form.paramList[1].ToString()))
                    {
                        // ファイル名設定
                        this.form.txt_FileName.Text = Path.GetFileName(this.form.paramList[1].ToString());
                    }
                    else
                    {
                        // 委託契約書からの呼出し、参照ファイル無しでローカル表示時は検索しない
                        result = false;
                    }
                }
            }
            else
            {
                // ファイルDBを検索する。
                this.fileDataList = this.fileDataDao.GetLightDataByKeyList(fileIdList);
                if (this.fileDataList.Any())
                {
                    // ファイル情報が存在する場合、「1.アップ済ファイル」を選択する。
                    this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_UP;
                }
                else
                {
                    // ファイル情報が存在しない場合、「2.ローカルファイル」を選択する。
                    this.form.cntxt_HyoujiKbn.Text = ConstCls.HYOUJI_KBN_LOCAL;
                }
            }

            return result;
        }

        /// <summary>
        /// システム個別設定入力の初期フォルダ配下のファイルを全て取得する。（フルパス）
        /// </summary>
        public List<string> GetLocalFiles()
        {
            List<string> fileList = null;

            // ユーザ定義情報を取得
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            // ファイルアップロード参照先のフォルダを取得
            this.initFolder = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");
            if (string.IsNullOrEmpty(this.initFolder))
            {
                this.msgBox.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
            }
            else
            {
                // フォルダ内のファイルを全て取得する。
                var files = Directory.GetFiles(this.initFolder, "*", System.IO.SearchOption.AllDirectories);
                if (files != null && 0 < files.Length)
                {
                    fileList = files.ToList();
                }
            }

            return fileList;
        }

        /// <summary>
        /// 一覧にデータを設定する。
        /// </summary>
        public void SetIchiran()
        {
            // 一覧をクリアする。
            this.form.customDataGridView1.Rows.Clear();

            // ローカルファイルを取得する。
            List<string> localFiles = this.GetLocalFiles();

            // 表示区分により処理を切り分ける。
            // 「1.アップ済ファイル」の場合
            if (this.form.cntxt_HyoujiKbn.Text.Equals(ConstCls.HYOUJI_KBN_UP))
            {
                if (this.form.fileIdList != null && 0 < this.form.fileIdList.Count)
                {
                    // ファイルIDをもとに、DBからアップロード済のファイルパスを取得する。
                    List<T_FILE_DATA> fileDataList = this.fileDataDao.GetLightDataByKeyList(this.form.fileIdList);
                    if (fileDataList != null)
                    {
                        foreach (var fileData in fileDataList)
                        {
                            // 検索条件の「ファイル名」判定
                            if (this.checkFileName(fileData.FILE_PATH))
                            {
                                this.form.customDataGridView1.Rows.Add();
                                this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["upJisshiKbn"].Value = "済";
                                this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["filePath"].Value = fileData.FILE_PATH;
                                this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["hidden_fileId"].Value = fileData.FILE_ID;
                            }
                        }
                    }
                }
            }
            // 「2.ローカルファイル」の場合
            else if (this.form.cntxt_HyoujiKbn.Text.Equals(ConstCls.HYOUJI_KBN_LOCAL))
            {
                if (localFiles != null)
                {
                    if (this.form.fileIdList != null && 0 < this.form.fileIdList.Count)
                    {
                        foreach (var localFile in localFiles)
                        {
                            // 検索条件の「ファイル名」判定
                            if (checkFileName(localFile))
                            {
                                this.form.customDataGridView1.Rows.Add();
                                this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["filePath"].Value = localFile;

                                // ファイルIDをもとに、DBからアップロード済のファイルパスを取得する。
                                List<T_FILE_DATA> fileDataList = this.fileDataDao.GetLightDataByKeyList(this.form.fileIdList);

                                // DBのファイルパスとローカルのファイルパスが一致していたら、「済」を表示する。
                                if (fileDataList != null)
                                {
                                    foreach (var fileData in fileDataList)
                                    {
                                        if (localFile.Equals(fileData.FILE_PATH))
                                        {
                                            this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["upJisshiKbn"].Value = "済";
                                            this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["hidden_fileId"].Value = fileData.FILE_ID;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // ファイルIDが設定されていない場合、ローカルファイルのみ一覧に表示する。
                        foreach (var localFile in localFiles)
                        {
                            // 検索条件の「ファイル名」判定
                            if (checkFileName(localFile))
                            {
                                this.form.customDataGridView1.Rows.Add();
                                this.form.customDataGridView1.Rows[this.form.customDataGridView1.Rows.Count - 1].Cells["filePath"].Value = localFile;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 検索条件のファイル名に一致するか確認する。
        /// </summary>
        /// <returns>false:部分一致しない、true:部分一致する</returns>
        private bool checkFileName(string filePath)
        {
            // ファイルパスからファイル名を取得する。
            string fileName = Path.GetFileName(filePath);

            // 検索条件の「ファイル名」が指定なしの場合、trueを返す。
            if (string.IsNullOrEmpty(this.form.txt_FileName.Text))
            {
                return true;
            }
            // 取得したファイル名に検索条件の「ファイル名」が含まれている場合、trueを返す。
            if (fileName.Contains(this.form.txt_FileName.Text))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region データ検索処理
        /// <summary>
        /// 検索ボタンクリック後ロジック処理
        /// </summary>
        /// <param name="dispErrMessage"></param>
        public virtual int SearchLogic(bool dispErrMessage = true)
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.SetIchiran();

                if (dispErrMessage && this.form.customDataGridView1.Rows.Count == 0)
                {
                    this.msgBox.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchLogic", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgBox.MessageBoxShow("E093");
                }
                else
                {
                    this.msgBox.MessageBoxShow("E245");
                }
            }
            LogUtility.DebugMethodEnd();

            //行数の戻る
            return 0;

        }
        #endregion

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();
            if (!this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
            {
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                newColumn.Name = ConstCls.CELL_CHECKBOX;
                newColumn.HeaderText = "";
                newColumn.Width = 25;
                DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                newheader.Value = "";
                newColumn.HeaderCell = newheader;
                newColumn.Resizable = DataGridViewTriState.False;

                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
            }

            LogUtility.DebugMethodEnd();

        }

        #region 未使用
        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region INXS処理

        /// <summary>
        /// Check is upload contract to INXS
        /// </summary>
        /// <returns>true is uploaded</returns>
        public bool CheckIsUploadContractToInxs(List<long> fileIds)
        {
            bool result = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM T_CONTRACT_FILE_UPLOAD_INXS ");
                sql.Append(" WHERE FILE_ID IN (");
                sql.Append(string.Join(",", fileIds));
                sql.Append(")");

                DataTable dt = this.fileLinkItakukeiyakuKihonDao.GetDateForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckIsUploadLicenseToInxs", ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check is upload licanse to INXS
        /// </summary>
        /// <returns>true is uploaded</returns>
        public bool CheckIsUploadLicenseToInxs(List<long> fileIds)
        {
            bool result = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM M_LICENSE_UPLOAD_STATUS_INXS ");
                sql.Append(" WHERE FILE_ID IN (");
                sql.Append(string.Join(",", fileIds));
                sql.Append(")");

                DataTable dt = this.fileLinkchiikibetsuKyokaDao.GetDateForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckIsUploadLicenseToInxs", ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// delete inxs contract
        /// </summary>
        /// <param name="fileIds"></param>
        private void DeleteContractInInsx(List<long> fileIds)
        {
            var systemId = this.form.paramList[0].ToString();
            CommonKeyFileIdsDto<string> commandArgs = new CommonKeyFileIdsDto<string>()
            {
                Id = systemId,
                FileIds = fileIds
            };

            var requestDto = new
            {
                CommandName = 8, //DeleteInxsContractByFileIds
                ShougunParentWindowName = this.parentbaseform.Text,
                CommandArgs = commandArgs
            };

            ExecuteSubAppCommand(requestDto);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// delete inxs license
        /// </summary>
        /// <param name="fileIds"></param>
        private void DeleteLicenseInInsx(List<long> fileIds)
        {
            var requestDto = new
            {
                CommandName = 12, //DeleteInxsLicensetByFileIds
                ShougunParentWindowName = this.parentbaseform.Text,
                CommandArgs = fileIds
            };

            ExecuteSubAppCommand(requestDto);
            Thread.Sleep(1000);
        }

        private void ExecuteSubAppCommand(object requestDto)
        {
            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = this.form.transactionId,
                ReferenceID = -1
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = Shougun.Core.ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand(Constans.StartFormText, execCommandDto);

        }

        private void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                    if (arg != null)
                    {
                        var msgDto = (CommunicateAppDto)arg;
                        var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                        if (token != null)
                        {
                            var tokenDto = (CommunicateTokenDto)token;
                            if (tokenDto.TransactionId == this.form.transactionId)
                            {
                                if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                                {
                                    var responeDto = JsonUtility.DeserializeObject<InxsExecuteResponseDto>(msgDto.Args[0].ToString());
                                    if (responeDto != null && responeDto.MessageType == Common.BusinessCommon.Enums.EnumMessageType.ERROR)
                                    {
                                        this.msgBox.MessageBoxShowError(responeDto.ResponseMessage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion
    }
}
