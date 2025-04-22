using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Dao;
using System.Collections.ObjectModel;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv
{
    #region ビジネスロジック
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>ボタンの設定用ファイルパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv.Setting.ButtonSetting.xml";

        /// <summary>
        /// 実績報告書（運搬実績）CSV出力のForm
        /// </summary>
        public UIForm form { get; set; }

        /// <summary>
        /// 実績報告書（運搬実績）CSV出力のHeader
        /// </summary>
        public UIHeader headerform { get; set; }

        /// <summary> 親フォーム</summary>
        public BasePopForm parentbaseform { get; set; }

        /// <summary>画面初期表示Flag</summary>
        private bool firstLoadFlg = true;

        /// <summary>Dao</summary>
        public DAOClass dao { get; set; }
        private EntryDAO EntryDao { get; set; }
        private ManiDetailDAO DetailDao { get; set; }
        private UnpanDAO UnpanDao { get; set; }
        private IM_HAIKI_SHURUIDao HaikishuruiDao { get; set; }
        private IM_GYOUSHADao GyoushaDao { get; set; }
        private IM_GENBADao GenbaDao { get; set; }
        private IM_CHIIKIBETSU_SHOBUNDao ChiikiShobunDao { get; set; }

        /// <summary>CustomDataGridView</summary>
        r_framework.CustomControl.CustomDataGridView gridCSV;

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        private MessageBoxShowLogic MsgBox;

        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        M_SYS_INFO mSysInfo;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        #endregion

        #region 初期化

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.EntryDao = DaoInitUtility.GetComponent<EntryDAO>();
            this.DetailDao = DaoInitUtility.GetComponent<ManiDetailDAO>();
            this.UnpanDao = DaoInitUtility.GetComponent<UnpanDAO>();
            this.HaikishuruiDao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.ChiikiShobunDao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_SHOBUNDao>();
            this.MsgBox = new MessageBoxShowLogic();
            this.mSysInfo = new DBAccessor().GetSysInfo();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            LogUtility.DebugMethodEnd(targetForm);
        }
        # endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.firstLoadFlg)
                {
                    // フォームインスタンスを取得
                    this.parentbaseform = (BasePopForm)this.form.Parent;
                    this.headerform = (UIHeader)parentbaseform.headerForm;

                    // ボタンのテキストを初期化
                    this.ButtonInit();

                    // イベントの初期化処理
                    this.EventInit();

                    if (this.ActionBeforeCheck())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E044");
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    if (this.DataCheck())
                    {
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    if (this.form.searchDto.CSV_SHUKEI_KBN == 1)
                    {
                        this.form.cbx_KoufuNo.Enabled = false;
                        this.form.cbx_KoufuDate.Enabled = false;
                    }
                    this.allControl = this.form.allControl;
                }
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
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BasePopForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
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
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BasePopForm)this.form.Parent;

            //CSV出力ボタン(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);
            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            this.headerform.btnZenOn.Click += new EventHandler(this.form.btnZenOn_Click);
            this.headerform.btnZenOff.Click += new EventHandler(this.form.btnZenOff_Click);
            this.form.btnHaishutuZenOn.Click += new EventHandler(this.form.btnHaishutuZenOn_Click);
            this.form.btnHaishutuZenOff.Click += new EventHandler(this.form.btnHaishutuZenOff_Click);
            this.form.btnUnpanZenOn.Click += new EventHandler(this.form.btnUnpanZenOn_Click);
            this.form.btnUnpanZenOff.Click += new EventHandler(this.form.btnUnpanZenOff_Click);
            this.form.btnTumikaeZenOn.Click += new EventHandler(this.form.btnTumikaeZenOn_Click);
            this.form.btnTumikaeZenOff.Click += new EventHandler(this.form.btnTumikaeZenOff_Click);
            this.form.btnShobunZenOn.Click += new EventHandler(this.form.btnShobunZenOn_Click);
            this.form.btnShobunZenOff.Click += new EventHandler(this.form.btnShobunZenOff_Click);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region データチェック
        /// <summary>
        /// データチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean DataCheck()
        {
            var messageShowLogic = new MessageBoxShowLogic();
            bool bunruiErrFlag = false;
            bool gyoushuErrFlag = false;
            this.SearchResult.Columns["運搬委託量"].ReadOnly = false;
            this.SearchResult.Columns["処分量"].ReadOnly = false;

            if (this.SearchResult == null || this.SearchResult.Rows.Count == 0)
            {
                return true;
            }
            for (int i = 0; i < this.SearchResult.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(Convert.ToString(this.SearchResult.Rows[i]["運搬品目CD"]))
                    || string.IsNullOrEmpty(Convert.ToString(this.SearchResult.Rows[i]["排出事業者CD"]))
                    || string.IsNullOrEmpty(Convert.ToString(this.SearchResult.Rows[i]["排出事業場CD"]))
                    || string.IsNullOrEmpty(Convert.ToString(this.SearchResult.Rows[i]["処分方法CD"]))
                    )
                {
                    DialogResult result = messageShowLogic.MessageBoxShow("C077", "マニフェスト伝票", "マニチェック表");
                    if (result == DialogResult.Yes)
                    {
                        FormManager.OpenForm("G124", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                        return true;
                    }
                    break;
                }
            }
            for (int i = 0; i < this.SearchResult.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.SearchResult.Rows[i]["HOUKOKUSHO_BUNRUI_CD"].ToString()))
                {
                    bunruiErrFlag = true;
                }
                else if (string.IsNullOrEmpty(this.SearchResult.Rows[i]["排出事業場業種CD"].ToString())
                        || string.IsNullOrEmpty(this.SearchResult.Rows[i]["処分事業場業種CD"].ToString())
                    )
                {
                    gyoushuErrFlag = true;
                }
            }

            if (bunruiErrFlag)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別分類", "マニチェック表", "分類コード");
                if (result == DialogResult.No)
                {
                    return true;
                }
            }

            if (gyoushuErrFlag)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別業種", "マニチェック表", "業種コード");
                if (result == DialogResult.No)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        internal void CSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if ((this.SearchResult.Rows == null) || (this.SearchResult.Rows.Count == 0))
                {
                    msgLogic.MessageBoxShow("E044");
                    return;
                }

                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    this.gridCSV = new r_framework.CustomControl.CustomDataGridView();
                    // CSV用グリッドにマルチローの表示データを移動
                    gridCSV.DataSource = this.GetDataTableForMultRow();
                    gridCSV.Refresh();

                    // CSV出力実行
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(gridCSV, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_JISSEKIHOKOKU_UNPAN_CSV), this.form);
                    var allControlAndHeaderControls = allControl.ToList();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// データをDataTable化
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTableForMultRow()
        {
            LogUtility.DebugMethodStart();

            // CSV用グリッドのバインド名からテーブル定義作成
            DataTable table = new DataTable();
            this.SetColumnName(table);
            if (table.Columns.Count == 1 && table.Columns[0].ColumnName == "マニフェスト区分")
            {
                table.Columns.Remove("マニフェスト区分");
            }
            this.form.Controls.Add(gridCSV);

            // 値を取得
            foreach (DataRow multiRow in this.SearchResult.Rows)
            {
                DataRow row = table.NewRow();

                foreach (DataColumn col in table.Columns)
                {
                    row[col.ColumnName] = multiRow[col.ColumnName];
                }
                table.Rows.Add(row);
            }
            DataTable dt = new DataTable();
            dt = table.Copy();
            if (this.form.cbx_UnpanJutakuRyou.Checked)
            {
                dt.Columns.Remove("運搬委託量");
            }
            if (this.form.cbx_ShobunRyou.Checked)
            {
                dt.Columns.Remove("処分量");
            }
            string[] arrayA = new string[dt.Columns.Count];

            for (int x = 0; x < dt.Columns.Count; x++)
            {

                DataColumn dr2 = dt.Columns[x];

                arrayA[x] = Convert.ToString(dr2.ColumnName);
            }
            int count = 1;
            string Columns = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Columns.Count == count)
                {
                    Columns += arrayA[j];
                }
                else
                {
                    Columns += arrayA[j] + ",";
                }
                count++;
            }
            DataTable dtCopy = table.Copy();
            DataView dv = table.DefaultView;
            dv.Sort = Columns;
            dtCopy = dv.ToTable();
            if (this.form.searchDto.CSV_SHUKEI_KBN == 1)
            {

                DataTable dtC = table.Clone();
                DataRow rowTemp = dtCopy.NewRow();
                bool blnFlg = false;
                decimal JYUTAKU_RYOU = 0;
                decimal SYOBUN_RYOU = 0;
                // 値を取得
                foreach (DataRow multiRow in dtCopy.Rows)
                {
                    DataRow row = dtC.NewRow();

                    foreach (DataColumn col in dtC.Columns)
                    {
                        row[col.ColumnName] = multiRow[col.ColumnName];
                        if ((col.ColumnName != "運搬委託量") && (col.ColumnName != "処分量"))
                        {
                            if (Convert.ToString(rowTemp[col.ColumnName]) != Convert.ToString(multiRow[col.ColumnName]))
                            {
                                blnFlg = true;
                            };
                        }
                    }
                    if (blnFlg)
                    {
                        dtC.Rows.Add(row);
                        if (this.form.cbx_UnpanJutakuRyou.Checked)
                        {
                            JYUTAKU_RYOU = Convert.ToDecimal(multiRow["運搬委託量"]);
                            blnFlg = false;
                        }
                        if (this.form.cbx_ShobunRyou.Checked)
                        {
                            SYOBUN_RYOU = Convert.ToDecimal(multiRow["処分量"]);
                            blnFlg = false;
                        }
                    }
                    else
                    {
                        if (this.form.cbx_UnpanJutakuRyou.Checked)
                        {
                            JYUTAKU_RYOU += Convert.ToDecimal(multiRow["運搬委託量"]);
                            dtC.Columns["運搬委託量"].ReadOnly = false;
                            dtC.Rows[dtC.Rows.Count - 1]["運搬委託量"] = JYUTAKU_RYOU;
                        }
                        if (this.form.cbx_ShobunRyou.Checked)
                        {
                            SYOBUN_RYOU += Convert.ToDecimal(multiRow["処分量"]);
                            dtC.Columns["処分量"].ReadOnly = false;
                            dtC.Rows[dtC.Rows.Count - 1]["処分量"] = SYOBUN_RYOU;
                        }
                    }
                    rowTemp = multiRow;
                }

                dtCopy = dtC;
            }

            // 数値項目にシステム設定のマニフェスト数量フォーマットを適用する。
            string manifestSuuryoFormatCD = this.mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();

            foreach (DataRow multiRow in dtCopy.Rows)
            {
                foreach (DataColumn col in dtCopy.Columns)
                {
                    if (col.ColumnName == "運搬委託量" || col.ColumnName == "処分量")
                    {
                        if (multiRow[col.ColumnName] != DBNull.Value)
                        {
                            decimal dec = Convert.ToDecimal(multiRow[col.ColumnName]);
                            dec = this.mlogic.GetSuuryoRound(dec, manifestSuuryoFormatCD);
                            multiRow[col.ColumnName] = dec;
                        }

                        continue;
                    }
                }
            }

            // 返却
            LogUtility.DebugMethodEnd(table);
            return dtCopy;
        }
        /// <summary>
        /// データをDataTable化
        /// </summary>
        /// <returns></returns>
        private void SetColumnName(DataTable table)
        {
            LogUtility.DebugMethodStart();
            // header部のColumnを生成
            // 排出事業者
            // 運搬品目
            if (this.form.cbx_UnpanHinmoku.Checked)
            {
                table.Columns.Add("運搬品目CD", typeof(string));
                table.Columns.Add("運搬品目", typeof(string));
                table.Columns.Add("石綿", typeof(string));
                table.Columns.Add("特定有害", typeof(string));
            }
            // 排出事業者
            if (this.form.cbx_HstGyousha.Checked)
            {
                table.Columns.Add("排出事業者CD", typeof(string));
                table.Columns.Add("排出事業者名", typeof(string));
            }
            // 排出事業者業種
            if (this.form.cbx_HstGyoushaGyoushu.Checked)
            {
                table.Columns.Add("排出事業者業種CD", typeof(string));
                table.Columns.Add("排出事業者業種名", typeof(string));
            }
            // 排出事業者住所
            if (this.form.cbx_HstGyoushaJusho.Checked)
            {
                table.Columns.Add("排出事業者住所", typeof(string));
            }
            // 排出事業者地域
            if (this.form.cbx_HstGyoushaChiiki.Checked)
            {
                table.Columns.Add("排出事業者地域CD", typeof(string));
                table.Columns.Add("排出事業者地域名", typeof(string));
            }
            // 排出事業場
            if (this.form.cbx_HstGenba.Checked)
            {
                table.Columns.Add("排出事業場CD", typeof(string));
                table.Columns.Add("排出事業場名", typeof(string));
            }
            // 排出事業場業種
            if (this.form.cbx_HstGenbaGyoushu.Checked)
            {
                table.Columns.Add("排出事業場業種CD", typeof(string));
                table.Columns.Add("排出事業場業種名", typeof(string));
            }
            // 排出事業場住所
            if (this.form.cbx_GenbaJusho.Checked)
            {
                table.Columns.Add("排出事業場住所", typeof(string));
            }
            // 排出事業場地域
            if (this.form.cbx_GenbaChiiki.Checked)
            {
                table.Columns.Add("排出事業場地域CD", typeof(string));
                table.Columns.Add("排出事業場地域名", typeof(string));
            }
            // 運搬委託量
            if (this.form.cbx_UnpanJutakuRyou.Checked)
            {
                table.Columns.Add("運搬委託量", typeof(string));
            }

            // 単位
            if (this.form.cbx_HstTani.Checked)
            {
                table.Columns.Add("単位1", typeof(string));
            }

            // 運搬受託者
            // 運搬受託者1
            if (this.form.cbx_UnpanJutakusho_1.Checked)
            {
                table.Columns.Add("運搬受託者CD1", typeof(string));
                table.Columns.Add("運搬受託者名1", typeof(string));
            }
            // 運搬受託者業種1
            if (this.form.cbx_UnpanGyoushu_1.Checked)
            {
                table.Columns.Add("運搬受託者業種CD1", typeof(string));
                table.Columns.Add("運搬受託者業種名1", typeof(string));
            }
            // 運搬受託者住所1
            if (this.form.cbx_UnpanJusho_1.Checked)
            {
                table.Columns.Add("運搬受託者住所1", typeof(string));
            }
            // 運搬受託者地域1
            if (this.form.cbx_UnpanChiiki_1.Checked)
            {
                table.Columns.Add("運搬受託者地域CD1", typeof(string));
                table.Columns.Add("運搬受託者地域名1", typeof(string));
            }
            // 運搬受託者許可番号1
            if (this.form.cbx_UnpanKyokaNo_1.Checked)
            {
                table.Columns.Add("運搬受託者許可番号1", typeof(string));
            }
            // 運搬受託者2
            if (this.form.cbx_UnpanJutakusho_2.Checked)
            {
                table.Columns.Add("運搬受託者CD2", typeof(string));
                table.Columns.Add("運搬受託者名2", typeof(string));
            }
            // 運搬受託者業種2
            if (this.form.cbx_UnpanGyoushu_2.Checked)
            {
                table.Columns.Add("運搬受託者業種CD2", typeof(string));
                table.Columns.Add("運搬受託者業種名2", typeof(string));
            }
            // 運搬受託者住所2
            if (this.form.cbx_UnpanJusho_2.Checked)
            {
                table.Columns.Add("運搬受託者住所2", typeof(string));
            }
            // 運搬受託者地域2
            if (this.form.cbx_UnpanChiiki_2.Checked)
            {
                table.Columns.Add("運搬受託者地域CD2", typeof(string));
                table.Columns.Add("運搬受託者地域名2", typeof(string));
            }
            // 運搬受託者許可番号2
            if (this.form.cbx_UnpanKyokaNo_2.Checked)
            {
                table.Columns.Add("運搬受託者許可番号2", typeof(string));
            }
            // 運搬受託者3
            if (this.form.cbx_UnpanJutakusho_3.Checked)
            {
                table.Columns.Add("運搬受託者CD3", typeof(string));
                table.Columns.Add("運搬受託者名3", typeof(string));
            }
            // 運搬受託者業種3
            if (this.form.cbx_UnpanGyoushu_3.Checked)
            {
                table.Columns.Add("運搬受託者業種CD3", typeof(string));
                table.Columns.Add("運搬受託者業種名3", typeof(string));
            }
            // 運搬受託者住所3
            if (this.form.cbx_UnpanJusho_3.Checked)
            {
                table.Columns.Add("運搬受託者住所3", typeof(string));
            }
            // 運搬受託者地域3
            if (this.form.cbx_UnpanChiiki_3.Checked)
            {
                table.Columns.Add("運搬受託者地域CD3", typeof(string));
                table.Columns.Add("運搬受託者地域名3", typeof(string));
            }
            // 運搬受託者許可番号3
            if (this.form.cbx_UnpanKyokaNo_3.Checked)
            {
                table.Columns.Add("運搬受託者許可番号3", typeof(string));
            }

            //積替保管
            //積替保管場所1
            if (this.form.cbx_TumikaeBasho_1.Checked)
            {
                table.Columns.Add("積替保管場所CD1", typeof(string));
                table.Columns.Add("積替保管場所名1", typeof(string));
            }
            //積替保管場所業種1
            if (this.form.cbx_TumikaeGyoushu_1.Checked)
            {
                table.Columns.Add("積替保管場所業種CD1", typeof(string));
                table.Columns.Add("積替保管場所業種名1", typeof(string));
            }
            //積替保管場所住所1
            if (this.form.cbx_TumikaeJusho_1.Checked)
            {
                table.Columns.Add("積替保管場所住所1", typeof(string));
            }
            //積替保管場所地域1
            if (this.form.cbx_Tumikae_chiiki_1.Checked)
            {
                table.Columns.Add("積替保管場所地域CD1", typeof(string));
                table.Columns.Add("積替保管場所地域名1", typeof(string));
            }
            //積替保管場所2
            if (this.form.cbx_TumikaeBasho_1.Checked)
            {
                table.Columns.Add("積替保管場所CD2", typeof(string));
                table.Columns.Add("積替保管場所名2", typeof(string));
            }
            //積替保管場所業種2
            if (this.form.cbx_TumikaeGyoushu_2.Checked)
            {
                table.Columns.Add("積替保管場所業種CD2", typeof(string));
                table.Columns.Add("積替保管場所業種名2", typeof(string));
            }
            //積替保管場所住所2
            if (this.form.cbx_TumikaeJusho_2.Checked)
            {
                table.Columns.Add("積替保管場所住所2", typeof(string));
            }
            //積替保管場所地域2
            if (this.form.cbx_Tumikae_chiiki_2.Checked)
            {
                table.Columns.Add("積替保管場所地域CD2", typeof(string));
                table.Columns.Add("積替保管場所地域名2", typeof(string));
            }

            //処分受託者
            //処分許可番号
            if (this.form.cbx_ShobunkyokaNo.Checked)
            {
                table.Columns.Add("処分許可番号", typeof(string));
            }
            //処分受託者
            if (this.form.cbx_ShobunJutakusha.Checked)
            {
                table.Columns.Add("処分受託者CD", typeof(string));
                table.Columns.Add("処分受託者名", typeof(string));
            }
            //処分受託者住所
            if (this.form.cbx_ShobunJutakushaJusho.Checked)
            {
                table.Columns.Add("処分受託者住所", typeof(string));
            }
            //処分受託者地域
            if (this.form.cbx_ShobunJutakushaChiiki.Checked)
            {
                table.Columns.Add("処分受託者地域CD", typeof(string));
                table.Columns.Add("処分受託者地域名", typeof(string));
            }
            //処分受託者業種
            if (this.form.cbx_ShobunJutakushaGyoushu.Checked)
            {
                table.Columns.Add("処分受託者業種CD", typeof(string));
                table.Columns.Add("処分受託者業種名", typeof(string));
            }
            //処分事業場
            if (this.form.cbx_ShobunGenba.Checked)
            {
                table.Columns.Add("処分事業場CD", typeof(string));
                table.Columns.Add("処分事業場名", typeof(string));
            }
            //処分事業場住所
            if (this.form.cbx_ShobunGenbaJusho.Checked)
            {
                table.Columns.Add("処分事業場住所", typeof(string));
            }
            //処分事業場業種
            if (this.form.cbx_ShobunGenbaGyoushu.Checked)
            {
                table.Columns.Add("処分事業場業種CD", typeof(string));
                table.Columns.Add("処分事業場業種名", typeof(string));
            }
            //処分事業場地域
            if (this.form.cbx_ShobunGenbaChiiki.Checked)
            {
                table.Columns.Add("処分事業場地域CD", typeof(string));
                table.Columns.Add("処分事業場地域名", typeof(string));
            }
            //処分量
            if (this.form.cbx_ShobunRyou.Checked)
            {
                table.Columns.Add("処分量", typeof(string));
            }
            //単位3
            if (this.form.cbx_Tani.Checked)
            {
                table.Columns.Add("単位3", typeof(string));
            }
            //処分方法
            if (this.form.cbx_ShobunHoushiki.Checked)
            {
                table.Columns.Add("処分方法CD", typeof(string));
                table.Columns.Add("処分方法", typeof(string));
            }

            //一次二次共通
            //交付番号
            if (this.form.cbx_KoufuNo.Checked)
            {
                table.Columns.Add("交付番号", typeof(string));
            }
            //交付年月日
            if (this.form.cbx_KoufuDate.Checked)
            {
                table.Columns.Add("交付年月日", typeof(string));
            }
            if (this.form.searchDto.CSV_SHUKEI_KBN == 2)
            {
                table.Columns.Add("マニフェスト区分", typeof(string));
            }
        }
        #endregion

        #region DataGridViewデータ件数チェック処理
        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();
                SearchResult = dao.GetManiData(this.form.searchDto);
                if (this.SearchResult.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録/更新/削除

        /// <summary>
        /// 
        /// </summary>
        public void LogicalDelete()
        {
            this.LogicalDelete2();
        }
        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDelete2()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion
}
