using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Shougun.Core.PaperManifest.JissekiHokokuCsv
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BasePopForm parentForm;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 検索条件entry
        /// </summary>
        private T_JISSEKI_HOUKOKU_ENTRY tjHoukokuentry;

        /// <summary>
        /// 集計フラグ
        /// </summary>
        private bool isSyukei = false;

        /// <summary>
        /// 検索用Dao
        /// </summary>
        private CsvDAO EntryDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.JissekiHokokuCsv.Setting.ButtonSetting.xml";

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic MsgBox;

        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        M_SYS_INFO mSysInfo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm, T_JISSEKI_HOUKOKU_ENTRY ent, bool syukeiFlg)
        {
            LogUtility.DebugMethodStart(targetForm, ent, syukeiFlg);

            this.EntryDao = DaoInitUtility.GetComponent<CsvDAO>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            this.mSysInfo = new DBAccessor().GetSysInfo();

            this.form = targetForm;
            this.tjHoukokuentry = ent;
            this.isSyukei = syukeiFlg;
            this.MsgBox = new MessageBoxShowLogic();
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BasePopForm)this.form.Parent;

                // ヘッダフォームオブジェクト取得
                headerForm = (UIHeader)parentForm.headerForm;

                ret = GetSearchResult();

                if (ret)
                {
                    // 初期値設定
                    SetInitValue();
                    // ボタンのテキストを初期化
                    ButtonInit();
                    // イベントの初期化処理
                    EventInit();
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

        /// <summary>
        /// 検索
        /// </summary>
        public bool GetSearchResult()
        {
            LogUtility.DebugMethodStart();

            bool ret = false;
            try
            {
                this.SearchResult = this.EntryDao.GetDataForEntity(this.tjHoukokuentry);

                int result = this.CheckResult();

                ret = !(result == -1);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    ret = false;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }

        /// <summary>
        /// 検索データチェック
        /// </summary>
        public int CheckResult()
        {

            var messageShowLogic = new MessageBoxShowLogic();

            bool bunruiErrFlag = false;
            bool gyoushuErrFlag = false;

            if (this.SearchResult.Rows.Count == 0)
            {
                DialogResult result = messageShowLogic.MessageBoxShow("C001");
                return -1;
            }
            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                foreach (DataRow dt in this.SearchResult.Rows)
                {
                    if (string.IsNullOrEmpty(dt["HST_GYOUSHA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["HST_GENBA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["SBN_GYOUSHA_CD"].ToString())
                        || string.IsNullOrEmpty(dt["SBN_GENBA_CD"].ToString())
                        || ("1".Equals(dt["ITAKU_KBN"].ToString()) && (string.IsNullOrEmpty(dt["ITAKUSAKI_CD"].ToString())
                                                       || string.IsNullOrEmpty(dt["ITAKUSAKI_GENBA_CD"].ToString())))
                        )
                    {
                        DialogResult result = messageShowLogic.MessageBoxShow("C077", "マニフェスト伝票", "マニチェック表");
                        if (result == DialogResult.Yes)
                        {
                            FormManager.OpenForm("G124", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                            return -1;
                        }
                        break;
                    }
                }

                foreach (DataRow dt in this.SearchResult.Rows)
                {

                    if (string.IsNullOrEmpty(dt["HOUKOKUSHO_BUNRUI_CD"].ToString()))
                    {
                        bunruiErrFlag = true;
                    }
                    else if ( string.IsNullOrEmpty(dt["HST_GENBA_GYOUSHU_CD"].ToString())
                          || string.IsNullOrEmpty(dt["SBN_GENBA_GYOUSHU_CD"].ToString())
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
                        return -1;
                    }
                }

                if (gyoushuErrFlag)
                {
                    DialogResult result = messageShowLogic.MessageBoxShow("C076", "マスター保守＞地域別業種", "マニチェック表", "業種コード");
                    if (result == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// 初期値設定
        /// </summary>
        private void SetInitValue()
        {
            // 個別設定読み込み
            this.SetHstJigyoushaVal(false);
            this.SetUpJutakushaVal1(false);
            this.SetTkHokanVal1(false);
            this.SetShobunJutakushaVal(false);
            this.SetCommonVal(false);
            this.SetUpJutakushaVal2(false);
            this.SetTkHokanVal2(false);
            this.SetItakuVal(false);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        internal void EventInit()
        {
            LogUtility.DebugMethodStart();

            // CSV出力処理
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // 全選択
            this.headerForm.btnZenOn.Click += new System.EventHandler(this.btnZenOn_Click);
            // 全解除
            this.headerForm.btnZenOff.Click += new System.EventHandler(this.btnZenOff_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.OutputCsvFile();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 排出事業者
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetHstJigyoushaVal(bool isVal)
        {
            this.form.cbxHstTani.Checked = isVal;
            this.form.cbxSyobunJutakusha.Checked = isVal;
            this.form.cbxGenbaChiiki.Checked = isVal;
            this.form.cbxGenbaJusho.Checked = isVal;
            this.form.cbxHstGenbaGyoushu.Checked = isVal;
            this.form.cbxHstGenba.Checked = isVal;
            this.form.cbxHstGyoushaChiiki.Checked = isVal;
            this.form.cbxHstGyoushaJusho.Checked = isVal;
            this.form.cbxHstGyoushaGyoushu.Checked = isVal;
            this.form.cbxHstGyousha.Checked = isVal;
            this.form.cbxSyobunHinmoku.Checked = isVal;
        }

        /// <summary>
        /// 運搬受託者（一次）
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetUpJutakushaVal1(bool isVal)
        {
            this.form.cbxUnpanKyokaNo1_3.Checked = isVal;
            this.form.cbxUnpanKyokaNo1_2.Checked = isVal;
            this.form.cbxUnpanKyokaNo1_1.Checked = isVal;
            this.form.cbxUnpanChiiki1_3.Checked = isVal;
            this.form.cbxUnpanChiiki1_2.Checked = isVal;
            this.form.cbxUnpanChiiki1_1.Checked = isVal;
            this.form.cbxUnpanJusho1_3.Checked = isVal;
            this.form.cbxUnpanJusho1_2.Checked = isVal;
            this.form.cbxUnpanJusho1_1.Checked = isVal;
            this.form.cbxUnpanGyoushu1_3.Checked = isVal;
            this.form.cbxUnpanGyoushu1_2.Checked = isVal;
            this.form.cbxUnpanGyoushu1_1.Checked = isVal;
            this.form.cbxUnpanJutakusho1_3.Checked = isVal;
            this.form.cbxUnpanJutakusho1_2.Checked = isVal;
            this.form.cbxUnpanJutakusho1_1.Checked = isVal;
        }

        /// <summary>
        /// 積替保管（一次）
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetTkHokanVal1(bool isVal)
        {
            this.form.cbxTumikaechiiki1_2.Checked = isVal;
            this.form.cbxTumikaechiiki1_1.Checked = isVal;
            this.form.cbxTumikaeJusho1_2.Checked = isVal;
            this.form.cbxTumikaeJusho1_1.Checked = isVal;
            this.form.cbxTumikaeGyoushu1_2.Checked = isVal;
            this.form.cbxTumikaeGyoushu1_1.Checked = isVal;
            this.form.cbxTumikaeBasho1_2.Checked = isVal;
            this.form.cbxTumikaeBasho1_1.Checked = isVal;
        }

        /// <summary>
        /// 処分受託者
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetShobunJutakushaVal(bool isVal)
        {
            this.form.cbxShobunGenba.Checked = isVal;
            this.form.cbxShobunJutakushaGyoushu.Checked = isVal;
            this.form.cbxShobunJutakushaChiiki.Checked = isVal;
            this.form.cbxShobunJutakushaJusho.Checked = isVal;
            this.form.cbxShobunJutakusha.Checked = isVal;
            this.form.cbxShobunkyokaNo.Checked = isVal;
            this.form.cbxShobunHoushiki.Checked = isVal;
            this.form.cbxShobunTani.Checked = isVal;
            this.form.cbxShobunRyou.Checked = isVal;
            this.form.cbxShobunGenbaChiiki.Checked = isVal;
            this.form.cbxShobunGenbaGyoushu.Checked = isVal;
            this.form.cbxShobunGenbaJusho.Checked = isVal;
        }

        /// <summary>
        /// 一次二次共通
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetCommonVal(bool isVal)
        {
            if (this.isSyukei)
            {
                isVal = false;
                this.form.gbCommon.Enabled = false;
            }
            this.form.cbxKoufuDate.Checked = isVal;
            this.form.cbxKoufuNo.Checked = isVal;
        }

        /// <summary>
        /// 運搬受託者（二次）
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetUpJutakushaVal2(bool isVal)
        {
            this.form.cbxUnpanKyokaNo2_3.Checked = isVal;
            this.form.cbxUnpanKyokaNo2_2.Checked = isVal;
            this.form.cbxUnpanKyokaNo2_1.Checked = isVal;
            this.form.cbxUnpanChiiki2_3.Checked = isVal;
            this.form.cbxUnpanChiiki2_2.Checked = isVal;
            this.form.cbxUnpanChiiki2_1.Checked = isVal;
            this.form.cbxUnpanJusho2_3.Checked = isVal;
            this.form.cbxUnpanJusho2_2.Checked = isVal;
            this.form.cbxUnpanJusho2_1.Checked = isVal;
            this.form.cbxUnpanGyoushu2_3.Checked = isVal;
            this.form.cbxUnpanGyoushu2_2.Checked = isVal;
            this.form.cbxUnpanGyoushu2_1.Checked = isVal;
            this.form.cbxUnpanJutakusho2_3.Checked = isVal;
            this.form.cbxUnpanJutakusho2_2.Checked = isVal;
            this.form.cbxUnpanJutakusho2_1.Checked = isVal;
        }

        /// <summary>
        /// 積替保管（二次）
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetTkHokanVal2(bool isVal)
        {
            this.form.cbxTumikaechiiki2_2.Checked = isVal;
            this.form.cbxTumikaechiiki2_1.Checked = isVal;
            this.form.cbxTumikaeJusho2_2.Checked = isVal;
            this.form.cbxTumikaeJusho2_1.Checked = isVal;
            this.form.cbxTumikaeGyoushu2_2.Checked = isVal;
            this.form.cbxTumikaeGyoushu2_1.Checked = isVal;
            this.form.cbxTumikaeBasho2_2.Checked = isVal;
            this.form.cbxTumikaeBasho2_1.Checked = isVal;
        }

        /// <summary>
        /// 委託先
        /// </summary>
        /// <param name="isVal">isVal</param>
        internal void SetItakuVal(bool isVal)
        {
            this.form.cbxHikiWatashi.Checked = isVal;
            this.form.cbxItakuJutakushaGyoushu.Checked = isVal;
            this.form.cbxShobunHouHou.Checked = isVal;
            this.form.cbxItakuJutakushaChiiki.Checked = isVal;
            this.form.cbxItakuHinmoku.Checked = isVal;
            this.form.cbxItakushaChiiki.Checked = isVal;
            this.form.cbxItakuGenbaGyoushu.Checked = isVal;
            this.form.cbxItakuJutakushaJusho.Checked = isVal;
            this.form.cbxSaiItaku.Checked = isVal;
            this.form.cbxItakuGenbaChiiki.Checked = isVal;
            this.form.cbxItakusha.Checked = isVal;
            this.form.cbxItakuGenbaJusho.Checked = isVal;
            this.form.cbxItakukyokaNo.Checked = isVal;
        }

        #region CSVファイル出力処理

        /// <summary>
        /// CSVファイル出力処理
        /// </summary>
        internal void OutputCsvFile()
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
                this.ConvertCustomDataGridViewToCsv(this.GetDataTableForMultRow(), true, true, WINDOW_TITLEExt.ToTitleString(this.form.WindowId));
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// データをDataTable化
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTableForMultRow()
        {
            LogUtility.DebugMethodStart();

            // CSV用グリッドのバインド名からテーブル定義作成
            DataTable tbl = new DataTable();
            DataRow rowTemp = this.SearchResult.NewRow();
            this.SetColumnName(tbl);
            if (tbl.Columns.Count > 0)
            {
                if (isSyukei)
                {
                    List<bool> listFlg = new List<bool>();

                    string strSort = string.Empty;
                    foreach (DataColumn col in tbl.Columns)
                    {
                        if (col.ColumnName == "JYUTAKU_RYOU" || col.ColumnName == "SBN_RYOU" || col.ColumnName == "ITAKU_RYOU")
                        {
                            continue;
                        }
                        else
                        {
                            if (strSort == string.Empty)
                            {
                                strSort = col.ColumnName;
                            }
                            else
                            {
                                strSort = strSort + "," + col.ColumnName;
                            }

                        }
                    }

                    DataRow[] dr = this.SearchResult.Select("", strSort);

                    bool blnFlg = false;
                    decimal JYUTAKU_RYOU = 0;
                    decimal SBN_RYOU = 0;
                    decimal ITAKU_RYOU = 0;

                    // 処分受託量
                    bool hasJyutakuryou = this.form.cbxSyobunJutakusha.Checked;

                    // 受託処分量
                    bool hasSbnryou = this.form.cbxShobunRyou.Checked;
                    // 引渡量
                    bool hasItakuryou = this.form.cbxHikiWatashi.Checked;

                    // 値を取得
                    foreach (DataRow multiRow in dr)
                    {
                        DataRow row = tbl.NewRow();

                        foreach (DataColumn col in tbl.Columns)
                        {
                            row[col.ColumnName] = multiRow[col.ColumnName];
                            if (col.ColumnName == "JYUTAKU_RYOU" || col.ColumnName == "SBN_RYOU" || col.ColumnName == "ITAKU_RYOU")
                            {
                                continue;
                            }
                            else
                            {
                                if (Convert.ToString(rowTemp[col.ColumnName]) != Convert.ToString(multiRow[col.ColumnName]))
                                {
                                    blnFlg = true;
                                }
                            }
                        }
                        if (blnFlg)
                        {
                            tbl.Rows.Add(row);
                            // 処分受託量
                            if (hasJyutakuryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["JYUTAKU_RYOU"].ToString()))
                                {
                                    JYUTAKU_RYOU = Convert.ToDecimal(multiRow["JYUTAKU_RYOU"]);
                                }
                            }
                            // 受託処分量
                            if (hasSbnryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["SBN_RYOU"].ToString()))
                                {
                                    SBN_RYOU = Convert.ToDecimal(multiRow["SBN_RYOU"]);
                                }
                            }
                            // 引渡量
                            if (hasItakuryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["ITAKU_RYOU"].ToString()))
                                {
                                    ITAKU_RYOU = Convert.ToDecimal(multiRow["ITAKU_RYOU"]);
                                }
                            }
                            blnFlg = false;
                        }
                        else
                        {
                            // 処分受託量
                            if (hasJyutakuryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["JYUTAKU_RYOU"].ToString()))
                                {
                                    JYUTAKU_RYOU += Convert.ToDecimal(multiRow["JYUTAKU_RYOU"]);
                                }
                                tbl.Columns["JYUTAKU_RYOU"].ReadOnly = false;
                                tbl.Rows[tbl.Rows.Count - 1]["JYUTAKU_RYOU"] = JYUTAKU_RYOU;
                            }
                            // 処分受託量
                            if (hasSbnryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["SBN_RYOU"].ToString()))
                                {
                                    SBN_RYOU += Convert.ToDecimal(multiRow["SBN_RYOU"]);
                                }
                                tbl.Columns["SBN_RYOU"].ReadOnly = false;
                                tbl.Rows[tbl.Rows.Count - 1]["SBN_RYOU"] = SBN_RYOU;
                            }
                            // 処分受託量
                            if (hasItakuryou)
                            {
                                if (!string.IsNullOrEmpty(multiRow["ITAKU_RYOU"].ToString()))
                                {
                                    ITAKU_RYOU += Convert.ToDecimal(multiRow["ITAKU_RYOU"]);
                                }
                                tbl.Columns["ITAKU_RYOU"].ReadOnly = false;
                                tbl.Rows[tbl.Rows.Count - 1]["ITAKU_RYOU"] = ITAKU_RYOU;
                            }
                        }
                        rowTemp = multiRow;
                    }
                }
                else
                {

                    // 値を取得
                    foreach (DataRow multiRow in this.SearchResult.Rows)
                    {
                        DataRow row = tbl.NewRow();

                        foreach (DataColumn col in tbl.Columns)
                        {
                            row[col.ColumnName] = multiRow[col.ColumnName];
                        }

                        tbl.Rows.Add(row);
                    }

                }
            }

            // 数値項目にシステム設定のマニフェスト数量フォーマットを適用する。
            string manifestSuuryoFormatCD = this.mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();

            foreach (DataRow multiRow in tbl.Rows)
            {
                foreach (DataColumn col in tbl.Columns)
                {
                    if (col.ColumnName == "JYUTAKU_RYOU" || col.ColumnName == "SBN_RYOU" || col.ColumnName == "ITAKU_RYOU")
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

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        /// <summary>
        /// データをDataTable化
        /// </summary>
        /// <returns></returns>
        private void SetColumnName(DataTable tbl)
        {
            LogUtility.DebugMethodStart();

            #region 排出業者
            // 処分品目
            if (this.form.cbxSyobunHinmoku.Checked)
            {
                tbl.Columns.Add("HOUKOKUSHO_BUNRUI_CD");
                tbl.Columns.Add("HOUKOKUSHO_BUNRUI_NAME");
                tbl.Columns.Add("SEKIMEN_KBN");
                tbl.Columns.Add("TOKUTEI_YUUGAI_KBN");
            }

            // 処分受託量
            if (this.form.cbxSyobunJutakusha.Checked)
            {
                tbl.Columns.Add("JYUTAKU_RYOU");
            }

            // 単位
            if (this.form.cbxHstTani.Checked)
            {
                tbl.Columns.Add("UNIT_NAME");
            }

            // 排出事業者
            if (this.form.cbxHstGyousha.Checked)
            {
                tbl.Columns.Add("HST_GYOUSHA_CD");
                tbl.Columns.Add("HST_GYOUSHA_NAME");
            }

            // 排出事業者住所
            if (this.form.cbxHstGyoushaJusho.Checked)
            {
                tbl.Columns.Add("HST_GYOUSHA_ADDRESS");
            }

            // 排出事業者地域
            if (this.form.cbxHstGyoushaChiiki.Checked)
            {
                tbl.Columns.Add("CHIIKI_CD2");
                tbl.Columns.Add("HST_GYOUSHA_CHIIKI_NAME");
            }

            // 排出事業者業種
            if (this.form.cbxHstGyoushaGyoushu.Checked)
            {
                tbl.Columns.Add("HST_GYOUSHA_GYOUSHU_CD");
                tbl.Columns.Add("HST_GYOUSHU_NAME");
            }

            // 排出事業場
            if (this.form.cbxHstGenba.Checked)
            {
                tbl.Columns.Add("HST_GENBA_CD");
                tbl.Columns.Add("HST_GENBA_NAME");
            }

            // 排出事業場住所
            if (this.form.cbxGenbaJusho.Checked)
            {
                tbl.Columns.Add("HST_GENBA_ADDRESS");
            }

            // 排出事業場地域
            if (this.form.cbxGenbaChiiki.Checked)
            {
                tbl.Columns.Add("HST_GENBA_CHIIKI_CD");
                tbl.Columns.Add("HST_GENBA_CHIIKI_NAME");
            }

            // 排出事業場業種
            if (this.form.cbxHstGenbaGyoushu.Checked)
            {
                tbl.Columns.Add("HST_GENBA_GYOUSHU_CD");
                tbl.Columns.Add("HST_GENBA_GYOUSHU_NAME");
            }
            #endregion

            #region 運搬受託者（一次）
            // 運搬受託者(1)
            if (this.form.cbxUnpanJutakusho1_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CD1");
                tbl.Columns.Add("UPN_GYOUSHA_NAME1");
            }

            // 運搬受託者住所(1)
            if (this.form.cbxUnpanJusho1_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS1");
            }

            // 運搬受託者地域(1)
            if (this.form.cbxUnpanChiiki1_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD1");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME1");
            }

            // 運搬受託者業種(1)
            if (this.form.cbxUnpanJusho1_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD1");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME1");
            }

            // 運搬受託者許可番号(1)
            if (this.form.cbxUnpanKyokaNo1_1.Checked)
            {
                tbl.Columns.Add("KYOKA_NO1");
            }

            // 運搬受託者(2)
            if (this.form.cbxUnpanJutakusho1_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CD2");
                tbl.Columns.Add("UPN_GYOUSHA_NAME2");
            }

            // 運搬受託者許可番号(2)
            if (this.form.cbxUnpanJusho1_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS2");
            }

            // 運搬受託者地域(2)
            if (this.form.cbxUnpanChiiki1_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD2");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME2");
            }

            // 運搬受託者業種(2)
            if (this.form.cbxUnpanChiiki1_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD2");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME2");
            }

            // 運搬受託者許可番号(2)
            if (this.form.cbxUnpanChiiki1_2.Checked)
            {
                tbl.Columns.Add("KYOKA_NO2");
            }

            // 運搬受託者(3)
            if (this.form.cbxUnpanJutakusho1_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CD3");
                tbl.Columns.Add("UPN_GYOUSHA_NAME3");
            }

            // 運搬受託者住所(3)
            if (this.form.cbxUnpanJusho1_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS3");
            }

            // 運搬受託者地域(3)
            if (this.form.cbxUnpanChiiki1_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD3");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME3");
            }

            // 運搬受託者業種(3)
            if (this.form.cbxUnpanGyoushu1_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD3");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME3");
            }

            // 運搬受託者許可番号(3)
            if (this.form.cbxUnpanKyokaNo1_3.Checked)
            {
                tbl.Columns.Add("KYOKA_NO3");
            }
            #endregion

            #region 積替保管（一次）

            // 積替保管場所(1)
            if (this.form.cbxTumikaeBasho1_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHA_CD1");
                tbl.Columns.Add("UPN_SAKI_GENBA_CD1");
                tbl.Columns.Add("UPN_SAKI_GENBA_NAME1");
            }

            // 積替保管場所住所(1)
            if (this.form.cbxTumikaeJusho1_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GENBA_ADDRESS1");

            }

            // 積替保管場所地域(1)
            if (this.form.cbxTumikaechiiki1_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_CHIIKI_CD1");
                tbl.Columns.Add("UPN_SAKI_CHIIKI_NAME1");
            }

            // 積替保管場所業種(1)
            if (this.form.cbxTumikaeGyoushu1_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_CD1");
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_NAME1");
            }

            // 積替保管場所(2)
            if (this.form.cbxTumikaeBasho1_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHA_CD2");
                tbl.Columns.Add("UPN_SAKI_GENBA_CD2");
                tbl.Columns.Add("UPN_SAKI_GENBA_NAME2");
            }

            // 積替保管場所住所(2)
            if (this.form.cbxTumikaeJusho1_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GENBA_ADDRESS2");
            }

            // 積替保管場所地域(2)
            if (this.form.cbxTumikaechiiki1_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_CHIIKI_CD2");
                tbl.Columns.Add("UPN_SAKI_CHIIKI_NAME2");
            }

            // 積替保管場所業種(2)
            if (this.form.cbxTumikaeGyoushu1_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_CD2");
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_NAME2");
            }
            #endregion

            #region 処分受託者
            // 処分許可番号
            if (this.form.cbxShobunkyokaNo.Checked)
            {
                tbl.Columns.Add("SBN_KYOKA_NO");
            }

            // 処分受託者
            if (this.form.cbxShobunJutakusha.Checked)
            {
                tbl.Columns.Add("SBN_GYOUSHA_CD");
                tbl.Columns.Add("SBN_GYOUSHA_NAME");
            }

            // 処分受託者住所
            if (this.form.cbxShobunJutakushaJusho.Checked)
            {
                tbl.Columns.Add("SBN_GYOUSHA_ADDRESS");
            }

            // 処分受託者地域
            if (this.form.cbxShobunJutakushaChiiki.Checked)
            {
                tbl.Columns.Add("SBN_CHIIKI_CD");
                tbl.Columns.Add("SBN_CHIIKI_NAME");
            }

            // 処分受託者業種
            if (this.form.cbxShobunJutakushaGyoushu.Checked)
            {
                tbl.Columns.Add("SBN_GYOUSHU_CD");
                tbl.Columns.Add("SBN_GYOUSHU_NAME");
            }

            // 処分事業場
            if (this.form.cbxShobunGenba.Checked)
            {
                tbl.Columns.Add("SBN_GENBA_CD");
                tbl.Columns.Add("SBN_GENBA_NAME");
            }

            // 処分事業場住所
            if (this.form.cbxShobunGenbaJusho.Checked)
            {
                tbl.Columns.Add("SBN_GENBA_ADDRESS");
            }

            // 処分事業場地域
            if (this.form.cbxShobunGenbaChiiki.Checked)
            {
                tbl.Columns.Add("SBN_GENBA_CHIIKI_CD");
                tbl.Columns.Add("SBN_GENBA_CHIIKI_NAME");
            }

            // 処分事業場業種
            if (this.form.cbxShobunGenbaGyoushu.Checked)
            {
                tbl.Columns.Add("SBN_GENBA_GYOUSHU_CD");
                tbl.Columns.Add("SBN_GENBA_GYOUSHU_NAME");
            }

            // 処分量
            if (this.form.cbxShobunRyou.Checked)
            {
                tbl.Columns.Add("SBN_RYOU");
            }

            // 単位
            if (this.form.cbxShobunTani.Checked)
            {
                tbl.Columns.Add("UNIT_NAME2");
            }

            // 処分方法
            if (this.form.cbxShobunHoushiki.Checked)
            {
                tbl.Columns.Add("SBN_HOUHOU_CD");
                tbl.Columns.Add("SBN_HOUHOU_NAME");
                tbl.Columns.Add("SBN_MOKUTEKI_CD");
                tbl.Columns.Add("SBN_MOKUTEKI_NAME");
                tbl.Columns.Add("HOUKOKU_SHISETSU_CD");
                tbl.Columns.Add("HOUKOKU_SHISETSU_NAME");
            }

            #endregion

            #region 運搬受託者（二次）
            // 運搬受託者(1)
            if (this.form.cbxUnpanJutakusho2_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_NEXT_CD1");
                tbl.Columns.Add("UPN_GYOUSHA_NAME_NEXT1");
            }
            // 運搬受託者住所(1)
            if (this.form.cbxUnpanJusho2_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS_NEXT1");
            }

            // 運搬受託者地域(1)
            if (this.form.cbxUnpanChiiki2_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD_NEXT1");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME_NEXT1");
            }

            // 運搬受託者業種(1)
            if (this.form.cbxUnpanGyoushu2_1.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD_NEXT1");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME_NEXT1");
            }

            // 運搬受託者許可番号(1)
            if (this.form.cbxUnpanKyokaNo2_1.Checked)
            {
                tbl.Columns.Add("KYOKA_NO_NEXT1");
            }

            // 運搬受託者(2)
            if (this.form.cbxUnpanJutakusho2_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_NEXT_CD2");
                tbl.Columns.Add("UPN_GYOUSHA_NAME_NEXT2");
            }

            // 運搬受託者住所(2)
            if (this.form.cbxUnpanJusho2_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS_NEXT2");
            }

            // 運搬受託者地域(2)
            if (this.form.cbxUnpanChiiki2_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD_NEXT2");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME_NEXT2");
            }

            // 運搬受託者業種(2)
            if (this.form.cbxUnpanGyoushu2_2.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD_NEXT2");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME_NEXT2");
            }

            // 運搬受託者許可番号(2)
            if (this.form.cbxUnpanKyokaNo2_2.Checked)
            {
                tbl.Columns.Add("KYOKA_NO_NEXT2");
            }

            // 運搬受託者(3)
            if (this.form.cbxUnpanJutakusho2_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_NEXT_CD3");
                tbl.Columns.Add("UPN_GYOUSHA_NAME_NEXT3");
            }

            // 運搬受託者住所(3)
            if (this.form.cbxUnpanJusho2_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_ADDRESS_NEXT3");
            }

            // 運搬受託者地域(3)
            if (this.form.cbxUnpanChiiki2_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_CD_NEXT3");
                tbl.Columns.Add("UPN_GYOUSHA_CHIIKI_NAME_NEXT3");
            }

            // 運搬受託者業種(3)
            if (this.form.cbxUnpanGyoushu2_3.Checked)
            {
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_CD_NEXT3");
                tbl.Columns.Add("UPN_GYOUSHA_GYOUSHU_NAME_NEXT3");
            }

            // 運搬受託者許可番号(3)
            if (this.form.cbxUnpanKyokaNo2_3.Checked)
            {
                tbl.Columns.Add("KYOKA_NO_NEXT3");
            }
            #endregion

            #region 積替保管（二次）
            // 積替保管場所(1)
            if (this.form.cbxTumikaeBasho2_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHA_CD_NEXT1");
                tbl.Columns.Add("UPN_SAKI_GENBA_CD_NEXT1");
                tbl.Columns.Add("UPN_SAKI_GENBA_NAME_NEXT1");
            }

            // 積替保管場所住所(1)
            if (this.form.cbxTumikaeJusho2_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GENBA_ADDRESS_NEXT1");
            }

            // 積替保管場所地域(1)
            if (this.form.cbxTumikaechiiki2_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_CHIIKI_CD_NEXT1");
                tbl.Columns.Add("UPN_SAKI_CHIIKI_NAME_NEXT1");
            }

            // 積替保管場所業種(1)
            if (this.form.cbxTumikaeGyoushu2_1.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_CD_NEXT1");
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_NAME_NEXT1");
            }

            // 積替保管場所(2)
            if (this.form.cbxTumikaeBasho2_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHA_CD_NEXT2");
                tbl.Columns.Add("UPN_SAKI_GENBA_CD_NEXT2");
                tbl.Columns.Add("UPN_SAKI_GENBA_NAME_NEXT2");
            }

            // 積替保管場所住所(2)
            if (this.form.cbxTumikaeJusho2_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GENBA_ADDRESS_NEXT2");
            }

            // 積替保管場所地域(2)
            if (this.form.cbxTumikaechiiki2_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_CHIIKI_CD_NEXT2");
                tbl.Columns.Add("UPN_SAKI_CHIIKI_NAME_NEXT2");
            }

            // 積替保管場所業種(2)
            if (this.form.cbxTumikaeGyoushu2_2.Checked)
            {
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_CD_NEXT2");
                tbl.Columns.Add("UPN_SAKI_GYOUSHU_NAME_NEXT2");
            }
            #endregion

            #region 委託先

            // 委託先許可番号
            if (this.form.cbxItakukyokaNo.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_KYOKA_NO");
            }

            // 処分委託者
            if (this.form.cbxItakusha.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_CD");
                tbl.Columns.Add("ITAKUSAKI_GYOUSHA_NAME");
            }

            // 処分委託者住所
            if (this.form.cbxItakuJutakushaJusho.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GYOUSHA_ADDRESS");
            }

            // 処分委託者地域
            if (this.form.cbxItakushaChiiki.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_CHIIKI_CD");
                tbl.Columns.Add("ITAKUSAKI_CHIIKI_NAME");
            }

            // 処分委託者業種
            if (this.form.cbxItakuJutakushaChiiki.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GYOUSHU_CD");
                tbl.Columns.Add("ITAKUSAKI_GYOUSHU_NAME");
            }

            // 処分委託事業場
            if (this.form.cbxItakuJutakushaGyoushu.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GENBA_CD");
                tbl.Columns.Add("ITAKUSAKI_GENBA_NAME");
            }

            // 処分委託事業場住所
            if (this.form.cbxItakuGenbaJusho.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GENBA_ADDRESS");
            }

            // 処分委託事業場地域
            if (this.form.cbxItakuGenbaChiiki.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GENBA_CHIIKI_CD");
                tbl.Columns.Add("ITAKUSAKI_GENBA_CHIIKI_NAME");
            }

            // 処分委託事業場業種
            if (this.form.cbxItakuGenbaGyoushu.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_GENBA_GYOUSHU_CD");
                tbl.Columns.Add("ITAKUSAKI_GENBA_GYOUSHU_NAME");
            }

            // 委託品目
            if (this.form.cbxItakuHinmoku.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_BUNRUI_CD");
                tbl.Columns.Add("ITAKUSAKI_BUNRUI_NAME");
            }

            // 処分方法
            if (this.form.cbxShobunHouHou.Checked)
            {
                tbl.Columns.Add("ITAKUSAKI_HOUHOU_CD");
                tbl.Columns.Add("ITAKUSAKI_HOUHOU_NAME");
            }

            // 引渡量
            if (this.form.cbxHikiWatashi.Checked)
            {

                tbl.Columns.Add("ITAKU_RYOU");
                tbl.Columns.Add("UNIT_NAME3");
            }

            // 再委託残渣委託
            if (this.form.cbxSaiItaku.Checked)
            {
                tbl.Columns.Add("SAI_ITAKU");
            }
            #endregion

            #region 一次・二次共通

            // 交付番号
            if (this.form.cbxKoufuNo.Checked)
            {
                tbl.Columns.Add("MANIFEST_ID");
                tbl.Columns.Add("MANIFEST_ID_NEXT");
            }

            // 交付年月日
            if (this.form.cbxKoufuDate.Checked)
            {
                tbl.Columns.Add("KOUFU_DATE");
                tbl.Columns.Add("KOUFU_DATE_NEXT");
            }
            #endregion

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZenOn_Click(object sender, EventArgs e)
        {
            this.SetHstJigyoushaVal(true);
            this.SetUpJutakushaVal1(true);
            this.SetTkHokanVal1(true);
            this.SetShobunJutakushaVal(true);
            this.SetCommonVal(true);
            this.SetUpJutakushaVal2(true);
            this.SetTkHokanVal2(true);
            this.SetItakuVal(true);
        }

        /// <summary>
        /// 全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZenOff_Click(object sender, EventArgs e)
        {
            this.SetHstJigyoushaVal(false);
            this.SetUpJutakushaVal1(false);
            this.SetTkHokanVal1(false);
            this.SetShobunJutakushaVal(false);
            this.SetCommonVal(false);
            this.SetUpJutakushaVal2(false);
            this.SetTkHokanVal2(false);
            this.SetItakuVal(false);
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public int Search()
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

        /// <summary>
        /// データグリッドのデータをCSVに出力します
        /// </summary>
        /// <param name="inDgv">出力対象のデータグリッド</param>
        /// <param name="isHeaderOutPut">Trueの場合、ヘッダを出力</param>
        /// <param name="isLastRowOutPut">Trueの場合、最終行を出力</param>
        /// <param name="defaultFilename">ファイル名（日時と拡張子はメソッド内で付加）</param>
        /// <param name="form">呼び出し元のフォーム</param>
        public void ConvertCustomDataGridViewToCsv(DataTable inDgv, Boolean isHeaderOutPut, Boolean isLastRowOutPut, string defaultFilename)
        {
            try
            {
                r_framework.Utility.LogUtility.DebugMethodStart(inDgv, isHeaderOutPut, isLastRowOutPut, defaultFilename);

                if (inDgv.Rows.Count == 0)
                {
                    MessageBox.Show("対象データが無い為、出力を中止しました", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                    var title = "CSVファイルの出力場所を選択してください。";
                    var initialPath = @"C:\Temp";
                    var windowHandle = form.Handle;
                    var isFileSelect = false;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //var fileName = EscapeFileName(defaultFilename) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    var fileName = EscapeFileName(defaultFilename) + "_" + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                    browserForFolder = null;

                    if (false == String.IsNullOrEmpty(filePath))
                    {
                        //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                        //using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                        // ㎥の文字化け対策としてUTF8(BOM付)で出力する #3227
                        using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, new UTF8Encoding(true)))
                        {
                            string strLine = "";
                            // 表示順にカラム順を変更  //ヘッダのDisplayIndex順に出力する
                            //var colList = inDgv.Columns.Cast<DataColumn>().ToList().OrderBy(t => t.);

                            //カラムヘッダを書き込む
                            if (isHeaderOutPut)
                            {
                                int index = 0;
                                foreach (DataColumn col in inDgv.Columns)
                                {
                                    index++;
                                    //改行を削って出力(デフォルト)
                                    var h = col.ColumnName;

                                    h = this.ChangeColumnName(h);

                                    if (h == null) h = "";

                                    //ヘッダの位置合わせを、スペースや改行で調整している場合の対策
                                    h = System.Text.RegularExpressions.Regex.Replace(h, " *\r *", "\r"); //改行前後にあるスペースは全部消す
                                    h = System.Text.RegularExpressions.Regex.Replace(h, " *\n *", "\n"); //同上

                                    h = h.Replace("\r", "").Replace("\n", "").Trim(); //改行コードを消す、念のためスペースも消す

                                    strLine += h;

                                    if (index < inDgv.Columns.Count)
                                    {
                                        strLine += ",";
                                    }
                                }
                                strLine = strLine.Remove(strLine.Length - 1);
                                sw.WriteLine(strLine);
                                strLine = "";
                            }

                            //データ行数
                            int rowCnt = inDgv.Rows.Count;

                            //最後行を出力しない場合
                            if (!isLastRowOutPut)
                            {
                                rowCnt--;
                            }

                            //カラム内容を書き込む
                            foreach (DataRow row in inDgv.Rows)
                            {
                                strLine = "";

                                foreach (DataColumn col in inDgv.Columns) //ヘッダのDisplayIndex順に出力する
                                {
                                    //var c = row[col.ColumnName];
                                    object c;
                                    switch (col.ColumnName)
                                    {
                                        case "HST_GYOUSHA_ADDRESS":
                                        case "HST_GENBA_ADDRESS":
                                        case "UPN_GYOUSHA_ADDRESS1":
                                        case "UPN_GYOUSHA_ADDRESS2":
                                        case "UPN_GYOUSHA_ADDRESS3":
                                        case "UPN_SAKI_GENBA_ADDRESS1":
                                        case "UPN_SAKI_GENBA_ADDRESS2":
                                        case "SBN_GYOUSHA_ADDRESS":
                                        case "SBN_GENBA_ADDRESS":
                                        case "UPN_GYOUSHA_ADDRESS_NEXT1":
                                        case "UPN_GYOUSHA_ADDRESS_NEXT2":
                                        case "UPN_GYOUSHA_ADDRESS_NEXT3":
                                        case "UPN_SAKI_GENBA_ADDRESS_NEXT1":
                                        case "UPN_SAKI_GENBA_ADDRESS_NEXT2":
                                        case "ITAKUSAKI_GYOUSHA_ADDRESS":
                                        case "ITAKUSAKI_GENBA_ADDRESS":
                                            c = SetAddressString(row[col.ColumnName].ToString());
                                            break;
                                        default:
                                            c = row[col.ColumnName];
                                            break;
                                    }

                                    if (c == null)
                                        strLine += "";
                                    else if (row[col.ColumnName] == null)
                                    {
                                        strLine += "";
                                    }
                                    else
                                    {

                                        string m = Convert.ToString(c);

                                        // 日付の後ろに曜日"(ddd)"がある場合はCSV(Excel)では邪魔なので取ってしまう。
                                        if (!string.IsNullOrEmpty(m) && c.GetType().Equals(typeof(DateTime)))
                                        {
                                            int index = m.IndexOf('(');
                                            if (index >= 0)
                                                m = m.Remove(index);
                                        }

                                        m = CSVQuote(m);

                                        strLine += m;
                                    }
                                    strLine += ",";

                                }

                                if (strLine.Length > 0) strLine = strLine.Remove(strLine.Length - 1); //末尾のカンマを消す

                                sw.WriteLine(strLine);
                            }
                        }//ファイルを閉じる（メッセージは閉じてから。メッセージ表示中に開く等をされる可能性を考慮）
                        r_framework.Utility.LogUtility.Info("inDgv.Name：" + inDgv.TableName);

                        //ディレクトリ初期値の記録
                        CSVExport.InitialDirectory = filePath;

                        MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }//保存ダイアログキャンセル時は何もしない
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ファイル名に使用できない文字を使用できる文字に変換します
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>変換後のファイル名</returns>
        private static string EscapeFileName(string fileName)
        {
            string ret = fileName;

            ret = ret.Replace("/", "／");
            ret = ret.Replace("\\", "￥");
            ret = ret.Replace(":", "：");
            ret = ret.Replace("*", "＊");
            ret = ret.Replace("?", "？");
            ret = ret.Replace("\"", "”");
            ret = ret.Replace("<", "＜");
            ret = ret.Replace(">", "＞");
            ret = ret.Replace("|", "｜");
            ret = ret.Replace("#", "＃");
            ret = ret.Replace("{", "｛");
            ret = ret.Replace("}", "｝");
            ret = ret.Replace("%", "％");
            ret = ret.Replace("&", "＆");
            ret = ret.Replace("~", "～");
            ret = ret.Replace(".", "．");

            return ret;
        }

        /// <summary>
        /// 項目をチェックして、必要あればダブルクオートをつける。"は""に変換する。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string CSVQuote(string m)
        {
            if (string.IsNullOrEmpty(m))
                return "";
            else
            {

                //"で囲む必要があるか調べる
                if (m.IndexOf('"') > -1 ||
                    m.IndexOf(',') > -1 ||
                    m.IndexOf('\r') > -1 ||
                    m.IndexOf('\n') > -1 ||
                    m.StartsWith(" ") || m.StartsWith("\t") ||
                    m.EndsWith(" ") || m.EndsWith("\t"))
                {
                    if (m.IndexOf('"') > -1)
                    {
                        //"を""とする
                        m = m.Replace("\"", "\"\"");
                    }
                    m = "\"" + m + "\"";
                }

                return m;
            }
        }

        /// <summary>
        /// CSV出力時　ヘットを変更
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string ChangeColumnName(string columnName)
        {
            string mame = columnName;
            switch (columnName)
            {
                case "HOUKOKUSHO_BUNRUI_CD":
                    mame = "処分品目CD";
                    break;
                case "HOUKOKUSHO_BUNRUI_NAME":
                    mame = "処分品目";
                    break;
                case "SEKIMEN_KBN":
                    mame = "石綿";
                    break;
                case "TOKUTEI_YUUGAI_KBN":
                    mame = "特定有害";
                    break;
                case "JYUTAKU_RYOU":
                    mame = "処分受託量";
                    break;
                case "UNIT_NAME":
                    mame = "単位1";
                    break;
                case "HST_GYOUSHA_CD":
                    mame = "排出事業者CD";
                    break;
                case "HST_GYOUSHA_NAME":
                    mame = "排出事業者名";
                    break;
                case "HST_GYOUSHA_ADDRESS":
                    mame = "排出事業者住所";
                    break;
                case "CHIIKI_CD2":
                    mame = "排出事業者地域CD";
                    break;
                case "HST_GYOUSHA_CHIIKI_NAME":
                    mame = "排出事業者地域名";
                    break;
                case "HST_GYOUSHA_GYOUSHU_CD":
                    mame = "排出事業者業種CD";
                    break;
                case "HST_GYOUSHU_NAME":
                    mame = "排出事業者業種名";
                    break;
                case "HST_GENBA_CD":
                    mame = "排出事業場CD";
                    break;
                case "HST_GENBA_NAME":
                    mame = "排出事業場名";
                    break;
                case "HST_GENBA_ADDRESS":
                    mame = "排出事業場住所";
                    break;
                case "HST_GENBA_CHIIKI_CD":
                    mame = "排出事業場地域CD";
                    break;
                case "HST_GENBA_CHIIKI_NAME":
                    mame = "排出事業場地域名";
                    break;
                case "HST_GENBA_GYOUSHU_CD":
                    mame = "排出事業場業種CD";
                    break;
                case "HST_GENBA_GYOUSHU_NAME":
                    mame = "排出事業場業種名";
                    break;
                case "UPN_GYOUSHA_CD1":
                    mame = "運搬受託者CD1_1";
                    break;
                case "UPN_GYOUSHA_NAME1":
                    mame = "運搬受託者名1_1";
                    break;
                case "UPN_GYOUSHA_ADDRESS1":
                    mame = "運搬受託者住所1_1";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD1":
                    mame = "運搬受託者地域CD1_1";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME1":
                    mame = "運搬受託者地域名1_1";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD1":
                    mame = "運搬受託者業種CD1_1";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME1":
                    mame = "運搬受託者業種名1_1";
                    break;
                case "KYOKA_NO1":
                    mame = "運搬受託者許可番号1_1";
                    break;
                case "UPN_GYOUSHA_CD2":
                    mame = "運搬受託者CD1_2";
                    break;
                case "UPN_GYOUSHA_NAME2":
                    mame = "運搬受託者名1_2";
                    break;
                case "UPN_GYOUSHA_ADDRESS2":
                    mame = "運搬受託者住所1_2";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD2":
                    mame = "運搬受託者地域CD1_2";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME2":
                    mame = "運搬受託者地域名1_2";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD2":
                    mame = "運搬受託者業種CD1_2";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME2":
                    mame = "運搬受託者業種名1_2";
                    break;
                case "KYOKA_NO2":
                    mame = "運搬受託者許可番号1_2";
                    break;
                case "UPN_GYOUSHA_CD3":
                    mame = "運搬受託者CD1_3";
                    break;
                case "UPN_GYOUSHA_NAME3":
                    mame = "運搬受託者名1_3";
                    break;
                case "UPN_GYOUSHA_ADDRESS3":
                    mame = "運搬受託者名1_3";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD3":
                    mame = "運搬受託者地域CD1_3";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME3":
                    mame = "運搬受託者地域名1_3";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD3":
                    mame = "運搬受託者業種CD1_3";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME3":
                    mame = "運搬受託者業種名1_3";
                    break;
                case "KYOKA_NO3":
                    mame = "運搬受託者許可番号1_3";
                    break;
                case "UPN_SAKI_GYOUSHA_CD1":
                    mame = "積替保管業者CD1_1";
                    break;
                case "UPN_SAKI_GENBA_CD1":
                    mame = "積替保管場所CD1_1";
                    break;
                case "UPN_SAKI_GENBA_NAME1":
                    mame = "積替保管場所名1_1";
                    break;
                case "UPN_SAKI_GENBA_ADDRESS1":
                    mame = "積替保管場所住所1_1";
                    break;
                case "UPN_SAKI_CHIIKI_CD1":
                    mame = "積替保管場所地域CD1_1";
                    break;
                case "UPN_SAKI_CHIIKI_NAME1":
                    mame = "積替保管場所地域名1_1";
                    break;
                case "UPN_SAKI_GYOUSHU_CD1":
                    mame = "積替保管場所業種CD1_1";
                    break;
                case "UPN_SAKI_GYOUSHU_NAME1":
                    mame = "積替保管場所業種名1_1";
                    break;
                case "UPN_SAKI_GYOUSHA_CD2":
                    mame = "積替保管業者CD1_2";
                    break;
                case "UPN_SAKI_GENBA_CD2":
                    mame = "積替保管場所CD1_2";
                    break;
                case "UPN_SAKI_GENBA_NAME2":
                    mame = "積替保管場所名1_2";
                    break;
                case "UPN_SAKI_GENBA_ADDRESS2":
                    mame = "積替保管場所住所1_2";
                    break;
                case "UPN_SAKI_CHIIKI_CD2":
                    mame = "積替保管場所地域CD1_2";
                    break;
                case "UPN_SAKI_CHIIKI_NAME2":
                    mame = "積替保管場所地域名1_2";
                    break;
                case "UPN_SAKI_GYOUSHU_CD2":
                    mame = "積替保管場所地域名1_2";
                    break;
                case "UPN_SAKI_GYOUSHU_NAME2":
                    mame = "積替保管場所業種名1_2";
                    break;
                case "SBN_KYOKA_NO":
                    mame = "処分受託許可番号";
                    break;
                case "SBN_GYOUSHA_CD":
                    mame = "処分受託者CD";
                    break;
                case "SBN_GYOUSHA_NAME":
                    mame = "処分受託者名";
                    break;
                case "SBN_GYOUSHA_ADDRESS":
                    mame = "処分受託者住所";
                    break;
                case "SBN_CHIIKI_CD":
                    mame = "処分受託者地域CD";
                    break;
                case "SBN_CHIIKI_NAME":
                    mame = "処分受託者地域名";
                    break;
                case "SBN_GYOUSHU_CD":
                    mame = "処分受託者業種CD";
                    break;
                case "SBN_GYOUSHU_NAME":
                    mame = "処分受託者業種名";
                    break;
                case "SBN_GENBA_CD":
                    mame = "処分事業場CD";
                    break;
                case "SBN_GENBA_NAME":
                    mame = "処分事業場名";
                    break;
                case "SBN_GENBA_ADDRESS":
                    mame = "処分事業場住所";
                    break;
                case "SBN_GENBA_CHIIKI_CD":
                    mame = "処分事業場地域CD";
                    break;
                case "SBN_GENBA_CHIIKI_NAME":
                    mame = "処分事業場地域名";
                    break;
                case "SBN_GENBA_GYOUSHU_CD":
                    mame = "処分事業場業種CD";
                    break;
                case "SBN_GENBA_GYOUSHU_NAME":
                    mame = "処分事業場業種名";
                    break;
                case "SBN_RYOU":
                    mame = "受託処分量";
                    break;
                case "UNIT_NAME2":
                    mame = "単位2";
                    break;
                case "SBN_HOUHOU_CD":
                    mame = "受託処分方法CD";
                    break;
                case "SBN_HOUHOU_NAME":
                    mame = "受託処分方法";
                    break;
                case "SBN_MOKUTEKI_CD":
                    mame = "処分目的CD";
                    break;
                case "SBN_MOKUTEKI_NAME":
                    mame = "処分目的";
                    break;
                case "HOUKOKU_SHISETSU_CD":
                    mame = "処分品目CD";
                    break;
                case "処理施設CD":
                    mame = "処分品目CD";
                    break;
                case "HOUKOKU_SHISETSU_NAME":
                    mame = "処理施設名";
                    break;
                case "UPN_GYOUSHA_NEXT_CD1":
                    mame = "運搬受託者CD2_1";
                    break;
                case "UPN_GYOUSHA_NAME_NEXT1":
                    mame = "運搬受託者名2_1";
                    break;
                case "UPN_GYOUSHA_ADDRESS_NEXT1":
                    mame = "運搬受託者住所2_1";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD_NEXT1":
                    mame = "運搬受託者地域CD2_1";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME_NEXT1":
                    mame = "運搬受託者地域名2_1";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD_NEXT1":
                    mame = "運搬受託者業種CD2_1";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME_NEXT1":
                    mame = "運搬受託者業種名2_1";
                    break;
                case "KYOKA_NO_NEXT1":
                    mame = "運搬受託者許可番号2_1";
                    break;
                case "UPN_GYOUSHA_NEXT_CD2":
                    mame = "運搬受託者CD2_2";
                    break;
                case "UPN_GYOUSHA_NAME_NEXT2":
                    mame = "運搬受託者名2_2";
                    break;
                case "UPN_GYOUSHA_ADDRESS_NEXT2":
                    mame = "運搬受託者住所2_2";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD_NEXT2":
                    mame = "運搬受託者地域CD2_2";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME_NEXT2":
                    mame = "運搬受託者地域名2_2";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD_NEXT2":
                    mame = "運搬受託者業種CD2_2";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME_NEXT2":
                    mame = "運搬受託者業種名2_2";
                    break;
                case "KYOKA_NO_NEXT2":
                    mame = "運搬受託者許可番号2_2";
                    break;
                case "UPN_GYOUSHA_NEXT_CD3":
                    mame = "運搬受託者CD2_3";
                    break;
                case "UPN_GYOUSHA_NAME_NEXT3":
                    mame = "運搬受託者名2_3";
                    break;
                case "UPN_GYOUSHA_ADDRESS_NEXT3":
                    mame = "運搬受託者住所2_3";
                    break;
                case "UPN_GYOUSHA_CHIIKI_CD_NEXT3":
                    mame = "運搬受託者地域CD2_3";
                    break;
                case "UPN_GYOUSHA_CHIIKI_NAME_NEXT3":
                    mame = "運搬受託者地域名2_3";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_CD_NEXT3":
                    mame = "運搬受託者業種CD2_3";
                    break;
                case "UPN_GYOUSHA_GYOUSHU_NAME_NEXT3":
                    mame = "運搬受託者業種名2_3";
                    break;
                case "KYOKA_NO_NEXT3":
                    mame = "運搬受託者許可番号2_3";
                    break;
                case "UPN_SAKI_GYOUSHA_CD_NEXT1":
                    mame = "積替保管業者CD2_1";
                    break;
                case "UPN_SAKI_GENBA_CD_NEXT1":
                    mame = "積替保管場所CD2_1";
                    break;
                case "UPN_SAKI_GENBA_NAME_NEXT1":
                    mame = "積替保管場所名2_1";
                    break;
                case "UPN_SAKI_GENBA_ADDRESS_NEXT1":
                    mame = "積替保管場所住所2_1";
                    break;
                case "UPN_SAKI_CHIIKI_CD_NEXT1":
                    mame = "積替保管場所地域CD2_1";
                    break;
                case "UPN_SAKI_CHIIKI_NAME_NEXT1":
                    mame = "積替保管場所地域名2_1";
                    break;
                case "UPN_SAKI_GYOUSHU_CD_NEXT1":
                    mame = "積替保管場所業種CD2_1";
                    break;
                case "UPN_SAKI_GYOUSHU_NAME_NEXT1":
                    mame = "積替保管場所業種名2_1";
                    break;
                case "UPN_SAKI_GYOUSHA_CD_NEXT2":
                    mame = "積替保管業者CD2_2";
                    break;
                case "UPN_SAKI_GENBA_CD_NEXT2":
                    mame = "積替保管場所CD2_2";
                    break;
                case "UPN_SAKI_GENBA_NAME_NEXT2":
                    mame = "積替保管場所名2_2";
                    break;
                case "UPN_SAKI_GENBA_ADDRESS_NEXT2":
                    mame = "積替保管場所住所2_2";
                    break;
                case "UPN_SAKI_CHIIKI_CD_NEXT2":
                    mame = "積替保管場所地域CD2_2";
                    break;
                case "UPN_SAKI_CHIIKI_NAME_NEXT2":
                    mame = "積替保管場所地域名2_2";
                    break;
                case "UPN_SAKI_GYOUSHU_CD_NEXT2":
                    mame = "積替保管場所業種CD2_2";
                    break;
                case "UPN_SAKI_GYOUSHU_NAME_NEXT2":
                    mame = "積替保管場所業種名2_2";
                    break;
                case "ITAKUSAKI_KYOKA_NO":
                    mame = "委託先許可番号";
                    break;
                case "ITAKUSAKI_CD":
                    mame = "処分委託者CD";
                    break;
                case "ITAKUSAKI_GYOUSHA_NAME":
                    mame = "処分委託者名";
                    break;
                case "ITAKUSAKI_GYOUSHA_ADDRESS":
                    mame = "処分委託者住所";
                    break;
                case "ITAKUSAKI_CHIIKI_CD":
                    mame = "処分委託者地域CD";
                    break;
                case "ITAKUSAKI_CHIIKI_NAME":
                    mame = "処分委託者地域名";
                    break;
                case "ITAKUSAKI_GYOUSHU_CD":
                    mame = "処分委託者業種CD";
                    break;
                case "ITAKUSAKI_GYOUSHU_NAME":
                    mame = "処分委託者業種名";
                    break;
                case "ITAKUSAKI_GENBA_CD":
                    mame = "処分委託事業場CD";
                    break;
                case "ITAKUSAKI_GENBA_NAME":
                    mame = "処分委託事業場名";
                    break;
                case "ITAKUSAKI_GENBA_ADDRESS":
                    mame = "処分委託事業場住所";
                    break;
                case "ITAKUSAKI_GENBA_CHIIKI_CD":
                    mame = "処分委託事業場地域CD";
                    break;
                case "ITAKUSAKI_GENBA_CHIIKI_NAME":
                    mame = "処分委託事業場地域名";
                    break;
                case "ITAKUSAKI_GENBA_GYOUSHU_CD":
                    mame = "処分委託事業場業種CD";
                    break;
                case "ITAKUSAKI_GENBA_GYOUSHU_NAME":
                    mame = "処分委託事業場業種名";
                    break;
                case "ITAKUSAKI_BUNRUI_CD":
                    mame = "委託品目CD";
                    break;
                case "ITAKUSAKI_BUNRUI_NAME":
                    mame = "委託品目";
                    break;
                case "ITAKUSAKI_HOUHOU_CD":
                    mame = "委託処分方法CD";
                    break;
                case "ITAKUSAKI_HOUHOU_NAME":
                    mame = "委託処分方法";
                    break;
                case "ITAKU_RYOU":
                    mame = "引渡量";
                    break;
                case "UNIT_NAME3":
                    mame = "単位3";
                    break;
                case "SAI_ITAKU":
                    mame = "再委託残渣委託";
                    break;
                case "MANIFEST_ID":
                    mame = "交付番号1";
                    break;
                case "MANIFEST_ID_NEXT":
                    mame = "交付番号2";
                    break;
                case "KOUFU_DATE":
                    mame = "交付年月日1";
                    break;
                case "KOUFU_DATE_NEXT":
                    mame = "交付年月日2";
                    break;
            }
            return mame;
        }

        /// <summary>
        /// 住所設定
        /// </summary>
        /// <param name="address"></param>
        private string SetAddressString(string address)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(address))
            {
                if (this.tjHoukokuentry != null && this.tjHoukokuentry.ADDRESS_KBN == 3)
                {
                    result = Regex.Match(address, ".+?郡.+?町|.+?郡.+?村|.+?市|.+?区|.+?町|.+?村").Value;
                }
                else
                {
                    result = address;
                }
            }

            return result;
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
