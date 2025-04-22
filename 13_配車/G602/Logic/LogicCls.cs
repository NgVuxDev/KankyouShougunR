using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Allocation.TeikiJissekiHoukoku.Accessor;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Message;
using Shougun.Core.Allocation.TeikiJissekiHoukoku.Dto;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku
{
	/// <summary>
	/// ビジネスロジック
	/// </summary>
	internal class LogicCls : IBuisinessLogic
	{
		#region フィールド

		/// <summary>
		/// UIForm
		/// </summary>
		private UIForm form;

		/// <summary>
		/// ベースフォーム
		/// </summary>
		private BusinessBaseForm parentForm;

		/// <summary>
		/// DBアクセサ
		/// </summary>
		internal DBAccessor accessor;

		/// <summary>
		/// 検索条件
		/// </summary>
		internal TeikiJissekiHoukokuDto SearchCondition = new TeikiJissekiHoukokuDto();

		/// <summary>
		/// 検索結果
		/// </summary>
		internal TeikiJissekiHoukokuDto[] SearchResult = new TeikiJissekiHoukokuDto[0] { };

        /// <summary>
        /// 出力レイアウト
        /// </summary>
        internal List<DataGridViewRow> OutputLayoutRows = new List<DataGridViewRow>();

        /// <summary>
        /// 出力行データ
        /// </summary>
        internal List<string> OutputLine = new List<string>();

		/// <summary>
		/// ボタン設定格納ファイル
		/// </summary>
		private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiJissekiHoukoku.Setting.ButtonSetting.xml";

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private r_framework.Dao.GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic MsgBox;
		#endregion

		#region Constructor
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public LogicCls(UIForm targetForm)
		{
			try
			{
				LogUtility.DebugMethodStart(targetForm);

				// フォーム
				this.form = targetForm;

				// DBアクセサ初期化
                this.accessor = new DBAccessor();

                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.dateDao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.MsgBox = new MessageBoxShowLogic();
			}
			catch (Exception ex)
			{
				LogUtility.Error(ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd();
			}
		}
		#endregion Constructor

		#region 画面初期化処理
		/// <summary>
		/// 画面初期化処理
		/// </summary>
		public void WindowInit()
		{
			try
			{
				LogUtility.DebugMethodStart();

				// 親フォーム
				this.parentForm = this.form.Parent as BusinessBaseForm;

				// 画面初期表示設定
				this.InitializeScreen();

				// ボタンのテキストを初期化
				this.ButtonInit();

				// イベントの初期化処理
				this.EventInit();

				// サブファンクション非表示
				this.parentForm.ProcessButtonPanel.Visible = false;

				// 拠点CD
				var userProfile = CurrentUserCustomConfigProfile.Load();
				var strKyotenCd = this.GetUserProfileValue(userProfile, "拠点CD");
				M_KYOTEN kyoten = null;
				if (!string.IsNullOrEmpty(strKyotenCd))
				{
					kyoten = this.accessor.GetKyoten(short.Parse(strKyotenCd));
				}

				// 拠点名称
				if (kyoten != null)
				{
					this.form.KYOTEN_CD.Text = strKyotenCd.PadLeft(this.form.KYOTEN_CD.MaxLength, '0');
					this.form.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;
				}
				else
				{
					// 拠点CD、拠点 : ブランク
					this.form.KYOTEN_CD.Text = string.Empty;
					this.form.KYOTEN_NAME.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
			}
			finally
			{
				LogUtility.DebugMethodEnd();
			}
		}

		/// <summary>
		/// 画面初期表示設定
		/// </summary>
		private void InitializeScreen()
		{
            var date = parentForm.sysDate.Date;

			//「期間From」／システム日付けの前月1日
			this.form.KIKAN_FROM.Value = new DateTime(date.Year, date.Month, 1).AddMonths(-1);

			//「期間To」／システム日付けの前月末日
			this.form.KIKAN_TO.Value = new DateTime(date.Year, date.Month, 1).AddDays(-1);

            // 期間
            this.form.KIKAN_KBN.Text = "1";

            // 集計区分
            this.form.SHUUKEI_KBN.Text = "1";

			// 集計対象数量
			this.form.SHUUKEISYUURYOU.Text = "1";

			// 一覧非表示
			this.form.DGV_TEIKI_JISSEKI_HOUKOKU.Visible = false;

            this.form.KIKAN_FROM.Enabled = true;
            this.form.KIKAN_TO.Enabled = true;
		}

		/// <summary>
		/// ボタンの初期化処理
		/// </summary>
		private void ButtonInit()
		{
			var buttonSetting = this.CreateButtonInfo();
			var parentForm = (BusinessBaseForm)this.form.Parent;
			ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
		}

		/// <summary>
		/// ボタン情報の設定を行う
		/// </summary>
		private ButtonSetting[] CreateButtonInfo()
		{
			var buttonSetting = new ButtonSetting();
			var thisAssembly = Assembly.GetExecutingAssembly();
			return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
		}

		/// <summary>
		/// ボタンイベント処理の初期化を行う
		/// </summary>
		private void EventInit()
        {
            // 「F1:前月」イベント生成
            parentForm.bt_func1.Enabled = false;
            this.parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);

            // 「F2:後月」イベント生成
            parentForm.bt_func2.Enabled = false;
            this.parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);

			// 「F6:CSV出力ボタン」イベント生成
			this.form.C_Regist(parentForm.bt_func6);
			this.parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);

			// 「F12:閉じるボタン」イベント生成
			this.parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);

			// 登録時チェックイベント生成
			//this.form.UserRegistCheck += new SuperForm.UserRegistCheckHandler(this.form.CheckRegist);

			// 一覧セルフォーマットイベント生成
			this.form.DGV_TEIKI_JISSEKI_HOUKOKU.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DGV_TEIKI_JISSEKI_HOUKOKU_CellFormatting);
		}

		/// <summary>
		/// 一覧セルフォーマット処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DGV_TEIKI_JISSEKI_HOUKOKU_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
            if (this.form.SHUUKEI_KBN_1.Checked && ConstCls.COL_INDEX_SUURYOU_START_1 <= e.ColumnIndex && !(e.Value is string))
			{
				e.CellStyle.Format = r_framework.Dto.SystemProperty.Format.Suuryou;
				e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (this.form.SHUUKEI_KBN_2.Checked && ConstCls.COL_INDEX_SUURYOU_START_2 <= e.ColumnIndex && !(e.Value is string))
            {
                e.CellStyle.Format = r_framework.Dto.SystemProperty.Format.Suuryou;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (this.form.SHUUKEI_KBN_3.Checked && ConstCls.COL_INDEX_SUURYOU_START_3 <= e.ColumnIndex && !(e.Value is string))
            {
                e.CellStyle.Format = r_framework.Dto.SystemProperty.Format.Suuryou;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
		}

		/// <summary>
		/// ユーザー定義情報取得処理
		/// </summary>
		/// <param name="profile"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
		{
			LogUtility.DebugMethodStart(profile, key);

			string result = string.Empty;

			foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
			{
				if (item.Name.Equals(key))
				{
					result = item.Value;
				}
			}

			LogUtility.DebugMethodEnd(result);
			return result;
		}

		#endregion

        #region 前月ボタン押下処理
        /// <summary>
        /// 前月ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtpFrom = DateTime.Parse(form.KIKAN_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(form.KIKAN_TO.GetResultText());

                DateTime chkFrom = dtpFrom.AddDays(1);
                DateTime chkTo = dtpTo.AddDays(1);

                DateTime preToMonth = dtpTo.AddMonths(-1);
                DateTime preFromMonth = dtpFrom.AddMonths(-1);

                if (chkFrom.Month != dtpFrom.Month)
                {
                    //入力された日付Fromが末日
                    int preDays = DateTime.DaysInMonth(preFromMonth.Year, preFromMonth.Month);
                    DateTime setFromDtp = new DateTime(preFromMonth.Year, preFromMonth.Month, preDays);

                    form.KIKAN_FROM.SetResultText(setFromDtp.ToString());
                }
                else
                {
                    form.KIKAN_FROM.SetResultText(preFromMonth.ToString());
                }

                if (chkTo.Month != dtpTo.Month)
                {
                    //入力された日付Toが末日
                    int preDays = DateTime.DaysInMonth(preToMonth.Year, preToMonth.Month);
                    DateTime setToDtp = new DateTime(preToMonth.Year, preToMonth.Month, preDays);

                    form.KIKAN_TO.SetResultText(setToDtp.ToString());
                }
                else
                {
                    form.KIKAN_TO.SetResultText(preToMonth.ToString());
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 次月ボタン押下処理
        /// <summary>
        /// 次月ボタン押下処理
        /// </summary>
        /// <returns></returns>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DateTime dtpFrom = DateTime.Parse(form.KIKAN_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(form.KIKAN_TO.GetResultText());

                DateTime chkFrom = dtpFrom.AddDays(1);
                DateTime chkTo = dtpTo.AddDays(1);

                DateTime nextToMonth = dtpTo.AddMonths(1);
                DateTime nextFromMonth = dtpFrom.AddMonths(1);

                if (chkFrom.Month != dtpFrom.Month)
                {
                    //入力された日付Fromが末日
                    int nextDays = DateTime.DaysInMonth(nextFromMonth.Year, nextFromMonth.Month);
                    DateTime setFromDtp = new DateTime(nextFromMonth.Year, nextFromMonth.Month, nextDays);

                    form.KIKAN_FROM.SetResultText(setFromDtp.ToString());
                }
                else
                {
                    form.KIKAN_FROM.SetResultText(nextFromMonth.ToString());
                }

                if (chkTo.Month != dtpTo.Month)
                {
                    //入力された日付Toが末日
                    int nextDays = DateTime.DaysInMonth(nextToMonth.Year, nextToMonth.Month);
                    DateTime setToDtp = new DateTime(nextToMonth.Year, nextToMonth.Month, nextDays);

                    form.KIKAN_TO.SetResultText(setToDtp.ToString());
                }
                else
                {
                    form.KIKAN_TO.SetResultText(nextToMonth.ToString());
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「F6 CSV出力ボタン」イベント処理
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.form.RegistErrorFlag)
                {
                    if (CheckDate())
                    {
                        return;
                    }

                    var result = MessageBoxUtility.MessageBoxShow("C012");
                    if (result == DialogResult.Yes)
                    {
                        // エラー色のクリア
                        this.ClearErrorColor();

                        // 検索実行処理
                        this.Search();
                    }
                }
                else
                {
                    var focusControl = this.form.allControl.OrderBy(c => c.TabIndex).OfType<ICustomAutoChangeBackColor>().Where(c => c.IsInputErrorOccured == true).FirstOrDefault();
                    if (null != focusControl)
                    {
                        ((Control)focusControl).Focus();
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「F12 閉じるボタン」イベント
        /// <summary>
        /// 「F12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

		#region 検索実行処理
		/// <summary>
		/// 検索実行処理
		/// </summary>
		/// <returns></returns>
		public int Search()
		{
			int result = 0;
			try
			{
				LogUtility.DebugMethodStart();

                // 行列リセット
                this.form.DGV_TEIKI_JISSEKI_HOUKOKU.ColumnCount = 0;
                this.form.DGV_TEIKI_JISSEKI_HOUKOKU.RowCount = 0;
                this.OutputLayoutRows.Clear();

				// 検索条件を設定する
				this.SetSearchCondition();

				// 検索実行
                if (this.form.SHUUKEI_KBN_1.Checked)
                {
                    this.SearchResult = this.accessor.GetJissekiHoukoku_1(this.SearchCondition);
                }
                else if (this.form.SHUUKEI_KBN_2.Checked)
                {
                    this.SearchResult = this.accessor.GetJissekiHoukoku_2(this.SearchCondition);
                }
                else if (this.form.SHUUKEI_KBN_3.Checked)
                {
                    this.SearchResult = this.accessor.GetJissekiHoukoku_3(this.SearchCondition);
                }
                if (0 < this.SearchResult.Length)
                {
                    // データ設定処理
                    this.SetDgvData();
                }
                else
                {
                    // メッセージ表示
                    MessageBoxUtility.MessageBoxShowWarn("対象データが無い為、出力を中止しました");
                }

				return this.SearchResult.Length;
			}
			catch (Exception ex)
			{
				// 例外エラー
				LogUtility.Error(ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd(result);
			}
		}
		#endregion

		#region CSV出力処理
		/// <summary>
		/// ファイル名取得処理
		/// </summary>
		/// <returns>ファイル名</returns>
		private string getDefaultFileName()
		{
			var title = ConstCls.CSV_TITLE;
			var shikuchousonName = this.form.SHIKUCHOUSON_NAME_RYAKU.Text;
			var defaultFileName = title;
			if (!string.IsNullOrEmpty(shikuchousonName))
			{
				defaultFileName = string.Format("{0}({1})", title, shikuchousonName);
			}

			return defaultFileName;
		}
		#endregion

		#region データ設定処理

        /// <summary>
        /// CSVファイル出力場所選択
        /// </summary>
        /// <returns></returns>
        internal string SelectCsvSaveFilePath()
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "CSVファイルの出力場所を選択してください。";
            var initialPath = @"C:\Temp";
            var windowHandle = this.form.Handle;
            var isFileSelect = false;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            return filePath;
        }

		/// <summary>
		/// データ設定処理
		/// </summary>
		internal void SetDgvData()
		{
			var rowIndex = -1;
            var dgv = this.form.DGV_TEIKI_JISSEKI_HOUKOKU;
            dgv = null; // 大量データ時に表示困難な為、DataGridViewには表示しない。

            //*****************************************************
            // 市区町村CD、実績分類CDが存在するデータのみ対象
            // ※実績分類CDなしでも出力されるよう修正
            // 順序：市区町村CD[昇順]
            // 　　：業者CD[昇順]、現場CD[昇順]
            // 　　：実績分類CD[昇順]
            //*****************************************************
            TeikiJissekiHoukokuDto[] results = this.SearchResult;
            if (this.form.SHUUKEI_KBN_1.Checked)
            {
                results = this.SearchResult
                    .Where(s => !s.CALC_UNIT_CD.IsNull)
                    .OrderBy(s => s.SHIKUCHOUSON_CD)
                    .ThenBy(s => s.GYOUSHA_CD)
                    .ThenBy(s => s.GENBA_CD)
                    .ThenBy(s => s.JISSEKI_BUNRUI_CD).ToArray();
            }
            else if (this.form.SHUUKEI_KBN_2.Checked)
            {
                results = this.SearchResult
                    .Where(s => !s.CALC_UNIT_CD.IsNull)
                    .OrderBy(s => s.SHIKUCHOUSON_CD)
                    .ThenBy(s => s.GYOUSHA_CD)
                    .ThenBy(s => s.GENBA_CD)
                    .ThenBy(s => s.NIOROSHI_GYOUSHA_CD)
                    .ThenBy(s => s.NIOROSHI_GENBA_CD)
                    .ThenBy(s => s.JISSEKI_BUNRUI_CD).ToArray();
            }
            else if (this.form.SHUUKEI_KBN_3.Checked)
            {
                results = this.SearchResult
                    .Where(s => !s.CALC_UNIT_CD.IsNull)
                    .OrderBy(s => s.SHIKUCHOUSON_CD)
                    .ThenBy(s => s.NIOROSHI_GYOUSHA_CD)
                    .ThenBy(s => s.NIOROSHI_GENBA_CD)
                    .ThenBy(s => s.JISSEKI_BUNRUI_CD).ToArray();
            }
            if (results.Count() == 0)
            {
                // メッセージ表示
                MessageBoxUtility.MessageBoxShowWarn("対象データが無い為、出力を中止しました");
                return;
            }
            //*****************************************************
            // CSVファイル出力場所選択
            //*****************************************************
            var filePath = this.SelectCsvSaveFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            try
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //var fileName = this.getDefaultFileName() + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var fileName = this.getDefaultFileName() + this.getDBDateTime().ToString("yyyyMMdd_HHmmss") + ".csv";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                using (var sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                {
                    //*****************************************************
                    // 単位の最大値を取得
                    //*****************************************************
                    var maxHinmeiUnit = results
                        .GroupBy(r =>
                            new
                            {
                                r.SHIKUCHOUSON_CD,
                                r.JISSEKI_BUNRUI_CD,
                                r.JISSEKI_BUNRUI_NAME,
                                r.CALC_UNIT_CD,
                                r.CALC_UNIT_NAME_RYAKU
                            })
                        .GroupBy(r =>
                            new
                            {
                                r.Key
                            })
                        .Select(r => r.Count())
                        .Max();

                    //*****************************************************
                    // 行列数設定処理
                    //*****************************************************
                    if (this.form.SHUUKEI_KBN_1.Checked)
                    {
                        this.setDgvColumnCount(dgv, ConstCls.COL_INDEX_HINMEI_START_1 + maxHinmeiUnit);
                    }
                    else if (this.form.SHUUKEI_KBN_2.Checked)
                    {
                        this.setDgvColumnCount(dgv, ConstCls.COL_INDEX_HINMEI_START_2 + maxHinmeiUnit);
                    }
                    else if (this.form.SHUUKEI_KBN_3.Checked)
                    {
                        this.setDgvColumnCount(dgv, ConstCls.COL_INDEX_HINMEI_START_3 + maxHinmeiUnit);
                    }
                    this.setDgvRow(sw, dgv, ref rowIndex);

                    //*****************************************************
                    // ◆グルーピングの単位
                    // ①市区町村
                    // ②実績分類
                    // ③業者・現場(1行)
                    // ④実績分類×単位(1列)
                    //*****************************************************
                    // 市区町村のグループを取得
                    var shikuchousonGroups = results
                        .GroupBy(s => new { s.SHIKUCHOUSON_CD, s.SHIKUCHOUSON_NAME_RYAKU })
                        .Select(s => new TeikiJissekiHoukokuDto.ShikuchousonGroup
                        {
                            SHIKUCHOUSON_CD = s.Key.SHIKUCHOUSON_CD,
                            SHIKUCHOUSON_NAME_RYAKU = s.Key.SHIKUCHOUSON_NAME_RYAKU
                        });
                    foreach (var shikuchousonGroup in shikuchousonGroups)
                    {
                        // 改行処理
                        if (rowIndex != 0)
                        {
                            // 市区町村変更時、改行(※初回を除く)
                            this.addDgvRow(sw, dgv, ref rowIndex, ConstCls.LINES_CHANGE_SHIKUCHOUSON);
                        }

                        // 市区町村毎にデータ抽出
                        var resultsByShikuchouson = results.Where(
                            s => s.SHIKUCHOUSON_CD == shikuchousonGroup.SHIKUCHOUSON_CD).ToArray();

                        // データ設定処理[市区町村毎]
                        this.setDgvDataByShikuchouson(sw, dgv, ref rowIndex, resultsByShikuchouson, shikuchousonGroup);
                    }
                }

                MessageBoxUtility.MessageBoxShowInformation("出力が完了しました。");
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBoxUtility.MessageBoxShowError("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。");
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBoxUtility.MessageBoxShowError("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。");
            }

            //*****************************************************
            // 表示領域に格納
            //*****************************************************
            if (dgv != null)
            {
                dgv.Rows.AddRange(this.OutputLayoutRows.ToArray());
            }
		}

		/// <summary>
		/// データ設定処理[市区町村毎]
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
		/// <param name="resultsByShikuchouson">市区町村毎に抽出したデータ</param>
		/// <param name="shikuchousonGroup">市区町村グループ</param>
		private void setDgvDataByShikuchouson(
            StreamWriter sw,
			DataGridView dgv,
			ref int rowIndex,
			TeikiJissekiHoukokuDto[] resultsByShikuchouson,
			TeikiJissekiHoukokuDto.ShikuchousonGroup shikuchousonGroup)
		{
			// 市区町村出力
            this.setDgvShikuchousonData(sw, dgv, ref rowIndex, shikuchousonGroup);
            this.addDgvRow(sw, dgv, ref rowIndex, ConstCls.LINES_AFTER_SHIKUCHOUSON);

			// 実績分類のグループを取得
			var jissekiBunruiGroups = resultsByShikuchouson
				.GroupBy(s => new { s.JISSEKI_BUNRUI_CD, s.JISSEKI_BUNRUI_NAME })
				.Select(s => new TeikiJissekiHoukokuDto.JissekiBunruiGroup
				{
					JISSEKI_BUNRUI_CD = s.Key.JISSEKI_BUNRUI_CD,
					JISSEKI_BUNRUI_NAME = s.Key.JISSEKI_BUNRUI_NAME
				}).ToArray();

            // データ設定処理[実績分類毎]
            this.setDgvDataByJissekiBunrui(sw, dgv, ref rowIndex, resultsByShikuchouson);
		}

		/// <summary>
		/// 市区町村出力
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
		/// <param name="shikuchousonGroup">市区町村グループ</param>
		private void setDgvShikuchousonData(
            StreamWriter sw,
			DataGridView dgv,
			ref int rowIndex,
			TeikiJissekiHoukokuDto.ShikuchousonGroup shikuchousonGroup)
		{
			// 市区町村[ヘッダ]出力
            this.setData(dgv, ConstCls.COL_INDEX_SHIKUCHOUSON_CD, ConstCls.HEADER_SHIKUCHOUSON_CD);
            this.setData(dgv, ConstCls.COL_INDEX_SHIKUCHOUSON_NAME, ConstCls.HEADER_SHIKUCHOUSON_NAME);
            this.addDgvRow(sw, dgv, ref rowIndex, 1);


            // 市区町村出力
            this.setData(dgv, ConstCls.COL_INDEX_SHIKUCHOUSON_CD, shikuchousonGroup.SHIKUCHOUSON_CD);
            this.setData(dgv, ConstCls.COL_INDEX_SHIKUCHOUSON_NAME, shikuchousonGroup.SHIKUCHOUSON_NAME_RYAKU);
            this.addDgvRow(sw, dgv, ref rowIndex, 1);
		}

		/// <summary>
		/// データ設定処理[実績分類毎]
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
		/// <param name="resultsByJissekiBunrui">実績分類毎に抽出したデータ</param>
        private void setDgvDataByJissekiBunrui(
            StreamWriter sw,
			DataGridView dgv,
			ref int rowIndex,
            TeikiJissekiHoukokuDto[] resultsByShikuchouson)
		{
			// 実績分類、単位のグループを取得
			// ※数量合計も算出
            var allHinmeiUnitGroups = resultsByShikuchouson
				.GroupBy(j =>
					new { j.JISSEKI_BUNRUI_CD, j.JISSEKI_BUNRUI_NAME, j.CALC_UNIT_CD, j.CALC_UNIT_NAME_RYAKU })
				.Select(j =>
					new TeikiJissekiHoukokuDto.HinmeiUnitGroup
					{
					    JISSEKI_BUNRUI_CD = j.Key.JISSEKI_BUNRUI_CD,
                        JISSEKI_BUNRUI_NAME = j.Key.JISSEKI_BUNRUI_NAME,
						CALC_UNIT_CD = j.Key.CALC_UNIT_CD,
						CALC_UNIT_NAME_RYAKU = j.Key.CALC_UNIT_NAME_RYAKU,
						SUM_SUURYOU = j.Sum(p => (p.CALC_SUURYOU))
					})
                    .OrderBy(j => j.JISSEKI_BUNRUI_CD)
                    .ToArray();

			// 実績分類出力
            this.setDgvJissekiBunruiHinmeiData(sw, dgv, ref rowIndex, allHinmeiUnitGroups);

			// 業者・現場ヘッダ＆単位出力
            this.setDgvGenbaUnitData(sw, dgv, ref rowIndex, allHinmeiUnitGroups);

			// 業者・現場のグループを取得
            var genbaGroups = resultsByShikuchouson
                .GroupBy(j => new
                {
                    j.GYOUSHA_CD, j.DISP_DETAIL_GYOUSHA_NAME,
                    j.GENBA_CD, j.DISP_DETAIL_GENBA_NAME,
                    j.NIOROSHI_GYOUSHA_CD, j.DISP_NIOROSHI_GYOUSHA_NAME,
                    j.NIOROSHI_GENBA_CD, j.DISP_NIOROSHI_GENBA_NAME
                })
				.Select(j => new TeikiJissekiHoukokuDto.GenbaGroup
				{
					GYOUSHA_CD = j.Key.GYOUSHA_CD,
                    DISP_DETAIL_GYOUSHA_NAME = j.Key.DISP_DETAIL_GYOUSHA_NAME,
					GENBA_CD = j.Key.GENBA_CD,
                    DISP_DETAIL_GENBA_NAME = j.Key.DISP_DETAIL_GENBA_NAME,
                    NIOROSHI_GYOUSHA_CD = j.Key.NIOROSHI_GYOUSHA_CD,
                    DISP_NIOROSHI_GYOUSHA_NAME = j.Key.DISP_NIOROSHI_GYOUSHA_NAME,
                    NIOROSHI_GENBA_CD = j.Key.NIOROSHI_GENBA_CD,
                    DISP_NIOROSHI_GENBA_NAME = j.Key.DISP_NIOROSHI_GENBA_NAME
				}).ToArray();

            // 業者・現場、実績分類、単位のグループを取得
            // ※数量合計も算出
            var genbaHinmeiUnitGroups = resultsByShikuchouson
                .GroupBy(g =>
                    new
                    {
                        g.GYOUSHA_CD,
                        g.DISP_DETAIL_GYOUSHA_NAME,
                        g.GENBA_CD,
                        g.DISP_DETAIL_GENBA_NAME,
                        g.NIOROSHI_GYOUSHA_CD,
                        g.DISP_NIOROSHI_GYOUSHA_NAME,
                        g.NIOROSHI_GENBA_CD,
                        g.DISP_NIOROSHI_GENBA_NAME,
                        g.JISSEKI_BUNRUI_CD,
                        g.JISSEKI_BUNRUI_NAME,
                        g.CALC_UNIT_CD,
                        g.CALC_UNIT_NAME_RYAKU
                    })
                .Select(g =>
                    new
                    {
                        g.Key,
                        SUM_SUURYOU = g.Sum(p => (p.CALC_SUURYOU))
                    }).ToArray();

			foreach (var genbaGroup in genbaGroups)
			{
                // 実績分類、単位のグループを取得
                var hinmeiUnitGroups = genbaHinmeiUnitGroups
                    .Where(g =>
                        g.Key.GYOUSHA_CD == genbaGroup.GYOUSHA_CD &&
                        g.Key.GENBA_CD == genbaGroup.GENBA_CD &&
                        g.Key.NIOROSHI_GYOUSHA_CD == genbaGroup.NIOROSHI_GYOUSHA_CD &&
                        g.Key.NIOROSHI_GENBA_CD == genbaGroup.NIOROSHI_GENBA_CD)
                    .Select(g =>
                        new TeikiJissekiHoukokuDto.HinmeiUnitGroup
                        {
                            JISSEKI_BUNRUI_CD = g.Key.JISSEKI_BUNRUI_CD,
                            JISSEKI_BUNRUI_NAME = g.Key.JISSEKI_BUNRUI_NAME,
                            CALC_UNIT_CD = g.Key.CALC_UNIT_CD,
                            CALC_UNIT_NAME_RYAKU = g.Key.CALC_UNIT_NAME_RYAKU,
                            SUM_SUURYOU = g.SUM_SUURYOU
                        }).ToArray();

				// データ設定処理[現場毎]
                this.setDgvDataByGenba(sw, dgv, ref rowIndex, hinmeiUnitGroups, allHinmeiUnitGroups, genbaGroup);
                this.addDgvRow(sw, dgv, ref rowIndex, 1);
			}
            this.addDgvRow(sw, dgv, ref rowIndex, ConstCls.LINES_BEFORE_JISSEKI_BUNRUI_GOUKEI);

			// 市区町村、実績分類毎の合計を出力
            this.setDgvJissekiBunruiGoukeiData(sw, dgv, ref rowIndex, allHinmeiUnitGroups);
		}

        /// <summary>
        /// 実績分類
        /// </summary>
        /// <param name="sw">出力</param>
        /// <param name="dgv">表示領域</param>
        /// <param name="rowIndex">行インデックス</param>
        /// <param name="allHinmeiUnitGroups">全品名・単価グループ配列</param>
        private void setDgvJissekiBunruiHinmeiData(
            StreamWriter sw,
            DataGridView dgv,
            ref int rowIndex,
            TeikiJissekiHoukokuDto.HinmeiUnitGroup[] allHinmeiUnitGroups)
        {
            // 実績分類ヘッダ出力
            int hinmeiIndex = 0;
            if (this.form.SHUUKEI_KBN_1.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_JISSEKI_BUNRUI_NAME_1, ConstCls.HEADER_JISSEKI_BUNRUI_NAME);
                hinmeiIndex = ConstCls.COL_INDEX_HINMEI_START_1;
            }
            if (this.form.SHUUKEI_KBN_2.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_JISSEKI_BUNRUI_NAME_2, ConstCls.HEADER_JISSEKI_BUNRUI_NAME);
                hinmeiIndex = ConstCls.COL_INDEX_HINMEI_START_2;
            }
            if (this.form.SHUUKEI_KBN_3.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_JISSEKI_BUNRUI_NAME_3, ConstCls.HEADER_JISSEKI_BUNRUI_NAME);
                hinmeiIndex = ConstCls.COL_INDEX_HINMEI_START_3;
            }

            // 実績分類出力
            foreach (var allHinmeiUnitGroup in allHinmeiUnitGroups)
            {
                this.setData(dgv, hinmeiIndex, allHinmeiUnitGroup.JISSEKI_BUNRUI_NAME);
                hinmeiIndex++;
            }
            this.addDgvRow(sw, dgv, ref rowIndex, 1);
        }

        /// <summary>
        /// 業者・現場ヘッダ＆単位出力
        /// </summary>
        /// <param name="sw">出力</param>
        /// <param name="dgv">表示領域</param>
        /// <param name="rowIndex">行インデックス</param>
        /// <param name="allHinmeiUnitGroups">全品名・単価グループ配列</param>
        private void setDgvGenbaUnitData(
            StreamWriter sw,
            DataGridView dgv,
            ref int rowIndex,
            TeikiJissekiHoukokuDto.HinmeiUnitGroup[] allHinmeiUnitGroups)
        {
            // 業者・現場ヘッダ出力
            int unitIndex = 0;
            if (this.form.SHUUKEI_KBN_1.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, ConstCls.HEADER_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, ConstCls.HEADER_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, ConstCls.HEADER_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, ConstCls.HEADER_GENBA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_UNIT_NAME_1, ConstCls.HEADER_UNIT_NAME);
                unitIndex = ConstCls.COL_INDEX_UNIT_START_1;
            }
            else if (this.form.SHUUKEI_KBN_2.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, ConstCls.HEADER_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, ConstCls.HEADER_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, ConstCls.HEADER_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, ConstCls.HEADER_GENBA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GYOUSHA_CD, ConstCls.HEADER_NIOROSHI_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GYOUSHA_NAME, ConstCls.HEADER_NIOROSHI_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GENBA_CD, ConstCls.HEADER_NIOROSHI_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GENBA_NAME, ConstCls.HEADER_NIOROSHI_GENBA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_UNIT_NAME_2, ConstCls.HEADER_UNIT_NAME);
                unitIndex = ConstCls.COL_INDEX_UNIT_START_2;
            }
            else if (this.form.SHUUKEI_KBN_3.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, ConstCls.HEADER_NIOROSHI_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, ConstCls.HEADER_NIOROSHI_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, ConstCls.HEADER_NIOROSHI_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, ConstCls.HEADER_NIOROSHI_GENBA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_UNIT_NAME_3, ConstCls.HEADER_UNIT_NAME);
                unitIndex = ConstCls.COL_INDEX_UNIT_START_3;
            }

            // 単位出力
            foreach (var allHinmeiUnitGroup in allHinmeiUnitGroups)
            {
                this.setData(dgv, unitIndex, allHinmeiUnitGroup.CALC_UNIT_NAME_RYAKU);
                unitIndex++;
            }
            this.addDgvRow(sw, dgv, ref rowIndex, 1);
        }

		/// <summary>
		/// 市区町村、実績分類毎の合計を出力
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
		/// <param name="allHinmeiUnitGroups">全品名・単価グループ配列</param>
        private void setDgvJissekiBunruiGoukeiData(
            StreamWriter sw,
			DataGridView dgv,
			ref int rowIndex,
			TeikiJissekiHoukokuDto.HinmeiUnitGroup[] allHinmeiUnitGroups)
		{
            // 市区町村、実績分類毎の合計を出力
            this.setData(dgv, ConstCls.COL_INDEX_HEADER_JISSEKI_BUNRUI_GOUKEI, ConstCls.HEADER_JISSEKI_BUNRUI_GOUKEI);

            int jissekiBunruiGoukeiIndex = 0;
            if (this.form.SHUUKEI_KBN_1.Checked)
            {
                jissekiBunruiGoukeiIndex = ConstCls.COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_1;
            }
            else if (this.form.SHUUKEI_KBN_2.Checked)
            {
                jissekiBunruiGoukeiIndex = ConstCls.COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_2;
            }
            else if (this.form.SHUUKEI_KBN_3.Checked)
            {
                jissekiBunruiGoukeiIndex = ConstCls.COL_INDEX_JISSEKI_BUNRUI_GOUKEI_START_3;
            }

			foreach (var allHinmeiUnitGroup in allHinmeiUnitGroups)
            {
                this.setData(dgv, jissekiBunruiGoukeiIndex, allHinmeiUnitGroup.SUM_SUURYOU, r_framework.Dto.SystemProperty.Format.Suuryou);
				jissekiBunruiGoukeiIndex++;
			}
            this.addDgvRow(sw, dgv, ref rowIndex, 1);
		}

		/// <summary>
		/// データ設定処理[現場毎]
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
        /// <param name="hinmeiUnitGroups">品名、単位のグループ配列</param>
		/// <param name="allHinmeiUnitGroups">全品名・単価グループ配列</param>
		/// <param name="genbaGroup">現場グループ</param>
        private void setDgvDataByGenba(
            StreamWriter sw,
			DataGridView dgv,
			ref int rowIndex,
            TeikiJissekiHoukokuDto.HinmeiUnitGroup[] hinmeiUnitGroups,
            TeikiJissekiHoukokuDto.HinmeiUnitGroup[] allHinmeiUnitGroups,
			TeikiJissekiHoukokuDto.GenbaGroup genbaGroup)
		{
            int suuryouIndex = 0;
            // 業者・現場情報出力
            if (this.form.SHUUKEI_KBN_1.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, genbaGroup.GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, genbaGroup.DISP_DETAIL_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, genbaGroup.GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, genbaGroup.DISP_DETAIL_GENBA_NAME);
                suuryouIndex = ConstCls.COL_INDEX_SUURYOU_START_1;
            }
            else if (this.form.SHUUKEI_KBN_2.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, genbaGroup.GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, genbaGroup.DISP_DETAIL_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, genbaGroup.GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, genbaGroup.DISP_DETAIL_GENBA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GYOUSHA_CD, genbaGroup.NIOROSHI_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GYOUSHA_NAME, genbaGroup.DISP_NIOROSHI_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GENBA_CD, genbaGroup.NIOROSHI_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_NIOROSHI_GENBA_NAME, genbaGroup.DISP_NIOROSHI_GENBA_NAME);
                suuryouIndex = ConstCls.COL_INDEX_SUURYOU_START_2;
            }
            else if (this.form.SHUUKEI_KBN_3.Checked)
            {
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_CD, genbaGroup.NIOROSHI_GYOUSHA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GYOUSHA_NAME, genbaGroup.DISP_NIOROSHI_GYOUSHA_NAME);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_CD, genbaGroup.NIOROSHI_GENBA_CD);
                this.setData(dgv, ConstCls.COL_INDEX_HEADER_GENBA_NAME, genbaGroup.DISP_NIOROSHI_GENBA_NAME);
                suuryouIndex = ConstCls.COL_INDEX_SUURYOU_START_3;
            }

            // 実績分類、単位情報出力
			foreach (var allHinmeiUnitGroup in allHinmeiUnitGroups)
			{
				var hinmeiUnitGroup = hinmeiUnitGroups.Where(h =>
                        h.JISSEKI_BUNRUI_CD == allHinmeiUnitGroup.JISSEKI_BUNRUI_CD &&
                        h.JISSEKI_BUNRUI_NAME == allHinmeiUnitGroup.JISSEKI_BUNRUI_NAME &&
						!h.CALC_UNIT_CD.IsNull && !allHinmeiUnitGroup.CALC_UNIT_CD.IsNull &&
						h.CALC_UNIT_CD.Value == allHinmeiUnitGroup.CALC_UNIT_CD.Value &&
						h.CALC_UNIT_NAME_RYAKU == allHinmeiUnitGroup.CALC_UNIT_NAME_RYAKU).FirstOrDefault();
				var SUM_SUURYOU = 0.0M;
				if (hinmeiUnitGroup != null)
				{
					SUM_SUURYOU = hinmeiUnitGroup.SUM_SUURYOU;
				}

                this.setData(dgv, suuryouIndex, SUM_SUURYOU, r_framework.Dto.SystemProperty.Format.Suuryou);
                suuryouIndex++;
			}
		}

        /// <summary>
        /// 値設定処理
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="colIndex"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        private void setData(DataGridView dgv, int colIndex, object value, string format = "")
        {
            if (dgv != null)
            {
                this.getLastRow().Cells[colIndex].Value = value;
            }

            this.setLineData(colIndex, value, format);
        }

        /// <summary>
        /// 行追加処理
        /// </summary>
        /// <param name="dgv">表示領域</param>
        /// <param name="rowIndex">行インデックス</param>
        private void setDgvRow(StreamWriter sw, DataGridView dgv, ref int rowIndex)
        {
            if (dgv != null)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dgv);
                this.OutputLayoutRows.Add(row);
            }

            rowIndex++;
        }

		/// <summary>
		/// 行追加処理
        /// </summary>
        /// <param name="sw">出力</param>
		/// <param name="dgv">表示領域</param>
		/// <param name="rowIndex">行インデックス</param>
		/// <param name="addRowCount">追加行数</param>
		private void addDgvRow(StreamWriter sw, DataGridView dgv, ref int rowIndex, int addRowCount)
		{
            if (dgv != null)
            {
                for (int i = 0; i < addRowCount; i++)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgv);
                    this.OutputLayoutRows.Add(row);
                }
            }

            rowIndex += addRowCount;

            this.writeLine(sw);
            this.addEmptyLine(sw, addRowCount - 1);
		}

        /// <summary>
        /// 行追加処理
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="addRowCount"></param>
        private void addEmptyLine(StreamWriter sw, int addRowCount)
        {
            for (int i = 0; i < addRowCount; i++)
            {
                sw.WriteLine();
            }
        }

        /// <summary>
        /// 1行出力
        /// </summary>
        /// <param name="sw"></param>
        private void writeLine(StreamWriter sw)
        {
            sw.WriteLine(string.Join(",", this.OutputLine));
            this.OutputLine.Clear();
        }

        /// <summary>
        /// 行データ設定
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="setData"></param>
        /// <param name="format"></param>
        private void setLineData(int colIndex, object setData, string format = "")
        {
            while (this.OutputLine.Count < (colIndex + 1))
            {
                this.OutputLine.Add(string.Empty);
            }

            if (setData is double && !string.IsNullOrEmpty(format))
            {
                var dData = (decimal)setData;
                this.OutputLine[colIndex] = Shougun.Core.Common.BusinessCommon.Utility.CSVExport.CSVQuote(dData.ToString(format));
            }
            else
            {
                this.OutputLine[colIndex] = Shougun.Core.Common.BusinessCommon.Utility.CSVExport.CSVQuote(Convert.ToString(setData));
            }
        }

		/// <summary>
		/// 列数設定処理
		/// </summary>
		/// <param name="dgv">表示領域</param>
		/// <param name="columnCount">設定列数</param>
		private void setDgvColumnCount(DataGridView dgv, int columnCount)
		{
            if (dgv != null)
            {
                while (dgv.ColumnCount < columnCount)
                {
                    var strColNumber = (dgv.ColumnCount + 1).ToString();
                    dgv.Columns.Add(strColNumber, strColNumber);
                }
            }
		}

        /// <summary>
        /// 出力レイアウトの最終行を取得
        /// </summary>
        /// <returns></returns>
        private DataGridViewRow getLastRow()
        {
            return this.OutputLayoutRows[this.OutputLayoutRows.Count - 1];
        }

		#endregion

		#region 検索条件の設定
		/// <summary>
		/// 検索条件の設定
		/// </summary>
		internal void SetSearchCondition()
		{
			try
			{
				LogUtility.DebugMethodStart();

				var searchCondition = new TeikiJissekiHoukokuDto();

				// 拠点
				if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
				{
					// ※99全社は、単一の拠点としてではなく、全拠点（all）の意味とします。
					if (this.form.KYOTEN_CD.Text != "99")
					{
						searchCondition.KYOTEN_CD = short.Parse(this.form.KYOTEN_CD.Text);
					}
				}

				// 市区町村
				if (!string.IsNullOrEmpty(this.form.SHIKUCHOUSON_CD.Text))
				{
					searchCondition.SHIKUCHOUSON_CD = this.form.SHIKUCHOUSON_CD.Text;
				}

				// 期間From
				if (!string.IsNullOrEmpty(this.form.KIKAN_FROM.Text) && this.form.KIKAN_FROM.Value is DateTime)
				{
					searchCondition.KIKAN_FROM = (DateTime)this.form.KIKAN_FROM.Value;
				}

				// 期間To
				if (!string.IsNullOrEmpty(this.form.KIKAN_TO.Text) && this.form.KIKAN_TO.Value is DateTime)
				{
					searchCondition.KIKAN_TO = (DateTime)this.form.KIKAN_TO.Value;
				}

				// 集計対象数量
				if (!string.IsNullOrEmpty(this.form.SHUUKEISYUURYOU.Text))
				{
					searchCondition.SHUUKEISUURYOU = int.Parse(this.form.SHUUKEISYUURYOU.Text);
				}

				this.SearchCondition = searchCondition;
			}
			catch (Exception ex)
			{
				LogUtility.Error(ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd();
			}
		}
		#endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.KIKAN_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.KIKAN_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (this.form.KIKAN_KBN_2.Checked)
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.KIKAN_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.KIKAN_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.KIKAN_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.KIKAN_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.KIKAN_FROM.IsInputErrorOccured = true;
                this.form.KIKAN_TO.IsInputErrorOccured = true;
                this.form.KIKAN_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.KIKAN_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                string[] errorMsg = { "期間From", "期間To" };
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.KIKAN_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
		#region エラー色のクリア
		/// <summary>
		/// エラー色のクリア
		/// </summary>
		internal void ClearErrorColor()
		{
			foreach (var control in this.form.allControl)
			{
				if (control.BackColor == Constans.ERROR_COLOR)
				{
					control.BackColor = Constans.NOMAL_COLOR;
				}
			}
		}
		#endregion

		#region Equals/GetHashCode/ToString

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{

			return base.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString();
		}

		#endregion

		#region 自動生成（実装なし）
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
		#endregion

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
