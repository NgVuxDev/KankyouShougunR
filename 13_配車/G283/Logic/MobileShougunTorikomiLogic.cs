// $Id: LogicClass.cs 24789 2014-07-07 01:25:23Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Allocation.MobileShougunTorikomi.APP;
using Shougun.Core.Allocation.MobileShougunTorikomi.CONST;
using Shougun.Core.Allocation.MobileShougunTorikomi.DAO;
using Shougun.Core.Allocation.MobileShougunTorikomi.DTO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.Logic
{
	/// <summary>ビジネスロジック</summary>
	internal class MobileShougunTorikomiLogic : IBuisinessLogic
	{
		#region - Fields -

        /// <summary>ベースフォーム</summary>
        private BusinessBaseForm parentForm;
        
        /// <summary>コントロールのユーティリティ</summary>
		public ControlUtility controlUtil = new ControlUtility();

		/// <summary>現在表示中の一覧</summary>
		public DataRow[] selectedRowsIchiran;

		/// <summary>検索結果件数（有効データ(DELETE_FLG=false)）</summary>
		public int YuukouData_count;

		/// <summary>検索結果件数（定期データ）</summary>
		public int teikiData_count;

		/// <summary>検索結果件数（スポットデータ）</summary>
		public int spotData_count;

		/// <summary>検索結果件数（コンテナデータ）</summary>
		public int contenaData_count;

		/// <summary>シーケンシャルナンバーのMAX値</summary>
		public Int64 Max_Seq_No;

		/// <summary>枝番のMAX値</summary>
		public Int64 Max_Edaban;

		/// <summary>ノード枝番</summary>
		public Int64 NODE_EDABAN_HAISHA = 1;            // 配車ヘッダレコード
		public Int64 NODE_EDABAN_SHUKKO = 2;            // 出庫実績レコード
		public Int64 NODE_EDABAN_KIKO = 3;              // 帰庫実績レコード
		public Int64 NODE_EDABAN_GENBAJISSEKI = 4;      // 現場実績レコード
		public Int64 NODE_EDABAN_DETAIL = 5;            // 現場明細レコード
		public Int64 NODE_EDABAN_HANNYUUJISSEKI = 6;    // 搬入実績レコード
		public Int64 NODE_EDABAN_CONTENA = 7;           // コンテナ明細レコード

		/// <summary>配車区分</summary>
		public static readonly Int16 HAISHA_KBN_TEIKI = 0;	// 定期
		public static readonly Int16 HAISHA_KBN_SPOT = 1;		// スポット
		public static readonly Int16 HAISHA_KBN_CONTENA = 2;	// コンテナ

		/// <summary>コンテナ切替フラグ
		/// true: コンテナ有、false: コンテナ無
		/// </summary>
		private bool contenaFlg = true;

		/// <summary>システムID＆明細システムID採番用のアクセサー</summary>
		private Shougun.Core.Common.BusinessCommon.DBAccessor CommonDBAccessor;

		/// <summary>年連番・日連番取得用のアクセサー</summary>
		private Shougun.Core.Allocation.MobileShougunTorikomi.Accessor.DBAccessor accessor;

		/// <summary>DAO</summary>
		private MobileShougunTorikomiDAOClass dao;

		/// <summary>SetMobileSyogunDataInsertDao</summary>
		private SetMobileSyogunDataInsertDao setMobileSyogunDataInsertDao;

		/// <summary>SetTeikiJissekiEntryDao</summary>
		private SetTeikiJissekiEntryDao setTeikiJissekiEntryDao;

		/// <summary>SetTeikiJissekiDetailDao</summary>
		private SetTeikiJissekiDetailDao setTeikiJissekiDetailDao;

		/// <summary>SetTeikiJissekiNioroshiDao</summary>
		private SetTeikiJissekiNioroshiDao setTeikiJissekiNioroshiDao;

		/// <summary>SetUrShEntryDao</summary>
		private SetUrShEntryDao setUrShEntryDao;

		/// <summary>SetUrShDetailDao</summary>
		private SetUrShDetailDao setUrShDetailDao;

		/// <summary>モバイル将軍用データ取込画面専用テーブル取得後の保存領域</summary>
		private DateTime dateContenaIdouDate;       // (コンテナ明細)移動日時
		private string strContenaShuruiCd;          // (コンテナ明細)コンテナ種類CD
		private string strContenaCd;                // (コンテナ明細)コンテナCD
		private string strGenbaJissekiGenbaCd;      // (現場実績)現場CD
		private string strGenbaJissekiGyoushaCd;    // (現場実績)業者CD 

		/// <summary>現場マスタ取得後の保存領域</summary>
		private string strGenbaMgenbaName;          // 現場名

		/// <summary>コンテナマスタ取得後の保存領域</summary>
		private string strContenaName;              // コンテナ名

		/// <summary>確定区分</summary>
		private Int16 KAKUTEI_KBN_MIKAKUTEI = 2;    // 確定区分

		/// <summary>xmlファイル名</summary>
		private string fn;

		/// <summary>月極区分</summary>
		private Int16 int16teikiTsukiKbn;

		/// <summary>伝票日付</summary>
		private DateTime dtDenpyouDate;

		/// <summary>XMLファイルパス（取込ファイル）</summary>
		private string mobileInPutFilePath;

		/// <summary>XMLファイルパス（集金実績ファイル）削除用</summary>
		private string[] mobileInPutFilePathToMove;

		/// <summary>XMLファイルパス（バックアップファイル）</summary>
		private string mobileBackUpPath;

		/// <summary>システムID</summary>
		private SqlInt64 int64SystemId;

		/// <summary>定期実績番号</summary>
		private SqlInt64 int64TeikiJissekiNumber;

		/// <summary>売上／支払番号</summary>
		private SqlInt64 int64UrShNumber;

		/// <summary>ボタン設定格納ファイル</summary>
		private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileShougunTorikomi.Setting.ButtonSetting.xml";

		/// <summary>MobileShougunTorikomiDTOClass</summary>
		private MobileShougunTorikomiDTOClass dto;

		/// <summary>システム情報格納専用DTOClass</summary>
		private DTO.DTOClass systemDto;

		/// <summary>Form</summary>
		private APP.UIForm form;

		/// <summary>定期配車番号</summary>
		private int HaishaDenNo;

		/// <summary>
		/// 車輌マスタのDao
		/// </summary>
		private IM_SHARYOUDao sharyouDao;
		/// <summary>
		/// 車種マスタのDao
		/// </summary>
		private IM_SHASHUDao shashuDao;
		/// <summary>
		/// 社員マスタのDao
		/// </summary>
		private IM_SHAINDao shainDao;
		/// <summary>
		/// 基本品名単価マスタのDao
		/// </summary>
		private IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao;
		/// <summary>
		/// 個別品名単価マスタのDao
		/// </summary>
		private IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;
		/// <summary>
		/// 取引先請求情報のDao
		/// </summary>
		private IM_TORIHIKISAKI_SEIKYUUDao torihikiSeikyuDao;
		/// <summary>
		/// 取引先支払情報のDao
		/// </summary>
		private IM_TORIHIKISAKI_SHIHARAIDao torihikiShiharaiDao;
		/// <summary>
		/// 業者のDao
		/// </summary>
		private IM_GYOUSHADao gyoushaDao;
		/// <summary>
		/// 現場のDao
		/// </summary>
		private IM_GENBADao genbaDao;
		/// <summary>
		/// 取引先のDao
		/// </summary>
		private IM_TORIHIKISAKIDao torihikisakiDao;

		/// <summary>コンテナフォームを保持するフィールド</summary>
		private Dictionary<int, ContenaFormDialogManager> contenaFormList = new Dictionary<int, ContenaFormDialogManager>();

        // 20141119 koukouei 締済期間チェックの追加 start
        private bool shimeiCheckFlg;
        private bool registFlg;
        // 20141119 koukouei 締済期間チェックの追加 end

        /// <summary>
        /// 登録チェックボックスのCellMouseClickでアラート表示しているか
        /// (ichiranCurrentCellDirtyStateChanged
        ///  でアラートを表示しない為の判断用FLG)
        ///  ※チェックボックスをマウスでクリックした時、
        ///  　CellMouseClick→CellDirtyStateChangedと通る為
        /// </summary>
        private bool IsAlertFlg = false;
        private MessageBoxShowLogic MsgBox;

        private bool isSpacePress = true;
		#endregion - Fields -

		#region - Constructors -

		/// <summary>コンストラクタ</summary>
		public MobileShougunTorikomiLogic(APP.UIForm targetForm)
		{
			LogUtility.DebugMethodStart(targetForm);

			this.form = targetForm;
			this.dto = new MobileShougunTorikomiDTOClass();
			this.systemDto = new DTO.DTOClass();
			this.dao = DaoInitUtility.GetComponent<MobileShougunTorikomiDAOClass>();
			this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();             // 車輌マスタのDao           
			this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();               // 車種マスタのDao
			this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();                 // 社員マスタのDao
			this.kihonHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KIHON_HINMEI_TANKADao>();                 // 基本品名単価マスタのDao
			this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();                 // 個別品名単価マスタのDao
			this.torihikiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
			this.torihikiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
			this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
			this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
			this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
			this.CommonDBAccessor = new Shougun.Core.Common.BusinessCommon.DBAccessor();
			this.accessor = new Shougun.Core.Allocation.MobileShougunTorikomi.Accessor.DBAccessor();
            this.setUrShEntryDao = DaoInitUtility.GetComponent<SetUrShEntryDao>();
            this.MsgBox = new MessageBoxShowLogic();
			this.InitializePath();

			LogUtility.DebugMethodEnd();
		}

		#endregion - Constructors -

		#region - Properties -

		/// <summary>検索結果取得用</summary>
		public DataTable dataResult { get; set; }

		/// <summary>コース名称データ</summary>
		public M_COURSE_NAME[] mCourseNameAll { get; set; }

		/// <summary>定期配車入力データ</summary>
		private DataTable teikiHaishaEntryResult { get; set; }

		/// <summary>コース名称マスタデータ</summary>
		private DataTable courseNameResult { get; set; }

		/// <summary>コンテナ種類マスタデータ</summary>
		private DataTable contenaShuruiResult { get; set; }

		/// <summary>コンテナマスタデータ</summary>
		private DataTable contenaResult { get; set; }

		/// <summary>モバイル将軍用データ取込画面専用テーブルデータ</summary>
		private DataTable mobileSyogunDataInsertResult { get; set; }

		/// <summary>現場マスタデータ</summary>
		private DataTable genbaResult { get; set; }

		/// <summary>受付(収集)明細データ</summary>
		private DataTable uketsukeSsDetailResult { get; set; }

		/// <summary>コース_明細内訳データ</summary>
		private DataTable courseDetailItemsResult { get; set; }

		/// <summary>取引先_請求情報マスタデータ</summary>
		private DataTable mtorihikisakiSeikyuuResult { get; set; }

		#endregion - Properties -

		#region - Methods -

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

		public void PopUpDataInit()
		{
			try
			{
				LogUtility.DebugMethodStart();
				// ｺｰｽ情報 ポップアップ取得
				// 表示用データ取得＆加工
				var ShainDataTable = this.GetPopUpData(this.form.ntxt_CourseCd.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
				// TableNameを設定すれば、ポップアップのタイトルになる
				ShainDataTable.TableName = "ｺｰｽ名称情報";

				// 列名とデータソース設定
				this.form.ntxt_CourseCd.PopupDataHeaderTitle = new string[] { "ｺｰｽ名称CD", "ｺｰｽ名称" };
				this.form.ntxt_CourseCd.PopupDataSource = ShainDataTable;
			}
			catch(Exception ex)
			{
				LogUtility.Error("PopUpDataInit", ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd();
			}
		}

		/// <summary>マスタポップアップ用データテーブル取得</summary>
		/// <param name="displayCol">表示対象列(物理名)</param>
		/// <returns>データーテーブル</returns>
		public DataTable GetPopUpData(IEnumerable<string> displayCol)
		{
			DataTable ret = new DataTable();
			try
			{
				LogUtility.DebugMethodStart(displayCol);
				M_COURSE_NAME[] CourseNameAll;
				CourseNameAll = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>().GetAllValidData(new M_COURSE_NAME());
				this.mCourseNameAll = CourseNameAll;
				if(displayCol.Any(s => s.Length == 0))
				{
					return new DataTable();
				}
				var dt = EntityUtility.EntityToDataTable(CourseNameAll);
				if(dt.Rows.Count == 0)
				{
					ret = dt;
					return dt;
				}
				var sortedDt = new DataTable();
				foreach(var col in displayCol)
				{
					// 表示対象の列だけを順番に追加
					sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
				}

				foreach(DataRow r in dt.Rows)
				{
					sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
				}
				ret = sortedDt;
				return sortedDt;
			}
			catch(Exception ex)
			{
				LogUtility.Error("GetPopUpData", ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd(ret);
			}
		}

		/// <summary>画面初期表示設定</summary>
		private void initializeScreen()
		{
			//「作業開始日」
			this.form.dtp_SagyouDateFrom.Value = null;
			this.form.dtp_SagyouDateFrom.Text = string.Empty;

			//「作業終了日」
			this.form.dtp_SagyouDateTo.Value = null;
			this.form.dtp_SagyouDateTo.Text = string.Empty;

			// 「拠点CD」
			this.form.ntxt_KyotenCd.Text = string.Empty;

			// 「拠点名」
			this.form.txt_KyotenName.Text = string.Empty;
			//this.form.txt_KyotenName.Enabled = false;

			// 「車種CD」
			this.form.ntxt_ShashuCd.Text = string.Empty;

			// 「車種名」
			this.form.txt_ShashuName.Text = string.Empty;
			//this.form.txt_ShashuName.Enabled = false;

			// 「車輌CD」
			this.form.ntxt_SharyouCd.Text = string.Empty;

			// 「車輌名」
			this.form.txt_SharyouName.Text = string.Empty;
			//this.form.txt_SharyouName.Enabled = false;

			// 「運転者CD」
			this.form.ntxt_UntenshaCd.Text = string.Empty;

			// 「運転者名」
			this.form.txt_UntenshaName.Text = string.Empty;
			//this.form.txt_UntenshaName.Enabled = false;

			// 「コースCD」
			this.form.ntxt_CourseCd.Text = string.Empty;

			// 「コース名」
			this.form.txt_CourseName.Text = string.Empty;
			//this.form.txt_CourseName.Enabled = false;

			// 表示・非表示判定
			setVisible("TEIKI");
		}

		/// <summary>表示・非表示判定</summary>
		public void setVisible(string haishaKbun)
		{
			switch(haishaKbun)
			{
				case "TEIKI":
					{ // 定期の場合
						this.form.lbl_Course.Visible = true;        // 「コースラベル」
						this.form.ntxt_CourseCd.Visible = true;     // 「コースCD」
						this.form.txt_CourseName.Visible = true;    // 「コース名」
					}
					break;

				case "SPOT":
				case "CONTENA":
					{ // スポット、コンテナの場合
						this.form.lbl_Course.Visible = false;       // 「コースラベル」
						this.form.ntxt_CourseCd.Visible = false;    // 「コースCD」
						this.form.txt_CourseName.Visible = false;   // 「コース名」
					}
					break;
			}

			// コンテナ未登録件数表示
			if(!contenaFlg)
			{   // コンテナ無

				// コンテナ未登録件数は表示しない
				((Label)this.form.Controls["lbl_ContenaMitouroku"]).Visible = false;
				((TextBox)this.form.Controls["ntxt_ContenaMitouroku"]).Visible = false;
				((Label)this.form.Controls["lblContenaKensu"]).Visible = false;
			}

		}

		/// <summary>ボタンの初期化処理</summary>
		public void ButtonInit()
		{
			var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
		}

		/// <summary>コース_明細内訳マスタ取得処理</summary>
		public int getCourseDetailItems(string courseNameCd)
		{
			this.int16teikiTsukiKbn = 0;

			// 月極区分の取得
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			entity.COURSE_NAME_CD = courseNameCd;
			this.courseDetailItemsResult = this.dao.GetCourseDetailItemsForEntity(entity);

			DataRow[] selectedRows = courseDetailItemsResult.Select();
			foreach(DataRow row in selectedRows)
			{
				// 月極区分
				if(row["TEIKI_TSUKI_KBN"] != DBNull.Value)
				{
					this.int16teikiTsukiKbn = (Int16)row["TEIKI_TSUKI_KBN"];
				}
			}

			return selectedRows.Length;
		}

		/// <summary>コンテナマスタ更新(実行)</summary>
		public void setContenaSql(M_CONTENA SetEntity)
		{
			ContenaDTOClass entity = new ContenaDTOClass();
			entity.CONTENA_SHURUI_CD = SetEntity.CONTENA_SHURUI_CD;
			entity.CONTENA_CD = SetEntity.CONTENA_CD;
			entity.GENBA_CD = SetEntity.GENBA_CD;
			entity.KYOTEN_CD = SetEntity.KYOTEN_CD;
			entity.SECCHI_DATE = SetEntity.SECCHI_DATE;
			entity.JOUKYOU_KBN = SetEntity.JOUKYOU_KBN;
			entity.UPDATE_USER = SetEntity.UPDATE_USER;
			entity.UPDATE_DATE = SetEntity.UPDATE_DATE;
			entity.UPDATE_PC = SetEntity.UPDATE_PC;
			this.dao.SetContenaForEntity(entity);
		}

        private void Ichiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 編集列名のセット
            var colName = this.form.Ichiran.Columns[e.ColumnIndex].Name;

            if (colName.Equals("REGIST_CHECK"))
            {
                this.isSpacePress = true;
            }
        }

        private void Ichiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != -1 && this.isSpacePress)
            {
                // 編集行のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];

                // 編集列名のセット
                var colName = this.form.Ichiran.Columns[e.ColumnIndex].Name;

                switch (colName)
                {
                    case "REGIST_CHECK":
                        if (false == (bool)row.Cells["REGIST_CHECK"].Value)
                        {
                            int cnt = 0;
                            if (row.Cells["HAISHA_DENPYOU_NO"].Value != null)
                            {
                                var uketsukeDenpyou = row.Cells["HAISHA_DENPYOU_NO"].Value.ToString();

                                DataTable dt = setUrShEntryDao.GetUriageShiharaiEntryData(Int64.Parse(uketsukeDenpyou));

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    cnt = dt.Rows.Count;
                                }
                                else
                                {

                                    foreach (DataGridViewRow dr in this.form.Ichiran.Rows)
                                    {
                                        if (dr.Index == row.Index
                                            || dr.Cells["REGIST_CHECK"].Value == null
                                            || (bool)dr.Cells["REGIST_CHECK"].Value == false)
                                        {
                                            continue;
                                        }

                                        if (dr.Cells["HAISHA_DENPYOU_NO"].Value != null
                                            && dr.Cells["HAISHA_DENPYOU_NO"].Value.ToString().Equals(uketsukeDenpyou))
                                        {
                                            cnt++;
                                        }
                                    }
                                }
                            }

                            if (cnt > 0)
                            {
                                var msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShowError("選択された受付番号は売上/支払で既に使用されているため、データを取り込む事ができません。");
                                row.Cells["REGIST_CHECK"].Value = false;
                                e.Cancel = true;
                            }

                            // 登録チェックOFF⇒ONの場合は、削除チェック状態を解除する
                            row.Cells["DELETE_CHECK"].Value = false;
                        }
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
        }

        /// <summary>
        /// CellClickイベント
        /// (CheckBoxクリック、セルクリック時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ichiranCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // 編集行のセット
                var row = this.form.Ichiran.Rows[e.RowIndex];

                // 編集列名のセット
                var colName = this.form.Ichiran.Columns[e.ColumnIndex].Name;

                this.IsAlertFlg = false;

                switch (colName)
                {
                    case "DELETE_CHECK":
                        if (false == (bool)row.Cells["DELETE_CHECK"].Value)
                        {
                            // 削除チェックOFF⇒ONの場合は、登録チェック状態を解除する
                            row.Cells["REGIST_CHECK"].Value = false;
                        }
                        break;
                    case "REGIST_CHECK":
                        // 同じ配車番号のレコードを二重登録しないか判定
                        // 選択された行に紐付く定期配車番号が定期実績にて使用されているかどうか判定
                        if (!string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text))
                        {
                            if (containsSameHaishaDenpyouNo(row, false) ||
                                this.accessor.isTeikiHaishaNumUsed(row))
                            {
                                // 既に定期配車番号が使用されていた場合、変更を取り消す
                                var msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E086", "選択された定期配車番号", "定期実績", "\nデータを取り込む事が");
                                this.form.Ichiran[e.ColumnIndex, e.RowIndex].Tag = "G283";
                                this.IsAlertFlg = true;
                                this.form.Ichiran[e.ColumnIndex, e.RowIndex].Value = false;
                                this.form.Ichiran.RefreshEdit();
                                this.form.Ichiran.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                return;
                            }
                        }

                        this.isSpacePress = true;

                        if (false == (bool)row.Cells["REGIST_CHECK"].Value)
                        {
                            // 登録チェックOFF⇒ONの場合は、削除チェック状態を解除する
                            row.Cells["DELETE_CHECK"].Value = false;
                        }
                        break;
                    case "SYOUSAI_BTN":
                        // 詳細ボタン選択時
                        ContenaForm frm = new ContenaForm(this.dataResult, row, e.RowIndex);

                        if (this.IsContenaFormAdd(e.RowIndex, frm))
                        {   // 既に存在している
                            ContenaFormDialogManager frmTmp = this.contenaFormList[e.RowIndex];
                            frmTmp.ContenaForm.Activate();
                        }
                        else
                        {   // まだ存在していない
                            frm.Show();
                        }
                        break;
                    default:
                        // DO NOTHING
                        break;
                }
            }
        }

        /// <summary>
        /// CurrentCellDirtyStateChanged処理
        /// セルの内容が変更された時に発生
        /// (CheckBoxクリック、スペースキー押下時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ichiranCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // カレントセルの取得
            var cell = this.form.Ichiran.CurrentCell;

            // 編集行のセット
            var row = this.form.Ichiran.Rows[cell.RowIndex];

            // 編集列名のセット
            var colName = this.form.Ichiran.Columns[cell.ColumnIndex].Name;

            switch (colName)
            {
                case "REGIST_CHECK":
                    // 定期取込時
                    if (false == string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text))
                    {
                        // 値変更時
                        if (this.form.Ichiran.IsCurrentCellDirty)
                        {
                            // CellMouseClickでアラート表示していなければ以下の判断へ
                            if (!this.IsAlertFlg)
                            {
                                // 同じ配車番号のレコードを二重登録しないか判定
                                // 選択された行に紐付く定期配車番号が定期実績にて使用されているかどうか判定
                                if ((true == containsSameHaishaDenpyouNo(row, true) ||
                                    true == this.accessor.isTeikiHaishaNumUsed(row)))
                                {
                                    // 既に定期配車番号が使用されていた場合、変更を取り消す
                                    var msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E086", "選択された定期配車番号", "定期実績", "\nデータを取り込む事が");
                                    this.IsAlertFlg = true;
                                    cell.Value = false;
                                    this.form.Ichiran.CancelEdit();
                                }
                            }
                        }
                    }
                    if ((bool)row.Cells["REGIST_CHECK"].Value && 
                        !this.form.Ichiran.IsCurrentCellDirty &&
                        !this.IsAlertFlg)
                    {
                        // 登録チェックOFF⇒ONの場合は、削除チェック状態を解除する
                        row.Cells["DELETE_CHECK"].Value = false;
                    }
                    break;
                case "DELETE_CHECK":
                    if ((bool)row.Cells["DELETE_CHECK"].Value)
                    {
                        // 削除チェックOFF⇒ONの場合は、登録チェック状態を解除する
                        row.Cells["REGIST_CHECK"].Value = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Ichiranでのキー押下
        /// ※登録チェックボックスでスペースキー押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_KeyDown(object sender, KeyEventArgs e)
        {
            var cell = this.form.Ichiran.CurrentCell;
            var row = this.form.Ichiran.CurrentRow;

            if (cell != null)
            {
                if (e.KeyCode == Keys.Space)
                {
                    this.isSpacePress = true;

                    if (this.form.Ichiran.Columns[cell.ColumnIndex].Name == "REGIST_CHECK")
                    {
                        this.IsAlertFlg = false;

                        if (row != null)
                        {
                            int cnt = 0;
                            if (row.Cells["HAISHA_DENPYOU_NO"].Value != null)
                            {
                                var uketsukeDenpyou = row.Cells["HAISHA_DENPYOU_NO"].Value.ToString();

                                DataTable dt = setUrShEntryDao.GetUriageShiharaiEntryData(Int64.Parse(uketsukeDenpyou));

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    cnt = dt.Rows.Count;
                                }
                                else
                                {
                                    foreach (DataGridViewRow dr in this.form.Ichiran.Rows)
                                    {
                                        if (dr.Index == row.Index
                                            || dr.Cells["REGIST_CHECK"].Value == null
                                            || (bool)dr.Cells["REGIST_CHECK"].Value == false)
                                        {
                                            continue;
                                        }

                                        if (dr.Cells["HAISHA_DENPYOU_NO"].Value != null
                                            && dr.Cells["HAISHA_DENPYOU_NO"].Value.ToString().Equals(uketsukeDenpyou))
                                        {
                                            cnt++;
                                        }
                                    }
                                }
                            }

                            if (cnt > 0)
                            {
                                //var msgLogic = new MessageBoxShowLogic();
                                //msgLogic.MessageBoxShowError("選択された受付番号は売上/支払で既に使用されているため、データを取り込む事ができません。");
                                cell.Value = false;
                                this.form.Ichiran.CurrentCell.Value = false;
                                this.form.Ichiran.CancelEdit();
                                this.form.Ichiran.NotifyCurrentCellDirty(true);
                                this.form.Ichiran.EndEdit();
                                this.form.Ichiran.CurrentRow.Cells["SAGYOU_DATE"].Selected = true;
                                this.form.Ichiran.CurrentRow.Cells["REGIST_CHECK"].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    this.isSpacePress = false;
                }
            }
        }

        /// <summary>
        /// 編集行と同じ定期配車番号が他のレコードで選択済みか判定
        /// </summary>
        /// <param name="row">編集行</param>
        /// <returns>TRUE:選択済み FALSE:未選択</returns>
        private bool containsSameHaishaDenpyouNo(DataGridViewRow row, bool IsAlradyCheck)
        {
            // 編集行より定期配車番号を取得
            var haishaDenpyouNo = row.Cells["HAISHA_DENPYOU_NO"].Value;
            if (haishaDenpyouNo == null)
            {
                return false;
            }

            // 編集行と同じ定期配車番号で登録欄のチェック済みのレコード数を取得
            int count = this.form.Ichiran.Rows.Cast<DataGridViewRow>()
                                              .Count(r => r.Cells["HAISHA_DENPYOU_NO"].Value != null
                                                       && r.Cells["HAISHA_DENPYOU_NO"].Value.Equals(haishaDenpyouNo)
                                                       && r.Cells["REGIST_CHECK"].EditedFormattedValue.Equals(true));

            //if (1 < count)
            if (count > 0 && (bool)row.Cells["REGIST_CHECK"].Value == IsAlradyCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>コース名称マスタ取得処理</summary>
		public string getCourseName(string courseCd)
		{
			string strCourseName = null;

			// コース名称マスタ取得取得
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			entity.COURSE_NAME_CD = courseCd;
			this.courseNameResult = this.dao.GetCourseNameDataForEntity(entity);

			DataRow[] selectedRows = courseNameResult.Select();
			foreach(DataRow row in selectedRows)
			{
				if(row["COURSE_NAME_RYAKU"] == DBNull.Value)
				{
					strCourseName = null;
				}
				else
				{
					strCourseName = (string)row["COURSE_NAME_RYAKU"];
				}
			}

			return strCourseName;
		}

		/// <summary>コース名称CDに紐付く拠点CDを返却</summary>
		public Int16 getKyotenCD(string courseCd)
		{
			Int16 iKyotenCD = -1;

			// コース名称マスタ取得取得
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			entity.COURSE_NAME_CD = courseCd;
			this.courseNameResult = this.dao.GetCourseNameDataForEntity(entity);

			DataRow[] selectedRows = courseNameResult.Select();
			foreach(DataRow row in selectedRows)
			{
				if(row["KYOTEN_CD"] == DBNull.Value)
				{
					iKyotenCD = -1;
				}
				else
				{
					iKyotenCD = (Int16)row["KYOTEN_CD"];
				}
			}

			return iKyotenCD;
		}

		/// <summary>コンテナ種類マスタ取得処理</summary>
		public string getContenaShurui(string contenaShuruiCd)
		{
			string strContenaShuruiName = null;

			ContenaShuruiDTOClass entity = new ContenaShuruiDTOClass();
			entity.CONTENA_SHURUI_CD = contenaShuruiCd;
			this.contenaShuruiResult = this.dao.GetContenaShuruiDataForEntity(entity);

			DataRow[] selectedRows = contenaShuruiResult.Select();
			foreach(DataRow row in selectedRows)
			{
				if(row["CONTENA_SHURUI_NAME"] == DBNull.Value)
				{
					strContenaShuruiName = null;
				}
				else
				{
					strContenaShuruiName = (string)row["CONTENA_SHURUI_NAME"];
				}
			}

			return strContenaShuruiName;
		}

		/// <summary>コンテナマスタ取得処理</summary>
		public int getContena(string contenaShuruiCd, string contenaCd)
		{
			this.strContenaName = null;

			ContenaDTOClass entity = new ContenaDTOClass();
			entity.CONTENA_SHURUI_CD = contenaShuruiCd;
			entity.CONTENA_CD = contenaCd;
			this.contenaResult = this.dao.GetContenaDataForEntity(entity);

			DataRow[] selectedRows = contenaResult.Select();
			foreach(DataRow row in selectedRows)
			{
				if(row["CONTENA_NAME"] == DBNull.Value)
				{
					this.strContenaName = null;
				}
				else
				{
					this.strContenaName = (string)row["CONTENA_NAME"];
				}
			}

			return selectedRows.Length;
		}

		/// <summary>モバイル将軍用データ取込画面専用テーブル取得処理（コンテナ用項目の取得）</summary>
		public int getMobileSyogunDataInsertContenaData(Int64 edaban, Int64 nodeEdaban)
		{
			this.strContenaShuruiCd = null;
			this.strContenaCd = null;
			this.strGenbaJissekiGenbaCd = null;
			this.strGenbaJissekiGyoushaCd = null;

			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			entity.EDABAN = edaban;
			entity.NODE_EDABAN = nodeEdaban;
			this.mobileSyogunDataInsertResult = this.dao.GetMobileSyogunDataInsertContenaDataForEntity(entity);

			DataRow[] selectedRows = mobileSyogunDataInsertResult.Select();
			foreach(DataRow row in selectedRows)
			{
				if(row["CONTENA_IDOUDATE"] != DBNull.Value)
				{
					this.dateContenaIdouDate = (DateTime)row["CONTENA_IDOUDATE"];
				}

				if(row["CONTENA_SHURUI_CD"] != DBNull.Value)
				{
					this.strContenaShuruiCd = (string)row["CONTENA_SHURUI_CD"];
				}

				if(row["CONTENA_CD"] != DBNull.Value)
				{
					this.strContenaCd = (string)row["CONTENA_CD"];
				}

				if(row["GENBA_JISSEKI_GENBACD"] != DBNull.Value)
				{
					this.strGenbaJissekiGenbaCd = (string)row["GENBA_JISSEKI_GENBACD"];
				}

				if(row["GENBA_JISSEKI_GYOUSHACD"] != DBNull.Value)
				{
					this.strGenbaJissekiGyoushaCd = (string)row["GENBA_JISSEKI_GYOUSHACD"];
				}
			}

			return selectedRows.Length;
		}

		/// <summary>現場マスタ取得処理</summary>
		public int getGenba(string genbaJissekiGyoushaCd, string genbaJissekiGenbaCd)
		{
			this.strGenbaMgenbaName = null;

			GenbaDTOClass entity = new GenbaDTOClass();
			entity.GYOUSHA_CD = genbaJissekiGyoushaCd;
			entity.GENBA_CD = genbaJissekiGenbaCd;
			this.genbaResult = this.dao.GetGenbaDataForEntity(entity);

			DataRow[] selectedRows = genbaResult.Select();
			foreach(DataRow row in selectedRows)
			{
				//if (row["GENBA_NAME1"] != DBNull.Value)
				//{
				//    this.strGenbaMgenbaName = (string)row["GENBA_NAME1"];
				//}
				//else
				//{
				//    if (row["GENBA_NAME2"] != DBNull.Value)
				//    {
				//        this.strGenbaMgenbaName = (string)row["GENBA_NAME2"];
				//    }
				//}

				this.strGenbaMgenbaName = (string)row["GENBA_NAME_RYAKU"];
			}

			return selectedRows.Length;
		}

		/// <summary>検索条件取得処理</summary>
		public string getSelectWhere()
		{
			string selectWhere = null;

			if(string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()) &&
				string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()) &&
				string.IsNullOrEmpty(this.form.ntxt_KyotenCd.Text) &&
				string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text) &&
				string.IsNullOrEmpty(this.form.ntxt_SharyouCd.Text) &&
				string.IsNullOrEmpty(this.form.ntxt_UntenshaCd.Text) &&
				string.IsNullOrEmpty(this.form.ntxt_CourseCd.Text))
			{ // 指定条件がない場合
				if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
				{ // 定期データ表示時
					selectWhere = "HAISHA_KBN = 0";
				}
				else
				{
					if(!(string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text)))
					{ // スポットデータ表示時
						selectWhere = "HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = false";
					}
					else
					{ // コンテナデータ表示時
						selectWhere = "HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = true";
					}
				}

				return selectWhere;
			}
			else
			{ // 指定条件がある場合

				DateTime dtSagyouDateFrom;
				DateTime dtSagyouDateTo;

				// 作業日Fromあり、作業日Toあり
				if((!(string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()))) &&
					(!(string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()))))
				{
					dtSagyouDateFrom = DateTime.Parse(this.form.dtp_SagyouDateFrom.Text);
					dtSagyouDateTo = DateTime.Parse(this.form.dtp_SagyouDateTo.Text);
					selectWhere = selectWhere + "(" +
												string.Format("[{0}] >= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateFrom) +
												") AND (" +
												string.Format("[{0}] <= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateTo) +
												")";
				}

				// 作業日Fromあり、作業日Toなし
				if((!(string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()))) &&
					  (string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim())))
				{
					dtSagyouDateFrom = DateTime.Parse(this.form.dtp_SagyouDateFrom.Text);
					selectWhere = selectWhere + "(" +
												string.Format("[{0}] >= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateFrom) +
												")";
				}

				// 作業日Fromなし、作業日Toあり
				if((string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim())) &&
					(!(string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()))))
				{
					dtSagyouDateTo = DateTime.Parse(this.form.dtp_SagyouDateTo.Text);
					selectWhere = selectWhere + "(" +
												string.Format("[{0}] <= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateTo) +
												")";
				}

				// 拠点あり
				if(!(string.IsNullOrEmpty(this.form.ntxt_KyotenCd.Text)))
				{
					selectWhere = selectWhere + " AND KYOTEN_CD = " + this.form.ntxt_KyotenCd.Text;
				}

				// 車種あり
				if(!(string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text)))
				{
					selectWhere = selectWhere + " AND SHASHU_CD = " + this.form.ntxt_ShashuCd.Text;
				}

				// 車輌あり
				if(!(string.IsNullOrEmpty(this.form.ntxt_SharyouCd.Text)))
				{
					selectWhere = selectWhere + " AND SHUKKO_SHARYOUCD = " + this.form.ntxt_SharyouCd.Text;
				}

				// 運転者あり
				if(!(string.IsNullOrEmpty(this.form.ntxt_UntenshaCd.Text)))
				{
					selectWhere = selectWhere + " AND HAISHA_UNTENSHA_CD = " + this.form.ntxt_UntenshaCd.Text;
				}

				// コースあり
				if(!(string.IsNullOrEmpty(this.form.ntxt_CourseCd.Text)))
				{
					selectWhere = selectWhere + " AND HAISHA_COURSE_NAME_CD = " + this.form.ntxt_CourseCd.Text;
				}

				// 定期orスポットorコンテナの設定
				if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
				{ // 定期データ表示時
					selectWhere = selectWhere + " AND HAISHA_KBN = 0";
				}
				else
				{
					if(!(string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text)))
					{ // スポットデータ表示時
						selectWhere = selectWhere + " AND HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = false";
					}
					else
					{ // コンテナデータ表示時
						selectWhere = selectWhere + " AND HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = true";
					}
				}

				// SQL文の整形
				if(selectWhere.Substring(0, 5) == " AND ")
				{
					StringBuilder sb = new System.Text.StringBuilder(selectWhere);
					sb.Replace(" AND ", string.Empty, 0, 5);
					selectWhere = sb.ToString();
				}
			}

			return selectWhere;
		}

		/// <summary>コース名称マスタ取得処理</summary>
		public DataTable getCourseNames(string courseCd)
		{
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			if(courseCd != null && courseCd != string.Empty)
			{
				entity.COURSE_NAME_CD = courseCd;
			}
			return this.dao.GetCourseNameDataForEntity(entity);
		}

		/// <summary>コンテナフォームが既に存在しているか否か取得し存在していない場合には追加する</summary>
		/// <param name="index">行インデックスを表す数値</param>
		/// <param name="form">コンテナフォーム</param>
		/// <returns>存在している場合は真：存在していない場合は偽</returns>
		public bool IsContenaFormAdd(int index, ContenaForm form)
		{
			if(this.contenaFormList.ContainsKey(index))
			{   // 既に存在
				return true;
			}

			this.contenaFormList.Add(index, new ContenaFormDialogManager(index, form));

			form.CloseEvent += (indexTmp) =>
			{
				this.ContenaFormRemove(indexTmp);
			};

			return false;
		}

		/// <summary>コンテナフォームの削除処理を実行する</summary>
		/// <param name="index">行インデックスを表す数値</param>
		private void ContenaFormRemove(int index)
		{
			if(this.contenaFormList.Count == 0)
			{
				return;
			}

			if(!this.contenaFormList.ContainsKey(index))
			{   // フォームが存在していない
				return;
			}

			if(!this.contenaFormList[index].ContenaForm.IsDisposed)
			{   // Active
				this.contenaFormList[index].ContenaForm.Close();
				this.contenaFormList[index].ContenaForm.Dispose();
			}

			this.contenaFormList.Remove(index);
		}

		/// <summary>コンテナフォームの全てを削除処理する</summary>
		private void ContenaFormRemoveAll()
		{
			if(this.contenaFormList.Count == 0)
			{
				return;
			}

			foreach(int index in this.contenaFormList.Keys)
			{
				if(!this.contenaFormList[index].ContenaForm.IsDisposed)
				{   // Active
					this.contenaFormList[index].ContenaForm.Close();
					this.contenaFormList[index].ContenaForm.Dispose();
				}
			}
		}

        /// <summary>
        /// モバイル将軍取込登録処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        private Boolean RegistMobileShougunData()
        {
            // 登録実行フラグの初期化
            Boolean chkFlg = false;
            // 20141119 koukouei 締済期間チェックの追加 start
            this.shimeiCheckFlg = true;
            this.registFlg = true;
            // 20141119 koukouei 締済期間チェックの追加 end
            using (Transaction tran = new Transaction())
            {
                for (int i = 0; i < selectedRowsIchiran.Length; i++)
                {
                    if ((bool)this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value)
                    {
                        // モバイル将軍用データ取込画面専用テーブル削除処理
                        deleteMobileShougunData(Int64.Parse(this.form.Ichiran.Rows[i].Cells["EDABAN"].Value.ToString()));

                        // コンテナフォームの削除処理
                        this.ContenaFormRemove(i);

                        // 登録実行フラグにチェックをする
                        chkFlg = true;
                    }

                    if ((bool)this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value)
                    {
                        // 各テーブル登録
                        insertJissekiData(Int64.Parse(this.form.Ichiran.Rows[i].Cells["EDABAN"].Value.ToString()));

                        // 20141119 koukouei 締済期間チェックの追加 start
                        if (!this.registFlg)
                        {
                            break;
                        }
                        // 20141119 koukouei 締済期間チェックの追加 end
                        // モバイル将軍用データ取込画面専用テーブル削除処理（登録後は物理削除する）
                        deleteMobileShougunData(Int64.Parse(this.form.Ichiran.Rows[i].Cells["EDABAN"].Value.ToString()));

                        // コンテナフォームの削除処理
                        this.ContenaFormRemove(i);

                        // 登録実行フラグにチェックをする
                        chkFlg = true;
                    }
                }
                
                // コミット
                tran.Commit();
            }

            return chkFlg;
        }

		/// <summary>一覧の表示</summary>
		internal void SetIchiran(string haishaKbun)
		{
			DataRow[] dataRowList = null;
			long edaban = 0;

            /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　start
            if (this.DateCheck())
            {
                return;
            }
            /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　end

			// 一覧の初期化
			this.form.Ichiran.Rows.Clear();

			// 取得したモバイル将軍用データ取込画面専用テーブルをセット
			var table = this.dataResult;
			table.BeginLoadData();

			// 一覧表示内容の絞込み
			switch(haishaKbun)
			{
				case "TEIKI":
					{ // 定期の場合
						this.selectedRowsIchiran = table.Select("HAISHA_KBN = 0");
					}
					break;

				case "SPOT":
					{ // スポットの場合
						this.selectedRowsIchiran = table.Select("HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = false");
					}
					break;

				case "CONTENA":
					{ // コンテナ場合
						this.selectedRowsIchiran = table.Select("HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = true");
					}
					break;

				default:
					{ // 検索ボタン押下時の場合 ※switch判定のhaishaKbunは"SEARCH"が編集されてくる

						// 検索条件取得処理
						this.selectedRowsIchiran = table.Select(getSelectWhere());

						if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
						{ // 定期データ表示時

							// 表示・非表示判定
							setVisible("TEIKI");

							// 未登録数の表示
							this.teikiData_count = this.selectedRowsIchiran.Length;
							this.form.ntxt_TeikiMitouroku.Text = this.teikiData_count.ToString();
							this.form.ntxt_SpotMitouroku.Text = string.Empty;
                            this.form.ntxt_ContenaMitouroku.Text = string.Empty;
						}
						else
						{
							if(!(string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text)))
							{ // スポットデータ表示時

								// 表示・非表示判定
								setVisible("SPOT");

								// 未登録数の表示
								this.spotData_count = this.selectedRowsIchiran.Length;
                                this.form.ntxt_TeikiMitouroku.Text = string.Empty;
								this.form.ntxt_SpotMitouroku.Text = this.spotData_count.ToString();
                                this.form.ntxt_ContenaMitouroku.Text = string.Empty;
							}
							else
							{ // コンテナデータ表示時

								// 表示・非表示判定
								setVisible("CONTENA");

								// 未登録数の表示
								this.contenaData_count = this.selectedRowsIchiran.Length;
                                this.form.ntxt_TeikiMitouroku.Text = string.Empty;
                                this.form.ntxt_SpotMitouroku.Text = string.Empty;
								this.form.ntxt_ContenaMitouroku.Text = this.contenaData_count.ToString();
							}
						}
					}
					break;
			}

			// 一覧表示内容の絞込み後の項目の設定
			for(int i = 0; i < this.selectedRowsIchiran.Length; i++)
			{
				this.form.Ichiran.Rows.Add();

				// 削除チェック・登録チェック
				this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value = false;
				this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = false;

				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["EDABAN"].ToString()))
				{
					edaban = 1;
				}
				else
				{
					edaban = (long)this.selectedRowsIchiran[i]["EDABAN"];
				}

				// 作業日
				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["HAISHA_SAGYOU_DATE"].ToString()))
				{
					this.form.Ichiran.Rows[i].Cells["SAGYOU_DATE"].Value = null;
				}
				else
				{
					this.form.Ichiran.Rows[i].Cells["SAGYOU_DATE"].Value = this.selectedRowsIchiran[i]["HAISHA_SAGYOU_DATE"].ToString().Substring(0, 10);
				}

                // 伝票番号
                if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["HAISHA_DENPYOU_NO"].ToString()))
                {
                    this.form.Ichiran.Rows[i].Cells["HAISHA_DENPYOU_NO"].Value = null;
                }
                else
                {
                    this.form.Ichiran.Rows[i].Cells["HAISHA_DENPYOU_NO"].Value = this.selectedRowsIchiran[i]["HAISHA_DENPYOU_NO"].ToString();
                }

				// 運転者CD
				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["HAISHA_UNTENSHA_CD"].ToString()))
				{
					this.form.Ichiran.Rows[i].Cells["UNTENSHA_CD"].Value = null;
				}
				else
				{
					this.form.Ichiran.Rows[i].Cells["UNTENSHA_CD"].Value = this.selectedRowsIchiran[i]["HAISHA_UNTENSHA_CD"];
				}

				// 運転者名
				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["UNTENSHA_NAME"].ToString()))
				{
					this.form.Ichiran.Rows[i].Cells["UNTENSHA_NAME"].Value = null;
				}
				else
				{
					this.form.Ichiran.Rows[i].Cells["UNTENSHA_NAME"].Value = this.selectedRowsIchiran[i]["UNTENSHA_NAME"].ToString();
				}

				// コースCD
				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["HAISHA_COURSE_NAME_CD"].ToString()))
				{
					this.form.Ichiran.Rows[i].Cells["COURSE_CD"].Value = null;
				}
				else
				{
					this.form.Ichiran.Rows[i].Cells["COURSE_CD"].Value = this.selectedRowsIchiran[i]["HAISHA_COURSE_NAME_CD"];
				}

				// コース名
				if(string.IsNullOrEmpty(this.selectedRowsIchiran[i]["HAISHA_COURSE_NAME_CD"].ToString()))
				{
					this.form.Ichiran.Rows[i].Cells["COURSE_NAME"].Value = null;
				}
				else
				{
					// コース名称マスタからコース名を取得
					this.form.Ichiran.Rows[i].Cells["COURSE_NAME"].Value = getCourseName(this.selectedRowsIchiran[i]["HAISHA_COURSE_NAME_CD"].ToString());
				}

				// コンテナ表示項目の取得
				if(getMobileSyogunDataInsertContenaData(Int64.Parse(this.selectedRowsIchiran[i]["EDABAN"].ToString()), NODE_EDABAN_CONTENA) != 0)
				{
					// 設置・引揚日付
					this.form.Ichiran.Rows[i].Cells["SETTI_HIKIAGE_DATE"].Value = this.dateContenaIdouDate;

					// コンテナ種類CD
					this.form.Ichiran.Rows[i].Cells["CONTENA_SYURUI_CD"].Value = this.strContenaShuruiCd;

					// コンテナ種類名
					this.form.Ichiran.Rows[i].Cells["CONTENA_SYURUI_NAME"].Value = getContenaShurui(this.strContenaShuruiCd);

					// コンテナCD
					this.form.Ichiran.Rows[i].Cells["CONTENA_CD"].Value = this.strContenaCd;

					if(getContena(this.strContenaShuruiCd, this.strContenaCd) != 0)
					{
						// コンテナ名
						this.form.Ichiran.Rows[i].Cells["CONTENA_NAME"].Value = this.strContenaName;
					}
				}

				if(getMobileSyogunDataInsertContenaData(Int64.Parse(this.selectedRowsIchiran[i]["EDABAN"].ToString()), NODE_EDABAN_GENBAJISSEKI) != 0)
				{
					if(getGenba(this.strGenbaJissekiGyoushaCd, this.strGenbaJissekiGenbaCd) != 0)
					{
						// 現場名
						this.form.Ichiran.Rows[i].Cells["GENBA_NAME"].Value = this.strGenbaMgenbaName;
					}
				}

				// 登録用データの編集
				this.form.Ichiran.Rows[i].Cells["SEQ_NO"].Value = this.selectedRowsIchiran[i]["SEQ_NO"];
				this.form.Ichiran.Rows[i].Cells["EDABAN"].Value = this.selectedRowsIchiran[i]["EDABAN"];

				// 暫定
				if(!haishaKbun.Equals("CONTENA"))
				{   // 実績またはスポット

					string format = string.Format("EDABAN = {0} AND NODE_EDABAN = 2", edaban);
					dataRowList = table.Select(format);

					if((dataRowList != null) && (0 != dataRowList.Count()))
					{
						// 車種CD
						if(string.IsNullOrEmpty(dataRowList[0]["SHASHU_CD"].ToString()))
						{
							this.form.Ichiran.Rows[i].Cells["SHASHU_CD"].Value = null;
						}
						else
						{
							this.form.Ichiran.Rows[i].Cells["SHASHU_CD"].Value = dataRowList[0]["SHASHU_CD"].ToString();
						}

						// 車種名
						if(string.IsNullOrEmpty(dataRowList[0]["SHASHU_NAME"].ToString()))
						{
							this.form.Ichiran.Rows[i].Cells["SHASHU_NAME"].Value = null;
						}
						else
						{
							this.form.Ichiran.Rows[i].Cells["SHASHU_NAME"].Value = dataRowList[0]["SHASHU_NAME"].ToString();
						}

						// 車輌CD
						if(string.IsNullOrEmpty(dataRowList[0]["SHUKKO_SHARYOUCD"].ToString()))
						{
							this.form.Ichiran.Rows[i].Cells["SHARYOU_CD"].Value = null;
						}
						else
						{
							this.form.Ichiran.Rows[i].Cells["SHARYOU_CD"].Value = dataRowList[0]["SHUKKO_SHARYOUCD"];
						}

						// 車輌名
						if(string.IsNullOrEmpty(dataRowList[0]["SHARYOU_NAME"].ToString()))
						{
							this.form.Ichiran.Rows[i].Cells["SHARYOU_NAME"].Value = null;
						}
						else
						{
							this.form.Ichiran.Rows[i].Cells["SHARYOU_NAME"].Value = dataRowList[0]["SHARYOU_NAME"].ToString();
						}
					}
				}
			}

			// グリッドセル色の設定
			for(int i = 0; i < this.selectedRowsIchiran.Length; i++)
			{
				this.form.Ichiran.Rows[i].Cells["SAGYOU_DATE"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
                this.form.Ichiran.Rows[i].Cells["HAISHA_DENPYOU_NO"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["SHASHU_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["SHASHU_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["SHARYOU_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["SHARYOU_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["UNTENSHA_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["UNTENSHA_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["COURSE_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["COURSE_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["SETTI_HIKIAGE_DATE"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["CONTENA_SYURUI_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["CONTENA_SYURUI_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["CONTENA_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["CONTENA_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
				this.form.Ichiran.Rows[i].Cells["GENBA_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);
			}

			// 表示・非表示設定
			switch(haishaKbun)
			{
				case "TEIKI":
					{ // 定期の場合
						this.form.Ichiran.Columns["SYOUSAI_BTN"].Visible = false;           // 「詳細」
						this.form.Ichiran.Columns["SAGYOU_DATE"].Visible = true;            // 「作業日」
                        this.form.Ichiran.Columns["HAISHA_DENPYOU_NO"].Visible = true;      // 「定期配車伝票番号」
                        this.form.Ichiran.Columns["HAISHA_DENPYOU_NO"].HeaderText = "定期配車伝票番号";
						this.form.Ichiran.Columns["SHASHU_CD"].Visible = true;              // 「車種CD」
						this.form.Ichiran.Columns["SHASHU_NAME"].Visible = true;            // 「車種名」
						this.form.Ichiran.Columns["SHARYOU_CD"].Visible = true;             // 「車輌CD」
						this.form.Ichiran.Columns["SHARYOU_NAME"].Visible = true;           // 「車輌名」
						this.form.Ichiran.Columns["UNTENSHA_CD"].Visible = true;            // 「運転者CD」
						this.form.Ichiran.Columns["UNTENSHA_NAME"].Visible = true;          // 「運転者名」
						this.form.Ichiran.Columns["COURSE_CD"].Visible = true;              // 「コースCD」
						this.form.Ichiran.Columns["COURSE_NAME"].Visible = true;            // 「コース名」
						this.form.Ichiran.Columns["SETTI_HIKIAGE_DATE"].Visible = false;    // 「設置・引揚日付」
						this.form.Ichiran.Columns["CONTENA_SYURUI_CD"].Visible = false;     // 「コンテナ種類CD」
						this.form.Ichiran.Columns["CONTENA_SYURUI_NAME"].Visible = false;   // 「コンテナ種類名」
						this.form.Ichiran.Columns["CONTENA_CD"].Visible = false;            // 「コンテナCD」
						this.form.Ichiran.Columns["CONTENA_NAME"].Visible = false;          // 「コンテナ名」
						this.form.Ichiran.Columns["GENBA_NAME"].Visible = false;            // 「現場名」
					}
					break;

				case "SPOT":
					{ // スポットの場合
						this.form.Ichiran.Columns["SYOUSAI_BTN"].Visible = false;           // 「詳細」
						this.form.Ichiran.Columns["SAGYOU_DATE"].Visible = true;            // 「作業日」
                        this.form.Ichiran.Columns["HAISHA_DENPYOU_NO"].Visible = true;      // 「受付伝票番号」
                        this.form.Ichiran.Columns["HAISHA_DENPYOU_NO"].HeaderText = "受付伝票番号";
						this.form.Ichiran.Columns["SHASHU_CD"].Visible = true;              // 「車種CD」
						this.form.Ichiran.Columns["SHASHU_NAME"].Visible = true;            // 「車種名」
						this.form.Ichiran.Columns["SHARYOU_CD"].Visible = true;             // 「車輌CD」
						this.form.Ichiran.Columns["SHARYOU_NAME"].Visible = true;           // 「車輌名」
						this.form.Ichiran.Columns["UNTENSHA_CD"].Visible = true;            // 「運転者CD」
						this.form.Ichiran.Columns["UNTENSHA_NAME"].Visible = true;          // 「運転者名」
						this.form.Ichiran.Columns["COURSE_CD"].Visible = false;             // 「コースCD」
						this.form.Ichiran.Columns["COURSE_NAME"].Visible = false;           // 「コース名」
						this.form.Ichiran.Columns["SETTI_HIKIAGE_DATE"].Visible = false;    // 「設置・引揚日付」
						this.form.Ichiran.Columns["CONTENA_SYURUI_CD"].Visible = false;     // 「コンテナ種類CD」
						this.form.Ichiran.Columns["CONTENA_SYURUI_NAME"].Visible = false;   // 「コンテナ種類名」
						this.form.Ichiran.Columns["CONTENA_CD"].Visible = false;            // 「コンテナCD」
						this.form.Ichiran.Columns["CONTENA_NAME"].Visible = false;          // 「コンテナ名」
						this.form.Ichiran.Columns["GENBA_NAME"].Visible = false;            // 「現場名」
					}
					break;

				case "CONTENA":
					{ // コンテナの場合
						this.form.Ichiran.Columns["SYOUSAI_BTN"].Visible = true;            // 「詳細」
						this.form.Ichiran.Columns["SAGYOU_DATE"].Visible = false;           // 「作業日」
                        this.form.Ichiran.Columns["HAISHA_DENPYOU_NO"].Visible = false;     // 「伝票番号」
						this.form.Ichiran.Columns["SHASHU_CD"].Visible = false;             // 「車種CD」
						this.form.Ichiran.Columns["SHASHU_NAME"].Visible = false;           // 「車種名」
						this.form.Ichiran.Columns["SHARYOU_CD"].Visible = false;            // 「車輌CD」
						this.form.Ichiran.Columns["SHARYOU_NAME"].Visible = false;          // 「車輌名」
						this.form.Ichiran.Columns["UNTENSHA_CD"].Visible = false;           // 「運転者CD」
						this.form.Ichiran.Columns["UNTENSHA_NAME"].Visible = false;         // 「運転者名」
						this.form.Ichiran.Columns["COURSE_CD"].Visible = false;             // 「コースCD」
						this.form.Ichiran.Columns["COURSE_NAME"].Visible = false;           // 「コース名」
						this.form.Ichiran.Columns["SETTI_HIKIAGE_DATE"].Visible = true;     // 「設置・引揚日付」
						this.form.Ichiran.Columns["CONTENA_SYURUI_CD"].Visible = true;      // 「コンテナ種類CD」
						this.form.Ichiran.Columns["CONTENA_SYURUI_NAME"].Visible = true;    // 「コンテナ種類名」
						this.form.Ichiran.Columns["CONTENA_CD"].Visible = true;             // 「コンテナCD」
						this.form.Ichiran.Columns["CONTENA_NAME"].Visible = true;           // 「コンテナ名」
						this.form.Ichiran.Columns["GENBA_NAME"].Visible = true;             // 「現場名」
					}
					break;
			}
		}

		/// <summary>画面情報の初期化を行う</summary>
		internal void WindowInit()
		{
            try
            {
                // 親フォームのセット
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 取り込み済みデータ取得処理
                this.getTorikomizumiData("YUUKOU");

                // 画面初期表示設定
                this.initializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 一覧の表示
                this.SetIchiran("TEIKI");

                // 未登録数の表示
                this.SetMitourokusu("TEIKI");
                PopUpDataInit();

                // タイトル表示の切り替え
                var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
                titleControl.Text = MobileShougunTorikomiConst.Title1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
		}

		internal bool CheckCourse()
		{
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                //this.form.ntxt_CourseCd.AutoChangeBackColorEnabled = true;
                //if (strCourse != this.form.ntxt_CourseCd.Text)
                //{
                bool bFound = false;
                this.form.txt_CourseName.Text = string.Empty;
                if (this.form.ntxt_CourseCd.Text != string.Empty)
                {
                    if (getCourseName(this.form.ntxt_CourseCd.Text) == null || getCourseName(this.form.ntxt_CourseCd.Text) == string.Empty)
                    {
                        bFound = false;
                    }
                    else
                    {
                        bFound = true;

                        this.form.txt_CourseName.Text = getCourseName(this.form.ntxt_CourseCd.Text);
                        //strCourse = this.form.ntxt_CourseCd.Text;
                    }

                    if (!bFound)
                    {
                        //this.form.ntxt_CourseCd.AutoChangeBackColorEnabled = false;
                        this.form.ntxt_CourseCd.BackColor = Constans.ERROR_COLOR;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "入金先");
                        this.form.ntxt_CourseCd.Focus();
                        // strCourse = this.form.ntxt_CourseCd.Text;
                    }
                }
                else
                {
                    this.form.txt_CourseName.Text = string.Empty;
                    //strPrevNyukinSakiCd = this.form.txtNyukinSakiCd.Text;
                }
                //}

                if (this.form.ntxt_CourseCd.Text == string.Empty)
                {
                    this.form.txt_CourseName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckCourse", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
			return ret;
		}

		/// <summary>画面情報の再表示を行う</summary>
		internal void ReWindow()
		{
			// 取り込み済みデータ取得処理
			this.getTorikomizumiData("YUUKOU");

			// 画面初期表示設定
			this.initializeScreen();

			if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
			{ // 定期データ表示時

				// 一覧の表示
				this.SetIchiran("TEIKI");

				// 未登録数の表示
				this.SetMitourokusu("TEIKI");

				// タイトル表示の切り替え
                var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
				titleControl.Text = MobileShougunTorikomiConst.Title1;
			}
			else
			{

				if(contenaFlg)
				{   // コンテナ有

					if(!(string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text)))
					{ // スポットデータ表示時

						// 一覧の表示
						this.SetIchiran("SPOT");

						// 未登録数の表示
						this.SetMitourokusu("SPOT");

						// タイトル表示の切り替え
                        var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
						titleControl.Text = MobileShougunTorikomiConst.Title2;
					}
					else
					{ // コンテナデータ表示時

						// 一覧の表示
						this.SetIchiran("CONTENA");

						// 未登録数の表示
						this.SetMitourokusu("CONTENA");

						// タイトル表示の切り替え
                        var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
						titleControl.Text = MobileShougunTorikomiConst.Title3;
					}
				}
				else
				{   // コンテナ無

					// 一覧の表示
					this.SetIchiran("SPOT");

					// 未登録数の表示
					this.SetMitourokusu("SPOT");

					// タイトル表示の切り替え
                    var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
					titleControl.Text = MobileShougunTorikomiConst.Title2;
				}
			}
		}

		/// <summary>ボタン情報の設定を行う</summary>
		private ButtonSetting[] CreateButtonInfo()
		{
			var buttonSetting = new ButtonSetting();

			var thisAssembly = Assembly.GetExecutingAssembly();
			return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
		}

		/// <summary>イベント処理の初期化を行う</summary>
		private void EventInit()
		{
			// 「Functionボタン」のイベント生成
			this.parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);
			this.parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);
			this.parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);
			this.parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
			this.parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);
			this.parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);
			this.parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);

            // DataGridViewイベント生成
            this.form.Ichiran.CurrentCellDirtyStateChanged += new EventHandler(this.ichiranCurrentCellDirtyStateChanged);
            this.form.Ichiran.CellMouseClick += new DataGridViewCellMouseEventHandler(this.ichiranCellMouseClick);
            this.form.Ichiran.KeyDown += new KeyEventHandler(this.Ichiran_KeyDown);
            this.form.Ichiran.CellBeginEdit += new DataGridViewCellCancelEventHandler(Ichiran_CellBeginEdit);
            this.form.Ichiran.CellContentClick += new DataGridViewCellEventHandler(Ichiran_CellContentClick);

            /// 20141023 Houkakou 「モバイル将軍取込」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.dtp_SagyouDateTo.MouseDoubleClick += new MouseEventHandler(SagyouDateTo_MouseDoubleClick);
            /// 20141023 Houkakou 「モバイル将軍取込」のダブルクリックを追加する　end

		}

		/// <summary>XML入力先ファイルパスの取得</summary>
		/// <param name="path">XML入力先ディレクトリ</param>
		private string GetInPutPath(string path)
		{
			string retPath = string.Empty;

            try
            {
                var iniPath = AppData.PrepareLocalAppDataFile("mobileXMLPath.ini");

                // XML入力先ディレクトリ読み込み
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
            catch(Exception)
            {
                LogUtility.Error("G283:モバイル将軍データ取込 iniファイル読込失敗");
                throw;
            }
			return retPath;
		}

		/// <summary>XMLディレクトリ設定</summary>
		private void InitializePath()
		{
			// 取込ファイル
			this.mobileInPutFilePath = GetInPutPath();

			// バックアップファイル
			this.mobileBackUpPath = GetBackUpFilePath();
		}

		/// <summary>XMLファイルパス（取込ファイル）</summary>
		private String GetInPutPath()
		{
			// XMLパス設定
			return GetXmlPath("mobileInPutPath");
		}

		/// <summary>XMLファイルパス（バックアップファイル）</summary>
		private String GetBackUpFilePath()
		{
			// XMLパス設定
			return GetXmlPath("mobileBackUpPath");
		}

		/// <summary>XMLパス設定</summary>
		/// <param name="path">XML出力先ディレクトリ</param>
		private string GetXmlPath(string path)
		{
			string retPath = string.Empty;

            try
            {
                var iniPath = AppData.PrepareLocalAppDataFile("mobileXMLPath.ini");
                
                // XMLディレクトリ読み込み
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
            catch(Exception)
            {
                LogUtility.Error("G283:モバイル将軍データ取込 iniファイル読込失敗");
                throw;
            }
			return retPath;
		}

        /// <summary>
        /// モバイル将軍用データ取込画面専用テーブルの作成
        /// </summary>
        /// <param name="xmlFullPath">XMLファイルのフルパス</param>
        [Transaction]
        private void CreateMobileSyogunData(string[] xmlFullPath)
        {
            // xmlドキュメント
            XmlDocument xmldoc = new XmlDocument();

            // ノード指定の定義
            XmlNodeList haishaNodes = null;
            XmlNodeList shukkoNodes = null;
            XmlNodeList kikoNodes = null;
            XmlNodeList genbaJissekiNodes = null;
            XmlNodeList detailNodes = null;
            XmlNodeList hannyuuJissekiNodes = null;
            XmlNodeList containerDetailNodes = null;

            using (Transaction tran = new Transaction())
            {
                // xmlファイル数分の処理をする
                for (int i = 0; i < xmlFullPath.Length; ++i)
                {
                    this.fn = xmlFullPath[i];

                    if (!File.Exists(this.fn))
                    {   // ファイルが存在しない
                        continue;
                    }

                    LogUtility.Info("データ取込対象XML:" + this.fn);

                    //// XMLファイルを読み込む
                    //xmldoc.Load(this.fn);

                    try
                    {
                        // XMLファイルを読み込む
                        xmldoc.Load(this.fn);
                    }
                    catch
                    {
                        MessageUtility messageUtil = new MessageUtility();
                        string errorMessage = messageUtil.GetMessage("E120").MESSAGE;
                        MessageBox.Show(errorMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    
                    if (Path.GetFileName(this.fn).StartsWith("wn"))
                    { // 定期・スポットの場合（ファイル名の先頭二桁＝wn）

                        // ノード指定
                        haishaNodes = xmldoc.SelectNodes("denpyoJisseki/haisha");                   // 配車ヘッダレコード
                        shukkoNodes = xmldoc.SelectNodes("denpyoJisseki/shukkokiko/shukko");        // 出庫実績レコード
                        kikoNodes = xmldoc.SelectNodes("denpyoJisseki/shukkokiko/kiko");            // 帰庫実績レコード
                        genbaJissekiNodes = xmldoc.SelectNodes("denpyoJisseki/genbaJisseki");       // 現場実績レコード
                        detailNodes = xmldoc.SelectNodes("denpyoJisseki/genbaJisseki/detail");      // 現場明細レコード
                        hannyuuJissekiNodes = xmldoc.SelectNodes("denpyoJisseki/hannyuuJisseki");   // 搬入実績レコード

                        int sumCnt = 0;        // 除外フラグの合計
                        for (int cnt = 0; cnt < genbaJissekiNodes.Count; cnt++)
                        {
                            string jyogaiFlgTxt = "0";

                            if (!string.IsNullOrEmpty(genbaJissekiNodes.Item(cnt).SelectSingleNode("jyogaiFlg").InnerText))
                            {
                                jyogaiFlgTxt = genbaJissekiNodes.Item(cnt).SelectSingleNode("jyogaiFlg").InnerText;
                            }

                            sumCnt += int.Parse(jyogaiFlgTxt);
                        }

                        // 全ての現場実績で除外FLG=1だった場合、登録しない
                        if (sumCnt == genbaJissekiNodes.Count)
                        {
                            this.mobileInPutFilePathToMove[i] = xmlFullPath[i];
                            continue;
                        }

                        // モバイル将軍用データ取込画面専用テーブル登録処理
                        setMobileSyogunData(haishaNodes, "haisha", Path.GetFileName(this.fn));                       // 配車ヘッダレコード
                        setMobileSyogunData(shukkoNodes, "shukko", Path.GetFileName(this.fn));                       // 出庫実績レコード
                        setMobileSyogunData(kikoNodes, "kiko", Path.GetFileName(this.fn));                           // 帰庫実績レコード
                        setMobileSyogunData(genbaJissekiNodes, "genbaJisseki", Path.GetFileName(this.fn));           // 現場実績レコード
                        setMobileSyogunData(detailNodes, "detail", Path.GetFileName(this.fn));                       // 現場明細レコード
                        setMobileSyogunData(hannyuuJissekiNodes, "hannyuuJisseki", Path.GetFileName(this.fn));       // 搬入実績レコード

                        // 登録を完了したファイルのパスを保持
                        this.mobileInPutFilePathToMove[i] = xmlFullPath[i];
                    }
                    else if (Path.GetFileName(this.fn).StartsWith("cn"))
                    { // コンテナの場合（ファイル名の先頭二桁＝cn）

                        // ノード指定
                        haishaNodes = xmldoc.SelectNodes("containerJisseki/haisha");                                   // 配車ヘッダレコード
                        genbaJissekiNodes = xmldoc.SelectNodes("containerJisseki/genbaJisseki");                       // 現場実績レコード
                        containerDetailNodes = xmldoc.SelectNodes("containerJisseki/genbaJisseki/containerDetail");    // コンテナ明細レコード

                        // モバイル将軍用データ取込画面専用テーブル登録処理
                        setMobileSyogunData(haishaNodes, "haisha", Path.GetFileName(this.fn));                       // 配車ヘッダレコード
                        setMobileSyogunData(genbaJissekiNodes, "genbaJisseki", Path.GetFileName(this.fn));           // 現場実績レコード
                        setMobileSyogunData(containerDetailNodes, "containerDetail", Path.GetFileName(this.fn));     // コンテナ明細レコード 

                        // 登録を完了したファイルのパスを保持
                        this.mobileInPutFilePathToMove[i] = xmlFullPath[i];
                    }
                    else
                    {
                        // 対象外のファイル
                    }
                }

                //コミット
                tran.Commit();
            }
        }

		/// <summary>取込データのバックアップ（１世代のみ保存）</summary>
		private void XmlBackup()
		{
			// mobileBackUpPathフォルダの
			//「wn_」（定期・スポット配車実績ファイル）または
			//「cn_」（コンテナ配車実績ファイル）始まるファイルを
			// で始まるファイルを削除します。
			string[] deleteFiles = Directory.GetFiles(this.mobileBackUpPath, "wn_*.xml");
			foreach(string file in deleteFiles)
			{
				FileInfo deleteFile = new FileInfo(file);
                deleteFile.Delete();
			}

			deleteFiles = Directory.GetFiles(this.mobileBackUpPath, "cn_*.xml");
			foreach(string file in deleteFiles)
			{
				FileInfo deleteFile = new FileInfo(file);
                deleteFile.Delete();
			}

			// mobileInPutShuukinJissekiPathフォルダの
			//「wn_」（定期・スポット配車実績ファイル）または
			//「cn_」（コンテナ配車実績ファイル）始まるファイルを
			// mobileBackUpPathフォルダに移動します。
			foreach(string file in this.mobileInPutFilePathToMove)
			{
				if(file == null)
				{
					return;
				}

				FileInfo moveFile = new FileInfo(file);
				moveFile.MoveTo(this.mobileBackUpPath + "/" + Path.GetFileName(file));
			}
		}

		/// <summary>モバイル将軍用データ取込画面専用テーブル登録処理</summary>
		/// <param name="Nodes">取込ファイルの登録対象ノード</param>
		/// <param name="kbn">ノード区分</param>
		/// <param name="fileName">取込ファイル名</param>
		void setMobileSyogunData(XmlNodeList Nodes, string kbn, string fileName)
		{
			// DAO編集
			this.setMobileSyogunDataInsertDao = DaoInitUtility.GetComponent<SetMobileSyogunDataInsertDao>();

			// モバイル将軍用データ取込画面専用テーブルエンティティの定義
			T_MOBILE_SYOGUN_DATA_INSERT mobileSyogunDataInsertEntity;

			if(fileName.StartsWith("wn"))
			{ // 定期・スポットの場合（ファイル名の先頭二桁＝wn）

				#region - 定期・スポットの場合 -

				// 子ノードの編集
				switch(kbn)
				{
					// 配車ヘッダレコード
					case "haisha":

						#region - 配車ヘッダレコード -

						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// 枝番のMAX値を取得する　※同一枝番が同一xmlファイルのノード群となる
							getTorikomizumiData("MAX_EDABAN");
							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_HAISHA;

							// 伝票番号より、拠点CD・運転者名を取得して編集する（伝票番号も編集）
							if(childNode.SelectSingleNode("no").InnerText != string.Empty)
							{
								// 伝票番号
								mobileSyogunDataInsertEntity.HAISHA_DENPYOU_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);

								// 配車区分により、紐付く伝票先を切り替える
								if(childNode.SelectSingleNode("haishaKbn").InnerText != string.Empty)
								{
									var haishaKbn = Int16.Parse(childNode.SelectSingleNode("haishaKbn").InnerText);
									if(haishaKbn == HAISHA_KBN_SPOT)
									{
										// 配車区分がスポットの場合は受付伝票より情報を取得
										var entity = this.accessor.getUketsukeSsEntry(int.Parse(mobileSyogunDataInsertEntity.HAISHA_DENPYOU_NO.ToString()));
										if(entity != null)
										{
											// 受付番号に紐付く受付収集伝票が存在する場合
											mobileSyogunDataInsertEntity.KYOTEN_CD = entity.KYOTEN_CD;
											mobileSyogunDataInsertEntity.UNTENSHA_NAME = entity.UNTENSHA_NAME;
										}
									}
									else if(haishaKbn == HAISHA_KBN_CONTENA)
									{
										// コンテナ
									}
									else
									{
										// 配車区分が定期の場合は定期入力伝票より情報を取得
										var entity = this.accessor.GetTeikiHaishaEntry(Int64.Parse(mobileSyogunDataInsertEntity.HAISHA_DENPYOU_NO.ToString()));
										if(entity != null)
										{
											// 受付番号に紐付く受付収集伝票が存在する場合
											mobileSyogunDataInsertEntity.KYOTEN_CD = entity.KYOTEN_CD;

											// 運転者CDから運転者名を取得
											var shainEntity = this.shainDao.GetDataByCd(entity.UNTENSHA_CD);
											if(shainEntity != null)
											{
												mobileSyogunDataInsertEntity.UNTENSHA_NAME = shainEntity.SHAIN_NAME_RYAKU;
											}
										}
									}
								}
							}

							// コースCD
							mobileSyogunDataInsertEntity.HAISHA_COURSE_NAME_CD = childNode.SelectSingleNode("courseCd").InnerText;

							// 拠点CDがセットされていない場合、コース名称CDに紐付く拠点CDをセットする
							if(true == mobileSyogunDataInsertEntity.KYOTEN_CD.IsNull)
							{
								Int16 iKyotenCD = getKyotenCD(mobileSyogunDataInsertEntity.HAISHA_COURSE_NAME_CD);
								if(iKyotenCD != -1)
								{
									mobileSyogunDataInsertEntity.KYOTEN_CD = (SqlInt16)iKyotenCD;
								}
							}

							// 拠点CDが未登録の場合、システム設定-個別タブの拠点CDを参照する
							if(true == mobileSyogunDataInsertEntity.KYOTEN_CD.IsNull)
							{
								var userConf = CurrentUserCustomConfigProfile.Load();
								foreach(var item in userConf.Settings.DefaultValue)
								{
									if(item.Name.Equals("拠点CD"))
									{
										mobileSyogunDataInsertEntity.KYOTEN_CD = Int16.Parse(item.Value);
									}
								}
							}

							mobileSyogunDataInsertEntity.HAISHA_SAGYOU_DATE = Convert.ToDateTime(childNode.SelectSingleNode("workDate").InnerText);
							mobileSyogunDataInsertEntity.HAISHA_UNTENSHA_CD = childNode.SelectSingleNode("untenshaCd").InnerText;

							// 運転者名
							string shainName = string.Empty;
							DataRow shainData = null;

							if(this.allShainData != null && this.allShainData.Rows.Count > 0
								&& !string.IsNullOrEmpty(mobileSyogunDataInsertEntity.HAISHA_UNTENSHA_CD))
							{
								var dataRows = this.allShainData.Select("SHAIN_CD = '" + mobileSyogunDataInsertEntity.HAISHA_UNTENSHA_CD + "'");
								if(dataRows != null && dataRows.Count() > 0)
								{
									// キーを指定しているため一行だけとれるはず
									shainData = dataRows[0];
								}
							}
							if(shainData != null)
							{
								mobileSyogunDataInsertEntity.UNTENSHA_NAME = Convert.ToString(shainData["SHAIN_NAME_RYAKU"]);
							}
							else
							{
								mobileSyogunDataInsertEntity.UNTENSHA_NAME = string.Empty;
							}

							mobileSyogunDataInsertEntity.HAISHA_KBN = Int16.Parse(childNode.SelectSingleNode("haishaKbn").InnerText);
							mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_DATE = DateTime.Now;
							mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_FILENAME = Path.GetFileName(this.fn);

							if(childNode.SelectSingleNode("containerFlg").InnerText == "1")
							{
								mobileSyogunDataInsertEntity.HAISHA_CONTENA_FLG = true;
							}
							else
							{
								mobileSyogunDataInsertEntity.HAISHA_CONTENA_FLG = false;
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 配車ヘッダレコード -

						break;

					// 出庫実績レコード
					case "shukko":

						#region - 出庫実績レコード -

						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_SHUKKO;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.SHUKKO_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.SHUKKO_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.SHUKKO_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.SHUKKO_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.SHUKKO_SHUKKODATE = DateTime.Parse(childNode.SelectSingleNode("shukkoDate").InnerText);

							// 業者コード
							if(childNode.SelectSingleNode("gyoushaCd").InnerText != string.Empty)
							{
								mobileSyogunDataInsertEntity.SHUKKO_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
							}
							else
							{
								// 登録なし時はブランク
								mobileSyogunDataInsertEntity.SHUKKO_GYOUSHACD = "";
							}

							// 車輌コード
							if(childNode.SelectSingleNode("sharyouCd").InnerText != string.Empty)
							{
								mobileSyogunDataInsertEntity.SHUKKO_SHARYOUCD = childNode.SelectSingleNode("sharyouCd").InnerText;
							}

							// 車輌マスタから取得
							var table = dao.GetTeikiDispData(mobileSyogunDataInsertEntity.SHUKKO_GYOUSHACD, mobileSyogunDataInsertEntity.SHUKKO_SHARYOUCD);
							if(table.Rows.Count != 0)
							{
								// 車輌名
								int index = table.Columns.IndexOf("SHARYOU_NAME_RYAKU");
								if(!DBNull.Value.Equals(table.Rows[0].ItemArray[index]))
								{
									mobileSyogunDataInsertEntity.SHARYOU_NAME = (string)table.Rows[0].ItemArray[index];
								}
								else
								{
									mobileSyogunDataInsertEntity.SHARYOU_NAME = string.Empty;
								}

								// 車種CD
								index = table.Columns.IndexOf("SHASHU_CD");
								if(!DBNull.Value.Equals(table.Rows[0].ItemArray[index]))
								{
									mobileSyogunDataInsertEntity.SHASHU_CD = (string)table.Rows[0].ItemArray[index];
								}
								else
								{
									mobileSyogunDataInsertEntity.SHASHU_CD = string.Empty;
								}

								// 車種名
								index = table.Columns.IndexOf("SHASHU_NAME_RYAKU");
								if(!DBNull.Value.Equals(table.Rows[0].ItemArray[index]))
								{
									mobileSyogunDataInsertEntity.SHASHU_NAME = (string)table.Rows[0].ItemArray[index];
								}
								else
								{
									mobileSyogunDataInsertEntity.SHASHU_NAME = string.Empty;
								}
							}

							if(childNode.SelectSingleNode("tenki") != null)
							{
								if(childNode.SelectSingleNode("tenki").InnerText != string.Empty)
								{ // 出庫実績レコードの業者CDが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
									mobileSyogunDataInsertEntity.SHUKKO_TENKI = childNode.SelectSingleNode("tenki").InnerText;
								}
							}

							if(childNode.SelectSingleNode("meter").InnerText != string.Empty)
							{ // 出庫実績レコードの出庫メーターが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
								mobileSyogunDataInsertEntity.SHUKKO_METER = decimal.Parse(childNode.SelectSingleNode("meter").InnerText);
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 出庫実績レコード -

						break;

					// 帰庫実績レコード
					case "kiko":

						#region - 帰庫実績レコード -

						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_KIKO;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.KIKO_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.KIKO_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.KIKO_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.KIKO_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.KIKO_KIKODATE = DateTime.Parse(childNode.SelectSingleNode("kikoDate").InnerText);

							if(childNode.SelectSingleNode("meter").InnerText != string.Empty)
							{ // 帰庫実績レコードの帰庫メーターが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
								mobileSyogunDataInsertEntity.KIKO_METER = decimal.Parse(childNode.SelectSingleNode("meter").InnerText);
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 帰庫実績レコード -

						break;

					// 現場実績レコード
					case "genbaJisseki":

						#region - 現場実績レコード -

						foreach(XmlNode childNode in Nodes)
						{
                            if (childNode.SelectSingleNode("jyogaiFlg").InnerText == "1")
                            {
                                continue;
                            }

							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_GENBAJISSEKI;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_SHUUSHUUTIME = DateTime.Parse(childNode.SelectSingleNode("shuushuuTime").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_GENBACD = childNode.SelectSingleNode("genbaCd").InnerText;

							if(childNode.SelectSingleNode("addGenbaFlg").InnerText == "1")
							{
								mobileSyogunDataInsertEntity.GENBA_JISSEKI_ADDGENBAFLG = true;
							}
							else
							{
								mobileSyogunDataInsertEntity.GENBA_JISSEKI_ADDGENBAFLG = false;
							}

							mobileSyogunDataInsertEntity.GENBA_JISSEKI_SHUKKONO = int.Parse(childNode.SelectSingleNode("shukkoNo").InnerText);

							if(childNode.SelectSingleNode("jyogaiFlg").InnerText == "1")
							{
								mobileSyogunDataInsertEntity.GENBA_JISSEKI_JYOGAIFLG = true;
							}
							else
							{ // 0or出力されていない場合
								mobileSyogunDataInsertEntity.GENBA_JISSEKI_JYOGAIFLG = false;
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 現場実績レコード -

						break;

					// 現場明細レコード
					case "detail":

						#region - 現場明細レコード -
						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_DETAIL;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.GENBA_DETAIL_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_DETAIL_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_DETAIL_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.GENBA_DETAIL_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.GENBA_DETAIL_HINMEICD = childNode.SelectSingleNode("hinmeiCd").InnerText;

							if(childNode.SelectSingleNode("suuryo1").InnerText != string.Empty)
							{
								mobileSyogunDataInsertEntity.GENBA_DETAIL_SUURYO1 = decimal.Parse(childNode.SelectSingleNode("suuryo1").InnerText);
							}

							if((childNode.SelectSingleNode("suuryo1").InnerText != string.Empty) &&
								(childNode.SelectSingleNode("unitCd1").InnerText != string.Empty))
							{ // 数量１が空なら無視
								mobileSyogunDataInsertEntity.GENBA_DETAIL_UNIT_CD1 = Int16.Parse(childNode.SelectSingleNode("unitCd1").InnerText);
							}

							if(childNode.SelectSingleNode("suuryo2") != null)
							{ // 現場明細レコードの数量２のタグが存在する場合　※タグ無しで取り込む場合もあるための処理
								if((childNode.SelectSingleNode("suuryo2").InnerText != string.Empty) &&
									(childNode.SelectSingleNode("unitCd2").InnerText != string.Empty))
								{ // 数量２、単位２は、どちらかが空ならもう一方も無視
                                    mobileSyogunDataInsertEntity.GENBA_DETAIL_SUURYO2 = decimal.Parse(childNode.SelectSingleNode("suuryo2").InnerText);
									mobileSyogunDataInsertEntity.GENBA_DETAIL_UNIT_CD2 = Int16.Parse(childNode.SelectSingleNode("unitCd2").InnerText);
								}
							}

							if(childNode.SelectSingleNode("addHinmeiFlg").InnerText == "1")
							{
								mobileSyogunDataInsertEntity.GENBA_DETAIL_ADDHINMEIFLG = true;
							}
							else
							{
								mobileSyogunDataInsertEntity.GENBA_DETAIL_ADDHINMEIFLG = false;
							}

							if(childNode.SelectSingleNode("maniNo") != null)
							{ // 現場明細レコードのマニフェスト番号のタグが存在する場合　※タグ無しで取り込む場合もあるための処理
                                if (childNode.SelectSingleNode("maniNo").InnerText != string.Empty)
                                {
                                    mobileSyogunDataInsertEntity.GENBA_DETAIL_MANINO = Int64.Parse(childNode.SelectSingleNode("maniNo").InnerText);
                                }
							}

							mobileSyogunDataInsertEntity.GENBA_DETAIL_MANIKBN = Int16.Parse(childNode.SelectSingleNode("maniKbn").InnerText);

							if(childNode.SelectSingleNode("hannyuuNo").InnerText != string.Empty)
							{ // 現場明細レコードの搬入番号が存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
								mobileSyogunDataInsertEntity.GENBA_DETAIL_HANNYUUNO = int.Parse(childNode.SelectSingleNode("hannyuuNo").InnerText);
							}

							mobileSyogunDataInsertEntity.GENBA_DETAIL_GENBA_NO = int.Parse(childNode.SelectSingleNode("genbaNo").InnerText);

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 現場明細レコード -

						break;

					// 搬入実績レコード
					case "hannyuuJisseki":

						#region - 搬入実績レコード -

						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_HANNYUUJISSEKI;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.HANNYUU_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.HANNYUU_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.HANNYUU_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.HANNYUU_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.HANNYUU_HANNYUUDATE = DateTime.Parse(childNode.SelectSingleNode("hannyuuDate").InnerText);
							mobileSyogunDataInsertEntity.HANNYUU_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
							mobileSyogunDataInsertEntity.HANNYUU_GENBACD = childNode.SelectSingleNode("genbaCd").InnerText;
                            mobileSyogunDataInsertEntity.HANNYUU_RYO = decimal.Parse(childNode.SelectSingleNode("ryo").InnerText);

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}

						#endregion - 搬入実績レコード -

						break;
				}

				#endregion - 定期・スポットの場合 -
			}
			else
			{ // コンテナの場合（ファイル名の先頭二桁＝cn）

				#region - コンテナの場合 -

				// 子ノードの編集
				switch(kbn)
				{
					// 配車ヘッダレコード   
					case "haisha":
						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// 枝番のMAX値を取得する　※同一枝番が同一xmlファイルのノード群となる
							getTorikomizumiData("MAX_EDABAN");
							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_HAISHA;

							// 伝票番号より、拠点CD・車種CD・車輌CDを取得して編集する（伝票番号も編集）
							if(childNode.SelectSingleNode("no").InnerText != string.Empty)
							{ //  配車ヘッダレコードの伝票番号が存在する場合　※タグ有り、値が空で取り込む場合もあるための処理

								// 伝票番号
								mobileSyogunDataInsertEntity.HAISHA_DENPYOU_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);

								var entity = this.accessor.getUketsukeSsEntry(int.Parse(childNode.SelectSingleNode("no").InnerText));
								if(entity != null)
								{
									// 受付番号に紐付く受付収集伝票が存在する場合
									mobileSyogunDataInsertEntity.KYOTEN_CD = entity.KYOTEN_CD;
									mobileSyogunDataInsertEntity.SHASHU_CD = entity.SHASHU_CD;
									mobileSyogunDataInsertEntity.SHARYOU_CD = entity.SHARYOU_CD;
								}
							}

							mobileSyogunDataInsertEntity.HAISHA_SAGYOU_DATE = Convert.ToDateTime(childNode.SelectSingleNode("workDate").InnerText);
							mobileSyogunDataInsertEntity.HAISHA_UNTENSHA_CD = childNode.SelectSingleNode("untenshaCd").InnerText;
							mobileSyogunDataInsertEntity.HAISHA_KBN = Int16.Parse(childNode.SelectSingleNode("haishaKbn").InnerText);
							mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_DATE = DateTime.Now;
							mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_FILENAME = Path.GetFileName(this.fn);

							if(childNode.SelectSingleNode("courseCd").InnerText != string.Empty)
							{
								mobileSyogunDataInsertEntity.HAISHA_COURSE_NAME_CD = childNode.SelectSingleNode("courseCd").InnerText;
							}

							if(childNode.SelectSingleNode("containerFlg").InnerText == "1")
							{
								mobileSyogunDataInsertEntity.HAISHA_CONTENA_FLG = true;
							}
							else
							{
								mobileSyogunDataInsertEntity.HAISHA_CONTENA_FLG = false;
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}
						break;

					// 現場実績レコード
					case "genbaJisseki":
						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_GENBAJISSEKI;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
							mobileSyogunDataInsertEntity.GENBA_JISSEKI_GENBACD = childNode.SelectSingleNode("genbaCd").InnerText;

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}
						break;

					// コンテナ明細レコード
					case "containerDetail":
						foreach(XmlNode childNode in Nodes)
						{
							// モバイル将軍用データ取込画面専用テーブルエンティティの初期化
							mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

							// ノード枝番の編集
							mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_CONTENA;

							mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
							mobileSyogunDataInsertEntity.CONTENA_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_NO = childNode.SelectSingleNode("no").InnerText;
							mobileSyogunDataInsertEntity.CONTENA_GENBA_NO = Int16.Parse(childNode.SelectSingleNode("genbaNo").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_IDOUDATE = DateTime.Parse(childNode.SelectSingleNode("idouDate").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_KYOTEN_CD = Int16.Parse(childNode.SelectSingleNode("kyotenCd").InnerText);
							mobileSyogunDataInsertEntity.CONTENA_IDOU_KBN = Int16.Parse(childNode.SelectSingleNode("idouKbn").InnerText);

							if(!(string.IsNullOrEmpty(childNode.SelectSingleNode("containerShuruiCd").InnerText)))
							{
								mobileSyogunDataInsertEntity.CONTENA_SHURUI_CD = childNode.SelectSingleNode("containerShuruiCd").InnerText;
							}
							else
							{ // コンテナマスタUPDATE時のKEY項目が無い場合

								// 取り込みNGメッセージの表示
								MessageBox.Show(MobileShougunTorikomiConst.Msg4);
							}

							if(!(string.IsNullOrEmpty(childNode.SelectSingleNode("containerCd").InnerText)))
							{
								mobileSyogunDataInsertEntity.CONTENA_CD = childNode.SelectSingleNode("containerCd").InnerText;
							}
							else
							{ // コンテナマスタUPDATE時のKEY項目が無い場合

								// 取り込みNGメッセージの表示
								MessageBox.Show(MobileShougunTorikomiConst.Msg5);
							}

							// モバイル将軍用データ取込画面専用テーブル登録
							setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
						}
						break;
				}
			}

				#endregion - コンテナの場合 -
		}

		/// <summary>モバイル将軍用データ取込画面専用テーブル登録</summary>
		void setMobileSyogunDataInsert(T_MOBILE_SYOGUN_DATA_INSERT Entity)
		{
			// シーケンシャルナンバーのMAX値を取得する
			getTorikomizumiData("MAX_SEQ_NO");

			// キー項目の編集
			Entity.SEQ_NO = this.Max_Seq_No + 1;

			// 共通項目の編集
			Entity.JISSEKI_REGIST_FLG = false;
			Entity.DELETE_FLG = false;

			// モバイル将軍用データ取込画面専用テーブルに登録する
			var dataBinderContenaResult = new DataBinderLogic<T_MOBILE_SYOGUN_DATA_INSERT>(Entity);
			dataBinderContenaResult.SetSystemProperty(Entity, false);
			setMobileSyogunDataInsertDao.Insert(Entity);
		}

		/// <summary>定期実績入力テーブル登録</summary>
		void setTeikiJissekiEntry(DataRow[] selectedRowsEdaban)
		{
			// 編集領域
			// 編集フラグと編集項目を含んだ配列を作成する
			// [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
			// [1]＝編集有無フラグが編集ありの場合に編集される項目値
			// （エンティティを初期化することによる、int項目のnull値許容対応）
			string[] KYOTEN_CD = new string[2];
			string[] WEATHER = new string[2];
			string[] DENPYOU_DATE = new string[2];
			string[] SEARCH_DENPYOU_DATE = new string[2];
			string[] SAGYOU_DATE = new string[2];
			string[] SEARCH_SAGYOU_DATE = new string[2];
			string[] COURSE_NAME_CD = new string[2];
			string[] SHARYOU_CD = new string[2];
			string[] SHASHU_CD = new string[2];
			string[] UNTENSHA_CD = new string[2];
			string[] HOJOIN_CD = new string[2];
			string[] SHUKKO_METER = new string[2];
			string[] SHUKKO_HOUR = new string[2];
			string[] SHUKKO_MINUTE = new string[2];
			string[] KIKO_METER = new string[2];
			string[] KIKO_HOUR = new string[2];
			string[] KIKO_MINUTE = new string[2];
			string[] TEIKI_HAISHA_NUMBER = new string[2];
			string[] MOBILE_SHOGUN_FILE_NAME = new string[2];
			string[] DELETE_FLG = new string[2];
            string[] UNPAN_GYOUSHA_CD = new string[2];

			// 編集ありフラグ
			string HensyuFlg_Ari = "1";
			string HensyuFlg_Nashi = "0";

			// 各項目の編集
			// 配車ヘッダレコードの情報を取得する
			for(int i = 0; i < selectedRowsEdaban.Length; i++)
			{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) != NODE_EDABAN_HAISHA)
				{   // 配車ヘッダレコードでない場合
					continue;
				}

				// 拠点CD：配車ヘッダレコードの拠点CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["KYOTEN_CD"].ToString())))
				{
					KYOTEN_CD[0] = HensyuFlg_Ari;
					KYOTEN_CD[1] = selectedRowsEdaban[i]["KYOTEN_CD"].ToString();
				}
				else
				{
					KYOTEN_CD[0] = HensyuFlg_Nashi;
				}

				//// 伝票日付：配車ヘッダレコードの伝票番号をキーに定期配車入力テーブルから取得
				//if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
				//{   // 配車ヘッダレコードの伝票番号が存在する場合

				//    if (getTeikiHaishaEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())) != 0)
				//    { // 配車ヘッダレコードの伝票番号に紐付く定期配車入力テーブルの伝票日付が存在する場合
				//        DENPYOU_DATE[0] = HensyuFlg_Ari;
				//        DENPYOU_DATE[1] = this.dtDenpyouDate.ToString();
				//    }
				//    else
				//    {
				DENPYOU_DATE[0] = HensyuFlg_Nashi;
				//    }
				//}

				// 作業日：配車ヘッダレコードの作業日を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString())))
				{
					SAGYOU_DATE[0] = HensyuFlg_Ari;
					SAGYOU_DATE[1] = selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString();
				}
				else
				{
					SAGYOU_DATE[0] = HensyuFlg_Nashi;
				}

				// コース名称CD：配車ヘッダレコードのコース名称CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString())))
				{
					COURSE_NAME_CD[0] = HensyuFlg_Ari;
					COURSE_NAME_CD[1] = selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString();
				}
				else
				{
					COURSE_NAME_CD[0] = HensyuFlg_Nashi;
				}

				// 車輌CD：出庫実績レコードの車輌コードを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHARYOU_CD"].ToString())))
				{
					SHARYOU_CD[0] = HensyuFlg_Ari;
					SHARYOU_CD[1] = selectedRowsEdaban[i]["SHARYOU_CD"].ToString();
				}
				else
				{
					SHARYOU_CD[0] = HensyuFlg_Nashi;
				}

				// 車種CD：配車ヘッダレコードの車種CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHASHU_CD"].ToString())))
				{
					SHASHU_CD[0] = HensyuFlg_Ari;
					SHASHU_CD[1] = selectedRowsEdaban[i]["SHASHU_CD"].ToString();
				}
				else
				{
					SHASHU_CD[0] = HensyuFlg_Nashi;
				}

				// 運転者CD：配車ヘッダレコードの運転者CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString())))
				{
					UNTENSHA_CD[0] = HensyuFlg_Ari;
					UNTENSHA_CD[1] = selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString();
				}
				else
				{
					UNTENSHA_CD[0] = HensyuFlg_Nashi;
				}

				// 補助員CD：配車ヘッダレコードの伝票番号をキーに受付(収集)入力テーブルから取得
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
				{   // 配車ヘッダレコードの伝票番号が存在する場合

					var entity = this.accessor.getUketsukeSsEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString()));
					if((entity != null) && (false == string.IsNullOrEmpty(entity.HOJOIN_CD)))
					{
						// 受付番号に紐付く受付収集伝票に補助員CDが存在する場合
						HOJOIN_CD[0] = HensyuFlg_Ari;
						HOJOIN_CD[1] = entity.HOJOIN_CD;
					}
					else
					{
						HOJOIN_CD[0] = HensyuFlg_Nashi;
					}
				}

				// 定期配車番号：配車ヘッダレコードの伝票番号を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
				{
					TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Ari;
					TEIKI_HAISHA_NUMBER[1] = selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString();
				}
				else
				{
					TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Nashi;
				}

				// モバイル将軍ファイル名：配車ヘッダレコードの取込ファイル名を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_TORIKOMI_FILENAME"].ToString())))
				{
					MOBILE_SHOGUN_FILE_NAME[0] = HensyuFlg_Ari;
					MOBILE_SHOGUN_FILE_NAME[1] = selectedRowsEdaban[i]["HAISHA_TORIKOMI_FILENAME"].ToString();
				}
				else
				{
					MOBILE_SHOGUN_FILE_NAME[0] = HensyuFlg_Nashi;
				}
			}

			// 現場実績レコードの情報を取得する
			for(int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
			{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_GENBAJISSEKI)
				{   // 現場実績レコードでない場合
					continue;
				}

				// 現場実績レコードの出庫Noに紐付く出庫実績レコードの各項目を編集する
				// 出庫実績レコードの情報を取得する
				for(int iSJ = 0; iSJ < selectedRowsEdaban.Length; iSJ++)
				{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

					if(Int64.Parse(selectedRowsEdaban[iSJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_SHUKKO)
					{   // 出庫実績レコードでない場合
						continue;
					}

					if(int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUKKONO"].ToString()) != int.Parse(selectedRowsEdaban[iSJ]["SHUKKO_NO"].ToString()))
					{   // 現場実績レコードの出庫Noと出庫実績レコードのレコード番号が紐付いていない場合（実質紐付くのは１レコードのみ）
						continue;
					}

					// 車輌CDと車種CDが登録されていなかった場合
					if((SHARYOU_CD[0] == HensyuFlg_Nashi) && (SHASHU_CD[0] == HensyuFlg_Nashi))
					{
						// 車輌CD：出庫実績レコードの車輌コードを編集する
						if(!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString())))
						{
							SHARYOU_CD[0] = HensyuFlg_Ari;
							SHARYOU_CD[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString();
						}
						else
						{
							SHARYOU_CD[0] = HensyuFlg_Nashi;
						}

						// 車種CD：配車ヘッダレコードの車種CDを編集する
						if(!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHASHU_CD"].ToString())))
						{
							SHASHU_CD[0] = HensyuFlg_Ari;
							SHASHU_CD[1] = selectedRowsEdaban[iSJ]["SHASHU_CD"].ToString();
						}
						else
						{
							SHASHU_CD[0] = HensyuFlg_Nashi;
						}
					}

                    // 運搬業者：
                    if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_GYOUSHACD"].ToString())))
                    {
                        UNPAN_GYOUSHA_CD[0] = HensyuFlg_Ari;
                        UNPAN_GYOUSHA_CD[1] = selectedRowsEdaban[iSJ]["SHUKKO_GYOUSHACD"].ToString();
                    }
                    else
                    {
                        UNPAN_GYOUSHA_CD[0] = HensyuFlg_Nashi;
                    }

                    // 天気：配車ヘッダレコードの天気を編集する
                    if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_TENKI"].ToString())))
                    {
                        WEATHER[0] = HensyuFlg_Ari;
                        WEATHER[1] = selectedRowsEdaban[iSJ]["SHUKKO_TENKI"].ToString();
                    }
                    else
                    {
                        WEATHER[0] = HensyuFlg_Nashi;
                    }

					// 出庫メーター：出庫実績レコードの出庫メーターを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_METER"].ToString())))
					{
						SHUKKO_METER[0] = HensyuFlg_Ari;
						SHUKKO_METER[1] = selectedRowsEdaban[iSJ]["SHUKKO_METER"].ToString();
					}
					else
					{
						SHUKKO_METER[0] = HensyuFlg_Nashi;
					}

					// 出庫時間_時
					// 出庫時間_分
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString())))
					{
						SHUKKO_HOUR[0] = HensyuFlg_Ari;
						SHUKKO_MINUTE[0] = HensyuFlg_Ari;

						if(selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(13, 1) == ":")
						{   // 時間表記が2文字の場合（例＝2013/10/02 15:41:05）

							// 出庫時間_時：出庫実績レコードの出庫日時の時を編集する
							SHUKKO_HOUR[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(11, 2);

							// 出庫時間_分：出庫実績レコードの出庫日時の分を編集する
							SHUKKO_MINUTE[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(14, 2);
						}
						else
						{   // 時間表記が1文字の場合（例＝2013/09/30 9:13:00）

							// 出庫時間_時：出庫実績レコードの出庫日時の時を編集する
							SHUKKO_HOUR[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(11, 1);

							// 出庫時間_分：出庫実績レコードの出庫日時の分を編集する
							SHUKKO_MINUTE[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(13, 2);
						}
					}
					else
					{
						SHUKKO_HOUR[0] = HensyuFlg_Nashi;
						SHUKKO_MINUTE[0] = HensyuFlg_Nashi;
					}
				}

				// 現場実績レコードの出庫Noに紐付く帰庫実績レコードの各項目を編集する
				// 帰庫実績レコードの情報を取得する
				for(int iKJ = 0; iKJ < selectedRowsEdaban.Length; iKJ++)
				{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

					if(Int64.Parse(selectedRowsEdaban[iKJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_KIKO)
					{   // 帰庫実績レコードでない場合
						continue;
					}

					if(int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUKKONO"].ToString()) != int.Parse(selectedRowsEdaban[iKJ]["KIKO_NO"].ToString()))
					{   // 現場実績レコードの出庫Noと帰庫実績レコードのレコード番号が紐付いていない場合（実質紐付くのは１レコードのみ）
						continue;
					}

					// 帰庫メーター：帰庫実績レコードの帰庫メーターを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iKJ]["KIKO_METER"].ToString())))
					{
						KIKO_METER[0] = HensyuFlg_Ari;
						KIKO_METER[1] = selectedRowsEdaban[iKJ]["KIKO_METER"].ToString();
					}
					else
					{
						KIKO_METER[0] = HensyuFlg_Nashi;
					}

					// 帰庫時間_時
					// 帰庫時間_分
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString())))
					{
						KIKO_HOUR[0] = HensyuFlg_Ari;
						KIKO_MINUTE[0] = HensyuFlg_Ari;

						if(selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(13, 1) == ":")
						{   // 時間表記が2文字の場合（例＝2013/10/02 15:41:05）

							// 帰庫時間_時：帰庫実績レコードの帰庫日時の時を編集する
							KIKO_HOUR[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(11, 2);

							// 帰庫時間_分：帰庫実績レコードの帰庫日時の分を編集する
							KIKO_MINUTE[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(14, 2);
						}
						else
						{   // 時間表記が1文字の場合（例＝2013/09/30 9:13:00）

							// 帰庫時間_時：帰庫実績レコードの帰庫日時の時を編集する
							KIKO_HOUR[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(11, 1);

							// 帰庫時間_分：帰庫実績レコードの帰庫日時の分を編集する
							KIKO_MINUTE[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(13, 2);
						}
					}
					else
					{
						KIKO_HOUR[0] = HensyuFlg_Nashi;
						KIKO_MINUTE[0] = HensyuFlg_Nashi;
					}
				}

				// 定期実績入力テーブルテーブルエンティティの初期化
				T_TEIKI_JISSEKI_ENTRY Entity = new T_TEIKI_JISSEKI_ENTRY();

				// 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
				// 定期実績明細、定期実績荷卸の編集用の退避

				// SYSTEM_IDの採番
				Entity.SYSTEM_ID = this.accessor.createSystemIdForTj();
				this.int64SystemId = Entity.SYSTEM_ID;

				// 枝番
				Entity.SEQ = 1;

				// 定期実績番号
				Entity.TEIKI_JISSEKI_NUMBER = this.accessor.createTjNumber();
				this.int64TeikiJissekiNumber = Entity.TEIKI_JISSEKI_NUMBER;

				// 拠点CD
				if(KYOTEN_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.KYOTEN_CD = Int16.Parse(KYOTEN_CD[1]);
				}

				// 天気
				if(WEATHER[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.WEATHER = WEATHER[1];
				}

				// 伝票日付
				if(DENPYOU_DATE[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.DENPYOU_DATE = DateTime.Parse(DENPYOU_DATE[1]);
				}

				// 作業日
				if(SAGYOU_DATE[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.SAGYOU_DATE = DateTime.Parse(SAGYOU_DATE[1]);
				}

				// コース名称CD
				if(COURSE_NAME_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.COURSE_NAME_CD = COURSE_NAME_CD[1];
				}

                // 運搬業者CD
                if (UNPAN_GYOUSHA_CD[0] == HensyuFlg_Ari)
                { // 編集ありの場合
                    Entity.UNPAN_GYOUSHA_CD = UNPAN_GYOUSHA_CD[1];
                }

				// 車輌CD
				if(SHARYOU_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.SHARYOU_CD = SHARYOU_CD[1];
				}

				// 車種CD
				if(SHASHU_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.SHASHU_CD = SHASHU_CD[1];
				}

				// 運転者CD
				if(UNTENSHA_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.UNTENSHA_CD = UNTENSHA_CD[1];
				}

				// 補助員CD
				if(HOJOIN_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.HOJOIN_CD = HOJOIN_CD[1];
				}

				// 出庫メーター
				if(SHUKKO_METER[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
                    Entity.SHUKKO_METER = decimal.Parse(SHUKKO_METER[1]);
				}

				// 出庫時間_時
				if(SHUKKO_HOUR[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.SHUKKO_HOUR = Int16.Parse(SHUKKO_HOUR[1]);
				}

				// 出庫時間_分
				if(SHUKKO_MINUTE[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.SHUKKO_MINUTE = Int16.Parse(SHUKKO_MINUTE[1]);
				}

				// 帰庫メーター
				if(KIKO_METER[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
                    Entity.KIKO_METER = decimal.Parse(KIKO_METER[1]);
				}

				// 帰庫時間_時
				if(KIKO_HOUR[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.KIKO_HOUR = Int16.Parse(KIKO_HOUR[1]);
				}

				// 帰庫時間_分
				if(KIKO_MINUTE[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.KIKO_MINUTE = Int16.Parse(KIKO_MINUTE[1]);
				}

				// 定期配車番号
				if(TEIKI_HAISHA_NUMBER[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.TEIKI_HAISHA_NUMBER = Int64.Parse(TEIKI_HAISHA_NUMBER[1]);
					this.HaishaDenNo = int.Parse(TEIKI_HAISHA_NUMBER[1]);
				}

				// モバイル将軍ファイル名
				if(MOBILE_SHOGUN_FILE_NAME[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.MOBILE_SHOGUN_FILE_NAME = MOBILE_SHOGUN_FILE_NAME[1];
				}

				Entity.DELETE_FLG = false;      // 削除フラグ

				// 定期実績入力テーブルに登録する
				var dataBinderContenaResult = new DataBinderLogic<T_TEIKI_JISSEKI_ENTRY>(Entity);
				dataBinderContenaResult.SetSystemProperty(Entity, false);
				setTeikiJissekiEntryDao.Insert(Entity);

				break;
			}
		}

		/// <summary>定期実績明細テーブル登録</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void setTeikiJissekiDetail(DataRow[] selectedRowsEdaban)
		{
			int index;
			// 編集領域
			// 編集フラグと編集項目を含んだ配列を作成する
			// [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
			// [1]＝編集有無フラグが編集ありの場合に編集される項目値
			// （エンティティを初期化することによる、int項目のnull値許容対応）
			string[] GYOUSHA_CD = new string[2];
			string[] GENBA_CD = new string[2];
			string[] HINMEI_CD = new string[2];
			string[] SUURYOU = new string[2];
			string[] UNIT_CD = new string[2];
			string[] KANSAN_SUURYOU = new string[2];
			string[] KANSAN_UNIT_CD = new string[2];
			string[] NIOROSHI_NUMBER = new string[2];
			string[] TSUKIGIME_KBN = new string[2];
			string[] SHUUSHUU_TIME = new string[2];
            string[] TEIKI_HAISHA_NUMBER = new string[2];
            string[] ROUND_NO = new string[2];
            string[] ROW_NO = new string[2];

			// 編集ありフラグ
			string HensyuFlg_Ari = "1";
			string HensyuFlg_Nashi = "0";

            List<T_TEIKI_JISSEKI_DETAIL> JissekiDetailEntityList = new List<T_TEIKI_JISSEKI_DETAIL>();

			// 各項目の編集
			// 配車ヘッダレコードの情報を取得する
			for(int i = 0; i < selectedRowsEdaban.Length; i++)
			{ // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) != NODE_EDABAN_HAISHA)
				{ // 配車ヘッダレコードでない場合
					continue;
				}

				// 月極区分：配車ヘッダレコードのコース名称CDをキーにコース_明細内訳マスタから取得
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString())))
				{ // 配車ヘッダレコードのコース名称CDが存在する場合
					TSUKIGIME_KBN[0] = HensyuFlg_Nashi;
				}

                // 定期配車番号：行番号の編集用時の検索条件用に保持
                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
                {
                    TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Ari;
                    TEIKI_HAISHA_NUMBER[1] = selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString();
                }
                else
                {
                    TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Nashi;
                }
            }

			// 現場実績レコードの情報を取得する
			for(int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
			{ // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_GENBAJISSEKI)
				{ // 現場実績レコードでない場合
					continue;
				}

				// 業者CD：現場実績レコードの業者CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString())))
				{
					GYOUSHA_CD[0] = HensyuFlg_Ari;
					GYOUSHA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString();
				}
				else
				{
					GYOUSHA_CD[0] = HensyuFlg_Nashi;
				}

				// 現場CD：現場実績レコードの現場CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString())))
				{
					GENBA_CD[0] = HensyuFlg_Ari;
					GENBA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString();
				}
				else
				{
					GENBA_CD[0] = HensyuFlg_Nashi;
				}

				// 収集時刻：現場実績レコードの収集時刻を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUUSHUUTIME"].ToString())))
				{
					SHUUSHUU_TIME[0] = HensyuFlg_Ari;
					SHUUSHUU_TIME[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUUSHUUTIME"].ToString();
				}
				else
				{
					SHUUSHUU_TIME[0] = HensyuFlg_Nashi;
				}

                // 明細リストに登録があり、業者CD・現場CDが存在した場合、回数を算出
                Int32 roundNo = 1;
                if(JissekiDetailEntityList.Count > 0)
				{
                    if((GYOUSHA_CD[0] == HensyuFlg_Ari) && (GENBA_CD[0] == HensyuFlg_Ari))
                    {
                        // 回数取得
                        roundNo = this.getRoundNo(GYOUSHA_CD[1], GENBA_CD[1], JissekiDetailEntityList);
                    }
                }

                ROUND_NO[0] = HensyuFlg_Ari;
                ROUND_NO[1] = roundNo.ToString();

                // 現場実績レコードのレコード番号に紐付く現場明細レコードの各項目を編集する
				// 現場明細レコードの情報を取得する
				for(int iGM = 0; iGM < selectedRowsEdaban.Length; iGM++)
				{ // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
					if(Int64.Parse(selectedRowsEdaban[iGM]["NODE_EDABAN"].ToString()) != NODE_EDABAN_DETAIL)
					{ // 現場明細レコードでない場合
						continue;
					}

					if(int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_NO"].ToString()) != int.Parse(selectedRowsEdaban[iGM]["GENBA_DETAIL_GENBA_NO"].ToString()))
					{ // 現場実績レコードのレコード番号と現場明細レコードの現場Noが紐付かない場合(現場実績：現場明細＝１：Ｎで紐付く)
						continue;
					}

                    // 品名CD：現場明細レコードの品名CDを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_HINMEICD"].ToString())))
					{
						HINMEI_CD[0] = HensyuFlg_Ari;
						HINMEI_CD[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_HINMEICD"].ToString();
					}
					else
					{
						HINMEI_CD[0] = HensyuFlg_Nashi;
					}

					// 数量：現場明細レコードの数量１を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO1"].ToString())))
					{
						SUURYOU[0] = HensyuFlg_Ari;
						SUURYOU[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO1"].ToString();
					}
					else
					{
						SUURYOU[0] = HensyuFlg_Nashi;
					}

					// 単位CD：現場明細レコードの単位１を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD1"].ToString())))
					{
						UNIT_CD[0] = HensyuFlg_Ari;
						UNIT_CD[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD1"].ToString();
					}
					else
					{
						UNIT_CD[0] = HensyuFlg_Nashi;
					}

					// 換算後数量：現場明細レコードの数量２を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO2"].ToString())))
					{
						KANSAN_SUURYOU[0] = HensyuFlg_Ari;
						KANSAN_SUURYOU[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO2"].ToString();
					}
					else
					{
						KANSAN_SUURYOU[0] = HensyuFlg_Nashi;
					}

					// 換算後単位CD：現場明細レコードの単位２を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD2"].ToString())))
					{
						KANSAN_UNIT_CD[0] = HensyuFlg_Ari;
						KANSAN_UNIT_CD[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD2"].ToString();
					}
					else
					{
						KANSAN_UNIT_CD[0] = HensyuFlg_Nashi;
					}

					// 荷卸No：現場明細レコードの搬入番号を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_HANNYUUNO"].ToString())))
					{
						NIOROSHI_NUMBER[0] = HensyuFlg_Ari;
						NIOROSHI_NUMBER[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_HANNYUUNO"].ToString();
					}
					else
					{
						NIOROSHI_NUMBER[0] = HensyuFlg_Nashi;
					}

                    // 行番号：現場明細レコードの行番号を編集する
                    if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString())))
                    {
                        ROW_NO[0] = HensyuFlg_Ari;
                        ROW_NO[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString();
                    }
                    else
                    {
                        ROW_NO[0] = HensyuFlg_Nashi;
                    }

                    // 定期実績明細テーブルエンティティの初期化
					T_TEIKI_JISSEKI_DETAIL Entity = new T_TEIKI_JISSEKI_DETAIL();

					// 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
					// システムID
					Entity.SYSTEM_ID = this.int64SystemId;

					// 枝番
					Entity.SEQ = 1;

					// 明細システムID
					Entity.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

					// 定期実績番号
					Entity.TEIKI_JISSEKI_NUMBER = this.int64TeikiJissekiNumber;

                    // 回数
                    if(ROUND_NO[0] == HensyuFlg_Ari)
                    { // 編集ありの場合
                        Entity.ROUND_NO = int.Parse(ROUND_NO[1]);
                    }
                    
                    // 業者CD
					if(GYOUSHA_CD[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.GYOUSHA_CD = GYOUSHA_CD[1];
					}

					// 現場CD
					if(GENBA_CD[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.GENBA_CD = GENBA_CD[1];
					}

					// 収集時刻
					if(SHUUSHUU_TIME[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.SHUUSHUU_TIME = DateTime.Parse(SHUUSHUU_TIME[1]);
					}

					// 品名CD
					if(HINMEI_CD[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.HINMEI_CD = HINMEI_CD[1];
					}

					// 数量
					if(SUURYOU[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
                        Entity.SUURYOU = decimal.Parse(SUURYOU[1]);
					}

					// 単位CD
					if(UNIT_CD[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.UNIT_CD = Int16.Parse(UNIT_CD[1]);
					}

					// 換算後数量
					if(KANSAN_SUURYOU[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
                        Entity.KANSAN_SUURYOU = decimal.Parse(KANSAN_SUURYOU[1]);
					}

					// 換算後単位CD
					if(KANSAN_UNIT_CD[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.KANSAN_UNIT_CD = Int16.Parse(KANSAN_UNIT_CD[1]);
					}

					// 荷卸No
					if(NIOROSHI_NUMBER[0] == HensyuFlg_Ari)
					{ // 編集ありの場合
						Entity.NIOROSHI_NUMBER = int.Parse(NIOROSHI_NUMBER[1]);
					}

					// 伝票区分、契約区分、月極区分の設定

                    // マスタの検索条件設定（業者CD、現場CD、品名CD、単位CD、行番号）
					MobileShougunTorikomiDTOClass dto = new MobileShougunTorikomiDTOClass();
					dto.UNIT_CD = Entity.UNIT_CD;
                    dto.ROUND_NO = Entity.ROUND_NO;
                    dto.GYOUSHA_CD = Entity.GYOUSHA_CD;
                    dto.HINMEI_CD = Entity.HINMEI_CD;
					dto.GENBA_CD = Entity.GENBA_CD;
                    if(ROW_NO[0] == HensyuFlg_Ari)
                    {
                        dto.ROW_NO = Int16.Parse(ROW_NO[1]);
                    }
                    else
                    {
                        dto.ROW_NO = 0;
                    }
                    if(TEIKI_HAISHA_NUMBER[0] == HensyuFlg_Ari)
                    {
                        dto.HAISHA_DENPYOU_NO = int.Parse(TEIKI_HAISHA_NUMBER[1]);
                    }
                    else
                    {
                        dto.HAISHA_DENPYOU_NO = 0;
                    }

                    // 品名情報取得
                    var hinmeiInfo = this.accessor.getTeikiHinmeiInfo(dto);
                    Entity.DENPYOU_KBN_CD = hinmeiInfo.DENPYOU_KBN_CD;
                    Entity.KEIYAKU_KBN = hinmeiInfo.KEIYAKU_KBN;
                    Entity.TSUKIGIME_KBN = hinmeiInfo.KEIJYOU_KBN;
                    Entity.ANBUN_FLG = hinmeiInfo.ANBUN_FLG;

                    // 換算後単位CD、換算数量、換算後単位モバイル出力フラグ
                    var kansanData = this.GetKansanData(Entity, TEIKI_HAISHA_NUMBER[1]);
                    if (kansanData != null)
                    {
                        if (false == kansanData.KANSAN_UNIT_CD.IsNull && true == Entity.KANSAN_UNIT_CD.IsNull)
                        {
                            Entity.KANSAN_UNIT_CD = kansanData.KANSAN_UNIT_CD;
                        }
                        if (false == kansanData.KANSAN_SUURYOU.IsNull && true == Entity.KANSAN_SUURYOU.IsNull)
                        {
                            Entity.KANSAN_SUURYOU = kansanData.KANSAN_SUURYOU;
                        }
                        Entity.KANSAN_UNIT_MOBILE_OUTPUT_FLG = kansanData.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
                    }

					// 定期実績明細テーブル登録
                    JissekiDetailEntityList.Add(Entity);
				}
			}

            Int64 haishaNum = 0;
			// 定期配車番号
            if (TEIKI_HAISHA_NUMBER[0] == HensyuFlg_Ari)
            { // 編集ありの場合
                haishaNum = Int64.Parse(TEIKI_HAISHA_NUMBER[1]);
            }

            // 行番号の付番処理
            Int16 rowNo = 1;
            foreach(var entity in JissekiDetailEntityList)
            {
                entity.ROW_NUMBER = rowNo;
                rowNo++;
            }

            foreach (var en in JissekiDetailEntityList)
            {
                setTeikiJissekiDetailDao.Insert(en);
            }
		}

        /// <summary>
        /// 指定されたカラム名からデータ取得
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="columnName">カラム名</param>
        /// <returns></returns>
        string getColumnValue(DataRow row, string columnName)
        {
            string result = string.Empty;

            if (row[columnName] == null || string.IsNullOrEmpty(row[columnName].ToString()))
            {
                return result;
            }

            result = row[columnName].ToString();

            return result;
        }

		/// <summary>定期実績荷卸テーブル登録</summary>
		void setTeikiJissekiNioroshi(DataRow[] selectedRowsEdaban)
		{
			// 編集領域
			// 編集フラグと編集項目を含んだ配列を作成する
			// [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
			// [1]＝編集有無フラグが編集ありの場合に編集される項目値
			// （エンティティを初期化することによる、int項目のnull値許容対応）
			string[] NIOROSHI_NUMBER = new string[2];
			string[] NIOROSHI_GYOUSHA_CD = new string[2];
			string[] NIOROSHI_GENBA_CD = new string[2];
			string[] NIOROSHI_RYOU = new string[2];
			string[] HANNYUU_DATE = new string[2];

			// 編集ありフラグ
			string HensyuFlg_Ari = "1";
			string HensyuFlg_Nashi = "0";

			// 各項目の編集
			// 搬入実績レコードの情報を取得する
			for(int iHJ = 0; iHJ < selectedRowsEdaban.Length; iHJ++)
			{ // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[iHJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_HANNYUUJISSEKI)
				{ // 搬入実績レコードでない場合
					continue;
				}

				// 荷卸No：搬入実績レコードの搬入番号を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_NO"].ToString())))
				{
					NIOROSHI_NUMBER[0] = HensyuFlg_Ari;
					NIOROSHI_NUMBER[1] = selectedRowsEdaban[iHJ]["HANNYUU_NO"].ToString();
				}
				else
				{
					NIOROSHI_NUMBER[0] = HensyuFlg_Nashi;
				}

				// 荷卸業者CD：搬入実績レコードの業者CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_GYOUSHACD"].ToString())))
				{
					NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Ari;
					NIOROSHI_GYOUSHA_CD[1] = selectedRowsEdaban[iHJ]["HANNYUU_GYOUSHACD"].ToString();
				}
				else
				{
					NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Nashi;
				}

				// 荷卸現場CD：搬入実績レコードの現場CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_GENBACD"].ToString())))
				{
					NIOROSHI_GENBA_CD[0] = HensyuFlg_Ari;
					NIOROSHI_GENBA_CD[1] = selectedRowsEdaban[iHJ]["HANNYUU_GENBACD"].ToString();
				}
				else
				{
					NIOROSHI_GENBA_CD[0] = HensyuFlg_Nashi;
				}

				// 荷卸量：搬入実績レコードの搬入量を編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_RYO"].ToString())))
				{
					NIOROSHI_RYOU[0] = HensyuFlg_Ari;
					NIOROSHI_RYOU[1] = selectedRowsEdaban[iHJ]["HANNYUU_RYO"].ToString();
				}
				else
				{
					NIOROSHI_RYOU[0] = HensyuFlg_Nashi;
				}

				// 搬入時間
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_HANNYUUDATE"].ToString())))
				{
					HANNYUU_DATE[0] = HensyuFlg_Ari;
					HANNYUU_DATE[1] = selectedRowsEdaban[iHJ]["HANNYUU_HANNYUUDATE"].ToString();
				}
				else
				{
					HANNYUU_DATE[0] = HensyuFlg_Nashi;
				}

				// 定期実績荷卸テーブルエンティティの初期化
				T_TEIKI_JISSEKI_NIOROSHI Entity = new T_TEIKI_JISSEKI_NIOROSHI();

				// 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
				// システムID
				//Entity.SYSTEM_ID = this.accessor.createSystemIdForTj();
				Entity.SYSTEM_ID = this.int64SystemId;

				// 枝番
				Entity.SEQ = 1;

				// 荷卸No
				if(NIOROSHI_NUMBER[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.NIOROSHI_NUMBER = int.Parse(NIOROSHI_NUMBER[1]);
				}

				// 定期実績番号
				//Entity.TEIKI_JISSEKI_NUMBER = this.accessor.createTjNumber() ;
				Entity.TEIKI_JISSEKI_NUMBER = this.int64TeikiJissekiNumber;

				// 行番号
				// TODO ダミー登録なので、考慮の必要あり
				Entity.ROW_NUMBER = 1;

				// 荷卸業者CD
				if(NIOROSHI_GYOUSHA_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.NIOROSHI_GYOUSHA_CD = NIOROSHI_GYOUSHA_CD[1];
				}

				// 荷卸現場CD
				if(NIOROSHI_GENBA_CD[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.NIOROSHI_GENBA_CD = NIOROSHI_GENBA_CD[1];
				}

				// 荷卸量
				if(NIOROSHI_RYOU[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
                    Entity.NIOROSHI_RYOU = decimal.Parse(NIOROSHI_RYOU[1]);
				}

				// 搬入時間
				if(HANNYUU_DATE[0] == HensyuFlg_Ari)
				{ // 編集ありの場合
					Entity.HANNYUU_DATE = DateTime.Parse(HANNYUU_DATE[1]);
				}

				// 定期実績荷卸テーブル登録
				setTeikiJissekiNioroshiDao.Insert(Entity);
			}
		}

		/// <summary>コンテナマスタ更新</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void setContena(DataRow[] selectedRowsEdaban)
		{
			// 編集領域
			// 編集フラグと編集項目を含んだ配列を作成する
			// [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
			// [1]＝編集有無フラグが編集ありの場合に編集される項目値
			// （エンティティを初期化することによる、int項目のnull値許容対応）
			string[] GENBA_CD = new string[2];
			string[] CONTENA_SHURUI_CD = new string[2];
			string[] CONTENA_CD = new string[2];
			string[] KYOTEN_CD = new string[2];
			string[] SECCHI_DATE = new string[2];
			string[] JOUKYOU_KBN = new string[2];

			// 編集ありフラグ
			string HensyuFlg_Ari = "1";
			string HensyuFlg_Nashi = "0";

			// 現場実績レコードの情報を取得する
			for(int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
			{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

				if(Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) != NODE_EDABAN_GENBAJISSEKI)
				{   // 現場実績レコードでない場合
					continue;
				}

				// 現場CD：現場実績レコードの現場CDを編集する
				if(!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString())))
				{
					GENBA_CD[0] = HensyuFlg_Ari;
					GENBA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString();
				}
				else
				{
					GENBA_CD[0] = HensyuFlg_Nashi;
				}

				// 現場実績レコードのレコード番号に紐付くコンテナ明細レコードの各項目を編集する
				// コンテナ明細レコードの情報を取得する
				for(int iCM = 0; iCM < selectedRowsEdaban.Length; iCM++)
				{   // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象

					if(Int64.Parse(selectedRowsEdaban[iCM]["NODE_EDABAN"].ToString()) != NODE_EDABAN_CONTENA)
					{   // コンテナ明細レコードでない場合
						continue;
					}

					if(int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_NO"].ToString()) != int.Parse(selectedRowsEdaban[iCM]["CONTENA_NO"].ToString()))
					{   // 現場実績レコードのレコード番号とコンテナ明細レコードのレコード番号が紐付かない場合(現場実績：コンテナ明細＝１：Ｎで紐付く)
						continue;
					}

					// コンテナ種類CD：コンテナ明細レコードのコンテナ種類CDを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iCM]["CONTENA_SHURUI_CD"].ToString())))
					{
						CONTENA_SHURUI_CD[0] = HensyuFlg_Ari;
						CONTENA_SHURUI_CD[1] = selectedRowsEdaban[iCM]["CONTENA_SHURUI_CD"].ToString();
					}
					else
					{
						CONTENA_SHURUI_CD[0] = HensyuFlg_Nashi;
					}

					// コンテナCD：コンテナ明細レコードのコンテナCDを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iCM]["CONTENA_CD"].ToString())))
					{
						CONTENA_CD[0] = HensyuFlg_Ari;
						CONTENA_CD[1] = selectedRowsEdaban[iCM]["CONTENA_CD"].ToString();
					}
					else
					{
						CONTENA_CD[0] = HensyuFlg_Nashi;
					}

					// 拠点CD：コンテナ明細レコードの拠点CDを編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iCM]["CONTENA_KYOTEN_CD"].ToString())))
					{
						KYOTEN_CD[0] = HensyuFlg_Ari;
						KYOTEN_CD[1] = selectedRowsEdaban[iCM]["CONTENA_KYOTEN_CD"].ToString();
					}
					else
					{
						KYOTEN_CD[0] = HensyuFlg_Nashi;
					}

					// 設置日：コンテナ明細レコードの設置日を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iCM]["CONTENA_IDOUDATE"].ToString())))
					{
						SECCHI_DATE[0] = HensyuFlg_Ari;
						SECCHI_DATE[1] = selectedRowsEdaban[iCM]["CONTENA_IDOUDATE"].ToString();
					}
					else
					{
						SECCHI_DATE[0] = HensyuFlg_Nashi;
					}

					// 状況区分：コンテナ明細レコードの状況区分を編集する
					if(!(string.IsNullOrEmpty(selectedRowsEdaban[iCM]["CONTENA_IDOU_KBN"].ToString())))
					{
						JOUKYOU_KBN[0] = HensyuFlg_Ari;
						JOUKYOU_KBN[1] = selectedRowsEdaban[iCM]["CONTENA_IDOU_KBN"].ToString();
					}
					else
					{
						JOUKYOU_KBN[0] = HensyuFlg_Nashi;
					}

					//コンテナマスタエンティティの初期化
					M_CONTENA Entity = new M_CONTENA();

					// 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
					// コンテナ種類CD ※UPDATE時のKEY項目のため、値は必須項目
					Entity.CONTENA_SHURUI_CD = CONTENA_SHURUI_CD[1];

					// コンテナCD ※UPDATE時のKEY項目のため、値は必須項目
					Entity.CONTENA_CD = CONTENA_CD[1];

					// 現場CD
					if(GENBA_CD[0] == HensyuFlg_Ari)
					{   // 編集ありの場合
						Entity.GENBA_CD = GENBA_CD[1];
					}

					// 拠点CD
					if(KYOTEN_CD[0] == HensyuFlg_Ari)
					{   // 編集ありの場合
						Entity.KYOTEN_CD = Int16.Parse(KYOTEN_CD[1]);
					}

					// 設置日
					if(SECCHI_DATE[0] == HensyuFlg_Ari)
					{   // 編集ありの場合
						Entity.SECCHI_DATE = DateTime.Parse(SECCHI_DATE[1]);
					}

					// 状況区分
					if(JOUKYOU_KBN[0] == HensyuFlg_Ari)
					{   // 編集ありの場合
						Entity.JOUKYOU_KBN = Int16.Parse(JOUKYOU_KBN[1]);
					}

					// コンテナマスタ更新(実行)
					var dataBinderContenaResult = new DataBinderLogic<M_CONTENA>(Entity);
					dataBinderContenaResult.SetSystemProperty(Entity, false);
					setContenaSql(Entity);
				}
			}
		}

		// DBへ複数アクセスしないよう変数へ社員データを格納
		private DataTable allShainData = null;

		/// <summary>Ｆ１ ﾃﾞｰﾀ取込ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_func1_Click(object sender, EventArgs e)
		{
			// XMLファイルが格納されているパス
			string xmlPath = this.GetInPutPath("mobileInPutPath");

			string[] fullPathNames = Directory.GetFiles(xmlPath, "*.xml");

			if(fullPathNames.Length <= 0)
			{   // ファイルが存在しない
				return;
			}

            //20151021 hoanghm #13495 start
            //Check all xml file. If one of them is open then return.
            if (this.CheckFileIsOpen(fullPathNames))
            {
                return;
            }
            //20151021 hoanghm #13495 end

            // 取込ファイル削除用
            this.mobileInPutFilePathToMove = new String[fullPathNames.Length];

            this.allShainData = null;                       // 初期化
            this.allShainData = this.dao.GetAllShainData();

            // 取込データの登録
            CreateMobileSyogunData(fullPathNames);

			// 取込データのバックアップ（１世代のみ保存）:ファイルの移動
			XmlBackup();

			// 取込完了メッセージの表示
			MessageBox.Show(MobileShougunTorikomiConst.Msg1);

			// 画面の再表示
			ReWindow();
		}

		/// <summary>Ｆ２ 切替ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_func2_Click(object sender, EventArgs e)
		{
			if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
			{ // 切替前が定期データ表示時の場合　→　スポットデータの表示

				// 一覧の表示
				this.SetIchiran("SPOT");

				// 表示・非表示判定
				setVisible("SPOT");

				// 未登録数の表示
				this.SetMitourokusu("SPOT");

				// タイトル表示の切り替え
                var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
				titleControl.Text = MobileShougunTorikomiConst.Title2;
			}
			else
			{
				if(contenaFlg)
				{
                    // 切替前がコンテナデータ表示時の場合　→　定期データの表示
                    // 一覧の表示
                    this.SetIchiran("TEIKI");

                    // 表示・非表示判定
                    setVisible("TEIKI");

                    // 未登録数の表示
                    this.SetMitourokusu("TEIKI");

                    // タイトル表示の切り替え
                    var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
                    titleControl.Text = MobileShougunTorikomiConst.Title1;
                }
				else
				{   // コンテナ無

					// 一覧の表示
					this.SetIchiran("TEIKI");

					// 表示・非表示判定
					setVisible("TEIKI");

					// 未登録数の表示
					this.SetMitourokusu("TEIKI");

					// タイトル表示の切り替え
                    var titleControl = (Label)controlUtil.FindControl(this.parentForm, "lb_title");
					titleControl.Text = MobileShougunTorikomiConst.Title1;
				}

			}
		}

		/// <summary>Ｆ７ 検索ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_func7_Click(object sender, EventArgs e)
		{
			// 一覧の表示
			this.SetIchiran("SEARCH");
		}

		/// <summary>Ｆ９ 登録ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_func9_Click(object sender, EventArgs e)
		{
            /* 月次ロックチェック */
            /* スポットの場合、売上/支払伝票が作成されるため既に月次処理済かチェック */
            if (!string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text))
            {
                // 作業日が伝票日付として登録されるので作業日が月次処理中、月次処理済年月内にいるかチェック
                GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
                for (int i = 0; i < selectedRowsIchiran.Length; i++)
                {
                    if ((bool)this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value)
                    {
                        DateTime checkDate = DateTime.Parse(this.form.Ichiran.Rows[i].Cells["SAGYOU_DATE"].Value.ToString());
                        short year = short.Parse(checkDate.Year.ToString());
                        short month = short.Parse(checkDate.Month.ToString());

                        // 月次処理中
                        if (checkLogic.CheckGetsujiShoriChu(checkDate))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E224", "登録");
                            return;
                        }
                        else if (checkLogic.CheckGetsujiShoriLock(year, month))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E223", "登録");
                            return;
                        }
                    }
                }
            }

            Boolean chkFlg = RegistMobileShougunData();

            // 20141119 koukouei 締済期間チェックの追加 start
            if (!this.registFlg)
            {
                return;
            }
            // 20141119 koukouei 締済期間チェックの追加 end

			if(chkFlg)
			{ // 削除処理or登録が実行された場合
				// 登録完了メッセージの表示
				MessageBox.Show(MobileShougunTorikomiConst.Msg2);

				// 画面の再表示
				ReWindow();
			}
			else
			{
				// 登録NGメッセージの表示
				MessageBox.Show(MobileShougunTorikomiConst.Msg3);
			}
		}

		/// <summary>Ｆ12 閉じるボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_func12_Click(object sender, EventArgs e)
		{
			// コンテナフォームの全てを削除処理
			this.ContenaFormRemoveAll();

            this.parentForm.Close();
		}

		/// <summary>モバイル将軍用データ取込画面専用テーブル削除処理</summary>
		void deleteMobileShougunData(Int64 EDABAN)
		{
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
			entity.EDABAN = EDABAN;

			this.dao.GetDeleteMobileShougunDataForEntity(entity);
		}

		/// <summary>各テーブル登録</summary>
		void insertJissekiData(Int64 EDABAN)
		{
			// DAO編集
			this.setTeikiJissekiEntryDao = DaoInitUtility.GetComponent<SetTeikiJissekiEntryDao>();
			this.setTeikiJissekiDetailDao = DaoInitUtility.GetComponent<SetTeikiJissekiDetailDao>();
			this.setTeikiJissekiNioroshiDao = DaoInitUtility.GetComponent<SetTeikiJissekiNioroshiDao>();
			this.setUrShEntryDao = DaoInitUtility.GetComponent<SetUrShEntryDao>();
			this.setUrShDetailDao = DaoInitUtility.GetComponent<SetUrShDetailDao>();

			// 初期化
			this.int64SystemId = 0;
			this.int64TeikiJissekiNumber = 0;
			this.HaishaDenNo = -1;

			// 対象枝番での絞り込み
			DataRow[] selectedRowsEdaban;
			selectedRowsEdaban = this.dataResult.Select("EDABAN = " + EDABAN);

			if(!(string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text)))
			{   // 定期データ表示時

                // 定期実績入力テーブル登録
                setTeikiJissekiEntry(selectedRowsEdaban);

                // 定期実績明細テーブル登録
                setTeikiJissekiDetail(selectedRowsEdaban);

                // 定期実績荷卸テーブル登録   
                setTeikiJissekiNioroshi(selectedRowsEdaban);
			}
			else if(!(string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text)))
			{   // スポットデータ表示時

				// 売上_支払入力テーブル登録
				this.SetUrShData(selectedRowsEdaban);
			}
			else
			{   // コンテナデータ表示時

				// コンテナマスタ更新   
				setContena(selectedRowsEdaban);
			}
		}

		/// <summary>[1] 全て選択(登録)ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_process1_Click(object sender, EventArgs e)
		{
            bool isError = false;

            for (int i = 0; i < selectedRowsIchiran.Length; i++)
            {
                this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value = false;
                //this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = true;

                int cnt = 0;
                if (this.form.Ichiran.Rows[i].Cells["HAISHA_DENPYOU_NO"].Value != null)
                {
                    var uketsukeDenpyou = this.form.Ichiran.Rows[i].Cells["HAISHA_DENPYOU_NO"].Value.ToString();

                    DataTable dt = setUrShEntryDao.GetUriageShiharaiEntryData(Int64.Parse(uketsukeDenpyou));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cnt = dt.Rows.Count;
                    }
                    else
                    {

                        foreach (DataGridViewRow dr in this.form.Ichiran.Rows)
                        {
                            if (dr.Index == i)
                            {
                                continue;
                            }

                            if (dr.Cells["HAISHA_DENPYOU_NO"].Value != null
                                && dr.Cells["HAISHA_DENPYOU_NO"].Value.ToString().Equals(uketsukeDenpyou))
                            {
                                cnt++;
                            }
                        }
                    }
                }

                if (cnt == 0)
                {
                    this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = true;
                }
                else
                {
                    isError = true;
                }
            }

            if (isError)
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError("取り込み可能なデータにのみチェックをつけます。\n既に取り込み済みのデータにはチェックは付きません。");
            }
            this.form.Ichiran.Refresh();
		}

		/// <summary>[2] 全て解除(登録)ボタンイベント</summary>
		/// <param name="sender">イベント呼び出し元オブジェクト</param>
		/// <param name="e">e</param>
		void bt_process2_Click(object sender, EventArgs e)
		{
			for(int i = 0; i < selectedRowsIchiran.Length; i++)
			{
				this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value = true;
				this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = false;
			}
            this.form.Ichiran.Refresh();
		}

		/// <summary>取り込み済みデータ取得処理</summary>
		/// <param name="sender">配車区分/param>
		/// <param name="e">e</param>
		void getTorikomizumiData(string haishaKbun)
		{
			// モバイル将軍用データ取込画面専用テーブル取得
			MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();

			switch(haishaKbun)
			{
				case "YUUKOU":
					{ // 有効データ(DELETE_FLG=false)の場合
						this.dataResult = this.dao.GetYuukouDataForEntity(entity);

						// 件数も取得
						this.YuukouData_count = this.dataResult.Rows.Count;
					}
					break;

				case "MAX_SEQ_NO":
					{ // シーケンシャルナンバーのMAX値を取得する場合
						this.dataResult = this.dao.GetMaxSeqForEntity(entity);

						DataRow[] selectedRows = dataResult.Select();
						foreach(DataRow row in selectedRows)
						{
							if(row["MAX_SEQ_NO"] == DBNull.Value)
							{
								this.Max_Seq_No = 0;
							}
							else
							{
								this.Max_Seq_No = (Int64)row["MAX_SEQ_NO"];
							}
						}
					}
					break;

				case "MAX_EDABAN":
					{ // 枝番のMAX値を取得する場合
						this.dataResult = this.dao.GetMaxEdabanForEntity(entity);

						DataRow[] selectedRows = dataResult.Select();
						foreach(DataRow row in selectedRows)
						{
							if(row["MAX_EDABAN"] == DBNull.Value)
							{
								this.Max_Edaban = 0;
							}
							else
							{
								this.Max_Edaban = (Int64)row["MAX_EDABAN"];
							}
						}
					}
					break;
			}
		}

		/// <summary>未登録数の表示</summary>
		void SetMitourokusu(string haishaKbun)
		{
			DataRow[] selectedRows;

			// 取得したモバイル将軍用データ取込画面専用テーブルをセット
			var table = this.dataResult;
			table.BeginLoadData();

			switch(haishaKbun)
			{
				case "TEIKI":   // 定期の場合
					selectedRows = table.Select("HAISHA_KBN = 0");
					this.teikiData_count = selectedRows.Length;
					this.form.ntxt_TeikiMitouroku.Text = this.teikiData_count.ToString();
					this.form.ntxt_TeikiMitouroku.ForeColor = System.Drawing.Color.Red;
					this.form.ntxt_SpotMitouroku.Text = string.Empty;
					this.form.ntxt_ContenaMitouroku.Text = string.Empty;

					break;

				case "SPOT":    // スポットの場合
					selectedRows = table.Select("HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = false");
					this.spotData_count = selectedRows.Length;
					this.form.ntxt_TeikiMitouroku.Text = string.Empty;
					this.form.ntxt_SpotMitouroku.Text = this.spotData_count.ToString();
					this.form.ntxt_SpotMitouroku.ForeColor = System.Drawing.Color.Red;
					this.form.ntxt_ContenaMitouroku.Text = string.Empty;

					break;

				case "CONTENA": // コンテナの場合
					selectedRows = table.Select("HAISHA_KBN = 1 AND HAISHA_CONTENA_FLG = true");
					this.contenaData_count = selectedRows.Length;
					this.form.ntxt_TeikiMitouroku.Text = string.Empty;
					this.form.ntxt_SpotMitouroku.Text = string.Empty;
					this.form.ntxt_ContenaMitouroku.Text = this.contenaData_count.ToString();
					this.form.ntxt_ContenaMitouroku.ForeColor = System.Drawing.Color.Red;

					break;
			}
		}

        /// <summary>換算値を取得する</summary>
        /// <param name="data">登録しようとしている実績明細情報</param>
        /// <param name="teikiHaishaNumber">定期配車番号</param>
        /// <returns name="DataTable">条件にヒットした換算値&換算単位を格納したEntity</returns>
        /// <remarks>該当するものが無い場合はnullを返却</remarks>
        private T_TEIKI_JISSEKI_DETAIL GetKansanData(T_TEIKI_JISSEKI_DETAIL data, string teikiHaishaNumber)
        {
            // 単位CD:[Kg]
            const string unitCdKg = "3";
            var returnData = new T_TEIKI_JISSEKI_DETAIL();

            // 要記入フラグ
            if (data.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = SqlBoolean.False;
            }
            else
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = data.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
            }

            // 取込データに換算後単位があればそれを使用（ここでは処理する必要なし）
            // 回収品名詳細に換算後単位があればそれを使用
            // 現場定期品名に換算後単位があればそれを使用

            // ① 他の単位をKgへ換算の場合
            // ② Kgを他の単位へ換算の場合
            // ③ Kg→Kgへの単位換算の場合
            //《公式》:[換算値] × [数量] = [換算後数量]
            // 数量が未入力の場合は換算後数量の計算を行わない

            // 現場定期品名から取得する
            var genbaTeikiEntity = this.dao.GetGenbaTeikiHinmeiDataForEntity(new MobileShougunTorikomiDTOClass()
            {
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (genbaTeikiEntity.Rows.Count > 0)
            {
                var kansanUnitCd = genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            // 回収品名詳細から取得する
            var dtKansanData = this.dao.GetKansanData(new MobileShougunTorikomiDTOClass()
            {
                HAISHA_DENPYOU_NO = Int32.Parse(teikiHaishaNumber),
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (dtKansanData.Rows.Count > 0)
            {
                var kansanUnitCd = dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            if (false == data.KANSAN_UNIT_CD.IsNull)
            {
                returnData.KANSAN_UNIT_CD = data.KANSAN_UNIT_CD;
            }

            return returnData;
        }

		/// <summary>売上_支払入力テーブル登録</summary>
		/// <param name="selectedRowsEdaban">実績データ</param>
		private void SetUrShData(DataRow[] selectedRowsEdaban)
		{
			// 売上支払伝票
			var urshEntryEntityList = new List<T_UR_SH_ENTRY>();
			var urshEntryEntity = new T_UR_SH_ENTRY();
			var headerEntity = new T_UR_SH_ENTRY();

			// 売上支払明細
			var urshDetailEntityList = new List<T_UR_SH_DETAIL>();
			var urshDetailEntity = new T_UR_SH_DETAIL();

			// 取引先請求支払情報
			M_TORIHIKISAKI_SEIKYUU torihikiSeikyuInfo = null;
			M_TORIHIKISAKI_SHIHARAI torihikiShiharaiInfo = null;

			// 受付収集伝票
			T_UKETSUKE_SS_ENTRY uketsukeSsEntry = null;

			// 検索用情報
			UketsukeSsDTOClass findDTO = new UketsukeSsDTOClass();

			// 業者Entity
			M_GYOUSHA gyoushaEntity = null;

			// 現場Entity
			M_GENBA genbaEntity = null;

			#region - 配車ヘッダ -
			// 配車ヘッダレコードの情報を取得する
			foreach(DataRow headerRow in selectedRowsEdaban)
			{
				if(Int64.Parse(headerRow["NODE_EDABAN"].ToString()) == NODE_EDABAN_HAISHA)
				{
					// 拠点CD
					if(false == string.IsNullOrEmpty(headerRow["KYOTEN_CD"].ToString()))
					{
						headerEntity.KYOTEN_CD = Int16.Parse(headerRow["KYOTEN_CD"].ToString());
					}

					// 伝票日付, 売上日付
					if(false == string.IsNullOrEmpty(headerRow["HAISHA_SAGYOU_DATE"].ToString()))
					{
						headerEntity.DENPYOU_DATE = DateTime.Parse(headerRow["HAISHA_SAGYOU_DATE"].ToString());
						headerEntity.URIAGE_DATE = headerEntity.DENPYOU_DATE;
						headerEntity.SHIHARAI_DATE = headerEntity.DENPYOU_DATE;
					}

					// 入力担当者CD, 入力担当者名
					headerEntity.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
					headerEntity.NYUURYOKU_TANTOUSHA_NAME = SystemProperty.Shain.Name;

					// 車輌CD
					if(false == string.IsNullOrEmpty(headerRow["SHARYOU_CD"].ToString()))
					{
						headerEntity.SHARYOU_CD = headerRow["SHARYOU_CD"].ToString();
					}

					// 車輌名
					if(false == string.IsNullOrEmpty(headerRow["SHARYOU_NAME"].ToString()))
					{

						headerEntity.SHARYOU_NAME = headerRow["SHARYOU_NAME"].ToString();
					}

					// 車種CD
					if(false == string.IsNullOrEmpty(headerRow["SHASHU_CD"].ToString()))
					{
						headerEntity.SHASHU_CD = headerRow["SHASHU_CD"].ToString();
					}

					// 車種名
					if(false == string.IsNullOrEmpty(headerRow["SHASHU_NAME"].ToString()))
					{
						headerEntity.SHASHU_NAME = headerRow["SHASHU_NAME"].ToString();
					}

					// 運転者CD
					if(false == string.IsNullOrEmpty(headerRow["HAISHA_UNTENSHA_CD"].ToString()))
					{
						headerEntity.UNTENSHA_CD = headerRow["HAISHA_UNTENSHA_CD"].ToString();
					}

					// 運転者名
					if(false == string.IsNullOrEmpty(headerRow["UNTENSHA_NAME"].ToString()))
					{
						headerEntity.UNTENSHA_NAME = headerRow["UNTENSHA_NAME"].ToString();
					}

					// 受付番号をキーに受付収集伝票から情報を取得
					if(false == string.IsNullOrEmpty(headerRow["HAISHA_DENPYOU_NO"].ToString()))
					{
						// 受付番号
						headerEntity.UKETSUKE_NUMBER = Int64.Parse(headerRow["HAISHA_DENPYOU_NO"].ToString());

						// 受付番号に紐付く受付収集伝票が存在する場合
						uketsukeSsEntry = this.accessor.getUketsukeSsEntry((int)headerEntity.UKETSUKE_NUMBER);
						if(uketsukeSsEntry != null)
						{
							// 取引先CD
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.TORIHIKISAKI_CD))
							{
								headerEntity.TORIHIKISAKI_CD = uketsukeSsEntry.TORIHIKISAKI_CD;

								// 取引先請求情報取得
								torihikiSeikyuInfo = this.torihikiSeikyuDao.GetDataByCd(uketsukeSsEntry.TORIHIKISAKI_CD);
								if(torihikiSeikyuInfo != null)
								{
									// 売上税計算区分CD
									if(false == torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD.IsNull)
									{
										headerEntity.URIAGE_ZEI_KEISAN_KBN_CD = torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD;
									}

									// 売上税区分CD
									if(false == torihikiSeikyuInfo.ZEI_KBN_CD.IsNull)
									{
										headerEntity.URIAGE_ZEI_KBN_CD = torihikiSeikyuInfo.ZEI_KBN_CD;
									}

									// 売上取引区分CD
									if(false == torihikiSeikyuInfo.TORIHIKI_KBN_CD.IsNull)
									{
										headerEntity.URIAGE_TORIHIKI_KBN_CD = torihikiSeikyuInfo.TORIHIKI_KBN_CD;
									}
								}

								// 取引先支払情報取得
								torihikiShiharaiInfo = this.torihikiShiharaiDao.GetDataByCd(uketsukeSsEntry.TORIHIKISAKI_CD);
								if(torihikiShiharaiInfo != null)
								{
									// 支払税計算区分CD
									if(false == torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD.IsNull)
									{
										headerEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD;
									}

									// 支払税区分CD
									if(false == torihikiShiharaiInfo.ZEI_KBN_CD.IsNull)
									{
										headerEntity.SHIHARAI_ZEI_KBN_CD = torihikiShiharaiInfo.ZEI_KBN_CD;
									}

									// 支払取引区分CD
									if(false == torihikiShiharaiInfo.TORIHIKI_KBN_CD.IsNull)
									{
										headerEntity.SHIHARAI_TORIHIKI_KBN_CD = torihikiShiharaiInfo.TORIHIKI_KBN_CD;
									}
								}

								// 取引先名
								if(false == string.IsNullOrEmpty(uketsukeSsEntry.TORIHIKISAKI_NAME))
								{
									headerEntity.TORIHIKISAKI_NAME = uketsukeSsEntry.TORIHIKISAKI_NAME;
								}
                            }

                            #region chenzz 20160126 業者、現場、運搬業者受付から 直接取得
                            // 業者CD
                            if (false == string.IsNullOrEmpty(uketsukeSsEntry.GYOUSHA_CD))
                            {
                                headerEntity.GYOUSHA_CD = uketsukeSsEntry.GYOUSHA_CD;
                            }

                            // 業者名
                            if (false == string.IsNullOrEmpty(uketsukeSsEntry.GYOUSHA_NAME))
                            {
                                headerEntity.GYOUSHA_NAME = uketsukeSsEntry.GYOUSHA_NAME;
                            }

                            // 現場CD
                            if (false == string.IsNullOrEmpty(uketsukeSsEntry.GENBA_CD))
                            {
                                headerEntity.GENBA_CD = uketsukeSsEntry.GENBA_CD;
                            }

                            // 現場名
                            if (false == string.IsNullOrEmpty(uketsukeSsEntry.GENBA_NAME))
                            {
                                headerEntity.GENBA_NAME = uketsukeSsEntry.GENBA_NAME;
                            }
                            #endregion chenzz 20160126 業者、現場、運搬業者受付データから 直接取得

                            // 荷卸業者CD
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.NIOROSHI_GYOUSHA_CD))
							{
								headerEntity.NIOROSHI_GYOUSHA_CD = uketsukeSsEntry.NIOROSHI_GYOUSHA_CD;
							}

							// 荷卸業者名
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.NIOROSHI_GYOUSHA_NAME))
							{
								headerEntity.NIOROSHI_GYOUSHA_NAME = uketsukeSsEntry.NIOROSHI_GYOUSHA_NAME;
							}

							// 荷卸現場CD
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.NIOROSHI_GENBA_CD))
							{
								headerEntity.NIOROSHI_GENBA_CD = uketsukeSsEntry.NIOROSHI_GENBA_CD;
							}

							// 荷卸現場名
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.NIOROSHI_GENBA_NAME))
							{
								headerEntity.NIOROSHI_GENBA_NAME = uketsukeSsEntry.NIOROSHI_GENBA_NAME;
							}

							// 営業担当者CD
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.EIGYOU_TANTOUSHA_CD))
							{
								headerEntity.EIGYOU_TANTOUSHA_CD = uketsukeSsEntry.EIGYOU_TANTOUSHA_CD;
							}

							// 営業担当者名
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.EIGYOU_TANTOUSHA_NAME))
							{
								headerEntity.EIGYOU_TANTOUSHA_NAME = uketsukeSsEntry.EIGYOU_TANTOUSHA_NAME;
							}

							// 運搬業者CD
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.UNPAN_GYOUSHA_CD))
							{
								headerEntity.UNPAN_GYOUSHA_CD = uketsukeSsEntry.UNPAN_GYOUSHA_CD;
							}

							// 運搬業者名
							if(false == string.IsNullOrEmpty(uketsukeSsEntry.UNPAN_GYOUSHA_NAME))
							{
								headerEntity.UNPAN_GYOUSHA_NAME = uketsukeSsEntry.UNPAN_GYOUSHA_NAME;
							}

							// コンテナ操作CD
							headerEntity.CONTENA_SOUSA_CD = uketsukeSsEntry.CONTENA_SOUSA_CD;

							// マニフェスト種類CD
							headerEntity.MANIFEST_SHURUI_CD = uketsukeSsEntry.MANIFEST_SHURUI_CD;

							// マニフェスト手配CD
							headerEntity.MANIFEST_TEHAI_CD = uketsukeSsEntry.MANIFEST_TEHAI_CD;
						}
					}
				}
			}

			#endregion - 配車ヘッダ -

			#region - 搬入実績 -
			// 搬入実績ヘッダレコードの情報を取得する
			foreach(DataRow hannyuuRow in selectedRowsEdaban)
			{
				if(Int64.Parse(hannyuuRow["NODE_EDABAN"].ToString()) == NODE_EDABAN_HANNYUUJISSEKI)
				{
					// 受付番号に紐付く受付収集伝票が存在しなかった場合、搬入実績より、荷卸業者・現場を補完する
					if(uketsukeSsEntry == null)
					{
						// 荷卸業者CD
						if(false == string.IsNullOrEmpty(hannyuuRow["HANNYUU_GYOUSHACD"].ToString()))
						{
							headerEntity.NIOROSHI_GYOUSHA_CD = hannyuuRow["HANNYUU_GYOUSHACD"].ToString();

							// 荷卸業者名
							gyoushaEntity = this.gyoushaDao.GetDataByCd(headerEntity.NIOROSHI_GYOUSHA_CD);
							if(gyoushaEntity != null)
							{
                                // 20160115 chenzz 13337 品名手入力に関する機能修正 start
                                if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                                {
                                    headerEntity.NIOROSHI_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME1;
                                }
                                else
                                {
                                    // 20160115 chenzz 13337 品名手入力に関する機能修正 end
                                    headerEntity.NIOROSHI_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                                }
							}
						}

						// 荷卸現場CD
						if(false == string.IsNullOrEmpty(hannyuuRow["HANNYUU_GENBACD"].ToString()))
						{
							headerEntity.NIOROSHI_GENBA_CD = hannyuuRow["HANNYUU_GENBACD"].ToString();

							// 荷卸現場名
							var findEntity = new M_GENBA();
							findEntity.GYOUSHA_CD = headerEntity.NIOROSHI_GYOUSHA_CD;
							findEntity.GENBA_CD = headerEntity.NIOROSHI_GENBA_CD;
							genbaEntity = this.genbaDao.GetDataByCd(findEntity);
							if(genbaEntity != null)
							{
                                // 20160115 chenzz 13337 品名手入力に関する機能修正 start
                                if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                                {
                                    headerEntity.NIOROSHI_GENBA_NAME = genbaEntity.GENBA_NAME1;
                                }
                                else
                                {
                                    // 20160115 chenzz 13337 品名手入力に関する機能修正 end
                                    headerEntity.NIOROSHI_GENBA_NAME = genbaEntity.GENBA_NAME_RYAKU;
                                }
							}
						}
					}
				}
			}

			#endregion - 搬入実績 -

			#region - 現場実績 -
			// 現場実績レコードの情報を取得する
			foreach(DataRow jissekiRow in selectedRowsEdaban)
			{
				if(Int64.Parse(jissekiRow["NODE_EDABAN"].ToString()) == NODE_EDABAN_GENBAJISSEKI)
				{
					// 初期化
					urshEntryEntity = this.createEntryEntityClone(headerEntity);

					#region - 出庫実績 -
					// 現場実績の出庫Noに紐付く出庫実績のセット
					// 出庫実績レコードの情報を取得する
					foreach(DataRow shukkoRow in selectedRowsEdaban)
					{
						if(Int64.Parse(shukkoRow["NODE_EDABAN"].ToString()) == NODE_EDABAN_SHUKKO)
						{
							if(int.Parse(jissekiRow["GENBA_JISSEKI_SHUKKONO"].ToString()) == int.Parse(shukkoRow["SHUKKO_NO"].ToString()))
							{
								// 車輌CD(出庫実績)
								if(false == string.IsNullOrEmpty(shukkoRow["SHUKKO_SHARYOUCD"].ToString()))
								{
									urshEntryEntity.SHARYOU_CD = shukkoRow["SHUKKO_SHARYOUCD"].ToString();
								}

								// 車輌名(出庫実績)
								if(false == string.IsNullOrEmpty(shukkoRow["SHARYOU_NAME"].ToString()))
								{

									urshEntryEntity.SHARYOU_NAME = shukkoRow["SHARYOU_NAME"].ToString();
								}

								// 車種CD(出庫実績)
								if(false == string.IsNullOrEmpty(shukkoRow["SHASHU_CD"].ToString()))
								{
									urshEntryEntity.SHASHU_CD = shukkoRow["SHASHU_CD"].ToString();
								}

								// 車種名(出庫実績)
								if(false == string.IsNullOrEmpty(shukkoRow["SHASHU_NAME"].ToString()))
								{
									urshEntryEntity.SHASHU_NAME = shukkoRow["SHASHU_NAME"].ToString();
								}

								// 運搬業者CD(出庫実績)
                                if (false == string.IsNullOrEmpty(shukkoRow["SHUKKO_GYOUSHACD"].ToString()) && uketsukeSsEntry == null)
								{
									urshEntryEntity.UNPAN_GYOUSHA_CD = shukkoRow["SHUKKO_GYOUSHACD"].ToString();

									// 運搬業者名
									gyoushaEntity = this.gyoushaDao.GetDataByCd(urshEntryEntity.UNPAN_GYOUSHA_CD);
									if(gyoushaEntity != null)
                                    {
                                        // 20160115 chenzz 13337 品名手入力に関する機能修正 start
                                        if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                                        {
                                            urshEntryEntity.UNPAN_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME1;
                                        }
                                        else
                                        {
                                            // 20160115 chenzz 13337 品名手入力に関する機能修正 end
                                            urshEntryEntity.UNPAN_GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                                        }
									}
								}
							}
						}
					}

					#endregion - 出庫実績 -

					// 業者CD
                    if (false == string.IsNullOrEmpty(jissekiRow["GENBA_JISSEKI_GYOUSHACD"].ToString()) && uketsukeSsEntry == null)
					{
						urshEntryEntity.GYOUSHA_CD = jissekiRow["GENBA_JISSEKI_GYOUSHACD"].ToString();

						// 業者名
						gyoushaEntity = this.gyoushaDao.GetDataByCd(urshEntryEntity.GYOUSHA_CD);
						if(gyoushaEntity != null)
                        {
                            // 20160113 chenzz 13337 品名手入力に関する機能修正 start
                            if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                urshEntryEntity.GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME1;
                            }
                            else
                            {
                                // 20160113 chenzz 13337 品名手入力に関する機能修正 end
                                urshEntryEntity.GYOUSHA_NAME = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                            }
						}
					}

					// 現場CD
					if(false == string.IsNullOrEmpty(jissekiRow["GENBA_JISSEKI_GENBACD"].ToString()) && uketsukeSsEntry == null)
					{
						urshEntryEntity.GENBA_CD = jissekiRow["GENBA_JISSEKI_GENBACD"].ToString();

						// 現場名
						var findEntity = new M_GENBA();
						findEntity.GYOUSHA_CD = urshEntryEntity.GYOUSHA_CD;
						findEntity.GENBA_CD = urshEntryEntity.GENBA_CD;
						genbaEntity = this.genbaDao.GetDataByCd(findEntity);
						if(genbaEntity != null)
						{
                            // 20160113 chenzz #13337 品名手入力に関する機能修正 start
                            if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                            {
                                urshEntryEntity.GENBA_NAME = genbaEntity.GENBA_NAME1;
                            }
                            else
                            {
                                // 20160113 chenzz #13337 品名手入力に関する機能修正 end
                                urshEntryEntity.GENBA_NAME = genbaEntity.GENBA_NAME_RYAKU;
                            }
						}
					}

					// 受付番号から受付収集伝票に紐付く取引先が取得出来ていなかった場合
					if(true == string.IsNullOrEmpty(urshEntryEntity.TORIHIKISAKI_CD))
					{
						if(genbaEntity != null)
						{
							// 現場マスタから取引先CDを取得
							urshEntryEntity.TORIHIKISAKI_CD = genbaEntity.TORIHIKISAKI_CD;
						}
						else
						{
							// 現場マスタが取得出来なかった場合
							if(gyoushaEntity != null)
							{
								// 業者マスタから取引先CDを取得
								urshEntryEntity.TORIHIKISAKI_CD = gyoushaEntity.TORIHIKISAKI_CD;
							}
						}

						if(false == string.IsNullOrEmpty(urshEntryEntity.TORIHIKISAKI_CD))
						{
							// 取引マスタより取引先名取得
							var entity = this.torihikisakiDao.GetDataByCd(urshEntryEntity.TORIHIKISAKI_CD);
							if(entity != null)
							{
                                // 20160113 chenzz #13337 品名手入力に関する機能修正 start
                                if (entity.SHOKUCHI_KBN.IsTrue)
                                {
                                    urshEntryEntity.TORIHIKISAKI_NAME = entity.TORIHIKISAKI_NAME1;
                                }
                                else
                                {
                                    // 20160113 chenzz #13337 品名手入力に関する機能修正 end
                                    // 取引先名
                                    if (false == string.IsNullOrEmpty(entity.TORIHIKISAKI_NAME_RYAKU))
                                    {
                                        urshEntryEntity.TORIHIKISAKI_NAME = entity.TORIHIKISAKI_NAME_RYAKU;
                                    }
                                }
							}

							// 取引先請求情報取得
							torihikiSeikyuInfo = this.torihikiSeikyuDao.GetDataByCd(urshEntryEntity.TORIHIKISAKI_CD);
							if(torihikiSeikyuInfo != null)
							{
								// 売上税計算区分CD
								if(false == torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD.IsNull)
								{
									urshEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD = torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD;
								}

								// 売上税区分CD
								if(false == torihikiSeikyuInfo.ZEI_KBN_CD.IsNull)
								{
									urshEntryEntity.URIAGE_ZEI_KBN_CD = torihikiSeikyuInfo.ZEI_KBN_CD;
								}

								// 売上取引区分CD
								if(false == torihikiSeikyuInfo.TORIHIKI_KBN_CD.IsNull)
								{
									urshEntryEntity.URIAGE_TORIHIKI_KBN_CD = torihikiSeikyuInfo.TORIHIKI_KBN_CD;
								}
							}

							// 取引先支払情報取得
							torihikiShiharaiInfo = this.torihikiShiharaiDao.GetDataByCd(urshEntryEntity.TORIHIKISAKI_CD);
							if(torihikiShiharaiInfo != null)
							{
								// 支払税計算区分CD
								if(false == torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD.IsNull)
								{
									urshEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD;
								}

								// 支払税区分CD
								if(false == torihikiShiharaiInfo.ZEI_KBN_CD.IsNull)
								{
									urshEntryEntity.SHIHARAI_ZEI_KBN_CD = torihikiShiharaiInfo.ZEI_KBN_CD;
								}

								// 支払取引区分CD
								if(false == torihikiShiharaiInfo.TORIHIKI_KBN_CD.IsNull)
								{
									urshEntryEntity.SHIHARAI_TORIHIKI_KBN_CD = torihikiShiharaiInfo.TORIHIKI_KBN_CD;
								}
							}
						}
					}

					// SYSTEM_IDの採番
					urshEntryEntity.SYSTEM_ID = this.accessor.createSystemIdForUrsh();

					// 枝番
					urshEntryEntity.SEQ = 1;

					// 売上／支払番号の採番
					urshEntryEntity.UR_SH_NUMBER = this.accessor.createUrshNumber();

					// 伝票日付と拠点CDから連番を取得
					DateTime denpyouDate = DateTime.Now;
					Int16 kyotenCd = -1;
					if(false == urshEntryEntity.DENPYOU_DATE.IsNull)
					{
						// 伝票日付
						denpyouDate = urshEntryEntity.DENPYOU_DATE.Value.Date;
					}
					if(false == urshEntryEntity.KYOTEN_CD.IsNull)
					{
						// 拠点CD取得
						kyotenCd = (Int16)urshEntryEntity.KYOTEN_CD;
					}

					// 日連番取得
					urshEntryEntity.DATE_NUMBER = this.GetDateNum(denpyouDate, kyotenCd);

					// 年連番取得
					urshEntryEntity.YEAR_NUMBER = this.GetYearNum(denpyouDate, kyotenCd);

					// 確定区分
					urshEntryEntity.KAKUTEI_KBN = this.KAKUTEI_KBN_MIKAKUTEI;

					#region - 現場明細 -
					// 現場明細レコードの情報を取得する
					foreach(DataRow detailRow in selectedRowsEdaban)
					{
						if(Int64.Parse(detailRow["NODE_EDABAN"].ToString()) == NODE_EDABAN_DETAIL)
						{
							// 現場実績のレコード番号に紐付く現場明細のセット
							if(int.Parse(jissekiRow["GENBA_JISSEKI_NO"].ToString()) == int.Parse(detailRow["GENBA_DETAIL_GENBA_NO"].ToString()))
							{
								// 初期化
								urshDetailEntity = new T_UR_SH_DETAIL();

								// 売上支払日付
								if(false == urshEntryEntity.DENPYOU_DATE.IsNull)
								{
									urshDetailEntity.URIAGESHIHARAI_DATE = urshEntryEntity.DENPYOU_DATE;
								}

								// 行番号
								if(false == string.IsNullOrEmpty(detailRow["GENBA_DETAIL_NO"].ToString()))
								{
									urshDetailEntity.ROW_NO = Int16.Parse(detailRow["GENBA_DETAIL_NO"].ToString());
								}

								// 品名CD
								if(false == string.IsNullOrEmpty(detailRow["GENBA_DETAIL_HINMEICD"].ToString()))
								{
									urshDetailEntity.HINMEI_CD = detailRow["GENBA_DETAIL_HINMEICD"].ToString();

									// 品名、伝票区分CD、税区分CDを取得
									var findEntity = new MobileShougunTorikomiDTOClass();
									findEntity.HINMEI_CD = urshDetailEntity.HINMEI_CD;
                                    var dataResultHinmei = this.dao.GetHinmeiDataForEntity(findEntity);
                                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                                    var findKobetsuEntity = new MobileShougunTorikomiDTOClass();
                                    findKobetsuEntity.GYOUSHA_CD = urshEntryEntity.GYOUSHA_CD;
                                    findKobetsuEntity.GENBA_CD = urshEntryEntity.GENBA_CD;
                                    findKobetsuEntity.HINMEI_CD = urshDetailEntity.HINMEI_CD;
                                    var dataResultKobetsuHinmei = this.dao.GetKobetsuHinmeiDataForEntity(findKobetsuEntity);
                                    // 20151021 katen #13337 品名手入力に関する機能修正 end
									if(dataResultHinmei.Rows.Count != 0)
                                    {
                                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                                        if (dataResultKobetsuHinmei.Rows.Count != 0)
                                        {
                                            urshDetailEntity.HINMEI_NAME = Convert.ToString(dataResultKobetsuHinmei.Rows[0]["SEIKYUU_HINMEI_NAME"]);
                                        }
                                        else
                                        {
                                            // 20151021 katen #13337 品名手入力に関する機能修正 end

                                            // 20160113 chenzz #13337 品名手入力に関する機能修正 start
                                            var findKobetsuEntity2 = new MobileShougunTorikomiDTOClass();
                                            findKobetsuEntity2.GYOUSHA_CD = urshEntryEntity.GYOUSHA_CD;
                                            findKobetsuEntity2.GENBA_CD = "";
                                            findKobetsuEntity2.HINMEI_CD = urshDetailEntity.HINMEI_CD;
                                            var dataResultKobetsuHinmei2 = this.dao.GetKobetsuHinmeiDataForEntity(findKobetsuEntity2);
                                            if (dataResultKobetsuHinmei2.Rows.Count != 0)
                                            {
                                                urshDetailEntity.HINMEI_NAME = Convert.ToString(dataResultKobetsuHinmei2.Rows[0]["SEIKYUU_HINMEI_NAME"]);
                                            }
                                            else
                                            {
                                                // 20160113 chenzz #13337 品名手入力に関する機能修正 end
                                                // 品名
                                                if (false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["HINMEI_NAME"].ToString()))
                                                {
                                                    urshDetailEntity.HINMEI_NAME = dataResultHinmei.Rows[0]["HINMEI_NAME"].ToString();
                                                }
                                            }
                                        }

										// 税区分CD
										if(false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["ZEI_KBN_CD"].ToString()))
										{
											urshDetailEntity.HINMEI_ZEI_KBN_CD = Int16.Parse(dataResultHinmei.Rows[0]["ZEI_KBN_CD"].ToString());
										}

										// 伝票区分CD
										if(false == string.IsNullOrEmpty(dataResultHinmei.Rows[0]["DENPYOU_KBN_CD"].ToString()))
										{
											// 伝票区分：支払以外は全て売上として扱う
											if(CommonConst.DENPYOU_KBN_SHIHARAI == Int16.Parse(dataResultHinmei.Rows[0]["DENPYOU_KBN_CD"].ToString()))
											{
												// 伝票区分：支払
												urshDetailEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
											}
											else
											{
												// 伝票区分：売上
												urshDetailEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
											}
										}
									}
								}

								// 数量
								if(false == string.IsNullOrEmpty(detailRow["GENBA_DETAIL_SUURYO1"].ToString()))
								{
                                    urshDetailEntity.SUURYOU = decimal.Parse(detailRow["GENBA_DETAIL_SUURYO1"].ToString());
								}

								// 単位CD
								if(false == string.IsNullOrEmpty(detailRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
								{
									urshDetailEntity.UNIT_CD = Int16.Parse(detailRow["GENBA_DETAIL_UNIT_CD1"].ToString());
								}

								// 受付番号に紐付く受付収集伝票が存在する場合
								if(uketsukeSsEntry != null)
								{
									// 受付番号と品名CD、単位CD、伝票区分CDをキーに受付(収集)明細テーブルから単価を取得
									if((false == urshEntryEntity.UKETSUKE_NUMBER.IsNull) &&
                                       (false == urshDetailEntity.ROW_NO.IsNull) &&
                                       (false == urshDetailEntity.HINMEI_CD.Equals(string.Empty)) &&
									   (false == urshDetailEntity.UNIT_CD.IsNull) &&
									   (false == urshDetailEntity.DENPYOU_KBN_CD.IsNull))
									{
										findDTO = new UketsukeSsDTOClass();
										findDTO.SYSTEM_ID = (Int64)uketsukeSsEntry.SYSTEM_ID;
										findDTO.SEQ = (Int32)uketsukeSsEntry.SEQ;
										findDTO.UKETSUKE_NUMBER = (Int64)urshEntryEntity.UKETSUKE_NUMBER;
                                        findDTO.ROW_NO = urshDetailEntity.ROW_NO.Value;
										findDTO.HINMEI_CD = urshDetailEntity.HINMEI_CD;
										findDTO.UNIT_CD = (Int16)urshDetailEntity.UNIT_CD;
										urshDetailEntity.TANKA = this.accessor.GetTanka((Int16)urshDetailEntity.DENPYOU_KBN_CD, findDTO);
									}
								}
								else
								{
									// 伝票登録情報をキーに単価を取得
									if((false == string.IsNullOrEmpty(urshDetailEntity.HINMEI_CD)) &&
									   (false == urshDetailEntity.UNIT_CD.IsNull) &&
									   (false == urshDetailEntity.DENPYOU_KBN_CD.IsNull))
									{
										urshDetailEntity.TANKA = this.accessor.GetTanka((Int16)urshDetailEntity.DENPYOU_KBN_CD,
																						 urshEntryEntity,
																						 urshDetailEntity.HINMEI_CD,
																						 (Int16)urshDetailEntity.UNIT_CD);
									}
								}

								// 受付番号と現場明細レコードのレコード番号をキーに受付(収集)明細テーブルから取得
								if((false == urshEntryEntity.UKETSUKE_NUMBER.IsNull) && (false == string.IsNullOrEmpty(detailRow["GENBA_DETAIL_NO"].ToString())))
								{
									// 受付番号に紐付く受付収集伝票が存在する場合
									if(uketsukeSsEntry != null)
									{
										findDTO = new UketsukeSsDTOClass();
										findDTO.SYSTEM_ID = (Int64)uketsukeSsEntry.SYSTEM_ID;
										findDTO.SEQ = (Int32)uketsukeSsEntry.SEQ;
										findDTO.UKETSUKE_NUMBER = (int)urshEntryEntity.UKETSUKE_NUMBER;
										findDTO.ROW_NO = Int16.Parse(detailRow["GENBA_DETAIL_NO"].ToString());
										var detailEntity = this.accessor.getUketsukeSsDetail(findDTO);
										if(detailEntity != null)
										{
											// 明細備考
											if(false == string.IsNullOrEmpty(detailEntity.MEISAI_BIKOU))
											{
												urshDetailEntity.MEISAI_BIKOU = detailEntity.MEISAI_BIKOU;
											}
										}
									}
								}

								// SYSTEM_IDの採番
								urshDetailEntity.SYSTEM_ID = urshEntryEntity.SYSTEM_ID;

								// 枝番
								urshDetailEntity.SEQ = 1;

								// 明細システムID
								urshDetailEntity.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

								// 売上／支払番号
								urshDetailEntity.UR_SH_NUMBER = urshEntryEntity.UR_SH_NUMBER;

								// 確定区分
								urshDetailEntity.KAKUTEI_KBN = this.KAKUTEI_KBN_MIKAKUTEI;

								// 伝票区分によった消費税端数CDの格納
								Int16 taxHasuCD = 0;
								Int16 kinHasuCD = 0;
								if(false == urshDetailEntity.DENPYOU_KBN_CD.IsNull)
								{
									if(CommonConst.DENPYOU_KBN_SHIHARAI == urshDetailEntity.DENPYOU_KBN_CD)
									{
										if(torihikiShiharaiInfo != null)
										{
											// 取引先支払情報より端数CDの取得
											taxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
											kinHasuCD = (Int16)torihikiShiharaiInfo.KINGAKU_HASUU_CD;
										}
									}
									else
									{
										if(torihikiSeikyuInfo != null)
										{
											// 取引先請求情報より端数CDの取得
											taxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
											kinHasuCD = (Int16)torihikiSeikyuInfo.KINGAKU_HASUU_CD;
										}
									}
								}

								// 明細金額再計算
								this.DetailCalc(urshEntryEntity, urshDetailEntity, taxHasuCD, kinHasuCD);

								// 売上_支払明細テーブル登録
								urshDetailEntityList.Add(urshDetailEntity);
							}
						}
					}

					#endregion - 現場明細 -

					// 総計取得
					this.GetMoneyTotal(urshEntryEntity, urshDetailEntityList);

					// 端数CDの取得
					Int16 shiTaxHasuCD = 0;
					Int16 uriTaxHasuCD = 0;
					if(torihikiShiharaiInfo != null)
					{
						// 取引先支払情報より端数CDの取得
						shiTaxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
					}
					if(torihikiSeikyuInfo != null)
					{
						// 取引先請求情報より端数CDの取得
						uriTaxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
					}

					// 伝票消費税計算
					this.EntryTaxCalc(urshEntryEntity, shiTaxHasuCD, "支払");
					this.EntryTaxCalc(urshEntryEntity, uriTaxHasuCD, "売上");

					// 売上支払伝票登録
					var bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(urshEntryEntity);
					bindLogic.SetSystemProperty(urshEntryEntity, false);
					urshEntryEntity.DELETE_FLG = false;
					urshEntryEntityList.Add(urshEntryEntity);
				}
			}

			#endregion - 現場実績 -

            // 20141119 koukouei 締済期間チェックの追加 start
            // 締済期間チェック
            if (shimeiCheckFlg && !ShimeiDateCheck(urshEntryEntityList, urshDetailEntityList))
            {
                return;
            }
            // 20141119 koukouei 締済期間チェックの追加 end

			if(false == urshEntryEntity.UKETSUKE_NUMBER.IsNull)
			{
				// 該当する受付伝票の配車状況に「売上」をセット
				// ※受付番号が登録されている配車ヘッダは常に唯一のため、
				// ※全ての現場実績のリストアップ後でも問題ない
				this.accessor.UpdateUketsukeDenpyo((Int64)urshEntryEntity.UKETSUKE_NUMBER);
			}

			// 売上支払入力伝票登録
			foreach(T_UR_SH_ENTRY entry in urshEntryEntityList)
			{
				setUrShEntryDao.Insert(entry);
			}

			// 売上支払明細伝票登録
			foreach(T_UR_SH_DETAIL detail in urshDetailEntityList)
			{
				setUrShDetailDao.Insert(detail);
			}
		}

		/// <summary>
		/// 日連番取得
		/// </summary>
		/// <param name="denpyouDate">伝票日付</param>
		/// <param name="kyotenCD">拠点CD</param>
		/// <returns name="int">日連番</returns>
		/// <remarks>日連番を取得し、連番管理Tableの更新を行う</remarks>
		private int GetDateNum(DateTime denpyouDate, Int16 kyotenCD)
		{
			byte[] numberDayTimeStamp = null;
			int retNum = 1;

			// 日連番取得
			DataTable numberDays = null;
			numberDays = this.accessor.GetNumberDay(denpyouDate.Date, this.accessor.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCD);

			if((numberDays == null) || (numberDays.Rows.Count == 0))
			{
				// 該当データが無い場合は初期値
				retNum = 1;
			}
			else
			{
				// 最新の番号に+1
				retNum = (int)numberDays.Rows[0]["CURRENT_NUMBER"] + 1;
				numberDayTimeStamp = (byte[])numberDays.Rows[0]["TIME_STAMP"];
			}

			// S_NUMBER_DAYテーブル情報セット
			var numberDay = new S_NUMBER_DAY();
			numberDay.NUMBERED_DAY = denpyouDate.Date;
			numberDay.DENSHU_KBN_CD = this.accessor.DENSHU_KBN_CD_URIAGESHIHARAI;
			numberDay.KYOTEN_CD = kyotenCD;
			numberDay.CURRENT_NUMBER = retNum;
			numberDay.DELETE_FLG = false;
			if(numberDayTimeStamp != null)
			{
				numberDay.TIME_STAMP = numberDayTimeStamp;
			}
			var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(numberDay);
			dataBinderNumberDay.SetSystemProperty(numberDay, false);

			// DBセット
			if(numberDay.CURRENT_NUMBER == 1)
			{
				// 初期値の場合はDB追加
				this.accessor.InsertNumberDay(numberDay);
			}
			else
			{
				// 初期値以降の場合はDB更新
				this.accessor.UpdateNumberDay(numberDay);
			}

			// 日連番を返却
			return retNum;
		}

		/// <summary>
		/// 年連番取得
		/// </summary>
		/// <param name="denpyouDate">伝票日付</param>
		/// <param name="kyotenCD">拠点CD</param>
		/// <returns name="int">年連番</returns>
		/// <remarks>年連番を取得し、連番管理Tableの更新を行う</remarks>
		private int GetYearNum(DateTime denpyouDate, Int16 kyotenCD)
		{
			byte[] numberYearTimeStamp = null;
			int retNum = 1;

			// 会社情報取得
			var corpInfo = new M_CORP_INFO();
			var corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
			var corpInfos = corpInfoDao.GetAllData();
			if((corpInfos != null) && (corpInfos.Length != 0))
			{
				corpInfo = corpInfos[0];
			}

			// 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
			DataTable numberYeas = null;
			SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)corpInfo.KISHU_MONTH);
			numberYeas = this.accessor.GetNumberYear(numberedYear, this.accessor.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCD);

			if(numberYeas == null || numberYeas.Rows.Count == 0)
			{
				// 該当データが無い場合は初期値
				retNum = 1;
			}
			else
			{
				// 最新の番号に+1
				retNum = (int)numberYeas.Rows[0]["CURRENT_NUMBER"] + 1;
				numberYearTimeStamp = (byte[])numberYeas.Rows[0]["TIME_STAMP"];
			}

			// S_NUMBER_YEARテーブル情報セット
			var numberYear = new S_NUMBER_YEAR();
			numberYear.NUMBERED_YEAR = numberedYear;
			numberYear.DENSHU_KBN_CD = this.accessor.DENSHU_KBN_CD_URIAGESHIHARAI;
			numberYear.KYOTEN_CD = kyotenCD;
			numberYear.CURRENT_NUMBER = retNum;
			numberYear.DELETE_FLG = false;
			if(numberYearTimeStamp != null)
			{
				numberYear.TIME_STAMP = numberYearTimeStamp;
			}
			var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(numberYear);
			dataBinderNumberYear.SetSystemProperty(numberYear, false);

			// DBセット
			if(numberYear.CURRENT_NUMBER == 1)
			{
				// 初期値の場合はDB追加
				this.accessor.InsertNumberYear(numberYear);
			}
			else
			{
				// 初期値以降の場合はDB更新
				this.accessor.UpdateNumberYear(numberYear);
			}

			// 年連番を返却
			return retNum;
		}

		/// <summary>
		/// 金額・消費税総計取得
		/// </summary>
		/// <param name="detailList">算出対象明細のリスト</param>
		/// <param name="entryEntity">格納先のEntry伝票</param>
		/// <remarks>
		/// 明細のListより、金額・消費税の総計を算出し、
		/// Entry伝票に格納する
		/// </remarks>
		private void GetMoneyTotal(T_UR_SH_ENTRY entryEntity, List<T_UR_SH_DETAIL> detailList)
		{
			// 一旦初期化
			entryEntity.URIAGE_AMOUNT_TOTAL = 0;
			entryEntity.URIAGE_TAX_SOTO_TOTAL = 0;
			entryEntity.URIAGE_TAX_UCHI_TOTAL = 0;
			entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
			entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
			entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
			entryEntity.SHIHARAI_AMOUNT_TOTAL = 0;
			entryEntity.SHIHARAI_TAX_SOTO_TOTAL = 0;
			entryEntity.SHIHARAI_TAX_UCHI_TOTAL = 0;
			entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
			entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
			entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;

			foreach(T_UR_SH_DETAIL detail in detailList)
			{
				if(CommonConst.DENPYOU_KBN_SHIHARAI == detail.DENPYOU_KBN_CD)
				{
					// 支払金額を積算
					entryEntity.SHIHARAI_AMOUNT_TOTAL += detail.KINGAKU;
					entryEntity.SHIHARAI_TAX_SOTO_TOTAL += detail.TAX_SOTO;
					entryEntity.SHIHARAI_TAX_UCHI_TOTAL += detail.TAX_UCHI;
					entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
					entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
					entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
				}
				else
				{
					// 売上金額を積算
					entryEntity.URIAGE_AMOUNT_TOTAL += detail.KINGAKU;
					entryEntity.URIAGE_TAX_SOTO_TOTAL += detail.TAX_SOTO;
					entryEntity.URIAGE_TAX_UCHI_TOTAL += detail.TAX_UCHI;
					entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
					entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
					entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
				}
			}
		}

		/// <summary>
		/// 明細金額再計算
		/// </summary>
		/// <param name="entryEntity">Entry伝票</param>
		/// <param name="detailEntity">Detail伝票</param>
		/// <param name="taxHasuCD">消費税端数CD</param>
		/// <param name="kinHasuCD">金額端数CD</param>
		/// <remarks>
		/// 明細金額の再計算を行い
		/// 明細金額、消費税を格納する
		/// </remarks>
		private void DetailCalc(T_UR_SH_ENTRY entryEntity, T_UR_SH_DETAIL detailEntity, Int16 taxHasuCD, Int16 kinHasuCD)
		{
			// 一旦初期化
			detailEntity.HINMEI_KINGAKU = 0;
			detailEntity.HINMEI_TAX_SOTO = 0;
			detailEntity.HINMEI_TAX_UCHI = 0;
			detailEntity.KINGAKU = 0;
			detailEntity.TAX_SOTO = 0;
			detailEntity.TAX_UCHI = 0;

			// 明細金額計算
			decimal kingaku = 0;
			if((false == detailEntity.SUURYOU.IsNull) && (false == detailEntity.TANKA.IsNull))
			{
				// 数量x単価
				decimal tempSuuryou = 0;
				decimal.TryParse(detailEntity.SUURYOU.ToString(), out tempSuuryou);
                kingaku = tempSuuryou * (decimal)detailEntity.TANKA;
			}
			else
			{
				// 数量もしくは単価が未格納の場合は0
				kingaku = 0;
			}

			// 税区分取得
			Int16 zeiKbn = 0;
			if(false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
			{
				// 品名税区分が登録されている場合
				zeiKbn = (Int16)detailEntity.HINMEI_ZEI_KBN_CD;
			}
			else
			{
				// 伝票区分によった税区分の格納
				if(false == detailEntity.DENPYOU_KBN_CD.IsNull)
				{
					if(CommonConst.DENPYOU_KBN_SHIHARAI == detailEntity.DENPYOU_KBN_CD)
					{
						// 支払税区分
						if(false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
						{
							zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
						}
					}
					else
					{
						// 売上税区分
						if(false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
						{
							zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
						}
					}
				}
			}

			// 一旦初期化
			entryEntity.URIAGE_SHOUHIZEI_RATE = 0;
			entryEntity.SHIHARAI_SHOUHIZEI_RATE = 0;

			// 消費税計算
			decimal sotoZei = 0;
			decimal uchiZei = 0;
			if(zeiKbn != 0)
			{
				// 消費税率の取得
				var rate = this.accessor.GetShouhizeiRate((DateTime)entryEntity.DENPYOU_DATE);
				entryEntity.URIAGE_SHOUHIZEI_RATE = rate;
				entryEntity.SHIHARAI_SHOUHIZEI_RATE = rate;

				// 税区分によった消費税の格納
				var zei = this.TaxCalc(zeiKbn, rate, kingaku);
				if(CommonConst.ZEI_KBN_SOTO == zeiKbn)
				{
					// 外税
					sotoZei = zei;
				}
				else if(CommonConst.ZEI_KBN_UCHI == zeiKbn)
				{
					// 内税
					uchiZei = zei;
				}
				else
				{
					// それ以外(非課税等)は0
					sotoZei = 0;
					uchiZei = 0;
				}
			}

			// 消費税端数処理
			if(taxHasuCD != 0)
			{
				sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
				uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
			}

			// 金額端数処理
			if(kinHasuCD != 0)
			{
				kingaku = CommonCalc.FractionCalc(kingaku, kinHasuCD);
			}

			// 金額、消費税のセット
			if(false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
			{
				// 品名税区分CDが登録されていた場合
				detailEntity.HINMEI_KINGAKU = kingaku;
				detailEntity.HINMEI_TAX_SOTO = sotoZei;
				detailEntity.HINMEI_TAX_UCHI = uchiZei;
			}
			else
			{
				// 品名税区分CDが登録されていなかった場合
				detailEntity.KINGAKU = kingaku;
				detailEntity.TAX_SOTO = sotoZei;
				detailEntity.TAX_UCHI = uchiZei;
			}
		}

		/// <summary>
		/// Entry伝票消費税計算
		/// </summary>
		/// <param name="entryEntity">Entry伝票</param>
		/// <param name="taxHasuCD">消費税端数CD</param>
		/// <param name="strProcType">処理種別("売上"or"支払")</param>
		/// <remarks>
		/// Entry伝票の消費税計算を行う
		/// </remarks>
		private void EntryTaxCalc(T_UR_SH_ENTRY entryEntity, Int16 taxHasuCD, string strProcType)
		{
			Int16 zeiKbn = 0;
			decimal rate = 0;
			decimal kingaku = 0;

			if(true == strProcType.Equals("支払"))
			{
				// 一旦初期化
				entryEntity.SHIHARAI_TAX_SOTO = 0;
				entryEntity.SHIHARAI_TAX_UCHI = 0;

				// 支払消費税計算
				if(false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
				{
					zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
				}
				rate = (decimal)entryEntity.SHIHARAI_SHOUHIZEI_RATE;
				kingaku = (decimal)entryEntity.SHIHARAI_AMOUNT_TOTAL;
			}
			else
			{
				// 一旦初期化
				entryEntity.URIAGE_TAX_SOTO = 0;
				entryEntity.URIAGE_TAX_UCHI = 0;

				// 売上消費税計算
				if(false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
				{
					zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
				}
				rate = (decimal)entryEntity.URIAGE_SHOUHIZEI_RATE;
				kingaku = (decimal)entryEntity.URIAGE_AMOUNT_TOTAL;
			}

			// 消費税計算
			decimal sotoZei = 0;
			decimal uchiZei = 0;
			var zei = this.TaxCalc(zeiKbn, rate, kingaku);
			if(zeiKbn != 0)
			{
				// 税区分によった消費税の格納
				if(CommonConst.ZEI_KBN_SOTO == zeiKbn)
				{
					// 外税
					sotoZei = zei;
				}
				else if(CommonConst.ZEI_KBN_UCHI == zeiKbn)
				{
					// 内税
					uchiZei = zei;
				}
				else
				{
					// それ以外(非課税等)は0
					sotoZei = 0;
					uchiZei = 0;
				}
			}

			// 消費税端数処理
			if(taxHasuCD != 0)
			{
				sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
				uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
			}

			// 消費税のセット
			if(true == strProcType.Equals("支払"))
			{
				// 支払
				entryEntity.SHIHARAI_TAX_SOTO = sotoZei;
				entryEntity.SHIHARAI_TAX_UCHI = uchiZei;
			}
			else
			{
				// 売上
				entryEntity.URIAGE_TAX_SOTO = sotoZei;
				entryEntity.URIAGE_TAX_UCHI = uchiZei;
			}
		}

		/// <summary>
		/// 消費税計算
		/// </summary>
		/// <param name="zeiKbn">税区分</param>
		/// <param name="rate">消費税率</param>
		/// <param name="kingaku">算出対象金額</param>
		/// <remarks>
		/// 税区分に従い、消費税の計算を行う
		/// </remarks>
		private decimal TaxCalc(Int16 zeiKbn, decimal rate, decimal kingaku)
		{
			decimal zei = 0;

			if(zeiKbn != 0)
			{
				// 税区分によった消費税の格納
				if(CommonConst.ZEI_KBN_SOTO == zeiKbn)
				{
					// 外税
					zei = kingaku * rate;
				}
				else if(CommonConst.ZEI_KBN_UCHI == zeiKbn)
				{
					// 内税
					zei = kingaku - (kingaku / (rate + 1));
				}
				else
				{
					// それ以外(非課税等)は0
					zei = 0;
				}
			}

			return zei;
		}

		/// <summary>
		/// Entry伝票複製
		/// </summary>
		/// <param name="entryEntity">複製対象のEntry伝票</param>
		/// <remarks>
		/// Entry伝票の複製を行う
		/// </remarks>
		private T_UR_SH_ENTRY createEntryEntityClone(T_UR_SH_ENTRY entryEntity)
		{
			var retEntity = new T_UR_SH_ENTRY();

			// 複製
			retEntity.SYSTEM_ID = entryEntity.SYSTEM_ID;
			retEntity.SEQ = entryEntity.SEQ;
			retEntity.KYOTEN_CD = entryEntity.KYOTEN_CD;
			retEntity.UR_SH_NUMBER = entryEntity.UR_SH_NUMBER;
			retEntity.DATE_NUMBER = entryEntity.DATE_NUMBER;
			retEntity.YEAR_NUMBER = entryEntity.YEAR_NUMBER;
			retEntity.KAKUTEI_KBN = entryEntity.KAKUTEI_KBN;
			retEntity.DENPYOU_DATE = entryEntity.DENPYOU_DATE;
			retEntity.SEARCH_DENPYOU_DATE = entryEntity.SEARCH_DENPYOU_DATE;
			retEntity.URIAGE_DATE = entryEntity.URIAGE_DATE;
			retEntity.SEARCH_URIAGE_DATE = entryEntity.SEARCH_URIAGE_DATE;
			retEntity.SHIHARAI_DATE = entryEntity.SHIHARAI_DATE;
			retEntity.SEARCH_SHIHARAI_DATE = entryEntity.SEARCH_SHIHARAI_DATE;
			retEntity.TORIHIKISAKI_CD = entryEntity.TORIHIKISAKI_CD;
			retEntity.TORIHIKISAKI_NAME = entryEntity.TORIHIKISAKI_NAME;
			retEntity.GYOUSHA_CD = entryEntity.GYOUSHA_CD;
			retEntity.GYOUSHA_NAME = entryEntity.GYOUSHA_NAME;
			retEntity.GENBA_CD = entryEntity.GENBA_CD;
			retEntity.GENBA_NAME = entryEntity.GENBA_NAME;
			retEntity.NIZUMI_GYOUSHA_CD = entryEntity.NIZUMI_GYOUSHA_CD;
			retEntity.NIZUMI_GYOUSHA_NAME = entryEntity.NIZUMI_GYOUSHA_NAME;
			retEntity.NIZUMI_GENBA_CD = entryEntity.NIZUMI_GENBA_CD;
			retEntity.NIZUMI_GENBA_NAME = entryEntity.NIZUMI_GENBA_NAME;
			retEntity.NIOROSHI_GYOUSHA_CD = entryEntity.NIOROSHI_GYOUSHA_CD;
			retEntity.NIOROSHI_GYOUSHA_NAME = entryEntity.NIOROSHI_GYOUSHA_NAME;
			retEntity.NIOROSHI_GENBA_CD = entryEntity.NIOROSHI_GENBA_CD;
			retEntity.NIOROSHI_GENBA_NAME = entryEntity.NIOROSHI_GENBA_NAME;
			retEntity.EIGYOU_TANTOUSHA_CD = entryEntity.EIGYOU_TANTOUSHA_CD;
			retEntity.EIGYOU_TANTOUSHA_NAME = entryEntity.EIGYOU_TANTOUSHA_NAME;
			retEntity.NYUURYOKU_TANTOUSHA_CD = entryEntity.NYUURYOKU_TANTOUSHA_CD;
			retEntity.NYUURYOKU_TANTOUSHA_NAME = entryEntity.NYUURYOKU_TANTOUSHA_NAME;
			retEntity.SHARYOU_CD = entryEntity.SHARYOU_CD;
			retEntity.SHARYOU_NAME = entryEntity.SHARYOU_NAME;
			retEntity.SHASHU_CD = entryEntity.SHASHU_CD;
			retEntity.SHASHU_NAME = entryEntity.SHASHU_NAME;
			retEntity.UNPAN_GYOUSHA_CD = entryEntity.UNPAN_GYOUSHA_CD;
			retEntity.UNPAN_GYOUSHA_NAME = entryEntity.UNPAN_GYOUSHA_NAME;
			retEntity.UNTENSHA_CD = entryEntity.UNTENSHA_CD;
			retEntity.UNTENSHA_NAME = entryEntity.UNTENSHA_NAME;
			retEntity.NINZUU_CNT = entryEntity.NINZUU_CNT;
			retEntity.KEITAI_KBN_CD = entryEntity.KEITAI_KBN_CD;
			retEntity.CONTENA_SOUSA_CD = entryEntity.CONTENA_SOUSA_CD;
			retEntity.MANIFEST_SHURUI_CD = entryEntity.MANIFEST_SHURUI_CD;
			retEntity.MANIFEST_TEHAI_CD = entryEntity.MANIFEST_TEHAI_CD;
			retEntity.DENPYOU_BIKOU = entryEntity.DENPYOU_BIKOU;
			retEntity.UKETSUKE_NUMBER = entryEntity.UKETSUKE_NUMBER;
			retEntity.RECEIPT_NUMBER = entryEntity.RECEIPT_NUMBER;
			retEntity.URIAGE_SHOUHIZEI_RATE = entryEntity.URIAGE_SHOUHIZEI_RATE;
			retEntity.URIAGE_AMOUNT_TOTAL = entryEntity.URIAGE_AMOUNT_TOTAL;
			retEntity.URIAGE_TAX_SOTO = entryEntity.URIAGE_TAX_SOTO;
			retEntity.URIAGE_TAX_UCHI = entryEntity.URIAGE_TAX_UCHI;
			retEntity.URIAGE_TAX_SOTO_TOTAL = entryEntity.URIAGE_TAX_SOTO_TOTAL;
			retEntity.URIAGE_TAX_UCHI_TOTAL = entryEntity.URIAGE_TAX_UCHI_TOTAL;
			retEntity.HINMEI_URIAGE_KINGAKU_TOTAL = entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL;
			retEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL;
			retEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL;
			retEntity.SHIHARAI_SHOUHIZEI_RATE = entryEntity.SHIHARAI_SHOUHIZEI_RATE;
			retEntity.SHIHARAI_AMOUNT_TOTAL = entryEntity.SHIHARAI_AMOUNT_TOTAL;
			retEntity.SHIHARAI_TAX_SOTO = entryEntity.SHIHARAI_TAX_SOTO;
			retEntity.SHIHARAI_TAX_UCHI = entryEntity.SHIHARAI_TAX_UCHI;
			retEntity.SHIHARAI_TAX_SOTO_TOTAL = entryEntity.SHIHARAI_TAX_SOTO_TOTAL;
			retEntity.SHIHARAI_TAX_UCHI_TOTAL = entryEntity.SHIHARAI_TAX_UCHI_TOTAL;
			retEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL;
			retEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL;
			retEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL;
			retEntity.URIAGE_ZEI_KEISAN_KBN_CD = entryEntity.URIAGE_ZEI_KEISAN_KBN_CD;
			retEntity.URIAGE_ZEI_KBN_CD = entryEntity.URIAGE_ZEI_KBN_CD;
			retEntity.URIAGE_TORIHIKI_KBN_CD = entryEntity.URIAGE_TORIHIKI_KBN_CD;
			retEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = entryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD;
			retEntity.SHIHARAI_ZEI_KBN_CD = entryEntity.SHIHARAI_ZEI_KBN_CD;
			retEntity.SHIHARAI_TORIHIKI_KBN_CD = entryEntity.SHIHARAI_TORIHIKI_KBN_CD;
			retEntity.TSUKI_CREATE_KBN = entryEntity.TSUKI_CREATE_KBN;
			retEntity.DELETE_FLG = entryEntity.DELETE_FLG;
			
			return retEntity;
		}

        /// <summary>
        /// 回数取得
        /// </summary>
        /// <param name="gyoushaCD">キーとなる業者CD</param>
        /// <param name="genbaCD">キーとなる現場CD</param>
        /// <param name="detailList">定期実績明細リスト</param>
        /// <returns name="Int32">回数</returns>
        /// <remarks>
        /// キーとなる業者・現場を定期実績明細リストより検索し、
        /// 合致するものがあれば、その最大値+1を回数とし返却する
        /// </remarks>
        private Int32 getRoundNo(string gyoushaCD, string genbaCD, List<T_TEIKI_JISSEKI_DETAIL> detailList)
        {
            Int32 roundNo = 1;

            // Insert対象の業者CD・現場CDと一致するものを検索
            var list = detailList.Where(r => (r.GYOUSHA_CD == gyoushaCD && r.GENBA_CD == genbaCD)).ToArray();

            if(list != null && list.Length > 0)
            {
                // 該当情報が存在すれば、回数の最大値+1をInsert対象の回数とする
                roundNo = (list.Select(r => r.ROUND_NO).Max() + 1).Value;
            }

            return roundNo;
        }

        #region 車輌チェック
		/// <summary>
		/// 車輌チェック
		/// </summary>
		/// <returns></returns>
		internal bool CheckSharyouCd()
		{
			bool returnVal = false;
			try
			{
				LogUtility.DebugMethodStart();

				// 車輌名をクリア
				this.form.txt_SharyouName.Text = string.Empty;

				// 入力されてない場合
				if(String.IsNullOrEmpty(this.form.ntxt_SharyouCd.Text))
				{
					// 処理終了
					returnVal = true;
                    LogUtility.DebugMethodEnd(returnVal);
					return returnVal;
				}

				// 車輌情報取得
				var sharyou = this.GetSharyou(this.form.ntxt_SharyouCd.Text);
				MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
				if(sharyou == null)
				{
					// 背景色変更
					this.form.ntxt_SharyouCd.IsInputErrorOccured = true;
					// メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
					return returnVal;
				}
				if(!string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text) && !sharyou.SHASYU_CD.Equals(this.form.ntxt_ShashuCd.Text))
				{
					// 背景色変更
					this.form.ntxt_SharyouCd.IsInputErrorOccured = true;
					// メッセージ表示
                    msgLogic.MessageBoxShow("E104", "車輌", "車種");
                    LogUtility.DebugMethodEnd(returnVal);
					return returnVal;
				}

				// 車輌名設定
				this.form.txt_SharyouName.Text = sharyou.SHARYOU_NAME_RYAKU;

				// 車種入力されてない場合
				if(string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text))
				{
					// 車種情報取得
					var shashu = this.GetShashu(sharyou.SHASYU_CD);
					if(shashu != null)
					{
						// 車種情報設定
						this.form.ntxt_ShashuCd.Text = shashu.SHASHU_CD;
						this.form.txt_ShashuName.Text = shashu.SHASHU_NAME_RYAKU;
					}
				}

				// 運転者入力されてない場合
				if(string.IsNullOrEmpty(this.form.ntxt_UntenshaCd.Text))
				{
					// 社員情報取得
					var shain = this.GetShain(sharyou.SHAIN_CD);
					if(shain != null)
					{
						// 運転者情報設定
						this.form.ntxt_UntenshaCd.Text = shain.SHAIN_CD;
						this.form.txt_UntenshaName.Text = shain.SHAIN_NAME_RYAKU;
					}
				}

				// 処理終了
				returnVal = true;
			}
			catch(Exception ex)
			{
                LogUtility.Error("ChechSharyouCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
			}
			finally
			{
				LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
		}

		/// <summary>
		/// 車輌情報取得
		/// </summary>
		/// <param name="sharyouCd">車輌CD</param>
		/// <returns></returns>
		public M_SHARYOU GetSharyou(string sharyouCd)
		{
			M_SHARYOU returnVal = null;
			try
			{
				LogUtility.DebugMethodStart(sharyouCd);

				if(string.IsNullOrEmpty(sharyouCd))
				{
					return returnVal;
				}

				// 検索条件設定
				M_SHARYOU keyEntity = new M_SHARYOU();
				keyEntity.GYOUSHA_CD = string.Empty;
				keyEntity.SHARYOU_CD = this.form.ntxt_SharyouCd.Text;

				// [業者CD="",車輌CD]でM_SHARYOUを検索する
				var returnEntitys = sharyouDao.GetAllValidData(keyEntity);
				if(returnEntitys != null && returnEntitys.Length > 0)
				{
					// PK指定のため1件
					returnVal = returnEntitys[0];
				}

				return returnVal;
			}
			catch(Exception ex)
			{
				LogUtility.Error("GetSharyou", ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd(returnVal);
			}
		}

		/// <summary>
		/// 車種情報取得
		/// </summary>
		/// <param name="shashuCd">車種CD</param>
		/// <returns></returns>
		public M_SHASHU GetShashu(string shashuCd)
		{
			M_SHASHU returnVal = null;
			try
			{
				LogUtility.DebugMethodStart(shashuCd);

				if(string.IsNullOrEmpty(shashuCd))
				{
					return returnVal;
				}

				// 検索条件設定
				M_SHASHU keyEntity = new M_SHASHU();
				keyEntity.SHASHU_CD = shashuCd;

				// [車種CD]でM_SHASHUを検索する
				var returnEntitys = this.shashuDao.GetAllValidData(keyEntity);
				if(returnEntitys != null && returnEntitys.Length > 0)
				{
					// PK指定のため1件
					returnVal = returnEntitys[0];
				}

				return returnVal;
			}
			catch(Exception ex)
			{
				LogUtility.Error("GetSharshu", ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd(returnVal);
			}
		}

		/// <summary>
		/// 社員情報取得
		/// </summary>
		/// <param name="shainCd">社員CD</param>
		/// <returns></returns>
		public M_SHAIN GetShain(string shainCd)
		{
			M_SHAIN returnVal = null;
			try
			{
				LogUtility.DebugMethodStart(shainCd);

				if(string.IsNullOrEmpty(shainCd))
				{
					return returnVal;
				}

				// 検索条件設定
				M_SHAIN keyEntity = new M_SHAIN();
				keyEntity.SHAIN_CD = shainCd;
				keyEntity.UNTEN_KBN = true;

				// [社員CD,運転者フラグ=true]でM_SHAINを検索する
				var returnEntitys = this.shainDao.GetAllValidData(keyEntity);
				if(returnEntitys != null && returnEntitys.Length > 0)
				{
					// PK指定のため1件
					returnVal = returnEntitys[0];
				}

				return returnVal;
			}
			catch(Exception ex)
			{
				LogUtility.Error("GetShain", ex);
				throw;
			}
			finally
			{
				LogUtility.DebugMethodEnd(returnVal);
			}
		}
		#endregion

        /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtp_SagyouDateFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtp_SagyouDateTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.dtp_SagyouDateFrom.Text);
            DateTime date_to = DateTime.Parse(this.form.dtp_SagyouDateTo.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtp_SagyouDateFrom.IsInputErrorOccured = true;
                this.form.dtp_SagyouDateTo.IsInputErrorOccured = true;
                this.form.dtp_SagyouDateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_SagyouDateTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_SagyouDateFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion
        /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　start

        // 20141119 koukouei 締済期間チェックの追加 start
        #region 締済期間チェック
        /// <summary>
        /// 締済期間チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShimeiDateCheck(List<T_UR_SH_ENTRY> entryList, List<T_UR_SH_DETAIL> detailList)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            ShimeCheckLogic CheckShimeDate = new ShimeCheckLogic();
            List<ReturnDate> returnDate_U = new List<ReturnDate>();
            List<ReturnDate> returnDate_S = new List<ReturnDate>();
            List<CheckDate> uriageList = new List<CheckDate>();
            List<CheckDate> shiharaiList = new List<CheckDate>();

            CheckDate cd = new CheckDate();

            foreach (T_UR_SH_ENTRY entry in entryList)
            {
                if (entry.KYOTEN_CD.IsNull || entry.DENPYOU_DATE.IsNull
                    || string.IsNullOrEmpty(entry.TORIHIKISAKI_CD))
                {
                    continue;
                }

                List<T_UR_SH_DETAIL> list = detailList.Where(t => t.SYSTEM_ID.Value == entry.SYSTEM_ID.Value
                                                            && t.SEQ.Value == entry.SEQ.Value).ToList();
                foreach (T_UR_SH_DETAIL detail in list)
                {
                    if (detail.DENPYOU_KBN_CD.IsNull)
                    {
                        continue;
                    }

                    cd = new CheckDate();
                    cd.KYOTEN_CD = entry.KYOTEN_CD.Value.ToString().PadLeft(2, '0');
                    cd.TORIHIKISAKI_CD = entry.TORIHIKISAKI_CD;
                    cd.CHECK_DATE = entry.DENPYOU_DATE.Value;

                    if (detail.DENPYOU_KBN_CD.Value == CommonConst.DENPYOU_KBN_URIAGE)
                    {
                        uriageList.Add(cd);
                    }
                    else
                    {
                        shiharaiList.Add(cd);
                    }
                }
            }

            // 売上チェック
            returnDate_U = CheckShimeDate.GetNearShimeDate(uriageList, 1);
            // 支払チェック
            returnDate_S = CheckShimeDate.GetNearShimeDate(shiharaiList, 2);

            // 売上
            if (returnDate_U.Count != 0)
            {
                this.shimeiCheckFlg = false;
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate_U)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        this.registFlg = false;
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C085", "請求") == DialogResult.Yes)
                {
                    this.registFlg = true;
                    return true;
                }
                else
                {
                    this.registFlg = false;
                    return false;
                }
            }
            // 支払
            else if (returnDate_S.Count != 0)
            {
                this.shimeiCheckFlg = false;
                //例外日付が含まれる
                foreach (ReturnDate rdDate in returnDate_S)
                {
                    if (rdDate.dtDATE == SqlDateTime.MinValue.Value)
                    {
                        this.registFlg = false;
                        msgLogic.MessageBoxShow("E214");
                        return false;
                    }
                }
                if (msgLogic.MessageBoxShow("C085", "請求") == DialogResult.Yes)
                {
                    this.registFlg = true;
                    return true;
                }
                else
                {
                    this.registFlg = false;
                    return false;
                }
            }
            return true;
        }
        #endregion
        // 20141119 koukouei 締済期間チェックの追加 end
        #endregion - Methods -

		#region - Inner Class -

		/// <summary>コンテナフォームダイアログを管理するクラス・コントロール</summary>
		private class ContenaFormDialogManager
		{
			#region - Constructors -

			/// <summary>Initializes a new instance of the <see cref="ContenaFormDialogManager"/> class.</summary>
			/// <param name="index">行インデックスを表す数値</param>
			/// <param name="frm">コンテナフォーム</param>
			public ContenaFormDialogManager(int index, ContenaForm frm)
			{
				// 行インデックス
				this.Index = index;

				// コンテナフォーム
				this.ContenaForm = frm;
			}

			#endregion - Constructors -

			#region - Properties -

			/// <summary>行インデックスを保持するプロパティ</summary>
			public int Index { get; private set; }

			/// <summary>コンテナフォームを保持するプロパティ</summary>
			public ContenaForm ContenaForm { get; private set; }

			#endregion - Properties -
		}

		#endregion - Inner Class -
        
        /// 20141128 Houkakou 「モバイル将軍取込」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SagyouDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var SagyouDateFromTextBox = this.form.dtp_SagyouDateFrom;
            var SagyouDateToTextBox = this.form.dtp_SagyouDateTo;

            SagyouDateToTextBox.Text = SagyouDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「モバイル将軍取込」のダブルクリックを追加する　end

        #region 20151021 hoanghm #13495

        /// <summary>
        /// Check files is open or not
        /// </summary>
        /// <param name="xmlFullPath">xml file path</param>
        /// <returns>true: file one of files is open; false: not open</returns>
        private bool CheckFileIsOpen(string[] xmlFullPath)
        {
            XmlDocument xmldoc = new XmlDocument();
            for (int i = 0; i < xmlFullPath.Length; ++i)
            {
                string fp = xmlFullPath[i];

                if (!File.Exists(fp))
                {   // ファイルが存在しない
                    continue;
                }

                FileInfo fin = new FileInfo(fp);
                FileStream stream = null;
                try
                {
                    stream = fin.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch
                {
                    MessageUtility messageUtil = new MessageUtility();
                    string errorMessage = messageUtil.GetMessage("E120").MESSAGE;
                    MessageBox.Show(errorMessage, Constans.WORNING_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    return true;//file is open
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }
            return false;//file is not open
        }
        
        #endregion
    }
}