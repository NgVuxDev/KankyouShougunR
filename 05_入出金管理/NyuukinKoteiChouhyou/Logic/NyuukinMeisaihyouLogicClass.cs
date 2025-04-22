using System;
using System.Data;
using System.Linq;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou
{
    /// <summary>
    /// 入金明細表出力画面ロジッククラス
    /// </summary>
    internal class NyuukinMeisaihyouLogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou.Setting.NyuukinMeisaihyouButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm_NyuukinMeisaihyou form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public NyuukinMeisaihyouLogicClass(UIForm_NyuukinMeisaihyou targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

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
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
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

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool CSV(NyuukinMeisaihyouDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                var dao = DaoInitUtility.GetComponent<INyuukinMeisaihyouDao>();
                DataTable dt;
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                string headStr = "";
                string[] csvHead;

                if (dto.Sort1 == ConstClass.SORT_1_NYUUKINSAKI)
                {
                    switch (Convert.ToInt16(this.form.SORT_2_KOUMOKU.Text))
                    {
                        case 1:
                        case 2:
                            headStr = "入金先CD,入金先,伝票日付,入金番号,入金区分CD,入金区分,金額,備考";
                            break;
                        case 3:
                            headStr = "伝票日付,入金番号,入金先CD,入金先,入金区分CD,入金区分,金額,備考";
                            break;
                        case 4:
                            headStr = "入金番号,伝票日付,入金先CD,入金先,入金区分CD,入金区分,金額,備考";
                            break;
                        case 5:
                            headStr = "入金区分CD,入金区分,伝票日付,入金番号,入金先CD,入金先,金額,備考";
                            break;
                    }

                    dt = dao.GetNyuukinMeisaiDataForNyuukinsaki(dto);
                }
                else
                {
                    switch (Convert.ToInt16(this.form.SORT_2_KOUMOKU.Text))
                    {
                        case 1:
                        case 2:
                            headStr = "取引先CD,取引先,入金先CD,入金先,伝票日付,入金番号,入金区分CD,入金区分,金額,備考";
                            break;
                        case 3:
                            headStr = "伝票日付,入金番号,取引先CD,取引先,入金先CD,入金先,入金区分CD,入金区分,金額,備考";
                            break;
                        case 4:
                            headStr = "入金番号,伝票日付,取引先CD,取引先,入金先CD,入金先,入金区分CD,入金区分,金額,備考";
                            break;
                        case 5:
                            headStr = "入金区分CD,入金区分,伝票日付,入金番号,取引先CD,取引先,入金先CD,入金先,金額,備考";
                            break;
                    }

                    dt = dao.GetNyuukinMeisaiDataForTorihikisaki(dto);
                }

                if (0 < dt.Rows.Count)
                {
                	csvHead = headStr.Split(',');
                	for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowTmp = csvDT.NewRow();

                        if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
                        {
                            if (dt.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                            {
                                rowTmp["取引先CD"] = dt.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if (dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["取引先"] = dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                            }
                        }

                        if (dt.Rows[i]["NYUUKINSAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKINSAKI_CD"].ToString())
                            && dt.Rows[i]["NYUUKINSAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKINSAKI_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["入金先CD"] = dt.Rows[i]["NYUUKINSAKI_CD"].ToString();
                        }

                        if (dt.Rows[i]["NYUUKINSAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKINSAKI_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["入金先"] = dt.Rows[i]["NYUUKINSAKI_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                        {
                            rowTmp["伝票日付"] = DateTime.Parse(dt.Rows[i]["DENPYOU_DATE"].ToString()).ToString("yyyy/MM/dd");
                        }

                        if (dt.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["KINGAKU"].ToString()))
                        {
                            rowTmp["金額"] = Convert.ToDecimal(dt.Rows[i]["KINGAKU"].ToString()).ToString("#,##0");
                        }

                        if (dt.Rows[i]["NYUUSHUKKIN_KBN_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_CD"].ToString()))
                        {
                            rowTmp["入金区分CD"] = Int32.Parse(dt.Rows[i]["NYUUSHUKKIN_KBN_CD"].ToString()).ToString("00");
                        }

                        if (dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["入金区分"] = dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["NYUUKIN_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKIN_NUMBER"].ToString()))
                        {
                            rowTmp["入金番号"] = dt.Rows[i]["NYUUKIN_NUMBER"].ToString();
                        }

                        if (dt.Rows[i]["MEISAI_BIKOU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["MEISAI_BIKOU"].ToString()))
                        {
                            rowTmp["備考"] = dt.Rows[i]["MEISAI_BIKOU"].ToString();
                        }

                        csvDT.Rows.Add(rowTmp);
                    }

                    this.form.CsvReport(csvDT);
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Search(NyuukinMeisaihyouDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                var dao = DaoInitUtility.GetComponent<INyuukinMeisaihyouDao>();
                DataTable dt;
                if (dto.Sort1 == ConstClass.SORT_1_NYUUKINSAKI)
                {
                    dt = dao.GetNyuukinMeisaiDataForNyuukinsaki(dto);
                }
                else
                {
                    dt = dao.GetNyuukinMeisaiDataForTorihikisaki(dto);
                }

                if (0 < dt.Rows.Count)
                {
                    var reportLogic = new NyuukinMeisaihyouReportClass();
                    reportLogic.CreateReport(dt, dto);
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン設定を作成します
        /// </summary>
        /// <returns>ボタン設定</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] ret;

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ret = buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // CSVボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 明細項目表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店を取得します（支店が複数ある場合は、先頭の支店のみ。口座情報が必要な場合は使用しないこと。）
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店エンティティ</returns>
        internal M_BANK_SHITEN GetBankShiten(string bankCd, string bankShitenCd, out bool catchErr)
        {
            catchErr = true;
            LogUtility.DebugMethodStart(bankCd, bankShitenCd);

            M_BANK_SHITEN ret = null;
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                var mBankShitenList = dao.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd, ISNOT_NEED_DELETE_FLG = true});
                if (mBankShitenList.Count() > 0)
                {
                    ret = mBankShitenList.FirstOrDefault();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetBankShiten", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetBankShiten", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        // luning 20141023 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            try
            {
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                {
                    this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
                {
                    this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
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
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    if (this.form.HIDUKE_SHURUI_1.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    this.form.HIDUKE_FROM.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            return false;
        }
        #endregion
        // luning 20141023 「From　>　To」のアラート表示タイミング変更 end

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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
    }
}
