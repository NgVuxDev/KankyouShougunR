// $Id:
using System;
using System.Linq;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Carriage.UnchinMeisaihyou.DAO;
using Shougun.Core.Carriage.UnchinMeisaihyouDto;
using System.Data;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Windows.Forms;

namespace Shougun.Core.Carriage.UnchinMeisaihyou
{
    #region - Class -

    #region - LogicClass -

    /// <summary>
    /// G642 運賃明細表 ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド</summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.Carriage.UnchinMeisaihyou.Setting.ButtonSetting.xml";

        /// <summary>フォーム</summary>
        private UIForm form;
        private HeaderBaseForm header;

        /// <summary>IM_GYOUSHADao</summary>
        private r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>IM_SHAINDao</summary>
        public r_framework.Dao.IM_SHAINDao shainDao;

        private Format format;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="UriageShiharaiMeisaihyouLogicClass"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();

            this.format = Format.CreateFormat();

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>画面初期化処理</summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                var msgBox = new MessageBoxShowLogic();
                msgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        #region - Function Key Proc -

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public void Search(DtoClass dto)
        {
            // とりあえず固定のクエリでデータ取ってくるだけ
            var dao = DaoInitUtility.GetComponent<IUnchinMeisaihyouDao>();
            var dt = dao.GetUnchinMeisaiData(dto);
            if (0 < dt.Rows.Count)
            {
                switch (dto.Order)
                {
                    // 1.運搬業者CD順
                    case 1:
                    // 2.フリガナ順
                    case 2:
                        var unpanGyoushaReport = new UnchinMeisaihyouUnpanGyoushaReportClass();
                        unpanGyoushaReport.CreateReport(dt, dto, this.format);
                        break;
                    // 3.伝票日付順
                    case 3:
                        var denpyouDateReport = new UnchinMeisaihyouDenpyouDateReportClass();
                        denpyouDateReport.CreateReport(dt, dto, this.format);
                        break;
                    // 4.伝票番号順
                    case 4:
                        var denpyouNumberReport = new UnchinMeisaihyouDenpyouNumberReportClass();
                        denpyouNumberReport.CreateReport(dt, dto, this.format);
                        break;
                }
            }
            else
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C001");
            }
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
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        public int Search()
        {
            throw new NotImplementedException();
        }

        #endregion - Function Key Proc -

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

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
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                // 明細項目CSVボタン(F5)イベント生成
                this.form.C_Regist(parentForm.bt_func5);
                parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

