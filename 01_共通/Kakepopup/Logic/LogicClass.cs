using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Shougun.Core.Common.Kakepopup.App;
using System.Windows.Forms;
using Shougun.Core.Common.Kakepopup.Accessor;
using Shougun.Core.Common.Kakepopup.Const;
using System.Data;

namespace Shougun.Core.Common.Kakepopup.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        //締日入力チェックメッセージ
        private const String SHIMEBIERRMSG = "【5,10,15,20,25,31】のみ入力してください。";
        
        //エラーメッセージID
        private const String E049 = "E049";

        //入力チェックメッセージタイトル
        private const String DIALOGTITLE = "インフォメーション";

        #region フィールド
        /// <summary>
        /// Form
        /// </summary>
        private KakePopupForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

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

        private IM_TORIHIKISAKIDao daoTorihikisaki;

        private const string DAISHOERROR = "終了取引先が開始取引先より前の値になっています。\n終了取引先には開始取引先以降の値を指定してください。";
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        internal LogicClass(KakePopupForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.dba = new DBAccessor();
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
        }

 		/// <summary>
		/// 画面初期化
		/// </summary>
		/// <param name="Joken">範囲条件情報</param>
        internal void WindowInit(UIConstans.ConditionInfo Joken)
        {
            LogUtility.DebugMethodStart(Joken);

            this.parentForm = (BusinessBaseForm)this.form.Parent;

            // イベントの初期化処理
            this.EventInit();

            // タイトルラベル設定
            switch (Joken.ShowDisplay)
            {
                case UIConstans.DispType.URIKAKE:
                    // 売上元帳時
                    this.form.lbl_hanishitei.Text = "売掛金一覧表 - 範囲条件指定";
                    this.form.Text = "売掛金一覧表 - 範囲条件指定";

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

                    /*
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
                    */
                    this.form.txt_starttorihikisakicd.popupWindowSetting = lstJoinMethodDto;
                    this.form.txt_endtorihikisakicd.popupWindowSetting = lstJoinMethodDto;
                    this.form.btn_kaishitorihikisaki.popupWindowSetting = lstJoinMethodDto;
                    this.form.btn_syuryoutorihikisaki.popupWindowSetting = lstJoinMethodDto;

                    break;
                case UIConstans.DispType.KAIKAKE:
                    // 支払元帳時
                    this.form.lbl_hanishitei.Text = "買掛金一覧表 - 範囲条件指定";
                    this.form.Text = "買掛金一覧表 - 範囲条件指定";

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

                    /*
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
                    */
                    this.form.txt_starttorihikisakicd.popupWindowSetting = lstJoinMethodDto2;
                    this.form.txt_endtorihikisakicd.popupWindowSetting = lstJoinMethodDto2;
                    this.form.btn_kaishitorihikisaki.popupWindowSetting = lstJoinMethodDto2;
                    this.form.btn_syuryoutorihikisaki.popupWindowSetting = lstJoinMethodDto2;

                    break;
                default:
                    // NOTHING
                    break;
            }
            // 画面表示項目初期化
            this.form.dtp_denpyouhizukefrom.Value = Joken.StartDay;
            this.form.dtp_denpyouhizuketo.Value = Joken.EndDay;
            this.form.txt_syuturyokunaiyou.Text = Joken.OutPutKBN.ToString();
            //this.form.txt_shimebi.ReadOnly = true;

            // CDから名前をSet
            this.form.txt_starttorihikisakicd.Text = Joken.StartTorihikisakiCD;
            this.form.txt_endtorihikisakicd.Text = Joken.EndTorihikisakiCD;
            if (false == string.IsNullOrEmpty(Joken.StartTorihikisakiCD))
            {
                // CDが設定されている場合、略称名のセット
                this.form.txt_starttorihikisakiname.Text = dba.GetName(Joken.StartTorihikisakiCD);
            }
            if (false == string.IsNullOrEmpty(Joken.EndTorihikisakiCD))
            {
                // CDが設定されている場合、略称名のセット
                this.form.txt_endtorihikisakiname.Text = dba.GetName(Joken.EndTorihikisakiCD);
            }

            LogUtility.DebugMethodEnd(Joken);
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

            // 検索実行ボタン(F9)イベント生成
            this.form.btn_kensakujikkou.DialogResult = DialogResult.OK;
            this.form.btn_kensakujikkou.Click += new EventHandler(this.form.SearchExec);

            // キャンセルボタン(F12)イベント生成
            this.form.btn_cancel.DialogResult = DialogResult.Cancel;
            this.form.btn_cancel.Click += new EventHandler(this.form.FormClose);

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
            if (this.allControl == null)
            {
                // 画面に表示しているすべてのControlを取得
                List<Control> allCtl = new List<Control>();
                allCtl.AddRange(CtrlUtil.GetAllControls(this.form));
                // コントロールが下からセットされるため反転させる
                allCtl.Reverse();
                this.allControl = allCtl.ToArray();
            }
            foreach (var c in allControl)
            {
                // 必須チェックが登録されているControlのみを抽出
                this.CheckControl = c as ICustomControl;
                if (this.CheckControl != null)
                {
                    var mthodList = this.CheckControl.RegistCheckMethod;
                    if (mthodList != null && mthodList.Count != 0)
                    {
                        var check = new CheckMethodSetting();
                        foreach (var checkMethodName in mthodList)
                        {
                            var methodSetting = check[checkMethodName.CheckMethodName];
                            if (methodSetting.MethodName == "MandatoryCheck")
                            {
                                // 必須チェック実行
                                ValidLogic = new Validator(this.CheckControl, null);
                                result = ValidLogic.MandatoryCheck();
                                if (result.Length != 0)
                                {
                                    // 入力されていないものがある場合はMessageListに登録
                                    MsgList.AppendLine(result);
                                }
                            }
                        }
                    }
                }
            }

            if (MsgList.Length != 0)
            {
                // エラー表示
                MessageBox.Show(MsgList.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Error = true;
            }
            else
            {
                //取引先CDの大小チェック
                if (0 < string.Compare(this.form.txt_starttorihikisakicd.Text, this.form.txt_endtorihikisakicd.Text))
                {
                    // エラー表示
                    MessageBox.Show(DAISHOERROR, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error = true;
                }
            }
            return Error;
        }

        #region "[F1]前月、[F2]次月押下時"
        /// <summary>
        /// 前月日付設定
        /// </summary>
        public void SetDatePreviousMonth(out object valDateFrom, out object valDateTo, bool isNextMonth)
        {
            valDateFrom = null;
            valDateTo = null;

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

            if (this.form.dtp_denpyouhizukefrom.Value != null)
            {
                DateTime dateFrom = (DateTime)this.form.dtp_denpyouhizukefrom.Value;
                valDateFrom = (object)dateFrom.AddMonths(monthFlg);
            }

            if (this.form.dtp_denpyouhizuketo.Value != null)
            {
                DateTime dateTo = (DateTime)this.form.dtp_denpyouhizuketo.Value;
                dateTo = dateTo.AddMonths(monthFlg);

                valDateTo = new DateTime(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month));
            }

            LogUtility.DebugMethodEnd(valDateFrom, valDateTo, isNextMonth);
        }
        #endregion "[F1]前月、[F2]次月押下時"

        #region "出力内容テキストボックス変更"
        public void TxtSyuturyokuNaiyouChangedLogic()
        {
            LogUtility.DebugMethodStart();

            TxtTyusyutsuHouhouChangedLogic(true);

            LogUtility.DebugMethodEnd();
        }
        #endregion "出力内容テキストボックス変更"

        #region "抽出方法テキストボックス変更"
        public void TxtTyusyutsuHouhouChangedLogic(bool txt_tyuusyutsuhouhou)
        {
            LogUtility.DebugMethodStart();

            if (txt_tyuusyutsuhouhou == true)
            {
                //抽出方法が1(月初・月末)の場合は開始日付に当月1日、終了日付に当月末日を設定する。
                DateTime today = this.form.sysDate;
                DateTime firstDay = today.AddDays(-today.Day + 1);
                DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                this.form.dtp_denpyouhizukefrom.Value = firstDay;
                this.form.dtp_denpyouhizuketo.Value = endDay;
                //前月・次月ボタンを有効にする。
                this.form.btn_zengetsu.Enabled = true;
                this.form.btn_jigetsu.Enabled = true;
            }
            else
            {
                //前月・次月ボタンを無効にする。
                this.form.btn_zengetsu.Enabled = false;
                this.form.btn_jigetsu.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion "抽出方法テキストボックス変更"

        /// <summary>
        /// 設定値保存
        /// </summary>
        internal void SaveParams()
        {
            LogUtility.DebugMethodStart();

            // 設定条件を保存
            var info = new UIConstans.ConditionInfo();
            info.ShowDisplay = this.form.Joken.ShowDisplay;							// 呼び出し画面(ポップアップ起動時のまま)
            info.OutPutKBN = int.Parse(this.form.txt_syuturyokunaiyou.Text);		// 出力区分
            //info.Shimebi = this.form.cb_shimebi.Text;		                        // 締日
            //info.TyusyutsuHouhou = int.Parse(this.form.txt_tyuusyutsuhouhou.Text);// 抽出方法
            info.TyusyutsuHouhou = 1;           // 抽出方法は月初月末のみ // No.4182
            info.StartDay = (DateTime)this.form.dtp_denpyouhizukefrom.Value;	    // 開始日付
            info.EndDay = (DateTime)this.form.dtp_denpyouhizuketo.Value;			// 終了日付
            info.StartTorihikisakiCD = this.form.txt_starttorihikisakicd.Text;		// 開始取引先CD
            info.EndTorihikisakiCD = this.form.txt_endtorihikisakicd.Text;			// 終了取引先CD
            info.DataSetFlag = true;												// 値格納フラグ
            this.form.Joken = info;

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
        /// 取引先CD MINを取得
        /// </summary>
        /// <returns></returns>
        public string GetTorihikisakiCdMin()
        { 
            string min_cd = "" ;
            var datatable = daoTorihikisaki.GetDateForStringSql("SELECT MIN(TORIHIKISAKI_CD) as TORIHIKISAKI_CD_MIN FROM M_TORIHIKISAKI_SHIHARAI");
            if(datatable != null){
                foreach (DataRow dr in datatable.Rows)
                {
                    min_cd = dr[0].ToString();
                    Console.Write(dr[0].ToString() +" MIN");
                    return min_cd;
                }
            }
            return min_cd;
        }
        /// <summary>
        /// 取引先CD MAXを取得
        /// </summary>
        /// <returns></returns>
        public string GetTorihikisakiCdMax()
        {
            string max_cd = "";
            var datatable = daoTorihikisaki.GetDateForStringSql("SELECT MAX(TORIHIKISAKI_CD) as TORIHIKISAKI_CD_MAX FROM M_TORIHIKISAKI_SHIHARAI");
            if (datatable != null)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    max_cd = dr[0].ToString();
                    Console.Write(dr[0].ToString() + " MAX");
                    return max_cd;
                }
            }
            return max_cd;
        }
    }
}
