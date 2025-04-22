using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    /// <summary>
    /// 定期配車入力画面
    /// </summary>
    [Implementation]
    public partial class UnchinNyuuryokuForm : SuperForm
    {
        /// <summary>
        /// ヘッダー
        /// </summary>
        private UIHeaderForm headerForm;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region フィールド
        /// <summary>
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、引数なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod();

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// </summary>
        public LastRunMethod closeMethod;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

        internal bool isCopyFlg = false;
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private UnchinNyuuryokuLogic logic;
        /// <summary>
        /// 連携伝種区分CD
        /// </summary>
        public SqlInt16 Renkei_Denshu_Kbn_cd { get; set; }
        /// <summary>
        /// 連携システム
        /// </summary>
        public SqlInt64 Renkei_Number { get; set; }
        /// <summary>
        /// 伝票区分CD
        /// </summary>
        public SqlInt16 Denpyou_kbn_cd { get; set; }

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private Dictionary<string, string> valuesForDetail = new Dictionary<string, string>();

        public SuperEntity[] entitys;

        private string previousNidumiGyoushaCd { get; set; }
        private string previousNioroshiGyoushaCd { get; set; }
        private string previousShashuCd { get; set; }
        private string previousGyoushaCd { get; set; }

        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowtype"></param>
        /// <param name="headerForm"></param>
        /// <param name="RENKEI_DENSHU_KBN_CD"></param>
        /// <param name="RENKEI_SYSTEM_ID"></param>
        public UnchinNyuuryokuForm(WINDOW_TYPE windowtype, UIHeaderForm headerForm, SqlInt16 RENKEI_DENSHU_KBN_CD, SqlInt64 RENKEI_NUMBER, SuperEntity[] entitys)
            : base(WINDOW_ID.T_UNCHIN_NYUURYOKU, windowtype)
        {
            try
            {
                LogUtility.DebugMethodStart(windowtype, headerForm, RENKEI_DENSHU_KBN_CD, RENKEI_NUMBER, entitys);

                if (!String.IsNullOrEmpty(RENKEI_DENSHU_KBN_CD.ToString()))
                {
                    this.Renkei_Denshu_Kbn_cd = RENKEI_DENSHU_KBN_CD;
                }
                if (!String.IsNullOrEmpty(RENKEI_NUMBER.ToString()))
                {
                    this.Renkei_Number = RENKEI_NUMBER;
                }
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new UnchinNyuuryokuLogic(this);

                this.entitys = entitys;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UnchinNyuuryokuForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public bool CheckAuth()
        {
            return this.logic.CheckAuth();
        }
        #endregion コンストラクタ

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit()) { return; }
            this.HeaderFormInit();

			// Anchorの設定は必ずOnLoadで行うこと
            if (this.dgvDetail != null)
            {
                this.dgvDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        /// <summary>
        /// 画面表示時の制御
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// 画面の全てのTEXTBOXの内容をクリアする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlLoopDel(Control.ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control control in controls)
            {
                if (control is r_framework.CustomControl.CustomAlphaNumTextBox)
                {
                    r_framework.CustomControl.CustomAlphaNumTextBox pb = (r_framework.CustomControl.CustomAlphaNumTextBox)control;
                    pb.Clear();
                }
                else if (control is System.Windows.Forms.Panel)
                {
                    ControlLoopDel(((System.Windows.Forms.Panel)control).Controls);
                }
            }
        }

        /// <summary>
        /// 画面クリア処理
        /// </summary>
        /// <param name="e"></param>
        internal void ClearScreen(EventArgs e)
        {
            LogUtility.DebugMethodStart();

            //base.OnLoad(e);

            if (!this.logic.ControlInit()) { return; }
            this.HeaderFormInit();

            //テキストボックスの削除
            ControlLoopDel(this.Controls);

            LogUtility.DebugMethodEnd();
        }

        #region ファンクションボタン
        /// <summary>
        /// 新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func02_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.Renkei_Denshu_Kbn_cd.IsNull)
            {
                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E158", "新規");
                    return;
                }
            }
            WINDOW_TYPE type = this.WindowType;
            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (!this.logic.CheckAuth())
            {
                this.WindowType = type;
                return;
            }
            //base.OnLoad(e);
            if (!this.logic.ControlInit()) { return; }
            if (!this.logic.ControlLock()) { return; }
            this.HeaderFormInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func03_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = false;
            if (this.logic.SearchDataAll(this.txt_DenpyouBango.Text.PadLeft(10, '0'), out catchErr, null))
            {
                if (catchErr) { return; }

                WINDOW_TYPE type = this.WindowType;
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                if (!this.logic.CheckAuth())
                {
                    this.WindowType = type;
                    return;
                }
                if (!r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正権限は無いが参照権限がある場合は参照モードで起動
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                if (!this.logic.ControlLock()) { return; }
                this.HeaderFormInit();
            }
            else
            {
                if (catchErr) { return; }

                // メッセージ表示
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                if (!this.logic.ControlInit()) { return; }
                if (!this.logic.ControlLock()) { return; }
                this.HeaderFormInit();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// (F7)一覧イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func07_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 20150514 一覧へ遷移権限制御追加 Start
                //FormManager.OpenForm("G641", DENSHU_KBN.UNCHIN.GetHashCode(), SystemProperty.Shain.CD);
                FormManager.OpenFormWithAuth("G641", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.UNCHIN.GetHashCode(), SystemProperty.Shain.CD);
                // 20150514 一覧へ遷移権限制御追加 End
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// (F9)登録イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func09_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    // 確認メッセージ
                    var result = this.logic.MsgBox.MessageBoxShow("C026");
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }

                    // データ削除
                    if (this.logic.LogicalDeleteData())
                    {
                        // メッセージ通知
                        this.logic.MsgBox.MessageBoxShow("I001", "削除");
                    }
                    else if (!this.logic.RegistResult)
                    { 
                        return;
                    }

                    if (r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 【新規】モードに変更
                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        if (!this.logic.ControlInit()) { return; }
                        if (!this.logic.ControlLock()) { return; }
                        this.HeaderFormInit();
                    }
                    else
                    {
                        base.CloseTopForm();
                    }
                    LogUtility.DebugMethodEnd();
                    return;
                }
                if (this.RegistErrorFlag) { return; }
                if (!this.logic.CreateEntitys()) { return; }
                // 登録処理
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        if (this.dgvDetail.Rows.Count == 1)
                        {
                            this.logic.MsgBox.MessageBoxShow("E001", "明細行");
                            return;
                        }

                        if (!this.logic.KingakuCheck(out catchErr))
                        {
                            if (catchErr) { return; }
                            // 数量*単価=金額かのチェック
                            // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                            this.logic.MsgBox.MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                            return;
                        }

                        // データ登録
                        this.logic.Regist(this.RegistErrorFlag);
                        if (this.logic.RegistResult)
                        {
                            // メッセージ通知
                            this.logic.MsgBox.MessageBoxShow("I001", "登録");

                            if (r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                if (!this.logic.ControlInit()) { return; }
                                if (!this.logic.ControlLock()) { return; }
                                this.HeaderFormInit();
                            }
                            else
                            {
                                base.CloseTopForm();
                            }
                        }

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 確認メッセージ
                        var result = this.logic.MsgBox.MessageBoxShow("C038");
                        if (result != DialogResult.Yes)
                        {
                            return;
                        }

                        if (this.dgvDetail.Rows.Count == 1)
                        {
                            this.logic.MsgBox.MessageBoxShow("E001", "明細行");
                        }

                        if (!this.logic.KingakuCheck(out catchErr))
                        {
                            if (catchErr) { return; }
                            // 数量*単価=金額かのチェック
                            // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                            this.logic.MsgBox.MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                            return;
                        }

                        // データ更新
                        this.logic.Update(this.RegistErrorFlag);
                        if (this.logic.UpdateResult)
                        {
                            // メッセージ通知
                            this.logic.MsgBox.MessageBoxShow("I001", "修正");
                            if (r_framework.Authority.Manager.CheckAuthority("G153", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                if (!this.logic.ControlInit()) { return; }
                                if (!this.logic.ControlLock()) { return; }
                                this.HeaderFormInit();
                            }
                            else
                            {
                                base.CloseTopForm();
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細に行を追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.dgvDetail.CellValidating -= this.dgvDetail_CellValidating;
            // 行挿入
            this.dgvDetail.Rows.Insert(this.dgvDetail.CurrentRow.Index, 1);
            this.dgvDetail["UNCHIN_HINMEI_CD", this.dgvDetail.CurrentRow.Index - 1].Selected = true;
            this.dgvDetail.Focus();

            this.dgvDetail.CellValidating += this.dgvDetail_CellValidating;
            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// 明細の行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.dgvDetail.CellValidating -= this.dgvDetail_CellValidating;
            if (this.dgvDetail.CurrentRow.Index == this.dgvDetail.Rows.Count - 1)
            {
                // 処理終了
                return;
            }
            this.dgvDetail.Rows.RemoveAt(this.dgvDetail.CurrentRow.Index);
            // 合計値を再計算
            if (!this.logic.KingakuTotal()) { return; }
            if (!this.logic.CalcDetailGoukeikingaku()) { return; }
            this.dgvDetail.CellValidating += this.dgvDetail_CellValidating;
            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// (F12)閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion

        #region 値保持

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }
        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;
            if (value.Name == this.txt_SHARYOU_CD.Name)
            {
                if (this.txt_SHARYOU_CD.AutoChangeBackColorEnabled)
                {
                    this.txt_SHARYOU_CD.UpdateBackColor(true);
                }
                else
                {
                    this.txt_SHARYOU_CD.BackColor = this.logic.sharyouCdBackColorBlue;
                }
            }
            else
            {
                this.txt_SHARYOU_CD.UpdateBackColor(false);
            }
            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }
        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender, string newText)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, newText); //一致する場合変更なし
        }

        #endregion

        #region 画面コントロールイベント
        #region 業者CD設定時に取引先CD,取引先名称を取得する。
        public void txt_GyoushaCd_PopBefore()
        {
            this.previousGyoushaCd = this.txt_GyoushaCd.Text;
        }
        public void txt_GyoushaCd_PopAfter()
        {
            if (this.previousGyoushaCd != this.txt_GyoushaCd.Text)
            {
                this.txt_SHARYOU_CD.Clear();
                this.txt_SHARYOU_MEI.Clear();

                // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                this.dgvDetailReload();
            }
        }

        //業者CD設定時に取引先CD,取引先名称を取得する。
        internal void txt_GyoushaCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            bool catchErr = false;
            try
            {
                this.txt_SHARYOU_CD.Text = string.Empty;
                this.txt_SHARYOU_MEI.Text = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.txt_GyoushaCd.Text))
                {
                    this.logic.CheckUnpanGyoushaCd(this.txt_GyoushaCd.Text, e, out catchErr);
                }
                else
                {
                    this.txt_GyoushaMei.Text = "";
                    if (this.txt_SHARYOU_MEI.ReadOnly)
                    {
                        this.txt_SHARYOU_CD.Text = "";
                        this.txt_SHARYOU_MEI.Text = "";
                    }
                }

                // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                this.dgvDetailReload();

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region「次ボタン」イベント
        /// <summary>
        /// 「次ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void btn_Tsugi_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                if (this.logic.SearchDataAll(this.txt_DenpyouBango.Text, out catchErr, null, "Next"))
                {
                    if (catchErr) { return; }

                    if (!this.logic.ControlLock()) { return; }
                    this.HeaderFormInit();
                    this.DENPYOU_DATE.Focus();
                }
                else
                {
                    if (catchErr) { return; }

                    // メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 「前ボタン」イベント
        /// <summary>
        /// 「前ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void btn_Mae_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                if (this.logic.SearchDataAll(this.txt_DenpyouBango.Text, out catchErr, null, "Pre"))
                {
                    if (catchErr) { return; }
                    if (!this.logic.ControlLock()) { return; }
                    this.HeaderFormInit();
                    this.DENPYOU_DATE.Focus();
                }
                else
                {
                    if (catchErr) { return; }

                    // メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region SearchDataAll
        internal void txt_DenpyouBango_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                if (!isChanged(sender)) { return; }
                if (this.txt_DenpyouBango.Text == "" || string.IsNullOrEmpty(this.txt_DenpyouBango.Text))
                {
                    return;
                }
                if (this.logic.SearchDataAll(this.txt_DenpyouBango.Text.PadLeft(10, '0'), out catchErr, e))
                {
                    if (catchErr) { return; }
                    if (!this.logic.ControlLock()) { return; }
                    this.HeaderFormInit();
                }
                else
                {
                    if (catchErr) { return; }
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    if (!this.logic.ControlInit()) { return; }
                    if (!this.logic.ControlLock()) { return; }
                    this.HeaderFormInit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 運転者
        public void txt_UntenshaCd_PopAfter()
        {
            var value = txt_UntenshaCd as Control;
            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = null;
            }
            else
            {
                _EnterValue.Add(value, null);
            }
        }

        internal void txt_UntenshaCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.CheckUntenshaCd(e))
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }
                // 拠点CDの値が無で拠点名が有の場合、拠点名をクリアする。
                if (string.IsNullOrEmpty(this.txt_UntenshaCd.Text) && !string.IsNullOrEmpty(this.txt_UntenshaMei.Text))
                {
                    this.txt_UntenshaMei.Clear();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 形態区分

        /// <summary>
        /// 虫眼鏡からの形態区分検索ポップアップ起動後に呼ばれるメソッド
        /// </summary>
        public void KEITAI_KBN_CD_PopAfter()
        {
            var value = KEITAI_KBN_CD as Control;
            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = null;
            }
            else
            {
                _EnterValue.Add(value, null);
            }
        }

        /// <summary>
        /// 形態区分Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KEITAI_KBN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                this.logic.CheckKeitaiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷積業者
        public void txt_NidumiGyoushaCd_PopBefore()
        {
            this.previousNidumiGyoushaCd = this.txt_NidumiGyoushaCd.Text;
        }
        public void txt_NidumiGyoushaCd_PopAfter()
        {
            //var value = txt_NidumiGyoushaCd as Control;
            //if (_EnterValue.ContainsKey(value))
            //{
            //    _EnterValue[value] = null;
            //}
            //else
            //{
            //    _EnterValue.Add(value, null);
            //}

            if (this.previousNidumiGyoushaCd != this.txt_NidumiGyoushaCd.Text)
            {
                this.txt_NidumiGenbaCd.Clear();
                this.txt_NidumiGenbaMei.Clear();
            }
        }
        internal void txt_NidumiGyoushaCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.CheckNizumiGyoushaCdFrom(e))
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }
                this.txt_NidumiGenbaCd.Clear();
                this.txt_NidumiGenbaMei.Clear();

                //荷積業者CDの値が無で拠点名が有の場合、拠点名をクリアする。
                if (string.IsNullOrEmpty(this.txt_NidumiGyoushaCd.Text))
                {
                    this.txt_NidumiGyoushaMei.Clear();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷積現場
        public void txt_NidumiGenbaCd_PopAfter()
        {
            var value = txt_NidumiGenbaCd as Control;
            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = null;
            }
            else
            {
                _EnterValue.Add(value, null);
            }
        }
        internal void txt_NidumiGenbaCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.CheckNizumiGenbaCdFrom(e))
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }

                // 拠点CDの値が無で拠点名が有の場合、拠点名をクリアする。
                if (string.IsNullOrEmpty(this.txt_NidumiGenbaCd.Text))
                {
                    this.txt_NidumiGenbaMei.Clear();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷降業者
        public void txt_NIOROSHI_GYOUSHA_CD_PopBefore()
        {
            this.previousNioroshiGyoushaCd = this.txt_NIOROSHI_GYOUSHA_CD.Text;
        }
        public void txt_NIOROSHI_GYOUSHA_CD_PopAfter()
        {
            //var value = txt_NIOROSHI_GYOUSHA_CD as Control;
            //if (_EnterValue.ContainsKey(value))
            //{
            //    _EnterValue[value] = null;
            //}
            //else
            //{
            //    _EnterValue.Add(value, null);
            //}

            if (this.previousNioroshiGyoushaCd != this.txt_NIOROSHI_GYOUSHA_CD.Text)
            {
                this.txt_NIOROSHI_GENBA_CD.Clear();
                this.txt_NIOROSHI_GENBA_Mei.Clear();
            }
        }
        /// <summary>
        /// 「荷降業者CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void txt_NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.CheckNioroshiGyoushaCD(e))
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }
                this.txt_NIOROSHI_GENBA_CD.Clear();
                this.txt_NIOROSHI_GENBA_Mei.Clear();

                // 運搬業者CD Fromの値が無で拠点名が有の場合、運搬業者名をクリアする。
                if (string.IsNullOrEmpty(this.txt_NIOROSHI_GYOUSHA_CD.Text))
                {
                    this.txt_NIOROSHI_GYOUSHA_Mei.Clear();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 荷降現場
        public void txt_NIOROSHI_GENBA_CD_PopAfter()
        {
            var value = txt_NIOROSHI_GENBA_CD as Control;
            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = null;
            }
            else
            {
                _EnterValue.Add(value, null);
            }
        }
        /// <summary>
        /// 「荷降現場CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void txt_NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.HannyuusakiDateCheck())
                {
                    e.Cancel = true;
                    return;
                }
                this.logic.checkNioroshiGenba(e);
                // 運搬業者CD Fromの値が無で拠点名が有の場合、運搬業者名をクリアする。
                if (string.IsNullOrEmpty(this.txt_NIOROSHI_GENBA_CD.Text) && !string.IsNullOrEmpty(this.txt_NIOROSHI_GENBA_Mei.Text))
                {
                    this.txt_NIOROSHI_GENBA_Mei.Clear();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 車輌

        /// <summary>EnterかTabボタンが押下されたかどうかの判定フラグ</summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// ※例外として車輌でも使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    this.pressedEnterOrTab = true;
                    break;

                default:
                    this.pressedEnterOrTab = false;
                    break;
            }
        }

        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        private void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            if (this.pressedEnterOrTab)
            {
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.SelectNextControl((Control)control, !isPressShift, true, true, true);
            }

            // マウス操作を考慮するためpressedEnterOrTabを初期化
            pressedEnterOrTab = false;
        }

        public void txt_SHARYOU_CD_PopBefore()
        {
            this.HiddenShayouNm.Text = "";

            // 車種も変更されるため前回値を保持
            this.previousShashuCd = this.txt_ShashuCd.Text;
        }
        public void txt_SHARYOU_CD_PopAfter()
        {
            if (!string.IsNullOrEmpty(this.HiddenShayouNm.Text))
            {
                this.txt_SHARYOU_MEI.Text = this.HiddenShayouNm.Text;

                var value = txt_SHARYOU_CD as Control;
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }
            }

            // 車種が変更されたら再読み込み
            if (this.previousShashuCd != this.txt_ShashuCd.Text)
            {
                // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                this.dgvDetailReload();
            }
        }

        /// <summary>
        /// 「車輌CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void txt_SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            this.MoveToNextControlForShokuchikbnCheck(this.txt_SHARYOU_CD);
            if (!this.txt_SHARYOU_CD.AutoChangeBackColorEnabled)
            {
                this.txt_SHARYOU_CD.BackColor = this.logic.sharyouCdBackColor;
            }
        }

        /// <summary>
        /// 「車輌CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void txt_SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!isChanged(sender)) { return; }
            try
            {
                if (!this.logic.CheckSharyou(e)) { return; }
                //車輌CDの値が無で拠点名が有の場合、拠点名をクリアする。
                if (string.IsNullOrEmpty(this.txt_SHARYOU_CD.Text) && !string.IsNullOrEmpty(this.txt_SHARYOU_MEI.Text))
                {
                    this.txt_SHARYOU_MEI.Clear();
                }

                // 車種が変更されたら車種明細欄を再読み込み
                if (this.previousShashuCd != this.txt_ShashuCd.Text)
                {
                    // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                    this.dgvDetailReload();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 車種

        public void txt_ShashuCd_PopBefore()
        {
            this.previousShashuCd = this.txt_ShashuCd.Text;
        }
        public void txt_ShashuCd_PopAfter()
        {
            if (this.previousShashuCd != this.txt_ShashuCd.Text)
            {
                // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
                this.dgvDetailReload();
            }
        }
        /// <summary>
        /// 「車種CD」ロストフォーカス時のイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void txt_ShashuCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
            this.dgvDetailReload();
        }
        #endregion

        #region event
        private bool errFlg = false;
        public void UNCHIN_HINMEI_CD_PopAfter()
        {
            //valuesForDetail["UNCHIN_HINMEI_CD"] = Convert.ToString(this.dgvDetail["UNCHIN_HINMEI_CD", this.dgvDetail.CurrentRow.Index].Value);
            if (!this.logic.CheckHinmeiCd(this.dgvDetail.CurrentRow.Index)) { return; }
            //単価設定
            if (!this.logic.CalcTanka(this.dgvDetail.CurrentRow)) { return; }
            //正味合計、消費税、合計金額編集
            //this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow);
            //this.logic.CalcDetailGoukeikingaku();
        }
        internal void dgvDetail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                #region Cell変更チェック
                // 前回値と変更が無かったら処理中断
                String cellname = this.dgvDetail.Columns[e.ColumnIndex].Name;
                if (cellname == "UNCHIN_HINMEI_CD")
                {
                    this.dgvDetail[cellname, e.RowIndex].Value = Convert.ToString(this.dgvDetail[cellname, e.RowIndex].Value).ToUpper();
                    this.dgvDetail.RefreshEdit();
                }
                String cellvalue = Convert.ToString(this.dgvDetail[cellname, e.RowIndex].Value);
                if (cellname == "UNCHIN_HINMEI_CD")
                {
                    if (!cellvalue.Equals(""))
                    {
                        cellvalue = cellvalue.PadLeft(6, '0');
                        this.dgvDetail[cellname, e.RowIndex].Value = cellvalue;
                    }
                }
                else if (cellname == "UNIT_CD")
                {
                    if (!cellvalue.Equals(""))
                    {
                        this.dgvDetail[cellname, e.RowIndex].Value = cellvalue;
                    }
                }
                if (valuesForDetail.ContainsKey(cellname)
                    && valuesForDetail[cellname].Equals(cellvalue))
                {
                    return;
                }
                #endregion

                // 選択行番号取得
                int intRowNo = this.dgvDetail.CurrentRow.Index;

                switch (cellname)
                {
                    case "UNCHIN_HINMEI_CD":

                        // 品名チェック
                        if (this.logic.CheckHinmeiCd(e.RowIndex))
                        {
                            e.Cancel = true;
                            errFlg = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["UNCHIN_HINMEI_CD"].EditedFormattedValue.ToString()))
                        {
                            // 運賃品名がクリアされたら単価と金額もクリアする
                            this.dgvDetail.Rows[e.RowIndex].Cells["TANKA"].Value = null;
                            this.dgvDetail.Rows[e.RowIndex].Cells["KINGAKU"].Value = null;
                        }
                        //if (string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].EditedFormattedValue.ToString()))
                        //{
                        //    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = 0;

                        //}
                        //if (string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue.ToString()))
                        //{
                        //    this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].Value = 0;
                        //}
                        decimal? j = this.logic.ToNDecimal(this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue);
                        switch (Convert.ToString(this.dgvDetail.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value))
                        {
                            case "ｔ":
                                if (j != null)
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(j.Value / 1000m, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                    if (catchErr) { return; }
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                }
                                else
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                }
                                break;
                            case "kg":
                                if (j != null)
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(j.Value, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                    if (catchErr) { return; }
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                }
                                else
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                }
                                break;
                            default:
                                this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                break;
                        }
                        //単価設定
                        if (!this.logic.CalcTanka(this.dgvDetail.CurrentRow)) { return; }
                        //正味合計、消費税、合計金額編集
                        if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                        if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                        break;

                    case "UNIT_CD":
                        this.logic.checkUnit(e);
                        if (e.Cancel)
                        {
                            errFlg = true;
                            return;
                        }
                        if (!this.logic.CalcTanka(this.dgvDetail.CurrentRow)) { return; }
                        decimal? n = this.logic.ToNDecimal(this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue);
                        switch (Convert.ToString(this.dgvDetail.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value))
                        {
                            case "ｔ":
                                if (n != null)
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(n.Value / 1000m, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                    if (catchErr) { return; }
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                }
                                else
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                }
                                //正味合計、消費税、合計金額編集
                                if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                                if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                                break;
                            case "kg":
                                if (n != null)
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(n.Value, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                    if (catchErr) { return; }
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                }
                                else
                                {
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                }
                                //正味合計、消費税、合計金額編集
                                if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                                if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                                break;
                            default:
                                //正味合計、消費税、合計金額編集
                                if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                                if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                                this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                break;
                        }
                        break;

                    case "NET_JYUURYOU":
                        if (string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue.ToString()))
                        {
                            this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].Value = "";
                            this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                        }
                        else
                        {
                            decimal? d = this.logic.ToNDecimal(this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue);
                            this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].Value = this.dgvDetail.Rows[e.RowIndex].Cells["NET_JYUURYOU"].EditedFormattedValue;
                            switch (Convert.ToString(this.dgvDetail.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value))
                            {
                                case "ｔ":
                                    if (d != null)
                                    {
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(d.Value / 1000m, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                        if (catchErr) { return; }
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                    }
                                    //正味合計、消費税、合計金額編集
                                    if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                                    if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                                    break;
                                case "kg":
                                    if (d != null)
                                    {
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(d.Value, SystemProperty.Format.Suuryou ?? "", out catchErr);
                                        if (catchErr) { return; }
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                    }
                                    //正味合計、消費税、合計金額編集
                                    if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                                    if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                                    break;
                                default:
                                    this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].ReadOnly = false;
                                    break;
                            }
                        }
                        if (!this.logic.KingakuTotal()) { return; }
                        break;

                    case "SUURYOU":
                        if (!string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].EditedFormattedValue.ToString()))
                        {
                            this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].Value = this.dgvDetail.Rows[e.RowIndex].Cells["SUURYOU"].EditedFormattedValue;
                        }
                        //正味合計、消費税、合計金額編集
                        if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                        if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                        break;

                    case "TANKA":
                        //if (string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["TANKA"].EditedFormattedValue.ToString()))
                        //{
                        //    this.dgvDetail.Rows[e.RowIndex].Cells["TANKA"].Value = "";
                        //}
                        //else
                        //{
                        //    this.dgvDetail.Rows[e.RowIndex].Cells["TANKA"].Value = this.dgvDetail.Rows[e.RowIndex].Cells["TANKA"].EditedFormattedValue;
                        //}
                        //正味合計、消費税、合計金額編集
                        if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow)) { return; }
                        if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                        break;

                    case "KINGAKU":
                        //if (!string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells["KINGAKU"].EditedFormattedValue.ToString()))
                        //{
                        //    this.dgvDetail.Rows[e.RowIndex].Cells["KINGAKU"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(this.dgvDetail.Rows[e.RowIndex].Cells["KINGAKU"].EditedFormattedValue));
                        //}
                        if (!this.logic.CalcDetailGoukeikingaku()) { return; }
                        break;

                    default:
                        break;
                }
                this.dgvDetail_CellDefaultValue(e.ColumnIndex, e.RowIndex);

                // 単価と金額の活性/非活性制御
                this.logic.SetDetailReadOnly(e.RowIndex);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// データグリッド値変更チェック
        /// </summary>
        private void dgvDetail_CellDefaultValue(int columnIndex, int rowIndex)
        {
            LogUtility.DebugMethodStart(columnIndex, rowIndex);
            try
            {
                String cellname = this.dgvDetail.Columns[columnIndex].Name;
                String cellvalue = Convert.ToString(this.dgvDetail[cellname, rowIndex].Value);
                if (cellname == "HINMEI_CD")
                {
                    if (!cellvalue.Equals(""))
                    {
                        cellvalue = cellvalue.PadLeft(6, '0');
                        this.dgvDetail[cellname, rowIndex].Value = cellvalue;
                    }
                }
                else if (cellname == "UNIT_CD")
                {
                    this.dgvDetail[cellname, rowIndex].Value = cellvalue;
                }
                if (valuesForDetail.ContainsKey(cellname))
                {

                    valuesForDetail[cellname] = cellvalue;
                }
                else
                {
                    valuesForDetail.Add(cellname, cellvalue);
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 明細のRowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dgvDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.NumberingDetailRowNo();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細のRowsRemovedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dgvDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.NumberingDetailRowNo();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細のCellPaintingイベント
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        internal void dgvDetail_CellPainting(object sender, System.Windows.Forms.DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                // ヘッダー以外は処理なし
                return;
            }
            int colIndex = 5;
            if (e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1)
            {
                // セルの矩形を取得
                Rectangle rect = e.CellBounds;

                DataGridView dgv = (DataGridView)sender;

                // 1列目の場合
                if (e.ColumnIndex == colIndex)
                {
                    // 2列目の幅を取得して、1列目の幅に足す
                    rect.Width += dgv.Columns[colIndex + 1].Width;
                    rect.Y = e.CellBounds.Y + 1;
                }
                else
                {
                    // 1列目の幅を取得して、2列目の幅に足す
                    rect.Width += dgv.Columns[colIndex].Width;
                    rect.Y = e.CellBounds.Y + 1;

                    // Leftを1列目に合わせる
                    rect.X -= dgv.Columns[colIndex].Width;
                }
                // 背景、枠線、セルの値を描画
                using (SolidBrush brush = new SolidBrush(dgv.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    // 背景の描画
                    e.Graphics.FillRectangle(brush, rect);

                    using (Pen pen = new Pen(dgv.GridColor))
                    {
                        // 枠線の描画
                        e.Graphics.DrawRectangle(pen, rect);
                    }

                    using (Pen pen1 = new Pen(Color.DarkGray))
                    {
                        // 直線を描画(ヘッダ上部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                        // 直線を描画(ヘッダ下部)
                        e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }
                }

                // セルに表示するテキストを描画
                TextRenderer.DrawText(e.Graphics,
                                "単位",
                                e.CellStyle.Font,
                                rect,
                                e.CellStyle.ForeColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            }

            // 結合セル以外は既定の描画を行う
            if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1))
            {
                e.Paint(e.ClipBounds, e.PaintParts);
            }

            // イベントハンドラ内で処理を行ったことを通知
            e.Handled = true;
        }

        /// <summary>
        /// 明細のCellPaintingイベント
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        internal void dgvDetail_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.dgvDetail.Refresh();
        }

        /// <summary>
        /// 明細のSizeChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dgvDetail_SizeChanged(object sender, EventArgs e)
        {
            this.label22.Location = new Point(this.label22.Location.X, this.dgvDetail.Location.Y + this.dgvDetail.Size.Height + 12);
            this.label24.Location = new Point(this.label24.Location.X, this.dgvDetail.Location.Y + this.dgvDetail.Size.Height + 12);
            this.txt_KingakuTotal.Location = new Point(this.txt_KingakuTotal.Location.X, this.dgvDetail.Location.Y + this.dgvDetail.Size.Height + 32);
            this.txt_Goukeikingaku.Location = new Point(this.txt_Goukeikingaku.Location.X, this.dgvDetail.Location.Y + this.dgvDetail.Size.Height + 32);
        }
        #endregion

        /// <summary>
        /// 車輌名フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHARYOU_NAME_RYAKU_Enter(object sender, EventArgs e)
        {
            this.isSelectingSharyouCd = false;
        }

        #region 各CELLのフォーカス取得時処理
        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dgvDetail_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // IMEMode制御
            dgvDetail.ImeMode = ImeMode.Off;
            // 品名、明細備考入力場合，ひらがな
            if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("HINMEI_NAME") ||
                this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("MEISAI_BIKOU"))
            {
                dgvDetail.ImeMode = ImeMode.Hiragana;
            }
            else
            {
                dgvDetail.ImeMode = ImeMode.Disable;
            }

            DataGridViewRow row = this.dgvDetail.CurrentRow;

            if (row == null)
            {
                return;
            }

            // 前回値チェック用データをセット
            String cellname = this.dgvDetail.Columns[e.ColumnIndex].Name;
            String cellvalue = Convert.ToString(this.dgvDetail[cellname, e.RowIndex].Value);
            if (!this.errFlg)
            {
                if (this.valuesForDetail.ContainsKey(cellname))
                {
                    this.valuesForDetail[cellname] = cellvalue;
                }
                else
                {
                    this.valuesForDetail.Add(cellname, cellvalue);
                }
            }
            else
            {
                this.errFlg = false;
            }

            LogUtility.DebugMethodEnd();

        }
        #endregion

        /// <summary>
        /// 登録時の自動実行処理
        /// </summary>
        internal void C_Regist(object sender, EventArgs e)
        {
            if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG) { return; }
            var button = sender as CustomButton;

            var bakErrorFlag = this.RegistErrorFlag;

            button.Focus();

            if (this.ActiveControl != this.FoucusControl)
            {
                this.FoucusControl.Focus();

                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.controlUtil.GetAllControls(this.logic.headerForm));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());

                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                if (this.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.logic.SetErrorFocus();
                }
            }
            else
            {
                this.FoucusControl.Focus();

                this.RegistErrorFlag = true;

                this.FocusOutErrorFlag = false;
            }

        }

        /// <summary>
        /// コンストラクタで渡された受入番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistData()
        {
            return this.logic.IsExistData(this.Renkei_Number.Value);
        }

        /// <summary>
        /// 明細の単価･金額･合計金額を再読み込みします
        /// </summary>
        /// <returns></returns>
        internal void dgvDetailReload()
        {
            // 明細欄の単価を全て再読み込み ＆ 金額を全て再計算
            this.dgvDetail.Rows.Cast<DataGridViewRow>()
                .Where(w => !w.IsNewRow).ToList()
                .ForEach(r =>
                {
                    this.logic.CalcTanka(r);
                    this.logic.CalcDetailKingaku(r);
                });

            // 合計金額再計算
            this.logic.CalcDetailGoukeikingaku();
        }


        #endregion

        /// <summary>
        /// 車両CDのENTERイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 自身の前回値はEnterEventInitによって設定されているイベントで取れているが、
            // 車種の前回値もほしいためここで設定
            this.previousShashuCd = this.txt_ShashuCd.Text;
        }
    }
}
