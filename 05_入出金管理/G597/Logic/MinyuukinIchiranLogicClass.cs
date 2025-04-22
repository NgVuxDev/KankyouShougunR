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

namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    /// <summary>
    /// 未入金一覧出力画面ロジッククラス
    /// </summary>
    internal class MinyuukinIchiranLogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.MinyuukinIchiran.Setting.MinyuukinIchiranButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm_MinyuukinIchiran form;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public MinyuukinIchiranLogicClass(UIForm_MinyuukinIchiran targetForm)
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
        public bool CSV(MinyuukinIchiranDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                var dao = DaoInitUtility.GetComponent<IMinyuukinIchiranDao>();
                bool checkSql = false;
                DataTable dt;
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                if (dto.Syosiki == ConstClass.SHOSHIKI_T)
                {
                    if (this.form.SORT_1_KOUMOKU.Text.Equals("1"))
                    {
                        string[] csvHead = { "営業担当者CD","営業担当者", "入金予定日", "未入金額", "取引先CD", "取引先", 
                                         "請求額", "入金額", "締日", "請求日", "回収方法"};
                        for (int i = 0; i < csvHead.Length; i++)
                        {
                            csvDT.Columns.Add(csvHead[i]);
                        }
                    }
                    else if (this.form.SORT_1_KOUMOKU.Text.Equals("2"))
                    {
                        string[] csvHead = { "取引先CD", "取引先",  "入金予定日", "未入金額", "営業担当者CD", "営業担当者", 
                                         "請求額", "入金額", "締日", "請求日", "回収方法"};
                        for (int i = 0; i < csvHead.Length; i++)
                        {
                            csvDT.Columns.Add(csvHead[i]);
                        }
                    }


                    dt = dao.GetMinyuukinIchiranData(dto);

                    if (0 < dt.Rows.Count)
                    {
                        checkSql = true;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
                else
                {
                    string[] csvHead = { "取引先CD", "取引先",  "業者CD", "業者", "現場CD", "現場", 
                                         "入金予定日", "未入金額",  "営業担当者CD", "営業担当者", "請求額", "入金額", 
                                         "締日", "請求日", "回収方法"};
                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }

                    dt = dao.GetMinyuukinIchiranData2(dto);

                    if (0 < dt.Rows.Count)
                    {
                        checkSql = true;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }

                if (checkSql == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowTmp = csvDT.NewRow();
                        if (dto.Syosiki != ConstClass.SHOSHIKI_T)
                        {
                            if (dt.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_CD"].ToString()))
                            {
                                rowTmp["業者CD"] = dt.Rows[i]["GYOUSHA_CD"].ToString();
                            }

                            if (dt.Rows[i]["GYOUSHA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["業者"] = dt.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                            }

                            if (dt.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_CD"].ToString()))
                            {
                                rowTmp["現場CD"] = dt.Rows[i]["GENBA_CD"].ToString();
                            }

                            if (dt.Rows[i]["GENBA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["GENBA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["現場"] = dt.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                            }
                        }

                        if (dt.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                        {
                            rowTmp["取引先CD"] = dt.Rows[i]["TORIHIKISAKI_CD"].ToString();
                        }

                        if (dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["取引先"] = dt.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["EIGYOUSHA_CD"] != null && !string.IsNullOrEmpty(dt.Rows[i]["EIGYOUSHA_CD"].ToString()))
                        {
                            rowTmp["営業担当者CD"] = dt.Rows[i]["EIGYOUSHA_CD"].ToString();
                        }

                        if (dt.Rows[i]["SHAIN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHAIN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["営業担当者"] = dt.Rows[i]["SHAIN_NAME_RYAKU"].ToString();
                        }

                        if (dt.Rows[i]["MINYUUKIN_GAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["MINYUUKIN_GAKU"].ToString()))
                        {
                            rowTmp["未入金額"] = Decimal.Parse(dt.Rows[i]["MINYUUKIN_GAKU"].ToString()).ToString("#,##0");
                        }

                        if (dt.Rows[i]["SEIKYUU_GAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SEIKYUU_GAKU"].ToString()))
                        {
                            rowTmp["請求額"] = Decimal.Parse(dt.Rows[i]["SEIKYUU_GAKU"].ToString()).ToString("#,##0");
                        }

                        if (dt.Rows[i]["NYUUKIN_GAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKIN_GAKU"].ToString()))
                        {
                            rowTmp["入金額"] = Decimal.Parse(dt.Rows[i]["NYUUKIN_GAKU"].ToString()).ToString("#,##0");
                        }

                        if (dt.Rows[i]["NYUUKIN_YOTEI_BI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUKIN_YOTEI_BI"].ToString()))
                        {
                            rowTmp["入金予定日"] = dt.Rows[i]["NYUUKIN_YOTEI_BI"].ToString();
                        }

                        if (dt.Rows[i]["SEIKYUU_DATE"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SEIKYUU_DATE"].ToString()))
                        {
                            rowTmp["請求日"] = dt.Rows[i]["SEIKYUU_DATE"].ToString();
                        }

                        if (dt.Rows[i]["SHIMEBI"] != null && !string.IsNullOrEmpty(dt.Rows[i]["SHIMEBI"].ToString()))
                        {
                            rowTmp["締日"] = dt.Rows[i]["SHIMEBI"].ToString();
                        }

                        if (dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString()))
                        {
                            rowTmp["回収方法"] = dt.Rows[i]["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                        }
                        csvDT.Rows.Add(rowTmp);
                    }
                    this.form.CsvReport(csvDT);
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
        public bool Search(MinyuukinIchiranDtoClass dto)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dto);

                var dao = DaoInitUtility.GetComponent<IMinyuukinIchiranDao>();
                DataTable dt;
                if (dto.Syosiki == ConstClass.SHOSHIKI_T)
                {
                    dt = dao.GetMinyuukinIchiranData(dto);

                    if (0 < dt.Rows.Count)
                    {
                        var reportLogic = new MinyuukinIchiranReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
                else
                {
                    dt = dao.GetMinyuukinIchiranData2(dto);

                    if (0 < dt.Rows.Count)
                    {
                        var reportLogic = new MinyuukinIchiranReportClass();
                        reportLogic.CreateReport2(dt, dto);
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
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

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 請求書書式の値変更イベント
        /// </summary>
        public void Syosiki_TextChanged()
        {
            LogUtility.DebugMethodStart();
            string syosiki = this.form.Syosiki.Text;
            if (syosiki == "2")
            {
                this.form.SORT_1_KOUMOKU.Text = "2";
                this.form.SORT_1_KOUMOKU_1.Enabled = false;
                //this.form.SORT_1_KOUMOKU.CharacterLimitList = new char[] { '2' };
                this.form.SORT_1_KOUMOKU.RangeSetting.Max = 2;
                this.form.SORT_1_KOUMOKU.RangeSetting.Min = 2;
            }
            else
            {
                this.form.SORT_1_KOUMOKU_1.Enabled = true;
                //this.form.SORT_1_KOUMOKU.CharacterLimitList = new char[] { '1', '2' };
                this.form.SORT_1_KOUMOKU.RangeSetting.Max = 2;
                this.form.SORT_1_KOUMOKU.RangeSetting.Min = 1;
            }
            LogUtility.DebugMethodEnd();
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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        internal decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = Decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}
