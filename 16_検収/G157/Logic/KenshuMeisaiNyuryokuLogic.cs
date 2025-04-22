// $Id: UIForm.cs 4357 2013-10-22 00:18:55Z sys_dev_12 $
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
    /// <summary>
    /// 検収入力ロジック
    /// </summary>
    public class KenshuMeisaiNyuryokuLogic : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// 単位Dao
        /// </summary>
        internal IM_UNITDao unitDao;
        /// <summary>
        /// 品名Dao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;
        /// <summary>
        /// 消費税Dao
        /// </summary>
        private IM_SHOUHIZEIDao shouhizeiDao;
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 伝票区分Dao
        /// </summary>
        private IM_DENPYOU_KBNDao denpyouKbnDao;
        /// <summary>取引先請求Dao</summary>
        private IM_TORIHIKISAKI_SEIKYUUDao toriSeikyuu;
        /// <summary>取引先支払Dao</summary>
        private IM_TORIHIKISAKI_SHIHARAIDao toriShiharai;

        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// Form
        /// </summary>
        private KenshuMeisaiNyuryokuForm form;
        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;
        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;
        /// <summary>
        /// ribbon
        /// </summary>
        private RibbonMainMenu ribbon;
        /// <summary>
        /// 編集前の編集行
        /// </summary>
        private DataGridViewRow oldDgvRow;
        /// <summary>
        /// 売上金額合計
        /// </summary>
        private decimal uriKingakuTotal = 0;
        /// <summary>
        /// 品名売上金額合計
        /// </summary>
        private decimal hUriKingakuTotal = 0;
        /// <summary>
        /// 支払金額合計
        /// </summary>
        private decimal shiKingakuTotal = 0;
        /// <summary>
        /// 品名支払金額合計
        /// </summary>
        private decimal hShiKingakuTotal = 0;
        /// <summary>
        /// メッセージ表示ロジック
        /// </summary>
        MessageBoxShowLogic msgLogic;
        /// <summary>
        /// 伝種区分：2 出荷
        /// </summary>
        private short denshuSyukka = 2;

        /// <summary>
        /// F12閉じるボタンと×ボタンのどちらで閉じられたか判断します。
        /// true:F12閉じるボタン      false:×ボタン
        /// </summary>
        private bool closeTypeF12Flg = false;

        /// <summary>
        /// 画面を閉じている途中か判断するフラグです
        /// </summary>
        internal bool isClosing = false;

        /// <summary>
        /// 検収伝票日付前回値保持用
        /// </summary>
        private string tmpDenpyouDate = string.Empty;

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        r_framework.Dao.IM_KOBETSU_HINMEIDao kobetsuHinmei;
        r_framework.Dao.IM_HINMEIDao hinmei;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        public bool isRegisted = false;

        /// <summary>変更前売上日付</summary>
        private string beforeUrageDate = string.Empty;
        /// <summary>変更前支払日付</summary>
        private string beforeShiharaiDate = string.Empty;

        /// <summary>変更前数量</summary>
        private decimal? beforeSuuryou = null;
        /// <summary>変更前単価</summary>
        private decimal? beforeTanka = null;

        /// <summary>取引先請求情報</summary>
        private M_TORIHIKISAKI_SEIKYUU torihikisakiSeikyuu;
        /// <summary>取引先支払情報</summary>
        private M_TORIHIKISAKI_SHIHARAI torihikisakiShiharai;

        internal M_SYS_INFO sysInfoEntity;

        #endregion フィールド

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KenshuMeisaiNyuryokuLogic(KenshuMeisaiNyuryokuForm targetForm)
        {
            // フィールドの初期化
            this.form = targetForm;
            this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<IM_SHOUHIZEIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>();
            this.toriSeikyuu = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.toriShiharai = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.uriKingakuTotal = 0;
            this.hUriKingakuTotal = 0;
            this.shiKingakuTotal = 0;
            this.hShiKingakuTotal = 0;
            this.msgLogic = new MessageBoxShowLogic();
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.kobetsuHinmei = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEIDao>();
            this.hinmei = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
        }

        #endregion コンストラクタ

        #region 初期化
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                // ParentFormのSet
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // HeaderFormのSet
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // RibbonMenuのSet
                this.ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

                // Errorフラグの初期化
                this.form.RegistErrorFlag = false;

                // リボンメニュー非表示
                this.ribbonHide();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // ヘッダ項目設定
                this.headerInit();

                // Formサイズに合わせてDataGridViewサイズを動的に変更する
                this.form.KENSHU_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

                // 必要なデータをDBから取得
                this.setMasterData();

                this.GetSysInfoInit();

                // 明細セット
                this.showDetail(false);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            // ボタン名の初期化
            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        ///  イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            this.parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);
            this.parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);
            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.bt_func9_Click);
            this.parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);
            this.parentForm.bt_func11.Click += new EventHandler(this.bt_func11_Click);
            this.parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);
            this.parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);
            this.parentForm.lb_process.Click += new EventHandler(this.lb_process_Click);
            this.form.KENSHU_ICHIRAN.RowEnter += new DataGridViewCellEventHandler(this.KENSHU_ICHIRAN_RowEnter);
            this.form.KENSHU_ICHIRAN.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.KENSHU_ICHIRAN_CellBeginEdit);
            this.form.KENSHU_ICHIRAN.CellEndEdit += new DataGridViewCellEventHandler(this.KENSHU_ICHIRAN_CellEndEdit);
            this.form.KENSHU_ICHIRAN.CellValidating += new DataGridViewCellValidatingEventHandler(this.KENSHU_ICHIRAN_CellValidating);
            this.form.KENSHU_ICHIRAN.Validated += new EventHandler(this.KENSHU_ICHIRAN_Validated);
            this.form.KENSHU_ICHIRAN.CellEnter += new DataGridViewCellEventHandler(this.KENSHU_ICHIRAN_CellEnter);
            this.headerForm.KENSHU_DENPYOU_DATE.Enter += new EventHandler(this.KENSHU_DENPYOU_DATE_Enter);
            this.headerForm.KENSHU_DENPYOU_DATE.Validated += new EventHandler(this.KENSHU_DENPYOU_DATE_Validated);
            this.parentForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KenshuMeisaiNyuryokuForm_FormClosing);
            this.form.KENSHU_URIAGE_DATE.Enter += new EventHandler(KENSHU_URIAGE_DATE_Enter);
            this.form.KENSHU_SHIHARAI_DATE.Enter += new EventHandler(KENSHU_SHIHARAI_DATE_Enter);
            this.form.KENSHU_URIAGE_DATE.Validated += new EventHandler(KENSHU_URIAGE_DATE_Validated);
            this.form.KENSHU_SHIHARAI_DATE.Validated += new EventHandler(KENSHU_SHIHARAI_DATE_Validated);
        }

        /// <summary>
        /// ヘッダ項目初期設定
        /// </summary>
        /// <remarks>ヘッダ項目の初期設定を行う</remarks>
        private void headerInit()
        {

            if (this.form.returnDto.kenshuDetailList.Count <= 0)
            {
                // 検収明細が登録されていなければ新規モード
                this.setEditMode(WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            else
            {
                // 検収明細が登録されていれば修正モード
                this.setEditMode(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }

            // 消費税項目設定
            this.setShouhizeiCtrl();

            if (this.form.returnDto.shukkaEntryEntity == null)
            {
                // 検収伝票日付には本日をセット
                this.headerForm.KENSHU_DENPYOU_DATE.Value = this.parentForm.sysDate;

                // 消費税率取得
                var entity = this.getShouhizeiRate((DateTime)this.headerForm.KENSHU_DENPYOU_DATE.Value);

                // 日付系は検収伝票日付と同様
                this.form.KENSHU_URIAGE_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;
                this.form.KENSHU_SHIHARAI_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;

                // 消費税率は入力時点での税率を取得する
                this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = entity.SHOUHIZEI_RATE.ToString();
                this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = entity.SHOUHIZEI_RATE.ToString();
            }
            else
            {
                // 検収伝票日付
                if (false == this.form.returnDto.shukkaEntryEntity.KENSHU_DATE.IsNull)
                {
                    // 検収データが格納されていればそちらを用いる
                    this.headerForm.KENSHU_DENPYOU_DATE.Value = this.form.returnDto.shukkaEntryEntity.KENSHU_DATE;
                }
                else
                {
                    // 格納されていなければ本日をセット
                    this.headerForm.KENSHU_DENPYOU_DATE.Value = this.parentForm.sysDate;
                }

                // 消費税率取得
                var entity = this.getShouhizeiRate((DateTime)this.headerForm.KENSHU_DENPYOU_DATE.Value);

                // 検収売上日付
                if (false == this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_DATE.IsNull)
                {
                    // 検収データが格納されていればそちらを用いる
                    this.form.KENSHU_URIAGE_DATE.Value = this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_DATE;
                }
                else if (false == this.form.returnDto.shukkaEntryEntity.URIAGE_DATE.IsNull)
                {
                    // 出荷売上日付が格納されていればそちらを用いる
                    this.form.KENSHU_URIAGE_DATE.Value = this.form.returnDto.shukkaEntryEntity.URIAGE_DATE;
                }
                else
                {
                    // 日付は検収伝票日付と同様
                    this.form.KENSHU_URIAGE_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;
                }

                // 検収支払日付
                if (false == this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE.IsNull)
                {
                    // 検収データが格納されていればそちらを用いる
                    this.form.KENSHU_SHIHARAI_DATE.Value = this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE;
                }
                else if (false == this.form.returnDto.shukkaEntryEntity.SHIHARAI_DATE.IsNull)
                {
                    // 出荷支払日付が格納されていればそちらを用いる
                    this.form.KENSHU_SHIHARAI_DATE.Value = this.form.returnDto.shukkaEntryEntity.SHIHARAI_DATE;
                }
                else
                {
                    // 日付は検収伝票日付と同様
                    this.form.KENSHU_SHIHARAI_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;
                }

                // 検収売上消費税率
                if (false == this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.IsNull)
                {
                    // 検収データが格納されていればそちらを用いる
                    this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE.ToString();
                }
                else
                {
                    // 消費税率は入力時点での税率を取得する
                    this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = entity.SHOUHIZEI_RATE.ToString();
                }

                // 検収売上消費税率
                if (false == this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.IsNull)
                {
                    // 検収データが格納されていればそちらを用いる
                    this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE.ToString();
                }
                else
                {
                    // 消費税率は入力時点での税率を取得する
                    this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = entity.SHOUHIZEI_RATE.ToString();
                }

                //20211228 Thanh 158917 s
                this.form.TORIHIKISAKI_CD.Text = this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_CD;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_NAME;
                this.form.GYOUSHA_CD.Text = this.form.returnDto.shukkaEntryEntity.GYOUSHA_CD;
                this.form.GYOUSHA_NAME_RYAKU.Text = this.form.returnDto.shukkaEntryEntity.GYOUSHA_NAME;
                this.form.GENBA_CD.Text = this.form.returnDto.shukkaEntryEntity.GENBA_CD;
                this.form.GENBA_NAME_RYAKU.Text = this.form.returnDto.shukkaEntryEntity.GENBA_NAME;
                this.form.UNPAN_GYOUSHA_CD.Text = this.form.returnDto.shukkaEntryEntity.UNPAN_GYOUSHA_CD;
                this.form.UNPAN_GYOUSHA_NAME.Text = this.form.returnDto.shukkaEntryEntity.UNPAN_GYOUSHA_NAME;
                //20211228 Thanh 158917 e
            }

            // 消費税率文字列変換
            this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = this.percentForShouhizeiRate(this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text);
            this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = this.percentForShouhizeiRate(this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text);
        }

        #endregion 初期化

        #region Function系イベント
        /// <summary>
        /// F5押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            // 出荷複写
            this.shukkaCopy(sender, e);
        }

        /// <summary>
        /// F8押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            // 再読込
            this.reLoad(sender, e);
        }

        /// <summary>
        /// F9押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            // 金額チェック
            if (!this.form.RegistErrorFlag && !this.CheckCalcDetail())
            {
                // 数量*単価=金額かのチェック
                // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                new MessageBoxShowLogic().MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                this.form.RegistErrorFlag = true;
            }

            // Errorが発生していなければ登録実行
            if (false == this.form.RegistErrorFlag)
            {
                // 入力を確定させるためにフォーカスをはずす
                this.headerForm.KENSHU_DENPYOU_DATE.Focus();

                // 登録処理
                this.regist(sender, e);

                // 登録実行時はOKを返却
                this.form.DialogResult = DialogResult.OK;

                isRegisted = true;
                this.isClosing = true;

                /// 画面Close
                this.parentForm.Close();
            }

            isRegisted = false;
        }

        /// <summary>
        /// F10押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            // 行挿入
            this.insertRow(sender, e);
        }

        /// <summary>
        /// F11押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            // 行削除
            this.deleteRow(sender, e);
        }

        /// <summary>
        /// F12押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            // 取消処理
            this.cancel(sender, e);
        }

        /// <summary>
        /// フォームクロージングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KenshuMeisaiNyuryokuForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // 取消処理
            this.cancel(sender, e);
        }

        #endregion Function系イベント

        #region SubFunction系イベント
        /// <summary>
        /// SubFunction1クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            // 運賃入力画面Open
            this.openUnchinForm(sender, e);
        }

        /// <summary>
        /// 処理Noラベルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e"></param>
        private void lb_process_Click(object sender, EventArgs e)
        {
            // カーソルを処理No入力にセット
            this.parentForm.txb_process.Focus();
        }

        #endregion SubFunction系イベント

        #region DataGridView系イベント
        /// <summary>
        /// DataGridView行切替時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 明細行切替時処理
            this.detailRowEnterProc(sender, e);
        }

        /// <summary>
        /// DataGridView編集前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 明細編集後処理
            this.detailEditBeginProc(sender, e);
        }

        /// <summary>
        /// DataGridView編集後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 明細編集後処理
            this.detailEditAfterProc(sender, e);
        }

        /// <summary>
        /// DataGridViewCellValidating処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!this.isClosing)
            {
                // 明細Validating処理
                this.detailEditValidatingProc(sender, e);
            }
        }

        /// <summary>
        /// DataGridViewValidated処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_Validated(object sender, EventArgs e)
        {
            // 前回値更新
            if (this.form.KENSHU_ICHIRAN.CurrentRow != null)
            {
                this.oldDgvRowUpdate(this.form.KENSHU_ICHIRAN.CurrentRow.Index);
            }
        }

        /// <summary>
        /// DataGridViewCellEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KENSHU_ICHIRAN_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.form.KENSHU_ICHIRAN == null) return;
            if (this.form.KENSHU_ICHIRAN.Rows.Count <= 0) return;

            string colName = this.form.KENSHU_ICHIRAN.Columns[e.ColumnIndex].Name;

            if (colName.Equals("SUURYOU") || colName.Equals("TANKA"))
            {
                DataGridViewRow row = this.form.KENSHU_ICHIRAN.Rows[e.RowIndex];
                if (row.Cells["SUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()))
                {
                    this.beforeSuuryou = (decimal)row.Cells["SUURYOU"].Value;
                }
                else
                {
                    this.beforeSuuryou = null;
                }

                if (row.Cells["TANKA"].Value != null && !string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString()))
                {
                    this.beforeTanka = (decimal)row.Cells["TANKA"].Value;
                }
                else
                {
                    this.beforeTanka = null;
                }
            }
        }

        /// <summary>
        /// 検収伝票日付のValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KENSHU_DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            this.CheckDenpyouDate();

            var inputDenpyouDate = this.headerForm.KENSHU_DENPYOU_DATE.Text;
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                this.beforeUrageDate = this.form.KENSHU_URIAGE_DATE.Text;
                this.beforeShiharaiDate = this.form.KENSHU_SHIHARAI_DATE.Text;

                this.form.KENSHU_URIAGE_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;
                this.form.KENSHU_SHIHARAI_DATE.Value = this.headerForm.KENSHU_DENPYOU_DATE.Value;
                this.KENSHU_URIAGE_DATE_Validated(sender, e);
                this.KENSHU_SHIHARAI_DATE_Validated(sender, e);
            }
        }

        #endregion DataGridView系イベント


        #region Enter系イベント

        /// <summary>
        /// 検収伝票日付のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KENSHU_DENPYOU_DATE_Enter(object sender, EventArgs e)
        {
            this.DenpyouDateSet();
        }

        #endregion Enter系イベント

        #region IF member
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

        #endregion IF member

        /// <summary>
        /// 出荷複写処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shukkaCopy(object sender, EventArgs e)
        {
            // カレント行の取得
            var curRow = this.form.KENSHU_ICHIRAN.CurrentRow;

            // カレント行の値チェック
            DialogResult result = DialogResult.Yes;
            bool showMsg = false;
            foreach (DataGridViewCell cell in curRow.Cells)
            {
                var colName = this.form.KENSHU_ICHIRAN.Columns[cell.ColumnIndex].Name;
                switch (colName)
                {
                    case "ROW_NO":
                    case "SHUKKA_HINMEI":
                    case "SHUKKA_NET":
                        // 出荷時項目は対象外
                        break;
                    case "KINGAKU":
                    case "HINMEI_KINGAKU":
                        if (false == string.IsNullOrEmpty(cell.Value.ToString()) && Convert.ToDecimal(cell.Value) != 0)
                        {
                            // カレント行に何らかの値がセットされていれば、注意喚起メッセージを表示させる
                            result = this.msgLogic.MessageBoxShow("C055", ConstClass.copyConfirmMsg);
                            showMsg = true;
                        }
                        break;
                    default:
                        if (false == string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            // カレント行に何らかの値がセットされていれば、注意喚起メッセージを表示させる
                            result = this.msgLogic.MessageBoxShow("C055", ConstClass.copyConfirmMsg);
                            showMsg = true;
                        }
                        break;
                }

                if (showMsg == true)
                {
                    // 注意喚起メッセージが表示された場合は即座に抜ける
                    break;
                }
            }

            if (result == DialogResult.Yes)
            {
                // 注意喚起メッセージで「はい」を選択、もしくはカレント行に何も値がセットされていない場合

                decimal decInit = 0;

                // カレント行の出荷行Noに紐付く出荷明細より出荷品名を取得する
                var shukkaDetail = this.getShukkaDetail((Int16)curRow.Cells["ROW_NO"].Value);

                // 一旦クリア
                curRow.Cells["HINMEI_CD"].Value = string.Empty;
                curRow.Cells["HINMEI_NAME"].Value = string.Empty;
                curRow.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                curRow.Cells["BUBIKI"].Value = DBNull.Value;
                curRow.Cells["KENSHU_NET"].Value = DBNull.Value;
                curRow.Cells["SUURYOU"].Value = DBNull.Value;
                curRow.Cells["UNIT_NAME"].Value = string.Empty;
                curRow.Cells["UNIT_CD"].Value = DBNull.Value;
                curRow.Cells["TANKA"].Value = DBNull.Value;
                curRow.Cells["KINGAKU"].Value = DBNull.Value;
                curRow.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                curRow.Cells["HINMEI_ZEI_KBN_CD"].Value = DBNull.Value;
                curRow.Cells["HINMEI_KINGAKU"].Value = DBNull.Value;
                curRow.Cells["SHOW_KINGAKU"].Value = DBNull.Value;

                // 出荷明細より情報をコピー
                // 品名CD
                curRow.Cells["HINMEI_CD"].Value = shukkaDetail.HINMEI_CD;
                // 品名
                curRow.Cells["HINMEI_NAME"].Value = shukkaDetail.HINMEI_NAME;
                // 正味
                if (decimal.TryParse(shukkaDetail.NET_JYUURYOU.ToString(), out decInit))
                {
                    curRow.Cells["KENSHU_NET"].Value = decInit;
                }
                // 数量
                decimal.TryParse(shukkaDetail.SUURYOU.ToString(), out decInit);
                curRow.Cells["SUURYOU"].Value = decInit;
                // 単位CD、単位名
                if (false == shukkaDetail.UNIT_CD.IsNull)
                {
                    curRow.Cells["UNIT_CD"].Value = shukkaDetail.UNIT_CD.Value;
                    var unitEntity = this.unitDao.GetDataByCd(shukkaDetail.UNIT_CD.Value);
                    if (unitEntity != null)
                    {
                        if (false == string.IsNullOrEmpty(unitEntity.UNIT_NAME_RYAKU))
                        {
                            // 単位名のセット
                            curRow.Cells["UNIT_NAME"].Value = unitEntity.UNIT_NAME_RYAKU;
                        }
                    }
                }
                // 伝票区分CD、伝票区分名
                if (false == shukkaDetail.DENPYOU_KBN_CD.IsNull)
                {
                    curRow.Cells["DENPYOU_KBN_CD"].Value = shukkaDetail.DENPYOU_KBN_CD.Value;
                    var denKbnEntity = this.denpyouKbnDao.GetDataByCd(shukkaDetail.DENPYOU_KBN_CD.ToString());
                    if (denKbnEntity != null)
                    {
                        if (false == string.IsNullOrEmpty(denKbnEntity.DENPYOU_KBN_NAME_RYAKU))
                        {
                            // 伝票区分名のセット
                            curRow.Cells["DENPYOU_KBN_NAME"].Value = denKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                        }
                    }
                }
                // 品名税区分CD
                if (false == shukkaDetail.HINMEI_ZEI_KBN_CD.IsNull)
                {
                    curRow.Cells["HINMEI_ZEI_KBN_CD"].Value = shukkaDetail.HINMEI_ZEI_KBN_CD.Value;
                }
                // 単価
                if (!shukkaDetail.TANKA.IsNull)
                {
                    curRow.Cells["TANKA"].Value = shukkaDetail.TANKA.Value;
                }

                // 金額
                if (!shukkaDetail.KINGAKU.IsNull)
                {
                    curRow.Cells["KINGAKU"].Value = shukkaDetail.KINGAKU.Value;
                }
                // 品名金額
                if (!shukkaDetail.HINMEI_KINGAKU.IsNull)
                {
                    curRow.Cells["HINMEI_KINGAKU"].Value = shukkaDetail.HINMEI_KINGAKU.Value;
                }
                // 表示金額
                if (!string.IsNullOrEmpty(curRow.Cells["SUURYOU"].Value.ToString()) &&
                    !string.IsNullOrEmpty(curRow.Cells["TANKA"].Value.ToString()))
                {
                    curRow.Cells["SHOW_KINGAKU"].Value = this.calcKingaku(curRow);
                }
                else
                {
                    // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                    curRow.Cells["SHOW_KINGAKU"].Value = DBNull.Value;
                }

                // 数量の活性/非活性制御
                this.SetDetailSuuryouReadonly(curRow.Index);

                // 合計値更新
                this.setTotal();

                // 単価と金額の活性/非活性制御
                this.SetDetailReadOnly(curRow.Index);

                //20210826 Thanh 154360 s
                this.CopyRowShukkaToKenshu(curRow);
                //20210826 Thanh 154360 e

                // ラベルの切り替え
                this.LabelEdit();
            }
        }

        /// <summary>
        /// 再読込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reLoad(object sender, EventArgs e)
        {
            if (DialogResult.Yes == this.msgLogic.MessageBoxShow("C055", ConstClass.reLoadConfirmMsg))
            {
                // 再読込
                this.showDetail(true);
            }
        }

        /// <summary>
        /// 金額計算チェック
        /// 数量*単価の値と合計値が正しいかのチェック
        /// </summary>
        /// <returns>true：全明細正しい　false：正しくない行が存在する</returns>
        internal bool CheckCalcDetail()
        {
            LogUtility.DebugMethodStart();

            bool val = true;
            string suryouCellName = "SUURYOU";
            string tankaCellName = "TANKA";
            string kingakuCellName = "SHOW_KINGAKU";

            foreach (DataGridViewRow row in this.form.KENSHU_ICHIRAN.Rows)
            {
                if (row == null) continue;
                if (row.IsNewRow) continue;

                if (row.Cells[suryouCellName].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[suryouCellName].FormattedValue.ToString()) &&
                    row.Cells[tankaCellName].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[tankaCellName].FormattedValue.ToString()))
                {
                    decimal kingaku = decimal.Parse(row.Cells[kingakuCellName].FormattedValue.ToString());
                    decimal tmpKingaku = calcKingakuForRegistCheck(row);

                    if (!tmpKingaku.Equals(kingaku))
                    {
                        val = false;
                        break;
                    }

                }
            }

            LogUtility.DebugMethodEnd(val);
            return val;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void regist(object sender, EventArgs e)
        {
            // Formの内容をEntityに格納
            this.createEntityFromForm();

            // 明細の内容をEntityに格納
            this.createEntityFromDetail();
        }

        /// <summary>
        /// 行挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertRow(object sender, EventArgs e)
        {
            var curRow = this.form.KENSHU_ICHIRAN.CurrentRow;
            if (curRow == null)
            {
                // 行選択されていない場合はエラー
                this.msgLogic.MessageBoxShow("E029", "検収明細", "一覧");
            }
            else
            {
                if (curRow.Cells["ROW_NO"].Value == null)
                {
                    // 出荷行Noが存在しない場合はエラー
                    this.msgLogic.MessageBoxShow("E122");
                }
                else
                {
                    // 選択行を挿入した状態のDataTableを再構築
                    var oldTable = (DataTable)this.form.KENSHU_ICHIRAN.DataSource;
                    var newTable = oldTable.Clone();
                    for (int i = 0; i < oldTable.Rows.Count; i++)
                    {
                        // 順次コピー
                        //20151006 hoanghm #11993 start
                        //newTable.ImportRow(oldTable.Rows[i]);
                        var oldRow = newTable.NewRow();
                        for (int j = 0; j < oldTable.Columns.Count; j++)
                        {
                            oldRow[j] = oldTable.Rows[i][j];
                        }
                        newTable.Rows.Add(oldRow);
                        //20151006 hoanghm #11993 end

                        if (i == curRow.Index)
                        {
                            // コピー行が選択行だった場合、コピー行の次に行を挿入する
                            // 挿入行のデータは選択行の出荷系データを引き継ぐ
                            // ※出荷時正味は0とする
                            var row = newTable.NewRow();
                            row["ROW_NO"] = oldTable.Rows[i]["ROW_NO"];
                            row["SHUKKA_HINMEI"] = oldTable.Rows[i]["SHUKKA_HINMEI"];
                            row["SHUKKA_NET"] = DBNull.Value;
                            newTable.Rows.Add(row);
                        }
                    }

                    // DataSourceセット
                    // ※DataSourceのセットを行うとCurrentIndexが初期化されてしまうため退避しておく
                    var curIndex = curRow.Index;
                    this.form.KENSHU_ICHIRAN.DataSource = newTable;
                    // 初期フォーカスセット
                    this.form.KENSHU_ICHIRAN.CurrentCell = this.form.KENSHU_ICHIRAN.Rows[curIndex].Cells[0];

                    // 合計値更新
                    this.setTotal();
                }
            }
        }

        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteRow(object sender, EventArgs e)
        {
            var curRow = this.form.KENSHU_ICHIRAN.CurrentRow;
            if (curRow == null)
            {
                // 行選択されていない場合はエラー
                this.msgLogic.MessageBoxShow("E029", "検収明細", "一覧");
            }
            else
            {
                if (curRow.Cells["ROW_NO"].Value == null)
                {
                    // 出荷行Noが存在しない場合はエラー
                    this.msgLogic.MessageBoxShow("E123");
                }
                else
                {
                    if (true == this.defaultRowCheck(curRow.Index))
                    {
                        // 選択行が挿入によるデフォルト行の場合、削除できないためエラー
                        this.msgLogic.MessageBoxShow("E036");
                    }
                    else
                    {
                        // 削除しますか？
                        var result = this.msgLogic.MessageBoxShow("C024");
                        if (result == DialogResult.Yes)
                        {
                            // 選択行を削除する
                            this.form.KENSHU_ICHIRAN.Rows.RemoveAt(curRow.Index);

                            // 合計値更新
                            this.setTotal();

                            // ラベルの切り替え
                            this.LabelEdit();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 取消処理（F12閉じるボタン用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel(object sender, EventArgs e)
        {
            if (DialogResult.Yes == this.msgLogic.MessageBoxShowConfirm(ConstClass.cancelConfirmMsg))
            {
                /// 画面Close
                this.isClosing = true;
                this.closeTypeF12Flg = true;
                this.parentForm.Close();
            }
            else
            {
                this.closeTypeF12Flg = false;
            }
        }

        /// <summary>
        /// 取消処理（×ボタン用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!this.closeTypeF12Flg)
            {
                if (isRegisted)
                {
                    this.msgLogic.MessageBoxShow("I014");
                }
                else
                {
                    if (DialogResult.No == this.msgLogic.MessageBoxShowConfirm(ConstClass.cancelConfirmMsg))
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        this.isClosing = true;
                    }
                }
            }
        }


        /// <summary>
        /// 明細行切替時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailRowEnterProc(object sender, DataGridViewCellEventArgs e)
        {
            if (true == this.defaultRowCheck(e.RowIndex))
            {
                // デフォルト行だった場合は出荷複写を許容する
                this.parentForm.bt_func5.Enabled = true;
            }
            else
            {
                // デフォルト行以外の場合は出荷複写を許容しない
                this.parentForm.bt_func5.Enabled = false;
            }
        }

        /// <summary>
        /// 明細編集前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailEditBeginProc(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 編集予定行のセット
            this.oldDgvRowUpdate(e.RowIndex);
        }

        /// <summary>
        /// 明細編集後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailEditAfterProc(object sender, DataGridViewCellEventArgs e)
        {
            // 編集行のセット
            var row = this.form.KENSHU_ICHIRAN.Rows[e.RowIndex];

            // 正味を数量にセット
            if (this.setSuuryou(row))
            {
                // 単価の再設定はせず金額再計算のみしておく
                // 正味入力後、EnterやTab以外の操作を行うと金額計算が走らないので応急処置
                if (!string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()) &&
                   !string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString()))
                {
                    row.Cells["SHOW_KINGAKU"].Value = this.calcKingaku(row);
                }
                else
                {
                    // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                    row.Cells["SHOW_KINGAKU"].Value = DBNull.Value;
                }
            }

            if (true == this.rowDiffCheck(this.oldDgvRow, row) || ((DgvCustomTextBoxCell)row.Cells["HINMEI_CD"]).TextBoxChanged)
            {
                // 編集前と編集後にて差分があれば各再計算処理を行う
                switch (this.form.KENSHU_ICHIRAN.Columns[e.ColumnIndex].Name)
                {
                    case "UNIT_CD":
                    case "HINMEI_CD":
                    case "DENPYOU_KBN_CD":
                        // 伝票区分CD、品名CD、単位CDが存在した場合は個別品名単価⇒基本品名単価マスタの順番で単価を取得する
                        bool isNotTanka = false;
                        var tanka = this.getTanka(row, out isNotTanka);
                        if (isNotTanka)
                        {
                            row.Cells["TANKA"].Value = DBNull.Value;
                        }
                        else
                        {
                            row.Cells["TANKA"].Value = tanka;
                        }

                        // 金額再計算
                        if (!string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()) &&
                           !string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString()))
                        {
                            row.Cells["SHOW_KINGAKU"].Value = this.calcKingaku(row);
                        }
                        else
                        {
                            // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                            row.Cells["SHOW_KINGAKU"].Value = DBNull.Value;
                        }

                        break;
                    default:
                        // DO NOTHING
                        break;
                }

                // 一旦クリア
                row.Cells["HINMEI_KINGAKU"].Value = 0;
                row.Cells["KINGAKU"].Value = 0;

                if (false == string.IsNullOrEmpty(row.Cells["HINMEI_ZEI_KBN_CD"].Value.ToString()))
                {
                    // 品名税区分CDが存在する場合は表示用金額項目を「品名金額」へ
                    row.Cells["HINMEI_KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                }
                else
                {
                    // 品名税区分CDが存在しない場合は表示用金額項目を「金額」へ
                    row.Cells["KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                }

                // 合計値更新
                this.setTotal();

                // 単価と金額の活性/非活性制御
                this.SetDetailReadOnly(e.RowIndex);

                // ラベルの切り替え
                this.LabelEdit();

            }
        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal bool hinmeiPop(int rowIndex)
        {
            bool ret = true;
            try
            {
                M_KOBETSU_HINMEI keyEntity = new M_KOBETSU_HINMEI();
                keyEntity.GYOUSHA_CD = this.form.returnDto.shukkaEntryEntity.GYOUSHA_CD;
                keyEntity.GENBA_CD = this.form.returnDto.shukkaEntryEntity.GENBA_CD;
                keyEntity.HINMEI_CD = Convert.ToString(this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells["HINMEI_CD"].Value);

                M_KOBETSU_HINMEI kobetsuHinmeis = this.kobetsuHinmei.GetDataByCd(keyEntity);

                if (kobetsuHinmeis == null)
                {
                    keyEntity.GENBA_CD = "";
                    kobetsuHinmeis = this.kobetsuHinmei.GetDataByCd(keyEntity);
                }

                if (kobetsuHinmeis != null)
                {
                    this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells["HINMEI_NAME"].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
                }
                else
                {
                    M_HINMEI[] hinmeis = this.hinmei.GetAllValidData(new M_HINMEI() { HINMEI_CD = Convert.ToString(this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells["HINMEI_CD"].Value) });

                    if (hinmeis != null || hinmeis.Count() > 0)
                    {
                        this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells["HINMEI_NAME"].Value = hinmeis[0].HINMEI_NAME;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("hinmeiPop", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("hinmeiPop", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>
        /// 明細Validating処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailEditValidatingProc(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 編集行のセット
            var row = this.form.KENSHU_ICHIRAN.Rows[e.RowIndex];

            // 編集名のセット
            var colName = this.form.KENSHU_ICHIRAN.Columns[e.ColumnIndex].Name;

            switch (colName)
            {
                case "HINMEI_CD":

                    // 伝票区分セット状態
                    bool setKbn = false;

                    row.Cells["HINMEI_CD"].Value = (row.Cells["HINMEI_CD"].Value == null || string.IsNullOrEmpty(row.Cells["HINMEI_CD"].Value.ToString())) ? string.Empty : row.Cells["HINMEI_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                    if (this.form.KENSHU_ICHIRAN.EditingControl != null)
                    {
                        this.form.KENSHU_ICHIRAN.EditingControl.Text = row.Cells["HINMEI_CD"].Value.ToString();
                        if (string.IsNullOrEmpty(this.form.KENSHU_ICHIRAN.EditingControl.Text))
                        {
                            row.Cells["HINMEI_ZEI_KBN_CD"].Value = DBNull.Value;
                            row.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                            row.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                            row.Cells["UNIT_CD"].Value = DBNull.Value;
                            row.Cells["UNIT_NAME"].Value = string.Empty;
                            row.Cells["HINMEI_NAME"].Value = string.Empty;
                            row.Cells["TANKA"].Value = DBNull.Value;
                            break;
                        }
                    }

                    if (true == this.rowDiffCheck(this.oldDgvRow, row) || ((DgvCustomTextBoxCell)row.Cells["HINMEI_CD"]).TextBoxChanged)
                    {
                        if (false == string.IsNullOrEmpty(row.Cells["HINMEI_CD"].Value.ToString()))
                        {
                            M_HINMEI[] hinmeis1 = this.hinmei.GetAllValidData(new M_HINMEI() { HINMEI_CD = Convert.ToString(row.Cells["HINMEI_CD"].Value) });
                            if (hinmeis1 == null || hinmeis1.Count() < 1)
                            {
                                row.Cells["HINMEI_ZEI_KBN_CD"].Value = DBNull.Value;
                                row.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                                row.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                                row.Cells["UNIT_CD"].Value = DBNull.Value;
                                row.Cells["UNIT_NAME"].Value = string.Empty;
                                row.Cells["HINMEI_NAME"].Value = string.Empty;

                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                                return;
                            }
                            else
                            {
                                if (hinmeis1[0].DENSHU_KBN_CD.Value != 2 && hinmeis1[0].DENSHU_KBN_CD.Value != 9)
                                {
                                    row.Cells["HINMEI_ZEI_KBN_CD"].Value = DBNull.Value;
                                    row.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                                    row.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                                    row.Cells["UNIT_CD"].Value = DBNull.Value;
                                    row.Cells["UNIT_NAME"].Value = string.Empty;
                                    row.Cells["HINMEI_NAME"].Value = string.Empty;

                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E058");
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            M_KOBETSU_HINMEI keyEntity = new M_KOBETSU_HINMEI();
                            keyEntity.GYOUSHA_CD = this.form.returnDto.shukkaEntryEntity.GYOUSHA_CD;
                            keyEntity.GENBA_CD = this.form.returnDto.shukkaEntryEntity.GENBA_CD;
                            keyEntity.HINMEI_CD = Convert.ToString(row.Cells["HINMEI_CD"].Value);

                            keyEntity.ISNOT_NEED_DELETE_FLG = false;
                            M_KOBETSU_HINMEI[] kobetsuHinmeis = this.kobetsuHinmei.GetAllValidData(keyEntity);
                            if (kobetsuHinmeis == null || kobetsuHinmeis.Length == 0)
                            {
                                keyEntity.GENBA_CD = "";
                                kobetsuHinmeis = this.kobetsuHinmei.GetAllValidData(keyEntity);
                            }

                            if (kobetsuHinmeis != null && 0 < kobetsuHinmeis.Length)
                            {
                                row.Cells["HINMEI_NAME"].Value = kobetsuHinmeis[0].SEIKYUU_HINMEI_NAME;
                            }
                            else
                            {
                                M_HINMEI[] hinmeis = this.hinmei.GetAllValidData(new M_HINMEI() { HINMEI_CD = Convert.ToString(row.Cells["HINMEI_CD"].Value) });

                                if (hinmeis == null || hinmeis.Count() < 1)
                                {
                                    row.Cells["HINMEI_ZEI_KBN_CD"].Value = DBNull.Value;
                                    row.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                                    row.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                                    row.Cells["UNIT_CD"].Value = DBNull.Value;
                                    row.Cells["UNIT_NAME"].Value = string.Empty;
                                    row.Cells["HINMEI_NAME"].Value = string.Empty;

                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E020", "品名");
                                    e.Cancel = true;
                                    return;
                                }
                                else
                                {
                                    row.Cells["HINMEI_NAME"].Value = hinmeis[0].HINMEI_NAME;
                                }
                            }

                            // 検収品名CDより品名税区分CD、伝票区分CD、単位、単価を取得
                            var hinmeiEntity = this.hinmeiDao.GetDataByCd(row.Cells["HINMEI_CD"].Value.ToString());
                            if (hinmeiEntity != null)
                            {
                                if (false == hinmeiEntity.ZEI_KBN_CD.IsNull)
                                {
                                    // 品名税区分CDのセット
                                    row.Cells["HINMEI_ZEI_KBN_CD"].Value = hinmeiEntity.ZEI_KBN_CD.Value;
                                }

                                if (((DgvCustomTextBoxCell)row.Cells["HINMEI_CD"]).TextBoxChanged)
                                {
                                    if (false == hinmeiEntity.UNIT_CD.IsNull)
                                    {
                                        // 単位CD、単位名のセット
                                        row.Cells["UNIT_CD"].Value = hinmeiEntity.UNIT_CD.Value;
                                        var unitEntity = this.unitDao.GetDataByCd((int)hinmeiEntity.UNIT_CD);
                                        row.Cells["UNIT_NAME"].Value = unitEntity.UNIT_NAME_RYAKU;
                                    }
                                }
                                // 正味を数量にセット
                                this.setSuuryou(row);

                                if ((hinmeiEntity.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_URIAGE) || (hinmeiEntity.DENPYOU_KBN_CD == CommonConst.DENPYOU_KBN_SHIHARAI))
                                {
                                    // 品名マスタに伝票区分CDが登録されている場合はそれを用いる
                                    // ※伝票区分が「売上」「支払」の場合
                                    row.Cells["DENPYOU_KBN_CD"].Value = hinmeiEntity.DENPYOU_KBN_CD.Value;
                                    var denKbnEntity = this.denpyouKbnDao.GetDataByCd(hinmeiEntity.DENPYOU_KBN_CD.ToString());
                                    if (denKbnEntity != null)
                                    {
                                        if (false == string.IsNullOrEmpty(denKbnEntity.DENPYOU_KBN_NAME_RYAKU))
                                        {
                                            // 伝票区分名のセット
                                            row.Cells["DENPYOU_KBN_NAME"].Value = denKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                                        }
                                    }

                                    // 伝票区分セット完了
                                    setKbn = true;
                                }
                            }
                        }
                    }

                    var targetRow = this.form.KENSHU_ICHIRAN.CurrentRow;
                    if (targetRow != null)
                    {
                        DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                        if (control.TextBoxChanged == true && !setKbn)
                        {
                            targetRow.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value; // 伝票区分をクリア
                        }
                    }

                    var isDenpyouKbnEnpty = string.IsNullOrEmpty(Convert.ToString(this.form.KENSHU_ICHIRAN["DENPYOU_KBN_CD", e.RowIndex].Value));
                    var ishinmeiCdEnpty = string.IsNullOrEmpty(Convert.ToString(this.form.KENSHU_ICHIRAN["HINMEI_NAME", e.RowIndex].Value));
                    if (isDenpyouKbnEnpty && !ishinmeiCdEnpty && setKbn == false)
                    {
                        // 伝票区分CDがセットされなかった場合は伝票区分CD選択PopUpから選択する
                        CustomControlExtLogic.PopUp((ICustomControl)row.Cells["DENPYOU_KBN_CD"]);

                        if ((row.Cells["DENPYOU_KBN_CD"].Value == null) || (true == string.IsNullOrEmpty(row.Cells["DENPYOU_KBN_CD"].Value.ToString())))
                        {
                            // キャンセルされた場合は値のキャンセルを行う
                            row.Cells["DENPYOU_KBN_CD"].Value = DBNull.Value;
                            row.Cells["DENPYOU_KBN_NAME"].Value = string.Empty;
                            e.Cancel = true;
                        }
                    }

                    break;
                case "TANKA":
                case "SUURYOU":
                    // 金額再計算
                    if (true == this.rowDiffCheck(this.oldDgvRow, row))
                    {
                        if (!string.IsNullOrEmpty(row.Cells["SUURYOU"].EditedFormattedValue.ToString()) &&
                           !string.IsNullOrEmpty(row.Cells["TANKA"].EditedFormattedValue.ToString()))
                        {
                            row.Cells["SHOW_KINGAKU"].Value = this.calcKingaku(row);

                            // table構成上、金額が設定されるのはKINGAKUかHINMEI_KINGAKUのどちらか一方。
                            // そのため、そのどちらか一方をSHOW_KINGAKUとして設定している。
                            // SHOW_KINGAKUが変更になる場合、KINGAKU、HINMEI_KINGAKUを再設定する必要がある。
                            if (false == string.IsNullOrEmpty(row.Cells["HINMEI_ZEI_KBN_CD"].Value.ToString()))
                            {
                                // 品名税区分CDが存在する場合は表示用金額項目を「品名金額」へ
                                row.Cells["HINMEI_KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                            }
                            else
                            {
                                // 品名税区分CDが存在しない場合は表示用金額項目を「金額」へ
                                row.Cells["KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                            }
                        }
                        else
                        {
                            decimal? suuryouValue = null;
                            decimal? tankaValue = null;
                            if (row.Cells["SUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()))
                            {
                                suuryouValue = (decimal)row.Cells["SUURYOU"].Value;
                            }
                            if (row.Cells["TANKA"].Value != null && !string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString()))
                            {
                                tankaValue = (decimal)row.Cells["TANKA"].Value;
                            }

                            if (beforeSuuryou != suuryouValue || beforeTanka != tankaValue)
                            {
                                // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                                row.Cells["SHOW_KINGAKU"].Value = DBNull.Value;

                                // table構成上、金額が設定されるのはKINGAKUかHINMEI_KINGAKUのどちらか一方。
                                // そのため、そのどちらか一方をSHOW_KINGAKUとして設定している。
                                // SHOW_KINGAKUが変更になる場合、KINGAKU、HINMEI_KINGAKUを再設定する必要がある。
                                if (false == string.IsNullOrEmpty(row.Cells["HINMEI_ZEI_KBN_CD"].Value.ToString()))
                                {
                                    // 品名税区分CDが存在する場合は表示用金額項目を「品名金額」へ
                                    row.Cells["HINMEI_KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                                }
                                else
                                {
                                    // 品名税区分CDが存在しない場合は表示用金額項目を「金額」へ
                                    row.Cells["KINGAKU"].Value = row.Cells["SHOW_KINGAKU"].Value;
                                }
                            }
                        }

                        // SHOW_KINGAKUが変更になるので合計値更新メソッドを呼び出す。
                        this.setTotal();
                    }

                    break;
                default:
                    // DO NOTHING
                    break;
            }

            // 単価と金額の活性/非活性制御
            this.SetDetailReadOnly(e.RowIndex);
        }

        /// <summary>
        /// 運賃入力遷移処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openUnchinForm(object sender, EventArgs e)
        {
            // 運賃入力画面(G153)をモーダル表示
            // 20150723 Go 引数順番修正 Start
            //FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, this.form.returnDto.shukkaEntryEntity.SYSTEM_ID);
            FormManager.OpenFormModal("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, this.form.returnDto.shukkaEntryEntity.SHUKKA_NUMBER, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA);
            // 20150723 Go 引数順番修正 End
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            // ButtonSetting.xmlよりボタン情報の読込
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.ButtonInfoXmlPath);
        }

        #region 必須データ取得処理
        /// <summary>
        /// 必要なマスタデータを設定する
        /// </summary>
        private void setMasterData()
        {
            // 初期化
            this.torihikisakiSeikyuu = new M_TORIHIKISAKI_SEIKYUU();
            this.torihikisakiShiharai = new M_TORIHIKISAKI_SHIHARAI();

            if (!string.IsNullOrEmpty(this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_CD))
            {
                this.torihikisakiSeikyuu = this.toriSeikyuu.GetDataByCd(this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_CD);
                this.torihikisakiShiharai = this.toriShiharai.GetDataByCd(this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_CD);
            }
        }
        #endregion

        /// <summary>
        /// 明細表示
        /// </summary>
        /// <param name="reLoadFlg">再読込フラグ TRUE:再読込実行時</param>
        /// <remarks>出荷入力画面より渡された情報を基に明細表示を行う</remarks>
        private void showDetail(bool reLoadFlg)
        {
            // DataSourceセット
            var table = this.createDetailData(reLoadFlg);
            this.form.KENSHU_ICHIRAN.DataSource = table;

            // ファンクションキー状態セット
            setFunctionKeyStatus();

            if (this.form.KENSHU_ICHIRAN.RowCount > 0)
            {
                // 初期フォーカスセット
                this.form.KENSHU_ICHIRAN.Focus();
                this.form.KENSHU_ICHIRAN.CurrentCell = this.form.KENSHU_ICHIRAN.Rows[0].Cells[0];

                // 不要項目を非表示
                this.form.KENSHU_ICHIRAN.Columns["HINMEI_ZEI_KBN_CD"].Visible = false;
                this.form.KENSHU_ICHIRAN.Columns["KINGAKU"].Visible = false;
                this.form.KENSHU_ICHIRAN.Columns["HINMEI_KINGAKU"].Visible = false;
            }

            // 合計値更新
            this.setTotal();

            // ラベルの切り替え
            this.LabelEdit();
        }

        /// <summary>
        /// ラベル切り換え
        /// </summary>
        /// <remarks>売上日付、支払日付のラベルを変更する</remarks>
        private void LabelEdit()
        {

            // ラベル表記、必須チェックを一旦初期化
            this.form.KENSHU_URIAGE_DATE_LBL.Text = this.form.KENSHU_URIAGE_DATE_LBL.Text.Replace("※", "");
            this.form.KENSHU_SHIHARAI_DATE_LBL.Text = this.form.KENSHU_SHIHARAI_DATE_LBL.Text.Replace("※", "");
            this.form.KENSHU_URIAGE_DATE.RegistCheckMethod = null;
            this.form.KENSHU_SHIHARAI_DATE.RegistCheckMethod = null;

            // 必須チェック設定
            var existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            var existChecks = new Collection<SelectCheckDto>();
            existChecks.Add(existCheck);

			// 伝票区分の値により、検収日付の必須チェックを有効にする
            var table = (DataTable)this.form.KENSHU_ICHIRAN.DataSource;
            foreach (DataRow row in table.Rows)
            {
                if (false == string.IsNullOrEmpty(row["DENPYOU_KBN_CD"].ToString()))
                {
                    if (CommonConst.DENPYOU_KBN_URIAGE == (Int16)row["DENPYOU_KBN_CD"])
                    {
                        if (false == this.form.KENSHU_URIAGE_DATE_LBL.Text.Contains("※"))
                        {
                            // 必須ラベルに変更
                            this.form.KENSHU_URIAGE_DATE_LBL.Text += "※";

                            // 必須チェック設定
                            this.form.KENSHU_URIAGE_DATE.RegistCheckMethod = existChecks;
                        }
                    }
                    else if (CommonConst.DENPYOU_KBN_SHIHARAI == (Int16)row["DENPYOU_KBN_CD"])
                    {
                        if (false == this.form.KENSHU_SHIHARAI_DATE_LBL.Text.Contains("※"))
                        {
                            // 必須ラベルに変更
                            this.form.KENSHU_SHIHARAI_DATE_LBL.Text += "※";

                            // 必須チェック設定
                            this.form.KENSHU_SHIHARAI_DATE.RegistCheckMethod = existChecks;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 消費税項目設定
        /// </summary>
        /// <remarks>消費税検索関連の設定を行う</remarks>
        private void setShouhizeiCtrl()
        {
            // 消費税率検索用DataSource生成
            var shouhizeiRates = this.shouhizeiDao.GetAllData();
            var table = EntityUtility.EntityToDataTable(shouhizeiRates);
            var shouhizeiTable = new DataTable();
            foreach (var col in this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()))
            {
                // PopupGetMasterFieldに登録されている各メンバ名にてDataTableを生成
                shouhizeiTable.Columns.Add(table.Columns[col].ColumnName, table.Columns[col].DataType);
            }
            foreach (DataRow row in table.Rows)
            {
                // 消費税Tableよりカラム名と一致するカラムの値を格納する
                shouhizeiTable.Rows.Add(shouhizeiTable.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
            }
            shouhizeiTable.TableName = "消費税率";

            // DataSourceセット
            this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.PopupDataHeaderTitle = new string[] { "消費税率" };
            this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.PopupDataSource = shouhizeiTable;
            this.form.KENSHU_URIAGE_SHOUHIZEI_RATE_SEARCH.PopupDataHeaderTitle = new string[] { "消費税率" };
            this.form.KENSHU_URIAGE_SHOUHIZEI_RATE_SEARCH.PopupDataSource = shouhizeiTable;
            this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.PopupDataHeaderTitle = new string[] { "消費税率" };
            this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.PopupDataSource = shouhizeiTable;
            this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE_SEARCH.PopupDataHeaderTitle = new string[] { "消費税率" };
            this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE_SEARCH.PopupDataSource = shouhizeiTable;
        }

        /// <summary>
        /// 消費税取得
        /// </summary>
        /// <param name="date">対象日付</param>
        /// <returns name="M_SHOUHIZEI">消費税Entity</returns>
        /// <remarks>
        /// 対象日付期間に該当する消費税率を取得する
        /// 取得できなかった場合はNULLを返却
        /// </remarks>
        private M_SHOUHIZEI getShouhizeiRate(DateTime date)
        {
            M_SHOUHIZEI returnEntity = null;

            if (date == null)
            {
                // 該当なし
                return returnEntity;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string selectStr = "SELECT * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + date + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + date + "', 111), 120) <= ISNULL(TEKIYOU_END,'9999/12/31')");
            sb.Append("  )");
            sb.Append(" )");
            whereStr = whereStr + sb.ToString();

            // SQL実行
            dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                // 該当なし
                return returnEntity;
            }

            // Entity構築
            var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();
            var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
            returnEntity = shouhizeis[0];

            return returnEntity;
        }

        /// <summary>
        /// 消費税率をパーセント表記で取得する
        /// </summary>
        /// <param name="rate">消費税率文字列</param>
        /// <returns>パーセント表示の消費税率</returns>
        internal string percentForShouhizeiRate(string rate)
        {
            string returnVal = string.Empty;

            if (!string.IsNullOrEmpty(rate))
            {
                decimal shouhizeiRate = 0;
                if ((false == rate.Contains("%")) && (decimal.TryParse(rate, out shouhizeiRate)))
                {
                    // パーセント換算値を算出し、パーセント表記に変換
                    returnVal = shouhizeiRate.ToString("P");
                }
                else if (true == rate.Contains("%"))
                {
                    // 既に%表記ならそのまま返す
                    returnVal = rate;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// パーセント表記の消費税率文字列から消費税率を取得する
        /// </summary>
        /// <param name="rate">消費税率文字列</param>
        /// <returns>消費税率値</returns>
        internal decimal valueForShouhizeiRate(string rate)
        {
            decimal returnVal = 0;

            if (!string.IsNullOrEmpty(rate))
            {
                // パーセント記号を削除
                var str = rate.Replace("%", "");

                // 表示消費税率から税率値を算出
                decimal shouhizeiRate = 0;
                if (true == decimal.TryParse(str, out shouhizeiRate))
                {
                    returnVal = shouhizeiRate / 100;
                }
            }

            return returnVal;
        }

        /// <summary>
        /// 明細表示用DataTableを生成する
        /// </summary>
        /// <param name="reLoadFlg">再読込フラグ TRUE:再読込実行時</param>
        /// <returns name="DataTable">明細用DataTable</returns>
        private DataTable createDetailData(bool reLoadFlg)
        {
            var table = new DataTable();
            DataRow row;

            // 表示用列作成
            table.Columns.Add("ROW_NO", typeof(Int16));
            table.Columns.Add("SHUKKA_HINMEI", typeof(string));
            table.Columns.Add("SHUKKA_NET", typeof(decimal));
            table.Columns.Add("HINMEI_CD", typeof(string));
            table.Columns.Add("HINMEI_NAME", typeof(string));
            table.Columns.Add("DENPYOU_KBN_NAME", typeof(string));
            table.Columns.Add("BUBIKI", typeof(decimal));
            table.Columns.Add("KENSHU_NET", typeof(decimal));
            table.Columns.Add("SUURYOU", typeof(decimal));
            table.Columns.Add("UNIT_NAME", typeof(string));
            table.Columns.Add("UNIT_CD", typeof(Int16));
            table.Columns.Add("TANKA", typeof(decimal));
            table.Columns.Add("KINGAKU", typeof(decimal));
            table.Columns.Add("DENPYOU_KBN_CD", typeof(Int16));
            table.Columns.Add("HINMEI_ZEI_KBN_CD", typeof(Int16));
            table.Columns.Add("HINMEI_KINGAKU", typeof(decimal));
            table.Columns.Add("SHOW_KINGAKU", typeof(decimal));

            if ((reLoadFlg == true) || (this.form.returnDto.kenshuDetailList.Count <= 0))
            {
                // 検収入力リストが存在しない場合、もしくは、再読込実行時
                if (this.form.returnDto.shukkaDetailList.Count > 0)
                {
                    // 出荷入力リストからDataTableを作成
                    // ※セットするのは出荷関連のみ
                    foreach (var entity in this.form.returnDto.shukkaDetailList)
                    {
                        row = table.NewRow();

                        // 出荷行No
                        if (false == entity.ROW_NO.IsNull)
                        {
                            row["ROW_NO"] = (Int16)entity.ROW_NO;
                        }
                        else
                        {
                            row["ROW_NO"] = 0;
                        }

                        // 出荷時正味
                        if (false == entity.NET_JYUURYOU.IsNull)
                        {
                            row["SHUKKA_NET"] = decimal.Parse(entity.NET_JYUURYOU.ToString());
                        }
                        else
                        {
                            //row["SHUKKA_NET"] = 0;
                        }

                        // その他文字列
                        row["SHUKKA_HINMEI"] = entity.HINMEI_NAME;

                        // 行追加
                        table.Rows.Add(row);
                    }
                }
            }
            else
            {
                // 検収入力リストが存在する場合は検収入力リストからDataTableを作成
                foreach (var entity in this.form.returnDto.kenshuDetailList)
                {
                    row = table.NewRow();
                    //出荷行No
                    if (false == entity.ROW_NO.IsNull)
                    {
                        row["ROW_NO"] = (Int16)entity.ROW_NO;
                    }
                    else
                    {
                        row["ROW_NO"] = 0;
                    }


                    // 出荷時正味
                    if (false == entity.SHUKKA_NET.IsNull)
                    {
                        row["SHUKKA_NET"] = decimal.Parse(entity.SHUKKA_NET.ToString());
                    }
                    else
                    {
                        //row["SHUKKA_NET"] = 0;
                    }


                    // 歩引き
                    if (false == entity.BUBIKI.IsNull)
                    {
                        row["BUBIKI"] = (decimal)entity.BUBIKI;
                    }
                    else
                    {
                        //row["BUBIKI"] = 0;
                    }

                    // 正味
                    if (false == entity.KENSHU_NET.IsNull)
                    {
                        row["KENSHU_NET"] = decimal.Parse(entity.KENSHU_NET.ToString());
                    }
                    else
                    {
                        //row["KENSHU_NET"] = 0;
                    }

                    // 数量
                    if (false == entity.SUURYOU.IsNull)
                    {
                        row["SUURYOU"] = decimal.Parse(entity.SUURYOU.ToString());
                    }
                    else
                    {
                        row["SUURYOU"] = 0;
                    }

                    // 単位CD
                    if (false == entity.UNIT_CD.IsNull)
                    {
                        row["UNIT_CD"] = (Int16)entity.UNIT_CD;

                        // 単位名取得
                        var unit = this.unitDao.GetDataByCd((int)entity.UNIT_CD);
                        row["UNIT_NAME"] = unit.UNIT_NAME_RYAKU;
                    }
                    else
                    {
                        row["UNIT_CD"] = 0;
                        row["UNIT_NAME"] = "";
                    }

                    // 単価
                    if (false == entity.TANKA.IsNull)
                    {
                        row["TANKA"] = (decimal)entity.TANKA;
                    }
                    else
                    {
                        //row["TANKA"] = 0;
                    }

                    // 伝票区分CD
                    if (false == entity.DENPYOU_KBN_CD.IsNull)
                    {
                        row["DENPYOU_KBN_CD"] = (Int16)entity.DENPYOU_KBN_CD;
                        var denKbnEntity = this.denpyouKbnDao.GetDataByCd(entity.DENPYOU_KBN_CD.ToString());
                        if (denKbnEntity != null)
                        {
                            if (false == string.IsNullOrEmpty(denKbnEntity.DENPYOU_KBN_NAME_RYAKU))
                            {
                                // 伝票区分名のセット
                                row["DENPYOU_KBN_NAME"] = denKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                            }
                        }
                    }

                    // 金額
                    if (false == entity.KINGAKU.IsNull)
                    {
                        row["KINGAKU"] = (decimal)entity.KINGAKU;
                    }
                    else
                    {
                        row["KINGAKU"] = 0;
                    }

                    // 品名別金額
                    if (false == entity.HINMEI_KINGAKU.IsNull)
                    {
                        row["HINMEI_KINGAKU"] = (decimal)entity.HINMEI_KINGAKU;
                    }
                    else
                    {
                        row["HINMEI_KINGAKU"] = 0;
                    }

                    // 品名税区分CD
                    if (false == entity.HINMEI_ZEI_KBN_CD.IsNull)
                    {
                        row["HINMEI_ZEI_KBN_CD"] = (Int16)entity.HINMEI_ZEI_KBN_CD;

                        // 品名税区分が登録されている場合は品名金額を表示する
                        row["SHOW_KINGAKU"] = row["HINMEI_KINGAKU"];
                    }
                    else
                    {
                        // 品名税区分が登録されていない場合は金額を表示する
                        row["SHOW_KINGAKU"] = row["KINGAKU"];
                    }

                    // その他文字列
                    row["HINMEI_CD"] = entity.HINMEI_CD;
                    row["HINMEI_NAME"] = entity.HINMEI_NAME;

                    // 出荷行Noに紐付く出荷明細より出荷品名を取得する
                    var shukkaDetail = this.getShukkaDetail(entity.ROW_NO.Value);
                    row["SHUKKA_HINMEI"] = "";
                    if (shukkaDetail != null)
                    {
                        row["SHUKKA_HINMEI"] = shukkaDetail.HINMEI_NAME;
                    }

                    // 行追加
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        /// <summary>
        /// ファンクションキー状態変更
        /// </summary>
        /// <remarks>明細の行数によって状態を切り替える</remarks>
        private void setFunctionKeyStatus()
        {
            if (this.form.returnDto.kenshuDetailList.Count > 0)
            {
                // 検収明細が存在する場合はコピー機能無効
                this.parentForm.bt_func8.Enabled = false;
            }
            else
            {
                // 検収明細が存在しない場合はコピー機能有効
                this.parentForm.bt_func8.Enabled = true;
            }

            if (this.form.KENSHU_ICHIRAN.Rows.Count > 0)
            {
                // 明細行が存在する場合は削除挿入機能有効
                this.parentForm.bt_func10.Enabled = true;
                this.parentForm.bt_func11.Enabled = true;
            }
            else
            {
                // 明細行が存在しない場合は挿入機能無効
                this.parentForm.bt_func10.Enabled = false;
                this.parentForm.bt_func11.Enabled = false;
            }
        }

        /// <summary>
        /// 合計セット
        /// </summary>
        /// <remarks>明細もしくは出荷情報より各合計値を算出しセットを行う</remarks>
        private void setTotal()
        {
            // 初期値セット
            decimal shukkaNetTotal = 0;
            decimal kenshuNetTotal = 0;
            decimal sabun = 0;
            decimal shukkaKingakuTotal = 0;
            decimal kenshuKingakuTotal = 0;
            decimal sagaku = 0;
            this.uriKingakuTotal = 0;
            this.hUriKingakuTotal = 0;
            this.shiKingakuTotal = 0;
            this.hShiKingakuTotal = 0;

            if (this.form.returnDto.shukkaEntryEntity != null)
            {
                // 出荷時正味セット
                if (false == this.form.returnDto.shukkaEntryEntity.NET_TOTAL.IsNull)
                {
                    shukkaNetTotal = decimal.Parse(this.form.returnDto.shukkaEntryEntity.NET_TOTAL.ToString());
                }

                // 出荷金額合計のセット
                if (false == this.form.returnDto.shukkaEntryEntity.URIAGE_AMOUNT_TOTAL.IsNull)
                {
                    // 売上金額合計をセット
                    shukkaKingakuTotal = (decimal)this.form.returnDto.shukkaEntryEntity.URIAGE_AMOUNT_TOTAL;

                    if (false == this.form.returnDto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL.IsNull)
                    {
                        // 売上金額合計+支払金額合計をセット
                        shukkaKingakuTotal += (decimal)this.form.returnDto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL;
                    }
                }
                else
                {
                    if (false == this.form.returnDto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL.IsNull)
                    {
                        // 支払金額合計をセット
                        shukkaKingakuTotal = (decimal)this.form.returnDto.shukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL;
                    }
                }
            }

            if (this.form.KENSHU_ICHIRAN.RowCount > 0)
            {
                // 明細行が存在する場合は明細から算出する
                // ※存在しない場合は０
                var table = (DataTable)this.form.KENSHU_ICHIRAN.DataSource;
                foreach (DataRow row in table.Rows)
                {
                    // 検収正味合計を積算
                    if (false == string.IsNullOrEmpty(row["KENSHU_NET"].ToString()))
                    {
                        kenshuNetTotal += (decimal)row["KENSHU_NET"];
                    }

                    // 伝票区分CDを取得
                    var strDenpyouKbn = row["DENPYOU_KBN_CD"].ToString();
                    Int16 denpyouKbn = 0;
                    if (false == string.IsNullOrEmpty(strDenpyouKbn))
                    {
                        denpyouKbn = Int16.Parse(strDenpyouKbn);
                    }

                    // 各金額合計値を算出
                    if (false == string.IsNullOrEmpty(row["SHOW_KINGAKU"].ToString()))
                    {
                        if (false == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                        {
                            // 品名税区分CDが登録されている場合
                            if (denpyouKbn != 0)
                            {
                                if (denpyouKbn == CommonConst.DENPYOU_KBN_SHIHARAI)
                                {
                                    // 伝票区分が『支払』の場合は品名支払として積算
                                    this.hShiKingakuTotal += (decimal)row["HINMEI_KINGAKU"];
                                }
                                else
                                {
                                    // 伝票区分が『売上』の場合は品名売上として積算
                                    this.hUriKingakuTotal += (decimal)row["HINMEI_KINGAKU"];
                                }
                            }
                            else
                            {
                                // 伝票区分が存在しない場合は品名売上として積算
                                this.hUriKingakuTotal += (decimal)row["HINMEI_KINGAKU"];
                            }
                        }
                        else
                        {
                            // 品名税区分CDが登録されていない場合
                            if (denpyouKbn != 0)
                            {
                                if (denpyouKbn == CommonConst.DENPYOU_KBN_SHIHARAI)
                                {
                                    // 伝票区分が『支払』の場合は支払として積算
                                    this.shiKingakuTotal += (decimal)row["KINGAKU"];
                                }
                                else
                                {
                                    // 伝票区分が『売上』の場合は売上として積算
                                    this.uriKingakuTotal += (decimal)row["KINGAKU"];
                                }
                            }
                            else
                            {
                                // 伝票区分が存在しない場合は売上として積算
                                this.uriKingakuTotal += (decimal)row["KINGAKU"];
                            }
                        }

                        // 検収金額合計を算出
                        if (denpyouKbn == CommonConst.DENPYOU_KBN_SHIHARAI)
                        {
                            // 伝票区分が『支払』の場合は合計金額から減算
                            kenshuKingakuTotal -= (decimal)row["SHOW_KINGAKU"];
                        }
                        else
                        {
                            // 伝票区分が『売上』の場合は合計金額に積算
                            kenshuKingakuTotal += (decimal)row["SHOW_KINGAKU"];
                        }
                    }
                }
            }

            // 差分(検収時正味合計から出荷時正味合計をひく)
            sabun = kenshuNetTotal - shukkaNetTotal;

            // 差額(検収金額合計から出荷金額合計をひく)
            sagaku = kenshuKingakuTotal - shukkaKingakuTotal;

            // 表示フォーマット設定を行った状態で各項目にセット
            this.form.SHUKKA_NET_TOTAL.Text = CommonCalc.DecimalFormat(shukkaNetTotal);
            this.form.KENSHU_NET_TOTAL.Text = CommonCalc.DecimalFormat(kenshuNetTotal);
            this.form.NET_SABUN.Text = CommonCalc.DecimalFormat(sabun);
            this.form.KENSHU_KINGAKU_TOTAL.Text = CommonCalc.DecimalFormat(kenshuKingakuTotal);
        }

        /// <summary>
        /// Form情報よりEntity生成
        /// </summary>
        private void createEntityFromForm()
        {
            // 検収伝票日付
            this.form.returnDto.shukkaEntryEntity.KENSHU_DATE = DateTime.Parse(this.headerForm.KENSHU_DENPYOU_DATE.Value.ToString());

            // 検収売上日付
            var str = this.form.KENSHU_URIAGE_DATE.Value.ToString();
            if (false == string.IsNullOrEmpty(str))
            {
                this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_DATE = DateTime.Parse(str);
            }

            // 検収支払日付
            str = this.form.KENSHU_SHIHARAI_DATE.Value.ToString();
            if (false == string.IsNullOrEmpty(str))
            {
                this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_DATE = DateTime.Parse(str);
            }

            // 検収正味合計
            this.form.returnDto.shukkaEntryEntity.KENSHU_NET_TOTAL = decimal.Parse(this.form.KENSHU_NET_TOTAL.Text);

            // 検収売上消費税率
            this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = this.valueForShouhizeiRate(this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text);

            // 検収売上金額合計
            this.form.returnDto.shukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL = this.uriKingakuTotal;

            // 検収品名売上金額合計
            this.form.returnDto.shukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL = this.hUriKingakuTotal;

            // 検収支払消費税率
            this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = this.valueForShouhizeiRate(this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text);

            // 検収支払金額合計
            this.form.returnDto.shukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = this.shiKingakuTotal;

            // 検収品名支払金額合計
            this.form.returnDto.shukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL = this.hShiKingakuTotal;

        }

        /// <summary>
        /// 明細情報よりEntity生成
        /// </summary>
        private void createEntityFromDetail()
        {
            Int16 kenshuRowNo = 1;
            T_KENSHU_DETAIL entity;

            // 初期化
            this.form.returnDto.kenshuDetailList.Clear();

            // 明細の内容をEntityに格納
            var table = (DataTable)this.form.KENSHU_ICHIRAN.DataSource;
            foreach (DataRow row in table.Rows)
            {
                // 初期化
                entity = new T_KENSHU_DETAIL();

                // 出荷行番号
                entity.ROW_NO = (Int16)row["ROW_NO"];

                // 検収行番号
                entity.KENSHU_ROW_NO = kenshuRowNo;

                // 伝票区分CD
                if (false == string.IsNullOrEmpty(row["DENPYOU_KBN_CD"].ToString()))
                {
                    entity.DENPYOU_KBN_CD = (Int16)row["DENPYOU_KBN_CD"];
                }

                // 検収品名CD
                entity.HINMEI_CD = (string)row["HINMEI_CD"];

                // 検収品名
                entity.HINMEI_NAME = (string)row["HINMEI_NAME"];

                // 出荷時正味
                if (false == string.IsNullOrEmpty(row["SHUKKA_NET"].ToString()))
                {
                    entity.SHUKKA_NET = decimal.Parse(row["SHUKKA_NET"].ToString());
                }

                // 歩引き
                if (false == string.IsNullOrEmpty(row["BUBIKI"].ToString()))
                {
                    entity.BUBIKI = (decimal)row["BUBIKI"];
                }

                // 検収正味
                if (false == string.IsNullOrEmpty(row["KENSHU_NET"].ToString()))
                {
                    entity.KENSHU_NET = decimal.Parse(row["KENSHU_NET"].ToString());
                }

                // 数量
                entity.SUURYOU = decimal.Parse(row["SUURYOU"].ToString());

                // 単位CD
                entity.UNIT_CD = (Int16)row["UNIT_CD"];

                // 単価
                if (false == string.IsNullOrEmpty(row["TANKA"].ToString()))
                {
                    entity.TANKA = (decimal)row["TANKA"];
                }

                // 金額
                if (false == string.IsNullOrEmpty(row["KINGAKU"].ToString()))
                {
                    entity.KINGAKU = (decimal)row["KINGAKU"];
                }

                // 品名税区分CD
                if (false == string.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                {
                    entity.HINMEI_ZEI_KBN_CD = (Int16)row["HINMEI_ZEI_KBN_CD"];
                }

                // 品名金額
                if (false == string.IsNullOrEmpty(row["HINMEI_KINGAKU"].ToString()))
                {
                    entity.HINMEI_KINGAKU = (decimal)row["HINMEI_KINGAKU"];
                }

                // 検収明細追加
                this.form.returnDto.kenshuDetailList.Add(entity);

                //検収行番号インクリメント
                kenshuRowNo++;
            }
        }

        /// <summary>
        /// 出荷行Noに紐付く出荷明細を返却する
        /// </summary>
        /// <param name="shukkaRowNo">出荷行No</param>
        /// <returns name="T_SHUKKA_DETAIL">渡された情報に紐付く出荷明細</returns>
        private T_SHUKKA_DETAIL getShukkaDetail(Int16 shukkaRowNo)
        {
            // 検収明細に紐付く出荷明細を抽出
            var shukkaDetail = this.form.returnDto.shukkaDetailList.Where(s => (s.ROW_NO.Value == shukkaRowNo)).FirstOrDefault();
            return shukkaDetail;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="row">単価セットを行うDataGridViewRow</param>
        /// <returns name="decimal">単価</returns>
        /// <remarks>
        /// 対象項目を直接指定し単価を取得する。個別品名単価⇒基本品名単価の順で取得する
        /// ※尚、出荷伝票には荷卸業者現場は存在しないため空で検索を行う
        /// </remarks>
        private decimal getTanka(DataGridViewRow row, out bool isNotTanka)
        {
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            decimal tanka = 0;
            isNotTanka = true;

            if ((false == string.IsNullOrEmpty(row.Cells["DENPYOU_KBN_CD"].Value.ToString())) &&
                (false == string.IsNullOrEmpty(row.Cells["HINMEI_CD"].Value.ToString())) &&
                (false == string.IsNullOrEmpty(row.Cells["UNIT_CD"].Value.ToString())))
            {
                // 個別品名単価から取得
                var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka(this.denshuSyukka,
                                                                   (Int16)row.Cells["DENPYOU_KBN_CD"].Value,
                                                                    this.form.returnDto.shukkaEntryEntity.TORIHIKISAKI_CD,
                                                                    this.form.returnDto.shukkaEntryEntity.GYOUSHA_CD,
                                                                    this.form.returnDto.shukkaEntryEntity.GENBA_CD,
                                                                    this.form.returnDto.shukkaEntryEntity.UNPAN_GYOUSHA_CD,
                                                                    string.Empty,
                                                                    string.Empty,
                                                                    (string)row.Cells["HINMEI_CD"].Value,
                                                                    (Int16)row.Cells["UNIT_CD"].Value,
                                                                    this.headerForm.KENSHU_DENPYOU_DATE.Text);
                if (kobetsuEntity != null)
                {
                    // 単価をセット
                    if (false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
                    {
                        tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
                        isNotTanka = false;
                    }
                }
                else
                {
                    // 基本品名単価から取得
                    var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka(this.denshuSyukka,
                                                                    (Int16)row.Cells["DENPYOU_KBN_CD"].Value,
                                                                    this.form.returnDto.shukkaEntryEntity.UNPAN_GYOUSHA_CD,
                                                                    string.Empty,
                                                                    string.Empty,
                                                                    (string)row.Cells["HINMEI_CD"].Value,
                                                                    (Int16)row.Cells["UNIT_CD"].Value,
                                                                    this.headerForm.KENSHU_DENPYOU_DATE.Text);
                    if (kihonEntity != null)
                    {
                        // 単価をセット
                        if (false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
                        {
                            tanka = decimal.Parse(kihonEntity.TANKA.ToString());
                            isNotTanka = false;
                        }
                    }
                    else
                    {
                        // 登録情報がない場合は0
                        tanka = 0;
                    }
                }
            }

            return tanka;
        }

        /// <summary>
        /// 金額計算
        /// </summary>
        /// <param name="row">金額セットを行うDataGridViewRow</param>
        /// <returns name="decimal">金額</returns>
        /// <remarks>単価x数量の計算により、金額を算出する</remarks>
        private decimal calcKingaku(DataGridViewRow row)
        {
            decimal kingaku = 0;

            if ((false == string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString())) &&
                (false == string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString())))
            {
                short kingakuHasuuCd = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["DENPYOU_KBN_CD"].Value)))
                {
                    var denpyouKbnCd = row.Cells["DENPYOU_KBN_CD"].Value.ToString();
                    switch(denpyouKbnCd)
                    {
                        case "1":
                            // 売上
                            short.TryParse(Convert.ToString(this.torihikisakiSeikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;

                        case "2":
                            // 支払
                            short.TryParse(Convert.ToString(this.torihikisakiShiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                    }
                }


                // 単価x数量
                kingaku = CommonCalc.FractionCalc((decimal)row.Cells["TANKA"].Value * (decimal)row.Cells["SUURYOU"].Value, kingakuHasuuCd);

                /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                if (kingaku.ToString().Length > 9)
                {
                    kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                }
            }

            return kingaku;
        }

        /// <summary>
        /// 金額計算(登録時金額チェック用)
        /// </summary>
        /// <param name="row">金額セットを行うDataGridViewRow</param>
        /// <returns name="decimal">金額</returns>
        /// <remarks>単価x数量の計算により、金額を算出する</remarks>
        private decimal calcKingakuForRegistCheck(DataGridViewRow row)
        {
            decimal kingaku = 0;

            if ((false == string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString())) &&
                (false == string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString())))
            {
                short kingakuHasuuCd = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["DENPYOU_KBN_CD"].Value)))
                {
                    var denpyouKbnCd = row.Cells["DENPYOU_KBN_CD"].Value.ToString();
                    switch (denpyouKbnCd)
                    {
                        case "1":
                            // 売上
                            short.TryParse(Convert.ToString(this.torihikisakiSeikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;

                        case "2":
                            // 支払
                            short.TryParse(Convert.ToString(this.torihikisakiShiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            break;
                    }
                }


                // 単価x数量
                kingaku = CommonCalc.FractionCalc((decimal)row.Cells["TANKA"].Value * (decimal)row.Cells["SUURYOU"].Value, kingakuHasuuCd);
            }

            return kingaku;
        }

        /// <summary>
        /// 差分チェック
        /// </summary>
        /// <param name="oldRow">編集前のカレント行</param>
        /// <param name="newRow">編集後のカレント行</param>
        /// <returns name="bool">TRUE:差分あり</returns>
        /// <remarks>編集前と編集後を比較し差分があったかのチェックを行う</remarks>
        private bool rowDiffCheck(DataGridViewRow oldRow, DataGridViewRow newRow)
        {
            bool ret = false;

            for (int i = 0; i < oldRow.Cells.Count; i++)
            {
                // 差分チェック
                if (false == oldRow.Cells[i].Value.Equals(newRow.Cells[i].Value))
                {
                    // 差分ありとして返却
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// デフォルト行チェック
        /// </summary>
        /// <param name="curIndex">現在選択行のインデックス</param>
        /// <returns name="bool">TRUE:デフォルト行</returns>
        /// <remarks>
        /// 選択されている行がデフォルト行かどうかのチェックを行う
        /// ※デフォルト行は削除禁止、出荷複写可能等の制限がある
        /// </remarks>
        private bool defaultRowCheck(int curIndex)
        {
            bool ret = false;

            if (curIndex == 0)
            {
                // 選択行が挿入による追加行ではない(＝同出荷行Noの一番上)場合、デフォルト行
                ret = true;
            }
            else
            {
                if (false == this.form.KENSHU_ICHIRAN.Rows[curIndex].Cells["ROW_NO"].Value.Equals(this.form.KENSHU_ICHIRAN.Rows[curIndex - 1].Cells["ROW_NO"].Value))
                {
                    // 選択行が挿入による追加行ではない(＝同出荷行Noの一番上)場合、デフォルト行
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// 数量セット
        /// </summary>
        /// <param name="row">数量セットを行うDataGridViewRow</param>
        /// <remarks>
        /// 数量がセットされていない場合、かつ
        /// 「kg(CD:3)」「t(CD:1)」選択時は数量に正味を格納する
        /// </remarks>
        private bool setSuuryou(DataGridViewRow row)
        {
            var ret = false;
            //if (true == string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()))
            //{
                // 数量項目が未セットの場合
                if (false == string.IsNullOrEmpty(row.Cells["KENSHU_NET"].Value.ToString()))
                {
                    // 正味項目が存在する場合
                    if (false == string.IsNullOrEmpty(row.Cells["UNIT_CD"].Value.ToString()))
                    {
                        // 単位CD項目が存在する場合
                        if (3 == (Int16)row.Cells["UNIT_CD"].Value)
                        {
                            // 「kg」選択時は数量に正味を格納
                            row.Cells["SUURYOU"].Value = row.Cells["KENSHU_NET"].Value;
                        }
                        else if (1 == (Int16)row.Cells["UNIT_CD"].Value)
                        {
                            // 「t」選択時は正味を基に算出し数量に格納
                            var suuryou = (decimal)row.Cells["KENSHU_NET"].Value;
                            row.Cells["SUURYOU"].Value = (suuryou / 1000);
                        }
                        ret = true;
                    }
                }
            //}

            // 数量の活性/非活性制御
            this.SetDetailSuuryouReadonly(row.Index);

            return ret;
        }

        /// <summary>
        /// リボンメニュー非表示
        /// </summary>
        private void ribbonHide()
        {
            // リボン非表示
            this.ribbon.Visible = false;

            // 非表示にした分、各Formを調整
            this.headerForm.Location = new Point(this.headerForm.Location.X, this.headerForm.Location.Y - this.ribbon.Height);
            this.form.Location = new Point(this.form.Location.X, this.form.Location.Y - this.ribbon.Height);
            this.parentForm.Size = new Size(this.parentForm.Size.Width, this.parentForm.Size.Height - this.ribbon.Height);

            this.parentForm.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// 編集モードセット
        /// </summary>
        /// <param name="type">編集モード</param>
        private void setEditMode(WINDOW_TYPE type)
        {
            // 編集モードセット
            this.headerForm.windowTypeLabel.WindowType = type;
            this.headerForm.windowTypeLabel.Text = type.ToTypeString();
            this.headerForm.windowTypeLabel.BackColor = type.ToLabelColor();
            this.headerForm.windowTypeLabel.ForeColor = type.ToLabelForeColor();
        }

        /// <summary>
        /// DataGridView前回値更新
        /// </summary>
        /// <param name="index">RowIndex</param>
        private void oldDgvRowUpdate(int index)
        {
            // 編集予定行のセット
            this.oldDgvRow = (DataGridViewRow)this.form.KENSHU_ICHIRAN.Rows[index].Clone();
            for (int i = 0; i < this.form.KENSHU_ICHIRAN.Rows[index].Cells.Count; i++)
            {
                this.oldDgvRow.Cells[i].Value = this.form.KENSHU_ICHIRAN.Rows[index].Cells[i].Value;
            }
        }

        /// <summary>
        /// 検収伝票日付初期セット
        /// </summary>
        internal void DenpyouDateSet()
        {
            tmpDenpyouDate = this.headerForm.KENSHU_DENPYOU_DATE.Text;
        }

        /// <summary>
        /// 明細全行で項目のReadOnly設定を行います
        /// 単価と金額の設定のみ
        /// </summary>
        internal void SetDetailReadOnly()
        {            
            foreach (DataGridViewRow row in this.form.KENSHU_ICHIRAN.Rows)
            {
                this.SetDetailReadOnly(row.Index);
            }
        }

        /// <summary>
        /// 指定された明細行で項目のReadOnly設定を行います
        /// 単価と金額の設定のみ
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        internal void SetDetailReadOnly(int rowIndex)
        {
            string tankaCellName = "TANKA";
            string kingakuCellName = "SHOW_KINGAKU";
            var row = this.form.KENSHU_ICHIRAN.Rows[rowIndex];

            if ((row.Cells[tankaCellName].FormattedValue == null || string.IsNullOrEmpty(row.Cells[tankaCellName].FormattedValue.ToString())) &&
                (row.Cells[kingakuCellName].FormattedValue == null || string.IsNullOrEmpty(row.Cells[kingakuCellName].FormattedValue.ToString())))
            {
                // 「単価」、「金額」どちらも空の場合、両方操作可
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].ReadOnly = false;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].UpdateBackColor(false);
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = false;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].UpdateBackColor(false);
            }
            else if (row.Cells[tankaCellName].Value != null && !string.IsNullOrEmpty(row.Cells[tankaCellName].Value.ToString()))
            {
                // 「単価」のみ入力済みの場合、「金額」操作不可
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].ReadOnly = false;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].UpdateBackColor(false);
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = true;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].UpdateBackColor(false);
            }
            else if (row.Cells[kingakuCellName].Value != null && !string.IsNullOrEmpty(row.Cells[kingakuCellName].Value.ToString()))
            {
                // 「金額」のみ入力済みの場合、「単価」操作不可
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].ReadOnly = true;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[tankaCellName].UpdateBackColor(false);
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].ReadOnly = false;
                this.form.KENSHU_ICHIRAN.Rows[rowIndex].Cells[kingakuCellName].UpdateBackColor(false);
            }
        }

        /// <summary>
        /// 明細全行で項目のReadOnly設定を行います
        /// 数量設定のみ
        /// </summary>
        internal void SetDetailSuuryouReadonly()
        {
            foreach (DataGridViewRow row in this.form.KENSHU_ICHIRAN.Rows)
            {
                this.SetDetailSuuryouReadonly(row.Index);
            }
        }

        /// <summary>
        /// 指定された明細行で項目のReadOnly設定を行います
        /// 単価と金額の設定のみ
        /// </summary>
        /// <param name="rowindex"></param>
        internal void SetDetailSuuryouReadonly(int rowindex)
        {
            DataGridViewRow row = this.form.KENSHU_ICHIRAN.Rows[rowindex];
            row.Cells["SUURYOU"].ReadOnly = false;
            if (row.Cells["KENSHU_NET"].Value != null && !string.IsNullOrEmpty(row.Cells["KENSHU_NET"].Value.ToString()) &&
               row.Cells["UNIT_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["UNIT_CD"].Value.ToString()))
            {
                if ((Int16)row.Cells["UNIT_CD"].Value == 1 || (Int16)row.Cells["UNIT_CD"].Value == 3)
                {
                    row.Cells["SUURYOU"].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 検収伝票日付チェック
        /// </summary>
        internal void CheckDenpyouDate()
        {
            var inputDenpyouDate = this.headerForm.KENSHU_DENPYOU_DATE.Text;

            // 伝票日付が空じゃないかつ変更があった場合
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                // 明細行すべての単価を再設定
                this.form.KENSHU_ICHIRAN.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToList().ForEach(r =>
                    {
                        // 単価前回値取得
                        var oldTanka = r.Cells["TANKA"].Value == null ? string.Empty : r.Cells["TANKA"].Value.ToString();

                        bool isNotTanka = false;
                        var tanka = this.getTanka(r, out isNotTanka);
                        if (isNotTanka)
                        {
                            r.Cells["TANKA"].Value = DBNull.Value;
                        }
                        else
                        {
                            r.Cells["TANKA"].Value = tanka;
                        }

                        if (!string.IsNullOrEmpty(r.Cells["SUURYOU"].Value.ToString()) &&
                            !string.IsNullOrEmpty(r.Cells["TANKA"].Value.ToString()))
                        {
                            r.Cells["SHOW_KINGAKU"].Value = this.calcKingaku(r);
                        }
                        else
                        {
                            // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                            r.Cells["SHOW_KINGAKU"].Value = DBNull.Value;
                        }

                        // 一旦クリア
                        r.Cells["HINMEI_KINGAKU"].Value = 0;
                        r.Cells["KINGAKU"].Value = 0;

                        if (false == string.IsNullOrEmpty(r.Cells["HINMEI_ZEI_KBN_CD"].Value.ToString()))
                        {
                            // 品名税区分CDが存在する場合は表示用金額項目を「品名金額」へ
                            r.Cells["HINMEI_KINGAKU"].Value = r.Cells["SHOW_KINGAKU"].Value;
                        }
                        else
                        {
                            // 品名税区分CDが存在しない場合は表示用金額項目を「金額」へ
                            r.Cells["KINGAKU"].Value = r.Cells["SHOW_KINGAKU"].Value;
                        }

                        var newTanka = r.Cells["TANKA"].Value == null ? string.Empty : r.Cells["TANKA"].Value.ToString();

                        // 単価に変更があった場合のみ再計算
                        if (!oldTanka.Equals(newTanka))
                        {
                            // 合計値更新
                            this.setTotal();
                        }

                        // 単価と金額の活性/非活性制御
                        this.SetDetailReadOnly(r.Index);
                    });
            }
        }

        /// <summary>
        /// 売上日付Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KENSHU_URIAGE_DATE_Enter(object sender, EventArgs e)
        {
            this.beforeUrageDate = this.form.KENSHU_URIAGE_DATE.Text;
        }

        /// <summary>
        /// 支払日付Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KENSHU_SHIHARAI_DATE_Enter(object sender, EventArgs e)
        {
            this.beforeShiharaiDate = this.form.KENSHU_SHIHARAI_DATE.Text;

        }

        /// <summary>
        /// 売上日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KENSHU_URIAGE_DATE_Validated(object sender, EventArgs e)
        {
            this.form.ActiveControl = this.form.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeUrageDate.Equals(this.form.KENSHU_URIAGE_DATE.Text))
            {
                this.SetUriageShouhizeiRate();        // 売上消費税率設定
            }
        }

        /// <summary>
        /// 支払日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KENSHU_SHIHARAI_DATE_Validated(object sender, EventArgs e)
        {
            this.form.ActiveControl = this.form.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeShiharaiDate.Equals(this.form.KENSHU_SHIHARAI_DATE.Text))
            {
                this.SetShiharaiShouhizeiRate();
            }
        }

        /// <summary>
        /// 売上日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetUriageShouhizeiRate()
        {
            try
            {
                DateTime uriageDate;
                if (DateTime.TryParse(this.form.KENSHU_URIAGE_DATE.Text, out uriageDate))
                {
                    var shouhizeiRate = this.GetShouhizeiRate(uriageDate);
                    if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.KENSHU_URIAGE_SHOUHIZEI_RATE.Text = string.Empty;
                }

                return true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex1);
                msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUriageShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 支払日付を基に売上消費税率を設定
        /// </summary>
        internal bool SetShiharaiShouhizeiRate()
        {
            try
            {
                DateTime shiharaiDate;
                if (DateTime.TryParse(this.form.KENSHU_SHIHARAI_DATE.Text, out shiharaiDate))
                {
                    var shouhizeiRate = this.GetShouhizeiRate(shiharaiDate);
                    if (!shouhizeiRate.SHOUHIZEI_RATE.IsNull)
                    {
                        this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = shouhizeiRate.SHOUHIZEI_RATE.ToString();
                    }
                }
                else
                {
                    this.form.KENSHU_SHIHARAI_SHOUHIZEI_RATE.Text = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiShouhizeiRate", ex);
                msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="denpyouHiduke">適用期間条件</param>
        /// <returns></returns>
        public M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        {
            M_SHOUHIZEI returnEntity = null;

            if (denpyouHiduke == null)
            {
                return returnEntity;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            DataTable dt = new DataTable();
            string selectStr = "SELECT * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";

            StringBuilder sb = new StringBuilder();
            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= ISNULL(TEKIYOU_END,'9999/12/31')");
            sb.Append("  )");
            sb.Append(" )");

            whereStr = whereStr + sb.ToString();

            dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                return returnEntity;
            }

            var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

            var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
            returnEntity = shouhizeis[0];

            return returnEntity;
        }

        //20210826 Thanh 154360 s
        /// <summary>
        /// CopyRowShukkaToKenshu
        /// </summary>
        /// <param name="curRow"></param>
        private void CopyRowShukkaToKenshu(DataGridViewRow curRow)
        {
            LogUtility.DebugMethodStart(curRow);
            foreach (var rowShukka in this.form.returnDto.shukkaDetailList)
            {
                int RowInsert = 0;
                for (int i = 0; i <= this.form.KENSHU_ICHIRAN.Rows.Count - 1; i++)
                {
                    var rowKenshu = this.form.KENSHU_ICHIRAN.Rows[i];
                    if (rowKenshu.IsNewRow)
                    {
                        continue;
                    }
                    int RowNoKenshu = (Int16)rowKenshu.Cells["ROW_NO"].Value;
                    if (RowNoKenshu == rowShukka.ROW_NO.Value)
                    {
                        RowInsert = -1;
                        break;
                    }
                    if (rowShukka.ROW_NO.Value >= RowNoKenshu)
                    {
                        RowInsert = i;
                    }
                    else
                    {
                        RowInsert = i - 1;
                        break;
                    }
                }
                if (RowInsert < 0)
                {
                    continue;
                }
                this.AddRowAddData(RowInsert, rowShukka);
                for (int i = 0; i <= this.form.KENSHU_ICHIRAN.Rows.Count - 1; i++)
                {
                    this.SetDetailSuuryouReadonly(i);
                    this.SetDetailReadOnly(i);
                }
                // 合計値更新
                this.setTotal();
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// AddRowAddData
        /// </summary>
        /// <param name="RowInsert"></param>
        /// <param name="detail"></param>
        private void AddRowAddData(int RowInsert, T_SHUKKA_DETAIL shukkaDetail)
        {
            var curRow = this.form.KENSHU_ICHIRAN.Rows[RowInsert];

            if (curRow.Cells["ROW_NO"].Value != null)
            {
                // 選択行を挿入した状態のDataTableを再構築
                var oldTable = (DataTable)this.form.KENSHU_ICHIRAN.DataSource;
                var newTable = oldTable.Clone();
                for (int i = 0; i < oldTable.Rows.Count; i++)
                {
                    // 順次コピー
                    var oldRow = newTable.NewRow();
                    for (int j = 0; j < oldTable.Columns.Count; j++)
                    {
                        oldRow[j] = oldTable.Rows[i][j];
                    }
                    newTable.Rows.Add(oldRow);
                    if (i == curRow.Index)
                    {
                        // コピー行が選択行だった場合、コピー行の次に行を挿入する
                        // 挿入行のデータは選択行の出荷系データを引き継ぐ
                        // ※出荷時正味は0とする
                        var row = newTable.NewRow();
                        // 出荷行No
                        if (false == shukkaDetail.ROW_NO.IsNull)
                        {
                            row["ROW_NO"] = (Int16)shukkaDetail.ROW_NO;
                        }
                        else
                        {
                            row["ROW_NO"] = 0;
                        }
                        // 出荷時正味
                        if (false == shukkaDetail.NET_JYUURYOU.IsNull)
                        {
                            row["SHUKKA_NET"] = decimal.Parse(shukkaDetail.NET_JYUURYOU.ToString());
                        }
                        // その他文字列
                        row["SHUKKA_HINMEI"] = shukkaDetail.HINMEI_NAME;
                        row["HINMEI_CD"] = string.Empty;
                        row["HINMEI_NAME"] = string.Empty;
                        row["DENPYOU_KBN_NAME"] = string.Empty;
                        row["BUBIKI"] = DBNull.Value;
                        row["KENSHU_NET"] = DBNull.Value;
                        row["SUURYOU"] = DBNull.Value;
                        row["UNIT_NAME"] = string.Empty;
                        row["UNIT_CD"] = DBNull.Value;
                        row["TANKA"] = DBNull.Value;
                        row["KINGAKU"] = DBNull.Value;
                        row["DENPYOU_KBN_CD"] = DBNull.Value;
                        row["HINMEI_ZEI_KBN_CD"] = DBNull.Value;
                        row["HINMEI_KINGAKU"] = DBNull.Value;
                        row["SHOW_KINGAKU"] = DBNull.Value;
                        // 出荷明細より情報をコピー
                        // 品名CD
                        row["HINMEI_CD"] = shukkaDetail.HINMEI_CD;
                        // 品名
                        row["HINMEI_NAME"] = shukkaDetail.HINMEI_NAME;
                        // 正味
                        decimal decInit = 0;
                        if (decimal.TryParse(shukkaDetail.NET_JYUURYOU.ToString(), out decInit))
                        {
                            row["KENSHU_NET"] = decInit;
                        }
                        // 数量
                        decimal.TryParse(shukkaDetail.SUURYOU.ToString(), out decInit);
                        row["SUURYOU"] = decInit;
                        // 単位CD、単位名
                        if (false == shukkaDetail.UNIT_CD.IsNull)
                        {
                            row["UNIT_CD"] = shukkaDetail.UNIT_CD.Value;
                            var unitEntity = this.unitDao.GetDataByCd(shukkaDetail.UNIT_CD.Value);
                            if (unitEntity != null)
                            {
                                if (false == string.IsNullOrEmpty(unitEntity.UNIT_NAME_RYAKU))
                                {
                                    // 単位名のセット
                                    row["UNIT_NAME"] = unitEntity.UNIT_NAME_RYAKU;
                                }
                            }
                        }
                        // 伝票区分CD、伝票区分名
                        if (false == shukkaDetail.DENPYOU_KBN_CD.IsNull)
                        {
                            row["DENPYOU_KBN_CD"] = shukkaDetail.DENPYOU_KBN_CD.Value;
                            var denKbnEntity = this.denpyouKbnDao.GetDataByCd(shukkaDetail.DENPYOU_KBN_CD.ToString());
                            if (denKbnEntity != null)
                            {
                                if (false == string.IsNullOrEmpty(denKbnEntity.DENPYOU_KBN_NAME_RYAKU))
                                {
                                    // 伝票区分名のセット
                                    row["DENPYOU_KBN_NAME"] = denKbnEntity.DENPYOU_KBN_NAME_RYAKU;
                                }
                            }
                        }
                        // 品名税区分CD
                        if (false == shukkaDetail.HINMEI_ZEI_KBN_CD.IsNull)
                        {
                            row["HINMEI_ZEI_KBN_CD"] = shukkaDetail.HINMEI_ZEI_KBN_CD.Value;
                        }
                        // 単価
                        if (!shukkaDetail.TANKA.IsNull)
                        {
                            row["TANKA"] = shukkaDetail.TANKA.Value;
                        }
                        // 金額
                        decimal kingaku = 0;
                        if (!shukkaDetail.KINGAKU.IsNull)
                        {
                            row["KINGAKU"] = shukkaDetail.KINGAKU.Value;
                            kingaku += shukkaDetail.KINGAKU.Value;
                        }
                        // 品名金額
                        if (!shukkaDetail.HINMEI_KINGAKU.IsNull)
                        {
                            row["HINMEI_KINGAKU"] = shukkaDetail.HINMEI_KINGAKU.Value;
                            kingaku += shukkaDetail.HINMEI_KINGAKU.Value;
                        }
                        // 表示金額
                        if (!string.IsNullOrEmpty(Convert.ToString(row["SUURYOU"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(row["TANKA"])))
                        {
                            row["SHOW_KINGAKU"] = kingaku;
                        }
                        else
                        {
                            // 金額 or 単価がブランクの場合は金額に「0」を設定しない
                            row["SHOW_KINGAKU"] = DBNull.Value;
                        }
                        newTable.Rows.Add(row);
                    }
                }
                // DataSourceセット
                // ※DataSourceのセットを行うとCurrentIndexが初期化されてしまうため退避しておく
                var curIndex = curRow.Index;
                this.form.KENSHU_ICHIRAN.DataSource = newTable;
                // 初期フォーカスセット
                this.form.KENSHU_ICHIRAN.CurrentCell = this.form.KENSHU_ICHIRAN.Rows[curIndex].Cells[0];
            }
        }

        /// <summary>
        /// GetSysInfoInit
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
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
    }
}
