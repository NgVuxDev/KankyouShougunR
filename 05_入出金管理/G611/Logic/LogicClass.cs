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
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon;
using r_framework.CustomControl;
using System.Drawing;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>Form</summary>
        private UIForm form;

        /// <summary>ベースフォーム</summary>
        private BusinessBaseForm baseForm;

        /// <summary>アクセッサー</summary>
        private DBAccessor accessor;

        //空行用の消込明細
        DataTable blankKeshikomiTable = new DataTable();

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>入金入力データ</summary>
        List<T_NYUUKIN_ENTRY> nyuukinEntryList = new List<T_NYUUKIN_ENTRY>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.accessor = new DBAccessor();
        }
        #endregion

        #region 初期化処理
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                this.baseForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G459", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 登録ボタン非活性
                    this.baseForm.bt_func9.Enabled = false;
                    // 明細ReadOnly
                    this.form.dgvNyukinDeleteMeisai.ReadOnly = true;
                }

                nyuukinEntryList = this.accessor.GetNyuukinEntryList(this.form.initDto.SumSystemId);

                /*
                 * 連携データ設定
                 */
                this.form.txtNyukinSakiName.Text = this.form.initDto.TorihikisakiName;
                this.form.txtKonkaiNyukinGaku.Text = this.form.initDto.KonkaiWarifurigaku;

                // 検索
                this.SearchKeshikomiInfoByNyuukinsakiCd();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 検索ボタン(F8)イベント
            //this.baseForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 登録ボタン(F9)イベント
            this.baseForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 閉じるボタン(F12)イベント
            this.baseForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
        }
        #endregion

        #region イベント

        #region 消込金額チェック
        //private const string NYUUKIN_ENTRY_NOT_REGIST_MEG = "この取引先は、入金されていないため消込できません。";
        private const string OVER_SEIKYUU_GAKU_MEG = "請求金額以下で入力してください。";
        //private const string OVER_NYUUKINGAKU_AMOUNT_TOTOAL_MEG = "この取引先の入金額を超える金額が入力されました。\n入金入力の明細行をご確認の上、入力してください。";

        /// <summary>
        /// 消込金額の不正な値かチェック
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns>true:不正値, false:正常値</returns>
        internal bool IsInvalidKeshikomiGaku(int rowIndex, int columnIndex)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool returnVal = false;

            try
            {
                var keshikomiGakuCell = this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells[columnIndex] as DgvCustomTextBoxCell;
                // 比較用の値
                //var limitKeshikomiGakuCell = this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["NYUUKIN_AMOUNT_TOTAL"] as DgvCustomTextBoxCell;
                var seikyuuKingakuCell = this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["SEIKYUUGAKU"] as DgvCustomTextBoxCell;
                //var torihikisakiCd = Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["TORIHIKISAKI_CD"].Value);
                var seikyuuNumber = this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["SEIKYUU_NUMBER"] as DgvCustomTextBoxCell;

                decimal keshikomiGakuTotal = 0;
                foreach (DataGridViewRow row in this.form.dgvNyukinDeleteMeisai.Rows)
                {
                    //var tempTorihikisakiCd = Convert.ToString(row.Cells["TORIHIKISAKI_CD"].Value);
                    var tempSeikyuuNumber = Convert.ToString(row.Cells["SEIKYUU_NUMBER"].Value);
                    if (!tempSeikyuuNumber.Equals(Convert.ToString(seikyuuNumber.Value)))
                    {
                        decimal tempKeshikomiGaku = 0;
                        decimal.TryParse(Convert.ToString(row.Cells["KeshikomiGaku"].Value), out tempKeshikomiGaku);
                        keshikomiGakuTotal += tempKeshikomiGaku;
                    }
                }

                // null空チェック
                if (keshikomiGakuCell.Value == null
                    || string.IsNullOrEmpty(Convert.ToString(keshikomiGakuCell.Value)))
                {
                    return returnVal;
                }

                // 今回入力された消込金額
                decimal keshikomiGaku = 0;
                // 総消込金額
                decimal keshikomiGakuTotal2 = 0;

                string seikyuuNum = Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["SEIKYUU_NUMBER"].Value);
                string kagamiNum = Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["KAGAMI_NUMBER"].Value);
                for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
                {
                    decimal kingaku = 0;
                    if (seikyuuNum == Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value)
                        && kagamiNum == Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value))
                    {
                        if (decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KeshikomiGaku"].Value), out kingaku))
                        {
                            keshikomiGakuTotal2 += kingaku;
                        }
                    }
                }

                if (decimal.TryParse(Convert.ToString(keshikomiGakuCell.Value), out keshikomiGaku))
                {
                    decimal seikyuuGaku = 0;

                    if (decimal.TryParse(Convert.ToString(seikyuuKingakuCell.Value), out seikyuuGaku))
                    {
                        // 請求額を超える入力はエラー
                        //if (Math.Abs(keshikomiGaku) > Math.Abs(seikyuuGaku))
                        //{
                        //    msgLogic.MessageBoxShowError(OVER_SEIKYUU_GAKU_MEG);
                        //    returnVal = true;
                        //    return returnVal;
                        //}

                        if (seikyuuGaku > 0 && keshikomiGaku < 0)
                        {
                            msgLogic.MessageBoxShow("E230", "0以上の値");
                            returnVal = true;
                            return returnVal;
                        }

                        if (seikyuuGaku < 0 && keshikomiGaku > 0)
                        {
                            msgLogic.MessageBoxShow("E230", "0以下の値");
                            returnVal = true;
                            return returnVal;
                        }

                        //if ((seikyuuGaku > 0 && seikyuuGaku - keshikomiGakuTotal2 < 0) || (seikyuuGaku < 0 && seikyuuGaku - keshikomiGakuTotal2 > 0))
                        //{
                        //    msgLogic.MessageBoxShowError(OVER_SEIKYUU_GAKU_MEG);
                        //    returnVal = true;
                        //    return returnVal;
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsInvalidKeshikomiGaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                returnVal = true;
            }

            return returnVal;
        }
        #endregion

        #region 検索
        /// <summary>
        /// 消込明細検索処理
        /// </summary>
        /// <returns></returns>
        internal void SearchKeshikomiInfoByNyuukinsakiCd()
        {
            // 必須チェック
            if (string.IsNullOrEmpty(this.form.initDto.TorihikisakiCd)
                || string.IsNullOrEmpty(this.form.initDto.DenpyoDate))
            {
                return;
            }

            if (!string.IsNullOrEmpty(this.form.txtNyukinSakiName.Text))
            {

                ClearKesikomi();

                DateTime denpyoDate = this.baseForm.sysDate;
                if (!DateTime.TryParse(this.form.initDto.DenpyoDate, out denpyoDate))
                {
                    return;
                }

                //Dictionary<String, DataRow> dicHashKeshikomi = new Dictionary<string, DataRow>();
                // 画面に表示する入金消込テーブル
                //DataTable dtKeshikomi = new DataTable();
                // 入金一括システムIDから入金消込を取得
                //DataTable dtKeshikomiSumSystemId = accessor.GetKeshikomi(this.form.initDto.SumSystemId.ToString(), denpyoDate.Date.ToShortDateString());
                // 入金先CDから入金消込を取得
                //DataTable dtKeshikomiNyuukinsakiCd = accessor.GetKeshikomiByNyuukinsakiCd(this.form.initDto.NyuukinsakiCd.ToString(), denpyoDate.Date.ToShortDateString());

                //入金一括システムIDから取得した入金消込を取引先CDと請求番号をキーにハッシュテーブルを作成
                //foreach (DataRow row in dtKeshikomiSumSystemId.Rows)
                //{
                //    string strKey = row["TORIHIKISAKI_CD"].ToString() + ":" + row["SEIKYUU_NUMBER"].ToString();
                //    dicHashKeshikomi[strKey] = row;
                //}

                DataTable searchData = accessor.GetKeshikomi(this.form.initDto.TorihikisakiCd.ToString(), denpyoDate.Date.ToShortDateString(), this.form.initDto.NyuukinNumber);

                //画面に表示する入金消込テーブルを作成
                //dtKeshikomi = dtKeshikomiSumSystemId.Clone();
                //dtKeshikomi.Columns["SYSTEM_ID"].AllowDBNull = true;
                //dtKeshikomi.Columns["ROW_NUMBER"].ReadOnly = false;
                bool gyoushaFlag = true;
                bool genbaFlag = true;
                int index = 0;
                DateTime date = this.baseForm.sysDate;
                if (searchData != null && searchData.Rows.Count > 0)
                {
                    for (int i = 0; i < searchData.Rows.Count; i++)
                    {
                        DataRow result = searchData.Rows[i];
                        decimal kingaku = 0;
                        if (result["SEIKYUUGAKU"] != null)
                        {
                            decimal.TryParse(Convert.ToString(result["SEIKYUUGAKU"]), out kingaku);
                        }
                        if (kingaku == decimal.Zero)
                        {
                            continue;
                        }
                        this.form.dgvNyukinDeleteMeisai.Rows.Add(1);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(result["GYOUSHA_CD"])))
                        {
                            gyoushaFlag = false;
                        }
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(result["GENBA_CD"])))
                        {
                            genbaFlag = false;
                        }
                        DataGridViewRow row = this.form.dgvNyukinDeleteMeisai.Rows[index];
                        row.Cells["ROW_NUMBER"].Value = Convert.ToString(result["ROW_NUMBER"]);
                        row.Cells["GYOUSHA_CD"].Value = Convert.ToString(result["GYOUSHA_CD"]);
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = Convert.ToString(result["GYOUSHA_NAME_RYAKU"]);
                        row.Cells["GENBA_CD"].Value = Convert.ToString(result["GENBA_CD"]);
                        row.Cells["GENBA_NAME_RYAKU"].Value = Convert.ToString(result["GENBA_NAME_RYAKU"]);
                        row.Cells["SEIKYUU_DATE"].Value = Convert.ToString(result["SEIKYUU_DATE"]);
                        if (DateTime.TryParse(Convert.ToString(row.Cells["SEIKYUU_DATE"].Value), out date))
                        {
                            switch (date.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(日)";
                                    break;
                                case DayOfWeek.Monday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(月)";
                                    break;
                                case DayOfWeek.Tuesday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(火)";
                                    break;
                                case DayOfWeek.Wednesday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(水)";
                                    break;
                                case DayOfWeek.Thursday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(木)";
                                    break;
                                case DayOfWeek.Friday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(金)";
                                    break;
                                case DayOfWeek.Saturday:
                                    row.Cells["SEIKYUU_DATE"].Value += "(土)";
                                    break;
                            }
                        }
                        row.Cells["SEIKYUUGAKU"].Value = Convert.ToString(result["SEIKYUUGAKU"]);
                        row.Cells["KeshikomiGaku"].Value = Convert.ToString(result["KeshikomiGaku"]);
                        row.Cells["keshikomiGakuTotal"].Value = Convert.ToString(result["KESHIKOMIGAKU_TOTAL"]);
                        row.Cells["MiKeshikomiGaku"].Value = Convert.ToString(result["MiKeshikomiGaku"]);
                        row.Cells["KESHIKOMI_BIKOU"].Value = Convert.ToString(result["KESHIKOMI_BIKOU"]);
                        row.Cells["SEIKYUU_NUMBER"].Value = Convert.ToString(result["SEIKYUU_NUMBER"]);
                        row.Cells["KAGAMI_NUMBER"].Value = Convert.ToString(result["KAGAMI_NUMBER"]);
                        row.Cells["SYSTEM_ID"].Value = Convert.ToString(result["SYSTEM_ID"]);
                        row.Cells["KESHIKOMI_SEQ"].Value = Convert.ToString(result["KESHIKOMI_SEQ"]);
                        row.Cells["NYUUKIN_NUMBER"].Value = Convert.ToString(result["NYUUKIN_NUMBER"]);

                        index++;
                    }
                }

                if (gyoushaFlag)
                {
                    this.form.dgvNyukinDeleteMeisai.Columns["GYOUSHA_CD"].Visible = false;
                    this.form.dgvNyukinDeleteMeisai.Columns["GYOUSHA_NAME_RYAKU"].Visible = false;
                }
                else
                {
                    this.form.dgvNyukinDeleteMeisai.Columns["GYOUSHA_CD"].Visible = true;
                    this.form.dgvNyukinDeleteMeisai.Columns["GYOUSHA_NAME_RYAKU"].Visible = true;
                }
                if (genbaFlag)
                {
                    this.form.dgvNyukinDeleteMeisai.Columns["GENBA_CD"].Visible = false;
                    this.form.dgvNyukinDeleteMeisai.Columns["GENBA_NAME_RYAKU"].Visible = false;
                }
                else
                {
                    this.form.dgvNyukinDeleteMeisai.Columns["GENBA_CD"].Visible = true;
                    this.form.dgvNyukinDeleteMeisai.Columns["GENBA_NAME_RYAKU"].Visible = true;
                }

                // 今回消込額、未入金額を再表示
                foreach (DataGridViewRow row in this.form.dgvNyukinDeleteMeisai.Rows)
                {
                    this.CalcTotalKeshikomigaku();
                }

                //foreach (DataRow keshikomiNyuukinsakiCdRow in dtKeshikomiNyuukinsakiCd.Rows)
                //{
                //    string strKey = keshikomiNyuukinsakiCdRow["TORIHIKISAKI_CD"].ToString() + ":" + keshikomiNyuukinsakiCdRow["SEIKYUU_NUMBER"].ToString();

                //    if (dicHashKeshikomi.ContainsKey(strKey))
                //    {
                //        decimal kingaku = 0;
                //        if (dicHashKeshikomi[strKey]["SEIKYUU_KINGAKU"] != null)
                //        {
                //            decimal.TryParse(Convert.ToString(dicHashKeshikomi[strKey]["SEIKYUU_KINGAKU"]), out kingaku);
                //        }
                //        // 請求金額が0の場合は非表示にする。
                //        if (kingaku != decimal.Zero)
                //        {
                //            DataRow row = dtKeshikomi.NewRow();
                //            row["ROW_NUMBER"] = DBNull.Value;
                //            row["TORIHIKISAKI_CD"] = dicHashKeshikomi[strKey]["TORIHIKISAKI_CD"];
                //            row["TORIHIKISAKI_NAME_RYAKU"] = dicHashKeshikomi[strKey]["TORIHIKISAKI_NAME_RYAKU"];
                //            row["SEIKYUU_DATE"] = dicHashKeshikomi[strKey]["SEIKYUU_DATE"];
                //            row["SORT_SEIKYUU_DATE"] = dicHashKeshikomi[strKey]["SORT_SEIKYUU_DATE"];
                //            row["SEIKYUU_NUMBER"] = dicHashKeshikomi[strKey]["SEIKYUU_NUMBER"];
                //            row["SEIKYUU_KINGAKU"] = dicHashKeshikomi[strKey]["SEIKYUU_KINGAKU"];
                //            row["SYSTEM_ID"] = dicHashKeshikomi[strKey]["SYSTEM_ID"];
                //            row["KESHIKOMI_BIKOU"] = dicHashKeshikomi[strKey]["KESHIKOMI_BIKOU"];

                //            dtKeshikomi.Rows.Add(row);
                //        }
                //    }
                //    else
                //    {
                //        decimal kingaku = 0;
                //        if (keshikomiNyuukinsakiCdRow["SEIKYUU_KINGAKU"] != null)
                //        {
                //            decimal.TryParse(Convert.ToString(keshikomiNyuukinsakiCdRow["SEIKYUU_KINGAKU"]), out kingaku);
                //        }
                //        // 請求金額が0の場合は非表示にする。
                //        if (kingaku != decimal.Zero)
                //        {
                //            DataRow row = dtKeshikomi.NewRow();
                //            row["ROW_NUMBER"] = DBNull.Value;
                //            row["TORIHIKISAKI_CD"] = keshikomiNyuukinsakiCdRow["TORIHIKISAKI_CD"];
                //            row["TORIHIKISAKI_NAME_RYAKU"] = keshikomiNyuukinsakiCdRow["TORIHIKISAKI_NAME_RYAKU"];
                //            row["SEIKYUU_DATE"] = keshikomiNyuukinsakiCdRow["SEIKYUU_DATE"];
                //            row["SORT_SEIKYUU_DATE"] = keshikomiNyuukinsakiCdRow["SORT_SEIKYUU_DATE"];
                //            row["SEIKYUU_NUMBER"] = keshikomiNyuukinsakiCdRow["SEIKYUU_NUMBER"];
                //            row["SEIKYUU_KINGAKU"] = keshikomiNyuukinsakiCdRow["SEIKYUU_KINGAKU"];
                //            row["SYSTEM_ID"] = keshikomiNyuukinsakiCdRow["SYSTEM_ID"];
                //            row["KESHIKOMI_BIKOU"] = keshikomiNyuukinsakiCdRow["KESHIKOMI_BIKOU"];

                //            dtKeshikomi.Rows.Add(row);
                //        }
                //    }
                //}

                // ROW_NUMBERを割り振る
                //if (dtKeshikomi.Rows.Count != 0)
                //{
                //    DataRow[] SortKeshikomiRows;
                //    Dictionary<String, int> dicHashSortKeshikomi = new Dictionary<string, int>();
                //    SortKeshikomiRows = dtKeshikomi.Select("", "SORT_SEIKYUU_DATE");

                //    for (int i = 0; i < SortKeshikomiRows.Length; i++)
                //    {
                //        SortKeshikomiRows[i]["ROW_NUMBER"] = i + 1;
                //    }
                //}

                //if (dtKeshikomi != null && dtKeshikomi.Rows.Count > 0)
                //{
                //    this.form.dgvNyukinDeleteMeisai.Rows.Add(dtKeshikomi.Rows.Count);
                //    for (int i = 0; i < dtKeshikomi.Rows.Count; i++)
                //    {
                //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["TORIHIKISAKI_CD"].Value = dtKeshikomi.Rows[i]["TORIHIKISAKI_CD"].ToString();
                //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["TORIHIKISAKI_NAME_RYAKU"].Value = dtKeshikomi.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_DATE"].Value = dtKeshikomi.Rows[i]["SEIKYUU_DATE"].ToString();
                //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SORT_SEIKYUU_DATE"].Value = dtKeshikomi.Rows[i]["SORT_SEIKYUU_DATE"].ToString();
                //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value = dtKeshikomi.Rows[i]["SEIKYUU_NUMBER"].ToString();

                //        decimal SeikyuKingaku = 0;
                //        if (dtKeshikomi.Rows[i]["SEIKYUU_KINGAKU"] != null)
                //        {
                //            decimal.TryParse(Convert.ToString(dtKeshikomi.Rows[i]["SEIKYUU_KINGAKU"]), out SeikyuKingaku);
                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_GAKU"].Value = SeikyuKingaku.ToString();
                //        }

                //        // T_NYUUKIN_ENTRY絞込(取引先)
                //        var nyuukinEntrys = this.nyuukinEntryList.Where(w => w.TORIHIKISAKI_CD.Equals(dtKeshikomi.Rows[i]["TORIHIKISAKI_CD"].ToString()));

                //        decimal KechikomiGaku = 0;

                //        if (dtKeshikomi.Rows[i]["SYSTEM_ID"].ToString() != "")
                //        {
                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SYSTEM_ID"].Value = dtKeshikomi.Rows[i]["SYSTEM_ID"].ToString();
                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUBIKOU"].Value = dtKeshikomi.Rows[i]["KESHIKOMI_BIKOU"].ToString();

                //            //入金消込の枝番を取得
                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KESHIKOMI_SEQ"].Value = "";
                //            int iSeikyuNumber = int.Parse(dtKeshikomi.Rows[i]["SEIKYUU_NUMBER"].ToString());
                //            int iSystemId = int.Parse(dtKeshikomi.Rows[i]["SYSTEM_ID"].ToString());
                //            T_NYUUKIN_KESHIKOMI entNyuukinKeshikomi = accessor.GetNyukinKeshikomi(iSystemId, iSeikyuNumber);
                //            if (entNyuukinKeshikomi != null && !entNyuukinKeshikomi.KESHIKOMI_SEQ.Equals(SqlInt32.Null))
                //            {
                //                decimal.TryParse(Convert.ToString(entNyuukinKeshikomi.KESHIKOMI_GAKU), out KechikomiGaku);
                //                this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KeshikomiGaku"].Value = KechikomiGaku.ToString();
                //                this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KESHIKOMI_SEQ"].Value = entNyuukinKeshikomi.KESHIKOMI_SEQ.ToString();
                //            }

                //        }
                //        else if (nyuukinEntrys != null && nyuukinEntrys.Count() > 0)
                //        {
                //            // 最大消込可能額 + SYSTEM_IDを設定
                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SYSTEM_ID"].Value = nyuukinEntrys.ElementAt(0).SYSTEM_ID;
                //        }

                //        if (nyuukinEntrys != null && nyuukinEntrys.Count() > 0)
                //        {
                //            decimal maxKeshikomiGaku = 0;
                //            foreach (var nyuukinEntry in nyuukinEntrys)
                //            {
                //                maxKeshikomiGaku += (decimal)nyuukinEntry.NYUUKIN_AMOUNT_TOTAL;
                //            }

                //            this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["NYUUKIN_AMOUNT_TOTAL"].Value = maxKeshikomiGaku;
                //        }
                //    }
                //}

                //CalculateKingakuForKeshikomiMeisai();

            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "入金先");
            }
        }

        /// <summary>
        /// 消込一覧をクリアする
        /// </summary>
        /// /// <returns></returns>
        private void ClearKesikomi()
        {
            this.form.dgvNyukinDeleteMeisai.EndEdit();
            this.form.dgvNyukinDeleteMeisai.Rows.Clear();
            this.form.txtKonkaiKeshikomiGaku.Text = "0";
            this.form.txtRuikeiMinyukinGaku.Text = "0";
            this.blankKeshikomiTable.Clear();
        }
        #endregion

        #region 合計金額計算処理
        /// <summary>
        /// 消込明細関連の金額を再計算する。
        /// </summary>
        /// /// <returns></returns>
        //internal void CalculateKingakuForKeshikomiMeisai()
        //{
        //    decimal keshikomigakuGoukei = 0;
        //    decimal seikyuuGakuTotal = 0;

        //    //for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
        //    //{
        //    //    decimal decSeikyuuKingaku = 0;
        //    //    decimal minyuukinGaku = 0;
        //    //    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KeshikomiGaku"].Value), out decSeikyuuKingaku);
        //    //    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out minyuukinGaku);
        //    //    this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["MiKeshikomiGaku"].Value = minyuukinGaku - decSeikyuuKingaku;
        //    //    keshikomigakuGoukei += decSeikyuuKingaku;
        //    //    seikyuuGakuTotal += minyuukinGaku;
        //    //}
        //    for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
        //    {
        //        decimal keshikomiGakuTotal = 0;
        //        decimal decSeikyuuKingaku = 0;
        //        decimal minyuukinGaku = 0;
        //        decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KeshikomiGaku"].Value), out decSeikyuuKingaku);
        //        decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out minyuukinGaku);
        //        string SEIKYUU_NUMBER = Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value);
        //        string KAGAMI_NUMBER = Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
        //        for (int j = 0; j < this.form.dgvNyukinDeleteMeisai.Rows.Count; j++)
        //        {
        //            if (Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["SEIKYUU_NUMBER"].Value) == SEIKYUU_NUMBER
        //            && Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["KAGAMI_NUMBER"].Value) == KAGAMI_NUMBER)
        //            {
        //                decimal KeshikomiGaku = 0;
        //                decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["KeshikomiGaku"].Value), out KeshikomiGaku);
        //                keshikomiGakuTotal += KeshikomiGaku;
        //            }
        //        }
        //        decimal seikyuuGaku = 0;
        //        decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out seikyuuGaku);
        //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value = keshikomiGakuTotal;
        //        this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["MiKeshikomiGaku"].Value = seikyuuGaku - keshikomiGakuTotal;
        //        keshikomigakuGoukei += decSeikyuuKingaku;
        //        seikyuuGakuTotal += minyuukinGaku;
        //    }

        //    this.form.txtRuikeiMinyukinGaku.Text = (seikyuuGakuTotal - keshikomigakuGoukei).ToString("#,##0");
        //    this.form.txtKonkaiKeshikomiGaku.Text = keshikomigakuGoukei.ToString("#,##0");
        //}
        internal bool CalculateKingakuForKeshikomiMeisai(int rowIndex, int columnIndex)
        {
            bool ret = true;
            try
            {
                decimal value = 0;
                decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells[columnIndex].Value), out value);
                long SEIKYUU_NUMBER = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["SEIKYUU_NUMBER"].Value);
                int KAGAMI_NUMBER = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["KAGAMI_NUMBER"].Value);
                if (value != this.form.keshikomiGakuWk)
                {
                    decimal keshikomiGakuTotal = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["keshikomiGakuTotal"].Value), out keshikomiGakuTotal);
                    decimal seikyuuGaku = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["SEIKYUUGAKU"].Value), out seikyuuGaku);
                    if (SEIKYUU_NUMBER == 0)
                    {
                        this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["keshikomiGakuTotal"].Value = keshikomiGakuTotal + value - this.form.keshikomiGakuWk;
                        this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["MiKeshikomiGaku"].Value = seikyuuGaku - (keshikomiGakuTotal + value - this.form.keshikomiGakuWk);
                    }
                    else
                    {
                        for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
                        {
                            long SEIKYUU_NUMBER2 = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value);
                            int KAGAMI_NUMBER2 = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
                            if (SEIKYUU_NUMBER2 == SEIKYUU_NUMBER && KAGAMI_NUMBER2 == KAGAMI_NUMBER)
                            {
                                this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value = keshikomiGakuTotal + value - this.form.keshikomiGakuWk;
                                this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["MiKeshikomiGaku"].Value = seikyuuGaku - (keshikomiGakuTotal + value - this.form.keshikomiGakuWk);
                            }
                        }
                    }
                    decimal keshikomiGaku = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[rowIndex].Cells["KeshikomiGaku"].Value), out keshikomiGaku);
                    this.form.keshikomiGakuWk = keshikomiGaku;
                }

                decimal keshikomigakuGoukei = 0;
                decimal seikyuuGakuTotal = 0;
                for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
                {
                    decimal decSeikyuuKingaku = 0;
                    decimal minyuukinGaku = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value), out decSeikyuuKingaku);
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out minyuukinGaku);
                    decimal seikyuuGaku = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out seikyuuGaku);
                    keshikomigakuGoukei += decSeikyuuKingaku;
                    long SEIKYUU_NUMBER3 = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value);
                    int KAGAMI_NUMBER3 = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
                    if (SEIKYUU_NUMBER3 == 0)
                    {
                        seikyuuGakuTotal += minyuukinGaku;
                    }
                    else
                    {
                        bool seikyuuGakuFlg = true;
                        for (int j = 0; j < i; j++)
                        {
                            long SEIKYUU_NUMBER4 = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["SEIKYUU_NUMBER"].Value);
                            int KAGAMI_NUMBER4 = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["KAGAMI_NUMBER"].Value);
                            if (SEIKYUU_NUMBER4 == SEIKYUU_NUMBER3 && KAGAMI_NUMBER4 == KAGAMI_NUMBER3)
                            {
                                seikyuuGakuFlg = false;
                                break;
                            }
                        }
                        if (seikyuuGakuFlg)
                        {
                            seikyuuGakuTotal += minyuukinGaku;
                        }
                    }
                }
                this.form.txtRuikeiMinyukinGaku.Text = (seikyuuGakuTotal - keshikomigakuGoukei).ToString("#,##0");
                this.form.txtKonkaiKeshikomiGaku.Text = keshikomigakuGoukei.ToString("#,##0");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalculateKingakuForKeshikomiMeisai", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 画面の今回消込額、未入金額を再計算
        /// </summary>
        private void CalcTotalKeshikomigaku()
        {
            decimal keshikomigakuGoukei = 0;
            decimal seikyuuGakuTotal = 0;
            for (int i = 0; i < this.form.dgvNyukinDeleteMeisai.Rows.Count; i++)
            {
                decimal decSeikyuuKingaku = 0;
                decimal minyuukinGaku = 0;
                decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value), out decSeikyuuKingaku);
                decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out minyuukinGaku);
                decimal seikyuuGaku = 0;
                decimal.TryParse(Convert.ToString(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUUGAKU"].Value), out seikyuuGaku);
                keshikomigakuGoukei += decSeikyuuKingaku;
                long SEIKYUU_NUMBER3 = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["SEIKYUU_NUMBER"].Value);
                int KAGAMI_NUMBER3 = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
                if (SEIKYUU_NUMBER3 == 0)
                {
                    seikyuuGakuTotal += minyuukinGaku;
                }
                else
                {
                    bool seikyuuGakuFlg = true;
                    for (int j = 0; j < i; j++)
                    {
                        long SEIKYUU_NUMBER4 = Convert.ToInt64(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["SEIKYUU_NUMBER"].Value);
                        int KAGAMI_NUMBER4 = Convert.ToInt32(this.form.dgvNyukinDeleteMeisai.Rows[j].Cells["KAGAMI_NUMBER"].Value);
                        if (SEIKYUU_NUMBER4 == SEIKYUU_NUMBER3 && KAGAMI_NUMBER4 == KAGAMI_NUMBER3)
                        {
                            seikyuuGakuFlg = false;
                            break;
                        }
                    }
                    if (seikyuuGakuFlg)
                    {
                        seikyuuGakuTotal += minyuukinGaku;
                    }
                }
            }
            this.form.txtRuikeiMinyukinGaku.Text = (seikyuuGakuTotal - keshikomigakuGoukei).ToString("#,##0");
            this.form.txtKonkaiKeshikomiGaku.Text = keshikomigakuGoukei.ToString("#,##0");
        }
        #endregion

        #region [F9]登録押下時
        private const string NYUUKIN_NUMBER_NOT_FOUND_MEG = "入金番号が見つかりませんでした。画面を閉じて再度実行してください。\n繰り返し発生する場合はシステム管理者へ問い合わせてください。";

        /// <summary>
        /// 登録ボタン押下時処理
        /// </summary>
        public void Function9ClickLogic()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            //消込明細0件チェック
            if (this.form.dgvNyukinDeleteMeisai.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("E061");
                return;
            }

            if (string.IsNullOrEmpty(this.form.initDto.NyuukinNumber))
            {
                msgLogic.MessageBoxShowError(NYUUKIN_NUMBER_NOT_FOUND_MEG);
                return;
            }

            try
            {
                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {

                    // 論理削除
                    // 入金消込取得
                    T_NYUUKIN_KESHIKOMI[] nyuukinKeshikomis = this.accessor.GetNyuukinKeshikomi(this.form.initDto.NyuukinNumber, this.form.initDto.TorihikisakiCd);

                    foreach (T_NYUUKIN_KESHIKOMI nyuukinKeshikomi in nyuukinKeshikomis)
                    {
                        // 起動元で締処理済みチェックしてくれてるので、何も考えずに論理削除
                        nyuukinKeshikomi.DELETE_FLG = true;
                        this.accessor.UpdateNyuukinKeshikomi(nyuukinKeshikomi);
                    }

                    // 一番大きいsystem_idを取得
                    //long maxSystemId = this.accessor.GetKeshikomiMaxSystemId();
                    //int index = 1;
                    // 新しいデータを追加
                    foreach (DataGridViewRow row in this.form.dgvNyukinDeleteMeisai.Rows)
                    {
                        if (row.IsNewRow == true)
                        {
                            break;
                        }

                        // 必要なデータがない場合は省く
                        //long systemId = 0;
                        //long nyuukinNumber = 0;
                        //decimal keshikomiGaku = 0;
                        //if (!long.TryParse(Convert.ToString(row.Cells["SYSTEM_ID"].Value), out systemId)
                        //    || !long.TryParse(this.form.initDto.NyuukinNumber, out nyuukinNumber)
                        //    || !decimal.TryParse(Convert.ToString(row.Cells["KeshikomiGaku"].Value), out keshikomiGaku))
                        //{
                        //    continue;
                        //}

                        //T_NYUUKIN_KESHIKOMI nyuukinKeshikomi = new T_NYUUKIN_KESHIKOMI();
                        //nyuukinKeshikomi.SYSTEM_ID = systemId;
                        //nyuukinKeshikomi.SEIKYUU_NUMBER = Convert.ToInt64(row.Cells["SEIKYUU_NUMBER"].Value.ToString());
                        //nyuukinKeshikomi.KAGAMI_NUMBER = 1;     // 2015/1/26時点での暫定対応
                        //int keshikomiSeq = this.accessor.GetNyuukinKeshikomiMaxKeshikomiSeq(nyuukinKeshikomi.SYSTEM_ID, nyuukinKeshikomi.SEIKYUU_NUMBER);
                        //keshikomiSeq++;

                        //nyuukinKeshikomi.KESHIKOMI_SEQ = keshikomiSeq;
                        //nyuukinKeshikomi.NYUUKIN_NUMBER = nyuukinNumber;
                        //nyuukinKeshikomi.KESHIKOMI_GAKU = keshikomiGaku;
                        //if (row.Cells["SEIKYUUBIKOU"].Value == null)
                        //{
                        //    nyuukinKeshikomi.KESHIKOMI_BIKOU = "";
                        //}
                        //else
                        //{
                        //    nyuukinKeshikomi.KESHIKOMI_BIKOU = row.Cells["SEIKYUUBIKOU"].Value.ToString();
                        //}
                        //// NYUUKIN_SEQは使わないため、ダミーデータとして0を設定
                        //nyuukinKeshikomi.NYUUKIN_SEQ = 0;
                        //nyuukinKeshikomi.DELETE_FLG = false;
                        //nyuukinKeshikomi.TORIHIKISAKI_CD = row.Cells["TORIHIKISAKI_CD"].Value.ToString();

                        //var dataNyuukinKeshikomi = new DataBinderLogic<T_NYUUKIN_KESHIKOMI>(nyuukinKeshikomi);
                        //dataNyuukinKeshikomi.SetSystemProperty(nyuukinKeshikomi, false);
                        T_NYUUKIN_KESHIKOMI nyuukinKeshikomi = new T_NYUUKIN_KESHIKOMI();
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["KESHIKOMI_SEQ"].Value)))
                        {

                            this.accessor.DeleteDataByCd(Convert.ToInt64(row.Cells["SYSTEM_ID"].Value), Convert.ToInt64(row.Cells["SEIKYUU_NUMBER"].Value), Convert.ToInt32(row.Cells["KESHIKOMI_SEQ"].Value));
                            nyuukinKeshikomi.SYSTEM_ID = Convert.ToInt64(row.Cells["SYSTEM_ID"].Value);
                            nyuukinKeshikomi.KESHIKOMI_SEQ = Convert.ToInt32(row.Cells["KESHIKOMI_SEQ"].Value) + 1;
                            nyuukinKeshikomi.SEIKYUU_NUMBER = Convert.ToInt64(row.Cells["SEIKYUU_NUMBER"].Value);
                            nyuukinKeshikomi.KAGAMI_NUMBER = Convert.ToInt32(row.Cells["KAGAMI_NUMBER"].Value);
                            nyuukinKeshikomi.NYUUKIN_NUMBER = Convert.ToInt64(row.Cells["NYUUKIN_NUMBER"].Value);
                            nyuukinKeshikomi.TORIHIKISAKI_CD = this.form.initDto.TorihikisakiCd;
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["KeshikomiGaku"].Value)))
                            {
                                nyuukinKeshikomi.KESHIKOMI_GAKU = Convert.ToDecimal(row.Cells["KeshikomiGaku"].Value);
                            }
                            else
                            {
                                continue;
                            }
                            nyuukinKeshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                            nyuukinKeshikomi.NYUUKIN_SEQ = 0;
                            nyuukinKeshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                            if (nyuukinKeshikomi.DELETE_FLG)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            foreach (T_NYUUKIN_ENTRY nyuukinEntry in nyuukinEntryList)
                            {
                                if (this.form.initDto.TorihikisakiCd == nyuukinEntry.TORIHIKISAKI_CD)
                                {
                                    nyuukinKeshikomi.SYSTEM_ID = nyuukinEntry.SYSTEM_ID;
                                    break;
                                }
                            }
                            //index++;
                            nyuukinKeshikomi.KESHIKOMI_SEQ = 1;
                            nyuukinKeshikomi.SEIKYUU_NUMBER = Convert.ToInt64(row.Cells["SEIKYUU_NUMBER"].Value);
                            nyuukinKeshikomi.KAGAMI_NUMBER = Convert.ToInt32(row.Cells["KAGAMI_NUMBER"].Value);

                            T_NYUUKIN_KESHIKOMI[] nyuukinKeshikomisDelList = this.accessor.GetNyuukinKeshikomi(nyuukinKeshikomi.SYSTEM_ID,
                                nyuukinKeshikomi.SEIKYUU_NUMBER, nyuukinKeshikomi.KAGAMI_NUMBER);
                            List<SqlInt32> KeshikomiSEQList = new List<SqlInt32>();
                            foreach (T_NYUUKIN_KESHIKOMI record in nyuukinKeshikomisDelList)
                            {
                                KeshikomiSEQList.Add(record.KESHIKOMI_SEQ);
                            }
                            if (KeshikomiSEQList.Count > 0)
                            {
                                nyuukinKeshikomi.KESHIKOMI_SEQ = KeshikomiSEQList.Max() + 1;
                            }

                            long nyuukinNumber = 0;
                            if (long.TryParse(this.form.initDto.NyuukinNumber, out nyuukinNumber))
                            {
                                nyuukinKeshikomi.NYUUKIN_NUMBER = nyuukinNumber;
                            }
                            else
                            {
                                nyuukinKeshikomi.NYUUKIN_NUMBER = 0;
                            }
                            nyuukinKeshikomi.TORIHIKISAKI_CD = this.form.initDto.TorihikisakiCd;
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["KeshikomiGaku"].Value)))
                            {
                                decimal KeshikomiGaku = 0;
                                if (decimal.TryParse(row.Cells["KeshikomiGaku"].Value.ToString(), out KeshikomiGaku))
                                {
                                    nyuukinKeshikomi.KESHIKOMI_GAKU = KeshikomiGaku;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                            nyuukinKeshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                            nyuukinKeshikomi.NYUUKIN_SEQ = 0;
                            nyuukinKeshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                            if (nyuukinKeshikomi.DELETE_FLG)
                            {
                                nyuukinKeshikomi.KESHIKOMI_GAKU = 0;
                            }
                        }
                        this.accessor.insertNyuukinKeshikomi(nyuukinKeshikomi);
                    }

                    // コミット
                    tran.Commit();
                }
                // 完了メッセージ表示
                msgLogic.MessageBoxShow("I001", "登録");

                this.SearchKeshikomiInfoByNyuukinsakiCd();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    // メッセージ出力して継続
                    msgLogic.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {

                    msgLogic.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region システムID採番
        /// <summary>
        /// 新規にシステムIDを取得します
        /// </summary>
        /// <returns>システムID</returns>
        private SqlInt64 CreateSystemId()
        {
            LogUtility.DebugMethodStart();

            var ret = SqlInt64.Null;

            var accessor = new DBAccessor();
            ret = accessor.createSystemId((int)DENSHU_KBN.NYUUKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        #endregion

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
    }
}
