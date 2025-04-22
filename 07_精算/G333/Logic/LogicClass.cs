using System;
using System.Data;
using System.Reflection;
using System.Text;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Accessor;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Function.ShougunCSCommon.Dto;
using CommonChouhyouPopup.App;
using System.Collections.Generic;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 締処理エラーDAO
        /// </summary>
        private ITSSEDaoCls tsseDao;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 自社情報
        /// </summary>
        private IM_CORP_INFODao mCorpInfoDao;

        private GET_SYSDATEDao dao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string buttonInfoXmlPath = "Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ヘッダー
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName;

        /// <summary>
        /// 排他エラー判定フラグ(True:排他エラー)
        /// </summary>
        bool isUpdate = false;

        #endregion

        #region 初期化処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">メインフォーム</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.tsseDao = DaoInitUtility.GetComponent<ITSSEDaoCls>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                ButtonInit();

                // イベントの初期化
                EventInit();

                //ユーザ情報の登録
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                this.headerForm.txtKyotenCd.Text = String.Format("{0:D2}", int.Parse(this.form.kyotenCd));

                // ユーザ拠点名称の取得
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)kyotenDao.GetDataByCd(this.headerForm.txtKyotenCd.Text);
                if (mKyoten == null)
                {
                    this.headerForm.txtKyotenName.Text = "";
                }
                else
                {
                    this.headerForm.txtKyotenName.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }

                // 自社名を取得
                corpName = SelectCorpName();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region ボタン初期化
        /// <summary>
        /// ボタン初期化
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = CreateButtonInfo();
            var parentForm = (BusinessBaseForm)form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, form.WindowType);

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

            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //「印刷」ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new System.EventHandler(Print);

            //「CSV」ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new System.EventHandler(CSVPrint);

            //「締処理続行」ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new System.EventHandler(ShimeShoriContinue);

            //「締処理中止」ボタン(F12)イベント生成
            parentForm.bt_func12.Click += new System.EventHandler(FormClose);

            this.form.ShiharaiShimeErrorItiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(CellImeMode);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ヘッダー設定
        /// <summary>
        /// ヘッダー設定
        /// </summary>
        public void setHeaderForm(UIHeader hs)
        {
            LogUtility.DebugMethodStart(hs);

            this.headerForm = hs;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion

        #region イベント

        #region 印刷（F6）処理
        /// <summary>
        /// 印刷（F6）処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Print(object sender, EventArgs e)
        {
            // 印刷対象データがなければ処理終了
            if (this.form.ShiharaiShimeErrorItiran.Rows.Count == 0)
            {
                return;
            }

            DataTable dt = new DataTable();

            dt.Columns.Add();

            string time = getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss");
            System.Text.StringBuilder sBuilder;
            foreach (DataGridViewRow row in this.form.ShiharaiShimeErrorItiran.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                sBuilder = new StringBuilder();

                sBuilder.Append("\"");
                sBuilder.Append("1-1");
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["CHECK_NAME"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["KYOTEN_NAME"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["SHIHARAI_DATE"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["DENPYOU_NUMBER"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["TORIHIKISAKI_CD"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["TORIHIKISAKI_NAME"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["GYO_NUMBER"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["ERROR_NAIYOU"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(row.Cells["RIYUU"].Value.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(corpName);
                sBuilder.Append("\",\"");
                sBuilder.Append(time);
                sBuilder.Append("\"");

                dr[0] = sBuilder.ToString();
                dt.Rows.Add(dr);
            }

            ReportInfoR474 report_r474 = new ReportInfoR474();

            report_r474.R474_Report(dt);

            // 印刷ポツプアップ画面表示
            using (FormReportPrintPopup report = new FormReportPrintPopup(report_r474))
            {
                report.ShowDialog();
                report.Dispose();
            }
        }
        #endregion

        #region CSV（F6）処理
        /// <summary>
        /// CSV（F6）処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CSVPrint(object sender, EventArgs e)
        {
            // CSV出力対象データがなければ処理終了
            if (this.form.ShiharaiShimeErrorItiran.Rows.Count == 0)
            {
                return;
            }

            string[] csvHead = { "区分", "拠点", "支払日付", "伝票番号", "取引先CD", "取引先", "明細番号", "エラー内容", "理由" };

            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }

            foreach (DataGridViewRow row in this.form.ShiharaiShimeErrorItiran.Rows)
            {
                rowTmp = csvDT.NewRow();

                if (row.Cells["CHECK_NAME"].Value != null)
                {
                    rowTmp["区分"] = row.Cells["CHECK_NAME"].Value.ToString();
                }

                if (row.Cells["KYOTEN_NAME"].Value != null)
                {
                    rowTmp["拠点"] = row.Cells["KYOTEN_NAME"].Value.ToString();
                }

                if (row.Cells["SHIHARAI_DATE"].Value != null)
                {
                    rowTmp["支払日付"] = Convert.ToDateTime(row.Cells["SHIHARAI_DATE"].Value).ToString("yyyy/MM/dd");
                }

                if (row.Cells["DENPYOU_NUMBER"].Value != null)
                {
                    rowTmp["伝票番号"] = row.Cells["DENPYOU_NUMBER"].Value.ToString();
                }

                if (row.Cells["TORIHIKISAKI_CD"].Value != null)
                {
                    rowTmp["取引先CD"] = row.Cells["TORIHIKISAKI_CD"].Value.ToString();
                }

                if (row.Cells["TORIHIKISAKI_NAME"].Value != null)
                {
                    rowTmp["取引先"] = row.Cells["TORIHIKISAKI_NAME"].Value.ToString();
                }

                if (row.Cells["GYO_NUMBER"].Value != null)
                {
                    rowTmp["明細番号"] = row.Cells["GYO_NUMBER"].Value.ToString();
                }

                if (row.Cells["ERROR_NAIYOU"].Value != null)
                {
                    rowTmp["エラー内容"] = row.Cells["ERROR_NAIYOU"].Value.ToString();
                }

                if (row.Cells["RIYUU"].Value != null)
                {
                    rowTmp["理由"] = row.Cells["RIYUU"].Value.ToString();
                }

                csvDT.Rows.Add(rowTmp);
            }

            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            MsgBox = new MessageBoxShowLogic();
            if (csvDT.Rows.Count == 0)
            {
                MsgBox.MessageBoxShow("E044");
                return;
            }
            // 出力先指定のポップアップを表示させる。
            if (MsgBox.MessageBoxShow("C013") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToCsv(csvDT, true, true, "支払締処理エラー一覧表", this.form);
            }
        }
        #endregion

        #region 締処理続行（F9）処理
        /// <summary>
        /// 締処理続行（F9）処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShimeShoriContinue(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ShimeShori();
            if (!isUpdate)
            {
                this.form.ParamOut_Continue = true;
                parentForm.Close();
            }
        }
        #endregion

        #region 締処理中止andフォームクローズ（F12）処理
        /// <summary>
        /// 締処理中止andフォームクローズ（F12）処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ShimeShori();
            if (!isUpdate)
            {
                this.form.ParamOut_Continue = false;
                parentForm.Close();
            }
        }
        #endregion
        
        #region セル内のIMEモード設定
        /// <summary>
        /// セル内のIMEモード設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CellImeMode(object sender, EventArgs e)
        {
            this.form.ShiharaiShimeErrorItiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
        }
        #endregion

        #endregion

        #region Unused
        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        public void Regist(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region メソッド

        #region 重複チェック&整形
        /// <summary>
        /// 主キーが重複していないかチェックを行い、重複のないDataTableを作成します
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal DataTable CheckAndFormat(DataTable dt)
        {
            DataTable formatedTable = dt.Clone();
            List<string> checkList = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                // primary key columns
                string shoriKbn = row["SHORI_KBN"].ToString();
                string checkKbn = row["CHECK_KBN"].ToString();
                string denpyouShuruiCd = row["DENPYOU_SHURUI_CD"].ToString();
                string systemId = row["SYSTEM_ID"].ToString();
                string seq = row["SEQ"].ToString();
                string detailSystemId = row["DETAIL_SYSTEM_ID"].ToString();

                // check & create DataTable
                string checkString = shoriKbn + "_" + checkKbn + "_" + denpyouShuruiCd + "_" + systemId + "_" + seq + "_" + detailSystemId;
                if (checkList.Contains(checkString))
                {
                    // Nothing
                }
                else
                {
                    // Add DataTable & CheckList
                    formatedTable.ImportRow(row);
                    checkList.Add(checkString);
                }
            }

            return formatedTable;
        }
        #endregion

        #region データ一覧設定
        /// <summary>
        /// データ一覧設定
        /// </summary>
        internal bool SetIchiran(System.Data.DataTable dt)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(dt);

                if (!dt.Columns.Contains("CHECK_NAME"))
                {
                    dt.Columns.Add("CHECK_NAME", typeof(string));
                }

                if (!dt.Columns.Contains("KYOTEN_NAME"))
                {
                    dt.Columns.Add("KYOTEN_NAME", typeof(string));
                }

                if (!dt.Columns.Contains("TORIHIKISAKI_NAME"))
                {
                    dt.Columns.Add("TORIHIKISAKI_NAME", typeof(string));
                }
                if (!dt.Columns.Contains("TIME_STAMP"))
                {
                    dt.Columns.Add("TIME_STAMP", typeof(byte[]));
                }

                //データ用意
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //伝票種類から区分確定
                    if (dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString() == "1")
                    {
                        dt.Rows[i]["CHECK_NAME"] = "受入伝票";
                    }
                    else if (dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString() == "2")
                    {
                        dt.Rows[i]["CHECK_NAME"] = "出荷伝票";
                    }
                    else if (dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString() == "3")
                    {
                        dt.Rows[i]["CHECK_NAME"] = "売上/支払伝票";
                        if (dt.Columns.Contains("DAINOU_FLG"))
                        {
                            bool bol = false;
                            if (bool.TryParse(Convert.ToString(dt.Rows[i]["DAINOU_FLG"]), out bol))
                            {
                                if (bol)
                                {
                                    dt.Rows[i]["CHECK_NAME"] = "代納伝票";
                                }
                            }
                        }
                    }
                    else if (dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString() == "20")
                    {
                        dt.Rows[i]["CHECK_NAME"] = "出金伝票";
                    }
                    else
                    {
                        dt.Rows[i]["CHECK_NAME"] = "";
                    }

                    //拠点名称取得
                    M_KYOTEN mtKyoten = new M_KYOTEN();
                    mtKyoten = (M_KYOTEN)kyotenDao.GetDataByCd(dt.Rows[i]["KYOTEN_CD"].ToString());
                    if (mtKyoten == null)
                    {
                        dt.Rows[i]["KYOTEN_NAME"] = "";
                    }
                    else
                    {
                        dt.Rows[i]["KYOTEN_NAME"] = mtKyoten.KYOTEN_NAME_RYAKU;
                    }

                    // 取引先名称取得
                    M_TORIHIKISAKI mtTorihikisaki = new M_TORIHIKISAKI();
                    mtTorihikisaki = (M_TORIHIKISAKI)torihikisakiDao.GetDataByCd(dt.Rows[i]["TORIHIKISAKI_CD"].ToString());
                    if (mtTorihikisaki == null)
                    {
                        dt.Rows[i]["TORIHIKISAKI_NAME"] = "";
                    }
                    else
                    {
                        dt.Rows[i]["TORIHIKISAKI_NAME"] = mtTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }

                    //理由取得
                    T_SHIME_SHORI_ERROR tsse = new T_SHIME_SHORI_ERROR();
                    tsse.SHORI_KBN = SqlInt16.Parse(dt.Rows[i]["SHORI_KBN"].ToString());
                    tsse.CHECK_KBN = SqlInt16.Parse(dt.Rows[i]["CHECK_KBN"].ToString());
                    tsse.DENPYOU_SHURUI_CD = SqlInt16.Parse(dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString());
                    tsse.SYSTEM_ID = SqlInt64.Parse(dt.Rows[i]["SYSTEM_ID"].ToString());
                    tsse.SEQ = SqlInt32.Parse(dt.Rows[i]["SEQ"].ToString());
                    if (string.IsNullOrEmpty(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString()))
                    {
                        tsse.DETAIL_SYSTEM_ID = 0;
                    }
                    else
                    {
                        tsse.DETAIL_SYSTEM_ID = SqlInt64.Parse(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    }
                    tsse = (T_SHIME_SHORI_ERROR)tsseDao.GetDataForEntity(tsse);
                    if (tsse != null)
                    {
                        dt.Rows[i]["RIYUU"] = tsse.RIYUU;
                    }

                    //タイムスタンプ取得
                    T_SHIME_SHORI_ERROR tsse2 = new T_SHIME_SHORI_ERROR();
                    tsse2.SHORI_KBN = ConstInfo.SHORI_KBN_SHIHARAI;
                    tsse2.CHECK_KBN = SqlInt16.Parse(dt.Rows[i]["CHECK_KBN"].ToString());
                    tsse2.DENPYOU_SHURUI_CD = SqlInt16.Parse(dt.Rows[i]["DENPYOU_SHURUI_CD"].ToString());
                    tsse2.SYSTEM_ID = SqlInt64.Parse(dt.Rows[i]["SYSTEM_ID"].ToString());
                    tsse2.SEQ = SqlInt32.Parse(dt.Rows[i]["SEQ"].ToString());
                    if (string.IsNullOrEmpty(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString()))
                    {
                        tsse2.DETAIL_SYSTEM_ID = 0;
                    }
                    else
                    {
                        tsse2.DETAIL_SYSTEM_ID = SqlInt64.Parse(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    }
                    tsse2 = (T_SHIME_SHORI_ERROR)tsseDao.GetDataForEntity(tsse2);
                    if (tsse2 != null)
                    {
                        dt.Rows[i]["TIME_STAMP"] = tsse2.TIME_STAMP;
                    }

                }

                //前の結果をクリア
                int maxRows = this.form.ShiharaiShimeErrorItiran.Rows.Count;
                for (int i = maxRows; i >= 1; i--)
                {
                    this.form.ShiharaiShimeErrorItiran.Rows.RemoveAt(this.form.ShiharaiShimeErrorItiran.Rows[i - 1].Index);
                }

                //データ設定
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.form.ShiharaiShimeErrorItiran.Rows.Add();
                    DataGridViewRow crtRow = this.form.ShiharaiShimeErrorItiran.Rows[i];

                    //表示項目
                    //区分
                    crtRow.Cells["CHECK_NAME"].Value = dt.Rows[i]["CHECK_NAME"];
                    //拠点
                    crtRow.Cells["KYOTEN_NAME"].Value = dt.Rows[i]["KYOTEN_NAME"];
                    //日付
                    crtRow.Cells["SHIHARAI_DATE"].Value = dt.Rows[i]["SHIHARAI_DATE"];
                    //伝票番号
                    crtRow.Cells["DENPYOU_NUMBER"].Value = dt.Rows[i]["DENPYOU_NUMBER"];
                    //明細番号
                    if (string.IsNullOrEmpty(dt.Rows[i]["GYO_NUMBER"].ToString()))
                    {
                        crtRow.Cells["GYO_NUMBER"].Value = "";
                    }
                    else
                    {
                        crtRow.Cells["GYO_NUMBER"].Value = dt.Rows[i]["GYO_NUMBER"];
                    }
                    //エラー内容
                    crtRow.Cells["ERROR_NAIYOU"].Value = dt.Rows[i]["ERROR_NAIYOU"];
                    //理由
                    if (string.IsNullOrEmpty(dt.Rows[i]["RIYUU"].ToString()))
                    {
                        crtRow.Cells["RIYUU"].Value = "";
                    }
                    else
                    {
                        crtRow.Cells["RIYUU"].Value = dt.Rows[i]["RIYUU"];
                    }

                    //非表示項目
                    crtRow.Cells["DENPYOU_SHURUI_CD"].Value = dt.Rows[i]["DENPYOU_SHURUI_CD"];
                    crtRow.Cells["CHECK_KBN"].Value = dt.Rows[i]["CHECK_KBN"];
                    crtRow.Cells["SYSTEM_ID"].Value = dt.Rows[i]["SYSTEM_ID"];
                    crtRow.Cells["TORIHIKISAKI_CD"].Value = dt.Rows[i]["TORIHIKISAKI_CD"];
                    crtRow.Cells["KYOTEN_CD"].Value = dt.Rows[i]["KYOTEN_CD"];
                    crtRow.Cells["TORIHIKISAKI_NAME"].Value = dt.Rows[i]["TORIHIKISAKI_NAME"];
                    crtRow.Cells["SEQ"].Value = dt.Rows[i]["SEQ"];
                    crtRow.Cells["DETAIL_SYSTEM_ID"].Value = dt.Rows[i]["DETAIL_SYSTEM_ID"];
                    crtRow.Cells["TIME_STAMP"].Value = dt.Rows[i]["TIME_STAMP"];

                    crtRow.Cells["RIYUU"].ToolTipText = ConstInfo.RIYUU_HINT_TXT;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetIchiran", ex1);
                this.MsgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
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

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        internal void ShimeShori()
        {
            LogUtility.DebugMethodStart();

            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;

            List<T_SHIME_SHORI_ERROR> insertList = new List<T_SHIME_SHORI_ERROR>();
            List<T_SHIME_SHORI_ERROR> updateList = new List<T_SHIME_SHORI_ERROR>();

            // 排他エラー判定用フラグ
            isUpdate = false;

            for (int i = 0; i < this.form.ShiharaiShimeErrorItiran.Rows.Count; i++)
            {
                T_SHIME_SHORI_ERROR tsse = new T_SHIME_SHORI_ERROR();
                DataGridViewRow crtRow = this.form.ShiharaiShimeErrorItiran.Rows[i];

                //処理区分（固定値：2）
                tsse.SHORI_KBN = ConstInfo.SHORI_KBN_SHIHARAI;
                //チェック区分      非表示項目
                tsse.CHECK_KBN = SqlInt16.Parse(crtRow.Cells["CHECK_KBN"].Value.ToString());
                //伝票種類          非表示項目
                tsse.DENPYOU_SHURUI_CD = SqlInt16.Parse(crtRow.Cells["DENPYOU_SHURUI_CD"].Value.ToString());
                //システムID        非表示項目
                tsse.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID"].Value.ToString());
                //SEQ               非表示項目
                tsse.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ"].Value.ToString());
                //明細IDシステム    非表示項目
                if (string.IsNullOrEmpty(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                {
                    //nullの場合、0を設定
                    tsse.DETAIL_SYSTEM_ID = 0;
                }
                else
                {
                    tsse.DETAIL_SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                }

                //データ存在確認
                T_SHIME_SHORI_ERROR tsseForCheck = new T_SHIME_SHORI_ERROR();
                tsseForCheck = (T_SHIME_SHORI_ERROR)tsseDao.GetDataForEntity(tsse);

                if (tsseForCheck == null)
                {
                    //データが存在しない場合新規追加
                    T_SHIME_SHORI_ERROR tsseAdd = new T_SHIME_SHORI_ERROR();
                    tsseAdd.SHORI_KBN = ConstInfo.SHORI_KBN_SHIHARAI;
                    tsseAdd.CHECK_KBN = SqlInt16.Parse(crtRow.Cells["CHECK_KBN"].Value.ToString());
                    tsseAdd.DENPYOU_SHURUI_CD = SqlInt16.Parse(crtRow.Cells["DENPYOU_SHURUI_CD"].Value.ToString());
                    tsseAdd.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID"].Value.ToString());
                    tsseAdd.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ"].Value.ToString());
                    if (string.IsNullOrEmpty(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                    {
                        tsseAdd.DETAIL_SYSTEM_ID = 0;
                    }
                    else
                    {
                        tsseAdd.DETAIL_SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                    }
                    if (crtRow.Cells["GYO_NUMBER"].Value.ToString() == "0" || string.IsNullOrEmpty(crtRow.Cells["GYO_NUMBER"].Value.ToString()))
                    {
                        tsseAdd.GYO_NUMBER = SqlInt32.Null;
                    }
                    else
                    {
                        tsseAdd.GYO_NUMBER = SqlInt32.Parse(crtRow.Cells["GYO_NUMBER"].Value.ToString());
                    }
                    tsseAdd.ERROR_NAIYOU = crtRow.Cells["ERROR_NAIYOU"].Value.ToString();
                    tsseAdd.RIYUU = crtRow.Cells["RIYUU"].Value.ToString();
                    if (!string.IsNullOrEmpty(crtRow.Cells["TIME_STAMP"].Value.ToString()))
                    {
                        tsseAdd.TIME_STAMP = (byte[])crtRow.Cells["TIME_STAMP"].Value;
                    }

                    insertList.Add(tsseAdd);
                }
                else
                {
                    // 理由を設定
                    if (crtRow.Cells["RIYUU"].Value != null)
                    {
                        tsseForCheck.RIYUU = crtRow.Cells["RIYUU"].Value.ToString();
                    }
                    else
                    {
                        tsseForCheck.RIYUU = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(crtRow.Cells["TIME_STAMP"].Value.ToString()))
                    {
                        tsseForCheck.TIME_STAMP = (byte[])crtRow.Cells["TIME_STAMP"].Value;
                    }

                    updateList.Add(tsseForCheck);
                }

            }

            try
            {
                // トランザクション開始
                using (Transaction tran = new Transaction())
                {
                    // 追加実行
                    foreach (T_SHIME_SHORI_ERROR val in insertList)
                    {
                        var dataBinder1 = new DataBinderLogic<T_SHIME_SHORI_ERROR>(val);
                        dataBinder1.SetSystemProperty(val, false);
                        DaoInsert(val);
                    }

                    // 更新実行
                    foreach (T_SHIME_SHORI_ERROR val in updateList)
                    {
                        var dataBinder1 = new DataBinderLogic<T_SHIME_SHORI_ERROR>(val);
                        dataBinder1.SetSystemProperty(val, false);
                        DaoUpdate(val);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MsgBox = new MessageBoxShowLogic();
                    MsgBox.MessageBoxShow(ConstInfo.EX_ERR);
                    isUpdate = true;
                }
                else
                {
                    throw;
                }
            }

            //画面の保持するTIME_STAMPを更新するため、再取得
            foreach (DataGridViewRow crtRow in this.form.ShiharaiShimeErrorItiran.Rows)
            {
                T_SHIME_SHORI_ERROR tsse = new T_SHIME_SHORI_ERROR();
                //処理区分（固定値：2）
                tsse.SHORI_KBN = ConstInfo.SHORI_KBN_SHIHARAI;
                //チェック区分      非表示項目
                tsse.CHECK_KBN = SqlInt16.Parse(crtRow.Cells["CHECK_KBN"].Value.ToString());
                //伝票種類          非表示項目
                tsse.DENPYOU_SHURUI_CD = SqlInt16.Parse(crtRow.Cells["DENPYOU_SHURUI_CD"].Value.ToString());
                //システムID        非表示項目
                tsse.SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["SYSTEM_ID"].Value.ToString());
                //SEQ               非表示項目
                tsse.SEQ = SqlInt32.Parse(crtRow.Cells["SEQ"].Value.ToString());
                //明細IDシステム    非表示項目
                if (string.IsNullOrEmpty(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                {
                    //nullの場合、0を設定
                    tsse.DETAIL_SYSTEM_ID = 0;
                }
                else
                {
                    tsse.DETAIL_SYSTEM_ID = SqlInt64.Parse(crtRow.Cells["DETAIL_SYSTEM_ID"].Value.ToString());
                }

                tsse = (T_SHIME_SHORI_ERROR)tsseDao.GetDataForEntity(tsse);
                if (tsse != null && isUpdate)
                {
                    crtRow.Cells["RIYUU"].Value = tsse.RIYUU;
                    crtRow.Cells["TIME_STAMP"].Value = tsse.TIME_STAMP;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region テーブル新規追加
        /// <summary>
        /// テーブル新規追加
        /// </summary>
        /// <param name="daoTable">テーブルデータ</param>
        [Transaction]
        internal void DaoInsert(T_SHIME_SHORI_ERROR daoTable)
        {
            LogUtility.DebugMethodStart();
            tsseDao.Insert(daoTable);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region テーブル更新
        /// <summary>
        /// テーブル更新
        /// </summary>
        /// <param name="daoTable">テーブルデータ</param>
        [Transaction]
        internal void DaoUpdate(T_SHIME_SHORI_ERROR daoTable)
        {
            LogUtility.DebugMethodStart();
            tsseDao.Update(daoTable);
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 自社情報マスタテーブル会社名SELECT
        /// <summary>
        /// 自社情報マスタテーブル会社名SELECT
        /// </summary>
        /// <returns></returns>
        private String SelectCorpName()
        {
            M_CORP_INFO[] corpInfo;

            corpInfo = (M_CORP_INFO[])mCorpInfoDao.GetAllData();
            return corpInfo[0].CORP_RYAKU_NAME;
        }
        #endregion

        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        #endregion
    }
}
