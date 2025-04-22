using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Const;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Function.ShougunCSCommon.Utility;
using AuthManager = r_framework.Authority.Manager;
using DBAccessor = Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Accessor.DBAccessor;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region フィールド・プロパティ

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeaderForm headerForm;

        /// <summary> 入力パラメータ </summary>
        public NyuuryokuParamDto NyuuryokuParam { get; set; }

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        public DBAccessor accessor;

        public Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public SearchDto dto;

        private Color sharyouCdBackColor = Color.FromArgb(255, 235, 160);

        public List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>> tuResult = new List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>>();
        public List<ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>> tsResult = new List<ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>>();
        public List<ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>> tusResult = new List<ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>>();

        public List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>> tuRegist = new List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>>();
        public List<ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>> tsRegist = new List<ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>>();
        public List<ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>> tusRegist = new List<ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>>();

        internal Dictionary<string, S_NUMBER_DAY[]> dayDic;
        internal Dictionary<string, S_NUMBER_YEAR[]> yearDic;

        internal GcCustomMoveToNextContorol_Cust cust_Next;
        internal GcCustomMoveToPreviousContorol_Cust cust_Previous;

        public List<Dictionary<string, TabGoDto>> map = new List<Dictionary<string, TabGoDto>>();

        internal string csvPath = string.Empty;

        public string[] columnUkeire = {"CHECK",
                                        "mr_KYOTEN_CD",
                                        "mr_DENPYOU_DATE",
                                        "mr_URIAGE_DATE",
                                        "mr_SHIHARAI_DATE",
                                        "mr_TORIHIKISAKI_CD",
                                        "mr_GYOUSHA_CD",
                                        "mr_GENBA_CD",
                                        "mr_NIOROSHI_GYOUSHA_CD",
                                        "mr_NIOROSHI_GENBA_CD",
                                        "mr_EIGYOU_TANTOUSHA_CD",
                                        "mr_UNPAN_GYOUSHA_CD",
                                        "mr_SHASHU_CD",
                                        "mr_SHARYOU_CD",
                                        "mr_UNTENSHA_CD",
                                        "mr_KEITAI_KBN_CD",
                                        "mr_DAIKAN_KBN_CD",
                                        "mr_MANIFEST_SHURUI_CD",
                                        "mr_MANIFEST_TEHAI_CD",
                                        "mr_DENPYOU_BIKOU",
                                        "mr_TAIRYUU_BIKOU"};

        public string[] columnShukka = {"CHECK",
                                        "mr_KYOTEN_CD",
                                        "mr_DENPYOU_DATE",
                                        "mr_URIAGE_DATE",
                                        "mr_SHIHARAI_DATE",
                                        "mr_TORIHIKISAKI_CD",
                                        "mr_GYOUSHA_CD",
                                        "mr_GENBA_CD",
                                        "mr_NIZUMI_GYOUSHA_CD",
                                        "mr_NIZUMI_GENBA_CD",
                                        "mr_EIGYOU_TANTOUSHA_CD",
                                        "mr_UNPAN_GYOUSHA_CD",
                                        "mr_SHASHU_CD",
                                        "mr_SHARYOU_CD",
                                        "mr_UNTENSHA_CD",
                                        "mr_KEITAI_KBN_CD",
                                        "mr_DAIKAN_KBN_CD",
                                        "mr_MANIFEST_SHURUI_CD",
                                        "mr_MANIFEST_TEHAI_CD",
                                        "mr_DENPYOU_BIKOU",
                                        "mr_TAIRYUU_BIKOU"};

        public string[] columnUrsh = {"CHECK",
                                      "mr_KYOTEN_CD",
                                      "mr_DENPYOU_DATE",
                                      "mr_URIAGE_DATE",
                                      "mr_SHIHARAI_DATE",
                                      "mr_TORIHIKISAKI_CD",
                                      "mr_GYOUSHA_CD",
                                      "mr_GENBA_CD",
                                      "mr_NIZUMI_GYOUSHA_CD",
                                      "mr_NIZUMI_GENBA_CD",
                                      "mr_NIOROSHI_GYOUSHA_CD",
                                      "mr_NIOROSHI_GENBA_CD",
                                      "mr_EIGYOU_TANTOUSHA_CD",
                                      "mr_UNPAN_GYOUSHA_CD",
                                      "mr_SHASHU_CD",
                                      "mr_SHARYOU_CD",
                                      "mr_UNTENSHA_CD",
                                      "mr_KEITAI_KBN_CD",
                                      "mr_MANIFEST_SHURUI_CD",
                                      "mr_MANIFEST_TEHAI_CD",
                                      "mr_DENPYOU_BIKOU"};
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;
            this.accessor = new DBAccessor();
            this.commonAccesser = new Common.BusinessCommon.DBAccessor();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region メソッド

        #region 画面の初期化

        /// <summary>
        /// 画面の初期化
        /// </summary>
        internal bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                this.headerForm = (UIHeaderForm)this.form.BaseForm.headerForm;

                this.cust_Next = new GcCustomMoveToNextContorol_Cust();
                this.cust_Next.accessor = this.accessor;
                this.cust_Next.map = this.map;
                this.cust_Previous = new GcCustomMoveToPreviousContorol_Cust();
                this.cust_Previous.accessor = this.accessor;
                this.cust_Previous.map = this.map;

                this.ButtonInit();
                this.EventInit();

                // コントロールを初期化
                if (!this.ControlInit())
                {
                    ret = false;
                    return ret;
                }

                if (!this.ControlLock())
                {
                    ret = false;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            ButtonControlUtility.SetButtonInfo(
                new ButtonSetting().LoadButtonSetting(Assembly.GetExecutingAssembly(), ConstCls.BUTTON_INFO_XML_PATH),
                this.form.BaseForm, this.form.WindowType);

            this.form.BaseForm.txb_process.Tag = "[Enter]を押下すると指定した番号の処理が実行されます";

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベントの初期化
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            #region イベント削除

            // ファンクション・プロセスボタン
            this.form.BaseForm.bt_func1.Click -= this.form.bt_func1_Click;
            this.form.BaseForm.bt_func7.Click -= this.form.bt_func7_Click;
            this.form.BaseForm.bt_func8.Click -= this.form.bt_func8_Click;
            this.form.BaseForm.bt_func9.Click -= this.form.bt_func9_Click;
            this.form.BaseForm.bt_func12.Click -= this.form.bt_func12_Click;
            this.form.BaseForm.bt_process1.Click -= this.form.bt_process1_Click;

            // 取引先業者現場
            this.form.TORIHIKISAKI_CD.Validating -= this.form.TORIHIKISAKI_CD_Validating;
            this.form.GYOUSHA_CD.Validating -= this.form.GYOUSHA_CD_Validating;
            this.form.GENBA_CD.Validating -= this.form.GENBA_CD_Validating;
            this.form.UPN_GYOUSHA_CD.Validating -= this.form.UPN_GYOUSHA_CD_Validating;
            this.form.NIZUMI_GYOUSHA_CD.Validating -= this.form.NIZUMI_GYOUSHA_CD_Validating;
            this.form.NIZUMI_GENBA_CD.Validating -= this.form.NIZUMI_GENBA_CD_Validating;
            this.form.NIOROSHI_GYOUSHA_CD.Validating -= this.form.NIOROSHI_GYOUSHA_CD_Validating;
            this.form.NIOROSHI_GENBA_CD.Validating -= this.form.NIOROSHI_GENBA_CD_Validating;
            this.form.EIGYOUTANTOU_CD.Validating -= this.form.EIGYOUTANTOU_CD_Validating;
            this.form.txtDenshuKbn.TextChanged -= this.form.txtDenshuKbn_TextChanged;

            #region 明細グリッド

            this.form.mrDetail.CellEnter -= this.form.mrDetail_CellEnter;
            this.form.mrDetail.CellValidating -= this.form.mrDetail_CellValidating;
            this.form.mrDetail.CellValidated -= this.form.mrDetail_CellValidated;
            this.form.mrDetail.CellClick -= this.form.mrDetail_CellClick;

            #endregion

            #endregion

            // ファンクション・プロセスボタン
            this.form.BaseForm.bt_func1.Click += this.form.bt_func1_Click;
            this.form.BaseForm.bt_func7.Click += this.form.bt_func7_Click;
            this.form.BaseForm.bt_func8.Click += this.form.bt_func8_Click;
            this.form.BaseForm.bt_func9.Click += this.form.bt_func9_Click;
            this.form.BaseForm.bt_func12.Click += this.form.bt_func12_Click;
            this.form.BaseForm.bt_process1.Click += this.form.bt_process1_Click;

            // 取引先業者現場
            this.form.TORIHIKISAKI_CD.Validating += this.form.TORIHIKISAKI_CD_Validating;
            this.form.GYOUSHA_CD.Validating += this.form.GYOUSHA_CD_Validating;
            this.form.GENBA_CD.Validating += this.form.GENBA_CD_Validating;
            this.form.UPN_GYOUSHA_CD.Validating += this.form.UPN_GYOUSHA_CD_Validating;
            this.form.NIZUMI_GYOUSHA_CD.Validating += this.form.NIZUMI_GYOUSHA_CD_Validating;
            this.form.NIZUMI_GENBA_CD.Validating += this.form.NIZUMI_GENBA_CD_Validating;
            this.form.NIOROSHI_GYOUSHA_CD.Validating += this.form.NIOROSHI_GYOUSHA_CD_Validating;
            this.form.NIOROSHI_GENBA_CD.Validating += this.form.NIOROSHI_GENBA_CD_Validating;
            this.form.EIGYOUTANTOU_CD.Validating += this.form.EIGYOUTANTOU_CD_Validating;
            this.form.txtDenshuKbn.TextChanged += this.form.txtDenshuKbn_TextChanged;
            this.form.SizeChanged += new EventHandler(this.form.mrDetail_SizeChanged);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Shift | Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Shift | Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Next, Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Next, Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Previous, Keys.Shift | Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Previous, Keys.Shift | Keys.Tab);

            #region 明細グリッド

            this.form.mrDetail.CellEnter += this.form.mrDetail_CellEnter;
            this.form.mrDetail.CellValidating += this.form.mrDetail_CellValidating;
            this.form.mrDetail.CellClick += this.form.mrDetail_CellClick;
            this.form.mrDetail.CellValidated += this.form.mrDetail_CellValidated;

            #endregion

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面コントロールの制御

        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <returns></returns>
        public bool ControlInit()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ヘッダー部
                const string CSV_OUTPUT_KBN = "更新CSV出力";
                const string MASTER_TANKA_UPDATE = "マスタ単価更新";
                const string CSV_PATH = "CSV保管先";
                this.headerForm.txtCsvOutputKbn.Text = this.GetUserProfileValue(userProfile, CSV_OUTPUT_KBN);
                this.headerForm.txtUpdateMode.Text = this.GetUserProfileValue(userProfile, MASTER_TANKA_UPDATE);
                this.csvPath = this.GetUserProfileValue(userProfile, CSV_PATH);

                this.form.txtKakuteiKbn.Text = "3";

                string denshuKbn = "";
                if (AuthManager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    denshuKbn = "3";
                }
                else if (AuthManager.CheckAuthority("G051", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    denshuKbn = "1";
                }
                else if (AuthManager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    denshuKbn = "2";
                }

                this.form.txtDenshuKbn.Text = denshuKbn;

                DateTime now = DateTime.Now;
                this.form.HIDUKE_FROM.Value = now;
                this.form.HIDUKE_TO.Value = now;

                this.form.KYOTEN_CD.Text = string.Empty;
                this.form.KYOTEN_NAME_RYAKU.Text = string.Empty;
                this.form.KYOTEN_CD.Text = "99";
                if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text.ToString()))
                {
                    this.form.KYOTEN_CD.Text = this.form.KYOTEN_CD.Text.ToString().PadLeft(this.form.KYOTEN_CD.MaxLength, '0');
                    this.form.KYOTEN_NAME_RYAKU.Text = this.accessor.GetKyotenNameFast(this.form.KYOTEN_CD.Text);
                }

                // 取引先業者現場
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.UPN_GYOUSHA_CD.Text = string.Empty;
                this.form.UPN_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIZUMI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                this.form.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.form.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.EIGYOUTANTOU_CD.Text = string.Empty;
                this.form.EIGYOUTANTOU_NAME_RYAKU.Text = string.Empty;

                // 入力エラークリア
                this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
                this.form.GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.GENBA_CD.IsInputErrorOccured = false;
                this.form.UPN_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
                this.form.EIGYOUTANTOU_CD.IsInputErrorOccured = false;

                // 明細クリア
                this.form.mrDetail.AllowUserToAddRows =
                    this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.form.mrDetail.Rows.Clear();

            }
            catch (Exception ex)
            {
                LogUtility.Error("ControlInit", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// コントロールの活性制御
        /// </summary>
        /// <returns></returns>
        public bool ControlLock()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
            }
            catch (Exception ex)
            {
                LogUtility.Error("ControlLock", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                ret = false;
            }

            return ret;
        }

        #endregion

        #region 検索結果を画面に表示

        /// <summary>
        /// 検索結果を画面に表示
        /// </summary>
        internal void DisplayEntitesToForm()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.DisplayEntityToForm();
            }
            catch (Exception ex)
            {
                LogUtility.Error("DisplayEntitesToForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 伝票情報表示
        /// </summary>
        private void DisplayEntityToForm()
        {
            LogUtility.DebugMethodStart();

            try
            {
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        #region 受入
                        this.form.mrDetail.Rows.Add(this.tuResult.Count);
                        for (int i = 0; i < this.tuResult.Count; i++)
                        {
                            T_UKEIRE_ENTRY tue = this.tuResult[i].entry;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_CHECK].Value = false;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tue.UKEIRE_NUMBER.Value;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = tue.KYOTEN_CD.IsNull ? "" : tue.KYOTEN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tue.KYOTEN_CD.IsNull ? "" : tue.KYOTEN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KAKUTEI_KBN].Value = SalesPaymentConstans.GetKakuteiKbnName(tue.KAKUTEI_KBN.Value);
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = tue.DENPYOU_DATE.Value;
                            if (!tue.URIAGE_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tue.URIAGE_DATE.Value;
                            }
                            if (!tue.SHIHARAI_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tue.SHIHARAI_DATE.Value;
                            }
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tue.TORIHIKISAKI_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tue.TORIHIKISAKI_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tue.GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tue.GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = tue.GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tue.GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = tue.NIOROSHI_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = tue.NIOROSHI_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = tue.NIOROSHI_GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = tue.NIOROSHI_GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = tue.EIGYOU_TANTOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = tue.EIGYOU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NYUURYOKU_TANTOUSHA_NAME].Value = tue.NYUURYOKU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = tue.SHARYOU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = tue.SHARYOU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = tue.SHASHU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = tue.SHASHU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tue.UNPAN_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tue.UNPAN_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = tue.UNTENSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = tue.UNTENSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = tue.KEITAI_KBN_CD.IsNull ? "" : tue.KEITAI_KBN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(tue.KEITAI_KBN_CD.IsNull ? "" : tue.KEITAI_KBN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = tue.DAIKAN_KBN.IsNull ? "" : tue.DAIKAN_KBN.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(tue.DAIKAN_KBN.IsNull ? "" : tue.DAIKAN_KBN.Value.ToString()));
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = tue.MANIFEST_SHURUI_CD.IsNull ? "" : tue.MANIFEST_SHURUI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = this.accessor.GetManiShuruiNameFast(tue.MANIFEST_SHURUI_CD.IsNull ? "" : tue.MANIFEST_SHURUI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = tue.MANIFEST_TEHAI_CD.IsNull ? "" : tue.MANIFEST_TEHAI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = this.accessor.GetManiTehaiNameFast(tue.MANIFEST_TEHAI_CD.IsNull ? "" : tue.MANIFEST_TEHAI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = tue.DENPYOU_BIKOU;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = tue.TAIRYUU_BIKOU;

                            this.map.Add(this.MapInit(this.dto.denshuKbnCd));
                            bool shokuchi = false;
                            shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = tue.TORIHIKISAKI_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tue.GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tue.GYOUSHA_CD, GENBA_CD = tue.GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tue.NIOROSHI_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tue.NIOROSHI_GYOUSHA_CD, GENBA_CD = tue.NIOROSHI_GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tue.UNPAN_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = tue.UNPAN_GYOUSHA_CD, SHARYOU_CD = tue.SHARYOU_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                        }
                        #endregion
                        break;

                    case "2":
                        #region 出荷
                        this.form.mrDetail.Rows.Add(this.tsResult.Count);
                        for (int i = 0; i < this.tsResult.Count; i++)
                        {
                            T_SHUKKA_ENTRY tse = this.tsResult[i].entry;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_CHECK].Value = false;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tse.SHUKKA_NUMBER.Value;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = tse.KYOTEN_CD.IsNull ? "" : tse.KYOTEN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tse.KYOTEN_CD.IsNull ? "" : tse.KYOTEN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KAKUTEI_KBN].Value = SalesPaymentConstans.GetKakuteiKbnName(tse.KAKUTEI_KBN.Value);
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = tse.DENPYOU_DATE.Value;
                            if (!tse.URIAGE_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tse.URIAGE_DATE.Value;
                            }
                            if (!tse.SHIHARAI_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tse.SHIHARAI_DATE.Value;
                            }
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tse.TORIHIKISAKI_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tse.TORIHIKISAKI_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tse.GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tse.GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = tse.GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tse.GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value = tse.NIZUMI_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = tse.NIZUMI_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = tse.NIZUMI_GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = tse.NIZUMI_GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = tse.EIGYOU_TANTOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = tse.EIGYOU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NYUURYOKU_TANTOUSHA_NAME].Value = tse.NYUURYOKU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = tse.SHARYOU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = tse.SHARYOU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = tse.SHASHU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = tse.SHASHU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tse.UNPAN_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tse.UNPAN_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = tse.UNTENSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = tse.UNTENSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = tse.KEITAI_KBN_CD.IsNull ? "" : tse.KEITAI_KBN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(tse.KEITAI_KBN_CD.IsNull ? "" : tse.KEITAI_KBN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = tse.DAIKAN_KBN.IsNull ? "" : tse.DAIKAN_KBN.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(tse.DAIKAN_KBN.IsNull ? "" : tse.DAIKAN_KBN.Value.ToString()));
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = tse.MANIFEST_SHURUI_CD.IsNull ? "" : tse.MANIFEST_SHURUI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = this.accessor.GetManiShuruiNameFast(tse.MANIFEST_SHURUI_CD.IsNull ? "" : tse.MANIFEST_SHURUI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = tse.MANIFEST_TEHAI_CD.IsNull ? "" : tse.MANIFEST_TEHAI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = this.accessor.GetManiTehaiNameFast(tse.MANIFEST_TEHAI_CD.IsNull ? "" : tse.MANIFEST_TEHAI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = tse.DENPYOU_BIKOU;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = tse.TAIRYUU_BIKOU;

                            this.map.Add(this.MapInit(this.dto.denshuKbnCd));
                            bool shokuchi = false;
                            shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = tse.TORIHIKISAKI_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tse.GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tse.GYOUSHA_CD, GENBA_CD = tse.GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tse.NIZUMI_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tse.NIZUMI_GYOUSHA_CD, GENBA_CD = tse.NIZUMI_GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tse.UNPAN_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = tse.UNPAN_GYOUSHA_CD, SHARYOU_CD = tse.SHARYOU_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                        }
                        #endregion
                        break;

                    case "3":
                        #region 売上支払
                        this.form.mrDetail.Rows.Add(this.tusResult.Count);
                        for (int i = 0; i < this.tusResult.Count; i++)
                        {
                            T_UR_SH_ENTRY tuse = this.tusResult[i].entry;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_CHECK].Value = false;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tuse.UR_SH_NUMBER.Value;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = tuse.KYOTEN_CD.IsNull ? "" : tuse.KYOTEN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tuse.KYOTEN_CD.IsNull ? "" : tuse.KYOTEN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KAKUTEI_KBN].Value = SalesPaymentConstans.GetKakuteiKbnName(tuse.KAKUTEI_KBN.Value);
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = tuse.DENPYOU_DATE.Value;
                            if (!tuse.URIAGE_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tuse.URIAGE_DATE.Value;
                            }
                            if (!tuse.SHIHARAI_DATE.IsNull)
                            {
                                this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tuse.SHIHARAI_DATE.Value;
                            }
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tuse.TORIHIKISAKI_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tuse.TORIHIKISAKI_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tuse.GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tuse.GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = tuse.GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tuse.GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value = tuse.NIZUMI_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = tuse.NIZUMI_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = tuse.NIZUMI_GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = tuse.NIZUMI_GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = tuse.NIOROSHI_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = tuse.NIOROSHI_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = tuse.NIOROSHI_GENBA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = tuse.NIOROSHI_GENBA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = tuse.EIGYOU_TANTOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = tuse.EIGYOU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NYUURYOKU_TANTOUSHA_NAME].Value = tuse.NYUURYOKU_TANTOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = tuse.SHARYOU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = tuse.SHARYOU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = tuse.SHASHU_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = tuse.SHASHU_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tuse.UNPAN_GYOUSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tuse.UNPAN_GYOUSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = tuse.UNTENSHA_CD;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = tuse.UNTENSHA_NAME;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = tuse.KEITAI_KBN_CD.IsNull ? "" : tuse.KEITAI_KBN_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(tuse.KEITAI_KBN_CD.IsNull ? "" : tuse.KEITAI_KBN_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = string.Empty;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = string.Empty;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = tuse.MANIFEST_SHURUI_CD.IsNull ? "" : tuse.MANIFEST_SHURUI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = this.accessor.GetManiShuruiNameFast(tuse.MANIFEST_SHURUI_CD.IsNull ? "" : tuse.MANIFEST_SHURUI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = tuse.MANIFEST_TEHAI_CD.IsNull ? "" : tuse.MANIFEST_TEHAI_CD.Value.ToString();
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = this.accessor.GetManiTehaiNameFast(tuse.MANIFEST_TEHAI_CD.IsNull ? "" : tuse.MANIFEST_TEHAI_CD.Value.ToString());
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = tuse.DENPYOU_BIKOU;
                            this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = string.Empty;

                            this.map.Add(this.MapInit(this.dto.denshuKbnCd));
                            bool shokuchi = false;
                            shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = tuse.TORIHIKISAKI_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tuse.GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tuse.GYOUSHA_CD, GENBA_CD = tuse.GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tuse.NIZUMI_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tuse.NIZUMI_GYOUSHA_CD, GENBA_CD = tuse.NIZUMI_GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tuse.NIOROSHI_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = tuse.NIOROSHI_GYOUSHA_CD, GENBA_CD = tuse.NIOROSHI_GENBA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = tuse.UNPAN_GYOUSHA_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                            shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = tuse.UNPAN_GYOUSHA_CD, SHARYOU_CD = tuse.SHARYOU_CD });
                            this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DisplayEntryEntityToForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 登録用データ作成

        /// <summary>
        /// 登録用データ作成
        /// </summary>
        /// <returns></returns>
        public bool CreateEntites()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            this.dayDic = new Dictionary<string, S_NUMBER_DAY[]>();
            this.yearDic = new Dictionary<string, S_NUMBER_YEAR[]>();
            try
            {
                // 画面モード別処理
                switch (this.form.txtDenshuKbn.Text)
                {
                    case "1":
                        #region 受入
                        this.tuRegist = new List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>>();
                        for (int i = 0; i < this.form.mrDetail.Rows.Count; i++)
                        {
                            if (this.form.mrDetail.Rows[i].Cells[0].Value != null && (bool)this.form.mrDetail.Rows[i].Cells[0].Value == true)
                            {
                                ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto = this.tuResult[i];
                                ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> newDto = new ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>();
                                newDto.entry = CopyEntity<T_UKEIRE_ENTRY, T_UKEIRE_ENTRY>.Copy(dto.entry);
                                for (int j = 0; j < dto.detailList.Count; j++)
                                {
                                    newDto.detailList.Add(CopyEntity<T_UKEIRE_DETAIL, T_UKEIRE_DETAIL>.Copy(dto.detailList[j]));
                                }
                                foreach (string key in dto.detailZaikoUkeireDetails.Keys)
                                {
                                    List<T_ZAIKO_UKEIRE_DETAIL> detailList = new List<T_ZAIKO_UKEIRE_DETAIL>();
                                    List<T_ZAIKO_UKEIRE_DETAIL> oldDetailList = dto.detailZaikoUkeireDetails[key];
                                    foreach (T_ZAIKO_UKEIRE_DETAIL old in oldDetailList)
                                    {
                                        detailList.Add(CopyEntity<T_ZAIKO_UKEIRE_DETAIL, T_ZAIKO_UKEIRE_DETAIL>.Copy(old));
                                    }
                                    newDto.detailZaikoUkeireDetails[key] = detailList;
                                }
                                foreach (string key in dto.detailZaikoHinmeiHuriwakes.Keys)
                                {
                                    List<T_ZAIKO_HINMEI_HURIWAKE> detailList = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                                    List<T_ZAIKO_HINMEI_HURIWAKE> oldDetailList = dto.detailZaikoHinmeiHuriwakes[key];
                                    foreach (T_ZAIKO_HINMEI_HURIWAKE old in oldDetailList)
                                    {
                                        detailList.Add(CopyEntity<T_ZAIKO_HINMEI_HURIWAKE, T_ZAIKO_HINMEI_HURIWAKE>.Copy(old));
                                    }
                                    newDto.detailZaikoHinmeiHuriwakes[key] = detailList;
                                }
                                foreach (var contena in dto.contenaResults)
                                {
                                    newDto.contenaResults.Add(CopyEntity<T_CONTENA_RESULT, T_CONTENA_RESULT>.Copy(contena));
                                }

                                newDto.denpyouNo = dto.denpyouNo;
                                newDto.rowIndex = dto.rowIndex;

                                // 画面入力値設定
                                newDto.entry.KYOTEN_CD = Convert.ToInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value);
                                newDto.entry.DENPYOU_DATE = Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
                                newDto.entry.URIAGE_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value);
                                newDto.entry.SHIHARAI_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value); ;
                                newDto.entry.TORIHIKISAKI_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                newDto.entry.TORIHIKISAKI_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value);
                                newDto.entry.GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value);
                                newDto.entry.GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value);
                                newDto.entry.GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value);
                                newDto.entry.GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value);
                                newDto.entry.NIOROSHI_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value);
                                newDto.entry.NIOROSHI_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value);
                                newDto.entry.NIOROSHI_GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value);
                                newDto.entry.NIOROSHI_GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value);
                                newDto.entry.SHARYOU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value);
                                newDto.entry.SHARYOU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value);
                                newDto.entry.SHASHU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value);
                                newDto.entry.SHASHU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value);
                                newDto.entry.UNPAN_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value);
                                newDto.entry.UNPAN_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value);
                                newDto.entry.UNTENSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value);
                                newDto.entry.UNTENSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value);
                                short? keitaiKbn = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value);
                                if (keitaiKbn == null)
                                {
                                    newDto.entry.KEITAI_KBN_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.KEITAI_KBN_CD = keitaiKbn.Value;
                                }
                                short? daikanKbn = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value);
                                if (daikanKbn == null)
                                {
                                    newDto.entry.DAIKAN_KBN = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.DAIKAN_KBN = daikanKbn.Value;
                                }
                                short? maniShurui = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value);
                                if (maniShurui == null)
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = maniShurui.Value;
                                }
                                short? maniTehai = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value);
                                if (maniTehai == null)
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = maniTehai.Value;
                                }
                                newDto.entry.DENPYOU_BIKOU = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value);
                                newDto.entry.TAIRYUU_BIKOU = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value);

                                newDto.seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(dto.entry.TORIHIKISAKI_CD);
                                newDto.shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(dto.entry.TORIHIKISAKI_CD);

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                // 日連番取得
                                S_NUMBER_DAY[] numberDays = null;
                                DateTime denpyouDate = newDto.entry.DENPYOU_DATE.Value;

                                short kyotenCd = newDto.entry.KYOTEN_CD.IsNull ? (short)-1 : newDto.entry.KYOTEN_CD.Value;    // 拠点CD
                                string dayKey = denpyouDate.Date.ToShortDateString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.dayDic.ContainsKey(dayKey))
                                    {
                                        numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE, kyotenCd);
                                    }
                                    else
                                    {
                                        numberDays = this.dayDic[dayKey];
                                    }
                                }

                                // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
                                S_NUMBER_YEAR[] numberYeas = null;
                                SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                string yearKey = numberedYear.Value.ToString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.yearDic.ContainsKey(yearKey))
                                    {
                                        numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE, kyotenCd);
                                    }
                                    else
                                    {
                                        numberYeas = this.yearDic[yearKey];
                                    }
                                }
                                // 日連番
                                short beforeKotenCd = -1;
                                short.TryParse(dto.entry.KYOTEN_CD.ToString(), out beforeKotenCd);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || !dto.entry.DENPYOU_DATE.Equals((SqlDateTime)denpyouDate))
                                {
                                    if (numberDays == null || numberDays.Length < 1)
                                    {
                                        newDto.entry.DATE_NUMBER = 1;

                                        S_NUMBER_DAY day = new S_NUMBER_DAY();
                                        day.NUMBERED_DAY = denpyouDate.Date;
                                        day.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
                                        day.KYOTEN_CD = kyotenCd;
                                        day.CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        day.DELETE_FLG = false;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(day);
                                        dataBinderNumberDay.SetSystemProperty(day, false);

                                        this.dayDic[dayKey] = new S_NUMBER_DAY[] { day };
                                    }
                                    else
                                    {
                                        newDto.entry.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                                        if (!this.dayDic.ContainsKey(dayKey))
                                        {
                                            this.dayDic[dayKey] = numberDays;
                                        }
                                        this.dayDic[dayKey][0].CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dayDic[dayKey][0]);
                                        dataBinderNumberDay.SetSystemProperty(this.dayDic[dayKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.DATE_NUMBER = newDto.entry.DATE_NUMBER;
                                }
                                // 年連番
                                SqlInt32 beforNumberedYear = CorpInfoUtility.GetCurrentYear((DateTime)dto.entry.DENPYOU_DATE, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || (numberYeas == null || numberYeas.Length < 1 || beforNumberedYear.Value != numberYeas[0].NUMBERED_YEAR.Value))
                                {
                                    if (numberYeas == null || numberYeas.Length < 1)
                                    {
                                        newDto.entry.YEAR_NUMBER = 1;

                                        S_NUMBER_YEAR year = new S_NUMBER_YEAR();
                                        year.NUMBERED_YEAR = numberedYear;
                                        year.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
                                        year.KYOTEN_CD = kyotenCd;
                                        year.CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        year.DELETE_FLG = false;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(year);
                                        dataBinderNumberYear.SetSystemProperty(year, false);

                                        this.yearDic[yearKey] = new S_NUMBER_YEAR[] { year };
                                    }
                                    else
                                    {
                                        newDto.entry.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;

                                        if (!this.yearDic.ContainsKey(yearKey))
                                        {
                                            this.yearDic[yearKey] = numberYeas;
                                        }
                                        this.yearDic[yearKey][0].CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(this.yearDic[yearKey][0]);
                                        dataBinderNumberYear.SetSystemProperty(this.yearDic[yearKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.YEAR_NUMBER = dto.entry.YEAR_NUMBER;
                                }
                                // 取引区分、税計算区分、税区分
                                if (this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value))
                                    && !Convert.ToString(dto.entry.TORIHIKISAKI_CD).Equals(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null)
                                    {
                                        newDto.seikyuu = SeikyuuTorihikiKbn;
                                        newDto.entry.URIAGE_TORIHIKI_KBN_CD = SeikyuuTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KEISAN_KBN_CD = SeikyuuTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KBN_CD = SeikyuuTorihikiKbn.ZEI_KBN_CD;
                                    }
                                    if (ShiharaiTorihikiKbn != null)
                                    {
                                        newDto.shiharai = ShiharaiTorihikiKbn;
                                        newDto.entry.SHIHARAI_TORIHIKI_KBN_CD = ShiharaiTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = ShiharaiTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KBN_CD = ShiharaiTorihikiKbn.ZEI_KBN_CD;
                                    }
                                }

                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_UKEIRE_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);

                                // 元データの論理削除
                                foreach (T_CONTENA_RESULT entity in dto.contenaResults)
                                {
                                    entity.DELETE_FLG = true;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                }
                                foreach (List<T_ZAIKO_UKEIRE_DETAIL> entityList in dto.detailZaikoUkeireDetails.Values)
                                {
                                    foreach (T_ZAIKO_UKEIRE_DETAIL entity in entityList)
                                    {
                                        entity.DELETE_FLG = true;
                                        // 自動設定
                                        var dataBinderZaikoUkeireDetail = new DataBinderLogic<T_ZAIKO_UKEIRE_DETAIL>(entity);
                                        dataBinderZaikoUkeireDetail.SetSystemProperty(entity, false);
                                    }
                                }
                                // 2次
                                foreach (T_CONTENA_RESULT entity in newDto.contenaResults)
                                {
                                    // systemidとseqは入力テーブルと同じ内容をセットする
                                    entity.SYSTEM_ID = newDto.entry.SYSTEM_ID;
                                    entity.SEQ = newDto.entry.SEQ;
                                    // 伝種区分セット
                                    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                    entity.CREATE_DATE = dto.entry.CREATE_DATE;
                                    entity.CREATE_PC = dto.entry.CREATE_PC;
                                    entity.CREATE_USER = dto.entry.CREATE_USER;
                                }
                                foreach (List<T_ZAIKO_UKEIRE_DETAIL> entityList in newDto.detailZaikoUkeireDetails.Values)
                                {
                                    foreach (T_ZAIKO_UKEIRE_DETAIL entity in entityList)
                                    {
                                        entity.SEQ = newDto.entry.SEQ;
                                        // 自動設定
                                        var dataBinderZaikoUkeireDetail = new DataBinderLogic<T_ZAIKO_UKEIRE_DETAIL>(entity);
                                        dataBinderZaikoUkeireDetail.SetSystemProperty(entity, false);
                                    }
                                }
                                foreach (List<T_ZAIKO_HINMEI_HURIWAKE> entityList in newDto.detailZaikoHinmeiHuriwakes.Values)
                                {
                                    foreach (T_ZAIKO_HINMEI_HURIWAKE entity in entityList)
                                    {
                                        entity.SEQ = newDto.entry.SEQ;
                                        // 自動設定
                                        var dataBinderZaikoHinmeiDetail = new DataBinderLogic<T_ZAIKO_HINMEI_HURIWAKE>(entity);
                                        dataBinderZaikoHinmeiDetail.SetSystemProperty(entity, false);
                                    }
                                }

                                newDto.entry.URIAGE_SHOUHIZEI_RATE = 0;
                                newDto.entry.SHIHARAI_SHOUHIZEI_RATE = 0;
                                if (!newDto.entry.URIAGE_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.URIAGE_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.URIAGE_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }
                                if (!newDto.entry.SHIHARAI_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.SHIHARAI_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.SHIHARAI_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }

                                foreach (T_UKEIRE_DETAIL detail in newDto.detailList)
                                {
                                    detail.SEQ = newDto.entry.SEQ;
                                    if (this.headerForm.txtUpdateMode.Text == "1")
                                    {
                                        string key = string.Format("{0}_{1}", detail.SYSTEM_ID.Value.ToString(), detail.DETAIL_SYSTEM_ID.Value.ToString());
                                        var oldTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();

                                        var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                                            (short)SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.TORIHIKISAKI_CD,
                                            newDto.entry.GYOUSHA_CD,
                                            newDto.entry.GENBA_CD,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GENBA_CD,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                            );

                                        // 個別品名単価から情報が取れない場合は基本品名単価の検索
                                        if (kobetsuhinmeiTanka == null)
                                        {
                                            var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                                                (short)SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GENBA_CD,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                                );
                                            if (kihonHinmeiTanka != null)
                                            {
                                                detail.TANKA = kihonHinmeiTanka.TANKA.Value;
                                            }
                                            else
                                            {
                                                detail.TANKA = SqlDecimal.Null;
                                            }
                                        }
                                        else
                                        {
                                            detail.TANKA = kobetsuhinmeiTanka.TANKA.Value;
                                        }
                                        var newTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();
                                        // 単価に変更があった場合のみ再計算
                                        if (!oldTanka.Equals(newTanka))
                                        {
                                            newDto.tankaChanged[key] = true;
                                            if (!this.CalcDetaiKingaku(newDto, detail))
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            newDto.tankaChanged[key] = false;
                                        }
                                    }
                                    var dataBinderDetailResult = new DataBinderLogic<T_UKEIRE_DETAIL>(detail);
                                    dataBinderDetailResult.SetSystemProperty(detail, false);
                                }

                                this.ZeiKeisan(newDto);

                                this.tuRegist.Add(newDto);
                            }
                        }
                        #endregion
                        break;
                    case "2":
                        #region 出荷
                        this.tsRegist = new List<ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>>();
                        for (int i = 0; i < this.form.mrDetail.Rows.Count; i++)
                        {
                            if (this.form.mrDetail.Rows[i].Cells[0].Value != null && (bool)this.form.mrDetail.Rows[i].Cells[0].Value == true)
                            {
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto = this.tsResult[i];
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> newDto = new ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>();
                                newDto.entry = CopyEntity<T_SHUKKA_ENTRY, T_SHUKKA_ENTRY>.Copy(dto.entry);
                                for (int j = 0; j < dto.detailList.Count; j++)
                                {
                                    newDto.detailList.Add(CopyEntity<T_SHUKKA_DETAIL, T_SHUKKA_DETAIL>.Copy(dto.detailList[j]));
                                }
                                foreach (string key in dto.detailZaikoShukkaDetails.Keys)
                                {
                                    List<T_ZAIKO_SHUKKA_DETAIL> detailList = new List<T_ZAIKO_SHUKKA_DETAIL>();
                                    List<T_ZAIKO_SHUKKA_DETAIL> oldDetailList = dto.detailZaikoShukkaDetails[key];
                                    foreach (T_ZAIKO_SHUKKA_DETAIL old in oldDetailList)
                                    {
                                        detailList.Add(CopyEntity<T_ZAIKO_SHUKKA_DETAIL, T_ZAIKO_SHUKKA_DETAIL>.Copy(old));
                                    }
                                    newDto.detailZaikoShukkaDetails[key] = detailList;
                                }
                                foreach (string key in dto.detailZaikoHinmeiHuriwakes.Keys)
                                {
                                    List<T_ZAIKO_HINMEI_HURIWAKE> detailList = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                                    List<T_ZAIKO_HINMEI_HURIWAKE> oldDetailList = dto.detailZaikoHinmeiHuriwakes[key];
                                    foreach (T_ZAIKO_HINMEI_HURIWAKE old in oldDetailList)
                                    {
                                        detailList.Add(CopyEntity<T_ZAIKO_HINMEI_HURIWAKE, T_ZAIKO_HINMEI_HURIWAKE>.Copy(old));
                                    }
                                    newDto.detailZaikoHinmeiHuriwakes[key] = detailList;
                                }
                                foreach (string key in dto.detailKenshuDetails.Keys)
                                {
                                    List<T_KENSHU_DETAIL> detailList = new List<T_KENSHU_DETAIL>();
                                    List<T_KENSHU_DETAIL> oldDetailList = dto.detailKenshuDetails[key];
                                    foreach (T_KENSHU_DETAIL old in oldDetailList)
                                    {
                                        detailList.Add(CopyEntity<T_KENSHU_DETAIL, T_KENSHU_DETAIL>.Copy(old));
                                    }
                                    newDto.detailKenshuDetails[key] = detailList;
                                }

                                newDto.denpyouNo = dto.denpyouNo;
                                newDto.rowIndex = dto.rowIndex;

                                // 画面入力値設定
                                newDto.entry.KYOTEN_CD = Convert.ToInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value);
                                newDto.entry.DENPYOU_DATE = Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
                                newDto.entry.URIAGE_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value);
                                newDto.entry.SHIHARAI_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value); ;
                                newDto.entry.TORIHIKISAKI_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                newDto.entry.TORIHIKISAKI_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value);
                                newDto.entry.GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value);
                                newDto.entry.GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value);
                                newDto.entry.GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value);
                                newDto.entry.GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value);
                                newDto.entry.NIZUMI_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value);
                                newDto.entry.NIZUMI_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value);
                                newDto.entry.NIZUMI_GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value);
                                newDto.entry.NIZUMI_GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value);
                                newDto.entry.SHARYOU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value);
                                newDto.entry.SHARYOU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value);
                                newDto.entry.SHASHU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value);
                                newDto.entry.SHASHU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value);
                                newDto.entry.UNPAN_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value);
                                newDto.entry.UNPAN_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value);
                                newDto.entry.UNTENSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value);
                                newDto.entry.UNTENSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value);
                                short? keitaiKbn = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value);
                                if (keitaiKbn == null)
                                {
                                    newDto.entry.KEITAI_KBN_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.KEITAI_KBN_CD = keitaiKbn.Value;
                                }
                                short? daikanKbn = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value);
                                if (daikanKbn == null)
                                {
                                    newDto.entry.DAIKAN_KBN = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.DAIKAN_KBN = daikanKbn.Value;
                                }
                                short? maniShurui = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value);
                                if (maniShurui == null)
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = maniShurui.Value;
                                }
                                short? maniTehai = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value);
                                if (maniTehai == null)
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = maniTehai.Value;
                                }
                                newDto.entry.DENPYOU_BIKOU = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value);
                                newDto.entry.TAIRYUU_BIKOU = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value);

                                newDto.seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(dto.entry.TORIHIKISAKI_CD);
                                newDto.shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(dto.entry.TORIHIKISAKI_CD);

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                // 日連番取得
                                S_NUMBER_DAY[] numberDays = null;
                                DateTime denpyouDate = newDto.entry.DENPYOU_DATE.Value;

                                short kyotenCd = newDto.entry.KYOTEN_CD.IsNull ? (short)-1 : newDto.entry.KYOTEN_CD.Value;    // 拠点CD
                                string dayKey = denpyouDate.Date.ToShortDateString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.dayDic.ContainsKey(dayKey))
                                    {
                                        numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, kyotenCd);
                                    }
                                    else
                                    {
                                        numberDays = this.dayDic[dayKey];
                                    }
                                }

                                // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
                                S_NUMBER_YEAR[] numberYeas = null;
                                SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                string yearKey = numberedYear.Value.ToString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.yearDic.ContainsKey(yearKey))
                                    {
                                        numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA, kyotenCd);
                                    }
                                    else
                                    {
                                        numberYeas = this.yearDic[yearKey];
                                    }
                                }
                                // 日連番
                                short beforeKotenCd = -1;
                                short.TryParse(dto.entry.KYOTEN_CD.ToString(), out beforeKotenCd);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || !dto.entry.DENPYOU_DATE.Equals((SqlDateTime)denpyouDate))
                                {
                                    if (numberDays == null || numberDays.Length < 1)
                                    {
                                        newDto.entry.DATE_NUMBER = 1;

                                        S_NUMBER_DAY day = new S_NUMBER_DAY();
                                        day.NUMBERED_DAY = denpyouDate.Date;
                                        day.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
                                        day.KYOTEN_CD = kyotenCd;
                                        day.CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        day.DELETE_FLG = false;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(day);
                                        dataBinderNumberDay.SetSystemProperty(day, false);

                                        this.dayDic[dayKey] = new S_NUMBER_DAY[] { day };
                                    }
                                    else
                                    {
                                        newDto.entry.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                                        if (!this.dayDic.ContainsKey(dayKey))
                                        {
                                            this.dayDic[dayKey] = numberDays;
                                        }
                                        this.dayDic[dayKey][0].CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dayDic[dayKey][0]);
                                        dataBinderNumberDay.SetSystemProperty(this.dayDic[dayKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.DATE_NUMBER = newDto.entry.DATE_NUMBER;
                                }
                                // 年連番
                                SqlInt32 beforNumberedYear = CorpInfoUtility.GetCurrentYear((DateTime)dto.entry.DENPYOU_DATE, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || (numberYeas == null || numberYeas.Length < 1 || beforNumberedYear.Value != numberYeas[0].NUMBERED_YEAR.Value))
                                {
                                    if (numberYeas == null || numberYeas.Length < 1)
                                    {
                                        newDto.entry.YEAR_NUMBER = 1;

                                        S_NUMBER_YEAR year = new S_NUMBER_YEAR();
                                        year.NUMBERED_YEAR = numberedYear;
                                        year.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
                                        year.KYOTEN_CD = kyotenCd;
                                        year.CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        year.DELETE_FLG = false;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(year);
                                        dataBinderNumberYear.SetSystemProperty(year, false);

                                        this.yearDic[yearKey] = new S_NUMBER_YEAR[] { year };
                                    }
                                    else
                                    {
                                        newDto.entry.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;

                                        if (!this.yearDic.ContainsKey(yearKey))
                                        {
                                            this.yearDic[yearKey] = numberYeas;
                                        }
                                        this.yearDic[yearKey][0].CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(this.yearDic[yearKey][0]);
                                        dataBinderNumberYear.SetSystemProperty(this.yearDic[yearKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.YEAR_NUMBER = dto.entry.YEAR_NUMBER;
                                }
                                // 取引区分
                                if (this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value))
                                    && !Convert.ToString(dto.entry.TORIHIKISAKI_CD).Equals(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null)
                                    {
                                        newDto.seikyuu = SeikyuuTorihikiKbn;
                                        newDto.entry.URIAGE_TORIHIKI_KBN_CD = SeikyuuTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KEISAN_KBN_CD = SeikyuuTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KBN_CD = SeikyuuTorihikiKbn.ZEI_KBN_CD;
                                    }
                                    if (ShiharaiTorihikiKbn != null)
                                    {
                                        newDto.shiharai = ShiharaiTorihikiKbn;
                                        newDto.entry.SHIHARAI_TORIHIKI_KBN_CD = ShiharaiTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = ShiharaiTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KBN_CD = ShiharaiTorihikiKbn.ZEI_KBN_CD;
                                    }
                                }

                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_SHUKKA_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);

                                // 元データの論理削除
                                foreach (T_CONTENA_RESULT entity in dto.contenaResults)
                                {
                                    entity.DELETE_FLG = true;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                }
                                foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in dto.detailZaikoShukkaDetails.Values)
                                {
                                    foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                                    {
                                        entity.DELETE_FLG = true;
                                        // 自動設定
                                        var dataBinderZaikoShukkaDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
                                        dataBinderZaikoShukkaDetail.SetSystemProperty(entity, false);
                                    }
                                }
                                // 2次
                                foreach (T_CONTENA_RESULT entity in newDto.contenaResults)
                                {
                                    // systemidとseqは入力テーブルと同じ内容をセットする
                                    entity.SYSTEM_ID = newDto.entry.SYSTEM_ID;
                                    entity.SEQ = newDto.entry.SEQ;
                                    // 伝種区分セット
                                    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                    entity.CREATE_DATE = dto.entry.CREATE_DATE;
                                    entity.CREATE_PC = dto.entry.CREATE_PC;
                                    entity.CREATE_USER = dto.entry.CREATE_USER;
                                }
                                foreach (List<T_ZAIKO_HINMEI_HURIWAKE> entityList in newDto.detailZaikoHinmeiHuriwakes.Values)
                                {
                                    foreach (T_ZAIKO_HINMEI_HURIWAKE entity in entityList)
                                    {
                                        entity.SEQ = newDto.entry.SEQ;
                                        // 自動設定
                                        var dataBinderZaikoHinmeiDetail = new DataBinderLogic<T_ZAIKO_HINMEI_HURIWAKE>(entity);
                                        dataBinderZaikoHinmeiDetail.SetSystemProperty(entity, false);
                                    }
                                }
                                foreach (List<T_KENSHU_DETAIL> entityList in newDto.detailKenshuDetails.Values)
                                {
                                    foreach (T_KENSHU_DETAIL entity in entityList)
                                    {
                                        entity.SEQ = newDto.entry.SEQ;
                                        // 自動設定
                                        var dataBinderZaikoHinmeiDetail = new DataBinderLogic<T_KENSHU_DETAIL>(entity);
                                        dataBinderZaikoHinmeiDetail.SetSystemProperty(entity, false);
                                    }
                                }

                                newDto.entry.URIAGE_SHOUHIZEI_RATE = 0;
                                newDto.entry.SHIHARAI_SHOUHIZEI_RATE = 0;
                                if (!newDto.entry.URIAGE_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.URIAGE_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.URIAGE_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }
                                if (!newDto.entry.SHIHARAI_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.SHIHARAI_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.SHIHARAI_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }

                                foreach (T_SHUKKA_DETAIL detail in newDto.detailList)
                                {
                                    detail.SEQ = newDto.entry.SEQ;
                                    if (this.headerForm.txtUpdateMode.Text == "1" && (!newDto.entry.KENSHU_MUST_KBN.IsTrue || newDto.entry.KENSHU_DATE.IsNull))
                                    {
                                        string key = string.Format("{0}_{1}", detail.SYSTEM_ID.Value.ToString(), detail.DETAIL_SYSTEM_ID.Value.ToString());
                                        var oldTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();

                                        var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                                            (short)SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.TORIHIKISAKI_CD,
                                            newDto.entry.GYOUSHA_CD,
                                            newDto.entry.GENBA_CD,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            null,
                                            null,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                            );

                                        // 個別品名単価から情報が取れない場合は基本品名単価の検索
                                        if (kobetsuhinmeiTanka == null)
                                        {
                                            var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                                                (short)SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            null,
                                            null,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                                );
                                            if (kihonHinmeiTanka != null)
                                            {
                                                detail.TANKA = kihonHinmeiTanka.TANKA.Value;
                                            }
                                            else
                                            {
                                                detail.TANKA = SqlDecimal.Null;
                                            }
                                        }
                                        else
                                        {
                                            detail.TANKA = kobetsuhinmeiTanka.TANKA.Value;
                                        }
                                        var newTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();
                                        // 単価に変更があった場合のみ再計算
                                        if (!oldTanka.Equals(newTanka))
                                        {
                                            newDto.tankaChanged[key] = true;
                                            if (!this.CalcDetaiKingaku(newDto, detail))
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            newDto.tankaChanged[key] = false;
                                        }
                                    }
                                    var dataBinderDetailResult = new DataBinderLogic<T_SHUKKA_DETAIL>(detail);
                                    dataBinderDetailResult.SetSystemProperty(detail, false);
                                }

                                this.ZeiKeisan(newDto);

                                this.tsRegist.Add(newDto);
                            }
                        }
                        #endregion
                        break;
                    case "3":
                        #region 売上支払
                        this.tusRegist = new List<ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>>();
                        for (int i = 0; i < this.form.mrDetail.Rows.Count; i++)
                        {
                            if (this.form.mrDetail.Rows[i].Cells[0].Value != null && (bool)this.form.mrDetail.Rows[i].Cells[0].Value == true)
                            {
                                ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto = this.tusResult[i];
                                ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> newDto = new ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>();
                                newDto.entry = CopyEntity<T_UR_SH_ENTRY, T_UR_SH_ENTRY>.Copy(dto.entry);
                                for (int j = 0; j < dto.detailList.Count; j++)
                                {
                                    newDto.detailList.Add(CopyEntity<T_UR_SH_DETAIL, T_UR_SH_DETAIL>.Copy(dto.detailList[j]));
                                }
                                foreach (var contena in dto.contenaResults)
                                {
                                    newDto.contenaResults.Add(CopyEntity<T_CONTENA_RESULT, T_CONTENA_RESULT>.Copy(contena));
                                }

                                newDto.denpyouNo = dto.denpyouNo;
                                newDto.rowIndex = dto.rowIndex;

                                // 画面入力値設定
                                newDto.entry.KYOTEN_CD = Convert.ToInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value);
                                newDto.entry.DENPYOU_DATE = Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value);
                                newDto.entry.URIAGE_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value);
                                newDto.entry.SHIHARAI_DATE = this.ToNDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value) == null ? SqlDateTime.Null : Convert.ToDateTime(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value); ;
                                newDto.entry.TORIHIKISAKI_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                newDto.entry.TORIHIKISAKI_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value);
                                newDto.entry.GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value);
                                newDto.entry.GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value);
                                newDto.entry.GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value);
                                newDto.entry.GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value);
                                newDto.entry.NIZUMI_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value);
                                newDto.entry.NIZUMI_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value);
                                newDto.entry.NIZUMI_GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value);
                                newDto.entry.NIZUMI_GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value);
                                newDto.entry.NIOROSHI_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value);
                                newDto.entry.NIOROSHI_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value);
                                newDto.entry.NIOROSHI_GENBA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value);
                                newDto.entry.NIOROSHI_GENBA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value);
                                newDto.entry.EIGYOU_TANTOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value);
                                newDto.entry.SHARYOU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value);
                                newDto.entry.SHARYOU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value);
                                newDto.entry.SHASHU_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value);
                                newDto.entry.SHASHU_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value);
                                newDto.entry.UNPAN_GYOUSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value);
                                newDto.entry.UNPAN_GYOUSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value);
                                newDto.entry.UNTENSHA_CD = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value);
                                newDto.entry.UNTENSHA_NAME = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value);
                                short? keitaiKbn = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value);
                                if (keitaiKbn == null)
                                {
                                    newDto.entry.KEITAI_KBN_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.KEITAI_KBN_CD = keitaiKbn.Value;
                                }
                                short? maniShurui = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value);
                                if (maniShurui == null)
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_SHURUI_CD = maniShurui.Value;
                                }
                                short? maniTehai = this.ToNInt16(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value);
                                if (maniTehai == null)
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = SqlInt16.Null;
                                }
                                else
                                {
                                    newDto.entry.MANIFEST_TEHAI_CD = maniTehai.Value;
                                }
                                newDto.entry.DENPYOU_BIKOU = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value);

                                newDto.seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(dto.entry.TORIHIKISAKI_CD);
                                newDto.shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(dto.entry.TORIHIKISAKI_CD);

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                // 日連番取得
                                S_NUMBER_DAY[] numberDays = null;
                                DateTime denpyouDate = newDto.entry.DENPYOU_DATE.Value;

                                short kyotenCd = newDto.entry.KYOTEN_CD.IsNull ? (short)-1 : newDto.entry.KYOTEN_CD.Value;    // 拠点CD
                                string dayKey = denpyouDate.Date.ToShortDateString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.dayDic.ContainsKey(dayKey))
                                    {
                                        numberDays = this.accessor.GetNumberDay(denpyouDate.Date, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);
                                    }
                                    else
                                    {
                                        numberDays = this.dayDic[dayKey];
                                    }
                                }

                                // 年連番取得(S_NUMBER_YEARテーブルから情報取得 + 年度の生成処理を追加)
                                S_NUMBER_YEAR[] numberYeas = null;
                                SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(denpyouDate.Date, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                string yearKey = numberedYear.Value.ToString() + "_" + SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI.Value.ToString() + "_" + kyotenCd.ToString();
                                if (-1 < kyotenCd)
                                {
                                    if (!this.yearDic.ContainsKey(yearKey))
                                    {
                                        numberYeas = this.accessor.GetNumberYear(numberedYear, SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI, kyotenCd);
                                    }
                                    else
                                    {
                                        numberYeas = this.yearDic[yearKey];
                                    }
                                }
                                // 日連番
                                short beforeKotenCd = -1;
                                short.TryParse(dto.entry.KYOTEN_CD.ToString(), out beforeKotenCd);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || !dto.entry.DENPYOU_DATE.Equals((SqlDateTime)denpyouDate))
                                {
                                    if (numberDays == null || numberDays.Length < 1)
                                    {
                                        newDto.entry.DATE_NUMBER = 1;

                                        S_NUMBER_DAY day = new S_NUMBER_DAY();
                                        day.NUMBERED_DAY = denpyouDate.Date;
                                        day.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                                        day.KYOTEN_CD = kyotenCd;
                                        day.CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        day.DELETE_FLG = false;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(day);
                                        dataBinderNumberDay.SetSystemProperty(day, false);

                                        this.dayDic[dayKey] = new S_NUMBER_DAY[] { day };
                                    }
                                    else
                                    {
                                        newDto.entry.DATE_NUMBER = numberDays[0].CURRENT_NUMBER + 1;
                                        if (!this.dayDic.ContainsKey(dayKey))
                                        {
                                            this.dayDic[dayKey] = numberDays;
                                        }
                                        this.dayDic[dayKey][0].CURRENT_NUMBER = newDto.entry.DATE_NUMBER;
                                        var dataBinderNumberDay = new DataBinderLogic<S_NUMBER_DAY>(this.dayDic[dayKey][0]);
                                        dataBinderNumberDay.SetSystemProperty(this.dayDic[dayKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.DATE_NUMBER = newDto.entry.DATE_NUMBER;
                                }
                                // 年連番
                                SqlInt32 beforNumberedYear = CorpInfoUtility.GetCurrentYear((DateTime)dto.entry.DENPYOU_DATE, (short)CommonShogunData.CORP_INFO.KISHU_MONTH);
                                if ((beforeKotenCd != kyotenCd
                                    || dto.entry.KYOTEN_CD != kyotenCd)
                                    || (numberYeas == null || numberYeas.Length < 1 || beforNumberedYear.Value != numberYeas[0].NUMBERED_YEAR.Value))
                                {
                                    if (numberYeas == null || numberYeas.Length < 1)
                                    {
                                        newDto.entry.YEAR_NUMBER = 1;

                                        S_NUMBER_YEAR year = new S_NUMBER_YEAR();
                                        year.NUMBERED_YEAR = numberedYear;
                                        year.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                                        year.KYOTEN_CD = kyotenCd;
                                        year.CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        year.DELETE_FLG = false;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(year);
                                        dataBinderNumberYear.SetSystemProperty(year, false);

                                        this.yearDic[yearKey] = new S_NUMBER_YEAR[] { year };
                                    }
                                    else
                                    {
                                        newDto.entry.YEAR_NUMBER = numberYeas[0].CURRENT_NUMBER + 1;

                                        if (!this.yearDic.ContainsKey(yearKey))
                                        {
                                            this.yearDic[yearKey] = numberYeas;
                                        }
                                        this.yearDic[yearKey][0].CURRENT_NUMBER = newDto.entry.YEAR_NUMBER;
                                        var dataBinderNumberYear = new DataBinderLogic<S_NUMBER_YEAR>(this.yearDic[yearKey][0]);
                                        dataBinderNumberYear.SetSystemProperty(this.yearDic[yearKey][0], false);
                                    }
                                }
                                else
                                {
                                    newDto.entry.YEAR_NUMBER = dto.entry.YEAR_NUMBER;
                                }
                                // 取引区分
                                if (this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value != null
                                    && !string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value))
                                    && !Convert.ToString(dto.entry.TORIHIKISAKI_CD).Equals(Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value)))
                                {
                                    string torihikisakiCd = Convert.ToString(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value);
                                    M_TORIHIKISAKI_SEIKYUU SeikyuuTorihikiKbn = this.accessor.GetTorihikisakiSeikyuuDataByCode(torihikisakiCd);
                                    M_TORIHIKISAKI_SHIHARAI ShiharaiTorihikiKbn = this.accessor.GetTorihikisakiShiharaiDataByCode(torihikisakiCd);

                                    if (SeikyuuTorihikiKbn != null)
                                    {
                                        newDto.seikyuu = SeikyuuTorihikiKbn;
                                        newDto.entry.URIAGE_TORIHIKI_KBN_CD = SeikyuuTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KEISAN_KBN_CD = SeikyuuTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.URIAGE_ZEI_KBN_CD = SeikyuuTorihikiKbn.ZEI_KBN_CD;
                                    }
                                    if (ShiharaiTorihikiKbn != null)
                                    {
                                        newDto.shiharai = ShiharaiTorihikiKbn;
                                        newDto.entry.SHIHARAI_TORIHIKI_KBN_CD = ShiharaiTorihikiKbn.TORIHIKI_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = ShiharaiTorihikiKbn.ZEI_KEISAN_KBN_CD;
                                        newDto.entry.SHIHARAI_ZEI_KBN_CD = ShiharaiTorihikiKbn.ZEI_KBN_CD;
                                    }
                                }

                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_UR_SH_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);

                                // 元データの論理削除
                                foreach (T_CONTENA_RESULT entity in dto.contenaResults)
                                {
                                    entity.DELETE_FLG = true;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                }
                                // 2次
                                foreach (T_CONTENA_RESULT entity in newDto.contenaResults)
                                {
                                    // systemidとseqは入力テーブルと同じ内容をセットする
                                    entity.SYSTEM_ID = newDto.entry.SYSTEM_ID;
                                    entity.SEQ = newDto.entry.SEQ;
                                    // 伝種区分セット
                                    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                    entity.CREATE_DATE = dto.entry.CREATE_DATE;
                                    entity.CREATE_PC = dto.entry.CREATE_PC;
                                    entity.CREATE_USER = dto.entry.CREATE_USER;
                                }

                                newDto.entry.URIAGE_SHOUHIZEI_RATE = 0;
                                newDto.entry.SHIHARAI_SHOUHIZEI_RATE = 0;
                                if (!newDto.entry.URIAGE_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.URIAGE_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.URIAGE_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }
                                if (!newDto.entry.SHIHARAI_DATE.IsNull)
                                {
                                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(newDto.entry.SHIHARAI_DATE.Value.Date);
                                    if (shouhizeiEntity != null && !shouhizeiEntity.SHOUHIZEI_RATE.IsNull)
                                    {
                                        newDto.entry.SHIHARAI_SHOUHIZEI_RATE = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                                    }
                                }

                                foreach (T_UR_SH_DETAIL detail in newDto.detailList)
                                {
                                    detail.SEQ = newDto.entry.SEQ;
                                    if (this.headerForm.txtUpdateMode.Text == "1")
                                    {
                                        string key = string.Format("{0}_{1}", detail.SYSTEM_ID.Value.ToString(), detail.DETAIL_SYSTEM_ID.Value.ToString());
                                        var oldTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();

                                        var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                                            (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.TORIHIKISAKI_CD,
                                            newDto.entry.GYOUSHA_CD,
                                            newDto.entry.GENBA_CD,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GENBA_CD,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                            );

                                        // 個別品名単価から情報が取れない場合は基本品名単価の検索
                                        if (kobetsuhinmeiTanka == null)
                                        {
                                            var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                                                (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI,
                                            detail.DENPYOU_KBN_CD.Value,
                                            newDto.entry.UNPAN_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GYOUSHA_CD,
                                            newDto.entry.NIOROSHI_GENBA_CD,
                                            detail.HINMEI_CD,
                                            detail.UNIT_CD.Value,
                                            newDto.entry.DENPYOU_DATE.Value.ToShortDateString()
                                                );
                                            if (kihonHinmeiTanka != null)
                                            {
                                                detail.TANKA = kihonHinmeiTanka.TANKA.Value;
                                            }
                                            else
                                            {
                                                detail.TANKA = SqlDecimal.Null;
                                            }
                                        }
                                        else
                                        {
                                            detail.TANKA = kobetsuhinmeiTanka.TANKA.Value;
                                        }
                                        var newTanka = detail.TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString();
                                        // 単価に変更があった場合のみ再計算
                                        if (!oldTanka.Equals(newTanka))
                                        {
                                            newDto.tankaChanged[key] = true;
                                            if (!this.CalcDetaiKingaku(newDto, detail))
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            newDto.tankaChanged[key] = false;
                                        }
                                    }
                                    var dataBinderDetailResult = new DataBinderLogic<T_UR_SH_DETAIL>(detail);
                                    dataBinderDetailResult.SetSystemProperty(detail, false);
                                }

                                this.ZeiKeisan(newDto);

                                this.tusRegist.Add(newDto);
                            }
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntites", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 受入明細金額計算
        /// <summary>
        /// 受入明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto, T_UKEIRE_DETAIL targetRow)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(dto, targetRow);

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                if (!targetRow.SUURYOU.IsNull)
                {
                    suuryou = targetRow.SUURYOU.Value;
                }
                if (!targetRow.TANKA.IsNull)
                {
                    tanka = targetRow.TANKA.Value;
                }

                // 金額端数取得
                if (!targetRow.DENPYOU_KBN_CD.IsNull)
                {
                    if (targetRow.DENPYOU_KBN_CD.Value == 1)
                    {
                        short.TryParse(Convert.ToString(dto.seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    if (targetRow.DENPYOU_KBN_CD.Value == 2)
                    {
                        short.TryParse(Convert.ToString(dto.shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                }

                if (!targetRow.SUURYOU.IsNull && !targetRow.TANKA.IsNull)
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.KINGAKU = kingaku;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcDetaiKingaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }
        #endregion

        #region 出荷明細金額計算
        /// <summary>
        /// 出荷明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto, T_SHUKKA_DETAIL targetRow)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(dto, targetRow);

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                if (!targetRow.SUURYOU.IsNull)
                {
                    suuryou = targetRow.SUURYOU.Value;
                }
                if (!targetRow.TANKA.IsNull)
                {
                    tanka = targetRow.TANKA.Value;
                }

                // 金額端数取得
                if (!targetRow.DENPYOU_KBN_CD.IsNull)
                {
                    if (targetRow.DENPYOU_KBN_CD.Value == 1)
                    {
                        short.TryParse(Convert.ToString(dto.seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    if (targetRow.DENPYOU_KBN_CD.Value == 2)
                    {
                        short.TryParse(Convert.ToString(dto.shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                }

                if (!targetRow.SUURYOU.IsNull && !targetRow.TANKA.IsNull)
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.KINGAKU = kingaku;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcDetaiKingaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }
        #endregion

        #region 売上支払明細金額計算
        /// <summary>
        /// 売上支払明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto, T_UR_SH_DETAIL targetRow)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(dto, targetRow);

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                if (!targetRow.SUURYOU.IsNull)
                {
                    suuryou = targetRow.SUURYOU.Value;
                }
                if (!targetRow.TANKA.IsNull)
                {
                    tanka = targetRow.TANKA.Value;
                }

                // 金額端数取得
                if (!targetRow.DENPYOU_KBN_CD.IsNull)
                {
                    if (targetRow.DENPYOU_KBN_CD.Value == 1)
                    {
                        short.TryParse(Convert.ToString(dto.seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    if (targetRow.DENPYOU_KBN_CD.Value == 2)
                    {
                        short.TryParse(Convert.ToString(dto.shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                }

                if (!targetRow.SUURYOU.IsNull && !targetRow.TANKA.IsNull)
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.KINGAKU = kingaku;
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CalcDetaiKingaku", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcDetaiKingaku", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;

        }
        #endregion

        #region DB関連処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="kbn"></param>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            var ret = 0;
            try
            {
                DateTime dtpFrom = DateTime.Parse(this.form.HIDUKE_FROM.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.form.HIDUKE_TO.GetResultText());

                this.dto = new SearchDto();
                dto.HidukeFrom = dtpFrom.ToShortDateString();
                dto.HidukeTo = dtpTo.ToShortDateString();
                if (!string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    dto.kyotenCd = Convert.ToInt16(this.form.KYOTEN_CD.Text);
                }
                if (!string.IsNullOrEmpty(this.form.txtKakuteiKbn.Text))
                {
                    dto.kakuteiKbn = Convert.ToInt16(this.form.txtKakuteiKbn.Text);
                }
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    dto.torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    dto.gyoushaCd = this.form.GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    dto.genbaCd = this.form.GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD.Text))
                {
                    dto.upnGyoushaCd = this.form.UPN_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                {
                    dto.nizumiGyoushaCd = this.form.NIZUMI_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                {
                    dto.nizumiGenbaCd = this.form.NIZUMI_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    dto.nioroshiGyoushaCd = this.form.NIOROSHI_GYOUSHA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    dto.nioroshiGenbaCd = this.form.NIOROSHI_GENBA_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.EIGYOUTANTOU_CD.Text))
                {
                    dto.eigyouTantoushaCd = this.form.EIGYOUTANTOU_CD.Text;
                }
                dto.denshuKbnCd = this.form.txtDenshuKbn.Text;

                switch (this.form.txtDenshuKbn.Text)
                {
                    case "1":
                        #region 受入
                        this.tuResult.Clear();

                        T_UKEIRE_ENTRY[] tue = this.accessor.SearchUkeireEntryData(dto);
                        if (tue != null && tue.Length > 0)
                        {
                            int i = 0;
                            foreach (var e in tue)
                            {
                                DateTime getsujiShoriCheckDate = e.DENPYOU_DATE.Value;
                                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                                // 月次処理中チェック
                                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                                {
                                    continue;
                                }
                                // 月次処理ロックチェック
                                else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                                {
                                    continue;
                                }
                                else if (CheckAllShimeStatus(e))
                                {
                                    continue;
                                }

                                T_UKEIRE_DETAIL[] tud = this.accessor.SearchUkeireDetailData(e);

                                ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> result = new ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>();
                                result.denpyouNo = e.UKEIRE_NUMBER.Value;
                                result.rowIndex = i;
                                result.entry = e;

                                foreach (var detail in tud)
                                {
                                    result.detailList.Add(detail);

                                    // 在庫明細
                                    T_ZAIKO_UKEIRE_DETAIL zaikoUkeireDetailEntity = new T_ZAIKO_UKEIRE_DETAIL();
                                    zaikoUkeireDetailEntity.SYSTEM_ID = detail.SYSTEM_ID;
                                    zaikoUkeireDetailEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                                    zaikoUkeireDetailEntity.SEQ = detail.SEQ;

                                    string key = string.Format("{0}_{1}_{2}", detail.SYSTEM_ID.Value.ToString(), detail.DETAIL_SYSTEM_ID.Value.ToString(), detail.SEQ.Value.ToString());

                                    var zaikoUkeireDetails = this.accessor.GetZaikoUkeireDetails(zaikoUkeireDetailEntity);
                                    if (zaikoUkeireDetails != null)
                                    {
                                        result.detailZaikoUkeireDetails[key] = zaikoUkeireDetails;
                                    }

                                    // 在庫品名振分
                                    T_ZAIKO_HINMEI_HURIWAKE zaikoHinmeiHuriwakeEntity = new T_ZAIKO_HINMEI_HURIWAKE();
                                    zaikoHinmeiHuriwakeEntity.SYSTEM_ID = detail.SYSTEM_ID;
                                    zaikoHinmeiHuriwakeEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                                    zaikoHinmeiHuriwakeEntity.SEQ = detail.SEQ;
                                    zaikoHinmeiHuriwakeEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;

                                    var zaikoHinmeiHuriwakes = this.accessor.GetZaikoHinmeiHuriwakes(zaikoHinmeiHuriwakeEntity);
                                    if (zaikoHinmeiHuriwakes != null)
                                    {
                                        result.detailZaikoHinmeiHuriwakes[key] = zaikoHinmeiHuriwakes;
                                    }
                                }
                                var contenaResultEntity = this.accessor.GetContena(result.entry.SYSTEM_ID.Value.ToString(), 1);
                                if (contenaResultEntity != null)
                                {
                                    foreach (T_CONTENA_RESULT entity in contenaResultEntity)
                                    {
                                        result.contenaResults.Add(entity);
                                    }
                                }

                                // 設置台数・引揚台数
                                var contenaReserveEntity = this.accessor.GetContenaReserve(result.entry.SYSTEM_ID.Value.ToString(), result.entry.SEQ.Value.ToString());
                                if (contenaReserveEntity != null)
                                {
                                    foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                                    {
                                        result.contenaReserveList.Add(entity);
                                    }
                                }

                                this.tuResult.Add(result);
                                i++;
                            }
                            ret = this.tuResult.Count;
                        }
                        #endregion
                        break;
                    case "2":
                        #region 出荷
                        this.tsResult.Clear();

                        T_SHUKKA_ENTRY[] tse = this.accessor.SearchShukkaEntryData(dto);
                        if (tse != null && tse.Length > 0)
                        {
                            int i = 0;
                            foreach (var e in tse)
                            {
                                DateTime getsujiShoriCheckDate = e.DENPYOU_DATE.Value;
                                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                                // 月次処理中チェック
                                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                                {
                                    continue;
                                }
                                // 月次処理ロックチェック
                                else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                                {
                                    continue;
                                }
                                else if (CheckAllShimeStatus(e))
                                {
                                    continue;
                                }

                                T_SHUKKA_DETAIL[] tsd = this.accessor.SearchShukkaDetailData(e);

                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> result = new ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>();
                                result.denpyouNo = e.SHUKKA_NUMBER.Value;
                                result.rowIndex = i;
                                result.entry = e;

                                foreach (var detail in tsd)
                                {
                                    result.detailList.Add(detail);

                                    // 在庫明細
                                    T_ZAIKO_SHUKKA_DETAIL zaikoShukkaDetailEntity = new T_ZAIKO_SHUKKA_DETAIL();
                                    zaikoShukkaDetailEntity.SYSTEM_ID = detail.SYSTEM_ID;
                                    zaikoShukkaDetailEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                                    zaikoShukkaDetailEntity.SEQ = detail.SEQ;

                                    string key = string.Format("{0}_{1}_{2}", detail.SYSTEM_ID.Value.ToString(), detail.DETAIL_SYSTEM_ID.Value.ToString(), detail.SEQ.Value.ToString());

                                    var zaikoShukkaDetails = this.accessor.GetZaikoShukkaDetails(zaikoShukkaDetailEntity);
                                    if (zaikoShukkaDetails != null)
                                    {
                                        result.detailZaikoShukkaDetails[key] = zaikoShukkaDetails;
                                    }

                                    // 在庫品名振分
                                    T_ZAIKO_HINMEI_HURIWAKE zaikoHinmeiHuriwakeEntity = new T_ZAIKO_HINMEI_HURIWAKE();
                                    zaikoHinmeiHuriwakeEntity.SYSTEM_ID = detail.SYSTEM_ID;
                                    zaikoHinmeiHuriwakeEntity.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                                    zaikoHinmeiHuriwakeEntity.SEQ = detail.SEQ;
                                    zaikoHinmeiHuriwakeEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

                                    var zaikoHinmeiHuriwakes = this.accessor.GetZaikoHinmeiHuriwakes(zaikoHinmeiHuriwakeEntity);
                                    if (zaikoHinmeiHuriwakes != null)
                                    {
                                        result.detailZaikoHinmeiHuriwakes[key] = zaikoHinmeiHuriwakes;
                                    }

                                    // 検収明細
                                    T_KENSHU_DETAIL KenshuDetail = new T_KENSHU_DETAIL();
                                    KenshuDetail.SYSTEM_ID = detail.SYSTEM_ID;
                                    KenshuDetail.DETAIL_SYSTEM_ID = detail.DETAIL_SYSTEM_ID;
                                    KenshuDetail.SEQ = detail.SEQ;

                                    var KenshuDetailData = this.accessor.GetKenshuDetails(KenshuDetail);
                                    if (KenshuDetailData != null)
                                    {
                                        result.detailKenshuDetails[key] = KenshuDetailData;
                                    }
                                }
                                var contenaResultEntity = this.accessor.GetContena(result.entry.SYSTEM_ID.Value.ToString(), 2);
                                if (contenaResultEntity != null)
                                {
                                    foreach (T_CONTENA_RESULT entity in contenaResultEntity)
                                    {
                                        result.contenaResults.Add(entity);
                                    }
                                }

                                // 設置台数・引揚台数
                                var contenaReserveEntity = this.accessor.GetContenaReserve(result.entry.SYSTEM_ID.Value.ToString(), result.entry.SEQ.Value.ToString());
                                if (contenaReserveEntity != null)
                                {
                                    foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                                    {
                                        result.contenaReserveList.Add(entity);
                                    }
                                }

                                this.tsResult.Add(result);
                                i++;
                            }
                            ret = this.tsResult.Count;
                        }
                        #endregion
                        break;
                    case "3":
                        #region 売上支払
                        this.tusResult.Clear();

                        T_UR_SH_ENTRY[] tuse = this.accessor.SearchUrshEntryData(dto);
                        if (tuse != null && tuse.Length > 0)
                        {
                            int i = 0;
                            foreach (var e in tuse)
                            {
                                DateTime getsujiShoriCheckDate = e.DENPYOU_DATE.Value;
                                GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
                                // 月次処理中チェック
                                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                                {
                                    continue;
                                }
                                // 月次処理ロックチェック
                                else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                                {
                                    continue;
                                }
                                else if (CheckAllShimeStatus(e))
                                {
                                    continue;
                                }

                                T_UR_SH_DETAIL[] tusd = this.accessor.SearchUrshDetailData(e);

                                ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> result = new ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL>();
                                result.denpyouNo = e.UR_SH_NUMBER.Value;
                                result.rowIndex = i;
                                result.entry = e;

                                foreach (var detail in tusd)
                                {
                                    result.detailList.Add(detail);
                                }

                                var contenaResultEntity = this.accessor.GetContena(result.entry.SYSTEM_ID.Value.ToString(), 3);
                                if (contenaResultEntity != null)
                                {
                                    foreach (T_CONTENA_RESULT entity in contenaResultEntity)
                                    {
                                        result.contenaResults.Add(entity);
                                    }
                                }

                                // 設置台数・引揚台数
                                var contenaReserveEntity = this.accessor.GetContenaReserve(result.entry.SYSTEM_ID.Value.ToString(), result.entry.SEQ.Value.ToString());
                                if (contenaReserveEntity != null)
                                {
                                    foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
                                    {
                                        result.contenaReserveList.Add(entity);
                                    }
                                }

                                this.tusResult.Add(result);
                                i++;
                            }
                            ret = this.tusResult.Count;
                        }
                        #endregion
                        break;
                }
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("Search", ex);
                this.form.MsgLogic.MessageBoxShow("E093");
                throw;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            try
            {
                using (Transaction tran = new Transaction())
                {
                    DataTable csvDt = new DataTable();
                    csvDt.Columns.Add("データ区分");
                    csvDt.Columns.Add("伝票番号");
                    csvDt.Columns.Add("拠点CD");
                    csvDt.Columns.Add("拠点名");
                    csvDt.Columns.Add("確定区分");
                    csvDt.Columns.Add("伝票日付");
                    csvDt.Columns.Add("売上日付");
                    csvDt.Columns.Add("支払日付");
                    csvDt.Columns.Add("取引先CD");
                    csvDt.Columns.Add("取引先名");
                    csvDt.Columns.Add("業者CD");
                    csvDt.Columns.Add("業者名");
                    csvDt.Columns.Add("現場CD");
                    csvDt.Columns.Add("現場名");
                    csvDt.Columns.Add("荷積業者CD");
                    csvDt.Columns.Add("荷積業者名");
                    csvDt.Columns.Add("荷積現場CD");
                    csvDt.Columns.Add("荷積現場名");
                    csvDt.Columns.Add("荷降業者CD");
                    csvDt.Columns.Add("荷降業者名");
                    csvDt.Columns.Add("荷降現場CD");
                    csvDt.Columns.Add("荷降現場名");
                    csvDt.Columns.Add("営業担当者CD");
                    csvDt.Columns.Add("営業担担当者");
                    csvDt.Columns.Add("入力担当者");
                    csvDt.Columns.Add("車輌CD");
                    csvDt.Columns.Add("車輌名");
                    csvDt.Columns.Add("車種CD");
                    csvDt.Columns.Add("車種名");
                    csvDt.Columns.Add("運搬業者CD");
                    csvDt.Columns.Add("運搬業者名");
                    csvDt.Columns.Add("運搬者CD");
                    csvDt.Columns.Add("運転者名");
                    csvDt.Columns.Add("形態区分CD");
                    csvDt.Columns.Add("形態区分");
                    csvDt.Columns.Add("台貫区分");
                    csvDt.Columns.Add("台貫");
                    csvDt.Columns.Add("マニ種類");
                    csvDt.Columns.Add("マニ種類名");
                    csvDt.Columns.Add("マニ手配");
                    csvDt.Columns.Add("マニ手配名");
                    csvDt.Columns.Add("伝票備考");
                    csvDt.Columns.Add("滞留備考");
                    csvDt.Columns.Add("単価マスタ更新");

                    switch (this.dto.denshuKbnCd)
                    {
                        case "1":
                            #region 受入
                            foreach (var dto in this.tuRegist)
                            {
                                ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> beforeDto = this.tuResult.Where(x => x.denpyouNo == dto.denpyouNo).ToList()[0];
                                this.accessor.UpdateUkeireEntryData(beforeDto.entry);
                                this.accessor.InsertUkeireEntryData(dto.entry);
                                foreach (var detail in dto.detailList)
                                {
                                    this.accessor.InsertUkeireDetailData(detail);
                                }

                                // コンテナ稼動予約の台数計算フラグを更新
                                this.accessor.UpdateContenaReserve(dto.contenaReserveList);
                                this.accessor.InsertContenaResult(dto.contenaResults);
                                this.accessor.UpdateContenaResult(beforeDto.contenaResults);

                                // 在庫系の更新
                                // 在庫管理の場合のみ設定する
                                if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
                                {
                                    //Dictionary関連修正
                                    this.accessor.InsertZaikoUkeireDetails(dto.detailZaikoUkeireDetails);
                                    this.accessor.UpdateZaikoUkeireDetails(beforeDto.detailZaikoUkeireDetails);

                                    this.accessor.InsertZaikoHinmeiHuriwakes(dto.detailZaikoHinmeiHuriwakes);
                                    this.accessor.UpdateZaikoHinmeiHuriwakes(beforeDto.detailZaikoHinmeiHuriwakes);
                                }

                                // 入力内容に基づいてコンテナマスタの更新
                                if (dto.contenaResults.Count > 0)
                                {
                                    dto.contenaMasterList = new List<M_CONTENA>();
                                    UpdateContenaMaster(dto);
                                    if (dto.contenaMasterList.Count > 0)
                                    {
                                        this.accessor.UpdateContenaMaster(dto.contenaMasterList);
                                    }
                                }
                                if (this.headerForm.txtCsvOutputKbn.Text == "1")
                                {
                                    bool tankaChanged = dto.tankaChanged.ContainsValue(true);

                                    var dr = csvDt.NewRow();
                                    dr["データ区分"] = "前";
                                    dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                    dr["拠点CD"] = beforeDto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(beforeDto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(beforeDto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = beforeDto.entry.URIAGE_DATE.IsNull ? "" : beforeDto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = beforeDto.entry.SHIHARAI_DATE.IsNull ? "" : beforeDto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                    dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                    dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = string.Empty;
                                    dr["荷積業者名"] = string.Empty;
                                    dr["荷積現場CD"] = string.Empty;
                                    dr["荷積現場名"] = string.Empty;
                                    dr["荷降業者CD"] = beforeDto.entry.NIOROSHI_GYOUSHA_CD;
                                    dr["荷降業者名"] = beforeDto.entry.NIOROSHI_GYOUSHA_NAME;
                                    dr["荷降現場CD"] = beforeDto.entry.NIOROSHI_GENBA_CD;
                                    dr["荷降現場名"] = beforeDto.entry.NIOROSHI_GENBA_NAME;
                                    dr["営業担当者CD"] = beforeDto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = beforeDto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = beforeDto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = beforeDto.entry.SHARYOU_CD;
                                    dr["車輌名"] = beforeDto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = beforeDto.entry.SHASHU_CD;
                                    dr["車種名"] = beforeDto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = beforeDto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = beforeDto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = beforeDto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = beforeDto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = beforeDto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : beforeDto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(beforeDto.entry.KEITAI_KBN_CD.IsNull ? "" : beforeDto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = beforeDto.entry.DAIKAN_KBN.IsNull ? "" : beforeDto.entry.DAIKAN_KBN.Value.ToString();
                                    dr["台貫"] = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(beforeDto.entry.DAIKAN_KBN.IsNull ? "" : beforeDto.entry.DAIKAN_KBN.Value.ToString()));
                                    dr["マニ種類"] = beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = beforeDto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = beforeDto.entry.TAIRYUU_BIKOU;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);

                                    dr = csvDt.NewRow();
                                    dr["データ区分"] = "後";
                                    dr["伝票番号"] = dto.denpyouNo.ToString();
                                    dr["拠点CD"] = dto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(dto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(dto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = dto.entry.URIAGE_DATE.IsNull ? "" : dto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = dto.entry.SHIHARAI_DATE.IsNull ? "" : dto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                    dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = dto.entry.GENBA_CD;
                                    dr["現場名"] = dto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = string.Empty;
                                    dr["荷積業者名"] = string.Empty;
                                    dr["荷積現場CD"] = string.Empty;
                                    dr["荷積現場名"] = string.Empty;
                                    dr["荷降業者CD"] = dto.entry.NIOROSHI_GYOUSHA_CD;
                                    dr["荷降業者名"] = dto.entry.NIOROSHI_GYOUSHA_NAME;
                                    dr["荷降現場CD"] = dto.entry.NIOROSHI_GENBA_CD;
                                    dr["荷降現場名"] = dto.entry.NIOROSHI_GENBA_NAME;
                                    dr["営業担当者CD"] = dto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = dto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = dto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = dto.entry.SHARYOU_CD;
                                    dr["車輌名"] = dto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = dto.entry.SHASHU_CD;
                                    dr["車種名"] = dto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = dto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = dto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = dto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = dto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = dto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : dto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(dto.entry.KEITAI_KBN_CD.IsNull ? "" : dto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = dto.entry.DAIKAN_KBN.IsNull ? "" : dto.entry.DAIKAN_KBN.Value.ToString();
                                    dr["台貫"] = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(dto.entry.DAIKAN_KBN.IsNull ? "" : dto.entry.DAIKAN_KBN.Value.ToString()));
                                    dr["マニ種類"] = dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = dto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = dto.entry.TAIRYUU_BIKOU;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);
                                }
                            }
                            foreach (string key in this.dayDic.Keys)
                            {
                                if (this.dayDic[key] != null && this.dayDic[key].Length > 0)
                                {
                                    if (this.dayDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberDay(this.dayDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberDay(this.dayDic[key][0]);
                                    }
                                }
                            }
                            foreach (string key in this.yearDic.Keys)
                            {
                                if (this.yearDic[key] != null && this.yearDic[key].Length > 0)
                                {
                                    if (this.yearDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberYear(this.yearDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberYear(this.yearDic[key][0]);
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "2":
                            #region 出荷
                            foreach (var dto in this.tsRegist)
                            {
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> beforeDto = this.tsResult.Where(x => x.denpyouNo == dto.denpyouNo).ToList()[0];
                                this.accessor.UpdateShukkaEntryData(beforeDto.entry);
                                this.accessor.InsertShukkaEntryData(dto.entry);
                                foreach (var detail in dto.detailList)
                                {
                                    this.accessor.InsertShukkaDetailData(detail);
                                }

                                // 在庫系の更新
                                // 在庫管理の場合のみ設定する
                                if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
                                {
                                    //Dictionary関連修正
                                    this.accessor.InsertZaikoShukkaDetails(dto.detailZaikoShukkaDetails);
                                    this.accessor.UpdateZaikoShukkaDetails(beforeDto.detailZaikoShukkaDetails);

                                    this.accessor.InsertZaikoHinmeiHuriwakes(dto.detailZaikoHinmeiHuriwakes);
                                    this.accessor.UpdateZaikoHinmeiHuriwakes(beforeDto.detailZaikoHinmeiHuriwakes);
                                }

                                // 検収明細
                                this.accessor.InsertKenshuDetail(dto.detailKenshuDetails);
                                this.accessor.UpdateKenshuDetail(beforeDto.detailKenshuDetails);

                                if (this.headerForm.txtCsvOutputKbn.Text == "1")
                                {
                                    bool tankaChanged = dto.tankaChanged.ContainsValue(true);

                                    var dr = csvDt.NewRow();
                                    dr["データ区分"] = "前";
                                    dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                    dr["拠点CD"] = beforeDto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(beforeDto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(beforeDto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = beforeDto.entry.URIAGE_DATE.IsNull ? "" : beforeDto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = beforeDto.entry.SHIHARAI_DATE.IsNull ? "" : beforeDto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                    dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                    dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = beforeDto.entry.NIZUMI_GYOUSHA_CD;
                                    dr["荷積業者名"] = beforeDto.entry.NIZUMI_GYOUSHA_NAME;
                                    dr["荷積現場CD"] = beforeDto.entry.NIZUMI_GENBA_CD;
                                    dr["荷積現場名"] = beforeDto.entry.NIZUMI_GENBA_NAME;
                                    dr["荷降業者CD"] = string.Empty;
                                    dr["荷降業者名"] = string.Empty;
                                    dr["荷降現場CD"] = string.Empty;
                                    dr["荷降現場名"] = string.Empty;
                                    dr["営業担当者CD"] = beforeDto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = beforeDto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = beforeDto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = beforeDto.entry.SHARYOU_CD;
                                    dr["車輌名"] = beforeDto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = beforeDto.entry.SHASHU_CD;
                                    dr["車種名"] = beforeDto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = beforeDto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = beforeDto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = beforeDto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = beforeDto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = beforeDto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : beforeDto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(beforeDto.entry.KEITAI_KBN_CD.IsNull ? "" : beforeDto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = beforeDto.entry.DAIKAN_KBN.IsNull ? "" : beforeDto.entry.DAIKAN_KBN.Value.ToString();
                                    dr["台貫"] = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(beforeDto.entry.DAIKAN_KBN.IsNull ? "" : beforeDto.entry.DAIKAN_KBN.Value.ToString()));
                                    dr["マニ種類"] = beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = beforeDto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = beforeDto.entry.TAIRYUU_BIKOU;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);

                                    dr = csvDt.NewRow();
                                    dr["データ区分"] = "後";
                                    dr["伝票番号"] = dto.denpyouNo.ToString();
                                    dr["拠点CD"] = dto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(dto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(dto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = dto.entry.URIAGE_DATE.IsNull ? "" : dto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = dto.entry.SHIHARAI_DATE.IsNull ? "" : dto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                    dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = dto.entry.GENBA_CD;
                                    dr["現場名"] = dto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = dto.entry.NIZUMI_GYOUSHA_CD;
                                    dr["荷積業者名"] = dto.entry.NIZUMI_GYOUSHA_NAME;
                                    dr["荷積現場CD"] = dto.entry.NIZUMI_GENBA_CD;
                                    dr["荷積現場名"] = dto.entry.NIZUMI_GENBA_NAME;
                                    dr["荷降業者CD"] = string.Empty;
                                    dr["荷降業者名"] = string.Empty;
                                    dr["荷降現場CD"] = string.Empty;
                                    dr["荷降現場名"] = string.Empty;
                                    dr["営業担当者CD"] = dto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = dto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = dto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = dto.entry.SHARYOU_CD;
                                    dr["車輌名"] = dto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = dto.entry.SHASHU_CD;
                                    dr["車種名"] = dto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = dto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = dto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = dto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = dto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = dto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : dto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(dto.entry.KEITAI_KBN_CD.IsNull ? "" : dto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = dto.entry.DAIKAN_KBN.IsNull ? "" : dto.entry.DAIKAN_KBN.Value.ToString();
                                    dr["台貫"] = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(dto.entry.DAIKAN_KBN.IsNull ? "" : dto.entry.DAIKAN_KBN.Value.ToString()));
                                    dr["マニ種類"] = dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = dto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = dto.entry.TAIRYUU_BIKOU;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);
                                }
                            }
                            foreach (string key in this.dayDic.Keys)
                            {
                                if (this.dayDic[key] != null && this.dayDic[key].Length > 0)
                                {
                                    if (this.dayDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberDay(this.dayDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberDay(this.dayDic[key][0]);
                                    }
                                }
                            }
                            foreach (string key in this.yearDic.Keys)
                            {
                                if (this.yearDic[key] != null && this.yearDic[key].Length > 0)
                                {
                                    if (this.yearDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberYear(this.yearDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberYear(this.yearDic[key][0]);
                                    }
                                }
                            }
                            #endregion
                            break;
                        case "3":
                            #region 売上支払
                            foreach (var dto in this.tusRegist)
                            {
                                ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> beforeDto = this.tusResult.Where(x => x.denpyouNo == dto.denpyouNo).ToList()[0];
                                this.accessor.UpdateUrshEntryData(beforeDto.entry);
                                this.accessor.InsertUrshEntryData(dto.entry);
                                foreach (var detail in dto.detailList)
                                {
                                    this.accessor.InsertUrshDetailData(detail);
                                }

                                // コンテナ稼動予約の台数計算フラグを更新
                                this.accessor.UpdateContenaReserve(dto.contenaReserveList);
                                this.accessor.InsertContenaResult(dto.contenaResults);
                                this.accessor.UpdateContenaResult(beforeDto.contenaResults);

                                // 入力内容に基づいてコンテナマスタの更新
                                if (dto.contenaResults.Count > 0)
                                {
                                    dto.contenaMasterList = new List<M_CONTENA>();
                                    UpdateContenaMaster(dto);
                                    if (dto.contenaMasterList.Count > 0)
                                    {
                                        this.accessor.UpdateContenaMaster(dto.contenaMasterList);
                                    }
                                }
                                if (this.headerForm.txtCsvOutputKbn.Text == "1")
                                {
                                    bool tankaChanged = dto.tankaChanged.ContainsValue(true);

                                    var dr = csvDt.NewRow();
                                    dr["データ区分"] = "前";
                                    dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                    dr["拠点CD"] = beforeDto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(beforeDto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(beforeDto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = beforeDto.entry.URIAGE_DATE.IsNull ? "" : beforeDto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = beforeDto.entry.SHIHARAI_DATE.IsNull ? "" : beforeDto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                    dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                    dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = beforeDto.entry.NIZUMI_GYOUSHA_CD;
                                    dr["荷積業者名"] = beforeDto.entry.NIZUMI_GYOUSHA_NAME;
                                    dr["荷積現場CD"] = beforeDto.entry.NIZUMI_GENBA_CD;
                                    dr["荷積現場名"] = beforeDto.entry.NIZUMI_GENBA_NAME;
                                    dr["荷降業者CD"] = beforeDto.entry.NIOROSHI_GYOUSHA_CD;
                                    dr["荷降業者名"] = beforeDto.entry.NIOROSHI_GYOUSHA_NAME;
                                    dr["荷降現場CD"] = beforeDto.entry.NIOROSHI_GENBA_CD;
                                    dr["荷降現場名"] = beforeDto.entry.NIOROSHI_GENBA_NAME;
                                    dr["営業担当者CD"] = beforeDto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = beforeDto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = beforeDto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = beforeDto.entry.SHARYOU_CD;
                                    dr["車輌名"] = beforeDto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = beforeDto.entry.SHASHU_CD;
                                    dr["車種名"] = beforeDto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = beforeDto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = beforeDto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = beforeDto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = beforeDto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = beforeDto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : beforeDto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(beforeDto.entry.KEITAI_KBN_CD.IsNull ? "" : beforeDto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = string.Empty;
                                    dr["台貫"] = string.Empty;
                                    dr["マニ種類"] = beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(beforeDto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(beforeDto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : beforeDto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = beforeDto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = string.Empty;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);

                                    dr = csvDt.NewRow();
                                    dr["データ区分"] = "後";
                                    dr["伝票番号"] = dto.denpyouNo.ToString();
                                    dr["拠点CD"] = dto.entry.KYOTEN_CD.Value.ToString();
                                    dr["拠点名"] = this.accessor.GetKyotenNameFast(dto.entry.KYOTEN_CD.Value.ToString());
                                    dr["確定区分"] = SalesPaymentConstans.GetKakuteiKbnName(dto.entry.KAKUTEI_KBN.Value);
                                    dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                    dr["売上日付"] = dto.entry.URIAGE_DATE.IsNull ? "" : dto.entry.URIAGE_DATE.Value.ToShortDateString();
                                    dr["支払日付"] = dto.entry.SHIHARAI_DATE.IsNull ? "" : dto.entry.SHIHARAI_DATE.Value.ToShortDateString();
                                    dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                    dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                    dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                    dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                    dr["現場CD"] = dto.entry.GENBA_CD;
                                    dr["現場名"] = dto.entry.GENBA_NAME;
                                    dr["荷積業者CD"] = dto.entry.NIZUMI_GYOUSHA_CD;
                                    dr["荷積業者名"] = dto.entry.NIZUMI_GYOUSHA_NAME;
                                    dr["荷積現場CD"] = dto.entry.NIZUMI_GENBA_CD;
                                    dr["荷積現場名"] = dto.entry.NIZUMI_GENBA_NAME;
                                    dr["荷降業者CD"] = dto.entry.NIOROSHI_GYOUSHA_CD;
                                    dr["荷降業者名"] = dto.entry.NIOROSHI_GYOUSHA_NAME;
                                    dr["荷降現場CD"] = dto.entry.NIOROSHI_GENBA_CD;
                                    dr["荷降現場名"] = dto.entry.NIOROSHI_GENBA_NAME;
                                    dr["営業担当者CD"] = dto.entry.EIGYOU_TANTOUSHA_CD;
                                    dr["営業担担当者"] = dto.entry.EIGYOU_TANTOUSHA_NAME;
                                    dr["入力担当者"] = dto.entry.NYUURYOKU_TANTOUSHA_NAME;
                                    dr["車輌CD"] = dto.entry.SHARYOU_CD;
                                    dr["車輌名"] = dto.entry.SHARYOU_NAME;
                                    dr["車種CD"] = dto.entry.SHASHU_CD;
                                    dr["車種名"] = dto.entry.SHASHU_NAME;
                                    dr["運搬業者CD"] = dto.entry.UNPAN_GYOUSHA_CD;
                                    dr["運搬業者名"] = dto.entry.UNPAN_GYOUSHA_NAME;
                                    dr["運搬者CD"] = dto.entry.UNTENSHA_CD;
                                    dr["運転者名"] = dto.entry.UNTENSHA_NAME;
                                    dr["形態区分CD"] = dto.entry.KEITAI_KBN_CD.IsNull ? string.Empty : dto.entry.KEITAI_KBN_CD.Value.ToString();
                                    dr["形態区分"] = this.accessor.GetKeitaiNameFast(dto.entry.KEITAI_KBN_CD.IsNull ? "" : dto.entry.KEITAI_KBN_CD.Value.ToString());
                                    dr["台貫区分"] = string.Empty;
                                    dr["台貫"] = string.Empty;
                                    dr["マニ種類"] = dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString();
                                    dr["マニ種類名"] = this.accessor.GetManiShuruiNameFast(dto.entry.MANIFEST_SHURUI_CD.IsNull ? "" : dto.entry.MANIFEST_SHURUI_CD.Value.ToString());
                                    dr["マニ手配"] = dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString();
                                    dr["マニ手配名"] = this.accessor.GetManiTehaiNameFast(dto.entry.MANIFEST_TEHAI_CD.IsNull ? "" : dto.entry.MANIFEST_TEHAI_CD.Value.ToString());
                                    dr["伝票備考"] = dto.entry.DENPYOU_BIKOU;
                                    dr["滞留備考"] = string.Empty;
                                    dr["単価マスタ更新"] = tankaChanged ? "有" : "無";
                                    csvDt.Rows.Add(dr);
                                }
                            }
                            foreach (string key in this.dayDic.Keys)
                            {
                                if (this.dayDic[key] != null && this.dayDic[key].Length > 0)
                                {
                                    if (this.dayDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberDay(this.dayDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberDay(this.dayDic[key][0]);
                                    }
                                }
                            }
                            foreach (string key in this.yearDic.Keys)
                            {
                                if (this.yearDic[key] != null && this.yearDic[key].Length > 0)
                                {
                                    if (this.yearDic[key][0].TIME_STAMP == null)
                                    {
                                        this.accessor.InsertNumberYear(this.yearDic[key][0]);
                                    }
                                    else
                                    {
                                        this.accessor.UpdateNumberYear(this.yearDic[key][0]);
                                    }
                                }
                            }
                            #endregion
                            break;
                    }
                    tran.Commit();

                    // 完了メッセージ表示
                    this.errmessage.MessageBoxShow("I001", "更新");
                    this.form.mrDetail.Rows.Clear();
                    if (this.headerForm.txtCsvOutputKbn.Text == "1")
                    {
                        if (new Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO.CsvUtility(csvDt, form, "伝票更新結果", outputHeader: true).Output(this.csvPath))
                        {
                            MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);

                if (ex is NotSingleRowUpdatedRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    this.errmessage.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(errorFlag);
            }
        }

        /// <summary>
        /// 受入コンテナマスタ更新
        /// </summary>
        internal void UpdateContenaMaster(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto)
        {
            LogUtility.DebugMethodStart(dto);

            foreach (T_CONTENA_RESULT contenaRes in dto.contenaResults)
            {

                M_CONTENA contenaMtr = this.accessor.GetContenaMaster(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                if (contenaMtr != null)
                {
                    // 設置日、引揚日をチェック
                    if ((!contenaMtr.SECCHI_DATE.IsNull
                        && contenaMtr.SECCHI_DATE.Value.Date > dto.entry.DENPYOU_DATE.Value.Date)
                        || (!contenaMtr.HIKIAGE_DATE.IsNull
                        && contenaMtr.HIKIAGE_DATE.Value.Date > dto.entry.DENPYOU_DATE.Value.Date))
                    {
                        // 設置日、引揚日が作業日より新しい場合は何もしない。
                        continue;
                    }

                    // 画面の入力内容をコンテナマスタに反映させる
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        contenaMtr.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        contenaMtr.GENBA_CD = this.form.GENBA_CD.Text;
                    }
                    contenaMtr.SHARYOU_CD = string.Empty;
                    if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        // 設置の場合
                        if (!dto.entry.DENPYOU_DATE.IsNull)
                        {
                            contenaMtr.SECCHI_DATE = dto.entry.DENPYOU_DATE.Value;
                            contenaMtr.HIKIAGE_DATE = SqlDateTime.Null;
                        }
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_SECCHI;
                    }
                    else if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        // 引揚の場合
                        contenaMtr.HIKIAGE_DATE = dto.entry.DENPYOU_DATE.Value;
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_HIKIAGE;
                    }
                    // 自動設定項目
                    string createUser = contenaMtr.CREATE_USER;
                    SqlDateTime createDate = contenaMtr.CREATE_DATE;
                    string createPC = contenaMtr.CREATE_PC;
                    var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMtr);
                    dataBinderUkeireEntry.SetSystemProperty(contenaMtr, false);
                    // Create情報は前の状態を引き継ぐ
                    contenaMtr.CREATE_USER = createUser;
                    contenaMtr.CREATE_DATE = createDate;
                    contenaMtr.CREATE_PC = createPC;
                    // 最終更新者
                    contenaMtr.UPDATE_USER = dto.entry.NYUURYOKU_TANTOUSHA_NAME;

                    dto.contenaMasterList.Add(contenaMtr);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 売上支払コンテナマスタ更新
        /// </summary>
        internal void UpdateContenaMaster(ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto)
        {
            LogUtility.DebugMethodStart(dto);

            foreach (T_CONTENA_RESULT contenaRes in dto.contenaResults)
            {

                M_CONTENA contenaMtr = this.accessor.GetContenaMaster(contenaRes.CONTENA_SHURUI_CD, contenaRes.CONTENA_CD);
                if (contenaMtr != null)
                {
                    // 設置日、引揚日をチェック
                    if ((!contenaMtr.SECCHI_DATE.IsNull
                        && contenaMtr.SECCHI_DATE.Value.Date > dto.entry.DENPYOU_DATE.Value.Date)
                        || (!contenaMtr.HIKIAGE_DATE.IsNull
                        && contenaMtr.HIKIAGE_DATE.Value.Date > dto.entry.DENPYOU_DATE.Value.Date))
                    {
                        // 設置日、引揚日が作業日より新しい場合は何もしない。
                        continue;
                    }

                    // 画面の入力内容をコンテナマスタに反映させる
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        contenaMtr.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    }
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        contenaMtr.GENBA_CD = this.form.GENBA_CD.Text;
                    }
                    contenaMtr.SHARYOU_CD = string.Empty;
                    if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        // 設置の場合
                        if (!dto.entry.DENPYOU_DATE.IsNull)
                        {
                            contenaMtr.SECCHI_DATE = dto.entry.DENPYOU_DATE.Value;
                            contenaMtr.HIKIAGE_DATE = SqlDateTime.Null;
                        }
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_SECCHI;
                    }
                    else if (contenaRes.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        // 引揚の場合
                        contenaMtr.HIKIAGE_DATE = dto.entry.DENPYOU_DATE.Value;
                        contenaMtr.JOUKYOU_KBN = ConstCls.CONTENA_JOUKYOU_KBN_HIKIAGE;
                    }
                    // 自動設定項目
                    string createUser = contenaMtr.CREATE_USER;
                    SqlDateTime createDate = contenaMtr.CREATE_DATE;
                    string createPC = contenaMtr.CREATE_PC;
                    var dataBinderUkeireEntry = new DataBinderLogic<M_CONTENA>(contenaMtr);
                    dataBinderUkeireEntry.SetSystemProperty(contenaMtr, false);
                    // Create情報は前の状態を引き継ぐ
                    contenaMtr.CREATE_USER = createUser;
                    contenaMtr.CREATE_DATE = createDate;
                    contenaMtr.CREATE_PC = createPC;
                    // 最終更新者
                    contenaMtr.UPDATE_USER = dto.entry.NYUURYOKU_TANTOUSHA_NAME;

                    dto.contenaMasterList.Add(contenaMtr);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 修正処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.form.MsgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.form.MsgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.form.MsgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // コミット
                    tran.Commit();
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.MsgLogic.MessageBoxShow("E080");
                // スローしない
            }
            catch (SQLRuntimeException ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.MsgLogic.MessageBoxShow("E093");
                throw;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns></returns>
        internal bool RegistWrap(bool errorFlag = false)
        {
            // WARN: 変更しないように。
            //       業務ロジックをRegist(bool)に書いてください。
            //       Do NOT modify any source here.
            //       Write logic process in Regist(bool).
            var ret = true;
            try
            {
                this.Regist(errorFlag);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errorFlag"></param>
        /// <returns></returns>
        internal bool UpdateWrap(bool errorFlag = false)
        {
            // WARN: 変更しないように。
            //       業務ロジックをUpdate(bool)に書いてください。
            //       Do NOT modify any source here.
            //       Write logic process in Update(bool).
            var ret = true;
            try
            {
                this.Update(errorFlag);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal bool DeleteWrap(bool errorFlag = false, bool logical = true)
        {
            // WARN: 変更しないように。
            //       業務ロジックをLogicalDelete()又はPhysicalDelete()に書いてください。
            //       Do NOT modify any source here.
            //       Write logic process in LogicalDelete() or PhysicalDelete().
            var ret = true;
            try
            {
                if (logical)
                    this.LogicalDelete();
                else
                    this.PhysicalDelete();
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 行カウント

        /// <summary>
        /// 行カウント
        /// </summary>
        internal int GetRowCount()
        {
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                return this.form.mrDetail.Rows.Count - 1;
            else
                return this.form.mrDetail.Rows.Count;
        }

        #endregion

        #region チェック

        #region マスタ存在チェック
        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var torihikiSaki = this.accessor.GetTorihikisakiDataByCode(this.form.TORIHIKISAKI_CD.Text);
                if (torihikiSaki == null || torihikiSaki.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "取引先");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikiSaki.TORIHIKISAKI_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckTorihikisaki", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisaki", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                var genba = this.accessor.GetGenbaDataByCode(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                if (genba == null || genba.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var gyoushaEntity = this.accessor.GetGyoushaDataByCode(this.form.GYOUSHA_CD.Text);
                if (gyoushaEntity == null || gyoushaEntity.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.UPN_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.UPN_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var gyousha = this.accessor.GetGyoushaDataByCode(this.form.UPN_GYOUSHA_CD.Text);
                if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "運搬業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }


                // 運搬業者区分チェック
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.form.UPN_GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "運搬業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckUnpanGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var gyousha = this.accessor.GetGyoushaDataByCode(this.form.NIOROSHI_GYOUSHA_CD.Text);
                if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "荷降業者");
                    this.form.NIOROSHI_GYOUSHA_CD.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }


                // 荷卸業者区分チェック
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue || gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                {
                    this.form.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷降業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 荷降場チェック
        /// </summary>
        internal bool CheckNioroshiGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E051", "荷降業者");
                    this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA genba = new M_GENBA();

                genba = this.accessor.GetGenbaDataByCode(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);

                // 荷降場チェック
                if (genba == null || genba.DELETE_FLG.IsTrue)
                {
                    // 一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷降現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 荷卸現場区分チェック
                if (genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                {
                    this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷降現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckNioroshiGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 荷積業者チェック
        /// </summary>
        internal bool CheckNizumiGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.NIZUMI_GYOUSHA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var gyousha = this.accessor.GetGyoushaDataByCode(this.form.NIZUMI_GYOUSHA_CD.Text);
                if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
                {
                    //エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "荷積業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }


                // 排出事業者区分、荷積み現場区分、運搬受託者区分チェック
                if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.form.NIZUMI_GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷積業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGyousha", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyousha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 荷積現場チェック
        /// </summary>
        internal bool CheckNizumiGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //初期化
                this.form.NIZUMI_GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                if (string.IsNullOrEmpty(this.form.NIZUMI_GYOUSHA_CD.Text))
                {
                    //エラーメッセージ
                    this.errmessage.MessageBoxShow("E051", "荷積業者");
                    this.form.NIZUMI_GENBA_CD.Text = string.Empty;
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA genba = new M_GENBA();

                genba = this.accessor.GetGenbaDataByCode(this.form.NIZUMI_GYOUSHA_CD.Text, this.form.NIZUMI_GENBA_CD.Text);

                //荷積現場をチェック
                if (genba == null || genba.DELETE_FLG.IsTrue)
                {
                    //一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷積現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 排出事業場区分、積み替え保管区分、荷卸現場区分チェック
                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                {
                    this.form.NIZUMI_GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                else
                {
                    //一致するデータがないのでエラー
                    this.errmessage.MessageBoxShow("E020", "荷積現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckNizumiGenba", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenba", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal bool CheckEigyoutantousha()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.EIGYOUTANTOU_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.EIGYOUTANTOU_CD.Text))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                var hinmei = this.accessor.GetShainDataByCode(this.form.EIGYOUTANTOU_CD.Text);
                if (hinmei == null || hinmei.DELETE_FLG.IsTrue || !hinmei.EIGYOU_TANTOU_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.errmessage.MessageBoxShow("E020", "営業担当者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                else
                {
                    this.form.EIGYOUTANTOU_NAME_RYAKU.Text = hinmei.SHAIN_NAME_RYAKU;
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckHinmei", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }
        #endregion

        #region 権限チェック
        /// <summary>
        /// 権限チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckAuth(string kbn)
        {
            LogUtility.DebugMethodStart(kbn);

            bool ret = true;
            try
            {
                switch (kbn)
                {
                    case "":
                        if (!AuthManager.CheckAuthority("G051", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                            && !AuthManager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false)
                            && !AuthManager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限無い場合はアラートを表示して処理中断
                            this.form.MsgLogic.MessageBoxShow("E158", "修正");
                            ret = false;
                        }
                        break;
                    case "1":
                        if (!AuthManager.CheckAuthority("G051", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限無い場合はアラートを表示して処理中断
                            this.form.MsgLogic.MessageBoxShow("E158", "修正");
                            ret = false;
                        }
                        break;
                    case "2":
                        if (!AuthManager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限無い場合はアラートを表示して処理中断
                            this.form.MsgLogic.MessageBoxShow("E158", "修正");
                            ret = false;
                        }
                        break;
                    case "3":
                        if (!AuthManager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限無い場合はアラートを表示して処理中断
                            this.form.MsgLogic.MessageBoxShow("E158", "修正");
                            ret = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckAuth", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret;
        }
        #endregion

        #region 車輌休動チェック
        internal bool SharyouDateCheck(string inputUnpanGyoushaCd, string inputSharyouCd, string inputSagyouDate, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_SHARYOU[] workclosedsharyouList = this.accessor.GetAllValidSharyouClosedData(inputUnpanGyoushaCd, inputSharyouCd, inputSagyouDate);

                //取得テータ
                if (workclosedsharyouList.Count() >= 1)
                {
                    this.errmessage.MessageBoxShow("E206", "車輌", "伝票日付：" + Convert.ToDateTime(inputSagyouDate).ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SharyouDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 運転者休動チェック
        internal bool UntenshaDateCheck(string inputUntenshaCd, string inputSagyouDate, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_UNTENSHA[] workcloseduntenshaList = this.accessor.GetAllValidUntenshaClosedData(inputUntenshaCd, inputSagyouDate);

                //取得テータ
                if (workcloseduntenshaList.Count() >= 1)
                {
                    errmessage.MessageBoxShow("E206", "運転者", "伝票日付：" + Convert.ToDateTime(inputSagyouDate).ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UntenshaDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 搬入先休動チェック
        internal bool HannyuusakiDateCheck(string inputNioroshiGyoushaCd, string inputNioroshiGenbaCd, string inputSagyouDate, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (String.IsNullOrEmpty(inputSagyouDate))
                {
                    return true;
                }

                M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = this.accessor.GetAllValidHannyuuClosedData(inputNioroshiGyoushaCd, inputNioroshiGenbaCd, inputSagyouDate);

                //取得テータ
                if (workclosedhannyuusakiList.Count() >= 1)
                {
                    //this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    errmessage.MessageBoxShow("E206", "荷降現場", "伝票日付：" + Convert.ToDateTime(inputSagyouDate).ToString("yyyy/MM/dd"));
                    return false;
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HannyuusakiDateCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
        }
        #endregion

        #region 受入在庫品名登録前チェック
        /// <summary>
        /// 受入在庫品名登録前チェック
        /// </summary>
        /// <returns></returns>
        internal bool ZaikoRegistCheck(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart(dto);


                // 在庫管理の場合のみチェックする
                if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
                {
                    Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>> rowZaikoHinmeiHuriwakes = new Dictionary<T_UKEIRE_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
                    foreach (T_UKEIRE_DETAIL d in dto.detailList)
                    {
                        List<T_ZAIKO_HINMEI_HURIWAKE> zaikoHinmeiHuriwakes = dto.GetZaikoHinmeiHuriwakeListByDetail(
                                    d.SYSTEM_ID,
                                    d.DETAIL_SYSTEM_ID,
                                    d.SEQ);
                        rowZaikoHinmeiHuriwakes[d] = zaikoHinmeiHuriwakes != null ? zaikoHinmeiHuriwakes : new List<T_ZAIKO_HINMEI_HURIWAKE>();
                    }

                    // 在庫を設定したか判定
                    var zaikoSetted = rowZaikoHinmeiHuriwakes.Sum(row => row.Value == null ? 0 : row.Value.Count) > 0;

                    // 現場自社区分
                    bool jishaKbn = false;
                    catchErr = true;
                    // 削除フラグ、適用期間の範囲は考慮しない
                    var retData = this.accessor.GetGenbaDataByCode(dto.entry.NIOROSHI_GYOUSHA_CD, dto.entry.NIOROSHI_GENBA_CD);
                    if (!catchErr)
                    {
                        return returnVal;
                    }
                    var genba = retData;
                    if (genba != null && !genba.JISHA_KBN.IsNull)
                    {
                        jishaKbn = genba.JISHA_KBN.IsTrue;
                    }

                    // 在庫が設定していないが、または(設定した且つ)現場は自社の場合
                    returnVal = !zaikoSetted || jishaKbn;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoRegistCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoRegistCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        #endregion

        #region 受入現金取引チェック
        /// <summary>
        /// 受入現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定以外の場合
        /// false = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定の場合
        /// </returns>
        internal bool GenkinTorihikiCheck(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto, out bool catchErr)
        {
            catchErr = true;
            var ren = true;
            try
            {
                var uriageTorihikiKbn = dto.seikyuu.TORIHIKI_KBN_CD.Value.ToString();
                var shiharaiTorihikiKbn = dto.shiharai.TORIHIKI_KBN_CD.Value.ToString();
                var kakuteiFlg = dto.entry.KAKUTEI_KBN.Value;
                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 1).Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 2).Count();
                }

                // 確定フラグが2：未確定の場合
                if ((uriageRowCount != 0 || siharaiRowCount != 0) && (kakuteiFlg == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E236");
                    ren = false;
                }

                return ren;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return ren;
        }
        #endregion

        #region 出荷在庫品名登録前チェック
        /// <summary>
        /// 出荷在庫品名登録前チェック
        /// </summary>
        /// <returns></returns>
        internal bool ZaikoRegistCheck(ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart(dto);


                // 在庫管理の場合のみチェックする
                if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
                {
                    Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>> rowZaikoHinmeiHuriwakes = new Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>>();
                    foreach (T_SHUKKA_DETAIL d in dto.detailList)
                    {
                        List<T_ZAIKO_HINMEI_HURIWAKE> zaikoHinmeiHuriwakes = dto.GetZaikoHinmeiHuriwakeListByDetail(
                                    d.SYSTEM_ID,
                                    d.DETAIL_SYSTEM_ID,
                                    d.SEQ);
                        rowZaikoHinmeiHuriwakes[d] = zaikoHinmeiHuriwakes != null ? zaikoHinmeiHuriwakes : new List<T_ZAIKO_HINMEI_HURIWAKE>();
                    }

                    // 在庫を設定したか判定
                    var zaikoSetted = rowZaikoHinmeiHuriwakes.Sum(row => row.Value == null ? 0 : row.Value.Count) > 0;

                    // 現場自社区分
                    bool jishaKbn = false;
                    catchErr = true;
                    // 削除フラグ、適用期間の範囲は考慮しない
                    var retData = this.accessor.GetGenbaDataByCode(dto.entry.NIZUMI_GYOUSHA_CD, dto.entry.NIZUMI_GENBA_CD);
                    if (!catchErr)
                    {
                        return returnVal;
                    }
                    var genba = retData;
                    if (genba != null && !genba.JISHA_KBN.IsNull)
                    {
                        jishaKbn = genba.JISHA_KBN.IsTrue;
                    }

                    // 在庫が設定していないが、または(設定した且つ)現場は自社の場合
                    returnVal = !zaikoSetted || jishaKbn;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoRegistCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoRegistCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        #endregion

        #region 出荷現金取引チェック
        /// <summary>
        /// 出荷現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定以外の場合
        /// false = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定の場合
        /// </returns>
        internal bool GenkinTorihikiCheck(ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto, out bool catchErr)
        {
            catchErr = true;
            var ren = true;
            try
            {
                var uriageTorihikiKbn = dto.seikyuu.TORIHIKI_KBN_CD.Value.ToString();
                var shiharaiTorihikiKbn = dto.shiharai.TORIHIKI_KBN_CD.Value.ToString();
                var kakuteiFlg = dto.entry.KAKUTEI_KBN.Value;
                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 1).Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 2).Count();
                }

                // 確定フラグが2：未確定の場合
                if ((uriageRowCount != 0 || siharaiRowCount != 0) && (kakuteiFlg == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E236");
                    ren = false;
                }

                return ren;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return ren;
        }
        #endregion

        #region 売上支払現金取引チェック
        /// <summary>
        /// 売上支払現金取引チェック
        /// </summary>
        /// <returns>
        /// true  = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定以外の場合
        /// false = 取引区分の売上支払のどちらかが現金 AND 確定フラグが2：未確定の場合
        /// </returns>
        internal bool GenkinTorihikiCheck(ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto, out bool catchErr)
        {
            catchErr = true;
            var ren = true;
            try
            {
                var uriageTorihikiKbn = dto.seikyuu.TORIHIKI_KBN_CD.Value.ToString();
                var shiharaiTorihikiKbn = dto.shiharai.TORIHIKI_KBN_CD.Value.ToString();
                var kakuteiFlg = dto.entry.KAKUTEI_KBN.Value;
                var genkin = SalesPaymentConstans.STR_TORIHIKI_KBN_1;
                var uriageRowCount = 0;
                var siharaiRowCount = 0;

                // 売上
                if (uriageTorihikiKbn == genkin)
                {
                    // 明細の売上行数
                    uriageRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 1).Count();
                }

                // 支払
                if (shiharaiTorihikiKbn == genkin)
                {
                    // 明細の支払行数
                    siharaiRowCount = dto.detailList.Where(r => r.DENPYOU_KBN_CD.Value == 2).Count();
                }

                // 確定フラグが2：未確定の場合
                if ((uriageRowCount != 0 || siharaiRowCount != 0) && (kakuteiFlg == SalesPaymentConstans.KAKUTEI_KBN_MIKAKUTEI))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E236");
                    ren = false;
                }

                return ren;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenkinTorihikiCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            return ren;
        }
        #endregion

        #region 月次ロックチェック

        /// <summary>
        /// [登録処理用] 月次ロックされているのかの判定を行います
        /// </summary>
        /// <returns>月次ロック中：True</returns>
        internal bool GetsujiLockCheck(DateTime beforDate, DateTime updateDate)
        {
            bool returnVal = false;

            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 月次処理中チェック
            if ((beforDate.CompareTo(updateDate) != 0) &&
                getsujiShoriCheckLogic.CheckGetsujiShoriChu(beforDate))
            {
                returnVal = true;
                msgLogic.MessageBoxShow("E224", "修正");
            }
            else if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(updateDate))
            {
                returnVal = true;
                msgLogic.MessageBoxShow("E224", "修正");
            }
            // 月次ロックチェック
            else if ((beforDate.CompareTo(updateDate) != 0) &&
                getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(beforDate.Year.ToString()), short.Parse(beforDate.Month.ToString())))
            {
                returnVal = true;
                msgLogic.MessageBoxShow("E223", "修正");
            }
            else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(updateDate.Year.ToString()), short.Parse(updateDate.Month.ToString())))
            {
                returnVal = true;
                msgLogic.MessageBoxShow("E223", "修正");
            }

            return returnVal;
        }

        #endregion

        #region 諸口ReadOnly変更
        public void SetShokuchi(object cdCell, object nameCell, bool shokuchiFlag, int rowIndex)
        {
            GcCustomTextBoxCell cd = cdCell as GcCustomTextBoxCell;
            GcCustomTextBoxCell name = nameCell as GcCustomTextBoxCell;
            if (name == null)
            {
                return;
            }
            name.ReadOnly = !shokuchiFlag;
            if (cd.Name == ConstCls.COLUMN_SHARYOU_CD)
            {
                if (shokuchiFlag)
                {
                    cd.AutoChangeBackColorEnabled = false;
                    cd.Style.BackColor = this.sharyouCdBackColor;
                }
                else
                {
                    cd.AutoChangeBackColorEnabled = true;
                }
            }
            (name as ICustomAutoChangeBackColor).UpdateBackColor(name.Selected);
            this.UpdateShokuchiMap(cd.Name, rowIndex, shokuchiFlag);
        }
        #endregion

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        #endregion

        #region TabStop情報取得処理
        private Dictionary<string, TabGoDto> MapInit(string kbn)
        {
            Dictionary<string, TabGoDto> mapRow = new Dictionary<string, TabGoDto>();
            string[] columnName = { };
            if (kbn == "1")
            {
                columnName = this.columnUkeire;
            }
            else if (kbn == "2")
            {
                columnName = this.columnShukka;
            }
            else if (kbn == "3")
            {
                columnName = this.columnUrsh;
            }
            for (int i = 0; i < columnName.Length; i++)
            {
                TabGoDto dto = new TabGoDto();
                dto.ControlName = columnName[i];
                if (i == 0)
                {
                    dto.PreviousControlName = columnName[columnName.Length - 1];
                    dto.isFirst = true;
                }
                else
                {
                    dto.PreviousControlName = columnName[i - 1];
                    dto.isFirst = false;
                }
                if (i == columnName.Length - 1)
                {
                    dto.NextControlName = columnName[0];
                    dto.isLast = true;
                }
                else
                {
                    dto.NextControlName = columnName[i + 1];
                    dto.isLast = false;
                }
                mapRow.Add(dto.ControlName, dto);
            }
            return mapRow;
        }

        public void UpdateShokuchiMap(string cellName, int index, bool shokuchi)
        {
            if (!this.map[index].ContainsKey(cellName)) { return; }
            TabGoDto thisControl = this.map[index][cellName];
            string value = Convert.ToString(this.form.mrDetail.Rows[index][cellName].Value);
            switch (cellName)
            {
                case ConstCls.COLUMN_TORIHIKISAKI_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_TORIHIKISAKI_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_TORIHIKISAKI_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_TORIHIKISAKI_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_TORIHIKISAKI_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_GYOUSHA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_GYOUSHA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_GYOUSHA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_GYOUSHA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_GYOUSHA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_GENBA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_GENBA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_GENBA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_GENBA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_GENBA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_NIZUMI_GYOUSHA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_NIZUMI_GENBA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_NIZUMI_GENBA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_NIZUMI_GENBA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_NIZUMI_GENBA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_NIZUMI_GENBA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_NIOROSHI_GENBA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_NIOROSHI_GENBA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_NIOROSHI_GENBA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_NIOROSHI_GENBA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_NIOROSHI_GENBA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_UNPAN_GYOUSHA_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_UNPAN_GYOUSHA_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_UNPAN_GYOUSHA_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_UNPAN_GYOUSHA_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_UNPAN_GYOUSHA_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
                case ConstCls.COLUMN_SHARYOU_CD:
                    if (shokuchi)
                    {
                        if (thisControl.NextControlName != ConstCls.COLUMN_SHARYOU_NAME)
                        {
                            this.AddTabStop(index, thisControl, ConstCls.COLUMN_SHARYOU_NAME);
                            thisControl.NextControlName = ConstCls.COLUMN_SHARYOU_NAME;
                            thisControl.isLast = false;
                        }
                    }
                    else if (thisControl.NextControlName == ConstCls.COLUMN_SHARYOU_NAME)
                    {
                        TabGoDto nextControl = this.map[index][thisControl.NextControlName];
                        this.RemoveTabStop(index, thisControl, nextControl);
                        thisControl.NextControlName = nextControl.NextControlName;
                        thisControl.isLast = nextControl.isLast;
                    }
                    break;
            }

        }

        private void AddTabStop(int rowIndex, TabGoDto thisControl, string newName)
        {
            TabGoDto nextControl = map[rowIndex][thisControl.NextControlName];
            nextControl.PreviousControlName = newName;
            TabGoDto newControl = new TabGoDto();
            newControl.ControlName = newName;
            newControl.PreviousControlName = thisControl.ControlName;
            newControl.NextControlName = nextControl.ControlName;
            newControl.isLast = thisControl.isLast;
            newControl.isFirst = false;
            map[rowIndex].Add(newControl.ControlName, newControl);
        }

        private void RemoveTabStop(int rowIndex, TabGoDto thisControl, TabGoDto removeControl)
        {
            TabGoDto nextControl = map[rowIndex][removeControl.NextControlName];
            nextControl.PreviousControlName = thisControl.ControlName;
            map[rowIndex].Remove(removeControl.ControlName);
        }
        #endregion

        #endregion

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headerForm.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }
        #endregion 必須チェックエラーフォーカス処理

        #region 締状況チェック処理
        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal bool CheckAllShimeStatus(T_UKEIRE_ENTRY tue)
        {
            bool retval = false;

            long systemId = -1;
            int seq = -1;

            if (!tue.SYSTEM_ID.IsNull) systemId = (long)tue.SYSTEM_ID;
            if (!tue.SEQ.IsNull) seq = (int)tue.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, tue.TORIHIKISAKI_CD, 1);
                DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, tue.TORIHIKISAKI_CD, 1);
                T_ZAIKO_UKEIRE_DETAIL zaikoData = this.accessor.GetZaikoUkeireData(systemId, seq);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(精算明細)
                if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                {
                    retval = true;
                }

                if (retval == false && zaikoData != null)
                {
                    retval = true;
                }
            }

            return retval;
        }

        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal bool CheckAllShimeStatus(T_SHUKKA_ENTRY tse)
        {
            bool retval = false;

            long systemId = -1;
            int seq = -1;

            if (!tse.SYSTEM_ID.IsNull) systemId = (long)tse.SYSTEM_ID;
            if (!tse.SEQ.IsNull) seq = (int)tse.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, tse.TORIHIKISAKI_CD, 2);
                DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, tse.TORIHIKISAKI_CD, 2);
                T_ZAIKO_UKEIRE_DETAIL zaikoData = this.accessor.GetZaikoUkeireData(systemId, seq);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(精算明細)
                if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                {
                    retval = true;
                }

                if (retval == false && zaikoData != null)
                {
                    retval = true;
                }
            }

            return retval;
        }

        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        internal bool CheckAllShimeStatus(T_UR_SH_ENTRY tse)
        {
            bool retval = false;

            long systemId = -1;
            int seq = -1;

            if (!tse.SYSTEM_ID.IsNull) systemId = (long)tse.SYSTEM_ID;
            if (!tse.SEQ.IsNull) seq = (int)tse.SEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = this.accessor.GetSeikyuMeisaiData(systemId, seq, -1, tse.TORIHIKISAKI_CD, 3);
                DataTable seisanData = this.accessor.GetSeisanMeisaiData(systemId, seq, -1, tse.TORIHIKISAKI_CD, 3);
                T_ZAIKO_UKEIRE_DETAIL zaikoData = this.accessor.GetZaikoUkeireData(systemId, seq);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(精算明細)
                if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                {
                    retval = true;
                }

                if (retval == false && zaikoData != null)
                {
                    retval = true;
                }
            }

            return retval;
        }
        #endregion 締状況チェック処理

        #region 受入登録チェック処理
        /// <summary>
        /// 受入登録チェック処理
        /// </summary>
        internal bool RegistCheck(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto, ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> beforeDto, int index)
        {
            bool ret = true;

            M_TORIHIKISAKI tori = this.accessor.GetTorihikisakiDataByCode(dto.entry.TORIHIKISAKI_CD);
            // 取引先と拠点コードの関連チェック
            if (tori != null && tori.DELETE_FLG.IsFalse && !this.form.CheckTorihikisakiKyoten(dto.entry.KYOTEN_CD.Value.ToString(), tori))
            {
                this.errmessage.MessageBoxShow("E146");
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD];
                ret = false;
                return ret;
            }

            bool catchErr = true;
            // 車輌休動チェック
            bool retCheck = this.SharyouDateCheck(dto.entry.UNPAN_GYOUSHA_CD, dto.entry.SHARYOU_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            bool retCheck2 = this.UntenshaDateCheck(dto.entry.UNTENSHA_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }
            // 運搬入先休動チェック
            bool retCheck3 = this.HannyuusakiDateCheck(dto.entry.NIOROSHI_GYOUSHA_CD, dto.entry.NIOROSHI_GENBA_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }

            // 車輌休動チェック
            if (!retCheck)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_SHARYOU_CD];
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            else if (!retCheck2)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_UNTENSHA_CD];
                ret = false;
                return ret;
            }
            // 運搬入先休動チェック
            else if (!retCheck3)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD];
                ret = false;
                return ret;
            }
            // 在庫品名振分チェック
            if (!this.form.RegistErrorFlag)
            {
                bool retRegist = this.ZaikoRegistCheck(dto, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }

                if (!retRegist)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShowError("在庫品名が選択されている場合、自社の荷降現場を選択する必要があります。");
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }
            // コンテナチェック
            else if (!this.form.RegistErrorFlag && dto.contenaResults.Count > 0 && string.IsNullOrEmpty(dto.entry.GENBA_CD))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E180");
                this.form.RegistErrorFlag = true;
            }

            // 現金取引チェック
            if (!this.form.RegistErrorFlag)
            {
                catchErr = true;
                retCheck = this.GenkinTorihikiCheck(dto, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }
                if (!retCheck)
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            /* 月次処理中 or 月次処理ロックチェック */
            if (!this.form.RegistErrorFlag)
            {
                if (this.GetsujiLockCheck(beforeDto.entry.DENPYOU_DATE.Value, dto.entry.DENPYOU_DATE.Value))
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            return ret;
        }
        #endregion

        #region 出荷登録チェック処理
        /// <summary>
        /// 出荷登録チェック処理
        /// </summary>
        internal bool RegistCheck(ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto, ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> beforeDto, int index)
        {
            bool ret = true;

            M_TORIHIKISAKI tori = this.accessor.GetTorihikisakiDataByCode(dto.entry.TORIHIKISAKI_CD);
            // 取引先と拠点コードの関連チェック
            if (tori != null && tori.DELETE_FLG.IsFalse && !this.form.CheckTorihikisakiKyoten(dto.entry.KYOTEN_CD.Value.ToString(), tori))
            {
                this.errmessage.MessageBoxShow("E146");
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD];
                ret = false;
                return ret;
            }

            bool catchErr = true;
            // 車輌休動チェック
            bool retCheck = this.SharyouDateCheck(dto.entry.UNPAN_GYOUSHA_CD, dto.entry.SHARYOU_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            bool retCheck2 = this.UntenshaDateCheck(dto.entry.UNTENSHA_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }

            // 車輌休動チェック
            if (!retCheck)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_SHARYOU_CD];
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            else if (!retCheck2)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_UNTENSHA_CD];
                ret = false;
                return ret;
            }
            // 在庫品名振分チェック
            if (!this.form.RegistErrorFlag)
            {
                bool retRegist = this.ZaikoRegistCheck(dto, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }

                if (!retRegist)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShowError("在庫品名が選択されている場合、自社の荷降現場を選択する必要があります。");
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            // 現金取引チェック
            if (!this.form.RegistErrorFlag)
            {
                catchErr = true;
                retCheck = this.GenkinTorihikiCheck(dto, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }
                if (!retCheck)
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            /* 月次処理中 or 月次処理ロックチェック */
            if (!this.form.RegistErrorFlag)
            {
                if (this.GetsujiLockCheck(beforeDto.entry.DENPYOU_DATE.Value, dto.entry.DENPYOU_DATE.Value))
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            return ret;
        }
        #endregion

        #region 売上支払登録チェック処理
        /// <summary>
        /// 売上支払登録チェック処理
        /// </summary>
        internal bool RegistCheck(ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto, ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> beforeDto, int index)
        {
            bool ret = true;

            M_TORIHIKISAKI tori = this.accessor.GetTorihikisakiDataByCode(dto.entry.TORIHIKISAKI_CD);
            // 取引先と拠点コードの関連チェック
            if (tori != null && tori.DELETE_FLG.IsFalse && !this.form.CheckTorihikisakiKyoten(dto.entry.KYOTEN_CD.Value.ToString(), tori))
            {
                this.errmessage.MessageBoxShow("E146");
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD];
                ret = false;
                return ret;
            }

            bool catchErr = true;
            // 車輌休動チェック
            bool retCheck = this.SharyouDateCheck(dto.entry.UNPAN_GYOUSHA_CD, dto.entry.SHARYOU_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            bool retCheck2 = this.UntenshaDateCheck(dto.entry.UNTENSHA_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }
            // 運搬入先休動チェック
            bool retCheck3 = this.HannyuusakiDateCheck(dto.entry.NIOROSHI_GYOUSHA_CD, dto.entry.NIOROSHI_GENBA_CD, Convert.ToString(dto.entry.DENPYOU_DATE.Value), out catchErr);
            if (!catchErr)
            {
                ret = false;
                return ret;
            }

            // 車輌休動チェック
            if (!retCheck)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_SHARYOU_CD];
                ret = false;
                return ret;
            }
            // 運転者休動チェック
            else if (!retCheck2)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_UNTENSHA_CD];
                ret = false;
                return ret;
            }
            // 運搬入先休動チェック
            else if (!retCheck3)
            {
                this.form.mrDetail.Focus();
                this.form.mrDetail.CurrentCell = this.form.mrDetail.Rows[index].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD];
                ret = false;
                return ret;
            }
            // コンテナチェック
            if (!this.form.RegistErrorFlag && dto.contenaResults.Count > 0 && string.IsNullOrEmpty(dto.entry.GENBA_CD))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E180");
                this.form.RegistErrorFlag = true;
            }

            // 現金取引チェック
            if (!this.form.RegistErrorFlag)
            {
                catchErr = true;
                retCheck = this.GenkinTorihikiCheck(dto, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }
                if (!retCheck)
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            /* 月次処理中 or 月次処理ロックチェック */
            if (!this.form.RegistErrorFlag)
            {
                if (this.GetsujiLockCheck(beforeDto.entry.DENPYOU_DATE.Value, dto.entry.DENPYOU_DATE.Value))
                {
                    this.form.RegistErrorFlag = true;
                    ret = false;
                }
            }

            return ret;
        }
        #endregion

        #region 補助ファンクション
        /// <summary>
        /// Int16?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int16? ToNInt16(object o)
        {
            Int16? ret = null;
            Int16 parse = 0;
            if (Int16.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int32?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal int? ToNInt32(object o)
        {
            int? ret = null;
            int parse = 0;
            if (int.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// Int64?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal Int64? ToNInt64(object o)
        {
            Int64? ret = null;
            Int64 parse = 0;
            if (Int64.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// double?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal double? ToNDouble(object o)
        {
            double? ret = null;
            double parse = 0;
            if (double.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// decimal?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal decimal? ToNDecimal(object o)
        {
            decimal? ret = null;
            decimal parse = 0;
            if (Decimal.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// bool?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal bool? ToNBoolean(object o)
        {
            bool? ret = null;
            bool parse = false;
            if (Boolean.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }

        /// <summary>
        /// DateTime?型に転換する
        /// </summary>
        /// <param name="o">o</param>
        internal DateTime? ToNDateTime(object o)
        {
            DateTime? ret = null;
            DateTime parse = DateTime.Now;
            if (DateTime.TryParse(Convert.ToString(o), out parse))
            {
                ret = parse;
            }
            return ret;
        }
        #endregion

        #region 受入税計算
        internal void ZeiKeisan(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto)
        {
            var denpyouHakouPopUpDTO = this.createParameterDTOClass(dto.entry, dto.seikyuu, dto.shiharai);
            decimal URIAGE_AMOUNT_TOTAL = 0;
            decimal SHIHARAI_KINGAKU_TOTAL = 0;
            foreach (var detail in dto.detailList)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.seikyuu.KINGAKU_HASUU_CD.IsNull ? 0 : dto.seikyuu.KINGAKU_HASUU_CD.Value);
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.Value;
                }
                else
                {
                    detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.shiharai.KINGAKU_HASUU_CD.IsNull ? 0 : dto.shiharai.KINGAKU_HASUU_CD.Value);
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.Value;
                }
            }

            /**
             * 伝票発行画面にて取得したデータ
             */
            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn))
            {
                dto.entry.URIAGE_ZEI_KEISAN_KBN_CD = (SqlInt16)seikyuZeikeisanKbn;
            }
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd))
                dto.entry.URIAGE_ZEI_KBN_CD = (SqlInt16)uriageZeiKbnCd;
            // 売上取引区分CD
            int uriageTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn, out uriageTorihikiKbnCd))
            {
                dto.entry.URIAGE_TORIHIKI_KBN_CD = (SqlInt16)uriageTorihikiKbnCd;
            }
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = (SqlInt16)shiharaiZeiKeisanKbnCd;
            }
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KBN_CD = (SqlInt16)shiharaiZeiKbnCd;
            }
            // 支払取引区分CD
            int ShiharaiTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn, out ShiharaiTorihikiKbnCd))
            {
                dto.entry.SHIHARAI_TORIHIKI_KBN_CD = (SqlInt16)ShiharaiTorihikiKbnCd;
            }
            decimal HimeiUrKingakuTotal = 0;
            decimal HimeiShKingakuTotal = 0;
            decimal HinmeiUrTaxSotoTotal = 0;
            decimal HinmeiShTaxSotoTotal = 0;
            decimal HinmeiUrTaxUchiTotal = 0;
            decimal HinmeiShTaxUchiTotal = 0;
            decimal UrTaxSotoTotal = 0;
            decimal ShTaxSotoTotal = 0;
            decimal UrTaxUchiTotal = 0;
            decimal ShTaxUchiTotal = 0;
            foreach (T_UKEIRE_DETAIL detail in dto.detailList)
            {
                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(detail.HINMEI_CD);
                detail.HINMEI_ZEI_KBN_CD = hinmei.ZEI_KBN_CD;
                if (detail.HINMEI_ZEI_KBN_CD.IsNull || detail.HINMEI_ZEI_KBN_CD == 0)
                {
                    detail.KINGAKU = detail.KINGAKU;
                    detail.HINMEI_KINGAKU = 0;
                }
                else
                {
                    if (!detail.KINGAKU.IsNull)
                    {
                        detail.HINMEI_KINGAKU = detail.KINGAKU.Value;

                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                        {
                            HimeiUrKingakuTotal += detail.KINGAKU.Value;
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                        {
                            HimeiShKingakuTotal += detail.KINGAKU.Value;
                        }
                    }
                    detail.KINGAKU = 0;
                }

                // 明細毎消費税合計を計算
                // この時点で明細.品名のデータは検索済みなので、品名データ取得処理はしない

                decimal meisaiKingaku = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);

                detail.TAX_SOTO = 0;          // 消費税外税初期値
                detail.TAX_UCHI = 0;          // 消費税内税初期値
                detail.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                detail.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!detail.URIAGESHIHARAI_DATE.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)detail.URIAGESHIHARAI_DATE).Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        detailShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }

                string CELL_NAME_SHOUHIZEI_RATE = "";
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.URIAGE_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.URIAGE_SHOUHIZEI_RATE.Value.ToString();
                }
                else
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.SHIHARAI_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.SHIHARAI_SHOUHIZEI_RATE.Value.ToString();
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (!string.IsNullOrEmpty(CELL_NAME_SHOUHIZEI_RATE)
                    && decimal.TryParse(CELL_NAME_SHOUHIZEI_RATE, out tempShouhizeiRate))
                {
                    detailShouhizeiRate = tempShouhizeiRate;
                }

                if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO: 明細毎消費税合計は品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO:明細毎消費税合計は 品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);

                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }


            dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = URIAGE_AMOUNT_TOTAL;
            dto.entry.URIAGE_KINGAKU_TOTAL = uriageTotal - dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                dto.entry.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(dto.entry.URIAGE_KINGAKU_TOTAL * dto.entry.URIAGE_SHOUHIZEI_RATE),
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_URIAGE_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiUrTaxSotoTotal,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                // 金額計算
                dto.entry.URIAGE_TAX_UCHI
                    = (dto.entry.URIAGE_KINGAKU_TOTAL
                        - (dto.entry.URIAGE_KINGAKU_TOTAL / (dto.entry.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                dto.entry.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.URIAGE_TAX_UCHI,
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税外税合計
            dto.entry.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            dto.entry.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = SHIHARAI_KINGAKU_TOTAL;
            dto.entry.SHIHARAI_KINGAKU_TOTAL = shiharaiTotal - dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                dto.entry.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_KINGAKU_TOTAL * (decimal)dto.entry.SHIHARAI_SHOUHIZEI_RATE,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiShTaxSotoTotal,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                // 金額計算
                dto.entry.SHIHARAI_TAX_UCHI
                    = dto.entry.SHIHARAI_KINGAKU_TOTAL
                        - (dto.entry.SHIHARAI_KINGAKU_TOTAL / (dto.entry.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                dto.entry.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_TAX_UCHI,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払明細毎消費税外税合計
            dto.entry.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            dto.entry.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;
        }

        /// <summary>
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass createParameterDTOClass(T_UKEIRE_ENTRY entry, M_TORIHIKISAKI_SEIKYUU seikyuu, M_TORIHIKISAKI_SHIHARAI shiharai)
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass returnVal = new Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass();

            // 新規、修正共通で設定
            returnVal.Uriage_Date = entry.URIAGE_DATE.IsNull ? "" : entry.URIAGE_DATE.Value.Date.ToString();
            returnVal.Shiharai_Date = entry.SHIHARAI_DATE.IsNull ? "" : entry.SHIHARAI_DATE.Value.Date.ToString();

            returnVal.Uriage_Shouhizei_Rate = Convert.ToString(entry.URIAGE_SHOUHIZEI_RATE.Value);
            returnVal.Shiharai_Shouhizei_Rate = Convert.ToString(entry.SHIHARAI_SHOUHIZEI_RATE.Value);

            returnVal.Seikyu_Zeikeisan_Kbn = Convert.ToString(entry.URIAGE_ZEI_KEISAN_KBN_CD);
            returnVal.Seikyu_Zei_Kbn = Convert.ToString(entry.URIAGE_ZEI_KBN_CD);
            returnVal.Seikyu_Rohiki_Kbn = Convert.ToString(entry.URIAGE_TORIHIKI_KBN_CD);
            returnVal.Seikyu_Seisan_Kbn = "2";

            returnVal.Shiharai_Zeikeisan_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KEISAN_KBN_CD);
            returnVal.Shiharai_Zei_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KBN_CD);
            returnVal.Shiharai_Rohiki_Kbn = Convert.ToString(entry.SHIHARAI_TORIHIKI_KBN_CD);
            returnVal.Shiharai_Seisan_Kbn = "2";

            returnVal.Sosatu = "2";
            return returnVal;
        }

        /// <summary>
        /// 売上明細毎消費税を計算する(外、内税両方)
        /// </summary>
        /// <param name="hinmei">明細.品名</param>
        /// <param name="kingaku">明細.金額</param>
        /// <param name="zeiKbn">伝票発行画面.請求税区分</param>
        /// <returns></returns>
        private decimal CalcTaxForUriageDetial(decimal kingaku, decimal uriageShouhizeiRate, int hasuuCd, string zeiKbn)
        {
            decimal returnVal = 0;

            // TODO: 税区分はConstクラスの値で判定
            switch (zeiKbn)
            {
                // 一般的な税区分を使用
                case "1":
                    returnVal = CommonCalc.FractionCalc((kingaku * uriageShouhizeiRate), hasuuCd);
                    break;

                case "2":
                    returnVal = kingaku - (kingaku / (uriageShouhizeiRate + 1));
                    // 端数処理
                    returnVal
                        = CommonCalc.FractionCalc(returnVal, hasuuCd);
                    break;

                default:
                    break;
            }

            return returnVal;
        }
        #endregion

        #region 出荷税計算
        internal void ZeiKeisan(ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto)
        {
            var denpyouHakouPopUpDTO = this.createParameterDTOClass(dto.entry, dto.seikyuu, dto.shiharai);
            decimal URIAGE_AMOUNT_TOTAL = 0;
            decimal SHIHARAI_KINGAKU_TOTAL = 0;
            foreach (var detail in dto.detailList)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.seikyuu.KINGAKU_HASUU_CD.IsNull ? 0 : dto.seikyuu.KINGAKU_HASUU_CD.Value);
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.Value;
                }
                else
                {
                    detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.shiharai.KINGAKU_HASUU_CD.IsNull ? 0 : dto.shiharai.KINGAKU_HASUU_CD.Value);
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.Value;
                }
            }

            /**
             * 伝票発行画面にて取得したデータ
             */
            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn))
            {
                dto.entry.URIAGE_ZEI_KEISAN_KBN_CD = (SqlInt16)seikyuZeikeisanKbn;
            }
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd))
                dto.entry.URIAGE_ZEI_KBN_CD = (SqlInt16)uriageZeiKbnCd;
            // 売上取引区分CD
            int uriageTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn, out uriageTorihikiKbnCd))
            {
                dto.entry.URIAGE_TORIHIKI_KBN_CD = (SqlInt16)uriageTorihikiKbnCd;
            }
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = (SqlInt16)shiharaiZeiKeisanKbnCd;
            }
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KBN_CD = (SqlInt16)shiharaiZeiKbnCd;
            }
            // 支払取引区分CD
            int ShiharaiTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn, out ShiharaiTorihikiKbnCd))
            {
                dto.entry.SHIHARAI_TORIHIKI_KBN_CD = (SqlInt16)ShiharaiTorihikiKbnCd;
            }
            decimal HimeiUrKingakuTotal = 0;
            decimal HimeiShKingakuTotal = 0;
            decimal HinmeiUrTaxSotoTotal = 0;
            decimal HinmeiShTaxSotoTotal = 0;
            decimal HinmeiUrTaxUchiTotal = 0;
            decimal HinmeiShTaxUchiTotal = 0;
            decimal UrTaxSotoTotal = 0;
            decimal ShTaxSotoTotal = 0;
            decimal UrTaxUchiTotal = 0;
            decimal ShTaxUchiTotal = 0;
            foreach (T_SHUKKA_DETAIL detail in dto.detailList)
            {
                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(detail.HINMEI_CD);
                detail.HINMEI_ZEI_KBN_CD = hinmei.ZEI_KBN_CD;
                if (detail.HINMEI_ZEI_KBN_CD.IsNull || detail.HINMEI_ZEI_KBN_CD == 0)
                {
                    detail.KINGAKU = detail.KINGAKU;
                    detail.HINMEI_KINGAKU = 0;
                }
                else
                {
                    if (!detail.KINGAKU.IsNull)
                    {
                        detail.HINMEI_KINGAKU = detail.KINGAKU.Value;

                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                        {
                            HimeiUrKingakuTotal += detail.KINGAKU.Value;
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                        {
                            HimeiShKingakuTotal += detail.KINGAKU.Value;
                        }
                    }
                    detail.KINGAKU = 0;
                }

                // 明細毎消費税合計を計算
                // この時点で明細.品名のデータは検索済みなので、品名データ取得処理はしない

                decimal meisaiKingaku = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);

                detail.TAX_SOTO = 0;          // 消費税外税初期値
                detail.TAX_UCHI = 0;          // 消費税内税初期値
                detail.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                detail.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!detail.URIAGESHIHARAI_DATE.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)detail.URIAGESHIHARAI_DATE).Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        detailShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }

                string CELL_NAME_SHOUHIZEI_RATE = "";
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.URIAGE_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.URIAGE_SHOUHIZEI_RATE.Value.ToString();
                }
                else
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.SHIHARAI_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.SHIHARAI_SHOUHIZEI_RATE.Value.ToString();
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (!string.IsNullOrEmpty(CELL_NAME_SHOUHIZEI_RATE)
                    && decimal.TryParse(CELL_NAME_SHOUHIZEI_RATE, out tempShouhizeiRate))
                {
                    detailShouhizeiRate = tempShouhizeiRate;
                }

                if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO: 明細毎消費税合計は品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO:明細毎消費税合計は 品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);

                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }


            dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = URIAGE_AMOUNT_TOTAL;
            dto.entry.URIAGE_AMOUNT_TOTAL = uriageTotal - dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                dto.entry.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(dto.entry.URIAGE_AMOUNT_TOTAL * dto.entry.URIAGE_SHOUHIZEI_RATE),
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_URIAGE_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiUrTaxSotoTotal,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                // 金額計算
                dto.entry.URIAGE_TAX_UCHI
                    = (dto.entry.URIAGE_AMOUNT_TOTAL
                        - (dto.entry.URIAGE_AMOUNT_TOTAL / (dto.entry.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                dto.entry.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.URIAGE_TAX_UCHI,
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税外税合計
            dto.entry.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            dto.entry.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = SHIHARAI_KINGAKU_TOTAL;
            dto.entry.SHIHARAI_AMOUNT_TOTAL = shiharaiTotal - dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                dto.entry.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_AMOUNT_TOTAL * (decimal)dto.entry.SHIHARAI_SHOUHIZEI_RATE,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiShTaxSotoTotal,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                // 金額計算
                dto.entry.SHIHARAI_TAX_UCHI
                    = dto.entry.SHIHARAI_AMOUNT_TOTAL
                        - (dto.entry.SHIHARAI_AMOUNT_TOTAL / (dto.entry.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                dto.entry.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_TAX_UCHI,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払明細毎消費税外税合計
            dto.entry.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            dto.entry.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;
        }

        /// <summary>
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass createParameterDTOClass(T_SHUKKA_ENTRY entry, M_TORIHIKISAKI_SEIKYUU seikyuu, M_TORIHIKISAKI_SHIHARAI shiharai)
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass returnVal = new Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass();

            // 新規、修正共通で設定
            returnVal.Uriage_Date = entry.URIAGE_DATE.IsNull ? "" : entry.URIAGE_DATE.Value.Date.ToString();
            returnVal.Shiharai_Date = entry.SHIHARAI_DATE.IsNull ? "" : entry.SHIHARAI_DATE.Value.Date.ToString();

            returnVal.Uriage_Shouhizei_Rate = Convert.ToString(entry.URIAGE_SHOUHIZEI_RATE.Value);
            returnVal.Shiharai_Shouhizei_Rate = Convert.ToString(entry.SHIHARAI_SHOUHIZEI_RATE.Value);

            returnVal.Seikyu_Zeikeisan_Kbn = Convert.ToString(entry.URIAGE_ZEI_KEISAN_KBN_CD);
            returnVal.Seikyu_Zei_Kbn = Convert.ToString(entry.URIAGE_ZEI_KBN_CD);
            returnVal.Seikyu_Rohiki_Kbn = Convert.ToString(entry.URIAGE_TORIHIKI_KBN_CD);
            returnVal.Seikyu_Seisan_Kbn = "2";

            returnVal.Shiharai_Zeikeisan_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KEISAN_KBN_CD);
            returnVal.Shiharai_Zei_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KBN_CD);
            returnVal.Shiharai_Rohiki_Kbn = Convert.ToString(entry.SHIHARAI_TORIHIKI_KBN_CD);
            returnVal.Shiharai_Seisan_Kbn = "2";

            returnVal.Sosatu = "2";
            return returnVal;
        }
        #endregion

        #region 売上支払税計算
        internal void ZeiKeisan(ResultDto<T_UR_SH_ENTRY, T_UR_SH_DETAIL> dto)
        {
            var denpyouHakouPopUpDTO = this.createParameterDTOClass(dto.entry, dto.seikyuu, dto.shiharai);
            decimal URIAGE_AMOUNT_TOTAL = 0;
            decimal SHIHARAI_KINGAKU_TOTAL = 0;
            foreach (var detail in dto.detailList)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    if (!detail.TANKA.IsNull)
                    {
                        detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.seikyuu.KINGAKU_HASUU_CD.IsNull ? 0 : dto.seikyuu.KINGAKU_HASUU_CD.Value);
                    }
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.Value;
                }
                else
                {
                    if (!detail.TANKA.IsNull)
                    {
                        detail.KINGAKU = CommonCalc.FractionCalc((this.ToNDecimal(detail.SUURYOU) ?? 0) * (this.ToNDecimal(detail.TANKA) ?? 0), dto.shiharai.KINGAKU_HASUU_CD.IsNull ? 0 : dto.shiharai.KINGAKU_HASUU_CD.Value);
                    }
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.Value;
                }
            }

            /**
             * 伝票発行画面にて取得したデータ
             */
            // 売上税計算区分CD
            int seikyuZeikeisanKbn = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zeikeisan_Kbn, out seikyuZeikeisanKbn))
            {
                dto.entry.URIAGE_ZEI_KEISAN_KBN_CD = (SqlInt16)seikyuZeikeisanKbn;
            }
            // 売上税区分CD
            int uriageZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn, out uriageZeiKbnCd))
                dto.entry.URIAGE_ZEI_KBN_CD = (SqlInt16)uriageZeiKbnCd;
            // 売上取引区分CD
            int uriageTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Seikyu_Rohiki_Kbn, out uriageTorihikiKbnCd))
            {
                dto.entry.URIAGE_TORIHIKI_KBN_CD = (SqlInt16)uriageTorihikiKbnCd;
            }
            // 支払税計算区分CD
            int shiharaiZeiKeisanKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zeikeisan_Kbn, out shiharaiZeiKeisanKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD = (SqlInt16)shiharaiZeiKeisanKbnCd;
            }
            // 支払税区分CD
            int shiharaiZeiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn, out shiharaiZeiKbnCd))
            {
                dto.entry.SHIHARAI_ZEI_KBN_CD = (SqlInt16)shiharaiZeiKbnCd;
            }
            // 支払取引区分CD
            int ShiharaiTorihikiKbnCd = 0;
            if (int.TryParse(denpyouHakouPopUpDTO.Shiharai_Rohiki_Kbn, out ShiharaiTorihikiKbnCd))
            {
                dto.entry.SHIHARAI_TORIHIKI_KBN_CD = (SqlInt16)ShiharaiTorihikiKbnCd;
            }
            decimal HimeiUrKingakuTotal = 0;
            decimal HimeiShKingakuTotal = 0;
            decimal HinmeiUrTaxSotoTotal = 0;
            decimal HinmeiShTaxSotoTotal = 0;
            decimal HinmeiUrTaxUchiTotal = 0;
            decimal HinmeiShTaxUchiTotal = 0;
            decimal UrTaxSotoTotal = 0;
            decimal ShTaxSotoTotal = 0;
            decimal UrTaxUchiTotal = 0;
            decimal ShTaxUchiTotal = 0;
            foreach (T_UR_SH_DETAIL detail in dto.detailList)
            {
                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(detail.HINMEI_CD);
                detail.HINMEI_ZEI_KBN_CD = hinmei.ZEI_KBN_CD;
                if (detail.HINMEI_ZEI_KBN_CD.IsNull || detail.HINMEI_ZEI_KBN_CD == 0)
                {
                    detail.KINGAKU = detail.KINGAKU;
                    detail.HINMEI_KINGAKU = 0;
                }
                else
                {
                    if (!detail.KINGAKU.IsNull)
                    {
                        detail.HINMEI_KINGAKU = detail.KINGAKU.Value;

                        if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                        {
                            HimeiUrKingakuTotal += detail.KINGAKU.Value;
                        }
                        else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                        {
                            HimeiShKingakuTotal += detail.KINGAKU.Value;
                        }
                    }
                    detail.KINGAKU = 0;
                }

                // 明細毎消費税合計を計算
                // この時点で明細.品名のデータは検索済みなので、品名データ取得処理はしない

                decimal meisaiKingaku = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);

                detail.TAX_SOTO = 0;          // 消費税外税初期値
                detail.TAX_UCHI = 0;          // 消費税内税初期値
                detail.HINMEI_TAX_SOTO = 0;   // 品名別消費税外税初期値
                detail.HINMEI_TAX_UCHI = 0;   // 品名別消費税内税初期値

                decimal detailShouhizeiRate = 0;
                if (!detail.URIAGESHIHARAI_DATE.IsNull)
                {
                    var shouhizeiEntity = this.accessor.GetShouhizeiRate(((DateTime)detail.URIAGESHIHARAI_DATE).Date);
                    if (shouhizeiEntity != null
                        && 0 < shouhizeiEntity.SHOUHIZEI_RATE)
                    {
                        detailShouhizeiRate = (decimal)shouhizeiEntity.SHOUHIZEI_RATE;
                    }
                }

                string CELL_NAME_SHOUHIZEI_RATE = "";
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.URIAGE_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.URIAGE_SHOUHIZEI_RATE.Value.ToString();
                }
                else
                {
                    CELL_NAME_SHOUHIZEI_RATE = dto.entry.SHIHARAI_SHOUHIZEI_RATE.IsNull ? "" : dto.entry.SHIHARAI_SHOUHIZEI_RATE.Value.ToString();
                }

                // もし消費税率が設定されていればそちらを優先して使う
                decimal tempShouhizeiRate = 0;
                if (!string.IsNullOrEmpty(CELL_NAME_SHOUHIZEI_RATE)
                    && decimal.TryParse(CELL_NAME_SHOUHIZEI_RATE, out tempShouhizeiRate))
                {
                    detailShouhizeiRate = tempShouhizeiRate;
                }

                if (SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO: 明細毎消費税合計は品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                HinmeiUrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Seikyu_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                UrTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO
                                    = CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD);

                                break;

                            case ConstClass.ZEI_KBN_2:
                                UrTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.seikyuu.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Seikyu_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.seikyuu.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
                else if (SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI == detail.DENPYOU_KBN_CD)
                {
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull
                        && detail.HINMEI_ZEI_KBN_CD != 0)
                    {
                        // TODO:明細毎消費税合計は 品名.税区分CDがある場合はそれを使って計算するかどうか
                        // 設計Tへ確認

                        switch (detail.HINMEI_ZEI_KBN_CD.ToString())
                        {
                            case ConstClass.ZEI_KBN_1:
                                // 品名別消費税外税
                                detail.HINMEI_TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            case ConstClass.ZEI_KBN_2:
                                // 品名別消費税内税
                                detail.HINMEI_TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.HINMEI_TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.HINMEI_TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                HinmeiShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        detail.HINMEI_ZEI_KBN_CD.ToString());
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
                        // publicにしてもらい、そこを参照すること
                        switch (denpyouHakouPopUpDTO.Shiharai_Zei_Kbn)
                        {
                            case ConstClass.ZEI_KBN_1:
                                ShTaxSotoTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税外
                                detail.TAX_SOTO =
                                    CommonCalc.FractionCalc(
                                        meisaiKingaku * detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD);

                                break;

                            case Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClass.ZEI_KBN_2:
                                ShTaxUchiTotal
                                    += this.CalcTaxForUriageDetial(
                                        meisaiKingaku,
                                        detailShouhizeiRate,
                                        (int)dto.shiharai.TAX_HASUU_CD,
                                        denpyouHakouPopUpDTO.Shiharai_Zei_Kbn);
                                // 消費税内
                                detail.TAX_UCHI = meisaiKingaku - (meisaiKingaku / (detailShouhizeiRate + 1));
                                detail.TAX_UCHI =
                                    CommonCalc.FractionCalc((decimal)detail.TAX_UCHI, (int)dto.shiharai.TAX_HASUU_CD);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }


            dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL = HimeiUrKingakuTotal;
            // entityの値を使って計算するため、処理の最後に計算
            decimal uriageTotal = URIAGE_AMOUNT_TOTAL;
            dto.entry.URIAGE_AMOUNT_TOTAL = uriageTotal - dto.entry.HINMEI_URIAGE_KINGAKU_TOTAL.Value;

            /**
             * 売上の税金系計算
             */
            // 売上伝票毎消費税外税、品名別売上消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                dto.entry.URIAGE_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)(dto.entry.URIAGE_AMOUNT_TOTAL * dto.entry.URIAGE_SHOUHIZEI_RATE),
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_URIAGE_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiUrTaxSotoTotal,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税内税、品名別売上消費税内税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Seikyu_Zei_Kbn))
            {
                // 金額計算
                dto.entry.URIAGE_TAX_UCHI
                    = (dto.entry.URIAGE_AMOUNT_TOTAL
                        - (dto.entry.URIAGE_AMOUNT_TOTAL / (dto.entry.URIAGE_SHOUHIZEI_RATE + 1)));
                // 端数処理
                dto.entry.URIAGE_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.URIAGE_TAX_UCHI,
                        (int)dto.seikyuu.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.URIAGE_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL = HinmeiUrTaxUchiTotal;

            // 端数処理
            dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_URIAGE_TAX_UCHI_TOTAL,
                    (int)dto.seikyuu.TAX_HASUU_CD);

            // 売上伝票毎消費税外税合計
            dto.entry.URIAGE_TAX_SOTO_TOTAL = UrTaxSotoTotal;

            // 売上伝票毎消費税内税合計
            dto.entry.URIAGE_TAX_UCHI_TOTAL = UrTaxUchiTotal;

            // 品名別支払金額合計
            dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL = HimeiShKingakuTotal;
            decimal shiharaiTotal = SHIHARAI_KINGAKU_TOTAL;
            dto.entry.SHIHARAI_AMOUNT_TOTAL = shiharaiTotal - dto.entry.HINMEI_SHIHARAI_KINGAKU_TOTAL.Value;

            /**
             * 支払の税金系計算
             */
            // 支払伝票毎消費税外税、品名別支払消費税外税合計
            // TODO: Shougun.Core.SalesPayment.DenpyouHakou.Const.ConstClassを
            // publicにしてもらい、そこを参照すること
            if (ConstClass.ZEI_KBN_1.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                dto.entry.SHIHARAI_TAX_SOTO
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_AMOUNT_TOTAL * (decimal)dto.entry.SHIHARAI_SHOUHIZEI_RATE,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_SOTO = 0;
            }

            dto.entry.HINMEI_SHIHARAI_TAX_SOTO_TOTAL
                = CommonCalc.FractionCalc(
                    HinmeiShTaxSotoTotal,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払伝票毎消費税内税、品名別支払消費税内税合計
            if (ConstClass.ZEI_KBN_2.Equals(denpyouHakouPopUpDTO.Shiharai_Zei_Kbn))
            {
                // 金額計算
                dto.entry.SHIHARAI_TAX_UCHI
                    = dto.entry.SHIHARAI_AMOUNT_TOTAL
                        - (dto.entry.SHIHARAI_AMOUNT_TOTAL / (dto.entry.SHIHARAI_SHOUHIZEI_RATE + 1));
                // 端数処理
                dto.entry.SHIHARAI_TAX_UCHI
                    = CommonCalc.FractionCalc(
                        (decimal)dto.entry.SHIHARAI_TAX_UCHI,
                        (int)dto.shiharai.TAX_HASUU_CD);
            }
            else
            {
                dto.entry.SHIHARAI_TAX_UCHI = 0;
            }

            // 金額計算
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = HinmeiShTaxUchiTotal;
            // 端数処理
            dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL
                = CommonCalc.FractionCalc(
                    (decimal)dto.entry.HINMEI_SHIHARAI_TAX_UCHI_TOTAL,
                    (int)dto.shiharai.TAX_HASUU_CD);

            // 支払明細毎消費税外税合計
            dto.entry.SHIHARAI_TAX_SOTO_TOTAL = ShTaxSotoTotal;

            // 支払明細毎消費税内税合計
            dto.entry.SHIHARAI_TAX_UCHI_TOTAL = ShTaxUchiTotal;
        }

        /// <summary>
        /// 伝票発行ポップアップ用連携オブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        internal Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass createParameterDTOClass(T_UR_SH_ENTRY entry, M_TORIHIKISAKI_SEIKYUU seikyuu, M_TORIHIKISAKI_SHIHARAI shiharai)
        {
            // 一度画面で選択されている場合を考慮し、formのParameterDTOClassで初期化
            Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass returnVal = new Shougun.Core.SalesPayment.DenpyouHakou.ParameterDTOClass();

            // 新規、修正共通で設定
            returnVal.Uriage_Date = entry.URIAGE_DATE.IsNull ? "" : entry.URIAGE_DATE.Value.Date.ToString();
            returnVal.Shiharai_Date = entry.SHIHARAI_DATE.IsNull ? "" : entry.SHIHARAI_DATE.Value.Date.ToString();

            returnVal.Uriage_Shouhizei_Rate = Convert.ToString(entry.URIAGE_SHOUHIZEI_RATE.Value);
            returnVal.Shiharai_Shouhizei_Rate = Convert.ToString(entry.SHIHARAI_SHOUHIZEI_RATE.Value);

            returnVal.Seikyu_Zeikeisan_Kbn = Convert.ToString(entry.URIAGE_ZEI_KEISAN_KBN_CD);
            returnVal.Seikyu_Zei_Kbn = Convert.ToString(entry.URIAGE_ZEI_KBN_CD);
            returnVal.Seikyu_Rohiki_Kbn = Convert.ToString(entry.URIAGE_TORIHIKI_KBN_CD);
            returnVal.Seikyu_Seisan_Kbn = "2";

            returnVal.Shiharai_Zeikeisan_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KEISAN_KBN_CD);
            returnVal.Shiharai_Zei_Kbn = Convert.ToString(entry.SHIHARAI_ZEI_KBN_CD);
            returnVal.Shiharai_Rohiki_Kbn = Convert.ToString(entry.SHIHARAI_TORIHIKI_KBN_CD);
            returnVal.Shiharai_Seisan_Kbn = "2";

            returnVal.Sosatu = "2";
            return returnVal;
        }
        #endregion
        #endregion

        /// <summary> 条件指定ポップアップ </summary>
        /// <param name="param">param</param>
        internal bool ShowPopUp()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();


                if (dto.denshuKbnCd == "1")
                {
                    #region 受入
                    var popUpForm = new UkeireDenpyouikkatsuPopupForm();

                    if (popUpForm.ShowDialog() == DialogResult.OK)
                    {
                        this.NyuuryokuParam = popUpForm.NyuuryokuParam;

                        if (this.NyuuryokuParam == null)
                        {
                            return false;
                        }

                        foreach (Row dgvRow in this.form.mrDetail.Rows)
                        {
                            if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                            {
                                int i = dgvRow.Index;
                                bool shokuchi = false;

                                if (!NyuuryokuParam.kyotenCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = NyuuryokuParam.kyotenCd.Value;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(NyuuryokuParam.kyotenCd.IsNull ? "" : NyuuryokuParam.kyotenCd.Value.ToString());
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = Convert.ToDateTime(NyuuryokuParam.denpyouDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.uriageDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = Convert.ToDateTime(NyuuryokuParam.uriageDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.shiharaiDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = Convert.ToDateTime(NyuuryokuParam.shiharaiDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = NyuuryokuParam.torihikisakiCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = NyuuryokuParam.torihikisakiCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = NyuuryokuParam.torihikisakiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = NyuuryokuParam.gyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = NyuuryokuParam.gyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = NyuuryokuParam.genbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd, GENBA_CD = NyuuryokuParam.genbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = NyuuryokuParam.genbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = NyuuryokuParam.nioroshiGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.nioroshiGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = NyuuryokuParam.nioroshiGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGenbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = NyuuryokuParam.nioroshiGenbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.nioroshiGyoushaCd, GENBA_CD = NyuuryokuParam.nioroshiGenbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGenbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = NyuuryokuParam.nioroshiGenbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = NyuuryokuParam.eigyouTantoushaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = NyuuryokuParam.eigyouTantoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = NyuuryokuParam.sharyouCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd, SHARYOU_CD = NyuuryokuParam.sharyouCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = NyuuryokuParam.sharyouName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = NyuuryokuParam.shashuCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = NyuuryokuParam.shashuName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = NyuuryokuParam.upnGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = NyuuryokuParam.upnGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = NyuuryokuParam.untenshaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = NyuuryokuParam.untenshaName;
                                }
                                if (!NyuuryokuParam.keitaiKbnCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = NyuuryokuParam.keitaiKbnCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(NyuuryokuParam.keitaiKbnCd.IsNull ? "" : NyuuryokuParam.keitaiKbnCd.Value.ToString());
                                }
                                if (!NyuuryokuParam.daikanKbn.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = NyuuryokuParam.daikanKbn;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(NyuuryokuParam.daikanKbn.IsNull ? "" : NyuuryokuParam.daikanKbn.Value.ToString()));
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestShuruiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = NyuuryokuParam.manifestShuruiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = NyuuryokuParam.manifestShuruiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestTehaiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = NyuuryokuParam.manifestTehaiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = NyuuryokuParam.manifestTehaiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = NyuuryokuParam.denpyouBikou;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.taipyuuBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = NyuuryokuParam.taipyuuBikou;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (dto.denshuKbnCd == "2")
                {
                    #region 出荷
                    var popUpForm = new ShukkaDenpyouikkatsuPopupForm();

                    if (popUpForm.ShowDialog() == DialogResult.OK)
                    {
                        this.NyuuryokuParam = popUpForm.NyuuryokuParam;

                        if (this.NyuuryokuParam == null)
                        {
                            return false;
                        }

                        foreach (Row dgvRow in this.form.mrDetail.Rows)
                        {
                            if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                            {
                                int i = dgvRow.Index;
                                bool shokuchi = false;

                                if (!NyuuryokuParam.kyotenCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = NyuuryokuParam.kyotenCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(NyuuryokuParam.kyotenCd.IsNull ? "" : NyuuryokuParam.kyotenCd.Value.ToString());
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = Convert.ToDateTime(NyuuryokuParam.denpyouDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.uriageDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = Convert.ToDateTime(NyuuryokuParam.uriageDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.shiharaiDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = Convert.ToDateTime(NyuuryokuParam.shiharaiDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = NyuuryokuParam.torihikisakiCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = NyuuryokuParam.torihikisakiCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = NyuuryokuParam.torihikisakiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = NyuuryokuParam.gyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = NyuuryokuParam.gyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = NyuuryokuParam.genbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd, GENBA_CD = NyuuryokuParam.genbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = NyuuryokuParam.genbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value = NyuuryokuParam.nizumiGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.nizumiGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = NyuuryokuParam.nizumiGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGenbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = NyuuryokuParam.nizumiGenbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.nizumiGyoushaCd, GENBA_CD = NyuuryokuParam.nizumiGenbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGenbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = NyuuryokuParam.nizumiGenbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = NyuuryokuParam.eigyouTantoushaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = NyuuryokuParam.eigyouTantoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = NyuuryokuParam.sharyouCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd, SHARYOU_CD = NyuuryokuParam.sharyouCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = NyuuryokuParam.sharyouName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = NyuuryokuParam.shashuCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = NyuuryokuParam.shashuName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = NyuuryokuParam.upnGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = NyuuryokuParam.upnGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = NyuuryokuParam.untenshaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = NyuuryokuParam.untenshaName;
                                }
                                if (!NyuuryokuParam.keitaiKbnCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = NyuuryokuParam.keitaiKbnCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(NyuuryokuParam.keitaiKbnCd.IsNull ? "" : NyuuryokuParam.keitaiKbnCd.Value.ToString());
                                }
                                if (!NyuuryokuParam.daikanKbn.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = NyuuryokuParam.daikanKbn;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(NyuuryokuParam.daikanKbn.IsNull ? "" : NyuuryokuParam.daikanKbn.Value.ToString()));
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestShuruiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = NyuuryokuParam.manifestShuruiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = NyuuryokuParam.manifestShuruiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestTehaiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = NyuuryokuParam.manifestTehaiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = NyuuryokuParam.manifestTehaiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = NyuuryokuParam.denpyouBikou;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.taipyuuBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = NyuuryokuParam.taipyuuBikou;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (dto.denshuKbnCd == "3")
                {
                    #region 売上支払
                    var popUpForm = new UriageDenpyouikkatsuPopupForm();

                    if (popUpForm.ShowDialog() == DialogResult.OK)
                    {
                        this.NyuuryokuParam = popUpForm.NyuuryokuParam;

                        if (this.NyuuryokuParam == null)
                        {
                            return false;
                        }

                        foreach (Row dgvRow in this.form.mrDetail.Rows)
                        {
                            if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].Value == true)
                            {
                                int i = dgvRow.Index;
                                bool shokuchi = false;

                                if (!NyuuryokuParam.kyotenCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_CD].Value = NyuuryokuParam.kyotenCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(NyuuryokuParam.kyotenCd.IsNull ? "" : NyuuryokuParam.kyotenCd.Value.ToString());
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = Convert.ToDateTime(NyuuryokuParam.denpyouDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.uriageDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = Convert.ToDateTime(NyuuryokuParam.uriageDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.shiharaiDate))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = Convert.ToDateTime(NyuuryokuParam.shiharaiDate);
                                }

                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = NyuuryokuParam.torihikisakiCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = NyuuryokuParam.torihikisakiCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.torihikisakiName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = NyuuryokuParam.torihikisakiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = NyuuryokuParam.gyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.gyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = NyuuryokuParam.gyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD].Value = NyuuryokuParam.genbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.gyoushaCd, GENBA_CD = NyuuryokuParam.genbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.genbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_GENBA_NAME].Value = NyuuryokuParam.genbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD].Value = NyuuryokuParam.nizumiGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.nizumiGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GYOUSHA_NAME].Value = NyuuryokuParam.nizumiGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGenbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD].Value = NyuuryokuParam.nizumiGenbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.nizumiGyoushaCd, GENBA_CD = NyuuryokuParam.nizumiGenbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nizumiGenbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIZUMI_GENBA_NAME].Value = NyuuryokuParam.nizumiGenbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = NyuuryokuParam.nioroshiGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.nioroshiGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = NyuuryokuParam.nioroshiGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGenbaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = NyuuryokuParam.nioroshiGenbaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GENBA() { GYOUSHA_CD = NyuuryokuParam.nioroshiGyoushaCd, GENBA_CD = NyuuryokuParam.nioroshiGenbaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.nioroshiGenbaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = NyuuryokuParam.nioroshiGenbaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_CD].Value = NyuuryokuParam.eigyouTantoushaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.eigyouTantoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_EIGYOU_TANTOUSHA_NAME].Value = NyuuryokuParam.eigyouTantoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD].Value = NyuuryokuParam.sharyouCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_SHARYOU() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd, SHARYOU_CD = NyuuryokuParam.sharyouCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.sharyouName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHARYOU_NAME].Value = NyuuryokuParam.sharyouName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_CD].Value = NyuuryokuParam.shashuCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.shashuName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_SHASHU_NAME].Value = NyuuryokuParam.shashuName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = NyuuryokuParam.upnGyoushaCd;
                                    shokuchi = this.accessor.IsShokuchi(new M_GYOUSHA() { GYOUSHA_CD = NyuuryokuParam.upnGyoushaCd });
                                    this.SetShokuchi(this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD], this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME], shokuchi, i);
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.upnGyoushaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = NyuuryokuParam.upnGyoushaName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_CD].Value = NyuuryokuParam.untenshaCd;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.untenshaName))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_UNTENSHA_NAME].Value = NyuuryokuParam.untenshaName;
                                }
                                if (!NyuuryokuParam.keitaiKbnCd.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN_CD].Value = NyuuryokuParam.keitaiKbnCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_KEITAI_KBN].Value = this.accessor.GetKeitaiNameFast(NyuuryokuParam.keitaiKbnCd.IsNull ? "" : NyuuryokuParam.keitaiKbnCd.Value.ToString());
                                }
                                if (!NyuuryokuParam.daikanKbn.IsNull)
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN_CD].Value = NyuuryokuParam.daikanKbn;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DAIKAN_KBN].Value = SalesPaymentConstans.DAIKAN_KBNExt.ToTypeString(SalesPaymentConstans.DAIKAN_KBNExt.ToDaikanKbn(NyuuryokuParam.daikanKbn.IsNull ? "" : NyuuryokuParam.daikanKbn.Value.ToString()));
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestShuruiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_CD].Value = NyuuryokuParam.manifestShuruiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_SHURUI_NAME].Value = NyuuryokuParam.manifestShuruiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.manifestTehaiCd))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_CD].Value = NyuuryokuParam.manifestTehaiCd;
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_MANIFEST_TEHAI_NAME].Value = NyuuryokuParam.manifestTehaiName;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.denpyouBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_DENPYOU_BIKOU].Value = NyuuryokuParam.denpyouBikou;
                                }
                                if (!string.IsNullOrEmpty(NyuuryokuParam.taipyuuBikou))
                                {
                                    this.form.mrDetail.Rows[i].Cells[ConstCls.COLUMN_TAIRYUU_BIKOU].Value = NyuuryokuParam.taipyuuBikou;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowPopUp", ex);
                this.form.MsgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
    }
}