                // 明細項目表示ボタン(F7)イベント生成
                this.form.C_Regist(parentForm.bt_func7);
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 業者CDの最大値と最小値をセット(入力が無い場合)
        /// </summary>
        internal void SetGyoushaCdFromTo()
        {
            LogUtility.DebugMethodStart();

            var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var mGyoushaList = dao.GetAllValidData(new M_GYOUSHA());

            if (mGyoushaList.Count() > 0)
            {
                var minGyousha = mGyoushaList.Where(g => g.GYOUSHA_CD == mGyoushaList.Min(gy => gy.GYOUSHA_CD)).FirstOrDefault();
                var maxGyousha = mGyoushaList.OrderByDescending(m => m.GYOUSHA_CD).Where(g => g.GYOUSHA_CD == mGyoushaList.Max(gy => gy.GYOUSHA_CD)).FirstOrDefault();

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text))
                {
                    this.form.UNPAN_GYOUSHA_CD_FROM.Text = minGyousha.GYOUSHA_CD;
                    this.form.UNPAN_GYOUSHA_NAME_FROM.Text = minGyousha.GYOUSHA_NAME_RYAKU;
                }

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text))
                {
                    this.form.UNPAN_GYOUSHA_CD_TO.Text = maxGyousha.GYOUSHA_CD;
                    this.form.UNPAN_GYOUSHA_NAME_TO.Text = maxGyousha.GYOUSHA_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool CheckDate(out bool catchErr)
        {
            bool ret = false;
            catchErr = false;
            MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart();
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;

                    if (this.form.customRadioButtonHiduke1.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.customRadioButtonHiduke2.Checked)
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }

                    this.form.HIDUKE_FROM.Focus();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                msglogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }

            return ret;
        }

        /// <summary>
        /// CSV出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public void CSVPrint(DtoClass dto)
        {
            // とりあえず固定のクエリでデータ取ってくるだけ
            var dao = DaoInitUtility.GetComponent<IUnchinMeisaihyouDao>();
            var dt = dao.GetUnchinMeisaiData(dto);

            string headStr = "";

            switch (dto.Order)
            {
                case 1:
                    headStr = "運搬業者CD,運搬業者,伝票日付,伝種区分,車種CD,車種,車輌CD,車輌,運転者,運賃品名CD,運賃品名,正味重量,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 2:
                    headStr = "運搬業者CD,運搬業者,伝票日付,伝種区分,車種CD,車種,車輌CD,車輌,運転者,運賃品名CD,運賃品名,正味重量,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 3:
                    headStr = "伝票日付,伝種区分,運搬業者CD,運搬業者,車種CD,車種,車輌CD,車輌,運転者,運賃品名CD,運賃品名,正味重量,数量,単位CD,単位,単価,金額,伝票番号";
                    break;
                case 4:
                    headStr = "伝種区分,伝票番号,運搬業者CD,運搬業者,伝票日付,車種CD,車種,車輌CD,車輌,運転者,運賃品名CD,運賃品名,正味重量,数量,単位CD,単位,単価,金額";
                    break;
            }

            string[] csvHead;
            csvHead = headStr.Split(',');

            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rowTmp = csvDT.NewRow();

                if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                {
                    rowTmp["伝票日付"] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
                }

                if (dt.Rows[i]["DENSHU_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENSHU_KBN_NAME_RYAKU"].ToString()))
                {
                    rowTmp["伝種区分"] = dt.Rows[i]["DENSHU_KBN_NAME_RYAKU"].ToString();
                }

                if (dt.Rows[i]["UNPAN_GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNPAN_GYOUSHA_CD"].ToString()))
                {
                    rowTmp["運搬業者CD"] = dt.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                }
                if (dt.Rows[i]["UNPAN_GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString()))
                {
                    rowTmp["運搬業者"] = dt.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["SHASHU_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHASHU_CD"].ToString()))
                {
                    rowTmp["車種CD"] = dt.Rows[i]["SHASHU_CD"].ToString();
                }

                if (dt.Rows[i]["SHASHU_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHASHU_NAME_RYAKU"].ToString()))
                {
                    rowTmp["車種"] = dt.Rows[i]["SHASHU_NAME_RYAKU"].ToString();
                }

                if (dt.Rows[i]["SHARYOU_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHARYOU_CD"].ToString()))
                {
                    rowTmp["車輌CD"] = dt.Rows[i]["SHARYOU_CD"].ToString();
                }

                if (dt.Rows[i]["SHARYOU_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHARYOU_NAME_RYAKU"].ToString()))
                {
                    rowTmp["車輌"] = dt.Rows[i]["SHARYOU_NAME_RYAKU"].ToString();
                }

                if (dt.Rows[i]["UNTENSHA_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNTENSHA_NAME"].ToString()))
                {
                    rowTmp["運転者"] = dt.Rows[i]["UNTENSHA_NAME"].ToString();
                }

                if (dt.Rows[i]["UNCHIN_HINMEI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNCHIN_HINMEI_CD"].ToString()))
                {
                    rowTmp["運賃品名CD"] = dt.Rows[i]["UNCHIN_HINMEI_CD"].ToString();
                }

                if (dt.Rows[i]["UNCHIN_HINMEI_NAME"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNCHIN_HINMEI_NAME"].ToString()))
                {
                    rowTmp["運賃品名"] = dt.Rows[i]["UNCHIN_HINMEI_NAME"].ToString();
                }

                if (!Convert.IsDBNull(dt.Rows[i]["NET_JYUURYOU"]) && dt.Rows[i]["NET_JYUURYOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NET_JYUURYOU"].ToString()))
                {
                    var tmpNetJyuuryou = ReportInfo.ConvertNullOrEmptyToZero(dt.Rows[i]["NET_JYUURYOU"]);
                    rowTmp["正味重量"] = tmpNetJyuuryou.ToString(format.JYUURYOU_FORMAT);
                }

                if (!Convert.IsDBNull(dt.Rows[i]["SUURYOU"]) && dt.Rows[i]["SUURYOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SUURYOU"].ToString()))
                {
                    rowTmp["数量"] = ReportInfo.ConvertNullOrEmptyToZero(dt.Rows[i]["SUURYOU"]).ToString(format.SUURYOU_FORMAT);
                }

                if (dt.Rows[i]["UNIT_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNIT_CD"].ToString()))
                {
                    rowTmp["単位CD"] = dt.Rows[i]["UNIT_CD"].ToString();
                }

                if (dt.Rows[i]["UNIT_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["UNIT_NAME_RYAKU"].ToString()))
                {
                    rowTmp["単位"] = dt.Rows[i]["UNIT_NAME_RYAKU"].ToString();
                }

                if (dt.Rows[i]["TANKA"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TANKA"].ToString()))
                {
                    rowTmp["単価"] = ReportInfo.ConvertNullOrEmptyToZero(dt.Rows[i]["TANKA"]).ToString(format.TANKA_FORMAT); ;
                }

                if (!Convert.IsDBNull(dt.Rows[i]["KINGAKU"]) && dt.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["KINGAKU"].ToString()))
                {
                    var tmpKingaku = ReportInfo.ConvertNullOrEmptyToZero(dt.Rows[i]["KINGAKU"]);
                    rowTmp["金額"] = tmpKingaku.ToString(format.KINGAKU_FORMAT);
                }

                if (dt.Rows[i]["DENPYOU_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_NUMBER"].ToString()))
                {
                    rowTmp["伝票番号"] = dt.Rows[i]["DENPYOU_NUMBER"].ToString();
                }

                csvDT.Rows.Add(rowTmp);
            }

            var msgLogic = new MessageBoxShowLogic();
            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            if (csvDT.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("E044");
                return;
            }
            // 出力先指定のポップアップを表示させる。
            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();

                csvExport.ConvertDataTableToCsv(csvDT, true, true, "運賃明細表", this.form);
            }
        }

        #endregion - Methods -

        #region 内部クラス
        /// <summary>
        /// フォーマット
        /// </summary>
        internal class Format
        {
            internal readonly string SHARP_FORMAT_DECIMAL = "#,###.###";
            internal readonly string ZERO_FORMAT_DECIMAL = "#,##0.###";
            internal readonly string SHARP_FORMAT_INTEGER = "#,###";
            internal readonly string ZERO_FORMAT_INTEGER = "#,##0";

            internal bool TANKA_EMPTY_ZERO { get; private set; }
            internal bool SUURYOU_EMPTY_ZERO { get; private set; }
            internal bool JYUURYOU_EMPTY_ZERO { get; private set; }
            internal bool KINGAKU_EMPTY_ZERO { get; private set; }
            internal string TANKA_FORMAT { get; private set; }
            internal string SUURYOU_FORMAT { get; private set; }
            internal string JYUURYOU_FORMAT { get; private set; }
            internal string KINGAKU_FORMAT { get; private set; }

            private Format() { }

            static internal Format CreateFormat()
            {
                Format f = new Format();

                if (SystemProperty.Format.Tanka.Split('.')[0].EndsWith("#"))
                {
                    f.TANKA_EMPTY_ZERO = true;
                    f.TANKA_FORMAT = f.SHARP_FORMAT_DECIMAL;
                }
                else
                {
                    f.TANKA_EMPTY_ZERO = false;
                    f.TANKA_FORMAT = f.ZERO_FORMAT_DECIMAL;
                }

                if (SystemProperty.Format.Suuryou.Split('.')[0].EndsWith("#"))
                {
                    f.SUURYOU_EMPTY_ZERO = true;
                    f.SUURYOU_FORMAT = f.SHARP_FORMAT_DECIMAL;
                }
                else
                {
                    f.SUURYOU_EMPTY_ZERO = false;
                    f.SUURYOU_FORMAT = f.ZERO_FORMAT_DECIMAL;
                }
                if (SystemProperty.Format.Jyuryou.Split('.')[0].EndsWith("#"))
                {
                    f.JYUURYOU_EMPTY_ZERO = true;
                    f.JYUURYOU_FORMAT = f.SHARP_FORMAT_DECIMAL;
                }
                else
                {
                    f.JYUURYOU_EMPTY_ZERO = false;
                    f.JYUURYOU_FORMAT = f.ZERO_FORMAT_DECIMAL;
                }

                f.KINGAKU_EMPTY_ZERO = false;
                f.KINGAKU_FORMAT = f.ZERO_FORMAT_INTEGER;

                return f;
            }
        }
        #endregion
    }

    #endregion - LogicClass -

    #endregion - Class -
}
