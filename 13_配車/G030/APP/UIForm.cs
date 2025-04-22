// $Id: UIForm.cs 55042 2015-07-08 06:56:26Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using System.Drawing;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    /// <summary>
    /// 定期配車入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeader header_new;

        /// <summary>
        /// 定期配車入力画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        private bool nowLoding = false;

        /// <summary>
        /// 変更前の配車番号（配車番号変更後処理用）
        /// </summary>
        public string bakTeikiHaishaNumber = string.Empty;

        // No.2840-->
        /// <summary>
        /// 配車割当からの車両情報
        /// </summary>
        public string[] insHaisyaList { get; set; }
        // No.2840<--
        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();


        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        /// <summary>
        /// [2]ｼｮｰﾄﾒｯｾｰｼﾞ押下フラグ
        /// </summary>
        private bool smsFlg = false;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報
        /// </summary>
        string[] paramList;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト
        /// </summary>
        List<int> smsReceiverList;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="paramIn_TeikiHaishaNumber">定期配車番号</param>
        /// <param name="slist">車両情報</param>
        public UIForm(WINDOW_TYPE windowType, UIHeader headerForm, String paramIn_TeikiHaishaNumber, string[] slist = null)
            : base(WINDOW_ID.T_TEIKI_HAISHA, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, headerForm, paramIn_TeikiHaishaNumber, slist);

                this.InitializeComponent();

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                // 時間コンボボックスのItemsをセット
                this.SAGYOU_BEGIN_HOUR.SetItems();
                this.SAGYOU_BEGIN_MINUTE.SetItems(5);
                this.SAGYOU_END_HOUR.SetItems();
                this.SAGYOU_END_MINUTE.SetItems(5);

                this.insHaisyaList = slist;   // No.2840

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                this.logic.SetHeader(header_new);
                this.logic.teikiHaishaNumber = paramIn_TeikiHaishaNumber;

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);

                this.UNTENSHA_CD.FocusOutCheckMethod.Add(new r_framework.Dto.SelectCheckDto()
                {
                    CheckMethodName = "社員マスタコードチェックandセッティング"
                });
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンストラクタ（参照モード用）
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="paramIn_sagyoubi">作業日</param>
        /// <param name="paramIn_courseNameCd">コース名称CD</param>
        /// <param name="paramIn_TeikiHaishaNumber">定期配車番号</param>
        /// <param name="slist">車両情報</param>
        public UIForm(WINDOW_TYPE windowType, UIHeader headerForm, DateTime paramIn_sagyoubi, String paramIn_courseNameCd, String paramIn_TeikiHaishaNumber, string[] slist = null, string paramIn_FurikaeKbn = null, string paramIn_DayCd = null)
            : base(WINDOW_ID.T_TEIKI_HAISHA, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, headerForm, paramIn_sagyoubi, paramIn_courseNameCd, paramIn_TeikiHaishaNumber, slist, paramIn_FurikaeKbn, paramIn_DayCd);

                this.InitializeComponent();

                // 時間コンボボックスのItemsをセット
                this.SAGYOU_BEGIN_HOUR.SetItems();
                this.SAGYOU_BEGIN_MINUTE.SetItems(5);
                this.SAGYOU_END_HOUR.SetItems();
                this.SAGYOU_END_MINUTE.SetItems(5);

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                this.insHaisyaList = slist;   // No.2840

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
                this.logic.SetHeader(header_new);

                // ロジックの曜日CD、コース名称CDに設定する
                this.logic.sagyoubi = paramIn_sagyoubi;
                if (paramIn_FurikaeKbn != "2")
                {
                    this.logic.dayCd = Shougun.Core.Common.BusinessCommon.Utility.DateUtility.GetShougunDayOfWeek(paramIn_sagyoubi).ToString();
                }
                else
                {
                    this.logic.dayCd = paramIn_DayCd;
                }
                this.logic.courseNameCd = paramIn_courseNameCd;

                if (!string.IsNullOrEmpty(paramIn_TeikiHaishaNumber))
                {
                    this.logic.teikiHaishaNumber = paramIn_TeikiHaishaNumber;
                }

                this.logic.furikaeKbn = paramIn_FurikaeKbn;
                if (paramIn_FurikaeKbn != null)
                {
                    this.insHaisyaList = null;
                }

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw ex;
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
                if (!this.logic.WindowInit(WindowType)) { return; }

                // すべてのコントロールを返す
                this.allControl = this.GetAllControl();

                // 変更前の配車番号を保存する
                this.bakTeikiHaishaNumber = this.TEIKI_HAISHA_NUMBER.Text;

                // Anchorの設定はOnLoadで行う
                if (this.NioroshiIchiran != null)
                {
                    this.NioroshiIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                }
                if (this.DetailIchiran != null)
                {
                    this.DetailIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        /// <summary>
        /// UIForm, ヘッダフォームのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(this.header_new));

            return allControl.ToArray();
        }
        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// F2【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 処理モード変更
                this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                // 初期化
                this.logic.tourokuZumiNioroshiList = new List<string>();
                // 配車番号
                this.TEIKI_HAISHA_NUMBER.Text = string.Empty;
                this.logic.teikiHaishaNumber = string.Empty;
                this.bakTeikiHaishaNumber = string.Empty;
                // 画面項目初期化【新規】モード
                if (!this.logic.ModeInit(WINDOW_TYPE.NEW_WINDOW_FLAG)) { return; }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F3【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    this.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                    // 画面項目初期化【修正】モード
                    if (!this.logic.WindowInitUpdate()) { return; }
                }
                else if (r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    this.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                    // 画面項目初期化【参照】モード
                    if (!this.logic.WindowInitReference()) { return; }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F4 上処理（カーソル行の明細を１行上の明細と入れ替える）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void rowUp(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カーソルCell
                DataGridViewCell currentCell = this.DetailIchiran.CurrentCell;
                // 選択行がない場合、何もしない
                if (currentCell == null)
                {
                    return;
                }

                // カーソル行
                int currentRowIndex = currentCell.RowIndex;
                DataGridViewRow currentRow = this.DetailIchiran.Rows[currentRowIndex];
                // カーソル行が１行目、末尾行の場合、何もしない
                if (currentRowIndex == 0 || currentRowIndex == this.DetailIchiran.Rows.Count - 1)
                {
                    return;
                }

                if (!this.logic.RenkeiCheck(true))
                {
                    return;
                }

                // コピー行を１行上に追加
                this.DetailIchiran.BeginEdit(false);
                this.DetailIchiran.Rows.InsertCopy(currentRowIndex, currentRowIndex - 1);
                // コピー行を作成
                DataGridViewRow newRow = this.DetailIchiran.Rows[currentRowIndex - 1];
                for (int colIndex = 0; colIndex < this.DetailIchiran.Columns.Count; colIndex++)
                {
                    if (colIndex == 2)
                    {
                        // No列再設定
                        newRow.Cells[colIndex].Value = currentRowIndex;
                    }
                    else
                    {
                        // No列以外の場合、そのままコピー
                        newRow.Cells[colIndex].Value = currentRow.Cells[colIndex].Value;
                    }
                }

                // カーソル行を削除
                this.DetailIchiran.Rows.RemoveAt(this.DetailIchiran.CurrentCell.RowIndex);

                // 元カーソルCellにフォーカス
                currentCell = this.DetailIchiran.Rows[currentRowIndex - 1].Cells[currentCell.ColumnIndex];
                currentCell.Selected = true;

                // No列再設定（元カーソル行の上一行）
                this.DetailIchiran.Rows[currentRowIndex].Cells[ConstCls.DetailColName.NO].Value = currentRowIndex + 1;

                this.DetailIchiran.EndEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("rowUp", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F5 下処理（カーソル行の明細を１行下の明細と入れ替える）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void rowDown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カーソルCell
                DataGridViewCell currentCell = this.DetailIchiran.CurrentCell;
                // 選択行がない場合、何もしない
                if (currentCell == null)
                {
                    return;
                }

                // カーソル行
                int currentRowIndex = currentCell.RowIndex;
                DataGridViewRow currentRow = this.DetailIchiran.Rows[currentRowIndex];
                // カーソル行が末尾２行の場合、何もしない
                if (currentRowIndex == this.DetailIchiran.NewRowIndex - 1 || currentRowIndex == this.DetailIchiran.NewRowIndex)
                {
                    return;
                }

                if (!this.logic.RenkeiCheck(false))
                {
                    return;
                }

                // コピー行を１行上に追加
                this.DetailIchiran.BeginEdit(false);
                this.DetailIchiran.Rows.InsertCopy(currentRowIndex, currentRowIndex + 2);
                // コピー行を作成
                DataGridViewRow newRow = this.DetailIchiran.Rows[currentRowIndex + 2];
                for (int colIndex = 0; colIndex < this.DetailIchiran.Columns.Count; colIndex++)
                {
                    if (colIndex == 2)
                    {
                        // No列再設定
                        newRow.Cells[colIndex].Value = currentRowIndex + 2;
                    }
                    else
                    {
                        // No列以外の場合、そのままコピー
                        newRow.Cells[colIndex].Value = currentRow.Cells[colIndex].Value;
                    }
                }

                //// コピー行を追加
                //this.DetailIchiran.Rows.InsertRange(currentRowIndex + 2, newRow);
                // カーソル行を削除
                this.DetailIchiran.Rows.RemoveAt(currentRowIndex);

                // 元カーソルCellにフォーカス
                if (currentRowIndex + 1 != this.DetailIchiran.NewRowIndex)
                {
                    this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[currentRowIndex + 1].Cells[currentCell.ColumnIndex];
                    this.DetailIchiran.CurrentCell.Selected = true;
                }

                // No列再設定（元カーソル行の上一行）
                this.DetailIchiran.Rows[currentRowIndex].Cells[ConstCls.DetailColName.NO].Value = currentRowIndex + 1;

                this.DetailIchiran.EndEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("rowDown", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 一覧（定期配車一覧画面を表示する）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                FormManager.OpenFormWithAuth("G032", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("reLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8【順番整列】明細行が順番の並びで並び替わります
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SortIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 明細行が空行１行の場合
                if (!this.CheckRequiredDataForDeital())
                {
                    return;
                }

                // 順番の必須チェック
                bool isErrorFlag = false;
                bool isAlertFlag = false;
                var JunbanL = new List<int>();

                for (int i = 0; i < this.DetailIchiran.Rows.Count - 1; i++)
                {
                    DataGridViewRow row = this.DetailIchiran.Rows[i];

                    //ReadOnlyの明細がある時だけ、アラートを出す
                    if (row.Cells[ConstCls.DetailColName.JUNNBANN].ReadOnly)
                    {
                        isAlertFlag = true;
                    }

                    //順番が未入力の行があるかチェック
                    if (string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.JUNNBANN].FormattedValue.ToString()))
                    {
                        isErrorFlag = true;
                    }
                    else
                    {
                        //順番をリストに収納
                        JunbanL.Add(int.Parse(row.Cells[ConstCls.DetailColName.JUNNBANN].FormattedValue.ToString()));
                    }
                }

                // 順番の重複チェック
                if (!isErrorFlag && isAlertFlag) 
                {
                    var duplicates = JunbanL.GroupBy(name => name).Where(name => name.Count() > 1)
                        .Select(group => group.Key).ToList();
                    if (duplicates.Count > 0)
                    {
                        isErrorFlag = true;
                    }
                }
                // 順番の欠番チェック
                if (!isErrorFlag && isAlertFlag)
                {
                    if (JunbanL.Count != JunbanL.Max())
                    {
                        isErrorFlag = true;
                    }
                }

                if (isErrorFlag)
                {
                    if (isAlertFlag)
                    {
                        MessageBox.Show("順番が不正です。\r\n\r\n順番の欠番、重複を確認してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //コース明細が表示されていない場合順番確定処理を行わない。
                    if (this.DetailIchiran.Rows.Count > 0)
                    {
                        // 順番を採番
                        for (int j = 0; j < this.DetailIchiran.Rows.Count - 1; j++)
                        {
                            this.DetailIchiran.Rows[j].Cells[ConstCls.DetailColName.JUNNBANN].Value = Decimal.Parse((j + 1).ToString());
                        }
                    }
                }

                // 明細リストを「順番」の昇順に並びかえる
                this.DetailIchiran.BeginEdit(false);
                this.DetailIchiran.Sort(this.DetailIchiran.Columns[ConstCls.DetailColName.JUNNBANN], System.ComponentModel.ListSortDirection.Ascending);

                // NO列を採番します                    
                for (int j = 0; j < this.DetailIchiran.Rows.Count - 1; j++)
                {
                    this.DetailIchiran[ConstCls.DetailColName.NO, j].Value = j + 1;
                }
                this.DetailIchiran.EndEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SortIchiran", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 登録処理（入力内容を登録します）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool MobieRegistCheck = false;

                if (!base.RegistErrorFlag)
                {
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                            // 登録時の入力チェックを行う
                            if (!RegistCheck())
                            {
                                return;
                            }
                            if (!this.logic.RenkeiCheckForLogi())
                            {
                                return;
                            }
                            // コース最適化(NAVITIME連携)がONの場合のみ
                            if (AppConfig.AppOptions.IsNAVITIME())
                            {
                                if (!this.logic.RenkeiCheckForNavi())
                                {
                                    return;
                                }
                            }
                            //モバイル連携事前チェック
                            if (this.logic.is_mobile)
                            {
                                if (!this.logic.MobileRegistCheck_pre())
                                {
                                    this.logic.MsgBox.MessageBoxShowError("モバイル連携が可能な条件ではない為\r\n登録処理を実行出来ません。"
                                        + "\r\n\r\nモバイル連携を解除するか、データを確認の上\r\n再度実行してください。");
                                    return;
                                }
                            }

                            //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
                            if (AppConfig.AppOptions.IsInxsUketsuke())
                            {
                                if (!this.logic.RenkeiCheckForConfirmDateInxs())
                                {
                                    return;
                                }
                            }
                            //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END
                            break;
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            if (!this.logic.RenkeiCheckForLogi())
                            {
                                return;
                            }
                            // コース最適化(NAVITIME連携)がONの場合のみ
                            if (AppConfig.AppOptions.IsNAVITIME())
                            {
                                if (!this.logic.RenkeiCheckForNavi())
                                {
                                    return;
                                }
                            }
                            break;

                        default:
                            break;
                    }

                    // 登録用データの作成
                    if (!this.logic.CreateEntity(base.WindowType)) { return; }

                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.Regist(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "登録");
                            }

                            if (this.logic.MobileRegistCheck())
                            {
                                //モバイルデータ登録チェック→モバイルデータ登録
                                MobieRegistCheck = this.logic.MobileRegist();
                                if (MobieRegistCheck)
                                {
                                    msgLogic.MessageBoxShow("I001", "連携");
                                }
                                else
                                {
                                    //msgLogic.MessageBoxShow("I007", "データの連携");
                                    msgLogic.MessageBoxShowWarn("モバイル将軍への連携処理に失敗しました。\r\n再度実行してください。");
                                }
                            }
                            // [4]ｼｮｰﾄﾒｯｾｰｼﾞで呼び出された場合、クリア前に情報取得
                            if (smsFlg)
                            {
                                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
                                paramList = this.logic.SmsParamListSetting();

                                smsReceiverList = this.logic.SmsReceiverListSetting();
                            }

                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "更新");
                            }

                            if (this.logic.MobileRegistCheck())
                            {
                                //モバイルデータ登録チェック→モバイルデータ登録
                                MobieRegistCheck = this.logic.MobileRegist();
                                if (MobieRegistCheck)
                                {
                                    msgLogic.MessageBoxShow("I001", "連携");
                                }
                                else
                                {
                                    //msgLogic.MessageBoxShow("I007", "データの連携");
                                    msgLogic.MessageBoxShowWarn("モバイル将軍への連携処理に失敗しました。\r\n再度実行してください。");

                                }
                            }
                            // [4]ｼｮｰﾄﾒｯｾｰｼﾞで呼び出された場合、クリア前に情報取得
                            if (smsFlg)
                            {
                                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
                                paramList = this.logic.SmsParamListSetting();

                                smsReceiverList = this.logic.SmsReceiverListSetting();
                            }
                            break;

                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            if (!this.logic.RenkeiCheckForDelete())
                            {
                                return;
                            }
                            this.logic.LogicalDelete();
                            if (this.logic.isRegist)
                            {
                                msgLogic.MessageBoxShow("I001", "削除");
                            }
                            break;

                        default:
                            break;
                    }

                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // DB更新後、新規モードで初期化する
                        this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                        // 配車番号をクリア
                        this.TEIKI_HAISHA_NUMBER.Text = string.Empty;
                        this.logic.teikiHaishaNumber = string.Empty;
                        this.bakTeikiHaishaNumber = string.Empty;

                        // 画面項目初期化
                        if (!this.logic.ModeInit(WINDOW_TYPE.NEW_WINDOW_FLAG)) { return; }
                        this.logic.isRegist = false;
                        this.TEIKI_HAISHA_NUMBER.Focus();
                    }
                    else
                    {
                        // 新規権限が無い場合は画面を閉じる
                        this.FormClose(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F12 Formクローズ処理
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
                LogUtility.Error("FormClose", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 荷降行削除ボタン処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void DeleteNioroshiRow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (null != this.NioroshiIchiran.CurrentRow
                && !this.NioroshiIchiran.CurrentRow.IsNewRow
                && this.logic.canNioroshiRemove(this.NioroshiIchiran.CurrentRow))
            {
                string nioroshiNumber = Convert.ToString(this.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                if (!this.logic.CheckNioroshiNumber(nioroshiNumber))
                {
                    this.NioroshiIchiran.Rows.Remove(this.NioroshiIchiran.CurrentRow);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降No一括入力ボタン処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void NioroshiIkkatsu(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ShowNioroshiIkkatsu();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F10] 地図表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MapOpen(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.COURSE_NAME_CD.Text))
                {
                    this.logic.MsgBox.MessageBoxShowError("コースを入力してください。");
                    return;
                }

                if (this.DetailIchiran.Rows.Count <= 1)
                {
                    this.logic.MsgBox.MessageBoxShowError("地図表示対象の明細がありません。");
                    return;
                }

                if (this.logic.MsgBox.MessageBoxShowConfirm("地図を表示しますか？" +
                    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.logic.createMapboxDto();

                int cnt = 0;
                foreach (mapDtoList item in dtos)
                {
                    if (item.dtos.Where(a => a.latitude != "").Count() > 0) { cnt++; }
                }

                if (cnt == 0)
                {
                    this.logic.MsgBox.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_TEIKI_HAISHA);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SmsNyuuryoku(object sender, EventArgs e)
        {
            try
            {
                if (!base.RegistErrorFlag)
                {
                    if (this.DetailIchiran.CurrentRow == null) 
                    {
                        this.logic.MsgBox.MessageBoxShowError("対象の明細がありません。");
                        return; 
                    }

                    // 作業日の必須チェック
                    if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                    {
                        this.SAGYOU_DATE.BackColor = Constans.ERROR_COLOR;
                        this.logic.MsgBox.MessageBoxShow("E001", "作業日");
                        this.SAGYOU_DATE.Focus();
                        return;
                    }

                    // 現場のチェック
                    if (this.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value == null || string.IsNullOrEmpty(this.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value.ToString()))
                    {
                        this.logic.MsgBox.MessageBoxShowError("業者CD、現場CDを入力している明細行を選択してください。");
                        return;
                    }

                    // ｼｮｰﾄﾒｯｾｰｼﾞ受信者チェック
                    var dao = this.logic.smsReceiverLinkGenbaDao.CheckDataForSmsNyuuryoku(this.DetailIchiran.CurrentRow.Cells["GYOUSHA_CD"].Value.ToString(), this.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value.ToString());
                    if(dao == null)
                    {
                        this.logic.MsgBox.MessageBoxShowError("現場入力（マスタ）に受信者情報が登録されていません。\r\n受信者情報を登録してください。");
                        return;
                    }
                    else
                    {
                        // チェック完了後、[2]押下フラグをtrueに変更
                        smsFlg = true;

                        // 登録処理
                        switch (this.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 確認メッセージ表示
                                if (this.logic.MsgBox.MessageBoxShowConfirm("定期配車伝票を登録し、ショートメッセージ入力画面を表示します") == DialogResult.No)
                                {
                                    return;
                                }
                                break;

                            default:
                                break;
                        }
                        // データ登録（[F9]登録処理）
                        this.Regist(sender, e);
                    }

                    // 不具合等でｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報が存在しない場合は、エラー表示
                    if (smsReceiverList == null || paramList == null)
                    {
                        this.logic.MsgBox.MessageBoxShowError("ｼｮｰﾄﾒｯｾｰｼﾞ入力への連携処理に失敗しました。");
                        return;
                    }

                    // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面を起動
                    FormManager.OpenForm("G767", smsReceiverList, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_ID.T_TEIKI_HAISHA, paramList);
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // [2]押下フラグの初期化
                smsFlg = false;
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 再読込ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void reLoad(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                base.OnLoad(e);
                this.logic.WindowInit(WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("reLoad", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ウインドウタイプ設定処理
        /// </summary>
        /// <param name="type"></param>
        public void SetWindowType(WINDOW_TYPE type)
        {
            try
            {
                LogUtility.DebugMethodStart(type);

                base.WindowType = type;
                base.OnLoad(new EventArgs());
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowType", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録時の入力チェックを行う
        /// </summary>
        /// <returns>true: エラーなし, false: エラーあり</returns>
        private bool RegistCheck()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool catchErr = false;

                // ①修正モード：配車番号必須チェック
                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(this.WindowType))
                {
                    if (string.IsNullOrEmpty(this.TEIKI_HAISHA_NUMBER.Text))
                    {
                        // アラートを表示し処理を中断
                        msgLogic.MessageBoxShow("E034", "配車番号");
                        this.TEIKI_HAISHA_NUMBER.Focus();
                        return returnVal;
                    }
                }

                // ②コースマスタの存在チェック（拠点）
                if (!this.logic.CheckCourseNameExsit1(out catchErr))
                {
                    if (!catchErr)
                    {
                        // アラートを表示し処理を中断
                        msgLogic.MessageBoxShow("E062", "コースCDは拠点");
                    }
                    this.COURSE_NAME_CD.Focus();
                    return returnVal;
                }

                // ③コースマスタの存在チェック（作業日）
                if (!this.logic.CheckCourseNameExsit2(out catchErr))
                {
                    if (!catchErr)
                    {
                        // アラートを表示し処理を中断
                        msgLogic.MessageBoxShow("E062", "コースCDは作業日の曜日");
                    }
                    this.SAGYOU_DATE.Focus();
                    return returnVal;
                }

                // ④車輌CD（＆車種CD）存在チェック
                if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    if (!this.logic.CheckSharyouExsit(out catchErr))
                    {
                        if (!catchErr)
                        {
                            // アラートを表示し処理を中断
                            msgLogic.MessageBoxShow("E020", "車輌");
                        }
                        // 車輌名をクリア
                        this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                        // 車輌CDにフォーカス
                        this.SHARYOU_CD.Focus();
                        return returnVal;
                    }
                }

                // ⑤作業時間の入力チェック
                if (!this.logic.CheckSagyouTime())
                {
                    return returnVal;
                }

                // ⑥明細行が空行１行の場合
                if (!this.CheckRequiredDataForDeital() || IsAllDeleteChecked())
                {
                    // アラートを表示し処理を中断                    
                    msgLogic.MessageBoxShow("E061");
                    return returnVal;
                }

                // ⑦明細部の入力チェックNGの場合(新規の場合のみ)
                if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    if (!this.logic.IsInputCheckOK())
                    {
                        return returnVal;
                    }
                }

                // ⑧荷降一覧と詳細にある荷卸Noの互換性をチェック
                string nioroshiNumber = string.Empty;
                if (!this.logic.CheckNioroshiNo(out catchErr, out nioroshiNumber))
                {
                    if (!catchErr)
                    {
                        // アラートを表示し処理を中断
                        msgLogic.MessageBoxShow("E012", string.Format("荷降No{0}の荷降明細", nioroshiNumber));
                    }
                    return returnVal;
                }

                // 20141015 koukouei 休動管理機能追加 start
                // 休動チェック
                if (!this.logic.ChkWordClose(true))
                {
                    return returnVal;
                }
                // 20141015 koukouei 休動管理機能追加 end

                #region オプションによる必須項目チェック
                // コース最適化(NAVITIME連携)がONの場合のみ
                if (AppConfig.AppOptions.IsNAVITIME())
                {
                    if (!this.logic.SagyouTimeCheck())
                    {
                        return returnVal;
                    }
                }
                #endregion

                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.logic.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// Detail-Detail-2部（回収明細部）が一行以上入力されているかチェックする
        /// </summary>
        /// <returns>true: 一件以上入力されている, false: 一件も入力されていない</returns>
        private bool CheckRequiredDataForDeital()
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                foreach (DataGridViewRow detailRow in this.DetailIchiran.Rows)
                {
                    if (detailRow == null) continue;
                    if (detailRow.IsNewRow) continue;

                    returnVal = true;
                    break;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRequiredDataForDeital", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// Detail-Detail-2部（回収明細部）が全てに削除チェックオンされているかチェックする
        /// </summary>
        /// <returns>true: 全てに削除チェックオンされている, false: 全てに削除チェックオンされていない</returns>
        private bool IsAllDeleteChecked()
        {
            bool returnVal = true;

            try
            {
                LogUtility.DebugMethodStart();

                foreach (DataGridViewRow detailRow in this.DetailIchiran.Rows)
                {
                    if (detailRow == null) continue;
                    if (detailRow.IsNewRow) continue;

                    // 削除チェックオフの場合、処理中断
                    if (detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value == null
                        || !bool.Parse(detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value.ToString()))
                    {
                        returnVal = false;
                        break;
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsAllDeleteChecked", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 配車番号変更後処理
        /// <summary>
        /// 配車番号変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TeikiHaishaNumberValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!nowLoding)
                {
                    nowLoding = true;
                    // ブランクの場合、処理しない
                    if (string.IsNullOrEmpty(this.TEIKI_HAISHA_NUMBER.Text)
                        || this.bakTeikiHaishaNumber.Equals(this.TEIKI_HAISHA_NUMBER.Text))
                    {
                        nowLoding = false;
                        return;
                    }

                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                        r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.logic.TeikiHaishaNumberValidated();
                        if (this.ActiveControl != null)
                        {
                            this.ActiveControl.Focus();
                        }
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.TEIKI_HAISHA_NUMBER.Focus();
                    }

                    nowLoding = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 配車番号「前」クリック処理
        /// <summary>
        /// 配車番号「前」クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TeikiHaishaNumberPreviousClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                    !r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                String previousTeikiHaishaNumber = string.Empty;
                String tableName = "T_TEIKI_HAISHA_ENTRY";
                String fieldName = "TEIKI_HAISHA_NUMBER";
                String TeikiHaishaNumber = this.TEIKI_HAISHA_NUMBER.Text;
                String kyoten = this.logic.headerForm.KYOTEN_CD.Text;

                // 前の配車番号を取得
                bool catchErr = false;
                previousTeikiHaishaNumber = this.logic.GetPreviousNumber(tableName, fieldName, TeikiHaishaNumber, kyoten, out catchErr);
                if (catchErr) { return; }
                // 取得できなかった場合、エラー
                if (String.IsNullOrEmpty(previousTeikiHaishaNumber))
                {
                    // アラート表示し、フォーカス移動しない
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    this.TEIKI_HAISHA_NUMBER.Focus();
                    return;
                }

                // 配車番号を設定
                this.TEIKI_HAISHA_NUMBER.Text = previousTeikiHaishaNumber;
                // 配車番号更新後処理
                this.logic.TeikiHaishaNumberValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberPreviousClick", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 配車番号「次」クリック処理
        /// <summary>
        /// 配車番号「次」クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TeikiHaishaNumberNextClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                    !r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                String nextTeikiHaishaNumber = string.Empty;
                String tableName = "T_TEIKI_HAISHA_ENTRY";
                String fieldName = "TEIKI_HAISHA_NUMBER";
                String TeikiHaishaNumber = this.TEIKI_HAISHA_NUMBER.Text;
                String kyoten = this.logic.headerForm.KYOTEN_CD.Text;

                // 次の配車番号を取得
                bool catchErr = false;
                nextTeikiHaishaNumber = this.logic.GetNextNumber(tableName, fieldName, TeikiHaishaNumber, kyoten, out catchErr);
                if (catchErr) { return; }
                // 取得できなかった場合、エラー
                if (String.IsNullOrEmpty(nextTeikiHaishaNumber))
                {
                    // アラート表示し、フォーカス移動しない
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    this.TEIKI_HAISHA_NUMBER.Focus();
                    return;
                }

                // 配車番号を設定
                this.TEIKI_HAISHA_NUMBER.Text = nextTeikiHaishaNumber;
                // 配車番号更新後処理
                this.logic.TeikiHaishaNumberValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberNextClick", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 作業日 Enterイベント
        private string beforeSagyouDate = string.Empty;

        /// <summary>
        /// 作業日 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SagyouDateEnter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DateTime dt;
                if (DateTime.TryParse(this.SAGYOU_DATE.Text, out dt))
                {
                    beforeSagyouDate = Convert.ToString(dt);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SagyouDateEnter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 作業日変更後処理
        /// <summary>
        /// 作業日変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SagyouDateValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (beforeSagyouDate != null
                    && !beforeSagyouDate.Equals(Convert.ToString(this.SAGYOU_DATE.Value)))
                {
                    if (!this.FURIKAE_HAISHA_KBN.Text.Equals("2"))
                    {
                        if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                            && this.FURIKAE_HAISHA_KBN.Text.Equals("1"))
                        {
                            this.logic.MsgBox.MessageBoxShow("E289");
                            this.SAGYOU_DATE.Value = beforeSagyouDate;
                            this.SAGYOU_DATE.Focus();
                        }
                        else
                        {
                            this.COURSE_NAME_CD.Text = string.Empty;
                            this.COURSE_NAME_RYAKU.Text = string.Empty;
                            this.NioroshiIchiran.Rows.Clear();
                            this.DetailIchiran.Rows.Clear();
                        }
                    }
                    else
                    {
                        ///////////////////
                        int AratrFLG = 0;
                        if (this.logic.is_mobile)
                        {
                            //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                            if (!string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                            {
                                if (!(DateTime.Parse(this.SAGYOU_DATE.Text).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                                {
                                    foreach (DataGridViewRow row in this.DetailIchiran.Rows)
                                    {
                                        if (AratrFLG == 0)
                                        {
                                            if (row.Cells[1].Value != null)
                                            {
                                                if (row.Cells[1].Value.ToString() == "True")
                                                {
                                                    AratrFLG = 1;
                                                    this.logic.MsgBox.MessageBoxShowInformation("作業日が当日の場合のみモバイル連携が可能です。\r\n明細のモバイル連携のチェックはクリアされます。");
                                                }
                                            }
                                        }
                                        row.Cells[1].Value = false;
                                    }
                                }
                            }
                        }
                        ////////////////
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SagyouDateValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        internal string dayCD;

        #region コースCD変更後処理
        /// <summary>
        /// コースCD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CourseNameCdValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var before = this.GetBeforeText(this.COURSE_NAME_CD.Name);

                // ブランクの場合
                if (string.IsNullOrEmpty(this.COURSE_NAME_CD.Text))
                {
                    // コース名称略称名をクリア
                    this.COURSE_NAME_RYAKU.Text = string.Empty;
                    this.DAY_CD.Text = string.Empty;
                    this.NioroshiIchiran.Rows.Clear();
                    this.DetailIchiran.Rows.Clear();
                    return;
                }

                // 必要情報チェック
                if (!this.logic.CheckKyotenAndSagyouDate())
                {
                    return;
                }

                if (this.COURSE_NAME_CD.Text == before && !this.logic.isInputError && this.DAY_CD.Text == this.dayCD)
                {
                    // 前回値と同じ場合は、コース明細データの取得は行わない。
                    return;
                }

                // コース明細データを取得し、画面に設定する
                this.logic.CourseNameCdValidated(false);

                switch (this.DAY_NM.Text)
                {
                    case "月":
                        this.DAY_CD.Text = "1";
                        break;
                    case "火":
                        this.DAY_CD.Text = "2";
                        break;
                    case "水":
                        this.DAY_CD.Text = "3";
                        break;
                    case "木":
                        this.DAY_CD.Text = "4";
                        break;
                    case "金":
                        this.DAY_CD.Text = "5";
                        break;
                    case "土":
                        this.DAY_CD.Text = "6";
                        break;
                    case "日":
                        this.DAY_CD.Text = "7";
                        break;
                }

                this.DAY_NM.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CourseNameCdValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コースCD Enterイベント
        /// <summary>
        /// コースCD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CourseNameCdEnter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 必要情報チェック
                if (!this.logic.CheckKyotenAndSagyouDate())
                {
                    return;
                }

                // コース名称 ポップアップデータを取得する
                if (!this.logic.CourseNamePopUpDataInit()) { return; }

                this.dayCD = this.DAY_CD.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CourseNameCdEnter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region DataGridView RowsAddedイベント
        /// <summary>
        /// RowsAddedイベント（回収明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex <= 0)
                {
                    return;
                }

                if (this.DetailIchiran.Rows[e.RowIndex].IsNewRow)
                {
                    // No列（1から連番）
                    this.DetailIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.DetailColName.NO].Value = e.RowIndex;
                    // 順番列（No列と同じ番号）
                    this.DetailIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.DetailColName.JUNNBANN].Value = e.RowIndex;
                }
                // 詳細ポップアップ用テーブル名設定
                if (string.IsNullOrEmpty(this.DetailIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString()))
                {
                    this.DetailIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].Value = ConstCls.preTableName + (e.RowIndex - 1).ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_RowsAdded", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// RowsAddedイベント（荷降明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.NioroshiIchiran.Rows[e.RowIndex].IsNewRow && e.RowIndex > 0)
                {
                    int no = 1;
                    var max = this.NioroshiIchiran.Rows.Cast<DataGridViewRow>().Max(r => r.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                    if (max != null)
                    {
                        no = Int32.Parse(max.ToString()) + 1;
                    }

                    this.NioroshiIchiran.Rows[e.RowIndex - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value = no;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NioroshiIchiran_RowsAdded", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region DataGridView CellContentClickイベント
        /// <summary>
        /// CellContentClickイベント（回収明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                // 「詳細」ボタン押下する時、回収品名詳細ポップアップを表示する
                if (this.DetailIchiran.Columns[ConstCls.DetailColName.SHOUSAI].Index == e.ColumnIndex)
                {
                    if (this.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, e.RowIndex].ReadOnly)
                    {
                        if (!this.logic.ShowShousai(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.DetailIchiran.Rows[e.RowIndex]))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!this.logic.ShowShousai(this.WindowType, this.DetailIchiran.Rows[e.RowIndex]))
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellContentClick", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 車輌更新後処理
        /// <summary>
        /// 車輌更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var before = this.GetBeforeText(this.SHARYOU_CD.Name);
                if ((!this.logic.isInputError && this.SHARYOU_CD.Text == before) &&
                    (!this.logic.isCalledSharyouPopupFromLogic && !string.IsNullOrEmpty(this.SHARYOU_NAME_RYAKU.Text)))
                {
                    return;
                }

                this.logic.isCalledSharyouPopupFromLogic = false;
                if (!this.logic.ChechSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }

                this.logic.isInputError = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        private string beforeGyoushaCd = string.Empty;
        private string beforeGenbaCd = string.Empty;
        private string afterGyoushaCd = string.Empty;
        private string afterGenbaCd = string.Empty;

        #region DataGridView CellEnterイベント
        /// <summary>
        /// IME無効処理（回収明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex)
                {
                    var row = this.DetailIchiran.Rows[e.RowIndex];
                    this.beforeGyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                }
                else if (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex)
                {
                    var row = this.DetailIchiran.Rows[e.RowIndex];
                    this.beforeGenbaCd = row.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                }
                else if (this.DetailIchiran.Columns[ConstCls.DetailColName.MEISAI_BIKOU].Index == e.ColumnIndex)
                {
                    // IME有効
                    this.DetailIchiran.ImeMode = System.Windows.Forms.ImeMode.On;
                }
                else
                {
                    // IME無効(半角英数のみ)
                    this.DetailIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }

                // 前回値チェック用データをセット
                String cellName = this.DetailIchiran.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.DetailIchiran[cellName, e.RowIndex].Value);
                if (beforeValuesForDetail.ContainsKey(cellName))
                {
                    beforeValuesForDetail[cellName] = cellValue;
                }
                else
                {
                    beforeValuesForDetail.Add(cellName, cellValue);
                }

                // 前回値チェック用データセット後に表示を変更しないとValidatingでフォーマット処理されないため
                if (this.DetailIchiran.Columns[ConstCls.DetailColName.KIBOU_TIME].Index == e.ColumnIndex && !this.DetailIchiran[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // 入力可能な場合「:」を削除
                    this.DetailIchiran[e.ColumnIndex, e.RowIndex].Value = Convert.ToString(this.DetailIchiran[e.ColumnIndex, e.RowIndex].Value).Replace(":", string.Empty);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellEnter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 荷降明細部 CellValidatingイベント
        /// <summary>
        /// 荷降業者チェック、荷降現場チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                String cellName = this.NioroshiIchiran.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.NioroshiIchiran[cellName, e.RowIndex].Value);

                if (e.RowIndex < 0 || (!this.logic.isInputError && beforeValuesForDetail[cellName] == cellValue))
                {
                    return;
                }

                switch (e.ColumnIndex)
                {
                    case 1:
                        // 荷降業者チェック
                        if (!this.logic.CheckNioroshiGyoushaCd(this.NioroshiIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.NioroshiIchiran.BeginEdit(false);
                            return;
                        }
                        break;
                    case 3:
                        // 荷降現場チェック
                        if (!this.logic.ChechNioroshiGenbaCd(this.NioroshiIchiran.Rows[e.RowIndex]))
                        {
                            e.Cancel = true;
                            this.NioroshiIchiran.BeginEdit(false);
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NioroshiIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 運転者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNTENSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNTENSHA_CD.Text))
                {
                    return;
                }

                this.logic.UNTENSHA_CDValidated();

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

        /// <summary>
        /// 運搬業者CD（FocusOutCheckMethodと併用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UNPAN_GYOUSHA_CDValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 前回値と同じ値だった場合  処理しない
                var before = this.GetBeforeText(this.UNPAN_GYOUSHA_CD.Name);
                if (!this.UNPAN_GYOUSHA_CD.Enabled || this.UNPAN_GYOUSHA_CD.Text == before)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                    if (this.UNPAN_GYOUSHA_CD.Text != before)
                    {
                        // ブランクに変更された場合、車輌情報もクリア
                        this.SHARYOU_CD.Text = string.Empty;
                        this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                        this.ClearBeforeText(this.SHARYOU_CD.Name);
                    }

                    return;
                }

                this.logic.UNPAN_GYOUSHA_CDValidated();

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

        /// <summary>
        /// 行切替処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.Disposing == false)
            {
                // 編集名のセット
                var row = this.DetailIchiran.Rows[e.RowIndex];

                // 回数の重複チェック
                bool catchErr = false;
                if (true == this.logic.roundNoOverlapCheck(row, out catchErr))
                {
                    if (!catchErr)
                    {
                        // エラーメッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E031", "回数・業者CD・現場CD");
                    }
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 更新後処理（回収明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    return;
                }
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].Index == e.ColumnIndex)
                {
                    // 業者が消されていたら現場も削除
                    {
                        var row = this.DetailIchiran.Rows[e.RowIndex];
                        if (row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString().Equals(string.Empty)
                            || !beforeGyoushaCd.Equals(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString()))
                        {
                            row.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                            row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                            row.Cells[ConstCls.DetailColName.HINMEI_INFO].Value = string.Empty;
                            // 業者、現場が変更になる場合は詳細をクリア
                            string tableName = this.DetailIchiran.Rows[e.RowIndex].Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString();    // 対象の詳細テーブル名を取得
                            if (this.logic.shousaiDataSet.Tables.Contains(tableName))
                            {
                                this.logic.shousaiDataSet.Tables.Remove(tableName);
                            }
                        }
                        this.afterGyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                    }
                }
                else if (this.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].Index == e.ColumnIndex)
                {
                    // 現場が消されていたら品名情報も削除 変更後の現場の状態を保持
                    {
                        var row = this.DetailIchiran.Rows[e.RowIndex];
                        if (row.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString().Equals(string.Empty)
                            || !beforeGenbaCd.Equals(row.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString()))
                        {
                            row.Cells[ConstCls.DetailColName.HINMEI_INFO].Value = string.Empty;
                            // 業者、現場が変更になる場合は詳細をクリア
                            string tableName = this.DetailIchiran.Rows[e.RowIndex].Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString();    // 対象の詳細テーブル名を取得
                            if (this.logic.shousaiDataSet.Tables.Contains(tableName))
                            {
                                this.logic.shousaiDataSet.Tables.Remove(tableName);
                            }
                            // 業者名をセット
                            if (!this.logic.SetGyoushaName(row)) { return; }
                        }

                        this.afterGenbaCd = row.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellValidated", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        internal string beforeNioroshiGyoushaCd = string.Empty;

        /// <summary>
        /// Enterイベント（荷降明細部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NioroshiIchiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                switch (e.ColumnIndex)
                {
                    case 1:
                        var row = this.NioroshiIchiran.Rows[e.RowIndex];
                        beforeNioroshiGyoushaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                        break;
                    default:
                        break;
                }

                // 前回値チェック用データをセット
                String cellName = this.NioroshiIchiran.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.NioroshiIchiran[cellName, e.RowIndex].Value);
                if (beforeValuesForDetail.ContainsKey(cellName))
                {
                    beforeValuesForDetail[cellName] = cellValue;
                }
                else
                {
                    beforeValuesForDetail.Add(cellName, cellValue);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NioroshiIchiran_CellEnter", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #region 時間項目の入力後チェック

        /// <summary>
        /// 作業開始_時の入力後チェック
        /// 時が空でなく分が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SAGYOU_BEGIN_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_BEGIN_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業開始_分の入力後チェック
        /// 分が空でなく時が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SAGYOU_BEGIN_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_BEGIN_HOUR.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業終了_時の入力後チェック
        /// 時が空でなく分が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SAGYOU_END_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_END_MINUTE.Text))
                {
                    this.SAGYOU_END_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業終了_分の入力後チェック
        /// 分が空でなく時が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SAGYOU_END_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_END_MINUTE.Text))
                {
                    this.SAGYOU_END_HOUR.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 時間項目の入力チェック

        #region ポップアップ後の処理
        /// <summary>
        /// 現場CDポップアップ後の処理
        /// </summary>
        public void GenbaPopupAfterMethod()
        {
            LogUtility.DebugMethodStart();

            // 現在行があるかチェック
            var row = this.DetailIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            // 業者名をセット
            if (!this.logic.SetGyoushaName(row)) { return; }
        }

        /// <summary>
        /// 荷降現場CDポップアップ後の処理
        /// </summary>
        public void NioroshiGenbaAfterPopupMethod()
        {
            LogUtility.DebugMethodStart();

            // 現在行があるかチェック
            var row = this.NioroshiIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            // 荷降業者名をセット
            if (!this.logic.SetNioroshiGyoushaName(row)) { return; }

            // フォーカスを現場にセット
            this.NioroshiIchiran["NIOROSHI_GENBA_CD", row.Index].Selected = true;
        }
        #endregion ポップアップ後の処理

        // 20141015 koukouei 休動管理機能追加 start
        #region
        /// <summary>
        /// 補助員CD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOJOIN_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.HOJOIN_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.HOJOIN_CD.Text))
                {
                    // 補助員名の初期化は行う
                    this.HOJOIN_NAME.Text = string.Empty;
                    return;
                }

                this.logic.HOJOIN_CDValidated();

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
        // 20141015 koukouei 休動管理機能追加 end

        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 明細現場チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var columnName = this.DetailIchiran.Columns[e.ColumnIndex].Name;
            var cellValue = Convert.ToString(this.DetailIchiran[columnName, e.RowIndex].Value);
            var row = this.DetailIchiran.Rows[e.RowIndex];
            var cell = this.DetailIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (this.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG || (!this.logic.isInputError && beforeValuesForDetail[columnName] == cellValue))
            {
                return;
            }

            switch (columnName)
            {
                // 業者CD
                case "GYOUSHA_CD":
                    // 業者CD取得
                    var gyoushaCd = string.Empty;
                    if (row.Cells["GYOUSHA_CD"] != null && row.Cells["GYOUSHA_CD"].Value != DBNull.Value && row.Cells["GYOUSHA_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["GYOUSHA_CD"].Value.ToString()))
                        {
                            row.Cells["GYOUSHA_CD"].Value = row.Cells["GYOUSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                            gyoushaCd = row.Cells["GYOUSHA_CD"].Value.ToString();
                        }
                    }
                    else
                    {
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                    }

                    // 業者チェック
                    if (!this.logic.ChechiGyoushaCd(this.DetailIchiran.Rows[e.RowIndex]))
                    {
                        e.Cancel = true;
                        this.DetailIchiran.BeginEdit(false);
                        return;
                    }

                    this.logic.isInputError = false;

                    break;
                // 現場CD
                case "GENBA_CD":
                    // 品名CD取得
                    var genbaCd = string.Empty;
                    if (row.Cells["GENBA_CD"] != null && row.Cells["GENBA_CD"].Value != DBNull.Value && row.Cells["GENBA_CD"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(row.Cells["GENBA_CD"].Value.ToString()))
                        {
                            row.Cells["GENBA_CD"].Value = row.Cells["GENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                            genbaCd = row.Cells["GENBA_CD"].Value.ToString();
                        }
                    }
                    else
                    {
                        row.Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                    }

                    // 現場チェック
                    if (!this.logic.ChechiGenbaCd(this.DetailIchiran.Rows[e.RowIndex]))
                    {
                        e.Cancel = true;
                        this.DetailIchiran.BeginEdit(false);
                        return;
                    }

                    this.logic.isInputError = false;

                    break;
                // 希望時間
                case "KIBOU_TIME":
                    if (!this.logic.IsTimeChkOK(cell))
                    {
                        e.Cancel = true;
                        this.DetailIchiran.BeginEdit(false);
                        return;
                    }

                    this.logic.isInputError = false;
                    break;
            }
        }
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

        /// <summary>
        /// 卸降業者 PopupAfterExecuteMethod
        /// </summary>
        public void NIOROSHI_GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.NioroshiIchiran.CurrentRow == null) { return; }
            if (this.NioroshiIchiran.CurrentCell.ColumnIndex == 1)
            {
                curGyoushaCd = this.NioroshiIchiran.CurrentCell.EditedFormattedValue.ToString();
                if (preGyoushaCd != curGyoushaCd)
                {
                    this.NioroshiIchiran.CurrentRow.Cells["NIOROSHI_GENBA_CD"].Value = string.Empty;
                    this.NioroshiIchiran.CurrentRow.Cells["NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// 卸降業者 PopupBeforeExecuteMethod
        /// </summary>
        public void NIOROSHI_GYOUSHA_PopupBeforeExecuteMethod()
        {
            if (this.NioroshiIchiran.CurrentRow == null) { return; }
            preGyoushaCd = string.Empty;
            if (this.NioroshiIchiran.CurrentCell.ColumnIndex == 1)
            {
                preGyoushaCd = this.NioroshiIchiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.DetailIchiran.CurrentRow == null) { return; }
            if (this.DetailIchiran.CurrentCell.ColumnIndex == 5)
            {
                curGyoushaCd = this.DetailIchiran.CurrentCell.EditedFormattedValue.ToString();
                if (preGyoushaCd != curGyoushaCd)
                {
                    this.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value = string.Empty;
                    this.DetailIchiran.CurrentRow.Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            if (this.DetailIchiran.CurrentRow == null) { return; }
            preGyoushaCd = string.Empty;
            if (this.DetailIchiran.CurrentCell.ColumnIndex == 5)
            {
                preGyoushaCd = this.DetailIchiran.CurrentCell.EditedFormattedValue.ToString();
            }
        }

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Type type = sender.GetType();
            if (type.Name == "CustomAlphaNumTextBox")
            {
                CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
            else if (type.Name == "CustomNumericTextBox2")
            {
                CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
            else if (type.Name == "CustomDateTimePicker")
            {
                CustomDateTimePicker ctrl = (CustomDateTimePicker)sender;
                if (dicControl.ContainsKey(ctrl.Name))
                {
                    dicControl[ctrl.Name] = ctrl.Text;
                }
                else
                {
                    dicControl.Add(ctrl.Name, ctrl.Text);
                }
            }
        }

        /// <summary>
        /// コントロールに入力された値をクリアします
        /// </summary>
        /// <param name="key">コントロール名</param>
        internal void ClearBeforeText(string key)
        {
            if (dicControl.ContainsKey(key))
            {
                dicControl[key] = string.Empty;
            }
        }

        /// <summary>
        /// コントロールに入力されていた値を取得します
        /// </summary>
        /// <param name="key">コントロール名</param>
        /// <returns>前回値</returns>
        internal String GetBeforeText(string key)
        {
            LogUtility.DebugMethodStart(key);

            string ret = this.dicControl.Where(r => r.Key == key).Select(r => r.Value).DefaultIfEmpty(String.Empty).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        #endregion

        public void FURIKAE_HAISHA_KBN_TextChanged(object sender, EventArgs e)
        {
            this.COURSE_NAME_CD.ReadOnly = false;
            // ２．振替から１．通常　に変更した場合
            if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                && this.FURIKAE_HAISHA_KBN.Text.Equals("1"))
            {
                DialogResult result = new DialogResult();
                result = this.logic.MsgBox.MessageBoxShow("C108");
                if (result.Equals(DialogResult.OK)
                   || result.Equals(DialogResult.Yes))
                {
                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.NEW_WINDOW_FLAG))
                    {
                        return;
                    }

                    // 処理モード変更
                    this.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                    // 初期化
                    this.logic.tourokuZumiNioroshiList = new List<string>();
                    // 配車番号
                    this.TEIKI_HAISHA_NUMBER.Text = string.Empty;
                    this.logic.teikiHaishaNumber = string.Empty;
                    this.bakTeikiHaishaNumber = string.Empty;
                    // 画面項目初期化【新規】モード
                    if (!this.logic.ModeInit(WINDOW_TYPE.NEW_WINDOW_FLAG)) { return; }
                }
                else
                {
                    this.COURSE_NAME_CD.ReadOnly = true;
                    this.FURIKAE_HAISHA_KBN.TextChanged -= new System.EventHandler(this.FURIKAE_HAISHA_KBN_TextChanged);
                    this.FURIKAE_HAISHA_KBN.Text = "2";
                    this.FURIKAE_HAISHA_KBN.TextChanged += new System.EventHandler(this.FURIKAE_HAISHA_KBN_TextChanged);
                    this.FURIKAE_HAISHA_KBN.Focus();
                }

            }
            // １．通常から２．振替　に変更した場合
            else if (this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                     && this.FURIKAE_HAISHA_KBN.Text.Equals("2"))
            {
                this.COURSE_NAME_CD.ReadOnly = true;
            }
            else
            {
                this.DAY_CD.Text = string.Empty;
                this.COURSE_NAME_CD.Text = string.Empty;
                this.COURSE_NAME_RYAKU.Text = string.Empty;
                this.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.SHASHU_CD.Text = string.Empty;
                this.SHASHU_NAME_RYAKU.Text = string.Empty;
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.UNTENSHA_CD.Text = string.Empty;
                this.UNTENSHA_NAME.Text = string.Empty;
                this.UNTENSHA_CD.Text = string.Empty;
                this.UNTENSHA_NAME.Text = string.Empty;
                this.HOJOIN_CD.Text = string.Empty;
                this.HOJOIN_NAME.Text = string.Empty;
                this.NioroshiIchiran.Rows.Clear();
                this.DetailIchiran.Rows.Clear();
                this.logic.shousaiDataSet = new DataSet();
                if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                {
                    this.SAGYOU_DATE.Value = ((BusinessBaseForm)this.ParentForm).sysDate;
                }
                this.SAGYOU_BEGIN_HOUR.Text = string.Empty;
                this.SAGYOU_BEGIN_MINUTE.Text = string.Empty;
                this.SAGYOU_END_HOUR.Text = string.Empty;
                this.SAGYOU_END_MINUTE.Text = string.Empty;
                this.SHUPPATSU_GYOUSHA_CD.Text = string.Empty;
                this.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;
                this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.SHUPPATSU_GENBA_NAME.Text = string.Empty;

                this.logic.CourseNamePopUpDataInit();
            }
        }

        public void PopupAfter()
        {
            switch (this.DAY_NM.Text)
            {
                case "月":
                    this.DAY_CD.Text = "1";
                    break;
                case "火":
                    this.DAY_CD.Text = "2";
                    break;
                case "水":
                    this.DAY_CD.Text = "3";
                    break;
                case "木":
                    this.DAY_CD.Text = "4";
                    break;
                case "金":
                    this.DAY_CD.Text = "5";
                    break;
                case "土":
                    this.DAY_CD.Text = "6";
                    break;
                case "日":
                    this.DAY_CD.Text = "7";
                    break;
            }
        }

        /// <summary>
        /// 運搬業者をクリアしても、車輌の情報もクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME_RYAKU.Text = string.Empty;
                dicControl["SHARYOU_CD"] = string.Empty;
            }
        }

        /// <summary>
        /// 出発業者Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUPPATSU_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 前回値と同じ値だった場合  処理しない
                var before = this.GetBeforeText(this.SHUPPATSU_GYOUSHA_CD.Name);
                if (!this.SHUPPATSU_GYOUSHA_CD.Enabled || this.SHUPPATSU_GYOUSHA_CD.Text == before)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.SHUPPATSU_GYOUSHA_CD.Text))
                {
                    this.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;

                    if (this.SHUPPATSU_GYOUSHA_CD.Text != before)
                    {
                        // ブランクに変更された場合、車輌情報もクリア
                        this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                        this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                        this.ClearBeforeText(this.SHUPPATSU_GENBA_CD.Name);
                    }

                    return;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 出発現場Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUPPATSU_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 業者CDが未入力の場合は、入力できない
            var gyoushaCd = this.SHUPPATSU_GYOUSHA_CD.Text;
            var genbaCd = this.SHUPPATSU_GENBA_CD.Text;
            if (!String.IsNullOrEmpty(genbaCd) && String.IsNullOrEmpty(gyoushaCd))
            {
                this.logic.MsgBox.MessageBoxShow("E051", "出発業者");
                this.SHUPPATSU_GYOUSHA_CD.Text = string.Empty;
                e.Cancel = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(genbaCd))
                {
                    var mGenba = DaoInitUtility.GetComponent<IM_GENBADao>().GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();
                    if (null != mGenba)
                    {
                        this.SHUPPATSU_GENBA_NAME.Text = mGenba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                        this.logic.MsgBox.MessageBoxShow("E020", "出発現場");

                        e.Cancel = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出発現場Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHUPPATSU_GENBA_CD_Validated(object sender, EventArgs e)
        {
            // 前回値と同じ値だった場合  処理しない
            var before = this.GetBeforeText(this.SHUPPATSU_GENBA_CD.Name);
            if (!this.SHUPPATSU_GENBA_CD.Enabled || this.SHUPPATSU_GENBA_CD.Text == before)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.SHUPPATSU_GENBA_CD.Text))
            {
                this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
            }
        }

        ///////////////////////////////////////
        //モバイル連携
        ///////////////////////////////////////

        #region 列ヘッダーにチェックボックスを表示
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void DetailIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(25, 25))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - checkBoxAll.Width) / 2, (bmp.Height - checkBoxAll.Height) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width);
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion

        #region 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void DetailIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //[削除]をクリック
            //削除チェックが付いたら、モバイル連携のチェックを外す
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.DetailIchiran[0, e.RowIndex];
                //null→true
                if (cell.Value == null)
                {
                    this.DetailIchiran[1, e.RowIndex].Value = false;
                    this.DetailIchiran.Refresh();
                }
                else
                {
                    //false→true
                    if (cell.Value.ToString() == "False")
                    {
                        this.DetailIchiran[1, e.RowIndex].Value = false;
                        this.DetailIchiran.Refresh();
                    }
                }
            }

            //[モバイル連携]をクリック
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                //スペースで、OFFの場合は抜ける
                if (((bool)this.logic.SpaceChk) && (!(bool)this.logic.SpaceON))
                {
                    return;
                }
                this.logic.SpaceON = false;

                //ReadOnlyなら抜ける。
                if (this.DetailIchiran[1, e.RowIndex].ReadOnly)
                {
                    return;
                }
                
                //[モバイル連携]がONに変更される場合、チェックを入れる（null⇒true、false⇒true）
                if ((this.DetailIchiran[1, e.RowIndex].Value == null)
                    || (!(bool)this.DetailIchiran[1, e.RowIndex].Value))
                {

                    //[システム日付] != [作業日]の場合はチェックをつけない
                    string SagyoA = string.Empty;
                    SagyoA = this.SAGYOU_DATE.Text;

                    if (!(DateTime.Parse(this.SAGYOU_DATE.Text).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                    {
                        this.logic.MsgBox.MessageBoxShowInformation("作業日が当日の場合のみ連携が可能です。");
                        //this.DetailIchiran[1, e.RowIndex].Value = true;
                        this.DetailIchiran[1, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                        this.logic.SpaceChk = false;
                        return;
                    }

                    //[削除]にチェックが付いていたら、チェックをつけない
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.DetailIchiran[0, e.RowIndex];
                    //not null && true
                    if (cell.Value != null)
                    {
                        if (cell.Value.ToString() == "True")
                        {
                            this.DetailIchiran[1, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            return;
                        }
                    }

                    if (this.logic.SpaceChk)
                    {
                        if (this.DetailIchiran[1, e.RowIndex].Value == null)
                        {
                            this.DetailIchiran[1, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.DetailIchiran[1, e.RowIndex].Value = !(bool)this.DetailIchiran[1, e.RowIndex].Value;
                        }
                        this.logic.SpaceChk = false;
                    }
                }
                else
                {
                    this.logic.SpaceChk = false;
                }
            }

            //[モバイル連携]のヘッダをクリック
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                this.DetailIchiran.Refresh();
            }
        }
        #endregion

        #region すべての行のチェック状態を切り替える
        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DetailIchiran.Rows.Count == 0)
            {
                return;
            }

            //[システム日付] != [作業日]の場合はチェックをつけない
            if (!(DateTime.Parse(this.SAGYOU_DATE.Text).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
            {
                return;
            }

            foreach (DataGridViewRow row in this.DetailIchiran.Rows)
            {
                //連携済み、新規行はチェックをつけない
                if ((!row.Cells[1].ReadOnly) && (!row.IsNewRow))
                {
                    //削除にチェックありはチェックをつけない
                    if (row.Cells[0].Value == null)
                    {
                        row.Cells[1].Value = checkBoxAll.Checked;
                    }
                    else if (!(bool)row.Cells[0].Value)
                    {
                        row.Cells[1].Value = checkBoxAll.Checked;
                    }
                }
            }
            this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[0].Cells[0];
            this.DetailIchiran.CurrentCell = this.DetailIchiran.Rows[0].Cells[1];
        }

        #endregion

        /// <summary>
        /// [モバイル連携]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailIchiran_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.DetailIchiran.CurrentCell;
                //[モバイル連携]
                if (curCell.ColumnIndex == 1 && curCell.RowIndex >= 0)
                {
                    //readonlyなら抜ける
                    if (this.DetailIchiran[1, curCell.RowIndex].ReadOnly)
                    {
                        return;
                    }
                
                    this.logic.SpaceChk = true;
                    this.logic.SpaceON = false;
                    //[モバイル連携]OFFにする場合は、何もしない。
                    //[モバイル連携]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                    if (this.DetailIchiran[1, curCell.RowIndex].Value == null)
                    {
                        this.logic.SpaceON = true;
                        this.DetailIchiran[1, curCell.RowIndex].Value = true;
                    }
                    else
                    {
                        if (!(bool)this.DetailIchiran[1, curCell.RowIndex].Value)
                        {
                            this.logic.SpaceON = true;
                            this.DetailIchiran[1, curCell.RowIndex].Value = !(bool)this.DetailIchiran[1, curCell.RowIndex].Value;
                        }
                    }
                    this.DetailIchiran.Refresh();
                }
            }
        }
    }
}
