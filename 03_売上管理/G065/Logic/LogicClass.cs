using System;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesManagement.UrikakekinItiranHyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 売掛金一覧表画面Form
        /// </summary>
        private UrikakekinItiranHyo.UIForm form;

        /// <summary>
        /// 範囲条件指定結果
        /// </summary>
        private Shougun.Core.Common.Kakepopup.Const.UIConstans.ConditionInfo param { get; set; }

        /// <summary>
        /// DBAccessor
        /// </summary>
        private Shougun.Core.SalesManagement.UrikakekinItiranHyo.Accessor.DBAccessor dba;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 明細用一覧DataTable
        /// </summary>
        private DataTable ichiranTable;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 範囲条件にて指定した範囲の全てのDataTable
        /// </summary>
        private DataTable allDataTable;

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName;

        /// <summary>
        /// RibbonForm
        /// </summary>
        private RibbonMainMenu ribbon;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dba = new Shougun.Core.SalesManagement.UrikakekinItiranHyo.Accessor.DBAccessor();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ParentFormのSet
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面表示項目初期化
                this.SetInitDisp();
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

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 印刷処理
        /// </summary>
        internal bool Print()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.UrikakekinItiranHyo.RowCount == 0)
                {
                    // データなし時はやらない
                    return ret;
                }

                decimal kurikoshigoukei = 0;
                decimal nyukingoukei = 0;
                decimal zeinukigoukei = 0;
                decimal shohizeigoukei = 0;
                decimal zeikomigoukei = 0;
                decimal sashihikigoukei = 0;

                // 取引先名称取得
                string startTorihikisaki = string.Empty;
                string endTorihikisaki = string.Empty;
                M_TORIHIKISAKI startTorihikisakiEntity = this.dba.TorihikisakiDao.GetDataByCd(param.StartTorihikisakiCD);
                M_TORIHIKISAKI endTorihikisakiEntity = this.dba.TorihikisakiDao.GetDataByCd(param.EndTorihikisakiCD);
                if (startTorihikisakiEntity != null)
                {
                    startTorihikisaki = startTorihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                }
                if (endTorihikisakiEntity != null)
                {
                    endTorihikisaki = endTorihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                }

                DataTable dt = new DataTable();

                dt.Columns.Add();

                System.Text.StringBuilder sBuilder;

                DataRow dr;

                dr = dt.NewRow();

                sBuilder = new StringBuilder();

                sBuilder.Append("\"");
                sBuilder.Append("1-1");
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.StartDay.ToString("yyyy/MM/dd"));
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.EndDay.ToString("yyyy/MM/dd"));
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.StartTorihikisakiCD);
                sBuilder.Append("\",\"");
                sBuilder.Append(this.param.EndTorihikisakiCD);
                sBuilder.Append("\",\"");
                sBuilder.Append(startTorihikisaki);
                sBuilder.Append("\",\"");
                sBuilder.Append(endTorihikisaki);
                sBuilder.Append("\",\"");
                sBuilder.Append(corpName);
                sBuilder.Append("\"");

                dr[0] = sBuilder.ToString();
                dt.Rows.Add(dr);

                foreach (DataGridViewRow row in this.form.UrikakekinItiranHyo.Rows)
                {
                    //DataRow dr;
                    dr = dt.NewRow();

                    sBuilder = new StringBuilder();

                    sBuilder.Append("\"");
                    sBuilder.Append("2-1");
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_TorihikisakiCD"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_TorihikisakiName"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_KurikosiZandaka"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_NyukinGaku"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_ZeinukiUriage"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_Shohizei"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_ZeikomiUriage"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_SashihikiUriageZandaka"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_Shimebi1"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_Shimebi2"].Value.ToString());
                    sBuilder.Append("\",\"");
                    sBuilder.Append(row.Cells["col_Shimebi3"].Value.ToString());
                    sBuilder.Append("\"");

                    dr[0] = sBuilder.ToString();
                    dt.Rows.Add(dr);

                    kurikoshigoukei += decimal.Parse(row.Cells["col_KurikosiZandaka"].Value.ToString());
                    nyukingoukei += decimal.Parse(row.Cells["col_NyukinGaku"].Value.ToString());
                    zeinukigoukei += decimal.Parse(row.Cells["col_ZeinukiUriage"].Value.ToString());
                    shohizeigoukei += decimal.Parse(row.Cells["col_Shohizei"].Value.ToString());
                    zeikomigoukei += decimal.Parse(row.Cells["col_ZeikomiUriage"].Value.ToString());
                    sashihikigoukei += decimal.Parse(row.Cells["col_SashihikiUriageZandaka"].Value.ToString());
                }

                dr = dt.NewRow();

                sBuilder = new StringBuilder();

                sBuilder.Append("\"");
                sBuilder.Append("2-2");
                sBuilder.Append("\",\"");
                sBuilder.Append(kurikoshigoukei.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(nyukingoukei.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(zeinukigoukei.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(shohizeigoukei.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(zeikomigoukei.ToString());
                sBuilder.Append("\",\"");
                sBuilder.Append(sashihikigoukei.ToString());
                sBuilder.Append("\"");

                dr[0] = sBuilder.ToString();
                dt.Rows.Add(dr);

                ReportInfoR361 report_r361 = new ReportInfoR361();

                report_r361.R361_Reprt(dt);

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r361))
                {
                    report.ReportCaption = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_URIGAKEKIN_ICHIRAN);
                    report.ShowDialog();
                    report.Dispose();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Print", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// CSV出力処理
        /// </summary>
        internal bool CSV()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();

                // 一覧に明細行がない場合
                if (this.form.UrikakekinItiranHyo.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    // CSV出力確認メッセージを表示する
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        // 共通部品を利用して、画面に表示されているデータをCSVに出力する
                        var CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.UrikakekinItiranHyo, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_URIGAKEKIN_ICHIRAN), this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 売掛金一覧表範囲条件指定画面表示処理
        /// </summary>
        /// <param name="type">画面種別</param>
        internal bool ShowPopUp(Shougun.Core.Common.Kakepopup.Const.UIConstans.DispType type)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(type);

                // ループフラグ
                Boolean searchFlg = true;

                // 受渡項目生成
                this.param = this.CreateParams(type);

                // 売上範囲条件指定画面表示
                var PopUpForm = new Shougun.Core.Common.Kakepopup.App.KakePopupForm(this.param);

                // 20150928 katen #12048 「システム日付」の基準作成、適用 start
                PopUpForm.sysDate = this.parentForm.sysDate;
                // 20150928 katen #12048 「システム日付」の基準作成、適用 end

                while (searchFlg)
                {
                    PopUpForm.ShowDialog();

                    // 実行結果
                    switch (PopUpForm.DialogResult)
                    {
                        case DialogResult.OK:		// 「F9:検索実行」押下
                            // 結果を次回条件値に保存
                            this.param = PopUpForm.Joken;

                            // 指定された範囲における全てのデータ取得
                            Cursor preCursor = Cursor.Current;
                            Cursor.Current = Cursors.WaitCursor;
                            bool hadSearched = false;
                            this.allDataTable = this.dba.GetIchiranData(this.param, this.ribbon.GlobalCommonInformation, this.form, ref hadSearched);
                            Cursor.Current = preCursor;

                            if (this.allDataTable.Rows.Count == 0)
                            {
                                // 開放 No.4182-->
                                PopUpForm.Dispose();
                                // 売上範囲条件指定画面表示
                                PopUpForm = new Shougun.Core.Common.Kakepopup.App.KakePopupForm(this.param);
                                // 20150928 katen #12048 「システム日付」の基準作成、適用 start
                                PopUpForm.sysDate = this.parentForm.sysDate;
                                // 20150928 katen #12048 「システム日付」の基準作成、適用 end
                                // No.4182<--
                                // 画面表示初期化
                                this.SetInitDisp();

                                // 検索完了時のみエラー表示
                                if (hadSearched)
                                {
                                    // 検索エラーメッセージ
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("C001");
                                }
                            }
                            else
                            {
                                searchFlg = false;
                                // 表示項目更新
                                this.headerForm.dtp_DenpyoDateFrom.Value = this.param.StartDay;
                                this.headerForm.dtp_DenpyoDateTo.Value = this.param.EndDay;

                                // 一覧画面更新
                                this.SetIchiran();
                            }
                            break;

                        case DialogResult.Cancel:	// 「F12:キャンセル」押下
                            // 何もしない
                            searchFlg = false;
                            break;

                        default:
                            searchFlg = false;
                            break;
                    }
                }

                // 開放
                PopUpForm.Dispose();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ShowPopUp", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowPopUp", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            this.form.UrikakekinItiranHyo.IsBrowsePurpose = false;

            int k = this.form.UrikakekinItiranHyo.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.UrikakekinItiranHyo.Rows.RemoveAt(this.form.UrikakekinItiranHyo.Rows[i - 1].Index);
            }

            DataView dv = new DataView();
            this.allDataTable.TableName = "売掛金一覧表";
            dv.Table = this.allDataTable;

            //出力区分により、ソート項目を変更する
            if (this.param.OutPutKBN == 2)
            {
                dv.Sort = "SASHIHIKI_URIAGE_ZANDAKA DESC";
            }
            else
            {
                dv.Sort = "TORIHIKISAKI_CD ASC";
                // No.4182-->コメントアウト
                //出力区分が3(締日別)の場合は、検索画面の締日に入力された締日と一致する取引先のデータのみを表示する
                //if (this.param.OutPutKBN == 3 && !string.IsNullOrEmpty(this.param.Shimebi))
                //{
                //    dv.RowFilter = "SHIMEBI1 = " + this.param.Shimebi + " OR SHIMEBI2 = " + this.param.Shimebi + " OR SHIMEBI3 = " + this.param.Shimebi;
                //}
                // No.4182<--
            }

            //フィルタ結果からデータテーブルを作成
            DataTable aftsorttable;
            aftsorttable = dv.ToTable();

            // データ件数をセット
            this.headerForm.txt_YomikomiDataKensu.Text = aftsorttable.Rows.Count.ToString();

            // 取得した一覧データをDataSourceにセット
            for (int i = 0; i < aftsorttable.Rows.Count; i++)
            {
                this.form.UrikakekinItiranHyo.Rows.Add();
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_TorihikisakiCD"].Value = aftsorttable.Rows[i]["TORIHIKISAKI_CD"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_TorihikisakiName"].Value = aftsorttable.Rows[i]["TORIHIKISAKI_NAME"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_KurikosiZandaka"].Value = aftsorttable.Rows[i]["KURIKOSHI_ZANDAKA"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_NyukinGaku"].Value = aftsorttable.Rows[i]["NYUKINGAKU"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_ZeinukiUriage"].Value = aftsorttable.Rows[i]["ZEINUKI_URIAGE"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shohizei"].Value = aftsorttable.Rows[i]["SHOHIZEI"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_ZeikomiUriage"].Value = aftsorttable.Rows[i]["ZEIKOMI_URIAGE"];
                this.form.UrikakekinItiranHyo.Rows[i].Cells["col_SashihikiUriageZandaka"].Value = aftsorttable.Rows[i]["SASHIHIKI_URIAGE_ZANDAKA"];
                if (SqlInt16.Parse(aftsorttable.Rows[i]["SHIMEBI1"].ToString()).IsNull == true)
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi1"].Value = string.Empty;
                }
                else
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi1"].Value = aftsorttable.Rows[i]["SHIMEBI1"];
                }
                if (SqlInt16.Parse(aftsorttable.Rows[i]["SHIMEBI2"].ToString()).IsNull == true)
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi2"].Value = string.Empty;
                }
                else
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi2"].Value = aftsorttable.Rows[i]["SHIMEBI2"];
                }
                if (SqlInt16.Parse(aftsorttable.Rows[i]["SHIMEBI3"].ToString()).IsNull == true)
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi3"].Value = string.Empty;
                }
                else
                {
                    this.form.UrikakekinItiranHyo.Rows[i].Cells["col_Shimebi3"].Value = aftsorttable.Rows[i]["SHIMEBI3"];
                }
            }

            this.form.UrikakekinItiranHyo.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        #region private

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
        /// <returns name="ButtonSetting[]">XMLに記載されたButtonのリスト</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.UIConstans.ButtonInfoXmlPath);
            return tmp;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // 印刷ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Print);

            // CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            // 検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受渡項目生成
        /// </summary>
        /// <param name="type">画面種別</param>
        /// <returns name="ConditionInfo">範囲条件情報</returns>
        private Shougun.Core.Common.Kakepopup.Const.UIConstans.ConditionInfo CreateParams(Shougun.Core.Common.Kakepopup.Const.UIConstans.DispType type)
        {
            var info = new Shougun.Core.Common.Kakepopup.Const.UIConstans.ConditionInfo();
            if (this.param.DataSetFlag == false)
            {
                // 初期設定条件を設定
                info.ShowDisplay = type;					// 呼び出し画面
                info.OutPutKBN = 1;							// 出力区分
                //info.Shimebi = string.Empty;				// 締日          // No.4182
                info.TyusyutsuHouhou = 1;					// 抽出方法
                info.StartDay = this.parentForm.sysDate;	// 開始日付
                info.EndDay = this.parentForm.sysDate;		// 終了日付
                info.StartTorihikisakiCD = string.Empty;	// 開始取引先CD
                info.EndTorihikisakiCD = string.Empty;		// 終了取引先CD
                info.DataSetFlag = false;					// 値格納フラグ
            }
            else
            {
                // 前回値のまま
                info = this.param;
            }

            return info;
        }

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            // 画面表示初期化
            this.headerForm.dtp_DenpyoDateFrom.Value = null;
            this.headerForm.dtp_DenpyoDateTo.Value = null;
            this.headerForm.txt_YomikomiDataKensu.Text = "0";
            this.parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("売掛金一覧表");
            this.parentForm.lb_hint.Text = "";
            this.form.UrikakekinItiranHyo.Rows.Clear();
            this.ichiranTable = null;
            this.form.UrikakekinItiranHyo.DataSource = this.ichiranTable;
            // 自社名を取得
            corpName = this.dba.SelectCorpName();
        }

        #endregion

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        //
        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}