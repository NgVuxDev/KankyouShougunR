using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
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
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using System.ComponentModel;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomiShusei
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
        internal BusinessBaseForm baseForm;

        /// <summary>アクセッサー</summary>
        //private DBAccessor accessor;

        //空行用の消込明細
        private DataTable blankKeshikomiTable = new DataTable();

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>出金入力データ</summary>
        private List<T_SHUKKIN_ENTRY> nyuukinEntryList = new List<T_SHUKKIN_ENTRY>();

        private SearchDTO searchString = new SearchDTO();

        private DAOClass kesikomiDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// 修正権限があるかどうかを取得・設定します
        /// </summary>
        internal bool AuthCanUpdate { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.kesikomiDao = DaoInitUtility.GetComponent<DAOClass>();
            // 権限をセット
            var formId = FormManager.GetFormID(Assembly.GetExecutingAssembly());
            this.AuthCanUpdate = Manager.CheckAuthority(formId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
        }

        #endregion

        #region 初期化処理

        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                this.baseForm = (BusinessBaseForm)this.form.Parent;
                var SEISAN_DATE_TO = this.form.SeisanDateto;
                if (string.IsNullOrWhiteSpace(SEISAN_DATE_TO))
                {
                    this.form.SEISAN_DATE_FROM.Value = this.baseForm.sysDate.AddMonths(-1).AddDays(1);
                    this.form.SEISAN_DATE_TO.Value = this.baseForm.sysDate;
                }
                else
                {
                    this.form.SEISAN_DATE_FROM.Value = Convert.ToDateTime(SEISAN_DATE_TO).AddMonths(-1).AddDays(1);
                    this.form.SEISAN_DATE_TO.Value = SEISAN_DATE_TO;
                }

                ((HeaderBaseForm)baseForm.headerForm).windowTypeLabel.Visible = false;
                ((HeaderBaseForm)baseForm.headerForm).lb_title.Location = new Point(0, 6);
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 新規モードまたは修正モードで、修正権限なしの場合は参照モードにする（この画面では追加権限は設定されない）
                if ((this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG) && !this.AuthCanUpdate)
                {
                    this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.form.dgvShukkinDeleteMeisai.ReadOnly = false;
                        this.baseForm.bt_func9.Enabled = true;
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        this.form.dgvShukkinDeleteMeisai.ReadOnly = true;
                        this.baseForm.bt_func9.Enabled = false;
                        this.baseForm.bt_func3.Enabled = false;
                        break;
                }
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
            this.baseForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            // 登録ボタン(F9)イベント
            this.baseForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 閉じるボタン(F12)イベント
            this.baseForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            this.form.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.TORIHIKISAKI_CD_Validating);
        }

        #endregion

        #region イベント

        #region 消込金額チェック

        private const string OVER_SEISAN_GAKU_MEG = "精算金額以下で入力してください。";

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
                var keshikomiGakuCell = this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells[columnIndex] as DgvCustomTextBoxCell;
                // 比較用の値
                var seisanKingakuCell = this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["SEISANGAKU"] as DgvCustomTextBoxCell;

                // null空チェック
                if (keshikomiGakuCell.Value == null
                    || string.IsNullOrEmpty(Convert.ToString(keshikomiGakuCell.Value)))
                {
                    return returnVal;
                }

                // 今回入力された消込金額
                decimal keshikomiGaku = 0;
                // 総消込金額
                decimal keshikomiGakuTotal = 0;

                long seisanNum = Convert.ToInt64(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["SEISAN_NUMBER"].Value);
                int kagamiNum = Convert.ToInt32(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["KAGAMI_NUMBER"].Value);
                for (int i = 0; i < this.form.dgvShukkinDeleteMeisai.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.dgvShukkinDeleteMeisai.Rows[i];
                    decimal kingaku = 0;
                    if (seisanNum == Convert.ToInt64(row.Cells["SEISAN_NUMBER"].Value)
                        && kagamiNum == Convert.ToInt32(row.Cells["KAGAMI_NUMBER"].Value))
                    {
                        if (decimal.TryParse(Convert.ToString(row.Cells["KeshikomiGaku"].Value), out kingaku))
                        {
                            keshikomiGakuTotal += kingaku;
                        }
                    }
                }

                if (decimal.TryParse(Convert.ToString(keshikomiGakuCell.Value), out keshikomiGaku))
                {
                    decimal seikyuuGaku = 0;

                    if (decimal.TryParse(Convert.ToString(seisanKingakuCell.Value), out seikyuuGaku))
                    {
                        if (seikyuuGaku > 0 && keshikomiGaku < 0)
                        {
                            msgLogic.MessageBoxShow("C103", "0円以下");
                        }

                        if (seikyuuGaku < 0 && keshikomiGaku > 0)
                        {
                            msgLogic.MessageBoxShow("C103", "0円以上");
                        }

                        //if ((seikyuuGaku > 0 && seikyuuGaku - keshikomiGakuTotal < 0) || (seikyuuGaku < 0 && seikyuuGaku - keshikomiGakuTotal > 0))
                        //{
                        //    msgLogic.MessageBoxShowError(OVER_SEISAN_GAKU_MEG);
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

        public bool IchiranSearch()
        {
            bool ret = true;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 必須チェック
                var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.form.RegistErrorFlag)
                {
                    var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                    if (null != focusControl)
                    {
                        ((Control)focusControl).Focus();
                    }
                    return ret;
                }

                if (CheckDate())
                {
                    return ret;
                }
                this.searchString.SEISAN_DATE_FROM = Convert.ToString(this.form.SEISAN_DATE_FROM.Value);
                this.searchString.SEISAN_DATE_TO = Convert.ToString(this.form.SEISAN_DATE_TO.Value);
                this.searchString.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                this.searchString.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME.Text;
                this.searchString.Zandaka = this.form.Zandaka.Checked ? "1" : "0";
                DataTable searchData = this.kesikomiDao.GetKeshikomi(searchString);

                ClearKesikomi();

                bool gyoushaFlag = true;
                bool genbaFlag = true;
                int index = 0;
                DateTime date = this.baseForm.sysDate;
                bool MenuFrom = true;
                if (searchData != null && searchData.Rows.Count > 0)
                {
                    //string select = "1=1";
                    if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && !string.IsNullOrEmpty(this.form.shukkinNumber))
                    {
                        //select += " AND SHUKKIN_NUMBER = " + this.form.nyuukinNumber;
                        //if (searchData.Select(select) == null)
                        //{
                        //    select = "1=1";
                        //}
                        MenuFrom = false;
                    }

                    //for (int i = 0; i < searchData.Select(select).Length; i++)
                    for (int i = 0; i < searchData.Rows.Count; i++)
                    {
                        //DataRow result = searchData.Select(select)[i];
                        DataRow result = searchData.Rows[i];

                        decimal kingaku = 0;
                        if (result["SEISANGAKU"] != null)
                        {
                            decimal.TryParse(Convert.ToString(result["SEISANGAKU"]), out kingaku);
                        }
                        if (kingaku == decimal.Zero)
                        {
                            continue;
                        }
                        this.form.dgvShukkinDeleteMeisai.Rows.Add(1);
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(result["GYOUSHA_CD"])))
                        {
                            gyoushaFlag = false;
                        }
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(result["GENBA_CD"])))
                        {
                            genbaFlag = false;
                        }
                        DataGridViewRow row = this.form.dgvShukkinDeleteMeisai.Rows[index];
                        row.Cells["ROW_NUMBER"].Value = Convert.ToString(result["ROW_NUMBER"]);
                        row.Cells["GYOUSHA_CD"].Value = Convert.ToString(result["GYOUSHA_CD"]);
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = Convert.ToString(result["GYOUSHA_NAME_RYAKU"]);
                        row.Cells["GENBA_CD"].Value = Convert.ToString(result["GENBA_CD"]);
                        row.Cells["GENBA_NAME_RYAKU"].Value = Convert.ToString(result["GENBA_NAME_RYAKU"]);
                        row.Cells["SEISAN_DATE"].Value = Convert.ToString(result["SEISAN_DATE"]);
                        if (DateTime.TryParse(Convert.ToString(row.Cells["SEISAN_DATE"].Value), out date))
                        {
                            switch (date.DayOfWeek)
                            {
                                case DayOfWeek.Sunday:
                                    row.Cells["SEISAN_DATE"].Value += "(日)";
                                    break;

                                case DayOfWeek.Monday:
                                    row.Cells["SEISAN_DATE"].Value += "(月)";
                                    break;

                                case DayOfWeek.Tuesday:
                                    row.Cells["SEISAN_DATE"].Value += "(火)";
                                    break;

                                case DayOfWeek.Wednesday:
                                    row.Cells["SEISAN_DATE"].Value += "(水)";
                                    break;

                                case DayOfWeek.Thursday:
                                    row.Cells["SEISAN_DATE"].Value += "(木)";
                                    break;

                                case DayOfWeek.Friday:
                                    row.Cells["SEISAN_DATE"].Value += "(金)";
                                    break;

                                case DayOfWeek.Saturday:
                                    row.Cells["SEISAN_DATE"].Value += "(土)";
                                    break;
                            }
                        }
                        row.Cells["SEISANGAKU"].Value = kingaku;
                        if (!string.IsNullOrEmpty(result["KeshikomiGaku"].ToString()))
                        {
                            row.Cells["KeshikomiGaku"].Value = Convert.ToDecimal(result["KeshikomiGaku"]);
                        }
                        else
                        {
                            row.Cells["KeshikomiGaku"].Value = string.Empty;
                        }
                        row.Cells["keshikomiGakuTotal"].Value = Convert.ToString(result["KESHIKOMIGAKU_TOTAL"]);
                        row.Cells["MiKeshikomiGaku"].Value = Convert.ToString(result["MiKeshikomiGaku"]);
                        row.Cells["KESHIKOMI_BIKOU"].Value = Convert.ToString(result["KESHIKOMI_BIKOU"]);
                        row.Cells["SEISAN_NUMBER"].Value = Convert.ToString(result["SEISAN_NUMBER"]);
                        row.Cells["KAGAMI_NUMBER"].Value = Convert.ToString(result["KAGAMI_NUMBER"]);
                        row.Cells["SYSTEM_ID"].Value = Convert.ToString(result["SYSTEM_ID"]);
                        row.Cells["KESHIKOMI_SEQ"].Value = Convert.ToString(result["KESHIKOMI_SEQ"]);
                        row.Cells["SHUKKIN_NUMBER"].Value = Convert.ToString(result["SHUKKIN_NUMBER"]);

                        index++;
                    }
                }

                if (gyoushaFlag)
                {
                    this.form.dgvShukkinDeleteMeisai.Columns["GYOUSHA_CD"].Visible = false;
                    this.form.dgvShukkinDeleteMeisai.Columns["GYOUSHA_NAME_RYAKU"].Visible = false;
                }
                else
                {
                    this.form.dgvShukkinDeleteMeisai.Columns["GYOUSHA_CD"].Visible = true;
                    this.form.dgvShukkinDeleteMeisai.Columns["GYOUSHA_NAME_RYAKU"].Visible = true;
                }
                if (genbaFlag)
                {
                    this.form.dgvShukkinDeleteMeisai.Columns["GENBA_CD"].Visible = false;
                    this.form.dgvShukkinDeleteMeisai.Columns["GENBA_NAME_RYAKU"].Visible = false;
                }
                else
                {
                    this.form.dgvShukkinDeleteMeisai.Columns["GENBA_CD"].Visible = true;
                    this.form.dgvShukkinDeleteMeisai.Columns["GENBA_NAME_RYAKU"].Visible = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IchiranSearch", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranSearch", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            //this.CalculateKingakuForKeshikomiMeisai(searchData);
            return ret;
        }

        /// <summary>
        /// 消込一覧をクリアする
        /// </summary>
        /// <returns></returns>
        private void ClearKesikomi()
        {
            this.form.dgvShukkinDeleteMeisai.EndEdit();
            this.form.dgvShukkinDeleteMeisai.Rows.Clear();
            this.blankKeshikomiTable.Clear();
        }

        #endregion

        #region 未消込額再計算

        /// <summary>
        /// 消込明細関連の未消込額を再計算する。
        /// </summary>
        /// /// <returns></returns>
        //internal void CalculateKingakuForKeshikomiMeisai(DataTable dt)
        //{
        //    for (int i = 0; i < this.form.dgvShukkinDeleteMeisai.Rows.Count; i++)
        //    {
        //        decimal keshikomiGakuTotal = 0;
        //        decimal maeKeshikomiGakuTotal = 0;
        //        long SEISAN_NUMBER = Convert.ToInt64(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["SEISAN_NUMBER"].Value);
        //        int KAGAMI_NUMBER = Convert.ToInt32(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
        //        if (dt == null)
        //        {
        //            for (int j = 0; j < this.form.dgvShukkinDeleteMeisai.Rows.Count; j++)
        //            {
        //                if (Convert.ToInt64(this.form.dgvShukkinDeleteMeisai.Rows[j].Cells["SEISAN_NUMBER"].Value) == SEISAN_NUMBER
        //                && Convert.ToInt32(this.form.dgvShukkinDeleteMeisai.Rows[j].Cells["KAGAMI_NUMBER"].Value) == KAGAMI_NUMBER)
        //                {
        //                    decimal decSeikyuuKingaku = 0;
        //                    decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[j].Cells["KeshikomiGaku"].Value), out decSeikyuuKingaku);
        //                    keshikomiGakuTotal += decSeikyuuKingaku;
        //                }
        //            }
        //            decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["maeKeshikomiGakuTotal"].Value), out maeKeshikomiGakuTotal);
        //        }
        //        else
        //        {
        //            DataRow[] rows = dt.Select("SEISANGAKU <> 0");
        //            if (rows == null)
        //            {
        //                return;
        //            }
        //            decimal keshikomiGaku = 0;
        //            for (int j = 0; j < rows.Length; j++)
        //            {
        //                if (Convert.ToInt64(rows[j]["SEISAN_NUMBER"]) == SEISAN_NUMBER
        //                && Convert.ToInt32(rows[j]["KAGAMI_NUMBER"]) == KAGAMI_NUMBER)
        //                {
        //                    decimal decSeikyuuKingaku = 0;
        //                    decimal.TryParse(Convert.ToString(rows[j]["KeshikomiGaku"]), out decSeikyuuKingaku);
        //                    keshikomiGakuTotal += decSeikyuuKingaku;

        //                    long nyuukinNumber1 = 0;
        //                    long nyuukinNumber2 = 0;
        //                    long.TryParse(Convert.ToString(rows[j]["SHUKKIN_NUMBER"]), out nyuukinNumber1);
        //                    long.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["SHUKKIN_NUMBER"].Value), out nyuukinNumber2);
        //                    if (nyuukinNumber1 != nyuukinNumber2)
        //                    {
        //                        decimal.TryParse(Convert.ToString(rows[j]["KeshikomiGaku"]), out keshikomiGaku);
        //                        maeKeshikomiGakuTotal += keshikomiGaku;
        //                    }
        //                }
        //            }
        //            this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["maeKeshikomiGakuTotal"].Value = maeKeshikomiGakuTotal;
        //        }
        //        decimal seikyuuGaku = 0;
        //        decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["SEISANGAKU"].Value), out seikyuuGaku);
        //        this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value = keshikomiGakuTotal;
        //        this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["MiKeshikomiGaku"].Value = seikyuuGaku - keshikomiGakuTotal;
        //    }

        //}

        /// <summary>
        /// 消込明細関連の未消込額を再計算する。
        /// </summary>
        /// /// <returns></returns>
        internal bool CalculateKingakuForKeshikomiMeisai(int rowIndex, int columnIndex)
        {
            bool ret = true;
            try
            {
                decimal value = 0;
                decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells[columnIndex].Value), out value);
                long SEISAN_NUMBER = Convert.ToInt64(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["SEISAN_NUMBER"].Value);
                int KAGAMI_NUMBER = Convert.ToInt32(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["KAGAMI_NUMBER"].Value);
                if (value != this.form.keshikomiGakuWk)
                {
                    decimal keshikomiGakuTotal = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["keshikomiGakuTotal"].Value), out keshikomiGakuTotal);
                    decimal seikyuuGaku = 0;
                    decimal.TryParse(Convert.ToString(this.form.dgvShukkinDeleteMeisai.Rows[rowIndex].Cells["SEISANGAKU"].Value), out seikyuuGaku);
                    for (int i = 0; i < this.form.dgvShukkinDeleteMeisai.Rows.Count; i++)
                    {
                        long SEISAN_NUMBER2 = Convert.ToInt64(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["SEISAN_NUMBER"].Value);
                        int KAGAMI_NUMBER2 = Convert.ToInt32(this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["KAGAMI_NUMBER"].Value);
                        if (SEISAN_NUMBER2 == SEISAN_NUMBER && KAGAMI_NUMBER2 == KAGAMI_NUMBER)
                        {
                            this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["SEISANGAKU"].Value = seikyuuGaku;
                            this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["keshikomiGakuTotal"].Value = keshikomiGakuTotal + value - this.form.keshikomiGakuWk;
                            this.form.dgvShukkinDeleteMeisai.Rows[i].Cells["MiKeshikomiGaku"].Value = seikyuuGaku - (keshikomiGakuTotal + value - this.form.keshikomiGakuWk);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalculateKingakuForKeshikomiMeisai", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region [F9]登録押下時

        private const string SHUKKIN_NUMBER_NOT_FOUND_MEG = "出金番号が見つかりませんでした。画面を閉じて再度実行してください。\n繰り返し発生する場合はシステム管理者へ問い合わせてください。";

        /// <summary>
        /// 登録ボタン押下時処理
        /// </summary>
        public void Function9ClickLogic()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 必須チェック
            var autoCheckLogic = new AutoRegistCheckLogic(this.form.allControl, this.form.allControl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (this.form.RegistErrorFlag)
            {
                var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                if (null != focusControl)
                {
                    ((Control)focusControl).Focus();
                }
                return;
            }

            if (CheckDate())
            {
                return;
            }

            DialogResult result = new DialogResult();
            if (!string.IsNullOrEmpty(this.searchString.TORIHIKISAKI_CD))
            {
                if (this.searchString.TORIHIKISAKI_CD != this.form.TORIHIKISAKI_CD.Text)
                {
                    result = msgLogic.MessageBoxShow("C087", "取引先");
                    if (result == DialogResult.Yes)
                    {
                        this.ClearKesikomi();
                        this.searchString = new SearchDTO();
                        return;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_CD.Text = this.searchString.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME.Text = this.searchString.TORIHIKISAKI_NAME;
                    }
                }

                if (this.searchString.SEISAN_DATE_FROM != Convert.ToString(this.form.SEISAN_DATE_FROM.Value)
                    || this.searchString.SEISAN_DATE_TO != Convert.ToString(this.form.SEISAN_DATE_TO.Value))
                {
                    this.ClearKesikomi();
                    this.searchString = new SearchDTO();
                    return;
                }
                else
                {
                    this.form.SEISAN_DATE_FROM.Value = this.searchString.SEISAN_DATE_FROM;
                    this.form.SEISAN_DATE_FROM.Text = this.searchString.SEISAN_DATE_FROM;
                    this.form.SEISAN_DATE_TO.Value = this.searchString.SEISAN_DATE_TO;
                    this.form.SEISAN_DATE_TO.Text = this.searchString.SEISAN_DATE_TO;
                }
            }

            //消込明細0件チェック
            if (this.form.dgvShukkinDeleteMeisai.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("E061");
                return;
            }

            try
            {
                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    // 新しいデータを追加
                    //long maxSystemId = this.kesikomiDao.GetKeshikomiMaxSystemId();
                    //int index = 1;
                    foreach (DataGridViewRow row in this.form.dgvShukkinDeleteMeisai.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        T_SHUKKIN_KESHIKOMI shukkinKeshikomi = new T_SHUKKIN_KESHIKOMI();
                        T_SHUKKIN_KESHIKOMI data = new T_SHUKKIN_KESHIKOMI();
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["SYSTEM_ID"].Value)))
                        {
                            shukkinKeshikomi.SYSTEM_ID = Convert.ToInt64(row.Cells["SYSTEM_ID"].Value);
                            shukkinKeshikomi.SEISAN_NUMBER = Convert.ToInt64(row.Cells["SEISAN_NUMBER"].Value);
                            shukkinKeshikomi.KAGAMI_NUMBER = Convert.ToInt32(row.Cells["KAGAMI_NUMBER"].Value);
                            shukkinKeshikomi.SHUKKIN_NUMBER = Convert.ToInt64(row.Cells["SHUKKIN_NUMBER"].Value);
                            shukkinKeshikomi.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                            shukkinKeshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["KeshikomiGaku"].Value)))
                            {
                                shukkinKeshikomi.KESHIKOMI_GAKU = Convert.ToDecimal(row.Cells["KeshikomiGaku"].Value);
                            }
                            else
                            {
                                shukkinKeshikomi.DELETE_FLG = true;
                            }
                            shukkinKeshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                            shukkinKeshikomi.SHUKKIN_SEQ = 0;

                            data = new T_SHUKKIN_KESHIKOMI();
                            data.SYSTEM_ID = shukkinKeshikomi.SYSTEM_ID;
                            data.SEISAN_NUMBER = shukkinKeshikomi.SEISAN_NUMBER;
                            data.KAGAMI_NUMBER = shukkinKeshikomi.KAGAMI_NUMBER;
                            //data.DELETE_FLG = false;
                            data = this.kesikomiDao.GetDataForEntity(data).LastOrDefault();
                            if (data != null)
                            {
                                this.kesikomiDao.DeleteDataByCd(data.SYSTEM_ID.Value, data.SEISAN_NUMBER.Value, data.KESHIKOMI_SEQ.Value);
                                shukkinKeshikomi.KESHIKOMI_SEQ = data.KESHIKOMI_SEQ + 1;
                            }
                            else
                            {
                                shukkinKeshikomi.KESHIKOMI_SEQ = 1;
                            }
                            if (shukkinKeshikomi.DELETE_FLG)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            shukkinKeshikomi.SYSTEM_ID = this.CreateSystemId();
                            shukkinKeshikomi.KESHIKOMI_SEQ = 1;
                            shukkinKeshikomi.SEISAN_NUMBER = Convert.ToInt64(row.Cells["SEISAN_NUMBER"].Value);
                            shukkinKeshikomi.KAGAMI_NUMBER = Convert.ToInt32(row.Cells["KAGAMI_NUMBER"].Value);
                            shukkinKeshikomi.SHUKKIN_NUMBER = 0;
                            shukkinKeshikomi.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["KeshikomiGaku"].Value)))
                            {
                                shukkinKeshikomi.KESHIKOMI_GAKU = Convert.ToDecimal(row.Cells["KeshikomiGaku"].Value);
                            }
                            else
                            {
                                continue;
                            }
                            shukkinKeshikomi.KESHIKOMI_BIKOU = Convert.ToString(row.Cells["KESHIKOMI_BIKOU"].Value);
                            shukkinKeshikomi.SHUKKIN_SEQ = 0;
                            shukkinKeshikomi.DELETE_FLG = Convert.ToBoolean(row.Cells["DELETE_FLG"].Value);
                            if (shukkinKeshikomi.DELETE_FLG)
                            {
                                shukkinKeshikomi.KESHIKOMI_GAKU = 0;
                            }
                        }

                        this.kesikomiDao.Insert(shukkinKeshikomi);
                    }

                    // コミット
                    tran.Commit();
                }
                if (!this.IchiranSearch())
                {
                    return;
                }

                // 完了メッセージ表示
                msgLogic.MessageBoxShow("I001", "登録");
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

        /// <summary>
        /// 取引先Validating処理
        /// </summary>
        public void TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                M_TORIHIKISAKI torihikisaki = new M_TORIHIKISAKI();
                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_CD.Text))
                {
                    torihikisaki.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    torihikisaki.ISNOT_NEED_DELETE_FLG = true;
                    IM_TORIHIKISAKIDao torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                    var result = torihikisakiDao.GetAllValidData(torihikisaki).FirstOrDefault();
                    if (result == null || !this.CheckTorihikisakiTekiyou(result))
                    {
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        CustomControlExtLogic.UpdateBackColor(this.form.TORIHIKISAKI_CD,false);
                        msgLogic.MessageBoxShow("E020", "取引先");
                        e.Cancel = true;
                    }
                    else
                    {
                        var torihikiShiharai = new M_TORIHIKISAKI_SHIHARAI();
                        IM_TORIHIKISAKI_SHIHARAIDao torihikiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                        torihikiShiharai = torihikiSeikyuuDao.GetDataByCd(torihikisaki.TORIHIKISAKI_CD);
                        if (torihikiShiharai == null || string.IsNullOrEmpty(torihikiShiharai.TORIHIKISAKI_CD) || torihikiShiharai.TORIHIKI_KBN_CD != 2)
                        {
                            this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                            this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                            CustomControlExtLogic.UpdateBackColor(this.form.TORIHIKISAKI_CD, false);
                            msgLogic.MessageBoxShow("E226", "");
                            e.Cancel = true;
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_NAME.Text = result.TORIHIKISAKI_NAME_RYAKU;
                        }
                    }
                }
                else
                {
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validating", ex1);
                this.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validating", ex);
                this.errmessage.MessageBoxShow("E245", "");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool CheckTorihikisakiTekiyou(M_TORIHIKISAKI entity)
        {
            bool validFlg = false;
            var sesanFrom = SqlDateTime.MinValue;
            var sesanTo = SqlDateTime.MaxValue;
            if (entity.TEKIYOU_BEGIN.IsNull)
            {
                entity.TEKIYOU_BEGIN = SqlDateTime.MinValue;
            }
            if (entity.TEKIYOU_END.IsNull)
            {
                entity.TEKIYOU_END = SqlDateTime.MaxValue;
            }

            if (!string.IsNullOrEmpty(this.form.SEISAN_DATE_FROM.Text))
            {
                sesanFrom = (DateTime)this.form.SEISAN_DATE_FROM.Value;
            }
            if (!string.IsNullOrEmpty(this.form.SEISAN_DATE_TO.Text))
            {
                sesanTo = (DateTime)this.form.SEISAN_DATE_TO.Value;
            }
            if ((sesanFrom.CompareTo(entity.TEKIYOU_BEGIN) >= 0 && sesanFrom.CompareTo(entity.TEKIYOU_END) <= 0)
                || (sesanTo.CompareTo(entity.TEKIYOU_BEGIN) >= 0 && sesanTo.CompareTo(entity.TEKIYOU_END) <= 0)
                || (sesanFrom.CompareTo(entity.TEKIYOU_BEGIN) <= 0 && sesanTo.CompareTo(entity.TEKIYOU_END) >= 0))
            {
                validFlg = true;
            }
            return validFlg;
        }

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.SEISAN_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.SEISAN_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.SEISAN_DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.SEISAN_DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.SEISAN_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.SEISAN_DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.SEISAN_DATE_FROM.IsInputErrorOccured = true;
                this.form.SEISAN_DATE_TO.IsInputErrorOccured = true;
                this.form.SEISAN_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.SEISAN_DATE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                string[] errorMsg = { "精算日付From", "精算日付To" };
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.SEISAN_DATE_FROM.Focus();
                return true;
            }
            return false;
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
            ret = accessor.createSystemId((int)DENSHU_KBN.SHUKKIN);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #endregion

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