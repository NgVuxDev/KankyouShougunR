using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Allocation.MobileShougunShutsuryoku.APP;

namespace Shougun.Core.Allocation.MobileShougunShutsuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class MobileShougunShutsuryokuLogic : IBuisinessLogic
    {

		/// <summary>
		/// 配車状況CD
		/// </summary>
		private static readonly Int16 HAISHA_JOKYO_JUCHU = 1;	// 受注
		private static readonly Int16 HAISHA_JOKYO_HAISHA = 2;	// 配車

		/// <summary>
        /// DAO
        /// </summary>
        private MobileShougunShutsuryokuDAOClass dao;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileShougunShutsuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private MobileShougunShutsuryokuDTOClass dto;

        /// <summary>
        /// XML出力先（マスタファイル）
        /// </summary>
        private string mobileOutPutMasterPath;

        /// <summary>
        /// XML出力先（配車ファイル）
        /// </summary>
        private string mobileOutPutTransPath;

        /// <summary>
        /// XML出力先（バックアップファイル）
        /// </summary>
        private string mobileBackUpPath;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 出力なし
        /// </summary>
        Boolean noOutPutData = false;

        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MobileShougunShutsuryokuLogic(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new MobileShougunShutsuryokuDTOClass();
            this.dao = DaoInitUtility.GetComponent<MobileShougunShutsuryokuDAOClass>();
            this.MsgBox = new MessageBoxShowLogic();
            this.InitializePath();

            LogUtility.DebugMethodEnd();
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

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                // 画面初期表示設定
                this.InitializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen()
        {
            //「作業区分」／マスタファイル選択
            this.form.txt_Sagyokubun.Text = "1";

            //「作業開始日」／システム日付の翌日
            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.dtp_TaishoKikanFrom.Value = parentForm.sysDate;

            //「作業終了日」／作業開始日の７日後
            this.form.dtp_TaishoKikanTo.Value = parentForm.sysDate.AddDays(7);

            //「作業開始日」／マスタファイルを選択時、編集不可
            this.form.dtp_TaishoKikanFrom.Enabled = false;

            //「作業終了日」／マスタファイルを選択時、編集不可
            this.form.dtp_TaishoKikanTo.Enabled = false;

            // 「拠点CD」／マスタファイルを選択時、編集不可
            this.form.txt_KyotenCD.Enabled = false;

            // 「拠点名」／マスタファイルを選択時、編集不可
            this.form.txt_KyotenName.Enabled = false;
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
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 「Ｆ6 ﾃﾞｰﾀ出力ボタン」イベントのイベント生成
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

            // 「Ｆ12 閉じるボタン」イベントのイベント生成
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            // 「作業区分」のイベント生成
            this.form.txt_Sagyokubun.TextChanged += new EventHandler(txt_Sagyokubun_Change);

            // 「拠点CD」のイベント生成
            this.form.txt_KyotenCD.Leave += new EventHandler(txt_KyotenCD_Leave);

            /// 20141023 Houkakou 「モバイル将軍出力」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtp_TaishoKikanTo.MouseDoubleClick += new MouseEventHandler(TaishoKikanTo_MouseDoubleClick);
            /// 20141023 Houkakou 「モバイル将軍出力」のダブルクリックを追加する　end


            /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
            this.form.dtp_TaishoKikanFrom.Leave += new System.EventHandler(TaishoKikanFrom_Leave);
            this.form.dtp_TaishoKikanTo.Leave += new System.EventHandler(TaishoKikanTo_Leave);
            /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end

        }

        /// <summary>
        /// XML出力先ディレクトリ設定
        /// </summary>
        private void InitializePath()
        {
            // マスタファイル
            this.mobileOutPutMasterPath = GetOutPutMasterPath();

            // 配車ファイル
            this.mobileOutPutTransPath = GetOutPutTransPath();

            // バックアップファイル
            this.mobileBackUpPath = GetBackUpFilePath();

        }

        /// <summary>
        /// XML出力先ファイルパス
        /// </summary>
        /// <param name="path">XML出力先ディレクトリ</param>
        private string GetOutPutPath(string path)
        {
            string retPath = string.Empty;

            try
            {
                var iniPath = AppData.PrepareLocalAppDataFile("mobileXMLPath.ini");

                // XML出力先ディレクトリ読み込み
                string[] lines = File.ReadAllLines(iniPath,
                  System.Text.Encoding.GetEncoding("Shift_JIS"));

                // ディレクトリ取得
                int maxCount = lines.Length;
                for (int i = 0; i < maxCount; i++)
                {
                    if (lines[i].IndexOf(path) >= 0)
                    {
                        // 「=」以降を取得
                        int num = lines[i].IndexOf("=");
                        retPath = lines[i].Substring(num + 1);
                    }

                }
            }
            catch (Exception)
            {
                LogUtility.Error("G284:モバイル将軍データ出力 iniファイル読込失敗");
                throw;
            }
            return retPath;
        }

        /// <summary>
        /// XML出力先ファイルパス（マスタファイル）
        /// </summary>
        private String GetOutPutMasterPath()
        {
            // XML出力先設定
            return GetOutPutPath("mobileOutPutMasterPath");

        }

        /// <summary>
        /// XML出力先ファイルパス（配車ファイル）
        /// </summary>
        private String GetOutPutTransPath()
        {
            // XML出力先設定
            return GetOutPutPath("mobileOutPutTransPath");

        }

        /// <summary>
        /// XML出力先ファイルパス（バックアップファイル）
        /// </summary>
        private String GetBackUpFilePath()
        {
            // XML出力先設定
            return GetOutPutPath("mobileBackUpPath");

        }

        /// <summary>
        /// 「Ｆ6 ﾃﾞｰﾀ出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {

            Boolean checkFlgHaisha = false;
            Boolean checkFlgMaster = false;

            noOutPutData = false;

            // 1,2,3以外の場合エラー
            if (!("1" == this.form.txt_Sagyokubun.Text
                || "2" == this.form.txt_Sagyokubun.Text
                || "3" == this.form.txt_Sagyokubun.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "作業区分");
                this.form.txt_Sagyokubun.Focus();

            }

            //「１．マスタファイル」選択
            if (this.form.rdo_Master.Checked)
            {
                // マスタXML出力
                checkFlgMaster = OutPutMaterXML();

                if (checkFlgMaster)
                {
                    // 完了メッセージ出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "マスタファイルの出力");
                    this.form.txt_Sagyokubun.Focus();
                }

            }

            //「２．配車ファイル」選択
            else if (this.form.rdo_Haisha.Checked)
            {

                // チェック
                if (this.form.dtp_TaishoKikanFrom.Value == null)
                {
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "作業日From");
                    this.form.dtp_TaishoKikanFrom.Focus();
                }
                else if (this.form.dtp_TaishoKikanTo.Value == null)
                {
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "作業日To");
                    this.form.dtp_TaishoKikanTo.Focus();
                }
                else if (DateTime.Parse(this.form.dtp_TaishoKikanFrom.Value.ToString()) > DateTime.Parse(this.form.dtp_TaishoKikanTo.Value.ToString()))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.dtp_TaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "作業日From", "作業日To" };
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.txt_Sagyokubun.Focus();
                }
                else
                {
                    // 配車XML出力
                    checkFlgHaisha = OutPutHaishaXML();

                    if (noOutPutData)
                    {
                        // 出力しない
                    }
                    else if (checkFlgHaisha)
                    {
                        // 完了メッセージ出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "配車ファイルの出力");
                        this.form.txt_Sagyokubun.Focus();
                    }

                }

            }

            //「３．すべて」選択
            else if (this.form.rdo_All.Checked)
            {

                // チェック
                if (this.form.dtp_TaishoKikanFrom.Value == null)
                {
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "作業日From");
                    this.form.dtp_TaishoKikanFrom.Focus();
                }
                else if (this.form.dtp_TaishoKikanTo.Value == null)
                {
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "作業日To");
                    this.form.dtp_TaishoKikanTo.Focus();
                }
                else if (DateTime.Parse(this.form.dtp_TaishoKikanFrom.Value.ToString()) > DateTime.Parse(this.form.dtp_TaishoKikanTo.Value.ToString()))
                {

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
                    this.form.dtp_TaishoKikanFrom.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanTo.IsInputErrorOccured = true;
                    this.form.dtp_TaishoKikanFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.dtp_TaishoKikanTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "作業日From", "作業日To" };
                    /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.txt_Sagyokubun.Focus();

                }
                else
                {

                    // 配車XML出力
                    checkFlgHaisha = OutPutHaishaXML();

                    if (noOutPutData)
                    {
                        // 出力しない
                    }
                    else
                    {
                        // マスタXML出力
                        checkFlgMaster = OutPutMaterXML();
                    }
                    if (noOutPutData)
                    {
                        // 出力しない

                    }
                    else if (checkFlgHaisha && checkFlgMaster)
                    {
                        // 完了メッセージ出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "すべてのファイルの出力");
                        this.form.txt_Sagyokubun.Focus();

                    }
                    else if (checkFlgHaisha && !checkFlgMaster)
                    {
                        // 完了メッセージ出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "配車ファイルの出力");
                        this.form.txt_Sagyokubun.Focus();
                    }
                    else if (!checkFlgHaisha && checkFlgMaster)
                    {
                        // 完了メッセージ出力
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "マスタファイルの出力");
                        this.form.txt_Sagyokubun.Focus();
                    }



                }

            }
        }

		/// <summary>
        /// 配車のXMLを出力する
        /// </summary>
        private Boolean OutPutHaishaXML()
        {
			MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

			// 配車データ格納用DataTable 定期配車
            DataTable dataResultTeiki = GetTeikiData();
			DataRow[] rowsTeiki = dataResultTeiki.Select("(M_UNTENSHA_CD IS NULL) OR (M_UNTENSHA_CD = '') OR (M_SHARYOU_CD IS NULL) OR (M_SHARYOU_CD = '')");

			// 配車データ格納用DataTable スポット配車（収集）
			// 配車状況が「配車」のものを抽出
			DataTable dataResultUketsukeSS = GetUketsukeSS(HAISHA_JOKYO_HAISHA);
			DataRow[] rowsUketsuke = dataResultUketsukeSS.Select("(UNTENSHA_CD IS NULL) OR (UNTENSHA_CD = '') OR (SHARYOU_CD IS NULL) OR (SHARYOU_CD = '')");

			// アラート表示
			if((rowsUketsuke.Length != 0) || (rowsTeiki.Length != 0))
			{
				// 運転者CD、車輌CDにブランクもしくはNULLが存在すればアラート表示を行う
				if(msgLogic.MessageBoxShow("C059") == DialogResult.No)
				{
					noOutPutData = true;
				}
			}

			// 「出力する」が選択されている場合
			if(noOutPutData == false)
			{
				// 配車データ格納用DataTable スポット配車（収集）
				// 配車状況が「受注」のものを抽出
				DataTable data = GetUketsukeSS(HAISHA_JOKYO_JUCHU);
				// アラート判定
				if(data.Rows.Count != 0)
				{
					// 抽出対象期間内に配車状況が「受注」のものがあればアラート表示を行う
					if(msgLogic.MessageBoxShow("C062") == DialogResult.No)
					{
						noOutPutData = true;
					}
				}
			}

			// 「出力する」が選択されている場合
			if(noOutPutData == false)
			{
				// アラート判定
				rowsUketsuke = dataResultUketsukeSS.Select("(GENBA_CD IS NULL) OR (GENBA_CD = '')");
				if(rowsUketsuke.Length != 0)
				{
					// 現場CDにブランクもしくはNULLが存在すればアラート表示を行う
					if(msgLogic.MessageBoxShow("C063") == DialogResult.No)
					{
						// 出力しない場合はキャンセル
						noOutPutData = true;
					}
					else
					{
						// 現場が登録されているもののみ出力するため、再構築
						var table = dataResultUketsukeSS.Clone();
						rowsUketsuke = dataResultUketsukeSS.Select("((GENBA_CD IS NOT NULL) AND (GENBA_CD <> ''))");
						foreach(DataRow row in rowsUketsuke)
						{
							table.ImportRow(row);
						}
						dataResultUketsukeSS = table;
					}
				}
			}

			// 配車データが両方とも無かった場合、エラーMSG表示
            if (dataResultTeiki.Rows.Count == 0 && dataResultUketsukeSS.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("W002", "配車ファイル");
                this.form.txt_Sagyokubun.Focus();
                return false;
            }
            else if (noOutPutData)
            {
                // 出力しない
                return false;
            }
            else
            {
				try
				{
					// mobileBackUpPathフォルダの「tn_」で始まるファイルを削除します。
					string[] deleteFiles = Directory.GetFiles(this.mobileBackUpPath, "tn_*.xml");
					foreach (string file in deleteFiles)
					{
						FileInfo deleteFile = new FileInfo(file);
						deleteFile.Delete();
					}

					// mobileOutPutPathフォルダの「tn_」で始まるファイルを移動します。
					string[] moveFiles = Directory.GetFiles(this.mobileOutPutTransPath, "tn_*.xml");
					foreach (string file in moveFiles)
					{
						FileInfo moveFile = new FileInfo(file);
						moveFile.MoveTo(this.mobileBackUpPath + "/" + Path.GetFileName(file));
					}

					// 定期配車が０件の場合、スポットのみ出力
					if (dataResultTeiki.Rows.Count == 0)
					{
						// スポット配車（収集）出力
						OutPutUketsukeSSData(dataResultUketsukeSS);

						return true;
					}
					// スポットが０件の場合、定期配車のみ出力
					else if (dataResultUketsukeSS.Rows.Count == 0)
					{
						// 定期配車出力
						OutPutTeikiData(dataResultTeiki);

						return true;
					}
					else
					{
						// 定期配車出力
						OutPutTeikiData(dataResultTeiki);

						// スポット配車（収集）出力
						OutPutUketsukeSSData(dataResultUketsukeSS);

						return true;
					}
				}
				catch(System.IO.IOException)
				{
					// ファイルが操作中だった場合はメッセージを出力する
					msgLogic.MessageBoxShow("E149");
					noOutPutData = true;
					return false;
				}
            }

        }

        /// <summary>
        /// マスタのXMLを出力する
        /// </summary>
        private Boolean OutPutMaterXML()
        {
			try
			{
				// 現場マスタ
				DataTable dataResultShipper = GetGenbaData();
				// 品名マスタ
				DataTable dataResultGoods = GetGoodsData();
				// 搬入先マスタ
				DataTable dataResultDisposer = GetHannyuData();
				// 運転者マスタ
				DataTable dataResultDriver = GetDriverData();
				// 自社情報マスタ
				DataTable dataResultCompany = GetMyCompanyKyotenData();
				// 単位マスタ
				DataTable dataResultUnit = GetUnitData();
				// 車輌マスタ
				DataTable dataResultCar = GetCarData();
				// コンテナマスタ
				DataTable dataResultContainer = GetContainerData();

				// マスタがない場合、エラーMSG表示
				if(dataResultShipper.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（現場）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultGoods.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（品名）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultDisposer.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（搬入先）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultDriver.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（運転者）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultCompany.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（拠点）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultUnit.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（単位）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else if(dataResultCar.Rows.Count == 0)
				{
					// マスタ削除
					DeleteFileMaster();

					MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
					msgLogic.MessageBoxShow("W002", "マスタファイル（車輌）");
					this.form.txt_Sagyokubun.Focus();
					return false;

				}
				else
				{
					// マスタ削除
					DeleteFileMaster();

					// 現場マスタ出力
					OutPutShipperData(dataResultShipper);

					// 品名マスタ出力
					OutPutGoodsData(dataResultGoods);

					// 搬入先マスタ出力
					OutPutDisposerData(dataResultDisposer);

					// 運転者マスタ出力
					OutPutDriverData(dataResultDriver);

					// 自社情報マスタ出力
					string companyName = this.GetMyCompanyNameData();
					OutPutMyCompanyData(companyName, dataResultCompany);

					// 単位マスタ出力
					OutPutUnitData(dataResultUnit);

					// 車輌マスタ出力
					OutPutCarData(dataResultCar);

					// コンテナマスタ出力
					OutPutContainerData(dataResultContainer);

					return true;
				}
			}
			catch(System.IO.IOException)
			{
				// ファイルが操作中だった場合はメッセージを出力する
				MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
				msgLogic.MessageBoxShow("E149");
				return false;
			}
        }

        /// <summary>
        /// マスタファイル削除
        /// </summary>
        private void DeleteFileMaster()
        {
			FileInfo deleteFile;
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Shipper.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Goods.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Disposer.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Driver.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_MyCompany.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Unit.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Car.xml");
			deleteFile.Delete();
			deleteFile = new System.IO.FileInfo(this.mobileOutPutMasterPath + "m_Container.xml");
			deleteFile.Delete();
        }

        /// <summary>
        /// 「Ｆ12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        /// <summary>
        /// 「作業区分」変更時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void txt_Sagyokubun_Change(object sender, EventArgs e)
        {

            // マスタファイルを選択時、編集不可とし、値はクリア
            if ("1".Equals(this.form.txt_Sagyokubun.Text))
            {
                //「作業開始日」
                this.form.dtp_TaishoKikanFrom.Enabled = false;

                //「作業終了日」
                this.form.dtp_TaishoKikanTo.Enabled = false;

                // 「拠点CD」
                this.form.txt_KyotenCD.Enabled = false;
                this.form.txt_KyotenCD.Clear();

                // 「拠点名」
                this.form.txt_KyotenName.Enabled = false;
                this.form.txt_KyotenName.Clear();

            }
            // マスタファイル以外を選択時、編集可
            else
            {
                //「作業開始日」
                this.form.dtp_TaishoKikanFrom.Enabled = true;

                //「作業終了日」
                this.form.dtp_TaishoKikanTo.Enabled = true;

                // 「拠点CD」
                this.form.txt_KyotenCD.Enabled = true;

                // 「拠点名」
                this.form.txt_KyotenName.Enabled = true;

            }

        }

        /// <summary>
        /// 「拠点CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void txt_KyotenCD_Leave(object sender, EventArgs e)
        {


            // 拠点CDの値が無で拠点名が有の場合、拠点名をクリアする。
            if (string.IsNullOrEmpty(this.form.txt_KyotenCD.Text) && !string.IsNullOrEmpty(this.form.txt_KyotenName.Text))
            {
                this.form.txt_KyotenName.Clear();
            }


        }

        /// <summary>
        /// 単位マスタ取得処理
        /// </summary>
        private DataTable GetUnitData()
        {

            // 単位マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetUnitDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// コンテナマスタ取得処理
        /// </summary>
        private DataTable GetContainerData()
        {

            // コンテナマスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetContainerDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 車輌マスタ取得処理
        /// </summary>
        private DataTable GetCarData()
        {

            // 車輌マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetCarDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 運転マスタ取得処理
        /// </summary>
        private DataTable GetDriverData()
        {

            // 運転マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetDriverDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 品名マスタ取得処理
        /// </summary>
        private DataTable GetGoodsData()
        {

            // 品名マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetGoodsDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 自社情報マスタ 会社略称名取得処理
        /// </summary>
        private String GetMyCompanyNameData()
        {

            // 会社略称名データ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetMyCompanyNameDataForEntity(entity);

            // 会社略称名 主キーでの検索結果の為、１レコード
            return dataResult.Rows[0].ItemArray[0].ToString();

        }

        /// <summary>
        /// 自社情報マスタ 拠点マスタ取得処理
        /// </summary>
        private DataTable GetMyCompanyKyotenData()
        {

            // 拠点マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetMyCompanyKyotenDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 現場マスタ取得処理
        /// </summary>
        private DataTable GetGenbaData()
        {
            // 現場マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetGenbaDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 搬入マスタ取得処理
        /// </summary>
        private DataTable GetHannyuData()
        {
            // 搬入マスタデータ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            DataTable dataResult = this.dao.GetHannyuDataForEntity(entity);

            return dataResult;
        }

        /// <summary>
        /// 単位マスタ出力処理
        /// </summary>
        private void OutPutUnitData(DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutUnitXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, GetOutPutPath("mobileOutPutMasterPath") + "m_Unit.xml", false);

        }

        /// <summary>
        /// 単位マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutUnitXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement生成
            XmlElement elem = xmlDocument.CreateElement("masterUnit");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // 単位マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 単位マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement生成
                XmlElement elem_unit = xmlDocument.CreateElement("unit");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");

                // XmlNode生成
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[1].ToString();

                // Node設定
                elem.AppendChild(elem_unit);
                elem_unit.AppendChild(elem_cd);
                elem_unit.AppendChild(elem_name);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
            }
            return xmlDocument;
        }

        /// <summary>
        /// コンテナマスタ出力処理
        /// </summary>
        private void OutPutContainerData(DataTable dataResult)
        {

            // XML設定
            XmlDocument xmlDocument = OutPutContainerXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Container.xml", false);

        }

        /// <summary>
        /// コンテナマスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutContainerXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterContainer");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // コンテナマスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // コンテナマスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement
                XmlElement elem_contena = xmlDocument.CreateElement("contena");
                XmlElement elem_shurui = xmlDocument.CreateElement("shurui");
                XmlElement elem_shurui_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_shurui_name = xmlDocument.CreateElement("name");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");

                // XmlNode
                XmlNode node_shurui_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_shurui_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_shurui_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_shurui_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_cd.Value = dataResult.Rows[i].ItemArray[2].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[3].ToString();

                // Node設定
                elem.AppendChild(elem_contena);
                elem_contena.AppendChild(elem_shurui);
                elem_contena.AppendChild(elem_cd);
                elem_contena.AppendChild(elem_name);
                elem_shurui.AppendChild(elem_shurui_cd);
                elem_shurui.AppendChild(elem_shurui_name);
                elem_shurui_cd.AppendChild(node_shurui_cd);
                elem_shurui_name.AppendChild(node_shurui_name);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
            }
            return xmlDocument;
        }

        /// <summary>
        /// 車輌マスタ出力処理
        /// </summary>
        private void OutPutCarData(DataTable dataResult)
        {

            // XML設定
            XmlDocument xmlDocument = OutPutCarXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Car.xml", false);
        }

        /// <summary>
        /// 車輌マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutCarXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterCar");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // 車輌マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 車輌マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement
                XmlElement elem_sharyou = xmlDocument.CreateElement("sharyou");
                XmlElement elem_shasyu = xmlDocument.CreateElement("shashu");
                XmlElement elem_shasyu_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_shasyu_name = xmlDocument.CreateElement("name");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");
                XmlElement elem_gyoushaCd = xmlDocument.CreateElement("gyoushaCd");

                // XmlNode
                XmlNode node_shasyu_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_shasyu_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_shasyu_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_shasyu_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_cd.Value = dataResult.Rows[i].ItemArray[2].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[3].ToString();
                node_gyousha_cd.Value = dataResult.Rows[i].ItemArray[4].ToString();

                // Node設定
                elem.AppendChild(elem_sharyou);
                elem_sharyou.AppendChild(elem_shasyu);
                elem_sharyou.AppendChild(elem_cd);
                elem_sharyou.AppendChild(elem_name);
                elem_sharyou.AppendChild(elem_gyoushaCd);
                elem_shasyu.AppendChild(elem_shasyu_cd);
                elem_shasyu.AppendChild(elem_shasyu_name);
                elem_shasyu_cd.AppendChild(node_shasyu_cd);
                elem_shasyu_name.AppendChild(node_shasyu_name);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
                elem_gyoushaCd.AppendChild(node_gyousha_cd);
            }
            return xmlDocument;
        }

        /// <summary>
        /// 運転者マスタ出力処理
        /// </summary>
        private void OutPutDriverData(DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutDriverXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Driver.xml", false);
        }

        /// <summary>
        /// 運転者マスタ設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutDriverXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterDriver");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // 運転者マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 運転者マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement
                XmlElement elem_untensha = xmlDocument.CreateElement("untensha");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");
                XmlElement elem_furigana = xmlDocument.CreateElement("furigana");

                // XmlNode
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_furigana.Value = dataResult.Rows[i].ItemArray[2].ToString();

                // Node設定
                elem.AppendChild(elem_untensha);
                elem_untensha.AppendChild(elem_cd);
                elem_untensha.AppendChild(elem_name);
                elem_untensha.AppendChild(elem_furigana);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
                elem_furigana.AppendChild(node_furigana);

            }
            return xmlDocument;
        }

        /// <summary>
        /// 品名マスタ出力処理
        /// </summary>
        private void OutPutGoodsData(DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutGoodsXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Goods.xml", false);
        }

        /// <summary>
        /// 品名マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutGoodsXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterGoods");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // 品名マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 品名マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement
                XmlElement elem_hinmei = xmlDocument.CreateElement("hinmei");
                XmlElement elem_shurui = xmlDocument.CreateElement("shurui");
                XmlElement elem_shurui_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_shurui_name = xmlDocument.CreateElement("name");
                XmlElement elem_bunrui = xmlDocument.CreateElement("bunrui");
                XmlElement elem_bunrui_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_bunrui_name = xmlDocument.CreateElement("name");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");
                XmlElement elem_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                XmlElement elem_furigana = xmlDocument.CreateElement("furigana");

                // XmlNode
                XmlNode node_shurui_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_shurui_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_bunrui_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_bunrui_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_shurui_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_shurui_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_bunrui_cd.Value = dataResult.Rows[i].ItemArray[2].ToString();
                node_bunrui_name.Value = dataResult.Rows[i].ItemArray[3].ToString();
                node_cd.Value = dataResult.Rows[i].ItemArray[4].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[5].ToString();
                node_nameRyaku.Value = dataResult.Rows[i].ItemArray[6].ToString();
                node_furigana.Value = dataResult.Rows[i].ItemArray[7].ToString();

                // Node設定
                elem.AppendChild(elem_hinmei);
                elem_hinmei.AppendChild(elem_shurui);
                elem_hinmei.AppendChild(elem_bunrui);
                elem_hinmei.AppendChild(elem_cd);
                elem_hinmei.AppendChild(elem_name);
                elem_hinmei.AppendChild(elem_cd);
                elem_hinmei.AppendChild(elem_nameRyaku);
                elem_hinmei.AppendChild(elem_furigana);
                elem_shurui.AppendChild(elem_shurui_cd);
                elem_shurui.AppendChild(elem_shurui_name);
                elem_bunrui.AppendChild(elem_bunrui_cd);
                elem_bunrui.AppendChild(elem_bunrui_name);
                elem_shurui_cd.AppendChild(node_shurui_cd);
                elem_shurui_name.AppendChild(node_shurui_name);
                elem_bunrui_cd.AppendChild(node_bunrui_cd);
                elem_bunrui_name.AppendChild(node_bunrui_name);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
                elem_nameRyaku.AppendChild(node_nameRyaku);
                elem_furigana.AppendChild(node_furigana);

            }
            return xmlDocument;
        }

        /// <summary>
        /// 自社情報マスタ出力処理
        /// </summary>
        private void OutPutMyCompanyData(string companyName, DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutMyCompanyXML(companyName, dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_MyCompany.xml", false);
        }

        /// <summary>
        /// 自社情報マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        /// <param name="companyName">会社略称名</param>
        private static XmlDocument OutPutMyCompanyXML(string companyName, DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterMyCompany");
            XmlElement elem_corp = xmlDocument.CreateElement("corp");
            XmlElement elem_name = elem_name = xmlDocument.CreateElement("name");

            // XmlNode
            XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

            // 項目設定
            node_name.Value = companyName;

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);
            elem.AppendChild(elem_corp);
            elem_corp.AppendChild(elem_name);
            elem_name.AppendChild(node_name);

            // 拠点マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 拠点マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // XmlElement（拠点マスタ）
                XmlElement elem_kyoten = xmlDocument.CreateElement("kyoten");
                XmlElement elem_kyoten_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_kyoten_name = xmlDocument.CreateElement("name");
                XmlElement elem_kyoten_post = xmlDocument.CreateElement("post");
                XmlElement elem_kyoten_todoufuken = xmlDocument.CreateElement("todoufuken");
                XmlElement elem_kyoten_address1 = xmlDocument.CreateElement("address1");
                XmlElement elem_kyoten_address2 = xmlDocument.CreateElement("address2");
                XmlElement elem_kyoten_tel = xmlDocument.CreateElement("tel");

                // XmlNode（拠点マスタ）
                XmlNode node_kyoten_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_post = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_todoufuken = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_address1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_address2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_kyoten_tel = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_kyoten_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_kyoten_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_kyoten_post.Value = dataResult.Rows[i].ItemArray[2].ToString();
                node_kyoten_todoufuken.Value = dataResult.Rows[i].ItemArray[3].ToString();
                node_kyoten_address1.Value = dataResult.Rows[i].ItemArray[4].ToString();
                node_kyoten_address2.Value = dataResult.Rows[i].ItemArray[5].ToString();
                node_kyoten_tel.Value = dataResult.Rows[i].ItemArray[6].ToString();

                // Node設定
                elem.AppendChild(elem_corp);
                elem_corp.AppendChild(elem_name);
                elem_corp.AppendChild(elem_kyoten);
                elem_kyoten.AppendChild(elem_kyoten_cd);
                elem_kyoten.AppendChild(elem_kyoten_name);
                elem_kyoten.AppendChild(elem_kyoten_post);
                elem_kyoten.AppendChild(elem_kyoten_todoufuken);
                elem_kyoten.AppendChild(elem_kyoten_address1);
                elem_kyoten.AppendChild(elem_kyoten_address2);
                elem_kyoten.AppendChild(elem_kyoten_tel);
                elem_kyoten_cd.AppendChild(node_kyoten_cd);
                elem_kyoten_name.AppendChild(node_kyoten_name);
                elem_kyoten_post.AppendChild(node_kyoten_post);
                elem_kyoten_todoufuken.AppendChild(node_kyoten_todoufuken);
                elem_kyoten_address1.AppendChild(node_kyoten_address1);
                elem_kyoten_address2.AppendChild(node_kyoten_address2);
                elem_kyoten_tel.AppendChild(node_kyoten_tel);

            }
            return xmlDocument;
        }

        /// <summary>
        /// 現場マスタ出力処理
        /// </summary>
        private void OutPutShipperData(DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutShipperXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Shipper.xml", false);
        }

        /// <summary>
        /// 現場マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutShipperXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterShipper");
            XmlElement elem_kaisyuuGyousha = xmlDocument.CreateElement("kaisyuuGyousha");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // Loopで業者コードの同一を判別させる為のキー
            String preGyoushaCode = "";

            // 現場マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 現場マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // 業者コードが同一の場合、現場レコードのみを出力
                if (preGyoushaCode.Equals(dataResult.Rows[i]["M_GYOUSHA_CD"].ToString()))
                {

                    // XmlElement
                    XmlElement elem_cd = xmlDocument.CreateElement("cd");
                    XmlElement elem_name1 = xmlDocument.CreateElement("name1");
                    XmlElement elem_name2 = xmlDocument.CreateElement("name2");
                    XmlElement elem_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                    XmlElement elem_furigana = xmlDocument.CreateElement("furigana");
                    XmlElement elem_keishou1 = xmlDocument.CreateElement("keishou1");
                    XmlElement elem_keishou2 = xmlDocument.CreateElement("keishou2");
                    XmlElement elem_genba = xmlDocument.CreateElement("genba");
                    XmlElement elem_genba_cd = xmlDocument.CreateElement("cd");
                    XmlElement elem_genba_name1 = xmlDocument.CreateElement("name1");
                    XmlElement elem_genba_name2 = xmlDocument.CreateElement("name2");
                    XmlElement elem_genba_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                    XmlElement elem_genba_furigana = xmlDocument.CreateElement("furigana");
                    XmlElement elem_genba_keishou1 = xmlDocument.CreateElement("keishou1");
                    XmlElement elem_genba_keishou2 = xmlDocument.CreateElement("keishou2");
                    XmlElement elem_genba_post = xmlDocument.CreateElement("post");
                    XmlElement elem_genba_todoufuken = xmlDocument.CreateElement("todoufuken");
                    XmlElement elem_genba_address1 = xmlDocument.CreateElement("address1");
                    XmlElement elem_genba_address2 = xmlDocument.CreateElement("address2");
                    XmlElement elem_genba_tel = xmlDocument.CreateElement("tel");
                    XmlElement elem_genba_lat = xmlDocument.CreateElement("lat");
                    XmlElement elem_genba_lon = xmlDocument.CreateElement("lon");
                    XmlElement elem_genba_bikou1 = xmlDocument.CreateElement("bikou1");
                    XmlElement elem_genba_bikou2 = xmlDocument.CreateElement("bikou2");
                    XmlElement elem_genba_bikou3 = xmlDocument.CreateElement("bikou3");
                    XmlElement elem_genba_bikou4 = xmlDocument.CreateElement("bikou4");

                    // XmlNode
                    XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_name1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_name2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_keishou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_keishou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_name1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_name2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_keishou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_keishou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_post = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_todoufuken = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_address1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_address2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_tel = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_lat = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_lon = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou3 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou4 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    // 項目設定
                    node_cd.Value = dataResult.Rows[i]["M_GYOUSHA_CD"].ToString();
                    node_name1.Value = dataResult.Rows[i]["M_GYOUSHA_NAME1"].ToString();
                    node_name2.Value = dataResult.Rows[i]["M_GYOUSHA_NAME2"].ToString();
                    node_nameRyaku.Value = dataResult.Rows[i]["M_GYOUSHA_RYAKU"].ToString();
                    node_furigana.Value = dataResult.Rows[i]["M_GYOUSHA_FURIGANA"].ToString();
                    node_keishou1.Value = dataResult.Rows[i]["M_GYOUSHA_KEISHOU1"].ToString();
                    node_keishou2.Value = dataResult.Rows[i]["M_GYOUSHA_KEISHOU2"].ToString();
                    node_genba_cd.Value = dataResult.Rows[i]["M_GENBA_CD"].ToString();
                    node_genba_name1.Value = dataResult.Rows[i]["M_GENBA_NAME1"].ToString();
                    node_genba_name2.Value = dataResult.Rows[i]["M_GENBA_NAME2"].ToString();
                    node_genba_nameRyaku.Value = dataResult.Rows[i]["M_GENBA_NAME_RYAKU"].ToString();
                    node_genba_furigana.Value = dataResult.Rows[i]["M_GENBA_FURIGANA"].ToString();
                    node_genba_keishou1.Value = dataResult.Rows[i]["M_GENBA_KEISHOU1"].ToString();
                    node_genba_keishou2.Value = dataResult.Rows[i]["M_GENBA_KEISHOU2"].ToString();
                    node_genba_post.Value = dataResult.Rows[i]["M_GENBA_POST"].ToString();
                    node_genba_todoufuken.Value = dataResult.Rows[i]["M_TODOUFUKEN_NAME_RYAKU"].ToString();
                    node_genba_address1.Value = dataResult.Rows[i]["M_GENBA_ADDRESS1"].ToString();
                    node_genba_address2.Value = dataResult.Rows[i]["M_GENBA_ADDRESS2"].ToString();
                    node_genba_tel.Value = dataResult.Rows[i]["M_GENBA_TEL"].ToString();
                    node_genba_bikou1.Value = dataResult.Rows[i]["M_BIKOU1"].ToString();
                    node_genba_bikou2.Value = dataResult.Rows[i]["M_BIKOU2"].ToString();
                    node_genba_bikou3.Value = dataResult.Rows[i]["M_BIKOU3"].ToString();
                    node_genba_bikou4.Value = dataResult.Rows[i]["M_BIKOU4"].ToString();

                    // Node設定
                    elem_kaisyuuGyousha.AppendChild(elem_genba);
                    elem_genba.AppendChild(elem_genba_cd);
                    elem_genba.AppendChild(elem_genba_name1);
                    elem_genba.AppendChild(elem_genba_name2);
                    elem_genba.AppendChild(elem_genba_nameRyaku);
                    elem_genba.AppendChild(elem_genba_furigana);

                    // 現場敬称1, 2
                    if(false == string.IsNullOrEmpty(node_genba_keishou1.Value))
                    {
                        elem_genba.AppendChild(elem_genba_keishou1);
                        elem_genba_keishou1.AppendChild(node_genba_keishou1);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_keishou2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_keishou2);
                        elem_genba_keishou2.AppendChild(node_genba_keishou2);
                    }

                    // 郵便番号
                    if(false == string.IsNullOrEmpty(node_genba_post.Value))
                    {
                        elem_genba.AppendChild(elem_genba_post);
                        elem_genba_post.AppendChild(node_genba_post);
                    }

                    elem_genba.AppendChild(elem_genba_todoufuken);
                    elem_genba.AppendChild(elem_genba_address1);

                    // 住所2
                    if(false == string.IsNullOrEmpty(node_genba_address2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_address2);
                        elem_genba_address2.AppendChild(node_genba_address2);
                    }

                    // 電話番号
                    if(false == string.IsNullOrEmpty(node_genba_tel.Value))
                    {
                        elem_genba.AppendChild(elem_genba_tel);
                        elem_genba_tel.AppendChild(node_genba_tel);
                    }

                    // 緯度経度
                    if(false == string.IsNullOrEmpty(node_genba_lat.Value))
                    {
                        elem_genba.AppendChild(elem_genba_lat);
                        elem_genba_lat.AppendChild(node_genba_lat);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_lon.Value))
                    {
                        elem_genba.AppendChild(elem_genba_lon);
                        elem_genba_lon.AppendChild(node_genba_lon);
                    }

                    // 備考1～4
                    if(false == string.IsNullOrEmpty(node_genba_bikou1.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou1);
                        elem_genba_bikou1.AppendChild(node_genba_bikou1);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou2);
                        elem_genba_bikou2.AppendChild(node_genba_bikou2);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou3.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou3);
                        elem_genba_bikou3.AppendChild(node_genba_bikou3);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou4.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou4);
                        elem_genba_bikou4.AppendChild(node_genba_bikou4);
                    }
                    
                    elem_genba_cd.AppendChild(node_genba_cd);
                    elem_genba_name1.AppendChild(node_genba_name1);
                    elem_genba_name2.AppendChild(node_genba_name2);
                    elem_genba_nameRyaku.AppendChild(node_genba_nameRyaku);
                    elem_genba_furigana.AppendChild(node_genba_furigana);
                    elem_genba_todoufuken.AppendChild(node_genba_todoufuken);
                    elem_genba_address1.AppendChild(node_genba_address1);

                }
                // 業者コードが別の場合、業者レコードと現場レコードを出力
                else
                {

                    // XmlElement
                    elem_kaisyuuGyousha = xmlDocument.CreateElement("kaisyuuGyousha");
                    XmlElement elem_cd = xmlDocument.CreateElement("cd");
                    XmlElement elem_name1 = xmlDocument.CreateElement("name1");
                    XmlElement elem_name2 = xmlDocument.CreateElement("name2");
                    XmlElement elem_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                    XmlElement elem_furigana = xmlDocument.CreateElement("furigana");
                    XmlElement elem_keishou1 = xmlDocument.CreateElement("keishou1");
                    XmlElement elem_keishou2 = xmlDocument.CreateElement("keishou2");
                    XmlElement elem_genba = xmlDocument.CreateElement("genba");
                    XmlElement elem_genba_cd = xmlDocument.CreateElement("cd");
                    XmlElement elem_genba_name1 = xmlDocument.CreateElement("name1");
                    XmlElement elem_genba_name2 = xmlDocument.CreateElement("name2");
                    XmlElement elem_genba_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                    XmlElement elem_genba_furigana = xmlDocument.CreateElement("furigana");
                    XmlElement elem_genba_keishou1 = xmlDocument.CreateElement("keishou1");
                    XmlElement elem_genba_keishou2 = xmlDocument.CreateElement("keishou2");
                    XmlElement elem_genba_post = xmlDocument.CreateElement("post");
                    XmlElement elem_genba_todoufuken = xmlDocument.CreateElement("todoufuken");
                    XmlElement elem_genba_address1 = xmlDocument.CreateElement("address1");
                    XmlElement elem_genba_address2 = xmlDocument.CreateElement("address2");
                    XmlElement elem_genba_tel = xmlDocument.CreateElement("tel");
                    XmlElement elem_genba_lat = xmlDocument.CreateElement("lat");
                    XmlElement elem_genba_lon = xmlDocument.CreateElement("lon");
                    XmlElement elem_genba_bikou1 = xmlDocument.CreateElement("bikou1");
                    XmlElement elem_genba_bikou2 = xmlDocument.CreateElement("bikou2");
                    XmlElement elem_genba_bikou3 = xmlDocument.CreateElement("bikou3");
                    XmlElement elem_genba_bikou4 = xmlDocument.CreateElement("bikou4");

                    // XmlNode
                    XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_name1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_name2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_keishou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_keishou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_name1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_name2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_keishou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_keishou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_post = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_todoufuken = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_address1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_address2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_tel = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_lat = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_lon = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou3 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_bikou4 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    // 項目設定
                    node_cd.Value = dataResult.Rows[i]["M_GYOUSHA_CD"].ToString();
                    node_name1.Value = dataResult.Rows[i]["M_GYOUSHA_NAME1"].ToString();
                    node_name2.Value = dataResult.Rows[i]["M_GYOUSHA_NAME2"].ToString();
                    node_nameRyaku.Value = dataResult.Rows[i]["M_GYOUSHA_RYAKU"].ToString();
                    node_furigana.Value = dataResult.Rows[i]["M_GYOUSHA_FURIGANA"].ToString();
                    node_keishou1.Value = dataResult.Rows[i]["M_GYOUSHA_KEISHOU1"].ToString();
                    node_keishou2.Value = dataResult.Rows[i]["M_GYOUSHA_KEISHOU2"].ToString();
                    node_genba_cd.Value = dataResult.Rows[i]["M_GENBA_CD"].ToString();
                    node_genba_name1.Value = dataResult.Rows[i]["M_GENBA_NAME1"].ToString();
                    node_genba_name2.Value = dataResult.Rows[i]["M_GENBA_NAME2"].ToString();
                    node_genba_nameRyaku.Value = dataResult.Rows[i]["M_GENBA_NAME_RYAKU"].ToString();
                    node_genba_furigana.Value = dataResult.Rows[i]["M_GENBA_FURIGANA"].ToString();
                    node_genba_keishou1.Value = dataResult.Rows[i]["M_GENBA_KEISHOU1"].ToString();
                    node_genba_keishou2.Value = dataResult.Rows[i]["M_GENBA_KEISHOU2"].ToString();
                    node_genba_post.Value = dataResult.Rows[i]["M_GENBA_POST"].ToString();
                    node_genba_todoufuken.Value = dataResult.Rows[i]["M_TODOUFUKEN_NAME_RYAKU"].ToString();
                    node_genba_address1.Value = dataResult.Rows[i]["M_GENBA_ADDRESS1"].ToString();
                    node_genba_address2.Value = dataResult.Rows[i]["M_GENBA_ADDRESS2"].ToString();
                    node_genba_tel.Value = dataResult.Rows[i]["M_GENBA_TEL"].ToString();
                    node_genba_bikou1.Value = dataResult.Rows[i]["M_BIKOU1"].ToString();
                    node_genba_bikou2.Value = dataResult.Rows[i]["M_BIKOU2"].ToString();
                    node_genba_bikou3.Value = dataResult.Rows[i]["M_BIKOU3"].ToString();
                    node_genba_bikou4.Value = dataResult.Rows[i]["M_BIKOU4"].ToString();

                    // Node設定
                    elem.AppendChild(elem_kaisyuuGyousha);
                    elem_kaisyuuGyousha.AppendChild(elem_cd);
                    elem_kaisyuuGyousha.AppendChild(elem_name1);
                    elem_kaisyuuGyousha.AppendChild(elem_name2);
                    elem_kaisyuuGyousha.AppendChild(elem_nameRyaku);
                    elem_kaisyuuGyousha.AppendChild(elem_furigana);

                    // 業者敬称1, 2
                    if(false == string.IsNullOrEmpty(node_keishou1.Value))
                    {
                        elem_kaisyuuGyousha.AppendChild(elem_keishou1);
                        elem_keishou1.AppendChild(node_keishou1);
                    }
                    if(false == string.IsNullOrEmpty(node_keishou2.Value))
                    {
                        elem_kaisyuuGyousha.AppendChild(elem_keishou2);
                        elem_keishou2.AppendChild(node_keishou2);
                    }

                    elem_kaisyuuGyousha.AppendChild(elem_genba);
                    elem_genba.AppendChild(elem_genba_cd);
                    elem_genba.AppendChild(elem_genba_name1);
                    elem_genba.AppendChild(elem_genba_name2);
                    elem_genba.AppendChild(elem_genba_nameRyaku);
                    elem_genba.AppendChild(elem_genba_furigana);

                    // 現場敬称1, 2
                    if(false == string.IsNullOrEmpty(node_genba_keishou1.Value))
                    {
                        elem_genba.AppendChild(elem_genba_keishou1);
                        elem_genba_keishou1.AppendChild(node_genba_keishou1);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_keishou2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_keishou2);
                        elem_genba_keishou2.AppendChild(node_genba_keishou2);
                    }

                    // 郵便番号
                    if(false == string.IsNullOrEmpty(node_genba_post.Value))
                    {
                        elem_genba.AppendChild(elem_genba_post);
                        elem_genba_post.AppendChild(node_genba_post);
                    }
                    
                    elem_genba.AppendChild(elem_genba_todoufuken);
                    elem_genba.AppendChild(elem_genba_address1);

                    // 住所2
                    if(false == string.IsNullOrEmpty(node_genba_address2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_address2);
                        elem_genba_address2.AppendChild(node_genba_address2);
                    }

                    // 電話番号
                    if(false == string.IsNullOrEmpty(node_genba_tel.Value))
                    {
                        elem_genba.AppendChild(elem_genba_tel);
                        elem_genba_tel.AppendChild(node_genba_tel);
                    }

                    // 緯度経度
                    if(false == string.IsNullOrEmpty(node_genba_lat.Value))
                    {
                        elem_genba.AppendChild(elem_genba_lat);
                        elem_genba_lat.AppendChild(node_genba_lat);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_lon.Value))
                    {
                        elem_genba.AppendChild(elem_genba_lon);
                        elem_genba_lon.AppendChild(node_genba_lon);
                    }

                    // 備考1～4
                    if(false == string.IsNullOrEmpty(node_genba_bikou1.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou1);
                        elem_genba_bikou1.AppendChild(node_genba_bikou1);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou2.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou2);
                        elem_genba_bikou2.AppendChild(node_genba_bikou2);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou3.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou3);
                        elem_genba_bikou3.AppendChild(node_genba_bikou3);
                    }
                    if(false == string.IsNullOrEmpty(node_genba_bikou4.Value))
                    {
                        elem_genba.AppendChild(elem_genba_bikou4);
                        elem_genba_bikou4.AppendChild(node_genba_bikou4);
                    }
                    
                    elem_cd.AppendChild(node_cd);
                    elem_name1.AppendChild(node_name1);
                    elem_name2.AppendChild(node_name2);
                    elem_nameRyaku.AppendChild(node_nameRyaku);
                    elem_furigana.AppendChild(node_furigana);
                    elem_genba_cd.AppendChild(node_genba_cd);
                    elem_genba_name1.AppendChild(node_genba_name1);
                    elem_genba_name2.AppendChild(node_genba_name2);
                    elem_genba_nameRyaku.AppendChild(node_genba_nameRyaku);
                    elem_genba_furigana.AppendChild(node_genba_furigana);
                    elem_genba_todoufuken.AppendChild(node_genba_todoufuken);
                    elem_genba_address1.AppendChild(node_genba_address1);
                }

                // 業者コード設定
                preGyoushaCode = dataResult.Rows[i]["M_GYOUSHA_CD"].ToString();

            }
            return xmlDocument;
        }

        /// <summary>
        /// 搬入マスタ出力処理
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private void OutPutDisposerData(DataTable dataResult)
        {
            // XML設定
            XmlDocument xmlDocument = OutPutDisposerXML(dataResult);

            // XML出力
            this.saveXML(xmlDocument, this.mobileOutPutMasterPath + "m_Disposer.xml", false);
        }

        /// <summary>
        /// 搬入マスタXML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private static XmlDocument OutPutDisposerXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("masterDisposer");
            XmlElement elem_kaisyuuGyousha = xmlDocument.CreateElement("hannyuuGyousha");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // Loopで業者コードの同一を判別させる為のキー
            String preGyoushaCode = "";

            // 現場マスタ件数取得
            int rowCount = dataResult.Rows.Count;

            // 現場マスタ件数分出力
            for (int i = 0; i < rowCount; i++)
            {

                // 業者コードが同一の場合、現場レコードのみを出力
                // 業者コードが別の場合、業者レコード及び現場レコードを出力

                // XmlElement
                if (preGyoushaCode != dataResult.Rows[i].ItemArray[0].ToString())
                {
                    elem_kaisyuuGyousha = xmlDocument.CreateElement("hannyuuGyousha");
                }
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");
                XmlElement elem_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                XmlElement elem_furigana = xmlDocument.CreateElement("furigana");
                XmlElement elem_genba = xmlDocument.CreateElement("genba");
                XmlElement elem_genba_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_genba_name = xmlDocument.CreateElement("name");
                XmlElement elem_genba_nameRyaku = xmlDocument.CreateElement("nameRyaku");
                XmlElement elem_genba_furigana = xmlDocument.CreateElement("furigana");
                XmlElement elem_genba_post = xmlDocument.CreateElement("post");
                XmlElement elem_genba_todoufuken = xmlDocument.CreateElement("todoufuken");
                XmlElement elem_genba_address1 = xmlDocument.CreateElement("address1");
                XmlElement elem_genba_address2 = xmlDocument.CreateElement("address2");
                XmlElement elem_genba_tel = xmlDocument.CreateElement("tel");
                XmlElement elem_genba_lat = xmlDocument.CreateElement("lat");
                XmlElement elem_genba_lon = xmlDocument.CreateElement("lon");
                XmlElement elem_genba_bikou1 = xmlDocument.CreateElement("bikou1");
                XmlElement elem_genba_bikou2 = xmlDocument.CreateElement("bikou2");
                XmlElement elem_genba_bikou3 = xmlDocument.CreateElement("bikou3");
                XmlElement elem_genba_bikou4 = xmlDocument.CreateElement("bikou4");

                // XmlNode
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_nameRyaku = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_furigana = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_post = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_todoufuken = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_address1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_address2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_tel = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_lat = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_lon = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_bikou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_bikou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_bikou3 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_bikou4 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_cd.Value = dataResult.Rows[i].ItemArray[0].ToString();
                node_name.Value = dataResult.Rows[i].ItemArray[1].ToString();
                node_nameRyaku.Value = dataResult.Rows[i].ItemArray[2].ToString();
                node_furigana.Value = dataResult.Rows[i].ItemArray[3].ToString();
                node_genba_cd.Value = dataResult.Rows[i].ItemArray[4].ToString();
                node_genba_name.Value = dataResult.Rows[i].ItemArray[5].ToString();
                node_genba_nameRyaku.Value = dataResult.Rows[i].ItemArray[6].ToString();
                node_genba_furigana.Value = dataResult.Rows[i].ItemArray[7].ToString();
                node_genba_post.Value = dataResult.Rows[i].ItemArray[8].ToString();
                node_genba_todoufuken.Value = dataResult.Rows[i].ItemArray[9].ToString();
                node_genba_address1.Value = dataResult.Rows[i].ItemArray[10].ToString();
                node_genba_address2.Value = dataResult.Rows[i].ItemArray[11].ToString();
                node_genba_tel.Value = dataResult.Rows[i].ItemArray[12].ToString();
                node_genba_bikou1.Value = dataResult.Rows[i].ItemArray[13].ToString();
                node_genba_bikou2.Value = dataResult.Rows[i].ItemArray[14].ToString();
                node_genba_bikou3.Value = dataResult.Rows[i].ItemArray[15].ToString();
                node_genba_bikou4.Value = dataResult.Rows[i].ItemArray[16].ToString();

                // Node設定
                if (preGyoushaCode != dataResult.Rows[i].ItemArray[0].ToString())
                {
                    elem.AppendChild(elem_kaisyuuGyousha);
                    elem_kaisyuuGyousha.AppendChild(elem_cd);
                    elem_kaisyuuGyousha.AppendChild(elem_name);
                    elem_kaisyuuGyousha.AppendChild(elem_nameRyaku);
                    elem_kaisyuuGyousha.AppendChild(elem_furigana);
                }
                elem_kaisyuuGyousha.AppendChild(elem_genba);
                elem_genba.AppendChild(elem_genba_cd);
                elem_genba.AppendChild(elem_genba_name);
                elem_genba.AppendChild(elem_genba_nameRyaku);
                elem_genba.AppendChild(elem_genba_furigana);
                elem_genba.AppendChild(elem_genba_post);
                elem_genba.AppendChild(elem_genba_todoufuken);
                elem_genba.AppendChild(elem_genba_address1);
                elem_genba.AppendChild(elem_genba_address2);
                elem_genba.AppendChild(elem_genba_tel);
                elem_genba.AppendChild(elem_genba_lat);
                elem_genba.AppendChild(elem_genba_lon);
                elem_genba.AppendChild(elem_genba_bikou1);
                elem_genba.AppendChild(elem_genba_bikou2);
                elem_genba.AppendChild(elem_genba_bikou3);
                elem_genba.AppendChild(elem_genba_bikou4);
                if (preGyoushaCode != dataResult.Rows[i].ItemArray[0].ToString())
                {
                    elem_cd.AppendChild(node_cd);
                    elem_name.AppendChild(node_name);
                    elem_nameRyaku.AppendChild(node_nameRyaku);
                    elem_furigana.AppendChild(node_furigana);
                }
                elem_genba_cd.AppendChild(node_genba_cd);
                elem_genba_name.AppendChild(node_genba_name);
                elem_genba_nameRyaku.AppendChild(node_genba_nameRyaku);
                elem_genba_furigana.AppendChild(node_genba_furigana);
                elem_genba_post.AppendChild(node_genba_post);
                elem_genba_todoufuken.AppendChild(node_genba_todoufuken);
                elem_genba_address1.AppendChild(node_genba_address1);
                elem_genba_address2.AppendChild(node_genba_address2);
                elem_genba_tel.AppendChild(node_genba_tel);
                elem_genba_bikou1.AppendChild(node_genba_bikou1);
                elem_genba_bikou2.AppendChild(node_genba_bikou2);
                elem_genba_bikou3.AppendChild(node_genba_bikou3);
                elem_genba_bikou4.AppendChild(node_genba_bikou4);
                elem_genba_lat.AppendChild(node_genba_lat);
                elem_genba_lon.AppendChild(node_genba_lon);

                // 業者コードが異なる場合、再設定
                if (preGyoushaCode != dataResult.Rows[i].ItemArray[0].ToString())
                {
                    preGyoushaCode = dataResult.Rows[i].ItemArray[0].ToString();
                }



            }
            return xmlDocument;
        }

        /// <summary>
        /// 定期配車出力
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private void OutPutTeikiData(DataTable dataResult)
        {

            // 配車番号ごとの配車データ格納用
            DataTable dataResultDriver = CreateTeikiDataTable();

            // 配車番号ごとの配車データを出力する。
            int rowCount = dataResult.Rows.Count;

            // 配車番号
            string haishaNumber = "";

            // XML設定
            XmlDocument xmlDocument;

            // 定期配車件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // 初回判断用にデータ設定
                if (i == 0)
                {
                    // 配車番号
                    haishaNumber = dataResult.Rows[i]["M_TEIKI_HAISHA_NUMBER"].ToString();
                }

                if (haishaNumber != dataResult.Rows[i]["M_TEIKI_HAISHA_NUMBER"].ToString())
                {
                    // XML設定
                    xmlDocument = OutPutTeikiXML(dataResultDriver);

                    // XML出力
                    this.saveXML(xmlDocument, this.mobileOutPutTransPath + GetFimeNameTeiki(dataResultDriver), false);

                    // 配車番号
                    haishaNumber = dataResult.Rows[i]["M_TEIKI_HAISHA_NUMBER"].ToString();

                    // 出力データの初期化
                    dataResultDriver = CreateTeikiDataTable();
                }

                // データ設定
                DataRow row = dataResultDriver.NewRow();
                row["TEIKI_HAISHA_NUMBER"] = dataResult.Rows[i]["M_TEIKI_HAISHA_NUMBER"].ToString();
                row["COURSE_NAME_CD"] = dataResult.Rows[i]["M_COURSE_NAME_CD"].ToString();
                row["COURSE_NAME"] = dataResult.Rows[i]["M_COURSE_NAME"].ToString();
                row["UNPAN_GYOUSHA_CD"] = dataResult.Rows[i]["M_UNPAN_GYOUSHA_CD"].ToString();
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["M_ROW_NUMBER"].ToString()))
                {
                    row["ROW_NUMBER"] = dataResult.Rows[i]["M_ROW_NUMBER"].ToString();
                }
                row["GYOUSHA_CD"] = dataResult.Rows[i]["M_GYOUSHA_CD"].ToString();
                row["GENBA_CD"] = dataResult.Rows[i]["M_GENBA_CD"].ToString();
                row["HINMEI_CD"] = dataResult.Rows[i]["M_HINMEI_CD"].ToString();
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["M_UNIT_CD"].ToString()))
                {
                    row["UNIT_CD"] = dataResult.Rows[i]["M_UNIT_CD"].ToString();
                }
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["M_SAGYOU_DATE"].ToString()))
                {
                    row["SAGYOU_DATE"] = dataResult.Rows[i]["M_SAGYOU_DATE"].ToString();
                }
                row["UNTENSHA_CD"] = dataResult.Rows[i]["M_UNTENSHA_CD"].ToString();
                row["SHARYOU_CD"] = dataResult.Rows[i]["M_SHARYOU_CD"].ToString();
                row["NIOROSHI_GYOUSHA_CD"] = dataResult.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString();
                row["NIOROSHI_GENBA_CD"] = dataResult.Rows[i]["NIOROSHI_GENBA_CD"].ToString();

                string kansanUnitCd = dataResult.Rows[i]["M_KANSAN_UNIT_CD"].ToString();          // M_KANSAN_UNIT_CD
                if (string.IsNullOrEmpty(kansanUnitCd))
                {
                    row["KANSAN_UNIT_CD"] = DBNull.Value;
                }
                else
                {
                    row["KANSAN_UNIT_CD"] = kansanUnitCd;
                }
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["M_NIOROSHI_NUMBER"].ToString()))
                {
                    row["M_NIOROSHI_NUMBER"] = dataResult.Rows[i]["M_NIOROSHI_NUMBER"].ToString();
                }
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["NIOROSHI_NUMBER"].ToString()))
                {
                    row["NIOROSHI_NUMBER"] = dataResult.Rows[i]["NIOROSHI_NUMBER"].ToString();
                }

                dataResultDriver.Rows.Add(row);

                // 最終データの場合、XML出力してLoop終了
                if (i == rowCount - 1)
                {
                    // XML設定
                    xmlDocument = OutPutTeikiXML(dataResultDriver);

                    // XML出力
                    this.saveXML(xmlDocument, this.mobileOutPutTransPath + GetFimeNameTeiki(dataResultDriver), false);
                    break;

                }
            }

        }

        /// <summary>
        /// 配車番号ごとの配車データ格納用
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTeikiDataTable()
        {
            DataTable dataResultDriver = new DataTable();
            dataResultDriver.Columns.Add("TEIKI_HAISHA_NUMBER", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("COURSE_NAME_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("COURSE_NAME", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNPAN_GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("ROW_NUMBER", Type.GetType("System.Decimal"));//bigint→smallint
            dataResultDriver.Columns.Add("GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("GENBA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("HINMEI_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNIT_CD", Type.GetType("System.Decimal")); //smallint
            dataResultDriver.Columns.Add("SAGYOU_DATE", Type.GetType("System.DateTime"));
            dataResultDriver.Columns.Add("UNTENSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("SHARYOU_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("NIOROSHI_GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("NIOROSHI_GENBA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("KANSAN_UNIT_CD", Type.GetType("System.Decimal")); //smallint
            dataResultDriver.Columns.Add("M_NIOROSHI_NUMBER", Type.GetType("System.Int32"));
            dataResultDriver.Columns.Add("NIOROSHI_NUMBER", Type.GetType("System.Int32"));
            return dataResultDriver;
        }

        /// <summary>
        /// 定期配車データ取得処理
        /// </summary>
        private DataTable GetTeikiData()
        {
            LogUtility.DebugMethodStart();

            // 定期配車データ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();

            // 値があった場合拠点CDを検索条件にいれる。
            if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
            {
                entity.KYOTEN_CD_TEIKI = Int16.Parse(this.form.txt_KyotenCD.Text);
            }

            entity.SAGYOU_DATE_TEIKI_FROM = DateTime.Parse(this.form.dtp_TaishoKikanFrom.Value.ToString()).ToString("yyyy/MM/dd");
            entity.SAGYOU_DATE_TEIKI_TO = DateTime.Parse(this.form.dtp_TaishoKikanTo.Value.ToString()).ToString("yyyy/MM/dd");


            DataTable dataResult = this.dao.GetTeikiDataForEntity(entity);

            LogUtility.DebugMethodEnd(dataResult);
            return dataResult;

        }

        /// <summary>
        /// 定期配車XML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private XmlDocument OutPutTeikiXML(DataTable dataResult)
        {
            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem = xmlDocument.CreateElement("course");
            XmlElement elem_haisha_kbn = xmlDocument.CreateElement("haishaKbn");
            XmlElement elem_genba = xmlDocument.CreateElement("genba");
            XmlElement elem_genba_seq = xmlDocument.CreateElement("seq");
            XmlElement elem_genba_gyousha_cd = xmlDocument.CreateElement("gyoushaCd");
            XmlElement elem_genba_cd = xmlDocument.CreateElement("genbaCd");
            XmlElement elem_genba_mani_no = xmlDocument.CreateElement("maniNo");
            XmlElement elem_genba_genchaku_time_name = xmlDocument.CreateElement("genchakuTimeName");
            XmlElement elem_genba_genchaku_time = xmlDocument.CreateElement("genchakuTime");
            XmlElement elem_genba_hannyuuMeisaiNo = xmlDocument.CreateElement("hannyuuMeisaiNo");
            XmlElement elem_genba_shijijikou1 = xmlDocument.CreateElement("shijiJikou1");
            XmlElement elem_genba_shijijikou2 = xmlDocument.CreateElement("shijiJikou2");
            XmlElement elem_genba_shijijikou3 = xmlDocument.CreateElement("shijiJikou3");
            XmlElement elem_genba_shijijikou4 = xmlDocument.CreateElement("shijiJikou4");
            XmlElement elem_detail = xmlDocument.CreateElement("detail");

            XmlElement elem_hannyuu = xmlDocument.CreateElement("hannyuu");
            XmlElement elem_hannyuu_detail = xmlDocument.CreateElement("detail");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem);

            // Loopで現場コードの同一を判別させる為のキー
            String preGyoushaCode = "";
            String preGenbaCode = "";
            String preRowNumber = "";

            // Loopで搬入明細コードの同一を判別させる為のキー
            String preNioroshiNumber = "";

            // 定期配車件数取得
            int rowCount = dataResult.Rows.Count;

            if (0 < rowCount)
            {
                // コースレコードは、１レコードのみ設定する。
                // XmlElement
                elem_haisha_kbn = xmlDocument.CreateElement("haishaKbn");
                XmlElement elem_no = xmlDocument.CreateElement("no");
                XmlElement elem_cd = xmlDocument.CreateElement("cd");
                XmlElement elem_name = xmlDocument.CreateElement("name");
                XmlElement elem_gyousha_cd = xmlDocument.CreateElement("gyoushaCd");// Add
                XmlElement elem_sharyo_cd = xmlDocument.CreateElement("sharyoCd");// Add

                // XmlNode
                XmlNode node_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_haisha_kbn = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");// Add
                XmlNode node_sharyo_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");// Add

                // 項目設定
                node_no.Value = dataResult.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                node_cd.Value = dataResult.Rows[0]["COURSE_NAME_CD"].ToString();
                node_name.Value = dataResult.Rows[0]["COURSE_NAME"].ToString();
                node_haisha_kbn.Value = "0";
                node_gyousha_cd.Value = dataResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();// Add
                node_sharyo_cd.Value = dataResult.Rows[0]["SHARYOU_CD"].ToString();// Add

                // Node設定
                elem.AppendChild(elem_no);
                elem.AppendChild(elem_cd);
                elem.AppendChild(elem_name);
                elem.AppendChild(elem_haisha_kbn);
                elem.AppendChild(elem_gyousha_cd);// Add
                elem.AppendChild(elem_sharyo_cd);// Add

                elem_no.AppendChild(node_no);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
                elem_haisha_kbn.AppendChild(node_haisha_kbn);
                elem_gyousha_cd.AppendChild(node_gyousha_cd);// Add
                elem_sharyo_cd.AppendChild(node_sharyo_cd);// Add

                // 現場レコード設定
                for (int i = 0; i < rowCount;i++ )
                {
                    // 同一現場で複数明細の場合、現場レコード１に対し、複数明細とする。

                    // XmlElement
                    if (IsDifferentGenba(dataResult.Rows[i], preGyoushaCode, preGenbaCode, preRowNumber))
                    {

                        elem_genba = xmlDocument.CreateElement("genba");
                        elem_genba_seq = xmlDocument.CreateElement("seq");
                        elem_genba_gyousha_cd = xmlDocument.CreateElement("gyoushaCd");
                        elem_genba_cd = xmlDocument.CreateElement("genbaCd");
                        elem_genba_mani_no = xmlDocument.CreateElement("maniNo");
                        elem_genba_genchaku_time_name = xmlDocument.CreateElement("genchakuTimeName");
                        elem_genba_genchaku_time = xmlDocument.CreateElement("genchakuTime");
                        elem_genba_hannyuuMeisaiNo = xmlDocument.CreateElement("hannyuuMeisaiNo");
                        elem_genba_shijijikou1 = xmlDocument.CreateElement("shijiJikou1");
                        elem_genba_shijijikou2 = xmlDocument.CreateElement("shijiJikou2");
                        elem_genba_shijijikou3 = xmlDocument.CreateElement("shijiJikou3");
                        elem_genba_shijijikou4 = xmlDocument.CreateElement("shijiJikou4");
                    }
                    elem_detail = xmlDocument.CreateElement("detail");
                    XmlElement elem_detail_hinmei_cd = xmlDocument.CreateElement("hinmeiCd");
                    XmlElement elem_detail_unit_cd1 = xmlDocument.CreateElement("unitCd1");
                    XmlElement elem_detail_unit_cd2 = xmlDocument.CreateElement("unitCd2");
                    XmlElement elem_detail_shikiichi1 = xmlDocument.CreateElement("shikiichi1");
                    XmlElement elem_detail_shikiichi2 = xmlDocument.CreateElement("shikiichi2");

                    // XmlNode
                    XmlNode node_genba_seq = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_mani_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_genchaku_time_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_genchaku_time = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_hannyuu_meisai_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_shijijikou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_shijijikou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_shijijikou3 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_genba_shijijikou4 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_detail_hinmei_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_detail_unit_cd1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_detail_unit_cd2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_detail_shikiichi1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_detail_shikiichi2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    // 項目設定
                    node_genba_seq.Value = dataResult.Rows[i]["ROW_NUMBER"].ToString();
                    node_genba_gyousha_cd.Value = dataResult.Rows[i]["GYOUSHA_CD"].ToString();
                    node_genba_cd.Value = dataResult.Rows[i]["GENBA_CD"].ToString();
                    node_genba_mani_no.Value = "";
                    node_genba_genchaku_time_name.Value = "";
                    node_genba_genchaku_time.Value = "";
                    node_genba_hannyuu_meisai_no.Value = dataResult.Rows[i]["M_NIOROSHI_NUMBER"].ToString();
                    node_genba_shijijikou1.Value = "";
                    node_genba_shijijikou2.Value = "";
                    node_genba_shijijikou3.Value = "";
                    node_genba_shijijikou4.Value = "";
                    node_detail_hinmei_cd.Value = dataResult.Rows[i]["HINMEI_CD"].ToString();
                    node_detail_unit_cd1.Value = dataResult.Rows[i]["UNIT_CD"].ToString();
                    node_detail_unit_cd2.Value = dataResult.Rows[i]["KANSAN_UNIT_CD"].ToString();   // KANSAN_UNIT_CD
                    node_detail_shikiichi1.Value = "";
                    node_detail_shikiichi2.Value = "";

                    // Node設定
                    if (IsDifferentGenba(dataResult.Rows[i], preGyoushaCode, preGenbaCode, preRowNumber))
                    {
                        elem.AppendChild(elem_genba);
                        elem_genba.AppendChild(elem_genba_seq);
                        elem_genba.AppendChild(elem_genba_gyousha_cd);
                        elem_genba.AppendChild(elem_genba_cd);
                        elem_genba.AppendChild(elem_genba_mani_no);
                        elem_genba.AppendChild(elem_genba_genchaku_time_name);
                        elem_genba.AppendChild(elem_genba_genchaku_time);
                        elem_genba.AppendChild(elem_genba_hannyuuMeisaiNo);
                        elem_genba.AppendChild(elem_genba_shijijikou1);
                        elem_genba.AppendChild(elem_genba_shijijikou2);
                        elem_genba.AppendChild(elem_genba_shijijikou3);
                        elem_genba.AppendChild(elem_genba_shijijikou4);
                        elem_genba_seq.AppendChild(node_genba_seq);
                        elem_genba_gyousha_cd.AppendChild(node_genba_gyousha_cd);
                        elem_genba_cd.AppendChild(node_genba_cd);
                        elem_genba_mani_no.AppendChild(node_genba_mani_no);
                        elem_genba_genchaku_time_name.AppendChild(node_genba_genchaku_time_name);
                        elem_genba_genchaku_time.AppendChild(node_genba_genchaku_time);
                        elem_genba_hannyuuMeisaiNo.AppendChild(node_genba_hannyuu_meisai_no);
                        elem_genba_shijijikou1.AppendChild(node_genba_shijijikou1);
                        elem_genba_shijijikou2.AppendChild(node_genba_shijijikou2);
                        elem_genba_shijijikou3.AppendChild(node_genba_shijijikou3);
                        elem_genba_shijijikou4.AppendChild(node_genba_shijijikou4);
                    }
                    elem_genba.AppendChild(elem_detail);
                    elem_detail.AppendChild(elem_detail_hinmei_cd);
                    elem_detail.AppendChild(elem_detail_unit_cd1);
                    elem_detail.AppendChild(elem_detail_unit_cd2);
                    elem_detail.AppendChild(elem_detail_shikiichi1);
                    elem_detail.AppendChild(elem_detail_shikiichi2);
                    elem_detail_hinmei_cd.AppendChild(node_detail_hinmei_cd);
                    elem_detail_unit_cd1.AppendChild(node_detail_unit_cd1);
                    elem_detail_unit_cd2.AppendChild(node_detail_unit_cd2);
                    elem_detail_shikiichi1.AppendChild(node_detail_shikiichi1);
                    elem_detail_shikiichi2.AppendChild(node_detail_shikiichi2);

                    // 現場コードが異なる場合、再設定
                    if (IsDifferentGenba(dataResult.Rows[i], preGyoushaCode, preGenbaCode, preRowNumber))
                    {
                        preGyoushaCode = dataResult.Rows[i]["GYOUSHA_CD"].ToString();
                        preGenbaCode = dataResult.Rows[i]["GENBA_CD"].ToString();
                        preRowNumber = dataResult.Rows[i]["ROW_NUMBER"].ToString();
                    }
                }

                // 搬入明細レコード
                for (int i = 0; i < rowCount; i++)
                {
                    if (i == 0)
                    {
                        elem_hannyuu = xmlDocument.CreateElement("hannyuu");
                        elem.AppendChild(elem_hannyuu);
                    }

                    if (!IsDifferentNioroshi(dataResult.Rows[i], preNioroshiNumber))
                    {
                        // 同じ荷卸番号なら次レコード
                        continue;
                    }

                    // XmlElement
                    elem_hannyuu_detail = xmlDocument.CreateElement("detail");
                    XmlElement elem_hannyuu_detail_hannyuu_meisai_no = xmlDocument.CreateElement("hannyuuMeisaiNo");
                    XmlElement elem_hannyuu_detail_hannyuu_gyousha_cd = xmlDocument.CreateElement("hannyuuGyoushaCd");
                    XmlElement elem_hannyuu_detail_hannyuu_genba_cd = xmlDocument.CreateElement("hannyuuGenbaCd");
                    XmlElement elem_hannyuu_detail_hannyuu_date = xmlDocument.CreateElement("hannyuuDate");

                    // XmlNode
                    XmlNode node_hannyuu_detail_hannyuu_meisai_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_hannyuu_detail_hannyuu_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_hannyuu_detail_hannyuu_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_hannyuu_detail_hannyuu_date = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    // 項目設定
                    node_hannyuu_detail_hannyuu_meisai_no.Value = dataResult.Rows[i]["NIOROSHI_NUMBER"].ToString(); ;
                    node_hannyuu_detail_hannyuu_gyousha_cd.Value = dataResult.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString(); ;
                    node_hannyuu_detail_hannyuu_genba_cd.Value = dataResult.Rows[i]["NIOROSHI_GENBA_CD"].ToString(); ;
                    node_hannyuu_detail_hannyuu_date.Value = "";

                    // Node設定
                    elem_hannyuu.AppendChild(elem_hannyuu_detail);
                    elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_meisai_no);
                    elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_gyousha_cd);
                    elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_genba_cd);
                    elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_date);
                    elem_hannyuu_detail_hannyuu_meisai_no.AppendChild(node_hannyuu_detail_hannyuu_meisai_no);
                    elem_hannyuu_detail_hannyuu_gyousha_cd.AppendChild(node_hannyuu_detail_hannyuu_gyousha_cd);
                    elem_hannyuu_detail_hannyuu_genba_cd.AppendChild(node_hannyuu_detail_hannyuu_genba_cd);
                    elem_hannyuu_detail_hannyuu_date.AppendChild(node_hannyuu_detail_hannyuu_date);

                    preNioroshiNumber = dataResult.Rows[i]["NIOROSHI_NUMBER"].ToString();
                }

            }

            return xmlDocument;

        }

        /// <summary>
        /// 別現場か判定
        /// </summary>
        /// <param name="row">対象レコード</param>
        /// <param name="preGyoushaCode">前回業者CD</param>
        /// <param name="preGenbaCode">前回現場CD</param>
        /// <param name="preRowNumber">前回行番号</param>
        /// <returns></returns>
        private bool IsDifferentGenba(DataRow row, string preGyoushaCode, string preGenbaCode, string preRowNumber = "")
        {
            if (preGyoushaCode.Equals(row["GYOUSHA_CD"].ToString())
                && preGenbaCode.Equals(row["GENBA_CD"].ToString())
                && preRowNumber.Equals(row["ROW_NUMBER"].ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 別搬入明細か判定
        /// </summary>
        /// <param name="row">対象レコード</param>
        /// <param name="preNioroshiNumber">前回荷降No</param>
        /// <returns></returns>
        private bool IsDifferentNioroshi(DataRow row, string preNioroshiNumber)
        {
            if (preNioroshiNumber.Equals(row["NIOROSHI_NUMBER"].ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 定期配車ファイル名設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private String GetFimeNameTeiki(DataTable dataResult)
        {
            String fimeName = "";

            // 日付変換
            DateTime dt = (DateTime)dataResult.Rows[0]["SAGYOU_DATE"];
            string outSagyobi = dt.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(dataResult.Rows[0]["UNTENSHA_CD"].ToString()))
            {
                // ファイル名を設定する。「tn_YYYYMMDD_配車区分_伝票番号」
                fimeName = "tn_" + outSagyobi + "_" + "0" + "_"
                    + dataResult.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString() + ".xml";
            }
            else
            {
                // ファイル名を設定する。「tn_YYYYMMDD_配車区分_伝票番号_ドライバーCD」
                fimeName = "tn_" + outSagyobi + "_" + "0" + "_"
                    + dataResult.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString() + "_" + dataResult.Rows[0]["UNTENSHA_CD"].ToString() + ".xml";
            }

            return fimeName;

        }

        /// <summary>
        /// スポット配車（収集）出力
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private void OutPutUketsukeSSData(DataTable dataResult)
        {
            // 受付番号ごとの配車データ格納用
            DataTable dataResultDriver = CreateUketsukeSSDataTable();

            // 受付番号ごとの配車データを出力する。
            int rowCount = dataResult.Rows.Count;

            // 受付番号
            string uketsukeNumber = "";

            // XML設定
            XmlDocument xmlDocument;

            var parentForm = (BusinessBaseForm)this.form.Parent;

            DateTime dt = parentForm.sysDate;

            // スポット配車（収集）件数分出力
            for (int i = 0; i < rowCount; i++)
            {
                // 初回判断用にデータ設定
                if (i == 0)
                {
                    // 受付番号
                    uketsukeNumber = dataResult.Rows[i]["UKETSUKE_NUMBER"].ToString();
                }

                if (uketsukeNumber != dataResult.Rows[i]["UKETSUKE_NUMBER"].ToString())
                {
                    // XML設定
                    xmlDocument = OutPutUketsukeSSXML(dataResultDriver);

                    // XML出力
                    this.saveXML(xmlDocument, this.mobileOutPutTransPath + GetFimeNameUketsukeSS(dataResultDriver), false);

                    // 受付番号
                    uketsukeNumber = dataResult.Rows[i]["UKETSUKE_NUMBER"].ToString();

                    // 初期化（配車データ格納用DataTable）
                    dataResultDriver = CreateUketsukeSSDataTable();
                }

                // データ設定
                DataRow row = dataResultDriver.NewRow();
                row["UKETSUKE_NUMBER"] = dataResult.Rows[i]["UKETSUKE_NUMBER"].ToString();
                row["GYOUSHA_CD"] = dataResult.Rows[i]["GYOUSHA_CD"].ToString();
                row["GENBA_CD"] = dataResult.Rows[i]["GENBA_CD"].ToString();
                row["HINMEI_CD"] = dataResult.Rows[i]["HINMEI_CD"].ToString();
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["UNIT_CD"].ToString()))
                {
                    row["UNIT_CD"] = dataResult.Rows[i]["UNIT_CD"].ToString();
                }
                row["UNTENSHA_CD"] = dataResult.Rows[i]["UNTENSHA_CD"].ToString();
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["SAGYOU_DATE"].ToString()))
                {
                    row["SAGYOU_DATE"] = dataResult.Rows[i]["SAGYOU_DATE"].ToString();
                }
                row["UNPAN_GYOUSHA_CD"] = dataResult.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                row["SHARYOU_CD"] = dataResult.Rows[i]["SHARYOU_CD"].ToString();
                row["SYSTEM_ID"] = dataResult.Rows[i]["SYSTEM_ID"].ToString();
                row["SEQ"] = dataResult.Rows[i]["SEQ"].ToString();
                row["NIOROSHI_GYOUSHA_CD"] = dataResult.Rows[i]["NIOROSHI_GYOUSHA_CD"].ToString();
                row["NIOROSHI_GENBA_CD"] = dataResult.Rows[i]["NIOROSHI_GENBA_CD"].ToString();
                row["GENCHAKU_TIME_NAME"] = dataResult.Rows[i]["GENCHAKU_TIME_NAME"].ToString();
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["GENCHAKU_TIME"].ToString()))
                {
                    dt = new DateTime(0).Add((TimeSpan)dataResult.Rows[i]["GENCHAKU_TIME"]);
                    row["GENCHAKU_TIME"] = dt.ToShortTimeString();
                }
                if (false == string.IsNullOrEmpty(dataResult.Rows[i]["NIOROSHI_DATE"].ToString()))
                {
                    dt = (DateTime)dataResult.Rows[i]["NIOROSHI_DATE"];
                    row["NIOROSHI_DATE"] = dt.ToShortDateString();
                }
                row["UNTENSHA_SIJIJIKOU1"] = dataResult.Rows[i]["UNTENSHA_SIJIJIKOU1"].ToString();
                row["UNTENSHA_SIJIJIKOU2"] = dataResult.Rows[i]["UNTENSHA_SIJIJIKOU2"].ToString();
                row["UNTENSHA_SIJIJIKOU3"] = dataResult.Rows[i]["UNTENSHA_SIJIJIKOU3"].ToString();

                dataResultDriver.Rows.Add(row);

                // 最終データの場合、XML出力してLoop終了
                if (i == rowCount - 1)
                {
                    // XML設定
                    xmlDocument = OutPutUketsukeSSXML(dataResultDriver);

                    // XML出力
                    this.saveXML(xmlDocument, this.mobileOutPutTransPath + GetFimeNameUketsukeSS(dataResultDriver), false);
                    break;
                }
            }
        }

        /// <summary>
        /// 受付番号ごとの配車データ格納用
        /// </summary>
        /// <returns></returns>
        private DataTable CreateUketsukeSSDataTable()
        {
            DataTable dataResultDriver = new DataTable();
            dataResultDriver.Columns.Add("UKETSUKE_NUMBER", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("GENBA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("HINMEI_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNIT_CD", Type.GetType("System.Decimal")); //smallint
            dataResultDriver.Columns.Add("UNTENSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("SAGYOU_DATE", Type.GetType("System.DateTime"));
            dataResultDriver.Columns.Add("UNPAN_GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("SHARYOU_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("SEQ", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("NIOROSHI_GYOUSHA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("NIOROSHI_GENBA_CD", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("GENCHAKU_TIME_NAME", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("GENCHAKU_TIME", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("NIOROSHI_DATE", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNTENSHA_SIJIJIKOU1", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNTENSHA_SIJIJIKOU2", Type.GetType("System.String"));
            dataResultDriver.Columns.Add("UNTENSHA_SIJIJIKOU3", Type.GetType("System.String"));
            return dataResultDriver;
        }

        /// <summary>
        /// スポット配車（収集）データ取得処理
        /// </summary>
		/// <param name="haishaJokyo">配車状況</param>
		private DataTable GetUketsukeSS(Int16 haishaJokyo)
        {
            LogUtility.DebugMethodStart();

            // スポット配車（収集）データ取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();

            // 検索条件（拠点CD）
            if (!string.IsNullOrEmpty(this.form.txt_KyotenCD.Text))
            {
                entity.KYOTEN_CD_SPOT_SS = Int16.Parse(this.form.txt_KyotenCD.Text);
            }

            // 検索条件（作業日）
            entity.SAGYOU_DATE_SPOT_SS_FROM = DateTime.Parse(this.form.dtp_TaishoKikanFrom.Value.ToString()).ToString("yyyy/MM/dd");
            entity.SAGYOU_DATE_SPOT_SS_TO = DateTime.Parse(this.form.dtp_TaishoKikanTo.Value.ToString()).ToString("yyyy/MM/dd");

			// 配車状況CD
			entity.HAISHA_JOKYO_CD_SPOT_SS = haishaJokyo;

            DataTable dataResult = this.dao.GetUketsukeSSDataForEntity(entity);

            LogUtility.DebugMethodEnd(dataResult);

            return dataResult;

        }

        /// <summary>
        /// スポット配車（収集）XML設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private XmlDocument OutPutUketsukeSSXML(DataTable dataResult)
        {

            // XmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // XML宣言
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration(@"1.0", @"UTF-8", "yes");

            // XmlElement
            XmlElement elem_course = xmlDocument.CreateElement("course");
            XmlElement elem_no = xmlDocument.CreateElement("no");
            XmlElement elem_cd = xmlDocument.CreateElement("cd");
            XmlElement elem_name = xmlDocument.CreateElement("name");
            XmlElement elem_gyousha_cd = xmlDocument.CreateElement("gyoushaCd");
            XmlElement elem_sharyo_cd = xmlDocument.CreateElement("sharyoCd");
            XmlElement elem_haisha_kbn = xmlDocument.CreateElement("haishaKbn");
            XmlElement elem_genba = xmlDocument.CreateElement("genba");
            XmlElement elem_genba_seq = xmlDocument.CreateElement("seq");
            XmlElement elem_genba_gyousha_cd = xmlDocument.CreateElement("gyoushaCd");
            XmlElement elem_genba_cd = xmlDocument.CreateElement("genbaCd");
            XmlElement elem_genba_mani_no = xmlDocument.CreateElement("maniNo");
            XmlElement elem_genba_genchaku_time_name = xmlDocument.CreateElement("genchakuTimeName");
            XmlElement elem_genba_genchaku_time = xmlDocument.CreateElement("genchakuTime");
            XmlElement elem_genba_hannyuu_meisai_no = xmlDocument.CreateElement("hannyuuMeisaiNo");
            XmlElement elem_genba_shijijikou1 = xmlDocument.CreateElement("shijiJikou1");
            XmlElement elem_genba_shijijikou2 = xmlDocument.CreateElement("shijiJikou2");
            XmlElement elem_genba_shijijikou3 = xmlDocument.CreateElement("shijiJikou3");
            XmlElement elem_genba_shijijikou4 = xmlDocument.CreateElement("shijiJikou4");

            // Node設定
            xmlDocument.AppendChild(declaration);
            xmlDocument.AppendChild(elem_course);

            // データ件数取得
            int rowCount = dataResult.Rows.Count;

            // add 2013/10/28 start　★

            // コンテナ情報取得
            MobileShougunShutsuryokuDTOClass entity = new MobileShougunShutsuryokuDTOClass();
            // 検索条件設定
            entity.SYSTEM_ID = dataResult.Rows[0]["SYSTEM_ID"].ToString();
            entity.SEQ = Int16.Parse(dataResult.Rows[0]["SEQ"].ToString());
            // 結果取得
            DataTable dataResultContena = this.dao.GetUketsukeSSContenaDataForEntity(entity);
            // 件数取得
            int count = dataResultContena.Rows.Count;

            if (0 < rowCount)
            {
                // コースレコード・現場レコードは、１レコードのみ設定する。
                // XmlNode
                XmlNode node_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_sharyo_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_haisha_kbn = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_seq = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_mani_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_genchaku_time_name = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_genchaku_time = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_hannyuu_meisai_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_shijijikou1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_shijijikou2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_shijijikou3 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_genba_shijijikou4 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_no.Value = dataResult.Rows[0]["UKETSUKE_NUMBER"].ToString();
                node_cd.Value = "";
                node_name.Value = "";
                node_haisha_kbn.Value = "1";
                node_gyousha_cd.Value = dataResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                node_sharyo_cd.Value = dataResult.Rows[0]["SHARYOU_CD"].ToString();
                node_genba_seq.Value = "001";
                node_genba_gyousha_cd.Value = dataResult.Rows[0]["GYOUSHA_CD"].ToString();
                node_genba_cd.Value = dataResult.Rows[0]["GENBA_CD"].ToString();
                node_genba_mani_no.Value = "";
                node_genba_genchaku_time_name.Value = dataResult.Rows[0]["GENCHAKU_TIME_NAME"].ToString();
                node_genba_genchaku_time.Value = dataResult.Rows[0]["GENCHAKU_TIME"].ToString();
                node_genba_hannyuu_meisai_no.Value = "1";
                node_genba_shijijikou1.Value = dataResult.Rows[0]["UNTENSHA_SIJIJIKOU1"].ToString();
                node_genba_shijijikou2.Value = dataResult.Rows[0]["UNTENSHA_SIJIJIKOU2"].ToString();
                node_genba_shijijikou3.Value = dataResult.Rows[0]["UNTENSHA_SIJIJIKOU3"].ToString();
                node_genba_shijijikou4.Value = "";

                // Node設定
                elem_course.AppendChild(elem_no);
                elem_course.AppendChild(elem_cd);
                elem_course.AppendChild(elem_name);
                elem_course.AppendChild(elem_gyousha_cd);
                elem_course.AppendChild(elem_sharyo_cd);
                elem_course.AppendChild(elem_haisha_kbn);
                elem_course.AppendChild(elem_genba);
                elem_genba.AppendChild(elem_genba_seq);
                elem_genba.AppendChild(elem_genba_cd);//add 2013/10/10
                elem_genba.AppendChild(elem_genba_gyousha_cd);
                elem_genba.AppendChild(elem_genba_mani_no);
                elem_genba.AppendChild(elem_genba_genchaku_time_name);
                elem_genba.AppendChild(elem_genba_genchaku_time);
                elem_genba.AppendChild(elem_genba_hannyuu_meisai_no);
                elem_genba.AppendChild(elem_genba_shijijikou1);
                elem_genba.AppendChild(elem_genba_shijijikou2);
                elem_genba.AppendChild(elem_genba_shijijikou3);
                elem_genba.AppendChild(elem_genba_shijijikou4);

                elem_no.AppendChild(node_no);
                elem_cd.AppendChild(node_cd);
                elem_name.AppendChild(node_name);
                elem_gyousha_cd.AppendChild(node_gyousha_cd);
                elem_sharyo_cd.AppendChild(node_sharyo_cd);
                elem_haisha_kbn.AppendChild(node_haisha_kbn);
                elem_genba_seq.AppendChild(node_genba_seq);
                elem_genba_gyousha_cd.AppendChild(node_genba_gyousha_cd);
                elem_genba_cd.AppendChild(node_genba_cd);
                elem_genba_mani_no.AppendChild(node_genba_mani_no);
                elem_genba_genchaku_time_name.AppendChild(node_genba_genchaku_time_name);
                elem_genba_genchaku_time.AppendChild(node_genba_genchaku_time);
                elem_genba_hannyuu_meisai_no.AppendChild(node_genba_hannyuu_meisai_no);
                elem_genba_shijijikou1.AppendChild(node_genba_shijijikou1);
                elem_genba_shijijikou2.AppendChild(node_genba_shijijikou2);
                elem_genba_shijijikou3.AppendChild(node_genba_shijijikou3);
                elem_genba_shijijikou4.AppendChild(node_genba_shijijikou4);

                // 明細レコード
                for (int i = 0; i < rowCount; i++)
                {
                    // XmlElement
                    XmlElement elem_detail = xmlDocument.CreateElement("detail");
                    XmlElement elem_hinmei_cd = xmlDocument.CreateElement("hinmeiCd");
                    XmlElement elem_unitCd1 = xmlDocument.CreateElement("unitCd1");
                    XmlElement elem_unitCd2 = xmlDocument.CreateElement("unitCd2");
                    XmlElement elem_shikiichi1 = xmlDocument.CreateElement("shikiichi1");
                    XmlElement elem_shikiichi2 = xmlDocument.CreateElement("shikiichi2");

                    // XmlNode
                    XmlNode node_hinmei_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_unitCd1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_unitCd2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shikiichi1 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shikiichi2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    // 項目設定
                    node_hinmei_cd.Value = dataResult.Rows[i]["HINMEI_CD"].ToString();
                    node_unitCd1.Value = dataResult.Rows[i]["UNIT_CD"].ToString();
                    node_unitCd2.Value = "";
                    node_shikiichi1.Value = "";
                    node_shikiichi2.Value = "";

                    // Node設定
                    elem_genba.AppendChild(elem_detail);
                    elem_detail.AppendChild(elem_hinmei_cd);
                    elem_detail.AppendChild(elem_unitCd1);
                    elem_detail.AppendChild(elem_unitCd2);
                    elem_detail.AppendChild(elem_shikiichi1);
                    elem_detail.AppendChild(elem_shikiichi2);

                    elem_hinmei_cd.AppendChild(node_hinmei_cd);
                    elem_unitCd1.AppendChild(node_unitCd1);
                    elem_unitCd2.AppendChild(node_unitCd2);
                    elem_shikiichi1.AppendChild(node_shikiichi1);
                    elem_shikiichi2.AppendChild(node_shikiichi2);
                }

                // コンテナレコード
                for (int i = 0; i < count; i++)
                {
                    // XmlElement
                    // add 2013/10/28 start
                    XmlElement elem_containerDetail = xmlDocument.CreateElement("containerDetail");
                    XmlElement elem_shuruiCd = xmlDocument.CreateElement("shuruiCd");
                    XmlElement elem_shuruiName = xmlDocument.CreateElement("shuruiName");
                    XmlElement elem_setti = xmlDocument.CreateElement("setti");
                    XmlElement elem_yoteiNum = xmlDocument.CreateElement("yoteiNum");
                    XmlElement elem_containerCd = xmlDocument.CreateElement("containerCd");

                    XmlElement elem_containerDetail2 = xmlDocument.CreateElement("containerDetail");
                    XmlElement elem_shuruiCd2 = xmlDocument.CreateElement("shuruiCd");
                    XmlElement elem_shuruiName2 = xmlDocument.CreateElement("shuruiName");
                    XmlElement elem_hikiage = xmlDocument.CreateElement("hikiage");
                    XmlElement elem_yoteiNum2 = xmlDocument.CreateElement("yoteiNum");
                    XmlElement elem_containerCd2 = xmlDocument.CreateElement("containerCd");
                    // add 2013/10/28 end

                    // XmlNode
                    // add 2013/10/28 start
                    XmlNode node_containerDetail = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shuruiCd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shuruiName = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_setti = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_yoteiNum = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_containerCd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                    XmlNode node_containerDetail2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shuruiCd2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_shuruiName2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_hikiage = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_yoteiNum2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    XmlNode node_containerCd2 = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                    // add 2013/10/28 end

                    // 項目設定
                    // add 2013/10/28 start　★
                    if ("1" == dataResultContena.Rows[i]["CONTENA_SET_KBN"].ToString())
                    {
                        // 設置
                        elem_genba.AppendChild(elem_containerDetail);
                        elem_containerDetail.AppendChild(elem_shuruiCd);
                        elem_containerDetail.AppendChild(elem_shuruiName);
                        elem_containerDetail.AppendChild(elem_setti);
                        elem_setti.AppendChild(elem_yoteiNum);

                        node_shuruiCd.Value = dataResultContena.Rows[i]["CONTENA_SHURUI_CD"].ToString();
                        node_shuruiName.Value = dataResultContena.Rows[i]["CONTENA_SHURUI_NAME"].ToString();
                        node_yoteiNum.Value = dataResultContena.Rows[i]["DAISUU_CNT"].ToString();

                        elem_shuruiCd.AppendChild(node_shuruiCd);
                        elem_shuruiName.AppendChild(node_shuruiName);
                        elem_yoteiNum.AppendChild(node_yoteiNum);

                        // 予定コンテナCDは空の場合、タグ自体出力させない
                        if (!string.IsNullOrEmpty(dataResultContena.Rows[i]["CONTENA_CD"].ToString()))
                        {
                            elem_setti.AppendChild(elem_containerCd);
                            node_containerCd.Value = dataResultContena.Rows[i]["CONTENA_CD"].ToString();
                            elem_containerCd.AppendChild(node_containerCd);
                        }
                    }
                    if ("2" == dataResultContena.Rows[i]["CONTENA_SET_KBN"].ToString())
                    {
                        // 引揚
                        elem_genba.AppendChild(elem_containerDetail2);
                        elem_containerDetail2.AppendChild(elem_shuruiCd2);
                        elem_containerDetail2.AppendChild(elem_shuruiName2);
                        elem_containerDetail2.AppendChild(elem_hikiage);
                        elem_hikiage.AppendChild(elem_yoteiNum2);

                        node_shuruiCd2.Value = dataResultContena.Rows[i]["CONTENA_SHURUI_CD"].ToString();
                        node_shuruiName2.Value = dataResultContena.Rows[i]["CONTENA_SHURUI_NAME"].ToString();
                        node_yoteiNum2.Value = dataResultContena.Rows[i]["DAISUU_CNT"].ToString();

                        elem_shuruiCd2.AppendChild(node_shuruiCd2);
                        elem_shuruiName2.AppendChild(node_shuruiName2);
                        elem_yoteiNum2.AppendChild(node_yoteiNum2);

                        // 予定コンテナCDは空の場合、タグ自体出力させない
                        if (!string.IsNullOrEmpty(dataResultContena.Rows[i]["CONTENA_CD"].ToString()))
                        {
                            elem_hikiage.AppendChild(elem_containerCd2);
                            node_containerCd2.Value = dataResultContena.Rows[i]["CONTENA_CD"].ToString();
                            elem_containerCd2.AppendChild(node_containerCd2);
                        }
                    }
                    // add 2013/10/28 end
                }

                // 搬入明細(1レコード固定)
                // XmlElement
                XmlElement elem_hannyuu = xmlDocument.CreateElement("hannyuu");
                XmlElement elem_hannyuu_detail = xmlDocument.CreateElement("detail");
                XmlElement elem_hannyuu_detail_hannyuu_meisai_no = xmlDocument.CreateElement("hannyuuMeisaiNo");
                XmlElement elem_hannyuu_detail_hannyuu_gyousha_cd = xmlDocument.CreateElement("hannyuuGyoushaCd");
                XmlElement elem_hannyuu_detail_hannyuu_genba_cd = xmlDocument.CreateElement("hannyuuGenbaCd");
                XmlElement elem_hannyuu_detail_hannyuu_date = xmlDocument.CreateElement("hannyuuDate");

                // XmlNode
                XmlNode node_hannyuu_detail_hannyuu_meisai_no = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_hannyuu_detail_hannyuu_gyousha_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_hannyuu_detail_hannyuu_genba_cd = xmlDocument.CreateNode(XmlNodeType.Text, "", "");
                XmlNode node_hannyuu_detail_hannyuu_date = xmlDocument.CreateNode(XmlNodeType.Text, "", "");

                // 項目設定
                node_hannyuu_detail_hannyuu_meisai_no.Value = "1";
                node_hannyuu_detail_hannyuu_gyousha_cd.Value = dataResult.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                node_hannyuu_detail_hannyuu_genba_cd.Value = dataResult.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                node_hannyuu_detail_hannyuu_date.Value = dataResult.Rows[0]["NIOROSHI_DATE"].ToString();

                // Node設定
                elem_course.AppendChild(elem_hannyuu);
                elem_hannyuu.AppendChild(elem_hannyuu_detail);
                elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_meisai_no);
                elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_gyousha_cd);
                elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_genba_cd);
                elem_hannyuu_detail.AppendChild(elem_hannyuu_detail_hannyuu_date);

                elem_hannyuu_detail_hannyuu_meisai_no.AppendChild(node_hannyuu_detail_hannyuu_meisai_no);
                elem_hannyuu_detail_hannyuu_gyousha_cd.AppendChild(node_hannyuu_detail_hannyuu_gyousha_cd);
                elem_hannyuu_detail_hannyuu_genba_cd.AppendChild(node_hannyuu_detail_hannyuu_genba_cd);
                elem_hannyuu_detail_hannyuu_date.AppendChild(node_hannyuu_detail_hannyuu_date);
            }

            return xmlDocument;
        }

        /// <summary>
        /// スポット配車（収集）ファイル名設定
        /// </summary>
        /// <param name="DataTable">データ検索結果</param>
        private String GetFimeNameUketsukeSS(DataTable dataResult)
        {
            String fimeName = "";

            // 日付変換
            DateTime dt = (DateTime)dataResult.Rows[0]["SAGYOU_DATE"];
            string outSagyobi = dt.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(dataResult.Rows[0]["UNTENSHA_CD"].ToString()))
            {
                // ファイル名を設定する。「tn_YYYYMMDD_配車区分_伝票番号」
                fimeName = "tn_" + outSagyobi + "_" + "1" + "_"
                    + dataResult.Rows[0]["UKETSUKE_NUMBER"].ToString() + ".xml";
            }
            else
            {
                // ファイル名を設定する。「tn_YYYYMMDD_配車区分_伝票番号_ドライバーCD」
                fimeName = "tn_" + outSagyobi + "_" + "1" + "_"
                    + dataResult.Rows[0]["UKETSUKE_NUMBER"].ToString() + "_" + dataResult.Rows[0]["UNTENSHA_CD"].ToString() + ".xml";

            }

            return fimeName;

        }

        /// <summary>
        /// XMLの保存を行う
        /// </summary>
        /// <param name="xmlDoc">XmlDocument</param>
        /// <param name="savePath">出力先ファイルパス</param>
        /// <param name="useBOM">TRUE:BOMあり, FALSE:BOMなし</param>
        private void saveXML(XmlDocument xmlDoc, string savePath, bool useBOM)
        {
            if(useBOM == true)
            {
                // BOMありで出力
                xmlDoc.Save(savePath);
            }
            else
            {
                // BOMなしで出力
                var xmlWriter = new StreamWriter(savePath, false, new UTF8Encoding());
                xmlDoc.Save(xmlWriter);
                xmlWriter.Close();
                xmlWriter.Dispose();
            }
        }

        /// 20141128 Houkakou 「モバイル将軍出力」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaishoKikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var TaishoKikanFromTextBox = this.form.dtp_TaishoKikanFrom;
            var TaishoKikanToTextBox = this.form.dtp_TaishoKikanTo;

            TaishoKikanToTextBox.Text = TaishoKikanFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「モバイル将軍出力」のダブルクリックを追加する　end

        /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　start
        #region TaishoKikanFrom_Leaveイベント
        /// <summary>
        /// TaishoKikanFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TaishoKikanFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_TaishoKikanFrom.Text))
            {
                this.form.dtp_TaishoKikanTo.IsInputErrorOccured = false;
                this.form.dtp_TaishoKikanTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region TaishoKikanTo_Leaveイベント
        /// <summary>
        /// TaishoKikanTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TaishoKikanTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_TaishoKikanTo.Text))
            {
                this.form.dtp_TaishoKikanFrom.IsInputErrorOccured = false;
                this.form.dtp_TaishoKikanFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141209 teikyou 「モバイル将軍出力」の日付チェックを追加する　end
    }
}