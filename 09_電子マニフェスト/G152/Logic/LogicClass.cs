using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon;
using System.Text.RegularExpressions;
using r_framework.Dto;
using System.Collections;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.DenshiCSVTorikomu.Setting.ButtonSetting.xml";

        #region Dao初期化
        /// <summary>
        ///電子マニフェストテーブル用Dao
        /// </summary>
        private DT_R18DaoCls DT_R18Dao;

        /// <summary>
        ///マニフェスト目次情報[DT_MF_TOC]用Dao
        /// </summary>
        private DT_MF_TOCDaoCls DT_MF_TOCDao;

        /// <summary>
        ///加入者番号[DT_MF_MEMBER]用Dao
        /// </summary>
        private DT_MF_MEMBERDaoCls DT_MF_MEMBERDao;

        /// <summary>
        /// 収集運搬情報[DT_R19]用Dao
        /// </summary>
        private DT_R19DaoCls DT_R19Dao;
        /// <summary>
        /// 有害物質情報[DT_R02]用Dao
        /// </summary>
        private DT_R02DaoCls DT_R02Dao;

        /// <summary>
        /// 最終処分事業場(予定)情報[DT_R04]用DAO
        /// </summary>
        private DT_R04DaoCls DT_R04Dao;

        /// <summary>
        /// 連絡番号情報[DT_R05]用DAO
        /// </summary>
        private DT_R05DaoCls DT_R05Dao;

        /// <summary>
        /// 備考情報[DT_R06]用DAO
        /// </summary>
        private DT_R06DaoCls DT_R06Dao;

        /// <summary>
        /// 最終処分終了日・事業場情報[DT_R13]用DAO
        /// </summary>
        private DT_R13DaoCls DT_R13Dao;

        /// <summary>
        /// 電子マニフェスト基本拡張テーブル[DT_R18_EX]登録更新削除用Dao
        /// </summary>
        private DT_R18_EXDaoCls DT_R18_EXDao;

        /// <summary>
        /// 電子マニフェスト収集運搬拡張[DT_R19_EX]登録更新削除用Dao
        /// </summary>
        private DT_R19_EXDaoCls DT_R19_EXDao;

        /// <summary>
        ///電子マニフェスト最終処分（予定）拡張[DT_R04_EX]登録更新削除用Dao
        /// </summary>
        private DT_R04_EXDaoCls DT_R04_EXDao;

        /// <summary>
        /// 電子マニフェスト最終処分拡張[DT_R13_EX]登録更新削除用Dao
        /// </summary>
        private DT_R13_EXDaoCls DT_R13_EXDao;

        /// <summary>
        /// 電子マニフェスト存在チェック検索用Dao
        /// </summary>
        private DT_R18SearchDaoCls DT_R18SearchDao;

        /// <summary>
        /// 電子業者検索用Dao
        /// </summary>
        private DENSHI_JIGYOUSHA_SearchDaoCls DENSHI_JIGYOUSHA_SearchDao;

        /// <summary>
        ///電子事業場検索用Dao
        /// </summary>
        private DENSHI_JIGYOUJOU_SearchDaoCls DENSHI_JIGYOUJOU_SearchDao;

        /// <summary>
        ///電子担当者検索用Dao
        /// </summary>
        private DENSHI_TANTOUSHA_SearchDaoCls DENSHI_TANTOUSHA_SearchDao;

        /// <summary>
        ///単位マスタ検索用Dao
        /// </summary>
        private M_UNITDao M_UNITDao;

        /// <summary>
        ///電子廃棄物名称マスタ検索用Dao
        /// </summary>
        private DENSHI_HAIKI_NAME_SearchDaoCls DENSHI_HAIKI_NAME_SearchDao;

        /// <summary>
        ///荷姿マスタ検索用Dao
        /// </summary>
        private M_NISUGATADao M_NISUGATADao;

        /// <summary>
        ///有害物質マスタ検索用Dao
        /// </summary>
        private DENSHI_YUUGAI_BUSSHITSU_SearchDaoCls DENSHI_YUUGAI_BUSSHITSU_SearchDao;

        /// <summary>
        ///運搬方法マスタ検索用Dao
        /// </summary>
        private M_UNPAN_HOUHOUDao M_UNPAN_HOUHOUDao;

        /// <summary>
        ///車輌マスタ検索用Dao
        /// </summary>
        private M_SHARYOUDao M_SHARYOUDao;

        /// <summary>
        ///処分方法マスタ検索用Dao
        /// </summary>
        private M_SHOBUN_HOUHOUDao M_SHOBUN_HOUHOUDao;

        /// <summary>
        ///システム情報検索用Dao
        /// </summary>
        private M_SYS_INFODao M_SYS_INFODao;

        /// <summary>
        /// 郵便辞書マスタのDao
        /// </summary>
        private IS_ZIP_CODEDao zipCodeDao;

        #endregion

        /// <summary>
        /// DataTable
        /// </summary>
        //public DataTable MotoTable;
        public List<string[]> lineList;

        /// <summary>
        /// 画面フォーム
        /// </summary>
        private UIForm form;
        private UIHeader header;
        private BusinessBaseForm footer;

        /// <summary>
        /// マニフェスト番号
        /// </summary>
        string manifestID;

        /// <summary>
        /// 管理番号
        /// </summary>
        string kanriID;

        /// <summary>
        /// システムID
        /// </summary>
        SqlInt64 systemID;

        /// <summary>
        /// 運搬区間分割区分
        /// </summary>
        int unpankukanKubun;

        /// <summary>
        /// 更新フラグ
        /// </summary>
        bool updateFlg;

        /// <summary>
        /// 紙テーブル最新枝番
        /// </summary>
        SqlDecimal latestSEQ;

        /// <summary>
        /// 紙テーブル更新前最新枝番
        /// </summary>
        SqlDecimal oldLatestSEQ;

        /// <summary>
        /// 拡張テーブル更新前最新枝番
        /// </summary>
        SqlInt32 EXlatestSEQ;

        /// <summary>
        /// 拡張テーブル更新前最新枝番
        /// </summary>
        SqlInt32 oldEXlatestSEQ;

        /// <summary>
        /// 最終処分事業場 予定n R04登録件数
        /// </summary>
        int yoteiYoukouKunkan;

        /// <summary>
        /// 最終処分事業場 実績n R13登録件数
        /// </summary>
        int jissekiYoukouKunkan;

        /// <summary>
        /// 最終処分事業場 実績n
        /// </summary>
        SqlDecimal DT_R19_YOUKOUSAIGO_UPN_SUU;

        /// <summary>
        /// 排出事業者の加入者番号
        /// </summary>
        string HST_SHA_EDI_MEMBER_ID;

        /// <summary>
        /// エラーカメッセージ
        /// </summary>
        string checkErrorMessage;

        /// <summary>
        /// エラーカメッセージカウント
        /// </summary>
        int checkErrorMessageCounter;

        int rowCounter;
        int columnCounter;

        public MasterDataCls MstDataInfo;

        List<DenshiManifestInfoCls> listDMInfo;

        /// <summary>
        /// 都道府県のリスト
        /// </summary>
        private List<String> todoufukenList;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.DENSHI_JIGYOUSHA_SearchDao = DaoInitUtility.GetComponent<DENSHI_JIGYOUSHA_SearchDaoCls>();
            this.DENSHI_JIGYOUJOU_SearchDao = DaoInitUtility.GetComponent<DENSHI_JIGYOUJOU_SearchDaoCls>();
            this.DENSHI_TANTOUSHA_SearchDao = DaoInitUtility.GetComponent<DENSHI_TANTOUSHA_SearchDaoCls>();

            this.M_SHARYOUDao = DaoInitUtility.GetComponent<M_SHARYOUDao>();
            this.DT_MF_TOCDao = DaoInitUtility.GetComponent<DT_MF_TOCDaoCls>();
            this.DT_R18Dao = DaoInitUtility.GetComponent<DT_R18DaoCls>();
            this.DT_MF_MEMBERDao = DaoInitUtility.GetComponent<DT_MF_MEMBERDaoCls>();
            this.DT_R19Dao = DaoInitUtility.GetComponent<DT_R19DaoCls>();
            this.DT_R02Dao = DaoInitUtility.GetComponent<DT_R02DaoCls>();
            this.DT_R04Dao = DaoInitUtility.GetComponent<DT_R04DaoCls>();
            this.DT_R05Dao = DaoInitUtility.GetComponent<DT_R05DaoCls>();
            this.DT_R06Dao = DaoInitUtility.GetComponent<DT_R06DaoCls>();
            this.DT_R13Dao = DaoInitUtility.GetComponent<DT_R13DaoCls>();
            this.DT_R18_EXDao = DaoInitUtility.GetComponent<DT_R18_EXDaoCls>();
            this.DT_R19_EXDao = DaoInitUtility.GetComponent<DT_R19_EXDaoCls>();
            this.DT_R04_EXDao = DaoInitUtility.GetComponent<DT_R04_EXDaoCls>();
            this.DT_R13_EXDao = DaoInitUtility.GetComponent<DT_R13_EXDaoCls>();
            this.M_SYS_INFODao = DaoInitUtility.GetComponent<M_SYS_INFODao>();
            this.M_UNITDao = DaoInitUtility.GetComponent<M_UNITDao>();
            this.M_NISUGATADao = DaoInitUtility.GetComponent<M_NISUGATADao>();
            this.M_UNPAN_HOUHOUDao = DaoInitUtility.GetComponent<M_UNPAN_HOUHOUDao>();
            this.M_SHOBUN_HOUHOUDao = DaoInitUtility.GetComponent<M_SHOBUN_HOUHOUDao>();
            this.DENSHI_HAIKI_NAME_SearchDao = DaoInitUtility.GetComponent<DENSHI_HAIKI_NAME_SearchDaoCls>();
            this.DENSHI_YUUGAI_BUSSHITSU_SearchDao = DaoInitUtility.GetComponent<DENSHI_YUUGAI_BUSSHITSU_SearchDaoCls>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            lineList = new List<string[]>();
            listDMInfo = new List<DenshiManifestInfoCls>();
            MstDataInfo = new MasterDataCls();

            var mTodoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            todoufukenList = mTodoufukenDao.GetAllData().Select(m => m.TODOUFUKEN_NAME).ToList();

            this.zipCodeDao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();

            LogUtility.DebugMethodEnd();
        }

        private void GetAllMstInfo()
        {
            LogUtility.DebugMethodStart();

            SearchMasterDataDTOCls data = new SearchMasterDataDTOCls();
            data.EDI_MEMBER_IDAry = new string[] { };
            for (int i = 1; i < lineList.Count; i++)
            {
                string[] ary = new string[]{lineList[i][6],lineList[i][45],lineList[i][53],lineList[i][76],
                    lineList[i][83],lineList[i][96],lineList[i][242],lineList[i][250],lineList[i][273],
                    lineList[i][280],lineList[i][288],lineList[i][311],lineList[i][318],lineList[i][326],
                    lineList[i][349],lineList[i][356],lineList[i][364],lineList[i][387]};
                data.EDI_MEMBER_IDAry = data.EDI_MEMBER_IDAry.Union(ary).ToArray();
            }
            if (data.EDI_MEMBER_IDAry.Length < 1)
            {
                data.EDI_MEMBER_IDAry = new string[] { "DAMIDATA" };
            }
            MstDataInfo.denshiJgyosyaTb = DENSHI_JIGYOUSHA_SearchDao.GetDataForEntity(data);
            MstDataInfo.denshiJgyoujoTb = DENSHI_JIGYOUJOU_SearchDao.GetDataForEntity(data);
            MstDataInfo.denshiTantousyaTb = DENSHI_TANTOUSHA_SearchDao.GetDataForEntity(data);
            MstDataInfo.denshiHakkiNameTb = DENSHI_HAIKI_NAME_SearchDao.GetDataForEntity(data);
            MstDataInfo.sharryouTb = M_SHARYOUDao.GetDataForEntity(data);

            MstDataInfo.denshiYugaibushituTb = DENSHI_YUUGAI_BUSSHITSU_SearchDao.GetDataForEntity(data);
            MstDataInfo.unpanHouhouTb = M_UNPAN_HOUHOUDao.GetDataForEntity(new M_UNPAN_HOUHOU());
            MstDataInfo.nisugataTb = M_NISUGATADao.GetDataForEntity(new M_NISUGATA());
            MstDataInfo.unitTb = M_UNITDao.GetDataForEntity(new M_UNIT());
            MstDataInfo.syoubunHouhouTb = M_SHOBUN_HOUHOUDao.GetDataForEntity(new M_SHOBUN_HOUHOU());
            MstDataInfo.msJwnetMember = DaoInitUtility.GetComponent<MS_JWNET_MEMBERDaoCls>().GetAllData();

            LogUtility.DebugMethodEnd();
        }

        #region 実現必須メソッド
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンを初期化
                this.ButtonInit();

                //footボタン処理イベントを初期化
                EventInit();

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                messboxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                messboxShow("E245", "");
                return false;
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
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            // データ取込(F1)イベント作成
            parentform.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            //実行ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // クリックイベント
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.form.CustomDataGridView1_CellClick);
            this.form.customDataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(this.form.CustomDataGridView1_CellMouseClick);
            this.form.customDataGridView1.CurrentCellDirtyStateChanged += new EventHandler(this.form.CustomDataGridView1_CurrentCellDirtyStateChanged);
            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.form.CustomDataGridView1_ColumnHeaderMouseClick);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ヘッダー初期化処理
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        } 
        #endregion

        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            LogUtility.DebugMethodStart(Kbn);

            try
            {
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //タイトル
                        this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_DENSHI_MANIFEST_CSV_INPUT);

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //処理No（ESC）
                        //this.footer.txb_process.Tag = "【Enter】を押下すると指定した番号の処理が実行されます";
                        this.footer.txb_process.ReadOnly = true;

                        //アラート件数表示
                        M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                        this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                        this.header.NumberAlert = this.header.InitialNumberAlert;
                        this.header.AlertNumber.Text = this.header.InitialNumberAlert.ToString();
                        this.header.ReadDataNumber.Text = "";

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        //this.SearchResult.Clear();

                        break;
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("ClearScreen", ex1);
                messboxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ClearScreen", ex2);
                messboxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                messboxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 処理No 処理
        /// <summary>
        /// 処理No ボタン選択
        /// </summary>
        //public void SelectButton()
        //{
        //    LogUtility.DebugMethodStart();
        //    try
        //    {
        //        switch (this.footer.txb_process.Text)
        //        {
        //            case "1"://【1】パターン一覧
        //                this.footer.bt_process1.PerformClick();
        //                break;

        //            case "2"://【2】検索条件設定
        //                this.footer.bt_process2.PerformClick();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Debug(ex);

        //        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
        //        {

        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// 処理No フォーカス移動
        /// </summary>
        //public void SetFocusTxbProcess()
        //{
        //    LogUtility.DebugMethodStart();

        //    try
        //    {
        //        this.footer.txb_process.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Debug(ex);

        //        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
        //        {

        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //} 
        #endregion

        #region CSVファイルリード
        /// <summary>
        /// CSVRead
        /// </summary>
        public void CSVRead(string filepath)
        {
            LogUtility.DebugMethodStart(filepath);

            if (string.IsNullOrWhiteSpace(filepath)) {
                return;
            }
            lineList.Clear();
            var messageUtil = new MessageUtility();
            string errorMessage = string.Empty;
            string templeErrorMessage = string.Empty;
            int errCounter = 0;
            DialogResult result;

            string[] strAry;
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filepath, Encoding.GetEncoding("Shift_JIS"));
            }
            catch
            {
                //ファイルはほかのユーザが使用中です
                errorMessage = messageUtil.GetMessage("E120").MESSAGE;
                errorMessage = String.Format(errorMessage);
                result = MessageBox.Show(errorMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.form.ctxt_FilePath.Text = "";
                this.header.ReadDataNumber.Text = "";
                return;
            }

            string[] head = lines[0].Split(',');
            int cnt = head.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Split(',').Length != 402)
                {
                    if (errCounter < 10)
                    {
                        templeErrorMessage = messageUtil.GetMessage("E099").MESSAGE;
                        templeErrorMessage = String.Format(templeErrorMessage, "CSVデータの項目数", "402項目");
                        errorMessage += (i + 1) + "行目：" + templeErrorMessage + "\r\n";
                    }
                    errCounter++;
                    continue;
                }
                strAry = new string[402];
                strAry = GetRow(lines[i], 402);
                lineList.Add(strAry);
            }
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                if (10 < errCounter) {
                    templeErrorMessage = messageUtil.GetMessage("E100").MESSAGE;
                    templeErrorMessage = String.Format(templeErrorMessage, (errCounter-10).ToString(), "CSVファイル");
                    errorMessage += templeErrorMessage + "\r\n";
                }
                result = MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lineList.Clear();
                this.form.ctxt_FilePath.Text = "";
            }

            if (!nagasaCheck())
            {
                //GetAllMstInfo();
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// String長さチェック
        /// </summary>
        /// <param name="lineList">行内容</param>
        /// <returns></returns>
        private bool nagasaCheck() {

            var messageUtil = new MessageUtility();
            string templeErrorMessage;
            string errorMessage = "";
            int errCounter = 0;

            int[] intary = { 11, 1, 20, 20, 20, 10, 7, 260, 8, 228, 15, 15, 120, 8, 228, 15, 24, 24,
                               7, 60, 60, 9, 1, 20, 60, 2, 40, 5, 2, 80, 2, 80, 2, 80, 2, 80, 2, 80,
                               2, 80, 2, 9, 1, 20, 1, 7, 260, 8, 228, 15, 15, 6, 1, 7, 260, 8, 228, 15,
                               15, 6, 1, 20, 24, 30, 10, 19, 24, 24, 9, 1, 20, 9, 1, 20, 30, 256, 7, 3,
                               120, 8, 228, 15, 1, 7, 260, 8, 228, 15, 15, 6, 3, 120, 8, 228, 15, 1, 7,
                               260, 8, 228, 15, 15, 6, 3, 30, 1, 10, 19, 24, 24, 10, 24, 30, 9, 1, 20,
                               256, 1, 120, 8, 228, 15, 120, 8, 228, 15, 120, 8, 228, 15, 120, 8, 228,
                               15, 120, 8, 228, 15, 120, 8, 228, 15, 120, 8, 228, 15, 120, 8, 228, 15,
                               120, 8, 228, 15, 120, 8, 228, 15, 10, 19, 10, 120, 8, 228, 15, 10, 120,
                               8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228,
                               15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15, 10,
                               120, 8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8,
                               228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15, 10, 120, 8, 228, 15,
                               50, 50, 50, 50, 50, 1, 1, 7, 260, 8, 228, 15, 15, 6, 1, 7, 260, 8, 228,
                               15, 15, 6, 1, 20, 24, 30, 10, 19, 24, 24, 9, 1, 20, 9, 1, 20, 30, 256,
                               7, 3, 120, 8, 228, 15, 1, 7, 260, 8, 228, 15, 15, 6, 1, 7, 260, 8, 228,
                               15, 15, 6, 1, 20, 24, 30, 10, 19, 24, 24, 9, 1, 20, 9, 1, 20, 30, 256,
                               7, 3, 120, 8, 228, 15, 1, 7, 260, 8, 228, 15, 15, 6, 1, 7, 260, 8, 228,
                               15, 15, 6, 1, 20, 24, 30, 10, 19, 24, 24, 9, 1, 20, 9, 1, 20, 30, 256,
                               7, 3, 120, 8, 228, 15, 1, 7, 260, 8, 228, 15, 15, 6, 1, 7, 260, 8, 228,
                               15, 15, 6, 1, 20, 24, 30, 10, 19, 24, 24, 9, 1, 20, 9, 1, 20, 30, 256,
                               7, 3, 120, 8, 228, 15, 1, 9, 19, 19, 19, 19, 10, 19, 1};

            for(int i =1;i<lineList.Count;i++){
                for (int j = 0; j < 402; j++)
                {
                    if (intary[j] < Encoding.Default.GetByteCount(lineList[i][j]))
                    {
                        if (errCounter < 10)
                        {
                            templeErrorMessage = messageUtil.GetMessage("E116").MESSAGE;
                            templeErrorMessage = String.Format(templeErrorMessage, lineList[0][j], intary[j]);
                            errorMessage += (i + 1) + "行目：" + templeErrorMessage + "\r\n";
                        }
                        errCounter++;
                        continue;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                if (10 < errCounter)
                {
                    templeErrorMessage = messageUtil.GetMessage("E100").MESSAGE;
                    templeErrorMessage = String.Format(templeErrorMessage, (errCounter - 10).ToString(), "CSVファイル");
                    errorMessage += templeErrorMessage + "\r\n";
                }
                MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lineList.Clear();
                this.form.ctxt_FilePath.Text = "";
                return true;
            }
            return false;
        }
        /// <summary>
        /// CSVファイルを解析
        /// </summary>
        /// <param name="line">行内容</param>
        /// <param name="cnt">行数</param>
        /// <returns></returns>
        static private string[] GetRow(string line, int cnt)
        {
            //line = line.Replace("\"\"", "\""); 
            string[] strs = line.Split(',');
            if (strs.Length == cnt)
            {
                return RemoveQuotes(strs);
            }
            List<string> list = new List<string>();
            int n = 0, begin = 0;
            bool flag = false;

            for (int i = 0; i < strs.Length; i++)
            {

                if (strs[i].IndexOf("\"") == -1
                    || (flag == false && strs[i][0] != '\"'))
                {
                    list.Add(strs[i]);
                    continue;
                }
                n = 0;
                foreach (char ch in strs[i])
                {
                    if (ch == '\"')
                    {
                        n++;
                    }
                }
                if (n % 2 == 0)
                {
                    list.Add(strs[i]);
                    continue;
                }
                flag = true;
                begin = i;
                i++;
                for (i = begin + 1; i < strs.Length; i++)
                {
                    foreach (char ch in strs[i])
                    {
                        if (ch == '\"')
                        {
                            n++;
                        }
                    }
                    if (strs[i][strs[i].Length - 1] == '\"' && n % 2 == 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (; begin <= i; begin++)
                        {
                            sb.Append(strs[begin]);
                            if (begin != i)
                            {
                                sb.Append(",");
                            }
                        }
                        list.Add(sb.ToString());
                        break;
                    }
                }
            }
            return RemoveQuotes(list.ToArray());
        }
        /// <summary>
        /// 余計な”を除いて
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        static string[] RemoveQuotes(string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == "\"\"")
                {
                    strs[i] = "";
                    continue;
                }
                if (strs[i].Length > 2 && strs[i][0] == '\"' && strs[i][strs[i].Length - 1] == '\"')
                {
                    strs[i] = strs[i].Substring(1, strs[i].Length - 2);
                }
                strs[i] = strs[i].Replace("\"\"", "\"");
            }
            return strs;
        }

        /// <summary>
        /// CSVファイルをグリッドに設定
        /// </summary>
        public bool SetDataToDgv()
        {
            LogUtility.DebugMethodStart();
            bool ret = false;
            int counter = 0;
            //行を全て削除
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Rows.Clear();
            this.form.customDataGridView1.Columns.RemoveAt(0);
            if (!HeaderCheckBoxSupport())
            {
                return ret;
            }

            //検索結果設定
            foreach (string[] strAry in this.lineList)
            {
                if ("0".Equals(strAry[401]))
                {
                    string str = strAry[40];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        switch (str)
                        {
                            case "01": str = "排出事業者";
                                break;
                            case "02": str = "処分業者";
                                break;
                            case "03": str = "収集運搬業者（区間1）";
                                break;
                            case "04": str = "収集運搬業者（区間2）";
                                break;
                            case "05": str = "収集運搬業者（区間3）";
                                break;
                            case "06": str = "収集運搬業者（区間4）";
                                break;
                            case "07": str = "収集運搬業者（区間5）";
                                break;
                        }
                    }

                    string str2;
                    if (string.IsNullOrWhiteSpace(strAry[21]))
                    {
                        str2 = "";
                    }
                    else
                    {
                        if (IsNumericAndMinusCheck(strAry[21]))
                        {
                            str2 = "";
                        }
                        else
                        {
                            str2 = string.Format("{0:N3}", decimal.Parse(strAry[21]));
                        }
                    }

                    this.form.customDataGridView1.Rows.Add(false,strAry[0], strAry[5],
                        strAry[7], strAry[12], strAry[2],
                        strAry[3], strAry[4], strAry[20],
                        strAry[24], str2, strAry[23],
                        strAry[26], str, strAry[29],
                        strAry[46], strAry[61], strAry[63],
                        strAry[84], strAry[91], strAry[104],
                        strAry[161], strAry[235], strAry[236],
                        strAry[237], strAry[238], strAry[239], 
                        lineList.IndexOf(strAry));
                    counter++;
                }
            }
            this.header.ReadDataNumber.Text = counter.ToString();
            ret = true;
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public bool HeaderCheckBoxSupport()
        {
            try
            {
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                newColumn.Name = "";
                newColumn.Width = 50;
                DatagridViewCheckBoxHeaderCell newheader = new DatagridViewCheckBoxHeaderCell(0);
                newColumn.HeaderCell = newheader;
                newColumn.ReadOnly = false;

                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex1);
                messboxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex);
                messboxShow("E245", "");
                return false;
            }
        } 
        #endregion

        #region エラーチェック
        /// <summary>
        /// 必須入力チェック
        /// </summary>
        public void setMessege(string str1,string str2)
        {
            if (checkErrorMessageCounter < 10)
            {
                var messageUtil = new MessageUtility();
                string tempMessage = messageUtil.GetMessage(str1).MESSAGE;
                tempMessage = String.Format(tempMessage, str2);
                checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
            }
            checkErrorMessageCounter++;
        }

        /// <summary>
        /// 文字列がマイナスの数値かどうかをチェックします
        /// </summary>
        /// <param name="str">チェック対象文字列</param>
        /// <returns>マイナスの数値のときはTrue、それ以外はFalse</returns>
        public bool IsNumericAndMinusCheck(string str)
        {
            LogUtility.DebugMethodStart(str);

            var ret = false;

            var parseResult = 0m;
            if (decimal.TryParse(str, out parseResult))
            {
                if (parseResult < 0)
                {
                    ret = true;
                }
            }
            else
            {
                ret = true;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 区分チェック
        /// </summary>
        public bool KunbunCheck(string str1,string str2)
        {
            LogUtility.DebugMethodStart(str1, str2);
            if (string.IsNullOrWhiteSpace(str1))
                return false;
            switch (str2)
            {
                case "登録の状態":
                    if (!"1".Equals(str1) && !"2".Equals(str1) && !"3".Equals(str1) && !"4".Equals(str1) && !"5".Equals(str1))
                    {
                        if (checkErrorMessageCounter < 10)
                        {
                            var messageUtil = new MessageUtility();
                            string tempMessage = messageUtil.GetMessage("E034").MESSAGE;
                            tempMessage = String.Format(tempMessage, "登録の状態には1～5");
                            checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                        }
                        checkErrorMessageCounter++;
                        return true;
                    }
                    break;
                case "数量の確定者（コード）":
                    if (!"01".Equals(str1) && !"02".Equals(str1) && !"03".Equals(str1) && !"04".Equals(str1) && !"05".Equals(str1)
                        && !"06".Equals(str1) && !"07".Equals(str1))
                    {
                        if (checkErrorMessageCounter < 10)
                        {
                            var messageUtil = new MessageUtility();
                            string tempMessage = messageUtil.GetMessage("E034").MESSAGE;
                            tempMessage = String.Format(tempMessage, "数量の確定者（コード）には01～07");
                            checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                        }
                        checkErrorMessageCounter++;
                        return true;
                    }
                    break;
                case "運搬委託区分":
                    if (!"0".Equals(str1) && !"1".Equals(str1) && !"2".Equals(str1) && !"3".Equals(str1))
                    {
                        return true;
                    }
                    break;
                case "報告区分【処分報告】":
                    if (!"1".Equals(str1) && !"2".Equals(str1))
                    {
                        return true;
                    }
                    break;

                case "運搬区間分割区分":
                    if (!"0".Equals(str1) && !"1".Equals(str1) && !"2".Equals(str1) && !"3".Equals(str1) && !"4".Equals(str1) && !"5".Equals(str1))
                    {
                        return true;
                    }
                    break;
                case "修正許可":
                    if (!"0".Equals(str1) && !"1".Equals(str1) && !"2".Equals(str1) && !"3".Equals(str1) && !"4".Equals(str1))
                    {
                        return true;
                    }
                    break;
                case "承認待ち":
                    if (!"0".Equals(str1) && !"1".Equals(str1))
                    {
                        return true;
                    }
                    break;
                case "廃棄物の種類（分類コード）":
                    if (str1.ToString().Length < 7)
                    {
                        if (checkErrorMessageCounter < 10)
                        {
                            string tempMessage = "廃棄物の種類（分類コード）の桁数が7桁未満です。";                            
                            checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                        }
                        checkErrorMessageCounter++;
                        return true;
                    }
                    break;
            }
            LogUtility.DebugMethodEnd();
            return false;
        }

        /// <summary>
        /// 日付の整合性チェック
        /// </summary>
        public bool DataCheck(string strDate)
        {
            LogUtility.DebugMethodStart(strDate);
            if ("Null".Equals(strDate)) {
                return true;
            }

            try
            {
                SqlDateTime.Parse(strDate);
            }
            catch
            {
                return true;
            }
            LogUtility.DebugMethodEnd(strDate);
            return false;
        }

        /// <summary>
        /// 単位マスタの存在チェック
        /// </summary>
        public bool TannyiMasterCheck(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            string SearchSQL = "UNIT_CD ='" + str + "'";

            DataRow[] drArr = MstDataInfo.unitTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                if (columnCounter == 22)
                {
                    if (checkErrorMessageCounter < 10)
                    {
                        var messageUtil = new MessageUtility();
                        string tempMessage = messageUtil.GetMessage("E170").MESSAGE;
                        tempMessage = String.Format(tempMessage, "単位", "名称");
                        checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                    }
                    checkErrorMessageCounter++;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 廃棄物名称マスタの存在チェック//
        /// </summary>
        public bool DensihaikinameMasterCheck(string str1, string str2)
        {
            string SearchSQL = "EDI_MEMBER_ID ='" + str1 + "'";
            SearchSQL += " AND HAIKI_NAME ='" + str2 + "'";
            DataRow[] drArr = MstDataInfo.denshiHakkiNameTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 荷姿マスタの存在チェック
        /// </summary>
        public bool NisugataMasterCheck(string str)
        {
            string SearchSQL = "NISUGATA_CD ='" + str + "'";

            DataRow[] drArr = MstDataInfo.nisugataTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                if (checkErrorMessageCounter < 10)
                {
                    var messageUtil = new MessageUtility();
                    string tempMessage = messageUtil.GetMessage("E170").MESSAGE;
                    tempMessage = String.Format(tempMessage, "荷姿", "名称");
                    checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                }
                checkErrorMessageCounter++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 電子有害物質マスタの存在チェック
        /// </summary>
        public bool DenshiyuugaibusshitsuMasterCheck(string str)
        {
            string SearchSQL = "YUUGAI_BUSSHITSU_CD ='" + str + "'";

            DataRow[] drArr = MstDataInfo.denshiYugaibushituTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 運搬方法マスタの存在チェック
        /// </summary>
        public bool UnpanhouhouMasterCheck(string str)
        {
            string SearchSQL = "UNPAN_HOUHOU_CD ='" + str + "'";

            DataRow[] drArr = MstDataInfo.unpanHouhouTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 車輌マスタの存在チェック
        /// </summary>
        public bool SharyouMasterCheck(string str1,string str2)
        {
            string SearchSQL = "GYOUSHA_CD ='" + str1 + "'";
            SearchSQL += " AND SHARYOU_NAME ='" + str2 + "'";
            DataRow[] drArr = MstDataInfo.sharryouTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 処分方法マスタの存在チェック
        /// </summary>
        public bool ShobunhouhouMasterCheck(string str)
        {
            string SearchSQL = "SHOBUN_HOUHOU_CD ='" + str + "'";
            DataRow[] drArr = MstDataInfo.syoubunHouhouTb.Select(SearchSQL);
            if (drArr.Length < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 郵便辞書マスタ（都道府県+市区町村）の存在チェック
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns>true：エラーあり false：エラーなし</returns>
        private bool AddressCheck(string str1, string str2)
        {
            int zipCodeCount = this.zipCodeDao.GetDataByJushoCountLikeSearch(str1);
            if(zipCodeCount == 0)
            {
                if (checkErrorMessageCounter < 10)
                {
                    var messageUtil = new MessageUtility();
                    string tempMessage = messageUtil.GetMessage("E170").MESSAGE;
                    tempMessage = String.Format(tempMessage, "郵便辞書", "住所");
                    var sb = new StringBuilder();
                    sb.Append(rowCounter).Append( "行目：").AppendLine(tempMessage);
                    sb.AppendLine(String.Format("（住所 :{0}）",str2));
                    checkErrorMessage += sb.ToString();
                }
                checkErrorMessageCounter++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void checkRow(string[] strAry)
        {
            LogUtility.DebugMethodStart(strAry);

            if (!"0".Equals(strAry[401])) { return; }

            kanriID = "";
            latestSEQ = 0;
            oldLatestSEQ = 0;
            manifestID = strAry[0];
            updateFlg = false;

            DT_R19_YOUKOUSAIGO_UPN_SUU = -1;

            //運搬区間分割区分
            if (!string.IsNullOrWhiteSpace(strAry[240]))
            {
                if (!KunbunCheck(strAry[240], "運搬区間分割区分"))
                {
                    unpankukanKubun = int.Parse(strAry[240]);
                }
            }

            //※通常は0オリジンだが、0行目はCSVのタイトル行が格納されているため1オリジンとして考える
            rowCounter = lineList.IndexOf(strAry);
            columnCounter = 0;
            DenshiManifestInfoCls DMInfo = new DenshiManifestInfoCls();

            HST_SHA_EDI_MEMBER_ID = strAry[6];
            string HST_JOU_NAME = strAry[12];
            string HST_JOU_ADDRESS = strAry[14];

            columnCounter = 0;

            if (string.IsNullOrWhiteSpace(strAry[0])) {
                setMessege("E001", lineList[0][0]);
            }

            if (string.IsNullOrWhiteSpace(strAry[1]))
            {
                setMessege("E001", lineList[0][1]);
            }
            else
            {
                KunbunCheck(strAry[1], "登録の状態");
            }

            if (string.IsNullOrWhiteSpace(strAry[5]))
            {
                setMessege("E001", lineList[0][5]);
            }
            else
            {
                if (DataCheck(strAry[5]))
                {
                    if (checkErrorMessageCounter < 10)
                    {
                        var messageUtil = new MessageUtility();
                        string tempMessage = messageUtil.GetMessage("E082").MESSAGE;
                        tempMessage = String.Format(tempMessage, lineList[0][5]);
                        checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                    }
                    checkErrorMessageCounter++;
                }
            }

            columnCounter = 6;
            if (string.IsNullOrWhiteSpace(strAry[6]))
            {
                setMessege("E001", lineList[0][6]);
            }

            columnCounter = 12;
            if (string.IsNullOrWhiteSpace(strAry[12]))
            {
                setMessege("E001", lineList[0][12]);
            }

            if (string.IsNullOrWhiteSpace(strAry[14])) {
                setMessege("E001", lineList[0][14]);
            }
            else
            {
                // 住所チェック（排出事業場の所在地）
                AddressCheck(strAry[14], lineList[0][14]);
            }

            columnCounter = 16;
            if (string.IsNullOrWhiteSpace(strAry[16]))
            {
                setMessege("E001", lineList[0][16]);
            }

            if (string.IsNullOrWhiteSpace(strAry[18])) {
                setMessege("E001", lineList[0][18]);
            }

            if (string.IsNullOrWhiteSpace(strAry[21]))
            {
                setMessege("E001", lineList[0][21]);
            }
            else
            {
                if (IsNumericAndMinusCheck(strAry[21])) {
                    if (checkErrorMessageCounter < 10)
                    {
                        var messageUtil = new MessageUtility();
                        string tempMessage = messageUtil.GetMessage("E082").MESSAGE;
                        tempMessage = String.Format(tempMessage, lineList[0][21]);
                        checkErrorMessage += (rowCounter) + "行目：" + tempMessage + "\r\n";
                    }
                    checkErrorMessageCounter++;
                };
            }

            columnCounter = 22;
            if (string.IsNullOrWhiteSpace(strAry[22]))
            {
                setMessege("E001", lineList[0][22]);
            }
            else
            {
                TannyiMasterCheck(strAry[22]);
            }

            if (string.IsNullOrWhiteSpace(strAry[25]))
            {
                setMessege("E001", lineList[0][25]);
            }
            else
            {
                NisugataMasterCheck(strAry[25]);
            }

            if (string.IsNullOrWhiteSpace(strAry[40]))
            {
                setMessege("E001", lineList[0][40]);
            }
            else
            {
                KunbunCheck(strAry[40], "数量の確定者（コード）");
            }

            columnCounter = 83;
            if (string.IsNullOrWhiteSpace(strAry[83]))
            {
                setMessege("E001", lineList[0][83]);
            }

            columnCounter = 91;
            if (string.IsNullOrWhiteSpace(strAry[91]))
            {
                setMessege("E001", lineList[0][91]);
            }

            columnCounter = 118;
            if ("1".Equals(strAry[117]))
            {
                if (string.IsNullOrWhiteSpace(strAry[118]))
                {
                    setMessege("E001", lineList[0][118]);
                }

                if (string.IsNullOrWhiteSpace(strAry[120]))
                {
                    setMessege("E001", lineList[0][120]);
                }
            }

            columnCounter = 230;
            if (string.IsNullOrWhiteSpace(strAry[18]))
            {
                setMessege("E001", lineList[0][18]);
            }
            else
            {
                KunbunCheck(strAry[18], "廃棄物の種類（分類コード）");
            }

            #region 住所チェック

            if(!string.IsNullOrWhiteSpace(strAry[9])) // 排出事業者の住所
            {
                AddressCheck(strAry[9], lineList[0][9]);
            }

            if(!string.IsNullOrWhiteSpace(strAry[86])) // 処分業者の住所
            {
                AddressCheck(strAry[86], lineList[0][86]);
            }

            if(!string.IsNullOrWhiteSpace(strAry[99])) // 処分業者の住所（再委託）
            {
                AddressCheck(strAry[99], lineList[0][99]);
            }

            #region 最終処分事業場（予定）
            if(!string.IsNullOrWhiteSpace(strAry[120]))
            {
                AddressCheck(strAry[120], lineList[0][120]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[124]))
            {
                AddressCheck(strAry[124], lineList[0][124]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[128]))
            {
                AddressCheck(strAry[128], lineList[0][128]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[132]))
            {
                AddressCheck(strAry[132], lineList[0][132]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[136]))
            {
                AddressCheck(strAry[136], lineList[0][136]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[140]))
            {
                AddressCheck(strAry[140], lineList[0][140]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[144]))
            {
                AddressCheck(strAry[144], lineList[0][144]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[148]))
            {
                AddressCheck(strAry[148], lineList[0][148]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[152]))
            {
                AddressCheck(strAry[152], lineList[0][152]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[156]))
            {
                AddressCheck(strAry[156], lineList[0][156]);
            }
            #endregion

            #region 最終処分事業場（実績）
            if(!string.IsNullOrWhiteSpace(strAry[163]))
            {
                AddressCheck(strAry[163], lineList[0][163]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[168]))
            {
                AddressCheck(strAry[168], lineList[0][168]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[173]))
            {
                AddressCheck(strAry[173], lineList[0][173]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[178]))
            {
                AddressCheck(strAry[178], lineList[0][178]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[183]))
            {
                AddressCheck(strAry[183], lineList[0][183]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[188]))
            {
                AddressCheck(strAry[188], lineList[0][188]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[193]))
            {
                AddressCheck(strAry[193], lineList[0][193]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[198]))
            {
                AddressCheck(strAry[198], lineList[0][198]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[203]))
            {
                AddressCheck(strAry[203], lineList[0][203]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[208]))
            {
                AddressCheck(strAry[208], lineList[0][208]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[213]))
            {
                AddressCheck(strAry[213], lineList[0][213]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[218]))
            {
                AddressCheck(strAry[218], lineList[0][218]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[223]))
            {
                AddressCheck(strAry[223], lineList[0][223]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[228]))
            {
                AddressCheck(strAry[228], lineList[0][228]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[233]))
            {
                AddressCheck(strAry[233], lineList[0][233]);
            }
            #endregion

            #region 運搬先事業場
            if(!string.IsNullOrWhiteSpace(strAry[80]))
            {
                AddressCheck(strAry[80], lineList[0][80]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[277]))
            {
                AddressCheck(strAry[277], lineList[0][277]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[315]))
            {
                AddressCheck(strAry[315], lineList[0][315]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[353]))
            {
                AddressCheck(strAry[353], lineList[0][353]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[391]))
            {
                AddressCheck(strAry[391], lineList[0][391]);
            }
            #endregion

            #region 収集運搬業者
            if(!string.IsNullOrWhiteSpace(strAry[48]))
            {
                AddressCheck(strAry[48], lineList[0][48]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[245]))
            {
                AddressCheck(strAry[245], lineList[0][245]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[283]))
            {
                AddressCheck(strAry[283], lineList[0][283]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[321]))
            {
                AddressCheck(strAry[321], lineList[0][321]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[359]))
            {
                AddressCheck(strAry[359], lineList[0][359]);
            }
            #endregion

            #region 収集運搬業者（再委託）
            if(!string.IsNullOrWhiteSpace(strAry[56]))
            {
                AddressCheck(strAry[56], lineList[0][56]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[253]))
            {
                AddressCheck(strAry[253], lineList[0][253]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[291]))
            {
                AddressCheck(strAry[291], lineList[0][291]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[329]))
            {
                AddressCheck(strAry[329], lineList[0][329]);
            }
            if(!string.IsNullOrWhiteSpace(strAry[367]))
            {
                AddressCheck(strAry[367], lineList[0][367]);
            }
            #endregion

            #endregion

            columnCounter = 0;
            if (0 < checkErrorMessageCounter) {
                return;
            }

            DT_MF_TOCsousa(strAry, DMInfo);
            DT_MF_MEMBERsousa(strAry, DMInfo);
            DT_R19sousa(strAry, DMInfo);
            DT_R02sousa(strAry, DMInfo);
            DT_R04sousa(strAry, DMInfo);
            DT_R05sousa(strAry, DMInfo);
            DT_R06sousa(strAry, DMInfo);
            DT_R13sousa(strAry, DMInfo);
            DT_R18sousa(strAry, DMInfo);

            var denshiCheckFlg = this.checkDenshiData(DMInfo);
            DT_R18_EXsousa(strAry, DMInfo, denshiCheckFlg);
            DT_R19_EXsousa(strAry, DMInfo, denshiCheckFlg);
            DT_R04_EXsousa(strAry, DMInfo, denshiCheckFlg);
            DT_R13_EXsousa(strAry, DMInfo, denshiCheckFlg);
            listDMInfo.Add(DMInfo);

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 各電子事業者、事業場の存在チェックと将軍Rの連携チェック
        /// </summary>
        /// <param name="DMInfo"></param>
        /// <returns></returns>
        public bool checkDenshiData(DenshiManifestInfoCls DMInfo)
        {
            if (!string.IsNullOrEmpty(DMInfo.dt_r18.KANRI_ID))
            {
                // 排出事業者
                if (!this.checkDenshiGyousha(DMInfo.dt_r18.HST_SHA_EDI_MEMBER_ID, 1))
                    return false;

                // 排出事業場
                if (!this.checkDenshiGenba(DMInfo.dt_r18.HST_SHA_EDI_MEMBER_ID, 1, DMInfo.dt_r18.FIRST_MANIFEST_FLAG
                    , DMInfo.dt_r18.HST_JOU_NAME
                    , DMInfo.dt_r18.HST_JOU_ADDRESS1 + DMInfo.dt_r18.HST_JOU_ADDRESS2 + DMInfo.dt_r18.HST_JOU_ADDRESS3 + DMInfo.dt_r18.HST_JOU_ADDRESS4))
                    return false;
            }

            if (DMInfo.lstDT_R19 != null)
            {
                for (int i = 0; i < DMInfo.lstDT_R19.Count; i++)
                {
                    var dtR19 = DMInfo.lstDT_R19[i];
                    if (!string.IsNullOrEmpty(dtR19.KANRI_ID))
                    {
                        // 収集運搬業者
                        if (!this.checkDenshiGyousha(dtR19.UPN_SHA_EDI_MEMBER_ID, 2))
                            return false;

                        if (i < DMInfo.lstDT_R19.Count - 1)
                        {
                            // 運搬先業者
                            if (!this.checkDenshiGyousha(dtR19.UPNSAKI_EDI_MEMBER_ID, 2))
                                return false;

                            // 運搬先事業場
                            if (!this.checkDenshiGenba(dtR19.UPNSAKI_EDI_MEMBER_ID, 2, ""
                                , dtR19.UPNSAKI_JOU_NAME
                                , dtR19.UPNSAKI_JOU_ADDRESS1 + dtR19.UPNSAKI_JOU_ADDRESS2 + dtR19.UPNSAKI_JOU_ADDRESS3 + dtR19.UPNSAKI_JOU_ADDRESS4))
                                return false;
                        }
                        else
                        {
                            // 処分業者
                            if (!this.checkDenshiGyousha(dtR19.UPNSAKI_EDI_MEMBER_ID, 3))
                                return false;

                            // 処分事業場
                            if (!this.checkDenshiGenba(dtR19.UPNSAKI_EDI_MEMBER_ID, 3, ""
                                , dtR19.UPNSAKI_JOU_NAME
                                , dtR19.UPNSAKI_JOU_ADDRESS1 + dtR19.UPNSAKI_JOU_ADDRESS2 + dtR19.UPNSAKI_JOU_ADDRESS3 + dtR19.UPNSAKI_JOU_ADDRESS4))
                                return false;
                        }
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 電子事業者の存在チェックと将軍Rの連携チェック
        /// </summary>
        /// <param name="ediMemberId">電子事業者加入番号</param>
        /// <param name="dType">
        /// 事業者区分
        /// 1.排出事業者
        /// 2.収集運搬業者
        /// 3.処分事業者
        /// </param>
        /// <param name="firstManifestFlg"></param>
        /// <param name="jigyoujouName">事業場名称</param>
        /// <param name="jigyoujouAddress">事業場住所</param>
        /// <returns></returns>
        public bool checkDenshiGenba(string ediMemberId, int dType, string firstManifestFlg, string jigyoujouName, string jigyoujouAddress)
        {
            bool rlt = false;
            for (int i = 0; i < MstDataInfo.denshiJgyoujoTb.Rows.Count; i++)
            {
                string mastEdiMemberId = MstDataInfo.denshiJgyoujoTb.Rows[i]["EDI_MEMBER_ID"].ToString();
                string mastGenbaCd = (MstDataInfo.denshiJgyoujoTb.Rows[i]["MST_GENBA_CD"] == null) ?
                    string.Empty : MstDataInfo.denshiJgyoujoTb.Rows[i]["MST_GENBA_CD"].ToString();

                int gsKbn = (MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUSHA_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUSHA_KBN"].ToString())) ? -1
                    : int.Parse(MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUSHA_KBN"].ToString());
                int gbKbn = (MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_KBN"].ToString())) ? -1
                    : int.Parse(MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_KBN"].ToString());

                string gbCd = (MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_CD"] == null) ? string.Empty
                    : MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_CD"].ToString();
                string gbName = (MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_NAME"] == null) ? string.Empty
                    : MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_NAME"].ToString();
                string gbAddress = (MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_ADDRESS"] == null) ? string.Empty
                    : MstDataInfo.denshiJgyoujoTb.Rows[i]["JIGYOUJOU_ADDRESS"].ToString();

                if (mastEdiMemberId.Equals(ediMemberId))
                {
                    switch (dType)
                    {
                        // 排出事業場
                        case 1:
                            if (((gsKbn == 1 && gbKbn == 1)
                                 || (gsKbn == 3 && gbKbn == 3 && (mastEdiMemberId.Substring(0, 1) == "3" || mastEdiMemberId.Substring(0, 2) == "D3"))
                                 || (!string.IsNullOrEmpty(firstManifestFlg) && gsKbn == 3 && gbKbn == 3))
                                && gbName.Equals(jigyoujouName)
                                && gbAddress.Equals(jigyoujouAddress))
                            {
                                if (!string.IsNullOrEmpty(gbCd) && !string.IsNullOrEmpty(mastGenbaCd))
                                {
                                    rlt = true;
                                    return rlt;
                                }
                            }
                            break;

                        // 運搬事業場
                        case 2:
                            if (gsKbn == 2
                                && gbKbn == 2
                                && gbName.Equals(jigyoujouName)
                                && gbAddress.Equals(jigyoujouAddress))
                            {
                                if (!string.IsNullOrEmpty(gbCd) && !string.IsNullOrEmpty(mastGenbaCd))
                                {
                                    rlt = true;
                                    return rlt;
                                }
                            }
                            break;

                        // 処分事業場
                        case 3:
                            if (gsKbn == 3
                                && gbKbn == 3
                                && gbName.Equals(jigyoujouName)
                                && gbAddress.Equals(jigyoujouAddress))
                            {
                                if (!string.IsNullOrEmpty(gbCd) && !string.IsNullOrEmpty(mastGenbaCd))
                                {
                                    rlt = true;
                                    return rlt;
                                }
                            }
                            break;
                    }
                }
            }

            return rlt;
        }
        /// <summary>
        /// 電子事業者の存在チェックと将軍Rの連携チェック
        /// </summary>
        /// <param name="ediMemberId">電子事業者加入番号</param>
        /// <param name="dType">
        /// 事業者区分
        /// 1.排出事業者
        /// 2.収集運搬業者
        /// 3.処分事業者
        /// </param>
        /// <returns></returns>
        public bool checkDenshiGyousha(string ediMemberId, int dType)
        {
            if (string.IsNullOrEmpty(ediMemberId))
                return true;

            bool rlt = false;
            for (int i = 0; i < MstDataInfo.denshiJgyosyaTb.Rows.Count; i++)
            {
                string mastEdiMemberId = MstDataInfo.denshiJgyosyaTb.Rows[i]["EDI_MEMBER_ID"].ToString();
                string mastGyoushaCd = (MstDataInfo.denshiJgyosyaTb.Rows[i]["MST_GYOUSHA_CD"] == null) ?
                    string.Empty : MstDataInfo.denshiJgyosyaTb.Rows[i]["MST_GYOUSHA_CD"].ToString();

                bool hstKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["HST_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["HST_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["HST_KBN"].ToString());
                bool upnKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["UPN_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["UPN_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["UPN_KBN"].ToString());
                bool sbnKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["SBN_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["SBN_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["SBN_KBN"].ToString());
                bool mastHstKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["HAISHUTSU_JIGYOUSHA_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["HAISHUTSU_JIGYOUSHA_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["HAISHUTSU_JIGYOUSHA_KBN"].ToString());
                bool mastUpnKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["UNPAN_JUTAKUSHA_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["UNPAN_JUTAKUSHA_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["UNPAN_JUTAKUSHA_KBN"].ToString());
                bool mastSbnKbn = (MstDataInfo.denshiJgyosyaTb.Rows[i]["SHOBUN_JUTAKUSHA_KBN"] == null || string.IsNullOrEmpty(MstDataInfo.denshiJgyosyaTb.Rows[i]["SHOBUN_JUTAKUSHA_KBN"].ToString())) ? false
                    : bool.Parse(MstDataInfo.denshiJgyosyaTb.Rows[i]["SHOBUN_JUTAKUSHA_KBN"].ToString());

                if (mastEdiMemberId.Equals(ediMemberId))
                {
                    switch (dType)
                    {
                        // 排出事業者
                        case 1:
                            if ((hstKbn || (sbnKbn && (mastEdiMemberId.Substring(0, 1) == "3" || mastEdiMemberId.Substring(0, 2) == "D3"))))
                            {
                                if (!string.IsNullOrEmpty(mastGyoushaCd) && mastHstKbn)
                                {
                                    rlt = true;
                                }
                            }
                            break;

                        // 収集運搬業者
                        case 2:
                            if (upnKbn)
                            {
                                if (!string.IsNullOrEmpty(mastGyoushaCd) && mastUpnKbn)
                                {
                                    rlt = true;
                                }
                            }
                            break;

                        // 処分事業者
                        case 3:
                            if (sbnKbn)
                            {
                                if (!string.IsNullOrEmpty(mastGyoushaCd) && mastSbnKbn)
                                {
                                    rlt = true;
                                }
                            }
                            break;
                    }
                    break;
                }
            }

            return rlt;
        }
        public bool errCheck(){
            LogUtility.DebugMethodStart();

            checkErrorMessage = string.Empty;
            checkErrorMessageCounter = 0;

            GetAllMstInfo();
            listDMInfo.Clear();

            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                // チェックがついているレコードを更新する
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        int j = int.Parse(dgvRow.Cells[dgvRow.Cells.Count - 1].Value.ToString());
                        checkRow(lineList[j]);
                    }
                }
            }

            if (0 < checkErrorMessageCounter)
            {
                if (10 < checkErrorMessageCounter) {
                    string errorMessage = string.Empty;
                    var messageUtil = new MessageUtility();
                    errorMessage = messageUtil.GetMessage("E100").MESSAGE;
                    errorMessage = String.Format(errorMessage, (checkErrorMessageCounter-10).ToString(), "CSVファイル");
                    checkErrorMessage += errorMessage;
                }
                MessageBox.Show(checkErrorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            LogUtility.DebugMethodEnd();
            return false;
        }

        #region 運搬先事業者チェック
        /// <summary>
        /// 運搬先事業者チェック
        /// 選択された行の運搬先事業者が、環境将軍RのM_DENSHI_JIGYOUSHAテーブルに登録されているか
        /// チェックする。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="message">"加入者番号：" + EDI_MENBER_IDの一覧</param>
        /// <returns>true:異常、false:正常</returns>
        internal bool CheckUpnSakiJigyousha(int rowIndex, out string message, out bool catchErr)
        {
            bool returnVal = false;
            catchErr = false;
            message = string.Empty;
            try
            {
                var row = this.form.customDataGridView1.Rows[rowIndex] as DataGridViewRow;
                int index = -1;
                if (int.TryParse(row.Cells[row.Cells.Count - 1].Value.ToString(), out index))
                {
                    var checkData = lineList[index];

                    for (int r19i = 0; r19i < 5; r19i++)
                    {
                        // 最終区間はチェック対象外(DT_R18.SBN_SHA_NAMEにデータが存在するため)
                        int unpankukan = -1;
                        if (!string.IsNullOrWhiteSpace(checkData[240].ToString()))
                        {
                            if (!KunbunCheck(checkData[240].ToString(), "運搬区間分割区分"))
                            {
                                unpankukan = int.Parse(checkData[240].ToString());
                            }
                        }

                        if (r19i == (unpankukan - 1))
                        {
                            // 最終区間
                            continue;
                        }

                        int celllength = 0;
                        if (r19i > 0)
                        {
                            celllength = 197;
                        }
                        else
                        {
                            celllength = 38;
                        }

                        int upnItakuIndex = 44 + celllength + (r19i - 1) * 38;
                        int upnSakiEdiMenberIdIndex = 76 + celllength + (r19i - 1) * 38;

                        if (checkData.Length < upnItakuIndex
                            || checkData.Length < upnSakiEdiMenberIdIndex)
                        {
                            continue;
                        }

                        string str = checkData[upnItakuIndex];

                        if ("0".Equals(str))
                        {
                            continue;
                        }

                        var upnSakiEdiMenberId = checkData[upnSakiEdiMenberIdIndex];
                        if (string.IsNullOrEmpty(upnSakiEdiMenberId))
                        {
                            continue;
                        }

                        var targetJigyousha = new SearchMasterDataDTOCls() { EDI_MEMBER_IDAry = new string[] { upnSakiEdiMenberId } };
                        var upnSakiJigyousha = this.DENSHI_JIGYOUSHA_SearchDao.GetDataForEntity(targetJigyousha);

                        if (upnSakiJigyousha == null || upnSakiJigyousha.Rows.Count < 1)
                        {
                            message += "\n加入者番号：" + upnSakiEdiMenberId;
                        }
                    }

                    if (!string.IsNullOrEmpty(message))
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUpnSakiJigyousha", ex1);
                messboxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUpnSakiJigyousha", ex);
                messboxShow("E245", "");
                catchErr = true;
            }
            return returnVal;
        }
        #endregion

        #endregion

        /// <summary>
        /// メッセージ表示する
        /// </summary>
        /// <param name="msgCD"></param>
        private DialogResult messboxShow(string msgCD, params string[] str)
        {
            var messageUtil = new MessageUtility();

            string tempMessage = messageUtil.GetMessage(msgCD).MESSAGE;

            for (int i = 0; i < str.Length; i++)
            {
                tempMessage = tempMessage.Replace("{" + i + "}", str[i]);
            }

            string strKubn = msgCD.Substring(0,1);

            if (strKubn =="Q")
            {
                return MessageBox.Show(tempMessage, Constans.WORNING_TITLE, MessageBoxButtons.YesNo,  MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button1);
            }
            else if (strKubn == "W")
            {
                return MessageBox.Show(tempMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (strKubn == "E")
            {
                return MessageBox.Show(tempMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (strKubn == "I")
            {
                return MessageBox.Show(tempMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return DialogResult.None;

        }

        public void insertAndUpdate()
        {

            LogUtility.DebugMethodStart();
            using (Transaction tran = new Transaction()) //トランザクション処理
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //SqlDateTime CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                SqlDateTime CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                SqlDateTime UPDATE_DATE = CREATE_DATE;
                string UPDATE_USER = SystemProperty.UserName;
                string UPDATE_PC = SystemInformation.ComputerName;
                foreach (DenshiManifestInfoCls DMInfo in listDMInfo)
                {
                    if (DMInfo.dt_mf_toc.CREATE_DATE.IsNull) {

                        DMInfo.dt_mf_toc.CREATE_DATE = CREATE_DATE;
                    }
                    DT_MF_TOCDao.Update(DMInfo.dt_mf_toc);

                    if (DMInfo.dt_mf_memberOld != null)
                    {
                        DT_MF_MEMBERDao.Update(DMInfo.dt_mf_memberOld);
                    }
                    if (DMInfo.dt_mf_member != null)
                    {
                        DMInfo.dt_mf_member.CREATE_DATE = CREATE_DATE;
                        DT_MF_MEMBERDao.Insert(DMInfo.dt_mf_member);
                    }

                    foreach (DT_R02 data in DMInfo.lstDT_R02)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R02Dao.Insert(data);
                    }
                    foreach (DT_R02 data in DMInfo.lstDT_R02Old)
                    {
                        DT_R02Dao.Update(data);
                    }

                    foreach (DT_R04 data in DMInfo.lstDT_R04)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R04Dao.Insert(data);
                    }
                    foreach (DT_R04 data in DMInfo.lstDT_R04Old)
                    {
                        DT_R04Dao.Update(data);
                    }

                    foreach (DT_R05 data in DMInfo.lstDT_R05)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R05Dao.Insert(data);
                    }
                    foreach (DT_R05 data in DMInfo.lstDT_R05Old)
                    {
                        DT_R05Dao.Update(data);
                    }

                    foreach (DT_R06 data in DMInfo.lstDT_R06)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R06Dao.Insert(data);
                    }
                    foreach (DT_R06 data in DMInfo.lstDT_R06Old)
                    {
                        DT_R06Dao.Update(data);
                    }

                    foreach (DT_R13 data in DMInfo.lstDT_R13)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R13Dao.Insert(data);
                    }
                    foreach (DT_R13 data in DMInfo.lstDT_R13Old)
                    {
                        DT_R13Dao.Update(data);
                    }

                    DMInfo.dt_r18.CREATE_DATE = CREATE_DATE;
                    DT_R18Dao.Insert(DMInfo.dt_r18);
                    if (DMInfo.dt_r18Old != null)
                    {
                        DT_R18Dao.Update(DMInfo.dt_r18Old);
                    }

                    foreach (DT_R19 data in DMInfo.lstDT_R19)
                    {
                        data.CREATE_DATE = CREATE_DATE;
                        DT_R19Dao.Insert(data);
                    }
                    foreach (DT_R19 data in DMInfo.lstDT_R19Old)
                    {
                        DT_R19Dao.Update(data);
                    }

                    if (DMInfo.dt_r18Ex != null)
                    {
                        if (DMInfo.dt_r18Ex.SYSTEM_ID == -1)
                        {
                            Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                            systemID = dba.createSystemIdWithTableLockNoTransaction((int)DENSHU_KBN.DENSHI_MANIFEST);
                        }
                        else
                        {
                            systemID = DMInfo.dt_r18Ex.SYSTEM_ID;
                        }
                    }

                    foreach (DT_R04_EX data in DMInfo.lstDT_R04_EX)
                    {
                        if (data.SYSTEM_ID == -1)
                        {
                            data.SYSTEM_ID = systemID;
                        }
                        DT_R04_EXDao.Insert(data);
                    }
                    foreach (DT_R04_EX data in DMInfo.lstDT_R04_EXOld)
                    {
                        data.UPDATE_DATE = UPDATE_DATE;
                        data.UPDATE_USER = UPDATE_USER;
                        data.UPDATE_PC = UPDATE_PC;
                        DT_R04_EXDao.Update(data);
                    }

                    foreach (DT_R13_EX data in DMInfo.lstDT_R13_EX)
                    {
                        if (data.SYSTEM_ID == -1)
                        {
                            data.SYSTEM_ID = systemID;
                        }
                        DT_R13_EXDao.Insert(data);
                    }
                    foreach (DT_R13_EX data in DMInfo.lstDT_R13_EXOld)
                    {
                        data.UPDATE_DATE = UPDATE_DATE;
                        data.UPDATE_USER = UPDATE_USER;
                        data.UPDATE_PC = UPDATE_PC;
                        DT_R13_EXDao.Update(data);
                    }

                    foreach (DT_R19_EX data in DMInfo.lstDT_R19_EX)
                    {
                        if (data.SYSTEM_ID == -1)
                        {
                            data.SYSTEM_ID = systemID;
                        }
                        DT_R19_EXDao.Insert(data);
                    }
                    foreach (DT_R19_EX data in DMInfo.lstDT_R19_EXOld)
                    {
                        data.UPDATE_DATE = UPDATE_DATE;
                        data.UPDATE_USER = UPDATE_USER;
                        data.UPDATE_PC = UPDATE_PC;
                        DT_R19_EXDao.Update(data);
                    }

                    if (DMInfo.dt_r18ExOld != null && !string.IsNullOrEmpty(DMInfo.dt_r18ExOld.SBN_HOUHOU_CD))
                    {
                        if (DMInfo.dt_r18Ex != null)
                        {
                            DMInfo.dt_r18Ex.SBN_HOUHOU_CD = DMInfo.dt_r18ExOld.SBN_HOUHOU_CD;
                        }
                    }

                    if (DMInfo.dt_r18Ex != null)
                    {
                        if (DMInfo.dt_r18Ex.SYSTEM_ID == -1)
                        {
                            DMInfo.dt_r18Ex.SYSTEM_ID = systemID;
                        }
                            DT_R18_EXDao.Insert(DMInfo.dt_r18Ex);
                    }
                    if (DMInfo.dt_r18ExOld != null)
                    {
                        DMInfo.dt_r18ExOld.UPDATE_DATE = UPDATE_DATE;
                        DMInfo.dt_r18ExOld.UPDATE_USER = UPDATE_USER;
                        DMInfo.dt_r18ExOld.UPDATE_PC = UPDATE_PC;
                        DT_R18_EXDao.Update(DMInfo.dt_r18ExOld);
                    }
                }
                tran.Commit();
            }

            LogUtility.DebugMethodEnd();
        }

        //マニフェスト目次情報[DT_MF_TOC]
        public void DT_MF_TOCsousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            DT_MF_TOC DT_MF_TOCDto = new DT_MF_TOC();
            //マニフェスト／予約番号
            DT_MF_TOCDto.MANIFEST_ID = manifestID;
            DT_MF_TOCDto = DT_MF_TOCDao.GetDataForEntity(DT_MF_TOCDto);
            if (DT_MF_TOCDto != null)
            {
                oldLatestSEQ = DT_MF_TOCDto.LATEST_SEQ;
                //最新SEQ
                DT_MF_TOCDto.LATEST_SEQ = oldLatestSEQ + 1;
                //状態フラグ
                if (strAry[1].Equals("3") || strAry[1].Equals("4"))
                {
                    DT_MF_TOCDto.STATUS_FLAG = 3;
                }
                else
                {
                    DT_MF_TOCDto.STATUS_FLAG = 4;
                }
                kanriID = DT_MF_TOCDto.KANRI_ID;
                updateFlg = true;
            }
            else
            {
                //管理番号の採番　
                DT_R18Dao.GetByJob(out kanriID);
                DT_MF_TOCDto = new DT_MF_TOC();
                DT_MF_TOCDto.KANRI_ID = kanriID;
                DT_MF_TOCDto = DT_MF_TOCDao.GetDataForEntity(DT_MF_TOCDto);
                if (DT_MF_TOCDto != null)
                {
                    //管理番号
                    DT_MF_TOCDto.KANRI_ID = kanriID;
                    //マニフェスト／予約番号
                    DT_MF_TOCDto.MANIFEST_ID = manifestID;
                    //最新SEQ
                    DT_MF_TOCDto.LATEST_SEQ = 1;
                    //状態フラグ
                    if (strAry[1].Equals("3") || strAry[1].Equals("4"))
                    {
                        DT_MF_TOCDto.STATUS_FLAG = 3;
                    }
                    else
                    {
                        DT_MF_TOCDto.STATUS_FLAG = 4;
                    }
                    //状態詳細フラグ
                    DT_MF_TOCDto.STATUS_DETAIL = 0;
                    //種類
                    DT_MF_TOCDto.KIND = 5;
                    //レコード作成日時
                    //DT_MF_TOCDto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                }
            }
            latestSEQ = DT_MF_TOCDto.LATEST_SEQ;
            DMInfo.dt_mf_toc=DT_MF_TOCDto;

            LogUtility.DebugMethodEnd();
        }

        //加入者番号[DT_MF_MEMBER]
        public void DT_MF_MEMBERsousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            DT_MF_MEMBER DT_MF_MEMBERDto = new DT_MF_MEMBER();
            DT_MF_MEMBERDto.KANRI_ID = kanriID;
            //排出事業者の加入者番号
            DT_MF_MEMBERDto.HST_MEMBER_ID = strAry[6];
            //収集運搬業者1加入者番号
            DT_MF_MEMBERDto.UPN1_MEMBER_ID = strAry[45];
            //収集運搬業者2加入者番号
            DT_MF_MEMBERDto.UPN2_MEMBER_ID = strAry[242];
            //収集運搬業者3加入者番号
            DT_MF_MEMBERDto.UPN3_MEMBER_ID = strAry[280];
            //収集運搬業者4加入者番号
            DT_MF_MEMBERDto.UPN4_MEMBER_ID = strAry[318];
            //収集運搬業者5加入者番号
            DT_MF_MEMBERDto.UPN5_MEMBER_ID = strAry[356];
            //処分業者の加入者番号
            DT_MF_MEMBERDto.SBN_MEMBER_ID = strAry[83];
            //レコード作成日時
            //DT_MF_MEMBERDto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

            DT_MF_MEMBER DT_MF_MEMBERDto2 = new DT_MF_MEMBER();
            DT_MF_MEMBERDto2.KANRI_ID = kanriID;

            DT_MF_MEMBERDto2 = DT_MF_MEMBERDao.GetDataForEntity(DT_MF_MEMBERDto2);
            if (DT_MF_MEMBERDto2 != null)
            {
                DT_MF_MEMBERDto.UPDATE_TS = DT_MF_MEMBERDto2.UPDATE_TS;
                DMInfo.dt_mf_memberOld=DT_MF_MEMBERDto;
            }
            else
            {
                DMInfo.dt_mf_member = DT_MF_MEMBERDto;
            }
            LogUtility.DebugMethodEnd();
        }

        //収集運搬情報[DT_R19]
        public void DT_R19sousa(string[] strAry,DenshiManifestInfoCls DMInfo){
            LogUtility.DebugMethodStart(strAry);

            if (updateFlg)
            {
                DT_R19 DT_R19DtoKey = new DT_R19();
                DT_R19DtoKey.KANRI_ID = kanriID;
                DT_R19DtoKey.SEQ = oldLatestSEQ;
                DT_R19[] DT_R19DtoAll = DT_R19Dao.GetDataForEntity(DT_R19DtoKey);

                if (DT_R19DtoAll != null && DT_R19DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R19DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R19Old.Add(DT_R19DtoAll[k]);
                    }
                }
            }

            for (int r19i = 0; r19i < 5; r19i++)
            {
                int celllength = 0;
                if (r19i > 0)
                {
                    celllength = 197;
                }
                else
                {
                    celllength = 38;
                }

                string str = strAry[44 + celllength + (r19i - 1) * 38];
                if ((r19i + 1) > unpankukanKubun)
                {
                    break;
                }

                if ("0".Equals(str))
                {
                    continue;
                }

                bool flg = false;
                int index = 44 + celllength + (r19i - 1) * 38;
                for (int i = 0; i < 38; i++)
                {
                    if (!string.IsNullOrWhiteSpace(strAry[index]))
                    {
                        flg = true;
                        break;
                    }
                    index++;
                }
                if (flg == false)
                {
                    continue;
                }

                DT_R19 DT_R19Dto = new DT_R19();
                //管理番号
                DT_R19Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R19Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R19Dto.SEQ = 1;
                }
                //マニフェスト／予約番号
                DT_R19Dto.MANIFEST_ID = manifestID;
                //区間番号
                DT_R19Dto.UPN_ROUTE_NO = (SqlInt16)r19i + 1;
                //収集運搬業者加入者番号
                DT_R19Dto.UPN_SHA_EDI_MEMBER_ID = strAry[45 + celllength + (r19i - 1) * 38];
                string gyoushaCD = "";
                string SearchSQL = "EDI_MEMBER_ID ='" + strAry[45 + celllength + (r19i - 1) * 38] + "'";
                DataRow[] drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    gyoushaCD = drArr[0]["GYOUSHA_CD"].ToString();
                }
                //収集運搬業者名
                DT_R19Dto.UPN_SHA_NAME = strAry[46 + celllength + (r19i - 1) * 38];
                //収集運搬業者郵便番号
                DT_R19Dto.UPN_SHA_POST = strAry[47 + celllength + (r19i - 1) * 38];

                if (!string.IsNullOrWhiteSpace(strAry[48 + celllength + (r19i - 1) * 38]))
                {
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(strAry[48 + celllength + (r19i - 1) * 38],
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                    //収集運搬業者所在地1
                    DT_R19Dto.UPN_SHA_ADDRESS1 = tempAddress1;
                    //収集運搬業者所在地2
                    DT_R19Dto.UPN_SHA_ADDRESS2 = tempAddress2;
                    //収集運搬業者所在地3
                    DT_R19Dto.UPN_SHA_ADDRESS3 = tempAddress3;
                    //収集運搬業者所在地4
                    DT_R19Dto.UPN_SHA_ADDRESS4 = tempAddress4;
                }

                //収集運搬業者電話番号
                DT_R19Dto.UPN_SHA_TEL = strAry[49 + celllength + (r19i - 1) * 38];
                //収集運搬業者FAX
                DT_R19Dto.UPN_SHA_FAX = strAry[50 + celllength + (r19i - 1) * 38];
                //収集運搬業者統一許可番号
                DT_R19Dto.UPN_SHA_KYOKA_ID = strAry[51 + celllength + (r19i - 1) * 38];
                //再委託先収集運搬業者加入者番号
                DT_R19Dto.SAI_UPN_SHA_EDI_MEMBER_ID = strAry[53 + celllength + (r19i - 1) * 38];
                //再委託先収集運搬業者名
                DT_R19Dto.SAI_UPN_SHA_NAME = strAry[54 + celllength + (r19i - 1) * 38];
                //再委託先収集運搬業者郵便番号
                DT_R19Dto.SAI_UPN_SHA_POST = strAry[55 + celllength + (r19i - 1) * 38];

                if(!string.IsNullOrWhiteSpace(strAry[56 + celllength + (r19i - 1) * 38]))
                {
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(strAry[56 + celllength + (r19i - 1) * 38],
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);

                    //再委託先収集運搬業者所在地1
                    DT_R19Dto.SAI_UPN_SHA_ADDRESS1 = tempAddress1;
                    //再委託先収集運搬業者所在地2
                    DT_R19Dto.SAI_UPN_SHA_ADDRESS2 = tempAddress2;
                    //再委託先収集運搬業者所在地3
                    DT_R19Dto.SAI_UPN_SHA_ADDRESS3 = tempAddress3;
                    //再委託先収集運搬業者所在地4
                    DT_R19Dto.SAI_UPN_SHA_ADDRESS4 = tempAddress4;
                }

                //再委託先収集運搬業者電話番号
                DT_R19Dto.SAI_UPN_SHA_TEL = strAry[57 + celllength + (r19i - 1) * 38];
                //再委託先収集運搬業者FAX
                DT_R19Dto.SAI_UPN_SHA_FAX = strAry[58 + celllength + (r19i - 1) * 38];
                //再委託先収集運搬業者統一許可番号
                DT_R19Dto.SAI_UPN_SHA_KYOKA_ID = strAry[59 + celllength + (r19i - 1) * 38];
                //運搬方法コード
                if (!UnpanhouhouMasterCheck(strAry[60 + celllength + (r19i - 1) * 38]))
                {
                    DT_R19Dto.UPN_WAY_CODE = strAry[60 + celllength + (r19i - 1) * 38];
                }
                //運搬担当者
                DT_R19Dto.UPN_TAN_NAME = strAry[62 + celllength + (r19i - 1) * 38];
                //車両番号
                DT_R19Dto.CAR_NO = strAry[63 + celllength + (r19i - 1) * 38];
                //運搬先加入者番号
                DT_R19Dto.UPNSAKI_EDI_MEMBER_ID = strAry[76 + celllength + (r19i - 1) * 38];

                // 運搬先事業者情報設定
                var targetJigyousha = new SearchMasterDataDTOCls() { EDI_MEMBER_IDAry = new string[] { DT_R19Dto.UPNSAKI_EDI_MEMBER_ID } };
                var upnSakiJigyousha = this.DENSHI_JIGYOUSHA_SearchDao.GetDataForEntity(targetJigyousha);

                //運搬先加入者名
                if (upnSakiJigyousha != null && upnSakiJigyousha.Rows.Count > 0
                    && upnSakiJigyousha.Rows[0]["JIGYOUSHA_NAME"] != null)
                {
                    DT_R19Dto.UPNSAKI_NAME = upnSakiJigyousha.Rows[0]["JIGYOUSHA_NAME"].ToString();
                }
                else if ((r19i + 1) == unpankukanKubun
                    && DT_R19Dto.UPNSAKI_EDI_MEMBER_ID.Equals(strAry[83].ToString()))
                {
                    // 最終区間は処分業者なので、処分業者名を設定する
                    DT_R19Dto.UPNSAKI_NAME = strAry[84].ToString();
                }
                else
                {
                    DT_R19Dto.UPNSAKI_NAME = string.Empty;
                }

                //運搬先事業場番号
                str = strAry[77 + celllength + (r19i - 1) * 38];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    DT_R19Dto.UPNSAKI_JOU_ID = SqlInt16.Parse(str);
                }
                //運搬先事業場区分
                if (r19i+1 < unpankukanKubun)
                {
                    DT_R19Dto.UPNSAKI_JOU_KBN = 1;
                }
                else if (r19i + 1 == unpankukanKubun)
                {
                    DT_R19Dto.UPNSAKI_JOU_KBN = 4;
                }
                //運搬先事業場名
                DT_R19Dto.UPNSAKI_JOU_NAME = strAry[78 + celllength + (r19i - 1) * 38];
                //運搬先事業場郵便番号
                DT_R19Dto.UPNSAKI_JOU_POST = strAry[79 + celllength + (r19i - 1) * 38];

                if (!string.IsNullOrWhiteSpace(strAry[80 + celllength + (r19i - 1) * 38]))
                {
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(strAry[80 + celllength + (r19i - 1) * 38],
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                    //運搬先事業場所在地1
                    DT_R19Dto.UPNSAKI_JOU_ADDRESS1 = tempAddress1;
                    //運搬先事業場所在地2
                    DT_R19Dto.UPNSAKI_JOU_ADDRESS2 = tempAddress2;
                    //運搬先事業場所在地3
                    DT_R19Dto.UPNSAKI_JOU_ADDRESS3 = tempAddress3;
                    //運搬先事業場所在地4
                    DT_R19Dto.UPNSAKI_JOU_ADDRESS4 = tempAddress4;
                }

                //運搬先事業場電話番号
                DT_R19Dto.UPNSAKI_JOU_TEL = strAry[81 + celllength + (r19i - 1) * 38];
                //運搬報告情報承認待ちフラグ 未指定
                //DT_R19Dto.UPN_SHOUNIN_FLAG = 0;
                //運搬終了日
                DT_R19Dto.UPN_END_DATE = hiduke8(strAry[64 + celllength + (r19i - 1) * 38]);
                //運搬報告記載の運搬担当者
                DT_R19Dto.UPNREP_UPN_TAN_NAME = strAry[66 + celllength + (r19i - 1) * 38];
                //運搬報告記載の車両番号
                DT_R19Dto.UPNREP_CAR_NO = strAry[74 + celllength + (r19i - 1) * 38];
                //運搬量
                str = strAry[68 + celllength + (r19i - 1) * 38];
                if (!IsNumericAndMinusCheck(str))
                {
                    DT_R19Dto.UPN_SUU = SqlDecimal.Parse(str);
                    DT_R19_YOUKOUSAIGO_UPN_SUU = DT_R19Dto.UPN_SUU;
                }
                //運搬量の単位コード
                if (!TannyiMasterCheck(strAry[69 + celllength + (r19i - 1) * 38]))
                {
                    DT_R19Dto.UPN_UNIT_CODE = strAry[69 + celllength + (r19i - 1) * 38];
                }
                //有価物拾集量
                str = strAry[71 + celllength + (r19i - 1) * 38];
                if (!IsNumericAndMinusCheck(str))
                {
                    DT_R19Dto.YUUKA_SUU = SqlDecimal.Parse(str);
                }
                //有価物拾集量の単位コード
                if (!TannyiMasterCheck(strAry[72 + celllength + (r19i - 1) * 38]))
                {
                    DT_R19Dto.YUUKA_UNIT_CODE = strAry[72 + celllength + (r19i - 1) * 38];
                }
                //報告担当者
                DT_R19Dto.REP_TAN_NAME = strAry[67 + celllength + (r19i - 1) * 38];
                //備考
                DT_R19Dto.BIKOU = strAry[75 + celllength + (r19i - 1) * 38];
                //レコード作成日時
                //DT_R19Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R19.Add(DT_R19Dto);
            }
            LogUtility.DebugMethodEnd();
        }

        //電子CSV．有害物質n（コード）が設定されているものが登録対象（最大6件）
        //有害物質情報[DT_R02]
        public void DT_R02sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            if (updateFlg)
            {
                DT_R02 DT_R02DtoKey = new DT_R02();
                DT_R02DtoKey.KANRI_ID = kanriID;
                DT_R02DtoKey.SEQ = oldLatestSEQ;
                DT_R02[] DT_R02DtoAll = DT_R02Dao.GetDataForEntity(DT_R02DtoKey);

                if (DT_R02DtoAll != null && DT_R02DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R02DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R02Old.Add(DT_R02DtoAll[k]);
                    }
                }
            }

            //List<DT_R02> DT_R02DtoList = new List<DT_R02>();
            for (int k = 0; k < 6; k++)
            {
                if (string.IsNullOrWhiteSpace(strAry[29 + k * 2]) && DenshiyuugaibusshitsuMasterCheck(strAry[28 + k * 2]))
                {
                    continue;
                }
                DT_R02 DT_R02Dto = new DT_R02();
                //管理番号
                DT_R02Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R02Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R02Dto.SEQ = 1;
                }
                //レコード連番
                DT_R02Dto.REC_SEQ = (SqlInt16)(k + 1);
                //マニフェスト／予約番号
                DT_R02Dto.MANIFEST_ID = manifestID;
                //有害物質コード
                if (!DenshiyuugaibusshitsuMasterCheck(strAry[28 + k * 2]))
                {
                    DT_R02Dto.YUUGAI_CODE = strAry[28 + k * 2];
                }
                //有害物質名
                DT_R02Dto.YUUGAI_NAME = strAry[29 + k * 2];
                //レコード作成日時 自動設定
                //DT_R02Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R02.Add(DT_R02Dto);
            }
            LogUtility.DebugMethodEnd();
        }

        //電子CSV．［最終処分事業場 予定n］最終処分事業場の名称が設定されているものが登録対象（最大10件）
        //最終処分事業場(予定)情報[DT_R04]
        public void DT_R04sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            if (updateFlg)
            {
                DT_R04 DT_R04DtoKey = new DT_R04();
                DT_R04DtoKey.KANRI_ID = kanriID;
                DT_R04DtoKey.SEQ = oldLatestSEQ;
                DT_R04[] DT_R04DtoAll = DT_R04Dao.GetDataForEntity(DT_R04DtoKey);

                if (DT_R04DtoAll != null && DT_R04DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R04DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R04Old.Add(DT_R04DtoAll[k]);
                    }
                }
            }

            //List<DT_R04> DT_R04DtoList = new List<DT_R04>();
            for (int k = 0; k < 10; k++)
            {
                if (string.IsNullOrWhiteSpace(strAry[118 + k * 4]) && string.IsNullOrWhiteSpace(strAry[119 + k * 4])
                    && string.IsNullOrWhiteSpace(strAry[120 + k * 4]) && string.IsNullOrWhiteSpace(strAry[121 + k * 4]))
                {
                    continue;
                }
                DT_R04 DT_R04Dto = new DT_R04();
                //管理番号
                DT_R04Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R04Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R04Dto.SEQ = 1;
                }
                //レコード連番
                DT_R04Dto.REC_SEQ = (SqlInt16)(k + 1);
                //マニフェスト／予約番号
                DT_R04Dto.MANIFEST_ID = manifestID;
                //最終処分事業場名称
                DT_R04Dto.LAST_SBN_JOU_NAME = strAry[118 + k * 4];
                //最終処分事業場所在地郵便番号
                DT_R04Dto.LAST_SBN_JOU_POST = strAry[119 + k * 4];

                if (!string.IsNullOrWhiteSpace(strAry[120 + k * 4]))
                {
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(strAry[120 + k * 4].ToString(),
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                    //最終処分事業場所在地１
                    DT_R04Dto.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                    //最終処分事業場所在地２
                    DT_R04Dto.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                    //最終処分事業場所在地３
                    DT_R04Dto.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                    //最終処分事業場所在地４
                    DT_R04Dto.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                }

                //最終処分事業場電話番号
                DT_R04Dto.LAST_SBN_JOU_TEL = strAry[121 + k * 4];
                //レコード作成日時 自動設定
                //DT_R04Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R04.Add(DT_R04Dto);
                yoteiYoukouKunkan = k;
            }
            LogUtility.DebugMethodEnd();
        }

        //電子CSV．連絡番号1～3が設定されているものが登録対象（最大3件）
        //連絡番号情報[DT_R05]
        public void DT_R05sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            if (updateFlg)
            {
                DT_R05 DT_R05DtoKey = new DT_R05();
                DT_R05DtoKey.KANRI_ID = kanriID;
                DT_R05DtoKey.SEQ = oldLatestSEQ;
                DT_R05[] DT_R05DtoAll = DT_R05Dao.GetDataForEntity(DT_R05DtoKey);

                if (DT_R05DtoAll != null && DT_R05DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R05DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R05Old.Add(DT_R05DtoAll[k]);
                    }
                }
            }

            for (int k = 0; k < 3; k++)
            {
                if (string.IsNullOrWhiteSpace(strAry[2 + k]))
                {
                    continue;
                }

                DT_R05 DT_R05Dto = new DT_R05();
                //管理番号
                DT_R05Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R05Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R05Dto.SEQ = 1;
                }
                //マニフェスト／予約番号	
                DT_R05Dto.MANIFEST_ID = manifestID;
                //連絡番号No	
                DT_R05Dto.RENRAKU_ID_NO = (SqlInt16)(k + 1);
                //連絡番号	
                DT_R05Dto.RENRAKU_ID = strAry[2 + k];
                //レコード作成日時 自動設定
                //DT_R05Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R05.Add(DT_R05Dto);
            }
            LogUtility.DebugMethodEnd();
        }

        //電子CSV．備考1～5が設定されているものが登録対象（最大5件）
        //備考情報[DT_R06]
        public void DT_R06sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            if (updateFlg)
            {
                DT_R06 DT_R06DtoKey = new DT_R06();
                DT_R06DtoKey.KANRI_ID = kanriID;
                DT_R06DtoKey.SEQ = oldLatestSEQ;
                DT_R06[] DT_R06DtoAll = DT_R06Dao.GetDataForEntity(DT_R06DtoKey);

                if (DT_R06DtoAll != null && DT_R06DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R06DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R06Old.Add(DT_R06DtoAll[k]);
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                if (string.IsNullOrWhiteSpace(strAry[235 + k]))
                {
                    continue;
                }

                DT_R06 DT_R06Dto = new DT_R06();
                //管理番号
                DT_R06Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R06Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R06Dto.SEQ = 1;
                }
                //マニフェスト／予約番号	
                DT_R06Dto.MANIFEST_ID = manifestID;
                //備考No	
                DT_R06Dto.BIKOU_NO = (SqlInt16)(k + 1);
                //備考	
                DT_R06Dto.BIKOU = strAry[235 + k];
                //レコード作成日時 自動設定
                //DT_R06Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R06.Add(DT_R06Dto);
            }
            LogUtility.DebugMethodEnd();
        }

        //電子CSV．［最終処分事業場 実績1～15］最終処分終了日【処分報告】が設定されているものが登録対象（最大15件）
        //最終処分終了日・事業場情報[DT_R13]
        public void DT_R13sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);
            
            if (updateFlg)
            {
                DT_R13 DT_R13DtoKey = new DT_R13();
                DT_R13DtoKey.KANRI_ID = kanriID;
                DT_R13DtoKey.SEQ = oldLatestSEQ;
                DT_R13[] DT_R13DtoAll = DT_R13Dao.GetDataForEntity(DT_R13DtoKey);

                if (DT_R13DtoAll != null && DT_R13DtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R13DtoAll.Length; k++)
                    {
                        DMInfo.lstDT_R13Old.Add(DT_R13DtoAll[k]);
                    }
                }
            }

            //List<DT_R13> DT_R13DtoList = new List<DT_R13>();
            for (int k = 0; k < 15; k++)
            {
                if (string.IsNullOrEmpty(strAry[160 + 5 * k]) && string.IsNullOrEmpty(strAry[161 + 5 * k])
                    && string.IsNullOrEmpty(strAry[162 + 5 * k]) && string.IsNullOrEmpty(strAry[163 + 5 * k])
                    && string.IsNullOrEmpty(strAry[164 + 5 * k]))
                {
                    break;
                }
                DT_R13 DT_R13Dto = new DT_R13();
                //管理番号
                DT_R13Dto.KANRI_ID = kanriID;
                if (updateFlg == true)
                {
                    DT_R13Dto.SEQ = latestSEQ;
                }
                else
                {
                    //枝番
                    DT_R13Dto.SEQ = 1;
                }

                //レコード連番
                DT_R13Dto.REC_SEQ = (SqlInt16)(k + 1);
                //マニフェスト／予約番号
                DT_R13Dto.MANIFEST_ID = manifestID;
                //最終処分事業場名称
                DT_R13Dto.LAST_SBN_JOU_NAME = strAry[161 + 5 * k];
                //最終処分事業場所在地の郵便番号
                DT_R13Dto.LAST_SBN_JOU_POST = strAry[162 + 5 * k];

                if (!string.IsNullOrWhiteSpace(strAry[163 + 5 * k]))
                {
                    var maniLogic = new ManifestoLogic();
                    string tempAddress1;
                    string tempAddress2;
                    string tempAddress3;
                    string tempAddress4;
                    maniLogic.SetAddress1ToAddress4(strAry[163 + 5 * k].ToString(),
                    out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                    //最終処分事業場所在地１
                    DT_R13Dto.LAST_SBN_JOU_ADDRESS1 = tempAddress1;
                    //最終処分事業場所在地２
                    DT_R13Dto.LAST_SBN_JOU_ADDRESS2 = tempAddress2;
                    //最終処分事業場所在地３
                    DT_R13Dto.LAST_SBN_JOU_ADDRESS3 = tempAddress3;
                    //最終処分事業場所在地４
                    DT_R13Dto.LAST_SBN_JOU_ADDRESS4 = tempAddress4;
                }

                //最終処分事業場電話番号
                DT_R13Dto.LAST_SBN_JOU_TEL = strAry[164 + 5 * k];
                //最終処分終了日
                DT_R13Dto.LAST_SBN_END_DATE = hiduke8(strAry[160 + 5 * k]);
                //レコード作成日時 自動設定
                //DT_R13Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                DMInfo.lstDT_R13.Add(DT_R13Dto);
                jissekiYoukouKunkan = k;
            }
            LogUtility.DebugMethodEnd();
        }

        //マニフェスト情報[DT_R18]
        public void DT_R18sousa(string[] strAry,DenshiManifestInfoCls DMInfo)
        {
            LogUtility.DebugMethodStart(strAry);

            DT_R18 DT_R18Dto = new DT_R18();
            DT_R18Dto.KANRI_ID = kanriID;

            if (updateFlg)
            {
                DT_R18Dto.SEQ = oldLatestSEQ;
                DT_R18Dto = DT_R18Dao.GetDataForEntity(DT_R18Dto);
                if (DT_R18Dto != null)
                {
                    DMInfo.dt_r18Old = DT_R18Dto;
                }
            }

            DT_R18Dto = new DT_R18();
            DT_R18Dto.KANRI_ID = kanriID;

            DT_R18Dto.SEQ = latestSEQ;
            //マニフェスト／予約番号
            DT_R18Dto.MANIFEST_ID = manifestID;
            //予約/ﾏﾆﾌｪｽﾄ区分
            string str = strAry[1];
            if ("3".Equals(str) || "4".Equals(str))
            {
                DT_R18Dto.MANIFEST_KBN = 1;
            }
            else if ("1".Equals(str) || "2".Equals(str) || "5".Equals(str))
            {
                DT_R18Dto.MANIFEST_KBN = 2;
            }
            //登録情報承認待ちフラグ
            DT_R18Dto.SHOUNIN_FLAG = 0;
            //引渡し日
            DT_R18Dto.HIKIWATASHI_DATE = (strAry[5]).Replace("/", "");
            //運搬終了報告済フラグ
            DT_R18Dto.UPN_ENDREP_FLAG = 0;
            str = strAry[240];
            if ("1".Equals(str))
            {
                if (!string.IsNullOrWhiteSpace(hiduke8(strAry[64])))
                {
                    DT_R18Dto.UPN_ENDREP_FLAG = 1;
                }
            }
            else if ("2".Equals(str))
            {
                if (!string.IsNullOrWhiteSpace(hiduke8(strAry[261])))
                {
                    DT_R18Dto.UPN_ENDREP_FLAG = 1;
                }
            }
            else if ("3".Equals(str))
            {
                if (!string.IsNullOrWhiteSpace(hiduke8(strAry[299])))
                {
                    DT_R18Dto.UPN_ENDREP_FLAG = 1;
                }
            }
            else if ("4".Equals(str))
            {
                if (!string.IsNullOrWhiteSpace(hiduke8(strAry[337])))
                {
                    DT_R18Dto.UPN_ENDREP_FLAG = 1;
                }
            }
            else if ("5".Equals(str))
            {
                if (!string.IsNullOrWhiteSpace(strAry[375]))
                {
                    DT_R18Dto.UPN_ENDREP_FLAG = 1;
                }
            }
            //処分終了報告済フラグ
            if (!string.IsNullOrWhiteSpace(strAry[106]))
            {
                DT_R18Dto.SBN_ENDREP_FLAG = 1;
            }
            //最終処分終了報告済フラグ
            if (!string.IsNullOrWhiteSpace(strAry[158]))
            {
                DT_R18Dto.LAST_SBN_ENDREP_FLAG = 1;
            }
            //課金日
            DT_R18Dto.KAKIN_DATE = hiduke8(strAry[399]);
            //登録日
            DT_R18Dto.REGI_DATE = hiduke8((strAry[395]));
            //運搬・処分終了報告期限日 未指定
            //DT_R18Dto.UPN_SBN_REP_LIMIT_DATE = "";
            //最終処分終了報告期限日 未指定
            //DT_R18Dto.LAST_SBN_REP_LIMIT_DATE = "";
            //予約情報有効期限日 未指定
            //DT_R18Dto.RESV_LIMIT_DATE = "";
            //処分終了報告区分
            if ("1".Equals(strAry[105]) || "2".Equals(strAry[105]))
            {
                DT_R18Dto.SBN_ENDREP_KBN = SqlInt16.Parse(strAry[105]);
            }
            //排出事業者の加入者番号
            DT_R18Dto.HST_SHA_EDI_MEMBER_ID = strAry[6];
            //排出事業者名称
            DT_R18Dto.HST_SHA_NAME = strAry[7];
            //排出事業者郵便番号
            DT_R18Dto.HST_SHA_POST = strAry[8];

            if (!string.IsNullOrWhiteSpace(strAry[9]))
            {
                var maniLogic = new ManifestoLogic();
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                maniLogic.SetAddress1ToAddress4(strAry[9].ToString(),
                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                //排出事業者所在地1
                DT_R18Dto.HST_SHA_ADDRESS1 = tempAddress1;
                //排出事業者所在地2
                DT_R18Dto.HST_SHA_ADDRESS2 = tempAddress2;
                //排出事業者所在地3
                DT_R18Dto.HST_SHA_ADDRESS3 = tempAddress3;
                //排出事業者所在地4
                DT_R18Dto.HST_SHA_ADDRESS4 = tempAddress4;
            }
            //排出事業者の代表番号
            DT_R18Dto.HST_SHA_TEL = strAry[10];
            //排出事業者の代表FAX
            DT_R18Dto.HST_SHA_FAX = strAry[11];
            //排出事業場名称
            DT_R18Dto.HST_JOU_NAME = strAry[12];
            //排出事業場所在地の郵便番号
            DT_R18Dto.HST_JOU_POST_NO = strAry[13];

            if (!string.IsNullOrWhiteSpace(strAry[14]))
            {
                var maniLogic = new ManifestoLogic();
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                maniLogic.SetAddress1ToAddress4(strAry[14].ToString(),
                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                //排出事業場所在地1
                DT_R18Dto.HST_JOU_ADDRESS1 = tempAddress1;
                //排出事業場所在地2
                DT_R18Dto.HST_JOU_ADDRESS2 = tempAddress2;
                //排出事業場所在地3
                DT_R18Dto.HST_JOU_ADDRESS3 = tempAddress3;
                //排出事業場所在地4
                DT_R18Dto.HST_JOU_ADDRESS4 = tempAddress4;
            }

            //排出事業場電話番号
            DT_R18Dto.HST_JOU_TEL = strAry[15];
            //登録担当者
            DT_R18Dto.REGI_TAN = strAry[17];
            //引渡し担当者
            DT_R18Dto.HIKIWATASHI_TAN_NAME = strAry[16];
            //大分類コード
            DT_R18Dto.HAIKI_DAI_CODE = strAry[18].ToString().Substring(0, 2);
            //中分類コード
            DT_R18Dto.HAIKI_CHU_CODE = strAry[18].ToString().Substring(2, 1);
            //小分類コード
            DT_R18Dto.HAIKI_SHO_CODE = strAry[18].ToString().Substring(3, 1);
            //細分類コード
            DT_R18Dto.HAIKI_SAI_CODE = strAry[18].ToString().Substring(4, 3);
            //廃棄物の大分類名称
            DT_R18Dto.HAIKI_BUNRUI = strAry[19];
            //廃棄物の種類
            DT_R18Dto.HAIKI_SHURUI = strAry[20];
            //廃棄物の名称

            if (!DensihaikinameMasterCheck(strAry[6],strAry[24]))
            {
                DT_R18Dto.HAIKI_NAME = strAry[24];
            }
            //廃棄物の数量
            DT_R18Dto.HAIKI_SUU = SqlDecimal.Parse(strAry[21]);
            //廃棄物の数量単位コード
            DT_R18Dto.HAIKI_UNIT_CODE = strAry[22];
            //数量確定者コード
            DT_R18Dto.SUU_KAKUTEI_CODE = strAry[40];
            //廃棄物の確定数量
            if (!IsNumericAndMinusCheck(strAry[41]))
            {
                DT_R18Dto.HAIKI_KAKUTEI_SUU = SqlDecimal.Parse(strAry[41]);
            }
            //廃棄物の確定数量の単位コード
            if (!TannyiMasterCheck(strAry[42]))
            {
                DT_R18Dto.HAIKI_KAKUTEI_UNIT_CODE = strAry[42];
            }
            //荷姿コード
            DT_R18Dto.NISUGATA_CODE = strAry[25];
            //荷姿名
            DT_R18Dto.NISUGATA_NAME = strAry[26];
            //荷姿の数量
            if (!IsNumericAndMinusCheck(strAry[27]))
            {
                DT_R18Dto.NISUGATA_SUU = strAry[27];
            }
            //処分業者加入者番号
            DT_R18Dto.SBN_SHA_MEMBER_ID = strAry[83];
            //処分業者名
            DT_R18Dto.SBN_SHA_NAME = strAry[84];
            //処分業者郵便番号
            DT_R18Dto.SBN_SHA_POST = strAry[85];

            if (!string.IsNullOrWhiteSpace(strAry[86]))
            {
                var maniLogic = new ManifestoLogic();
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                maniLogic.SetAddress1ToAddress4(strAry[86].ToString(),
                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                //処分業者所在地1
                DT_R18Dto.SBN_SHA_ADDRESS1 = tempAddress1;
                //処分業者所在地2
                DT_R18Dto.SBN_SHA_ADDRESS2 = tempAddress2;
                //処分業者所在地3
                DT_R18Dto.SBN_SHA_ADDRESS3 = tempAddress3;
                //処分業者所在地4
                DT_R18Dto.SBN_SHA_ADDRESS4 = tempAddress4;
            }

            //処分業者電話番号
            DT_R18Dto.SBN_SHA_TEL = strAry[87];
            //処分業者FAX
            DT_R18Dto.SBN_SHA_FAX = strAry[88];
            //処分業者統一許可番号
            DT_R18Dto.SBN_SHA_KYOKA_ID = strAry[89];
            //再委託先処分業者加入者番号
            DT_R18Dto.SAI_SBN_SHA_MEMBER_ID = strAry[96];
            //再委託先処分業者名
            DT_R18Dto.SAI_SBN_SHA_NAME = strAry[97];
            //再委託先処分業者郵便場号
            DT_R18Dto.SAI_SBN_SHA_POST = strAry[98];

            if (!string.IsNullOrWhiteSpace(strAry[99]))
            {
                var maniLogic = new ManifestoLogic();
                string tempAddress1;
                string tempAddress2;
                string tempAddress3;
                string tempAddress4;
                maniLogic.SetAddress1ToAddress4(strAry[99].ToString(),
                out tempAddress1, out tempAddress2, out tempAddress3, out tempAddress4);
                //再委託先処分業者所在地1
                DT_R18Dto.SAI_SBN_SHA_ADDRESS1 = tempAddress1;
                //再委託先処分業者所在地2
                DT_R18Dto.SAI_SBN_SHA_ADDRESS2 = tempAddress2;
                //再委託先処分業者所在地3
                DT_R18Dto.SAI_SBN_SHA_ADDRESS3 = tempAddress3;
                //再委託先処分業者所在地4
                DT_R18Dto.SAI_SBN_SHA_ADDRESS4 = tempAddress4;
            }

            //再委託先処分業者電話番号
            DT_R18Dto.SAI_SBN_SHA_TEL = strAry[100];
            //再委託先処分業者FAX
            DT_R18Dto.SAI_SBN_SHA_FAX = strAry[101];
            //再委託先処分業者統一許可番号
            DT_R18Dto.SAI_SBN_SHA_KYOKA_ID = strAry[102];
            //処分方法コード
            if (!ShobunhouhouMasterCheck(strAry[103]))
            {
                DT_R18Dto.SBN_WAY_CODE = SqlInt16.Parse(strAry[103]);
            }
            //処分方法名
            DT_R18Dto.SBN_WAY_NAME = strAry[104];
            //処分報告情報承認待ちフラグ 未指定
            //DT_R18Dto.SBN_SHOUNIN_FLAG = 0;
            //処分終了日
            DT_R18Dto.SBN_END_DATE = hiduke8(strAry[106]);
            //廃棄物の受領日
            DT_R18Dto.HAIKI_IN_DATE = hiduke8(strAry[110]);
            //受入量
            if (!IsNumericAndMinusCheck(strAry[113]))
            {
                DT_R18Dto.RECEPT_SUU = SqlDecimal.Parse(strAry[113]);
            }
            //受入量の単位コード
            if (!TannyiMasterCheck(strAry[114]))
            {
                DT_R18Dto.RECEPT_UNIT_CODE = strAry[114];
            }
            //運搬担当者
            DT_R18Dto.UPN_TAN_NAME = strAry[111];
            //車両番号
            string gyoushaCD="";
            string SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            DataRow[] drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                gyoushaCD = drArr[0]["GYOUSHA_CD"].ToString();
            }

            DT_R18Dto.CAR_NO = strAry[112];

            //報告担当者
            DT_R18Dto.REP_TAN_NAME = strAry[109];
            //処分担当者
            DT_R18Dto.SBN_TAN_NAME = strAry[108];
            //処分終了報告日
            DT_R18Dto.SBN_END_REP_DATE = hiduke8(strAry[107]);
            //処分報告備考
            DT_R18Dto.SBN_REP_BIKOU = strAry[116];
            //予約登録の修正権限コード
            if (!KunbunCheck(strAry[393], "修正許可"))
            {
                if (!IsNumericAndMinusCheck(strAry[393]))
                {
                    DT_R18Dto.KENGEN_CODE = SqlInt16.Parse(strAry[393]);
                }
                else
                {
                    DT_R18Dto.KENGEN_CODE = SqlInt16.Null;
                }
            }
            //最終処分事業場記載フラグ
            if ("0".Equals(strAry[117]) || "1".Equals(strAry[117]))
            {
                DT_R18Dto.LAST_SBN_JOU_KISAI_FLAG = strAry[117];
            }
            //中間処理産業廃棄物情報管理方法フラグ（1:当欄指定の通り、2:１次不要、3:帳簿記載の通り）
            string sbnGyoushaHeader = strAry[6].Substring(0, 1);
            if (sbnGyoushaHeader.Equals("D"))
            {
                // デモ版の場合
                sbnGyoushaHeader = strAry[6].Substring(1, 1);
            }
            if (sbnGyoushaHeader.Equals("3"))
            {
                // 排出事業者が処分事業者の場合、中間処理産業廃棄物情報管理方法フラグ=3:帳簿記載の通り→２次マニ
                DT_R18Dto.FIRST_MANIFEST_FLAG = "3";
            }
            else
            {
                // 排出事業者が処分事業者でない場合、中間処理産業廃棄物情報管理方法フラグ=ブランク→１次マニ
                DT_R18Dto.FIRST_MANIFEST_FLAG = "";
            }            
            //最終処分終了日
            DT_R18Dto.LAST_SBN_END_DATE = hiduke8(strAry[158]);
            //最終処分終了報告日
            DT_R18Dto.LAST_SBN_END_REP_DATE = hiduke8(strAry[159]);
            //修正日
            DT_R18Dto.SHUSEI_DATE = hiduke8(strAry[398]);
            //取消フラグ
            str = strAry[1];
            if ("1".Equals(str) || "3".Equals(str) || "5".Equals(str))
            {
                DT_R18Dto.CANCEL_FLAG = 0;
            }
            else if ("2".Equals(str) || "4".Equals(str))
            {
                DT_R18Dto.CANCEL_FLAG = 1;
            }
            //取消日
            DT_R18Dto.CANCEL_DATE = hiduke8(strAry[397]);
            //最終更新日
            DT_R18Dto.LAST_UPDATE_DATE = hiduke8(strAry[400]);
            //有害物質情報件数
            DT_R18Dto.YUUGAI_CNT = DMInfo.lstDT_R02.Count;        // "有害物質情報[DT_R02]の登録件数
            //収集運搬情報件数
            DT_R18Dto.UPN_ROUTE_CNT = DMInfo.lstDT_R19.Count;     //収集運搬情報[DT_R19]の登録件数
            //最終処分事業場（予定）情報件数
            DT_R18Dto.LAST_SBN_PLAN_CNT = DMInfo.lstDT_R04.Count; //最終処分事業場(予定)情報[DT_R04]の登録件数
            //最終処分終了日･事業場情報件数
            DT_R18Dto.LAST_SBN_CNT = DMInfo.lstDT_R13.Count;      //最終処分終了日・事業場情報[DT_R13]の登録件数
            //連絡番号情報件数
            DT_R18Dto.RENRAKU_CNT = DMInfo.lstDT_R05.Count;       //連絡番号情報[DT_R05]の登録件数
            //備考情報件数
            DT_R18Dto.BIKOU_CNT = DMInfo.lstDT_R06.Count;         //備考情報[DT_R06]の登録件数
            //中間処理産業廃棄物情報件数
            if (!string.IsNullOrWhiteSpace(strAry[394]))
            {
                DT_R18Dto.FIRST_MANIFEST_CNT = SqlInt16.Parse(strAry[394]);
            }
            //レコード作成日時
            //DT_R18Dto.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

            DMInfo.dt_r18 = DT_R18Dto;
            LogUtility.DebugMethodEnd();
        }

        //電子マニフェスト基本拡張[DT_R18_EX] DT_R18と同レコード数
        public void DT_R18_EXsousa(string[] strAry, DenshiManifestInfoCls DMInfo, bool denshiCheckFlg = true)
        {
            LogUtility.DebugMethodStart(strAry);

            DT_R18_EX DT_R18_EXDto = new DT_R18_EX();
            if (updateFlg)
            {
                DT_R18_EX DT_R18_EXDto1 = new DT_R18_EX();
                DT_R18_EXDto1.KANRI_ID = kanriID;
                DT_R18_EXDto1.DELETE_FLG = false;
                DT_R18_EX DT_R18_EXDtoS = DT_R18_EXDao.GetDataForEntity(DT_R18_EXDto1);

                if (DT_R18_EXDtoS != null)
                {
                    //削除フラグ
                    DT_R18_EXDtoS.DELETE_FLG = true;
                    DMInfo.dt_r18ExOld = DT_R18_EXDtoS;
                    systemID = DT_R18_EXDtoS.SYSTEM_ID;
                    oldEXlatestSEQ = DT_R18_EXDtoS.SEQ;
                    EXlatestSEQ = DT_R18_EXDtoS.SEQ + 1;
                }
                else if (denshiCheckFlg)
                {
                    //システムIDの採番
                    //Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                    //systemID = dba.createSystemIdWithTableLock((int)DENSHU_KBN.DENSHI_MANIFEST);
                    systemID = -1;
                    EXlatestSEQ = 1;
                }
            }
            else if (denshiCheckFlg)
            {
                //システムIDの採番
                //Common.BusinessCommon.DBAccessor dba = new Common.BusinessCommon.DBAccessor();
                //systemID = dba.createSystemIdWithTableLock((int)DENSHU_KBN.DENSHI_MANIFEST);
                systemID = -1;
                EXlatestSEQ = 1;
            }

            if (!denshiCheckFlg)
                return;

            DataTable dt = new DataTable();

            DT_R18_EXDto.SYSTEM_ID = systemID;
            //枝番
            if (updateFlg == true)
            {
                DT_R18_EXDto.SEQ = EXlatestSEQ;
            }
            else
            {
                //枝番
                DT_R18_EXDto.SEQ = 1;
            }
            //管理番号
            DT_R18_EXDto.KANRI_ID = kanriID;
            //マニフェスト／予約番号
            DT_R18_EXDto.MANIFEST_ID = manifestID;

            //排出事業者CD
            string SearchSQL = "EDI_MEMBER_ID ='" + strAry[6] + "'";
            DataRow[] drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
            if (drArr.Length == 1)
            {
                DT_R18_EXDto.HST_GYOUSHA_CD = drArr[0]["GYOUSHA_CD"].ToString();
            }

            //排出事業場CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[6] + "'";
            SearchSQL += " AND JIGYOUJOU_NAME ='" + strAry[12] + "'";
            SearchSQL += " AND JIGYOUJOU_ADDRESS ='" + strAry[14] + "'";
            drArr = MstDataInfo.denshiJgyoujoTb.Select(SearchSQL);//
            if (drArr.Length == 1)
            {
                DT_R18_EXDto.HST_GENBA_CD = drArr[0]["GENBA_CD"].ToString();
            }
            //処分受託者CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.SBN_GYOUSHA_CD = drArr[0]["GYOUSHA_CD"].ToString();
            }

            //処分事業場CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            SearchSQL += " AND JIGYOUJOU_NAME ='" + strAry[91] + "'";
            SearchSQL += " AND JIGYOUJOU_ADDRESS ='" + strAry[93] + "'";
            drArr = MstDataInfo.denshiJgyoujoTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.SBN_GENBA_CD = drArr[0]["GENBA_CD"].ToString();
            }

            //報告不要処分事業者加入者番号
            SqlInt16 i = SqlInt16.Parse(strAry[82].ToString());
            if (i == 0)
            {
            }
            else if (i == 2)
            {
                DT_R18_EXDto.NO_REP_SBN_EDI_MEMBER_ID = "0000000";
            }
            else if (i == 1 || i == 3)
            {
                DT_R18_EXDto.NO_REP_SBN_EDI_MEMBER_ID = strAry[83].ToString();
            }

            //廃棄物名称CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[6] + "'";
            SearchSQL += " AND HAIKI_NAME ='" + strAry[24] + "'";
            drArr = MstDataInfo.denshiHakkiNameTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.HAIKI_NAME_CD = drArr[0]["HAIKI_NAME_CD"].ToString();
            }

            //処分方法CD
            if (!ShobunhouhouMasterCheck(strAry[103]))
            {
                DT_R18_EXDto.SBN_HOUHOU_CD = strAry[103];
            }
            //報告担当者CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            SearchSQL += " AND TANTOUSHA_KBN ='4'";
            SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[109] + "'";

            drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.HOUKOKU_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
            }

            //処分担当者CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            SearchSQL += " AND TANTOUSHA_KBN ='5'";
            SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[108] + "'";

            drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.SBN_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
            }

            //運搬担当者CD
            SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
            SearchSQL += " AND TANTOUSHA_KBN ='3'";
            SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[111] + "'";

            drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.UPN_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
            }

            //車輌CD
            SearchSQL = "SHARYOU_NAME ='" + strAry[112] + "'";
            SearchSQL += " AND GYOUSHA_CD ='" + DT_R18_EXDto.SBN_GYOUSHA_CD + "'";
            drArr = MstDataInfo.sharryouTb.Select(SearchSQL);//
            if (drArr.Length >= 1)
            {
                DT_R18_EXDto.SHARYOU_CD = drArr[0]["SHARYOU_CD"].ToString();
            }

            //換算後数量 ※換算値計算シート参照
            SqlDecimal suuryou = -1;
            string unitCd = string.Empty;
            bool isEmptySuuryou = true;
            M_SYS_INFO M_SYS_INFODto = new M_SYS_INFO();
            string str = M_SYS_INFODao.GetDataForEntity(M_SYS_INFODto).MANIFEST_REPORT_SUU_KBN.ToString();
            try
            {
                switch (str)
                {
                    case "1":
                        if (!IsNumericAndMinusCheck(strAry[41]))
                        {
                            suuryou = SqlDecimal.Parse(strAry[41]);
                            unitCd = strAry[42];
                            isEmptySuuryou = false;
                        }
                        break;
                    case "2":
                        suuryou = SqlDecimal.Parse(strAry[21]);
                        unitCd = strAry[22];
                        isEmptySuuryou = false;
                        break;
                    case "3":
                        for (int r19i = 4; r19i >= 0; r19i--)
                        {
                            int celllength = 0;
                            if (r19i > 0)
                            {
                                celllength = 197;
                            }
                            else
                            {
                                celllength = 38;
                            }
                            // EDI_MEMBER_IDチェック
                            string upnEdiMemberId = strAry[45 + celllength + (r19i - 1) * 38];
                            if (string.IsNullOrEmpty(upnEdiMemberId))
                            {
                                continue;
                            }

                            // MS_JWNET_MEMBERチェック
                            SearchSQL = "EDI_MEMBER_ID ='" + upnEdiMemberId + "'";
                            var jwnetMember = MstDataInfo.msJwnetMember.Select(SearchSQL);

                            if (jwnetMember == null || jwnetMember.Count() < 1)
                            {
                                continue;
                            }

                            if (!string.IsNullOrEmpty(jwnetMember[0]["EDI_PASSWORD"].ToString())
                                && !IsNumericAndMinusCheck(strAry[68 + celllength + (r19i - 1) * 38].ToString()))
                            {
                                suuryou = SqlDecimal.Parse(strAry[68 + celllength + (r19i - 1) * 38].ToString());
                                unitCd = strAry[69 + celllength + (r19i - 1) * 38].ToString();
                                isEmptySuuryou = false;
                                break;
                            }
                        }
                        break;
                    case "4":
                        if (!IsNumericAndMinusCheck(strAry[113]))
                        {
                            suuryou = SqlDecimal.Parse(strAry[113]);
                            unitCd = strAry[114];
                            isEmptySuuryou = false;
                        }
                        break;
                }

                if (isEmptySuuryou)
                {
                    suuryou = SqlDecimal.Parse(strAry[21]);
                    unitCd = strAry[22];
                }
            }
            catch {
                suuryou = -1;
            }

            ManifestoLogic maniLogic = new ManifestoLogic();
            // 換算後数量、減容後数量で利用する廃棄物名称CD
            string tempHaikiNameCd = string.Empty;

            if (0 < suuryou)
            {
                //換算後数量の計算を行う
                //SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                DenshiSearchParameterDtoCls dto = new DenshiSearchParameterDtoCls();
                
                //廃棄物種類CD
                if (strAry[18] != null)
                {
                    dto.HAIKI_SHURUI_CD = strAry[18].Substring(0, 4);
                }
                else
                {
                    dto.HAIKI_SHURUI_CD = string.Empty;
                }

                //廃棄物種類CD
                dto.EDI_MEMBER_ID = strAry[6];
                //廃棄物名称
                //if (strAry[24] != null)
                //{
                //    dto.HAIKI_NAME = strAry[24];
                //}
                //廃棄物名称CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[6] + "'";
                SearchSQL += " AND HAIKI_NAME ='" + strAry[24] + "'";
                drArr = MstDataInfo.denshiHakkiNameTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    if (drArr[0]["HAIKI_NAME_CD"] != null)
                    {
                        dto.HAIKI_NAME_CD = drArr[0]["HAIKI_NAME_CD"].ToString();
                        // 減容後数量でも利用したので変数に設定
                        tempHaikiNameCd = drArr[0]["HAIKI_NAME_CD"].ToString();
                    }
                }
                else
                {
                    dto.HAIKI_NAME_CD = string.Empty;
                }

                //単位CD
                dto.UNIT_CD = unitCd;

                //　荷姿CD
                if (strAry[25] != null)
                {
                    dto.NISUGATA_CD = strAry[25];
                }
                else
                {
                    dto.NISUGATA_CD = string.Empty;
                }

                //換算式と換算値取得
                //DataTable tbl = DT_R18Dao.GetKansanshikiKansanchiData(dto);
                DataTable tbl = new DenshiMasterDataLogic().GetDenmaniKansanData(dto);
                if (tbl.Rows.Count == 1)
                {
                    SqlDecimal kansanti = SqlDecimal.Parse(tbl.Rows[0][1].ToString());
                    if (0 != kansanti)
                    {
                        //乗算式
                        if (tbl.Rows[0][0].ToString() == "0")
                        {
                            DT_R18_EXDto.KANSAN_SUU = maniLogic.Round((decimal)(suuryou * kansanti), SystemProperty.Format.ManifestSuuryou);
                        }
                        //除算式
                        else
                        {
                            DT_R18_EXDto.KANSAN_SUU = maniLogic.Round((decimal)(suuryou / kansanti), SystemProperty.Format.ManifestSuuryou);
                        }
                    }
                    else
                    {
                        DT_R18_EXDto.KANSAN_SUU = maniLogic.Round((decimal)0, SystemProperty.Format.ManifestSuuryou);
                    }
                }
            }

            //減容後数量 電マニ入力画面から引用
            // ※Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.LogicClass#GetGenYougou_suuメソッドより引用
            SqlDecimal genyou_suu = DT_R18_EXDto.KANSAN_SUU;

            if (!IsNumericAndMinusCheck(DT_R18_EXDto.KANSAN_SUU.ToString()))
            {
                SearchMasterDataDTOCls dto = new SearchMasterDataDTOCls();
                SqlDecimal GENNYOURITSU = 0;

                DataTable tbl = new DataTable();

                //報告書分類＋廃棄物名称＋処分方法＋減容率 で検索
                if (!string.IsNullOrEmpty(strAry[18])
                    && !string.IsNullOrEmpty(tempHaikiNameCd)
                    && !string.IsNullOrEmpty(DT_R18_EXDto.SBN_HOUHOU_CD))
                {
                    dto.HAIKI_SHURUI_CD = strAry[18].Substring(0, 4);
                    dto.HAIKI_NAME_CD = tempHaikiNameCd;
                    dto.SHOBUN_HOUHOU_CD = DT_R18_EXDto.SBN_HOUHOU_CD;

                    tbl = DT_R18Dao.GetGenyoritsu(dto);
                }

                // 報告書分類＋処分方法＋減容率
                if (tbl.Rows.Count < 1)
                {
                    if (!string.IsNullOrEmpty(strAry[18])
                        && !string.IsNullOrEmpty(DT_R18_EXDto.SBN_HOUHOU_CD))
                    {
                        dto.HAIKI_SHURUI_CD = strAry[18].Substring(0, 4);
                        dto.HAIKI_NAME_CD = string.Empty;
                        dto.SHOBUN_HOUHOU_CD = DT_R18_EXDto.SBN_HOUHOU_CD;

                        tbl = DT_R18Dao.GetGenyoritsu(dto);
                    }
                }

                // 報告書分類＋減容率
                if (tbl.Rows.Count < 1)
                {
                    if (!string.IsNullOrEmpty(strAry[18]))
                    {
                        dto.HAIKI_SHURUI_CD = strAry[18].Substring(0, 4);
                        dto.HAIKI_NAME_CD = string.Empty;
                        dto.SHOBUN_HOUHOU_CD = string.Empty;
                        tbl = DT_R18Dao.GetGenyoritsu(dto);
                    }
                }
                if (tbl.Rows.Count == 1 && tbl.Rows[0]["GENNYOURITSU"] != null)
                {
                    GENNYOURITSU = SqlDecimal.Parse(tbl.Rows[0]["GENNYOURITSU"].ToString());
                    genyou_suu = (decimal)(SqlDecimal.Divide(SqlDecimal.Multiply(DT_R18_EXDto.KANSAN_SUU, 100 - GENNYOURITSU), 100.00m));
                    genyou_suu = maniLogic.Round((decimal)genyou_suu, SystemProperty.Format.ManifestSuuryou);
                }
            }

            DT_R18_EXDto.GENNYOU_SUU = genyou_suu;

            //削除フラグ
            DT_R18_EXDto.DELETE_FLG = false;

            var dataBinderEntryEX18 = new DataBinderLogic<DT_R18_EX>(DT_R18_EXDto);
            dataBinderEntryEX18.SetSystemProperty(DT_R18_EXDto, true);

            DMInfo.dt_r18Ex = DT_R18_EXDto;
            LogUtility.DebugMethodEnd();
        }

        public void DT_R19_EXsousa(string[] strAry, DenshiManifestInfoCls DMInfo, bool denshiCheckFlg = true)
        {
            LogUtility.DebugMethodStart(strAry);

            DataTable dt = new DataTable();
            if (updateFlg)
            {
                DT_R19_EX DT_R19_EXDto = new DT_R19_EX();
                DT_R19_EXDto.KANRI_ID = kanriID;
                DT_R19_EXDto.DELETE_FLG = false;
                DT_R19_EX[] DT_R19_EXDtoAll = DT_R19_EXDao.GetDataForEntity(DT_R19_EXDto);

                if (DT_R19_EXDtoAll != null && DT_R19_EXDtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R19_EXDtoAll.Length; k++)
                    {
                        //削除フラグ
                        DT_R19_EXDtoAll[k].DELETE_FLG = true;
                        DMInfo.lstDT_R19_EXOld.Add(DT_R19_EXDtoAll[k]);
                    }
                }
            }

            if (!denshiCheckFlg)
                return;

            for (int r19i = 0; r19i < 5; r19i++)
            {
                DT_R19_EX DT_R19_EXDto = new DT_R19_EX();

                int celllength = 0;
                if (r19i > 0)
                {
                    celllength = 197;
                }
                else
                {
                    celllength = 38;
                }

                string str = strAry[44 + celllength + (r19i - 1) * 38];
                if ((r19i + 1) > unpankukanKubun)
                {
                    break;
                }

                if ("0".Equals(str)){
                    continue;
                }

                bool flg = false;
                int index = 44 + celllength + (r19i - 1) * 38;
                for (int i = 0; i < 38; i++)
                {
                    if (!string.IsNullOrWhiteSpace(strAry[index]))
                    {
                        flg = true;
                        break;
                    }
                    index++;
                }
                if (flg == false) {
                    continue;
                }

                //システムID DT_R18のSYSTEM_ID
                DT_R19_EXDto.SYSTEM_ID = systemID;
                //枝番
                if (updateFlg == true)
                {
                    DT_R19_EXDto.SEQ = EXlatestSEQ;
                }
                else
                {
                    DT_R19_EXDto.SEQ = 1;
                }
                //管理番号 DT_R18のKANRI_ID
                DT_R19_EXDto.KANRI_ID = kanriID;
                //区間番号 DT_R19のUPN_ROUTE_NO
                DT_R19_EXDto.UPN_ROUTE_NO = SqlDecimal.Parse((r19i + 1).ToString());
                //マニフェスト／予約番号
                DT_R19_EXDto.MANIFEST_ID = manifestID;
                //収集運搬業者CD
                string SearchSQL = "EDI_MEMBER_ID ='" + strAry[45 + celllength + (r19i - 1) * 38] + "'";
                DataRow[] drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
                if (drArr.Length == 1)
                {
                    DT_R19_EXDto.UPN_GYOUSHA_CD = drArr[0]["GYOUSHA_CD"].ToString();
                }

                //報告不要収集運搬業者加入者番号
                if (!KunbunCheck(str, "運搬委託区分"))
                {
                    if ("2".Equals(strAry[44 + celllength + (r19i - 1) * 38]))
                    {
                        DT_R19_EXDto.NO_REP_UPN_EDI_MEMBER_ID = "0000000";
                    }
                    else
                    {
                        DT_R19_EXDto.NO_REP_UPN_EDI_MEMBER_ID = strAry[45 + celllength + (r19i - 1) * 38];
                    }
                }
                //運搬先業者CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[76 + celllength + (r19i - 1) * 38] + "'";
                drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);//
                if (drArr.Length == 1)
                {
                    DT_R19_EXDto.UPNSAKI_GYOUSHA_CD = drArr[0]["GYOUSHA_CD"].ToString();
                }

                //報告不要運搬先業者加入者番号
                if (!KunbunCheck(str, "運搬委託区分"))
                {
                    if ("2".Equals(strAry[44 + celllength + (r19i - 1) * 38]))
                    {
                        DT_R19_EXDto.NO_REP_UPNSAKI_EDI_MEMBER_ID = "0000000";
                    }
                    else
                    {
                        DT_R19_EXDto.NO_REP_UPNSAKI_EDI_MEMBER_ID = strAry[76 + celllength + (r19i - 1) * 38];
                    }
                }
                //運搬先事業場CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[76 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND JIGYOUJOU_NAME ='" + strAry[78 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND JIGYOUJOU_ADDRESS ='" + strAry[80 + celllength + (r19i - 1) * 38] + "'";
                drArr = MstDataInfo.denshiJgyoujoTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.UPNSAKI_GENBA_CD = drArr[0]["GENBA_CD"].ToString();
                }

                //運搬担当者CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[45 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND TANTOUSHA_KBN ='3'";
                SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[62 + celllength + (r19i - 1) * 38] + "'";
                drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.UPN_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
                }

                //車輌CD
                SearchSQL = "SHARYOU_NAME ='" + strAry[63 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND GYOUSHA_CD ='" + DT_R19_EXDto.UPN_GYOUSHA_CD + "'";
                drArr = MstDataInfo.sharryouTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.SHARYOU_CD = drArr[0]["SHARYOU_CD"].ToString();
                }

                //運搬報告記載の運搬担当者CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[45 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND TANTOUSHA_KBN ='3'";
                SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[66 + celllength + (r19i - 1) * 38] + "'";
                drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.UPNREP_UPN_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
                }

                //運搬報告記載の車輌CD
                SearchSQL = "SHARYOU_NAME ='" + strAry[74 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND GYOUSHA_CD ='" + DT_R19_EXDto.UPN_GYOUSHA_CD + "'";
                drArr = MstDataInfo.sharryouTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.UPNREP_SHARYOU_CD = drArr[0]["SHARYOU_CD"].ToString();
                }

                //報告担当者CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[45 + celllength + (r19i - 1) * 38] + "'";
                SearchSQL += " AND TANTOUSHA_KBN ='4'";
                SearchSQL += " AND TANTOUSHA_NAME ='" + strAry[67 + celllength + (r19i - 1) * 38] + "'";
                drArr = MstDataInfo.denshiTantousyaTb.Select(SearchSQL);//
                if (drArr.Length >= 1)
                {
                    DT_R19_EXDto.HOUKOKU_TANTOUSHA_CD = drArr[0]["TANTOUSHA_CD"].ToString();
                }

                DT_R19_EXDto.DELETE_FLG = false;

                var dataBinderEntry19 = new DataBinderLogic<DT_R19_EX>(DT_R19_EXDto);
                dataBinderEntry19.SetSystemProperty(DT_R19_EXDto, true);

                DMInfo.lstDT_R19_EX.Add(DT_R19_EXDto);
            }
            LogUtility.DebugMethodEnd();
        }

        public void DT_R04_EXsousa(string[] strAry, DenshiManifestInfoCls DMInfo, bool denshiCheckFlg = true)
        {
            LogUtility.DebugMethodStart(strAry);

            DataTable dt = new DataTable();
            //電子マニフェスト最終処分（予定）拡張[DT_R04_EX]
            if (updateFlg)
            {
                DT_R04_EX DT_R04_EXDto = new DT_R04_EX();
                DT_R04_EXDto.KANRI_ID = kanriID;
                DT_R04_EXDto.DELETE_FLG = false;
                DT_R04_EX[] DT_R04_EXDtoAll = DT_R04_EXDao.GetDataForEntity(DT_R04_EXDto);

                if (DT_R04_EXDtoAll != null && DT_R04_EXDtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R04_EXDtoAll.Length; k++)
                    {
                        //削除フラグ
                        DT_R04_EXDtoAll[k].DELETE_FLG = true;
                        DMInfo.lstDT_R04_EXOld.Add(DT_R04_EXDtoAll[k]);
                    }
                }
            }

            if (!denshiCheckFlg)
                return;

            for (int r04i = 0; r04i < 10; r04i++)
            {
                if (string.IsNullOrWhiteSpace(strAry[118 + r04i * 4]) && string.IsNullOrWhiteSpace(strAry[119 + r04i * 4])
                    && string.IsNullOrWhiteSpace(strAry[120 + r04i * 4]) && string.IsNullOrWhiteSpace(strAry[121 + r04i * 4]))
                {
                    continue;
                }

                DT_R04_EX DT_R04_EXDto = new DT_R04_EX();
                //システムID DT_R18のSYSTEM_ID
                DT_R04_EXDto.SYSTEM_ID = systemID;

                if (updateFlg == true)
                {
                    DT_R04_EXDto.SEQ = EXlatestSEQ;
                }
                else
                {
                    //枝番
                    DT_R04_EXDto.SEQ = 1;
                }

                //管理番号
                DT_R04_EXDto.KANRI_ID = kanriID;

                //レコード枝番 DT_R04のREC_SEQ
                DT_R04_EXDto.REC_SEQ = SqlDecimal.Parse((r04i + 1).ToString());

                //マニフェスト／予約番号
                DT_R04_EXDto.MANIFEST_ID = manifestID;

                //最終処分業者CD
                string SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
                DataRow[] drArr = MstDataInfo.denshiJgyosyaTb.Select(SearchSQL);
                if (drArr.Length >= 1)
                {
                    DT_R04_EXDto.LAST_SBN_GYOUSHA_CD = drArr[0]["GYOUSHA_CD"].ToString();
                }

                //最終処分事業場CD
                SearchSQL = "EDI_MEMBER_ID ='" + strAry[83] + "'";
                SearchSQL += " AND JIGYOUJOU_NAME ='" + strAry[118 + 4 * r04i] + "'";
                SearchSQL += " AND JIGYOUJOU_ADDRESS ='" + strAry[120 + 4 * r04i] + "'";
                drArr = MstDataInfo.denshiJgyoujoTb.Select(SearchSQL);
                if (drArr.Length >= 1)
                {
                    DT_R04_EXDto.LAST_SBN_GENBA_CD = drArr[0]["GENBA_CD"].ToString();
                }

                //削除フラグ
                DT_R04_EXDto.DELETE_FLG = false;

                var dataBinderEntry04 = new DataBinderLogic<DT_R04_EX>(DT_R04_EXDto);
                dataBinderEntry04.SetSystemProperty(DT_R04_EXDto, true);

                DMInfo.lstDT_R04_EX.Add(DT_R04_EXDto);
            }
            LogUtility.DebugMethodEnd();
        }

        public void DT_R13_EXsousa(string[] strAry,DenshiManifestInfoCls DMInfo, bool denshiCheckFlg = true)
        {
            LogUtility.DebugMethodStart(strAry);

            DataTable dt = new DataTable();
            if (updateFlg)
            {
                DT_R13_EX DT_R13_EXDto = new DT_R13_EX();
                DT_R13_EXDto.KANRI_ID = kanriID;
                DT_R13_EXDto.DELETE_FLG = false;
                DT_R13_EX[] DT_R13_EXDtoAll = DT_R13_EXDao.GetDataForEntity(DT_R13_EXDto);

                if (DT_R13_EXDtoAll != null && DT_R13_EXDtoAll.Length > 0)
                {
                    for (int k = 0; k < DT_R13_EXDtoAll.Length; k++)
                    {
                        //削除フラグ
                        DT_R13_EXDtoAll[k].DELETE_FLG = true;
                        DMInfo.lstDT_R13_EXOld.Add(DT_R13_EXDtoAll[k]);
                    }
                }
            }

            if (!denshiCheckFlg)
                return;

            //List<DT_R13> DT_R13DtoList = new List<DT_R13>();
            for (int r13i = 0; r13i < 15; r13i++)
            {
                if (string.IsNullOrEmpty(strAry[160 + 5 * r13i]) && string.IsNullOrEmpty(strAry[161 + 5 * r13i])
                    && string.IsNullOrEmpty(strAry[162 + 5 * r13i]) && string.IsNullOrEmpty(strAry[163 + 5 * r13i])
                    && string.IsNullOrEmpty(strAry[164 + 5 * r13i]))
                {
                    break;
                }

                DT_R13_EX DT_R13_EXDto = new DT_R13_EX();
                //システムID DT_R18のSYSTEM_ID
                DT_R13_EXDto.SYSTEM_ID = systemID;
                //枝番
                DT_R13_EXDto.SEQ = 1;

                if (updateFlg == true)
                {
                    DT_R13_EXDto.SEQ = EXlatestSEQ;
                }
                else
                {
                    //枝番
                    DT_R13_EXDto.SEQ = 1;
                }

                //管理番号
                DT_R13_EXDto.KANRI_ID = kanriID;

                //レコード枝番 DT_R04のREC_SEQ
                DT_R13_EXDto.REC_SEQ = SqlDecimal.Parse((r13i + 1).ToString());

                //マニフェスト／予約番号
                DT_R13_EXDto.MANIFEST_ID = manifestID;

                //削除フラグ
                DT_R13_EXDto.DELETE_FLG = false;

                var dataBinderEntry13 = new DataBinderLogic<DT_R13_EX>(DT_R13_EXDto);
                dataBinderEntry13.SetSystemProperty(DT_R13_EXDto, true);

                DMInfo.lstDT_R13_EX.Add(DT_R13_EXDto);
            }
            LogUtility.DebugMethodEnd();
        }
        
        //実行ボタンを押すと処理を実行します
        [Transaction]
        public void jikkou()
        {
            LogUtility.DebugMethodStart();

            //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上有った場合判断
            bool updataflag = false;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        updataflag = true;
                        break;
                    }
                }
            }
            if (updataflag)
            {
                if (errCheck())
                {
                    return;
                };
                string errorMessage = string.Empty;
                //チェックされた電子マニフェスト情報を更新するかを確認する。
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("C048").MESSAGE;
                errorMessage = String.Format(errorMessage);
                DialogResult result = MessageBox.Show(errorMessage, Constans.CONFIRM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                //明細行のチェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                string errorMessage = string.Empty;
                var messageUtil = new MessageUtility();
                errorMessage = messageUtil.GetMessage("E029").MESSAGE;
                errorMessage = String.Format(errorMessage, "登録する電子マニフェスト情報", "明細");
                MessageBox.Show(errorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                insertAndUpdate();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                new MessageBoxShowLogic().MessageBoxShow("I007", "登録処理");

                LogUtility.DebugMethodEnd();

                return;
            }
            string infoMessage = string.Empty;

            var messageUtil1 = new MessageUtility();
            infoMessage = messageUtil1.GetMessage("I001").MESSAGE;
            infoMessage = String.Format(infoMessage, "登録処理");
            MessageBox.Show(infoMessage, Constans.CONFIRM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            LogUtility.DebugMethodEnd();
        }

        public string hiduke8(string str) {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    str = DateTime.Parse(str).ToString();
                }
                catch {
                    return null;
                }
                str = str.Substring(0, 10);
                str = str.Replace("/", "");
            }
            return str;
        }

        /// <summary>
        /// CSV取込み
        /// </summary>
        public void CSVImport()
        {
            try
            {
                // ファイル存在チェック
                if (!File.Exists(this.form.ctxt_FilePath.Text))
                {
                    // E200
                    new MessageBoxShowLogic().MessageBoxShow("E200");
                    return;
                }

                // 取り込み&DGV設定
                this.CSVRead(this.form.ctxt_FilePath.Text);
                if (!this.SetDataToDgv()) { return; }

                if (!string.IsNullOrEmpty(this.form.ctxt_FilePath.Text))
                {
                    // ファイルパス保存
                    r_framework.Configuration.AppConfig.SaveLocalFilePath(
                        WINDOW_ID.T_DENSHI_MANIFEST_CSV_INPUT.ToString(), this.form.ctxt_FilePath.Text);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CSVImport", ex1);
                messboxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVImport", ex);
                messboxShow("E245", "");
            }
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
