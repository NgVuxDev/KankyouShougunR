using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClassShoukai
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private ShoukaiJouken form;

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 電子契約WebAPIの接続情報リスト
        /// </summary>
        private List<S_DENSHI_CONNECT> denshiConnectList;

        /// <summary>
        /// 登録済みの電子契約仮Entityリスト
        /// </summary>
        private List<T_DENSHI_KEIYAKU_KARI_ENTRY> denshiKariEntryList;

        /// <summary>
        /// 電子契約APIのDao
        /// </summary>
        private IS_DENSHI_CONNECTDao denshiConnectDao;

        /// <summary>
        /// 電子契約仮EntryDao
        /// </summary>
        internal DenshiKeiyakuKariEntryDAO denshiKeiyakuKariEntryDao;

        /// <summary>
        /// 電子契約仮DetailDao
        /// </summary>
        private DenshiKeiyakuKariDetailDAO denshiKeiyakuKariDetailDao;

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiLogic denshiLogic;

        /// <summary>
        /// システムID
        /// </summary>
        private string systemId;

        /// <summary>
        /// クライアントID
        /// </summary>
        private string clientId;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClassShoukai(ShoukaiJouken targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            try
            {
                this.form = targetForm;
                this.denshiLogic = new DenshiLogic();
                this.denshiKeiyakuKariEntryDao = DaoInitUtility.GetComponent<DenshiKeiyakuKariEntryDAO>();
                this.denshiKeiyakuKariDetailDao = DaoInitUtility.GetComponent<DenshiKeiyakuKariDetailDAO>();
                this.denshiConnectDao = DaoInitUtility.GetComponent<IS_DENSHI_CONNECTDao>();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 初期化

        #region WindowInit

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンの初期化
                this.ButtonInit();

                // Headerの初期化
                this.SetHeaderText();

                // コントロール初期化
                this.ControlInit();

                // イベントの初期化
                this.EventInit();

                // ロジック初期化
                this.msgLogic = new MessageBoxShowLogic();

                S_DENSHI_CONNECT[] denshiConnectData = this.denshiConnectDao.GetAllData();
                this.denshiConnectList = new List<S_DENSHI_CONNECT>(denshiConnectData);

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
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

        #region コントロールの初期化

        /// <summary>
        /// コントロール初期化処理
        /// </summary>
        private void ControlInit()
        {
            try
            {
                // 初期値はDBから読み込む形になるかも

                // 契約状況の初期値を設定する
                this.form.KEIYAKU_JYOUKYOU_CD.Text = ConstClsShoukai.KEIYAKU_JOUKYOU_DEFAULT;
                this.form.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_2;

                // ページの初期値
                this.form.txt_Page.Text = ConstClsShoukai.PAGE_DEFAULT;

                // 全ファイルの初期値
                this.form.cbx_AllFile.Checked = ConstClsShoukai.ALL_FILE_CHECK_DEFAULT;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
        }

        #endregion

        #region ヘッダの初期化

        /// <summary>
        /// Headerの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //ヘッダを設定する
                this.form.lb_title.Text = ConstClsShoukai.FORM_HEADER_TITLE;

                // Formタイトルの設定
                this.form.Text = ConstClsShoukai.FORM_HEADER_TITLE;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.form.bt_func8.Text = ConstClsShoukai.BUTTON_REFERENCE_NAME;
                this.form.bt_func12.Text = ConstClsShoukai.BUTTON_CANCEL_NAME;

                this.form.bt_func8.Enabled = true;
                this.form.bt_func12.Enabled = true;

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            try
            {
                // 最新照会(F8)イベント生成
                this.form.bt_func8.Click += new EventHandler(this.form.Reference);

                // キャンセル(F12)イベント生成
                this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is NotSingleRowUpdatedRuntimeException)) { throw; }
            }
        }

        #endregion

        #endregion

        #region ファンクションイベント


        #region F8 最新照会

        #region 書類情報一覧のループ処理

        /// <summary>
        /// F8 最新照会
        /// </summary>
        /// <returns></returns>
        internal bool DenshiKeiyakuReference()
        {
            LogUtility.DebugMethodStart();

            bool result = false;
            bool bolLoop = true;

            try
            {
                int loopCnt = 0;

                // 登録済みの電子契約仮Entryを抽出しておく
                T_DENSHI_KEIYAKU_KARI_ENTRY[] denshiKariEntryData = this.denshiKeiyakuKariEntryDao.GetAllData();
                this.denshiKariEntryList = new List<T_DENSHI_KEIYAKU_KARI_ENTRY>(denshiKariEntryData);

                // 取得ページ数によってここからループ
                if (this.form.cbx_AllFile.Checked)
                {
                    // 全ファイルの場合は全ページ取得するので
                    // まず1をセット
                    loopCnt = 1;
                }
                else
                {
                    loopCnt = Convert.ToInt32(this.form.txt_Page.Text);
                }

                // bolLoopがtrueの場合ループを続ける
                while (bolLoop)
                {
                    // トークンを取得する。
                    var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_DOCUMENTS));
                    var api = connect.URL;                  // documents
                    var contentType = connect.CONTENT_TYPE; // application/x-www-form-urlencoded

                    REQ_DOCUMENTS_GET req = new REQ_DOCUMENTS_GET();
                    req.client_id = this.clientId;

                    // URLにパラメータを付与
                    // page
                    api = api + "?page=" + loopCnt;
                    // status
                    api = api + "&status=" + Convert.ToInt32(this.form.KEIYAKU_JYOUKYOU_CD.Text);
                    // with_files
                    api = api + "&with_files=y";
                    // with_participants
                    api = api + "&with_participants=n";

                    DOCUMENT_LIST_MODEL documentDto;
                    result = this.denshiLogic.HttpGET<DOCUMENT_LIST_MODEL>(api, contentType, req, out documentDto);

                    if (!result || documentDto.Documents == null)
                    {
                        // エラーもしくはレスポンスが取得できなくなったら抜ける
                        break;
                    }
                    else
                    {
                        // 書類情報詳細の取得、登録処理
                        this.regist(documentDto);
                    }

                    if (this.form.cbx_AllFile.Checked)
                    {
                        // 全ファイルチェックの場合は次のページへ
                        loopCnt++;
                    }
                    else
                    {
                        // ページ指定の場合は終了
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogUtility.Error("DenshiKeiyakuReference", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            return result;
        }

        #endregion

        #region 書類情報詳細の取得、登録処理

        /// <summary>
        /// 登録処理の中身
        /// </summary>
        /// <param name="documentDto"></param>
        private bool regist(DOCUMENT_LIST_MODEL documentDto)
        {
            // ドキュメントID
            string documentId = string.Empty;

            // Documentsの中身
            foreach (DOCUMENT_MODEL doc in documentDto.Documents)
            {
                T_DENSHI_KEIYAKU_KARI_ENTRY denshiKariEntry = new T_DENSHI_KEIYAKU_KARI_ENTRY();
                List<T_DENSHI_KEIYAKU_KARI_DETAIL> denshiKariDetails = new List<T_DENSHI_KEIYAKU_KARI_DETAIL>();

                // ドキュメントID保存
                documentId = doc.Id;

                denshiKariEntry.PAGE = (Int32)documentDto.Page;
                denshiKariEntry.DOCUMENT_ID = doc.Id;
                denshiKariEntry.SEND_MESSAGE = doc.Message;
                denshiKariEntry.SEND_TITLE = doc.Title;
                denshiKariEntry.SHANAI_BIKO = doc.Note;
                denshiKariEntry.KEIYAKU_JYOUKYOU = (Int32)doc.Status;
                denshiKariEntry.KEIYAKUSHO_CREATE_DATE = DateTime.Parse(doc.Created_At);
                denshiKariEntry.KEIYAKUSHO_UPDATE_DATE = DateTime.Parse(doc.Updated_At);

                foreach (FILE_MODEL file in doc.Files)
                {
                    T_DENSHI_KEIYAKU_KARI_DETAIL denshiKariDetail = new T_DENSHI_KEIYAKU_KARI_DETAIL();
                    denshiKariDetail.DOCUMENT_ID = doc.Id;
                    denshiKariDetail.FILE_ID = file.Id;
                    denshiKariDetail.FILE_NAME = file.Name;
                    denshiKariDetails.Add(denshiKariDetail);
                }

                // 書籍情報の取得
                var connect = this.denshiConnectList.Find(n => n.CONTENT_NAME.Equals(DenshiConst.CONTENT_NAME_ATTRIBUTE));
                var api = string.Format(connect.URL, documentId);   // /documents/{0}/attribute
                var contentType = connect.CONTENT_TYPE;             // application/x-www-form-urlencoded

                // reqに渡すのはClientIdのみでOK
                REQ_COMMON cId = new REQ_COMMON();
                cId.client_id = this.clientId;

                ATTRIBUTE_MODEL attributeDto;
                bool result = this.denshiLogic.HttpGET<ATTRIBUTE_MODEL>(api, contentType, cId, out attributeDto);
                if (!result)
                {
                    // 2018/09/04以前に作成した電子契約データは書類情報が存在しないため、
                    // 必ず404が返ってくると思われるためFalseとなる。
                    // ※2018/09/05に作成したデータは書類情報が返ってきたため。
                }
                else
                { 
                    denshiKariEntry.KEIYAKUSHA = attributeDto.Counterparty;
                    if (attributeDto.Contract_at != null)
                    {
                        denshiKariEntry.KEIYAKUSHO_KEIYAKU_DATE = DateTime.Parse(attributeDto.Contract_at);
                    }
                    if (attributeDto.Validity_start_at != null)
                    {
                        denshiKariEntry.YUUKOU_BEGIN = DateTime.Parse(attributeDto.Validity_start_at);
                    }
                    if (attributeDto.Validity_end_at != null)
                    {
                        denshiKariEntry.YUUKOU_END = DateTime.Parse(attributeDto.Validity_end_at);
                    }

                    // 管理用タイトル
                    denshiKariEntry.KANRI_TITLE = attributeDto.Title;

                    // 自動更新の有無
                    denshiKariEntry.AUTO_UPDATE = attributeDto.Auto_update;

                    // 解約通知期限
                    if (attributeDto.Validity_end_notice_at != null)
                    {
                        denshiKariEntry.KAIYAKU_TSUUCHI = DateTime.Parse(attributeDto.Validity_end_notice_at);
                    }

                    // 管理番号
                    denshiKariEntry.LOCAL_ID = attributeDto.Local_id;

                    // 取引金額
                    if (string.IsNullOrEmpty(attributeDto.Amount))
                    {
                        denshiKariEntry.AMOUNT = 0;
                    }
                    else
                    {
                        denshiKariEntry.AMOUNT = Convert.ToDecimal(attributeDto.Amount);
                    }

                    // クラウドサイン項目
                    foreach (ATTRIBUTE_OPTIONS_MODEL option in attributeDto.Options)
                    {
                        switch (option.Order)
                        {
                            case 1:
                                denshiKariEntry.SHORUI_INFO_NAME1 = option.Content;
                                break;
                            case 2:
                                denshiKariEntry.SHORUI_INFO_NAME2 = option.Content;
                                break;
                            case 3:
                                denshiKariEntry.SHORUI_INFO_NAME3 = option.Content;
                                break;
                            case 4:
                                denshiKariEntry.SHORUI_INFO_NAME4 = option.Content;
                                break;
                            case 5:
                                denshiKariEntry.SHORUI_INFO_NAME5 = option.Content;
                                break;
                            case 6:
                                denshiKariEntry.SHORUI_INFO_NAME6 = option.Content;
                                break;
                            case 7:
                                denshiKariEntry.SHORUI_INFO_NAME7 = option.Content;
                                break;
                            case 8:
                                denshiKariEntry.SHORUI_INFO_NAME8 = option.Content;
                                break;
                            case 9:
                                denshiKariEntry.SHORUI_INFO_NAME9 = option.Content;
                                break;
                            case 10:
                                denshiKariEntry.SHORUI_INFO_NAME10 = option.Content;
                                break;
                            default:
                                break;
                        }
                    }
                }

                try
                {
                    T_DENSHI_KEIYAKU_KARI_ENTRY entry = this.denshiKariEntryList.Find(a => a.DOCUMENT_ID.Equals(documentId));

                    if (entry == null)
                    {
                        // INSERT

                        // SYSTEM_IDの採番
                        this.CreateSystemId();
                        // SEQ
                        int seq = 1;

                        denshiKariEntry.SYSTEM_ID = this.systemId;

                        // 更新者情報設定
                        var kihonDataBinderLogic = new DataBinderLogic<T_DENSHI_KEIYAKU_KARI_ENTRY>(denshiKariEntry);
                        kihonDataBinderLogic.SetSystemProperty(denshiKariEntry, false);

                        using (Transaction tran = new Transaction())
                        {
                            // T_DENSHI_KEIYAKU_KARI_ENTRY登録
                            this.denshiKeiyakuKariEntryDao.Insert(denshiKariEntry);

                            // T_DENSHI_KEIYAKU_KARI_ENTRY登録
                            foreach (T_DENSHI_KEIYAKU_KARI_DETAIL detail in denshiKariDetails)
                            {
                                detail.SYSTEM_ID = this.systemId;
                                detail.SEQ = seq++;
                                this.denshiKeiyakuKariDetailDao.Insert(detail);
                            }

                            tran.Commit();
                        }
                    }
                    else
                    {
                        // UPDATE
                        // 更新の場合は契約状況とクラウドサイン項目のみ更新する
                        // DETAILの更新も無し
                        denshiKariEntry = entry;

                        // 契約状況
                        denshiKariEntry.KEIYAKU_JYOUKYOU = (Int32)doc.Status;
                        //クラウドサイン備考
                        denshiKariEntry.SHANAI_BIKO = doc.Note;

                        // 昔のデータ参照時(クラウドサインでattributeが実装される前に連携したデータ)
                        // 存在せず読み込めないケースがあるので必ずここでチェック
                        if (attributeDto != null)
                        {
                            // 管理用タイトル
                            denshiKariEntry.KANRI_TITLE = attributeDto.Title;

                            // 自動更新の有無
                            denshiKariEntry.AUTO_UPDATE = attributeDto.Auto_update;

                            // 解約通知期限
                            if (attributeDto.Validity_end_notice_at != null)
                            {
                                denshiKariEntry.KAIYAKU_TSUUCHI = DateTime.Parse(attributeDto.Validity_end_notice_at);
                            }

                            // 管理番号
                            denshiKariEntry.LOCAL_ID = attributeDto.Local_id;

                            // 取引金額
                            if (string.IsNullOrEmpty(attributeDto.Amount))
                            {
                                denshiKariEntry.AMOUNT = 0;
                            }
                            else
                            {
                                denshiKariEntry.AMOUNT = Convert.ToDecimal(attributeDto.Amount);
                            }

                            // 契約者
                            denshiKariEntry.KEIYAKUSHA = attributeDto.Counterparty;
                            if (attributeDto.Contract_at != null)
                            {
                                denshiKariEntry.KEIYAKUSHO_KEIYAKU_DATE = DateTime.Parse(attributeDto.Contract_at);
                            }
                            if (attributeDto.Validity_start_at != null)
                            {
                                denshiKariEntry.YUUKOU_BEGIN = DateTime.Parse(attributeDto.Validity_start_at);
                            }
                            if (attributeDto.Validity_end_at != null)
                            {
                                denshiKariEntry.YUUKOU_END = DateTime.Parse(attributeDto.Validity_end_at);
                            }


                            // クラウドサイン項目
                            foreach (ATTRIBUTE_OPTIONS_MODEL option in attributeDto.Options)
                            {
                                switch (option.Order)
                                {
                                    case 1:
                                        denshiKariEntry.SHORUI_INFO_NAME1 = option.Content;
                                        break;
                                    case 2:
                                        denshiKariEntry.SHORUI_INFO_NAME2 = option.Content;
                                        break;
                                    case 3:
                                        denshiKariEntry.SHORUI_INFO_NAME3 = option.Content;
                                        break;
                                    case 4:
                                        denshiKariEntry.SHORUI_INFO_NAME4 = option.Content;
                                        break;
                                    case 5:
                                        denshiKariEntry.SHORUI_INFO_NAME5 = option.Content;
                                        break;
                                    case 6:
                                        denshiKariEntry.SHORUI_INFO_NAME6 = option.Content;
                                        break;
                                    case 7:
                                        denshiKariEntry.SHORUI_INFO_NAME7 = option.Content;
                                        break;
                                    case 8:
                                        denshiKariEntry.SHORUI_INFO_NAME8 = option.Content;
                                        break;
                                    case 9:
                                        denshiKariEntry.SHORUI_INFO_NAME9 = option.Content;
                                        break;
                                    case 10:
                                        denshiKariEntry.SHORUI_INFO_NAME10 = option.Content;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        //denshiKariEntry.TIME_STAMP = entry.TIME_STAMP;

                        // 更新者情報設定
                        var kihonDataBinderLogic = new DataBinderLogic<T_DENSHI_KEIYAKU_KARI_ENTRY>(denshiKariEntry);
                        kihonDataBinderLogic.SetSystemProperty(denshiKariEntry, false);

                        using (Transaction tran = new Transaction())
                        {
                            // T_DENSHI_KEIYAKU_KARI_ENTRY登録
                            this.denshiKeiyakuKariEntryDao.Update(denshiKariEntry);
                            tran.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("regist", ex);
                    if (ex is NotSingleRowUpdatedRuntimeException)
                    {
                        this.msgLogic.MessageBoxShow("E080");
                    }
                    else if (ex is SQLRuntimeException)
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
            return true;
        }

        /// <summary>
        /// システムID採番
        /// </summary>
        private void CreateSystemId()
        {
            // T_DENSHI_KEIYAKU_KARI_ENTRYのシステムIDを採番する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" MAX(SYSTEM_ID) AS SYSTEM_ID ");
            sql.Append(" FROM T_DENSHI_KEIYAKU_KARI_ENTRY ");

            var dt = this.denshiKeiyakuKariEntryDao.getDateForStringSql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string maxSystemID = dr["SYSTEM_ID"].ToString();

                if (!string.IsNullOrEmpty(maxSystemID))
                {
                    this.systemId = String.Format("{0:D9}", Int64.Parse(maxSystemID) + 1);
                }
                else
                {
                    this.systemId = String.Format("{0:D9}", 1);
                }
            }
            else
            {
                this.systemId = String.Format("{0:D9}", 1);
            }
        }

        #endregion

        #endregion

        #endregion

        #region チェック処理

        /// <summary>
        /// クライアントIDが設定されているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckClientId()
        {
            // クライアントIDを取得する。
            this.GetClientID();

            // クライアントIDが取得できない場合、エラーとする。
            if (this.clientId == null || string.IsNullOrEmpty(this.clientId))
            {
                this.msgLogic.MessageBoxShowError("クライアントIDの登録が行われていません。クラウドサインとの連携処理を中断します。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 社員CDからクライアントIDを取得する。
        /// </summary>
        private void GetClientID()
        {
            // 自身の社員CDから、クライアントIDを取得する。
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append(" CI.DENSHI_KEIYAKU_CLIENT_ID ");
            sql.Append(" FROM M_DENSHI_KEIYAKU_CLIENT_ID CI ");
            sql.Append(" WHERE ");
            sql.AppendFormat(" CI.SHAIN_CD = '{0}' ", this.form.SHAIN_CD.Text);
            sql.Append(" AND CI.DELETE_FLG = 0 ");

            var dt = this.denshiKeiyakuKariEntryDao.getDateForStringSql(sql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.clientId = dr["DENSHI_KEIYAKU_CLIENT_ID"].ToString();
            }
        }

        /// <summary>
        /// ページの入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckPage()
        {
            // 全ファイルチェックがオフの場合チェックする
            if (!this.form.cbx_AllFile.Checked)
            {
                // ページが未入力の場合
                if (string.IsNullOrEmpty(this.form.txt_Page.Text))
                {
                    this.msgLogic.MessageBoxShowError("ページを入力してください。");
                    return false;
                }

                // 入力されたページが0の場合
                if (int.Parse(this.form.txt_Page.Text) == 0)
                {
                    this.msgLogic.MessageBoxShowError("１以上の数字を入力してください。");
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region その他イベント

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        internal void CheckListPopup()
        {
            try
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;

                // この画面では1(先方確認中)、2(締結済)のみでよい

                //1
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_1;
                dt.Rows.Add(row);
                //2
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = ConstCls.KEIYAKU_JYOUKYOU_NAME_2;
                dt.Rows.Add(row);

                form.table = dt;
                form.PopupTitleLabel = "契約状況";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "契約状況CD", "契約状況名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.form.KEIYAKU_JYOUKYOU_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.form.KEIYAKU_JYOUKYOU_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckListPopup", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #endregion
    }
}
