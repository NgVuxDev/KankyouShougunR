using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DataGridViewCheckBoxColumnHeeader;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Message;
using Shougun.Core.PaperManifest.ManifestIkkatsuKousin;
using Shougun.Core.PaperManifest.ManifestIkkatsuKousin.DAO;

namespace Shougun.Core.PapeMranifest.ManifestIkkatsuKousin
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        ///// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public int denShu_Kbn { get; set; }

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass HkIchiran;

        /// <summary>
        /// 共通
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private HeaderForm headForm;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestIkkatsuKousin.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity { get; set; }

        /// <summary>
        /// 検索中フラグ
        /// </summary>
        private bool searching = false;

        /// <summary>
        /// 登録用：マニフェストDao
        /// </summary>
        private T_MANIFEST_ENTRYdaocls MANIFEST_ENTRYDao;

        /// <summary>
        /// 登録用：マニフェスト印字
        /// </summary>
        private T_MANIFEST_PRTdaocls MANIFEST_PRTDao;

        /// <summary>
        /// 登録用：マニフェスト運搬
        /// </summary>
        private T_MANIFEST_UPNdaocls MANIFEST_UPNDao;

        /// <summary>
        /// 登録用：マニフェスト詳細
        /// </summary>
        private T_MANIFEST_DETAILdaocls MANIFEST_DETAILDao;

        /// <summary>
        /// 登録用：マニフェスト印字詳細
        /// </summary>
        private T_MANIFEST_DETAIL_PRTdaocls MANIFEST_DETAIL_PRTDao;

        /// <summary>
        /// 登録用：マニ印字_建廃_形状
        /// </summary>
        private T_MANIFEST_KP_KEIJYOUdaocls MANIFEST_KP_KEIJYOUDao;

        /// <summary>
        /// 登録用：マニ印字_建廃_荷姿
        /// </summary>
        private T_MANIFEST_KP_NISUGATAdaocls MANIFEST_KP_NISUGATADao;

        /// <summary>
        /// 登録用：マニ印字_建廃_処分方法
        /// </summary>
        private T_MANIFEST_KP_SBN_HOUHOUdaocls MANIFEST_KP_SBN_HOUHOUDao;

        /// <summary>
        /// 登録用：マニフェスト返却日
        /// </summary>
        private T_MANIFEST_RET_DATE_daocls MANIFEST_RET_DATEDao;

        /// <summary>
        /// 
        /// </summary>
        private MessageBoxShowLogic MsgBox;
        private IM_HAIKI_SHURUIDao dao_HaikiShurui;
        private IM_HAIKI_NAMEDao dao_HaikiName;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.HkIchiran = DaoInitUtility.GetComponent<DAOClass>();
            MANIFEST_RET_DATEDao = DaoInitUtility.GetComponent<T_MANIFEST_RET_DATE_daocls>();

            this.MANIFEST_ENTRYDao = DaoInitUtility.GetComponent<T_MANIFEST_ENTRYdaocls>(); //マニフェストDao
            this.MANIFEST_DETAILDao = DaoInitUtility.GetComponent<T_MANIFEST_DETAILdaocls>(); //マニフェスト明細Dao
            this.MANIFEST_UPNDao = DaoInitUtility.GetComponent<T_MANIFEST_UPNdaocls>(); //マニフェスト運搬Dao
            this.MANIFEST_PRTDao = DaoInitUtility.GetComponent<T_MANIFEST_PRTdaocls>(); //マニフェスト印字Dao
            this.MANIFEST_DETAIL_PRTDao = DaoInitUtility.GetComponent<T_MANIFEST_DETAIL_PRTdaocls>(); //マニフェスト印字詳細Dao
            this.MANIFEST_KP_KEIJYOUDao = DaoInitUtility.GetComponent<T_MANIFEST_KP_KEIJYOUdaocls>(); //マニ印字_建廃_形状Dao
            this.MANIFEST_KP_NISUGATADao = DaoInitUtility.GetComponent<T_MANIFEST_KP_NISUGATAdaocls>(); //マニ印字_建廃_荷姿Dao
            this.MANIFEST_KP_SBN_HOUHOUDao = DaoInitUtility.GetComponent<T_MANIFEST_KP_SBN_HOUHOUdaocls>(); //マニ印字_建廃_処分方法Dao
            this.dao_HaikiShurui = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>(); //廃棄物種類Dao
            this.dao_HaikiName = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>(); //廃棄物名Dao
            this.MsgBox = new MessageBoxShowLogic();
            mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret = 0;
            try
            {
                // 検索中フラグ
                searching = true;

                ////SQLの作成
                MakeSearchCondition();

                this.SearchResult = new DataTable();

                DataTable db = new DataTable();

                //マニフェストデータを取得する。
                db = HkIchiran.getdateforstringsql(this.createSql);

                db.Columns["CHECKBOX"].ReadOnly = false;

                int count = SearchResult.Rows.Count;

                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();

                // パターンが登録されていない場合は表示しない
                if (this.selectQuery != null)
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX";
                    newColumn.DataPropertyName = "CHECKBOX";
                    newColumn.ReadOnly = false;
                    DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                    newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                        datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);
                    newColumn.HeaderCell = newheader;
                    newColumn.HeaderText = string.Empty;
                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                }

                string orderByDT = this.orderByQuery.Replace("\"", "");
                db.DefaultView.Sort = orderByDT;
                DataTable dt = db.DefaultView.ToTable();

                this.SearchResult = dt;

                // DataGridViewを作り直すとイベントが消えるため再設定
                var tempCheckBoxCell = this.form.customDataGridView1.Columns["CHECKBOX"].HeaderCell as DataGridviewCheckboxHeaderCell;
                tempCheckBoxCell.OnCheckBoxClicked -= new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                    datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);
                this.form.ShowData();
                tempCheckBoxCell = this.form.customDataGridView1.Columns["CHECKBOX"].HeaderCell as DataGridviewCheckboxHeaderCell;
                tempCheckBoxCell.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                    datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);

                DataTable dbcopy = (DataTable)this.form.customDataGridView1.DataSource;
                dbcopy = dbcopy.Copy();
                int min = 0;
                for (int index = 0; index < this.form.customDataGridView1.Columns.Count; index++)
                {
                    DataGridViewColumn column = this.form.customDataGridView1.Columns[index];

                    if ("処分終了日".Equals(column.Name)
                        || "運搬終了日(区間1)".Equals(column.Name)
                        || "運搬終了日(区間2)".Equals(column.Name)
                        || "運搬終了日(区間3)".Equals(column.Name)
                        || "最終処分終了日".Equals(column.Name))
                    {
                        this.form.customDataGridView1.Columns.RemoveAt(index);
                        DgvCustomDataTimeColumn newColumnDataTime = new DgvCustomDataTimeColumn();
                        newColumnDataTime.Name = column.Name;
                        newColumnDataTime.DataPropertyName = column.DataPropertyName;
                        this.form.customDataGridView1.Columns.Insert(index, newColumnDataTime);
                        min++;
                    }

                    //有効化
                    if ("処分終了日".Equals(column.Name)
                        || "運搬終了日(区間1)".Equals(column.Name)
                        || "運搬終了日(区間2)".Equals(column.Name)
                        || "運搬終了日(区間3)".Equals(column.Name)
                        || "最終処分終了日".Equals(column.Name))
                    {
                        column.ReadOnly = false;
                    }

                    if ("CHECKBOX".Equals(column.Name))
                    {
                        column.ReadOnly = false;
                    }
                }

                //一覧の項目を非表示
                this.form.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
                this.form.customDataGridView1.Columns["SEQ"].Visible = false;
                this.form.customDataGridView1.Columns["TMRD_SEQ"].Visible = false;
                this.form.customDataGridView1.Columns["HAIKI_KBN_CD"].Visible = false;
                this.form.customDataGridView1.Columns["MANIFEST_KBN"].Visible = false;
                this.form.customDataGridView1.Columns["DETAIL_SYSTEM_ID"].Visible = false;
                this.form.customDataGridView1.Columns["TIME_STAMP_ENTRY"].Visible = false;
                this.form.customDataGridView1.Columns["TIME_STAMP_RET_DATE"].Visible = false;
                this.form.customDataGridView1.Columns["NEXT_SYSTEM_ID"].Visible = false;
                this.form.customDataGridView1.Columns["NEXT_HAIKI_KBN_CD"].Visible = false;
                this.form.customDataGridView1.Columns["GYOUSHA_NAME"].Visible = false;
                this.form.customDataGridView1.Columns["GENBA_NAME"].Visible = false;
                this.form.customDataGridView1.Columns["LAST_SBN_END_DATE"].Visible = false;
                this.form.customDataGridView1.Columns["UPN_SAKI_KBN1"].Visible = false;
                this.form.customDataGridView1.Columns["UPN_SAKI_KBN2"].Visible = false;
                this.form.customDataGridView1.Columns["SLASH_UPN_JYUTAKUSHA2_FLG"].Visible = false;
                this.form.customDataGridView1.Columns["SLASH_UPN_JYUTAKUSHA3_FLG"].Visible = false;
                this.form.customDataGridView1.Columns["UPN_JYUTAKUSHA_CD1"].Visible = false;
                this.form.customDataGridView1.Columns["UPN_JYUTAKUSHA_CD2"].Visible = false;

                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    this.form.customDataGridView1.Rows[i].Cells["CHECKBOX"].Value = false;
                }

                this.setEnabled();

                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = false;
                this.form.customSortHeader1.SortDataTable(this.form.customDataGridView1.DataSource as DataTable);
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = true;

                // 検索中フラグ初期化
                searching = false;

                if ((DataTable)this.form.customDataGridView1.DataSource == null || ((DataTable)this.form.customDataGridView1.DataSource).Rows.Count <= 0 || dt.Rows.Count <= 0)
                {
                    this.MsgBox.MessageBoxShow("C001");
                    return 0;
                }

                ret = SearchResult.Rows.Count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = -1;
            }
            return ret;
        }

        public void setEnabled()
        {
            //getdateforstringsqlで取得していない項目を初期化する。
            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                if ("1".Equals(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString()))
                {
                    this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                    this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                }
                else if ("2".Equals(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString()))
                {
                    this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                    if ("True".Equals(this.form.customDataGridView1.Rows[i].Cells["SLASH_UPN_JYUTAKUSHA2_FLG"].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                    }
                }
                else if ("3".Equals(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString()))
                {
                    if ("1".Equals(this.form.customDataGridView1.Rows[i].Cells["UPN_SAKI_KBN1"].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                    }
                    else if ("1".Equals(this.form.customDataGridView1.Rows[i].Cells["UPN_SAKI_KBN2"].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                    }

                    if ("True".Equals(this.form.customDataGridView1.Rows[i].Cells["SLASH_UPN_JYUTAKUSHA2_FLG"].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                    }

                    if ("True".Equals(this.form.customDataGridView1.Rows[i].Cells["SLASH_UPN_JYUTAKUSHA3_FLG"].Value.ToString()))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                    }
                }

                if (this.form.customDataGridView1.Rows[i].Cells["DETAIL_SYSTEM_ID"].Value == null
                    || string.IsNullOrEmpty(this.form.customDataGridView1.Rows[i].Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                {
                    this.form.customDataGridView1.Rows[i].Cells["処分終了日"].ReadOnly = true;
                    this.form.customDataGridView1.Rows[i].Cells["最終処分終了日"].ReadOnly = true;
                }

                if ("False".Equals(this.form.customDataGridView1.Rows[i].Cells["MANIFEST_KBN"].Value.ToString()))
                {
                    if ((this.form.customDataGridView1.Rows[i].Cells["NEXT_SYSTEM_ID"].Value.ToString() != string.Empty))
                    {
                        this.form.customDataGridView1.Rows[i].Cells["処分終了日"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間1)"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["最終処分終了日"].ReadOnly = true;
                    }
                    else
                    {
                        var Search = new CommonSerchParameterDtoCls();
                        Search.SYSTEM_ID = this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString();
                        Search.HAIKI_KBN_CD = this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString();
                        DataTable dtDetail = this.mlogic.SearchDetailData(Search);
                        if (dtDetail.Rows.Count > 0)
                        {
                            bool ariFlag = false;
                            foreach (DataRow dr in dtDetail.Rows)
                            {
                                if (!string.IsNullOrEmpty(dr["NEXT_SYSTEM_ID"].ToString()))
                                {
                                    ariFlag = true;
                                }
                            }
                            if (ariFlag)
                            {
                                this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間1)"].ReadOnly = true;
                                this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間2)"].ReadOnly = true;
                                this.form.customDataGridView1.Rows[i].Cells["運搬終了日(区間3)"].ReadOnly = true;
                            }
                        }
                    }
                }
                else
                {
                    // 紐付いている一次電マニが最終処分終了報告済みかフラグ
                    bool isFixedFirstElecMani = false;
                    bool isExecutingFirstElecMani = false;
                    isFixedFirstElecMani = this.mlogic.IsFixedRelationFirstMani(SqlInt64.Parse(this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString()), Convert.ToInt32(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString()));
                    isExecutingFirstElecMani = this.mlogic.IsExecutingLastSbnEndRep(SqlInt64.Parse(this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString()), SqlInt16.Parse(this.form.customDataGridView1.Rows[i].Cells["HAIKI_KBN_CD"].Value.ToString()));
                    if ((isFixedFirstElecMani || isExecutingFirstElecMani)
                      && this.form.customDataGridView1.Rows[i].Cells["LAST_SBN_END_DATE"].Value != null
                      && this.form.customDataGridView1.Rows[i].Cells["GYOUSHA_NAME"].Value != null
                      && this.form.customDataGridView1.Rows[i].Cells["GENBA_NAME"].Value != null)
                    {
                        this.form.customDataGridView1.Rows[i].Cells["最終処分終了日"].ReadOnly = true;
                    }
                }
            }
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //一括入力ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);
            //条件ｸﾘｱ(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);

            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移

            //parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //紐付1次最終処分終了報告画面へ遷移

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();

            //checkbox全選択処理
            this.form.customDataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DetailIchiran_ColumnHeaderMouseClick);

            this.form.customDataGridView1.RowEnter += new DataGridViewCellEventHandler(this.customDataGridView1_RowEnter);

            this.form.customDataGridView1.CellValidated += new DataGridViewCellEventHandler(this.customDataGridView1_CellValidated);

            // 「To」のイベント生成
            this.form.KOUFU_DATE_TO.MouseDoubleClick += new MouseEventHandler(KOUFU_DATE_TO_MouseDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        #region 検索文字列の作成

        /// <summary>
        /// 検索文字列を作成する
        /// </summary>
        /// <param name="orderbyKbn">orderbyKbn</param>
        private void MakeSearchCondition()
        {
            //LogUtility.DebugMethodStart();

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句

            sql.Append(" SELECT ");
            sql.Append(" CONVERT(bit, 0) AS CHECKBOX ");                      //システムID
            sql.Append(" , T_MANIFEST_ENTRY.SYSTEM_ID SYSTEM_ID ");                      //システムID
            sql.Append(" , T_MANIFEST_ENTRY.SEQ SEQ ");                                //枝番
            sql.Append(" , T_MANIFEST_RET_DATE.SEQ TMRD_SEQ ");                                //返却日枝番
            sql.Append(" , T_MANIFEST_ENTRY.HAIKI_KBN_CD HAIKI_KBN_CD ");              //廃棄物区分
            sql.Append(" , T_MANIFEST_ENTRY.FIRST_MANIFEST_KBN MANIFEST_KBN ");              //一次二次区分
            sql.Append(" , T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID DETAIL_SYSTEM_ID ");     //明細システムID
            sql.Append(" , CAST(T_MANIFEST_ENTRY.TIME_STAMP AS int) AS TIME_STAMP_ENTRY ");                      //TIME_STAMP_ENTRY
            sql.Append(" , CAST(T_MANIFEST_RET_DATE.TIME_STAMP AS int) AS TIME_STAMP_RET_DATE ");                      //TMRD_TIME_STAMP
            sql.Append(" , TMR.MANIFEST_ID AS NEXT_SYSTEM_ID ");                      //NEXT_SYSTEM_ID
            sql.Append(" , TMR.NEXT_HAIKI_KBN_CD AS NEXT_HAIKI_KBN_CD ");                      //二次マニ交付番号
            sql.Append(" , MG.GYOUSHA_NAME1 + MG.GYOUSHA_NAME2 AS GYOUSHA_NAME ");                      //最終処分業者名
            sql.Append(" , MGA.GENBA_NAME1 + MGA.GENBA_NAME2 AS GENBA_NAME ");                      //最終処分場所名
            sql.Append(" , T_MANIFEST_DETAIL.LAST_SBN_END_DATE AS LAST_SBN_END_DATE ");                      //最終処分終了日
            sql.Append(" , ISNULL(T_MANIFEST_UPN1.UPN_SAKI_KBN, 0) AS UPN_SAKI_KBN1 ");                      //運搬区分１
            sql.Append(" , ISNULL(T_MANIFEST_UPN2.UPN_SAKI_KBN, 0) AS UPN_SAKI_KBN2 ");                      //運搬区分２
            sql.Append(" , T_MANIFEST_PRT.SLASH_UPN_JYUTAKUSHA2_FLG AS SLASH_UPN_JYUTAKUSHA2_FLG ");                      //運搬区間2斜線
            sql.Append(" , T_MANIFEST_PRT.SLASH_UPN_JYUTAKUSHA3_FLG AS SLASH_UPN_JYUTAKUSHA3_FLG ");                      //運搬区間3斜線
            sql.Append(" , T_MANIFEST_UPN1.UPN_JYUTAKUSHA_CD AS UPN_JYUTAKUSHA_CD1 ");                      //運搬区間1受託者
            sql.Append(" , T_MANIFEST_UPN2.UPN_JYUTAKUSHA_CD AS UPN_JYUTAKUSHA_CD2 ");                      //運搬区間2受託者

            String MOutputPatternSelect = this.selectQuery;
            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
            }
            else
            {
                if (!this.selectQuery.Contains("処分終了日"))
                {
                    sql.Append(" , T_MANIFEST_DETAIL.SBN_END_DATE 処分終了日 ");
                }
                if (!this.selectQuery.Contains("運搬終了日(区間1)"))
                {
                    sql.Append(" , T_MANIFEST_UPN1.UPN_END_DATE 運搬終了日(区間1) ");
                }
                if (!this.selectQuery.Contains("運搬終了日(区間2)"))
                {
                    sql.Append(" , T_MANIFEST_UPN2.UPN_END_DATE 運搬終了日(区間2) ");
                }
                if (!this.selectQuery.Contains("運搬終了日(区間3)"))
                {
                    sql.Append(" , T_MANIFEST_UPN3.UPN_END_DATE 運搬終了日(区間3) ");
                }
                if (!this.selectQuery.Contains("最終処分終了日"))
                {
                    sql.Append(" , T_MANIFEST_DETAIL.LAST_SBN_END_DATE 最終処分終了日 ");
                }

                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
            }

            #endregion

            #region FROM句

            //FROM句作成
            sql.Append(" FROM ");
            sql.Append(" T_MANIFEST_ENTRY ");
            sql.Append(" LEFT JOIN T_MANIFEST_DETAIL ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID  ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ  ");

            sql.Append(" LEFT JOIN T_MANIFEST_PRT ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_PRT.SYSTEM_ID  ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_PRT.SEQ  ");

            sql.Append(" LEFT JOIN T_MANIFEST_UPN AS T_MANIFEST_UPN1");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN1.SYSTEM_ID ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN1.SEQ ");
            sql.Append(" AND T_MANIFEST_UPN1.UPN_ROUTE_NO = 1 ");

            sql.Append(" LEFT JOIN T_MANIFEST_UPN AS T_MANIFEST_UPN2");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN2.SYSTEM_ID ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN2.SEQ ");
            sql.Append(" AND T_MANIFEST_UPN2.UPN_ROUTE_NO = 2 ");
            sql.Append(" LEFT JOIN T_MANIFEST_UPN AS T_MANIFEST_UPN3 ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_UPN3.SYSTEM_ID ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_UPN3.SEQ ");
            sql.Append(" AND T_MANIFEST_UPN3.UPN_ROUTE_NO = 3 ");

            sql.Append(" LEFT JOIN T_MANIFEST_RET_DATE ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_RET_DATE.SYSTEM_ID  ");
            sql.Append(" AND T_MANIFEST_RET_DATE.DELETE_FLG = 'false'  ");

            sql.Append(" LEFT JOIN M_HAIKI_SHURUI ");
            sql.Append(" ON T_MANIFEST_ENTRY.HAIKI_KBN_CD = M_HAIKI_SHURUI.HAIKI_KBN_CD ");
            sql.Append(" AND T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD  ");

            sql.Append(" LEFT JOIN M_HAIKI_NAME ");
            sql.Append(" ON T_MANIFEST_DETAIL.HAIKI_NAME_CD = M_HAIKI_NAME.HAIKI_NAME_CD  ");

            sql.Append(" LEFT JOIN M_UNIT ");
            sql.Append(" ON T_MANIFEST_DETAIL.HAIKI_UNIT_CD = M_UNIT.UNIT_CD  ");

            sql.Append(" LEFT JOIN M_GYOUSHA AS MG WITH(NOLOCK) ");
            sql.Append(" ON T_MANIFEST_DETAIL.LAST_SBN_GYOUSHA_CD = MG.GYOUSHA_CD  ");

            sql.Append(" LEFT JOIN M_GENBA AS MGA WITH(NOLOCK) ");
            sql.Append(" ON T_MANIFEST_DETAIL.LAST_SBN_GYOUSHA_CD = MGA.GYOUSHA_CD  ");
            sql.Append(" AND T_MANIFEST_DETAIL.LAST_SBN_GENBA_CD = MGA.GENBA_CD  ");

            //if (!string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
            //{
            sql.Append(" LEFT JOIN (SELECT TMU1.* FROM T_MANIFEST_ENTRY TME1 INNER JOIN T_MANIFEST_UPN TMU1 ON TMU1.SYSTEM_ID = TME1.SYSTEM_ID AND TMU1.SEQ = TME1.SEQ AND TMU1.UPN_ROUTE_NO = 1 WHERE (TME1.HAIKI_KBN_CD = 1 OR TME1.HAIKI_KBN_CD =2) AND TME1.DELETE_FLG =0 AND TME1.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = TME1.SYSTEM_ID) ");
            sql.Append(" UNION ");
            sql.Append(" SELECT TMU2.* FROM T_MANIFEST_ENTRY TME2 INNER JOIN T_MANIFEST_UPN TMU2 ON TMU2.SYSTEM_ID = TME2.SYSTEM_ID AND TMU2.SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3  AND TMU2.UPN_ROUTE_NO = ( ");
            sql.Append(" SELECT MIN(UPN_ROUTE_NO) FROM T_MANIFEST_UPN WHERE TME2.SYSTEM_ID = SYSTEM_ID AND SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3 AND UPN_SAKI_KBN = 1) AND TME2.DELETE_FLG = 0) ");
            sql.Append(" TMU ON TMU.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID AND TMU.SEQ = T_MANIFEST_ENTRY.SEQ ");
            //}

            // 紐付2次 START
            sql.Append(" LEFT OUTER JOIN (  ");
            sql.Append(" SELECT COL_TMR.NEXT_SYSTEM_ID  ");
            sql.Append(" , COL_TMR.SEQ  ");
            sql.Append(" , COL_TMR.REC_SEQ ");
            sql.Append(" , COL_TMR.NEXT_HAIKI_KBN_CD  ");
            sql.Append(" , COL_TMR.FIRST_SYSTEM_ID  ");
            sql.Append(" , COL_TMR.FIRST_HAIKI_KBN_CD  ");
            sql.Append(" , COL_TMR.DELETE_FLG  ");
            sql.Append(" , COL_TMR.TIME_STAMP  ");
            sql.Append(" , CASE COL_TMR.NEXT_HAIKI_KBN_CD   ");
            sql.Append(" WHEN 1 THEN COL_TME.MANIFEST_ID  ");
            sql.Append(" WHEN 2 THEN COL_TME.MANIFEST_ID  ");
            sql.Append(" WHEN 3 THEN COL_TME.MANIFEST_ID  ");
            sql.Append(" WHEN 4 THEN COL_DR18E.MANIFEST_ID  ");
            sql.Append(" ELSE ''  ");
            sql.Append(" END AS MANIFEST_ID  ");
            sql.Append(" FROM T_MANIFEST_RELATION AS COL_TMR WITH(NOLOCK)  ");
            sql.Append(" INNER JOIN (  ");
            sql.Append(" SELECT NEXT_SYSTEM_ID  ");
            sql.Append(" , MAX(SEQ) AS SEQ  ");
            sql.Append(" FROM T_MANIFEST_RELATION WITH(NOLOCK)  ");
            sql.Append(" WHERE DELETE_FLG = 'false'  ");
            sql.Append(" GROUP BY NEXT_SYSTEM_ID  ");
            sql.Append(" ) AS MAX_TMR  ");
            sql.Append(" ON COL_TMR.NEXT_SYSTEM_ID = MAX_TMR.NEXT_SYSTEM_ID  ");
            sql.Append(" AND COL_TMR.SEQ = MAX_TMR.SEQ   ");
            //紙マニ START
            sql.Append(" LEFT OUTER JOIN (  ");
            sql.Append(" SELECT DISTINCT COL2_TME.SYSTEM_ID   ");
            sql.Append(" , COL2_TME.SEQ  ");
            sql.Append(" , COL2_TME.MANIFEST_ID  ");
            sql.Append(" FROM T_MANIFEST_ENTRY AS COL2_TME WITH(NOLOCK)  ");
            sql.Append(" INNER JOIN (  ");
            sql.Append(" SELECT SYSTEM_ID  ");
            sql.Append(" , MAX(SEQ) AS SEQ  ");
            sql.Append(" FROM T_MANIFEST_ENTRY AS COL_TME WITH(NOLOCK)  ");
            sql.Append(" WHERE DELETE_FLG = 'false'  ");
            sql.Append(" GROUP BY SYSTEM_ID  ");
            sql.Append(" ) AS MAX2_TME  ");
            sql.Append(" ON COL2_TME.SYSTEM_ID = MAX2_TME.SYSTEM_ID  ");
            sql.Append(" AND COL2_TME.SEQ = MAX2_TME.SEQ  ");
            sql.Append(" LEFT OUTER JOIN (  ");
            sql.Append(" SELECT SYSTEM_ID  ");
            sql.Append(" , SEQ  ");
            sql.Append(" , MAX(LAST_SBN_END_DATE) AS LAST_SBN_END_DATE  ");
            sql.Append(" FROM T_MANIFEST_DETAIL  ");
            sql.Append(" GROUP BY SYSTEM_ID  ");
            sql.Append(" , SEQ  ");
            sql.Append(" ) AS COL2_TMD  ");
            sql.Append(" ON COL2_TME.SYSTEM_ID = COL2_TMD.SYSTEM_ID  ");
            sql.Append(" AND COL2_TME.SEQ = COL2_TMD.SEQ  ");
            sql.Append(" LEFT OUTER JOIN M_GYOUSHA AS MG2 WITH(NOLOCK)  ");
            sql.Append(" ON COL2_TME.LAST_SBN_GYOUSHA_CD = MG2.GYOUSHA_CD  ");
            sql.Append(" AND MG2.DELETE_FLG = 'false'  ");
            sql.Append(" LEFT OUTER JOIN M_GENBA AS MGA2 WITH(NOLOCK)  ");
            sql.Append(" ON COL2_TME.LAST_SBN_GYOUSHA_CD = MGA2.GYOUSHA_CD  ");
            sql.Append(" AND COL2_TME.LAST_SBN_GENBA_CD = MGA2.GENBA_CD  ");
            sql.Append(" AND MGA2.DELETE_FLG = 'false'  ");
            sql.Append(" WHERE COL2_TME.DELETE_FLG = 'false'  ");
            sql.Append(" )COL_TME  ");
            sql.Append(" ON COL_TMR.NEXT_SYSTEM_ID = COL_TME.SYSTEM_ID  ");
            //電子マニ START
            sql.Append(" LEFT OUTER JOIN (  ");
            sql.Append(" SELECT DISTINCT COL_DR18E.SYSTEM_ID   ");
            sql.Append(" , COL_DR18E.SEQ  ");
            sql.Append(" , COL_DR18E.MANIFEST_ID  ");
            sql.Append(" FROM DT_R18_EX AS COL_DR18E WITH(NOLOCK)  ");
            sql.Append(" INNER JOIN (  ");
            sql.Append(" SELECT SYSTEM_ID  ");
            sql.Append(" , MAX(SEQ) AS SEQ  ");
            sql.Append(" FROM DT_R18_EX WITH(NOLOCK)  ");
            sql.Append(" WHERE DELETE_FLG = 'false'  ");
            sql.Append(" GROUP BY SYSTEM_ID  ");
            sql.Append(" ) MAX_DR18E  ");
            sql.Append(" ON COL_DR18E.SYSTEM_ID = MAX_DR18E.SYSTEM_ID  ");
            sql.Append(" AND COL_DR18E.SEQ = MAX_DR18E.SEQ  ");
            sql.Append(" INNER JOIN DT_MF_TOC AS DMT WITH(NOLOCK)  ");
            sql.Append(" ON COL_DR18E.KANRI_ID = DMT.KANRI_ID  ");
            sql.Append(" AND COL_DR18E.MANIFEST_ID = DMT.MANIFEST_ID  ");
            sql.Append(" INNER JOIN DT_R18 AS DR18 WITH(NOLOCK)  ");
            sql.Append(" ON DMT.KANRI_ID = DR18.KANRI_ID  ");
            sql.Append(" AND DMT.LATEST_SEQ = DR18.SEQ   ");
            sql.Append(" LEFT OUTER JOIN DT_R13_EX AS DR13E WITH(NOLOCK)  ");
            sql.Append(" ON COL_DR18E.SYSTEM_ID = DR13E.SYSTEM_ID  ");
            sql.Append(" AND COL_DR18E.SEQ = DR13E.SEQ  ");
            sql.Append(" LEFT OUTER JOIN M_GYOUSHA AS MG3 WITH(NOLOCK)  ");
            sql.Append(" ON DR13E.LAST_SBN_GYOUSHA_CD = MG3.GYOUSHA_CD  ");
            sql.Append(" AND MG3.DELETE_FLG = 'false'  ");
            sql.Append(" LEFT OUTER JOIN M_GENBA AS MGA3 WITH(NOLOCK)  ");
            sql.Append(" ON DR13E.LAST_SBN_GYOUSHA_CD = MGA3.GYOUSHA_CD  ");
            sql.Append(" AND DR13E.LAST_SBN_GENBA_CD = MGA3.GENBA_CD  ");
            sql.Append(" AND MGA3.DELETE_FLG = 'false'  ");
            sql.Append(" WHERE COL_DR18E.DELETE_FLG = 'false'  ");
            sql.Append(" )COL_DR18E  ");
            sql.Append(" ON COL_TMR.NEXT_SYSTEM_ID = COL_DR18E.SYSTEM_ID   ");
            sql.Append(" WHERE COL_TMR.DELETE_FLG = 'false'   ");
            //電子マニ END
            sql.Append(" )TMR   ");
            sql.Append(" ON T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID = TMR.FIRST_SYSTEM_ID   ");
            sql.Append(" AND TMR.FIRST_HAIKI_KBN_CD <> 4   ");
            // 紐付2次 END
            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句
            sql.Append(" WHERE ");
            sql.Append(" T_MANIFEST_ENTRY.DELETE_FLG = '0' ");

            //排出事業者
            if (!String.IsNullOrEmpty(this.form.HST_GYOUSHA_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.HST_GYOUSHA_CD = '" + this.form.HST_GYOUSHA_CD.Text + "' ");
            }

            //排出事業場
            if (!String.IsNullOrEmpty(this.form.HST_GENBA_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.HST_GENBA_CD = '" + this.form.HST_GENBA_CD.Text + "' ");
            }

            //処分受託者
            if (!String.IsNullOrEmpty(this.form.SBN_GYOUSHA_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.SBN_GYOUSHA_CD = '" + this.form.SBN_GYOUSHA_CD.Text + "' ");
            }

            //処分事業場
            if (!string.IsNullOrEmpty(this.form.SBN_GENBA_CD.Text))
            {
                sql.Append(" AND TMU.UPN_SAKI_GENBA_CD = '" + this.form.SBN_GENBA_CD.Text + "' ");
            }

            //処分方法
            if (!String.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_DETAIL.SBN_HOUHOU_CD = '" + this.form.SHOBUN_HOUHOU_CD.Text + "' ");
            }

            //運搬受託者（区間１）
            if (!String.IsNullOrEmpty(this.form.UPN_JYUTAKUSHA_CD1.Text))
            {
                sql.Append(" AND T_MANIFEST_UPN1.UPN_JYUTAKUSHA_CD = '" + this.form.UPN_JYUTAKUSHA_CD1.Text + "' ");
            }

            //運搬受託者（区間２）
            if (!String.IsNullOrEmpty(this.form.UPN_JYUTAKUSHA_CD2.Text))
            {
                sql.Append(" AND T_MANIFEST_UPN2.UPN_JYUTAKUSHA_CD = '" + this.form.UPN_JYUTAKUSHA_CD2.Text + "' ");
            }

            //運搬受託者（区間３）
            if (!String.IsNullOrEmpty(this.form.UPN_JYUTAKUSHA_CD3.Text))
            {
                sql.Append(" AND T_MANIFEST_UPN3.UPN_JYUTAKUSHA_CD = '" + this.form.UPN_JYUTAKUSHA_CD3.Text + "' ");
            }

            //廃棄物種類
            if (!String.IsNullOrEmpty(this.form.HAIKI_SHURUI_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = '" + this.form.HAIKI_SHURUI_CD.Text + "' ");
            }

            //廃棄物名称
            if (!String.IsNullOrEmpty(this.form.HAIKI_NAME_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_DETAIL.HAIKI_NAME_CD = '" + this.form.HAIKI_NAME_CD.Text + "' ");
            }

            //廃棄物区分
            if (!String.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = '" + this.form.HAIKI_KBN_CD.Text + "' ");
            }

            //一次二次区分
            if (!String.IsNullOrEmpty(this.form.MANIFEST_KBN.Text))
            {
                if (this.form.MANIFEST_KBN.Text == "1")
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.FIRST_MANIFEST_KBN = 0 ");
                }
                else if (this.form.MANIFEST_KBN.Text == "2")
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.FIRST_MANIFEST_KBN = 1 ");
                }
            }

            //交付年月日FROM
            if (!String.IsNullOrEmpty(this.form.KOUFU_DATE_FROM.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.KOUFU_DATE >= '" + this.form.KOUFU_DATE_FROM.Value.ToString() + "' ");
            }

            //交付年月日TO
            if (!String.IsNullOrEmpty(this.form.KOUFU_DATE_TO.Text))
            {
                sql.Append(" AND T_MANIFEST_ENTRY.KOUFU_DATE <= '" + this.form.KOUFU_DATE_TO.Value.ToString() + "' ");
            }

            //最終処分業者
            if (!string.IsNullOrEmpty(this.form.LAST_SHOBUN_GYOUSHA_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_DETAIL.LAST_SBN_GYOUSHA_CD = '" + this.form.LAST_SHOBUN_GYOUSHA_CD.Text + "' ");
            }

            //最終処分場所
            if (!String.IsNullOrEmpty(this.form.LAST_SHOBUN_GENBA_CD.Text))
            {
                sql.Append(" AND T_MANIFEST_DETAIL.LAST_SBN_GENBA_CD = '" + this.form.LAST_SHOBUN_GENBA_CD.Text + "' ");
            }

            // 以下の条件はORで繋ぐ
            bool orFlg = false;

            // 処分終了日
            string dateCondition = string.Empty;
            string date_from = string.Empty;
            string date_to = string.Empty;
            date_from = this.form.SBN_END_DATE_FROM.Value == null ? string.Empty : this.form.SBN_END_DATE_FROM.Value.ToString();
            date_to = this.form.SBN_END_DATE_TO.Value == null ? string.Empty : this.form.SBN_END_DATE_TO.Value.ToString();
            dateCondition = this.get_dateCondition(date_from, date_to, this.form.cbx_sbn.Checked, "T_MANIFEST_DETAIL.SBN_END_DATE");
            if (!string.IsNullOrEmpty(dateCondition))
            {
                sql.AppendFormat(" AND (( {0} ) ", dateCondition);
                orFlg = true;
            }

            // 運搬終了日（区間１）
            dateCondition = string.Empty;
            date_from = this.form.UPN_END_DATE1_FROM.Value == null ? string.Empty : this.form.UPN_END_DATE1_FROM.Value.ToString();
            date_to = this.form.UPN_END_DATE1_TO.Value == null ? string.Empty : this.form.UPN_END_DATE1_TO.Value.ToString();
            dateCondition = this.get_dateCondition(date_from, date_to, this.form.cbx_upn1.Checked, "T_MANIFEST_UPN1.UPN_END_DATE");
            if (!string.IsNullOrEmpty(dateCondition))
            {
                if (orFlg)
                {
                    sql.AppendFormat(" OR ( {0} ) ", dateCondition);
                }
                else
                {
                    sql.AppendFormat(" AND (( {0} ) ", dateCondition);
                    orFlg = true;
                }
            }

            // 運搬終了日（区間２）
            dateCondition = string.Empty;
            date_from = this.form.UPN_END_DATE2_FROM.Value == null ? string.Empty : this.form.UPN_END_DATE2_FROM.Value.ToString();
            date_to = this.form.UPN_END_DATE2_TO.Value == null ? string.Empty : this.form.UPN_END_DATE2_TO.Value.ToString();
            dateCondition = this.get_dateCondition(date_from, date_to, this.form.cbx_upn2.Checked, "T_MANIFEST_UPN2.UPN_END_DATE");
            if (!string.IsNullOrEmpty(dateCondition))
            {
                if (orFlg)
                {
                    sql.AppendFormat(" OR ( {0} ) ", dateCondition);
                }
                else
                {
                    sql.AppendFormat(" AND (( {0} ) ", dateCondition);
                    orFlg = true;
                }
            }

            // 運搬終了日（区間３）
            dateCondition = string.Empty;
            date_from = this.form.UPN_END_DATE3_FROM.Value == null ? string.Empty : this.form.UPN_END_DATE3_FROM.Value.ToString();
            date_to = this.form.UPN_END_DATE3_TO.Value == null ? string.Empty : this.form.UPN_END_DATE3_TO.Value.ToString();
            dateCondition = this.get_dateCondition(date_from, date_to, this.form.cbx_upn3.Checked, "T_MANIFEST_UPN3.UPN_END_DATE");
            if (!string.IsNullOrEmpty(dateCondition))
            {
                if (orFlg)
                {
                    sql.AppendFormat(" OR ( {0} ) ", dateCondition);
                }
                else
                {
                    sql.AppendFormat(" AND (( {0} ) ", dateCondition);
                    orFlg = true;
                }
            }

            //最終処分終了日
            dateCondition = string.Empty;
            date_from = this.form.LAST_SBN_END_DATE_FROM.Value == null ? string.Empty : this.form.LAST_SBN_END_DATE_FROM.Value.ToString();
            date_to = this.form.LAST_SBN_END_DATE_TO.Value == null ? string.Empty : this.form.LAST_SBN_END_DATE_TO.Value.ToString();
            dateCondition = this.get_dateCondition(date_from, date_to, this.form.cbx_lastSbn.Checked, "T_MANIFEST_DETAIL.LAST_SBN_END_DATE");
            if (!string.IsNullOrEmpty(dateCondition))
            {
                if (orFlg)
                {
                    sql.AppendFormat(" OR ( {0} ) ", dateCondition);
                }
                else
                {
                    sql.AppendFormat(" AND (( {0} ) ", dateCondition);
                    orFlg = true;
                }
            }

            if (orFlg)
            {
                sql.Append(" ) ");
            }
            #endregion

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");
        }

        /// <summary>
        /// 日付SQL
        /// </summary>
        private String get_dateCondition(String date_from, String date_to, bool checkFlg, String columnName)
        {
            StringBuilder dateCondition = new StringBuilder();
            //年月日（開始）
            if (date_from != "")
            {
                dateCondition.AppendFormat("                    {0} >= '{1}' ", new object[] { columnName, date_from });
            }

            //年月日（終了）
            if (date_to != "")
            {
                if (!string.IsNullOrEmpty(dateCondition.ToString()))
                {
                    dateCondition.Append(" AND ");
                }
                dateCondition.AppendFormat("                    {0} <= '{1}' ", new object[] { columnName, date_to });
            }

            if (checkFlg)
            {
                if (string.IsNullOrEmpty(dateCondition.ToString()))
                    dateCondition.AppendFormat("{0} IS NULL", columnName);
                else
                    dateCondition.AppendFormat("OR {0} IS NULL", columnName);
            }
            return dateCondition.ToString();
        }

        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                parentForm = (BusinessBaseForm)this.form.Parent;
                this.allControl = this.form.allControl;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                //this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション
                this.form.customDataGridView1.ColumnHeadersVisible = true;

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();

                // 検索結果一覧のDao初期化
                HkIchiran = DaoInitUtility.GetComponent<DAOClass>();
                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_IKKATSUKOUSIN));

                parentForm.bt_func4.Enabled = false;

                // 条件を初期化
                if (!jyoukenInit())
                {
                    ret = false;
                    return ret;
                }

                // 廃棄物区分によって、コントロール制御
                if (!HAIKI_KBN_CD_CHANGE("1"))
                {
                    ret = false;
                    return ret;
                }

                // 権限チェックによるボタン制御
                var enabled = Manager.CheckAuthority("G686", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                parentForm.bt_func1.Enabled = enabled;  // 一括入力
                parentForm.bt_func9.Enabled = enabled;  // 登録
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
        /// 条件を初期化
        /// </summary>
        internal bool jyoukenInit()
        {
            bool ret = true;
            try
            {
                // 排出事業者CD
                this.form.HST_GYOUSHA_CD.Text = string.Empty;
                // 排出事業者名
                this.form.HST_GYOUSHA_NAME.Text = string.Empty;
                // 排出事業場CD
                this.form.HST_GENBA_CD.Text = string.Empty;
                // 排出事業場名
                this.form.HST_GENBA_NAME.Text = string.Empty;
                // 処分受託者CD
                this.form.SBN_GYOUSHA_CD.Text = string.Empty;
                // 処分受託者名
                this.form.SBN_GYOUSHA_NAME.Text = string.Empty;
                // 処分事業場CD
                this.form.SBN_GENBA_CD.Text = string.Empty;
                // 処分事業場名
                this.form.SBN_GENBA_NAME.Text = string.Empty;
                // 処分方法CD
                this.form.SHOBUN_HOUHOU_CD.Text = string.Empty;
                // 処分方法名
                this.form.SHOBUN_HOUHOU_NAME.Text = string.Empty;
                // 運搬受託者（区間１）CD
                this.form.UPN_JYUTAKUSHA_CD1.Text = string.Empty;
                // 運搬受託者（区間１）名
                this.form.UPN_JYUTAKUSHA_NAME1.Text = string.Empty;
                // 運搬受託者（区間２）CD
                this.form.UPN_JYUTAKUSHA_CD2.Text = string.Empty;
                // 運搬受託者（区間２）名
                this.form.UPN_JYUTAKUSHA_NAME2.Text = string.Empty;
                // 運搬受託者（区間３）CD
                this.form.UPN_JYUTAKUSHA_CD3.Text = string.Empty;
                // 運搬受託者（区間３）名
                this.form.UPN_JYUTAKUSHA_NAME3.Text = string.Empty;
                // 廃棄物種類CD
                this.form.HAIKI_SHURUI_CD.Text = string.Empty;
                // 廃棄物種類名
                this.form.HAIKI_SHURUI_NAME.Text = string.Empty;
                // 廃棄物名称CD
                this.form.HAIKI_NAME_CD.Text = string.Empty;
                // 廃棄物名称名
                this.form.HAIKI_NAME.Text = string.Empty;
                // 最終処分業者CD
                this.form.LAST_SHOBUN_GYOUSHA_CD.Text = string.Empty;
                // 最終処分業者名
                this.form.LAST_SHOBUN_GYOUSHA_NAME.Text = string.Empty;
                // 最終処分現場CD
                this.form.LAST_SHOBUN_GENBA_CD.Text = string.Empty;
                // 最終処分現場名
                this.form.LAST_SHOBUN_GENBA_NAME.Text = string.Empty;
                // 廃棄物区分：1.産廃　2.建廃　3.積替
                this.form.HAIKI_KBN_CD.Text = "1";
                // 一次二次区分：1.一次　2.二次
                this.form.MANIFEST_KBN.Text = "1";
                // 交付年月日FROM
                this.form.KOUFU_DATE_FROM.Text = this.parentForm.sysDate.ToString();
                // 交付年月日TO
                this.form.KOUFU_DATE_TO.Text = this.parentForm.sysDate.ToString();
                // 処分終了日FROM
                this.form.SBN_END_DATE_FROM.Text = string.Empty;
                // 処分終了日TO
                this.form.SBN_END_DATE_TO.Text = string.Empty;
                // 運搬終了日（区間１）FROM
                this.form.UPN_END_DATE1_FROM.Text = string.Empty;
                // 運搬終了日（区間１）TO
                this.form.UPN_END_DATE1_TO.Text = string.Empty;
                // 運搬終了日（区間２）FROM
                this.form.UPN_END_DATE2_FROM.Text = string.Empty;
                // 運搬終了日（区間２）TO
                this.form.UPN_END_DATE2_TO.Text = string.Empty;
                // 運搬終了日（区間３）FROM
                this.form.UPN_END_DATE3_FROM.Text = string.Empty;
                // 運搬終了日（区間３）TO
                this.form.UPN_END_DATE3_TO.Text = string.Empty;
                // 最終処分終了日FROM
                this.form.LAST_SBN_END_DATE_FROM.Text = string.Empty;
                // 最終処分終了日TO
                this.form.LAST_SBN_END_DATE_TO.Text = string.Empty;
                // 日付一括入力
                this.form.txt_hitsuke.Text = "1";
                // 交付年月日TO
                this.form.kousinyoDate.Text = string.Empty;

                // 終了日未入力
                // 処分終了日
                this.form.cbx_sbn.Checked = true;
                // 処分終了日
                this.form.cbx_upn1.Checked = true;
                // 処分終了日
                this.form.cbx_upn2.Checked = false;
                // 処分終了日
                this.form.cbx_upn3.Checked = false;
                // 処分終了日
                this.form.cbx_lastSbn.Checked = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("jyoukenInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物区分によって、コントロール制御
        /// </summary>
        /// <param name="HAIKI_KBN_CD">廃棄物区分：1.産廃　2.建廃　3.積替</param>
        internal bool HAIKI_KBN_CD_CHANGE(string HAIKI_KBN_CD)
        {
            bool ret = true;
            try
            {
                switch (HAIKI_KBN_CD)
                {
                    case "3":
                        this.form.UPN_JYUTAKUSHA_CD2.Enabled = true;
                        this.form.cbtn_UPN_JYUTAKUSHA2.Enabled = true;
                        this.form.UPN_JYUTAKUSHA_CD3.Enabled = true;
                        this.form.cbtn_UPN_JYUTAKUSHA3.Enabled = true;
                        this.form.UPN_END_DATE2_FROM.Enabled = true;
                        this.form.UPN_END_DATE2_TO.Enabled = true;
                        this.form.UPN_END_DATE3_FROM.Enabled = true;
                        this.form.UPN_END_DATE3_TO.Enabled = true;
                        this.form.cbx_upn2.Enabled = true;
                        this.form.cbx_upn3.Enabled = true;
                        break;
                    case "2":
                        this.form.UPN_JYUTAKUSHA_CD2.Enabled = true;
                        this.form.cbtn_UPN_JYUTAKUSHA2.Enabled = true;
                        this.form.UPN_JYUTAKUSHA_CD3.Enabled = false;
                        this.form.cbtn_UPN_JYUTAKUSHA3.Enabled = false;
                        this.form.UPN_JYUTAKUSHA_CD3.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_NAME3.Text = string.Empty;
                        this.form.UPN_END_DATE2_FROM.Enabled = true;
                        this.form.UPN_END_DATE2_TO.Enabled = true;
                        this.form.UPN_END_DATE3_FROM.Enabled = false;
                        this.form.UPN_END_DATE3_TO.Enabled = false;
                        this.form.UPN_END_DATE3_FROM.Text = string.Empty;
                        this.form.UPN_END_DATE3_TO.Text = string.Empty;
                        this.form.cbx_upn2.Enabled = true;
                        this.form.cbx_upn3.Enabled = false;
                        this.form.cbx_upn3.Checked = false;
                        break;
                    case "1":
                        this.form.UPN_JYUTAKUSHA_CD2.Enabled = false;
                        this.form.cbtn_UPN_JYUTAKUSHA2.Enabled = false;
                        this.form.UPN_JYUTAKUSHA_CD3.Enabled = false;
                        this.form.cbtn_UPN_JYUTAKUSHA3.Enabled = false;
                        this.form.UPN_JYUTAKUSHA_CD2.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_NAME2.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_CD3.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_NAME3.Text = string.Empty;
                        this.form.UPN_END_DATE2_FROM.Enabled = false;
                        this.form.UPN_END_DATE2_TO.Enabled = false;
                        this.form.UPN_END_DATE3_FROM.Enabled = false;
                        this.form.UPN_END_DATE3_TO.Enabled = false;
                        this.form.UPN_END_DATE2_FROM.Text = string.Empty;
                        this.form.UPN_END_DATE2_TO.Text = string.Empty;
                        this.form.UPN_END_DATE3_FROM.Text = string.Empty;
                        this.form.UPN_END_DATE3_TO.Text = string.Empty;
                        this.form.cbx_upn2.Enabled = false;
                        this.form.cbx_upn2.Checked = false;
                        this.form.cbx_upn3.Enabled = false;
                        this.form.cbx_upn3.Checked = false;
                        break;
                    default:
                        this.form.UPN_JYUTAKUSHA_CD2.Enabled = false;
                        this.form.UPN_JYUTAKUSHA_CD3.Enabled = false;
                        this.form.UPN_JYUTAKUSHA_CD2.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_NAME2.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_CD3.Text = string.Empty;
                        this.form.UPN_JYUTAKUSHA_NAME3.Text = string.Empty;
                        this.form.UPN_END_DATE2_FROM.Enabled = false;
                        this.form.UPN_END_DATE2_TO.Enabled = false;
                        this.form.UPN_END_DATE3_FROM.Enabled = false;
                        this.form.UPN_END_DATE3_TO.Enabled = false;
                        this.form.UPN_END_DATE2_FROM.Text = string.Empty;
                        this.form.UPN_END_DATE2_TO.Text = string.Empty;
                        this.form.UPN_END_DATE3_FROM.Text = string.Empty;
                        this.form.UPN_END_DATE3_TO.Text = string.Empty;
                        this.form.cbx_upn2.Enabled = false;
                        this.form.cbx_upn2.Checked = false;
                        this.form.cbx_upn3.Enabled = false;
                        this.form.cbx_upn3.Checked = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("HAIKI_KBN_CD_CHANGE", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.SearchResult = this.form.Table;
                this.form.ShowData();
            }
            this.selectQuery = this.form.SelectQuery;
            this.orderByQuery = this.form.OrderByQuery;
            this.joinQuery = this.form.JoinQuery;
        }

        /// <summary>
        /// 紐付1次最終処分終了報告画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            r_framework.FormManager.FormManager.OpenFormWithAuth("G687", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }
        #endregion

        /// <summary>
        /// F1 一括入力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上有った場合判断
            bool updataflag = false;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        updataflag = true;
                    }
                }
            }

            //明細行が１件以上有った場合判断
            if (updataflag)
            {
                string errorMessage = string.Empty;

                string hizukename = "";
                if (this.form.radbtn_hitsuke1.Checked)
                {
                    hizukename = this.form.radbtn_hitsuke1.Text;
                }
                else if (this.form.radbtn_hitsuke2.Checked)
                {
                    hizukename = this.form.radbtn_hitsuke2.Text;
                }
                else if (this.form.radbtn_hitsuke3.Checked)
                {
                    hizukename = this.form.radbtn_hitsuke3.Text;
                }
                else if (this.form.radbtn_hitsuke4.Checked)
                {
                    hizukename = this.form.radbtn_hitsuke4.Text;
                }
                else if (this.form.radbtn_hitsuke5.Checked)
                {
                    hizukename = this.form.radbtn_hitsuke5.Text;
                }

                //日付一括更新]日付の値が入力かどうか判断。
                if (string.IsNullOrEmpty(this.form.kousinyoDate.Text.Trim()))
                {
                    //コードが存在しない場合エラー
                    this.MsgBox.MessageBoxShow("E048", "日付一括更新日");
                    this.form.kousinyoDate.Focus();
                    return;
                }

                //チェックされたマニフェストの日付を更新するかを確認する。
                DialogResult result = this.MsgBox.MessageBoxShow("C100", hizukename, this.form.kousinyoDate.Text.ToString());
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                this.MsgBox.MessageBoxShow("E051", "マニフェストの明細");
                return;
            }

            // DataGridViewの内容を書き換えるとソートが発生するので、DataSorceを書き換える
            string sortInfo = ((DataTable)this.form.customDataGridView1.DataSource).DefaultView.Sort;
            DataTable tempDataSorce = ((DataTable)this.form.customDataGridView1.DataSource).DefaultView.ToTable();
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (!Convert.ToBoolean(dgvRow.Cells[0].Value))
                {
                    continue;
                }

                if (this.form.radbtn_hitsuke1.Checked)
                {
                    if (!dgvRow.Cells["運搬終了日(区間1)"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["運搬終了日(区間1)"] = this.form.kousinyoDate.Value;
                    }
                }
                if (this.form.radbtn_hitsuke2.Checked)
                {
                    if (!dgvRow.Cells["運搬終了日(区間2)"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["運搬終了日(区間2)"] = this.form.kousinyoDate.Value;
                    }
                }
                if (this.form.radbtn_hitsuke3.Checked)
                {
                    if (!dgvRow.Cells["運搬終了日(区間3)"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["運搬終了日(区間3)"] = this.form.kousinyoDate.Value;
                    }
                }
                if (this.form.radbtn_hitsuke4.Checked)
                {
                    if (!dgvRow.Cells["処分終了日"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["処分終了日"] = this.form.kousinyoDate.Value;
                    }
                }
                if (this.form.radbtn_hitsuke5.Checked)
                {
                    if (!dgvRow.Cells["最終処分終了日"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["最終処分終了日"] = this.form.kousinyoDate.Value;
                    }
                }
            }

            // DataSorce変更に伴い、チェックボックスがソートに追従しない問題と、
            // 全選択チェックボックスのイベントが消える問題を対処
            bool isExistCHECKBOX = false;
            if (this.form.customDataGridView1.Columns.Contains("CHECKBOX"))
            {
                isExistCHECKBOX = true;
            }

            if (isExistCHECKBOX)
            {
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = false;
            }

            var tempCheckBoxCell = this.form.customDataGridView1.Columns["CHECKBOX"].HeaderCell as DataGridviewCheckboxHeaderCell;
            tempCheckBoxCell.OnCheckBoxClicked -= new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);

            this.form.customDataGridView1.DataSource = tempDataSorce;
            tempDataSorce.DefaultView.Sort = sortInfo;

            tempCheckBoxCell = this.form.customDataGridView1.Columns["CHECKBOX"].HeaderCell as DataGridviewCheckboxHeaderCell;
            tempCheckBoxCell.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);

            if (isExistCHECKBOX)
            {
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = true;
            }
            this.setEnabled();
        }

        /// <summary>
        /// F7 条件ｸﾘｱボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            // 条件を初期化
            jyoukenInit();

            // 廃棄物区分によって、コントロール制御
            HAIKI_KBN_CD_CHANGE("1");
            
            if (this.SearchResult != null)
            {
                this.SearchResult.Clear();
            }
            // ソート条件の初期化
            this.form.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// F8 検索ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            if (this.form.PatternNo == 0)
            {
                this.MsgBox.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            //必須チェック
            if (this.SearchCheck())
            {
                return;
            }

            if (this.DateCheck(this.form.KOUFU_DATE_FROM, this.form.KOUFU_DATE_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.SBN_END_DATE_FROM, this.form.SBN_END_DATE_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE1_FROM, this.form.UPN_END_DATE1_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE2_FROM, this.form.UPN_END_DATE2_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE3_FROM, this.form.UPN_END_DATE3_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.LAST_SBN_END_DATE_FROM, this.form.LAST_SBN_END_DATE_TO))
            {
                return;
            }

            if (this.selectQuery != null)
            {
                this.Search();
            }
        }

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = allControl.ToList();
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.SetErrorFocus();

                    LogUtility.DebugMethodEnd(isErr);
                    return isErr;
                }
                isErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// F9 登録ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            //必須チェック
            if (this.SearchCheck())
            {
                return;
            }

            if (this.DateCheck(this.form.KOUFU_DATE_FROM, this.form.KOUFU_DATE_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.SBN_END_DATE_FROM, this.form.SBN_END_DATE_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE1_FROM, this.form.UPN_END_DATE1_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE2_FROM, this.form.UPN_END_DATE2_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.UPN_END_DATE3_FROM, this.form.UPN_END_DATE3_TO))
            {
                return;
            }

            if (this.DateCheck(this.form.LAST_SBN_END_DATE_FROM, this.form.LAST_SBN_END_DATE_TO))
            {
                return;
            }

            //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上有った場合判断
            bool updataflag = false;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        updataflag = true;
                    }
                }
            }

            if (updataflag)
            {
                string errorMessage = string.Empty;

                //チェックされたマニフェストを更新するかを確認する。
                DialogResult result = this.MsgBox.MessageBoxShow("C101", "マニフェスト");
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                this.MsgBox.MessageBoxShow("E051", "更新するマニフェスト");
                return;
            }

            //DataGridViewのデータを取得する。
            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            if (dbkoshin != null)
            {
                if (dbkoshin.Rows.Count > 0)
                {
                    // 更新処理
                    var registResult = this.Touroku();
                    if (true == registResult)
                    {
                        //更新後、DataGridViewを更新する。
                        this.Search();
                    }
                }
            }
        }

        /// <summary>
        /// F10 並替移動ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            // CHECKBOXカラムが存在しないタイミングもあるので、判定変数を用意
            bool isExistCHECKBOX = false;
            if (this.form.customDataGridView1.Columns.Contains("CHECKBOX"))
            {
                isExistCHECKBOX = true;
            }

            if (isExistCHECKBOX)
            {
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = false;
            }

            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            if (isExistCHECKBOX)
            {
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = true;
            }
            this.setEnabled();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        /// <summary>
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderForm hs)
        {
            this.headForm = hs;
        }

        public void ch_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (e.CheckedState)
                {
                    dgvRow.Cells[0].Value = true;
                }
                else
                {
                    dgvRow.Cells[0].Value = false;
                }
            }
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <returns></returns>
        private bool Touroku()
        {
            LogUtility.DebugMethodStart();
            try
            {
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    List<DTOClass> dtoList = new List<DTOClass>();
                    foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                    {
                        if (dgvRow.Cells[0].Value != null)
                        {
                            if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                            {
                                //システムID(全般･マニ返却日)
                                String SystemID = dgvRow.Cells["SYSTEM_ID"].Value.ToString();

                                //枝番(全般)
                                String Seq = dgvRow.Cells["SEQ"].Value.ToString();

                                //枝番(マニ返却日)
                                String SeqRD = dgvRow.Cells["TMRD_SEQ"].Value.ToString();

                                //廃棄物区分
                                String HAIKI_KBN_CD = dgvRow.Cells["HAIKI_KBN_CD"].Value.ToString();

                                int maniFlag = 1;
                                if ("True".Equals(dgvRow.Cells["MANIFEST_KBN"].Value.ToString()))
                                {
                                    maniFlag = 2;
                                }

                                byte[] TIME_STAMP_ENTRY = null;

                                byte[] TIME_STAMP_RET_DATE = null;

                                //タイムスタンプ
                                if (dgvRow.Cells["TIME_STAMP_ENTRY"].Value.ToString() != string.Empty)
                                {
                                    int data2 = (int)dgvRow.Cells["TIME_STAMP_ENTRY"].Value;
                                    byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                                    TIME_STAMP_ENTRY = d;
                                }

                                //マニ返却日タイムスタンプ
                                if (dgvRow.Cells["TIME_STAMP_RET_DATE"].Value.ToString() != string.Empty)
                                {
                                    int data2 = (int)dgvRow.Cells["TIME_STAMP_RET_DATE"].Value;
                                    byte[] d = Shougun.Core.Common.BusinessCommon.Utility.ConvertStrByte.In32ToByteArray(data2);
                                    TIME_STAMP_RET_DATE = d;
                                }

                                bool ariFlag = false;
                                foreach (var dto in dtoList)
                                {
                                    if (dto.SYSTEM_ID.Value == Convert.ToInt64(SystemID) && dto.SEQ.Value == Convert.ToInt32(Seq))
                                    {
                                        if (dgvRow.Cells["DETAIL_SYSTEM_ID"].Value != null
                                        && !string.IsNullOrEmpty(dgvRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                                        {
                                            DETAIL detail = new DETAIL();
                                            detail.DETAIL_SYSTEM_ID = Convert.ToInt64(dgvRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                                            if (dgvRow.Cells["処分終了日"].Value.ToString() != string.Empty)
                                            {
                                                detail.SBN_END_DATE = SqlDateTime.Parse(dgvRow.Cells["処分終了日"].Value.ToString());
                                            }
                                            if (dgvRow.Cells["最終処分終了日"].Value.ToString() != string.Empty)
                                            {
                                                detail.LAST_SBN_END_DATE = SqlDateTime.Parse(dgvRow.Cells["最終処分終了日"].Value.ToString());
                                            }
                                            dto.detailList.Add(detail);
                                        }
                                        ariFlag = true;
                                    }
                                }

                                if (!ariFlag)
                                {

                                    DTOClass dtoNew = new DTOClass();
                                    dtoNew.SYSTEM_ID = Convert.ToInt64(SystemID);
                                    dtoNew.SEQ = Convert.ToInt32(Seq);
                                    dtoNew.HAIKI_KBN_CD = HAIKI_KBN_CD;
                                    dtoNew.maniFlag = maniFlag;
                                    if (!String.IsNullOrEmpty(SeqRD))
                                    {
                                        dtoNew.SeqRD = Convert.ToInt32(SeqRD);
                                    }
                                    dtoNew.UPN_JYUTAKUSHA_CD1 = dgvRow.Cells["UPN_JYUTAKUSHA_CD1"].Value.ToString();
                                    dtoNew.UPN_JYUTAKUSHA_CD2 = dgvRow.Cells["UPN_JYUTAKUSHA_CD2"].Value.ToString();
                                    dtoNew.TIME_STAMP_ENTRY = TIME_STAMP_ENTRY;
                                    dtoNew.TIME_STAMP_RET_DATE = TIME_STAMP_RET_DATE;
                                    if (dgvRow.Cells["運搬終了日(区間1)"].Value.ToString() != string.Empty)
                                    {
                                        dtoNew.UPN_END_DATE1 = SqlDateTime.Parse(dgvRow.Cells["運搬終了日(区間1)"].Value.ToString());
                                    }
                                    if (dgvRow.Cells["運搬終了日(区間2)"].Value.ToString() != string.Empty)
                                    {
                                        dtoNew.UPN_END_DATE2 = SqlDateTime.Parse(dgvRow.Cells["運搬終了日(区間2)"].Value.ToString());
                                    }
                                    if (dgvRow.Cells["運搬終了日(区間3)"].Value.ToString() != string.Empty)
                                    {
                                        dtoNew.UPN_END_DATE3 = SqlDateTime.Parse(dgvRow.Cells["運搬終了日(区間3)"].Value.ToString());
                                    }

                                    if (dgvRow.Cells["DETAIL_SYSTEM_ID"].Value != null
                                        && !string.IsNullOrEmpty(dgvRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                                    {
                                        DETAIL detail = new DETAIL();
                                        detail.DETAIL_SYSTEM_ID = Convert.ToInt64(dgvRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                                        if (dgvRow.Cells["処分終了日"].Value.ToString() != string.Empty)
                                        {
                                            detail.SBN_END_DATE = SqlDateTime.Parse(dgvRow.Cells["処分終了日"].Value.ToString());
                                        }
                                        if (dgvRow.Cells["最終処分終了日"].Value.ToString() != string.Empty)
                                        {
                                            detail.LAST_SBN_END_DATE = SqlDateTime.Parse(dgvRow.Cells["最終処分終了日"].Value.ToString());
                                        }

                                        dtoNew.detailList.Add(detail);
                                    }
                                    dtoList.Add(dtoNew);
                                }
                            }
                        }
                    }

                    foreach (var dto in dtoList)
                    {
                        mlogic.LogicalEntityDel(dto.SYSTEM_ID.Value.ToString(), dto.SEQ.Value.ToString(), dto.TIME_STAMP_ENTRY);

                        if (!dto.SeqRD.IsNull)
                        {
                            mlogic.LogicalRetDateDel(dto.SYSTEM_ID.Value.ToString(), dto.SeqRD.Value.ToString(), dto.TIME_STAMP_RET_DATE);
                        }

                        //データ登録
                        T_MANIFEST_ENTRY entryDto = new T_MANIFEST_ENTRY();
                        entryDto.SYSTEM_ID = dto.SYSTEM_ID;
                        entryDto.SEQ = dto.SEQ;
                        string oldCREATE_USER = string.Empty;
                        SqlDateTime oldCREATE_DATE = DateTime.Now;
                        string oldCREATE_PC = string.Empty;
                        T_MANIFEST_ENTRY updateData = this.MANIFEST_ENTRYDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateData != null)
                        {
                            //入力Entityの作成情報を設定
                            oldCREATE_USER = updateData.CREATE_USER;
                            oldCREATE_DATE = updateData.CREATE_DATE;
                            oldCREATE_PC = updateData.CREATE_PC;
                            // 更新者情報設定
                            var dataBinderLogicEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_ENTRY>(updateData);
                            dataBinderLogicEntry.SetSystemProperty(updateData, true);
                            updateData.CREATE_USER = oldCREATE_USER;
                            updateData.CREATE_DATE = oldCREATE_DATE;
                            updateData.CREATE_PC = oldCREATE_PC;
                            //updateData.DELETE_FLG = true;
                            //int CntUpd = this.MANIFEST_ENTRYDao.Update(updateData);
                            updateData.SEQ = updateData.SEQ + 1;
                            updateData.DELETE_FLG = false;
                            if (dto.HAIKI_KBN_CD == "2")
                            {
                                if (!string.IsNullOrEmpty(dto.UPN_JYUTAKUSHA_CD2))
                                {
                                    updateData.SBN_JYURYOU_DATE = dto.UPN_END_DATE2;
                                }
                                else
                                {
                                    updateData.SBN_JYURYOU_DATE = dto.UPN_END_DATE1;
                                }
                            }
                            int CntUpd = this.MANIFEST_ENTRYDao.Insert(updateData);
                        }

                        T_MANIFEST_PRT updatePRTData = this.MANIFEST_PRTDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updatePRTData != null)
                        {
                            updatePRTData.SEQ = updatePRTData.SEQ + 1;
                            int CntUpd = this.MANIFEST_PRTDao.Insert(updatePRTData);
                        }

                        T_MANIFEST_UPN[] updateUPNData = this.MANIFEST_UPNDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateUPNData != null)
                        {
                            foreach (T_MANIFEST_UPN getData in updateUPNData)
                            {
                                getData.SEQ = getData.SEQ + 1;
                                if (getData.UPN_ROUTE_NO == 1)
                                {
                                    getData.UPN_END_DATE = dto.UPN_END_DATE1;
                                }
                                else if (getData.UPN_ROUTE_NO == 2)
                                {
                                    getData.UPN_END_DATE = dto.UPN_END_DATE2;
                                }
                                else if (getData.UPN_ROUTE_NO == 3)
                                {
                                    getData.UPN_END_DATE = dto.UPN_END_DATE3;
                                }
                                int CntUpd = this.MANIFEST_UPNDao.Insert(getData);
                            }
                        }

                        T_MANIFEST_DETAIL[] updateDETAILData = this.MANIFEST_DETAILDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateDETAILData != null && updateDETAILData.Length > 0)
                        {
                            foreach (T_MANIFEST_DETAIL getData in updateDETAILData)
                            {
                                var list = dto.detailList.Where(t => t.DETAIL_SYSTEM_ID.Value == getData.DETAIL_SYSTEM_ID.Value).ToList();
                                if (list != null && list.Count > 0)
                                {
                                    getData.SBN_END_DATE = list[0].SBN_END_DATE;
                                    getData.LAST_SBN_END_DATE = list[0].LAST_SBN_END_DATE;
                                }
                                getData.SEQ = getData.SEQ + 1;
                                int CntUpd = this.MANIFEST_DETAILDao.Insert(getData);
                                if (list != null && list.Count > 0)
                                {
                                    if (dto.maniFlag == 2)
                                    {
                                        this.mlogic.UpdateFirstManiDetail(getData.DETAIL_SYSTEM_ID.Value.ToString(), dto.HAIKI_KBN_CD);
                                    }

                                }
                            }
                        }

                        T_MANIFEST_DETAIL_PRT[] updateDETAILPRTData = this.MANIFEST_DETAIL_PRTDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateDETAILPRTData != null && updateDETAILPRTData.Length > 0)
                        {
                            foreach (T_MANIFEST_DETAIL_PRT getData in updateDETAILPRTData)
                            {
                                getData.SEQ = getData.SEQ + 1;
                                int CntUpd = this.MANIFEST_DETAIL_PRTDao.Insert(getData);
                            }
                        }

                        T_MANIFEST_KP_KEIJYOU[] updateKPKEIJYOUData = this.MANIFEST_KP_KEIJYOUDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateKPKEIJYOUData != null && updateKPKEIJYOUData.Length > 0)
                        {
                            foreach (T_MANIFEST_KP_KEIJYOU getData in updateKPKEIJYOUData)
                            {
                                getData.SEQ = getData.SEQ + 1;
                                int CntUpd = this.MANIFEST_KP_KEIJYOUDao.Insert(getData);
                            }
                        }

                        T_MANIFEST_KP_NISUGATA[] updateKPNISUGATAData = this.MANIFEST_KP_NISUGATADao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateKPNISUGATAData != null && updateKPNISUGATAData.Length > 0)
                        {
                            foreach (T_MANIFEST_KP_NISUGATA getData in updateKPNISUGATAData)
                            {
                                getData.SEQ = getData.SEQ + 1;
                                int CntUpd = this.MANIFEST_KP_NISUGATADao.Insert(getData);
                            }
                        }

                        T_MANIFEST_KP_SBN_HOUHOU[] updateKPSBNHOUHOUData = this.MANIFEST_KP_SBN_HOUHOUDao.GetDataByCd(entryDto.SYSTEM_ID, entryDto.SEQ);
                        if (updateKPSBNHOUHOUData != null && updateKPSBNHOUHOUData.Length > 0)
                        {
                            foreach (T_MANIFEST_KP_SBN_HOUHOU getData in updateKPSBNHOUHOUData)
                            {
                                getData.SEQ = getData.SEQ + 1;
                                int CntUpd = this.MANIFEST_KP_SBN_HOUHOUDao.Insert(getData);
                            }
                        }

                        T_MANIFEST_RET_DATE updateRETDATEData = this.MANIFEST_RET_DATEDao.GetDataByCd(entryDto.SYSTEM_ID, dto.SeqRD);
                        if (updateRETDATEData != null)
                        {
                            //入力Entityの作成情報を設定
                            oldCREATE_USER = updateRETDATEData.CREATE_USER;
                            oldCREATE_DATE = updateRETDATEData.CREATE_DATE;
                            oldCREATE_PC = updateRETDATEData.CREATE_PC;
                            // 更新者情報設定
                            var dataBinderLogicEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_RET_DATE>(updateRETDATEData);
                            dataBinderLogicEntry.SetSystemProperty(updateRETDATEData, true);
                            updateRETDATEData.CREATE_USER = oldCREATE_USER;
                            updateRETDATEData.CREATE_DATE = oldCREATE_DATE;
                            updateRETDATEData.CREATE_PC = oldCREATE_PC;
                            updateRETDATEData.SEQ = updateRETDATEData.SEQ + 1;
                            updateRETDATEData.DELETE_FLG = false;
                            int CntUpd = this.MANIFEST_RET_DATEDao.Insert(updateRETDATEData);
                        }
                    }
                    tran.Commit();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException notSingleRowUpdateRuntimeException)
            {
                LogUtility.Debug(notSingleRowUpdateRuntimeException.Message);

                this.MsgBox.MessageBoxShow("E080", "");

                return false;
            }
            catch (SQLRuntimeException sqlRuntimeException)
            {
                LogUtility.Debug(sqlRuntimeException.Message);

                this.MsgBox.MessageBoxShow("E093", "");

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex.Message);

                this.MsgBox.MessageBoxShow("E245", "");

                return false;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        /// <summary>
        /// 業者チェック(排出事業者、運搬受託者、処分受託者、最終処分業者) 
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkGyosya(object obj, string colname, string colname2 = "")
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(obj, colname, colname2);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt.Text;
                Serch.GYOUSHAKBN_MANI = "True";
                Serch.ISNOT_NEED_DELETE_FLG = true;
                //最終処分業者の場合、最終処分場区分の条件を追加した
                if (!string.IsNullOrEmpty(colname2))
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "True";
                }
                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGenbaAll(Serch);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][colname].ToString() == "True")
                        {
                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                    }
                }
                this.MsgBox.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyosya", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 業者マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="gyoshaCd">業者CD</param>
        /// <param name="gyoshaName">業者名</param>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int SetAddressGyousha(object gyoshaCd, object gyoshaName)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(gyoshaCd, gyoshaName);

                TextBox txt_gyoshaCd = (TextBox)gyoshaCd;
                TextBox txt_gyoshaName = (TextBox)gyoshaName;

                if (txt_gyoshaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGyoushaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = true;

                this.SearchResult = new DataTable();
                DataTable dt = mlogic.GetGyousha(Serch);

                if (dt.Rows.Count > 0)
                {
                    if (txt_gyoshaName != null)
                    {
                        txt_gyoshaName.Text = dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                    }
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressGyousha", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場チェック(排出事業者、運搬受託者、処分受託者、最終処分業者) 
        /// </summary>
        /// <param name="genba">現場CD</param>
        /// <param name="gyosya">事業者CD</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <param name="colname2">[処分事業場専用]チェックカラム名称2</param>
        /// <param name="genba">[処分事業場専用]現場名</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkJigyouba(object genba, object gyosya, string colname, string colname2 = "", object genbaName = null)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(genba, gyosya, colname, colname2, genbaName);

                TextBox txt1 = (TextBox)genba;
                TextBox txt2 = (TextBox)gyosya;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (txt2.Text == string.Empty)
                {
                    switch (colname)
                    {
                        case "HAISHUTSU_NIZUMI_GENBA_KBN":
                            this.MsgBox.MessageBoxShow("E051", "排出事業者");
                            break;

                        case "SHOBUN_NIOROSHI_GENBA_KBN":
                            this.MsgBox.MessageBoxShow("E051", "処分受託者");
                            break;

                        case "TSUMIKAEHOKAN_KBN":
                            this.MsgBox.MessageBoxShow("E051", "積替保管業者");
                            break;

                        case "SAISHUU_SHOBUNJOU_KBN":
                            this.MsgBox.MessageBoxShow("E051", "最終処分業者");
                            break;
                    }
                    txt1.Text = string.Empty;
                    txt1.Focus();
                    txt1.SelectAll();

                    ret = 2;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GENBA_CD = txt1.Text;
                Serch.GYOUSHA_CD = txt2.Text;
                Serch.ISNOT_NEED_DELETE_FLG = true;

                this.SearchResult = new DataTable();
                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.MsgBox.MessageBoxShow("E020", "現場");
                        break;

                    case 1:
                        if (dt.Rows[0][colname].ToString() == "True" && string.IsNullOrEmpty(colname2))
                        {
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        else if (!string.IsNullOrEmpty(colname2) && (dt.Rows[0][colname].ToString() == "True" || dt.Rows[0][colname2].ToString() == "True"))
                        {
                            /* 処分事業場用(現場の名称も同時に設定) */
                            txt1.Text = dt.Rows[0]["GENBA_CD"].ToString();
                            txt2.Text = dt.Rows[0]["GYOUSHA_CD"].ToString();
                            ((TextBox)genbaName).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                            ret = 0;
                            LogUtility.DebugMethodEnd(ret);
                            return ret;
                        }
                        this.MsgBox.MessageBoxShow("E020", "現場");
                        break;

                    default:
                        switch (colname)
                        {
                            case "HAISHUTSU_NIZUMI_GENBA_KBN":
                                this.MsgBox.MessageBoxShow("E034", "排出事業者");
                                break;

                            case "SAISHUU_SHOBUNJOU_KBN":
                                this.MsgBox.MessageBoxShow("E034", "最終処分の業者");
                                break;

                            case "SHOBUN_NIOROSHI_GENBA_KBN":
                                this.MsgBox.MessageBoxShow("E034", "処分受託者");
                                break;

                            case "TSUMIKAEHOKAN_KBN":
                                this.MsgBox.MessageBoxShow("E034", "積替保管業者");
                                break;

                            case "UNPAN_JUTAKUSHA_KAISHA_KBN":
                                this.MsgBox.MessageBoxShow("E034", "運搬の受託者");
                                break;
                        }
                        break;
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();//システム情報のDao
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        // 2014.03.25 システム情報から取得したマニフェスト情報使用区分。by 胡 end

        #region checkbox全選択処理

        /// <summary>
        /// checkbox全選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
                if (col is DataGridViewCheckBoxColumn)
                {
                    DataGridviewCheckboxHeaderCell header = col.HeaderCell as DataGridviewCheckboxHeaderCell;
                    if (header != null)
                    {
                        header.MouseClick(e);
                    }
                }

                if (this.form.customDataGridView1.CurrentCell != null)
                {
                    int colIndex = this.form.customDataGridView1.CurrentCell.ColumnIndex;
                    int rowIndex = this.form.customDataGridView1.CurrentCell.RowIndex;
                    if (colIndex == 0)
                    {
                        if (this.form.customDataGridView1.Rows.Count == 1)
                        {
                            if (this.form.customDataGridView1.Rows[rowIndex].Cells["処分終了日"].ReadOnly)
                            {
                                this.form.customDataGridView1.CurrentCell = null;
                            }
                            else
                            {
                                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[rowIndex].Cells["処分終了日"];
                            }
                        }
                        else if (this.form.customDataGridView1.Rows.Count > 1 && this.form.customDataGridView1.Rows.Count != rowIndex + 1)
                        {
                            //現在のセルを表示
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[colIndex, rowIndex + 1];
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[colIndex, rowIndex];
                        }
                        else if (this.form.customDataGridView1.Rows.Count == rowIndex + 1)
                        {
                            //現在のセルを表示
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[colIndex, rowIndex - 1];
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1[colIndex, rowIndex];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// 一覧の行初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.form.customDataGridView1 == null || this.form.customDataGridView1.Rows.Count == 0 || searching)
            {
                return;
            }
        }

        /// <summary>
        /// データグリッドのセルのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customDataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var columnName = this.form.customDataGridView1.Columns[e.ColumnIndex].Name;
                var cellValue = this.form.customDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                string system_id = this.form.customDataGridView1.Rows[e.RowIndex].Cells["SYSTEM_ID"].Value.ToString();
                string seq = this.form.customDataGridView1.Rows[e.RowIndex].Cells["SEQ"].Value.ToString();

                if (columnName == "運搬終了日(区間1)" || columnName == "運搬終了日(区間2)" || columnName == "運搬終了日(区間3)")
                {
                    for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                    {
                        if (i == e.RowIndex)
                        {
                            continue;
                        }
                        if (system_id.Equals(this.form.customDataGridView1.Rows[i].Cells["SYSTEM_ID"].Value.ToString())
                            && seq.Equals(this.form.customDataGridView1.Rows[i].Cells["SEQ"].Value.ToString()))
                        {
                            this.form.customDataGridView1.Rows[i].Cells[e.ColumnIndex].Value = cellValue;
                        }
                    }
                }
            }
        }

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(CustomDateTimePicker obj_from, CustomDateTimePicker obj_to)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (obj_from == null || obj_to == null)
            {
                return false;
            }

            obj_from.BackColor = Constans.NOMAL_COLOR;
            obj_to.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(obj_from.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(obj_to.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(obj_from.Text);
            DateTime date_to = DateTime.Parse(obj_to.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                obj_from.IsInputErrorOccured = true;
                obj_to.IsInputErrorOccured = true;
                obj_from.BackColor = Constans.ERROR_COLOR;
                obj_to.BackColor = Constans.ERROR_COLOR;
                msgLogic.MessageBoxShow("E030", obj_from.DisplayItemName, obj_to.DisplayItemName);
                obj_from.Focus();
                return true;
            }

            return false;
        }

        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.KOUFU_DATE_FROM;
            var ToTextBox = this.form.KOUFU_DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 現場マスタから住所情報を取得してTextBoxに設定
        /// </summary>
        /// <param name="NameFlg">現場名 部分採用 All:正式名称1+正式名称2、Part1:正式名称1のみ</param>
        /// <param name="txt_gyoshaCd">業者CD</param>
        /// <param name="txt_JigyoubaCd">現場CD</param>
        /// <param name="txt_JigyoubaName">現場名</param>
        /// <param name="HAISHUTSU_NIZUMI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SAISHUU_SHOBUNJOU_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="SHOBUN_NIOROSHI_GENBA_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="TSUMIKAEHOKAN_KBN">区分 trueだと1でマスタを検索</param>
        /// <param name="ISNOT_NEED_DELETE_FLG">削除チェックいるかどうかの判断フラッグ</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int SetAddressJigyouba(string NameFlg, CustomTextBox txt_gyoshaCd, CustomTextBox txt_JigyoubaCd, CustomTextBox txt_JigyoubaName
            , bool HAISHUTSU_NIZUMI_GENBA_KBN
            , bool SAISHUU_SHOBUNJOU_KBN
            , bool SHOBUN_NIOROSHI_GENBA_KBN
            , bool TSUMIKAEHOKAN_KBN
            , bool ISNOT_NEED_DELETE_FLG = false
            )
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(NameFlg, txt_gyoshaCd, txt_JigyoubaCd, txt_JigyoubaName
                    , HAISHUTSU_NIZUMI_GENBA_KBN, SAISHUU_SHOBUNJOU_KBN, SHOBUN_NIOROSHI_GENBA_KBN, TSUMIKAEHOKAN_KBN, ISNOT_NEED_DELETE_FLG);

                //空
                if (txt_gyoshaCd.Text == string.Empty || txt_JigyoubaCd.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new CommonGenbaDtoCls();
                Serch.GYOUSHA_CD = txt_gyoshaCd.Text;
                Serch.GENBA_CD = txt_JigyoubaCd.Text;
                Serch.ISNOT_NEED_DELETE_FLG = ISNOT_NEED_DELETE_FLG;

                //区分
                if (HAISHUTSU_NIZUMI_GENBA_KBN)
                {
                    Serch.HAISHUTSU_NIZUMI_GYOUSHA_KBN = "1";
                    Serch.HAISHUTSU_NIZUMI_GENBA_KBN = "1";
                }
                if (SAISHUU_SHOBUNJOU_KBN)
                {
                    Serch.SAISHUU_SHOBUNJOU_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    Serch.SHOBUN_NIOROSHI_GENBA_KBN = "1";
                    Serch.SHOBUN_NIOROSHI_GYOUSHA_KBN = "1";
                }
                if (TSUMIKAEHOKAN_KBN)
                {
                    Serch.TSUMIKAEHOKAN_KBN = "1";
                }

                DataTable dt = this.mlogic.GetGenba(Serch);
                switch (dt.Rows.Count)
                {
                    case 1://正常
                        break;

                    default://エラー
                        ret = 2;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                }

                //現場名
                if (txt_JigyoubaName != null)
                {

                    switch (NameFlg)
                    {
                        case "All"://「正式名称1 + 正式名称2」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME"].ToString();
                            break;

                        case "Part1"://「正式名称1」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME1"].ToString();
                            break;

                        case "Ryakushou_Name"://「略称名」をセットする。
                            txt_JigyoubaName.Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAddressJigyouba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="haikibutuShurui">廃棄物種類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuShurui(object haikiKbn, object haikibutuShurui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikiKbn, haikibutuShurui);

                TextBox txt1 = (TextBox)haikiKbn;
                TextBox txt2 = (TextBox)haikibutuShurui;

                //空
                if (txt2.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_HAIKI_SHURUI Serch = new M_HAIKI_SHURUI();
                Serch.HAIKI_KBN_CD = Convert.ToInt16(txt1.Text);
                Serch.HAIKI_SHURUI_CD = txt2.Text;

                Serch = this.dao_HaikiShurui.GetDataByCd(Serch);
                if (Serch != null)
                {
                    this.form.HAIKI_SHURUI_NAME.Text = Serch.HAIKI_SHURUI_NAME_RYAKU;
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E020", "廃棄物種類");
                }
                txt2.Focus();
                txt2.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuShurui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物名称チェック
        /// </summary>
        /// <param name="haikibutuShurui">廃棄物名称CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuName(object haikibutuName)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuName);

                TextBox txt1 = (TextBox)haikibutuName;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_HAIKI_NAME Serch = this.dao_HaikiName.GetDataByCd(txt1.Text);
                if (Serch != null)
                {
                    this.form.HAIKI_NAME.Text = Serch.HAIKI_NAME_RYAKU;
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E020", "廃棄物名称");
                }
                txt1.Focus();
                txt1.SelectAll();
                ret = 2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuName", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
    }
}