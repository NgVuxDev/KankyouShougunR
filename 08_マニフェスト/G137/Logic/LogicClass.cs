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
using Shougun.Core.PaperManifest.HenkyakuIchiran;
using Shougun.Core.PaperManifest.HenkyakuIchiran.DAO;
using Shougun.Core.Common.BusinessCommon.Accessor;

namespace Shougun.Core.PapeMranifest.HenkyakuIchiran
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
        /// パターン一覧のDao
        /// </summary>
        private T_MANIFEST_RET_DATE_daocls t_manifest_ret_date_daocls;

        /// <summary>
        /// 現場Dao
        /// </summary>
        private MGENBADao genbaDao;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private MGYOUSHADao gyoushaDao;

        /// <summary>
        /// 取引先Dao
        /// </summary>
        private MTORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 共通
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        internal HeaderForm headForm;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// List<T_MANIFEST_RET_DATE> mmanifestretdateMsListInsert
        /// </summary>
        private List<T_MANIFEST_RET_DATE> mmanifestretdateMsListInsert;

        /// <summary>
        /// List<T_MANIFEST_RET_DATE> mmanifestretdateMsList
        /// </summary>
        private List<T_MANIFEST_RET_DATE> mmanifestretdateMsList;

        /// <summary>
        /// List<T_MANIFEST_ENTRY> mmanifestentryMsList
        /// </summary>
        private List<T_MANIFEST_ENTRY> mmanifestentryMsList;    // SYSTEM_IDとSEQ保持用

        // システム情報から取得したマニフェスト情報使用区分
        private int intManifest_Use_A;
        private int intManifest_Use_B1;
        private int intManifest_Use_B2;
        private int intManifest_Use_B4;
        private int intManifest_Use_B6;
        private int intManifest_Use_C1;
        private int intManifest_Use_C2;
        private int intManifest_Use_D;
        private int intManifest_Use_E;

        /// <summary>
        /// 
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.HenkyakuIchiran.Setting.ButtonSetting.xml";

        ///// <summary>
        /// バーコードリーダー用
        /// </summary>
        public DataTable SearchBarcodeResult { get; set; }

        /// <summary>
        /// バーコードフラグ
        /// </summary>
        public bool barcodeFlg;

        /// <summary>
        /// バーコード下段判定キー
        /// </summary>
        public string barcodeGedanKey = "00000000";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string mCharacterLimitList;

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
        /// 全現場マスタ
        /// </summary>
        private List<M_GENBA> allGenba;

        /// <summary>
        /// 
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        private List<string> lstGyoushaGenbaCd = new List<string>(); //CongBinh 20200330 #134989

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.HkIchiran = DaoInitUtility.GetComponent<DAOClass>();
            t_manifest_ret_date_daocls = DaoInitUtility.GetComponent<T_MANIFEST_RET_DATE_daocls>();

            this.MANIFEST_ENTRYDao = DaoInitUtility.GetComponent<T_MANIFEST_ENTRYdaocls>(); //マニフェストDao
            this.MANIFEST_DETAILDao = DaoInitUtility.GetComponent<T_MANIFEST_DETAILdaocls>(); //マニフェスト明細Dao
            this.MANIFEST_UPNDao = DaoInitUtility.GetComponent<T_MANIFEST_UPNdaocls>(); //マニフェスト運搬Dao
            this.MANIFEST_PRTDao = DaoInitUtility.GetComponent<T_MANIFEST_PRTdaocls>(); //マニフェスト印字Dao
            this.MANIFEST_DETAIL_PRTDao = DaoInitUtility.GetComponent<T_MANIFEST_DETAIL_PRTdaocls>(); //マニフェスト印字詳細Dao
            this.MANIFEST_KP_KEIJYOUDao = DaoInitUtility.GetComponent<T_MANIFEST_KP_KEIJYOUdaocls>(); //マニ印字_建廃_形状Dao
            this.MANIFEST_KP_NISUGATADao = DaoInitUtility.GetComponent<T_MANIFEST_KP_NISUGATAdaocls>(); //マニ印字_建廃_荷姿Dao
            this.MANIFEST_KP_SBN_HOUHOUDao = DaoInitUtility.GetComponent<T_MANIFEST_KP_SBN_HOUHOUdaocls>(); //マニ印字_建廃_処分方法Dao
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

                this.SearchResult = null;

                //検索実行
                this.SearchResult = new DataTable();

                DataTable db = new DataTable();

                //マニフェストデータを取得する。
                db = HkIchiran.getdateforstringsql(this.createSql);

                db.Columns["CHECKBOX"].ReadOnly = false;

                // 廃棄物種類略称名カラムの最大文字数を最大にする
                if (db.Columns.Contains("廃棄物種類略称名"))
                {
                    db.Columns["廃棄物種類略称名"].MaxLength = 65535;
                }

                //複数SystemIDとSeqをフィルタする｡
                Dictionary<string, string> sys_seq = new Dictionary<string, string>();

                //複数HAIKI_SHURUI_NAME_RYAKUを格納する。
                Dictionary<string, string> haiki_shurui_name_ryaku = new Dictionary<string, string>();

                //システムＩＤ
                string systemid0 = "";
                //枝番
                string seq0 = "";

                for (int i = 0; i < db.Rows.Count; i++)
                {
                    systemid0 = db.Rows[i]["SYSTEM_ID"].ToString();
                    seq0 = db.Rows[i]["SEQ"].ToString();
                    int IntCount = 1;
                    if (!sys_seq.ContainsKey(db.Rows[i]["SYSTEM_ID"].ToString() + ',' + db.Rows[i]["SEQ"].ToString()))
                    {
                        sys_seq.Add((db.Rows[i]["SYSTEM_ID"].ToString() + ',' + db.Rows[i]["SEQ"].ToString()), IntCount.ToString());
                    }

                    foreach (DataRow dtRow in db.Rows)
                    {
                        //複数行のシステムＩＤと枝番をフィルタする。
                        if (systemid0 == dtRow["SYSTEM_ID"].ToString() && seq0 == dtRow["SEQ"].ToString())
                        {
                            if (sys_seq.ContainsKey(dtRow["SYSTEM_ID"].ToString() + ',' + dtRow["SEQ"].ToString()))
                            {
                                sys_seq[dtRow["SYSTEM_ID"].ToString() + ',' + dtRow["SEQ"].ToString()] = (IntCount).ToString();
                                IntCount = IntCount + 1;
                            }
                        }
                    }
                    // 2014.05.27 by 胡 end
                    //CongBinh 20200330 #134989 S
                    if (!string.IsNullOrEmpty(db.Rows[i]["hiddenHST_GYOUSHA_CD"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["hiddenHST_GENBA_CD"].ToString()))
                    {
                        this.lstGyoushaGenbaCd.Add(db.Rows[i]["hiddenHST_GYOUSHA_CD"].ToString() + db.Rows[i]["hiddenHST_GENBA_CD"].ToString());
                    }
                    //CongBinh 20200330 #134989 E
                }

                //db_haiki_shurui列一番大きいデータをフィルタする。
                foreach (KeyValuePair<string, string> kvp in sys_seq)
                {
                    // 2014.05.27 by 胡 start
                    if (!string.IsNullOrEmpty(kvp.Key) && int.Parse(kvp.Value) == 1)
                    {
                        continue;
                    }
                    //システムＩＤ
                    string systemid = kvp.Key.Split(',')[0];
                    //枝番
                    string seq = kvp.Key.Split(',')[1];
                    // 2014.05.27 by 胡 end
                    string sbn_end_date = "1900/01/01 0:00:00";
                    string last_sbn_end_date = "1900/01/01 0:00:00";

                    // haiki_shurui_name_ryakuに廃棄物種類(HAIKI_SHURUI_NAME_RYAKU)を追加する
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        if (systemid == db.Rows[i]["SYSTEM_ID"].ToString()
                            && seq == db.Rows[i]["SEQ"].ToString())
                        {
                            if (!string.IsNullOrEmpty(db.Rows[i]["廃棄物種類略称名"].ToString()))
                            {
                                if (!haiki_shurui_name_ryaku.ContainsKey(db.Rows[i]["廃棄物種類略称名"].ToString()))
                                {
                                    haiki_shurui_name_ryaku[db.Rows[i]["廃棄物種類略称名"].ToString()] = "";
                                }
                            }
                        }
                    }
                    //最大処分終了日
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        if (systemid == db.Rows[i]["SYSTEM_ID"].ToString()
                           && seq == db.Rows[i]["SEQ"].ToString())
                        {
                            if (string.IsNullOrEmpty(db.Rows[i]["処分終了日"].ToString()))
                            {
                                sbn_end_date = string.Empty;
                                break;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sbn_end_date))
                                {
                                    switch ((DateTime.Parse(sbn_end_date).ToString("yyyyMMdd")).
                                        CompareTo(DateTime.Parse(db.Rows[i]["処分終了日"].ToString()).ToString("yyyyMMdd")))
                                    {
                                        case -1://小
                                            sbn_end_date = db.Rows[i]["処分終了日"].ToString();
                                            break;

                                        case 0://等
                                            break;

                                        case 1://大
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    //最大最終処分終了日
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        if (systemid == db.Rows[i]["SYSTEM_ID"].ToString()
                           && seq == db.Rows[i]["SEQ"].ToString())
                        {
                            if (string.IsNullOrEmpty(db.Rows[i]["最終処分終了日"].ToString()))
                            {
                                last_sbn_end_date = string.Empty;
                                break;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(last_sbn_end_date))
                                {
                                    switch ((DateTime.Parse(last_sbn_end_date).ToString("yyyyMMdd"))
                                        .CompareTo(DateTime.Parse(db.Rows[i]["最終処分終了日"].ToString()).ToString("yyyyMMdd")))
                                    {
                                        case -1://小
                                            last_sbn_end_date = db.Rows[i]["最終処分終了日"].ToString();
                                            break;

                                        case 0://等
                                            break;

                                        case 1://大
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    DataRow dr = db.NewRow();
                    //dbテブルの重複システムと枝番データを削除する。
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        if (systemid == db.Rows[i]["SYSTEM_ID"].ToString()
                            && seq == db.Rows[i]["SEQ"].ToString())
                        {
                            dr.ItemArray = db.Rows[i].ItemArray;
                            db.Rows.Remove(db.Rows[i]);
                            i--;
                        }
                    }
                    if (db.Columns.Contains("処分終了日"))
                    {
                        if (string.IsNullOrEmpty(sbn_end_date))
                        {
                            dr["処分終了日"] = DBNull.Value;
                        }
                        else
                        {
                            dr["処分終了日"] = sbn_end_date;
                        }
                    }
                    if (db.Columns.Contains("最終処分終了日"))
                    {
                        if (string.IsNullOrEmpty(last_sbn_end_date))
                        {
                            dr["最終処分終了日"] = DBNull.Value;
                        }
                        else
                        {
                            dr["最終処分終了日"] = last_sbn_end_date;
                        }
                    }

                    //あらため廃棄物種類を作成する。
                    string haikishuruinameryaku = null;
                    int h = 0;
                    foreach (KeyValuePair<string, string> kvp_haiki_shurui_name_ryaku in haiki_shurui_name_ryaku)
                    {
                        if (h == 0 && haiki_shurui_name_ryaku.Count > 1)
                        {
                            haikishuruinameryaku = kvp_haiki_shurui_name_ryaku.Key + ',';
                        }
                        else if (h == 0 && haiki_shurui_name_ryaku.Count == 1)
                        {
                            haikishuruinameryaku = kvp_haiki_shurui_name_ryaku.Key;
                        }
                        else if (haiki_shurui_name_ryaku.Count > 1 && h != haiki_shurui_name_ryaku.Count - 1)
                        {
                            haikishuruinameryaku = haikishuruinameryaku + kvp_haiki_shurui_name_ryaku.Key + ',';
                        }
                        else
                        {
                            haikishuruinameryaku = haikishuruinameryaku + kvp_haiki_shurui_name_ryaku.Key;
                        }
                        h++;
                    }
                    if (db.Columns.Contains("廃棄物種類略称名"))
                    {
                        dr["廃棄物種類略称名"] = haikishuruinameryaku;
                    }

                    db.Rows.Add(dr.ItemArray);

                    //初期化
                    sys_seq = new Dictionary<string, string>();
                    haiki_shurui_name_ryaku = new Dictionary<string, string>();
                }

                // 処分終了日、最終処分終了日についてはSQLでの絞り込みが難しいため、Logic内で絞り込む
                string where = "";
                if (this.form.radbtn_shobunshuryobi.Checked)
                {
                    if (this.form.HIDSUKE_FROM.Value != null && this.form.HIDSUKE_TO.Value != null)
                    {
                        where += "処分終了日 >= " + "'" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "'";
                        where += " " + "AND" + " " + "処分終了日 <= " + "'" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "'";
                        
                    }
                    if (this.form.HIDSUKE_FROM.Value != null && this.form.HIDSUKE_TO.Value == null)
                    {
                        where += "処分終了日 >= " + "'" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "'";
                    }
                    if (this.form.HIDSUKE_FROM.Value == null && this.form.HIDSUKE_TO.Value != null)
                    {
                        where += "処分終了日 <= " + "'" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "'";
                    } 
                }

                if (this.form.radbtn_SaishuShobun.Checked)
                {
                    where = "";
                    if (this.form.HIDSUKE_FROM.Value != null && this.form.HIDSUKE_TO.Value != null)
                    {
                        where += "最終処分終了日 >= " + "'" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "'";
                        where += " " + "AND" + " " + "最終処分終了日 <= " + "'" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "'";

                    }
                    if (this.form.HIDSUKE_FROM.Value != null && this.form.HIDSUKE_TO.Value == null)
                    {
                        where += "最終処分終了日 >= " + "'" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "'";
                    }
                    if (this.form.HIDSUKE_FROM.Value == null && this.form.HIDSUKE_TO.Value != null)
                    {
                        where += "最終処分終了日 <= " + "'" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "'";
                    }
                }

                DataRow[] dbArr = db.Select(where);
                if (dbArr != null && dbArr.Length > 0)
                {
                    DataTable tmp = dbArr[0].Table.Clone();

                    foreach (DataRow row in dbArr)
                    {
                        tmp.ImportRow(row);
                    }
                    db = tmp.Copy();
                }
                else
                {
                    db.Clear();
                }
                int count = SearchResult.Rows.Count;
                if (barcodeFlg == true && this.form.customDataGridView1.RowCount == 0)
                {
                    this.SearchBarcodeResult = new DataTable();//CongBinh 20200330 #134989
                }

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
              
                string orderByDT = this.orderByQuery.Replace("\"", "");//thongh 2015/12/22 #14705
                db.DefaultView.Sort = orderByDT;
                DataTable dt = db.DefaultView.ToTable();

                //バーコードリーダーの処理を追加
                if (barcodeFlg == true)
                {
                    if (dt.Rows.Count != 0)
                    {
                        if (SearchBarcodeResult.Rows.Count != 0)
                        {
                            DataRow[] foundRows = SearchBarcodeResult.Select(" hiddenSYSTEM_ID = " + dt.Rows[0]["hiddenSYSTEM_ID"].ToString() + " AND " +
                            "hiddenSEQ = '" + dt.Rows[0]["hiddenSEQ"].ToString() + "'" + " AND " +
                            "hiddenMANIFEST_ID = '" + dt.Rows[0]["hiddenMANIFEST_ID"].ToString() + "'"
                            , "", DataViewRowState.Added);

                            //同じ交付番号が一覧に存在していないかチェック
                            if (foundRows.Length == 0)
                            {
                                //同じ番号が存在していない場合、一覧に追加
                                this.SearchBarcodeResult.Merge(dt);
                            }
                        }
                        else
                        {
                            this.SearchBarcodeResult.Merge(dt);
                        }
                    }
                    else
                    {
                        this.SearchBarcodeResult.Merge(dt);
                    }
                }
                else
                {
                    this.SearchResult = dt;
                }

                // DataGridViewを作り直すとイベントが消えるため再設定
                var tempCheckBoxCell = this.form.customDataGridView1.Columns["CHECKBOX"].HeaderCell as DataGridviewCheckboxHeaderCell;
                tempCheckBoxCell.OnCheckBoxClicked -= new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                    datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);
                if (barcodeFlg == true)//thongh 2015/12/22 #14705
                {
                    this.SearchBarcodeResult.DefaultView.Sort = orderByDT;
                }
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

                    if ("A票".Equals(column.Name)
                        || "B1票".Equals(column.Name)
                        || "B2票".Equals(column.Name)
                        || "B4票".Equals(column.Name)
                        || "B6票".Equals(column.Name)
                        || "C1票".Equals(column.Name)
                        || "C2票".Equals(column.Name)
                        || "D票".Equals(column.Name)
                        || "E票".Equals(column.Name))
                    {
                        this.form.customDataGridView1.Columns.RemoveAt(index);
                        DgvCustomDataTimeColumn newColumnDataTime = new DgvCustomDataTimeColumn();
                        newColumnDataTime.Name = column.Name;
                        newColumnDataTime.DataPropertyName = column.DataPropertyName;
                        this.form.customDataGridView1.Columns.Insert(index, newColumnDataTime);
                        min++;
                    }

                    //有効化
                    if ("A票".Equals(column.Name)
                        || "B1票".Equals(column.Name)
                        || "B2票".Equals(column.Name)
                        || "B4票".Equals(column.Name)
                        || "B6票".Equals(column.Name)
                        || "C1票".Equals(column.Name)
                        || "C2票".Equals(column.Name)
                        || "D票".Equals(column.Name)
                        || "E票".Equals(column.Name)
                        || "".Equals(column.Name))
                    {
                        column.ReadOnly = false;
                    }

                    if ("CHECKBOX".Equals(column.Name))
                    {
                        column.ReadOnly = false;
                    }
                }

                for (int index = 0; index < this.form.customDataGridView1.Columns.Count; index++)
                {
                    DataGridViewColumn column = this.form.customDataGridView1.Columns[index];

                    if ("廃棄物種類略称名".Equals(column.Name) && !this.selectQuery.Contains("廃棄物種類略称名"))
                    {
                        column.Visible = false;
                    }
                    if ("処分終了日".Equals(column.Name) && !this.selectQuery.Contains("処分終了日"))
                    {
                        column.Visible = false;
                    }
                    if ("最終処分終了日".Equals(column.Name) && !this.selectQuery.Contains("最終処分終了日"))
                    {
                        column.Visible = false;
                    }
                }

                //一覧の項目を非表示
                this.form.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
                this.form.customDataGridView1.Columns["SEQ"].Visible = false;
                this.form.customDataGridView1.Columns["TIME_STAMP"].Visible = false;
                this.form.customDataGridView1.Columns["hiddenSYSTEM_ID"].Visible = false;
                this.form.customDataGridView1.Columns["hiddenSEQ"].Visible = false;
                this.form.customDataGridView1.Columns["hiddenMANIFEST_ID"].Visible = false;
                this.form.customDataGridView1.Columns["hiddenHST_GYOUSHA_CD"].Visible = false;
                this.form.customDataGridView1.Columns["hiddenHST_GENBA_CD"].Visible = false;
                // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう start
                this.form.customDataGridView1.Columns["hiddenHAIKI_KBN_CD"].Visible = false;
                // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう end
                this.form.customDataGridView1.Columns["hiddenRET_DATE_SEQ"].Visible = false;

                //getdateforstringsqlで取得していない項目を初期化する。
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    this.form.customDataGridView1.Rows[i].Cells["CHECKBOX"].Value = false;
                }

                // 2014.05.20 by 胡 start
                this.SetHenkyakuhiNyuuryokuEnabled(true);
                // 2014.05.20 by 胡 end
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = false;
                this.form.customSortHeader1.SortDataTable(this.form.customDataGridView1.DataSource as DataTable);
                this.form.customDataGridView1.Columns["CHECKBOX"].Visible = true;

                // 検索中フラグ初期化
                searching = false;

                // ----20140624 syunrei EV004974_バーコード読込時アラート start
                //データない時は処理
                if ((DataTable)this.form.customDataGridView1.DataSource == null || ((DataTable)this.form.customDataGridView1.DataSource).Rows.Count <= 0 || dt.Rows.Count <= 0)
                {
                    // 20140624 syunrei EV004974_バーコード読込時アラート end

                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
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

        /// <summary>
        /// システム情報のマニフェスト情報使用区分により入力制御を行う
        /// </summary>
        private void SetReadOnlyCheckAToCheckE()
        {
            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                if (this.form.customDataGridView1.Columns["A票"] != null && intManifest_Use_A.Equals(2))
                {
                    this.form.customDataGridView1.Columns["A票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["B1票"] != null && intManifest_Use_B1.Equals(2))
                {
                    this.form.customDataGridView1.Columns["B1票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["B2票"] != null && intManifest_Use_B2.Equals(2))
                {
                    this.form.customDataGridView1.Columns["B2票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["B4票"] != null && intManifest_Use_B4.Equals(2))
                {
                    this.form.customDataGridView1.Columns["B4票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["B6票"] != null && intManifest_Use_B6.Equals(2))
                {
                    this.form.customDataGridView1.Columns["B6票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["C1票"] != null && intManifest_Use_C1.Equals(2))
                {
                    this.form.customDataGridView1.Columns["C1票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["C2票"] != null && intManifest_Use_C2.Equals(2))
                {
                    this.form.customDataGridView1.Columns["C2票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["D票"] != null && intManifest_Use_D.Equals(2))
                {
                    this.form.customDataGridView1.Columns["D票"].ReadOnly = true;
                }

                if (this.form.customDataGridView1.Columns["E票"] != null && intManifest_Use_E.Equals(2))
                {
                    this.form.customDataGridView1.Columns["E票"].ReadOnly = true;
                }
            }
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 日付一括入力状態検索
        /// </summary>
        private DataTable SearchHizukeIkatsuNyuryokuEntry()
        {
            DataTable db = new DataTable();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            sql.Append(" SELECT MANIFEST_USE_A ");
            sql.Append(" ,MANIFEST_USE_A ");
            sql.Append(" ,MANIFEST_USE_B1 ");
            sql.Append(" ,MANIFEST_USE_B2 ");
            sql.Append(" ,MANIFEST_USE_B4 ");
            sql.Append(" ,MANIFEST_USE_B6 ");
            sql.Append(" ,MANIFEST_USE_C1 ");
            sql.Append(" ,MANIFEST_USE_C2 ");
            sql.Append(" ,MANIFEST_USE_D ");
            sql.Append(" ,MANIFEST_USE_E ");
            sql.Append(" FROM ");
            sql.Append(" M_SYS_INFO ");
            sql.Append(" WHERE ");
            sql.Append(" SYS_ID ='0' ");
            db = HkIchiran.getdateforstringsql(sql.ToString());
            return db;
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.parentForm = parentForm; //CongBinh 20200330 #134989
            this.headForm = ((HeaderForm)(parentForm).headerForm); //CongBinh 20200330 #134989

            //一括入力ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);
            // 2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by 胡 begin
            //ﾌｫｰｶｽ設定ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);
            // 2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by 胡 end
            //返送案内ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);

            //並替移動ボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);

            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移

            //checkbox全選択処理
            this.form.customDataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DetailIchiran_ColumnHeaderMouseClick);

            this.form.customDataGridView1.RowEnter += new DataGridViewCellEventHandler(this.customDataGridView1_RowEnter);

            // 20141128 Houkakou 「返却日入力」のダブルクリックを追加する start
            // 「To」のイベント生成
            this.form.HIDSUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDSUKE_TO_MouseDoubleClick);
            // 20141128 Houkakou 「返却日入力」のダブルクリックを追加する end

            this.headForm.radbtn_Barcode_On.CheckedChanged += new EventHandler(this.form.radbtn_Barcode_On_CheckedChanged);//CongBinh 20200330 #134989
            this.headForm.radbtn_Barcode_Off.CheckedChanged += new EventHandler(this.form.radbtn_Barcode_Off_CheckedChanged); //CongBinh 20200330 #134989
            LogUtility.DebugMethodEnd();
        }
     

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
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
            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句

            sql.Append(" SELECT ");
            sql.Append(" CONVERT(bit, 0) AS CHECKBOX ");                      //システムID
            sql.Append(" , T_MANIFEST_ENTRY.SYSTEM_ID SYSTEM_ID ");                      //システムID
            sql.Append(" , T_MANIFEST_ENTRY.SEQ SEQ ");                                //枝番
            sql.Append(" , T_MANIFEST_ENTRY.SYSTEM_ID AS hiddenSYSTEM_ID ");                      //システムID
            sql.Append(" , T_MANIFEST_ENTRY.SEQ AS hiddenSEQ ");                                //枝番
            sql.Append(" , T_MANIFEST_ENTRY.MANIFEST_ID AS hiddenMANIFEST_ID ");                                //交付番号
            sql.Append(" , CAST(T_MANIFEST_RET_DATE.TIME_STAMP AS int) AS TIME_STAMP ");                      //TIME_STAMP
            //排出事業者
            sql.Append(" , T_MANIFEST_ENTRY.HST_GYOUSHA_CD AS hiddenHST_GYOUSHA_CD ");                                //業者CD
            sql.Append(" , T_MANIFEST_ENTRY.HST_GENBA_CD AS hiddenHST_GENBA_CD ");                                //現場CD
            // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう start
            sql.Append(" , T_MANIFEST_ENTRY.HAIKI_KBN_CD AS hiddenHAIKI_KBN_CD ");                                //現場CD
            // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう end
            sql.Append(" , T_MANIFEST_RET_DATE.SEQ AS hiddenRET_DATE_SEQ ");                                      //マニフェスト返却日.枝番

            String MOutputPatternSelect = this.selectQuery;
            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
            }
            else
            {
                if (!this.selectQuery.Contains("廃棄物種類略称名"))
                {
                    sql.Append(" , M_HAIKI_SHURUI.HAIKI_SHURUI_NAME_RYAKU 廃棄物種類略称名 ");
                }
                if (!this.selectQuery.Contains("T_MANIFEST_DETAIL.SBN_END_DATE"))
                {
                    sql.Append(" , T_MANIFEST_DETAIL.SBN_END_DATE 処分終了日 ");
                }
                if (!this.selectQuery.Contains("T_MANIFEST_DETAIL.LAST_SBN_END_DATE"))
                {
                    sql.Append(" , T_MANIFEST_DETAIL.LAST_SBN_END_DATE 最終処分終了日 ");
                }

                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
            }

            //sql.Append(" FROM (");

            #endregion

            #region FROM句

            //FROM句作成
            //取引先マスタ+社員マスタ、業者マスタ+社員マスタ、現場マスタ+社員マスタを結合したものを、結合)
            sql.Append(" FROM ");
            sql.Append(" T_MANIFEST_ENTRY ");
            sql.Append(" LEFT JOIN T_MANIFEST_DETAIL ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID  ");
            sql.Append(" AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ  ");
            // 2014.05.27 by 胡 start
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
            // 2014.05.27 by 胡 end
            sql.Append(" LEFT JOIN T_MANIFEST_RET_DATE ");
            sql.Append(" ON T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_RET_DATE.SYSTEM_ID ");
            sql.Append(" AND T_MANIFEST_RET_DATE.DELETE_FLG = '0'  ");
            sql.Append(" LEFT JOIN M_HAIKI_SHURUI ");
            sql.Append(" ON T_MANIFEST_ENTRY.HAIKI_KBN_CD = M_HAIKI_SHURUI.HAIKI_KBN_CD ");
            sql.Append(" AND T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD  ");

            sql.Append(" LEFT JOIN M_HAIKI_KBN G  ");
            sql.Append(" ON T_MANIFEST_ENTRY.HAIKI_KBN_CD = G.HAIKI_KBN_CD ");
            sql.Append(" LEFT JOIN M_GENBA H  ");
            sql.Append(" ON T_MANIFEST_ENTRY.HST_GYOUSHA_CD = H.GYOUSHA_CD ");
            sql.Append(" AND T_MANIFEST_ENTRY.HST_GENBA_CD = H.GENBA_CD ");
            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句

            //取引先マスタの取引先ふりがな
            sql.Append(" WHERE ");
            sql.Append(" T_MANIFEST_ENTRY.DELETE_FLG = '0' ");
            sql.Append(" AND T_MANIFEST_ENTRY.FIRST_MANIFEST_KBN = '0' ");
            sql.Append(" AND ISNULL(T_MANIFEST_ENTRY.HST_GYOUSHA_CD, '') <> ''");
            sql.Append(" AND ISNULL(T_MANIFEST_ENTRY.HST_GENBA_CD, '') <> ''");
            //MOD NHU 20211004 #155769 S
            if (this.headForm.KYOTEN_CD.Text != "" && this.headForm.KYOTEN_CD.Text != "99")
            {
                sql.AppendFormat(" AND T_MANIFEST_ENTRY.KYOTEN_CD = '{0}' ", this.headForm.KYOTEN_CD.Text);
            }
            //MOD NHU 20211004 #155769 E
            //ここで手動とバーコードで条件を変える
            if (this.form.panel_Barcode_On.Visible == true || "1".Equals(this.form.txtNum_Renzoku.Text))//CongBinh 20200330 #134989
            {
                //バーコード
                //交付番号
                if (!String.IsNullOrEmpty(this.form.txtKoufuNo_Hyou.Text))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.MANIFEST_ID = + '" + this.form.txtKoufuNo_Hyou.Text + "' ");
                }
                //CongBinh 20200330 #134989 S
                if ("1".Equals(this.form.txtNum_Renzoku.Text))
                {
                    if (!String.IsNullOrEmpty(this.form.KOUFUBANNGO.Text))
                    {
                        sql.Append(" AND T_MANIFEST_ENTRY.MANIFEST_ID = + '" + this.form.KOUFUBANNGO.Text + "' ");
                    }
                }
                //CongBinh 20200330 #134989 E
            }
            else
            {
                //手動
                //排出事業者
                if (!String.IsNullOrEmpty(this.form.HAISHUTSUJIGYOUSYA_CD.Text))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.HST_GYOUSHA_CD = '" + this.form.HAISHUTSUJIGYOUSYA_CD.Text + "' ");
                }

                //排出事業場
                if (!String.IsNullOrEmpty(this.form.HAISHUTSUJIGYOUBA_CD.Text))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.HST_GENBA_CD = '" + this.form.HAISHUTSUJIGYOUBA_CD.Text + "' ");
                }
                //MOD NHU 20211103 #157020 S
                //処分受託者
                if (!String.IsNullOrEmpty(this.form.cantxt_SyobunJyutakuNameCd.Text))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY.SBN_GYOUSHA_CD = '" + this.form.cantxt_SyobunJyutakuNameCd.Text + "' ");
                }

                //処分事業場
                if (!String.IsNullOrEmpty(this.form.cantxt_UnpanJyugyobaNameCd.Text))
                {
                    if (!string.IsNullOrEmpty(this.form.txtNum_ManifestShurui.Text) && this.form.txtNum_ManifestShurui.Text == "4")
                    {
                        sql.AppendFormat(" AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION ");
                        sql.AppendFormat(" WHERE ((T_MANIFEST_ENTRY.HAIKI_KBN_CD <> 3 AND TMU_CONDITION.UPN_SAKI_GENBA_CD ='{0}' ) OR ( T_MANIFEST_ENTRY.HAIKI_KBN_CD = 3 AND TMU_CONDITION.UPN_SAKI_KBN = 1 AND TMU_CONDITION.UPN_SAKI_GENBA_CD ='{0}' ) ) ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        sql.AppendFormat(" AND TMU_CONDITION.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID ");
                        sql.AppendFormat(" AND TMU_CONDITION.SEQ = T_MANIFEST_ENTRY.SEQ) ");
                    }
                    if (!string.IsNullOrEmpty(this.form.txtNum_ManifestShurui.Text) && this.form.txtNum_ManifestShurui.Text == "3")
                    {
                        sql.AppendFormat(" AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION ");
                        sql.AppendFormat(" WHERE TMU_CONDITION.UPN_SAKI_KBN = 1 AND TMU_CONDITION.UPN_SAKI_GENBA_CD ='{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        sql.AppendFormat(" AND TMU_CONDITION.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID ");
                        sql.AppendFormat(" AND TMU_CONDITION.SEQ = T_MANIFEST_ENTRY.SEQ) ");
                    }
                    if (!string.IsNullOrEmpty(this.form.txtNum_ManifestShurui.Text) && this.form.txtNum_ManifestShurui.Text != "3")
                    {
                        sql.AppendFormat(" AND EXISTS (SELECT * FROM T_MANIFEST_UPN TMU_CONDITION ");
                        sql.AppendFormat(" WHERE TMU_CONDITION.UPN_SAKI_GENBA_CD ='{0}' ", this.form.cantxt_UnpanJyugyobaNameCd.Text);
                        sql.AppendFormat(" AND TMU_CONDITION.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID ");
                        sql.AppendFormat(" AND TMU_CONDITION.SEQ = T_MANIFEST_ENTRY.SEQ) ");
                    }
                }
                //MOD NHU 20211103 #157020 E
                //返送先
                //SQL文返送先格納
                var sqlHensousaki = new StringBuilder();

                //A票
                //SQL文(A票)格納
                var sqlA = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlA.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlA.Length != 0)
                    {
                        sqlA.Append(" AND ");
                    }

                    sqlA.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_A = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlA.Length != 0)
                    {
                        sqlA.Append(" AND ");
                    }

                    sqlA.Append(" H.MANI_HENSOUSAKI_GENBA_CD_A = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlA.Length != 0)
                {
                    sqlHensousaki.Append(sqlA.ToString());
                }

                //B1票
                //SQL文(B1票)格納
                var sqlB1 = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlB1.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlB1.Length != 0)
                    {
                        sqlB1.Append(" AND ");
                    }

                    sqlB1.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_B1 = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlB1.Length != 0)
                    {
                        sqlB1.Append(" AND ");
                    }

                    sqlB1.Append(" H.MANI_HENSOUSAKI_GENBA_CD_B1 = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlB1.Length != 0)
                {
                    if (sqlA.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlB1.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlB1.ToString());
                    }
                }

                //B2票
                //SQL文(B2票)格納
                var sqlB2 = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlB2.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlB2.Length != 0)
                    {
                        sqlB2.Append(" AND ");
                    }

                    sqlB2.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_B2 = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlB2.Length != 0)
                    {
                        sqlB2.Append(" AND ");
                    }
                    sqlB2.Append(" H.MANI_HENSOUSAKI_GENBA_CD_B2 = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlB2.Length != 0)
                {
                    if (sqlA.Length != 0 || sqlB1.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlB2.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlB2.ToString());
                    }
                }

                //C1票
                //SQL文(C1票)格納
                var sqlC1 = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlC1.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlC1.Length != 0)
                    {
                        sqlC1.Append(" AND ");
                    }

                    sqlC1.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_C1 = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlC1.Length != 0)
                    {
                        sqlC1.Append(" AND ");
                    }

                    sqlC1.Append(" H.MANI_HENSOUSAKI_GENBA_CD_C1 = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlC1.Length != 0)
                {
                    if (sqlA.Length != 0 || sqlB1.Length != 0 || sqlB2.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlC1.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlC1.ToString());
                    }
                }

                //C2票
                //SQL文(C2票)格納
                var sqlC2 = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlC2.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlC2.Length != 0)
                    {
                        sqlC2.Append(" AND ");
                    }

                    sqlC2.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_C2 = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlC2.Length != 0)
                    {
                        sqlC2.Append(" AND ");
                    }

                    sqlC2.Append(" H.MANI_HENSOUSAKI_GENBA_CD_C2 = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlC2.Length != 0)
                {
                    if (sqlA.Length != 0 || sqlB1.Length != 0 || sqlB2.Length != 0 || sqlC1.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlC2.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlC2.ToString());
                    }
                }

                //D票
                //SQL文(D票)格納
                var sqlD = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlD.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlD.Length != 0)
                    {
                        sqlD.Append(" AND ");
                    }

                    sqlD.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_D = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlD.Length != 0)
                    {
                        sqlD.Append(" AND ");
                    }

                    sqlD.Append(" H.MANI_HENSOUSAKI_GENBA_CD_D = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlD.Length != 0)
                {
                    if (sqlA.Length != 0 || sqlB1.Length != 0 || sqlB2.Length != 0 || sqlC1.Length != 0 || sqlC2.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlD.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlD.ToString());
                    }
                }

                //E票
                //SQL文(E票)格納
                var sqlE = new StringBuilder();
                //返送先CD(取引先)
                if (!String.IsNullOrEmpty(this.form.HENSOU_TORIHIKISAKI_CD.Text))
                {
                    sqlE.Append(" H.MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = '" + this.form.HENSOU_TORIHIKISAKI_CD.Text + "' ");
                }

                //返却先CD(業者)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GYOUSHA_CD.Text))
                {
                    if (sqlE.Length != 0)
                    {
                        sqlE.Append(" AND ");
                    }

                    sqlE.Append(" H.MANI_HENSOUSAKI_GYOUSHA_CD_E = '" + this.form.HENSOU_GYOUSHA_CD.Text + "' ");
                }

                //返却先CD(現場)
                if (!String.IsNullOrEmpty(this.form.HENSOU_GENBA_CD.Text))
                {
                    if (sqlE.Length != 0)
                    {
                        sqlE.Append(" AND ");
                    }

                    sqlE.Append(" H.MANI_HENSOUSAKI_GENBA_CD_E = '" + this.form.HENSOU_GENBA_CD.Text + "' ");
                }

                //入力があった場合、条件として追加する
                if (sqlD.Length != 0)
                {
                    if (sqlA.Length != 0 || sqlB1.Length != 0 || sqlB2.Length != 0 || sqlC1.Length != 0 || sqlC2.Length != 0 || sqlD.Length != 0)
                    {
                        sqlHensousaki.Append(" OR ( " + sqlE.ToString() + " ) ");
                    }
                    else
                    {
                        sqlHensousaki.Append(sqlE.ToString());
                    }
                }

                if (sqlHensousaki.Length != 0)
                {
                    sql.Append(" AND ( " + sqlHensousaki.ToString() + " ) ");
                }
              
                //日付区分
                if (this.form.radbtn_Unpanshuryobi.Checked)
                {
                    // 2014.05.27 by 胡 start
                    if (this.form.HIDSUKE_FROM.Value != null)
                    {
                        sql.Append(" AND ( ");
                        sql.Append("  ( ");
                        sql.Append(" T_MANIFEST_UPN1.UPN_END_DATE >= '" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "' ");
                        if (this.form.HIDSUKE_TO.Value != null)
                        {
                            sql.Append(" AND  ");
                            sql.Append(" T_MANIFEST_UPN1.UPN_END_DATE <= '" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "' ");
                            sql.Append(" ) ");
                        }
                        else
                        {
                            sql.Append(" ) ");
                        }
                        sql.Append(" OR ");
                        sql.Append("  ( ");
                        sql.Append(" T_MANIFEST_UPN2.UPN_END_DATE >= '" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "' ");
                        if (this.form.HIDSUKE_TO.Value != null)
                        {
                            sql.Append(" AND  ");
                            sql.Append(" T_MANIFEST_UPN2.UPN_END_DATE <= '" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "' ");
                            sql.Append(" ) ");
                        }
                        else
                        {
                            sql.Append(" ) ");
                        }
                        sql.Append(" OR ");
                        sql.Append("  ( ");
                        sql.Append(" T_MANIFEST_UPN3.UPN_END_DATE >= '" + DateTime.Parse(this.form.HIDSUKE_FROM.Value.ToString().Substring(0, 10)) + "' ");
                        if (this.form.HIDSUKE_TO.Value != null)
                        {
                            sql.Append(" AND  ");
                            sql.Append(" T_MANIFEST_UPN3.UPN_END_DATE <= '" + DateTime.Parse(this.form.HIDSUKE_TO.Value.ToString().Substring(0, 10)) + "' ");
                            sql.Append(" ) ");
                        }
                        else
                        {
                            sql.Append(" ) ");
                        }
                        sql.Append(" ) ");
                    }
                }
                //CongBinh 20200330 #134989 S
                if (this.CheckExistsCheckboxOn())
                {

                    sql.Append(" AND ( ");
                    bool start = true;
                    //返却状態
                    if (this.form.radbtn_Hencyakuzumi.Checked)
                    {
                        if (this.form.checkbox_Ahyou.Checked)
                        {
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_A IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_B1hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B1 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_B2hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B2 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_B4hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B4 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_B6hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B6 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_C1hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_C1 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_C2hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_C2 IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_Dhyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_D IS NOT NULL ");
                            start = false;
                        }
                        if (this.form.checkbox_Ehyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_E IS NOT NULL ");
                            start = false;
                        }
                    }
                    else if (this.form.radbtn_Mihencyaku.Checked)
                    {
                        if (this.form.checkbox_Ahyou.Checked)
                        {
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_A is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_B1hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B1 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_B2hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B2 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_B4hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B4 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_B6hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_B6 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_C1hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_C1 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_C2hyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_C2 is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_Dhyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_D is NULL  ");
                            start = false;
                        }
                        if (this.form.checkbox_Ehyou.Checked)
                        {
                            if (!start)
                            {
                                sql.Append(" OR ");
                            }
                            sql.Append(" T_MANIFEST_RET_DATE.SEND_E is NULL  ");
                            start = false;
                        }
                    }
                    sql.Append(" ) ");
                }
                //CongBinh 20200330 #134989 E
                // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
                if (!string.IsNullOrEmpty(this.form.txtNum_ManifestShurui.Text) && this.form.txtNum_ManifestShurui.Text != "4")
                {
                    sql.AppendFormat(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD = {0}", this.form.txtNum_ManifestShurui.Text);
                }
                // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end
              
                // 入力不可な項目が、日付一括入力の抽出条件に設定されている場合は除外して抽出する
                //CongBinh 20200330 #134989 S
                if (this.CheckExistsCheckboxOn())
                {
                    sql.Append(" AND ( ");
                    bool start1 = true;

                    if (this.form.checkbox_Ahyou.Checked)
                    {
                        sql.Append(" H.MANI_HENSOUSAKI_USE_A = 1 ");
                        start1 = false;
                    }
                    if (this.form.checkbox_B1hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" (H.MANI_HENSOUSAKI_USE_B1 = 1 ");
                        sql.Append(" AND T_MANIFEST_ENTRY.HAIKI_KBN_CD != 3) ");
                        start1 = false;
                    }
                    if (this.form.checkbox_B2hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" H.MANI_HENSOUSAKI_USE_B2 = 1 ");
                        start1 = false;
                    }
                    if (this.form.checkbox_B4hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" (H.MANI_HENSOUSAKI_USE_B4 = 1 ");
                        sql.Append(" AND (T_MANIFEST_ENTRY.HAIKI_KBN_CD != 1 AND T_MANIFEST_ENTRY.HAIKI_KBN_CD != 2)) ");
                        start1 = false;
                    }
                    if (this.form.checkbox_B6hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" (H.MANI_HENSOUSAKI_USE_B6 = 1 ");
                        sql.Append(" AND (T_MANIFEST_ENTRY.HAIKI_KBN_CD != 1 AND T_MANIFEST_ENTRY.HAIKI_KBN_CD != 2)) ");
                        start1 = false;
                    }
                    if (this.form.checkbox_C1hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" H.MANI_HENSOUSAKI_USE_C1 = 1 ");
                        start1 = false;
                    }
                    if (this.form.checkbox_C2hyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" H.MANI_HENSOUSAKI_USE_C2 = 1 ");
                        start1 = false;
                    }
                    if (this.form.checkbox_Dhyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" H.MANI_HENSOUSAKI_USE_D = 1 ");
                        start1 = false;
                    }
                    if (this.form.checkbox_Ehyou.Checked)
                    {
                        if (!start1)
                        {
                            sql.Append(" OR ");
                        }

                        sql.Append(" H.MANI_HENSOUSAKI_USE_E = 1 ");
                        start1 = false;
                    }
                    sql.Append(" ) ");
                }
                //CongBinh 20200330 #134989 E
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

            //LogUtility.DebugMethodEnd();
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

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション
                this.form.customDataGridView1.ColumnHeadersVisible = true;

                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                intManifest_Use_A = mSysInfo.MANIFEST_USE_A.Value;
                intManifest_Use_B1 = mSysInfo.MANIFEST_USE_B1.Value;
                intManifest_Use_B2 = mSysInfo.MANIFEST_USE_B2.Value;
                intManifest_Use_B4 = mSysInfo.MANIFEST_USE_B4.Value;
                intManifest_Use_B6 = mSysInfo.MANIFEST_USE_B6.Value;
                intManifest_Use_C1 = mSysInfo.MANIFEST_USE_C1.Value;
                intManifest_Use_C2 = mSysInfo.MANIFEST_USE_C2.Value;
                intManifest_Use_D = mSysInfo.MANIFEST_USE_D.Value;
                intManifest_Use_E = mSysInfo.MANIFEST_USE_E.Value;

                // 検索結果一覧のDao初期化
                HkIchiran = DaoInitUtility.GetComponent<DAOClass>();

                //現場のDao初期化
                genbaDao = DaoInitUtility.GetComponent<MGENBADao>();

                //業者のDao初期化
                gyoushaDao = DaoInitUtility.GetComponent<MGYOUSHADao>();

                //取引先のDao初期化
                torihikisakiDao = DaoInitUtility.GetComponent<MTORIHIKISAKIDao>();

                //バーコード区分
                this.headForm.txtNum_BarcodeKubun.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_BARCODE_KBN;
                //交付番号_票
                this.form.txtKoufuNo_Hyou.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_KOUFUBANGO_HYOU;
                //返却日
                this.form.dtimeHenkyakubi.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENKYAKUBI;

                //排出事業場CD
                this.form.HAISHUTSUJIGYOUBA_CD.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYAJOU_CD;
                //排出事業場名
                this.form.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYAJOU_NAME;
                //返送先(取引先)CD
                this.form.HENSOU_TORIHIKISAKI_CD.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_TORIHIKISAKI_CD;
                //返送先(取引先)名
                this.form.HENSOU_TORIHIKISAKI_NAME_RYAKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_TORIHIKISAKI_NAME;
                //返送先(業者)CD
                this.form.HENSOU_GYOUSHA_CD.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GYOUSYA_CD;
                //返送先(業者)名
                this.form.HENSOU_GYOUSHA_NAME_RYAKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GYOUSYA_NAME;
                //返送先(現場)CD
                this.form.HENSOU_GENBA_CD.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GENBA_CD;
                //返送先(現場)名
                this.form.HENSOU_GENBA_NAME_RYAKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GENBA_NAME;

                // マニフェスト、日付、返却状態TextBoxを初期化
                this.form.txtNum_Hidsuke.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDSUKE;
                this.form.txtNum_Hencyakujyoutai.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENCYAKU;
                this.form.HAISHUTSUJIGYOUSYA_CD.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYA_CD;
                this.form.HAISHUTSUJIGYOUSYA_NAME_RYAKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYA_NAME;
                this.form.KOUFUBANNGO.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_KOUFUBANNGO;
                //MOD NHU 20211103 #157020 S
                this.form.cantxt_SyobunJyutakuNameCd.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GYOUSHA_CD;
                this.form.ctxt_SyobunJyutakuName.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GYOUSHA_NAME;
                this.form.cantxt_UnpanJyugyobaNameCd.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GENBA_CD;
                this.form.ctxt_UnpanJyugyobaName.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GENBA_NAME;
                //MOD NHU 20211103 #157020 E
                if (Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_FROM != " ")
                {
                    this.form.HIDSUKE_FROM.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_FROM;
                }
                else
                {
                    this.form.HIDSUKE_FROM.Text = "";
                    this.form.HIDSUKE_FROM.Value = null;
                }
                if (Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_TO != " ")
                {
                    this.form.HIDSUKE_TO.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_TO;
                }
                else
                {
                    this.form.HIDSUKE_TO.Text = "";
                    this.form.HIDSUKE_TO.Value = null;
                }

                if (Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_IKATSU_NYURYOKU != " ")
                {
                    this.form.IKATSU_NYURYOKU.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_IKATSU_NYURYOKU;
                }
                else
                {
                    this.form.IKATSU_NYURYOKU.Text = "";
                    this.form.IKATSU_NYURYOKU.Value = null;
                }

                //日付一括入力状態設定
                SetHidukeIkatsuNyuryoku();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Text = parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENKYAKUBI));

                //バーコード区分の初期設定
                this.headForm.txtNum_BarcodeKubun.Text = "2";             
                // 2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by 胡 begin
                parentForm.bt_func4.Enabled = false;
                // 2014.03.06 バーコードoff時は・Func4を使用不可,キ－ダウン無効にする。by 胡 end
                // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
                this.form.txtNum_ManifestShurui.Text = "4";
                // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end

                // 権限チェックによるボタン制御
                var enabled = Manager.CheckAuthority("G137", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                parentForm.bt_func1.Enabled = enabled;  // 一括入力
                parentForm.bt_func9.Enabled = enabled;  // 登録

                //CongBinh 20200330 #134989 S
                this.form.txtNum_Renzoku.Text = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_RENZOKU_KBN;
                this.form.checkbox_Ahyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_A == "1" ? true : false;
                this.form.checkbox_B1hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B1 == "1" ? true : false;
                this.form.checkbox_B2hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B2 == "1" ? true : false;
                this.form.checkbox_B4hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B4 == "1" ? true : false;
                this.form.checkbox_B6hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B6 == "1" ? true : false;
                this.form.checkbox_C1hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_C1 == "1" ? true : false;
                this.form.checkbox_C2hyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_C2 == "1" ? true : false;
                this.form.checkbox_Dhyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_D == "1" ? true : false;
                this.form.checkbox_Ehyou.Checked = Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_E == "1" ? true : false;
                this.form.txtNum_Renzoku.Text = "2";
                //CongBinh 20200330 #134989 E
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

        #region プロセスボタン押下処理（※処理未実装）

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
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("検索条件設定画面", "画面遷移");
        }

        #endregion

        /// <summary>
        /// F1 一括入力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            //CongBinh 20200330 #134989 S
            if (!this.CheckExistsCheckboxOn())
            {
                MessageBoxUtility.MessageBoxShowInformation("いずれかの日付入力対象を選択してください");
                return;
            }
            //明細行が１件以上有った場合判断
            if (this.form.customDataGridView1.Rows.Count > 0)
            {
                //1.全ての明細行の各返却日(A票～E票)へヘッダの[日付一括更新]の日付をセットするかを確認する。
                string errorMessage = string.Empty;             
                //日付一括更新]日付の値が入力かどうか判断。
                if (string.IsNullOrEmpty(this.form.IKATSU_NYURYOKU.Text.Trim()))
                {
                    //コードが存在しない場合エラー
                    MessageBoxUtility.MessageBoxShowError("日付一括更新日を入力してください");
                    this.form.IKATSU_NYURYOKU.Focus();
                    return;
                }
                //CongBinh 20200330 #134989 S
                bool errFlg = false;
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (Convert.ToBoolean(dgvRow.Cells[0].Value))
                    {
                        errFlg = true;
                        break;
                    }
                }
                if (!errFlg)
                {
                    MessageBoxUtility.MessageBoxShowError("返却日を入力するマニフェストを選択してください");
                    return;
                }
                //CongBinh 20200330 #134989 E
                //チェックされたマニフェストの返却日を更新するかを確認する。
                errorMessage = "選択されている票の返却日を『{0}』で一括入力します。よろしいですか？";
                DialogResult result = MessageBoxUtility.MessageBoxShowConfirm(string.Format(errorMessage, this.form.IKATSU_NYURYOKU.Text.ToString()));
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            // DataGridViewの内容を書き換えるとソートが発生するので、DataSorceを書き換える
            string sortInfo = ((DataTable)this.form.customDataGridView1.DataSource).DefaultView.Sort;
            DataTable tempDataSorce = ((DataTable)this.form.customDataGridView1.DataSource).DefaultView.ToTable();
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                // 20140619 katen EV004472 チェックをつけていないマニフェストまで一括入力されてしまう start
                if (!Convert.ToBoolean(dgvRow.Cells[0].Value))
                {
                    continue;
                }
                // 20140619 katen EV004472 チェックをつけていないマニフェストまで一括入力されてしまう end

                //CongBinh 20200330 #134989 S
                if (this.form.checkbox_Ahyou.Checked)
                {
                    if (!dgvRow.Cells["A票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["A票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_B1hyou.Checked)
                {
                    if (!dgvRow.Cells["B1票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["B1票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_B2hyou.Checked)
                {
                    if (!dgvRow.Cells["B2票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["B2票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_B4hyou.Checked)
                {
                    if (!dgvRow.Cells["B4票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["B4票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_B6hyou.Checked)
                {
                    if (!dgvRow.Cells["B6票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["B6票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_C1hyou.Checked)
                {
                    if (!dgvRow.Cells["C1票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["C1票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_C2hyou.Checked)
                {
                    if (!dgvRow.Cells["C2票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["C2票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_Dhyou.Checked)
                {
                    if (!dgvRow.Cells["D票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["D票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                if (this.form.checkbox_Ehyou.Checked)
                {
                    if (!dgvRow.Cells["E票"].ReadOnly)
                    {
                        tempDataSorce.Rows[dgvRow.Index]["E票"] = this.form.IKATSU_NYURYOKU.Value;
                    }
                }
                //CongBinh 20200330 #134989 E
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

            // A票～E票のReadOnlyの再設定
            SetHenkyakuhiNyuuryokuEnabled(false);
        }

        /// <summary>
        /// F4 ﾌｫｰｶｽ設定
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            //1.[F4]フォーカス設定ボタンをクリック後、「交付番号/票」にフォーカスをセットする。
            this.form.txtKoufuNo_Hyou.Focus();
        }

        /// <summary>
        /// F6 返送案内ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            //返送案内書画面
            FormManager.OpenFormWithAuth("G324", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
        }

        /// <summary>
        /// F8 検索ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            //必須チェック
            if (SearchCheck())//155769
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            // 20141022 Houkakou 「返却日入力」の日付チェックを追加する start
            if (this.DateCheck())
            {
                return;
            }
            //CongBinh 20200513 #136893 S
            if (!this.CheckExistsCheckboxOn())
            {
                MessageBoxUtility.MessageBoxShowInformation("いずれかの日付入力対象を選択してください");
                return;
            }
            //CongBinh 20200513 #136893 E
            // 20141022 Houkakou 「返却日入力」の日付チェックを追加する end
            if (this.selectQuery != null)
            {
                this.Search();
            }
        }

        /// <summary>
        /// F9 登録ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            // 20141022 Houkakou 「返却日入力」の日付チェックを追加する start
            if (this.headForm.radbtn_Barcode_Off.Checked && this.DateCheck())
            {
                return;
            }
            // 20141022 Houkakou 「返却日入力」の日付チェックを追加する end

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

                //チェックされたマニフェストの返却日を更新するかを確認する。
                DialogResult result = MessageBoxUtility.MessageBoxShow("C033");
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                MessageBoxUtility.MessageBoxShow("E051", "更新するマニフェスト");
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
                        if (this.headForm.radbtn_Barcode_Off.Checked && "2".Equals(this.form.txtNum_Renzoku.Text))//CongBinh 20200513 #136892
                        {
                            //更新後、DataGridViewを更新する。
                            this.Search();
                        }
                        else
                        {
                            //④バーコードのデータを登録したあとに明細をクリアしてください。
                            this.SearchBarcodeResult.Clear();
                            this.form.customDataGridView1.DataSource = this.SearchBarcodeResult;
                            this.form.barCode = "";
                        }
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

            // A票～E票のReadOnlyの再設定
            SetHenkyakuhiNyuuryokuEnabled(false);
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            //拠点、伝票日付From、伝票日付To、確定区分、伝票種類の項目をセッティングファイルに保存する。
            //バーコード区分
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_BARCODE_KBN = this.headForm.txtNum_BarcodeKubun.Text;
            //交付番号_票
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_KOUFUBANGO_HYOU = this.form.txtKoufuNo_Hyou.Text;
            //返却日
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENKYAKUBI = this.form.dtimeHenkyakubi.Text;

            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDSUKE = this.form.txtNum_Hidsuke.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENCYAKU = this.form.txtNum_Hencyakujyoutai.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYA_CD = this.form.HAISHUTSUJIGYOUSYA_CD.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYA_NAME = this.form.HAISHUTSUJIGYOUSYA_NAME_RYAKU.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_KOUFUBANNGO = this.form.KOUFUBANNGO.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_FROM = this.form.HIDSUKE_FROM.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HIDUKE_TO = this.form.HIDSUKE_TO.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_IKATSU_NYURYOKU = this.form.IKATSU_NYURYOKU.Text;
            //排出事業場CD
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYAJOU_CD = this.form.HAISHUTSUJIGYOUBA_CD.Text;
            //排出事業場名
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HAISHUTSUJIGYOUSYAJOU_NAME = this.form.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text;
            //返送先(取引先)CD
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_TORIHIKISAKI_CD = this.form.HENSOU_TORIHIKISAKI_CD.Text;
            //返送先(取引先)名
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_TORIHIKISAKI_NAME = this.form.HENSOU_TORIHIKISAKI_NAME_RYAKU.Text;
            //返送先(業者)CD
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GYOUSYA_CD = this.form.HENSOU_GYOUSHA_CD.Text;
            //返送先(業者)名
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GYOUSYA_NAME = this.form.HENSOU_GYOUSHA_NAME_RYAKU.Text;
            //返送先(現場)CD
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GENBA_CD = this.form.HENSOU_GENBA_CD.Text;
            //返送先(現場)名
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_HENSOUSAKI_GENBA_NAME = this.form.HENSOU_GENBA_NAME_RYAKU.Text;

            //CongBinh 20200330 #134989 S
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_RENZOKU_KBN = this.form.txtNum_Renzoku.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_A = this.form.checkbox_Ahyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B1 = this.form.checkbox_B1hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B2 = this.form.checkbox_B2hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B4 = this.form.checkbox_B4hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_B6 = this.form.checkbox_B6hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_C1 = this.form.checkbox_C1hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_C2 = this.form.checkbox_C2hyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_D = this.form.checkbox_Dhyou.Checked ? "1" : "0";
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.HYOU_E = this.form.checkbox_Ehyou.Checked ? "1" : "0";
            //CongBinh 20200330 #134989 E
            //MOD NHU 20211103 #157020 S
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GYOUSHA_CD = this.form.cantxt_SyobunJyutakuNameCd.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GYOUSHA_NAME = this.form.ctxt_SyobunJyutakuName.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text;
            Shougun.Core.PaperManifest.HenkyakuIchiran.Properties.Settings.Default.SET_SBN_GENBA_NAME = this.form.ctxt_UnpanJyugyobaName.Text;
            //MOD NHU 20211103 #157020 E
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

        /// <summary>
        /// 日付一括入力状態設定
        /// </summary>
        /// <returns></returns>
        public void SetHidukeIkatsuNyuryoku()
        {
            DataTable dt = new DataTable();
            dt = SearchHizukeIkatsuNyuryokuEntry();
            string use_a = null;
            string use_b1 = null;
            string use_b2 = null;
            string use_b4 = null;
            string use_b6 = null;
            string use_c1 = null;
            string use_c2 = null;
            string use_d = null;
            string use_e = null;
            if (dt.Rows.Count > 0)
            {
                use_a = dt.Rows[0]["MANIFEST_USE_A"].ToString();
                use_b1 = dt.Rows[0]["MANIFEST_USE_B1"].ToString();
                use_b2 = dt.Rows[0]["MANIFEST_USE_B2"].ToString();
                use_b4 = dt.Rows[0]["MANIFEST_USE_B4"].ToString();
                use_b6 = dt.Rows[0]["MANIFEST_USE_B6"].ToString();
                use_c1 = dt.Rows[0]["MANIFEST_USE_C1"].ToString();
                use_c2 = dt.Rows[0]["MANIFEST_USE_C2"].ToString();
                use_d = dt.Rows[0]["MANIFEST_USE_D"].ToString();
                use_e = dt.Rows[0]["MANIFEST_USE_E"].ToString();
            }
        }

        /// <summary>
        /// 明細部日付一括入力状態設定
        /// </summary>
        /// <returns></returns>
        public void SetHidukeIkatsuNyuryokuMeisai()
        {
            DataTable dt = new DataTable();
            dt = SearchHizukeIkatsuNyuryokuEntry();
            string use_a = null;
            string use_b1 = null;
            string use_b2 = null;
            string use_b4 = null;
            string use_b6 = null;
            string use_c1 = null;
            string use_c2 = null;
            string use_d = null;
            string use_e = null;
            if (dt.Rows.Count > 0)
            {
                use_a = dt.Rows[0]["MANIFEST_USE_A"].ToString();
                use_b1 = dt.Rows[0]["MANIFEST_USE_B1"].ToString();
                use_b2 = dt.Rows[0]["MANIFEST_USE_B2"].ToString();
                use_b4 = dt.Rows[0]["MANIFEST_USE_B4"].ToString();
                use_b6 = dt.Rows[0]["MANIFEST_USE_B6"].ToString();
                use_c1 = dt.Rows[0]["MANIFEST_USE_C1"].ToString();
                use_c2 = dt.Rows[0]["MANIFEST_USE_C2"].ToString();
                use_d = dt.Rows[0]["MANIFEST_USE_D"].ToString();
                use_e = dt.Rows[0]["MANIFEST_USE_E"].ToString();
            }
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

            string oldCREATE_USER = string.Empty;
            SqlDateTime oldCREATE_DATE = DateTime.Now;
            string oldCREATE_PC = string.Empty;

            try
            {
                //明細部データ取得
                this.GetMeisaiIchiranData();
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    if (mmanifestretdateMsList != null && mmanifestretdateMsList.Count() > 0)
                    {
                        foreach (T_MANIFEST_RET_DATE updateData in mmanifestretdateMsList)
                        {
                            T_MANIFEST_RET_DATE dt = t_manifest_ret_date_daocls.GetDataByCd(updateData.SYSTEM_ID, updateData.SEQ);
                            if (dt != null)
                            {
                                //入力Entityの作成情報を設定
                                oldCREATE_USER = dt.CREATE_USER;
                                oldCREATE_DATE = dt.CREATE_DATE;
                                oldCREATE_PC = dt.CREATE_PC;
                                // 更新者情報設定
                                var dataBinderLogicEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_RET_DATE>(dt);
                                dataBinderLogicEntry.SetSystemProperty(dt, true);
                                dt.DELETE_FLG = true;
                                dt.CREATE_USER = oldCREATE_USER;
                                dt.CREATE_DATE = oldCREATE_DATE;
                                dt.CREATE_PC = oldCREATE_PC;
                                int CntMopkUpd = t_manifest_ret_date_daocls.Update(dt);

                                dt.SEND_A = updateData.SEND_A;
                                dt.SEND_B1 = updateData.SEND_B1;
                                dt.SEND_B2 = updateData.SEND_B2;
                                dt.SEND_B4 = updateData.SEND_B4;
                                dt.SEND_B6 = updateData.SEND_B6;
                                dt.SEND_C1 = updateData.SEND_C1;
                                dt.SEND_C2 = updateData.SEND_C2;
                                dt.SEND_D = updateData.SEND_D;
                                dt.SEND_E = updateData.SEND_E;
                                dt.SEQ = dt.SEQ + 1;
                                dt.DELETE_FLG = false;
                                CntMopkUpd = t_manifest_ret_date_daocls.Insert(dt);
                            }
                            else
                            {
                                //更新者情報設定
                                //入力Entityの作成情報を設定
                                dt = new T_MANIFEST_RET_DATE();
                                dt.SYSTEM_ID = updateData.SYSTEM_ID;
                                dt.SEQ = 1;
                                dt.SEND_A = updateData.SEND_A;
                                dt.SEND_B1 = updateData.SEND_B1;
                                dt.SEND_B2 = updateData.SEND_B2;
                                dt.SEND_B4 = updateData.SEND_B4;
                                dt.SEND_B6 = updateData.SEND_B6;
                                dt.SEND_C1 = updateData.SEND_C1;
                                dt.SEND_C2 = updateData.SEND_C2;
                                dt.SEND_D = updateData.SEND_D;
                                dt.SEND_E = updateData.SEND_E;
                                var dataBinderLogicEntry = new DataBinderLogic<r_framework.Entity.T_MANIFEST_RET_DATE>(dt);
                                dataBinderLogicEntry.SetSystemProperty(dt, false);
                                dt.DELETE_FLG = false;
                                int CntMopkUpd = t_manifest_ret_date_daocls.Insert(dt);
                            }
                        }
                    }
                    //返却日一覧で登録後、T_MANIFEST_ENTRY、T_MANIFEST_DETAIL、T_MANIFEST_UPN、T_MANIFEST_PRT、T_MANIFEST_DETAIL_PRT、
                    // T_MANIFEST_ENTRY updatainsert
                    if (mmanifestentryMsList != null && mmanifestentryMsList.Count() > 0)
                    {
                        foreach (T_MANIFEST_ENTRY manifestentry in mmanifestentryMsList)
                        {
                            T_MANIFEST_ENTRY updateData = this.MANIFEST_ENTRYDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
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
                                updateData.DELETE_FLG = true;
                                int CntUpd = this.MANIFEST_ENTRYDao.Update(updateData);
                                updateData.SEQ = updateData.SEQ + 1;
                                updateData.DELETE_FLG = false;
                                CntUpd = this.MANIFEST_ENTRYDao.Insert(updateData);
                            }

                            T_MANIFEST_PRT updatePRTData = this.MANIFEST_PRTDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updatePRTData != null)
                            {
                                updatePRTData.SEQ = updatePRTData.SEQ + 1;
                                int CntUpd = this.MANIFEST_PRTDao.Insert(updatePRTData);
                            }

                            T_MANIFEST_UPN[] updateUPNData = this.MANIFEST_UPNDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateUPNData != null)
                            {
                                foreach (T_MANIFEST_UPN getData in updateUPNData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_UPNDao.Insert(getData);
                                }
                            }

                            T_MANIFEST_DETAIL[] updateDETAILData = this.MANIFEST_DETAILDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateDETAILData != null && updateDETAILData.Length > 0)
                            {
                                foreach (T_MANIFEST_DETAIL getData in updateDETAILData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_DETAILDao.Insert(getData);
                                }
                            }

                            T_MANIFEST_DETAIL_PRT[] updateDETAILPRTData = this.MANIFEST_DETAIL_PRTDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateDETAILPRTData != null && updateDETAILPRTData.Length > 0)
                            {
                                foreach (T_MANIFEST_DETAIL_PRT getData in updateDETAILPRTData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_DETAIL_PRTDao.Insert(getData);
                                }
                            }

                            T_MANIFEST_KP_KEIJYOU[] updateKPKEIJYOUData = this.MANIFEST_KP_KEIJYOUDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateKPKEIJYOUData != null && updateKPKEIJYOUData.Length > 0)
                            {
                                foreach (T_MANIFEST_KP_KEIJYOU getData in updateKPKEIJYOUData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_KP_KEIJYOUDao.Insert(getData);
                                }
                            }

                            T_MANIFEST_KP_NISUGATA[] updateKPNISUGATAData = this.MANIFEST_KP_NISUGATADao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateKPNISUGATAData != null && updateKPNISUGATAData.Length > 0)
                            {
                                foreach (T_MANIFEST_KP_NISUGATA getData in updateKPNISUGATAData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_KP_NISUGATADao.Insert(getData);
                                }
                            }

                            T_MANIFEST_KP_SBN_HOUHOU[] updateKPSBNHOUHOUData = this.MANIFEST_KP_SBN_HOUHOUDao.GetDataByCd(manifestentry.SYSTEM_ID, manifestentry.SEQ);
                            if (updateKPSBNHOUHOUData != null && updateKPSBNHOUHOUData.Length > 0)
                            {
                                foreach (T_MANIFEST_KP_SBN_HOUHOU getData in updateKPSBNHOUHOUData)
                                {
                                    getData.SEQ = getData.SEQ + 1;
                                    int CntUpd = this.MANIFEST_KP_SBN_HOUHOUDao.Insert(getData);
                                }
                            }
                        }
                    }
                    tran.Commit();
                    new MessageBoxShowLogic().MessageBoxShow("I001", "登録");
                }
            }
            catch (NotSingleRowUpdatedRuntimeException notSingleRowUpdateRuntimeException)
            {
                LogUtility.Debug(notSingleRowUpdateRuntimeException.Message);

                new MessageBoxShowLogic().MessageBoxShow("E093");

                return false;
            }
            catch (SQLRuntimeException sqlRuntimeException)
            {
                LogUtility.Debug(sqlRuntimeException.Message);

                if ("ESSR0071" == sqlRuntimeException.MessageCode)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E199");
                }

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex.Message);

                throw ex;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        /// <summary>
        /// 明細部データ取得
        /// </summary>
        private void GetMeisaiIchiranData()
        {
            LogUtility.DebugMethodStart();

            try
            {
                DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
                List<T_MANIFEST_RET_DATE> manifestretdateMsList = new List<T_MANIFEST_RET_DATE>();
                List<T_MANIFEST_RET_DATE> manifestretdateMsListInsert = new List<T_MANIFEST_RET_DATE>();
                List<T_MANIFEST_ENTRY> manifestentryMsListInsert = new List<T_MANIFEST_ENTRY>();
                String UsrName = System.Environment.UserName;
                UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
                DateTime datatime = this.parentForm.sysDate;
                string pcname = System.Environment.MachineName;

                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (dgvRow.Cells[0].Value != null)
                    {
                        if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                        {
                            T_MANIFEST_RET_DATE manifestretdate = new T_MANIFEST_RET_DATE();
                            T_MANIFEST_RET_DATE manifestretdateInsert = new T_MANIFEST_RET_DATE();
                            T_MANIFEST_ENTRY manifestentryInsert = new T_MANIFEST_ENTRY();

                            //insert
                            manifestentryInsert.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells["SYSTEM_ID"].Value.ToString());
                            manifestentryInsert.SEQ = SqlInt32.Parse(dgvRow.Cells["SEQ"].Value.ToString());

                            manifestretdateInsert.SYSTEM_ID = SqlInt64.Parse(dgvRow.Cells["SYSTEM_ID"].Value.ToString());
                            manifestretdateInsert.SEQ = SqlInt32.Parse(dgvRow.Cells["hiddenRET_DATE_SEQ"].Value.ToString());
                            if (dgvRow.Cells["A票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["A票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_A = SqlDateTime.Parse(dgvRow.Cells["A票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["B1票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["B1票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_B1 = SqlDateTime.Parse(dgvRow.Cells["B1票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["B2票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["B2票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_B2 = SqlDateTime.Parse(dgvRow.Cells["B2票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["B4票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["B4票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_B4 = SqlDateTime.Parse(dgvRow.Cells["B4票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["B6票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["B6票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_B6 = SqlDateTime.Parse(dgvRow.Cells["B6票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["C1票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["C1票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_C1 = SqlDateTime.Parse(dgvRow.Cells["C1票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["C2票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["C2票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_C2 = SqlDateTime.Parse(dgvRow.Cells["C2票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["D票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["D票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_D = SqlDateTime.Parse(dgvRow.Cells["D票"].Value.ToString());
                                }
                            }

                            if (dgvRow.Cells["E票"].Value != null)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells["E票"].Value.ToString()))
                                {
                                    manifestretdateInsert.SEND_E = SqlDateTime.Parse(dgvRow.Cells["E票"].Value.ToString());
                                }
                            }

                            manifestretdateMsListInsert.Add(manifestretdateInsert);
                            manifestentryMsListInsert.Add(manifestentryInsert);
                        }
                    }
                }
                mmanifestretdateMsList = manifestretdateMsListInsert;
                mmanifestretdateMsListInsert = manifestretdateMsListInsert;
                mmanifestentryMsList = manifestentryMsListInsert;
            }
            catch (Exception ex)
            {
                new MessageBoxShowLogic().MessageBoxShow("E093");

                LogUtility.Fatal("登録データ作成失敗", ex);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者チェック(排出事業者)
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="colname">チェックカラム名称</param>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int ChkGyosya(object obj, string colname)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(obj, colname);

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

                MessageBoxUtility.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
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
        /// 排出業者削除
        /// </summary>
        public void HaisyutuGyousyaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.HAISHUTSUJIGYOUSYA_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 排出業場削除
        /// </summary>
        public void HaisyutuGyouBaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.HAISHUTSUJIGYOUBA_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 返送取引先削除
        /// </summary>
        public void HensouTorihikisakiDel()
        {
            LogUtility.DebugMethodStart();

            this.form.HENSOU_TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 返送業者削除
        /// </summary>
        public void HensouGyoushaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.HENSOU_GYOUSHA_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 返送現場削除
        /// </summary>
        public void HensouGenbaDel()
        {
            LogUtility.DebugMethodStart();

            this.form.HENSOU_GENBA_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd();
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
        /// ファンクションボタン(F1F8)制御
        /// </summary>
        /// <param name="chek">1:オン 2:オフ</param>
        public void SetEnableFnc1(string chek)
        {
            LogUtility.DebugMethodStart();

            // 親フォームオブジェクト取得
            parentForm = (BusinessBaseForm)this.form.Parent;

            if (chek == "1")
            {
                this.parentForm.bt_func1.Enabled = true;
                this.parentForm.bt_func8.Enabled = true;
            }
            else
            {
                this.parentForm.bt_func1.Enabled = false;
                this.parentForm.bt_func8.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場チェック(排出事業場)
        /// </summary>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int ChkGenba(object objGyousha, object objGenBa, object objNm)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(objGyousha, objGenBa, objNm);

                TextBox txtGyousha = (TextBox)objGyousha;
                TextBox txtGenba = (TextBox)objGenBa;
                if (txtGenba.Text == string.Empty)
                {
                    ((TextBox)objNm).Text = string.Empty;
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new M_GENBA();
                Serch.GYOUSHA_CD = txtGyousha.Text;
                Serch.GENBA_CD = txtGenba.Text;

                this.SearchResult = new DataTable();
                DataTable dt = genbaDao.GetGenbaDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    ((TextBox)objNm).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                MessageBoxUtility.MessageBoxShow("E020", "現場");

                txtGenba.Focus();
                txtGenba.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGenba", ex);
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
        /// 取引先チェック(返送先)
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int ChkHensouTorihikisaki(object obj)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(obj);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodStart(ret);
                    return ret;
                }

                var Serch = new M_TORIHIKISAKI();
                Serch.TORIHIKISAKI_CD = txt.Text;

                this.SearchResult = new DataTable();
                DataTable dt = torihikisakiDao.GetTorihikisakiDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    ret = 0;
                    LogUtility.DebugMethodStart(ret);
                    return ret;
                }

                MessageBoxUtility.MessageBoxShow("E020", "取引先");

                txt.Focus();
                txt.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHensouTorihikisaki", ex);
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
        /// 業者チェック(返送先)
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int ChkHensouGyosya(object obj)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(obj);

                TextBox txt = (TextBox)obj;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new M_GYOUSHA();
                Serch.GYOUSHA_CD = txt.Text;

                this.SearchResult = new DataTable();
                DataTable dt = gyoushaDao.GetGyoushaDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                string errorMessage = string.Empty;

                MessageBoxUtility.MessageBoxShow("E020", "業者");

                txt.Focus();
                txt.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHensouGyosya", ex);
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
        /// 現場チェック(返送先)
        /// </summary>
        /// <param name="obj">チェックコントロール</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>０：正常 1:空 2：エラー</returns>
        public int ChkHensouGenba(object objCd, object objNM, string gyoushaCd)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(objCd, objNM, gyoushaCd);

                TextBox txt = (TextBox)objCd;
                if (txt.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    ((TextBox)objNM).Text = string.Empty;
                    return ret;
                }

                var Serch = new M_GENBA();
                Serch.GENBA_CD = txt.Text;
                Serch.GYOUSHA_CD = gyoushaCd;

                this.SearchResult = new DataTable();
                DataTable dt = genbaDao.GetGenbaGyoushaDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    ((TextBox)objNM).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                MessageBoxUtility.MessageBoxShow("E020", "現場");

                txt.Focus();
                txt.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHensouGenba", ex);
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
        /// バーコード上段、下段チェック
        /// </summary>
        /// <param name="strbarcode">バーコード</param>
        /// <returns>True：上段 False:下段</returns>
        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
        public int chkBarcode(string strbarcode, bool isKoufubango = false) //CongBinh 20200330 #134989
        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
        {
            LogUtility.DebugMethodStart(strbarcode, isKoufubango);//CongBinh 20200330 #134989
            //CongBinh 20200330 #134989 S
            if (!isKoufubango)
            {
                if (strbarcode.Substring(0, 8) == barcodeGedanKey)
                {
                    // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
                    LogUtility.DebugMethodEnd(1);
                    return 1;
                    // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
                }
            }
            //CongBinh 20200330 #134989 E
            // 2014.05.30  1行目のマニフェストをバーコード読み込む。 返却日はクリアしないように修正
            if (this.SearchBarcodeResult != null && this.SearchBarcodeResult.Columns.Count > 0 && this.SearchBarcodeResult.Select(" hiddenMANIFEST_ID = '" + (isKoufubango ? this.form.KOUFUBANNGO.Text : this.form.txtKoufuNo_Hyou.Text) + "'").Length > 0)//CongBinh 20200330 #134989
            {
                // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
                LogUtility.DebugMethodEnd(2);
                return 2;
                // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
            }
            // 2014.05.30  1行目のマニフェストをバーコード読み込む。 返却日はクリアしないように修正

            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
            LogUtility.DebugMethodEnd(0);
            return 0;
            // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
        }

        /// <summary>
        /// バーコード(下段)票チェック
        /// </summary>
        /// <param name="strbarcode"></param>
        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
        public bool chkBarcodeHyou(string strbarcode, out bool catchErr)
        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(strbarcode);
                // 20140624 syunrei EV004974_バーコード読込時アラート start
                MessageBoxShowLogic ms = new MessageBoxShowLogic();
                // 20140624 syunrei EV004974_バーコード読込時アラート end
                //選択行を取得
                int cnt = this.form.customDataGridView1.CurrentCell.RowIndex;
                //返却日を取得
                string strdate = this.form.dtimeHenkyakubi.Text;
                // 2014.05.16 返却日を入力してから、再度バーコードを読み取ってください。by胡 start
                if (string.IsNullOrEmpty(strdate))
                {
                    //返却日フォーカス設定しました。
                    this.form.dtimeHenkyakubi.Focus();
                    //返却日を入力してから、再度バーコードを読み取ってください。
                    MessageBoxUtility.MessageBoxShow("W005");
                    // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
                    return ret;
                    // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
                }
                // 2014.05.16 返却日を入力してから、再度バーコードを読み取ってください。by胡 end

                //画面返却日を設定
                switch (strbarcode.Substring(7, 4))
                {
                    case "0101":
                        //A票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["A票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["A票"].Value = strdate;
                        break;

                    case "0201":
                        //B1票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["B1票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["B1票"].Value = strdate;
                        break;

                    case "0202":
                        //B2票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["B2票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["B2票"].Value = strdate;
                        break;

                    case "0301":
                        //C1票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["C1票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["C1票"].Value = strdate;
                        break;

                    case "0302":
                        //C2票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["C2票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["C2票"].Value = strdate;
                        break;

                    case "0401":
                        //D票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["D票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["D票"].Value = strdate;
                        break;

                    case "0501":
                        //E票
                        // 20140623 katen EV004973_使用しない返却日に日付が入る start
                        if (!this.CheckHyoReadOnly(this.form.customDataGridView1.Rows[cnt].Cells["E票"])) { return false; }
                        // 20140623 katen EV004973_使用しない返却日に日付が入る end
                        this.form.customDataGridView1.Rows[cnt].Cells["E票"].Value = strdate;
                        break;

                    default:
                        // 20140624 syunrei EV004974_バーコード読込時アラート start
                        ms.MessageBoxShow("C001");
                        // 20140624 syunrei EV004974_バーコード読込時アラート end
                        // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない start
                        //return;
                        return ret;
                    // 20140623 katen EV004872 バーコードを読むときに票種類からよむことができない end
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("chkBarcodeHyou", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        // 20140623 katen EV004973_使用しない返却日に日付が入る start
        public bool CheckHyoReadOnly(DataGridViewCell cell)
        {
            if (cell.ReadOnly)
            {
                MessageBoxUtility.MessageBoxShow("E179");
                return false;
            }
            return true;
        }

        // 20140623 katen EV004973_使用しない返却日に日付が入る end

        // 2014.03.25 システム情報から取得したマニフェスト情報使用区分。by 胡 begin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objMANIFEST_USE"></param>
        /// <param name="strKbnValue"></param>
        /// <param name="objRadioButton"></param>
        private void SetHitsukeikatsu(SqlInt16 objMANIFEST_USE, string strKbnValue, CustomRadioButton objRadioButton)
        {
            // 票
            if (objMANIFEST_USE == 1)
            {
                objRadioButton.Enabled = true;
                if (string.IsNullOrEmpty(this.mCharacterLimitList))
                {
                    this.mCharacterLimitList = strKbnValue;
                }
                else
                {
                    this.mCharacterLimitList = this.mCharacterLimitList + "," + strKbnValue;
                }
            }
            else
            {
                objRadioButton.Enabled = false;
            }
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
                            if (this.form.customDataGridView1.Rows[rowIndex].Cells["A票"].ReadOnly)
                            {
                                this.form.customDataGridView1.CurrentCell = null;
                            }
                            else
                            {
                                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[rowIndex].Cells["A票"];
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

            // 並び替えの対象項目の値が変更された時、一覧にソートが発生するための対策
            SetHenkyakuhiNyuuryokuEnabled(false);
        }

        // 2014.05.20 by 胡 start
        /// <summary>
        /// マニフェストの排出事業場の現場毎にA～E票使用区分が「使用しない」となっている場合、グレーアウトしてください。
        /// </summary>
        /// <param name="hasSearchedGenba">true:現場検索する false:現場検索しない</param>
        private void SetHenkyakuhiNyuuryokuEnabled(bool hasSearchedGenba)
        {
            LogUtility.DebugMethodStart();

            // システム設定の情報を反映
            SetReadOnlyCheckAToCheckE();

            IM_GENBADao GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

            if (allGenba == null || hasSearchedGenba)
            {
                // TODO 全カラムではなく、必要なカラムのみに絞ること
                //CongBinh 20200330 #134989 S
                var cond = string.Join("','", this.lstGyoushaGenbaCd);
                string sql = " SELECT * FROM M_GENBA WHERE GYOUSHA_CD + GENBA_CD in('" + cond + "')";
                var tmp = GenbaDao.GetDateForStringSql(sql);
                allGenba = EntityUtility.DataTableToEntity<M_GENBA>(tmp).ToList();
                //CongBinh 20200330 #134989 E
            }

            //明細行が１件以上有った場合判断
            if (this.form.customDataGridView1.Rows.Count > 0)
            {
                //1.全ての明細行の各返却日(A票～E票「使用しない」となっている場合、グレーアウトしてください
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    string gyoushaCd = dgvRow.Cells["hiddenHST_GYOUSHA_CD"].Value == null ? string.Empty : dgvRow.Cells["hiddenHST_GYOUSHA_CD"].Value.ToString();
                    string genbaCd = dgvRow.Cells["hiddenHST_GENBA_CD"].Value == null ? string.Empty : dgvRow.Cells["hiddenHST_GENBA_CD"].Value.ToString();

                    M_GENBA dtGenba = allGenba.Where(n => n.GYOUSHA_CD.Equals(gyoushaCd) && n.GENBA_CD.Equals(genbaCd))
                                              .FirstOrDefault();

                    if (dtGenba != null && this.sysInfoEntity != null)
                    {
                        if (this.sysInfoEntity.MANIFEST_USE_A == 1 && dtGenba.MANI_HENSOUSAKI_USE_A == 1)
                        {
                            dgvRow.Cells["A票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["A票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_B1 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B1 == 1)
                        {
                            dgvRow.Cells["B1票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["B1票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_B2 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B2 == 1)
                        {
                            dgvRow.Cells["B2票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["B2票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_B4 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B4 == 1)
                        {
                            dgvRow.Cells["B4票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["B4票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_B6 == 1 && dtGenba.MANI_HENSOUSAKI_USE_B6 == 1)
                        {
                            dgvRow.Cells["B6票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["B6票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_C1 == 1 && dtGenba.MANI_HENSOUSAKI_USE_C1 == 1)
                        {
                            dgvRow.Cells["C1票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["C1票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_C2 == 1 && dtGenba.MANI_HENSOUSAKI_USE_C2 == 1)
                        {
                            dgvRow.Cells["C2票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["C2票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_D == 1 && dtGenba.MANI_HENSOUSAKI_USE_D == 1)
                        {
                            dgvRow.Cells["D票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["D票"].ReadOnly = true;
                        }
                        if (this.sysInfoEntity.MANIFEST_USE_E == 1 && dtGenba.MANI_HENSOUSAKI_USE_E == 1)
                        {
                            dgvRow.Cells["E票"].ReadOnly = false;
                        }
                        else
                        {
                            dgvRow.Cells["E票"].ReadOnly = true;
                        }
                    }
                    // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう start
                    if (dgvRow.Cells["hiddenHAIKI_KBN_CD"].Value.ToString() == "1" || dgvRow.Cells["hiddenHAIKI_KBN_CD"].Value.ToString() == "2")
                    {
                        dgvRow.Cells["B4票"].ReadOnly = true;
                        dgvRow.Cells["B6票"].ReadOnly = true;
                    }
                    else
                    {
                        dgvRow.Cells["B1票"].ReadOnly = true;
                        dgvRow.Cells["B4票"].ReadOnly = false;
                        dgvRow.Cells["B6票"].ReadOnly = false;
                    }
                    // 20140623 katen EV004672 返却日入力で産廃(直行)と建廃でもB4票とB6票の返却日が入力できてしまう end

                    // 現場がいない場合、返却日は入力できないようにする。
                    if (dtGenba == null || this.sysInfoEntity == null)
                    {
                        dgvRow.Cells["A票"].ReadOnly = true;
                        dgvRow.Cells["B1票"].ReadOnly = true;
                        dgvRow.Cells["B2票"].ReadOnly = true;
                        dgvRow.Cells["B4票"].ReadOnly = true;
                        dgvRow.Cells["B6票"].ReadOnly = true;
                        dgvRow.Cells["C1票"].ReadOnly = true;
                        dgvRow.Cells["C2票"].ReadOnly = true;
                        dgvRow.Cells["D票"].ReadOnly = true;
                        dgvRow.Cells["E票"].ReadOnly = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }
        // 2014.05.20 by 胡 end
        
        // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
        /// 交付番号入力チェック
        /// </summary>
        /// <returns>true:異常 false:正常</returns>
        public bool ChkKohuNo()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();
                string ret = Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic.ChkKoufuNo(this.form.KOUFUBANNGO.Text, false);

                if (!string.IsNullOrEmpty(ret))
                {
                    //エラー時は自前で表示
                    Message.MessageBoxUtility.MessageBoxShowError(ret);
                }
                isErr = !string.IsNullOrEmpty(ret);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }
        // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end

        // 20141022 Houkakou 「マニチェック表」の日付チェックを追加する start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.HIDSUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDSUKE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.txtNum_Hidsuke.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HIDSUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HIDSUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.HIDSUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.HIDSUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.HIDSUKE_FROM.IsInputErrorOccured = true;
                this.form.HIDSUKE_TO.IsInputErrorOccured = true;
                this.form.HIDSUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HIDSUKE_TO.BackColor = Constans.ERROR_COLOR;
                if (this.form.txtNum_Hidsuke.Text.Equals("1"))
                {
                    msgLogic.MessageBoxShow("E030", "運搬終了日From", "運搬終了日To");
                }
                else if (this.form.txtNum_Hidsuke.Text.Equals("2"))
                {
                    msgLogic.MessageBoxShow("E030", "処分終了日From", "処分終了日To");
                }
                else
                {
                    msgLogic.MessageBoxShow("E030", "最終処分終了日From", "最終処分終了日To");
                }
                this.form.HIDSUKE_FROM.Focus();
                return true;
            }

            return false;
        }

        #endregion

        // 20141022 Houkakou 「マニチェック表」の日付チェックを追加する end

        // 20141128 Houkakou 「返却日入力」のダブルクリックを追加する start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDSUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDSUKE_FROM;
            var ToTextBox = this.form.HIDSUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141128 Houkakou 「返却日入力」のダブルクリックを追加する end
        #region CongBinh 20200330 #134989
        public bool CheckExistsCheckboxOn()
        {
            bool isExists = false;

            if (this.form.checkbox_Ahyou.Checked || this.form.checkbox_B1hyou.Checked || this.form.checkbox_B2hyou.Checked
                || this.form.checkbox_B4hyou.Checked || this.form.checkbox_B6hyou.Checked || this.form.checkbox_C1hyou.Checked
                || this.form.checkbox_C2hyou.Checked || this.form.checkbox_Dhyou.Checked || this.form.checkbox_Ehyou.Checked)
            {
                isExists = true;
            }

            return isExists;
        }
        #endregion

        #region MOD NHU 20211004 #155770
        /// <summary>
        /// ユーザ設定から拠点を画面に設定します
        /// </summary>
        internal void SetKyoten()
        {
            LogUtility.DebugMethodStart();

            var fileAccess = new XMLAccessor();
            var config = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            var kyotenCd = config.ItemSetVal1;

            if (!string.IsNullOrEmpty(kyotenCd))
            {
                this.headForm.KYOTEN_CD.Text = config.ItemSetVal1.PadLeft(2, '0');
            }

            this.headForm.KYOTEN_NAME.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                var kyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>().GetDataByCd(this.headForm.KYOTEN_CD.Text);
                if (null != kyoten)
                {
                    this.headForm.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = this.form.controlUtil.GetAllControls(this.headForm);
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.headForm.KYOTEN_CD.Focus();
                    isErr = true;
                }
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
        #endregion

        #region MOD NHU 20211103 #157020
        public int ChkGenbaSbn(object objGyousha, object objGenBa, object objNm)
        {
            int ret = 2;
            try
            {
                LogUtility.DebugMethodStart(objGyousha, objGenBa, objNm);

                TextBox txtGyousha = (TextBox)objGyousha;
                TextBox txtGenba = (TextBox)objGenBa;
                if (txtGenba.Text == string.Empty)
                {
                    ((TextBox)objNm).Text = string.Empty;
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var Serch = new M_GENBA();
                Serch.GYOUSHA_CD = txtGyousha.Text;
                Serch.GENBA_CD = txtGenba.Text;

                this.SearchResult = new DataTable();
                DataTable dt = genbaDao.GetGenbaSbnDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    ((TextBox)objNm).Text = dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    ret = 0;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                MessageBoxUtility.MessageBoxShow("E020", "現場");

                txtGenba.Focus();
                txtGenba.SelectAll();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGenba", ex);
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
        #endregion
    }
}