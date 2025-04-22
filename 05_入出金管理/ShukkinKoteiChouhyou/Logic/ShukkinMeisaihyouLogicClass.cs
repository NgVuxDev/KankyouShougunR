using System;
using System.Data;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKoteiChouhyou
{
    /// <summary>
    /// 出金明細表出力画面ロジッククラス
    /// </summary>
    internal class ShukkinMeisaihyouLogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.ShukkinKoteiChouhyou.Setting.ShukkinMeisaihyouButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm_ShukkinMeisaihyou form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public ShukkinMeisaihyouLogicClass(UIForm_ShukkinMeisaihyou targetForm)
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
        /// CSV
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool CSV(ShukkinMeisaihyouDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                // とりあえず固定のクエリでデータ取ってくるだけ
                var dao = DaoInitUtility.GetComponent<IShukkinMeisaihyouDao>();
                var dt = dao.GetShukkinMeisaiData(dto);
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                string headStr = "";
                string[] csvHead;

                switch (Convert.ToInt16(this.form.SORT_1_KOUMOKU.Text))
                {
                    case 1:
                    case 2:
                        headStr = "取引先CD,取引先,伝票日付,出金番号,出金区分CD,出金区分,金額,備考";
                        break;
                    case 3:
                        headStr = "伝票日付,出金番号,取引先CD,取引先,出金区分CD,出金区分,金額,備考";
                        break;
                    case 4:
                        headStr = "出金番号,伝票日付,取引先CD,取引先,出金区分CD,出金区分,金額,備考";
                        break;
                    case 5:
                        headStr = "出金区分CD,出金区分,伝票日付,出金番号,取引先CD,取引先,金額,備考";
                        break;
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

                        if (dt.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                        {
                            rowTmp["取引先CD"] = dt.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }

                        if (dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["取引先"] = dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["DENPYOU_DATE"].ToString()))
                        {
                            rowTmp["伝票日付"] = DateTime.Parse(dt.Rows[i]["DENPYOU_DATE"].ToString()).ToString("yyyy/MM/dd");
                        }

                        if (dt.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["KINGAKU"].ToString()))
                        {
                            rowTmp["金額"] = Convert.ToDecimal(dt.Rows[i]["KINGAKU"].ToString()).ToString("#,##0");
                        }

                        if (dt.Rows[i]["NYUUSHUKKIN_KBN_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_CD"].ToString())
                            && dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["出金区分CD"] = Int32.Parse(dt.Rows[i]["NYUUSHUKKIN_KBN_CD"].ToString()).ToString("00");
                        }

                        if (dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["出金区分"] = dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["SHUKKIN_NUMBER"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHUKKIN_NUMBER"].ToString()))
                        {
                            rowTmp["出金番号"] = dt.Rows[i]["SHUKKIN_NUMBER"].ToString();
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
        public bool Search(ShukkinMeisaihyouDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                // とりあえず固定のクエリでデータ取ってくるだけ
                var dao = DaoInitUtility.GetComponent<IShukkinMeisaihyouDao>();
                var dt = dao.GetShukkinMeisaiData(dto);


                if (0 < dt.Rows.Count)
                {
                    var reportLogic = new ShukkinMeisaihyouReportClass();
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

        /// 20141023 Houkakou 「出金明細表」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
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
                    if (this.form.HIDUKE_SHURUI.Text.Equals("1"))
                    {
                        msgLogic.MessageBoxShow("E030", "伝票日付From", "伝票日付To");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E030", "入力日付From", "入力日付To");
                    }
                    this.form.HIDUKE_FROM.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }

            return false;
        }
        #endregion
        /// 20141023 Houkakou 「出金明細表」の日付チェックを追加する　end

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
