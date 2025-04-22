// $Id: LogicClass.cs 36295 2014-12-02 02:55:46Z fangjk@oec-h.com $
using System;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Accessor;
using Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.APP;
using Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Const;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Logic
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
        private DBAccessor accessor;
        /// <summary>
        /// 取引先請求情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao TorihikiSeikyuDao;
        /// <summary>
        /// 取引先支払情報のDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao TorihikiShiharaiDao;
        private MessageBoxShowLogic MsgBox;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">対象フォーム</param>
        internal LogicClass(UIForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.accessor = new DBAccessor();
            this.MsgBox = new MessageBoxShowLogic();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <param name="Joken">範囲条件情報</param>
        internal void WindowInit(UIConstans.ConditionInfo Joken)
        {
            try
            {
                LogUtility.DebugMethodStart(Joken);

                // イベントの初期化処理
                this.EventInit();

                this.form.Text = this.form.TITLE_LABEL.Text;

                // 画面表示項目初期化
                this.form.DATE_HANI_START.Value = Joken.StartDay;
                this.form.DATE_HANI_END.Value = Joken.EndDay;

                // CDから名前をSet
                this.form.UNPAN_GYOUSHA_CD_FROM.Text = Joken.StartUnpanGyoushaCD;
                this.form.UNPAN_GYOUSHA_CD_END.Text = Joken.EndUnpanGyoushaCD;
                if (!string.IsNullOrEmpty(Joken.StartUnpanGyoushaCD))
                {
                    // CDが設定されている場合、略称名のセット
                    this.form.UNPAN_GYOUSHA_NAME_FROM.Text = this.accessor.GetName(Joken.StartUnpanGyoushaCD);
                }
                if (!string.IsNullOrEmpty(Joken.EndUnpanGyoushaCD))
                {
                    // CDが設定されている場合、略称名のセット
                    this.form.UNPAN_GYOUSHA_NAME_END.Text = this.accessor.GetName(Joken.EndUnpanGyoushaCD);
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("WindowInit", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(Joken);
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // UIFormキーイベント生成
            //this.form.KeyUp += new KeyEventHandler(this.form.ItemKeyUp);

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 必須チェック実行
        /// </summary>
        /// <param name="catchErr"></param>
        /// <returns></returns>
        internal bool RegistCheck(out bool catchErr)
        {
            /*************************************************/
            /**        元帳PopUpはヘッダやフッタを使用しない    **/
            /**        単一フォームであるため、必須チェックの    **/
            /**        トリガを自前で発生させる                **/
            /*************************************************/
            bool Error = false;
            catchErr = false;

            try
            {
                var CtrlUtil = new ControlUtility();
                var MsgList = new StringBuilder();
                Validator ValidLogic;
                string result;
                if (this.allControl == null)
                {
                    // 画面に表示しているすべてのControlを取得
                    this.allControl = CtrlUtil.GetAllControls(this.form);
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
                    // 運搬業者CDの大小チェック
                    if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text) &&
                        !string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_END.Text) &&
                        0 < string.Compare(this.form.UNPAN_GYOUSHA_CD_FROM.Text, this.form.UNPAN_GYOUSHA_CD_END.Text))
                    {
                        // エラー表示
                        string[] errorMsg = { "運搬業者From", "運搬業者To" };
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E032", errorMsg);
                        this.form.UNPAN_GYOUSHA_CD_FROM.Focus();
                        this.form.UNPAN_GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                        this.form.UNPAN_GYOUSHA_CD_END.IsInputErrorOccured = true;
                        Error = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }

            return Error;
        }

        #region [F1]前月、[F2]次月押下時
        /// <summary>
        /// 前月日付設定
        /// </summary>
        public void SetDatePreviousMonth(out object valDateFrom, out object valDateTo, bool isNextMonth, out bool catchErr)
        {
            valDateFrom = null;
            valDateTo = null;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart(valDateFrom, valDateTo, isNextMonth, catchErr);

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
                    valDateTo = (object)dateTo.AddMonths(monthFlg);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDatePreviousMonth", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd(valDateFrom, valDateTo, isNextMonth, catchErr);
            }
        }
        #endregion

        /// <summary>
        /// 設定値保存
        /// </summary>
        internal void SaveParams()
        {
            LogUtility.DebugMethodStart();

            // 設定条件を保存
            var info = new UIConstans.ConditionInfo();
            info.StartDay = (DateTime)this.form.DATE_HANI_START.Value;              // 開始日付
            info.EndDay = (DateTime)this.form.DATE_HANI_END.Value;                  // 終了日付
            info.StartUnpanGyoushaCD = this.form.UNPAN_GYOUSHA_CD_FROM.Text;        // 開始運搬業者CD
            info.EndUnpanGyoushaCD = this.form.UNPAN_GYOUSHA_CD_END.Text;           // 終了運搬業者CD
            info.DataSetFlag = true;												// 値格納フラグ
            this.form.Joken = info;

            LogUtility.DebugMethodEnd();
        }

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool ret = false;
            try
            {
                this.form.DATE_HANI_START.BackColor = Constans.NOMAL_COLOR;
                this.form.DATE_HANI_END.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.DATE_HANI_START.Text))
                {
                    return ret;
                }
                if (string.IsNullOrEmpty(this.form.DATE_HANI_END.Text))
                {
                    return ret;
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
                    string[] errorMsg = { "伝票日付From", "伝票日付To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.DATE_HANI_START.Focus();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = true;
            }

            return false;
        }
        #endregion

        #region 運搬業者チェック処理

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        /// <param name="unpanType">
        /// 1:開始運搬業者
        /// 2:終了運搬業者
        /// </param>
        internal void UnpanGyoushaCheck(int unpanType)
        {
            try
            {
                CustomAlphaNumTextBox unpanCd = new CustomAlphaNumTextBox();
                CustomTextBox unpanNm = new CustomTextBox();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                switch (unpanType)
                {
                    case 1:
                        unpanCd = this.form.UNPAN_GYOUSHA_CD_FROM;
                        unpanNm = this.form.UNPAN_GYOUSHA_NAME_FROM;
                        break;
                    case 2:
                        unpanCd = this.form.UNPAN_GYOUSHA_CD_END;
                        unpanNm = this.form.UNPAN_GYOUSHA_NAME_END;
                        break;
                }

                if (string.IsNullOrEmpty(unpanCd.Text))
                {
                    unpanNm.Text = string.Empty;
                    return;
                }

                var gyousha = this.accessor.GetGyousya(unpanCd.Text);
                if (gyousha == null || !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    unpanNm.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "業者");
                    unpanCd.Focus();
                    unpanCd.IsInputErrorOccured = true;
                }
                else
                {
                    unpanNm.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("UnpanGyoushaCheck", sqlEx);
                this.MsgBox.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnpanGyoushaCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        #endregion
    }
}
