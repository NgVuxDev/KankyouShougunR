// $Id: LogicClass.cs 51163 2015-06-01 07:48:00Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.App;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.Const;
using Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.DAO;
using Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.DTO;
using Shougun.Function.ShougunCSCommon.Dao;
using System.Drawing;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 伝票登録用Dao
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_ENTRYDao ukeireEntryDao;
        private Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_DETAILDao ukeireDetailDao;
        private IT_SHUKKA_ENTRYDao shukkaEntryDao;
        private IT_SHUKKA_DETAILDao shukkaDetailDao;
        private IT_UR_SH_ENTRYDao urshEntryDao;
        private IT_UR_SH_DETAILDao urshDetailDao;
        private TCRClass contenaEntryDao;
        private TZHHClass zaikoDao;
        private TKDClass kenshuDao;

        /// <summary>
        /// NyuuSyutuKinIchiranForm
        /// </summary>
        private DenpyouKakuteiNyuryokuForm form;

        /// <summary>
        /// ribbon
        /// </summary>
        private RibbonMainMenu ribbon;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private HeaderForm headForm;

        /// <summary>
        /// 受入明細リスト
        /// </summary>
        private List<T_UKEIRE_DETAIL> ukeireDetailList;

        /// <summary>
        /// 受入入力リスト(新規追加用)
        /// </summary>
        private List<T_UKEIRE_ENTRY> ukeireEntryList;

        /// <summary>
        /// 受入入力リスト(論理削除用)
        /// </summary>
        private List<T_UKEIRE_ENTRY> ukeireEntryDeleteList;

        /// <summary>
        /// 出荷明細リスト
        /// </summary>
        private List<T_SHUKKA_DETAIL> shukkaDetailList;

        /// <summary>
        /// 検収明細リスト
        /// </summary>
        private List<T_KENSHU_DETAIL> kenshuDetailList;

        /// <summary>
        /// 出荷入力リスト(新規追加用)
        /// </summary>
        private List<T_SHUKKA_ENTRY> shukkaEntryList;

        /// <summary>
        /// 出荷入力リスト(論理削除用)
        /// </summary>
        private List<T_SHUKKA_ENTRY> shukkaEntryDeleteList;

        /// <summary>
        /// 売上支払明細リスト
        /// </summary>
        private List<T_UR_SH_DETAIL> urshDetailList;

        /// <summary>
        /// 売上支払入力リスト(新規追加用)
        /// </summary>
        private List<T_UR_SH_ENTRY> urshEntryList;

        /// <summary>
        /// 売上支払入力リスト(論理削除用)
        /// </summary>
        private List<T_UR_SH_ENTRY> urshEntryDeleteList;

        /// <summary>
        /// コンテナ稼動実績リスト(新規追加用)
        /// </summary>
        private List<T_CONTENA_RESULT> contenaResultEntryList;

        /// <summary>
        /// コンテナ稼動実績削除リストリスト(論理削除用)
        /// </summary>
        private List<T_CONTENA_RESULT> contenaResultEntryDeleteList;

        /// <summary>
        /// 在庫品名振分リスト
        /// </summary>
        private List<T_ZAIKO_HINMEI_HURIWAKE> zaikoList;

        /// <summary>
        /// 伝票確定入力Dao
        /// </summary>
        private DAOClass kakuteiDao;

        /// <summary>
        /// MessageBoxShowLogic
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システム情報
        /// </summary>
        private M_SYS_INFO sysInfo;

        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(DenpyouKakuteiNyuryokuForm targetForm)
        {
            this.form = targetForm;
            this.kakuteiDao = DaoInitUtility.GetComponent<DAOClass>();
            this.ukeireEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_ENTRYDao>();
            this.ukeireDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_DETAILDao>();
            this.shukkaEntryDao = DaoInitUtility.GetComponent<IT_SHUKKA_ENTRYDao>();
            this.shukkaDetailDao = DaoInitUtility.GetComponent<IT_SHUKKA_DETAILDao>();
            this.urshEntryDao = DaoInitUtility.GetComponent<IT_UR_SH_ENTRYDao>();
            this.urshDetailDao = DaoInitUtility.GetComponent<IT_UR_SH_DETAILDao>();
            this.contenaEntryDao = DaoInitUtility.GetComponent<TCRClass>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.zaikoDao = DaoInitUtility.GetComponent<TZHHClass>();
            this.kenshuDao = DaoInitUtility.GetComponent<TKDClass>();

            // メッセージ出力用
            this.msgLogic = new MessageBoxShowLogic();
        }

        #endregion コンストラクタ

        #region 初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ParentFormのSet
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // HeaderFormのSet
                this.headForm = (HeaderForm)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // システム情報を取得
                this.sysInfo = this.ribbon.GlobalCommonInformation.SysInfo;

                //　ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 各初期値設定
                if (!this.SetHeaderInit())
                {
                    return false;
                }
                this.SetFormInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            //Functionボタンのイベント生成
            this.parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);              //確定解除 
            this.parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //検索条件クリア
            this.parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
            this.parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);     //確定登録
            this.parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);
            this.parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる

            // DataGridView系のイベント生成
            this.form.Ichiran.CellClick += new DataGridViewCellEventHandler(this.Ichiran_CellClick);
            this.form.Ichiran.CellPainting += new DataGridViewCellPaintingEventHandler(this.Ichiran_CellPainting);

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            // 20141201 teikyou ダブルクリックを追加する　start
            this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

        }

        /// <summary>
        /// ヘッダ初期値設定
        /// </summary>
        private bool SetHeaderInit()
        {
            try
            {
                this.headForm.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;

                //前回保存値がない場合、または空の場合はシステム設定ファイルから拠点CDを設定する
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                if (string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
                }

                // ユーザ拠点名称の取得
                var kyotenEntity = this.kyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                if (kyotenEntity == null)
                {
                    this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                }
                else
                {
                    if (this.headForm.KYOTEN_CD.Text != "")
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = kyotenEntity.KYOTEN_NAME_RYAKU;
                    }
                }

                // 日付
                if (String.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_FROM))
                {
                    this.headForm.HIDUKE_FROM.Text = this.parentForm.sysDate.ToString();
                }
                else
                {
                    this.headForm.HIDUKE_FROM.Text = Properties.Settings.Default.SET_HIDUKE_FROM;
                }

                if (String.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_TO))
                {
                    this.headForm.HIDUKE_TO.Text = this.parentForm.sysDate.ToString();
                }
                else
                {
                    this.headForm.HIDUKE_TO.Text = Properties.Settings.Default.SET_HIDUKE_TO;
                }

                this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKE_KBN;
                // 日付区分
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_KBN))
                {
                    // 前回入力していない場合は伝票日付をセット
                    this.headForm.txtNum_HidukeSentaku.Text = "1";
                }
                else
                {
                    this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKE_KBN;
                }

                // 読込データ件数初期値0
                this.headForm.ReadDataNumber.Text = "0";

                // WindowTitleの設定
                this.headForm.lb_title.Text = "伝票確定入力";
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetHeaderInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeaderInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// メインフォーム初期値設定
        /// </summary>
        private void SetFormInit()
        {
            // 「XX確定を利用」の意味：対象の伝票上に確定区分を表示するかどうかの設定値
            // 上記のため伝票種類の非活性制御はしない
            this.form.CheckBox_Jyunyu.Checked = this.sysInfo.URIAGE_HYOUJI_JOUKEN_UKEIRE.Value;
            this.form.CheckBox_Syuka.Checked = this.sysInfo.URIAGE_HYOUJI_JOUKEN_SHUKKA.Value;
            this.form.CheckBox_Uriageshiharai.Checked = this.sysInfo.URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI.Value;
            this.form.CheckBox_Dainou.Checked = false;

            // No.1781
            // システム情報を取得
            this.form.txtNum_KakuteiKbnSentaku.Text = this.sysInfo.URIAGE_HYOUJI_JOUKEN.Value.ToString();

            // 伝票区分項目の初期値は「売上」
            this.form.radbtn_DenKbnUriage.Checked = true;

            // Formサイズに合わせてDataGridViewサイズを動的に変更する
            this.form.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;

            // 明細項目固定となったため親FormのDGV、汎用検索は非表示
            // ※継承Formを変更すると影響大のため
            this.form.customDataGridView1.Visible = false;
            this.form.searchString.Visible = false;

            this.form.customSortHeader1.ClearCustomSortSetting();
        }

        #endregion 初期化処理

        #region Functionキー押下イベント
        /// <summary>
        /// F1 確定解除
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
            if (CheckDate())
            {
                return;
            }
            // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

            if (this.form.Ichiran.RowCount > 0)
            {
                // 月次処理中チェック
                bool catchErr = true;
                bool isErr = this.CheckGetsujiShoriChu(false, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (isErr)
                {
                    this.msgLogic.MessageBoxShow("E224", "実行");
                    return;
                }

                // 月次処理ロックチェック
                isErr = this.CheckGetsujiShoriLock(false, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (isErr)
                {
                    this.msgLogic.MessageBoxShow("E223", "実行");
                    return;
                }

                // PASSWORD認証
                PasswordPopupForm pwForm = new PasswordPopupForm(this.sysInfo.URIAGE_KAKUTEI_KAIJO_PASSWORD, "3");
                pwForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                pwForm.ShowDialog();

                // PASSWORD認証OK
                if (pwForm.rightpassword)
                {
                    // 確定解除処理＆再表示
                    Cursor preCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    var result = this.setKakuteiStatus(false);
                    this.showDetail();
                    Cursor.Current = preCursor;

                    if (0 < result)
                    {
                        //確定解除完了メッセージを表示
                        this.msgLogic.MessageBoxShow("I001", "確定解除");
                    }
                    else if (0 == result)
                    {
                        //未登録メッセージを表示
                        this.msgLogic.MessageBoxShow("E169", "対象データが未変更", "確定解除処理");
                    }
                }
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            if (this.form.Ichiran.RowCount > 0)
            {
                // 明細クリア
                var table = ((DataTable)this.form.Ichiran.DataSource).Clone();
                this.form.Ichiran.DataSource = table;
            }

            // 各初期値設定
            if (!this.SetHeaderInit())
            {
                return;
            }
            this.SetFormInit();
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
            if (CheckDate())
            {
                return;
            }
            // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

            if ((this.form.CheckBox_Jyunyu.Checked == false) &&
               (this.form.CheckBox_Syuka.Checked == false) &&
               (this.form.CheckBox_Uriageshiharai.Checked == false) &&
               (this.form.CheckBox_Dainou.Checked == false))
            {
                // 伝種区分が全て選択されていない場合は促すメッセージ表示
                this.msgLogic.MessageBoxShow("E051", "伝票種類");
            }
            else
            {
                //検索処理実行
                Cursor preCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                this.showDetail();
                Cursor.Current = preCursor;

                if (this.form.Ichiran.RowCount == 0)
                {
                    // 検索結果が存在しない場合メッセージ表示
                    this.msgLogic.MessageBoxShow("C001");
                }
            }
        }

        /// <summary>
        /// F9 確定登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            DataTable dbkoshin = (DataTable)this.form.Ichiran.DataSource;
            if (dbkoshin != null)
            {
                if (dbkoshin.Rows.Count > 0)
                {
                    bool catchErr = true;
                    bool isErr = this.CheckGetsujiShoriChu(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 月次処理中チェック
                    if (isErr)
                    {
                        this.msgLogic.MessageBoxShow("E224", "実行");
                        return;
                    }

                    // 月次処理ロックチェック
                    isErr = this.CheckGetsujiShoriLock(true, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isErr)
                    {
                        this.msgLogic.MessageBoxShow("E223", "実行");
                        return;
                    }

                    //確定登録の確認メッセージを表示
                    if (this.msgLogic.MessageBoxShow("C046", "確定登録") == DialogResult.Yes)
                    {
                        // 確定登録処理＆再表示
                        Cursor preCursor = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        var result = this.setKakuteiStatus(true);
                        this.showDetail();
                        Cursor.Current = preCursor;

                        if (0 < result)
                        {
                            //確定登録完了メッセージを表示
                            this.msgLogic.MessageBoxShow("I001", "確定登録");
                        }
                        else if (0 == result)
                        {
                            //未登録メッセージを表示
                            this.msgLogic.MessageBoxShow("E046", "対象データが未変更", "確定");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            // 並べ替えPopUP表示
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            // 検索条件をセッティングファイルへ保存

            //拠点CD
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;

            DateTime resultDt;
            //伝票日付From
            //if (this.headForm.HIDUKE_FROM.Value == null)
            if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString();
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.headForm.HIDUKE_FROM.Text = string.Empty;
            }

            //伝票日付To
            //if (this.headForm.HIDUKE_TO.Value == null)
            if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text, out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString();
            }
            else
            {
                Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                this.headForm.HIDUKE_TO.Text = string.Empty;
            }

            // 日付区分
            Properties.Settings.Default.SET_HIDUKE_KBN = this.headForm.txtNum_HidukeSentaku.Text;

            // 保存実行
            Properties.Settings.Default.Save();

            // 画面Close
            this.parentForm.Close();
        }

        #endregion Functionキー押下イベント

        #region DataGridView系イベント
        /// <summary>
        /// DataGridView操作時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
            {
                if (this.form.Ichiran.RowCount > 0)
                {
                    if (this.form.Ichiran.Columns[e.ColumnIndex].Name == "KAKUTEI_KBN")
                    {
                        // 全選択チェックボックスが押下された場合、チェックボックス状態を反転する
                        this.form.KAKUTEI_KBN_CHECK_ALL.Checked = !this.form.KAKUTEI_KBN_CHECK_ALL.Checked;

                        // チェックボックスの全ての状態を書き換え
                        foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                        {
                            row.Cells["KAKUTEI_KBN"].Value = this.form.KAKUTEI_KBN_CHECK_ALL.Checked;
                        }

                        // 再描画
                        this.parentForm.txb_process.Focus();
                        this.form.Ichiran.Focus();
                        this.form.Ichiran.Refresh();
                    }
                }
            }
            //else
            //{
            //    // 編集行のセット
            //    var row = this.form.Ichiran.Rows[e.RowIndex];

            //    if (this.form.Ichiran.Columns[e.ColumnIndex].Name == "KAKUTEI_KBN")
            //    {
            //        // 確定区分チェックボックスが操作された場合、チェック内容を反転する
            //        if (row.Cells["KAKUTEI_KBN"].Value.ToString().Equals(CommonConst.KAKUTEI_KBN_KAKUTEI.ToString()))
            //        {
            //            row.Cells["KAKUTEI_KBN"].Value = false;
            //        }
            //        else
            //        {
            //            row.Cells["KAKUTEI_KBN"].Value = true;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// セル描画イベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 全選択CheckBoxの描画
            if (e.ColumnIndex == 0)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.ColumnIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - this.form.KAKUTEI_KBN_CHECK_ALL.Width) / 2, (bmp.Height - this.form.KAKUTEI_KBN_CHECK_ALL.Height + 28) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        this.form.KAKUTEI_KBN_CHECK_ALL.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルに描画
                        int x = ((e.CellBounds.Width - (this.form.KAKUTEI_KBN_CHECK_ALL.Width + 8)) / 2);
                        int y = ((e.CellBounds.Height - bmp.Height) / 2) - this.form.KAKUTEI_KBN_CHECK_ALL.Height;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion DataGridView系イベント

        #region その他イベント
        /// <summary>
        /// ESCキー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                this.parentForm.txb_process.Focus();
            }
        }

        #endregion その他イベント

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <returns>登録件数(例外発生時は-1)</returns>
        [Transaction]
        public virtual int setKakuteiStatus(bool procKbn)
        {
            int registNum = 0;

            try
            {
                // 登録用データリスト初期化
                this.ukeireEntryList = new List<T_UKEIRE_ENTRY>();
                this.ukeireEntryDeleteList = new List<T_UKEIRE_ENTRY>();
                this.ukeireDetailList = new List<T_UKEIRE_DETAIL>();
                this.shukkaEntryList = new List<T_SHUKKA_ENTRY>();
                this.shukkaEntryDeleteList = new List<T_SHUKKA_ENTRY>();
                this.shukkaDetailList = new List<T_SHUKKA_DETAIL>();
                this.kenshuDetailList = new List<T_KENSHU_DETAIL>();
                this.urshEntryList = new List<T_UR_SH_ENTRY>();
                this.urshEntryDeleteList = new List<T_UR_SH_ENTRY>();
                this.contenaResultEntryList = new List<T_CONTENA_RESULT>();
                this.contenaResultEntryDeleteList = new List<T_CONTENA_RESULT>();
                this.urshDetailList = new List<T_UR_SH_DETAIL>();
                this.zaikoList = new List<T_ZAIKO_HINMEI_HURIWAKE>();

                // 明細から登録用データ作成
                this.createCommitData(procKbn);

                // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                using (Transaction tran = new Transaction())
                {
                    if ((this.ukeireEntryList != null) && (this.ukeireEntryList.Count() > 0))
                    {
                        foreach (T_UKEIRE_DETAIL ukeire in this.ukeireDetailList)
                        {
                            registNum += this.ukeireDetailDao.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in this.ukeireEntryList)
                        {
                            registNum += this.ukeireEntryDao.Insert(ukeire);
                        }
                        foreach (T_UKEIRE_ENTRY ukeire in this.ukeireEntryDeleteList)
                        {
                            registNum += this.ukeireEntryDao.Update(ukeire);
                        }
                    }

                    if ((this.shukkaEntryList != null) && (this.shukkaEntryList.Count() > 0))
                    {
                        foreach (T_SHUKKA_DETAIL shuka in this.shukkaDetailList)
                        {
                            registNum += this.shukkaDetailDao.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in this.shukkaEntryList)
                        {
                            registNum += this.shukkaEntryDao.Insert(shuka);
                        }
                        foreach (T_SHUKKA_ENTRY shuka in this.shukkaEntryDeleteList)
                        {
                            registNum += this.shukkaEntryDao.Update(shuka);
                        }
                    }

                    if ((this.zaikoList != null) && (this.zaikoList.Count() > 0))
                    {
                        foreach (T_ZAIKO_HINMEI_HURIWAKE zaiko in this.zaikoList)
                        {
                            registNum += this.zaikoDao.Insert(zaiko);
                        }
                    }

                    if ((this.urshEntryList != null) && (this.urshEntryList.Count() > 0))
                    {
                        foreach (T_UR_SH_DETAIL ursh in this.urshDetailList)
                        {
                            registNum += this.urshDetailDao.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in this.urshEntryList)
                        {
                            registNum += this.urshEntryDao.Insert(ursh);
                        }
                        foreach (T_UR_SH_ENTRY ursh in this.urshEntryDeleteList)
                        {
                            registNum += this.urshEntryDao.Update(ursh);
                        }
                    }

                    if ((this.contenaResultEntryList != null) && (this.contenaResultEntryList.Count() > 0))
                    {
                        foreach (T_CONTENA_RESULT contena in this.contenaResultEntryList)
                        {
                            registNum += this.contenaEntryDao.Insert(contena);
                        }
                        foreach (T_CONTENA_RESULT contena in this.contenaResultEntryDeleteList)
                        {
                            registNum += this.contenaEntryDao.Update(contena);
                        }
                    }

                    if ((this.kenshuDetailList != null) && (this.kenshuDetailList.Count() > 0))
                    {
                        foreach (T_KENSHU_DETAIL kenshu in this.kenshuDetailList)
                        {
                            registNum += this.kenshuDao.Insert(kenshu);
                        }
                    }
                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                // 例外時は登録件数をマイナス
                registNum = -1;
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    this.msgLogic.MessageBoxShow("E080", "");

                }
                else if (ex is Seasar.Framework.Exceptions.SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }

            return registNum;
        }

        /// <summary>
        /// 登録用データ作成(受入)
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <param name="entityTable">登録対象受データ</param>
        private void createCommitDataForUkeire(bool procKbn, DataTable entityTable)
        {
            if (entityTable.Rows.Count != 0)
            {
                foreach (DataRow row in entityTable.Rows)
                {
                    Int16 kakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                    Int16 preKakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;

                    if (false == string.IsNullOrEmpty(row["KAKUTEI_KBN"].ToString()))
                    {
                        kakuteiKbn = Int16.Parse(row["KAKUTEI_KBN"].ToString());
                    }
                    if (false == string.IsNullOrEmpty(row["PRE_KAKUTEI_KBN"].ToString()))
                    {
                        preKakuteiKbn = Int16.Parse(row["PRE_KAKUTEI_KBN"].ToString());
                    }

                    if ((procKbn == true) && (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI) ||
                       (procKbn == false) && (kakuteiKbn != CommonConst.KAKUTEI_KBN_KAKUTEI))
                    {
                        // 登録時、変更対象に確定が設定されていた場合
                        // もしくは解除時、変更対象に未確定が設定されていた場合
                        if (kakuteiKbn != preKakuteiKbn)
                        {
                            // かつ、確定区分に変更があった場合は処理する

                            // 明細行のSYSTEM_IDとSEQから入力伝票を取得
                            var table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_UKEIRE_ENTRY");
                            var contenaTable = this.searchDenpyou("1", row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_CONTENA_RESULT");

                            // DataTableからEntityを生成
                            // ※SYSTEM_ID, SEQからの伝票を取得するため、データは唯一となる
                            var entryList = EntityUtility.DataTableToEntity<T_UKEIRE_ENTRY>(table);
                            var entryDeleteEntity = entryList[0];
                            entryDeleteEntity.TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];

                            // 確定処理前伝票の論理削除
                            entryDeleteEntity.DELETE_FLG = true;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            var bindLogic = new DataBinderLogic<T_UKEIRE_ENTRY>(entryDeleteEntity);
                            //bindLogic.SetSystemProperty(entryDeleteEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                            this.ukeireEntryDeleteList.Add(entryDeleteEntity);

                            // 確定処理後伝票を新規登録
                            entryList = EntityUtility.DataTableToEntity<T_UKEIRE_ENTRY>(table);
                            var entryEntity = entryList[0];
                            entryEntity.TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];
                            if (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI)
                            {
                                // 確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_KAKUTEI;
                            }
                            else
                            {
                                // 未確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                            }
                            bindLogic = new DataBinderLogic<T_UKEIRE_ENTRY>(entryEntity);
                            bindLogic.SetSystemProperty(entryEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            entryEntity.CREATE_DATE = entryDeleteEntity.CREATE_DATE;
                            entryEntity.CREATE_PC = entryDeleteEntity.CREATE_PC;
                            entryEntity.CREATE_USER = entryDeleteEntity.CREATE_USER;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                            entryEntity.SEQ += 1;
                            this.ukeireEntryList.Add(entryEntity);

                            // コンテナ稼動実績
                            var contenaEntryList = EntityUtility.DataTableToEntity<T_CONTENA_RESULT>(contenaTable);
                            var contenaDeleteEntryList = EntityUtility.DataTableToEntity<T_CONTENA_RESULT>(contenaTable);
                            for(int i = 0; i < contenaEntryList.Count(); i++)
                            {
                                // 確定処理前伝票の論理削除
                                contenaDeleteEntryList[i].TIME_STAMP = (byte[])contenaTable.Rows[i]["TIME_STAMP"];
                                contenaDeleteEntryList[i].DELETE_FLG = true;
                                var contenaBindLogic = new DataBinderLogic<T_CONTENA_RESULT>(contenaDeleteEntryList[i]);
                                this.contenaResultEntryDeleteList.Add(contenaDeleteEntryList[i]);

                                // 確定処理後伝票を新規登録
                                contenaBindLogic = new DataBinderLogic<T_CONTENA_RESULT>(contenaEntryList[i]);
                                contenaBindLogic.SetSystemProperty(contenaEntryList[i], false);
                                contenaEntryList[i].TIME_STAMP = (byte[])contenaTable.Rows[i]["TIME_STAMP"];
                                contenaEntryList[i].DELETE_FLG = false;
                                contenaEntryList[i].CREATE_DATE = contenaDeleteEntryList[i].CREATE_DATE;
                                contenaEntryList[i].CREATE_PC = contenaDeleteEntryList[i].CREATE_PC;
                                contenaEntryList[i].CREATE_USER = contenaDeleteEntryList[i].CREATE_USER;
                                contenaEntryList[i].SEQ += 1;
                                this.contenaResultEntryList.Add(contenaEntryList[i]);
                            }


                            // 明細行のSYSTEM_IDとSEQから明細伝票を取得
                            table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_UKEIRE_DETAIL");

                            // DataTableからEntityを生成
                            T_UKEIRE_DETAIL[] detailList = EntityUtility.DataTableToEntity<T_UKEIRE_DETAIL>(table);

                            // 入力伝票が新規登録されたため明細を紐付け直す
                            foreach (T_UKEIRE_DETAIL entity in detailList)
                            {
                                entity.SEQ = entryEntity.SEQ;
                                entity.KAKUTEI_KBN = entryEntity.KAKUTEI_KBN;
                                this.ukeireDetailList.Add(entity);
                            }

                            T_ZAIKO_HINMEI_HURIWAKE zaiko = new T_ZAIKO_HINMEI_HURIWAKE();
                            zaiko.SYSTEM_ID = entryEntity.SYSTEM_ID;
                            zaiko.SEQ = entryEntity.SEQ - 1;
                            zaiko.DENSHU_KBN_CD = 1;
                            List<T_ZAIKO_HINMEI_HURIWAKE> list = this.zaikoDao.GetZaikoInfo(zaiko);
                            foreach (T_ZAIKO_HINMEI_HURIWAKE entity in list)
                            {
                                entity.SEQ = entryEntity.SEQ;
                                this.zaikoList.Add(entity);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 登録用データ作成(出荷)
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <param name="entityTable">登録対象データ</param>
        private void createCommitDataForShukka(bool procKbn, DataTable entityTable)
        {
            if (entityTable.Rows.Count != 0)
            {
                foreach (DataRow row in entityTable.Rows)
                {
                    Int16 kakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                    Int16 preKakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;

                    if (false == string.IsNullOrEmpty(row["KAKUTEI_KBN"].ToString()))
                    {
                        kakuteiKbn = Int16.Parse(row["KAKUTEI_KBN"].ToString());
                    }
                    if (false == string.IsNullOrEmpty(row["PRE_KAKUTEI_KBN"].ToString()))
                    {
                        preKakuteiKbn = Int16.Parse(row["PRE_KAKUTEI_KBN"].ToString());
                    }

                    if ((procKbn == true) && (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI) ||
                       (procKbn == false) && (kakuteiKbn != CommonConst.KAKUTEI_KBN_KAKUTEI))
                    {
                        // 登録時、変更対象に確定が設定されていた場合
                        // もしくは解除時、変更対象に未確定が設定されていた場合
                        if (kakuteiKbn != preKakuteiKbn)
                        {
                            // かつ、確定区分に変更があった場合は処理する

                            // 明細行のSYSTEM_IDとSEQから入力伝票を取得
                            var table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_SHUKKA_ENTRY");

                            // DataTableからEntityを生成
                            // ※SYSTEM_ID, SEQからの伝票を取得するため、データは唯一となる
                            var entryList = EntityUtility.DataTableToEntity<T_SHUKKA_ENTRY>(table);
                            var entryDeleteEntity = entryList[0];
                            entryDeleteEntity.TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];

                            // 確定処理前伝票の論理削除
                            entryDeleteEntity.DELETE_FLG = true;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            var bindLogic = new DataBinderLogic<T_SHUKKA_ENTRY>(entryDeleteEntity);
                            //bindLogic.SetSystemProperty(entryDeleteEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                            this.shukkaEntryDeleteList.Add(entryDeleteEntity);

                            // 確定処理後伝票を新規登録
                            entryList = EntityUtility.DataTableToEntity<T_SHUKKA_ENTRY>(table);
                            var entryEntity = entryList[0];
                            entryEntity.DELETE_FLG = false;
                            if (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI)
                            {
                                // 確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_KAKUTEI;
                            }
                            else
                            {
                                // 未確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                            }
                            bindLogic = new DataBinderLogic<T_SHUKKA_ENTRY>(entryEntity);
                            bindLogic.SetSystemProperty(entryEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            entryEntity.CREATE_DATE = entryDeleteEntity.CREATE_DATE;
                            entryEntity.CREATE_PC = entryDeleteEntity.CREATE_PC;
                            entryEntity.CREATE_USER = entryDeleteEntity.CREATE_USER;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                            entryEntity.SEQ += 1;
                            this.shukkaEntryList.Add(entryEntity);

                            // 明細行のSYSTEM_IDとSEQから明細伝票を取得
                            table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_SHUKKA_DETAIL");

                            // DataTableからEntityを生成
                            T_SHUKKA_DETAIL[] detailList = EntityUtility.DataTableToEntity<T_SHUKKA_DETAIL>(table);

                            // 入力伝票が新規登録されたため明細を紐付け直す
                            foreach (T_SHUKKA_DETAIL entity in detailList)
                            {
                                entity.SEQ = entryEntity.SEQ;
                                entity.KAKUTEI_KBN = entryEntity.KAKUTEI_KBN;
                                this.shukkaDetailList.Add(entity);
                            }

                            //明細行のSYSTEM_IDとSEQから検収明細を取得
                            table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_KENSHU_DETAIL");

                            //検収明細がある場合DataTableからEntityを作成
                            if (table.Rows.Count > 0 && table != null)
                            {
                                T_KENSHU_DETAIL[] kenshuList = EntityUtility.DataTableToEntity<T_KENSHU_DETAIL>(table);

                                //入力伝票が新規登録されたため検収明細も紐付を行う
                                foreach (T_KENSHU_DETAIL entity in kenshuList)
                                {
                                    entity.SEQ = entryEntity.SEQ;
                                    this.kenshuDetailList.Add(entity);
                                }
                            }


                            T_ZAIKO_HINMEI_HURIWAKE zaiko = new T_ZAIKO_HINMEI_HURIWAKE();
                            zaiko.SYSTEM_ID = entryEntity.SYSTEM_ID;
                            zaiko.SEQ = entryEntity.SEQ - 1;
                            zaiko.DENSHU_KBN_CD = 2;
                            List<T_ZAIKO_HINMEI_HURIWAKE> list = this.zaikoDao.GetZaikoInfo(zaiko);
                            foreach (T_ZAIKO_HINMEI_HURIWAKE entity in list)
                            {
                                entity.SEQ = entryEntity.SEQ;
                                this.zaikoList.Add(entity);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 登録用データ作成(売上支払)
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <param name="entityTable">登録対象受入データ</param>
        private void createCommitDataForUrSh(bool procKbn, DataTable entityTable)
        {
            if (entityTable.Rows.Count != 0)
            {
                foreach (DataRow row in entityTable.Rows)
                {
                    Int16 kakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                    Int16 preKakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;

                    if (false == string.IsNullOrEmpty(row["KAKUTEI_KBN"].ToString()))
                    {
                        kakuteiKbn = Int16.Parse(row["KAKUTEI_KBN"].ToString());
                    }
                    if (false == string.IsNullOrEmpty(row["PRE_KAKUTEI_KBN"].ToString()))
                    {
                        preKakuteiKbn = Int16.Parse(row["PRE_KAKUTEI_KBN"].ToString());
                    }

                    if ((procKbn == true) && (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI) ||
                       (procKbn == false) && (kakuteiKbn != CommonConst.KAKUTEI_KBN_KAKUTEI))
                    {
                        // 登録時、変更対象に確定が設定されていた場合
                        // もしくは解除時、変更対象に未確定が設定されていた場合
                        if (kakuteiKbn != preKakuteiKbn)
                        {
                            // かつ、確定区分に変更があった場合は処理する

                            // 明細行のSYSTEM_IDとSEQから入力伝票を取得
                            var table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_UR_SH_ENTRY");
                            var contenaTable = this.searchDenpyou("3", row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_CONTENA_RESULT");

                            // DataTableからEntityを生成
                            // ※SYSTEM_ID, SEQからの伝票を取得するため、データは唯一となる
                            var entryList = EntityUtility.DataTableToEntity<T_UR_SH_ENTRY>(table);
                            var entryDeleteEntity = entryList[0];
                            entryDeleteEntity.TIME_STAMP = (byte[])table.Rows[0]["TIME_STAMP"];

                            // 確定処理前伝票の論理削除
                            entryDeleteEntity.DELETE_FLG = true;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            var bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(entryDeleteEntity);
                            //bindLogic.SetSystemProperty(entryDeleteEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            this.urshEntryDeleteList.Add(entryDeleteEntity);

                            // 確定処理後伝票を新規登録
                            entryList = EntityUtility.DataTableToEntity<T_UR_SH_ENTRY>(table);
                            var entryEntity = entryList[0];
                            entryEntity.DELETE_FLG = false;
                            if (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI)
                            {
                                // 確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_KAKUTEI;
                            }
                            else
                            {
                                // 未確定
                                entryEntity.KAKUTEI_KBN = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                            }
                            bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(entryEntity);
                            bindLogic.SetSystemProperty(entryEntity, false);
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　start
                            entryEntity.CREATE_DATE = entryDeleteEntity.CREATE_DATE;
                            entryEntity.CREATE_PC = entryDeleteEntity.CREATE_PC;
                            entryEntity.CREATE_USER = entryDeleteEntity.CREATE_USER;
                            /// 20141118 Houkakou 「更新日、登録日の見直し」　end
                            entryEntity.SEQ += 1;
                            this.urshEntryList.Add(entryEntity);

                            // コンテナ稼動実績
                            var contenaEntryList = EntityUtility.DataTableToEntity<T_CONTENA_RESULT>(contenaTable);
                            var contenaDeleteEntryList = EntityUtility.DataTableToEntity<T_CONTENA_RESULT>(contenaTable);
                            for (int i = 0; i < contenaEntryList.Count(); i++)
                            {
                                // 確定処理前伝票の論理削除
                                contenaDeleteEntryList[i].TIME_STAMP = (byte[])contenaTable.Rows[i]["TIME_STAMP"];
                                contenaDeleteEntryList[i].DELETE_FLG = true;
                                var contenaBindLogic = new DataBinderLogic<T_CONTENA_RESULT>(contenaDeleteEntryList[i]);
                                this.contenaResultEntryDeleteList.Add(contenaDeleteEntryList[i]);

                                // 確定処理後伝票を新規登録
                                contenaBindLogic = new DataBinderLogic<T_CONTENA_RESULT>(contenaEntryList[i]);
                                contenaBindLogic.SetSystemProperty(contenaEntryList[i], false);
                                contenaEntryList[i].TIME_STAMP = (byte[])contenaTable.Rows[i]["TIME_STAMP"];
                                contenaEntryList[i].DELETE_FLG = false;
                                contenaEntryList[i].CREATE_DATE = contenaDeleteEntryList[i].CREATE_DATE;
                                contenaEntryList[i].CREATE_PC = contenaDeleteEntryList[i].CREATE_PC;
                                contenaEntryList[i].CREATE_USER = contenaDeleteEntryList[i].CREATE_USER;
                                contenaEntryList[i].SEQ += 1;
                                this.contenaResultEntryList.Add(contenaEntryList[i]);
                            }

                            // 明細行のSYSTEM_IDとSEQから明細伝票を取得
                            table = this.searchDenpyou(row["SYSTEM_ID"].ToString(), row["SEQ"].ToString(), "T_UR_SH_DETAIL");

                            // DataTableからEntityを生成
                            T_UR_SH_DETAIL[] detailList = EntityUtility.DataTableToEntity<T_UR_SH_DETAIL>(table);

                            // 入力伝票が新規登録されたため明細を紐付け直す
                            foreach (T_UR_SH_DETAIL entity in detailList)
                            {
                                entity.SEQ = entryEntity.SEQ;
                                entity.KAKUTEI_KBN = entryEntity.KAKUTEI_KBN;
                                this.urshDetailList.Add(entity);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 明細内のデータ取得処理
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        private void createCommitData(bool procKbn)
        {
            var table = (DataTable)this.form.Ichiran.DataSource;

            // 分割用バッファ生成
            var ukeireTable = table.Clone();    //受入
            var shukkaTable = table.Clone();    //出荷    
            var urshTable = table.Clone();      //売上_支払

            foreach (DataRow row in table.Rows)
            {
                // 明細データを伝種区分毎に分割
                if (CommonConst.DENSHU_KBN_UKEIRE.ToString() == row["DENPYOU_SHURUI_CD"].ToString())
                {
                    // 伝種区分が「受入」
                    ukeireTable.ImportRow(row);
                }
                else if (CommonConst.DENSHU_KBN_SHUKKA.ToString() == row["DENPYOU_SHURUI_CD"].ToString())
                {
                    // 伝種区分が「出荷」
                    shukkaTable.ImportRow(row);
                }
                else if (CommonConst.DENSHU_KBN_UR_SH.ToString() == row["DENPYOU_SHURUI_CD"].ToString())
                {
                    // 伝種区分が「売上支払」
                    urshTable.ImportRow(row);
                }
            }

            // 登録用データ作成
            this.createCommitDataForUkeire(procKbn, ukeireTable);
            this.createCommitDataForShukka(procKbn, shukkaTable);
            this.createCommitDataForUrSh(procKbn, urshTable);
        }

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }

        /// <summary>
        /// 伝票検索処理
        /// </summary>
        /// <param name="system_id">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <param name="tableName">検索対象のテーブル名</param>
        /// <returns name="DataTable">該当する伝票のDataTable</returns>
        /// <remarks>指定されたテーブルよりシステムIDとSEQに該当する伝票情報を返却する</remarks>
        private DataTable searchDenpyou(string system_id, string seq, string tableName)
        {
            var table = new DataTable();
            var sql = new StringBuilder();

            // SQL文生成
            sql.Append(" SELECT * ");
            sql.Append(" FROM ");
            sql.Append(tableName);
            sql.Append(" WHERE ");
            sql.Append(" SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");

            // SQL発行
            table = this.kakuteiDao.getDataForStringSql(sql.ToString());
            return table;
        }

        /// <summary>
        /// 伝票検索処理(コンテナ稼動実績用）
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="system_id">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <param name="tableName">検索対象のテーブル名</param>
        /// <returns name="DataTable">該当する伝票のDataTable</returns>
        /// <remarks>指定されたテーブルよりシステムIDとSEQに該当する伝票情報を返却する</remarks>
        private DataTable searchDenpyou(string denshuKbn,string system_id, string seq, string tableName)
        {
            var table = new DataTable();
            var sql = new StringBuilder();

            // SQL文生成
            sql.Append(" SELECT * ");
            sql.Append(" FROM ");
            sql.Append(tableName);
            sql.Append(" WHERE ");
            sql.Append(" DENSHU_KBN_CD = " + "'" + denshuKbn + "'");
            sql.Append(" AND SYSTEM_ID = " + "'" + system_id + "'");
            sql.Append(" AND SEQ = " + "'" + seq + "'");
            sql.Append(" AND DELETE_FLG = 0");

            // SQL発行
            table = this.kakuteiDao.getDataForStringSql(sql.ToString());
            return table;
        }

        #region IF member
        public int Search()
        {
            throw new NotImplementedException();
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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion IF member

        /// <summary>
        /// 明細表示
        /// </summary>
        private bool showDetail()
        {
            try
            {
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return false;
                }
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

                // DataSourceセット
                var table = this.createDetailData();
                table.Columns["KAKUTEI_KBN"].ReadOnly = false;
                table.Columns["KAKUTEI_KBN"].AllowDBNull = true;
                this.form.Ichiran.DataSource = table;
                this.headForm.ReadDataNumber.Text = this.form.Ichiran.RowCount.ToString();

                this.form.customSortHeader1.SortDataTable(table);

                if (this.form.Ichiran.RowCount > 0)
                {
                    // 初期フォーカスセット
                    this.form.Ichiran.Focus();
                    this.form.Ichiran.CurrentCell = this.form.Ichiran.Rows[0].Cells[0];
                }

                // 不要項目を非表示
                this.form.Ichiran.Columns["DENPYOU_SHURUI_CD"].Visible = false;
                this.form.Ichiran.Columns["SYSTEM_ID"].Visible = false;
                this.form.Ichiran.Columns["SEQ"].Visible = false;
                this.form.Ichiran.Columns["KYOTEN_CD"].Visible = false;
                this.form.Ichiran.Columns["DENPYOU_KBN"].Visible = false;
                this.form.Ichiran.Columns["PRE_KAKUTEI_KBN"].Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("showDetail", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 明細表示用DataTableを生成する
        /// </summary>
        /// <returns name="DataTable">明細用DataTable</returns>
        private DataTable createDetailData()
        {
            // 検索条件の指定
            var dto = getSearchCondition();
            var table = this.kakuteiDao.GetIchiranData(dto);

            return table;
        }

        /// <summary>
        /// 画面の検索条件を設定したDTOを取得
        /// </summary>
        /// <returns name="DTOClass">検索条件DTO</returns>
        private DTOClass getSearchCondition()
        {
            var dto = new DTOClass();

            // 拠点
            if (!string.IsNullOrWhiteSpace(this.headForm.KYOTEN_CD.Text))
            {
                dto.KyotenCD = Int16.Parse(this.headForm.KYOTEN_CD.Text);
            }

            // 日付種類
            if (!string.IsNullOrWhiteSpace(this.headForm.txtNum_HidukeSentaku.Text))
            {
                dto.DateKbn = Int16.Parse(this.headForm.txtNum_HidukeSentaku.Text);
            }

            // 日付
            if (!string.IsNullOrWhiteSpace(this.headForm.HIDUKE_FROM.Text))
            {
                dto.DateFrom = DateTime.Parse(this.headForm.HIDUKE_FROM.Text).ToString("yyyy/MM/dd");
            }
            if (!string.IsNullOrWhiteSpace(this.headForm.HIDUKE_TO.Text))
            {
                dto.DateTo = DateTime.Parse(this.headForm.HIDUKE_TO.Text).ToString("yyyy/MM/dd");
            }

            // 確定区分
            if (!string.IsNullOrWhiteSpace(this.form.txtNum_KakuteiKbnSentaku.Text))
            {
                Int16 kakuteiKbn = Int16.Parse(this.form.txtNum_KakuteiKbnSentaku.Text);
                if (ConstClass.SEARCH_KAKUTEI_KBN_ALL == kakuteiKbn)
                {
                    dto.KakuteiKbn = SqlInt16.Null;
                }
                else if (ConstClass.SEARCH_KAKUTEI_KBN_MIKAKUTEI == kakuteiKbn)
                {
                    dto.KakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                }
                else if (ConstClass.SEARCH_KAKUTEI_KBN_KAKUTEI == kakuteiKbn)
                {
                    dto.KakuteiKbn = CommonConst.KAKUTEI_KBN_KAKUTEI;
                }
            }

            // 伝票種類
            dto.DenpyouShuruiUkeire = this.form.CheckBox_Jyunyu.Checked;
            dto.DenpyouShuruiShukka = this.form.CheckBox_Syuka.Checked;
            dto.DenpyouShuruiUrSh = this.form.CheckBox_Uriageshiharai.Checked;
            dto.DenpyouShuruiDainou = this.form.CheckBox_Dainou.Checked;

            // 伝票区分
            if (!string.IsNullOrWhiteSpace(this.form.txtNum_DenpyouKbnSentaku.Text))
            {
                Int16 denpyouKbn = Int16.Parse(this.form.txtNum_DenpyouKbnSentaku.Text);
                if (ConstClass.SEARCH_DENPYOU_KBN_ALL == denpyouKbn)
                {
                    dto.DenpyouKbn = SqlInt16.Null;
                }
                else if (ConstClass.SEARCH_DENPYOU_KBN_URIAGE == denpyouKbn)
                {
                    dto.DenpyouKbn = CommonConst.DENPYOU_KBN_URIAGE;
                }
                else if (ConstClass.SEARCH_DENPYOU_KBN_SHIHARAI == denpyouKbn)
                {
                    dto.DenpyouKbn = CommonConst.DENPYOU_KBN_SHIHARAI;
                }
            }

            return dto;
        }

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            try
            {
                this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
                // 入力されない場合
                if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.headForm.HIDUKE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.headForm.HIDUKE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                    this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    if ("1".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    if ("2".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }

                    this.headForm.HIDUKE_FROM.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.headForm.HIDUKE_FROM;
            var hidukeToTextBox = this.headForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141201 teikyou ダブルクリックを追加する　end
        #endregion

        #region 月次処理チェック

        /// <summary>
        /// 月次処理中かのチェックを行います
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <returns>True：月次処理中</returns>
        private bool CheckGetsujiShoriChu(bool procKbn, out bool catchErr)
        {
            bool val = false;
            catchErr = true;

            try
            {
                // 最新月次処理中データ取得
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                string strDate = getsujiShoriCheckLogic.GetLatestGetsjiShoriChuDateTime();

                if (!string.IsNullOrEmpty(strDate))
                {
                    DateTime shorichuDate = DateTime.Parse(strDate);

                    var table = (DataTable)this.form.Ichiran.DataSource;
                    foreach (DataRow row in table.Rows)
                    {
                        Int16 kakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                        Int16 preKakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;

                        if (!string.IsNullOrEmpty(row["KAKUTEI_KBN"].ToString()))
                        {
                            kakuteiKbn = Int16.Parse(row["KAKUTEI_KBN"].ToString());
                        }
                        if (!string.IsNullOrEmpty(row["PRE_KAKUTEI_KBN"].ToString()))
                        {
                            preKakuteiKbn = Int16.Parse(row["PRE_KAKUTEI_KBN"].ToString());
                        }

                        if (procKbn && (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI) ||
                            !procKbn && (kakuteiKbn != CommonConst.KAKUTEI_KBN_KAKUTEI))
                        {
                            if (kakuteiKbn != preKakuteiKbn)
                            {
                                DateTime denpyouDate = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                                if (denpyouDate.CompareTo(shorichuDate) <= 0)
                                {
                                    // 最新月次処理中年月の日付内の伝票日付を持つ伝票が1件でもいれば月次処理月次処理中とする
                                    // ※ロック対象外の日付を持つ伝票は実行可能とする動作にはしていない
                                    val = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGetsujiShoriChu", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGetsujiShoriChu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return val;
        }
        
        /// <summary>
        /// 月次処理によってロックされているかのチェックを行います
        /// </summary>
        /// <param name="procKbn">TRUE:確定登録, FALSE:確定解除</param>
        /// <returns>True：ロックされている　False：ロックされていない</returns>
        private bool CheckGetsujiShoriLock(bool procKbn, out bool catchErr)
        {
            bool val = false;
            catchErr = true;

            try
            {
                int year = 0;
                int month = 0;
                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                getsujiShoriCheckLogic.GetLatestGetsujiDate(ref year, ref month);

                if (year == 0 || month == 0)
                {
                    // 月次処理データ無し
                    return val;
                }

                // 月次年月を月末日に設定する
                DateTime getsuDate = new DateTime(year, month, 1);
                getsuDate = getsuDate.AddMonths(1).AddDays(-1);

                var table = (DataTable)this.form.Ichiran.DataSource;
                foreach (DataRow row in table.Rows)
                {
                    Int16 kakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;
                    Int16 preKakuteiKbn = CommonConst.KAKUTEI_KBN_MIKAKUTEI;

                    if (!string.IsNullOrEmpty(row["KAKUTEI_KBN"].ToString()))
                    {
                        kakuteiKbn = Int16.Parse(row["KAKUTEI_KBN"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["PRE_KAKUTEI_KBN"].ToString()))
                    {
                        preKakuteiKbn = Int16.Parse(row["PRE_KAKUTEI_KBN"].ToString());
                    }

                    if (procKbn && (kakuteiKbn == CommonConst.KAKUTEI_KBN_KAKUTEI) ||
                        !procKbn && (kakuteiKbn != CommonConst.KAKUTEI_KBN_KAKUTEI))
                    {
                        if (kakuteiKbn != preKakuteiKbn)
                        {
                            DateTime denpyouDate = DateTime.Parse(row["DENPYOU_DATE"].ToString());
                            if (denpyouDate.CompareTo(getsuDate) <= 0)
                            {
                                // 最新月次年月の日付内の伝票日付を持つ伝票が1件でもいれば月次処理によるロックとする
                                // ※ロック対象外の日付を持つ伝票は実行可能とする動作にはしていない
                                val = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGetsujiShoriLock", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGetsujiShoriLock", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return val;
        }

        #endregion
    }
}