// $Id: LogicClass.cs 36295 2014-12-02 02:55:46Z fangjk@oec-h.com $
using System;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Accessor;
using Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.APP;
using Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Const;
using System.Collections.Generic;
using System.Linq;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Logic
{
    /// <summary>
    /// 範囲条件指定ポップアップ画面ロジック
    /// </summary>
    public class LogicClass
    {
        #region フィールド
		/// <summary>
		/// 範囲条件指定画面Form
		/// </summary>
		private UIForm form;
		/// <summary>
		/// 画面に表示しているすべてのコントロールを格納するフィールド
		/// </summary>
		private Control[] allControl;
		/// <summary>
		/// チェック対象コントロール
		/// </summary>
		public ICustomControl CheckControl { get; set; }
		/// <summary>
		/// DBAccessor
		/// </summary>
		private DBAccessor dba;
		/// <remarks>
		/// レイアウトに変更があった場合、下記値を再設定する必要有
		/// </remarks>
		private readonly int TitleMaxWidth = 500;
		/// <summary>
		/// 取引先請求情報のDao
		/// </summary>
		private IM_TORIHIKISAKI_SEIKYUUDao TorihikiSeikyuDao;
		/// <summary>
		/// 取引先支払情報のDao
		/// </summary>
		private IM_TORIHIKISAKI_SHIHARAIDao TorihikiShiharaiDao;

        internal MessageBoxShowLogic errmessage;
		/// <summary>
		/// 取引先チェック時のエラーメッセージ
		/// </summary>
		private const string DAISHOERROR = "終了取引先が開始取引先より前の値になっています。\n終了取引先には開始取引先以降の値を指定してください。";
		#endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
		/// <param name="targetForm">対象フォーム</param>
		internal LogicClass(UIForm targetForm)
        {
			// フィールドの初期化
			this.form = targetForm;
			this.dba = new DBAccessor();

			this.TorihikiSeikyuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
			this.TorihikiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.errmessage = new MessageBoxShowLogic();
		}

		/// <summary>
		/// 画面初期化
		/// </summary>
		/// <param name="Joken">範囲条件情報</param>
		internal bool WindowInit(UIConstans.ConditionInfo Joken)
		{
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(Joken);

                // イベントの初期化処理
                this.EventInit();

                // タイトルラベル設定
                switch (Joken.ShowDisplay)
                {
                    case UIConstans.DispType.URIAGE:
                        // 売上元帳時
                        this.form.TITLE_LABEL.Text = "売上元帳 - 範囲条件指定";
                        this.form.URIAGE_DATE_SHIMEBI.Text = "2.売上日付：締日";

                        //取引先_請求情報マスタをJOIN
                        System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto> lstJoinMethodDto = new System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>();
                        r_framework.Dto.JoinMethodDto cJoinMethodDto = new r_framework.Dto.JoinMethodDto();
                        cJoinMethodDto.IsCheckLeftTable = true;
                        cJoinMethodDto.IsCheckRightTable = true;
                        cJoinMethodDto.Join = r_framework.Const.JOIN_METHOD.INNER_JOIN;
                        cJoinMethodDto.LeftKeyColumn = "TORIHIKISAKI_CD";
                        cJoinMethodDto.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
                        cJoinMethodDto.RightKeyColumn = "TORIHIKISAKI_CD";
                        cJoinMethodDto.RightTable = "M_TORIHIKISAKI";

                        lstJoinMethodDto.Add(cJoinMethodDto);

                        cJoinMethodDto = new r_framework.Dto.JoinMethodDto();
                        cJoinMethodDto.IsCheckLeftTable = true;
                        cJoinMethodDto.IsCheckRightTable = true;
                        cJoinMethodDto.Join = r_framework.Const.JOIN_METHOD.WHERE;
                        cJoinMethodDto.LeftKeyColumn = string.Empty;
                        cJoinMethodDto.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
                        cJoinMethodDto.RightKeyColumn = string.Empty;
                        cJoinMethodDto.RightTable = string.Empty;

                        System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto> lstSearchCondition = new System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto>();
                        r_framework.Dto.SearchConditionsDto cSearchCondDto = new r_framework.Dto.SearchConditionsDto();

                        cSearchCondDto.And_Or = CONDITION_OPERATOR.AND;
                        cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto.LeftColumn = "SHIMEBI1";
                        cSearchCondDto.RightColumn = string.Empty;
                        cSearchCondDto.Value = "cb_shimebi";
                        cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition.Add(cSearchCondDto);

                        cSearchCondDto = new r_framework.Dto.SearchConditionsDto();
                        cSearchCondDto.And_Or = CONDITION_OPERATOR.OR;
                        cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto.LeftColumn = "SHIMEBI2";
                        cSearchCondDto.RightColumn = string.Empty;
                        cSearchCondDto.Value = "cb_shimebi";
                        cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition.Add(cSearchCondDto);

                        cSearchCondDto = new r_framework.Dto.SearchConditionsDto();
                        cSearchCondDto.And_Or = CONDITION_OPERATOR.OR;
                        cSearchCondDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto.LeftColumn = "SHIMEBI3";
                        cSearchCondDto.RightColumn = string.Empty;
                        cSearchCondDto.Value = "cb_shimebi";
                        cSearchCondDto.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition.Add(cSearchCondDto);

                        cJoinMethodDto.SearchCondition = lstSearchCondition;

                        lstJoinMethodDto.Add(cJoinMethodDto);
                        this.form.TORIHIKISAKI_CD_START.popupWindowSetting = lstJoinMethodDto;
                        this.form.TORIHIKISAKI_CD_END.popupWindowSetting = lstJoinMethodDto;
                        this.form.TORIHIKISAKI_SEARCH_START.popupWindowSetting = lstJoinMethodDto;
                        this.form.TORIHIKISAKI_SEARCH_END.popupWindowSetting = lstJoinMethodDto;

                        this.form.URIAGE_DATE_SHIMEBI.Text = "2.売上日付";
                        this.form.DATE_HANI_START.Enabled = true;
                        this.form.DATE_HANI_END.Enabled = true;
                        this.form.cb_shimebi.Enabled = true;

                        break;
                    case UIConstans.DispType.SHIHARAI:
                        // 支払元帳時
                        this.form.TITLE_LABEL.Text = "支払元帳 - 範囲条件指定";
                        this.form.URIAGE_DATE_SHIMEBI.Text = "2.支払日付：締日";

                        //取引先_支払情報マスタをJOIN
                        System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto> lstJoinMethodDto2 = new System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>();
                        r_framework.Dto.JoinMethodDto cJoinMethodDto2 = new r_framework.Dto.JoinMethodDto();
                        cJoinMethodDto2.IsCheckLeftTable = true;
                        cJoinMethodDto2.IsCheckRightTable = true;
                        cJoinMethodDto2.Join = r_framework.Const.JOIN_METHOD.INNER_JOIN;
                        cJoinMethodDto2.LeftKeyColumn = "TORIHIKISAKI_CD";
                        cJoinMethodDto2.LeftTable = "M_TORIHIKISAKI_SHIHARAI";
                        cJoinMethodDto2.RightKeyColumn = "TORIHIKISAKI_CD";
                        cJoinMethodDto2.RightTable = "M_TORIHIKISAKI";

                        lstJoinMethodDto2.Add(cJoinMethodDto2);

                        cJoinMethodDto2 = new r_framework.Dto.JoinMethodDto();
                        cJoinMethodDto2.IsCheckLeftTable = true;
                        cJoinMethodDto2.IsCheckRightTable = true;
                        cJoinMethodDto2.Join = r_framework.Const.JOIN_METHOD.WHERE;
                        cJoinMethodDto2.LeftKeyColumn = string.Empty;
                        cJoinMethodDto2.LeftTable = "M_TORIHIKISAKI_SHIHARAI";
                        cJoinMethodDto2.RightKeyColumn = string.Empty;
                        cJoinMethodDto2.RightTable = string.Empty;

                        System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto> lstSearchCondition2 = new System.Collections.ObjectModel.Collection<r_framework.Dto.SearchConditionsDto>();
                        r_framework.Dto.SearchConditionsDto cSearchCondDto2 = new r_framework.Dto.SearchConditionsDto();

                        cSearchCondDto2.And_Or = CONDITION_OPERATOR.AND;
                        cSearchCondDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto2.LeftColumn = "SHIMEBI1";
                        cSearchCondDto2.RightColumn = string.Empty;
                        cSearchCondDto2.Value = "cb_shimebi";
                        cSearchCondDto2.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition2.Add(cSearchCondDto2);

                        cSearchCondDto2 = new r_framework.Dto.SearchConditionsDto();
                        cSearchCondDto2.And_Or = CONDITION_OPERATOR.OR;
                        cSearchCondDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto2.LeftColumn = "SHIMEBI2";
                        cSearchCondDto2.RightColumn = string.Empty;
                        cSearchCondDto2.Value = "cb_shimebi";
                        cSearchCondDto2.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition2.Add(cSearchCondDto2);

                        cSearchCondDto2 = new r_framework.Dto.SearchConditionsDto();
                        cSearchCondDto2.And_Or = CONDITION_OPERATOR.OR;
                        cSearchCondDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                        cSearchCondDto2.LeftColumn = "SHIMEBI3";
                        cSearchCondDto2.RightColumn = string.Empty;
                        cSearchCondDto2.Value = "cb_shimebi";
                        cSearchCondDto2.ValueColumnType = DB_TYPE.IN_SMALLINT;

                        lstSearchCondition2.Add(cSearchCondDto2);

                        cJoinMethodDto2.SearchCondition = lstSearchCondition2;

                        lstJoinMethodDto2.Add(cJoinMethodDto2);
                        this.form.TORIHIKISAKI_CD_START.popupWindowSetting = lstJoinMethodDto2;
                        this.form.TORIHIKISAKI_CD_END.popupWindowSetting = lstJoinMethodDto2;
                        this.form.TORIHIKISAKI_SEARCH_START.popupWindowSetting = lstJoinMethodDto2;
                        this.form.TORIHIKISAKI_SEARCH_END.popupWindowSetting = lstJoinMethodDto2;

                        this.form.URIAGE_DATE_SHIMEBI.Text = "2.支払日付";
                        this.form.DATE_HANI_START.Enabled = true;
                        this.form.DATE_HANI_END.Enabled = true;
                        this.form.cb_shimebi.Enabled = true;

                        break;
                    default:
                        // NOTHING
                        this.form.TITLE_LABEL.Text = "";
                        break;
                }

                // タイトルラベル範囲調整
                ControlUtility.AdjustTitleSize(this.form.TITLE_LABEL, this.TitleMaxWidth);

                // 画面表示項目初期化
                this.form.TORIHIKI_KBN.Text = Joken.TorihikiKBN.ToString();
                this.form.TYUUSYUTU_KBN.Text = Joken.TyuusyutuKBN.ToString();
                this.form.cb_shimebi.Text = Joken.Shimebi;
                this.form.DATE_HANI_START.Value = Joken.StartDay;
                this.form.DATE_HANI_END.Value = Joken.EndDay;
                this.form.OUTPUT_KBN.Text = Joken.OutPutKBN.ToString();
                this.form.Text = this.form.TITLE_LABEL.Text;

                // CDから名前をSet
                this.form.TORIHIKISAKI_CD_START.Text = Joken.StartTorihikisakiCD;
                this.form.TORIHIKISAKI_CD_END.Text = Joken.EndTorihikisakiCD;
                if (false == string.IsNullOrEmpty(Joken.StartTorihikisakiCD))
                {
                    // CDが設定されている場合、略称名のセット
                    this.form.TORIHIKISAKI_NAME_START.Text = dba.GetName(Joken.StartTorihikisakiCD);
                }
                if (false == string.IsNullOrEmpty(Joken.EndTorihikisakiCD))
                {
                    // CDが設定されている場合、略称名のセット
                    this.form.TORIHIKISAKI_NAME_END.Text = dba.GetName(Joken.EndTorihikisakiCD);
                }
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
		}

		/// <summary>
		/// イベントの初期化処理
		/// </summary>
		private void EventInit()
		{
			LogUtility.DebugMethodStart();

			// UIFormキーイベント生成
			this.form.KeyUp += new KeyEventHandler(this.form.ItemKeyUp);

			// 前月ボタン(F1)イベント生成
			this.form.btn_zengetsu.Click += new EventHandler(this.form.Function1Click);

			// 次月ボタン(F2)イベント生成
			this.form.btn_jigetsu.Click += new EventHandler(this.form.Function2Click);

			// 検索実行ボタン(F8)イベント生成
			this.form.bt_func8.DialogResult = DialogResult.OK;
			this.form.bt_func8.Click += new EventHandler(this.form.SearchExec);

			// キャンセルボタン(F12)イベント生成
			this.form.bt_func12.DialogResult = DialogResult.Cancel;
			this.form.bt_func12.Click += new EventHandler(this.form.FormClose);

            /// 20141128 Houkakou ダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.DATE_HANI_END.MouseDoubleClick += new MouseEventHandler(DATE_HANI_END_MouseDoubleClick);
            /// 20141128 Houkakou ダブルクリックを追加する　end

			LogUtility.DebugMethodEnd();
		}

		/// <summary>
		/// 必須チェック実行
		/// </summary>
		/// <returns name="bool">エラー発生</returns>
		internal bool RegistCheck()
		{
			/*************************************************/
			/**		元帳PopUpはヘッダやフッタを使用しない	**/
			/**		単一フォームであるため、必須チェックの	**/
			/**		トリガを自前で発生させる				**/
			/*************************************************/
			bool Error = false;

			var CtrlUtil = new ControlUtility();
			var MsgList = new StringBuilder();
			Validator ValidLogic;
			string result;
			if(this.allControl == null)
			{
				// 画面に表示しているすべてのControlを取得
				this.allControl = CtrlUtil.GetAllControls(this.form);
			}
			foreach(var c in allControl)
			{
				// 必須チェックが登録されているControlのみを抽出
				this.CheckControl = c as ICustomControl;
				if(this.CheckControl != null)
				{
					var mthodList = this.CheckControl.RegistCheckMethod;
					if(mthodList != null && mthodList.Count != 0)
					{
						var check = new CheckMethodSetting();
						foreach(var checkMethodName in mthodList)
						{
							var methodSetting = check[checkMethodName.CheckMethodName];
							if(methodSetting.MethodName == "MandatoryCheck")
							{
								// 必須チェック実行
								ValidLogic = new Validator(this.CheckControl, null);
								result = ValidLogic.MandatoryCheck();
								if(result.Length != 0)
								{
									// 入力されていないものがある場合はMessageListに登録
									MsgList.AppendLine(result);
								}
							}
						}
					}
				}
			}
			
			if(MsgList.Length != 0)
			{
				// エラー表示
				MessageBox.Show(MsgList.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Error = true;
			}
			else
			{
				//取引先CDの大小チェック
				if (0 < string.Compare(this.form.TORIHIKISAKI_CD_START.Text, this.form.TORIHIKISAKI_CD_END.Text))
				{
					// エラー表示
					MessageBox.Show(DAISHOERROR, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Error = true;
				}
			}

			return Error;
		}

		#region "締日コンボボックス変更"
		/// <summary>
		/// 締日コンボボックス変更
		/// </summary>
		public bool CmbShimeDateValidatedLogic()
		{
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 締日が変更されたら取引先を空にする
                this.form.TORIHIKISAKI_CD_START.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_START.Text = string.Empty;
                this.form.TORIHIKISAKI_CD_END.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_END.Text = string.Empty;

                // 日付範囲指定を入力可に設定
                this.form.DATE_HANI_START.Enabled = true;
                this.form.DATE_HANI_END.Enabled = true;

                // 締日に応じて日付範囲指定を変更する
                if (!string.IsNullOrEmpty(this.form.cb_shimebi.Text))
                {
                    if (this.form.cb_shimebi.Text == "31" || this.form.cb_shimebi.Text == "0")
                    {
                        // 締日が31日又は０日の場合は開始日付に当月1日、終了日付に当月末日を設定する。
                        // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                        //DateTime today = DateTime.Today;
                        DateTime today = this.form.sysDate;
                        // 20150902 katen #12048 「システム日付」の基準作成、適用 end
                        DateTime firstDay = today.AddDays(-today.Day + 1);
                        DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                        this.form.DATE_HANI_START.Value = firstDay;
                        this.form.DATE_HANI_END.Value = endDay;

                        // 締日が０日の場合
                        if (this.form.cb_shimebi.Text == "0")
                        {
                            // 日付範囲指定を入力可能に設定
                            this.form.DATE_HANI_START.Enabled = true;
                            this.form.DATE_HANI_END.Enabled = true;
                        }
                    }
                    else
                    {
                        //締日が31日以外の場合は開始日付に前月締日＋1日、終了日付に当月締日を設定する。
                        // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                        //DateTime today = DateTime.Today;
                        DateTime today = this.form.sysDate;
                        // 20150902 katen #12048 「システム日付」の基準作成、適用 end
                        string stday = today.ToString("yyyy/MM/dd HH:mm:ss");
                        stday = stday.Substring(0, 8) + String.Format("{0:00}", int.Parse(this.form.cb_shimebi.Text));
                        string f = "yyyy/MM/dd";
                        DateTime endDay = DateTime.ParseExact(stday, f, null);
                        DateTime firstDay = endDay.AddMonths(-1).AddDays(1);
                        this.form.DATE_HANI_START.Value = firstDay;
                        this.form.DATE_HANI_END.Value = endDay;
                    }
                }
                else if (this.form.cb_shimebi.Text == string.Empty)
                {
                    DateTime today = this.form.sysDate;
                    DateTime firstDay = today.AddDays(-today.Day + 1);
                    DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                    this.form.DATE_HANI_START.Value = firstDay;
                    this.form.DATE_HANI_END.Value = endDay;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CmbShimeDateValidatedLogic", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
		}
		#endregion "締日テキストボックス変更"

		#region "[F1]前月、[F2]次月押下時"
		/// <summary>
		/// 前月日付設定
		/// </summary>
		public bool SetDatePreviousMonth(out object valDateFrom, out object valDateTo, bool isNextMonth)
		{
            bool ret = true;
            valDateFrom = null;
            valDateTo = null;

            try
            {
                LogUtility.DebugMethodStart(valDateFrom, valDateTo, isNextMonth);

                int monthFlg = 0;

                if (isNextMonth)
                {
                    monthFlg = 1;
                }
                else
                {
                    monthFlg = -1;
                }

                // 日付範囲指定(FROM)を設定
                if (this.form.DATE_HANI_START.Value != null)
                {
                    DateTime dateFrom = (DateTime)this.form.DATE_HANI_START.Value;
                    valDateFrom = (object)dateFrom.AddMonths(monthFlg);
                }

                // 日付範囲指定(TO)を設定
                if (this.form.DATE_HANI_END.Value != null)
                {
                    DateTime dateTo = (DateTime)this.form.DATE_HANI_END.Value;
                    dateTo = dateTo.AddMonths(monthFlg);

                    //締日が末日または０またはブランクの場合は月初月末を設定
                    if (this.form.cb_shimebi.Text == "31" || this.form.cb_shimebi.Text == "" || this.form.cb_shimebi.Text == "0")
                    {
                        valDateTo = new DateTime(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month));
                    }
                    else
                    {
                        valDateTo = dateTo;
                    }
                }
            }
            catch (Exception ex1)
            {
                LogUtility.Error("SetDatePreviousMonth", ex1);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret, valDateFrom, valDateTo);
            return ret;
		}
		#endregion "[F1]前月、[F2]次月押下時"

		/// <summary>
		/// 設定値保存
		/// </summary>
		internal bool SaveParams()
		{
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 設定条件を保存
                var info = new UIConstans.ConditionInfo();
                info.ShowDisplay = this.form.Joken.ShowDisplay;							// 呼び出し画面(ポップアップ起動時のまま)
                info.StartDay = (DateTime)this.form.DATE_HANI_START.Value;				// 開始日付
                info.EndDay = (DateTime)this.form.DATE_HANI_END.Value;					// 終了日付
                info.StartTorihikisakiCD = this.form.TORIHIKISAKI_CD_START.Text;		// 開始取引先CD
                info.EndTorihikisakiCD = this.form.TORIHIKISAKI_CD_END.Text;			// 終了取引先CD
                info.OutPutKBN = int.Parse(this.form.OUTPUT_KBN.Text);					// 出力区分
                info.DataSetFlag = true;												// 値格納フラグ
                info.TorihikiKBN = int.Parse(this.form.TORIHIKI_KBN.Text);              // 取引区分CD(元帳種類)
                info.TyuusyutuKBN = int.Parse(this.form.TYUUSYUTU_KBN.Text);            // 抽出方法
                info.Shimebi = this.form.cb_shimebi.Text;                               // 締日
                this.form.Joken = info;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveParams", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
		}

		/// <summary>
		/// 取引先と締日の関係をチェックします
		/// </summary>
		/// <param name="shimebi">締日</param>
		/// <param name="torihikisakiCd">取引先CD</param>
		/// <param name="fromToKBN">関連チェックをしたい取引先を指定
		///                                取引先FROM = "1"
		///                                取引先TO   = "2"</param>
		/// <returns>チェック結果</returns>
		internal bool CheckTorihikisakiShimebi(string shimebi, string torihikisakiCd, string fromToKBN)
		{
            try
            {
                //取引先が空だったらReturn
                if (string.Empty == torihikisakiCd)
                {

                    if (fromToKBN == "1")
                    {
                        // 取引先FROMをクリア
                        this.form.TORIHIKISAKI_CD_START.Text = string.Empty;
                        this.form.TORIHIKISAKI_NAME_START.Text = string.Empty;
                    }
                    else if (fromToKBN == "2")
                    {
                        // 取引先TOをクリア
                        this.form.TORIHIKISAKI_CD_END.Text = string.Empty;
                        this.form.TORIHIKISAKI_NAME_END.Text = string.Empty;
                    }
                    return true;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                M_TORIHIKISAKI_SEIKYUU torihikiInfoSe = new M_TORIHIKISAKI_SEIKYUU();
                M_TORIHIKISAKI_SHIHARAI torihikiInfoSi = new M_TORIHIKISAKI_SHIHARAI();

                // 取引先情報取得
                if (this.form.Joken.ShowDisplay == UIConstans.DispType.URIAGE)
                {
                    // 売上元帳の場合
                    torihikiInfoSe = this.TorihikiSeikyuDao.GetDataByCd(torihikisakiCd);

                    // 取引先が取得できなかった場合
                    if (null == torihikiInfoSe)
                    {
                        // 取引先名設定
                        if (fromToKBN == "1")
                        {
                            // 取引先FROMをクリア
                            this.form.TORIHIKISAKI_NAME_START.Text = string.Empty;
                        }
                        else if (fromToKBN == "2")
                        {
                            // 取引先TOをクリア
                            this.form.TORIHIKISAKI_NAME_END.Text = string.Empty;
                        }

                        return false;
                    }

                    // 締日がブランクの場合は締日チェックをしない
                    if (String.IsNullOrEmpty(shimebi))
                    {
                        return true;
                    }

                    // 締日チェック
                    var shimebi1 = torihikiInfoSe.SHIMEBI1.ToString();
                    var shimebi2 = torihikiInfoSe.SHIMEBI2.ToString();
                    var shimebi3 = torihikiInfoSe.SHIMEBI3.ToString();

                    if (shimebi != shimebi1 &&
                    shimebi != shimebi2 &&
                    shimebi != shimebi3)
                    {
                        msgLogic.MessageBoxShow("E058");

                        return false;
                    }

                }
                else if (this.form.Joken.ShowDisplay == UIConstans.DispType.SHIHARAI)
                {
                    // 支払元帳の場合
                    torihikiInfoSi = this.TorihikiShiharaiDao.GetDataByCd(torihikisakiCd);

                    // 取引先が取得できなかった場合
                    if (null == torihikiInfoSi)
                    {
                        // 取引先名設定
                        if (fromToKBN == "1")
                        {
                            // 取引先FROMをクリア
                            this.form.TORIHIKISAKI_NAME_START.Text = string.Empty;
                        }
                        else if (fromToKBN == "2")
                        {
                            // 取引先TOをクリア
                            this.form.TORIHIKISAKI_NAME_END.Text = string.Empty;
                        }

                        return false;
                    }

                    // 締日がブランクの場合は締日チェックをしない
                    if (String.IsNullOrEmpty(shimebi))
                    {
                        return true;
                    }

                    // 締日チェック
                    var shimebi1 = torihikiInfoSi.SHIMEBI1.ToString();
                    var shimebi2 = torihikiInfoSi.SHIMEBI2.ToString();
                    var shimebi3 = torihikiInfoSi.SHIMEBI3.ToString();

                    if (shimebi != shimebi1 &&
                    shimebi != shimebi2 &&
                    shimebi != shimebi3)
                    {
                        msgLogic.MessageBoxShow("E058");

                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiShimebi", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiShimebi", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
			return true;
		}

        /// 20141023 Houkakou 日付チェックを追加する　start
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

                this.form.DATE_HANI_START.BackColor = Constans.NOMAL_COLOR;
                this.form.DATE_HANI_END.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.DATE_HANI_START.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.DATE_HANI_END.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.DATE_HANI_START.Text);
                DateTime date_to = DateTime.Parse(this.form.DATE_HANI_END.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.DATE_HANI_START.IsInputErrorOccured = true;
                    this.form.DATE_HANI_END.IsInputErrorOccured = true;
                    this.form.DATE_HANI_START.BackColor = Constans.ERROR_COLOR;
                    this.form.DATE_HANI_END.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "日付範囲指定From", "日付範囲指定To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.DATE_HANI_START.Focus();
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
        /// 20141023 Houkakou 日付チェックを追加する　end
        
        /// 20141128 Houkakou ダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_HANI_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.DATE_HANI_START;
            var ToTextBox = this.form.DATE_HANI_END;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou ダブルクリックを追加する　end
	}
}
