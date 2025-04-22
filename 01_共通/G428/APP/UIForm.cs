using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using r_framework.Entity;
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Logic;
using GrapeCity.Win.MultiRow;
using r_framework.Dto;
using System.Collections.ObjectModel;
namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.APP
{
    public partial class UIForm : SuperForm
    {
        #region
        /// <summary>
        /// 覚書一括入力画面ロジック
        /// </summary>
        private LogicCls logic;
        /// <summary>
        /// 伝票番号
        /// </summary>
        private String mDenpyouNumber;
        public bool mstartFlg = false;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 検索文字列
        /// </summary>
        public string[] SearchString { get; private set; }

        /// <summary>
        /// popup呼出し前の業者コード
        /// </summary>
        private string saveGyoushaCd = string.Empty;

        /// <summary>
        /// popup呼出し前の荷卸業者コード
        /// </summary>
        private string saveNioroshiGyoushaCd = string.Empty;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_DENPYOU_TANKA_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();
                //伝票番号
                this.mDenpyouNumber = string.Empty;
                this.mstartFlg = true;
                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
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

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                this.logic.WindowInit(base.WindowType);
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

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {

                LogUtility.DebugMethodStart(sender, e);


                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl("Regist"), this.GetAllControl("Regist"));
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                //2.マスタ単価読み込みの場合、登録前新単価の必須チェック
                if (this.txtNum_HenkouHouhou.Text.Equals("2"))
                {
                    this.logic.ShinTankaChk();
                }
                // エラーの場合
                if (base.RegistErrorFlag)
                {
                    // 処理中止
                    return;
                }

                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　end

                // 登録用データの作成
                this.logic.CreateEntity();

                // データ登録
                if (!this.logic.RegistData())
                {
                    return;
                }
                //メッセージ通知
                this.logic.msgLogic.MessageBoxShow("I001", "登録");

                //登録ボッタン無効状態になる（連続登録防止）
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func9.Enabled = false;
                // 画面初期化
                this.PreviousValue = string.Empty;
                //再検索
                int cnt = this.logic.Search();
                if (cnt > 0)
                {
                    //検索結果を一覧に設定
                    this.logic.SetIchiran();
                    //単価設定
                    this.logic.CalcTanka();
                    //金額計算
                    this.logic.CalcKingaku();
                    //消費税計算
                    this.logic.CalcShouhizei();
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

        #region 検索処理
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {

                LogUtility.DebugMethodStart(sender, e);
                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl("Search"), this.GetAllControl("Search"));
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                // エラーの場合
                if (base.RegistErrorFlag)
                {
                    // 処理中止
                    return;
                }

                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　end

                if (this.logic.Search() == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    return;
                }

                //MultiRowのCellValueChangedイウェントを削除する（重複金額、消費税計算を退避）。
                grdIchiran.CellValueChanged -= this.grdIchiran_CellValueChanged;
                //検索結果を一覧に設定
                this.logic.SetIchiran();
                //単価設定
                this.logic.CalcTanka();
                //金額計算
                this.logic.CalcKingaku();
                //消費税計算
                this.logic.CalcShouhizei();


                //単価手入力以外の場合、新単価は読取専用
                if (null != this.txtNum_HenkouHouhou.Text && this.txtNum_HenkouHouhou.Text != "1")
                {
                    this.logic.changeIchiranTankaReadOnly(true);
                }
                else
                {
                    this.logic.changeIchiranTankaReadOnly(false);
                }

                //登録できる状態になる
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func9.Enabled = true;

                //MultiRowのCellValueChangedイウェントを追加する。
                grdIchiran.CellValueChanged += this.grdIchiran_CellValueChanged;


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

        #region 取消処理
        /// <summary>
        /// [F11] 取消処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //取消処理
                this.logic.Cancel();
                //画面初期値設定
                this.logic.InitData();
                //システム情報初期化処理
                this.logic.HearerSysInfoInit();
                //登録できない状態になる
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func9.Enabled = false;
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

        #region  F12 Formクローズ処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
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

        #region マルチローのセルのバリューチェンジイウェント
        /// <summary>
        /// マルチローのセルのバリューチェンジイウェント 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdIchiran_CellValueChanged(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //新単価
                if (e.CellName.Equals("SHIN_TANKA"))
                {
                    Row row = this.grdIchiran.Rows[e.RowIndex];
                    //明細金額計算
                    this.logic.CalcDetailKingaku(row);

                    //明細消費税計算
                    this.logic.CalcDetailShouhizei(row);
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

        #region マスタ単価読み込み変更モードに切り替え
        /// <summary>
        /// マスタ単価読み込み変更モードに切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnMasterTenka_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (radbtnMasterTenka.Checked)
                {
                    // 必須チェック設定
                    SelectCheckDto existCheck = new SelectCheckDto();
                    existCheck.CheckMethodName = "必須チェック";
                    Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                    excitChecks.Add(existCheck);
                    Lab_Hinmei.Text = "品名※";
                    HINMEI_CD.RegistCheckMethod = excitChecks;
                    Lab_Tani.Text = "単位※";
                    UNIT_CD.RegistCheckMethod = excitChecks;
                    Lab_IkatsuTanka.Text = "一括単価";
                    IKATSU_TANKA.RegistCheckMethod = null;
                    IKATSU_TANKA.IsInputErrorOccured = false;
                    this.logic.changeIchiranTankaReadOnly(true);
                    ////マスタ単価読み込みの場合は、新単価セルは入力不可
                    //this.logic.changeIchiranTankaReadOnly(true);
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

        #region 一括単価変更モードに切り替え
        /// <summary>
        /// 一括単価変更モードに切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radbtnIkatsuTenka_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (radbtnIkatsuTenka.Checked)
                {
                    // 必須チェック設定
                    SelectCheckDto existCheck = new SelectCheckDto();
                    existCheck.CheckMethodName = "必須チェック";
                    Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                    excitChecks.Add(existCheck);
                    Lab_Hinmei.Text = "品名※";
                    HINMEI_CD.RegistCheckMethod = excitChecks;
                    Lab_Tani.Text = "単位";
                    UNIT_CD.RegistCheckMethod = null;
                    UNIT_CD.IsInputErrorOccured = false;
                    Lab_IkatsuTanka.Text = "一括単価※";
                    IKATSU_TANKA.RegistCheckMethod = excitChecks;
                    ////一括単価の場合は、新単価セルは入力不可
                    //this.logic.changeIchiranTankaReadOnly(true);
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

        #region 単価手入力変更モードに切り替え
        /// <summary>
        /// 単価手入力変更モードに切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnTankaTenyuryoku_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (radbtnTankaTenyuryoku.Checked)
                {
                    Lab_Hinmei.Text = "品名";
                    HINMEI_CD.RegistCheckMethod = null;
                    HINMEI_CD.IsInputErrorOccured = false;
                    Lab_Tani.Text = "単位";
                    UNIT_CD.RegistCheckMethod = null;
                    UNIT_CD.IsInputErrorOccured = false;
                    Lab_IkatsuTanka.Text = "一括単価";
                    IKATSU_TANKA.RegistCheckMethod = null;
                    IKATSU_TANKA.IsInputErrorOccured = false;
                    ////単価手入力の場合は、新単価セルは入力可
                    //this.logic.changeIchiranTankaReadOnly(false);
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

        #region UIFormの必須入力コントロールを返す
        /// <summary>
        /// UIFormの必須入力コントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl(string CheKubun)
        {
            try
            {
                //LogUtility.DebugMethodStart();
                List<Control> allControl = new List<Control>();
                switch (CheKubun)
                {
                    case "Search":
                        //検索時必須チェック
                        //allControl.AddRange(this.allControl);
                        //伝票日付開始
                        allControl.Add(this.DENPYOU_DATE_FROM);
                        //伝票日付終了
                        allControl.Add(this.DENPYOU_DATE_TO);
                        //品名CD
                        allControl.Add(this.HINMEI_CD);
                        //単位CD
                        allControl.Add(this.UNIT_CD);
                        //一括単価
                        allControl.Add(this.IKATSU_TANKA);

                        break;
                    case "Regist":
                        //登録時
                        //allControl.AddRange(this.allControl);
                        //一覧view必須チェック
                        allControl.Add(this.grdIchiran);
                        break;
                }


                return allControl.ToArray();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 現場更新後処理
        /// <summary>
        /// 現場Validatingイヴェント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            //業者入ってない、現場入ってる場合、エラーを出す。
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.logic.msgLogic.MessageBoxShow("E012", "業者CD");
                this.logic.msgLogic.MessageBoxShow("E051", "業者CD");
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                e.Cancel = true;
            }
            else
            {
                e.Cancel = !this.logic.CheckGenba();
            }
        }

        /// <summary>
        /// /業者コードポップアップ後の処理
        /// </summary>
        public void GenbaCdPopupMethod()
        {
            //業者入ってない、現場入ってる場合、エラーを出す。
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.logic.msgLogic.MessageBoxShow("E012", "業者CD");
                this.logic.msgLogic.MessageBoxShow("E051", "業者CD");
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                this.GENBA_CD.Focus();
            }
        }
        #endregion

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPAN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyousha();
        }

        #region 荷降現場更新後処理
        /// <summary>
        /// 荷降現場Validatingイヴェント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            //荷降業者入ってない、荷降現場入ってる場合、エラーを出す。
            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.logic.msgLogic.MessageBoxShow("E012", "荷降業者業者CD");
                this.logic.msgLogic.MessageBoxShow("E051", "荷降業者業者CD");
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                e.Cancel = true;
            }
            else
            {
                e.Cancel = !this.logic.CheckNiororhiGenba();
            }
        }

        /// <summary>
        /// 荷降現場コードポップアップ後の処理
        /// </summary>
        public void NioroshiGenbaCdPopupMethod()
        {
            //荷降業者入ってない、荷降現場入ってる場合、エラーを出す。
            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text) &&
                !string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.logic.msgLogic.MessageBoxShow("E012", "荷降業者業者CD");
                this.logic.msgLogic.MessageBoxShow("E051", "荷降業者業者CD");
                // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                this.NIOROSHI_GENBA_CD.Focus();
            }
        }
        #endregion

        #region　ロストフォーカスの時、ゼロをチェックする
        /// <summary>
        /// 変更方法は一括単価の場合は、ゼロ入力をチェックする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IKATSU_TANKA_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //変更方法は一括単価の場合は、ゼロ入力をチェックする。
                if (radbtnIkatsuTenka.Checked)
                {
                    if (!string.IsNullOrEmpty(IKATSU_TANKA.Text))
                    {
                        if (decimal.Parse(IKATSU_TANKA.Text) <= 0)
                        {
                            //メッセージ通知
                            this.logic.msgLogic.MessageBoxShow("E042", "新単価は1以上");
                            e.Cancel = true;
                        }
                    }
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

        /// <summary>
        /// 一括単価の表示フォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IKATSU_TANKA_Leave(object sender, EventArgs e)
        {

            // 単価フォーマット
            String systemTankaFormat = this.logic.ChgDBNullToValue(this.logic.sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();

            if (!string.IsNullOrEmpty(this.IKATSU_TANKA.Text))
            {
                string str = this.IKATSU_TANKA.Text.ToString();
                str = str.Replace(",", "");

                decimal val = decimal.Parse(str);
                this.IKATSU_TANKA.Text = this.logic.SuuryouAndTankFormat(val, systemTankaFormat);
            }

        }

        /// <summary>
        /// 業者コードポップアップ前の処理
        /// </summary>
        public void GyoushaCdPopupBeforeMethod()
        {
            this.saveGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者コードポップアップ後の処理
        /// </summary>
        public void GyoushaCdPopupAfterMethod()
        {
            if (this.GYOUSHA_CD.Text.CompareTo(this.saveGyoushaCd) != 0)
            {
                //業者を変更時、従属している現場を初期化する
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 荷卸業者コードポップアップ前の処理
        /// </summary>
        public void NioroshiGyoushaCdPopupBeforeMethod()
        {
            this.saveNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷卸業者コードポップアップ後の処理
        /// </summary>
        public void NioroshiGyoushaCdPopupAfterMethod()
        {
            if (this.NIOROSHI_GYOUSHA_CD.Text.CompareTo(this.saveNioroshiGyoushaCd) != 0)
            {
                //荷卸業者を変更時、従属している荷卸現場を初期化する
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 荷卸業者コード変更前の処理
        /// </summary>
        private void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.saveNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷卸業者コード変更後の処理
        /// </summary>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (this.NIOROSHI_GYOUSHA_CD.Text.CompareTo(this.saveNioroshiGyoushaCd) != 0)
            {
                //荷卸業者を変更時、従属している荷卸現場を初期化する
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
            e.Cancel = !this.logic.CheckNioroshiGyoushaCd();
        }

        /// <summary>
        /// 業者コード変更前の処理
        /// </summary>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.saveGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者コード変更後の処理
        /// </summary>
        private void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (this.GYOUSHA_CD.Text.CompareTo(this.saveGyoushaCd) != 0)
            {
                //業者を変更時、従属している現場を初期化する
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
            }
        }
        // 20140624 syunrei EV004530_伝票単価一括変更の伝票日付と入力日付のラジオボタンを切り替えた時にラベルが変わらない　start
        /// <summary>
        /// 伝票日付Validated処理
        /// </summary>

        private void txtNum_DenpyouHiduke_TextChanged(object sender, EventArgs e)
        {
            switch (this.txtNum_DenpyouHiduke.Text)
            {
                case "1":
                    this.Lab_DenpyouHiduke.Text = "伝票日付※";
                    break;
                case "2":
                    this.Lab_DenpyouHiduke.Text = "入力日付※";
                    break;
                default:
                    break;
            }
        }
        // 20140624 syunrei EV004530_伝票単価一括変更の伝票日付と入力日付のラジオボタンを切り替えた時にラベルが変わらない　start

    }
}
