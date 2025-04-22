using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.FileUploadIchiran.APP;
using Shougun.Core.ExternalConnection.FileUploadIchiran.DAO;
using Shougun.Core.ExternalConnection.FileUploadIchiran.DTO;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using Shougun.Core.FileUpload.FileUploadCommon.Utility;

namespace Shougun.Core.ExternalConnection.FileUploadIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>
        /// 明細部のカラム名
        /// </summary>
        /// <summary></summary>
        private static readonly string TAISHO = "TAISHO";
        /// <summary></summary>
        private static readonly string DATA_TAISHO = "DATA_TAISHO";
        /// <summary>一覧のカラム名</summary>
        /// <remarks>一覧に項目追加した場合、ここにも追加すること</remarks>
        private static readonly List<string> ICHIRAN_COLUMN = new List<string>() { "CREATE_DATE", "FILE_PATH", "FILE_LENGTH", "WINDOW_NAME", "CREATE_USER", "FILE_ID" };
        #endregion

        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.FileUploadIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        private FileUploadLogic uploadLogic;

        /// <summary>
        /// ファイルアップロード一覧DAO
        /// </summary>
        private DAOClass dao;

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
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = FileConnectionUtility.GetComponent<DAOClass>();
            this.fileDataDao = FileConnectionUtility.GetComponent<FILE_DATADAO>();
            this.fileLinkItakukeiyakuKihonDao = DaoInitUtility.GetComponent<IM_FILE_LINK_ITAKU_KEIYAKU_KIHONDao>();
            this.fileLinkchiikibetsuKyokaDao = DaoInitUtility.GetComponent<IM_FILE_LINK_CHIIKIBETSU_KYOKADao>();

            this.uploadLogic = new FileUploadLogic();
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //フォーム初期化
                var parentForm = (BusinessBaseForm)this.form.Parent;
                var headerForm = (UIHeader)parentForm.headerForm;

                // 明細ヘッダーのチェックボックス設定
                this.HeaderCheckBoxSupport();

                // ヘッダーの初期化
                headerForm.totalSize.Text = "0";
                headerForm.readDataNumber.Text = "0";

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                this.form.Ichiran.AutoGenerateColumns = false;
                this.form.Ichiran.DataSource = CreateEmptyDataTable();

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart(parentForm);

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

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
        /// <param name="parentForm">ベースフォーム</param>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                // 削除ボタン(F4)イベント生成
                parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

                // プレビューボタン(F5)イベント生成
                parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);

                // CSVボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

                // 条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

                // ダウンロードボタン(F9)イベント生成
                parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

                // 並び替えボタン(F10)イベント生成
                parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

                // フィルタボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

                this.form.CREATE_DATE_TO.MouseDoubleClick += new MouseEventHandler(this.form.CREATE_DATE_TO_MouseDoubleClick);
                this.form.Ichiran.CellFormatting += new DataGridViewCellFormattingEventHandler(this.form.Ichiran_CellFormatting);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 空のデータテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateEmptyDataTable()
        {
            // 並び替え、フィルタの初期表示対策
            DataTable dt = new DataTable();

            ICHIRAN_COLUMN.ForEach(n => dt.Columns.Add(n));

            return dt;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <returns></returns>
        internal bool Delete()
        {
            try
            {
                var selectList = this.form.Ichiran.Rows
                                                  .Cast<DataGridViewRow>()
                                                  .Where(n => n.Cells[DATA_TAISHO].Value != null
                                                           && n.Cells[DATA_TAISHO].Value.ToString().Equals("True")
                                                           && n.Cells["FILE_ID"].Value != null)
                                                  .Select(n => new
                                                  {
                                                      FILE_ID = long.Parse(n.Cells["FILE_ID"].Value.ToString()),
                                                      WINDOW_NAME = n.Cells["WINDOW_NAME"].Value.ToString()
                                                  }).ToList();
                if (selectList.Count == 0)
                {
                    this.msgLogic.MessageBoxShowError("チェックされている明細がありません。\r\n削除する明細にチェックを付けてください。");
                    return false;
                }

                var result = this.msgLogic.MessageBoxShowConfirm("チェックされた明細を削除します。よろしいですか？");
                if (result != DialogResult.Yes)
                {
                    return true;
                }

                using (var tran = new Transaction())
                {
                    // ファイルIDをもとに、ファイルサーバーのファイルデータを削除する。
                    var fileIdList = selectList.Select(n => n.FILE_ID).ToList();
                    this.uploadLogic.DeleteFileData(fileIdList);

                    // 連携テーブル削除
                    // 委託契約書
                    var itakuList = selectList.Where(n => WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI.ToTitleString().Equals(n.WINDOW_NAME)
                                                       || WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI.ToTitleString().Equals(n.WINDOW_NAME))
                                              .Select(n => n.FILE_ID)
                                              .ToList();
                    if (0 < itakuList.Count)
                    {
                        DeleteFileLinkItakuKeiyakuKihon(itakuList);
                    }

                    // 地域別許可番号
                    var chiikiList = selectList.Where(n => WINDOW_ID.M_CHIIKIBETSU_KYOKA.ToTitleString().Equals(n.WINDOW_NAME))
                                               .Select(n => n.FILE_ID)
                                               .ToList();
                    if (0 < chiikiList.Count)
                    {
                        DeleteFileLinkChikibetsuKyoka(chiikiList);
                    }

                    // トランザクション終了
                    tran.Commit();
                }

                // リロード
                this.Search();

                this.msgLogic.MessageBoxShow("I001", "削除");
            }
            catch (SQLRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Delete", ex1);
                this.msgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Delete", ex2);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
            return true;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <returns></returns>
        internal bool Preview()
        {
            try
            {
                if (this.form.Ichiran.RowCount == 0 || this.form.Ichiran.CurrentRow == null)
                {
                    this.msgLogic.MessageBoxShowError("プレビュー対象の行を選択してください。");
                    return false;
                }

                var cell = this.form.Ichiran.CurrentRow.Cells["FILE_ID"].Value;
                if (cell == null)
                {
                    return false;
                }

                var fileId = long.Parse(cell.ToString());
                this.uploadLogic.Preview(fileId);

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
                return false;
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <returns></returns>
        internal bool Clear()
        {
            this.form.CREATE_DATE_FROM.Text = string.Empty;
            this.form.CREATE_DATE_TO.Text = string.Empty;
            this.form.FILE_NAME.Text = string.Empty;

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            return true;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {

            //DB設定確認
            if ((!this.uploadLogic.CanConnectDB()) || (!new FileUploadLogic().ConnectDB()))
            {
                this.msgLogic.MessageBoxShowWarn("ファイルアップロードの事前準備が未完了です。\r\n接続先データベースを設定してください");
                return 0;
            }

            //日付チェックを追加
            if (this.DateCheck())
            {
                return 0;
            }

            // 検索条件作成
            var dto = CreateSearchDto();

            // 検索
            var list = this.dao.GetIchiranDataSql(dto);
            this.form.Ichiran.DataSource = list;

            // ソート、並び替え対応
            this.form.customSortHeader1.SortDataTable(list);
            this.form.customSearchHeader1.SearchDataTable(list);

            // ヘッダフォーム更新
            RefreshHeaderForm();

            if (list.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShow("C001");
            }

            return 0;
        }

        /// <summary>
        /// ヘッダフォーム更新
        /// </summary>
        internal void RefreshHeaderForm()
        {
            // ヘッダフォーム更新
            var parentForm = (BusinessBaseForm)this.form.Parent;
            var headerForm = (UIHeader)parentForm.headerForm;

            if (this.form.Ichiran != null)
            {
                var totalSize = this.form.Ichiran.Rows.Cast<DataGridViewRow>()
                                                 .Sum(n => decimal.Parse(n.Cells["FILE_LENGTH"].Value.ToString()));
                if (0 < totalSize)
                {
                    totalSize = Math.Round(totalSize / 1024, 3, MidpointRounding.AwayFromZero);

                }
                headerForm.totalSize.Text = totalSize.ToString();
                headerForm.readDataNumber.Text = this.form.Ichiran.Rows.Count.ToString();
            }
            else
            {
                headerForm.totalSize.Text = "0";
                headerForm.readDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// 検索条件用のDTO作成
        /// </summary>
        /// <returns></returns>
        private SearchDTO CreateSearchDto()
        {
            var dto = new SearchDTO();
            dto.FILE_NAME = this.form.FILE_NAME.Text;

            if (!string.IsNullOrEmpty(form.CREATE_DATE_FROM.Text))
            {
                dto.HIDUKE_FROM = this.form.CREATE_DATE_FROM.Value.ToString();
            }
            if (!string.IsNullOrEmpty(this.form.CREATE_DATE_TO.Text))
            {
                dto.HIDUKE_TO = this.form.CREATE_DATE_TO.Value.ToString();
            }

            return dto;
        }

        /// <summary>
        /// ダウンロード
        /// </summary>
        /// <returns></returns>
        internal bool DownLoad()
        {
            try
            {
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ファイルアップロード参照先のフォルダを取得
                var initFolder = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");
                if (string.IsNullOrEmpty(initFolder) || !Directory.Exists(initFolder))
                {
                    this.msgLogic.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
                    return false;
                }

                List<long> checkList = this.form.Ichiran.Rows
                                                        .Cast<DataGridViewRow>()
                                                        .Where(n => n.Cells[DATA_TAISHO].Value != null
                                                                 && n.Cells[DATA_TAISHO].Value.ToString().Equals("True")
                                                                 && n.Cells["FILE_ID"].Value != null)
                                                        .Select(n => long.Parse(n.Cells["FILE_ID"].Value.ToString()))
                                                        .ToList();

                if (checkList.Count == 0)
                {
                    this.msgLogic.MessageBoxShowError("チェックされている明細がありません。\r\nダウンロードを行う明細にチェックを付けてください。");
                    return false;
                }

                var result = this.msgLogic.MessageBoxShowConfirm("チェックされた明細をダウンロードします。よろしいですか？");
                if (DialogResult.Yes != result)
                {
                    return false;
                }

                if (this.uploadLogic.DownLoad(checkList, initFolder))
                {
                    this.msgLogic.MessageBoxShowInformation("ダウンロード完了しました。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DownLoad", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// ファイル連携データ削除
        /// </summary>
        /// <param name="systemId">システムID</param>
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


        #region 明細ヘッダーにチェックボックスを追加
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {
            LogUtility.DebugMethodStart();

            if (!this.form.Ichiran.Columns.Contains(DATA_TAISHO))
            {
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                newColumn.Name = DATA_TAISHO;
                newColumn.HeaderText = "";
                newColumn.DataPropertyName = TAISHO;
                newColumn.Width = 25;
                DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                newheader.Value = "";
                newColumn.HeaderCell = newheader;
                newColumn.Resizable = DataGridViewTriState.False;
                if (0 < this.form.Ichiran.Columns.Count)
                {
                    this.form.Ichiran.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.Ichiran.Columns.Add(newColumn);
                }
                this.form.Ichiran.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細ヘッダーのチェックボックス解除
        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        internal void HeaderCheckBoxFalse()
        {
            if (this.form.Ichiran.Columns.Contains(DATA_TAISHO))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.Ichiran.Columns[DATA_TAISHO].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        private bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.CREATE_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.CREATE_DATE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.CREATE_DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.CREATE_DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.CREATE_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.CREATE_DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.CREATE_DATE_FROM.IsInputErrorOccured = true;
                this.form.CREATE_DATE_TO.IsInputErrorOccured = true;
                this.form.CREATE_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.CREATE_DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "登録日付From", "登録日付To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.CREATE_DATE_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 未使用
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
    }
}
